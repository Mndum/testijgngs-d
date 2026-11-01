using System;
using System.Collections.Generic;
using IC.AutomaticReference;
using MoonSharp.Interpreter;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataGenerated", menuName = "Automated/SoundDataGenerated", order = 1)]
public class SoundDataGenerated : SingletonScriptableObject<SoundDataGenerated>, ISerializationCallbackReceiver, IAutomaticReferenceContainer
{
	[Serializable]
	public class SoundContainer
	{
		public SerializableAutomaticReference<AudioClip> Bell_bell1;

		public SerializableAutomaticReference<AudioClip> Bell_bell2;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Bell_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Bell_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Bell_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout1;

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout2;

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout3;

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout4;

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout5;

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout6;

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout7;

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout8;

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout9;

		public SerializableAutomaticReference<AudioClip> Blowjob_bottomout_bottomout10;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Blowjob_bottomout_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Blowjob_bottomout_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Blowjob_bottomout_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Blowjob_lick_lick1;

		public SerializableAutomaticReference<AudioClip> Blowjob_lick_lick2;

		public SerializableAutomaticReference<AudioClip> Blowjob_lick_lick3;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Blowjob_lick_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Blowjob_lick_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Blowjob_lick_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Blowjob_move_blowjobmove1;

		public SerializableAutomaticReference<AudioClip> Blowjob_move_blowjobmove2;

		public SerializableAutomaticReference<AudioClip> Blowjob_move_blowjobmove3;

		public SerializableAutomaticReference<AudioClip> Blowjob_move_blowjobmove4;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Blowjob_move_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Blowjob_move_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Blowjob_move_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Blowjob_pullout_pullout1;

		public SerializableAutomaticReference<AudioClip> Blowjob_pullout_pullout2;

		public SerializableAutomaticReference<AudioClip> Blowjob_pullout_pullout3;

		public SerializableAutomaticReference<AudioClip> Blowjob_pullout_pullout4;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Blowjob_pullout_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Blowjob_pullout_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Blowjob_pullout_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Blowjob_swallow_swallow1;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Blowjob_swallow_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Blowjob_swallow_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Blowjob_swallow_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Casino_Card_Place;

		public SerializableAutomaticReference<AudioClip> Casino_Card_Take;

		public SerializableAutomaticReference<AudioClip> Casino_Slot_Click;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Casino_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Casino_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Casino_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> CookingMinigame_clap;

		public SerializableAutomaticReference<AudioClip> CookingMinigame_kick;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> CookingMinigame_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> CookingMinigame_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] CookingMinigame_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> FishingMinigame_Bite_Bite1;

