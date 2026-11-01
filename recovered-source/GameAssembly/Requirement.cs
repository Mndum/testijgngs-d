using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class Requirement
{
	protected class StatusChangedAutoReporting : IDisposable
	{
		private Requirement _parent;

		private bool _statusBefore;

		public StatusChangedAutoReporting(Requirement parent)
		{
			_parent = parent;
			_statusBefore = parent.CheckIfDone();
		}

		public void Dispose()
		{
			if (_statusBefore != _parent.CheckIfDone())
			{
				_parent.ReportStatusChanged();
			}
		}
	}

	[field: NonSerialized]
	public CocktractContract BCocktractContract { get; private set; }

	[field: NonSerialized]
	public int b_depth { get; private set; }

	[field: NonSerialized]
	public Requirement b_parent { get; private set; }

	public string ColorString
	{
		get
		{
			bool flag = CheckIfDone();
			bool flag2 = CheckIfPossible();
			if (flag)
			{
				return "green";
			}
			if (flag2)
			{
				return "yellow";
			}
			return "red";
		}
	}

	public static string AlwaysTrueString => LOC.UI.Requirements_AlwaysTrue;

	public virtual void Initialize(CocktractContract cocktractContract, Requirement parent, int depth = 0)
	{
		BCocktractContract = cocktractContract;
		b_depth = depth;
		b_parent = parent;
	}

	public void CheckIfUniqueInNode<T>()
	{
		if (GetAll<T>().Any((T t) => !object.Equals(t, this)))
		{
			Debug.LogError("T cannot be a child of one. But it is! Something's fishy here!!!");
		}
	}

	public List<T> GetAll<T>()
	{
		List<T> list = new List<T>();
		GetAllToList(list);
		return list;
	}

	public virtual void GetAllToList<T>(List<T> aggregate)
	{
		if (this is T item)
		{
			aggregate.Add(item);
		}
	}

	public abstract float GetDifficulty();

	protected void ReportStatusChanged()
	{
		b_parent?.ChildStatusChanged(this);
	}

	protected virtual void ChildStatusChanged(Requirement child)
	{
		b_parent?.ChildStatusChanged(this);
	}

	public virtual bool CheckIfPossible()
	{
		return true;
	}

	public virtual bool CheckIfDone()
	{
		return false;
	}

	public virtual string GenerateDescription()
	{
		return GenerateDescriptionInternal().TMPColorSurround(ColorString);
	}

	protected abstract string GenerateDescriptionInternal();

	public static string GetDepthPrefix(int depth, string prefix = "-")
	{
		if (depth < 0)
		{
			return "";
		}
		return new string(' ', depth * 2 + 3) + prefix;
	}

	public virtual string GenerateDescriptionWithDepth(string prefix = "-")
	{
		return GetDepthPrefix(b_depth, prefix) + GenerateDescription();
	}

	public RequirementContainer ToRequirementContainer()
	{
		return new RequirementContainer(this);
	}
}
