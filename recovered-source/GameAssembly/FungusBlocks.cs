public static class FungusBlocks
{
	public class FungusBlockReference
	{
		public readonly string Name;

		public readonly string Label;

		public FungusBlockReference(string name)
		{
			Name = name;
			Label = null;
		}

		public FungusBlockReference(string name, string label)
		{
			Name = name;
			Label = label;
		}

		public override string ToString()
		{
			if (Label != null)
			{
				return "Block:\"" + Name + "\" Label:\"" + Label + "\"";
			}
			return "Block:\"" + Name + "\"";
		}
	}

	public class FungusMessageReceiverReference
	{
		public readonly string Message;

		public readonly FungusBlockReference RelatedBlock;

		public FungusMessageReceiverReference(string message, FungusBlockReference relatedBlock)
		{
			Message = message;
			RelatedBlock = relatedBlock;
		}
	}

	public static readonly FungusBlockReference Prologue_Start = new FungusBlockReference("Prologue/Start");

	public static readonly FungusBlockReference Prologue_Anon = new FungusBlockReference("Prologue/Anon");

	public static readonly FungusBlockReference Prologue_DAY_1__21_37 = new FungusBlockReference("Prologue/DAY 1, 21:37");

	public static readonly FungusBlockReference Outside_OutsideStart = new FungusBlockReference("Outside/OutsideStart");

	public static readonly FungusBlockReference Go_Home = new FungusBlockReference("Go Home");

	public static readonly FungusBlockReference exclamationI_GameLoad = new FungusBlockReference("!I/GameLoad");

	public static readonly FungusBlockReference Sex_PrepareFricking = new FungusBlockReference("Sex/PrepareFricking");

	public static readonly FungusBlockReference Sex_Missionary = new FungusBlockReference("Sex/Missionary");

	public static readonly FungusBlockReference Sex_End_Frick = new FungusBlockReference("Sex/End Frick");

	public static readonly FungusBlockReference StartPC = new FungusBlockReference("StartPC");

	public static readonly FungusBlockReference Sleep_Sleep_for_480_minutes = new FungusBlockReference("Sleep/Sleep for 480 minutes");

	public static readonly FungusBlockReference Out_of_stamina = new FungusBlockReference("Out of stamina");

	public static readonly FungusBlockReference Sleep_Go_to_sleep = new FungusBlockReference("Sleep/Go to sleep");

	public static readonly FungusBlockReference DroneDelivery = new FungusBlockReference("DroneDelivery");

	public static readonly FungusBlockReference Sleep_Sleep_for_240_minutes = new FungusBlockReference("Sleep/Sleep for 240 minutes");

	public static readonly FungusBlockReference Sleep_Sleep_for_60_minutes = new FungusBlockReference("Sleep/Sleep for 60 minutes");

	public static readonly FungusBlockReference Stage1_Linear1 = new FungusBlockReference("Stage1/Linear1");

	public static readonly FungusBlockReference Shop_Main = new FungusBlockReference("Shop/Main");

	public static readonly FungusBlockReference Pharmacy_Main = new FungusBlockReference("Pharmacy/Main");

	public static readonly FungusBlockReference Sex_After_First_Stream_Frick = new FungusBlockReference("Sex/After/First Stream Frick");

	public static readonly FungusBlockReference RE_DAY_2__after_12_32 = new FungusBlockReference("RE/DAY 2, after 12:32");

	public static readonly FungusBlockReference RE_DAY_2__after_12_32_Good_End_1 = new FungusBlockReference("RE/DAY 2, after 12:32/Good End 1");

	public static readonly FungusBlockReference RE_DAY_2__after_12_32_Continue_Game = new FungusBlockReference("RE/DAY 2, after 12:32/Continue Game");

	public static readonly FungusBlockReference RE_DAY_3__16_29 = new FungusBlockReference("RE/DAY 3, 16:29");

	public static readonly FungusBlockReference RE_DAY_4__16_29 = new FungusBlockReference("RE/DAY 4, 16:29");

	public static readonly FungusBlockReference ReturnHome_Coffee_event = new FungusBlockReference("ReturnHome/Coffee event");

	public static readonly FungusBlockReference RE_DAY_5__22_15 = new FungusBlockReference("RE/DAY 5, 22:15");

	public static readonly FungusBlockReference RE_DAY_6__22_12 = new FungusBlockReference("RE/DAY 6, 22:12");

	public static readonly FungusBlockReference Debug_DEBUG = new FungusBlockReference("Debug/DEBUG");

	public static readonly FungusBlockReference RE_DAY_7__22_17 = new FungusBlockReference("RE/DAY 7, 22:17");

	public static readonly FungusBlockReference RE_RentPayment = new FungusBlockReference("RE/RentPayment");

	public static readonly FungusBlockReference RE_Rent_Bad_End_1 = new FungusBlockReference("RE/Rent/Bad End 1");

	public static readonly FungusBlockReference RE_Rent_Bad_End_1_Label_StartOfTheEnd = new FungusBlockReference("RE/Rent/Bad End 1", "StartOfTheEnd");

	public static readonly FungusBlockReference RE_Rent_Continue = new FungusBlockReference("RE/Rent/Continue");

	public static readonly FungusBlockReference RE_DAY_9__17_12 = new FungusBlockReference("RE/DAY 9, 17:12");

	public static readonly FungusBlockReference Email_Friend_Email = new FungusBlockReference("Email/Friend Email");

	public static readonly FungusBlockReference RE_DAY_11__16_56 = new FungusBlockReference("RE/DAY 11, 16:56");

	public static readonly FungusBlockReference RE_BitchEvent = new FungusBlockReference("RE/BitchEvent");

	public static readonly FungusBlockReference Email_BitchEmail1 = new FungusBlockReference("Email/BitchEmail1");

	public static readonly FungusBlockReference Email_BitchEmail2 = new FungusBlockReference("Email/BitchEmail2");

	public static readonly FungusBlockReference Email_BitchEmail3 = new FungusBlockReference("Email/BitchEmail3");

	public static readonly FungusBlockReference Email_Bad_End_2 = new FungusBlockReference("Email/Bad End 2");

	public static readonly FungusBlockReference Email_Day_14_part_2 = new FungusBlockReference("Email/Day 14 part 2");

	public static readonly FungusBlockReference Email_Bad_End_3 = new FungusBlockReference("Email/Bad End 3");

	public static readonly FungusBlockReference Email_Continue_Game_3 = new FungusBlockReference("Email/Continue Game 3");

	public static readonly FungusBlockReference Stage1_Linear2 = new FungusBlockReference("Stage1/Linear2");

	public static readonly FungusBlockReference Stage1_Linear3 = new FungusBlockReference("Stage1/Linear3");

	public static readonly FungusBlockReference Stage1_Generic1 = new FungusBlockReference("Stage1/Generic1");

	public static readonly FungusBlockReference Stage1_Generic2 = new FungusBlockReference("Stage1/Generic2");

	public static readonly FungusBlockReference Stage1_Generic3 = new FungusBlockReference("Stage1/Generic3");

	public static readonly FungusBlockReference Stage1_Generic4 = new FungusBlockReference("Stage1/Generic4");

	public static readonly FungusBlockReference Stage1_Generic5 = new FungusBlockReference("Stage1/Generic5");

	public static readonly FungusBlockReference Stage1_Generic6 = new FungusBlockReference("Stage1/Generic6");

	public static readonly FungusBlockReference Stage1_Generic7 = new FungusBlockReference("Stage1/Generic7");

	public static readonly FungusBlockReference Stage1_Generic8 = new FungusBlockReference("Stage1/Generic8");

	public static readonly FungusBlockReference Stage1_Unique1 = new FungusBlockReference("Stage1/Unique1");

	public static readonly FungusBlockReference Stage2_Linear1 = new FungusBlockReference("Stage2/Linear1");

	public static readonly FungusBlockReference Stage2_Generic1 = new FungusBlockReference("Stage2/Generic1");

	public static readonly FungusBlockReference Stage2_Generic2 = new FungusBlockReference("Stage2/Generic2");

	public static readonly FungusBlockReference Stage2_Generic3 = new FungusBlockReference("Stage2/Generic3");

	public static readonly FungusBlockReference Stage2_Unique1 = new FungusBlockReference("Stage2/Unique1");

	public static readonly FungusBlockReference Stage2_Unique2 = new FungusBlockReference("Stage2/Unique2");

	public static readonly FungusBlockReference Stage2_Unique3 = new FungusBlockReference("Stage2/Unique3");

	public static readonly FungusBlockReference Stage2_Unique4 = new FungusBlockReference("Stage2/Unique4");

	public static readonly FungusBlockReference Stage2_Unique5 = new FungusBlockReference("Stage2/Unique5");

	public static readonly FungusBlockReference Stage2_Unique6 = new FungusBlockReference("Stage2/Unique6");

	public static readonly FungusBlockReference Stage3_Linear1 = new FungusBlockReference("Stage3/Linear1");

	public static readonly FungusBlockReference Stage3_GenericDetest1 = new FungusBlockReference("Stage3/GenericDetest1");

	public static readonly FungusBlockReference Stage3_GenericDetest2 = new FungusBlockReference("Stage3/GenericDetest2");

	public static readonly FungusBlockReference Stage3_GenericHate1 = new FungusBlockReference("Stage3/GenericHate1");

	public static readonly FungusBlockReference Stage3_GenericHate2 = new FungusBlockReference("Stage3/GenericHate2");

	public static readonly FungusBlockReference Stage3_GenericDislike2 = new FungusBlockReference("Stage3/GenericDislike2");

	public static readonly FungusBlockReference Stage3_GenericDislike1 = new FungusBlockReference("Stage3/GenericDislike1");

	public static readonly FungusBlockReference Stage3_Generic1 = new FungusBlockReference("Stage3/Generic1");

	public static readonly FungusBlockReference Stage3_Generic2 = new FungusBlockReference("Stage3/Generic2");

	public static readonly FungusBlockReference Stage3_Generic3 = new FungusBlockReference("Stage3/Generic3");

	public static readonly FungusBlockReference Stage3_Generic4 = new FungusBlockReference("Stage3/Generic4");

	public static readonly FungusBlockReference Stage3_Generic5 = new FungusBlockReference("Stage3/Generic5");

	public static readonly FungusBlockReference Stage3_GenericGame1 = new FungusBlockReference("Stage3/GenericGame1");

	public static readonly FungusBlockReference Stage3_GenericGame2 = new FungusBlockReference("Stage3/GenericGame2");

	public static readonly FungusBlockReference Stage3_GenericGame3 = new FungusBlockReference("Stage3/GenericGame3");

	public static readonly FungusBlockReference Stage3_GenericLike1 = new FungusBlockReference("Stage3/GenericLike1");

	public static readonly FungusBlockReference Stage3_GenericLike2 = new FungusBlockReference("Stage3/GenericLike2");

	public static readonly FungusBlockReference Stage3_GenericLike3 = new FungusBlockReference("Stage3/GenericLike3");

	public static readonly FungusBlockReference Stage3_GenericLove1 = new FungusBlockReference("Stage3/GenericLove1");

	public static readonly FungusBlockReference Stage3_GenericLove2 = new FungusBlockReference("Stage3/GenericLove2");

	public static readonly FungusBlockReference Stage3_GenericSLove1 = new FungusBlockReference("Stage3/GenericSLove1");

	public static readonly FungusBlockReference Sleep_End_Sleep = new FungusBlockReference("Sleep/End Sleep");

	public static readonly FungusBlockReference Stage3_UniqueDislike1 = new FungusBlockReference("Stage3/UniqueDislike1");

	public static readonly FungusBlockReference Stage3_Unique1 = new FungusBlockReference("Stage3/Unique1");

	public static readonly FungusBlockReference Stage3_Unique2 = new FungusBlockReference("Stage3/Unique2");

	public static readonly FungusBlockReference Stage3_UniqueGame1 = new FungusBlockReference("Stage3/UniqueGame1");

	public static readonly FungusBlockReference Stage3_Unique4 = new FungusBlockReference("Stage3/Unique4");

	public static readonly FungusBlockReference Stage3_Unique5 = new FungusBlockReference("Stage3/Unique5");

	public static readonly FungusBlockReference Stage3_Unique6 = new FungusBlockReference("Stage3/Unique6");

	public static readonly FungusBlockReference Stage3_Unique7 = new FungusBlockReference("Stage3/Unique7");

	public static readonly FungusBlockReference Stage3_UniqueGame2 = new FungusBlockReference("Stage3/UniqueGame2");

	public static readonly FungusBlockReference Stage3_UniqueGame3 = new FungusBlockReference("Stage3/UniqueGame3");

	public static readonly FungusBlockReference Stage3_UniqueLike1 = new FungusBlockReference("Stage3/UniqueLike1");

	public static readonly FungusBlockReference Stage3_UniqueAffection1 = new FungusBlockReference("Stage3/UniqueAffection1");

	public static readonly FungusBlockReference Stage3_UniqueLove1 = new FungusBlockReference("Stage3/UniqueLove1");

	public static readonly FungusBlockReference Stage3_UniqueLove2 = new FungusBlockReference("Stage3/UniqueLove2");

	public static readonly FungusBlockReference Labour_Office_Main = new FungusBlockReference("Labour Office/Main");

	public static readonly FungusBlockReference Church_Church_start = new FungusBlockReference("Church/Church start");

	public static readonly FungusBlockReference Outside_Good_Dragonite = new FungusBlockReference("Outside/Good Dragonite");

	public static readonly FungusBlockReference Outside_Wac_Duck = new FungusBlockReference("Outside/Wac Duck");

	public static readonly FungusBlockReference Outside_Search_For_Job = new FungusBlockReference("Outside/Search For Job");

	public static readonly FungusBlockReference Labour_Office_Closed = new FungusBlockReference("Labour Office/Closed");

	public static readonly FungusBlockReference Sex_Uncover_bot = new FungusBlockReference("Sex/Uncover bot");

	public static readonly FungusBlockReference Sex_Stream_Missionary = new FungusBlockReference("Sex/Stream Missionary");

	public static readonly FungusBlockReference Prologue_After_prologue = new FungusBlockReference("Prologue/After prologue");

	public static readonly FungusBlockReference Prologue_Prologue = new FungusBlockReference("Prologue/Prologue");

	public static readonly FungusBlockReference Prologue_Skip_prologue = new FungusBlockReference("Prologue/Skip prologue");

	public static readonly FungusBlockReference Outside_Walk = new FungusBlockReference("Outside/Walk");

	public static readonly FungusBlockReference Outside_Walk_Label_WalkGenericStart = new FungusBlockReference("Outside/Walk", "WalkGenericStart");

	public static readonly FungusBlockReference Outside_Walk_Label_WalkJustEnding = new FungusBlockReference("Outside/Walk", "WalkJustEnding");

	public static readonly FungusBlockReference Outside_Fall_asleep_in_the_city = new FungusBlockReference("Outside/Fall asleep in the city");

	public static readonly FungusBlockReference Outside_Stonks = new FungusBlockReference("Outside/Stonks");

	public static readonly FungusBlockReference Church_Monolog_przed_kosciolem = new FungusBlockReference("Church/Monolog przed kosciolem");

	public static readonly FungusBlockReference Church_Spotyka_bota_A_poczatek = new FungusBlockReference("Church/Spotyka bota A poczatek");

	public static readonly FungusBlockReference Church_Spotyka_zakonnice = new FungusBlockReference("Church/Spotyka zakonnice");

	public static readonly FungusBlockReference Church_Spotkanie_bota_core = new FungusBlockReference("Church/Spotkanie bota core");

	public static readonly FungusBlockReference Church_There_is_no_real_priest_nearbyQ = new FungusBlockReference("Church/There is no real priest nearby?");

	public static readonly FungusBlockReference Church_This_place_is_a_mess_to_be_honestdot = new FungusBlockReference("Church/This place is a mess to be honest.");

	public static readonly FungusBlockReference Church_Spotyka_bota_A_poczatek_A = new FungusBlockReference("Church/Spotyka bota A poczatek A");

	public static readonly FungusBlockReference Church_Spotyka_bota_A_poczatek_B = new FungusBlockReference("Church/Spotyka bota A poczatek B");

	public static readonly FungusBlockReference Church_Spotyka_bota_A_koniec = new FungusBlockReference("Church/Spotyka bota A koniec");

	public static readonly FungusBlockReference Church_Visit_end = new FungusBlockReference("Church/Visit end");

	public static readonly FungusBlockReference Church_I_should_cheer_her_updot = new FungusBlockReference("Church/I should cheer her up.");

	public static readonly FungusBlockReference Church_She_needs_to_face_the_reality = new FungusBlockReference("Church/She needs to face the reality");

	public static readonly FungusBlockReference Church_Spotkanie_2B = new FungusBlockReference("Church/Spotkanie 2B");

	public static readonly FungusBlockReference Church_Spotkanie_2C = new FungusBlockReference("Church/Spotkanie 2C");

	public static readonly FungusBlockReference Church_Why_can_t_she_be_baptizedQ = new FungusBlockReference("Church/Why can’t she be baptized?");

	public static readonly FungusBlockReference Church_Spotkanie_2C_end = new FungusBlockReference("Church/Spotkanie 2C end");

	public static readonly FungusBlockReference Church_Spotyka_bota___crash_koniec = new FungusBlockReference("Church/Spotyka bota + crash koniec");

	public static readonly FungusBlockReference Church_Spotyka_bota___crash_B = new FungusBlockReference("Church/Spotyka bota + crash B");

	public static readonly FungusBlockReference Church_Spotyka_bota___crash_A = new FungusBlockReference("Church/Spotyka bota + crash A");

	public static readonly FungusBlockReference Church_Spotyka_bota___crash = new FungusBlockReference("Church/Spotyka bota + crash");

	public static readonly FungusBlockReference Church_Drugie_pojscie_do_kosciola_A = new FungusBlockReference("Church/Drugie pojscie do kosciola A");

	public static readonly FungusBlockReference Church_NIE_POWINIENEM_TAM_IŚĆ_O_TEJ_PORZE = new FungusBlockReference("Church/NIE POWINIENEM TAM IŚĆ O TEJ PORZE");

	public static readonly FungusBlockReference Church_2_pojscie_do_kosciola_B_wstęp_B = new FungusBlockReference("Church/2 pojscie do kosciola B wstęp B");

	public static readonly FungusBlockReference Church_2_pojscie_do_kosciola_B = new FungusBlockReference("Church/2 pojscie do kosciola B");

	public static readonly FungusBlockReference Church_2_pojscie_do_kosciola_B_wstęp_A = new FungusBlockReference("Church/2 pojscie do kosciola B wstęp A");

	public static readonly FungusBlockReference Church_Fuck_them = new FungusBlockReference("Church/Fuck them");

	public static readonly FungusBlockReference Church_Help_them = new FungusBlockReference("Church/Help them");

	public static readonly FungusBlockReference Church_Visit_end_short = new FungusBlockReference("Church/Visit end short");

	public static readonly FungusBlockReference Church_THE_WORST_END = new FungusBlockReference("Church/THE WORST END");

	public static readonly FungusBlockReference Church_Revival = new FungusBlockReference("Church/Revival");

	public static readonly FungusBlockReference Church_Just_go_there_alreadyexclamation_switch = new FungusBlockReference("Church/Just go there already! switch");

	public static readonly FungusBlockReference Church_I_don_t_knowexclamation_He_s_the_priest__he_knows_the_bestexclamation = new FungusBlockReference("Church/I don’t know! He’s the priest, he knows the best!");

	public static readonly FungusBlockReference Church_Just_go_there_alreadyexclamation = new FungusBlockReference("Church/Just go there already!");

	public static readonly FungusBlockReference Church_Just_go_there_alreadyexclamation_bot__10 = new FungusBlockReference("Church/Just go there already! bot +10");

	public static readonly FungusBlockReference Church_What_happened_after = new FungusBlockReference("Church/What happened after");

	public static readonly FungusBlockReference Church_SpotkanieBC = new FungusBlockReference("Church/SpotkanieBC");

	public static readonly FungusBlockReference Church_DrugiePojscieABA = new FungusBlockReference("Church/DrugiePojscieABA");

	public static readonly FungusBlockReference Church_DrugiePojscieBA = new FungusBlockReference("Church/DrugiePojscieBA");

	public static readonly FungusBlockReference Church_TheWorstEndStart = new FungusBlockReference("Church/TheWorstEndStart");

	public static readonly FungusBlockReference Church_DrugiePojscieBB = new FungusBlockReference("Church/DrugiePojscieBB");

	public static readonly FungusBlockReference RE_Rent_Continue_generic = new FungusBlockReference("RE/Rent/Continue-generic");

	public static readonly FungusBlockReference Email_Bo_Brogan_Podcast = new FungusBlockReference("Email/Bo Brogan Podcast");

	public static readonly FungusBlockReference Email_Join_Us_Email = new FungusBlockReference("Email/Join Us Email");

	public static readonly FungusBlockReference Email_Join_Us_Ignore = new FungusBlockReference("Email/Join Us Ignore");

	public static readonly FungusBlockReference Email_Join_Us_Accept = new FungusBlockReference("Email/Join Us Accept");

	public static readonly FungusBlockReference RE_Day13_DAY_13__14_52 = new FungusBlockReference("RE/Day13/DAY 13, 14:52");

	public static readonly FungusBlockReference Day13_Day_13_busy = new FungusBlockReference("Day13/Day 13 busy");

	public static readonly FungusBlockReference Day13_Scam_part_1 = new FungusBlockReference("Day13/Scam part 1");

	public static readonly FungusBlockReference Day13_Scam_part_2 = new FungusBlockReference("Day13/Scam part 2");

	public static readonly FungusBlockReference Day13_Scam_part_3 = new FungusBlockReference("Day13/Scam part 3");

	public static readonly FungusBlockReference Day13_Got_Scammed = new FungusBlockReference("Day13/Got Scammed");

	public static readonly FungusBlockReference Day13_Denied_scam = new FungusBlockReference("Day13/Denied scam");

	public static readonly FungusBlockReference Email_Friend_Email2 = new FungusBlockReference("Email/Friend Email2");

	public static readonly FungusBlockReference Email_Doctor_Email = new FungusBlockReference("Email/Doctor Email");

	public static readonly FungusBlockReference RE_DiarrheaEvent = new FungusBlockReference("RE/DiarrheaEvent");

	public static readonly FungusBlockReference LandlordMoneyEvent_Start = new FungusBlockReference("LandlordMoneyEvent/Start");

	public static readonly FungusBlockReference LandlordMoneyEvent_Can_I_pay_it_for_themQ = new FungusBlockReference("LandlordMoneyEvent/Can I pay it for them?");

	public static readonly FungusBlockReference LandlordMoneyEvent_Say_nothing = new FungusBlockReference("LandlordMoneyEvent/Say nothing");

	public static readonly FungusBlockReference StaircaseAlone_StaircaseEventReturn = new FungusBlockReference("StaircaseAlone/StaircaseEventReturn");

	public static readonly FungusBlockReference LandlordMoneyEvent_Pay = new FungusBlockReference("LandlordMoneyEvent/Pay");

	public static readonly FungusBlockReference LandlordMoneyEvent_Don_t_Pay = new FungusBlockReference("LandlordMoneyEvent/Don't Pay");

	public static readonly FungusBlockReference LandlordMoneyEvent_Yes = new FungusBlockReference("LandlordMoneyEvent/Yes");

	public static readonly FungusBlockReference LandlordMoneyEvent_No = new FungusBlockReference("LandlordMoneyEvent/No");

	public static readonly FungusBlockReference Stage3_UniqueAffection2 = new FungusBlockReference("Stage3/UniqueAffection2");

	public static readonly FungusBlockReference Stage3_UniqueLike2 = new FungusBlockReference("Stage3/UniqueLike2");

	public static readonly FungusBlockReference Stage3_Unique8 = new FungusBlockReference("Stage3/Unique8");

	public static readonly FungusBlockReference Stage3_Unique9 = new FungusBlockReference("Stage3/Unique9");

	public static readonly FungusBlockReference Stage3_GenericLike4 = new FungusBlockReference("Stage3/GenericLike4");

	public static readonly FungusBlockReference Stage3_GenericLove3 = new FungusBlockReference("Stage3/GenericLove3");

	public static readonly FungusBlockReference Stage3_GenericLove4 = new FungusBlockReference("Stage3/GenericLove4");

	public static readonly FungusBlockReference Stage3_GenericLove5 = new FungusBlockReference("Stage3/GenericLove5");

	public static readonly FungusBlockReference Stage3_GenericLove6 = new FungusBlockReference("Stage3/GenericLove6");

	public static readonly FungusBlockReference Sleep_Sleep_until_rested = new FungusBlockReference("Sleep/Sleep until rested");

	public static readonly FungusBlockReference Dispenser_Main = new FungusBlockReference("Dispenser/Main");

	public static readonly FungusBlockReference Outside_FD_wait = new FungusBlockReference("Outside/FD wait");

	public static readonly FungusBlockReference Outside_FD_get = new FungusBlockReference("Outside/FD get");

	public static readonly FungusBlockReference Stage1_Generic9 = new FungusBlockReference("Stage1/Generic9");

	public static readonly FungusBlockReference Stage1_Generic10 = new FungusBlockReference("Stage1/Generic10");

	public static readonly FungusBlockReference Stage1_Unique2 = new FungusBlockReference("Stage1/Unique2");

	public static readonly FungusBlockReference Stage1_Unique3 = new FungusBlockReference("Stage1/Unique3");

	public static readonly FungusBlockReference Stage1_Unique4 = new FungusBlockReference("Stage1/Unique4");

	public static readonly FungusBlockReference Stage1_Unique5 = new FungusBlockReference("Stage1/Unique5");

	public static readonly FungusBlockReference Stage2_Unique7 = new FungusBlockReference("Stage2/Unique7");

	public static readonly FungusBlockReference Stage2_Unique8 = new FungusBlockReference("Stage2/Unique8");

	public static readonly FungusBlockReference Stage3_GenericDetest3 = new FungusBlockReference("Stage3/GenericDetest3");

	public static readonly FungusBlockReference Stage3_Generic6 = new FungusBlockReference("Stage3/Generic6");

	public static readonly FungusBlockReference Stage3_Generic7 = new FungusBlockReference("Stage3/Generic7");

	public static readonly FungusBlockReference Stage3_Generic8 = new FungusBlockReference("Stage3/Generic8");

	public static readonly FungusBlockReference Stage3_Generic9 = new FungusBlockReference("Stage3/Generic9");

	public static readonly FungusBlockReference Stage3_Generic10 = new FungusBlockReference("Stage3/Generic10");

	public static readonly FungusBlockReference Stage3_GenericLove7 = new FungusBlockReference("Stage3/GenericLove7");

	public static readonly FungusBlockReference Stage3_GenericLike5 = new FungusBlockReference("Stage3/GenericLike5");

	public static readonly FungusBlockReference Stage3_GenericLike6 = new FungusBlockReference("Stage3/GenericLike6");

	public static readonly FungusBlockReference Stage3_GenericLike7 = new FungusBlockReference("Stage3/GenericLike7");

	public static readonly FungusBlockReference Stage3_GenericSLove2 = new FungusBlockReference("Stage3/GenericSLove2");

	public static readonly FungusBlockReference Stage3_UniqueLike3 = new FungusBlockReference("Stage3/UniqueLike3");

	public static readonly FungusBlockReference Stage3_UniqueLike4 = new FungusBlockReference("Stage3/UniqueLike4");

	public static readonly FungusBlockReference Stage2_Unique9 = new FungusBlockReference("Stage2/Unique9");

	public static readonly FungusBlockReference Stage3_UniqueAffection3 = new FungusBlockReference("Stage3/UniqueAffection3");

	public static readonly FungusBlockReference Stage3_UniqueAffection4 = new FungusBlockReference("Stage3/UniqueAffection4");

	public static readonly FungusBlockReference Stage3_GenericDislike3 = new FungusBlockReference("Stage3/GenericDislike3");

	public static readonly FungusBlockReference Stage3_GenericDislike4 = new FungusBlockReference("Stage3/GenericDislike4");

	public static readonly FungusBlockReference Stage3_GenericDislike5 = new FungusBlockReference("Stage3/GenericDislike5");

	public static readonly FungusBlockReference Stage3_UniqueLove3 = new FungusBlockReference("Stage3/UniqueLove3");

	public static readonly FungusBlockReference Stage3_UniqueLove4 = new FungusBlockReference("Stage3/UniqueLove4");

	public static readonly FungusBlockReference Stage3_UniqueLove5 = new FungusBlockReference("Stage3/UniqueLove5");

	public static readonly FungusBlockReference RE_HealthEnd = new FungusBlockReference("RE/HealthEnd");

	public static readonly FungusBlockReference RE_DAY_10__22_17 = new FungusBlockReference("RE/DAY 10, 22:17");

	public static readonly FungusBlockReference RE_DAY_13__22_17 = new FungusBlockReference("RE/DAY 13, 22:17");

	public static readonly FungusBlockReference RE_Spook1 = new FungusBlockReference("RE/Spook1");

	public static readonly FungusBlockReference RE_Spook2 = new FungusBlockReference("RE/Spook2");

	public static readonly FungusBlockReference Trash_Throw_away = new FungusBlockReference("Trash/Throw away");

	public static readonly FungusBlockReference RE_Trash_Fed_End = new FungusBlockReference("RE/Trash/Fed End");

	public static readonly FungusBlockReference RE_Trash_Get_caught = new FungusBlockReference("RE/Trash/Get caught");

	public static readonly FungusBlockReference RE_Trash_Trash1 = new FungusBlockReference("RE/Trash/Trash1");

	public static readonly FungusBlockReference RE_Trash_Trash2 = new FungusBlockReference("RE/Trash/Trash2");

	public static readonly FungusBlockReference Outside_bigdaddyhurt = new FungusBlockReference("Outside/bigdaddyhurt");

	public static readonly FungusBlockReference WakeUp_Schizo_End = new FungusBlockReference("WakeUp/Schizo End");

	public static readonly FungusBlockReference RE_Blaubeim1 = new FungusBlockReference("RE/Blaubeim1");

	public static readonly FungusBlockReference RE_GenericSchizoEnd = new FungusBlockReference("RE/GenericSchizoEnd");

	public static readonly FungusBlockReference RE_CasinoStart = new FungusBlockReference("RE/CasinoStart");

	public static readonly FungusBlockReference WakeUp_Headache_hash3 = new FungusBlockReference("WakeUp/Headache #3");

	public static readonly FungusBlockReference Stage3_Greeting = new FungusBlockReference("Stage3/Greeting");

	public static readonly FungusBlockReference Stage3_Unique10 = new FungusBlockReference("Stage3/Unique10");

	public static readonly FungusBlockReference Stage3_UniqueLike5 = new FungusBlockReference("Stage3/UniqueLike5");

	public static readonly FungusBlockReference Stage3_UniqueLike6 = new FungusBlockReference("Stage3/UniqueLike6");

	public static readonly FungusBlockReference Stage3_UniqueLike7 = new FungusBlockReference("Stage3/UniqueLike7");

	public static readonly FungusBlockReference Stage3_UniqueAffection5 = new FungusBlockReference("Stage3/UniqueAffection5");

	public static readonly FungusBlockReference Stage3_UniqueAffection6 = new FungusBlockReference("Stage3/UniqueAffection6");

	public static readonly FungusBlockReference Stage3_UniqueAffection7 = new FungusBlockReference("Stage3/UniqueAffection7");

	public static readonly FungusBlockReference Stage3_UniqueAffection8 = new FungusBlockReference("Stage3/UniqueAffection8");

	public static readonly FungusBlockReference Stage3_UniqueAffection9 = new FungusBlockReference("Stage3/UniqueAffection9");

	public static readonly FungusBlockReference Stage3_UniqueAffection10 = new FungusBlockReference("Stage3/UniqueAffection10");

	public static readonly FungusBlockReference Outside_anotherrobopriest = new FungusBlockReference("Outside/anotherrobopriest");

	public static readonly FungusBlockReference Outside_junkie = new FungusBlockReference("Outside/junkie");

	public static readonly FungusBlockReference Outside_Paper_bag = new FungusBlockReference("Outside/Paper bag");

	public static readonly FungusBlockReference Outside_junkie5 = new FungusBlockReference("Outside/junkie5");

	public static readonly FungusBlockReference Outside_junkie6 = new FungusBlockReference("Outside/junkie6");

	public static readonly FungusBlockReference Outside_junkie4 = new FungusBlockReference("Outside/junkie4");

	public static readonly FungusBlockReference Outside_junkie8 = new FungusBlockReference("Outside/junkie8");

	public static readonly FungusBlockReference Outside_junkie7 = new FungusBlockReference("Outside/junkie7");

	public static readonly FungusBlockReference Outside_junkie3 = new FungusBlockReference("Outside/junkie3");

	public static readonly FungusBlockReference Outside_GoHome = new FungusBlockReference("Outside/GoHome");

	public static readonly FungusBlockReference Outside_Walk_with_Landlord = new FungusBlockReference("Outside/Walk with Landlord");

	public static readonly FungusBlockReference Outside_trashdiving = new FungusBlockReference("Outside/trashdiving");

	public static readonly FungusBlockReference Outside_Clothier_Main = new FungusBlockReference("Outside/Clothier/Main");

	public static readonly FungusBlockReference Outside_Clothier_event1 = new FungusBlockReference("Outside/Clothier/event1");

	public static readonly FungusBlockReference Outside_Clothier_event2 = new FungusBlockReference("Outside/Clothier/event2");

	public static readonly FungusBlockReference Outside_Clothier_event3 = new FungusBlockReference("Outside/Clothier/event3");

	public static readonly FungusBlockReference Outside_Clothier_event4 = new FungusBlockReference("Outside/Clothier/event4");

	public static readonly FungusBlockReference Stage3_FirstClothes = new FungusBlockReference("Stage3/FirstClothes");

	public static readonly FungusBlockReference Stage3_Panties = new FungusBlockReference("Stage3/Panties");

	public static readonly FungusBlockReference Stage3_CuteBow = new FungusBlockReference("Stage3/CuteBow");

	public static readonly FungusBlockReference Stage3_Stockings = new FungusBlockReference("Stage3/Stockings");

	public static readonly FungusBlockReference Stage3_PleatedSkirt = new FungusBlockReference("Stage3/PleatedSkirt");

	public static readonly FungusBlockReference Stage3_LingerieBra = new FungusBlockReference("Stage3/LingerieBra");

	public static readonly FungusBlockReference Stage3_LingeriePanties = new FungusBlockReference("Stage3/LingeriePanties");

	public static readonly FungusBlockReference Stage3_LingerieStockings = new FungusBlockReference("Stage3/LingerieStockings");

	public static readonly FungusBlockReference Stage3_Statistics = new FungusBlockReference("Stage3/Statistics");

	public static readonly FungusBlockReference Outside_Clothier_ShopWithBot = new FungusBlockReference("Outside/Clothier/ShopWithBot");

	public static readonly FungusBlockReference Outside_Clothier_Birthday = new FungusBlockReference("Outside/Clothier/Birthday");

	public static readonly FungusBlockReference Outside_Clothier_Coat = new FungusBlockReference("Outside/Clothier/Coat");

	public static readonly FungusBlockReference Outside_Clothier_GoingOutside1 = new FungusBlockReference("Outside/Clothier/GoingOutside1");

	public static readonly FungusBlockReference Outside_Clothier_FirstTime = new FungusBlockReference("Outside/Clothier/FirstTime");

	public static readonly FungusBlockReference Outside_Clothier_GoingOutsideSide = new FungusBlockReference("Outside/Clothier/GoingOutsideSide");

	public static readonly FungusBlockReference Stage3_GoingOutside1 = new FungusBlockReference("Stage3/GoingOutside1");

	public static readonly FungusBlockReference Outside_OutsideStartWithBot = new FungusBlockReference("Outside/OutsideStartWithBot");

	public static readonly FungusBlockReference Outside_Clothier_startWithBot = new FungusBlockReference("Outside/Clothier/startWithBot");

	public static readonly FungusBlockReference Outside_Clothier_startWithBot_Label_ShopStart = new FungusBlockReference("Outside/Clothier/startWithBot", "ShopStart");

	public static readonly FungusBlockReference Outside_Clothier_startWithBot_Label_Leave = new FungusBlockReference("Outside/Clothier/startWithBot", "Leave");

	public static readonly FungusBlockReference Outside_OutsideWithBot = new FungusBlockReference("Outside/OutsideWithBot");

	public static readonly FungusBlockReference Outside_OutsideWithBot_Label_StartOutsideBeforeMenu = new FungusBlockReference("Outside/OutsideWithBot", "StartOutsideBeforeMenu");

	public static readonly FungusBlockReference Outside_OutsideWithBot_Label_StartOutside = new FungusBlockReference("Outside/OutsideWithBot", "StartOutside");

	public static readonly FungusBlockReference Outside_OutsideEndWithBot = new FungusBlockReference("Outside/OutsideEndWithBot");

	public static readonly FungusBlockReference Outside_Clothier_GenericGreeting = new FungusBlockReference("Outside/Clothier/GenericGreeting");

	public static readonly FungusBlockReference Outside_Clothier_BestClient = new FungusBlockReference("Outside/Clothier/BestClient");

	public static readonly FungusBlockReference Outside_Clothier_Question = new FungusBlockReference("Outside/Clothier/Question");

	public static readonly FungusBlockReference Outside_Clothier_GenericFarewell = new FungusBlockReference("Outside/Clothier/GenericFarewell");

	public static readonly FungusBlockReference Outside_GoingOutside3 = new FungusBlockReference("Outside/GoingOutside3");

	public static readonly FungusBlockReference Outside_LandlordConspiracy0 = new FungusBlockReference("Outside/LandlordConspiracy0");

	public static readonly FungusBlockReference Outside_Walk_LandlordConspiracy1 = new FungusBlockReference("Outside/Walk/LandlordConspiracy1");

	public static readonly FungusBlockReference Outside_Walk_WalkWithBotStart = new FungusBlockReference("Outside/Walk/WalkWithBotStart");

	public static readonly FungusBlockReference Outside_LandlordConspiracy3 = new FungusBlockReference("Outside/LandlordConspiracy3");

	public static readonly FungusBlockReference RE_LandlordConspiracy2 = new FungusBlockReference("RE/LandlordConspiracy2");

	public static readonly FungusBlockReference Outside_LandlordDaughter1 = new FungusBlockReference("Outside/LandlordDaughter1");

	public static readonly FungusBlockReference Outside_GenericStaircase = new FungusBlockReference("Outside/GenericStaircase");

	public static readonly FungusBlockReference Outside_Walk_AirPollutionMask = new FungusBlockReference("Outside/Walk/AirPollutionMask");

	public static readonly FungusBlockReference Outside_Clothier_GenericGreetingWithBot = new FungusBlockReference("Outside/Clothier/GenericGreetingWithBot");

	public static readonly FungusBlockReference Outside_Clothier_GenericFarewellWithBot = new FungusBlockReference("Outside/Clothier/GenericFarewellWithBot");

	public static readonly FungusBlockReference Outside_Walk_FirstWalk = new FungusBlockReference("Outside/Walk/FirstWalk");

	public static readonly FungusBlockReference Outside_Walk_GenericWalk = new FungusBlockReference("Outside/Walk/GenericWalk");

	public static readonly FungusBlockReference Outside_PassOutWithBot = new FungusBlockReference("Outside/PassOutWithBot");

	public static readonly FungusBlockReference RE_Rent_Bad_End_4 = new FungusBlockReference("RE/Rent/Bad End 4");

	public static readonly FungusBlockReference RE_Rent_Bad_End_4_Label_StartOfTheEnd = new FungusBlockReference("RE/Rent/Bad End 4", "StartOfTheEnd");

	public static readonly FungusBlockReference Outside_Exposure_night = new FungusBlockReference("Outside/Exposure night");

	public static readonly FungusBlockReference Outside_Exposure_day = new FungusBlockReference("Outside/Exposure day");

	public static readonly FungusBlockReference Outside_Exposure_little_girl = new FungusBlockReference("Outside/Exposure little girl");

	public static readonly FungusBlockReference Outside_Generic_Exposure = new FungusBlockReference("Outside/Generic Exposure");

	public static readonly FungusBlockReference Outside_Clothier_CustomClothing = new FungusBlockReference("Outside/Clothier/CustomClothing");

	public static readonly FungusBlockReference Outside_Clothier_MentalHealth = new FungusBlockReference("Outside/Clothier/MentalHealth");

	public static readonly FungusBlockReference RE_Search = new FungusBlockReference("RE/Search");

	public static readonly FungusBlockReference RE_Ending_Search = new FungusBlockReference("RE/Ending Search");

	public static readonly FungusBlockReference RE_TooMuchCum1 = new FungusBlockReference("RE/TooMuchCum1");

	public static readonly FungusBlockReference RE_TooMuchCumEnding = new FungusBlockReference("RE/TooMuchCumEnding");

	public static readonly FungusBlockReference RE_SearchLandlord = new FungusBlockReference("RE/SearchLandlord");

	public static readonly FungusBlockReference Stage3_GenericLove8 = new FungusBlockReference("Stage3/GenericLove8");

	public static readonly FungusBlockReference Stage3_GenericLove10 = new FungusBlockReference("Stage3/GenericLove10");

	public static readonly FungusBlockReference Stage3_GenericLove11 = new FungusBlockReference("Stage3/GenericLove11");

	public static readonly FungusBlockReference Stage3_GenericLove12 = new FungusBlockReference("Stage3/GenericLove12");

	public static readonly FungusBlockReference Stage3_GenericLove13 = new FungusBlockReference("Stage3/GenericLove13");

	public static readonly FungusBlockReference Stage3_UniqueAffection11 = new FungusBlockReference("Stage3/UniqueAffection11");

	public static readonly FungusBlockReference Stage3_UniqueAffection12 = new FungusBlockReference("Stage3/UniqueAffection12");

	public static readonly FungusBlockReference Stage3_UniqueAffection13 = new FungusBlockReference("Stage3/UniqueAffection13");

	public static readonly FungusBlockReference Stage3_UniqueAffection14 = new FungusBlockReference("Stage3/UniqueAffection14");

	public static readonly FungusBlockReference Stage3_UniqueAffection15 = new FungusBlockReference("Stage3/UniqueAffection15");

	public static readonly FungusBlockReference Stage3_UniqueAffection16 = new FungusBlockReference("Stage3/UniqueAffection16");

	public static readonly FungusBlockReference Stage3_UniqueAffection17 = new FungusBlockReference("Stage3/UniqueAffection17");

	public static readonly FungusBlockReference Stage3_UniqueAffection18 = new FungusBlockReference("Stage3/UniqueAffection18");

	public static readonly FungusBlockReference Stage3_UniqueAffection19 = new FungusBlockReference("Stage3/UniqueAffection19");

	public static readonly FungusBlockReference Stage3_UniqueAffection20 = new FungusBlockReference("Stage3/UniqueAffection20");

	public static readonly FungusBlockReference Stage3_UniqueAffection21 = new FungusBlockReference("Stage3/UniqueAffection21");

	public static readonly FungusBlockReference Outside_RandomSearch1 = new FungusBlockReference("Outside/RandomSearch1");

	public static readonly FungusBlockReference Outside_RandomSearch2 = new FungusBlockReference("Outside/RandomSearch2");

	public static readonly FungusBlockReference Outside_RandomSearch3 = new FungusBlockReference("Outside/RandomSearch3");

	public static readonly FungusBlockReference Outside_Hobo1 = new FungusBlockReference("Outside/Hobo1");

	public static readonly FungusBlockReference Outside_Hobo2 = new FungusBlockReference("Outside/Hobo2");

	public static readonly FungusBlockReference Outside_NunInvestigation1 = new FungusBlockReference("Outside/NunInvestigation1");

	public static readonly FungusBlockReference Church_AfterStory_NunInvestigation2 = new FungusBlockReference("Church/AfterStory/NunInvestigation2");

	public static readonly FungusBlockReference Outside_NunInvestigation3 = new FungusBlockReference("Outside/NunInvestigation3");

	public static readonly FungusBlockReference Outside_Psychologist1 = new FungusBlockReference("Outside/Psychologist1");

	public static readonly FungusBlockReference Outside_WalkEvents = new FungusBlockReference("Outside/WalkEvents");

	public static readonly FungusBlockReference Outside_ReturnToWalk = new FungusBlockReference("Outside/ReturnToWalk");

	public static readonly FungusBlockReference Email_Join_Us_Email_2 = new FungusBlockReference("Email/Join Us Email 2");

	public static readonly FungusBlockReference Outside_Lorerant1 = new FungusBlockReference("Outside/Lorerant1");

	public static readonly FungusBlockReference Outside_ArtGalleryDate1 = new FungusBlockReference("Outside/ArtGalleryDate1");

	public static readonly FungusBlockReference Outside_ArtGalleryDate2 = new FungusBlockReference("Outside/ArtGalleryDate2");

	public static readonly FungusBlockReference Outside_RestaurantDate1 = new FungusBlockReference("Outside/RestaurantDate1");

	public static readonly FungusBlockReference Outside_RestaurantDate2 = new FungusBlockReference("Outside/RestaurantDate2");

	public static readonly FungusBlockReference Outside_Kino1 = new FungusBlockReference("Outside/Kino1");

	public static readonly FungusBlockReference Outside_Kino2 = new FungusBlockReference("Outside/Kino2");

	public static readonly FungusBlockReference Outside_CheckDate = new FungusBlockReference("Outside/CheckDate");

	public static readonly FungusBlockReference Stage3_HalloweenEvent1 = new FungusBlockReference("Stage3/HalloweenEvent1");

	public static readonly FungusBlockReference Stage3_HalloweenOutside = new FungusBlockReference("Stage3/HalloweenOutside");

	public static readonly FungusBlockReference Outside_Halloween_TrickOrTreat = new FungusBlockReference("Outside/Halloween/TrickOrTreat");

	public static readonly FungusBlockReference Stage3_HalloweenEvent2 = new FungusBlockReference("Stage3/HalloweenEvent2");

	public static readonly FungusBlockReference Stage3_HalloweenEvent3 = new FungusBlockReference("Stage3/HalloweenEvent3");

	public static readonly FungusBlockReference Outside_Clothier_HalloweenAlone = new FungusBlockReference("Outside/Clothier/HalloweenAlone");

	public static readonly FungusBlockReference Outside_Clothier_HalloweenWithBot = new FungusBlockReference("Outside/Clothier/HalloweenWithBot");

	public static readonly FungusBlockReference Outside_Clothier_HalloweenWithBot2 = new FungusBlockReference("Outside/Clothier/HalloweenWithBot2");

	public static readonly FungusBlockReference Outside_Halloween_TOT1 = new FungusBlockReference("Outside/Halloween/TOT1");

	public static readonly FungusBlockReference Outside_Halloween_TOTCheck = new FungusBlockReference("Outside/Halloween/TOTCheck");

	public static readonly FungusBlockReference Outside_Halloween_TOTStart = new FungusBlockReference("Outside/Halloween/TOTStart");

	public static readonly FungusBlockReference Outside_Halloween_TOT2 = new FungusBlockReference("Outside/Halloween/TOT2");

	public static readonly FungusBlockReference Outside_Halloween_TOT3 = new FungusBlockReference("Outside/Halloween/TOT3");

	public static readonly FungusBlockReference Outside_Halloween_TOT4 = new FungusBlockReference("Outside/Halloween/TOT4");

	public static readonly FungusBlockReference Outside_Halloween_TOTGeneric = new FungusBlockReference("Outside/Halloween/TOTGeneric");

	public static readonly FungusBlockReference Item_CazinoBundleEvent = new FungusBlockReference("Item/CazinoBundleEvent");

	public static readonly FungusBlockReference Outside_NunBack1 = new FungusBlockReference("Outside/NunBack1");

	public static readonly FungusBlockReference Church_AfterStory_VisitStart = new FungusBlockReference("Church/AfterStory/VisitStart");

	public static readonly FungusBlockReference Church_AfterStory_VisitStart_Label_GenericGreeting = new FungusBlockReference("Church/AfterStory/VisitStart", "GenericGreeting");

	public static readonly FungusBlockReference Church_AfterStory_VisitStart_Label_AlreadyVisitedToday = new FungusBlockReference("Church/AfterStory/VisitStart", "AlreadyVisitedToday");

	public static readonly FungusBlockReference Church_AfterStory_VisitStart_Label_TooLate = new FungusBlockReference("Church/AfterStory/VisitStart", "TooLate");

	public static readonly FungusBlockReference Church_AfterStory_NunBack2 = new FungusBlockReference("Church/AfterStory/NunBack2");

	public static readonly FungusBlockReference Church_AfterStory_VisitEnd = new FungusBlockReference("Church/AfterStory/VisitEnd");

	public static readonly FungusBlockReference Church_AfterStory_NunBack3 = new FungusBlockReference("Church/AfterStory/NunBack3");

	public static readonly FungusBlockReference Church_AfterStory_NunBack4 = new FungusBlockReference("Church/AfterStory/NunBack4");

	public static readonly FungusBlockReference Church_AfterStory_Shop = new FungusBlockReference("Church/AfterStory/Shop");

	public static readonly FungusBlockReference Church_AfterStory_Shop_Label_AfterGreeting = new FungusBlockReference("Church/AfterStory/Shop", "AfterGreeting");

	public static readonly FungusBlockReference Church_AfterStory_GokusMotivation = new FungusBlockReference("Church/AfterStory/GokusMotivation");

	public static readonly FungusBlockReference Church_AfterStory_ShopBypassGreeting = new FungusBlockReference("Church/AfterStory/ShopBypassGreeting");

	public static readonly FungusBlockReference Church_AfterStory_NunBack5 = new FungusBlockReference("Church/AfterStory/NunBack5");

	public static readonly FungusBlockReference Church_AfterStory_GenericGreeting = new FungusBlockReference("Church/AfterStory/GenericGreeting");

	public static readonly FungusBlockReference Church_AfterStory_GenericGoodbye = new FungusBlockReference("Church/AfterStory/GenericGoodbye");

	public static readonly FungusBlockReference Church_AfterStory_GenericitemRepair = new FungusBlockReference("Church/AfterStory/GenericitemRepair");

	public static readonly FungusBlockReference Stage3_nunBack = new FungusBlockReference("Stage3/nunBack");

	public static readonly FungusBlockReference RE_NunBack6 = new FungusBlockReference("RE/NunBack6");

	public static readonly FungusBlockReference Outside_Walk_WalkWithBotEnd = new FungusBlockReference("Outside/Walk/WalkWithBotEnd");

	public static readonly FungusBlockReference Outside_Walk_MeetingNunWithBot = new FungusBlockReference("Outside/Walk/MeetingNunWithBot");

	public static readonly FungusBlockReference Outside_NunBack7 = new FungusBlockReference("Outside/NunBack7");

	public static readonly FungusBlockReference RE_BigDaddyKnowsEnding = new FungusBlockReference("RE/BigDaddyKnowsEnding");

	public static readonly FungusBlockReference RE_BigDaddyEpilogue = new FungusBlockReference("RE/BigDaddyEpilogue");

	public static readonly FungusBlockReference Outside_BigdaddyStoryline1 = new FungusBlockReference("Outside/BigdaddyStoryline1");

	public static readonly FungusBlockReference Outside_VisitBigDaddy = new FungusBlockReference("Outside/VisitBigDaddy");

	public static readonly FungusBlockReference Outside_VisitBigDaddy_Label_AlreadyVisitedToday = new FungusBlockReference("Outside/VisitBigDaddy", "AlreadyVisitedToday");

	public static readonly FungusBlockReference Outside_VisitBigDaddy_Label_Work = new FungusBlockReference("Outside/VisitBigDaddy", "Work");

	public static readonly FungusBlockReference Outside_VisitBigDaddy_Label_Sleeping = new FungusBlockReference("Outside/VisitBigDaddy", "Sleeping");

	public static readonly FungusBlockReference Outside_VisitBigDaddyGeneric = new FungusBlockReference("Outside/VisitBigDaddyGeneric");

	public static readonly FungusBlockReference Outside_Bigdaddystoryline2 = new FungusBlockReference("Outside/Bigdaddystoryline2");

	public static readonly FungusBlockReference Outside_Bigdaddystoryline3 = new FungusBlockReference("Outside/Bigdaddystoryline3");

	public static readonly FungusBlockReference Outside_BigdaddyInvitation = new FungusBlockReference("Outside/BigdaddyInvitation");

	public static readonly FungusBlockReference RE_DinnerWithBigDaddy1 = new FungusBlockReference("RE/DinnerWithBigDaddy1");

	public static readonly FungusBlockReference Outside_Bigdaddystoryline4 = new FungusBlockReference("Outside/Bigdaddystoryline4");

	public static readonly FungusBlockReference Church_AfterStory_BigDaddyStorylineChurchPart1 = new FungusBlockReference("Church/AfterStory/BigDaddyStorylineChurchPart1");

	public static readonly FungusBlockReference RE_CocktractsStart = new FungusBlockReference("RE/CocktractsStart");

	public static readonly FungusBlockReference Stage3_UniqueAffection22 = new FungusBlockReference("Stage3/UniqueAffection22");

	public static readonly FungusBlockReference Outside_Karaoke1 = new FungusBlockReference("Outside/Karaoke1");

	public static readonly FungusBlockReference Outside_Walk_Karaoke2 = new FungusBlockReference("Outside/Walk/Karaoke2");

	public static readonly FungusBlockReference Outside_MentalHealthSpook = new FungusBlockReference("Outside/MentalHealthSpook");

	public static readonly FungusBlockReference Sleep_Sleep_With_Bot = new FungusBlockReference("Sleep/Sleep With Bot");

	public static readonly FungusBlockReference Outside_Clothier_ClothierViewWithBot = new FungusBlockReference("Outside/Clothier/ClothierViewWithBot");

	public static readonly FungusBlockReference Outside_Clothier_ChangingRoom = new FungusBlockReference("Outside/Clothier/ChangingRoom");

	public static readonly FungusBlockReference Outside_Clothier_ShopWithoutBot = new FungusBlockReference("Outside/Clothier/ShopWithoutBot");

	public static readonly FungusBlockReference Stage3_ChangeBotName = new FungusBlockReference("Stage3/ChangeBotName");

	public static readonly FungusBlockReference Stage3_ChangePlayerName = new FungusBlockReference("Stage3/ChangePlayerName");

	public static readonly FungusBlockReference Outside_RiotsStart = new FungusBlockReference("Outside/RiotsStart");

	public static readonly FungusBlockReference Shop_Riots_FirstTime = new FungusBlockReference("Shop/Riots/FirstTime");

	public static readonly FungusBlockReference Shop_Riots_Generic = new FungusBlockReference("Shop/Riots/Generic");

	public static readonly FungusBlockReference Shop_Riots_Hungry = new FungusBlockReference("Shop/Riots/Hungry");

	public static readonly FungusBlockReference Shop_AfterRiots_FirstTime = new FungusBlockReference("Shop/AfterRiots/FirstTime");

	public static readonly FungusBlockReference Dispenser_Riots_FirstTime = new FungusBlockReference("Dispenser/Riots/FirstTime");

	public static readonly FungusBlockReference Dispenser_Riots_Generic = new FungusBlockReference("Dispenser/Riots/Generic");

	public static readonly FungusBlockReference Dispenser_Riots_Hobo = new FungusBlockReference("Dispenser/Riots/Hobo");

	public static readonly FungusBlockReference Dispenser_AfterRiots = new FungusBlockReference("Dispenser/AfterRiots");

	public static readonly FungusBlockReference Pharmacy_Riots_Generic = new FungusBlockReference("Pharmacy/Riots/Generic");

	public static readonly FungusBlockReference Pharmacy_Riots_FirstTime = new FungusBlockReference("Pharmacy/Riots/FirstTime");

	public static readonly FungusBlockReference Pharmacy_Riots_Random = new FungusBlockReference("Pharmacy/Riots/Random");

	public static readonly FungusBlockReference Pharmacy_AfterRiots = new FungusBlockReference("Pharmacy/AfterRiots");

	public static readonly FungusBlockReference Labour_Office_Riots_FirstTime = new FungusBlockReference("Labour Office/Riots/FirstTime");

	public static readonly FungusBlockReference Labour_Office_Riots_Landlord = new FungusBlockReference("Labour Office/Riots/Landlord");

	public static readonly FungusBlockReference Labour_Office_Riots_Generic = new FungusBlockReference("Labour Office/Riots/Generic");

	public static readonly FungusBlockReference Labour_Office_AfterRiots = new FungusBlockReference("Labour Office/AfterRiots");

	public static readonly FungusBlockReference Labour_Office_WorkCheck = new FungusBlockReference("Labour Office/WorkCheck");

	public static readonly FungusBlockReference Dispenser_EmptyCheck = new FungusBlockReference("Dispenser/EmptyCheck");

	public static readonly FungusBlockReference Labour_Office_Return = new FungusBlockReference("Labour Office/Return");

	public static readonly FungusBlockReference Dispenser_Return = new FungusBlockReference("Dispenser/Return");

	public static readonly FungusBlockReference Shop_Return = new FungusBlockReference("Shop/Return");

	public static readonly FungusBlockReference Pharmacy_Return = new FungusBlockReference("Pharmacy/Return");

	public static readonly FungusBlockReference Outside_Clothier_Riots1 = new FungusBlockReference("Outside/Clothier/Riots1");

	public static readonly FungusBlockReference Outside_Clothier_RiotsGeneric = new FungusBlockReference("Outside/Clothier/RiotsGeneric");

	public static readonly FungusBlockReference Outside_Clothier_Riots2 = new FungusBlockReference("Outside/Clothier/Riots2");

	public static readonly FungusBlockReference Outside_Clothier_AfterRiots = new FungusBlockReference("Outside/Clothier/AfterRiots");

	public static readonly FungusBlockReference Outside_Clothier_Return = new FungusBlockReference("Outside/Clothier/Return");

	public static readonly FungusBlockReference Outside_Clothier_AfterRiots2 = new FungusBlockReference("Outside/Clothier/AfterRiots2");

	public static readonly FungusBlockReference Outside_Clothier_AfterRiotsNeverWent = new FungusBlockReference("Outside/Clothier/AfterRiotsNeverWent");

	public static readonly FungusBlockReference Church_AfterStory_Riots1 = new FungusBlockReference("Church/AfterStory/Riots1");

	public static readonly FungusBlockReference Church_AfterStory_Riots3 = new FungusBlockReference("Church/AfterStory/Riots3");

	public static readonly FungusBlockReference Sleep_SleepTogetherEvent = new FungusBlockReference("Sleep/SleepTogetherEvent");

	public static readonly FungusBlockReference Outside_Clothier_AfterRiotsWithBotNeverWent = new FungusBlockReference("Outside/Clothier/AfterRiotsWithBotNeverWent");

	public static readonly FungusBlockReference Outside_Clothier_AfterRiotsWithBotHelped = new FungusBlockReference("Outside/Clothier/AfterRiotsWithBotHelped");

	public static readonly FungusBlockReference Outside_Clothier_AfterRiotsWithBotWentAlone = new FungusBlockReference("Outside/Clothier/AfterRiotsWithBotWentAlone");

	public static readonly FungusBlockReference Outside_Clothier_ReturnWithBot = new FungusBlockReference("Outside/Clothier/ReturnWithBot");

	public static readonly FungusBlockReference Church_AfterStory_RiotsGeneric = new FungusBlockReference("Church/AfterStory/RiotsGeneric");

	public static readonly FungusBlockReference Church_AfterStory_RiotsGeneric_Label_Generic1 = new FungusBlockReference("Church/AfterStory/RiotsGeneric", "Generic1");

	public static readonly FungusBlockReference Church_AfterStory_RiotsGeneric_Label_Generic2 = new FungusBlockReference("Church/AfterStory/RiotsGeneric", "Generic2");

	public static readonly FungusBlockReference Church_AfterStory_RiotsGeneric_Label_Generic3 = new FungusBlockReference("Church/AfterStory/RiotsGeneric", "Generic3");

	public static readonly FungusBlockReference Church_AfterStory_RiotsGeneric_Label_Generic4 = new FungusBlockReference("Church/AfterStory/RiotsGeneric", "Generic4");

	public static readonly FungusBlockReference Church_AfterStory_RiotsGeneric_Label_End = new FungusBlockReference("Church/AfterStory/RiotsGeneric", "End");

	public static readonly FungusBlockReference Church_ShowNun = new FungusBlockReference("Church/ShowNun");

	public static readonly FungusBlockReference Church_AfterStory_AfterRiotsNeverWent = new FungusBlockReference("Church/AfterStory/AfterRiotsNeverWent");

	public static readonly FungusBlockReference Church_AfterStory_AfterRiotsSavedPriestbot = new FungusBlockReference("Church/AfterStory/AfterRiotsSavedPriestbot");

	public static readonly FungusBlockReference Outside_Riots_FirstWalk = new FungusBlockReference("Outside/Riots/FirstWalk");

	public static readonly FungusBlockReference Outside_RiotsReturnToWalk = new FungusBlockReference("Outside/RiotsReturnToWalk");

	public static readonly FungusBlockReference Outside_Riots_WalkGeneric = new FungusBlockReference("Outside/Riots/WalkGeneric");

	public static readonly FungusBlockReference Outside_Riots_WalkGeneric_Label_generic1 = new FungusBlockReference("Outside/Riots/WalkGeneric", "generic1");

	public static readonly FungusBlockReference Outside_Riots_WalkGeneric_Label_generic2 = new FungusBlockReference("Outside/Riots/WalkGeneric", "generic2");

	public static readonly FungusBlockReference Outside_Riots_WalkGeneric_Label_generic3 = new FungusBlockReference("Outside/Riots/WalkGeneric", "generic3");

	public static readonly FungusBlockReference Outside_Riots_WalkGeneric_Label_generic4 = new FungusBlockReference("Outside/Riots/WalkGeneric", "generic4");

	public static readonly FungusBlockReference Outside_Riots_WalkGeneric_Label_generic5 = new FungusBlockReference("Outside/Riots/WalkGeneric", "generic5");

	public static readonly FungusBlockReference Outside_Riots_WalkGeneric_Label_generic6 = new FungusBlockReference("Outside/Riots/WalkGeneric", "generic6");

	public static readonly FungusBlockReference Outside_Riots_WalkGeneric_Label_generic7 = new FungusBlockReference("Outside/Riots/WalkGeneric", "generic7");

	public static readonly FungusBlockReference Outside_Riots_WalkGeneric_Label_generic8 = new FungusBlockReference("Outside/Riots/WalkGeneric", "generic8");

	public static readonly FungusBlockReference Outside_Riots_ChurchRiots2 = new FungusBlockReference("Outside/Riots/ChurchRiots2");

	public static readonly FungusBlockReference Outside_BdhReturn = new FungusBlockReference("Outside/BdhReturn");

	public static readonly FungusBlockReference Outside_Bigdaddystoryline_RiotsFirst = new FungusBlockReference("Outside/Bigdaddystoryline/RiotsFirst");

	public static readonly FungusBlockReference Outside_Bigdaddystoryline_RiotsGeneric = new FungusBlockReference("Outside/Bigdaddystoryline/RiotsGeneric");

	public static readonly FungusBlockReference Outside_Bigdaddystoryline_RiotsGeneric_Label_Generic1 = new FungusBlockReference("Outside/Bigdaddystoryline/RiotsGeneric", "Generic1");

	public static readonly FungusBlockReference Outside_Bigdaddystoryline_RiotsGeneric_Label_Generic2 = new FungusBlockReference("Outside/Bigdaddystoryline/RiotsGeneric", "Generic2");

	public static readonly FungusBlockReference Church_AfterStory_AfterRiotsDidntSavePriestbot = new FungusBlockReference("Church/AfterStory/AfterRiotsDidntSavePriestbot");

	public static readonly FungusBlockReference Outside_AfterRiotsWithBot = new FungusBlockReference("Outside/AfterRiotsWithBot");

	public static readonly FungusBlockReference Stage3_CuddleFirst = new FungusBlockReference("Stage3/CuddleFirst");

	public static readonly FungusBlockReference Sleep_BotFullLeadSex = new FungusBlockReference("Sleep/BotFullLeadSex");

	public static readonly FungusBlockReference Outside_Melissa1 = new FungusBlockReference("Outside/Melissa1");

	public static readonly FungusBlockReference Shop_Melissa2 = new FungusBlockReference("Shop/Melissa2");

	public static readonly FungusBlockReference Shop_Melissa3 = new FungusBlockReference("Shop/Melissa3");

	public static readonly FungusBlockReference Shop_Melissa4 = new FungusBlockReference("Shop/Melissa4");

	public static readonly FungusBlockReference StaircaseAlone_Melissa4Landlord = new FungusBlockReference("StaircaseAlone/Melissa4Landlord");

	public static readonly FungusBlockReference Outside_Melissa4BigDaddy = new FungusBlockReference("Outside/Melissa4BigDaddy");

	public static readonly FungusBlockReference Stage3_Melissa4bot = new FungusBlockReference("Stage3/Melissa4bot");

	public static readonly FungusBlockReference Church_AfterStory_Melissa4church = new FungusBlockReference("Church/AfterStory/Melissa4church");

	public static readonly FungusBlockReference Outside_Clothier_Melissa4Annalie = new FungusBlockReference("Outside/Clothier/Melissa4Annalie");

	public static readonly FungusBlockReference Outside_Melissa4Hobo = new FungusBlockReference("Outside/Melissa4Hobo");

	public static readonly FungusBlockReference Shop_Melissa4Rejection = new FungusBlockReference("Shop/Melissa4Rejection");

	public static readonly FungusBlockReference Outside_Walk_Melissa5Normal = new FungusBlockReference("Outside/Walk/Melissa5Normal");

	public static readonly FungusBlockReference Outside_Walk_Melissa5Common = new FungusBlockReference("Outside/Walk/Melissa5Common");

	public static readonly FungusBlockReference Outside_Walk_Melissa5AfterRejection = new FungusBlockReference("Outside/Walk/Melissa5AfterRejection");

	public static readonly FungusBlockReference Stage3_Melissa5bot = new FungusBlockReference("Stage3/Melissa5bot");

	public static readonly FungusBlockReference Outside_Date = new FungusBlockReference("Outside/Date");

	public static readonly FungusBlockReference Stage3_churchHoboBots2 = new FungusBlockReference("Stage3/churchHoboBots2");

	public static readonly FungusBlockReference Outside_Friends = new FungusBlockReference("Outside/Friends");

	public static readonly FungusBlockReference Outside_IceSkating1 = new FungusBlockReference("Outside/IceSkating1");

	public static readonly FungusBlockReference Outside_IceSkating2Shanice = new FungusBlockReference("Outside/IceSkating2Shanice");

	public static readonly FungusBlockReference Outside_EndDate = new FungusBlockReference("Outside/EndDate");

	public static readonly FungusBlockReference Outside_Clothier_AnnalieCafe1 = new FungusBlockReference("Outside/Clothier/AnnalieCafe1");

	public static readonly FungusBlockReference Outside_AnnalieCafe2 = new FungusBlockReference("Outside/AnnalieCafe2");

	public static readonly FungusBlockReference Outside_AfterRiotsNotice = new FungusBlockReference("Outside/AfterRiotsNotice");

	public static readonly FungusBlockReference Outside_RiotsEndingSoonNotice = new FungusBlockReference("Outside/RiotsEndingSoonNotice");

	public static readonly FungusBlockReference Shop_MelissaGenericRejection = new FungusBlockReference("Shop/MelissaGenericRejection");

	public static readonly FungusBlockReference Shop_MelissaGenericRejection_Label_Normal1 = new FungusBlockReference("Shop/MelissaGenericRejection", "Normal1");

	public static readonly FungusBlockReference Shop_MelissaGenericRejection_Label_Normal2 = new FungusBlockReference("Shop/MelissaGenericRejection", "Normal2");

	public static readonly FungusBlockReference Shop_MelissaGenericRejection_Label_Normal3 = new FungusBlockReference("Shop/MelissaGenericRejection", "Normal3");

	public static readonly FungusBlockReference Shop_MelissaGenericRejection_Label_Morning = new FungusBlockReference("Shop/MelissaGenericRejection", "Morning");

	public static readonly FungusBlockReference Shop_MelissaGenericRejection_Label_Afternoon = new FungusBlockReference("Shop/MelissaGenericRejection", "Afternoon");

	public static readonly FungusBlockReference Shop_MelissaShopOpen = new FungusBlockReference("Shop/MelissaShopOpen");

	public static readonly FungusBlockReference Shop_MelissaGenericBefore = new FungusBlockReference("Shop/MelissaGenericBefore");

	public static readonly FungusBlockReference Shop_MelissaGenericBefore_Label_Normal1 = new FungusBlockReference("Shop/MelissaGenericBefore", "Normal1");

	public static readonly FungusBlockReference Shop_MelissaGenericBefore_Label_Normal2 = new FungusBlockReference("Shop/MelissaGenericBefore", "Normal2");

	public static readonly FungusBlockReference Shop_MelissaGenericBefore_Label_Normal3 = new FungusBlockReference("Shop/MelissaGenericBefore", "Normal3");

	public static readonly FungusBlockReference Shop_MelissaGenericMelissa5 = new FungusBlockReference("Shop/MelissaGenericMelissa5");

	public static readonly FungusBlockReference Shop_MelissaGenericMelissa5_Label_Normal = new FungusBlockReference("Shop/MelissaGenericMelissa5", "Normal");

	public static readonly FungusBlockReference Shop_MelissaGenericMelissa5_Label_Morning = new FungusBlockReference("Shop/MelissaGenericMelissa5", "Morning");

	public static readonly FungusBlockReference Shop_MelissaGenericMelissa5_Label_Afternoon = new FungusBlockReference("Shop/MelissaGenericMelissa5", "Afternoon");

	public static readonly FungusBlockReference Shop_MelissaGenericMelissa5_Label_Evening = new FungusBlockReference("Shop/MelissaGenericMelissa5", "Evening");

	public static readonly FungusBlockReference Shop_MelissaGenericMelissa5_Label_Night = new FungusBlockReference("Shop/MelissaGenericMelissa5", "Night");

	public static readonly FungusBlockReference Stage3_AfterRepairs = new FungusBlockReference("Stage3/AfterRepairs");

	public static readonly FungusBlockReference Outside_BowlingMelissa = new FungusBlockReference("Outside/BowlingMelissa");

	public static readonly FungusBlockReference Shop_Riots_Hungry2 = new FungusBlockReference("Shop/Riots/Hungry2");

	public static readonly FungusBlockReference Stage3_RiotsReporter = new FungusBlockReference("Stage3/RiotsReporter");

	public static readonly FungusBlockReference WakeUp_BotBringsFood = new FungusBlockReference("WakeUp/BotBringsFood");

	public static readonly FungusBlockReference Fishing_Start = new FungusBlockReference("Fishing/Start");

	public static readonly FungusBlockReference Outside_RichArea_Main = new FungusBlockReference("Outside/RichArea/Main");

	public static readonly FungusBlockReference Outside_RichArea_Return = new FungusBlockReference("Outside/RichArea/Return");

	public static readonly FungusBlockReference RichShop_Main = new FungusBlockReference("RichShop/Main");

	public static readonly FungusBlockReference RichShop_Return = new FungusBlockReference("RichShop/Return");

	public static readonly FungusBlockReference RichShop_FirstVisit = new FungusBlockReference("RichShop/FirstVisit");

	public static readonly FungusBlockReference RichShop_FirstShopEnterCommon = new FungusBlockReference("RichShop/FirstShopEnterCommon");

	public static readonly FungusBlockReference RichShop_VisitNoMembership = new FungusBlockReference("RichShop/VisitNoMembership");

	public static readonly FungusBlockReference RichShop_VisitWithMembership = new FungusBlockReference("RichShop/VisitWithMembership");

	public static readonly FungusBlockReference Item_BdhAdviceLingerieCatalogue = new FungusBlockReference("Item/BdhAdviceLingerieCatalogue");

	public static readonly FungusBlockReference Email_SlowmodeUnlocked = new FungusBlockReference("Email/SlowmodeUnlocked");

	public static readonly FungusBlockReference Email_Join_Us_Email_3 = new FungusBlockReference("Email/Join Us Email 3");

	public static readonly FungusBlockReference exclamationI_EmailEvents = new FungusBlockReference("!I/EmailEvents");

	public static readonly FungusBlockReference exclamationI_Random_Events = new FungusBlockReference("!I/Random Events");

	public static readonly FungusBlockReference RE_JoinUs3Richard = new FungusBlockReference("RE/JoinUs3Richard");

	public static readonly FungusBlockReference Email_Join_Us_Email_4 = new FungusBlockReference("Email/Join Us Email 4");

	public static readonly FungusBlockReference RE_JoinUs4Richard = new FungusBlockReference("RE/JoinUs4Richard");

	public static readonly FungusBlockReference Email_Join_Us_Email_5 = new FungusBlockReference("Email/Join Us Email 5");

	public static readonly FungusBlockReference RE_JoinUs5Richard = new FungusBlockReference("RE/JoinUs5Richard");

	public static readonly FungusBlockReference Email_Join_Us_Email_6 = new FungusBlockReference("Email/Join Us Email 6");

	public static readonly FungusBlockReference RE_JoinUs6Richard = new FungusBlockReference("RE/JoinUs6Richard");

	public static readonly FungusBlockReference RE_JoinUs6Mission = new FungusBlockReference("RE/JoinUs6Mission");

	public static readonly FungusBlockReference RE_JoinUs6MissionBailoutAfter = new FungusBlockReference("RE/JoinUs6MissionBailoutAfter");

	public static readonly FungusBlockReference RE_JoinUs6MissionShaniceAfter = new FungusBlockReference("RE/JoinUs6MissionShaniceAfter");

	public static readonly FungusBlockReference Email_Join_Us_Email_BigDaddy1 = new FungusBlockReference("Email/Join Us Email BigDaddy1");

	public static readonly FungusBlockReference RE_JoinUs7BigDaddy2 = new FungusBlockReference("RE/JoinUs7BigDaddy2");

	public static readonly FungusBlockReference Outside_JoinUs7BigDaddy3 = new FungusBlockReference("Outside/JoinUs7BigDaddy3");

	public static readonly FungusBlockReference Email_Join_Us_Email_BigDaddy4 = new FungusBlockReference("Email/Join Us Email BigDaddy4");

	public static readonly FungusBlockReference RE_JoinUs7BigDaddy5 = new FungusBlockReference("RE/JoinUs7BigDaddy5");

	public static readonly FungusBlockReference RE_JoinUs7BigDaddy6 = new FungusBlockReference("RE/JoinUs7BigDaddy6");

	public static readonly FungusBlockReference RE_JoinUs7BigDaddy7 = new FungusBlockReference("RE/JoinUs7BigDaddy7");

	public static readonly FungusBlockReference RE_JoinUs7BigDaddy8 = new FungusBlockReference("RE/JoinUs7BigDaddy8");

	public static readonly FungusBlockReference Email_Join_Us_Email_BigDaddy9 = new FungusBlockReference("Email/Join Us Email BigDaddy9");

	public static readonly FungusBlockReference Email_Join_Us_Email_Richard1 = new FungusBlockReference("Email/Join Us Email Richard1");

	public static readonly FungusBlockReference RE_JoinUs7Richard2 = new FungusBlockReference("RE/JoinUs7Richard2");

	public static readonly FungusBlockReference Outside_JoinUs7Richard3 = new FungusBlockReference("Outside/JoinUs7Richard3");

	public static readonly FungusBlockReference Email_Join_Us_Email_Richard4 = new FungusBlockReference("Email/Join Us Email Richard4");

	public static readonly FungusBlockReference RE_JoinUs7Richard5 = new FungusBlockReference("RE/JoinUs7Richard5");

	public static readonly FungusBlockReference RE_JunDateIndustrialPark = new FungusBlockReference("RE/JunDateIndustrialPark");

	public static readonly FungusBlockReference RE_JunStarGazing = new FungusBlockReference("RE/JunStarGazing");

	public static readonly FungusBlockReference Outside_Planetarium1 = new FungusBlockReference("Outside/Planetarium1");

	public static readonly FungusBlockReference RE_JunAmusementPark1 = new FungusBlockReference("RE/JunAmusementPark1");

	public static readonly FungusBlockReference Outside_AmusementPark2 = new FungusBlockReference("Outside/AmusementPark2");

	public static readonly FungusBlockReference RE_JunGokartDate1 = new FungusBlockReference("RE/JunGokartDate1");

	public static readonly FungusBlockReference Outside_FailedDateRequirements = new FungusBlockReference("Outside/FailedDateRequirements");

	public static readonly FungusBlockReference Return_Home_Housewife1 = new FungusBlockReference("Return Home/Housewife1");

	public static readonly FungusBlockReference Return_Home_Housewife2 = new FungusBlockReference("Return Home/Housewife2");

	public static readonly FungusBlockReference Return_Home_Housewife3 = new FungusBlockReference("Return Home/Housewife3");

	public static readonly FungusBlockReference Return_Home_Housewife4 = new FungusBlockReference("Return Home/Housewife4");

	public static readonly FungusBlockReference Return_Home_Housewife5 = new FungusBlockReference("Return Home/Housewife5");

	public static readonly FungusBlockReference Return_Home_Housewife6 = new FungusBlockReference("Return Home/Housewife6");

	public static readonly FungusBlockReference Outside_MeetingMrsFisher1 = new FungusBlockReference("Outside/MeetingMrsFisher1");

	public static readonly FungusBlockReference Item_Grenade = new FungusBlockReference("Item/Grenade");

	public static readonly FungusBlockReference Fishing_Fishing2 = new FungusBlockReference("Fishing/Fishing2");

	public static readonly FungusBlockReference Fisher_Start = new FungusBlockReference("Fisher/Start");

	public static readonly FungusBlockReference Fisher_Start_Label_BeforeMenu = new FungusBlockReference("Fisher/Start", "BeforeMenu");

	public static readonly FungusBlockReference Fisher_GenericGreeting = new FungusBlockReference("Fisher/GenericGreeting");

	public static readonly FungusBlockReference Fisher_GenericLeave = new FungusBlockReference("Fisher/GenericLeave");

	public static readonly FungusBlockReference Fisher_WrongTime = new FungusBlockReference("Fisher/WrongTime");

	public static readonly FungusBlockReference Fisher_Talk1 = new FungusBlockReference("Fisher/Talk1");

	public static readonly FungusBlockReference Fisher_Talk2 = new FungusBlockReference("Fisher/Talk2");

	public static readonly FungusBlockReference Fisher_Bluegill = new FungusBlockReference("Fisher/Bluegill");

	public static readonly FungusBlockReference Fisher_LargeMouthBassBig = new FungusBlockReference("Fisher/LargeMouthBassBig");

	public static readonly FungusBlockReference Fisher_LargeMouthBassMany = new FungusBlockReference("Fisher/LargeMouthBassMany");

	public static readonly FungusBlockReference Fisher_NorthernPikeBig = new FungusBlockReference("Fisher/NorthernPikeBig");

	public static readonly FungusBlockReference Fisher_YellowPerchBig = new FungusBlockReference("Fisher/YellowPerchBig");

	public static readonly FungusBlockReference Fisher_CatfishBig = new FungusBlockReference("Fisher/CatfishBig");

	public static readonly FungusBlockReference Fisher_CatfishMany = new FungusBlockReference("Fisher/CatfishMany");

	public static readonly FungusBlockReference Fisher_WalleyeBIg = new FungusBlockReference("Fisher/WalleyeBIg");

	public static readonly FungusBlockReference Fisher_CarpMany = new FungusBlockReference("Fisher/CarpMany");

	public static readonly FungusBlockReference Fisher_TilapiaHeavy = new FungusBlockReference("Fisher/TilapiaHeavy");

	public static readonly FungusBlockReference Fisher_RainbowTroutMany = new FungusBlockReference("Fisher/RainbowTroutMany");

	public static readonly FungusBlockReference Fisher_SmallMouthBassMany = new FungusBlockReference("Fisher/SmallMouthBassMany");

	public static readonly FungusBlockReference Fisher_BrownTroutBig = new FungusBlockReference("Fisher/BrownTroutBig");

	public static readonly FungusBlockReference Fisher_SalmonBig = new FungusBlockReference("Fisher/SalmonBig");

	public static readonly FungusBlockReference Fisher_RedSnapperBig = new FungusBlockReference("Fisher/RedSnapperBig");

	public static readonly FungusBlockReference Fisher_BlackSeaBassBig = new FungusBlockReference("Fisher/BlackSeaBassBig");

	public static readonly FungusBlockReference Fisher_HalibutBig = new FungusBlockReference("Fisher/HalibutBig");

	public static readonly FungusBlockReference Fisher_GoldenSalmonBig = new FungusBlockReference("Fisher/GoldenSalmonBig");

	public static readonly FungusBlockReference Fisher_MackrelMany = new FungusBlockReference("Fisher/MackrelMany");

	public static readonly FungusBlockReference Fisher_FlounderAny = new FungusBlockReference("Fisher/FlounderAny");

	public static readonly FungusBlockReference Fisher_FlounderBig = new FungusBlockReference("Fisher/FlounderBig");

	public static readonly FungusBlockReference Console_First = new FungusBlockReference("Console/First");

	public static readonly FungusBlockReference Console_InteractStart = new FungusBlockReference("Console/InteractStart");

	public static readonly FungusBlockReference Console_Interact1 = new FungusBlockReference("Console/Interact1");

	public static readonly FungusBlockReference Console_InteractGeneric = new FungusBlockReference("Console/InteractGeneric");

	public static readonly FungusBlockReference Console_Interact2 = new FungusBlockReference("Console/Interact2");

	public static readonly FungusBlockReference Console_Interact3 = new FungusBlockReference("Console/Interact3");

	public static readonly FungusBlockReference Console_Interact4 = new FungusBlockReference("Console/Interact4");

	public static readonly FungusBlockReference Console_Interact5 = new FungusBlockReference("Console/Interact5");

	public static readonly FungusBlockReference Console_Interactrandom1 = new FungusBlockReference("Console/Interactrandom1");

	public static readonly FungusBlockReference Console_Interactrandom2 = new FungusBlockReference("Console/Interactrandom2");

	public static readonly FungusBlockReference Console_Interactrandom3 = new FungusBlockReference("Console/Interactrandom3");

	public static readonly FungusBlockReference Console_Interactrandom4 = new FungusBlockReference("Console/Interactrandom4");

	public static readonly FungusBlockReference Console_Interactrandom5 = new FungusBlockReference("Console/Interactrandom5");

	public static readonly FungusBlockReference Console_Interactrandom7 = new FungusBlockReference("Console/Interactrandom7");

	public static readonly FungusBlockReference Console_Interactrandom6 = new FungusBlockReference("Console/Interactrandom6");

	public static readonly FungusBlockReference Stage3_UniqueAffection24 = new FungusBlockReference("Stage3/UniqueAffection24");

	public static readonly FungusBlockReference RE_ConsoleRevenge = new FungusBlockReference("RE/ConsoleRevenge");

	public static readonly FungusBlockReference Console_Interactrandom8 = new FungusBlockReference("Console/Interactrandom8");

	public static readonly FungusBlockReference Console_Interactrandom9 = new FungusBlockReference("Console/Interactrandom9");

	public static readonly FungusBlockReference Return_Home_Beans = new FungusBlockReference("Return Home/Beans");

	public static readonly FungusBlockReference Return_Home_SausageOnions = new FungusBlockReference("Return Home/SausageOnions");

	public static readonly FungusBlockReference Return_Home_DrinkTea = new FungusBlockReference("Return Home/DrinkTea");

	public static readonly FungusBlockReference Return_Home_Bread = new FungusBlockReference("Return Home/Bread");

	public static readonly FungusBlockReference Return_Home_DrinkHotChocolate = new FungusBlockReference("Return Home/DrinkHotChocolate");

	public static readonly FungusBlockReference Return_Home_OnionRings = new FungusBlockReference("Return Home/OnionRings");

	public static readonly FungusBlockReference Return_Home_DrinkCarrotJuice = new FungusBlockReference("Return Home/DrinkCarrotJuice");

	public static readonly FungusBlockReference Return_Home_SteakWithPotatoes = new FungusBlockReference("Return Home/SteakWithPotatoes");

	public static readonly FungusBlockReference Stage3_AnnalieHairstyle = new FungusBlockReference("Stage3/AnnalieHairstyle");

	public static readonly FungusBlockReference Outside_Clothier_HairstyleComment = new FungusBlockReference("Outside/Clothier/HairstyleComment");

	public static readonly FungusBlockReference Stage3_UniqueAffection23 = new FungusBlockReference("Stage3/UniqueAffection23");

	public static readonly FungusBlockReference Stage3_UniqueAffection25 = new FungusBlockReference("Stage3/UniqueAffection25");

	public static readonly FungusBlockReference Go_Home_With_Bot = new FungusBlockReference("Go Home With Bot");

	public static readonly FungusBlockReference Stage3_UniqueAffection26 = new FungusBlockReference("Stage3/UniqueAffection26");

	public static readonly FungusBlockReference Stage3_UniqueAffection27 = new FungusBlockReference("Stage3/UniqueAffection27");

	public static readonly FungusBlockReference Stage3_UniqueAffection28 = new FungusBlockReference("Stage3/UniqueAffection28");

	public static readonly FungusBlockReference StaircaseAlone_Bathroom1 = new FungusBlockReference("StaircaseAlone/Bathroom1");

	public static readonly FungusBlockReference StaircaseAlone_Bathroom2 = new FungusBlockReference("StaircaseAlone/Bathroom2");

	public static readonly FungusBlockReference RE_Bathroom3 = new FungusBlockReference("RE/Bathroom3");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric = new FungusBlockReference("RE/Rent/BeforeGeneric");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C1 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C1");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C2 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C2");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C3 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C3");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C4 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C4");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C5 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C5");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C6 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C6");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C7 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C7");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C8 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C8");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C9 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C9");

	public static readonly FungusBlockReference RE_Rent_BeforeGeneric_Label_C10 = new FungusBlockReference("RE/Rent/BeforeGeneric", "C10");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric = new FungusBlockReference("RE/Rent/AfterGeneric");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric_Label_C1 = new FungusBlockReference("RE/Rent/AfterGeneric", "C1");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric_Label_C2 = new FungusBlockReference("RE/Rent/AfterGeneric", "C2");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric_Label_C3 = new FungusBlockReference("RE/Rent/AfterGeneric", "C3");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric_Label_C4 = new FungusBlockReference("RE/Rent/AfterGeneric", "C4");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric_Label_C5 = new FungusBlockReference("RE/Rent/AfterGeneric", "C5");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric_Label_C6 = new FungusBlockReference("RE/Rent/AfterGeneric", "C6");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric_Label_C7 = new FungusBlockReference("RE/Rent/AfterGeneric", "C7");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric_Label_C8 = new FungusBlockReference("RE/Rent/AfterGeneric", "C8");

	public static readonly FungusBlockReference RE_Rent_AfterGeneric_Label_C9 = new FungusBlockReference("RE/Rent/AfterGeneric", "C9");

	public static readonly FungusBlockReference RE_Rent_End = new FungusBlockReference("RE/Rent/End");

	public static readonly FungusBlockReference RE_Rent_FirstAfterBotDiscovery = new FungusBlockReference("RE/Rent/FirstAfterBotDiscovery");

	public static readonly FungusBlockReference RE_Rent_FirstAfterBathroom = new FungusBlockReference("RE/Rent/FirstAfterBathroom");

	public static readonly FungusBlockReference RE_Rent_End2 = new FungusBlockReference("RE/Rent/End2");

	public static readonly FungusBlockReference RE_Rent_FirstDayLate = new FungusBlockReference("RE/Rent/FirstDayLate");

	public static readonly FungusBlockReference RE_Rent_FirstEvening = new FungusBlockReference("RE/Rent/FirstEvening");

	public static readonly FungusBlockReference RE_Rent_SecondDayLate = new FungusBlockReference("RE/Rent/SecondDayLate");

	public static readonly FungusBlockReference RE_Rent_FirstFishing = new FungusBlockReference("RE/Rent/FirstFishing");

	public static readonly FungusBlockReference RE_Rent_FirstAfterRiots = new FungusBlockReference("RE/Rent/FirstAfterRiots");

	public static readonly FungusBlockReference RE_Rent_LandlordDaughter2 = new FungusBlockReference("RE/Rent/LandlordDaughter2");

	public static readonly FungusBlockReference RE_Rent_GoodSoul = new FungusBlockReference("RE/Rent/GoodSoul");

	public static readonly FungusBlockReference RE_Rent_RichPeopleShop = new FungusBlockReference("RE/Rent/RichPeopleShop");

	public static readonly FungusBlockReference Stage3_SecondPaymentDayLateBotTalk = new FungusBlockReference("Stage3/SecondPaymentDayLateBotTalk");

	public static readonly FungusBlockReference RE_Rent_FirstAfterShadyLawyer = new FungusBlockReference("RE/Rent/FirstAfterShadyLawyer");

	public static readonly FungusBlockReference RE_ShadyLawyerTrickedLandlord = new FungusBlockReference("RE/ShadyLawyerTrickedLandlord");

	public static readonly FungusBlockReference Re_Rent_DayLateGeneric = new FungusBlockReference("Re/Rent/DayLateGeneric");

	public static readonly FungusBlockReference Bathroom_Bathroom4 = new FungusBlockReference("Bathroom/Bathroom4");

	public static readonly FungusBlockReference Bathroom_Generic = new FungusBlockReference("Bathroom/Generic");

	public static readonly FungusBlockReference Bathroom_Start = new FungusBlockReference("Bathroom/Start");

	public static readonly FungusBlockReference Outside_Melissa6 = new FungusBlockReference("Outside/Melissa6");

	public static readonly FungusBlockReference Outside_Clothier_BeforeEnter_Melissa7 = new FungusBlockReference("Outside/Clothier/BeforeEnter/Melissa7");

	public static readonly FungusBlockReference Outside_Clothier_Melissa6to7info = new FungusBlockReference("Outside/Clothier/Melissa6to7info");

	public static readonly FungusBlockReference RE_Melissa8 = new FungusBlockReference("RE/Melissa8");

	public static readonly FungusBlockReference Outside_Melissa9 = new FungusBlockReference("Outside/Melissa9");

	public static readonly FungusBlockReference RE_Melissa10 = new FungusBlockReference("RE/Melissa10");

	public static readonly FungusBlockReference Outside_Melissa11 = new FungusBlockReference("Outside/Melissa11");

	public static readonly FungusBlockReference Outside_Melissa12 = new FungusBlockReference("Outside/Melissa12");

	public static readonly FungusBlockReference RE_Melissa13 = new FungusBlockReference("RE/Melissa13");

	public static readonly FungusBlockReference Shop_Melissa14 = new FungusBlockReference("Shop/Melissa14");

	public static readonly FungusBlockReference Shop_NoMelissa = new FungusBlockReference("Shop/NoMelissa");

	public static readonly FungusBlockReference RE_Melissa15 = new FungusBlockReference("RE/Melissa15");

	public static readonly FungusBlockReference Outside_Melissa16 = new FungusBlockReference("Outside/Melissa16");

	public static readonly FungusBlockReference Outside_Melissa17 = new FungusBlockReference("Outside/Melissa17");

	public static readonly FungusBlockReference Outside_Clothier_Melissa18BotBlock = new FungusBlockReference("Outside/Clothier/Melissa18BotBlock");

	public static readonly FungusBlockReference Outside_Clothier_BeforeEnter_Melissa18 = new FungusBlockReference("Outside/Clothier/BeforeEnter/Melissa18");

	public static readonly FungusBlockReference Outside_Melissa19 = new FungusBlockReference("Outside/Melissa19");

	public static readonly FungusBlockReference RE_Melissa20 = new FungusBlockReference("RE/Melissa20");

	public static readonly FungusBlockReference Outside_Melissa21 = new FungusBlockReference("Outside/Melissa21");

	public static readonly FungusBlockReference Outside_Melissa21dot5 = new FungusBlockReference("Outside/Melissa21.5");

	public static readonly FungusBlockReference RE_Melissa22 = new FungusBlockReference("RE/Melissa22");

	public static readonly FungusBlockReference RE_Melissa23 = new FungusBlockReference("RE/Melissa23");

	public static readonly FungusBlockReference Outside_Clothier_Melissa24BotBlock = new FungusBlockReference("Outside/Clothier/Melissa24BotBlock");

	public static readonly FungusBlockReference Outside_Clothier_BeforeEnter_Melissa24 = new FungusBlockReference("Outside/Clothier/BeforeEnter/Melissa24");

	public static readonly FungusBlockReference RE_Melissa25 = new FungusBlockReference("RE/Melissa25");

	public static readonly FungusBlockReference Bathroom_BotShowerAlone = new FungusBlockReference("Bathroom/BotShowerAlone");

	public static readonly FungusBlockReference Bathroom_TogetherGeneric = new FungusBlockReference("Bathroom/TogetherGeneric");

	public static readonly FungusBlockReference Sex_After_CumOnHerGeneric = new FungusBlockReference("Sex/After/CumOnHerGeneric");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Start = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Start");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Start_Label_Bot1 = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Start", "Bot1");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Start_Label_Bot2 = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Start", "Bot2");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Start_Label_Bot3 = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Start", "Bot3");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Start_Label_Anon1 = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Start", "Anon1");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Start_Label_Anon2 = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Start", "Anon2");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Start_Label_Anon3 = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Start", "Anon3");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Start_Label_AnonCleanChoice = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Start", "AnonCleanChoice");

	public static readonly FungusBlockReference Sex_After_Rag = new FungusBlockReference("Sex/After/Rag");

	public static readonly FungusBlockReference Sex_End_Frick_Common_End = new FungusBlockReference("Sex/End Frick Common End");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Leave = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Leave");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Entry = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Entry");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_Rag = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/Rag");

	public static readonly FungusBlockReference StageAny_CleanWithARag = new FungusBlockReference("StageAny/CleanWithARag");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_LeaveGeneric = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/LeaveGeneric");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_Uninterested = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/LeaveGeneric", "Uninterested");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_Horny = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/LeaveGeneric", "Horny");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_VeryHorny = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/LeaveGeneric", "VeryHorny");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_UsuallyCleans = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/LeaveGeneric", "UsuallyCleans");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_UsuallyLeavesUninterested = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/LeaveGeneric", "UsuallyLeavesUninterested");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_UsuallyLeavesHorny = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/LeaveGeneric", "UsuallyLeavesHorny");

	public static readonly FungusBlockReference Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_END = new FungusBlockReference("Sex/After/CumOnHerGenericSmart/LeaveGeneric", "END");

	public static readonly FungusBlockReference Outside_Clothier_BeforeEnter_AnnalieESL1 = new FungusBlockReference("Outside/Clothier/BeforeEnter/AnnalieESL1");

	public static readonly FungusBlockReference Outside_Clothier_BeforeEnter_AnnalieESL2 = new FungusBlockReference("Outside/Clothier/BeforeEnter/AnnalieESL2");

	public static readonly FungusBlockReference Outside_AnnalieESL3 = new FungusBlockReference("Outside/AnnalieESL3");

	public static readonly FungusBlockReference Outside_Clothier_BeforeEnter_AnnalieESL4 = new FungusBlockReference("Outside/Clothier/BeforeEnter/AnnalieESL4");

	public static readonly FungusBlockReference Outside_AnnalieESL5 = new FungusBlockReference("Outside/AnnalieESL5");

	public static readonly FungusBlockReference Outside_Clothier_AnnalieESL6 = new FungusBlockReference("Outside/Clothier/AnnalieESL6");

	public static readonly FungusBlockReference Outside_AnnalieESL7 = new FungusBlockReference("Outside/AnnalieESL7");

	public static readonly FungusBlockReference Outside_Clothier_AnnalieESL7to8info = new FungusBlockReference("Outside/Clothier/AnnalieESL7to8info");

	public static readonly FungusBlockReference Outside_AnnalieESL8 = new FungusBlockReference("Outside/AnnalieESL8");

	public static readonly FungusBlockReference Outside_Clothier_AnnalieESL7to8infoWithbot = new FungusBlockReference("Outside/Clothier/AnnalieESL7to8infoWithbot");

	public static readonly FungusBlockReference Ending_AnnalieESL9 = new FungusBlockReference("Ending/AnnalieESL9");

	public static readonly FungusBlockReference Ending_Island = new FungusBlockReference("Ending/Island");

	public static readonly FungusBlockReference Ending_AfterGoodEndingContinue = new FungusBlockReference("Ending/AfterGoodEndingContinue");

	public static readonly FungusBlockReference StaircaseAlone_LandlordESL1 = new FungusBlockReference("StaircaseAlone/LandlordESL1");

	public static readonly FungusBlockReference StaircaseAlone_LandlordESL2 = new FungusBlockReference("StaircaseAlone/LandlordESL2");

	public static readonly FungusBlockReference StaircaseAlone_LandlordESL3 = new FungusBlockReference("StaircaseAlone/LandlordESL3");

	public static readonly FungusBlockReference StaircaseAlone_LandlordESL4 = new FungusBlockReference("StaircaseAlone/LandlordESL4");

	public static readonly FungusBlockReference StaircaseAlone_LandlordESL5 = new FungusBlockReference("StaircaseAlone/LandlordESL5");

	public static readonly FungusBlockReference Outside_LandlordESL6 = new FungusBlockReference("Outside/LandlordESL6");

	public static readonly FungusBlockReference Outside_IslandPrologue1 = new FungusBlockReference("Outside/IslandPrologue1");

	public static readonly FungusBlockReference Outside_IslandPrologue2 = new FungusBlockReference("Outside/IslandPrologue2");

	public static readonly FungusBlockReference Outside_IslandPrologue3 = new FungusBlockReference("Outside/IslandPrologue3");

	public static readonly FungusBlockReference Outside_IslandPrologue4_Generic = new FungusBlockReference("Outside/IslandPrologue4_Generic");

	public static readonly FungusBlockReference Ending_LandlordESL7 = new FungusBlockReference("Ending/LandlordESL7");

	public static readonly FungusBlockReference Ending_RevengeOfTheHobos = new FungusBlockReference("Ending/RevengeOfTheHobos");

	public static readonly FungusBlockReference Outside_ReturnToWalkWalkJustEnding = new FungusBlockReference("Outside/ReturnToWalkWalkJustEnding");

	public static readonly FungusBlockReference Sex_After_LandlordCockblock1 = new FungusBlockReference("Sex/After/LandlordCockblock1");

	public static readonly FungusBlockReference Sex_After_LandlordCockblock2 = new FungusBlockReference("Sex/After/LandlordCockblock2");

	public static readonly FungusBlockReference Sex_After_LandlordCockblockGeneric = new FungusBlockReference("Sex/After/LandlordCockblockGeneric");

	public static readonly FungusBlockReference Stage3_LandlordCockblock3 = new FungusBlockReference("Stage3/LandlordCockblock3");

	public static readonly FungusBlockReference Sex_After_LandlordCockblock3dot5 = new FungusBlockReference("Sex/After/LandlordCockblock3.5");

	public static readonly FungusBlockReference RE_LandlordCockblock4 = new FungusBlockReference("RE/LandlordCockblock4");

	public static readonly FungusBlockReference Outside_Clothier_LandlordCockblock5 = new FungusBlockReference("Outside/Clothier/LandlordCockblock5");

	public static readonly FungusBlockReference Outside_Clothier_LandlordCockblock6 = new FungusBlockReference("Outside/Clothier/LandlordCockblock6");

	public static readonly FungusBlockReference WakeUp_Housewife7 = new FungusBlockReference("WakeUp/Housewife7");

	public static readonly FungusBlockReference Return_Home_Housewife8 = new FungusBlockReference("Return Home/Housewife8");

	public static readonly FungusBlockReference Outside_Housewife9 = new FungusBlockReference("Outside/Housewife9");

	public static readonly FungusBlockReference Return_Home_Housewife10 = new FungusBlockReference("Return Home/Housewife10");

	public static readonly FungusBlockReference Stage3_UniqueLove6 = new FungusBlockReference("Stage3/UniqueLove6");

	public static readonly FungusBlockReference Stage3_UniqueLove7 = new FungusBlockReference("Stage3/UniqueLove7");

	public static readonly FungusBlockReference Stage3_UniqueLove8 = new FungusBlockReference("Stage3/UniqueLove8");

	public static readonly FungusBlockReference Stage3_UniqueLove9 = new FungusBlockReference("Stage3/UniqueLove9");

	public static readonly FungusBlockReference Stage3_UniqueLove10 = new FungusBlockReference("Stage3/UniqueLove10");

	public static readonly FungusBlockReference Stage3_UniqueLove11 = new FungusBlockReference("Stage3/UniqueLove11");

	public static readonly FungusBlockReference Return_Home_Housewife11 = new FungusBlockReference("Return Home/Housewife11");

	public static readonly FungusBlockReference Fisher_J_FishJoint1 = new FungusBlockReference("Fisher/J/FishJoint1");

	public static readonly FungusBlockReference Fisher_J_FishJoint1Win = new FungusBlockReference("Fisher/J/FishJoint1Win");

	public static readonly FungusBlockReference Fisher_J_EndFishJointCommon = new FungusBlockReference("Fisher/J/EndFishJointCommon");

	public static readonly FungusBlockReference Fisher_J_FishJoint1Repeat = new FungusBlockReference("Fisher/J/FishJoint1Repeat");

	public static readonly FungusBlockReference Fisher_StartFishJoint = new FungusBlockReference("Fisher/StartFishJoint");

	public static readonly FungusBlockReference Fisher_J_FishJoint2 = new FungusBlockReference("Fisher/J/FishJoint2");

	public static readonly FungusBlockReference Fisher_J_FishJoint2Repeat = new FungusBlockReference("Fisher/J/FishJoint2Repeat");

	public static readonly FungusBlockReference Fisher_J_FishJoint2Win = new FungusBlockReference("Fisher/J/FishJoint2Win");

	public static readonly FungusBlockReference Fisher_J_EndFishJointInRoom = new FungusBlockReference("Fisher/J/EndFishJointInRoom");

	public static readonly FungusBlockReference Fisher_Shop = new FungusBlockReference("Fisher/Shop");

	public static readonly FungusBlockReference Fisher_End = new FungusBlockReference("Fisher/End");

	public static readonly FungusBlockReference Fisher_J_FishJoint2dot5Priestbot = new FungusBlockReference("Fisher/J/FishJoint2.5Priestbot");

	public static readonly FungusBlockReference Fisher_J_FishJoint2dot5PriestbotRepeat = new FungusBlockReference("Fisher/J/FishJoint2.5PriestbotRepeat");

	public static readonly FungusBlockReference Fisher_J_FishJoint2dot5PriestbotWin = new FungusBlockReference("Fisher/J/FishJoint2.5PriestbotWin");

	public static readonly FungusBlockReference Fisher_J_FishJoint2dot5Common = new FungusBlockReference("Fisher/J/FishJoint2.5Common");

	public static readonly FungusBlockReference Fisher_J_FishJoint2dot5Shanice = new FungusBlockReference("Fisher/J/FishJoint2.5Shanice");

	public static readonly FungusBlockReference Fisher_J_FishJoint3 = new FungusBlockReference("Fisher/J/FishJoint3");

	public static readonly FungusBlockReference Fisher_J_FishJoint3Win = new FungusBlockReference("Fisher/J/FishJoint3Win");

	public static readonly FungusBlockReference Fisher_J_FishJoint3Repeat = new FungusBlockReference("Fisher/J/FishJoint3Repeat");

	public static readonly FungusBlockReference Fisher_J_FishJoint4 = new FungusBlockReference("Fisher/J/FishJoint4");

	public static readonly FungusBlockReference Fisher_J_FishJoint4Win = new FungusBlockReference("Fisher/J/FishJoint4Win");

	public static readonly FungusBlockReference Fisher_J_FishJoint4Repeat = new FungusBlockReference("Fisher/J/FishJoint4Repeat");

	public static readonly FungusBlockReference Fisher_J_FishJoint4RepeatCommonEnd = new FungusBlockReference("Fisher/J/FishJoint4RepeatCommonEnd");

	public static readonly FungusBlockReference Fisher_J_FishJoint5 = new FungusBlockReference("Fisher/J/FishJoint5");

	public static readonly FungusBlockReference Outside_FishJointWalk = new FungusBlockReference("Outside/FishJointWalk");

	public static readonly FungusBlockReference WakeUp_FishjointDream = new FungusBlockReference("WakeUp/FishjointDream");

	public static readonly FungusBlockReference Outside_FishJointEnding = new FungusBlockReference("Outside/FishJointEnding");

	public static readonly FungusBlockReference Stage3_UniqueLove12 = new FungusBlockReference("Stage3/UniqueLove12");

	public static readonly FungusBlockReference Outside_FishingDate1 = new FungusBlockReference("Outside/FishingDate1");

	public static readonly FungusBlockReference Outside_FishingDate2 = new FungusBlockReference("Outside/FishingDate2");

	public static readonly FungusBlockReference Outside_AquariumDate = new FungusBlockReference("Outside/AquariumDate");
}
