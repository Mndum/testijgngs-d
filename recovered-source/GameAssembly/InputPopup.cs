using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPopup : PrefabPopup<InputPopup>
{
	[SerializeField]
	private TextMeshProUGUI titleTmp;

	[SerializeField]
	private TextMeshProUGUI textTmp;

	[SerializeField]
	private TMP_InputField inputTmp;

	[SerializeField]
	private ButtonList buttonList;

	[SerializeField]
	private Button pasteFromClipboardButton;

	[SerializeField]
	private TextMeshProUGUI webglInstructions;

	public TMP_InputField InputField => inputTmp;

	public void Open(List<PopupStringChoice> choices, string title, string text, string defaultInput, TMP_InputField.ContentType type = TMP_InputField.ContentType.Standard)
	{
		textTmp.text = text;
		titleTmp.text = title;
		inputTmp.text = defaultInput;
		inputTmp.contentType = type;
		IModificationPeriodButtonList modificationPeriod;
		using (buttonList.DoInitialization(out modificationPeriod))
		{
			foreach (PopupStringChoice choice in choices)
			{
				ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Alternative);
				buttonListWrapper.SetText(choice.Text);
				buttonListWrapper.OnClick = () =>
				{
					choice.Action?.Invoke(inputTmp.text);
					CloseFromUIOverlay();
				};
			}
		}
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			pasteFromClipboardButton.gameObject.SetActive(value: false);
			webglInstructions.gameObject.SetActive(value: true);
			webglInstructions.text = LOC.UI.WebGL_InputFieldInfo;
		}
		else
		{
			pasteFromClipboardButton.gameObject.SetActive(value: true);
			webglInstructions.gameObject.SetActive(value: false);
		}
		pasteFromClipboardButton.onClick.AddListener(PasteFromClipboardButtonClicked);
		Open();
	}

	private void PasteFromClipboardButtonClicked()
	{
		UiOverlay instance = UiOverlay.Instance;
		string systemCopyBuffer = GUIUtility.systemCopyBuffer;
		if (string.IsNullOrEmpty(systemCopyBuffer))
		{
			instance.ShowFloatingText(instance.MouseToWorld(), LOC.UI.PasteFromClipboard_ClipboardEmpty);
			return;
		}
		inputTmp.text = systemCopyBuffer;
		instance.ShowFloatingText(instance.MouseToWorld(), LOC.UI.PasteFromClipboard_SuccessfullyPasted);
	}
}
