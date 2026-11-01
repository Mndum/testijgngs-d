using System.Collections;
using UnityEngine;

public class SpecialAudioSource
{
	private Coroutine _playingCoroutine;

	private AudioSource source;

	public bool IsPlaying { get; private set; }

	public float Volume
	{
		get
		{
			return source.volume;
		}
		set
		{
			source.volume = value;
		}
	}

	public SpecialAudioSource(AudioSource source)
	{
		this.source = source;
	}

	public void PlayLoop()
	{
		if (source != null)
		{
			IsPlaying = true;
			source.loop = true;
			source.Play();
		}
	}

	public Coroutine Play()
	{
		return _playingCoroutine = SingletonMonoBehaviour<SoundSingleton>.Instance.StartCoroutine(PlayInternal());
	}

	private IEnumerator PlayInternal()
	{
		if (source != null && source.clip != null)
		{
			if (_playingCoroutine != null)
			{
				SingletonMonoBehaviour<SoundSingleton>.Instance.StopCoroutine(_playingCoroutine);
			}
			IsPlaying = true;
			source.loop = false;
			source.Play();
			yield return WaitForSecondCache.Seconds0_05;
			yield return new WaitForSeconds(source.GetClipRemainingTime());
			IsPlaying = false;
			_playingCoroutine = null;
			Remove();
		}
	}

	public void Remove()
	{
		if (source != null)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.ReturnToPool(source);
			source = null;
		}
	}
}
