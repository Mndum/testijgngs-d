using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommonButtonHandler : MonoBehaviour, IPooledPrefab
{
	public enum CommonButtonSizeEnum
	{
		Normal,
		Smaller
	}

	[SerializeField]
	private TextMeshProUGUI buttonText;

	[SerializeField]
	private BetterButton button;

	[SerializeField]
	private Image backgroundImage;

	public TextMeshProUGUI ButtonText => buttonText;

	public BetterButton Button => button;

	public Image BackgroundImage => backgroundImage;

	public float UnclampedTargetWidth { get; private set; }

	public GameObject SourcePrefab { get; set; }

	public void SetOnClick(UnityAction action)
	{
		button.onClick.SetListener(action);
	}

	public void SetOnPointerDown(Action action)
	{
		button.PointerDownAction = action;
	}

	private void SetColorPalette(ButtonColorPalette buttonColorPalette)
	{
		ColorBlock colors = default;
		Color normalColor = (colors.selectedColor = buttonColorPalette.BackgroundColor);
		colors.normalColor = normalColor;
		normalColor = (colors.highlightedColor = buttonColorPalette.BackgroundColorActive);
		colors.pressedColor = normalColor;
		colors.disabledColor = buttonColorPalette.BackgroundColorDisabled;
		colors.colorMultiplier = 1f;
		colors.fadeDuration = 0.1f;
		button.colors = colors;
		buttonText.faceColor = buttonColorPalette.TextColor;
		button.InitDefaultColor();
	}

	protected virtual ButtonColorPalette GetColorPaletteFromColorType(CommonButtonColorType ct)
	{
		return UiOverlay.Instance.GetButtonColorPalette(ct);
	}

	public void SetColorType(CommonButtonColorType ct)
	{
		SetColorPalette(GetColorPaletteFromColorType(ct));
	}

	public void SetSize(CommonButtonSizeEnum size)
	{
		RectTransform rectTransform = (RectTransform)transform;
		if (size == CommonButtonSizeEnum.Normal || size != CommonButtonSizeEnum.Smaller)
		{
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 50f);
			buttonText.fontSizeMax = 24f;
		}
		else
		{
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 30f);
			buttonText.fontSizeMax = 18f;
		}
	}

	public void PrepareTextLayoutElement(float maxWidth)
	{
		LayoutElement orAddComponent = gameObject.GetOrAddComponent<LayoutElement>();
		float num = (UnclampedTargetWidth = buttonText.GetPreferredValues().x - ((RectTransform)buttonText.transform).sizeDelta.x);
		if (num > maxWidth)
		{
			num = maxWidth;
		}
		Vector2 sizeDelta = ((RectTransform)transform).sizeDelta;
		orAddComponent.layoutPriority = 0;
		orAddComponent.preferredWidth = num;
		orAddComponent.minWidth = num * 0.7f;
		orAddComponent.minHeight = sizeDelta.y;
		orAddComponent.enabled = true;
	}

	public void ClearLayoutElement()
	{
		if (transform.TryGetComponent<LayoutElement>(out var component))
		{
			component.enabled = false;
			component.layoutPriority = -1;
			component.preferredWidth = 0f;
			component.minWidth = 0f;
			component.minWidth = 0f;
		}
	}

	public void AfterReturnedToPool()
	{
		buttonText.text = "";
		button.onClick.RemoveAllListeners();
		button.PointerDownAction = null;
		button.interactable = true;
		ClearLayoutElement();
	}

	public void BeforeGetFromPool()
	{
		Button.enabled = true;
		backgroundImage.enabled = true;
	}
}
