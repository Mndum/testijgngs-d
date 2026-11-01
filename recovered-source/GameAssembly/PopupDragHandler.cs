using UnityEngine;
using UnityEngine.EventSystems;

public class PopupDragHandler : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	private RectTransform relatedPopupParentRt;

	private UIPopup relatedPopup;

	private RectTransform relatedPopupRt;

	private Vector3 startPositionMouse;

	private Vector3 startPositionPopup;

	public void Awake()
	{
		relatedPopup = gameObject.GetAncestor<UIPopup>();
		relatedPopupRt = relatedPopup.transform.AsRectTransform();
		relatedPopupParentRt = relatedPopupRt.parent.AsRectTransform();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		startPositionPopup = relatedPopupRt.localPosition;
		startPositionMouse = relatedPopupParentRt.InverseTransformPoint(UiOverlay.Instance.MouseToWorld());
	}

	public void OnDrag(PointerEventData eventData)
	{
		Rect rect = relatedPopupParentRt.rect;
		Rect rect2 = relatedPopupRt.rect;
		Vector2 vector = rect.min - rect2.min;
		Vector2 vector2 = rect.max - rect2.max;
		Vector3 vector3 = relatedPopupParentRt.InverseTransformPoint(UiOverlay.Instance.MouseToWorld()) - startPositionMouse;
		Vector3 vector4 = startPositionPopup + vector3;
		relatedPopupRt.localPosition = new Vector3(Mathf.Clamp(vector4.x, vector.x, vector2.x), Mathf.Clamp(vector4.y, vector.y, vector2.y));
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Vector3 localPosition = relatedPopupRt.localPosition;
		relatedPopupRt.localPosition = new Vector3(Mathf.Round(localPosition.x), Mathf.Round(localPosition.y), Mathf.Round(localPosition.z));
	}
}
