using UnityEngine;

public class SimpleEventMemory : EventMemory
{
	public float Length { get; protected set; }

	public float LastLength { get; protected set; }

	public float RecentTime { get; set; } = 3f;

	public override void FireSingle()
	{
		Length = 0f;
		LastLength = Length;
	}

	public override void Update()
	{
		Length += Time.fixedDeltaTime;
	}

	public override void Reset()
	{
		Length = 0f;
		LastLength = -3.4028235E+38f;
	}

	public bool IsRecent()
	{
		if (LastLength >= 0f)
		{
			return Length < RecentTime;
		}
		return false;
	}

	public bool IsRecent(float recentTime)
	{
		if (LastLength >= 0f)
		{
			return Length < recentTime;
		}
		return false;
	}
}
