using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

public static class StoryFlags
{
	public class FlagEntry
	{
		public readonly string Value;

		public readonly string ConstantName;

		[CanBeNull]
		public readonly string Comment;

		public FlagEntry(string value, string constantName, [CanBeNull] string comment = null)
		{
			Value = value;
			ConstantName = constantName;
			Comment = comment;
		}

		public override string ToString()
		{
			if (Comment != null)
			{
				return "Flag \"" + Value + "\", StoryFlags." + ConstantName + ", Comment = \"" + Comment + "\"";
			}
			return "Flag \"" + Value + "\", StoryFlags." + ConstantName;
		}
	}

	public const string AfterPrologue = "afterprologue";

	public const string EventDay2 = "DAY 2, after 12:32";

	public const string EventDay3 = "DAY 3, 16:29";

	public const string EventDay4 = "DAY 4, 16:29";

	public const string EventDay5 = "DAY 5, 22:15";

	public const string EventDay6 = "DAY 6, 22:12";

	public const string EventDay7 = "DAY 7, 22:17";

	[StoryFlagInfo("Paying rent")]
	public const string LandlordRentTimer = "LandlordEvent";

	public const string EventDay9 = "DAY 9, 17:12";

	public const string EventDay10 = "DAY 10, 22:17";

	public const string EventDay11 = "DAY 11, 16:56";

	public const string EventDay13_1 = "DAY 13, 14:52";

	public const string EventDay13_2 = "DAY 13, 22:17";

	public const string ThrownOutIllegalTrash = "illegalTrash";

	public const string Trash1 = "Trash1";

	public const string ChurchNunBackBotDialogue = "church-nun_back_bot_dialogue";

	public const string ChurchBigDaddyKnows = "church-Big_Daddy_Knows";

	public const string ChurchEnd_GoThereGood = "church-endJustGoThereGood";

	public const string DinnerWithBigDaddy1Invited = "DinnerWithBigDaddy1Invited";

	public const string FriendEmailSent = "FriendEmail";

	public const string BoBroganEmailSent = "BoBrogan";

	public const string FriendEmail2Sent = "FriendEmail2";

	public const string DoctorEmailSent = "DoctorEmail";

	public const string Cheated = "c";

	[StoryFlagInfo("Are stonks unlocked?")]
	public const string StonksUnlocked = "stonks";

	[StoryFlagInfo("Did bot say she loves MC?")]
	public const string DidBotSaySheLovesMC = "love";

	public const string BeforeBotSaysSheLovesMC = "prelove";

	public const string EverPlayedCardsWithBot = "cards";

	public const string EverPlayedWordChainWithBot = "wordchain";

	public const string EverPlayedChessWithBot = "chess";

	public const string BotsStatisticsUnlocked = "statistics";

	public const string FirstClothes = "firstClothes";

	public const string WeebMusicNeighbour = "WeebMusic";

	public const string DadJoke = "dadJoke";

	public const string PetsConversationWithBot = "pets1";

	public const string TooMuchCumWarning = "TooMuchCum1";

	public const string FoodUnlocked = "food";

	public const string RichPeopleShopAllowed = "richpeopleshopallowed";

	public const string LabourOfficeUnlocked = "labouroffice";

	public const string EndlessMode = "endlessMode";

	public const string DidntStreamAfterPrologue = "DidntStreamAfterPrologue";

	[StoryFlagInfo("Has ever streamed")]
	public const string StreamStarted = "StreamStarted";

	public const string AfterFirstStreamFuck = "AfterFirstFuck";

	[StoryFlagInfo("Are stonks unlocked?")]
	public const string GotFlaccid = "firsttimeflacid";

	[StoryFlagInfo("Has ever started the slow mode?")]
	public const string SlowMode = "firstslowmode";

	public const string SlowModeUnlockedEmailFlag = "unlockedslowmodeemail";

	public const string SlowModeUnlockedFlag = "unlockedslowmode";

	public const string BitchEventStarted = "BitchEventStarted";

	public const string BitchEventHappened = "BitchEventHappened";

	public const string SchizoEventHappened = "schizo";

	public const string Headache3 = "Headache3";

	public const string MentalHealthDeathDoor = "MentalHealthDeathDoor";

	public const string BlaubeimEventStarted = "BlaubeimEventStarted";

	public const string Blaubeim1 = "Blaubeim1";

	public const string BlaubeimMedicineUnlocked = "BlaubeimMedicine";

	public const string HalloweenBot1 = "HalloweenBot1";

	public const string HalloweenStarted = "HalloweenStarted";

	public const string HalloweenTOT1 = "HalloweenTOT1";

	public const string HalloweenTOT2 = "HalloweenTOT2";

	public const string HalloweenTOT3 = "HalloweenTOT3";

	public const string HalloweenTOT4 = "HalloweenTOT4";

	public const string ClothierHalloweenWithBot = "ClothierHalloweenWithBot";

	public const string ClothierHalloweenWithBot2 = "ClothierHalloweenWithBot2";

	public const string ClothierHalloweenAlone = "ClothierHalloweenAlone";

	[StoryFlagInfo("Has the event where mc asks bot to sleep with him happened?")]
	public const string SleepingWithBotUnlocked = "SleepingWithBotUnlocked";

