using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public class StringRepresentationHelper<TItem> : IComparer<TItem>
{
	public interface INameFuncStage
	{
		IAppendNameActionStage WithGetNameFunc(Func<TItem, StringRepresentationHelperEnum, string> getNameFunc);
	}

	public interface IAppendNameActionStage
	{
		ISearchFuncStage WithAppendNameAction(Action<TItem, StringBuilder, StringRepresentationHelperEnum> appendNameAction);

		ISearchFuncStage WithoutAppendNameAction();
	}

	public interface ISearchFuncStage
	{
		IBuildStage WithSearchFunc(Func<TItem, string, bool> searchFuncRaw);

		IBuildStage WithDefaultSearch();
	}

	public interface IBuildStage
	{
		StringRepresentationHelper<TItem> Build();
	}

	private class BuilderStages : INameFuncStage, IAppendNameActionStage, ISearchFuncStage, IBuildStage
	{
		private Func<TItem, StringRepresentationHelperEnum, string> _getNameFunc;

		private Action<TItem, StringBuilder, StringRepresentationHelperEnum> _appendNameAction;

		private Func<TItem, string, bool> _searchFuncRaw;

		public IAppendNameActionStage WithGetNameFunc(Func<TItem, StringRepresentationHelperEnum, string> getNameFunc)
		{
			_getNameFunc = getNameFunc ?? throw new ArgumentNullException("getNameFunc");
			return this;
		}

		public ISearchFuncStage WithAppendNameAction(Action<TItem, StringBuilder, StringRepresentationHelperEnum> appendNameAction)
		{
			_appendNameAction = appendNameAction;
			return this;
		}

		public ISearchFuncStage WithoutAppendNameAction()
		{
			_appendNameAction = null;
			return this;
		}

		public IBuildStage WithSearchFunc(Func<TItem, string, bool> searchFuncRaw)
		{
			_searchFuncRaw = searchFuncRaw;
			return this;
		}

		public IBuildStage WithDefaultSearch()
		{
			_searchFuncRaw = null;
			return this;
		}

		public StringRepresentationHelper<TItem> Build()
		{
			return new StringRepresentationHelper<TItem>(_getNameFunc, _appendNameAction, _searchFuncRaw);
		}
	}

	private readonly Func<TItem, StringRepresentationHelperEnum, string> _getNameFunc;

	private readonly Action<TItem, StringBuilder, StringRepresentationHelperEnum> _appendNameAction;

	private readonly Func<TItem, string, bool> _searchFuncRaw;

	private StringRepresentationHelper(Func<TItem, StringRepresentationHelperEnum, string> getNameFunc, Action<TItem, StringBuilder, StringRepresentationHelperEnum> appendNameAction, Func<TItem, string, bool> searchFuncRaw)
	{
		_getNameFunc = getNameFunc ?? throw new ArgumentNullException("getNameFunc");
		_appendNameAction = appendNameAction;
		_searchFuncRaw = searchFuncRaw;
	}

	public string GetNameOfItem(TItem item, StringRepresentationHelperEnum stringRepresentationHelperEnum = StringRepresentationHelperEnum.Full)
	{
		return _getNameFunc(item, stringRepresentationHelperEnum);
	}

	public void AppendNameOfItem(TItem item, StringBuilder sb, StringRepresentationHelperEnum stringRepresentationHelperEnum = StringRepresentationHelperEnum.Full)
	{
		if (_appendNameAction != null)
		{
			_appendNameAction(item, sb, stringRepresentationHelperEnum);
		}
		else
		{
			sb.Append(GetNameOfItem(item, stringRepresentationHelperEnum));
		}
	}

	public bool IsMatch(TItem item, string searchTerm)
	{
		if (string.IsNullOrEmpty(searchTerm))
		{
			return true;
		}
		if (_searchFuncRaw == null)
		{
			return SingletonMonoBehaviour<LocalizationSingleton>.Instance.CurrentLanguageCompareInfo.IndexOf(GetNameOfItem(item, StringRepresentationHelperEnum.Short), searchTerm, CompareOptions.IgnoreCase) >= 0;
		}
		return _searchFuncRaw(item, searchTerm);
	}

	public int Compare(TItem a, TItem b)
	{
		string nameOfItem = GetNameOfItem(a, StringRepresentationHelperEnum.Short);
		string nameOfItem2 = GetNameOfItem(b, StringRepresentationHelperEnum.Short);
		return SingletonMonoBehaviour<LocalizationSingleton>.Instance.CurrentLanguageCompareInfo.Compare(nameOfItem, nameOfItem2, CompareOptions.IgnoreCase);
	}

	public static StringRepresentationHelper<TItem> CreateBasic(Func<TItem, StringRepresentationHelperEnum, string> toString)
	{
		return new StringRepresentationHelper<TItem>(toString, null, null);
	}

	public static INameFuncStage Builder()
	{
		return new BuilderStages();
	}
}
