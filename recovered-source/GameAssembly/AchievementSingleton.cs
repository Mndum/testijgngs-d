using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

public sealed class AchievementSingleton : SingletonLoadableMonoBehaviour<AchievementSingleton>
{
	public static Achievement[] AllAchievements = new Achievement[48]
	{
		new Achievement("ending-ballsexploded", "Too much juice...", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-goodEnd1", "Selling your bot", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-badEnd1", "Termination of tenancy", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-badEnd2", "Failure...", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-badEnd3", "Coward...", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-badEnd4.1", "Termination of tenancy v2: hobo", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-badEnd4.2", "Termination of tenancy v2: murder", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-healthEnd", "Starvation", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-fedEnd", "Got caught...", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-schizoEnd", "I'm not crazy...", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-genericSchizoEnd", "Wall-kun", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-KilledByADirtyCop1", "Never trust the cops...", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-KilledByADirtyCop2", "Never trust the cops... AGAIN", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-theyKnowOblivious", "They know... (oblivious)", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-theyKnow", "They know... ", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-unluckyEncounterWithLead", "Unlucky encounter with lead", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-itWasntSupposedToEndLikeThis", "It wasn't supposed to end like this...", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-friendsTillTheVeryEnd", "At least, to the end, we were friends...", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-victimOfARevolution", "Victim of a revolution", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-grenade", "Unlucky catch...", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-revengeOfTheHomeless", "Revenge of the homeless", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-revengeOfTheHomelessBeforeTheFlight", "Revenge of the homeless before the flight", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-veryBadTrip", "Very bad trip", AchievementTypeEnum.MainEnding_Bad),
		new Achievement("ending-islandAnnalie", "Paradisium... with Annalie", AchievementTypeEnum.MainEnding_Good),
		new Achievement("ending-islandLandlord", "Paradisium... with Landlord", AchievementTypeEnum.MainEnding_Good),
		new Achievement("ending-islandLandlordEveryone", "Paradisium... with Everyone", AchievementTypeEnum.MainEnding_Good),
		new Achievement("church-endSpotkanie2B", "The oblivious ending", AchievementTypeEnum.ChurchEnding),
		new Achievement("church-endTheWorstEnd", "The worst ending", AchievementTypeEnum.ChurchEnding),
		new Achievement("church-endIdontknow", "The true ending", AchievementTypeEnum.ChurchEnding),
		new Achievement("church-endJustGoThereGood", "The happy ending", AchievementTypeEnum.ChurchEnding),
		new Achievement("church-endJustGoThereBad", "The not good enough ending", AchievementTypeEnum.ChurchEnding),
		new Achievement("bigdaddyhurtSecretEpilogue", "Secret bigdaddyhurt epilogue", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("badTripSavedByMelissa", "Bad trip saved by Melissa", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("lastTripMetDad", "Met d** during the fishing trip", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("defeatedBotInFishingDuel", "Defeated bot during the fishing duel", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("helpedAnnalieDuringRiots", "Helped Annalie during the riots", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("helpedPristbotDuringRiots", "Helped Priestbot during the riots", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("melissaFriendsDead", "Melissa ending 1", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("melissaFriendsLeaves", "Melissa ending 2", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("melissaDestory", "Melissa ending 3", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("melissaLeave", "Melissa ending 4", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("melissaLeave", "Melissa ending 4", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("shower", "Try the shower scene", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("sleepingTogether", "Try sleeping together", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("runway-Landlord", "See landlord on the runway", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("runway-Shanice", "See Shanice on the runway", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("runway-Fisher", "See fisher on the runway", AchievementTypeEnum.OtherStoryRelated),
		new Achievement("saveEdited", "Edit your save or cheat the game!", AchievementTypeEnum.Other)
	};

	public static IEnumerable<Achievement> AllMainEndings => AllAchievements.Where((Achievement x) => x.AchievementType == AchievementTypeEnum.MainEnding_Bad || x.AchievementType == AchievementTypeEnum.MainEnding_Good);

	public static IEnumerable<Achievement> AllGoodMainEndings => AllAchievements.Where((Achievement x) => x.AchievementType == AchievementTypeEnum.MainEnding_Good);

	public static IEnumerable<Achievement> AllBadMainEndings => AllAchievements.Where((Achievement x) => x.AchievementType == AchievementTypeEnum.MainEnding_Bad);

	public static IEnumerable<Achievement> AllChurchEndings => AllAchievements.Where((Achievement x) => x.AchievementType == AchievementTypeEnum.ChurchEnding);

	public IAchievementHandler AchievementHandler { get; private set; } = new SaveAchievementHandler();

	public override Type[] GetDependenciesArray()
	{
		return new Type[1] { typeof(SaveSingleton) };
	}

	protected override UniTask LoadInner(IProgressManager progressManager)
	{
		AchievementHandler.Init();
		return UniTask.CompletedTask;
	}
}
