using System;
using System.Collections.Generic;
using IC.AutomaticReference;
using MoonSharp.Interpreter;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicDataGenerated", menuName = "Automated/MusicDataGenerated", order = 1)]
public class MusicDataGenerated : SingletonScriptableObject<MusicDataGenerated>, ISerializationCallbackReceiver, IAutomaticReferenceContainer
{
	[Serializable]
	public class MusicGroupsContainer
	{
		public SerializableAutomaticReference<MusicGroup> CashierMusicGroup;

		public SerializableAutomaticReference<MusicGroup> OutsideGroup;

		public SerializableAutomaticReference<MusicGroup> OutsideRiotsGroup;

		public SerializableAutomaticReference<MusicGroup> OutsideWithBotGroup;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicGroup>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicGroup>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicGroup>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicGroup>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicGroup>[] All = Array.Empty<SerializableAutomaticReference<MusicGroup>>();
	}

	[Serializable]
	public class MusicContainer
	{
		public SerializableAutomaticReference<MusicWithInfo> BadEnd_A_Soft_Decay;

		public SerializableAutomaticReference<MusicWithInfo> BadEnd_First_Time_for_Everything;

		public SerializableAutomaticReference<MusicWithInfo> BadEnd_There_s_Always_Tomorrow;

		public SerializableAutomaticReference<MusicWithInfo> BadEnd_Demise;

		public SerializableAutomaticReference<MusicWithInfo> BadEnd_Futurism__featdot_KÆDO_;

		public SerializableAutomaticReference<MusicWithInfo> BadEnd_Walking_Through_a_Mine_Field;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> BadEnd_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> BadEnd_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] BadEnd_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Cooking_RAPID_COOKING_CHALLENGE;

		public SerializableAutomaticReference<MusicWithInfo> Cooking_Air_Fryer;

		public SerializableAutomaticReference<MusicWithInfo> Cooking_Pudollarsy_Poppin__Pasta;

		public SerializableAutomaticReference<MusicWithInfo> Cooking_Cream_N_Pie;

		public SerializableAutomaticReference<MusicWithInfo> Cooking_Cocktail;

		public SerializableAutomaticReference<MusicWithInfo> Cooking_Let_Him_Cook;

		public SerializableAutomaticReference<MusicWithInfo> Cooking_Protein_Shake;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Cooking_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Cooking_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Cooking_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Cuddle_Jun_s_Song;

		public SerializableAutomaticReference<MusicWithInfo> Cuddle_Take_a_deep_breath;

		public SerializableAutomaticReference<MusicWithInfo> Cuddle_If_Only;

		public SerializableAutomaticReference<MusicWithInfo> Cuddle_Steel_In_Your_Socks;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Cuddle_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Cuddle_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Cuddle_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Dates_She_s_Real_to_Me;

		public SerializableAutomaticReference<MusicWithInfo> Dates_Just_You_and_Me;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Dates_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Dates_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Dates_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Fishing_Candlelight;

		public SerializableAutomaticReference<MusicWithInfo> Fishing_Rainy_Day;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Fishing_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Fishing_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Fishing_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> MainMenu_Arabesque;

