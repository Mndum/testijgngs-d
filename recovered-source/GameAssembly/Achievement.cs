using UnityEngine.Localization;

public class Achievement
{
	public string EnglishName;

	public string Id { get; private set; }

	public AchievementTypeEnum AchievementType { get; private set; }

	public string Name => LocalizedStringName.GetLocalizedStringAsync().Result;

	public LocalizedString LocalizedStringName { get; private set; }

	public Achievement(string id, string name, AchievementTypeEnum type)
	{
		Id = id;
		EnglishName = name;
		LocalizedStringName = new LocalizedString(LOC.Other.TABLE_REFERENCE, "Achievement_" + id);
		AchievementType = type;
	}
}
