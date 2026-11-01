using UnityEngine;

public struct ReactionManagerData
{
	public InteractionMemoryEnum LastEvent;

	public int LastEventCount;

	public double LastSuccessfulReactionTime;

	public bool HasReactedRecently(double recentlyTimeSeconds = 12.5)
	{
		if (LastEvent != InteractionMemoryEnum.None)
		{
			return Time.timeAsDouble - LastSuccessfulReactionTime < recentlyTimeSeconds;
		}
		return false;
	}

	public void Reset()
	{
		LastEvent = InteractionMemoryEnum.None;
		LastEventCount = -1;
		LastSuccessfulReactionTime = Time.timeAsDouble;
	}

	public void LogDebugData()
	{
		string text = (Time.timeAsDouble - LastSuccessfulReactionTime).ToString("F2");
		Debug.Log($"LastEvent: {LastEvent} | " + $"Count: {LastEventCount} | " + "Last success: " + text + "s ago | " + $"Raw time: {LastSuccessfulReactionTime:F1} | '" + $"Reacted recently: {HasReactedRecently()}");
	}
}
