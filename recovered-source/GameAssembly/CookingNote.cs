using System;
using UnityEngine;
using UnityEngine.UI;

public class CookingNote : MonoBehaviour, IPooledPrefab
{
	public struct ConsumeInfo
	{
		public CookingInputConsumeResult ConsumeResult;

		public double Offset;

		public double OffsetAbs;

		public ConsumeInfo(CookingInputConsumeResult consumeResult)
		{
			ConsumeResult = consumeResult;
			Offset = 0.0;
			OffsetAbs = 0.0;
		}

		public ConsumeInfo(double targetTime, double clickTime, double allowedErrorTime)
		{
			Offset = targetTime - clickTime;
			OffsetAbs = Math.Abs(Offset);
			if (OffsetAbs > allowedErrorTime)
			{
				ConsumeResult = ((Offset > 0.0) ? CookingInputConsumeResult.CannotTimeEarly : CookingInputConsumeResult.CannotTimeLate);
			}
			else
			{
				ConsumeResult = CookingInputConsumeResult.Can;
			}
		}
	}

	public Sprite Carrot;

	public Sprite Aubergine;

	public Sprite Miss;

	[SerializeField]
	private Image line;

	[SerializeField]
	private Image completeLine;

	[SerializeField]
	private Image endCircle;

	[SerializeField]
	private Image circle;

	[SerializeField]
	private Image symbol;

	private float _flyOffProgress;

	[SerializeField]
	private double _noteProgress;

	private double _durationNoteProgress;

	private Vector2 _flyOffVariation;

	private NoteClickedScoreEnum _startScore;

	private NoteClickedScoreEnum _endScore;

	public Image Line => line;

	public CookingSongNoteEvent RelatedSongNoteEvent { get; private set; }

	public bool IsDuration => RelatedSongNoteEvent.IsDuration;

	public double DurationNoteProgress => _durationNoteProgress;

	public NoteClickedScoreEnum StartScore
	{
		get
		{
			return _startScore;
		}
		set
		{
			_startScore = value;
		}
	}

	public NoteClickedScoreEnum EndScore
	{
		get
		{
			return _endScore;
		}
		set
		{
			_endScore = value;
			if (CookingGameplayScoreData.IsSuccess(value))
			{
				SetDurationNoteProgress(1.0);
			}
		}
	}

	public bool StartScored => StartScore != NoteClickedScoreEnum.None;

	public bool EndScored => EndScore != NoteClickedScoreEnum.None;

	public bool IsFinished
	{
		get
		{
			if (StartScored)
			{
				if (RelatedSongNoteEvent.IsDuration)
				{
					return EndScored;
				}
				return true;
			}
			return false;
		}
	}

	public GameObject SourcePrefab { get; set; }

	private void SetColors(Color color, Color insideColor)
	{
		line.color = color;
		circle.color = color;
		endCircle.color = color;
		completeLine.color = insideColor;
	}

	public Color GetMainColor()
	{
		return circle.color;
	}

	public void InitializeAsNewNote(CookingSongNoteEvent songNoteEvent, double pixelsPerSecond)
	{
		RelatedSongNoteEvent = songNoteEvent;
		StartScore = NoteClickedScoreEnum.None;
		EndScore = NoteClickedScoreEnum.None;
		RectTransform rectTransform = (RectTransform)transform;
		rectTransform.localScale = Vector3.one;
		rectTransform.anchoredPosition = Vector2.zero;
		rectTransform.pivot = new Vector2(0.5f, 0.5f);
		circle.gameObject.SetActive(value: true);
		if (songNoteEvent.IsDuration)
		{
			line.gameObject.SetActive(value: true);
			completeLine.gameObject.SetActive(value: true);
			endCircle.gameObject.SetActive(value: true);
			float x = (float)(pixelsPerSecond * songNoteEvent.Duration);
			endCircle.transform.AsRectTransform().SetAnchoredPositionX(x);
			line.transform.AsRectTransform().SetSizeDeltaX(x);
			SetDurationNoteProgress(0.0);
		}
		else
		{
			line.gameObject.SetActive(value: false);
			completeLine.gameObject.SetActive(value: false);
			endCircle.gameObject.SetActive(value: false);
		}
		NoteButtonTypeEnum noteButtonType = songNoteEvent.NoteButtonType;
		if (noteButtonType == NoteButtonTypeEnum.Left || noteButtonType != NoteButtonTypeEnum.Right)
		{
			symbol.sprite = Carrot;
			SetColors(CookingGameplayUtilities.LeftClickMainColor, CookingGameplayUtilities.LeftClickSecondColor);
		}
		else
		{
			symbol.sprite = Aubergine;
			SetColors(CookingGameplayUtilities.RightClickMainColor, CookingGameplayUtilities.RightClickSecondColor);
		}
	}

