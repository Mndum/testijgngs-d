using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompoundItemList : MonoBehaviour
{
	public interface ICompoundItemListAdapter<TItem>
	{
		Selection<TItem> ReferenceSelection { get; set; }

		AdvancedItemList.IAdvancedItemListAdapter<TItem> ItemListAdapter { get; }

		void AppendNameOfItem(TItem item, StringBuilder sb, StringRepresentationHelperEnum stringRepresentationHelperEnum = StringRepresentationHelperEnum.Full);

		string GetNameOfItem(TItem item, StringRepresentationHelperEnum stringRepresentationHelperEnum = StringRepresentationHelperEnum.Full);
	}

	public sealed class CompoundItemListAdapter<TItem, TListItemPrefab> : CompoundItemListAdapterBase, ICompoundItemListAdapter<TItem> where TListItemPrefab : AdvancedItemListPrefab
	{
		public class CompoundItemListInitScope : IDisposable
		{
			private CompoundItemListAdapter<TItem, TListItemPrefab> _gameItemList;

			private bool hasInitialFocusValue;

			private TItem initialFocusValue;

			public CompoundItemListInitScope(CompoundItemListAdapter<TItem, TListItemPrefab> gameItemList)
			{
				_gameItemList = gameItemList;
			}

			public void SetInitialFocusValue(TItem value)
			{
				hasInitialFocusValue = true;
				initialFocusValue = value;
			}

			public void Dispose()
			{
				_gameItemList.FirstUpdateAll(initialFocusValue, hasInitialFocusValue);
			}
		}

		public interface IBuilderNames
		{
			IBuilderSortingStage WithStringRepresentationHelper(StringRepresentationHelper<TItem> helper);

			IBuilderSortingStage WithToStringOnly(Func<TItem, StringRepresentationHelperEnum, string> toString);
		}

		public interface IBuilderSortingStage
		{
			StringRepresentationHelper<TItem> StringRepresentationHelper { get; }

			IBuilderFolders WithSortTypes(SortType<TItem>[] sortTypes);

			IBuilderFolders WithNoSorting();
		}

		public interface IBuilderFolders
		{
			IBuilderSetItem WithAllFolder();

			IBuilderSetItem WithFolders(List<Folder<TItem>> folders);
		}

		public interface IBuilderSetItem
		{
			IBuilderListItemPool WithSetItem(Action<TItem, TListItemPrefab, CompoundItemListAdapter<TItem, TListItemPrefab>> SetFunc);
		}

		public interface IBuilderListItemPool
		{
			IBuilderBeforeBuild WithListPool(PrefabPool<TListItemPrefab> listItemPool);
		}

		public interface IBuilderBeforeBuild
		{
			IBuilderBeforePrepare BuildConvert<TItemIn>(DataSource<TItemIn> itemSource, Func<TItemIn, TItem> convertFunc);

			IBuilderBeforePrepare Build(DataSource<TItem> itemSource);

			CompoundItemListInitScope BuildAndPrepareConvert<TItemIn>(CompoundItemList owner, out CompoundItemListAdapter<TItem, TListItemPrefab> mainAdapter, DataSource<TItemIn> itemSource, Func<TItemIn, TItem> convertFunc, out AdvancedItemList.AdvancedItemListAdapterConvert<TItemIn, TItem, TListItemPrefab> convertAdapter);

			CompoundItemListInitScope BuildAndPrepare(CompoundItemList owner, DataSource<TItem> itemSource, out CompoundItemListAdapter<TItem, TListItemPrefab> mainAdapter, out AdvancedItemList.AdvancedItemListAdapter<TItem, TListItemPrefab> adapter);
		}

		public interface IBuilderBeforePrepare
		{
			CompoundItemListInitScope Prepare(CompoundItemList owner, out CompoundItemListAdapter<TItem, TListItemPrefab> mainAdapter, out AdvancedItemList.IAdvancedItemListAdapter<TItem> subAdapter);

			void AddFolder(Folder<TItem> folder);
		}

		public sealed class Builder : IBuilderFolders, IBuilderSortingStage, IBuilderListItemPool, IBuilderNames, IBuilderSetItem, IBuilderBeforeBuild, IBuilderBeforePrepare
		{
			private PrefabPool<TListItemPrefab> _listItemPool;

			private CompoundItemListAdapter<TItem, TListItemPrefab> _mainAdapter;

			private Func<CompoundItemList, AdvancedItemList.IAdvancedItemListAdapter<TItem>> _prepareFunc;

			public StringRepresentationHelper<TItem> StringRepresentationHelper => _mainAdapter._stringRepresentationHelper;

			private Builder()
			{
				_mainAdapter = new CompoundItemListAdapter<TItem, TListItemPrefab>();
			}

			public static IBuilderNames Start()
			{
				return new Builder();
			}

			public IBuilderSetItem WithFolders(List<Folder<TItem>> folders)
			{
				if (folders == null)
				{
					return WithAllFolder();
				}
				_mainAdapter._folders = folders;
				return this;
			}

			public IBuilderSetItem WithAllFolder()
			{
				_mainAdapter._folders = Folder<TItem>.GetPooledWithAllFolder();
				return this;
			}

			public IBuilderFolders WithSortTypes(SortType<TItem>[] sortTypes)
			{
				_mainAdapter._sortTypes = sortTypes;
				return this;
			}

			public IBuilderFolders WithNoSorting()
			{
				_mainAdapter._sortTypes = new SortType<TItem>[1]
				{
					new SortType<TItem>(() => LOC.UI.Equipment_SortNoSort, null)
				};
				return this;
			}

			public IBuilderSortingStage WithStringRepresentationHelper(StringRepresentationHelper<TItem> helper)
			{
				_mainAdapter._stringRepresentationHelper = helper;
				return this;
			}

			public IBuilderSortingStage WithToStringOnly(Func<TItem, StringRepresentationHelperEnum, string> toString)
			{
				_mainAdapter._stringRepresentationHelper = StringRepresentationHelper<TItem>.CreateBasic(toString);
				return this;
			}

			public IBuilderListItemPool WithSetItem(Action<TItem, TListItemPrefab, CompoundItemListAdapter<TItem, TListItemPrefab>> SetFunc)
			{
				_mainAdapter._setItemAction = SetFunc;
				return this;
			}

			public IBuilderBeforeBuild WithListPool(PrefabPool<TListItemPrefab> listItemPool)
			{
				_listItemPool = listItemPool;
				return this;
			}

			private AdvancedItemList.AdvancedItemListAdapterConvert<TItemIn, TItem, TListItemPrefab> BuildConvertInner<TItemIn>(CompoundItemList owner, DataSource<TItemIn> itemSource, Func<TItemIn, TItem> convertFunc)
			{
				_mainAdapter.Owner = owner;
				CompoundItemListAdapter<TItem, TListItemPrefab> tmp = _mainAdapter;
				AdvancedItemList.AdvancedItemListAdapterConvert<TItemIn, TItem, TListItemPrefab> advancedItemListAdapterConvert = new AdvancedItemList.AdvancedItemListAdapterConvert<TItemIn, TItem, TListItemPrefab>(itemSource.Convert((TItemIn x) => (x: x, convertFunc(x))), _listItemPool, ((TItemIn, TItem) a, TListItemPrefab b) =>
				{
					tmp.UpdateItem(a.Item2, b);
				}, (AdvancedItemListPrefab.TriggerType a, TListItemPrefab b, (TItemIn, TItem) c, PointerEventData d) =>
				{
					tmp.TriggerAction(a, b, c.Item2, d);
				});
				owner.itemList.SetAdapter(advancedItemListAdapterConvert);
				return advancedItemListAdapterConvert;
			}

			private AdvancedItemList.AdvancedItemListAdapter<TItem, TListItemPrefab> BuildInner(CompoundItemList owner, DataSource<TItem> itemSource)
			{
				_mainAdapter.Owner = owner;
				return owner.itemList.SetAdapter(itemSource, _listItemPool, _mainAdapter.UpdateItem, _mainAdapter.TriggerAction);
			}

			private void PrepareInner<T>(T adapter) where T : AdvancedItemList.AdvancedItemListAdapterBase, AdvancedItemList.IAdvancedItemListAdapter<TItem>
			{
				_mainAdapter._itemListAdapter = adapter;
				_mainAdapter.InitAdapter(adapter);
				_mainAdapter.Prepare();
			}

			public CompoundItemListInitScope BuildAndPrepare(CompoundItemList owner, DataSource<TItem> itemSource, out CompoundItemListAdapter<TItem, TListItemPrefab> mainAdapter, out AdvancedItemList.AdvancedItemListAdapter<TItem, TListItemPrefab> adapter)
			{
				adapter = BuildInner(owner, itemSource);
				PrepareInner(adapter);
				mainAdapter = _mainAdapter;
				return new CompoundItemListInitScope(_mainAdapter);
			}

			public CompoundItemListInitScope BuildAndPrepareConvert<TItemIn>(CompoundItemList owner, out CompoundItemListAdapter<TItem, TListItemPrefab> mainAdapter, DataSource<TItemIn> itemSource, Func<TItemIn, TItem> convertFunc, out AdvancedItemList.AdvancedItemListAdapterConvert<TItemIn, TItem, TListItemPrefab> convertAdapter)
			{
				convertAdapter = BuildConvertInner(owner, itemSource, convertFunc);
				PrepareInner(convertAdapter);
				mainAdapter = _mainAdapter;
				return new CompoundItemListInitScope(_mainAdapter);
			}

			public IBuilderBeforePrepare BuildConvert<TItemIn>(DataSource<TItemIn> itemSource, Func<TItemIn, TItem> convertFunc)
			{
				_prepareFunc = (CompoundItemList owner) =>
				{
					AdvancedItemList.AdvancedItemListAdapterConvert<TItemIn, TItem, TListItemPrefab> advancedItemListAdapterConvert = BuildConvertInner(owner, itemSource, convertFunc);
					PrepareInner(advancedItemListAdapterConvert);
					return advancedItemListAdapterConvert;
				};
				return this;
			}

			public IBuilderBeforePrepare Build(DataSource<TItem> itemSource)
			{
				_prepareFunc = (CompoundItemList owner) =>
				{
					AdvancedItemList.AdvancedItemListAdapter<TItem, TListItemPrefab> advancedItemListAdapter = BuildInner(owner, itemSource);
					PrepareInner(advancedItemListAdapter);
					return advancedItemListAdapter;
				};
				return this;
			}

			public CompoundItemListInitScope Prepare(CompoundItemList owner, out CompoundItemListAdapter<TItem, TListItemPrefab> mainAdapter, out AdvancedItemList.IAdvancedItemListAdapter<TItem> subAdapter)
			{
				subAdapter = _prepareFunc(owner);
				_prepareFunc = null;
				mainAdapter = _mainAdapter;
				return new CompoundItemListInitScope(_mainAdapter);
			}

			public void AddFolder(Folder<TItem> folder)
			{
				_mainAdapter._folders.Add(folder);
			}
		}

		public delegate void ItemsClickedDelegate(TListItemPrefab targetIndex);

		public delegate void ItemsMovedDelegate(TListItemPrefab targetRepresentation, TItem sourceItem, int sourceIndex);

		private PrefabPool<TListItemPrefab> _listItemPool;

		public StringRepresentationHelper<TItem> _stringRepresentationHelper;

		private Action<TItem, TListItemPrefab, CompoundItemListAdapter<TItem, TListItemPrefab>> _setItemAction;

		private AdvancedItemList.IAdvancedItemListAdapter<TItem> _itemListAdapter;

		private SortableItemList.ItemListAdapter<Folder<TItem>> _folderItemListAdapter;

		private List<Folder<TItem>> _folders;

		private SortType<TItem>[] _sortTypes;

		public ItemsClickedDelegate ItemRightClickedAction;

		public ItemsMovedDelegate ItemsMovedAction;

		[NonSerialized]
		private FastCheckComponent<DraggedItem> _draggedItemGUI;

		[NonSerialized]
		private int _startedDragProcessedDataIndex;

		private Button _cancelSearchButton;

		private string _currentSearchString = "";

		private Predicate<TItem> _searchCachedMember;

		public Selection<TItem> ReferenceSelection { get; set; }

		public AdvancedItemList.IAdvancedItemListAdapter<TItem> ItemListAdapter => _itemListAdapter;

		public SortableItemList.ItemListAdapter<Folder<TItem>> FolderListAdapter => _folderItemListAdapter;

		public SortType<TItem> CurrentSortType { get; private set; }

		public bool CanRightClickItems => ItemRightClickedAction != null;

		public bool CanMoveItems => ItemsMovedAction != null;

		private void FirstUpdateAll(TItem initialFocusValue, bool hasInitialFocusValue)
		{
			_itemListAdapter.UpdateAll();
			_folderItemListAdapter.UpdateAll();
			_folderItemListAdapter.Focus(0);
			if (hasInitialFocusValue)
			{
				_itemListAdapter.Focus(_itemListAdapter.DetermineIndexOfItem(initialFocusValue));
			}
			else
			{
				_itemListAdapter.Focus(0);
			}
		}

		private void InitAdapter<T>(T adapter) where T : AdvancedItemList.IAdvancedItemListAdapter<TItem>
		{
			_itemListAdapter = adapter;
		}

		private void Prepare()
		{
			PrepareNoItemsInfo();
			PrepareFolders();
			PrepareSort();
			PrepareSearch();
		}

		public string GetNameOfItem(TItem item, StringRepresentationHelperEnum stringRepresentationHelperEnum = StringRepresentationHelperEnum.Full)
		{
			return _stringRepresentationHelper.GetNameOfItem(item, stringRepresentationHelperEnum);
		}

		public void AppendNameOfItem(TItem item, StringBuilder sb, StringRepresentationHelperEnum stringRepresentationHelperEnum = StringRepresentationHelperEnum.Full)
		{
			_stringRepresentationHelper.AppendNameOfItem(item, sb, stringRepresentationHelperEnum);
		}

		private void UpdateItem(TItem item, TListItemPrefab display)
		{
			display.SetFocusOnClick();
			if (CanMoveItems)
			{
				display.EnableDragTrigger();
			}
			if (CanRightClickItems)
			{
				display.EnableRightClickTrigger();
			}
			_setItemAction(item, display, this);
		}

		public override void UpdateAll()
		{
			_itemListAdapter.UpdateAll();
			_folderItemListAdapter.UpdateAll();
			_folderItemListAdapter.FocusCurrentlyFocused();
		}

		private void PrepareFolders()
		{
			_folderItemListAdapter = Owner.folderItemList.SetAdapter(_folders.ToDirectDataSource());
			_folderItemListAdapter.MaskFunc = (Folder<TItem> folder) => !folder.HideIfEmpty || folder.RemainingFolder || _itemListAdapter.ItemCountWithMask(folder.MaskFunc) > 0;
			_folderItemListAdapter.ItemUpdatedAction = (GameObject tmp, Folder<TItem> folder, int i, bool focused) =>
			{
				TextMeshProUGUI componentInChildren = tmp.transform.GetComponentInChildren<TextMeshProUGUI>();
				if (folder != null)
				{
					componentInChildren.text = folder.NameFunc();
				}
				if (focused)
				{
					componentInChildren.fontStyle = FontStyles.Italic | FontStyles.Underline;
					componentInChildren.color = new Color32(207, 10, 79, 255);
				}
				else
				{
					componentInChildren.fontStyle = FontStyles.Normal;
					componentInChildren.color = Color.white;
				}
			};
			_folderItemListAdapter.ItemFocusedAction = (Folder<TItem> folder, int i) =>
			{
				if (folder == null)
				{
					if (_folderItemListAdapter.CurrentlyDisplayedItemCount > 0)
					{
						_folderItemListAdapter.Focus(Mathf.Clamp(i, 0, _folderItemListAdapter.CurrentlyDisplayedItemCount - 1));
						return;
					}
					_itemListAdapter.SetMaskFunc((TItem x) => false);
				}
				else if (folder.RemainingFolder)
				{
					_itemListAdapter.SetMaskFunc(RandomHelpers.CombinePredicates((TItem obj) =>
					{
						foreach (Folder<TItem> currentlyDisplayedItem in _folderItemListAdapter.CurrentlyDisplayedItemList)
						{
							if (currentlyDisplayedItem != folder && !currentlyDisplayedItem.RemainingFolder && !currentlyDisplayedItem.IgnoreForRemainingFolder && currentlyDisplayedItem.MaskFunc != null && currentlyDisplayedItem.MaskFunc(obj))
							{
								return false;
							}
						}
						return true;
					}, _searchCachedMember));
				}
				else
				{
					_itemListAdapter.SetMaskFunc(RandomHelpers.CombinePredicates(folder?.MaskFunc, _searchCachedMember));
				}
				_itemListAdapter.UpdateAll();
				if (!_itemListAdapter.TryGetCurrentlyFocusedItem(out var _))
				{
					_itemListAdapter.Focus(0);
				}
			};
			_folderItemListAdapter.OnAfterUpdated = (int count) =>
			{
				_folderItemListAdapter.ItemList.gameObject.SetActive(count > 0);
				LayoutGroup component = _folderItemListAdapter.ItemList.GetComponent<LayoutGroup>();
				if (component != null && component.transform is RectTransform rt)
				{
					rt.ForceRebuildLayoutRecursive();
				}
			};
		}

		private void PrepareNoItemsInfo()
		{
			if (Owner.noItemsInfo != null)
			{
				Owner.noItemsInfo.SetText(LOC.UI.NoValidItemsFound);
				_itemListAdapter.OnAfterDataChanged = (int count) =>
				{
					Owner.noItemsInfo.gameObject.SetActive(count == 0);
				};
			}
		}

		private void SetSort(SortType<TItem> type)
		{
			CurrentSortType = type;
			if (type == null)
			{
				Owner._sortButtonTMP.text = LOC.UI.Equipment_SortNoSort;
				return;
			}
			Owner._sortButtonTMP.text = type.NameFunc();
			_itemListAdapter.SetSortComparer(type.Comparer);
		}

		private void PrepareSort()
		{
			if (Owner.sortButton != null)
			{
				Owner._sortButtonTMP = Owner.sortButton.GetComponentInChildren<TextMeshProUGUI>();
				Owner.sortButton.onClick.SetListener(SortButtonClicked);
			}
			if (_sortTypes.Length != 0)
			{
				SetSort(_sortTypes[0]);
			}
		}

		private void SortButtonClicked()
		{
			UiOverlay.Instance.OpenContextMenuPopup(_sortTypes.Select((SortType<TItem> t) => new PopupChoice
			{
				Text = t.NameFunc(),
				Action = () =>
				{
					SetSort(t);
					_itemListAdapter.UpdateAll();
				}
			}).ToList());
		}

		private void TriggerAction(AdvancedItemListPrefab.TriggerType triggerType, TListItemPrefab prefab, TItem data, PointerEventData pointerEventData)
		{
			switch (triggerType)
			{
			case AdvancedItemListPrefab.TriggerType.BeginDrag:
				if (CanMoveItems)
				{
					ItemDragBegin(prefab, data);
				}
				break;
			case AdvancedItemListPrefab.TriggerType.Drag:
				ItemDragged(pointerEventData);
				break;
			case AdvancedItemListPrefab.TriggerType.EndDrag:
				ItemDropped(pointerEventData);
				break;
			case AdvancedItemListPrefab.TriggerType.RightClick:
				ItemRightClickedAction?.Invoke(prefab);
				break;
			}
		}

		private void FillDraggedItem(TListItemPrefab prefab, TItem item, DraggedItem draggedItem)
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				AppendNameOfItem(item, stringBuilder);
				draggedItem.ItemName.SetText(stringBuilder);
			}
		}

		private void ItemDragBegin(TListItemPrefab prefab, TItem item)
		{
			FastCheckComponentUtils.ReturnToPoolIfExists(ref _draggedItemGUI);
			if (item != null)
			{
				_startedDragProcessedDataIndex = prefab.Index;
				DraggedItem fromPool = UiOverlay.Instance.draggedItemPrefab.GetPool().GetFromPool(Owner.transform);
				_draggedItemGUI.InitNotNull(fromPool);
				FillDraggedItem(prefab, item, fromPool);
			}
		}

		private void ItemDragged(PointerEventData pointerEventData)
		{
			if (_draggedItemGUI.TryGet(out var component))
			{
				component.transform.localPosition = UiOverlay.Instance.ScreenToTransform(Owner.transform, pointerEventData.position);
			}
		}

		private void ItemDropped(PointerEventData pointerEventData)
		{
			if (!_draggedItemGUI.TryGet(out var component))
			{
				return;
			}
			List<RaycastResult> list = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerEventData, list);
			if (list.Count != 0 && _itemListAdapter is AdvancedItemList.IAdvancedItemListPrefab<TListItemPrefab> advancedItemListPrefab && advancedItemListPrefab.TryDetermineDroppedOnIndex(list, component.transform.position.y, out var foundTargetIndex) && _itemListAdapter.TryGetItemAtProcessedData(_startedDragProcessedDataIndex, out var o))
			{
				if (CurrentSortType == null || CurrentSortType.ItemMoveAllowed)
				{
					ItemsMovedAction?.Invoke(foundTargetIndex, o, _startedDragProcessedDataIndex);
				}
				else
				{
					StringBuilder stringBuilder;
					using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
					{
						stringBuilder.AppendLine(LOC.UI.Equipment_CannotChangeOrderWhileSortedPrompt);
						int num = Array.FindIndex(_sortTypes, (SortType<TItem> x) => x.ForceAllowItemMove || x.Comparer == null);
						if (num >= 0)
						{
							stringBuilder.AppendLine(LOC.UI.Equipment_CannotChangeOrderWhileSortedAllowedSortPrompt(_sortTypes[num].NameFunc()));
						}
						UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, stringBuilder.ToString());
					}
				}
			}
			FastCheckComponentUtils.ReturnToPoolIfExists(ref _draggedItemGUI);
		}

		public override void UnityLoopUpdate()
		{
			if (_draggedItemGUI.Exists && !Input.GetMouseButton(0))
			{
				ItemDropped(new PointerEventData(EventSystem.current));
			}
		}

		private bool SearchMaskFunc(TItem item)
		{
			return _stringRepresentationHelper.IsMatch(item, _currentSearchString);
		}

		private void ChangeSearchString(string newString)
		{
			if (!(_currentSearchString == newString))
			{
				_currentSearchString = newString;
				if (string.IsNullOrEmpty(_currentSearchString))
				{
					_cancelSearchButton.gameObject.SetActive(value: false);
					Owner.inputField.SetTextWithoutNotify("");
				}
				else
				{
					_cancelSearchButton.gameObject.SetActive(value: true);
				}
				_folderItemListAdapter.FocusCurrentlyFocused();
			}
		}

		private void PrepareSearch()
		{
			_cancelSearchButton = Owner.inputField.transform.Find("CancelSearchButton").GetComponent<Button>();
			_cancelSearchButton.gameObject.SetActive(value: false);
			_searchCachedMember = SearchMaskFunc;
			_currentSearchString = "";
			Owner.inputField.SetTextWithoutNotify("");
			Owner.inputField.onValueChanged.SetListener(ChangeSearchString);
			_cancelSearchButton.onClick.SetListener(() =>
			{
				ChangeSearchString("");
				EventSystem.current.SetSelectedGameObject(null);
			});
		}

		public override void Dispose()
		{
			MyCollectionPool<Folder<TItem>>.ReleaseAndSetNull(ref _folders);
		}
	}

	public abstract class CompoundItemListAdapterBase : IDisposable
	{
		public CompoundItemList Owner { get; protected set; }

		public abstract void Dispose();

		public abstract void UpdateAll();

		public abstract void UnityLoopUpdate();
	}

	[SerializeField]
	private AdvancedItemList itemList;

	[SerializeField]
	private SortableItemList folderItemList;

	[SerializeField]
	private Button sortButton;

	private TextMeshProUGUI _sortButtonTMP;

	[SerializeField]
	private TMP_InputField inputField;

	[SerializeField]
	private TextMeshProUGUI noItemsInfo;

	private CompoundItemListAdapterBase _currentAdapter;

	public void Update()
	{
		_currentAdapter?.UnityLoopUpdate();
	}

	private void OnDestroy()
	{
		EnsureNoAdapter();
	}

	private void EnsureNoAdapter()
	{
		if (_currentAdapter != null)
		{
			_currentAdapter.Dispose();
			_currentAdapter = null;
		}
	}

	public void ClearEverything()
	{
		EnsureNoAdapter();
		itemList.RemoveAdapter();
	}

	public static CompoundItemListAdapter<TItem, TPrefab>.IBuilderNames StartBuilder<TItem, TPrefab>() where TPrefab : AdvancedItemListPrefab
	{
		return CompoundItemListAdapter<TItem, TPrefab>.Builder.Start();
	}
}
