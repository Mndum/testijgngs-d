using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFuckBrainState : CommonLewdSceneBrainState<Live2DLewdSceneController>, IHasNonJitClassDescription
{
	public enum GenericFuckBrainReactions
	{
		Cumming,
		Insert
	}

	private static readonly ReactionManager<GenericFuckBrainState> ReactionManager;

	private ReactionManagerData _reactionManagerData;

	public override bool IsInterested => _reactionManagerData.HasReactedRecently();

	static GenericFuckBrainState()
	{
		ReactionManager = new ReactionManager<GenericFuckBrainState>(new Dictionary<InteractionMemoryEnum, ReactionManager<GenericFuckBrainState>.Reaction>
		{
			[InteractionMemoryEnum.Handholding] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.sympathy++;
					gameVariables.lust++;
					if (Random.value > 0.6f)
					{
						caller.CommonDialogue.Handholding.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.FaceStroke] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.sympathy++;
					gameVariables.lust++;
				}
			},
			[InteractionMemoryEnum.KissMouth] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
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
						float x = Random.Range(-0.8f, 0.8f);
						float y = -0.5f;
						if (Random.Range(0f, 1f) < 0.9f)
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
			[InteractionMemoryEnum.KissBody] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (memory.TryGetAdditionalDataAs<StoryBotDialogueTagDefinition>(out var activateData) && caller.Owner.NotRecentlyStartedTalking && Random.value > 0.4f)
					{
						activateData.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.Fingering] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (Random.value > 0.6f)
					{
						caller.CommonDialogue.FingeringDuring.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.FingeringAnus] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (Random.value > 0.6f)
					{
						caller.CommonDialogue.FingeringAnusDuring.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.Headpat] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.sympathy++;
					gameVariables.lust++;
					if (Random.value > 0.6f)
					{
						caller.CommonDialogue.Headpat.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.BoobsFondle] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (Random.value > 0.6f)
					{
						caller.CommonDialogue.BoobFondle.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.NipplePull] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (Random.value > 0.6f)
					{
						caller.CommonDialogue.NipplePull.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.ClitRub] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (Random.value > 0.6f)
					{
						caller.CommonDialogue.ClitRub.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.PussySpread] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
					if (Random.value > 0.6f)
					{
						caller.CommonDialogue.PussySpread.StartDialogueFromModelBrainState(caller);
					}
				}
			},
			[InteractionMemoryEnum.ThumbMouth] = (GenericFuckBrainState caller, ComplexEventMemory memory, int row) =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.sympathy > 5 && gameVariables.IsBotAwake && memory.Length > 2f)
				{
					gameVariables.lust++;
				}
			}
		});
	}

	private void React(GenericFuckBrainReactions reactions)
	{
		if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotAwake || IsReactingUnstoppable)
		{
			return;
		}
		switch (reactions)
		{
		case GenericFuckBrainReactions.Cumming:
			if (!IsReacting)
			{
				controller.Eyes.DisableBlinking = true;
				controller.Expression.DisableExpression = true;
				DoReactionUnstoppable(Cum(), (ManagedCoroutine.FinishedType x) =>
				{
					controller.Eyes.DisableBlinking = false;
					controller.Expression.DisableExpression = false;
				});
			}
			break;
		case GenericFuckBrainReactions.Insert:
			if (!IsReacting)
			{
				DoReaction(Insert());
			}
			break;
		}
	}

	private IEnumerator Cum()
	{
		using Live2DExpression.CummingExpressionState cm = controller.Expression.StartCumming();
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
		for (int i = 0; i < cm.NumberOfTwitches; i++)
		{
			yield return new WaitForSeconds(cm.TwitchLength / 2f);
			cm.DoQuiver1();
			yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
			cm.DoQuiver2();
			yield return new WaitForSeconds(cm.TwitchLength / 2f);
			cm.AfterQuiver();
			if (Owner.PersonalityData.MoanOdds.Check())
			{
				MoanIfPossible();
			}
		}
		yield return cm.CumFinishTimeOut();
	}

	private IEnumerator Insert()
	{
		if (controller is ILive2DController_Lewd_BotTense live2DController_Lewd_BotTense)
		{
			live2DController_Lewd_BotTense.ParamTense.Value = 0f;
		}
		yield return WaitForSecondCache.Seconds0_2;
		if (controller is ILive2DController_BodyY live2DController_BodyY)
		{
			live2DController_BodyY.ParamBodyY.AddPercent(0.5f);
		}
		if (controller is ILive2DController_Lewd_BotTense live2DController_Lewd_BotTense2)
		{
			live2DController_Lewd_BotTense2.ParamTense.Value = 1f;
		}
		if (Owner.PersonalityData.MoanOdds.Check(0f, 0.5f))
		{
			MoanIfPossible();
		}
		yield return WaitForSecondCache.Seconds0_4;
	}

	public override void OnEventFired(InteractionMemoryEventEnum type)
	{
		if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotAwake)
		{
			return;
		}
		switch (type)
		{
		case InteractionMemoryEventEnum.Insert:
			React(GenericFuckBrainReactions.Insert);
			break;
		case InteractionMemoryEventEnum.CumInside:
			if (!IsTalking && Owner.PersonalityData.ChanceToCommentCummingInside.Check())
			{
				CommonDialogue.CumInside.StartDialogueFromModelBrainState(this);
			}
			break;
		case InteractionMemoryEventEnum.CumInsideAnal:
			if (!IsTalking && Owner.PersonalityData.ChanceToCommentCummingInsideAnal.Check())
			{
				CommonDialogue.CumInsideAnal.StartDialogueFromModelBrainState(this);
			}
			break;
		case InteractionMemoryEventEnum.CumOutside:
			if (!IsTalking && Owner.PersonalityData.ChanceToCommentCummingOutside.Check())
			{
				CommonDialogue.CumOutside.StartDialogueFromModelBrainState(this);
			}
			break;
		}
	}

	public override void EnterState()
	{
		base.EnterState();
		controller.SetCurrentToolSelectorActive(value: true);
	}

	protected override IEnumerator Brain()
	{
		yield return null;
		if (!FrickData.D_HasSaidStartFrickConversation)
		{
			FrickData.D_HasSaidStartFrickConversation = true;
			yield return CommonDialogue.StartFrick.StartDialogueFromModelBrainStateAndWait(this);
		}
		while (true)
		{
			if (!IsReacting)
			{
				if (!controller.IsApproximatelyDefaultFaceAngle() && !Owner.InteractionMemory.GetComplexMemory(InteractionMemoryEnum.GrabHead).Active)
				{
					yield return WaitForSecondCache.Seconds0_5;
					controller.ToDefaultFaceAngle();
				}
				if (Owner.CanCum)
				{
					React(GenericFuckBrainReactions.Cumming);
				}
				yield return WaitForSecondCache.Seconds0_2;
				controller.Eyes.ToDefaultEyesPosition();
				if (ReactionManager.React(ref _reactionManagerData, Owner.InteractionMemory, this))
				{
					yield return WaitForSecondCache.Seconds1;
				}
				else if (Owner.NotTalkedForAWhile)
				{
					if (controller is ILive2DController_Manager_CockManager live2DController_Manager_CockManager && live2DController_Manager_CockManager.CockManager.TryGetCockLimbUsage(out var o) && o.IsInside())
					{
						if (Owner.InteractionMemory.GetComplexMemory(InteractionMemoryEnum.CockInside).ActiveLength > 3f)
						{
							switch (o.CurrentTargetOrificeType())
							{
							case InsertionHandlerOrificeTypeEnum.Anal:
								yield return CommonDialogue.GenericFuckBrainStateRandomDialogueCockInsideAnal.StartDialogueFromModelBrainStateAndWait(this);
								break;
							case InsertionHandlerOrificeTypeEnum.Vagina:
								yield return CommonDialogue.GenericFuckBrainStateRandomDialogueCockInside.StartDialogueFromModelBrainStateAndWait(this);
								break;
							}
						}
					}
					else if (!Owner.IsInterested && Owner.InteractionMemory.GetComplexMemory(InteractionMemoryEnum.CockOutside).ActiveLength > 3f)
					{
						yield return CommonDialogue.GenericFuckBrainStateRandomDialogueCockOutside.StartDialogueFromModelBrainStateAndWait(this);
					}
				}
			}
			yield return WaitForSecondCache.Seconds0_5;
			if (Random.value > 0.5f)
			{
				yield return WaitForSecondCache.Seconds0_2;
			}
			if (Random.value > 0.5f)
			{
				yield return WaitForSecondCache.Seconds0_2;
			}
		}
	}

	public override void DoStopBotLeadReaction()
	{
	}
}
