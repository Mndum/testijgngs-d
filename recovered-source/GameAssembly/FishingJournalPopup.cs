using UnityEngine;

public class FishingJournalPopup : PrefabPopup<FishingJournalPopup>
{
	[SerializeField]
	private AdvancedItemList _fishItemList;

	[SerializeField]
	private PooledPrefabReference<FishingJournalFishEntry> _fishEntryPrefab;

	private AdvancedItemList.IAdvancedItemListAdapter<GameId> _fishItemListAdapter;

	public override void Open()
	{
		_fishItemListAdapter = _fishItemList.SetAdapter(DataSourceUtils.CreateMultipleUsePooledHashset(() =>
		{
			FishingLocationDataManager.GetAllPossibleFishes(out var fishIds);
			return fishIds;
		}), _fishEntryPrefab.GetPool(), (GameId data, FishingJournalFishEntry display) =>
		{
			display.SetData(data);
		});
		_fishItemListAdapter.UpdateAll();
		base.Open();
	}
}
