using System.Linq;
using Fungus;
using UnityEngine;

public class SayDialogMovement : MonoBehaviour
{
	private abstract class Target
	{
		public float Progress;

		public Vector3 StartingPosition;

		public abstract Vector3 GetPosition(Transform transform, RectTransform parent);
	}

	private class Live2DCharacterTarget : Target
	{
		private Live2DController _controller;

		public Live2DCharacterTarget(Live2DController activeModel)
		{
			_controller = activeModel;
		}

		public override Vector3 GetPosition(Transform transform, RectTransform parent)
		{
			return parent.InverseTransformPoint(_controller.GetApproximateMouthPositionPart().CalculateWorldBoundsCenter()) + _controller.GetMouthOffset();
		}
	}

	private class DefaultTarget : Target
	{
		public override Vector3 GetPosition(Transform transform, RectTransform parent)
		{
			return new Vector3(0f, 0f - parent.rect.height / 2f + 100f, 0f);
		}
	}

	[SerializeField]
	private SayDialog _sayDialog;

	private Target _target;

	private RectTransform _parent;

	private const float MoveTime = 0.25f;

	private ISayDialogCharacter _lastCharacter;

	private bool _wasPrimed;

	private void SetTarget(Target target)
	{
		_parent = (RectTransform)transform.parent;
		_target = target;
		_target.Progress = 0f;
		_target.StartingPosition = transform.localPosition;
		Prime();
	}

	private void OnCharacterChanged(SayDialog sayDialog, ISayDialogCharacter character)
	{
		if (sayDialog != _sayDialog)
		{
			return;
		}
		_lastCharacter = character;
		if (character.IsBotCharacter())
		{
			Live2DController live2DController = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.AllCurrentlyUsedControllersFor("bot").FirstOrDefault();
			if (live2DController != null)
			{
				SetTarget(new Live2DCharacterTarget(live2DController));
				return;
			}
		}
		SetTarget(new DefaultTarget());
	}

	public void OnEnable()
	{
		_sayDialog.GetWriter().startPause = 0.25f;
		SayDialogSignals.OnSayDialogCharacterChanged += OnCharacterChanged;
	}

	public void OnDisable()
	{
		SayDialogSignals.OnSayDialogCharacterChanged -= OnCharacterChanged;
		_wasPrimed = false;
	}

	public void Prime()
	{
		if (!_wasPrimed)
		{
			transform.localPosition = _target.GetPosition(transform, _parent);
			_target.Progress = 1f;
			_wasPrimed = true;
		}
	}

	public void Update()
	{
		if (_target != null)
		{
			Transform transform = base.transform;
			Vector3 vector = _target.GetPosition(transform, _parent) + new Vector3(Mathf.Cos(Time.time * 1.15f) * 5f, Mathf.Sin(Time.time * 2f) * 5f, 0f);
			if (_target.Progress < 1f)
			{
				transform.localPosition = Vector3.Lerp(_target.StartingPosition, vector, EasingFunction.EaseInQuad(0f, 1f, _target.Progress));
				_target.Progress += Time.deltaTime / 0.25f;
			}
			else
			{
				transform.localPosition = vector;
			}
		}
	}
}
