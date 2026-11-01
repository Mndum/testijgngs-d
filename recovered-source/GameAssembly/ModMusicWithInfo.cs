using System;
using Cysharp.Threading.Tasks;

public class ModMusicWithInfo : IMusicWithInfo
{
	private ModFileBase SourceModFile;

	public string SongName { get; }

	public string Author { get; }

	public string Info => SongName + " - " + Author;

	public ModMusicWithInfo(ModFileBase sourceModFile, string songName, string author)
	{
		SourceModFile = sourceModFile;
		SongName = songName;
		Author = author;
	}

	public UniTask<AudioClipDisposableWrapper> GetAudioClip()
	{
		return new UniTask<AudioClipDisposableWrapper>(new AudioClipWrapperDestroy(SourceModFile.LoadAsAudioClip()));
	}

	public bool HasText(string s)
	{
		if (SongName.IndexOf(s, StringComparison.OrdinalIgnoreCase) > 0)
		{
			return true;
		}
		if (Author.IndexOf(s, StringComparison.OrdinalIgnoreCase) > 0)
		{
			return true;
		}
		return false;
	}
}
