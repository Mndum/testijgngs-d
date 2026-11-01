using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(SpriteRenderer))]
public class SimpleLightMultiplyScript : MonoBehaviour
{
	public bool affectedByRoomLight;

	public Gradient multiplyGradient;

	[SerializeField]
	private SpriteRenderer LightMultiply;

	public bool LightSwitchOn
	{
		get
		{
			return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lightSwitchOn;
		}
		private set
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lightSwitchOn = value;
		}
	}

	public void OnValidate()
	{
		LightMultiply = GetComponent<SpriteRenderer>();
	}

	public void OnEnable()
	{
		if (GameScript.HasGameStarted)
		{
			SetTime();
		}
		SingletonLoadableMonoBehaviour<GameScript>.Instance.OnMinutePassed += SetTime;
	}

	public void OnDisable()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.OnMinutePassed -= SetTime;
	}

	public void SetTime()
	{
		SetTime(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.TimeDay);
	}

	public void SetTime(int time)
	{
		float num = 1440f;
		float time2 = (float)time / num;
		Color color = multiplyGradient.Evaluate(time2);
		if (affectedByRoomLight && LightSwitchOn)
		{
			LightMultiply.color = (color + Color.white) / 2f;
		}
		else
		{
			LightMultiply.color = color;
		}
	}
}
