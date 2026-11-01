using UnityEngine;

[CreateAssetMenu(fileName = "Artist", menuName = "Music/MusicArtist", order = 1)]
public class MusicArtist : ScriptableObject
{
	public string Name;

	public string AdditionalInfo;

	public string SpotifyLink;

	public string AppleMusicLink;

	public string YoutubeLink;

	public bool HasAllLinks
	{
		get
		{
			if (!string.IsNullOrWhiteSpace(YoutubeLink) && !string.IsNullOrWhiteSpace(AppleMusicLink))
			{
				return !string.IsNullOrWhiteSpace(SpotifyLink);
			}
			return false;
		}
	}

	public string ShortInfo => Name;

	public string Info
	{
		get
		{
			if (!string.IsNullOrWhiteSpace(AdditionalInfo))
			{
				return Name + " (" + AdditionalInfo + ")";
			}
			return Name;
		}
	}

	public string GetSpotifyUri()
	{
		if (string.IsNullOrWhiteSpace(SpotifyLink))
		{
			return "";
		}
		int num = SpotifyLink.LastIndexOf('/');
		if (num < 0 || num + 1 >= SpotifyLink.Length)
		{
			Debug.LogError("Invalid spotify link in " + Info);
			return "";
		}
		return "spotify:artist:" + SpotifyLink.Substring(num + 1);
	}
}