		public SerializableAutomaticReference<AudioClip> FishingMinigame_Bite_Bite2;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> FishingMinigame_Bite_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> FishingMinigame_Bite_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] FishingMinigame_Bite_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> FishingMinigame_Duel_Defeat;

		public SerializableAutomaticReference<AudioClip> FishingMinigame_Duel_Victory;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> FishingMinigame_Duel_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> FishingMinigame_Duel_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] FishingMinigame_Duel_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> FishingMinigame_FishFight_FishFight1;

		public SerializableAutomaticReference<AudioClip> FishingMinigame_FishFight_FishFight2;

		public SerializableAutomaticReference<AudioClip> FishingMinigame_FishFight_FishFight3;

		public SerializableAutomaticReference<AudioClip> FishingMinigame_FishFight_FishFight4;

		public SerializableAutomaticReference<AudioClip> FishingMinigame_FishFight_FishFight5;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> FishingMinigame_FishFight_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> FishingMinigame_FishFight_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] FishingMinigame_FishFight_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> FishingMinigame_FishPullOut_Splash_21;

		public SerializableAutomaticReference<AudioClip> FishingMinigame_FishPullOut_Splash_22;

		public SerializableAutomaticReference<AudioClip> FishingMinigame_FishPullOut_Splash_23;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> FishingMinigame_FishPullOut_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> FishingMinigame_FishPullOut_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] FishingMinigame_FishPullOut_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> FishingMinigame_FloatIn_FloatIn;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> FishingMinigame_FloatIn_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> FishingMinigame_FloatIn_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] FishingMinigame_FloatIn_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> FishingMinigame_Reel_ReelLoop;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> FishingMinigame_Reel_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> FishingMinigame_Reel_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] FishingMinigame_Reel_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> FishingMinigame_Throw_throw;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> FishingMinigame_Throw_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> FishingMinigame_Throw_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] FishingMinigame_Throw_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Food_drink;

		public SerializableAutomaticReference<AudioClip> Food_eat;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Food_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Food_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Food_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Frick_clothes_move_clothes_move_1;

		public SerializableAutomaticReference<AudioClip> Frick_clothes_move_clothes_move_2;

		public SerializableAutomaticReference<AudioClip> Frick_clothes_move_clothes_move_3;

		public SerializableAutomaticReference<AudioClip> Frick_clothes_move_clothes_move_4;

		public SerializableAutomaticReference<AudioClip> Frick_clothes_move_clothes_move_5;

		public SerializableAutomaticReference<AudioClip> Frick_clothes_move_clothes_move_6;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Frick_clothes_move_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Frick_clothes_move_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Frick_clothes_move_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Frick_cum_cum1;

		public SerializableAutomaticReference<AudioClip> Frick_cum_cum2;

		public SerializableAutomaticReference<AudioClip> Frick_cum_cum3;

		public SerializableAutomaticReference<AudioClip> Frick_cum_cum4;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Frick_cum_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Frick_cum_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Frick_cum_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Frick_insert_anal_insert_anal_1;

		public SerializableAutomaticReference<AudioClip> Frick_insert_anal_insert_anal_2;

		public SerializableAutomaticReference<AudioClip> Frick_insert_anal_insert_anal_3;

		public SerializableAutomaticReference<AudioClip> Frick_insert_anal_insert_anal_4;

		public SerializableAutomaticReference<AudioClip> Frick_insert_anal_insert_anal_5;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Frick_insert_anal_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Frick_insert_anal_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Frick_insert_anal_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Frick_insert_vagina_insert_1;

		public SerializableAutomaticReference<AudioClip> Frick_insert_vagina_insert_2;

		public SerializableAutomaticReference<AudioClip> Frick_insert_vagina_insert_3;

		public SerializableAutomaticReference<AudioClip> Frick_insert_vagina_insert_4;

		public SerializableAutomaticReference<AudioClip> Frick_insert_vagina_insert_5;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Frick_insert_vagina_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Frick_insert_vagina_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Frick_insert_vagina_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Frick_move_inside_move_1;

		public SerializableAutomaticReference<AudioClip> Frick_move_inside_move_2;

		public SerializableAutomaticReference<AudioClip> Frick_move_inside_move_3;

		public SerializableAutomaticReference<AudioClip> Frick_move_inside_move_4;

		public SerializableAutomaticReference<AudioClip> Frick_move_inside_move_5;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Frick_move_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Frick_move_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Frick_move_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Frick_slam_slap1;

		public SerializableAutomaticReference<AudioClip> Frick_slam_slap2;

		public SerializableAutomaticReference<AudioClip> Frick_slam_slap3;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Frick_slam_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Frick_slam_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Frick_slam_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_001;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_002;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_005;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_006;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_008;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_009;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_011;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_012;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_013;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_014;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_015;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_016;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_028;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_029;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Light_Squish___Spanks_031;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Frick_spank_Light_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Frick_spank_Light_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Frick_spank_Light_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_003;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_004;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_007;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_010;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_017;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_018;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_019;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_020;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_021;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_022;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_024;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_025;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_027;

		public SerializableAutomaticReference<AudioClip> Frick_spank_Normal_Squish___Spanks_032;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Frick_spank_Normal_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Frick_spank_Normal_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Frick_spank_Normal_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Frick_unzip_unzip;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Frick_unzip_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Frick_unzip_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Frick_unzip_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Generic_Whoosh_Organic_Whoosh_1;

		public SerializableAutomaticReference<AudioClip> Generic_Whoosh_Organic_Whoosh_2;

		public SerializableAutomaticReference<AudioClip> Generic_Whoosh_Organic_Whoosh_3;

		public SerializableAutomaticReference<AudioClip> Generic_Whoosh_Organic_Whoosh_4;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Generic_Whoosh_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Generic_Whoosh_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Generic_Whoosh_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Money_Coins_Handled_01;

		public SerializableAutomaticReference<AudioClip> Money_Coins_Handled_02;

		public SerializableAutomaticReference<AudioClip> Money_Coins_Handled_03;

		public SerializableAutomaticReference<AudioClip> Money_Coins_Shake_Hand;

		public SerializableAutomaticReference<AudioClip> Money_Coins_Sift_Through_01;

		public SerializableAutomaticReference<AudioClip> Money_Coins_Sift_Through_02;

		public SerializableAutomaticReference<AudioClip> Money_Coins_Sift_Through_03;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Money_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Money_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Money_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Outside_Open_shop;

		public SerializableAutomaticReference<AudioClip> Outside_Shop_bell;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Outside_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Outside_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Outside_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> PC_pc_continue;

		public SerializableAutomaticReference<AudioClip> PC_pc_end_1;

		public SerializableAutomaticReference<AudioClip> PC_pc_start;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> PC_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> PC_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] PC_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Room_Clock_tick;

		public SerializableAutomaticReference<AudioClip> Room_Cloth;

		public SerializableAutomaticReference<AudioClip> Room_door_close;

		public SerializableAutomaticReference<AudioClip> Room_Door_knocking;

		public SerializableAutomaticReference<AudioClip> Room_door_open;

		public SerializableAutomaticReference<AudioClip> Room_Hangup;

		public SerializableAutomaticReference<AudioClip> Room_Plug_in;

		public SerializableAutomaticReference<AudioClip> Room_Ringtone;

		public SerializableAutomaticReference<AudioClip> Room_switch1;

		public SerializableAutomaticReference<AudioClip> Room_switch2;

		public SerializableAutomaticReference<AudioClip> Room_Vibration;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Room_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Room_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Room_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Shower_Bubble_Bubbles_1;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Shower_Bubble_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Shower_Bubble_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Shower_Bubble_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Shower_runShower;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Shower_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Shower_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Shower_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Spook_Spook1;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Spook_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Spook_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Spook_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		public SerializableAutomaticReference<AudioClip> Story_cuffs;

		public SerializableAutomaticReference<AudioClip> Story_Dumpster_hit;

		public SerializableAutomaticReference<AudioClip> Story_Ear_ring;

		public SerializableAutomaticReference<AudioClip> Story_Explosion;

		public SerializableAutomaticReference<AudioClip> Story_Explosion2;

		public SerializableAutomaticReference<AudioClip> Story_falling_concrete;

		public SerializableAutomaticReference<AudioClip> Story_Gunshot;

		public SerializableAutomaticReference<AudioClip> Story_Melissafalls;

		public SerializableAutomaticReference<AudioClip> Story_Opening_a_car_door;

		public SerializableAutomaticReference<AudioClip> Story_pee;

		public SerializableAutomaticReference<AudioClip> Story_Police;

		public SerializableAutomaticReference<AudioClip> Story_pop;

		public SerializableAutomaticReference<AudioClip> Story_slam;

		public SerializableAutomaticReference<AudioClip> Story_snort;

		public SerializableAutomaticReference<AudioClip> Story_Splat;

		public SerializableAutomaticReference<AudioClip> Story_Steps;

		public SerializableAutomaticReference<AudioClip> Story_stepsDAK;

		public SerializableAutomaticReference<AudioClip> Story_tape_load;

		public SerializableAutomaticReference<AudioClip> Story_tazer;

		public SerializableAutomaticReference<AudioClip> Story_Trash_Open;

		public SerializableAutomaticReference<AudioClip> Story_window_break;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<AudioClip>> Story_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<AudioClip>> Story_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] Story_All = Array.Empty<SerializableAutomaticReference<AudioClip>>();

		[NonSerialized]
		public SerializableAutomaticReference<AudioClip>[] All = Array.Empty<SerializableAutomaticReference<AudioClip>>();
	}

	public SoundContainer Sound;

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		Sound.Bell_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["bell1"] = Sound.Bell_bell1,
			["bell2"] = Sound.Bell_bell2
		};
		Sound.Bell_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("bell1", "bell1")] = Sound.Bell_bell1,
			[("bell2", "bell2")] = Sound.Bell_bell2
		};
		Sound.Bell_All = new SerializableAutomaticReference<AudioClip>[2] { Sound.Bell_bell1, Sound.Bell_bell2 };
		Sound.Blowjob_bottomout_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["bottomout1"] = Sound.Blowjob_bottomout_bottomout1,
			["bottomout2"] = Sound.Blowjob_bottomout_bottomout2,
			["bottomout3"] = Sound.Blowjob_bottomout_bottomout3,
			["bottomout4"] = Sound.Blowjob_bottomout_bottomout4,
			["bottomout5"] = Sound.Blowjob_bottomout_bottomout5,
			["bottomout6"] = Sound.Blowjob_bottomout_bottomout6,
			["bottomout7"] = Sound.Blowjob_bottomout_bottomout7,
			["bottomout8"] = Sound.Blowjob_bottomout_bottomout8,
			["bottomout9"] = Sound.Blowjob_bottomout_bottomout9,
			["bottomout10"] = Sound.Blowjob_bottomout_bottomout10
		};
		Sound.Blowjob_bottomout_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("bottomout1", "bottomout1")] = Sound.Blowjob_bottomout_bottomout1,
			[("bottomout2", "bottomout2")] = Sound.Blowjob_bottomout_bottomout2,
			[("bottomout3", "bottomout3")] = Sound.Blowjob_bottomout_bottomout3,
			[("bottomout4", "bottomout4")] = Sound.Blowjob_bottomout_bottomout4,
			[("bottomout5", "bottomout5")] = Sound.Blowjob_bottomout_bottomout5,
			[("bottomout6", "bottomout6")] = Sound.Blowjob_bottomout_bottomout6,
			[("bottomout7", "bottomout7")] = Sound.Blowjob_bottomout_bottomout7,
			[("bottomout8", "bottomout8")] = Sound.Blowjob_bottomout_bottomout8,
			[("bottomout9", "bottomout9")] = Sound.Blowjob_bottomout_bottomout9,
			[("bottomout10", "bottomout10")] = Sound.Blowjob_bottomout_bottomout10
		};
		Sound.Blowjob_bottomout_All = new SerializableAutomaticReference<AudioClip>[10] { Sound.Blowjob_bottomout_bottomout1, Sound.Blowjob_bottomout_bottomout2, Sound.Blowjob_bottomout_bottomout3, Sound.Blowjob_bottomout_bottomout4, Sound.Blowjob_bottomout_bottomout5, Sound.Blowjob_bottomout_bottomout6, Sound.Blowjob_bottomout_bottomout7, Sound.Blowjob_bottomout_bottomout8, Sound.Blowjob_bottomout_bottomout9, Sound.Blowjob_bottomout_bottomout10 };
		Sound.Blowjob_lick_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["lick1"] = Sound.Blowjob_lick_lick1,
			["lick2"] = Sound.Blowjob_lick_lick2,
			["lick3"] = Sound.Blowjob_lick_lick3
		};
		Sound.Blowjob_lick_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("lick1", "lick1")] = Sound.Blowjob_lick_lick1,
			[("lick2", "lick2")] = Sound.Blowjob_lick_lick2,
			[("lick3", "lick3")] = Sound.Blowjob_lick_lick3
		};
		Sound.Blowjob_lick_All = new SerializableAutomaticReference<AudioClip>[3] { Sound.Blowjob_lick_lick1, Sound.Blowjob_lick_lick2, Sound.Blowjob_lick_lick3 };
		Sound.Blowjob_move_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["blowjobmove1"] = Sound.Blowjob_move_blowjobmove1,
			["blowjobmove2"] = Sound.Blowjob_move_blowjobmove2,
			["blowjobmove3"] = Sound.Blowjob_move_blowjobmove3,
			["blowjobmove4"] = Sound.Blowjob_move_blowjobmove4
		};
		Sound.Blowjob_move_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("blowjobmove1", "blowjobmove1")] = Sound.Blowjob_move_blowjobmove1,
			[("blowjobmove2", "blowjobmove2")] = Sound.Blowjob_move_blowjobmove2,
			[("blowjobmove3", "blowjobmove3")] = Sound.Blowjob_move_blowjobmove3,
			[("blowjobmove4", "blowjobmove4")] = Sound.Blowjob_move_blowjobmove4
		};
		Sound.Blowjob_move_All = new SerializableAutomaticReference<AudioClip>[4] { Sound.Blowjob_move_blowjobmove1, Sound.Blowjob_move_blowjobmove2, Sound.Blowjob_move_blowjobmove3, Sound.Blowjob_move_blowjobmove4 };
		Sound.Blowjob_pullout_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["pullout1"] = Sound.Blowjob_pullout_pullout1,
			["pullout2"] = Sound.Blowjob_pullout_pullout2,
			["pullout3"] = Sound.Blowjob_pullout_pullout3,
			["pullout4"] = Sound.Blowjob_pullout_pullout4
		};
		Sound.Blowjob_pullout_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("pullout1", "pullout1")] = Sound.Blowjob_pullout_pullout1,
			[("pullout2", "pullout2")] = Sound.Blowjob_pullout_pullout2,
			[("pullout3", "pullout3")] = Sound.Blowjob_pullout_pullout3,
			[("pullout4", "pullout4")] = Sound.Blowjob_pullout_pullout4
		};
		Sound.Blowjob_pullout_All = new SerializableAutomaticReference<AudioClip>[4] { Sound.Blowjob_pullout_pullout1, Sound.Blowjob_pullout_pullout2, Sound.Blowjob_pullout_pullout3, Sound.Blowjob_pullout_pullout4 };
		Sound.Blowjob_swallow_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>> { ["swallow1"] = Sound.Blowjob_swallow_swallow1 };
		Sound.Blowjob_swallow_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>> { [("swallow1", "swallow1")] = Sound.Blowjob_swallow_swallow1 };
		Sound.Blowjob_swallow_All = new SerializableAutomaticReference<AudioClip>[1] { Sound.Blowjob_swallow_swallow1 };
		Sound.Casino_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Card Place"] = Sound.Casino_Card_Place,
			["Card Take"] = Sound.Casino_Card_Take,
			["Slot Click"] = Sound.Casino_Slot_Click
		};
		Sound.Casino_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Card Place", "Card Place")] = Sound.Casino_Card_Place,
			[("Card Take", "Card Take")] = Sound.Casino_Card_Take,
			[("Slot Click", "Slot Click")] = Sound.Casino_Slot_Click
		};
		Sound.Casino_All = new SerializableAutomaticReference<AudioClip>[3] { Sound.Casino_Card_Place, Sound.Casino_Card_Take, Sound.Casino_Slot_Click };
		Sound.CookingMinigame_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["clap"] = Sound.CookingMinigame_clap,
			["kick"] = Sound.CookingMinigame_kick
		};
		Sound.CookingMinigame_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("clap", "clap")] = Sound.CookingMinigame_clap,
			[("kick", "kick")] = Sound.CookingMinigame_kick
		};
		Sound.CookingMinigame_All = new SerializableAutomaticReference<AudioClip>[2] { Sound.CookingMinigame_clap, Sound.CookingMinigame_kick };
		Sound.FishingMinigame_Bite_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Bite1"] = Sound.FishingMinigame_Bite_Bite1,
			["Bite2"] = Sound.FishingMinigame_Bite_Bite2
		};
		Sound.FishingMinigame_Bite_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Bite1", "Bite1")] = Sound.FishingMinigame_Bite_Bite1,
			[("Bite2", "Bite2")] = Sound.FishingMinigame_Bite_Bite2
		};
		Sound.FishingMinigame_Bite_All = new SerializableAutomaticReference<AudioClip>[2] { Sound.FishingMinigame_Bite_Bite1, Sound.FishingMinigame_Bite_Bite2 };
		Sound.FishingMinigame_Duel_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Defeat"] = Sound.FishingMinigame_Duel_Defeat,
			["Victory"] = Sound.FishingMinigame_Duel_Victory
		};
		Sound.FishingMinigame_Duel_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Defeat", "Defeat")] = Sound.FishingMinigame_Duel_Defeat,
			[("Victory", "Victory")] = Sound.FishingMinigame_Duel_Victory
		};
		Sound.FishingMinigame_Duel_All = new SerializableAutomaticReference<AudioClip>[2] { Sound.FishingMinigame_Duel_Defeat, Sound.FishingMinigame_Duel_Victory };
		Sound.FishingMinigame_FishFight_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["FishFight1"] = Sound.FishingMinigame_FishFight_FishFight1,
			["FishFight2"] = Sound.FishingMinigame_FishFight_FishFight2,
			["FishFight3"] = Sound.FishingMinigame_FishFight_FishFight3,
			["FishFight4"] = Sound.FishingMinigame_FishFight_FishFight4,
			["FishFight5"] = Sound.FishingMinigame_FishFight_FishFight5
		};
		Sound.FishingMinigame_FishFight_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("FishFight1", "FishFight1")] = Sound.FishingMinigame_FishFight_FishFight1,
			[("FishFight2", "FishFight2")] = Sound.FishingMinigame_FishFight_FishFight2,
			[("FishFight3", "FishFight3")] = Sound.FishingMinigame_FishFight_FishFight3,
			[("FishFight4", "FishFight4")] = Sound.FishingMinigame_FishFight_FishFight4,
			[("FishFight5", "FishFight5")] = Sound.FishingMinigame_FishFight_FishFight5
		};
		Sound.FishingMinigame_FishFight_All = new SerializableAutomaticReference<AudioClip>[5] { Sound.FishingMinigame_FishFight_FishFight1, Sound.FishingMinigame_FishFight_FishFight2, Sound.FishingMinigame_FishFight_FishFight3, Sound.FishingMinigame_FishFight_FishFight4, Sound.FishingMinigame_FishFight_FishFight5 };
		Sound.FishingMinigame_FishPullOut_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Splash_21"] = Sound.FishingMinigame_FishPullOut_Splash_21,
			["Splash_22"] = Sound.FishingMinigame_FishPullOut_Splash_22,
			["Splash_23"] = Sound.FishingMinigame_FishPullOut_Splash_23
		};
		Sound.FishingMinigame_FishPullOut_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Splash_21", "Splash_21")] = Sound.FishingMinigame_FishPullOut_Splash_21,
			[("Splash_22", "Splash_22")] = Sound.FishingMinigame_FishPullOut_Splash_22,
			[("Splash_23", "Splash_23")] = Sound.FishingMinigame_FishPullOut_Splash_23
		};
		Sound.FishingMinigame_FishPullOut_All = new SerializableAutomaticReference<AudioClip>[3] { Sound.FishingMinigame_FishPullOut_Splash_21, Sound.FishingMinigame_FishPullOut_Splash_22, Sound.FishingMinigame_FishPullOut_Splash_23 };
		Sound.FishingMinigame_FloatIn_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>> { ["FloatIn"] = Sound.FishingMinigame_FloatIn_FloatIn };
		Sound.FishingMinigame_FloatIn_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>> { [("FloatIn", "FloatIn")] = Sound.FishingMinigame_FloatIn_FloatIn };
		Sound.FishingMinigame_FloatIn_All = new SerializableAutomaticReference<AudioClip>[1] { Sound.FishingMinigame_FloatIn_FloatIn };
		Sound.FishingMinigame_Reel_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>> { ["ReelLoop"] = Sound.FishingMinigame_Reel_ReelLoop };
		Sound.FishingMinigame_Reel_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>> { [("ReelLoop", "ReelLoop")] = Sound.FishingMinigame_Reel_ReelLoop };
		Sound.FishingMinigame_Reel_All = new SerializableAutomaticReference<AudioClip>[1] { Sound.FishingMinigame_Reel_ReelLoop };
		Sound.FishingMinigame_Throw_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>> { ["throw"] = Sound.FishingMinigame_Throw_throw };
		Sound.FishingMinigame_Throw_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>> { [("throw", "throw")] = Sound.FishingMinigame_Throw_throw };
		Sound.FishingMinigame_Throw_All = new SerializableAutomaticReference<AudioClip>[1] { Sound.FishingMinigame_Throw_throw };
		Sound.Food_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["drink"] = Sound.Food_drink,
			["eat"] = Sound.Food_eat
		};
		Sound.Food_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("drink", "drink")] = Sound.Food_drink,
			[("eat", "eat")] = Sound.Food_eat
		};
		Sound.Food_All = new SerializableAutomaticReference<AudioClip>[2] { Sound.Food_drink, Sound.Food_eat };
		Sound.Frick_clothes_move_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["clothes_move_1"] = Sound.Frick_clothes_move_clothes_move_1,
			["clothes_move_2"] = Sound.Frick_clothes_move_clothes_move_2,
			["clothes_move_3"] = Sound.Frick_clothes_move_clothes_move_3,
			["clothes_move_4"] = Sound.Frick_clothes_move_clothes_move_4,
			["clothes_move_5"] = Sound.Frick_clothes_move_clothes_move_5,
			["clothes_move_6"] = Sound.Frick_clothes_move_clothes_move_6
		};
		Sound.Frick_clothes_move_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("clothes_move_1", "clothes_move_1")] = Sound.Frick_clothes_move_clothes_move_1,
			[("clothes_move_2", "clothes_move_2")] = Sound.Frick_clothes_move_clothes_move_2,
			[("clothes_move_3", "clothes_move_3")] = Sound.Frick_clothes_move_clothes_move_3,
			[("clothes_move_4", "clothes_move_4")] = Sound.Frick_clothes_move_clothes_move_4,
			[("clothes_move_5", "clothes_move_5")] = Sound.Frick_clothes_move_clothes_move_5,
			[("clothes_move_6", "clothes_move_6")] = Sound.Frick_clothes_move_clothes_move_6
		};
		Sound.Frick_clothes_move_All = new SerializableAutomaticReference<AudioClip>[6] { Sound.Frick_clothes_move_clothes_move_1, Sound.Frick_clothes_move_clothes_move_2, Sound.Frick_clothes_move_clothes_move_3, Sound.Frick_clothes_move_clothes_move_4, Sound.Frick_clothes_move_clothes_move_5, Sound.Frick_clothes_move_clothes_move_6 };
		Sound.Frick_cum_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["cum1"] = Sound.Frick_cum_cum1,
			["cum2"] = Sound.Frick_cum_cum2,
			["cum3"] = Sound.Frick_cum_cum3,
			["cum4"] = Sound.Frick_cum_cum4
		};
		Sound.Frick_cum_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("cum1", "cum1")] = Sound.Frick_cum_cum1,
			[("cum2", "cum2")] = Sound.Frick_cum_cum2,
			[("cum3", "cum3")] = Sound.Frick_cum_cum3,
			[("cum4", "cum4")] = Sound.Frick_cum_cum4
		};
		Sound.Frick_cum_All = new SerializableAutomaticReference<AudioClip>[4] { Sound.Frick_cum_cum1, Sound.Frick_cum_cum2, Sound.Frick_cum_cum3, Sound.Frick_cum_cum4 };
		Sound.Frick_insert_anal_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["insert_anal_1"] = Sound.Frick_insert_anal_insert_anal_1,
			["insert_anal_2"] = Sound.Frick_insert_anal_insert_anal_2,
			["insert_anal_3"] = Sound.Frick_insert_anal_insert_anal_3,
			["insert_anal_4"] = Sound.Frick_insert_anal_insert_anal_4,
			["insert_anal_5"] = Sound.Frick_insert_anal_insert_anal_5
		};
		Sound.Frick_insert_anal_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("insert_anal_1", "insert_anal_1")] = Sound.Frick_insert_anal_insert_anal_1,
			[("insert_anal_2", "insert_anal_2")] = Sound.Frick_insert_anal_insert_anal_2,
			[("insert_anal_3", "insert_anal_3")] = Sound.Frick_insert_anal_insert_anal_3,
			[("insert_anal_4", "insert_anal_4")] = Sound.Frick_insert_anal_insert_anal_4,
			[("insert_anal_5", "insert_anal_5")] = Sound.Frick_insert_anal_insert_anal_5
		};
		Sound.Frick_insert_anal_All = new SerializableAutomaticReference<AudioClip>[5] { Sound.Frick_insert_anal_insert_anal_1, Sound.Frick_insert_anal_insert_anal_2, Sound.Frick_insert_anal_insert_anal_3, Sound.Frick_insert_anal_insert_anal_4, Sound.Frick_insert_anal_insert_anal_5 };
		Sound.Frick_insert_vagina_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["insert_1"] = Sound.Frick_insert_vagina_insert_1,
			["insert_2"] = Sound.Frick_insert_vagina_insert_2,
			["insert_3"] = Sound.Frick_insert_vagina_insert_3,
			["insert_4"] = Sound.Frick_insert_vagina_insert_4,
			["insert_5"] = Sound.Frick_insert_vagina_insert_5
		};
		Sound.Frick_insert_vagina_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("insert_1", "insert_1")] = Sound.Frick_insert_vagina_insert_1,
			[("insert_2", "insert_2")] = Sound.Frick_insert_vagina_insert_2,
			[("insert_3", "insert_3")] = Sound.Frick_insert_vagina_insert_3,
			[("insert_4", "insert_4")] = Sound.Frick_insert_vagina_insert_4,
			[("insert_5", "insert_5")] = Sound.Frick_insert_vagina_insert_5
		};
		Sound.Frick_insert_vagina_All = new SerializableAutomaticReference<AudioClip>[5] { Sound.Frick_insert_vagina_insert_1, Sound.Frick_insert_vagina_insert_2, Sound.Frick_insert_vagina_insert_3, Sound.Frick_insert_vagina_insert_4, Sound.Frick_insert_vagina_insert_5 };
		Sound.Frick_move_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["inside_move_1"] = Sound.Frick_move_inside_move_1,
			["inside_move_2"] = Sound.Frick_move_inside_move_2,
			["inside_move_3"] = Sound.Frick_move_inside_move_3,
			["inside_move_4"] = Sound.Frick_move_inside_move_4,
			["inside_move_5"] = Sound.Frick_move_inside_move_5
		};
		Sound.Frick_move_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("inside_move_1", "inside_move_1")] = Sound.Frick_move_inside_move_1,
			[("inside_move_2", "inside_move_2")] = Sound.Frick_move_inside_move_2,
			[("inside_move_3", "inside_move_3")] = Sound.Frick_move_inside_move_3,
			[("inside_move_4", "inside_move_4")] = Sound.Frick_move_inside_move_4,
			[("inside_move_5", "inside_move_5")] = Sound.Frick_move_inside_move_5
		};
		Sound.Frick_move_All = new SerializableAutomaticReference<AudioClip>[5] { Sound.Frick_move_inside_move_1, Sound.Frick_move_inside_move_2, Sound.Frick_move_inside_move_3, Sound.Frick_move_inside_move_4, Sound.Frick_move_inside_move_5 };
		Sound.Frick_slam_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["slap1"] = Sound.Frick_slam_slap1,
			["slap2"] = Sound.Frick_slam_slap2,
			["slap3"] = Sound.Frick_slam_slap3
		};
		Sound.Frick_slam_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("slap1", "slap1")] = Sound.Frick_slam_slap1,
			[("slap2", "slap2")] = Sound.Frick_slam_slap2,
			[("slap3", "slap3")] = Sound.Frick_slam_slap3
		};
		Sound.Frick_slam_All = new SerializableAutomaticReference<AudioClip>[3] { Sound.Frick_slam_slap1, Sound.Frick_slam_slap2, Sound.Frick_slam_slap3 };
		Sound.Frick_spank_Light_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Squish - Spanks-001"] = Sound.Frick_spank_Light_Squish___Spanks_001,
			["Squish - Spanks-002"] = Sound.Frick_spank_Light_Squish___Spanks_002,
			["Squish - Spanks-005"] = Sound.Frick_spank_Light_Squish___Spanks_005,
			["Squish - Spanks-006"] = Sound.Frick_spank_Light_Squish___Spanks_006,
			["Squish - Spanks-008"] = Sound.Frick_spank_Light_Squish___Spanks_008,
			["Squish - Spanks-009"] = Sound.Frick_spank_Light_Squish___Spanks_009,
			["Squish - Spanks-011"] = Sound.Frick_spank_Light_Squish___Spanks_011,
			["Squish - Spanks-012"] = Sound.Frick_spank_Light_Squish___Spanks_012,
			["Squish - Spanks-013"] = Sound.Frick_spank_Light_Squish___Spanks_013,
			["Squish - Spanks-014"] = Sound.Frick_spank_Light_Squish___Spanks_014,
			["Squish - Spanks-015"] = Sound.Frick_spank_Light_Squish___Spanks_015,
			["Squish - Spanks-016"] = Sound.Frick_spank_Light_Squish___Spanks_016,
			["Squish - Spanks-028"] = Sound.Frick_spank_Light_Squish___Spanks_028,
			["Squish - Spanks-029"] = Sound.Frick_spank_Light_Squish___Spanks_029,
			["Squish - Spanks-031"] = Sound.Frick_spank_Light_Squish___Spanks_031
		};
		Sound.Frick_spank_Light_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Squish - Spanks-001", "Squish - Spanks-001")] = Sound.Frick_spank_Light_Squish___Spanks_001,
			[("Squish - Spanks-002", "Squish - Spanks-002")] = Sound.Frick_spank_Light_Squish___Spanks_002,
			[("Squish - Spanks-005", "Squish - Spanks-005")] = Sound.Frick_spank_Light_Squish___Spanks_005,
			[("Squish - Spanks-006", "Squish - Spanks-006")] = Sound.Frick_spank_Light_Squish___Spanks_006,
			[("Squish - Spanks-008", "Squish - Spanks-008")] = Sound.Frick_spank_Light_Squish___Spanks_008,
			[("Squish - Spanks-009", "Squish - Spanks-009")] = Sound.Frick_spank_Light_Squish___Spanks_009,
			[("Squish - Spanks-011", "Squish - Spanks-011")] = Sound.Frick_spank_Light_Squish___Spanks_011,
			[("Squish - Spanks-012", "Squish - Spanks-012")] = Sound.Frick_spank_Light_Squish___Spanks_012,
			[("Squish - Spanks-013", "Squish - Spanks-013")] = Sound.Frick_spank_Light_Squish___Spanks_013,
			[("Squish - Spanks-014", "Squish - Spanks-014")] = Sound.Frick_spank_Light_Squish___Spanks_014,
			[("Squish - Spanks-015", "Squish - Spanks-015")] = Sound.Frick_spank_Light_Squish___Spanks_015,
			[("Squish - Spanks-016", "Squish - Spanks-016")] = Sound.Frick_spank_Light_Squish___Spanks_016,
			[("Squish - Spanks-028", "Squish - Spanks-028")] = Sound.Frick_spank_Light_Squish___Spanks_028,
			[("Squish - Spanks-029", "Squish - Spanks-029")] = Sound.Frick_spank_Light_Squish___Spanks_029,
			[("Squish - Spanks-031", "Squish - Spanks-031")] = Sound.Frick_spank_Light_Squish___Spanks_031
		};
		Sound.Frick_spank_Light_All = new SerializableAutomaticReference<AudioClip>[15]
		{
			Sound.Frick_spank_Light_Squish___Spanks_001, Sound.Frick_spank_Light_Squish___Spanks_002, Sound.Frick_spank_Light_Squish___Spanks_005, Sound.Frick_spank_Light_Squish___Spanks_006, Sound.Frick_spank_Light_Squish___Spanks_008, Sound.Frick_spank_Light_Squish___Spanks_009, Sound.Frick_spank_Light_Squish___Spanks_011, Sound.Frick_spank_Light_Squish___Spanks_012, Sound.Frick_spank_Light_Squish___Spanks_013, Sound.Frick_spank_Light_Squish___Spanks_014,
			Sound.Frick_spank_Light_Squish___Spanks_015, Sound.Frick_spank_Light_Squish___Spanks_016, Sound.Frick_spank_Light_Squish___Spanks_028, Sound.Frick_spank_Light_Squish___Spanks_029, Sound.Frick_spank_Light_Squish___Spanks_031
		};
		Sound.Frick_spank_Normal_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Squish - Spanks-003"] = Sound.Frick_spank_Normal_Squish___Spanks_003,
			["Squish - Spanks-004"] = Sound.Frick_spank_Normal_Squish___Spanks_004,
			["Squish - Spanks-007"] = Sound.Frick_spank_Normal_Squish___Spanks_007,
			["Squish - Spanks-010"] = Sound.Frick_spank_Normal_Squish___Spanks_010,
			["Squish - Spanks-017"] = Sound.Frick_spank_Normal_Squish___Spanks_017,
			["Squish - Spanks-018"] = Sound.Frick_spank_Normal_Squish___Spanks_018,
			["Squish - Spanks-019"] = Sound.Frick_spank_Normal_Squish___Spanks_019,
			["Squish - Spanks-020"] = Sound.Frick_spank_Normal_Squish___Spanks_020,
			["Squish - Spanks-021"] = Sound.Frick_spank_Normal_Squish___Spanks_021,
			["Squish - Spanks-022"] = Sound.Frick_spank_Normal_Squish___Spanks_022,
			["Squish - Spanks-024"] = Sound.Frick_spank_Normal_Squish___Spanks_024,
			["Squish - Spanks-025"] = Sound.Frick_spank_Normal_Squish___Spanks_025,
			["Squish - Spanks-027"] = Sound.Frick_spank_Normal_Squish___Spanks_027,
			["Squish - Spanks-032"] = Sound.Frick_spank_Normal_Squish___Spanks_032
		};
		Sound.Frick_spank_Normal_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Squish - Spanks-003", "Squish - Spanks-003")] = Sound.Frick_spank_Normal_Squish___Spanks_003,
			[("Squish - Spanks-004", "Squish - Spanks-004")] = Sound.Frick_spank_Normal_Squish___Spanks_004,
			[("Squish - Spanks-007", "Squish - Spanks-007")] = Sound.Frick_spank_Normal_Squish___Spanks_007,
			[("Squish - Spanks-010", "Squish - Spanks-010")] = Sound.Frick_spank_Normal_Squish___Spanks_010,
			[("Squish - Spanks-017", "Squish - Spanks-017")] = Sound.Frick_spank_Normal_Squish___Spanks_017,
			[("Squish - Spanks-018", "Squish - Spanks-018")] = Sound.Frick_spank_Normal_Squish___Spanks_018,
			[("Squish - Spanks-019", "Squish - Spanks-019")] = Sound.Frick_spank_Normal_Squish___Spanks_019,
			[("Squish - Spanks-020", "Squish - Spanks-020")] = Sound.Frick_spank_Normal_Squish___Spanks_020,
			[("Squish - Spanks-021", "Squish - Spanks-021")] = Sound.Frick_spank_Normal_Squish___Spanks_021,
			[("Squish - Spanks-022", "Squish - Spanks-022")] = Sound.Frick_spank_Normal_Squish___Spanks_022,
			[("Squish - Spanks-024", "Squish - Spanks-024")] = Sound.Frick_spank_Normal_Squish___Spanks_024,
			[("Squish - Spanks-025", "Squish - Spanks-025")] = Sound.Frick_spank_Normal_Squish___Spanks_025,
			[("Squish - Spanks-027", "Squish - Spanks-027")] = Sound.Frick_spank_Normal_Squish___Spanks_027,
			[("Squish - Spanks-032", "Squish - Spanks-032")] = Sound.Frick_spank_Normal_Squish___Spanks_032
		};
		Sound.Frick_spank_Normal_All = new SerializableAutomaticReference<AudioClip>[14]
		{
			Sound.Frick_spank_Normal_Squish___Spanks_003, Sound.Frick_spank_Normal_Squish___Spanks_004, Sound.Frick_spank_Normal_Squish___Spanks_007, Sound.Frick_spank_Normal_Squish___Spanks_010, Sound.Frick_spank_Normal_Squish___Spanks_017, Sound.Frick_spank_Normal_Squish___Spanks_018, Sound.Frick_spank_Normal_Squish___Spanks_019, Sound.Frick_spank_Normal_Squish___Spanks_020, Sound.Frick_spank_Normal_Squish___Spanks_021, Sound.Frick_spank_Normal_Squish___Spanks_022,
			Sound.Frick_spank_Normal_Squish___Spanks_024, Sound.Frick_spank_Normal_Squish___Spanks_025, Sound.Frick_spank_Normal_Squish___Spanks_027, Sound.Frick_spank_Normal_Squish___Spanks_032
		};
		Sound.Frick_unzip_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>> { ["unzip"] = Sound.Frick_unzip_unzip };
		Sound.Frick_unzip_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>> { [("unzip", "unzip")] = Sound.Frick_unzip_unzip };
		Sound.Frick_unzip_All = new SerializableAutomaticReference<AudioClip>[1] { Sound.Frick_unzip_unzip };
		Sound.Generic_Whoosh_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Organic_Whoosh_1"] = Sound.Generic_Whoosh_Organic_Whoosh_1,
			["Organic_Whoosh_2"] = Sound.Generic_Whoosh_Organic_Whoosh_2,
			["Organic_Whoosh_3"] = Sound.Generic_Whoosh_Organic_Whoosh_3,
			["Organic_Whoosh_4"] = Sound.Generic_Whoosh_Organic_Whoosh_4
		};
		Sound.Generic_Whoosh_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Organic_Whoosh_1", "Organic_Whoosh_1")] = Sound.Generic_Whoosh_Organic_Whoosh_1,
			[("Organic_Whoosh_2", "Organic_Whoosh_2")] = Sound.Generic_Whoosh_Organic_Whoosh_2,
			[("Organic_Whoosh_3", "Organic_Whoosh_3")] = Sound.Generic_Whoosh_Organic_Whoosh_3,
			[("Organic_Whoosh_4", "Organic_Whoosh_4")] = Sound.Generic_Whoosh_Organic_Whoosh_4
		};
		Sound.Generic_Whoosh_All = new SerializableAutomaticReference<AudioClip>[4] { Sound.Generic_Whoosh_Organic_Whoosh_1, Sound.Generic_Whoosh_Organic_Whoosh_2, Sound.Generic_Whoosh_Organic_Whoosh_3, Sound.Generic_Whoosh_Organic_Whoosh_4 };
		Sound.Money_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Coins_Handled_01"] = Sound.Money_Coins_Handled_01,
			["Coins_Handled_02"] = Sound.Money_Coins_Handled_02,
			["Coins_Handled_03"] = Sound.Money_Coins_Handled_03,
			["Coins_Shake_Hand"] = Sound.Money_Coins_Shake_Hand,
			["Coins_Sift_Through_01"] = Sound.Money_Coins_Sift_Through_01,
			["Coins_Sift_Through_02"] = Sound.Money_Coins_Sift_Through_02,
			["Coins_Sift_Through_03"] = Sound.Money_Coins_Sift_Through_03
		};
		Sound.Money_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Coins_Handled_01", "Coins_Handled_01")] = Sound.Money_Coins_Handled_01,
			[("Coins_Handled_02", "Coins_Handled_02")] = Sound.Money_Coins_Handled_02,
			[("Coins_Handled_03", "Coins_Handled_03")] = Sound.Money_Coins_Handled_03,
			[("Coins_Shake_Hand", "Coins_Shake_Hand")] = Sound.Money_Coins_Shake_Hand,
			[("Coins_Sift_Through_01", "Coins_Sift_Through_01")] = Sound.Money_Coins_Sift_Through_01,
			[("Coins_Sift_Through_02", "Coins_Sift_Through_02")] = Sound.Money_Coins_Sift_Through_02,
			[("Coins_Sift_Through_03", "Coins_Sift_Through_03")] = Sound.Money_Coins_Sift_Through_03
		};
		Sound.Money_All = new SerializableAutomaticReference<AudioClip>[7] { Sound.Money_Coins_Handled_01, Sound.Money_Coins_Handled_02, Sound.Money_Coins_Handled_03, Sound.Money_Coins_Shake_Hand, Sound.Money_Coins_Sift_Through_01, Sound.Money_Coins_Sift_Through_02, Sound.Money_Coins_Sift_Through_03 };
		Sound.Outside_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Open shop"] = Sound.Outside_Open_shop,
			["Shop bell"] = Sound.Outside_Shop_bell
		};
		Sound.Outside_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Open shop", "Open shop")] = Sound.Outside_Open_shop,
			[("Shop bell", "Shop bell")] = Sound.Outside_Shop_bell
		};
		Sound.Outside_All = new SerializableAutomaticReference<AudioClip>[2] { Sound.Outside_Open_shop, Sound.Outside_Shop_bell };
		Sound.PC_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["pc continue"] = Sound.PC_pc_continue,
			["pc end 1"] = Sound.PC_pc_end_1,
			["pc start"] = Sound.PC_pc_start
		};
		Sound.PC_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("pc continue", "pc continue")] = Sound.PC_pc_continue,
			[("pc end 1", "pc end 1")] = Sound.PC_pc_end_1,
			[("pc start", "pc start")] = Sound.PC_pc_start
		};
		Sound.PC_All = new SerializableAutomaticReference<AudioClip>[3] { Sound.PC_pc_continue, Sound.PC_pc_end_1, Sound.PC_pc_start };
		Sound.Room_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["Clock tick"] = Sound.Room_Clock_tick,
			["Cloth"] = Sound.Room_Cloth,
			["door close"] = Sound.Room_door_close,
			["Door knocking"] = Sound.Room_Door_knocking,
			["door open"] = Sound.Room_door_open,
			["Hangup"] = Sound.Room_Hangup,
			["Plug in"] = Sound.Room_Plug_in,
			["Ringtone"] = Sound.Room_Ringtone,
			["switch1"] = Sound.Room_switch1,
			["switch2"] = Sound.Room_switch2,
			["Vibration"] = Sound.Room_Vibration
		};
		Sound.Room_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("Clock tick", "Clock tick")] = Sound.Room_Clock_tick,
			[("Cloth", "Cloth")] = Sound.Room_Cloth,
			[("door close", "door close")] = Sound.Room_door_close,
			[("Door knocking", "Door knocking")] = Sound.Room_Door_knocking,
			[("door open", "door open")] = Sound.Room_door_open,
			[("Hangup", "Hangup")] = Sound.Room_Hangup,
			[("Plug in", "Plug in")] = Sound.Room_Plug_in,
			[("Ringtone", "Ringtone")] = Sound.Room_Ringtone,
			[("switch1", "switch1")] = Sound.Room_switch1,
			[("switch2", "switch2")] = Sound.Room_switch2,
			[("Vibration", "Vibration")] = Sound.Room_Vibration
		};
		Sound.Room_All = new SerializableAutomaticReference<AudioClip>[11]
		{
			Sound.Room_Clock_tick, Sound.Room_Cloth, Sound.Room_door_close, Sound.Room_Door_knocking, Sound.Room_door_open, Sound.Room_Hangup, Sound.Room_Plug_in, Sound.Room_Ringtone, Sound.Room_switch1, Sound.Room_switch2,
			Sound.Room_Vibration
		};
		Sound.Shower_Bubble_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>> { ["Bubbles_1"] = Sound.Shower_Bubble_Bubbles_1 };
		Sound.Shower_Bubble_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>> { [("Bubbles_1", "Bubbles_1")] = Sound.Shower_Bubble_Bubbles_1 };
		Sound.Shower_Bubble_All = new SerializableAutomaticReference<AudioClip>[1] { Sound.Shower_Bubble_Bubbles_1 };
		Sound.Shower_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>> { ["runShower"] = Sound.Shower_runShower };
		Sound.Shower_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>> { [("runShower", "runShower")] = Sound.Shower_runShower };
		Sound.Shower_All = new SerializableAutomaticReference<AudioClip>[1] { Sound.Shower_runShower };
		Sound.Spook_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>> { ["Spook1"] = Sound.Spook_Spook1 };
		Sound.Spook_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>> { [("Spook1", "Spook1")] = Sound.Spook_Spook1 };
		Sound.Spook_All = new SerializableAutomaticReference<AudioClip>[1] { Sound.Spook_Spook1 };
		Sound.Story_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<AudioClip>>
		{
			["cuffs"] = Sound.Story_cuffs,
			["Dumpster hit"] = Sound.Story_Dumpster_hit,
			["Ear ring"] = Sound.Story_Ear_ring,
			["Explosion"] = Sound.Story_Explosion,
			["Explosion2"] = Sound.Story_Explosion2,
			["falling concrete"] = Sound.Story_falling_concrete,
			["Gunshot"] = Sound.Story_Gunshot,
			["Melissafalls"] = Sound.Story_Melissafalls,
			["Opening a car door"] = Sound.Story_Opening_a_car_door,
			["pee"] = Sound.Story_pee,
			["Police"] = Sound.Story_Police,
			["pop"] = Sound.Story_pop,
			["slam"] = Sound.Story_slam,
			["snort"] = Sound.Story_snort,
			["Splat"] = Sound.Story_Splat,
			["Steps"] = Sound.Story_Steps,
			["stepsDAK"] = Sound.Story_stepsDAK,
			["tape load"] = Sound.Story_tape_load,
			["tazer"] = Sound.Story_tazer,
			["Trash Open"] = Sound.Story_Trash_Open,
			["window break"] = Sound.Story_window_break
		};
		Sound.Story_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<AudioClip>>
		{
			[("cuffs", "cuffs")] = Sound.Story_cuffs,
			[("Dumpster hit", "Dumpster hit")] = Sound.Story_Dumpster_hit,
			[("Ear ring", "Ear ring")] = Sound.Story_Ear_ring,
			[("Explosion", "Explosion")] = Sound.Story_Explosion,
			[("Explosion2", "Explosion2")] = Sound.Story_Explosion2,
			[("falling concrete", "falling concrete")] = Sound.Story_falling_concrete,
			[("Gunshot", "Gunshot")] = Sound.Story_Gunshot,
			[("Melissafalls", "Melissafalls")] = Sound.Story_Melissafalls,
			[("Opening a car door", "Opening a car door")] = Sound.Story_Opening_a_car_door,
			[("pee", "pee")] = Sound.Story_pee,
			[("Police", "Police")] = Sound.Story_Police,
			[("pop", "pop")] = Sound.Story_pop,
			[("slam", "slam")] = Sound.Story_slam,
			[("snort", "snort")] = Sound.Story_snort,
			[("Splat", "Splat")] = Sound.Story_Splat,
			[("Steps", "Steps")] = Sound.Story_Steps,
			[("stepsDAK", "stepsDAK")] = Sound.Story_stepsDAK,
			[("tape load", "tape load")] = Sound.Story_tape_load,
			[("tazer", "tazer")] = Sound.Story_tazer,
			[("Trash Open", "Trash Open")] = Sound.Story_Trash_Open,
			[("window break", "window break")] = Sound.Story_window_break
		};
		Sound.Story_All = new SerializableAutomaticReference<AudioClip>[21]
		{
			Sound.Story_cuffs, Sound.Story_Dumpster_hit, Sound.Story_Ear_ring, Sound.Story_Explosion, Sound.Story_Explosion2, Sound.Story_falling_concrete, Sound.Story_Gunshot, Sound.Story_Melissafalls, Sound.Story_Opening_a_car_door, Sound.Story_pee,
			Sound.Story_Police, Sound.Story_pop, Sound.Story_slam, Sound.Story_snort, Sound.Story_Splat, Sound.Story_Steps, Sound.Story_stepsDAK, Sound.Story_tape_load, Sound.Story_tazer, Sound.Story_Trash_Open,
			Sound.Story_window_break
		};
		Sound.All = new SerializableAutomaticReference<AudioClip>[155]
		{
			Sound.Bell_bell1, Sound.Bell_bell2, Sound.Blowjob_bottomout_bottomout1, Sound.Blowjob_bottomout_bottomout2, Sound.Blowjob_bottomout_bottomout3, Sound.Blowjob_bottomout_bottomout4, Sound.Blowjob_bottomout_bottomout5, Sound.Blowjob_bottomout_bottomout6, Sound.Blowjob_bottomout_bottomout7, Sound.Blowjob_bottomout_bottomout8,
			Sound.Blowjob_bottomout_bottomout9, Sound.Blowjob_bottomout_bottomout10, Sound.Blowjob_lick_lick1, Sound.Blowjob_lick_lick2, Sound.Blowjob_lick_lick3, Sound.Blowjob_move_blowjobmove1, Sound.Blowjob_move_blowjobmove2, Sound.Blowjob_move_blowjobmove3, Sound.Blowjob_move_blowjobmove4, Sound.Blowjob_pullout_pullout1,
			Sound.Blowjob_pullout_pullout2, Sound.Blowjob_pullout_pullout3, Sound.Blowjob_pullout_pullout4, Sound.Blowjob_swallow_swallow1, Sound.Casino_Card_Place, Sound.Casino_Card_Take, Sound.Casino_Slot_Click, Sound.CookingMinigame_clap, Sound.CookingMinigame_kick, Sound.FishingMinigame_Bite_Bite1,
			Sound.FishingMinigame_Bite_Bite2, Sound.FishingMinigame_Duel_Defeat, Sound.FishingMinigame_Duel_Victory, Sound.FishingMinigame_FishFight_FishFight1, Sound.FishingMinigame_FishFight_FishFight2, Sound.FishingMinigame_FishFight_FishFight3, Sound.FishingMinigame_FishFight_FishFight4, Sound.FishingMinigame_FishFight_FishFight5, Sound.FishingMinigame_FishPullOut_Splash_21, Sound.FishingMinigame_FishPullOut_Splash_22,
			Sound.FishingMinigame_FishPullOut_Splash_23, Sound.FishingMinigame_FloatIn_FloatIn, Sound.FishingMinigame_Reel_ReelLoop, Sound.FishingMinigame_Throw_throw, Sound.Food_drink, Sound.Food_eat, Sound.Frick_clothes_move_clothes_move_1, Sound.Frick_clothes_move_clothes_move_2, Sound.Frick_clothes_move_clothes_move_3, Sound.Frick_clothes_move_clothes_move_4,
			Sound.Frick_clothes_move_clothes_move_5, Sound.Frick_clothes_move_clothes_move_6, Sound.Frick_cum_cum1, Sound.Frick_cum_cum2, Sound.Frick_cum_cum3, Sound.Frick_cum_cum4, Sound.Frick_insert_anal_insert_anal_1, Sound.Frick_insert_anal_insert_anal_2, Sound.Frick_insert_anal_insert_anal_3, Sound.Frick_insert_anal_insert_anal_4,
			Sound.Frick_insert_anal_insert_anal_5, Sound.Frick_insert_vagina_insert_1, Sound.Frick_insert_vagina_insert_2, Sound.Frick_insert_vagina_insert_3, Sound.Frick_insert_vagina_insert_4, Sound.Frick_insert_vagina_insert_5, Sound.Frick_move_inside_move_1, Sound.Frick_move_inside_move_2, Sound.Frick_move_inside_move_3, Sound.Frick_move_inside_move_4,
			Sound.Frick_move_inside_move_5, Sound.Frick_slam_slap1, Sound.Frick_slam_slap2, Sound.Frick_slam_slap3, Sound.Frick_spank_Light_Squish___Spanks_001, Sound.Frick_spank_Light_Squish___Spanks_002, Sound.Frick_spank_Light_Squish___Spanks_005, Sound.Frick_spank_Light_Squish___Spanks_006, Sound.Frick_spank_Light_Squish___Spanks_008, Sound.Frick_spank_Light_Squish___Spanks_009,
			Sound.Frick_spank_Light_Squish___Spanks_011, Sound.Frick_spank_Light_Squish___Spanks_012, Sound.Frick_spank_Light_Squish___Spanks_013, Sound.Frick_spank_Light_Squish___Spanks_014, Sound.Frick_spank_Light_Squish___Spanks_015, Sound.Frick_spank_Light_Squish___Spanks_016, Sound.Frick_spank_Light_Squish___Spanks_028, Sound.Frick_spank_Light_Squish___Spanks_029, Sound.Frick_spank_Light_Squish___Spanks_031, Sound.Frick_spank_Normal_Squish___Spanks_003,
			Sound.Frick_spank_Normal_Squish___Spanks_004, Sound.Frick_spank_Normal_Squish___Spanks_007, Sound.Frick_spank_Normal_Squish___Spanks_010, Sound.Frick_spank_Normal_Squish___Spanks_017, Sound.Frick_spank_Normal_Squish___Spanks_018, Sound.Frick_spank_Normal_Squish___Spanks_019, Sound.Frick_spank_Normal_Squish___Spanks_020, Sound.Frick_spank_Normal_Squish___Spanks_021, Sound.Frick_spank_Normal_Squish___Spanks_022, Sound.Frick_spank_Normal_Squish___Spanks_024,
			Sound.Frick_spank_Normal_Squish___Spanks_025, Sound.Frick_spank_Normal_Squish___Spanks_027, Sound.Frick_spank_Normal_Squish___Spanks_032, Sound.Frick_unzip_unzip, Sound.Generic_Whoosh_Organic_Whoosh_1, Sound.Generic_Whoosh_Organic_Whoosh_2, Sound.Generic_Whoosh_Organic_Whoosh_3, Sound.Generic_Whoosh_Organic_Whoosh_4, Sound.Money_Coins_Handled_01, Sound.Money_Coins_Handled_02,
			Sound.Money_Coins_Handled_03, Sound.Money_Coins_Shake_Hand, Sound.Money_Coins_Sift_Through_01, Sound.Money_Coins_Sift_Through_02, Sound.Money_Coins_Sift_Through_03, Sound.Outside_Open_shop, Sound.Outside_Shop_bell, Sound.PC_pc_continue, Sound.PC_pc_end_1, Sound.PC_pc_start,
			Sound.Room_Clock_tick, Sound.Room_Cloth, Sound.Room_door_close, Sound.Room_Door_knocking, Sound.Room_door_open, Sound.Room_Hangup, Sound.Room_Plug_in, Sound.Room_Ringtone, Sound.Room_switch1, Sound.Room_switch2,
			Sound.Room_Vibration, Sound.Shower_Bubble_Bubbles_1, Sound.Shower_runShower, Sound.Spook_Spook1, Sound.Story_cuffs, Sound.Story_Dumpster_hit, Sound.Story_Ear_ring, Sound.Story_Explosion, Sound.Story_Explosion2, Sound.Story_falling_concrete,
			Sound.Story_Gunshot, Sound.Story_Melissafalls, Sound.Story_Opening_a_car_door, Sound.Story_pee, Sound.Story_Police, Sound.Story_pop, Sound.Story_slam, Sound.Story_snort, Sound.Story_Splat, Sound.Story_Steps,
			Sound.Story_stepsDAK, Sound.Story_tape_load, Sound.Story_tazer, Sound.Story_Trash_Open, Sound.Story_window_break
		};
	}

	public void RegisterLua()
	{
		UserData.RegisterType<SoundDataGenerated>();
		UserData.RegisterType<SoundContainer>();
		UserData.RegisterType<AudioClip>();
		UserData.RegisterType<SerializableAutomaticReference<AudioClip>>();
	}
}
