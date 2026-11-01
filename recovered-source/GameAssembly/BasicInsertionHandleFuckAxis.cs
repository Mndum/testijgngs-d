using System;
using JetBrains.Annotations;
using UnityEngine;

public sealed class BasicInsertionHandleFuckAxis : InsertionHandlerFuckAxis
{
	public float ParameterToAngleX = 0.17453292f;

	public float ParameterToAngleY = (float)Math.PI / 60f;

	private IPenisInfo CurrentPenis = IPenisInfo.DefaultNoPenisInfo;

	public Action<BasicInsertionHandleFuckAxis> OnStart;

	public const float SpeedInside = 3f;

	public const float SpeedOutside = 5f;

	public Action<BasicInsertionHandleFuckAxis> OnReset;

	private bool _isInside;

	private float _realValueLastUpdate;

	private bool _didSlamSoundThisStroke;

	public float InsideStart { get; private set; }

	public float OutsideEnd { get; private set; }

	public Live2DParameter ThrustParameter { get; private set; }

	public Live2DParameter BodyMovementParameterOverride { get; set; }

	public Live2DParameter ReciprocatingParameter { get; set; }

	public ParameterMapping ThrustParameterMapping { get; set; } = ParameterMappingDirect.Direct;

	public float BotPleasureMulti { get; set; } = 1f;

	[CanBeNull]
	public Live2DParameter CockAngleXParameter { get; set; }

	[CanBeNull]
	public Live2DParameter CockAngleYParameter { get; set; }

	public override bool IsInside => _isInside;

	public override bool IsAfterOutside => _realValueLastUpdate > OutsideEnd;

	public BasicInsertionHandleFuckAxis(float outsideEnd, float insideStart, Live2DParameter thrustParameter)
	{
		InsideStart = insideStart;
		OutsideEnd = outsideEnd;
		ThrustParameter = thrustParameter;
	}

	[CanBeNull]
	public Live2DParameter GetBodyMovementParameter()
	{
		if (BodyMovementParameterOverride != null)
		{
			return BodyMovementParameterOverride;
		}
		if (Controller is ILive2DController_BodyAngles live2DController_BodyAngles)
		{
			return live2DController_BodyAngles.ParamBodyY;
		}
		return null;
	}

	public override void Start(IPenisInfo penisInfo)
	{
		CurrentPenis = penisInfo;
		OnStart?.Invoke(this);
	}

	public override IPenisInfo GetPenisInfo()
	{
		return CurrentPenis;
	}

	private float ExtractAngle(Live2DParameter parameter, float paramToAngle)
	{
		if (parameter != null)
		{
			return parameter.GetRealValue() * paramToAngle;
		}
		return 0f;
	}

	public override float GetAngleX()
	{
		return ExtractAngle(CockAngleXParameter, ParameterToAngleX);
	}

	public override float GetAngleY()
	{
		return ExtractAngle(CockAngleYParameter, ParameterToAngleY);
	}

	public override void Reset()
	{
		ThrustParameter.ResetToDefault();
		CockAngleXParameter?.ResetToDefault();
		CockAngleYParameter?.ResetToDefault();
		if (_isInside)
		{
			OnPullOut();
			_isInside = false;
		}
		_realValueLastUpdate = 0f;
		OnReset?.Invoke(this);
	}

	public void OnInserted()
	{
		switch (Owner.OrificeType)
		{
		case InsertionHandlerOrificeTypeEnum.Vagina:
			R.Sound.Frick_insert_vagina_All.PlayRandomSlightlyRandomVolumeAndPitch();
			break;
		case InsertionHandlerOrificeTypeEnum.Anal:
			R.Sound.Frick_insert_anal_All.PlayRandomSlightlyRandomVolumeAndPitch();
			break;
		}
		switch (CurrentPenis.PenisType)
		{
		case PenisTypeEnum.AnonPenis:
			Controller.FireEvent(InteractionMemoryEventEnum.Insert);
			break;
		case PenisTypeEnum.Fingers:
			if (Owner.OrificeType == InsertionHandlerOrificeTypeEnum.Anal)
			{
				Controller.FireEvent(InteractionMemoryEventEnum.FingeringAnalInsert);
			}
			else
			{
				Controller.FireEvent(InteractionMemoryEventEnum.FingeringInsert);
			}
			break;
		}
		if (Controller is ILive2DController_Manager_XRay live2DController_Manager_XRay)
		{
			live2DController_Manager_XRay.XRayManager.SyncXRay();
		}
	}

