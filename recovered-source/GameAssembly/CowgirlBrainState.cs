using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Fungus;
using UnityEngine;

public class CowgirlBrainState : CommonLewdSceneBrainState<Live2DCowgirlController>, IHasNonJitClassDescription
{
	public enum CowgirlReactions
	{
		Nothing,
		Stop,
		GoSlow,
		GoFast,
		Insert,
		Pullout,
		BotLead,
		BegStop,
		SwitchToBlowjob
	}

	[Serializable]
	public class CowgirlConversations : LocalizedStringReferenceContainer
	{
		public SerializedLocalizedConversation BeforeInsertRape;

		public SerializedLocalizedConversation MadeMcCumInsideWhileCummingHerself;

		public SerializedLocalizedConversation BeforeSwitchToBlowjobBotLead;

		public SerializedLocalizedConversation BeforeSwitchToBlowjobBotRape;

		public SerializedLocalizedConversation AnonAfterFirstInsertRape;

		public SerializedLocalizedConversation AfterFirstInsertBotRape;

		public SerializedLocalizedConversation AnonAfterFirstInsertRapeConsent;

		public override string ContainerName => "Cowgirl";
	}

	public class CowgirlSceneDialogueArea : StoryBotDialogueAreaDefinition
	{
		public StoryBotDialogueTagDefinition RemovePants;

		public StoryBotDialogueTagDefinition BeforeFirstInsertBotLead;

		public StoryBotDialogueTagDefinition BeforeFirstInsert;

		public StoryBotDialogueTagDefinition EdgingForceStopped;

		public StoryBotDialogueTagDefinition EdgingBreak;

		public StoryBotDialogueTagDefinition FlacidReaction;

		public StoryBotDialogueTagDefinition WhereToCumQuestion;

		public StoryBotDialogueTagDefinition WhereToCumQuestionDidntAnswer;

		public StoryBotDialogueTagDefinition BeforeFirstInsertRape;

		public StoryBotDialogueTagDefinition MadeMcCumInsideWhileCummingHerself;

		public StoryBotDialogueTagDefinition BeforeSwitchToBlowjobBotLead;

		public StoryBotDialogueTagDefinition BeforeSwitchToBlowjobRape;

		public StoryBotDialogueTagDefinition AfterFirstInsertBotRape;

		[StorySingleton.PersonalitySourceIgnore]
		public StoryBotDialogueTagDefinition AnonAfterFirstInsertRape;

		[StorySingleton.PersonalitySourceIgnore]
		public StoryBotDialogueTagDefinition AnonAfterFirstInsertRapeConsent;

		public override string GetPrefix()
		{
			return "Cowgirl_";
		}

		protected override void InitInner(StorySingleton storySingleton, StoryBotDialogueStage stage1, StoryBotDialogueStage stage2, StoryBotDialogueStage stage3)
		{
			CowgirlConversations cowgirlConversations = SingletonScriptableObject<CFLocalizationDataSO>.Instance.CowgirlConversations;
			RemovePants.FromTupleReference(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(0), LOC.Other.TupleReferences.Cowgirl_RemovePants);
			BeforeFirstInsertBotLead.FromTupleReference(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(1), LOC.Other.TupleReferences.Cowgirl_BeforeInsertBotLead);
			BeforeFirstInsert.FromTupleReference(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(2), LOC.Other.TupleReferences.Cowgirl_BeforeInsert);
			EdgingForceStopped.FromTupleReference(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(3), LOC.Other.TupleReferences.Cowgirl_EdgeForceStopped);
			EdgingBreak.FromTupleReference(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(4), LOC.Other.TupleReferences.Cowgirl_EdgingBreak);
			FlacidReaction.FromTupleReference(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(5), LOC.Other.TupleReferences.Cowgirl_FlacidReaction);
			WhereToCumQuestion.FromTupleReference(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(6), LOC.Other.TupleReferences.Cowgirl_CumQuestion);
			WhereToCumQuestionDidntAnswer.FromTupleReference(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(7), LOC.Other.TupleReferences.Cowgirl_DidntDecide);
			BeforeFirstInsertRape.FromConversation(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(8), cowgirlConversations.BeforeInsertRape);
			MadeMcCumInsideWhileCummingHerself.FromConversation(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(9), cowgirlConversations.MadeMcCumInsideWhileCummingHerself);
			BeforeSwitchToBlowjobBotLead.FromConversation(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(11), cowgirlConversations.BeforeSwitchToBlowjobBotLead);
			BeforeSwitchToBlowjobRape.FromConversation(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(12), cowgirlConversations.BeforeSwitchToBlowjobBotRape);
			AfterFirstInsertBotRape.FromConversation(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(14), cowgirlConversations.AfterFirstInsertBotRape);
			AnonAfterFirstInsertRape.FromConversation(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(15), cowgirlConversations.AnonAfterFirstInsertRape);
			AnonAfterFirstInsertRapeConsent.FromConversation(stage3, Stage3VanillaDialogueIds.CowgirlDialogues.GetGameId(16), cowgirlConversations.AnonAfterFirstInsertRapeConsent);
		}
	}

