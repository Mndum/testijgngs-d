using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsPopup : PrefabPopup<AchievementsPopup>
{
	[SerializeField]
	private TextMeshProUGUI infoText;

	[SerializeField]
	private Toggle moreDetailsToggle;

	private bool _showMoreDetails;

	private void DetailsValueChanged(bool arg0)
	{
		_showMoreDetails = arg0;
		UpdateInfo();
	}

	private void UpdateInfo()
	{
		IAchievementHandler achievementHandler = SingletonLoadableMonoBehaviour<AchievementSingleton>.Instance.AchievementHandler;
		SaveContainer save = SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save;
		save.ValidateVisitedWebsites();
		if (_showMoreDetails)
		{
			DoDetailed(achievementHandler, save);
		}
		else
		{
			DoSimple(achievementHandler, save);
		}
		((RectTransform)transform).ForceRebuildLayoutRecursive();
	}

	private void DoDetailed(IAchievementHandler achievementHandler, SaveContainer save)
	{
		StringBuilder sb;
		using (StringBuilderCache.AcquireWithUsing(out sb))
		{
			_ = LOC.UI.Achievements_EndingSeen;
			_ = LOC.UI.Achievements_EndingUnseen;
			sb.AppendLineHeader(LOC.UI.Achievements_MainEndingsHeader);
			sb.AppendLineSubHeader(LOC.UI.Achievements_GoodEndingsHeader);
			foreach (Achievement item in AchievementSingleton.AllMainEndings.Where((Achievement x) => x.AchievementType == AchievementTypeEnum.MainEnding_Good))
			{
				AppendLineSeen(item);
			}
			sb.AppendLine();
			sb.AppendLineSubHeader(LOC.UI.Achievements_BadEndingsHeader);
			foreach (Achievement item2 in AchievementSingleton.AllMainEndings.Where((Achievement x) => x.AchievementType == AchievementTypeEnum.MainEnding_Bad))
			{
				AppendLineSeen(item2);
			}
			sb.AppendLine();
			sb.AppendLineHeader(LOC.UI.Achievements_ChurchEndingsHeader);
			foreach (Achievement allChurchEnding in AchievementSingleton.AllChurchEndings)
			{
				AppendLineSeen(allChurchEnding);
			}
			sb.AppendLine();
			sb.AppendLineHeader(LOC.UI.Achievements_OtherHeader);
			foreach (Achievement item3 in AchievementSingleton.AllAchievements.Where((Achievement x) => x.AchievementType == AchievementTypeEnum.OtherStoryRelated))
			{
				AppendLineSeen(item3);
			}
			infoText.SetText(sb);
		}
		void AppendLineSeen(Achievement achievement)
		{
			string value = achievement.Name;
			if (achievementHandler.GetAchievement(achievement.Id))
			{
				using (sb.StartColorTagUsing(Color.green))
				{
					sb.Append("✓ ");
					sb.Append(value);
				}
			}
			else
			{
				using (sb.StartColorTagUsing(Color.red))
				{
					sb.Append(value);
				}
			}
			sb.AppendLine();
		}
	}

	private void DoSimple(IAchievementHandler achievementHandler, SaveContainer save)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLineSubHeader(LOC.UI.Achievements_MainEndingsHeader);
			stringBuilder.AppendLine(LOC.UI.Achievements_1(AchievementSingleton.AllMainEndings.Count((Achievement t) => achievementHandler.GetAchievement(t.Id)), AchievementSingleton.AllMainEndings.Count()));
			stringBuilder.AppendLine(LOC.UI.Achievements_BadEndingsCount(AchievementSingleton.AllBadMainEndings.Count((Achievement t) => achievementHandler.GetAchievement(t.Id)), AchievementSingleton.AllBadMainEndings.Count()));
			stringBuilder.AppendLine(LOC.UI.Achievements_GoodEndingsCount(AchievementSingleton.AllGoodMainEndings.Count((Achievement t) => achievementHandler.GetAchievement(t.Id)), AchievementSingleton.AllGoodMainEndings.Count()));
			stringBuilder.AppendLine();
			stringBuilder.AppendLineSubHeader(LOC.UI.Achievements_ChurchEndingsHeader);
			int num = AchievementSingleton.AllChurchEndings.Count((Achievement t) => achievementHandler.GetAchievement(t.Id));
			stringBuilder.AppendLine(LOC.UI.Achievements_3(num, AchievementSingleton.AllChurchEndings.Count()));
			stringBuilder.AppendLine();
			stringBuilder.AppendLineSubHeader(LOC.UI.Achievements_OtherHeader);
			int count = save.VisitedWebsites.Count;
			int count2 = SingletonMonoBehaviour<InternetSingleton>.Instance.ListedWebsitePrefabs.Count;
			stringBuilder.AppendLine(LOC.UI.Achievements_5(count, count2));
			if (SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.HasGlobalFlag("c"))
			{
				stringBuilder.TMPAppendFinePrintSizeTag();
				stringBuilder.AppendLine(LOC.UI.Achievements_Cheating);
				stringBuilder.TMPAppendCloseSizeTag();
			}
			stringBuilder.AppendLine(LOC.UI.Achievements_EstimatedTimePlayed(GlobalFunctions.TimePeriod(OptionsStatic.MinutesSpentPlaying)));
			stringBuilder.AppendLine(LOC.UI.Achievments_StoryTextPercent(save.GetPercentageOfSeenStoryText()));
			infoText.SetText(stringBuilder);
		}
	}

	public override void Open()
	{
		UpdateInfo();
		moreDetailsToggle.SetIsOnWithoutNotify(value: false);
		moreDetailsToggle.onValueChanged.SetListener(DetailsValueChanged);
		base.Open();
	}

	public override bool CanClose()
	{
		return true;
	}
}
