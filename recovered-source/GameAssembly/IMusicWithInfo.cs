using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

public interface IMusicWithInfo
{
	string SongName { get; }

	string Info { get; }

	[MustDisposeResource]
	UniTask<AudioClipDisposableWrapper> GetAudioClip();

	bool HasText(string s);
}
