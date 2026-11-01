using System;
using System.Collections.ObjectModel;

public class ProjectedKeyCollection<TKey, TItem> : KeyedCollection<TKey, TItem>
{
	internal readonly Func<TItem, TKey> keySelector;

	public ProjectedKeyCollection(Func<TItem, TKey> keySelector)
	{
		this.keySelector = keySelector;
	}

	protected override TKey GetKeyForItem(TItem item)
	{
		return keySelector(item);
	}

	public new bool TryGetValue(TKey key, out TItem item)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		if (Dictionary != null)
		{
			return Dictionary.TryGetValue(key, out item);
		}
		foreach (TItem item2 in Items)
		{
			TKey keyForItem = GetKeyForItem(item2);
			if (keyForItem != null && Comparer.Equals(key, keyForItem))
			{
				item = item2;
				return true;
			}
		}
		item = default;
		return false;
	}
}
