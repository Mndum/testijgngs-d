using System.IO;
using Melanchall.DryWetMidi.Core;
using UnityEngine;

[PreferBinarySerialization]
public class MidiSongAsset : ScriptableObject
{
	[HideInInspector]
	[SerializeField]
	private byte[] midiBytes;

	public void SetMidiFileData(byte[] bytes)
	{
		midiBytes = bytes;
	}

	public MidiFile GetMidiFile()
	{
		if (midiBytes == null)
		{
			Debug.LogError("MIDI file bytes not loaded.");
			return null;
		}
		return MidiFile.Read(new MemoryStream(midiBytes));
	}
}
