using System;

public sealed class SelectionFactory
{
	public static Selection<TSelectable> CreateInstance<TSelectable>() where TSelectable : ISelectable
	{
		return new Selection<TSelectable>((TSelectable x) => x.GetCount());
	}

	public static Selection<TNonSelectable> CreateInstanceCustom<TNonSelectable>(Func<TNonSelectable, int> countGetter)
	{
		return new Selection<TNonSelectable>(countGetter);
	}

	public static Selection<TNonSelectable> CreateInstanceSingular<TNonSelectable>()
	{
		return new Selection<TNonSelectable>((TNonSelectable x) => 1);
	}
}
