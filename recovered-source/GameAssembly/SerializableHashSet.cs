using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver
{
	[SerializeField]
	private List<T> values = new List<T>();

	public void OnBeforeSerialize()
	{
		if (values == null)
		{
			values = new List<T>();
		}
		values.Clear();
		values.AddRange(this);
	}

	public void OnAfterDeserialize()
	{
		Clear();
		UnionWith(values);
	}
}
