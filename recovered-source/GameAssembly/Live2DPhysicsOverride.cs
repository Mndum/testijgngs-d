using System;
using Live2D.Cubism.Framework.Physics;
using UnityEngine;

public class Live2DPhysicsOverride
{
	public CubismPhysicsSubRig SubRig { get; private set; }

	public bool DisablePhysics
	{
		get
		{
			return SubRig.Output[0].DisableOutputToParameter;
		}
		set
		{
			for (int i = 0; i < SubRig.Output.Length; i++)
			{
				SubRig.Output[i].DisableOutputToParameter = value;
			}
		}
	}

	public float Scale
	{
		get
		{
			return SubRig.Output[0].Weight / 100f;
		}
		set
		{
			for (int i = 0; i < SubRig.Output.Length; i++)
			{
				SubRig.Output[i].Weight = value * 100f;
			}
		}
	}

	public Live2DPhysicsOverride(CubismPhysicsSubRig subRig, bool disablePhysics = false, float scale = 1f)
	{
		SubRig = subRig;
		DisablePhysics = disablePhysics;
		Scale = scale;
	}

	public static Vector2 DirectionFromToTo(Vector2 from, float radian)
	{
		float x = Mathf.Cos(radian) * from.x - Mathf.Sin(radian) * from.y;
		float y = Mathf.Sin(radian) * from.x + Mathf.Cos(radian) * from.y;
		return new Vector2(x, y);
	}

	public float GetPhysicsValue(int outputIndex)
	{
		return SubRig.Rig.ParametersCache[SubRig.Output[outputIndex].DestinationIndex];
	}

	public float GetFirstParticlePhysicsAngleRadians()
	{
		return SubRig.GetFirstParticlePhysicsAngleRadians();
	}

	public void SetPhysicsAngleTo(float targetRadians)
	{
		SubRig.SetAngleTo(targetRadians);
	}

	public void JerkPhysicsInDirection(float directionRadians)
	{
		SetPhysicsAngleTo(GetFirstParticlePhysicsAngleRadians() + directionRadians);
	}

	private void PullToTarget(float targetOutputValue, int outputIndex)
	{
		CubismPhysicsOutput cubismPhysicsOutput = SubRig.Output[outputIndex];
		CubismPhysicsRig rig = SubRig.Rig;
		Vector2 to = Vector2.zero;
		switch (cubismPhysicsOutput.SourceComponent)
		{
		default:
			to.x = targetOutputValue / cubismPhysicsOutput.TranslationScale.x;
			break;
		case CubismPhysicsSourceComponent.Y:
			to.y = targetOutputValue / cubismPhysicsOutput.TranslationScale.y;
			break;
		case CubismPhysicsSourceComponent.Angle:
			to = DirectionFromToTo(rig.Gravity * -1f, targetOutputValue / cubismPhysicsOutput.AngleScale);
			break;
		}
		float num = CubismPhysicsMath.DirectionToRadian(Vector2.up, to);
		if (cubismPhysicsOutput.IsInverted)
		{
			num *= -1f;
		}
		SubRig.ForceParameterValue(outputIndex, targetOutputValue);
		SubRig.PullOn(num, cubismPhysicsOutput.ParticleIndex);
	}

	public void StartParameterControl(Live2DParameter parameter)
	{
		string destinationId = parameter.Parameter.name;
		int num = Array.FindIndex(SubRig.Output, (CubismPhysicsOutput x) => destinationId == x.DestinationId);
		if (num < 0)
		{
			Debug.LogError(parameter.Parameter.name + " not found in " + SubRig.Name + "!!");
			return;
		}
		parameter.SetRealValue(GetPhysicsValue(num));
		DisablePhysics = true;
	}

	public void EndParameterControl(Live2DParameter parameter)
	{
		string destinationId = parameter.Parameter.name;
		int num = Array.FindIndex(SubRig.Output, (CubismPhysicsOutput x) => destinationId == x.DestinationId);
		if (num < 0)
		{
			Debug.LogError(parameter.Parameter.name + " not found in " + SubRig.Name + "!!");
			return;
		}
		PullToTarget(parameter.GetRealValue(), num);
		DisablePhysics = false;
	}
}
