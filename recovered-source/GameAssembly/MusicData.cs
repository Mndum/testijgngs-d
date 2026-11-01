using System.Collections.Generic;
using IC.AutomaticReference;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "Music/MusicData", order = 1)]
public class MusicData : SingletonScriptableObject<MusicData>
{
	public MusicWithInfo NeighbourSong => R.Music.Themes_AnnoyingNeighbour_Juice__Muffled_.Get();

	public MusicWithInfo Demise => R.Music.BadEnd_Demise.Get();

	public IEnumerable<string> GenerateCredits()
	{
		SerializableAutomaticReference<MusicArtist>[] allArtists = R.Artists.All;
		yield return "\nMusic by:";
		SerializableAutomaticReference<MusicArtist>[] array = allArtists;
		foreach (SerializableAutomaticReference<MusicArtist> serializableAutomaticReference in array)
		{
			yield return serializableAutomaticReference.Get().Info;
		}
		yield return "\nMusic:";
		SerializableAutomaticReference<MusicWithInfo>[] all = R.Music.All;
		foreach (SerializableAutomaticReference<MusicWithInfo> serializableAutomaticReference2 in all)
		{
			yield return serializableAutomaticReference2.Get().Info;
		}
	}
}
