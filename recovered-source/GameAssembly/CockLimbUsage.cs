using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public sealed class CockLimbUsage<T> : ICockLimbUsage, ILimbUsage, ILimbUsageGUI where T : Live2DLewdSceneController, ILive2DController_Lewd_CumShotBasic, ILive2DController_Manager_CockManager, ILive2DController_Manager_FrickStaticGuiManager, ILive2DController_Manager_InsertionManager
{
	public class CockLimbUsageTarget
	{
		public readonly InsertionHandler TargetInsertionHandler;

		public readonly InsertionHandlerFuckAxis TargetAxis;

		public CockLimbUsageTarget(InsertionHandler targetInsertionHandler, InsertionHandlerFuckAxis targetAxis)
		{
			TargetInsertionHandler = targetInsertionHandler;
			TargetAxis = targetAxis;
		}

		public void Verify()
		{
			if (!TargetInsertionHandler.ContainsAxis(TargetAxis))
			{
				Debug.LogError("Wrong axis provided!");
			}
		}
	}

	private enum AutoState
	{
		Normal,
		CummingImminent,
		CummingInside,
		CummingOutside
	}

	public abstract class Live2DFuckControllerState : UpdatableStateMachineState<Live2DFuckControllerState, CockLimbUsage<T>>
	{
		public T Controller => Owner._controller;
	}

	private class NormalState : Live2DFuckControllerState
	{
		public override void EnterState()
		{
			if (Controller.CockManager.ShouldGetFlacid)
			{
				if (Controller.CurrentBrain?.CurrentState is CowgirlBrainState cowgirlBrainState)
				{
					cowgirlBrainState.FlacidityActivated = true;
				}
				else
				{
					Controller.CockManager.IsFlacid = true;
				}
			}
		}

		public override void ExitState()
		{
		}

		public override void UpdateState()
		{
		}

		public override void FixedUpdateState()
		{
			bool flag = Controller.CockManager.FixedUpdateCumBarValue();
			if (!Controller.CockManager.IsFlacid || Controller.CockManager.AllowFlaccidCumming)
			{
				if (flag)
				{
					StateMachine.ChangeState(new CumImminentState());
				}
				if (!(Controller.ParamFlacidity.GetRealPercent() > 0.05f))
				{
					Owner.UpdateAuto(AutoState.Normal);
				}
			}
		}
	}

	private class CumImminentState : Live2DFuckControllerState
	{
		private float _timer;

		private float _length = 2.5f;

		public override void EnterState()
		{
			Controller.FrickStaticGuiManager.ShowCumImage();
			Controller.FireEvent(InteractionMemoryEventEnum.CumImminent);
		}

		public void End()
		{
			Controller.CockManager.TargetCumBarValue = 1f;
			Controller.CockManager.FixedUpdateCumBarValue();
			float cumml = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.ExtractCumFromBalls();
			StateMachine.ChangeState(new CumState(cumml));
		}

		public override void ExitState()
		{
			Controller.FrickStaticGuiManager.ClearCumBar();
		}

		public override void UpdateState()
		{
			float num = _timer / _length;
			Controller.FrickStaticGuiManager.SetCumImageAlpha(num);
			Controller.FrickStaticGuiManager.FlashRedCumBar(num);
		}

		public override void FixedUpdateState()
		{
			Controller.CockManager.TargetCumBarValue = 1f;
			Controller.CockManager.FixedUpdateCumBarValue();
			_timer += Time.fixedDeltaTime;
			Owner.UpdateAuto(AutoState.CummingImminent);
			if (_timer > _length)
			{
				End();
				_timer = 0f;
			}
		}
	}

	private class CumState : Live2DFuckControllerState
	{
		public const float CumBarValueAfterNutting = 0.2f;

		private Coroutine _corourine;

		private float fullml;

		private float leftml;

		private Dictionary<CumTargetTypeEnum, int> _cumTargetCount = new Dictionary<CumTargetTypeEnum, int>();

		[CanBeNull]
		private Live2DParameter _cumShotLengthParam;

		[CanBeNull]
		private Live2DParameter _cockXParam;

		[CanBeNull]
		private Live2DParameter _cockYParam;

		public CumState(float cumml)
		{
			fullml = (leftml = cumml);
		}

		public override void EnterState()
		{
			InsertionHandler currentInsertionHandler = Owner._currentInsertionHandler;
			if (currentInsertionHandler.IsInside())
			{
				currentInsertionHandler.ForceCockAllTheWayInside();
			}
			else
			{
				currentInsertionHandler.ForceCockAllTheWayOutside();
			}
			Controller.FireEvent(InteractionMemoryEventEnum.CummingStarted);
			_corourine = Controller.StartCoroutine(Logic());
		}

		public override void ExitState()
		{
			Owner._currentInsertionHandler.ResetAccumulatedMovementToCurrentPosition();
			GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
			foreach (KeyValuePair<CumTargetTypeEnum, int> item in _cumTargetCount)
			{
				item.Deconstruct(out var key, out var _);
				switch (key)
				{
				default:
					gameVariables.timesCameInside++;
					break;
				case CumTargetTypeEnum.Anal:
					gameVariables.timesCameInsideAnal++;
					break;
				case CumTargetTypeEnum.Mouth:
					gameVariables.timesCameInMouth++;
					break;
				case CumTargetTypeEnum.Outside:
					gameVariables.timesCameOutside++;
					break;
				case CumTargetTypeEnum.Thighs:
					gameVariables.timesCameThighjob++;
					break;
				}
			}
			gameVariables.botLive2DCommonState.FrickData.D_TimesAnonCame++;
			Controller.FrickStaticGuiManager.HideCumImage();
			Controller.FrickStaticGuiManager.ToggleCumFlashBang(active: false);
			Controller.CockManager.TargetCumBarValue = 0.2f;
			if (_corourine != null)
			{
				Controller.StopCoroutine(_corourine);
			}
		}

		private IEnumerator Logic()
		{
			Live2DParameter cumAnimParam = Controller.ParamCumShotAnim;
			_cumShotLengthParam = (Controller as ILive2DController_Lewd_CumShotLength)?.ParamCumShotLengthY;
			_cockXParam = (Controller as ILive2DController_Lewd_CumShotX)?.ParamCumOutsideX;
			_cockYParam = (Controller as ILive2DController_Lewd_CumShotY)?.ParamCumOutsideY;
			if (!Controller.CockManager.IsFlacid)
			{
				while (leftml > 0f)
				{
					(CumOutsideManager.CumLive2DRepresentation, bool) random = Controller.CumOutsideManager.GetRandom();
					CumOutsideManager.CumLive2DRepresentation target = random.Item1;
					bool areAllCumStainsFull = random.Item2;
					float targetSquirt = leftml * 0.1f + 0.01f;
					targetSquirt += ((!areAllCumStainsFull) ? Random.Range(0f, target.Left) : Random.Range(0f, target.Capacity));
					targetSquirt = Mathf.Min(targetSquirt, leftml);
					float currentDepth = 0f;
					if (!Owner._currentInsertionHandler.TryGetActiveFuckAxis(out var currentAxis) || !currentAxis.IsInside)
					{
						Live2DLewdSceneController.CumOutsideCockValues cumOutsideCockValues = Owner._controller.CalculateCumOutsideAimParameters(target);
						if (_cockXParam != null)
						{
							_cockXParam.Value = cumOutsideCockValues.AngleX + Random.Range(-0.001f, 0.001f);
						}
						if (_cockYParam != null)
						{
							_cockYParam.Value = cumOutsideCockValues.AngleY + Random.Range(-0.001f, 0.001f);
						}
						if (_cumShotLengthParam != null)
						{
							_cumShotLengthParam.SetPercent(cumOutsideCockValues.CumshotLength + Random.Range(-0.03f, 0.03f));
						}
					}
					else
					{
						currentDepth = currentAxis.GetCurrentRealInsertionPosition();
						if (_cockXParam != null)
						{
							_cockXParam.Value = 0f;
						}
						if (_cockYParam != null)
						{
							_cockYParam.Value = 0f;
						}
						if (_cumShotLengthParam != null)
						{
							if (InsertionHandlerExtensions.ShouldCumFarWhenInside(currentAxis.Owner.OrificeType))
							{
								if ((double)Random.value > 0.5)
								{
									_cumShotLengthParam.SetPercent(Random.Range(0f, 1f));
								}
								else
								{
									_cumShotLengthParam.SetPercent(Random.Range(0f, 0.25f));
								}
							}
							else
							{
								_cumShotLengthParam.SetPercent(0.025f);
							}
						}
					}
					if (_cockXParam != null)
					{
						yield return CoroutineUtilities.WaitUntilWithTimeout(_cockXParam.HasFinishedMove);
					}
					if (_cockYParam != null)
					{
						yield return CoroutineUtilities.WaitUntilWithTimeout(_cockYParam.HasFinishedMove);
					}
					yield return WaitForSecondCache.RandomUnderThirdSecond();
					cumAnimParam.SetOnlyRealValue(cumAnimParam.MinimumValue);
					cumAnimParam.Value = cumAnimParam.MaximumValue;
					R.Sound.Frick_cum_All.PlayRandomSlightlyRandomVolume();
					Owner._currentInsertionHandler.ThrustMovementAccumulator += Random.value * 0.01f;
					Controller.FrickStaticGuiManager.ToggleCumFlashBang(active: true);
					yield return WaitForSecondCache.RandomUnderThirdSecond();
					Controller.FrickStaticGuiManager.ToggleCumFlashBang(active: false);
					Owner._currentInsertionHandler.ThrustMovementAccumulator += Random.value * 0.01f;
					yield return CoroutineUtilities.WaitUntilWithTimeout(cumAnimParam.HasFinishedMove);
					cumAnimParam.SetRealValue(cumAnimParam.MinimumValue);
					Owner._currentInsertionHandler.ThrustMovementAccumulator -= Random.value * 0.02f;
					leftml -= targetSquirt;
					Controller.CockManager.TargetCumBarValue = 0.2f + leftml / fullml * 0.8f;
					InsertionHandlerOrificeTypeEnum orificeType = Owner._currentInsertionHandler.OrificeType;
					if (Owner.IsInside() || (orificeType == InsertionHandlerOrificeTypeEnum.Mouth && ((Controller.Mouth.IsOpenEnoughForCum() && (areAllCumStainsFull || Random.value > 0.5f)) ? true : false)))
					{
						Controller.CumInsideManager.CumInside(targetSquirt, currentDepth, orificeType);
						_cumTargetCount.AddToCountDictionary(orificeType.ToCumTarget());
					}
					else
					{
						target.SquirtAndUpdate(targetSquirt);
						Controller.FireEvent(InteractionMemoryEventEnum.CumOutside);
						_cumTargetCount.AddToCountDictionary(CumTargetTypeEnum.Outside);
					}
					if (leftml < 0f)
					{
						break;
					}
				}
			}
			if (_cockXParam != null)
			{
				_cockXParam.Value = 0f;
			}
			if (_cockYParam != null)
			{
				_cockYParam.Value = 0f;
			}
			if (_cumShotLengthParam != null)
			{
				_cumShotLengthParam.Value = 0f;
			}
			yield return CoroutineUtilities.WaitUntilWithTimeout(cumAnimParam.HasFinishedMove);
			StateMachine.ChangeState(new NormalState());
		}

		public override void UpdateState()
		{
			if (!Owner._currentInsertionHandler.TryGetActiveFuckAxis(out var currentAxis))
			{
				return;
			}
			InsertionHandlerOrificeTypeEnum orificeType = currentAxis.Owner.OrificeType;
			bool isInside = currentAxis.IsInside;
			float realDistanceToInsertion = currentAxis.GetRealDistanceToInsertion();
			if (isInside)
			{
				if (_cockXParam != null)
				{
					_cockXParam.SetRealValue(0f);
				}
				if (_cockYParam != null)
				{
					_cockYParam.SetRealValue(0f);
				}
				if (!InsertionHandlerExtensions.ShouldCumFarWhenInside(orificeType) && _cumShotLengthParam != null)
				{
					_cumShotLengthParam.SetPercent(0.025f);
				}
			}
			else if (realDistanceToInsertion < 0.6f)
			{
				if (_cockXParam != null)
				{
					_cockXParam.ClampValue((0f - realDistanceToInsertion) * 0.1f, realDistanceToInsertion * 0.1f);
					_cockXParam.ClampRealValueOnly((0f - realDistanceToInsertion) * 0.5f, realDistanceToInsertion * 0.5f);
				}
				if (_cockYParam != null)
				{
					_cockYParam.ClampValue((0f - realDistanceToInsertion) * 0.1f, realDistanceToInsertion * 0.1f);
					_cockYParam.ClampRealValueOnly((0f - realDistanceToInsertion) * 0.5f, realDistanceToInsertion * 0.5f);
				}
			}
			bool flag = isInside && InsertionHandlerExtensions.ShouldHideCumAnimationIfInside(orificeType);
			bool flag2 = Controller is ILive2DController_Manager_XRay live2DController_Manager_XRay && live2DController_Manager_XRay.XRayManager.IsXrayActiveFor(Owner._currentInsertionHandler);
			Controller.ParamHideCumShot.Set01Bool(flag && !flag2);
			Controller.ParamHideCumShot.FinishMove();
		}

		public override void FixedUpdateState()
		{
			Controller.CockManager.FixedUpdateCumBarValue();
			Controller.FrickStaticGuiManager.SetCumImageAlpha((Controller.CockManager.CumBarValue - 0.2f) / 0.8f);
			Owner.UpdateAuto(Owner._currentInsertionHandler.IsInside() ? AutoState.CummingInside : AutoState.CummingOutside);
		}
	}

	private CockLimbUsageTarget[] _targets;

	private int _targetIndex;

	private InsertionHandler _currentInsertionHandler;

	private InsertionHandlerFuckAxis _currentFuckAxis;

	private T _controller;

	[CanBeNull]
	private Live2DParameter _cockVisibleParameter;

	private ButtonListWrapper _autoButtonWrapper;

	private ButtonListWrapper _switchCockTargetButtonWrapper;

	private AutoTypeEnum _autoStage;

	private bool _autoDirection = true;

	private float _pushTimer;

	public LimbUsageChoiceEnum TargetLimbUsage => LimbUsageChoiceEnum.Cock;

	public AutoTypeEnum AutoStage
	{
		get
		{
			return _autoStage;
		}
		set
		{
			if (value != _autoStage)
			{
				_autoStage = value;
				_autoDirection = true;
			}
		}
	}

	public UpdatableStateMachine<Live2DFuckControllerState, CockLimbUsage<T>> FrickStateMachine { get; private set; }

	public bool IsInCummingState => FrickStateMachine.CurrentState is CumState;

	public bool IsInNormalState
	{
		get
		{
			if (FrickStateMachine.CurrentState != null)
			{
				return FrickStateMachine.CurrentState is NormalState;
			}
			return true;
		}
	}

	public bool CanHideCock => FrickStateMachine.CurrentState is NormalState;

	public CockLimbUsage(T controller, [CanBeNull] Live2DParameter cockVisibleParameter, params CockLimbUsageTarget[] targets)
	{
		_controller = controller;
		_cockVisibleParameter = cockVisibleParameter;
		_targets = targets;
		CopyCurrentTarget();
	}

	private void CopyCurrentTarget()
	{
		CockLimbUsageTarget cockLimbUsageTarget = _targets[_targetIndex];
		_currentInsertionHandler = cockLimbUsageTarget.TargetInsertionHandler;
		_currentFuckAxis = cockLimbUsageTarget.TargetAxis;
	}

	public bool TryGetActiveData(out InsertionHandler insertionHandler, out InsertionHandlerFuckAxis fuckAxis)
	{
		insertionHandler = _currentInsertionHandler;
		fuckAxis = _currentFuckAxis;
		return _currentInsertionHandler.IsCurrentAxis(_currentFuckAxis);
	}

	public void PullOutInstantly()
	{
		_currentInsertionHandler.EndIfCurrentAxis(0);
	}

	public bool TryGetActiveData(out InsertionHandler insertionHandler)
	{
		insertionHandler = _currentInsertionHandler;
		return _currentInsertionHandler.IsCurrentAxis(_currentFuckAxis);
	}

	public bool IsInside()
	{
		if (TryGetActiveData(out var insertionHandler))
		{
			return insertionHandler.IsInside();
		}
		return false;
	}

	public InsertionHandlerOrificeTypeEnum CurrentTargetOrificeType()
	{
		return _currentInsertionHandler.OrificeType;
	}

	public void SelectNextTarget()
	{
		_targetIndex = (_targetIndex + 1) % _targets.Length;
		_currentInsertionHandler.Reset();
		CopyCurrentTarget();
		InitInsertionHandler();
	}

	private void InitInsertionHandler()
	{
		_currentInsertionHandler.SetCurrentAxis(_currentFuckAxis, GameVariables.Current.GetGameStats().OtherFrickGameStats.AnonPenisInfo.Value);
	}

	public void Start()
	{
		InitInsertionHandler();
		AutoStage = AutoTypeEnum.None;
		_cockVisibleParameter?.Set01Bool(value: true);
		_controller.FireEvent(InteractionMemoryEventEnum.Unzip);
		GameVariables.Current.botLive2DCommonState.FrickData.D_UnzippedPantsAlready = true;
		FrickStateMachine = new UpdatableStateMachine<Live2DFuckControllerState, CockLimbUsage<T>>(this, "Cock Limb State Machine");
		FrickStateMachine.ChangeState(new NormalState());
		_controller.FrickStaticGuiManager.UpdateLeftButtonList();
	}

	public void Update()
	{
		FrickStateMachine.Update();
	}

	public void FixedUpdate()
	{
		FrickStateMachine.FixedUpdate();
	}

	public void End(LetGoLimbsCaller caller)
	{
		_currentInsertionHandler.EndIfCurrentAxis(0);
		_cockVisibleParameter?.Set01Bool(value: false);
		_controller.FrickStaticGuiManager.UpdateLeftButtonList();
	}

	private static AutoTypeEnum NextAutoStage(AutoTypeEnum current)
	{
		return current switch
		{
			AutoTypeEnum.None => AutoTypeEnum.Slow, 
			AutoTypeEnum.Slow => AutoTypeEnum.Medium, 
			AutoTypeEnum.Medium => AutoTypeEnum.Fast, 
			_ => AutoTypeEnum.None, 
		};
	}

	private void UpdateAuto(AutoState autoState)
	{
		if (!_controller.CockManager.AutoFuckImplemented || AutoStage == AutoTypeEnum.None)
		{
			return;
		}
		if (AutoStage == AutoTypeEnum.Pullout)
		{
			UpdatePulloutAuto();
			return;
		}
		switch (autoState)
		{
		default:
			UpdateNormalAutoState();
			break;
		case AutoState.CummingImminent:
			if (_autoDirection)
			{
				_currentInsertionHandler.ThrustMovementAccumulator += 0.09f;
			}
			else
			{
				_currentInsertionHandler.ThrustMovementAccumulator -= 0.09f;
			}
			break;
		case AutoState.CummingInside:
			_currentInsertionHandler.ThrustMovementAccumulator += 0.09f;
			break;
		case AutoState.CummingOutside:
			_currentInsertionHandler.ThrustMovementAccumulator -= 0.09f;
			break;
		}
	}

	private void UpdatePulloutAuto()
	{
		float num = 0.15f;
		if (_currentInsertionHandler.TryGetActiveFuckAxis(out var currentAxis))
		{
			_currentInsertionHandler.ThrustMovementAccumulator -= num;
			if (currentAxis.GetCurrentRealInsertionPosition() < -0.9f)
			{
				AutoStage = AutoTypeEnum.None;
				_autoButtonWrapper.UpdateButtonText();
			}
		}
	}

	private void UpdateNormalAutoState()
	{
		float autoDefaultSpeed = GetAutoDefaultSpeed();
		if (!_currentInsertionHandler.TryGetActiveFuckAxis(out var currentAxis))
		{
			return;
		}
		if (_autoDirection)
		{
			_currentInsertionHandler.ThrustMovementAccumulator += autoDefaultSpeed;
			if (!currentAxis.CanThrustForward())
			{
				_pushTimer -= Time.fixedDeltaTime;
				if (_pushTimer < 0f)
				{
					_autoDirection = false;
				}
			}
		}
		else if (currentAxis.CanThrustBackwards())
		{
			_currentInsertionHandler.ThrustMovementAccumulator -= autoDefaultSpeed;
		}
		else
		{
			_pushTimer = Random.Range(0.05f, 0.1f);
			_autoDirection = true;
		}
	}

	private float GetAutoDefaultSpeed()
	{
		return AutoStage switch
		{
			AutoTypeEnum.Slow => 0.02f, 
			AutoTypeEnum.Medium => 0.05f, 
			AutoTypeEnum.Fast => 0.09f, 
			AutoTypeEnum.Pullout => 0.15f, 
			_ => 0f, 
		};
	}

	public void PullOutAuto()
	{
		AutoStage = AutoTypeEnum.Pullout;
		_autoButtonWrapper.UpdateButtonText();
	}

	public void DisableAuto()
	{
		AutoTypeEnum autoStage = AutoStage;
		AutoStage = AutoTypeEnum.None;
		if (autoStage != AutoTypeEnum.None)
		{
			_autoButtonWrapper.UpdateButtonText();
		}
	}

	public void LeftButtonGuiFill(IModificationPeriodButtonList buttonList)
	{
		if (_controller.CockManager.AutoFuckImplemented)
		{
			_autoButtonWrapper = buttonList.AddButton();
			_autoButtonWrapper.TextFunc = () => _autoStage switch
			{
				AutoTypeEnum.Slow => LOC.UI.Frick_Slow, 
				AutoTypeEnum.Medium => LOC.UI.Frick_Medium, 
				AutoTypeEnum.Fast => LOC.UI.Frick_Fast, 
				AutoTypeEnum.Pullout => LOC.UI.UI_PullingOutActionContinuuing, 
				_ => LOC.UI.Frick_Manual, 
			};
			_autoButtonWrapper.OnClick = AutoButtonClicked;
			_autoButtonWrapper.ActiveFunc = () => !_controller.CockManager.IsFlacid;
		}
		_switchCockTargetButtonWrapper = buttonList.AddButton(10);
		_switchCockTargetButtonWrapper.ActiveFunc = () => _controller.InsertionManager.InsertionHandlers.Count > 1;
		_switchCockTargetButtonWrapper.TextFunc = () => _currentInsertionHandler.GetLocalizedStringName();
		_switchCockTargetButtonWrapper.OnClick = NextInsertionHandlerButtonClicked;
	}

	private void AutoButtonClicked()
	{
		if (_controller.CockManager.AutoFuckImplemented)
		{
			AutoStage = NextAutoStage(AutoStage);
			_autoButtonWrapper.UpdateButtonText();
		}
	}

	private void NextInsertionHandlerButtonClicked()
	{
		if (_currentInsertionHandler.IsInside())
		{
			UiOverlay.Instance.ShowFloatingTextAtMouse(LOC.UI.Frick_PullOutFirst);
			return;
		}
		if (IsInCummingState)
		{
			UiOverlay.Instance.ShowFloatingTextAtMouse(LOC.UI.Frick_CantDoThatWhenCumming);
			return;
		}
		SelectNextTarget();
		_switchCockTargetButtonWrapper.UpdateButtonText();
	}

	public bool IfCumImminentSpeedItUp()
	{
		if (FrickStateMachine.CurrentState is CumImminentState cumImminentState)
		{
			cumImminentState.End();
			return true;
		}
		return false;
	}
}
