using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards;

public class CardDeck
{
	public enum Rank
	{
		c2 = 2,
		c3 = 3,
		c4 = 4,
		c5 = 5,
		c6 = 6,
		c7 = 7,
		c8 = 8,
		c9 = 9,
		c10 = 10,
		Jack = 20,
		King = 21,
		Queen = 22,
		Ace = 23
	}

	public enum Suit
	{
		Spade,
		Heart,
		Diamond,
		Club
	}

	public class Card
	{
		public Rank Rank;

		public Suit Suit;

		public override string ToString()
		{
			return $"{Rank} {Suit}";
		}
	}

	public static readonly List<Rank> ranks = Enum.GetValues(typeof(Rank)).Cast<Rank>().ToList();

	public static readonly List<Suit> suits = Enum.GetValues(typeof(Suit)).Cast<Suit>().ToList();

	public List<Card> CardsInADeck;

	public static int RankToNumber(Rank rank)
	{
		if (rank < Rank.Jack)
		{
			return (int)(rank - 2);
		}
		return (int)(rank - 20 + 9);
	}

	private CardDeck()
	{
		CardsInADeck = new List<Card>();
	}

	public static CardDeck GenerateFullDeck()
	{
		CardDeck cardDeck = new CardDeck();
		foreach (Suit suit in suits)
		{
			foreach (Rank rank in ranks)
			{
				cardDeck.CardsInADeck.Add(new Card
				{
					Rank = rank,
					Suit = suit
				});
			}
		}
		return cardDeck;
	}

	public static CardDeck GenerateSmallerDeck()
	{
		CardDeck cardDeck = new CardDeck();
		Suit[] array = new Suit[2]
		{
			Suit.Heart,
			Suit.Diamond
		};
		foreach (Suit suit in array)
		{
			foreach (Rank rank in ranks)
			{
				cardDeck.CardsInADeck.Add(new Card
				{
					Rank = rank,
					Suit = suit
				});
			}
		}
		return cardDeck;
	}

	public void Shuffle()
	{
		CardsInADeck = CardsInADeck.OrderBy((Card a) => UnityEngine.Random.value).ToList();
	}

	public Card TakeRandom()
	{
		int index = UnityEngine.Random.Range(0, CardsInADeck.Count);
		Card result = CardsInADeck[index];
		CardsInADeck.RemoveAt(index);
		return result;
	}
}
