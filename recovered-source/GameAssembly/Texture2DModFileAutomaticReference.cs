using UnityEngine;

public class Texture2DModFileAutomaticReference : ModFileAutomaticReference<Texture2D>
{
	public Texture2DModFileAutomaticReference(ModFileBase modFileSource)
		: base(modFileSource)
	{
	}

	protected override Texture2D Load()
	{
		return ModFileSource.GetAsTexture2D();
	}
}
