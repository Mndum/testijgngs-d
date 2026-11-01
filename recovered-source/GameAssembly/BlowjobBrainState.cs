using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Fungus;
using IC.AutomaticReference;
using UnityEngine;

public class BlowjobBrainState : CommonLewdSceneBrainState<Live2DBlowjobController>, IHasNonJitClassDescription
{
	public enum BlowjobReaction
	{
		Nothing,
		Fast,
		Slow,
		Lick,
		SwallowCum,
		CommentEdging,
		BotLead,
		SpecialUnstoppableReaction,
		BegStop,
		SwitchToCowgirl
	}

	[Serializable]
	public class BlowjobConversations : LocalizedStringReferenceContainer
	{
		public SerializedLocalizedConversation SwitchToCowgirlBotLead;

		public SerializedLocalizedConversation SwitchToCowgirlBotRape;

		public SerializedLocalizedConversation SwallowWithMl;

		public override string ContainerName => "Blowjob";
	}

	public class BlowjobDialogueArea : StoryBotDialogueAreaDefinition
	{
		public StoryBotDialogueTagDefinition EdgingAlmostCame;

		public StoryBotDialogueTagDefinition EdgingHadToSlowdown;

		public StoryBotDialogueTagDefinition EdgingForceStop;

		public StoryBotDialogueTagDefinition SwallowWithMl;

		public StoryBotDialogueTagDefinition SwallowFull;

		public StoryBotDialogueTagDefinition SwallowHalf;

		public StoryBotDialogueTagDefinition SwallowLittle;

		public StoryBotDialogueTagDefinition SwallowAlmostEmpty;

		public StoryBotDialogueTagDefinition SwitchToCowgirlBotLead;

		public StoryBotDialogueTagDefinition SwitchToCowgirlBotRape;

		public override string GetPrefix()
		{
			return "Blowjob_";
		}

		protected override void InitInner(StorySingleton storySingleton, StoryBotDialogueStage stage1, StoryBotDialogueStage stage2, StoryBotDialogueStage stage3)
		{
			Stage3VanillaDialogueIds id = Stage3VanillaDialogueIds.BlowjobDialogues;
			EdgingAlmostCame.FromTupleReference(stage3, id.GetGameId(0), LOC.Other.TupleReferences.Blowjob_EdgingAlmostCame);
			EdgingHadToSlowdown.FromTupleReference(stage3, id.GetGameId(1), LOC.Other.TupleReferences.Blowjob_EdgingHadToSlowdown);
			EdgingForceStop.FromTupleReference(stage3, id.GetGameId(2), LOC.Other.TupleReferences.Blowjob_EdgingForceStop);
			SwallowFull.FromTupleReference(stage3, id.GetGameId(3), LOC.Other.TupleReferences.Blowjob_SwallowFull);
			SwallowHalf.FromTupleReference(stage3, id.GetGameId(4), LOC.Other.TupleReferences.Blowjob_SwallowHalf);
			SwallowLittle.FromTupleReference(stage3, id.GetGameId(5), LOC.Other.TupleReferences.Blowjob_SwallowLittle);
			SwallowAlmostEmpty.FromTupleReference(stage3, id.GetGameId(6), LOC.Other.TupleReferences.Blowjob_SwallowAlmostEmpty);
			BlowjobConversations blowjobConversations = SingletonScriptableObject<CFLocalizationDataSO>.Instance.BlowjobConversations;
			SwitchToCowgirlBotLead.FromConversation(stage3, id.GetGameId(7), blowjobConversations.SwitchToCowgirlBotLead);
			SwitchToCowgirlBotRape.FromConversation(stage3, id.GetGameId(8), blowjobConversations.SwitchToCowgirlBotRape);
			SwallowWithMl.CreateContainer(stage3, out var o);
			o.AddBranch(new StoryBotDialogueBranch(id.GetGameId(9), blowjobConversations.SwallowWithMl));
		}
	}

	private class BlowjobBrainStateButtonSource : ButtonListButtonSource
	{
		private BlowjobBrainState _blowjobBrainState;

