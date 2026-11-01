using System.Collections;
using UnityEngine;

public abstract class TooltipDisplayAreaBase : MonoBehaviour
{
	private Tooltip _tooltip;

	private ManagedCoroutine _coroutine = new ManagedCoroutine();

	private void EnsureTooltip()
	{
		if (_tooltip == null)
		{
			_tooltip = UiOverlay.Instance.defaultTooltip.GetPool().GetFromPool(transform.GetRootCanvas().transform);
		}
	}

	private void DisableTooltip()
	{
		UiOverlay.Instance.defaultTooltip.GetPool().ReturnToPool(_tooltip);
		_tooltip = null;
	}

	private void OnDisable()
	{
		OnExit();
	}

	private void OnDestroy()
	{
		OnExit();
	}

	protected abstract void SetTooltipText(Tooltip tooltip);

	private IEnumerator Logic()
	{
		if (0f > 0.1f && !Input.anyKeyDown)
		{
			yield return null;
		}
		EnsureTooltip();
		SetTooltipText(_tooltip);
		RectTransform rt = (RectTransform)_tooltip.transform;
		RectTransform parentRt = (RectTransform)rt.parent;
		while (!Input.anyKeyDown)
		{
			Rect rect = parentRt.rect;
			Rect rect2 = rt.rect;
			Vector2 vector = rect.min - rect2.min;
			Vector2 vector2 = rect.max - rect2.max;
			Vector3 vector3 = UiOverlay.Instance.MouseToViewport();
			Vector3 vector4;
			if (vector3.y > vector3.x)
			{
				vector4 = new Vector3(25f, -25f, 0f);
				rt.pivot = new Vector2(0f, 1f);
			}
			else
			{
				vector4 = new Vector3(-25f, 25f, 0f);
				rt.pivot = new Vector2(1f, 0f);
			}
			Vector3 vector5 = parentRt.InverseTransformPoint(UiOverlay.Instance.MouseToWorld()) + vector4;
			rt.localPosition = new Vector3(Mathf.Clamp(vector5.x, vector.x, vector2.x), Mathf.Clamp(vector5.y, vector.y, vector2.y));
			yield return null;
		}
	}

	protected void OnEnter()
	{
		_coroutine.Start(this, Logic(), DisableTooltip);
	}

	protected void OnExit()
	{
		_coroutine.Stop(this);
	}

	public void ForceTextUpdate()
	{
		if (_tooltip != null)
		{
			SetTooltipText(_tooltip);
		}
	}
}