	[StoryFlagInfo("Has the event where bot first takes full lead happened?")]
	public const string BotFullLeadFirstEvent = "FirstBotRaepMc";

	[StoryFlagInfo("Did bot full lead event happen?")]
	public const string GotBotFullLead = "botRaep";

	[StoryFlagInfo("Did bot make MC's coffee when he left?")]
	public const string CoffeeEventHappened = "coffee";

	public const string DiarrheaEventPlanned = "diarrhea";

	public const string DiarrheaEventHappened = "diarrheaHappened";

	public const string FirstLandlordPayment = "FirstPayment";

	public const string Housewife1 = "housewife1";

	public const string Housewife2 = "housewife2";

	[StoryFlagInfo("You get the stove here(UNLOCKED COOKING)")]
	public const string Housewife3 = "housewife3";

	[StoryFlagInfo("Jun fixes the stove here!")]
	public const string Housewife4 = "housewife4";

	public const string Housewife5 = "housewife5";

	public const string Housewife6 = "housewife6";

	public const string Housewife7 = "housewife7";

	public const string Housewife8 = "housewife8";

	public const string Housewife9 = "housewife9";

	public const string Housewife10 = "housewife10";

	public const string Housewife11 = "housewife11";

	public const string HousewifeBeans = "housewife_beans";

	public const string HousewifeSausageOnions = "housewife_sausageonions";

	public const string HousewifeTea = "housewife_tea";

	public const string HousewifeBread = "housewife_bread";

	public const string HousewifeHotChocolate = "housewife_hotchocolate";

	public const string HousewifeOnionRings = "housewife_onionrings";

	public const string HousewifeCarrotJuice = "housewife_carrotjuice";

	public const string HousewifeSteakPotatoes = "housewife_steakpotatoes";

	public const string OutOfStaminaHome = "outofstamina";

	[StoryFlagInfo("Set in Stage3UniqueLove6")]
	public const string Compliments = "compliments";

	public const string OrderedFirstDelivery = "firstdelivery";

	public const string LadypartsStoreUnlocked = "ladyparts";

	public const string CasinoUnlocked = "casino";

	public const string CasinoBundleOpened = "cazinobundleopened";

	public const string CocktractsUnlockPlanned = "cocktractsunlockedplanned";

	public const string CocktractsUnlocked = "cocktractsunlocked";

	public const string StatusAppOpenedFirstTime = "console1";

	public const string InteractConsole1 = "interact1";

	public const string InteractConsole2 = "interact2";

	public const string InteractConsole3 = "interact3";

	public const string InteractConsole4 = "interact4";

	public const string InteractConsole5 = "interact5";

	[StoryFlagInfo("Not in console")]
	public const string InteractConsole5BotTalk = "interact5botTalk";

	[StoryFlagInfo("Not in console")]
	public const string InteractConsole6 = "interact6";

	public const string InteractConsoleRandom1 = "interactr1";

	public const string InteractConsoleRandom2 = "interactr2";

	public const string InteractConsoleRandom3 = "interactr3";

	public const string InteractConsoleRandom4 = "interactr4";

	public const string InteractConsoleRandom5 = "interactr5";

	public const string InteractConsoleRandom6 = "interactr6";

	public const string InteractConsoleRandom7 = "interactr7";

	public const string InteractConsoleRandom8 = "interactr8";

	public const string InteractConsoleRandom9 = "interactr9";

	public const string JournalFileOpened = "secret_files";

	[StoryFlagInfo("Landlord's bag broke and you can steal")]
	public const string LandlordGroceries = "groceries";

	public const string LandlordWalkWithLandlord = "walkwithlandlord";

	public const string LandlordConspiracy0 = "LandlordConspiracy0";

	public const string LandlordConspiracy1 = "LandlordConspiracy1";

	[StoryFlagInfo("Landlord learned bot exists and raised your rent!")]
	public const string LandlordConspiracy2 = "LandlordConspiracy2";

	[StoryFlagInfo("Landlord learned bot is your girlfriend!")]
	public const string LandlordConspiracy3 = "LandlordConspiracy3";

	public const string TalkedWithBotAboutLandlord = "TalkAboutLandlord1";

	public const string LandlordDaughter1 = "LandlordDaughter1";

	[StoryFlagInfo("Met landlord on the staircase with the bot")]
	public const string LandlordStaircase = "LandlordStaircase";

	public const string SearchAngryLandlord = "angry_landlord";

	public const string GoodSoulLandlord = "good_soul";

	public const string HelpedLandlordWithPaperBag = "helpedlandlord";

	public const string SearchLandlordEventHappened = "SearchLandlord";

	public const string LandlordGivenLastChance = "LastChance";

	public const string FirstPaymentAfterBotDiscovery = "FirstPaymentAfterBotDiscovery";

	public const string FirstPaymentAfterBathroom = "FirstPaymentAfterBathroom";

	public const string FirstPaymentAfterRiots = "FirstPaymentAfterRiots";

	public const string LandlordDaughter2 = "LandlordDaughter2";

	public const string FirstPaymentGoodSoul = "FirstPaymentGoodSoul";

	public const string FirstPaymentRichPeopleShop = "FirstPaymentRichPeopleShop";

