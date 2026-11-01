using System.Globalization;
using UnityEngine;

public class SusModifier : LuaUtilities.ILuaTransferable
{
	public SusArea Area { get; set; }

	public float Modifier { get; set; }

	private SusModifier(SusArea area, float modifier)
	{
		Area = area;
		Modifier = Mathf.Clamp01(modifier);
	}

	public static SusModifier CreateInstance(SusArea area, float coverPercentage)
	{
		return new SusModifier(area, coverPercentage);
	}

	public SusModifier Clone()
	{
		return (SusModifier)MemberwiseClone();
	}

	public string ToLuaString()
	{
		return "SusModifier.CreateInstance(" + Area.ToLuaString() + ", " + Modifier.ToString(CultureInfo.InvariantCulture) + ")";
	}
}
