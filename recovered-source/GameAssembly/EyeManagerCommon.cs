using UnityEngine;

public sealed class EyeManagerCommon : Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_FixedUpdate, Live2DController.ILive2DControllerManager_Callback, Live2DController.ILive2DControllerManager_ResetStage1, Live2DController.ILive2DControllerManager_StartUsageGameplay
{
	private readonly Live2DController _controller;

	private bool heartEyesTargetState;

	private float heartEyesTimeLeft;

	private const float MinHeartEyesLength = 5f;

	private float _shockEyesValue;

	private bool _eyesClosed;

	private float _blinkTimer;

	private float _blinkUpdateTime = 2f;

	public bool DisableBlinking { get; set; }

	public bool IsAlive { get; set; }

	private EyeManagerCommon(Live2DController controller)
	{
		_controller = controller;
		if (!(controller is ILive2DController_EyeLMovement) && !(controller is ILive2DController_EyeRMovement))
		{
			Debug.LogWarning("No eye movement in " + controller.name + "!");
		}
	}

	public static EyeManagerCommon Create<T>(T controller) where T : Live2DController, ILive2DController_IndependentBlinking
	{
		return new EyeManagerCommon(controller);
	}

	public static EyeManagerCommon CreateNoBlinkLeftEyeOnly<T>(T controller) where T : Live2DController, ILive2DController_EyeLCloseAndShock
	{
		return new EyeManagerCommon(controller);
	}

	public static EyeManagerCommon CreateNoBlinkRightEyeOnly<T>(T controller) where T : Live2DController, ILive2DController_EyeRCloseAndShock
	{
		return new EyeManagerCommon(controller);
	}

	public static EyeManagerCommon CreateNoBlinkBothEyes<T>(T controller) where T : Live2DController, ILive2DController_EyeLCloseAndShock, ILive2DController_EyeRCloseAndShock
	{
		return new EyeManagerCommon(controller);
	}

	public void SetHeartEyesTarget(bool enable)
	{
		heartEyesTargetState = enable;
		if (enable)
		{
			SetHeartEyes_Inner(value: true);
			heartEyesTimeLeft = 5f;
		}
	}

	private void SetHeartEyes_Inner(bool value)
	{
		if (_controller is ILive2DController_BlushAndHeartEyes live2DController_BlushAndHeartEyes)
		{
			live2DController_BlushAndHeartEyes.ParamHeart.Set01Bool(value);
		}
	}

	private void FixedUpdateHeartEyes()
	{
		heartEyesTimeLeft -= Time.fixedDeltaTime;
		if (!heartEyesTargetState && heartEyesTimeLeft <= 0f)
		{
			SetHeartEyes_Inner(value: false);
		}
	}

	private void SetLeftEyePosition(Vector2 pos)
	{
		if (_controller is ILive2DController_EyeLMovement live2DController_EyeLMovement)
		{
			live2DController_EyeLMovement.ParamEyeballLX.Value = pos.x;
			live2DController_EyeLMovement.ParamEyeballLY.Value = pos.y;
		}
	}

	private void SetRightEyePosition(Vector2 pos)
	{
		if (_controller is ILive2DController_EyeRMovement live2DController_EyeRMovement)
		{
			live2DController_EyeRMovement.ParamEyeballRX.Value = pos.x;
			live2DController_EyeRMovement.ParamEyeballRY.Value = pos.y;
		}
	}

	private Vector2 GetLeftEyePosition()
	{
		if (_controller is ILive2DController_EyeLMovement live2DController_EyeLMovement)
		{
			return new Vector2(live2DController_EyeLMovement.ParamEyeballLX.Value, live2DController_EyeLMovement.ParamEyeballLY.Value);
		}
		return Vector2.zero;
	}

	private Vector2 GetRightEyePosition()
	{
		if (_controller is ILive2DController_EyeRMovement live2DController_EyeRMovement)
		{
			return new Vector2(live2DController_EyeRMovement.ParamEyeballRX.Value, live2DController_EyeRMovement.ParamEyeballRY.Value);
		}
		return Vector2.zero;
	}

	public void CloseEyes()
	{
		_eyesClosed = true;
		_blinkUpdateTime = Random.Range(0.1f, 0.15f);
		_blinkTimer = 0f;
		if (_controller is ILive2DController_IndependentBlinking live2DController_IndependentBlinking)
		{
			live2DController_IndependentBlinking.ParamEyeOpen.SetMinValue();
		}
		else
		{
			SetShockEyes_Inner(-1f);
		}
	}

	public void CloseEyesForSeconds(float seconds)
	{
		_eyesClosed = true;
		_blinkUpdateTime = seconds;
		_blinkTimer = 0f;
		if (_controller is ILive2DController_IndependentBlinking live2DController_IndependentBlinking)
		{
			live2DController_IndependentBlinking.ParamEyeOpen.SetMinValue();
		}
		else
		{
			SetShockEyes_Inner(-1f);
		}
	}

	public void HalfCloseEyes(float percentage)
	{
		_eyesClosed = true;
		_blinkUpdateTime = Random.Range(0.1f, 0.15f);
		_blinkTimer = 0f;
		if (_controller is ILive2DController_IndependentBlinking live2DController_IndependentBlinking)
		{
			live2DController_IndependentBlinking.ParamEyeOpen.SetMaxValue();
		}
		SetShockEyes_Inner(0f - percentage);
	}