	private class CowgirlBrainStateButtonSource : ButtonListButtonSource
	{
		private CowgirlBrainState _cowgirlBrainState;

		private ButtonListWrapper _requestButtonWrapper;

		public CowgirlBrainStateButtonSource(CowgirlBrainState cowgirlBrainState)
		{
			_cowgirlBrainState = cowgirlBrainState;
		}

		public override void AddButtonsToList(IModificationPeriodButtonList buttonList)
		{
			_requestButtonWrapper = buttonList.AddButton(-5);
			_requestButtonWrapper.TextFunc = () => LOC.UI.Frick_Request;
			_requestButtonWrapper.OnClick = RequestButtonClicked;
		}

		private void RequestButtonClicked()
		{
			List<MenuOption> list = new List<MenuOption>();
			Live2DCowgirlController controller = _cowgirlBrainState.controller;
			Live2DControllerCommonStateFrickData brainAdditionalData = _cowgirlBrainState.FrickData;
			CowgirlReactions currentRequest = _cowgirlBrainState.CurrentReaction;
			if (!controller.CockManager.IsCockCummingState && _cowgirlBrainState.IsFree)
			{
				List<(CowgirlReactions, long)> list2 = new List<(CowgirlReactions, long)>();
				switch (brainAdditionalData.D_BotLeadStatus)
				{
				default:
					list2.Add((CowgirlReactions.BotLead, 117175207793135616L));
					if (controller.CockManager.IsCockLimbInside())
					{
						list2.Add((CowgirlReactions.GoFast, 98791638976827392L));
						list2.Add((CowgirlReactions.GoSlow, 98791757939871744L));
						list2.Add((CowgirlReactions.Stop, 98791785311899648L));
						list2.Add((CowgirlReactions.Pullout, 99146374619992064L));
					}
					else
					{
						list2.Add((CowgirlReactions.Insert, 99146376071221248L));
					}
					break;
				case BotFrickControlStatusEnum.BotLead:
					list2.Add((CowgirlReactions.Stop, 98791785311899648L));
					break;
				case BotFrickControlStatusEnum.BotFullLead:
					list2.Add((CowgirlReactions.BegStop, 203832649683177472L));
					break;
				}
				list.AddRange(list2.Where(((CowgirlReactions request, long localizationId) t) => t.request != currentRequest).Select(((CowgirlReactions request, long localizationId) t) => new MenuOption
				{
					beforeNextBlock = () =>
					{
						_cowgirlBrainState.React(t.request);
						UiOverlay.Instance.CloseFungusOverlay();
					},
					text = t.localizationId.GetUI()
				}));
				List<(OnCumChoiceEnum, long)> list3 = new List<(OnCumChoiceEnum, long)>();
				list3.Add((OnCumChoiceEnum.Automatic, 41349591252996096L));
				list3.Add((OnCumChoiceEnum.Inside, 41349597364097024L));
				list3.Add((OnCumChoiceEnum.Outside, 41349599742267392L));
				list3.RemoveAll(((OnCumChoiceEnum request, long localizedId) t) => t.request == brainAdditionalData.D_CurrentOnCum);
				list.AddRange(list3.Select(((OnCumChoiceEnum request, long localizedId) t) => new MenuOption
				{
					beforeNextBlock = () =>
					{
						brainAdditionalData.D_CurrentOnCum = t.request;
						UiOverlay.Instance.CloseFungusOverlay();
					},
					text = t.localizedId.GetUI()
				}));
			}
			if (!brainAdditionalData.IsBotCompletelyInTheLead)
			{
				list.Add(new MenuOption
				{
					text = (brainAdditionalData.D_Edge ? LOC.UI.Frick_Blowjob_LetMeCum : LOC.UI.Frick_Blowjob_DontLetMeCum),
					beforeNextBlock = () =>
					{
						UiOverlay.Instance.CloseFungusOverlay();
						brainAdditionalData.D_Edge = !brainAdditionalData.D_Edge;
						_cowgirlBrainState.CommentEdging();
					}
				});
			}
			list.Add(new MenuOption
			{
				text = LOC.UI.Frick_Blowjob_Nevermind,
				beforeNextBlock = UiOverlay.Instance.CloseFungusOverlay
			});
			UiOverlay.Instance.OpenFungusOverlay();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartMenu(list);
		}
	}

