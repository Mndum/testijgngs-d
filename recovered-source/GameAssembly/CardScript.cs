using System.Collections;
using System.Globalization;
using Cards;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
	public enum CardImageSource
	{
		Casino,
		Real
	}

	[SerializeField]
	private Image _image;

	[SerializeField]
	private CardImageSource cardImageSource;

	private Coroutine _moveCoroutine;

	private CardDeck.Card _card;

	private bool _visible = true;

	public RectTransform RectTransform => (RectTransform)transform;

	public bool Visible
	{
		get
		{
			return _visible;
		}
		set
		{
			_visible = value;
			LoadAndSetSprite();
		}
	}

	public CardDeck.Card InternalCard
	{
		get
		{
			return _card;
		}
		set
		{
			_card = value;
			LoadAndSetSprite();
		}
	}

	private IEnumerator MoveCoroutine(Vector2 target)
	{
		RectTransform rt = RectTransform;
		Vector2 start = rt.anchoredPosition;
		if (Mathf.Approximately((target - start).magnitude, 0f))
		{
			rt.anchoredPosition = target;
			_moveCoroutine = null;
			yield break;
		}
		yield return CoroutineUtilities.GenericTween((float t) =>
		{
			t = EasingFunction.EaseOutCubic(0f, 1f, t);
			rt.anchoredPosition = Vector2.Lerp(start, target, t);
		}, 0.5f);
		_moveCoroutine = null;
	}

	public void MoveAnchoredPosition(Vector2 targetAnchoredPosition)
	{
		if (_moveCoroutine != null)
		{
			StopCoroutine(_moveCoroutine);
		}
		_moveCoroutine = StartCoroutine(MoveCoroutine(targetAnchoredPosition));
	}

	private (string path, string name) CardToFileName(CardDeck.Card card)
	{
		if (cardImageSource != CardImageSource.Casino)
		{
			_ = 1;
			if (!_visible)
			{
				return (path: "CardBack", name: "CardBack");
			}
			string text = card.Suit switch
			{
				CardDeck.Suit.Heart => "Hearts", 
				CardDeck.Suit.Diamond => "Tiles", 
				CardDeck.Suit.Club => "Clovers", 
				_ => "Pikes", 
			};
			string text2;
			if (card.Rank > CardDeck.Rank.c10)
			{
				text2 = ((card.Rank != CardDeck.Rank.Ace) ? card.Rank.ToString() : "A");
			}
			else
			{
				int rank = (int)card.Rank;
				text2 = rank.ToString();
			}
			string text3 = text + "_" + text2 + "_white";
			return (path: text3, name: text3);
		}
		if (!_visible)
		{
			return (path: "casinoCards", name: "cardBackBlack");
		}
		return (path: "casinoCards", name: (InternalCard.Suit switch
		{
			CardDeck.Suit.Heart => 1, 
			CardDeck.Suit.Diamond => 2, 
			CardDeck.Suit.Spade => 3, 
			_ => 0, 
		} + CardDeck.RankToNumber(card.Rank) * 4).ToString(CultureInfo.InvariantCulture));
	}

	private void LoadAndSetSprite()
	{
		(string, string) key = CardToFileName(_card);
		if ((cardImageSource switch
		{
			CardImageSource.Casino => R.CardsCasino.RelativePathAndNameToReference, 
			_ => R.CardsMain.RelativePathAndNameToReference, 
		}).TryGetValue(key, out var value))
		{
			_image.sprite = value.Get();
			return;
		}
		Debug.LogError($"Card \"{key.Item1}\" \"{key.Item2}\" not found! ({cardImageSource})");
		_image.sprite = null;
	}

	public void ResetRaycastPadding()
	{
		_image.raycastPadding = new Vector4(0f, 0f, 0f, 0f);
	}

	public void MoveUp()
	{
		float num = 0.1f;
		Vector2 pivot = RectTransform.pivot;
		pivot.y -= num;
		RectTransform.pivot = pivot;
		float num2 = num * RectTransform.sizeDelta.y;
		Vector4 raycastPadding = _image.raycastPadding;
		raycastPadding.y -= num2;
		_image.raycastPadding = raycastPadding;
	}

	public override string ToString()
	{
		return string.Format("{0}({1} Visible:{2})", "CardScript", _card, _visible);
	}
}
