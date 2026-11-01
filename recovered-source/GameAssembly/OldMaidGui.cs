using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;
using UnityEngine.UI;

public class OldMaidGui : StaticGuiBase
{
	public enum OldMaidEvents
	{
		WaitingForPlayer,
		CardPickingByPlayerStart,
		CardHoverByPlayerQueen,
		CardHoverByPlayerOther,
		CardPickedByPlayerQueen,
		CardPickedByPlayerOther,
		CardPickingStart,
		CardHover,
		CardPickedChosen,
		CardPickedQueen,
		CardPickedOther,
		Win,
		Lose,
		StartingWithoutQueen,
		StartingWithQueen,
		PlayerLeft,
		StartGame
	}

	private enum HandPositions
	{
		Normal,
		PlayerPicking
	}

	[SerializeField]
	private HandScript playerHand;

	[SerializeField]
	private HandScript opponentHand;

	[SerializeField]
	private Button returnButton;

	[SerializeField]
	private ButtonList buttonList;

	private CardDeck _deck;

	private OldMaidBrainState _brainState;

	public HandScript PlayerHand => playerHand;

	public HandScript OpponentHand => opponentHand;

	public Button ReturnButton => returnButton;

	public ButtonList ButtonList => buttonList;

	protected override void FillReferencesInherited()
	{
		Transform transform = base.transform;
		transform.EnsureComponentInChild<HandScript>(out playerHand, "Player Hand");
		transform.EnsureComponentInChild<HandScript>(out opponentHand, "Opponent Hand");
		transform.EnsureComponentInChild<Button>(out returnButton, "Return Button");
		transform.EnsureComponentInChild<ButtonList>(out buttonList, "Side Buttons");
	}

	private IEnumerator DoEvent(OldMaidEvents type)
	{
		_brainState.ReportOldMaidEvent(type);
		return _brainState.WaitUntilNotReactingAndNotTalking();
	}

