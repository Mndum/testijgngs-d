using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Interaction;

public static class MidiExtensions
{
	public static MetricTimeSpan MetricTimeSpanFromDoubleSeconds(double seconds)
	{
		return new MetricTimeSpan(TimeSpan.FromSeconds(seconds));
	}

	public static void CalculateMaxMinAverageBpm(this TempoMap tempoMap, long songLength, out double minBpm, out double maxBpm, out double averageBpm)
	{
		List<ValueChange<Tempo>> list = tempoMap.GetTempoChanges().ToList();
		if (list.Count == 0)
		{
			maxBpm = (minBpm = (averageBpm = Tempo.Default.BeatsPerMinute));
			return;
		}
		averageBpm = ((list[0].Time != 0L) ? ((double)list[0].Time * Tempo.Default.BeatsPerMinute) : 0.0);
		minBpm = 1.7976931348623157E+308;
		maxBpm = -1.7976931348623157E+308;
		for (int i = 0; i < list.Count; i++)
		{
			ValueChange<Tempo> valueChange = list[i];
			long num = ((i + 1 < list.Count) ? list[i].Time : songLength);
			double beatsPerMinute = valueChange.Value.BeatsPerMinute;
			long num2 = num - valueChange.Time;
			minBpm = Math.Min(minBpm, beatsPerMinute);
			maxBpm = Math.Max(maxBpm, beatsPerMinute);
			averageBpm += (double)num2 * beatsPerMinute;
		}
		averageBpm /= songLength;
	}
}
