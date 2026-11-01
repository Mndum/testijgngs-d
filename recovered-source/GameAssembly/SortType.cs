using System;
using System.Collections.Generic;

public class SortType<T>
{
	public readonly Func<string> NameFunc;

	public readonly IComparer<T> Comparer;

	public readonly bool ForceAllowItemMove;

	public bool ItemMoveAllowed
	{
		get
		{
			if (Comparer != null)
			{
				return ForceAllowItemMove;
			}
			return true;
		}
	}

	public SortType(Func<string> nameFunc, IComparer<T> comparer)
	{
		NameFunc = nameFunc;
		Comparer = comparer;
	}

	public SortType(Func<string> nameFunc, IComparer<T> comparer, bool forceAllowItemMove)
	{
		NameFunc = nameFunc;
		Comparer = comparer;
		ForceAllowItemMove = forceAllowItemMove;
	}
}
