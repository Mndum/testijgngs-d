using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Streamer
{
	public enum StreamerType
	{
		Generic,
		Gay,
		Gaming,
		Female,
		Furry,
		Disgusting,
		Frozen,
		XQC
	}

	public int seed;

	private static Dictionary<StreamerType, List<string>> descriptionData = new Dictionary<StreamerType, List<string>>
	{
		{
			StreamerType.Generic,
			new List<string> { "Just fucking", "Whore review", "Sexworker union meeting", "FAKE fire engine", "BJ while cooking", "Sex and more", "Naked poker", "Interacial, fuck you bigots", "Crazy bitch can't stop fucking!", "Ebony queen dicking" }
		},
		{
			StreamerType.Gay,
			new List<string>
			{
				"Buttplug review", "HIV test reveal", "New toys~", "1 stranger 1 blowjob", "How gay is your dad?", "Suicide statistics still up?", "Cute new cosplay", "Even I can't fit it in...", "Monster Cock impalement", "Deep dark fantasies",
				"10 guys one manhole"
			}
		},
		{
			StreamerType.Gaming,
			new List<string> { "MK speedrun while jerking off", "Dying cunt gameplay", "New Porn game", "Factorial Omega Online", "GT&A eRP", "Making celebrities in character creation", "Going to gold, vibrator on!", "RE3 watchalong" }
		},
		{
			StreamerType.Female,
			new List<string> { "NTR- hubby and his bff", "BMWF again <3", "Ball crushing", "Extra deepthroat", "Femdom", "Double fisting~", "Cute new cosplay", "JackCox Party gangbang", "Cheating on bf LIVE", "Daddy's daughter <3" }
		},
		{
			StreamerType.Furry,
			new List<string> { "Fursuit review", "Diaper time", "Kinky play with real lion", "AIDS epidemic in our community?", "Washing off cum from my suit", "Tail play", "Pawjob", "Fur stuck in peehole", "PETAA wants to take my dogs!?" }
		},
		{
			StreamerType.Disgusting,
			new List<string>
			{
				"Unprepared for anal? No problem", "Golden shower enjoyer", "Imagine the smelle...", "Swallowing up everything. EVERYTHING", "You puke you lose", "Is that too far?", "Taking a GIANT shit", "Watch quick before ban", "Don't like it dirty? Don't watch", "Extreme violence is so hot",
				"Until she passes out", "She was my real sister?!"
			}
		},
		{
			StreamerType.Frozen,
			new List<string> { "MK speedrun, juicers INSTABAN" }
		},
		{
			StreamerType.XQC,
			new List<string> { "POGMAN! ABSOLUTE GOD STREAMER", "BEST FUCKER AT EVERY POSITION", "WORLD RECORD AT FUCKING TONITE", "MY JUICER IN HERS JUICE-TANKS", "SEXLORD WINNING YET AGAIN", "BIG SHONZER UP HER ASS" }
		}
	};

	[field: SerializeField]
	public string Name { get; private set; }

	[field: SerializeField]
	public int EarliestHour { get; private set; }

	[field: SerializeField]
	public int LatestHour { get; private set; }

	[field: SerializeField]
	public int Followers { get; private set; }

	[field: SerializeField]
	public StreamerType Type { get; private set; }

	public string Description
	{
		get
		{
			UnityEngine.Random.State state = UnityEngine.Random.state;
			UnityEngine.Random.InitState(seed);
			try
			{
				List<string> value;
				return descriptionData.TryGetValue(Type, out value) ? value.GetRandom() : "Just fucking";
			}
			finally
			{
				UnityEngine.Random.state = state;
			}
		}
	}

	public int ViewerCount
	{
		get
		{
			int num = UnityEngine.Random.Range(-50, 50);
			UnityEngine.Random.State state = UnityEngine.Random.state;
			UnityEngine.Random.InitState(seed);
			int num2 = UnityEngine.Random.Range(-Followers / 5, Followers / 5);
			try
			{
				int num3 = Followers + num2 + num;
				return (num3 >= 0) ? num3 : 0;
			}
			finally
			{
				UnityEngine.Random.state = state;
			}
		}
	}

	public Streamer(string name, int earliestHour, int latestHour, int followers, StreamerType type)
	{
		Name = name;
		EarliestHour = earliestHour;
		LatestHour = latestHour;
		Followers = followers;
		Type = type;
		seed = UnityEngine.Random.Range(-2147483648, 2147483647);
	}

	public void SyncFollowers()
	{
		Followers += UnityEngine.Random.Range(-10, 50);
		if (Followers < 0)
		{
			Followers = 0;
		}
	}

	public bool ShouldBeStreaming(int hour)
	{
		if (EarliestHour <= LatestHour)
		{
			if (EarliestHour <= hour)
			{
				return hour <= LatestHour;
			}
			return false;
		}
		if (hour < EarliestHour)
		{
			return hour <= LatestHour;
		}
		return true;
	}

	public bool IsStreaming(int hour)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		UnityEngine.Random.InitState(seed);
		try
		{
			if (ShouldBeStreaming(hour))
			{
				return UnityEngine.Random.Range(0f, 1f) < 0.9f;
			}
			return UnityEngine.Random.Range(0f, 1f) > 0.9f;
		}
		finally
		{
			UnityEngine.Random.state = state;
		}
	}
}