	public const string FirstPaymentFishing = "FirstPaymentFishing";

	public const string FirstPaymentEvening = "FirstPaymentEvening";

	public const string FirstPaymentDayLate = "FirstPaymentDayLate";

	public const string SecondPaymentDayLate = "SecondPaymentDayLate";

	public const string GenericPaymentDayLate = "GenericPaymentDayLate";

	public const string SecondPaymentDayLateBotTalk_EmployedLawyer = "SecondPaymentDayLateBotTalk_EmployedLawyer";

	public const string SecondPaymentDayLateBotTalk_DidntEmployLawyer = "SecondPaymentDayLateBotTalk_NoLawyer";

	[StoryFlagInfo("Lawyer tricks landlord and lowers your rent!")]
	public const string ShadyLawyerTrickedLandlord = "ShadyLawyerTrickedLandlord";

	public const string FirstPaymentAfterShadyLawyer = "FirstPaymentAfterShadyLawyer";

	public const string Landlord_Cockblock1 = "Landlord_Cockblock1";

	public const string Landlord_Cockblock2 = "Landlord_Cockblock2";

	public const string Landlord_CockblockGeneric = "Landlord_CockblockGeneric";

	public const string Landlord_Cockblock3 = "Landlord_Cockblock3";

	public const string Landlord_Cockblock3_5 = "Landlord_Cockblock3.5";

	public const string Landlord_Cockblock_Recorded = "Landlord_Cockblock_Recorded";

	public const string Landlord_Cockblock4 = "Landlord_Cockblock4";

	public const string Landlord_Cockblock4_Help = "Landlord_Cockblock4_Help";

	public const string Landlord_Cockblock5 = "Landlord_Cockblock5";

	public const string Landlord_Cockblock6 = "Landlord_Cockblock6";

	public const string FirstVisitAtRichPeopleShop = "richpeopleshop_firstvisit";

	public const string HasRichPeopleShopMembership = "richpeopleshop_membership";

	[StoryFlagInfo("Set every time you visit Annalie")]
	public const string AnnalieVisitTimer = "clothiervisit";

	public const string AnnalieCustomClothingUnlocked = "CustomClothing";

	[StoryFlagInfo("Annalie invited you to go to a cafe together")]
	public const string AnnalieCafe1 = "Annalie_cafe1";

	public const string Clothier1 = "clothier1";

	public const string Clothier2 = "clothier2";

	public const string Clothier3 = "clothier3";

	public const string Clothier4 = "clothier4";

	public const string AnnalieBirthdayTalk = "birthday";

	public const string AnnalieMentalHealth = "AnnalieMentalHealth";

	public const string AnnalieCoat = "coat";

	public const string AnnalieGoingOutsideSide = "GoingOutsideSide";

	public const string AnnalieQuestion = "Question";

	public const string AnnalieBestClient = "BestClient";

	public const string AnnalieHairstyleComment = "AnnalieHairstyleComment";

	public const string IslandPrologue1 = "Island_Prologue1";

	public const string IslandPrologue2 = "Island_Prologue2";

	public const string IslandPrologue3 = "Island_Prologue3";

	public const string IslandPrologue4 = "Island_Prologue4";

	[StoryFlagInfo("Said yes to at least one of the events asking if you want to leave. Required for AnnalieESL1")]
	public const string WillingToLeave = "willing_to_leave";

	public const string AnnalieEnding1 = "Annalie_ending1";

	public const string AnnalieEnding2 = "Annalie_ending2";

	public const string AnnalieEnding3 = "Annalie_ending3";

	public const string AnnalieEnding4 = "Annalie_ending4";

	public const string AnnalieEnding5 = "Annalie_ending5";

	public const string AnnalieEnding6 = "Annalie_ending6";

	public const string AnnalieEnding7 = "Annalie_ending7";

	public const string AnnalieEnding8 = "Annalie_ending8";

	public const string AnnalieEnding8Shanice = "runway_shanice";

	public const string AnnalieEnding8Fisher = "runway_fisher";

	public const string AnnalieEnding8Landlord = "runway_landlord";

	public const string AnnalieEnding8LBotNaked = "annalie_saw_bot_naked";

	public const string AnnalieEnding9 = "Annalie_ending9";

	public const string LandlordESL1 = "Landlord_ending1";

	public const string LandlordESL1_Accepted = "Landlord_ending1_accepted";

	public const string LandlordESL2 = "Landlord_ending2";

	public const string LandlordESL2_GaveMoney = "Landlord_ending2_gamemoney";

	public const string LandlordESL3 = "Landlord_ending3";

	public const string LandlordESL3_StrongarmedHobos = "Landlord_ending3_strongarmedHobos";

	public const string LandlordESL4 = "Landlord_ending4";

	public const string LandlordESL5 = "Landlord_ending5";

	public const string LandlordESL6 = "Landlord_ending6";

	public const string LandlordESL7 = "Landlord_ending7";

	public const string BritanniaIsland = "Britannia_island";

	public const string BritanniaStoryline1 = "BritanniaSL1";

	public const string GoingOutside1 = "GoingOutside1";

	public const string GoingOutside2 = "GoingOutside2";

