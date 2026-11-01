using System.Collections.Generic;
using System.Linq;

public abstract class PrefabManagerDataOneToOne<TKey, TPrefab, TSerializedPart> : PrefabManagerDataBase<TKey, TPrefab, TSerializedPart> where TPrefab : IKey<TKey> where TSerializedPart : IKey<TKey>, IWithPrefab<TPrefab>
{
	public void EnsureSerializedDataExistsAndHasPrefabs(List<TSerializedPart> serializedList)
	{
		foreach (var (keyValuePair, val) in from _003C_003Eh__TransparentIdentifier0 in _prefabs.GroupJoin(serializedList, (KeyValuePair<TKey, TPrefab> keyValuePair3) =>
			{
				KeyValuePair<TKey, TPrefab> keyValuePair2 = keyValuePair3;
				return keyValuePair2.Key;
			}, (TSerializedPart serializedPart) =>
			{
				TSerializedPart val2 = serializedPart;
				return val2.Key;
			}, (KeyValuePair<TKey, TPrefab> prefab2, IEnumerable<TSerializedPart> gj) => new
			{
				prefab = prefab2,
				gj = gj
			})
			from serializedPartWithNull in _003C_003Eh__TransparentIdentifier0.gj.DefaultIfEmpty()
			select (prefab: _003C_003Eh__TransparentIdentifier0.prefab, serializedPartWithNull: serializedPartWithNull))
		{
			if (val == null)
			{
				serializedList.Add(CreateSerializedPart(keyValuePair.Key, keyValuePair.Value));
			}
			else
			{
				val.InitPrefab(keyValuePair.Value);
			}
		}
		foreach (TSerializedPart serialized in serializedList)
		{
			if (serialized.Prefab == null)
			{
				TPrefab prefab = Get(serialized.Key);
				serialized.InitPrefab(prefab);
			}
		}
	}

	protected abstract TSerializedPart CreateSerializedPart(TKey key, TPrefab prefab);
}
