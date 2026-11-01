public abstract class PrefabPart<TKey> : IKey<TKey>
{
	public TKey Key { get; }

	protected PrefabPart(TKey key)
	{
		Key = key;
	}
}
