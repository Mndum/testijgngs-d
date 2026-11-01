using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseItemsComplexPopup : PrefabPopup<ChooseItemsComplexPopup>, IPopupCloseScriptHandler
{
	public interface IBuilderAdapterReady
	{
		ChooseItemsAdapterBase FinishInit(ChooseItemsComplexPopup owner);
	}

	public class ChooseItemsAdapter<TData, TPrefab, TDisplayPrefab> : ChooseItemsAdapterBase where TPrefab : AdvancedItemListPrefab where TDisplayPrefab : MonoBehaviour, IPooledPrefab
	{
		public interface IBuilderOutsideSelection
		{
			IBuilderOutsideOnFinished WithStartingSelection(Selection<TData> startSelection, Selection<TData> alreadyClaimedSelection = null);

			IBuilderOutsideOnFinished WithSingularStartingSelection(Selection<TData> alreadyClaimedSelection = null);

			IBuilderOutsideOnFinished WithNoChoosingSelection();

			IBuilderOutsideOnFinished WithSingularStartingSelectionSingleItemAllowed(Selection<TData> alreadyClaimedSelection = null);
		}

		public interface IBuilderOutsideOnFinished
		{
			IBuilderAdapterReady WithOnFinished(Action<UiOverlay.PopupFinishTypesEnum, Selection<TData>> onFinished);

			IBuilderAdapterReady WithSuccessfulOnlyOnFinished(Action<Selection<TData>> onFinished);
		}

		public class Builder : IBuilderOutsideSelection, IBuilderOutsideOnFinished, IBuilderAdapterReady
		{
			private readonly ChooseItemsAdapter<TData, TPrefab, TDisplayPrefab> _adapter;

			private readonly CompoundItemList.CompoundItemListAdapter<TData, TPrefab>.IBuilderBeforePrepare _compoundItemListBuilder;

			private Builder(CompoundItemList.CompoundItemListAdapter<TData, TPrefab>.IBuilderBeforePrepare compoundItemListBuilder, PrefabPool<TDisplayPrefab> itemDataDisplayPool, Action<ChooseItemsAdapter<TData, TPrefab, TDisplayPrefab>, TData, TDisplayPrefab> showDataOnDisplay)
			{
				_compoundItemListBuilder = compoundItemListBuilder;
				_adapter = new ChooseItemsAdapter<TData, TPrefab, TDisplayPrefab>
				{
					_displayPrefabPool = itemDataDisplayPool,
					_showDisplayAction = showDataOnDisplay
				};
			}

			public static IBuilderOutsideSelection Start(CompoundItemList.CompoundItemListAdapter<TData, TPrefab>.IBuilderBeforePrepare compoundItemListBuilder, PrefabPool<TDisplayPrefab> itemDataDisplayPool, Action<ChooseItemsAdapter<TData, TPrefab, TDisplayPrefab>, TData, TDisplayPrefab> showDataOnDisplay)
			{
				return new Builder(compoundItemListBuilder, itemDataDisplayPool, showDataOnDisplay);
			}

			public IBuilderOutsideOnFinished WithSingularStartingSelection(Selection<TData> alreadyClaimedSelection = null)
			{
				_adapter._currentSelection = SelectionFactory.CreateInstanceSingular<TData>();
				_adapter._currentSelection.ParentSelection = alreadyClaimedSelection;
				return this;
			}

			public IBuilderOutsideOnFinished WithSingularStartingSelectionSingleItemAllowed(Selection<TData> alreadyClaimedSelection = null)
			{
				_adapter._currentSelection = SelectionFactory.CreateInstanceSingular<TData>();
				_adapter._currentSelection.PartialAllowed = false;
				_adapter._currentSelection.MaxUniqueItemCount = 1;
				_adapter._currentSelection.ParentSelection = alreadyClaimedSelection;
				return this;
			}

			public IBuilderOutsideOnFinished WithNoChoosingSelection()
			{
				_adapter._currentSelection = SelectionFactory.CreateInstanceSingular<TData>();
				_adapter._currentSelection.PartialAllowed = false;
				_adapter._currentSelection.MaxUniqueItemCount = 0;
				_adapter._currentSelection.MaxTotalItemCount = 0;
				return this;
			}

			public IBuilderOutsideOnFinished WithStartingSelection([NotNull] Selection<TData> startSelection, Selection<TData> alreadyClaimedSelection = null)
			{
				_adapter._currentSelection = startSelection;
				_adapter._currentSelection.ParentSelection = alreadyClaimedSelection;
				return this;
			}

			public IBuilderAdapterReady WithOnFinished(Action<UiOverlay.PopupFinishTypesEnum, Selection<TData>> onFinished)
			{
				_adapter._onFinished = onFinished;
				return this;
			}

			public IBuilderAdapterReady WithSuccessfulOnlyOnFinished(Action<Selection<TData>> onFinished)
			{
				_adapter._onFinished = (UiOverlay.PopupFinishTypesEnum a, Selection<TData> b) =>
				{
					if (a == UiOverlay.PopupFinishTypesEnum.Successful)
					{
						onFinished(b);
					}
				};
				return this;
			}

			public ChooseItemsAdapterBase FinishInit(ChooseItemsComplexPopup owner)
			{
				_adapter.FinishInit(owner, _compoundItemListBuilder);
				return _adapter;
			}
		}

		private IEnumerable<TData> _toChooseFrom;

		private Action<ChooseItemsAdapter<TData, TPrefab, TDisplayPrefab>, TData, TDisplayPrefab> _showDisplayAction;

		private PrefabPool<TDisplayPrefab> _displayPrefabPool;

		private Action<UiOverlay.PopupFinishTypesEnum, Selection<TData>> _onFinished;

		private Action<ChooseItemsAdapter<TData, TPrefab, TDisplayPrefab>> _afterItemListPrepare;

		private Selection<TData> _currentSelection;

		private TDisplayPrefab _itemDataDisplay;

		private AdvancedItemList.IAdvancedItemListAdapter<TData> _gameItemListAdapter;

		private CompoundItemList.CompoundItemListAdapter<TData, TPrefab> _compoundItemListAdapter;

		private TData _currentItem;

		public CompoundItemList.ICompoundItemListAdapter<TData> CompoundItemListAdapter => _compoundItemListAdapter;

		private void FinishInit(ChooseItemsComplexPopup owner, CompoundItemList.CompoundItemListAdapter<TData, TPrefab>.IBuilderBeforePrepare compoundItemListBuilder)
		{
			SetOwner(owner);
			_itemDataDisplay = _displayPrefabPool.GetFromPool(Owner.displayArea);
			if (_currentSelection == null)
			{
				_currentSelection = SelectionFactory.CreateInstanceSingular<TData>();
			}
			if (_currentSelection.NoSelectionPossible)
			{
				owner.fullSelectionTitle.text = "";
			}
			else
			{
				owner.fullSelectionTitle.text = LOC.UI.CurrentSelection;
				compoundItemListBuilder.AddFolder(new Folder<TData>(() => LOC.UI.Items_SelectedItems, _currentSelection.IsSelected));
			}
			using CompoundItemList.CompoundItemListAdapter<TData, TPrefab>.CompoundItemListInitScope compoundItemListInitScope = compoundItemListBuilder.Prepare(owner.gameItemList, out _compoundItemListAdapter, out _gameItemListAdapter);
			_compoundItemListAdapter.ReferenceSelection = _currentSelection;
			_compoundItemListAdapter.ItemRightClickedAction = (TPrefab item) =>
			{
				_gameItemListAdapter.Focus(item.Index);
				Owner.currentItemSelectionButtons.ShowAsContextMenu();
			};
			_gameItemListAdapter.OnFocusedItemChanged = ItemFocused;
			if (_currentSelection.CanSelectOnlySingleItem && _currentSelection.SelectedUniqueItemCount == 1)
			{
				compoundItemListInitScope.SetInitialFocusValue(_currentSelection.GetFirstItemOrNull());
			}
			_afterItemListPrepare?.Invoke(this);
			UpdateBottomButtonList();
			UpdateTopButtonList();
		}

		public override void Finished(UiOverlay.PopupFinishTypesEnum finishType)
		{
			_itemDataDisplay.ReturnToPool();
			_itemDataDisplay = null;
			_currentSelection.ParentSelection = null;
			_onFinished?.Invoke(finishType, _currentSelection);
		}

		private void UpdateCurrentSelectionText(TData currentItem, int selectedCount)
		{
			int maxSelectedCountForItem = _currentSelection.GetMaxSelectedCountForItem(currentItem);
			if (_currentSelection.NoSelectionPossible)
			{
				Owner.currentItemSelectionInfo.SetText("");
				Owner.fullSelectionInfo.SetText("");
				return;
			}
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				if (maxSelectedCountForItem == 1)
				{
					stringBuilder.Append((selectedCount != 0) ? LOC.UI.Selected : LOC.UI.NotSelected);
				}
				else if (maxSelectedCountForItem > 0)
				{
					stringBuilder.Append(selectedCount);
					stringBuilder.Append("/");
					stringBuilder.Append(maxSelectedCountForItem);
				}
				else
				{
					stringBuilder.Append(LOC.UI.ItemSelectionCantSelect);
				}
				Owner.currentItemSelectionInfo.SetText(stringBuilder);
				stringBuilder.Clear();
				_currentSelection.AppendDescribeSelection(stringBuilder, (TData i, StringBuilder sb) =>
				{
					_compoundItemListAdapter.AppendNameOfItem(i, sb, StringRepresentationHelperEnum.Short);
				});
				Owner.fullSelectionInfo.SetText(stringBuilder);
			}
		}

		private void SelectionScrollbarValueChanged(float arg)
		{
			TData currentItem = _currentItem;
			if (currentItem != null)
			{
				Scrollbar currentItemSelectionScrollbar = Owner.currentItemSelectionScrollbar;
				int itemSelectedCount = _currentSelection.GetItemSelectedCount(currentItem);
				int valueInt = currentItemSelectionScrollbar.GetValueInt();
				int num = _currentSelection.SetItemCount(currentItem, valueInt);
				if (itemSelectedCount != num)
				{
					AfterSelectionModified();
				}
			}
		}

		private (int current, int max) GetCurrentItemCount()
		{
			TData currentItem = _currentItem;
			if (currentItem != null)
			{
				return (current: _currentSelection.GetItemSelectedCount(currentItem), max: _currentSelection.GetMaxSelectedCountForItem(currentItem));
			}
			return (current: 0, max: 0);
		}

		private void UpdateCurrentItem()
		{
			Scrollbar currentItemSelectionScrollbar = Owner.currentItemSelectionScrollbar;
			ButtonList currentItemSelectionButtons = Owner.currentItemSelectionButtons;
			RectTransform selectionArea = Owner.selectionArea;
			TextMeshProUGUI currentItemSelectionInfo = Owner.currentItemSelectionInfo;
			currentItemSelectionScrollbar.onValueChanged.RemoveAllListeners();
			ButtonList.ButtonListInitialization buttonListInitialization = currentItemSelectionButtons.DoInitialization(out var modificationPeriod);
			TData currentItem = _currentItem;
			if (currentItem != null)
			{
				int itemSelectedCount = _currentSelection.GetItemSelectedCount(currentItem);
				int maxSelectedCountForItem = _currentSelection.GetMaxSelectedCountForItem(currentItem);
				if ((_currentSelection.MaxUniqueItemCount == 1 && !_currentSelection.PartialAllowed) || _currentSelection.NoSelectionPossible)
				{
					currentItemSelectionScrollbar.gameObject.SetActive(value: false);
				}
				else if (maxSelectedCountForItem == 0)
				{
					ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Red);
					buttonListWrapper.SetText(LOC.UI.UnselectOthers);
					buttonListWrapper.OnClick = UnselectOthers;
					currentItemSelectionScrollbar.gameObject.SetActive(value: false);
				}
				else
				{
					ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(0, CommonButtonColorType.Red);
					buttonListWrapper2.SetText((maxSelectedCountForItem > 1) ? LOC.UI.SelectNoneShort : LOC.UI.UnselectAction);
					buttonListWrapper2.OnClick = SelectNoneCurrentItem;
					buttonListWrapper2.ActiveFunc = () => GetCurrentItemCount().current > 0;
					ButtonListWrapper buttonListWrapper3 = modificationPeriod.AddButton(0, CommonButtonColorType.Alternative);
					buttonListWrapper3.SetText((maxSelectedCountForItem > 1) ? LOC.UI.SelectAllShort : LOC.UI.SelectAction);
					buttonListWrapper3.OnClick = SelectAllCurrentItem;
					buttonListWrapper3.ActiveFunc = () =>
					{
						var (num, num2) = GetCurrentItemCount();
						return num < num2;
					};
					if (_currentSelection.PartialAllowed && maxSelectedCountForItem > 1)
					{
						currentItemSelectionScrollbar.numberOfSteps = maxSelectedCountForItem + 1;
						currentItemSelectionScrollbar.SetValueInt(itemSelectedCount);
						currentItemSelectionScrollbar.SetSizeForStepsInt(0.2f);
						currentItemSelectionScrollbar.onValueChanged.AddListener(SelectionScrollbarValueChanged);
						currentItemSelectionScrollbar.gameObject.SetActive(value: true);
					}
					else
					{
						currentItemSelectionScrollbar.gameObject.SetActive(value: false);
					}
				}
				buttonListInitialization.Dispose();
				UpdateCurrentSelectionText(currentItem, itemSelectedCount);
				selectionArea.gameObject.SetActive(value: true);
			}
			else
			{
				selectionArea.gameObject.SetActive(value: false);
				currentItemSelectionInfo.SetText("");
			}
		}

		private void SelectNoneCurrentItem()
		{
			TData currentItem = _currentItem;
			if (currentItem != null)
			{
				_currentSelection.RemoveItemFull(currentItem);
				AfterSelectionModified();
			}
		}

		private void SelectAllCurrentItem()
		{
			TData currentItem = _currentItem;
			if (currentItem != null)
			{
				int maxSelectedCountForItem = _currentSelection.GetMaxSelectedCountForItem(currentItem);
				_currentSelection.SetItemCount(currentItem, maxSelectedCountForItem);
				AfterSelectionModified();
			}
		}

		private void UnselectOthers()
		{
			TData currentItem = _currentItem;
			if (currentItem != null)
			{
				int itemSelectedCount = _currentSelection.GetItemSelectedCount(currentItem);
				_currentSelection.Clear();
				_currentSelection.AddItem(currentItem, itemSelectedCount);
				AfterSelectionModified();
			}
		}

		private void UnselectAll()
		{
			_currentSelection.Clear();
			AfterSelectionModified();
		}

		private void SelectAll()
		{
			_currentSelection.SelectAllItems(_compoundItemListAdapter.ItemListAdapter.GetProcessedData());
			AfterSelectionModified();
		}

		private void AfterSelectionModified()
		{
			_compoundItemListAdapter.ItemListAdapter.UpdateVisibleNotData();
			UpdateCurrentItem();
			UpdateTopButtonList();
		}

		private void ItemFocused(TData item, int arg2)
		{
			TData currentItem = _currentItem;
			_currentItem = item;
			if (!object.Equals(currentItem, item))
			{
				if (_currentSelection.CanSelectOnlySingleItem && _currentItem != null)
				{
					_currentSelection.Clear();
					int maxSelectedCountForItem = _currentSelection.GetMaxSelectedCountForItem(item);
					_currentSelection.AddItem(item, maxSelectedCountForItem);
					AfterSelectionModified();
				}
				else
				{
					UpdateCurrentItem();
				}
			}
			_showDisplayAction(this, item, _itemDataDisplay);
		}

		private void UpdateTopButtonList()
		{
			IModificationPeriodButtonList modificationPeriod;
			using (Owner.mainTopButtonList.DoInitialization(out modificationPeriod))
			{
				if (_currentSelection.CanPotentiallySelectMultipleItems)
				{
					if (_currentSelection.SelectedItemsCount > 0)
					{
						ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Red);
						buttonListWrapper.SetText(LOC.UI.UnselectAllAction);
						buttonListWrapper.OnClick = UnselectAll;
					}
					if (!_currentSelection.AreAllFullySelected(_compoundItemListAdapter.ItemListAdapter.GetProcessedData()) || !_compoundItemListAdapter.ItemListAdapter.GetProcessedData().Any())
					{
						ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(0, CommonButtonColorType.Alternative);
						buttonListWrapper2.SetText(LOC.UI.SelectAllAction);
						buttonListWrapper2.OnClick = SelectAll;
					}
				}
			}
		}

		private void UpdateBottomButtonList()
		{
			IModificationPeriodButtonList modificationPeriod;
			using (Owner.mainBottomButtonList.DoInitialization(out modificationPeriod))
			{
				if (Owner._showCancel)
				{
					ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Red);
					buttonListWrapper.SetText(LOC.UI.UI_Cancel);
					buttonListWrapper.OnClick = () =>
					{
						Owner.Finish(UiOverlay.PopupFinishTypesEnum.Cancelled);
					};
				}
				ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(10, CommonButtonColorType.Green);
				buttonListWrapper2.SetText(LOC.UI.UI_Continue);
				buttonListWrapper2.OnClick = () =>
				{
					Owner.Finish(UiOverlay.PopupFinishTypesEnum.Successful);
				};
			}
		}
	}

	public abstract class ChooseItemsAdapterBase
	{
		public ChooseItemsComplexPopup Owner { get; private set; }

		protected void SetOwner(ChooseItemsComplexPopup owner)
		{
			Owner = owner;
		}

		public abstract void Finished(UiOverlay.PopupFinishTypesEnum finishType);
	}

	[SerializeField]
	private TextMeshProUGUI titleTmp;

	[SerializeField]
	private TextMeshProUGUI descriptionTmp;

	[SerializeField]
	private Button closeButton;

	[SerializeField]
	private ButtonList mainTopButtonList;

	[SerializeField]
	private ButtonList mainBottomButtonList;

	[SerializeField]
	private CompoundItemList gameItemList;

	[SerializeField]
	private RectTransform displayArea;

	[SerializeField]
	private RectTransform selectionArea;

	[SerializeField]
	private ButtonList currentItemSelectionButtons;

	[SerializeField]
	private Scrollbar currentItemSelectionScrollbar;

	[SerializeField]
	private TextMeshProUGUI currentItemSelectionInfo;

	[SerializeField]
	private TextMeshProUGUI fullSelectionInfo;

	[SerializeField]
	private TextMeshProUGUI fullSelectionTitle;

	private ChooseItemsAdapterBase _adapter;

	private bool _showCancel;

	private void Finish(UiOverlay.PopupFinishTypesEnum finishType)
	{
		_adapter?.Finished(finishType);
		_adapter = null;
		CloseFromUIOverlay();
	}

	public void CloseFromPopupCloseScript()
	{
		Finish(UiOverlay.PopupFinishTypesEnum.Closed);
	}

	public void Prepare(string title, string description, bool allowClosing, bool showCancel)
	{
		titleTmp.text = title;
		descriptionTmp.text = description;
		_showCancel = showCancel;
		if (!allowClosing)
		{
			closeButton.gameObject.SetActive(value: false);
		}
	}

	public void Open(IBuilderAdapterReady adapter)
	{
		_adapter = adapter.FinishInit(this);
		Open();
	}
}
