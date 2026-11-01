using System.Linq;
using Fungus;
using UnityEngine;

[CommandInfo("My", "AddAchievement", "Adds a achievement", 0)]
internal class AddAchievement : Command
{
	[Tooltip("Achievement to add")]
	[SerializeField]
	protected string achievement;

	public override void OnEnter()
	{
		SingletonLoadableMonoBehaviour<AchievementSingleton>.Instance.AchievementHandler.AddAchievement(achievement);
		SingletonLoadableMonoBehaviour<AchievementSingleton>.Instance.AchievementHandler.Save();
		Continue();
	}

	public override string GetSummary()
	{
		Achievement achievement = AchievementSingleton.AllAchievements.FirstOrDefault((Achievement t) => t.Id == this.achievement);
		if (achievement == null)
		{
			return "Error! This achievement doesn't exist!";
		}
		return "Adds achievement \"" + this.achievement + "\" with name \"" + achievement.Name + "\"";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
