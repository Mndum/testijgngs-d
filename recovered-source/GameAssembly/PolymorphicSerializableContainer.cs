using System;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class PolymorphicSerializableContainer<TCont, TObj> : ISerializationCallbackReceiver where TCont : PolymorphicSerializableContainer<TCont, TObj> where TObj : class
{
	[SerializeField]
	private string _serialized;

	[NonSerialized]
	private TObj _deserializedObject;

	public abstract (Type type, string prefix)[] PolymorphicTypes { get; }

	public bool IsNull => _deserializedObject == null;

	public TObj Value => _deserializedObject;

	protected PolymorphicSerializableContainer(TObj deserializedObject)
	{
		_deserializedObject = deserializedObject;
	}

	public void OnBeforeSerialize()
	{
		if (_deserializedObject == null)
		{
			_serialized = null;
			return;
		}
		Type type = _deserializedObject.GetType();
		(Type, string)[] polymorphicTypes = PolymorphicTypes;
		for (int i = 0; i < polymorphicTypes.Length; i++)
		{
			var (type2, text) = polymorphicTypes[i];
			if (type2 == type)
			{
				_serialized = text + JsonUtility.ToJson(_deserializedObject);
				return;
			}
		}
		Debug.LogError($"Object {JsonUtility.ToJson(_deserializedObject)} of type {type}. Cannot be saved to a string. " + "No prefix defined. Possible types \"" + string.Join(", ", PolymorphicTypes.Select(((Type type, string prefix) t) => $"{t.type} Prefix:{t.prefix}")) + "\"");
		_serialized = null;
	}

	public void OnAfterDeserialize()
	{
		if (string.IsNullOrWhiteSpace(_serialized))
		{
			return;
		}
		(Type, string)[] polymorphicTypes = PolymorphicTypes;
		for (int i = 0; i < polymorphicTypes.Length; i++)
		{
			var (type, text) = polymorphicTypes[i];
			if (_serialized.StartsWith(text))
			{
				string json = _serialized.Remove(0, text.Length);
				_deserializedObject = (TObj)JsonUtility.FromJson(json, type);
				return;
			}
		}
		Debug.LogError("Object " + _serialized + ". Cannot be loaded from a string. No prefix found. Possible types \"" + string.Join(", ", PolymorphicTypes.Select(((Type type, string prefix) t) => $"{t.type} Prefix:{t.prefix}")) + "\"");
		_serialized = "";
	}
}
