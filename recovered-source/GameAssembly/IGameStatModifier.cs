using System.Collections.Generic;

public interface IGameStatModifier
{
	private sealed class OrderRelationalComparer : IComparer<IGameStatModifier>
	{
		public int Compare(IGameStatModifier x, IGameStatModifier y)
		{
			if (x == y)
			{
				return 0;
			}
			if (y == null)
			{
				return 1;
			}
			return x?.Order.CompareTo(y.Order) ?? (-1);
		}
	}

	int Order { get; }

	bool IsValid { get; }

	static IComparer<IGameStatModifier> OrderComparer { get; }

	void ApplyToGameStats(GameVariables gameVariables, GameStats gameStats);

	static IGameStatModifier()
	{
		OrderComparer = new OrderRelationalComparer();
	}
}
