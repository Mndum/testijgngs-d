using System;
using System.Text;
using HSVPicker;
using TMPro;
using UnityEngine;

public class ItemColorSlotPicker : MonoBehaviour, IPooledPrefab
{
	[SerializeField]
	private TextMeshProUGUI itemName;

	[SerializeField]
	private TextMeshProUGUI endInfo;

	[SerializeField]
	private ColorPicker colorPicker;

	[SerializeField]
	private ColorPresets colorPresets;

	[SerializeField]
	private CanvasGroup canvasGroup;

	private Item _item;

	private ColorSlot _colorSlot;

	private int _index = -1;

	public Color CurrentColor => colorPicker.CurrentColor;

	public GameObject SourcePrefab { get; set; }

	public bool IsCurrent((Item, ColorSlot) tuple)
	{
		if (_item == tuple.Item1)
		{
			return _colorSlot == tuple.Item2;
		}
		return false;
	}

	public void Awake()
	{
		PullPresetsFromGameVariables();
	}

	public void ForceChangeColor(Color color)
	{
		bool colorChangeAllowed = colorPicker.ColorChangeAllowed;
		colorPicker.ColorChangeAllowed = true;
		colorPicker.CurrentColor = color;
		colorPicker.ColorChangeAllowed = colorChangeAllowed;
	}

	public bool TryGetValidChangeableItemSlot(out Item item, out int index)
	{
		return TryGetValidChangeableItemSlot(out item, out index, OptionsStatic.GetCheatTier() >= CheatTierEnum.AdvancedCheats);
	}

	private bool TryGetValidChangeableItemSlot(out Item item, out int index, bool cheatsEnabled)
	{
		item = _item;
		if (_item != null && _index >= 0 && _item.GetItemPrefab().ColorSlots.TryGetValue(_index, out var _) && (_item.CanChangeColor | cheatsEnabled))
		{
			index = _index;
			return true;
		}
		index = -1;
		return false;
	}

	public void SetItem(Item item, ColorSlot colorSlot)
	{
		_item = item;
		_colorSlot = colorSlot;
		if (item != null)
		{
			ItemPrefab itemPrefab = item.GetItemPrefab();
			ColorSlot[] colorSlots = itemPrefab.ColorSlots;
			_index = Array.IndexOf(colorSlots, colorSlot);
			if (_index >= 0)
			{
				canvasGroup.interactable = true;
				bool flag = OptionsStatic.GetCheatTier() >= CheatTierEnum.AdvancedCheats;
				bool canChangeColor = itemPrefab.CanChangeColor;
				bool flag2 = itemPrefab.CanChangeColor | flag;
				Color color = item.GetColor(_index);
				colorPicker.ColorChangeAllowed = true;
				colorPicker.CurrentColor = color;
				StringBuilder stringBuilder;
				using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
				{
					item.GetNameStringBuilder(stringBuilder, ItemNameIncludeEnum.DefaultNoColorNoFavourite);
					stringBuilder.AppendLine();
					stringBuilder.Append(colorSlot.Name);
					itemName.SetText(stringBuilder);
					if (flag2)
					{
						if (!canChangeColor)
						{
							endInfo.gameObject.SetActive(value: true);
							endInfo.color = Color.green;
							endInfo.SetText(LOC.UI.CanChangeColorThanksToCheats);
						}
						else
						{
							endInfo.gameObject.SetActive(value: false);
						}
					}
					else
					{
						colorPicker.ColorChangeAllowed = false;
						endInfo.gameObject.SetActive(value: true);
						endInfo.color = Color.red;
						endInfo.SetText(LOC.UI.CantChangeColor);
					}
					return;
				}
			}
		}
		canvasGroup.interactable = false;
		itemName.SetText(LOC.UI.Equipment_NoItemSelected);
		endInfo.gameObject.SetActive(value: false);
	}

	public void PullPresetsFromGameVariables()
	{
		if (GameVariables.Current != null)
		{
			colorPresets.ChangeColorPresets(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.GetPresetColors());
		}
	}

	public void PushPresetsToGameVariables()
	{
		GameVariables.Current?.SetPresetColors(colorPresets.GetColorPresets());
	}

	public void AfterReturnedToPool()
	{
		_item = null;
		_colorSlot = null;
		_index = -1;
		PushPresetsToGameVariables();
		SetItem(null, null);
	}

	public void BeforeGetFromPool()
	{
	}
}
