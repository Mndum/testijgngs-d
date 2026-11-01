using System;
using UnityEngine;

[Serializable]
public struct LazySerializableContainer<TObj> : ISerializationCallbackReceiver where TObj : class
{
	[SerializeField]
	private string s;

	[NonSerialized]
	private TObj _deserializedObject;

	[NonSerialized]
	private bool _sourceOfTruthIsDeserialized;

	public TObj Value
	{
		get
		{
			return GetValue();
		}
		set
		{
			SetValue(value);
		}
	}

	public LazySerializableContainer(TObj deserializedObject)
	{
		this = default;
		SetValue(deserializedObject);
	}

	private void EnsureDeserialized()
	{
		if (!_sourceOfTruthIsDeserialized)
		{
			if (string.IsNullOrEmpty(s))
			{
				_deserializedObject = null;
			}
			else
			{
				_deserializedObject = JsonUtility.FromJson<TObj>(s);
			}
			_sourceOfTruthIsDeserialized = true;
		}
	}

	public void SetValue(TObj newValue)
	{
		_deserializedObject = newValue;
		s = "";
		_sourceOfTruthIsDeserialized = true;
	}

	public TObj GetValue()
	{
		EnsureDeserialized();
		return _deserializedObject;
	}

	public void OnBeforeSerialize()
	{
		if (_sourceOfTruthIsDeserialized)
		{
			s = ((_deserializedObject == null) ? "" : JsonUtility.ToJson(_deserializedObject));
		}
	}

	public void OnAfterDeserialize()
	{
		_sourceOfTruthIsDeserialized = false;
	}

	public static implicit operator LazySerializableContainer<TObj>(TObj obj)
	{
		return new LazySerializableContainer<TObj>(obj);
	}

	public static implicit operator TObj(LazySerializableContainer<TObj> container)
	{
		return container.GetValue();
	}
}
