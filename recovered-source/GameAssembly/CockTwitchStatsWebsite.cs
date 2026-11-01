using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CockTwitchStatsWebsite : Website
{
	private MyGraph followersGraph;

	private TextMeshProUGUI statsText;

	private void SyncData()
	{
		GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
		LinkedList<int> clearData = gameVariables.cockTwitchManager.FollowerMemory;
		IEnumerable<int> data = clearData.Append(gameVariables.followers);
		followersGraph.SetData(data, new MyGraph.GraphStyleContainer((float f) => Mathf.FloorToInt((float)clearData.Count - f).ToString(), (float f) => Mathf.CeilToInt(f).ToString(), 10f, -1, LOC.UI.cocktwitchdotic_DaysAgo, LOC.UI.cocktwitchdotic_Followers));
		if (GameVariables.Current.CanStartStream)
		{
			statsText.text = LOC.UI.cocktwitchdotic_Summary(gameVariables.followers, gameVariables.subs, GlobalFunctions.TimePeriod(gameVariables.streamedFor), GlobalFunctions.TimePeriod(gameVariables.longestStream));
		}
		else
		{
			statsText.text = LOC.UI.CockTwitch_AccountInactive;
		}
	}

	public override IEnumerator Open()
	{
		followersGraph = transform.Find("FollowersGraph").GetComponent<MyGraph>();
		statsText = transform.Find("Stats").GetComponentInChildren<TextMeshProUGUI>();
		SyncData();
		DisableAllChildren();
		yield return WaitForSecondCache.Seconds0_1;
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
