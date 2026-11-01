using StoryConditions;

public class FrickDialogueArea : StoryBotDialogueAreaDefinition
{
	public StoryBotDialogueTagDefinition CumInside;

	public StoryBotDialogueTagDefinition CumInsideAnal;

	public StoryBotDialogueTagDefinition CumOutside;

	public StoryBotDialogueTagDefinition CumTogether;

	public StoryBotDialogueTagDefinition BotCumming;

	public StoryBotDialogueTagDefinition EdgingOn;

	public StoryBotDialogueTagDefinition EdgingOff;

	public StoryBotDialogueTagDefinition Moan;

	[StorySingleton.PersonalitySourceIgnore]
	public StoryBotDialogueTagDefinition AnonRapeBegStop;

	public StoryBotDialogueTagDefinition RapeBegStopSuccess;

	public StoryBotDialogueTagDefinition RapeBegStopRefuse;

	public StoryBotDialogueTagDefinition RapeFlacidReaction;

	public StoryBotDialogueTagDefinition StartFrick;

	public StoryBotDialogueTagDefinition GenericFuckBrainStateRandomDialogueCockOutside;

	public StoryBotDialogueTagDefinition GenericFuckBrainStateRandomDialogueCockInside;

	public StoryBotDialogueTagDefinition GenericFuckBrainStateRandomDialogueCockInsideAnal;

	public StoryBotDialogueTagDefinition Headpat;

	public StoryBotDialogueTagDefinition FingeringDuring;

	public StoryBotDialogueTagDefinition FingeringBotCumming;

	public StoryBotDialogueTagDefinition BoobFondle;

	public StoryBotDialogueTagDefinition NipplePull;

	public StoryBotDialogueTagDefinition PussySpread;

	public StoryBotDialogueTagDefinition ClitRub;

	public StoryBotDialogueTagDefinition Handholding;

	public StoryBotDialogueTagDefinition UpsetOnlySheMoves;

	public StoryBotDialogueTagDefinition CumThighjob;

	public StoryBotDialogueTagDefinition FlacidBoredReaction;

	public StoryBotDialogueTagDefinition AnusSpread;

	public StoryBotDialogueTagDefinition FingeringAnusDuring;

	public StoryBotDialogueTagDefinition FingeringAnusBotCumming;

	public override string GetPrefix()
	{
		return "Frick_";
	}

