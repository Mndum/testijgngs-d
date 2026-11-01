using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed class Selection<T>
{
	private Func<T, int> _countGetter;

	private Dictionary<T, int> _selectedItems = new Dictionary<T, int>();

	private int _selectedItemsCount;

	private Selection<T> _parentSelection;

	private int _maxTotalItemCount = -1;

	private int _maxPerItemCount = -1;

	private int _maxUniqueItemCount = -1;

	private bool _partialAllowed = true;

	public int MaxTotalItemCount
	{
		get
		{
			return _maxTotalItemCount;
		}
		set
		{
			_maxTotalItemCount = value;
			ClearIfLimitsNotMet();
		}
	}

	public bool HasTotalItemCountLimit => _maxTotalItemCount >= 0;

	public int MaxPerItemCount
	{
		get
		{
			return _maxPerItemCount;
		}
		set
		{
			_maxPerItemCount = value;
			ClearIfLimitsNotMet();
		}
	}

	public bool HasPerItemCountLimit => _maxPerItemCount >= 0;

	public int MaxUniqueItemCount
	{
		get
		{
			return _maxUniqueItemCount;
		}
		set
		{
			_maxUniqueItemCount = value;
			ClearIfLimitsNotMet();
		}
	}

	public bool HasUniqueItemCountLimit => _maxUniqueItemCount >= 0;

	public bool PartialAllowed
	{
		get
		{
			return _partialAllowed;
		}
		set
		{
			_partialAllowed = value;
			ClearIfLimitsNotMet();
		}
	}

	public int TotalItemCountLeft
	{
		get
		{
			if (!HasTotalItemCountLimit)
			{
				return 2147483647;
			}
			return MaxTotalItemCount - _selectedItemsCount;
		}
	}

	public int SelectedItemsCount => _selectedItemsCount;

	public int SelectedUniqueItemCount => _selectedItems.Count;

	public bool CanSelectOnlySingleItem
	{
		get
		{
			if (MaxUniqueItemCount != 1 || PartialAllowed)
			{
				return MaxTotalItemCount == 1;
			}
			return true;
		}
	}

	public bool NoSelectionPossible
	{
		get
		{
			if (MaxUniqueItemCount != 0)
			{
				return MaxTotalItemCount == 0;
			}
			return true;
		}
	}

	public bool CanPotentiallySelectMultipleItems
	{
		get
		{
			if (HasTotalItemCountLimit && MaxTotalItemCount <= 1)
			{
				return false;
			}
			if (HasUniqueItemCountLimit && MaxUniqueItemCount <= 1)
			{
				return false;
			}
			return true;
		}
	}

	public Selection<T> ParentSelection
	{
		get
		{
			return _parentSelection;
		}
		set
		{
			_parentSelection = value;
		}
	}

	public Selection(Func<T, int> countGetter)
	{
		_countGetter = countGetter;
	}

	public Selection<T> Clone()
	{
		Selection<T> selection = (Selection<T>)MemberwiseClone();
		selection._selectedItems = new Dictionary<T, int>(_selectedItems);
		return selection;
	}

	public void DescribeItemSelection(StringBuilder stringBuilder, T item)
	{
		if (_selectedItems.TryGetValue(item, out var value))
		{
			stringBuilder.Append(" (");
			stringBuilder.Append(LOC.UI.Selected);
			int itemCount = GetItemCount(item);
			if (itemCount > 1)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(value);
				stringBuilder.Append("/");
				stringBuilder.Append(itemCount);
			}
			stringBuilder.Append(")");
		}
	}

	public IEnumerable<KeyValuePair<T, int>> GetAllItemsWithCounts()
	{
		return _selectedItems;
	}

	public IReadOnlyCollection<T> GetAllItems()
	{
		return _selectedItems.Keys;
	}

	public bool IsSelected(T item)
	{
		return _selectedItems.ContainsKey(item);
	}

	public bool IsFullySelected(T item)
	{
		return GetMaxSelectedCountForItem(item) == GetItemSelectedCount(item);
	}

	public int GetItemSelectedCount(T item)
	{
		return _selectedItems.GetValueOrDefault(item, 0);
	}

	public int GetMaxSelectedCountForItem(T item)
	{
		return GetItemMaxAllowedCountFollowingRules(item, GetItemSelectedCount(item));
	}

	public T GetFirstItemOrNull()
	{
		return _selectedItems.Keys.FirstOrDefault();
	}

	public bool TryGetFirstItem(out T item)
	{
		if (_selectedItems.Count > 0)
		{
			item = _selectedItems.Keys.First();
			return true;
		}
		item = default;
		return false;
	}

	public bool AreAllFullySelected(IEnumerable<T> items)
	{
		return items.All(IsFullySelected);
	}

	private void ClearIfLimitsNotMet()
	{
		bool num = HasPerItemCountLimit && _selectedItems.Any((KeyValuePair<T, int> x) => x.Value > _maxTotalItemCount);
		bool flag = HasTotalItemCountLimit && _selectedItemsCount > _maxTotalItemCount;
		bool flag2 = HasUniqueItemCountLimit && _selectedItems.Count > _maxUniqueItemCount;
		bool flag3 = !_partialAllowed && _selectedItems.Any((KeyValuePair<T, int> x) => x.Value != GetItemMaxAllowedCountFollowingRules(x.Key, x.Value));
		if (num | flag | flag2 | flag3)
		{
			Clear();
		}
	}

	private int GetItemCount(T item)
	{
		if (_parentSelection == null)
		{
			return _countGetter(item);
		}
		return _countGetter(item) - _parentSelection.GetItemSelectedCount(item);
	}

	private int GetItemMaxAllowedCountFollowingRules(T item, int currentCount)
	{
		int num = GetItemCount(item);
		if (HasPerItemCountLimit)
		{
			num = Mathf.Min(num, _maxPerItemCount);
		}
		if (HasTotalItemCountLimit)
		{
			int b = MaxTotalItemCount - _selectedItemsCount + currentCount;
			num = Mathf.Min(num, b);
		}
		if (HasUniqueItemCountLimit && _selectedItems.Count >= MaxUniqueItemCount && currentCount == 0)
		{
			return 0;
		}
		return num;
	}

	public int SetItemCount(T item, int newCount)
	{
		int itemSelectedCount = GetItemSelectedCount(item);
		int itemMaxAllowedCountFollowingRules = GetItemMaxAllowedCountFollowingRules(item, itemSelectedCount);
		newCount = Mathf.Clamp(newCount, 0, itemMaxAllowedCountFollowingRules);
		if (newCount <= 0)
		{
			RemoveItemFull(item);
			return 0;
		}
		if (!PartialAllowed && newCount != itemMaxAllowedCountFollowingRules)
		{
			Debug.LogWarning("Tried to set partial value to a selection not allowing partial selections!");
			RemoveItemFull(item);
			return 0;
		}
		int num = newCount - itemSelectedCount;
		_selectedItemsCount += num;
		_selectedItems[item] = newCount;
		return newCount;
	}

	public void SelectAllItems(IEnumerable<T> items)
	{
		foreach (T item in items)
		{
			int maxSelectedCountForItem = GetMaxSelectedCountForItem(item);
			SetItemCount(item, maxSelectedCountForItem);
		}
	}

	public void Clear()
	{
		_selectedItemsCount = 0;
		_selectedItems.Clear();
	}

	public void RemoveItemFull(T item)
	{
		if (_selectedItems.TryGetValue(item, out var value))
		{
			_selectedItemsCount -= value;
			_selectedItems.Remove(item);
		}
	}

	public void AddItemFull(T item)
	{
		SetItemCount(item, 2147483647);
	}

	public int AddItem(T item, int toAddCount)
	{
		if (toAddCount < 0)
		{
			Debug.LogError($"Trying to add a negative item count {toAddCount}! {item}");
			return 0;
		}
		int itemSelectedCount = GetItemSelectedCount(item);
		int num = itemSelectedCount + toAddCount;
		int itemMaxAllowedCountFollowingRules = GetItemMaxAllowedCountFollowingRules(item, itemSelectedCount);
		if (num > itemMaxAllowedCountFollowingRules)
		{
			Debug.LogWarning($"Trying to add more items than exist to the selection! {item}");
			num = itemMaxAllowedCountFollowingRules;
		}
		return SetItemCount(item, num) - itemSelectedCount;
	}

	public void RemoveItem(T item, int toRemoveCount)
	{
		if (toRemoveCount < 0)
		{
			Debug.LogError($"Trying to remove negative item count {toRemoveCount}! {item}");
			return;
		}
		int num = GetItemSelectedCount(item) - toRemoveCount;
		if (num < 0)
		{
			Debug.LogWarning($"Trying to remove more items than exist from the selection! {item}");
			num = 0;
		}
		SetItemCount(item, num);
	}
}
