using System;
using System.Collections.Generic;
using System.Text;
using MoonSharp.Interpreter.Interop;
using UnityEngine;

public class FishPreferences : LuaUtilities.ILuaTransferable, ICloneable<FishPreferences>, ICloneable
{
	private Dictionary<GameId, float> _specificFishingBaitItemPreference = new Dictionary<GameId, float>();

	private Dictionary<FishingBaitType, float> _fishingBaitTypesPreference = new Dictionary<FishingBaitType, float>();

	private int _mostActiveTimeHour;

	private int _mostActiveHourRange;

	private float _mostActiveHourStrength01;

	public static readonly FishPreferences PredatoryFishPreferences = CreateInstance(new Dictionary<FishingBaitType, float>
	{
		{
			FishingBaitType.WormBait,
			0.5f
		},
		{
			FishingBaitType.InsectBait,
			0.5f
		},
		{
			FishingBaitType.MeatBait,
			1f
		},
		{
			FishingBaitType.FishBait,
			1f
		},
		{
			FishingBaitType.LiveFishBait,
			1f
		},
		{
			FishingBaitType.PlantBait,
			0.3f
		},
		{
			FishingBaitType.CrustaceanBait,
			0.8f
		},
		{
			FishingBaitType.MolluskBait,
			0.1f
		}
	}, new Dictionary<GameId, float>());

	public static readonly FishPreferences BottomFeedersAndScavengersPreferences = CreateInstance(new Dictionary<FishingBaitType, float>
	{
		{
			FishingBaitType.WormBait,
			1f
		},
		{
			FishingBaitType.InsectBait,
			0.1f
		},
		{
			FishingBaitType.MeatBait,
			0.1f
		},
		{
			FishingBaitType.FishBait,
			0.1f
		},
		{
			FishingBaitType.CrustaceanBait,
			0.8f
		},
		{
			FishingBaitType.MolluskBait,
			0.1f
		}
	}, new Dictionary<GameId, float>());

	public static readonly FishPreferences BenthicFeedersPreferences = CreateInstance(new Dictionary<FishingBaitType, float>
	{
		{
			FishingBaitType.FishBait,
			1f
		},
		{
			FishingBaitType.LiveFishBait,
			0.5f
		},
		{
			FishingBaitType.CrustaceanBait,
			1f
		},
		{
			FishingBaitType.MolluskBait,
			1f
		}
	}, new Dictionary<GameId, float>());

	public static readonly FishPreferences OmnivorousFishPreferences = CreateInstance(new Dictionary<FishingBaitType, float>
	{
		{
			FishingBaitType.WormBait,
			0.7f
		},
		{
			FishingBaitType.InsectBait,
			0.7f
		},
		{
			FishingBaitType.MeatBait,
			0.3f
		},
		{
			FishingBaitType.FishBait,
			0.3f
		},
		{
			FishingBaitType.LiveFishBait,
			0.1f
		},
		{
			FishingBaitType.PlantBait,
			0.5f
		},
		{
			FishingBaitType.CrustaceanBait,
			0.5f
		},
		{
			FishingBaitType.MolluskBait,
			0.5f
		}
	}, new Dictionary<GameId, float>());

	public Dictionary<FishingBaitType, float> FishingBaitTypesPreference
	{
		get
		{
			return _fishingBaitTypesPreference;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_fishingBaitTypesPreference = value ?? new Dictionary<FishingBaitType, float>();
		}
	}

	public Dictionary<GameId, float> SpecificFishingBaitItemPreference
	{
		get
		{
			return _specificFishingBaitItemPreference;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_specificFishingBaitItemPreference = value ?? new Dictionary<GameId, float>();
		}
	}

	public int MostActiveTimeHour
	{
		get
		{
			return _mostActiveTimeHour;
		}
		set
		{
			_mostActiveTimeHour = Mathf.Clamp(value, 0, 23);
		}
	}

	public int MostActiveHourRange
	{
		get
		{
			return _mostActiveHourRange;
		}
		set
		{
			_mostActiveHourRange = Mathf.Clamp(value, 0, 12);
		}
	}

	public float MostActiveHourStrength01
	{
		get
		{
			return _mostActiveHourStrength01;
		}
		set
		{
			_mostActiveHourStrength01 = Mathf.Clamp(value, 0f, 1f);
		}
	}

	public static FishPreferences CreateInstance(Dictionary<FishingBaitType, float> fishingBaitTypesPreference, Dictionary<GameId, float> specificFishingBaitItemPreference)
	{
		return new FishPreferences
		{
			FishingBaitTypesPreference = fishingBaitTypesPreference,
			SpecificFishingBaitItemPreference = specificFishingBaitItemPreference
		};
	}

	public static FishPreferences CreateInstance()
	{
		return new FishPreferences();
	}

	public float DetermineActiveHourModifier(int currentHour)
	{
		if (MostActiveHourStrength01 <= 0f)
		{
			return 1f;
		}
		int num = Math.Abs(currentHour - MostActiveTimeHour);
		int num2 = Mathf.Min(num, 24 - num);
		if (num2 == 0)
		{
			return 1f;
		}
		if (num2 <= MostActiveHourRange)
		{
			float num3 = 1f - (float)num2 / (float)MostActiveHourRange;
			float b = num3 * num3;
			return Mathf.Lerp(1f, b, MostActiveHourStrength01);
		}
		return 1f - MostActiveHourStrength01;
	}

	public string ToLuaString()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine("(function()");
			stringBuilder.AppendLine("local fpd = FishPreferences.CreateInstance()");
			stringBuilder.AppendLine("fpd.FishingBaitTypesPreference = " + FishingBaitTypesPreference.ToLuaString((FishingBaitType x) => x.ToLuaString(), (float x) => x.ToLuaString()));
			stringBuilder.AppendLine("fpd.SpecificFishingBaitItemPreference = " + SpecificFishingBaitItemPreference.ToLuaString((GameId x) => x.ToLuaString(), (float x) => x.ToLuaString()));
			stringBuilder.AppendLine("fpd.MostActiveTimeHour = " + MostActiveTimeHour.ToLuaString() + " -- 0-23 Hour the fish is most active at.");
			stringBuilder.AppendLine("fpd.MostActiveHourRange = " + MostActiveHourRange.ToLuaString() + " -- 0-12 How many hours of difference does the most active hour allow");
			stringBuilder.AppendLine("fpd.MostActiveHourStrength01 = " + MostActiveHourStrength01.ToLuaString() + " -- 0-1 float value meaning how strong the preference of the \"most active time\" is.");
			stringBuilder.Append("\treturn fpd end)()");
			return stringBuilder.ToString();
		}
	}

	public FishPreferences Clone()
	{
		FishPreferences fishPreferences = (FishPreferences)MemberwiseClone();
		fishPreferences.FishingBaitTypesPreference = new Dictionary<FishingBaitType, float>(FishingBaitTypesPreference);
		fishPreferences.SpecificFishingBaitItemPreference = new Dictionary<GameId, float>(SpecificFishingBaitItemPreference);
		return fishPreferences;
	}
}
