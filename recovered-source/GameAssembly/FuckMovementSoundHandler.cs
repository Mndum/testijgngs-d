using System;
using UnityEngine;

public class FuckMovementSoundHandler
{
	private bool _hasAudioSource;

	private AudioSourceUsage _penetrationSoundAudioSourceUsage;

	private float _smoothedAbsDelta;

	private float _pitchOffset;

	private float _lastFixedUpdateInsertion;

	private Func<AudioClip> _randomClipProvider;

	public float SmoothedAbsDelta => _smoothedAbsDelta;

	public FuckMovementSoundHandler(Func<AudioClip> randomClipProvider)
	{
		_randomClipProvider = randomClipProvider;
		Reset();
	}

	public void Reset()
	{
		_pitchOffset = 0f;
		_lastFixedUpdateInsertion = -1f;
		_smoothedAbsDelta = 0f;
		_penetrationSoundAudioSourceUsage.Dispose();
	}

	public void FixedUpdate(bool isInside, float insertion)
	{
		float sfxVolume = SoundSingleton.SfxVolume;
		_penetrationSoundAudioSourceUsage.EnsureAcquired(out var source);
		if (isInside)
		{
			float target = Mathf.Abs(_lastFixedUpdateInsertion - insertion);
			if (!source.isPlaying && (double)UnityEngine.Random.value > 0.7)
			{
				source.clip = _randomClipProvider();
				source.Play();
				_pitchOffset = UnityEngine.Random.Range(-0.1f, 0.1f);
			}
			_smoothedAbsDelta = GlobalFunctions.SmoothLerpEaseOutDeltaT(_smoothedAbsDelta, target, 0.075f);
			source.volume = _smoothedAbsDelta * sfxVolume * 2f;
			source.pitch = 0.75f + _smoothedAbsDelta * 0.5f + _pitchOffset;
		}
		else
		{
			source.volume = 0.01f * sfxVolume;
			source.pitch = 1f + _pitchOffset;
			_smoothedAbsDelta = 0f;
		}
		_lastFixedUpdateInsertion = insertion;
	}
}