	public void OnPullOut()
	{
		switch (CurrentPenis.PenisType)
		{
		case PenisTypeEnum.AnonPenis:
			Controller.FireEvent(InteractionMemoryEventEnum.Pullout);
			break;
		case PenisTypeEnum.Fingers:
			if (Owner.OrificeType == InsertionHandlerOrificeTypeEnum.Anal)
			{
				Controller.FireEvent(InteractionMemoryEventEnum.FingeringAnalPullout);
			}
			else
			{
				Controller.FireEvent(InteractionMemoryEventEnum.FingeringPullout);
			}
			break;
		}
		if (Controller is ILive2DController_BodyAngles live2DController_BodyAngles)
		{
			live2DController_BodyAngles.ParamBodyY.ResetToDefault();
		}
	}

	public void OnAnyMovement(float before, float after, float input)
	{
		if (IsInside)
		{
			Live2DParameter bodyMovementParameter = GetBodyMovementParameter();
			if (bodyMovementParameter != null)
			{
				float num = after - before;
				if (after > 0.9f)
				{
					bodyMovementParameter.Value += input;
				}
				else if (num > 0f)
				{
					bodyMovementParameter.Value += num * 0.2f;
				}
				else
				{
					bodyMovementParameter.Value += num;
				}
			}
			float thrust = Mathf.Abs(input);
			float repro = ((ReciprocatingParameter != null) ? ReciprocatingParameter.GetAbsChangeFromLastFrame() : 0f);
			ReportFuckingSpeedUpdate(thrust, repro);
			if (CurrentPenis.CausesAnonsPleasure && Controller is ILive2DController_Manager_CockManager live2DController_Manager_CockManager)
			{
				live2DController_Manager_CockManager.CockManager.AddCumBarValue(0.0007f * FuckingSpeedSum);
			}
			Controller.CurrentBrain.CausePleasure(0.00035f * FuckingSpeedSum * BotPleasureMulti);
			switch (CurrentPenis.PenisType)
			{
			case PenisTypeEnum.AnonPenis:
				switch (Owner.OrificeType)
				{
				default:
					if ((double)FuckingSpeedSum < 0.01)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FuckingNotMoving);
					}
					else if ((double)FuckingSpeedSum < 0.25)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FuckingSlow);
					}
					else if ((double)FuckingSpeedSum < 0.8)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FuckingMedium);
					}
					else
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FuckingFast);
					}
					break;
				case InsertionHandlerOrificeTypeEnum.Anal:
					if ((double)FuckingSpeedSum < 0.01)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FuckingNotMovingAnal);
					}
					else if ((double)FuckingSpeedSum < 0.25)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FuckingSlowAnal);
					}
					else if ((double)FuckingSpeedSum < 0.8)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FuckingMediumAnal);
					}
					else
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FuckingFastAnal);
					}
					break;
				case InsertionHandlerOrificeTypeEnum.Thighjob:
					if ((double)FuckingSpeedSum < 0.01)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.ThighjobNotMoving);
					}
					else if ((double)FuckingSpeedSum < 0.25)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.ThighjobSlow);
					}
					else if ((double)FuckingSpeedSum < 0.8)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.ThighjobMedium);
					}
					else
					{
						Controller.FireEvent(InteractionMemoryEventEnum.ThighjobFast);
					}
					break;
				}
				if (_didSlamSoundThisStroke)
				{
					if (after < 0.7f)
					{
						_didSlamSoundThisStroke = false;
					}
				}
				else if (after > 0.975f)
				{
					float volume = Mathf.Min(0.5f + FuckingSpeedSum * 0.5f, 1.1f) * UnityEngine.Random.Range(0.9f, 1.1f);
					R.Sound.Frick_slam_All.PlayRandom(volume);
					_didSlamSoundThisStroke = true;
				}
				break;
			case PenisTypeEnum.Fingers:
				if (Owner.OrificeType == InsertionHandlerOrificeTypeEnum.Anal)
				{
					if ((double)FuckingSpeedSum < 0.25)
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FingeringAnalSlow);
					}
					else
					{
						Controller.FireEvent(InteractionMemoryEventEnum.FingeringAnalFast);
					}
				}
				else if ((double)FuckingSpeedSum < 0.25)
				{
					Controller.FireEvent(InteractionMemoryEventEnum.FingeringSlow);
				}
				else
				{
					Controller.FireEvent(InteractionMemoryEventEnum.FingeringFast);
				}
				break;
			}
		}
		else
		{
			ReportFuckingSpeedUpdate(0f, 0f);
		}
	}

	public override void Update()
	{
		float num = Owner.ThrustMovementAccumulator;
		if (CockAngleXParameter != null)
		{
			CockAngleXParameter.Value += Owner.SidewaysMovementAccumulator;
		}
		if (CockAngleYParameter != null)
		{
			CockAngleYParameter.Value += Owner.YMovementAccumulator;
		}
		float num2 = (InsideStart - OutsideEnd) * 0.1f;
		float num3 = ThrustParameterMapping.MapFrom(ThrustParameter.GetRealValue());
		float num4 = ThrustParameterMapping.MapFrom(ThrustParameter.Value);
		float num5 = ThrustParameter.MaximumValue;
		foreach (InsertionHandlerBlock blockage in Owner.Blockages)
		{
			if (blockage.ActiveFunc() && blockage.Block < num5)
			{
				num5 = blockage.Block;
			}
		}
		if (num3 > num5)
		{
			num3 = num5;
		}
		if (num3 > InsideStart)
		{
			if (!_isInside)
			{
				_isInside = true;
				OnInserted();
			}
			num4 += num * 3f;
			if (num4 < InsideStart - num2)
			{
				num4 = InsideStart - num2;
			}
		}
		else if (num3 >= OutsideEnd)
		{
			if (_isInside)
			{
				if (num < -0.1f)
				{
					num = -0.1f;
				}
				num4 += ((num < 0f) ? (num * 0.3f) : (num * 3f));
			}
			else
			{
				num4 += ((num > 0f) ? (num * 0.3f) : (num * 5f));
			}
		}
		else
		{
			if (_isInside)
			{
				_isInside = false;
				OnPullOut();
			}
			num4 += num * 5f;
			if (num4 > OutsideEnd + num2)
			{
				num4 = OutsideEnd + num2;
			}
		}
		if (num4 > num5)
		{
			num4 = num5;
		}
		ThrustParameter.Value = ThrustParameterMapping.MapTo(num4);
		OnAnyMovement(_realValueLastUpdate, num3, num);
		_realValueLastUpdate = num3;
	}

	public override float GetCurrentRealInsertionPosition()
	{
		return ThrustParameterMapping.MapFrom(ThrustParameter.GetRealValue());
	}

	public override float GetRealDistanceToInsertion()
	{
		return InsideStart - GetCurrentRealInsertionPosition();
	}

	public override float GetCurrentInsertionPosition()
	{
		return ThrustParameterMapping.MapFrom(ThrustParameter.Value);
	}

	public override void ResetAccumulatedMovementToCurrentPosition()
	{
		ThrustParameter.Value = ThrustParameter.GetRealValue();
	}

	public override bool CanThrustForward()
	{
		return ThrustParameter.GetRealValue() < 0.98f;
	}

	public override bool CanThrustBackwards()
	{
		return ThrustParameter.GetRealValue() > InsideStart + 0.1f;
	}
}
