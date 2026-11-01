using UnityEngine;

public class CumManagerCommon : ICumManagerCommon, Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_SerializedState, Live2DController.ILive2DControllerManager_Callback, Live2DController.ILive2DControllerManager_FixedUpdate, Live2DController.ILive2DControllerManager_StartUsageGameplay
{
	private Live2DController _controller;

	private bool _fastWombLeak;

	public Live2DParameter CumInsideVaginaParameter { get; private set; }

	public Live2DParameter CumInsideWombParameter { get; private set; }

	public Live2DParameter CumInsideAnalParameter { get; private set; }

	public Live2DParameter CumInMouthParameter { get; private set; }

	public float CurrentCumMouth => CumInMouthParameter.GetPercent() * 30f;

	public float CurrentCumInsideVaginaOnly => CumInsideVaginaParameter.GetPercent() * 50f;

	public float CurrentCumInsideWombOnly => CumInsideWombParameter.GetPercent() * 40f;

	public float CurrentCumInside => CurrentCumInsideVaginaOnly + CurrentCumInsideWombOnly;

	public float CurrentCumInsideAnal => CumInsideAnalParameter.GetPercent() * 30f;

	public CumManagerCommon(Live2DController controller)
	{
		_controller = controller;
		CumInsideVaginaParameter = controller.GetParamOrCreateFake((ILive2DController_Lewd_CumInsideVagina c) => c.ParamCumInside);
		CumInsideWombParameter = controller.GetParamOrCreateFake((ILive2DController_Lewd_CumInsideWomb c) => c.ParamCumInsideWomb);
		CumInsideAnalParameter = controller.GetParamOrCreateFake((ILive2DController_Lewd_CumInsideAnal c) => c.ParamCumInsideAnal);
		CumInMouthParameter = controller.GetParamOrCreateFake((ILive2DController_Lewd_CumInsideMouth c) => c.ParamCumInMouth);
	}

	public void CumInside(float amount, float depth01, InsertionHandlerOrificeTypeEnum anal)
	{
		switch (anal)
		{
		case InsertionHandlerOrificeTypeEnum.Anal:
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.mlCameInAss += amount;
			CumInsideAnalParameter.AddPercent(amount / 30f);
			_controller.FireEvent(InteractionMemoryEventEnum.CumInsideAnal);
			break;
		case InsertionHandlerOrificeTypeEnum.Vagina:
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.mlCameInVagina += amount;
			if (CumInsideVaginaParameter.IsApproximatelyMaximumValue)
			{
				depth01 = 1f;
			}
			else if (CumInsideWombParameter.IsApproximatelyMaximumValue)
			{
				depth01 = 0f;
			}
			depth01 += Random.Range(-0.05f, 0.05f);
			depth01 = Mathf.Clamp01(depth01);
			float num = depth01 * amount;
			float num2 = amount - num;
			CumInsideWombParameter.AddPercent(num / 40f);
			CumInsideVaginaParameter.AddPercent(num2 / 50f);
			_controller.FireEvent(InteractionMemoryEventEnum.CumInside);
			break;
		}
		case InsertionHandlerOrificeTypeEnum.Mouth:
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.mlCameInMouth += amount;
			CumInMouthParameter.AddPercent(amount / 30f);
			_controller.FireEvent(InteractionMemoryEventEnum.CumInsideMouth);
			break;
		case InsertionHandlerOrificeTypeEnum.Thighjob:
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.mlCameFromThighjob += amount;
			_controller.FireEvent(InteractionMemoryEventEnum.CumThighjob);
			break;
		}
	}

	public void ClearCumInside(InsertionHandlerOrificeTypeEnum orfice)
	{
		switch (orfice)
		{
		case InsertionHandlerOrificeTypeEnum.Vagina:
			CumInsideVaginaParameter.ResetToDefault();
			CumInsideWombParameter.ResetToDefault();
			break;
		case InsertionHandlerOrificeTypeEnum.Anal:
			CumInsideAnalParameter.ResetToDefault();
			break;
		case InsertionHandlerOrificeTypeEnum.Mouth:
			CumInMouthParameter.ResetToDefault();
			break;
		case InsertionHandlerOrificeTypeEnum.Thighjob:
			break;
		}
	}

	public void MoveCumDownwardsFast(InsertionHandlerOrificeTypeEnum orfice)
	{
		if (orfice == InsertionHandlerOrificeTypeEnum.Vagina)
		{
			MoveCumFromWombToVagina(5f);
		}
	}

	public bool TryExtractCum(InsertionHandlerOrificeTypeEnum orfice, float f)
	{
		switch (orfice)
		{
		case InsertionHandlerOrificeTypeEnum.Vagina:
			if (CurrentCumInsideVaginaOnly > f)
			{
				CumInsideVaginaParameter.Add((0f - f) / 50f);
				return true;
			}
			break;
		case InsertionHandlerOrificeTypeEnum.Anal:
			if (CurrentCumInsideAnal > f)
			{
				CumInsideAnalParameter.Add((0f - f) / 30f);
				return true;
			}
			break;
		case InsertionHandlerOrificeTypeEnum.Mouth:
			if (CurrentCumMouth > f)
			{
				CumInMouthParameter.Add((0f - f) / 30f);
				return true;
			}
			break;
		}
		return false;
	}

	private void MoveCumFromWombToVagina(float maxMove)
	{
		float num = Mathf.Min(CumInsideWombParameter.Value, maxMove);
		CumInsideWombParameter.Value -= num;
		CumInsideVaginaParameter.Value += num * 1.25f;
	}

	public void StartUsageGameplay()
	{
		SetFastWombLeakActive(value: false);
	}

	public void SetFastWombLeakActive(bool value)
	{
		_fastWombLeak = value;
	}

	public void FixedUpdate()
	{
		if (CumInsideVaginaParameter.GetPercent() < CumInsideWombParameter.GetPercent() || (_fastWombLeak && CumInsideWombParameter.Value > 0f))
		{
			if (_fastWombLeak)
			{
				MoveCumFromWombToVagina(4f * Time.fixedDeltaTime / 40f);
			}
			else
			{
				MoveCumFromWombToVagina(0.5f * Time.fixedDeltaTime / 40f);
			}
		}
	}

	public void SaveState(Live2DControllerCommonState state)
	{
		state.CumInsideAnalPercent = CumInsideAnalParameter.GetPercent();
		state.CumInsideVaginaPercent = CumInsideVaginaParameter.GetPercent();
		state.CumInsideWombPercent = CumInsideWombParameter.GetPercent();
		state.CumInMouthPercent = CumInMouthParameter.GetPercent();
	}

	public void LoadState(Live2DControllerCommonState state)
	{
		CumInsideAnalParameter.SetPercent(state.CumInsideAnalPercent);
		CumInsideWombParameter.SetPercent(state.CumInsideWombPercent);
		CumInsideVaginaParameter.SetPercent(state.CumInsideVaginaPercent);
		CumInMouthParameter.SetPercent(state.CumInMouthPercent);
	}
}
