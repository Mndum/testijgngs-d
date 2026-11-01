using System;
using Fungus;
using UnityEngine;

[CommandInfo("Live2D", "Tween parameter", "Tweens parameter", 0)]
public class TweenParameter : TweenBase
{
	public string targetCharacter = "bot";

	public string targetParameterName;

	[Range(0f, 1f)]
	public float percent;

	public static string lastCharacter;

	public Live2DController GetCurrentController()
	{
		return SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.GetReadyController(targetCharacter);
	}

	public override void OnCommandAdded(Block parentBlock)
	{
		if (targetCharacter == null)
		{
			targetCharacter = lastCharacter;
		}
	}

	public override void OnValidate()
	{
		base.OnValidate();
		if (targetCharacter != null)
		{
			lastCharacter = targetCharacter;
		}
	}

	public override Action<float> GetSetValueAction()
	{
		if (GetCurrentController() == null)
		{
			return null;
		}
		ParameterBase param = GetCurrentController().GetParamByPropertyName(targetParameterName);
		if (param == null)
		{
			return null;
		}
		float startPercent = param.GetPercent();
		float move = percent - startPercent;
		return (float f) =>
		{
			param.SetPercent(startPercent + move * f);
		};
	}

	public override string GetSummary()
	{
		if (targetCharacter == null)
		{
			return "ERROR: No character selected!";
		}
		return "Tweens some shit. Can't get the summary outside of editor!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(255, 150, 255, 255);
	}
}