	private float targetFuckSpeed = 1f;

	private CowgirlBrainStateButtonSource _buttonSource;

	public bool FlacidityActivated { get; set; }

	public CowgirlReactions CurrentReaction { get; set; }

	private CowgirlSceneDialogueArea CowgirlDialogue => SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<CowgirlSceneDialogueArea>();

	public bool IsFree
	{
		get
		{
			if (!IsReactingUnstoppable)
			{
				return !FlacidityActivated;
			}
			return false;
		}
	}

	private float TargetHeadShakeValue => 0.4f + (float)FrickData.D_TimesBotCame * 0.2f;

	private void StopStoppableReactions()
	{
		if (IsReacting && (CurrentReaction == CowgirlReactions.GoFast || CurrentReaction == CowgirlReactions.GoSlow))
		{
			StopReaction();
		}
	}

	private void MakeSureFuckReaction()
	{
		if ((CurrentReaction != CowgirlReactions.GoSlow && CurrentReaction != CowgirlReactions.GoFast) || !IsReacting)
		{
			DoReaction(Fuck());
		}
	}

	public override void DoStopBotLeadReaction()
	{
		React(CowgirlReactions.Stop);
	}

	public void React(CowgirlReactions reactions)
	{
		if (IsReactingUnstoppable)
		{
			return;
		}
		switch (reactions)
		{
		default:
			return;
		case CowgirlReactions.Nothing:
			StopStoppableReactions();
			break;
		case CowgirlReactions.Stop:
			StopStoppableReactions();
			SetBrainFrickControlStatus(BotFrickControlStatusEnum.None);
			break;
		case CowgirlReactions.BegStop:
			StopStoppableReactions();
			DoReactionUnstoppable(BegStop());
			break;
		case CowgirlReactions.GoSlow:
			targetFuckSpeed = 1f;
			MakeSureFuckReaction();
			break;
		case CowgirlReactions.GoFast:
			targetFuckSpeed = (FrickData.IsBotCompletelyInTheLead ? 8f : 5f);
			MakeSureFuckReaction();
			break;
		case CowgirlReactions.Insert:
			DoReactionUnstoppable(Insert(), (ManagedCoroutine.FinishedType x) =>
			{
				controller.VaginaCockInsertionFuckAxis.DisableDickAngle = false;
			});
			break;
		case CowgirlReactions.Pullout:
			DoReactionUnstoppable(PullOut(), (ManagedCoroutine.FinishedType x) =>
			{
				controller.VaginaCockInsertionFuckAxis.DisableDickAngle = false;
			});
			break;
		case CowgirlReactions.BotLead:
			StopStoppableReactions();
			SetBrainFrickControlStatus(BotFrickControlStatusEnum.BotLead);
			break;
		}
		CurrentReaction = reactions;
	}

	private IEnumerator GoToTarget(float target, float movementSpeedMulti = 1f)
	{
		float t = 0f;
		float movementVariationMulti = 1f;
		float startValue = controller.ParamBotBodyMovementY.Value;
		float num = Mathf.Abs(target - startValue);
		if (Mathf.Approximately(0f, num))
		{
			controller.ParamBotBodyMovementY.Value = target;
			yield break;
		}
		movementSpeedMulti /= num;
		while (true)
		{
			float tDelta = Time.deltaTime * movementVariationMulti * movementSpeedMulti;
			if (FrickData.D_Edge && controller.CockManager.TargetCumBarValue > 0.8f && controller.CockManager.IsCockLimbInside())
			{
				FrickData.D_HadToSlowDown = true;
				tDelta *= 1f - controller.CockManager.TargetCumBarValue;
			}
			movementVariationMulti = (movementVariationMulti + UnityEngine.Random.Range(0.8f, 1.2f)) / 2f;
			if (FrickData.D_Edge && controller.CockManager.TargetCumBarValue > 0.95f && controller.CockManager.IsCockLimbInside())
			{
				FrickData.D_HadToStop = true;
				yield return WaitForSecondCache.Seconds1;
			}
			t += tDelta;
			if (!(t < 1f))
			{
				break;
			}
			controller.ParamBotBodyMovementY.Value = EasingFunction.EaseInQuad(startValue, target, t);
			yield return null;
		}
		controller.ParamBotBodyMovementY.Value = target;
	}