	[StoryFlagInfo("Bot meets Annalie here!")]
	public const string GoingOutside3 = "GoingOutside3";

	public const string AfterRiotsWithBotFirstTime = "afterRiots_withBot_firstTime";

	[StoryFlagInfo("Set whenever you walk with bot outside")]
	public const string WalkWithBot = "WalkWithBot";

	public const string AirPollutionMaskEvent = "AirPollutionMask";

	public const string BoughtMaskDuringAirPollutionEvent = "mask";

	public const string DatesUnlocked = "date";

	public const string DateTimer = "lastdate";

	public const string ArtGallery1 = "artgallery1";

	public const string ArtGallery2 = "artgallery2";

	public const string Restaurant1 = "restaurant1";

	public const string Restaurant2 = "restaurant2";

	public const string KinoTalk = "kinoTalk";

	public const string Kino1 = "kino1";

	public const string KinoTalk2 = "kinoTalk2";

	public const string Kino2 = "kino2";

	public const string KaraokeTalk = "KaraokeTalk";

	public const string Karaoke1 = "Karaoke1";

	public const string Karaoke2Nun = "Karaoke2";

	public const string IceSkating1 = "ice_skating1";

	public const string IceSkating1Talk = "ice_skating1talk";

	public const string IceSkating2 = "ice_skating2";

	public const string Planetarium1 = "planetarium1";

	public const string JunDateAmusementPark1 = "amusement_park1";

	public const string AmusementPark1_ShootingFail = "shooting_failure";

	public const string AmusementPark1_BasketballFail = "basketball_failure";

	public const string AmusementPark2 = "amusement_park2";

	public const string JunDateGokart1 = "gokart1";

	public const string JunDateGokart1_RacingQueen = "racing_queen";

	public const string AnnalieCafe2 = "Annalie_cafe2";

	public const string BowlingWithMelissa1 = "bowling1";

	public const string HangedOutWithMelissa = "hangingOutWithMelissa";

	public const string HangedOutWithAnnalie = "hangingOutWithAnnalie";

	public const string HangedOutWithShanice = "hangingOutWithShanice";

	public const string JunDateIndustrialPark = "Jun_Inv_Industrial_Park";

	public const string JunDateStarGazing = "Jun_inv_star_gazing";

	public const string JunDateAquarium = "aquarium_date";

	public const string ExposureTimer = "exposure";

	public const string ExposureNight = "exposure_night";

	public const string ExposureDay = "exposure_day";

	public const string ExposureLittleGirl = "exposure_little_girl";

	public const string SearchWarningEventHappened = "Search";

	public const string GotACallFromTheFeds = "FedPhoneCall";

	public const string ChurchUnlocked = "church";

	public const string ChurchNextWorstEnd = "church-nextTheWorstEnd";

	public const string ChurchNextRevival = "church-nextRevival";

	public const string ChurchNextDrugiePojscieBA = "church-nextDrugiePojscieBA";

	public const string ChurchNextNextDrugiePojscieABA = "church-nextDrugiePojscieABA";

	public const string ChurchNextSpotkanieBC = "church-nextSpotkanieBC";

	public const string ChurchNextSpotkanieBB = "church-nextDrugiePojscieBB";

	public const string ChurchNextSpotkanieB = "church-nextSpotkanieB";

	public const string ChurchNextSpotykaBotaCrash = "church-nextSpotykaBotaCrash";

	public const string ChurchShaniceCum = "church-shaniceFoundCumInBot";

	public const string ChurchShaniceKnowsStreaming = "church-shaniceFoundCumInBot";

	public const string ChurchEnd_Prefix = "church-end";

	public const string ChurchEnd_TheWorstEnd = "church-endTheWorstEnd";

	public const string ChurchEnd_Spotkanie2B = "church-endSpotkanie2B";

	public const string ChurchEnd_IDontKnow = "church-endIdontknow";

	public const string ChurchEnd_JustGoThereBAd = "church-endJustGoThereBad";

	public const string ChurchAfterStory = "church-whatHappenedAfter";

	public const string ChurchNunBack1 = "church-nun_back1";

	public const string ChurchNunBack2 = "church-nun_back2";

	public const string ChurchNunBack3 = "church-nun_back3";

	public const string ChurchNunBack4 = "church-nun_back4";

	public const string ChurchNunBack5 = "church-nun_back5";

	public const string ChurchNunBack6 = "church-nun_back6";

	public const string ChurchNunBack7 = "church-nun_back7";

	public const string ChurchNunBackGoku = "church-nun_back_gokusmotivation";

	public const string ChurchNunBackBDHStoryline1 = "church-bigdaddystorylinepart1";

	public const string ChurchPriestbotTruth = "church-priestbot_truth";

	public const string ChurchPriestbotLie = "church-priestbot_lie";

	public const string ChurchLoyalToBigDaddy = "church-loyaltobigdaddy";

	public const string ChurchSharedBigDaddyKnowledge = "church-sharedbigdaddyknowledge";

	public const string ChurchNunBackVisitTimer = "church-nun_back_visit_timer";

	[StoryFlagInfo("Walk with bot")]
	public const string ChurchMeetingNunWithTheBot = "church-MeetingNunWithTheBot";

	public const string ChurchBigStoryLinePossible = "church-bigdaddystorylinepossible";

