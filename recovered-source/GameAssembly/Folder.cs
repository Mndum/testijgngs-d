using System;
using System.Collections.Generic;

public class Folder<T>
{
	public readonly Func<string> NameFunc;

	public readonly Predicate<T> MaskFunc;

	public readonly bool RemainingFolder;

	public readonly bool IgnoreForRemainingFolder;

	public readonly bool HideIfEmpty;

	public static Folder<T> AllFolder = new Folder<T>(() => LOC.UI.Equipment_All);

	public Folder(Func<string> nameFunc, Predicate<T> maskFunc = null, bool hideIfEmpty = false, bool remainingFolder = false, bool ignoreForRemainingFolder = false)
	{
		NameFunc = nameFunc;
		RemainingFolder = remainingFolder;
		MaskFunc = maskFunc;
		HideIfEmpty = hideIfEmpty;
		IgnoreForRemainingFolder = ignoreForRemainingFolder;
	}

	public static List<Folder<T>> GetPooledWithAllFolder()
	{
		List<Folder<T>> list = MyCollectionPool<Folder<T>>.GetList();
		list.Add(AllFolder);
		return list;
	}
}