		public SerializableAutomaticReference<MusicWithInfo> MainMenu_Clair_de_lune;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> MainMenu_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> MainMenu_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] MainMenu_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Other_Juice__Unmuffled___Extended_;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Other_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Other_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Other_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Outside_Normal_Make_It_Snappy;

		public SerializableAutomaticReference<MusicWithInfo> Outside_Normal_Polluted_Sunshine;

		public SerializableAutomaticReference<MusicWithInfo> Outside_Normal_Streets__Slowed_;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Outside_Normal_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Outside_Normal_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Outside_Normal_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Outside_Riots_ISO_Lated;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Outside_Riots_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Outside_Riots_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Outside_Riots_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Outside_WithBot_Mile_High;

		public SerializableAutomaticReference<MusicWithInfo> Outside_WithBot_Simulated_Happiness;

		public SerializableAutomaticReference<MusicWithInfo> Outside_WithBot_Our_Time;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Outside_WithBot_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Outside_WithBot_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Outside_WithBot_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> PC_Exotic_Hardware;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> PC_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> PC_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] PC_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Room_Day_Faded_Fate;

		public SerializableAutomaticReference<MusicWithInfo> Room_Day_Against_All_Odds;

		public SerializableAutomaticReference<MusicWithInfo> Room_Day_Maybe_It_Ain_t_So_Bad;

		public SerializableAutomaticReference<MusicWithInfo> Room_Day_This_Feels_Nice;

		public SerializableAutomaticReference<MusicWithInfo> Room_Day_Daybreak_With_You;

		public SerializableAutomaticReference<MusicWithInfo> Room_Day_Distraction;

		public SerializableAutomaticReference<MusicWithInfo> Room_Day_Keep_Going;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Room_Day_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Room_Day_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Room_Day_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Room_Day_Late_Game_One_Wish;

		public SerializableAutomaticReference<MusicWithInfo> Room_Day_Late_Game_White_Dress;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Room_Day_Late_Game_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Room_Day_Late_Game_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Room_Day_Late_Game_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Room_Low_Sanity_God_s_Loophole;

		public SerializableAutomaticReference<MusicWithInfo> Room_Low_Sanity_I_Am_an_Orange_and_I_Need_to_be_Peeled;

		public SerializableAutomaticReference<MusicWithInfo> Room_Low_Sanity_Rainbow_Room;

		public SerializableAutomaticReference<MusicWithInfo> Room_Low_Sanity_The_Walls_are_Melting;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Room_Low_Sanity_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Room_Low_Sanity_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Room_Low_Sanity_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Valium;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Working_From_Home;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Don_t_Worry_About_Me;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Unfading_Clouds;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Lofi_In_My_Bedroom;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Focus;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Spirasomnus;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Warm_Nights;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Dull_Feelings;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Snake_Eyes__v2_;

		public SerializableAutomaticReference<MusicWithInfo> Room_Night_Reflection;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Room_Night_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Room_Night_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Room_Night_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Room_Special_Nightmare;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Room_Special_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Room_Special_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Room_Special_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Serious_Just_You_Wait;

		public SerializableAutomaticReference<MusicWithInfo> Serious_Danger_Around_the_Corner;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Serious_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Serious_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Serious_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Sex_Ass_Effect;

		public SerializableAutomaticReference<MusicWithInfo> Sex_Boner_Jam;

		public SerializableAutomaticReference<MusicWithInfo> Sex_CyberHunk_2069;

		public SerializableAutomaticReference<MusicWithInfo> Sex_Death_Grip;

		public SerializableAutomaticReference<MusicWithInfo> Sex_Up_All_Night;

		public SerializableAutomaticReference<MusicWithInfo> Sex_Yogurt_Male;

		public SerializableAutomaticReference<MusicWithInfo> Sex_Questionable;

		public SerializableAutomaticReference<MusicWithInfo> Sex_Carrot;

		public SerializableAutomaticReference<MusicWithInfo> Sex_Evox_Machine;

		public SerializableAutomaticReference<MusicWithInfo> Sex_Banger;

		public SerializableAutomaticReference<MusicWithInfo> Sex_Sex;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Sex_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Sex_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Sex_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Story_See_It_Now;

		public SerializableAutomaticReference<MusicWithInfo> Story_Thank_You;

		public SerializableAutomaticReference<MusicWithInfo> Story_Wish_Things_Were_Easier;

		public SerializableAutomaticReference<MusicWithInfo> Story_Moan_Choir;

		public SerializableAutomaticReference<MusicWithInfo> Story_Hypersexual;

		public SerializableAutomaticReference<MusicWithInfo> Story_Motoyasu;

		public SerializableAutomaticReference<MusicWithInfo> Story_Fall;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Story_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Story_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Story_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Story_Prologue_Coincidences;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Story_Prologue_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Story_Prologue_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Story_Prologue_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Themes_AnnoyingNeighbour_Juice__Muffled_;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Themes_AnnoyingNeighbour_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Themes_AnnoyingNeighbour_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Themes_AnnoyingNeighbour_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Themes_BigDaddyHurt_Night;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Themes_BigDaddyHurt_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Themes_BigDaddyHurt_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Themes_BigDaddyHurt_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Themes_Cashier_Mystery_Meat;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Themes_Cashier_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Themes_Cashier_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Themes_Cashier_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Themes_Church_Choir_Boy_Drama;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Themes_Church_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Themes_Church_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Themes_Church_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Themes_FishingJoint_Blow_A_Joint;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Themes_FishingJoint_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Themes_FishingJoint_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Themes_FishingJoint_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Themes_Lady_Annalie_Oddly_Enough;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Themes_Lady_Annalie_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Themes_Lady_Annalie_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Themes_Lady_Annalie_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Themes_Landlord_It_s_Him_Again;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Themes_Landlord_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Themes_Landlord_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Themes_Landlord_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Themes_Police_Jazz_On_My_Face;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Themes_Police_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Themes_Police_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Themes_Police_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		public SerializableAutomaticReference<MusicWithInfo> Work_Laid_Runner;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> Work_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicWithInfo>> Work_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] Work_All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicWithInfo>[] All = Array.Empty<SerializableAutomaticReference<MusicWithInfo>>();
	}

	[Serializable]
	public class ArtistsContainer
	{
		public SerializableAutomaticReference<MusicArtist> Celestyn;

		public SerializableAutomaticReference<MusicArtist> Debussy;

		public SerializableAutomaticReference<MusicArtist> DreemChxsr;

		public SerializableAutomaticReference<MusicArtist> FlowerKidV;

		public SerializableAutomaticReference<MusicArtist> Kaedo;

		public SerializableAutomaticReference<MusicArtist> Kukidoe;

		public SerializableAutomaticReference<MusicArtist> LightM;

		public SerializableAutomaticReference<MusicArtist> Mad_Joram;

		public SerializableAutomaticReference<MusicArtist> Nitrori;

		public SerializableAutomaticReference<MusicArtist> Rad_Pechi;

		public SerializableAutomaticReference<MusicArtist> RottingSteel;

		public SerializableAutomaticReference<MusicArtist> Sheep;

		public SerializableAutomaticReference<MusicArtist> SJ68;

		public SerializableAutomaticReference<MusicArtist> SMYLexclamation;

		public SerializableAutomaticReference<MusicArtist> Toni_S;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MusicArtist>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicArtist>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MusicArtist>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicArtist>>();

		[NonSerialized]
		public SerializableAutomaticReference<MusicArtist>[] All = Array.Empty<SerializableAutomaticReference<MusicArtist>>();
	}

	[Serializable]
	public class MidiContainer
	{
		public SerializableAutomaticReference<MidiSongAsset> CELESTYN_RAPIDCOOKINGCHALLENGE;

		public SerializableAutomaticReference<MidiSongAsset> DreemChxsr_AirFryer;

		public SerializableAutomaticReference<MidiSongAsset> Kaedo_MysteryMeat;

		public SerializableAutomaticReference<MidiSongAsset> Kaedo_PPP;

		public SerializableAutomaticReference<MidiSongAsset> LightM_Carrot;

		public SerializableAutomaticReference<MidiSongAsset> LightM_MoanChoir;

		public SerializableAutomaticReference<MidiSongAsset> Nitrori_CreamNPie;

		public SerializableAutomaticReference<MidiSongAsset> Nitrori_EvoxMachine;

		public SerializableAutomaticReference<MidiSongAsset> Nitrori_Spirasomnus;

		public SerializableAutomaticReference<MidiSongAsset> RadPechi_ExoticHardware;

		public SerializableAutomaticReference<MidiSongAsset> RadPechi_Hypersexual;

		public SerializableAutomaticReference<MidiSongAsset> RadPechi_IfOnly;

		public SerializableAutomaticReference<MidiSongAsset> RadPechi_ISOLated;

		public SerializableAutomaticReference<MidiSongAsset> RadPechi_ItsHimAgain;

		public SerializableAutomaticReference<MidiSongAsset> RadPechi_ShesRealToMe;

		public SerializableAutomaticReference<MidiSongAsset> Sheep_Demise;

		public SerializableAutomaticReference<MidiSongAsset> SJ68_Cocktail;

		public SerializableAutomaticReference<MidiSongAsset> SJ68_lethimcook;

		public SerializableAutomaticReference<MidiSongAsset> SJ68_ProteinShake;

		public SerializableAutomaticReference<MidiSongAsset> ToniS_Reflection;

		public SerializableAutomaticReference<MidiSongAsset> ToniS_WhiteDress;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<MidiSongAsset>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MidiSongAsset>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<MidiSongAsset>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MidiSongAsset>>();

		[NonSerialized]
		public SerializableAutomaticReference<MidiSongAsset>[] All = Array.Empty<SerializableAutomaticReference<MidiSongAsset>>();
	}

	public MusicGroupsContainer MusicGroups;

	public MusicContainer Music;

	public ArtistsContainer Artists;

	public MidiContainer Midi;

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		MusicGroups.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicGroup>>
		{
			["CashierMusicGroup"] = MusicGroups.CashierMusicGroup,
			["OutsideGroup"] = MusicGroups.OutsideGroup,
			["OutsideRiotsGroup"] = MusicGroups.OutsideRiotsGroup,
			["OutsideWithBotGroup"] = MusicGroups.OutsideWithBotGroup
		};
		MusicGroups.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicGroup>>
		{
			[("CashierMusicGroup", "CashierMusicGroup")] = MusicGroups.CashierMusicGroup,
			[("OutsideGroup", "OutsideGroup")] = MusicGroups.OutsideGroup,
			[("OutsideRiotsGroup", "OutsideRiotsGroup")] = MusicGroups.OutsideRiotsGroup,
			[("OutsideWithBotGroup", "OutsideWithBotGroup")] = MusicGroups.OutsideWithBotGroup
		};
		MusicGroups.All = new SerializableAutomaticReference<MusicGroup>[4] { MusicGroups.CashierMusicGroup, MusicGroups.OutsideGroup, MusicGroups.OutsideRiotsGroup, MusicGroups.OutsideWithBotGroup };
		Music.BadEnd_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["KÆDO - A Soft Decay"] = Music.BadEnd_A_Soft_Decay,
			["KÆDO - First Time for Everything"] = Music.BadEnd_First_Time_for_Everything,
			["KÆDO - There's Always Tomorrow"] = Music.BadEnd_There_s_Always_Tomorrow,
			["Sheep - Demise"] = Music.BadEnd_Demise,
			["Tony S - Futurism (feat. KAEDO)"] = Music.BadEnd_Futurism__featdot_KÆDO_,
			["Walking-Through-a-Mine-Field"] = Music.BadEnd_Walking_Through_a_Mine_Field
		};
		Music.BadEnd_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("KÆDO - A Soft Decay", "KÆDO - A Soft Decay")] = Music.BadEnd_A_Soft_Decay,
			[("KÆDO - First Time for Everything", "KÆDO - First Time for Everything")] = Music.BadEnd_First_Time_for_Everything,
			[("KÆDO - There's Always Tomorrow", "KÆDO - There's Always Tomorrow")] = Music.BadEnd_There_s_Always_Tomorrow,
			[("Sheep - Demise", "Sheep - Demise")] = Music.BadEnd_Demise,
			[("Tony S - Futurism (feat. KAEDO)", "Tony S - Futurism (feat. KAEDO)")] = Music.BadEnd_Futurism__featdot_KÆDO_,
			[("Walking-Through-a-Mine-Field", "Walking-Through-a-Mine-Field")] = Music.BadEnd_Walking_Through_a_Mine_Field
		};
		Music.BadEnd_All = new SerializableAutomaticReference<MusicWithInfo>[6] { Music.BadEnd_A_Soft_Decay, Music.BadEnd_First_Time_for_Everything, Music.BadEnd_There_s_Always_Tomorrow, Music.BadEnd_Demise, Music.BadEnd_Futurism__featdot_KÆDO_, Music.BadEnd_Walking_Through_a_Mine_Field };
		Music.Cooking_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["CELESTYN - RAPID COOKING CHALLENGE"] = Music.Cooking_RAPID_COOKING_CHALLENGE,
			["DreemChxsr - Air Fryer"] = Music.Cooking_Air_Fryer,
			["KAEDO - Pu$sy Poppin_ Pasta"] = Music.Cooking_Pudollarsy_Poppin__Pasta,
			["Nitrori - Cream N Pie"] = Music.Cooking_Cream_N_Pie,
			["SJ68 - Cocktail"] = Music.Cooking_Cocktail,
			["SJ68 - let him cook"] = Music.Cooking_Let_Him_Cook,
			["SJ68 - Protein Shake"] = Music.Cooking_Protein_Shake
		};
		Music.Cooking_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("CELESTYN - RAPID COOKING CHALLENGE", "CELESTYN - RAPID COOKING CHALLENGE")] = Music.Cooking_RAPID_COOKING_CHALLENGE,
			[("DreemChxsr - Air Fryer", "DreemChxsr - Air Fryer")] = Music.Cooking_Air_Fryer,
			[("KAEDO - Pu$sy Poppin_ Pasta", "KAEDO - Pu$sy Poppin_ Pasta")] = Music.Cooking_Pudollarsy_Poppin__Pasta,
			[("Nitrori - Cream N Pie", "Nitrori - Cream N Pie")] = Music.Cooking_Cream_N_Pie,
			[("SJ68 - Cocktail", "SJ68 - Cocktail")] = Music.Cooking_Cocktail,
			[("SJ68 - let him cook", "SJ68 - let him cook")] = Music.Cooking_Let_Him_Cook,
			[("SJ68 - Protein Shake", "SJ68 - Protein Shake")] = Music.Cooking_Protein_Shake
		};
		Music.Cooking_All = new SerializableAutomaticReference<MusicWithInfo>[7] { Music.Cooking_RAPID_COOKING_CHALLENGE, Music.Cooking_Air_Fryer, Music.Cooking_Pudollarsy_Poppin__Pasta, Music.Cooking_Cream_N_Pie, Music.Cooking_Cocktail, Music.Cooking_Let_Him_Cook, Music.Cooking_Protein_Shake };
		Music.Cuddle_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["KÆDO - Jun_s Song"] = Music.Cuddle_Jun_s_Song,
			["LightM - Take a deep breath"] = Music.Cuddle_Take_a_deep_breath,
			["Rad Pechi - If Only"] = Music.Cuddle_If_Only,
			["RottingSteel - Steel-In Your Socks"] = Music.Cuddle_Steel_In_Your_Socks
		};
		Music.Cuddle_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("KÆDO - Jun_s Song", "KÆDO - Jun_s Song")] = Music.Cuddle_Jun_s_Song,
			[("LightM - Take a deep breath", "LightM - Take a deep breath")] = Music.Cuddle_Take_a_deep_breath,
			[("Rad Pechi - If Only", "Rad Pechi - If Only")] = Music.Cuddle_If_Only,
			[("RottingSteel - Steel-In Your Socks", "RottingSteel - Steel-In Your Socks")] = Music.Cuddle_Steel_In_Your_Socks
		};
		Music.Cuddle_All = new SerializableAutomaticReference<MusicWithInfo>[4] { Music.Cuddle_Jun_s_Song, Music.Cuddle_Take_a_deep_breath, Music.Cuddle_If_Only, Music.Cuddle_Steel_In_Your_Socks };
		Music.Dates_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["Rad Pechi - She_s Real to Me"] = Music.Dates_She_s_Real_to_Me,
			["SJ68 - Just You and Me"] = Music.Dates_Just_You_and_Me
		};
		Music.Dates_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("Rad Pechi - She_s Real to Me", "Rad Pechi - She_s Real to Me")] = Music.Dates_She_s_Real_to_Me,
			[("SJ68 - Just You and Me", "SJ68 - Just You and Me")] = Music.Dates_Just_You_and_Me
		};
		Music.Dates_All = new SerializableAutomaticReference<MusicWithInfo>[2] { Music.Dates_She_s_Real_to_Me, Music.Dates_Just_You_and_Me };
		Music.Fishing_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["KÆDO - Candlelight"] = Music.Fishing_Candlelight,
			["KÆDO - Rainy Day"] = Music.Fishing_Rainy_Day
		};
		Music.Fishing_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("KÆDO - Candlelight", "KÆDO - Candlelight")] = Music.Fishing_Candlelight,
			[("KÆDO - Rainy Day", "KÆDO - Rainy Day")] = Music.Fishing_Rainy_Day
		};
		Music.Fishing_All = new SerializableAutomaticReference<MusicWithInfo>[2] { Music.Fishing_Candlelight, Music.Fishing_Rainy_Day };
		Music.MainMenu_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["Arabesque MIDI Reworked"] = Music.MainMenu_Arabesque,
			["Clair de lune"] = Music.MainMenu_Clair_de_lune
		};
		Music.MainMenu_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("Arabesque MIDI Reworked", "Arabesque MIDI Reworked")] = Music.MainMenu_Arabesque,
			[("Clair de lune", "Clair de lune")] = Music.MainMenu_Clair_de_lune
		};
		Music.MainMenu_All = new SerializableAutomaticReference<MusicWithInfo>[2] { Music.MainMenu_Arabesque, Music.MainMenu_Clair_de_lune };
		Music.Other_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["SJ68 & Kukidoe - Juice (Unmuffled) (Extended)"] = Music.Other_Juice__Unmuffled___Extended_ };
		Music.Other_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("SJ68 & Kukidoe - Juice (Unmuffled) (Extended)", "SJ68 & Kukidoe - Juice (Unmuffled) (Extended)")] = Music.Other_Juice__Unmuffled___Extended_ };
		Music.Other_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Other_Juice__Unmuffled___Extended_ };
		Music.Outside_Normal_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["KÆDO - Make It Snappy"] = Music.Outside_Normal_Make_It_Snappy,
			["SJ68 - Polluted Sunshine"] = Music.Outside_Normal_Polluted_Sunshine,
			["Toni S - Streets (Slowed)"] = Music.Outside_Normal_Streets__Slowed_
		};
		Music.Outside_Normal_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("KÆDO - Make It Snappy", "KÆDO - Make It Snappy")] = Music.Outside_Normal_Make_It_Snappy,
			[("SJ68 - Polluted Sunshine", "SJ68 - Polluted Sunshine")] = Music.Outside_Normal_Polluted_Sunshine,
			[("Toni S - Streets (Slowed)", "Toni S - Streets (Slowed)")] = Music.Outside_Normal_Streets__Slowed_
		};
		Music.Outside_Normal_All = new SerializableAutomaticReference<MusicWithInfo>[3] { Music.Outside_Normal_Make_It_Snappy, Music.Outside_Normal_Polluted_Sunshine, Music.Outside_Normal_Streets__Slowed_ };
		Music.Outside_Riots_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["Rad Pechi - ISO Lated"] = Music.Outside_Riots_ISO_Lated };
		Music.Outside_Riots_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("Rad Pechi - ISO Lated", "Rad Pechi - ISO Lated")] = Music.Outside_Riots_ISO_Lated };
		Music.Outside_Riots_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Outside_Riots_ISO_Lated };
		Music.Outside_WithBot_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["KÆDO - Mile High"] = Music.Outside_WithBot_Mile_High,
			["KÆDO - Simulated Happiness"] = Music.Outside_WithBot_Simulated_Happiness,
			["Mad Joram - Our Time"] = Music.Outside_WithBot_Our_Time
		};
		Music.Outside_WithBot_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("KÆDO - Mile High", "KÆDO - Mile High")] = Music.Outside_WithBot_Mile_High,
			[("KÆDO - Simulated Happiness", "KÆDO - Simulated Happiness")] = Music.Outside_WithBot_Simulated_Happiness,
			[("Mad Joram - Our Time", "Mad Joram - Our Time")] = Music.Outside_WithBot_Our_Time
		};
		Music.Outside_WithBot_All = new SerializableAutomaticReference<MusicWithInfo>[3] { Music.Outside_WithBot_Mile_High, Music.Outside_WithBot_Simulated_Happiness, Music.Outside_WithBot_Our_Time };
		Music.PC_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["Rad Pechi - Exotic Hardware"] = Music.PC_Exotic_Hardware };
		Music.PC_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("Rad Pechi - Exotic Hardware", "Rad Pechi - Exotic Hardware")] = Music.PC_Exotic_Hardware };
		Music.PC_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.PC_Exotic_Hardware };
		Music.Room_Day_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["DreemChxsr - Faded Fate"] = Music.Room_Day_Faded_Fate,
			["KÆDO - Against All Odds"] = Music.Room_Day_Against_All_Odds,
			["KÆDO - Maybe It Ain_t So Bad"] = Music.Room_Day_Maybe_It_Ain_t_So_Bad,
			["KÆDO - This Feels Nice"] = Music.Room_Day_This_Feels_Nice,
			["Light M. - Daybreak With You"] = Music.Room_Day_Daybreak_With_You,
			["Light M. - Distraction"] = Music.Room_Day_Distraction,
			["SJ68 - Keep Going"] = Music.Room_Day_Keep_Going
		};
		Music.Room_Day_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("DreemChxsr - Faded Fate", "DreemChxsr - Faded Fate")] = Music.Room_Day_Faded_Fate,
			[("KÆDO - Against All Odds", "KÆDO - Against All Odds")] = Music.Room_Day_Against_All_Odds,
			[("KÆDO - Maybe It Ain_t So Bad", "KÆDO - Maybe It Ain_t So Bad")] = Music.Room_Day_Maybe_It_Ain_t_So_Bad,
			[("KÆDO - This Feels Nice", "KÆDO - This Feels Nice")] = Music.Room_Day_This_Feels_Nice,
			[("Light M. - Daybreak With You", "Light M. - Daybreak With You")] = Music.Room_Day_Daybreak_With_You,
			[("Light M. - Distraction", "Light M. - Distraction")] = Music.Room_Day_Distraction,
			[("SJ68 - Keep Going", "SJ68 - Keep Going")] = Music.Room_Day_Keep_Going
		};
		Music.Room_Day_All = new SerializableAutomaticReference<MusicWithInfo>[7] { Music.Room_Day_Faded_Fate, Music.Room_Day_Against_All_Odds, Music.Room_Day_Maybe_It_Ain_t_So_Bad, Music.Room_Day_This_Feels_Nice, Music.Room_Day_Daybreak_With_You, Music.Room_Day_Distraction, Music.Room_Day_Keep_Going };
		Music.Room_Day_Late_Game_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["DreemChxsr - One Wish"] = Music.Room_Day_Late_Game_One_Wish,
			["Toni S - White Dress"] = Music.Room_Day_Late_Game_White_Dress
		};
		Music.Room_Day_Late_Game_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("DreemChxsr - One Wish", "DreemChxsr - One Wish")] = Music.Room_Day_Late_Game_One_Wish,
			[("Toni S - White Dress", "Toni S - White Dress")] = Music.Room_Day_Late_Game_White_Dress
		};
		Music.Room_Day_Late_Game_All = new SerializableAutomaticReference<MusicWithInfo>[2] { Music.Room_Day_Late_Game_One_Wish, Music.Room_Day_Late_Game_White_Dress };
		Music.Room_Low_Sanity_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["KÆDO - God's Loophole"] = Music.Room_Low_Sanity_God_s_Loophole,
			["KÆDO - I Am an Orange and I Need to be Peeled"] = Music.Room_Low_Sanity_I_Am_an_Orange_and_I_Need_to_be_Peeled,
			["KÆDO - Rainbow Room"] = Music.Room_Low_Sanity_Rainbow_Room,
			["KÆDO - The Walls are Melting"] = Music.Room_Low_Sanity_The_Walls_are_Melting
		};
		Music.Room_Low_Sanity_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("KÆDO - God's Loophole", "KÆDO - God's Loophole")] = Music.Room_Low_Sanity_God_s_Loophole,
			[("KÆDO - I Am an Orange and I Need to be Peeled", "KÆDO - I Am an Orange and I Need to be Peeled")] = Music.Room_Low_Sanity_I_Am_an_Orange_and_I_Need_to_be_Peeled,
			[("KÆDO - Rainbow Room", "KÆDO - Rainbow Room")] = Music.Room_Low_Sanity_Rainbow_Room,
			[("KÆDO - The Walls are Melting", "KÆDO - The Walls are Melting")] = Music.Room_Low_Sanity_The_Walls_are_Melting
		};
		Music.Room_Low_Sanity_All = new SerializableAutomaticReference<MusicWithInfo>[4] { Music.Room_Low_Sanity_God_s_Loophole, Music.Room_Low_Sanity_I_Am_an_Orange_and_I_Need_to_be_Peeled, Music.Room_Low_Sanity_Rainbow_Room, Music.Room_Low_Sanity_The_Walls_are_Melting };
		Music.Room_Night_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["Dj-Anton-Valium"] = Music.Room_Night_Valium,
			["Kukidoe - Working From Home"] = Music.Room_Night_Working_From_Home,
			["KÆDO - Don't Worry About Me"] = Music.Room_Night_Don_t_Worry_About_Me,
			["Light_M - Unfading Clouds"] = Music.Room_Night_Unfading_Clouds,
			["Lofi In My Bedroom"] = Music.Room_Night_Lofi_In_My_Bedroom,
			["Mad Joram - Focus"] = Music.Room_Night_Focus,
			["Nitrori - Spirasomnus"] = Music.Room_Night_Spirasomnus,
			["RottingSteel__Warm-Nights"] = Music.Room_Night_Warm_Nights,
			["SJ68 - Dull Feelings"] = Music.Room_Night_Dull_Feelings,
			["SMYL! - Snake Eyes (v2)"] = Music.Room_Night_Snake_Eyes__v2_,
			["Toni S - Reflection"] = Music.Room_Night_Reflection
		};
		Music.Room_Night_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("Dj-Anton-Valium", "Dj-Anton-Valium")] = Music.Room_Night_Valium,
			[("Kukidoe - Working From Home", "Kukidoe - Working From Home")] = Music.Room_Night_Working_From_Home,
			[("KÆDO - Don't Worry About Me", "KÆDO - Don't Worry About Me")] = Music.Room_Night_Don_t_Worry_About_Me,
			[("Light_M - Unfading Clouds", "Light_M - Unfading Clouds")] = Music.Room_Night_Unfading_Clouds,
			[("Lofi In My Bedroom", "Lofi In My Bedroom")] = Music.Room_Night_Lofi_In_My_Bedroom,
			[("Mad Joram - Focus", "Mad Joram - Focus")] = Music.Room_Night_Focus,
			[("Nitrori - Spirasomnus", "Nitrori - Spirasomnus")] = Music.Room_Night_Spirasomnus,
			[("RottingSteel__Warm-Nights", "RottingSteel__Warm-Nights")] = Music.Room_Night_Warm_Nights,
			[("SJ68 - Dull Feelings", "SJ68 - Dull Feelings")] = Music.Room_Night_Dull_Feelings,
			[("SMYL! - Snake Eyes (v2)", "SMYL! - Snake Eyes (v2)")] = Music.Room_Night_Snake_Eyes__v2_,
			[("Toni S - Reflection", "Toni S - Reflection")] = Music.Room_Night_Reflection
		};
		Music.Room_Night_All = new SerializableAutomaticReference<MusicWithInfo>[11]
		{
			Music.Room_Night_Valium, Music.Room_Night_Working_From_Home, Music.Room_Night_Don_t_Worry_About_Me, Music.Room_Night_Unfading_Clouds, Music.Room_Night_Lofi_In_My_Bedroom, Music.Room_Night_Focus, Music.Room_Night_Spirasomnus, Music.Room_Night_Warm_Nights, Music.Room_Night_Dull_Feelings, Music.Room_Night_Snake_Eyes__v2_,
			Music.Room_Night_Reflection
		};
		Music.Room_Special_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["Toni S - Nightmare"] = Music.Room_Special_Nightmare };
		Music.Room_Special_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("Toni S - Nightmare", "Toni S - Nightmare")] = Music.Room_Special_Nightmare };
		Music.Room_Special_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Room_Special_Nightmare };
		Music.Serious_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["KÆDO - Just You Wait"] = Music.Serious_Just_You_Wait,
			["RottingSteel - Dagner Around the Corner"] = Music.Serious_Danger_Around_the_Corner
		};
		Music.Serious_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("KÆDO - Just You Wait", "KÆDO - Just You Wait")] = Music.Serious_Just_You_Wait,
			[("RottingSteel - Dagner Around the Corner", "RottingSteel - Dagner Around the Corner")] = Music.Serious_Danger_Around_the_Corner
		};
		Music.Serious_All = new SerializableAutomaticReference<MusicWithInfo>[2] { Music.Serious_Just_You_Wait, Music.Serious_Danger_Around_the_Corner };
		Music.Sex_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["KÆDO - Ass Effect"] = Music.Sex_Ass_Effect,
			["KÆDO - Boner Jam (WIP)"] = Music.Sex_Boner_Jam,
			["KÆDO - CyberHunk 2069"] = Music.Sex_CyberHunk_2069,
			["KÆDO - Death Grip"] = Music.Sex_Death_Grip,
			["KÆDO - Up All Night (now extended 24 brs)"] = Music.Sex_Up_All_Night,
			["KÆDO - Yogurt Male"] = Music.Sex_Yogurt_Male,
			["Light M - Questionable"] = Music.Sex_Questionable,
			["LightM - Carrot"] = Music.Sex_Carrot,
			["Nitrori - Evox Machine"] = Music.Sex_Evox_Machine,
			["SJ68 - Banger"] = Music.Sex_Banger,
			["SMYL! - Sex"] = Music.Sex_Sex
		};
		Music.Sex_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("KÆDO - Ass Effect", "KÆDO - Ass Effect")] = Music.Sex_Ass_Effect,
			[("KÆDO - Boner Jam (WIP)", "KÆDO - Boner Jam (WIP)")] = Music.Sex_Boner_Jam,
			[("KÆDO - CyberHunk 2069", "KÆDO - CyberHunk 2069")] = Music.Sex_CyberHunk_2069,
			[("KÆDO - Death Grip", "KÆDO - Death Grip")] = Music.Sex_Death_Grip,
			[("KÆDO - Up All Night (now extended 24 brs)", "KÆDO - Up All Night (now extended 24 brs)")] = Music.Sex_Up_All_Night,
			[("KÆDO - Yogurt Male", "KÆDO - Yogurt Male")] = Music.Sex_Yogurt_Male,
			[("Light M - Questionable", "Light M - Questionable")] = Music.Sex_Questionable,
			[("LightM - Carrot", "LightM - Carrot")] = Music.Sex_Carrot,
			[("Nitrori - Evox Machine", "Nitrori - Evox Machine")] = Music.Sex_Evox_Machine,
			[("SJ68 - Banger", "SJ68 - Banger")] = Music.Sex_Banger,
			[("SMYL! - Sex", "SMYL! - Sex")] = Music.Sex_Sex
		};
		Music.Sex_All = new SerializableAutomaticReference<MusicWithInfo>[11]
		{
			Music.Sex_Ass_Effect, Music.Sex_Boner_Jam, Music.Sex_CyberHunk_2069, Music.Sex_Death_Grip, Music.Sex_Up_All_Night, Music.Sex_Yogurt_Male, Music.Sex_Questionable, Music.Sex_Carrot, Music.Sex_Evox_Machine, Music.Sex_Banger,
			Music.Sex_Sex
		};
		Music.Story_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>>
		{
			["KÆDO - See It Now"] = Music.Story_See_It_Now,
			["KÆDO - Thank You"] = Music.Story_Thank_You,
			["Light M. - Wish Things Were Easier"] = Music.Story_Wish_Things_Were_Easier,
			["LightM - Moan Choir"] = Music.Story_Moan_Choir,
			["Rad Pechi - Hypersexual"] = Music.Story_Hypersexual,
			["Sheep - Motoyasu"] = Music.Story_Motoyasu,
			["Toni S-Fall"] = Music.Story_Fall
		};
		Music.Story_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>>
		{
			[("KÆDO - See It Now", "KÆDO - See It Now")] = Music.Story_See_It_Now,
			[("KÆDO - Thank You", "KÆDO - Thank You")] = Music.Story_Thank_You,
			[("Light M. - Wish Things Were Easier", "Light M. - Wish Things Were Easier")] = Music.Story_Wish_Things_Were_Easier,
			[("LightM - Moan Choir", "LightM - Moan Choir")] = Music.Story_Moan_Choir,
			[("Rad Pechi - Hypersexual", "Rad Pechi - Hypersexual")] = Music.Story_Hypersexual,
			[("Sheep - Motoyasu", "Sheep - Motoyasu")] = Music.Story_Motoyasu,
			[("Toni S-Fall", "Toni S-Fall")] = Music.Story_Fall
		};
		Music.Story_All = new SerializableAutomaticReference<MusicWithInfo>[7] { Music.Story_See_It_Now, Music.Story_Thank_You, Music.Story_Wish_Things_Were_Easier, Music.Story_Moan_Choir, Music.Story_Hypersexual, Music.Story_Motoyasu, Music.Story_Fall };
		Music.Story_Prologue_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["SJ68 & Kukidoe - Coincidences"] = Music.Story_Prologue_Coincidences };
		Music.Story_Prologue_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("SJ68 & Kukidoe - Coincidences", "SJ68 & Kukidoe - Coincidences")] = Music.Story_Prologue_Coincidences };
		Music.Story_Prologue_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Story_Prologue_Coincidences };
		Music.Themes_AnnoyingNeighbour_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["SJ68 & kukidoe - Juice (Muffled)"] = Music.Themes_AnnoyingNeighbour_Juice__Muffled_ };
		Music.Themes_AnnoyingNeighbour_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("SJ68 & kukidoe - Juice (Muffled)", "SJ68 & kukidoe - Juice (Muffled)")] = Music.Themes_AnnoyingNeighbour_Juice__Muffled_ };
		Music.Themes_AnnoyingNeighbour_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Themes_AnnoyingNeighbour_Juice__Muffled_ };
		Music.Themes_BigDaddyHurt_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["Toni S - Night"] = Music.Themes_BigDaddyHurt_Night };
		Music.Themes_BigDaddyHurt_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("Toni S - Night", "Toni S - Night")] = Music.Themes_BigDaddyHurt_Night };
		Music.Themes_BigDaddyHurt_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Themes_BigDaddyHurt_Night };
		Music.Themes_Cashier_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["KÆDO - Mystery Meat"] = Music.Themes_Cashier_Mystery_Meat };
		Music.Themes_Cashier_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("KÆDO - Mystery Meat", "KÆDO - Mystery Meat")] = Music.Themes_Cashier_Mystery_Meat };
		Music.Themes_Cashier_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Themes_Cashier_Mystery_Meat };
		Music.Themes_Church_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["KÆDO - Choir Boy Drama"] = Music.Themes_Church_Choir_Boy_Drama };
		Music.Themes_Church_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("KÆDO - Choir Boy Drama", "KÆDO - Choir Boy Drama")] = Music.Themes_Church_Choir_Boy_Drama };
		Music.Themes_Church_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Themes_Church_Choir_Boy_Drama };
		Music.Themes_FishingJoint_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["Toni S - Blow A Joint"] = Music.Themes_FishingJoint_Blow_A_Joint };
		Music.Themes_FishingJoint_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("Toni S - Blow A Joint", "Toni S - Blow A Joint")] = Music.Themes_FishingJoint_Blow_A_Joint };
		Music.Themes_FishingJoint_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Themes_FishingJoint_Blow_A_Joint };
		Music.Themes_Lady_Annalie_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["KÆDO - Oddly Enough"] = Music.Themes_Lady_Annalie_Oddly_Enough };
		Music.Themes_Lady_Annalie_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("KÆDO - Oddly Enough", "KÆDO - Oddly Enough")] = Music.Themes_Lady_Annalie_Oddly_Enough };
		Music.Themes_Lady_Annalie_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Themes_Lady_Annalie_Oddly_Enough };
		Music.Themes_Landlord_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["Rad Pechi - It_s Him Again"] = Music.Themes_Landlord_It_s_Him_Again };
		Music.Themes_Landlord_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("Rad Pechi - It_s Him Again", "Rad Pechi - It_s Him Again")] = Music.Themes_Landlord_It_s_Him_Again };
		Music.Themes_Landlord_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Themes_Landlord_It_s_Him_Again };
		Music.Themes_Police_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["KÆDO - Jazz On My Face"] = Music.Themes_Police_Jazz_On_My_Face };
		Music.Themes_Police_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("KÆDO - Jazz On My Face", "KÆDO - Jazz On My Face")] = Music.Themes_Police_Jazz_On_My_Face };
		Music.Themes_Police_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Themes_Police_Jazz_On_My_Face };
		Music.Work_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicWithInfo>> { ["KÆDO - Laid Runner"] = Music.Work_Laid_Runner };
		Music.Work_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicWithInfo>> { [("KÆDO - Laid Runner", "KÆDO - Laid Runner")] = Music.Work_Laid_Runner };
		Music.Work_All = new SerializableAutomaticReference<MusicWithInfo>[1] { Music.Work_Laid_Runner };
		Music.All = new SerializableAutomaticReference<MusicWithInfo>[87]
		{
			Music.BadEnd_A_Soft_Decay, Music.BadEnd_First_Time_for_Everything, Music.BadEnd_There_s_Always_Tomorrow, Music.BadEnd_Demise, Music.BadEnd_Futurism__featdot_KÆDO_, Music.BadEnd_Walking_Through_a_Mine_Field, Music.Cooking_RAPID_COOKING_CHALLENGE, Music.Cooking_Air_Fryer, Music.Cooking_Pudollarsy_Poppin__Pasta, Music.Cooking_Cream_N_Pie,
			Music.Cooking_Cocktail, Music.Cooking_Let_Him_Cook, Music.Cooking_Protein_Shake, Music.Cuddle_Jun_s_Song, Music.Cuddle_Take_a_deep_breath, Music.Cuddle_If_Only, Music.Cuddle_Steel_In_Your_Socks, Music.Dates_She_s_Real_to_Me, Music.Dates_Just_You_and_Me, Music.Fishing_Candlelight,
			Music.Fishing_Rainy_Day, Music.MainMenu_Arabesque, Music.MainMenu_Clair_de_lune, Music.Other_Juice__Unmuffled___Extended_, Music.Outside_Normal_Make_It_Snappy, Music.Outside_Normal_Polluted_Sunshine, Music.Outside_Normal_Streets__Slowed_, Music.Outside_Riots_ISO_Lated, Music.Outside_WithBot_Mile_High, Music.Outside_WithBot_Simulated_Happiness,
			Music.Outside_WithBot_Our_Time, Music.PC_Exotic_Hardware, Music.Room_Day_Faded_Fate, Music.Room_Day_Against_All_Odds, Music.Room_Day_Maybe_It_Ain_t_So_Bad, Music.Room_Day_This_Feels_Nice, Music.Room_Day_Daybreak_With_You, Music.Room_Day_Distraction, Music.Room_Day_Keep_Going, Music.Room_Day_Late_Game_One_Wish,
			Music.Room_Day_Late_Game_White_Dress, Music.Room_Low_Sanity_God_s_Loophole, Music.Room_Low_Sanity_I_Am_an_Orange_and_I_Need_to_be_Peeled, Music.Room_Low_Sanity_Rainbow_Room, Music.Room_Low_Sanity_The_Walls_are_Melting, Music.Room_Night_Valium, Music.Room_Night_Working_From_Home, Music.Room_Night_Don_t_Worry_About_Me, Music.Room_Night_Unfading_Clouds, Music.Room_Night_Lofi_In_My_Bedroom,
			Music.Room_Night_Focus, Music.Room_Night_Spirasomnus, Music.Room_Night_Warm_Nights, Music.Room_Night_Dull_Feelings, Music.Room_Night_Snake_Eyes__v2_, Music.Room_Night_Reflection, Music.Room_Special_Nightmare, Music.Serious_Just_You_Wait, Music.Serious_Danger_Around_the_Corner, Music.Sex_Ass_Effect,
			Music.Sex_Boner_Jam, Music.Sex_CyberHunk_2069, Music.Sex_Death_Grip, Music.Sex_Up_All_Night, Music.Sex_Yogurt_Male, Music.Sex_Questionable, Music.Sex_Carrot, Music.Sex_Evox_Machine, Music.Sex_Banger, Music.Sex_Sex,
			Music.Story_See_It_Now, Music.Story_Thank_You, Music.Story_Wish_Things_Were_Easier, Music.Story_Moan_Choir, Music.Story_Hypersexual, Music.Story_Motoyasu, Music.Story_Fall, Music.Story_Prologue_Coincidences, Music.Themes_AnnoyingNeighbour_Juice__Muffled_, Music.Themes_BigDaddyHurt_Night,
			Music.Themes_Cashier_Mystery_Meat, Music.Themes_Church_Choir_Boy_Drama, Music.Themes_FishingJoint_Blow_A_Joint, Music.Themes_Lady_Annalie_Oddly_Enough, Music.Themes_Landlord_It_s_Him_Again, Music.Themes_Police_Jazz_On_My_Face, Music.Work_Laid_Runner
		};
		Artists.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MusicArtist>>
		{
			["Celestyn"] = Artists.Celestyn,
			["Debussy"] = Artists.Debussy,
			["DreemChxsr"] = Artists.DreemChxsr,
			["FlowerKidV"] = Artists.FlowerKidV,
			["Kaedo"] = Artists.Kaedo,
			["Kukidoe"] = Artists.Kukidoe,
			["LightM"] = Artists.LightM,
			["Mad Joram"] = Artists.Mad_Joram,
			["Nitrori"] = Artists.Nitrori,
			["Rad Pechi"] = Artists.Rad_Pechi,
			["RottingSteel"] = Artists.RottingSteel,
			["Sheep"] = Artists.Sheep,
			["SJ68"] = Artists.SJ68,
			["SMYL!"] = Artists.SMYLexclamation,
			["Toni S"] = Artists.Toni_S
		};
		Artists.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MusicArtist>>
		{
			[("Celestyn", "Celestyn")] = Artists.Celestyn,
			[("Debussy", "Debussy")] = Artists.Debussy,
			[("DreemChxsr", "DreemChxsr")] = Artists.DreemChxsr,
			[("FlowerKidV", "FlowerKidV")] = Artists.FlowerKidV,
			[("Kaedo", "Kaedo")] = Artists.Kaedo,
			[("Kukidoe", "Kukidoe")] = Artists.Kukidoe,
			[("LightM", "LightM")] = Artists.LightM,
			[("Mad Joram", "Mad Joram")] = Artists.Mad_Joram,
			[("Nitrori", "Nitrori")] = Artists.Nitrori,
			[("Rad Pechi", "Rad Pechi")] = Artists.Rad_Pechi,
			[("RottingSteel", "RottingSteel")] = Artists.RottingSteel,
			[("Sheep", "Sheep")] = Artists.Sheep,
			[("SJ68", "SJ68")] = Artists.SJ68,
			[("SMYL!", "SMYL!")] = Artists.SMYLexclamation,
			[("Toni S", "Toni S")] = Artists.Toni_S
		};
		Artists.All = new SerializableAutomaticReference<MusicArtist>[15]
		{
			Artists.Celestyn, Artists.Debussy, Artists.DreemChxsr, Artists.FlowerKidV, Artists.Kaedo, Artists.Kukidoe, Artists.LightM, Artists.Mad_Joram, Artists.Nitrori, Artists.Rad_Pechi,
			Artists.RottingSteel, Artists.Sheep, Artists.SJ68, Artists.SMYLexclamation, Artists.Toni_S
		};
		Midi.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<MidiSongAsset>>
		{
			["CELESTYN-RAPIDCOOKINGCHALLENGE"] = Midi.CELESTYN_RAPIDCOOKINGCHALLENGE,
			["DreemChxsr-AirFryer"] = Midi.DreemChxsr_AirFryer,
			["Kaedo-MysteryMeat"] = Midi.Kaedo_MysteryMeat,
			["Kaedo-PPP"] = Midi.Kaedo_PPP,
			["LightM-Carrot"] = Midi.LightM_Carrot,
			["LightM-MoanChoir"] = Midi.LightM_MoanChoir,
			["Nitrori-CreamNPie"] = Midi.Nitrori_CreamNPie,
			["Nitrori-EvoxMachine"] = Midi.Nitrori_EvoxMachine,
			["Nitrori-Spirasomnus"] = Midi.Nitrori_Spirasomnus,
			["RadPechi-ExoticHardware"] = Midi.RadPechi_ExoticHardware,
			["RadPechi-Hypersexual"] = Midi.RadPechi_Hypersexual,
			["RadPechi-IfOnly"] = Midi.RadPechi_IfOnly,
			["RadPechi-ISOLated"] = Midi.RadPechi_ISOLated,
			["RadPechi-ItsHimAgain"] = Midi.RadPechi_ItsHimAgain,
			["RadPechi-ShesRealToMe"] = Midi.RadPechi_ShesRealToMe,
			["Sheep-Demise"] = Midi.Sheep_Demise,
			["SJ68-Cocktail"] = Midi.SJ68_Cocktail,
			["SJ68-lethimcook"] = Midi.SJ68_lethimcook,
			["SJ68-ProteinShake"] = Midi.SJ68_ProteinShake,
			["ToniS-Reflection"] = Midi.ToniS_Reflection,
			["ToniS-WhiteDress"] = Midi.ToniS_WhiteDress
		};
		Midi.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<MidiSongAsset>>
		{
			[("CELESTYN-RAPIDCOOKINGCHALLENGE", "CELESTYN-RAPIDCOOKINGCHALLENGE")] = Midi.CELESTYN_RAPIDCOOKINGCHALLENGE,
			[("DreemChxsr-AirFryer", "DreemChxsr-AirFryer")] = Midi.DreemChxsr_AirFryer,
			[("Kaedo-MysteryMeat", "Kaedo-MysteryMeat")] = Midi.Kaedo_MysteryMeat,
			[("Kaedo-PPP", "Kaedo-PPP")] = Midi.Kaedo_PPP,
			[("LightM-Carrot", "LightM-Carrot")] = Midi.LightM_Carrot,
			[("LightM-MoanChoir", "LightM-MoanChoir")] = Midi.LightM_MoanChoir,
			[("Nitrori-CreamNPie", "Nitrori-CreamNPie")] = Midi.Nitrori_CreamNPie,
			[("Nitrori-EvoxMachine", "Nitrori-EvoxMachine")] = Midi.Nitrori_EvoxMachine,
			[("Nitrori-Spirasomnus", "Nitrori-Spirasomnus")] = Midi.Nitrori_Spirasomnus,
			[("RadPechi-ExoticHardware", "RadPechi-ExoticHardware")] = Midi.RadPechi_ExoticHardware,
			[("RadPechi-Hypersexual", "RadPechi-Hypersexual")] = Midi.RadPechi_Hypersexual,
			[("RadPechi-IfOnly", "RadPechi-IfOnly")] = Midi.RadPechi_IfOnly,
			[("RadPechi-ISOLated", "RadPechi-ISOLated")] = Midi.RadPechi_ISOLated,
			[("RadPechi-ItsHimAgain", "RadPechi-ItsHimAgain")] = Midi.RadPechi_ItsHimAgain,
			[("RadPechi-ShesRealToMe", "RadPechi-ShesRealToMe")] = Midi.RadPechi_ShesRealToMe,
			[("Sheep-Demise", "Sheep-Demise")] = Midi.Sheep_Demise,
			[("SJ68-Cocktail", "SJ68-Cocktail")] = Midi.SJ68_Cocktail,
			[("SJ68-lethimcook", "SJ68-lethimcook")] = Midi.SJ68_lethimcook,
			[("SJ68-ProteinShake", "SJ68-ProteinShake")] = Midi.SJ68_ProteinShake,
			[("ToniS-Reflection", "ToniS-Reflection")] = Midi.ToniS_Reflection,
			[("ToniS-WhiteDress", "ToniS-WhiteDress")] = Midi.ToniS_WhiteDress
		};
		Midi.All = new SerializableAutomaticReference<MidiSongAsset>[21]
		{
			Midi.CELESTYN_RAPIDCOOKINGCHALLENGE, Midi.DreemChxsr_AirFryer, Midi.Kaedo_MysteryMeat, Midi.Kaedo_PPP, Midi.LightM_Carrot, Midi.LightM_MoanChoir, Midi.Nitrori_CreamNPie, Midi.Nitrori_EvoxMachine, Midi.Nitrori_Spirasomnus, Midi.RadPechi_ExoticHardware,
			Midi.RadPechi_Hypersexual, Midi.RadPechi_IfOnly, Midi.RadPechi_ISOLated, Midi.RadPechi_ItsHimAgain, Midi.RadPechi_ShesRealToMe, Midi.Sheep_Demise, Midi.SJ68_Cocktail, Midi.SJ68_lethimcook, Midi.SJ68_ProteinShake, Midi.ToniS_Reflection,
			Midi.ToniS_WhiteDress
		};
	}

	public void RegisterLua()
	{
		UserData.RegisterType<MusicDataGenerated>();
		UserData.RegisterType<MusicGroupsContainer>();
		UserData.RegisterType<MusicGroup>();
		UserData.RegisterType<SerializableAutomaticReference<MusicGroup>>();
		UserData.RegisterType<MusicContainer>();
		UserData.RegisterType<MusicWithInfo>();
		UserData.RegisterType<SerializableAutomaticReference<MusicWithInfo>>();
		UserData.RegisterType<ArtistsContainer>();
		UserData.RegisterType<MusicArtist>();
		UserData.RegisterType<SerializableAutomaticReference<MusicArtist>>();
		UserData.RegisterType<MidiContainer>();
		UserData.RegisterType<MidiSongAsset>();
		UserData.RegisterType<SerializableAutomaticReference<MidiSongAsset>>();
	}
}
