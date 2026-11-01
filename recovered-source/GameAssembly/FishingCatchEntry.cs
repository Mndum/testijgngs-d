public abstract class FishingCatchEntry
{
	public abstract bool ShownInTips { get; }

	public abstract FishingBite ConvertToBite(float throwValue, FishingGameStats fishingGameStats, FishingLocationData fishingLocationData);

	public abstract bool CalculateOdds(FishingGameStats fishingGameStats, int currentHour, out float odds);

	public abstract string Describe();
}
