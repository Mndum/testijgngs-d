using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using StoryConditions;
using UnityEngine;

public class ShowerBrainState : CommonLewdSceneBrainState<Live2DShowerController>, IHasNonJitClassDescription
{
	public enum ShowerSceneStateEnum
	{
		Washing,
		WashingDone,
		Sex,
		Bluescreen
	}

	[Serializable]
	public class ShowerConversations : LocalizedStringReferenceContainer
	{
		public SerializedLocalizedConversation Beginning;

		public SerializedLocalizedConversation Beginning_FirstTime;

		public SerializedLocalizedConversation SoapInEyes;

		public SerializedLocalizedConversation ApplyingSoap;

		public SerializedLocalizedConversation ScrubbingShort;

		public SerializedLocalizedConversation ScrubbingMedium;

		public SerializedLocalizedConversation ScrubbingLong;

		public SerializedLocalizedConversation ScrubbingAlreadyClean;

		public SerializedLocalizedConversation ScrubbingFocusAss;

		public SerializedLocalizedConversation ScrubbingFocusBoobs;

		public SerializedLocalizedConversation BoredDuringScrubbing;

		public SerializedLocalizedConversation BoredDuringScrubbing_AlreadyClean;

		public SerializedLocalizedConversation BeforeWashingNormal;

		public SerializedLocalizedConversation AfterWashingNormal;

		public SerializedLocalizedConversation AfterWashingHorny;

		public SerializedLocalizedConversation AfterWashingVeryHorny;

		public SerializedLocalizedConversation NeedsScrubbing_NoSoap;

		public SerializedLocalizedConversation NeedsScrubbing_ALot;

		public SerializedLocalizedConversation NeedsScrubbing_ALittle;

		public SerializedLocalizedConversation AfterSex;

		public SerializedLocalizedConversation AfterSexDirty;

		public SerializedLocalizedConversation AfterSexDirtyCleanedUp;

		public SerializedLocalizedConversation AfterSexDirtyLeaveCum;

		public SerializedLocalizedConversation BlueScreenStarted;

		public SerializedLocalizedConversation BlueScreenEnded;

		public override string ContainerName => "Shower";
	}

	public class ShowerDialogue : StoryBotDialogueAreaDefinition
	{
		public StoryBotDialogueTagDefinition Beginning;

		public StoryBotDialogueTagDefinition SoapInEyes;

		public StoryBotDialogueTagDefinition ApplyingSoap;

		public StoryBotDialogueTagDefinition Scrubbing;

		public StoryBotDialogueTagDefinition Scrubbing_FocusAss;

		public StoryBotDialogueTagDefinition Scrubbing_FocusBoobs;

		public StoryBotDialogueTagDefinition NeedsScrubbing_NoSoap;

		public StoryBotDialogueTagDefinition NeedsScrubbing_ALot;

		public StoryBotDialogueTagDefinition NeedsScrubbing_ALittle;

		public StoryBotDialogueTagDefinition BoredDuringScrubbing;

		public StoryBotDialogueTagDefinition BeforeWashing;

		public StoryBotDialogueTagDefinition AfterWashing;

		public StoryBotDialogueTagDefinition AfterSex;

		public StoryBotDialogueTagDefinition AfterSexDirty;

		public StoryBotDialogueTagDefinition AfterSexDirtyCleanedUp;

		public StoryBotDialogueTagDefinition AfterSexDirtyLeaveCum;

		public StoryBotDialogueTagDefinition BlueScreenStarted;

		public StoryBotDialogueTagDefinition BlueScreenEnded;

		public override string GetPrefix()
		{
			return "Shower_";
		}

