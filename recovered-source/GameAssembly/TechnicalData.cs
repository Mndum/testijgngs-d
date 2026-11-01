using System;

[Serializable]
public class TechnicalData
{
	public long timeCreated;

	public SerializableGuid guid;

	public static TechnicalData CreateDefault()
	{
		return new TechnicalData
		{
			timeCreated = DateTime.UtcNow.Ticks,
			guid = Guid.NewGuid()
		};
	}
}
