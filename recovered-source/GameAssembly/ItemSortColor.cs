using UnityEngine;

public class ItemSortColor : ItemComparer
{
	public static readonly ItemSortColor Comparer = new ItemSortColor();

	private ItemSortColor()
	{
	}

	public override int CompareNotNull(Item x, Item y)
	{
		return CompareByColor(x, y);
	}

	public static int CompareByColor(Item x, Item y)
	{
		Color mainColor = x.GetMainColor();
		Color mainColor2 = y.GetMainColor();
		int num = y.GetItemPrefab().HasColor.CompareTo(x.GetItemPrefab().HasColor);
		if (num != 0)
		{
			return num;
		}
		Color.RGBToHSV(mainColor, out var H, out var S, out var V);
		Color.RGBToHSV(mainColor2, out var H2, out V, out S);
		num = H2.CompareTo(H);
		if (num != 0)
		{
			return num;
		}
		return (mainColor2.r * 3f + mainColor2.g * 2f + mainColor2.b * 1f).CompareTo(mainColor.r * 3f + mainColor.g * 2f + mainColor.b * 1f);
	}
}
