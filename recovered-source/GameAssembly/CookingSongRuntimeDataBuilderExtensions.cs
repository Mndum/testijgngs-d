using IC.AutomaticReference;
using Melanchall.DryWetMidi.Core;

public static class CookingSongRuntimeDataBuilderExtensions
{
	public static CookingSongRuntimeData.Builder ToCookingSongRuntimeDataBuilder(this IReference<MidiSongAsset> midiFile, CookingSong song)
	{
		return new CookingSongRuntimeData.Builder(midiFile.GetResource().GetMidiFile(), song);
	}

	public static CookingSongRuntimeData.Builder ToCookingSongRuntimeDataBuilder(this IReference<MidiFile> midiFile, CookingSong song)
	{
		return new CookingSongRuntimeData.Builder(midiFile.GetResource(), song);
	}
}
