using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackWebsite : CasinoWebsiteCore
{
	public enum MoveChoice
	{
		None,
		Hit,
		Stand,
		Double
	}

	private enum Outcome
	{
		Win,
		Draw,
		Loss
	}

	private enum ButtonState
	{
		Disabled,
		PlayerMove,
		GameStart
	}

	[SerializeField]
	private GameObject cardPrefab;

	[SerializeField]
	private AudioClip takeCard;

	[SerializeField]
	private AudioClip placeCard;

	private StakeSelector _stakeSelector;

	private CasinoTextPopup _casinoTextPopup;

	private int _currentStake;

	private MoveChoice _moveChoice;

	private CardDeck _cardDeck;

	private HandScript _playersHand;

	private HandScript _dealersHand;

	private Coroutine _gameCoroutine;

	private TextMeshProUGUI _playerHandInfo;

	private TextMeshProUGUI _dealersHandInfo;

	private Transform _buttonsContainer;

	private Button _startButton;

	private Button _hitButton;

	private Button _standButton;

	private Button _doubleButton;

	private int GetCardValue(CardDeck.Card card)
	{
		if (card.Rank <= CardDeck.Rank.c10)
		{
			return (int)card.Rank;
		}
		if (card.Rank == CardDeck.Rank.Ace)
		{
			return 11;
		}
		return 10;
	}

	private int GetHandValue(HandScript hand)
	{
		ReadOnlyCollection<CardScript> cards = hand.Cards;
		int num = 0;
		int num2 = 0;
		foreach (CardScript item in cards)
		{
			if (item.Visible)
			{
				CardDeck.Card internalCard = item.InternalCard;
				if (internalCard.Rank == CardDeck.Rank.Ace)
				{
					num++;
				}
				num2 += GetCardValue(internalCard);
			}
		}
		while (num > 0 && num2 > 21)
		{
			num2 -= 10;
			num--;
		}
		return num2;
	}

	private IEnumerator Conclusion(string info, Outcome outcome)
	{
		yield return _casinoTextPopup.ShowText(info);
		switch (outcome)
		{
		case Outcome.Win:
			AvailableTokens += _currentStake * 2;
			yield return _casinoTextPopup.ShowText(LOC.UI.casinodotic_Won(_currentStake * 2));
			break;
		default:
			AvailableTokens += _currentStake;
			yield return _casinoTextPopup.ShowText(LOC.UI.casinodotic_GotBack(_currentStake));
			break;
		case Outcome.Loss:
			break;
		}
	}

	private IEnumerator AddCardToHand(HandScript hand, float speed = 1f, bool visible = true)
	{
		CardDeck.Card card = _cardDeck.TakeRandom();
		yield return hand.AddCardEnumerator(card, visible, speed);
	}

	private IEnumerator RevealDealersCard()
	{
		_dealersHand.SetCurrentCardsVisibility(value: true);
		SingletonMonoBehaviour<SoundSingleton>.Instance.Play(placeCard, Random.Range(0.9f, 1f));
		yield return new WaitForSeconds(placeCard.length + 0.2f);
	}

	private void FinishGame()
	{
		_gameCoroutine = null;
		_stakeSelector.LockedIn = false;
		ChangeButtonsState(ButtonState.GameStart);
	}

	private IEnumerator BlackJackGame()
	{
		_cardDeck = CardDeck.GenerateFullDeck();
		_playersHand.ClearHand();
		_dealersHand.ClearHand();
		int playersHandValue = 0;
		int dealersHandValue = 0;
		UpdateHandInfo(playersHandValue, dealersHandValue);
		yield return AddCardToHand(_dealersHand, 0.2f, visible: false);
		yield return AddCardToHand(_dealersHand, 0.2f);
		dealersHandValue = GetHandValue(_dealersHand);
		UpdateHandInfo(playersHandValue, dealersHandValue);
		yield return AddCardToHand(_playersHand, 0.2f);
		playersHandValue = GetHandValue(_playersHand);
		UpdateHandInfo(playersHandValue, dealersHandValue);
		yield return AddCardToHand(_playersHand, 0.2f);
		playersHandValue = GetHandValue(_playersHand);
		UpdateHandInfo(playersHandValue, dealersHandValue);
		if (playersHandValue == 21)
		{
			yield return RevealDealersCard();
			dealersHandValue = GetHandValue(_dealersHand);
			UpdateHandInfo(playersHandValue, dealersHandValue);
			if (dealersHandValue == 21)
			{
				yield return Conclusion(LOC.UI.casinodotic_DoubleBlackjack, Outcome.Draw);
			}
			else
			{
				yield return Conclusion(LOC.UI.casinodotic_PlayerBlackjack, Outcome.Win);
			}
			FinishGame();
			yield break;
		}
		bool playerInput = playersHandValue < 21;
		while (playerInput)
		{
			ChangeButtonsState(ButtonState.PlayerMove);
			_moveChoice = MoveChoice.None;
			yield return new WaitWhile(() => _moveChoice == MoveChoice.None);
			switch (_moveChoice)
			{
			default:
				yield return AddCardToHand(_playersHand);
				playersHandValue = GetHandValue(_playersHand);
				UpdateHandInfo(playersHandValue, dealersHandValue);
				if (playersHandValue >= 21)
				{
					playerInput = false;
				}
				break;
			case MoveChoice.Stand:
				playerInput = false;
				break;
			case MoveChoice.Double:
				AvailableTokens -= _currentStake;
				_currentStake *= 2;
				yield return AddCardToHand(_playersHand);
				playersHandValue = GetHandValue(_playersHand);
				UpdateHandInfo(playersHandValue, dealersHandValue);
				playerInput = false;
				break;
			}
		}
		yield return RevealDealersCard();
		dealersHandValue = GetHandValue(_dealersHand);
		UpdateHandInfo(playersHandValue, dealersHandValue);
		while (dealersHandValue < 17 && playersHandValue <= 21)
		{
			yield return AddCardToHand(_dealersHand);
			dealersHandValue = GetHandValue(_dealersHand);
			UpdateHandInfo(playersHandValue, dealersHandValue);
		}
		if (playersHandValue > 21)
		{
			yield return Conclusion(LOC.UI.casinodotic_OverLose, Outcome.Loss);
		}
		else if (dealersHandValue > 21)
		{
			yield return Conclusion(LOC.UI.casinodotic_OverWin, Outcome.Win);
		}
		else if (playersHandValue < dealersHandValue)
		{
			yield return Conclusion(LOC.UI.casinodotic_CloserLose, Outcome.Loss);
		}
		else if (playersHandValue == dealersHandValue)
		{
			yield return Conclusion(LOC.UI.casinodotic_Draw, Outcome.Draw);
		}
		else
		{
			yield return Conclusion(LOC.UI.casinodotic_CloserWin, Outcome.Win);
		}
		FinishGame();
	}

	private void ChangeButtonsState(ButtonState buttonState)
	{
		switch (buttonState)
		{
		case ButtonState.Disabled:
			_startButton.gameObject.SetActive(value: false);
			_hitButton.gameObject.SetActive(value: false);
			_standButton.gameObject.SetActive(value: false);
			_doubleButton.gameObject.SetActive(value: false);
			break;
		case ButtonState.PlayerMove:
			_startButton.gameObject.SetActive(value: false);
			_hitButton.gameObject.SetActive(value: true);
			_standButton.gameObject.SetActive(value: true);
			_doubleButton.gameObject.SetActive(AvailableTokens >= _currentStake && _playersHand.CardCount == 2);
			break;
		default:
			_startButton.gameObject.SetActive(value: true);
			_hitButton.gameObject.SetActive(value: false);
			_standButton.gameObject.SetActive(value: false);
			_doubleButton.gameObject.SetActive(value: false);
			break;
		}
	}

	private void UpdateHandInfo(int player, int dealer)
	{
		_dealersHandInfo.text = LOC.UI.casinodotic_DealersHand(dealer);
		_playerHandInfo.text = LOC.UI.casinodotic_PlayersHand(player);
	}

	private void StartButtonClicked()
	{
		if (_gameCoroutine == null)
		{
			_currentStake = _stakeSelector.GetStake();
			if (_currentStake <= AvailableTokens && _currentStake >= 1)
			{
				_stakeSelector.LockedIn = true;
				AvailableTokens -= _currentStake;
				ChangeButtonsState(ButtonState.Disabled);
				_gameCoroutine = StartCoroutine(BlackJackGame());
			}
		}
	}

	private void HitButtonClicked()
	{
		_moveChoice = MoveChoice.Hit;
		ChangeButtonsState(ButtonState.Disabled);
	}

	private void StandButtonClicked()
	{
		_moveChoice = MoveChoice.Stand;
		ChangeButtonsState(ButtonState.Disabled);
	}

	private void DoubleButtonClicked()
	{
		_moveChoice = MoveChoice.Double;
		ChangeButtonsState(ButtonState.Disabled);
	}

	public void Awake()
	{
		_stakeSelector = transform.GetComponentInChildren<StakeSelector>();
	}

	public override IEnumerator Open()
	{
		_casinoTextPopup = gameObject.GetOrAddComponent<CasinoTextPopup>();
		_playersHand = transform.Find("Players Hand").GetComponent<HandScript>();
		_dealersHand = transform.Find("Dealers Hand").GetComponent<HandScript>();
		_playerHandInfo = transform.Find("Players Hand Info").GetComponent<TextMeshProUGUI>();
		_dealersHandInfo = transform.Find("Dealers Hand Info").GetComponent<TextMeshProUGUI>();
		UpdateHandInfo(0, 0);
		_buttonsContainer = transform.Find("Choices");
		_startButton = _buttonsContainer.Find("Start").GetComponent<Button>();
		_startButton.onClick.AddListener(StartButtonClicked);
		_hitButton = _buttonsContainer.Find("Hit").GetComponent<Button>();
		_hitButton.onClick.AddListener(HitButtonClicked);
		_standButton = _buttonsContainer.Find("Stand").GetComponent<Button>();
		_standButton.onClick.AddListener(StandButtonClicked);
		_doubleButton = _buttonsContainer.Find("Double").GetComponent<Button>();
		_doubleButton.onClick.AddListener(DoubleButtonClicked);
		ChangeButtonsState(ButtonState.GameStart);
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		yield return SlowlyEnableAllChildren(new List<Transform> { _casinoTextPopup.WinningsText.transform as RectTransform });
		_cardDeck = CardDeck.GenerateFullDeck();
		BrowserPopup.FinishLoading();
	}
}
