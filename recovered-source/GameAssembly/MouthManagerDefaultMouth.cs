using UnityEngine;

public sealed class MouthManagerDefaultMouth : Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_FixedUpdate, Live2DController.ILive2DControllerManager_Callback, Live2DController.ILive2DControllerManager_ResetStage1
{
	private readonly ILive2DController_Mouth _face;

	private readonly Live2DController _controller;

	private float _mouthOpenTimer;

	public bool DisableLetterOutputToMouth { get; set; }

	private MouthManagerDefaultMouth(ILive2DController_Mouth face)
	{
		_face = face;
	}

	public static MouthManagerDefaultMouth Create<T>(T controller) where T : Live2DController, ILive2DController_Mouth
	{
		return new MouthManagerDefaultMouth(controller);
	}

	public void ResetStage1()
	{
		DisableLetterOutputToMouth = false;
	}

	public void SayLetter(char letter)
	{
		if (!DisableLetterOutputToMouth)
		{
			float mouthOpen = GetMouthOpen(letter);
			_face.ParamMouthOpen.Value = mouthOpen * 0.7f;
		}
	}

	public void SayEnd()
	{
		if (!DisableLetterOutputToMouth)
		{
			if (_face.ParamMouthOpen.Value > 0.9f)
			{
				_face.ParamMouthOpen.Value = 1f;
			}
			else
			{
				_face.ParamMouthOpen.Value = 0f;
			}
		}
	}

	public void FixedUpdate()
	{
		if (_face.ParamMouthOpen.Value > 1f)
		{
			_mouthOpenTimer += Time.fixedDeltaTime;
			if (_mouthOpenTimer > 15f && _face is ILive2DController_SalivaMouth live2DController_SalivaMouth)
			{
				live2DController_SalivaMouth.ParamSaliva.SetMaxValue();
			}
		}
		else
		{
			_mouthOpenTimer = 0f;
		}
	}

	public void CloseMouth()
	{
		_mouthOpenTimer = 0f;
		_face.ParamMouthOpen.Value = 0f;
	}

	public void OpenMouthHalf()
	{
		_face.ParamMouthOpen.Value = 1f;
	}

	public void SetMouth(float value)
	{
		_face.ParamMouthOpen.Value = value;
	}

	public void SetMouthAtLeast(float value)
	{
		_face.ParamMouthOpen.Value = Mathf.Max(value, _face.ParamMouthOpen.Value);
	}

	public void OpenMouthFull()
	{
		_face.ParamMouthOpen.Value = 2f;
	}

	public void MakeSureMouthAtLeastHalfOpen()
	{
		if (_face.ParamMouthOpen.Value < 1f)
		{
			_face.ParamMouthOpen.Value = 1f;
		}
	}

	public bool IsOpenEnoughForCum()
	{
		return _face.ParamMouthOpen.Value >= 0.7f;
	}

	private static float GetMouthOpen(char letter)
	{
		if (char.IsNumber(letter))
		{
			return 0.3f + Random.Range(0.1f, 0.5f);
		}
		if (!char.IsLetter(letter))
		{
			return 0f;
		}
		if ("aeiouAEIOU".IndexOf(letter) >= 0)
		{
			if (char.IsUpper(letter))
			{
				return 0.8f;
			}
			return 1f;
		}
		if (".,".IndexOf(letter) >= 0)
		{
			return 0f;
		}
		return Random.Range(0f, 1f);
	}
}