	public const string NunInvestigation1 = "nunInvestigation1";

	[StoryFlagInfo("Gave up on searching for the nun!")]
	public const string NunInvestigation1Coward = "nunInvestigation1Coward";

	public const string NunInvestigation1WentToChurch = "nunInvestigation1WentToChurch";

	public const string NunInvestigation2 = "nunInvestigation2";

	public const string LandlordMoneyEvent = "LandlordMoneyEvent";

	public const string WentOnAWalk = "firstwalk";

	public const string HalloweenTrickOrTreatAlone = "HalloweenTrickOrTreat";

	public const string PriestBotMark1 = "Priestbot_Mark_1";

	public const string Hobo = "hobo";

	[StoryFlagInfo("Hobo got beaten up! Bad end of the hobo storyline")]
	public const string HoboBeaten = "hobo_beaten";

	[StoryFlagInfo("Hobo stole money from you!")]
	public const string HoboFailed = "hobo_failed";

	public const string Hobo1 = "hobo1";

	public const string Hobo1EventHappened = "hobo1eventHappened";

	public const string Hobo2 = "hobo2";

	[StoryFlagInfo("Set if you trash dive and steal a part!")]
	public const string HoboArrested = "hoboarrested";

	public const string Hobo4Hobo = "Melissa4Hobo";

	public const string TrashDiving = "trashdiving";

	public const string LoreRant1 = "lorerant1";

	public const string Psychologist1 = "psychologist1";

	public const string RandomSearch1 = "randomsearch1";

	public const string RandomSearch2 = "randomsearch2";

	public const string RandomSearch3 = "randomsearch3";

	public const string JoinUsEmailSent = "JoinUsEmail";

	public const string JoinUs = "joinus";

	[StoryFlagInfo("Received second joinus email?")]
	public const string JoinUs2EmailSent = "joinusemail2";

	public const string JoinUs2 = "JoinUs2";

	public const string JoinUs2EmailOpened = "joinusemail2opened";

	public const string JoinUs2EmailRejected = "joinusemail2rejected";

	public const string JoinUsBigDaddy = "joinusBigDaddy";

	[StoryFlagInfo("Learned about the package he left during joinus2 event because of Police")]
	public const string JoinUsRegret = "JoinUsRegret";

	public const string JoinUs3Email = "joinusemail3";

	public const string JoinUs3 = "joinus3";

	public const string JoinUs3Richard = "joinus3Richard";

	public const string JoinUsLastLogin = "joinusebloglogin";

	public const string JoinUs4Email = "joinusemail4";

	public const string JoinUs4 = "joinus4";

	public const string JoinUs4Richard = "joinus4Richard";

	public const string JoinUs5Email = "joinuseemail5";

	public const string JoinUs5 = "joinus5";

	public const string JoinUs5Richard = "joinus5richard";

	public const string JoinUs6Email = "joinuseemail6";

	public const string JoinUs6 = "joinus6";

	public const string JoinUs6Richard = "joinus6richard";

	public const string JoinUs6Mission = "joinus6mission";

	public const string JoinUs6MissionBailedOut = "joinus6mission_bailoutend";

	public const string JoinUs6MissionBailedOutAfter = "joinus6mission_bailoutendafter";

	public const string JoinUs6MissionShanicePhonecall = "joinus6mission_shanicephonecall";

	public const string JoinUs6MissionShanicePhonecallAfter = "joinus6mission_shanicephonecallafter";

	public const string JoinUs6MissionBigDaddyChosen = "joinus6mission_bigdaddychosen";

	public const string JoinUs6MissionRichardChosen = "joinus6mission_richardchosen";

	public const string JoinUs7BigDaddy1Email = "joinus7_bigdaddy1email";

	public const string JoinUs7BigDaddy1 = "joinus7_bigdaddy1";

	public const string JoinUs7BigDaddy2 = "joinus7_bigdaddy2";

	public const string JoinUs7BigDaddy3 = "joinus7_bigdaddy3";

	public const string JoinUs7BigDaddy4Email = "joinus7_bigdaddy4email";

	public const string JoinUs7BigDaddy4 = "joinus7_bigdaddy4";

	public const string JoinUs7BigDaddy5 = "joinus7_bigdaddy5";

	public const string JoinUs7BigDaddy6 = "joinus7_bigdaddy6";

	public const string JoinUs7BigDaddy7 = "joinus7_bigdaddy7";

	public const string JoinUs7BigDaddy8 = "joinus7_bigdaddy8";

	public const string JoinUs7BigDaddy8_GoalSexbot = "joinus7_bigdaddy8_goalsexbot";

	public const string JoinUs7BigDaddy8_GoalHappiness = "joinus7_bigdaddy8_goalhappiness";

	public const string JoinUs7BigDaddy8_GoalRevolution = "joinus7_bigdaddy8_goalrevolution";

	public const string JoinUs7BigDaddy8_SideWithAdmin = "joinus7_bigdaddy8_sidewithadmin";

	public const string JoinUs7BigDaddy9Email = "joinus7_bigdaddy9email";

	public const string JoinUs7BigDaddy9 = "joinus7_bigdaddy9";

	public const string JoinUs7Richard1Email = "joinus7_richard1email";

