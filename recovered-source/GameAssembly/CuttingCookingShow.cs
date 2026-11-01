using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CuttingCookingShow : CookingShow
{
	private enum BotFace
	{
		Neutral,
		Miss,
		Good,
		Fever
	}

	[FormerlySerializedAs("hands")]
	[SerializeField]
	private RectTransform cuttingTarget;

	[SerializeField]
	private Image leftHand;

	[SerializeField]
	private Image rightHand;

	[SerializeField]
	private RectTransform bot;

	[FormerlySerializedAs("cuttableFoodPrefabPool")]
	[FormerlySerializedAs("cuttableFoodPrefab")]
	[SerializeField]
	private PooledPrefabReference<CuttableFood> cuttableFoodPrefabReference;

	[SerializeField]
	private Sprite meatSprite;

	[SerializeField]
	private Sprite carrotSprite;

	[SerializeField]
	private Sprite onionSprite;

	private RectTransform botHead;

	private RectTransform botHandsDown;

	private RectTransform botHandsUp;

	private RectTransform botFever;

	private RectTransform botGood;

	private RectTransform botMiss;

	private RectTransform botNeutral;

	private List<CuttableFood> flyingOffCuttableFood = new List<CuttableFood>();

	private CuttableFood currentlySlicedFood;

	private BotFace _botFace;

	private const float DefaultKnifeRaiseHeight = 100f;

	private const float HandMaxMoveLeft = 50f;

	private const float HandReturnRaise = 25f;

	private CookingGameplayStaticGui staticGui;

	private float _leftHandValue;

	private double _lastCutTime;

	private double _lastBotFaceChange;

	private float _lastVegetableSlicePosition;

	private float _rightHandValue;

	private float _botUp;

	private void Awake()
	{
		botHead = bot.Find("Head").AsRectTransform();
		botHandsDown = bot.Find("Hands Down").AsRectTransform();
		botHandsUp = bot.Find("Hands Up").AsRectTransform();
		botGood = botHead.Find("Good").AsRectTransform();
		botMiss = botHead.Find("Miss").AsRectTransform();
		botNeutral = botHead.Find("Neutral").AsRectTransform();
		botFever = botHead.Find("Fever").AsRectTransform();
	}

	private void SetBotFace(BotFace botFace)
	{
		botGood.gameObject.SetActive(value: false);
		botMiss.gameObject.SetActive(value: false);
		botNeutral.gameObject.SetActive(value: false);
		botFever.gameObject.SetActive(value: false);
		switch (botFace)
		{
		default:
			botNeutral.gameObject.SetActive(value: true);
			break;
		case BotFace.Miss:
			botMiss.gameObject.SetActive(value: true);
			break;
		case BotFace.Good:
			botGood.gameObject.SetActive(value: true);
			break;
		case BotFace.Fever:
			botFever.gameObject.SetActive(value: true);
			break;
		}
		_botFace = botFace;
	}

	private void SetBotFaceToScore(NoteClickedScoreEnum clickedScoreEnum)
	{
		_lastBotFaceChange = staticGui.CurrentTime;
		switch (clickedScoreEnum)
		{
		default:
			SetBotFace(BotFace.Neutral);
			break;
		case NoteClickedScoreEnum.Great:
		case NoteClickedScoreEnum.Good:
		case NoteClickedScoreEnum.Bad:
			if (staticGui.IsFever)
			{
				SetBotFace(BotFace.Fever);
			}
			else
			{
				SetBotFace(BotFace.Good);
			}
			break;
		case NoteClickedScoreEnum.WrongNote:
		case NoteClickedScoreEnum.Miss:
			SetBotFace(BotFace.Miss);
			break;
		}
	}

	private void ResetEverything()
	{
		staticGui = null;
		SetBotFace(BotFace.Neutral);
		_botUp = 0f;
		_rightHandValue = 0f;
		_lastVegetableSlicePosition = 0f;
		_lastCutTime = -1.0;
		PrefabPool<CuttableFood> pool = cuttableFoodPrefabReference.GetPool();
		pool.InitOrResetPooledList(ref flyingOffCuttableFood);
		if (currentlySlicedFood != null)
		{
			pool.ReturnToPool(currentlySlicedFood);
			currentlySlicedFood = null;
		}
	}

	public override void StartShow(CookingGameplayStaticGui _staticGui)
	{
		ResetEverything();
		staticGui = _staticGui;
	}

	public override void EndShow()
	{
		ResetEverything();
	}

	private void LeftHandFeed(float value)
	{
		value = Mathf.Clamp01(value);
		leftHand.transform.AsRectTransform().anchoredPosition = new Vector2(-50f * value, 0f);
	}

	private void LeftHandReturn(float value)
	{
		value = Mathf.Clamp01(value);
		leftHand.transform.AsRectTransform().anchoredPosition = new Vector2(-50f * value, Mathf.Sin((float)Math.PI * value) * 25f);
	}

	private bool TryGetNextNoteInfo(List<CookingNote> activeNotes, out CookingNote activeNote)
	{
		for (int i = 0; i < activeNotes.Count; i++)
		{
			activeNote = activeNotes[i];
			if (!activeNote.IsFinished)
			{
				return true;
			}
		}
		activeNote = null;
		return false;
	}

	public override void UpdateShow(double beatCloseness, List<CookingNote> nextNoteInfo)
	{
		double currentTime = staticGui.CurrentTime;
		float num = (float)beatCloseness;
		cuttableFoodPrefabReference.GetPool().UpdatePooledListAndRemoveFinished(flyingOffCuttableFood, (CuttableFood cuttableFood) => cuttableFood.UpdateFlyingOff());
		cuttingTarget.localScale = Vector3.one * (1f + num * 0.05f);
		UpdateBot(currentTime, num);
		bool flag = TryGetNextNoteInfo(nextNoteInfo, out var activeNote);
		if (flag)
		{
			if (currentlySlicedFood == null)
			{
				currentlySlicedFood = cuttableFoodPrefabReference.GetPool().GetFromPool(cuttingTarget);
				switch (UnityEngine.Random.Range(0, 3))
				{
				case 0:
					currentlySlicedFood.InitCutting(meatSprite);
					break;
				case 1:
					currentlySlicedFood.InitCutting(carrotSprite);
					break;
				default:
					currentlySlicedFood.InitCutting(onionSprite);
					break;
				}
				currentlySlicedFood.transform.SetAsFirstSibling();
				_lastVegetableSlicePosition = 100f;
				currentlySlicedFood.rectTransform.SetAnchoredPositionX(_lastVegetableSlicePosition);
			}
			float num2 = Mathf.InverseLerp((float)_lastCutTime, (float)activeNote.GetNextClickTime(), (float)currentTime);
			if (num2 < 0.5f)
			{
				float num3 = 1f - num2 * 2f;
				_leftHandValue = (_leftHandValue + num3) * 0.5f;
				LeftHandReturn(_leftHandValue);
			}
			else
			{
				float num4 = EasingFunction.EaseOutCubic(0f, 1f, (num2 - 0.5f) * 2f);
				LeftHandFeed(num4);
				RectTransform rectTransform = currentlySlicedFood.rectTransform;
				float x = rectTransform.sizeDelta.x;
				float num5 = currentlySlicedFood.cutX1 * x;
				float num6 = currentlySlicedFood.cutX2 * x - num5;
				float num7 = ((!activeNote.IsDuration) ? Mathf.Min(num6 / 2f, 60f) : (num6 / 2f));
				float b = 0f - num7 - num5;
				rectTransform.SetAnchoredPositionX(Mathf.Lerp(_lastVegetableSlicePosition, b, num4));
			}
		}
		else
		{
			if (_leftHandValue > 0f)
			{
				_leftHandValue -= Time.deltaTime * 5f;
			}
			LeftHandReturn(_leftHandValue);
		}
		if (flag && activeNote.RelatedSongNoteEvent.IsDuration && activeNote.StartScored)
		{
			float num8 = (float)activeNote.DurationNoteProgress;
			float num9 = num8 * num8;
			_rightHandValue = num9 * -1f;
			if (_rightHandValue > 0.5f)
			{
				rightHand.rectTransform.SetAnchoredPositionX(UnityEngine.Random.Range(-30f, 30f) * num9);
			}
		}
		else
		{
			rightHand.rectTransform.SetAnchoredPositionX(0f);
			_rightHandValue = GlobalFunctions.MoveTowards(_rightHandValue, 0f, Time.deltaTime * 5f);
		}
		float num10 = ((!(currentlySlicedFood != null)) ? 100f : currentlySlicedFood.rectTransform.sizeDelta.y);
		rightHand.transform.AsRectTransform().SetAnchoredPositionY(num10 - _rightHandValue * num10);
	}

	private void UpdateBot(double currentTime, float beatClosenessFloat)
	{
		bot.SetAnchoredPositionY(beatClosenessFloat * 5f + _botUp * 10f);
		if (_botUp > 0f)
		{
			_botUp -= Time.deltaTime * 5f;
		}
		if (staticGui.IsFever)
		{
			botHandsDown.gameObject.SetActive(value: false);
			botHandsUp.gameObject.SetActive(value: true);
			botHandsUp.SetAnchoredPositionY(beatClosenessFloat * 10f);
			botHandsUp.localRotation = Quaternion.Euler(0f, (beatClosenessFloat - 0.5f) * 10f, beatClosenessFloat * 10f);
		}
		else
		{
			botHandsDown.gameObject.SetActive(value: true);
			botHandsUp.gameObject.SetActive(value: false);
			botHandsDown.SetAnchoredPositionY(beatClosenessFloat * 10f);
		}
		if (_botFace != BotFace.Neutral && currentTime - _lastBotFaceChange > 1.0)
		{
			_lastBotFaceChange = currentTime;
			SetBotFace(_botFace);
		}
	}

	public override void Score(NoteClickedScoreEnum clickedScoreEnum, CookingNote relatedNote)
	{
		SetBotFaceToScore(clickedScoreEnum);
		_botUp = UnityEngine.Random.Range(0.8f, 1.1f);
		if (!CookingGameplayScoreData.IsSuccess(clickedScoreEnum) || !(currentlySlicedFood != null))
		{
			return;
		}
		RectTransform rectTransform = currentlySlicedFood.rectTransform;
		_lastVegetableSlicePosition = rectTransform.anchoredPosition.x;
		if (relatedNote.IsFinished)
		{
			_rightHandValue = 1f;
			_lastCutTime = staticGui.CurrentTime;
			float num = Mathf.Clamp01((0f - _lastVegetableSlicePosition) / rectTransform.sizeDelta.x);
			Sprite sprite = currentlySlicedFood.Sprite;
			currentlySlicedFood.CutAt(num, out var sliceStart);
			if (num - sliceStart > 0f)
			{
				CuttableFood fromPool = cuttableFoodPrefabReference.GetPool().GetFromPool(cuttingTarget);
				fromPool.rectTransform.anchoredPosition = currentlySlicedFood.rectTransform.anchoredPosition;
				fromPool.InitFlyingOff(sprite, sliceStart, num);
				flyingOffCuttableFood.Add(fromPool);
			}
			if (num > 0.85f || relatedNote.IsDuration)
			{
				currentlySlicedFood.InitFlyingOff(sprite, num, 1f);
				flyingOffCuttableFood.Add(currentlySlicedFood);
				currentlySlicedFood = null;
			}
		}
	}
}
