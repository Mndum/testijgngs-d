using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingJournalFishEntry : AdvancedItemListPrefab
{
	[SerializeField]
	private TextMeshProUGUI fishInfo;

	[SerializeField]
	private TextMeshProUGUI historyInfo;

	[SerializeField]
	private Button mainButton;

	public override bool HasDefaultButton => true;

	public override Button GetDefaultButton()
	{
		return mainButton;
	}

	public void SetData(GameId fishItemGameId)
	{
		ItemPrefab itemById = ItemPrefab.ItemPrefabManager.GetItemById(fishItemGameId);
		FishItemPrefabData fishData = itemById.FishData;
		if (fishData == null)
		{
			Debug.LogError("NOT A FISH IN FISH LIST!!!!");
			return;
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			FishSave save = GameVariables.Current.fishingMinigameManager.GetSave(fishItemGameId);
			if (save.WasEverCaught)
			{
				stringBuilder.AppendLine(LOC.UI.Fishing_CaughtXTimes(save.TimesCaught));
				float num = fishData.DetermineFishPercentile(save.MinWeightCaught);
				stringBuilder.Append(LOC.UI.Fishing_MinWeight(save.MinWeightCaught));
				stringBuilder.Append(" (");
				stringBuilder.Append(LOC.UI.Fishing_SmallerThanPercent(1f - num));
				stringBuilder.Append(")");
				stringBuilder.AppendLine();
				float num2 = fishData.DetermineFishPercentile(save.MaxWeightCaught);
				stringBuilder.Append(LOC.UI.Fishing_MaxWeight(save.MaxWeightCaught));
				stringBuilder.Append(" (");
				stringBuilder.Append(LOC.UI.Fishing_BiggerThanPercent(num2));
				stringBuilder.Append(")");
				stringBuilder.AppendLine();
				if (FishingLocationDataManager.TryGetLocationData(save.LastCaughtLocationDataKey, out var fishingLocationData))
				{
					stringBuilder.AppendLine(LOC.UI.Fishing_LastCaught(fishingLocationData.Name));
				}
			}
			else
			{
				stringBuilder.AppendLine(LOC.UI.Fishing_NeverCaught);
			}
			historyInfo.SetText(stringBuilder);
			stringBuilder.Clear();
			if (save.WasEverCaught)
			{
				stringBuilder.TMPAppendHeaderSizeTag();
				stringBuilder.AppendLine(itemById.Name);
				stringBuilder.TMPAppendCloseSizeTag();
				stringBuilder.AppendLine(itemById.Description);
			}
			else
			{
				stringBuilder.TMPAppendHeaderSizeTag();
				stringBuilder.AppendLine(itemById.Name.PartiallyRedactString(4, '?'));
				stringBuilder.TMPAppendCloseSizeTag();
				stringBuilder.AppendLine("???");
			}
			fishInfo.SetText(stringBuilder);
		}
	}
}
