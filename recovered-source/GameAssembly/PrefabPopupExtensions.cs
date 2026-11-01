using IC.AutomaticReference;

public static class PrefabPopupExtensions
{
	public static T InstantiatePrefab<T>(this SerializableAutomaticReference<T> x) where T : PrefabPopup<T>
	{
		return x.Get().InstantiatePrefab();
	}
}
