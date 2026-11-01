using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimpleLocalizedString : LuaUtilities.ILuaTransferable
{
	[SerializeField]
	private LOS.TupleReference Id;

	public string ReplacementValue;

	public LOS.TupleReference Reference => Id;

	public bool IsReferenceEmpty => Reference.IsNullOrEmpty();

	public string Value => GetValue();

	private SimpleLocalizedString(string replacement)
	{
		ReplacementValue = replacement;
	}

	private SimpleLocalizedString(LOS.TupleReference id)
	{
		Id = id;
	}

	public string GetValue(IList<object> arguments = null)
	{
		if (!IsReferenceEmpty)
		{
			return Reference.Get(arguments);
		}
		return ReplacementValue;
	}

	public string GetValueProjectLocale(IList<object> arguments = null)
	{
		if (!IsReferenceEmpty)
		{
			return Reference.GetWithProjectLocale(arguments);
		}
		return ReplacementValue;
	}

	public string ToBakedArgument()
	{
		SimpleLocalizedString simpleLocalizedString = new SimpleLocalizedString(Reference.Clone());
		simpleLocalizedString.Reference.MoveToStringTableEntryRepresentation();
		return JsonUtility.ToJson(simpleLocalizedString);
	}

	public static SimpleLocalizedString CreateInstance(LOS.TupleReference id)
	{
		return new SimpleLocalizedString(id);
	}

	public static SimpleLocalizedString CreateInstance(string tableReference, string tableEntryReference)
	{
		return new SimpleLocalizedString(new LOS.TupleReference(tableReference, tableEntryReference));
	}

	[UsedInLua]
	public static SimpleLocalizedString CreateInstanceReplacement(string replacement)
	{
		return new SimpleLocalizedString(replacement);
	}

	public static implicit operator SimpleLocalizedString(LOS.TupleReference b)
	{
		return new SimpleLocalizedString(b);
	}

	public override string ToString()
	{
		if (IsReferenceEmpty)
		{
			return "SimpleLocalizedString: Replacement value \"" + ReplacementValue + "\"";
		}
		return string.Format("{0}: {1}", "SimpleLocalizedString", Reference);
	}

	public string ToLuaString()
	{
		return "SimpleLocalizedString.CreateInstanceReplacement(" + Value.ToLuaString() + ")";
	}
}