	private IEnumerator Insert()
	{
		controller.CockManager.WhipOutCock();
		if (controller.IsPantsOn && !controller.IsPantsAside)
		{
			if (!FrickData.IsBotCompletelyInTheLead)
			{
				CowgirlDialogue.RemovePants.StartDialogueFromModelBrainState(this);
			}
			controller.ParamPantsToSide.SetPercent(1f);
			R.Sound.Room_Cloth.Get().Play();
			yield return WaitForSecondCache.Seconds1;
		}
		if (controller.IsPantiesOn && !controller.IsPantiesAside)
		{
			controller.ParamToSide.SetPercent(1f);
			yield return WaitForSecondCache.Seconds1;
		}
		if (!FrickData.D_AlreadyInsertedOnce && !UiOverlay.Instance.MainCamera.GetComponent<EyeClosingShader>().enabled)
		{
			switch (FrickData.D_BotLeadStatus)
			{
			default:
				CowgirlDialogue.BeforeFirstInsert.StartDialogueFromModelBrainState(this);
				break;
			case BotFrickControlStatusEnum.BotLead:
				CowgirlDialogue.BeforeFirstInsertBotLead.StartDialogueFromModelBrainState(this);
				break;
			case BotFrickControlStatusEnum.BotFullLead:
				CowgirlDialogue.BeforeFirstInsertRape.StartDialogueFromModelBrainState(this);
				break;
			}
		}
		controller.ParamToSide.SetPercent(1f);
		controller.VaginaCockInsertionFuckAxis.DisableDickAngle = true;
		yield return GoToTarget(1f, 5f);
		controller.ParamCockAngleY.Value = -1f;
		yield return Live2DTween(controller.ParamMoveMc, -1f, 0.5f);
		controller.ParamToSide.SetPercent(1f);
		yield return Live2DTweenRelative(controller.ParamCockAngleY, -1f, 0.5f);
		controller.ParamToSide.SetPercent(1f);
		controller.VaginaCockInsertionFuckAxis.PutInside();
		controller.ParamCockAngleY.Value = 0f;
		yield return Live2DTween(controller.ParamMoveMc, 0f, 1f);
		yield return GoToTarget(0f);
		if (!FrickData.D_AlreadyInsertedOnce && FrickData.IsBotCompletelyInTheLead && !FlacidityActivated)
		{
			yield return CowgirlDialogue.AnonAfterFirstInsertRape.StartDialogueFromModelBrainStateAndWait(this);
			yield return CowgirlDialogue.AfterFirstInsertBotRape.StartDialogueFromModelBrainStateAndWait(this);
			yield return CowgirlDialogue.AnonAfterFirstInsertRapeConsent.StartDialogueFromModelBrainStateAndWait(this);
		}
		FrickData.D_AlreadyInsertedOnce = true;
		CurrentReaction = CowgirlReactions.Stop;
	}

	private IEnumerator PullOut(float speedMulti = 1f)
	{
		controller.FireEvent(InteractionMemoryEventEnum.Pullout);
		float speedMultiInv = 1f / speedMulti;
		controller.VaginaCockInsertionFuckAxis.DisableDickAngle = true;
		yield return GoToTarget(1f, 5f * speedMulti);
		controller.ParamCockAngleY.Value = -1f;
		yield return Live2DTween(controller.ParamMoveMc, -1f, 0.5f * speedMultiInv);
		yield return Live2DTweenRelative(controller.ParamCockAngleY, -1f, 0.5f * speedMultiInv);
		controller.VaginaCockInsertionFuckAxis.IsInsideCowgirl = false;
		controller.ParamCockAngleY.Value = 0f;
		yield return Live2DTween(controller.ParamMoveMc, 0f, 1f * speedMultiInv);
		CurrentReaction = CowgirlReactions.Stop;
	}