	protected override void InitInner(StorySingleton storySingleton, StoryBotDialogueStage stage1, StoryBotDialogueStage stage2, StoryBotDialogueStage stage3)
	{
		Stage3VanillaDialogueIds id = Stage3VanillaDialogueIds.FrickDialogues;
		FrickConversations frickConversations = SingletonScriptableObject<CFLocalizationDataSO>.Instance.FrickConversations;
		Stage3VanillaDialogueIds id2 = Stage3VanillaDialogueIds.FrickHeadpatDialogues;
		CumInside.FromConversation(stage3, id.GetGameId(0), frickConversations.CumInside);
		CumOutside.FromTupleReference(stage3, id.GetGameId(1), LOC.Other.TupleReferences.Frick_CumOutside);
		CumTogether.FromTupleReference(stage3, id.GetGameId(2), LOC.Other.TupleReferences.Cowgirl_CumTogether);
		BotCumming.FromTupleReference(stage3, id.GetGameId(3), LOC.Other.TupleReferences.Frick_BotCum);
		EdgingOn.FromTupleReference(stage3, id.GetGameId(4), LOC.Other.TupleReferences.Blowjob_EdgingOn);
		EdgingOff.FromTupleReference(stage3, id.GetGameId(5), LOC.Other.TupleReferences.Blowjob_EdgingOff);
		Moan.FromConversation(stage3, id.GetGameId(6), frickConversations.Moan);
		AnonRapeBegStop.FromConversation(stage3, id.GetGameId(7), frickConversations.AnonRapeBegStop);
		RapeBegStopSuccess.FromConversation(stage3, id.GetGameId(8), frickConversations.RapeBegStopSuccess);
		RapeBegStopRefuse.FromConversation(stage3, id.GetGameId(9), frickConversations.RapeBegStopRefuse);
		RapeFlacidReaction.FromConversation(stage3, id.GetGameId(10), frickConversations.RapeFlacidReaction);
		CumInsideAnal.FromConversation(stage3, id.GetGameId(11), frickConversations.CumInsideAnal);
		DataCondition<float> horny = StoryCondition.CommonConditions.Horny;
		DataCondition<float> veryHorny = StoryCondition.CommonConditions.VeryHorny;
		StartFrick.CreateContainer(stage3, out var o);
		o.SetConditions(StoryCondition.CommonConditions.SympathyBeforeLikeOnly);
		o.AddBranch(new StoryBotDialogueBranch(id.GetGameId(100), frickConversations.StartFrickNormal));
		o.AddBranch(new StoryBotDialogueBranch(id.GetGameId(101), frickConversations.StartFrickHorny).AddConditions(horny).SetPriority(1));
		o.AddBranch(new StoryBotDialogueBranch(id.GetGameId(102), frickConversations.StartFrickVeryHorny).AddConditions(veryHorny).SetPriority(1));
		StartFrick.CreateContainer(stage3, out var o2);
		o2.SetConditions(StoryCondition.CommonConditions.SympathyLikeOnly);
		o2.AddBranch(new StoryBotDialogueBranch(id.GetGameId(110), frickConversations.StartFrickNormalLike));
		o2.AddBranch(new StoryBotDialogueBranch(id.GetGameId(111), frickConversations.StartFrickHornyLike).AddConditions(horny).SetPriority(1));
		o2.AddBranch(new StoryBotDialogueBranch(id.GetGameId(112), frickConversations.StartFrickVeryHornyLike).AddConditions(veryHorny).SetPriority(1));
		StartFrick.CreateContainer(stage3, out var o3);
		o3.SetConditions(StoryCondition.CommonConditions.SympathyAffectionOnly);
		o3.AddBranch(new StoryBotDialogueBranch(id.GetGameId(120), frickConversations.StartFrickNormalAffection));
		o3.AddBranch(new StoryBotDialogueBranch(id.GetGameId(121), frickConversations.StartFrickHornyAffection).AddConditions(horny).SetPriority(1));
		o3.AddBranch(new StoryBotDialogueBranch(id.GetGameId(122), frickConversations.StartFrickVeryHornyAffection).AddConditions(veryHorny).SetPriority(1));
		StartFrick.CreateContainer(stage3, out var o4);
		o4.SetConditions(StoryCondition.CommonConditions.SympathyLove);
		o4.AddBranch(new StoryBotDialogueBranch(id.GetGameId(130), frickConversations.StartFrickNormalLove));
		o4.AddBranch(new StoryBotDialogueBranch(id.GetGameId(131), frickConversations.StartFrickHornyLove).AddConditions(horny).SetPriority(1));
		o4.AddBranch(new StoryBotDialogueBranch(id.GetGameId(132), frickConversations.StartFrickVeryHornyLove).AddConditions(veryHorny).SetPriority(1));
		InteractionMemoryCondition interactionMemoryCondition = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.CockInside, 30f);
		InteractionMemoryCondition interactionMemoryCondition2 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.CockOutside, 30f);
		InteractionMemoryCondition interactionMemoryCondition3 = StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.CockOutside, 60f);
		GenericFuckBrainStateRandomDialogueCockInsideAnal.CreateContainer(stage3, out var o5);
		o5.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o5.AddBranch(new StoryBotDialogueBranch(id.GetGameId(200), frickConversations.CockInsideAnal30Affection).AddConditions(interactionMemoryCondition).SetOdds(5f));
		o5.AddBranch(new StoryBotDialogueBranch(id.GetGameId(201), frickConversations.CockInsideAnal30HornyAffection).AddConditions(interactionMemoryCondition, horny).SetOdds(10f));
		GenericFuckBrainStateRandomDialogueCockInsideAnal.CreateContainer(stage3, out var o6);
		o6.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o6.AddBranch(new StoryBotDialogueBranch(id.GetGameId(210), frickConversations.CockInsideAnal30HornyLike).AddConditions(interactionMemoryCondition, horny));
		GenericFuckBrainStateRandomDialogueCockInside.CreateContainer(stage3, out var o7);
		o7.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o7.AddBranch(new StoryBotDialogueBranch(id.GetGameId(220), frickConversations.CockInside30Affection).AddConditions(interactionMemoryCondition).SetOdds(5f));
		o7.AddBranch(new StoryBotDialogueBranch(id.GetGameId(221), frickConversations.CockInside30HornyAffection).AddConditions(interactionMemoryCondition, horny).SetOdds(10f));
		GenericFuckBrainStateRandomDialogueCockInside.CreateContainer(stage3, out var o8);
		o8.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o8.AddBranch(new StoryBotDialogueBranch(id.GetGameId(231), frickConversations.CockInside30HornyLike).AddConditions(interactionMemoryCondition, horny));
		GenericFuckBrainStateRandomDialogueCockOutside.CreateContainer(stage3, out var o9);
		o9.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o9.AddBranch(new StoryBotDialogueBranch(id.GetGameId(240), frickConversations.CockOutside30Affection).AddConditions(interactionMemoryCondition2).SetOdds(5f));
		o9.AddBranch(new StoryBotDialogueBranch(id.GetGameId(241), frickConversations.CockOutside60Affection).AddConditions(interactionMemoryCondition3).SetOdds(10f));
		o9.AddBranch(new StoryBotDialogueBranch(id.GetGameId(242), frickConversations.CockOutsideHorny30Affection).AddConditions(interactionMemoryCondition2, horny).SetOdds(100f));
		GenericFuckBrainStateRandomDialogueCockOutside.CreateContainer(stage3, out var o10);
		o10.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o10.AddBranch(new StoryBotDialogueBranch(id.GetGameId(250), frickConversations.CockOutside30Like).AddConditions(interactionMemoryCondition2));
		FingeringBotCumming.CreateContainer(stage3, out var o11);
		o11.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o11.AddBranch(new StoryBotDialogueBranch(id.GetGameId(260), frickConversations.FingeringBotCummingLike));
		FingeringDuring.CreateContainer(stage3, out var o12);
		o12.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o12.AddBranch(new StoryBotDialogueBranch(id.GetGameId(265), frickConversations.FingeringDuringLike));
		o12.AddBranch(new StoryBotDialogueBranch(id.GetGameId(266), frickConversations.FingeringDuringLikeHorny).SetOdds(5f));
		FingeringDuring.CreateContainer(stage3, out var o13);
		o13.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o13.AddBranch(new StoryBotDialogueBranch(id.GetGameId(267), frickConversations.FingeringDuringAffection).SetOdds(10f));
		o13.AddBranch(new StoryBotDialogueBranch(id.GetGameId(268), frickConversations.FingeringDuringAffectionHorny).SetOdds(20f).AddConditions(horny));
		o13.AddBranch(new StoryBotDialogueBranch(id.GetGameId(269), frickConversations.FingeringDuringAffectionLong).SetOdds(30f).AddConditions(StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.Fingering, 15f)));
		BoobFondle.CreateContainer(stage3, out var o14);
		o14.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o14.AddBranch(new StoryBotDialogueBranch(id.GetGameId(270), frickConversations.BoobFondleLike));
		o14.AddBranch(new StoryBotDialogueBranch(id.GetGameId(271), frickConversations.BoobFondleLikeHorny).AddConditions(horny).SetOdds(10f));
		BoobFondle.CreateContainer(stage3, out var o15);
		o15.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o15.AddBranch(new StoryBotDialogueBranch(id.GetGameId(275), frickConversations.BoobFondleAffection).SetOdds(5f));
		o15.AddBranch(new StoryBotDialogueBranch(id.GetGameId(276), frickConversations.BoobFondleAffectionHorny).AddConditions(horny).SetOdds(10f));
		NipplePull.CreateContainer(stage3, out var o16);
		o16.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o16.AddBranch(new StoryBotDialogueBranch(id.GetGameId(280), frickConversations.NipplePullLike));
		o16.AddBranch(new StoryBotDialogueBranch(id.GetGameId(281), frickConversations.NipplePullLikeHorny).AddConditions(horny).SetOdds(10f));
		NipplePull.CreateContainer(stage3, out var o17);
		o17.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o17.AddBranch(new StoryBotDialogueBranch(id.GetGameId(285), frickConversations.NipplePullAffection).SetOdds(5f));
		o17.AddBranch(new StoryBotDialogueBranch(id.GetGameId(286), frickConversations.NipplePullAffectionHorny).AddConditions(horny).SetOdds(10f));
		PussySpread.CreateContainer(stage3, out var o18);
		o18.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o18.AddBranch(new StoryBotDialogueBranch(id.GetGameId(290), frickConversations.PussySpreadLike));
		o18.AddBranch(new StoryBotDialogueBranch(id.GetGameId(291), frickConversations.PussySpreadLikeLong).AddConditions(StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.PussySpread, 10f)).SetOdds(5f));
		PussySpread.CreateContainer(stage3, out var o19);
		o19.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o19.AddBranch(new StoryBotDialogueBranch(id.GetGameId(295), frickConversations.PussySpreadAffection).SetOdds(20f));
		o19.AddBranch(new StoryBotDialogueBranch(id.GetGameId(296), frickConversations.PussySpreadAffectionLong).AddConditions(StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.PussySpread, 10f)).AddConditions(horny).SetOdds(40f));
		ClitRub.CreateContainer(stage3, out var o20);
		o20.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o20.AddBranch(new StoryBotDialogueBranch(id.GetGameId(300), frickConversations.ClitRubLike));
		ClitRub.CreateContainer(stage3, out var o21);
		o21.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o21.AddBranch(new StoryBotDialogueBranch(id.GetGameId(305), frickConversations.ClitRubAffection).SetOdds(5f));
		o21.AddBranch(new StoryBotDialogueBranch(id.GetGameId(306), frickConversations.ClitRubAffectionHorny).AddConditions(horny).SetOdds(10f));
		Handholding.CreateContainer(stage3, out var o22);
		o22.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o22.AddBranch(new StoryBotDialogueBranch(id.GetGameId(310), frickConversations.HandholdingLike));
		Handholding.CreateContainer(stage3, out var o23);
		o23.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o23.AddBranch(new StoryBotDialogueBranch(id.GetGameId(314), frickConversations.HandholdingAffection).SetOdds(4f));
		o22.AddBranch(new StoryBotDialogueBranch(id.GetGameId(315), frickConversations.HandholdingAffectionCockInside).AddConditions(StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.CockInside, 5f)).SetOdds(7f));
		Handholding.CreateContainer(stage3, out var o24);
		o24.SetConditions(StoryCondition.CommonConditions.SympathyLove);
		o24.AddBranch(new StoryBotDialogueBranch(id.GetGameId(317), frickConversations.HandholdingLove).SetOdds(4f));
		o22.AddBranch(new StoryBotDialogueBranch(id.GetGameId(319), frickConversations.HandholdingLoveCockInside).AddConditions(StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.CockInside, 5f)).SetOdds(7f));
		UpsetOnlySheMoves.CreateContainer(stage3, out var o25);
		o25.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o25.AddBranch(new StoryBotDialogueBranch(id.GetGameId(320), frickConversations.UpsetOnlySheMoves));
		o25.AddBranch(new StoryBotDialogueBranch(id.GetGameId(321), frickConversations.UpsetOnlySheMovesHorny).AddConditions(StoryCondition.CommonConditions.Horny));
		FlacidBoredReaction.CreateContainer(stage3, out var o26);
		o26.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o26.AddBranch(new StoryBotDialogueBranch(id.GetGameId(325), frickConversations.FlacidBoredReaction));
		CumThighjob.CreateContainer(stage3, out var o27);
		o27.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o27.AddBranch(new StoryBotDialogueBranch(id.GetGameId(330), frickConversations.CumThighjob));
		AnusSpread.CreateContainer(stage3, out var o28);
		o28.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o28.AddBranch(new StoryBotDialogueBranch(id.GetGameId(340), frickConversations.AnusSpreadLike));
		o28.AddBranch(new StoryBotDialogueBranch(id.GetGameId(341), frickConversations.AnusSpreadLikeLong).AddConditions(StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.AnusSpread, 10f)).SetOdds(5f));
		AnusSpread.CreateContainer(stage3, out var o29);
		o29.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o29.AddBranch(new StoryBotDialogueBranch(id.GetGameId(346), frickConversations.AnusSpreadAffection).SetOdds(20f));
		o29.AddBranch(new StoryBotDialogueBranch(id.GetGameId(357), frickConversations.AnusSpreadAffectionLong).AddConditions(StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.AnusSpread, 10f)).AddConditions(horny).SetOdds(40f));
		FingeringAnusBotCumming.CreateContainer(stage3, out var o30);
		o30.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o30.AddBranch(new StoryBotDialogueBranch(id.GetGameId(360), frickConversations.FingeringAnusBotCummingLike));
		FingeringAnusDuring.CreateContainer(stage3, out var o31);
		o31.SetConditions(StoryCondition.CommonConditions.SympathyLike);
		o31.AddBranch(new StoryBotDialogueBranch(id.GetGameId(365), frickConversations.FingeringAnusDuringLike));
		o31.AddBranch(new StoryBotDialogueBranch(id.GetGameId(366), frickConversations.FingeringAnusDuringLikeHorny).SetOdds(5f));
		FingeringAnusDuring.CreateContainer(stage3, out var o32);
		o32.SetConditions(StoryCondition.CommonConditions.SympathyAffection);
		o32.AddBranch(new StoryBotDialogueBranch(id.GetGameId(370), frickConversations.FingeringAnusDuringAffection).SetOdds(10f));
		o32.AddBranch(new StoryBotDialogueBranch(id.GetGameId(371), frickConversations.FingeringAnusDuringAffectionHorny).SetOdds(20f).AddConditions(horny));
		o32.AddBranch(new StoryBotDialogueBranch(id.GetGameId(372), frickConversations.FingeringAnusDuringAffectionLong).SetOdds(30f).AddConditions(StoryCondition.CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum.FingeringAnus, 15f)));
		Headpat.CreateContainer(stage3, out var o33);
		o33.AddMultipleBranchesWithSameCondition(StoryCondition.CommonConditions.SympathyPositive, new StoryBotDialogueBranch(id2.GetGameId(0), frickConversations.Headpat));
	}
}
