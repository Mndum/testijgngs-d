public class SaveAchievementHandler : IAchievementHandler
{
	public void Init()
	{
	}

	public void AddAchievement(string name)
	{
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.UnlockAchievement(name);
	}

	public void DeleteAchievement(string name)
	{
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.RemoveAchievement(name);
	}

	public bool GetAchievement(string name)
	{
		return SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.HasAchievement(name);
	}

	public void Save()
	{
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.SaveAllChanges();
	}
}