	private IEnumerator BotCumStop()
	{
		using (Live2DExpression.CummingExpressionState cm = controller.Expression.StartCumming())
		{
			controller.PhysicsSubRig_HeadShake.DisablePhysics = true;
			controller.Eyes.DisableBlinking = true;
			yield return GoToTarget(-1f, 5f);
			bool madeMcCum = false;
			for (int i = 0; i < cm.NumberOfTwitches; i++)
			{
				yield return GoToTarget(-1f, 5f);
				yield return new WaitForSeconds(cm.TwitchLength / 2f);
				controller.ParamHeadY.AddPercent(UnityEngine.Random.Range(-0.05f, 0.2f));
				controller.ParamBotBodyMovementY.AddPercent(UnityEngine.Random.Range(0.05f, 0.2f));
				controller.ParamLegsQuiver.SetPercent(cm.AdjustedIntensity);
				yield return WaitForSecondCache.RandomUnderThirdSecond();
				controller.ParamBotBodyMovementY.AddPercent(-0.25f);
				controller.CockManager.AddCumBarValue(FrickData.IsBotCompletelyInTheLead ? 0.03f : 0.015f);
				controller.Eyes.HalfCloseEyes(UnityEngine.Random.Range(cm.AdjustedIntensity / 4f, cm.AdjustedIntensity / 2f));
				yield return new WaitForSeconds(cm.TwitchLength / 2f);
				controller.ParamLegsQuiver.ResetToDefault();
				if (i % 8 == 0 && Owner.PersonalityData.MoanOdds.Check())
				{
					MoanIfPossible();
				}
				if (!madeMcCum && controller.TryGetLimbUsageAt(LimbUsageChoiceEnum.Cock, out ICockLimbUsage limbUsage) && limbUsage.IfCumImminentSpeedItUp())
				{
					madeMcCum = true;
					CowgirlDialogue.MadeMcCumInsideWhileCummingHerself.StartDialogueFromModelBrainState(this);
				}
			}
			yield return cm.CumFinishTimeOut();
			controller.ParamHeadY.Value = 0f;
			yield return new WaitForSeconds(0.04f);
		}
		controller.PhysicsSubRig_HeadShake.Scale = 0f;
		controller.PhysicsSubRig_HeadShake.DisablePhysics = false;
		controller.Eyes.DisableBlinking = false;
		yield return CoroutineUtilities.GenericTween((float t) =>
		{
			controller.PhysicsSubRig_HeadShake.Scale = t;
		}, 0f, TargetHeadShakeValue, TargetHeadShakeValue / 5f);
		UpdateHeadShakeValue();
		CurrentReaction = CowgirlReactions.Nothing;
	}

	private IEnumerator BotCumKeepGoing()
	{
		using (Live2DExpression.CummingExpressionState cm = controller.Expression.StartCumming())
		{
			targetFuckSpeed = 8f;
			if (Owner.PersonalityData.MoanOdds.Check(0f, 0.5f))
			{
				MoanIfPossible();
			}
			int count = Mathf.Min(6, Mathf.CeilToInt(cm.Length / 0.25f));
			bool madeMcCum = false;
			for (int i = 0; i < count; i++)
			{
				madeMcCum |= controller.TryGetLimbUsageAt(LimbUsageChoiceEnum.Cock, out ICockLimbUsage limbUsage) && limbUsage.IfCumImminentSpeedItUp();
				controller.FireEvent(InteractionMemoryEventEnum.FuckingFast);
				controller.ParamLegsQuiver.SetPercent(cm.AdjustedIntensity);
				yield return GoToTarget(1f, targetFuckSpeed);
				yield return WaitForSecondCache.Seconds0_05;
				controller.CockManager.AddCumBarValue(FrickData.IsBotCompletelyInTheLead ? 0.02f : 0.01f);
				yield return GoToTarget(-1f, targetFuckSpeed);
				yield return WaitForSecondCache.Seconds0_2;
				controller.ParamLegsQuiver.ResetToDefault();
				if (!madeMcCum && controller.TryGetLimbUsageAt(LimbUsageChoiceEnum.Cock, out ICockLimbUsage limbUsage2) && limbUsage2.IfCumImminentSpeedItUp())
				{
					madeMcCum = true;
					CowgirlDialogue.MadeMcCumInsideWhileCummingHerself.StartDialogueFromModelBrainState(this);
				}
			}
			if (Owner.PersonalityData.MoanOdds.Check(0f, 0.5f))
			{
				MoanIfPossible();
			}
			yield return GoToTarget(-1f, 5f);
			controller.ParamLegsQuiver.SetPercent(cm.AdjustedIntensity);
			yield return cm.CumFinishTimeOut();
		}
		controller.PhysicsSubRig_HeadShake.DisablePhysics = false;
		controller.Eyes.DisableBlinking = false;
		controller.ParamLegsQuiver.ResetToDefault();
		CurrentReaction = CowgirlReactions.Nothing;
	}

