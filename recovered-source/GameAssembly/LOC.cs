using System;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public static class LOC
{
	public static class Blog
	{
		public const string TABLE_NAME = "Blog";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			14, 133, 176, 136, 85, 246, 148, 167, 155, 128,
			85, 250, 180, 143, 245, 128
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}
	}

	public static class Comments
	{
		public const string TABLE_NAME = "Comments";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			47, 65, 139, 176, 175, 102, 116, 246, 25, 212,
			137, 254, 143, 246, 194, 231
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}
	}

	public static class Common
	{
		public static class KeyID
		{
			public const long BigDaddyHurt = 280313597952L;

			public const long LadyAnnalie = 283585155072L;

			public const long Landlord = 285984296960L;

			public const long Jun = 6160727567896576L;

			public const long You = 8386212125347840L;

			public const long Bot = 8386212137930752L;

			public const long Policewoman = 189884893851684864L;

			public const long Policeman = 189884893902016512L;

			public const long Mom = 189884893906210816L;

			public const long Annoying_neighbor = 189884893906210817L;

			public const long Police_s_TTS_text_to_speech_ = 189884893906210818L;

			public const long Police_s_TTS = 189884893906210819L;

			public const long QQQ = 189884893910405120L;

			public const long QQQ2 = 189884893910405121L;

			public const long Thug = 189884893910405122L;

			public const long Mysterious_pink_man = 189884893910405123L;

			public const long Ecclesiasticalot_FF = 189884893914599424L;

			public const long Sexy_nun = 189884893914599425L;

			public const long _Nun__Shanice = 189884893914599426L;

			public const long Nun_Shanice = 189884893914599427L;

			public const long Priestcan = 189884893918793728L;

			public const long Nun = 189884893918793729L;

			public const long Shanice = 189884893918793730L;

			public const long Future_nun_Shanice = 189884893918793731L;

			public const long Priestbot = 189884893922988032L;

			public const long Ecclesiasticalot_FF_and_You = 189884893922988033L;

			public const long Goku_the_second = 189884893922988034L;

			public const long Bo = 189884893922988035L;

			public const long Nigevchenko = 189884893927182336L;

			public const long Jamie = 189884893927182337L;

			public const long Announcer = 189884893927182338L;

			public const long Some_woman = 189884893927182339L;

			public const long Dispenser = 189884893931376640L;

			public const long QQQ1 = 189884893931376641L;

			public const long Blaubleim_s_secretary = 189884893931376642L;

			public const long Blaubleim = 189884893931376643L;

			public const long dAdDy = 189884893935570944L;

			public const long yOU = 189884893935570945L;

			public const long Priestbot_Mark = 189884893935570947L;

			public const long Hobo = 189884893939765248L;

			public const long Annalie = 189884893939765250L;

			public const long You_and_Bot = 189884893939765251L;

			public const long Stranger = 189884893943959552L;

			public const long Stranger_1 = 189884893943959553L;

			public const long Stranger_2 = 189884893943959554L;

			public const long Stranger_3 = 189884893943959555L;

			public const long Some_little_girl = 189884893948153856L;

			public const long Girl_s_mother = 189884893948153857L;

			public const long Some_old_woman = 189884893948153858L;

			public const long Policemen_3_and_4 = 189884893948153859L;

			public const long Policeman_2 = 189884893948153860L;

			public const long Policewoman_and_Policewoman = 189884893952348161L;

			public const long QQQ_1 = 189884893952348162L;

			public const long QQQ_2 = 189884893952348163L;

			public const long QQQ_3 = 189884893956542464L;

			public const long JoshQ = 189884893956542465L;

			public const long Josh = 189884893956542466L;

			public const long Receptionist = 189884893956542467L;

			public const long Detective = 189884893960736768L;

			public const long Waiter = 189884893960736769L;

			public const long Mr_Ghost = 189884893960736770L;

			public const long Baseball_player = 189884893960736771L;

			public const long Death = 189884893964931072L;

			public const long Ghost = 189884893964931073L;

			public const long Busy_suit_guy = 189884893964931074L;

			public const long Some_old_lady = 189884893964931075L;

			public const long Ecclesiasticalot_FF_ = 189884893969125377L;

			public const long New_concierge = 189884893969125379L;

			public const long Concierge_Leonardo = 189884893973319680L;

			public const long Big_Daddy = 189884893973319681L;

			public const long Pharmacist = 197348848315654144L;

			public const long Some_female_clerk = 197348848403734528L;

			public const long Melissa = 207821852338282496L;

			public const long Leonardo = 207821852422168576L;

			public const long Cashier = 207821852422168577L;

			public const long News_Presenter = 230535899521212416L;

			public const long Reporter = 230535899592515584L;

			public const long Rioter = 230535899592515585L;

			public const long Shop_speaker = 296908734589820928L;

			public const long Rich_woman_in_a_pink_jacket = 296908734682095616L;

			public const long Rich_woman_with_overly_huge_boobs = 296908734682095617L;

			public const long Little_Richard = 319409216818241536L;

			public const long AbusedTenant = 319409216914710528L;

			public const long Mrsdot_Fisher = 326769732238696448L;

			public const long Staff = 326769732276445184L;

			public const long Staff_member = 326769732276445185L;

			public const long DAK = 326769732276445186L;

			public const long Admin = 326769732276445187L;

			public const long Lucy = 328532400263913472L;

			public const long Old_Lady = 328532400360382464L;

			public const long Young_woman = 328532400360382465L;

			public const long Amei = 471117888949837824L;

			public const long Real_Melissa = 471117889046306816L;

			public const long Jacobi_Edot_Wunderanwalt = 471117889046306817L;

			public const long Britannia = 479127667148316672L;

			public const long Unknown_woman = 479127667198648320L;

			public const long Random_woman = 479859701739151360L;

			public const long QQQ2and3 = 481727777795465216L;

			public const long QQQ3 = 481727777896128512L;

			public const long QQQ4 = 481727777896128513L;

			public const long QQQ5 = 481727777896128514L;

			public const long Old_Man = 481728203517321216L;

			public const long Recording = 542258247985258496L;
		}

		public static class TupleReferences
		{
			public static readonly LOS.TupleReference BigDaddyHurt = GetTupleReference(280313597952L);

			public static readonly LOS.TupleReference LadyAnnalie = GetTupleReference(283585155072L);

			public static readonly LOS.TupleReference Landlord = GetTupleReference(285984296960L);

			public static readonly LOS.TupleReference Jun = GetTupleReference(6160727567896576L);

			public static readonly LOS.TupleReference You = GetTupleReference(8386212125347840L);

			public static readonly LOS.TupleReference Bot = GetTupleReference(8386212137930752L);

			public static readonly LOS.TupleReference Policewoman = GetTupleReference(189884893851684864L);

			public static readonly LOS.TupleReference Policeman = GetTupleReference(189884893902016512L);

			public static readonly LOS.TupleReference Mom = GetTupleReference(189884893906210816L);

			public static readonly LOS.TupleReference Annoying_neighbor = GetTupleReference(189884893906210817L);

			public static readonly LOS.TupleReference Police_s_TTS_text_to_speech_ = GetTupleReference(189884893906210818L);

			public static readonly LOS.TupleReference Police_s_TTS = GetTupleReference(189884893906210819L);

			public static readonly LOS.TupleReference QQQ = GetTupleReference(189884893910405120L);

			public static readonly LOS.TupleReference QQQ2 = GetTupleReference(189884893910405121L);

			public static readonly LOS.TupleReference Thug = GetTupleReference(189884893910405122L);

			public static readonly LOS.TupleReference Mysterious_pink_man = GetTupleReference(189884893910405123L);

			public static readonly LOS.TupleReference Ecclesiasticalot_FF = GetTupleReference(189884893914599424L);

			public static readonly LOS.TupleReference Sexy_nun = GetTupleReference(189884893914599425L);

			public static readonly LOS.TupleReference _Nun__Shanice = GetTupleReference(189884893914599426L);

			public static readonly LOS.TupleReference Nun_Shanice = GetTupleReference(189884893914599427L);

			public static readonly LOS.TupleReference Priestcan = GetTupleReference(189884893918793728L);

			public static readonly LOS.TupleReference Nun = GetTupleReference(189884893918793729L);

			public static readonly LOS.TupleReference Shanice = GetTupleReference(189884893918793730L);

			public static readonly LOS.TupleReference Future_nun_Shanice = GetTupleReference(189884893918793731L);

			public static readonly LOS.TupleReference Priestbot = GetTupleReference(189884893922988032L);

			public static readonly LOS.TupleReference Ecclesiasticalot_FF_and_You = GetTupleReference(189884893922988033L);

			public static readonly LOS.TupleReference Goku_the_second = GetTupleReference(189884893922988034L);

			public static readonly LOS.TupleReference Bo = GetTupleReference(189884893922988035L);

			public static readonly LOS.TupleReference Nigevchenko = GetTupleReference(189884893927182336L);

			public static readonly LOS.TupleReference Jamie = GetTupleReference(189884893927182337L);

			public static readonly LOS.TupleReference Announcer = GetTupleReference(189884893927182338L);

			public static readonly LOS.TupleReference Some_woman = GetTupleReference(189884893927182339L);

			public static readonly LOS.TupleReference Dispenser = GetTupleReference(189884893931376640L);

			public static readonly LOS.TupleReference QQQ1 = GetTupleReference(189884893931376641L);

			public static readonly LOS.TupleReference Blaubleim_s_secretary = GetTupleReference(189884893931376642L);

			public static readonly LOS.TupleReference Blaubleim = GetTupleReference(189884893931376643L);

			public static readonly LOS.TupleReference dAdDy = GetTupleReference(189884893935570944L);

			public static readonly LOS.TupleReference yOU = GetTupleReference(189884893935570945L);

			public static readonly LOS.TupleReference Priestbot_Mark = GetTupleReference(189884893935570947L);

			public static readonly LOS.TupleReference Hobo = GetTupleReference(189884893939765248L);

			public static readonly LOS.TupleReference Annalie = GetTupleReference(189884893939765250L);

			public static readonly LOS.TupleReference You_and_Bot = GetTupleReference(189884893939765251L);

			public static readonly LOS.TupleReference Stranger = GetTupleReference(189884893943959552L);

			public static readonly LOS.TupleReference Stranger_1 = GetTupleReference(189884893943959553L);

			public static readonly LOS.TupleReference Stranger_2 = GetTupleReference(189884893943959554L);

			public static readonly LOS.TupleReference Stranger_3 = GetTupleReference(189884893943959555L);

			public static readonly LOS.TupleReference Some_little_girl = GetTupleReference(189884893948153856L);

			public static readonly LOS.TupleReference Girl_s_mother = GetTupleReference(189884893948153857L);

			public static readonly LOS.TupleReference Some_old_woman = GetTupleReference(189884893948153858L);

			public static readonly LOS.TupleReference Policemen_3_and_4 = GetTupleReference(189884893948153859L);

			public static readonly LOS.TupleReference Policeman_2 = GetTupleReference(189884893948153860L);

			public static readonly LOS.TupleReference Policewoman_and_Policewoman = GetTupleReference(189884893952348161L);

			public static readonly LOS.TupleReference QQQ_1 = GetTupleReference(189884893952348162L);

			public static readonly LOS.TupleReference QQQ_2 = GetTupleReference(189884893952348163L);

			public static readonly LOS.TupleReference QQQ_3 = GetTupleReference(189884893956542464L);

			public static readonly LOS.TupleReference JoshQ = GetTupleReference(189884893956542465L);

			public static readonly LOS.TupleReference Josh = GetTupleReference(189884893956542466L);

			public static readonly LOS.TupleReference Receptionist = GetTupleReference(189884893956542467L);

			public static readonly LOS.TupleReference Detective = GetTupleReference(189884893960736768L);

			public static readonly LOS.TupleReference Waiter = GetTupleReference(189884893960736769L);

			public static readonly LOS.TupleReference Mr_Ghost = GetTupleReference(189884893960736770L);

			public static readonly LOS.TupleReference Baseball_player = GetTupleReference(189884893960736771L);

			public static readonly LOS.TupleReference Death = GetTupleReference(189884893964931072L);

			public static readonly LOS.TupleReference Ghost = GetTupleReference(189884893964931073L);

			public static readonly LOS.TupleReference Busy_suit_guy = GetTupleReference(189884893964931074L);

			public static readonly LOS.TupleReference Some_old_lady = GetTupleReference(189884893964931075L);

			public static readonly LOS.TupleReference Ecclesiasticalot_FF_ = GetTupleReference(189884893969125377L);

			public static readonly LOS.TupleReference New_concierge = GetTupleReference(189884893969125379L);

			public static readonly LOS.TupleReference Concierge_Leonardo = GetTupleReference(189884893973319680L);

			public static readonly LOS.TupleReference Big_Daddy = GetTupleReference(189884893973319681L);

			public static readonly LOS.TupleReference Pharmacist = GetTupleReference(197348848315654144L);

			public static readonly LOS.TupleReference Some_female_clerk = GetTupleReference(197348848403734528L);

			public static readonly LOS.TupleReference Melissa = GetTupleReference(207821852338282496L);

			public static readonly LOS.TupleReference Leonardo = GetTupleReference(207821852422168576L);

			public static readonly LOS.TupleReference Cashier = GetTupleReference(207821852422168577L);

			public static readonly LOS.TupleReference News_Presenter = GetTupleReference(230535899521212416L);

			public static readonly LOS.TupleReference Reporter = GetTupleReference(230535899592515584L);

			public static readonly LOS.TupleReference Rioter = GetTupleReference(230535899592515585L);

			public static readonly LOS.TupleReference Shop_speaker = GetTupleReference(296908734589820928L);

			public static readonly LOS.TupleReference Rich_woman_in_a_pink_jacket = GetTupleReference(296908734682095616L);

			public static readonly LOS.TupleReference Rich_woman_with_overly_huge_boobs = GetTupleReference(296908734682095617L);

			public static readonly LOS.TupleReference Little_Richard = GetTupleReference(319409216818241536L);

			public static readonly LOS.TupleReference AbusedTenant = GetTupleReference(319409216914710528L);

			public static readonly LOS.TupleReference Mrsdot_Fisher = GetTupleReference(326769732238696448L);

			public static readonly LOS.TupleReference Staff = GetTupleReference(326769732276445184L);

			public static readonly LOS.TupleReference Staff_member = GetTupleReference(326769732276445185L);

			public static readonly LOS.TupleReference DAK = GetTupleReference(326769732276445186L);

			public static readonly LOS.TupleReference Admin = GetTupleReference(326769732276445187L);

			public static readonly LOS.TupleReference Lucy = GetTupleReference(328532400263913472L);

			public static readonly LOS.TupleReference Old_Lady = GetTupleReference(328532400360382464L);

			public static readonly LOS.TupleReference Young_woman = GetTupleReference(328532400360382465L);

			public static readonly LOS.TupleReference Amei = GetTupleReference(471117888949837824L);

			public static readonly LOS.TupleReference Real_Melissa = GetTupleReference(471117889046306816L);

			public static readonly LOS.TupleReference Jacobi_Edot_Wunderanwalt = GetTupleReference(471117889046306817L);

			public static readonly LOS.TupleReference Britannia = GetTupleReference(479127667148316672L);

			public static readonly LOS.TupleReference Unknown_woman = GetTupleReference(479127667198648320L);

			public static readonly LOS.TupleReference Random_woman = GetTupleReference(479859701739151360L);

			public static readonly LOS.TupleReference QQQ2and3 = GetTupleReference(481727777795465216L);

			public static readonly LOS.TupleReference QQQ3 = GetTupleReference(481727777896128512L);

			public static readonly LOS.TupleReference QQQ4 = GetTupleReference(481727777896128513L);

			public static readonly LOS.TupleReference QQQ5 = GetTupleReference(481727777896128514L);

			public static readonly LOS.TupleReference Old_Man = GetTupleReference(481728203517321216L);

			public static readonly LOS.TupleReference Recording = GetTupleReference(542258247985258496L);
		}

		public static class Sets
		{
		}

		public const string TABLE_NAME = "Common";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			234, 128, 153, 108, 192, 34, 4, 98, 40, 156,
			252, 17, 224, 46, 209, 16
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string BigDaddyHurt => GS(280313597952L);

		public static string LadyAnnalie => GS(283585155072L);

		public static string Landlord => GS(285984296960L);

		public static string Jun => GS(6160727567896576L);

		public static string You => GS(8386212125347840L);

		public static string Bot => GS(8386212137930752L);

		public static string Policewoman => GS(189884893851684864L);

		public static string Policeman => GS(189884893902016512L);

		public static string Mom => GS(189884893906210816L);

		public static string Annoying_neighbor => GS(189884893906210817L);

		public static string Police_s_TTS_text_to_speech_ => GS(189884893906210818L);

		public static string Police_s_TTS => GS(189884893906210819L);

		public static string QQQ => GS(189884893910405120L);

		public static string QQQ2 => GS(189884893910405121L);

		public static string Thug => GS(189884893910405122L);

		public static string Mysterious_pink_man => GS(189884893910405123L);

		public static string Ecclesiasticalot_FF => GS(189884893914599424L);

		public static string Sexy_nun => GS(189884893914599425L);

		public static string _Nun__Shanice => GS(189884893914599426L);

		public static string Nun_Shanice => GS(189884893914599427L);

		public static string Priestcan => GS(189884893918793728L);

		public static string Nun => GS(189884893918793729L);

		public static string Shanice => GS(189884893918793730L);

		public static string Future_nun_Shanice => GS(189884893918793731L);

		public static string Priestbot => GS(189884893922988032L);

		public static string Ecclesiasticalot_FF_and_You => GS(189884893922988033L);

		public static string Goku_the_second => GS(189884893922988034L);

		public static string Bo => GS(189884893922988035L);

		public static string Nigevchenko => GS(189884893927182336L);

		public static string Jamie => GS(189884893927182337L);

		public static string Announcer => GS(189884893927182338L);

		public static string Some_woman => GS(189884893927182339L);

		public static string Dispenser => GS(189884893931376640L);

		public static string QQQ1 => GS(189884893931376641L);

		public static string Blaubleim_s_secretary => GS(189884893931376642L);

		public static string Blaubleim => GS(189884893931376643L);

		public static string dAdDy => GS(189884893935570944L);

		public static string yOU => GS(189884893935570945L);

		public static string Priestbot_Mark => GS(189884893935570947L);

		public static string Hobo => GS(189884893939765248L);

		public static string Annalie => GS(189884893939765250L);

		public static string You_and_Bot => GS(189884893939765251L);

		public static string Stranger => GS(189884893943959552L);

		public static string Stranger_1 => GS(189884893943959553L);

		public static string Stranger_2 => GS(189884893943959554L);

		public static string Stranger_3 => GS(189884893943959555L);

		public static string Some_little_girl => GS(189884893948153856L);

		public static string Girl_s_mother => GS(189884893948153857L);

		public static string Some_old_woman => GS(189884893948153858L);

		public static string Policemen_3_and_4 => GS(189884893948153859L);

		public static string Policeman_2 => GS(189884893948153860L);

		public static string Policewoman_and_Policewoman => GS(189884893952348161L);

		public static string QQQ_1 => GS(189884893952348162L);

		public static string QQQ_2 => GS(189884893952348163L);

		public static string QQQ_3 => GS(189884893956542464L);

		public static string JoshQ => GS(189884893956542465L);

		public static string Josh => GS(189884893956542466L);

		public static string Receptionist => GS(189884893956542467L);

		public static string Detective => GS(189884893960736768L);

		public static string Waiter => GS(189884893960736769L);

		public static string Mr_Ghost => GS(189884893960736770L);

		public static string Baseball_player => GS(189884893960736771L);

		public static string Death => GS(189884893964931072L);

		public static string Ghost => GS(189884893964931073L);

		public static string Busy_suit_guy => GS(189884893964931074L);

		public static string Some_old_lady => GS(189884893964931075L);

		public static string Ecclesiasticalot_FF_ => GS(189884893969125377L);

		public static string New_concierge => GS(189884893969125379L);

		public static string Concierge_Leonardo => GS(189884893973319680L);

		public static string Big_Daddy => GS(189884893973319681L);

		public static string Pharmacist => GS(197348848315654144L);

		public static string Some_female_clerk => GS(197348848403734528L);

		public static string Melissa => GS(207821852338282496L);

		public static string Leonardo => GS(207821852422168576L);

		public static string Cashier => GS(207821852422168577L);

		public static string News_Presenter => GS(230535899521212416L);

		public static string Reporter => GS(230535899592515584L);

		public static string Rioter => GS(230535899592515585L);

		public static string Shop_speaker => GS(296908734589820928L);

		public static string Rich_woman_in_a_pink_jacket => GS(296908734682095616L);

		public static string Rich_woman_with_overly_huge_boobs => GS(296908734682095617L);

		public static string Little_Richard => GS(319409216818241536L);

		public static string AbusedTenant => GS(319409216914710528L);

		public static string Mrsdot_Fisher => GS(326769732238696448L);

		public static string Staff => GS(326769732276445184L);

		public static string Staff_member => GS(326769732276445185L);

		public static string DAK => GS(326769732276445186L);

		public static string Admin => GS(326769732276445187L);

		public static string Lucy => GS(328532400263913472L);

		public static string Old_Lady => GS(328532400360382464L);

		public static string Young_woman => GS(328532400360382465L);

		public static string Amei => GS(471117888949837824L);

		public static string Real_Melissa => GS(471117889046306816L);

		public static string Jacobi_Edot_Wunderanwalt => GS(471117889046306817L);

		public static string Britannia => GS(479127667148316672L);

		public static string Unknown_woman => GS(479127667198648320L);

		public static string Random_woman => GS(479859701739151360L);

		public static string QQQ2and3 => GS(481727777795465216L);

		public static string QQQ3 => GS(481727777896128512L);

		public static string QQQ4 => GS(481727777896128513L);

		public static string QQQ5 => GS(481727777896128514L);

		public static string Old_Man => GS(481728203517321216L);

		public static string Recording => GS(542258247985258496L);

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static LOS.TupleReference GetTupleReference(long entryId)
		{
			return new LOS.TupleReference("Common", entryId);
		}
	}

	public static class Contracts
	{
		public static class KeyID
		{
			public const long Luxe_Name = 712197667688448L;

			public const long Stilou_Name = 712225090048000L;

			public const long VolksAuto_Name = 712230022549504L;

			public const long Luxe_Description = 712485292085248L;

			public const long Stilou_Description = 712488009994240L;

			public const long VolksAuto_Description = 712490644017152L;

			public const long Monizmed_Description = 1029065012023296L;

			public const long Monizmed_Name = 1029062432526336L;

			public const long Diabete_Description = 1029102848839680L;

			public const long Cazino_Description = 1029148482867200L;

			public const long Cazino_Name = 1029146662539264L;

			public const long Diabete_Name = 1029101317918720L;

			public const long Luxe_Contract_First = 6115890206478336L;

			public const long Luxe_Contract_Gold1 = 6115934267641856L;

			public const long Luxe_Contract_Gold2 = 6115948675076096L;

			public const long Luxe_Contract_Gold3 = 6115961136353280L;

			public const long Luxe_Contract_Gold4 = 6116059962544128L;

			public const long Luxe_Contract_Generic1 = 6116097723863040L;

			public const long Luxe_Contract_Generic2 = 6116145702506496L;

			public const long Luxe_Contract_Generic3 = 6116160458067968L;

			public const long Stilou_Contract_First = 7677221678223360L;

			public const long Stilou_Contract_Gold1 = 7677260676861952L;

			public const long Stilou_Contract_Gold2 = 7677281950371840L;

			public const long Stilou_Contract_Gold3 = 7677301068009472L;

			public const long Stilou_Contract_Gold4 = 7677310958178304L;

			public const long Stilou_Contract_Generic1 = 7677352263684096L;

			public const long Stilou_Contract_Generic2 = 7677371230326784L;

			public const long Stilou_Contract_Generic3 = 7677378612301824L;

			public const long VolksAuto_Contract_First = 7678163236556800L;

			public const long VolksAuto_Contract_Gold1 = 7678500177580032L;

			public const long VolksAuto_Contract_Gold2 = 7678518015954944L;

			public const long VolksAuto_Contract_Gold3 = 7678525272100864L;

			public const long VolksAuto_Contract_Gold4 = 7678533153198080L;

			public const long VolksAuto_Contract_Generic1 = 7678544129691648L;

			public const long VolksAuto_Contract_Generic2 = 7678565411590144L;

			public const long VolksAuto_Contract_Generic3 = 7678574966214656L;

			public const long Monizmed_Contract_First = 7679147874586624L;

			public const long Monizmed_Contract_Gold1 = 7680147490476032L;

			public const long Monizmed_Contract_Gold2 = 7680167228870656L;

			public const long Monizmed_Contract_Gold3 = 7680178347970560L;

			public const long Monizmed_Contract_Gold4 = 7680187546079232L;

			public const long Monizmed_Contract_Generic1 = 7680199734726656L;

			public const long Monizmed_Contract_Generic2 = 7680219842220032L;

			public const long Monizmed_Contract_Generic3 = 7680229338124288L;

			public const long Diabete_Contract_First = 7680634038128640L;

			public const long Diabete_Contract_Gold1 = 7680890498846720L;

			public const long Diabete_Contract_Gold2 = 7680913437495296L;

			public const long Diabete_Contract_Gold3 = 7680921331175424L;

			public const long Diabete_Contract_Gold4 = 7680931892432896L;

			public const long Diabete_Contract_Generic1 = 7681003241738240L;

			public const long Diabete_Contract_Generic2 = 7681053569191936L;

			public const long Diabete_Contract_Generic3 = 7681060737257472L;

			public const long Cazino_Contract_First = 7681910272561152L;

			public const long Cazino_Contract_Gold1 = 7681939796267008L;

			public const long Cazino_Contract_Gold2 = 7681963632496640L;

			public const long Cazino_Contract_Gold3 = 7681974646738944L;

			public const long Cazino_Contract_Gold4 = 7681982070657024L;

			public const long Cazino_Contract_Generic1 = 7682001347678208L;

			public const long Cazino_Contract_Generic2 = 7682023808176128L;

			public const long Cazino_Contract_Generic3 = 7682032565882880L;
		}

		public static class TupleReferences
		{
			public static readonly LOS.TupleReference Luxe_Name = GetTupleReference(712197667688448L);

			public static readonly LOS.TupleReference Stilou_Name = GetTupleReference(712225090048000L);

			public static readonly LOS.TupleReference VolksAuto_Name = GetTupleReference(712230022549504L);

			public static readonly LOS.TupleReference Luxe_Description = GetTupleReference(712485292085248L);

			public static readonly LOS.TupleReference Stilou_Description = GetTupleReference(712488009994240L);

			public static readonly LOS.TupleReference VolksAuto_Description = GetTupleReference(712490644017152L);

			public static readonly LOS.TupleReference Monizmed_Description = GetTupleReference(1029065012023296L);

			public static readonly LOS.TupleReference Monizmed_Name = GetTupleReference(1029062432526336L);

			public static readonly LOS.TupleReference Diabete_Description = GetTupleReference(1029102848839680L);

			public static readonly LOS.TupleReference Cazino_Description = GetTupleReference(1029148482867200L);

			public static readonly LOS.TupleReference Cazino_Name = GetTupleReference(1029146662539264L);

			public static readonly LOS.TupleReference Diabete_Name = GetTupleReference(1029101317918720L);

			public static readonly LOS.TupleReference Luxe_Contract_First = GetTupleReference(6115890206478336L);

			public static readonly LOS.TupleReference Luxe_Contract_Gold1 = GetTupleReference(6115934267641856L);

			public static readonly LOS.TupleReference Luxe_Contract_Gold2 = GetTupleReference(6115948675076096L);

			public static readonly LOS.TupleReference Luxe_Contract_Gold3 = GetTupleReference(6115961136353280L);

			public static readonly LOS.TupleReference Luxe_Contract_Gold4 = GetTupleReference(6116059962544128L);

			public static readonly LOS.TupleReference Luxe_Contract_Generic1 = GetTupleReference(6116097723863040L);

			public static readonly LOS.TupleReference Luxe_Contract_Generic2 = GetTupleReference(6116145702506496L);

			public static readonly LOS.TupleReference Luxe_Contract_Generic3 = GetTupleReference(6116160458067968L);

			public static readonly LOS.TupleReference Stilou_Contract_First = GetTupleReference(7677221678223360L);

			public static readonly LOS.TupleReference Stilou_Contract_Gold1 = GetTupleReference(7677260676861952L);

			public static readonly LOS.TupleReference Stilou_Contract_Gold2 = GetTupleReference(7677281950371840L);

			public static readonly LOS.TupleReference Stilou_Contract_Gold3 = GetTupleReference(7677301068009472L);

			public static readonly LOS.TupleReference Stilou_Contract_Gold4 = GetTupleReference(7677310958178304L);

			public static readonly LOS.TupleReference Stilou_Contract_Generic1 = GetTupleReference(7677352263684096L);

			public static readonly LOS.TupleReference Stilou_Contract_Generic2 = GetTupleReference(7677371230326784L);

			public static readonly LOS.TupleReference Stilou_Contract_Generic3 = GetTupleReference(7677378612301824L);

			public static readonly LOS.TupleReference VolksAuto_Contract_First = GetTupleReference(7678163236556800L);

			public static readonly LOS.TupleReference VolksAuto_Contract_Gold1 = GetTupleReference(7678500177580032L);

			public static readonly LOS.TupleReference VolksAuto_Contract_Gold2 = GetTupleReference(7678518015954944L);

			public static readonly LOS.TupleReference VolksAuto_Contract_Gold3 = GetTupleReference(7678525272100864L);

			public static readonly LOS.TupleReference VolksAuto_Contract_Gold4 = GetTupleReference(7678533153198080L);

			public static readonly LOS.TupleReference VolksAuto_Contract_Generic1 = GetTupleReference(7678544129691648L);

			public static readonly LOS.TupleReference VolksAuto_Contract_Generic2 = GetTupleReference(7678565411590144L);

			public static readonly LOS.TupleReference VolksAuto_Contract_Generic3 = GetTupleReference(7678574966214656L);

			public static readonly LOS.TupleReference Monizmed_Contract_First = GetTupleReference(7679147874586624L);

			public static readonly LOS.TupleReference Monizmed_Contract_Gold1 = GetTupleReference(7680147490476032L);

			public static readonly LOS.TupleReference Monizmed_Contract_Gold2 = GetTupleReference(7680167228870656L);

			public static readonly LOS.TupleReference Monizmed_Contract_Gold3 = GetTupleReference(7680178347970560L);

			public static readonly LOS.TupleReference Monizmed_Contract_Gold4 = GetTupleReference(7680187546079232L);

			public static readonly LOS.TupleReference Monizmed_Contract_Generic1 = GetTupleReference(7680199734726656L);

			public static readonly LOS.TupleReference Monizmed_Contract_Generic2 = GetTupleReference(7680219842220032L);

			public static readonly LOS.TupleReference Monizmed_Contract_Generic3 = GetTupleReference(7680229338124288L);

			public static readonly LOS.TupleReference Diabete_Contract_First = GetTupleReference(7680634038128640L);

			public static readonly LOS.TupleReference Diabete_Contract_Gold1 = GetTupleReference(7680890498846720L);

			public static readonly LOS.TupleReference Diabete_Contract_Gold2 = GetTupleReference(7680913437495296L);

			public static readonly LOS.TupleReference Diabete_Contract_Gold3 = GetTupleReference(7680921331175424L);

			public static readonly LOS.TupleReference Diabete_Contract_Gold4 = GetTupleReference(7680931892432896L);

			public static readonly LOS.TupleReference Diabete_Contract_Generic1 = GetTupleReference(7681003241738240L);

			public static readonly LOS.TupleReference Diabete_Contract_Generic2 = GetTupleReference(7681053569191936L);

			public static readonly LOS.TupleReference Diabete_Contract_Generic3 = GetTupleReference(7681060737257472L);

			public static readonly LOS.TupleReference Cazino_Contract_First = GetTupleReference(7681910272561152L);

			public static readonly LOS.TupleReference Cazino_Contract_Gold1 = GetTupleReference(7681939796267008L);

			public static readonly LOS.TupleReference Cazino_Contract_Gold2 = GetTupleReference(7681963632496640L);

			public static readonly LOS.TupleReference Cazino_Contract_Gold3 = GetTupleReference(7681974646738944L);

			public static readonly LOS.TupleReference Cazino_Contract_Gold4 = GetTupleReference(7681982070657024L);

			public static readonly LOS.TupleReference Cazino_Contract_Generic1 = GetTupleReference(7682001347678208L);

			public static readonly LOS.TupleReference Cazino_Contract_Generic2 = GetTupleReference(7682023808176128L);

			public static readonly LOS.TupleReference Cazino_Contract_Generic3 = GetTupleReference(7682032565882880L);
		}

		public static class Sets
		{
		}

		public const string TABLE_NAME = "Contracts";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			130, 101, 24, 5, 149, 69, 116, 196, 57, 146,
			8, 214, 153, 241, 191, 182
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string Luxe_Name => GS(712197667688448L);

		public static string Stilou_Name => GS(712225090048000L);

		public static string VolksAuto_Name => GS(712230022549504L);

		public static string Luxe_Description => GS(712485292085248L);

		public static string Stilou_Description => GS(712488009994240L);

		public static string VolksAuto_Description => GS(712490644017152L);

		public static string Monizmed_Description => GS(1029065012023296L);

		public static string Monizmed_Name => GS(1029062432526336L);

		public static string Diabete_Description => GS(1029102848839680L);

		public static string Cazino_Description => GS(1029148482867200L);

		public static string Cazino_Name => GS(1029146662539264L);

		public static string Diabete_Name => GS(1029101317918720L);

		public static string Luxe_Contract_First => GS(6115890206478336L);

		public static string Luxe_Contract_Gold3 => GS(6115961136353280L);

		public static string Luxe_Contract_Gold4 => GS(6116059962544128L);

		public static string Luxe_Contract_Generic1 => GS(6116097723863040L);

		public static string Luxe_Contract_Generic2 => GS(6116145702506496L);

		public static string Luxe_Contract_Generic3 => GS(6116160458067968L);

		public static string Stilou_Contract_First => GS(7677221678223360L);

		public static string Stilou_Contract_Gold1 => GS(7677260676861952L);

		public static string Stilou_Contract_Gold2 => GS(7677281950371840L);

		public static string Stilou_Contract_Gold3 => GS(7677301068009472L);

		public static string Stilou_Contract_Gold4 => GS(7677310958178304L);

		public static string Stilou_Contract_Generic1 => GS(7677352263684096L);

		public static string Stilou_Contract_Generic2 => GS(7677371230326784L);

		public static string Stilou_Contract_Generic3 => GS(7677378612301824L);

		public static string VolksAuto_Contract_First => GS(7678163236556800L);

		public static string VolksAuto_Contract_Gold1 => GS(7678500177580032L);

		public static string VolksAuto_Contract_Gold2 => GS(7678518015954944L);

		public static string VolksAuto_Contract_Gold3 => GS(7678525272100864L);

		public static string VolksAuto_Contract_Gold4 => GS(7678533153198080L);

		public static string VolksAuto_Contract_Generic1 => GS(7678544129691648L);

		public static string VolksAuto_Contract_Generic2 => GS(7678565411590144L);

		public static string VolksAuto_Contract_Generic3 => GS(7678574966214656L);

		public static string Monizmed_Contract_First => GS(7679147874586624L);

		public static string Monizmed_Contract_Gold1 => GS(7680147490476032L);

		public static string Monizmed_Contract_Gold2 => GS(7680167228870656L);

		public static string Monizmed_Contract_Gold3 => GS(7680178347970560L);

		public static string Monizmed_Contract_Gold4 => GS(7680187546079232L);

		public static string Monizmed_Contract_Generic1 => GS(7680199734726656L);

		public static string Monizmed_Contract_Generic2 => GS(7680219842220032L);

		public static string Monizmed_Contract_Generic3 => GS(7680229338124288L);

		public static string Diabete_Contract_First => GS(7680634038128640L);

		public static string Diabete_Contract_Gold1 => GS(7680890498846720L);

		public static string Diabete_Contract_Gold2 => GS(7680913437495296L);

		public static string Diabete_Contract_Gold3 => GS(7680921331175424L);

		public static string Diabete_Contract_Gold4 => GS(7680931892432896L);

		public static string Diabete_Contract_Generic1 => GS(7681003241738240L);

		public static string Diabete_Contract_Generic2 => GS(7681053569191936L);

		public static string Diabete_Contract_Generic3 => GS(7681060737257472L);

		public static string Cazino_Contract_First => GS(7681910272561152L);

		public static string Cazino_Contract_Gold1 => GS(7681939796267008L);

		public static string Cazino_Contract_Gold2 => GS(7681963632496640L);

		public static string Cazino_Contract_Gold3 => GS(7681974646738944L);

		public static string Cazino_Contract_Gold4 => GS(7681982070657024L);

		public static string Cazino_Contract_Generic1 => GS(7682001347678208L);

		public static string Cazino_Contract_Generic2 => GS(7682023808176128L);

		public static string Cazino_Contract_Generic3 => GS(7682032565882880L);

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string Luxe_Contract_Gold1(object arg0)
		{
			return GS(6115934267641856L, arg0);
		}

		public static string Luxe_Contract_Gold2(object arg0)
		{
			return GS(6115948675076096L, arg0);
		}

		public static LOS.TupleReference GetTupleReference(long entryId)
		{
			return new LOS.TupleReference("Contracts", entryId);
		}
	}

	public static class Dialogue
	{
		public const string TABLE_NAME = "Dialogue";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			90, 27, 213, 49, 113, 185, 4, 186, 248, 209,
			189, 136, 46, 69, 195, 182
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}
	}

	public static class Emails
	{
		public const string TABLE_NAME = "Emails";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			37, 149, 31, 218, 41, 121, 36, 209, 185, 168,
			34, 245, 25, 17, 219, 238
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}
	}

	public static class Items
	{
		public const string TABLE_NAME = "Items";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			208, 61, 29, 251, 87, 187, 196, 153, 154, 120,
			36, 90, 224, 11, 33, 103
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}
	}

	public static class Moddable
	{
		public const string TABLE_NAME = "Moddable";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			138, 147, 36, 7, 243, 232, 116, 24, 203, 3,
			22, 203, 119, 33, 104, 218
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}
	}

	public static class News
	{
		public const string TABLE_NAME = "News";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			149, 19, 185, 252, 44, 152, 36, 75, 57, 194,
			193, 236, 176, 48, 15, 139
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}
	}

	public static class Other
	{
		public static class KeyID
		{
			public const long Generator_color = 1709149392896L;

			public const long Generator_femalename = 1709170364416L;

			public const long Generator_country = 1709170364417L;

			public const long Generator_country2 = 1709170364418L;

			public const long Generator_bodypart = 1709170364419L;

			public const long Generator_animegirl = 1709170364420L;

			public const long Generator_animeboy = 1709170364421L;

			public const long Generator_celebrity = 1709170364422L;

			public const long Generator_company = 1709170364423L;

			public const long Generator_animal = 1709170364424L;

			public const long Generator_UScity = 1709170364425L;

			public const long Generator_terroristorganisation = 1709170364426L;

			public const long Generator_sexuality = 1709170364427L;

			public const long Generator_sexuality2 = 1709170364428L;

			public const long Generator_deathcause = 1709170364429L;

			public const long Generator_art = 1709170364430L;

			public const long Generator_disease = 1709170364431L;

			public const long Generator_name = 1709170364432L;

			public const long Achievement_ending_ballsexploded = 652887969370112L;

			public const long Achievement_ending_goodEnd1 = 652887981953024L;

			public const long Achievement_ending_badEnd1 = 652887981953025L;

			public const long Achievement_ending_badEnd2 = 652887981953026L;

			public const long Achievement_ending_badEnd3 = 652887981953027L;

			public const long Achievement_ending_badEnd4dot1 = 652887981953028L;

			public const long Achievement_ending_badEnd4dot2 = 652887981953029L;

			public const long Achievement_ending_healthEnd = 652887981953030L;

			public const long Achievement_ending_fedEnd = 652887981953031L;

			public const long Achievement_ending_schizoEnd = 652887986147328L;

			public const long Achievement_ending_genericSchizoEnd = 652887986147329L;

			public const long Achievement_church_endSpotkanie2B = 652887986147330L;

			public const long Achievement_church_endTheWorstEnd = 652887986147331L;

			public const long Achievement_church_endIdontknow = 652887986147332L;

			public const long Achievement_church_endJustGoThereGood = 652887986147333L;

			public const long Achievement_church_endJustGoThereBad = 652887986147334L;

			public const long Achievement_saveEdited = 652887986147335L;

			public const long Shop_StatusAnnalie = 3155255940222976L;

			public const long Shop_NotReadyEndings = 3155252333121536L;

			public const long Shop_ReadyEndings = 3155250466656256L;

			public const long Outside_CantGo = 3158103419654144L;

			public const long Outside_TooOften = 3158230012137472L;

			public const long Outside_Info = 3158417979871232L;

			public const long Outside_VeryDangerous = 3158477727731712L;

			public const long Outside_Dangerous = 3158572758077440L;

			public const long Outside_Medium = 3158597827432448L;

			public const long Outside_AlmostSafe = 3158634041053184L;

			public const long Outside_Safe = 3158679217901568L;

			public const long Outside_NoPanties = 3158785157632000L;

			public const long Outside_AllLimbs = 3159199408066560L;

			public const long Outside_UpperBody = 3159271713673216L;

			public const long Outside_LowerBody = 3159334548541440L;

			public const long Outside_Shoes = 3159388130775040L;

			public const long Equipment_Generic_0 = 4598202804080640L;

			public const long Equipment_Generic_1 = 4598202820857856L;

			public const long Equipment_Generic_2 = 4598202820857857L;

			public const long Equipment_Generic_3 = 4598920667598848L;

			public const long Equipment_Generic_4 = 4598920667598849L;

			public const long Equipment_Generic_5 = 4598920667598850L;

			public const long Equipment_Generic_6 = 4598920667598851L;

			public const long Equipment_Generic_7 = 4598920667598852L;

			public const long Equipment_Generic_8 = 4598920667598853L;

			public const long Equipment_Generic_9 = 4598920667598854L;

			public const long Equipment_Generic_10 = 4598920667598855L;

			public const long Equipment_Generic_11 = 4598920667598856L;

			public const long Equipment_Generic_12 = 4598920667598857L;

			public const long Equipment_Generic_13 = 4598920667598858L;

			public const long Equipment_Generic_14 = 4598920667598859L;

			public const long Equipment_Generic_15 = 4598920667598860L;

			public const long Equipment_Generic_16 = 4598920667598861L;

			public const long Equipment_Generic_17 = 4598920667598862L;

			public const long Equipment_Generic_18 = 4598920667598863L;

			public const long Equipment_Generic_19 = 4598920667598864L;

			public const long Equipment_Generic_20 = 4598920667598865L;

			public const long Equipment_Generic_21 = 4598920667598866L;

			public const long Equipment_Generic_22 = 4598920667598867L;

			public const long Equipment_Generic_23 = 4598920667598868L;

			public const long Equipment_Shirt_0 = 4604487419170816L;

			public const long Equipment_StripedPanties_0 = 4604615798427648L;

			public const long Equipment_Stocking_0 = 4604730810437632L;

			public const long Equipment_Lingerie_0 = 4604807767527424L;

			public const long Equipment_LongSock_0 = 4604903447990272L;

			public const long Equipment_CuteBow_0 = 4604997891133440L;

			public const long Equipment_PleatedSkirt_0 = 4605154791657472L;

			public const long Equipment_PlainBra_0 = 4605066920988672L;

			public const long Equipment_PleatedSkirt_1 = 4605229781618688L;

			public const long Equipment_PlainSneakers_0 = 4605354394390528L;

			public const long Equipment_Sneakers_0 = 4605485478973440L;

			public const long Equipment_Panties_0 = 4605579670458368L;

			public const long Equipment_Panties_1 = 4605603133394944L;

			public const long BotDialogueStage3_23000 = 5719102916005888L;

			public const long BotDialogueStage3_23001 = 5719102928588800L;

			public const long BotDialogueStage3_23002 = 5719102928588801L;

			public const long BotDialogueStage3_24000 = 5719102928588802L;

			public const long BotDialogueStage3_24001 = 5719102928588803L;

			public const long BotDialogueStage3_24002 = 5719102928588804L;

			public const long BotDialogueStage3_26000 = 5719102928588805L;

			public const long BotDialogueStage3_26001 = 5719102928588806L;

			public const long BotDialogueStage3_26002 = 5719102928588807L;

			public const long BotDialogueStage3_27000 = 5719102928588808L;

			public const long BotDialogueStage3_27001 = 5719102928588809L;

			public const long BotDialogueStage3_27002 = 5719102928588810L;

			public const long BotDialogueStage3_28000 = 5719102928588811L;

			public const long BotDialogueStage3_28001 = 5719102928588812L;

			public const long BotDialogueStage3_28002 = 5719102928588813L;

			public const long BotDialogueStage3_29000 = 5719102928588814L;

			public const long BotDialogueStage3_29001 = 5719102928588815L;

			public const long BotDialogueStage3_30000 = 5719102928588816L;

			public const long BotDialogueStage3_30001 = 5719102928588817L;

			public const long BotDialogueStage3_30002 = 5719102928588818L;

			public const long BotDialogueStage3_31000 = 5719102928588819L;

			public const long BotDialogueStage3_31001 = 5719102928588820L;

			public const long BotDialogueStage3_31002 = 5719102928588821L;

			public const long BotDialogueStage3_32000 = 5719102928588822L;

			public const long BotDialogueStage3_32001 = 5719102928588823L;

			public const long BotDialogueStage3_32002 = 5719102928588824L;

			public const long BotDialogueStage3_33000 = 5719102928588825L;

			public const long BotDialogueStage3_33001 = 5719102928588826L;

			public const long BotDialogueStage3_34000 = 5719102928588827L;

			public const long BotDialogueStage3_34001 = 5719102928588828L;

			public const long BotDialogueStage3_34002 = 5719102928588829L;

			public const long BotDialogueStage3_35000 = 5719102928588830L;

			public const long BotDialogueStage3_35001 = 5719102928588831L;

			public const long BotDialogueStage3_35002 = 5719102928588832L;

			public const long ColorName_JunsGrey = 6480075763355648L;

			public const long ColorName_JunsMagenta = 6480075767549952L;

			public const long ColorName_JunsPupils = 6480075767549953L;

			public const long ColorName_JunsIrises = 6480075767549954L;

			public const long ColorName_DefaultSkin = 6480075767549955L;

			public const long ColorName_JunsFlesh = 6480075767549956L;

			public const long ColorName_JunsBlush = 6480075767549957L;

			public const long ColorName_OldPlainWhite = 6480075767549958L;

			public const long ColorName_OldSadGrey = 6480075767549959L;

			public const long ColorName_OldCuteMagenta = 6480075767549960L;

			public const long ColorName_OldUglyBlue = 6480075771744256L;

			public const long ColorName_OldDreamyCyan = 6480075771744257L;

			public const long ColorName_OldGrassyGreen = 6480075771744258L;

			public const long ColorName_OldFakeGold = 6480075771744259L;

			public const long ColorName_OldBloodyRed = 6480075771744260L;

			public const long ColorName_OldSpecialTurquoise = 6480075771744261L;

			public const long ColorName_OldToxicGreen = 6480075771744262L;

			public const long ColorName_OldKitsunePiku = 6480075771744263L;

			public const long ColorName_OldAsphalt = 6480075771744264L;

			public const long ColorName_White = 6480075771744265L;

			public const long ColorName_LightGrey = 6480075771744266L;

			public const long ColorName_Grey = 6480075771744267L;

			public const long ColorName_DarkGrey = 6480075771744268L;

			public const long ColorName_VeryDarkGrey = 6480075771744269L;

			public const long ColorName_UnicornGalaxy = 6480075771744270L;

			public const long ColorName_CuteMagenta = 6480075771744271L;

			public const long ColorName_StrawberyMilk = 6480075771744272L;

			public const long ColorName_ToxicPurple = 6480075771744273L;

			public const long ColorName_MidnightBlue = 6480075771744274L;

			public const long ColorName_BasicBlack = 6480075771744275L;

			public const long ColorName_BasicWhite = 6480075771744276L;

			public const long ColorName_BloodyRed = 6480075771744277L;

			public const long ColorName_Miku = 6480075771744278L;

			public const long ColorName_FakeGold = 6480075771744279L;

			public const long ColorName_DreamyCyan = 6480075771744280L;

			public const long ColorName_UglyBlue = 6480075771744281L;

			public const long ColorName_EBPurple = 6480075771744282L;

			public const long ColorName_EBBlue = 6480075771744283L;

			public const long ColorName_EBGreen = 6480075771744284L;

			public const long ColorName_EBYellow = 6480075771744285L;

			public const long ColorName_EBOrange = 6480075771744286L;

			public const long ColorName_EBRed = 6480075771744287L;

			public const long ColorName_Custom = 6484980532121600L;

			public const long Blowjob_SwallowFull = 36649044804399104L;

			public const long Blowjob_SwallowHalf = 36649314556866560L;

			public const long Blowjob_SwallowLittle = 36649699321344000L;

			public const long Blowjob_SwallowAlmostEmpty = 36650295650709504L;

			public const long Blowjob_EdgingForceStop = 36651023119183872L;

			public const long Blowjob_EdgingAlmostCame = 36651290724167680L;

			public const long Blowjob_EdgingHadToSlowdown = 36651869752029184L;

			public const long Blowjob_EdgingOn = 36658999078973440L;

			public const long Blowjob_EdgingOff = 36659004787421184L;

			public const long Game_OldMaid_StartingWithQueen = 37005369136672768L;

			public const long Game_OldMaid_StartingWithoutQueen = 37005500103815168L;

			public const long Game_OldMaid_Waiting = 37006170395537408L;

			public const long Game_OldMaid_PlayerPickingStartWithQueen = 37006206491717632L;

			public const long Game_OldMaid_PlayerPickingStartWithoutQueen = 37006398297239552L;

			public const long Game_OldMaid_PlayerPickingDoesntMatter = 37006908236525568L;

			public const long Game_OldMaid_PlayerPickedQueenAfterLying = 37007086569943040L;

			public const long Game_OldMaid_PlayerPickedQueen = 37007252152676352L;

			public const long Game_OldMaid_PlayerPickedCardOtherThanQueenWhileBotHasQueen = 37007384810123264L;

			public const long Game_OldMaid_BotPickingCardsHover = 37007703203934208L;

			public const long Game_OldMaid_BotPickingCardsChosen = 37007761286656000L;

			public const long Game_OldMaid_BotChooseQueen = 37007916232634368L;

			public const long Game_OldMaid_BotChooseOtherWhilePlayerHasQueen = 37007950978248704L;

			public const long Game_OldMaid_PlayerWon = 37008621588738048L;

			public const long Game_OldMaid_PlayerLost = 37008665297580032L;

			public const long Game_OldMaid_Summary = 37008920160268288L;

			public const long Game_ThanksForPlaying = 37018473102864384L;

			public const long Game_OldMaid_HintDont = 37018886514438144L;

			public const long Game_OldMaid_HintDo = 37018961298878464L;

			public const long Game_OldMaid_HintNoInfo = 37019204346212352L;

			public const long Game_OldMaid_PlayerLeft = 37246397311643648L;

			public const long Game_OldMaid_Summary_Left = 37247700263792640L;

			public const long Achievement_ending_KilledByADirtyCop1 = 38074455498297344L;

			public const long Achievement_ending_KilledByADirtyCop2 = 38074638151847936L;

			public const long Game_OldMaid_StartGamePlayer = 38333175650193408L;

			public const long Game_OldMaid_StartGameBot = 38333588281626624L;

			public const long MentalHealth_HeadacheHigh = 40262055420104704L;

			public const long MentalHealth_HeadacheLow = 40262059543105536L;

			public const long VeryHungry_HighHealth = 40261798590287872L;

			public const long VeryHungry_MediumHealth = 40261805439586304L;

			public const long VeryHungry_LowHealth = 40261821143060480L;

			public const long MediumHungry_HighHealth = 40261838666862592L;

			public const long MediumHungry_MediumHealth = 40261840285863936L;

			public const long MediumHungry_LowHealth = 40261841682567168L;

			public const long LittleHungry_HighHealth = 40261887589224448L;

			public const long LittleHungry_LowHealth = 40261890986610688L;

			public const long LittleHungry_MediumHealth = 40261889208225792L;

			public const long LostMoney = 46677620225347584L;

			public const long GotSweets = 46801134785794048L;

			public const long StreamEventRequirement_CumInside = 78731296074764288L;

			public const long StreamEventRequirement_CumOutside = 78731297593102336L;

			public const long StreamEventRequirement_CumInsideAnal = 78731298868170752L;

			public const long StreamEventRequirement_CumInsideMouth = 78731300428451840L;

			public const long StreamEventRequirement_GotFlacid = 78731935706124288L;

			public const long StreamEventRequirement_Insert = 78738233873010688L;

			public const long StreamEventRequirement_Pullout = 78738256622915584L;

			public const long Sender_Unknown = 79021807289372672L;

			public const long Delivery_StatusToBeCollected = 79040422487134208L;

			public const long Delivery_StatusToBeSorted = 79040500077563904L;

			public const long Delivery_DeliveryDispatched = 79040503714025472L;

			public const long Delivery_DeliverySoon = 79040651068313600L;

			public const long Delivery_PredictedTime = 79040864239620096L;

			public const long Delivery_PredictedTimeSoon = 79041449756069888L;

			public const long Sender_BigDaddyHurt = 79046713855053824L;

			public const long Sender_Multiple = 79368941532483584L;

			public const long MentalHealth_DeathDoor = 91351293984903168L;

			public const long WhatTheHell = 109890604040589312L;

			public const long IAmScared = 109893380904718336L;

			public const long ColorName_VolksAutoGrey = 110467144243613696L;

			public const long GotItem = 111561705527316480L;

			public const long ColorName_NavyGreen = 117395373416124416L;

			public const long ColorName_TempleCyan = 118217092112453632L;

			public const long ColorName_AvocadoGreen = 118217092112453633L;

			public const long ColorName_LunarPrincessGrey = 118217092112453634L;

			public const long ColorName_SatinBlack = 118217092112453635L;

			public const long ColorName_StilouBlue = 118217092112453636L;

			public const long ColorName_StilouRed = 118217092112453637L;

			public const long ColorName_StilouYellow = 118217092112453638L;

			public const long ColorName_MonizmedBurgundy = 118217092112453639L;

			public const long ColorName_MonizmedPurple = 118217092112453640L;

			public const long ColorName_DiabetePurple = 118217092112453641L;

			public const long ColorName_DiabeteGold = 118217092112453642L;

			public const long ColorName_DiabeteColaRed = 118217092112453643L;

			public const long ColorName_DiabeteColaYellow = 118217092112453644L;

			public const long ColorName_DiabeteColaBlue = 118217092112453645L;

			public const long ColorName_DiabeteDrSugarRed = 118217092112453646L;

			public const long ColorName_DiabeteDrSugarBlue = 118217092112453647L;

			public const long ColorName_DiabeteSweetPotatoBrown = 118217092112453648L;

			public const long ColorName_DiabeteSweetPotatoOrange = 118217092112453649L;

			public const long ColorName_CazinoGreen = 118217092112453650L;

			public const long ColorName_CazinoBrown = 118217092112453651L;

			public const long Cowgirl_RemovePants = 120033781170966528L;

			public const long Cowgirl_BeforeInsert = 120034458442010624L;

			public const long Cowgirl_BeforeInsertBotLead = 120034827842752512L;

			public const long Cowgirl_CumInMouth = 120039386887622656L;

			public const long Cowgirl_CumInside = 120039449936400384L;

			public const long Cowgirl_CumOutside = 120039509310967808L;

			public const long Cowgirl_DidntDecide = 120040555219390464L;

			public const long Frick_CumOutside = 120043369240858624L;

			public const long Cowgirl_CumQuestion = 120045205435846656L;

			public const long Cowgirl_EdgeVeryClose = 120136478058483712L;

			public const long NunRepair_EquipedItem = 120730844321943552L;

			public const long NunRepair_Denied = 120731892331712512L;

			public const long NunRepair_DeniedNoMoney = 120732450169950208L;

			public const long NunRepair_Busy = 120733919916986368L;

			public const long NunRepair_RepairResult = 120739286138884096L;

			public const long NunRepair_RepairStarted = 120742247099404288L;

			public const long ColorName_Black = 121122169009606656L;

			public const long Achievement_ending_theyKnowOblivious = 121130260094652416L;

			public const long Achievement_ending_theyKnow = 121130260115623936L;

			public const long Achievement_bigdaddyhurtSecretEpilogue = 121152291804680192L;

			public const long Cowgirl_CumTogether = 122058373351739392L;

			public const long Cowgirl_EdgeForceStopped = 122691757786664960L;

			public const long Cowgirl_FlacidReaction = 122719261100478464L;

			public const long Cowgirl_EdgingBreak = 122729071132938240L;

			public const long Frick_BotCum = 122925642265006080L;

			public const long Cowgirl_CumAlone = 122685814826995712L;

			public const long Stage2Headpat1 = 188398410854699008L;

			public const long Stage2Headpat2 = 188398413132206080L;

			public const long Stage2Headpat3 = 188398414898008064L;

			public const long Word_Brother = 192453224328728576L;

			public const long Word_Sister = 192453258243870720L;

			public const long Outside_RiotsDanger = 196436563699163136L;

			public const long Fishing_PollutedLakeName = 268423275278753792L;

			public const long Fishing_PollutedLakeDescription = 268423666603122688L;

			public const long Fishing_TrashedRiverName = 268424045873061888L;

			public const long Fishing_TrashedRiverDescription = 268424115800498176L;

			public const long Fishing_PremiumFishFarmName = 268424601735782400L;

			public const long Fishing_PremiumFishFarmDescription = 268424603933597696L;

			public const long Fishing_MicroplasticsCoastName = 272492651078475776L;

			public const long Fishing_MicroplasticsCoastDescription = 272492655176310784L;

			public const long ColorName_Bastille = 276056677704261632L;

			public const long ColorName_Mirage = 276056677775564800L;

			public const long ColorName_Toledo = 276056677821702144L;

			public const long ColorName_Bunker = 276056677872033792L;

			public const long ColorName_HippiePink = 276056677918171136L;

			public const long ColorName_LinkWater = 276056677964308480L;

			public const long ColorName_TurquoiseBlue = 276056678014640128L;

			public const long ColorName_Amethyst = 276056678060777472L;

			public const long ColorName_Fern = 276056678111109120L;

			public const long ColorName_DarkPink = 276056678157246464L;

			public const long Sender_FishingWebsite = 326943858014281728L;

			public const long CowGayMeter = 290361684091985920L;

			public const long FoodSatiation_FullMeal = 296384348640239616L;

			public const long FoodSatiation_BarelyABite = 296384402218278912L;

			public const long FoodSatiation_Meal = 296384984555446272L;

			public const long FoodSatiation_Snack = 296385004788768768L;

			public const long FoodSatiation_LightMeal = 296384997876555776L;

			public const long ColorName_EbonyClay = 301479725122969600L;

			public const long ColorName_Rope = 301479725148135424L;

			public const long ColorName_Leather = 301479725148135425L;

			public const long ColorName_CopperRust = 301479725148135426L;

			public const long ColorName_MellowApricot = 301479725148135427L;

			public const long ColorName_InkBlue = 301479725148135428L;

			public const long ColorName_WeatheredGray = 301479725148135430L;

			public const long ColorName_DarkLeather = 301481769946832896L;

			public const long ColorName_BlackLeather = 301481769967804416L;

			public const long ColorPalette_OldPartPalette = 301524803753336832L;

			public const long ColorPalette_DefaultPartPalette = 301524803753336833L;

			public const long ColorPalette_GrayScalePalette = 301524803753336834L;

			public const long ColorPalette_JunPalette = 301524803753336835L;

			public const long ColorPalette_SkinPalette = 301524803753336836L;

			public const long ColorPalette_LeatherPalette = 301524803753336837L;

			public const long ColorPalette_OtherPalette = 301524803753336838L;

			public const long ColorPalette_HotPinkPalette = 301524803753336839L;

			public const long ColorPalette_XoulionMainPalette = 301524803753336840L;

			public const long ColorPalette_XoulionLogoPalette = 301524803753336841L;

			public const long ColorPalette_SkinTonePalette = 301567685663121408L;

			public const long ColorName_Burgundy = 301567685688287232L;

			public const long ColorName_DarkUmber = 301567685688287233L;

			public const long ColorName_Sienna = 301567685688287234L;

			public const long ColorName_Sandstone = 301567685688287235L;

			public const long ColorName_Almond = 301567685688287236L;

			public const long ColorName_LightPeach = 301567685688287237L;

			public const long ColorName_DeepMahogany = 301567685688287238L;

			public const long ColorName_Bronze = 301567685688287239L;

			public const long ColorName_GoldenHoney = 301567685688287240L;

			public const long ColorName_WarmIvory = 301567685688287241L;

			public const long ColorName_PalePorcelain = 301567685688287242L;

			public const long ColorName_GoblinGreen = 301567685688287243L;

			public const long ColorName_SoftClay = 301572741179367424L;

			public const long ColorName_Rosewood = 301572741179367425L;

			public const long ColorName_Tawny = 301572741179367426L;

			public const long ColorName_BurntSienna = 301572741179367427L;

			public const long ColorName_EarthBrown = 301572741179367428L;

			public const long ColorName_PaleDoomer = 301572741179367429L;

			public const long ColorName_AnnaliesViolet = 313837547580022784L;

			public const long Achievement_ending_unluckyEncounterWithLead = 321774208251060224L;

			public const long Achievement_ending_itWasntSupposedToEndLikeThis = 321818294404243456L;

			public const long Achievement_ending_friendsTillTheVeryEnd = 321823121737768960L;

			public const long Outside_DAKDanger = 321874011689574400L;

			public const long Achievement_ending_victimOfARevolution = 321883160968159232L;

			public const long Dish_AbsolutelyButchered = 326149039096586240L;

			public const long Dish_VeryBad = 326149122840059904L;

			public const long Dish_Bad = 326149139432726528L;

			public const long Dish_Average = 326149140951064576L;

			public const long Dish_Tasty = 326149142440042496L;

			public const long Dish_Delicious = 326149173121376256L;

			public const long Dish_Exquisite = 326149183456141312L;

			public const long Dish_Masterpiece = 326149200812171264L;

			public const long Dish_WorldClass = 326149218306613248L;

			public const long Achievement_ending_grenade = 328734652681412608L;

			public const long Report3File = 329484386803113984L;

			public const long Report2File = 329484473197387776L;

			public const long Report1File = 329485745719861248L;

			public const long JournalFile1 = 330996496531972096L;

			public const long JournalFile2 = 329503582547075072L;

			public const long JournalFile3 = 329503584661004288L;

			public const long Shower_AfterSex_CleanCum = 474009182340636672L;

			public const long Shower_AfterSex_LeaveItOn = 474009187306692608L;

			public const long Achievement_ending_islandAnnalie = 483386357271486464L;

			public const long Achievement_ending_islandLandlord = 483386357300846592L;

			public const long Achievement_ending_islandLandlordEveryone = 483386357300846593L;

			public const long Achievement_ending_revengeOfTheHomeless = 483717650156871680L;

			public const long Achievement_ending_revengeOfTheHomelessBeforeTheFlight = 483770983890542592L;

			public const long Fishing_ImaginarySeaName = 542204484809383936L;

			public const long Fishing_ImaginarySeaDescription = 542204545714872320L;

			public const long Achievement_ending_veryBadTrip = 544684971587002368L;

			public const long Achievement_badTripSavedByMelissa = 544684971612168192L;

			public const long Achievement_lastTripMetDad = 544944266295631872L;

			public const long Achievement_defeatedBotInFishingDuel = 544944266320797696L;

			public const long Achievement_helpedAnnalieDuringRiots = 544960584205983744L;

			public const long Achievement_helpedPristbotDuringRiots = 544960584210178048L;

			public const long Achievement_melissaFriendsDead = 544961927830298624L;

			public const long Achievement_melissaFriendsLeaves = 544961927834492928L;

			public const long Achievement_melissaDestory = 544961927834492929L;

			public const long Achievement_melissaLeave = 544961927834492930L;

			public const long Achievement_shower = 544964046893342720L;

			public const long Achievement_sleepingTogether = 544964046893342721L;

			public const long Achievement_runway_Landlord = 544964046893342722L;

			public const long Achievement_runway_Shanice = 544964046893342723L;

			public const long Achievement_runway_Fisher = 544964046893342724L;
		}

		public static class TupleReferences
		{
			public static readonly LOS.TupleReference Generator_color = GetTupleReference(1709149392896L);

			public static readonly LOS.TupleReference Generator_femalename = GetTupleReference(1709170364416L);

			public static readonly LOS.TupleReference Generator_country = GetTupleReference(1709170364417L);

			public static readonly LOS.TupleReference Generator_country2 = GetTupleReference(1709170364418L);

			public static readonly LOS.TupleReference Generator_bodypart = GetTupleReference(1709170364419L);

			public static readonly LOS.TupleReference Generator_animegirl = GetTupleReference(1709170364420L);

			public static readonly LOS.TupleReference Generator_animeboy = GetTupleReference(1709170364421L);

			public static readonly LOS.TupleReference Generator_celebrity = GetTupleReference(1709170364422L);

			public static readonly LOS.TupleReference Generator_company = GetTupleReference(1709170364423L);

			public static readonly LOS.TupleReference Generator_animal = GetTupleReference(1709170364424L);

			public static readonly LOS.TupleReference Generator_UScity = GetTupleReference(1709170364425L);

			public static readonly LOS.TupleReference Generator_terroristorganisation = GetTupleReference(1709170364426L);

			public static readonly LOS.TupleReference Generator_sexuality = GetTupleReference(1709170364427L);

			public static readonly LOS.TupleReference Generator_sexuality2 = GetTupleReference(1709170364428L);

			public static readonly LOS.TupleReference Generator_deathcause = GetTupleReference(1709170364429L);

			public static readonly LOS.TupleReference Generator_art = GetTupleReference(1709170364430L);

			public static readonly LOS.TupleReference Generator_disease = GetTupleReference(1709170364431L);

			public static readonly LOS.TupleReference Generator_name = GetTupleReference(1709170364432L);

			public static readonly LOS.TupleReference Achievement_ending_ballsexploded = GetTupleReference(652887969370112L);

			public static readonly LOS.TupleReference Achievement_ending_goodEnd1 = GetTupleReference(652887981953024L);

			public static readonly LOS.TupleReference Achievement_ending_badEnd1 = GetTupleReference(652887981953025L);

			public static readonly LOS.TupleReference Achievement_ending_badEnd2 = GetTupleReference(652887981953026L);

			public static readonly LOS.TupleReference Achievement_ending_badEnd3 = GetTupleReference(652887981953027L);

			public static readonly LOS.TupleReference Achievement_ending_badEnd4dot1 = GetTupleReference(652887981953028L);

			public static readonly LOS.TupleReference Achievement_ending_badEnd4dot2 = GetTupleReference(652887981953029L);

			public static readonly LOS.TupleReference Achievement_ending_healthEnd = GetTupleReference(652887981953030L);

			public static readonly LOS.TupleReference Achievement_ending_fedEnd = GetTupleReference(652887981953031L);

			public static readonly LOS.TupleReference Achievement_ending_schizoEnd = GetTupleReference(652887986147328L);

			public static readonly LOS.TupleReference Achievement_ending_genericSchizoEnd = GetTupleReference(652887986147329L);

			public static readonly LOS.TupleReference Achievement_church_endSpotkanie2B = GetTupleReference(652887986147330L);

			public static readonly LOS.TupleReference Achievement_church_endTheWorstEnd = GetTupleReference(652887986147331L);

			public static readonly LOS.TupleReference Achievement_church_endIdontknow = GetTupleReference(652887986147332L);

			public static readonly LOS.TupleReference Achievement_church_endJustGoThereGood = GetTupleReference(652887986147333L);

			public static readonly LOS.TupleReference Achievement_church_endJustGoThereBad = GetTupleReference(652887986147334L);

			public static readonly LOS.TupleReference Achievement_saveEdited = GetTupleReference(652887986147335L);

			public static readonly LOS.TupleReference Shop_StatusAnnalie = GetTupleReference(3155255940222976L);

			public static readonly LOS.TupleReference Shop_NotReadyEndings = GetTupleReference(3155252333121536L);

			public static readonly LOS.TupleReference Shop_ReadyEndings = GetTupleReference(3155250466656256L);

			public static readonly LOS.TupleReference Outside_CantGo = GetTupleReference(3158103419654144L);

			public static readonly LOS.TupleReference Outside_TooOften = GetTupleReference(3158230012137472L);

			public static readonly LOS.TupleReference Outside_Info = GetTupleReference(3158417979871232L);

			public static readonly LOS.TupleReference Outside_VeryDangerous = GetTupleReference(3158477727731712L);

			public static readonly LOS.TupleReference Outside_Dangerous = GetTupleReference(3158572758077440L);

			public static readonly LOS.TupleReference Outside_Medium = GetTupleReference(3158597827432448L);

			public static readonly LOS.TupleReference Outside_AlmostSafe = GetTupleReference(3158634041053184L);

			public static readonly LOS.TupleReference Outside_Safe = GetTupleReference(3158679217901568L);

			public static readonly LOS.TupleReference Outside_NoPanties = GetTupleReference(3158785157632000L);

			public static readonly LOS.TupleReference Outside_AllLimbs = GetTupleReference(3159199408066560L);

			public static readonly LOS.TupleReference Outside_UpperBody = GetTupleReference(3159271713673216L);

			public static readonly LOS.TupleReference Outside_LowerBody = GetTupleReference(3159334548541440L);

			public static readonly LOS.TupleReference Outside_Shoes = GetTupleReference(3159388130775040L);

			public static readonly LOS.TupleReference Equipment_Generic_0 = GetTupleReference(4598202804080640L);

			public static readonly LOS.TupleReference Equipment_Generic_1 = GetTupleReference(4598202820857856L);

			public static readonly LOS.TupleReference Equipment_Generic_2 = GetTupleReference(4598202820857857L);

			public static readonly LOS.TupleReference Equipment_Generic_3 = GetTupleReference(4598920667598848L);

			public static readonly LOS.TupleReference Equipment_Generic_4 = GetTupleReference(4598920667598849L);

			public static readonly LOS.TupleReference Equipment_Generic_5 = GetTupleReference(4598920667598850L);

			public static readonly LOS.TupleReference Equipment_Generic_6 = GetTupleReference(4598920667598851L);

			public static readonly LOS.TupleReference Equipment_Generic_7 = GetTupleReference(4598920667598852L);

			public static readonly LOS.TupleReference Equipment_Generic_8 = GetTupleReference(4598920667598853L);

			public static readonly LOS.TupleReference Equipment_Generic_9 = GetTupleReference(4598920667598854L);

			public static readonly LOS.TupleReference Equipment_Generic_10 = GetTupleReference(4598920667598855L);

			public static readonly LOS.TupleReference Equipment_Generic_11 = GetTupleReference(4598920667598856L);

			public static readonly LOS.TupleReference Equipment_Generic_12 = GetTupleReference(4598920667598857L);

			public static readonly LOS.TupleReference Equipment_Generic_13 = GetTupleReference(4598920667598858L);

			public static readonly LOS.TupleReference Equipment_Generic_14 = GetTupleReference(4598920667598859L);

			public static readonly LOS.TupleReference Equipment_Generic_15 = GetTupleReference(4598920667598860L);

			public static readonly LOS.TupleReference Equipment_Generic_16 = GetTupleReference(4598920667598861L);

			public static readonly LOS.TupleReference Equipment_Generic_17 = GetTupleReference(4598920667598862L);

			public static readonly LOS.TupleReference Equipment_Generic_18 = GetTupleReference(4598920667598863L);

			public static readonly LOS.TupleReference Equipment_Generic_19 = GetTupleReference(4598920667598864L);

			public static readonly LOS.TupleReference Equipment_Generic_20 = GetTupleReference(4598920667598865L);

			public static readonly LOS.TupleReference Equipment_Generic_21 = GetTupleReference(4598920667598866L);

			public static readonly LOS.TupleReference Equipment_Generic_22 = GetTupleReference(4598920667598867L);

			public static readonly LOS.TupleReference Equipment_Generic_23 = GetTupleReference(4598920667598868L);

			public static readonly LOS.TupleReference Equipment_Shirt_0 = GetTupleReference(4604487419170816L);

			public static readonly LOS.TupleReference Equipment_StripedPanties_0 = GetTupleReference(4604615798427648L);

			public static readonly LOS.TupleReference Equipment_Stocking_0 = GetTupleReference(4604730810437632L);

			public static readonly LOS.TupleReference Equipment_Lingerie_0 = GetTupleReference(4604807767527424L);

			public static readonly LOS.TupleReference Equipment_LongSock_0 = GetTupleReference(4604903447990272L);

			public static readonly LOS.TupleReference Equipment_CuteBow_0 = GetTupleReference(4604997891133440L);

			public static readonly LOS.TupleReference Equipment_PleatedSkirt_0 = GetTupleReference(4605154791657472L);

			public static readonly LOS.TupleReference Equipment_PlainBra_0 = GetTupleReference(4605066920988672L);

			public static readonly LOS.TupleReference Equipment_PleatedSkirt_1 = GetTupleReference(4605229781618688L);

			public static readonly LOS.TupleReference Equipment_PlainSneakers_0 = GetTupleReference(4605354394390528L);

			public static readonly LOS.TupleReference Equipment_Sneakers_0 = GetTupleReference(4605485478973440L);

			public static readonly LOS.TupleReference Equipment_Panties_0 = GetTupleReference(4605579670458368L);

			public static readonly LOS.TupleReference Equipment_Panties_1 = GetTupleReference(4605603133394944L);

			public static readonly LOS.TupleReference BotDialogueStage3_23000 = GetTupleReference(5719102916005888L);

			public static readonly LOS.TupleReference BotDialogueStage3_23001 = GetTupleReference(5719102928588800L);

			public static readonly LOS.TupleReference BotDialogueStage3_23002 = GetTupleReference(5719102928588801L);

			public static readonly LOS.TupleReference BotDialogueStage3_24000 = GetTupleReference(5719102928588802L);

			public static readonly LOS.TupleReference BotDialogueStage3_24001 = GetTupleReference(5719102928588803L);

			public static readonly LOS.TupleReference BotDialogueStage3_24002 = GetTupleReference(5719102928588804L);

			public static readonly LOS.TupleReference BotDialogueStage3_26000 = GetTupleReference(5719102928588805L);

			public static readonly LOS.TupleReference BotDialogueStage3_26001 = GetTupleReference(5719102928588806L);

			public static readonly LOS.TupleReference BotDialogueStage3_26002 = GetTupleReference(5719102928588807L);

			public static readonly LOS.TupleReference BotDialogueStage3_27000 = GetTupleReference(5719102928588808L);

			public static readonly LOS.TupleReference BotDialogueStage3_27001 = GetTupleReference(5719102928588809L);

			public static readonly LOS.TupleReference BotDialogueStage3_27002 = GetTupleReference(5719102928588810L);

			public static readonly LOS.TupleReference BotDialogueStage3_28000 = GetTupleReference(5719102928588811L);

			public static readonly LOS.TupleReference BotDialogueStage3_28001 = GetTupleReference(5719102928588812L);

			public static readonly LOS.TupleReference BotDialogueStage3_28002 = GetTupleReference(5719102928588813L);

			public static readonly LOS.TupleReference BotDialogueStage3_29000 = GetTupleReference(5719102928588814L);

			public static readonly LOS.TupleReference BotDialogueStage3_29001 = GetTupleReference(5719102928588815L);

			public static readonly LOS.TupleReference BotDialogueStage3_30000 = GetTupleReference(5719102928588816L);

			public static readonly LOS.TupleReference BotDialogueStage3_30001 = GetTupleReference(5719102928588817L);

			public static readonly LOS.TupleReference BotDialogueStage3_30002 = GetTupleReference(5719102928588818L);

			public static readonly LOS.TupleReference BotDialogueStage3_31000 = GetTupleReference(5719102928588819L);

			public static readonly LOS.TupleReference BotDialogueStage3_31001 = GetTupleReference(5719102928588820L);

			public static readonly LOS.TupleReference BotDialogueStage3_31002 = GetTupleReference(5719102928588821L);

			public static readonly LOS.TupleReference BotDialogueStage3_32000 = GetTupleReference(5719102928588822L);

			public static readonly LOS.TupleReference BotDialogueStage3_32001 = GetTupleReference(5719102928588823L);

			public static readonly LOS.TupleReference BotDialogueStage3_32002 = GetTupleReference(5719102928588824L);

			public static readonly LOS.TupleReference BotDialogueStage3_33000 = GetTupleReference(5719102928588825L);

			public static readonly LOS.TupleReference BotDialogueStage3_33001 = GetTupleReference(5719102928588826L);

			public static readonly LOS.TupleReference BotDialogueStage3_34000 = GetTupleReference(5719102928588827L);

			public static readonly LOS.TupleReference BotDialogueStage3_34001 = GetTupleReference(5719102928588828L);

			public static readonly LOS.TupleReference BotDialogueStage3_34002 = GetTupleReference(5719102928588829L);

			public static readonly LOS.TupleReference BotDialogueStage3_35000 = GetTupleReference(5719102928588830L);

			public static readonly LOS.TupleReference BotDialogueStage3_35001 = GetTupleReference(5719102928588831L);

			public static readonly LOS.TupleReference BotDialogueStage3_35002 = GetTupleReference(5719102928588832L);

			public static readonly LOS.TupleReference ColorName_JunsGrey = GetTupleReference(6480075763355648L);

			public static readonly LOS.TupleReference ColorName_JunsMagenta = GetTupleReference(6480075767549952L);

			public static readonly LOS.TupleReference ColorName_JunsPupils = GetTupleReference(6480075767549953L);

			public static readonly LOS.TupleReference ColorName_JunsIrises = GetTupleReference(6480075767549954L);

			public static readonly LOS.TupleReference ColorName_DefaultSkin = GetTupleReference(6480075767549955L);

			public static readonly LOS.TupleReference ColorName_JunsFlesh = GetTupleReference(6480075767549956L);

			public static readonly LOS.TupleReference ColorName_JunsBlush = GetTupleReference(6480075767549957L);

			public static readonly LOS.TupleReference ColorName_OldPlainWhite = GetTupleReference(6480075767549958L);

			public static readonly LOS.TupleReference ColorName_OldSadGrey = GetTupleReference(6480075767549959L);

			public static readonly LOS.TupleReference ColorName_OldCuteMagenta = GetTupleReference(6480075767549960L);

			public static readonly LOS.TupleReference ColorName_OldUglyBlue = GetTupleReference(6480075771744256L);

			public static readonly LOS.TupleReference ColorName_OldDreamyCyan = GetTupleReference(6480075771744257L);

			public static readonly LOS.TupleReference ColorName_OldGrassyGreen = GetTupleReference(6480075771744258L);

			public static readonly LOS.TupleReference ColorName_OldFakeGold = GetTupleReference(6480075771744259L);

			public static readonly LOS.TupleReference ColorName_OldBloodyRed = GetTupleReference(6480075771744260L);

			public static readonly LOS.TupleReference ColorName_OldSpecialTurquoise = GetTupleReference(6480075771744261L);

			public static readonly LOS.TupleReference ColorName_OldToxicGreen = GetTupleReference(6480075771744262L);

			public static readonly LOS.TupleReference ColorName_OldKitsunePiku = GetTupleReference(6480075771744263L);

			public static readonly LOS.TupleReference ColorName_OldAsphalt = GetTupleReference(6480075771744264L);

			public static readonly LOS.TupleReference ColorName_White = GetTupleReference(6480075771744265L);

			public static readonly LOS.TupleReference ColorName_LightGrey = GetTupleReference(6480075771744266L);

			public static readonly LOS.TupleReference ColorName_Grey = GetTupleReference(6480075771744267L);

			public static readonly LOS.TupleReference ColorName_DarkGrey = GetTupleReference(6480075771744268L);

			public static readonly LOS.TupleReference ColorName_VeryDarkGrey = GetTupleReference(6480075771744269L);

			public static readonly LOS.TupleReference ColorName_UnicornGalaxy = GetTupleReference(6480075771744270L);

			public static readonly LOS.TupleReference ColorName_CuteMagenta = GetTupleReference(6480075771744271L);

			public static readonly LOS.TupleReference ColorName_StrawberyMilk = GetTupleReference(6480075771744272L);

			public static readonly LOS.TupleReference ColorName_ToxicPurple = GetTupleReference(6480075771744273L);

			public static readonly LOS.TupleReference ColorName_MidnightBlue = GetTupleReference(6480075771744274L);

			public static readonly LOS.TupleReference ColorName_BasicBlack = GetTupleReference(6480075771744275L);

			public static readonly LOS.TupleReference ColorName_BasicWhite = GetTupleReference(6480075771744276L);

			public static readonly LOS.TupleReference ColorName_BloodyRed = GetTupleReference(6480075771744277L);

			public static readonly LOS.TupleReference ColorName_Miku = GetTupleReference(6480075771744278L);

			public static readonly LOS.TupleReference ColorName_FakeGold = GetTupleReference(6480075771744279L);

			public static readonly LOS.TupleReference ColorName_DreamyCyan = GetTupleReference(6480075771744280L);

			public static readonly LOS.TupleReference ColorName_UglyBlue = GetTupleReference(6480075771744281L);

			public static readonly LOS.TupleReference ColorName_EBPurple = GetTupleReference(6480075771744282L);

			public static readonly LOS.TupleReference ColorName_EBBlue = GetTupleReference(6480075771744283L);

			public static readonly LOS.TupleReference ColorName_EBGreen = GetTupleReference(6480075771744284L);

			public static readonly LOS.TupleReference ColorName_EBYellow = GetTupleReference(6480075771744285L);

			public static readonly LOS.TupleReference ColorName_EBOrange = GetTupleReference(6480075771744286L);

			public static readonly LOS.TupleReference ColorName_EBRed = GetTupleReference(6480075771744287L);

			public static readonly LOS.TupleReference ColorName_Custom = GetTupleReference(6484980532121600L);

			public static readonly LOS.TupleReference Blowjob_SwallowFull = GetTupleReference(36649044804399104L);

			public static readonly LOS.TupleReference Blowjob_SwallowHalf = GetTupleReference(36649314556866560L);

			public static readonly LOS.TupleReference Blowjob_SwallowLittle = GetTupleReference(36649699321344000L);

			public static readonly LOS.TupleReference Blowjob_SwallowAlmostEmpty = GetTupleReference(36650295650709504L);

			public static readonly LOS.TupleReference Blowjob_EdgingForceStop = GetTupleReference(36651023119183872L);

			public static readonly LOS.TupleReference Blowjob_EdgingAlmostCame = GetTupleReference(36651290724167680L);

			public static readonly LOS.TupleReference Blowjob_EdgingHadToSlowdown = GetTupleReference(36651869752029184L);

			public static readonly LOS.TupleReference Blowjob_EdgingOn = GetTupleReference(36658999078973440L);

			public static readonly LOS.TupleReference Blowjob_EdgingOff = GetTupleReference(36659004787421184L);

			public static readonly LOS.TupleReference Game_OldMaid_StartingWithQueen = GetTupleReference(37005369136672768L);

			public static readonly LOS.TupleReference Game_OldMaid_StartingWithoutQueen = GetTupleReference(37005500103815168L);

			public static readonly LOS.TupleReference Game_OldMaid_Waiting = GetTupleReference(37006170395537408L);

			public static readonly LOS.TupleReference Game_OldMaid_PlayerPickingStartWithQueen = GetTupleReference(37006206491717632L);

			public static readonly LOS.TupleReference Game_OldMaid_PlayerPickingStartWithoutQueen = GetTupleReference(37006398297239552L);

			public static readonly LOS.TupleReference Game_OldMaid_PlayerPickingDoesntMatter = GetTupleReference(37006908236525568L);

			public static readonly LOS.TupleReference Game_OldMaid_PlayerPickedQueenAfterLying = GetTupleReference(37007086569943040L);

			public static readonly LOS.TupleReference Game_OldMaid_PlayerPickedQueen = GetTupleReference(37007252152676352L);

			public static readonly LOS.TupleReference Game_OldMaid_PlayerPickedCardOtherThanQueenWhileBotHasQueen = GetTupleReference(37007384810123264L);

			public static readonly LOS.TupleReference Game_OldMaid_BotPickingCardsHover = GetTupleReference(37007703203934208L);

			public static readonly LOS.TupleReference Game_OldMaid_BotPickingCardsChosen = GetTupleReference(37007761286656000L);

			public static readonly LOS.TupleReference Game_OldMaid_BotChooseQueen = GetTupleReference(37007916232634368L);

			public static readonly LOS.TupleReference Game_OldMaid_BotChooseOtherWhilePlayerHasQueen = GetTupleReference(37007950978248704L);

			public static readonly LOS.TupleReference Game_OldMaid_PlayerWon = GetTupleReference(37008621588738048L);

			public static readonly LOS.TupleReference Game_OldMaid_PlayerLost = GetTupleReference(37008665297580032L);

			public static readonly LOS.TupleReference Game_OldMaid_Summary = GetTupleReference(37008920160268288L);

			public static readonly LOS.TupleReference Game_ThanksForPlaying = GetTupleReference(37018473102864384L);

			public static readonly LOS.TupleReference Game_OldMaid_HintDont = GetTupleReference(37018886514438144L);

			public static readonly LOS.TupleReference Game_OldMaid_HintDo = GetTupleReference(37018961298878464L);

			public static readonly LOS.TupleReference Game_OldMaid_HintNoInfo = GetTupleReference(37019204346212352L);

			public static readonly LOS.TupleReference Game_OldMaid_PlayerLeft = GetTupleReference(37246397311643648L);

			public static readonly LOS.TupleReference Game_OldMaid_Summary_Left = GetTupleReference(37247700263792640L);

			public static readonly LOS.TupleReference Achievement_ending_KilledByADirtyCop1 = GetTupleReference(38074455498297344L);

			public static readonly LOS.TupleReference Achievement_ending_KilledByADirtyCop2 = GetTupleReference(38074638151847936L);

			public static readonly LOS.TupleReference Game_OldMaid_StartGamePlayer = GetTupleReference(38333175650193408L);

			public static readonly LOS.TupleReference Game_OldMaid_StartGameBot = GetTupleReference(38333588281626624L);

			public static readonly LOS.TupleReference MentalHealth_HeadacheHigh = GetTupleReference(40262055420104704L);

			public static readonly LOS.TupleReference MentalHealth_HeadacheLow = GetTupleReference(40262059543105536L);

			public static readonly LOS.TupleReference VeryHungry_HighHealth = GetTupleReference(40261798590287872L);

			public static readonly LOS.TupleReference VeryHungry_MediumHealth = GetTupleReference(40261805439586304L);

			public static readonly LOS.TupleReference VeryHungry_LowHealth = GetTupleReference(40261821143060480L);

			public static readonly LOS.TupleReference MediumHungry_HighHealth = GetTupleReference(40261838666862592L);

			public static readonly LOS.TupleReference MediumHungry_MediumHealth = GetTupleReference(40261840285863936L);

			public static readonly LOS.TupleReference MediumHungry_LowHealth = GetTupleReference(40261841682567168L);

			public static readonly LOS.TupleReference LittleHungry_HighHealth = GetTupleReference(40261887589224448L);

			public static readonly LOS.TupleReference LittleHungry_LowHealth = GetTupleReference(40261890986610688L);

			public static readonly LOS.TupleReference LittleHungry_MediumHealth = GetTupleReference(40261889208225792L);

			public static readonly LOS.TupleReference LostMoney = GetTupleReference(46677620225347584L);

			public static readonly LOS.TupleReference GotSweets = GetTupleReference(46801134785794048L);

			public static readonly LOS.TupleReference StreamEventRequirement_CumInside = GetTupleReference(78731296074764288L);

			public static readonly LOS.TupleReference StreamEventRequirement_CumOutside = GetTupleReference(78731297593102336L);

			public static readonly LOS.TupleReference StreamEventRequirement_CumInsideAnal = GetTupleReference(78731298868170752L);

			public static readonly LOS.TupleReference StreamEventRequirement_CumInsideMouth = GetTupleReference(78731300428451840L);

			public static readonly LOS.TupleReference StreamEventRequirement_GotFlacid = GetTupleReference(78731935706124288L);

			public static readonly LOS.TupleReference StreamEventRequirement_Insert = GetTupleReference(78738233873010688L);

			public static readonly LOS.TupleReference StreamEventRequirement_Pullout = GetTupleReference(78738256622915584L);

			public static readonly LOS.TupleReference Sender_Unknown = GetTupleReference(79021807289372672L);

			public static readonly LOS.TupleReference Delivery_StatusToBeCollected = GetTupleReference(79040422487134208L);

			public static readonly LOS.TupleReference Delivery_StatusToBeSorted = GetTupleReference(79040500077563904L);

			public static readonly LOS.TupleReference Delivery_DeliveryDispatched = GetTupleReference(79040503714025472L);

			public static readonly LOS.TupleReference Delivery_DeliverySoon = GetTupleReference(79040651068313600L);

			public static readonly LOS.TupleReference Delivery_PredictedTime = GetTupleReference(79040864239620096L);

			public static readonly LOS.TupleReference Delivery_PredictedTimeSoon = GetTupleReference(79041449756069888L);

			public static readonly LOS.TupleReference Sender_BigDaddyHurt = GetTupleReference(79046713855053824L);

			public static readonly LOS.TupleReference Sender_Multiple = GetTupleReference(79368941532483584L);

			public static readonly LOS.TupleReference MentalHealth_DeathDoor = GetTupleReference(91351293984903168L);

			public static readonly LOS.TupleReference WhatTheHell = GetTupleReference(109890604040589312L);

			public static readonly LOS.TupleReference IAmScared = GetTupleReference(109893380904718336L);

			public static readonly LOS.TupleReference ColorName_VolksAutoGrey = GetTupleReference(110467144243613696L);

			public static readonly LOS.TupleReference GotItem = GetTupleReference(111561705527316480L);

			public static readonly LOS.TupleReference ColorName_NavyGreen = GetTupleReference(117395373416124416L);

			public static readonly LOS.TupleReference ColorName_TempleCyan = GetTupleReference(118217092112453632L);

			public static readonly LOS.TupleReference ColorName_AvocadoGreen = GetTupleReference(118217092112453633L);

			public static readonly LOS.TupleReference ColorName_LunarPrincessGrey = GetTupleReference(118217092112453634L);

			public static readonly LOS.TupleReference ColorName_SatinBlack = GetTupleReference(118217092112453635L);

			public static readonly LOS.TupleReference ColorName_StilouBlue = GetTupleReference(118217092112453636L);

			public static readonly LOS.TupleReference ColorName_StilouRed = GetTupleReference(118217092112453637L);

			public static readonly LOS.TupleReference ColorName_StilouYellow = GetTupleReference(118217092112453638L);

			public static readonly LOS.TupleReference ColorName_MonizmedBurgundy = GetTupleReference(118217092112453639L);

			public static readonly LOS.TupleReference ColorName_MonizmedPurple = GetTupleReference(118217092112453640L);

			public static readonly LOS.TupleReference ColorName_DiabetePurple = GetTupleReference(118217092112453641L);

			public static readonly LOS.TupleReference ColorName_DiabeteGold = GetTupleReference(118217092112453642L);

			public static readonly LOS.TupleReference ColorName_DiabeteColaRed = GetTupleReference(118217092112453643L);

			public static readonly LOS.TupleReference ColorName_DiabeteColaYellow = GetTupleReference(118217092112453644L);

			public static readonly LOS.TupleReference ColorName_DiabeteColaBlue = GetTupleReference(118217092112453645L);

			public static readonly LOS.TupleReference ColorName_DiabeteDrSugarRed = GetTupleReference(118217092112453646L);

			public static readonly LOS.TupleReference ColorName_DiabeteDrSugarBlue = GetTupleReference(118217092112453647L);

			public static readonly LOS.TupleReference ColorName_DiabeteSweetPotatoBrown = GetTupleReference(118217092112453648L);

			public static readonly LOS.TupleReference ColorName_DiabeteSweetPotatoOrange = GetTupleReference(118217092112453649L);

			public static readonly LOS.TupleReference ColorName_CazinoGreen = GetTupleReference(118217092112453650L);

			public static readonly LOS.TupleReference ColorName_CazinoBrown = GetTupleReference(118217092112453651L);

			public static readonly LOS.TupleReference Cowgirl_RemovePants = GetTupleReference(120033781170966528L);

			public static readonly LOS.TupleReference Cowgirl_BeforeInsert = GetTupleReference(120034458442010624L);

			public static readonly LOS.TupleReference Cowgirl_BeforeInsertBotLead = GetTupleReference(120034827842752512L);

			public static readonly LOS.TupleReference Cowgirl_CumInMouth = GetTupleReference(120039386887622656L);

			public static readonly LOS.TupleReference Cowgirl_CumInside = GetTupleReference(120039449936400384L);

			public static readonly LOS.TupleReference Cowgirl_CumOutside = GetTupleReference(120039509310967808L);

			public static readonly LOS.TupleReference Cowgirl_DidntDecide = GetTupleReference(120040555219390464L);

			public static readonly LOS.TupleReference Frick_CumOutside = GetTupleReference(120043369240858624L);

			public static readonly LOS.TupleReference Cowgirl_CumQuestion = GetTupleReference(120045205435846656L);

			public static readonly LOS.TupleReference Cowgirl_EdgeVeryClose = GetTupleReference(120136478058483712L);

			public static readonly LOS.TupleReference NunRepair_EquipedItem = GetTupleReference(120730844321943552L);

			public static readonly LOS.TupleReference NunRepair_Denied = GetTupleReference(120731892331712512L);

			public static readonly LOS.TupleReference NunRepair_DeniedNoMoney = GetTupleReference(120732450169950208L);

			public static readonly LOS.TupleReference NunRepair_Busy = GetTupleReference(120733919916986368L);

			public static readonly LOS.TupleReference NunRepair_RepairResult = GetTupleReference(120739286138884096L);

			public static readonly LOS.TupleReference NunRepair_RepairStarted = GetTupleReference(120742247099404288L);

			public static readonly LOS.TupleReference ColorName_Black = GetTupleReference(121122169009606656L);

			public static readonly LOS.TupleReference Achievement_ending_theyKnowOblivious = GetTupleReference(121130260094652416L);

			public static readonly LOS.TupleReference Achievement_ending_theyKnow = GetTupleReference(121130260115623936L);

			public static readonly LOS.TupleReference Achievement_bigdaddyhurtSecretEpilogue = GetTupleReference(121152291804680192L);

			public static readonly LOS.TupleReference Cowgirl_CumTogether = GetTupleReference(122058373351739392L);

			public static readonly LOS.TupleReference Cowgirl_EdgeForceStopped = GetTupleReference(122691757786664960L);

			public static readonly LOS.TupleReference Cowgirl_FlacidReaction = GetTupleReference(122719261100478464L);

			public static readonly LOS.TupleReference Cowgirl_EdgingBreak = GetTupleReference(122729071132938240L);

			public static readonly LOS.TupleReference Frick_BotCum = GetTupleReference(122925642265006080L);

			public static readonly LOS.TupleReference Cowgirl_CumAlone = GetTupleReference(122685814826995712L);

			public static readonly LOS.TupleReference Stage2Headpat1 = GetTupleReference(188398410854699008L);

			public static readonly LOS.TupleReference Stage2Headpat2 = GetTupleReference(188398413132206080L);

			public static readonly LOS.TupleReference Stage2Headpat3 = GetTupleReference(188398414898008064L);

			public static readonly LOS.TupleReference Word_Brother = GetTupleReference(192453224328728576L);

			public static readonly LOS.TupleReference Word_Sister = GetTupleReference(192453258243870720L);

			public static readonly LOS.TupleReference Outside_RiotsDanger = GetTupleReference(196436563699163136L);

			public static readonly LOS.TupleReference Fishing_PollutedLakeName = GetTupleReference(268423275278753792L);

			public static readonly LOS.TupleReference Fishing_PollutedLakeDescription = GetTupleReference(268423666603122688L);

			public static readonly LOS.TupleReference Fishing_TrashedRiverName = GetTupleReference(268424045873061888L);

			public static readonly LOS.TupleReference Fishing_TrashedRiverDescription = GetTupleReference(268424115800498176L);

			public static readonly LOS.TupleReference Fishing_PremiumFishFarmName = GetTupleReference(268424601735782400L);

			public static readonly LOS.TupleReference Fishing_PremiumFishFarmDescription = GetTupleReference(268424603933597696L);

			public static readonly LOS.TupleReference Fishing_MicroplasticsCoastName = GetTupleReference(272492651078475776L);

			public static readonly LOS.TupleReference Fishing_MicroplasticsCoastDescription = GetTupleReference(272492655176310784L);

			public static readonly LOS.TupleReference ColorName_Bastille = GetTupleReference(276056677704261632L);

			public static readonly LOS.TupleReference ColorName_Mirage = GetTupleReference(276056677775564800L);

			public static readonly LOS.TupleReference ColorName_Toledo = GetTupleReference(276056677821702144L);

			public static readonly LOS.TupleReference ColorName_Bunker = GetTupleReference(276056677872033792L);

			public static readonly LOS.TupleReference ColorName_HippiePink = GetTupleReference(276056677918171136L);

			public static readonly LOS.TupleReference ColorName_LinkWater = GetTupleReference(276056677964308480L);

			public static readonly LOS.TupleReference ColorName_TurquoiseBlue = GetTupleReference(276056678014640128L);

			public static readonly LOS.TupleReference ColorName_Amethyst = GetTupleReference(276056678060777472L);

			public static readonly LOS.TupleReference ColorName_Fern = GetTupleReference(276056678111109120L);

			public static readonly LOS.TupleReference ColorName_DarkPink = GetTupleReference(276056678157246464L);

			public static readonly LOS.TupleReference Sender_FishingWebsite = GetTupleReference(326943858014281728L);

			public static readonly LOS.TupleReference CowGayMeter = GetTupleReference(290361684091985920L);

			public static readonly LOS.TupleReference FoodSatiation_FullMeal = GetTupleReference(296384348640239616L);

			public static readonly LOS.TupleReference FoodSatiation_BarelyABite = GetTupleReference(296384402218278912L);

			public static readonly LOS.TupleReference FoodSatiation_Meal = GetTupleReference(296384984555446272L);

			public static readonly LOS.TupleReference FoodSatiation_Snack = GetTupleReference(296385004788768768L);

			public static readonly LOS.TupleReference FoodSatiation_LightMeal = GetTupleReference(296384997876555776L);

			public static readonly LOS.TupleReference ColorName_EbonyClay = GetTupleReference(301479725122969600L);

			public static readonly LOS.TupleReference ColorName_Rope = GetTupleReference(301479725148135424L);

			public static readonly LOS.TupleReference ColorName_Leather = GetTupleReference(301479725148135425L);

			public static readonly LOS.TupleReference ColorName_CopperRust = GetTupleReference(301479725148135426L);

			public static readonly LOS.TupleReference ColorName_MellowApricot = GetTupleReference(301479725148135427L);

			public static readonly LOS.TupleReference ColorName_InkBlue = GetTupleReference(301479725148135428L);

			public static readonly LOS.TupleReference ColorName_WeatheredGray = GetTupleReference(301479725148135430L);

			public static readonly LOS.TupleReference ColorName_DarkLeather = GetTupleReference(301481769946832896L);

			public static readonly LOS.TupleReference ColorName_BlackLeather = GetTupleReference(301481769967804416L);

			public static readonly LOS.TupleReference ColorPalette_OldPartPalette = GetTupleReference(301524803753336832L);

			public static readonly LOS.TupleReference ColorPalette_DefaultPartPalette = GetTupleReference(301524803753336833L);

			public static readonly LOS.TupleReference ColorPalette_GrayScalePalette = GetTupleReference(301524803753336834L);

			public static readonly LOS.TupleReference ColorPalette_JunPalette = GetTupleReference(301524803753336835L);

			public static readonly LOS.TupleReference ColorPalette_SkinPalette = GetTupleReference(301524803753336836L);

			public static readonly LOS.TupleReference ColorPalette_LeatherPalette = GetTupleReference(301524803753336837L);

			public static readonly LOS.TupleReference ColorPalette_OtherPalette = GetTupleReference(301524803753336838L);

			public static readonly LOS.TupleReference ColorPalette_HotPinkPalette = GetTupleReference(301524803753336839L);

			public static readonly LOS.TupleReference ColorPalette_XoulionMainPalette = GetTupleReference(301524803753336840L);

			public static readonly LOS.TupleReference ColorPalette_XoulionLogoPalette = GetTupleReference(301524803753336841L);

			public static readonly LOS.TupleReference ColorPalette_SkinTonePalette = GetTupleReference(301567685663121408L);

			public static readonly LOS.TupleReference ColorName_Burgundy = GetTupleReference(301567685688287232L);

			public static readonly LOS.TupleReference ColorName_DarkUmber = GetTupleReference(301567685688287233L);

			public static readonly LOS.TupleReference ColorName_Sienna = GetTupleReference(301567685688287234L);

			public static readonly LOS.TupleReference ColorName_Sandstone = GetTupleReference(301567685688287235L);

			public static readonly LOS.TupleReference ColorName_Almond = GetTupleReference(301567685688287236L);

			public static readonly LOS.TupleReference ColorName_LightPeach = GetTupleReference(301567685688287237L);

			public static readonly LOS.TupleReference ColorName_DeepMahogany = GetTupleReference(301567685688287238L);

			public static readonly LOS.TupleReference ColorName_Bronze = GetTupleReference(301567685688287239L);

			public static readonly LOS.TupleReference ColorName_GoldenHoney = GetTupleReference(301567685688287240L);

			public static readonly LOS.TupleReference ColorName_WarmIvory = GetTupleReference(301567685688287241L);

			public static readonly LOS.TupleReference ColorName_PalePorcelain = GetTupleReference(301567685688287242L);

			public static readonly LOS.TupleReference ColorName_GoblinGreen = GetTupleReference(301567685688287243L);

			public static readonly LOS.TupleReference ColorName_SoftClay = GetTupleReference(301572741179367424L);

			public static readonly LOS.TupleReference ColorName_Rosewood = GetTupleReference(301572741179367425L);

			public static readonly LOS.TupleReference ColorName_Tawny = GetTupleReference(301572741179367426L);

			public static readonly LOS.TupleReference ColorName_BurntSienna = GetTupleReference(301572741179367427L);

			public static readonly LOS.TupleReference ColorName_EarthBrown = GetTupleReference(301572741179367428L);

			public static readonly LOS.TupleReference ColorName_PaleDoomer = GetTupleReference(301572741179367429L);

			public static readonly LOS.TupleReference ColorName_AnnaliesViolet = GetTupleReference(313837547580022784L);

			public static readonly LOS.TupleReference Achievement_ending_unluckyEncounterWithLead = GetTupleReference(321774208251060224L);

			public static readonly LOS.TupleReference Achievement_ending_itWasntSupposedToEndLikeThis = GetTupleReference(321818294404243456L);

			public static readonly LOS.TupleReference Achievement_ending_friendsTillTheVeryEnd = GetTupleReference(321823121737768960L);

			public static readonly LOS.TupleReference Outside_DAKDanger = GetTupleReference(321874011689574400L);

			public static readonly LOS.TupleReference Achievement_ending_victimOfARevolution = GetTupleReference(321883160968159232L);

			public static readonly LOS.TupleReference Dish_AbsolutelyButchered = GetTupleReference(326149039096586240L);

			public static readonly LOS.TupleReference Dish_VeryBad = GetTupleReference(326149122840059904L);

			public static readonly LOS.TupleReference Dish_Bad = GetTupleReference(326149139432726528L);

			public static readonly LOS.TupleReference Dish_Average = GetTupleReference(326149140951064576L);

			public static readonly LOS.TupleReference Dish_Tasty = GetTupleReference(326149142440042496L);

			public static readonly LOS.TupleReference Dish_Delicious = GetTupleReference(326149173121376256L);

			public static readonly LOS.TupleReference Dish_Exquisite = GetTupleReference(326149183456141312L);

			public static readonly LOS.TupleReference Dish_Masterpiece = GetTupleReference(326149200812171264L);

			public static readonly LOS.TupleReference Dish_WorldClass = GetTupleReference(326149218306613248L);

			public static readonly LOS.TupleReference Achievement_ending_grenade = GetTupleReference(328734652681412608L);

			public static readonly LOS.TupleReference Report3File = GetTupleReference(329484386803113984L);

			public static readonly LOS.TupleReference Report2File = GetTupleReference(329484473197387776L);

			public static readonly LOS.TupleReference Report1File = GetTupleReference(329485745719861248L);

			public static readonly LOS.TupleReference JournalFile1 = GetTupleReference(330996496531972096L);

			public static readonly LOS.TupleReference JournalFile2 = GetTupleReference(329503582547075072L);

			public static readonly LOS.TupleReference JournalFile3 = GetTupleReference(329503584661004288L);

			public static readonly LOS.TupleReference Shower_AfterSex_CleanCum = GetTupleReference(474009182340636672L);

			public static readonly LOS.TupleReference Shower_AfterSex_LeaveItOn = GetTupleReference(474009187306692608L);

			public static readonly LOS.TupleReference Achievement_ending_islandAnnalie = GetTupleReference(483386357271486464L);

			public static readonly LOS.TupleReference Achievement_ending_islandLandlord = GetTupleReference(483386357300846592L);

			public static readonly LOS.TupleReference Achievement_ending_islandLandlordEveryone = GetTupleReference(483386357300846593L);

			public static readonly LOS.TupleReference Achievement_ending_revengeOfTheHomeless = GetTupleReference(483717650156871680L);

			public static readonly LOS.TupleReference Achievement_ending_revengeOfTheHomelessBeforeTheFlight = GetTupleReference(483770983890542592L);

			public static readonly LOS.TupleReference Fishing_ImaginarySeaName = GetTupleReference(542204484809383936L);

			public static readonly LOS.TupleReference Fishing_ImaginarySeaDescription = GetTupleReference(542204545714872320L);

			public static readonly LOS.TupleReference Achievement_ending_veryBadTrip = GetTupleReference(544684971587002368L);

			public static readonly LOS.TupleReference Achievement_badTripSavedByMelissa = GetTupleReference(544684971612168192L);

			public static readonly LOS.TupleReference Achievement_lastTripMetDad = GetTupleReference(544944266295631872L);

			public static readonly LOS.TupleReference Achievement_defeatedBotInFishingDuel = GetTupleReference(544944266320797696L);

			public static readonly LOS.TupleReference Achievement_helpedAnnalieDuringRiots = GetTupleReference(544960584205983744L);

			public static readonly LOS.TupleReference Achievement_helpedPristbotDuringRiots = GetTupleReference(544960584210178048L);

			public static readonly LOS.TupleReference Achievement_melissaFriendsDead = GetTupleReference(544961927830298624L);

			public static readonly LOS.TupleReference Achievement_melissaFriendsLeaves = GetTupleReference(544961927834492928L);

			public static readonly LOS.TupleReference Achievement_melissaDestory = GetTupleReference(544961927834492929L);

			public static readonly LOS.TupleReference Achievement_melissaLeave = GetTupleReference(544961927834492930L);

			public static readonly LOS.TupleReference Achievement_shower = GetTupleReference(544964046893342720L);

			public static readonly LOS.TupleReference Achievement_sleepingTogether = GetTupleReference(544964046893342721L);

			public static readonly LOS.TupleReference Achievement_runway_Landlord = GetTupleReference(544964046893342722L);

			public static readonly LOS.TupleReference Achievement_runway_Shanice = GetTupleReference(544964046893342723L);

			public static readonly LOS.TupleReference Achievement_runway_Fisher = GetTupleReference(544964046893342724L);
		}

		public static class Sets
		{
		}

		public const string TABLE_NAME = "Other";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			118, 28, 23, 147, 78, 135, 212, 70, 169, 239,
			200, 49, 110, 222, 246, 165
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string Generator_color => GS(1709149392896L);

		public static string Generator_femalename => GS(1709170364416L);

		public static string Generator_country => GS(1709170364417L);

		public static string Generator_country2 => GS(1709170364418L);

		public static string Generator_bodypart => GS(1709170364419L);

		public static string Generator_animegirl => GS(1709170364420L);

		public static string Generator_animeboy => GS(1709170364421L);

		public static string Generator_celebrity => GS(1709170364422L);

		public static string Generator_company => GS(1709170364423L);

		public static string Generator_animal => GS(1709170364424L);

		public static string Generator_UScity => GS(1709170364425L);

		public static string Generator_terroristorganisation => GS(1709170364426L);

		public static string Generator_sexuality => GS(1709170364427L);

		public static string Generator_sexuality2 => GS(1709170364428L);

		public static string Generator_deathcause => GS(1709170364429L);

		public static string Generator_art => GS(1709170364430L);

		public static string Generator_disease => GS(1709170364431L);

		public static string Generator_name => GS(1709170364432L);

		public static string Achievement_ending_ballsexploded => GS(652887969370112L);

		public static string Achievement_ending_goodEnd1 => GS(652887981953024L);

		public static string Achievement_ending_badEnd1 => GS(652887981953025L);

		public static string Achievement_ending_badEnd2 => GS(652887981953026L);

		public static string Achievement_ending_badEnd3 => GS(652887981953027L);

		public static string Achievement_ending_badEnd4dot1 => GS(652887981953028L);

		public static string Achievement_ending_badEnd4dot2 => GS(652887981953029L);

		public static string Achievement_ending_healthEnd => GS(652887981953030L);

		public static string Achievement_ending_fedEnd => GS(652887981953031L);

		public static string Achievement_ending_schizoEnd => GS(652887986147328L);

		public static string Achievement_ending_genericSchizoEnd => GS(652887986147329L);

		public static string Achievement_church_endSpotkanie2B => GS(652887986147330L);

		public static string Achievement_church_endTheWorstEnd => GS(652887986147331L);

		public static string Achievement_church_endIdontknow => GS(652887986147332L);

		public static string Achievement_church_endJustGoThereGood => GS(652887986147333L);

		public static string Achievement_church_endJustGoThereBad => GS(652887986147334L);

		public static string Achievement_saveEdited => GS(652887986147335L);

		public static string Shop_NotReadyEndings => GS(3155252333121536L);

		public static string Shop_ReadyEndings => GS(3155250466656256L);

		public static string Outside_CantGo => GS(3158103419654144L);

		public static string Outside_TooOften => GS(3158230012137472L);

		public static string Outside_Info => GS(3158417979871232L);

		public static string Outside_VeryDangerous => GS(3158477727731712L);

		public static string Outside_Dangerous => GS(3158572758077440L);

		public static string Outside_Medium => GS(3158597827432448L);

		public static string Outside_AlmostSafe => GS(3158634041053184L);

		public static string Outside_Safe => GS(3158679217901568L);

		public static string Outside_NoPanties => GS(3158785157632000L);

		public static string Outside_AllLimbs => GS(3159199408066560L);

		public static string Outside_UpperBody => GS(3159271713673216L);

		public static string Outside_LowerBody => GS(3159334548541440L);

		public static string Outside_Shoes => GS(3159388130775040L);

		public static string Equipment_Generic_0 => GS(4598202804080640L);

		public static string Equipment_Generic_1 => GS(4598202820857856L);

		public static string Equipment_Generic_2 => GS(4598202820857857L);

		public static string Equipment_Generic_3 => GS(4598920667598848L);

		public static string Equipment_Generic_4 => GS(4598920667598849L);

		public static string Equipment_Generic_5 => GS(4598920667598850L);

		public static string Equipment_Generic_6 => GS(4598920667598851L);

		public static string Equipment_Generic_7 => GS(4598920667598852L);

		public static string Equipment_Generic_8 => GS(4598920667598853L);

		public static string Equipment_Generic_9 => GS(4598920667598854L);

		public static string Equipment_Generic_10 => GS(4598920667598855L);

		public static string Equipment_Generic_11 => GS(4598920667598856L);

		public static string Equipment_Generic_12 => GS(4598920667598857L);

		public static string Equipment_Generic_13 => GS(4598920667598858L);

		public static string Equipment_Generic_14 => GS(4598920667598859L);

		public static string Equipment_Generic_15 => GS(4598920667598860L);

		public static string Equipment_Generic_16 => GS(4598920667598861L);

		public static string Equipment_Generic_17 => GS(4598920667598862L);

		public static string Equipment_Generic_18 => GS(4598920667598863L);

		public static string Equipment_Generic_19 => GS(4598920667598864L);

		public static string Equipment_Generic_20 => GS(4598920667598865L);

		public static string Equipment_Generic_21 => GS(4598920667598866L);

		public static string Equipment_Generic_22 => GS(4598920667598867L);

		public static string Equipment_Generic_23 => GS(4598920667598868L);

		public static string Equipment_Shirt_0 => GS(4604487419170816L);

		public static string Equipment_StripedPanties_0 => GS(4604615798427648L);

		public static string Equipment_Stocking_0 => GS(4604730810437632L);

		public static string Equipment_Lingerie_0 => GS(4604807767527424L);

		public static string Equipment_LongSock_0 => GS(4604903447990272L);

		public static string Equipment_CuteBow_0 => GS(4604997891133440L);

		public static string Equipment_PleatedSkirt_0 => GS(4605154791657472L);

		public static string Equipment_PlainBra_0 => GS(4605066920988672L);

		public static string Equipment_PleatedSkirt_1 => GS(4605229781618688L);

		public static string Equipment_PlainSneakers_0 => GS(4605354394390528L);

		public static string Equipment_Sneakers_0 => GS(4605485478973440L);

		public static string Equipment_Panties_0 => GS(4605579670458368L);

		public static string Equipment_Panties_1 => GS(4605603133394944L);

		public static string BotDialogueStage3_23000 => GS(5719102916005888L);

		public static string BotDialogueStage3_23001 => GS(5719102928588800L);

		public static string BotDialogueStage3_23002 => GS(5719102928588801L);

		public static string BotDialogueStage3_24000 => GS(5719102928588802L);

		public static string BotDialogueStage3_24001 => GS(5719102928588803L);

		public static string BotDialogueStage3_24002 => GS(5719102928588804L);

		public static string BotDialogueStage3_26000 => GS(5719102928588805L);

		public static string BotDialogueStage3_26001 => GS(5719102928588806L);

		public static string BotDialogueStage3_26002 => GS(5719102928588807L);

		public static string BotDialogueStage3_27000 => GS(5719102928588808L);

		public static string BotDialogueStage3_27001 => GS(5719102928588809L);

		public static string BotDialogueStage3_27002 => GS(5719102928588810L);

		public static string BotDialogueStage3_28000 => GS(5719102928588811L);

		public static string BotDialogueStage3_28001 => GS(5719102928588812L);

		public static string BotDialogueStage3_28002 => GS(5719102928588813L);

		public static string BotDialogueStage3_29000 => GS(5719102928588814L);

		public static string BotDialogueStage3_29001 => GS(5719102928588815L);

		public static string BotDialogueStage3_30000 => GS(5719102928588816L);

		public static string BotDialogueStage3_30001 => GS(5719102928588817L);

		public static string BotDialogueStage3_30002 => GS(5719102928588818L);

		public static string BotDialogueStage3_31000 => GS(5719102928588819L);

		public static string BotDialogueStage3_31001 => GS(5719102928588820L);

		public static string BotDialogueStage3_31002 => GS(5719102928588821L);

		public static string BotDialogueStage3_32000 => GS(5719102928588822L);

		public static string BotDialogueStage3_32001 => GS(5719102928588823L);

		public static string BotDialogueStage3_32002 => GS(5719102928588824L);

		public static string BotDialogueStage3_33000 => GS(5719102928588825L);

		public static string BotDialogueStage3_33001 => GS(5719102928588826L);

		public static string BotDialogueStage3_34000 => GS(5719102928588827L);

		public static string BotDialogueStage3_34001 => GS(5719102928588828L);

		public static string BotDialogueStage3_34002 => GS(5719102928588829L);

		public static string BotDialogueStage3_35000 => GS(5719102928588830L);

		public static string BotDialogueStage3_35001 => GS(5719102928588831L);

		public static string BotDialogueStage3_35002 => GS(5719102928588832L);

		public static string ColorName_JunsGrey => GS(6480075763355648L);

		public static string ColorName_JunsMagenta => GS(6480075767549952L);

		public static string ColorName_JunsPupils => GS(6480075767549953L);

		public static string ColorName_JunsIrises => GS(6480075767549954L);

		public static string ColorName_DefaultSkin => GS(6480075767549955L);

		public static string ColorName_JunsFlesh => GS(6480075767549956L);

		public static string ColorName_JunsBlush => GS(6480075767549957L);

		public static string ColorName_OldPlainWhite => GS(6480075767549958L);

		public static string ColorName_OldSadGrey => GS(6480075767549959L);

		public static string ColorName_OldCuteMagenta => GS(6480075767549960L);

		public static string ColorName_OldUglyBlue => GS(6480075771744256L);

		public static string ColorName_OldDreamyCyan => GS(6480075771744257L);

		public static string ColorName_OldGrassyGreen => GS(6480075771744258L);

		public static string ColorName_OldFakeGold => GS(6480075771744259L);

		public static string ColorName_OldBloodyRed => GS(6480075771744260L);

		public static string ColorName_OldSpecialTurquoise => GS(6480075771744261L);

		public static string ColorName_OldToxicGreen => GS(6480075771744262L);

		public static string ColorName_OldKitsunePiku => GS(6480075771744263L);

		public static string ColorName_OldAsphalt => GS(6480075771744264L);

		public static string ColorName_White => GS(6480075771744265L);

		public static string ColorName_LightGrey => GS(6480075771744266L);

		public static string ColorName_Grey => GS(6480075771744267L);

		public static string ColorName_DarkGrey => GS(6480075771744268L);

		public static string ColorName_VeryDarkGrey => GS(6480075771744269L);

		public static string ColorName_UnicornGalaxy => GS(6480075771744270L);

		public static string ColorName_CuteMagenta => GS(6480075771744271L);

		public static string ColorName_StrawberyMilk => GS(6480075771744272L);

		public static string ColorName_ToxicPurple => GS(6480075771744273L);

		public static string ColorName_MidnightBlue => GS(6480075771744274L);

		public static string ColorName_BasicBlack => GS(6480075771744275L);

		public static string ColorName_BasicWhite => GS(6480075771744276L);

		public static string ColorName_BloodyRed => GS(6480075771744277L);

		public static string ColorName_Miku => GS(6480075771744278L);

		public static string ColorName_FakeGold => GS(6480075771744279L);

		public static string ColorName_DreamyCyan => GS(6480075771744280L);

		public static string ColorName_UglyBlue => GS(6480075771744281L);

		public static string ColorName_EBPurple => GS(6480075771744282L);

		public static string ColorName_EBBlue => GS(6480075771744283L);

		public static string ColorName_EBGreen => GS(6480075771744284L);

		public static string ColorName_EBYellow => GS(6480075771744285L);

		public static string ColorName_EBOrange => GS(6480075771744286L);

		public static string ColorName_EBRed => GS(6480075771744287L);

		public static string ColorName_Custom => GS(6484980532121600L);

		public static string Blowjob_SwallowFull => GS(36649044804399104L);

		public static string Blowjob_SwallowHalf => GS(36649314556866560L);

		public static string Blowjob_SwallowLittle => GS(36649699321344000L);

		public static string Blowjob_SwallowAlmostEmpty => GS(36650295650709504L);

		public static string Blowjob_EdgingForceStop => GS(36651023119183872L);

		public static string Blowjob_EdgingAlmostCame => GS(36651290724167680L);

		public static string Blowjob_EdgingHadToSlowdown => GS(36651869752029184L);

		public static string Blowjob_EdgingOn => GS(36658999078973440L);

		public static string Blowjob_EdgingOff => GS(36659004787421184L);

		public static string Game_OldMaid_StartingWithQueen => GS(37005369136672768L);

		public static string Game_OldMaid_StartingWithoutQueen => GS(37005500103815168L);

		public static string Game_OldMaid_Waiting => GS(37006170395537408L);

		public static string Game_OldMaid_PlayerPickingStartWithQueen => GS(37006206491717632L);

		public static string Game_OldMaid_PlayerPickingStartWithoutQueen => GS(37006398297239552L);

		public static string Game_OldMaid_PlayerPickingDoesntMatter => GS(37006908236525568L);

		public static string Game_OldMaid_PlayerPickedQueenAfterLying => GS(37007086569943040L);

		public static string Game_OldMaid_PlayerPickedQueen => GS(37007252152676352L);

		public static string Game_OldMaid_PlayerPickedCardOtherThanQueenWhileBotHasQueen => GS(37007384810123264L);

		public static string Game_OldMaid_BotPickingCardsHover => GS(37007703203934208L);

		public static string Game_OldMaid_BotPickingCardsChosen => GS(37007761286656000L);

		public static string Game_OldMaid_BotChooseQueen => GS(37007916232634368L);

		public static string Game_OldMaid_BotChooseOtherWhilePlayerHasQueen => GS(37007950978248704L);

		public static string Game_OldMaid_PlayerWon => GS(37008621588738048L);

		public static string Game_OldMaid_PlayerLost => GS(37008665297580032L);

		public static string Game_OldMaid_Summary => GS(37008920160268288L);

		public static string Game_ThanksForPlaying => GS(37018473102864384L);

		public static string Game_OldMaid_HintDont => GS(37018886514438144L);

		public static string Game_OldMaid_HintDo => GS(37018961298878464L);

		public static string Game_OldMaid_HintNoInfo => GS(37019204346212352L);

		public static string Game_OldMaid_PlayerLeft => GS(37246397311643648L);

		public static string Game_OldMaid_Summary_Left => GS(37247700263792640L);

		public static string Achievement_ending_KilledByADirtyCop1 => GS(38074455498297344L);

		public static string Achievement_ending_KilledByADirtyCop2 => GS(38074638151847936L);

		public static string Game_OldMaid_StartGamePlayer => GS(38333175650193408L);

		public static string Game_OldMaid_StartGameBot => GS(38333588281626624L);

		public static string MentalHealth_HeadacheHigh => GS(40262055420104704L);

		public static string MentalHealth_HeadacheLow => GS(40262059543105536L);

		public static string VeryHungry_HighHealth => GS(40261798590287872L);

		public static string VeryHungry_MediumHealth => GS(40261805439586304L);

		public static string VeryHungry_LowHealth => GS(40261821143060480L);

		public static string MediumHungry_HighHealth => GS(40261838666862592L);

		public static string MediumHungry_MediumHealth => GS(40261840285863936L);

		public static string MediumHungry_LowHealth => GS(40261841682567168L);

		public static string LittleHungry_HighHealth => GS(40261887589224448L);

		public static string LittleHungry_LowHealth => GS(40261890986610688L);

		public static string LittleHungry_MediumHealth => GS(40261889208225792L);

		public static string StreamEventRequirement_CumInside => GS(78731296074764288L);

		public static string StreamEventRequirement_CumOutside => GS(78731297593102336L);

		public static string StreamEventRequirement_CumInsideAnal => GS(78731298868170752L);

		public static string StreamEventRequirement_CumInsideMouth => GS(78731300428451840L);

		public static string StreamEventRequirement_GotFlacid => GS(78731935706124288L);

		public static string StreamEventRequirement_Insert => GS(78738233873010688L);

		public static string StreamEventRequirement_Pullout => GS(78738256622915584L);

		public static string Sender_Unknown => GS(79021807289372672L);

		public static string Delivery_StatusToBeCollected => GS(79040422487134208L);

		public static string Delivery_StatusToBeSorted => GS(79040500077563904L);

		public static string Delivery_DeliveryDispatched => GS(79040503714025472L);

		public static string Delivery_DeliverySoon => GS(79040651068313600L);

		public static string Delivery_PredictedTimeSoon => GS(79041449756069888L);

		public static string Sender_BigDaddyHurt => GS(79046713855053824L);

		public static string Sender_Multiple => GS(79368941532483584L);

		public static string MentalHealth_DeathDoor => GS(91351293984903168L);

		public static string WhatTheHell => GS(109890604040589312L);

		public static string IAmScared => GS(109893380904718336L);

		public static string ColorName_VolksAutoGrey => GS(110467144243613696L);

		public static string ColorName_NavyGreen => GS(117395373416124416L);

		public static string ColorName_TempleCyan => GS(118217092112453632L);

		public static string ColorName_AvocadoGreen => GS(118217092112453633L);

		public static string ColorName_LunarPrincessGrey => GS(118217092112453634L);

		public static string ColorName_SatinBlack => GS(118217092112453635L);

		public static string ColorName_StilouBlue => GS(118217092112453636L);

		public static string ColorName_StilouRed => GS(118217092112453637L);

		public static string ColorName_StilouYellow => GS(118217092112453638L);

		public static string ColorName_MonizmedBurgundy => GS(118217092112453639L);

		public static string ColorName_MonizmedPurple => GS(118217092112453640L);

		public static string ColorName_DiabetePurple => GS(118217092112453641L);

		public static string ColorName_DiabeteGold => GS(118217092112453642L);

		public static string ColorName_DiabeteColaRed => GS(118217092112453643L);

		public static string ColorName_DiabeteColaYellow => GS(118217092112453644L);

		public static string ColorName_DiabeteColaBlue => GS(118217092112453645L);

		public static string ColorName_DiabeteDrSugarRed => GS(118217092112453646L);

		public static string ColorName_DiabeteDrSugarBlue => GS(118217092112453647L);

		public static string ColorName_DiabeteSweetPotatoBrown => GS(118217092112453648L);

		public static string ColorName_DiabeteSweetPotatoOrange => GS(118217092112453649L);

		public static string ColorName_CazinoGreen => GS(118217092112453650L);

		public static string ColorName_CazinoBrown => GS(118217092112453651L);

		public static string Cowgirl_RemovePants => GS(120033781170966528L);

		public static string Cowgirl_BeforeInsert => GS(120034458442010624L);

		public static string Cowgirl_BeforeInsertBotLead => GS(120034827842752512L);

		public static string Cowgirl_CumInMouth => GS(120039386887622656L);

		public static string Cowgirl_CumInside => GS(120039449936400384L);

		public static string Cowgirl_CumOutside => GS(120039509310967808L);

		public static string Cowgirl_DidntDecide => GS(120040555219390464L);

		public static string Frick_CumOutside => GS(120043369240858624L);

		public static string Cowgirl_CumQuestion => GS(120045205435846656L);

		public static string Cowgirl_EdgeVeryClose => GS(120136478058483712L);

		public static string NunRepair_EquipedItem => GS(120730844321943552L);

		public static string NunRepair_Denied => GS(120731892331712512L);

		public static string NunRepair_DeniedNoMoney => GS(120732450169950208L);

		public static string NunRepair_RepairStarted => GS(120742247099404288L);

		public static string ColorName_Black => GS(121122169009606656L);

		public static string Achievement_ending_theyKnowOblivious => GS(121130260094652416L);

		public static string Achievement_ending_theyKnow => GS(121130260115623936L);

		public static string Achievement_bigdaddyhurtSecretEpilogue => GS(121152291804680192L);

		public static string Cowgirl_CumTogether => GS(122058373351739392L);

		public static string Cowgirl_EdgeForceStopped => GS(122691757786664960L);

		public static string Cowgirl_FlacidReaction => GS(122719261100478464L);

		public static string Cowgirl_EdgingBreak => GS(122729071132938240L);

		public static string Frick_BotCum => GS(122925642265006080L);

		public static string Cowgirl_CumAlone => GS(122685814826995712L);

		public static string Stage2Headpat1 => GS(188398410854699008L);

		public static string Stage2Headpat2 => GS(188398413132206080L);

		public static string Stage2Headpat3 => GS(188398414898008064L);

		public static string Word_Brother => GS(192453224328728576L);

		public static string Word_Sister => GS(192453258243870720L);

		public static string Outside_RiotsDanger => GS(196436563699163136L);

		public static string Fishing_PollutedLakeName => GS(268423275278753792L);

		public static string Fishing_PollutedLakeDescription => GS(268423666603122688L);

		public static string Fishing_TrashedRiverName => GS(268424045873061888L);

		public static string Fishing_TrashedRiverDescription => GS(268424115800498176L);

		public static string Fishing_PremiumFishFarmName => GS(268424601735782400L);

		public static string Fishing_PremiumFishFarmDescription => GS(268424603933597696L);

		public static string Fishing_MicroplasticsCoastName => GS(272492651078475776L);

		public static string Fishing_MicroplasticsCoastDescription => GS(272492655176310784L);

		public static string ColorName_Bastille => GS(276056677704261632L);

		public static string ColorName_Mirage => GS(276056677775564800L);

		public static string ColorName_Toledo => GS(276056677821702144L);

		public static string ColorName_Bunker => GS(276056677872033792L);

		public static string ColorName_HippiePink => GS(276056677918171136L);

		public static string ColorName_LinkWater => GS(276056677964308480L);

		public static string ColorName_TurquoiseBlue => GS(276056678014640128L);

		public static string ColorName_Amethyst => GS(276056678060777472L);

		public static string ColorName_Fern => GS(276056678111109120L);

		public static string ColorName_DarkPink => GS(276056678157246464L);

		public static string Sender_FishingWebsite => GS(326943858014281728L);

		public static string FoodSatiation_FullMeal => GS(296384348640239616L);

		public static string FoodSatiation_BarelyABite => GS(296384402218278912L);

		public static string FoodSatiation_Meal => GS(296384984555446272L);

		public static string FoodSatiation_Snack => GS(296385004788768768L);

		public static string FoodSatiation_LightMeal => GS(296384997876555776L);

		public static string ColorName_EbonyClay => GS(301479725122969600L);

		public static string ColorName_Rope => GS(301479725148135424L);

		public static string ColorName_Leather => GS(301479725148135425L);

		public static string ColorName_CopperRust => GS(301479725148135426L);

		public static string ColorName_MellowApricot => GS(301479725148135427L);

		public static string ColorName_InkBlue => GS(301479725148135428L);

		public static string ColorName_WeatheredGray => GS(301479725148135430L);

		public static string ColorName_DarkLeather => GS(301481769946832896L);

		public static string ColorName_BlackLeather => GS(301481769967804416L);

		public static string ColorPalette_OldPartPalette => GS(301524803753336832L);

		public static string ColorPalette_DefaultPartPalette => GS(301524803753336833L);

		public static string ColorPalette_GrayScalePalette => GS(301524803753336834L);

		public static string ColorPalette_JunPalette => GS(301524803753336835L);

		public static string ColorPalette_SkinPalette => GS(301524803753336836L);

		public static string ColorPalette_LeatherPalette => GS(301524803753336837L);

		public static string ColorPalette_OtherPalette => GS(301524803753336838L);

		public static string ColorPalette_HotPinkPalette => GS(301524803753336839L);

		public static string ColorPalette_XoulionMainPalette => GS(301524803753336840L);

		public static string ColorPalette_XoulionLogoPalette => GS(301524803753336841L);

		public static string ColorPalette_SkinTonePalette => GS(301567685663121408L);

		public static string ColorName_Burgundy => GS(301567685688287232L);

		public static string ColorName_DarkUmber => GS(301567685688287233L);

		public static string ColorName_Sienna => GS(301567685688287234L);

		public static string ColorName_Sandstone => GS(301567685688287235L);

		public static string ColorName_Almond => GS(301567685688287236L);

		public static string ColorName_LightPeach => GS(301567685688287237L);

		public static string ColorName_DeepMahogany => GS(301567685688287238L);

		public static string ColorName_Bronze => GS(301567685688287239L);

		public static string ColorName_GoldenHoney => GS(301567685688287240L);

		public static string ColorName_WarmIvory => GS(301567685688287241L);

		public static string ColorName_PalePorcelain => GS(301567685688287242L);

		public static string ColorName_GoblinGreen => GS(301567685688287243L);

		public static string ColorName_SoftClay => GS(301572741179367424L);

		public static string ColorName_Rosewood => GS(301572741179367425L);

		public static string ColorName_Tawny => GS(301572741179367426L);

		public static string ColorName_BurntSienna => GS(301572741179367427L);

		public static string ColorName_EarthBrown => GS(301572741179367428L);

		public static string ColorName_PaleDoomer => GS(301572741179367429L);

		public static string ColorName_AnnaliesViolet => GS(313837547580022784L);

		public static string Achievement_ending_unluckyEncounterWithLead => GS(321774208251060224L);

		public static string Achievement_ending_itWasntSupposedToEndLikeThis => GS(321818294404243456L);

		public static string Achievement_ending_friendsTillTheVeryEnd => GS(321823121737768960L);

		public static string Outside_DAKDanger => GS(321874011689574400L);

		public static string Achievement_ending_victimOfARevolution => GS(321883160968159232L);

		public static string Dish_AbsolutelyButchered => GS(326149039096586240L);

		public static string Dish_VeryBad => GS(326149122840059904L);

		public static string Dish_Bad => GS(326149139432726528L);

		public static string Dish_Average => GS(326149140951064576L);

		public static string Dish_Tasty => GS(326149142440042496L);

		public static string Dish_Delicious => GS(326149173121376256L);

		public static string Dish_Exquisite => GS(326149183456141312L);

		public static string Dish_Masterpiece => GS(326149200812171264L);

		public static string Dish_WorldClass => GS(326149218306613248L);

		public static string Achievement_ending_grenade => GS(328734652681412608L);

		public static string Report3File => GS(329484386803113984L);

		public static string Report2File => GS(329484473197387776L);

		public static string Report1File => GS(329485745719861248L);

		public static string JournalFile1 => GS(330996496531972096L);

		public static string JournalFile2 => GS(329503582547075072L);

		public static string JournalFile3 => GS(329503584661004288L);

		public static string Shower_AfterSex_CleanCum => GS(474009182340636672L);

		public static string Shower_AfterSex_LeaveItOn => GS(474009187306692608L);

		public static string Achievement_ending_islandAnnalie => GS(483386357271486464L);

		public static string Achievement_ending_islandLandlord => GS(483386357300846592L);

		public static string Achievement_ending_islandLandlordEveryone => GS(483386357300846593L);

		public static string Achievement_ending_revengeOfTheHomeless => GS(483717650156871680L);

		public static string Achievement_ending_revengeOfTheHomelessBeforeTheFlight => GS(483770983890542592L);

		public static string Fishing_ImaginarySeaName => GS(542204484809383936L);

		public static string Fishing_ImaginarySeaDescription => GS(542204545714872320L);

		public static string Achievement_ending_veryBadTrip => GS(544684971587002368L);

		public static string Achievement_badTripSavedByMelissa => GS(544684971612168192L);

		public static string Achievement_lastTripMetDad => GS(544944266295631872L);

		public static string Achievement_defeatedBotInFishingDuel => GS(544944266320797696L);

		public static string Achievement_helpedAnnalieDuringRiots => GS(544960584205983744L);

		public static string Achievement_helpedPristbotDuringRiots => GS(544960584210178048L);

		public static string Achievement_melissaFriendsDead => GS(544961927830298624L);

		public static string Achievement_melissaFriendsLeaves => GS(544961927834492928L);

		public static string Achievement_melissaDestory => GS(544961927834492929L);

		public static string Achievement_melissaLeave => GS(544961927834492930L);

		public static string Achievement_shower => GS(544964046893342720L);

		public static string Achievement_sleepingTogether => GS(544964046893342721L);

		public static string Achievement_runway_Landlord => GS(544964046893342722L);

		public static string Achievement_runway_Shanice => GS(544964046893342723L);

		public static string Achievement_runway_Fisher => GS(544964046893342724L);

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string Shop_StatusAnnalie(object arg0, object arg1)
		{
			return GS(3155255940222976L, arg0, arg1);
		}

		public static string LostMoney(object arg0)
		{
			return GS(46677620225347584L, arg0);
		}

		public static string GotSweets(object arg0)
		{
			return GS(46801134785794048L, arg0);
		}

		public static string Delivery_PredictedTime(object arg0, object arg1)
		{
			return GS(79040864239620096L, arg0, arg1);
		}

		public static string GotItem(object arg0)
		{
			return GS(111561705527316480L, arg0);
		}

		public static string NunRepair_Busy(object arg0)
		{
			return GS(120733919916986368L, arg0);
		}

		public static string NunRepair_RepairResult(object arg0, object arg1, object arg2)
		{
			return GS(120739286138884096L, arg0, arg1, arg2);
		}

		public static string CowGayMeter(object arg0, object arg1)
		{
			return GS(290361684091985920L, arg0, arg1);
		}

		public static LOS.TupleReference GetTupleReference(long entryId)
		{
			return new LOS.TupleReference("Other", entryId);
		}
	}

	public static class Polyglot
	{
		public static class KeyID
		{
			public const long LANGUAGE_CODE = 907575955456L;

			public const long LANGUAGE_EN = 907601121280L;

			public const long LANGUAGE_DIRECTION = 1066867232768L;

			public const long CONTEXT_CHARACTER_CLASS = 907605315585L;

			public const long CONTEXT_CHARACTER_DELETE = 907605315586L;

			public const long CONTEXT_CHARACTER_NEW = 907605315587L;

			public const long CONTEXT_CLASS_AI = 907605315588L;

			public const long CONTEXT_CLASS_ALIEN = 907605315589L;

			public const long CONTEXT_CLASS_CLONE = 907605315590L;

			public const long CONTEXT_CLASS_CYBORG = 907605315591L;

			public const long CONTEXT_CLASS_DEITY = 907605315592L;

			public const long CONTEXT_CLASS_DEMON = 907605315593L;

			public const long CONTEXT_CLASS_DEMONNESS = 907605315594L;

			public const long CONTEXT_CLASS_DRAGON = 907605315595L;

			public const long CONTEXT_CLASS_DRAGON_PLURAL = 907605315596L;

			public const long CONTEXT_CLASS_GHOST = 907605315597L;

			public const long CONTEXT_CLASS_GOD = 907605315598L;

			public const long CONTEXT_CLASS_GOD_PLURAL = 907605315599L;

			public const long CONTEXT_CLASS_GODDESS = 907605315600L;

			public const long CONTEXT_CLASS_MAGE = 907605315601L;

			public const long CONTEXT_CLASS_MEDIC = 907605315602L;

			public const long CONTEXT_CLASS_MONSTER = 907605315603L;

			public const long CONTEXT_CLASS_NINJA = 907605315604L;

			public const long CONTEXT_CLASS_PIRATE = 907605315605L;

			public const long CONTEXT_CLASS_PRIEST = 907605315606L;

			public const long CONTEXT_CLASS_PRINCE = 907605315607L;

			public const long CONTEXT_CLASS_PRINCESS = 907605315608L;

			public const long CONTEXT_CLASS_ROBOT = 907605315609L;

			public const long CONTEXT_CLASS_SKELETON = 907605315610L;

			public const long CONTEXT_CLASS_SNIPER = 907605315611L;

			public const long CONTEXT_CLASS_SOLDIER = 907605315612L;

			public const long CONTEXT_CLASS_THIEF = 907605315613L;

			public const long CONTEXT_CLASS_WARRIOR = 907605315614L;

			public const long CONTEXT_CLASS_WIZARD = 907605315615L;

			public const long CONTEXT_CLASS_ZOMBIE = 907605315616L;

			public const long CONTEXT_CLASS_ZOMBIE_PLURAL = 907605315617L;

			public const long CONTEXT_COMPETITION_FIRST = 907605315618L;

			public const long CONTEXT_COMPETITION_PLACE_NOUN = 907605315619L;

			public const long CONTEXT_COMPETITION_SECOND = 907605315620L;

			public const long CONTEXT_COMPETITION_THIRD = 907605315621L;

			public const long CONTEXT_CONDITION_BURNING = 907605315622L;

			public const long CONTEXT_CONDITION_CRITICAL = 907605315623L;

			public const long CONTEXT_CONDITION_DEAD = 907605315624L;

			public const long CONTEXT_CONDITION_FROZEN = 907605315625L;

			public const long CONTEXT_CONDITION_POISONED = 907605315626L;

			public const long CONTEXT_CONDITION_SLOWED = 907605315627L;

			public const long CONTEXT_CONDITION_STUNNED = 907605315628L;

			public const long CONTEXT_CONDITION_UNCONSCIOUS = 907605315629L;

			public const long CONTEXT_CONDITION_UNHURT = 907605315630L;

			public const long CONTEXT_CONDITION_WET = 907605315631L;

			public const long CONTEXT_CONDITION_WOUNDED = 907605315632L;

			public const long CONTEXT_DESCRIPTION_BIG = 907605315633L;

			public const long CONTEXT_DESCRIPTION_HUGE = 907605315634L;

			public const long CONTEXT_DESCRIPTION_SMALL = 907605315635L;

			public const long CONTEXT_DESCRIPTION_TINY = 907605315636L;

			public const long CONTEXT_ELEMENT_AIR = 907605315637L;

			public const long CONTEXT_ELEMENT_EARTH = 907605315638L;

			public const long CONTEXT_ELEMENT_FIRE = 907605315639L;

			public const long CONTEXT_ELEMENT_METAL = 907605315640L;

			public const long CONTEXT_ELEMENT_WATER = 907605315641L;

			public const long CONTEXT_GAMBLING_BET = 907605315642L;

			public const long CONTEXT_GAMBLING_DISCARD = 907605315643L;

			public const long CONTEXT_GAMBLING_JACKPOT = 907605315644L;

			public const long CONTEXT_GAMBLING_MAX = 907605315645L;

			public const long CONTEXT_GAMBLING_MIN = 907605315646L;

			public const long CONTEXT_ITEM_ARMOR = 907605315647L;

			public const long CONTEXT_ITEM_ARROW = 907605315648L;

			public const long CONTEXT_ITEM_ARROW_PLURAL = 907605315649L;

			public const long CONTEXT_ITEM_BOARD = 907605315650L;

			public const long CONTEXT_ITEM_BOMB = 907609509888L;

			public const long CONTEXT_ITEM_BOOK = 907609509889L;

			public const long CONTEXT_ITEM_BOOTS = 907609509890L;

			public const long CONTEXT_ITEM_BOW = 907609509891L;

			public const long CONTEXT_ITEM_CARD = 907609509892L;

			public const long CONTEXT_ITEM_CARD_PLURAL = 907609509893L;

			public const long CONTEXT_ITEM_CHEST = 907609509894L;

			public const long CONTEXT_ITEM_COND_MAGICAL = 907609509895L;

			public const long CONTEXT_ITEM_COND_RUSTY = 907609509896L;

			public const long CONTEXT_ITEM_DAGGER = 907609509897L;

			public const long CONTEXT_ITEM_DIE_NOUN = 907609509898L;

			public const long CONTEXT_ITEM_DIE_PLURAL = 907609509899L;

			public const long CONTEXT_ITEM_DOOR = 907609509900L;

			public const long CONTEXT_ITEM_GAUNTLETS = 907609509901L;

			public const long CONTEXT_ITEM_GEM = 907609509902L;

			public const long CONTEXT_ITEM_GIFT_NOUN = 907609509903L;

			public const long CONTEXT_ITEM_GIFT_PLURAL = 907609509904L;

			public const long CONTEXT_ITEM_GLOVE = 907609509905L;

			public const long CONTEXT_ITEM_GUN = 907609509906L;

			public const long CONTEXT_ITEM_HAMMER = 907609509907L;

			public const long CONTEXT_ITEM_HELMET = 907609509908L;

			public const long CONTEXT_ITEM_KEY = 907609509909L;

			public const long CONTEXT_ITEM_KNIFE = 907609509910L;

			public const long CONTEXT_ITEM_LOCK = 907609509911L;

			public const long CONTEXT_ITEM_LOVE = 907609509912L;

			public const long CONTEXT_ITEM_MACHINE_ITEM_GUN = 907609509913L;

			public const long CONTEXT_ITEM_MAGIC = 907609509914L;

			public const long CONTEXT_ITEM_PISTOL = 907609509915L;

			public const long CONTEXT_ITEM_QUEST_NOUN = 907609509916L;

			public const long CONTEXT_ITEM_RANK_NOUN = 907609509917L;

			public const long CONTEXT_ITEM_RIFLE = 907609509918L;

			public const long CONTEXT_ITEM_ROCKET_ITEM_LAUNCHER = 907609509919L;

			public const long CONTEXT_ITEM_RPG = 907609509920L;

			public const long CONTEXT_ITEM_SCROLL = 907609509921L;

			public const long CONTEXT_ITEM_SHIELD = 907609509922L;

			public const long CONTEXT_ITEM_SHIELD_PLURAL = 907609509923L;

			public const long CONTEXT_ITEM_SHIELD_SCIFI = 907609509924L;

			public const long CONTEXT_ITEM_SHIELD_SCIFI_PLURAL = 907609509925L;

			public const long CONTEXT_ITEM_SHOTGUN = 907609509926L;

			public const long CONTEXT_ITEM_SNIPER_ITEM_RIFLE = 907609509927L;

			public const long CONTEXT_ITEM_SPELL = 907609509928L;

			public const long CONTEXT_ITEM_STAFF = 907609509929L;

			public const long CONTEXT_ITEM_SWORD = 907609509930L;

			public const long CONTEXT_ITEM_TREASURE = 907609509931L;

			public const long CONTEXT_ITEM_TYPE_EPIC = 907609509932L;

			public const long CONTEXT_ITEM_TYPE_JUNK = 907609509933L;

			public const long CONTEXT_ITEM_WAND = 907609509934L;

			public const long CONTEXT_ITEM_WOOD = 907609509935L;

			public const long CONTEXT_MACHINE_ALTITUDE = 907613704192L;

			public const long CONTEXT_MACHINE_BRAKES = 907613704193L;

			public const long CONTEXT_MACHINE_CHASSIS = 907613704194L;

			public const long CONTEXT_MACHINE_DEPTH = 907613704195L;

			public const long CONTEXT_MACHINE_FUEL = 907613704196L;

			public const long CONTEXT_MACHINE_GEAR = 907613704197L;

			public const long CONTEXT_MACHINE_HANDLING = 907613704198L;

			public const long CONTEXT_MACHINE_SPEED = 907613704199L;

			public const long CONTEXT_MACHINE_STEERING = 907613704200L;

			public const long CONTEXT_MACHINE_SUSPENSION = 907613704201L;

			public const long CONTEXT_MACHINE_WHEELS = 907613704202L;

			public const long CONTEXT_MACHINE_WINGS = 907613704203L;

			public const long CONTEXT_MEDAL = 907613704204L;

			public const long CONTEXT_MEDAL_BRONZE = 907613704205L;

			public const long CONTEXT_MEDAL_GOLD = 907613704206L;

			public const long CONTEXT_MEDAL_SILVER = 907613704207L;

			public const long CONTEXT_METAL_BRONZE = 907613704208L;

			public const long CONTEXT_METAL_COIN = 907613704209L;

			public const long CONTEXT_METAL_GOLD = 907613704210L;

			public const long CONTEXT_METAL_INGOT = 907613704211L;

			public const long CONTEXT_METAL_PLATINUM = 907613704212L;

			public const long CONTEXT_METAL_SILVER = 907613704213L;

			public const long CONTEXT_PLACE_CASTLE = 907613704214L;

			public const long CONTEXT_PLACE_CAVE = 907613704215L;

			public const long CONTEXT_PLACE_DUNGEON = 907613704216L;

			public const long CONTEXT_PLACE_INN = 907613704217L;

			public const long CONTEXT_PLACE_PLANET = 907613704218L;

			public const long CONTEXT_PLACE_PLANET_EARTH = 907613704219L;

			public const long CONTEXT_PLACE_SPACE = 907613704220L;

			public const long CONTEXT_PLACE_SPACESHIP = 907613704221L;

			public const long CONTEXT_STORY = 907613704222L;

			public const long CONTEXT_STORY_ADVENTURE = 907613704223L;

			public const long CONTEXT_STORY_CHAPTER = 907613704224L;

			public const long CONTEXT_STORY_PAGE = 907613704225L;

			public const long CREDITS_GAME = 907613704226L;

			public const long CREDITS_GAME_DEVELOPMENT = 907613704227L;

			public const long CREDITS_GAME_DEVELOPER = 907613704228L;

			public const long CREDITS_DEVELOPED_BY = 907613704229L;

			public const long CREDITS_PRODUCTION = 907613704230L;

			public const long CREDITS_PRODUCER = 907613704231L;

			public const long CREDITS_PUBLISHER = 907613704232L;

			public const long CREDITS_DIRECTOR = 907613704233L;

			public const long CREDITS_LEAD = 907613704234L;

			public const long CREDITS_ASSISTANT = 907613704235L;

			public const long CREDITS_ASSOCIATE = 907613704236L;

			public const long CREDITS_SENIOR = 907613704237L;

			public const long CREDITS_JUNIOR = 907613704238L;

			public const long CREDITS_CHIEF_TECH_OFFICER = 907613704239L;

			public const long CREDITS_TECHNICAL_DIRECTOR = 907613704240L;

			public const long CREDITS_CREATIVE_DIRECTOR = 907613704241L;

			public const long CREDITS_ANIMATION_DIRECTOR = 907613704242L;

			public const long CREDITS_ART_DIRECTOR = 907613704243L;

			public const long CREDITS_AUDIO_DIRECTOR = 907613704244L;

			public const long CREDITS_DESIGN_DIRECTOR = 907613704245L;

			public const long CREDITS_PROGRAM_MANAGER = 907613704246L;

			public const long CREDITS_PROJECT_MANAGER = 907613704247L;

			public const long CREDITS_GAME_DESIGN = 907613704248L;

			public const long CREDITS_GAME_DESIGNER = 907613704249L;

			public const long CREDITS_CODE = 907613704250L;

			public const long CREDITS_PROGRAMMING = 907613704251L;

			public const long CREDITS_PROGRAMMER = 907613704252L;

			public const long CREDITS_PROGRAMMER_PLURAL = 907613704253L;

			public const long CREDITS_LEAD_PROGRAMMER = 907613704254L;

			public const long CREDITS_GRAPHIC_DESIGNER = 907613704255L;

			public const long CREDITS_TECHNICAL_ARTIST = 907613704256L;

			public const long CREDITS_CONCEPT_ARTIST = 907613704257L;

			public const long CREDITS_CHARACTER_DESIGNER = 907613704258L;

			public const long CREDITS_ART = 907613704259L;

			public const long CREDITS_ARTIST = 907613704260L;

			public const long CREDITS_ARTIST_PLURAL = 907613704261L;

			public const long CREDITS_LEVEL_DESIGN = 907613704262L;

			public const long CREDITS_LEVEL_DESIGNER = 907613704263L;

			public const long CREDITS_CINEMATICS_DIRECTOR = 907613704264L;

			public const long CREDITS_CINEMATIC_ANIMATOR = 907613704265L;

			public const long CREDITS_INTERFACE_ARTIST = 907613704266L;

			public const long CREDITS_COMPOSER = 907613704267L;

			public const long CREDITS_MUSICIAN = 907613704268L;

			public const long CREDITS_SPECIAL_THANKS = 907613704269L;

			public const long CREDITS_SPECIAL_THANKS_TO = 907613704270L;

			public const long CREDITS_IMAGE = 907617898496L;

			public const long CREDITS_IMAGE_PLURAL = 907617898497L;

			public const long CREDITS_FONT = 907617898498L;

			public const long CREDITS_FONT_PLURAL = 907617898499L;

			public const long CREDITS_SOUNDTRACK = 907617898500L;

			public const long CREDITS_WRITER = 907617898501L;

			public const long CREDITS_MANUSCRIPT = 907617898502L;

			public const long CREDITS_SCRIPT_WRITER = 907617898503L;

			public const long CREDITS_ANIMATION = 907617898504L;

			public const long CREDITS_ANIMATOR = 907617898505L;

			public const long CREDITS_VOICE_ACTING = 907617898506L;

			public const long CREDITS_VOICE_ACTOR = 907617898507L;

			public const long CREDITS_SOUND_DESIGNER = 907617898508L;

			public const long CREDITS_SOUND_ENGINEER = 907617898509L;

			public const long CREDITS_SFX = 907617898510L;

			public const long CREDITS_VFX = 907617898511L;

			public const long CREDITS_RECORDIST = 907617898512L;

			public const long CREDITS_FOLEY_ARTIST = 907617898513L;

			public const long CREDITS_TEST_MANAGER = 907617898514L;

			public const long CREDITS_TEST_LEAD = 907617898515L;

			public const long CREDITS_TESTER = 907617898516L;

			public const long CREDITS_DATABASE_ADMIN = 907617898517L;

			public const long CREDITS_3D_MODELING = 907617898518L;

			public const long CREDITS_MODELS = 907617898519L;

			public const long CREDITS_LOCALIZATION = 907617898520L;

			public const long CREDITS_TRANSLATION = 907617898521L;

			public const long CREDITS_POLYGLOT = 907617898522L;

			public const long GAME_GENRE_ACTION = 907617898523L;

			public const long GAME_GENRE_ADVENTURE = 907617898524L;

			public const long GAME_GENRE_CARD = 907617898525L;

			public const long GAME_GENRE_CASUAL = 907617898526L;

			public const long GAME_GENRE_FANTASY = 907617898527L;

			public const long GAME_GENRE_HARDCORE = 907617898528L;

			public const long GAME_GENRE_INDIE = 907617898529L;

			public const long GAME_GENRE_MEDIEVAL = 907617898530L;

			public const long GAME_GENRE_MISC = 907617898531L;

			public const long GAME_GENRE_MULTIPLAYER = 907617898532L;

			public const long GAME_GENRE_NAME = 907617898533L;

			public const long GAME_GENRE_PLATFORMER = 907617898534L;

			public const long GAME_GENRE_PUZZLE = 907617898535L;

			public const long GAME_GENRE_QUIZ = 907617898536L;

			public const long GAME_GENRE_RACING = 907617898537L;

			public const long GAME_GENRE_RETRO = 907617898538L;

			public const long GAME_GENRE_RPG = 907617898539L;

			public const long GAME_GENRE_RTS = 907617898540L;

			public const long GAME_GENRE_SCIFI = 907617898541L;

			public const long GAME_GENRE_SHOOTER = 907617898542L;

			public const long GAME_GENRE_SIMULATION = 907617898543L;

			public const long GAME_GENRE_SINGLEPLAYER = 907617898544L;

			public const long GAME_GENRE_SPORTS = 907617898545L;

			public const long GAME_GENRE_STRATEGY = 907617898546L;

			public const long GAME_GENRE_TBS = 907617898547L;

			public const long GAME_GENRE_TOWER_DEFENSE = 907617898548L;

			public const long ERROR_DEVICE_NOT_FOUND = 907617898549L;

			public const long ERROR_DEVICE_VR_LEAVING_TRACKAREA = 907617898550L;

			public const long ERROR_DEVICE_VR_NO_HMD = 907617898551L;

			public const long ERROR_GENERIC_ERRCODE = 907617898552L;

			public const long ERROR_GENERIC_TEXT = 907617898553L;

			public const long ERROR_GENERIC_INVALID_NAME = 907617898554L;

			public const long ERROR_GENERIC_INVALID_CD_KEY = 907617898555L;

			public const long ERROR_GENERIC_CHEATING = 907617898556L;

			public const long ERROR_NETWORK_CONNECTION_LOST = 907617898557L;

			public const long ERROR_NETWORK_DISCONNECTED_YOUR_OPPONENT = 907617898558L;

			public const long ERROR_NETWORK_DISCONNECTED_YOUR_PLAYER = 907617898559L;

			public const long ERROR_NETWORK_DISCONNECTED_YOUR_TEAMMATE = 907617898560L;

			public const long ERROR_NETWORK_SERVER = 907617898561L;

			public const long ERROR_SAVE_CORRUPTED = 907617898562L;

			public const long ERROR_SAVE_NO_GAMES_FOUND = 907617898563L;

			public const long ERROR_SAVE_NO_OPEN_GAMES = 907622092800L;

			public const long GAME_ACTION_ATTACK = 907622092801L;

			public const long GAME_ACTION_BOOST = 907622092802L;

			public const long GAME_ACTION_BUILD = 907622092803L;

			public const long GAME_ACTION_CAST_FISHING = 907622092804L;

			public const long GAME_ACTION_CAST_SPELL = 907622092805L;

			public const long GAME_ACTION_CLAIM = 907622092806L;

			public const long GAME_ACTION_CLIMB = 907622092807L;

			public const long GAME_ACTION_CLOSE = 907622092808L;

			public const long GAME_ACTION_COLLECT = 907622092809L;

			public const long GAME_ACTION_COOK = 907622092810L;

			public const long GAME_ACTION_CRAFT = 907622092811L;

			public const long GAME_ACTION_CROUCH = 907622092812L;

			public const long GAME_ACTION_DODGE = 907622092813L;

			public const long GAME_ACTION_DRIVE = 907622092814L;

			public const long GAME_ACTION_FLY = 907622092815L;

			public const long GAME_ACTION_FULL_SPEED = 907622092816L;

			public const long GAME_ACTION_GO = 907622092817L;

			public const long GAME_ACTION_GO_TO_PLACE = 907622092818L;

			public const long GAME_ACTION_HALF_SPEED = 907622092819L;

			public const long GAME_ACTION_HEAL = 907622092820L;

			public const long GAME_ACTION_JUMP = 907622092821L;

			public const long GAME_ACTION_LAND = 907622092822L;

			public const long GAME_ACTION_LOOK = 907622092823L;

			public const long GAME_ACTION_MIX = 907622092824L;

			public const long GAME_ACTION_OPEN = 907622092825L;

			public const long GAME_ACTION_PICK_UP = 907622092826L;

			public const long GAME_ACTION_RACE = 907622092827L;

			public const long GAME_ACTION_READ = 907622092828L;

			public const long GAME_ACTION_REPLACE = 907622092829L;

			public const long GAME_ACTION_RUN = 907622092830L;

			public const long GAME_ACTION_RUN_AWAY = 907622092831L;

			public const long GAME_ACTION_SHOOT = 907622092832L;

			public const long GAME_ACTION_SNEAK = 907622092833L;

			public const long GAME_ACTION_SPIN = 907622092834L;

			public const long GAME_ACTION_SPRINT = 907622092835L;

			public const long GAME_ACTION_STEER = 907622092836L;

			public const long GAME_ACTION_TAKE_OFF = 907622092837L;

			public const long GAME_ACTION_TALK = 907622092838L;

			public const long GAME_ACTION_THROW = 907622092839L;

			public const long GAME_ACTION_WALK = 907622092840L;

			public const long GAME_CHARACTER_ENDURANCE = 907622092841L;

			public const long GAME_CHARACTER_EXP = 907622092842L;

			public const long GAME_CHARACTER_FEMALE = 907622092843L;

			public const long GAME_CHARACTER_HEALTH = 907622092844L;

			public const long GAME_CHARACTER_HEART = 907622092845L;

			public const long GAME_CHARACTER_HEART_PLURAL = 907622092846L;

			public const long GAME_CHARACTER_LEVEL = 907622092847L;

			public const long GAME_CHARACTER_LEVEL_UP = 907622092848L;

			public const long GAME_CHARACTER_LIVES = 907622092849L;

			public const long GAME_CHARACTER_MALE = 907622092850L;

			public const long GAME_CHARACTER_MAN = 907622092851L;

			public const long GAME_CHARACTER_MAN_PLURAL = 907622092852L;

			public const long GAME_CHARACTER_RESPAWN = 907622092853L;

			public const long GAME_CHARACTER_RESPAWN_IN = 907622092854L;

			public const long GAME_CHARACTER_RESPAWN_QUESTION = 907622092855L;

			public const long GAME_CHARACTER_RESPAWNING = 907622092856L;

			public const long GAME_CHARACTER_SKILL = 907622092857L;

			public const long GAME_CHARACTER_SPAWN_POINT = 907622092858L;

			public const long GAME_CHARACTER_TRANSGENDER = 907622092859L;

			public const long GAME_CHARACTER_WOMAN = 907622092860L;

			public const long GAME_CHARACTER_WOMAN_PLURAL = 907622092861L;

			public const long GAME_DAMAGE = 907622092862L;

			public const long GAME_DAMAGE_CRITICAL_HIT = 907622092863L;

			public const long GAME_DAMAGE_CRITICAL_MISS = 907622092864L;

			public const long GAME_DAMAGE_HIT_NOUN = 907622092865L;

			public const long GAME_DAMAGE_HIT_POINTS_PLURAL = 907622092866L;

			public const long GAME_DAMAGE_MISS_NOUN = 907622092867L;

			public const long GAME_DAMAGE_UNIT = 907622092868L;

			public const long GAME_DAMAGE_UNIT_PLURAL = 907622092869L;

			public const long GAME_ENEMY = 907622092870L;

			public const long GAME_ENEMY_FEMALE = 907622092871L;

			public const long GAME_ENEMY_FEMALE_PLURAL = 907622092872L;

			public const long GAME_ENEMY_PLURAL = 907622092873L;

			public const long GAME_INVENTORY = 907622092874L;

			public const long GAME_INVENTORY_AMMO = 907622092875L;

			public const long GAME_INVENTORY_AMMUNITION = 907622092876L;

			public const long GAME_INVENTORY_BULLET = 907622092877L;

			public const long GAME_INVENTORY_BULLET_PLURAL = 907622092878L;

			public const long GAME_INVENTORY_BUY = 907622092879L;

			public const long GAME_INVENTORY_CAPACITY = 907622092880L;

			public const long GAME_INVENTORY_DROP = 907622092881L;

			public const long GAME_INVENTORY_ENCUMBRANCE = 907622092882L;

			public const long GAME_INVENTORY_FULL = 907622092883L;

			public const long GAME_INVENTORY_ITEM = 907622092884L;

			public const long GAME_INVENTORY_KEY = 907622092885L;

			public const long GAME_INVENTORY_POWERUP = 907622092886L;

			public const long GAME_INVENTORY_SELL = 907626287104L;

			public const long GAME_INVENTORY_USE = 907626287105L;

			public const long GAME_INVENTORY_WEAPON = 907626287106L;

			public const long GAME_INVENTORY_WEAPONS = 907626287107L;

			public const long GAME_INVENTORY_YOU_CANNOT_CARRY = 907626287108L;

			public const long GAME_LABEL_BE_CAREFUL = 907626287109L;

			public const long GAME_LABEL_PLEASE_BE_CAREFUL = 907626287110L;

			public const long GAME_LABEL_TACTICS = 907626287111L;

			public const long GAME_OBJECTIVE = 907626287112L;

			public const long GAME_OBJECTIVE_ASSIST = 907626287113L;

			public const long GAME_OBJECTIVE_ASSIST_PLURAL = 907626287114L;

			public const long GAME_OBJECTIVE_ATTACK = 907626287115L;

			public const long GAME_OBJECTIVE_BONUS = 907626287116L;

			public const long GAME_OBJECTIVE_CAPTURE = 907626287117L;

			public const long GAME_OBJECTIVE_COMPLETE = 907626287118L;

			public const long GAME_OBJECTIVE_DEATH = 907626287119L;

			public const long GAME_OBJECTIVE_DEATH_PLURAL = 907626287120L;

			public const long GAME_OBJECTIVE_DEFEND = 907626287121L;

			public const long GAME_OBJECTIVE_DESTROY = 907626287122L;

			public const long GAME_OBJECTIVE_ESCAPE = 907626287123L;

			public const long GAME_OBJECTIVE_GAME_FINISHED = 907626287124L;

			public const long GAME_OBJECTIVE_GAMEOVER = 907626287125L;

			public const long GAME_OBJECTIVE_GAMEOVER_CAPS = 907626287126L;

			public const long GAME_OBJECTIVE_HIGH_SCORE = 907626287127L;

			public const long GAME_OBJECTIVE_INCOMPLETE = 907626287128L;

			public const long GAME_OBJECTIVE_KILL = 907626287129L;

			public const long GAME_OBJECTIVE_KILL_ASSIST = 907626287130L;

			public const long GAME_OBJECTIVE_KILL_NOUN = 907626287131L;

			public const long GAME_OBJECTIVE_KILL_PLURAL = 907626287132L;

			public const long GAME_OBJECTIVE_LOCKED = 907626287133L;

			public const long GAME_OBJECTIVE_LOSSES = 907626287134L;

			public const long GAME_OBJECTIVE_MATCH = 907626287135L;

			public const long GAME_OBJECTIVE_MISSION = 907626287136L;

			public const long GAME_OBJECTIVE_MISSION_OBJECTIVE_PLURAL = 907626287137L;

			public const long GAME_OBJECTIVE_MISSION_OBJECTIVE = 907626287138L;

			public const long GAME_OBJECTIVE_MULTIPLIER = 907626287139L;

			public const long GAME_OBJECTIVE_PLURAL = 907626287140L;

			public const long GAME_OBJECTIVE_POSITION = 907626287141L;

			public const long GAME_OBJECTIVE_RACE_LAP = 907626287142L;

			public const long GAME_OBJECTIVE_RACE_LAP_PLURAL = 907626287143L;

			public const long GAME_OBJECTIVE_ROUND = 907626287144L;

			public const long GAME_OBJECTIVE_SCORE = 907626287145L;

			public const long GAME_OBJECTIVE_TIME = 907626287146L;

			public const long GAME_OBJECTIVE_TIME_LEFT = 907626287147L;

			public const long GAME_OBJECTIVE_TOURNAMENT = 907626287148L;

			public const long GAME_OBJECTIVE_TOURNAMENT_PLURAL = 907626287149L;

			public const long GAME_OBJECTIVE_WINS = 907626287150L;

			public const long GAME_STATUS_DEFEAT = 907626287151L;

			public const long GAME_STATUS_DEFEATED = 907626287152L;

			public const long GAME_STATUS_LOSER = 907626287153L;

			public const long GAME_STATUS_TEAM_LOST = 907626287154L;

			public const long GAME_STATUS_TEAM_WON = 907626287155L;

			public const long GAME_STATUS_VICTORY = 907626287156L;

			public const long GAME_STATUS_WINNER = 907626287157L;

			public const long GAME_STATUS_YOU_ARE_DEAD = 907626287158L;

			public const long GAME_STATUS_YOU_DIED = 907626287159L;

			public const long GAME_STATUS_YOU_LOSE = 907626287160L;

			public const long GAME_STATUS_YOU_LOST = 907626287161L;

			public const long GAME_STATUS_YOU_WIN = 907626287162L;

			public const long GAME_STATUS_YOU_WON = 907626287163L;

			public const long MENU_CAMERA = 907626287164L;

			public const long MENU_CAMERA_INVERTED = 907626287165L;

			public const long MENU_CAMERA_NORMAL = 907626287166L;

			public const long MENU_CAMERA_XAXIS = 907626287167L;

			public const long MENU_CAMERA_YAXIS = 907626287168L;

			public const long MENU_CONTROLS_BUTTON = 907626287169L;

			public const long MENU_CONTROLS_CLICK = 907626287170L;

			public const long MENU_CONTROLS_DEADZONE = 907626287171L;

			public const long MENU_CONTROLS_DOUBLE_CLICK = 907626287172L;

			public const long MENU_CONTROLS_DRAG = 907626287173L;

			public const long MENU_CONTROLS_DRAG_DROP = 907626287174L;

			public const long MENU_CONTROLS_FINGER = 907626287175L;

			public const long MENU_CONTROLS_GAMEPAD = 907626287176L;

			public const long MENU_CONTROLS_GYROSCOPE = 907626287177L;

			public const long MENU_CONTROLS_KEY = 907626287178L;

			public const long MENU_CONTROLS_KEYBOARD = 907626287179L;

			public const long MENU_CONTROLS_MOUSE = 907626287180L;

			public const long MENU_CONTROLS_MOUSE_SENSITIVITY = 907626287181L;

			public const long MENU_CONTROLS_PINCH = 907626287182L;

			public const long MENU_CONTROLS_ROTATE = 907626287183L;

			public const long MENU_CONTROLS_SCROLL = 907626287184L;

			public const long MENU_CONTROLS_SHAKE = 907626287185L;

			public const long MENU_CONTROLS_SPREAD = 907630481408L;

			public const long MENU_CONTROLS_SWIPE = 907630481409L;

			public const long MENU_CONTROLS_TAP = 907630481410L;

			public const long MENU_CONTROLS_THE_BUTTON = 907630481411L;

			public const long MENU_CONTROLS_THE_KEY = 907630481412L;

			public const long MENU_CONTROLS_TILT = 907630481413L;

			public const long MENU_CUSTOMIZE_ARM = 907630481414L;

			public const long MENU_CUSTOMIZE_ARM_PLURAL = 907630481415L;

			public const long MENU_CUSTOMIZE_CHEST = 907630481416L;

			public const long MENU_CUSTOMIZE_CHIN = 907630481417L;

			public const long MENU_CUSTOMIZE_COLOR = 907630481418L;

			public const long MENU_CUSTOMIZE_EAR = 907630481419L;

			public const long MENU_CUSTOMIZE_EAR_PLURAL = 907630481420L;

			public const long MENU_CUSTOMIZE_EYE = 907630481421L;

			public const long MENU_CUSTOMIZE_EYE_PLURAL = 907630481422L;

			public const long MENU_CUSTOMIZE_FEET = 907630481423L;

			public const long MENU_CUSTOMIZE_FOOT = 907630481424L;

			public const long MENU_CUSTOMIZE_HAIR = 907630481425L;

			public const long MENU_CUSTOMIZE_HAIR_BEARD = 907630481426L;

			public const long MENU_CUSTOMIZE_HAIR_FUR = 907630481427L;

			public const long MENU_CUSTOMIZE_HAIR_HEAD = 907630481428L;

			public const long MENU_CUSTOMIZE_HAND = 907630481429L;

			public const long MENU_CUSTOMIZE_HAND_PLURAL = 907630481430L;

			public const long MENU_CUSTOMIZE_HEAD = 907630481431L;

			public const long MENU_CUSTOMIZE_HIPS = 907630481432L;

			public const long MENU_CUSTOMIZE_LEG = 907630481433L;

			public const long MENU_CUSTOMIZE_LEG_PLURAL = 907630481434L;

			public const long MENU_CUSTOMIZE_MOUTH = 907630481435L;

			public const long MENU_CUSTOMIZE_NECK = 907630481436L;

			public const long MENU_CUSTOMIZE_NOSE = 907630481437L;

			public const long MENU_CUSTOMIZE_SKIN = 907630481438L;

			public const long MENU_CUSTOMIZE_THORAX = 907630481439L;

			public const long MENU_DIFFICULTY = 907630481440L;

			public const long MENU_DIFFICULTY_EASY = 907630481441L;

			public const long MENU_DIFFICULTY_HARD = 907630481442L;

			public const long MENU_DIFFICULTY_IMPOSSIBLE = 907630481443L;

			public const long MENU_DIFFICULTY_INSANE = 907630481444L;

			public const long MENU_DIFFICULTY_MEDIUM = 907630481445L;

			public const long MENU_DIFFICULTY_VERY_EASY = 907630481446L;

			public const long MENU_DIFFICULTY_VERY_HARD = 907630481447L;

			public const long MENU_DIR_DOWN = 907630481448L;

			public const long MENU_DIR_LEFT = 907630481449L;

			public const long MENU_DIR_RIGHT = 907630481450L;

			public const long MENU_DIR_UP = 907630481451L;

			public const long MENU_IO_LOAD = 907630481452L;

			public const long MENU_IO_LOAD_GAME = 907630481453L;

			public const long MENU_IO_LOADING = 907630481454L;

			public const long MENU_IO_PLEASE_WAIT = 907630481455L;

			public const long MENU_IO_RESET_SAVE_DATA = 907630481456L;

			public const long MENU_IO_SAVE = 907630481457L;

			public const long MENU_IO_SAVE_DATA = 907630481458L;

			public const long MENU_IO_SAVE_GAME = 907630481459L;

			public const long MENU_IO_SAVING = 907630481460L;

			public const long MENU_LABEL_ALL = 907630481461L;

			public const long MENU_LABEL_AGE = 907630481462L;

			public const long MENU_LABEL_ARE_YOU_REALLY_SURE = 907630481463L;

			public const long MENU_LABEL_ARE_YOU_SURE = 907630481464L;

			public const long MENU_LABEL_BACK = 907630481465L;

			public const long MENU_LABEL_BACKGROUND_MUSIC = 907630481466L;

			public const long MENU_LABEL_CANCEL = 907630481467L;

			public const long MENU_LABEL_CLOSE = 907630481468L;

			public const long MENU_LABEL_CONFIRM_BUTTON = 907630481469L;

			public const long MENU_LABEL_CONTINUE = 907630481470L;

			public const long MENU_LABEL_CONTINUE_QUESTION = 907630481471L;

			public const long MENU_LABEL_CREDITS = 907630481472L;

			public const long MENU_LABEL_DESKTOP = 907630481473L;

			public const long MENU_LABEL_DESKTOP_QUESTION = 907630481474L;

			public const long MENU_LABEL_DLC = 907630481475L;

			public const long MENU_LABEL_DOWNLOAD = 907630481476L;

			public const long MENU_LABEL_DOWNLOADABLE = 907630481477L;

			public const long MENU_LABEL_EXIT = 907630481478L;

			public const long MENU_LABEL_EXIT_QUESTION = 907630481479L;

			public const long MENU_LABEL_EXTRAS = 907630481480L;

			public const long MENU_LABEL_FRAMESPERSEC = 907630481481L;

			public const long MENU_LABEL_GALLERY = 907630481482L;

			public const long MENU_LABEL_GOODBYE = 907630481483L;

			public const long MENU_LABEL_GRAPHICS = 907630481484L;

			public const long MENU_LABEL_IAP = 907630481485L;

			public const long MENU_LABEL_LANGUAGE = 907630481486L;

			public const long MENU_LABEL_LEVEL = 907630481487L;

			public const long MENU_LABEL_MAIL = 907630481488L;

			public const long MENU_LABEL_MAINMENU = 907630481489L;

			public const long MENU_LABEL_MORE = 907630481490L;

			public const long MENU_LABEL_MUSIC = 907630481491L;

			public const long MENU_LABEL_MUSIC_PLAYER = 907630481492L;

			public const long MENU_LABEL_NEW_GAME = 907634675712L;

			public const long MENU_LABEL_NEXT = 907634675713L;

			public const long MENU_LABEL_NEXT_TRACK = 907634675714L;

			public const long MENU_LABEL_NO = 907634675715L;

			public const long MENU_LABEL_NO_CAPS = 907634675716L;

			public const long MENU_LABEL_NOPE = 907634675717L;

			public const long MENU_LABEL_OK = 907634675718L;

			public const long MENU_LABEL_PAUSE = 907634675719L;

			public const long MENU_LABEL_PAUSED = 907634675720L;

			public const long MENU_LABEL_PLAY = 907634675721L;

			public const long MENU_LABEL_PLAY_AGAIN = 907760504832L;

			public const long MENU_LABEL_PRESS_ANYKEY = 907760504833L;

			public const long MENU_LABEL_PRESS_ANYKEY_CONTINUE = 907760504834L;

			public const long MENU_LABEL_PRESS_START = 907760504835L;

			public const long MENU_LABEL_PRESS_START_CONTINUE = 907760504836L;

			public const long MENU_LABEL_PREVIOUS = 907760504837L;

			public const long MENU_LABEL_PREVIOUS_TRACK = 907760504838L;

			public const long MENU_LABEL_PRICE = 907764699136L;

			public const long MENU_LABEL_PRICE_FREE = 907764699137L;

			public const long MENU_LABEL_PURCHASE = 907764699138L;

			public const long MENU_LABEL_QUIT = 907764699139L;

			public const long MENU_LABEL_QUIT_CONFIRM = 907764699140L;

			public const long MENU_LABEL_QUIT_QUESTION = 907764699141L;

			public const long MENU_LABEL_REPLAY = 907764699142L;

			public const long MENU_LABEL_RETURN = 907764699143L;

			public const long MENU_LABEL_RETURN_MAIN = 907764699144L;

			public const long MENU_LABEL_RETURN_MAIN_QUESTION = 907764699145L;

			public const long MENU_LABEL_SHARE = 907764699146L;

			public const long MENU_LABEL_SKIP = 907764699147L;

			public const long MENU_LABEL_SOUND = 907764699148L;

			public const long MENU_LABEL_THANK_YOU = 907764699149L;

			public const long MENU_LABEL_UNLOCK = 907764699150L;

			public const long MENU_LABEL_VIDEO = 907764699151L;

			public const long MENU_LABEL_VOICES = 907764699152L;

			public const long MENU_LABEL_WARNING = 907764699153L;

			public const long MENU_LABEL_WELCOME = 907764699154L;

			public const long MENU_LABEL_WORLD = 907764699155L;

			public const long MENU_LABEL_YEAH = 907764699156L;

			public const long MENU_LABEL_YES = 907764699157L;

			public const long MENU_LABEL_YES_CAPS = 907764699158L;

			public const long MENU_LANGUAGE_DIRECTION = 907764699159L;

			public const long MENU_LANGUAGE_THIS = 907764699160L;

			public const long MENU_LANGUAGE_THIS_EN = 907764699161L;

			public const long MENU_LOGIN = 907764699162L;

			public const long MENU_LOGIN_EMAIL = 907764699163L;

			public const long MENU_LOGIN_FORGOT = 907764699164L;

			public const long MENU_LOGIN_PASSWORD = 907764699165L;

			public const long MENU_LOGIN_REGISTER = 907764699166L;

			public const long MENU_LOGIN_REMEMBER = 907764699167L;

			public const long MENU_MODE_CHALLENGE = 907764699168L;

			public const long MENU_MODE_CHALLENGE_MODE = 907764699169L;

			public const long MENU_MODE_COOP = 907764699170L;

			public const long MENU_MODE_COOPERATIVE = 907764699171L;

			public const long MENU_MODE_DEATHMATCH = 907764699172L;

			public const long MENU_MODE_HELP = 907764699173L;

			public const long MENU_MODE_INSTRUCTIONS = 907764699174L;

			public const long MENU_MODE_MULTIPLAYER = 907764699175L;

			public const long MENU_MODE_MULTIPLAYER_LOCAL = 907764699176L;

			public const long MENU_MODE_MULTIPLAYER_ONLINE = 907764699177L;

			public const long MENU_MODE_PLAY_ONLINE = 907764699178L;

			public const long MENU_MODE_SINGLEPLAYER = 907764699179L;

			public const long MENU_MODE_TRAINING = 907764699180L;

			public const long MENU_MODE_TUTORIAL = 907764699181L;

			public const long MENU_MODE_VERSUS = 907764699182L;

			public const long MENU_NAME = 907764699183L;

			public const long MENU_NAME_FIRST = 907764699184L;

			public const long MENU_NAME_LAST = 907764699185L;

			public const long MENU_NAME_USER = 907764699186L;

			public const long MENU_OPTIONS = 907764699187L;

			public const long MENU_OPTIONS_ADVANCEDGRAPHICS = 907764699188L;

			public const long MENU_OPTIONS_ANTIALIASING = 907764699189L;

			public const long MENU_OPTIONS_BLOOM = 907764699190L;

			public const long MENU_OPTIONS_CONTROLS = 907764699191L;

			public const long MENU_OPTIONS_DIFFICULTY = 907764699192L;

			public const long MENU_OPTIONS_DISPLAY = 907764699193L;

			public const long MENU_OPTIONS_FIRST_PERSON_VIEW = 907764699194L;

			public const long MENU_OPTIONS_FOV = 907764699195L;

			public const long MENU_OPTIONS_FULLSCREEN = 907764699196L;

			public const long MENU_OPTIONS_GAMEPLAY = 907764699197L;

			public const long MENU_OPTIONS_GRAPHICS = 907764699198L;

			public const long MENU_OPTIONS_HUD = 907764699199L;

			public const long MENU_OPTIONS_MUSIC_VOLUME = 907764699200L;

			public const long MENU_OPTIONS_NETWORK = 907764699201L;

			public const long MENU_OPTIONS_QUALITY = 907764699202L;

			public const long MENU_OPTIONS_RESOLUTION = 907764699203L;

			public const long MENU_OPTIONS_SFX_VOLUME = 907764699204L;

			public const long MENU_OPTIONS_SHADOW = 907764699205L;

			public const long MENU_OPTIONS_SHADOW_PLURAL = 907764699206L;

			public const long MENU_OPTIONS_SIZE = 907768893440L;

			public const long MENU_OPTIONS_SOUND = 907768893441L;

			public const long MENU_OPTIONS_SOUND_VOLUME = 907768893442L;

			public const long MENU_OPTIONS_SPLITSCREEN = 907768893443L;

			public const long MENU_OPTIONS_THIRD_PERSON_VIEW = 907768893444L;

			public const long MENU_OPTIONS_VIDEO = 907768893445L;

			public const long MENU_OPTIONS_VIDEO_QUALITY = 907768893446L;

			public const long MENU_OPTIONS_VOICE_VOLUME = 907768893447L;

			public const long MENU_OPTIONS_VSYNC = 907768893448L;

			public const long MENU_OPTIONS_WINDOWED = 907768893449L;

			public const long MENU_OPTIONS_SHADER_DETAIL = 907768893450L;

			public const long MENU_OPTIONS_TEXTURE_DETAIL = 907768893451L;

			public const long MENU_OPTIONS_MULTICORE_RENDERING = 907768893452L;

			public const long MENU_OPTIONS_MODEL_DETAIL = 907768893453L;

			public const long MENU_OPTIONS_WIDESCREEN = 907768893454L;

			public const long MENU_OPTIONS_EFFECT_DETAIL = 907768893455L;

			public const long MENU_OPTIONS_FILTERING_ANISOTROPIC = 907768893456L;

			public const long MENU_OPTIONS_FILTERING_BILINEAR = 907768893457L;

			public const long MENU_OPTIONS_FILTERING_TRILINEAR = 907768893458L;

			public const long MENU_OPTIONS_FILTERING_MODE = 907768893459L;

			public const long MENU_SETTING_ULTRA = 907768893460L;

			public const long MENU_SETTING_VERY_HIGH = 907768893461L;

			public const long MENU_SETTING_HIGH = 907768893462L;

			public const long MENU_SETTING_MEDIUM = 907768893463L;

			public const long MENU_SETTING_LOW = 907768893464L;

			public const long MENU_VERSION = 907768893465L;

			public const long MENU_VERSION_DEMO = 907768893466L;

			public const long MENU_VERSION_FREE = 907768893467L;

			public const long MENU_VERSION_FULL = 907768893468L;

			public const long MENU_VERSION_LITE = 907768893469L;

			public const long MENU_VERSION_PREMIUM = 907768893470L;

			public const long MENU_VERSION_SHAREWARE = 907768893471L;

			public const long MENU_VERSION_TRIAL = 907768893472L;

			public const long MENU_VR_COMFORT_MODE = 907768893473L;

			public const long MENU_VR_HMD_ORIENT_RESET = 907768893474L;

			public const long MENU_VR_VIRTUAL_REALITY = 907768893475L;
		}

		public static class TupleReferences
		{
			public static readonly LOS.TupleReference LANGUAGE_CODE = GetTupleReference(907575955456L);

			public static readonly LOS.TupleReference LANGUAGE_EN = GetTupleReference(907601121280L);

			public static readonly LOS.TupleReference LANGUAGE_DIRECTION = GetTupleReference(1066867232768L);

			public static readonly LOS.TupleReference CONTEXT_CHARACTER_CLASS = GetTupleReference(907605315585L);

			public static readonly LOS.TupleReference CONTEXT_CHARACTER_DELETE = GetTupleReference(907605315586L);

			public static readonly LOS.TupleReference CONTEXT_CHARACTER_NEW = GetTupleReference(907605315587L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_AI = GetTupleReference(907605315588L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_ALIEN = GetTupleReference(907605315589L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_CLONE = GetTupleReference(907605315590L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_CYBORG = GetTupleReference(907605315591L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_DEITY = GetTupleReference(907605315592L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_DEMON = GetTupleReference(907605315593L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_DEMONNESS = GetTupleReference(907605315594L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_DRAGON = GetTupleReference(907605315595L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_DRAGON_PLURAL = GetTupleReference(907605315596L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_GHOST = GetTupleReference(907605315597L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_GOD = GetTupleReference(907605315598L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_GOD_PLURAL = GetTupleReference(907605315599L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_GODDESS = GetTupleReference(907605315600L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_MAGE = GetTupleReference(907605315601L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_MEDIC = GetTupleReference(907605315602L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_MONSTER = GetTupleReference(907605315603L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_NINJA = GetTupleReference(907605315604L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_PIRATE = GetTupleReference(907605315605L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_PRIEST = GetTupleReference(907605315606L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_PRINCE = GetTupleReference(907605315607L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_PRINCESS = GetTupleReference(907605315608L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_ROBOT = GetTupleReference(907605315609L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_SKELETON = GetTupleReference(907605315610L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_SNIPER = GetTupleReference(907605315611L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_SOLDIER = GetTupleReference(907605315612L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_THIEF = GetTupleReference(907605315613L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_WARRIOR = GetTupleReference(907605315614L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_WIZARD = GetTupleReference(907605315615L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_ZOMBIE = GetTupleReference(907605315616L);

			public static readonly LOS.TupleReference CONTEXT_CLASS_ZOMBIE_PLURAL = GetTupleReference(907605315617L);

			public static readonly LOS.TupleReference CONTEXT_COMPETITION_FIRST = GetTupleReference(907605315618L);

			public static readonly LOS.TupleReference CONTEXT_COMPETITION_PLACE_NOUN = GetTupleReference(907605315619L);

			public static readonly LOS.TupleReference CONTEXT_COMPETITION_SECOND = GetTupleReference(907605315620L);

			public static readonly LOS.TupleReference CONTEXT_COMPETITION_THIRD = GetTupleReference(907605315621L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_BURNING = GetTupleReference(907605315622L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_CRITICAL = GetTupleReference(907605315623L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_DEAD = GetTupleReference(907605315624L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_FROZEN = GetTupleReference(907605315625L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_POISONED = GetTupleReference(907605315626L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_SLOWED = GetTupleReference(907605315627L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_STUNNED = GetTupleReference(907605315628L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_UNCONSCIOUS = GetTupleReference(907605315629L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_UNHURT = GetTupleReference(907605315630L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_WET = GetTupleReference(907605315631L);

			public static readonly LOS.TupleReference CONTEXT_CONDITION_WOUNDED = GetTupleReference(907605315632L);

			public static readonly LOS.TupleReference CONTEXT_DESCRIPTION_BIG = GetTupleReference(907605315633L);

			public static readonly LOS.TupleReference CONTEXT_DESCRIPTION_HUGE = GetTupleReference(907605315634L);

			public static readonly LOS.TupleReference CONTEXT_DESCRIPTION_SMALL = GetTupleReference(907605315635L);

			public static readonly LOS.TupleReference CONTEXT_DESCRIPTION_TINY = GetTupleReference(907605315636L);

			public static readonly LOS.TupleReference CONTEXT_ELEMENT_AIR = GetTupleReference(907605315637L);

			public static readonly LOS.TupleReference CONTEXT_ELEMENT_EARTH = GetTupleReference(907605315638L);

			public static readonly LOS.TupleReference CONTEXT_ELEMENT_FIRE = GetTupleReference(907605315639L);

			public static readonly LOS.TupleReference CONTEXT_ELEMENT_METAL = GetTupleReference(907605315640L);

			public static readonly LOS.TupleReference CONTEXT_ELEMENT_WATER = GetTupleReference(907605315641L);

			public static readonly LOS.TupleReference CONTEXT_GAMBLING_BET = GetTupleReference(907605315642L);

			public static readonly LOS.TupleReference CONTEXT_GAMBLING_DISCARD = GetTupleReference(907605315643L);

			public static readonly LOS.TupleReference CONTEXT_GAMBLING_JACKPOT = GetTupleReference(907605315644L);

			public static readonly LOS.TupleReference CONTEXT_GAMBLING_MAX = GetTupleReference(907605315645L);

			public static readonly LOS.TupleReference CONTEXT_GAMBLING_MIN = GetTupleReference(907605315646L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_ARMOR = GetTupleReference(907605315647L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_ARROW = GetTupleReference(907605315648L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_ARROW_PLURAL = GetTupleReference(907605315649L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_BOARD = GetTupleReference(907605315650L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_BOMB = GetTupleReference(907609509888L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_BOOK = GetTupleReference(907609509889L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_BOOTS = GetTupleReference(907609509890L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_BOW = GetTupleReference(907609509891L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_CARD = GetTupleReference(907609509892L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_CARD_PLURAL = GetTupleReference(907609509893L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_CHEST = GetTupleReference(907609509894L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_COND_MAGICAL = GetTupleReference(907609509895L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_COND_RUSTY = GetTupleReference(907609509896L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_DAGGER = GetTupleReference(907609509897L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_DIE_NOUN = GetTupleReference(907609509898L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_DIE_PLURAL = GetTupleReference(907609509899L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_DOOR = GetTupleReference(907609509900L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_GAUNTLETS = GetTupleReference(907609509901L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_GEM = GetTupleReference(907609509902L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_GIFT_NOUN = GetTupleReference(907609509903L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_GIFT_PLURAL = GetTupleReference(907609509904L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_GLOVE = GetTupleReference(907609509905L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_GUN = GetTupleReference(907609509906L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_HAMMER = GetTupleReference(907609509907L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_HELMET = GetTupleReference(907609509908L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_KEY = GetTupleReference(907609509909L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_KNIFE = GetTupleReference(907609509910L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_LOCK = GetTupleReference(907609509911L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_LOVE = GetTupleReference(907609509912L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_MACHINE_ITEM_GUN = GetTupleReference(907609509913L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_MAGIC = GetTupleReference(907609509914L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_PISTOL = GetTupleReference(907609509915L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_QUEST_NOUN = GetTupleReference(907609509916L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_RANK_NOUN = GetTupleReference(907609509917L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_RIFLE = GetTupleReference(907609509918L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_ROCKET_ITEM_LAUNCHER = GetTupleReference(907609509919L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_RPG = GetTupleReference(907609509920L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_SCROLL = GetTupleReference(907609509921L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_SHIELD = GetTupleReference(907609509922L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_SHIELD_PLURAL = GetTupleReference(907609509923L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_SHIELD_SCIFI = GetTupleReference(907609509924L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_SHIELD_SCIFI_PLURAL = GetTupleReference(907609509925L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_SHOTGUN = GetTupleReference(907609509926L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_SNIPER_ITEM_RIFLE = GetTupleReference(907609509927L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_SPELL = GetTupleReference(907609509928L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_STAFF = GetTupleReference(907609509929L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_SWORD = GetTupleReference(907609509930L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_TREASURE = GetTupleReference(907609509931L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_TYPE_EPIC = GetTupleReference(907609509932L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_TYPE_JUNK = GetTupleReference(907609509933L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_WAND = GetTupleReference(907609509934L);

			public static readonly LOS.TupleReference CONTEXT_ITEM_WOOD = GetTupleReference(907609509935L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_ALTITUDE = GetTupleReference(907613704192L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_BRAKES = GetTupleReference(907613704193L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_CHASSIS = GetTupleReference(907613704194L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_DEPTH = GetTupleReference(907613704195L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_FUEL = GetTupleReference(907613704196L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_GEAR = GetTupleReference(907613704197L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_HANDLING = GetTupleReference(907613704198L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_SPEED = GetTupleReference(907613704199L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_STEERING = GetTupleReference(907613704200L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_SUSPENSION = GetTupleReference(907613704201L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_WHEELS = GetTupleReference(907613704202L);

			public static readonly LOS.TupleReference CONTEXT_MACHINE_WINGS = GetTupleReference(907613704203L);

			public static readonly LOS.TupleReference CONTEXT_MEDAL = GetTupleReference(907613704204L);

			public static readonly LOS.TupleReference CONTEXT_MEDAL_BRONZE = GetTupleReference(907613704205L);

			public static readonly LOS.TupleReference CONTEXT_MEDAL_GOLD = GetTupleReference(907613704206L);

			public static readonly LOS.TupleReference CONTEXT_MEDAL_SILVER = GetTupleReference(907613704207L);

			public static readonly LOS.TupleReference CONTEXT_METAL_BRONZE = GetTupleReference(907613704208L);

			public static readonly LOS.TupleReference CONTEXT_METAL_COIN = GetTupleReference(907613704209L);

			public static readonly LOS.TupleReference CONTEXT_METAL_GOLD = GetTupleReference(907613704210L);

			public static readonly LOS.TupleReference CONTEXT_METAL_INGOT = GetTupleReference(907613704211L);

			public static readonly LOS.TupleReference CONTEXT_METAL_PLATINUM = GetTupleReference(907613704212L);

			public static readonly LOS.TupleReference CONTEXT_METAL_SILVER = GetTupleReference(907613704213L);

			public static readonly LOS.TupleReference CONTEXT_PLACE_CASTLE = GetTupleReference(907613704214L);

			public static readonly LOS.TupleReference CONTEXT_PLACE_CAVE = GetTupleReference(907613704215L);

			public static readonly LOS.TupleReference CONTEXT_PLACE_DUNGEON = GetTupleReference(907613704216L);

			public static readonly LOS.TupleReference CONTEXT_PLACE_INN = GetTupleReference(907613704217L);

			public static readonly LOS.TupleReference CONTEXT_PLACE_PLANET = GetTupleReference(907613704218L);

			public static readonly LOS.TupleReference CONTEXT_PLACE_PLANET_EARTH = GetTupleReference(907613704219L);

			public static readonly LOS.TupleReference CONTEXT_PLACE_SPACE = GetTupleReference(907613704220L);

			public static readonly LOS.TupleReference CONTEXT_PLACE_SPACESHIP = GetTupleReference(907613704221L);

			public static readonly LOS.TupleReference CONTEXT_STORY = GetTupleReference(907613704222L);

			public static readonly LOS.TupleReference CONTEXT_STORY_ADVENTURE = GetTupleReference(907613704223L);

			public static readonly LOS.TupleReference CONTEXT_STORY_CHAPTER = GetTupleReference(907613704224L);

			public static readonly LOS.TupleReference CONTEXT_STORY_PAGE = GetTupleReference(907613704225L);

			public static readonly LOS.TupleReference CREDITS_GAME = GetTupleReference(907613704226L);

			public static readonly LOS.TupleReference CREDITS_GAME_DEVELOPMENT = GetTupleReference(907613704227L);

			public static readonly LOS.TupleReference CREDITS_GAME_DEVELOPER = GetTupleReference(907613704228L);

			public static readonly LOS.TupleReference CREDITS_DEVELOPED_BY = GetTupleReference(907613704229L);

			public static readonly LOS.TupleReference CREDITS_PRODUCTION = GetTupleReference(907613704230L);

			public static readonly LOS.TupleReference CREDITS_PRODUCER = GetTupleReference(907613704231L);

			public static readonly LOS.TupleReference CREDITS_PUBLISHER = GetTupleReference(907613704232L);

			public static readonly LOS.TupleReference CREDITS_DIRECTOR = GetTupleReference(907613704233L);

			public static readonly LOS.TupleReference CREDITS_LEAD = GetTupleReference(907613704234L);

			public static readonly LOS.TupleReference CREDITS_ASSISTANT = GetTupleReference(907613704235L);

			public static readonly LOS.TupleReference CREDITS_ASSOCIATE = GetTupleReference(907613704236L);

			public static readonly LOS.TupleReference CREDITS_SENIOR = GetTupleReference(907613704237L);

			public static readonly LOS.TupleReference CREDITS_JUNIOR = GetTupleReference(907613704238L);

			public static readonly LOS.TupleReference CREDITS_CHIEF_TECH_OFFICER = GetTupleReference(907613704239L);

			public static readonly LOS.TupleReference CREDITS_TECHNICAL_DIRECTOR = GetTupleReference(907613704240L);

			public static readonly LOS.TupleReference CREDITS_CREATIVE_DIRECTOR = GetTupleReference(907613704241L);

			public static readonly LOS.TupleReference CREDITS_ANIMATION_DIRECTOR = GetTupleReference(907613704242L);

			public static readonly LOS.TupleReference CREDITS_ART_DIRECTOR = GetTupleReference(907613704243L);

			public static readonly LOS.TupleReference CREDITS_AUDIO_DIRECTOR = GetTupleReference(907613704244L);

			public static readonly LOS.TupleReference CREDITS_DESIGN_DIRECTOR = GetTupleReference(907613704245L);

			public static readonly LOS.TupleReference CREDITS_PROGRAM_MANAGER = GetTupleReference(907613704246L);

			public static readonly LOS.TupleReference CREDITS_PROJECT_MANAGER = GetTupleReference(907613704247L);

			public static readonly LOS.TupleReference CREDITS_GAME_DESIGN = GetTupleReference(907613704248L);

			public static readonly LOS.TupleReference CREDITS_GAME_DESIGNER = GetTupleReference(907613704249L);

			public static readonly LOS.TupleReference CREDITS_CODE = GetTupleReference(907613704250L);

			public static readonly LOS.TupleReference CREDITS_PROGRAMMING = GetTupleReference(907613704251L);

			public static readonly LOS.TupleReference CREDITS_PROGRAMMER = GetTupleReference(907613704252L);

			public static readonly LOS.TupleReference CREDITS_PROGRAMMER_PLURAL = GetTupleReference(907613704253L);

			public static readonly LOS.TupleReference CREDITS_LEAD_PROGRAMMER = GetTupleReference(907613704254L);

			public static readonly LOS.TupleReference CREDITS_GRAPHIC_DESIGNER = GetTupleReference(907613704255L);

			public static readonly LOS.TupleReference CREDITS_TECHNICAL_ARTIST = GetTupleReference(907613704256L);

			public static readonly LOS.TupleReference CREDITS_CONCEPT_ARTIST = GetTupleReference(907613704257L);

			public static readonly LOS.TupleReference CREDITS_CHARACTER_DESIGNER = GetTupleReference(907613704258L);

			public static readonly LOS.TupleReference CREDITS_ART = GetTupleReference(907613704259L);

			public static readonly LOS.TupleReference CREDITS_ARTIST = GetTupleReference(907613704260L);

			public static readonly LOS.TupleReference CREDITS_ARTIST_PLURAL = GetTupleReference(907613704261L);

			public static readonly LOS.TupleReference CREDITS_LEVEL_DESIGN = GetTupleReference(907613704262L);

			public static readonly LOS.TupleReference CREDITS_LEVEL_DESIGNER = GetTupleReference(907613704263L);

			public static readonly LOS.TupleReference CREDITS_CINEMATICS_DIRECTOR = GetTupleReference(907613704264L);

			public static readonly LOS.TupleReference CREDITS_CINEMATIC_ANIMATOR = GetTupleReference(907613704265L);

			public static readonly LOS.TupleReference CREDITS_INTERFACE_ARTIST = GetTupleReference(907613704266L);

			public static readonly LOS.TupleReference CREDITS_COMPOSER = GetTupleReference(907613704267L);

			public static readonly LOS.TupleReference CREDITS_MUSICIAN = GetTupleReference(907613704268L);

			public static readonly LOS.TupleReference CREDITS_SPECIAL_THANKS = GetTupleReference(907613704269L);

			public static readonly LOS.TupleReference CREDITS_SPECIAL_THANKS_TO = GetTupleReference(907613704270L);

			public static readonly LOS.TupleReference CREDITS_IMAGE = GetTupleReference(907617898496L);

			public static readonly LOS.TupleReference CREDITS_IMAGE_PLURAL = GetTupleReference(907617898497L);

			public static readonly LOS.TupleReference CREDITS_FONT = GetTupleReference(907617898498L);

			public static readonly LOS.TupleReference CREDITS_FONT_PLURAL = GetTupleReference(907617898499L);

			public static readonly LOS.TupleReference CREDITS_SOUNDTRACK = GetTupleReference(907617898500L);

			public static readonly LOS.TupleReference CREDITS_WRITER = GetTupleReference(907617898501L);

			public static readonly LOS.TupleReference CREDITS_MANUSCRIPT = GetTupleReference(907617898502L);

			public static readonly LOS.TupleReference CREDITS_SCRIPT_WRITER = GetTupleReference(907617898503L);

			public static readonly LOS.TupleReference CREDITS_ANIMATION = GetTupleReference(907617898504L);

			public static readonly LOS.TupleReference CREDITS_ANIMATOR = GetTupleReference(907617898505L);

			public static readonly LOS.TupleReference CREDITS_VOICE_ACTING = GetTupleReference(907617898506L);

			public static readonly LOS.TupleReference CREDITS_VOICE_ACTOR = GetTupleReference(907617898507L);

			public static readonly LOS.TupleReference CREDITS_SOUND_DESIGNER = GetTupleReference(907617898508L);

			public static readonly LOS.TupleReference CREDITS_SOUND_ENGINEER = GetTupleReference(907617898509L);

			public static readonly LOS.TupleReference CREDITS_SFX = GetTupleReference(907617898510L);

			public static readonly LOS.TupleReference CREDITS_VFX = GetTupleReference(907617898511L);

			public static readonly LOS.TupleReference CREDITS_RECORDIST = GetTupleReference(907617898512L);

			public static readonly LOS.TupleReference CREDITS_FOLEY_ARTIST = GetTupleReference(907617898513L);

			public static readonly LOS.TupleReference CREDITS_TEST_MANAGER = GetTupleReference(907617898514L);

			public static readonly LOS.TupleReference CREDITS_TEST_LEAD = GetTupleReference(907617898515L);

			public static readonly LOS.TupleReference CREDITS_TESTER = GetTupleReference(907617898516L);

			public static readonly LOS.TupleReference CREDITS_DATABASE_ADMIN = GetTupleReference(907617898517L);

			public static readonly LOS.TupleReference CREDITS_3D_MODELING = GetTupleReference(907617898518L);

			public static readonly LOS.TupleReference CREDITS_MODELS = GetTupleReference(907617898519L);

			public static readonly LOS.TupleReference CREDITS_LOCALIZATION = GetTupleReference(907617898520L);

			public static readonly LOS.TupleReference CREDITS_TRANSLATION = GetTupleReference(907617898521L);

			public static readonly LOS.TupleReference CREDITS_POLYGLOT = GetTupleReference(907617898522L);

			public static readonly LOS.TupleReference GAME_GENRE_ACTION = GetTupleReference(907617898523L);

			public static readonly LOS.TupleReference GAME_GENRE_ADVENTURE = GetTupleReference(907617898524L);

			public static readonly LOS.TupleReference GAME_GENRE_CARD = GetTupleReference(907617898525L);

			public static readonly LOS.TupleReference GAME_GENRE_CASUAL = GetTupleReference(907617898526L);

			public static readonly LOS.TupleReference GAME_GENRE_FANTASY = GetTupleReference(907617898527L);

			public static readonly LOS.TupleReference GAME_GENRE_HARDCORE = GetTupleReference(907617898528L);

			public static readonly LOS.TupleReference GAME_GENRE_INDIE = GetTupleReference(907617898529L);

			public static readonly LOS.TupleReference GAME_GENRE_MEDIEVAL = GetTupleReference(907617898530L);

			public static readonly LOS.TupleReference GAME_GENRE_MISC = GetTupleReference(907617898531L);

			public static readonly LOS.TupleReference GAME_GENRE_MULTIPLAYER = GetTupleReference(907617898532L);

			public static readonly LOS.TupleReference GAME_GENRE_NAME = GetTupleReference(907617898533L);

			public static readonly LOS.TupleReference GAME_GENRE_PLATFORMER = GetTupleReference(907617898534L);

			public static readonly LOS.TupleReference GAME_GENRE_PUZZLE = GetTupleReference(907617898535L);

			public static readonly LOS.TupleReference GAME_GENRE_QUIZ = GetTupleReference(907617898536L);

			public static readonly LOS.TupleReference GAME_GENRE_RACING = GetTupleReference(907617898537L);

			public static readonly LOS.TupleReference GAME_GENRE_RETRO = GetTupleReference(907617898538L);

			public static readonly LOS.TupleReference GAME_GENRE_RPG = GetTupleReference(907617898539L);

			public static readonly LOS.TupleReference GAME_GENRE_RTS = GetTupleReference(907617898540L);

			public static readonly LOS.TupleReference GAME_GENRE_SCIFI = GetTupleReference(907617898541L);

			public static readonly LOS.TupleReference GAME_GENRE_SHOOTER = GetTupleReference(907617898542L);

			public static readonly LOS.TupleReference GAME_GENRE_SIMULATION = GetTupleReference(907617898543L);

			public static readonly LOS.TupleReference GAME_GENRE_SINGLEPLAYER = GetTupleReference(907617898544L);

			public static readonly LOS.TupleReference GAME_GENRE_SPORTS = GetTupleReference(907617898545L);

			public static readonly LOS.TupleReference GAME_GENRE_STRATEGY = GetTupleReference(907617898546L);

			public static readonly LOS.TupleReference GAME_GENRE_TBS = GetTupleReference(907617898547L);

			public static readonly LOS.TupleReference GAME_GENRE_TOWER_DEFENSE = GetTupleReference(907617898548L);

			public static readonly LOS.TupleReference ERROR_DEVICE_NOT_FOUND = GetTupleReference(907617898549L);

			public static readonly LOS.TupleReference ERROR_DEVICE_VR_LEAVING_TRACKAREA = GetTupleReference(907617898550L);

			public static readonly LOS.TupleReference ERROR_DEVICE_VR_NO_HMD = GetTupleReference(907617898551L);

			public static readonly LOS.TupleReference ERROR_GENERIC_ERRCODE = GetTupleReference(907617898552L);

			public static readonly LOS.TupleReference ERROR_GENERIC_TEXT = GetTupleReference(907617898553L);

			public static readonly LOS.TupleReference ERROR_GENERIC_INVALID_NAME = GetTupleReference(907617898554L);

			public static readonly LOS.TupleReference ERROR_GENERIC_INVALID_CD_KEY = GetTupleReference(907617898555L);

			public static readonly LOS.TupleReference ERROR_GENERIC_CHEATING = GetTupleReference(907617898556L);

			public static readonly LOS.TupleReference ERROR_NETWORK_CONNECTION_LOST = GetTupleReference(907617898557L);

			public static readonly LOS.TupleReference ERROR_NETWORK_DISCONNECTED_YOUR_OPPONENT = GetTupleReference(907617898558L);

			public static readonly LOS.TupleReference ERROR_NETWORK_DISCONNECTED_YOUR_PLAYER = GetTupleReference(907617898559L);

			public static readonly LOS.TupleReference ERROR_NETWORK_DISCONNECTED_YOUR_TEAMMATE = GetTupleReference(907617898560L);

			public static readonly LOS.TupleReference ERROR_NETWORK_SERVER = GetTupleReference(907617898561L);

			public static readonly LOS.TupleReference ERROR_SAVE_CORRUPTED = GetTupleReference(907617898562L);

			public static readonly LOS.TupleReference ERROR_SAVE_NO_GAMES_FOUND = GetTupleReference(907617898563L);

			public static readonly LOS.TupleReference ERROR_SAVE_NO_OPEN_GAMES = GetTupleReference(907622092800L);

			public static readonly LOS.TupleReference GAME_ACTION_ATTACK = GetTupleReference(907622092801L);

			public static readonly LOS.TupleReference GAME_ACTION_BOOST = GetTupleReference(907622092802L);

			public static readonly LOS.TupleReference GAME_ACTION_BUILD = GetTupleReference(907622092803L);

			public static readonly LOS.TupleReference GAME_ACTION_CAST_FISHING = GetTupleReference(907622092804L);

			public static readonly LOS.TupleReference GAME_ACTION_CAST_SPELL = GetTupleReference(907622092805L);

			public static readonly LOS.TupleReference GAME_ACTION_CLAIM = GetTupleReference(907622092806L);

			public static readonly LOS.TupleReference GAME_ACTION_CLIMB = GetTupleReference(907622092807L);

			public static readonly LOS.TupleReference GAME_ACTION_CLOSE = GetTupleReference(907622092808L);

			public static readonly LOS.TupleReference GAME_ACTION_COLLECT = GetTupleReference(907622092809L);

			public static readonly LOS.TupleReference GAME_ACTION_COOK = GetTupleReference(907622092810L);

			public static readonly LOS.TupleReference GAME_ACTION_CRAFT = GetTupleReference(907622092811L);

			public static readonly LOS.TupleReference GAME_ACTION_CROUCH = GetTupleReference(907622092812L);

			public static readonly LOS.TupleReference GAME_ACTION_DODGE = GetTupleReference(907622092813L);

			public static readonly LOS.TupleReference GAME_ACTION_DRIVE = GetTupleReference(907622092814L);

			public static readonly LOS.TupleReference GAME_ACTION_FLY = GetTupleReference(907622092815L);

			public static readonly LOS.TupleReference GAME_ACTION_FULL_SPEED = GetTupleReference(907622092816L);

			public static readonly LOS.TupleReference GAME_ACTION_GO = GetTupleReference(907622092817L);

			public static readonly LOS.TupleReference GAME_ACTION_GO_TO_PLACE = GetTupleReference(907622092818L);

			public static readonly LOS.TupleReference GAME_ACTION_HALF_SPEED = GetTupleReference(907622092819L);

			public static readonly LOS.TupleReference GAME_ACTION_HEAL = GetTupleReference(907622092820L);

			public static readonly LOS.TupleReference GAME_ACTION_JUMP = GetTupleReference(907622092821L);

			public static readonly LOS.TupleReference GAME_ACTION_LAND = GetTupleReference(907622092822L);

			public static readonly LOS.TupleReference GAME_ACTION_LOOK = GetTupleReference(907622092823L);

			public static readonly LOS.TupleReference GAME_ACTION_MIX = GetTupleReference(907622092824L);

			public static readonly LOS.TupleReference GAME_ACTION_OPEN = GetTupleReference(907622092825L);

			public static readonly LOS.TupleReference GAME_ACTION_PICK_UP = GetTupleReference(907622092826L);

			public static readonly LOS.TupleReference GAME_ACTION_RACE = GetTupleReference(907622092827L);

			public static readonly LOS.TupleReference GAME_ACTION_READ = GetTupleReference(907622092828L);

			public static readonly LOS.TupleReference GAME_ACTION_REPLACE = GetTupleReference(907622092829L);

			public static readonly LOS.TupleReference GAME_ACTION_RUN = GetTupleReference(907622092830L);

			public static readonly LOS.TupleReference GAME_ACTION_RUN_AWAY = GetTupleReference(907622092831L);

			public static readonly LOS.TupleReference GAME_ACTION_SHOOT = GetTupleReference(907622092832L);

			public static readonly LOS.TupleReference GAME_ACTION_SNEAK = GetTupleReference(907622092833L);

			public static readonly LOS.TupleReference GAME_ACTION_SPIN = GetTupleReference(907622092834L);

			public static readonly LOS.TupleReference GAME_ACTION_SPRINT = GetTupleReference(907622092835L);

			public static readonly LOS.TupleReference GAME_ACTION_STEER = GetTupleReference(907622092836L);

			public static readonly LOS.TupleReference GAME_ACTION_TAKE_OFF = GetTupleReference(907622092837L);

			public static readonly LOS.TupleReference GAME_ACTION_TALK = GetTupleReference(907622092838L);

			public static readonly LOS.TupleReference GAME_ACTION_THROW = GetTupleReference(907622092839L);

			public static readonly LOS.TupleReference GAME_ACTION_WALK = GetTupleReference(907622092840L);

			public static readonly LOS.TupleReference GAME_CHARACTER_ENDURANCE = GetTupleReference(907622092841L);

			public static readonly LOS.TupleReference GAME_CHARACTER_EXP = GetTupleReference(907622092842L);

			public static readonly LOS.TupleReference GAME_CHARACTER_FEMALE = GetTupleReference(907622092843L);

			public static readonly LOS.TupleReference GAME_CHARACTER_HEALTH = GetTupleReference(907622092844L);

			public static readonly LOS.TupleReference GAME_CHARACTER_HEART = GetTupleReference(907622092845L);

			public static readonly LOS.TupleReference GAME_CHARACTER_HEART_PLURAL = GetTupleReference(907622092846L);

			public static readonly LOS.TupleReference GAME_CHARACTER_LEVEL = GetTupleReference(907622092847L);

			public static readonly LOS.TupleReference GAME_CHARACTER_LEVEL_UP = GetTupleReference(907622092848L);

			public static readonly LOS.TupleReference GAME_CHARACTER_LIVES = GetTupleReference(907622092849L);

			public static readonly LOS.TupleReference GAME_CHARACTER_MALE = GetTupleReference(907622092850L);

			public static readonly LOS.TupleReference GAME_CHARACTER_MAN = GetTupleReference(907622092851L);

			public static readonly LOS.TupleReference GAME_CHARACTER_MAN_PLURAL = GetTupleReference(907622092852L);

			public static readonly LOS.TupleReference GAME_CHARACTER_RESPAWN = GetTupleReference(907622092853L);

			public static readonly LOS.TupleReference GAME_CHARACTER_RESPAWN_IN = GetTupleReference(907622092854L);

			public static readonly LOS.TupleReference GAME_CHARACTER_RESPAWN_QUESTION = GetTupleReference(907622092855L);

			public static readonly LOS.TupleReference GAME_CHARACTER_RESPAWNING = GetTupleReference(907622092856L);

			public static readonly LOS.TupleReference GAME_CHARACTER_SKILL = GetTupleReference(907622092857L);

			public static readonly LOS.TupleReference GAME_CHARACTER_SPAWN_POINT = GetTupleReference(907622092858L);

			public static readonly LOS.TupleReference GAME_CHARACTER_TRANSGENDER = GetTupleReference(907622092859L);

			public static readonly LOS.TupleReference GAME_CHARACTER_WOMAN = GetTupleReference(907622092860L);

			public static readonly LOS.TupleReference GAME_CHARACTER_WOMAN_PLURAL = GetTupleReference(907622092861L);

			public static readonly LOS.TupleReference GAME_DAMAGE = GetTupleReference(907622092862L);

			public static readonly LOS.TupleReference GAME_DAMAGE_CRITICAL_HIT = GetTupleReference(907622092863L);

			public static readonly LOS.TupleReference GAME_DAMAGE_CRITICAL_MISS = GetTupleReference(907622092864L);

			public static readonly LOS.TupleReference GAME_DAMAGE_HIT_NOUN = GetTupleReference(907622092865L);

			public static readonly LOS.TupleReference GAME_DAMAGE_HIT_POINTS_PLURAL = GetTupleReference(907622092866L);

			public static readonly LOS.TupleReference GAME_DAMAGE_MISS_NOUN = GetTupleReference(907622092867L);

			public static readonly LOS.TupleReference GAME_DAMAGE_UNIT = GetTupleReference(907622092868L);

			public static readonly LOS.TupleReference GAME_DAMAGE_UNIT_PLURAL = GetTupleReference(907622092869L);

			public static readonly LOS.TupleReference GAME_ENEMY = GetTupleReference(907622092870L);

			public static readonly LOS.TupleReference GAME_ENEMY_FEMALE = GetTupleReference(907622092871L);

			public static readonly LOS.TupleReference GAME_ENEMY_FEMALE_PLURAL = GetTupleReference(907622092872L);

			public static readonly LOS.TupleReference GAME_ENEMY_PLURAL = GetTupleReference(907622092873L);

			public static readonly LOS.TupleReference GAME_INVENTORY = GetTupleReference(907622092874L);

			public static readonly LOS.TupleReference GAME_INVENTORY_AMMO = GetTupleReference(907622092875L);

			public static readonly LOS.TupleReference GAME_INVENTORY_AMMUNITION = GetTupleReference(907622092876L);

			public static readonly LOS.TupleReference GAME_INVENTORY_BULLET = GetTupleReference(907622092877L);

			public static readonly LOS.TupleReference GAME_INVENTORY_BULLET_PLURAL = GetTupleReference(907622092878L);

			public static readonly LOS.TupleReference GAME_INVENTORY_BUY = GetTupleReference(907622092879L);

			public static readonly LOS.TupleReference GAME_INVENTORY_CAPACITY = GetTupleReference(907622092880L);

			public static readonly LOS.TupleReference GAME_INVENTORY_DROP = GetTupleReference(907622092881L);

			public static readonly LOS.TupleReference GAME_INVENTORY_ENCUMBRANCE = GetTupleReference(907622092882L);

			public static readonly LOS.TupleReference GAME_INVENTORY_FULL = GetTupleReference(907622092883L);

			public static readonly LOS.TupleReference GAME_INVENTORY_ITEM = GetTupleReference(907622092884L);

			public static readonly LOS.TupleReference GAME_INVENTORY_KEY = GetTupleReference(907622092885L);

			public static readonly LOS.TupleReference GAME_INVENTORY_POWERUP = GetTupleReference(907622092886L);

			public static readonly LOS.TupleReference GAME_INVENTORY_SELL = GetTupleReference(907626287104L);

			public static readonly LOS.TupleReference GAME_INVENTORY_USE = GetTupleReference(907626287105L);

			public static readonly LOS.TupleReference GAME_INVENTORY_WEAPON = GetTupleReference(907626287106L);

			public static readonly LOS.TupleReference GAME_INVENTORY_WEAPONS = GetTupleReference(907626287107L);

			public static readonly LOS.TupleReference GAME_INVENTORY_YOU_CANNOT_CARRY = GetTupleReference(907626287108L);

			public static readonly LOS.TupleReference GAME_LABEL_BE_CAREFUL = GetTupleReference(907626287109L);

			public static readonly LOS.TupleReference GAME_LABEL_PLEASE_BE_CAREFUL = GetTupleReference(907626287110L);

			public static readonly LOS.TupleReference GAME_LABEL_TACTICS = GetTupleReference(907626287111L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE = GetTupleReference(907626287112L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_ASSIST = GetTupleReference(907626287113L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_ASSIST_PLURAL = GetTupleReference(907626287114L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_ATTACK = GetTupleReference(907626287115L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_BONUS = GetTupleReference(907626287116L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_CAPTURE = GetTupleReference(907626287117L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_COMPLETE = GetTupleReference(907626287118L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_DEATH = GetTupleReference(907626287119L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_DEATH_PLURAL = GetTupleReference(907626287120L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_DEFEND = GetTupleReference(907626287121L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_DESTROY = GetTupleReference(907626287122L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_ESCAPE = GetTupleReference(907626287123L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_GAME_FINISHED = GetTupleReference(907626287124L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_GAMEOVER = GetTupleReference(907626287125L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_GAMEOVER_CAPS = GetTupleReference(907626287126L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_HIGH_SCORE = GetTupleReference(907626287127L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_INCOMPLETE = GetTupleReference(907626287128L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_KILL = GetTupleReference(907626287129L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_KILL_ASSIST = GetTupleReference(907626287130L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_KILL_NOUN = GetTupleReference(907626287131L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_KILL_PLURAL = GetTupleReference(907626287132L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_LOCKED = GetTupleReference(907626287133L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_LOSSES = GetTupleReference(907626287134L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_MATCH = GetTupleReference(907626287135L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_MISSION = GetTupleReference(907626287136L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_MISSION_OBJECTIVE_PLURAL = GetTupleReference(907626287137L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_MISSION_OBJECTIVE = GetTupleReference(907626287138L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_MULTIPLIER = GetTupleReference(907626287139L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_PLURAL = GetTupleReference(907626287140L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_POSITION = GetTupleReference(907626287141L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_RACE_LAP = GetTupleReference(907626287142L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_RACE_LAP_PLURAL = GetTupleReference(907626287143L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_ROUND = GetTupleReference(907626287144L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_SCORE = GetTupleReference(907626287145L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_TIME = GetTupleReference(907626287146L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_TIME_LEFT = GetTupleReference(907626287147L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_TOURNAMENT = GetTupleReference(907626287148L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_TOURNAMENT_PLURAL = GetTupleReference(907626287149L);

			public static readonly LOS.TupleReference GAME_OBJECTIVE_WINS = GetTupleReference(907626287150L);

			public static readonly LOS.TupleReference GAME_STATUS_DEFEAT = GetTupleReference(907626287151L);

			public static readonly LOS.TupleReference GAME_STATUS_DEFEATED = GetTupleReference(907626287152L);

			public static readonly LOS.TupleReference GAME_STATUS_LOSER = GetTupleReference(907626287153L);

			public static readonly LOS.TupleReference GAME_STATUS_TEAM_LOST = GetTupleReference(907626287154L);

			public static readonly LOS.TupleReference GAME_STATUS_TEAM_WON = GetTupleReference(907626287155L);

			public static readonly LOS.TupleReference GAME_STATUS_VICTORY = GetTupleReference(907626287156L);

			public static readonly LOS.TupleReference GAME_STATUS_WINNER = GetTupleReference(907626287157L);

			public static readonly LOS.TupleReference GAME_STATUS_YOU_ARE_DEAD = GetTupleReference(907626287158L);

			public static readonly LOS.TupleReference GAME_STATUS_YOU_DIED = GetTupleReference(907626287159L);

			public static readonly LOS.TupleReference GAME_STATUS_YOU_LOSE = GetTupleReference(907626287160L);

			public static readonly LOS.TupleReference GAME_STATUS_YOU_LOST = GetTupleReference(907626287161L);

			public static readonly LOS.TupleReference GAME_STATUS_YOU_WIN = GetTupleReference(907626287162L);

			public static readonly LOS.TupleReference GAME_STATUS_YOU_WON = GetTupleReference(907626287163L);

			public static readonly LOS.TupleReference MENU_CAMERA = GetTupleReference(907626287164L);

			public static readonly LOS.TupleReference MENU_CAMERA_INVERTED = GetTupleReference(907626287165L);

			public static readonly LOS.TupleReference MENU_CAMERA_NORMAL = GetTupleReference(907626287166L);

			public static readonly LOS.TupleReference MENU_CAMERA_XAXIS = GetTupleReference(907626287167L);

			public static readonly LOS.TupleReference MENU_CAMERA_YAXIS = GetTupleReference(907626287168L);

			public static readonly LOS.TupleReference MENU_CONTROLS_BUTTON = GetTupleReference(907626287169L);

			public static readonly LOS.TupleReference MENU_CONTROLS_CLICK = GetTupleReference(907626287170L);

			public static readonly LOS.TupleReference MENU_CONTROLS_DEADZONE = GetTupleReference(907626287171L);

			public static readonly LOS.TupleReference MENU_CONTROLS_DOUBLE_CLICK = GetTupleReference(907626287172L);

			public static readonly LOS.TupleReference MENU_CONTROLS_DRAG = GetTupleReference(907626287173L);

			public static readonly LOS.TupleReference MENU_CONTROLS_DRAG_DROP = GetTupleReference(907626287174L);

			public static readonly LOS.TupleReference MENU_CONTROLS_FINGER = GetTupleReference(907626287175L);

			public static readonly LOS.TupleReference MENU_CONTROLS_GAMEPAD = GetTupleReference(907626287176L);

			public static readonly LOS.TupleReference MENU_CONTROLS_GYROSCOPE = GetTupleReference(907626287177L);

			public static readonly LOS.TupleReference MENU_CONTROLS_KEY = GetTupleReference(907626287178L);

			public static readonly LOS.TupleReference MENU_CONTROLS_KEYBOARD = GetTupleReference(907626287179L);

			public static readonly LOS.TupleReference MENU_CONTROLS_MOUSE = GetTupleReference(907626287180L);

			public static readonly LOS.TupleReference MENU_CONTROLS_MOUSE_SENSITIVITY = GetTupleReference(907626287181L);

			public static readonly LOS.TupleReference MENU_CONTROLS_PINCH = GetTupleReference(907626287182L);

			public static readonly LOS.TupleReference MENU_CONTROLS_ROTATE = GetTupleReference(907626287183L);

			public static readonly LOS.TupleReference MENU_CONTROLS_SCROLL = GetTupleReference(907626287184L);

			public static readonly LOS.TupleReference MENU_CONTROLS_SHAKE = GetTupleReference(907626287185L);

			public static readonly LOS.TupleReference MENU_CONTROLS_SPREAD = GetTupleReference(907630481408L);

			public static readonly LOS.TupleReference MENU_CONTROLS_SWIPE = GetTupleReference(907630481409L);

			public static readonly LOS.TupleReference MENU_CONTROLS_TAP = GetTupleReference(907630481410L);

			public static readonly LOS.TupleReference MENU_CONTROLS_THE_BUTTON = GetTupleReference(907630481411L);

			public static readonly LOS.TupleReference MENU_CONTROLS_THE_KEY = GetTupleReference(907630481412L);

			public static readonly LOS.TupleReference MENU_CONTROLS_TILT = GetTupleReference(907630481413L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_ARM = GetTupleReference(907630481414L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_ARM_PLURAL = GetTupleReference(907630481415L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_CHEST = GetTupleReference(907630481416L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_CHIN = GetTupleReference(907630481417L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_COLOR = GetTupleReference(907630481418L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_EAR = GetTupleReference(907630481419L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_EAR_PLURAL = GetTupleReference(907630481420L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_EYE = GetTupleReference(907630481421L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_EYE_PLURAL = GetTupleReference(907630481422L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_FEET = GetTupleReference(907630481423L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_FOOT = GetTupleReference(907630481424L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_HAIR = GetTupleReference(907630481425L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_HAIR_BEARD = GetTupleReference(907630481426L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_HAIR_FUR = GetTupleReference(907630481427L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_HAIR_HEAD = GetTupleReference(907630481428L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_HAND = GetTupleReference(907630481429L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_HAND_PLURAL = GetTupleReference(907630481430L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_HEAD = GetTupleReference(907630481431L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_HIPS = GetTupleReference(907630481432L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_LEG = GetTupleReference(907630481433L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_LEG_PLURAL = GetTupleReference(907630481434L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_MOUTH = GetTupleReference(907630481435L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_NECK = GetTupleReference(907630481436L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_NOSE = GetTupleReference(907630481437L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_SKIN = GetTupleReference(907630481438L);

			public static readonly LOS.TupleReference MENU_CUSTOMIZE_THORAX = GetTupleReference(907630481439L);

			public static readonly LOS.TupleReference MENU_DIFFICULTY = GetTupleReference(907630481440L);

			public static readonly LOS.TupleReference MENU_DIFFICULTY_EASY = GetTupleReference(907630481441L);

			public static readonly LOS.TupleReference MENU_DIFFICULTY_HARD = GetTupleReference(907630481442L);

			public static readonly LOS.TupleReference MENU_DIFFICULTY_IMPOSSIBLE = GetTupleReference(907630481443L);

			public static readonly LOS.TupleReference MENU_DIFFICULTY_INSANE = GetTupleReference(907630481444L);

			public static readonly LOS.TupleReference MENU_DIFFICULTY_MEDIUM = GetTupleReference(907630481445L);

			public static readonly LOS.TupleReference MENU_DIFFICULTY_VERY_EASY = GetTupleReference(907630481446L);

			public static readonly LOS.TupleReference MENU_DIFFICULTY_VERY_HARD = GetTupleReference(907630481447L);

			public static readonly LOS.TupleReference MENU_DIR_DOWN = GetTupleReference(907630481448L);

			public static readonly LOS.TupleReference MENU_DIR_LEFT = GetTupleReference(907630481449L);

			public static readonly LOS.TupleReference MENU_DIR_RIGHT = GetTupleReference(907630481450L);

			public static readonly LOS.TupleReference MENU_DIR_UP = GetTupleReference(907630481451L);

			public static readonly LOS.TupleReference MENU_IO_LOAD = GetTupleReference(907630481452L);

			public static readonly LOS.TupleReference MENU_IO_LOAD_GAME = GetTupleReference(907630481453L);

			public static readonly LOS.TupleReference MENU_IO_LOADING = GetTupleReference(907630481454L);

			public static readonly LOS.TupleReference MENU_IO_PLEASE_WAIT = GetTupleReference(907630481455L);

			public static readonly LOS.TupleReference MENU_IO_RESET_SAVE_DATA = GetTupleReference(907630481456L);

			public static readonly LOS.TupleReference MENU_IO_SAVE = GetTupleReference(907630481457L);

			public static readonly LOS.TupleReference MENU_IO_SAVE_DATA = GetTupleReference(907630481458L);

			public static readonly LOS.TupleReference MENU_IO_SAVE_GAME = GetTupleReference(907630481459L);

			public static readonly LOS.TupleReference MENU_IO_SAVING = GetTupleReference(907630481460L);

			public static readonly LOS.TupleReference MENU_LABEL_ALL = GetTupleReference(907630481461L);

			public static readonly LOS.TupleReference MENU_LABEL_AGE = GetTupleReference(907630481462L);

			public static readonly LOS.TupleReference MENU_LABEL_ARE_YOU_REALLY_SURE = GetTupleReference(907630481463L);

			public static readonly LOS.TupleReference MENU_LABEL_ARE_YOU_SURE = GetTupleReference(907630481464L);

			public static readonly LOS.TupleReference MENU_LABEL_BACK = GetTupleReference(907630481465L);

			public static readonly LOS.TupleReference MENU_LABEL_BACKGROUND_MUSIC = GetTupleReference(907630481466L);

			public static readonly LOS.TupleReference MENU_LABEL_CANCEL = GetTupleReference(907630481467L);

			public static readonly LOS.TupleReference MENU_LABEL_CLOSE = GetTupleReference(907630481468L);

			public static readonly LOS.TupleReference MENU_LABEL_CONFIRM_BUTTON = GetTupleReference(907630481469L);

			public static readonly LOS.TupleReference MENU_LABEL_CONTINUE = GetTupleReference(907630481470L);

			public static readonly LOS.TupleReference MENU_LABEL_CONTINUE_QUESTION = GetTupleReference(907630481471L);

			public static readonly LOS.TupleReference MENU_LABEL_CREDITS = GetTupleReference(907630481472L);

			public static readonly LOS.TupleReference MENU_LABEL_DESKTOP = GetTupleReference(907630481473L);

			public static readonly LOS.TupleReference MENU_LABEL_DESKTOP_QUESTION = GetTupleReference(907630481474L);

			public static readonly LOS.TupleReference MENU_LABEL_DLC = GetTupleReference(907630481475L);

			public static readonly LOS.TupleReference MENU_LABEL_DOWNLOAD = GetTupleReference(907630481476L);

			public static readonly LOS.TupleReference MENU_LABEL_DOWNLOADABLE = GetTupleReference(907630481477L);

			public static readonly LOS.TupleReference MENU_LABEL_EXIT = GetTupleReference(907630481478L);

			public static readonly LOS.TupleReference MENU_LABEL_EXIT_QUESTION = GetTupleReference(907630481479L);

			public static readonly LOS.TupleReference MENU_LABEL_EXTRAS = GetTupleReference(907630481480L);

			public static readonly LOS.TupleReference MENU_LABEL_FRAMESPERSEC = GetTupleReference(907630481481L);

			public static readonly LOS.TupleReference MENU_LABEL_GALLERY = GetTupleReference(907630481482L);

			public static readonly LOS.TupleReference MENU_LABEL_GOODBYE = GetTupleReference(907630481483L);

			public static readonly LOS.TupleReference MENU_LABEL_GRAPHICS = GetTupleReference(907630481484L);

			public static readonly LOS.TupleReference MENU_LABEL_IAP = GetTupleReference(907630481485L);

			public static readonly LOS.TupleReference MENU_LABEL_LANGUAGE = GetTupleReference(907630481486L);

			public static readonly LOS.TupleReference MENU_LABEL_LEVEL = GetTupleReference(907630481487L);

			public static readonly LOS.TupleReference MENU_LABEL_MAIL = GetTupleReference(907630481488L);

			public static readonly LOS.TupleReference MENU_LABEL_MAINMENU = GetTupleReference(907630481489L);

			public static readonly LOS.TupleReference MENU_LABEL_MORE = GetTupleReference(907630481490L);

			public static readonly LOS.TupleReference MENU_LABEL_MUSIC = GetTupleReference(907630481491L);

			public static readonly LOS.TupleReference MENU_LABEL_MUSIC_PLAYER = GetTupleReference(907630481492L);

			public static readonly LOS.TupleReference MENU_LABEL_NEW_GAME = GetTupleReference(907634675712L);

			public static readonly LOS.TupleReference MENU_LABEL_NEXT = GetTupleReference(907634675713L);

			public static readonly LOS.TupleReference MENU_LABEL_NEXT_TRACK = GetTupleReference(907634675714L);

			public static readonly LOS.TupleReference MENU_LABEL_NO = GetTupleReference(907634675715L);

			public static readonly LOS.TupleReference MENU_LABEL_NO_CAPS = GetTupleReference(907634675716L);

			public static readonly LOS.TupleReference MENU_LABEL_NOPE = GetTupleReference(907634675717L);

			public static readonly LOS.TupleReference MENU_LABEL_OK = GetTupleReference(907634675718L);

			public static readonly LOS.TupleReference MENU_LABEL_PAUSE = GetTupleReference(907634675719L);

			public static readonly LOS.TupleReference MENU_LABEL_PAUSED = GetTupleReference(907634675720L);

			public static readonly LOS.TupleReference MENU_LABEL_PLAY = GetTupleReference(907634675721L);

			public static readonly LOS.TupleReference MENU_LABEL_PLAY_AGAIN = GetTupleReference(907760504832L);

			public static readonly LOS.TupleReference MENU_LABEL_PRESS_ANYKEY = GetTupleReference(907760504833L);

			public static readonly LOS.TupleReference MENU_LABEL_PRESS_ANYKEY_CONTINUE = GetTupleReference(907760504834L);

			public static readonly LOS.TupleReference MENU_LABEL_PRESS_START = GetTupleReference(907760504835L);

			public static readonly LOS.TupleReference MENU_LABEL_PRESS_START_CONTINUE = GetTupleReference(907760504836L);

			public static readonly LOS.TupleReference MENU_LABEL_PREVIOUS = GetTupleReference(907760504837L);

			public static readonly LOS.TupleReference MENU_LABEL_PREVIOUS_TRACK = GetTupleReference(907760504838L);

			public static readonly LOS.TupleReference MENU_LABEL_PRICE = GetTupleReference(907764699136L);

			public static readonly LOS.TupleReference MENU_LABEL_PRICE_FREE = GetTupleReference(907764699137L);

			public static readonly LOS.TupleReference MENU_LABEL_PURCHASE = GetTupleReference(907764699138L);

			public static readonly LOS.TupleReference MENU_LABEL_QUIT = GetTupleReference(907764699139L);

			public static readonly LOS.TupleReference MENU_LABEL_QUIT_CONFIRM = GetTupleReference(907764699140L);

			public static readonly LOS.TupleReference MENU_LABEL_QUIT_QUESTION = GetTupleReference(907764699141L);

			public static readonly LOS.TupleReference MENU_LABEL_REPLAY = GetTupleReference(907764699142L);

			public static readonly LOS.TupleReference MENU_LABEL_RETURN = GetTupleReference(907764699143L);

			public static readonly LOS.TupleReference MENU_LABEL_RETURN_MAIN = GetTupleReference(907764699144L);

			public static readonly LOS.TupleReference MENU_LABEL_RETURN_MAIN_QUESTION = GetTupleReference(907764699145L);

			public static readonly LOS.TupleReference MENU_LABEL_SHARE = GetTupleReference(907764699146L);

			public static readonly LOS.TupleReference MENU_LABEL_SKIP = GetTupleReference(907764699147L);

			public static readonly LOS.TupleReference MENU_LABEL_SOUND = GetTupleReference(907764699148L);

			public static readonly LOS.TupleReference MENU_LABEL_THANK_YOU = GetTupleReference(907764699149L);

			public static readonly LOS.TupleReference MENU_LABEL_UNLOCK = GetTupleReference(907764699150L);

			public static readonly LOS.TupleReference MENU_LABEL_VIDEO = GetTupleReference(907764699151L);

			public static readonly LOS.TupleReference MENU_LABEL_VOICES = GetTupleReference(907764699152L);

			public static readonly LOS.TupleReference MENU_LABEL_WARNING = GetTupleReference(907764699153L);

			public static readonly LOS.TupleReference MENU_LABEL_WELCOME = GetTupleReference(907764699154L);

			public static readonly LOS.TupleReference MENU_LABEL_WORLD = GetTupleReference(907764699155L);

			public static readonly LOS.TupleReference MENU_LABEL_YEAH = GetTupleReference(907764699156L);

			public static readonly LOS.TupleReference MENU_LABEL_YES = GetTupleReference(907764699157L);

			public static readonly LOS.TupleReference MENU_LABEL_YES_CAPS = GetTupleReference(907764699158L);

			public static readonly LOS.TupleReference MENU_LANGUAGE_DIRECTION = GetTupleReference(907764699159L);

			public static readonly LOS.TupleReference MENU_LANGUAGE_THIS = GetTupleReference(907764699160L);

			public static readonly LOS.TupleReference MENU_LANGUAGE_THIS_EN = GetTupleReference(907764699161L);

			public static readonly LOS.TupleReference MENU_LOGIN = GetTupleReference(907764699162L);

			public static readonly LOS.TupleReference MENU_LOGIN_EMAIL = GetTupleReference(907764699163L);

			public static readonly LOS.TupleReference MENU_LOGIN_FORGOT = GetTupleReference(907764699164L);

			public static readonly LOS.TupleReference MENU_LOGIN_PASSWORD = GetTupleReference(907764699165L);

			public static readonly LOS.TupleReference MENU_LOGIN_REGISTER = GetTupleReference(907764699166L);

			public static readonly LOS.TupleReference MENU_LOGIN_REMEMBER = GetTupleReference(907764699167L);

			public static readonly LOS.TupleReference MENU_MODE_CHALLENGE = GetTupleReference(907764699168L);

			public static readonly LOS.TupleReference MENU_MODE_CHALLENGE_MODE = GetTupleReference(907764699169L);

			public static readonly LOS.TupleReference MENU_MODE_COOP = GetTupleReference(907764699170L);

			public static readonly LOS.TupleReference MENU_MODE_COOPERATIVE = GetTupleReference(907764699171L);

			public static readonly LOS.TupleReference MENU_MODE_DEATHMATCH = GetTupleReference(907764699172L);

			public static readonly LOS.TupleReference MENU_MODE_HELP = GetTupleReference(907764699173L);

			public static readonly LOS.TupleReference MENU_MODE_INSTRUCTIONS = GetTupleReference(907764699174L);

			public static readonly LOS.TupleReference MENU_MODE_MULTIPLAYER = GetTupleReference(907764699175L);

			public static readonly LOS.TupleReference MENU_MODE_MULTIPLAYER_LOCAL = GetTupleReference(907764699176L);

			public static readonly LOS.TupleReference MENU_MODE_MULTIPLAYER_ONLINE = GetTupleReference(907764699177L);

			public static readonly LOS.TupleReference MENU_MODE_PLAY_ONLINE = GetTupleReference(907764699178L);

			public static readonly LOS.TupleReference MENU_MODE_SINGLEPLAYER = GetTupleReference(907764699179L);

			public static readonly LOS.TupleReference MENU_MODE_TRAINING = GetTupleReference(907764699180L);

			public static readonly LOS.TupleReference MENU_MODE_TUTORIAL = GetTupleReference(907764699181L);

			public static readonly LOS.TupleReference MENU_MODE_VERSUS = GetTupleReference(907764699182L);

			public static readonly LOS.TupleReference MENU_NAME = GetTupleReference(907764699183L);

			public static readonly LOS.TupleReference MENU_NAME_FIRST = GetTupleReference(907764699184L);

			public static readonly LOS.TupleReference MENU_NAME_LAST = GetTupleReference(907764699185L);

			public static readonly LOS.TupleReference MENU_NAME_USER = GetTupleReference(907764699186L);

			public static readonly LOS.TupleReference MENU_OPTIONS = GetTupleReference(907764699187L);

			public static readonly LOS.TupleReference MENU_OPTIONS_ADVANCEDGRAPHICS = GetTupleReference(907764699188L);

			public static readonly LOS.TupleReference MENU_OPTIONS_ANTIALIASING = GetTupleReference(907764699189L);

			public static readonly LOS.TupleReference MENU_OPTIONS_BLOOM = GetTupleReference(907764699190L);

			public static readonly LOS.TupleReference MENU_OPTIONS_CONTROLS = GetTupleReference(907764699191L);

			public static readonly LOS.TupleReference MENU_OPTIONS_DIFFICULTY = GetTupleReference(907764699192L);

			public static readonly LOS.TupleReference MENU_OPTIONS_DISPLAY = GetTupleReference(907764699193L);

			public static readonly LOS.TupleReference MENU_OPTIONS_FIRST_PERSON_VIEW = GetTupleReference(907764699194L);

			public static readonly LOS.TupleReference MENU_OPTIONS_FOV = GetTupleReference(907764699195L);

			public static readonly LOS.TupleReference MENU_OPTIONS_FULLSCREEN = GetTupleReference(907764699196L);

			public static readonly LOS.TupleReference MENU_OPTIONS_GAMEPLAY = GetTupleReference(907764699197L);

			public static readonly LOS.TupleReference MENU_OPTIONS_GRAPHICS = GetTupleReference(907764699198L);

			public static readonly LOS.TupleReference MENU_OPTIONS_HUD = GetTupleReference(907764699199L);

			public static readonly LOS.TupleReference MENU_OPTIONS_MUSIC_VOLUME = GetTupleReference(907764699200L);

			public static readonly LOS.TupleReference MENU_OPTIONS_NETWORK = GetTupleReference(907764699201L);

			public static readonly LOS.TupleReference MENU_OPTIONS_QUALITY = GetTupleReference(907764699202L);

			public static readonly LOS.TupleReference MENU_OPTIONS_RESOLUTION = GetTupleReference(907764699203L);

			public static readonly LOS.TupleReference MENU_OPTIONS_SFX_VOLUME = GetTupleReference(907764699204L);

			public static readonly LOS.TupleReference MENU_OPTIONS_SHADOW = GetTupleReference(907764699205L);

			public static readonly LOS.TupleReference MENU_OPTIONS_SHADOW_PLURAL = GetTupleReference(907764699206L);

			public static readonly LOS.TupleReference MENU_OPTIONS_SIZE = GetTupleReference(907768893440L);

			public static readonly LOS.TupleReference MENU_OPTIONS_SOUND = GetTupleReference(907768893441L);

			public static readonly LOS.TupleReference MENU_OPTIONS_SOUND_VOLUME = GetTupleReference(907768893442L);

			public static readonly LOS.TupleReference MENU_OPTIONS_SPLITSCREEN = GetTupleReference(907768893443L);

			public static readonly LOS.TupleReference MENU_OPTIONS_THIRD_PERSON_VIEW = GetTupleReference(907768893444L);

			public static readonly LOS.TupleReference MENU_OPTIONS_VIDEO = GetTupleReference(907768893445L);

			public static readonly LOS.TupleReference MENU_OPTIONS_VIDEO_QUALITY = GetTupleReference(907768893446L);

			public static readonly LOS.TupleReference MENU_OPTIONS_VOICE_VOLUME = GetTupleReference(907768893447L);

			public static readonly LOS.TupleReference MENU_OPTIONS_VSYNC = GetTupleReference(907768893448L);

			public static readonly LOS.TupleReference MENU_OPTIONS_WINDOWED = GetTupleReference(907768893449L);

			public static readonly LOS.TupleReference MENU_OPTIONS_SHADER_DETAIL = GetTupleReference(907768893450L);

			public static readonly LOS.TupleReference MENU_OPTIONS_TEXTURE_DETAIL = GetTupleReference(907768893451L);

			public static readonly LOS.TupleReference MENU_OPTIONS_MULTICORE_RENDERING = GetTupleReference(907768893452L);

			public static readonly LOS.TupleReference MENU_OPTIONS_MODEL_DETAIL = GetTupleReference(907768893453L);

			public static readonly LOS.TupleReference MENU_OPTIONS_WIDESCREEN = GetTupleReference(907768893454L);

			public static readonly LOS.TupleReference MENU_OPTIONS_EFFECT_DETAIL = GetTupleReference(907768893455L);

			public static readonly LOS.TupleReference MENU_OPTIONS_FILTERING_ANISOTROPIC = GetTupleReference(907768893456L);

			public static readonly LOS.TupleReference MENU_OPTIONS_FILTERING_BILINEAR = GetTupleReference(907768893457L);

			public static readonly LOS.TupleReference MENU_OPTIONS_FILTERING_TRILINEAR = GetTupleReference(907768893458L);

			public static readonly LOS.TupleReference MENU_OPTIONS_FILTERING_MODE = GetTupleReference(907768893459L);

			public static readonly LOS.TupleReference MENU_SETTING_ULTRA = GetTupleReference(907768893460L);

			public static readonly LOS.TupleReference MENU_SETTING_VERY_HIGH = GetTupleReference(907768893461L);

			public static readonly LOS.TupleReference MENU_SETTING_HIGH = GetTupleReference(907768893462L);

			public static readonly LOS.TupleReference MENU_SETTING_MEDIUM = GetTupleReference(907768893463L);

			public static readonly LOS.TupleReference MENU_SETTING_LOW = GetTupleReference(907768893464L);

			public static readonly LOS.TupleReference MENU_VERSION = GetTupleReference(907768893465L);

			public static readonly LOS.TupleReference MENU_VERSION_DEMO = GetTupleReference(907768893466L);

			public static readonly LOS.TupleReference MENU_VERSION_FREE = GetTupleReference(907768893467L);

			public static readonly LOS.TupleReference MENU_VERSION_FULL = GetTupleReference(907768893468L);

			public static readonly LOS.TupleReference MENU_VERSION_LITE = GetTupleReference(907768893469L);

			public static readonly LOS.TupleReference MENU_VERSION_PREMIUM = GetTupleReference(907768893470L);

			public static readonly LOS.TupleReference MENU_VERSION_SHAREWARE = GetTupleReference(907768893471L);

			public static readonly LOS.TupleReference MENU_VERSION_TRIAL = GetTupleReference(907768893472L);

			public static readonly LOS.TupleReference MENU_VR_COMFORT_MODE = GetTupleReference(907768893473L);

			public static readonly LOS.TupleReference MENU_VR_HMD_ORIENT_RESET = GetTupleReference(907768893474L);

			public static readonly LOS.TupleReference MENU_VR_VIRTUAL_REALITY = GetTupleReference(907768893475L);
		}

		public static class Sets
		{
		}

		public const string TABLE_NAME = "Polyglot";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			2, 154, 173, 32, 228, 171, 132, 160, 216, 177,
			33, 48, 35, 51, 107, 167
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string LANGUAGE_CODE => GS(907575955456L);

		public static string LANGUAGE_EN => GS(907601121280L);

		public static string LANGUAGE_DIRECTION => GS(1066867232768L);

		public static string CONTEXT_CHARACTER_CLASS => GS(907605315585L);

		public static string CONTEXT_CHARACTER_DELETE => GS(907605315586L);

		public static string CONTEXT_CHARACTER_NEW => GS(907605315587L);

		public static string CONTEXT_CLASS_AI => GS(907605315588L);

		public static string CONTEXT_CLASS_ALIEN => GS(907605315589L);

		public static string CONTEXT_CLASS_CLONE => GS(907605315590L);

		public static string CONTEXT_CLASS_CYBORG => GS(907605315591L);

		public static string CONTEXT_CLASS_DEITY => GS(907605315592L);

		public static string CONTEXT_CLASS_DEMON => GS(907605315593L);

		public static string CONTEXT_CLASS_DEMONNESS => GS(907605315594L);

		public static string CONTEXT_CLASS_DRAGON => GS(907605315595L);

		public static string CONTEXT_CLASS_DRAGON_PLURAL => GS(907605315596L);

		public static string CONTEXT_CLASS_GHOST => GS(907605315597L);

		public static string CONTEXT_CLASS_GOD => GS(907605315598L);

		public static string CONTEXT_CLASS_GOD_PLURAL => GS(907605315599L);

		public static string CONTEXT_CLASS_GODDESS => GS(907605315600L);

		public static string CONTEXT_CLASS_MAGE => GS(907605315601L);

		public static string CONTEXT_CLASS_MEDIC => GS(907605315602L);

		public static string CONTEXT_CLASS_MONSTER => GS(907605315603L);

		public static string CONTEXT_CLASS_NINJA => GS(907605315604L);

		public static string CONTEXT_CLASS_PIRATE => GS(907605315605L);

		public static string CONTEXT_CLASS_PRIEST => GS(907605315606L);

		public static string CONTEXT_CLASS_PRINCE => GS(907605315607L);

		public static string CONTEXT_CLASS_PRINCESS => GS(907605315608L);

		public static string CONTEXT_CLASS_ROBOT => GS(907605315609L);

		public static string CONTEXT_CLASS_SKELETON => GS(907605315610L);

		public static string CONTEXT_CLASS_SNIPER => GS(907605315611L);

		public static string CONTEXT_CLASS_SOLDIER => GS(907605315612L);

		public static string CONTEXT_CLASS_THIEF => GS(907605315613L);

		public static string CONTEXT_CLASS_WARRIOR => GS(907605315614L);

		public static string CONTEXT_CLASS_WIZARD => GS(907605315615L);

		public static string CONTEXT_CLASS_ZOMBIE => GS(907605315616L);

		public static string CONTEXT_CLASS_ZOMBIE_PLURAL => GS(907605315617L);

		public static string CONTEXT_COMPETITION_FIRST => GS(907605315618L);

		public static string CONTEXT_COMPETITION_PLACE_NOUN => GS(907605315619L);

		public static string CONTEXT_COMPETITION_SECOND => GS(907605315620L);

		public static string CONTEXT_COMPETITION_THIRD => GS(907605315621L);

		public static string CONTEXT_CONDITION_BURNING => GS(907605315622L);

		public static string CONTEXT_CONDITION_CRITICAL => GS(907605315623L);

		public static string CONTEXT_CONDITION_DEAD => GS(907605315624L);

		public static string CONTEXT_CONDITION_FROZEN => GS(907605315625L);

		public static string CONTEXT_CONDITION_POISONED => GS(907605315626L);

		public static string CONTEXT_CONDITION_SLOWED => GS(907605315627L);

		public static string CONTEXT_CONDITION_STUNNED => GS(907605315628L);

		public static string CONTEXT_CONDITION_UNCONSCIOUS => GS(907605315629L);

		public static string CONTEXT_CONDITION_UNHURT => GS(907605315630L);

		public static string CONTEXT_CONDITION_WET => GS(907605315631L);

		public static string CONTEXT_CONDITION_WOUNDED => GS(907605315632L);

		public static string CONTEXT_DESCRIPTION_BIG => GS(907605315633L);

		public static string CONTEXT_DESCRIPTION_HUGE => GS(907605315634L);

		public static string CONTEXT_DESCRIPTION_SMALL => GS(907605315635L);

		public static string CONTEXT_DESCRIPTION_TINY => GS(907605315636L);

		public static string CONTEXT_ELEMENT_AIR => GS(907605315637L);

		public static string CONTEXT_ELEMENT_EARTH => GS(907605315638L);

		public static string CONTEXT_ELEMENT_FIRE => GS(907605315639L);

		public static string CONTEXT_ELEMENT_METAL => GS(907605315640L);

		public static string CONTEXT_ELEMENT_WATER => GS(907605315641L);

		public static string CONTEXT_GAMBLING_BET => GS(907605315642L);

		public static string CONTEXT_GAMBLING_DISCARD => GS(907605315643L);

		public static string CONTEXT_GAMBLING_JACKPOT => GS(907605315644L);

		public static string CONTEXT_GAMBLING_MAX => GS(907605315645L);

		public static string CONTEXT_GAMBLING_MIN => GS(907605315646L);

		public static string CONTEXT_ITEM_ARMOR => GS(907605315647L);

		public static string CONTEXT_ITEM_ARROW => GS(907605315648L);

		public static string CONTEXT_ITEM_ARROW_PLURAL => GS(907605315649L);

		public static string CONTEXT_ITEM_BOARD => GS(907605315650L);

		public static string CONTEXT_ITEM_BOMB => GS(907609509888L);

		public static string CONTEXT_ITEM_BOOK => GS(907609509889L);

		public static string CONTEXT_ITEM_BOOTS => GS(907609509890L);

		public static string CONTEXT_ITEM_BOW => GS(907609509891L);

		public static string CONTEXT_ITEM_CARD => GS(907609509892L);

		public static string CONTEXT_ITEM_CARD_PLURAL => GS(907609509893L);

		public static string CONTEXT_ITEM_CHEST => GS(907609509894L);

		public static string CONTEXT_ITEM_COND_MAGICAL => GS(907609509895L);

		public static string CONTEXT_ITEM_COND_RUSTY => GS(907609509896L);

		public static string CONTEXT_ITEM_DAGGER => GS(907609509897L);

		public static string CONTEXT_ITEM_DIE_NOUN => GS(907609509898L);

		public static string CONTEXT_ITEM_DIE_PLURAL => GS(907609509899L);

		public static string CONTEXT_ITEM_DOOR => GS(907609509900L);

		public static string CONTEXT_ITEM_GAUNTLETS => GS(907609509901L);

		public static string CONTEXT_ITEM_GEM => GS(907609509902L);

		public static string CONTEXT_ITEM_GIFT_NOUN => GS(907609509903L);

		public static string CONTEXT_ITEM_GIFT_PLURAL => GS(907609509904L);

		public static string CONTEXT_ITEM_GLOVE => GS(907609509905L);

		public static string CONTEXT_ITEM_GUN => GS(907609509906L);

		public static string CONTEXT_ITEM_HAMMER => GS(907609509907L);

		public static string CONTEXT_ITEM_HELMET => GS(907609509908L);

		public static string CONTEXT_ITEM_KEY => GS(907609509909L);

		public static string CONTEXT_ITEM_KNIFE => GS(907609509910L);

		public static string CONTEXT_ITEM_LOCK => GS(907609509911L);

		public static string CONTEXT_ITEM_LOVE => GS(907609509912L);

		public static string CONTEXT_ITEM_MACHINE_ITEM_GUN => GS(907609509913L);

		public static string CONTEXT_ITEM_MAGIC => GS(907609509914L);

		public static string CONTEXT_ITEM_PISTOL => GS(907609509915L);

		public static string CONTEXT_ITEM_QUEST_NOUN => GS(907609509916L);

		public static string CONTEXT_ITEM_RANK_NOUN => GS(907609509917L);

		public static string CONTEXT_ITEM_RIFLE => GS(907609509918L);

		public static string CONTEXT_ITEM_ROCKET_ITEM_LAUNCHER => GS(907609509919L);

		public static string CONTEXT_ITEM_RPG => GS(907609509920L);

		public static string CONTEXT_ITEM_SCROLL => GS(907609509921L);

		public static string CONTEXT_ITEM_SHIELD => GS(907609509922L);

		public static string CONTEXT_ITEM_SHIELD_PLURAL => GS(907609509923L);

		public static string CONTEXT_ITEM_SHIELD_SCIFI => GS(907609509924L);

		public static string CONTEXT_ITEM_SHIELD_SCIFI_PLURAL => GS(907609509925L);

		public static string CONTEXT_ITEM_SHOTGUN => GS(907609509926L);

		public static string CONTEXT_ITEM_SNIPER_ITEM_RIFLE => GS(907609509927L);

		public static string CONTEXT_ITEM_SPELL => GS(907609509928L);

		public static string CONTEXT_ITEM_STAFF => GS(907609509929L);

		public static string CONTEXT_ITEM_SWORD => GS(907609509930L);

		public static string CONTEXT_ITEM_TREASURE => GS(907609509931L);

		public static string CONTEXT_ITEM_TYPE_EPIC => GS(907609509932L);

		public static string CONTEXT_ITEM_TYPE_JUNK => GS(907609509933L);

		public static string CONTEXT_ITEM_WAND => GS(907609509934L);

		public static string CONTEXT_ITEM_WOOD => GS(907609509935L);

		public static string CONTEXT_MACHINE_ALTITUDE => GS(907613704192L);

		public static string CONTEXT_MACHINE_BRAKES => GS(907613704193L);

		public static string CONTEXT_MACHINE_CHASSIS => GS(907613704194L);

		public static string CONTEXT_MACHINE_DEPTH => GS(907613704195L);

		public static string CONTEXT_MACHINE_FUEL => GS(907613704196L);

		public static string CONTEXT_MACHINE_GEAR => GS(907613704197L);

		public static string CONTEXT_MACHINE_HANDLING => GS(907613704198L);

		public static string CONTEXT_MACHINE_SPEED => GS(907613704199L);

		public static string CONTEXT_MACHINE_STEERING => GS(907613704200L);

		public static string CONTEXT_MACHINE_SUSPENSION => GS(907613704201L);

		public static string CONTEXT_MACHINE_WHEELS => GS(907613704202L);

		public static string CONTEXT_MACHINE_WINGS => GS(907613704203L);

		public static string CONTEXT_MEDAL => GS(907613704204L);

		public static string CONTEXT_MEDAL_BRONZE => GS(907613704205L);

		public static string CONTEXT_MEDAL_GOLD => GS(907613704206L);

		public static string CONTEXT_MEDAL_SILVER => GS(907613704207L);

		public static string CONTEXT_METAL_BRONZE => GS(907613704208L);

		public static string CONTEXT_METAL_COIN => GS(907613704209L);

		public static string CONTEXT_METAL_GOLD => GS(907613704210L);

		public static string CONTEXT_METAL_INGOT => GS(907613704211L);

		public static string CONTEXT_METAL_PLATINUM => GS(907613704212L);

		public static string CONTEXT_METAL_SILVER => GS(907613704213L);

		public static string CONTEXT_PLACE_CASTLE => GS(907613704214L);

		public static string CONTEXT_PLACE_CAVE => GS(907613704215L);

		public static string CONTEXT_PLACE_DUNGEON => GS(907613704216L);

		public static string CONTEXT_PLACE_INN => GS(907613704217L);

		public static string CONTEXT_PLACE_PLANET => GS(907613704218L);

		public static string CONTEXT_PLACE_PLANET_EARTH => GS(907613704219L);

		public static string CONTEXT_PLACE_SPACE => GS(907613704220L);

		public static string CONTEXT_PLACE_SPACESHIP => GS(907613704221L);

		public static string CONTEXT_STORY => GS(907613704222L);

		public static string CONTEXT_STORY_ADVENTURE => GS(907613704223L);

		public static string CONTEXT_STORY_CHAPTER => GS(907613704224L);

		public static string CONTEXT_STORY_PAGE => GS(907613704225L);

		public static string CREDITS_GAME => GS(907613704226L);

		public static string CREDITS_GAME_DEVELOPMENT => GS(907613704227L);

		public static string CREDITS_GAME_DEVELOPER => GS(907613704228L);

		public static string CREDITS_DEVELOPED_BY => GS(907613704229L);

		public static string CREDITS_PRODUCTION => GS(907613704230L);

		public static string CREDITS_PRODUCER => GS(907613704231L);

		public static string CREDITS_PUBLISHER => GS(907613704232L);

		public static string CREDITS_DIRECTOR => GS(907613704233L);

		public static string CREDITS_LEAD => GS(907613704234L);

		public static string CREDITS_ASSISTANT => GS(907613704235L);

		public static string CREDITS_ASSOCIATE => GS(907613704236L);

		public static string CREDITS_SENIOR => GS(907613704237L);

		public static string CREDITS_JUNIOR => GS(907613704238L);

		public static string CREDITS_CHIEF_TECH_OFFICER => GS(907613704239L);

		public static string CREDITS_TECHNICAL_DIRECTOR => GS(907613704240L);

		public static string CREDITS_CREATIVE_DIRECTOR => GS(907613704241L);

		public static string CREDITS_ANIMATION_DIRECTOR => GS(907613704242L);

		public static string CREDITS_ART_DIRECTOR => GS(907613704243L);

		public static string CREDITS_AUDIO_DIRECTOR => GS(907613704244L);

		public static string CREDITS_DESIGN_DIRECTOR => GS(907613704245L);

		public static string CREDITS_PROGRAM_MANAGER => GS(907613704246L);

		public static string CREDITS_PROJECT_MANAGER => GS(907613704247L);

		public static string CREDITS_GAME_DESIGN => GS(907613704248L);

		public static string CREDITS_GAME_DESIGNER => GS(907613704249L);

		public static string CREDITS_CODE => GS(907613704250L);

		public static string CREDITS_PROGRAMMING => GS(907613704251L);

		public static string CREDITS_PROGRAMMER => GS(907613704252L);

		public static string CREDITS_PROGRAMMER_PLURAL => GS(907613704253L);

		public static string CREDITS_LEAD_PROGRAMMER => GS(907613704254L);

		public static string CREDITS_GRAPHIC_DESIGNER => GS(907613704255L);

		public static string CREDITS_TECHNICAL_ARTIST => GS(907613704256L);

		public static string CREDITS_CONCEPT_ARTIST => GS(907613704257L);

		public static string CREDITS_CHARACTER_DESIGNER => GS(907613704258L);

		public static string CREDITS_ART => GS(907613704259L);

		public static string CREDITS_ARTIST => GS(907613704260L);

		public static string CREDITS_ARTIST_PLURAL => GS(907613704261L);

		public static string CREDITS_LEVEL_DESIGN => GS(907613704262L);

		public static string CREDITS_LEVEL_DESIGNER => GS(907613704263L);

		public static string CREDITS_CINEMATICS_DIRECTOR => GS(907613704264L);

		public static string CREDITS_CINEMATIC_ANIMATOR => GS(907613704265L);

		public static string CREDITS_INTERFACE_ARTIST => GS(907613704266L);

		public static string CREDITS_COMPOSER => GS(907613704267L);

		public static string CREDITS_MUSICIAN => GS(907613704268L);

		public static string CREDITS_SPECIAL_THANKS => GS(907613704269L);

		public static string CREDITS_SPECIAL_THANKS_TO => GS(907613704270L);

		public static string CREDITS_IMAGE => GS(907617898496L);

		public static string CREDITS_IMAGE_PLURAL => GS(907617898497L);

		public static string CREDITS_FONT => GS(907617898498L);

		public static string CREDITS_FONT_PLURAL => GS(907617898499L);

		public static string CREDITS_SOUNDTRACK => GS(907617898500L);

		public static string CREDITS_WRITER => GS(907617898501L);

		public static string CREDITS_MANUSCRIPT => GS(907617898502L);

		public static string CREDITS_SCRIPT_WRITER => GS(907617898503L);

		public static string CREDITS_ANIMATION => GS(907617898504L);

		public static string CREDITS_ANIMATOR => GS(907617898505L);

		public static string CREDITS_VOICE_ACTING => GS(907617898506L);

		public static string CREDITS_VOICE_ACTOR => GS(907617898507L);

		public static string CREDITS_SOUND_DESIGNER => GS(907617898508L);

		public static string CREDITS_SOUND_ENGINEER => GS(907617898509L);

		public static string CREDITS_SFX => GS(907617898510L);

		public static string CREDITS_VFX => GS(907617898511L);

		public static string CREDITS_RECORDIST => GS(907617898512L);

		public static string CREDITS_FOLEY_ARTIST => GS(907617898513L);

		public static string CREDITS_TEST_MANAGER => GS(907617898514L);

		public static string CREDITS_TEST_LEAD => GS(907617898515L);

		public static string CREDITS_TESTER => GS(907617898516L);

		public static string CREDITS_DATABASE_ADMIN => GS(907617898517L);

		public static string CREDITS_3D_MODELING => GS(907617898518L);

		public static string CREDITS_MODELS => GS(907617898519L);

		public static string CREDITS_LOCALIZATION => GS(907617898520L);

		public static string CREDITS_TRANSLATION => GS(907617898521L);

		public static string CREDITS_POLYGLOT => GS(907617898522L);

		public static string GAME_GENRE_ACTION => GS(907617898523L);

		public static string GAME_GENRE_ADVENTURE => GS(907617898524L);

		public static string GAME_GENRE_CARD => GS(907617898525L);

		public static string GAME_GENRE_CASUAL => GS(907617898526L);

		public static string GAME_GENRE_FANTASY => GS(907617898527L);

		public static string GAME_GENRE_HARDCORE => GS(907617898528L);

		public static string GAME_GENRE_INDIE => GS(907617898529L);

		public static string GAME_GENRE_MEDIEVAL => GS(907617898530L);

		public static string GAME_GENRE_MISC => GS(907617898531L);

		public static string GAME_GENRE_MULTIPLAYER => GS(907617898532L);

		public static string GAME_GENRE_NAME => GS(907617898533L);

		public static string GAME_GENRE_PLATFORMER => GS(907617898534L);

		public static string GAME_GENRE_PUZZLE => GS(907617898535L);

		public static string GAME_GENRE_QUIZ => GS(907617898536L);

		public static string GAME_GENRE_RACING => GS(907617898537L);

		public static string GAME_GENRE_RETRO => GS(907617898538L);

		public static string GAME_GENRE_RPG => GS(907617898539L);

		public static string GAME_GENRE_RTS => GS(907617898540L);

		public static string GAME_GENRE_SCIFI => GS(907617898541L);

		public static string GAME_GENRE_SHOOTER => GS(907617898542L);

		public static string GAME_GENRE_SIMULATION => GS(907617898543L);

		public static string GAME_GENRE_SINGLEPLAYER => GS(907617898544L);

		public static string GAME_GENRE_SPORTS => GS(907617898545L);

		public static string GAME_GENRE_STRATEGY => GS(907617898546L);

		public static string GAME_GENRE_TBS => GS(907617898547L);

		public static string GAME_GENRE_TOWER_DEFENSE => GS(907617898548L);

		public static string ERROR_DEVICE_VR_LEAVING_TRACKAREA => GS(907617898550L);

		public static string ERROR_DEVICE_VR_NO_HMD => GS(907617898551L);

		public static string ERROR_GENERIC_ERRCODE => GS(907617898552L);

		public static string ERROR_GENERIC_TEXT => GS(907617898553L);

		public static string ERROR_GENERIC_INVALID_NAME => GS(907617898554L);

		public static string ERROR_GENERIC_INVALID_CD_KEY => GS(907617898555L);

		public static string ERROR_GENERIC_CHEATING => GS(907617898556L);

		public static string ERROR_NETWORK_CONNECTION_LOST => GS(907617898557L);

		public static string ERROR_NETWORK_DISCONNECTED_YOUR_OPPONENT => GS(907617898558L);

		public static string ERROR_NETWORK_DISCONNECTED_YOUR_PLAYER => GS(907617898559L);

		public static string ERROR_NETWORK_DISCONNECTED_YOUR_TEAMMATE => GS(907617898560L);

		public static string ERROR_NETWORK_SERVER => GS(907617898561L);

		public static string ERROR_SAVE_CORRUPTED => GS(907617898562L);

		public static string ERROR_SAVE_NO_GAMES_FOUND => GS(907617898563L);

		public static string ERROR_SAVE_NO_OPEN_GAMES => GS(907622092800L);

		public static string GAME_ACTION_ATTACK => GS(907622092801L);

		public static string GAME_ACTION_BOOST => GS(907622092802L);

		public static string GAME_ACTION_BUILD => GS(907622092803L);

		public static string GAME_ACTION_CAST_FISHING => GS(907622092804L);

		public static string GAME_ACTION_CAST_SPELL => GS(907622092805L);

		public static string GAME_ACTION_CLAIM => GS(907622092806L);

		public static string GAME_ACTION_CLIMB => GS(907622092807L);

		public static string GAME_ACTION_CLOSE => GS(907622092808L);

		public static string GAME_ACTION_COLLECT => GS(907622092809L);

		public static string GAME_ACTION_COOK => GS(907622092810L);

		public static string GAME_ACTION_CRAFT => GS(907622092811L);

		public static string GAME_ACTION_CROUCH => GS(907622092812L);

		public static string GAME_ACTION_DODGE => GS(907622092813L);

		public static string GAME_ACTION_DRIVE => GS(907622092814L);

		public static string GAME_ACTION_FLY => GS(907622092815L);

		public static string GAME_ACTION_FULL_SPEED => GS(907622092816L);

		public static string GAME_ACTION_GO => GS(907622092817L);

		public static string GAME_ACTION_GO_TO_PLACE => GS(907622092818L);

		public static string GAME_ACTION_HALF_SPEED => GS(907622092819L);

		public static string GAME_ACTION_HEAL => GS(907622092820L);

		public static string GAME_ACTION_JUMP => GS(907622092821L);

		public static string GAME_ACTION_LAND => GS(907622092822L);

		public static string GAME_ACTION_LOOK => GS(907622092823L);

		public static string GAME_ACTION_MIX => GS(907622092824L);

		public static string GAME_ACTION_OPEN => GS(907622092825L);

		public static string GAME_ACTION_PICK_UP => GS(907622092826L);

		public static string GAME_ACTION_RACE => GS(907622092827L);

		public static string GAME_ACTION_READ => GS(907622092828L);

		public static string GAME_ACTION_REPLACE => GS(907622092829L);

		public static string GAME_ACTION_RUN => GS(907622092830L);

		public static string GAME_ACTION_RUN_AWAY => GS(907622092831L);

		public static string GAME_ACTION_SHOOT => GS(907622092832L);

		public static string GAME_ACTION_SNEAK => GS(907622092833L);

		public static string GAME_ACTION_SPIN => GS(907622092834L);

		public static string GAME_ACTION_SPRINT => GS(907622092835L);

		public static string GAME_ACTION_STEER => GS(907622092836L);

		public static string GAME_ACTION_TAKE_OFF => GS(907622092837L);

		public static string GAME_ACTION_TALK => GS(907622092838L);

		public static string GAME_ACTION_THROW => GS(907622092839L);

		public static string GAME_ACTION_WALK => GS(907622092840L);

		public static string GAME_CHARACTER_ENDURANCE => GS(907622092841L);

		public static string GAME_CHARACTER_EXP => GS(907622092842L);

		public static string GAME_CHARACTER_FEMALE => GS(907622092843L);

		public static string GAME_CHARACTER_HEALTH => GS(907622092844L);

		public static string GAME_CHARACTER_HEART => GS(907622092845L);

		public static string GAME_CHARACTER_HEART_PLURAL => GS(907622092846L);

		public static string GAME_CHARACTER_LEVEL => GS(907622092847L);

		public static string GAME_CHARACTER_LEVEL_UP => GS(907622092848L);

		public static string GAME_CHARACTER_LIVES => GS(907622092849L);

		public static string GAME_CHARACTER_MALE => GS(907622092850L);

		public static string GAME_CHARACTER_MAN => GS(907622092851L);

		public static string GAME_CHARACTER_MAN_PLURAL => GS(907622092852L);

		public static string GAME_CHARACTER_RESPAWN => GS(907622092853L);

		public static string GAME_CHARACTER_RESPAWN_IN => GS(907622092854L);

		public static string GAME_CHARACTER_RESPAWN_QUESTION => GS(907622092855L);

		public static string GAME_CHARACTER_RESPAWNING => GS(907622092856L);

		public static string GAME_CHARACTER_SKILL => GS(907622092857L);

		public static string GAME_CHARACTER_SPAWN_POINT => GS(907622092858L);

		public static string GAME_CHARACTER_TRANSGENDER => GS(907622092859L);

		public static string GAME_CHARACTER_WOMAN => GS(907622092860L);

		public static string GAME_CHARACTER_WOMAN_PLURAL => GS(907622092861L);

		public static string GAME_DAMAGE => GS(907622092862L);

		public static string GAME_DAMAGE_CRITICAL_HIT => GS(907622092863L);

		public static string GAME_DAMAGE_CRITICAL_MISS => GS(907622092864L);

		public static string GAME_DAMAGE_HIT_NOUN => GS(907622092865L);

		public static string GAME_DAMAGE_HIT_POINTS_PLURAL => GS(907622092866L);

		public static string GAME_DAMAGE_MISS_NOUN => GS(907622092867L);

		public static string GAME_DAMAGE_UNIT => GS(907622092868L);

		public static string GAME_DAMAGE_UNIT_PLURAL => GS(907622092869L);

		public static string GAME_ENEMY => GS(907622092870L);

		public static string GAME_ENEMY_FEMALE => GS(907622092871L);

		public static string GAME_ENEMY_FEMALE_PLURAL => GS(907622092872L);

		public static string GAME_ENEMY_PLURAL => GS(907622092873L);

		public static string GAME_INVENTORY => GS(907622092874L);

		public static string GAME_INVENTORY_AMMO => GS(907622092875L);

		public static string GAME_INVENTORY_AMMUNITION => GS(907622092876L);

		public static string GAME_INVENTORY_BULLET => GS(907622092877L);

		public static string GAME_INVENTORY_BULLET_PLURAL => GS(907622092878L);

		public static string GAME_INVENTORY_BUY => GS(907622092879L);

		public static string GAME_INVENTORY_CAPACITY => GS(907622092880L);

		public static string GAME_INVENTORY_DROP => GS(907622092881L);

		public static string GAME_INVENTORY_ENCUMBRANCE => GS(907622092882L);

		public static string GAME_INVENTORY_FULL => GS(907622092883L);

		public static string GAME_INVENTORY_ITEM => GS(907622092884L);

		public static string GAME_INVENTORY_KEY => GS(907622092885L);

		public static string GAME_INVENTORY_POWERUP => GS(907622092886L);

		public static string GAME_INVENTORY_SELL => GS(907626287104L);

		public static string GAME_INVENTORY_USE => GS(907626287105L);

		public static string GAME_INVENTORY_WEAPON => GS(907626287106L);

		public static string GAME_INVENTORY_WEAPONS => GS(907626287107L);

		public static string GAME_INVENTORY_YOU_CANNOT_CARRY => GS(907626287108L);

		public static string GAME_LABEL_BE_CAREFUL => GS(907626287109L);

		public static string GAME_LABEL_PLEASE_BE_CAREFUL => GS(907626287110L);

		public static string GAME_LABEL_TACTICS => GS(907626287111L);

		public static string GAME_OBJECTIVE => GS(907626287112L);

		public static string GAME_OBJECTIVE_ASSIST => GS(907626287113L);

		public static string GAME_OBJECTIVE_ASSIST_PLURAL => GS(907626287114L);

		public static string GAME_OBJECTIVE_ATTACK => GS(907626287115L);

		public static string GAME_OBJECTIVE_BONUS => GS(907626287116L);

		public static string GAME_OBJECTIVE_CAPTURE => GS(907626287117L);

		public static string GAME_OBJECTIVE_COMPLETE => GS(907626287118L);

		public static string GAME_OBJECTIVE_DEATH => GS(907626287119L);

		public static string GAME_OBJECTIVE_DEATH_PLURAL => GS(907626287120L);

		public static string GAME_OBJECTIVE_DEFEND => GS(907626287121L);

		public static string GAME_OBJECTIVE_DESTROY => GS(907626287122L);

		public static string GAME_OBJECTIVE_ESCAPE => GS(907626287123L);

		public static string GAME_OBJECTIVE_GAME_FINISHED => GS(907626287124L);

		public static string GAME_OBJECTIVE_GAMEOVER => GS(907626287125L);

		public static string GAME_OBJECTIVE_GAMEOVER_CAPS => GS(907626287126L);

		public static string GAME_OBJECTIVE_HIGH_SCORE => GS(907626287127L);

		public static string GAME_OBJECTIVE_INCOMPLETE => GS(907626287128L);

		public static string GAME_OBJECTIVE_KILL => GS(907626287129L);

		public static string GAME_OBJECTIVE_KILL_ASSIST => GS(907626287130L);

		public static string GAME_OBJECTIVE_KILL_NOUN => GS(907626287131L);

		public static string GAME_OBJECTIVE_KILL_PLURAL => GS(907626287132L);

		public static string GAME_OBJECTIVE_LOCKED => GS(907626287133L);

		public static string GAME_OBJECTIVE_LOSSES => GS(907626287134L);

		public static string GAME_OBJECTIVE_MATCH => GS(907626287135L);

		public static string GAME_OBJECTIVE_MISSION => GS(907626287136L);

		public static string GAME_OBJECTIVE_MISSION_OBJECTIVE_PLURAL => GS(907626287137L);

		public static string GAME_OBJECTIVE_MISSION_OBJECTIVE => GS(907626287138L);

		public static string GAME_OBJECTIVE_MULTIPLIER => GS(907626287139L);

		public static string GAME_OBJECTIVE_PLURAL => GS(907626287140L);

		public static string GAME_OBJECTIVE_POSITION => GS(907626287141L);

		public static string GAME_OBJECTIVE_RACE_LAP => GS(907626287142L);

		public static string GAME_OBJECTIVE_RACE_LAP_PLURAL => GS(907626287143L);

		public static string GAME_OBJECTIVE_ROUND => GS(907626287144L);

		public static string GAME_OBJECTIVE_SCORE => GS(907626287145L);

		public static string GAME_OBJECTIVE_TIME => GS(907626287146L);

		public static string GAME_OBJECTIVE_TIME_LEFT => GS(907626287147L);

		public static string GAME_OBJECTIVE_TOURNAMENT => GS(907626287148L);

		public static string GAME_OBJECTIVE_TOURNAMENT_PLURAL => GS(907626287149L);

		public static string GAME_OBJECTIVE_WINS => GS(907626287150L);

		public static string GAME_STATUS_DEFEAT => GS(907626287151L);

		public static string GAME_STATUS_DEFEATED => GS(907626287152L);

		public static string GAME_STATUS_LOSER => GS(907626287153L);

		public static string GAME_STATUS_TEAM_LOST => GS(907626287154L);

		public static string GAME_STATUS_TEAM_WON => GS(907626287155L);

		public static string GAME_STATUS_VICTORY => GS(907626287156L);

		public static string GAME_STATUS_WINNER => GS(907626287157L);

		public static string GAME_STATUS_YOU_ARE_DEAD => GS(907626287158L);

		public static string GAME_STATUS_YOU_DIED => GS(907626287159L);

		public static string GAME_STATUS_YOU_LOSE => GS(907626287160L);

		public static string GAME_STATUS_YOU_LOST => GS(907626287161L);

		public static string GAME_STATUS_YOU_WIN => GS(907626287162L);

		public static string GAME_STATUS_YOU_WON => GS(907626287163L);

		public static string MENU_CAMERA => GS(907626287164L);

		public static string MENU_CAMERA_INVERTED => GS(907626287165L);

		public static string MENU_CAMERA_NORMAL => GS(907626287166L);

		public static string MENU_CAMERA_XAXIS => GS(907626287167L);

		public static string MENU_CAMERA_YAXIS => GS(907626287168L);

		public static string MENU_CONTROLS_BUTTON => GS(907626287169L);

		public static string MENU_CONTROLS_CLICK => GS(907626287170L);

		public static string MENU_CONTROLS_DEADZONE => GS(907626287171L);

		public static string MENU_CONTROLS_DOUBLE_CLICK => GS(907626287172L);

		public static string MENU_CONTROLS_DRAG => GS(907626287173L);

		public static string MENU_CONTROLS_DRAG_DROP => GS(907626287174L);

		public static string MENU_CONTROLS_FINGER => GS(907626287175L);

		public static string MENU_CONTROLS_GAMEPAD => GS(907626287176L);

		public static string MENU_CONTROLS_GYROSCOPE => GS(907626287177L);

		public static string MENU_CONTROLS_KEY => GS(907626287178L);

		public static string MENU_CONTROLS_KEYBOARD => GS(907626287179L);

		public static string MENU_CONTROLS_MOUSE => GS(907626287180L);

		public static string MENU_CONTROLS_MOUSE_SENSITIVITY => GS(907626287181L);

		public static string MENU_CONTROLS_PINCH => GS(907626287182L);

		public static string MENU_CONTROLS_ROTATE => GS(907626287183L);

		public static string MENU_CONTROLS_SCROLL => GS(907626287184L);

		public static string MENU_CONTROLS_SHAKE => GS(907626287185L);

		public static string MENU_CONTROLS_SPREAD => GS(907630481408L);

		public static string MENU_CONTROLS_SWIPE => GS(907630481409L);

		public static string MENU_CONTROLS_TAP => GS(907630481410L);

		public static string MENU_CONTROLS_THE_BUTTON => GS(907630481411L);

		public static string MENU_CONTROLS_THE_KEY => GS(907630481412L);

		public static string MENU_CONTROLS_TILT => GS(907630481413L);

		public static string MENU_CUSTOMIZE_ARM => GS(907630481414L);

		public static string MENU_CUSTOMIZE_ARM_PLURAL => GS(907630481415L);

		public static string MENU_CUSTOMIZE_CHEST => GS(907630481416L);

		public static string MENU_CUSTOMIZE_CHIN => GS(907630481417L);

		public static string MENU_CUSTOMIZE_COLOR => GS(907630481418L);

		public static string MENU_CUSTOMIZE_EAR => GS(907630481419L);

		public static string MENU_CUSTOMIZE_EAR_PLURAL => GS(907630481420L);

		public static string MENU_CUSTOMIZE_EYE => GS(907630481421L);

		public static string MENU_CUSTOMIZE_EYE_PLURAL => GS(907630481422L);

		public static string MENU_CUSTOMIZE_FEET => GS(907630481423L);

		public static string MENU_CUSTOMIZE_FOOT => GS(907630481424L);

		public static string MENU_CUSTOMIZE_HAIR => GS(907630481425L);

		public static string MENU_CUSTOMIZE_HAIR_BEARD => GS(907630481426L);

		public static string MENU_CUSTOMIZE_HAIR_FUR => GS(907630481427L);

		public static string MENU_CUSTOMIZE_HAIR_HEAD => GS(907630481428L);

		public static string MENU_CUSTOMIZE_HAND => GS(907630481429L);

		public static string MENU_CUSTOMIZE_HAND_PLURAL => GS(907630481430L);

		public static string MENU_CUSTOMIZE_HEAD => GS(907630481431L);

		public static string MENU_CUSTOMIZE_HIPS => GS(907630481432L);

		public static string MENU_CUSTOMIZE_LEG => GS(907630481433L);

		public static string MENU_CUSTOMIZE_LEG_PLURAL => GS(907630481434L);

		public static string MENU_CUSTOMIZE_MOUTH => GS(907630481435L);

		public static string MENU_CUSTOMIZE_NECK => GS(907630481436L);

		public static string MENU_CUSTOMIZE_NOSE => GS(907630481437L);

		public static string MENU_CUSTOMIZE_SKIN => GS(907630481438L);

		public static string MENU_CUSTOMIZE_THORAX => GS(907630481439L);

		public static string MENU_DIFFICULTY => GS(907630481440L);

		public static string MENU_DIFFICULTY_EASY => GS(907630481441L);

		public static string MENU_DIFFICULTY_HARD => GS(907630481442L);

		public static string MENU_DIFFICULTY_IMPOSSIBLE => GS(907630481443L);

		public static string MENU_DIFFICULTY_INSANE => GS(907630481444L);

		public static string MENU_DIFFICULTY_MEDIUM => GS(907630481445L);

		public static string MENU_DIFFICULTY_VERY_EASY => GS(907630481446L);

		public static string MENU_DIFFICULTY_VERY_HARD => GS(907630481447L);

		public static string MENU_DIR_DOWN => GS(907630481448L);

		public static string MENU_DIR_LEFT => GS(907630481449L);

		public static string MENU_DIR_RIGHT => GS(907630481450L);

		public static string MENU_DIR_UP => GS(907630481451L);

		public static string MENU_IO_LOAD => GS(907630481452L);

		public static string MENU_IO_LOAD_GAME => GS(907630481453L);

		public static string MENU_IO_LOADING => GS(907630481454L);

		public static string MENU_IO_PLEASE_WAIT => GS(907630481455L);

		public static string MENU_IO_RESET_SAVE_DATA => GS(907630481456L);

		public static string MENU_IO_SAVE => GS(907630481457L);

		public static string MENU_IO_SAVE_DATA => GS(907630481458L);

		public static string MENU_IO_SAVE_GAME => GS(907630481459L);

		public static string MENU_IO_SAVING => GS(907630481460L);

		public static string MENU_LABEL_ALL => GS(907630481461L);

		public static string MENU_LABEL_AGE => GS(907630481462L);

		public static string MENU_LABEL_ARE_YOU_REALLY_SURE => GS(907630481463L);

		public static string MENU_LABEL_ARE_YOU_SURE => GS(907630481464L);

		public static string MENU_LABEL_BACK => GS(907630481465L);

		public static string MENU_LABEL_BACKGROUND_MUSIC => GS(907630481466L);

		public static string MENU_LABEL_CANCEL => GS(907630481467L);

		public static string MENU_LABEL_CLOSE => GS(907630481468L);

		public static string MENU_LABEL_CONFIRM_BUTTON => GS(907630481469L);

		public static string MENU_LABEL_CONTINUE => GS(907630481470L);

		public static string MENU_LABEL_CONTINUE_QUESTION => GS(907630481471L);

		public static string MENU_LABEL_CREDITS => GS(907630481472L);

		public static string MENU_LABEL_DESKTOP => GS(907630481473L);

		public static string MENU_LABEL_DESKTOP_QUESTION => GS(907630481474L);

		public static string MENU_LABEL_DLC => GS(907630481475L);

		public static string MENU_LABEL_DOWNLOAD => GS(907630481476L);

		public static string MENU_LABEL_DOWNLOADABLE => GS(907630481477L);

		public static string MENU_LABEL_EXIT => GS(907630481478L);

		public static string MENU_LABEL_EXIT_QUESTION => GS(907630481479L);

		public static string MENU_LABEL_EXTRAS => GS(907630481480L);

		public static string MENU_LABEL_FRAMESPERSEC => GS(907630481481L);

		public static string MENU_LABEL_GALLERY => GS(907630481482L);

		public static string MENU_LABEL_GOODBYE => GS(907630481483L);

		public static string MENU_LABEL_GRAPHICS => GS(907630481484L);

		public static string MENU_LABEL_IAP => GS(907630481485L);

		public static string MENU_LABEL_LANGUAGE => GS(907630481486L);

		public static string MENU_LABEL_LEVEL => GS(907630481487L);

		public static string MENU_LABEL_MAIL => GS(907630481488L);

		public static string MENU_LABEL_MAINMENU => GS(907630481489L);

		public static string MENU_LABEL_MORE => GS(907630481490L);

		public static string MENU_LABEL_MUSIC => GS(907630481491L);

		public static string MENU_LABEL_MUSIC_PLAYER => GS(907630481492L);

		public static string MENU_LABEL_NEW_GAME => GS(907634675712L);

		public static string MENU_LABEL_NEXT => GS(907634675713L);

		public static string MENU_LABEL_NEXT_TRACK => GS(907634675714L);

		public static string MENU_LABEL_NO => GS(907634675715L);

		public static string MENU_LABEL_NO_CAPS => GS(907634675716L);

		public static string MENU_LABEL_NOPE => GS(907634675717L);

		public static string MENU_LABEL_OK => GS(907634675718L);

		public static string MENU_LABEL_PAUSE => GS(907634675719L);

		public static string MENU_LABEL_PAUSED => GS(907634675720L);

		public static string MENU_LABEL_PLAY => GS(907634675721L);

		public static string MENU_LABEL_PLAY_AGAIN => GS(907760504832L);

		public static string MENU_LABEL_PRESS_ANYKEY => GS(907760504833L);

		public static string MENU_LABEL_PRESS_ANYKEY_CONTINUE => GS(907760504834L);

		public static string MENU_LABEL_PRESS_START => GS(907760504835L);

		public static string MENU_LABEL_PRESS_START_CONTINUE => GS(907760504836L);

		public static string MENU_LABEL_PREVIOUS => GS(907760504837L);

		public static string MENU_LABEL_PREVIOUS_TRACK => GS(907760504838L);

		public static string MENU_LABEL_PRICE => GS(907764699136L);

		public static string MENU_LABEL_PRICE_FREE => GS(907764699137L);

		public static string MENU_LABEL_PURCHASE => GS(907764699138L);

		public static string MENU_LABEL_QUIT => GS(907764699139L);

		public static string MENU_LABEL_QUIT_CONFIRM => GS(907764699140L);

		public static string MENU_LABEL_QUIT_QUESTION => GS(907764699141L);

		public static string MENU_LABEL_REPLAY => GS(907764699142L);

		public static string MENU_LABEL_RETURN => GS(907764699143L);

		public static string MENU_LABEL_RETURN_MAIN => GS(907764699144L);

		public static string MENU_LABEL_RETURN_MAIN_QUESTION => GS(907764699145L);

		public static string MENU_LABEL_SHARE => GS(907764699146L);

		public static string MENU_LABEL_SKIP => GS(907764699147L);

		public static string MENU_LABEL_SOUND => GS(907764699148L);

		public static string MENU_LABEL_THANK_YOU => GS(907764699149L);

		public static string MENU_LABEL_UNLOCK => GS(907764699150L);

		public static string MENU_LABEL_VIDEO => GS(907764699151L);

		public static string MENU_LABEL_VOICES => GS(907764699152L);

		public static string MENU_LABEL_WARNING => GS(907764699153L);

		public static string MENU_LABEL_WELCOME => GS(907764699154L);

		public static string MENU_LABEL_WORLD => GS(907764699155L);

		public static string MENU_LABEL_YEAH => GS(907764699156L);

		public static string MENU_LABEL_YES => GS(907764699157L);

		public static string MENU_LABEL_YES_CAPS => GS(907764699158L);

		public static string MENU_LANGUAGE_DIRECTION => GS(907764699159L);

		public static string MENU_LANGUAGE_THIS => GS(907764699160L);

		public static string MENU_LANGUAGE_THIS_EN => GS(907764699161L);

		public static string MENU_LOGIN => GS(907764699162L);

		public static string MENU_LOGIN_EMAIL => GS(907764699163L);

		public static string MENU_LOGIN_FORGOT => GS(907764699164L);

		public static string MENU_LOGIN_PASSWORD => GS(907764699165L);

		public static string MENU_LOGIN_REGISTER => GS(907764699166L);

		public static string MENU_LOGIN_REMEMBER => GS(907764699167L);

		public static string MENU_MODE_CHALLENGE => GS(907764699168L);

		public static string MENU_MODE_CHALLENGE_MODE => GS(907764699169L);

		public static string MENU_MODE_COOP => GS(907764699170L);

		public static string MENU_MODE_COOPERATIVE => GS(907764699171L);

		public static string MENU_MODE_DEATHMATCH => GS(907764699172L);

		public static string MENU_MODE_HELP => GS(907764699173L);

		public static string MENU_MODE_INSTRUCTIONS => GS(907764699174L);

		public static string MENU_MODE_MULTIPLAYER => GS(907764699175L);

		public static string MENU_MODE_MULTIPLAYER_LOCAL => GS(907764699176L);

		public static string MENU_MODE_MULTIPLAYER_ONLINE => GS(907764699177L);

		public static string MENU_MODE_PLAY_ONLINE => GS(907764699178L);

		public static string MENU_MODE_SINGLEPLAYER => GS(907764699179L);

		public static string MENU_MODE_TRAINING => GS(907764699180L);

		public static string MENU_MODE_TUTORIAL => GS(907764699181L);

		public static string MENU_MODE_VERSUS => GS(907764699182L);

		public static string MENU_NAME => GS(907764699183L);

		public static string MENU_NAME_FIRST => GS(907764699184L);

		public static string MENU_NAME_LAST => GS(907764699185L);

		public static string MENU_NAME_USER => GS(907764699186L);

		public static string MENU_OPTIONS => GS(907764699187L);

		public static string MENU_OPTIONS_ADVANCEDGRAPHICS => GS(907764699188L);

		public static string MENU_OPTIONS_ANTIALIASING => GS(907764699189L);

		public static string MENU_OPTIONS_BLOOM => GS(907764699190L);

		public static string MENU_OPTIONS_CONTROLS => GS(907764699191L);

		public static string MENU_OPTIONS_DIFFICULTY => GS(907764699192L);

		public static string MENU_OPTIONS_DISPLAY => GS(907764699193L);

		public static string MENU_OPTIONS_FIRST_PERSON_VIEW => GS(907764699194L);

		public static string MENU_OPTIONS_FOV => GS(907764699195L);

		public static string MENU_OPTIONS_FULLSCREEN => GS(907764699196L);

		public static string MENU_OPTIONS_GAMEPLAY => GS(907764699197L);

		public static string MENU_OPTIONS_GRAPHICS => GS(907764699198L);

		public static string MENU_OPTIONS_HUD => GS(907764699199L);

		public static string MENU_OPTIONS_MUSIC_VOLUME => GS(907764699200L);

		public static string MENU_OPTIONS_NETWORK => GS(907764699201L);

		public static string MENU_OPTIONS_QUALITY => GS(907764699202L);

		public static string MENU_OPTIONS_RESOLUTION => GS(907764699203L);

		public static string MENU_OPTIONS_SFX_VOLUME => GS(907764699204L);

		public static string MENU_OPTIONS_SHADOW => GS(907764699205L);

		public static string MENU_OPTIONS_SHADOW_PLURAL => GS(907764699206L);

		public static string MENU_OPTIONS_SIZE => GS(907768893440L);

		public static string MENU_OPTIONS_SOUND => GS(907768893441L);

		public static string MENU_OPTIONS_SOUND_VOLUME => GS(907768893442L);

		public static string MENU_OPTIONS_SPLITSCREEN => GS(907768893443L);

		public static string MENU_OPTIONS_THIRD_PERSON_VIEW => GS(907768893444L);

		public static string MENU_OPTIONS_VIDEO => GS(907768893445L);

		public static string MENU_OPTIONS_VIDEO_QUALITY => GS(907768893446L);

		public static string MENU_OPTIONS_VOICE_VOLUME => GS(907768893447L);

		public static string MENU_OPTIONS_VSYNC => GS(907768893448L);

		public static string MENU_OPTIONS_WINDOWED => GS(907768893449L);

		public static string MENU_OPTIONS_SHADER_DETAIL => GS(907768893450L);

		public static string MENU_OPTIONS_TEXTURE_DETAIL => GS(907768893451L);

		public static string MENU_OPTIONS_MULTICORE_RENDERING => GS(907768893452L);

		public static string MENU_OPTIONS_MODEL_DETAIL => GS(907768893453L);

		public static string MENU_OPTIONS_WIDESCREEN => GS(907768893454L);

		public static string MENU_OPTIONS_EFFECT_DETAIL => GS(907768893455L);

		public static string MENU_OPTIONS_FILTERING_ANISOTROPIC => GS(907768893456L);

		public static string MENU_OPTIONS_FILTERING_BILINEAR => GS(907768893457L);

		public static string MENU_OPTIONS_FILTERING_TRILINEAR => GS(907768893458L);

		public static string MENU_OPTIONS_FILTERING_MODE => GS(907768893459L);

		public static string MENU_SETTING_ULTRA => GS(907768893460L);

		public static string MENU_SETTING_VERY_HIGH => GS(907768893461L);

		public static string MENU_SETTING_HIGH => GS(907768893462L);

		public static string MENU_SETTING_MEDIUM => GS(907768893463L);

		public static string MENU_SETTING_LOW => GS(907768893464L);

		public static string MENU_VERSION => GS(907768893465L);

		public static string MENU_VERSION_DEMO => GS(907768893466L);

		public static string MENU_VERSION_FREE => GS(907768893467L);

		public static string MENU_VERSION_FULL => GS(907768893468L);

		public static string MENU_VERSION_LITE => GS(907768893469L);

		public static string MENU_VERSION_PREMIUM => GS(907768893470L);

		public static string MENU_VERSION_SHAREWARE => GS(907768893471L);

		public static string MENU_VERSION_TRIAL => GS(907768893472L);

		public static string MENU_VR_COMFORT_MODE => GS(907768893473L);

		public static string MENU_VR_HMD_ORIENT_RESET => GS(907768893474L);

		public static string MENU_VR_VIRTUAL_REALITY => GS(907768893475L);

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string ERROR_DEVICE_NOT_FOUND(object arg0)
		{
			return GS(907617898549L, arg0);
		}

		public static LOS.TupleReference GetTupleReference(long entryId)
		{
			return new LOS.TupleReference("Polyglot", entryId);
		}
	}

	public static class Story
	{
		public const string TABLE_NAME = "Story";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			166, 56, 130, 8, 62, 136, 148, 141, 233, 225,
			141, 117, 98, 240, 213, 211
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}
	}

	public static class UI
	{
		public static class KeyID
		{
			public const long UI_Achievements = 5641477165056L;

			public const long UI_Mods = 5874114236416L;

			public const long UI_SHILLING = 52622597689344L;

			public const long Room_Status = 54939963232256L;

			public const long Room_Equipment = 55646338551808L;

			public const long Room_Items = 55750864801792L;

			public const long Room_Outside = 55832175579136L;

			public const long Room_Sleep = 55998030942208L;

			public const long Room_PC = 56098648100864L;

			public const long PC_EquipmentShort = 60582942085120L;

			public const long PC_Browser = 60984836100096L;

			public const long PC_Shortcut = 66758454939648L;

			public const long PC_UnreadEmails = 71063857664000L;

			public const long Options_FontColor = 78759663112192L;

			public const long Options_Sensitivity_Tooltip = 79691826204672L;

			public const long Options_ResetGameData = 81463160475648L;

			public const long Achievements_2 = 1750558276718592L;

			public const long Achievements_1 = 1750513515106304L;

			public const long Achievements_3 = 1750573665619968L;

			public const long Achievements_4 = 1752188019675136L;

			public const long Achievements_5 = 1752408820420608L;

			public const long Achievements_Cheating = 1752715424043008L;

			public const long Save_FromFile = 1754802484547584L;

			public const long Save_ToFile = 1755092688441344L;

			public const long UI_Error = 1761228200218624L;

			public const long UI_Success = 1761256759234560L;

			public const long UI_Override = 1761293052547072L;

			public const long UI_Failed = 1761603401682944L;

			public const long SAVE_OverridePrompt = 1763006232793088L;

			public const long SAVE_LoadPrompt = 1763445565165568L;

			public const long SAVE_LoadErrorPrompt = 1763689476526080L;

			public const long SAVE_LoadSuccessPrompt = 1763972839510016L;

			public const long UI_AreYouSure = 1764674592706560L;

			public const long SAVE_LoadFromFilePrompt = 1764968076546048L;

			public const long SAVE_LoadFromFilePrompt2 = 1765023525244928L;

			public const long SAVE_ExportSuccess = 1765173991706624L;

			public const long SAVE_ExportFailed = 1765233185918976L;

			public const long SAVE_DeleteSavePrompt = 1765677111054336L;

			public const long SAVE_Slot = 1766022692343808L;

			public const long SAVE_SlotEmpty = 1766333414772736L;

			public const long SAVE_ImportFailure = 1766569004634112L;

			public const long SAVE_FileExtension = 1766739557617664L;

			public const long UI_Delete = 1769398180122624L;

			public const long Save_NotesInfo = 1771411831918592L;

			public const long Options_ResetGameDataPrompt = 1784018898915328L;

			public const long Options_ResetGameDataPrompt2 = 1784182296416256L;

			public const long Mods_DataFiles = 1785235481956352L;

			public const long Mods_Status = 1785407087710208L;

			public const long Mods_VersionMismatch = 1785619994775552L;

			public const long Mods_Activating = 1785885225783296L;

			public const long Mods_NotActivating = 1786412189749248L;

			public const long Mods_RestartToActivate = 1786541231706112L;

			public const long Mods_RestartToDeactivate = 1786564413624320L;

			public const long Mods_Deactivate = 1786827136438272L;

			public const long Mods_Activate = 1786867661803520L;

			public const long Mods_NoModSelected = 1787004014432256L;

			public const long Mods_RestartRequired = 1787320051044352L;

			public const long Mods_ClosingTheGame = 1787433662156800L;

			public const long Mods_ClosingTheGamePrompt = 1787721605320704L;

			public const long Mods_DeleteModPrompt = 1788021279952896L;

			public const long Mods_LoadingSuccess = 1788364734730240L;

			public const long Mods_LoadingError = 1788709607182336L;

			public const long Mods_LoadOrder = 1789300911771648L;

			public const long Mods_ExportLog = 1789456721776640L;

			public const long Mods_Export = 1789851787464704L;

			public const long Mods_LoadMods = 1789943563030528L;

			public const long Mods_CreateNewMod = 1790020499148800L;

			public const long Mods_Edit = 1793475519320064L;

			public const long Mods_Status_Exception = 1795788409540608L;

			public const long Mods_Status_ExceptionDescription = 1795852351705088L;

			public const long Mods_Status_NotLoaded = 1795932022509568L;

			public const long Mods_Status_NotLoadedDescription = 1795937823232000L;

			public const long Mods_Status_Ready = 1796038075486208L;

			public const long Mods_Status_ReadyDescription = 1796041699364864L;

			public const long Mods_Status_Active = 1796444847476736L;

			public const long Mods_Status_ActiveDescription = 1796448366497792L;

			public const long Mods_Status_ActiveButOutdatedDescription = 1796689778053120L;

			public const long Mods_Status_ActiveButOutdated = 1796688003862528L;

			public const long Mods_Status_Failed = 1796889347231744L;

			public const long Mods_Status_FailedDescription = 1796891691847680L;

			public const long UI_Error_PathTooLong = 1800544611864576L;

			public const long UI_HungerBarTooltip = 2094004392824832L;

			public const long UI_MentalBarTooltip = 2094244428648448L;

			public const long UI_StaminaBarTooltip = 2094353702850560L;

			public const long UI_StaminaBarTooltipDynamic = 2095981008916480L;

			public const long Satiation_Full = 2096487982829568L;

			public const long Satiation_LittleHungry = 2096772633464832L;

			public const long Satiation_Hungry = 2096884579438592L;

			public const long Satiation_VeryHungry = 2096949058473984L;

			public const long Health_VeryHealthy = 2097683539824640L;

			public const long Health_Healthy = 2097731199700992L;

			public const long Health_Tired = 2097793426395136L;

			public const long Health_NearDeath = 2097933532925952L;

			public const long Health_Terrible = 2097831221268480L;

			public const long Health_Awful = 2097827458977792L;

			public const long Cum_Overfilled = 2099237902749696L;

			public const long Cum_Full = 2099316139102208L;

			public const long Cum_Half = 2099386154618880L;

			public const long Cum_Recovering = 2099429599219712L;

			public const long Cum_Empty = 2099465959641088L;

			public const long Cum_NoCum = 2099764451479552L;

			public const long Cum_Dynamic = 2099884156915712L;

			public const long UI_Day = 2100490221260800L;

			public const long Frick_CommentsVisible = 2114199559737344L;

			public const long Frick_CommentsHidden = 2114244979855360L;

			public const long Frick_Unzip = 2114513243344896L;

			public const long Frick_HideCock = 2114425968267264L;

			public const long Frick_Vagina = 2114698392506368L;

			public const long Frick_Anal = 2114694688935936L;

			public const long Frick_Manual = 2114825995816960L;

			public const long Frick_Slow = 2114827686121472L;

			public const long Frick_Medium = 2114829372231680L;

			public const long Frick_Fast = 2114832597651456L;

			public const long Frick_SlowmodeUnlockedPrompt = 2115271707725824L;

			public const long Frick_SlowMode = 2115415199059968L;

			public const long Frick_SlowModeDisable = 2115796822003712L;

			public const long Frick_SlowModeEnable = 2115848101564416L;

			public const long Frick_SlowModeDisabled = 2115896130539520L;

			public const long Frick_SlowModeEnabled = 2115898215108608L;

			public const long UI_CantDoThat = 2116365439602688L;

			public const long Frick_EndFailedPrompt = 2116415813193728L;

			public const long Frick_StreamEndPrompt = 2116779413213184L;

			public const long Frick_LastStreamSummary = 2116905913421824L;

			public const long Frick_LastStreamSummaryPrompt = 2117264815820800L;

			public const long Frick_EndFrickPrompt = 2117694098640896L;

			public const long Frick_ChatStart = 2144031383359488L;

			public const long Frick_ChatBanned = 2144246949613568L;

			public const long Frick_StreamSummary = 2144474717097984L;

			public const long Frick_Position = 2146112311791616L;

			public const long Interact_Frick = 2147091111350272L;

			public const long Interact_Talk = 2147310427312128L;

			public const long Interact_SmallTalk = 2147521937674240L;

			public const long Interact_Generic = 2147759142342656L;

			public const long Interact_Statistics = 2148276371329024L;

			public const long Interact_Nevermind = 2148378422939648L;

			public const long Equipment_Zoom = 2442436240347136L;

			public const long Equipment_LockPosition = 2442551281717248L;

			public const long Equipment_UnlockPosition = 2442590787866624L;

			public const long Equipment_EquippedPostfix = 2443240200343552L;

			public const long Equipment_EquipSetPrompt = 2443693403279360L;

			public const long UI_Done = 2444114893082624L;

			public const long Equipment_YesReplaceCurrent = 2444395538157568L;

			public const long Equipment_SetSavedSuccess = 2444932593618944L;

			public const long Equipment_SetSavedFailure = 2445047660154880L;

			public const long Equipment_SetDeletePrompt = 2445385301626880L;

			public const long Equipment_SetLoadedSuccess = 2445837711839232L;

			public const long Equipment_SetLoadedFailure = 2446030670794752L;

			public const long Equipment_SetFiletype = 2446724601614336L;

			public const long Equipment_All = 2446868155863040L;

			public const long Equipment_Food = 2447029712064512L;

			public const long Equipment_Package = 2447128693444608L;

			public const long Equipment_Modules = 2447224562651136L;

			public const long Equipment_Clothes = 2447265415172096L;

			public const long Equipment_Other = 2447384768286720L;

			public const long Equipment_Equipped = 2447438627344384L;

			public const long Equipment_NotEquipped = 2447476803899392L;

			public const long Equipment_ButtonEquip = 2447962097455104L;

			public const long Equipment_ButtonUnequip = 2447995454754816L;

			public const long Equipment_UnequipDependencyPrompt = 2448396446994432L;

			public const long Equipment_ButtonUse = 2448593407315968L;

			public const long Equipment_ButtonThrowAway = 2448687506526208L;

			public const long Equipment_ThrowAwayPrompt = 2448896844238848L;

			public const long Equipment_ThrowAwayUnequipFirst = 2449237694353408L;

			public const long Equipment_UnpackedItems = 2449390274744320L;

			public const long Equipment_UnpackedItemsPrompt = 2449415880970240L;

			public const long Equipment_FromCurrent = 2454147609530368L;

			public const long Equipment_FromFile = 2454277171580928L;

			public const long Equipment_ButtonLoadSet = 2454765636030464L;

			public const long Eqiupment_ButtonExportSet = 2454986185117696L;

			public const long Equipment_Set = 2455815231246336L;

			public const long Equipment_SetCreationPrompt = 2465429096108032L;

			public const long Equipment_SetCreationAllItems = 2465591445032960L;

			public const long Equipment_SetCreationSelect = 2465725461434368L;

			public const long Equipment_SetCreationSelectPrompt = 2465916432289792L;

			public const long Equipment_SetDefaultName = 2466019020771328L;

			public const long Equipment_SetEmpty = 2466301146435584L;

			public const long Equipment_SetListHeader = 2466378640396288L;

			public const long Equipment_SetSummaryShort = 2466914664058880L;

			public const long Equipment_SetEquippingIteration = 2467180310302720L;

			public const long Equipment_SetEquipping1 = 2467492509126656L;

			public const long Equipment_SetEquipping2 = 2467605923106816L;

			public const long Equipment_SetEquipping3 = 2467812836511744L;

			public const long Equipment_SetEquippingAs = 2468124016119808L;

			public const long Equipment_SetEquippingChangingColor = 2468356758048768L;

			public const long Equipment_SetEquippingCannotEquip = 2468601126588416L;

			public const long Equipment_SetEquippingMissing = 2468881482256384L;

			public const long Equipment_SetEquippingDroppedListHeader = 2469383586582528L;

			public const long Equipment_SetEquippingFinished = 2469075598839808L;

			public const long Equipment_SetEquippingLeftListHeader = 2469555460771840L;

			public const long Equipment_SetEquippingAllItemsFound = 2469699606417408L;

			public const long UI_ColorChangeInfo = 2482018793164800L;

			public const long UI_ColorChoose = 2482856454393856L;

			public const long Equipment_NoItemSelected = 2483178690187264L;

			public const long UI_Price = 2483338505752576L;

			public const long UI_Condition = 2483655213453312L;

			public const long Condition_Trashed = 2484800145199104L;

			public const long Condition_VeryHeavilyUsed = 2484864238358528L;

			public const long Condition_HeavilyUsed = 2484932391604224L;

			public const long Condition_Used = 2484937630289920L;

			public const long Condition_NearMint = 2484938712420352L;

			public const long Condition_New = 2485069838946304L;

			public const long UI_ReturnToMainMenuPrompt = 2836390274273280L;

			public const long Shop_BuyPrompt = 2836835176681472L;

			public const long Shop_NotEnoughMoney = 2837005276680192L;

			public const long UI_ImportFile = 2837910927577088L;

			public const long UI_ExportFile = 2838133380878336L;

			public const long UI_ExportFilePrompt = 2838282203172864L;

			public const long UI_Apply = 2838512692760576L;

			public const long UI_Cancel = 2838545097953280L;

			public const long UI_Selected = 2838828569989120L;

			public const long ColorPicker_CopyFromPreset = 2839218262773760L;

			public const long ColorPicker_RemoveInfo = 2844882489970688L;

			public const long Shop_Buy = 2872136079118336L;

			public const long Shop_BuyCount = 2872154328535040L;

			public const long Shop = 2872424458489856L;

			public const long Shop_Money = 2872751165411328L;

			public const long Live2DTool_Hand = 3176845411225600L;

			public const long Live2DTool_Mouth = 3176881821978624L;

			public const long Live2DTool_Remove = 3176929033064448L;

			public const long Interactable_MoveShirt = 3180725943537664L;

			public const long Interactable_SuckNipple = 3180833070256128L;

			public const long Interactable_PlayBoobs = 3181058551844864L;

			public const long Interactable_FuckHer = 3181141871693824L;

			public const long Interactable_SuckTongue = 3181232678375424L;

			public const long Interactable_KissHer = 3181320771342336L;

			public const long Interactable_Headpat = 3181493639581696L;

			public const long Interactable_OpenHerMouth = 3181570848329728L;

			public const long Interactable_CloseMouth = 3181733927063552L;

			public const long Interactable_PlayTongue = 3181659931152384L;

			public const long Interactable_MovePanties = 3181865342996480L;

			public const long Interactable_MoveSkirt = 3182005277560832L;

			public const long Interactable_SuckRightNipple = 3182163927109632L;

			public const long Interactable_SuckLeftNipple = 3182185431306240L;

			public const long Interactable_PullBothNipples = 3182187922722816L;

			public const long Interactable_PullRightNipple = 3182189734662144L;

			public const long Interactable_PullLeftNipple = 3182191475298304L;

			public const long Interactable_FondleBoobsBra = 3182192997830656L;

			public const long Interactable_MovePullPanties = 3182196890144768L;

			public const long Interactable_SpreadPussy = 3182198572060672L;

			public const long Interactable_EatHerOut = 3182200035872768L;

			public const long Interactable_EatHerAssOut = 3182201667457024L;

			public const long Interactable_MoveBody = 3182203118686208L;

			public const long Shop_bigdaddyhurtshop = 3185695065509888L;

			public const long Shop_ConvenienceStore = 3186228853608448L;

			public const long Shop_Pharmacy = 3186434319978496L;

			public const long Shop_Annalie = 3186567791120384L;

			public const long UI_Hour = 3190461971697664L;

			public const long Minigame_Ready = 3198124537286656L;

			public const long Minigame_Info = 3198278338220032L;

			public const long Minigame_Shift = 3198362492735488L;

			public const long Minigame_Overtime = 3198556772896768L;

			public const long Minigame_Go = 3198696980090880L;

			public const long Minigame_Miss = 3199298212597760L;

			public const long Minigame_Good = 3199320366911488L;

			public const long Minigame_Okay = 3199343792099328L;

			public const long Minigame_Perfect = 3199409651060736L;

			public const long Minigame_TotalScore = 3201999449231360L;

			public const long Minigame_TotalScoreNoMiss = 3202548034834432L;

			public const long Loading_Module = 3204295029559296L;

			public const long Loading_GC = 3204808756301824L;

			public const long Loading_Finished = 3204906265481216L;

			public const long Save_Description = 3209855691001856L;

			public const long UI_ChooseItems = 4247513603117056L;

			public const long UI_ChooseItemsPrompt = 4247654141661184L;

			public const long Shop_OrderFinish = 4261872622505984L;

			public const long Shop_OrderFinishPrompt = 4262224306507776L;

			public const long UI_Finish = 4262458403196928L;

			public const long Shop_NotOrdered = 4262970800345088L;

			public const long UI_PluralDays = 4263186110746624L;

			public const long UI_PluralHours = 4263253978779648L;

			public const long UI_PluralMinutes = 4263318080327680L;

			public const long Shop_Delivery = 4264050888151040L;

			public const long Shop_OrderCollectPrompt = 4264224402313216L;

			public const long Shop_Collect = 4264309144031232L;

			public const long Shop_CanBeCollected = 4264324285468672L;

			public const long Shop_OrderPrompt = 4264592087584768L;

			public const long Shop_CustomOrders = 4272462363496448L;

			public const long Shop_CreateNewOrder = 4272747366453248L;

			public const long bangdotic_Mail = 4275219812192256L;

			public const long bangdotic_News = 4275981124505600L;

			public const long stonksdotic_Title = 4289575077679104L;

			public const long stonksdotic_Sell = 4290444858888192L;

			public const long stonksdotic_Buy = 4290548839878656L;

			public const long stonksdotic_Company = 4290699692216320L;

			public const long stonksdotic_CurrentPrice = 4291006685908992L;

			public const long stonksdotic_StockSummary = 4291269228367872L;

			public const long stonksdotic_NoStocks = 4291489542574080L;

			public const long stonksdotic_Income = 4292488923582464L;

			public const long stonksdotic_Loss = 4292552890912768L;

			public const long stonksdotic_IncomeInfo = 4292699553140736L;

			public const long stocksdotic_Portfolio = 4293268715999232L;

			public const long stocksdotic_ShortInfo = 4293610207842304L;

			public const long stocksdotic_PortfolioEmpty = 4294177059639296L;

			public const long stocksdotic_SellButton = 4296638616965120L;

			public const long stocksdotic_SellAllButton = 4296818456137728L;

			public const long stocksdotic_BuyButton = 4297034282438656L;

			public const long cocktwitchdotic_StartStreamPrompt = 4310433892536320L;

			public const long cocktwitchdotic_Summary = 4310926626787328L;

			public const long cocktwitchdotic_Followers = 4311278495338496L;

			public const long cocktwitchdotic_DaysAgo = 4311379120885760L;

			public const long cocktwitchdotic_LeaderboardEntry = 4311636617596928L;

			public const long cocktwitchdotic_stats = 4312053221036032L;

			public const long cocktwitchdotic_Leaderboard = 4312147097948160L;

			public const long cocktwitchdotic_Home = 4312231097274368L;

			public const long cocktwitchdotic_YourProfile = 4312380057980928L;

			public const long cocktwitchdotic_FollowersGraph = 4312448764874752L;

			public const long casinodotic_Tokens = 4627441351790592L;

			public const long casinodotic_Stake = 4628268514676736L;

			public const long casinodotic_NotEnoughTokens = 4628376622862336L;

			public const long casinodotic_Win = 4628732987707392L;

			public const long casinodotic_BuyTokensPrompt = 4629025448136704L;

			public const long casinodotic_Payout = 4629333205192704L;

			public const long casinodotic_MinimalStake = 4629507973451776L;

			public const long casinodotic_MinimalPayout = 4629699871248384L;

			public const long casinodotic_Single = 4629979501301760L;

			public const long casinodotic_Double = 4630019972141056L;

			public const long casinodotic_Triple = 4630025105969152L;

			public const long casinodotic_ClickToStopWheel = 4630434809778176L;

			public const long casinodotic_ClickToStopColumn = 4630511871725568L;

			public const long casinodotic_Vertical = 4630644147490816L;

			public const long casinodotic_Horizontal = 4630694550441984L;

			public const long casinodotic_Diagonal = 4630732764745728L;

			public const long casinodotic_Won = 4631102643638272L;

			public const long casinodotic_GotBack = 4631196336001024L;

			public const long casinodotic_PlayerBlackjack = 4631424908791808L;

			public const long casinodotic_DoubleBlackjack = 4631509755367424L;

			public const long casinodotic_OverLose = 4631623106433024L;

			public const long casinodotic_OverWin = 4631672779575296L;

			public const long casinodotic_CloserLose = 4631737699012608L;

			public const long casinodotic_Draw = 4631809358696448L;

			public const long casinodotic_CloserWin = 4631841419956224L;

			public const long casinodotic_DealersHand = 4632078955974656L;

			public const long casinodotic_PlayersHand = 4632171352297472L;

			public const long casinodotic_BuyTokens = 4634206017556480L;

			public const long casinodotic_Off = 4634288825700352L;

			public const long casinodotic_BuyMoreTokens = 4635290396135424L;

			public const long casinodotic_Payou = 4635539038670848L;

			public const long casinodotic_Slots = 4635694110478336L;

			public const long casinodotic_Blackjack = 4635781226172416L;

			public const long casinodotic_Book = 4635880111083520L;

			public const long casinodotic_Start = 4645856011489280L;

			public const long casinodotic_BlackjackHit = 4645994750676992L;

			public const long casinodotic_BlackjackStand = 4646092150804480L;

			public const long casinodotic_BlackjackDouble = 4646147507228672L;

			public const long casinodotic_leaf = 4663911118671872L;

			public const long casinodotic_10 = 4664055159459840L;

			public const long casinodotic_jack = 4664176815247360L;

			public const long casinodotic_queen = 4664290736738304L;

			public const long casinodotic_king = 4664427777232896L;

			public const long casinodotic_dollar = 4664512145657856L;

			public const long iamacheater_Button = 4668579106394112L;

			public const long WebsiteNotFound = 4669150752280576L;

			public const long peepeedotic_Text = 4669471675256832L;

			public const long duckdotic_Quack = 4669867277815808L;

			public const long duckdotic_QUACKexclamationexclamationexclamation = 4669977160192000L;

			public const long hotanimebabesdotic_Text = 4670230953332736L;

			public const long adredirectdotic_Text = 4981858383040512L;

			public const long adredirectdotic_Text2 = 4982288534081536L;

			public const long Room_OutsideWithBot = 6830207121887232L;

			public const long Room_OutsideAlone = 6830209105793024L;

			public const long bangdotic_email_Topic = 6834585920737280L;

			public const long bangdotic_email_From = 7201200239677440L;

			public const long bangdotic_email_NoEmailSelected = 7201264261533696L;

			public const long bangdotic_email_NoEmails = 7201601403883520L;

			public const long bangdotic_email_UnreadPostfix = 7201702394335232L;

			public const long Equipment_Own = 7580205748981760L;

			public const long Frick_Restore = 7595071813586944L;

			public const long Frick_RestorePrompt = 7595105242189824L;

			public const long Options_TranslationProgress = 8322437082910720L;

			public const long bangdotic_email_Delete = 9397964988354560L;

			public const long bangdotic_email_DeletePrompt = 9398025864482816L;

			public const long pixelglassesdotic_BuyNow = 9403561913749504L;

			public const long pixelglassesdotic_Discount = 9404716110712832L;

			public const long pixelglassesdotic_NotEnoughMoney = 9405293699928064L;

			public const long pixelglassesdotic_GoAway = 9405459827920896L;

			public const long pixelglassesdotic_StopWastingTime = 9405532750090240L;

			public const long pixelglassesdotic_Dumb = 9405643618127872L;

			public const long pixelglassesdotic_AlreadyOwn = 9405699050049536L;

			public const long pixelglassesdotic_What = 9405799486853120L;

			public const long pixelglassesdotic_WhatPrompt = 9405825323765760L;

			public const long pixelglassesdotic_SoulHarvest = 9405969612017664L;

			public const long pixelglassesdotic_SoulHarvestPrompt = 9406023039062016L;

			public const long pixelglassesdotic_SoulNotTaken = 9406142497034240L;

			public const long pixelglassesdotic_SoulNotTakenPrompt = 9406180866527232L;

			public const long pixelglassesdotic_ThrewOut = 9406311892389888L;

			public const long pixelglassesdotic_SoulTaken = 9406417932783616L;

			public const long pixelglassesdotic_Get = 9406571473670144L;

			public const long Equipment_CannotEquipWithoutSlot = 28870155642368000L;

			public const long Equipment_CannotRemove = 28871745480716288L;

			public const long Slot_LegL = 28872469346283520L;

			public const long Slot_LegR = 28872529521963008L;

			public const long Slot_ShoeL = 28872531589754880L;

			public const long Slot_ShoeR = 28872604117659648L;

			public const long Slot_StockingL = 28876294375370752L;

			public const long Slot_StockingR = 28876223722319872L;

			public const long Slot_ArmL = 28876357243793408L;

			public const long Slot_ArmR = 28876521857642496L;

			public const long Slot_Skin = 28876616858628096L;

			public const long Slot_Eyes = 28876642221584384L;

			public const long Slot_Flesh = 28876771519393792L;

			public const long Slot_Head = 28876866503602176L;

			public const long Slot_Teeth = 28876952067403776L;

			public const long Slot_Hair = 28877006232645632L;

			public const long Slot_Shirt = 28877060272058368L;

			public const long Slot_Skirt = 28877154362880000L;

			public const long Slot_Bra = 28877218342793216L;

			public const long Slot_Panties = 28877297912934400L;

			public const long Slot_HairAccessoryL = 28877364140994560L;

			public const long Slot_Anon_Glasses = 28878144751300608L;

			public const long UI_RentToday = 28884866534502400L;

			public const long UI_RentIn = 28884898440572928L;

			public const long Options_MusicPopup = 29617154138337280L;

			public const long Equipment_CannotChangeOrderWhileSortedPrompt = 29734279280619520L;

			public const long Equipment_SortDescending = 29734444372619264L;

			public const long Equipment_SortAscending = 29734477662810112L;

			public const long Equipment_SortQuality = 29734557237145600L;

			public const long Equipment_SortPrice = 29734614137073664L;

			public const long Equipment_SortSlot = 29734664636493824L;

			public const long Equipment_SortColor = 29734729774034944L;

			public const long Equipment_SortAlphabetically = 29734773965221888L;

			public const long Equipment_SortNoSort = 29734845788483584L;

			public const long UI_GameInstruction = 31429975663841280L;

			public const long Equipment_Can_tEatTitle = 35048148581617664L;

			public const long Equipment_Can_tEatPrompt = 35048187899023360L;

			public const long Equipment_Unequipped = 35049316422651904L;

			public const long Naming_TooShort = 35060771767324672L;

			public const long Naming_TooLong = 35060841048838144L;

			public const long Naming_AreYouSure = 35060892756217856L;

			public const long Options_MachineTranslation = 35136683552673792L;

			public const long Equipment_Remove = 35201171735842816L;

			public const long Frick_PullOutFirst = 36665627488579584L;

			public const long Frick_Request = 36857017535328256L;

			public const long Frick_MusicOn = 37006396653072384L;

			public const long Frick_MusicOff = 37006427472818176L;

			public const long Frick_Blowjob_Nevermind = 37739390984548352L;

			public const long Frick_Blowjob_SwallowCum = 37739555132829696L;

			public const long Frick_Blowjob_StopAfterCumming = 37740242814136320L;

			public const long Frick_Blowjob_ContinueAfterCumming = 37740319083360256L;

			public const long Frick_Blowjob_LetMeCum = 37740430576349184L;

			public const long Frick_Blowjob_DontLetMeCum = 37740477418336256L;

			public const long Frick_Blowjob_Fast = 37740606896500736L;

			public const long Frick_Blowjob_Slow = 37740608968486912L;

			public const long Frick_Blowjob_LickTip = 37740610595876864L;

			public const long Frick_Blowjob_Stop = 37740717030535168L;

			public const long Interactable_ThrustHips = 37740994458578944L;

			public const long Interactable_GrabHead = 37741213023760384L;

			public const long Interact_PlayGame = 38039561060511744L;

			public const long Interact_OldMaid = 38042541298053120L;

			public const long OldMaid_OpponentPicking = 38117090177871872L;

			public const long OldMaid_PlayerPicking = 38117310177505280L;

			public const long OldMaid_RemovePairs = 38117454897770496L;

			public const long OldMaid_Instruction = 38117500364025856L;

			public const long OldMaid_StopPrompt = 38340543652851712L;

			public const long Frick_FlacidFirstTime = 41000833755516928L;

			public const long Frick_StreamDonated = 41001351890472960L;

			public const long Options_KeepRunningWhenMinimzed = 41278844509753344L;

			public const long Blowjob_PreferInside = 41349597364097024L;

			public const long Blowjob_PreferOutside = 41349599742267392L;

			public const long Blowjob_Automatic = 41349591252996096L;

			public const long GameName = 41361575591677952L;

			public const long Slot_EyeShadow = 41709500691779584L;

			public const long Slot_NeckAccessory = 41656432306556928L;

			public const long Slot_Glasses = 41667221952270336L;

			public const long Slot_EyeMakeup = 41709550939541504L;

			public const long ladypartsdotic_PurchaseComplete = 46297168025096192L;

			public const long ladypartsdotic_PurchaseCompletePrompt = 46297305749262336L;

			public const long Loading_FailedProbablyBecauseOfModsPrompt = 46305052108656640L;

			public const long Loading_FailedNoIdeaWhy = 46305053757018112L;

			public const long Loading_IgnoreIfClosedGame = 46345947193413632L;

			public const long Slot_Ears = 46801794386235392L;

			public const long Slot_Mat = 46801805190762496L;

			public const long Slot_Plush = 46801981087289344L;

			public const long Slot_Banner = 46802008576757760L;

			public const long Slot_WallDecoration = 46802026645819392L;

			public const long Equipment_SetLoadedFailureTryingToLoadMod = 47014647600910336L;

			public const long Slot_Septum = 47396080739815424L;

			public const long SAVE_AutoSlot = 76497017957650432L;

			public const long SAVE_AutoSaveSlotEmpty = 76497026111377408L;

			public const long UI_AutoSave = 76498690067271680L;

			public const long Rewards_Money = 77960481725091840L;

			public const long Rewards_Item = 77960487836192768L;

			public const long UI_Reward = 77960632552263680L;

			public const long Requirements_OrGate = 77968724316786688L;

			public const long Requirements_Time = 77969540075360256L;

			public const long UI_Requirements = 77971207130845184L;

			public const long Reward_OnSuccess = 77971499981344768L;

			public const long Reward_OnFailure = 77971552632442880L;

			public const long Rewards_RespectGain = 103325919220731904L;

			public const long Frick_NewDonation = 90675915951837184L;

			public const long Reward_NoReward = 77988725115457536L;

			public const long Contract_OnAcceptBonus = 77989245775384576L;

			public const long UI_Contract = 78005752894984192L;

			public const long Contract_AreYouSureYouWantToAccept = 78005823376068608L;

			public const long Contract_RememberOnlyOne = 78008542123921408L;

			public const long Contract_NoContract = 78013682130227200L;

			public const long Contract_AbandonButton = 78017373298102272L;

			public const long Contract_CompleteButton = 78018006554120192L;

			public const long Contract_AreYouSureYouWantToAbandonCurrent = 78019729460957184L;

			public const long Contract_Failed = 78019823845380096L;

			public const long Contract_CompletionPossible = 78019951792623616L;

			public const long Contract_CompletePromptTitle = 78022746088280064L;

			public const long Contract_CompletePrompt = 78022906289721344L;

			public const long Requirements_TimeLeft = 78029779415994368L;

			public const long Requirements_OutOfTime = 78040863174692864L;

			public const long Requirements_AndGate = 78189863643254784L;

			public const long Requirements_StreamForXMinutesSimple = 78214396286734336L;

			public const long Requirements_StreamForXMinutesCompound = 78214498862632960L;

			public const long Requirements_Done = 78215051860643840L;

			public const long Requirements_AlwaysTrue = 78224114250027008L;

			public const long Requirements_ViewersRequired = 78224465707536384L;

			public const long Requirements_FollowersRequired = 78224468010209280L;

			public const long Requirements_SubsRequired = 78224469469827072L;

			public const long Requirements_InOneStreamCompound = 78261032882933760L;

			public const long Requirements_Viewership = 78291985713991680L;

			public const long Position_Missionary = 78302377395752960L;

			public const long Position_Doggy = 78302451840454656L;

			public const long Position_Blowjob = 78302481456435200L;

			public const long Requirements_Position = 78303109276635136L;

			public const long Contract_CurrentContract = 78335096897015808L;

			public const long Requirement_TimeBetween = 79766729042825216L;

			public const long Requirement_TimesAtLeast = 79824472382509056L;

			public const long Requirements_TimesDone = 79787745735479296L;

			public const long Requirement_TimeBeforeAfter = 79817738016395264L;

			public const long Requirement_TimesExactly = 79824376790126592L;

			public const long Requirement_TimesLess = 79824873425080320L;

			public const long Requirement_TimesAtLeastAndLess = 79825643025338368L;

			public const long Requirement_Or = 79833939971018752L;

			public const long Requirements_InOrderGate = 79841721059250176L;

			public const long Delivery_NumberTitle = 80137031224528896L;

			public const long Delivery_SenderTitle = 80137078762770432L;

			public const long Delivery_ArrivalTimeTitle = 80137116549255168L;

			public const long Delivery_ContentsTitle = 80137169988882432L;

			public const long Delivery_StatusTitle = 80137278709436416L;

			public const long Delivery_DeliveryTypeTitle = 80484645652533248L;

			public const long Delivery_PremiumDelivery = 80485045986267136L;

			public const long Delivery_StandardDelivery = 80485094212374528L;

			public const long Delivery_BuyPremium = 80488503632072704L;

			public const long Delivery_ReportedPrice = 80489256945848320L;

			public const long Delivery_NoDeliveries = 80498249911111680L;

			public const long Deliveries_BuyPremiumPrompt = 80500201034223616L;

			public const long Deliveries_DeliverySentPrompt = 80504339507843072L;

			public const long UI_NarrativeLog = 80847789147164672L;

			public const long NarrativeLog_GameOnFor = 80852450835042304L;

			public const long UI_ItemDescription = 84754642570711040L;

			public const long Mods_ModDescription = 86983942145445888L;

			public const long Mods_ModCreatorPopup = 87031642605883392L;

			public const long Live2DTool_CurrentTool = 88106106160549888L;

			public const long Frick_StreamInfo = 88789582526418944L;

			public const long Frick_ViewersTooltip = 88794441371750400L;

			public const long Frick_FollowersTooltip = 88794615393423360L;

			public const long Frick_SubsTooltip = 88794619596115968L;

			public const long UI_AreYouSureYouWantToQuit = 90576802618937344L;

			public const long UI_ColorSelection_CancelChange = 91708102876729344L;

			public const long Equipment_Modded = 92402373011312640L;

			public const long Equipment_Vanilla = 92402405940793344L;

			public const long Equipment_SearchForItemByName = 93095892050907136L;

			public const long Position_Cowgirl = 93855167043997696L;

			public const long Position_Interaction = 94586810725699584L;

			public const long Frick_LegsRequired = 98489303369125888L;

			public const long Frick_CowgirlRequest_Fast = 98791638976827392L;

			public const long Frick_CowgirlRequest_Slow = 98791757939871744L;

			public const long Frick_CowgirlRequest_Stop = 98791785311899648L;

			public const long Frick_CowgirlRequest_Pullout = 99146374619992064L;

			public const long Frick_CowgirlRequest_Insert = 99146376071221248L;

			public const long Requirement_WhileWearing = 100693490689511424L;

			public const long Requirement_ItemIncludedInOnAcceptBonus = 100693834530164736L;

			public const long Equipment_Contract = 100713655892316160L;

			public const long Equipment_ItemIsInCurrentContract = 100715059465818112L;

			public const long cocktracts_header_home = 100726093731729408L;

			public const long cocktracts_header_available = 100727667761745920L;

			public const long cocktracts_header_history = 100727703828566016L;

			public const long cocktracts_NoAvailableContracts = 102504297911947264L;

			public const long cocktracts_NoHistoryContracts = 102504301103812608L;

			public const long cocktracts_Main_Summary = 102510519830581248L;

			public const long Mods_OpenModFolder = 103219101073018880L;

			public const long Equipment_MissingItem = 103223837037826048L;

			public const long Equipment_MissingItemUnknownMod = 103653687980593152L;

			public const long Equipment_MissingItemKnownMod = 103653268436946944L;

			public const long Equipment_MissingItemVanilla = 103653695874273280L;

			public const long Rewards_RespectLoss = 103326065513861120L;

			public const long Requirement_OnlyProvidedItemCounts = 103327646636445696L;

			public const long Requirement_AnyItemCounts = 103328220698251264L;

			public const long Requirement_OnlyCertainItemWorks = 103329181135147008L;

			public const long Requirement_ColorsMustMatch = 103329570618216448L;

			public const long Contract_Partners = 103597203649335296L;

			public const long cocktracts_header_partners = 103997420844486656L;

			public const long Rewards_RespectBasicallyNone = 104348586640019456L;

			public const long Rewards_RespectTinyAmount = 104348686762250240L;

			public const long Rewards_RespectNormalAmount = 104348751757185024L;

			public const long Rewards_RespectDecentAmount = 104348877410144256L;

			public const long Rewards_RespectBigAmount = 104348879599570944L;

			public const long Rewards_RespectHugeAmount = 104348883156340736L;

			public const long Rewards_RespectGiganticAmount = 104348952320413696L;

			public const long cocktracts_Partner_ContractData = 104682370048593920L;

			public const long cocktracts_Partner_NotUnlocked = 104683857613033472L;

			public const long Contract_GenericTitle = 105066058041135104L;

			public const long Contract_GenericNarration = 105067472167505920L;

			public const long Contact_HasOtherRewardShort = 105070972372914176L;

			public const long Contract_NoRewards = 105072268601593856L;

			public const long Contract_RewardShort = 105073804811571200L;

			public const long cocktracts_NewContract = 105817092137029632L;

			public const long cocktracts_CurrentlyAvailableContracts = 105848347574538240L;

			public const long Contracts_GoldContract = 119378088885620736L;

			public const long WebGLWarningMightLoseSavePrompt = 107250247087820800L;

			public const long UI_DontShowTheWarningAgain = 107251903930175488L;

			public const long Options_DeviceDetected = 110884429420695552L;

			public const long Options_DriverDetected = 110884559582531584L;

			public const long Options_DriverNotDetected = 110884689845030912L;

			public const long Options_Connect = 110884765585772544L;

			public const long Options_Calibrate = 110884795004620800L;

			public const long Options_Connecting = 110884842836463616L;

			public const long Options_Calibrating = 110885184496078848L;

			public const long Options_WaitingForDriver = 110885405749809152L;

			public const long Options_CalibrationPrompt = 110886562241056768L;

			public const long Options_CalibrationAreYouSure = 110886826775810048L;

			public const long Options_CalibrationValues = 110887030639955968L;

			public const long Options_CalibrationComplete = 110887155391139840L;

			public const long Options_TryingToConnect = 110887212823744512L;

			public const long Rewards_Tokens = 111323388428816384L;

			public const long cocktracts_AcceptContract = 111323846547476480L;

			public const long cocktracts_RejectContract = 111511411909070848L;

			public const long Contract_AreYouSureYouWantToReject = 111514635026173952L;

			public const long Contract_ThisWillAngerThePartner = 111514776562962432L;

			public const long Contract_AvailableContract = 111516167872319488L;

			public const long Contract_AcceptedContract = 111533608618823680L;

			public const long Contract_ExpiredContract = 111533613798789120L;

			public const long Contract_RejectedContract = 111533614545375232L;

			public const long Contract_CompletedContract = 111533615140966400L;

			public const long Contract_FailedContract = 111534575334592512L;

			public const long Contract_AbandonedContract = 111534577532407808L;

			public const long Contract_AcceptedAt = 111535180773986304L;

			public const long Contract_FailedAt = 111536244386566144L;

			public const long Contract_CompletedAt = 111536198479908864L;

			public const long Contract_AbandonedAt = 111536288699387904L;

			public const long cocktracts_NoRelatedContracts = 111665695493591040L;

			public const long cocktracts_AbandonContract = 111684337090301952L;

			public const long cocktracts_CompleteContract = 111684421555195904L;

			public const long Slot_LowerBody = 112797584455503872L;

			public const long Slot_UpperBodyOuter = 112799150298238976L;

			public const long Slot_UpperBody = 112801253397762048L;

			public const long Interactable_MoveHoodie = 113409013214744576L;

			public const long Interactable_RemovePutOnPants = 113472541338505216L;

			public const long OpeningFolderNotSupportedOnThisPlatform = 115600670286811136L;

			public const long PathCopiedToClipboard = 115602100355731456L;

			public const long Mods_RelatedModNotFound = 115604257586941952L;

			public const long UI_ChangeColor = 115604514160906240L;

			public const long Mods_OpenModWorkingDirectory = 115604614488657920L;

			public const long Mods_ReloadTextures = 115605370902024192L;

			public const long Frick_CowgirlRequest_BotLoad = 117175207793135616L;

			public const long AddItem = 118484088477585408L;

			public const long LinkPrompt = 119007219394940928L;

			public const long CheckItOut = 119007846246256640L;

			public const long Requirement_WhileWearingMerch = 119322262330281984L;

			public const long Requirement_YouDontHaveAnyMerch = 119322904511778816L;

			public const long Contracts_GoldContractTooltip = 119378233735909376L;

			public const long Contracts_TimeLeftToAccept = 119616393732558848L;

			public const long UI_LessThanAnHour = 119617920362745856L;

			public const long Contract_FailedPrompt = 119771359088611328L;

			public const long ReloadedAllTexturesPrompt = 123949974063812608L;

			public const long Frick_LegsAndArmsRequired = 121127165742682112L;

			public const long NunRepair_NoValidItems = 121783962669289472L;

			public const long NunRepair_Repair = 121814217182285824L;

			public const long NunRepair_MaxQuality = 121815228538032128L;

			public const long NunRepair_RepairPossible = 121815230467411968L;

			public const long NunRepair_AlreadyRepaired = 121817020021739520L;

			public const long NunRepair_RepairPrompt = 121820352937332736L;

			public const long Cheat_ResetChurch = 122323925517340672L;

			public const long Cheat_ResetChurchPrompt = 122324301981290496L;

			public const long Cheat_ResetChurchDone = 122324738528645120L;

			public const long Mods_Preparing = 122446762513141760L;

			public const long Mods_Starting = 122446862475988992L;

			public const long Equipment_SetEquippingIterationLimitReached = 122896743783075840L;

			public const long cocktracts_header_help = 123602581472546816L;

			public const long cocktracts_help_description = 123607893294321664L;

			public const long cocktracts_header_description = 123612752278065152L;

			public const long NoFileAccessPrompt = 131190198519074816L;

			public const long NoFileAccessPrompt2 = 131193600770707456L;

			public const long Mods_Status_ActiveButGameOutdated = 131208725485457408L;

			public const long Mods_Status_ActiveButGameOutdatedDescription = 131208764622508032L;

			public const long Options_NoToy = 142777605022420992L;

			public const long Options_Other = 142783772649984000L;

			public const long Options_ToyLabel = 142787469446615040L;

			public const long Options_ToySettings = 142807620820799488L;

			public const long Options_ListOfSupportedToys = 142813080831528960L;

			public const long Options_CurrentToy = 142813670638751744L;

			public const long Toy_AGui = 142816138198126592L;

			public const long Options_DisconnectToy = 142823013287333888L;

			public const long Options_AlreadyConnectedPrompt = 142848208458878976L;

			public const long Options_CalibrationPrompt2 = 142853923147747328L;

			public const long Options_ToyCurrentInsertion = 142921688344825856L;

			public const long Options_ToySettingsTooltip = 143124457693515776L;

			public const long UI_ChangeQuality = 148476157488926720L;

			public const long RecentSave = 161906883278278656L;

			public const long Shop_TryOn = 179782432629592064L;

			public const long ChangingRoom = 179879880014303232L;

			public const long ReturnChangingRoomItem = 180108504285974528L;

			public const long ReturnChangingRoomItem_Prompt = 180111010378420224L;

			public const long ItemBorrowedFromAnnalieDescription = 180123564106452992L;

			public const long ItemBorrowedShort = 180124503123038208L;

			public const long ItemsReturnedPrompt = 180168777147486208L;

			public const long TryItemPrompt = 180177057936707584L;

			public const long ReturnToDefault = 181229886776414208L;

			public const long ReturnToDefaultPrompt = 181230290838884352L;

			public const long Undress = 181265240959094784L;

			public const long UndressPrompt = 181265560342761472L;

			public const long Unpack = 181276072157777920L;

			public const long UnpackPrompt = 181276113471672320L;

			public const long Options_CalibrationWrongPrompt = 182153022174793728L;

			public const long Options_NeedsCalibration = 182155339737468928L;

			public const long Slot_HeartPupils = 182945513559953408L;

			public const long DebugModePrompt = 183616489851326464L;

			public const long DebugModeEnabledPrompt = 183631250118807552L;

			public const long Options_DisableDebugButton = 183633597637201920L;

			public const long Options_DisableDebugPrompt = 183634906121637888L;

			public const long Achievements_EstimatedTimePlayed = 183672075477454848L;

			public const long ThankYou = 183678134879391744L;

			public const long ShillingPrompt = 183678261178273792L;

			public const long ClickToOpen = 183687878033674240L;

			public const long Interact_Action = 185142908448280576L;

			public const long Cuddle_GetCloser = 185154362496913408L;

			public const long Cuddle_MoveAway = 185154407791202304L;

			public const long Slot_Tail = 186677922902183936L;

			public const long bangdotic_DeleteSpam = 187383148432986112L;

			public const long bangdotic_DeleteSpamPrompt = 187383311713046528L;

			public const long Live2DTool_TabTip = 187424961176932352L;

			public const long Interactable_MoveTail = 189924979553210368L;

			public const long PlayerName = 193471983856259072L;

			public const long PlayerName_DefaultMaster = 193471986729357312L;

			public const long PlayerName_Prompt = 193471985475260416L;

			public const long BotName = 193485218361774080L;

			public const long BotName_Prompt = 193485220102410240L;

			public const long BotName_Default = 193485221486530560L;

			public const long Interact_Other = 193514289871327232L;

			public const long ChangeBotName = 193515065645600768L;

			public const long ChangePlayerName = 193515119747928064L;

			public const long Frick_CowgirlRequest_BegStop = 203832649683177472L;

			public const long Interactable_MoveBlanket = 205536974418731008L;

			public const long Interactable_RubHerFace = 205536977090502656L;

			public const long Interactable_HoldHerHand = 205537104119193600L;

			public const long Cuddle_GoToSleep = 205538471843979264L;

			public const long Cuddle_AskToStopHeadpats = 205545741474586624L;

			public const long Cuddle_EndPrompt = 207592949275418624L;

			public const long Slot_Lips = 207911663787040768L;

			public const long Equipment_FromMod = 207959451258314752L;

			public const long Slot_PersonalityModule = 208385084266373120L;

			public const long Equipment_PersonalityModuleInfo = 208756289724997632L;

			public const long Equipment_PersonalityModules = 210961474161467392L;

			public const long Equipment_HighQuality = 210961814600540160L;

			public const long Equipment_Under20k = 210962206369505280L;

			public const long Cuddle_AskForHeadpats = 214844375504510976L;

			public const long Cuddle_AskToSaySomethingNice = 214855388001984512L;

			public const long Options_DeviceNotDetected = 214897433626271744L;

			public const long Options_WaitingForDevice = 110885475282980864L;

			public const long Mods_ApproximateExtractedSize = 216000683855306752L;

			public const long Mods_MajorVersionBehind = 216003925590925312L;

			public const long Mods_MinorVersionBehind = 216004093535051776L;

			public const long Mods_PatchVersionBehind = 216004097012129792L;

			public const long Mods_MajorVersionAhead = 216004209444642816L;

			public const long Mods_MinorVersionAhead = 216004239974981632L;

			public const long Mods_PatchVersionAhead = 216004242319597568L;

			public const long Mods_ExactlySameVersion = 216005188114178048L;

			public const long Mods_UpdateYourGame = 216005754861117440L;

			public const long New_Entry_1 = 216018123439988736L;

			public const long OutdatedVersionPrompt = 217497412949762048L;

			public const long DownloadLatestPublicItchIo = 217497671214030848L;

			public const long Equipment_Medicine = 217577985613488128L;

			public const long Error_GameCannotSave = 218218778581786624L;

			public const long AGUI_UseDriverDepth = 233121712609038336L;

			public const long AGUI_UseDriverDepth_Tooltip = 233121787800326144L;

			public const long SecretRoomPrompt = 243536364995338240L;

			public const long Room_Cook = 246153693377847296L;

			public const long Interactable_MoveLegs = 252905369350692864L;

			public const long Cooking_SelectedSong = 263867424090087424L;

			public const long Cooking_Results = 263867923883352064L;

			public const long FishingGear = 268938463266463744L;

			public const long FishingProgress = 268938574251941888L;

			public const long FishingFishingRods = 268942624322215936L;

			public const long FishingResults = 269257882794450944L;

			public const long Fishing = 269289693771718656L;

			public const long Fishing_StartFishing = 269511379708272640L;

			public const long Fishing_FishingSpot_Cleanliness = 269526879226028032L;

			public const long Fishing_FishingSpot_FishQuantity = 269526934053969920L;

			public const long Fishing_FishingSpot_FishSize = 269526988798025728L;

			public const long Fishing_FishingLocation = 269533393185660928L;

			public const long SelectItem = 269542812636143616L;

			public const long NoValidItemsFound = 269564918052675584L;

			public const long FishingFishingRod = 269569491827941376L;

			public const long FishingBait = 269572895136538624L;

			public const long FishingNoFishinRodEquipped = 269573447031447552L;

			public const long FishingNoBaitEquipped = 269573606360473600L;

			public const long UI_Change = 269575506841559040L;

			public const long FishingNoFishingRodEquippedPrompt = 273571584536469504L;

			public const long Fishing_FishingSpot_Price = 273929969140228096L;

			public const long CookingSong_BPM = 273953207970947072L;

			public const long CookingSong_Difficulty = 273953175725137920L;

			public const long CookingSong_Length = 273953146817994752L;

			public const long CookingSong_MinBPM = 273955578268606464L;

			public const long CookingSong_MaxBPM = 273955582001537024L;

			public const long CookingSong_MissingIngredient = 274253650940911616L;

			public const long Fishing_TimeEndsAt = 276192940222570496L;

			public const long Fishing_TimeEnd = 276193668341161984L;

			public const long Rewards_ItemDelivery = 276482683070046208L;

			public const long Fishing_FishingResults = 276485746140897280L;

			public const long Fishing_FishingResultsNothing = 276485998394728448L;

			public const long Interactable_Fingering = 277099352406614016L;

			public const long Interactable_ThumbInMouth = 277130913491443712L;

			public const long Interactable_PlayBoobL = 279351619797114880L;

			public const long Interactable_PlayBoobR = 279351690819264512L;

			public const long Interactable_ClitRub = 280796031672639488L;

			public const long Cooking_Input = 281130209593253888L;

			public const long Cooking_CookingVisualizationTitle = 281132009436209152L;

			public const long Interactable_GrabAss = 281169972673110016L;

			public const long Interactable_GrabHea = 281170013890535424L;

			public const long Interactable_MoveDress = 283365635473268736L;

			public const long FishingJournal = 285858811819843584L;

			public const long Fishing_CaughtXTimes = 285884661038571520L;

			public const long Fishing_MinWeight = 285884766244298752L;

			public const long Fishing_MaxWeight = 285884767963963392L;

			public const long Fishing_NeverCaught = 285884902534012928L;

			public const long Fishing_LastCaught = 285884960692232192L;

			public const long Fishing_BiggerThanPercent = 285900715160756224L;

			public const long Fishing_SmallerThanPercent = 285900763114233856L;

			public const long FishItemWeightItemInfo = 286219092781686784L;

			public const long FishSize_10Percentile = 286219584098263040L;

			public const long FishSize_20Percentile = 286223013008891904L;

			public const long FishSize_30Percentile = 286223014862774272L;

			public const long FishSize_40Percentile = 286223015605166080L;

			public const long FishSize_60Percentile = 286223017555517440L;

			public const long FishSize_70Percentile = 286223019375845376L;

			public const long FishSize_80Percentile = 286223081359269888L;

			public const long FishSize_90Percentile = 286223083179597824L;

			public const long FishSize_100Percentile = 286223106114052096L;

			public const long Item_MarkAsFavourite = 286958548862099456L;

			public const long Item_UnmarkAsFavourite = 286958671264473088L;

			public const long Item_Favourite = 286958768836567040L;

			public const long Cooking_ScoreLabel = 287281353327632384L;

			public const long Cooking_AccuracyLabel = 287349996405121024L;

			public const long FishingWebsite_TipsFromThePros = 288460403148578816L;

			public const long FishingWebsite_Name = 328038276087668736L;

			public const long FishingWebsite_Tip1 = 288779861457100800L;

			public const long FishingWebsite_NoTips = 288780499117137920L;

			public const long MillilitersDisplay = 291360229049753600L;

			public const long CommandArgumentsExpectationInfo = 291364104926388224L;

			public const long ProvidedArgumentsCountError = 291364817899347968L;

			public const long ValueNotFound = 291368438418366464L;

			public const long ClosestMatch = 291368638771879936L;

			public const long ClsCommandDescription = 291674740382228480L;

			public const long CowsayCommandDescription = 291674841934716928L;

			public const long HelpCommandDescription = 291675064895528960L;

			public const long SummaryCommandDescription = 291675429393129472L;

			public const long ViewVariableCommandDescription = 291675631189483520L;

			public const long ExitCommandDescription = 291676145096581120L;

			public const long SummaryCommandName = 291676700569231360L;

			public const long SummaryCommandMasterName = 291676739815333888L;

			public const long SummaryCommandLust = 291676809012961280L;

			public const long SummaryCommandSympathy = 291676887047987200L;

			public const long SummaryCommandTimeSinceLastHeadpat = 291676985823846400L;

			public const long HelpCommandDetailedInfoNotProvided = 291677830095298560L;

			public const long CommandNotFound = 291678442090389504L;

			public const long HelpCommandDetailedInfoHeader = 291678804859936768L;

			public const long CurrentDateInfo = 291685365296652288L;

			public const long DateCommandDescription = 291686735630295040L;

			public const long CurrentDayOfTheWeek = 291691209128140800L;

			public const long CowsayJunFlagDescription = 291707417571352576L;

			public const long CowsayAnnalieFlagDescription = 291707485191921664L;

			public const long CowsayGayFlagDescription = 291707563273084928L;

			public const long DateCommandDayOfWeekFlagDescription = 291708251470290944L;

			public const long CommandFlagsNone = 291710365869268992L;

			public const long CommandFlagsList = 291710458894737408L;

			public const long HelpCommandCommandNameHeader = 291712261178777600L;

			public const long HelpCommandDescriptionHeader = 291712367357583360L;

			public const long CommandInputNothing = 291714070983204864L;

			public const long StatusAppDisconnect = 291714617454878720L;

			public const long CommandNotAWholeNumber = 291763400070914048L;

			public const long CommandNotWithinRange = 291763485726990336L;

			public const long SnakeCommandDescription = 291766193762590720L;

			public const long SnakeCommandHeader = 291786284394864640L;

			public const long SnakeCommandScore = 291786780740411392L;

			public const long CubeCommandInputGradient = 292070395239981056L;

			public const long CubeCommandGradientCustom = 292071315659022336L;

			public const long CubeCommandDescription = 292081865256329216L;

			public const long StyleCommandDescription = 292088042878918656L;

			public const long StyleCommandHeader = 292088106003193856L;

			public const long StyleCommandInfo = 292088243299540992L;

			public const long StyleCommandNextTextColor = 292090165658116096L;

			public const long StyleCommandNextBackgroundColor = 292090204983910400L;

			public const long CommandClosed = 292098556631703552L;

			public const long StyleCommandDefaultColors = 292099808820191232L;

			public const long StyleCommandSaving = 292103942780739584L;

			public const long StyleCommandSavingDone = 292104049936818176L;

			public const long StyleCommandRandomColors = 292112160504406016L;

			public const long EightBallCommandYes = 292117632015917056L;

			public const long EightBallCommandNo = 292117690597761024L;

			public const long EightBallCommandMaybe = 292117708234809344L;

			public const long EightBallCommandDefinitely = 292117767928143872L;

			public const long EightBallCommandIDontThinkSo = 292117842712584192L;

			public const long EightBallCommandAbsolutely = 292117876732583936L;

			public const long EightBallCommandNotLikely = 292117950413922304L;

			public const long EightBallCommandTryAgainLater = 292118014037319680L;

			public const long EightBallCommandDescription = 292119612482379776L;

			public const long EightBallCommandText = 292119713296670720L;

			public const long CurrentlyLoading = 292459566337089536L;

			public const long LogExportSuccessful = 292497579461042176L;

			public const long LogExportFailed = 292497790078017536L;

			public const long ErrorOccured = 294207689531056128L;

			public const long PlayVideoLoading = 294219412971446272L;

			public const long PlayVideoLoadingCancelled = 294219519695511552L;

			public const long PlayVideoCurrentGradient = 294219566034182144L;

			public const long PlayVideoVideoSize = 294219611206836224L;

			public const long PlayVideoTurnOnLooping = 294219798847414272L;

			public const long PlayVideoTurnOffLooping = 294219802634870784L;

			public const long PlayVideoSkip = 294220167136665600L;

			public const long PlayVideoDescription = 294220377594257408L;

			public const long EasterEgg = 294220554702938112L;

			public const long PlayVideoInvalidPath = 294220909868212224L;

			public const long ExpensiveItemModifier = 294310927785779200L;

			public const long ExpensiveItemModifierDescription = 294311088398262272L;

			public const long PopularItemModifier = 294601542884909056L;

			public const long PopularItemModifierDescription = 294601693305233408L;

			public const long StreamersItemModifier = 294607100912328704L;

			public const long StreamersItemModifierDescription = 294607140934377472L;

			public const long ItemModifierInfo = 294625452372254720L;

			public const long ItemModifierInfoDescription = 294625865280512000L;

			public const long ItemModifierLevel = 294627986285518848L;

			public const long ItemModifierListHeader = 294630602709131264L;

			public const long ItemModifierCursed = 294641044772872192L;

			public const long ItemModifierCursedDescription = 294641082420944896L;

			public const long ItemModifierFastFishing = 294830347943665664L;

			public const long ItemModifierFastFishingDescription = 294830370198642688L;

			public const long ItemModifierWideFishing = 294830600080056320L;

			public const long ItemModifierWideFishingDescription = 294830603439693824L;

			public const long ClickAnywhereToStartFishing = 294878476382494720L;

			public const long ThrowMinigameExplain = 294889799619174400L;

			public const long CookingSong_MissingIngredients = 294910130442461184L;

			public const long CookingSong_ReadyToCook = 294911354013212672L;

			public const long CookingSong_IngredientList = 294911821199958016L;

			public const long CookingSong_ModifierEffects = 294915607926661120L;

			public const long CookingSong_MoreScore = 294915737463545856L;

			public const long CookingSong_LessScore = 294915738977689600L;

			public const long CookingSong_FasterSong = 294915915000045568L;

			public const long CookingSong_SlowerSong = 294915916690350080L;

			public const long CookingSong_ModifierEffectsNone = 294917261694590976L;

			public const long CookingSong_Modifiers = 294918534284173312L;

			public const long CookingSong_ModifierSlowMode = 294918660834713600L;

			public const long CookingSong_ModifierFastMode = 294918688034775040L;

			public const long CookingSong_ModifierUltraFastMode = 294918750383104000L;

			public const long CookingSong_ModifierInsaneMode = 294918789377548288L;

			public const long CookingSong_NoModifiers = 294918963223060480L;

			public const long CookingSong_NoActiveModifiers = 294919766151258112L;

			public const long CookingSong_ActiveModifiers = 294919900985548800L;

			public const long CookingSong_ModifierPractice = 294923164196003840L;

			public const long CookingSong_ModifierPracticeInfo = 294924161282727936L;

			public const long CookingSong_DeactiveModifier = 294924838369222656L;

			public const long CookingSong_Play = 294925626919346176L;

			public const long CookingSong_PracticePlayPrompt = 294925901008723968L;

			public const long CookingMinigame_Great = 296007040863559680L;

			public const long CookingMinigame_Good = 296007088561184768L;

			public const long CookingMinigame_Miss = 296007090285043712L;

			public const long CookingMinigame_MaxCombo = 296007091706912768L;

			public const long CookingMinigame_Score = 296007092784848896L;

			public const long CookingMinigame_Accuracy = 296007245277159424L;

			public const long CookingMinigame_Bad = 296007651256426496L;

			public const long CookingMinigame_CookedDish = 296032299150991360L;

			public const long CookingMinigame_NewRecord = 296035465368166400L;

			public const long CookingMinigame_Incomplete = 296036171634434048L;

			public const long CookingMinigame_LostIngredients = 296037921447731200L;

			public const long CookingMinigame_ModifierPracticeInfoResults = 296038257164017664L;

			public const long CookingMinigame_Results = 296038954961985536L;

			public const long CookingMinigame_TimesPlayed = 296040594465087488L;

			public const long CookingMinigame_TimesFinished = 296040645044199424L;

			public const long CookingMinigame_BestScore = 296040681379454976L;

			public const long CookingMinigame_BestAccuracy = 296040730045964288L;

			public const long ItemName = 296065819206475776L;

			public const long ItemDescription = 296065843676045312L;

			public const long ItemDescription_Ingredients = 327250349971726336L;

			public const long CookingMinigame_RecipeExecutionMultipleItems = 296457857605296128L;

			public const long CookingMinigame_ItemSelectedForTheRecipeExecution = 296458707115433984L;

			public const long Items_SelectedItems = 296808590854840320L;

			public const long Items_NoItemsSelected = 296815914730381312L;

			public const long UI_Continue = 296817701986230272L;

			public const long ItemsMissing = 297140014199857152L;

			public const long UnselectOthers = 297142663217405952L;

			public const long ItemSelectionCantSelect = 297151815499112448L;

			public const long CurrentSelection = 297154642904604672L;

			public const long SelectNoneShort = 297156319145615360L;

			public const long SelectAllShort = 297156343434829824L;

			public const long RecipeChoosingItemsNoValidItemsFoundPrompt = 297189572783104000L;

			public const long Equipment_ButtonEat = 297212801987895296L;

			public const long FoodSatiation_Label = 297481849556426752L;

			public const long CookingMinigame_Grade = 298962322543599616L;

			public const long ShillingRatingPrompt = 298999654550331392L;

			public const long RateOnX = 299000698021863424L;

			public const long ClsResult = 299705356344352768L;

			public const long Shop_RichPeopleShopName = 300063969743200256L;

			public const long ColorPalletes = 302589938785837056L;

			public const long Colors = 302590454408404992L;

			public const long CurrentColor = 302627257874112512L;

			public const long YourColorPresets = 302635637791514624L;

			public const long ColorPicker = 302651357866745856L;

			public const long ColorPickerItemPrompt = 302651419346853888L;

			public const long Equipment_SetModGood = 306475541545869312L;

			public const long Equipment_SetModUnknown = 306475787847983104L;

			public const long Equipment_SetModMissing = 306475705794813952L;

			public const long Equipment_SetUsedMods = 306476096737501184L;

			public const long Equipment_SetUsedModsStatus = 306483346902343680L;

			public const long Equipment_CannotEquipItemsToRemove = 306568702104428544L;

			public const long Equipment_CannotEquipPopupTitle = 306560179287089152L;

			public const long Equipment_CannotEquipPromptStart = 306560360392941568L;

			public const long Equipment_CannotEquipPromptEnd = 306560550168420352L;

			public const long LowMemoryHeader = 310933611869102080L;

			public const long LowMemoryToggle = 310933790127022080L;

			public const long LowMemoryDescription = 310934031433719808L;

			public const long LowMemoryTextureScale = 310934948295991296L;

			public const long LowMemoryTextureScaleOptionsInfo = 310941797204353024L;

			public const long OptionsRestartRequired = 310950382307106816L;

			public const long WebGLMobileWarning = 310968355528892416L;

			public const long Slot_Hologram = 311768631944929280L;

			public const long Equipment_ButtonModify = 313883178461814784L;

			public const long VariableBoolEnableContextMenu = 313887217803059200L;

			public const long VariableBoolDisableContextMenu = 313887667524722688L;

			public const long Equipment_Hair = 313891150017585152L;

			public const long SpecialItemVariableEnabled = 313892573551779840L;

			public const long SpecialItemVariableDisabled = 313892612399423488L;

			public const long EquipmentCommandDescription = 314987481612148736L;

			public const long EquipmentCommand_ItemMissing = 314989713090928640L;

			public const long Selected = 316258147070107648L;

			public const long NotSelected = 316258203424776192L;

			public const long SelectAction = 316259970090786816L;

			public const long UnselectAction = 316259971604930560L;

			public const long SelectionTruncatedSummaryEnd = 316261124338081792L;

			public const long UnselectAllAction = 316601446230384640L;

			public const long SelectAllAction = 316607361348263936L;

			public const long SelectItemsForANewOrderPrompt = 316612122302734336L;

			public const long IngredientSelectionTitle = 316612373772230656L;

			public const long ChooseEquipmentPrompt = 316612621794009088L;

			public const long PriceVariesByQuality = 316719705944481792L;

			public const long TargetSlot = 316720103287676928L;

			public const long IngredientSelectionPrompt = 317096312936849408L;

			public const long EquiupmentCommandDescription = 319255852818096128L;

			public const long RentDue = 319260558508417024L;

			public const long CockTwitch_AccountInactive = 319328371549921280L;

			public const long CockTwitch_CantStartStreamAccountInactive = 319328406836600832L;

			public const long Blog_AccountDeleted = 319700772192247808L;

			public const long Blog_CommentsHeader = 319703659744059392L;

			public const long Blog_WrittenBy = 319705146469638144L;

			public const long Blog_Topic = 319705191344496640L;

			public const long Blog_Progress = 319900903973838848L;

			public const long ClickHere = 319911777543380992L;

			public const long Blog_Username = 319926690701631488L;

			public const long Blog_Password = 319926789292941312L;

			public const long Blog_ClickToLogin = 319935753967624192L;

			public const long Blog_LoggedInAs = 322492133576531968L;

			public const long Blog_NotLoggedIn = 322492181681004544L;

			public const long Blog_LoggedOutForYourSafety = 322492225586978816L;

			public const long Blog_AccountBanned = 322534704394788864L;

			public const long Blog_ChiliRecipe1 = 322557654602866688L;

			public const long Blog_ChiliRecipe2 = 322557686781566976L;

			public const long Blog_ChiliRecipe3 = 322562073406988288L;

			public const long Blog_RecipesFromMembers = 322564471848427520L;

			public const long Blog_Logout = 322873137319370752L;

			public const long Blog_BlogLinkEmailInsertHyperlinkText = 322906135402110976L;

			public const long StatusApp_BasicModules = 323965704895193088L;

			public const long StatusApp_ApproximateCondition = 323966017257594880L;

			public const long StatusApp_OtherModules = 323966295016988672L;

			public const long StatusApp_NoOtherModulesFound = 323967460999946240L;

			public const long StatusApp_CompatiblityCheck = 323967509062475776L;

			public const long StatusApp_NoCompatiblityIssuesFound = 323967725501145088L;

			public const long StatusApp_FullConnetionEstablished = 323967880124162048L;

			public const long StatusApp_ConsoleWidth = 323968019110813696L;

			public const long StatusApp_BeginningHardwareCheck = 323968164145651712L;

			public const long StatusApp_ConnectingTo = 323968599883505664L;

			public const long StatusApp_ConnectedTo = 323968861058621440L;

			public const long StatusApp_HelloMessage = 323969204228186112L;

			public const long StatusApp_Ram = 323972012495069184L;

			public const long StatusApp_CPU = 323972033739218944L;

			public const long StatusApp_BIOS = 323972070204497920L;

			public const long StatusApp_BiosModified = 323972141620912128L;

			public const long StatusApp_Memory = 323972443522719744L;

			public const long StatusApp_System = 323977953940594688L;

			public const long StatusApp_PcIconName = 323990393575170048L;

			public const long itemAddedToYourInventory = 323998529409245184L;

			public const long Equipment_HairSection = 324422569844269056L;

			public const long HairSettings_Header = 324666248097947648L;

			public const long HairSettings_Help = 324668049937391616L;

			public const long HairSetting_HelpDescription1 = 324679602858352640L;

			public const long Slot_HairAdditional = 324680820343169024L;

			public const long HairSetting_HelpDescription2 = 324681566241415168L;

			public const long HairSetting_HelpDescription3 = 324682385728724992L;

			public const long HairSetting_HelpDescription4 = 324684559770705920L;

			public const long Equipment_ColorSection = 324704662713262080L;

			public const long CantChangeColor = 324724537720561664L;

			public const long Folder_Hypercamo = 324742261779718144L;

			public const long Folder_NotHypercamo = 324742303357853696L;

			public const long ColorsSection_PickFromPresets = 324742806699499520L;

			public const long Slot_HairAugmentation = 325046368583278592L;

			public const long Hair_Ahoge = 326496452730675200L;

			public const long Hair_Front = 326496470711656448L;

			public const long Hair_Side = 326496727755382784L;

			public const long Hair_Back = 326496729714122752L;

			public const long Hair_Braid = 326497075953917952L;

			public const long Sort_ByDifficulty = 326949855952887808L;

			public const long CookingSongList_Header = 326951340845555712L;

			public const long CookingMinigame_WrongKey = 326955687113383936L;

			public const long Sort_AlphabeticallyBySongName = 327230647882997760L;

			public const long CookingMinigame_FullCombo = 327240905971982336L;

			public const long DishQuality_Header = 327244749757997056L;

			public const long ItemDescription_NutritionalValues = 327252742549528576L;

			public const long FoodSatiation_Satiation = 327253747311828992L;

			public const long FoodEffect_MaxCumIncrease = 327254785859248128L;

			public const long FoodEffect_MaxCumDecrease = 327257149269860352L;

			public const long FoodEffect_StaminaRecovery = 327257721863659520L;

			public const long FoodEffect_StaminaLoss = 327257724082446336L;

			public const long FoodEffect_CurrentCumIncrease = 327258303588458496L;

			public const long FoodEffect_CurrentCumDecrease = 327258305442340864L;

			public const long FoodEffect_MentalHealthDecrease = 327259150795923456L;

			public const long FoodEffect_WarningPrompt = 327259479696465920L;

			public const long CookingSong_PlayAuto = 327275935859466240L;

			public const long CookingSong_PlayAutoPrompt = 327276190738931712L;

			public const long CookingSong_IngredientFishAtLeastXkg = 327605476918288384L;

			public const long CookingSong_IngredientOptional = 327631757932224512L;

			public const long CookingSong_LockedDIsh = 327634950372188160L;

			public const long CookingSong_NewDish = 327636876157509632L;

			public const long CookingSong_BestGrade = 327637086371831808L;

			public const long Folder_CookingIngredients = 327937932930310144L;

			public const long FoodEffect_MentalHealthIncreaseSmall = 327958095562006528L;

			public const long FoodEffect_MentalHealthIncreaseBig = 327958423577550848L;

			public const long FoodEffect_MentalHealthIncreaseModerate = 327958669204381696L;

			public const long FoodEffect_YouNeedToCookThis = 327961407338307584L;

			public const long Cooking_SpentTime = 327979375921192960L;

			public const long Ingredient_AnyPasta = 327983672008368128L;

			public const long Ingredient_AnyBread = 327983694011686912L;

			public const long Ingredient_AnyFlavourVegetable = 327983590005530624L;

			public const long Ingredient_AnyMainVegetable = 327983527388766208L;

			public const long Ingredient_AnyMeat = 327983479418511360L;

			public const long Ingredient_AnyBean = 327983854280237056L;

			public const long Ingredient_AnyBeverage = 327984090495049728L;

			public const long Ingredient_AnyCookedDish = 327984168140005376L;

			public const long Ingredient_AnyFish = 327984263929520128L;

			public const long Ingredient_AnyOther = 327984380392759296L;

			public const long GenericIngredientHelpStart = 327984874716651520L;

			public const long FishingRod_Acceleration = 328689309109444608L;

			public const long FishingRod_ReelingSpeed = 328689312964009984L;

			public const long FishingRod_Size = 328689289895337984L;

			public const long ComparsionToEquippedItem_IncreasedByPercent = 328691781546795008L;

			public const long ComparsionToEquippedItem_DecreasedByPercent = 328691782935109632L;

			public const long ComparsionToEquippedItem_NoChange = 328691727478022144L;

			public const long FishingRod_MaxEffectiveFishWeight = 328696489023889408L;

			public const long WeightKgShort = 328696865701748736L;

			public const long ItemModifier_ReinforcedLine = 328697289624248320L;

			public const long ItemModifier_ReinforcedLineDescription = 328697339892981760L;

			public const long ItemModifier_Precise = 328698041478406144L;

			public const long ItemModifier_PreciseDescription = 328698048633888768L;

			public const long ItemModifier_Lightweight = 328699166793392128L;

			public const long ItemModifier_LightweightDescription = 328699599867863040L;

			public const long ItemModifier_FishCalibrated = 328984205804429312L;

			public const long ItemModifier_FishCalibratedDescription = 328984269666902016L;

			public const long Item_FishInfoHeader = 329015496075632640L;

			public const long Item_FishPolluted = 329025178949378048L;

			public const long Item_FishPollutedDescription = 329026021870264320L;

			public const long Equipment_Fish = 329055552375087104L;

			public const long FishFirstTimeCaught = 329745697759813632L;

			public const long CaughtItemCount = 329431909940584448L;

			public const long TooTiredToFishInfo = 329744041978617856L;

			public const long CaughtCountFish = 329744322158125056L;

			public const long FishNewMaxSizeRecord = 329745659939774464L;

			public const long FishNewMinSizeRecord = 329773428245520384L;

			public const long CatchListHeader = 329775437442965504L;

			public const long FishBiteMissed = 329790253570187264L;

			public const long FishingDone = 329790563772522496L;

			public const long HeavyFishWarning = 329792392925274112L;

			public const long FishNothingCaught = 329802480138313728L;

			public const long Item_Trash = 329826286345117696L;

			public const long Fisher_SellFish = 329851276452945920L;

			public const long Fisher_SellAllFish = 329866980849868800L;

			public const long Fisher_SellAllFishPrompt = 329868300516319232L;

			public const long Fisher_SellFishTitle = 329938634368081920L;

			public const long Fishing_TotalFishesCaught = 329942960369033216L;

			public const long Fishing_DifferentFishesCaught = 329943435785003008L;

			public const long Fishing_BigFishesCaught = 329944719321718784L;

			public const long FileCommandDescription = 330575905115070464L;

			public const long FileCommandFileNotFound = 330576798375993344L;

			public const long FileCommandHelp = 330576860195840000L;

			public const long FileCommandHelp2 = 330578028066234368L;

			public const long CommandExampleUsage = 330578181087027200L;

			public const long FileCommandAccessDenied = 330579698426830848L;

			public const long FileCommandMustInputFilename = 330581303033323520L;

			public const long FileCommandEmptyFile = 330581742961287168L;

			public const long InteractCommandDescription = 330582428763545600L;

			public const long FileCommandFileContents = 330588015542919168L;

			public const long InteractDirectConverasationOver = 330597129824231424L;

			public const long Fishing_WaitForBitesInfo = 332060315479957504L;

			public const long Fishing_MainMinigameInfo = 332060801968889856L;

			public const long CookingMInigame_Help1 = 332101605533933568L;

			public const long CookingMInigame_Help2 = 332101628095094784L;

			public const long NewSaveRecomendedPrompt0_90 = 332408564678123520L;

			public const long FishingMinigame_Help1 = 335500190728314880L;

			public const long FishingMinigame_Help2 = 335500222462418944L;

			public const long FishingMinigame_Help3 = 335501113718464512L;

			public const long FishingMinigame_Help4 = 335501636798504960L;

			public const long FishingMinigame_Help5 = 335502144930045952L;

			public const long FishingMinigame_InfoAboveFishingGauge = 335510175046688768L;

			public const long FishingMinigame_InfoBelowFishingGauge = 335510226649210880L;

			public const long FishingMinigame_InfoAboveFishingProgress = 335510248031772672L;

			public const long FishingMinigame_InfoBelowFishingProgress = 335510271205302272L;

			public const long stocksdotic_OwnershipLimitReached = 341649999747080192L;

			public const long stocksdotic_OwnershipLimitInfo = 341650822040379392L;

			public const long Equipment_SetEquippingCannotCopyVariable = 341677855076245504L;

			public const long Equipment_SetEquippingCopiedBooleanVariable = 341678415997296640L;

			public const long Live2DTool_Soap = 443545661811449856L;

			public const long Live2DTool_Sponge = 443545739800338432L;

			public const long Frick_ThighJob = 445755131346018304L;

			public const long Room_Shower = 445768145315028992L;

			public const long Options_DisplaySettingCheckPrompt = 446085771639644160L;

			public const long Room_Interact = 448969526033244160L;

			public const long Shower_Cleanliness = 450764703022440448L;

			public const long Shower_CleanedInfo = 450802848761896960L;

			public const long Shower_SoapLeft = 450802917011611648L;

			public const long Shower_SoapLeftNoneInfo = 450802950742204416L;

			public const long Shower_Soap = 454039327529230336L;

			public const long Shower_Sponge = 454039364061618176L;

			public const long Shower_WashOff = 454039409787920384L;

			public const long Shower_ChooseSoap = 454040063709274112L;

			public const long Shower_EnoughSoap = 454040364956770304L;

			public const long Shower_NoSoap = 454040604627689472L;

			public const long UI_RentAmountInfo = 456239488389079040L;

			public const long UI_ShoweredToday = 458372233793888256L;

			public const long UI_ShoweredYesterday = 458372287472590848L;

			public const long UI_YouHaventShoweredForXDays = 458372339297411072L;

			public const long Shower_Sex = 471121236486774784L;

			public const long bangdotic_worldnews = 472119528704172032L;

			public const long bangdotic_USAHeader = 472124086796935168L;

			public const long bangdotic_UKHeader = 472124268846505984L;

			public const long bangdotic_OpinionsHeader = 472125338964451328L;

			public const long bangdotic_SportHeader = 472125548503490560L;

			public const long bangdotic_ProjectsHeader = 472126417856880640L;

			public const long bangdotic_EmailStatusHeader = 472129961276203008L;

			public const long ItemDescription_DishIngredients = 296079490024464384L;

			public const long ExpectedIngredient = 297186126352867328L;

			public const long bangdotic_OpinionsTitle = 472134547655352320L;

			public const long Shower_ExitPrompt = 472543019022671872L;

			public const long Cleanliness_Clean = 474648345637281792L;

			public const long Cleanliness_SomeCum = 474648503838040064L;

			public const long Cleanliness_ALotOfCum = 474648570812686336L;

			public const long Interact_Clean = 474693331179266048L;

			public const long Interact_CleanWithARag = 474696176054034432L;

			public const long Shower_ExitPromptBeforeAnything = 476105129992380416L;

			public const long Shower_ExitPromptNotFinished = 476105271864713216L;

			public const long Equipment_DontOwnColor = 476191777304338432L;

			public const long Equipment_DontOwn = 476191862738116608L;

			public const long Equipment_OwnPackage = 476191918648188928L;

			public const long Equipment_DontOwnPackage = 476192284051759104L;

			public const long Equipment_OwnSlot = 476193003509112832L;

			public const long Equipment_DontOwnSlot = 476193361698480128L;

			public const long Equipment_ViewRelated = 476193497661038592L;

			public const long Equipment_ExactItemMatch = 476193841459748864L;

			public const long Equipment_RelatedItems = 476193969608318976L;

			public const long Equipment_RelatedOwnedItems = 476194087434706944L;

			public const long Equipment_RelatedOwnedItemsInfoText = 476194174818836480L;

			public const long Achievments_StoryTextPercent = 476526596789886976L;

			public const long Options_SkipUnseedTextToggle = 476546070280658944L;

			public const long UI_SkipEnabledNewTextPrompt = 476552496302514176L;

			public const long Options_CumFlashBangToggle = 478056522227638272L;

			public const long Options_CumFlashbangTooltip = 478056759537164288L;

			public const long Options_SkipUnseenTextTooltip = 478060044352479232L;

			public const long Room_LeaveTheCity = 483040306832334848L;

			public const long Mods_SafetyWarning1 = 483407230179205120L;

			public const long Mods_SafetyWarning2 = 483407824948289536L;

			public const long Mods_SafetyWarning3 = 483407827385180160L;

			public const long Mods_SafetyWarning4 = 483407829138399232L;

			public const long Achievements_NextUnseedGoodEnding = 484477438352416768L;

			public const long Achievements_MainEndingsHeader = 484477599141060608L;

			public const long Achievements_OtherHeader = 484477662353416192L;

			public const long Achievements_ChurchEndingsHeader = 484478029224992768L;

			public const long Achievements_EndingSeen = 484478221374447616L;

			public const long Achievements_EndingUnseen = 484478249354649600L;

			public const long Achievements_ShowExtraDetailsToggle = 484479769101983744L;

			public const long Achievements_GoodEndingsHeader = 484483204786413568L;

			public const long Achievements_BadEndingsHeader = 484483236382105600L;

			public const long Achievements_GoodEndingsCount = 484487836430491648L;

			public const long Achievements_BadEndingsCount = 484487912431280128L;

			public const long Credits_GoodEndingAchieved = 484532030339538944L;

			public const long UI_NewSkipEnabledNewTextPrompt = 485487702006816768L;

			public const long UI_SkipButtonTooltip = 485888026693591040L;

			public const long UI_NarrationLogTooltip = 485889586559442944L;

			public const long CookingSong_BestDishQuality = 486295437891854336L;

			public const long UI_EarlyAccessThanks = 487788642143559680L;

			public const long UI_EarlyAccessWarning = 487788813547986944L;

			public const long UI_EarlyAccessPirateWarning = 487795609801056256L;

			public const long UI_EarlyAccessOfficialSources = 487796447680389120L;

			public const long UI_PullingOutActionContinuuing = 487809587918921728L;

			public const long Slot_BodyModules = 487880583661215744L;

			public const long Slot_FeetBodyModules = 488162793228328960L;

			public const long Slot_CalfBodyModules = 488162780284706816L;

			public const long Slot_ThighBodyModules = 488162753244028928L;

			public const long Equipment_SetEquippingCopiedFloatVariable = 488163137152868352L;

			public const long BodyMod_ThighSize = 488166734942953472L;

			public const long BodyMod_CalfSize = 488166887586258944L;

			public const long BodyMod_FootWidth = 488166928447168512L;

			public const long BodyMod_FootLength = 488166975201075200L;

			public const long Equipment_RandomOutfit = 488975334281584640L;

			public const long Equipment_RandomOutfitPrompt = 488975408898252800L;

			public const long SAVE_LoadErrorInvalidSavePrompt = 489264642502696960L;

			public const long Options_GrayOutSeenTextToggle = 489268972827975680L;

			public const long Options_GreyOutSeenTextTooltip = 489272145760231424L;

			public const long Equipment_CannotChangeOrderWhileSortedAllowedSortPrompt = 489289234281668608L;

			public const long Mods_SpecialButton = 489336303788761088L;

			public const long Eyepatch_Left = 489558131031416832L;

			public const long Eyepatch_Right = 489558461962002432L;

			public const long Slot_TongueAccessory = 489588868422049792L;

			public const long ChooseLanguage = 489645086704410624L;

			public const long ChooseModTranslationGenerationLanguage = 489645144103460864L;

			public const long SuccessfulyGenerateTranslationFile = 489646130897362944L;

			public const long FailedGeneratingTranslationFile = 489647547020222464L;

			public const long Mods_ModVersion = 489661828419821568L;

			public const long SelectModTranslationFilesPrompt = 489663437761683456L;

			public const long ModTranslationFiles = 489663838821031936L;

			public const long SuccessfulyLoadedModTranslationFile = 489666750372651008L;

			public const long FailedLoadingModTranslationFile = 489667826001911808L;

			public const long Mods_ViewLogs = 489670808600846336L;

			public const long Mods_ModTranslatedToLanguage = 489686432504881152L;

			public const long Mods_ModNotTranslatedToLangauge = 489686547558834176L;

			public const long Mods_GenerateModTranslationFile = 489987962034462720L;

			public const long Mods_ImportModTranslationFiles = 489988037330608128L;

			public const long Mods_NoTranslationData = 489990660490272768L;

			public const long Mods_ImportedTranslations = 489990733857038336L;

			public const long Mods_ModTranslationNotes = 489998867560407040L;

			public const long Mods_DeleteInstalledTranslationModFiles = 490004860151275520L;

			public const long Mods_DeleteTranslationModFIlesPrompt = 490004949460590592L;

			public const long Mods_DeleteTranslationModFIlesPickerPrompt = 490944453897887744L;

			public const long Mods_ExportInstalledTranslationModFiles = 490944849613692928L;

			public const long Mods_ExportTranslationModFIlesPickerPrompt = 490945246227079168L;

			public const long Mods_LocalizingMods = 491009961577947136L;

			public const long Mods_NoTranslationIssuesFound = 491025278207270912L;

			public const long Mods_TranslationIssuesExtraKeys = 491025385531121664L;

			public const long Mods_TranslationIssuesMissingKeys = 491025453571121152L;

			public const long Mods_TranslationIssuesBrokenKeys = 491025488312541184L;

			public const long Mods_TranslationIssuesLogInfo = 491032741463728128L;

			public const long Mods_TranslationIssuesBroken = 491033140786634752L;

			public const long Mods_TargetModVersion = 491037523914006528L;

			public const long HoverToView = 491037976072560640L;

			public const long FullyTranslated = 491040484891303936L;

			public const long Mods_TranslationIssues = 491043588659486720L;

			public const long Mods_ModsOriginalLangauge = 491061162591920128L;

			public const long Folder_Sliders = 491337545033457664L;

			public const long Folder_Toggles = 491337567032582144L;

			public const long Slot_Manicure = 491372144350470144L;

			public const long Slot_Pedicure = 491421408766205952L;

			public const long InputEmail = 493902357215059968L;

			public const long InputEmailAreYouSure = 493907265213440000L;

			public const long InvalidEmail = 493909894538731520L;

			public const long FailedToVerify = 493910851381108736L;

			public const long VerifyOnDiscord = 493922460669321216L;

			public const long VerifyWithoutDiscord = 493922652495814656L;

			public const long IHaveTheCode = 493922962240970752L;

			public const long LoginInfo_GameEarlyAccess = 493924743922589696L;

			public const long LoginInfo_AccessInfo = 493925030305472512L;

			public const long LoginInfo_DiscordCommand = 493927354675503104L;

			public const long Login_Email = 493932568472199168L;

			public const long LinksToStuff = 493944224614981632L;

			public const long JoinDiscordServer = 493946882415403008L;

			public const long LoginInfo_AccessInfoOnlyOnce = 493949397156503552L;

			public const long LoginInfo_PrivateMessage = 493982020503347200L;

			public const long LoginInfo_DiscordCommand2 = 493982440189599744L;

			public const long LoginInfo_Alternative = 493982925793533952L;

			public const long LoginInfo_Alternative2 = 493983195621498880L;

			public const long LoginInfo_WhatToDoWithCode = 493985080814346240L;

			public const long LoginInfo_IHaveTheCodePrompt = 493986248156258304L;

			public const long LoginInfo_SuccessfulVerification = 493987150627872768L;

			public const long BodyMod_BoobSize = 496402615639744512L;

			public const long Slot_BoobBodyModules = 496403323130748928L;

			public const long Equipment_Legs = 496802670846681088L;

			public const long Equipment_Arms = 496802702727585792L;

			public const long Equipment_Socks = 496803314248720384L;

			public const long Equipment_Shoes = 496804553137070080L;

			public const long Equipment_LowerBody = 496810800242499584L;

			public const long Equipment_UpperBody = 496810849705926656L;

			public const long Equipment_Outer = 496810876587220992L;

			public const long Equipment_BodySizeModules = 496814697568804864L;

			public const long Room_Clothes = 497811185744089088L;

			public const long Equipment_MakeUp = 497824627733434368L;

			public const long Equipment_Neck = 497826289369878528L;

			public const long Equipment_Piercing = 497826636654055424L;

			public const long BodyMod_AssSize = 497858709615939584L;

			public const long Slot_AssBodyModules = 498300297139851264L;

			public const long SAVE_LoadFromSlotFileChooseSlotPrompt = 498332293912203264L;

			public const long SAVE_InvalidSlotNumberNotNumber = 498334095147667456L;

			public const long SAVE_InvalidSlotNumberOutOfRange = 498334455421603840L;

			public const long NumbersRangeInfo = 498334650720980992L;

			public const long SAVE_InvalidSlotNumberAlreadyTaken = 498336309140393984L;

			public const long SAVE_SuccessfullyLoadedSlotDataIntoSlotNumber = 498338501477928960L;

			public const long SAVE_SlotLoadingWarning = 498342642031923200L;

			public const long SAVE_SlotFileExtension = 498343662011158528L;

			public const long Room_DoSomethingTogether = 498365790617997312L;

			public const long Interact_Sex = 498368129751977984L;

			public const long LoginInfo_PublicReleaseData = 498590055413977088L;

			public const long Slot_Navel = 499141756822884352L;

			public const long Slot_NippleLeft = 499141866881421312L;

			public const long Slot_NippleRight = 499141868802412544L;

			public const long ModifableVariables_Header = 499378805064732672L;

			public const long ModifiableVariables_CanBeChanged = 499378856839221248L;

			public const long PasteFromClipboard = 499406199741878272L;

			public const long PasteFromClipboard_SuccessfullyPasted = 499407869011955712L;

			public const long PasteFromClipboard_ClipboardEmpty = 499407920878718976L;

			public const long Options_ClearVerification = 499410529249267712L;

			public const long Interactable_Spank = 537744955715608576L;

			public const long Interactable_SpreadAnus = 537769387989393408L;

			public const long Interactable_GrabWaist = 538486512190218240L;

			public const long Interactable_FingeringAnus = 538508930501828608L;

			public const long Frick_ChangeCamera = 540001448330518528L;

			public const long Frick_ShowCock = 540990535136960512L;

			public const long Hair_FrontClippedUp = 542208471210123264L;

			public const long FishingDuelWon = 543309117950779392L;

			public const long FishingDuelLost = 543309152826417152L;

			public const long FishingDuelWinnerYou = 543502681200648192L;

			public const long FishingDuelWinnerEnemy = 543524635571904512L;

			public const long FishingFishJointStatBoost = 543559430871965696L;

			public const long LoginInfo_MakeSureToEnableDms = 545019226687004672L;

			public const long FishingDuelScore = 546458543988924416L;

			public const long Frick_CantDoThatWhenCumming = 546782993317879808L;

			public const long CheatsTitle = 550048406222921728L;

			public const long Options_DisableAdvancedPrompt = 550049084538347520L;

			public const long Cheat_Money = 550063086752489472L;

			public const long Cheat_Items = 550063118595645440L;

			public const long Cheat_ItemColors = 550063161595650048L;

			public const long Cheat_Quality = 550063278054694912L;

			public const long Cheat_CumCheats = 550063365107474432L;

			public const long Cheats_AccessLinks = 550065358517559296L;

			public const long Cheats_GetCheatCode = 550068833926242304L;

			public const long Cheats_HaveCheatCode = 550069333585289216L;

			public const long OpenedLink = 550092321927979008L;

			public const long Cheat_DiscordCommand2 = 550095134015873024L;

			public const long CopiedText = 550095455563800576L;

			public const long Cheat_AreYouSubscribed = 550095603262021632L;

			public const long Cheat_SadlyOnlyForPatrons = 550095920481427456L;

			public const long IDontWantToUseDiscord = 550098058615971840L;

			public const long NotUsingDiscordNotRecommended = 550098103817986048L;

			public const long Cheat_SendToDevelopers = 550098656807608320L;

			public const long Cheat_DisableCheats = 550101143010664448L;

			public const long Cheat_AreYouSureDisable = 550101607492083712L;

			public const long Cheat_InvalidCode = 550103423273394176L;

			public const long Cheat_SuccessBasicCheats = 550103618782486528L;

			public const long Cheat_SuccessAdvancedCheats = 550103664789807104L;

			public const long Cheat_DisableCheatsHelp = 550104163198951424L;

			public const long Cheat_Active = 550106583442382848L;

			public const long Cheat_Status = 550109001706102784L;

			public const long Cheat_BasicCheats = 550109357429219328L;

			public const long Cheat_DisabledCheats = 550109393877721088L;

			public const long Cheat_AdvancedCheats = 550109429743214592L;

			public const long UI_CheatsMainMenu = 550110490625302528L;

			public const long UI_OtherMainMenu = 550111901958266880L;

			public const long CanChangeColorThanksToCheats = 550134053780578304L;

			public const long Cheat_AndMore = 550138070548078592L;

			public const long Cheat_RandomiseModifiers = 550138237066141696L;

			public const long Cheat_RemoveModifiers = 550140764364988416L;

			public const long Cheat_RestoreCum = 550148455409901568L;

			public const long Cheat_IncreaseMaxCum = 550148500142153728L;

			public const long Cheat_CumCheat = 550148553929908224L;

			public const long Cheat_CumRestorationFull = 550150305165074432L;

			public const long Cheat_CumRestorationOverflow = 550150361016426496L;

			public const long Cheat_CumRestorationAlreadyFull = 550150909765607424L;

			public const long Cheat_ResetCumCapacity = 550151074840829952L;

			public const long Cheat_ResetCumCapacitySuccess = 550151292432932864L;

			public const long Cheat_IncreasedCumCapacity = 550151887713722368L;

			public const long Cheat_IncreasedCumCapacityWarning = 550152587306852352L;

			public const long Cheat_AddMoney = 550157358130520064L;

			public const long Cheat_MaxMoneyReached = 550157731629096960L;

			public const long Tooltip_SaveGame = 550158855799037952L;

			public const long Tooltip_ReturnToMainMenu = 550158882395119616L;

			public const long Tooltip_Cheats = 550158924879224832L;

			public const long iamacheater_ButtonNew = 550162874974068736L;

			public const long iamacheater_SadNews = 550162995329622016L;

			public const long Cheats_Other = 550165115571265536L;

			public const long Cheat_ResetChurchPromptWarning = 550170296421703680L;

			public const long WebGL_InputFieldInfo = 551219030114951168L;

			public const long Cheat_AllOfTheBasic = 551227257611345920L;

			public const long SubscribeOn = 552261364487282688L;

			public const long SubscribeNowToAccess = 552264424500486144L;

			public const long Benefit_EarlyAccess = 552264468259659776L;

			public const long Benefit_Devlogs = 552264495984009216L;

			public const long Benefit_ConceptArt = 552264522567507968L;

			public const long Benefit_AndMore = 552264558722408448L;

			public const long DiscordNotExpired = 552278588459573248L;

			public const long Cheat_ChangeFishWeight = 553314502581411840L;

			public const long Cheat_PlayCookiningCheat = 572861515845066752L;

			public const long Cheat_CookInstantlyPrompt = 572864247184220160L;

			public const long Cheat_PlayCookiningCheatSaveScore = 572864683995815936L;

			public const long Cheat_PlayCookiningCheatDiscardScore = 572864686193631232L;
		}

		public static class TupleReferences
		{
			public static readonly LOS.TupleReference UI_Achievements = GetTupleReference(5641477165056L);

			public static readonly LOS.TupleReference UI_Mods = GetTupleReference(5874114236416L);

			public static readonly LOS.TupleReference UI_SHILLING = GetTupleReference(52622597689344L);

			public static readonly LOS.TupleReference Room_Status = GetTupleReference(54939963232256L);

			public static readonly LOS.TupleReference Room_Equipment = GetTupleReference(55646338551808L);

			public static readonly LOS.TupleReference Room_Items = GetTupleReference(55750864801792L);

			public static readonly LOS.TupleReference Room_Outside = GetTupleReference(55832175579136L);

			public static readonly LOS.TupleReference Room_Sleep = GetTupleReference(55998030942208L);

			public static readonly LOS.TupleReference Room_PC = GetTupleReference(56098648100864L);

			public static readonly LOS.TupleReference PC_EquipmentShort = GetTupleReference(60582942085120L);

			public static readonly LOS.TupleReference PC_Browser = GetTupleReference(60984836100096L);

			public static readonly LOS.TupleReference PC_Shortcut = GetTupleReference(66758454939648L);

			public static readonly LOS.TupleReference PC_UnreadEmails = GetTupleReference(71063857664000L);

			public static readonly LOS.TupleReference Options_FontColor = GetTupleReference(78759663112192L);

			public static readonly LOS.TupleReference Options_Sensitivity_Tooltip = GetTupleReference(79691826204672L);

			public static readonly LOS.TupleReference Options_ResetGameData = GetTupleReference(81463160475648L);

			public static readonly LOS.TupleReference Achievements_2 = GetTupleReference(1750558276718592L);

			public static readonly LOS.TupleReference Achievements_1 = GetTupleReference(1750513515106304L);

			public static readonly LOS.TupleReference Achievements_3 = GetTupleReference(1750573665619968L);

			public static readonly LOS.TupleReference Achievements_4 = GetTupleReference(1752188019675136L);

			public static readonly LOS.TupleReference Achievements_5 = GetTupleReference(1752408820420608L);

			public static readonly LOS.TupleReference Achievements_Cheating = GetTupleReference(1752715424043008L);

			public static readonly LOS.TupleReference Save_FromFile = GetTupleReference(1754802484547584L);

			public static readonly LOS.TupleReference Save_ToFile = GetTupleReference(1755092688441344L);

			public static readonly LOS.TupleReference UI_Error = GetTupleReference(1761228200218624L);

			public static readonly LOS.TupleReference UI_Success = GetTupleReference(1761256759234560L);

			public static readonly LOS.TupleReference UI_Override = GetTupleReference(1761293052547072L);

			public static readonly LOS.TupleReference UI_Failed = GetTupleReference(1761603401682944L);

			public static readonly LOS.TupleReference SAVE_OverridePrompt = GetTupleReference(1763006232793088L);

			public static readonly LOS.TupleReference SAVE_LoadPrompt = GetTupleReference(1763445565165568L);

			public static readonly LOS.TupleReference SAVE_LoadErrorPrompt = GetTupleReference(1763689476526080L);

			public static readonly LOS.TupleReference SAVE_LoadSuccessPrompt = GetTupleReference(1763972839510016L);

			public static readonly LOS.TupleReference UI_AreYouSure = GetTupleReference(1764674592706560L);

			public static readonly LOS.TupleReference SAVE_LoadFromFilePrompt = GetTupleReference(1764968076546048L);

			public static readonly LOS.TupleReference SAVE_LoadFromFilePrompt2 = GetTupleReference(1765023525244928L);

			public static readonly LOS.TupleReference SAVE_ExportSuccess = GetTupleReference(1765173991706624L);

			public static readonly LOS.TupleReference SAVE_ExportFailed = GetTupleReference(1765233185918976L);

			public static readonly LOS.TupleReference SAVE_DeleteSavePrompt = GetTupleReference(1765677111054336L);

			public static readonly LOS.TupleReference SAVE_Slot = GetTupleReference(1766022692343808L);

			public static readonly LOS.TupleReference SAVE_SlotEmpty = GetTupleReference(1766333414772736L);

			public static readonly LOS.TupleReference SAVE_ImportFailure = GetTupleReference(1766569004634112L);

			public static readonly LOS.TupleReference SAVE_FileExtension = GetTupleReference(1766739557617664L);

			public static readonly LOS.TupleReference UI_Delete = GetTupleReference(1769398180122624L);

			public static readonly LOS.TupleReference Save_NotesInfo = GetTupleReference(1771411831918592L);

			public static readonly LOS.TupleReference Options_ResetGameDataPrompt = GetTupleReference(1784018898915328L);

			public static readonly LOS.TupleReference Options_ResetGameDataPrompt2 = GetTupleReference(1784182296416256L);

			public static readonly LOS.TupleReference Mods_DataFiles = GetTupleReference(1785235481956352L);

			public static readonly LOS.TupleReference Mods_Status = GetTupleReference(1785407087710208L);

			public static readonly LOS.TupleReference Mods_VersionMismatch = GetTupleReference(1785619994775552L);

			public static readonly LOS.TupleReference Mods_Activating = GetTupleReference(1785885225783296L);

			public static readonly LOS.TupleReference Mods_NotActivating = GetTupleReference(1786412189749248L);

			public static readonly LOS.TupleReference Mods_RestartToActivate = GetTupleReference(1786541231706112L);

			public static readonly LOS.TupleReference Mods_RestartToDeactivate = GetTupleReference(1786564413624320L);

			public static readonly LOS.TupleReference Mods_Deactivate = GetTupleReference(1786827136438272L);

			public static readonly LOS.TupleReference Mods_Activate = GetTupleReference(1786867661803520L);

			public static readonly LOS.TupleReference Mods_NoModSelected = GetTupleReference(1787004014432256L);

			public static readonly LOS.TupleReference Mods_RestartRequired = GetTupleReference(1787320051044352L);

			public static readonly LOS.TupleReference Mods_ClosingTheGame = GetTupleReference(1787433662156800L);

			public static readonly LOS.TupleReference Mods_ClosingTheGamePrompt = GetTupleReference(1787721605320704L);

			public static readonly LOS.TupleReference Mods_DeleteModPrompt = GetTupleReference(1788021279952896L);

			public static readonly LOS.TupleReference Mods_LoadingSuccess = GetTupleReference(1788364734730240L);

			public static readonly LOS.TupleReference Mods_LoadingError = GetTupleReference(1788709607182336L);

			public static readonly LOS.TupleReference Mods_LoadOrder = GetTupleReference(1789300911771648L);

			public static readonly LOS.TupleReference Mods_ExportLog = GetTupleReference(1789456721776640L);

			public static readonly LOS.TupleReference Mods_Export = GetTupleReference(1789851787464704L);

			public static readonly LOS.TupleReference Mods_LoadMods = GetTupleReference(1789943563030528L);

			public static readonly LOS.TupleReference Mods_CreateNewMod = GetTupleReference(1790020499148800L);

			public static readonly LOS.TupleReference Mods_Edit = GetTupleReference(1793475519320064L);

			public static readonly LOS.TupleReference Mods_Status_Exception = GetTupleReference(1795788409540608L);

			public static readonly LOS.TupleReference Mods_Status_ExceptionDescription = GetTupleReference(1795852351705088L);

			public static readonly LOS.TupleReference Mods_Status_NotLoaded = GetTupleReference(1795932022509568L);

			public static readonly LOS.TupleReference Mods_Status_NotLoadedDescription = GetTupleReference(1795937823232000L);

			public static readonly LOS.TupleReference Mods_Status_Ready = GetTupleReference(1796038075486208L);

			public static readonly LOS.TupleReference Mods_Status_ReadyDescription = GetTupleReference(1796041699364864L);

			public static readonly LOS.TupleReference Mods_Status_Active = GetTupleReference(1796444847476736L);

			public static readonly LOS.TupleReference Mods_Status_ActiveDescription = GetTupleReference(1796448366497792L);

			public static readonly LOS.TupleReference Mods_Status_ActiveButOutdatedDescription = GetTupleReference(1796689778053120L);

			public static readonly LOS.TupleReference Mods_Status_ActiveButOutdated = GetTupleReference(1796688003862528L);

			public static readonly LOS.TupleReference Mods_Status_Failed = GetTupleReference(1796889347231744L);

			public static readonly LOS.TupleReference Mods_Status_FailedDescription = GetTupleReference(1796891691847680L);

			public static readonly LOS.TupleReference UI_Error_PathTooLong = GetTupleReference(1800544611864576L);

			public static readonly LOS.TupleReference UI_HungerBarTooltip = GetTupleReference(2094004392824832L);

			public static readonly LOS.TupleReference UI_MentalBarTooltip = GetTupleReference(2094244428648448L);

			public static readonly LOS.TupleReference UI_StaminaBarTooltip = GetTupleReference(2094353702850560L);

			public static readonly LOS.TupleReference UI_StaminaBarTooltipDynamic = GetTupleReference(2095981008916480L);

			public static readonly LOS.TupleReference Satiation_Full = GetTupleReference(2096487982829568L);

			public static readonly LOS.TupleReference Satiation_LittleHungry = GetTupleReference(2096772633464832L);

			public static readonly LOS.TupleReference Satiation_Hungry = GetTupleReference(2096884579438592L);

			public static readonly LOS.TupleReference Satiation_VeryHungry = GetTupleReference(2096949058473984L);

			public static readonly LOS.TupleReference Health_VeryHealthy = GetTupleReference(2097683539824640L);

			public static readonly LOS.TupleReference Health_Healthy = GetTupleReference(2097731199700992L);

			public static readonly LOS.TupleReference Health_Tired = GetTupleReference(2097793426395136L);

			public static readonly LOS.TupleReference Health_NearDeath = GetTupleReference(2097933532925952L);

			public static readonly LOS.TupleReference Health_Terrible = GetTupleReference(2097831221268480L);

			public static readonly LOS.TupleReference Health_Awful = GetTupleReference(2097827458977792L);

			public static readonly LOS.TupleReference Cum_Overfilled = GetTupleReference(2099237902749696L);

			public static readonly LOS.TupleReference Cum_Full = GetTupleReference(2099316139102208L);

			public static readonly LOS.TupleReference Cum_Half = GetTupleReference(2099386154618880L);

			public static readonly LOS.TupleReference Cum_Recovering = GetTupleReference(2099429599219712L);

			public static readonly LOS.TupleReference Cum_Empty = GetTupleReference(2099465959641088L);

			public static readonly LOS.TupleReference Cum_NoCum = GetTupleReference(2099764451479552L);

			public static readonly LOS.TupleReference Cum_Dynamic = GetTupleReference(2099884156915712L);

			public static readonly LOS.TupleReference UI_Day = GetTupleReference(2100490221260800L);

			public static readonly LOS.TupleReference Frick_CommentsVisible = GetTupleReference(2114199559737344L);

			public static readonly LOS.TupleReference Frick_CommentsHidden = GetTupleReference(2114244979855360L);

			public static readonly LOS.TupleReference Frick_Unzip = GetTupleReference(2114513243344896L);

			public static readonly LOS.TupleReference Frick_HideCock = GetTupleReference(2114425968267264L);

			public static readonly LOS.TupleReference Frick_Vagina = GetTupleReference(2114698392506368L);

			public static readonly LOS.TupleReference Frick_Anal = GetTupleReference(2114694688935936L);

			public static readonly LOS.TupleReference Frick_Manual = GetTupleReference(2114825995816960L);

			public static readonly LOS.TupleReference Frick_Slow = GetTupleReference(2114827686121472L);

			public static readonly LOS.TupleReference Frick_Medium = GetTupleReference(2114829372231680L);

			public static readonly LOS.TupleReference Frick_Fast = GetTupleReference(2114832597651456L);

			public static readonly LOS.TupleReference Frick_SlowmodeUnlockedPrompt = GetTupleReference(2115271707725824L);

			public static readonly LOS.TupleReference Frick_SlowMode = GetTupleReference(2115415199059968L);

			public static readonly LOS.TupleReference Frick_SlowModeDisable = GetTupleReference(2115796822003712L);

			public static readonly LOS.TupleReference Frick_SlowModeEnable = GetTupleReference(2115848101564416L);

			public static readonly LOS.TupleReference Frick_SlowModeDisabled = GetTupleReference(2115896130539520L);

			public static readonly LOS.TupleReference Frick_SlowModeEnabled = GetTupleReference(2115898215108608L);

			public static readonly LOS.TupleReference UI_CantDoThat = GetTupleReference(2116365439602688L);

			public static readonly LOS.TupleReference Frick_EndFailedPrompt = GetTupleReference(2116415813193728L);

			public static readonly LOS.TupleReference Frick_StreamEndPrompt = GetTupleReference(2116779413213184L);

			public static readonly LOS.TupleReference Frick_LastStreamSummary = GetTupleReference(2116905913421824L);

			public static readonly LOS.TupleReference Frick_LastStreamSummaryPrompt = GetTupleReference(2117264815820800L);

			public static readonly LOS.TupleReference Frick_EndFrickPrompt = GetTupleReference(2117694098640896L);

			public static readonly LOS.TupleReference Frick_ChatStart = GetTupleReference(2144031383359488L);

			public static readonly LOS.TupleReference Frick_ChatBanned = GetTupleReference(2144246949613568L);

			public static readonly LOS.TupleReference Frick_StreamSummary = GetTupleReference(2144474717097984L);

			public static readonly LOS.TupleReference Frick_Position = GetTupleReference(2146112311791616L);

			public static readonly LOS.TupleReference Interact_Frick = GetTupleReference(2147091111350272L);

			public static readonly LOS.TupleReference Interact_Talk = GetTupleReference(2147310427312128L);

			public static readonly LOS.TupleReference Interact_SmallTalk = GetTupleReference(2147521937674240L);

			public static readonly LOS.TupleReference Interact_Generic = GetTupleReference(2147759142342656L);

			public static readonly LOS.TupleReference Interact_Statistics = GetTupleReference(2148276371329024L);

			public static readonly LOS.TupleReference Interact_Nevermind = GetTupleReference(2148378422939648L);

			public static readonly LOS.TupleReference Equipment_Zoom = GetTupleReference(2442436240347136L);

			public static readonly LOS.TupleReference Equipment_LockPosition = GetTupleReference(2442551281717248L);

			public static readonly LOS.TupleReference Equipment_UnlockPosition = GetTupleReference(2442590787866624L);

			public static readonly LOS.TupleReference Equipment_EquippedPostfix = GetTupleReference(2443240200343552L);

			public static readonly LOS.TupleReference Equipment_EquipSetPrompt = GetTupleReference(2443693403279360L);

			public static readonly LOS.TupleReference UI_Done = GetTupleReference(2444114893082624L);

			public static readonly LOS.TupleReference Equipment_YesReplaceCurrent = GetTupleReference(2444395538157568L);

			public static readonly LOS.TupleReference Equipment_SetSavedSuccess = GetTupleReference(2444932593618944L);

			public static readonly LOS.TupleReference Equipment_SetSavedFailure = GetTupleReference(2445047660154880L);

			public static readonly LOS.TupleReference Equipment_SetDeletePrompt = GetTupleReference(2445385301626880L);

			public static readonly LOS.TupleReference Equipment_SetLoadedSuccess = GetTupleReference(2445837711839232L);

			public static readonly LOS.TupleReference Equipment_SetLoadedFailure = GetTupleReference(2446030670794752L);

			public static readonly LOS.TupleReference Equipment_SetFiletype = GetTupleReference(2446724601614336L);

			public static readonly LOS.TupleReference Equipment_All = GetTupleReference(2446868155863040L);

			public static readonly LOS.TupleReference Equipment_Food = GetTupleReference(2447029712064512L);

			public static readonly LOS.TupleReference Equipment_Package = GetTupleReference(2447128693444608L);

			public static readonly LOS.TupleReference Equipment_Modules = GetTupleReference(2447224562651136L);

			public static readonly LOS.TupleReference Equipment_Clothes = GetTupleReference(2447265415172096L);

			public static readonly LOS.TupleReference Equipment_Other = GetTupleReference(2447384768286720L);

			public static readonly LOS.TupleReference Equipment_Equipped = GetTupleReference(2447438627344384L);

			public static readonly LOS.TupleReference Equipment_NotEquipped = GetTupleReference(2447476803899392L);

			public static readonly LOS.TupleReference Equipment_ButtonEquip = GetTupleReference(2447962097455104L);

			public static readonly LOS.TupleReference Equipment_ButtonUnequip = GetTupleReference(2447995454754816L);

			public static readonly LOS.TupleReference Equipment_UnequipDependencyPrompt = GetTupleReference(2448396446994432L);

			public static readonly LOS.TupleReference Equipment_ButtonUse = GetTupleReference(2448593407315968L);

			public static readonly LOS.TupleReference Equipment_ButtonThrowAway = GetTupleReference(2448687506526208L);

			public static readonly LOS.TupleReference Equipment_ThrowAwayPrompt = GetTupleReference(2448896844238848L);

			public static readonly LOS.TupleReference Equipment_ThrowAwayUnequipFirst = GetTupleReference(2449237694353408L);

			public static readonly LOS.TupleReference Equipment_UnpackedItems = GetTupleReference(2449390274744320L);

			public static readonly LOS.TupleReference Equipment_UnpackedItemsPrompt = GetTupleReference(2449415880970240L);

			public static readonly LOS.TupleReference Equipment_FromCurrent = GetTupleReference(2454147609530368L);

			public static readonly LOS.TupleReference Equipment_FromFile = GetTupleReference(2454277171580928L);

			public static readonly LOS.TupleReference Equipment_ButtonLoadSet = GetTupleReference(2454765636030464L);

			public static readonly LOS.TupleReference Eqiupment_ButtonExportSet = GetTupleReference(2454986185117696L);

			public static readonly LOS.TupleReference Equipment_Set = GetTupleReference(2455815231246336L);

			public static readonly LOS.TupleReference Equipment_SetCreationPrompt = GetTupleReference(2465429096108032L);

			public static readonly LOS.TupleReference Equipment_SetCreationAllItems = GetTupleReference(2465591445032960L);

			public static readonly LOS.TupleReference Equipment_SetCreationSelect = GetTupleReference(2465725461434368L);

			public static readonly LOS.TupleReference Equipment_SetCreationSelectPrompt = GetTupleReference(2465916432289792L);

			public static readonly LOS.TupleReference Equipment_SetDefaultName = GetTupleReference(2466019020771328L);

			public static readonly LOS.TupleReference Equipment_SetEmpty = GetTupleReference(2466301146435584L);

			public static readonly LOS.TupleReference Equipment_SetListHeader = GetTupleReference(2466378640396288L);

			public static readonly LOS.TupleReference Equipment_SetSummaryShort = GetTupleReference(2466914664058880L);

			public static readonly LOS.TupleReference Equipment_SetEquippingIteration = GetTupleReference(2467180310302720L);

			public static readonly LOS.TupleReference Equipment_SetEquipping1 = GetTupleReference(2467492509126656L);

			public static readonly LOS.TupleReference Equipment_SetEquipping2 = GetTupleReference(2467605923106816L);

			public static readonly LOS.TupleReference Equipment_SetEquipping3 = GetTupleReference(2467812836511744L);

			public static readonly LOS.TupleReference Equipment_SetEquippingAs = GetTupleReference(2468124016119808L);

			public static readonly LOS.TupleReference Equipment_SetEquippingChangingColor = GetTupleReference(2468356758048768L);

			public static readonly LOS.TupleReference Equipment_SetEquippingCannotEquip = GetTupleReference(2468601126588416L);

			public static readonly LOS.TupleReference Equipment_SetEquippingMissing = GetTupleReference(2468881482256384L);

			public static readonly LOS.TupleReference Equipment_SetEquippingDroppedListHeader = GetTupleReference(2469383586582528L);

			public static readonly LOS.TupleReference Equipment_SetEquippingFinished = GetTupleReference(2469075598839808L);

			public static readonly LOS.TupleReference Equipment_SetEquippingLeftListHeader = GetTupleReference(2469555460771840L);

			public static readonly LOS.TupleReference Equipment_SetEquippingAllItemsFound = GetTupleReference(2469699606417408L);

			public static readonly LOS.TupleReference UI_ColorChangeInfo = GetTupleReference(2482018793164800L);

			public static readonly LOS.TupleReference UI_ColorChoose = GetTupleReference(2482856454393856L);

			public static readonly LOS.TupleReference Equipment_NoItemSelected = GetTupleReference(2483178690187264L);

			public static readonly LOS.TupleReference UI_Price = GetTupleReference(2483338505752576L);

			public static readonly LOS.TupleReference UI_Condition = GetTupleReference(2483655213453312L);

			public static readonly LOS.TupleReference Condition_Trashed = GetTupleReference(2484800145199104L);

			public static readonly LOS.TupleReference Condition_VeryHeavilyUsed = GetTupleReference(2484864238358528L);

			public static readonly LOS.TupleReference Condition_HeavilyUsed = GetTupleReference(2484932391604224L);

			public static readonly LOS.TupleReference Condition_Used = GetTupleReference(2484937630289920L);

			public static readonly LOS.TupleReference Condition_NearMint = GetTupleReference(2484938712420352L);

			public static readonly LOS.TupleReference Condition_New = GetTupleReference(2485069838946304L);

			public static readonly LOS.TupleReference UI_ReturnToMainMenuPrompt = GetTupleReference(2836390274273280L);

			public static readonly LOS.TupleReference Shop_BuyPrompt = GetTupleReference(2836835176681472L);

			public static readonly LOS.TupleReference Shop_NotEnoughMoney = GetTupleReference(2837005276680192L);

			public static readonly LOS.TupleReference UI_ImportFile = GetTupleReference(2837910927577088L);

			public static readonly LOS.TupleReference UI_ExportFile = GetTupleReference(2838133380878336L);

			public static readonly LOS.TupleReference UI_ExportFilePrompt = GetTupleReference(2838282203172864L);

			public static readonly LOS.TupleReference UI_Apply = GetTupleReference(2838512692760576L);

			public static readonly LOS.TupleReference UI_Cancel = GetTupleReference(2838545097953280L);

			public static readonly LOS.TupleReference UI_Selected = GetTupleReference(2838828569989120L);

			public static readonly LOS.TupleReference ColorPicker_CopyFromPreset = GetTupleReference(2839218262773760L);

			public static readonly LOS.TupleReference ColorPicker_RemoveInfo = GetTupleReference(2844882489970688L);

			public static readonly LOS.TupleReference Shop_Buy = GetTupleReference(2872136079118336L);

			public static readonly LOS.TupleReference Shop_BuyCount = GetTupleReference(2872154328535040L);

			public static readonly LOS.TupleReference Shop = GetTupleReference(2872424458489856L);

			public static readonly LOS.TupleReference Shop_Money = GetTupleReference(2872751165411328L);

			public static readonly LOS.TupleReference Live2DTool_Hand = GetTupleReference(3176845411225600L);

			public static readonly LOS.TupleReference Live2DTool_Mouth = GetTupleReference(3176881821978624L);

			public static readonly LOS.TupleReference Live2DTool_Remove = GetTupleReference(3176929033064448L);

			public static readonly LOS.TupleReference Interactable_MoveShirt = GetTupleReference(3180725943537664L);

			public static readonly LOS.TupleReference Interactable_SuckNipple = GetTupleReference(3180833070256128L);

			public static readonly LOS.TupleReference Interactable_PlayBoobs = GetTupleReference(3181058551844864L);

			public static readonly LOS.TupleReference Interactable_FuckHer = GetTupleReference(3181141871693824L);

			public static readonly LOS.TupleReference Interactable_SuckTongue = GetTupleReference(3181232678375424L);

			public static readonly LOS.TupleReference Interactable_KissHer = GetTupleReference(3181320771342336L);

			public static readonly LOS.TupleReference Interactable_Headpat = GetTupleReference(3181493639581696L);

			public static readonly LOS.TupleReference Interactable_OpenHerMouth = GetTupleReference(3181570848329728L);

			public static readonly LOS.TupleReference Interactable_CloseMouth = GetTupleReference(3181733927063552L);

			public static readonly LOS.TupleReference Interactable_PlayTongue = GetTupleReference(3181659931152384L);

			public static readonly LOS.TupleReference Interactable_MovePanties = GetTupleReference(3181865342996480L);

			public static readonly LOS.TupleReference Interactable_MoveSkirt = GetTupleReference(3182005277560832L);

			public static readonly LOS.TupleReference Interactable_SuckRightNipple = GetTupleReference(3182163927109632L);

			public static readonly LOS.TupleReference Interactable_SuckLeftNipple = GetTupleReference(3182185431306240L);

			public static readonly LOS.TupleReference Interactable_PullBothNipples = GetTupleReference(3182187922722816L);

			public static readonly LOS.TupleReference Interactable_PullRightNipple = GetTupleReference(3182189734662144L);

			public static readonly LOS.TupleReference Interactable_PullLeftNipple = GetTupleReference(3182191475298304L);

			public static readonly LOS.TupleReference Interactable_FondleBoobsBra = GetTupleReference(3182192997830656L);

			public static readonly LOS.TupleReference Interactable_MovePullPanties = GetTupleReference(3182196890144768L);

			public static readonly LOS.TupleReference Interactable_SpreadPussy = GetTupleReference(3182198572060672L);

			public static readonly LOS.TupleReference Interactable_EatHerOut = GetTupleReference(3182200035872768L);

			public static readonly LOS.TupleReference Interactable_EatHerAssOut = GetTupleReference(3182201667457024L);

			public static readonly LOS.TupleReference Interactable_MoveBody = GetTupleReference(3182203118686208L);

			public static readonly LOS.TupleReference Shop_bigdaddyhurtshop = GetTupleReference(3185695065509888L);

			public static readonly LOS.TupleReference Shop_ConvenienceStore = GetTupleReference(3186228853608448L);

			public static readonly LOS.TupleReference Shop_Pharmacy = GetTupleReference(3186434319978496L);

			public static readonly LOS.TupleReference Shop_Annalie = GetTupleReference(3186567791120384L);

			public static readonly LOS.TupleReference UI_Hour = GetTupleReference(3190461971697664L);

			public static readonly LOS.TupleReference Minigame_Ready = GetTupleReference(3198124537286656L);

			public static readonly LOS.TupleReference Minigame_Info = GetTupleReference(3198278338220032L);

			public static readonly LOS.TupleReference Minigame_Shift = GetTupleReference(3198362492735488L);

			public static readonly LOS.TupleReference Minigame_Overtime = GetTupleReference(3198556772896768L);

			public static readonly LOS.TupleReference Minigame_Go = GetTupleReference(3198696980090880L);

			public static readonly LOS.TupleReference Minigame_Miss = GetTupleReference(3199298212597760L);

			public static readonly LOS.TupleReference Minigame_Good = GetTupleReference(3199320366911488L);

			public static readonly LOS.TupleReference Minigame_Okay = GetTupleReference(3199343792099328L);

			public static readonly LOS.TupleReference Minigame_Perfect = GetTupleReference(3199409651060736L);

			public static readonly LOS.TupleReference Minigame_TotalScore = GetTupleReference(3201999449231360L);

			public static readonly LOS.TupleReference Minigame_TotalScoreNoMiss = GetTupleReference(3202548034834432L);

			public static readonly LOS.TupleReference Loading_Module = GetTupleReference(3204295029559296L);

			public static readonly LOS.TupleReference Loading_GC = GetTupleReference(3204808756301824L);

			public static readonly LOS.TupleReference Loading_Finished = GetTupleReference(3204906265481216L);

			public static readonly LOS.TupleReference Save_Description = GetTupleReference(3209855691001856L);

			public static readonly LOS.TupleReference UI_ChooseItems = GetTupleReference(4247513603117056L);

			public static readonly LOS.TupleReference UI_ChooseItemsPrompt = GetTupleReference(4247654141661184L);

			public static readonly LOS.TupleReference Shop_OrderFinish = GetTupleReference(4261872622505984L);

			public static readonly LOS.TupleReference Shop_OrderFinishPrompt = GetTupleReference(4262224306507776L);

			public static readonly LOS.TupleReference UI_Finish = GetTupleReference(4262458403196928L);

			public static readonly LOS.TupleReference Shop_NotOrdered = GetTupleReference(4262970800345088L);

			public static readonly LOS.TupleReference UI_PluralDays = GetTupleReference(4263186110746624L);

			public static readonly LOS.TupleReference UI_PluralHours = GetTupleReference(4263253978779648L);

			public static readonly LOS.TupleReference UI_PluralMinutes = GetTupleReference(4263318080327680L);

			public static readonly LOS.TupleReference Shop_Delivery = GetTupleReference(4264050888151040L);

			public static readonly LOS.TupleReference Shop_OrderCollectPrompt = GetTupleReference(4264224402313216L);

			public static readonly LOS.TupleReference Shop_Collect = GetTupleReference(4264309144031232L);

			public static readonly LOS.TupleReference Shop_CanBeCollected = GetTupleReference(4264324285468672L);

			public static readonly LOS.TupleReference Shop_OrderPrompt = GetTupleReference(4264592087584768L);

			public static readonly LOS.TupleReference Shop_CustomOrders = GetTupleReference(4272462363496448L);

			public static readonly LOS.TupleReference Shop_CreateNewOrder = GetTupleReference(4272747366453248L);

			public static readonly LOS.TupleReference bangdotic_Mail = GetTupleReference(4275219812192256L);

			public static readonly LOS.TupleReference bangdotic_News = GetTupleReference(4275981124505600L);

			public static readonly LOS.TupleReference stonksdotic_Title = GetTupleReference(4289575077679104L);

			public static readonly LOS.TupleReference stonksdotic_Sell = GetTupleReference(4290444858888192L);

			public static readonly LOS.TupleReference stonksdotic_Buy = GetTupleReference(4290548839878656L);

			public static readonly LOS.TupleReference stonksdotic_Company = GetTupleReference(4290699692216320L);

			public static readonly LOS.TupleReference stonksdotic_CurrentPrice = GetTupleReference(4291006685908992L);

			public static readonly LOS.TupleReference stonksdotic_StockSummary = GetTupleReference(4291269228367872L);

			public static readonly LOS.TupleReference stonksdotic_NoStocks = GetTupleReference(4291489542574080L);

			public static readonly LOS.TupleReference stonksdotic_Income = GetTupleReference(4292488923582464L);

			public static readonly LOS.TupleReference stonksdotic_Loss = GetTupleReference(4292552890912768L);

			public static readonly LOS.TupleReference stonksdotic_IncomeInfo = GetTupleReference(4292699553140736L);

			public static readonly LOS.TupleReference stocksdotic_Portfolio = GetTupleReference(4293268715999232L);

			public static readonly LOS.TupleReference stocksdotic_ShortInfo = GetTupleReference(4293610207842304L);

			public static readonly LOS.TupleReference stocksdotic_PortfolioEmpty = GetTupleReference(4294177059639296L);

			public static readonly LOS.TupleReference stocksdotic_SellButton = GetTupleReference(4296638616965120L);

			public static readonly LOS.TupleReference stocksdotic_SellAllButton = GetTupleReference(4296818456137728L);

			public static readonly LOS.TupleReference stocksdotic_BuyButton = GetTupleReference(4297034282438656L);

			public static readonly LOS.TupleReference cocktwitchdotic_StartStreamPrompt = GetTupleReference(4310433892536320L);

			public static readonly LOS.TupleReference cocktwitchdotic_Summary = GetTupleReference(4310926626787328L);

			public static readonly LOS.TupleReference cocktwitchdotic_Followers = GetTupleReference(4311278495338496L);

			public static readonly LOS.TupleReference cocktwitchdotic_DaysAgo = GetTupleReference(4311379120885760L);

			public static readonly LOS.TupleReference cocktwitchdotic_LeaderboardEntry = GetTupleReference(4311636617596928L);

			public static readonly LOS.TupleReference cocktwitchdotic_stats = GetTupleReference(4312053221036032L);

			public static readonly LOS.TupleReference cocktwitchdotic_Leaderboard = GetTupleReference(4312147097948160L);

			public static readonly LOS.TupleReference cocktwitchdotic_Home = GetTupleReference(4312231097274368L);

			public static readonly LOS.TupleReference cocktwitchdotic_YourProfile = GetTupleReference(4312380057980928L);

			public static readonly LOS.TupleReference cocktwitchdotic_FollowersGraph = GetTupleReference(4312448764874752L);

			public static readonly LOS.TupleReference casinodotic_Tokens = GetTupleReference(4627441351790592L);

			public static readonly LOS.TupleReference casinodotic_Stake = GetTupleReference(4628268514676736L);

			public static readonly LOS.TupleReference casinodotic_NotEnoughTokens = GetTupleReference(4628376622862336L);

			public static readonly LOS.TupleReference casinodotic_Win = GetTupleReference(4628732987707392L);

			public static readonly LOS.TupleReference casinodotic_BuyTokensPrompt = GetTupleReference(4629025448136704L);

			public static readonly LOS.TupleReference casinodotic_Payout = GetTupleReference(4629333205192704L);

			public static readonly LOS.TupleReference casinodotic_MinimalStake = GetTupleReference(4629507973451776L);

			public static readonly LOS.TupleReference casinodotic_MinimalPayout = GetTupleReference(4629699871248384L);

			public static readonly LOS.TupleReference casinodotic_Single = GetTupleReference(4629979501301760L);

			public static readonly LOS.TupleReference casinodotic_Double = GetTupleReference(4630019972141056L);

			public static readonly LOS.TupleReference casinodotic_Triple = GetTupleReference(4630025105969152L);

			public static readonly LOS.TupleReference casinodotic_ClickToStopWheel = GetTupleReference(4630434809778176L);

			public static readonly LOS.TupleReference casinodotic_ClickToStopColumn = GetTupleReference(4630511871725568L);

			public static readonly LOS.TupleReference casinodotic_Vertical = GetTupleReference(4630644147490816L);

			public static readonly LOS.TupleReference casinodotic_Horizontal = GetTupleReference(4630694550441984L);

			public static readonly LOS.TupleReference casinodotic_Diagonal = GetTupleReference(4630732764745728L);

			public static readonly LOS.TupleReference casinodotic_Won = GetTupleReference(4631102643638272L);

			public static readonly LOS.TupleReference casinodotic_GotBack = GetTupleReference(4631196336001024L);

			public static readonly LOS.TupleReference casinodotic_PlayerBlackjack = GetTupleReference(4631424908791808L);

			public static readonly LOS.TupleReference casinodotic_DoubleBlackjack = GetTupleReference(4631509755367424L);

			public static readonly LOS.TupleReference casinodotic_OverLose = GetTupleReference(4631623106433024L);

			public static readonly LOS.TupleReference casinodotic_OverWin = GetTupleReference(4631672779575296L);

			public static readonly LOS.TupleReference casinodotic_CloserLose = GetTupleReference(4631737699012608L);

			public static readonly LOS.TupleReference casinodotic_Draw = GetTupleReference(4631809358696448L);

			public static readonly LOS.TupleReference casinodotic_CloserWin = GetTupleReference(4631841419956224L);

			public static readonly LOS.TupleReference casinodotic_DealersHand = GetTupleReference(4632078955974656L);

			public static readonly LOS.TupleReference casinodotic_PlayersHand = GetTupleReference(4632171352297472L);

			public static readonly LOS.TupleReference casinodotic_BuyTokens = GetTupleReference(4634206017556480L);

			public static readonly LOS.TupleReference casinodotic_Off = GetTupleReference(4634288825700352L);

			public static readonly LOS.TupleReference casinodotic_BuyMoreTokens = GetTupleReference(4635290396135424L);

			public static readonly LOS.TupleReference casinodotic_Payou = GetTupleReference(4635539038670848L);

			public static readonly LOS.TupleReference casinodotic_Slots = GetTupleReference(4635694110478336L);

			public static readonly LOS.TupleReference casinodotic_Blackjack = GetTupleReference(4635781226172416L);

			public static readonly LOS.TupleReference casinodotic_Book = GetTupleReference(4635880111083520L);

			public static readonly LOS.TupleReference casinodotic_Start = GetTupleReference(4645856011489280L);

			public static readonly LOS.TupleReference casinodotic_BlackjackHit = GetTupleReference(4645994750676992L);

			public static readonly LOS.TupleReference casinodotic_BlackjackStand = GetTupleReference(4646092150804480L);

			public static readonly LOS.TupleReference casinodotic_BlackjackDouble = GetTupleReference(4646147507228672L);

			public static readonly LOS.TupleReference casinodotic_leaf = GetTupleReference(4663911118671872L);

			public static readonly LOS.TupleReference casinodotic_10 = GetTupleReference(4664055159459840L);

			public static readonly LOS.TupleReference casinodotic_jack = GetTupleReference(4664176815247360L);

			public static readonly LOS.TupleReference casinodotic_queen = GetTupleReference(4664290736738304L);

			public static readonly LOS.TupleReference casinodotic_king = GetTupleReference(4664427777232896L);

			public static readonly LOS.TupleReference casinodotic_dollar = GetTupleReference(4664512145657856L);

			public static readonly LOS.TupleReference iamacheater_Button = GetTupleReference(4668579106394112L);

			public static readonly LOS.TupleReference WebsiteNotFound = GetTupleReference(4669150752280576L);

			public static readonly LOS.TupleReference peepeedotic_Text = GetTupleReference(4669471675256832L);

			public static readonly LOS.TupleReference duckdotic_Quack = GetTupleReference(4669867277815808L);

			public static readonly LOS.TupleReference duckdotic_QUACKexclamationexclamationexclamation = GetTupleReference(4669977160192000L);

			public static readonly LOS.TupleReference hotanimebabesdotic_Text = GetTupleReference(4670230953332736L);

			public static readonly LOS.TupleReference adredirectdotic_Text = GetTupleReference(4981858383040512L);

			public static readonly LOS.TupleReference adredirectdotic_Text2 = GetTupleReference(4982288534081536L);

			public static readonly LOS.TupleReference Room_OutsideWithBot = GetTupleReference(6830207121887232L);

			public static readonly LOS.TupleReference Room_OutsideAlone = GetTupleReference(6830209105793024L);

			public static readonly LOS.TupleReference bangdotic_email_Topic = GetTupleReference(6834585920737280L);

			public static readonly LOS.TupleReference bangdotic_email_From = GetTupleReference(7201200239677440L);

			public static readonly LOS.TupleReference bangdotic_email_NoEmailSelected = GetTupleReference(7201264261533696L);

			public static readonly LOS.TupleReference bangdotic_email_NoEmails = GetTupleReference(7201601403883520L);

			public static readonly LOS.TupleReference bangdotic_email_UnreadPostfix = GetTupleReference(7201702394335232L);

			public static readonly LOS.TupleReference Equipment_Own = GetTupleReference(7580205748981760L);

			public static readonly LOS.TupleReference Frick_Restore = GetTupleReference(7595071813586944L);

			public static readonly LOS.TupleReference Frick_RestorePrompt = GetTupleReference(7595105242189824L);

			public static readonly LOS.TupleReference Options_TranslationProgress = GetTupleReference(8322437082910720L);

			public static readonly LOS.TupleReference bangdotic_email_Delete = GetTupleReference(9397964988354560L);

			public static readonly LOS.TupleReference bangdotic_email_DeletePrompt = GetTupleReference(9398025864482816L);

			public static readonly LOS.TupleReference pixelglassesdotic_BuyNow = GetTupleReference(9403561913749504L);

			public static readonly LOS.TupleReference pixelglassesdotic_Discount = GetTupleReference(9404716110712832L);

			public static readonly LOS.TupleReference pixelglassesdotic_NotEnoughMoney = GetTupleReference(9405293699928064L);

			public static readonly LOS.TupleReference pixelglassesdotic_GoAway = GetTupleReference(9405459827920896L);

			public static readonly LOS.TupleReference pixelglassesdotic_StopWastingTime = GetTupleReference(9405532750090240L);

			public static readonly LOS.TupleReference pixelglassesdotic_Dumb = GetTupleReference(9405643618127872L);

			public static readonly LOS.TupleReference pixelglassesdotic_AlreadyOwn = GetTupleReference(9405699050049536L);

			public static readonly LOS.TupleReference pixelglassesdotic_What = GetTupleReference(9405799486853120L);

			public static readonly LOS.TupleReference pixelglassesdotic_WhatPrompt = GetTupleReference(9405825323765760L);

			public static readonly LOS.TupleReference pixelglassesdotic_SoulHarvest = GetTupleReference(9405969612017664L);

			public static readonly LOS.TupleReference pixelglassesdotic_SoulHarvestPrompt = GetTupleReference(9406023039062016L);

			public static readonly LOS.TupleReference pixelglassesdotic_SoulNotTaken = GetTupleReference(9406142497034240L);

			public static readonly LOS.TupleReference pixelglassesdotic_SoulNotTakenPrompt = GetTupleReference(9406180866527232L);

			public static readonly LOS.TupleReference pixelglassesdotic_ThrewOut = GetTupleReference(9406311892389888L);

			public static readonly LOS.TupleReference pixelglassesdotic_SoulTaken = GetTupleReference(9406417932783616L);

			public static readonly LOS.TupleReference pixelglassesdotic_Get = GetTupleReference(9406571473670144L);

			public static readonly LOS.TupleReference Equipment_CannotEquipWithoutSlot = GetTupleReference(28870155642368000L);

			public static readonly LOS.TupleReference Equipment_CannotRemove = GetTupleReference(28871745480716288L);

			public static readonly LOS.TupleReference Slot_LegL = GetTupleReference(28872469346283520L);

			public static readonly LOS.TupleReference Slot_LegR = GetTupleReference(28872529521963008L);

			public static readonly LOS.TupleReference Slot_ShoeL = GetTupleReference(28872531589754880L);

			public static readonly LOS.TupleReference Slot_ShoeR = GetTupleReference(28872604117659648L);

			public static readonly LOS.TupleReference Slot_StockingL = GetTupleReference(28876294375370752L);

			public static readonly LOS.TupleReference Slot_StockingR = GetTupleReference(28876223722319872L);

			public static readonly LOS.TupleReference Slot_ArmL = GetTupleReference(28876357243793408L);

			public static readonly LOS.TupleReference Slot_ArmR = GetTupleReference(28876521857642496L);

			public static readonly LOS.TupleReference Slot_Skin = GetTupleReference(28876616858628096L);

			public static readonly LOS.TupleReference Slot_Eyes = GetTupleReference(28876642221584384L);

			public static readonly LOS.TupleReference Slot_Flesh = GetTupleReference(28876771519393792L);

			public static readonly LOS.TupleReference Slot_Head = GetTupleReference(28876866503602176L);

			public static readonly LOS.TupleReference Slot_Teeth = GetTupleReference(28876952067403776L);

			public static readonly LOS.TupleReference Slot_Hair = GetTupleReference(28877006232645632L);

			public static readonly LOS.TupleReference Slot_Shirt = GetTupleReference(28877060272058368L);

			public static readonly LOS.TupleReference Slot_Skirt = GetTupleReference(28877154362880000L);

			public static readonly LOS.TupleReference Slot_Bra = GetTupleReference(28877218342793216L);

			public static readonly LOS.TupleReference Slot_Panties = GetTupleReference(28877297912934400L);

			public static readonly LOS.TupleReference Slot_HairAccessoryL = GetTupleReference(28877364140994560L);

			public static readonly LOS.TupleReference Slot_Anon_Glasses = GetTupleReference(28878144751300608L);

			public static readonly LOS.TupleReference UI_RentToday = GetTupleReference(28884866534502400L);

			public static readonly LOS.TupleReference UI_RentIn = GetTupleReference(28884898440572928L);

			public static readonly LOS.TupleReference Options_MusicPopup = GetTupleReference(29617154138337280L);

			public static readonly LOS.TupleReference Equipment_CannotChangeOrderWhileSortedPrompt = GetTupleReference(29734279280619520L);

			public static readonly LOS.TupleReference Equipment_SortDescending = GetTupleReference(29734444372619264L);

			public static readonly LOS.TupleReference Equipment_SortAscending = GetTupleReference(29734477662810112L);

			public static readonly LOS.TupleReference Equipment_SortQuality = GetTupleReference(29734557237145600L);

			public static readonly LOS.TupleReference Equipment_SortPrice = GetTupleReference(29734614137073664L);

			public static readonly LOS.TupleReference Equipment_SortSlot = GetTupleReference(29734664636493824L);

			public static readonly LOS.TupleReference Equipment_SortColor = GetTupleReference(29734729774034944L);

			public static readonly LOS.TupleReference Equipment_SortAlphabetically = GetTupleReference(29734773965221888L);

			public static readonly LOS.TupleReference Equipment_SortNoSort = GetTupleReference(29734845788483584L);

			public static readonly LOS.TupleReference UI_GameInstruction = GetTupleReference(31429975663841280L);

			public static readonly LOS.TupleReference Equipment_Can_tEatTitle = GetTupleReference(35048148581617664L);

			public static readonly LOS.TupleReference Equipment_Can_tEatPrompt = GetTupleReference(35048187899023360L);

			public static readonly LOS.TupleReference Equipment_Unequipped = GetTupleReference(35049316422651904L);

			public static readonly LOS.TupleReference Naming_TooShort = GetTupleReference(35060771767324672L);

			public static readonly LOS.TupleReference Naming_TooLong = GetTupleReference(35060841048838144L);

			public static readonly LOS.TupleReference Naming_AreYouSure = GetTupleReference(35060892756217856L);

			public static readonly LOS.TupleReference Options_MachineTranslation = GetTupleReference(35136683552673792L);

			public static readonly LOS.TupleReference Equipment_Remove = GetTupleReference(35201171735842816L);

			public static readonly LOS.TupleReference Frick_PullOutFirst = GetTupleReference(36665627488579584L);

			public static readonly LOS.TupleReference Frick_Request = GetTupleReference(36857017535328256L);

			public static readonly LOS.TupleReference Frick_MusicOn = GetTupleReference(37006396653072384L);

			public static readonly LOS.TupleReference Frick_MusicOff = GetTupleReference(37006427472818176L);

			public static readonly LOS.TupleReference Frick_Blowjob_Nevermind = GetTupleReference(37739390984548352L);

			public static readonly LOS.TupleReference Frick_Blowjob_SwallowCum = GetTupleReference(37739555132829696L);

			public static readonly LOS.TupleReference Frick_Blowjob_StopAfterCumming = GetTupleReference(37740242814136320L);

			public static readonly LOS.TupleReference Frick_Blowjob_ContinueAfterCumming = GetTupleReference(37740319083360256L);

			public static readonly LOS.TupleReference Frick_Blowjob_LetMeCum = GetTupleReference(37740430576349184L);

			public static readonly LOS.TupleReference Frick_Blowjob_DontLetMeCum = GetTupleReference(37740477418336256L);

			public static readonly LOS.TupleReference Frick_Blowjob_Fast = GetTupleReference(37740606896500736L);

			public static readonly LOS.TupleReference Frick_Blowjob_Slow = GetTupleReference(37740608968486912L);

			public static readonly LOS.TupleReference Frick_Blowjob_LickTip = GetTupleReference(37740610595876864L);

			public static readonly LOS.TupleReference Frick_Blowjob_Stop = GetTupleReference(37740717030535168L);

			public static readonly LOS.TupleReference Interactable_ThrustHips = GetTupleReference(37740994458578944L);

			public static readonly LOS.TupleReference Interactable_GrabHead = GetTupleReference(37741213023760384L);

			public static readonly LOS.TupleReference Interact_PlayGame = GetTupleReference(38039561060511744L);

			public static readonly LOS.TupleReference Interact_OldMaid = GetTupleReference(38042541298053120L);

			public static readonly LOS.TupleReference OldMaid_OpponentPicking = GetTupleReference(38117090177871872L);

			public static readonly LOS.TupleReference OldMaid_PlayerPicking = GetTupleReference(38117310177505280L);

			public static readonly LOS.TupleReference OldMaid_RemovePairs = GetTupleReference(38117454897770496L);

			public static readonly LOS.TupleReference OldMaid_Instruction = GetTupleReference(38117500364025856L);

			public static readonly LOS.TupleReference OldMaid_StopPrompt = GetTupleReference(38340543652851712L);

			public static readonly LOS.TupleReference Frick_FlacidFirstTime = GetTupleReference(41000833755516928L);

			public static readonly LOS.TupleReference Frick_StreamDonated = GetTupleReference(41001351890472960L);

			public static readonly LOS.TupleReference Options_KeepRunningWhenMinimzed = GetTupleReference(41278844509753344L);

			public static readonly LOS.TupleReference Blowjob_PreferInside = GetTupleReference(41349597364097024L);

			public static readonly LOS.TupleReference Blowjob_PreferOutside = GetTupleReference(41349599742267392L);

			public static readonly LOS.TupleReference Blowjob_Automatic = GetTupleReference(41349591252996096L);

			public static readonly LOS.TupleReference GameName = GetTupleReference(41361575591677952L);

			public static readonly LOS.TupleReference Slot_EyeShadow = GetTupleReference(41709500691779584L);

			public static readonly LOS.TupleReference Slot_NeckAccessory = GetTupleReference(41656432306556928L);

			public static readonly LOS.TupleReference Slot_Glasses = GetTupleReference(41667221952270336L);

			public static readonly LOS.TupleReference Slot_EyeMakeup = GetTupleReference(41709550939541504L);

			public static readonly LOS.TupleReference ladypartsdotic_PurchaseComplete = GetTupleReference(46297168025096192L);

			public static readonly LOS.TupleReference ladypartsdotic_PurchaseCompletePrompt = GetTupleReference(46297305749262336L);

			public static readonly LOS.TupleReference Loading_FailedProbablyBecauseOfModsPrompt = GetTupleReference(46305052108656640L);

			public static readonly LOS.TupleReference Loading_FailedNoIdeaWhy = GetTupleReference(46305053757018112L);

			public static readonly LOS.TupleReference Loading_IgnoreIfClosedGame = GetTupleReference(46345947193413632L);

			public static readonly LOS.TupleReference Slot_Ears = GetTupleReference(46801794386235392L);

			public static readonly LOS.TupleReference Slot_Mat = GetTupleReference(46801805190762496L);

			public static readonly LOS.TupleReference Slot_Plush = GetTupleReference(46801981087289344L);

			public static readonly LOS.TupleReference Slot_Banner = GetTupleReference(46802008576757760L);

			public static readonly LOS.TupleReference Slot_WallDecoration = GetTupleReference(46802026645819392L);

			public static readonly LOS.TupleReference Equipment_SetLoadedFailureTryingToLoadMod = GetTupleReference(47014647600910336L);

			public static readonly LOS.TupleReference Slot_Septum = GetTupleReference(47396080739815424L);

			public static readonly LOS.TupleReference SAVE_AutoSlot = GetTupleReference(76497017957650432L);

			public static readonly LOS.TupleReference SAVE_AutoSaveSlotEmpty = GetTupleReference(76497026111377408L);

			public static readonly LOS.TupleReference UI_AutoSave = GetTupleReference(76498690067271680L);

			public static readonly LOS.TupleReference Rewards_Money = GetTupleReference(77960481725091840L);

			public static readonly LOS.TupleReference Rewards_Item = GetTupleReference(77960487836192768L);

			public static readonly LOS.TupleReference UI_Reward = GetTupleReference(77960632552263680L);

			public static readonly LOS.TupleReference Requirements_OrGate = GetTupleReference(77968724316786688L);

			public static readonly LOS.TupleReference Requirements_Time = GetTupleReference(77969540075360256L);

			public static readonly LOS.TupleReference UI_Requirements = GetTupleReference(77971207130845184L);

			public static readonly LOS.TupleReference Reward_OnSuccess = GetTupleReference(77971499981344768L);

			public static readonly LOS.TupleReference Reward_OnFailure = GetTupleReference(77971552632442880L);

			public static readonly LOS.TupleReference Rewards_RespectGain = GetTupleReference(103325919220731904L);

			public static readonly LOS.TupleReference Frick_NewDonation = GetTupleReference(90675915951837184L);

			public static readonly LOS.TupleReference Reward_NoReward = GetTupleReference(77988725115457536L);

			public static readonly LOS.TupleReference Contract_OnAcceptBonus = GetTupleReference(77989245775384576L);

			public static readonly LOS.TupleReference UI_Contract = GetTupleReference(78005752894984192L);

			public static readonly LOS.TupleReference Contract_AreYouSureYouWantToAccept = GetTupleReference(78005823376068608L);

			public static readonly LOS.TupleReference Contract_RememberOnlyOne = GetTupleReference(78008542123921408L);

			public static readonly LOS.TupleReference Contract_NoContract = GetTupleReference(78013682130227200L);

			public static readonly LOS.TupleReference Contract_AbandonButton = GetTupleReference(78017373298102272L);

			public static readonly LOS.TupleReference Contract_CompleteButton = GetTupleReference(78018006554120192L);

			public static readonly LOS.TupleReference Contract_AreYouSureYouWantToAbandonCurrent = GetTupleReference(78019729460957184L);

			public static readonly LOS.TupleReference Contract_Failed = GetTupleReference(78019823845380096L);

			public static readonly LOS.TupleReference Contract_CompletionPossible = GetTupleReference(78019951792623616L);

			public static readonly LOS.TupleReference Contract_CompletePromptTitle = GetTupleReference(78022746088280064L);

			public static readonly LOS.TupleReference Contract_CompletePrompt = GetTupleReference(78022906289721344L);

			public static readonly LOS.TupleReference Requirements_TimeLeft = GetTupleReference(78029779415994368L);

			public static readonly LOS.TupleReference Requirements_OutOfTime = GetTupleReference(78040863174692864L);

			public static readonly LOS.TupleReference Requirements_AndGate = GetTupleReference(78189863643254784L);

			public static readonly LOS.TupleReference Requirements_StreamForXMinutesSimple = GetTupleReference(78214396286734336L);

			public static readonly LOS.TupleReference Requirements_StreamForXMinutesCompound = GetTupleReference(78214498862632960L);

			public static readonly LOS.TupleReference Requirements_Done = GetTupleReference(78215051860643840L);

			public static readonly LOS.TupleReference Requirements_AlwaysTrue = GetTupleReference(78224114250027008L);

			public static readonly LOS.TupleReference Requirements_ViewersRequired = GetTupleReference(78224465707536384L);

			public static readonly LOS.TupleReference Requirements_FollowersRequired = GetTupleReference(78224468010209280L);

			public static readonly LOS.TupleReference Requirements_SubsRequired = GetTupleReference(78224469469827072L);

			public static readonly LOS.TupleReference Requirements_InOneStreamCompound = GetTupleReference(78261032882933760L);

			public static readonly LOS.TupleReference Requirements_Viewership = GetTupleReference(78291985713991680L);

			public static readonly LOS.TupleReference Position_Missionary = GetTupleReference(78302377395752960L);

			public static readonly LOS.TupleReference Position_Doggy = GetTupleReference(78302451840454656L);

			public static readonly LOS.TupleReference Position_Blowjob = GetTupleReference(78302481456435200L);

			public static readonly LOS.TupleReference Requirements_Position = GetTupleReference(78303109276635136L);

			public static readonly LOS.TupleReference Contract_CurrentContract = GetTupleReference(78335096897015808L);

			public static readonly LOS.TupleReference Requirement_TimeBetween = GetTupleReference(79766729042825216L);

			public static readonly LOS.TupleReference Requirement_TimesAtLeast = GetTupleReference(79824472382509056L);

			public static readonly LOS.TupleReference Requirements_TimesDone = GetTupleReference(79787745735479296L);

			public static readonly LOS.TupleReference Requirement_TimeBeforeAfter = GetTupleReference(79817738016395264L);

			public static readonly LOS.TupleReference Requirement_TimesExactly = GetTupleReference(79824376790126592L);

			public static readonly LOS.TupleReference Requirement_TimesLess = GetTupleReference(79824873425080320L);

			public static readonly LOS.TupleReference Requirement_TimesAtLeastAndLess = GetTupleReference(79825643025338368L);

			public static readonly LOS.TupleReference Requirement_Or = GetTupleReference(79833939971018752L);

			public static readonly LOS.TupleReference Requirements_InOrderGate = GetTupleReference(79841721059250176L);

			public static readonly LOS.TupleReference Delivery_NumberTitle = GetTupleReference(80137031224528896L);

			public static readonly LOS.TupleReference Delivery_SenderTitle = GetTupleReference(80137078762770432L);

			public static readonly LOS.TupleReference Delivery_ArrivalTimeTitle = GetTupleReference(80137116549255168L);

			public static readonly LOS.TupleReference Delivery_ContentsTitle = GetTupleReference(80137169988882432L);

			public static readonly LOS.TupleReference Delivery_StatusTitle = GetTupleReference(80137278709436416L);

			public static readonly LOS.TupleReference Delivery_DeliveryTypeTitle = GetTupleReference(80484645652533248L);

			public static readonly LOS.TupleReference Delivery_PremiumDelivery = GetTupleReference(80485045986267136L);

			public static readonly LOS.TupleReference Delivery_StandardDelivery = GetTupleReference(80485094212374528L);

			public static readonly LOS.TupleReference Delivery_BuyPremium = GetTupleReference(80488503632072704L);

			public static readonly LOS.TupleReference Delivery_ReportedPrice = GetTupleReference(80489256945848320L);

			public static readonly LOS.TupleReference Delivery_NoDeliveries = GetTupleReference(80498249911111680L);

			public static readonly LOS.TupleReference Deliveries_BuyPremiumPrompt = GetTupleReference(80500201034223616L);

			public static readonly LOS.TupleReference Deliveries_DeliverySentPrompt = GetTupleReference(80504339507843072L);

			public static readonly LOS.TupleReference UI_NarrativeLog = GetTupleReference(80847789147164672L);

			public static readonly LOS.TupleReference NarrativeLog_GameOnFor = GetTupleReference(80852450835042304L);

			public static readonly LOS.TupleReference UI_ItemDescription = GetTupleReference(84754642570711040L);

			public static readonly LOS.TupleReference Mods_ModDescription = GetTupleReference(86983942145445888L);

			public static readonly LOS.TupleReference Mods_ModCreatorPopup = GetTupleReference(87031642605883392L);

			public static readonly LOS.TupleReference Live2DTool_CurrentTool = GetTupleReference(88106106160549888L);

			public static readonly LOS.TupleReference Frick_StreamInfo = GetTupleReference(88789582526418944L);

			public static readonly LOS.TupleReference Frick_ViewersTooltip = GetTupleReference(88794441371750400L);

			public static readonly LOS.TupleReference Frick_FollowersTooltip = GetTupleReference(88794615393423360L);

			public static readonly LOS.TupleReference Frick_SubsTooltip = GetTupleReference(88794619596115968L);

			public static readonly LOS.TupleReference UI_AreYouSureYouWantToQuit = GetTupleReference(90576802618937344L);

			public static readonly LOS.TupleReference UI_ColorSelection_CancelChange = GetTupleReference(91708102876729344L);

			public static readonly LOS.TupleReference Equipment_Modded = GetTupleReference(92402373011312640L);

			public static readonly LOS.TupleReference Equipment_Vanilla = GetTupleReference(92402405940793344L);

			public static readonly LOS.TupleReference Equipment_SearchForItemByName = GetTupleReference(93095892050907136L);

			public static readonly LOS.TupleReference Position_Cowgirl = GetTupleReference(93855167043997696L);

			public static readonly LOS.TupleReference Position_Interaction = GetTupleReference(94586810725699584L);

			public static readonly LOS.TupleReference Frick_LegsRequired = GetTupleReference(98489303369125888L);

			public static readonly LOS.TupleReference Frick_CowgirlRequest_Fast = GetTupleReference(98791638976827392L);

			public static readonly LOS.TupleReference Frick_CowgirlRequest_Slow = GetTupleReference(98791757939871744L);

			public static readonly LOS.TupleReference Frick_CowgirlRequest_Stop = GetTupleReference(98791785311899648L);

			public static readonly LOS.TupleReference Frick_CowgirlRequest_Pullout = GetTupleReference(99146374619992064L);

			public static readonly LOS.TupleReference Frick_CowgirlRequest_Insert = GetTupleReference(99146376071221248L);

			public static readonly LOS.TupleReference Requirement_WhileWearing = GetTupleReference(100693490689511424L);

			public static readonly LOS.TupleReference Requirement_ItemIncludedInOnAcceptBonus = GetTupleReference(100693834530164736L);

			public static readonly LOS.TupleReference Equipment_Contract = GetTupleReference(100713655892316160L);

			public static readonly LOS.TupleReference Equipment_ItemIsInCurrentContract = GetTupleReference(100715059465818112L);

			public static readonly LOS.TupleReference cocktracts_header_home = GetTupleReference(100726093731729408L);

			public static readonly LOS.TupleReference cocktracts_header_available = GetTupleReference(100727667761745920L);

			public static readonly LOS.TupleReference cocktracts_header_history = GetTupleReference(100727703828566016L);

			public static readonly LOS.TupleReference cocktracts_NoAvailableContracts = GetTupleReference(102504297911947264L);

			public static readonly LOS.TupleReference cocktracts_NoHistoryContracts = GetTupleReference(102504301103812608L);

			public static readonly LOS.TupleReference cocktracts_Main_Summary = GetTupleReference(102510519830581248L);

			public static readonly LOS.TupleReference Mods_OpenModFolder = GetTupleReference(103219101073018880L);

			public static readonly LOS.TupleReference Equipment_MissingItem = GetTupleReference(103223837037826048L);

			public static readonly LOS.TupleReference Equipment_MissingItemUnknownMod = GetTupleReference(103653687980593152L);

			public static readonly LOS.TupleReference Equipment_MissingItemKnownMod = GetTupleReference(103653268436946944L);

			public static readonly LOS.TupleReference Equipment_MissingItemVanilla = GetTupleReference(103653695874273280L);

			public static readonly LOS.TupleReference Rewards_RespectLoss = GetTupleReference(103326065513861120L);

			public static readonly LOS.TupleReference Requirement_OnlyProvidedItemCounts = GetTupleReference(103327646636445696L);

			public static readonly LOS.TupleReference Requirement_AnyItemCounts = GetTupleReference(103328220698251264L);

			public static readonly LOS.TupleReference Requirement_OnlyCertainItemWorks = GetTupleReference(103329181135147008L);

			public static readonly LOS.TupleReference Requirement_ColorsMustMatch = GetTupleReference(103329570618216448L);

			public static readonly LOS.TupleReference Contract_Partners = GetTupleReference(103597203649335296L);

			public static readonly LOS.TupleReference cocktracts_header_partners = GetTupleReference(103997420844486656L);

			public static readonly LOS.TupleReference Rewards_RespectBasicallyNone = GetTupleReference(104348586640019456L);

			public static readonly LOS.TupleReference Rewards_RespectTinyAmount = GetTupleReference(104348686762250240L);

			public static readonly LOS.TupleReference Rewards_RespectNormalAmount = GetTupleReference(104348751757185024L);

			public static readonly LOS.TupleReference Rewards_RespectDecentAmount = GetTupleReference(104348877410144256L);

			public static readonly LOS.TupleReference Rewards_RespectBigAmount = GetTupleReference(104348879599570944L);

			public static readonly LOS.TupleReference Rewards_RespectHugeAmount = GetTupleReference(104348883156340736L);

			public static readonly LOS.TupleReference Rewards_RespectGiganticAmount = GetTupleReference(104348952320413696L);

			public static readonly LOS.TupleReference cocktracts_Partner_ContractData = GetTupleReference(104682370048593920L);

			public static readonly LOS.TupleReference cocktracts_Partner_NotUnlocked = GetTupleReference(104683857613033472L);

			public static readonly LOS.TupleReference Contract_GenericTitle = GetTupleReference(105066058041135104L);

			public static readonly LOS.TupleReference Contract_GenericNarration = GetTupleReference(105067472167505920L);

			public static readonly LOS.TupleReference Contact_HasOtherRewardShort = GetTupleReference(105070972372914176L);

			public static readonly LOS.TupleReference Contract_NoRewards = GetTupleReference(105072268601593856L);

			public static readonly LOS.TupleReference Contract_RewardShort = GetTupleReference(105073804811571200L);

			public static readonly LOS.TupleReference cocktracts_NewContract = GetTupleReference(105817092137029632L);

			public static readonly LOS.TupleReference cocktracts_CurrentlyAvailableContracts = GetTupleReference(105848347574538240L);

			public static readonly LOS.TupleReference Contracts_GoldContract = GetTupleReference(119378088885620736L);

			public static readonly LOS.TupleReference WebGLWarningMightLoseSavePrompt = GetTupleReference(107250247087820800L);

			public static readonly LOS.TupleReference UI_DontShowTheWarningAgain = GetTupleReference(107251903930175488L);

			public static readonly LOS.TupleReference Options_DeviceDetected = GetTupleReference(110884429420695552L);

			public static readonly LOS.TupleReference Options_DriverDetected = GetTupleReference(110884559582531584L);

			public static readonly LOS.TupleReference Options_DriverNotDetected = GetTupleReference(110884689845030912L);

			public static readonly LOS.TupleReference Options_Connect = GetTupleReference(110884765585772544L);

			public static readonly LOS.TupleReference Options_Calibrate = GetTupleReference(110884795004620800L);

			public static readonly LOS.TupleReference Options_Connecting = GetTupleReference(110884842836463616L);

			public static readonly LOS.TupleReference Options_Calibrating = GetTupleReference(110885184496078848L);

			public static readonly LOS.TupleReference Options_WaitingForDriver = GetTupleReference(110885405749809152L);

			public static readonly LOS.TupleReference Options_CalibrationPrompt = GetTupleReference(110886562241056768L);

			public static readonly LOS.TupleReference Options_CalibrationAreYouSure = GetTupleReference(110886826775810048L);

			public static readonly LOS.TupleReference Options_CalibrationValues = GetTupleReference(110887030639955968L);

			public static readonly LOS.TupleReference Options_CalibrationComplete = GetTupleReference(110887155391139840L);

			public static readonly LOS.TupleReference Options_TryingToConnect = GetTupleReference(110887212823744512L);

			public static readonly LOS.TupleReference Rewards_Tokens = GetTupleReference(111323388428816384L);

			public static readonly LOS.TupleReference cocktracts_AcceptContract = GetTupleReference(111323846547476480L);

			public static readonly LOS.TupleReference cocktracts_RejectContract = GetTupleReference(111511411909070848L);

			public static readonly LOS.TupleReference Contract_AreYouSureYouWantToReject = GetTupleReference(111514635026173952L);

			public static readonly LOS.TupleReference Contract_ThisWillAngerThePartner = GetTupleReference(111514776562962432L);

			public static readonly LOS.TupleReference Contract_AvailableContract = GetTupleReference(111516167872319488L);

			public static readonly LOS.TupleReference Contract_AcceptedContract = GetTupleReference(111533608618823680L);

			public static readonly LOS.TupleReference Contract_ExpiredContract = GetTupleReference(111533613798789120L);

			public static readonly LOS.TupleReference Contract_RejectedContract = GetTupleReference(111533614545375232L);

			public static readonly LOS.TupleReference Contract_CompletedContract = GetTupleReference(111533615140966400L);

			public static readonly LOS.TupleReference Contract_FailedContract = GetTupleReference(111534575334592512L);

			public static readonly LOS.TupleReference Contract_AbandonedContract = GetTupleReference(111534577532407808L);

			public static readonly LOS.TupleReference Contract_AcceptedAt = GetTupleReference(111535180773986304L);

			public static readonly LOS.TupleReference Contract_FailedAt = GetTupleReference(111536244386566144L);

			public static readonly LOS.TupleReference Contract_CompletedAt = GetTupleReference(111536198479908864L);

			public static readonly LOS.TupleReference Contract_AbandonedAt = GetTupleReference(111536288699387904L);

			public static readonly LOS.TupleReference cocktracts_NoRelatedContracts = GetTupleReference(111665695493591040L);

			public static readonly LOS.TupleReference cocktracts_AbandonContract = GetTupleReference(111684337090301952L);

			public static readonly LOS.TupleReference cocktracts_CompleteContract = GetTupleReference(111684421555195904L);

			public static readonly LOS.TupleReference Slot_LowerBody = GetTupleReference(112797584455503872L);

			public static readonly LOS.TupleReference Slot_UpperBodyOuter = GetTupleReference(112799150298238976L);

			public static readonly LOS.TupleReference Slot_UpperBody = GetTupleReference(112801253397762048L);

			public static readonly LOS.TupleReference Interactable_MoveHoodie = GetTupleReference(113409013214744576L);

			public static readonly LOS.TupleReference Interactable_RemovePutOnPants = GetTupleReference(113472541338505216L);

			public static readonly LOS.TupleReference OpeningFolderNotSupportedOnThisPlatform = GetTupleReference(115600670286811136L);

			public static readonly LOS.TupleReference PathCopiedToClipboard = GetTupleReference(115602100355731456L);

			public static readonly LOS.TupleReference Mods_RelatedModNotFound = GetTupleReference(115604257586941952L);

			public static readonly LOS.TupleReference UI_ChangeColor = GetTupleReference(115604514160906240L);

			public static readonly LOS.TupleReference Mods_OpenModWorkingDirectory = GetTupleReference(115604614488657920L);

			public static readonly LOS.TupleReference Mods_ReloadTextures = GetTupleReference(115605370902024192L);

			public static readonly LOS.TupleReference Frick_CowgirlRequest_BotLoad = GetTupleReference(117175207793135616L);

			public static readonly LOS.TupleReference AddItem = GetTupleReference(118484088477585408L);

			public static readonly LOS.TupleReference LinkPrompt = GetTupleReference(119007219394940928L);

			public static readonly LOS.TupleReference CheckItOut = GetTupleReference(119007846246256640L);

			public static readonly LOS.TupleReference Requirement_WhileWearingMerch = GetTupleReference(119322262330281984L);

			public static readonly LOS.TupleReference Requirement_YouDontHaveAnyMerch = GetTupleReference(119322904511778816L);

			public static readonly LOS.TupleReference Contracts_GoldContractTooltip = GetTupleReference(119378233735909376L);

			public static readonly LOS.TupleReference Contracts_TimeLeftToAccept = GetTupleReference(119616393732558848L);

			public static readonly LOS.TupleReference UI_LessThanAnHour = GetTupleReference(119617920362745856L);

			public static readonly LOS.TupleReference Contract_FailedPrompt = GetTupleReference(119771359088611328L);

			public static readonly LOS.TupleReference ReloadedAllTexturesPrompt = GetTupleReference(123949974063812608L);

			public static readonly LOS.TupleReference Frick_LegsAndArmsRequired = GetTupleReference(121127165742682112L);

			public static readonly LOS.TupleReference NunRepair_NoValidItems = GetTupleReference(121783962669289472L);

			public static readonly LOS.TupleReference NunRepair_Repair = GetTupleReference(121814217182285824L);

			public static readonly LOS.TupleReference NunRepair_MaxQuality = GetTupleReference(121815228538032128L);

			public static readonly LOS.TupleReference NunRepair_RepairPossible = GetTupleReference(121815230467411968L);

			public static readonly LOS.TupleReference NunRepair_AlreadyRepaired = GetTupleReference(121817020021739520L);

			public static readonly LOS.TupleReference NunRepair_RepairPrompt = GetTupleReference(121820352937332736L);

			public static readonly LOS.TupleReference Cheat_ResetChurch = GetTupleReference(122323925517340672L);

			public static readonly LOS.TupleReference Cheat_ResetChurchPrompt = GetTupleReference(122324301981290496L);

			public static readonly LOS.TupleReference Cheat_ResetChurchDone = GetTupleReference(122324738528645120L);

			public static readonly LOS.TupleReference Mods_Preparing = GetTupleReference(122446762513141760L);

			public static readonly LOS.TupleReference Mods_Starting = GetTupleReference(122446862475988992L);

			public static readonly LOS.TupleReference Equipment_SetEquippingIterationLimitReached = GetTupleReference(122896743783075840L);

			public static readonly LOS.TupleReference cocktracts_header_help = GetTupleReference(123602581472546816L);

			public static readonly LOS.TupleReference cocktracts_help_description = GetTupleReference(123607893294321664L);

			public static readonly LOS.TupleReference cocktracts_header_description = GetTupleReference(123612752278065152L);

			public static readonly LOS.TupleReference NoFileAccessPrompt = GetTupleReference(131190198519074816L);

			public static readonly LOS.TupleReference NoFileAccessPrompt2 = GetTupleReference(131193600770707456L);

			public static readonly LOS.TupleReference Mods_Status_ActiveButGameOutdated = GetTupleReference(131208725485457408L);

			public static readonly LOS.TupleReference Mods_Status_ActiveButGameOutdatedDescription = GetTupleReference(131208764622508032L);

			public static readonly LOS.TupleReference Options_NoToy = GetTupleReference(142777605022420992L);

			public static readonly LOS.TupleReference Options_Other = GetTupleReference(142783772649984000L);

			public static readonly LOS.TupleReference Options_ToyLabel = GetTupleReference(142787469446615040L);

			public static readonly LOS.TupleReference Options_ToySettings = GetTupleReference(142807620820799488L);

			public static readonly LOS.TupleReference Options_ListOfSupportedToys = GetTupleReference(142813080831528960L);

			public static readonly LOS.TupleReference Options_CurrentToy = GetTupleReference(142813670638751744L);

			public static readonly LOS.TupleReference Toy_AGui = GetTupleReference(142816138198126592L);

			public static readonly LOS.TupleReference Options_DisconnectToy = GetTupleReference(142823013287333888L);

			public static readonly LOS.TupleReference Options_AlreadyConnectedPrompt = GetTupleReference(142848208458878976L);

			public static readonly LOS.TupleReference Options_CalibrationPrompt2 = GetTupleReference(142853923147747328L);

			public static readonly LOS.TupleReference Options_ToyCurrentInsertion = GetTupleReference(142921688344825856L);

			public static readonly LOS.TupleReference Options_ToySettingsTooltip = GetTupleReference(143124457693515776L);

			public static readonly LOS.TupleReference UI_ChangeQuality = GetTupleReference(148476157488926720L);

			public static readonly LOS.TupleReference RecentSave = GetTupleReference(161906883278278656L);

			public static readonly LOS.TupleReference Shop_TryOn = GetTupleReference(179782432629592064L);

			public static readonly LOS.TupleReference ChangingRoom = GetTupleReference(179879880014303232L);

			public static readonly LOS.TupleReference ReturnChangingRoomItem = GetTupleReference(180108504285974528L);

			public static readonly LOS.TupleReference ReturnChangingRoomItem_Prompt = GetTupleReference(180111010378420224L);

			public static readonly LOS.TupleReference ItemBorrowedFromAnnalieDescription = GetTupleReference(180123564106452992L);

			public static readonly LOS.TupleReference ItemBorrowedShort = GetTupleReference(180124503123038208L);

			public static readonly LOS.TupleReference ItemsReturnedPrompt = GetTupleReference(180168777147486208L);

			public static readonly LOS.TupleReference TryItemPrompt = GetTupleReference(180177057936707584L);

			public static readonly LOS.TupleReference ReturnToDefault = GetTupleReference(181229886776414208L);

			public static readonly LOS.TupleReference ReturnToDefaultPrompt = GetTupleReference(181230290838884352L);

			public static readonly LOS.TupleReference Undress = GetTupleReference(181265240959094784L);

			public static readonly LOS.TupleReference UndressPrompt = GetTupleReference(181265560342761472L);

			public static readonly LOS.TupleReference Unpack = GetTupleReference(181276072157777920L);

			public static readonly LOS.TupleReference UnpackPrompt = GetTupleReference(181276113471672320L);

			public static readonly LOS.TupleReference Options_CalibrationWrongPrompt = GetTupleReference(182153022174793728L);

			public static readonly LOS.TupleReference Options_NeedsCalibration = GetTupleReference(182155339737468928L);

			public static readonly LOS.TupleReference Slot_HeartPupils = GetTupleReference(182945513559953408L);

			public static readonly LOS.TupleReference DebugModePrompt = GetTupleReference(183616489851326464L);

			public static readonly LOS.TupleReference DebugModeEnabledPrompt = GetTupleReference(183631250118807552L);

			public static readonly LOS.TupleReference Options_DisableDebugButton = GetTupleReference(183633597637201920L);

			public static readonly LOS.TupleReference Options_DisableDebugPrompt = GetTupleReference(183634906121637888L);

			public static readonly LOS.TupleReference Achievements_EstimatedTimePlayed = GetTupleReference(183672075477454848L);

			public static readonly LOS.TupleReference ThankYou = GetTupleReference(183678134879391744L);

			public static readonly LOS.TupleReference ShillingPrompt = GetTupleReference(183678261178273792L);

			public static readonly LOS.TupleReference ClickToOpen = GetTupleReference(183687878033674240L);

			public static readonly LOS.TupleReference Interact_Action = GetTupleReference(185142908448280576L);

			public static readonly LOS.TupleReference Cuddle_GetCloser = GetTupleReference(185154362496913408L);

			public static readonly LOS.TupleReference Cuddle_MoveAway = GetTupleReference(185154407791202304L);

			public static readonly LOS.TupleReference Slot_Tail = GetTupleReference(186677922902183936L);

			public static readonly LOS.TupleReference bangdotic_DeleteSpam = GetTupleReference(187383148432986112L);

			public static readonly LOS.TupleReference bangdotic_DeleteSpamPrompt = GetTupleReference(187383311713046528L);

			public static readonly LOS.TupleReference Live2DTool_TabTip = GetTupleReference(187424961176932352L);

			public static readonly LOS.TupleReference Interactable_MoveTail = GetTupleReference(189924979553210368L);

			public static readonly LOS.TupleReference PlayerName = GetTupleReference(193471983856259072L);

			public static readonly LOS.TupleReference PlayerName_DefaultMaster = GetTupleReference(193471986729357312L);

			public static readonly LOS.TupleReference PlayerName_Prompt = GetTupleReference(193471985475260416L);

			public static readonly LOS.TupleReference BotName = GetTupleReference(193485218361774080L);

			public static readonly LOS.TupleReference BotName_Prompt = GetTupleReference(193485220102410240L);

			public static readonly LOS.TupleReference BotName_Default = GetTupleReference(193485221486530560L);

			public static readonly LOS.TupleReference Interact_Other = GetTupleReference(193514289871327232L);

			public static readonly LOS.TupleReference ChangeBotName = GetTupleReference(193515065645600768L);

			public static readonly LOS.TupleReference ChangePlayerName = GetTupleReference(193515119747928064L);

			public static readonly LOS.TupleReference Frick_CowgirlRequest_BegStop = GetTupleReference(203832649683177472L);

			public static readonly LOS.TupleReference Interactable_MoveBlanket = GetTupleReference(205536974418731008L);

			public static readonly LOS.TupleReference Interactable_RubHerFace = GetTupleReference(205536977090502656L);

			public static readonly LOS.TupleReference Interactable_HoldHerHand = GetTupleReference(205537104119193600L);

			public static readonly LOS.TupleReference Cuddle_GoToSleep = GetTupleReference(205538471843979264L);

			public static readonly LOS.TupleReference Cuddle_AskToStopHeadpats = GetTupleReference(205545741474586624L);

			public static readonly LOS.TupleReference Cuddle_EndPrompt = GetTupleReference(207592949275418624L);

			public static readonly LOS.TupleReference Slot_Lips = GetTupleReference(207911663787040768L);

			public static readonly LOS.TupleReference Equipment_FromMod = GetTupleReference(207959451258314752L);

			public static readonly LOS.TupleReference Slot_PersonalityModule = GetTupleReference(208385084266373120L);

			public static readonly LOS.TupleReference Equipment_PersonalityModuleInfo = GetTupleReference(208756289724997632L);

			public static readonly LOS.TupleReference Equipment_PersonalityModules = GetTupleReference(210961474161467392L);

			public static readonly LOS.TupleReference Equipment_HighQuality = GetTupleReference(210961814600540160L);

			public static readonly LOS.TupleReference Equipment_Under20k = GetTupleReference(210962206369505280L);

			public static readonly LOS.TupleReference Cuddle_AskForHeadpats = GetTupleReference(214844375504510976L);

			public static readonly LOS.TupleReference Cuddle_AskToSaySomethingNice = GetTupleReference(214855388001984512L);

			public static readonly LOS.TupleReference Options_DeviceNotDetected = GetTupleReference(214897433626271744L);

			public static readonly LOS.TupleReference Options_WaitingForDevice = GetTupleReference(110885475282980864L);

			public static readonly LOS.TupleReference Mods_ApproximateExtractedSize = GetTupleReference(216000683855306752L);

			public static readonly LOS.TupleReference Mods_MajorVersionBehind = GetTupleReference(216003925590925312L);

			public static readonly LOS.TupleReference Mods_MinorVersionBehind = GetTupleReference(216004093535051776L);

			public static readonly LOS.TupleReference Mods_PatchVersionBehind = GetTupleReference(216004097012129792L);

			public static readonly LOS.TupleReference Mods_MajorVersionAhead = GetTupleReference(216004209444642816L);

			public static readonly LOS.TupleReference Mods_MinorVersionAhead = GetTupleReference(216004239974981632L);

			public static readonly LOS.TupleReference Mods_PatchVersionAhead = GetTupleReference(216004242319597568L);

			public static readonly LOS.TupleReference Mods_ExactlySameVersion = GetTupleReference(216005188114178048L);

			public static readonly LOS.TupleReference Mods_UpdateYourGame = GetTupleReference(216005754861117440L);

			public static readonly LOS.TupleReference New_Entry_1 = GetTupleReference(216018123439988736L);

			public static readonly LOS.TupleReference OutdatedVersionPrompt = GetTupleReference(217497412949762048L);

			public static readonly LOS.TupleReference DownloadLatestPublicItchIo = GetTupleReference(217497671214030848L);

			public static readonly LOS.TupleReference Equipment_Medicine = GetTupleReference(217577985613488128L);

			public static readonly LOS.TupleReference Error_GameCannotSave = GetTupleReference(218218778581786624L);

			public static readonly LOS.TupleReference AGUI_UseDriverDepth = GetTupleReference(233121712609038336L);

			public static readonly LOS.TupleReference AGUI_UseDriverDepth_Tooltip = GetTupleReference(233121787800326144L);

			public static readonly LOS.TupleReference SecretRoomPrompt = GetTupleReference(243536364995338240L);

			public static readonly LOS.TupleReference Room_Cook = GetTupleReference(246153693377847296L);

			public static readonly LOS.TupleReference Interactable_MoveLegs = GetTupleReference(252905369350692864L);

			public static readonly LOS.TupleReference Cooking_SelectedSong = GetTupleReference(263867424090087424L);

			public static readonly LOS.TupleReference Cooking_Results = GetTupleReference(263867923883352064L);

			public static readonly LOS.TupleReference FishingGear = GetTupleReference(268938463266463744L);

			public static readonly LOS.TupleReference FishingProgress = GetTupleReference(268938574251941888L);

			public static readonly LOS.TupleReference FishingFishingRods = GetTupleReference(268942624322215936L);

			public static readonly LOS.TupleReference FishingResults = GetTupleReference(269257882794450944L);

			public static readonly LOS.TupleReference Fishing = GetTupleReference(269289693771718656L);

			public static readonly LOS.TupleReference Fishing_StartFishing = GetTupleReference(269511379708272640L);

			public static readonly LOS.TupleReference Fishing_FishingSpot_Cleanliness = GetTupleReference(269526879226028032L);

			public static readonly LOS.TupleReference Fishing_FishingSpot_FishQuantity = GetTupleReference(269526934053969920L);

			public static readonly LOS.TupleReference Fishing_FishingSpot_FishSize = GetTupleReference(269526988798025728L);

			public static readonly LOS.TupleReference Fishing_FishingLocation = GetTupleReference(269533393185660928L);

			public static readonly LOS.TupleReference SelectItem = GetTupleReference(269542812636143616L);

			public static readonly LOS.TupleReference NoValidItemsFound = GetTupleReference(269564918052675584L);

			public static readonly LOS.TupleReference FishingFishingRod = GetTupleReference(269569491827941376L);

			public static readonly LOS.TupleReference FishingBait = GetTupleReference(269572895136538624L);

			public static readonly LOS.TupleReference FishingNoFishinRodEquipped = GetTupleReference(269573447031447552L);

			public static readonly LOS.TupleReference FishingNoBaitEquipped = GetTupleReference(269573606360473600L);

			public static readonly LOS.TupleReference UI_Change = GetTupleReference(269575506841559040L);

			public static readonly LOS.TupleReference FishingNoFishingRodEquippedPrompt = GetTupleReference(273571584536469504L);

			public static readonly LOS.TupleReference Fishing_FishingSpot_Price = GetTupleReference(273929969140228096L);

			public static readonly LOS.TupleReference CookingSong_BPM = GetTupleReference(273953207970947072L);

			public static readonly LOS.TupleReference CookingSong_Difficulty = GetTupleReference(273953175725137920L);

			public static readonly LOS.TupleReference CookingSong_Length = GetTupleReference(273953146817994752L);

			public static readonly LOS.TupleReference CookingSong_MinBPM = GetTupleReference(273955578268606464L);

			public static readonly LOS.TupleReference CookingSong_MaxBPM = GetTupleReference(273955582001537024L);

			public static readonly LOS.TupleReference CookingSong_MissingIngredient = GetTupleReference(274253650940911616L);

			public static readonly LOS.TupleReference Fishing_TimeEndsAt = GetTupleReference(276192940222570496L);

			public static readonly LOS.TupleReference Fishing_TimeEnd = GetTupleReference(276193668341161984L);

			public static readonly LOS.TupleReference Rewards_ItemDelivery = GetTupleReference(276482683070046208L);

			public static readonly LOS.TupleReference Fishing_FishingResults = GetTupleReference(276485746140897280L);

			public static readonly LOS.TupleReference Fishing_FishingResultsNothing = GetTupleReference(276485998394728448L);

			public static readonly LOS.TupleReference Interactable_Fingering = GetTupleReference(277099352406614016L);

			public static readonly LOS.TupleReference Interactable_ThumbInMouth = GetTupleReference(277130913491443712L);

			public static readonly LOS.TupleReference Interactable_PlayBoobL = GetTupleReference(279351619797114880L);

			public static readonly LOS.TupleReference Interactable_PlayBoobR = GetTupleReference(279351690819264512L);

			public static readonly LOS.TupleReference Interactable_ClitRub = GetTupleReference(280796031672639488L);

			public static readonly LOS.TupleReference Cooking_Input = GetTupleReference(281130209593253888L);

			public static readonly LOS.TupleReference Cooking_CookingVisualizationTitle = GetTupleReference(281132009436209152L);

			public static readonly LOS.TupleReference Interactable_GrabAss = GetTupleReference(281169972673110016L);

			public static readonly LOS.TupleReference Interactable_GrabHea = GetTupleReference(281170013890535424L);

			public static readonly LOS.TupleReference Interactable_MoveDress = GetTupleReference(283365635473268736L);

			public static readonly LOS.TupleReference FishingJournal = GetTupleReference(285858811819843584L);

			public static readonly LOS.TupleReference Fishing_CaughtXTimes = GetTupleReference(285884661038571520L);

			public static readonly LOS.TupleReference Fishing_MinWeight = GetTupleReference(285884766244298752L);

			public static readonly LOS.TupleReference Fishing_MaxWeight = GetTupleReference(285884767963963392L);

			public static readonly LOS.TupleReference Fishing_NeverCaught = GetTupleReference(285884902534012928L);

			public static readonly LOS.TupleReference Fishing_LastCaught = GetTupleReference(285884960692232192L);

			public static readonly LOS.TupleReference Fishing_BiggerThanPercent = GetTupleReference(285900715160756224L);

			public static readonly LOS.TupleReference Fishing_SmallerThanPercent = GetTupleReference(285900763114233856L);

			public static readonly LOS.TupleReference FishItemWeightItemInfo = GetTupleReference(286219092781686784L);

			public static readonly LOS.TupleReference FishSize_10Percentile = GetTupleReference(286219584098263040L);

			public static readonly LOS.TupleReference FishSize_20Percentile = GetTupleReference(286223013008891904L);

			public static readonly LOS.TupleReference FishSize_30Percentile = GetTupleReference(286223014862774272L);

			public static readonly LOS.TupleReference FishSize_40Percentile = GetTupleReference(286223015605166080L);

			public static readonly LOS.TupleReference FishSize_60Percentile = GetTupleReference(286223017555517440L);

			public static readonly LOS.TupleReference FishSize_70Percentile = GetTupleReference(286223019375845376L);

			public static readonly LOS.TupleReference FishSize_80Percentile = GetTupleReference(286223081359269888L);

			public static readonly LOS.TupleReference FishSize_90Percentile = GetTupleReference(286223083179597824L);

			public static readonly LOS.TupleReference FishSize_100Percentile = GetTupleReference(286223106114052096L);

			public static readonly LOS.TupleReference Item_MarkAsFavourite = GetTupleReference(286958548862099456L);

			public static readonly LOS.TupleReference Item_UnmarkAsFavourite = GetTupleReference(286958671264473088L);

			public static readonly LOS.TupleReference Item_Favourite = GetTupleReference(286958768836567040L);

			public static readonly LOS.TupleReference Cooking_ScoreLabel = GetTupleReference(287281353327632384L);

			public static readonly LOS.TupleReference Cooking_AccuracyLabel = GetTupleReference(287349996405121024L);

			public static readonly LOS.TupleReference FishingWebsite_TipsFromThePros = GetTupleReference(288460403148578816L);

			public static readonly LOS.TupleReference FishingWebsite_Name = GetTupleReference(328038276087668736L);

			public static readonly LOS.TupleReference FishingWebsite_Tip1 = GetTupleReference(288779861457100800L);

			public static readonly LOS.TupleReference FishingWebsite_NoTips = GetTupleReference(288780499117137920L);

			public static readonly LOS.TupleReference MillilitersDisplay = GetTupleReference(291360229049753600L);

			public static readonly LOS.TupleReference CommandArgumentsExpectationInfo = GetTupleReference(291364104926388224L);

			public static readonly LOS.TupleReference ProvidedArgumentsCountError = GetTupleReference(291364817899347968L);

			public static readonly LOS.TupleReference ValueNotFound = GetTupleReference(291368438418366464L);

			public static readonly LOS.TupleReference ClosestMatch = GetTupleReference(291368638771879936L);

			public static readonly LOS.TupleReference ClsCommandDescription = GetTupleReference(291674740382228480L);

			public static readonly LOS.TupleReference CowsayCommandDescription = GetTupleReference(291674841934716928L);

			public static readonly LOS.TupleReference HelpCommandDescription = GetTupleReference(291675064895528960L);

			public static readonly LOS.TupleReference SummaryCommandDescription = GetTupleReference(291675429393129472L);

			public static readonly LOS.TupleReference ViewVariableCommandDescription = GetTupleReference(291675631189483520L);

			public static readonly LOS.TupleReference ExitCommandDescription = GetTupleReference(291676145096581120L);

			public static readonly LOS.TupleReference SummaryCommandName = GetTupleReference(291676700569231360L);

			public static readonly LOS.TupleReference SummaryCommandMasterName = GetTupleReference(291676739815333888L);

			public static readonly LOS.TupleReference SummaryCommandLust = GetTupleReference(291676809012961280L);

			public static readonly LOS.TupleReference SummaryCommandSympathy = GetTupleReference(291676887047987200L);

			public static readonly LOS.TupleReference SummaryCommandTimeSinceLastHeadpat = GetTupleReference(291676985823846400L);

			public static readonly LOS.TupleReference HelpCommandDetailedInfoNotProvided = GetTupleReference(291677830095298560L);

			public static readonly LOS.TupleReference CommandNotFound = GetTupleReference(291678442090389504L);

			public static readonly LOS.TupleReference HelpCommandDetailedInfoHeader = GetTupleReference(291678804859936768L);

			public static readonly LOS.TupleReference CurrentDateInfo = GetTupleReference(291685365296652288L);

			public static readonly LOS.TupleReference DateCommandDescription = GetTupleReference(291686735630295040L);

			public static readonly LOS.TupleReference CurrentDayOfTheWeek = GetTupleReference(291691209128140800L);

			public static readonly LOS.TupleReference CowsayJunFlagDescription = GetTupleReference(291707417571352576L);

			public static readonly LOS.TupleReference CowsayAnnalieFlagDescription = GetTupleReference(291707485191921664L);

			public static readonly LOS.TupleReference CowsayGayFlagDescription = GetTupleReference(291707563273084928L);

			public static readonly LOS.TupleReference DateCommandDayOfWeekFlagDescription = GetTupleReference(291708251470290944L);

			public static readonly LOS.TupleReference CommandFlagsNone = GetTupleReference(291710365869268992L);

			public static readonly LOS.TupleReference CommandFlagsList = GetTupleReference(291710458894737408L);

			public static readonly LOS.TupleReference HelpCommandCommandNameHeader = GetTupleReference(291712261178777600L);

			public static readonly LOS.TupleReference HelpCommandDescriptionHeader = GetTupleReference(291712367357583360L);

			public static readonly LOS.TupleReference CommandInputNothing = GetTupleReference(291714070983204864L);

			public static readonly LOS.TupleReference StatusAppDisconnect = GetTupleReference(291714617454878720L);

			public static readonly LOS.TupleReference CommandNotAWholeNumber = GetTupleReference(291763400070914048L);

			public static readonly LOS.TupleReference CommandNotWithinRange = GetTupleReference(291763485726990336L);

			public static readonly LOS.TupleReference SnakeCommandDescription = GetTupleReference(291766193762590720L);

			public static readonly LOS.TupleReference SnakeCommandHeader = GetTupleReference(291786284394864640L);

			public static readonly LOS.TupleReference SnakeCommandScore = GetTupleReference(291786780740411392L);

			public static readonly LOS.TupleReference CubeCommandInputGradient = GetTupleReference(292070395239981056L);

			public static readonly LOS.TupleReference CubeCommandGradientCustom = GetTupleReference(292071315659022336L);

			public static readonly LOS.TupleReference CubeCommandDescription = GetTupleReference(292081865256329216L);

			public static readonly LOS.TupleReference StyleCommandDescription = GetTupleReference(292088042878918656L);

			public static readonly LOS.TupleReference StyleCommandHeader = GetTupleReference(292088106003193856L);

			public static readonly LOS.TupleReference StyleCommandInfo = GetTupleReference(292088243299540992L);

			public static readonly LOS.TupleReference StyleCommandNextTextColor = GetTupleReference(292090165658116096L);

			public static readonly LOS.TupleReference StyleCommandNextBackgroundColor = GetTupleReference(292090204983910400L);

			public static readonly LOS.TupleReference CommandClosed = GetTupleReference(292098556631703552L);

			public static readonly LOS.TupleReference StyleCommandDefaultColors = GetTupleReference(292099808820191232L);

			public static readonly LOS.TupleReference StyleCommandSaving = GetTupleReference(292103942780739584L);

			public static readonly LOS.TupleReference StyleCommandSavingDone = GetTupleReference(292104049936818176L);

			public static readonly LOS.TupleReference StyleCommandRandomColors = GetTupleReference(292112160504406016L);

			public static readonly LOS.TupleReference EightBallCommandYes = GetTupleReference(292117632015917056L);

			public static readonly LOS.TupleReference EightBallCommandNo = GetTupleReference(292117690597761024L);

			public static readonly LOS.TupleReference EightBallCommandMaybe = GetTupleReference(292117708234809344L);

			public static readonly LOS.TupleReference EightBallCommandDefinitely = GetTupleReference(292117767928143872L);

			public static readonly LOS.TupleReference EightBallCommandIDontThinkSo = GetTupleReference(292117842712584192L);

			public static readonly LOS.TupleReference EightBallCommandAbsolutely = GetTupleReference(292117876732583936L);

			public static readonly LOS.TupleReference EightBallCommandNotLikely = GetTupleReference(292117950413922304L);

			public static readonly LOS.TupleReference EightBallCommandTryAgainLater = GetTupleReference(292118014037319680L);

			public static readonly LOS.TupleReference EightBallCommandDescription = GetTupleReference(292119612482379776L);

			public static readonly LOS.TupleReference EightBallCommandText = GetTupleReference(292119713296670720L);

			public static readonly LOS.TupleReference CurrentlyLoading = GetTupleReference(292459566337089536L);

			public static readonly LOS.TupleReference LogExportSuccessful = GetTupleReference(292497579461042176L);

			public static readonly LOS.TupleReference LogExportFailed = GetTupleReference(292497790078017536L);

			public static readonly LOS.TupleReference ErrorOccured = GetTupleReference(294207689531056128L);

			public static readonly LOS.TupleReference PlayVideoLoading = GetTupleReference(294219412971446272L);

			public static readonly LOS.TupleReference PlayVideoLoadingCancelled = GetTupleReference(294219519695511552L);

			public static readonly LOS.TupleReference PlayVideoCurrentGradient = GetTupleReference(294219566034182144L);

			public static readonly LOS.TupleReference PlayVideoVideoSize = GetTupleReference(294219611206836224L);

			public static readonly LOS.TupleReference PlayVideoTurnOnLooping = GetTupleReference(294219798847414272L);

			public static readonly LOS.TupleReference PlayVideoTurnOffLooping = GetTupleReference(294219802634870784L);

			public static readonly LOS.TupleReference PlayVideoSkip = GetTupleReference(294220167136665600L);

			public static readonly LOS.TupleReference PlayVideoDescription = GetTupleReference(294220377594257408L);

			public static readonly LOS.TupleReference EasterEgg = GetTupleReference(294220554702938112L);

			public static readonly LOS.TupleReference PlayVideoInvalidPath = GetTupleReference(294220909868212224L);

			public static readonly LOS.TupleReference ExpensiveItemModifier = GetTupleReference(294310927785779200L);

			public static readonly LOS.TupleReference ExpensiveItemModifierDescription = GetTupleReference(294311088398262272L);

			public static readonly LOS.TupleReference PopularItemModifier = GetTupleReference(294601542884909056L);

			public static readonly LOS.TupleReference PopularItemModifierDescription = GetTupleReference(294601693305233408L);

			public static readonly LOS.TupleReference StreamersItemModifier = GetTupleReference(294607100912328704L);

			public static readonly LOS.TupleReference StreamersItemModifierDescription = GetTupleReference(294607140934377472L);

			public static readonly LOS.TupleReference ItemModifierInfo = GetTupleReference(294625452372254720L);

			public static readonly LOS.TupleReference ItemModifierInfoDescription = GetTupleReference(294625865280512000L);

			public static readonly LOS.TupleReference ItemModifierLevel = GetTupleReference(294627986285518848L);

			public static readonly LOS.TupleReference ItemModifierListHeader = GetTupleReference(294630602709131264L);

			public static readonly LOS.TupleReference ItemModifierCursed = GetTupleReference(294641044772872192L);

			public static readonly LOS.TupleReference ItemModifierCursedDescription = GetTupleReference(294641082420944896L);

			public static readonly LOS.TupleReference ItemModifierFastFishing = GetTupleReference(294830347943665664L);

			public static readonly LOS.TupleReference ItemModifierFastFishingDescription = GetTupleReference(294830370198642688L);

			public static readonly LOS.TupleReference ItemModifierWideFishing = GetTupleReference(294830600080056320L);

			public static readonly LOS.TupleReference ItemModifierWideFishingDescription = GetTupleReference(294830603439693824L);

			public static readonly LOS.TupleReference ClickAnywhereToStartFishing = GetTupleReference(294878476382494720L);

			public static readonly LOS.TupleReference ThrowMinigameExplain = GetTupleReference(294889799619174400L);

			public static readonly LOS.TupleReference CookingSong_MissingIngredients = GetTupleReference(294910130442461184L);

			public static readonly LOS.TupleReference CookingSong_ReadyToCook = GetTupleReference(294911354013212672L);

			public static readonly LOS.TupleReference CookingSong_IngredientList = GetTupleReference(294911821199958016L);

			public static readonly LOS.TupleReference CookingSong_ModifierEffects = GetTupleReference(294915607926661120L);

			public static readonly LOS.TupleReference CookingSong_MoreScore = GetTupleReference(294915737463545856L);

			public static readonly LOS.TupleReference CookingSong_LessScore = GetTupleReference(294915738977689600L);

			public static readonly LOS.TupleReference CookingSong_FasterSong = GetTupleReference(294915915000045568L);

			public static readonly LOS.TupleReference CookingSong_SlowerSong = GetTupleReference(294915916690350080L);

			public static readonly LOS.TupleReference CookingSong_ModifierEffectsNone = GetTupleReference(294917261694590976L);

			public static readonly LOS.TupleReference CookingSong_Modifiers = GetTupleReference(294918534284173312L);

			public static readonly LOS.TupleReference CookingSong_ModifierSlowMode = GetTupleReference(294918660834713600L);

			public static readonly LOS.TupleReference CookingSong_ModifierFastMode = GetTupleReference(294918688034775040L);

			public static readonly LOS.TupleReference CookingSong_ModifierUltraFastMode = GetTupleReference(294918750383104000L);

			public static readonly LOS.TupleReference CookingSong_ModifierInsaneMode = GetTupleReference(294918789377548288L);

			public static readonly LOS.TupleReference CookingSong_NoModifiers = GetTupleReference(294918963223060480L);

			public static readonly LOS.TupleReference CookingSong_NoActiveModifiers = GetTupleReference(294919766151258112L);

			public static readonly LOS.TupleReference CookingSong_ActiveModifiers = GetTupleReference(294919900985548800L);

			public static readonly LOS.TupleReference CookingSong_ModifierPractice = GetTupleReference(294923164196003840L);

			public static readonly LOS.TupleReference CookingSong_ModifierPracticeInfo = GetTupleReference(294924161282727936L);

			public static readonly LOS.TupleReference CookingSong_DeactiveModifier = GetTupleReference(294924838369222656L);

			public static readonly LOS.TupleReference CookingSong_Play = GetTupleReference(294925626919346176L);

			public static readonly LOS.TupleReference CookingSong_PracticePlayPrompt = GetTupleReference(294925901008723968L);

			public static readonly LOS.TupleReference CookingMinigame_Great = GetTupleReference(296007040863559680L);

			public static readonly LOS.TupleReference CookingMinigame_Good = GetTupleReference(296007088561184768L);

			public static readonly LOS.TupleReference CookingMinigame_Miss = GetTupleReference(296007090285043712L);

			public static readonly LOS.TupleReference CookingMinigame_MaxCombo = GetTupleReference(296007091706912768L);

			public static readonly LOS.TupleReference CookingMinigame_Score = GetTupleReference(296007092784848896L);

			public static readonly LOS.TupleReference CookingMinigame_Accuracy = GetTupleReference(296007245277159424L);

			public static readonly LOS.TupleReference CookingMinigame_Bad = GetTupleReference(296007651256426496L);

			public static readonly LOS.TupleReference CookingMinigame_CookedDish = GetTupleReference(296032299150991360L);

			public static readonly LOS.TupleReference CookingMinigame_NewRecord = GetTupleReference(296035465368166400L);

			public static readonly LOS.TupleReference CookingMinigame_Incomplete = GetTupleReference(296036171634434048L);

			public static readonly LOS.TupleReference CookingMinigame_LostIngredients = GetTupleReference(296037921447731200L);

			public static readonly LOS.TupleReference CookingMinigame_ModifierPracticeInfoResults = GetTupleReference(296038257164017664L);

			public static readonly LOS.TupleReference CookingMinigame_Results = GetTupleReference(296038954961985536L);

			public static readonly LOS.TupleReference CookingMinigame_TimesPlayed = GetTupleReference(296040594465087488L);

			public static readonly LOS.TupleReference CookingMinigame_TimesFinished = GetTupleReference(296040645044199424L);

			public static readonly LOS.TupleReference CookingMinigame_BestScore = GetTupleReference(296040681379454976L);

			public static readonly LOS.TupleReference CookingMinigame_BestAccuracy = GetTupleReference(296040730045964288L);

			public static readonly LOS.TupleReference ItemName = GetTupleReference(296065819206475776L);

			public static readonly LOS.TupleReference ItemDescription = GetTupleReference(296065843676045312L);

			public static readonly LOS.TupleReference ItemDescription_Ingredients = GetTupleReference(327250349971726336L);

			public static readonly LOS.TupleReference CookingMinigame_RecipeExecutionMultipleItems = GetTupleReference(296457857605296128L);

			public static readonly LOS.TupleReference CookingMinigame_ItemSelectedForTheRecipeExecution = GetTupleReference(296458707115433984L);

			public static readonly LOS.TupleReference Items_SelectedItems = GetTupleReference(296808590854840320L);

			public static readonly LOS.TupleReference Items_NoItemsSelected = GetTupleReference(296815914730381312L);

			public static readonly LOS.TupleReference UI_Continue = GetTupleReference(296817701986230272L);

			public static readonly LOS.TupleReference ItemsMissing = GetTupleReference(297140014199857152L);

			public static readonly LOS.TupleReference UnselectOthers = GetTupleReference(297142663217405952L);

			public static readonly LOS.TupleReference ItemSelectionCantSelect = GetTupleReference(297151815499112448L);

			public static readonly LOS.TupleReference CurrentSelection = GetTupleReference(297154642904604672L);

			public static readonly LOS.TupleReference SelectNoneShort = GetTupleReference(297156319145615360L);

			public static readonly LOS.TupleReference SelectAllShort = GetTupleReference(297156343434829824L);

			public static readonly LOS.TupleReference RecipeChoosingItemsNoValidItemsFoundPrompt = GetTupleReference(297189572783104000L);

			public static readonly LOS.TupleReference Equipment_ButtonEat = GetTupleReference(297212801987895296L);

			public static readonly LOS.TupleReference FoodSatiation_Label = GetTupleReference(297481849556426752L);

			public static readonly LOS.TupleReference CookingMinigame_Grade = GetTupleReference(298962322543599616L);

			public static readonly LOS.TupleReference ShillingRatingPrompt = GetTupleReference(298999654550331392L);

			public static readonly LOS.TupleReference RateOnX = GetTupleReference(299000698021863424L);

			public static readonly LOS.TupleReference ClsResult = GetTupleReference(299705356344352768L);

			public static readonly LOS.TupleReference Shop_RichPeopleShopName = GetTupleReference(300063969743200256L);

			public static readonly LOS.TupleReference ColorPalletes = GetTupleReference(302589938785837056L);

			public static readonly LOS.TupleReference Colors = GetTupleReference(302590454408404992L);

			public static readonly LOS.TupleReference CurrentColor = GetTupleReference(302627257874112512L);

			public static readonly LOS.TupleReference YourColorPresets = GetTupleReference(302635637791514624L);

			public static readonly LOS.TupleReference ColorPicker = GetTupleReference(302651357866745856L);

			public static readonly LOS.TupleReference ColorPickerItemPrompt = GetTupleReference(302651419346853888L);

			public static readonly LOS.TupleReference Equipment_SetModGood = GetTupleReference(306475541545869312L);

			public static readonly LOS.TupleReference Equipment_SetModUnknown = GetTupleReference(306475787847983104L);

			public static readonly LOS.TupleReference Equipment_SetModMissing = GetTupleReference(306475705794813952L);

			public static readonly LOS.TupleReference Equipment_SetUsedMods = GetTupleReference(306476096737501184L);

			public static readonly LOS.TupleReference Equipment_SetUsedModsStatus = GetTupleReference(306483346902343680L);

			public static readonly LOS.TupleReference Equipment_CannotEquipItemsToRemove = GetTupleReference(306568702104428544L);

			public static readonly LOS.TupleReference Equipment_CannotEquipPopupTitle = GetTupleReference(306560179287089152L);

			public static readonly LOS.TupleReference Equipment_CannotEquipPromptStart = GetTupleReference(306560360392941568L);

			public static readonly LOS.TupleReference Equipment_CannotEquipPromptEnd = GetTupleReference(306560550168420352L);

			public static readonly LOS.TupleReference LowMemoryHeader = GetTupleReference(310933611869102080L);

			public static readonly LOS.TupleReference LowMemoryToggle = GetTupleReference(310933790127022080L);

			public static readonly LOS.TupleReference LowMemoryDescription = GetTupleReference(310934031433719808L);

			public static readonly LOS.TupleReference LowMemoryTextureScale = GetTupleReference(310934948295991296L);

			public static readonly LOS.TupleReference LowMemoryTextureScaleOptionsInfo = GetTupleReference(310941797204353024L);

			public static readonly LOS.TupleReference OptionsRestartRequired = GetTupleReference(310950382307106816L);

			public static readonly LOS.TupleReference WebGLMobileWarning = GetTupleReference(310968355528892416L);

			public static readonly LOS.TupleReference Slot_Hologram = GetTupleReference(311768631944929280L);

			public static readonly LOS.TupleReference Equipment_ButtonModify = GetTupleReference(313883178461814784L);

			public static readonly LOS.TupleReference VariableBoolEnableContextMenu = GetTupleReference(313887217803059200L);

			public static readonly LOS.TupleReference VariableBoolDisableContextMenu = GetTupleReference(313887667524722688L);

			public static readonly LOS.TupleReference Equipment_Hair = GetTupleReference(313891150017585152L);

			public static readonly LOS.TupleReference SpecialItemVariableEnabled = GetTupleReference(313892573551779840L);

			public static readonly LOS.TupleReference SpecialItemVariableDisabled = GetTupleReference(313892612399423488L);

			public static readonly LOS.TupleReference EquipmentCommandDescription = GetTupleReference(314987481612148736L);

			public static readonly LOS.TupleReference EquipmentCommand_ItemMissing = GetTupleReference(314989713090928640L);

			public static readonly LOS.TupleReference Selected = GetTupleReference(316258147070107648L);

			public static readonly LOS.TupleReference NotSelected = GetTupleReference(316258203424776192L);

			public static readonly LOS.TupleReference SelectAction = GetTupleReference(316259970090786816L);

			public static readonly LOS.TupleReference UnselectAction = GetTupleReference(316259971604930560L);

			public static readonly LOS.TupleReference SelectionTruncatedSummaryEnd = GetTupleReference(316261124338081792L);

			public static readonly LOS.TupleReference UnselectAllAction = GetTupleReference(316601446230384640L);

			public static readonly LOS.TupleReference SelectAllAction = GetTupleReference(316607361348263936L);

			public static readonly LOS.TupleReference SelectItemsForANewOrderPrompt = GetTupleReference(316612122302734336L);

			public static readonly LOS.TupleReference IngredientSelectionTitle = GetTupleReference(316612373772230656L);

			public static readonly LOS.TupleReference ChooseEquipmentPrompt = GetTupleReference(316612621794009088L);

			public static readonly LOS.TupleReference PriceVariesByQuality = GetTupleReference(316719705944481792L);

			public static readonly LOS.TupleReference TargetSlot = GetTupleReference(316720103287676928L);

			public static readonly LOS.TupleReference IngredientSelectionPrompt = GetTupleReference(317096312936849408L);

			public static readonly LOS.TupleReference EquiupmentCommandDescription = GetTupleReference(319255852818096128L);

			public static readonly LOS.TupleReference RentDue = GetTupleReference(319260558508417024L);

			public static readonly LOS.TupleReference CockTwitch_AccountInactive = GetTupleReference(319328371549921280L);

			public static readonly LOS.TupleReference CockTwitch_CantStartStreamAccountInactive = GetTupleReference(319328406836600832L);

			public static readonly LOS.TupleReference Blog_AccountDeleted = GetTupleReference(319700772192247808L);

			public static readonly LOS.TupleReference Blog_CommentsHeader = GetTupleReference(319703659744059392L);

			public static readonly LOS.TupleReference Blog_WrittenBy = GetTupleReference(319705146469638144L);

			public static readonly LOS.TupleReference Blog_Topic = GetTupleReference(319705191344496640L);

			public static readonly LOS.TupleReference Blog_Progress = GetTupleReference(319900903973838848L);

			public static readonly LOS.TupleReference ClickHere = GetTupleReference(319911777543380992L);

			public static readonly LOS.TupleReference Blog_Username = GetTupleReference(319926690701631488L);

			public static readonly LOS.TupleReference Blog_Password = GetTupleReference(319926789292941312L);

			public static readonly LOS.TupleReference Blog_ClickToLogin = GetTupleReference(319935753967624192L);

			public static readonly LOS.TupleReference Blog_LoggedInAs = GetTupleReference(322492133576531968L);

			public static readonly LOS.TupleReference Blog_NotLoggedIn = GetTupleReference(322492181681004544L);

			public static readonly LOS.TupleReference Blog_LoggedOutForYourSafety = GetTupleReference(322492225586978816L);

			public static readonly LOS.TupleReference Blog_AccountBanned = GetTupleReference(322534704394788864L);

			public static readonly LOS.TupleReference Blog_ChiliRecipe1 = GetTupleReference(322557654602866688L);

			public static readonly LOS.TupleReference Blog_ChiliRecipe2 = GetTupleReference(322557686781566976L);

			public static readonly LOS.TupleReference Blog_ChiliRecipe3 = GetTupleReference(322562073406988288L);

			public static readonly LOS.TupleReference Blog_RecipesFromMembers = GetTupleReference(322564471848427520L);

			public static readonly LOS.TupleReference Blog_Logout = GetTupleReference(322873137319370752L);

			public static readonly LOS.TupleReference Blog_BlogLinkEmailInsertHyperlinkText = GetTupleReference(322906135402110976L);

			public static readonly LOS.TupleReference StatusApp_BasicModules = GetTupleReference(323965704895193088L);

			public static readonly LOS.TupleReference StatusApp_ApproximateCondition = GetTupleReference(323966017257594880L);

			public static readonly LOS.TupleReference StatusApp_OtherModules = GetTupleReference(323966295016988672L);

			public static readonly LOS.TupleReference StatusApp_NoOtherModulesFound = GetTupleReference(323967460999946240L);

			public static readonly LOS.TupleReference StatusApp_CompatiblityCheck = GetTupleReference(323967509062475776L);

			public static readonly LOS.TupleReference StatusApp_NoCompatiblityIssuesFound = GetTupleReference(323967725501145088L);

			public static readonly LOS.TupleReference StatusApp_FullConnetionEstablished = GetTupleReference(323967880124162048L);

			public static readonly LOS.TupleReference StatusApp_ConsoleWidth = GetTupleReference(323968019110813696L);

			public static readonly LOS.TupleReference StatusApp_BeginningHardwareCheck = GetTupleReference(323968164145651712L);

			public static readonly LOS.TupleReference StatusApp_ConnectingTo = GetTupleReference(323968599883505664L);

			public static readonly LOS.TupleReference StatusApp_ConnectedTo = GetTupleReference(323968861058621440L);

			public static readonly LOS.TupleReference StatusApp_HelloMessage = GetTupleReference(323969204228186112L);

			public static readonly LOS.TupleReference StatusApp_Ram = GetTupleReference(323972012495069184L);

			public static readonly LOS.TupleReference StatusApp_CPU = GetTupleReference(323972033739218944L);

			public static readonly LOS.TupleReference StatusApp_BIOS = GetTupleReference(323972070204497920L);

			public static readonly LOS.TupleReference StatusApp_BiosModified = GetTupleReference(323972141620912128L);

			public static readonly LOS.TupleReference StatusApp_Memory = GetTupleReference(323972443522719744L);

			public static readonly LOS.TupleReference StatusApp_System = GetTupleReference(323977953940594688L);

			public static readonly LOS.TupleReference StatusApp_PcIconName = GetTupleReference(323990393575170048L);

			public static readonly LOS.TupleReference itemAddedToYourInventory = GetTupleReference(323998529409245184L);

			public static readonly LOS.TupleReference Equipment_HairSection = GetTupleReference(324422569844269056L);

			public static readonly LOS.TupleReference HairSettings_Header = GetTupleReference(324666248097947648L);

			public static readonly LOS.TupleReference HairSettings_Help = GetTupleReference(324668049937391616L);

			public static readonly LOS.TupleReference HairSetting_HelpDescription1 = GetTupleReference(324679602858352640L);

			public static readonly LOS.TupleReference Slot_HairAdditional = GetTupleReference(324680820343169024L);

			public static readonly LOS.TupleReference HairSetting_HelpDescription2 = GetTupleReference(324681566241415168L);

			public static readonly LOS.TupleReference HairSetting_HelpDescription3 = GetTupleReference(324682385728724992L);

			public static readonly LOS.TupleReference HairSetting_HelpDescription4 = GetTupleReference(324684559770705920L);

			public static readonly LOS.TupleReference Equipment_ColorSection = GetTupleReference(324704662713262080L);

			public static readonly LOS.TupleReference CantChangeColor = GetTupleReference(324724537720561664L);

			public static readonly LOS.TupleReference Folder_Hypercamo = GetTupleReference(324742261779718144L);

			public static readonly LOS.TupleReference Folder_NotHypercamo = GetTupleReference(324742303357853696L);

			public static readonly LOS.TupleReference ColorsSection_PickFromPresets = GetTupleReference(324742806699499520L);

			public static readonly LOS.TupleReference Slot_HairAugmentation = GetTupleReference(325046368583278592L);

			public static readonly LOS.TupleReference Hair_Ahoge = GetTupleReference(326496452730675200L);

			public static readonly LOS.TupleReference Hair_Front = GetTupleReference(326496470711656448L);

			public static readonly LOS.TupleReference Hair_Side = GetTupleReference(326496727755382784L);

			public static readonly LOS.TupleReference Hair_Back = GetTupleReference(326496729714122752L);

			public static readonly LOS.TupleReference Hair_Braid = GetTupleReference(326497075953917952L);

			public static readonly LOS.TupleReference Sort_ByDifficulty = GetTupleReference(326949855952887808L);

			public static readonly LOS.TupleReference CookingSongList_Header = GetTupleReference(326951340845555712L);

			public static readonly LOS.TupleReference CookingMinigame_WrongKey = GetTupleReference(326955687113383936L);

			public static readonly LOS.TupleReference Sort_AlphabeticallyBySongName = GetTupleReference(327230647882997760L);

			public static readonly LOS.TupleReference CookingMinigame_FullCombo = GetTupleReference(327240905971982336L);

			public static readonly LOS.TupleReference DishQuality_Header = GetTupleReference(327244749757997056L);

			public static readonly LOS.TupleReference ItemDescription_NutritionalValues = GetTupleReference(327252742549528576L);

			public static readonly LOS.TupleReference FoodSatiation_Satiation = GetTupleReference(327253747311828992L);

			public static readonly LOS.TupleReference FoodEffect_MaxCumIncrease = GetTupleReference(327254785859248128L);

			public static readonly LOS.TupleReference FoodEffect_MaxCumDecrease = GetTupleReference(327257149269860352L);

			public static readonly LOS.TupleReference FoodEffect_StaminaRecovery = GetTupleReference(327257721863659520L);

			public static readonly LOS.TupleReference FoodEffect_StaminaLoss = GetTupleReference(327257724082446336L);

			public static readonly LOS.TupleReference FoodEffect_CurrentCumIncrease = GetTupleReference(327258303588458496L);

			public static readonly LOS.TupleReference FoodEffect_CurrentCumDecrease = GetTupleReference(327258305442340864L);

			public static readonly LOS.TupleReference FoodEffect_MentalHealthDecrease = GetTupleReference(327259150795923456L);

			public static readonly LOS.TupleReference FoodEffect_WarningPrompt = GetTupleReference(327259479696465920L);

			public static readonly LOS.TupleReference CookingSong_PlayAuto = GetTupleReference(327275935859466240L);

			public static readonly LOS.TupleReference CookingSong_PlayAutoPrompt = GetTupleReference(327276190738931712L);

			public static readonly LOS.TupleReference CookingSong_IngredientFishAtLeastXkg = GetTupleReference(327605476918288384L);

			public static readonly LOS.TupleReference CookingSong_IngredientOptional = GetTupleReference(327631757932224512L);

			public static readonly LOS.TupleReference CookingSong_LockedDIsh = GetTupleReference(327634950372188160L);

			public static readonly LOS.TupleReference CookingSong_NewDish = GetTupleReference(327636876157509632L);

			public static readonly LOS.TupleReference CookingSong_BestGrade = GetTupleReference(327637086371831808L);

			public static readonly LOS.TupleReference Folder_CookingIngredients = GetTupleReference(327937932930310144L);

			public static readonly LOS.TupleReference FoodEffect_MentalHealthIncreaseSmall = GetTupleReference(327958095562006528L);

			public static readonly LOS.TupleReference FoodEffect_MentalHealthIncreaseBig = GetTupleReference(327958423577550848L);

			public static readonly LOS.TupleReference FoodEffect_MentalHealthIncreaseModerate = GetTupleReference(327958669204381696L);

			public static readonly LOS.TupleReference FoodEffect_YouNeedToCookThis = GetTupleReference(327961407338307584L);

			public static readonly LOS.TupleReference Cooking_SpentTime = GetTupleReference(327979375921192960L);

			public static readonly LOS.TupleReference Ingredient_AnyPasta = GetTupleReference(327983672008368128L);

			public static readonly LOS.TupleReference Ingredient_AnyBread = GetTupleReference(327983694011686912L);

			public static readonly LOS.TupleReference Ingredient_AnyFlavourVegetable = GetTupleReference(327983590005530624L);

			public static readonly LOS.TupleReference Ingredient_AnyMainVegetable = GetTupleReference(327983527388766208L);

			public static readonly LOS.TupleReference Ingredient_AnyMeat = GetTupleReference(327983479418511360L);

			public static readonly LOS.TupleReference Ingredient_AnyBean = GetTupleReference(327983854280237056L);

			public static readonly LOS.TupleReference Ingredient_AnyBeverage = GetTupleReference(327984090495049728L);

			public static readonly LOS.TupleReference Ingredient_AnyCookedDish = GetTupleReference(327984168140005376L);

			public static readonly LOS.TupleReference Ingredient_AnyFish = GetTupleReference(327984263929520128L);

			public static readonly LOS.TupleReference Ingredient_AnyOther = GetTupleReference(327984380392759296L);

			public static readonly LOS.TupleReference GenericIngredientHelpStart = GetTupleReference(327984874716651520L);

			public static readonly LOS.TupleReference FishingRod_Acceleration = GetTupleReference(328689309109444608L);

			public static readonly LOS.TupleReference FishingRod_ReelingSpeed = GetTupleReference(328689312964009984L);

			public static readonly LOS.TupleReference FishingRod_Size = GetTupleReference(328689289895337984L);

			public static readonly LOS.TupleReference ComparsionToEquippedItem_IncreasedByPercent = GetTupleReference(328691781546795008L);

			public static readonly LOS.TupleReference ComparsionToEquippedItem_DecreasedByPercent = GetTupleReference(328691782935109632L);

			public static readonly LOS.TupleReference ComparsionToEquippedItem_NoChange = GetTupleReference(328691727478022144L);

			public static readonly LOS.TupleReference FishingRod_MaxEffectiveFishWeight = GetTupleReference(328696489023889408L);

			public static readonly LOS.TupleReference WeightKgShort = GetTupleReference(328696865701748736L);

			public static readonly LOS.TupleReference ItemModifier_ReinforcedLine = GetTupleReference(328697289624248320L);

			public static readonly LOS.TupleReference ItemModifier_ReinforcedLineDescription = GetTupleReference(328697339892981760L);

			public static readonly LOS.TupleReference ItemModifier_Precise = GetTupleReference(328698041478406144L);

			public static readonly LOS.TupleReference ItemModifier_PreciseDescription = GetTupleReference(328698048633888768L);

			public static readonly LOS.TupleReference ItemModifier_Lightweight = GetTupleReference(328699166793392128L);

			public static readonly LOS.TupleReference ItemModifier_LightweightDescription = GetTupleReference(328699599867863040L);

			public static readonly LOS.TupleReference ItemModifier_FishCalibrated = GetTupleReference(328984205804429312L);

			public static readonly LOS.TupleReference ItemModifier_FishCalibratedDescription = GetTupleReference(328984269666902016L);

			public static readonly LOS.TupleReference Item_FishInfoHeader = GetTupleReference(329015496075632640L);

			public static readonly LOS.TupleReference Item_FishPolluted = GetTupleReference(329025178949378048L);

			public static readonly LOS.TupleReference Item_FishPollutedDescription = GetTupleReference(329026021870264320L);

			public static readonly LOS.TupleReference Equipment_Fish = GetTupleReference(329055552375087104L);

			public static readonly LOS.TupleReference FishFirstTimeCaught = GetTupleReference(329745697759813632L);

			public static readonly LOS.TupleReference CaughtItemCount = GetTupleReference(329431909940584448L);

			public static readonly LOS.TupleReference TooTiredToFishInfo = GetTupleReference(329744041978617856L);

			public static readonly LOS.TupleReference CaughtCountFish = GetTupleReference(329744322158125056L);

			public static readonly LOS.TupleReference FishNewMaxSizeRecord = GetTupleReference(329745659939774464L);

			public static readonly LOS.TupleReference FishNewMinSizeRecord = GetTupleReference(329773428245520384L);

			public static readonly LOS.TupleReference CatchListHeader = GetTupleReference(329775437442965504L);

			public static readonly LOS.TupleReference FishBiteMissed = GetTupleReference(329790253570187264L);

			public static readonly LOS.TupleReference FishingDone = GetTupleReference(329790563772522496L);

			public static readonly LOS.TupleReference HeavyFishWarning = GetTupleReference(329792392925274112L);

			public static readonly LOS.TupleReference FishNothingCaught = GetTupleReference(329802480138313728L);

			public static readonly LOS.TupleReference Item_Trash = GetTupleReference(329826286345117696L);

			public static readonly LOS.TupleReference Fisher_SellFish = GetTupleReference(329851276452945920L);

			public static readonly LOS.TupleReference Fisher_SellAllFish = GetTupleReference(329866980849868800L);

			public static readonly LOS.TupleReference Fisher_SellAllFishPrompt = GetTupleReference(329868300516319232L);

			public static readonly LOS.TupleReference Fisher_SellFishTitle = GetTupleReference(329938634368081920L);

			public static readonly LOS.TupleReference Fishing_TotalFishesCaught = GetTupleReference(329942960369033216L);

			public static readonly LOS.TupleReference Fishing_DifferentFishesCaught = GetTupleReference(329943435785003008L);

			public static readonly LOS.TupleReference Fishing_BigFishesCaught = GetTupleReference(329944719321718784L);

			public static readonly LOS.TupleReference FileCommandDescription = GetTupleReference(330575905115070464L);

			public static readonly LOS.TupleReference FileCommandFileNotFound = GetTupleReference(330576798375993344L);

			public static readonly LOS.TupleReference FileCommandHelp = GetTupleReference(330576860195840000L);

			public static readonly LOS.TupleReference FileCommandHelp2 = GetTupleReference(330578028066234368L);

			public static readonly LOS.TupleReference CommandExampleUsage = GetTupleReference(330578181087027200L);

			public static readonly LOS.TupleReference FileCommandAccessDenied = GetTupleReference(330579698426830848L);

			public static readonly LOS.TupleReference FileCommandMustInputFilename = GetTupleReference(330581303033323520L);

			public static readonly LOS.TupleReference FileCommandEmptyFile = GetTupleReference(330581742961287168L);

			public static readonly LOS.TupleReference InteractCommandDescription = GetTupleReference(330582428763545600L);

			public static readonly LOS.TupleReference FileCommandFileContents = GetTupleReference(330588015542919168L);

			public static readonly LOS.TupleReference InteractDirectConverasationOver = GetTupleReference(330597129824231424L);

			public static readonly LOS.TupleReference Fishing_WaitForBitesInfo = GetTupleReference(332060315479957504L);

			public static readonly LOS.TupleReference Fishing_MainMinigameInfo = GetTupleReference(332060801968889856L);

			public static readonly LOS.TupleReference CookingMInigame_Help1 = GetTupleReference(332101605533933568L);

			public static readonly LOS.TupleReference CookingMInigame_Help2 = GetTupleReference(332101628095094784L);

			public static readonly LOS.TupleReference NewSaveRecomendedPrompt0_90 = GetTupleReference(332408564678123520L);

			public static readonly LOS.TupleReference FishingMinigame_Help1 = GetTupleReference(335500190728314880L);

			public static readonly LOS.TupleReference FishingMinigame_Help2 = GetTupleReference(335500222462418944L);

			public static readonly LOS.TupleReference FishingMinigame_Help3 = GetTupleReference(335501113718464512L);

			public static readonly LOS.TupleReference FishingMinigame_Help4 = GetTupleReference(335501636798504960L);

			public static readonly LOS.TupleReference FishingMinigame_Help5 = GetTupleReference(335502144930045952L);

			public static readonly LOS.TupleReference FishingMinigame_InfoAboveFishingGauge = GetTupleReference(335510175046688768L);

			public static readonly LOS.TupleReference FishingMinigame_InfoBelowFishingGauge = GetTupleReference(335510226649210880L);

			public static readonly LOS.TupleReference FishingMinigame_InfoAboveFishingProgress = GetTupleReference(335510248031772672L);

			public static readonly LOS.TupleReference FishingMinigame_InfoBelowFishingProgress = GetTupleReference(335510271205302272L);

			public static readonly LOS.TupleReference stocksdotic_OwnershipLimitReached = GetTupleReference(341649999747080192L);

			public static readonly LOS.TupleReference stocksdotic_OwnershipLimitInfo = GetTupleReference(341650822040379392L);

			public static readonly LOS.TupleReference Equipment_SetEquippingCannotCopyVariable = GetTupleReference(341677855076245504L);

			public static readonly LOS.TupleReference Equipment_SetEquippingCopiedBooleanVariable = GetTupleReference(341678415997296640L);

			public static readonly LOS.TupleReference Live2DTool_Soap = GetTupleReference(443545661811449856L);

			public static readonly LOS.TupleReference Live2DTool_Sponge = GetTupleReference(443545739800338432L);

			public static readonly LOS.TupleReference Frick_ThighJob = GetTupleReference(445755131346018304L);

			public static readonly LOS.TupleReference Room_Shower = GetTupleReference(445768145315028992L);

			public static readonly LOS.TupleReference Options_DisplaySettingCheckPrompt = GetTupleReference(446085771639644160L);

			public static readonly LOS.TupleReference Room_Interact = GetTupleReference(448969526033244160L);

			public static readonly LOS.TupleReference Shower_Cleanliness = GetTupleReference(450764703022440448L);

			public static readonly LOS.TupleReference Shower_CleanedInfo = GetTupleReference(450802848761896960L);

			public static readonly LOS.TupleReference Shower_SoapLeft = GetTupleReference(450802917011611648L);

			public static readonly LOS.TupleReference Shower_SoapLeftNoneInfo = GetTupleReference(450802950742204416L);

			public static readonly LOS.TupleReference Shower_Soap = GetTupleReference(454039327529230336L);

			public static readonly LOS.TupleReference Shower_Sponge = GetTupleReference(454039364061618176L);

			public static readonly LOS.TupleReference Shower_WashOff = GetTupleReference(454039409787920384L);

			public static readonly LOS.TupleReference Shower_ChooseSoap = GetTupleReference(454040063709274112L);

			public static readonly LOS.TupleReference Shower_EnoughSoap = GetTupleReference(454040364956770304L);

			public static readonly LOS.TupleReference Shower_NoSoap = GetTupleReference(454040604627689472L);

			public static readonly LOS.TupleReference UI_RentAmountInfo = GetTupleReference(456239488389079040L);

			public static readonly LOS.TupleReference UI_ShoweredToday = GetTupleReference(458372233793888256L);

			public static readonly LOS.TupleReference UI_ShoweredYesterday = GetTupleReference(458372287472590848L);

			public static readonly LOS.TupleReference UI_YouHaventShoweredForXDays = GetTupleReference(458372339297411072L);

			public static readonly LOS.TupleReference Shower_Sex = GetTupleReference(471121236486774784L);

			public static readonly LOS.TupleReference bangdotic_worldnews = GetTupleReference(472119528704172032L);

			public static readonly LOS.TupleReference bangdotic_USAHeader = GetTupleReference(472124086796935168L);

			public static readonly LOS.TupleReference bangdotic_UKHeader = GetTupleReference(472124268846505984L);

			public static readonly LOS.TupleReference bangdotic_OpinionsHeader = GetTupleReference(472125338964451328L);

			public static readonly LOS.TupleReference bangdotic_SportHeader = GetTupleReference(472125548503490560L);

			public static readonly LOS.TupleReference bangdotic_ProjectsHeader = GetTupleReference(472126417856880640L);

			public static readonly LOS.TupleReference bangdotic_EmailStatusHeader = GetTupleReference(472129961276203008L);

			public static readonly LOS.TupleReference ItemDescription_DishIngredients = GetTupleReference(296079490024464384L);

			public static readonly LOS.TupleReference ExpectedIngredient = GetTupleReference(297186126352867328L);

			public static readonly LOS.TupleReference bangdotic_OpinionsTitle = GetTupleReference(472134547655352320L);

			public static readonly LOS.TupleReference Shower_ExitPrompt = GetTupleReference(472543019022671872L);

			public static readonly LOS.TupleReference Cleanliness_Clean = GetTupleReference(474648345637281792L);

			public static readonly LOS.TupleReference Cleanliness_SomeCum = GetTupleReference(474648503838040064L);

			public static readonly LOS.TupleReference Cleanliness_ALotOfCum = GetTupleReference(474648570812686336L);

			public static readonly LOS.TupleReference Interact_Clean = GetTupleReference(474693331179266048L);

			public static readonly LOS.TupleReference Interact_CleanWithARag = GetTupleReference(474696176054034432L);

			public static readonly LOS.TupleReference Shower_ExitPromptBeforeAnything = GetTupleReference(476105129992380416L);

			public static readonly LOS.TupleReference Shower_ExitPromptNotFinished = GetTupleReference(476105271864713216L);

			public static readonly LOS.TupleReference Equipment_DontOwnColor = GetTupleReference(476191777304338432L);

			public static readonly LOS.TupleReference Equipment_DontOwn = GetTupleReference(476191862738116608L);

			public static readonly LOS.TupleReference Equipment_OwnPackage = GetTupleReference(476191918648188928L);

			public static readonly LOS.TupleReference Equipment_DontOwnPackage = GetTupleReference(476192284051759104L);

			public static readonly LOS.TupleReference Equipment_OwnSlot = GetTupleReference(476193003509112832L);

			public static readonly LOS.TupleReference Equipment_DontOwnSlot = GetTupleReference(476193361698480128L);

			public static readonly LOS.TupleReference Equipment_ViewRelated = GetTupleReference(476193497661038592L);

			public static readonly LOS.TupleReference Equipment_ExactItemMatch = GetTupleReference(476193841459748864L);

			public static readonly LOS.TupleReference Equipment_RelatedItems = GetTupleReference(476193969608318976L);

			public static readonly LOS.TupleReference Equipment_RelatedOwnedItems = GetTupleReference(476194087434706944L);

			public static readonly LOS.TupleReference Equipment_RelatedOwnedItemsInfoText = GetTupleReference(476194174818836480L);

			public static readonly LOS.TupleReference Achievments_StoryTextPercent = GetTupleReference(476526596789886976L);

			public static readonly LOS.TupleReference Options_SkipUnseedTextToggle = GetTupleReference(476546070280658944L);

			public static readonly LOS.TupleReference UI_SkipEnabledNewTextPrompt = GetTupleReference(476552496302514176L);

			public static readonly LOS.TupleReference Options_CumFlashBangToggle = GetTupleReference(478056522227638272L);

			public static readonly LOS.TupleReference Options_CumFlashbangTooltip = GetTupleReference(478056759537164288L);

			public static readonly LOS.TupleReference Options_SkipUnseenTextTooltip = GetTupleReference(478060044352479232L);

			public static readonly LOS.TupleReference Room_LeaveTheCity = GetTupleReference(483040306832334848L);

			public static readonly LOS.TupleReference Mods_SafetyWarning1 = GetTupleReference(483407230179205120L);

			public static readonly LOS.TupleReference Mods_SafetyWarning2 = GetTupleReference(483407824948289536L);

			public static readonly LOS.TupleReference Mods_SafetyWarning3 = GetTupleReference(483407827385180160L);

			public static readonly LOS.TupleReference Mods_SafetyWarning4 = GetTupleReference(483407829138399232L);

			public static readonly LOS.TupleReference Achievements_NextUnseedGoodEnding = GetTupleReference(484477438352416768L);

			public static readonly LOS.TupleReference Achievements_MainEndingsHeader = GetTupleReference(484477599141060608L);

			public static readonly LOS.TupleReference Achievements_OtherHeader = GetTupleReference(484477662353416192L);

			public static readonly LOS.TupleReference Achievements_ChurchEndingsHeader = GetTupleReference(484478029224992768L);

			public static readonly LOS.TupleReference Achievements_EndingSeen = GetTupleReference(484478221374447616L);

			public static readonly LOS.TupleReference Achievements_EndingUnseen = GetTupleReference(484478249354649600L);

			public static readonly LOS.TupleReference Achievements_ShowExtraDetailsToggle = GetTupleReference(484479769101983744L);

			public static readonly LOS.TupleReference Achievements_GoodEndingsHeader = GetTupleReference(484483204786413568L);

			public static readonly LOS.TupleReference Achievements_BadEndingsHeader = GetTupleReference(484483236382105600L);

			public static readonly LOS.TupleReference Achievements_GoodEndingsCount = GetTupleReference(484487836430491648L);

			public static readonly LOS.TupleReference Achievements_BadEndingsCount = GetTupleReference(484487912431280128L);

			public static readonly LOS.TupleReference Credits_GoodEndingAchieved = GetTupleReference(484532030339538944L);

			public static readonly LOS.TupleReference UI_NewSkipEnabledNewTextPrompt = GetTupleReference(485487702006816768L);

			public static readonly LOS.TupleReference UI_SkipButtonTooltip = GetTupleReference(485888026693591040L);

			public static readonly LOS.TupleReference UI_NarrationLogTooltip = GetTupleReference(485889586559442944L);

			public static readonly LOS.TupleReference CookingSong_BestDishQuality = GetTupleReference(486295437891854336L);

			public static readonly LOS.TupleReference UI_EarlyAccessThanks = GetTupleReference(487788642143559680L);

			public static readonly LOS.TupleReference UI_EarlyAccessWarning = GetTupleReference(487788813547986944L);

			public static readonly LOS.TupleReference UI_EarlyAccessPirateWarning = GetTupleReference(487795609801056256L);

			public static readonly LOS.TupleReference UI_EarlyAccessOfficialSources = GetTupleReference(487796447680389120L);

			public static readonly LOS.TupleReference UI_PullingOutActionContinuuing = GetTupleReference(487809587918921728L);

			public static readonly LOS.TupleReference Slot_BodyModules = GetTupleReference(487880583661215744L);

			public static readonly LOS.TupleReference Slot_FeetBodyModules = GetTupleReference(488162793228328960L);

			public static readonly LOS.TupleReference Slot_CalfBodyModules = GetTupleReference(488162780284706816L);

			public static readonly LOS.TupleReference Slot_ThighBodyModules = GetTupleReference(488162753244028928L);

			public static readonly LOS.TupleReference Equipment_SetEquippingCopiedFloatVariable = GetTupleReference(488163137152868352L);

			public static readonly LOS.TupleReference BodyMod_ThighSize = GetTupleReference(488166734942953472L);

			public static readonly LOS.TupleReference BodyMod_CalfSize = GetTupleReference(488166887586258944L);

			public static readonly LOS.TupleReference BodyMod_FootWidth = GetTupleReference(488166928447168512L);

			public static readonly LOS.TupleReference BodyMod_FootLength = GetTupleReference(488166975201075200L);

			public static readonly LOS.TupleReference Equipment_RandomOutfit = GetTupleReference(488975334281584640L);

			public static readonly LOS.TupleReference Equipment_RandomOutfitPrompt = GetTupleReference(488975408898252800L);

			public static readonly LOS.TupleReference SAVE_LoadErrorInvalidSavePrompt = GetTupleReference(489264642502696960L);

			public static readonly LOS.TupleReference Options_GrayOutSeenTextToggle = GetTupleReference(489268972827975680L);

			public static readonly LOS.TupleReference Options_GreyOutSeenTextTooltip = GetTupleReference(489272145760231424L);

			public static readonly LOS.TupleReference Equipment_CannotChangeOrderWhileSortedAllowedSortPrompt = GetTupleReference(489289234281668608L);

			public static readonly LOS.TupleReference Mods_SpecialButton = GetTupleReference(489336303788761088L);

			public static readonly LOS.TupleReference Eyepatch_Left = GetTupleReference(489558131031416832L);

			public static readonly LOS.TupleReference Eyepatch_Right = GetTupleReference(489558461962002432L);

			public static readonly LOS.TupleReference Slot_TongueAccessory = GetTupleReference(489588868422049792L);

			public static readonly LOS.TupleReference ChooseLanguage = GetTupleReference(489645086704410624L);

			public static readonly LOS.TupleReference ChooseModTranslationGenerationLanguage = GetTupleReference(489645144103460864L);

			public static readonly LOS.TupleReference SuccessfulyGenerateTranslationFile = GetTupleReference(489646130897362944L);

			public static readonly LOS.TupleReference FailedGeneratingTranslationFile = GetTupleReference(489647547020222464L);

			public static readonly LOS.TupleReference Mods_ModVersion = GetTupleReference(489661828419821568L);

			public static readonly LOS.TupleReference SelectModTranslationFilesPrompt = GetTupleReference(489663437761683456L);

			public static readonly LOS.TupleReference ModTranslationFiles = GetTupleReference(489663838821031936L);

			public static readonly LOS.TupleReference SuccessfulyLoadedModTranslationFile = GetTupleReference(489666750372651008L);

			public static readonly LOS.TupleReference FailedLoadingModTranslationFile = GetTupleReference(489667826001911808L);

			public static readonly LOS.TupleReference Mods_ViewLogs = GetTupleReference(489670808600846336L);

			public static readonly LOS.TupleReference Mods_ModTranslatedToLanguage = GetTupleReference(489686432504881152L);

			public static readonly LOS.TupleReference Mods_ModNotTranslatedToLangauge = GetTupleReference(489686547558834176L);

			public static readonly LOS.TupleReference Mods_GenerateModTranslationFile = GetTupleReference(489987962034462720L);

			public static readonly LOS.TupleReference Mods_ImportModTranslationFiles = GetTupleReference(489988037330608128L);

			public static readonly LOS.TupleReference Mods_NoTranslationData = GetTupleReference(489990660490272768L);

			public static readonly LOS.TupleReference Mods_ImportedTranslations = GetTupleReference(489990733857038336L);

			public static readonly LOS.TupleReference Mods_ModTranslationNotes = GetTupleReference(489998867560407040L);

			public static readonly LOS.TupleReference Mods_DeleteInstalledTranslationModFiles = GetTupleReference(490004860151275520L);

			public static readonly LOS.TupleReference Mods_DeleteTranslationModFIlesPrompt = GetTupleReference(490004949460590592L);

			public static readonly LOS.TupleReference Mods_DeleteTranslationModFIlesPickerPrompt = GetTupleReference(490944453897887744L);

			public static readonly LOS.TupleReference Mods_ExportInstalledTranslationModFiles = GetTupleReference(490944849613692928L);

			public static readonly LOS.TupleReference Mods_ExportTranslationModFIlesPickerPrompt = GetTupleReference(490945246227079168L);

			public static readonly LOS.TupleReference Mods_LocalizingMods = GetTupleReference(491009961577947136L);

			public static readonly LOS.TupleReference Mods_NoTranslationIssuesFound = GetTupleReference(491025278207270912L);

			public static readonly LOS.TupleReference Mods_TranslationIssuesExtraKeys = GetTupleReference(491025385531121664L);

			public static readonly LOS.TupleReference Mods_TranslationIssuesMissingKeys = GetTupleReference(491025453571121152L);

			public static readonly LOS.TupleReference Mods_TranslationIssuesBrokenKeys = GetTupleReference(491025488312541184L);

			public static readonly LOS.TupleReference Mods_TranslationIssuesLogInfo = GetTupleReference(491032741463728128L);

			public static readonly LOS.TupleReference Mods_TranslationIssuesBroken = GetTupleReference(491033140786634752L);

			public static readonly LOS.TupleReference Mods_TargetModVersion = GetTupleReference(491037523914006528L);

			public static readonly LOS.TupleReference HoverToView = GetTupleReference(491037976072560640L);

			public static readonly LOS.TupleReference FullyTranslated = GetTupleReference(491040484891303936L);

			public static readonly LOS.TupleReference Mods_TranslationIssues = GetTupleReference(491043588659486720L);

			public static readonly LOS.TupleReference Mods_ModsOriginalLangauge = GetTupleReference(491061162591920128L);

			public static readonly LOS.TupleReference Folder_Sliders = GetTupleReference(491337545033457664L);

			public static readonly LOS.TupleReference Folder_Toggles = GetTupleReference(491337567032582144L);

			public static readonly LOS.TupleReference Slot_Manicure = GetTupleReference(491372144350470144L);

			public static readonly LOS.TupleReference Slot_Pedicure = GetTupleReference(491421408766205952L);

			public static readonly LOS.TupleReference InputEmail = GetTupleReference(493902357215059968L);

			public static readonly LOS.TupleReference InputEmailAreYouSure = GetTupleReference(493907265213440000L);

			public static readonly LOS.TupleReference InvalidEmail = GetTupleReference(493909894538731520L);

			public static readonly LOS.TupleReference FailedToVerify = GetTupleReference(493910851381108736L);

			public static readonly LOS.TupleReference VerifyOnDiscord = GetTupleReference(493922460669321216L);

			public static readonly LOS.TupleReference VerifyWithoutDiscord = GetTupleReference(493922652495814656L);

			public static readonly LOS.TupleReference IHaveTheCode = GetTupleReference(493922962240970752L);

			public static readonly LOS.TupleReference LoginInfo_GameEarlyAccess = GetTupleReference(493924743922589696L);

			public static readonly LOS.TupleReference LoginInfo_AccessInfo = GetTupleReference(493925030305472512L);

			public static readonly LOS.TupleReference LoginInfo_DiscordCommand = GetTupleReference(493927354675503104L);

			public static readonly LOS.TupleReference Login_Email = GetTupleReference(493932568472199168L);

			public static readonly LOS.TupleReference LinksToStuff = GetTupleReference(493944224614981632L);

			public static readonly LOS.TupleReference JoinDiscordServer = GetTupleReference(493946882415403008L);

			public static readonly LOS.TupleReference LoginInfo_AccessInfoOnlyOnce = GetTupleReference(493949397156503552L);

			public static readonly LOS.TupleReference LoginInfo_PrivateMessage = GetTupleReference(493982020503347200L);

			public static readonly LOS.TupleReference LoginInfo_DiscordCommand2 = GetTupleReference(493982440189599744L);

			public static readonly LOS.TupleReference LoginInfo_Alternative = GetTupleReference(493982925793533952L);

			public static readonly LOS.TupleReference LoginInfo_Alternative2 = GetTupleReference(493983195621498880L);

			public static readonly LOS.TupleReference LoginInfo_WhatToDoWithCode = GetTupleReference(493985080814346240L);

			public static readonly LOS.TupleReference LoginInfo_IHaveTheCodePrompt = GetTupleReference(493986248156258304L);

			public static readonly LOS.TupleReference LoginInfo_SuccessfulVerification = GetTupleReference(493987150627872768L);

			public static readonly LOS.TupleReference BodyMod_BoobSize = GetTupleReference(496402615639744512L);

			public static readonly LOS.TupleReference Slot_BoobBodyModules = GetTupleReference(496403323130748928L);

			public static readonly LOS.TupleReference Equipment_Legs = GetTupleReference(496802670846681088L);

			public static readonly LOS.TupleReference Equipment_Arms = GetTupleReference(496802702727585792L);

			public static readonly LOS.TupleReference Equipment_Socks = GetTupleReference(496803314248720384L);

			public static readonly LOS.TupleReference Equipment_Shoes = GetTupleReference(496804553137070080L);

			public static readonly LOS.TupleReference Equipment_LowerBody = GetTupleReference(496810800242499584L);

			public static readonly LOS.TupleReference Equipment_UpperBody = GetTupleReference(496810849705926656L);

			public static readonly LOS.TupleReference Equipment_Outer = GetTupleReference(496810876587220992L);

			public static readonly LOS.TupleReference Equipment_BodySizeModules = GetTupleReference(496814697568804864L);

			public static readonly LOS.TupleReference Room_Clothes = GetTupleReference(497811185744089088L);

			public static readonly LOS.TupleReference Equipment_MakeUp = GetTupleReference(497824627733434368L);

			public static readonly LOS.TupleReference Equipment_Neck = GetTupleReference(497826289369878528L);

			public static readonly LOS.TupleReference Equipment_Piercing = GetTupleReference(497826636654055424L);

			public static readonly LOS.TupleReference BodyMod_AssSize = GetTupleReference(497858709615939584L);

			public static readonly LOS.TupleReference Slot_AssBodyModules = GetTupleReference(498300297139851264L);

			public static readonly LOS.TupleReference SAVE_LoadFromSlotFileChooseSlotPrompt = GetTupleReference(498332293912203264L);

			public static readonly LOS.TupleReference SAVE_InvalidSlotNumberNotNumber = GetTupleReference(498334095147667456L);

			public static readonly LOS.TupleReference SAVE_InvalidSlotNumberOutOfRange = GetTupleReference(498334455421603840L);

			public static readonly LOS.TupleReference NumbersRangeInfo = GetTupleReference(498334650720980992L);

			public static readonly LOS.TupleReference SAVE_InvalidSlotNumberAlreadyTaken = GetTupleReference(498336309140393984L);

			public static readonly LOS.TupleReference SAVE_SuccessfullyLoadedSlotDataIntoSlotNumber = GetTupleReference(498338501477928960L);

			public static readonly LOS.TupleReference SAVE_SlotLoadingWarning = GetTupleReference(498342642031923200L);

			public static readonly LOS.TupleReference SAVE_SlotFileExtension = GetTupleReference(498343662011158528L);

			public static readonly LOS.TupleReference Room_DoSomethingTogether = GetTupleReference(498365790617997312L);

			public static readonly LOS.TupleReference Interact_Sex = GetTupleReference(498368129751977984L);

			public static readonly LOS.TupleReference LoginInfo_PublicReleaseData = GetTupleReference(498590055413977088L);

			public static readonly LOS.TupleReference Slot_Navel = GetTupleReference(499141756822884352L);

			public static readonly LOS.TupleReference Slot_NippleLeft = GetTupleReference(499141866881421312L);

			public static readonly LOS.TupleReference Slot_NippleRight = GetTupleReference(499141868802412544L);

			public static readonly LOS.TupleReference ModifableVariables_Header = GetTupleReference(499378805064732672L);

			public static readonly LOS.TupleReference ModifiableVariables_CanBeChanged = GetTupleReference(499378856839221248L);

			public static readonly LOS.TupleReference PasteFromClipboard = GetTupleReference(499406199741878272L);

			public static readonly LOS.TupleReference PasteFromClipboard_SuccessfullyPasted = GetTupleReference(499407869011955712L);

			public static readonly LOS.TupleReference PasteFromClipboard_ClipboardEmpty = GetTupleReference(499407920878718976L);

			public static readonly LOS.TupleReference Options_ClearVerification = GetTupleReference(499410529249267712L);

			public static readonly LOS.TupleReference Interactable_Spank = GetTupleReference(537744955715608576L);

			public static readonly LOS.TupleReference Interactable_SpreadAnus = GetTupleReference(537769387989393408L);

			public static readonly LOS.TupleReference Interactable_GrabWaist = GetTupleReference(538486512190218240L);

			public static readonly LOS.TupleReference Interactable_FingeringAnus = GetTupleReference(538508930501828608L);

			public static readonly LOS.TupleReference Frick_ChangeCamera = GetTupleReference(540001448330518528L);

			public static readonly LOS.TupleReference Frick_ShowCock = GetTupleReference(540990535136960512L);

			public static readonly LOS.TupleReference Hair_FrontClippedUp = GetTupleReference(542208471210123264L);

			public static readonly LOS.TupleReference FishingDuelWon = GetTupleReference(543309117950779392L);

			public static readonly LOS.TupleReference FishingDuelLost = GetTupleReference(543309152826417152L);

			public static readonly LOS.TupleReference FishingDuelWinnerYou = GetTupleReference(543502681200648192L);

			public static readonly LOS.TupleReference FishingDuelWinnerEnemy = GetTupleReference(543524635571904512L);

			public static readonly LOS.TupleReference FishingFishJointStatBoost = GetTupleReference(543559430871965696L);

			public static readonly LOS.TupleReference LoginInfo_MakeSureToEnableDms = GetTupleReference(545019226687004672L);

			public static readonly LOS.TupleReference FishingDuelScore = GetTupleReference(546458543988924416L);

			public static readonly LOS.TupleReference Frick_CantDoThatWhenCumming = GetTupleReference(546782993317879808L);

			public static readonly LOS.TupleReference CheatsTitle = GetTupleReference(550048406222921728L);

			public static readonly LOS.TupleReference Options_DisableAdvancedPrompt = GetTupleReference(550049084538347520L);

			public static readonly LOS.TupleReference Cheat_Money = GetTupleReference(550063086752489472L);

			public static readonly LOS.TupleReference Cheat_Items = GetTupleReference(550063118595645440L);

			public static readonly LOS.TupleReference Cheat_ItemColors = GetTupleReference(550063161595650048L);

			public static readonly LOS.TupleReference Cheat_Quality = GetTupleReference(550063278054694912L);

			public static readonly LOS.TupleReference Cheat_CumCheats = GetTupleReference(550063365107474432L);

			public static readonly LOS.TupleReference Cheats_AccessLinks = GetTupleReference(550065358517559296L);

			public static readonly LOS.TupleReference Cheats_GetCheatCode = GetTupleReference(550068833926242304L);

			public static readonly LOS.TupleReference Cheats_HaveCheatCode = GetTupleReference(550069333585289216L);

			public static readonly LOS.TupleReference OpenedLink = GetTupleReference(550092321927979008L);

			public static readonly LOS.TupleReference Cheat_DiscordCommand2 = GetTupleReference(550095134015873024L);

			public static readonly LOS.TupleReference CopiedText = GetTupleReference(550095455563800576L);

			public static readonly LOS.TupleReference Cheat_AreYouSubscribed = GetTupleReference(550095603262021632L);

			public static readonly LOS.TupleReference Cheat_SadlyOnlyForPatrons = GetTupleReference(550095920481427456L);

			public static readonly LOS.TupleReference IDontWantToUseDiscord = GetTupleReference(550098058615971840L);

			public static readonly LOS.TupleReference NotUsingDiscordNotRecommended = GetTupleReference(550098103817986048L);

			public static readonly LOS.TupleReference Cheat_SendToDevelopers = GetTupleReference(550098656807608320L);

			public static readonly LOS.TupleReference Cheat_DisableCheats = GetTupleReference(550101143010664448L);

			public static readonly LOS.TupleReference Cheat_AreYouSureDisable = GetTupleReference(550101607492083712L);

			public static readonly LOS.TupleReference Cheat_InvalidCode = GetTupleReference(550103423273394176L);

			public static readonly LOS.TupleReference Cheat_SuccessBasicCheats = GetTupleReference(550103618782486528L);

			public static readonly LOS.TupleReference Cheat_SuccessAdvancedCheats = GetTupleReference(550103664789807104L);

			public static readonly LOS.TupleReference Cheat_DisableCheatsHelp = GetTupleReference(550104163198951424L);

			public static readonly LOS.TupleReference Cheat_Active = GetTupleReference(550106583442382848L);

			public static readonly LOS.TupleReference Cheat_Status = GetTupleReference(550109001706102784L);

			public static readonly LOS.TupleReference Cheat_BasicCheats = GetTupleReference(550109357429219328L);

			public static readonly LOS.TupleReference Cheat_DisabledCheats = GetTupleReference(550109393877721088L);

			public static readonly LOS.TupleReference Cheat_AdvancedCheats = GetTupleReference(550109429743214592L);

			public static readonly LOS.TupleReference UI_CheatsMainMenu = GetTupleReference(550110490625302528L);

			public static readonly LOS.TupleReference UI_OtherMainMenu = GetTupleReference(550111901958266880L);

			public static readonly LOS.TupleReference CanChangeColorThanksToCheats = GetTupleReference(550134053780578304L);

			public static readonly LOS.TupleReference Cheat_AndMore = GetTupleReference(550138070548078592L);

			public static readonly LOS.TupleReference Cheat_RandomiseModifiers = GetTupleReference(550138237066141696L);

			public static readonly LOS.TupleReference Cheat_RemoveModifiers = GetTupleReference(550140764364988416L);

			public static readonly LOS.TupleReference Cheat_RestoreCum = GetTupleReference(550148455409901568L);

			public static readonly LOS.TupleReference Cheat_IncreaseMaxCum = GetTupleReference(550148500142153728L);

			public static readonly LOS.TupleReference Cheat_CumCheat = GetTupleReference(550148553929908224L);

			public static readonly LOS.TupleReference Cheat_CumRestorationFull = GetTupleReference(550150305165074432L);

			public static readonly LOS.TupleReference Cheat_CumRestorationOverflow = GetTupleReference(550150361016426496L);

			public static readonly LOS.TupleReference Cheat_CumRestorationAlreadyFull = GetTupleReference(550150909765607424L);

			public static readonly LOS.TupleReference Cheat_ResetCumCapacity = GetTupleReference(550151074840829952L);

			public static readonly LOS.TupleReference Cheat_ResetCumCapacitySuccess = GetTupleReference(550151292432932864L);

			public static readonly LOS.TupleReference Cheat_IncreasedCumCapacity = GetTupleReference(550151887713722368L);

			public static readonly LOS.TupleReference Cheat_IncreasedCumCapacityWarning = GetTupleReference(550152587306852352L);

			public static readonly LOS.TupleReference Cheat_AddMoney = GetTupleReference(550157358130520064L);

			public static readonly LOS.TupleReference Cheat_MaxMoneyReached = GetTupleReference(550157731629096960L);

			public static readonly LOS.TupleReference Tooltip_SaveGame = GetTupleReference(550158855799037952L);

			public static readonly LOS.TupleReference Tooltip_ReturnToMainMenu = GetTupleReference(550158882395119616L);

			public static readonly LOS.TupleReference Tooltip_Cheats = GetTupleReference(550158924879224832L);

			public static readonly LOS.TupleReference iamacheater_ButtonNew = GetTupleReference(550162874974068736L);

			public static readonly LOS.TupleReference iamacheater_SadNews = GetTupleReference(550162995329622016L);

			public static readonly LOS.TupleReference Cheats_Other = GetTupleReference(550165115571265536L);

			public static readonly LOS.TupleReference Cheat_ResetChurchPromptWarning = GetTupleReference(550170296421703680L);

			public static readonly LOS.TupleReference WebGL_InputFieldInfo = GetTupleReference(551219030114951168L);

			public static readonly LOS.TupleReference Cheat_AllOfTheBasic = GetTupleReference(551227257611345920L);

			public static readonly LOS.TupleReference SubscribeOn = GetTupleReference(552261364487282688L);

			public static readonly LOS.TupleReference SubscribeNowToAccess = GetTupleReference(552264424500486144L);

			public static readonly LOS.TupleReference Benefit_EarlyAccess = GetTupleReference(552264468259659776L);

			public static readonly LOS.TupleReference Benefit_Devlogs = GetTupleReference(552264495984009216L);

			public static readonly LOS.TupleReference Benefit_ConceptArt = GetTupleReference(552264522567507968L);

			public static readonly LOS.TupleReference Benefit_AndMore = GetTupleReference(552264558722408448L);

			public static readonly LOS.TupleReference DiscordNotExpired = GetTupleReference(552278588459573248L);

			public static readonly LOS.TupleReference Cheat_ChangeFishWeight = GetTupleReference(553314502581411840L);

			public static readonly LOS.TupleReference Cheat_PlayCookiningCheat = GetTupleReference(572861515845066752L);

			public static readonly LOS.TupleReference Cheat_CookInstantlyPrompt = GetTupleReference(572864247184220160L);

			public static readonly LOS.TupleReference Cheat_PlayCookiningCheatSaveScore = GetTupleReference(572864683995815936L);

			public static readonly LOS.TupleReference Cheat_PlayCookiningCheatDiscardScore = GetTupleReference(572864686193631232L);
		}

		public static class Sets
		{
		}

		public const string TABLE_NAME = "UI";

		public static readonly Guid TABLE_GUID = new Guid(new byte[16]
		{
			167, 174, 6, 140, 62, 152, 148, 5, 10, 175,
			173, 153, 110, 239, 109, 214
		});

		public static readonly TableReference TABLE_REFERENCE = TABLE_GUID;

		public static string UI_Achievements => GS(5641477165056L);

		public static string UI_Mods => GS(5874114236416L);

		public static string UI_SHILLING => GS(52622597689344L);

		public static string Room_Status => GS(54939963232256L);

		public static string Room_Equipment => GS(55646338551808L);

		public static string Room_Items => GS(55750864801792L);

		public static string Room_Outside => GS(55832175579136L);

		public static string Room_Sleep => GS(55998030942208L);

		public static string Room_PC => GS(56098648100864L);

		public static string PC_EquipmentShort => GS(60582942085120L);

		public static string PC_Browser => GS(60984836100096L);

		public static string Options_FontColor => GS(78759663112192L);

		public static string Options_Sensitivity_Tooltip => GS(79691826204672L);

		public static string Options_ResetGameData => GS(81463160475648L);

		public static string Achievements_Cheating => GS(1752715424043008L);

		public static string Save_FromFile => GS(1754802484547584L);

		public static string Save_ToFile => GS(1755092688441344L);

		public static string UI_Error => GS(1761228200218624L);

		public static string UI_Success => GS(1761256759234560L);

		public static string UI_Override => GS(1761293052547072L);

		public static string UI_Failed => GS(1761603401682944L);

		public static string SAVE_OverridePrompt => GS(1763006232793088L);

		public static string SAVE_LoadPrompt => GS(1763445565165568L);

		public static string SAVE_LoadErrorPrompt => GS(1763689476526080L);

		public static string UI_AreYouSure => GS(1764674592706560L);

		public static string SAVE_LoadFromFilePrompt => GS(1764968076546048L);

		public static string SAVE_LoadFromFilePrompt2 => GS(1765023525244928L);

		public static string SAVE_ExportSuccess => GS(1765173991706624L);

		public static string SAVE_ExportFailed => GS(1765233185918976L);

		public static string SAVE_ImportFailure => GS(1766569004634112L);

		public static string SAVE_FileExtension => GS(1766739557617664L);

		public static string UI_Delete => GS(1769398180122624L);

		public static string Save_NotesInfo => GS(1771411831918592L);

		public static string Options_ResetGameDataPrompt => GS(1784018898915328L);

		public static string Options_ResetGameDataPrompt2 => GS(1784182296416256L);

		public static string Mods_DataFiles => GS(1785235481956352L);

		public static string Mods_Status => GS(1785407087710208L);

		public static string Mods_Activating => GS(1785885225783296L);

		public static string Mods_NotActivating => GS(1786412189749248L);

		public static string Mods_RestartToActivate => GS(1786541231706112L);

		public static string Mods_RestartToDeactivate => GS(1786564413624320L);

		public static string Mods_Deactivate => GS(1786827136438272L);

		public static string Mods_Activate => GS(1786867661803520L);

		public static string Mods_NoModSelected => GS(1787004014432256L);

		public static string Mods_RestartRequired => GS(1787320051044352L);

		public static string Mods_ClosingTheGame => GS(1787433662156800L);

		public static string Mods_ClosingTheGamePrompt => GS(1787721605320704L);

		public static string Mods_LoadOrder => GS(1789300911771648L);

		public static string Mods_ExportLog => GS(1789456721776640L);

		public static string Mods_Export => GS(1789851787464704L);

		public static string Mods_LoadMods => GS(1789943563030528L);

		public static string Mods_CreateNewMod => GS(1790020499148800L);

		public static string Mods_Edit => GS(1793475519320064L);

		public static string Mods_Status_Exception => GS(1795788409540608L);

		public static string Mods_Status_ExceptionDescription => GS(1795852351705088L);

		public static string Mods_Status_NotLoaded => GS(1795932022509568L);

		public static string Mods_Status_NotLoadedDescription => GS(1795937823232000L);

		public static string Mods_Status_Ready => GS(1796038075486208L);

		public static string Mods_Status_ReadyDescription => GS(1796041699364864L);

		public static string Mods_Status_Active => GS(1796444847476736L);

		public static string Mods_Status_ActiveDescription => GS(1796448366497792L);

		public static string Mods_Status_ActiveButOutdatedDescription => GS(1796689778053120L);

		public static string Mods_Status_ActiveButOutdated => GS(1796688003862528L);

		public static string Mods_Status_Failed => GS(1796889347231744L);

		public static string Mods_Status_FailedDescription => GS(1796891691847680L);

		public static string UI_HungerBarTooltip => GS(2094004392824832L);

		public static string UI_MentalBarTooltip => GS(2094244428648448L);

		public static string UI_StaminaBarTooltip => GS(2094353702850560L);

		public static string Satiation_Full => GS(2096487982829568L);

		public static string Satiation_LittleHungry => GS(2096772633464832L);

		public static string Satiation_Hungry => GS(2096884579438592L);

		public static string Satiation_VeryHungry => GS(2096949058473984L);

		public static string Health_VeryHealthy => GS(2097683539824640L);

		public static string Health_Healthy => GS(2097731199700992L);

		public static string Health_Tired => GS(2097793426395136L);

		public static string Health_NearDeath => GS(2097933532925952L);

		public static string Health_Terrible => GS(2097831221268480L);

		public static string Health_Awful => GS(2097827458977792L);

		public static string Cum_Overfilled => GS(2099237902749696L);

		public static string Cum_Full => GS(2099316139102208L);

		public static string Cum_Half => GS(2099386154618880L);

		public static string Cum_Recovering => GS(2099429599219712L);

		public static string Cum_Empty => GS(2099465959641088L);

		public static string Cum_NoCum => GS(2099764451479552L);

		public static string Frick_CommentsVisible => GS(2114199559737344L);

		public static string Frick_CommentsHidden => GS(2114244979855360L);

		public static string Frick_Unzip => GS(2114513243344896L);

		public static string Frick_HideCock => GS(2114425968267264L);

		public static string Frick_Vagina => GS(2114698392506368L);

		public static string Frick_Anal => GS(2114694688935936L);

		public static string Frick_Manual => GS(2114825995816960L);

		public static string Frick_Slow => GS(2114827686121472L);

		public static string Frick_Medium => GS(2114829372231680L);

		public static string Frick_Fast => GS(2114832597651456L);

		public static string Frick_SlowMode => GS(2115415199059968L);

		public static string Frick_SlowModeDisable => GS(2115796822003712L);

		public static string Frick_SlowModeEnable => GS(2115848101564416L);

		public static string Frick_SlowModeDisabled => GS(2115896130539520L);

		public static string Frick_SlowModeEnabled => GS(2115898215108608L);

		public static string UI_CantDoThat => GS(2116365439602688L);

		public static string Frick_EndFailedPrompt => GS(2116415813193728L);

		public static string Frick_StreamEndPrompt => GS(2116779413213184L);

		public static string Frick_LastStreamSummary => GS(2116905913421824L);

		public static string Frick_EndFrickPrompt => GS(2117694098640896L);

		public static string Frick_ChatStart => GS(2144031383359488L);

		public static string Frick_ChatBanned => GS(2144246949613568L);

		public static string Frick_Position => GS(2146112311791616L);

		public static string Interact_Frick => GS(2147091111350272L);

		public static string Interact_Talk => GS(2147310427312128L);

		public static string Interact_SmallTalk => GS(2147521937674240L);

		public static string Interact_Generic => GS(2147759142342656L);

		public static string Interact_Statistics => GS(2148276371329024L);

		public static string Interact_Nevermind => GS(2148378422939648L);

		public static string Equipment_Zoom => GS(2442436240347136L);

		public static string Equipment_LockPosition => GS(2442551281717248L);

		public static string Equipment_UnlockPosition => GS(2442590787866624L);

		public static string Equipment_EquippedPostfix => GS(2443240200343552L);

		public static string UI_Done => GS(2444114893082624L);

		public static string Equipment_YesReplaceCurrent => GS(2444395538157568L);

		public static string Equipment_SetLoadedFailure => GS(2446030670794752L);

		public static string Equipment_SetFiletype => GS(2446724601614336L);

		public static string Equipment_All => GS(2446868155863040L);

		public static string Equipment_Food => GS(2447029712064512L);

		public static string Equipment_Package => GS(2447128693444608L);

		public static string Equipment_Modules => GS(2447224562651136L);

		public static string Equipment_Clothes => GS(2447265415172096L);

		public static string Equipment_Other => GS(2447384768286720L);

		public static string Equipment_Equipped => GS(2447438627344384L);

		public static string Equipment_NotEquipped => GS(2447476803899392L);

		public static string Equipment_ButtonEquip => GS(2447962097455104L);

		public static string Equipment_ButtonUnequip => GS(2447995454754816L);

		public static string Equipment_UnequipDependencyPrompt => GS(2448396446994432L);

		public static string Equipment_ButtonUse => GS(2448593407315968L);

		public static string Equipment_ButtonThrowAway => GS(2448687506526208L);

		public static string Equipment_ThrowAwayUnequipFirst => GS(2449237694353408L);

		public static string Equipment_UnpackedItems => GS(2449390274744320L);

		public static string Equipment_FromCurrent => GS(2454147609530368L);

		public static string Equipment_FromFile => GS(2454277171580928L);

		public static string Equipment_ButtonLoadSet => GS(2454765636030464L);

		public static string Eqiupment_ButtonExportSet => GS(2454986185117696L);

		public static string Equipment_Set => GS(2455815231246336L);

		public static string Equipment_SetCreationPrompt => GS(2465429096108032L);

		public static string Equipment_SetCreationAllItems => GS(2465591445032960L);

		public static string Equipment_SetCreationSelect => GS(2465725461434368L);

		public static string Equipment_SetCreationSelectPrompt => GS(2465916432289792L);

		public static string Equipment_SetEmpty => GS(2466301146435584L);

		public static string Equipment_SetListHeader => GS(2466378640396288L);

		public static string Equipment_SetEquipping1 => GS(2467492509126656L);

		public static string Equipment_SetEquipping2 => GS(2467605923106816L);

		public static string Equipment_SetEquipping3 => GS(2467812836511744L);

		public static string Equipment_SetEquippingDroppedListHeader => GS(2469383586582528L);

		public static string Equipment_SetEquippingFinished => GS(2469075598839808L);

		public static string Equipment_SetEquippingLeftListHeader => GS(2469555460771840L);

		public static string Equipment_SetEquippingAllItemsFound => GS(2469699606417408L);

		public static string UI_ColorChangeInfo => GS(2482018793164800L);

		public static string UI_ColorChoose => GS(2482856454393856L);

		public static string Equipment_NoItemSelected => GS(2483178690187264L);

		public static string Condition_Trashed => GS(2484800145199104L);

		public static string Condition_VeryHeavilyUsed => GS(2484864238358528L);

		public static string Condition_HeavilyUsed => GS(2484932391604224L);

		public static string Condition_Used => GS(2484937630289920L);

		public static string Condition_NearMint => GS(2484938712420352L);

		public static string Condition_New => GS(2485069838946304L);

		public static string UI_ReturnToMainMenuPrompt => GS(2836390274273280L);

		public static string Shop_NotEnoughMoney => GS(2837005276680192L);

		public static string UI_ImportFile => GS(2837910927577088L);

		public static string UI_ExportFile => GS(2838133380878336L);

		public static string UI_Apply => GS(2838512692760576L);

		public static string UI_Cancel => GS(2838545097953280L);

		public static string UI_Selected => GS(2838828569989120L);

		public static string ColorPicker_CopyFromPreset => GS(2839218262773760L);

		public static string ColorPicker_RemoveInfo => GS(2844882489970688L);

		public static string Shop_Buy => GS(2872136079118336L);

		public static string Shop => GS(2872424458489856L);

		public static string Live2DTool_Hand => GS(3176845411225600L);

		public static string Live2DTool_Mouth => GS(3176881821978624L);

		public static string Live2DTool_Remove => GS(3176929033064448L);

		public static string Interactable_MoveShirt => GS(3180725943537664L);

		public static string Interactable_SuckNipple => GS(3180833070256128L);

		public static string Interactable_PlayBoobs => GS(3181058551844864L);

		public static string Interactable_FuckHer => GS(3181141871693824L);

		public static string Interactable_SuckTongue => GS(3181232678375424L);

		public static string Interactable_KissHer => GS(3181320771342336L);

		public static string Interactable_Headpat => GS(3181493639581696L);

		public static string Interactable_OpenHerMouth => GS(3181570848329728L);

		public static string Interactable_CloseMouth => GS(3181733927063552L);

		public static string Interactable_PlayTongue => GS(3181659931152384L);

		public static string Interactable_MovePanties => GS(3181865342996480L);

		public static string Interactable_MoveSkirt => GS(3182005277560832L);

		public static string Interactable_SuckRightNipple => GS(3182163927109632L);

		public static string Interactable_SuckLeftNipple => GS(3182185431306240L);

		public static string Interactable_PullBothNipples => GS(3182187922722816L);

		public static string Interactable_PullRightNipple => GS(3182189734662144L);

		public static string Interactable_PullLeftNipple => GS(3182191475298304L);

		public static string Interactable_FondleBoobsBra => GS(3182192997830656L);

		public static string Interactable_MovePullPanties => GS(3182196890144768L);

		public static string Interactable_SpreadPussy => GS(3182198572060672L);

		public static string Interactable_EatHerOut => GS(3182200035872768L);

		public static string Interactable_EatHerAssOut => GS(3182201667457024L);

		public static string Interactable_MoveBody => GS(3182203118686208L);

		public static string Shop_bigdaddyhurtshop => GS(3185695065509888L);

		public static string Shop_ConvenienceStore => GS(3186228853608448L);

		public static string Shop_Pharmacy => GS(3186434319978496L);

		public static string Shop_Annalie => GS(3186567791120384L);

		public static string Minigame_Ready => GS(3198124537286656L);

		public static string Minigame_Info => GS(3198278338220032L);

		public static string Minigame_Overtime => GS(3198556772896768L);

		public static string Minigame_Go => GS(3198696980090880L);

		public static string Minigame_Miss => GS(3199298212597760L);

		public static string Minigame_Good => GS(3199320366911488L);

		public static string Minigame_Okay => GS(3199343792099328L);

		public static string Minigame_Perfect => GS(3199409651060736L);

		public static string Loading_GC => GS(3204808756301824L);

		public static string Loading_Finished => GS(3204906265481216L);

		public static string UI_ChooseItems => GS(4247513603117056L);

		public static string UI_ChooseItemsPrompt => GS(4247654141661184L);

		public static string Shop_OrderFinish => GS(4261872622505984L);

		public static string UI_Finish => GS(4262458403196928L);

		public static string Shop_NotOrdered => GS(4262970800345088L);

		public static string Shop_OrderCollectPrompt => GS(4264224402313216L);

		public static string Shop_Collect => GS(4264309144031232L);

		public static string Shop_CanBeCollected => GS(4264324285468672L);

		public static string Shop_OrderPrompt => GS(4264592087584768L);

		public static string Shop_CustomOrders => GS(4272462363496448L);

		public static string Shop_CreateNewOrder => GS(4272747366453248L);

		public static string bangdotic_Mail => GS(4275219812192256L);

		public static string bangdotic_News => GS(4275981124505600L);

		public static string stonksdotic_Title => GS(4289575077679104L);

		public static string stonksdotic_Sell => GS(4290444858888192L);

		public static string stonksdotic_Buy => GS(4290548839878656L);

		public static string stonksdotic_Company => GS(4290699692216320L);

		public static string stonksdotic_NoStocks => GS(4291489542574080L);

		public static string stonksdotic_Income => GS(4292488923582464L);

		public static string stonksdotic_Loss => GS(4292552890912768L);

		public static string stocksdotic_Portfolio => GS(4293268715999232L);

		public static string stocksdotic_PortfolioEmpty => GS(4294177059639296L);

		public static string cocktwitchdotic_StartStreamPrompt => GS(4310433892536320L);

		public static string cocktwitchdotic_Followers => GS(4311278495338496L);

		public static string cocktwitchdotic_DaysAgo => GS(4311379120885760L);

		public static string cocktwitchdotic_stats => GS(4312053221036032L);

		public static string cocktwitchdotic_Leaderboard => GS(4312147097948160L);

		public static string cocktwitchdotic_Home => GS(4312231097274368L);

		public static string cocktwitchdotic_YourProfile => GS(4312380057980928L);

		public static string cocktwitchdotic_FollowersGraph => GS(4312448764874752L);

		public static string casinodotic_NotEnoughTokens => GS(4628376622862336L);

		public static string casinodotic_Single => GS(4629979501301760L);

		public static string casinodotic_Double => GS(4630019972141056L);

		public static string casinodotic_Triple => GS(4630025105969152L);

		public static string casinodotic_ClickToStopWheel => GS(4630434809778176L);

		public static string casinodotic_ClickToStopColumn => GS(4630511871725568L);

		public static string casinodotic_Vertical => GS(4630644147490816L);

		public static string casinodotic_Horizontal => GS(4630694550441984L);

		public static string casinodotic_Diagonal => GS(4630732764745728L);

		public static string casinodotic_PlayerBlackjack => GS(4631424908791808L);

		public static string casinodotic_DoubleBlackjack => GS(4631509755367424L);

		public static string casinodotic_OverLose => GS(4631623106433024L);

		public static string casinodotic_OverWin => GS(4631672779575296L);

		public static string casinodotic_CloserLose => GS(4631737699012608L);

		public static string casinodotic_Draw => GS(4631809358696448L);

		public static string casinodotic_CloserWin => GS(4631841419956224L);

		public static string casinodotic_BuyMoreTokens => GS(4635290396135424L);

		public static string casinodotic_Payou => GS(4635539038670848L);

		public static string casinodotic_Slots => GS(4635694110478336L);

		public static string casinodotic_Blackjack => GS(4635781226172416L);

		public static string casinodotic_Book => GS(4635880111083520L);

		public static string casinodotic_Start => GS(4645856011489280L);

		public static string casinodotic_BlackjackHit => GS(4645994750676992L);

		public static string casinodotic_BlackjackStand => GS(4646092150804480L);

		public static string casinodotic_BlackjackDouble => GS(4646147507228672L);

		public static string casinodotic_leaf => GS(4663911118671872L);

		public static string casinodotic_10 => GS(4664055159459840L);

		public static string casinodotic_jack => GS(4664176815247360L);

		public static string casinodotic_queen => GS(4664290736738304L);

		public static string casinodotic_king => GS(4664427777232896L);

		public static string casinodotic_dollar => GS(4664512145657856L);

		public static string iamacheater_Button => GS(4668579106394112L);

		public static string WebsiteNotFound => GS(4669150752280576L);

		public static string peepeedotic_Text => GS(4669471675256832L);

		public static string duckdotic_Quack => GS(4669867277815808L);

		public static string duckdotic_QUACKexclamationexclamationexclamation => GS(4669977160192000L);

		public static string hotanimebabesdotic_Text => GS(4670230953332736L);

		public static string adredirectdotic_Text => GS(4981858383040512L);

		public static string Room_OutsideWithBot => GS(6830207121887232L);

		public static string Room_OutsideAlone => GS(6830209105793024L);

		public static string bangdotic_email_NoEmailSelected => GS(7201264261533696L);

		public static string bangdotic_email_NoEmails => GS(7201601403883520L);

		public static string bangdotic_email_UnreadPostfix => GS(7201702394335232L);

		public static string Frick_Restore => GS(7595071813586944L);

		public static string Frick_RestorePrompt => GS(7595105242189824L);

		public static string bangdotic_email_Delete => GS(9397964988354560L);

		public static string bangdotic_email_DeletePrompt => GS(9398025864482816L);

		public static string pixelglassesdotic_BuyNow => GS(9403561913749504L);

		public static string pixelglassesdotic_Discount => GS(9404716110712832L);

		public static string pixelglassesdotic_NotEnoughMoney => GS(9405293699928064L);

		public static string pixelglassesdotic_GoAway => GS(9405459827920896L);

		public static string pixelglassesdotic_StopWastingTime => GS(9405532750090240L);

		public static string pixelglassesdotic_Dumb => GS(9405643618127872L);

		public static string pixelglassesdotic_AlreadyOwn => GS(9405699050049536L);

		public static string pixelglassesdotic_What => GS(9405799486853120L);

		public static string pixelglassesdotic_WhatPrompt => GS(9405825323765760L);

		public static string pixelglassesdotic_SoulHarvest => GS(9405969612017664L);

		public static string pixelglassesdotic_SoulHarvestPrompt => GS(9406023039062016L);

		public static string pixelglassesdotic_SoulNotTaken => GS(9406142497034240L);

		public static string pixelglassesdotic_SoulNotTakenPrompt => GS(9406180866527232L);

		public static string pixelglassesdotic_ThrewOut => GS(9406311892389888L);

		public static string pixelglassesdotic_SoulTaken => GS(9406417932783616L);

		public static string pixelglassesdotic_Get => GS(9406571473670144L);

		public static string Slot_LegL => GS(28872469346283520L);

		public static string Slot_LegR => GS(28872529521963008L);

		public static string Slot_ShoeL => GS(28872531589754880L);

		public static string Slot_ShoeR => GS(28872604117659648L);

		public static string Slot_StockingL => GS(28876294375370752L);

		public static string Slot_StockingR => GS(28876223722319872L);

		public static string Slot_ArmL => GS(28876357243793408L);

		public static string Slot_ArmR => GS(28876521857642496L);

		public static string Slot_Skin => GS(28876616858628096L);

		public static string Slot_Eyes => GS(28876642221584384L);

		public static string Slot_Flesh => GS(28876771519393792L);

		public static string Slot_Head => GS(28876866503602176L);

		public static string Slot_Teeth => GS(28876952067403776L);

		public static string Slot_Hair => GS(28877006232645632L);

		public static string Slot_Shirt => GS(28877060272058368L);

		public static string Slot_Skirt => GS(28877154362880000L);

		public static string Slot_Bra => GS(28877218342793216L);

		public static string Slot_Panties => GS(28877297912934400L);

		public static string Slot_HairAccessoryL => GS(28877364140994560L);

		public static string Slot_Anon_Glasses => GS(28878144751300608L);

		public static string UI_RentToday => GS(28884866534502400L);

		public static string Options_MusicPopup => GS(29617154138337280L);

		public static string Equipment_CannotChangeOrderWhileSortedPrompt => GS(29734279280619520L);

		public static string Equipment_SortDescending => GS(29734444372619264L);

		public static string Equipment_SortAscending => GS(29734477662810112L);

		public static string Equipment_SortQuality => GS(29734557237145600L);

		public static string Equipment_SortPrice => GS(29734614137073664L);

		public static string Equipment_SortSlot => GS(29734664636493824L);

		public static string Equipment_SortColor => GS(29734729774034944L);

		public static string Equipment_SortAlphabetically => GS(29734773965221888L);

		public static string Equipment_SortNoSort => GS(29734845788483584L);

		public static string UI_GameInstruction => GS(31429975663841280L);

		public static string Equipment_Can_tEatTitle => GS(35048148581617664L);

		public static string Equipment_Can_tEatPrompt => GS(35048187899023360L);

		public static string Naming_TooShort => GS(35060771767324672L);

		public static string Naming_TooLong => GS(35060841048838144L);

		public static string Frick_PullOutFirst => GS(36665627488579584L);

		public static string Frick_Request => GS(36857017535328256L);

		public static string Frick_MusicOn => GS(37006396653072384L);

		public static string Frick_MusicOff => GS(37006427472818176L);

		public static string Frick_Blowjob_Nevermind => GS(37739390984548352L);

		public static string Frick_Blowjob_SwallowCum => GS(37739555132829696L);

		public static string Frick_Blowjob_StopAfterCumming => GS(37740242814136320L);

		public static string Frick_Blowjob_ContinueAfterCumming => GS(37740319083360256L);

		public static string Frick_Blowjob_LetMeCum => GS(37740430576349184L);

		public static string Frick_Blowjob_DontLetMeCum => GS(37740477418336256L);

		public static string Frick_Blowjob_Fast => GS(37740606896500736L);

		public static string Frick_Blowjob_Slow => GS(37740608968486912L);

		public static string Frick_Blowjob_LickTip => GS(37740610595876864L);

		public static string Frick_Blowjob_Stop => GS(37740717030535168L);

		public static string Interactable_ThrustHips => GS(37740994458578944L);

		public static string Interactable_GrabHead => GS(37741213023760384L);

		public static string Interact_PlayGame => GS(38039561060511744L);

		public static string Interact_OldMaid => GS(38042541298053120L);

		public static string OldMaid_OpponentPicking => GS(38117090177871872L);

		public static string OldMaid_PlayerPicking => GS(38117310177505280L);

		public static string OldMaid_RemovePairs => GS(38117454897770496L);

		public static string OldMaid_Instruction => GS(38117500364025856L);

		public static string OldMaid_StopPrompt => GS(38340543652851712L);

		public static string Frick_FlacidFirstTime => GS(41000833755516928L);

		public static string Options_KeepRunningWhenMinimzed => GS(41278844509753344L);

		public static string Blowjob_PreferInside => GS(41349597364097024L);

		public static string Blowjob_PreferOutside => GS(41349599742267392L);

		public static string Blowjob_Automatic => GS(41349591252996096L);

		public static string GameName => GS(41361575591677952L);

		public static string Slot_EyeShadow => GS(41709500691779584L);

		public static string Slot_NeckAccessory => GS(41656432306556928L);

		public static string Slot_Glasses => GS(41667221952270336L);

		public static string Slot_EyeMakeup => GS(41709550939541504L);

		public static string ladypartsdotic_PurchaseComplete => GS(46297168025096192L);

		public static string Loading_FailedProbablyBecauseOfModsPrompt => GS(46305052108656640L);

		public static string Loading_FailedNoIdeaWhy => GS(46305053757018112L);

		public static string Loading_IgnoreIfClosedGame => GS(46345947193413632L);

		public static string Slot_Ears => GS(46801794386235392L);

		public static string Slot_Mat => GS(46801805190762496L);

		public static string Slot_Plush => GS(46801981087289344L);

		public static string Slot_Banner => GS(46802008576757760L);

		public static string Slot_WallDecoration => GS(46802026645819392L);

		public static string Equipment_SetLoadedFailureTryingToLoadMod => GS(47014647600910336L);

		public static string Slot_Septum => GS(47396080739815424L);

		public static string UI_AutoSave => GS(76498690067271680L);

		public static string UI_Reward => GS(77960632552263680L);

		public static string Requirements_OrGate => GS(77968724316786688L);

		public static string UI_Requirements => GS(77971207130845184L);

		public static string Reward_OnSuccess => GS(77971499981344768L);

		public static string Reward_OnFailure => GS(77971552632442880L);

		public static string Frick_NewDonation => GS(90675915951837184L);

		public static string Reward_NoReward => GS(77988725115457536L);

		public static string Contract_OnAcceptBonus => GS(77989245775384576L);

		public static string UI_Contract => GS(78005752894984192L);

		public static string Contract_AreYouSureYouWantToAccept => GS(78005823376068608L);

		public static string Contract_RememberOnlyOne => GS(78008542123921408L);

		public static string Contract_NoContract => GS(78013682130227200L);

		public static string Contract_AbandonButton => GS(78017373298102272L);

		public static string Contract_CompleteButton => GS(78018006554120192L);

		public static string Contract_AreYouSureYouWantToAbandonCurrent => GS(78019729460957184L);

		public static string Contract_Failed => GS(78019823845380096L);

		public static string Contract_CompletionPossible => GS(78019951792623616L);

		public static string Contract_CompletePromptTitle => GS(78022746088280064L);

		public static string Contract_CompletePrompt => GS(78022906289721344L);

		public static string Requirements_OutOfTime => GS(78040863174692864L);

		public static string Requirements_AndGate => GS(78189863643254784L);

		public static string Requirements_Done => GS(78215051860643840L);

		public static string Requirements_AlwaysTrue => GS(78224114250027008L);

		public static string Requirements_InOneStreamCompound => GS(78261032882933760L);

		public static string Position_Missionary => GS(78302377395752960L);

		public static string Position_Doggy => GS(78302451840454656L);

		public static string Position_Blowjob => GS(78302481456435200L);

		public static string Contract_CurrentContract => GS(78335096897015808L);

		public static string Requirement_Or => GS(79833939971018752L);

		public static string Delivery_NumberTitle => GS(80137031224528896L);

		public static string Delivery_SenderTitle => GS(80137078762770432L);

		public static string Delivery_ArrivalTimeTitle => GS(80137116549255168L);

		public static string Delivery_ContentsTitle => GS(80137169988882432L);

		public static string Delivery_StatusTitle => GS(80137278709436416L);

		public static string Delivery_DeliveryTypeTitle => GS(80484645652533248L);

		public static string Delivery_PremiumDelivery => GS(80485045986267136L);

		public static string Delivery_StandardDelivery => GS(80485094212374528L);

		public static string Delivery_BuyPremium => GS(80488503632072704L);

		public static string Delivery_NoDeliveries => GS(80498249911111680L);

		public static string UI_NarrativeLog => GS(80847789147164672L);

		public static string UI_ItemDescription => GS(84754642570711040L);

		public static string Mods_ModDescription => GS(86983942145445888L);

		public static string Mods_ModCreatorPopup => GS(87031642605883392L);

		public static string Frick_StreamInfo => GS(88789582526418944L);

		public static string Frick_ViewersTooltip => GS(88794441371750400L);

		public static string Frick_FollowersTooltip => GS(88794615393423360L);

		public static string Frick_SubsTooltip => GS(88794619596115968L);

		public static string UI_AreYouSureYouWantToQuit => GS(90576802618937344L);

		public static string UI_ColorSelection_CancelChange => GS(91708102876729344L);

		public static string Equipment_Modded => GS(92402373011312640L);

		public static string Equipment_Vanilla => GS(92402405940793344L);

		public static string Equipment_SearchForItemByName => GS(93095892050907136L);

		public static string Position_Cowgirl => GS(93855167043997696L);

		public static string Position_Interaction => GS(94586810725699584L);

		public static string Frick_LegsRequired => GS(98489303369125888L);

		public static string Frick_CowgirlRequest_Fast => GS(98791638976827392L);

		public static string Frick_CowgirlRequest_Slow => GS(98791757939871744L);

		public static string Frick_CowgirlRequest_Stop => GS(98791785311899648L);

		public static string Frick_CowgirlRequest_Pullout => GS(99146374619992064L);

		public static string Frick_CowgirlRequest_Insert => GS(99146376071221248L);

		public static string Requirement_ItemIncludedInOnAcceptBonus => GS(100693834530164736L);

		public static string Equipment_Contract => GS(100713655892316160L);

		public static string Equipment_ItemIsInCurrentContract => GS(100715059465818112L);

		public static string cocktracts_header_home => GS(100726093731729408L);

		public static string cocktracts_header_available => GS(100727667761745920L);

		public static string cocktracts_header_history => GS(100727703828566016L);

		public static string cocktracts_NoAvailableContracts => GS(102504297911947264L);

		public static string cocktracts_NoHistoryContracts => GS(102504301103812608L);

		public static string Mods_OpenModFolder => GS(103219101073018880L);

		public static string Equipment_MissingItem => GS(103223837037826048L);

		public static string Requirement_OnlyProvidedItemCounts => GS(103327646636445696L);

		public static string Requirement_AnyItemCounts => GS(103328220698251264L);

		public static string Requirement_OnlyCertainItemWorks => GS(103329181135147008L);

		public static string Requirement_ColorsMustMatch => GS(103329570618216448L);

		public static string Contract_Partners => GS(103597203649335296L);

		public static string cocktracts_header_partners => GS(103997420844486656L);

		public static string Rewards_RespectBasicallyNone => GS(104348586640019456L);

		public static string Rewards_RespectTinyAmount => GS(104348686762250240L);

		public static string Rewards_RespectNormalAmount => GS(104348751757185024L);

		public static string Rewards_RespectDecentAmount => GS(104348877410144256L);

		public static string Rewards_RespectBigAmount => GS(104348879599570944L);

		public static string Rewards_RespectHugeAmount => GS(104348883156340736L);

		public static string Rewards_RespectGiganticAmount => GS(104348952320413696L);

		public static string cocktracts_Partner_NotUnlocked => GS(104683857613033472L);

		public static string Contract_GenericTitle => GS(105066058041135104L);

		public static string Contract_GenericNarration => GS(105067472167505920L);

		public static string Contact_HasOtherRewardShort => GS(105070972372914176L);

		public static string Contract_NoRewards => GS(105072268601593856L);

		public static string Contract_RewardShort => GS(105073804811571200L);

		public static string cocktracts_NewContract => GS(105817092137029632L);

		public static string cocktracts_CurrentlyAvailableContracts => GS(105848347574538240L);

		public static string Contracts_GoldContract => GS(119378088885620736L);

		public static string WebGLWarningMightLoseSavePrompt => GS(107250247087820800L);

		public static string UI_DontShowTheWarningAgain => GS(107251903930175488L);

		public static string Options_DeviceDetected => GS(110884429420695552L);

		public static string Options_DriverDetected => GS(110884559582531584L);

		public static string Options_DriverNotDetected => GS(110884689845030912L);

		public static string Options_Connect => GS(110884765585772544L);

		public static string Options_Calibrate => GS(110884795004620800L);

		public static string Options_Connecting => GS(110884842836463616L);

		public static string Options_Calibrating => GS(110885184496078848L);

		public static string Options_WaitingForDriver => GS(110885405749809152L);

		public static string Options_CalibrationPrompt => GS(110886562241056768L);

		public static string Options_CalibrationAreYouSure => GS(110886826775810048L);

		public static string Options_CalibrationValues => GS(110887030639955968L);

		public static string Options_CalibrationComplete => GS(110887155391139840L);

		public static string Options_TryingToConnect => GS(110887212823744512L);

		public static string cocktracts_AcceptContract => GS(111323846547476480L);

		public static string cocktracts_RejectContract => GS(111511411909070848L);

		public static string Contract_AreYouSureYouWantToReject => GS(111514635026173952L);

		public static string Contract_AvailableContract => GS(111516167872319488L);

		public static string Contract_AcceptedContract => GS(111533608618823680L);

		public static string Contract_ExpiredContract => GS(111533613798789120L);

		public static string Contract_RejectedContract => GS(111533614545375232L);

		public static string Contract_CompletedContract => GS(111533615140966400L);

		public static string Contract_FailedContract => GS(111534575334592512L);

		public static string Contract_AbandonedContract => GS(111534577532407808L);

		public static string cocktracts_NoRelatedContracts => GS(111665695493591040L);

		public static string cocktracts_AbandonContract => GS(111684337090301952L);

		public static string cocktracts_CompleteContract => GS(111684421555195904L);

		public static string Slot_LowerBody => GS(112797584455503872L);

		public static string Slot_UpperBodyOuter => GS(112799150298238976L);

		public static string Slot_UpperBody => GS(112801253397762048L);

		public static string Interactable_MoveHoodie => GS(113409013214744576L);

		public static string Interactable_RemovePutOnPants => GS(113472541338505216L);

		public static string PathCopiedToClipboard => GS(115602100355731456L);

		public static string Mods_RelatedModNotFound => GS(115604257586941952L);

		public static string UI_ChangeColor => GS(115604514160906240L);

		public static string Mods_OpenModWorkingDirectory => GS(115604614488657920L);

		public static string Mods_ReloadTextures => GS(115605370902024192L);

		public static string Frick_CowgirlRequest_BotLoad => GS(117175207793135616L);

		public static string AddItem => GS(118484088477585408L);

		public static string CheckItOut => GS(119007846246256640L);

		public static string Requirement_YouDontHaveAnyMerch => GS(119322904511778816L);

		public static string Contracts_GoldContractTooltip => GS(119378233735909376L);

		public static string UI_LessThanAnHour => GS(119617920362745856L);

		public static string Contract_FailedPrompt => GS(119771359088611328L);

		public static string ReloadedAllTexturesPrompt => GS(123949974063812608L);

		public static string Frick_LegsAndArmsRequired => GS(121127165742682112L);

		public static string NunRepair_NoValidItems => GS(121783962669289472L);

		public static string NunRepair_Repair => GS(121814217182285824L);

		public static string NunRepair_MaxQuality => GS(121815228538032128L);

		public static string NunRepair_AlreadyRepaired => GS(121817020021739520L);

		public static string Cheat_ResetChurch => GS(122323925517340672L);

		public static string Cheat_ResetChurchPrompt => GS(122324301981290496L);

		public static string Cheat_ResetChurchDone => GS(122324738528645120L);

		public static string cocktracts_header_help => GS(123602581472546816L);

		public static string cocktracts_help_description => GS(123607893294321664L);

		public static string cocktracts_header_description => GS(123612752278065152L);

		public static string NoFileAccessPrompt => GS(131190198519074816L);

		public static string NoFileAccessPrompt2 => GS(131193600770707456L);

		public static string Mods_Status_ActiveButGameOutdated => GS(131208725485457408L);

		public static string Mods_Status_ActiveButGameOutdatedDescription => GS(131208764622508032L);

		public static string Options_NoToy => GS(142777605022420992L);

		public static string Options_Other => GS(142783772649984000L);

		public static string Options_ToyLabel => GS(142787469446615040L);

		public static string Options_ToySettings => GS(142807620820799488L);

		public static string Options_ListOfSupportedToys => GS(142813080831528960L);

		public static string Options_CurrentToy => GS(142813670638751744L);

		public static string Toy_AGui => GS(142816138198126592L);

		public static string Options_DisconnectToy => GS(142823013287333888L);

		public static string Options_AlreadyConnectedPrompt => GS(142848208458878976L);

		public static string Options_CalibrationPrompt2 => GS(142853923147747328L);

		public static string Options_ToySettingsTooltip => GS(143124457693515776L);

		public static string UI_ChangeQuality => GS(148476157488926720L);

		public static string RecentSave => GS(161906883278278656L);

		public static string Shop_TryOn => GS(179782432629592064L);

		public static string ChangingRoom => GS(179879880014303232L);

		public static string ReturnChangingRoomItem => GS(180108504285974528L);

		public static string ItemBorrowedFromAnnalieDescription => GS(180123564106452992L);

		public static string ItemBorrowedShort => GS(180124503123038208L);

		public static string ReturnToDefault => GS(181229886776414208L);

		public static string ReturnToDefaultPrompt => GS(181230290838884352L);

		public static string Undress => GS(181265240959094784L);

		public static string UndressPrompt => GS(181265560342761472L);

		public static string Unpack => GS(181276072157777920L);

		public static string Options_CalibrationWrongPrompt => GS(182153022174793728L);

		public static string Options_NeedsCalibration => GS(182155339737468928L);

		public static string Slot_HeartPupils => GS(182945513559953408L);

		public static string DebugModePrompt => GS(183616489851326464L);

		public static string DebugModeEnabledPrompt => GS(183631250118807552L);

		public static string Options_DisableDebugButton => GS(183633597637201920L);

		public static string Options_DisableDebugPrompt => GS(183634906121637888L);

		public static string ThankYou => GS(183678134879391744L);

		public static string ClickToOpen => GS(183687878033674240L);

		public static string Interact_Action => GS(185142908448280576L);

		public static string Cuddle_GetCloser => GS(185154362496913408L);

		public static string Cuddle_MoveAway => GS(185154407791202304L);

		public static string Slot_Tail => GS(186677922902183936L);

		public static string bangdotic_DeleteSpam => GS(187383148432986112L);

		public static string bangdotic_DeleteSpamPrompt => GS(187383311713046528L);

		public static string Live2DTool_TabTip => GS(187424961176932352L);

		public static string Interactable_MoveTail => GS(189924979553210368L);

		public static string PlayerName => GS(193471983856259072L);

		public static string PlayerName_DefaultMaster => GS(193471986729357312L);

		public static string PlayerName_Prompt => GS(193471985475260416L);

		public static string BotName => GS(193485218361774080L);

		public static string BotName_Prompt => GS(193485220102410240L);

		public static string BotName_Default => GS(193485221486530560L);

		public static string Interact_Other => GS(193514289871327232L);

		public static string ChangeBotName => GS(193515065645600768L);

		public static string ChangePlayerName => GS(193515119747928064L);

		public static string Frick_CowgirlRequest_BegStop => GS(203832649683177472L);

		public static string Interactable_MoveBlanket => GS(205536974418731008L);

		public static string Interactable_RubHerFace => GS(205536977090502656L);

		public static string Interactable_HoldHerHand => GS(205537104119193600L);

		public static string Cuddle_GoToSleep => GS(205538471843979264L);

		public static string Cuddle_AskToStopHeadpats => GS(205545741474586624L);

		public static string Cuddle_EndPrompt => GS(207592949275418624L);

		public static string Slot_Lips => GS(207911663787040768L);

		public static string Slot_PersonalityModule => GS(208385084266373120L);

		public static string Equipment_PersonalityModuleInfo => GS(208756289724997632L);

		public static string Equipment_PersonalityModules => GS(210961474161467392L);

		public static string Equipment_HighQuality => GS(210961814600540160L);

		public static string Equipment_Under20k => GS(210962206369505280L);

		public static string Cuddle_AskForHeadpats => GS(214844375504510976L);

		public static string Cuddle_AskToSaySomethingNice => GS(214855388001984512L);

		public static string Options_DeviceNotDetected => GS(214897433626271744L);

		public static string Options_WaitingForDevice => GS(110885475282980864L);

		public static string Mods_ApproximateExtractedSize => GS(216000683855306752L);

		public static string Mods_ExactlySameVersion => GS(216005188114178048L);

		public static string Mods_UpdateYourGame => GS(216005754861117440L);

		public static string New_Entry_1 => GS(216018123439988736L);

		public static string OutdatedVersionPrompt => GS(217497412949762048L);

		public static string Equipment_Medicine => GS(217577985613488128L);

		public static string Error_GameCannotSave => GS(218218778581786624L);

		public static string AGUI_UseDriverDepth => GS(233121712609038336L);

		public static string AGUI_UseDriverDepth_Tooltip => GS(233121787800326144L);

		public static string SecretRoomPrompt => GS(243536364995338240L);

		public static string Room_Cook => GS(246153693377847296L);

		public static string Interactable_MoveLegs => GS(252905369350692864L);

		public static string Cooking_SelectedSong => GS(263867424090087424L);

		public static string Cooking_Results => GS(263867923883352064L);

		public static string FishingGear => GS(268938463266463744L);

		public static string FishingProgress => GS(268938574251941888L);

		public static string FishingFishingRods => GS(268942624322215936L);

		public static string FishingResults => GS(269257882794450944L);

		public static string Fishing => GS(269289693771718656L);

		public static string Fishing_StartFishing => GS(269511379708272640L);

		public static string Fishing_FishingSpot_Cleanliness => GS(269526879226028032L);

		public static string Fishing_FishingSpot_FishQuantity => GS(269526934053969920L);

		public static string Fishing_FishingSpot_FishSize => GS(269526988798025728L);

		public static string Fishing_FishingLocation => GS(269533393185660928L);

		public static string SelectItem => GS(269542812636143616L);

		public static string NoValidItemsFound => GS(269564918052675584L);

		public static string FishingFishingRod => GS(269569491827941376L);

		public static string FishingBait => GS(269572895136538624L);

		public static string FishingNoFishinRodEquipped => GS(269573447031447552L);

		public static string FishingNoBaitEquipped => GS(269573606360473600L);

		public static string UI_Change => GS(269575506841559040L);

		public static string FishingNoFishingRodEquippedPrompt => GS(273571584536469504L);

		public static string CookingSong_BPM => GS(273953207970947072L);

		public static string CookingSong_Difficulty => GS(273953175725137920L);

		public static string CookingSong_Length => GS(273953146817994752L);

		public static string CookingSong_MinBPM => GS(273955578268606464L);

		public static string CookingSong_MaxBPM => GS(273955582001537024L);

		public static string CookingSong_MissingIngredient => GS(274253650940911616L);

		public static string Fishing_TimeEnd => GS(276193668341161984L);

		public static string Fishing_FishingResults => GS(276485746140897280L);

		public static string Fishing_FishingResultsNothing => GS(276485998394728448L);

		public static string Interactable_Fingering => GS(277099352406614016L);

		public static string Interactable_ThumbInMouth => GS(277130913491443712L);

		public static string Interactable_PlayBoobL => GS(279351619797114880L);

		public static string Interactable_PlayBoobR => GS(279351690819264512L);

		public static string Interactable_ClitRub => GS(280796031672639488L);

		public static string Cooking_Input => GS(281130209593253888L);

		public static string Cooking_CookingVisualizationTitle => GS(281132009436209152L);

		public static string Interactable_GrabAss => GS(281169972673110016L);

		public static string Interactable_GrabHea => GS(281170013890535424L);

		public static string Interactable_MoveDress => GS(283365635473268736L);

		public static string FishingJournal => GS(285858811819843584L);

		public static string Fishing_NeverCaught => GS(285884902534012928L);

		public static string FishSize_10Percentile => GS(286219584098263040L);

		public static string FishSize_20Percentile => GS(286223013008891904L);

		public static string FishSize_30Percentile => GS(286223014862774272L);

		public static string FishSize_40Percentile => GS(286223015605166080L);

		public static string FishSize_60Percentile => GS(286223017555517440L);

		public static string FishSize_70Percentile => GS(286223019375845376L);

		public static string FishSize_80Percentile => GS(286223081359269888L);

		public static string FishSize_90Percentile => GS(286223083179597824L);

		public static string FishSize_100Percentile => GS(286223106114052096L);

		public static string Item_MarkAsFavourite => GS(286958548862099456L);

		public static string Item_UnmarkAsFavourite => GS(286958671264473088L);

		public static string Item_Favourite => GS(286958768836567040L);

		public static string Cooking_ScoreLabel => GS(287281353327632384L);

		public static string Cooking_AccuracyLabel => GS(287349996405121024L);

		public static string FishingWebsite_TipsFromThePros => GS(288460403148578816L);

		public static string FishingWebsite_Name => GS(328038276087668736L);

		public static string FishingWebsite_NoTips => GS(288780499117137920L);

		public static string ClsCommandDescription => GS(291674740382228480L);

		public static string CowsayCommandDescription => GS(291674841934716928L);

		public static string SummaryCommandDescription => GS(291675429393129472L);

		public static string ExitCommandDescription => GS(291676145096581120L);

		public static string HelpCommandDetailedInfoHeader => GS(291678804859936768L);

		public static string DateCommandDescription => GS(291686735630295040L);

		public static string CowsayJunFlagDescription => GS(291707417571352576L);

		public static string CowsayAnnalieFlagDescription => GS(291707485191921664L);

		public static string CowsayGayFlagDescription => GS(291707563273084928L);

		public static string DateCommandDayOfWeekFlagDescription => GS(291708251470290944L);

		public static string CommandFlagsNone => GS(291710365869268992L);

		public static string CommandFlagsList => GS(291710458894737408L);

		public static string HelpCommandCommandNameHeader => GS(291712261178777600L);

		public static string HelpCommandDescriptionHeader => GS(291712367357583360L);

		public static string CommandInputNothing => GS(291714070983204864L);

		public static string StatusAppDisconnect => GS(291714617454878720L);

		public static string SnakeCommandDescription => GS(291766193762590720L);

		public static string SnakeCommandHeader => GS(291786284394864640L);

		public static string CubeCommandGradientCustom => GS(292071315659022336L);

		public static string CubeCommandDescription => GS(292081865256329216L);

		public static string StyleCommandDescription => GS(292088042878918656L);

		public static string StyleCommandHeader => GS(292088106003193856L);

		public static string StyleCommandInfo => GS(292088243299540992L);

		public static string StyleCommandNextTextColor => GS(292090165658116096L);

		public static string StyleCommandNextBackgroundColor => GS(292090204983910400L);

		public static string StyleCommandDefaultColors => GS(292099808820191232L);

		public static string StyleCommandSaving => GS(292103942780739584L);

		public static string StyleCommandSavingDone => GS(292104049936818176L);

		public static string StyleCommandRandomColors => GS(292112160504406016L);

		public static string EightBallCommandYes => GS(292117632015917056L);

		public static string EightBallCommandNo => GS(292117690597761024L);

		public static string EightBallCommandMaybe => GS(292117708234809344L);

		public static string EightBallCommandDefinitely => GS(292117767928143872L);

		public static string EightBallCommandIDontThinkSo => GS(292117842712584192L);

		public static string EightBallCommandAbsolutely => GS(292117876732583936L);

		public static string EightBallCommandNotLikely => GS(292117950413922304L);

		public static string EightBallCommandTryAgainLater => GS(292118014037319680L);

		public static string EightBallCommandDescription => GS(292119612482379776L);

		public static string CurrentlyLoading => GS(292459566337089536L);

		public static string LogExportSuccessful => GS(292497579461042176L);

		public static string LogExportFailed => GS(292497790078017536L);

		public static string ErrorOccured => GS(294207689531056128L);

		public static string PlayVideoLoadingCancelled => GS(294219519695511552L);

		public static string PlayVideoCurrentGradient => GS(294219566034182144L);

		public static string PlayVideoVideoSize => GS(294219611206836224L);

		public static string PlayVideoTurnOnLooping => GS(294219798847414272L);

		public static string PlayVideoTurnOffLooping => GS(294219802634870784L);

		public static string PlayVideoSkip => GS(294220167136665600L);

		public static string PlayVideoDescription => GS(294220377594257408L);

		public static string EasterEgg => GS(294220554702938112L);

		public static string PlayVideoInvalidPath => GS(294220909868212224L);

		public static string ExpensiveItemModifier => GS(294310927785779200L);

		public static string ExpensiveItemModifierDescription => GS(294311088398262272L);

		public static string PopularItemModifier => GS(294601542884909056L);

		public static string PopularItemModifierDescription => GS(294601693305233408L);

		public static string StreamersItemModifier => GS(294607100912328704L);

		public static string StreamersItemModifierDescription => GS(294607140934377472L);

		public static string ItemModifierInfo => GS(294625452372254720L);

		public static string ItemModifierLevel => GS(294627986285518848L);

		public static string ItemModifierListHeader => GS(294630602709131264L);

		public static string ItemModifierCursed => GS(294641044772872192L);

		public static string ItemModifierFastFishing => GS(294830347943665664L);

		public static string ItemModifierFastFishingDescription => GS(294830370198642688L);

		public static string ItemModifierWideFishing => GS(294830600080056320L);

		public static string ItemModifierWideFishingDescription => GS(294830603439693824L);

		public static string ClickAnywhereToStartFishing => GS(294878476382494720L);

		public static string ThrowMinigameExplain => GS(294889799619174400L);

		public static string CookingSong_MissingIngredients => GS(294910130442461184L);

		public static string CookingSong_ReadyToCook => GS(294911354013212672L);

		public static string CookingSong_IngredientList => GS(294911821199958016L);

		public static string CookingSong_ModifierEffects => GS(294915607926661120L);

		public static string CookingSong_ModifierEffectsNone => GS(294917261694590976L);

		public static string CookingSong_Modifiers => GS(294918534284173312L);

		public static string CookingSong_ModifierSlowMode => GS(294918660834713600L);

		public static string CookingSong_ModifierFastMode => GS(294918688034775040L);

		public static string CookingSong_ModifierUltraFastMode => GS(294918750383104000L);

		public static string CookingSong_ModifierInsaneMode => GS(294918789377548288L);

		public static string CookingSong_NoModifiers => GS(294918963223060480L);

		public static string CookingSong_NoActiveModifiers => GS(294919766151258112L);

		public static string CookingSong_ActiveModifiers => GS(294919900985548800L);

		public static string CookingSong_ModifierPractice => GS(294923164196003840L);

		public static string CookingSong_ModifierPracticeInfo => GS(294924161282727936L);

		public static string CookingSong_Play => GS(294925626919346176L);

		public static string CookingSong_PracticePlayPrompt => GS(294925901008723968L);

		public static string CookingMinigame_Great => GS(296007040863559680L);

		public static string CookingMinigame_Good => GS(296007088561184768L);

		public static string CookingMinigame_Miss => GS(296007090285043712L);

		public static string CookingMinigame_MaxCombo => GS(296007091706912768L);

		public static string CookingMinigame_Score => GS(296007092784848896L);

		public static string CookingMinigame_Accuracy => GS(296007245277159424L);

		public static string CookingMinigame_Bad => GS(296007651256426496L);

		public static string CookingMinigame_CookedDish => GS(296032299150991360L);

		public static string CookingMinigame_NewRecord => GS(296035465368166400L);

		public static string CookingMinigame_Incomplete => GS(296036171634434048L);

		public static string CookingMinigame_LostIngredients => GS(296037921447731200L);

		public static string CookingMinigame_ModifierPracticeInfoResults => GS(296038257164017664L);

		public static string CookingMinigame_Results => GS(296038954961985536L);

		public static string CookingMinigame_TimesPlayed => GS(296040594465087488L);

		public static string CookingMinigame_TimesFinished => GS(296040645044199424L);

		public static string CookingMinigame_BestScore => GS(296040681379454976L);

		public static string CookingMinigame_BestAccuracy => GS(296040730045964288L);

		public static string ItemName => GS(296065819206475776L);

		public static string ItemDescription => GS(296065843676045312L);

		public static string ItemDescription_Ingredients => GS(327250349971726336L);

		public static string CookingMinigame_RecipeExecutionMultipleItems => GS(296457857605296128L);

		public static string CookingMinigame_ItemSelectedForTheRecipeExecution => GS(296458707115433984L);

		public static string Items_SelectedItems => GS(296808590854840320L);

		public static string Items_NoItemsSelected => GS(296815914730381312L);

		public static string UI_Continue => GS(296817701986230272L);

		public static string UnselectOthers => GS(297142663217405952L);

		public static string ItemSelectionCantSelect => GS(297151815499112448L);

		public static string CurrentSelection => GS(297154642904604672L);

		public static string SelectNoneShort => GS(297156319145615360L);

		public static string SelectAllShort => GS(297156343434829824L);

		public static string Equipment_ButtonEat => GS(297212801987895296L);

		public static string FoodSatiation_Label => GS(297481849556426752L);

		public static string CookingMinigame_Grade => GS(298962322543599616L);

		public static string ShillingRatingPrompt => GS(298999654550331392L);

		public static string ClsResult => GS(299705356344352768L);

		public static string Shop_RichPeopleShopName => GS(300063969743200256L);

		public static string ColorPalletes => GS(302589938785837056L);

		public static string Colors => GS(302590454408404992L);

		public static string CurrentColor => GS(302627257874112512L);

		public static string YourColorPresets => GS(302635637791514624L);

		public static string ColorPicker => GS(302651357866745856L);

		public static string Equipment_SetUsedMods => GS(306476096737501184L);

		public static string Equipment_CannotEquipItemsToRemove => GS(306568702104428544L);

		public static string Equipment_CannotEquipPopupTitle => GS(306560179287089152L);

		public static string Equipment_CannotEquipPromptStart => GS(306560360392941568L);

		public static string LowMemoryHeader => GS(310933611869102080L);

		public static string LowMemoryToggle => GS(310933790127022080L);

		public static string LowMemoryDescription => GS(310934031433719808L);

		public static string LowMemoryTextureScale => GS(310934948295991296L);

		public static string OptionsRestartRequired => GS(310950382307106816L);

		public static string WebGLMobileWarning => GS(310968355528892416L);

		public static string Slot_Hologram => GS(311768631944929280L);

		public static string Equipment_ButtonModify => GS(313883178461814784L);

		public static string Equipment_Hair => GS(313891150017585152L);

		public static string SpecialItemVariableEnabled => GS(313892573551779840L);

		public static string SpecialItemVariableDisabled => GS(313892612399423488L);

		public static string EquipmentCommandDescription => GS(314987481612148736L);

		public static string EquipmentCommand_ItemMissing => GS(314989713090928640L);

		public static string Selected => GS(316258147070107648L);

		public static string NotSelected => GS(316258203424776192L);

		public static string SelectAction => GS(316259970090786816L);

		public static string UnselectAction => GS(316259971604930560L);

		public static string UnselectAllAction => GS(316601446230384640L);

		public static string SelectAllAction => GS(316607361348263936L);

		public static string SelectItemsForANewOrderPrompt => GS(316612122302734336L);

		public static string IngredientSelectionTitle => GS(316612373772230656L);

		public static string PriceVariesByQuality => GS(316719705944481792L);

		public static string TargetSlot => GS(316720103287676928L);

		public static string EquiupmentCommandDescription => GS(319255852818096128L);

		public static string RentDue => GS(319260558508417024L);

		public static string CockTwitch_AccountInactive => GS(319328371549921280L);

		public static string CockTwitch_CantStartStreamAccountInactive => GS(319328406836600832L);

		public static string Blog_AccountDeleted => GS(319700772192247808L);

		public static string Blog_CommentsHeader => GS(319703659744059392L);

		public static string Blog_WrittenBy => GS(319705146469638144L);

		public static string Blog_Topic => GS(319705191344496640L);

		public static string Blog_Progress => GS(319900903973838848L);

		public static string ClickHere => GS(319911777543380992L);

		public static string Blog_Username => GS(319926690701631488L);

		public static string Blog_Password => GS(319926789292941312L);

		public static string Blog_ClickToLogin => GS(319935753967624192L);

		public static string Blog_NotLoggedIn => GS(322492181681004544L);

		public static string Blog_LoggedOutForYourSafety => GS(322492225586978816L);

		public static string Blog_AccountBanned => GS(322534704394788864L);

		public static string Blog_ChiliRecipe1 => GS(322557654602866688L);

		public static string Blog_ChiliRecipe2 => GS(322557686781566976L);

		public static string Blog_ChiliRecipe3 => GS(322562073406988288L);

		public static string Blog_RecipesFromMembers => GS(322564471848427520L);

		public static string Blog_Logout => GS(322873137319370752L);

		public static string Blog_BlogLinkEmailInsertHyperlinkText => GS(322906135402110976L);

		public static string StatusApp_BasicModules => GS(323965704895193088L);

		public static string StatusApp_ApproximateCondition => GS(323966017257594880L);

		public static string StatusApp_OtherModules => GS(323966295016988672L);

		public static string StatusApp_NoOtherModulesFound => GS(323967460999946240L);

		public static string StatusApp_CompatiblityCheck => GS(323967509062475776L);

		public static string StatusApp_NoCompatiblityIssuesFound => GS(323967725501145088L);

		public static string StatusApp_FullConnetionEstablished => GS(323967880124162048L);

		public static string StatusApp_BeginningHardwareCheck => GS(323968164145651712L);

		public static string StatusApp_ConnectedTo => GS(323968861058621440L);

		public static string StatusApp_Ram => GS(323972012495069184L);

		public static string StatusApp_CPU => GS(323972033739218944L);

		public static string StatusApp_BIOS => GS(323972070204497920L);

		public static string StatusApp_BiosModified => GS(323972141620912128L);

		public static string StatusApp_Memory => GS(323972443522719744L);

		public static string StatusApp_System => GS(323977953940594688L);

		public static string StatusApp_PcIconName => GS(323990393575170048L);

		public static string Equipment_HairSection => GS(324422569844269056L);

		public static string HairSettings_Header => GS(324666248097947648L);

		public static string HairSettings_Help => GS(324668049937391616L);

		public static string HairSetting_HelpDescription1 => GS(324679602858352640L);

		public static string Slot_HairAdditional => GS(324680820343169024L);

		public static string Equipment_ColorSection => GS(324704662713262080L);

		public static string CantChangeColor => GS(324724537720561664L);

		public static string Folder_Hypercamo => GS(324742261779718144L);

		public static string Folder_NotHypercamo => GS(324742303357853696L);

		public static string ColorsSection_PickFromPresets => GS(324742806699499520L);

		public static string Slot_HairAugmentation => GS(325046368583278592L);

		public static string Hair_Ahoge => GS(326496452730675200L);

		public static string Hair_Front => GS(326496470711656448L);

		public static string Hair_Side => GS(326496727755382784L);

		public static string Hair_Back => GS(326496729714122752L);

		public static string Hair_Braid => GS(326497075953917952L);

		public static string Sort_ByDifficulty => GS(326949855952887808L);

		public static string CookingSongList_Header => GS(326951340845555712L);

		public static string CookingMinigame_WrongKey => GS(326955687113383936L);

		public static string Sort_AlphabeticallyBySongName => GS(327230647882997760L);

		public static string CookingMinigame_FullCombo => GS(327240905971982336L);

		public static string DishQuality_Header => GS(327244749757997056L);

		public static string ItemDescription_NutritionalValues => GS(327252742549528576L);

		public static string FoodSatiation_Satiation => GS(327253747311828992L);

		public static string FoodEffect_MentalHealthDecrease => GS(327259150795923456L);

		public static string FoodEffect_WarningPrompt => GS(327259479696465920L);

		public static string CookingSong_PlayAuto => GS(327275935859466240L);

		public static string CookingSong_PlayAutoPrompt => GS(327276190738931712L);

		public static string CookingSong_IngredientOptional => GS(327631757932224512L);

		public static string CookingSong_LockedDIsh => GS(327634950372188160L);

		public static string CookingSong_NewDish => GS(327636876157509632L);

		public static string CookingSong_BestGrade => GS(327637086371831808L);

		public static string Folder_CookingIngredients => GS(327937932930310144L);

		public static string FoodEffect_MentalHealthIncreaseSmall => GS(327958095562006528L);

		public static string FoodEffect_MentalHealthIncreaseBig => GS(327958423577550848L);

		public static string FoodEffect_MentalHealthIncreaseModerate => GS(327958669204381696L);

		public static string FoodEffect_YouNeedToCookThis => GS(327961407338307584L);

		public static string Cooking_SpentTime => GS(327979375921192960L);

		public static string Ingredient_AnyPasta => GS(327983672008368128L);

		public static string Ingredient_AnyBread => GS(327983694011686912L);

		public static string Ingredient_AnyFlavourVegetable => GS(327983590005530624L);

		public static string Ingredient_AnyMainVegetable => GS(327983527388766208L);

		public static string Ingredient_AnyMeat => GS(327983479418511360L);

		public static string Ingredient_AnyBean => GS(327983854280237056L);

		public static string Ingredient_AnyBeverage => GS(327984090495049728L);

		public static string Ingredient_AnyCookedDish => GS(327984168140005376L);

		public static string Ingredient_AnyFish => GS(327984263929520128L);

		public static string Ingredient_AnyOther => GS(327984380392759296L);

		public static string GenericIngredientHelpStart => GS(327984874716651520L);

		public static string FishingRod_Acceleration => GS(328689309109444608L);

		public static string FishingRod_ReelingSpeed => GS(328689312964009984L);

		public static string FishingRod_Size => GS(328689289895337984L);

		public static string ComparsionToEquippedItem_NoChange => GS(328691727478022144L);

		public static string FishingRod_MaxEffectiveFishWeight => GS(328696489023889408L);

		public static string WeightKgShort => GS(328696865701748736L);

		public static string ItemModifier_ReinforcedLine => GS(328697289624248320L);

		public static string ItemModifier_ReinforcedLineDescription => GS(328697339892981760L);

		public static string ItemModifier_Precise => GS(328698041478406144L);

		public static string ItemModifier_PreciseDescription => GS(328698048633888768L);

		public static string ItemModifier_Lightweight => GS(328699166793392128L);

		public static string ItemModifier_LightweightDescription => GS(328699599867863040L);

		public static string Item_FishInfoHeader => GS(329015496075632640L);

		public static string Item_FishPolluted => GS(329025178949378048L);

		public static string Item_FishPollutedDescription => GS(329026021870264320L);

		public static string Equipment_Fish => GS(329055552375087104L);

		public static string FishFirstTimeCaught => GS(329745697759813632L);

		public static string TooTiredToFishInfo => GS(329744041978617856L);

		public static string FishNewMaxSizeRecord => GS(329745659939774464L);

		public static string FishNewMinSizeRecord => GS(329773428245520384L);

		public static string CatchListHeader => GS(329775437442965504L);

		public static string FishBiteMissed => GS(329790253570187264L);

		public static string FishingDone => GS(329790563772522496L);

		public static string HeavyFishWarning => GS(329792392925274112L);

		public static string FishNothingCaught => GS(329802480138313728L);

		public static string Item_Trash => GS(329826286345117696L);

		public static string Fisher_SellFish => GS(329851276452945920L);

		public static string Fisher_SellAllFish => GS(329866980849868800L);

		public static string Fisher_SellFishTitle => GS(329938634368081920L);

		public static string Fishing_TotalFishesCaught => GS(329942960369033216L);

		public static string Fishing_DifferentFishesCaught => GS(329943435785003008L);

		public static string Fishing_BigFishesCaught => GS(329944719321718784L);

		public static string FileCommandDescription => GS(330575905115070464L);

		public static string FileCommandFileNotFound => GS(330576798375993344L);

		public static string FileCommandHelp => GS(330576860195840000L);

		public static string FileCommandHelp2 => GS(330578028066234368L);

		public static string CommandExampleUsage => GS(330578181087027200L);

		public static string FileCommandAccessDenied => GS(330579698426830848L);

		public static string FileCommandMustInputFilename => GS(330581303033323520L);

		public static string FileCommandEmptyFile => GS(330581742961287168L);

		public static string InteractCommandDescription => GS(330582428763545600L);

		public static string InteractDirectConverasationOver => GS(330597129824231424L);

		public static string Fishing_WaitForBitesInfo => GS(332060315479957504L);

		public static string Fishing_MainMinigameInfo => GS(332060801968889856L);

		public static string CookingMInigame_Help1 => GS(332101605533933568L);

		public static string NewSaveRecomendedPrompt0_90 => GS(332408564678123520L);

		public static string FishingMinigame_Help1 => GS(335500190728314880L);

		public static string FishingMinigame_Help2 => GS(335500222462418944L);

		public static string FishingMinigame_Help3 => GS(335501113718464512L);

		public static string FishingMinigame_Help4 => GS(335501636798504960L);

		public static string FishingMinigame_Help5 => GS(335502144930045952L);

		public static string FishingMinigame_InfoAboveFishingGauge => GS(335510175046688768L);

		public static string FishingMinigame_InfoBelowFishingGauge => GS(335510226649210880L);

		public static string FishingMinigame_InfoAboveFishingProgress => GS(335510248031772672L);

		public static string FishingMinigame_InfoBelowFishingProgress => GS(335510271205302272L);

		public static string stocksdotic_OwnershipLimitReached => GS(341649999747080192L);

		public static string Equipment_SetEquippingCannotCopyVariable => GS(341677855076245504L);

		public static string Live2DTool_Soap => GS(443545661811449856L);

		public static string Live2DTool_Sponge => GS(443545739800338432L);

		public static string Frick_ThighJob => GS(445755131346018304L);

		public static string Room_Shower => GS(445768145315028992L);

		public static string Room_Interact => GS(448969526033244160L);

		public static string Shower_Cleanliness => GS(450764703022440448L);

		public static string Shower_CleanedInfo => GS(450802848761896960L);

		public static string Shower_SoapLeft => GS(450802917011611648L);

		public static string Shower_SoapLeftNoneInfo => GS(450802950742204416L);

		public static string Shower_Soap => GS(454039327529230336L);

		public static string Shower_Sponge => GS(454039364061618176L);

		public static string Shower_WashOff => GS(454039409787920384L);

		public static string Shower_ChooseSoap => GS(454040063709274112L);

		public static string Shower_EnoughSoap => GS(454040364956770304L);

		public static string Shower_NoSoap => GS(454040604627689472L);

		public static string UI_ShoweredToday => GS(458372233793888256L);

		public static string UI_ShoweredYesterday => GS(458372287472590848L);

		public static string Shower_Sex => GS(471121236486774784L);

		public static string bangdotic_worldnews => GS(472119528704172032L);

		public static string bangdotic_USAHeader => GS(472124086796935168L);

		public static string bangdotic_UKHeader => GS(472124268846505984L);

		public static string bangdotic_OpinionsHeader => GS(472125338964451328L);

		public static string bangdotic_SportHeader => GS(472125548503490560L);

		public static string bangdotic_ProjectsHeader => GS(472126417856880640L);

		public static string ItemDescription_DishIngredients => GS(296079490024464384L);

		public static string ExpectedIngredient => GS(297186126352867328L);

		public static string bangdotic_OpinionsTitle => GS(472134547655352320L);

		public static string Shower_ExitPrompt => GS(472543019022671872L);

		public static string Cleanliness_Clean => GS(474648345637281792L);

		public static string Cleanliness_SomeCum => GS(474648503838040064L);

		public static string Cleanliness_ALotOfCum => GS(474648570812686336L);

		public static string Interact_Clean => GS(474693331179266048L);

		public static string Interact_CleanWithARag => GS(474696176054034432L);

		public static string Shower_ExitPromptBeforeAnything => GS(476105129992380416L);

		public static string Shower_ExitPromptNotFinished => GS(476105271864713216L);

		public static string Equipment_DontOwnColor => GS(476191777304338432L);

		public static string Equipment_DontOwn => GS(476191862738116608L);

		public static string Equipment_DontOwnPackage => GS(476192284051759104L);

		public static string Equipment_DontOwnSlot => GS(476193361698480128L);

		public static string Equipment_ViewRelated => GS(476193497661038592L);

		public static string Equipment_ExactItemMatch => GS(476193841459748864L);

		public static string Equipment_RelatedItems => GS(476193969608318976L);

		public static string Equipment_RelatedOwnedItems => GS(476194087434706944L);

		public static string Options_SkipUnseedTextToggle => GS(476546070280658944L);

		public static string UI_SkipEnabledNewTextPrompt => GS(476552496302514176L);

		public static string Options_CumFlashBangToggle => GS(478056522227638272L);

		public static string Options_CumFlashbangTooltip => GS(478056759537164288L);

		public static string Options_SkipUnseenTextTooltip => GS(478060044352479232L);

		public static string Room_LeaveTheCity => GS(483040306832334848L);

		public static string Mods_SafetyWarning1 => GS(483407230179205120L);

		public static string Mods_SafetyWarning2 => GS(483407824948289536L);

		public static string Mods_SafetyWarning3 => GS(483407827385180160L);

		public static string Mods_SafetyWarning4 => GS(483407829138399232L);

		public static string Achievements_MainEndingsHeader => GS(484477599141060608L);

		public static string Achievements_OtherHeader => GS(484477662353416192L);

		public static string Achievements_ChurchEndingsHeader => GS(484478029224992768L);

		public static string Achievements_EndingSeen => GS(484478221374447616L);

		public static string Achievements_EndingUnseen => GS(484478249354649600L);

		public static string Achievements_ShowExtraDetailsToggle => GS(484479769101983744L);

		public static string Achievements_GoodEndingsHeader => GS(484483204786413568L);

		public static string Achievements_BadEndingsHeader => GS(484483236382105600L);

		public static string Credits_GoodEndingAchieved => GS(484532030339538944L);

		public static string UI_NewSkipEnabledNewTextPrompt => GS(485487702006816768L);

		public static string UI_SkipButtonTooltip => GS(485888026693591040L);

		public static string UI_NarrationLogTooltip => GS(485889586559442944L);

		public static string CookingSong_BestDishQuality => GS(486295437891854336L);

		public static string UI_EarlyAccessThanks => GS(487788642143559680L);

		public static string UI_EarlyAccessWarning => GS(487788813547986944L);

		public static string UI_EarlyAccessPirateWarning => GS(487795609801056256L);

		public static string UI_EarlyAccessOfficialSources => GS(487796447680389120L);

		public static string UI_PullingOutActionContinuuing => GS(487809587918921728L);

		public static string Slot_BodyModules => GS(487880583661215744L);

		public static string Slot_FeetBodyModules => GS(488162793228328960L);

		public static string Slot_CalfBodyModules => GS(488162780284706816L);

		public static string Slot_ThighBodyModules => GS(488162753244028928L);

		public static string BodyMod_ThighSize => GS(488166734942953472L);

		public static string BodyMod_CalfSize => GS(488166887586258944L);

		public static string BodyMod_FootWidth => GS(488166928447168512L);

		public static string BodyMod_FootLength => GS(488166975201075200L);

		public static string Equipment_RandomOutfit => GS(488975334281584640L);

		public static string Equipment_RandomOutfitPrompt => GS(488975408898252800L);

		public static string SAVE_LoadErrorInvalidSavePrompt => GS(489264642502696960L);

		public static string Options_GrayOutSeenTextToggle => GS(489268972827975680L);

		public static string Options_GreyOutSeenTextTooltip => GS(489272145760231424L);

		public static string Mods_SpecialButton => GS(489336303788761088L);

		public static string Eyepatch_Left => GS(489558131031416832L);

		public static string Eyepatch_Right => GS(489558461962002432L);

		public static string Slot_TongueAccessory => GS(489588868422049792L);

		public static string ChooseLanguage => GS(489645086704410624L);

		public static string ChooseModTranslationGenerationLanguage => GS(489645144103460864L);

		public static string FailedGeneratingTranslationFile => GS(489647547020222464L);

		public static string Mods_ModVersion => GS(489661828419821568L);

		public static string SelectModTranslationFilesPrompt => GS(489663437761683456L);

		public static string ModTranslationFiles => GS(489663838821031936L);

		public static string Mods_ViewLogs => GS(489670808600846336L);

		public static string Mods_ModTranslatedToLanguage => GS(489686432504881152L);

		public static string Mods_ModNotTranslatedToLangauge => GS(489686547558834176L);

		public static string Mods_GenerateModTranslationFile => GS(489987962034462720L);

		public static string Mods_ImportModTranslationFiles => GS(489988037330608128L);

		public static string Mods_NoTranslationData => GS(489990660490272768L);

		public static string Mods_ImportedTranslations => GS(489990733857038336L);

		public static string Mods_ModTranslationNotes => GS(489998867560407040L);

		public static string Mods_DeleteInstalledTranslationModFiles => GS(490004860151275520L);

		public static string Mods_DeleteTranslationModFIlesPrompt => GS(490004949460590592L);

		public static string Mods_DeleteTranslationModFIlesPickerPrompt => GS(490944453897887744L);

		public static string Mods_ExportInstalledTranslationModFiles => GS(490944849613692928L);

		public static string Mods_ExportTranslationModFIlesPickerPrompt => GS(490945246227079168L);

		public static string Mods_LocalizingMods => GS(491009961577947136L);

		public static string Mods_NoTranslationIssuesFound => GS(491025278207270912L);

		public static string Mods_TranslationIssuesExtraKeys => GS(491025385531121664L);

		public static string Mods_TranslationIssuesMissingKeys => GS(491025453571121152L);

		public static string Mods_TranslationIssuesBrokenKeys => GS(491025488312541184L);

		public static string Mods_TranslationIssuesLogInfo => GS(491032741463728128L);

		public static string Mods_TranslationIssuesBroken => GS(491033140786634752L);

		public static string Mods_TargetModVersion => GS(491037523914006528L);

		public static string HoverToView => GS(491037976072560640L);

		public static string FullyTranslated => GS(491040484891303936L);

		public static string Mods_TranslationIssues => GS(491043588659486720L);

		public static string Mods_ModsOriginalLangauge => GS(491061162591920128L);

		public static string Folder_Sliders => GS(491337545033457664L);

		public static string Folder_Toggles => GS(491337567032582144L);

		public static string Slot_Manicure => GS(491372144350470144L);

		public static string Slot_Pedicure => GS(491421408766205952L);

		public static string InputEmail => GS(493902357215059968L);

		public static string InputEmailAreYouSure => GS(493907265213440000L);

		public static string InvalidEmail => GS(493909894538731520L);

		public static string FailedToVerify => GS(493910851381108736L);

		public static string VerifyOnDiscord => GS(493922460669321216L);

		public static string VerifyWithoutDiscord => GS(493922652495814656L);

		public static string IHaveTheCode => GS(493922962240970752L);

		public static string LoginInfo_AccessInfo => GS(493925030305472512L);

		public static string Login_Email => GS(493932568472199168L);

		public static string LinksToStuff => GS(493944224614981632L);

		public static string JoinDiscordServer => GS(493946882415403008L);

		public static string LoginInfo_AccessInfoOnlyOnce => GS(493949397156503552L);

		public static string LoginInfo_PrivateMessage => GS(493982020503347200L);

		public static string LoginInfo_DiscordCommand2 => GS(493982440189599744L);

		public static string LoginInfo_Alternative2 => GS(493983195621498880L);

		public static string LoginInfo_WhatToDoWithCode => GS(493985080814346240L);

		public static string LoginInfo_IHaveTheCodePrompt => GS(493986248156258304L);

		public static string LoginInfo_SuccessfulVerification => GS(493987150627872768L);

		public static string BodyMod_BoobSize => GS(496402615639744512L);

		public static string Slot_BoobBodyModules => GS(496403323130748928L);

		public static string Equipment_Legs => GS(496802670846681088L);

		public static string Equipment_Arms => GS(496802702727585792L);

		public static string Equipment_Socks => GS(496803314248720384L);

		public static string Equipment_Shoes => GS(496804553137070080L);

		public static string Equipment_LowerBody => GS(496810800242499584L);

		public static string Equipment_UpperBody => GS(496810849705926656L);

		public static string Equipment_Outer => GS(496810876587220992L);

		public static string Equipment_BodySizeModules => GS(496814697568804864L);

		public static string Room_Clothes => GS(497811185744089088L);

		public static string Equipment_MakeUp => GS(497824627733434368L);

		public static string Equipment_Neck => GS(497826289369878528L);

		public static string Equipment_Piercing => GS(497826636654055424L);

		public static string BodyMod_AssSize => GS(497858709615939584L);

		public static string Slot_AssBodyModules => GS(498300297139851264L);

		public static string SAVE_LoadFromSlotFileChooseSlotPrompt => GS(498332293912203264L);

		public static string SAVE_InvalidSlotNumberAlreadyTaken => GS(498336309140393984L);

		public static string SAVE_SlotLoadingWarning => GS(498342642031923200L);

		public static string SAVE_SlotFileExtension => GS(498343662011158528L);

		public static string Room_DoSomethingTogether => GS(498365790617997312L);

		public static string Interact_Sex => GS(498368129751977984L);

		public static string Slot_Navel => GS(499141756822884352L);

		public static string Slot_NippleLeft => GS(499141866881421312L);

		public static string Slot_NippleRight => GS(499141868802412544L);

		public static string ModifableVariables_Header => GS(499378805064732672L);

		public static string ModifiableVariables_CanBeChanged => GS(499378856839221248L);

		public static string PasteFromClipboard => GS(499406199741878272L);

		public static string PasteFromClipboard_SuccessfullyPasted => GS(499407869011955712L);

		public static string PasteFromClipboard_ClipboardEmpty => GS(499407920878718976L);

		public static string Options_ClearVerification => GS(499410529249267712L);

		public static string Interactable_Spank => GS(537744955715608576L);

		public static string Interactable_SpreadAnus => GS(537769387989393408L);

		public static string Interactable_GrabWaist => GS(538486512190218240L);

		public static string Interactable_FingeringAnus => GS(538508930501828608L);

		public static string Frick_ChangeCamera => GS(540001448330518528L);

		public static string Frick_ShowCock => GS(540990535136960512L);

		public static string Hair_FrontClippedUp => GS(542208471210123264L);

		public static string FishingDuelWon => GS(543309117950779392L);

		public static string FishingDuelLost => GS(543309152826417152L);

		public static string FishingDuelWinnerYou => GS(543502681200648192L);

		public static string FishingDuelWinnerEnemy => GS(543524635571904512L);

		public static string FishingFishJointStatBoost => GS(543559430871965696L);

		public static string LoginInfo_MakeSureToEnableDms => GS(545019226687004672L);

		public static string FishingDuelScore => GS(546458543988924416L);

		public static string Frick_CantDoThatWhenCumming => GS(546782993317879808L);

		public static string CheatsTitle => GS(550048406222921728L);

		public static string Options_DisableAdvancedPrompt => GS(550049084538347520L);

		public static string Cheat_Money => GS(550063086752489472L);

		public static string Cheat_Items => GS(550063118595645440L);

		public static string Cheat_ItemColors => GS(550063161595650048L);

		public static string Cheat_Quality => GS(550063278054694912L);

		public static string Cheat_CumCheats => GS(550063365107474432L);

		public static string Cheats_AccessLinks => GS(550065358517559296L);

		public static string Cheats_GetCheatCode => GS(550068833926242304L);

		public static string Cheats_HaveCheatCode => GS(550069333585289216L);

		public static string Cheat_DiscordCommand2 => GS(550095134015873024L);

		public static string CopiedText => GS(550095455563800576L);

		public static string Cheat_AreYouSubscribed => GS(550095603262021632L);

		public static string Cheat_SadlyOnlyForPatrons => GS(550095920481427456L);

		public static string IDontWantToUseDiscord => GS(550098058615971840L);

		public static string NotUsingDiscordNotRecommended => GS(550098103817986048L);

		public static string Cheat_SendToDevelopers => GS(550098656807608320L);

		public static string Cheat_DisableCheats => GS(550101143010664448L);

		public static string Cheat_AreYouSureDisable => GS(550101607492083712L);

		public static string Cheat_InvalidCode => GS(550103423273394176L);

		public static string Cheat_SuccessBasicCheats => GS(550103618782486528L);

		public static string Cheat_SuccessAdvancedCheats => GS(550103664789807104L);

		public static string Cheat_DisableCheatsHelp => GS(550104163198951424L);

		public static string Cheat_Active => GS(550106583442382848L);

		public static string Cheat_Status => GS(550109001706102784L);

		public static string Cheat_BasicCheats => GS(550109357429219328L);

		public static string Cheat_DisabledCheats => GS(550109393877721088L);

		public static string Cheat_AdvancedCheats => GS(550109429743214592L);

		public static string UI_CheatsMainMenu => GS(550110490625302528L);

		public static string UI_OtherMainMenu => GS(550111901958266880L);

		public static string CanChangeColorThanksToCheats => GS(550134053780578304L);

		public static string Cheat_AndMore => GS(550138070548078592L);

		public static string Cheat_RandomiseModifiers => GS(550138237066141696L);

		public static string Cheat_RemoveModifiers => GS(550140764364988416L);

		public static string Cheat_RestoreCum => GS(550148455409901568L);

		public static string Cheat_IncreaseMaxCum => GS(550148500142153728L);

		public static string Cheat_CumCheat => GS(550148553929908224L);

		public static string Cheat_CumRestorationFull => GS(550150305165074432L);

		public static string Cheat_CumRestorationOverflow => GS(550150361016426496L);

		public static string Cheat_CumRestorationAlreadyFull => GS(550150909765607424L);

		public static string Cheat_ResetCumCapacity => GS(550151074840829952L);

		public static string Cheat_ResetCumCapacitySuccess => GS(550151292432932864L);

		public static string Cheat_IncreasedCumCapacityWarning => GS(550152587306852352L);

		public static string Cheat_MaxMoneyReached => GS(550157731629096960L);

		public static string Tooltip_SaveGame => GS(550158855799037952L);

		public static string Tooltip_ReturnToMainMenu => GS(550158882395119616L);

		public static string Tooltip_Cheats => GS(550158924879224832L);

		public static string iamacheater_ButtonNew => GS(550162874974068736L);

		public static string iamacheater_SadNews => GS(550162995329622016L);

		public static string Cheats_Other => GS(550165115571265536L);

		public static string Cheat_ResetChurchPromptWarning => GS(550170296421703680L);

		public static string WebGL_InputFieldInfo => GS(551219030114951168L);

		public static string Cheat_AllOfTheBasic => GS(551227257611345920L);

		public static string SubscribeNowToAccess => GS(552264424500486144L);

		public static string Benefit_EarlyAccess => GS(552264468259659776L);

		public static string Benefit_Devlogs => GS(552264495984009216L);

		public static string Benefit_ConceptArt => GS(552264522567507968L);

		public static string Benefit_AndMore => GS(552264558722408448L);

		public static string DiscordNotExpired => GS(552278588459573248L);

		public static string Cheat_ChangeFishWeight => GS(553314502581411840L);

		public static string Cheat_PlayCookiningCheat => GS(572861515845066752L);

		public static string Cheat_CookInstantlyPrompt => GS(572864247184220160L);

		public static string Cheat_PlayCookiningCheatSaveScore => GS(572864683995815936L);

		public static string Cheat_PlayCookiningCheatDiscardScore => GS(572864686193631232L);

		public static string GS(long key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(long key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, null, FallbackBehavior.UseProjectSettings).Result;
		}

		public static string GS(string key, params object[] o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string GS(string key, IList<object> o)
		{
			return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_REFERENCE, key, o).Result;
		}

		public static string PC_Shortcut(object arg0)
		{
			return GS(66758454939648L, arg0);
		}

		public static string PC_UnreadEmails(int arg0)
		{
			return GS(71063857664000L, arg0);
		}

		public static string Achievements_2(object arg0)
		{
			return GS(1750558276718592L, arg0);
		}

		public static string Achievements_1(object arg0, object arg1)
		{
			return GS(1750513515106304L, arg0, arg1);
		}

		public static string Achievements_3(object arg0, object arg1)
		{
			return GS(1750573665619968L, arg0, arg1);
		}

		public static string Achievements_4(object arg0)
		{
			return GS(1752188019675136L, arg0);
		}

		public static string Achievements_5(object arg0, object arg1)
		{
			return GS(1752408820420608L, arg0, arg1);
		}

		public static string SAVE_LoadSuccessPrompt(object arg0)
		{
			return GS(1763972839510016L, arg0);
		}

		public static string SAVE_DeleteSavePrompt(object arg0, object arg1)
		{
			return GS(1765677111054336L, arg0, arg1);
		}

		public static string SAVE_Slot(object arg0)
		{
			return GS(1766022692343808L, arg0);
		}

		public static string SAVE_SlotEmpty(object arg0)
		{
			return GS(1766333414772736L, arg0);
		}

		public static string Mods_VersionMismatch(object arg0, object arg1)
		{
			return GS(1785619994775552L, arg0, arg1);
		}

		public static string Mods_DeleteModPrompt(object arg0)
		{
			return GS(1788021279952896L, arg0);
		}

		public static string Mods_LoadingSuccess(object arg0)
		{
			return GS(1788364734730240L, arg0);
		}

		public static string Mods_LoadingError(object arg0, object arg1)
		{
			return GS(1788709607182336L, arg0, arg1);
		}

		public static string UI_Error_PathTooLong(object arg0, object arg1, object arg2)
		{
			return GS(1800544611864576L, arg0, arg1, arg2);
		}

		public static string UI_StaminaBarTooltipDynamic(int arg0)
		{
			return GS(2095981008916480L, arg0);
		}

		public static string Cum_Dynamic(int arg0)
		{
			return GS(2099884156915712L, arg0);
		}

		public static string UI_Day(object arg0)
		{
			return GS(2100490221260800L, arg0);
		}

		public static string Frick_SlowmodeUnlockedPrompt(object arg0)
		{
			return GS(2115271707725824L, arg0);
		}

		public static string Frick_LastStreamSummaryPrompt(int arg0, object arg1, object arg2, object arg3, object arg4, object arg5)
		{
			return GS(2117264815820800L, arg0, arg1, arg2, arg3, arg4, arg5);
		}

		public static string Frick_StreamSummary(object arg0, object arg1, object arg2)
		{
			return GS(2144474717097984L, arg0, arg1, arg2);
		}

		public static string Equipment_EquipSetPrompt(object arg0, object arg1)
		{
			return GS(2443693403279360L, arg0, arg1);
		}

		public static string Equipment_SetSavedSuccess(object arg0)
		{
			return GS(2444932593618944L, arg0);
		}

		public static string Equipment_SetSavedFailure(object arg0, object arg1)
		{
			return GS(2445047660154880L, arg0, arg1);
		}

		public static string Equipment_SetDeletePrompt(object arg0, object arg1)
		{
			return GS(2445385301626880L, arg0, arg1);
		}

		public static string Equipment_SetLoadedSuccess(object arg0)
		{
			return GS(2445837711839232L, arg0);
		}

		public static string Equipment_ThrowAwayPrompt(object arg0)
		{
			return GS(2448896844238848L, arg0);
		}

		public static string Equipment_UnpackedItemsPrompt(int arg0)
		{
			return GS(2449415880970240L, arg0);
		}

		public static string Equipment_SetDefaultName(object arg0)
		{
			return GS(2466019020771328L, arg0);
		}

		public static string Equipment_SetSummaryShort(int arg0, object arg1)
		{
			return GS(2466914664058880L, arg0, arg1);
		}

		public static string Equipment_SetEquippingIteration(object arg0)
		{
			return GS(2467180310302720L, arg0);
		}

		public static string Equipment_SetEquippingAs(object arg0, object arg1)
		{
			return GS(2468124016119808L, arg0, arg1);
		}

		public static string Equipment_SetEquippingChangingColor(object arg0)
		{
			return GS(2468356758048768L, arg0);
		}

		public static string Equipment_SetEquippingCannotEquip(object arg0)
		{
			return GS(2468601126588416L, arg0);
		}

		public static string Equipment_SetEquippingMissing(object arg0)
		{
			return GS(2468881482256384L, arg0);
		}

		public static string UI_Price(object arg0)
		{
			return GS(2483338505752576L, arg0);
		}

		public static string UI_Condition(object arg0)
		{
			return GS(2483655213453312L, arg0);
		}

		public static string Shop_BuyPrompt(object arg0)
		{
			return GS(2836835176681472L, arg0);
		}

		public static string UI_ExportFilePrompt(object arg0)
		{
			return GS(2838282203172864L, arg0);
		}

		public static string Shop_BuyCount(object arg0)
		{
			return GS(2872154328535040L, arg0);
		}

		public static string Shop_Money(object arg0)
		{
			return GS(2872751165411328L, arg0);
		}

		public static string UI_Hour(object arg0, object arg1)
		{
			return GS(3190461971697664L, arg0, arg1);
		}

		public static string Minigame_Shift(object arg0)
		{
			return GS(3198362492735488L, arg0);
		}

		public static string Minigame_TotalScore(object arg0)
		{
			return GS(3201999449231360L, arg0);
		}

		public static string Minigame_TotalScoreNoMiss(object arg0, object arg1, object arg2)
		{
			return GS(3202548034834432L, arg0, arg1, arg2);
		}

		public static string Loading_Module(object arg0)
		{
			return GS(3204295029559296L, arg0);
		}

		public static string Save_Description(object arg0)
		{
			return GS(3209855691001856L, arg0);
		}

		public static string Shop_OrderFinishPrompt(object arg0, object arg1)
		{
			return GS(4262224306507776L, arg0, arg1);
		}

		public static string UI_PluralDays(int arg0)
		{
			return GS(4263186110746624L, arg0);
		}

		public static string UI_PluralHours(int arg0)
		{
			return GS(4263253978779648L, arg0);
		}

		public static string UI_PluralMinutes(int arg0)
		{
			return GS(4263318080327680L, arg0);
		}

		public static string Shop_Delivery(object arg0)
		{
			return GS(4264050888151040L, arg0);
		}

		public static string stonksdotic_CurrentPrice(object arg0)
		{
			return GS(4291006685908992L, arg0);
		}

		public static string stonksdotic_StockSummary(int arg0, object arg1, object arg2)
		{
			return GS(4291269228367872L, arg0, arg1, arg2);
		}

		public static string stonksdotic_IncomeInfo(object arg0, object arg1, object arg2, object arg3)
		{
			return GS(4292699553140736L, arg0, arg1, arg2, arg3);
		}

		public static string stocksdotic_ShortInfo(int arg0, object arg1, object arg2)
		{
			return GS(4293610207842304L, arg0, arg1, arg2);
		}

		public static string stocksdotic_SellButton(int arg0, object arg1)
		{
			return GS(4296638616965120L, arg0, arg1);
		}

		public static string stocksdotic_SellAllButton(object arg0)
		{
			return GS(4296818456137728L, arg0);
		}

		public static string stocksdotic_BuyButton(int arg0, object arg1)
		{
			return GS(4297034282438656L, arg0, arg1);
		}

		public static string cocktwitchdotic_Summary(object arg0, object arg1, object arg2, object arg3)
		{
			return GS(4310926626787328L, arg0, arg1, arg2, arg3);
		}

		public static string cocktwitchdotic_LeaderboardEntry(object arg0, object arg1, object arg2, int arg3)
		{
			return GS(4311636617596928L, arg0, arg1, arg2, arg3);
		}

		public static string casinodotic_Tokens(object arg0)
		{
			return GS(4627441351790592L, arg0);
		}

		public static string casinodotic_Stake(object arg0)
		{
			return GS(4628268514676736L, arg0);
		}

		public static string casinodotic_Win(object arg0, object arg1, object arg2)
		{
			return GS(4628732987707392L, arg0, arg1, arg2);
		}

		public static string casinodotic_BuyTokensPrompt(object arg0, object arg1)
		{
			return GS(4629025448136704L, arg0, arg1);
		}

		public static string casinodotic_Payout(object arg0, object arg1)
		{
			return GS(4629333205192704L, arg0, arg1);
		}

		public static string casinodotic_MinimalStake(object arg0)
		{
			return GS(4629507973451776L, arg0);
		}

		public static string casinodotic_MinimalPayout(object arg0)
		{
			return GS(4629699871248384L, arg0);
		}

		public static string casinodotic_Won(object arg0)
		{
			return GS(4631102643638272L, arg0);
		}

		public static string casinodotic_GotBack(object arg0)
		{
			return GS(4631196336001024L, arg0);
		}

		public static string casinodotic_DealersHand(object arg0)
		{
			return GS(4632078955974656L, arg0);
		}

		public static string casinodotic_PlayersHand(object arg0)
		{
			return GS(4632171352297472L, arg0);
		}

		public static string casinodotic_BuyTokens(object arg0, object arg1)
		{
			return GS(4634206017556480L, arg0, arg1);
		}

		public static string casinodotic_Off(object arg0)
		{
			return GS(4634288825700352L, arg0);
		}

		public static string adredirectdotic_Text2(object arg0)
		{
			return GS(4982288534081536L, arg0);
		}

		public static string bangdotic_email_Topic(object arg0)
		{
			return GS(6834585920737280L, arg0);
		}

		public static string bangdotic_email_From(object arg0)
		{
			return GS(7201200239677440L, arg0);
		}

		public static string Equipment_Own(object arg0)
		{
			return GS(7580205748981760L, arg0);
		}

		public static string Options_TranslationProgress(object arg0)
		{
			return GS(8322437082910720L, arg0);
		}

		public static string Equipment_CannotEquipWithoutSlot(object arg0)
		{
			return GS(28870155642368000L, arg0);
		}

		public static string Equipment_CannotRemove(object arg0)
		{
			return GS(28871745480716288L, arg0);
		}

		public static string UI_RentIn(object arg0)
		{
			return GS(28884898440572928L, arg0);
		}

		public static string Equipment_Unequipped(object arg0)
		{
			return GS(35049316422651904L, arg0);
		}

		public static string Naming_AreYouSure(object arg0)
		{
			return GS(35060892756217856L, arg0);
		}

		public static string Options_MachineTranslation(object arg0)
		{
			return GS(35136683552673792L, arg0);
		}

		public static string Equipment_Remove(object arg0)
		{
			return GS(35201171735842816L, arg0);
		}

		public static string Frick_StreamDonated(object arg0)
		{
			return GS(41001351890472960L, arg0);
		}

		public static string ladypartsdotic_PurchaseCompletePrompt(object arg0)
		{
			return GS(46297305749262336L, arg0);
		}

		public static string SAVE_AutoSlot(object arg0)
		{
			return GS(76497017957650432L, arg0);
		}

		public static string SAVE_AutoSaveSlotEmpty(object arg0)
		{
			return GS(76497026111377408L, arg0);
		}

		public static string Rewards_Money(object arg0)
		{
			return GS(77960481725091840L, arg0);
		}

		public static string Rewards_Item(object arg0)
		{
			return GS(77960487836192768L, arg0);
		}

		public static string Requirements_Time(object arg0)
		{
			return GS(77969540075360256L, arg0);
		}

		public static string Rewards_RespectGain(object arg0)
		{
			return GS(103325919220731904L, arg0);
		}

		public static string Requirements_TimeLeft(object arg0)
		{
			return GS(78029779415994368L, arg0);
		}

		public static string Requirements_StreamForXMinutesSimple(object arg0)
		{
			return GS(78214396286734336L, arg0);
		}

		public static string Requirements_StreamForXMinutesCompound(object arg0)
		{
			return GS(78214498862632960L, arg0);
		}

		public static string Requirements_ViewersRequired(int arg0)
		{
			return GS(78224465707536384L, arg0);
		}

		public static string Requirements_FollowersRequired(int arg0)
		{
			return GS(78224468010209280L, arg0);
		}

		public static string Requirements_SubsRequired(int arg0)
		{
			return GS(78224469469827072L, arg0);
		}

		public static string Requirements_Viewership(object arg0)
		{
			return GS(78291985713991680L, arg0);
		}

		public static string Requirements_Position(object arg0)
		{
			return GS(78303109276635136L, arg0);
		}

		public static string Requirement_TimeBetween(object arg0, object arg1)
		{
			return GS(79766729042825216L, arg0, arg1);
		}

		public static string Requirement_TimesAtLeast(int arg0)
		{
			return GS(79824472382509056L, arg0);
		}

		public static string Requirements_TimesDone(int arg0)
		{
			return GS(79787745735479296L, arg0);
		}

		public static string Requirement_TimeBeforeAfter(object arg0, object arg1)
		{
			return GS(79817738016395264L, arg0, arg1);
		}

		public static string Requirement_TimesExactly(int arg0)
		{
			return GS(79824376790126592L, arg0);
		}

		public static string Requirement_TimesLess(int arg0)
		{
			return GS(79824873425080320L, arg0);
		}

		public static string Requirement_TimesAtLeastAndLess(int arg0, int arg1)
		{
			return GS(79825643025338368L, arg0, arg1);
		}

		public static string Requirements_InOrderGate(object arg0)
		{
			return GS(79841721059250176L, arg0);
		}

		public static string Delivery_ReportedPrice(object arg0)
		{
			return GS(80489256945848320L, arg0);
		}

		public static string Deliveries_BuyPremiumPrompt(object arg0, object arg1)
		{
			return GS(80500201034223616L, arg0, arg1);
		}

		public static string Deliveries_DeliverySentPrompt(object arg0, object arg1)
		{
			return GS(80504339507843072L, arg0, arg1);
		}

		public static string NarrativeLog_GameOnFor(object arg0)
		{
			return GS(80852450835042304L, arg0);
		}

		public static string Live2DTool_CurrentTool(object arg0)
		{
			return GS(88106106160549888L, arg0);
		}

		public static string Requirement_WhileWearing(object arg0)
		{
			return GS(100693490689511424L, arg0);
		}

		public static string cocktracts_Main_Summary(int arg0, int arg1)
		{
			return GS(102510519830581248L, arg0, arg1);
		}

		public static string Equipment_MissingItemUnknownMod(object arg0, object arg1)
		{
			return GS(103653687980593152L, arg0, arg1);
		}

		public static string Equipment_MissingItemKnownMod(object arg0, object arg1, object arg2)
		{
			return GS(103653268436946944L, arg0, arg1, arg2);
		}

		public static string Equipment_MissingItemVanilla(object arg0)
		{
			return GS(103653695874273280L, arg0);
		}

		public static string Rewards_RespectLoss(object arg0)
		{
			return GS(103326065513861120L, arg0);
		}

		public static string cocktracts_Partner_ContractData(object arg0, object arg1)
		{
			return GS(104682370048593920L, arg0, arg1);
		}

		public static string Rewards_Tokens(object arg0, object arg1)
		{
			return GS(111323388428816384L, arg0, arg1);
		}

		public static string Contract_ThisWillAngerThePartner(object arg0)
		{
			return GS(111514776562962432L, arg0);
		}

		public static string Contract_AcceptedAt(object arg0)
		{
			return GS(111535180773986304L, arg0);
		}

		public static string Contract_FailedAt(object arg0)
		{
			return GS(111536244386566144L, arg0);
		}

		public static string Contract_CompletedAt(object arg0)
		{
			return GS(111536198479908864L, arg0);
		}

		public static string Contract_AbandonedAt(object arg0)
		{
			return GS(111536288699387904L, arg0);
		}

		public static string OpeningFolderNotSupportedOnThisPlatform(object arg0)
		{
			return GS(115600670286811136L, arg0);
		}

		public static string LinkPrompt(object arg0)
		{
			return GS(119007219394940928L, arg0);
		}

		public static string Requirement_WhileWearingMerch(object arg0)
		{
			return GS(119322262330281984L, arg0);
		}

		public static string Contracts_TimeLeftToAccept(object arg0)
		{
			return GS(119616393732558848L, arg0);
		}

		public static string NunRepair_RepairPossible(object arg0)
		{
			return GS(121815230467411968L, arg0);
		}

		public static string NunRepair_RepairPrompt(object arg0, object arg1)
		{
			return GS(121820352937332736L, arg0, arg1);
		}

		public static string Mods_Preparing(object arg0)
		{
			return GS(122446762513141760L, arg0);
		}

		public static string Mods_Starting(object arg0)
		{
			return GS(122446862475988992L, arg0);
		}

		public static string Equipment_SetEquippingIterationLimitReached(object arg0)
		{
			return GS(122896743783075840L, arg0);
		}

		public static string Options_ToyCurrentInsertion(object arg0)
		{
			return GS(142921688344825856L, arg0);
		}

		public static string ReturnChangingRoomItem_Prompt(object arg0)
		{
			return GS(180111010378420224L, arg0);
		}

		public static string ItemsReturnedPrompt(int arg0)
		{
			return GS(180168777147486208L, arg0);
		}

		public static string TryItemPrompt(object arg0)
		{
			return GS(180177057936707584L, arg0);
		}

		public static string UnpackPrompt(object arg0)
		{
			return GS(181276113471672320L, arg0);
		}

		public static string Achievements_EstimatedTimePlayed(object arg0)
		{
			return GS(183672075477454848L, arg0);
		}

		public static string ShillingPrompt(object arg0, object arg1)
		{
			return GS(183678261178273792L, arg0, arg1);
		}

		public static string Equipment_FromMod(object arg0)
		{
			return GS(207959451258314752L, arg0);
		}

		public static string Mods_MajorVersionBehind(int arg0)
		{
			return GS(216003925590925312L, arg0);
		}

		public static string Mods_MinorVersionBehind(int arg0)
		{
			return GS(216004093535051776L, arg0);
		}

		public static string Mods_PatchVersionBehind(int arg0)
		{
			return GS(216004097012129792L, arg0);
		}

		public static string Mods_MajorVersionAhead(int arg0)
		{
			return GS(216004209444642816L, arg0);
		}

		public static string Mods_MinorVersionAhead(int arg0)
		{
			return GS(216004239974981632L, arg0);
		}

		public static string Mods_PatchVersionAhead(int arg0)
		{
			return GS(216004242319597568L, arg0);
		}

		public static string DownloadLatestPublicItchIo(object arg0)
		{
			return GS(217497671214030848L, arg0);
		}

		public static string Fishing_FishingSpot_Price(object arg0)
		{
			return GS(273929969140228096L, arg0);
		}

		public static string Fishing_TimeEndsAt(object arg0)
		{
			return GS(276192940222570496L, arg0);
		}

		public static string Rewards_ItemDelivery(object arg0)
		{
			return GS(276482683070046208L, arg0);
		}

		public static string Fishing_CaughtXTimes(int arg0)
		{
			return GS(285884661038571520L, arg0);
		}

		public static string Fishing_MinWeight(object arg0)
		{
			return GS(285884766244298752L, arg0);
		}

		public static string Fishing_MaxWeight(object arg0)
		{
			return GS(285884767963963392L, arg0);
		}

		public static string Fishing_LastCaught(object arg0)
		{
			return GS(285884960692232192L, arg0);
		}

		public static string Fishing_BiggerThanPercent(object arg0)
		{
			return GS(285900715160756224L, arg0);
		}

		public static string Fishing_SmallerThanPercent(object arg0)
		{
			return GS(285900763114233856L, arg0);
		}

		public static string FishItemWeightItemInfo(object arg0)
		{
			return GS(286219092781686784L, arg0);
		}

		public static string FishingWebsite_Tip1(object arg0, object arg1, object arg2)
		{
			return GS(288779861457100800L, arg0, arg1, arg2);
		}

		public static string MillilitersDisplay(object arg0)
		{
			return GS(291360229049753600L, arg0);
		}

		public static string CommandArgumentsExpectationInfo(object arg0, int arg1, object arg2)
		{
			return GS(291364104926388224L, arg0, arg1, arg2);
		}

		public static string ProvidedArgumentsCountError(int arg0)
		{
			return GS(291364817899347968L, arg0);
		}

		public static string ValueNotFound(object arg0)
		{
			return GS(291368438418366464L, arg0);
		}

		public static string ClosestMatch(object arg0)
		{
			return GS(291368638771879936L, arg0);
		}

		public static string HelpCommandDescription(object arg0)
		{
			return GS(291675064895528960L, arg0);
		}

		public static string ViewVariableCommandDescription(object arg0, object arg1)
		{
			return GS(291675631189483520L, arg0, arg1);
		}

		public static string SummaryCommandName(object arg0)
		{
			return GS(291676700569231360L, arg0);
		}

		public static string SummaryCommandMasterName(object arg0)
		{
			return GS(291676739815333888L, arg0);
		}

		public static string SummaryCommandLust(object arg0)
		{
			return GS(291676809012961280L, arg0);
		}

		public static string SummaryCommandSympathy(object arg0)
		{
			return GS(291676887047987200L, arg0);
		}

		public static string SummaryCommandTimeSinceLastHeadpat(object arg0)
		{
			return GS(291676985823846400L, arg0);
		}

		public static string HelpCommandDetailedInfoNotProvided(object arg0)
		{
			return GS(291677830095298560L, arg0);
		}

		public static string CommandNotFound(object arg0)
		{
			return GS(291678442090389504L, arg0);
		}

		public static string CurrentDateInfo(object arg0)
		{
			return GS(291685365296652288L, arg0);
		}

		public static string CurrentDayOfTheWeek(object arg0)
		{
			return GS(291691209128140800L, arg0);
		}

		public static string CommandNotAWholeNumber(object arg0)
		{
			return GS(291763400070914048L, arg0);
		}

		public static string CommandNotWithinRange(object arg0, object arg1, object arg2)
		{
			return GS(291763485726990336L, arg0, arg1, arg2);
		}

		public static string SnakeCommandScore(object arg0, object arg1)
		{
			return GS(291786780740411392L, arg0, arg1);
		}

		public static string CubeCommandInputGradient(object arg0)
		{
			return GS(292070395239981056L, arg0);
		}

		public static string CommandClosed(object arg0)
		{
			return GS(292098556631703552L, arg0);
		}

		public static string EightBallCommandText(object arg0, object arg1)
		{
			return GS(292119713296670720L, arg0, arg1);
		}

		public static string PlayVideoLoading(object arg0)
		{
			return GS(294219412971446272L, arg0);
		}

		public static string ItemModifierInfoDescription(object arg0)
		{
			return GS(294625865280512000L, arg0);
		}

		public static string ItemModifierCursedDescription(object arg0)
		{
			return GS(294641082420944896L, arg0);
		}

		public static string CookingSong_MoreScore(object arg0)
		{
			return GS(294915737463545856L, arg0);
		}

		public static string CookingSong_LessScore(object arg0)
		{
			return GS(294915738977689600L, arg0);
		}

		public static string CookingSong_FasterSong(object arg0)
		{
			return GS(294915915000045568L, arg0);
		}

		public static string CookingSong_SlowerSong(object arg0)
		{
			return GS(294915916690350080L, arg0);
		}

		public static string CookingSong_DeactiveModifier(object arg0)
		{
			return GS(294924838369222656L, arg0);
		}

		public static string ItemsMissing(int arg0)
		{
			return GS(297140014199857152L, arg0);
		}

		public static string RecipeChoosingItemsNoValidItemsFoundPrompt(object arg0)
		{
			return GS(297189572783104000L, arg0);
		}

		public static string RateOnX(object arg0)
		{
			return GS(299000698021863424L, arg0);
		}

		public static string ColorPickerItemPrompt(object arg0)
		{
			return GS(302651419346853888L, arg0);
		}

		public static string Equipment_SetModGood(object arg0, object arg1)
		{
			return GS(306475541545869312L, arg0, arg1);
		}

		public static string Equipment_SetModUnknown(object arg0)
		{
			return GS(306475787847983104L, arg0);
		}

		public static string Equipment_SetModMissing(object arg0, object arg1)
		{
			return GS(306475705794813952L, arg0, arg1);
		}

		public static string Equipment_SetUsedModsStatus(object arg0, int arg1)
		{
			return GS(306483346902343680L, arg0, arg1);
		}

		public static string Equipment_CannotEquipPromptEnd(object arg0)
		{
			return GS(306560550168420352L, arg0);
		}

		public static string LowMemoryTextureScaleOptionsInfo(object arg0, object arg1, object arg2)
		{
			return GS(310941797204353024L, arg0, arg1, arg2);
		}

		public static string VariableBoolEnableContextMenu(object arg0)
		{
			return GS(313887217803059200L, arg0);
		}

		public static string VariableBoolDisableContextMenu(object arg0)
		{
			return GS(313887667524722688L, arg0);
		}

		public static string SelectionTruncatedSummaryEnd(int arg0)
		{
			return GS(316261124338081792L, arg0);
		}

		public static string ChooseEquipmentPrompt(object arg0)
		{
			return GS(316612621794009088L, arg0);
		}

		public static string IngredientSelectionPrompt(object arg0)
		{
			return GS(317096312936849408L, arg0);
		}

		public static string Blog_LoggedInAs(object arg0)
		{
			return GS(322492133576531968L, arg0);
		}

		public static string StatusApp_ConsoleWidth(object arg0)
		{
			return GS(323968019110813696L, arg0);
		}

		public static string StatusApp_ConnectingTo(object arg0)
		{
			return GS(323968599883505664L, arg0);
		}

		public static string StatusApp_HelloMessage(object arg0)
		{
			return GS(323969204228186112L, arg0);
		}

		public static string itemAddedToYourInventory(object arg0)
		{
			return GS(323998529409245184L, arg0);
		}

		public static string HairSetting_HelpDescription2(object arg0, object arg1)
		{
			return GS(324681566241415168L, arg0, arg1);
		}

		public static string HairSetting_HelpDescription3(object arg0)
		{
			return GS(324682385728724992L, arg0);
		}

		public static string HairSetting_HelpDescription4(object arg0)
		{
			return GS(324684559770705920L, arg0);
		}

		public static string FoodEffect_MaxCumIncrease(object arg0)
		{
			return GS(327254785859248128L, arg0);
		}

		public static string FoodEffect_MaxCumDecrease(object arg0)
		{
			return GS(327257149269860352L, arg0);
		}

		public static string FoodEffect_StaminaRecovery(object arg0)
		{
			return GS(327257721863659520L, arg0);
		}

		public static string FoodEffect_StaminaLoss(object arg0)
		{
			return GS(327257724082446336L, arg0);
		}

		public static string FoodEffect_CurrentCumIncrease(object arg0)
		{
			return GS(327258303588458496L, arg0);
		}

		public static string FoodEffect_CurrentCumDecrease(object arg0)
		{
			return GS(327258305442340864L, arg0);
		}

		public static string CookingSong_IngredientFishAtLeastXkg(object arg0)
		{
			return GS(327605476918288384L, arg0);
		}

		public static string ComparsionToEquippedItem_IncreasedByPercent(object arg0)
		{
			return GS(328691781546795008L, arg0);
		}

		public static string ComparsionToEquippedItem_DecreasedByPercent(object arg0)
		{
			return GS(328691782935109632L, arg0);
		}

		public static string ItemModifier_FishCalibrated(object arg0)
		{
			return GS(328984205804429312L, arg0);
		}

		public static string ItemModifier_FishCalibratedDescription(object arg0)
		{
			return GS(328984269666902016L, arg0);
		}

		public static string CaughtItemCount(int arg0)
		{
			return GS(329431909940584448L, arg0);
		}

		public static string CaughtCountFish(int arg0)
		{
			return GS(329744322158125056L, arg0);
		}

		public static string Fisher_SellAllFishPrompt(int arg0, object arg1)
		{
			return GS(329868300516319232L, arg0, arg1);
		}

		public static string FileCommandFileContents(object arg0)
		{
			return GS(330588015542919168L, arg0);
		}

		public static string CookingMInigame_Help2(object arg0, object arg1)
		{
			return GS(332101628095094784L, arg0, arg1);
		}

		public static string stocksdotic_OwnershipLimitInfo(object arg0)
		{
			return GS(341650822040379392L, arg0);
		}

		public static string Equipment_SetEquippingCopiedBooleanVariable(object arg0, object arg1)
		{
			return GS(341678415997296640L, arg0, arg1);
		}

		public static string Options_DisplaySettingCheckPrompt(int arg0)
		{
			return GS(446085771639644160L, arg0);
		}

		public static string UI_RentAmountInfo(object arg0)
		{
			return GS(456239488389079040L, arg0);
		}

		public static string UI_YouHaventShoweredForXDays(int arg0)
		{
			return GS(458372339297411072L, arg0);
		}

		public static string bangdotic_EmailStatusHeader(object arg0)
		{
			return GS(472129961276203008L, arg0);
		}

		public static string Equipment_OwnPackage(object arg0)
		{
			return GS(476191918648188928L, arg0);
		}

		public static string Equipment_OwnSlot(object arg0, object arg1)
		{
			return GS(476193003509112832L, arg0, arg1);
		}

		public static string Equipment_RelatedOwnedItemsInfoText(object arg0)
		{
			return GS(476194174818836480L, arg0);
		}

		public static string Achievments_StoryTextPercent(object arg0)
		{
			return GS(476526596789886976L, arg0);
		}

		public static string Achievements_NextUnseedGoodEnding(object arg0)
		{
			return GS(484477438352416768L, arg0);
		}

		public static string Achievements_GoodEndingsCount(object arg0, object arg1)
		{
			return GS(484487836430491648L, arg0, arg1);
		}

		public static string Achievements_BadEndingsCount(object arg0, object arg1)
		{
			return GS(484487912431280128L, arg0, arg1);
		}

		public static string Equipment_SetEquippingCopiedFloatVariable(object arg0, object arg1)
		{
			return GS(488163137152868352L, arg0, arg1);
		}

		public static string Equipment_CannotChangeOrderWhileSortedAllowedSortPrompt(object arg0)
		{
			return GS(489289234281668608L, arg0);
		}

		public static string SuccessfulyGenerateTranslationFile(object arg0, object arg1)
		{
			return GS(489646130897362944L, arg0, arg1);
		}

		public static string SuccessfulyLoadedModTranslationFile(object arg0, object arg1, object arg2)
		{
			return GS(489666750372651008L, arg0, arg1, arg2);
		}

		public static string FailedLoadingModTranslationFile(object arg0, object arg1)
		{
			return GS(489667826001911808L, arg0, arg1);
		}

		public static string LoginInfo_GameEarlyAccess(object arg0)
		{
			return GS(493924743922589696L, arg0);
		}

		public static string LoginInfo_DiscordCommand(object arg0)
		{
			return GS(493927354675503104L, arg0);
		}

		public static string LoginInfo_Alternative(object arg0)
		{
			return GS(493982925793533952L, arg0);
		}

		public static string SAVE_InvalidSlotNumberNotNumber(object arg0)
		{
			return GS(498334095147667456L, arg0);
		}

		public static string SAVE_InvalidSlotNumberOutOfRange(object arg0)
		{
			return GS(498334455421603840L, arg0);
		}

		public static string NumbersRangeInfo(object arg0, object arg1)
		{
			return GS(498334650720980992L, arg0, arg1);
		}

		public static string SAVE_SuccessfullyLoadedSlotDataIntoSlotNumber(object arg0)
		{
			return GS(498338501477928960L, arg0);
		}

		public static string LoginInfo_PublicReleaseData(object arg0)
		{
			return GS(498590055413977088L, arg0);
		}

		public static string OpenedLink(object arg0)
		{
			return GS(550092321927979008L, arg0);
		}

		public static string Cheat_IncreasedCumCapacity(object arg0, object arg1)
		{
			return GS(550151887713722368L, arg0, arg1);
		}

		public static string Cheat_AddMoney(object arg0)
		{
			return GS(550157358130520064L, arg0);
		}

		public static string SubscribeOn(object arg0)
		{
			return GS(552261364487282688L, arg0);
		}

		public static LOS.TupleReference GetTupleReference(long entryId)
		{
			return new LOS.TupleReference("UI", entryId);
		}
	}
}
