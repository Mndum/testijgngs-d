using System;
using System.Text;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemModifiableVariableDisplay : AdvancedItemListPrefab
{
	[SerializeField]
	private TextMeshProUGUI infoText;

	[SerializeField]
	private Button mainButton;

	[SerializeField]
	private Toggle toggle;

	[SerializeField]
	private Scrollbar slider;

	[SerializeField]
	private Button defaultButton;

	[CanBeNull]
	private Item _currentItem;

	[CanBeNull]
	private ItemModifiableVariable _currentVariable;

	public override bool HasDefaultButton => true;

	public override Button GetDefaultButton()
	{
		return mainButton;
	}

	public void Start()
	{
		slider.onValueChanged.SetListener(SliderValueChanged);
		toggle.onValueChanged.SetListener(ToggleValueChanged);
		defaultButton.onClick.SetListener(DefaultButtonClicked);
	}

	private void DefaultButtonClicked()
	{
		if (CheckVariableType<ItemModifiableVariableFloat01Percent>(out var var, out var item))
		{
			float defaultValue = var.DefaultValue;
			var.SetValue(item, defaultValue);
			slider.SetValueWithoutNotify(defaultValue);
			GameVariables.Current.itemManager.ReportChangeInItemData(item);
		}
	}

	private bool CheckVariableType<T>(out T var, out Item item) where T : ItemModifiableVariable
	{
		item = _currentItem;
		if (_currentVariable is T val)
		{
			var = val;
			return item != null;
		}
		var = null;
		return false;
	}

	private void ToggleValueChanged(bool newValue)
	{
		if (CheckVariableType<ItemModifiableVariableIntBackedBool>(out var var, out var item))
		{
			ItemPrefab itemPrefab = item.GetItemPrefab();
			int variableIndex = Array.IndexOf(itemPrefab.ItemModifiableVariables, var);
			bool value = var.GetValue(item);
			if (itemPrefab.BoolModifiableVariableChangeAllowed(item, variableIndex, newValue))
			{
				itemPrefab.SetBoolModifiableVariableRespectRequirements(item, var, newValue);
				GameVariables.Current.itemManager.ReportChangeInItemData(item);
			}
			else
			{
				toggle.SetIsOnWithoutNotify(value);
			}
		}
	}

	private void UpdateToggle()
	{
		if (CheckVariableType<ItemModifiableVariableIntBackedBool>(out var var, out var item))
		{
			ItemPrefab itemPrefab = item.GetItemPrefab();
			int variableIndex = Array.IndexOf(itemPrefab.ItemModifiableVariables, var);
			bool value = var.GetValue(item);
			toggle.interactable = itemPrefab.BoolModifiableVariableChangeAllowed(item, variableIndex, !value);
			toggle.SetIsOnWithoutNotify(value);
			toggle.gameObject.SetActive(value: true);
		}
		else
		{
			toggle.gameObject.SetActive(value: false);
		}
	}

	private void SliderValueChanged(float arg0)
	{
		if (CheckVariableType<ItemModifiableVariableFloat01Percent>(out var var, out var item))
		{
			var.SetValue(item, arg0);
			GameVariables.Current.itemManager.ReportChangeInItemData(item);
		}
	}

	private void UpdateSlider()
	{
		if (CheckVariableType<ItemModifiableVariableFloat01Percent>(out var var, out var item))
		{
			slider.SetValueWithoutNotify(var.GetValue(item));
			slider.gameObject.SetActive(value: true);
			defaultButton.gameObject.SetActive(value: true);
		}
		else
		{
			slider.gameObject.SetActive(value: false);
			defaultButton.gameObject.SetActive(value: false);
		}
	}

	public void SetData((Item, ItemModifiableVariable) tuple, CompoundItemList.CompoundItemListAdapter<(Item, ItemModifiableVariable), ItemModifiableVariableDisplay> adapter)
	{
		(_currentItem, _currentVariable) = tuple;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.Append(tuple.Item1.GetItemPrefab().Name);
			stringBuilder.Append(" - ");
			stringBuilder.Append(tuple.Item2.GetLocalizedName());
			UpdateToggle();
			UpdateSlider();
			infoText.SetText(stringBuilder);
		}
	}

	public override void AfterReturnedToPool()
	{
		base.AfterReturnedToPool();
		_currentItem = null;
		_currentVariable = null;
	}
}
