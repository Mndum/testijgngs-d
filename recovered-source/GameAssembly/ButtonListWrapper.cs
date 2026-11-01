using System;

public class ButtonListWrapper
{
	private CommonButtonHandler _buttonHandler;

	private CommonButtonHandler.CommonButtonSizeEnum _targetButtonSize;

	private Action _onClick;

	public int Order { get; set; }

	public Func<string> TextFunc { get; set; }

	public Func<bool> ActiveFunc { get; set; }

	public bool Interactable
	{
		get
		{
			return _buttonHandler.Button.interactable;
		}
		set
		{
			_buttonHandler.Button.interactable = value;
		}
	}

	public Action OnClick
	{
		get
		{
			return _onClick;
		}
		set
		{
			_onClick = value;
			SetActive(value: true);
			_buttonHandler.SetOnClick(value.Invoke);
		}
	}

	public void Init(int order, CommonButtonHandler buttonHandler, CommonButtonHandler.CommonButtonSizeEnum buttonSizeEnum)
	{
		Order = order;
		_buttonHandler = buttonHandler;
		buttonHandler.SetSize(buttonSizeEnum);
	}

	public void UnwrapAndReturnButtonToPool()
	{
		_buttonHandler.ReturnToPool();
		_buttonHandler = null;
		Order = 0;
		_targetButtonSize = CommonButtonHandler.CommonButtonSizeEnum.Normal;
		TextFunc = null;
		ActiveFunc = null;
		_onClick = null;
	}

	public void Click()
	{
		OnClick?.Invoke();
	}

	public float GetUnclampedTargetWidth()
	{
		return _buttonHandler.UnclampedTargetWidth;
	}

	public string GetCurrentText()
	{
		return _buttonHandler.ButtonText.text;
	}

	public bool IsButtonActive()
	{
		return _buttonHandler.Button.isActiveAndEnabled;
	}

	public bool IsButtonActiveAndInteractable()
	{
		if (IsButtonActive())
		{
			return Interactable;
		}
		return false;
	}

	public void SetTextFuncLocalizedString(LOS.TupleReference tableEntryReference)
	{
		TextFunc = tableEntryReference.Get;
	}

	public void SetText(string text)
	{
		_buttonHandler.ButtonText.SetText(text);
	}

	private void SetActive(bool value)
	{
		_buttonHandler.gameObject.SetActive(value);
	}

	public void UpdateButtonText()
	{
		if (TextFunc != null)
		{
			_buttonHandler.ButtonText.SetText(TextFunc());
		}
	}

	public void UpdateButtonInner(float maxWidth)
	{
		if (ActiveFunc == null || ActiveFunc())
		{
			UpdateButtonText();
			_buttonHandler.SetSize(_targetButtonSize);
			_buttonHandler.PrepareTextLayoutElement(maxWidth);
			SetActive(value: true);
		}
		else
		{
			SetActive(value: false);
		}
	}

	public void SetSiblingIndex(int index)
	{
		_buttonHandler.transform.SetSiblingIndex(index);
	}

	public void ApplyChoice(PopupChoice choice, Action after)
	{
		PopupChoice.TriggerType trigger = choice.Trigger;
		if (trigger == PopupChoice.TriggerType.Normal || trigger != PopupChoice.TriggerType.PointerDown)
		{
			OnClick = () =>
			{
				choice.Action?.Invoke();
				after();
			};
		}
		else
		{
			_buttonHandler.SetOnPointerDown(() =>
			{
				choice.Action?.Invoke();
				after();
			});
		}
		SetText(choice.Text);
		Interactable = choice.Interactable;
	}

	public CommonButtonHandler GetButtonDebugOnly()
	{
		return _buttonHandler;
	}
}