	public const string JoinUs7Richard1 = "joinus7_richard1";

	public const string JoinUs7Richard2 = "joinus7_richard2";

	public const string JoinUs7Richard2BotDAK = "joinus7_richard2_BotDAK";

	public const string JoinUs7Richard3 = "joinus7_richard3";

	public const string JoinUs7Richard3BotNaked = "joinus7_richard3_BotNaked";

	public const string JoinUs7Richard4Email = "joinus7_richard4email";

	public const string JoinUs7Richard4 = "joinus7_richard4";

	public const string JoinUs7Richard5 = "joinus7_richard5";

	public const string ShopAfterRiotsFirstTime_Melissa = "Melissa";

	public const string Melissa1 = "Melissa1";

	public const string Melissa2 = "Melissa2";

	public const string Melissa3 = "Melissa3";

	public const string Melissa4 = "Melissa4";

	public const string Melissa4bot = "Melissa4bot";

	public const string Melissa4Church = "Melissa4church";

	public const string Melissa4Landlord = "Melissa4Landlord";

	public const string Melissa4LandlordAsked = "Melissa4LandlordAsked";

	public const string Melissa4Annalie = "Melissa4Annalie";

	public const string Melissa4rejection = "Melissa4rejection";

	public const string Melissa5AfterRejection = "Melissa5AfterRejection";

	public const string Melissa5Normal = "Melissa5Normal";

	public const string Melissa5_Prefix = "Melissa5";

	[StoryFlagInfo("Conversation with the bot after Melissa 5")]
	public const string Melissa5Bot = "Melissa5bot";

	public const string Melissa6 = "Melissa6";

	public const string Melissa7 = "Melissa7";

	public const string Melissa8 = "Melissa8";

	[StoryFlagInfo("Bowling date with Annalie and Melissa!")]
	public const string Melissa9 = "Melissa9";

	public const string Melissa10 = "Melissa10";

	[StoryFlagInfo("Second bowling date with Annalie and Melissa!")]
	public const string Melissa11 = "Melissa11";

	[StoryFlagInfo("Karaoke date with Annalie and Melissa!")]
	public const string Melissa12 = "Melissa12";

	[StoryFlagInfo("Didn't do Karaoke2 before kareoke date with Melissa")]
	public const string Melissa12_karaoke_idol = "karaoke_idol";

	public const string Melissa13 = "Melissa13";

	public const string Melissa14 = "Melissa14";

	public const string Melissa14_StillFriends = "Melissa14_StillFriends";

	[StoryFlagInfo("Melissa storyline ending 1")]
	public const string Melissa14_DestroyEnd = "Melissa14_DestroyEnd";

	[StoryFlagInfo("Melissa storyline ending 2")]
	public const string Melissa14_LeaveEnd = "Melissa14_LeaveEnd";

	public const string Melissa15 = "Melissa15";

	[StoryFlagInfo("Cafe with Annalie and Melissa!")]
	public const string Melissa16 = "Melissa16";

	[StoryFlagInfo("Gokarts with Annalie and Melissa!")]
	public const string Melissa17 = "Melissa17";

	[StoryFlagInfo("Alone visit Annalie after gokarts!")]
	public const string Melissa18 = "Melissa18";

	[StoryFlagInfo("Third bowling date with Annalie and Melissa!")]
	public const string Melissa19 = "Melissa19";

	public const string Melissa20 = "Melissa20";

	[StoryFlagInfo("Cafe with Annalie and Melissa 2!")]
	public const string Melissa21 = "Melissa21";

	[StoryFlagInfo("Optional date with Melissa and Bot. Happens only with \"karaoke_idol\" flag!")]
	public const string Melissa21_5 = "Melissa21.5";

	public const string Melissa22 = "Melissa22";

	public const string Melissa22_Melissa_dead = "Melissa_dead";

	public const string Melissa22_Melissa_leaves = "Melissa_leaves";

	[StoryFlagInfo("Talk with bot about Melissa after she leaves or dies.")]
	public const string Melissa23 = "Melissa23";

	[StoryFlagInfo("Talk with Annalie about Melissa after she leaves or dies.")]
	public const string Melissa24 = "Melissa24";

	[StoryFlagInfo("Melissa storyline ending 3 (Mealissa_dead or Melissa_leaves)")]
	public const string Melissa25 = "Melissa25";

	[StoryFlagInfo("Inputted correct name while throwing out an illegal item(Removed once got to part2)")]
	public const string BigDaddysAdvice = "bigDaddysAdvice";

	[StoryFlagInfo("Ordered the lingerie catalogue from BDH's shop(Removed once finished)")]
	public const string BigDaddysAdvice2 = "bigDaddysAdviceBeforePart2";

	[StoryFlagInfo("Opened the lingerie catalogue from BDH's shop(Removed once finished)")]
	public const string BigDaddysAdvice3 = "bigDaddysAdvicePart2";

	public const string BigDaddysAdviceFinished = "bigDaddysAdviceFinished";

	public const string BigDaddyHurtStoryline1 = "bigdaddystoryline1";

	public const string BigDaddyHurtStoryline2 = "bigdaddystoryline2";

	public const string BigDaddyHurtStoryline3 = "bigdaddystoryline3";

