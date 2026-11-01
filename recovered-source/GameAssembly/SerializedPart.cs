public abstract class SerializedPart<TKey, TPrefab> : ISerializedPart<TKey, TPrefab>, IKey<TKey>, IWithPrefab<TPrefab>
{
	public TPrefab Prefab { get; protected set; }

	public abstract TKey Key { get; }

	public virtual void InitPrefab(TPrefab prefab)
	{
		Prefab = prefab;
	}
}
