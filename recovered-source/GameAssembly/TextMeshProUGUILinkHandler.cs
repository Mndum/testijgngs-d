using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProUGUILinkHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private TextMeshProUGUI textMeshProUGUI;

	public Action<string> OnLinkClicked;

	private void OnValidate()
	{
		GetReference();
	}

	private void GetReference()
	{
		textMeshProUGUI = GetComponent<TextMeshProUGUI>();
	}

	private void OnEnable()
	{
		GetReference();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		int num = TMP_TextUtilities.FindIntersectingLink(textMeshProUGUI, eventData.position, eventData.pressEventCamera);
		if (num != -1)
		{
			TMP_LinkInfo tMP_LinkInfo = textMeshProUGUI.textInfo.linkInfo[num];
			string linkID = tMP_LinkInfo.GetLinkID();
			if (linkID != "")
			{
				OnLinkClicked?.Invoke(linkID);
			}
		}
	}
}
