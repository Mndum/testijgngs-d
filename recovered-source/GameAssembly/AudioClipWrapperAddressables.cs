using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioClipWrapperAddressables : AudioClipDisposableWrapper
{
	private AsyncOperationHandle<AudioClip> _asyncOperationHandle;

	public AudioClipWrapperAddressables(AudioClip audioClip, AsyncOperationHandle<AudioClip> asyncOperationHandle)
		: base(audioClip)
	{
		_asyncOperationHandle = asyncOperationHandle;
	}

	public override void Dispose()
	{
		if (_asyncOperationHandle.IsValid())
		{
			Addressables.Release(_asyncOperationHandle);
		}
	}
}