	public const string BigDaddyHurtStoryline4 = "bigdaddystoryline4";

	public const string DinnerWithBigDaddy = "dinnerwithbigdaddy1";

	public const string Melissa4BigDaddy = "Melissa4BigDaddy";

	public const string BigDaddyHurtInvitation = "bigdaddyinvitation";

	public const string BigDaddyHurtVisitTimer = "Bigdaddystoryline_timer";

	[StoryFlagInfo("First encounter with the fisher")]
	public const string Fishing1 = "fishing1";

	public const string Fishing2 = "fishing2";

	[StoryFlagInfo("This unlocks all the locations!")]
	public const string Fishing3 = "fishing3";

	public const string Fishing4 = "fishing4";

	public const string Fishing_Bluegill_ManyTalk = "Fishing_Bluegill_ManyTalk";

	public const string Fishing_LargemouthBass_BigTalk = "Fishing_LargemouthBass_BigTalk";

	public const string Fishing_LargemouthBass_ManyTalk = "Fishing_LargemouthBass_ManyTalk";

	public const string Fishing_NorthernPike_BigTalk = "Fishing_NorthernPike_BigTalk";

	public const string Fishing_YellowPerch_BigTalk = "Fishing_YellowPerch_BigTalk";

	public const string Fishing_Catfish_BigTalk = "Fishing_Catfish_BigTalk";

	public const string Fishing_Catfish_ManyTalk = "Fishing_Catish_ManyTalk";

	public const string Fishing_Walleye_BigTalk = "Fishing_Walleye_BigTalk";

	public const string Fishing_Carp_ManyTalk = "Fishing_Carp_ManyTalk";

	public const string Fishing_Tilapia_BigTalk = "Fishing_Tilapia_BigTalk";

	public const string Fishing_RainbowTrout_ManyTalk = "Fishing_RainbowTrout_ManyTalk";

	public const string Fishing_SmallmouthBass_ManyTalk = "Fishing_SmallmouthBass_ManyTalk";

	public const string Fishing_BrownTrout_BigTalk = "Fishing_BrownTrout_BigTalk";

	public const string Fishing_Salmon_BigTalk = "Fishing_Salmon_BigTalk";

	public const string Fishing_RedSnapper_BigTalk = "Fishing_RedSnapper_BigTalk";

	public const string Fishing_BlackSeaBass_BigTalk = "Fishing_BlackSeaBass_BigTalk";

	public const string Fishing_Halibut_BigTalk = "Fishing_Halibut_BigTalk";

	public const string Fishing_GoldenSalmon_BigTalk = "Fishing_GoldenSalmon_BigTalk";

	public const string Fishing_Mackerel_ManyTalk = "Fishing_Mackerel_ManyTalk";

	public const string Fishing_Flounder_AnyTalk = "Fishing_Flounder_AnyTalk";

	public const string Fishing_Flounder_BigTalk = "Fishing_Flounder_BigTalk";

	public const string AfterSexRag = "AfterSex_Rag";

	public const string AfterSexShowerAlone = "AfterSex_ShowerAlone";

	public const string AfterSexShowerTogether = "AfterSex_ShowerTogether";

	public const string AfterSexLeaveCum = "AfterSex_LeaveCum";

	public const string ShowerAnon = "ShowerAnon";

	public const string ShowerBot = "ShowerBot";

	public const string ShowerTogetherComplete = "ShowerTogetherComplete";

	public const string Bathroom1 = "Bathroom1";

	public const string Bathroom1_Haggled = "Bathroom1_haggle";

	public const string Bathroom1_100k = "Bathroom1_100k";

	public const string Bathroom2 = "Bathroom2";

	public const string Bathroom2_NotYet = "Bathroom2_notyet";

	public const string Bathroom2_Bought = "Bathroom2_bought";

	[StoryFlagInfo("Landlord has installed the door to the bathroom!")]
	public const string Bathroom3 = "Bathroom3";

	[StoryFlagInfo("Started the first shower with the bot")]
	public const string Bathroom4 = "Bathroom4";

	public const string Riots = "riots_start_event";

	public const string ChurchRiots1 = "church_riots1";

	public const string ChurchHoboBots = "church_hobo_bots";

	public const string ChurchHoboBots2 = "church_hobo_bots2";

	public const string ChurchPriestBotBack = "church_priestbot_back_riots";

	public const string RiotsBeatenUp = "riots_beaten_up";

	public const string GroceryRiotsFirstTime = "Grocery_Riots_FirstTime";

	public const string GroceryRiotsHungry = "Grocery_Riots_Hungry";

	public const string GroceryRiotsHungry2 = "Grocery_Riots_Hungry2";

	public const string DispenserRiotsFirstTime = "Dispenser_Riots_FirstTime";

	public const string DispenserRiotsHobo = "Dispenser_Riots_Hobo";

	public const string DispenserEmpty24Hours = "dispenserempty24hours";

	public const string PharmacyRiotsFirstTime = "Pharmacy_Riots_FirstTime";

	public const string PharmacyRiotsRandom = "Pharmacy_Riots_Random";

	public const string LabourOfficeRiotsFirstTime = "LabourOffice_Riots_FirstTime";

