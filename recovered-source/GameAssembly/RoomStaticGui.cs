using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomStaticGui : StaticGuiBase
{
	[SerializeField]
	private PooledPrefabReference<RoomButtonPooledPrefab> buttonPrefab;

	private RoomButtonPooledPrefab _interactionButton;

	private const string ComputerButtonName = "Computer Button";

	private const string SleepButtonName = "Sleep Button";

	private const string OutsideButtonName = "Outside Button";

	private const string ItemsButtonName = "Items Button";

	private const string EquipmentButtonName = "Equipment Button";

	private const string ClothesButtonName = "Clothes Button";

	private const string StatusButtonName = "Status Button";

	private const string InteractButtonName = "Interact Button";

	private const string ShowerButtonName = "Shower Button";

	[NonSerialized]
	private List<RoomButtonPooledPrefab> _currentRoomButtons = new List<RoomButtonPooledPrefab>();

	private readonly RoomSheep roomSheep = new RoomSheep();

	[Header("Auto")]
	[SerializeField]
	private BetterButton A_Save_button;

	[SerializeField]
	private BetterButton A_Cheats_button;

	[SerializeField]
	private BetterButton A_Main_Menu_button;

	public Button SleepButton => GetButtonByName("Sleep Button");

	public Button OutsideButton => GetButtonByName("Outside Button");

	public Button ItemsButton => GetButtonByName("Items Button");

	public RoomSheep RoomSheep => roomSheep;

	public void UpdateInteractButtonPosition(Live2DInteractionController targetController)
	{
		Vector3 position = targetController.transform.parent.position;
		Camera mainCamera = UiOverlay.Instance.MainCamera;
		Vector3 vector = mainCamera.WorldToScreenPoint(position);
		RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, vector, mainCamera, out var localPoint);
		float y = (targetController.ParamBlanketSitting.IsApproximatelyMaximumValue ? 350f : 650f);
		Vector2 size = new Vector2(160f, y);
		localPoint.x -= size.x / 2f;
		localPoint.y += 300f;
		_interactionButton.SetLocalPosition(localPoint, size);
	}

	private Button GetButtonByName(string buttonName)
	{
		return _currentRoomButtons.First((RoomButtonPooledPrefab x) => x.name == buttonName).Button;
	}

	private void SaveButtonClicked()
	{
		List<PopupChoice> list = new List<PopupChoice>();
		list.Add(new PopupChoice(LOC.Polyglot.MENU_IO_SAVE_GAME, UiOverlay.Instance.OpenSaveMenu));
		list.Add(new PopupChoice(LOC.Polyglot.MENU_IO_LOAD_GAME, UiOverlay.Instance.OpenLoadMenu));
		UiOverlay.Instance.OpenContextMenuPopup(list);
	}

	private RoomButtonPooledPrefab AddRoomButton(Rect rect, string buttonName, string text, UnityAction onClick)
	{
		return AddRoomButton(rect, buttonName, text, onClick, new Vector2(0.5f, 0.5f));
	}

	private RoomButtonPooledPrefab AddRoomButton(Rect rect, string buttonName, string text, UnityAction onClick, Vector2 indicatorCenter)
	{
		RoomButtonPooledPrefab fromPool = buttonPrefab.GetFromPool(transform);
		fromPool.SetAnchoredPositionToRect(rect);
		fromPool.name = buttonName;
		fromPool.ButtonText.text = text;
		fromPool.Button.onClick.SetListener(onClick);
		fromPool.SetIndicatorCenter(indicatorCenter);
		_currentRoomButtons.Add(fromPool);
		return fromPool;
	}

	private void Awake()
	{
		A_Save_button.onClick.SetListener(SaveButtonClicked);
		A_Main_Menu_button.onClick.SetListener(SingletonLoadableMonoBehaviour<GameScript>.Instance.ReturnToMainMenuPrompt);
		A_Cheats_button.onClick.SetListener(RoomSheep.RoomCheatsButtonClicked);
	}

	public void OnEnable()
	{
		UpdateButtons();
	}

	public void UpdateButtons()
	{
		buttonPrefab.GetPool().InitOrResetPooledList(ref _currentRoomButtons);
		GameVariables current = GameVariables.Current;
		AddRoomButton(new Rect(0f, -680f, 503f, 400f), "Computer Button", LOC.UI.Room_PC, () =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.StartPc();
		});
		AddRoomButton(new Rect(784f, -880f, 320f, 320f), "Sleep Button", LOC.UI.Room_Sleep, () =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GoToSleep();
		});
		AddRoomButton(new Rect(503f, -149f, 139f, 767f), "Outside Button", LOC.UI.Room_Outside, () =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GoOutside();
		});
		Rect rect = new Rect(755f, 0f, 560f, 255f);
		if (current.HasMetAnnalie)
		{
			Rect rect2;
			(rect, rect2) = rect.SplitVerticallyByRightWidth(200f);
			AddRoomButton(rect2, "Clothes Button", LOC.UI.Room_Clothes, () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.StartEquipmentClothes();
			});
		}
		AddRoomButton(rect, "Items Button", LOC.UI.Room_Items, () =>
		{
			UiOverlay.Instance.OpenItemsMenu();
		});
		AddRoomButton(new Rect(1325f, 0f, 362f, 255f), "Equipment Button", LOC.UI.Room_Equipment, () =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.StartEquipment();
		});
		AddRoomButton(new Rect(1529f, -433f, 325f, 320f), "Status Button", LOC.UI.Room_Status, () =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ShowStatus();
		});
		_interactionButton = AddRoomButton(new Rect(854f, -498f, 258f, 418f), "Interact Button", LOC.UI.Room_Interact, () =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.StartInteract();
		}, new Vector2(0.5f, 1f));
		if (current.BathroomDoorInstalled)
		{
			AddRoomButton(new Rect(1805f, -145f, 125f, 900f), "Shower Button", LOC.UI.Room_Shower, () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.StartBathroom();
			});
		}
		A_Cheats_button.gameObject.SetActive(OptionsStatic.AnyCheatsEnabled);
	}
}
