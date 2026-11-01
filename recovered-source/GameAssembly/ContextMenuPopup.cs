using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuPopup : PrefabPopup<ContextMenuPopup>
{
	[SerializeField]
	private ButtonList buttonList;

	public ButtonList ButtonList => buttonList;

	public void Open(List<PopupChoice> choices)
	{
		Open(UiOverlay.Instance.MouseToWorld(), choices);
	}

	public void Open(Vector2 position, List<PopupChoice> choices)
	{
		buttonList.InitializeFromPopupChoices(choices, CloseFromUIOverlay);
		float num = 0f;
		foreach (ButtonListWrapper button in buttonList.Buttons)
		{
			float unclampedTargetWidth = button.GetUnclampedTargetWidth();
			if (num < unclampedTargetWidth)
			{
				num = unclampedTargetWidth;
			}
		}
		num = Mathf.Max(num, 150f);
		VerticalLayoutGroup component = GetComponent<VerticalLayoutGroup>();
		RectTransform rectTransform = (RectTransform)transform;
		rectTransform.sizeDelta = new Vector2(num + (float)component.padding.left + (float)component.padding.right, rectTransform.sizeDelta.y);
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		RectTransform rectTransform2 = (RectTransform)rectTransform.parent;
		Rect rect = rectTransform2.rect;
		Rect rect2 = rectTransform.rect;
		Vector2 vector = rect.min - rect2.min + new Vector2(10f, 10f);
		Vector2 vector2 = rect.max - rect2.max - new Vector2(10f, 10f);
		Vector3 vector3 = rectTransform2.InverseTransformPoint(position) + new Vector3(10f, -10f, 0f);
		rectTransform.localPosition = new Vector3(Mathf.Clamp(vector3.x, vector.x, vector2.x), Mathf.Clamp(vector3.y, vector.y, vector2.y));
		Open();
	}
}
