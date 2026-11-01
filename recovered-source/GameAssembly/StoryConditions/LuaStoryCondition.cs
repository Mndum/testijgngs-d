using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace StoryConditions;

public sealed class LuaStoryCondition : StoryCondition, IEquatable<LuaStoryCondition>
{
	private Closure _closure;

	public readonly string ConditionCode;

	public bool ValidClosure => _closure != null;

	public LuaStoryCondition(string conditionCode, bool negate)
		: base(negate)
	{
		ConditionCode = conditionCode;
	}

	public void GenerateClosureUnsafe()
	{
		_closure = ModsSingleton.LuaEnvironment.LoadLuaFunction("return not not (" + ConditionCode + ")", null);
	}

	public void GenerateClosure()
	{
		if (_closure == null)
		{
			try
			{
				GenerateClosureUnsafe();
			}
			catch (Exception message)
			{
				_closure = null;
				Debug.LogError(message);
			}
			if (_closure == null)
			{
				Debug.LogError("Error generating closure code: \n" + ConditionCode);
			}
		}
	}

	protected override bool InnerConditionMet()
	{
		GenerateClosure();
		if (_closure != null)
		{
			return _closure.Call().CastToBool();
		}
		return false;
	}

	public override string Explain()
	{
		if (Negate)
		{
			return "not lua\"" + ConditionCode + "\"";
		}
		return "lua\"" + ConditionCode + "\"";
	}

	public override string GenerateCode()
	{
		return "StoryCondition.Lua(@\"" + ConditionCode.Replace("\"", "\"\"") + "\"" + GenerateCodeNegationParameter() + ")";
	}

	public bool Equals(LuaStoryCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Equals((StoryCondition)other))
		{
			return ConditionCode == other.ConditionCode;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is LuaStoryCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, ConditionCode);
	}
}
