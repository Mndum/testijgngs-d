using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cards;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandScript : MonoBehaviour
{
	public enum CardMode
	{
		Normal,
		Compressed
	}

	public class HandModifier : IDisposable
	{
		private Action _action;

		private HandScript _handScript;

		public HandModifier(Action action, HandScript handScript)
		{
			_action = action;
			_handScript = handScript;
		}

		public void Apply()
		{
			_action();
		}

		public void Dispose()
		{
			_handScript.RemoveModifer(this);
		}
	}

	[SerializeField]
	private GameObject cardPrefab;

	[SerializeField]
	private AudioClip takeCard;

	[SerializeField]
	private AudioClip placeCard;

	[SerializeField]
	private Transform cardSourceTransform;

	private readonly List<CardScript> _cards = new List<CardScript>();

	private CardMode _mode;

	private List<HandModifier> _specialModifiers = new List<HandModifier>();

	private bool _hoverable;

	private CardScript _hoveredCard;

	public RectTransform RectTransform => (RectTransform)transform;

	public int CardCount => _cards.Count;

	public ReadOnlyCollection<CardScript> Cards => _cards.AsReadOnly();

	public CardMode Mode
	{
		get
		{
			return _mode;
		}
		set
		{
			_mode = value;
			SyncCards();
		}
	}

	public bool Hoverable
	{
		get
		{
			return _hoverable;
		}
		set
		{
			_hoverable = value;
			foreach (CardScript card in _cards)
			{
				SyncHoverable(card);
			}
		}
	}

	public event Action<CardScript> OnCardHovered;

	private void LayoutCards(float spacing)
	{
		float num = 0f;
		foreach (CardScript card in _cards)
		{
			num += ((RectTransform)card.transform).sizeDelta.x + spacing;
		}
		float width = ((RectTransform)transform).rect.width;
		float num2 = ((_cards.Count > 0) ? (_cards[0].RectTransform.sizeDelta.x / 2f) : 0f);
		float num3 = ((_cards.Count > 0) ? (_cards[_cards.Count - 1].RectTransform.sizeDelta.x / 2f) : 0f);
		width -= num2;
		width -= num3;
		if (width < num)
		{
			num = width;
		}
		float a = (0f - num) / 2f;
		float b = num / 2f;
		float num4 = Mathf.Max(_cards.Count - 1, 1);
		for (int i = 0; i < _cards.Count; i++)
		{
			CardScript cardScript = _cards[i];
			float t = (float)i / num4;
			cardScript.MoveAnchoredPosition(new Vector2(Mathf.Lerp(a, b, t), 0f));
		}
	}

	private void SyncCards()
	{
		switch (_mode)
		{
		case CardMode.Normal:
		{
			float spacing = -150f;
			LayoutCards(spacing);
			for (int j = 0; j < _cards.Count; j++)
			{
				Transform transform2 = _cards[j].transform;
				transform2.SetSiblingIndex(j);
				RectTransform rectTransform2 = (RectTransform)transform2.transform;
				rectTransform2.pivot = new Vector2(0.5f, 0.5f);
				rectTransform2.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
			break;
		}
		case CardMode.Compressed:
		{
			float spacing = -240f;
			LayoutCards(spacing);
			float num = Mathf.Min((float)_cards.Count * 0.05f, 1f);
			float num2 = Mathf.Max(_cards.Count - 1, 1);
			for (int i = 0; i < _cards.Count; i++)
			{
				Transform transform = _cards[i].transform;
				transform.SetSiblingIndex(i);
				float t = (float)i / num2;
				RectTransform rectTransform = (RectTransform)transform;
				float num3 = Mathf.Lerp(-0.5f, 0.5f, t);
				rectTransform.pivot = new Vector2(0.5f, 0.5f - Mathf.Cos(num3 * (float)Math.PI) * 0.1f * num);
				rectTransform.rotation = Quaternion.Euler(0f, 0f, (0f - num3) * 20f * num);
			}
			break;
		}
		}
		foreach (CardScript card in _cards)
		{
			card.ResetRaycastPadding();
		}
		_hoveredCard?.MoveUp();
		foreach (HandModifier specialModifier in _specialModifiers)
		{
			specialModifier.Apply();
		}
	}

	public HandModifier AddModifer(Action<CardScript> action)
	{
		HandModifier handModifier = new HandModifier(() =>
		{
			foreach (CardScript card in _cards)
			{
				action(card);
			}
		}, this);
		_specialModifiers.Add(handModifier);
		SyncCards();
		return handModifier;
	}

	public HandModifier AddModifer(Action action)
	{
		HandModifier handModifier = new HandModifier(action, this);
		_specialModifiers.Add(handModifier);
		SyncCards();
		return handModifier;
	}

	private void RemoveModifer(HandModifier modifier)
	{
		_specialModifiers.Remove(modifier);
		SyncCards();
	}

	private IEnumerator TakeCardSound(float speed)
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayWithPitch(takeCard, UnityEngine.Random.Range(0.9f, 1f), UnityEngine.Random.Range(0.9f, 1f));
		yield return new WaitForSeconds((takeCard.length + 0.2f) * speed);
	}

	private IEnumerator PlaceCardSound(float speed)
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayWithPitch(placeCard, UnityEngine.Random.Range(0.9f, 1f), UnityEngine.Random.Range(0.9f, 1.1f));
		yield return new WaitForSeconds((placeCard.length + 0.2f) * speed);
	}

	public IEnumerator AddCardEnumerator(CardDeck.Card card, bool visible, float speed = 1f)
	{
		yield return TakeCardSound(speed);
		AddCard(card, visible);
		yield return PlaceCardSound(speed);
	}

	public void AddCard(CardDeck.Card card, bool visible = true)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(cardPrefab, transform, worldPositionStays: false);
		if (cardSourceTransform != null)
		{
			gameObject.transform.position = cardSourceTransform.transform.position;
		}
		gameObject.name = $"{card.Rank} {card.Suit}";
		CardScript component = gameObject.GetComponent<CardScript>();
		component.InternalCard = card;
		component.Visible = visible;
		SyncHoverable(component);
		_cards.Add(component);
		SyncCards();
	}

	public void AddCard(CardScript card)
	{
		card.transform.SetParent(transform, worldPositionStays: true);
		_cards.Add(card);
		_specialModifiers.Clear();
		SyncHoverable(card);
		SyncCards();
	}

	public void GetCardFromOtherHand(HandScript handScript, CardScript card)
	{
		if (!(handScript == this))
		{
			handScript.RemoveCardInner(card, destroy: false);
			AddCard(card);
		}
	}

	public void Shuffle()
	{
		_cards.Shuffle();
		SyncCards();
	}

	public void SetCurrentCardsVisibility(bool value)
	{
		foreach (CardScript card in _cards)
		{
			card.Visible = value;
		}
	}

	public IEnumerator RemoveCardEnumerator(CardScript cardScript, float speed = 1f)
	{
		yield return TakeCardSound(speed);
		RemoveCard(cardScript);
	}

	private void RemoveCardInner(CardScript cardScript, bool destroy)
	{
		_cards.Remove(cardScript);
		if (_hoveredCard == cardScript)
		{
			_hoveredCard = null;
		}
		if (destroy)
		{
			UnityEngine.Object.Destroy(cardScript.gameObject);
		}
		SyncCards();
	}

	public void RemoveCard(CardScript cardScript)
	{
		RemoveCardInner(cardScript, destroy: true);
	}

	public void ClearHand()
	{
		foreach (Transform item in transform)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		_hoveredCard = null;
		_cards.Clear();
		SyncCards();
	}

	private void SyncHoverable(CardScript cardScript)
	{
		if (_hoverable)
		{
			Hoverable orAddComponent = cardScript.gameObject.GetOrAddComponent<Hoverable>();
			orAddComponent.Clear();
			orAddComponent.OnPointerEnter += (PointerEventData data) =>
			{
				CardHovered(cardScript, data);
			};
			orAddComponent.OnPointerExit += (PointerEventData data) =>
			{
				CardHoveredEnd(cardScript, data);
			};
		}
		else
		{
			UnityEngine.Object.Destroy(cardScript.gameObject.GetComponent<Hoverable>());
		}
	}

	private void CardHovered(CardScript card, PointerEventData pointerEventData)
	{
		if (_hoveredCard != card)
		{
			_hoveredCard = card;
			SyncCards();
			OnCardHovered?.Invoke(card);
		}
	}

	private void CardHoveredEnd(CardScript card, PointerEventData pointerEventData)
	{
		if (_hoveredCard == card)
		{
			_hoveredCard = null;
			SyncCards();
		}
	}

	public void PickCard(Action<CardScript> OnPicked, Func<CardScript, bool> pickable = null)
	{
		List<CardScript> cardsToPickFrom = ((pickable == null) ? _cards : _cards.Where(pickable).ToList());
		if (cardsToPickFrom.Count == 0)
		{
			OnPicked(null);
		}
		HandModifier modifer = null;
		if (cardsToPickFrom.Count != _cards.Count)
		{
			modifer = AddModifer(() =>
			{
				foreach (CardScript item in cardsToPickFrom)
				{
					item.MoveUp();
				}
			});
		}
		foreach (CardScript card in cardsToPickFrom)
		{
			card.gameObject.GetOrAddComponent<Clickable>().OnPointerClick += (PointerEventData data) =>
			{
				ClearClickable();
				OnPicked(card);
			};
		}
		void ClearClickable()
		{
			foreach (CardScript item2 in cardsToPickFrom)
			{
				if (item2.gameObject.TryGetComponent<Clickable>(out var component))
				{
					component.Clear();
				}
			}
			modifer?.Dispose();
		}
	}
}
