using System;
using UnityEngine;

[Serializable]
public class GlobalFlag
{
	[SerializeField]
	private long f;

	[SerializeField]
	private long l;

	[SerializeField]
	private int c;

	public DateTime FirstSet => new DateTime(f, DateTimeKind.Utc);

	public DateTime LastSet => new DateTime(l, DateTimeKind.Utc);

	public int TimesSet => c;

	public void SetFlag()
	{
		if (c == 0)
		{
			f = DateTime.UtcNow.Ticks;
		}
		l = DateTime.UtcNow.Ticks;
		c++;
	}

	public GlobalFlag()
	{
		c = 0;
		SetFlag();
	}
}
