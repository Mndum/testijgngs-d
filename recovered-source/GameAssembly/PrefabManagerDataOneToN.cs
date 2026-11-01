using System.Collections.Generic;

public abstract class PrefabManagerDataOneToN<TKey, TPrefab, TSerializedPart> : PrefabManagerDataBase<TKey, TPrefab, TSerializedPart> where TPrefab : IKey<TKey> where TSerializedPart : IKey<TKey>, IWithPrefab<TPrefab>
{
	public void FillWithPrefabs(IEnumerable<TSerializedPart> serializedList)
	{
		foreach (TSerializedPart serialized in serializedList)
		{
			TPrefab prefab = Get(serialized.Key);
			serialized.InitPrefab(prefab);
		}
	}

	public void FillWithPrefab(TSerializedPart serializedPart)
	{
		TPrefab prefab = Get(serializedPart.Key);
		serializedPart.InitPrefab(prefab);
	}
}
