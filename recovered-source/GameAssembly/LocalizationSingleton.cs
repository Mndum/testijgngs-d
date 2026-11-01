using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationSingleton : SingletonMonoBehaviour<LocalizationSingleton>
{
	private static HashSet<ILocaleChangedReceiver> _receivers = new HashSet<ILocaleChangedReceiver>();

	private static HashSet<Action<Locale>> _staticReceivers = new HashSet<Action<Locale>>();

	[field: SerializeField]
	public GameObject MainText { get; private set; }

	[field: SerializeField]
	public TextAsset CommonCharacters { get; private set; }

	public CultureInfo CurrentLanguageCultureInfo { get; private set; }

	public CompareInfo CurrentLanguageCompareInfo { get; private set; }

	private void UpdateCultureInfo(Locale locale)
	{
		CurrentLanguageCultureInfo = locale.Identifier.CultureInfo ?? CultureInfo.InvariantCulture;
		CurrentLanguageCompareInfo = CurrentLanguageCultureInfo.CompareInfo;
	}

	public static void Register(ILocaleChangedReceiver receiver)
	{
		_receivers.Add(receiver);
	}

	public static void Unregister(ILocaleChangedReceiver receiver)
	{
		_receivers.Remove(receiver);
	}

	public static void Register(Action<Locale> receiver)
	{
		_staticReceivers.Add(receiver);
	}

	public static void Unregister(Action<Locale> receiver)
	{
		_staticReceivers.Remove(receiver);
	}

	public void Init()
	{
		UpdateCultureInfo(LocalizationSettings.SelectedLocale);
	}

	public void ReportLocaleChanged()
	{
		Locale selectedLocale = LocalizationSettings.SelectedLocale;
		if (selectedLocale == null)
		{
			return;
		}
		UpdateCultureInfo(selectedLocale);
		List<ILocaleChangedReceiver> list;
		using (MyCollectionPool<ILocaleChangedReceiver>.GetListAndFill(_receivers, out list))
		{
			List<Action<Locale>> list2;
			using (MyCollectionPool<Action<Locale>>.GetListAndFill(_staticReceivers, out list2))
			{
				foreach (ILocaleChangedReceiver item in list)
				{
					item.OnLocaleChanged(selectedLocale);
				}
				foreach (Action<Locale> item2 in list2)
				{
					item2(selectedLocale);
				}
			}
		}
	}
}
