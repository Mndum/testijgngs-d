using System;

public class ClothierViewScene : ViewScene
{
	public bool halloweenDecorationsEnabled;

	protected override void SwitchNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		Guid uniqueGuid = new Guid("577F3C45-8D0E-4C90-A127-5F70CB6DF5FF");
		Guid uniqueGuid2 = new Guid("A660F548-7C1C-4562-8411-2EBEB06B0A4D");
		Guid uniqueGuid3 = new Guid("A75BD93B-00A0-4722-A312-04A4408EC0FD");
		base.SwitchNewView(info, thisCallback);
		TextureOverriderManager clothierManager = TextureOverriderManager.ClothierManager;
		if (!halloweenDecorationsEnabled)
		{
			clothierManager.RestoreSprite(uniqueGuid, "/Blink");
			clothierManager.RestoreSprite(uniqueGuid2, "/Halloween Decoration");
			clothierManager.RestoreSprite(uniqueGuid3, "/Halloween Cat");
		}
		else
		{
			clothierManager.SetSprite(uniqueGuid, "/Blink", R.AltBackgrounds.Blink_Halloween);
			clothierManager.SetSprite(uniqueGuid2, "/Halloween Decoration", R.AltBackgrounds.Decoration_Halloween);
			clothierManager.SetSprite(uniqueGuid3, "/Halloween Cat", R.AltBackgrounds.Halloween_Cat);
		}
	}
}
