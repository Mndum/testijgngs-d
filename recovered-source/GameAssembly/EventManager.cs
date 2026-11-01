using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryConditions;
using UnityEngine;

[Serializable]
public class EventManager : GameVariablesSubClass, ISerializationCallbackReceiver
{
	[Serializable]
	public class PlanningData
	{
		public enum TimeOffsetType
		{
			Relative,
			Absolute,
			Zero
		}

		public int dayOffset;

		public int hourOffset;

		public int minuteOffset;

		public TimeOffsetType timeOffsetType;

		public bool forceHourInDay;

		[Range(0f, 23f)]
		public int plannedHour;

		[Range(0f, 59f)]
		public int plannedMinute;

		public int randomOffsetBefore;

		public int randomOffsetAfter;

		private int TimeOffset => dayOffset * 60 * 24 + hourOffset * 60 + minuteOffset;

		private string RandomInfoShort()
		{
			if (randomOffsetBefore >= randomOffsetAfter)
			{
				return " exactly";
			}
			return $"+<{randomOffsetBefore}-{randomOffsetAfter}) minutes";
		}

		public string PlanningInfo()
		{
			List<int> list = new List<int>
			{
				GameVariables.TimeFromDate(1, 3, 50),
				GameVariables.TimeFromDate(1, 5, 50),
				GameVariables.TimeFromDate(1, 8, 50),
				GameVariables.TimeFromDate(1, 11, 59),
				GameVariables.TimeFromDate(1, 12, 20),
				GameVariables.TimeFromDate(15, 12, 20),
				GameVariables.TimeFromDate(23, 23, 59),
				GameVariables.TimeFromDate(23, 0, 2)
			};
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables != null)
			{
				list.Add(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time);
			}
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				foreach (int item in list)
				{
					stringBuilder.AppendLine("At " + GameVariables.TimeToDateString(item) + " planned to " + GameVariables.TimeToDateString(GetPlannedTime(item)));
				}
				return stringBuilder.ToString();
			}
		}

		public string PlanningInfoShort()
		{
			string text = "";
			switch (timeOffsetType)
			{
			default:
				text = "Plans in " + GameVariables.TimeToDateString(TimeOffset) + " from now";
				break;
			case TimeOffsetType.Absolute:
				text = "Plans for " + GameVariables.TimeToDateString(TimeOffset);
				break;
			case TimeOffsetType.Zero:
				return "Plans event now!";
			}
			if (!forceHourInDay)
			{
				return text + RandomInfoShort();
			}
			return text + $" and forces it to be at {plannedHour}:{plannedMinute}" + RandomInfoShort();
		}

		public int GetPlannedTime(int currentTime)
		{
			int num = timeOffsetType switch
			{
				TimeOffsetType.Absolute => TimeOffset, 
				TimeOffsetType.Zero => 0, 
				_ => TimeOffset + currentTime, 
			};
			int num2 = 0;
			if (randomOffsetBefore < randomOffsetAfter)
			{
				num2 = UnityEngine.Random.Range(randomOffsetBefore, randomOffsetAfter);
			}
			if (!forceHourInDay)
			{
				return num + num2;
			}
			int num3 = num / 1440;
			int num4 = num3 * 1440 + plannedHour * 60 + plannedMinute + num2;
			if (num4 <= currentTime)
			{
				num4 = (num3 + 1) * 1440 + plannedHour * 60 + plannedMinute + num2;
				if (num4 <= currentTime)
				{
					return currentTime + 1;
				}
			}
			return num4;
		}
	}

	[Serializable]
	public class NormalEvent
	{
		public EventHolder eventHolder;

		public int startTime;
	}

	private List<RuntimeEvent> _runTimeEvents;

	[SerializeField]
	private List<NormalEvent> _events;

	public IEnumerable<RuntimeEvent> RuntimeEvents => _runTimeEvents;

	public bool DisableHungerAndMentalHealthInfo { get; set; }

	public static EventManager CreateInstance()
	{
		EventManager eventManager = new EventManager();
		eventManager.InitRuntimeEvents();
		return eventManager;
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		InitRuntimeEvents();
	}

	public static void FillRuntimeEvents(List<RuntimeEvent> toFill)
	{
		toFill.Add(new RuntimeEvent("DAY 2, after 12:32", FungusBlocks.RE_DAY_2__after_12_32, 2, 9, 32));
		toFill.Add(new RuntimeEvent("DAY 3, 16:29", FungusBlocks.RE_DAY_3__16_29, 3, 16, 29, PreviousEventFlagCondition()));
		toFill.Add(new RuntimeEvent("DAY 4, 16:29", FungusBlocks.RE_DAY_4__16_29, 4, 16, 29, PreviousEventFlagCondition()));
		toFill.Add(new RuntimeEvent("DAY 5, 22:15", FungusBlocks.RE_DAY_5__22_15, 5, 22, 15, PreviousEventFlagCondition()));
		toFill.Add(new RuntimeEvent("DAY 6, 22:12", FungusBlocks.RE_DAY_6__22_12, 6, 22, 12, PreviousEventFlagCondition()));
		toFill.Add(new RuntimeEvent("DAY 7, 22:17", FungusBlocks.RE_DAY_7__22_17, 7, 22, 17, PreviousEventFlagCondition()));
		toFill.Add(new RuntimeEvent(null, FungusBlocks.RE_RentPayment, 8, 10, 0, PreviousEventFlagCondition(), StoryCondition.VariableConditionBoolTrue("IsRentDue"), StoryCondition.CommonConditions.LandlordNotAsleep));
		toFill.Add(new RuntimeEvent("DAY 9, 17:12", FungusBlocks.RE_DAY_9__17_12, 9, 17, 12, PreviousEventFlagCondition()));
		toFill.Add(new RuntimeEvent("DAY 10, 22:17", FungusBlocks.RE_DAY_10__22_17, 10, 22, 17, PreviousEventFlagCondition()));
		toFill.Add(new RuntimeEvent("DAY 11, 16:56", FungusBlocks.RE_DAY_11__16_56, 11, 16, 56, PreviousEventFlagCondition()));
		toFill.Add(new RuntimeEvent("DAY 13, 14:52", FungusBlocks.RE_Day13_DAY_13__14_52, 13, 14, 52, PreviousEventFlagCondition()));
		toFill.Add(new RuntimeEvent("DAY 13, 22:17", FungusBlocks.RE_DAY_13__22_17, 13, 22, 17, PreviousEventFlagCondition()));
		int num = 1440;
		toFill.Add(new RuntimeEvent("casino", FungusBlocks.RE_CasinoStart, StoryCondition.DayAbove(3), StoryCondition.HourBetweenEqual(21, 4), StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 800)));
		toFill.Add(new RuntimeEvent("LandlordConspiracy2", FungusBlocks.RE_LandlordConspiracy2, StoryCondition.FlagSetBeforeRelative("LandlordConspiracy1", 1440), StoryCondition.HourBetweenEqual(8, 20), StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 100), StoryCondition.VariableConditionBoolFalse("IsRentDue")));
		toFill.Add(new RuntimeEvent("Search", FungusBlocks.RE_Search, StoryCondition.VariableCondition("search", DataConditionComparators.SingleArgumentComparators.Above, 0.6f), StoryCondition.Or(StoryCondition.FlagSet("exposure_night"), StoryCondition.FlagSet("exposure_day")), StoryCondition.HourBetweenEqual(8, 20)));
		toFill.Add(new RuntimeEvent(null, FungusBlocks.RE_Ending_Search, StoryCondition.VariableCondition("search", DataConditionComparators.SingleArgumentComparators.Above, 1f), StoryCondition.HourBetweenEqual(8, 20), StoryCondition.FlagSetBeforeRelative("Search", num / 2)));
		toFill.Add(new RuntimeEvent("SearchLandlord", FungusBlocks.RE_SearchLandlord, StoryCondition.FlagSetBeforeRelative("angry_landlord", 1440), StoryCondition.HourBetweenEqual(8, 20)));
		toFill.Add(new RuntimeEvent("Trash1", FungusBlocks.RE_Trash_Trash1, StoryCondition.FlagSetBeforeRelative("illegalTrash", num * 2)));
		toFill.Add(new RuntimeEvent("FedPhoneCall", FungusBlocks.RE_Trash_Trash2, StoryCondition.FlagSetBeforeRelative("Trash1", num * 2)));
		toFill.Add(new RuntimeEvent("TooMuchCum1", FungusBlocks.RE_TooMuchCum1, StoryCondition.VariableCondition("MaxCum", DataConditionComparators.SingleArgumentComparators.Above, 710f), StoryCondition.VariableCondition("RemainingCum", DataConditionComparators.SingleArgumentComparators.Above, 1000f)));
		toFill.Add(new RuntimeEvent(null, FungusBlocks.RE_TooMuchCumEnding, StoryCondition.FlagSet("TooMuchCum1"), StoryCondition.VariableCondition("MaxCum", DataConditionComparators.SingleArgumentComparators.Above, 1010f), StoryCondition.VariableCondition("RemainingCum", DataConditionComparators.SingleArgumentComparators.Above, 1200f), StoryCondition.CommonConditions.SympathyLike));
		toFill.Add(new RuntimeEvent("church-nun_back6", FungusBlocks.RE_NunBack6, StoryCondition.FlagSetBeforeRelative("church-nun_back_bot_dialogue", num / 2), StoryCondition.CommonConditions.ItsDay));
		toFill.Add(new RuntimeEvent(null, FungusBlocks.RE_BigDaddyKnowsEnding, StoryCondition.FlagSetBeforeRelative("church-Big_Daddy_Knows", 60), StoryCondition.CommonConditions.ItsEvening));
		toFill.Add(new RuntimeEvent("church-whatHappenedAfter", FungusBlocks.Church_What_happened_after, StoryCondition.FlagSetBeforeRelative("church-endJustGoThereGood", num * 4), StoryCondition.CommonConditions.ItsDay));
		toFill.Add(new RuntimeEvent("dinnerwithbigdaddy1", FungusBlocks.RE_DinnerWithBigDaddy1, StoryCondition.Or(StoryCondition.FlagSetBeforeRelative("DinnerWithBigDaddy1Invited", num), StoryCondition.FlagSetBeforeRelative("bigdaddyinvitation", num)), StoryCondition.CommonConditions.ItsEvening));
		toFill.Add(new RuntimeEvent("BitchEventHappened", FungusBlocks.RE_BitchEvent, StoryCondition.FlagSetBeforeRelative("BitchEventStarted", num * 5), StoryCondition.CommonConditions.ItsEvening));
		toFill.Add(new RuntimeEvent("Blaubeim1", FungusBlocks.RE_Blaubeim1, StoryCondition.DayAbove(17), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.7f), StoryCondition.CommonConditions.ItsEvening));
		toFill.Add(new RuntimeEvent("cocktractsunlocked", FungusBlocks.RE_CocktractsStart, StoryCondition.FlagSet("cocktractsunlockedplanned"), StoryCondition.CommonConditions.ItsDay));
		toFill.Add(new RuntimeEvent("diarrheaHappened", FungusBlocks.RE_DiarrheaEvent, StoryCondition.FlagSetBeforeRelative("diarrhea", 60)));
		toFill.Add(new RuntimeEvent("JoinUsEmail", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail), 12, 15, 59));
		toFill.Add(new RuntimeEvent("joinusemail2", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail2), StoryCondition.FlagSetBeforeRelative("joinus", 1440), StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 1000)));
		toFill.Add(new RuntimeEvent("joinusemail3", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail3), StoryCondition.FlagSetBeforeRelative("JoinUs2", 1440), StoryCondition.FlagSetBeforeRelative("bigdaddystoryline3", 720), StoryCondition.CommonConditions.AfterRiots));
		toFill.Add(new RuntimeEvent("joinusemail2rejected", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail2Rejected), StoryCondition.FlagSetBeforeRelative("joinusemail2opened", 300), StoryCondition.FlagNotSet("JoinUs2")));
		toFill.Add(new RuntimeEvent("joinus3Richard", FungusBlocks.RE_JoinUs3Richard, StoryCondition.FlagSetBeforeRelative("joinus3", num / 2), StoryCondition.CommonConditions.ItsNotNight));
		toFill.Add(new RuntimeEvent("joinusemail4", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail4), StoryCondition.FlagSetBeforeRelative("joinus3Richard", num / 2), StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("joinus4Richard", FungusBlocks.RE_JoinUs4Richard, StoryCondition.FlagSetBeforeRelative("joinus4", num / 2), StoryCondition.CommonConditions.ItsNotNight));
		toFill.Add(new RuntimeEvent("joinuseemail5", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail5), StoryCondition.FlagSetBeforeRelative("joinus4Richard", num / 2), StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("joinus5richard", FungusBlocks.RE_JoinUs5Richard, StoryCondition.FlagSetBeforeRelative("joinus5", num / 2), StoryCondition.CommonConditions.ItsEvening));
		toFill.Add(new RuntimeEvent("joinuseemail6", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail6), StoryCondition.FlagSetBeforeRelative("joinus5richard", num / 2), StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("joinus6richard", FungusBlocks.RE_JoinUs6Richard, StoryCondition.FlagSetBeforeRelative("joinus6", num / 2), StoryCondition.CommonConditions.ItsDay));
		toFill.Add(new RuntimeEvent("joinus6mission", FungusBlocks.RE_JoinUs6Mission, StoryCondition.FlagSetBeforeRelative("joinus6richard", num), StoryCondition.CommonConditions.ItsEvening));
		toFill.Add(new RuntimeEvent("joinus6mission_bailoutendafter", FungusBlocks.RE_JoinUs6MissionBailoutAfter, StoryCondition.FlagSetBeforeRelative("joinus6mission_bailoutend", num)));
		toFill.Add(new RuntimeEvent("joinus6mission_shanicephonecallafter", FungusBlocks.RE_JoinUs6MissionShaniceAfter, StoryCondition.FlagSetBeforeRelative("joinus6mission_shanicephonecall", 240), StoryCondition.CommonConditions.ItsDay));
		toFill.Add(new RuntimeEvent("joinus7_bigdaddy1email", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail7_BigDaddy1), StoryCondition.FlagSetBeforeRelative("joinus6mission_bigdaddychosen", num / 2), StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("joinus7_bigdaddy2", FungusBlocks.RE_JoinUs7BigDaddy2, StoryCondition.FlagSetBeforeRelative("joinus7_bigdaddy1", num / 2), StoryCondition.CommonConditions.ItsDay));
		toFill.Add(new RuntimeEvent("joinus7_bigdaddy4email", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail7_BigDaddy4), StoryCondition.FlagSetBeforeRelative("joinus7_bigdaddy3", num / 2), StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("joinus7_bigdaddy5", FungusBlocks.RE_JoinUs7BigDaddy5, StoryCondition.FlagSetBeforeRelative("joinus7_bigdaddy4", num / 2), StoryCondition.CommonConditions.ItsEvening));
		toFill.Add(new RuntimeEvent("joinus7_bigdaddy6", FungusBlocks.RE_JoinUs7BigDaddy6, StoryCondition.FlagSetBeforeRelative("joinus7_bigdaddy5", num / 2)));
		toFill.Add(new RuntimeEvent("joinus7_bigdaddy7", FungusBlocks.RE_JoinUs7BigDaddy7, StoryCondition.FlagSetBeforeRelative("joinus7_bigdaddy6", num / 2), StoryCondition.CommonConditions.ItsDay));
		toFill.Add(new RuntimeEvent("joinus7_bigdaddy8", FungusBlocks.RE_JoinUs7BigDaddy8, StoryCondition.FlagSetBeforeRelative("joinus7_bigdaddy7", num / 2), StoryCondition.HourBetweenEqual(22, 3)));
		toFill.Add(new RuntimeEvent("joinus7_bigdaddy9email", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail7_BigDaddy9), StoryCondition.FlagSetBeforeRelative("joinus7_bigdaddy8_sidewithadmin", num / 2), StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("joinus7_richard1email", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail7_Richard1), StoryCondition.FlagSetBeforeRelative("joinus6mission_richardchosen", num / 2), StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("joinus7_richard2", FungusBlocks.RE_JoinUs7Richard2, StoryCondition.FlagSetBeforeRelative("joinus7_richard1", num / 2), StoryCondition.CommonConditions.ItsDay));
		toFill.Add(new RuntimeEvent("joinus7_richard4email", new SendEmailEventTarget(EmailPrefabManager.JoinUsEmail7_Richard4), StoryCondition.FlagSetBeforeRelative("joinus7_richard3", num / 2), StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("joinus7_richard5", FungusBlocks.RE_JoinUs7Richard5, StoryCondition.FlagSetBeforeRelative("joinus7_richard4", num / 2), StoryCondition.CommonConditions.ItsDay));
		toFill.Add(new RuntimeEvent("FriendEmail", new SendEmailEventTarget(EmailPrefabManager.FriendEmail), 8, 8, 25));
		toFill.Add(new RuntimeEvent("BoBrogan", new SendEmailEventTarget(EmailPrefabManager.BoBroganEmail), 10, 17, 12));
		toFill.Add(new RuntimeEvent("FriendEmail2", new SendEmailEventTarget(EmailPrefabManager.FriendEmail2), 14, 16, 50));
		toFill.Add(new RuntimeEvent("DoctorEmail", new SendEmailEventTarget(EmailPrefabManager.DoctorEmail), 15, 17, 35));
		toFill.Add(new RuntimeEvent("Jun_Inv_Industrial_Park", FungusBlocks.RE_JunDateIndustrialPark, StoryCondition.FlagSetBeforeRelative("restaurant2", num), StoryCondition.CommonConditions.CanBotStartDateFromRoom, StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("Jun_inv_star_gazing", FungusBlocks.RE_JunStarGazing, StoryCondition.FlagSetBeforeRelative("Jun_Inv_Industrial_Park", num), StoryCondition.CommonConditions.CanBotStartDateFromRoom, StoryCondition.CommonConditions.ItsNight));
		toFill.Add(new RuntimeEvent("amusement_park1", FungusBlocks.RE_JunAmusementPark1, StoryCondition.FlagSetBeforeRelative("planetarium1", num), StoryCondition.MoneyAbove(10000), StoryCondition.CommonConditions.CanBotStartDateFromRoom, StoryCondition.HourBetweenEqual(15, 18)));
		toFill.Add(new RuntimeEvent("gokart1", FungusBlocks.RE_JunGokartDate1, StoryCondition.FlagSetBeforeRelative("amusement_park2", num), StoryCondition.MoneyAbove(1000), StoryCondition.CommonConditions.CanBotStartDateFromRoom, StoryCondition.HourBetweenEqual(13, 18)));
		toFill.Add(new RuntimeEvent("interact6", FungusBlocks.RE_ConsoleRevenge, StoryCondition.FlagSetBeforeRelative("interact5botTalk", num)));
		toFill.Add(new RuntimeEvent("Bathroom3", FungusBlocks.RE_Bathroom3, StoryCondition.HourBetweenEqual(16, 22), StoryCondition.FlagSetBeforeRelative("Bathroom2_bought", 720), StoryCondition.CommonConditions.CanGoOutsideWithBot));
		toFill.Add(new RuntimeEvent("ShadyLawyerTrickedLandlord", FungusBlocks.RE_ShadyLawyerTrickedLandlord, StoryCondition.HourBetweenEqual(12, 20), StoryCondition.FlagSetBeforeRelative("SecondPaymentDayLateBotTalk_EmployedLawyer", 720), StoryCondition.HourRandom(123, 0.5f)));
		toFill.Add(new RuntimeEvent("Melissa8", FungusBlocks.RE_Melissa8, StoryCondition.HourBetweenEqual(11, 18), StoryCondition.FlagSetBeforeRelative("Melissa7", 720)));
		toFill.Add(new RuntimeEvent("Melissa10", FungusBlocks.RE_Melissa10, StoryCondition.HourBetweenEqual(18, 24), StoryCondition.FlagSetBeforeRelative("Melissa9", 720), StoryCondition.HourRandom(962, 0.8f)));
		toFill.Add(new RuntimeEvent("Melissa13", FungusBlocks.RE_Melissa13, StoryCondition.HourBetweenEqual(11, 18), StoryCondition.FlagSetBeforeRelative("Melissa12", 720), StoryCondition.HourRandom(963, 0.8f)));
		toFill.Add(new RuntimeEvent("Melissa15", FungusBlocks.RE_Melissa15, StoryCondition.HourBetweenEqual(15, 20), StoryCondition.FlagSetBeforeRelative("Melissa14_StillFriends", 720), StoryCondition.HourRandom(466, 0.8f)));
		toFill.Add(new RuntimeEvent("Melissa20", FungusBlocks.RE_Melissa20, StoryCondition.HourBetweenEqual(10, 20), StoryCondition.FlagSetBeforeRelative("Melissa19", 720), StoryCondition.HourRandom(1235, 0.8f)));
		toFill.Add(new RuntimeEvent("Melissa22", FungusBlocks.RE_Melissa22, StoryCondition.HourBetweenEqual(11, 18), StoryCondition.FlagSetBeforeRelative("Melissa21", 720), StoryCondition.Or(StoryCondition.FlagSetBeforeRelative("Melissa21.5", 720), StoryCondition.FlagNotSet("karaoke_idol")), StoryCondition.HourRandom(1235, 0.5f)));
		toFill.Add(new RuntimeEvent("Melissa23", FungusBlocks.RE_Melissa23, StoryCondition.HourBetweenEqual(10, 20), StoryCondition.FlagSetBeforeRelative("Melissa22", 1440), StoryCondition.HourRandom(1235, 0.8f)));
		toFill.Add(new RuntimeEvent("Melissa25", FungusBlocks.RE_Melissa25, StoryCondition.FlagSetBeforeRelative("Melissa24", 1440), StoryCondition.FlagSet("Melissa23"), StoryCondition.HourRandom(4, 0.8f)));
		toFill.Add(new RuntimeEvent("Landlord_Cockblock4", FungusBlocks.RE_LandlordCockblock4, StoryCondition.FlagSet("Landlord_Cockblock_Recorded"), StoryCondition.MoneyAbove(500), StoryCondition.CommonConditions.LateAtNight));
		StoryCondition PreviousEventFlagCondition()
		{
			return StoryCondition.FlagSet(toFill[toFill.Count - 1].RelatedFlag);
		}
	}

	private void InitRuntimeEvents()
	{
		_runTimeEvents = _runTimeEvents.ClearOrCreate();
		FillRuntimeEvents(_runTimeEvents);
	}

	public void ConvertOldShittyEvents()
	{
		HashSet<string> OldRemainingStartEventMessages;
		using (MyCollectionPool<string>.GetHashSet(out OldRemainingStartEventMessages))
		{
			OldRemainingStartEventMessages.Add("DAY 2, after 12:32");
			OldRemainingStartEventMessages.Add("DAY 3, 16:29");
			OldRemainingStartEventMessages.Add("DAY 4, 16:29");
			OldRemainingStartEventMessages.Add("DAY 5, 22:15");
			OldRemainingStartEventMessages.Add("DAY 6, 22:12");
			OldRemainingStartEventMessages.Add("DAY 7, 22:17");
			OldRemainingStartEventMessages.Add("LandlordEvent");
			OldRemainingStartEventMessages.Add("DAY 9, 17:12");
			OldRemainingStartEventMessages.Add("DAY 10, 22:17");
			OldRemainingStartEventMessages.Add("DAY 11, 16:56");
			OldRemainingStartEventMessages.Add("DAY 13, 14:52");
			OldRemainingStartEventMessages.Add("DAY 13, 22:17");
			OldRemainingStartEventMessages.Add("FriendEmail");
			OldRemainingStartEventMessages.Add("BoBrogan");
			OldRemainingStartEventMessages.Add("JoinUsEmail");
			OldRemainingStartEventMessages.Add("FriendEmail2");
			OldRemainingStartEventMessages.Add("DoctorEmail");
			HashSet<string> hashSet;
			using (MyCollectionPool<string>.GetHashSet(out hashSet))
			{
				HashSet<string> hashSet2;
				using (MyCollectionPool<string>.GetHashSet(out hashSet2))
				{
					foreach (NormalEvent @event in _events)
					{
						if (@event.eventHolder == null)
						{
							continue;
						}
						string data = @event.eventHolder.data;
						switch (@event.eventHolder.eventEnum)
						{
						case EventEnum.Email:
						{
							Email email = JsonUtility.FromJson<Email>(data);
							if (email != null && email.TryConvertToNewEmail(EmailPrefabManager.AllOldEmails, out var newEmail2))
							{
								SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddEmail(newEmail2);
								HandleEmail(newEmail2);
							}
							break;
						}
						case EventEnum.NewEmail:
						{
							NewEmail newEmail = JsonUtility.FromJson<NewEmail>(data);
							if (newEmail != null)
							{
								SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddEmail(newEmail);
								HandleEmail(newEmail);
							}
							break;
						}
						case EventEnum.ItemByDrone:
						{
							Item item = JsonUtility.FromJson<Item>(data);
							if (item != null)
							{
								int timeTillDelivery = @event.startTime - RelatedGameVariables.time;
								RelatedGameVariables.deliveryManager.CreateDelivery(timeTillDelivery, DeliveryManager.BDHSender, item);
							}
							break;
						}
						case EventEnum.StoryStateBlock:
							hashSet2.Add(data);
							break;
						case EventEnum.StoryState:
						case EventEnum.FungusMessage:
							if (!OldRemainingStartEventMessages.Remove(data))
							{
								hashSet.Add(data);
							}
							break;
						default:
							Debug.LogWarning($"Unhandled event {@event} ");
							break;
						}
					}
					_events.Clear();
					foreach (string item2 in OldRemainingStartEventMessages)
					{
						RelatedGameVariables.AddFlag(item2);
					}
					foreach (KeyValuePair<string, (string, string, string)> item3 in new Dictionary<string, (string, string, string)>
					{
						["Trash/Trash1"] = ("illegalTrash", "Trash1", "FedPhoneCall"),
						["Trash/Trash2"] = ("Trash1", "FedPhoneCall", null),
						["NunBack6"] = ("church-nun_back_bot_dialogue", "church-nun_back6", "church-nun_back7"),
						["BigDaddyKnowsEnding"] = ("church-Big_Daddy_Knows", null, null),
						["WhatHappenedAfter"] = ("church-endJustGoThereGood", "church-whatHappenedAfter", "church-nun_back1"),
						["DinnerWithBigDaddy1"] = ("DinnerWithBigDaddy1Invited", "dinnerwithbigdaddy1", "bigdaddystoryline4"),
						["CocktractsStart"] = ("cocktractsunlockedplanned", "cocktractsunlocked", null),
						["DiarrheaEvent"] = ("diarrhea", "diarrheaHappened", null),
						["LandlordEvent"] = ("LandlordEvent", null, null)
					})
					{
						string key = item3.Key;
						var (flag, text, text2) = item3.Value;
						if (hashSet2.Remove(key))
						{
							RelatedGameVariables.AddFlag(flag);
						}
						else if (text != null && text2 != null && RelatedGameVariables.CheckFlag(text2) && !RelatedGameVariables.CheckFlag(text))
						{
							RelatedGameVariables.AddFlag(text);
						}
					}
					foreach (KeyValuePair<string, (string, string, string)> item4 in new Dictionary<string, (string, string, string)>
					{
						["BitchEvent"] = ("BitchEventStarted", "BitchEventHappened", null),
						["Blaubeim1"] = ("BlaubeimEventStarted", "Blaubeim1", "BlaubeimMedicine")
					})
					{
						string key2 = item4.Key;
						var (flag2, text3, text4) = item4.Value;
						if (hashSet.Remove(key2))
						{
							RelatedGameVariables.AddFlag(flag2);
						}
						else if (text3 != null && text4 != null && RelatedGameVariables.CheckFlag(text4) && !RelatedGameVariables.CheckFlag(text3))
						{
							RelatedGameVariables.AddFlag(text3);
						}
					}
					if (hashSet2.Any())
					{
						Debug.LogError("Following event blocks weren't handled: " + hashSet2.ToStringBetter());
					}
					if (hashSet.Any())
					{
						Debug.LogError("Following old event messages weren't handled: " + hashSet.ToStringBetter());
					}
					if (RelatedGameVariables.Day > 10)
					{
						RelatedGameVariables.AddFlag("diarrheaHappened");
					}
					RelatedGameVariables.AddFlag("StreamStarted");
					if (RelatedGameVariables.CheckFlag("church-nextRevival"))
					{
						RelatedGameVariables.AddFlag("church-shaniceFoundCumInBot");
						RelatedGameVariables.AddFlag("church-shaniceFoundCumInBot");
					}
					RelatedGameVariables.AddFlag("afterprologue");
				}
			}
		}
		void HandleEmail(NewEmail newEmail3)
		{
			if (newEmail3.onReadEventHolder != null && newEmail3.onReadEventHolder.eventEnum == EventEnum.FungusMessage)
			{
				string data2 = newEmail3.onReadEventHolder.data;
				if (OldRemainingStartEventMessages.Remove(data2))
				{
					Debug.LogWarning("\"" + data2 + "\" not handled!");
				}
			}
			newEmail3.onReadEventHolder = null;
		}
	}

	public void SyncConditionalEvents()
	{
		GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
		if (!gameVariables.CheckFlag("BitchEventStarted") && gameVariables.streamedFor > 600 && gameVariables.followers > 2000 && gameVariables.Day > 10)
		{
			gameVariables.AddFlag("BitchEventStarted");
			gameVariables.PlanEmail(EmailPrefabManager.GetEmail(EmailPrefabManager.BitchEmail), new PlanningData
			{
				dayOffset = 2,
				forceHourInDay = true,
				hourOffset = 0,
				minuteOffset = 0,
				plannedHour = 12,
				plannedMinute = 45,
				timeOffsetType = PlanningData.TimeOffsetType.Relative
			});
			gameVariables.PlanEmail(EmailPrefabManager.GetEmail(EmailPrefabManager.BitchEmail2), new PlanningData
			{
				dayOffset = 3,
				forceHourInDay = true,
				hourOffset = 0,
				minuteOffset = 0,
				plannedHour = 23,
				plannedMinute = 11,
				timeOffsetType = PlanningData.TimeOffsetType.Relative
			});
			gameVariables.PlanEmail(EmailPrefabManager.GetEmail(EmailPrefabManager.BitchEmail3), new PlanningData
			{
				dayOffset = 4,
				forceHourInDay = true,
				hourOffset = 0,
				minuteOffset = 0,
				plannedHour = 13,
				plannedMinute = 34,
				timeOffsetType = PlanningData.TimeOffsetType.Relative
			});
		}
	}

	private bool SyncHunger(GameScript gameScript, GameVariables gameVariables)
	{
		if (gameVariables.TimeSinceLastHungerInfo <= 720)
		{
			return false;
		}
		if (!gameVariables.CheckFlag("food"))
		{
			gameVariables.lastHungerInfoAt = gameVariables.time;
			return false;
		}
		if (Mathf.Approximately(gameVariables.Satiation, 0f))
		{
			if (gameVariables.Health > 0.7f)
			{
				gameScript.ShowText(LOC.Other.VeryHungry_HighHealth);
			}
			else if (gameVariables.Health > 0.3f)
			{
				gameScript.ShowText(LOC.Other.VeryHungry_MediumHealth);
			}
			else
			{
				if (gameVariables.Health == 0f)
				{
					return false;
				}
				gameScript.ShowText(LOC.Other.VeryHungry_LowHealth);
			}
		}
		else if (gameVariables.Satiation < 0.15f)
		{
			if (gameVariables.Health > 0.7f)
			{
				gameScript.ShowText(LOC.Other.MediumHungry_HighHealth);
			}
			else if (gameVariables.Health > 0.3f)
			{
				gameScript.ShowText(LOC.Other.MediumHungry_MediumHealth);
			}
			else
			{
				gameScript.ShowText(LOC.Other.MediumHungry_LowHealth);
			}
		}
		else
		{
			if (!(gameVariables.Satiation < 0.3f))
			{
				return false;
			}
			if (gameVariables.Health > 0.7f)
			{
				gameScript.ShowText(LOC.Other.LittleHungry_HighHealth);
			}
			else if (gameVariables.Health > 0.3f)
			{
				gameScript.ShowText(LOC.Other.LittleHungry_MediumHealth);
			}
			else
			{
				gameScript.ShowText(LOC.Other.LittleHungry_LowHealth);
			}
		}
		gameVariables.lastHungerInfoAt = gameVariables.time;
		return true;
	}

	private bool SyncMentalHealth(GameScript gameScript, GameVariables gameVariables)
	{
		if (!gameVariables.TimeForNextMentalHealthInfo)
		{
			return false;
		}
		if (Mathf.Approximately(gameVariables.MentalHealth, 0f))
		{
			if (!gameVariables.CheckFlag("MentalHealthDeathDoor"))
			{
				gameScript.ShowText(LOC.Other.MentalHealth_DeathDoor);
				gameVariables.AddFlag("MentalHealthDeathDoor");
			}
			else
			{
				if (gameVariables.CheckFlag("schizo") && gameVariables.sympathy >= 100)
				{
					return false;
				}
				gameScript.ChangeToStoryStateBlockReference(FungusBlocks.RE_GenericSchizoEnd);
			}
		}
		else if (gameVariables.MentalHealth < 0.11f)
		{
			gameScript.ChangeToStoryStateBlockReference(FungusBlocks.RE_Spook2);
		}
		else if (gameVariables.MentalHealth < 0.31f)
		{
			gameScript.ChangeToStoryStateBlockReference(FungusBlocks.RE_Spook1);
		}
		else if (gameVariables.MentalHealth < 0.51f)
		{
			gameScript.ShowText(LOC.Other.MentalHealth_HeadacheHigh);
		}
		else
		{
			if (!(gameVariables.MentalHealth < 0.71f))
			{
				return false;
			}
			gameScript.ShowText(LOC.Other.MentalHealth_HeadacheLow);
		}
		gameVariables.lastMentalHealthInfoAt = gameVariables.time;
		return true;
	}

	private bool SyncHungerAndMentalHealth()
	{
		GameScript instance = SingletonLoadableMonoBehaviour<GameScript>.Instance;
		GameVariables gameVariables = instance.GameVariables;
		if (gameVariables.Health < 0.05f)
		{
			instance.ChangeToStoryStateBlockReference(FungusBlocks.RE_HealthEnd);
			return true;
		}
		if (SyncHunger(instance, gameVariables))
		{
			return true;
		}
		if (SyncMentalHealth(instance, gameVariables))
		{
			return true;
		}
		return false;
	}

	public bool SyncEvents()
	{
		for (int i = 0; i < _runTimeEvents.Count; i++)
		{
			RuntimeEvent runtimeEvent = _runTimeEvents[i];
			if (!RelatedGameVariables.CheckFlag(runtimeEvent.RelatedFlag) && runtimeEvent.Conditions.ConditionsMet())
			{
				if (runtimeEvent.RelatedFlag != null)
				{
					RelatedGameVariables.AddFlag(runtimeEvent.RelatedFlag);
				}
				if (runtimeEvent.Target.Start(runtimeEvent))
				{
					return true;
				}
			}
		}
		if (RelatedGameVariables.IsOutOfStamina)
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Out_of_stamina);
			return true;
		}
		if (DisableHungerAndMentalHealthInfo)
		{
			return false;
		}
		return SyncHungerAndMentalHealth();
	}
}
