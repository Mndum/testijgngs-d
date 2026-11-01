public static class ItemManagerUtility
{
	public static bool HasFlagFast(this Item.ItemManager.ItemManagerCallbacks e, Item.ItemManager.ItemManagerCallbacks flag)
	{
		return (e & flag) != 0;
	}
}
