using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStaticGui : StaticGuiBase
{
	[SerializeField]
	private RectTransform background;

	private float defaultSizeDeltaX;

	private float openSizeDeltaX;

	private ManagedCoroutine _coroutine = new ManagedCoroutine();

	private int _clickCount;

	private float _lastClickTime;

	private const int ClickToUnlockDebug = 8;

	private string[] funnyVersionNames = new string[9] { "nice cock edition", "sheep was here", "see you space cowboy", "SiMonk0 was here", "<color=yellow>jan pawel drugi edition", "177013", "why are we still here?", "tell your mum you love her!", "too cool for steam" };

	[SerializeField]
	private TextMeshProUGUI versionText;

	[SerializeField]
	private Button versionButton;

	[SerializeField]
	private Transform buttonHolder;

	[Header("Auto")]
	[SerializeField]
	private RectTransform A_InfoPopup;

	[SerializeField]
	private TextMeshProUGUI A_InfoPopupTitle;

	[SerializeField]
	private TextMeshProUGUI A_InfoPopupTextTextMeshProUGUI;

	[SerializeField]
	private BetterButton A_InfoPopupTextBetterButton;

	[SerializeField]
	private TextMeshProUGUILinkHandler A_InfoPopupTextTextMeshProUGUILinkHandler;

	[SerializeField]
	private BetterButton A_New_game;

	[SerializeField]
	private BetterButton A_Load_game;

	[SerializeField]
	private BetterButton A_Cheats;

	[SerializeField]
	private BetterButton A_Other;

	[SerializeField]
	private BetterButton A_Exit;

	public Button NewGameButton => A_New_game;

	public TextMeshProUGUI VersionText => versionText;

	public Button VersionButton => versionButton;

	public Transform ButtonHolder => buttonHolder;

	private void ResetToDefault()
	{
		background.sizeDelta = new Vector2(defaultSizeDeltaX, background.sizeDelta.y);
	}

	public void Awake()
	{
		defaultSizeDeltaX = background.sizeDelta.x;
		openSizeDeltaX = 0f;
	}

	private void VersionButtonClicked()
	{
		GlobalFunctions.ButtonClickingInARow_Click(ref _clickCount, ref _lastClickTime);
		R.Sound.Frick_cum_All.GetRandom().Get().Play((float)_clickCount / 8f);
		if (_clickCount >= 8)
		{
			UiOverlay.Instance.OpenCheatsPopup();
			_clickCount = 0;
		}
	}

	private void UpdateVersionText()
	{
		string text = "Version " + GameVariables.CurrentGameVersion;
		if (OptionsStatic.MinutesSpentPlaying >= 300 && Random.value > 0.995f)
		{
			text = text + " - " + funnyVersionNames.GetRandom();
		}
		versionText.text = text;
		versionButton.onClick.SetListener(VersionButtonClicked);
	}

	public void OnEnable()
	{
		UiOverlay.Instance.OnUiOverlayStatusChanged += UIOverlayStatusChanged;
		SingletonMonoBehaviour<ItchIoSingleton>.Instance.OnItchIoVersionResponse += UpdateOutdatedVersionInfo;
		ResetToDefault();
		SingletonMonoBehaviour<ItchIoSingleton>.Instance.MakeSureGotLatestVersionFromItchIo();
		UpdateVersionText();
		UpdateOutdatedVersionInfo();
		A_Exit.gameObject.SetActive(Application.platform != RuntimePlatform.WebGLPlayer);
		A_New_game.onClick.SetListener(StartButtonClicked);
		A_Load_game.onClick.SetListener(LoadButtonClicked);
		A_Other.onClick.SetListener(OtherButtonClicked);
		A_Cheats.onClick.SetListener(CheatsButtonClicked);
	}

	private void StartButtonClicked()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.StartPrologue();
	}

	private void LoadButtonClicked()
	{
		UiOverlay.Instance.OpenLoadMenu();
	}

	private void CheatsButtonClicked()
	{
		UiOverlay.Instance.OpenCheatsPopup();
	}

	private void OtherButtonClicked()
	{
		UiOverlay.Instance.OpenContextMenuPopup(new List<PopupChoice>
		{
			new PopupChoice(LOC.UI.UI_Achievements, UiOverlay.Instance.OpenAchievements),
			new PopupChoice(LOC.Polyglot.MENU_LABEL_CREDITS, SingletonLoadableMonoBehaviour<GameScript>.Instance.RollCredits)
		});
	}

	private void UpdateOutdatedVersionInfo()
	{
		ItchIoSingleton instance = SingletonMonoBehaviour<ItchIoSingleton>.Instance;
		if (instance.TryGetVersionFromItchIo(out var value))
		{
			ParsedVersion target = ParsedVersion.CreateFromString(value);
			if (ParsedVersion.CurrentGameVersion.CompareWith(target).IsOlder)
			{
				A_InfoPopupTitle.text = LOC.Polyglot.MENU_LABEL_WARNING;
				StringBuilder stringBuilder;
				using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
				{
					stringBuilder.AppendLine(LOC.UI.OutdatedVersionPrompt);
					stringBuilder.AppendLine(LOC.UI.DownloadLatestPublicItchIo(value));
					stringBuilder.AppendColorTagStart(Color.red);
					stringBuilder.Append("(");
					stringBuilder.Append(LOC.UI.ClickToOpen);
					stringBuilder.Append(")");
					stringBuilder.AppendColorTagEnd();
					A_InfoPopupTextTextMeshProUGUI.SetText(stringBuilder);
					A_InfoPopupTextTextMeshProUGUILinkHandler.OnLinkClicked = null;
					A_InfoPopupTextBetterButton.onClick.SetListener(() =>
					{
						SingletonLoadableMonoBehaviour<GameScript>.Instance.OpenItchIO("outdatedInfo");
					});
					A_InfoPopup.gameObject.SetActive(value: true);
					return;
				}
			}
		}
		if (instance.IsInPaidEarlyAccessPeriod())
		{
			A_InfoPopupTitle.text = LOC.Polyglot.MENU_LABEL_THANK_YOU;
			StringBuilder stringBuilder2;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder2))
			{
				using (stringBuilder2.StartColorTagUsing(Color.green))
				{
					stringBuilder2.AppendLine(LOC.UI.UI_EarlyAccessThanks);
				}
				stringBuilder2.AppendLine();
				stringBuilder2.AppendLine(LOC.UI.UI_EarlyAccessWarning);
				stringBuilder2.Append("<u>");
				stringBuilder2.AppendLine(LOC.UI.UI_EarlyAccessPirateWarning);
				stringBuilder2.Append("</u>");
				stringBuilder2.AppendLine();
				stringBuilder2.AppendLine(LOC.UI.UI_EarlyAccessOfficialSources);
				using (stringBuilder2.StartColorTagUsing(Color.green))
				{
					stringBuilder2.TMPAppendLinkTagWithString("p", "Patreon (" + LOC.UI.ClickToOpen + ")");
					stringBuilder2.AppendLine();
				}
				using (stringBuilder2.StartColorTagUsing(Color.green))
				{
					stringBuilder2.TMPAppendLinkTagWithString("s", "SubscribeStar (" + LOC.UI.ClickToOpen + ")");
					stringBuilder2.AppendLine();
				}
				A_InfoPopupTextTextMeshProUGUI.SetText(stringBuilder2);
				A_InfoPopupTextTextMeshProUGUILinkHandler.OnLinkClicked = (string x) =>
				{
					if (!(x == "p"))
					{
						if (x == "s")
						{
							SingletonLoadableMonoBehaviour<GameScript>.Instance.OpenSubscribeStar("maybe_arrr");
						}
					}
					else
					{
						SingletonLoadableMonoBehaviour<GameScript>.Instance.OpenPatreon("maybe_arrr");
					}
				};
				A_InfoPopupTextBetterButton.onClick.RemoveAllListeners();
				A_InfoPopup.gameObject.SetActive(value: true);
				return;
			}
		}
		A_InfoPopup.gameObject.SetActive(value: false);
		A_InfoPopupTextTextMeshProUGUILinkHandler.OnLinkClicked = null;
		A_InfoPopupTextBetterButton.onClick.RemoveAllListeners();
	}

	public void OnDisable()
	{
		if (gameObject.scene.isLoaded)
		{
			UiOverlay.Instance.OnUiOverlayStatusChanged -= UIOverlayStatusChanged;
			SingletonMonoBehaviour<ItchIoSingleton>.Instance.OnItchIoVersionResponse -= UpdateOutdatedVersionInfo;
			_coroutine.Stop(this);
		}
	}

	private void UIOverlayStatusChanged(bool newValue)
	{
		_coroutine.Start(this, Move(newValue));
	}

	private IEnumerator Move(bool open)
	{
		Vector2 sizeDelta = background.sizeDelta;
		float start = sizeDelta.x;
		float target = (open ? openSizeDeltaX : defaultSizeDeltaX);
		return CoroutineUtilities.GenericTween((float t) =>
		{
			float x = EasingFunction.EaseOutQuart(start, target, t);
			sizeDelta.x = x;
			background.sizeDelta = sizeDelta;
		}, 1f);
	}

	protected override void FillReferencesInherited()
	{
		if (transform.EnsureComponentInChildNested<TextMeshProUGUI>(out versionText, "Version Box", "Version Text"))
		{
			versionText.transform.EnsureComponent<Button>(out versionButton);
		}
	}
}
