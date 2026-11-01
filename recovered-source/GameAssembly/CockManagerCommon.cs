using System;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

public class CockManagerCommon<T> : ICockManager, Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_SerializedState, Live2DController.ILive2DControllerManager_Callback, Live2DController.ILive2DControllerManager_Update, Live2DController.ILive2DControllerManager_FixedUpdate where T : Live2DController, ILive2DController_Manager_CockManager
{
	private T _controller;

	[CanBeNull]
	private ICockLimbUsage _cockLimbUsage;

	protected bool _isFlacid;

	protected float _targetCumBarValue;

	private ButtonListWrapper _cockButtonWrapper;

	private ButtonListWrapper _pullOutButtonWrapper;

	private IModificationPeriodButtonList _buttonList;

	public bool AlwaysEnableCockAtStart { get; set; }

	public bool AllowHidingCock { get; set; } = true;

	public bool AllowFlaccidCumming { get; set; }

	public bool AutoFuckImplemented { get; set; } = true;

	public bool AutoPulloutImplemented { get; set; } = true;

	public bool IsMCNaked { get; set; }

	public Action<CockManagerCommon<T>> ToyUpdateOverride { get; set; }

	public bool CanGetFlacid => !SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.VinegaraActive;

	public bool ShouldGetFlacid
	{
		get
		{
			if (GameVariables.Current.RemainingCum <= 5f)
			{
				return CanGetFlacid;
			}
			return false;
		}
	}

	public bool IsFlacid
	{
		get
		{
			return _isFlacid;
		}
		set
		{
			if (!_isFlacid && value)
			{
				if (CanGetFlacid)
				{
					_isFlacid = true;
					ActivateFlaccidity();
				}
			}
			else if (_isFlacid && !value)
			{
				_isFlacid = false;
				DeactivateFlaccidity();
			}
		}
	}

	public bool IsCockCummingState
	{
		get
		{
			if (_controller.TryGetLimbUsageAt(LimbUsageChoiceEnum.Cock, out ICockLimbUsage limbUsage))
			{
				return limbUsage.IsInCummingState;
			}
			return false;
		}
	}

	public bool IsCockNormalStateOrHidden
	{
		get
		{
			if (_controller.TryGetLimbUsageAt(LimbUsageChoiceEnum.Cock, out ICockLimbUsage limbUsage))
			{
				return limbUsage.IsInNormalState;
			}
			return true;
		}
	}

	public float CumBarValue { get; protected set; }

	public float TargetCumBarValue
	{
		get
		{
			return _targetCumBarValue;
		}
		set
		{
			_targetCumBarValue = value;
		}
	}

	public CockManagerCommon(T controller, [CanBeNull] ICockLimbUsage cockLimbUsage)
	{
		_controller = controller;
		_cockLimbUsage = cockLimbUsage;
	}

	public void DeactivateFlaccidity()
	{
		_controller.ParamFlacidity.Value = 0f;
	}

