using System;
using System.Collections.Generic;
using IC.AutomaticReference;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Tools;
using UnityEngine;

public class CookingSongRuntimeData
{
	public class Builder
	{
		private MidiFile _midiFile;

		private CookingSong _relatedCookingSong;

		private TempoMap _songTempoMap;

		private double _songOffsetSeconds;

		private double _pixelsPerSecond = 640.0;

		private int _songVersion;

		private List<CookingSongNoteEvent.Builder> _notes = MyCollectionPool<CookingSongNoteEvent.Builder>.GetList();

		private List<MidiFile> _channels = MyCollectionPool<MidiFile>.GetList();

		public static readonly BarBeatFractionTimeSpan QuarterBeat = new BarBeatFractionTimeSpan(0L, 0.25);

		public static readonly BarBeatFractionTimeSpan HalfBeat = new BarBeatFractionTimeSpan(0L, 0.5);

		public TempoMap TempoMap => _songTempoMap;

		public MidiFile MidiFile => _midiFile;

		public Builder(MidiFile midiFile, CookingSong cookingSong)
		{
			_midiFile = midiFile;
			_channels.AddRange(_midiFile.SplitByChannel());
			_songTempoMap = _midiFile.GetTempoMap();
			_relatedCookingSong = cookingSong;
		}

		private bool TryGetChannel(int channelIndex, out MidiFile channel)
		{
			return _channels.TryGetValue(channelIndex, out channel);
		}

		public Builder ChannelToNotes(int channel, Func<Builder, int, Note, NoteButtonTypeEnum> buttonDeterminer, bool ignoreLength = false)
		{
			if (!TryGetChannel(channel, out var channel2))
			{
				Debug.LogError($"Trying to use a channel nr {channel}! Such channel doesn't exist in the midi file!");
				return this;
			}
			ICollection<Note> notes = channel2.GetNotes();
			int num = 0;
			foreach (Note item in notes)
			{
				double totalSeconds = TimeConverter.ConvertTo<MetricTimeSpan>(item.Time, _songTempoMap).TotalSeconds;
				_notes.Add(new CookingSongNoteEvent.Builder(buttonDeterminer(this, num, item), totalSeconds, ignoreLength ? totalSeconds : TimeConverter.ConvertTo<MetricTimeSpan>(item.EndTime, _songTempoMap).TotalSeconds));
				num++;
			}
			return this;
		}

		public Builder ChannelToNotesAllTheSame(int channel, NoteButtonTypeEnum button, bool ignoreLength = false)
		{
			if (!TryGetChannel(channel, out var channel2))
			{
				Debug.LogError($"Trying to use a channel nr {channel}! Such channel doesn't exist in the midi file!");
				return this;
			}
			foreach (Note note in channel2.GetNotes())
			{
				double totalSeconds = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, _songTempoMap).TotalSeconds;
				_notes.Add(new CookingSongNoteEvent.Builder(button, totalSeconds, ignoreLength ? totalSeconds : TimeConverter.ConvertTo<MetricTimeSpan>(note.EndTime, _songTempoMap).TotalSeconds));
			}
			return this;
		}

		public Builder WithVersion(int version)
		{
			_songVersion = version;
			return this;
		}

		public Builder WithPixelsPerSecondDifficulty(float difficulty)
		{
			_pixelsPerSecond = CookingGameplayUtilities.GetPixelsPerSecondsFromDifficulty(difficulty);
			return this;
		}

		public Builder WithSongOffset(double f)
		{
			_songOffsetSeconds = f;
			return this;
		}

