using System;
using UnityEngine;

[Serializable]
public class BetterObjectValue
{
	public string typeName;

	public string value;

	public UnityEngine.Object objectValue;

	public object GetValue()
	{
		if (BetterInvokeMethodHelper.IsUnityObject(typeName))
		{
			return objectValue;
		}
		if (BetterInvokeMethodHelper.TryFromString(typeName, value, out var o))
		{
			return o;
		}
		Debug.LogError("Failed parsing \"" + value + "\" as " + typeName + "!");
		return null;
	}
}
