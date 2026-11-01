using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoDisplay : DataDisplay<CocktractPartner>
{
	[SerializeField]
	private Image image;

	protected override void SetData(CocktractPartner newValue)
	{
		Sprite sprite = newValue?.LogoSprite;
		Button component = image.GetComponent<Button>();
		if (sprite != null)
		{
			image.sprite = sprite;
			image.preserveAspect = true;
			Rect rect = sprite.rect;
			image.GetComponent<AspectRatioFitter>().aspectRatio = rect.width / rect.height;
			gameObject.SetActive(value: true);
			component.enabled = true;
			component.onClick.SetListener(() =>
			{
				UiOverlay.Instance.GetCurrentlyOpenedBrowser().ChangeWebsite(new GameUrlWithArguments("cocktracts.ic/partner", new Dictionary<string, string> { ["Id"] = newValue.Key }));
			});
		}
		else
		{
			image.sprite = null;
			gameObject.SetActive(value: false);
			component.enabled = false;
			component.onClick.RemoveAllListeners();
		}
		_current = newValue;
	}
}
