public static class ItemUtilities
{
	public static bool HasFlagFast(this Item.ItemUIAllowedButtons value, Item.ItemUIAllowedButtons flag)
	{
		return (value & flag) == flag;
	}

	public static bool HasFlagFast(this ItemNameIncludeEnum self, ItemNameIncludeEnum flag)
	{
		return (self & flag) == flag;
	}

	public static bool HasFlagFast(this ItemDescriptionIncludeEnum self, ItemDescriptionIncludeEnum flag)
	{
		return (self & flag) == flag;
	}
}