	public void OpenEyes()
	{
		_eyesClosed = false;
		_blinkUpdateTime = Random.Range(5f, 7f);
		_blinkTimer = 0f;
		if (_controller is ILive2DController_IndependentBlinking live2DController_IndependentBlinking)
		{
			live2DController_IndependentBlinking.ParamEyeOpen.Value = 0f;
		}
		else
		{
			UpdateShockEyes();
		}
	}

	private void UpdateShockEyes()
	{
		if (!_eyesClosed)
		{
			SetShockEyes_Inner(_shockEyesValue);
		}
	}

	private void SetShockEyes_Inner(float value)
	{
		if (_controller is ILive2DController_EyeLCloseAndShock live2DController_EyeLCloseAndShock)
		{
			live2DController_EyeLCloseAndShock.ParamEyeLShock.Value = value;
		}
		if (_controller is ILive2DController_EyeRCloseAndShock live2DController_EyeRCloseAndShock)
		{
			live2DController_EyeRCloseAndShock.ParamEyeRShock.Value = value;
		}
	}

	public void SetShockEyesValue(float value)
	{
		_shockEyesValue = value;
		UpdateShockEyes();
	}

	public void ResetStage1()
	{
		_eyesClosed = false;
		_blinkTimer = 0f;
		_blinkUpdateTime = 2f;
		DisableBlinking = false;
		IsAlive = false;
	}

	public void StartUsageGameplay()
	{
		IsAlive = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotAwake;
	}

	public void FixedUpdate()
	{
		if (!IsAlive)
		{
			OpenEyes();
			return;
		}
		FixedUpdateHeartEyes();
		if (DisableBlinking)
		{
			return;
		}
		_blinkTimer += Time.fixedDeltaTime;
		if (_eyesClosed)
		{
			if (_blinkTimer > _blinkUpdateTime)
			{
				OpenEyes();
				if (_controller is ILive2DController_CatEarsMovement live2DController_CatEarsMovement)
				{
					live2DController_CatEarsMovement.ParamEarsWiggle.Value = 0f;
				}
			}
		}
		else if (_blinkTimer > _blinkUpdateTime)
		{
			CloseEyes();
			if ((!(_controller is ILive2DController_Headpat live2DController_Headpat) || !live2DController_Headpat.ParamHeadpat.Exists || live2DController_Headpat.ParamHeadpat.IsApproximatelyMinimalValue) && _controller is ILive2DController_CatEarsMovement live2DController_CatEarsMovement2)
			{
				live2DController_CatEarsMovement2.ParamEarsWiggle.Value = 1f;
			}
			_blinkTimer = 0f;
		}
	}

	public static Vector2 CalculateEyePositionFromCenter(Vector2 center, float x, float y)
	{
		return new Vector2(GlobalFunctions.CalculateOffsetFromCenter(-1f, 1f, center.x, x), GlobalFunctions.CalculateOffsetFromCenter(-1f, 1f, center.y, y));
	}

	public static Vector2 CalculateEyeInputFromCenterOffset(Vector2 center, Vector2 position)
	{
		return new Vector2(GlobalFunctions.CalculateInputFromCenterOffset(-1f, 1f, center.x, position.x), GlobalFunctions.CalculateInputFromCenterOffset(-1f, 1f, center.y, position.y));
	}

	public void ToDefaultEyesPosition()
	{
		SetEyesPosition(0f, 0f);
	}

	public void SetEyesPosition(float x, float y, Live2DController.EyeSelection eyeSelection = Live2DController.EyeSelection.BothEyes)
	{
		Vector2 defaultEyesPosition = _controller.DefaultEyesPosition;
		switch (eyeSelection)
		{
		case Live2DController.EyeSelection.LeftEye:
			SetLeftEyePosition(CalculateEyePositionFromCenter(defaultEyesPosition, x, y));
			break;
		case Live2DController.EyeSelection.RightEye:
			SetRightEyePosition(CalculateEyePositionFromCenter(defaultEyesPosition, x, y));
			break;
		case Live2DController.EyeSelection.BothEyesTarget:
		{
			float num = 1f;
			SetLeftEyePosition(CalculateEyePositionFromCenter(defaultEyesPosition, (x - num) * 0.8f, y));
			SetRightEyePosition(CalculateEyePositionFromCenter(defaultEyesPosition, (x + num) * 0.8f, y));
			break;
		}
		default:
		{
			Vector2 vector = CalculateEyePositionFromCenter(defaultEyesPosition, x, y);
			SetLeftEyePosition(vector);
			SetRightEyePosition(vector);
			break;
		}
		}
	}

	public Vector2 GetMouseOffsetFromEyes()
	{
		Vector2 result = UiOverlay.Instance.MouseToWorld() - _controller.GetApproximateEyesPositionPart().CalculateWorldBoundsCenter();
		result.Normalize();
		return result;
	}

	public void LookAtCursor()
	{
		Vector2 mouseOffsetFromEyes = GetMouseOffsetFromEyes();
		SetEyesPosition(mouseOffsetFromEyes.x, mouseOffsetFromEyes.y);
	}

	public bool TryLookAtCurrentInteractable()
	{
		if (!_controller.TryGetCurrentlyHoveredOrHeldInteractablePosition(out var position))
		{
			return false;
		}
		Vector3 vector = _controller.GetApproximateEyesPositionPart().CalculateWorldBoundsCenter();
		Vector2 vector2 = position - vector;
		vector2.y *= 2f;
		vector2.Normalize();
		SetEyesPosition(vector2.x, vector2.y);
		return true;
	}

	public Vector2 GetLeftEyeOffsetFromCenter()
	{
		return CalculateEyeInputFromCenterOffset(_controller.DefaultEyesPosition, GetLeftEyePosition());
	}
}
