using System.Collections;
using TMPro;
using UnityEngine;

public class CockTwitchWebsite : Website
{
	public GameObject StreamerInfoPrefab;

	private Transform LiveHolder;

	private void SetLiveData(Transform liveData, CockTwitchManager.StreamInfo streamInfo)
	{
		liveData.Find("Info").GetComponent<TextMeshProUGUI>().text = streamInfo.streamer.Name + "\n<size=59%>" + streamInfo.streamer.Description;
		liveData.Find("Views").GetComponent<TextMeshProUGUI>().text = CockTwitchManager.FormatViewers(streamInfo.Viewers);
	}

	private void GenerateViewerInfo()
	{
		foreach (CockTwitchManager.StreamInfo activeStreamer in SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cockTwitchManager.GetActiveStreamers(14))
		{
			SetLiveData(Object.Instantiate(StreamerInfoPrefab, LiveHolder).transform, activeStreamer);
		}
	}

	public void GoLiveButton()
	{
		if (GameVariables.Current.CanStartStream)
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.cocktwitchdotic_StartStreamPrompt, () =>
			{
				_ = SingletonMonoBehaviour<GameUtilities>.Instance;
				SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Sex_Stream_Missionary);
			});
		}
		else
		{
			UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.CockTwitch_CantStartStreamAccountInactive);
		}
	}

	public override IEnumerator Open()
	{
		LiveHolder = transform.Find("LiveHolder");
		GenerateViewerInfo();
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