	private IEnumerator BotCumFingering()
	{
		using (Live2DExpression.CummingExpressionState cm = controller.Expression.StartCumming())
		{
			targetFuckSpeed = 8f;
			CommonDialogue.FingeringBotCumming.StartDialogueFromModelBrainState(this);
			int count = Mathf.Min(6, Mathf.CeilToInt(cm.Length / 0.25f));
			for (int i = 0; i < count; i++)
			{
				controller.ParamLegsQuiver.SetPercent(cm.AdjustedIntensity);
				yield return GoToTarget(1f, targetFuckSpeed);
				yield return WaitForSecondCache.Seconds0_05;
				yield return GoToTarget(-1f, targetFuckSpeed);
				yield return WaitForSecondCache.Seconds0_2;
				controller.ParamLegsQuiver.ResetToDefault();
			}
			if (Owner.PersonalityData.MoanOdds.Check(0f, 0.5f))
			{
				MoanIfPossible();
			}
			yield return GoToTarget(-1f, 5f);
			controller.ParamLegsQuiver.SetPercent(cm.AdjustedIntensity);
			yield return cm.CumFinishTimeOut();
		}
		controller.PhysicsSubRig_HeadShake.DisablePhysics = false;
		controller.Eyes.DisableBlinking = false;
		controller.ParamLegsQuiver.ResetToDefault();
		CurrentReaction = CowgirlReactions.Nothing;
	}

