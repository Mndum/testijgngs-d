using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipDisplayAreaTextLink : TooltipDisplayAreaBase, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerMoveHandler
{
	public delegate string GetLinkTextDelegate(string linkId);

	[SerializeField]
	private TMP_Text textComponent;

	private string currentLinkId;

	private string currentContent;

	public GetLinkTextDelegate TextFunc { get; set; }

	public void OnPointerEnter(PointerEventData eventData)
	{
		CheckLinkUnderPointer(eventData);
	}

	public void OnPointerMove(PointerEventData eventData)
	{
		CheckLinkUnderPointer(eventData);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ClearLinkHover();
	}

	private void CheckLinkUnderPointer(PointerEventData eventData)
	{
		if (TextFunc == null)
		{
			Debug.Log("No text func provided for " + name + "!");
			ClearLinkHover();
			return;
		}
		int num = TMP_TextUtilities.FindIntersectingLink(textComponent, eventData.position, eventData.pressEventCamera);
		if (num != -1)
		{
			TMP_LinkInfo tMP_LinkInfo = textComponent.textInfo.linkInfo[num];
			string linkID = tMP_LinkInfo.GetLinkID();
			if (!string.IsNullOrEmpty(linkID))
			{
				if (linkID != currentLinkId)
				{
					currentLinkId = linkID;
					currentContent = TextFunc(linkID);
					if (!string.IsNullOrWhiteSpace(currentContent))
					{
						OnEnter();
					}
					else
					{
						OnExit();
					}
				}
				return;
			}
		}
		ClearLinkHover();
	}

	private void ClearLinkHover()
	{
		if (currentLinkId != null)
		{
			currentLinkId = null;
			currentContent = null;
			OnExit();
		}
	}

	protected override void SetTooltipText(Tooltip tooltip)
	{
		if (currentLinkId != null && TextFunc != null)
		{
			currentContent = TextFunc(currentLinkId);
			tooltip.SetText(currentContent);
		}
	}
}
