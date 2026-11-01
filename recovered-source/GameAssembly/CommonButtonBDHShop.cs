using UnityEngine;

public class CommonButtonBDHShop : CommonButtonHandler
{
	protected override ButtonColorPalette GetColorPaletteFromColorType(CommonButtonColorType ct)
	{
		return new ButtonColorPalette(Color.white, Color.white / 2f);
	}
}
