using System;
using System.Collections;
using System.Collections.Generic;
using StoryConditions;
using UnityEngine;

public class NewInteractBrainState : ModelBrainState<Live2DInteractionController>, IHasNonJitClassDescription
{
	[Serializable]
	public class InteractConversations : LocalizedStringReferenceContainer
	{
		public SerializedLocalizedConversation Headpat;

		public SerializedLocalizedConversation HeadpatOver10;

		public SerializedLocalizedConversation HeadpatOver30;

		public SerializedLocalizedConversation HeadpatOver60;

		public SerializedLocalizedConversation Handholding;

		public SerializedLocalizedConversation HandholdingOver15;

		public SerializedLocalizedConversation HandholdingOver60;

		public SerializedLocalizedConversation FaceStroke;

		public SerializedLocalizedConversation FaceStrokeAffection;

		public SerializedLocalizedConversation FaceStrokeAffectionLong;

		public SerializedLocalizedConversation FaceStrokeAffectionVeryLong;

		public SerializedLocalizedConversation FaceStrokeLove;

		public SerializedLocalizedConversation GreetingFresh;

		public SerializedLocalizedConversation GreetingRecentSexCumInside;

		public SerializedLocalizedConversation GreetingRecentSexCumInAss;

		public SerializedLocalizedConversation GreetingHighLonging;

		public SerializedLocalizedConversation GreetingWokeUpEarly;

		public SerializedLocalizedConversation GreetingWokeUpLate;

		public SerializedLocalizedConversation GreetingLongTimeNoSee;

		public SerializedLocalizedConversation GreetingWokeUpLastSleptWithBot;

		public SerializedLocalizedConversation GreetingGenericBeforeLike;

		public SerializedLocalizedConversation GreetingGenericLike;

		public SerializedLocalizedConversation GreetingGenericAffection;

		public SerializedLocalizedConversation GreetingGenericLove;

		public SerializedLocalizedConversation GreetingGenericSuperLove;

		public SerializedLocalizedConversation GreetingLowMentalHealth;

		public SerializedLocalizedConversation GreetingVeryLowMentalHealth;

		public override string ContainerName => "Interact";
	}

	public class InteractDialogueArea : StoryBotDialogueAreaDefinition
	{
		public StoryBotDialogueTagDefinition Greeting;

		public StoryBotDialogueTagDefinition BotStart;

		public StoryBotDialogueTagDefinition Headpat;

		public StoryBotDialogueTagDefinition Handholding;

		public StoryBotDialogueTagDefinition FaceStroke;

		public override string GetPrefix()
		{
			return "Interact_";
		}

