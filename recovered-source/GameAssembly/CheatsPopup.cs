using System.Text;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheatsPopup : PrefabPopup<CheatsPopup>
{
	[SerializeField]
	private TextMeshProUGUI infoText;

	[SerializeField]
	private ButtonList buttonList;

	[SerializeField]
	private Image price1;

	[SerializeField]
	private Image price2;

	[SerializeField]
	private TextMeshProUGUI price1text;

	[SerializeField]
	private TextMeshProUGUI price2text;

	[SerializeField]
	private TextMeshProUGUI otherPatreonBenefits;

	[SerializeField]
	private Button botbutton;

	private ButtonListWrapper getCheatCodeButton;

	private const string utm = "cheats";

	public override void Open()
	{
		botbutton.onClick.SetListener(() =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.OpenDirectSubscribePatreon("cheats");
		});
		UpdateCheatDescription();
		IModificationPeriodButtonList modificationPeriod;
		using (buttonList.DoInitialization(out modificationPeriod))
		{
			getCheatCodeButton = modificationPeriod.AddButton(0, CommonButtonColorType.Green);
			getCheatCodeButton.SetText(LOC.UI.Cheats_GetCheatCode);
			getCheatCodeButton.OnClick = Sheep.GetCheatCodeGuide;
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
			buttonListWrapper.SetText(LOC.UI.Cheats_HaveCheatCode);
			buttonListWrapper.OnClick = () =>
			{
				Sheep.InputCheatCodeButtonClicked().ContinueWith(UpdateCheatStatus).Forget();
			};
			buttonListWrapper.ActiveFunc = () => !OptionsStatic.AnyCheatsEnabled;
			ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton();
			buttonListWrapper2.SetText(LOC.UI.Cheat_DisableCheats);
			buttonListWrapper2.OnClick = DisableCheatsButtonClicked;
			buttonListWrapper2.ActiveFunc = () => OptionsStatic.AnyCheatsEnabled;
		}
		base.Open();
	}

	private void UpdateCheatStatus()
	{
		buttonList.UpdateCurrentButtonState();
		UpdateCheatDescription();
	}

	private void UpdatePricePosition(Image priceBackground, string linkID, TextMeshProUGUI priceText, string newPriceText)
	{
		priceText.text = newPriceText;
		TMP_TextInfo textInfo = infoText.textInfo;
		if (textInfo.linkCount == 0)
		{
			return;
		}
		TMP_LinkInfo tMP_LinkInfo = default;
		bool flag = false;
		for (int i = 0; i < textInfo.linkCount; i++)
		{
			if (textInfo.linkInfo[i].GetLinkID() == linkID)
			{
				tMP_LinkInfo = textInfo.linkInfo[i];
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return;
		}
		float num = 3.4028235E+38f;
		float num2 = -3.4028235E+38f;
		float num3 = 3.4028235E+38f;
		float num4 = -3.4028235E+38f;
		int linkTextfirstCharacterIndex = tMP_LinkInfo.linkTextfirstCharacterIndex;
		int num5 = linkTextfirstCharacterIndex + tMP_LinkInfo.linkTextLength;
		for (int j = linkTextfirstCharacterIndex; j < num5; j++)
		{
			TMP_CharacterInfo tMP_CharacterInfo = textInfo.characterInfo[j];
			if (tMP_CharacterInfo.isVisible)
			{
				num = Mathf.Min(num, tMP_CharacterInfo.bottomLeft.x);
				num2 = Mathf.Max(num2, tMP_CharacterInfo.topRight.x);
				num3 = Mathf.Min(num3, tMP_CharacterInfo.descender);
				num4 = Mathf.Max(num4, tMP_CharacterInfo.ascender);
			}
		}
		if (num != 3.4028235E+38f)
		{
			Vector2 vector = new Vector2((num + num2) * 0.5f, (num3 + num4) * 0.5f);
			Vector3 localPosition = priceBackground.rectTransform.localPosition;
			localPosition.x = num2 + 10f;
			localPosition.y = vector.y;
			priceBackground.rectTransform.localPosition = localPosition;
			priceBackground.rectTransform.SetSizeDeltaX(priceText.GetPreferredValues().x + 10f);
		}
	}

	private void UpdateCheatDescription()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.Append(LOC.UI.Cheats_AccessLinks);
			stringBuilder.Append(": ");
			CheatTierEnum cheatTier = OptionsStatic.GetCheatTier();
			using (stringBuilder.StartColorTagUsing(new Color32(255, 66, 77, 255)))
			{
				stringBuilder.TMPAppendLinkTagWithString("p", "Patreon");
			}
			stringBuilder.Append(", ");
			using (stringBuilder.StartColorTagUsing(new Color32(0, 150, 136, 255)))
			{
				stringBuilder.TMPAppendLinkTagWithString("s", "SubscribeStar");
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			if (cheatTier >= CheatTierEnum.BasicCheats)
			{
				stringBuilder.AppendColorTagStart(Color.green);
			}
			using (stringBuilder.StartColorTagUsing(new Color(0.45f, 0.85f, 0.6f)))
			{
				stringBuilder.TMPAppendLinkTag("price1");
				stringBuilder.Append("Robot GF Enjoyer");
				stringBuilder.TMPAppendCloseLinkTag();
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendStartThinFontWeight();
			stringBuilder.Append(LOC.UI.Cheat_BasicCheats);
			stringBuilder.Append(":");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("- " + LOC.UI.Cheat_Money);
			stringBuilder.AppendLine("- " + LOC.UI.Cheat_ResetChurch);
			stringBuilder.AppendFontWeightEnd();
			stringBuilder.AppendColorTagEnd();
			stringBuilder.AppendLine();
			if (cheatTier >= CheatTierEnum.AdvancedCheats)
			{
				stringBuilder.AppendColorTagStart(Color.green);
			}
			using (stringBuilder.StartColorTagUsing(new Color(1f, 0.75f, 0.25f)))
			{
				stringBuilder.TMPAppendLinkTag("price2");
				stringBuilder.Append("Robot GF Connoisseur");
				stringBuilder.TMPAppendCloseLinkTag();
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendStartThinFontWeight();
			stringBuilder.Append(LOC.UI.Cheat_AdvancedCheats);
			stringBuilder.Append(":");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("- " + LOC.UI.Cheat_AllOfTheBasic);
			stringBuilder.AppendLine("- " + LOC.UI.Cheat_CumCheats);
			stringBuilder.AppendLine("- " + LOC.UI.Cheat_ItemColors);
			stringBuilder.AppendLine("- " + LOC.UI.Cheat_Quality);
			stringBuilder.AppendLine("- " + LOC.UI.Cheat_Items);
			stringBuilder.AppendLine("- " + LOC.UI.Cheat_AndMore);
			stringBuilder.AppendFontWeightEnd();
			stringBuilder.AppendColorTagEnd();
			stringBuilder.AppendLine();
			stringBuilder.AppendStartDisplay(LOC.UI.Cheat_Status);
			switch (cheatTier)
			{
			case CheatTierEnum.NoCheats:
				using (stringBuilder.StartColorTagUsing(Color.aquamarine))
				{
					stringBuilder.Append(LOC.UI.Cheat_DisabledCheats);
				}
				break;
			case CheatTierEnum.BasicCheats:
				using (stringBuilder.StartColorTagUsing(Color.green))
				{
					stringBuilder.Append(LOC.UI.Cheat_BasicCheats);
				}
				break;
			case CheatTierEnum.AdvancedCheats:
				using (stringBuilder.StartColorTagUsing(Color.green))
				{
					stringBuilder.Append(LOC.UI.Cheat_AdvancedCheats);
				}
				break;
			case CheatTierEnum.Debug:
				using (stringBuilder.StartColorTagUsing(Color.green))
				{
					stringBuilder.Append("DEBUG");
				}
				break;
			}
			infoText.SetText(stringBuilder);
			infoText.ForceMeshUpdate();
			UpdatePricePosition(price1, "price1", price1text, (cheatTier >= CheatTierEnum.BasicCheats) ? LOC.UI.Cheat_Active : "6.99$");
			UpdatePricePosition(price2, "price2", price2text, (cheatTier >= CheatTierEnum.AdvancedCheats) ? LOC.UI.Cheat_Active : "14.99$");
			infoText.gameObject.GetOrAddComponent<TextMeshProUGUILinkHandler>().OnLinkClicked = (string x) =>
			{
				if (!(x == "p"))
				{
					if (x == "s")
					{
						SingletonLoadableMonoBehaviour<GameScript>.Instance.OpenSubscribeStar("cheats");
					}
				}
				else
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.OpenDirectSubscribePatreon("cheats");
				}
			};
			stringBuilder.Clear();
			stringBuilder.Append(LOC.UI.SubscribeNowToAccess);
			stringBuilder.AppendLine(":");
			stringBuilder.AppendLine(LOC.UI.Benefit_EarlyAccess);
			stringBuilder.AppendLine(LOC.UI.CheatsTitle);
			stringBuilder.AppendLine(LOC.UI.Benefit_Devlogs);
			stringBuilder.AppendLine(LOC.UI.Benefit_ConceptArt);
			stringBuilder.AppendLine(LOC.UI.Benefit_AndMore);
			otherPatreonBenefits.SetText(stringBuilder);
		}
	}

	private void DisableCheatsButtonClicked()
	{
		UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Cheat_AreYouSureDisable, () =>
		{
			OptionsStatic.ClearCheatCode();
			UpdateCheatStatus();
		});
	}

	public void Update()
	{
		float num = Mathf.Sin(Time.realtimeSinceStartup * 1.5f) * 5f;
		float x = Mathf.Sin(Time.realtimeSinceStartup * 1.3f) * 5f;
		otherPatreonBenefits.rectTransform.anchoredPosition = new Vector2(x, num - 100f);
		float z = Mathf.Sin(Time.realtimeSinceStartup * 0.8f) * 3f;
		otherPatreonBenefits.rectTransform.localRotation = Quaternion.Euler(0f, 0f, z);
		TMP_TextInfo textInfo = otherPatreonBenefits.textInfo;
		for (int i = 0; i < textInfo.characterCount; i++)
		{
			if (textInfo.characterInfo[i].isVisible)
			{
				int materialReferenceIndex = textInfo.characterInfo[i].materialReferenceIndex;
				int vertexIndex = textInfo.characterInfo[i].vertexIndex;
				Color32[] colors = textInfo.meshInfo[materialReferenceIndex].colors32;
				Color color = Color.HSVToRGB((Time.realtimeSinceStartup * 0.1f + (float)i / (float)textInfo.characterCount) % 1f, 1f, 1f);
				colors[vertexIndex] = color;
				colors[vertexIndex + 1] = color;
				colors[vertexIndex + 2] = color;
				colors[vertexIndex + 3] = color;
			}
		}
		for (int j = 0; j < textInfo.meshInfo.Length; j++)
		{
			textInfo.meshInfo[j].mesh.colors32 = textInfo.meshInfo[j].colors32;
			otherPatreonBenefits.UpdateGeometry(textInfo.meshInfo[j].mesh, j);
		}
	}

	public override bool CanClose()
	{
		return true;
	}
}
