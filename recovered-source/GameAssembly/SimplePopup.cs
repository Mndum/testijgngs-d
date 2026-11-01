using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimplePopup : PrefabPopup<SimplePopup>
{
	[SerializeField]
	private RectTransform scrollView;

	[SerializeField]
	private TextMeshProUGUI titleTmp;

	[SerializeField]
	private TextMeshProUGUI textTmp;

	[SerializeField]
	private ButtonList buttonList;

	public CursorSingleton.CursorOverride _cursorOverride;

	private float MaxPopupHeight = 700f;

	private float MinPopupHeight = 250f;

	private float CenterTextPopupHeightMax = 300f;

	private int CenterTextLengthMax = 100;

	public ButtonList GetButtonList()
	{
		return buttonList;
	}

	public void AddLinkHandler(Action<string> OnLinkClicked)
	{
		textTmp.gameObject.AddComponent<TextMeshProUGUILinkHandler>().OnLinkClicked = OnLinkClicked;
		textTmp.raycastTarget = true;
	}

	public void AddLinkHandler(Dictionary<string, Action> linkIdToAction)
	{
		AddLinkHandler((string s) =>
		{
			if (linkIdToAction.TryGetValue(s, out var value))
			{
				value?.Invoke();
			}
		});
	}

	private void UpdateSize()
	{
		if (scrollView.anchorMax != Vector2.one || scrollView.anchorMin != Vector2.zero || scrollView.pivot != new Vector2(0f, 1f))
		{
			Debug.LogError("Scroll view anchors or pivots are incorrect! Padding might not be accurate!");
		}
		float num = Mathf.Max(0f - scrollView.sizeDelta.y, 0f);
		RectTransform rectTransform = transform.AsRectTransform();
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		float y = textTmp.GetPreferredValues().y;
		float num2 = MinPopupHeight - num;
		textTmp.gameObject.AddComponent<LayoutElement>().minHeight = num2;
		float num3 = Mathf.Clamp(y + num + 5f, MinPopupHeight, MaxPopupHeight);
		textTmp.rectTransform.SetSizeDeltaY(Mathf.Max(num2, y));
		rectTransform.SetSizeDeltaY(num3);
		if (num3 < CenterTextPopupHeightMax && textTmp.text.Length < CenterTextLengthMax)
		{
			textTmp.horizontalAlignment = HorizontalAlignmentOptions.Center;
			textTmp.verticalAlignment = VerticalAlignmentOptions.Middle;
		}
	}

	public void Open(string title, string text, List<PopupChoice> choices)
	{
		titleTmp.text = title;
		textTmp.text = text;
		buttonList.InitializeFromPopupChoices(choices, CloseFromUIOverlay);
		Open();
		UpdateSize();
		CursorSingleton.ContinueOrStartCursorOverride(ref _cursorOverride, CursorSingleton.CursorType.Normal);
	}

	public void OnDestroy()
	{
		CursorSingleton.EndCursorOverride(ref _cursorOverride);
	}
}
