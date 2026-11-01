using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

public static class ItemStatsSheep
{
	public static void ItemStatsCheatsButtonClicked(ItemStats _itemStats)
	{
		List<PopupChoice> list = new List<PopupChoice>();
		int num = (int)Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("IiILJis8GQ4jci8dJUMbFV5qcxY8Rm8ibyMeBQ==", 5), 24), 4)).GetMethod(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("M2oyHwgMG3UQCmdnZWkxTDsBQzE1Xw5n", 11), 17), 20), BindingFlags.Static | BindingFlags.Public).Invoke(null, null);
		if ((!((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("JRx4Qh4lF24mPkNvZmgATQAiX0Q5fgMgYXswag==", 11), 0), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("f0lWT0RFdlhMXnVKd1FqVg==", 2), 14), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("VlBbXQ==", 22), 12)) && !((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("ZYqTaZRcZWlwgopjf3+CZ4dpSVeDR4x1SERgUA==", 13), 15), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("En9zfBUZMCB5QQETAG8pCw==", 13), 1), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("RjAfCw==", 18), 17))) || ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("W5CHc1Jyb21fWGl0gkpYaFlgPl1WQjxeeG1aWg==", 3), 9), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("NRsAQgEgFxw9AHtAUQMPfQ==", 11), 3), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.Length < 10)
		{
			return;
		}
		if (_itemStats.CurrentItem != null)
		{
			if (OptionsStatic.DProofPref.Value != DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("LWsZDgwbJCcYNDV6Iww1QA==", 3), 23), 1))
			{
				return;
			}
			ItemPrefab prefab = _itemStats.CurrentItem.GetItemPrefab();
			if (_itemStats.CurrentItem.GameId.IsModded && num == 30)
			{
				list.Add(new PopupChoice(LOC.UI.Mods_OpenModWorkingDirectory, () =>
				{
					Mod modByGuid = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetModByGuid(_itemStats.CurrentItem.GameId.Guid);
					if (modByGuid != null)
					{
						UiOverlay.Instance.OpenNativeFolderPath(SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetWorkingPath(modByGuid));
					}
					else
					{
						Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("d3d5YHxAdVxyQThI", 6), 20)).GetProperty(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("K1kJHiRiOgMqQQFv", 9), 19)).GetValue(null)
							.GetType()
							.GetMethod(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("TUxPTnI7dFUzO0dJ", 5), 5))
							.Invoke(Type.GetType(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("Iw05HBM/ThVmHGtz", 15), 3)).GetProperty(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("Fl5eQhUtexgvUgxQ", 20), 2)).GetValue(null), new object[3]
							{
								LOC.UI.UI_Error,
								LOC.UI.Mods_RelatedModNotFound,
								null
							});
					}
				}));
				if (prefab.HasAnyTextures)
				{
					list.Add(new PopupChoice(LOC.UI.Mods_ReloadTextures, () =>
					{
						StringBuilder stringBuilder;
						using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
						{
							prefab.ReloadTextures(stringBuilder);
							SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.ResetAllTextures();
							SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.UpdateAllTextures();
							_itemStats.ReportCurrentItemInternalChange(_itemStats.CurrentItem);
							UiOverlay.Instance.OkPopup(LOC.UI.UI_Done, stringBuilder.ToString());
						}
					}));
				}
			}
			if (num >= 20)
			{
				if (prefab.FishData != null)
				{
					list.Add(new PopupChoice(LOC.UI.Cheat_ChangeFishWeight, () =>
					{
						ChangeFishWeightPopup(_itemStats, _itemStats.CurrentItem);
					}));
				}
				if (prefab.AllowedModifiersData != null)
				{
					list.Add(new PopupChoice(LOC.UI.Cheat_RandomiseModifiers, () =>
					{
						GameVariables.Current.MarkAsCheater();
						prefab.AllowedModifiersData.SetRandomModifiers(_itemStats.CurrentItem, tryNoEmpty: true);
						_itemStats.ReportCurrentItemInternalChange(_itemStats.CurrentItem);
					}));
					list.Add(new PopupChoice(LOC.UI.Cheat_RemoveModifiers, () =>
					{
						GameVariables.Current.MarkAsCheater();
						_itemStats.CurrentItem.ClearItemModifiers();
						_itemStats.ReportCurrentItemInternalChange(_itemStats.CurrentItem);
					}));
				}
				if (_itemStats.CurrentItem.HasColor)
				{
					list.Add(new PopupChoice(LOC.UI.UI_ChangeColor, () =>
					{
						GameVariables.Current.MarkAsCheater();
						_itemStats.ItemColorPopup(_itemStats.CurrentItem);
					}));
				}
				if (_itemStats.CurrentItem.GetItemPrefab().HasQuality)
				{
					list.Add(new PopupChoice(LOC.UI.UI_ChangeQuality, () =>
					{
						GameVariables.Current.MarkAsCheater();
						ItemQualityPopup(_itemStats, _itemStats.CurrentItem);
					}));
				}
			}
		}
		if (num >= 20)
		{
			list.Add(new PopupChoice(LOC.UI.AddItem, UiOverlay.Instance.OpenAddItemCheat));
		}
		if ((((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("YJVnelaGX2hgkWt9golZZWlfeXVheoRNTT+DTA==", 8), 9), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("RkdjcW5UPFxIZ09oU1RtWA==", 0), 22), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("KzYMfA==", 10), 13)) || ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("DmtgNTBiBytpZWxyeRc7JnAYCGIEfAF/PiEteg==", 24), 3), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("RHt+dGaDVWBBRGNrUTg2Ww==", 5), 15), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("U25Ffg==", 19), 20))) && ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("Ehpdb3EJEHs4IgIBL1FxFVFpChMbfRE+QgIffQ==", 19), 21), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("mVZThFeFcGtfY4JbfW5XVA==", 19), 6), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.Length >= 10)
		{
			UiOverlay.Instance.OpenContextMenuPopup(list);
		}
	}

	private static void ChangeFishWeightPopup(ItemStats _itemStats, Item item)
	{
		FishItemPrefabData fishData = item.GetItemPrefab().FishData;
		if (fishData == null || (!((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("WEpecUJ9R2peXUFxgVR8ZVhrYFtVTF9cd3IyVw==", 0), 11), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("Hi5EbBlECTsCew4Vbx1cNw==", 9), 0), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B("QWpDYw==", 1), 18)) && !((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("dzM+fxcnUFg6QVgYe2kgbg5xFSNgAgwuCChURQ==", 23), 4), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("PjUQeHIkGDh9IRB9dwBqKg==", 10), 4), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.StartsWith(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.B("WH1fhA==", 24), 21))) || ((PlayerPrefsStringKey)Type.GetType(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A("FCEsGw8IQw4bCx8yBW5FWQIwRxsyV1NcbgMLCg==", 8), 2), 16)).GetField(DUPADUPADUPADUPADUPA.B(DUPADUPADUPADUPADUPA.A(DUPADUPADUPADUPADUPA.A("PxkWKSM2C1skIwhILRghYQ==", 3), 8), 11), BindingFlags.Static | BindingFlags.Public).GetValue(null)).Value.Length < 10)
		{
			return;
		}
		List<PopupChoice> list = new List<PopupChoice>();
		float[] array = new float[5] { 0.1f, 0.25f, 0.5f, 0.75f, 1f };
		foreach (float percentile in array)
		{
			float weight = fishData.DetermineWeightFromPercentile(percentile);
			list.Add(new PopupChoice(weight.ToString(CultureInfo.InvariantCulture), () =>
			{
				GameVariables.Current.MarkAsCheater();
				item.FishWeight = weight;
				_itemStats.ReportCurrentItemInternalChange(item);
			}));
		}
		UiOverlay.Instance.OpenContextMenuPopup(list);
	}

	private static void ItemQualityPopup(ItemStats _itemStats, Item item)
	{
		UiOverlay.Instance.OpenContextMenuPopup(Item.QualityDescriptors.Select((Item.QualityDescriptor t) => new PopupChoice
		{
			Text = t.SimpleLocalizedString.Value,
			Action = () =>
			{
				item.SetQuality(t.MinQuality);
				_itemStats.ReportCurrentItemInternalChange(item);
			}
		}).ToList());
	}
}
