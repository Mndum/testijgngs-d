using System;
using UnityEngine;

public abstract class EmailBase
{
	public bool read;

	public int visibleAt;

	[Obsolete]
	public EventHolder onReadEventHolder;

	public abstract string Sender { get; }

	public abstract string Topic { get; }

	public abstract string Text { get; }

	public static event Action OnReadStaticEvent;

	public bool IsVisible(int currentTime)
	{
		return visibleAt <= currentTime;
	}

	public void ReadEmail()
	{
		if (!read)
		{
			read = true;
			OnEmailRead();
			OnReadStaticEvent?.Invoke();
		}
	}

	protected abstract void OnEmailRead();

	public abstract string Serialize();

	public static EmailBase Deserialize(string text)
	{
		if (text.Length < 1)
		{
			return null;
		}
		if (text[0] == NewEmail.Prefix)
		{
			text = text.Remove(0, 1);
			return JsonUtility.FromJson<NewEmail>(text);
		}
		if (text[0] == Email.Prefix)
		{
			text = text.Remove(0, 1);
			return JsonUtility.FromJson<Email>(text);
		}
		return null;
	}
}
