using Fungus;
using UnityEngine;

public class ParalaxManager : MonoBehaviour
{
	public View paralaxBounds;

	private ParalaxReceiver[] receivers = new ParalaxReceiver[0];

	private Vector3 _lastPosition;

	private Vector2 _paralaxOffset;

	private Vector2 _target;

	private Rect _paralaxBoundsRect;

	public void Awake()
	{
		receivers = GetComponentsInChildren<ParalaxReceiver>();
	}

	public void OnEnable()
	{
		_lastPosition = UiOverlay.Instance.MouseToWorld();
		_paralaxOffset = new Vector2(0f, 0f);
		_target = Vector2.zero;
	}

	private void CalculateBounds()
	{
		Camera mainCamera = UiOverlay.Instance.MainCamera;
		float orthographicSize = mainCamera.orthographicSize;
		float num = orthographicSize * mainCamera.aspect;
		_paralaxBoundsRect = paralaxBounds.GetViewRect();
		_paralaxBoundsRect = new Rect(_paralaxBoundsRect.x + num, _paralaxBoundsRect.y + orthographicSize, _paralaxBoundsRect.width - num * 2f, _paralaxBoundsRect.height - orthographicSize * 2f);
		_paralaxBoundsRect.center = Vector2.zero;
	}

	public void Update()
	{
		Vector3 vector = UiOverlay.Instance.MouseToWorld();
		Vector3 vector2 = vector - _lastPosition;
		_lastPosition = vector;
		if (!UiOverlay.Instance.IsOpen)
		{
			Vector2 vector3 = new Vector2(0.02f, 0.01f);
			float deltaTime = Time.deltaTime;
			_paralaxOffset.x = Mathf.MoveTowards(_paralaxOffset.x + vector2.x * vector3.x, _target.x, (deltaTime + Mathf.Abs(_paralaxOffset.x)) * vector3.x * 0.1f);
			_paralaxOffset.y = Mathf.MoveTowards(_paralaxOffset.y + vector2.y * vector3.y, _target.y, (deltaTime + Mathf.Abs(_paralaxOffset.y)) * vector3.y * 0.1f);
			CalculateBounds();
			_paralaxOffset = _paralaxBoundsRect.ClampPoint(_paralaxOffset);
			ParalaxReceiver[] array = receivers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ApplyParalax(_paralaxOffset);
			}
		}
	}
}
