using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Live2D.Cubism.Core;
using StoryConditions;
using UnityEngine;

public class CuddleBrainState : ModelBrainState<Live2DInteractionController>, IHasNonJitClassDescription
{
	[Serializable]
	public class CuddleConversations : LocalizedStringReferenceContainer
	{
		public SerializedLocalizedConversation BeginningNormal;

		public SerializedLocalizedConversation BeginningTired;

		public SerializedLocalizedConversation BeginningNotTired;

		public SerializedLocalizedConversation FarRandomNormal;

		public SerializedLocalizedConversation FarRandomUniqueRiots;

		public SerializedLocalizedConversation FarRandomSlept;

		public SerializedLocalizedConversation CloseRandomNormal;

		public SerializedLocalizedConversation CloseRandomLowMentalHealth;

		public SerializedLocalizedConversation CloseRandomSlept;

		public SerializedLocalizedConversation Headpat;

		public SerializedLocalizedConversation HeadpatOver5;

		public SerializedLocalizedConversation HeadpatOver15;

		public SerializedLocalizedConversation HeadpatOver30;

		public SerializedLocalizedConversation BeforeGotCloseNormal;

		public SerializedLocalizedConversation BeforeGotCloseSlept;

		public SerializedLocalizedConversation BeforeGotCloseMeme;

		public SerializedLocalizedConversation BeforeMoveAwayNormal;

		public SerializedLocalizedConversation BeforeMoveAwaySlept;

		public SerializedLocalizedConversation HeadpattingAnonStart;

		public SerializedLocalizedConversation HeadpattingAnonStartAnonAsked;

		public SerializedLocalizedConversation HeadpattingAnonContinued;

		public SerializedLocalizedConversation AskedToStopHeadpattingAnon;

		public SerializedLocalizedConversation StoppedHeadpattingAnon;

		public SerializedLocalizedConversation BeforeFallAsleep;

		public SerializedLocalizedConversation BeforeFallAsleepSlept;

		public SerializedLocalizedConversation AfterWakeUpGeneric;

		public SerializedLocalizedConversation AfterWakeUpNight;

		public SerializedLocalizedConversation AfterWakeUpEarly;

		public SerializedLocalizedConversation AfterWakeUpEvening;

		public SerializedLocalizedConversation AfterWakeUpAfterNoon;

		public SerializedLocalizedConversation AfterWakeUpNoon;

		public SerializedLocalizedConversation Handholding;

		public SerializedLocalizedConversation HandholdingOver10;

		public SerializedLocalizedConversation HandholdingOver30;

		public SerializedLocalizedConversation FaceStroke;

		public SerializedLocalizedConversation FaceStrokeOver10;

		public SerializedLocalizedConversation FaceStrokeOver30;

		public SerializedLocalizedConversation AnonCantSleepNotTired;

		public SerializedLocalizedConversation AnonFellAsleep;

		public SerializedLocalizedConversation AnonAskForHeadpats;

		public SerializedLocalizedConversation AnonAskForSomethingNice;

		public SerializedLocalizedConversation NiceThing;

		public SerializedLocalizedConversation NiceThingRiotsActive;

		public SerializedLocalizedConversation NiceThingSlept;

		public SerializedLocalizedConversation NiceThingLowMentalHealth;

		public SerializedLocalizedConversation NiceThingLonging;

		public SerializedLocalizedConversation NiceThingRecentSex;

		public override string ContainerName => "Cuddle";
	}

	public class CuddleDialogue : StoryBotDialogueAreaDefinition
	{
		public StoryBotDialogueTagDefinition Beginning;

		public StoryBotDialogueTagDefinition FarRandom;

		public StoryBotDialogueTagDefinition CloseRandom;

		public StoryBotDialogueTagDefinition Headpat;

		public StoryBotDialogueTagDefinition BeforeGotClose;

		public StoryBotDialogueTagDefinition BeforeMoveAway;

		public StoryBotDialogueTagDefinition HeadpattingAnonStart;

		public StoryBotDialogueTagDefinition HeadpattingAnonStartAnonAsked;

		public StoryBotDialogueTagDefinition HeadpattingAnonContinued;

		public StoryBotDialogueTagDefinition AskedToStopHeadpattingAnon;

		public StoryBotDialogueTagDefinition StoppedHeadpattingAnon;

		public StoryBotDialogueTagDefinition AfterWakeUp;

		public StoryBotDialogueTagDefinition BeforeFallAsleep;

		public StoryBotDialogueTagDefinition Handholding;

