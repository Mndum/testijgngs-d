using System;
using System.Collections;
using Fungus;
using UnityEngine;

public abstract class TweenBase : Command
{
	[Range(0f, 10f)]
	public float time = 0.5f;

	public EasingFunction.Ease easingType = EasingFunction.Ease.EaseInCubic;

	public bool waitForEnd = true;

	public void OnComplete()
	{
	}

	public abstract Action<float> GetSetValueAction();

	private void End()
	{
		OnComplete();
		if (waitForEnd)
		{
			Continue();
		}
	}

	private IEnumerator Logic(Action<float> SetValue)
	{
		if (SetValue == null)
		{
			yield return new WaitForSeconds(time);
			End();
			yield break;
		}
		if (time <= 0f)
		{
			SetValue(1f);
			End();
			yield break;
		}
		SetValue(0f);
		EasingFunction.Function easingFunc = EasingFunction.GetEasingFunction(easingType);
		float timer = 0f;
		while (timer < time)
		{
			yield return null;
			timer += Time.deltaTime;
			SetValue(easingFunc(0f, 1f, timer / time));
		}
		SetValue(1f);
		End();
	}

	public override void OnEnter()
	{
		StartCoroutine(Logic(GetSetValueAction()));
		if (!waitForEnd)
		{
			Continue();
		}
	}
}
