using System;
using UnityEngine;

public abstract class AudioClipDisposableWrapper : IDisposable
{
	public AudioClip AudioClip { get; protected set; }

	protected AudioClipDisposableWrapper(AudioClip audioClip)
	{
		AudioClip = audioClip;
	}

	public abstract void Dispose();
}
