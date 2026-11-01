public class CookingGameplayStartInfo
{
	public CookingSong Song;

	public CookingGameplayModifiers ActiveModifiers;

	public RecipeExecution RecipeExecution;

	public CookingGameplayCheatStartInfo CheatInfo;

	public CookingGameplayStartInfo(CookingSong song, CookingGameplayModifiers activeModifiers, RecipeExecution recipeExecution)
	{
		Song = song;
		ActiveModifiers = activeModifiers;
		RecipeExecution = recipeExecution;
	}
}
