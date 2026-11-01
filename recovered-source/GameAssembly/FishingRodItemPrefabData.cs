using System;

public class FishingRodItemPrefabData : LuaUtilities.ILuaTransferable, ICloneable<FishingRodItemPrefabData>, ICloneable
{
	public float FishingRodSize01;

	public float FishingRodAcceleration01;

	public float ReelingSpeed01;

	public float MaxEffectiveFishWeightKg;

	private FishingRodItemPrefabData(float fishingRodSize01, float fishingRodAcceleration01, float reelingSpeed01, float maxEffectiveFishWeightKg)
	{
		FishingRodSize01 = fishingRodSize01;
		FishingRodAcceleration01 = fishingRodAcceleration01;
		ReelingSpeed01 = reelingSpeed01;
		MaxEffectiveFishWeightKg = maxEffectiveFishWeightKg;
	}

	public static FishingRodItemPrefabData CreateInstance(float fishingRodSize01, float fishingRodAcceleration01, float fishReelingSpeed01, float maxEffectiveFishWeightKg)
	{
		return new FishingRodItemPrefabData(fishingRodSize01, fishingRodAcceleration01, fishReelingSpeed01, maxEffectiveFishWeightKg);
	}

	public string ToLuaString()
	{
		return string.Format("{0}.{1}({2}, {3}, {4}, {5})", "FishingRodItemPrefabData", "CreateInstance", FishingRodSize01, FishingRodAcceleration01, ReelingSpeed01, MaxEffectiveFishWeightKg);
	}

	public FishingRodItemPrefabData Clone()
	{
		return (FishingRodItemPrefabData)MemberwiseClone();
	}
}