		private ButtonListWrapper _requestButtonWrapper;

		public BlowjobBrainStateButtonSource(BlowjobBrainState blowjobBrainState)
		{
			_blowjobBrainState = blowjobBrainState;
		}

		public override void AddButtonsToList(IModificationPeriodButtonList buttonList)
		{
			_requestButtonWrapper = buttonList.AddButton(-10);
			_requestButtonWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Frick_Request);
			_requestButtonWrapper.OnClick = RequestButtonClicked;
		}

		private void RequestButtonClicked()
		{
			List<MenuOption> list = new List<MenuOption>();
			Live2DControllerCommonStateFrickData brainAdditionalData = _blowjobBrainState.FrickData;
			BlowjobReaction currentRequest = _blowjobBrainState.CurrentReaction;
			if (_blowjobBrainState.IsFree && _blowjobBrainState.Controller.CockManager.IsCockNormalStateOrHidden)
			{
				List<(BlowjobReaction, long)> list2 = new List<(BlowjobReaction, long)>();
				List<(OnCumChoiceEnum, long)> list3 = new List<(OnCumChoiceEnum, long)>();
				switch (brainAdditionalData.D_BotLeadStatus)
				{
				default:
					if (_blowjobBrainState.controller.CumInsideManager.CurrentCumMouth > 0.01f)
					{
						list.Add(new MenuOption
						{
							text = LOC.UI.Frick_Blowjob_SwallowCum,
							beforeNextBlock = () =>
							{
								_blowjobBrainState.React(BlowjobReaction.SwallowCum);
								UiOverlay.Instance.CloseFungusOverlay();
							}
						});
					}
					if (GameVariables.Current.IsBotSmart)
					{
						list2.Add((BlowjobReaction.BotLead, 117175207793135616L));
					}
					list2.Add((BlowjobReaction.Fast, 37740606896500736L));
					list2.Add((BlowjobReaction.Slow, 37740608968486912L));
					list2.Add((BlowjobReaction.Lick, 37740610595876864L));
					list2.Add((BlowjobReaction.Nothing, 37740717030535168L));
					list2.RemoveAll(((BlowjobReaction request, long localizationId) t) => t.request == currentRequest);
					list3.Add((OnCumChoiceEnum.Automatic, 41349591252996096L));
					list3.Add((OnCumChoiceEnum.Inside, 41349597364097024L));
					list3.Add((OnCumChoiceEnum.Outside, 41349599742267392L));
					list3.RemoveAll(((OnCumChoiceEnum request, long localizedId) t) => t.request == brainAdditionalData.D_CurrentOnCum);
					break;
				case BotFrickControlStatusEnum.BotLead:
					list2.Add((BlowjobReaction.Nothing, 37740717030535168L));
					break;
				case BotFrickControlStatusEnum.BotFullLead:
					list2.Add((BlowjobReaction.BegStop, 203832649683177472L));
					break;
				}
				list.AddRange(list2.Select(((BlowjobReaction request, long localizationId) t) => new MenuOption
				{
					beforeNextBlock = () =>
					{
						_blowjobBrainState.React(t.request);
						UiOverlay.Instance.CloseFungusOverlay();
					},
					text = t.localizationId.GetUI()
				}));
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
						if (currentRequest == BlowjobReaction.Nothing)
						{
							_blowjobBrainState.React(BlowjobReaction.CommentEdging);
						}
					}
				});
				if (!brainAdditionalData.D_Edge && !brainAdditionalData.BotHasLead)
				{
					list.Add(new MenuOption
					{
						text = (brainAdditionalData.D_ContinueAfterCumming ? LOC.UI.Frick_Blowjob_StopAfterCumming : LOC.UI.Frick_Blowjob_ContinueAfterCumming),
						beforeNextBlock = () =>
						{
							UiOverlay.Instance.CloseFungusOverlay();
							brainAdditionalData.D_ContinueAfterCumming = !brainAdditionalData.D_ContinueAfterCumming;
						}
					});
				}
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

	private AudioClip[] lickSoundEffect;

	private AudioClip[] swallowSoundEffect;

	private float _blowjobSpeed;

	private BlowjobBrainStateButtonSource _buttonSource;

	protected static BlowjobDialogueArea BlowjobDialogue => SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<BlowjobDialogueArea>();

	public bool IsFree => !IsReactingUnstoppable;

	public BlowjobReaction CurrentReaction { get; private set; }

	private IEnumerator GoToTarget(float target, float speed = 1f)
	{
		float speedMulti = 1f;
		float dir;
		while (true)
		{
			float currentDepth = controller.MouthFuckAxis.CurrentDepth;
			dir = target - currentDepth;
			float maxMove = Time.deltaTime * speedMulti * speed;
			if (FrickData.D_Edge && controller.CockManager.TargetCumBarValue > 0.8f && controller.MouthFuckAxis.IsInside)
			{
				FrickData.D_HadToSlowDown = true;
				maxMove *= 1f - controller.CockManager.TargetCumBarValue;
			}
			speedMulti = (speedMulti + UnityEngine.Random.Range(0.8f, 1.2f)) / 2f;
			if (FrickData.D_Edge && controller.CockManager.TargetCumBarValue > 0.95f && controller.MouthFuckAxis.IsInside)
			{
				FrickData.D_HadToStop = true;
				yield return WaitForSecondCache.Seconds1;
			}
			if (Mathf.Abs(dir) < maxMove)
			{
				break;
			}
			dir = Mathf.Clamp(dir, 0f - maxMove, maxMove);
			controller.MouthFuckAxis.ThrustBot(dir);
			yield return null;
		}
		controller.MouthFuckAxis.ThrustBot(dir);
	}

	private void TrySwitchToCowgirl()
	{
		if (Controller.CockManager.TryGetCockLimbUsage(out var o) && o.IsInNormalState)
		{
			UiOverlay.Instance.OpenFungusOverlay();
			SingletonMonoBehaviour<FungusEnumerators>.Instance.SwitchSexScene(SingletonMonoBehaviour<ViewSingleton>.Instance.CowgirlView).Forget();
		}
	}

	private IEnumerator BegStopBlowjob()
	{
		yield return GoToTarget(0f, 2f);
		controller.Mouth.CloseMouth();
		yield return BegStop();
	}

	private IEnumerator EdgingResponses()
	{
		if (!FrickData.D_HadToSlowDown && !FrickData.D_HadToStop)
		{
			yield break;
		}
		float value = UnityEngine.Random.value;
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotSmart)
		{
			if (value < 0.2f)
			{
				yield return GoToTarget(0f);
				yield return WaitForSecondCache.Seconds0_1;
				if (FrickData.D_HadToStop)
				{
					yield return BlowjobDialogue.EdgingAlmostCame.StartDialogueFromModelBrainStateAndWait(this);
				}
				else
				{
					yield return BlowjobDialogue.EdgingHadToSlowdown.StartDialogueFromModelBrainStateAndWait(this);
				}
			}
			else if (value < 0.25f && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lust > 100 && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CurrentHorniness > 0.5f && FrickData.D_EdgedForThrusts > 5)
			{
				FrickData.D_EdgedForThrusts = 0;
				yield return GoToTarget(0f);
				yield return WaitForSecondCache.Seconds0_1;
				yield return BlowjobDialogue.EdgingForceStop.StartDialogueFromModelBrainStateAndWait(this);
				FrickData.D_Edge = false;
			}
		}
		FrickData.D_HadToStop = false;
		FrickData.D_HadToSlowDown = false;
	}

	private IEnumerator Blowjob()
	{
		if (_blowjobSpeed <= 0f)
		{
			_blowjobSpeed = 1f;
		}
		while (true)
		{
			FrickData.D_HadToSlowDown = false;
			yield return GoToTarget(controller.TipHeight + 0.05f, UnityEngine.Random.Range(0.8f, 1.2f) * _blowjobSpeed);
			yield return WaitForSecondCache.Seconds0_05;
			yield return GoToTarget(0.95f + UnityEngine.Random.Range(0f, 0.05f), UnityEngine.Random.Range(0.8f, 1.2f) * _blowjobSpeed);
			yield return WaitForSecondCache.Seconds0_05;
			if (FrickData.D_Edge)
			{
				FrickData.D_EdgedForThrusts++;
				yield return EdgingResponses();
			}
		}
	}

	private void ReactionMakeSureBlowjob()
	{
		if ((CurrentReaction != BlowjobReaction.Slow && CurrentReaction != BlowjobReaction.Fast) || !IsReacting)
		{
			DoReaction(Blowjob());
		}
	}

	private IEnumerator WaitTillEndsCumming()
	{
		if (FrickData.D_Edge && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotSmart)
		{
			Owner.CausePain(0.5f);
		}
		if (!controller.HeadHeld)
		{
			switch (FrickData.D_CurrentOnCum)
			{
			case OnCumChoiceEnum.Inside:
				yield return GoToTarget(1f, 2f).StopAfter(0.5f);
				break;
			case OnCumChoiceEnum.Outside:
				yield return GoToTarget(0f, 2f).StopAfter(0.5f);
				controller.ParamMouthOpen.Value = 2f;
				break;
			default:
				if (controller.MouthFuckAxis.CurrentDepth > 1f - (1f - controller.TipHeight) * 0.5f)
				{
					yield return GoToTarget(1f, 2f).StopAfter(0.5f);
					break;
				}
				yield return GoToTarget(0f, 2f).StopAfter(0.5f);
				controller.ParamMouthOpen.Value = 2f;
				break;
			}
		}
		yield return new WaitUntil(() => Controller.CockManager.TryGetCockLimbUsage(out var o) && o.IsInNormalState);
	}

	private void SetLickValue(Vector2 v)
	{
		controller.ParamToungeX.Value = v.x;
		controller.ParamToungeY.Value = v.y;
		v *= 0.1f;
		controller.ParamHeadX.Value = v.x;
		controller.ParamHeadY.Value = v.y;
		controller.ParamBodyX.Value = v.x;
		controller.ParamCockAngleZ.Value = v.x;
		controller.ParamDickAngleY.Value = v.y;
	}

	private IEnumerator LickTarget(Vector2 target, float time = 0.5f)
	{
		Vector2 start = new Vector2(controller.ParamToungeX.Value, controller.ParamToungeY.Value);
		Vector2 last = start;
		return CoroutineUtilities.GenericTween((float t) =>
		{
			t = EasingFunction.EaseOutCubic(0f, 1f, t);
			Vector2 vector = Vector2.Lerp(start, target, t);
			SetLickValue(vector);
			controller.FireEvent(InteractionMemoryEventEnum.LickingTheTip);
			Vector2 vector2 = vector - last;
			if (FrickData.D_Edge && controller.CockManager.TargetCumBarValue > 0.9f)
			{
				FrickData.D_HadToSlowDown = true;
			}
			else
			{
				controller.CockManager.AddCumBarValue(vector2.magnitude * 0.01f);
			}
			last = vector;
		}, time);
	}

	private bool ValidLick()
	{
		return Mathf.Abs(controller.MouthFuckAxis.CurrentDepth - controller.TipHeight) < 0.01f;
	}

	private IEnumerator ReactionLick()
	{
		int i = 0;
		while (true)
		{
			yield return WaitForSecondCache.Seconds0_1;
			if (UnityEngine.Random.value < 0.5f)
			{
				controller.ParamMouthOpen.Value = 2f;
				SingletonMonoBehaviour<SoundSingleton>.Instance.PlayWithPitch(lickSoundEffect.GetRandom(), UnityEngine.Random.Range(0.8f, 1.2f), UnityEngine.Random.Range(0.8f, 1.2f));
				yield return LickTarget(new Vector2(1f, 0f)).CheckEveryStep(ValidLick);
				SingletonMonoBehaviour<SoundSingleton>.Instance.PlayWithPitch(lickSoundEffect.GetRandom(), UnityEngine.Random.Range(0.8f, 1.2f), UnityEngine.Random.Range(0.8f, 1.2f));
				yield return LickTarget(new Vector2(-1f, 0f)).CheckEveryStep(ValidLick);
			}
			else
			{
				controller.ParamMouthOpen.Value = 1f;
				SingletonMonoBehaviour<SoundSingleton>.Instance.PlayWithPitch(lickSoundEffect.GetRandom(), UnityEngine.Random.Range(0.8f, 1.2f), UnityEngine.Random.Range(0.8f, 1.2f));
				yield return LickTarget(new Vector2(0f, -1f)).CheckEveryStep(ValidLick);
				SingletonMonoBehaviour<SoundSingleton>.Instance.PlayWithPitch(lickSoundEffect.GetRandom(), UnityEngine.Random.Range(0.8f, 1.2f), UnityEngine.Random.Range(0.8f, 1.2f));
				yield return LickTarget(new Vector2(0f, 1f)).CheckEveryStep(ValidLick);
			}
			i++;
			if (i > 5)
			{
				if (FrickData.D_Edge)
				{
					yield return EdgingResponses();
				}
				i = 0;
				FrickData.D_HadToStop = false;
				FrickData.D_HadToSlowDown = false;
			}
			yield return GoToTarget(controller.TipHeight, 2f);
		}
	}

	private void ResetEyes()
	{
		controller.Eyes.ToDefaultEyesPosition();
	}

	private IEnumerator ReactionSwitchToCowgirl()
	{
		if (GameVariables.Current.CanDoCowgirl && !controller.CockManager.IsFlacid)
		{
			CurrentReaction = BlowjobReaction.SwitchToCowgirl;
			if (FrickData.IsBotCompletelyInTheLead)
			{
				yield return BlowjobDialogue.SwitchToCowgirlBotRape.StartDialogueFromModelBrainStateAndWait(this);
			}
			else
			{
				yield return BlowjobDialogue.SwitchToCowgirlBotLead.StartDialogueFromModelBrainStateAndWait(this);
			}
			TrySwitchToCowgirl();
			CurrentReaction = BlowjobReaction.Nothing;
		}
	}

	private IEnumerator ReactionSwallow()
	{
		yield return GoToTarget(0f, 2f);
		yield return WaitForSecondCache.Seconds0_5;
		float currentCumMouth = controller.CumInsideManager.CurrentCumMouth;
		float normalizedMl = currentCumMouth / 30f;
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotSmart)
		{
			if (Owner.PersonalityData.ChanceToSayExactCumMlWhenSwallowing.Check() && currentCumMouth > 2f)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.SetIntegerVariable("Integer", Mathf.CeilToInt(currentCumMouth));
				yield return BlowjobDialogue.SwallowWithMl.StartDialogueFromModelBrainStateAndWait(this);
			}
			else if ((double)normalizedMl > 0.75)
			{
				yield return BlowjobDialogue.SwallowFull.StartDialogueFromModelBrainStateAndWait(this);
			}
			else if (normalizedMl > 0.5f)
			{
				yield return BlowjobDialogue.SwallowHalf.StartDialogueFromModelBrainStateAndWait(this);
			}
			else if (normalizedMl > 0.25f)
			{
				yield return BlowjobDialogue.SwallowLittle.StartDialogueFromModelBrainStateAndWait(this);
			}
			else
			{
				yield return BlowjobDialogue.SwallowAlmostEmpty.StartDialogueFromModelBrainStateAndWait(this);
			}
		}
		controller.ParamMouthOpen.Value = 2f;
		yield return WaitForSecondCache.Seconds0_5;
		controller.ParamMouthOpen.Value = 0f;
		yield return WaitForSecondCache.Seconds0_5;
		controller.ParamMouthOpen.Value = -1f;
		controller.ParamHeadY.SetPercent(0.8f);
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayWithPitch(swallowSoundEffect.GetRandom(), 1f, 1.25f - normalizedMl * 0.5f);
		yield return WaitForSecondCache.Seconds0_5;
		controller.ParamMouthOpen.Value = 0f;
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.SwallowCum(controller.CumInsideManager.CurrentCumMouth);
		controller.FireEvent((normalizedMl < 0.4f) ? InteractionMemoryEventEnum.SwallowingCumLittle : InteractionMemoryEventEnum.SwallowingCumALot);
		controller.CumInsideManager.ClearCumInside(InsertionHandlerOrificeTypeEnum.Mouth);
		controller.ParamHeadY.Value = 0f;
		yield return WaitForSecondCache.Seconds0_5;
		controller.ParamMouthOpen.Value = 2f;
		if (FrickData.BotHasLead && Owner.PersonalityData.SwitchToCowgirlAfterSwallowOdds.Check())
		{
			yield return ReactionSwitchToCowgirl();
		}
		CurrentReaction = BlowjobReaction.Nothing;
	}

	private IEnumerator ReactionCommentEdging()
	{
		yield return GoToTarget(0f, 0.5f);
		if (FrickData.D_Edge)
		{
			yield return CommonDialogue.EdgingOn.StartDialogueFromModelBrainStateAndWait(this);
		}
		else
		{
			yield return CommonDialogue.EdgingOff.StartDialogueFromModelBrainStateAndWait(this);
		}
		CurrentReaction = BlowjobReaction.Nothing;
	}

	public override void DoStopBotLeadReaction()
	{
		React(BlowjobReaction.Nothing);
	}

	public void React(BlowjobReaction reaction)
	{
		if (IsReactingUnstoppable)
		{
			return;
		}
		ResetEyes();
		switch (reaction)
		{
		default:
			StopReaction();
			SetBrainFrickControlStatus(BotFrickControlStatusEnum.None);
			break;
		case BlowjobReaction.BegStop:
			DoReaction(BegStopBlowjob());
			return;
		case BlowjobReaction.Fast:
			_blowjobSpeed = 4f;
			ReactionMakeSureBlowjob();
			break;
		case BlowjobReaction.Slow:
			_blowjobSpeed = 0.7f;
			ReactionMakeSureBlowjob();
			break;
		case BlowjobReaction.Lick:
			DoReaction(ReactionLick(), (ManagedCoroutine.FinishedType x) =>
			{
				controller.ParamToungeX.Value = 0f;
				controller.ParamMouthOpen.Value = 2f;
				SetLickValue(Vector2.zero);
			});
			break;
		case BlowjobReaction.SwallowCum:
		{
			Live2DToolSelector toolSelector = controller.CurrentToolSelector;
			StopReaction();
			toolSelector.gameObject.SetActive(value: false);
			DoReactionUnstoppable(ReactionSwallow(), (ManagedCoroutine.FinishedType x) =>
			{
				toolSelector.gameObject.SetActive(value: true);
			});
			break;
		}
		case BlowjobReaction.CommentEdging:
			DoReaction(ReactionCommentEdging());
			break;
		case BlowjobReaction.BotLead:
			SetBrainFrickControlStatus(BotFrickControlStatusEnum.BotLead);
			break;
		}
		CurrentReaction = reaction;
	}

	public override void OnEventFired(InteractionMemoryEventEnum type)
	{
		switch (type)
		{
		case InteractionMemoryEventEnum.CummingStarted:
			OnCum();
			break;
		case InteractionMemoryEventEnum.Insert:
			OnInserted();
			break;
		case InteractionMemoryEventEnum.ForcedBlowjob:
			OnForced();
			break;
		}
	}

	private void OnInserted()
	{
		ResetEyes();
		if (CurrentReaction == BlowjobReaction.Lick)
		{
			React(BlowjobReaction.Slow);
		}
	}

	private void OnForced()
	{
		ResetEyes();
		if (CurrentReaction != BlowjobReaction.Nothing)
		{
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotSmart)
			{
				float num = 0.8f - Owner.Pain;
				if (num > 0f)
				{
					Owner.CausePain(num);
				}
			}
			StopReaction();
		}
		SayDialog sayDialog = UiOverlay.Instance._speechSayDialogPopup.sayDialog;
		if (sayDialog.GetWriter().IsWriting && sayDialog.SpeakingCharacter.IsBotCharacter())
		{
			sayDialog.Stop();
		}
	}

	private void OnCum()
	{
		BlowjobReaction previousReaction = CurrentReaction;
		ResetEyes();
		StopReaction();
		DoReactionUnstoppable(WaitTillEndsCumming(), (ManagedCoroutine.FinishedType x) =>
		{
			CurrentReaction = BlowjobReaction.Nothing;
			if (!FrickData.BotHasLead)
			{
				if (FrickData.D_ContinueAfterCumming)
				{
					if (previousReaction == BlowjobReaction.Fast || previousReaction == BlowjobReaction.Lick || previousReaction == BlowjobReaction.Slow)
					{
						React(previousReaction);
					}
				}
				else
				{
					React(BlowjobReaction.Nothing);
				}
			}
		});
	}

	private IEnumerator BotLeadBrain()
	{
		while (FrickData.BotHasLead)
		{
			if (!IsReacting && !controller.HeadHeld)
			{
				if (controller.CockManager.IsFlacid && FrickData.IsBotCompletelyInTheLead)
				{
					SetBrainFrickControlStatus(BotFrickControlStatusEnum.None);
					TrySwitchToCowgirl();
					break;
				}
				float value = UnityEngine.Random.value;
				if (value < 0.6f)
				{
					if (controller.CumInsideManager.CurrentCumMouth > 1.5f)
					{
						React(BlowjobReaction.SwallowCum);
					}
					else if (!controller.CockManager.IsFlacid)
					{
						React(BlowjobReaction.Fast);
					}
					else
					{
						React(BlowjobReaction.Slow);
					}
				}
				else if (value < 0.85f)
				{
					React(BlowjobReaction.Slow);
				}
				else
				{
					React(BlowjobReaction.Lick);
				}
			}
			yield return WaitForSecondCache.Seconds0_2;
			yield return WaitUntilReactionNotRunningOrTimeOut(UnityEngine.Random.Range(1, 5));
		}
	}

	protected override IEnumerator Brain()
	{
		if (FrickData.BotHasLead)
		{
			FrickData.D_AlreadyInsertedOnce = true;
			React(BlowjobReaction.Fast);
		}
		WaitForSeconds wait = WaitForSecondCache.Seconds1;
		while (true)
		{
			yield return wait;
			if (!IsReacting && !controller.HeadHeld)
			{
				controller.ParamHeadY.ResetToDefault();
				if (FrickData.BotHasLead)
				{
					yield return BotLeadBrain();
				}
				else if (CurrentReaction != BlowjobReaction.Nothing)
				{
					if (controller.CockManager.IsCockLimbInside())
					{
						React((CurrentReaction != BlowjobReaction.Lick) ? BlowjobReaction.Fast : BlowjobReaction.Slow);
					}
					else
					{
						React(BlowjobReaction.Lick);
					}
				}
			}
			if (UnityEngine.Random.value > 0.5f && CurrentReaction == BlowjobReaction.Nothing)
			{
				controller.Eyes.SetEyesPosition(0f, -0.5f, Live2DController.EyeSelection.BothEyesTarget);
			}
			else
			{
				ResetEyes();
			}
		}
	}

	public override void EnterState()
	{
		base.EnterState();
		lickSoundEffect = R.Sound.Blowjob_lick_All.LoadAll().ToArray();
		swallowSoundEffect = R.Sound.Blowjob_swallow_All.LoadAll().ToArray();
		controller.SetCurrentToolSelectorActive(value: true);
		UiOverlay.Instance.GetStaticGUI<FrickStaticGui>(out var staticGui);
		_buttonSource = new BlowjobBrainStateButtonSource(this);
		staticGui.RightButtonList.AddButtonSource(_buttonSource);
	}

	public override void ExitState()
	{
		_buttonSource.Remove();
		_buttonSource = null;
		base.ExitState();
	}
}
