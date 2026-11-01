public class TrashCatchEntry : FishingCatchEntry
{
	public readonly GameId TrashItemGameId;

	private float _rawCatchOdds;

	public override bool ShownInTips => false;

	public TrashCatchEntry(float catchCatchOdds, GameId trashItemGameId)
	{
		_rawCatchOdds = catchCatchOdds;
		TrashItemGameId = trashItemGameId;
	}

	public ItemPrefab GetTrashItemPrefab()
	{
		return ItemPrefab.ItemPrefabManager.GetItemById(TrashItemGameId);
	}

	public override FishingBite ConvertToBite(float throwValue, FishingGameStats fishingGameStats, FishingLocationData fishingLocationData)
	{
		return new FishingBite(1f, FishingCatchBehaviourType.Rock, this, new Item[1] { Item.CreateRandomItem(TrashItemGameId) });
	}

	public override bool CalculateOdds(FishingGameStats fishingGameStats, int currentHour, out float odds)
	{
		odds = _rawCatchOdds;
		return true;
	}

	public override string Describe()
	{
		return GetTrashItemPrefab().Name;
	}
}
