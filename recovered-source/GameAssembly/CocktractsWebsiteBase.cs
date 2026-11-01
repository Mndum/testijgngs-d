public class CocktractsWebsiteBase : Website
{
	protected CocktractManager CTM => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cocktractManager;

	public override bool IsWebsiteUnlocked()
	{
		return CocktractManager.CocktractsUnlocked;
	}
}
