public static class Live2DPrefabHolderExtensions
{
	public static bool TryGetController<T>(this Live2DPrefabHolder.Live2DControllerUsage<T> usage, out T controller) where T : Live2DController
	{
		controller = null;
		if (usage == null || !usage.IsValid)
		{
			return false;
		}
		controller = usage.Controller;
		return true;
	}
}
