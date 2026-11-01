using System;
using UnityEngine;

[Serializable]
public struct SerializableGuid : ISerializationCallbackReceiver
{
	private Guid guid;

	[SerializeField]
	private string serializedGuid;

	public Guid RealGuid => guid;

	public SerializableGuid(Guid guid)
	{
		this.guid = guid;
		serializedGuid = null;
	}

	public override bool Equals(object obj)
	{
		if (obj is SerializableGuid serializableGuid)
		{
			return guid.Equals(serializableGuid.guid);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return guid.GetHashCode();
	}

	public void OnAfterDeserialize()
	{
		try
		{
			guid = (string.IsNullOrEmpty(serializedGuid) ? Guid.Empty : Guid.Parse(serializedGuid));
		}
		catch
		{
			guid = Guid.Empty;
			Debug.LogWarning("Attempted to parse invalid GUID string '" + serializedGuid + "'. GUID will set to System.Guid.Empty");
		}
	}

	public void OnBeforeSerialize()
	{
		serializedGuid = ((guid == Guid.Empty) ? "" : guid.ToString());
	}

	public override string ToString()
	{
		return guid.ToString();
	}

	public static bool operator ==(SerializableGuid a, SerializableGuid b)
	{
		return a.guid == b.guid;
	}

	public static bool operator !=(SerializableGuid a, SerializableGuid b)
	{
		return a.guid != b.guid;
	}

	public static implicit operator SerializableGuid(Guid guid)
	{
		return new SerializableGuid(guid);
	}

	public static implicit operator Guid(SerializableGuid serializable)
	{
		return serializable.guid;
	}

	public static implicit operator SerializableGuid(string serializedGuid)
	{
		return new SerializableGuid(Guid.Parse(serializedGuid));
	}

	public static implicit operator string(SerializableGuid serializedGuid)
	{
		return serializedGuid.ToString();
	}
}
