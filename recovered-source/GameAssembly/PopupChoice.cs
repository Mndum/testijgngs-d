using System;

public class PopupChoice
{
	public enum TriggerType
	{
		Normal,
		PointerDown
	}

	public string Text;

	public Action Action;

	public TriggerType Trigger;

	public CommonButtonColorType ButtonColorType = CommonButtonColorType.Alternative;

	public bool Interactable = true;

	public PopupChoice()
	{
	}

	public PopupChoice(string text)
	{
		Text = text;
	}

	public PopupChoice(string text, Action action, TriggerType trigger = TriggerType.Normal)
	{
		Text = text;
		Action = action;
		Trigger = trigger;
	}

	public PopupChoice(string text, Action action, CommonButtonColorType buttonColorType)
	{
		Text = text;
		Action = action;
		ButtonColorType = buttonColorType;
	}
}