	public ConsumeInfo CanConsumeInput(double allowedErrorTime, CookingGameplayStaticGui.QueuedInput queuedInput)
	{
		if (RelatedSongNoteEvent.IsDuration)
		{
			if (StartScored && EndScored)
			{
				return new ConsumeInfo(CookingInputConsumeResult.CannotFinished);
			}
			if (StartScored)
			{
				ConsumeInfo result = new ConsumeInfo(RelatedSongNoteEvent.EndTime, queuedInput.InputTime, allowedErrorTime);
				if (!queuedInput.InputType.HasFlagFast(CookingInputFlagEnum.Up))
				{
					result.ConsumeResult |= CookingInputConsumeResult.CannotWrongType;
				}
				else if (result.ConsumeResult.HasFlagFast(CookingInputConsumeResult.CannotTimeEarly))
				{
					result.ConsumeResult &= ~CookingInputConsumeResult.CannotTimeEarly;
				}
				if (!CookingGameplayStaticGui.SameButton(queuedInput.InputType, RelatedSongNoteEvent.NoteButtonType))
				{
					result.ConsumeResult |= CookingInputConsumeResult.CannotWrongButton;
				}
				return result;
			}
			ConsumeInfo result2 = new ConsumeInfo(RelatedSongNoteEvent.StartTime, queuedInput.InputTime, allowedErrorTime);
			if (!queuedInput.InputType.HasFlagFast(CookingInputFlagEnum.Down))
			{
				result2.ConsumeResult |= CookingInputConsumeResult.CannotWrongType;
			}
			if (!CookingGameplayStaticGui.SameButton(queuedInput.InputType, RelatedSongNoteEvent.NoteButtonType))
			{
				result2.ConsumeResult |= CookingInputConsumeResult.CannotWrongButton;
			}
			return result2;
		}
		if (StartScored)
		{
			return new ConsumeInfo(CookingInputConsumeResult.CannotFinished);
		}
		ConsumeInfo result3 = new ConsumeInfo(RelatedSongNoteEvent.StartTime, queuedInput.InputTime, allowedErrorTime);
		if (!queuedInput.InputType.HasFlagFast(CookingInputFlagEnum.Down))
		{
			result3.ConsumeResult |= CookingInputConsumeResult.CannotWrongType;
		}
		if (!CookingGameplayStaticGui.SameButton(queuedInput.InputType, RelatedSongNoteEvent.NoteButtonType))
		{
			result3.ConsumeResult |= CookingInputConsumeResult.CannotWrongButton;
		}
		return result3;
	}

	public bool SetNextScore(NoteClickedScoreEnum score)
	{
		if (RelatedSongNoteEvent.IsDuration)
		{
			if (StartScore == NoteClickedScoreEnum.None)
			{
				StartScore = score;
				return false;
			}
			EndScore = score;
			return true;
		}
		NoteClickedScoreEnum startScore = (EndScore = score);
		StartScore = startScore;
		return true;
	}

	public double GetNextClickTime()
	{
		if (StartScored)
		{
			return RelatedSongNoteEvent.EndTime;
		}
		return RelatedSongNoteEvent.StartTime;
	}

	public void StartFlyingOff()
	{
		_flyOffProgress = 0.01f;
		_flyOffVariation = new Vector2(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f));
		if (StartScore == NoteClickedScoreEnum.Miss || EndScore == NoteClickedScoreEnum.Miss)
		{
			symbol.sprite = Miss;
			circle.gameObject.SetActive(value: false);
		}
		UpdateFlyOffProgress();
	}

	public bool UpdateFlyOffProgress()
	{
		_flyOffProgress += Time.deltaTime * 2f;
		float num = Mathf.Pow(1f - _flyOffProgress, 3f);
		float num2 = 1f - num;
		RectTransform rectTransform = (RectTransform)transform;
		_ = (RectTransform)line.transform;
		if (RelatedSongNoteEvent.IsDuration)
		{
			rectTransform.anchoredPosition = 200f * (new Vector2(0f - num2, 0f) + _flyOffVariation * num2);
		}
		else if (RelatedSongNoteEvent.NoteButtonType == NoteButtonTypeEnum.Left)
		{
			rectTransform.anchoredPosition = 200f * (new Vector2(0f - _flyOffProgress, num2) + _flyOffVariation * num2);
		}
		else
		{
			rectTransform.anchoredPosition = 200f * (new Vector2(0f - _flyOffProgress, 0f - num2) + _flyOffVariation * num2);
		}
		rectTransform.localScale = Vector3.one * num;
		return _flyOffProgress > 1f;
	}

	private void SetDurationNoteProgress(double progress)
	{
		if (progress > 1.0)
		{
			progress = 1.0;
		}
		completeLine.transform.AsRectTransform().SetSizeDeltaX((float)((double)line.transform.AsRectTransform().sizeDelta.x * progress));
		_durationNoteProgress = progress;
	}

	public void UpdateNote(double currentTime, double currentLookAheadTime, double lookAheadTimeOffset)
	{
		double num = (currentLookAheadTime - RelatedSongNoteEvent.StartTime) / lookAheadTimeOffset;
		RectTransform rectTransform = (RectTransform)transform;
		Vector2 anchorMax = (rectTransform.anchorMin = new Vector2((float)(1.0 - num), 0.5f));
		rectTransform.anchorMax = anchorMax;
		_noteProgress = num;
		if (RelatedSongNoteEvent.IsDuration && EndScore == NoteClickedScoreEnum.None && CookingGameplayScoreData.IsSuccess(StartScore))
		{
			double durationNoteProgress = (currentTime - RelatedSongNoteEvent.StartTime) / RelatedSongNoteEvent.Duration;
			SetDurationNoteProgress(durationNoteProgress);
		}
	}

	public void AfterReturnedToPool()
	{
	}

	public void BeforeGetFromPool()
	{
	}
}
