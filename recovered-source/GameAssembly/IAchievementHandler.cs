public interface IAchievementHandler
{
	void AddAchievement(string name);

	void DeleteAchievement(string name);

	bool GetAchievement(string name);

	void Save();

	void Init();
}
