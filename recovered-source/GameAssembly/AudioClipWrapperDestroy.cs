using UnityEngine;

public class AudioClipWrapperDestroy : AudioClipDisposableWrapper
{
	public AudioClipWrapperDestroy(AudioClip audioClip)
		: base(audioClip)
	{
	}

	public override void Dispose()
	{
		if (AudioClip != null)
		{
			Object.Destroy(AudioClip);
			AudioClip = null;
		}
	}
}