	public void ActivateFlaccidity()
	{
		_controller.ParamFlacidity.Value = 1f;
		_controller.ParamFlacidity.SetWeight(100f);
		if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("firsttimeflacid"))
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ShowText(LOC.UI.Frick_FlacidFirstTime);
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddFlag("firsttimeflacid");
		}
		_controller.FireEvent(InteractionMemoryEventEnum.GotFlacid);
		_targetCumBarValue = 0f;
		if (!AllowFlaccidCumming && TryGetCockLimbUsage(out var cockUsage))
		{
			cockUsage.PullOutInstantly();
		}
		if (_controller is ILive2DController_Manager_FrickStaticGuiManager live2DController_Manager_FrickStaticGuiManager)
		{
			live2DController_Manager_FrickStaticGuiManager.FrickStaticGuiManager.UpdateUI();
		}
	}

	public bool TryGetCockLimbUsage(out ICockLimbUsage cockUsage)
	{
		if (_controller.TryGetLimbUsageAt(LimbUsageChoiceEnum.Cock, out cockUsage))
		{
			return true;
		}
		return false;
	}

	public bool IsCockVisible()
	{
		ICockLimbUsage cockUsage;
		return TryGetCockLimbUsage(out cockUsage);
	}

	public bool IsCockLimbInside()
	{
		if (_controller.TryGetLimbUsageAt(LimbUsageChoiceEnum.Cock, out ICockLimbUsage limbUsage))
		{
			return limbUsage.IsInside();
		}
		return false;
	}

	public void WhipOutCock()
	{
		if (_cockLimbUsage != null)
		{
			_controller.EnsureLimbs(_cockLimbUsage);
		}
	}

	public void PrintDebugInfo()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			if (TryGetCockLimbUsage(out var cockUsage))
			{
				if (cockUsage != null && cockUsage.TryGetActiveData(out var o, out var fuckAxis))
				{
					stringBuilder.AppendLine($"  Orifice type             : {o?.OrificeType}");
					if (fuckAxis != null)
					{
						stringBuilder.AppendLine("  Axis type                : " + fuckAxis.GetType().Name);
						stringBuilder.AppendLine($"  IsInside                 : {fuckAxis.IsInside}");
						stringBuilder.AppendLine($"  Current insertion pos    : {fuckAxis.GetCurrentInsertionPosition()}");
						stringBuilder.AppendLine($"  Angle X                  : {fuckAxis.GetAngleX():F3}");
						stringBuilder.AppendLine($"  Angle Y                  : {fuckAxis.GetAngleY():F3}");
					}
					else
					{
						stringBuilder.AppendLine("  fuckAxis is null");
					}
				}
				else
				{
					stringBuilder.AppendLine("Has limb usage, but no active data");
				}
			}
			else
			{
				stringBuilder.AppendLine("No cock limb usage");
			}
			Debug.Log(stringBuilder);
		}
	}

	public void AddCumBarValue(float amount)
	{
		if (!(amount <= 0f))
		{
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.DeathGripActive)
			{
				_targetCumBarValue += amount * 0.1f;
			}
			else
			{
				_targetCumBarValue += amount;
			}
		}
	}

	public bool FixedUpdateCumBarValue()
	{
		_targetCumBarValue -= 1E-05f;
		_targetCumBarValue = Mathf.Clamp(_targetCumBarValue, 0f, 2.5f);
		float num = 10f + _targetCumBarValue;
		CumBarValue = (_targetCumBarValue + CumBarValue * num) / (1f + num);
		UiOverlay.Instance.GetStaticGUI<FrickStaticGui>(out var staticGui);
		staticGui.SyncCumbar(CumBarValue);
		return CumBarValue >= 1f;
	}

	public void FillLeftButtonList(IModificationPeriodButtonList buttonList)
	{
		_buttonList = buttonList;
		if (AutoFuckImplemented && AutoPulloutImplemented)
		{
			_pullOutButtonWrapper = buttonList.AddButton(-15);
			_pullOutButtonWrapper.ActiveFunc = () => _controller.TryGetLimbUsageAt(LimbUsageChoiceEnum.Cock, out ICockLimbUsage limbUsage) && limbUsage.IsInside() && limbUsage.AutoStage != AutoTypeEnum.Pullout;
			_pullOutButtonWrapper.SetText(LOC.UI.Frick_CowgirlRequest_Pullout);
			_pullOutButtonWrapper.OnClick = PullOutButtonClicked;
		}
		if (!AllowHidingCock)
		{
			return;
		}
		_cockButtonWrapper = buttonList.AddButton(50);
		_cockButtonWrapper.TextFunc = () =>
		{
			if (IsCockVisible())
			{
				return LOC.UI.Frick_HideCock;
			}
			return (!IsMCNaked && !GameVariables.Current.botLive2DCommonState.FrickData.D_UnzippedPantsAlready) ? LOC.UI.Frick_Unzip : LOC.UI.Frick_ShowCock;
		};
		_cockButtonWrapper.OnClick = CockButtonClicked;
		_cockButtonWrapper.Interactable = true;
	}

	private void UpdatePulloutButton()
	{
		if (_pullOutButtonWrapper != null && _buttonList != null)
		{
			_buttonList.UpdateButtonSingle(_pullOutButtonWrapper);
		}
	}

	private void PullOutButtonClicked()
	{
		if (AutoPulloutImplemented && IsCockLimbInside() && TryGetCockLimbUsage(out var cockUsage))
		{
			cockUsage.PullOutAuto();
			UpdatePulloutButton();
		}
	}

	private void CockButtonClicked()
	{
		if (!AllowHidingCock)
		{
			return;
		}
		if (TryGetCockLimbUsage(out var cockUsage))
		{
			if (IsCockLimbInside() || !cockUsage.CanHideCock || _controller.CurrentBrain.CurrentState is CowgirlBrainState { IsReactingUnstoppable: not false })
			{
				UiOverlay.Instance.ShowFloatingTextAtMouse(LOC.UI.Frick_EndFailedPrompt);
				return;
			}
			_controller.LetGoLimbs(LimbUsageChoiceEnum.Cock, LetGoLimbsCaller.Outside);
			_cockButtonWrapper.UpdateButtonText();
			return;
		}
		_cockButtonWrapper.Interactable = false;
		_controller.ParamFlacidity.SetPercent(1f);
		if (IsMCNaked || DebugDefines.FastViewSwitching || GameVariables.Current.botLive2DCommonState.FrickData.D_UnzippedPantsAlready)
		{
			_controller.LetGoOfCurrentlyHeld();
			PullOutCock_Local();
			return;
		}
		SingletonMonoBehaviour<SoundSingleton>.Instance.Play(R.Sound.Frick_unzip_unzip.Get(), 1f, () =>
		{
			_controller.LetGoOfCurrentlyHeld();
			PullOutCock_Local();
		});
		void PullOutCock_Local()
		{
			if (!IsFlacid)
			{
				_controller.ParamFlacidity.SetPercent(0f);
				_controller.ParamFlacidity.FinishMove();
			}
			WhipOutCock();
			_cockButtonWrapper.Interactable = true;
			_cockButtonWrapper.UpdateButtonText();
		}
	}

	void Live2DController.ILive2DControllerManager_Update.Update()
	{
		if (UiOverlay.Instance.IsOpen)
		{
			return;
		}
		if (ToyUpdateOverride != null)
		{
			ToyUpdateOverride(this);
			return;
		}
		IToy currentToy = SingletonMonoBehaviour<ToySingleton>.Instance.CurrentToy;
		if (currentToy == null || !currentToy.Active || !TryGetCockLimbUsage(out var cockUsage) || !cockUsage.TryGetActiveData(out var o))
		{
			return;
		}
		currentToy.Update();
		float num = currentToy.Insertion;
		if (!o.TryGetActiveFuckAxis(out var currentAxis))
		{
			return;
		}
		if (o.IsInside())
		{
			if (num < 0.04f)
			{
				o.ThrustMovementAccumulator = 0f - Time.deltaTime;
			}
			else
			{
				if (num > 0.9f)
				{
					num = 1f;
				}
				float currentInsertionPosition = currentAxis.GetCurrentInsertionPosition();
				float f = num - currentInsertionPosition;
				float num2 = Mathf.Abs(f);
				float num3 = Mathf.Sign(f);
				float num4 = num2 * Mathf.Min(25f * Time.deltaTime, 0.9f) * num3;
				o.ThrustMovementAccumulator = num4 / 3f;
			}
		}
		else if (!(_controller is Live2DCowgirlController) && num > 0.04f)
		{
			o.ThrustMovementAccumulator = Time.deltaTime;
		}
		if (currentToy.CumButtonClicked)
		{
			AddCumBarValue(0.4f * Time.deltaTime);
		}
	}

	void Live2DController.ILive2DControllerManager_FixedUpdate.FixedUpdate()
	{
		if (!UiOverlay.Instance.IsOpen)
		{
			UpdatePulloutButton();
			if (!TryGetCockLimbUsage(out var _))
			{
				FixedUpdateCumBarValue();
			}
		}
	}

	void Live2DController.ILive2DControllerManager_SerializedState.SaveState(Live2DControllerCommonState state)
	{
		Live2DControllerCommonStateFrickData frickData = state.FrickData;
		frickData.S_IsFlaccid = IsFlacid;
		frickData.S_IsCockOut = IsCockVisible();
		frickData.S_TargetCumBarValue = _targetCumBarValue;
		frickData.S_CurrentCumBarValue = CumBarValue;
	}

	void Live2DController.ILive2DControllerManager_SerializedState.LoadState(Live2DControllerCommonState state)
	{
		Live2DControllerCommonStateFrickData frickData = state.FrickData;
		IsFlacid = frickData.S_IsFlaccid;
		if (frickData.S_IsCockOut || AlwaysEnableCockAtStart)
		{
			WhipOutCock();
		}
		_targetCumBarValue = frickData.S_TargetCumBarValue;
		CumBarValue = frickData.S_CurrentCumBarValue;
	}
}
