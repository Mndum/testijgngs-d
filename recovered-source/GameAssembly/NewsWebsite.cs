using System;
using System.Collections;
using IC.AutomaticReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsWebsite : Website
{
	[Serializable]
	private class NewsHolder
	{
		[SerializeField]
		public Image image;

		[SerializeField]
		public TextMeshProUGUI text;

		private Sprite GetSprite(DialogueChain newsContainer, NewsDataManager.NewsData newsData)
		{
			if (newsContainer.TryGetVariable("image", out string output))
			{
				output = output.Trim();
				if (R.NewsImages.RelativePathToFirst.TryGetValue(output, out var value))
				{
					return value.Get();
				}
				Debug.LogError("News image \"" + output + "\" not found! Falling back!");
			}
			SerializableAutomaticReference<Sprite>[] all = R.NewsImages.All;
			int num = newsData.newsSeed % all.Length;
			return all[num].Get();
		}

		public void SetNews(DialogueChainContainer manager, NewsDataManager.NewsData newsData)
		{
			DialogueChain byId = manager.GetById(newsData.gameId);
			text.text = byId.GetText(newsData.newsSeed);
			image.sprite = GetSprite(byId, newsData);
		}
	}

	[SerializeField]
	private NewsHolder mainNews;

	[SerializeField]
	private NewsHolder sideNews1;

	[SerializeField]
	private NewsHolder sideNews2;

	[SerializeField]
	private NewsHolder sideNews3;

	[SerializeField]
	private NewsHolder opinionsNews;

	public void CreateNews()
	{
		mainNews.SetNews(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.MainNews, SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.newsDataManager.mainNews);
		sideNews1.SetNews(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.SideNews, SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.newsDataManager.sideNews1);
		sideNews2.SetNews(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.SideNews, SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.newsDataManager.sideNews2);
		sideNews3.SetNews(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.SideNews, SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.newsDataManager.sideNews3);
		opinionsNews.SetNews(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.OpinionNews, SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.newsDataManager.opinionNews);
	}

	public override IEnumerator Open()
	{
		CreateNews();
		DisableAllChildren();
		yield return WaitForSecondCache.Seconds0_1;
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