	private IEnumerator Fuck()
	{
		while (true)
		{
			controller.FireEvent(InteractionMemoryEventEnum.FuckingFast);
			yield return GoToTarget(1f, targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_05;
			yield return GoToTarget(-1f, targetFuckSpeed);
			yield return WaitForSecondCache.Seconds0_2;
		}
	}

	private void TrySwitchToBlowjob()
	{
		if (Controller.CockManager.TryGetCockLimbUsage(out var o) && o.IsInNormalState)
		{
			UiOverlay.Instance.OpenFungusOverlay();
			SingletonMonoBehaviour<FungusEnumerators>.Instance.SwitchSexScene(SingletonMonoBehaviour<ViewSingleton>.Instance.BlowjobView).Forget();
		}
	}

	private IEnumerator BotLeadSwitchToBlowjobReaction()
	{
		if (GameVariables.Current.CanDoBlowjob || !controller.CockManager.IsFlacid)
		{
			CurrentReaction = CowgirlReactions.SwitchToBlowjob;
			yield return CowgirlDialogue.BeforeSwitchToBlowjobBotLead.StartDialogueFromModelBrainStateAndWait(this);
			TrySwitchToBlowjob();
			CurrentReaction = CowgirlReactions.Nothing;
		}
	}

	private IEnumerator BotCumInsideReaction()
	{
		yield return GoToTarget(-1f, 10f);
		yield return WaitAfterMCCame();
		if (FrickData.BotHasLead && Owner.PersonalityData.CowgirlLeadSwitchToBlowjobAfterCummingOdds.Check())
		{
			yield return BotLeadSwitchToBlowjobReaction();
		}
		CurrentReaction = CowgirlReactions.Nothing;
	}

	private IEnumerator BotLeadCumInside()
	{
		targetFuckSpeed = 8f;
		CoroutineUtilities.WaitForResultIEnumerator<bool> waitTillCum = WaitTillMCAboutToCum();
		yield return waitTillCum;
		if (waitTillCum.Result)
		{
			if (Owner.PersonalityData.ChanceToCommentCummingInside.Check())
			{
				CommonDialogue.CumInside.StartDialogueFromModelBrainState(this);
			}
			yield return DoReactionUnstoppable(BotCumInsideReaction());
		}
	}

	private IEnumerator BotCumOutsideReaction()
	{
		yield return PullOut(4f);
		yield return WaitForSecondCache.Seconds0_5;
		yield return WaitAfterMCCame();
		if (FrickData.BotHasLead && Owner.PersonalityData.CowgirlLeadSwitchToBlowjobAfterCummingOdds.Check())
		{
			yield return BotLeadSwitchToBlowjobReaction();
		}
		CurrentReaction = CowgirlReactions.Nothing;
	}

	private CoroutineUtilities.WaitForResultIEnumerator<bool> WaitTillMCAboutToCum()
	{
		return CoroutineUtilities.WaitUntilWithTimeoutResult(() => controller.CockManager.IsCockCummingState, 25f);
	}

	private CoroutineUtilities.WaitForResultIEnumerator<bool> WaitAfterMCCame()
	{
		return CoroutineUtilities.WaitUntilWithTimeoutResult(() => !controller.CockManager.IsCockCummingState, 25f);
	}

	private IEnumerator BotLeadCumOutside()
	{
		targetFuckSpeed = 8f;
		CoroutineUtilities.WaitForResultIEnumerator<bool> waitTillCum = WaitTillMCAboutToCum();
		yield return waitTillCum;
		if (waitTillCum.Result)
		{
			CommonDialogue.CumOutside.StartDialogueFromModelBrainState(this);
			yield return WaitForSecondCache.Seconds0_5;
			yield return DoReactionUnstoppable(BotCumOutsideReaction());
		}
	}

	private IEnumerator BotLeadCumTogether()
	{
		FrickData.D_Edge = false;
		FrickData.D_HadToStop = false;
		FrickData.D_HadToSlowDown = false;
		CommonDialogue.CumTogether.StartDialogueFromModelBrainState(this);
		targetFuckSpeed = 8f;
		CoroutineUtilities.WaitForResultIEnumerator<bool> waitTillCum = WaitTillMCAboutToCum();
		yield return waitTillCum;
		if (waitTillCum.Result)
		{
			StopReaction();
			if (Owner.PersonalityData.CowgirlStopMovingWhenCummingOdds.Check())
			{
				yield return DoReactionUnstoppable(BotCumStop());
			}
			else
			{
				yield return DoReactionUnstoppable(BotCumKeepGoing());
			}
		}
	}

	private IEnumerator BotLeadCumByHerself()
	{
		if (FrickData.D_Edge && (FrickData.D_HadToSlowDown || FrickData.D_HadToStop))
		{
			FrickData.D_Edge = false;
			FrickData.D_HadToStop = false;
			FrickData.D_HadToSlowDown = false;
			CowgirlDialogue.EdgingForceStopped.StartDialogueFromModelBrainState(this);
		}
		else
		{
			CommonDialogue.BotCumming.StartDialogueFromModelBrainState(this);
		}
		StopReaction();
		if (Owner.PersonalityData.CowgirlStopMovingWhenCummingOdds.Check() || FrickData.D_Edge)
		{
			yield return DoReactionUnstoppable(BotCumStop());
		}
		else
		{
			yield return DoReactionUnstoppable(BotCumKeepGoing());
		}
	}

	private IEnumerator BotLeadCumChoose()
	{
		if (FrickData.D_CurrentOnCum == OnCumChoiceEnum.Automatic)
		{
			if (UnityEngine.Random.value < 0.8f)
			{
				return BotLeadCumInside();
			}
			return BotLeadCumOutside();
		}
		if (FrickData.D_CurrentOnCum == OnCumChoiceEnum.Inside)
		{
			return BotLeadCumInside();
		}
		return BotLeadCumOutside();
	}

	private IEnumerator BotLeadGettingClose()
	{
		if (FrickData.D_Edge)
		{
			if (IsFree)
			{
				StopReaction();
				yield return GoToTarget(-1f);
				CowgirlDialogue.EdgingBreak.StartDialogueFromModelBrainState(this);
				yield return WaitForSecondCache.RandomCoupleSeconds();
			}
		}
		else if (Owner.CanCum)
		{
			yield return BotLeadCumTogether();
		}
		else if (Owner.PersonalityData.CowgirlWhereToCumQuestionOdds.Check())
		{
			yield return CowgirlDialogue.WhereToCumQuestion.StartDialogueFromModelBrainStateAndWait(this);
			int chosen = 0;
			yield return SingletonMonoBehaviour<FungusEnumerators>.Instance.DoMenu(new List<MenuOption>
			{
				new MenuOption(LOC.Other.Cowgirl_CumInside),
				new MenuOption(LOC.Other.Cowgirl_CumInMouth),
				new MenuOption(LOC.Other.Cowgirl_CumOutside),
				new MenuOption("...")
			}, (int i, MenuOption option) =>
			{
				chosen = i;
			}, (MenuDialog md) =>
			{
				md.ShowTimer(2f, 3);
			});
			switch (chosen)
			{
			case 0:
				yield return BotLeadCumInside();
				break;
			case 1:
				FrickData.D_CurrentOnCum = OnCumChoiceEnum.Inside;
				FrickData.D_ContinueAfterCumming = true;
				TrySwitchToBlowjob();
				break;
			case 2:
				yield return BotLeadCumOutside();
				break;
			case 3:
				yield return CowgirlDialogue.WhereToCumQuestionDidntAnswer.StartDialogueFromModelBrainStateAndWait(this);
				yield return BotLeadCumChoose();
				break;
			}
		}
		else
		{
			yield return BotLeadCumChoose();
		}
	}

	private IEnumerator GotFlacidReaction()
	{
		controller.CockManager.IsFlacid = true;
		yield return GoToTarget(1f);
		if (!FrickData.IsBotCompletelyInTheLead)
		{
			yield return CowgirlDialogue.FlacidReaction.StartDialogueFromModelBrainStateAndWait(this);
		}
		else
		{
			yield return CommonDialogue.RapeFlacidReaction.StartDialogueFromModelBrainStateAndWait(this);
		}
		SetBrainFrickControlStatus(BotFrickControlStatusEnum.None);
	}

	private IEnumerator BotLead()
	{
		yield return WaitUntilReactionNotRunning();
		while (FrickData.BotHasLead && !FlacidityActivated)
		{
			if (!controller.CockManager.IsCockLimbInside())
			{
				React(CowgirlReactions.Insert);
				yield return WaitUntilReactionNotRunning();
			}
			if (!controller.CockManager.IsCockLimbInside())
			{
				yield return null;
				continue;
			}
			if (FrickData.IsBotCompletelyInTheLead)
			{
				React(CowgirlReactions.GoFast);
			}
			else if (FrickData.D_Edge && controller.CockManager.CumBarValue > 0.5f)
			{
				React(CowgirlReactions.GoSlow);
			}
			else
			{
				React(CowgirlReactions.GoFast);
			}
			yield return WaitForSecondCache.Seconds0_1;
			if (controller.CockManager.CumBarValue > 0.8f)
			{
				yield return BotLeadGettingClose();
			}
			else if (Owner.CanCum)
			{
				yield return BotLeadCumByHerself();
			}
		}
	}

	public void UpdateHeadShakeValue()
	{
		controller.PhysicsSubRig_HeadShake.Scale = Mathf.Min(TargetHeadShakeValue, 1f);
	}

	public override void EnterState()
	{
		base.EnterState();
		UpdateHeadShakeValue();
		FlacidityActivated = controller.CockManager.IsFlacid;
		controller.SetCurrentToolSelectorActive(value: true);
		UiOverlay.Instance.GetStaticGUI<FrickStaticGui>(out var staticGui);
		_buttonSource = new CowgirlBrainStateButtonSource(this);
		staticGui.RightButtonList.AddButtonSource(_buttonSource);
	}

	public override void ExitState()
	{
		base.ExitState();
		_buttonSource.Remove();
		_buttonSource = null;
	}

	protected override IEnumerator Brain()
	{
		FrickData.D_HasSaidStartFrickConversation = true;
		while (!FlacidityActivated)
		{
			yield return WaitForSecondCache.Seconds0_1;
			if (FrickData.BotHasLead)
			{
				yield return BotLead();
			}
			else if (Owner.CanCum)
			{
				if (Controller.CockManager.IsCockLimbInside())
				{
					yield return BotLeadCumByHerself();
				}
				else if (Controller.VaginaInsertionHandler.IsCurrentAxis(Controller.VaginaFingeringFuckAxis))
				{
					yield return BotCumFingering();
				}
			}
		}
		yield return WaitUntilReactionNotRunning();
		DoReactionUnstoppable(GotFlacidReaction());
	}

	public override void OnEventFired(InteractionMemoryEventEnum type)
	{
	}

	public void CommentEdging()
	{
		if (!IsTalking)
		{
			if (FrickData.D_Edge)
			{
				CommonDialogue.EdgingOn.StartDialogueFromModelBrainState(this);
			}
			else
			{
				CommonDialogue.EdgingOff.StartDialogueFromModelBrainState(this);
			}
		}
	}

	public static IEnumerator Live2DTween(Live2DParameter parameter, float targetValue, float time)
	{
		return CoroutineUtilities.GenericTweenEaseOutQuart(parameter.SetRealValue, parameter.GetRealValue(), targetValue, time);
	}

	public static IEnumerator Live2DTweenRelative(Live2DParameter parameter, float targetValue, float secondsFullChange)
	{
		float num = Mathf.Abs(targetValue - parameter.GetRealValue()) / parameter.Length;
		return CoroutineUtilities.GenericTweenEaseOutQuart(parameter.SetRealValue, parameter.GetRealValue(), targetValue, num * secondsFullChange);
	}
}
