public abstract class ButtonListButtonSource
{
	private ButtonList _buttonList;

	public ButtonList ConnectedButtonList => _buttonList;

	public abstract void AddButtonsToList(IModificationPeriodButtonList buttonList);

	public void UpdateConnectedButtonList()
	{
		_buttonList.ResetAndUpdateEverything();
	}

	public void SetConnectedButtonList(ButtonList buttonList)
	{
		_buttonList = buttonList;
	}

	public virtual void OnAfterAdded()
	{
	}

	public virtual void OnBeforeRemoved()
	{
	}

	public void Remove()
	{
		if (_buttonList != null)
		{
			_buttonList.RemoveButtonSource(this);
			_buttonList = null;
		}
	}
}
