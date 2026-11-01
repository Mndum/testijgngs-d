using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NunRepairPopup : PrefabPopup<NunRepairPopup>
{
	[SerializeField]
	private CompoundItemList _gameItemList;

	private CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> _compoundGameItemListAdapter;

	private AdvancedItemList.AdvancedItemListAdapter<Item, GameItemContainer> _gameItemListAdapter;

	[SerializeField]
	private ItemStats itemStats;

	[SerializeField]
	private ButtonList itemStatsButtonList;

	[SerializeField]
	private TextMeshProUGUI _repairInfo;

	private Action _afterClose;

	private ManagedCoroutine _nunsResponseCoroutine = new ManagedCoroutine();

	private void SwitchDisplayedItem(Item item, int i)
	{
		IModificationPeriodButtonList modificationPeriod;
		using (itemStatsButtonList.DoInitialization(out modificationPeriod))
		{
			itemStats.SetItem(item);
			if (item != null)
			{
				Result result = item.CanBeRepaired();
				if (result.IsSuccess)
				{
					ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
					buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.NunRepair_Repair);
					buttonListWrapper.OnClick = RepairButtonClicked;
					_repairInfo.text = LOC.UI.NunRepair_RepairPossible(ItemRepairOrder.CalculateRepairPrice(item));
				}
				else
				{
					_repairInfo.text = result.GetDescription();
				}
			}
			else
			{
				_repairInfo.text = "";
			}
		}
	}

	private IEnumerator MovePopup(float target, float time = 0.5f)
	{
		RectTransform rt = (RectTransform)transform;
		float num = 800f;
		Vector2 anchoredPosition = rt.anchoredPosition;
		float xAnchoredPosition = anchoredPosition.x;
		float y = anchoredPosition.y;
		float endValue = target * num;
		return CoroutineUtilities.GenericTweenEaseOutQuart((float x) =>
		{
			rt.anchoredPosition = new Vector2(xAnchoredPosition, x);
		}, y, endValue, time);
	}

	private IEnumerator NunSayEnumerator(string text)
	{
		yield return MovePopup(1f);
		yield return SingletonMonoBehaviour<FungusEnumerators>.Instance.DoConversation(text);
		yield return MovePopup(0f);
	}

	private void NunSay(string text)
	{
		if (_nunsResponseCoroutine.NotRunning)
		{
			_nunsResponseCoroutine.Start(this, NunSayEnumerator(text));
		}
	}

	private void RepairButtonClicked()
	{
		Item item = itemStats.CurrentItem;
		if (item == null)
		{
			return;
		}
		if (item.IsEquipped())
		{
			NunSay(LOC.Other.NunRepair_EquipedItem);
			return;
		}
		if (GameVariables.Current.itemManager.NunBusyWithRepair)
		{
			RemindBusy();
			return;
		}
		int num = ItemRepairOrder.CalculateRepairPrice(item);
		UiOverlay.Instance.BuyPopup(LOC.UI.UI_AreYouSure, num, () =>
		{
			GameVariables.Current.itemManager.MakeNunRepairOrder(item);
			SyncAll();
			NunSay(StringUtils.SplitStringAndPickRandom(LOC.Other.NunRepair_RepairStarted));
		}, LOC.UI.NunRepair_RepairPrompt(item.GetName(), num), () =>
		{
			NunSay(StringUtils.SplitStringAndPickRandom(LOC.Other.NunRepair_Denied));
		}, () =>
		{
			NunSay(StringUtils.SplitStringAndPickRandom(LOC.Other.NunRepair_DeniedNoMoney));
		});
	}

	private void RemindBusy()
	{
		if (GameVariables.Current.itemManager.TryGetCurrentNunRepairOrder(out var itemRepairOrder))
		{
			NunSay(StringUtils.SplitStringAndPickRandom(LOC.Other.NunRepair_Busy(itemRepairOrder.RepairedItem.GetName())));
		}
	}

	private void SyncAll()
	{
		_compoundGameItemListAdapter.UpdateAll();
	}

	public override void Open()
	{
		using (CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
			.WithAllFolder()
			.WithDefaultGameItemSetItem()
			.WithDefaultGameItemListItemPool()
			.BuildAndPrepare(_gameItemList, DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.itemManager.GetAllRepairableItems()), out _compoundGameItemListAdapter, out _gameItemListAdapter))
		{
			_gameItemListAdapter.UpdateItemAction = (Item item, GameItemContainer display) =>
			{
				display.SetItem(item);
				display.SetFocusOnClick();
			};
			_gameItemListAdapter.OnFocusedItemChanged = SwitchDisplayedItem;
		}
		itemStats.CurrentItemInternallyChanged = GameVariables.Current.itemManager.ReportChangeInItemData;
		base.Open();
		RemindBusy();
	}

	public override bool CloseInner()
	{
		itemStats.CurrentItemInternallyChanged = null;
		base.CloseInner();
		_afterClose?.Invoke();
		return true;
	}
}
