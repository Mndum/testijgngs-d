using System.Collections;
using System.Linq;
using Cards;
using UnityEngine;

public class OldMaidBrainState : ModelBrainState<Live2DInteractionController>, IHasNonJitClassDescription
{
	private OldMaidGui _oldMaid;

	private int clues;

	private bool lied;

	private float silenceRate = 0.5f;

	private float lieRateWant = 0.7f;

	private float lieRateDontWant = 0.2f;

	private float lieSuccessRate = 0.8f;

	public OldMaidBrainState(OldMaidGui oldMaid)
	{
		_oldMaid = oldMaid;
	}

	public override void EnterState()
	{
		base.EnterState();
		CanBeChanged = false;
		controller.Eyes.ToDefaultEyesPosition();
		controller.ToDefaultFaceAngle();
	}

	private IEnumerator Lie()
	{
		if (Random.value < 0.5f)
		{
			controller.Eyes.SetEyesPosition(-0.5f, 0f);
			yield return WaitForSecondCache.Seconds0_2;
			controller.Eyes.SetEyesPosition(0f, 0f);
			yield return WaitForSecondCache.Seconds1;
			controller.Eyes.SetEyesPosition(-0.5f, -0.5f);
			yield return WaitForSecondCache.Seconds0_2;
			controller.Eyes.SetEyesPosition(0f, 0f);
			yield return WaitForSecondCache.Seconds1;
		}
		else
		{
			controller.ParamHeadZ.AddPercent(0.2f);
			yield return WaitForSecondCache.Seconds2;
			controller.ParamHeadZ.AddPercent(-0.2f);
		}
	}

	private IEnumerator GiveClue(bool wantToTake)
	{
		if (Random.value < silenceRate)
		{
			yield return SayOldMaidSpecial(LOC.Other.Game_OldMaid_HintNoInfo);
			yield break;
		}
		float num = (wantToTake ? lieRateWant : lieRateDontWant);
		bool isLie = Random.value < num;
		if (isLie)
		{
			lied = true;
		}
		if (isLie && !(Random.value < lieSuccessRate))
		{
			yield return DoReaction(Lie(), (ManagedCoroutine.FinishedType type) =>
			{
				controller.ToDefaultFaceAngle();
				controller.Eyes.ToDefaultEyesPosition();
			});
		}
		if (wantToTake ^ isLie)
		{
			yield return SayOldMaidSpecial(LOC.Other.Game_OldMaid_HintDo);
		}
		else
		{
			yield return SayOldMaidSpecial(LOC.Other.Game_OldMaid_HintDont);
		}
	}

	private IEnumerator SayOldMaidSpecial(string text)
	{
		return SingletonMonoBehaviour<FungusEnumerators>.Instance.DoConversation(StringUtils.SplitStringAndPickRandom(text), waitForInput: false);
	}

	public void ReportOldMaidEvent(OldMaidGui.OldMaidEvents oldMaidEvent)
	{
		IEnumerator reaction = OldMaidSay(oldMaidEvent);
		DoTalking(reaction);
	}

	private IEnumerator OldMaidSay(OldMaidGui.OldMaidEvents oldMaidEvent)
	{
		bool flag = _oldMaid.PlayerHand.Cards.Any((CardScript t) => t.InternalCard.Rank == CardDeck.Rank.Queen);
		switch (oldMaidEvent)
		{
		case OldMaidGui.OldMaidEvents.StartGame:
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_StartGamePlayer).Then(SayOldMaidSpecial(LOC.Other.Game_OldMaid_StartGameBot));
		case OldMaidGui.OldMaidEvents.StartingWithQueen:
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_StartingWithQueen);
		case OldMaidGui.OldMaidEvents.StartingWithoutQueen:
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_StartingWithoutQueen);
		case OldMaidGui.OldMaidEvents.WaitingForPlayer:
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_Waiting);
		case OldMaidGui.OldMaidEvents.CardPickingByPlayerStart:
			clues = _oldMaid.OpponentHand.CardCount + 1;
			lied = false;
			if (!flag)
			{
				return SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerPickingStartWithQueen);
			}
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerPickingStartWithoutQueen);
		case OldMaidGui.OldMaidEvents.CardHoverByPlayerQueen:
			if (flag)
			{
				if (clues > 0)
				{
					clues = 0;
					return SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerPickingDoesntMatter);
				}
				return null;
			}
			if (clues <= 0)
			{
				return null;
			}
			clues--;
			return GiveClue(wantToTake: true);
		case OldMaidGui.OldMaidEvents.CardHoverByPlayerOther:
			if (flag)
			{
				if (clues > 0)
				{
					clues = 0;
					return SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerPickingDoesntMatter);
				}
				return null;
			}
			if (clues <= 0)
			{
				return null;
			}
			clues--;
			return GiveClue(wantToTake: false);
		case OldMaidGui.OldMaidEvents.CardPickedByPlayerQueen:
			if (lied)
			{
				return SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerPickedQueenAfterLying);
			}
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerPickedQueen);
		case OldMaidGui.OldMaidEvents.CardPickedByPlayerOther:
			if (_oldMaid.OpponentHand.Cards.Any((CardScript t) => t.InternalCard.Rank == CardDeck.Rank.Queen))
			{
				return SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerPickedCardOtherThanQueenWhileBotHasQueen);
			}
			return null;
		case OldMaidGui.OldMaidEvents.CardHover:
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_BotPickingCardsHover);
		case OldMaidGui.OldMaidEvents.CardPickedChosen:
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_BotPickingCardsChosen);
		case OldMaidGui.OldMaidEvents.CardPickedQueen:
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_BotChooseQueen);
		case OldMaidGui.OldMaidEvents.CardPickedOther:
			if (flag)
			{
				return SayOldMaidSpecial(LOC.Other.Game_OldMaid_BotChooseOtherWhilePlayerHasQueen);
			}
			return null;
		case OldMaidGui.OldMaidEvents.Win:
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.timesWonOldMaid++;
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.sympathy += 10;
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.MentalHealth += 0.05f;
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Mood += 0.1f;
			IEnumerator a2 = SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerWon).Then(SayOldMaidSpecial(LOC.Other.Game_OldMaid_Summary));
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.timesRanAwayOldMaid > 0)
			{
				a2 = a2.Then(SayOldMaidSpecial(LOC.Other.Game_OldMaid_Summary_Left));
			}
			return a2.Then(SayOldMaidSpecial(LOC.Other.Game_ThanksForPlaying));
		}
		case OldMaidGui.OldMaidEvents.Lose:
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.timesLostOldMaid++;
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.sympathy += 10;
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.MentalHealth += 0.05f;
			IEnumerator a = SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerLost).Then(SayOldMaidSpecial(LOC.Other.Game_OldMaid_Summary));
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.timesRanAwayOldMaid > 0)
			{
				a = a.Then(SayOldMaidSpecial(LOC.Other.Game_OldMaid_Summary_Left));
			}
			return a.Then(SayOldMaidSpecial(LOC.Other.Game_ThanksForPlaying));
		}
		case OldMaidGui.OldMaidEvents.PlayerLeft:
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.sympathy -= 4;
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.timesRanAwayOldMaid++;
			return SayOldMaidSpecial(LOC.Other.Game_OldMaid_PlayerLeft);
		default:
			return null;
		}
	}

	protected override IEnumerator Brain()
	{
		yield break;
	}
}
