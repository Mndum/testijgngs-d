using System;
using GameItems;

namespace StoryConditions;

public abstract class StoryCondition
{
	public class CommonConditionsContainer
	{
		public readonly DataCondition<int> SympathyDetest = VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.BelowEqual, -100);

		public readonly DataCondition<int> SympathyHate = VariableCondition("sympathy", DataConditionComparators.TwoArgumentComparators.BetweenEqual, -99, -50);

		public readonly DataCondition<int> SympathyDislike = VariableCondition("sympathy", DataConditionComparators.TwoArgumentComparators.Between, -50, 0);

		public readonly DataCondition<int> SympathyPositive = VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.AboveEqual, 0);

		public const int LikeStart = 50;

		public const int AffectionStart = 200;

		public const int LoveStart = 500;

		public const int SuperLoveStart = 1000;

		public readonly DataCondition<float> NotHorny = VariableCondition("PotentialHorniness", DataConditionComparators.SingleArgumentComparators.BelowEqual, 0.5f);

		public readonly DataCondition<float> Horny = VariableCondition("PotentialHorniness", DataConditionComparators.SingleArgumentComparators.Above, 0.5f);

		public readonly DataCondition<float> VeryHorny = VariableCondition("PotentialHorniness", DataConditionComparators.SingleArgumentComparators.Above, 0.75f);

		public readonly DataCondition<int> NoLust = VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Below, 100);

		public readonly DataCondition<int> HighLust = VariableCondition("sympathy", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 100, 300);

		public readonly DataCondition<int> VeryHighLust = VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 300);

		public readonly DataCondition<int> ExtremelyLust = VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 600);

		public readonly DataCondition<int> SympathyLike = VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.AboveEqual, 50);

		public readonly DataCondition<int> SympathyAffection = VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.AboveEqual, 200);

		public readonly DataCondition<int> SympathyLove = VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.AboveEqual, 500);

		public readonly DataCondition<int> SympathySuperLove = VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.AboveEqual, 1000);

		public readonly DataCondition<int> SympathyBeforeLikeOnly = VariableCondition("sympathy", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 0, 50);

		public readonly DataCondition<int> SympathyLikeOnly = VariableCondition("sympathy", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 50, 200);

		public readonly DataCondition<int> SympathyAffectionOnly = VariableCondition("sympathy", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 200, 500);

		public readonly DataCondition<int> SympathyLoveOnly = VariableCondition("sympathy", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 500, 1000);

		public readonly DataCondition<int> SympathySuperLoveOnly = VariableCondition("sympathy", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 1000, 2147483647);

		public readonly DataCondition<int> ItsEvening = HourBetweenEqual(18, 23);

		public readonly DataCondition<int> ItsNight = HourBetweenEqual(23, 5);

		public readonly DataCondition<int> ItsMorning = HourBetweenEqual(6, 11);

		public readonly DataCondition<int> ItsDay = HourBetweenEqual(12, 17);

		public readonly DataCondition<int> ItsNotNight = HourBetweenEqual(6, 22);

		public readonly DataCondition<int> LandlordNotAsleep = HourBetweenEqual(6, 22);

		public readonly DataCondition<int> LateAtNight = HourBetweenEqual(23, 4);

		public readonly DataCondition<int> NotLateAtNight = HourBetweenEqual(5, 22);

		public readonly StoryCondition LandlordKnowsAboutBot = FlagSet("LandlordConspiracy2");

		public readonly StoryCondition LandlordKnowsBotIsYourGirlfriend = FlagSet("LandlordConspiracy3");

		public readonly DataCondition<int> FisherAtMicroplasticCoast = HourBetweenEqual(5, 15);

		public readonly DataCondition<float> LowMentalHealth = VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.5f);

		public readonly DataCondition<bool> RiotsActive = VariableConditionBoolTrue("RiotsActive");

		public readonly DataCondition<bool> RiotsNotActive = VariableConditionBoolFalse("RiotsActive");

		public readonly DataCondition<bool> RiotsEndingSoon = VariableConditionBoolTrue("RiotsEndingSoon");

		public readonly DataCondition<bool> AfterRiots = VariableConditionBoolTrue("RiotsAfter");

		public readonly FlagConditionBasic SleepTogetherUnlocked = FlagSet("SleepingWithBotUnlocked");

		public readonly StoryCondition EverStartedStream = FlagSet("StreamStarted");

		public readonly StoryCondition CanStartStream = Or(FlagSet("StreamStarted"), FlagSetAfterRelative("DidntStreamAfterPrologue", 14400));

		public const int FreshTime = 30;

		public readonly DataCondition<bool> UsuallyLeavesCumAfterSex = VariableConditionBoolTrue("UsuallyLeavesCumAfterSex");

		public readonly DataCondition<bool> UsuallyCleansCumAfterSex = VariableConditionBoolTrue("UsuallyCleansCumAfterSex");

		public readonly DataCondition<float> VeryHighLonging = VariableCondition("Longing", DataConditionComparators.SingleArgumentComparators.Above, 9.5f);

		public readonly DataCondition<float> EnoughNutForSex = VariableCondition("RemainingCum", DataConditionComparators.SingleArgumentComparators.Above, 20f);

		public readonly DataCondition<bool> CanDoCowgirl = VariableCondition("CanDoCowgirl", DataConditionComparators.SingleArgumentComparators.Equal, value: true);

		public readonly DataCondition<int> RecentlyWokeUp = VariableCondition("AwakeFor", DataConditionComparators.SingleArgumentComparators.Below, 30);

		public readonly DataCondition<int> RecentlySex = VariableCondition("TimeSinceLastFuck", DataConditionComparators.SingleArgumentComparators.Below, 30);

		public readonly DataCondition<bool> LastSleptWithBot = VariableConditionBoolTrue("lastSleptWithBot");

		public readonly DataCondition<bool> LastSleptAlone = VariableConditionBoolFalse("lastSleptWithBot");

		public readonly DataCondition<bool> IsBotSmart = VariableConditionBoolTrue("IsBotSmart");

		public readonly DataCondition<bool> EverFuckedBot = VariableConditionBoolTrue("EverFuckedBot");

		public readonly AndCondition Melissa4Before5;

		public readonly AndCondition Melissa4Before5NotRejection;

		public readonly FlagConditionSpecialWithTime Melissa5Done = FlagSetSpecialWithTime("Melissa5", GameVariables.CheckType.Start, 0, GameVariables.TimeCheckType.None);

		public readonly FlagConditionSpecialWithTime Melissa5NotDone = FlagSetSpecialWithTime("Melissa5", GameVariables.CheckType.Start, 0, GameVariables.TimeCheckType.None, negate: true);

		public readonly StoryCondition MelissaRejection = FlagSet("Melissa4rejection");

		public readonly StoryCondition MelissaAlive = And(FlagNotSet("Melissa14_DestroyEnd"), FlagNotSet("Melissa14_LeaveEnd"), FlagNotSet("Melissa22"));

		public readonly StoryCondition MelissaNotInShop = Or(And(FlagSet("Melissa12"), FlagNotSet("Melissa14_StillFriends")), FlagSet("Melissa22"));

		public readonly StoryCondition MelissaAnyEnding = Or(FlagSet("Melissa14_DestroyEnd"), FlagSet("Melissa14_LeaveEnd"), FlagSet("Melissa25"));

		public readonly StoryCondition AnnalieRunwayReady = And(FlagSet("Annalie_ending7"), FlagNotSet("Annalie_ending8"));

		public readonly StoryCondition ChurchAnyEndingDone = VariableConditionBoolTrue("AnyChurchEndingDone");

		public readonly StoryCondition JoinUsDAKDanger = And(FlagSet("joinus7_richard2"), FlagNotSet("joinus7_richard3"));

		public readonly StoryCondition CanGoOutsideWithBot = VariableConditionBoolTrue("CanGoOutsideWithBot");

		public readonly StoryCondition HasTheOptionToGoOutsideWithBot = VariableConditionBoolTrue("HasTheOptionToGoOutsideWithBot");

		public readonly StoryCondition HasLegsAndOneArm = VariableConditionBoolTrue("HasBothLegsAndAtLeastOneArm");

		public readonly StoryCondition MadeNoiseLastSex = VariableConditionBoolTrue("MadeNoiseLastSexScene");

		public readonly StoryCondition CanStartIslandEnding;

		public readonly StoryCondition CanStartPoorReturnHomeFoodEvent;

		public readonly StoryCondition CanStartAnyReturnHomeFoodEvent;

		public readonly StoryCondition BotMetAnnalie = FlagSet("GoingOutside3");

		public readonly StoryCondition HasAnnalieHairstyle = VariableConditionBoolTrue("HasAnnalieHairstyle");

		public readonly StoryCondition CanBotStartDateFromRoom = And(VariableConditionBoolTrue("CanGoOutsideWithBot"), VariableConditionBoolTrue("IsItSafeToStartLongActivity"), FlagSetBeforeRelative("lastdate", 720));

		public CommonConditionsContainer()
		{
			Melissa4Before5 = And(FlagSet("Melissa4"), Melissa5NotDone);
			Melissa4Before5NotRejection = And(FlagSet("Melissa4"), Melissa5NotDone, FlagNotSet("Melissa4rejection"));
			CanStartPoorReturnHomeFoodEvent = And(FlagSetBeforeRelative("housewife6", 2880), FlagNotSet("richpeopleshop_membership"), MoneyBelow(40000), VariableCondition("Satiation", DataConditionComparators.SingleArgumentComparators.Below, 0.5f), SympathyAffection, HasLegsAndOneArm);
			CanStartAnyReturnHomeFoodEvent = And(FlagSetBeforeRelative("housewife6", 2880), VariableCondition("Satiation", DataConditionComparators.SingleArgumentComparators.Below, 0.5f), SympathyAffection, HasLegsAndOneArm);
			CanStartIslandEnding = And(AfterRiots, MoneyAbove(10000000), FlagSet("Annalie_cafe2"), MelissaAnyEnding, FlagSet("fishing1"), FlagSetBeforeRelative("housewife3", 4320), FlagSetBeforeRelative("richpeopleshop_membership", 4320), FlagSetBeforeRelative("Bathroom4", 2880), ChurchAnyEndingDone, Or(FlagNotSet("church-endJustGoThereGood"), FlagSet("ice_skating1")), SympathyLove, FlagSet("LandlordMoneyEvent"), FlagSet("groceries"));
		}
	}

	protected readonly bool Negate;

	public static readonly ConstCondition AlwaysTrueCondition = new ConstCondition(negate: false);

	public static readonly ConstCondition AlwaysFalseCondition = new ConstCondition(negate: true);

	public static readonly CommonConditionsContainer CommonConditions = new CommonConditionsContainer();

	protected StoryCondition(bool negate)
	{
		Negate = negate;
	}

	protected abstract bool InnerConditionMet();

	public bool ConditionMet()
	{
		return Negate != InnerConditionMet();
	}

	public abstract string Explain();

	public abstract string GenerateCode();

	protected static string TimeCheckTypeToCode(GameVariables.TimeCheckType timeCheck)
	{
		return string.Format("{0}.{1}.{2}", "GameVariables", "TimeCheckType", timeCheck);
	}

	protected static string CheckTypeToCode(GameVariables.CheckType checkType)
	{
		return string.Format("{0}.{1}.{2}", "GameVariables", "TimeCheckType", checkType);
	}

	protected string GenerateCodeNegationParameter()
	{
		if (!Negate)
		{
			return "";
		}
		return ", true";
	}

	public static FlagConditionBasic FlagSet(string flag, bool negate = false)
	{
		return new FlagConditionBasic(flag, negate);
	}

	public static FlagConditionBasic FlagNotSet(string flag)
	{
		return new FlagConditionBasic(flag, negate: true);
	}

	public static FlagConditionWithTime FlagSetBeforeRelative(string flag, int time, bool negate = false)
	{
		return new FlagConditionWithTime(flag, -time, GameVariables.TimeCheckType.BeforeRelative, negate);
	}

	public static FlagConditionWithTime FlagSetAfterRelative(string flag, int time, bool negate = false)
	{
		return new FlagConditionWithTime(flag, -time, GameVariables.TimeCheckType.AfterRelative, negate);
	}

	public static FlagConditionWithTime FlagSetWithTime(string flag, int timePeriod, GameVariables.TimeCheckType timeCheck, bool negate = false)
	{
		return new FlagConditionWithTime(flag, timePeriod, timeCheck, negate);
	}

	public static FlagConditionSpecialWithTime FlagSetSpecialWithTime(string flag, GameVariables.CheckType checkType, int timePeriod, GameVariables.TimeCheckType timeCheck, bool negate = false)
	{
		return new FlagConditionSpecialWithTime(flag, checkType, timePeriod, timeCheck, negate);
	}

	public static LuaStoryCondition Lua(string conditionCode, bool negate = false)
	{
		return new LuaStoryCondition(conditionCode, negate);
	}

	public static DelegateCondition Delegate(Func<bool> condition, bool negate = false)
	{
		return new DelegateCondition(condition, negate);
	}

	public static DataCondition<bool> VariableConditionBoolTrue(string name)
	{
		return new DataCondition<bool>(DataConditionValues.Create<bool>(name), new DataConditionComparators.EqualityConditionComparator<bool>(compareValue: true));
	}

	public static DataCondition<bool> VariableConditionBoolFalse(string name)
	{
		return new DataCondition<bool>(DataConditionValues.Create<bool>(name), new DataConditionComparators.EqualityConditionComparator<bool>(compareValue: true), negate: true);
	}

	public static DataCondition<T> VariableCondition<T>(string name, DataConditionComparators.SingleArgumentComparators comparatorType, T value) where T : IComparable<T>
	{
		var (comparator, negate) = DataConditionComparators.GetSingleArgumentComparator(comparatorType, value);
		return new DataCondition<T>(DataConditionValues.Create<T>(name), comparator, negate);
	}

	public static DataCondition<int> MoneyAbove(int value)
	{
		return VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, value);
	}

	public static DataCondition<int> MoneyBelow(int value)
	{
		return VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Below, value);
	}

	public static DataCondition<T> VariableCondition<T>(string name, DataConditionComparators.TwoArgumentComparators comparatorType, T value, T value2) where T : IComparable<T>
	{
		var (comparator, negate) = DataConditionComparators.GetTwoArgumentComparator(comparatorType, value, value2);
		return new DataCondition<T>(DataConditionValues.Create<T>(name), comparator, negate);
	}

	public static DataCondition<TValue> ModelBrainStateVariableCondition<TValue, TState>(string name, DataConditionComparators.SingleArgumentComparators comparatorType, TValue value) where TValue : IComparable<TValue> where TState : ModelBrainState
	{
		var (comparator, negate) = DataConditionComparators.GetSingleArgumentComparator(comparatorType, value);
		return new DataCondition<TValue>(DataConditionValues.ModelBrainStateConditionValue<TValue, TState>.Create(name), comparator, negate);
	}

	public static DataCondition<TValue> ModelBrainStateVariableCondition<TValue, TState>(string name, DataConditionComparators.TwoArgumentComparators comparatorType, TValue value, TValue value2) where TValue : IComparable<TValue> where TState : ModelBrainState
	{
		var (comparator, negate) = DataConditionComparators.GetTwoArgumentComparator(comparatorType, value, value2);
		return new DataCondition<TValue>(DataConditionValues.ModelBrainStateConditionValue<TValue, TState>.Create(name), comparator, negate);
	}

	public static DataCondition<int> HourBetweenEqual(int t1, int t2)
	{
		if (t2 > t1)
		{
			return VariableCondition("Hour", DataConditionComparators.TwoArgumentComparators.BetweenEqual, t1, t2);
		}
		return VariableCondition("Hour", DataConditionComparators.TwoArgumentComparators.AroundEqual, t2, t1);
	}

	public static DataCondition<int> TimeAfter(int time)
	{
		return VariableCondition("time", DataConditionComparators.SingleArgumentComparators.AboveEqual, time);
	}

	public static DataCondition<int> DayAbove(int t1)
	{
		return VariableCondition("Day", DataConditionComparators.SingleArgumentComparators.Above, t1);
	}

	public static DataCondition<int> DayBelow(int t1)
	{
		return VariableCondition("Day", DataConditionComparators.SingleArgumentComparators.Below, t1);
	}

	public static ItemCondition HasItem(GameId itemId, bool negate = false)
	{
		return new ItemCondition(itemId, ItemCondition.ItemComparisonTypeEnum.InInventoryById, negate);
	}

	public static ItemCondition HasItemEquipped(GameId itemId, bool negate = false)
	{
		return new ItemCondition(itemId, ItemCondition.ItemComparisonTypeEnum.EquippedById, negate);
	}

	public static ItemInSlotCondition ItemInSlotCondition(string slot, bool negate = false)
	{
		return new ItemInSlotCondition(negate, slot);
	}

	public static StoryEntryPointOptionsExistCondition StoryAutoEntryPointAnyExistCondition(string name, bool negate = false)
	{
		return new StoryEntryPointOptionsExistCondition(StoryEntryPointOptionsExistCondition.EntryTypeEnum.Auto, name, negate);
	}

	public static StoryEntryPointOptionsExistCondition StoryMenuEntryPointAnyExistCondition(string name, bool negate = false)
	{
		return new StoryEntryPointOptionsExistCondition(StoryEntryPointOptionsExistCondition.EntryTypeEnum.Menu, name, negate);
	}

	public static StoryEntryPointOptionsExistCondition StoryMenuEntryPointConditionalExistCondition(string name, bool negate = false)
	{
		return new StoryEntryPointOptionsExistCondition(StoryEntryPointOptionsExistCondition.EntryTypeEnum.MenuConditional, name, negate);
	}

	public static InteractionMemoryCondition CreateInteractionMemoryActiveForCondition(InteractionMemoryEnum memory, float time, bool negate = false)
	{
		return new InteractionMemoryCondition(memory, InteractionMemoryCondition.MemoryConditionTypeEnum.ActiveFor, time, negate);
	}

	public static InteractionMemoryCondition CreateInteractionMemoryRecentCondition(InteractionMemoryEnum memory, float time = 3f, bool negate = false)
	{
		return new InteractionMemoryCondition(memory, InteractionMemoryCondition.MemoryConditionTypeEnum.ActiveOrRecent, time, negate);
	}

	public static FishCaughtCondition FishesCaught(ItemEnum fish, int count, bool negate = false)
	{
		return new FishCaughtCondition(fish.ToGameId(), count, negate);
	}

	public static TotalFishesCaughtCondition FishesCaught(int count, bool negate = false)
	{
		return new TotalFishesCaughtCondition(count, negate);
	}

	public static FishSizeCondition CaughtFishAtLeastPercentile(ItemEnum fish, float p1, bool negate = false)
	{
		return new FishSizeCondition(fish.ToGameId(), p1, negate);
	}

	public static StoryCondition CaughtFishBig(ItemEnum fish)
	{
		return CaughtFishAtLeastPercentile(fish, 0.8f);
	}

	public static OrCondition Or(params StoryCondition[] conditions)
	{
		return new OrCondition(conditions, negate: false);
	}

	public static OrCondition NotOr(params StoryCondition[] conditions)
	{
		return new OrCondition(conditions, negate: true);
	}

	public static AndCondition And(params StoryCondition[] conditions)
	{
		return new AndCondition(conditions, negate: false);
	}

	public static AndCondition NotAnd(params StoryCondition[] conditions)
	{
		return new AndCondition(conditions, negate: true);
	}

	public static StoryCondition HourRandom(int seed, float odds)
	{
		return new TimedRandom(TimedRandomType.HourDay, seed, odds);
	}

	public static StoryCondition DayRandom(int seed, float odds)
	{
		return new TimedRandom(TimedRandomType.Day, seed, odds);
	}

	protected bool Equals(StoryCondition other)
	{
		return Negate == other.Negate;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (this == obj)
		{
			return true;
		}
		if (obj.GetType() != GetType())
		{
			return false;
		}
		return Equals((StoryCondition)obj);
	}

	public override int GetHashCode()
	{
		bool negate = Negate;
		return negate.GetHashCode();
	}
}
