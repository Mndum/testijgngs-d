using System.Collections;
using UnityEngine;

public class SpecialAudioLoop
{
	private SpecialAudioSource _startSource;

	private SpecialAudioSource _loopSource;

	private SpecialAudioSource _endSource;

	private Coroutine playCoroutine;

	public bool IsPlayingStartOrLoop => playCoroutine != null;

	public SpecialAudioLoop(SpecialAudioSource startSource, SpecialAudioSource loopSource, SpecialAudioSource endSource, float volume)
	{
		_startSource = startSource;
		_loopSource = loopSource;
		_endSource = endSource;
		_startSource.Volume = volume;
		_loopSource.Volume = volume;
		_endSource.Volume = volume;
	}

	private void StopCoroutine()
	{
		if (playCoroutine != null)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.StopCoroutine(playCoroutine);
			playCoroutine = null;
		}
	}

	public void Play()
	{
		StopCoroutine();
		playCoroutine = SingletonMonoBehaviour<SoundSingleton>.Instance.StartCoroutine(PlayInternal());
	}

	public IEnumerator PlayInternal()
	{
		yield return _startSource.Play();
		_loopSource.PlayLoop();
	}

	public void End(bool kill = false)
	{
		_startSource.Remove();
		_loopSource.Remove();
		StopCoroutine();
		if (_endSource != null)
		{
			if (!kill)
			{
				_endSource.Play();
			}
			else
			{
				_endSource.Remove();
			}
		}
	}
}
