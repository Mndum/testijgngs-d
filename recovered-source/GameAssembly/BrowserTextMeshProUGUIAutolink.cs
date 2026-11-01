using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BrowserTextMeshProUGUIAutolink : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private TextMeshProUGUI textMeshProUGUI;

	private bool _ignoreNextChange;

	private static Regex _linkRegex = new Regex("(?<!\")\\b\\w{2,15}\\.\\w{2,}(\\/\\w+)*(?:\\?\\w{1,20}=\\w{1,50}(?:&\\w{1,20}=\\w{1,50})*)?");

	private void OnValidate()
	{
		GetReference();
	}

	private void GetReference()
	{
		textMeshProUGUI = GetComponent<TextMeshProUGUI>();
	}

	private void TextChanged(object o)
	{
		if (_ignoreNextChange)
		{
			return;
		}
		string text = "<noparse>";
		if (!(o is TextMeshProUGUI textMeshProUGUI) || !(textMeshProUGUI == this.textMeshProUGUI))
		{
			return;
		}
		string text2 = this.textMeshProUGUI.text;
		if (text2 != null && !text2.EndsWith(text))
		{
			bool found = false;
			string text3 = _linkRegex.Replace(text2, (Match match) =>
			{
				found = true;
				return BrowserPopup.FormatLink(match.Value);
			});
			if (found)
			{
				_ignoreNextChange = true;
				this.textMeshProUGUI.text = text3 + text;
				this.textMeshProUGUI.ForceMeshUpdate();
				_ignoreNextChange = false;
			}
		}
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
				Debug.Log("Opening link " + linkID);
				UiOverlay.Instance.OpenBrowser(linkID);
			}
		}
	}

	public void OnEnable()
	{
		GetReference();
		TMPro_EventManager.TEXT_CHANGED_EVENT.Add(TextChanged);
	}

	public void OnDisable()
	{
		TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(TextChanged);
	}
}
