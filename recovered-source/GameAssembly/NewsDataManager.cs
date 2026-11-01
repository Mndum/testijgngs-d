using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NewsDataManager
{
	[Serializable]
	public class NewsData : ISerializationCallbackReceiver
	{
		public int newsId;

		public GameId gameId;

		public int newsSeed;

		public NewsData()
		{
			newsId = -1;
			newsSeed = 0;
			gameId = GameId.CreateVanilla(0);
		}

		public NewsData(GameId newsId, int newsSeed)
		{
			this.newsId = -1;
			gameId = newsId;
			this.newsSeed = newsSeed;
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			if (newsId != -1)
			{
				gameId = GameId.CreateVanilla(newsId);
				newsId = -1;
			}
		}
	}

	public enum NewsId
	{
		MainNews,
		SideNews1,
		SideNews2,
		SideNews3,
		OpinionNews
	}

	public NewsData mainNews;

	public NewsData sideNews1;

	public NewsData sideNews2;

	public NewsData sideNews3;

	public NewsData opinionNews;

	private NewsDataManager()
	{
	}

	public static NewsDataManager CreateInstance()
	{
		return new NewsDataManager
		{
			mainNews = new NewsData(),
			sideNews1 = new NewsData(),
			sideNews2 = new NewsData(),
			sideNews3 = new NewsData(),
			opinionNews = new NewsData()
		};
	}

	public void InitializeNewGame()
	{
		NewsId[] array = (NewsId[])Enum.GetValues(typeof(NewsId));
		foreach (NewsId type in array)
		{
			Randomize(type);
		}
	}

	public void Randomize(NewsId type)
	{
		int newsSeed = UnityEngine.Random.Range(0, 10000);
		switch (type)
		{
		case NewsId.MainNews:
		{
			DialogueChain randomPossibleUntaggedPrioritizeConditionsWithoutIgnored2 = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.MainNews.GetRandomPossibleUntaggedPrioritizeConditionsWithoutIgnored();
			mainNews = new NewsData(randomPossibleUntaggedPrioritizeConditionsWithoutIgnored2.Identificator, newsSeed);
			randomPossibleUntaggedPrioritizeConditionsWithoutIgnored2.Activate();
			break;
		}
		case NewsId.SideNews1:
		{
			DialogueChain randomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd3 = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.SideNews.GetRandomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd(new List<GameId> { sideNews2.gameId, sideNews3.gameId });
			sideNews1 = new NewsData(randomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd3.Identificator, newsSeed);
			randomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd3.Activate();
			break;
		}
		case NewsId.SideNews2:
		{
			DialogueChain randomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd2 = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.SideNews.GetRandomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd(new List<GameId> { sideNews1.gameId, sideNews3.gameId });
			sideNews2 = new NewsData(randomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd2.Identificator, newsSeed);
			randomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd2.Activate();
			break;
		}
		case NewsId.SideNews3:
		{
			DialogueChain randomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.SideNews.GetRandomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd(new List<GameId> { sideNews1.gameId, sideNews2.gameId });
			sideNews3 = new NewsData(randomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd.Identificator, newsSeed);
			randomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd.Activate();
			break;
		}
		case NewsId.OpinionNews:
		{
			DialogueChain randomPossibleUntaggedPrioritizeConditionsWithoutIgnored = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.OpinionNews.GetRandomPossibleUntaggedPrioritizeConditionsWithoutIgnored();
			opinionNews = new NewsData(randomPossibleUntaggedPrioritizeConditionsWithoutIgnored.Identificator, newsSeed);
			randomPossibleUntaggedPrioritizeConditionsWithoutIgnored.Activate();
			break;
		}
		default:
			Debug.LogError("Something weird in randomizing news " + type);
			break;
		}
	}

	public void HourPassed(int hourInDay)
	{
		switch (hourInDay)
		{
		case 12:
			Randomize(NewsId.MainNews);
			break;
		case 4:
			Randomize(NewsId.OpinionNews);
			break;
		case 7:
		case 19:
			Randomize(NewsId.SideNews1);
			break;
		case 3:
		case 15:
			Randomize(NewsId.SideNews2);
			break;
		case 11:
		case 23:
			Randomize(NewsId.SideNews3);
			break;
		}
	}

	[UsedInLua]
	public void Force(NewsId type, GameId newsGameId, int seed = -1)
	{
		int newsSeed = ((seed == -1) ? UnityEngine.Random.Range(0, 10000) : seed);
		DialogueChain byId;
		switch (type)
		{
		case NewsId.MainNews:
			byId = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.MainNews.GetById(newsGameId);
			mainNews = new NewsData(byId.Identificator, newsSeed);
			break;
		case NewsId.SideNews1:
			byId = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.SideNews.GetById(newsGameId);
			sideNews1 = new NewsData(byId.Identificator, newsSeed);
			break;
		case NewsId.SideNews2:
			byId = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.SideNews.GetById(newsGameId);
			sideNews2 = new NewsData(byId.Identificator, newsSeed);
			break;
		case NewsId.SideNews3:
			byId = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.SideNews.GetById(newsGameId);
			sideNews3 = new NewsData(byId.Identificator, newsSeed);
			break;
		case NewsId.OpinionNews:
			byId = SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.OpinionNews.GetById(newsGameId);
			opinionNews = new NewsData(byId.Identificator, newsSeed);
			break;
		default:
			Debug.LogError("Something weird in setting the news " + type);
			return;
		}
		byId.Activate();
	}
}
