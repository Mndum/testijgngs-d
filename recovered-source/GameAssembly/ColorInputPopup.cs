using System;
using System.Collections.Generic;
using HSVPicker;
using TMPro;
using UnityEngine;

public class ColorInputPopup : PrefabPopup<ColorInputPopup>
{
	[SerializeField]
	private TextMeshProUGUI titleTmp;

	[SerializeField]
	private TextMeshProUGUI textTmp;

	[SerializeField]
	private ColorPicker colorPicker;

	[SerializeField]
	private ColorPresets colorPresets;

	[SerializeField]
	private AdvancedItemList colorPaletteAdvancedItemList;

	[SerializeField]
	private AdvancedItemList colorsAdvancedItemList;

	[SerializeField]
	private PooledPrefabReference<ColorInputPopupEntry> colorInputPrefab;

	[SerializeField]
	private ButtonList buttonList;

	private AdvancedItemList.AdvancedItemListAdapter<ColorPaletteManager.ColorPalette, ColorInputPopupEntry> _palettesAdvancedItemList;

	private AdvancedItemList.AdvancedItemListAdapter<ColorPaletteManager.ColorPaletteEntry, ColorInputPopupEntry> _colorsAdvancedItemList;

	public void Open(string title, string text, Action<Color> yesaction, Action<Color> noaction, Color startColor)
	{
		titleTmp.text = title;
		textTmp.text = text;
		List<PopupChoice> list = new List<PopupChoice>();
		list.Add(new PopupChoice
		{
			Text = LOC.UI.UI_Apply,
			Action = () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.SetPresetColors(colorPresets.GetColorPresets());
				yesaction?.Invoke(colorPicker.CurrentColor);
			}
		});
		list.Add(new PopupChoice
		{
			Text = LOC.UI.UI_Cancel,
			Action = () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.SetPresetColors(colorPresets.GetColorPresets());
				noaction?.Invoke(colorPicker.CurrentColor);
			}
		});
		buttonList.InitializeFromPopupChoices(list, CloseFromUIOverlay);
		colorPresets.ChangeColorPresets(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.GetPresetColors());
		colorPicker.CurrentColor = startColor;
		_palettesAdvancedItemList = colorPaletteAdvancedItemList.SetAdapter(DataSourceUtils.CreateMultipleUse(() => ColorPaletteManager.AllVisiblePalettes), colorInputPrefab.GetPool(), (ColorPaletteManager.ColorPalette data, ColorInputPopupEntry display) =>
		{
			display.SetPalette(data);
			display.SetFocusOnClick();
		});
		_colorsAdvancedItemList = colorsAdvancedItemList.SetAdapter(DataSourceUtils.CreateMultipleUse(() => _palettesAdvancedItemList.TryGetCurrentlyFocusedItem(out var item) ? item.Entries : Array.Empty<ColorPaletteManager.ColorPaletteEntry>()), colorInputPrefab.GetPool(), (ColorPaletteManager.ColorPaletteEntry data, ColorInputPopupEntry display) =>
		{
			display.SetColor(data);
			display.SetDefaultButtonAction(() =>
			{
				colorPicker.CurrentColor = data.Color;
			});
		});
		_colorsAdvancedItemList.SortComparer = Comparer<ColorPaletteManager.ColorPaletteEntry>.Create((ColorPaletteManager.ColorPaletteEntry a, ColorPaletteManager.ColorPaletteEntry b) =>
		{
			Color.RGBToHSV(a.Color, out var H, out var S, out var V);
			Color.RGBToHSV(b.Color, out var H2, out var S2, out var V2);
			int num = H.CompareTo(H2);
			if (num != 0)
			{
				return num;
			}
			int num2 = S.CompareTo(S2);
			if (num2 != 0)
			{
				return num2;
			}
			int num3 = V.CompareTo(V2);
			return (num3 != 0) ? num3 : 0;
		});
		Open();
		_palettesAdvancedItemList.OnFocusedItemChanged = (ColorPaletteManager.ColorPalette data, int index) =>
		{
			_colorsAdvancedItemList.UpdateAll();
		};
		_palettesAdvancedItemList.UpdateAll();
		_palettesAdvancedItemList.Focus(0);
	}
}