	public void UpdateButtons()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (buttonList.DoInitialization(out modificationPeriod))
		{
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
			buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.UI_GameInstruction);
			buttonListWrapper.OnClick = () =>
			{
				UiOverlay.Instance.OkPopup(LOC.UI.UI_GameInstruction, LOC.UI.OldMaid_Instruction);
			};
		}
	}

	public void Clear()
	{
		playerHand.ClearHand();
		opponentHand.ClearHand();
	}

	public void StartGame(OldMaidBrainState oldMaidBrainState, Action finished)
	{
		_brainState = oldMaidBrainState;
		StartCoroutine(Game().Then(finished));
		UpdateButtons();
		ReturnButton.gameObject.SetActive(value: true);
		ReturnButton.onClick.SetListener(() =>
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.OldMaid_StopPrompt, () =>
			{
				ReturnButton.gameObject.SetActive(value: false);
				StopAllCoroutines();
				StartCoroutine(DoEvent(OldMaidEvents.PlayerLeft).Then(finished));
			});
		});
	}

	private static CoroutineUtilities.WaitForResult<CardScript> ChooseCard(HandScript hand, Func<CardScript, bool> pickable = null)
	{
		CoroutineUtilities.WaitForResult<CardScript> waitForResult = CoroutineUtilities.PrepareWaitForResultBetter<CardScript>();
		hand.PickCard(waitForResult.SetResult, pickable);
		return waitForResult;
	}

	private IEnumerator RemoveDuplicatePlayer()
	{
		CoroutineUtilities.WaitForResult<CardScript> chooseCard = ChooseCard(playerHand);
		yield return chooseCard;
		CardScript card1 = chooseCard.Result;
		while (true)
		{
			using (playerHand.AddModifer(() =>
			{
				foreach (CardScript card2 in playerHand.Cards)
				{
					if (card2.InternalCard.Rank == card1.InternalCard.Rank)
					{
						card2.MoveUp();
					}
				}
			}))
			{
				CoroutineUtilities.WaitForResult<CardScript> chooseCard2 = ChooseCard(playerHand);
				yield return chooseCard2;
				CardScript result = chooseCard2.Result;
				if (card1.InternalCard.Rank != result.InternalCard.Rank || card1 == result)
				{
					card1 = result;
					continue;
				}
				playerHand.RemoveCard(card1);
				playerHand.RemoveCard(result);
				break;
			}
		}
	}

	private bool HasDuplicates(HandScript handScript)
	{
		return handScript.Cards.ToLookup((CardScript t) => t.InternalCard.Rank).Any((IGrouping<CardDeck.Rank, CardScript> t) => t.Count() > 1);
	}

	private IEnumerator RemoveAllDuplicatesPlayer()
	{
		bool flag = HasDuplicates(playerHand);
		Coroutine coroutine = (flag ? StartCoroutine(MinigameCore.ShowText(transform, LOC.UI.OldMaid_RemovePairs, 1f)) : null);
		while (flag)
		{
			yield return RemoveDuplicatePlayer();
			flag = HasDuplicates(playerHand);
		}
		yield return coroutine;
	}

	private IEnumerator TakeCardFromOpponent()
	{
		yield return MoveHandsToPosition(HandPositions.PlayerPicking, 1f);
		yield return DoEvent(OldMaidEvents.CardPickingByPlayerStart);
		opponentHand.Hoverable = true;
		opponentHand.Mode = HandScript.CardMode.Normal;
		Coroutine coroutine = StartCoroutine(MinigameCore.ShowText(transform, LOC.UI.OldMaid_PlayerPicking, 1f));
		CoroutineUtilities.WaitForResult<CardScript> chooseCard1 = ChooseCard(opponentHand);
		yield return chooseCard1;
		CardScript chosenFirst = chooseCard1.Result;
		while (true)
		{
			using (opponentHand.AddModifer(() =>
			{
				chosenFirst.MoveUp();
			}))
			{
				DoEvent((chosenFirst.InternalCard.Rank == CardDeck.Rank.Queen) ? OldMaidEvents.CardHoverByPlayerQueen : OldMaidEvents.CardHoverByPlayerOther);
				CoroutineUtilities.WaitForResult<CardScript> chooseCard2 = ChooseCard(opponentHand);
				yield return chooseCard2;
				CardScript result = chooseCard2.Result;
				if (chosenFirst != result)
				{
					chosenFirst = result;
					continue;
				}
			}
			break;
		}
		chosenFirst.Visible = true;
		DoEvent((chosenFirst.InternalCard.Rank == CardDeck.Rank.Queen) ? OldMaidEvents.CardPickedByPlayerQueen : OldMaidEvents.CardPickedByPlayerOther);
		yield return WaitForSecondCache.Seconds0_1;
		opponentHand.Hoverable = false;
		opponentHand.Mode = HandScript.CardMode.Compressed;
		playerHand.GetCardFromOtherHand(opponentHand, chosenFirst);
		yield return MoveHandsToPosition(HandPositions.Normal, 1f);
		yield return coroutine;
	}

	private IEnumerator RemoveAllOpponentDuplicates()
	{
		ILookup<CardDeck.Rank, CardScript> lookup = opponentHand.Cards.ToLookup((CardScript t) => t.InternalCard.Rank);
		foreach (IGrouping<CardDeck.Rank, CardScript> item in lookup)
		{
			List<CardScript> rank = item.ToList();
			for (int i = 0; i < rank.Count - 1; i += 2)
			{
				yield return opponentHand.RemoveCardEnumerator(rank[i + 1]);
				opponentHand.RemoveCard(rank[i]);
			}
			if (rank.Count >= 2)
			{
				yield return WaitForSecondCache.Seconds0_5;
			}
		}
	}

	private IEnumerator TakeCardFromPlayer()
	{
		yield return MinigameCore.ShowText(transform, LOC.UI.OldMaid_OpponentPicking, 1f);
		yield return DoEvent(OldMaidEvents.CardPickingStart);
		CardScript card = playerHand.Cards[UnityEngine.Random.Range(0, playerHand.Cards.Count)];
		playerHand.Hoverable = false;
		if (playerHand.Cards.Any((CardScript t) => t.InternalCard.Rank == CardDeck.Rank.Queen))
		{
			int fakePicks = UnityEngine.Random.Range(0, Mathf.Max(playerHand.Cards.Count / 2, 2));
			for (int i = 0; i < fakePicks; i++)
			{
				CardScript randomCard = playerHand.Cards[UnityEngine.Random.Range(0, playerHand.Cards.Count)];
				yield return DoEvent(OldMaidEvents.CardHover);
				using (playerHand.AddModifer(() =>
				{
					randomCard.MoveUp();
				}))
				{
					yield return WaitForSecondCache.RandomUnderHalfSecond();
				}
			}
		}
		using (playerHand.AddModifer(() =>
		{
			card.MoveUp();
		}))
		{
			yield return DoEvent(OldMaidEvents.CardPickedChosen);
			yield return WaitForSecondCache.Seconds0_5;
		}
		playerHand.Hoverable = true;
		opponentHand.GetCardFromOtherHand(playerHand, card);
		yield return DoEvent((card.InternalCard.Rank == CardDeck.Rank.Queen) ? OldMaidEvents.CardPickedQueen : OldMaidEvents.CardPickedOther);
		card.Visible = false;
		yield return WaitForSecondCache.Seconds0_1;
		opponentHand.Shuffle();
		yield return WaitForSecondCache.Seconds0_1;
	}

	private IEnumerator MoveHandToAnchor(HandScript hand, Vector2 anchorTarget, float time = 1f)
	{
		Vector2 anchorStart = hand.RectTransform.anchoredPosition;
		if (time <= 0f)
		{
			hand.RectTransform.anchoredPosition = anchorTarget;
			return null;
		}
		return CoroutineUtilities.GenericTween((float t) =>
		{
			t = Mathf.SmoothStep(0f, 1f, t);
			hand.RectTransform.anchoredPosition = Vector2.Lerp(anchorStart, anchorTarget, t);
		}, time);
	}

	private IEnumerator MoveHandsToPosition(HandPositions ohp, float time)
	{
		if (ohp == HandPositions.Normal || ohp != HandPositions.PlayerPicking)
		{
			yield return CoroutineUtilities.WaitTogether(this, MoveHandToAnchor(opponentHand, new Vector2(425f, -50f), time), MoveHandToAnchor(playerHand, new Vector2(0f, 50f), time));
		}
		else
		{
			yield return CoroutineUtilities.WaitTogether(this, MoveHandToAnchor(playerHand, new Vector2(0f, -300f), time), MoveHandToAnchor(opponentHand, new Vector2(0f, -600f), time));
		}
	}

	private IEnumerator Game()
	{
		yield return DoEvent(OldMaidEvents.StartGame);
		_deck = CardDeck.GenerateSmallerDeck();
		_deck.Shuffle();
		int index = _deck.CardsInADeck.FindIndex((CardDeck.Card t) => t.Rank == CardDeck.Rank.Queen);
		_ = _deck.CardsInADeck[index];
		_deck.CardsInADeck.RemoveAt(index);
		playerHand.Mode = HandScript.CardMode.Normal;
		playerHand.Hoverable = true;
		opponentHand.Mode = HandScript.CardMode.Compressed;
		yield return MoveHandsToPosition(HandPositions.Normal, 0f);
		bool first = UnityEngine.Random.value > 0.5f;
		ManagedCoroutine mouseInputCoroutine = CoroutineUtilities.WaitForAnyKeyDown(this);
		float speed = 0.25f;
		for (int i = 0; i < _deck.CardsInADeck.Count; i++)
		{
			CardDeck.Card card = _deck.CardsInADeck[i];
			if ((i % 2 == 0) ^ first)
			{
				yield return playerHand.AddCardEnumerator(card, visible: true, speed);
			}
			else
			{
				yield return opponentHand.AddCardEnumerator(card, visible: false, speed);
			}
			if (mouseInputCoroutine.NotRunning)
			{
				speed = 0.05f;
			}
		}
		mouseInputCoroutine.Stop(this);
		if (opponentHand.Cards.Any((CardScript t) => t.InternalCard.Rank == CardDeck.Rank.Queen))
		{
			yield return DoEvent(OldMaidEvents.StartingWithQueen);
		}
		else
		{
			yield return DoEvent(OldMaidEvents.StartingWithoutQueen);
		}
		Coroutine coroutine = StartCoroutine(RemoveAllOpponentDuplicates());
		ManagedCoroutine playerPairs = new ManagedCoroutine();
		playerPairs.Start(this, RemoveAllDuplicatesPlayer());
		yield return coroutine;
		yield return WaitForSecondCache.Seconds2;
		if (playerPairs.Running)
		{
			yield return DoEvent(OldMaidEvents.WaitingForPlayer);
		}
		yield return playerPairs;
		bool turn = UnityEngine.Random.value > 0.5f;
		while (true)
		{
			if (playerHand.Cards.Count == 0)
			{
				opponentHand.Mode = HandScript.CardMode.Normal;
				opponentHand.SetCurrentCardsVisibility(value: true);
				yield return MinigameCore.ShowText(transform, LOC.Polyglot.GAME_STATUS_YOU_WIN);
				yield return DoEvent(OldMaidEvents.Win);
				yield break;
			}
			if (opponentHand.Cards.Count == 0)
			{
				break;
			}
			if (turn)
			{
				yield return TakeCardFromOpponent();
				yield return RemoveAllDuplicatesPlayer();
			}
			else
			{
				yield return TakeCardFromPlayer();
				yield return RemoveAllOpponentDuplicates();
			}
			var (enumerator, finishAction) = CoroutineUtilities.PrepareWaitForAction();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.StartPassTime(turn ? UnityEngine.Random.Range(1, 5) : UnityEngine.Random.Range(1, 3), finishAction, suppressEvents: true, GameVariables.StaminaSyncType.Normal);
			yield return enumerator;
			turn = !turn;
		}
		opponentHand.Mode = HandScript.CardMode.Normal;
		opponentHand.SetCurrentCardsVisibility(value: true);
		yield return MinigameCore.ShowText(transform, LOC.Polyglot.GAME_STATUS_YOU_LOST);
		yield return DoEvent(OldMaidEvents.Lose);
	}
}