		public CookingSongRuntimeData Build()
		{
			_notes.Sort((CookingSongNoteEvent.Builder a, CookingSongNoteEvent.Builder b) => a.StartTime.CompareTo(b.StartTime));
			for (int num = _notes.Count - 1; num > 0; num--)
			{
				if (Math.Abs(_notes[num].StartTime - _notes[num - 1].StartTime) < 0.0001)
				{
					_notes.RemoveAt(num);
				}
			}
			for (int num2 = 0; num2 < _notes.Count - 1; num2++)
			{
				CookingSongNoteEvent.Builder value = _notes[num2];
				CookingSongNoteEvent.Builder builder = _notes[num2 + 1];
				bool flag = false;
				if (value.IsDuration)
				{
					double num3 = builder.StartTime - value.EndTime;
					double totalSeconds = LengthConverter.ConvertTo<MetricTimeSpan>(QuarterBeat, MidiExtensions.MetricTimeSpanFromDoubleSeconds(builder.StartTime), _songTempoMap).TotalSeconds;
					if (num3 < totalSeconds)
					{
						value.EndTime = builder.StartTime - totalSeconds;
						flag = true;
					}
				}
				double totalSeconds2 = LengthConverter.ConvertTo<MetricTimeSpan>(HalfBeat, MidiExtensions.MetricTimeSpanFromDoubleSeconds(value.StartTime), _songTempoMap).TotalSeconds;
				if (value.Duration <= totalSeconds2)
				{
					value.EndTime = value.StartTime;
					flag = true;
				}
				if (flag)
				{
					_notes[num2] = value;
				}
			}
			List<CookingSongNoteEvent> list = MyCollectionPool<CookingSongNoteEvent>.GetList();
			list.EnsureCapacityAtLeast(_notes.Count);
			foreach (CookingSongNoteEvent.Builder note in _notes)
			{
				list.Add(note.Build());
			}
			MyCollectionPool<CookingSongNoteEvent.Builder>.ReleaseAndSetNull(ref _notes);
			MyCollectionPool<MidiFile>.ReleaseAndSetNull(ref _channels);
			return new CookingSongRuntimeData(_songVersion, _relatedCookingSong, _midiFile, _songTempoMap, list, _pixelsPerSecond, _songOffsetSeconds);
		}
	}

	private const double MinNoteDifference = 0.005;

	public readonly CookingSong RelatedCookingSong;

	public readonly TempoMap SongTempoMap;

	public readonly double SongOffsetSeconds;

	public readonly double SongLengthSeconds;

	public readonly double AverageBPM;

	public readonly double MinBPM;

	public readonly double MaxBPM;

	public readonly double PixelsPerSecond;

	public readonly int SongVersion;

	public readonly List<CookingSongNoteEvent> Notes;

	private readonly Dictionary<CookingGameplayModifiers, CookingSongDifficulty> _cachedDifficulties = new Dictionary<CookingGameplayModifiers, CookingSongDifficulty>();

	private CookingGameplayScoreData _perfectScore;

	private CookingSongRuntimeData(int version, CookingSong cookingSong, MidiFile midiFile, TempoMap songTempoMap, List<CookingSongNoteEvent> notes, double pixelsPerSecond, double songOffsetSeconds = 0.0)
	{
		SongVersion = version;
		RelatedCookingSong = cookingSong;
		SongOffsetSeconds = Math.Clamp(songOffsetSeconds, -0.5, 0.5);
		PixelsPerSecond = pixelsPerSecond;
		SongTempoMap = songTempoMap;
		SongLengthSeconds = midiFile.GetDuration<MetricTimeSpan>().TotalSeconds;
		songTempoMap.CalculateMaxMinAverageBpm(midiFile.GetDuration<MidiTimeSpan>().TimeSpan, out MinBPM, out MaxBPM, out AverageBPM);
		Notes = notes;
	}

	private static double DifferenceSafe(double a, double b)
	{
		return Math.Max(Math.Abs(a - b), 0.005);
	}

	private static CookingSongDifficulty DetermineDifficulty(CookingSongRuntimeData runtimeData, CookingGameplayModifiers gameplayModifiers)
	{
		List<CookingSongNoteEvent> notes = runtimeData.Notes;
		CookingSongDifficulty result = new CookingSongDifficulty
		{
			SpeedDifficulty = CookingGameplayUtilities.GetSpeedDifficultyFromPixelsPerSecond(runtimeData.PixelsPerSecond)
		};
		if (notes.Count <= 1)
		{
			return result;
		}
		double num = gameplayModifiers.DetermineTargetPitch();
		for (int i = 0; i < notes.Count - 1; i++)
		{
			double num2 = 0.01;
			CookingSongNoteEvent cookingSongNoteEvent = notes[i];
			int num3 = Mathf.Min(i + 30, notes.Count - 1);
			double num4 = cookingSongNoteEvent.EndTime + 2.0;
			for (int j = i + 1; j <= num3; j++)
			{
				CookingSongNoteEvent cookingSongNoteEvent2 = notes[j];
				if (cookingSongNoteEvent2.StartTime > num4)
				{
					break;
				}
				double num5 = num / DifferenceSafe(cookingSongNoteEvent2.StartTime, cookingSongNoteEvent.EndTime);
				if (cookingSongNoteEvent2.IsDuration && cookingSongNoteEvent2.EndTime < num4)
				{
					num5 += num / DifferenceSafe(cookingSongNoteEvent2.EndTime, cookingSongNoteEvent.EndTime) * 0.05;
				}
				num2 += num5;
			}
			if (num2 > result.MaxNoteDifficulty)
			{
				result.MaxNoteDifficulty = num2;
				result.MaxNoteDifficultyTime = cookingSongNoteEvent.StartTime;
			}
			result.AverageNoteDifficulty += num2;
		}
		result.AverageNoteDifficulty /= notes.Count;
		result.SongDifficulty = result.AverageNoteDifficulty * 0.5 + result.MaxNoteDifficulty * 0.1 + Math.Max(result.SpeedDifficulty * 0.1 - 0.5, 0.0);
		return result;
	}

	public CookingSongDifficulty GetDefaultDifficulty()
	{
		return GetDifficulty(CookingGameplayModifiers.NoMods);
	}

	public CookingSongDifficulty GetDifficulty(CookingGameplayModifiers modifiers)
	{
		if (_cachedDifficulties.TryGetValue(modifiers, out var value))
		{
			return value;
		}
		return _cachedDifficulties[modifiers] = DetermineDifficulty(this, modifiers);
	}

	public double GetSongLength(CookingGameplayModifiers modifiers)
	{
		return SongLengthSeconds / (double)modifiers.DetermineTargetPitch();
	}

	public void GetSongLengthDiscreet(CookingGameplayModifiers modifiers, out int minutes, out int seconds)
	{
		int num = (int)Math.Ceiling(GetSongLength(modifiers));
		minutes = num / 60;
		seconds = num % 60;
	}

	public CookingGameplayScoreData GetPerfectScoreReadonly()
	{
		return _perfectScore ?? (_perfectScore = CookingGameplayScoreData.CreatePerfect(RelatedCookingSong));
	}

	public static Builder StartBuilder(IReference<MidiFile> midiFile, CookingSong cookingSong)
	{
		return new Builder(midiFile.GetResource(), cookingSong);
	}
}
