using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FrickStaticGui : StaticGuiBase
{
	public enum RightGuiState
	{
		ChatVisible,
		ChatHidden,
		OpeningChat,
		ClosingChat
	}

	[Header("Prefabs")]
	[SerializeField]
	private GameObject commentPrefab;

	[Header("Right GUI")]
	[SerializeField]
	private RectTransform rightGui;

	[SerializeField]
	private Slider cumBar;

	[SerializeField]
	private Button mainReturnButton;

	[SerializeField]
	private ButtonList buttonList;

	[SerializeField]
	private RectTransform newChat;

	[SerializeField]
	private Button chatCloseButton;

	[SerializeField]
	private RectTransform commentHolder;

	private RightGuiState _rightGuiState;

	private float _newChatWidth;

	[Header("Stream Info Window")]
	[SerializeField]
	private Transform streamInfoWindow;

	[SerializeField]
	private TextMeshProUGUI viewersInfo;

	[SerializeField]
	private TextMeshProUGUI followersInfo;

	[SerializeField]
	private TextMeshProUGUI subsInfo;

	[Header("Donations")]
	[SerializeField]
	private RectTransform donationTransform;

	[SerializeField]
	private TextMeshProUGUI donationMessageText;

	[SerializeField]
	private TextMeshProUGUI donationValueText;

	[Header("Other")]
	[SerializeField]
	private Button xrayToggle;

	[SerializeField]
	private Image xrayOnImage;

	[SerializeField]
	private Image xrayOffImage;

	[SerializeField]
	private Image cumImage;

	[SerializeField]
	private ButtonList leftButtonList;

	[Header("Auto")]
	[SerializeField]
	private Image A_Flashbang_Image;

	public GameObject CommentPrefab => commentPrefab;

	public Slider CumBar => cumBar;

	public Button MainReturnButton => mainReturnButton;

	public ButtonList RightButtonList => buttonList;

	public RectTransform CommentHolder => commentHolder;

	public Transform StreamInfoWindow => streamInfoWindow;

	public TextMeshProUGUI ViewersInfo => viewersInfo;

	public TextMeshProUGUI FollowersInfo => followersInfo;

	public TextMeshProUGUI SubsInfo => subsInfo;

	public RectTransform DonationTransform => donationTransform;

	public TextMeshProUGUI DonationMessageText => donationMessageText;

	public TextMeshProUGUI DonationValueText => donationValueText;

	public Button XrayToggle => xrayToggle;

	public Image XrayOnImage => xrayOnImage;

	public Image XrayOffImage => xrayOffImage;

	public Image CumImage => cumImage;

	public Image FlashBangImage => A_Flashbang_Image;

	public ButtonList LeftButtonList => leftButtonList;

	public bool IsDefaultUIActive
	{
		get
		{
			if (XrayToggle.gameObject.activeSelf && LeftButtonList.gameObject.activeSelf)
			{
				return CumBar.gameObject.activeSelf;
			}
			return false;
		}
	}

	public event Action<RightGuiState> OnRightGuiStateChanged;

	public void SetCumBarActive(bool active)
	{
		CumBar.gameObject.SetActive(active);
	}

	private void PrepareStreamGui(bool streamOn)
	{
		_newChatWidth = newChat.rect.width;
		donationTransform.gameObject.SetActive(value: false);
		if (streamOn)
		{
			newChat.gameObject.SetActive(value: true);
			streamInfoWindow.gameObject.SetActive(value: true);
			chatCloseButton.onClick.SetListener(() =>
			{
				SetRightGuiState(RightGuiState.ClosingChat);
			});
			SetRightGuiState(RightGuiState.ChatVisible);
		}
		else
		{
			newChat.gameObject.SetActive(value: false);
			streamInfoWindow.gameObject.SetActive(value: false);
			SetRightGuiState(RightGuiState.ChatHidden);
		}
	}

	public void SetRightGuiState(RightGuiState state)
	{
		_rightGuiState = state;
		switch (state)
		{
		case RightGuiState.ChatVisible:
			rightGui.SetAnchoredPositionX(0f);
			break;
		case RightGuiState.ChatHidden:
			rightGui.SetAnchoredPositionX(_newChatWidth);
			break;
		}
		OnRightGuiStateChanged?.Invoke(_rightGuiState);
	}

	public bool IsChatVisible()
	{
		if (_rightGuiState != RightGuiState.ChatVisible)
		{
			return _rightGuiState == RightGuiState.OpeningChat;
		}
		return true;
	}

	public void SetXrayButtonActive(bool active)
	{
		XrayToggle.gameObject.SetActive(active);
	}

	public void SetLeftButtonsActive(bool active)
	{
		LeftButtonList.gameObject.SetActive(active);
	}

	public void SyncCumbar(float newValue)
	{
		cumBar.value = newValue;
		Vector2 pivot = new Vector2(0f, -0f);
		RectTransform component = cumBar.GetComponent<RectTransform>();
		if (cumBar.value > 0.5f)
		{
			float num = (cumBar.value - 0.5f) * 5f;
			Rect rect = component.rect;
			float num2 = num / rect.width;
			float num3 = num / rect.height;
			float x = pivot.x + UnityEngine.Random.Range(0f - num2, num2);
			float y = pivot.y + UnityEngine.Random.Range(0f - num3, num3);
			component.pivot = new Vector2(x, y);
		}
		else
		{
			component.pivot = pivot;
		}
	}

	public void SetDefaultUIActive(bool active, bool noXray = false)
	{
		if (!noXray)
		{
			SetXrayButtonActive(active);
		}
		SetLeftButtonsActive(active);
		SetCumBarActive(active);
	}

	public void InitGui(UnityAction onReturnButtonClicked, bool streamOn, bool defaultUiActive = true)
	{
		SetDefaultUIActive(defaultUiActive);
		mainReturnButton.onClick.SetListener(onReturnButtonClicked);
		PrepareStreamGui(streamOn);
	}

	public void FixedUpdate()
	{
		float speed = 15f;
		switch (_rightGuiState)
		{
		case RightGuiState.ClosingChat:
			if (rightGui.MoveAnchoredPositionTowardsX(_newChatWidth, speed))
			{
				_rightGuiState = RightGuiState.ChatHidden;
			}
			break;
		case RightGuiState.OpeningChat:
			if (rightGui.MoveAnchoredPositionTowardsX(0f, speed))
			{
				_rightGuiState = RightGuiState.ChatVisible;
			}
			break;
		}
	}

	public void EndGui()
	{
		buttonList.RemoveAllButtonSources();
		buttonList.ResetButtons();
	}

	protected override void FillReferencesInherited()
	{
		Transform transform = base.transform;
		if (transform.EnsureComponentInChild<RectTransform>(out rightGui, "Right GUI"))
		{
			if (rightGui.EnsureChild(out var child, "Column"))
			{
				child.EnsureComponentInChild<Slider>(out cumBar, "Cum Bar");
				child.EnsureComponentInChild<Button>(out mainReturnButton, "Return Button");
				child.EnsureComponentInChild<ButtonList>(out buttonList, "Dynamic Buttons");
			}
			if (rightGui.EnsureComponentInChild<RectTransform>(out newChat, "New Chat"))
			{
				newChat.EnsureComponentInChild<Button>(out chatCloseButton, "Return Button");
				if (newChat.EnsureChildrenNested(out var child2, "ScrollView", "Comments"))
				{
					child2.EnsureComponent<RectTransform>(out commentHolder);
				}
			}
		}
		if (transform.EnsureChild(out streamInfoWindow, "Stream Info Window") && streamInfoWindow.EnsureChild(out var child3, "Content"))
		{
			child3.EnsureComponentInChildChildren<TextMeshProUGUI>(out viewersInfo, "Viewers");
			child3.EnsureComponentInChildChildren<TextMeshProUGUI>(out followersInfo, "Followers");
			child3.EnsureComponentInChildChildren<TextMeshProUGUI>(out subsInfo, "Subs");
		}
		if (transform.EnsureComponentInChild<RectTransform>(out donationTransform, "Donation"))
		{
			donationTransform.EnsureComponentInChild<TextMeshProUGUI>(out donationMessageText, "Message");
			donationTransform.EnsureComponentInChildChildren<TextMeshProUGUI>(out donationValueText, "Box");
		}
		if (transform.EnsureChild(out var child4, "Xray Toggle"))
		{
			child4.EnsureComponent<Button>(out xrayToggle);
			child4.EnsureComponentInChild<Image>(out xrayOnImage, "On");
			child4.EnsureComponentInChild<Image>(out xrayOffImage, "Off");
		}
		transform.EnsureComponentInChild<Image>(out cumImage, "Cum Image");
	}
}
