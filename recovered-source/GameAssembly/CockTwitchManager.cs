using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class CockTwitchManager : GameVariablesSubClass, ISerializationCallbackReceiver
{
	public class StreamInfo
	{
		public Streamer streamer;

		public int Viewers;
	}

	public class CocktwitchEmailSubReadEventTarget : EmailEventTarget
	{
		public override bool Start(NewEmail parent)
		{
			if (parent.PreParsedSpecialValues.TryGetValue(2, out var value) && value is int amount)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddMoney(amount);
			}
			return false;
		}
	}

	[SerializeField]
	private List<Streamer> _streamers;

	private float[] _viewerShipByHour = new float[25]
	{
		1.2f, 0.8f, 0.5f, 0.38f, 0.3f, 0.4f, 0.7f, 0.8f, 1f, 1.01f,
		1.02f, 1.03f, 1.04f, 1.1f, 1.11f, 1.12f, 1.1f, 1f, 0.9f, 0.95f,
		1.05f, 1.2f, 1.3f, 1.25f, 1f
	};

	private static int MemorySize = 13;

	private LinkedList<int> _followerMemory;

	[SerializeField]
	private List<int> followerMemorySerialize;

	public LinkedList<int> FollowerMemory
	{
		get
		{
			return _followerMemory ?? (_followerMemory = new LinkedList<int>(followerMemorySerialize));
		}
		private set
		{
			_followerMemory = value;
		}
	}

	public static CockTwitchManager CreateInstance()
	{
		CockTwitchManager cockTwitchManager = new CockTwitchManager();
		cockTwitchManager._streamers = new List<Streamer>();
		cockTwitchManager.FollowerMemory = new LinkedList<int>();
		for (int i = 0; i < MemorySize; i++)
		{
			cockTwitchManager.FollowerMemory.AddLast(0);
		}
		return cockTwitchManager;
	}

	public static string FormatViewers(int num)
	{
		if (num >= 100000000)
		{
			return (num / 1000000).ToString("#,0M");
		}
		if (num >= 10000000)
		{
			return (num / 1000000).ToString("0.#") + "M";
		}
		if (num >= 100000)
		{
			return (num / 1000).ToString("#,0K");
		}
		if (num >= 10000)
		{
			return (num / 1000).ToString("0.#") + "K";
		}
		return num.ToString("#,0");
	}

	public void InitializeNewGame()
	{
		List<char> collection = new List<char> { 'c', 'd', 'i', 't', 'd', 'e', 'v' };
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			for (int i = 0; i < 5; i++)
			{
				stringBuilder.Append(char.ToLower(StringUtils.RandomLetter()));
				stringBuilder.Append(StringUtils.RandomLetter());
				stringBuilder.Append(collection.GetRandom());
				_streamers.Add(new Streamer(stringBuilder.ToString(), UnityEngine.Random.Range(0, 24), UnityEngine.Random.Range(0, 24), UnityEngine.Random.Range(20, 2000), Streamer.StreamerType.Gaming));
			}
			_streamers.Add(new Streamer("Frozen", 17, 22, 2412, Streamer.StreamerType.Frozen));
			_streamers.Add(new Streamer("Pokerino", 15, 21, 1351, Streamer.StreamerType.Disgusting));
			_streamers.Add(new Streamer("CockyPoki", 13, 16, 3562, Streamer.StreamerType.Female));
			_streamers.Add(new Streamer("SodaCucking", 16, 23, 2671, Streamer.StreamerType.Furry));
			_streamers.Add(new Streamer("Hashan", 23, 8, 666, Streamer.StreamerType.Gay));
			_streamers.Add(new Streamer("Shinobi", 15, 18, 4721, Streamer.StreamerType.Gaming));
			_streamers.Add(new Streamer("PVC", 22, 10, 5514, Streamer.StreamerType.XQC));
			_streamers.Add(new Streamer("4Cummer", 12, 18, 721, Streamer.StreamerType.Gay));
			_streamers.Add(new Streamer("CockTyler69 1", 22, 4, 3152, Streamer.StreamerType.Gay));
		}
	}

	public List<StreamInfo> GetActiveStreamers(int count = -1)
	{
		int currentHour = RelatedGameVariables.Hour;
		if (count < 0)
		{
			return (from t in _streamers
				where t.IsStreaming(currentHour)
				select new StreamInfo
				{
					streamer = t,
					Viewers = t.ViewerCount
				} into t
				orderby t.Viewers descending
				select t).ToList();
		}
		return (from t in _streamers
			where t.IsStreaming(currentHour)
			select new StreamInfo
			{
				streamer = t,
				Viewers = t.ViewerCount
			} into t
			orderby t.Viewers descending
			select t).Take(count).ToList();
	}

	public List<Streamer> GetTopStreamers(int count)
	{
		if (!RelatedGameVariables.CanStartStream)
		{
			return _streamers.OrderByDescending((Streamer t) => t.Followers).Take(count).ToList();
		}
		return (from t in _streamers.Append(new Streamer("Anon", 0, 24, RelatedGameVariables.followers, Streamer.StreamerType.Gaming))
			orderby t.Followers descending
			select t).Take(count).ToList();
	}

	public void MinutePassed()
	{
		int time = RelatedGameVariables.time;
		if (time % 17 == 0)
		{
			for (int i = 0; i < _streamers.Count / 3; i++)
			{
				Streamer streamer = _streamers[UnityEngine.Random.Range(0, _streamers.Count)];
				streamer.seed = UnityEngine.Random.Range(-2147483648, 2147483647);
				streamer.SyncFollowers();
			}
		}
		if (time % 60 != 0)
		{
			return;
		}
		foreach (Streamer streamer2 in _streamers)
		{
			streamer2.SyncFollowers();
		}
	}

	public void EndOfDay()
	{
		if (RelatedGameVariables.CanStartStream)
		{
			int followers = RelatedGameVariables.followers;
			int subs = RelatedGameVariables.subs;
			float num = (float)Math.Tanh((double)RelatedGameVariables.TimeSinceLastStream / 2880.0);
			float value = UnityEngine.Random.value;
			float num2 = 0f;
			if (value < num)
			{
				num2 = UnityEngine.Random.value * 0.1f;
			}
			else
			{
				num2 = ((!(value < num * 2f)) ? (UnityEngine.Random.value * 0.01f) : (UnityEngine.Random.value * 0.05f));
			}
			subs = Mathf.RoundToInt((float)subs * Mathf.Clamp01(1f - num2));
			subs -= UnityEngine.Random.Range(1, 10);
			followers = Mathf.RoundToInt((float)followers * Mathf.Clamp01(1f - num2 * 0.2f));
			followers -= UnityEngine.Random.Range(1, 10);
			if (subs > followers)
			{
				subs = GlobalFunctions.MoveTowards(subs, followers, 10);
			}
			RelatedGameVariables.subs = Mathf.Max(subs, 0);
			RelatedGameVariables.followers = Mathf.Max(followers, 0);
			if (RelatedGameVariables.subs > 0 && (RelatedGameVariables.DayOfTheWeek == 0 || RelatedGameVariables.DayOfTheWeek == 3))
			{
				int num3 = RelatedGameVariables.subs * 8;
				RelatedGameVariables.AddEmail(EmailPrefabManager.GetEmail(EmailPrefabManager.CockTwitchEmail, RelatedGameVariables.subs.ToString(), 8.ToString(), num3.ToString()));
			}
		}
		else
		{
			RelatedGameVariables.followers = 0;
			RelatedGameVariables.subs = 0;
		}
		AddFollowerMemory(RelatedGameVariables.followers);
	}

	private float CalculatePeak(int hour, int peak, float area, float strength)
	{
		int num = Math.Abs(peak - hour);
		num = ((num < 12) ? num : (24 - num));
		float num2 = (float)num / area;
		if (num2 < 1f)
		{
			num2 = 1f - num2;
			return strength * num2;
		}
		return 0f;
	}

	public float CalculateViewerShipTimeMultiplier(int dayOfTheWeek, int hour)
	{
		float num = _viewerShipByHour[hour];
		switch (dayOfTheWeek)
		{
		case 0:
			num += CalculatePeak(hour, 4, 5f, -0.2f);
			break;
		case 4:
			num += CalculatePeak(hour, 16, 2f, 0.5f);
			break;
		case 5:
			num += CalculatePeak(hour, 13, 5f, 0.3f);
			break;
		case 6:
			num += CalculatePeak(hour, 15, 4f, 0.3f);
			num += CalculatePeak(hour, 23, 1f, 0.3f);
			break;
		}
		return num;
	}

	public void OnBeforeSerialize()
	{
		followerMemorySerialize = followerMemorySerialize.ClearOrCreate();
		followerMemorySerialize.AddRange(FollowerMemory);
	}

	public void OnAfterDeserialize()
	{
		FollowerMemory = new LinkedList<int>(followerMemorySerialize);
	}

	private void AddFollowerMemory(int value)
	{
		FollowerMemory.AddLast(value);
		if (FollowerMemory.Count > MemorySize)
		{
			FollowerMemory.RemoveFirst();
		}
	}
}
