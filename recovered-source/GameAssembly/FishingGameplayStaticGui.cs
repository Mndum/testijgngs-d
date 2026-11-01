using System;
using System.Text;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameItems;
using IC.AutomaticReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingGameplayStaticGui : StaticGuiBase
{
	private enum FloatState
	{
		None,
		Normal,
		Submerged,
		FishOnTight,
		FishOnLoose
	}

	private enum DuelCharacterEmoteEnum
	{
		Normal,
		Smile,
		Angry
	}

	[SerializeField]
	private SpriteSerializableAutomaticReference floatNormalSprite;

	[SerializeField]
	private SpriteSerializableAutomaticReference floatSubmergedSprite;

	[SerializeField]
	private SpriteSerializableAutomaticReference fishOnTightSprite;

	[SerializeField]
	private SpriteSerializableAutomaticReference fishOnLooseSprite;

	private CancellationTokenSource _fishingMinigameCancellation = new CancellationTokenSource();

	private FishingResults _fishingResults;

	private Action<FishingResults> _onFinishedAction;

	private const float FishingLengthSecondsRealPerInGameHour = 45f;

	public const float TryGetBiteMinLength = 0.05f;

	public const float TryGetBiteMaxLength = 0.015f;

	[Header("Auto")]
	[SerializeField]
	private RectTransform A_Fishing_Info_Window;

	[SerializeField]
	private TextMeshProUGUI A_Fishing_Progress_Up_Info;

	[SerializeField]
	private Scrollbar A_Fishing_Progress;

	[SerializeField]
	private TextMeshProUGUI A_Fishing_Progress_Down_Info;

	[SerializeField]
	private TextMeshProUGUI A_Fishing_Gauge_Up_Info;

	[SerializeField]
	private RectTransform A_Fishing_Gauge;

	[SerializeField]
	private RectTransform A_Fishing_Gauge_Rod;

	[SerializeField]
	private RectTransform A_Fishing_Gauge_Fish;

	[SerializeField]
	private TextMeshProUGUI A_Fishing_Gauge_Down_Info;

	[SerializeField]
	private RectTransform A_Fishing_Time_Window;

	[SerializeField]
	private TextMeshProUGUI A_Time_Text;

	[SerializeField]
	private Slider A_Time_Slider;

	[SerializeField]
	private RectTransform A_Fishing_Display_Window;

	[SerializeField]
	private Image A_Water_Image;

	[SerializeField]
	private Image A_Float_Image;

	[SerializeField]
	private TextMeshProUGUI A_Fishing_Info_Text;

	[SerializeField]
	private RectTransform A_Floating_Text_Source;

	[SerializeField]
	private RectTransform A_Fishing_Duel_Window;

	[SerializeField]
	private TextMeshProUGUI A_Duel_Your_Score_Text;

	[SerializeField]
	private TextMeshProUGUI A_Duel_Current_Winner_Text;

	[SerializeField]
	private TextMeshProUGUI A_Duel_Enemy_Score_Text;

	[SerializeField]
	private RectTransform A_Duel_BarArea;

	[SerializeField]
	private Image A_Duel_BarCenter;

	[SerializeField]
	private Image A_Duel_EnemyBar;

	[SerializeField]
	private Image A_Duel_YourBar;

	[SerializeField]
	private Slider A_Fishing_Throw;

	[SerializeField]
	private RectTransform A_Throw_Slide_Area;

	[SerializeField]
	private RectTransform A_Okay;

	[SerializeField]
	private RectTransform A_Good;

	[SerializeField]
	private RectTransform A_Perfect;

	private void SetFloatState(FloatState floatState)
	{
		switch (floatState)
		{
		case FloatState.None:
			A_Float_Image.gameObject.SetActive(value: false);
			A_Float_Image.sprite = null;
			return;
		case FloatState.Normal:
			A_Float_Image.sprite = floatNormalSprite.Get();
			break;
		case FloatState.Submerged:
			A_Float_Image.sprite = floatSubmergedSprite.Get();
			break;
		case FloatState.FishOnTight:
			A_Float_Image.sprite = fishOnTightSprite.Get();
			break;
		case FloatState.FishOnLoose:
			A_Float_Image.sprite = fishOnLooseSprite.Get();
			break;
		}
		A_Float_Image.gameObject.SetActive(value: true);
	}

	private async UniTask<float> ThrowFloatMinigame(UniTask timerTask, FishingGameStats fishingGameStats, CancellationToken cancellationToken)
	{
		A_Fishing_Throw.gameObject.SetActive(value: true);
		A_Fishing_Info_Text.text = LOC.UI.ThrowMinigameExplain;
		float height = A_Throw_Slide_Area.GetLocalRect().height;
		A_Okay.SetSizeDeltaY(height * 0.45f);
		A_Good.SetSizeDeltaY(height * 0.3f);
		A_Perfect.SetSizeDeltaY(height * 0.15f);
		A_Fishing_Throw.value = 0f;
		try
		{
			bool movingUp = false;
			float value = 0f;
			while (!timerTask.Status.IsCompleted())
			{
				await UniTask.NextFrame(cancellationToken);
				if (movingUp)
				{
					value += Time.deltaTime * 3f;
					if (value >= 1f)
					{
						movingUp = false;
						value = 1f;
					}
				}
				else
				{
					value -= Time.deltaTime * 3f * 2f;
					if (value <= 0f)
					{
						movingUp = true;
						value = 0f;
					}
				}
				A_Fishing_Throw.value = value * value;
				if (Input.anyKeyDown)
				{
					R.Sound.FishingMinigame_Throw_All.GetRandom().Get().PlaySlightlyRandomVolume();
					await UniTask.WaitForSeconds(UnityEngine.Random.Range(0.4f, 0.6f), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
					R.Sound.FishingMinigame_FloatIn_All.GetRandom().Get().PlaySlightlyRandomVolume();
					SetFloatState(FloatState.Normal);
					return A_Fishing_Throw.value;
				}
			}
		}
		finally
		{
			A_Fishing_Throw.gameObject.SetActive(value: false);
			A_Fishing_Info_Text.text = "";
		}
		return 0f;
	}

	private async UniTask<bool> NothingHappening(UniTask timerTask, FishingGameStats fishingGameStats, CancellationToken cancellationToken)
	{
		A_Fishing_Info_Text.text = LOC.UI.ClickAnywhereToStartFishing;
		try
		{
			while (!timerTask.Status.IsCompleted())
			{
				await UniTask.NextFrame(cancellationToken);
				if (Input.anyKeyDown)
				{
					return true;
				}
			}
		}
		finally
		{
			A_Fishing_Info_Text.text = "";
		}
		return false;
	}

	private async UniTask<FishingBite> WaitingForBites(UniTask timerTask, float value, FishingGameStats fishingGameStats, CancellationToken cancellationToken)
	{
		SetFloatState(FloatState.Normal);
		float time = 0f;
		float targetTime = UnityEngine.Random.Range(0.05f, 0.015f);
		UiOverlay.Instance.ShowFloatingText(A_Floating_Text_Source.position, LOC.UI.Fishing_WaitForBitesInfo);
		while (!timerTask.Status.IsCompleted())
		{
			time += Time.deltaTime;
			if (Input.anyKeyDown)
			{
				R.Sound.FishingMinigame_FishFight_All.PlayRandom();
				SetFloatState(FloatState.None);
				UiOverlay.Instance.ShowFloatingText(A_Floating_Text_Source.position, LOC.UI.FishNothingCaught);
				return null;
			}
			if (time > targetTime)
			{
				if (_fishingResults.FishingStartInfo.FishingLocation.TryGetBite(targetTime, value, fishingGameStats, out var result))
				{
					return result;
				}
				targetTime += UnityEngine.Random.Range(0.05f, 0.015f);
			}
			await UniTask.NextFrame(cancellationToken);
		}
		return null;
	}

	private async UniTask FishBiteFishLogic(CancellationToken cancellationToken)
	{
		while (true)
		{
			R.Sound.FishingMinigame_Bite_All.GetRandom().Get().PlaySlightlyRandomVolume();
			SetFloatState(FloatState.Submerged);
			await UniTask.WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			SetFloatState(FloatState.Normal);
			await UniTask.WaitForSeconds(UnityEngine.Random.Range(0.5f, 2f), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		}
	}

	private async UniTask<bool> FishBite(FishingBite fishOnHook, FishingGameStats fishingGameStats, CancellationToken cancellationToken)
	{
		float maxWait = UnityEngine.Random.Range(1.25f, 5f);
		float timer = 0f;
		using CancellationTokenSource fishLogicCancellation = new CancellationTokenSource();
		FishBiteFishLogic(fishLogicCancellation.Token).Forget();
		try
		{
			while (timer < maxWait)
			{
				await UniTask.NextFrame(cancellationToken);
				timer += Time.deltaTime;
				if (Input.anyKeyDown)
				{
					return true;
				}
			}
			UiOverlay.Instance.ShowFloatingText(A_Floating_Text_Source.position, LOC.UI.FishBiteMissed);
			SetFloatState(FloatState.Normal);
			R.Sound.FishingMinigame_FishFight_All.GetRandom().Get().PlaySlightlyRandomVolume();
			return false;
		}
		finally
		{
			fishLogicCancellation.Cancel();
		}
	}

	private async UniTask<bool> FishReelingIn(FishingBite fishInfo, FishingGameStats fishingGameStats, CancellationToken cancellationToken)
	{
		int num6 = 1;
		try
		{
			if (fishInfo.FishStrengthApparentWeightKg > fishingGameStats.FishingRodMaxEffectiveWeight)
			{
				UiOverlay.Instance.ShowFloatingText(A_Floating_Text_Source.position, LOC.UI.HeavyFishWarning);
				await UniTask.WaitForSeconds(0.01f, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			UiOverlay.Instance.ShowFloatingText(A_Floating_Text_Source.position, LOC.UI.Fishing_MainMinigameInfo);
			A_Fishing_Gauge_Up_Info.SetText(LOC.UI.FishingMinigame_InfoAboveFishingGauge);
			A_Fishing_Gauge_Down_Info.SetText(LOC.UI.FishingMinigame_InfoBelowFishingGauge);
			A_Fishing_Progress_Up_Info.SetText(LOC.UI.FishingMinigame_InfoAboveFishingProgress);
			A_Fishing_Progress_Down_Info.SetText(LOC.UI.FishingMinigame_InfoBelowFishingProgress);
			A_Fishing_Info_Window.gameObject.SetActive(value: true);
			float fishPosition = UnityEngine.Random.value;
			float fishPositionTarget = UnityEngine.Random.value;
			float fishSpeedPercent = 0f;
			float fishSpeedPercentTarget = 0.5f;
			float fishTiredness = 0f;
			float fishingRodPosition = fishPosition;
			float fishingRodSpeed = 0f;
			FishingGameStats.BasicFishingGameplayStats gameplayStats = fishingGameStats.GetBasicGameplayStats();
			float fishingGaugeHeight = A_Fishing_Gauge.sizeDelta.y;
			A_Fishing_Gauge_Rod.SetSizeDeltaY(fishingGaugeHeight * gameplayStats.FishingRodSize);
			A_Fishing_Progress.size = 0.3f;
			AudioSource _reelSoundSource;
			using (SingletonMonoBehaviour<SoundSingleton>.Instance.GetFreeAudioSource(out _reelSoundSource))
			{
				_reelSoundSource.clip = R.Sound.FishingMinigame_Reel_ReelLoop.Get();
				_reelSoundSource.loop = true;
				_reelSoundSource.Play();
				float reelSpeed = 0f;
				int fishCaughtCount = GameVariables.Current.fishingMinigameManager.FishCaughtCount;
				float fishDifficultyEasierMultiplier = Mathf.Lerp(0.5f, 1f, Mathf.Clamp01((float)fishCaughtCount / 14f));
				while (true)
				{
					float num = fishInfo.GetFishDifficulty01(fishTiredness, gameplayStats.FishingRodMaxEffectiveWeight) * fishDifficultyEasierMultiplier;
					if (Mathf.Abs(fishPosition - fishingRodPosition) <= gameplayStats.FishingRodSize * 0.5f)
					{
						SetFloatState(FloatState.FishOnTight);
						A_Fishing_Progress.size += Time.deltaTime * gameplayStats.FishingRodFishReelingSpeed;
						reelSpeed = Mathf.MoveTowards(reelSpeed, 1f, Time.deltaTime);
					}
					else
					{
						SetFloatState(FloatState.FishOnLoose);
						A_Fishing_Progress.size -= Time.deltaTime * gameplayStats.ReelingSpeedNegative(num);
						reelSpeed = Mathf.MoveTowards(reelSpeed, 0f, 3f * Time.deltaTime);
					}
					if (Mathf.Approximately(A_Fishing_Progress.size, 1f))
					{
						R.Sound.FishingMinigame_FishPullOut_All.GetRandom().Get().PlaySlightlyRandomVolume();
						return true;
					}
					if (Mathf.Approximately(A_Fishing_Progress.size, 0f))
					{
						break;
					}
					_reelSoundSource.volume = reelSpeed;
					_reelSoundSource.pitch = 0.97f + reelSpeed * 0.03f;
					if (reelSpeed < 0.1f)
					{
						if (_reelSoundSource.isPlaying)
						{
							_reelSoundSource.Pause();
						}
					}
					else if (!_reelSoundSource.isPlaying)
					{
						_reelSoundSource.UnPause();
					}
					float num2 = GlobalFunctions.Lerp3(0.18f, 0.45f, 1.4f, num);
					fishPosition = Mathf.MoveTowards(fishPosition, fishPositionTarget, fishSpeedPercent * num2 * Time.deltaTime);
					switch (fishInfo.BehaviourType)
					{
					default:
						if (Mathf.Approximately(fishPosition, fishPositionTarget))
						{
							fishPositionTarget += UnityEngine.Random.Range(-0.3f, 0.3f);
							fishPositionTarget = Mathf.Repeat(fishPositionTarget, 1f);
						}
						fishSpeedPercentTarget = UnityEngine.Random.Range(0.6f, 1f);
						fishSpeedPercent = Mathf.MoveTowards(fishSpeedPercent, fishSpeedPercentTarget, 5f * Time.deltaTime);
						break;
					case FishingCatchBehaviourType.Fighter:
						if (reelSpeed > 0.05f)
						{
							if (Mathf.Approximately(fishPosition, fishPositionTarget))
							{
								fishPositionTarget = ((!(fishingRodPosition > fishPosition)) ? (fishingRodPosition + gameplayStats.FishingRodSize * 0.5f + 0.05f) : (fishingRodPosition - gameplayStats.FishingRodSize * 0.5f - 0.05f));
								if (fishPositionTarget > 1f)
								{
									fishPositionTarget = 1f - gameplayStats.FishingRodSize - 0.05f;
								}
								else if (fishPositionTarget < 0f)
								{
									fishPositionTarget = gameplayStats.FishingRodSize + 0.05f;
								}
							}
							float num3;
							fishSpeedPercentTarget = (num3 = 0.8f);
							fishSpeedPercent = num3;
						}
						else
						{
							if (Mathf.Approximately(fishPosition, fishPositionTarget))
							{
								float num4 = fishingRodPosition - gameplayStats.FishingRodSize * 0.5f;
								float num5 = fishingRodPosition + gameplayStats.FishingRodSize * 0.5f;
								if (fishPosition < num4)
								{
									fishPositionTarget = UnityEngine.Random.Range(0f, num4);
								}
								else
								{
									fishPositionTarget = ((!(fishPosition > num5)) ? UnityEngine.Random.value : UnityEngine.Random.Range(num5, 1f));
								}
							}
							fishSpeedPercentTarget = 0.5f;
							fishSpeedPercent = Mathf.MoveTowards(fishSpeedPercent, fishSpeedPercentTarget, 5f * Time.deltaTime);
						}
						break;
					case FishingCatchBehaviourType.Rock:
					{
						fishPositionTarget = 0.05f;
						float num3;
						fishSpeedPercentTarget = (num3 = 0.2f);
						fishSpeedPercent = num3;
						break;
					}
					case FishingCatchBehaviourType.Floater:
						if (Mathf.Approximately(fishPosition, fishPositionTarget))
						{
							fishPositionTarget += UnityEngine.Random.Range(-0.5f, 0.5f);
							fishPositionTarget = Mathf.Repeat(fishPositionTarget, 1f);
							fishSpeedPercentTarget = ((fishPositionTarget > fishPosition) ? 1f : 0.7f);
						}
						fishSpeedPercent = Mathf.MoveTowards(fishSpeedPercent, fishSpeedPercentTarget, 5f * Time.deltaTime);
						break;
					case FishingCatchBehaviourType.Sinker:
						if (Mathf.Approximately(fishPosition, fishPositionTarget))
						{
							fishPositionTarget += UnityEngine.Random.Range(-0.5f, 0.5f);
							fishPositionTarget = Mathf.Repeat(fishPositionTarget, 1f);
							fishSpeedPercentTarget = ((fishPositionTarget > fishPosition) ? 0.7f : 1.5f);
						}
						fishSpeedPercent = Mathf.MoveTowards(fishSpeedPercent, fishSpeedPercentTarget, 5f * Time.deltaTime);
						break;
					}
					float target = (Input.anyKey ? 1f : (-1f));
					fishingRodSpeed = Mathf.MoveTowards(fishingRodSpeed, target, gameplayStats.FishingRodAcceleration * Time.deltaTime);
					fishingRodPosition += fishingRodSpeed * Time.deltaTime;
					fishingRodPosition = Mathf.Clamp(fishingRodPosition, gameplayStats.FishingRodSize * 0.5f, 1f - gameplayStats.FishingRodSize * 0.5f);
					A_Fishing_Gauge_Rod.SetAnchoredPositionY(fishingGaugeHeight * fishingRodPosition);
					A_Fishing_Gauge_Fish.SetAnchoredPositionY(fishingGaugeHeight * fishPosition);
					fishTiredness += Time.deltaTime * 2f;
					await UniTask.NextFrame(cancellationToken);
				}
				R.Sound.FishingMinigame_FishPullOut_All.GetRandom().Get().PlaySlightlyRandomVolume();
				return false;
			}
		}
		finally
		{
			SetFloatState(FloatState.None);
			A_Fishing_Info_Window.gameObject.SetActive(value: false);
		}
	}

	private async UniTask Timer(CancellationToken timerCancellation)
	{
		A_Fishing_Time_Window.gameObject.SetActive(value: true);
		float fishingLengthSecondsPerMinute = 0.75f;
		int length = _fishingResults.FishingStartInfo.FishingLengthMinutes;
		int time = GameVariables.Current.time + length;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.Append(LOC.UI.Fishing_TimeEndsAt(GameVariables.TimeToHourString(time)));
			A_Time_Text.SetText(stringBuilder);
		}
		for (int i = 0; i < length; i++)
		{
			float timer = 0f;
			float thisValue = (float)i / (float)(length - 1);
			float nextValue = (float)(i + 1) / (float)(length - 1);
			SingletonLoadableMonoBehaviour<GameScript>.Instance.PassMinute(suppressEvents: true, GameVariables.StaminaSyncType.Normal, sound: true);
			while (timer < fishingLengthSecondsPerMinute)
			{
				timer += Time.deltaTime;
				A_Time_Slider.value = Mathf.Lerp(thisValue, nextValue, timer / fishingLengthSecondsPerMinute);
				await UniTask.NextFrame(timerCancellation);
			}
		}
		A_Time_Text.SetText(LOC.UI.Fishing_TimeEnd);
		A_Time_Slider.value = 1f;
	}

	private void PrepareFishingUi(bool showDuelWindow)
	{
		A_Fishing_Info_Window.gameObject.SetActive(value: false);
		A_Fishing_Display_Window.gameObject.SetActive(value: true);
		A_Fishing_Info_Text.text = "";
		A_Fishing_Throw.gameObject.SetActive(value: false);
		A_Fishing_Duel_Window.gameObject.SetActive(showDuelWindow);
		SetFloatState(FloatState.None);
	}

	private async UniTask<FishingBite> FishingFullIteration(FishingStartInfo fishingStartInfo, FishingGameStats fishingGameStats, UniTask timerTask, CancellationToken cancellationToken)
	{
		if (!(await NothingHappening(timerTask, fishingGameStats, cancellationToken)))
		{
			return null;
		}
		FishingBite fishOnHook = await WaitingForBites(timerTask, await ThrowFloatMinigame(timerTask, fishingGameStats, cancellationToken), fishingGameStats, cancellationToken);
		if (fishOnHook == null)
		{
			return null;
		}
		if (!(await FishBite(fishOnHook, fishingGameStats, cancellationToken)))
		{
			return null;
		}
		if (!(await FishReelingIn(fishOnHook, fishingGameStats, cancellationToken)))
		{
			return null;
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			foreach (Item item in fishOnHook.GetItems())
			{
				FishRegisteringFlagsEnum fishRegisteringFlagsEnum = GameVariables.Current.fishingMinigameManager.RegisterCaughtFish(fishingStartInfo.FishingLocation, item);
				item.GetNameStringBuilder(stringBuilder);
				fishRegisteringFlagsEnum.AppendLocalizedString(stringBuilder);
				UiOverlay.Instance.ShowFloatingText(A_Floating_Text_Source.position, stringBuilder);
				_fishingResults.AddItem(item, fishRegisteringFlagsEnum);
				await UniTask.WaitForSeconds(0.1f, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			return fishOnHook;
		}
	}

	private FishingGameStats GetFishingGameStats(FishingStartInfo fishingStartInfo)
	{
		if (fishingStartInfo.FishingLocation.IsImaginary)
		{
			FishingGameStats fishingGameStats = new FishingGameStats();
			fishingGameStats.SetFishingRod(new Item(ItemEnum.FiberGlassFishingRod));
			Item item = new Item(ItemEnum.FishChunksBait);
			fishingGameStats.SetBait(item, item.GetItemPrefab().FishingBaitItemPrefabData);
			return fishingGameStats;
		}
		return GameVariables.Current.GetGameStats().FishingGameStats;
	}

	private async UniTask FishingMinigameNormal(FishingStartInfo fishingStartInfo, CancellationToken cancellationToken)
	{
		_fishingResults = FishingResults.CreateNormal(fishingStartInfo);
		PrepareFishingUi(showDuelWindow: false);
		FishingGameStats fishingGameStats = GetFishingGameStats(fishingStartInfo);
		UniTask timerTask = Timer(cancellationToken);
		do
		{
			FishingBite fishingBite = await FishingFullIteration(fishingStartInfo, fishingGameStats, timerTask, cancellationToken);
			if (fishingBite == null)
			{
				continue;
			}
			foreach (Item item in fishingBite.GetItems())
			{
				GameVariables.Current.itemManager.AddItem(item);
			}
		}
		while (!timerTask.Status.IsCompleted());
		timerTask.Forget();
		A_Fishing_Info_Text.text = LOC.UI.FishingDone;
		await UniTask.WaitForSeconds(0.5f, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		OnFinishedFishing(_fishingResults);
	}

	private async UniTask DuelScoreTask(FishingResults fishingResults, CancellationToken cancellationToken)
	{
		string winnerYouText = LOC.UI.FishingDuelWinnerYou;
		string winnerEnemyText = LOC.UI.FishingDuelWinnerEnemy;
		FishingDuelResults fishingDuelResults = fishingResults.GetDuelResultsOrErr();
		int lastPlayerScore = -1;
		int lastEnemyScore = -1;
		SetBarAnchorsImmediate(0.5f);
		UpdateTexts(0, 0);
		while (!cancellationToken.IsCancellationRequested)
		{
			int playerScore = fishingDuelResults.PlayerScore;
			int enemyScore = fishingDuelResults.EnemyScore;
			if (playerScore != lastPlayerScore || enemyScore != lastEnemyScore)
			{
				TweenBars(fishingDuelResults.GetCurrentRatio());
				A_Fishing_Duel_Window.DOShakePosition(0.25f, 8f, 20);
				UpdateTexts(playerScore, enemyScore);
				lastPlayerScore = playerScore;
				lastEnemyScore = enemyScore;
			}
			await UniTask.NextFrame(cancellationToken);
		}
		DOTween.Kill(A_Duel_EnemyBar.rectTransform);
		DOTween.Kill(A_Duel_YourBar.rectTransform);
		DOTween.Kill(A_Fishing_Duel_Window);
		SetBarAnchorsImmediate(fishingDuelResults.GetCurrentRatio());
		UpdateTexts(fishingDuelResults.PlayerScore, fishingDuelResults.EnemyScore);
		void UpdateTexts(int num, int num2)
		{
			A_Duel_Your_Score_Text.SetText(num);
			A_Duel_Enemy_Score_Text.SetText(num2);
			if (num > num2)
			{
				A_Duel_Current_Winner_Text.text = winnerYouText;
			}
			else
			{
				A_Duel_Current_Winner_Text.text = winnerEnemyText;
			}
		}
	}

	private void SetDuelCharacterEmote(bool hasCharacter, SimpleCharacter character, FungusEnumerators.FishingJointDuelId fishingJointDuelId, DuelCharacterEmoteEnum emote)
	{
		if (!hasCharacter)
		{
			return;
		}
		switch (fishingJointDuelId)
		{
		case FungusEnumerators.FishingJointDuelId.Hobo:
			switch (emote)
			{
			case DuelCharacterEmoteEnum.Normal:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("face", "normal")
				});
				break;
			case DuelCharacterEmoteEnum.Smile:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("face", "smile")
				});
				break;
			case DuelCharacterEmoteEnum.Angry:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("face", "angry")
				});
				break;
			}
			break;
		case FungusEnumerators.FishingJointDuelId.PoliceMan:
			switch (emote)
			{
			case DuelCharacterEmoteEnum.Normal:
			case DuelCharacterEmoteEnum.Smile:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("face", "normal")
				});
				break;
			case DuelCharacterEmoteEnum.Angry:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("face", "angry")
				});
				break;
			}
			break;
		case FungusEnumerators.FishingJointDuelId.PriestBot:
			switch (emote)
			{
			case DuelCharacterEmoteEnum.Normal:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("Face", "botFaceNeutral")
				});
				break;
			case DuelCharacterEmoteEnum.Smile:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("Face", "botFacePeaceful")
				});
				break;
			case DuelCharacterEmoteEnum.Angry:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("Face", "botFaceAngry")
				});
				break;
			}
			break;
		case FungusEnumerators.FishingJointDuelId.BigDaddyHurt:
			switch (emote)
			{
			case DuelCharacterEmoteEnum.Normal:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("Face", "bdhNormal")
				});
				break;
			case DuelCharacterEmoteEnum.Smile:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("Face", "bdhSmile")
				});
				break;
			case DuelCharacterEmoteEnum.Angry:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("Face", "bdhSerious")
				});
				break;
			}
			break;
		case FungusEnumerators.FishingJointDuelId.Landlord:
			switch (emote)
			{
			case DuelCharacterEmoteEnum.Normal:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("Face", "Normal")
				});
				break;
			case DuelCharacterEmoteEnum.Smile:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("Face", "Grin")
				});
				break;
			case DuelCharacterEmoteEnum.Angry:
				character.SetEmote(new EmoteData[1]
				{
					new EmoteData("Face", "Unhappy")
				});
				break;
			}
			break;
		}
	}

	private async UniTask DuelEnemyTask(FishingResults fishingResults, UniTask timerTask, FishingGameStats fishingGameStats, CancellationToken cancellationToken)
	{
		FishingDuelResults fishingDuelResults = fishingResults.GetDuelResultsOrErr();
		float difficulty = 0f;
		switch (fishingResults.FishingStartInfo.FishingJointDuelId)
		{
		case FungusEnumerators.FishingJointDuelId.Hobo:
			difficulty = 0.1f;
			break;
		case FungusEnumerators.FishingJointDuelId.PoliceMan:
			difficulty = 0.3f;
			break;
		case FungusEnumerators.FishingJointDuelId.PriestBot:
			difficulty = 0.5f;
			break;
		case FungusEnumerators.FishingJointDuelId.BigDaddyHurt:
			difficulty = 0.8f;
			break;
		case FungusEnumerators.FishingJointDuelId.Landlord:
			difficulty = 0.9f;
			break;
		case FungusEnumerators.FishingJointDuelId.Bot:
			difficulty = 1f;
			break;
		}
		bool hasCharacter = SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.TryGetOnlyActiveCharacterOrErr(out var character);
		if (hasCharacter)
		{
			SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.DoAnimation(character, SimpleCharacterSingleton.AnimationsList.MoveToRightAbs, null, 0.3f);
		}
		DuelCharacterEmoteEnum _lastEmote = (DuelCharacterEmoteEnum)(-1);
		await DelayForFishes();
		float time = 0f;
		while (!cancellationToken.IsCancellationRequested && timerTask.Status == UniTaskStatus.Pending)
		{
			SetEmoteAuto();
			float minInclusive = 0.4f;
			float maxInclusive = Mathf.Lerp(0.6f, 0.85f, difficulty);
			if (_fishingResults.FishingStartInfo.FishingLocation.TryGetBite(time, UnityEngine.Random.Range(minInclusive, maxInclusive), fishingGameStats, out var possibleCatch))
			{
				SetEmote(DuelCharacterEmoteEnum.Smile);
				await UniTask.WaitForSeconds((0.3f + UnityEngine.Random.value) * possibleCatch.GetFishDifficulty01(0f, fishingGameStats.FishingRodMaxEffectiveWeight) * 3f, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
				if (fishingResults.FishingStartInfo.FishingJointDuelId == FungusEnumerators.FishingJointDuelId.PoliceMan)
				{
					R.Sound.Story_Gunshot.Get().PlaySlightlyRandomVolume();
				}
				else
				{
					R.Sound.FishingMinigame_FishPullOut_All.PlayRandomSlightlyRandomVolumeAndPitch();
				}
				foreach (Item item in possibleCatch.GetItems())
				{
					StringBuilder stringBuilder;
					using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
					{
						UiOverlay.Instance.ShowFloatingText(A_Duel_Enemy_Score_Text.transform.position, item.GetNameStringBuilder(stringBuilder));
					}
					ItemPrefab itemPrefab = item.GetItemPrefab();
					if (itemPrefab != null && itemPrefab.FishData != null)
					{
						item.FishWeight = Mathf.Min(item.FishWeight, fishingGameStats.FishingRodMaxEffectiveWeight);
					}
					fishingDuelResults.AddEnemyScore(item.GetPrice());
				}
				SetEmoteAuto();
				await DelayForFishes();
				time = 0f;
			}
			await UniTask.WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.015f), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			possibleCatch = null;
		}
		UniTask DelayForFishes()
		{
			float num = Mathf.Lerp(6.25f, 4.2f, difficulty);
			return UniTask.WaitForSeconds(UnityEngine.Random.Range(1f, 1.25f) * num, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		}
		void SetEmote(DuelCharacterEmoteEnum emote)
		{
			if (_lastEmote != DuelCharacterEmoteEnum.Normal)
			{
				SetDuelCharacterEmote(hasCharacter, character, fishingResults.FishingStartInfo.FishingJointDuelId, emote);
				_lastEmote = emote;
			}
		}
		void SetEmoteAuto()
		{
			if (fishingDuelResults.IsPlayerWinning())
			{
				SetEmote(DuelCharacterEmoteEnum.Angry);
			}
			else
			{
				SetEmote(DuelCharacterEmoteEnum.Smile);
			}
		}
	}

	private void TweenBars(float ratio)
	{
		RectTransform rectTransform = A_Duel_YourBar.rectTransform;
		rectTransform.DOAnchorMax(new Vector2(ratio, rectTransform.anchorMax.y), 0.35f).SetEase(Ease.OutQuad).SetUpdate(isIndependentUpdate: true);
		RectTransform rectTransform2 = A_Duel_EnemyBar.rectTransform;
		rectTransform2.DOAnchorMin(new Vector2(ratio, rectTransform2.anchorMin.y), 0.35f).SetEase(Ease.OutQuad);
	}

	private void SetBarAnchorsImmediate(float ratio)
	{
		A_Duel_YourBar.rectTransform.SetAnchorMaxX(ratio);
		A_Duel_EnemyBar.rectTransform.SetAnchorMinX(ratio);
	}

	private async UniTask FishingMinigameDuel(FishingStartInfo fishingStartInfo, CancellationToken cancellationToken)
	{
		_fishingResults = FishingResults.CreateDuel(fishingStartInfo);
		PrepareFishingUi(showDuelWindow: true);
		FishingGameStats fishingGameStats = GetFishingGameStats(fishingStartInfo);
		UniTask timerTask = Timer(cancellationToken);
		using CancellationTokenSource duelCancellationSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		DuelScoreTask(_fishingResults, duelCancellationSource.Token).Forget();
		DuelEnemyTask(_fishingResults, timerTask, fishingGameStats, duelCancellationSource.Token).Forget();
		do
		{
			FishingBite fishingBite = await FishingFullIteration(fishingStartInfo, fishingGameStats, timerTask, cancellationToken);
			if (fishingBite == null)
			{
				continue;
			}
			foreach (Item item in fishingBite.GetItems())
			{
				_fishingResults.GetDuelResultsOrErr().AddPlayerScore(item.GetPrice());
				if (!fishingStartInfo.FishingLocation.IsImaginary)
				{
					GameVariables.Current.itemManager.AddItem(item);
				}
			}
		}
		while (!timerTask.Status.IsCompleted());
		timerTask.Forget();
		duelCancellationSource.Cancel();
		A_Fishing_Info_Text.text = LOC.UI.FishingDone;
		if (_fishingResults.GetDuelResultsOrErr().IsPlayerWinning())
		{
			if (fishingStartInfo.FishingLocation.IsImaginary)
			{
				R.Sound.FishingMinigame_Duel_Victory.Get().Play();
			}
			UiOverlay.Instance.ShowFloatingText(A_Floating_Text_Source.position, LOC.UI.FishingDuelWon);
		}
		else
		{
			if (fishingStartInfo.FishingLocation.IsImaginary)
			{
				R.Sound.FishingMinigame_Duel_Defeat.Get().Play();
			}
			UiOverlay.Instance.ShowFloatingText(A_Floating_Text_Source.position, LOC.UI.FishingDuelLost);
		}
		await UniTask.WaitForSeconds(0.5f, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		OnFinishedFishing(_fishingResults);
	}

	public void StartFishingMinigame(FishingStartInfo fishingStartInfo, Action<FishingResults> OnFinished)
	{
		KillMinigameTask();
		_onFinishedAction = OnFinished;
		_fishingMinigameCancellation = new CancellationTokenSource();
		if (fishingStartInfo.IsFishingJointDuel())
		{
			FishingMinigameDuel(fishingStartInfo, _fishingMinigameCancellation.Token).Forget();
		}
		else
		{
			FishingMinigameNormal(fishingStartInfo, _fishingMinigameCancellation.Token).Forget();
		}
	}

	private void OnFinishedFishing(FishingResults results)
	{
		if (_onFinishedAction != null)
		{
			Item.ItemManager itemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
			Item firstItemAtSlot = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.Anon_FishingBait.Id);
			if (firstItemAtSlot != null)
			{
				itemManager.ConsumeItem(firstItemAtSlot, 1);
			}
			_onFinishedAction?.Invoke(results);
			_onFinishedAction = null;
		}
	}

	public void KillMinigameTask()
	{
		if (_fishingMinigameCancellation != null)
		{
			_fishingMinigameCancellation.Cancel();
			_fishingMinigameCancellation.Dispose();
			_fishingMinigameCancellation = null;
		}
	}

	public void OnDestroy()
	{
		KillMinigameTask();
	}
}
