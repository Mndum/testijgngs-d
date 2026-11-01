using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SimpleLocalizedStringWithBakedArguments
{
	public LOS.TupleReference Id;

	public string ReplacementValue;

	[SerializeField]
	private List<string> arguments;

	private List<object> _preParsedSpecialValues;

	public List<string> Arguments
	{
		get
		{
			return arguments;
		}
		set
		{
			_preParsedSpecialValues = null;
			arguments = value;
		}
	}

	public List<object> PreParsedSpecialValues => _preParsedSpecialValues ?? (_preParsedSpecialValues = LOS.PreparseSerializedStringArgumentsForLocalization(arguments));

	public string Value
	{
		get
		{
			if (!Id.IsNullOrEmpty())
			{
				return Id.Get(PreParsedSpecialValues);
			}
			return ReplacementValue;
		}
	}

	public bool IsEmpty
	{
		get
		{
			if (Id.IsNullOrEmpty())
			{
				return string.IsNullOrEmpty(ReplacementValue);
			}
			return false;
		}
	}

	public SimpleLocalizedStringWithBakedArguments(string tableReference, string tableEntryReference, params string[] arguments)
		: this(new LOS.TupleReference(tableReference, tableEntryReference), arguments.ToList())
	{
	}

	public SimpleLocalizedStringWithBakedArguments(string tableReference, string tableEntryReference, List<string> arguments = null)
		: this(new LOS.TupleReference(tableReference, tableEntryReference), arguments)
	{
	}

	public static SimpleLocalizedStringWithBakedArguments CreateInstance(string tableReference, string tableEntryReference, List<string> arguments = null)
	{
		return new SimpleLocalizedStringWithBakedArguments(tableReference, tableEntryReference, arguments);
	}

	public SimpleLocalizedStringWithBakedArguments(string replacement)
	{
		ReplacementValue = replacement;
	}

	public static SimpleLocalizedStringWithBakedArguments CreateInstance(string replacement)
	{
		return new SimpleLocalizedStringWithBakedArguments(replacement);
	}

	public SimpleLocalizedStringWithBakedArguments(LOS.TupleReference id, List<string> arguments = null)
	{
		Id = id;
		this.arguments = arguments;
	}

	public SimpleLocalizedStringWithBakedArguments(LOS.TupleReference id, params string[] arguments)
	{
		Id = id;
		this.arguments = arguments.ToList();
	}

	public static SimpleLocalizedStringWithBakedArguments CreateInstance(LOS.TupleReference id, List<string> arguments = null)
	{
		return new SimpleLocalizedStringWithBakedArguments(id, arguments);
	}

	public static SimpleLocalizedStringWithBakedArguments CreateInstance(LOS.TupleReference id, params string[] arguments)
	{
		return new SimpleLocalizedStringWithBakedArguments(id, arguments.ToList());
	}
}