		public StoryBotDialogueTagDefinition FaceStroke;

		[StorySingleton.PersonalitySourceIgnore]
		public StoryBotDialogueTagDefinition AnonCantSleepNotTired;

		[StorySingleton.PersonalitySourceIgnore]
		public StoryBotDialogueTagDefinition AnonFellAsleep;

		[StorySingleton.PersonalitySourceIgnore]
		public StoryBotDialogueTagDefinition AnonAskForHeadpats;

		[StorySingleton.PersonalitySourceIgnore]
		public StoryBotDialogueTagDefinition AnonAskForSomethingNice;

		public StoryBotDialogueTagDefinition NiceThing;

		public override string GetPrefix()
		{
			return "Cuddle_";
		}

		protected override void InitInner(StorySingleton storySingleton, StoryBotDialogueStage stage1, StoryBotDialogueStage stage2, StoryBotDialogueStage stage3)
		{
			Beginning.CreateContainer(stage3, out var o, StoryBotContainerTypeEnum.Prologue);
			o.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(0), FungusBlocks.Stage3_CuddleFirst).MarkOneTime());
			DataCondition<bool> condition = StoryCondition.ModelBrainStateVariableCondition<bool, CuddleBrainState>("HasSlept", DataConditionComparators.SingleArgumentComparators.Equal, value: false);
			DataCondition<bool> dataCondition = StoryCondition.ModelBrainStateVariableCondition<bool, CuddleBrainState>("HasSlept", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
			CuddleConversations cuddleConversations = SingletonScriptableObject<CFLocalizationDataSO>.Instance.CuddleConversations;
			Beginning.CreateContainer(stage3, out var o2);
			o2.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(10), cuddleConversations.BeginningNormal));
			o2.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(11), cuddleConversations.BeginningTired).SetMaxOdds().AddConditions(StoryCondition.VariableCondition("Stamina", DataConditionComparators.SingleArgumentComparators.Below, 0.4f)));
			o2.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(12), cuddleConversations.BeginningNotTired).SetMaxOdds().AddConditions(StoryCondition.VariableCondition("Stamina", DataConditionComparators.SingleArgumentComparators.Above, 0.7f)));
			FarRandom.CreateContainer(stage3, out var o3);
			o3.AddMultipleBranchesWithSameCondition(condition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(200), cuddleConversations.FarRandomNormal));
			o3.AddMultipleBranchesWithSameCondition(dataCondition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(201), cuddleConversations.FarRandomSlept));
			o3.AddBranches(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(210), cuddleConversations.FarRandomUniqueRiots).AddConditions(StoryCondition.CommonConditions.RiotsActive).MarkOneTime().SetOdds(500f));
			CloseRandom.CreateContainer(stage3, out var o4);
			o4.AddMultipleBranchesWithSameCondition(condition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(300), cuddleConversations.CloseRandomNormal));
			o4.AddMultipleBranchesWithSameCondition(dataCondition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(301), cuddleConversations.CloseRandomSlept));
			o4.AddBranches(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(310), cuddleConversations.CloseRandomLowMentalHealth).AddConditions(StoryCondition.CommonConditions.LowMentalHealth));
			StoryCondition storyCondition = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Headpat, 5f);
			StoryCondition storyCondition2 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Headpat, 15f);
			StoryCondition storyCondition3 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Headpat, 30f);
			Headpat.CreateContainer(stage3, out var o5);
			o5.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(400), cuddleConversations.Headpat));
			o5.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(401), cuddleConversations.HeadpatOver5).AddConditions(storyCondition).SetOdds(10f));
			o5.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(402), cuddleConversations.HeadpatOver15).AddConditions(storyCondition2).SetOdds(50f));
			o5.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(403), cuddleConversations.HeadpatOver30).AddConditions(storyCondition3).SetOdds(100f));
			BeforeGotClose.CreateContainer(stage3, out var o6);
			o6.AddMultipleBranchesWithSameCondition(condition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(500), cuddleConversations.BeforeGotCloseNormal), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(501), cuddleConversations.BeforeGotCloseMeme).SetOdds(0.01f));
			o6.AddMultipleBranchesWithSameCondition(dataCondition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(502), cuddleConversations.BeforeGotCloseSlept));
			BeforeMoveAway.CreateContainer(stage3, out var o7);
			o7.AddMultipleBranchesWithSameCondition(condition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(600), cuddleConversations.BeforeMoveAwayNormal));
			o7.AddMultipleBranchesWithSameCondition(dataCondition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(601), cuddleConversations.BeforeMoveAwaySlept));
			HeadpattingAnonStart.CreateContainer(stage3, out var o8);
			o8.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(700), cuddleConversations.HeadpattingAnonStart));
			HeadpattingAnonStartAnonAsked.CreateContainer(stage3, out var o9);
			o9.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(705), cuddleConversations.HeadpattingAnonStartAnonAsked));
			HeadpattingAnonContinued.CreateContainer(stage3, out var o10);
			o10.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(710), cuddleConversations.HeadpattingAnonContinued));
			AskedToStopHeadpattingAnon.CreateContainer(stage3, out var o11);
			o11.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(720), cuddleConversations.AskedToStopHeadpattingAnon));
			StoppedHeadpattingAnon.CreateContainer(stage3, out var o12);
			o12.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(730), cuddleConversations.StoppedHeadpattingAnon));
			AfterWakeUp.CreateContainer(stage3, out var o13);
			o13.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(740), cuddleConversations.AfterWakeUpGeneric).SetOdds(0.1f));
			o13.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(741), cuddleConversations.AfterWakeUpNight).AddConditions(StoryCondition.HourBetweenEqual(23, 3)));
			o13.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(742), cuddleConversations.AfterWakeUpEarly).AddConditions(StoryCondition.HourBetweenEqual(4, 8)));
			o13.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(743), cuddleConversations.AfterWakeUpNoon).AddConditions(StoryCondition.HourBetweenEqual(9, 12)));
			o13.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(744), cuddleConversations.AfterWakeUpAfterNoon).AddConditions(StoryCondition.HourBetweenEqual(13, 18)));
			o13.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(745), cuddleConversations.AfterWakeUpEvening).AddConditions(StoryCondition.HourBetweenEqual(19, 22)));
			AnonCantSleepNotTired.FromConversation(stage3, Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(755), cuddleConversations.AnonCantSleepNotTired);
			BeforeFallAsleep.CreateContainer(stage3, out var o14);
			o14.AddMultipleBranchesWithSameCondition(condition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(760), cuddleConversations.BeforeFallAsleep));
			o14.AddMultipleBranchesWithSameCondition(dataCondition, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(761), cuddleConversations.BeforeFallAsleepSlept));
			StoryCondition condition2 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Handholding, 10f);
			StoryCondition condition3 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Handholding, 30f);
			Handholding.CreateContainer(stage3, out var o15);
			o15.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(900), cuddleConversations.Handholding));
			o15.AddMultipleBranchesWithSameCondition(condition2, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(901), cuddleConversations.HandholdingOver10).SetOdds(10f));
			o15.AddMultipleBranchesWithSameCondition(condition3, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(902), cuddleConversations.HandholdingOver30).SetOdds(100f));
			StoryCondition condition4 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.FaceStroke, 10f);
			StoryCondition condition5 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.FaceStroke, 30f);
			FaceStroke.CreateContainer(stage3, out var o16);
			o16.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(950), cuddleConversations.FaceStroke));
			o16.AddMultipleBranchesWithSameCondition(condition4, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(951), cuddleConversations.FaceStrokeOver10).SetOdds(10f));
			o16.AddMultipleBranchesWithSameCondition(condition5, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(952), cuddleConversations.FaceStrokeOver30).SetOdds(100f));
			AnonFellAsleep.CreateContainer(stage3, out var o17);
			o17.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(960), cuddleConversations.AnonFellAsleep));
			AnonAskForHeadpats.CreateContainer(stage3, out var o18);
			o18.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(970), cuddleConversations.AnonAskForHeadpats));
			AnonAskForSomethingNice.CreateContainer(stage3, out var o19);
			o19.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(979), cuddleConversations.AnonAskForSomethingNice));
			NiceThing.CreateContainer(stage3, out var o20);
			o20.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(980), cuddleConversations.NiceThing).SetOdds(2f));
			o20.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(981), cuddleConversations.NiceThingRiotsActive).AddConditions(StoryCondition.CommonConditions.RiotsActive));
			o20.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(982), cuddleConversations.NiceThingSlept).AddConditions(dataCondition));
			o20.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(983), cuddleConversations.NiceThingLowMentalHealth).AddConditions(StoryCondition.CommonConditions.LowMentalHealth));
			o20.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(984), cuddleConversations.NiceThingLonging).AddConditions(StoryCondition.CommonConditions.VeryHighLonging).SetOdds(0.1f));
			o20.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.CuddleDialogues.GetGameId(985), cuddleConversations.NiceThingRecentSex).AddConditions(StoryCondition.CommonConditions.RecentlySex).SetOdds(0.1f));
		}
	}

	public enum CuddleReactions
	{
		Nothing,
		ComeCloser,
		MoveAway,
		BotHeadpatAnonSelf,
		BotHeadpatAnonAsked,
		Sleep,
		SaySomethingNice
	}

	public enum CuddleState
	{
		Far,
		Close
	}

	private static readonly ReactionManager<CuddleBrainState> ReactionManager;

	private ReactionManagerData _reactionManagerData;

	private Live2DInteractable _blanketInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _faceInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _handInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _headpatInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _mouthOpen;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _mouthClose;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _tongueSuck;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _mouthKiss;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _thumbMouth;

	public override bool IsInterested => _reactionManagerData.HasReactedRecently();

	public virtual StoryBotDialogueTagDefinition HeadpatTag => Dialogue.Headpat;

	public virtual StoryBotDialogueTagDefinition HandholdingTag => Dialogue.Handholding;

	public virtual StoryBotDialogueTagDefinition FaceStrokeTag => Dialogue.FaceStroke;

	private CuddleDialogue Dialogue => SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<CuddleDialogue>();

	public bool IsFree => !IsReactingUnstoppable;

	public bool IsReactionAnonHeadPats
	{
		get
		{
			if (CurrentReaction != CuddleReactions.BotHeadpatAnonAsked)
			{
				return CurrentReaction == CuddleReactions.BotHeadpatAnonSelf;
			}
			return true;
		}
	}

	public CuddleState CurrentState { get; private set; }

	public CuddleReactions CurrentReaction { get; private set; }

	public bool HasGreeted { get; private set; }

	public bool HasSlept => SleptForMinutes > 0;

	public int SleptForMinutes { get; private set; }

	public int CuddleStartedAtTime { get; private set; }

	public int CuddleLength => GameVariables.Current.time - CuddleStartedAtTime;

	public int CuddleLengthWithoutSleeping => Mathf.Max(GameVariables.Current.time - CuddleStartedAtTime - SleptForMinutes, 0);

	public bool ShouldTimePass => CurrentReaction != CuddleReactions.Sleep;

	static CuddleBrainState()
	{
		ReactionManager = new ReactionManager<CuddleBrainState>(new Dictionary<InteractionMemoryEnum, ReactionManager<CuddleBrainState>.Reaction>
		{
			[InteractionMemoryEnum.Handholding] = (CuddleBrainState caller, ComplexEventMemory memory, int row) =>
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
			[InteractionMemoryEnum.FaceStroke] = (CuddleBrainState caller, ComplexEventMemory memory, int row) =>
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
			[InteractionMemoryEnum.KissMouth] = (CuddleBrainState caller, ComplexEventMemory memory, int row) =>
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
			[InteractionMemoryEnum.KissBody] = (CuddleBrainState caller, ComplexEventMemory memory, int row) =>
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
			[InteractionMemoryEnum.Headpat] = (CuddleBrainState caller, ComplexEventMemory memory, int row) =>
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
		_blanketInteractable = controller.AddInteractable("BlanketCuddle", new Live2DInteractable
		{
			OnHoldStart = (CubismDrawable d) =>
			{
			},
			OnHeld = (Vector2 mouseMove) =>
			{
				controller.ParamBlanketCuddleMove.AddPercent((0f - mouseMove.x) * 20f);
				controller.ParamBlanketCuddleMove.Value = Mathf.Clamp(controller.ParamBlanketCuddleMove.Value, 0.65f, 1f);
			},
			OnHeldFixed = () =>
			{
			},
			OnHoldEnd = () =>
			{
			},
			InfoSls = LOC.UI.TupleReferences.Interactable_MoveBlanket,
			ActiveFunc = IsNotClose
		});
		Live2DController.PrepareCommonMouthInteractables(controller, out _mouthOpen, out _mouthClose);
		Live2DController.PrepareCommonMouthKissInteractables(controller, out _tongueSuck, out _mouthKiss);
		Live2DInteractionController.PrepareThumbMouthInteractables(controller, out _thumbMouth);
		Live2DInteractionController.PrepareFaceRubInteractable(controller, out _faceInteractable);
		Live2DInteractionController.PrepareHandHoldingCuddleInteractable(controller, out _handInteractable);
		Live2DInteractionController.PrepareCuddleHeadpatInteractable(controller, out _headpatInteractable);
		_headpatInteractable.ActiveFunc = (Live2DInteractionController c) => IsNotClose();
		CuddleStartedAtTime = GameVariables.Current.time;
	}

	private bool IsNotClose()
	{
		return CurrentState != CuddleState.Close;
	}

	public override void ExitState()
	{
		base.ExitState();
		controller.RemoveInteractable(_thumbMouth);
		controller.RemoveInteractable(_tongueSuck);
		controller.RemoveInteractable(_mouthKiss);
		controller.RemoveInteractable(_mouthOpen);
		controller.RemoveInteractable(_mouthClose);
		controller.RemoveInteractable(_blanketInteractable);
		controller.RemoveInteractable(_faceInteractable);
		controller.RemoveInteractable(_handInteractable);
		controller.RemoveInteractable(_headpatInteractable);
	}

	private void AddStopHeadPatsOption(List<MenuOption> menuOption)
	{
		menuOption.Add(new MenuOption(LOC.UI.Cuddle_AskToStopHeadpats, () =>
		{
			UiOverlay.Instance.CloseFungusOverlay();
			if (IsReactionAnonHeadPats)
			{
				CurrentReaction = CuddleReactions.Nothing;
			}
		}));
	}

	public void FillActionOptions(List<MenuOption> menuOption)
	{
		if (IsReactionAnonHeadPats)
		{
			AddStopHeadPatsOption(menuOption);
		}
		else
		{
			if (IsReactingUnstoppable)
			{
				return;
			}
			menuOption.Add(new MenuOption(LOC.UI.Cuddle_GoToSleep, () =>
			{
				UiOverlay.Instance.CloseFungusOverlay();
				React(CuddleReactions.Sleep);
			}));
			CuddleState currentState = CurrentState;
			if (currentState == CuddleState.Far || currentState != CuddleState.Close)
			{
				menuOption.Add(new MenuOption(LOC.UI.Cuddle_GetCloser, () =>
				{
					UiOverlay.Instance.CloseFungusOverlay();
					React(CuddleReactions.ComeCloser);
				}));
			}
			else
			{
				menuOption.Add(new MenuOption(LOC.UI.Cuddle_MoveAway, () =>
				{
					UiOverlay.Instance.CloseFungusOverlay();
					React(CuddleReactions.MoveAway);
				}));
			}
		}
	}

	public void FillTalkOptions(List<MenuOption> menuOption)
	{
		if (IsReactionAnonHeadPats)
		{
			AddStopHeadPatsOption(menuOption);
		}
		else if (!IsReactingUnstoppable)
		{
			menuOption.Add(new MenuOption(LOC.UI.Cuddle_AskToSaySomethingNice, () =>
			{
				UiOverlay.Instance.CloseFungusOverlay();
				React(CuddleReactions.SaySomethingNice);
			}));
			_ = CurrentState;
			menuOption.Add(new MenuOption(LOC.UI.Cuddle_AskForHeadpats, () =>
			{
				UiOverlay.Instance.CloseFungusOverlay();
				React(CuddleReactions.BotHeadpatAnonAsked);
			}));
		}
	}

	public void React(CuddleReactions reaction)
	{
		if (IsReactingUnstoppable)
		{
			return;
		}
		switch (reaction)
		{
		default:
			return;
		case CuddleReactions.ComeCloser:
			DoReactionUnstoppable(GetClose(), (ManagedCoroutine.FinishedType type) =>
			{
				if (type == ManagedCoroutine.FinishedType.Normal)
				{
					controller.SetCurrentToolSelectorActive(value: true);
				}
			});
			break;
		case CuddleReactions.MoveAway:
			DoReactionUnstoppable(MoveAway(), (ManagedCoroutine.FinishedType type) =>
			{
				if (type == ManagedCoroutine.FinishedType.Normal)
				{
					controller.SetCurrentToolSelectorActive(value: true);
				}
			});
			break;
		case CuddleReactions.BotHeadpatAnonSelf:
			DoReactionUnstoppable(BotHeadpatAnonSelf(), (ManagedCoroutine.FinishedType type) =>
			{
				if (IsReactionAnonHeadPats)
				{
					CurrentReaction = CuddleReactions.Nothing;
				}
				if (type == ManagedCoroutine.FinishedType.Normal)
				{
					controller.SetCurrentToolSelectorActive(value: true);
				}
			});
			break;
		case CuddleReactions.BotHeadpatAnonAsked:
			DoReactionUnstoppable(BotHeadpatAnonAsked(), (ManagedCoroutine.FinishedType type) =>
			{
				if (IsReactionAnonHeadPats)
				{
					CurrentReaction = CuddleReactions.Nothing;
				}
				if (type == ManagedCoroutine.FinishedType.Normal)
				{
					controller.SetCurrentToolSelectorActive(value: true);
				}
			});
			break;
		case CuddleReactions.Sleep:
		{
			EyeClosingShader eyeClosingScript = UiOverlay.Instance.MainCamera.GetComponent<EyeClosingShader>();
			DoReactionUnstoppable(GoToSleep(eyeClosingScript), (ManagedCoroutine.FinishedType type) =>
			{
				eyeClosingScript.enabled = false;
				if (type == ManagedCoroutine.FinishedType.Normal)
				{
					controller.SetCurrentToolSelectorActive(value: true);
				}
			}, noWrapper: true);
			break;
		}
		case CuddleReactions.SaySomethingNice:
			DoReactionUnstoppable(SaySomethingNice());
			break;
		}
		CurrentReaction = reaction;
	}

	private IEnumerator SaySomethingNice()
	{
		yield return Dialogue.AnonAskForSomethingNice.StartDialogueFromModelBrainStateAndWait(this);
		yield return Dialogue.NiceThing.StartDialogueFromModelBrainStateAndWait(this);
		GameVariables.Current.MentalHealth += 0.1f;
	}

	private IEnumerator MoveArmUpFromNormal()
	{
		controller.SetArmLCuddle(Live2DInteractionController.ArmLCuddleState.Up);
		yield return WaitForSecondCache.Seconds0_2;
	}

	private IEnumerator MoveArmDownFromNormal()
	{
		controller.SetArmLCuddle(Live2DInteractionController.ArmLCuddleState.Down);
		yield return WaitForSecondCache.Seconds0_2;
	}

	private IEnumerator MoveArmNormalFromUp()
	{
		controller.SetArmLNormal(Live2DInteractionController.ArmState.DownNormal);
		controller.ParamArmLRot.ResetToDefault();
		controller.ParamArmLRot.FinishMove();
		yield return WaitForSecondCache.Seconds0_1;
	}

	private IEnumerator MoveArmNormalFromDown()
	{
		yield return controller.ParamArmLRot.EnumeratorMoveToMaxLinear(0.2f);
		controller.SetArmLNormal(Live2DInteractionController.ArmState.DownNormal);
		controller.ParamArmLRot.ResetToDefault();
		yield return WaitForSecondCache.Seconds0_1;
	}

	private IEnumerator EnsureArmCuddleState(Live2DInteractionController.ArmLCuddleState cuddleState)
	{
		if (controller.ArmLCuddleLState == cuddleState)
		{
			yield break;
		}
		switch (cuddleState)
		{
		case Live2DInteractionController.ArmLCuddleState.Down:
			if (controller.ArmLCuddleLState == Live2DInteractionController.ArmLCuddleState.Up)
			{
				yield return MoveArmNormalFromUp();
			}
			yield return MoveArmDownFromNormal();
			break;
		case Live2DInteractionController.ArmLCuddleState.Normal:
			yield return (controller.ArmLCuddleLState == Live2DInteractionController.ArmLCuddleState.Up) ? MoveArmNormalFromUp() : MoveArmNormalFromDown();
			break;
		case Live2DInteractionController.ArmLCuddleState.Up:
			if (controller.ArmLCuddleLState == Live2DInteractionController.ArmLCuddleState.Down)
			{
				yield return MoveArmNormalFromDown();
			}
			yield return MoveArmUpFromNormal();
			break;
		}
	}

	private IEnumerator GoToSleep(EyeClosingShader eyeClosingScript)
	{
		controller.CurrentToolSelector.gameObject.SetActive(value: false);
		if (GameVariables.Current.Stamina > 0.95f)
		{
			yield return Dialogue.AnonCantSleepNotTired.StartDialogueFromModelBrainStateAndWait(this);
			yield break;
		}
		if (CurrentState == CuddleState.Far)
		{
			yield return EnsureArmCuddleState(Live2DInteractionController.ArmLCuddleState.Up);
			CuddleScene cuddleScene = (CuddleScene)SingletonMonoBehaviour<ViewSingleton>.Instance.CuddleView.Scene;
			var (enumerator, afterFinished) = CoroutineUtilities.PrepareWaitForAction();
			SingletonMonoBehaviour<GameUtilities>.Instance.MoveToView(cuddleScene.ZoomView, afterFinished, 1f, CameraManager.PanType.ConstantHeight);
			CurrentState = CuddleState.Close;
			yield return enumerator;
		}
		if (GameVariables.Current.IsOutOfStamina)
		{
			Dialogue.AnonFellAsleep.StartDialogueFromModelBrainState(this);
			yield return WaitForSecondCache.Seconds0_2;
			yield return eyeClosingScript.QuickFallAsleep();
		}
		else
		{
			yield return Dialogue.BeforeFallAsleep.StartDialogueFromModelBrainStateAndWait(this);
			yield return eyeClosingScript.GoToSleep();
		}
		int timeBefore = GameVariables.Current.time;
		GameVariables.Current.MentalHealth += 0.1f;
		var (enumerator2, finishAction) = CoroutineUtilities.PrepareWaitForAction();
		SingletonLoadableMonoBehaviour<GameScript>.Instance.Sleep(UnityEngine.Random.Range(420, 540), finishAction, suppressEvents: true, GameVariables.StaminaSyncType.Recovery);
		yield return enumerator2;
		SleptForMinutes += GameVariables.Current.time - timeBefore;
		GameVariables.Current.ReportSleepEnd(withBot: true);
		yield return WaitForSecondCache.Seconds4;
		yield return eyeClosingScript.WakeUp();
		yield return Dialogue.AfterWakeUp.StartDialogueFromModelBrainStateAndWait(this);
		CurrentReaction = CuddleReactions.Nothing;
	}

	private IEnumerator GetClose()
	{
		controller.CurrentToolSelector.gameObject.SetActive(value: false);
		yield return Dialogue.BeforeGotClose.StartDialogueFromModelBrainStateAndWait(this);
		yield return EnsureArmCuddleState(Live2DInteractionController.ArmLCuddleState.Up);
		CurrentState = CuddleState.Close;
		CuddleScene cuddleScene = (CuddleScene)SingletonMonoBehaviour<ViewSingleton>.Instance.CuddleView.Scene;
		var (enumerator, afterFinished) = CoroutineUtilities.PrepareWaitForAction();
		SingletonMonoBehaviour<GameUtilities>.Instance.MoveToView(cuddleScene.ZoomView, afterFinished, 1f, CameraManager.PanType.ConstantHeight);
		yield return enumerator;
	}

	private IEnumerator MoveAway()
	{
		controller.CurrentToolSelector.gameObject.SetActive(value: false);
		yield return Dialogue.BeforeMoveAway.StartDialogueFromModelBrainStateAndWait(this);
		CuddleScene cuddleScene = (CuddleScene)SingletonMonoBehaviour<ViewSingleton>.Instance.CuddleView.Scene;
		var (enumerator, afterFinished) = CoroutineUtilities.PrepareWaitForAction();
		SingletonMonoBehaviour<GameUtilities>.Instance.MoveToView(cuddleScene.MainView, afterFinished, 1f, CameraManager.PanType.ConstantHeight);
		yield return enumerator;
		yield return EnsureArmCuddleState(Live2DInteractionController.ArmLCuddleState.Down);
		CurrentState = CuddleState.Far;
	}

	private IEnumerator BotHeadpatAnonAsked()
	{
		controller.SetCurrentToolSelectorActive(value: false);
		yield return Dialogue.AnonAskForHeadpats.StartDialogueFromModelBrainStateAndWait(this);
		Dialogue.HeadpattingAnonStartAnonAsked.StartDialogueFromModelBrainState(this);
		yield return BotHeadpatAnon();
	}

	private IEnumerator BotHeadpatAnonSelf()
	{
		controller.SetCurrentToolSelectorActive(value: false);
		Dialogue.HeadpattingAnonStart.StartDialogueFromModelBrainState(this);
		yield return BotHeadpatAnon();
	}

	private IEnumerator BotHeadpatAnon()
	{
		yield return EnsureArmCuddleState(Live2DInteractionController.ArmLCuddleState.Up);
		Live2DParameter x = controller.ParamAnonHeadpatForearm;
		Live2DParameter xx = controller.ParamAnonHeadpatUpperarm;
		float current = x.Value;
		Transform mainCameraTransform = UiOverlay.Instance.MainCamera.transform;
		Vector3 cameraStartPosition = mainCameraTransform.position;
		float maxInclusive = 30f;
		float headpatLength = 0f;
		float headpatTargetLength = UnityEngine.Random.Range(5f, maxInclusive);
		float lastHeadpatComment = 0f;
		while (IsReactionAnonHeadPats && headpatLength < headpatTargetLength)
		{
			float num = UnityEngine.Random.Range(0.5f, 1f);
			float change;
			if (current > 0.5f)
			{
				change = 0f - num;
			}
			else
			{
				change = num;
			}
			float next = Mathf.Clamp(current + change, -1f, 1f);
			change = next - current;
			num = Mathf.Abs(change);
			float num2 = UnityEngine.Random.Range(0.5f, 0.8f);
			float num3 = num * num2;
			headpatLength += num3;
			yield return CoroutineUtilities.GenericTweenMultiple(num3, x.GetTweenAction(next, ParameterBase.TweenType.EaseOut), xx.GetTweenAction(next, ParameterBase.TweenType.EaseOut), (float num5) =>
			{
				Vector3 position = cameraStartPosition;
				float num4 = Mathf.Sin(num5 * (float)Math.PI);
				position.y += num4 * 0.02f;
				position.x -= num4 * 0.1f * change;
				mainCameraTransform.position = position;
			});
			current = next;
			headpatLength += 0.1f;
			yield return WaitForSecondCache.Seconds0_1;
			if (Owner.PersonalityData.CuddleAnonHeadpatConversationOdds.Check((lastHeadpatComment - headpatLength) / 8f))
			{
				lastHeadpatComment = headpatLength;
				Dialogue.HeadpattingAnonContinued.StartDialogueFromModelBrainState(this);
			}
		}
		GameVariables.Current.MentalHealth += headpatLength * 0.001f;
		yield return EnsureArmCuddleState(Live2DInteractionController.ArmLCuddleState.Down);
		if (IsReactionAnonHeadPats)
		{
			yield return Dialogue.StoppedHeadpattingAnon.StartDialogueFromModelBrainStateAndWait(this);
		}
		else
		{
			yield return Dialogue.AskedToStopHeadpattingAnon.StartDialogueFromModelBrainStateAndWait(this);
		}
	}

	private IEnumerator BrainFar()
	{
		if (IsReacting)
		{
			yield return WaitForSecondCache.Seconds2;
		}
		else if (ReactionManager.React(ref _reactionManagerData, Owner.InteractionMemory, this))
		{
			yield return WaitForSecondCache.Seconds1;
		}
		else if (Owner.PersonalityData.CuddleAnonHeadpatOdds.Check(0f, GameVariables.Current.InverseMentalHealth * 0.5f + 0.5f) && Owner.NotRecentlyStartedTalking)
		{
			React(CuddleReactions.BotHeadpatAnonSelf);
			yield return WaitForSecondCache.Seconds1;
		}
		else if (Owner.PersonalityData.CuddleFarDialogueStartOdds.Check() && Owner.NotRecentlyStartedTalking)
		{
			yield return Dialogue.FarRandom.StartDialogueFromModelBrainStateAndWait(this);
			yield return WaitForSecondCache.Seconds5;
		}
		else
		{
			yield return WaitForSecondCache.Seconds2;
		}
	}

	private IEnumerator BrainClose()
	{
		if (IsReacting)
		{
			yield return WaitForSecondCache.Seconds2;
		}
		else if (ReactionManager.React(ref _reactionManagerData, Owner.InteractionMemory, this))
		{
			yield return WaitForSecondCache.Seconds1;
		}
		else if (Owner.PersonalityData.CuddleCloseDialogueStartOdds.Check() && Owner.NotRecentlyStartedTalking)
		{
			yield return Dialogue.CloseRandom.StartDialogueFromModelBrainStateAndWait(this);
			yield return WaitForSecondCache.Seconds5;
		}
		else
		{
			yield return WaitForSecondCache.Seconds2;
		}
	}

	protected override IEnumerator Brain()
	{
		if (!HasGreeted)
		{
			HasGreeted = true;
			yield return null;
			yield return Dialogue.Beginning.StartDialogueFromModelBrainStateAndWait(this);
		}
		yield return WaitForSecondCache.Seconds4;
		while (true)
		{
			if (GameVariables.Current.IsOutOfStamina)
			{
				React(CuddleReactions.Sleep);
				yield return WaitForSecondCache.Seconds5;
				continue;
			}
			switch (CurrentState)
			{
			case CuddleState.Far:
				yield return BrainFar();
				break;
			case CuddleState.Close:
				yield return BrainClose();
				break;
			default:
				yield return WaitForSecondCache.Seconds2;
				break;
			}
		}
	}
}
