using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipDisplayArea : TooltipDisplayAreaBase, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[TextArea]
	public string tooltipText;

	public Func<string> TextFunc { get; set; }

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnEnter();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnExit();
	}

	private string GetText()
	{
		if (TextFunc != null)
		{
			return TextFunc();
		}
		return tooltipText;
	}

	protected override void SetTooltipText(Tooltip tooltip)
	{
		tooltip.SetText(GetText());
	}
}
