using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PCStaticGui : StaticGuiBase
{
	[Header("Prefabs")]
	[SerializeField]
	private PooledPrefabReference<PCIconPrefab> _pcIconPrefab;

	private List<PCIconPrefab> shortcuts = new List<PCIconPrefab>();

	private PCIconPrefab emailIcon;

	[Header("Auto")]
	[SerializeField]
	private RectTransform A_Icons;

	[SerializeField]
	private TextMeshProUGUI A_Main_Text;

	[SerializeField]
	private Button A_Return_Button;

	public Button ReturnButton => A_Return_Button;

	private PCIconPrefab AddIcon(Sprite sprite, string text, UnityAction onClick)
	{
		PCIconPrefab fromPool = _pcIconPrefab.GetPool().GetFromPool(A_Icons);
		fromPool.Init(sprite, text, onClick);
		shortcuts.Add(fromPool);
		return fromPool;
	}

	private PCIconPrefab AddBrowserShortcutIcon(string link, string postfix = null)
	{
		return AddIcon(R.PcIcons.deepweb.Get(), LOC.UI.PC_Shortcut(link) + postfix, () =>
		{
			UiOverlay.Instance.OpenBrowser(link);
		});
	}

	public void OnEnable()
	{
		AddIcon(R.PcIcons.save.Get(), LOC.Polyglot.MENU_IO_SAVE, UiOverlay.Instance.OpenSaveMenu);
		AddIcon(R.PcIcons.load.Get(), LOC.Polyglot.MENU_IO_LOAD, UiOverlay.Instance.OpenLoadMenu);
		AddIcon(R.PcIcons.config.Get(), LOC.UI.StatusApp_PcIconName, () =>
		{
			UiOverlay.Instance.OpenBotStatusApp();
		});
		AddIcon(R.PcIcons.deepweb.Get(), LOC.UI.PC_Browser, () =>
		{
			UiOverlay.Instance.OpenBrowser();
		});
		emailIcon = AddBrowserShortcutIcon("bang.ic/email");
		AddBrowserShortcutIcon("cocktwitch.ic");
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("stonks"))
		{
			AddBrowserShortcutIcon("stonks.ic");
		}
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("ladyparts"))
		{
			AddBrowserShortcutIcon("ladyparts.ic");
		}
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("casino"))
		{
			AddBrowserShortcutIcon("casino.ic");
		}
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("firstdelivery"))
		{
			AddBrowserShortcutIcon("fedup.ic");
		}
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("cocktractsunlocked"))
		{
			AddBrowserShortcutIcon("cocktracts.ic");
		}
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("fishing1"))
		{
			AddBrowserShortcutIcon("realmenfishing.ic");
		}
		UpdateEmailsIcon();
		A_Return_Button.onClick.SetListener(SingletonLoadableMonoBehaviour<GameScript>.Instance.GoHome);
		SingletonLoadableMonoBehaviour<GameScript>.Instance.OnMinutePassed += UpdateDate;
		EmailBase.OnReadStaticEvent += UpdateEmailsIcon;
		UpdateDate();
	}

	private void UpdateDate()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			CultureInfo currentLanguageCultureInfo = SingletonMonoBehaviour<LocalizationSingleton>.Instance.CurrentLanguageCultureInfo;
			stringBuilder.AppendFormat(currentLanguageCultureInfo, "{0:g}", GameVariables.Current.GameDateTime);
			A_Main_Text.SetText(stringBuilder);
		}
	}

	public void OnDisable()
	{
		A_Return_Button.onClick.RemoveAllListeners();
		SingletonLoadableMonoBehaviour<GameScript>.Instance.OnMinutePassed -= UpdateDate;
		EmailBase.OnReadStaticEvent -= UpdateEmailsIcon;
		_pcIconPrefab.GetPool().InitOrResetPooledList(ref shortcuts);
	}

	private void UpdateEmailsIcon()
	{
		int num = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.VisibleEmails.Count((NewEmail t) => !t.read);
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.Append(LOC.UI.PC_Shortcut("bang.ic/email"));
			if (num != 0)
			{
				stringBuilder.AppendColorTagStart("green");
				stringBuilder.AppendLine();
				stringBuilder.Append(LOC.UI.PC_UnreadEmails(num));
				stringBuilder.AppendColorTagEnd();
			}
			emailIcon.IconText.SetText(stringBuilder);
		}
	}
}
