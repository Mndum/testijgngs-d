using System.Collections.Generic;

public abstract class ItemComparer : IComparer<Item>
{
	public int Compare(Item x, Item y)
	{
		if (x == null && y == null)
		{
			return 0;
		}
		if (x == null)
		{
			return -1;
		}
		if (y == null)
		{
			return 1;
		}
		return CompareNotNull(x, y);
	}

	public abstract int CompareNotNull(Item x, Item y);
}
