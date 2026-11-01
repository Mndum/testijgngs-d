using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ButtonList : MonoBehaviour, IModificationPeriodButtonList
{
	public struct ButtonListInitialization : IDisposable
	{
		private ButtonList _targetButtonList;

		public ButtonListInitialization(ButtonList targetButtonList)
		{
			_targetButtonList = targetButtonList;
			targetButtonList.ResetButtons_Inner();
		}

		public void Dispose()
		{
			_targetButtonList.FinishUpdatingEverything();
		}
	}

	private static BetterObjectPool<ButtonListWrapper> buttonWrapperPool = new BetterObjectPool<ButtonListWrapper>();

	private List<ButtonListButtonSource> _buttonSources = new List<ButtonListButtonSource>();

	private List<ButtonListWrapper> _buttons = new List<ButtonListWrapper>();

	[SerializeField]
	private PooledPrefabReference<CommonButtonHandler> overrideButtonContainer;

	public IEnumerable<ButtonListWrapper> Buttons => _buttons;

	public void AddButtonSource(ButtonListButtonSource buttonSource)
	{
		if (_buttonSources.Contains(buttonSource))
		{
			Debug.LogError($"{buttonSource} is already in list {name}");
			return;
		}
		buttonSource.SetConnectedButtonList(this);
		_buttonSources.Add(buttonSource);
		buttonSource.OnAfterAdded();
	}

	public void RemoveButtonSource(ButtonListButtonSource buttonSource)
	{
		buttonSource.OnBeforeRemoved();
		buttonSource.SetConnectedButtonList(null);
		_buttonSources.Remove(buttonSource);
	}

	public void RemoveAllButtonSources()
	{
		foreach (ButtonListButtonSource buttonSource in _buttonSources)
		{
			buttonSource.OnBeforeRemoved();
			buttonSource.SetConnectedButtonList(null);
		}
		_buttonSources.Clear();
	}

	private CommonButtonHandler GetButton(Transform t, CommonButtonColorType ct)
	{
		PrefabPool<CommonButtonHandler> prefabPool = overrideButtonContainer.GetPoolOrNull();
		if (prefabPool == null)
		{
			prefabPool = UiOverlay.Instance.commonButton.GetPool();
		}
		CommonButtonHandler fromPool = prefabPool.GetFromPool(t);
		fromPool.SetColorType(ct);
		return fromPool;
	}

	public void OnDestroy()
	{
		if (gameObject.scene.isLoaded)
		{
			ResetButtons_Inner();
		}
	}

	private ButtonListWrapper AddButtonInner(CommonButtonHandler createdButton, int order, CommonButtonHandler.CommonButtonSizeEnum size)
	{
		ButtonListWrapper buttonListWrapper = buttonWrapperPool.Acquire();
		buttonListWrapper.Init(order, createdButton, size);
		_buttons.Add(buttonListWrapper);
		return buttonListWrapper;
	}

	ButtonListWrapper IModificationPeriodButtonList.AddButton(int order, CommonButtonColorType buttonColorType)
	{
		CommonButtonHandler button = GetButton(transform, buttonColorType);
		return AddButtonInner(button, order, CommonButtonHandler.CommonButtonSizeEnum.Normal);
	}

	ButtonListWrapper IModificationPeriodButtonList.AddCommonSmall(int order, CommonButtonColorType buttonColorType)
	{
		CommonButtonHandler button = GetButton(transform, buttonColorType);
		return AddButtonInner(button, order, CommonButtonHandler.CommonButtonSizeEnum.Smaller);
	}

	private float DetermineMaxWidth()
	{
		float num = ((RectTransform)transform).rect.width;
		if ((double)num <= 1.0)
		{
			num = 3.4028235E+38f;
		}
		return num;
	}

	public void UpdateButtonSingle(ButtonListWrapper buttonListWrapper)
	{
		float maxWidth = DetermineMaxWidth();
		if (!_buttons.Contains(buttonListWrapper))
		{
			Debug.LogError("Trying to update button list which does not appear in the button list " + name + "!");
		}
		else
		{
			buttonListWrapper.UpdateButtonInner(maxWidth);
		}
	}

	[MustDisposeResource]
	public ButtonListInitialization DoInitialization(out IModificationPeriodButtonList modificationPeriod)
	{
		modificationPeriod = this;
		return new ButtonListInitialization(this);
	}

	private void ResetButtons_Inner()
	{
		foreach (ButtonListWrapper button in _buttons)
		{
			button.UnwrapAndReturnButtonToPool();
			buttonWrapperPool.Release(button);
		}
		_buttons.Clear();
	}

	public void ResetButtons()
	{
		ResetButtons_Inner();
	}

	private void FinishUpdatingEverything()
	{
		foreach (ButtonListButtonSource buttonSource in _buttonSources)
		{
			buttonSource.AddButtonsToList(this);
		}
		UpdateCurrentButtonState();
	}

	public void ResetAndUpdateEverything()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (DoInitialization(out modificationPeriod))
		{
		}
	}

	public void UpdateCurrentButtonState()
	{
		float maxWidth = DetermineMaxWidth();
		_buttons.Sort((ButtonListWrapper a, ButtonListWrapper b) => a.Order.CompareTo(b.Order));
		for (int num = 0; num < _buttons.Count; num++)
		{
			ButtonListWrapper buttonListWrapper = _buttons[num];
			buttonListWrapper.UpdateButtonInner(maxWidth);
			buttonListWrapper.SetSiblingIndex(num);
		}
	}

	public void ShowAsContextMenu()
	{
		List<PopupChoice> list = (from t in _buttons
			where t.IsButtonActiveAndInteractable() && t.OnClick != null
			select new PopupChoice(t.GetCurrentText(), t.OnClick)).ToList();
		if (list.Count > 0)
		{
			UiOverlay.Instance.OpenContextMenuPopup(list);
		}
	}

	public void InitializeFromPopupChoices(List<PopupChoice> choices, Action after)
	{
		IModificationPeriodButtonList modificationPeriod;
		using (DoInitialization(out modificationPeriod))
		{
			foreach (PopupChoice choice in choices)
			{
				modificationPeriod.AddButton(0, choice.ButtonColorType).ApplyChoice(choice, after);
			}
		}
	}
}
