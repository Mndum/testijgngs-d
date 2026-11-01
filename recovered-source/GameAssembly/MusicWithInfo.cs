using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "Music", menuName = "Music/Music", order = 1)]
public class MusicWithInfo : ScriptableObject, IMusicWithInfo
{
	public enum MusicGroupName
	{
		Volume1,
		Volume2,
		Volume3,
		Volume4,
		Volume5,
		Other
	}

	public List<MusicArtist> Artists;

	public string Name;

	[SerializeField]
	private AssetReferenceT<AudioClip> ClipReference;

	public MusicGroupName Group = MusicGroupName.Volume5;

	public int Order;

	[TextArea]
	public string Notes;

	public string ArtistsInfo
	{
		get
		{
			if (Artists != null && Artists.Count != 0)
			{
				return string.Join(", ", Artists.Select((MusicArtist t) => t.ShortInfo));
			}
			return "Unknown";
		}
	}

	public string NameInfo
	{
		get
		{
			if (!string.IsNullOrWhiteSpace(Name))
			{
				return Name;
			}
			return "No song";
		}
	}

	public string Info => ArtistsInfo + " - " + NameInfo;

	public string SongName => Name;

	public AssetReferenceT<AudioClip> GetClipReference()
	{
		return ClipReference;
	}

	public async UniTask<AudioClipDisposableWrapper> GetAudioClip()
	{
		AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(ClipReference);
		await handle;
		return new AudioClipWrapperAddressables(handle.Result, handle);
	}

	public bool HasText(string s)
	{
		if (Name.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
		{
			return true;
		}
		foreach (MusicArtist artist in Artists)
		{
			if (artist.Name.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
			{
				return true;
			}
		}
		return false;
	}
}
