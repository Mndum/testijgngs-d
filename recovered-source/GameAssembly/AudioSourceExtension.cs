using System;
using System.Collections;
using IC.AutomaticReference;
using UnityEngine;

public static class AudioSourceExtension
{
	public static bool IsReversePitch(this AudioSource source)
	{
		return source.pitch < 0f;
	}

	public static float GetClipRemainingTime(this AudioSource source)
	{
		float num = (source.clip.length - source.time) / source.pitch;
		if (!source.IsReversePitch())
		{
			return num;
		}
		return source.clip.length + num;
	}

	public static void Play(this AudioClip audioClip, float volume = 1f, Action onComplete = null)
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.Play(audioClip, volume, onComplete);
	}

	public static IEnumerator PlayAndWait(this AudioClip audioClip, float volume = 1f)
	{
		var (result, onComplete) = CoroutineUtilities.PrepareWaitForAction();
		SingletonMonoBehaviour<SoundSingleton>.Instance.Play(audioClip, volume, onComplete);
		return result;
	}

	public static void PlayRandom(this SerializableAutomaticReference<AudioClip>[] audioClips, float volume = 1f)
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.Play(audioClips.GetRandom().Get(), volume);
	}

	public static void PlayRandomWithPitch(this SerializableAutomaticReference<AudioClip>[] audioClips, float volume, float pitch)
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayWithPitch(audioClips.GetRandom().Get(), volume, pitch);
	}

	public static void PlayRandomSlightlyRandomVolume(this SerializableAutomaticReference<AudioClip>[] audioClips)
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlaySlightlyRandomVolume(audioClips.GetRandom().Get());
	}

	public static void PlayRandomSlightlyRandomVolumeAndPitch(this SerializableAutomaticReference<AudioClip>[] audioClips)
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayWithPitch(audioClips.GetRandom().Get(), UnityEngine.Random.Range(0.91f, 1f), UnityEngine.Random.Range(0.91f, 1f));
	}

	public static void PlaySlightlyRandomVolume(this AudioClip audioClip)
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlaySlightlyRandomVolume(audioClip);
	}
}
