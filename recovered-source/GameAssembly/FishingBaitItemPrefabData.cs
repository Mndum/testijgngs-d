using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Interop;

public class FishingBaitItemPrefabData : LuaUtilities.ILuaTransferable, ICloneable<FishingBaitItemPrefabData>, ICloneable
{
	private Dictionary<FishingBaitType, float> _fishingBaitComposition;

	public Dictionary<FishingBaitType, float> FishingBaitComposition
	{
		get
		{
			return _fishingBaitComposition;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_fishingBaitComposition = value;
		}
	}

	public FishingBaitItemPrefabData(Dictionary<FishingBaitType, float> fishingBaitComposition)
	{
		FishingBaitComposition = fishingBaitComposition;
	}

	public static FishingBaitItemPrefabData CreateInstance(Dictionary<FishingBaitType, float> fishingBaitComposition)
	{
		return new FishingBaitItemPrefabData(fishingBaitComposition);
	}

	public static FishingBaitItemPrefabData CreateInstance(FishingBaitType fishingBaitType, float fishingBaitStrength)
	{
		return new FishingBaitItemPrefabData(new Dictionary<FishingBaitType, float> { [fishingBaitType] = fishingBaitStrength });
	}

	public string ToLuaString()
	{
		return "FishingBaitItemPrefabData.CreateInstance(" + FishingBaitComposition.ToLuaString((FishingBaitType x) => x.ToLuaString(), (float y) => y.ToLuaString()) + ")";
	}

	public FishingBaitItemPrefabData Clone()
	{
		FishingBaitItemPrefabData fishingBaitItemPrefabData = (FishingBaitItemPrefabData)MemberwiseClone();
		fishingBaitItemPrefabData.FishingBaitComposition = new Dictionary<FishingBaitType, float>(fishingBaitItemPrefabData.FishingBaitComposition);
		return fishingBaitItemPrefabData;
	}
}