		protected override void InitInner(StorySingleton storySingleton, StoryBotDialogueStage stage1, StoryBotDialogueStage stage2, StoryBotDialogueStage stage3)
		{
			InteractConversations interactConversations = SingletonScriptableObject<CFLocalizationDataSO>.Instance.InteractConversations;
			int value = 30;
			Greeting.CreateContainer(stage3, out var o);
			o.AddMultipleBranchesWithSameCondition(StoryCondition.CommonConditions.SympathyAffection, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(0), interactConversations.GreetingLongTimeNoSee).AddConditions(StoryCondition.VariableCondition("TimeSinceLastSawBot", DataConditionComparators.SingleArgumentComparators.Above, 2000)).SetPriority(50), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(50), interactConversations.GreetingRecentSexCumInside).AddConditions(StoryCondition.CommonConditions.RecentlySex, StoryCondition.VariableCondition("CumInside", DataConditionComparators.SingleArgumentComparators.Above, 5f)).SetOdds(3f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(51), interactConversations.GreetingRecentSexCumInAss).AddConditions(StoryCondition.CommonConditions.RecentlySex, StoryCondition.VariableCondition("CumInsideAnal", DataConditionComparators.SingleArgumentComparators.Above, 5f)).SetOdds(3f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(52), interactConversations.GreetingHighLonging).AddConditions(StoryCondition.VariableCondition("Longing", DataConditionComparators.SingleArgumentComparators.Above, 6f), StoryCondition.VariableCondition("TimeSinceLastFuck", DataConditionComparators.SingleArgumentComparators.Above, 480)).SetOdds(3f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(60), interactConversations.GreetingLowMentalHealth).AddConditions(StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 0.3f, 0.4f)).SetOdds(5f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(61), interactConversations.GreetingVeryLowMentalHealth).AddConditions(StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.3f)).SetOdds(5f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(100), interactConversations.GreetingWokeUpEarly).AddConditions(StoryCondition.CommonConditions.RecentlyWokeUp, StoryCondition.CommonConditions.LastSleptAlone, StoryCondition.HourBetweenEqual(2, 6)).SetOdds(5f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(101), interactConversations.GreetingWokeUpLate).AddConditions(StoryCondition.CommonConditions.RecentlyWokeUp, StoryCondition.CommonConditions.LastSleptAlone, StoryCondition.HourBetweenEqual(13, 16)).SetOdds(5f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(102), interactConversations.GreetingWokeUpLastSleptWithBot).AddConditions(StoryCondition.CommonConditions.RecentlyWokeUp, StoryCondition.CommonConditions.LastSleptWithBot).SetOdds(5f));
			o.AddBranches(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(24), interactConversations.GreetingFresh).AddConditions(StoryCondition.VariableCondition("TimeSinceLastInteract", DataConditionComparators.SingleArgumentComparators.Below, value)).SetPriority(5), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(150), interactConversations.GreetingGenericBeforeLike).AddConditions(StoryCondition.CommonConditions.SympathyBeforeLikeOnly), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(151), interactConversations.GreetingGenericLike).AddConditions(StoryCondition.CommonConditions.SympathyLikeOnly), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(152), interactConversations.GreetingGenericAffection).AddConditions(StoryCondition.CommonConditions.SympathyAffectionOnly), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(153), interactConversations.GreetingGenericLove).AddConditions(StoryCondition.CommonConditions.SympathyLoveOnly), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GreetingDialogues.GetGameId(154), interactConversations.GreetingGenericSuperLove).AddConditions(StoryCondition.CommonConditions.SympathySuperLoveOnly));
			Headpat.CreateContainer(stage2, out var o2);
			o2.AddMultipleBranchesWithoutConditions(new StoryBotDialogueBranch(Stage2VanillaDialogueIds.HeadpatDialogues.GetGameId(0), LOC.Other.TupleReferences.Stage2Headpat1), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.HeadpatDialogues.GetGameId(1), LOC.Other.TupleReferences.Stage2Headpat2), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.HeadpatDialogues.GetGameId(2), LOC.Other.TupleReferences.Stage2Headpat3));
			InteractionMemoryCondition interactionMemoryCondition = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Headpat, 10f);
			InteractionMemoryCondition interactionMemoryCondition2 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Headpat, 30f);
			InteractionMemoryCondition interactionMemoryCondition3 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Headpat, 60f);
			Headpat.CreateContainer(stage3, out var o3);
			o3.AddMultipleBranchesWithSameCondition(StoryCondition.CommonConditions.SympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HeadpatDialogues.GetGameId(3), interactConversations.Headpat), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HeadpatDialogues.GetGameId(4), interactConversations.HeadpatOver10).AddConditions(interactionMemoryCondition).SetOdds(2f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HeadpatDialogues.GetGameId(5), interactConversations.HeadpatOver30).AddConditions(interactionMemoryCondition2).SetOdds(4f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HeadpatDialogues.GetGameId(6), interactConversations.HeadpatOver60).AddConditions(interactionMemoryCondition3).SetOdds(8f));
			InteractionMemoryCondition interactionMemoryCondition4 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Handholding, 15f);
			InteractionMemoryCondition interactionMemoryCondition5 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Handholding, 60f);
			Handholding.CreateContainer(stage3, out var o4);
			o4.AddMultipleBranchesWithSameCondition(StoryCondition.CommonConditions.SympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HandholdingDialogues.GetGameId(0), interactConversations.Handholding), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HandholdingDialogues.GetGameId(1), interactConversations.HandholdingOver15).AddConditions(interactionMemoryCondition4).SetOdds(4f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HandholdingDialogues.GetGameId(2), interactConversations.HandholdingOver60).AddConditions(interactionMemoryCondition5).SetOdds(8f));
			InteractionMemoryCondition interactionMemoryCondition6 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.FaceStroke, 15f);
			InteractionMemoryCondition interactionMemoryCondition7 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.FaceStroke, 60f);
			FaceStroke.CreateContainer(stage3, out var o5);
			o5.AddMultipleBranchesWithSameCondition(StoryCondition.CommonConditions.SympathyLike, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.FaceStrokeDialogues.GetGameId(1), interactConversations.FaceStroke));
			o5.AddMultipleBranchesWithSameCondition(StoryCondition.CommonConditions.SympathyAffection, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.FaceStrokeDialogues.GetGameId(2), interactConversations.FaceStrokeAffection));
			o5.AddMultipleBranchesWithSameCondition(StoryCondition.CommonConditions.SympathyAffection, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.FaceStrokeDialogues.GetGameId(3), interactConversations.FaceStrokeAffectionLong).AddConditions(interactionMemoryCondition6));
			o5.AddMultipleBranchesWithSameCondition(StoryCondition.CommonConditions.SympathyAffection, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.FaceStrokeDialogues.GetGameId(4), interactConversations.FaceStrokeAffectionVeryLong).AddConditions(interactionMemoryCondition7));
			o5.AddMultipleBranchesWithSameCondition(StoryCondition.CommonConditions.SympathyLove, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.FaceStrokeDialogues.GetGameId(5), interactConversations.FaceStrokeLove));
		}
	}

	private static readonly ReactionManager<NewInteractBrainState> ReactionManager;

	private ReactionManagerData _reactionManagerData;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _handholdingLInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _handholdingRInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _headpatInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _pantiesInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _skirtInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _faceInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _openMouth;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _closeMouth;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _suckTongue;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _kissMouth;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _thumbMouth;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _boobL;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _boobR;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _boobsBoth;

	public override bool IsInterested => _reactionManagerData.HasReactedRecently();

	public bool AlreadyDidGreeting { get; set; }

	public virtual StoryBotDialogueTagDefinition HeadpatTag => Dialogue.Headpat;

	public virtual StoryBotDialogueTagDefinition HandholdingTag => Dialogue.Handholding;

	public virtual StoryBotDialogueTagDefinition FaceStrokeTag => Dialogue.FaceStroke;

	private InteractDialogueArea Dialogue => SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<InteractDialogueArea>();

	static NewInteractBrainState()
	{
		ReactionManager = new ReactionManager<NewInteractBrainState>(new Dictionary<InteractionMemoryEnum, ReactionManager<NewInteractBrainState>.Reaction>
		{
			[InteractionMemoryEnum.Handholding] = (NewInteractBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake)
				{
					if (memory.Length > 2f)
					{
						gameVariables.sympathy++;
						gameVariables.lust++;
					}
					if (memory.Length > 1f && UnityEngine.Random.value > 0.7f && caller.Owner.NotRecentlyStartedTalking)
					{
						caller.Dialogue.Handholding.StartDialogueFromModelBrainState(caller);
					}
					if (memory.Length > 5f)
					{
						if (UnityEngine.Random.Range(0f, 1f) < 0.2f)
						{
							caller.controller.Eyes.ToDefaultEyesPosition();
						}
						else
						{
							caller.controller.Eyes.TryLookAtCurrentInteractable();
						}
					}
				}
			},
			[InteractionMemoryEnum.BoobsFondle] = (NewInteractBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake)
				{
					if (memory.Length > 2f)
					{
						gameVariables.lust++;
					}
					if (memory.Length > 1f && UnityEngine.Random.value > 0.7f && caller.Owner.NotRecentlyStartedTalking)
					{
						SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<FrickDialogueArea>().BoobFondle.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.FaceStroke] = (NewInteractBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake)
				{
					if (memory.Length > 2f)
					{
						gameVariables.sympathy++;
						gameVariables.lust++;
					}
					if (memory.Length > 1f && UnityEngine.Random.value > 0.7f && caller.Owner.NotRecentlyStartedTalking)
					{
						caller.Dialogue.FaceStroke.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.KissMouth] = (NewInteractBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake)
				{
					float length = memory.Length;
					if (length > 2f)
					{
						gameVariables.sympathy++;
					}
					if (length > 5f)
					{
						gameVariables.lust++;
					}
					if (length < 5f)
					{
						float x = UnityEngine.Random.Range(-0.8f, 0.8f);
						float y = -0.5f;
						if (UnityEngine.Random.Range(0f, 1f) < 0.9f)
						{
							x = 0f;
							y = 0f;
						}
						caller.controller.Eyes.SetEyesPosition(x, y);
					}
					else if (length < 10f)
					{
						caller.controller.Eyes.SetEyesPosition(0f, 0f);
					}
					else if (!caller.controller.Eyes.DisableBlinking)
					{
						caller.controller.Eyes.DisableBlinking = true;
						caller.controller.Eyes.CloseEyes();
						memory.OnDeactivate += EndKissing;
					}
				}
				void EndKissing()
				{
					memory.OnDeactivate -= EndKissing;
					caller.controller.Eyes.OpenEyes();
					caller.controller.Eyes.DisableBlinking = false;
				}
			},
			[InteractionMemoryEnum.KissBody] = (NewInteractBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (memory.TryGetAdditionalDataAs<StoryBotDialogueTagDefinition>(out var activateData) && caller.Owner.NotRecentlyStartedTalking && UnityEngine.Random.value > 0.4f)
					{
						activateData.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.Headpat] = (NewInteractBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake)
				{
					if (memory.Length > 2f)
					{
						gameVariables.sympathy++;
						gameVariables.lust++;
					}
					if (memory.Length > 1f && UnityEngine.Random.value > 0.7f && caller.Owner.NotRecentlyStartedTalking)
					{
						StoryBotDialogueTagDefinition headpat = caller.Dialogue.Headpat;
						if (headpat != null && headpat.StartDialogueFromModelBrainState(caller))
						{
							gameVariables.sympathy++;
						}
					}
					caller.controller.Eyes.SetEyesPosition(UnityEngine.Random.Range(-0.8f, 0.8f), -0.5f);
				}
			}
		});
	}

	public override void EnterState()
	{
		base.EnterState();
		Live2DController.PrepareCommonHeadpatInteractables(controller, controller.HitAreaHeadpat, out _headpatInteractable);
		Live2DController.PrepareCommonMouthInteractables(controller, out _openMouth, out _closeMouth);
		Live2DController.PrepareCommonMouthKissInteractables(controller, out _suckTongue, out _kissMouth);
		Live2DInteractionController.PrepareThumbMouthInteractables(controller, out _thumbMouth);
		Live2DInteractionController.PrepareHandholdingInteractable(controller, out _handholdingLInteractable, out _handholdingRInteractable);
		Live2DInteractionController.PrepareFaceRubInteractable(controller, out _faceInteractable);
		Live2DInteractionController.PreparePantiesAndSkirt(controller, out _pantiesInteractable, out _skirtInteractable);
		Live2DInteractionController.PrepareBoobsInteractables(controller, out _boobL, out _boobR, out _boobsBoth);
	}

	public override void ExitState()
	{
		base.ExitState();
		controller.RemoveInteractable(_thumbMouth);
		controller.RemoveInteractable(_openMouth);
		controller.RemoveInteractable(_closeMouth);
		controller.RemoveInteractable(_suckTongue);
		controller.RemoveInteractable(_kissMouth);
		controller.RemoveInteractable(_headpatInteractable);
		controller.RemoveInteractable(_handholdingLInteractable);
		controller.RemoveInteractable(_handholdingRInteractable);
		controller.RemoveInteractable(_pantiesInteractable);
		controller.RemoveInteractable(_skirtInteractable);
		controller.RemoveInteractable(_faceInteractable);
		controller.RemoveInteractable(_boobL);
		controller.RemoveInteractable(_boobR);
		controller.RemoveInteractable(_boobsBoth);
	}

	private IEnumerator ReactionBored()
	{
		controller.ParamHeadX.ResetToDefault();
		controller.ParamHeadY.ResetToDefault();
		controller.ParamHeadZ.ResetToDefault();
		controller.ParamHeadY.AddPercent(-0.2f);
		while (true)
		{
			float num = UnityEngine.Random.Range(0f, 1f);
			float num2;
			float num3;
			if (num < 0.2f)
			{
				num2 = 0f;
				num3 = 0f;
			}
			else if (num < 0.4f)
			{
				(num2, num3) = controller.Eyes.GetMouseOffsetFromEyes().Deconstruct();
			}
			else
			{
				num2 = UnityEngine.Random.Range(-0.5f, 0.5f);
				num3 = UnityEngine.Random.Range(-0.5f, 0.2f);
			}
			controller.ParamHeadX.Value = num2;
			controller.ParamHeadY.Value = num3;
			controller.Eyes.SetEyesPosition(num2, num3);
			yield return new WaitForSeconds(UnityEngine.Random.Range(4f, 10f));
		}
	}

	private IEnumerator ReactionResetPosition()
	{
		if (!controller.IsApproximatelyDefaultFaceAngle())
		{
			yield return WaitForSecondCache.Seconds0_5;
			controller.ToDefaultFaceAngle();
		}
		yield return WaitForSecondCache.Seconds0_2;
		controller.Eyes.ToDefaultEyesPosition();
		yield return WaitForSecondCache.Seconds1;
	}

	protected override IEnumerator Brain()
	{
		yield return null;
		if (!AlreadyDidGreeting)
		{
			yield return WaitForSecondCache.Seconds0_1;
			AlreadyDidGreeting = true;
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Mood > -0.1f)
			{
				Dialogue.Greeting.StartDialogueFromModelBrainState(this);
			}
		}
		while (true)
		{
			yield return DoReaction(ReactionResetPosition());
			for (int i = 0; i < 6; i++)
			{
				while (Owner.IsInterested)
				{
					SyncAction();
					i = 0;
					yield return WaitForSecondCache.Seconds1;
				}
				yield return WaitForSecondCache.Seconds1;
			}
			if (GameVariables.Current.IsBotAwake)
			{
				DoReaction(ReactionBored());
				while (!Owner.IsInterested)
				{
					yield return null;
				}
				StopReaction();
			}
		}
	}

	private void TryStartTalk()
	{
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Mood < -0.5f)
		{
			return;
		}
		int timeSinceBotLastStartedTalk = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.TimeSinceBotLastStartedTalk;
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotAwake && timeSinceBotLastStartedTalk > 60 && Dialogue.BotStart.TryGetTag(out var o) && o.GetDialogueForModelBrainState(this, out var branch) && (timeSinceBotLastStartedTalk >= 120 || branch.IsOneTime))
		{
			Debug.Log("Chosen: " + branch.Summary());
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.TryGetStateAs<InteractState>(out var state))
			{
				state.StartDialogue(branch);
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lastBotStartedTalkAt = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
			}
		}
	}

	private void SyncAction()
	{
		if (!ReactionManager.React(ref _reactionManagerData, Owner.InteractionMemory, this) && Owner.NotRecentlyStartedTalking)
		{
			TryStartTalk();
		}
	}
}
