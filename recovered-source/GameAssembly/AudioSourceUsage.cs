using System;
using UnityEngine;

public struct AudioSourceUsage(AudioSource audioSource) : IDisposable
{
	private bool _isValid = true;

	private AudioSource _audioSource = audioSource;

	public AudioSource Source => _audioSource;

	public bool IsValid => _isValid;

	public void EnsureAcquired(out AudioSource source)
	{
		if (!_isValid)
		{
			source = (_audioSource = SingletonMonoBehaviour<SoundSingleton>.Instance.GetFreeAudioSource());
			_isValid = true;
		}
		else
		{
			source = _audioSource;
		}
	}

	public void Dispose()
	{
		if (_isValid)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.ReturnToPool(_audioSource);
			_audioSource = null;
			_isValid = false;
		}
	}
}
