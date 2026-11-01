using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorInputPopupEntry : AdvancedItemListPrefab
{
	[SerializeField]
	private TextMeshProUGUI _text;

	[SerializeField]
	private Button _button;

	[SerializeField]
	private Image _colorSample;

	[SerializeField]
	private Image _colorSampleInner;

	private bool _hasOutline;

	public override bool HasDefaultButton => true;

	public override Button GetDefaultButton()
	{
		return _button;
	}

	private void SetOutline(Color color)
	{
		_colorSample.color = color;
		float num = 0.3f;
		Material fontMaterial = _text.fontMaterial;
		fontMaterial.EnableKeyword(ShaderUtilities.Keyword_Outline);
		fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, num / 2f);
		fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, num);
		fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, color);
		_text.SetMaterialDirty();
		_hasOutline = true;
	}

	private void EnsureNoOutline()
	{
		if (_hasOutline)
		{
			Material fontMaterial = _text.fontMaterial;
			fontMaterial.DisableKeyword(ShaderUtilities.Keyword_Outline);
			fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0f);
		}
	}

	public void SetColor(ColorPaletteManager.ColorPaletteEntry data)
	{
		_colorSample.gameObject.SetActive(value: true);
		_text.rectTransform.SetLeft(5f + _colorSample.rectTransform.sizeDelta.x + 5f);
		Color32 color = data.Color;
		_text.faceColor = data.Color;
		if (color.HasEnoughContrast(_button.colors.normalColor, 2.5f))
		{
			_colorSample.color = Color.clear;
			EnsureNoOutline();
		}
		else if (color.HasEnoughContrast(MyColor32Presets.White))
		{
			SetOutline(MyColor32Presets.White);
		}
		else
		{
			SetOutline(MyColor32Presets.Black);
		}
		_text.fontStyle = FontStyles.Normal;
		_colorSampleInner.color = color;
		_text.text = data.GetLocalizedName();
	}

	public void SetPalette(ColorPaletteManager.ColorPalette data)
	{
		_colorSample.gameObject.SetActive(value: false);
		_text.rectTransform.SetLeft(5f);
		EnsureNoOutline();
		if (IsFocused)
		{
			_text.faceColor = UiOverlay.Instance.ActiveColor;
			_text.fontStyle = FontStyles.Bold;
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.Append(data.GetLocalizedName());
				stringBuilder.Append(LOC.UI.UI_Selected);
				_text.SetText(stringBuilder);
				return;
			}
		}
		_text.faceColor = Color.white;
		_text.fontStyle = FontStyles.Normal;
		_text.SetText(data.GetLocalizedName());
	}
}
