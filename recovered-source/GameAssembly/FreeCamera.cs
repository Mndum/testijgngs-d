using System;
using Fungus;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
	private Camera _camera;

	private Rect _allowedBounds;

	private float _startingOrthographicSize;

	private float _cameraZ;

	private Transform _cameraTransform;

	private Rect _cameraAllowedBounds;

	private Vector2 _interestCenter;

	private const float ScalePowConstant = 2f;

	private const int ScaleMaxLevel = 3;

	[NonSerialized]
	public Vector3 OffsetPosition;

	public Vector3? StartedMoveAt;

	private Vector3 CalculateInterestCenterOffset()
	{
		float orthographicSize = _camera.orthographicSize;
		float num = orthographicSize * _camera.aspect;
		return new Vector3(_interestCenter.x * num, _interestCenter.y * orthographicSize, 0f);
	}

	public void InitBounds(View boundsView)
	{
		InitBounds(boundsView, new Vector2(0.5f, 0f));
	}

	public void InitBounds(View boundsView, Vector2 interestCenter)
	{
		_allowedBounds = boundsView.GetViewRect();
		_camera = UiOverlay.Instance.MainCamera;
		_cameraTransform = _camera.transform;
		_cameraZ = _cameraTransform.localPosition.z;
		_startingOrthographicSize = _camera.orthographicSize;
		_interestCenter = interestCenter;
		SyncBounds();
	}

	public void SetZoomLevel(int level)
	{
		SetZoom(Mathf.Pow(2f, level));
	}

	public void SetZoom(float targetZoom)
	{
		SetOrthoSize(_startingOrthographicSize / targetZoom);
	}

	public void SetOrthoSize(float newOrthoSize)
	{
		Vector3 vector = CalculateInterestCenterOffset();
		_camera.orthographicSize = newOrthoSize;
		Vector3 vector2 = CalculateInterestCenterOffset();
		SyncBounds();
		Vector3 vector3 = vector - vector2;
		Vector3 localPosition = _cameraTransform.localPosition;
		localPosition.x += vector3.x;
		localPosition.y += vector3.y;
		_camera.transform.localPosition = _cameraAllowedBounds.ClampPoint(localPosition);
	}

	public void MoveToWorldBounds(Bounds bounds, float expandRatio = 1.4f)
	{
		Vector3 extents = bounds.extents;
		float a = Mathf.Max(extents.x / _camera.aspect, extents.y) * expandRatio;
		_camera.orthographicSize = Mathf.Max(Mathf.Min(a, _startingOrthographicSize), _startingOrthographicSize / Mathf.Pow(2f, 3f));
		SyncBounds();
		MoveToWorldPosition(bounds.center);
	}

	public void MoveToWorldPosition(Vector3 worldPosition)
	{
		Vector3 localPosition = _cameraAllowedBounds.ClampPoint(worldPosition - CalculateInterestCenterOffset());
		localPosition.z = _cameraZ;
		_cameraTransform.localPosition = localPosition;
	}

	private void SyncBounds()
	{
		float orthographicSize = _camera.orthographicSize;
		float num = orthographicSize * _camera.aspect;
		_cameraAllowedBounds = new Rect(_allowedBounds.x + num, _allowedBounds.y + orthographicSize, _allowedBounds.width - num * 2f, _allowedBounds.height - orthographicSize * 2f);
	}

	private Vector3 MousePosCamera()
	{
		return _cameraTransform.localPosition - UiOverlay.Instance.MouseToWorld();
	}

	public void UpdatePosition()
	{
		Vector3 vector = MousePosCamera();
		if (!StartedMoveAt.HasValue)
		{
			if (Input.GetMouseButtonDown(0) && !UiOverlay.Instance.IsPointerOverGameObject())
			{
				StartedMoveAt = MousePosCamera();
				OffsetPosition = _cameraTransform.localPosition;
			}
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 vector2 = vector - StartedMoveAt.Value;
			Vector3 point = new Vector3(OffsetPosition.x + vector2.x, OffsetPosition.y + vector2.y, OffsetPosition.z);
			_cameraTransform.localPosition = _cameraAllowedBounds.ClampPoint(point);
		}
		else
		{
			StartedMoveAt = null;
		}
	}

	public void StopMove()
	{
		StartedMoveAt = null;
	}
}
