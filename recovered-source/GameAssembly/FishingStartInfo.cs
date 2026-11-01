public class FishingStartInfo
{
	public FishingLocationData FishingLocation;

	public int FishingLengthMinutes;

	public FungusEnumerators.FishingJointDuelId FishingJointDuelId;

	public bool IsFishingJointDuel()
	{
		return FishingJointDuelId != FungusEnumerators.FishingJointDuelId.None;
	}

	public bool IsValid()
	{
		return FishingLocation != null;
	}
}
