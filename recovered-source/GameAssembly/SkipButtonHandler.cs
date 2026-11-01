using UnityEngine;
using UnityEngine.EventSystems;

public class SkipButtonHandler : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IUpdateSelectedHandler
{
	private enum SkipButtonState
	{
		Nothing,
		PressedWaiting,
		PressedSingleClick,
		PressedDoubleClick
	}

	private SayDialogPopup _parent;

	private int clickCount;

	private SkipButtonState buttonState;

	private float lastClickTime;

	private const float DoubleClickTime = 0.4f;

	private int frameCount;

	private void Awake()
	{
		_parent = GetComponentInParent<SayDialogPopup>();
		if (_parent == null)
		{
			Debug.LogError("SkipButtonHandler: SayDialogPopup not found in parent!", this);
			enabled = false;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		buttonState = SkipButtonState.PressedWaiting;
		if (OptionsStatic.SkipUnseenTextPref.Value)
		{
			buttonState = SkipButtonState.PressedDoubleClick;
		}
		else
		{
			GlobalFunctions.ButtonClickingInARow_Click(ref clickCount, ref lastClickTime, 0.4f);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		buttonState = SkipButtonState.Nothing;
	}

	public void OnUpdateSelected(BaseEventData data)
	{
		if (buttonState == SkipButtonState.PressedWaiting && GlobalFunctions.ButtonClickingInARow_TimeoutDone(lastClickTime))
		{
			frameCount = 0;
			if (clickCount == 1)
			{
				buttonState = SkipButtonState.PressedSingleClick;
			}
			else if (clickCount >= 2)
			{
				buttonState = SkipButtonState.PressedDoubleClick;
			}
			else
			{
				buttonState = SkipButtonState.Nothing;
			}
		}
	}

	private void UpdateKeyPress()
	{
		if ((buttonState == SkipButtonState.PressedDoubleClick || buttonState == SkipButtonState.PressedSingleClick) && !Input.GetMouseButton(0))
		{
			buttonState = SkipButtonState.Nothing;
		}
	}

	private void OnDisable()
	{
		UpdateKeyPress();
	}

	private void OnEnable()
	{
		UpdateKeyPress();
	}

	private void FixedUpdate()
	{
		UpdateKeyPress();
		if (frameCount % 3 == 0 || Input.GetKey(KeyCode.LeftShift))
		{
			if (buttonState == SkipButtonState.PressedDoubleClick)
			{
				_parent.NextLineSkip(forced: true);
			}
			else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || buttonState == SkipButtonState.PressedSingleClick)
			{
				_parent.NextLineSkip(forced: false);
			}
		}
		frameCount++;
	}
}