	public const string LabourOfficeRiotsLandlord = "LabourOffice_Riots_Landlord";

	public const string AnnalieRiots1 = "Annalie_riots1";

	public const string AnnalieRiots2 = "Annalie_riots2";

	public const string AnnalieRiots_GaveMoney = "Annalie_gave_money";

	public const string BigDaddyRiots = "Big_Daddy_Riots";

	public const string RiotsBotBringsFoodEvent = "RiotsBotBringsFood";

	public const string RiotsFirstWalk = "riots_firstWalk";

	public const string RiotsEndNotice = "RiotsEndNotice";

	public const string RiotsEndingSoonNotice = "RiotsEndingSoonNotice";

	public const string DispenserAfterRiots = "Dispenser_AfterRiots";

	public const string PharmacyAfterRiots = "Pharmacy_AfterRiots";

	public const string LabourOfficeAfterRiots = "LabourOffice_AfterRiots";

	public const string AnnalieAfterRiots_Helped = "Annalie_afterriots_helped";

	public const string AnnalieAfterRiots_NeverWent = "Annalie_afterriots_neverWent";

	public const string AnnalieAfterRiotsWithBot_WentAlone = "Annalie_afterriotsWithBot_wentAlone";

	public const string AnnalieAfterRiotsWithBot_Helped = "Annalie_afterriotsWithBot_helped";

	public const string AnnalieAfterRiotsWithBot_NeverWent = "Annalie_afterriotsWithBot_neverWent";

	public const string ChurchAfterRiots_NeverWent = "church-afterriots_neverwent";

	public const string ChurchAfterRiots_SavedPriestBot = "church-afterriots_savedpriestbot";

	public const string ChurchAfterRiots_DidntSavePriestBot = "church-afterriots_didntsavepriestbot";

	public const string AnnalieAfterRiots_ReturnedMoney = "Annalie_riots_returnedMoney";

	public const string EveryUsedLibidoDestroyer = "libido_destroyer";

	public const string EveryUsedLibidoAmplifier = "libido_amplifier";

	public const string HellWebsiteVisited = "hell";

	public const string SoulTakenFlag = "soultaken";

	public const string FishJoint = "Fish_joint";

	public const string FishJoint1 = "Fish_joint1";

	public const string FishJoint1_Win = "Fish_joint1_win";

	public const string FishJoint1_Loss = "Fish_joint1_loss";

	public const string FishJoint2 = "Fish_joint2";

	public const string FishJoint2_Win = "Fish_joint2_win";

	public const string FishJoint2_Loss = "Fish_joint2_loss";

	public const string FishJoint2_RepeatHealthAdvice = "health_advice";

	public const string FishJoint2_5_Priestbot = "Fish_joint2.5_Priestbot";

	public const string FishJoint2_5_Priestbot_Win = "Fish_joint2.5_Priestbot_win";

	public const string FishJoint2_5_Priestbot_Loss = "Fish_joint2.5_Priestbot_loss";

	public const string FishJoint2_5_Shanice = "Fish_joint2.5_Shanice";

	public const string FishJoint3 = "Fish_joint3";

	public const string FishJoint3_RepeatText = "Fish_joint3_repeat";

	public const string FishJoint3_Win = "Fish_joint3_win";

	public const string FishJoint3_Loss = "Fish_joint3_loss";

	public const string FishJoint4 = "Fish_joint4";

	public const string FishJoint4_Win = "Fish_joint4_win";

	public const string FishJoint4_Loss = "Fish_joint4_loss";

	public const string FishJoint5 = "Fish_joint5";

	public const string FishJoint6Walk = "Fish_joint6_walk";

	public const string FishJointDream = "Fish_joint_dream";

	public const string FishJointEnding = "Fish_joint_ending";

	public const string FishJointEndingMelissaSaved = "Fish_joint_ending_melissa_saved";

	public const string FishJoint7Talk = "Fish_joint7_talk";

	public const string FishingDate1 = "Fishing_date1";

	public const string FishingDate2 = "Fishing_date2";

	[CanBeNull]
	private static List<FlagEntry> _cachedKnownFlags;

	public static IEnumerable<string> GetAllFlags()
	{
		return from x in typeof(StoryFlags).GetFields(BindingFlags.Static | BindingFlags.Public)
			where !x.Name.EndsWith("Prefix")
			select (string)x.GetValue(null);
	}

	public static IReadOnlyList<FlagEntry> GetAllKnownFlags()
	{
		if (_cachedKnownFlags != null)
		{
			return _cachedKnownFlags;
		}
		IEnumerable<FieldInfo> enumerable = from f in typeof(StoryFlags).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
			where f.FieldType == typeof(string) && f.IsLiteral
			select f;
		List<FlagEntry> list = new List<FlagEntry>();
		foreach (FieldInfo item in enumerable)
		{
			string value = (string)item.GetRawConstantValue();
			string comment = item.GetCustomAttribute<StoryFlagInfoAttribute>()?.Comment;
			list.Add(new FlagEntry(value, item.Name, comment));
		}
		list.Sort((FlagEntry a, FlagEntry b) => string.CompareOrdinal(a.Value, b.Value));
		_cachedKnownFlags = list;
		return _cachedKnownFlags;
	}
}
