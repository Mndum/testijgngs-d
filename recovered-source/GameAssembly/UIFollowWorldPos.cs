using UnityEngine;

public class UIFollowWorldPos : MonoBehaviour
{
	private Transform _target;

	private Transform _parent;

	private RectTransform _thisTransform;

	public void Initialize(Transform target)
	{
		_target = target;
		_parent = transform.parent;
		_thisTransform = (RectTransform)transform;
	}

	public void Update()
	{
		Vector3 position = _target.position;
		Vector3 localPosition = _parent.InverseTransformPoint(position);
		_thisTransform.localPosition = localPosition;
	}
}
