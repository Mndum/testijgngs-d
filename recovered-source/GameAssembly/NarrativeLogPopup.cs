using Fungus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class NarrativeLogPopup : PrefabPopup<NarrativeLogPopup>
{
	[SerializeField]
	private TextMeshProUGUI narrativeText;

	private ScrollRect _scrollRect;

	public override bool DontDrawPrevious => true;

	public override bool CanClose()
	{
		return true;
	}

	public override void Open()
	{
		base.Open();
		StringBuilderCache.AcquireWithUsing(out var stringBuilder);
		stringBuilder.AppendLine(LOC.UI.NarrativeLog_GameOnFor(GlobalFunctions.TimePeriod(Mathf.RoundToInt(Time.realtimeSinceStartup / 60f))));
		foreach (Line recentLine in FungusManager.Instance.NarrativeLog.GetRecentLines())
		{
			ISayDialogCharacter character = recentLine.character;
			if (character != null && !string.IsNullOrWhiteSpace(character.NameText))
			{
				stringBuilder.Append("<b>");
				stringBuilder.AppendColorTagStart(character.NameColor);
				stringBuilder.Append(character.NameText);
				stringBuilder.AppendColorTagEnd();
				stringBuilder.Append("</b>: ");
			}
			stringBuilder.Append(recentLine.text);
			stringBuilder.Append("</color></b></i>");
			stringBuilder.AppendLine();
		}
		narrativeText.SetText(stringBuilder);
		LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
		_scrollRect = GetComponentInChildren<ScrollRect>();
		_scrollRect.ScrollToBottom();
		_scrollRect.verticalScrollbar.value = 0f;
	}

	public void Update()
	{
		Vector2 mouseScrollDelta = Input.mouseScrollDelta;
		if (mouseScrollDelta.y != 0f && Application.isFocused)
		{
			UiOverlay instance = UiOverlay.Instance;
			if (IsPopupAtTheTop() && instance.IsMouseInsideWindow() && !instance.IsEditingInputField() && _scrollRect.IsAtTheBottom() && mouseScrollDelta.y < 0f)
			{
				CloseFromUIOverlay();
			}
		}
	}
}
