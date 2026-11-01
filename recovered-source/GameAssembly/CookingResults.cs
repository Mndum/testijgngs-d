using System;

[Serializable]
public class CookingResults
{
	public CookingGameplayStartInfo StartInfo;

	public CookingGameplayScoreData Score;

	public CookingSong Song => StartInfo.Song;

	public CookingGameplayModifiers ActiveModifiers => StartInfo.ActiveModifiers;

	public RecipeExecution RecipeExecution => StartInfo.RecipeExecution;

	public CookingResults(CookingGameplayStartInfo startInfo, CookingGameplayScoreData score)
	{
		StartInfo = startInfo;
		Score = score;
	}
}
