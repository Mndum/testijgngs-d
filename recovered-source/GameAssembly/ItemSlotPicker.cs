using System;
using TMPro;
using UnityEngine;

public class ItemSlotPicker : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI title;

	[SerializeField]
	private TextMeshProUGUI description;

	[SerializeField]
	private ItemStats itemStats;

	[SerializeField]
	private ButtonList choiceButtonList;

	private ItemSlot _currentSlot;

	private Func<string> _noItemInfoTextGetter;

	private bool _initalized;

	private void EnsureInit()
	{
		if (!_initalized)
		{
			_initalized = true;
			GameVariables.Current.itemManager.OnEquippedItemsModified += UpdateSlotData;
		}
	}

	private void EnsureKilled()
	{
		if (_initalized)
		{
			_initalized = false;
			GameVariables.Current.itemManager.OnEquippedItemsModified -= UpdateSlotData;
		}
	}

	public void OnDisable()
	{
		EnsureKilled();
	}

	public void OnDestroy()
	{
		EnsureKilled();
	}

	public void SetTargetSlot(ItemSlot slot, Func<string> noItemInfoTextGetter)
	{
		_currentSlot = slot;
		_noItemInfoTextGetter = noItemInfoTextGetter;
		title.SetText(slot.FriendlyName);
		EnsureInit();
		UpdateSlotData();
	}

	public void UpdateSlotData()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (choiceButtonList.DoInitialization(out modificationPeriod))
		{
			if (_currentSlot != null)
			{
				Item firstItemAtSlot = GameVariables.Current.itemManager.GetFirstItemAtSlot(_currentSlot.Id);
				if (firstItemAtSlot != null)
				{
					itemStats.gameObject.SetActive(value: true);
					description.gameObject.SetActive(value: false);
					itemStats.SetItem(firstItemAtSlot);
					ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
					buttonListWrapper.SetText(LOC.UI.UI_Change);
					buttonListWrapper.OnClick = () =>
					{
						UiOverlay.Instance.OpenSlotEquipItemPopup(_currentSlot);
					};
					ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(5, CommonButtonColorType.Red);
					buttonListWrapper2.SetText(LOC.UI.Equipment_ButtonUnequip);
					buttonListWrapper2.OnClick = () =>
					{
						if (_currentSlot != null)
						{
							GameVariables.Current.itemManager.UnequipItemWithDependencies(_currentSlot.Id);
						}
					};
					return;
				}
			}
			itemStats.gameObject.SetActive(value: false);
			description.gameObject.SetActive(value: true);
			itemStats.SetItem(null);
			description.SetText(_noItemInfoTextGetter());
			ButtonListWrapper buttonListWrapper3 = modificationPeriod.AddButton();
			buttonListWrapper3.SetText(LOC.UI.Equipment_ButtonEquip);
			buttonListWrapper3.OnClick = () =>
			{
				UiOverlay.Instance.OpenSlotEquipItemPopup(_currentSlot);
			};
		}
	}
}