		protected override void InitInner(StorySingleton storySingleton, StoryBotDialogueStage stage1, StoryBotDialogueStage stage2, StoryBotDialogueStage stage3)
		{
			DataCondition<float> dataCondition = StoryCondition.VariableCondition("PotentialHorniness", DataConditionComparators.SingleArgumentComparators.Above, 0.5f);
			DataCondition<float> dataCondition2 = StoryCondition.VariableCondition("PotentialHorniness", DataConditionComparators.SingleArgumentComparators.Above, 0.75f);
			ShowerConversations showerConversations = SingletonScriptableObject<CFLocalizationDataSO>.Instance.ShowerConversations;
			Beginning.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(1), showerConversations.Beginning)).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(2), showerConversations.Beginning_FirstTime).MarkOneTime().SetPriority(1));
			SoapInEyes.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(10), showerConversations.SoapInEyes));
			ApplyingSoap.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(11), showerConversations.ApplyingSoap));
			DataCondition<bool> dataCondition3 = StoryCondition.ModelBrainStateVariableCondition<bool, ShowerBrainState>("IsClean", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
			Scrubbing.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(20), showerConversations.ScrubbingShort).AddConditions(new InteractionMemoryCondition(InteractionMemoryEnum.Scrubbing, InteractionMemoryCondition.MemoryConditionTypeEnum.ActiveFor, 1f))).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(21), showerConversations.ScrubbingMedium).AddConditions(new InteractionMemoryCondition(InteractionMemoryEnum.Scrubbing, InteractionMemoryCondition.MemoryConditionTypeEnum.ActiveFor, 5f)))
				.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(22), showerConversations.ScrubbingLong).AddConditions(new InteractionMemoryCondition(InteractionMemoryEnum.Scrubbing, InteractionMemoryCondition.MemoryConditionTypeEnum.ActiveFor, 10f)))
				.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(23), showerConversations.ScrubbingAlreadyClean).AddConditions(dataCondition3))
				.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(24), showerConversations.ScrubbingFocusAss).AddConditions(new InteractionMemoryCondition(InteractionMemoryEnum.AssFondle, InteractionMemoryCondition.MemoryConditionTypeEnum.ActiveFor, 7f)).SetPriority(1))
				.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(25), showerConversations.ScrubbingFocusBoobs).AddConditions(new InteractionMemoryCondition(InteractionMemoryEnum.BoobsFondle, InteractionMemoryCondition.MemoryConditionTypeEnum.ActiveFor, 7f)).SetPriority(1));
			NeedsScrubbing_NoSoap.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(27), showerConversations.NeedsScrubbing_NoSoap));
			NeedsScrubbing_ALot.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(28), showerConversations.NeedsScrubbing_ALot));
			NeedsScrubbing_ALittle.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(29), showerConversations.NeedsScrubbing_ALittle));
			BoredDuringScrubbing.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(30), showerConversations.BoredDuringScrubbing)).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(31), showerConversations.BoredDuringScrubbing_AlreadyClean).AddConditions(dataCondition3).SetOdds(10f));
			BeforeWashing.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(35), showerConversations.BeforeWashingNormal));
			AfterWashing.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(40), showerConversations.AfterWashingNormal)).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(41), showerConversations.AfterWashingHorny).AddConditions(dataCondition).SetOdds(10f))
				.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(42), showerConversations.AfterWashingVeryHorny).AddConditions(dataCondition2).SetOdds(20f));
			AfterSex.CreateContainer(stage3, out var o);
			o.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(50), showerConversations.AfterSex));
			AfterSexDirty.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(55), showerConversations.AfterSexDirty));
			AfterSexDirtyCleanedUp.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(60), showerConversations.AfterSexDirtyCleanedUp));
			AfterSexDirtyLeaveCum.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(65), showerConversations.AfterSexDirtyLeaveCum));
			BlueScreenStarted.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(70), showerConversations.BlueScreenStarted));
			BlueScreenEnded.CreateContainerReturn(stage3).AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.ShowerDialogues.GetGameId(75), showerConversations.BlueScreenEnded));
		}
	}

	public enum ShowerReactions
	{
		Nothing,
		SoapInEyes,
		Bored,
		BotCumming,
		CockOut,
		Spank,
		Thighjob,
		Sex,
		WashOffSoap,
		NeedsScrubbing,
		EndSex,
		Bluescreen
	}

	private static readonly ReactionManager<ShowerBrainState> ReactionManager;

	private ReactionManagerData _reactionManagerData;

	private ShowerSceneStateEnum _state;

	private const float MinFuckSpeed = 2f;

	private const float MaxFuckSpeed = 18f;

	private float targetFuckSpeed = 2f;

	public override bool IsInterested => _reactionManagerData.HasReactedRecently();

	public bool IsClean => Controller.GetCleaningProgress() >= 1f;

	public bool UsedSoap => Controller.UsedAnySoap();

	private ShowerDialogue Dialogue => SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<ShowerDialogue>();

	private FrickDialogueArea SexDialogue => SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<FrickDialogueArea>();

	public ShowerReactions CurrentReaction { get; private set; }

	static ShowerBrainState()
	{
		ReactionManager = new ReactionManager<ShowerBrainState>(new Dictionary<InteractionMemoryEnum, ReactionManager<ShowerBrainState>.Reaction>
		{
			[InteractionMemoryEnum.Scrubbing] = (ShowerBrainState caller, ComplexEventMemory memory, int count) =>
			{
				if (UnityEngine.Random.value > 0.5f && caller.Owner.NotTalkedForAWhile)
				{
					caller.Dialogue.Scrubbing.StartDialogueFromModelBrainState(caller);
				}
			},
			[InteractionMemoryEnum.AssFondle] = (ShowerBrainState caller, ComplexEventMemory memory, int count) =>
			{
				if (UnityEngine.Random.value > 0.5f && caller.Owner.NotTalkedForAWhile)
				{
					caller.Dialogue.Scrubbing.StartDialogueFromModelBrainState(caller);
				}
			},
			[InteractionMemoryEnum.Fingering] = (ShowerBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (UnityEngine.Random.value > 0.6f)
					{
						caller.CommonDialogue.FingeringDuring.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.FingeringAnus] = (ShowerBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (UnityEngine.Random.value > 0.6f)
					{
						caller.CommonDialogue.FingeringAnusDuring.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.Headpat] = (ShowerBrainState caller, ComplexEventMemory memory, int count) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.sympathy++;
					gameVariables.lust++;
					if (UnityEngine.Random.value > 0.6f)
					{
						caller.CommonDialogue.Headpat.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.PussySpread] = (ShowerBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (UnityEngine.Random.value > 0.6f)
					{
						caller.CommonDialogue.PussySpread.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.AnusSpread] = (ShowerBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (UnityEngine.Random.value > 0.6f)
					{
						caller.CommonDialogue.AnusSpread.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.ClitRub] = (ShowerBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (UnityEngine.Random.value > 0.6f)
					{
						caller.CommonDialogue.ClitRub.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.BoobsFondle] = (ShowerBrainState caller, ComplexEventMemory memory, int count) =>
			{
				if (UnityEngine.Random.value > 0.5f && caller.Owner.NotTalkedForAWhile)
				{
					caller.Dialogue.Scrubbing.StartDialogueFromModelBrainState(caller);
				}
			}
		});
	}

	public void StartAfterWashing()
	{
		_state = ShowerSceneStateEnum.WashingDone;
		controller.ManifestHorninessAndResetFrickCommonData();
	}

	private IEnumerator SoapInEyes()
	{
		yield return null;
		Dialogue.SoapInEyes.StartDialogueFromModelBrainState(this);
		Owner.CausePain(1f);
		controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, -1f);
		int left = UnityEngine.Random.Range(2, 5);
		while (controller.MoveAllSoapInEyeArea() || left > 0)
		{
			controller.Eyes.CloseEyes();
			controller.MoveAllSoapInEyeArea();
			yield return WaitForSecondCache.RandomUnderThirdSecond();
			controller.Eyes.OpenEyes();
			yield return WaitForSecondCache.RandomUnderThirdSecond();
			left--;
		}
	}

	private IEnumerator CockOut()
	{
		yield return null;
		KillTalkingCoroutine();
		yield return null;
		controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Smug, 0.7f);
		LookRandomBack();
		SexDialogue.StartFrick.StartDialogueFromModelBrainState(this);
	}

	private IEnumerator NeedsScrubbing()
	{
		yield return null;
		if (!controller.UsedAnySoap())
		{
			Dialogue.NeedsScrubbing_NoSoap.StartDialogueFromModelBrainState(this);
		}
		else if (controller.GetCleaningProgress() < 0.5f)
		{
			Dialogue.NeedsScrubbing_ALot.StartDialogueFromModelBrainState(this);
		}
		else
		{
			Dialogue.NeedsScrubbing_ALittle.StartDialogueFromModelBrainState(this);
		}
	}

	private IEnumerator WashOffSoap(EyeClosingShader eyeClosingScript)
	{
		yield return null;
		KillTalkingCoroutine();
		yield return Dialogue.BeforeWashing.StartDialogueFromModelBrainStateAndWait(this, isRealDialogue: true, noWrapper: true);
		yield return eyeClosingScript.QuickCloseEyes();
		Controller.DestroyAllSoap();
		Controller.CumOutsideManager.ClearAllCum();
		yield return R.Sound.Shower_runShower.Get().PlayAndWait();
		SingletonLoadableMonoBehaviour<GameScript>.Instance.PassTimeInstantly(15, GameVariables.StaminaSyncType.Recovery);
		GameVariables.Current.AnonShowerEffect();
		GameVariables.Current.BotShowerEffect();
		GameVariables.Current.AddFlag("ShowerTogetherComplete");
		yield return eyeClosingScript.QuickOpenEyes();
		StartAfterWashing();
		yield return Dialogue.AfterWashing.StartDialogueFromModelBrainStateAndWait(this, isRealDialogue: true, noWrapper: true);
	}

	private IEnumerator Bored()
	{
		yield return null;
		switch (_state)
		{
		default:
			yield return Dialogue.BoredDuringScrubbing.StartDialogueFromModelBrainStateAndWait(this);
			break;
		case ShowerSceneStateEnum.WashingDone:
			yield return Dialogue.AfterWashing.StartDialogueFromModelBrainStateAndWait(this);
			break;
		case ShowerSceneStateEnum.Sex:
			if (Controller.CockManager.IsFlacid)
			{
				yield return SexDialogue.FlacidBoredReaction.StartDialogueFromModelBrainStateAndWait(this);
			}
			else
			{
				yield return SexDialogue.GenericFuckBrainStateRandomDialogueCockOutside.StartDialogueFromModelBrainStateAndWait(this);
			}
			break;
		case ShowerSceneStateEnum.Bluescreen:
			break;
		}
	}

	private IEnumerator EndSex(EyeClosingShader eyeClosingScript)
	{
		yield return null;
		KillTalkingCoroutine();
		LookAndMoveHeadBack();
		if (Controller.CumOutsideManager.AnySplatterNotEmpty)
		{
			yield return Dialogue.AfterSexDirty.StartDialogueFromModelBrainStateAndWait(this, isRealDialogue: true, noWrapper: true);
			int chosen = 0;
			yield return SingletonMonoBehaviour<FungusEnumerators>.Instance.DoMenu(new List<MenuOption>
			{
				new MenuOption(LOC.Other.Shower_AfterSex_CleanCum),
				new MenuOption(LOC.Other.Shower_AfterSex_LeaveItOn)
			}, (int i, MenuOption option) =>
			{
				chosen = i;
			});
			if (chosen != 1)
			{
				yield return eyeClosingScript.QuickCloseEyes();
				Controller.DestroyAllSoap();
				Controller.CumOutsideManager.ClearAllCum();
				yield return R.Sound.Shower_runShower.Get().PlayAndWait();
				GameVariables.Current.AnonShowerEffect();
				GameVariables.Current.BotShowerEffect();
				yield return eyeClosingScript.QuickOpenEyes();
				yield return Dialogue.AfterSexDirtyCleanedUp.StartDialogueFromModelBrainStateAndWait(this, isRealDialogue: true, noWrapper: true);
			}
			else
			{
				yield return Dialogue.AfterSexDirtyLeaveCum.StartDialogueFromModelBrainStateAndWait(this, isRealDialogue: true, noWrapper: true);
			}
		}
		else
		{
			yield return Dialogue.AfterSex.StartDialogueFromModelBrainStateAndWait(this, isRealDialogue: true, noWrapper: true);
		}
		GameVariables.Current.EndSex();
		yield return WaitForSecondCache.Seconds1;
	}

	private IEnumerator BlueScreen()
	{
		yield return null;
		KillTalkingCoroutine();
		controller.Mouth.MakeSureMouthAtLeastHalfOpen();
		controller.Eyes.OpenEyes();
		controller.Bluescreen.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		controller.Bluescreen.Value = 1f;
		controller.Eyes.DisableBlinking = true;
		controller.Mouth.DisableLetterOutputToMouth = true;
		Dialogue.BlueScreenStarted.StartDialogueFromModelBrainState(this);
		float timer = UnityEngine.Random.Range(4, 7);
		while (timer > 0f)
		{
			if (controller.CurrentSceneCamera == Live2DShowerController.ShowerSceneCameraEnum.POVCamera)
			{
				controller.ParamHeadX.MoveValueTowardsInUpdate(-1f, Time.deltaTime, 0.2f);
			}
			controller.ParamHeadY.MoveValueTowardsInUpdate(-1f, Time.deltaTime, 0.2f);
			timer -= Time.deltaTime;
			yield return null;
		}
		controller.Bluescreen.SetRealValue(0f);
		controller.Eyes.DisableBlinking = false;
		controller.Mouth.DisableLetterOutputToMouth = false;
		controller.Eyes.ToDefaultEyesPosition();
		controller.ToDefaultFaceAngle();
		Dialogue.BlueScreenEnded.StartDialogueFromModelBrainState(this);
		_state = ShowerSceneStateEnum.Sex;
	}

	public void MoveSexSpeedTo01(float targetPercent)
	{
		targetFuckSpeed = targetFuckSpeed * 0.5f + Mathf.Lerp(2f, 18f, targetPercent) * 0.5f;
	}

	private void AdjustSexSpeedByHorniness()
	{
		MoveSexSpeedTo01(Mathf.Max(Owner.Pleasure, controller.CockManager.TargetCumBarValue));
	}

	private IEnumerator GoToBodyTarget(Vector2 target, float movementSpeed = 1f)
	{
		target.x = Mathf.Clamp(target.x, -1f, 1f);
		target.y = Mathf.Clamp(target.y, 0f, 1f);
		Vector2 vector = GetCurrentValue();
		Vector2 current = vector;
		float num = Vector2.Distance(vector, target);
		float variation = 1f;
		float t = 0f;
		float duration = num / movementSpeed;
		while (true)
		{
			variation = Mathf.Lerp(variation, UnityEngine.Random.Range(0.88f, 1.12f), 0.5f);
			variation = Mathf.Clamp(variation, 0.75f, 1.25f);
			float num2 = variation * Time.deltaTime;
			t += num2;
			Vector2 value = Vector2.Lerp(current, target, EasingFunction.EaseInQuad(0f, 1f, t / duration));
			MoveCurrentValueTowards(value, movementSpeed * num2 * 1.05f);
			if (!(t >= 1f))
			{
				current = GetCurrentValue();
				num = Vector2.Distance(current, target);
				if (!(num < 0.0001f))
				{
					yield return null;
					continue;
				}
				break;
			}
			break;
		}
		Vector2 GetCurrentValue()
		{
			return new Vector2(controller.ButtMovement.Value, controller.ParamBotBodyStraighten.Value);
		}
		void MoveCurrentValueTowards(Vector2 vector2, float maxDelta)
		{
			controller.ButtMovement.Value = Mathf.MoveTowards(controller.ButtMovement.Value, vector2.x, maxDelta);
			controller.ParamBotBodyStraighten.Value = Mathf.MoveTowards(controller.ParamBotBodyStraighten.Value, vector2.y, maxDelta);
		}
	}

	private IEnumerator ThighjobReaction()
	{
		ICockLimbUsage o;
		InsertionHandler o2;
		InsertionHandlerFuckAxis fuckAxis;
		while (Controller.CockManager.TryGetCockLimbUsage(out o) && o.TryGetActiveData(out o2, out fuckAxis) && (fuckAxis.IsInside || fuckAxis.IsAfterOutside))
		{
			controller.FireEvent(InteractionMemoryEventEnum.ThighjobFast);
			yield return GoToBodyTarget(new Vector2(-1f, controller.ParamBotBodyStraighten.GetRealValue()), targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_05;
			yield return GoToBodyTarget(new Vector2(1f, controller.ParamBotBodyStraighten.GetRealValue() + UnityEngine.Random.Range(-0.05f, 0.1f)), targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_2;
			AdjustSexSpeedByHorniness();
		}
	}

	private void MakeSureThighjobReaction_Inner()
	{
		AdjustSexSpeedByHorniness();
		if (CurrentReaction != ShowerReactions.Thighjob || !IsReacting)
		{
			DoReaction(ThighjobReaction());
		}
	}

	private bool ShouldContinueSex(out ICockLimbUsage cockUsage, out InsertionHandler orfice, out InsertionHandlerFuckAxis fuckAxis)
	{
		orfice = null;
		fuckAxis = null;
		if (Controller.CockManager.TryGetCockLimbUsage(out cockUsage) && cockUsage.TryGetActiveData(out orfice, out fuckAxis))
		{
			if (!fuckAxis.IsInside)
			{
				return fuckAxis.IsAfterOutside;
			}
			return true;
		}
		return false;
	}

	private IEnumerator Sex1()
	{
		int left = UnityEngine.Random.Range(4, 8);
		ICockLimbUsage cockUsage;
		InsertionHandler orfice;
		InsertionHandlerFuckAxis fuckAxis;
		while (left > 0 && ShouldContinueSex(out cockUsage, out orfice, out fuckAxis))
		{
			yield return GoToBodyTarget(new Vector2(1f, 0f), targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_2;
			yield return GoToBodyTarget(new Vector2(-1f, 0f), targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_05;
			AdjustSexSpeedByHorniness();
			left--;
		}
	}

	private IEnumerator Sex2()
	{
		int left = UnityEngine.Random.Range(3, 5);
		ICockLimbUsage cockUsage;
		InsertionHandler orfice;
		InsertionHandlerFuckAxis fuckAxis;
		while (left > 0 && ShouldContinueSex(out cockUsage, out orfice, out fuckAxis))
		{
			yield return GoToBodyTarget(new Vector2(1f, 0f), targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_2;
			yield return GoToBodyTarget(new Vector2(-1f, 0.25f), targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_05;
			yield return GoToBodyTarget(new Vector2(1f, 0.25f), targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_2;
			yield return GoToBodyTarget(new Vector2(-1f, 0.25f), targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_05;
			AdjustSexSpeedByHorniness();
			left--;
		}
	}

	private IEnumerator Sex3()
	{
		int left = UnityEngine.Random.Range(20, 40);
		float waitedFor = 0f;
		ICockLimbUsage cockUsage;
		InsertionHandler orfice;
		InsertionHandlerFuckAxis fuckAxis;
		while (left > 0 && ShouldContinueSex(out cockUsage, out orfice, out fuckAxis))
		{
			MoveSexSpeedTo01(fuckAxis.FuckingSpeedThrust * 0.5f + 0.5f);
			float currentRealInsertionPosition = fuckAxis.GetCurrentRealInsertionPosition();
			if (currentRealInsertionPosition > 0.8f)
			{
				waitedFor = 0f;
				yield return GoToBodyTarget(new Vector2(-1f, 0f), targetFuckSpeed);
			}
			else if (currentRealInsertionPosition < 0.3f)
			{
				waitedFor = 0f;
				yield return GoToBodyTarget(new Vector2(1f, 0f), targetFuckSpeed);
			}
			else
			{
				waitedFor += Time.deltaTime;
				if (waitedFor > 1.5f)
				{
					break;
				}
				yield return null;
			}
			left--;
		}
	}

	private IEnumerator SexReaction()
	{
		ICockLimbUsage cockUsage;
		InsertionHandler orfice;
		InsertionHandlerFuckAxis fuckAxis;
		while (ShouldContinueSex(out cockUsage, out orfice, out fuckAxis))
		{
			float value = UnityEngine.Random.value;
			if (value < 0.3f)
			{
				yield return Sex1();
			}
			else if (value < 0.6f)
			{
				yield return Sex2();
			}
			else if (fuckAxis.FuckingSpeedSum > 0.1f)
			{
				yield return Sex3();
			}
			else
			{
				yield return Sex2();
			}
		}
	}

	private void MakeSureSexReaction_Inner()
	{
		AdjustSexSpeedByHorniness();
		if (CurrentReaction != ShowerReactions.Sex || !IsReacting)
		{
			DoReaction(SexReaction());
		}
	}

	private IEnumerator CumReaction()
	{
		controller.Eyes.OpenEyes();
		using Live2DExpression.CummingExpressionState cm = controller.Expression.StartCumming();
		controller.Eyes.DisableBlinking = true;
		if (!IsTalking)
		{
			if (Owner.InteractionMemory.IsActiveComplexWithSourceEventTimeout(InteractionMemoryEnum.FingeringAnus))
			{
				CommonDialogue.FingeringAnusBotCumming.StartDialogueFromModelBrainState(this);
			}
			else if (Owner.InteractionMemory.IsActiveComplexWithSourceEventTimeout(InteractionMemoryEnum.Fingering))
			{
				CommonDialogue.FingeringBotCumming.StartDialogueFromModelBrainState(this);
			}
			else
			{
				CommonDialogue.BotCumming.StartDialogueFromModelBrainState(this);
			}
		}
		yield return GoToBodyTarget(new Vector2(-1f, 0f), 5f);
		bool madeMcCum = false;
		for (int i = 0; i < cm.NumberOfTwitches; i++)
		{
			yield return new WaitForSeconds(cm.TwitchLength / 2f);
			cm.DoQuiver1();
			controller.ButtMovement.Value -= UnityEngine.Random.Range(0.1f, 0.2f);
			controller.ParamBotBodyStraighten.Value -= UnityEngine.Random.Range(0.1f, 0.2f);
			yield return WaitForSecondCache.RandomUnderThirdSecond();
			controller.ButtMovement.Value += UnityEngine.Random.Range(0.1f, 0.2f);
			controller.ParamBotBodyStraighten.Value += UnityEngine.Random.Range(0.1f, 0.2f);
			cm.DoQuiver2();
			yield return new WaitForSeconds(cm.TwitchLength / 2f);
			cm.AfterQuiver();
			if (i % 8 == 0 && Owner.PersonalityData.MoanOdds.Check())
			{
				MoanIfPossible();
			}
			if (!madeMcCum && controller.TryGetLimbUsageAt(LimbUsageChoiceEnum.Cock, out ICockLimbUsage limbUsage) && limbUsage.IfCumImminentSpeedItUp())
			{
				madeMcCum = true;
			}
		}
		yield return cm.CumFinishTimeOut();
		MoveHeadRandom();
		CurrentReaction = ShowerReactions.Nothing;
	}

	private IEnumerator SpankReaction()
	{
		yield return null;
		KillTalkingCoroutine();
		Controller.ButtMovement.Add(0f - UnityEngine.Random.Range(0.1f, 0.3f));
		Controller.ParamBotBodyStraighten.Add(UnityEngine.Random.Range(0.1f, 0.3f));
		yield return WaitForSecondCache.RandomUnderThirdSecond();
		if (Owner.PersonalityData.MoanOdds.Check(0.5f))
		{
			MoanIfPossible();
		}
		Controller.ButtMovement.Add(UnityEngine.Random.Range(0.1f, 0.3f));
		Controller.ParamBotBodyStraighten.Add(0f - UnityEngine.Random.Range(0.1f, 0.3f));
		ReactToCorrectSexType();
	}

	public void React(ShowerReactions reaction)
	{
		if (IsReactingUnstoppable)
		{
			return;
		}
		switch (reaction)
		{
		default:
			return;
		case ShowerReactions.BotCumming:
			DoReactionUnstoppable(CumReaction());
			break;
		case ShowerReactions.Bluescreen:
			_state = ShowerSceneStateEnum.Bluescreen;
			DoReactionUnstoppable(BlueScreen());
			break;
		case ShowerReactions.Bored:
			DoReaction(Bored());
			break;
		case ShowerReactions.SoapInEyes:
			controller.Eyes.DisableBlinking = true;
			DoReactionUnstoppable(SoapInEyes(), (ManagedCoroutine.FinishedType type) =>
			{
				controller.Eyes.DisableBlinking = false;
			});
			break;
		case ShowerReactions.Thighjob:
			MakeSureThighjobReaction_Inner();
			break;
		case ShowerReactions.Sex:
			MakeSureSexReaction_Inner();
			break;
		case ShowerReactions.NeedsScrubbing:
			DoReaction(NeedsScrubbing());
			break;
		case ShowerReactions.WashOffSoap:
		{
			controller.SetCurrentToolSelectorActive(value: false);
			UiOverlay.Instance.OpenFungusOverlay(Color.clear);
			EyeClosingShader eyeClosingScript2 = UiOverlay.Instance.MainCamera.GetComponent<EyeClosingShader>();
			DoReactionUnstoppable(WashOffSoap(eyeClosingScript2), (ManagedCoroutine.FinishedType type) =>
			{
				eyeClosingScript2.enabled = false;
				if (type == ManagedCoroutine.FinishedType.Normal)
				{
					controller.SetCurrentToolSelectorActive(value: true);
					UiOverlay.Instance.CloseFungusOverlay();
				}
			}, noWrapper: true);
			break;
		}
		case ShowerReactions.CockOut:
			_state = ShowerSceneStateEnum.Sex;
			DoReactionUnstoppable(CockOut());
			break;
		case ShowerReactions.EndSex:
		{
			controller.SetCurrentToolSelectorActive(value: false);
			UiOverlay.Instance.OpenFungusOverlay(Color.clear);
			EyeClosingShader eyeClosingScript = UiOverlay.Instance.MainCamera.GetComponent<EyeClosingShader>();
			DoReactionUnstoppable(EndSex(eyeClosingScript), (ManagedCoroutine.FinishedType type) =>
			{
				eyeClosingScript.enabled = false;
				if (type == ManagedCoroutine.FinishedType.Normal)
				{
					controller.SetCurrentToolSelectorActive(value: true);
					UiOverlay.Instance.CloseFungusOverlay();
					SingletonLoadableMonoBehaviour<GameScript>.Instance.EndInteracting();
				}
			}, noWrapper: true);
			break;
		}
		case ShowerReactions.Spank:
			DoReaction(SpankReaction());
			return;
		}
		CurrentReaction = reaction;
	}

	public override void OnEventFired(InteractionMemoryEventEnum type)
	{
		if (_state == ShowerSceneStateEnum.Bluescreen)
		{
			return;
		}
		switch (type)
		{
		case InteractionMemoryEventEnum.ApplyingSoap:
			if (!IsTalking && !controller.Expression.IsCumming)
			{
				Dialogue.ApplyingSoap.StartDialogueFromModelBrainState(this);
			}
			break;
		case InteractionMemoryEventEnum.SoapEye:
			React(ShowerReactions.SoapInEyes);
			break;
		case InteractionMemoryEventEnum.Insert:
			controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Shock, 0.5f);
			LookRandomBack();
			break;
		case InteractionMemoryEventEnum.CumImminent:
			MoveSexSpeedTo01(2f);
			break;
		case InteractionMemoryEventEnum.CumThighjob:
			controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Smug, 0.5f);
			controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, UnityEngine.Random.Range(0.5f, 0.7f));
			if (!IsTalking && !controller.Expression.IsCumming && Owner.PersonalityData.ChanceToCommentCummingInside.Check())
			{
				LookRandomBack();
				CommonDialogue.CumThighjob.StartDialogueFromModelBrainState(this);
			}
			break;
		case InteractionMemoryEventEnum.CumInside:
			controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Smug, 0.5f);
			controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, UnityEngine.Random.Range(0.5f, 0.7f));
			if (!IsTalking && !controller.Expression.IsCumming && Owner.PersonalityData.ChanceToCommentCummingInside.Check())
			{
				LookRandomBack();
				CommonDialogue.CumInside.StartDialogueFromModelBrainState(this);
			}
			break;
		case InteractionMemoryEventEnum.CumInsideAnal:
			controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Smug, 0.5f);
			controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, UnityEngine.Random.Range(0.5f, 0.7f));
			if (!IsTalking && !controller.Expression.IsCumming && Owner.PersonalityData.ChanceToCommentCummingInsideAnal.Check())
			{
				LookRandomBack();
				CommonDialogue.CumInsideAnal.StartDialogueFromModelBrainState(this);
			}
			break;
		case InteractionMemoryEventEnum.CumOutside:
			controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Smug, 0.5f);
			if (!IsTalking && !controller.Expression.IsCumming && Owner.PersonalityData.ChanceToCommentCummingOutside.Check())
			{
				LookRandomBack();
				CommonDialogue.CumOutside.StartDialogueFromModelBrainState(this);
			}
			break;
		case InteractionMemoryEventEnum.Spank:
			Owner.CausePain(1f);
			controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Shock, 0.5f);
			MoveSexSpeedTo01(1f);
			React(ShowerReactions.Spank);
			break;
		}
	}

	public override void DoStopBotLeadReaction()
	{
	}

	private void LookRandomBack()
	{
		float x = UnityEngine.Random.Range(0.2f, 1f);
		float y = UnityEngine.Random.Range(-1f, 0f);
		Controller.Eyes.SetEyesPosition(x, y);
	}

	private void LookAndMoveHeadBack()
	{
		Controller.Eyes.SetEyesPosition(1f, 0f);
		controller.ParamHeadX.SetPercent(-1f);
		controller.ParamHeadY.SetPercent(0f);
	}

	private void MoveHeadRandom()
	{
		float value = UnityEngine.Random.value;
		if (Controller.CurrentSceneCamera == Live2DShowerController.ShowerSceneCameraEnum.POVCamera)
		{
			if (value < 0.33f)
			{
				float percent = UnityEngine.Random.Range(0.1f, 0.2f) * Mathf.Sign(UnityEngine.Random.value - 0.5f);
				float percent2 = UnityEngine.Random.Range(0.1f, 0.2f) * Mathf.Sign(UnityEngine.Random.value - 0.5f);
				controller.ParamHeadX.AddPercent(percent);
				controller.ParamHeadX.ClampValue(-1f, 0f);
				controller.ParamHeadY.AddPercent(percent2);
			}
			else if ((double)value < 0.6)
			{
				controller.ToDefaultFaceAngle();
			}
			else
			{
				controller.ParamHeadX.SetPercent(UnityEngine.Random.Range(-1f, 0f));
				controller.ParamHeadY.SetPercent(UnityEngine.Random.Range(-0.8f, 0.8f));
			}
		}
		else if (value < 0.33f)
		{
			float percent3 = UnityEngine.Random.Range(0.1f, 0.2f) * Mathf.Sign(UnityEngine.Random.value - 0.5f);
			float percent4 = UnityEngine.Random.Range(0.1f, 0.2f) * Mathf.Sign(UnityEngine.Random.value - 0.5f);
			controller.ParamHeadX.AddPercent(percent3);
			controller.ParamHeadY.AddPercent(percent4);
		}
		else if (value < 0.66f)
		{
			Vector2 leftEyeOffsetFromCenter = Controller.Eyes.GetLeftEyeOffsetFromCenter();
			controller.ParamHeadX.Value = leftEyeOffsetFromCenter.x;
			controller.ParamHeadX.Value = leftEyeOffsetFromCenter.y;
		}
		else if (value < 0.88f)
		{
			controller.ParamHeadX.SetPercent(UnityEngine.Random.Range(-0.8f, 0.8f));
			controller.ParamHeadY.SetPercent(UnityEngine.Random.Range(-0.8f, 0.8f));
		}
		else
		{
			controller.ToDefaultFaceAngle();
		}
	}

	private void ReactToCorrectSexType()
	{
		if (Controller.CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2, out var _))
		{
			switch (o2.OrificeType)
			{
			case InsertionHandlerOrificeTypeEnum.Vagina:
			case InsertionHandlerOrificeTypeEnum.Anal:
				React(ShowerReactions.Sex);
				break;
			case InsertionHandlerOrificeTypeEnum.Thighjob:
				React(ShowerReactions.Thighjob);
				break;
			}
		}
	}

	protected override IEnumerator Brain()
	{
		yield return null;
		LookRandomBack();
		yield return Dialogue.Beginning.StartDialogueFromModelBrainStateAndWait(this);
		int timesOnlyBotMoving = 0;
		int timesNothing = 0;
		int timesNoHeadMove = 0;
		while (true)
		{
			if (IsReacting)
			{
				timesNothing = 0;
			}
			else if (ReactionManager.React(ref _reactionManagerData, Owner.InteractionMemory, this))
			{
				timesNothing = 0;
			}
			else if (!Owner.IsInterested)
			{
				timesNothing++;
				if (timesNothing >= 5)
				{
					timesNothing = 0;
					React(ShowerReactions.Bored);
					continue;
				}
			}
			yield return WaitForSecondCache.RandomAroundASecond();
			switch (_state)
			{
			case ShowerSceneStateEnum.Bluescreen:
				break;
			default:
				LookRandomBack();
				break;
			case ShowerSceneStateEnum.Sex:
			{
				if (Controller.CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2, out var fuckAxis))
				{
					ReactToCorrectSexType();
					if (fuckAxis.FuckingSpeedThrust > 0.5f && UnityEngine.Random.value < 0.4f)
					{
						controller.Eyes.CloseEyesForSeconds(UnityEngine.Random.Range(2f, 3f));
					}
					timesOnlyBotMoving = ((fuckAxis.FuckingSpeedThrust < 0.1f && fuckAxis.FuckingSpeedReciprocating > 0.1f && o2.OrificeType != InsertionHandlerOrificeTypeEnum.Thighjob) ? (timesOnlyBotMoving + 1) : 0);
					if (Owner.CanCum)
					{
						React(ShowerReactions.BotCumming);
						if (CurrentReaction == ShowerReactions.BotCumming)
						{
							yield return WaitUntilReactionNotRunning();
							if (FrickData.D_TimesBotCame > 3 || (FrickData.D_TimesBotCame > 4 && UnityEngine.Random.value < 0.25f))
							{
								React(ShowerReactions.Bluescreen);
								if (CurrentReaction == ShowerReactions.Bluescreen)
								{
									yield return WaitUntilReactionNotRunning();
								}
							}
						}
					}
					else if (timesOnlyBotMoving >= 5 && Owner.NotTalkedForAWhile)
					{
						controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Angry, 0.5f);
						controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, -0.5f);
						controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Smug, 0f);
						SexDialogue.UpsetOnlySheMoves.StartDialogueFromModelBrainState(this);
					}
				}
				timesNoHeadMove++;
				if (timesNoHeadMove <= 3)
				{
					break;
				}
				timesNoHeadMove = 0;
				if (UnityEngine.Random.value > 0.33f)
				{
					if (!controller.Eyes.TryLookAtCurrentInteractable())
					{
						LookRandomBack();
					}
				}
				else
				{
					controller.Eyes.ToDefaultEyesPosition();
				}
				MoveHeadRandom();
				break;
			}
			}
		}
	}
}
