using System;
using System.Collections.Generic;
using IC.AutomaticReference;
using MoonSharp.Interpreter;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteTextureDataGenerated", menuName = "Automated/SpriteTextureDataGenerated", order = 1)]
public class SpriteTextureDataGenerated : SingletonScriptableObject<SpriteTextureDataGenerated>, ISerializationCallbackReceiver, IAutomaticReferenceContainer
{
	[Serializable]
	public class AltBackgroundsContainer
	{
		public SerializableAutomaticReference<Sprite> Bat_Banner;

		public SerializableAutomaticReference<Sprite> Ghost_Background;

		public SerializableAutomaticReference<Sprite> Pumpkin_Mat;

		public SerializableAutomaticReference<Sprite> SpiderPlushy;

		public SerializableAutomaticReference<Sprite> Blink_Halloween;

		public SerializableAutomaticReference<Sprite> Decoration_Halloween;

		public SerializableAutomaticReference<Sprite> Halloween_Cat;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	[Serializable]
	public class FishingBackgroundsContainer
	{
		public SerializableAutomaticReference<Sprite> FishingJoint_GreenField;

		public SerializableAutomaticReference<Sprite> FishingJoint_SunnyBeach;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> FishingJoint_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> FishingJoint_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] FishingJoint_All = Array.Empty<SerializableAutomaticReference<Sprite>>();

		public SerializableAutomaticReference<Sprite> microplastic_coast;

		public SerializableAutomaticReference<Sprite> polluted_lake;

		public SerializableAutomaticReference<Sprite> premium_farm;

		public SerializableAutomaticReference<Sprite> trashed_river;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> NullRelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> NullRelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	[Serializable]
	public class PartnerLogosContainer
	{
		public SerializableAutomaticReference<Sprite> Alternative_DiabeteColaPow;

		public SerializableAutomaticReference<Sprite> Alternative_DiabeteDrSugarTrans;

		public SerializableAutomaticReference<Sprite> Alternative_DiabeteSweetPotatoTrans;

		public SerializableAutomaticReference<Sprite> Alternative_DiabeteTransparent;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> Alternative_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> Alternative_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] Alternative_All = Array.Empty<SerializableAutomaticReference<Sprite>>();

		public SerializableAutomaticReference<Sprite> Cazino;

		public SerializableAutomaticReference<Sprite> Diabete;

		public SerializableAutomaticReference<Sprite> Luxe;

		public SerializableAutomaticReference<Sprite> Monizmed;

		public SerializableAutomaticReference<Sprite> RehabTech;

		public SerializableAutomaticReference<Sprite> Stilou;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> NullRelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> NullRelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	[Serializable]
	public class OtherLogosContainer
	{
		public SerializableAutomaticReference<Sprite> AGUI_Logo;

		public SerializableAutomaticReference<Sprite> Baka;

		public SerializableAutomaticReference<Sprite> Banana;

		public SerializableAutomaticReference<Sprite> Bedabots_Logo;

		public SerializableAutomaticReference<Sprite> BloodyMoon;

		public SerializableAutomaticReference<Sprite> BotLogo1;

		public SerializableAutomaticReference<Sprite> Bow;

		public SerializableAutomaticReference<Sprite> CELESTYN;

		public SerializableAutomaticReference<Sprite> CIA;

		public SerializableAutomaticReference<Sprite> CosplayHouse;

		public SerializableAutomaticReference<Sprite> DDlogo1;

		public SerializableAutomaticReference<Sprite> DogeCoin;

		public SerializableAutomaticReference<Sprite> fishFearMeLogo;

		public SerializableAutomaticReference<Sprite> Flowerkidv;

		public SerializableAutomaticReference<Sprite> Strawberry;

		public SerializableAutomaticReference<Sprite> Cherry;

		public SerializableAutomaticReference<Sprite> Peach;

		public SerializableAutomaticReference<Sprite> Avocado;

		public SerializableAutomaticReference<Sprite> galaxy;

		public SerializableAutomaticReference<Sprite> GamerTshirt;

		public SerializableAutomaticReference<Sprite> HikkeiruLogo;

		public SerializableAutomaticReference<Sprite> HotPinkGames_Logo;

		public SerializableAutomaticReference<Sprite> InHeatLogo;

		public SerializableAutomaticReference<Sprite> LightSonic;

		public SerializableAutomaticReference<Sprite> MadLogo;

		public SerializableAutomaticReference<Sprite> MILF;

		public SerializableAutomaticReference<Sprite> mirthal;

		public SerializableAutomaticReference<Sprite> Mushroom;

		public SerializableAutomaticReference<Sprite> NitroriLogo;

		public SerializableAutomaticReference<Sprite> NutekuLogo;

		public SerializableAutomaticReference<Sprite> Polandball;

		public SerializableAutomaticReference<Sprite> priestbot_tshirt;

		public SerializableAutomaticReference<Sprite> projekt_melody_nbtw_69;

		public SerializableAutomaticReference<Sprite> projekt_melody_nbtw_logo;

		public SerializableAutomaticReference<Sprite> radioactive;

		public SerializableAutomaticReference<Sprite> Rose;

		public SerializableAutomaticReference<Sprite> RottingSteelLogo;

		public SerializableAutomaticReference<Sprite> Shady_Corner_Logo;

		public SerializableAutomaticReference<Sprite> shcHoodie;

		public SerializableAutomaticReference<Sprite> shcPanties;

		public SerializableAutomaticReference<Sprite> Sheep;

		public SerializableAutomaticReference<Sprite> siluman_Alice;

		public SerializableAutomaticReference<Sprite> siluman_logo;

		public SerializableAutomaticReference<Sprite> SJ68WHITE;

		public SerializableAutomaticReference<Sprite> skull;

		public SerializableAutomaticReference<Sprite> sylphy_chibi;

		public SerializableAutomaticReference<Sprite> Temple;

		public SerializableAutomaticReference<Sprite> tonis_album;

		public SerializableAutomaticReference<Sprite> ufo;

		public SerializableAutomaticReference<Sprite> USP;

		public SerializableAutomaticReference<Sprite> WeebDesign;

		public SerializableAutomaticReference<Sprite> WithStupid;

		public SerializableAutomaticReference<Sprite> world_tamer_logo;

		public SerializableAutomaticReference<Sprite> WyldSpace;

		public SerializableAutomaticReference<Sprite> XoulionLogo;

		public SerializableAutomaticReference<Sprite> yaranaika;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	[Serializable]
	public class IconsContainer
	{
		public SerializableAutomaticReference<Sprite> arm;

		public SerializableAutomaticReference<Sprite> arm_alt;

		public SerializableAutomaticReference<Sprite> btn_icon_arrow_next_0;

		public SerializableAutomaticReference<Sprite> btn_icon_arrow_prev_0;

		public SerializableAutomaticReference<Sprite> back;

		public SerializableAutomaticReference<Sprite> bait;

		public SerializableAutomaticReference<Sprite> btn_icon_book_0;

		public SerializableAutomaticReference<Sprite> btn_icon_book_open_1;

		public SerializableAutomaticReference<Sprite> btn_icon_book_open_0;

		public SerializableAutomaticReference<Sprite> btn_icon_book_open_2;

		public SerializableAutomaticReference<Sprite> bra;

		public SerializableAutomaticReference<Sprite> cat_ears;

		public SerializableAutomaticReference<Sprite> chip;

		public SerializableAutomaticReference<Sprite> close_round;

		public SerializableAutomaticReference<Sprite> clothes;

		public SerializableAutomaticReference<Sprite> dish_1;

		public SerializableAutomaticReference<Sprite> dish_2;

		public SerializableAutomaticReference<Sprite> dish_3;

		public SerializableAutomaticReference<Sprite> dress;

		public SerializableAutomaticReference<Sprite> fish;

		public SerializableAutomaticReference<Sprite> fishing_rod;

		public SerializableAutomaticReference<Sprite> food;

		public SerializableAutomaticReference<Sprite> gift;

		public SerializableAutomaticReference<Sprite> glasses;

		public SerializableAutomaticReference<Sprite> halloween;

		public SerializableAutomaticReference<Sprite> heart;

		public SerializableAutomaticReference<Sprite> hoodie;

		public SerializableAutomaticReference<Sprite> Icon_check;

		public SerializableAutomaticReference<Sprite> Icon_close;

		public SerializableAutomaticReference<Sprite> Icon_dick;

		public SerializableAutomaticReference<Sprite> Icon_dropdown;

		public SerializableAutomaticReference<Sprite> Icon_energy;

		public SerializableAutomaticReference<Sprite> Icon_follow;

		public SerializableAutomaticReference<Sprite> Icon_hand;

		public SerializableAutomaticReference<Sprite> Icon_noview;

		public SerializableAutomaticReference<Sprite> Icon_psyche;

		public SerializableAutomaticReference<Sprite> Icon_Soap;

		public SerializableAutomaticReference<Sprite> Icon_Sponge;

		public SerializableAutomaticReference<Sprite> Icon_subs;

		public SerializableAutomaticReference<Sprite> Icon_tongue;

		public SerializableAutomaticReference<Sprite> Icon_trash;

		public SerializableAutomaticReference<Sprite> Icon_Trophy_0;

		public SerializableAutomaticReference<Sprite> Icon_Trophy_1;

		public SerializableAutomaticReference<Sprite> Icon_view;

		public SerializableAutomaticReference<Sprite> ingredient;

		public SerializableAutomaticReference<Sprite> leg;

		public SerializableAutomaticReference<Sprite> leg_alt;

		public SerializableAutomaticReference<Sprite> Lock_Locked_0;

		public SerializableAutomaticReference<Sprite> Lock_Unlocked;

		public SerializableAutomaticReference<Sprite> log;

		public SerializableAutomaticReference<Sprite> Logo_bluesky;

		public SerializableAutomaticReference<Sprite> Logo_discord;

		public SerializableAutomaticReference<Sprite> Logo_itch;

		public SerializableAutomaticReference<Sprite> Logo_pat;

		public SerializableAutomaticReference<Sprite> Logo_substar;

		public SerializableAutomaticReference<Sprite> Logo_tw;

		public SerializableAutomaticReference<Sprite> medicine;

		public SerializableAutomaticReference<Sprite> necklace;

		public SerializableAutomaticReference<Sprite> pants;

		public SerializableAutomaticReference<Sprite> parts;

		public SerializableAutomaticReference<Sprite> btn_icon_refresh_0;

		public SerializableAutomaticReference<Sprite> save;

		public SerializableAutomaticReference<Sprite> shirt;

		public SerializableAutomaticReference<Sprite> shoes;

		public SerializableAutomaticReference<Sprite> skip;

		public SerializableAutomaticReference<Sprite> skirt;

		public SerializableAutomaticReference<Sprite> socks;

		public SerializableAutomaticReference<Sprite> special;

		public SerializableAutomaticReference<Sprite> stuff;

		public SerializableAutomaticReference<Sprite> tail;

		public SerializableAutomaticReference<Sprite> thong;

		public SerializableAutomaticReference<Sprite> btn_icon_undo_0;

		public SerializableAutomaticReference<Sprite> vote_downvote;

		public SerializableAutomaticReference<Sprite> vote_upvote;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	[Serializable]
	public class CardsMainContainer
	{
		public SerializableAutomaticReference<Sprite> CardBack;

		public SerializableAutomaticReference<Sprite> CardShadow;

		public SerializableAutomaticReference<Sprite> Clovers_2_white;

		public SerializableAutomaticReference<Sprite> Clovers_3_white;

		public SerializableAutomaticReference<Sprite> Clovers_4_white;

		public SerializableAutomaticReference<Sprite> Clovers_5_white;

		public SerializableAutomaticReference<Sprite> Clovers_6_white;

		public SerializableAutomaticReference<Sprite> Clovers_7_white;

		public SerializableAutomaticReference<Sprite> Clovers_8_white;

		public SerializableAutomaticReference<Sprite> Clovers_9_white;

		public SerializableAutomaticReference<Sprite> Clovers_10_white;

		public SerializableAutomaticReference<Sprite> Clovers_A_white;

		public SerializableAutomaticReference<Sprite> Clovers_Jack_white;

		public SerializableAutomaticReference<Sprite> Clovers_King_white;

		public SerializableAutomaticReference<Sprite> Clovers_Queen_white;

		public SerializableAutomaticReference<Sprite> Hearts_2_white;

		public SerializableAutomaticReference<Sprite> Hearts_3_white;

		public SerializableAutomaticReference<Sprite> Hearts_4_white;

		public SerializableAutomaticReference<Sprite> Hearts_5_white;

		public SerializableAutomaticReference<Sprite> Hearts_6_white;

		public SerializableAutomaticReference<Sprite> Hearts_7_white;

		public SerializableAutomaticReference<Sprite> Hearts_8_white;

		public SerializableAutomaticReference<Sprite> Hearts_9_white;

		public SerializableAutomaticReference<Sprite> Hearts_10_white;

		public SerializableAutomaticReference<Sprite> Hearts_A_white;

		public SerializableAutomaticReference<Sprite> Hearts_Jack_white;

		public SerializableAutomaticReference<Sprite> Hearts_King_white;

		public SerializableAutomaticReference<Sprite> Hearts_Queen_white;

		public SerializableAutomaticReference<Sprite> Pikes_2_white;

		public SerializableAutomaticReference<Sprite> Pikes_3_white;

		public SerializableAutomaticReference<Sprite> Pikes_4_white;

		public SerializableAutomaticReference<Sprite> Pikes_5_white;

		public SerializableAutomaticReference<Sprite> Pikes_6_white;

		public SerializableAutomaticReference<Sprite> Pikes_7_white;

		public SerializableAutomaticReference<Sprite> Pikes_8_white;

		public SerializableAutomaticReference<Sprite> Pikes_9_white;

		public SerializableAutomaticReference<Sprite> Pikes_10_white;

		public SerializableAutomaticReference<Sprite> Pikes_A_white;

		public SerializableAutomaticReference<Sprite> Pikes_Jack_white;

		public SerializableAutomaticReference<Sprite> Pikes_King_white;

		public SerializableAutomaticReference<Sprite> Pikes_Queen_white;

		public SerializableAutomaticReference<Sprite> Tiles_2_white;

		public SerializableAutomaticReference<Sprite> Tiles_3_white;

		public SerializableAutomaticReference<Sprite> Tiles_4_white;

		public SerializableAutomaticReference<Sprite> Tiles_5_white;

		public SerializableAutomaticReference<Sprite> Tiles_6_white;

		public SerializableAutomaticReference<Sprite> Tiles_7_white;

		public SerializableAutomaticReference<Sprite> Tiles_8_white;

		public SerializableAutomaticReference<Sprite> Tiles_9_white;

		public SerializableAutomaticReference<Sprite> Tiles_10_white;

		public SerializableAutomaticReference<Sprite> Tiles_A_white;

		public SerializableAutomaticReference<Sprite> Tiles_Jack_white;

		public SerializableAutomaticReference<Sprite> Tiles_King_white;

		public SerializableAutomaticReference<Sprite> Tiles_Queen_white;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	[Serializable]
	public class CardsCasinoContainer
	{
		public SerializableAutomaticReference<Sprite> _50;

		public SerializableAutomaticReference<Sprite> _51;

		public SerializableAutomaticReference<Sprite> _32;

		public SerializableAutomaticReference<Sprite> _43;

		public SerializableAutomaticReference<Sprite> _33;

		public SerializableAutomaticReference<Sprite> _42;

		public SerializableAutomaticReference<Sprite> _30;

		public SerializableAutomaticReference<Sprite> _41;

		public SerializableAutomaticReference<Sprite> _31;

		public SerializableAutomaticReference<Sprite> _25;

		public SerializableAutomaticReference<Sprite> _40;

		public SerializableAutomaticReference<Sprite> _36;

		public SerializableAutomaticReference<Sprite> _24;

		public SerializableAutomaticReference<Sprite> _47;

		public SerializableAutomaticReference<Sprite> _37;

		public SerializableAutomaticReference<Sprite> _27;

		public SerializableAutomaticReference<Sprite> _46;

		public SerializableAutomaticReference<Sprite> _34;

		public SerializableAutomaticReference<Sprite> _26;

		public SerializableAutomaticReference<Sprite> _45;

		public SerializableAutomaticReference<Sprite> _35;

		public SerializableAutomaticReference<Sprite> _21;

		public SerializableAutomaticReference<Sprite> _44;

		public SerializableAutomaticReference<Sprite> _20;

		public SerializableAutomaticReference<Sprite> _23;

		public SerializableAutomaticReference<Sprite> _38;

		public SerializableAutomaticReference<Sprite> _22;

		public SerializableAutomaticReference<Sprite> _49;

		public SerializableAutomaticReference<Sprite> _39;

		public SerializableAutomaticReference<Sprite> _48;

		public SerializableAutomaticReference<Sprite> _29;

		public SerializableAutomaticReference<Sprite> _28;

		public SerializableAutomaticReference<Sprite> cardBackWhite;

		public SerializableAutomaticReference<Sprite> _18;

		public SerializableAutomaticReference<Sprite> _19;

		public SerializableAutomaticReference<Sprite> _14;

		public SerializableAutomaticReference<Sprite> _15;

		public SerializableAutomaticReference<Sprite> _16;

		public SerializableAutomaticReference<Sprite> _17;

		public SerializableAutomaticReference<Sprite> _10;

		public SerializableAutomaticReference<Sprite> _11;

		public SerializableAutomaticReference<Sprite> _12;

		public SerializableAutomaticReference<Sprite> _13;

		public SerializableAutomaticReference<Sprite> _5;

		public SerializableAutomaticReference<Sprite> _4;

		public SerializableAutomaticReference<Sprite> _7;

		public SerializableAutomaticReference<Sprite> _6;

		public SerializableAutomaticReference<Sprite> _1;

		public SerializableAutomaticReference<Sprite> cardBackBlack;

		public SerializableAutomaticReference<Sprite> _0;

		public SerializableAutomaticReference<Sprite> _3;

		public SerializableAutomaticReference<Sprite> _2;

		public SerializableAutomaticReference<Sprite> _9;

		public SerializableAutomaticReference<Sprite> _8;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	[Serializable]
	public class PcIconsContainer
	{
		public SerializableAutomaticReference<Sprite> arrow;

		public SerializableAutomaticReference<Sprite> browser;

		public SerializableAutomaticReference<Sprite> config;

		public SerializableAutomaticReference<Sprite> deepweb;

		public SerializableAutomaticReference<Sprite> load;

		public SerializableAutomaticReference<Sprite> OnOff;

		public SerializableAutomaticReference<Sprite> save;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	[Serializable]
	public class NewsImagesContainer
	{
		public SerializableAutomaticReference<Sprite> animal;

		public SerializableAutomaticReference<Sprite> army;

		public SerializableAutomaticReference<Sprite> blackman;

		public SerializableAutomaticReference<Sprite> brogan;

		public SerializableAutomaticReference<Sprite> cartoon;

		public SerializableAutomaticReference<Sprite> cemetery;

		public SerializableAutomaticReference<Sprite> child;

		public SerializableAutomaticReference<Sprite> computer;

		public SerializableAutomaticReference<Sprite> demonstration;

		public SerializableAutomaticReference<Sprite> demonstration2;

		public SerializableAutomaticReference<Sprite> fat;

		public SerializableAutomaticReference<Sprite> happykid;

		public SerializableAutomaticReference<Sprite> hotdog;

		public SerializableAutomaticReference<Sprite> island;

		public SerializableAutomaticReference<Sprite> jumper;

		public SerializableAutomaticReference<Sprite> OPanime;

		public SerializableAutomaticReference<Sprite> OPanimeboy;

		public SerializableAutomaticReference<Sprite> OPblacks;

		public SerializableAutomaticReference<Sprite> OPcar;

		public SerializableAutomaticReference<Sprite> OPchildandadult;

		public SerializableAutomaticReference<Sprite> OPcompany;

		public SerializableAutomaticReference<Sprite> OPcomputer2;

		public SerializableAutomaticReference<Sprite> OPcooking;

		public SerializableAutomaticReference<Sprite> OPdragqueen;

		public SerializableAutomaticReference<Sprite> OPfemale;

		public SerializableAutomaticReference<Sprite> OPfuneral;

		public SerializableAutomaticReference<Sprite> OPnaked;

		public SerializableAutomaticReference<Sprite> OPpolice;

		public SerializableAutomaticReference<Sprite> OPpolice2;

		public SerializableAutomaticReference<Sprite> OPpolice3;

		public SerializableAutomaticReference<Sprite> OPpolitician;

		public SerializableAutomaticReference<Sprite> OPpolitics;

		public SerializableAutomaticReference<Sprite> OPpoor;

		public SerializableAutomaticReference<Sprite> OPpope;

		public SerializableAutomaticReference<Sprite> OPporn;

		public SerializableAutomaticReference<Sprite> OPsocialmedia;

		public SerializableAutomaticReference<Sprite> OPsport;

		public SerializableAutomaticReference<Sprite> OPsurgery;

		public SerializableAutomaticReference<Sprite> OPuni;

		public SerializableAutomaticReference<Sprite> pills;

		public SerializableAutomaticReference<Sprite> police;

		public SerializableAutomaticReference<Sprite> police2;

		public SerializableAutomaticReference<Sprite> police3;

		public SerializableAutomaticReference<Sprite> policechild;

		public SerializableAutomaticReference<Sprite> priest;

		public SerializableAutomaticReference<Sprite> protest;

		public SerializableAutomaticReference<Sprite> religion;

		public SerializableAutomaticReference<Sprite> religion2;

		public SerializableAutomaticReference<Sprite> revolution;

		public SerializableAutomaticReference<Sprite> ring;

		public SerializableAutomaticReference<Sprite> satellite;

		public SerializableAutomaticReference<Sprite> sewer;

		public SerializableAutomaticReference<Sprite> sex;

		public SerializableAutomaticReference<Sprite> soldiers;

		public SerializableAutomaticReference<Sprite> toilet;

		public SerializableAutomaticReference<Sprite> virus;

		public SerializableAutomaticReference<Sprite> weapon;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	[Serializable]
	public class GameLogoContainer
	{
		public SerializableAutomaticReference<Sprite> Logo_B_empty;

		public SerializableAutomaticReference<Sprite> Logo_B_small;

		public SerializableAutomaticReference<Sprite> Logo_C_small;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Sprite>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Sprite>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>();

		[NonSerialized]
		public SerializableAutomaticReference<Sprite>[] All = Array.Empty<SerializableAutomaticReference<Sprite>>();
	}

	public AltBackgroundsContainer AltBackgrounds;

	public FishingBackgroundsContainer FishingBackgrounds;

	public PartnerLogosContainer PartnerLogos;

	public OtherLogosContainer OtherLogos;

	public IconsContainer Icons;

	public CardsMainContainer CardsMain;

	public CardsCasinoContainer CardsCasino;

	public PcIconsContainer PcIcons;

	public NewsImagesContainer NewsImages;

	public GameLogoContainer GameLogo;

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		AltBackgrounds.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["Bat Banner"] = AltBackgrounds.Bat_Banner,
			["Ghost Background"] = AltBackgrounds.Ghost_Background,
			["Pumpkin Mat"] = AltBackgrounds.Pumpkin_Mat,
			["SpiderPlushy"] = AltBackgrounds.SpiderPlushy,
			["Blink Halloween"] = AltBackgrounds.Blink_Halloween,
			["Decoration Halloween"] = AltBackgrounds.Decoration_Halloween,
			["Halloween Cat"] = AltBackgrounds.Halloween_Cat
		};
		AltBackgrounds.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("Bat Banner", "Bat Banner")] = AltBackgrounds.Bat_Banner,
			[("Ghost Background", "Ghost Background")] = AltBackgrounds.Ghost_Background,
			[("Pumpkin Mat", "Pumpkin Mat")] = AltBackgrounds.Pumpkin_Mat,
			[("SpiderPlushy", "SpiderPlushy")] = AltBackgrounds.SpiderPlushy,
			[("Blink Halloween", "Blink Halloween")] = AltBackgrounds.Blink_Halloween,
			[("Decoration Halloween", "Decoration Halloween")] = AltBackgrounds.Decoration_Halloween,
			[("Halloween Cat", "Halloween Cat")] = AltBackgrounds.Halloween_Cat
		};
		AltBackgrounds.All = new SerializableAutomaticReference<Sprite>[7] { AltBackgrounds.Bat_Banner, AltBackgrounds.Ghost_Background, AltBackgrounds.Pumpkin_Mat, AltBackgrounds.SpiderPlushy, AltBackgrounds.Blink_Halloween, AltBackgrounds.Decoration_Halloween, AltBackgrounds.Halloween_Cat };
		FishingBackgrounds.FishingJoint_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["GreenField"] = FishingBackgrounds.FishingJoint_GreenField,
			["SunnyBeach"] = FishingBackgrounds.FishingJoint_SunnyBeach
		};
		FishingBackgrounds.FishingJoint_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("GreenField", "GreenField")] = FishingBackgrounds.FishingJoint_GreenField,
			[("SunnyBeach", "SunnyBeach")] = FishingBackgrounds.FishingJoint_SunnyBeach
		};
		FishingBackgrounds.FishingJoint_All = new SerializableAutomaticReference<Sprite>[2] { FishingBackgrounds.FishingJoint_GreenField, FishingBackgrounds.FishingJoint_SunnyBeach };
		FishingBackgrounds.NullRelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["microplastic coast"] = FishingBackgrounds.microplastic_coast,
			["polluted_lake"] = FishingBackgrounds.polluted_lake,
			["premium_farm"] = FishingBackgrounds.premium_farm,
			["trashed_river"] = FishingBackgrounds.trashed_river
		};
		FishingBackgrounds.NullRelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("microplastic coast", "microplastic coast")] = FishingBackgrounds.microplastic_coast,
			[("polluted_lake", "polluted_lake")] = FishingBackgrounds.polluted_lake,
			[("premium_farm", "premium_farm")] = FishingBackgrounds.premium_farm,
			[("trashed_river", "trashed_river")] = FishingBackgrounds.trashed_river
		};
		FishingBackgrounds.All = new SerializableAutomaticReference<Sprite>[6] { FishingBackgrounds.FishingJoint_GreenField, FishingBackgrounds.FishingJoint_SunnyBeach, FishingBackgrounds.microplastic_coast, FishingBackgrounds.polluted_lake, FishingBackgrounds.premium_farm, FishingBackgrounds.trashed_river };
		PartnerLogos.Alternative_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["DiabeteColaPow"] = PartnerLogos.Alternative_DiabeteColaPow,
			["DiabeteDrSugarTrans"] = PartnerLogos.Alternative_DiabeteDrSugarTrans,
			["DiabeteSweetPotatoTrans"] = PartnerLogos.Alternative_DiabeteSweetPotatoTrans,
			["DiabeteTransparent"] = PartnerLogos.Alternative_DiabeteTransparent
		};
		PartnerLogos.Alternative_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("DiabeteColaPow", "DiabeteColaPow")] = PartnerLogos.Alternative_DiabeteColaPow,
			[("DiabeteDrSugarTrans", "DiabeteDrSugarTrans")] = PartnerLogos.Alternative_DiabeteDrSugarTrans,
			[("DiabeteSweetPotatoTrans", "DiabeteSweetPotatoTrans")] = PartnerLogos.Alternative_DiabeteSweetPotatoTrans,
			[("DiabeteTransparent", "DiabeteTransparent")] = PartnerLogos.Alternative_DiabeteTransparent
		};
		PartnerLogos.Alternative_All = new SerializableAutomaticReference<Sprite>[4] { PartnerLogos.Alternative_DiabeteColaPow, PartnerLogos.Alternative_DiabeteDrSugarTrans, PartnerLogos.Alternative_DiabeteSweetPotatoTrans, PartnerLogos.Alternative_DiabeteTransparent };
		PartnerLogos.NullRelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["Cazino"] = PartnerLogos.Cazino,
			["Diabete"] = PartnerLogos.Diabete,
			["Luxe"] = PartnerLogos.Luxe,
			["Monizmed"] = PartnerLogos.Monizmed,
			["RehabTech"] = PartnerLogos.RehabTech,
			["Stilou"] = PartnerLogos.Stilou
		};
		PartnerLogos.NullRelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("Cazino", "Cazino")] = PartnerLogos.Cazino,
			[("Diabete", "Diabete")] = PartnerLogos.Diabete,
			[("Luxe", "Luxe")] = PartnerLogos.Luxe,
			[("Monizmed", "Monizmed")] = PartnerLogos.Monizmed,
			[("RehabTech", "RehabTech")] = PartnerLogos.RehabTech,
			[("Stilou", "Stilou")] = PartnerLogos.Stilou
		};
		PartnerLogos.All = new SerializableAutomaticReference<Sprite>[10] { PartnerLogos.Alternative_DiabeteColaPow, PartnerLogos.Alternative_DiabeteDrSugarTrans, PartnerLogos.Alternative_DiabeteSweetPotatoTrans, PartnerLogos.Alternative_DiabeteTransparent, PartnerLogos.Cazino, PartnerLogos.Diabete, PartnerLogos.Luxe, PartnerLogos.Monizmed, PartnerLogos.RehabTech, PartnerLogos.Stilou };
		OtherLogos.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["AGUI_Logo"] = OtherLogos.AGUI_Logo,
			["Baka"] = OtherLogos.Baka,
			["Banana"] = OtherLogos.Banana,
			["Bedabots_Logo"] = OtherLogos.Bedabots_Logo,
			["BloodyMoon"] = OtherLogos.BloodyMoon,
			["BotLogo1"] = OtherLogos.BotLogo1,
			["Bow"] = OtherLogos.Bow,
			["CELESTYN"] = OtherLogos.CELESTYN,
			["CIA"] = OtherLogos.CIA,
			["CosplayHouse"] = OtherLogos.CosplayHouse,
			["DDlogo1"] = OtherLogos.DDlogo1,
			["DogeCoin"] = OtherLogos.DogeCoin,
			["fishFearMeLogo"] = OtherLogos.fishFearMeLogo,
			["Flowerkidv"] = OtherLogos.Flowerkidv,
			["Fruit+"] = OtherLogos.Strawberry,
			["galaxy"] = OtherLogos.galaxy,
			["GamerTshirt"] = OtherLogos.GamerTshirt,
			["HikkeiruLogo"] = OtherLogos.HikkeiruLogo,
			["HotPinkGames_Logo"] = OtherLogos.HotPinkGames_Logo,
			["InHeatLogo"] = OtherLogos.InHeatLogo,
			["LightSonic"] = OtherLogos.LightSonic,
			["MadLogo"] = OtherLogos.MadLogo,
			["MILF"] = OtherLogos.MILF,
			["mirthal"] = OtherLogos.mirthal,
			["Mushroom"] = OtherLogos.Mushroom,
			["NitroriLogo"] = OtherLogos.NitroriLogo,
			["NutekuLogo"] = OtherLogos.NutekuLogo,
			["Polandball"] = OtherLogos.Polandball,
			["priestbot_tshirt"] = OtherLogos.priestbot_tshirt,
			["projekt_melody_nbtw_69"] = OtherLogos.projekt_melody_nbtw_69,
			["projekt_melody_nbtw_logo"] = OtherLogos.projekt_melody_nbtw_logo,
			["radioactive"] = OtherLogos.radioactive,
			["Rose"] = OtherLogos.Rose,
			["RottingSteelLogo"] = OtherLogos.RottingSteelLogo,
			["Shady_Corner_Logo"] = OtherLogos.Shady_Corner_Logo,
			["shcHoodie"] = OtherLogos.shcHoodie,
			["shcPanties"] = OtherLogos.shcPanties,
			["Sheep"] = OtherLogos.Sheep,
			["siluman_Alice"] = OtherLogos.siluman_Alice,
			["siluman_logo"] = OtherLogos.siluman_logo,
			["SJ68WHITE"] = OtherLogos.SJ68WHITE,
			["skull"] = OtherLogos.skull,
			["sylphy_chibi"] = OtherLogos.sylphy_chibi,
			["Temple"] = OtherLogos.Temple,
			["tonis album"] = OtherLogos.tonis_album,
			["ufo"] = OtherLogos.ufo,
			["USP"] = OtherLogos.USP,
			["WeebDesign"] = OtherLogos.WeebDesign,
			["WithStupid"] = OtherLogos.WithStupid,
			["world_tamer_logo"] = OtherLogos.world_tamer_logo,
			["WyldSpace"] = OtherLogos.WyldSpace,
			["XoulionLogo"] = OtherLogos.XoulionLogo,
			["yaranaika"] = OtherLogos.yaranaika
		};
		OtherLogos.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("AGUI_Logo", "AGUI_Logo")] = OtherLogos.AGUI_Logo,
			[("Baka", "Baka")] = OtherLogos.Baka,
			[("Banana", "Banana")] = OtherLogos.Banana,
			[("Bedabots_Logo", "Bedabots_Logo")] = OtherLogos.Bedabots_Logo,
			[("BloodyMoon", "BloodyMoon")] = OtherLogos.BloodyMoon,
			[("BotLogo1", "BotLogo1")] = OtherLogos.BotLogo1,
			[("Bow", "Bow")] = OtherLogos.Bow,
			[("CELESTYN", "CELESTYN")] = OtherLogos.CELESTYN,
			[("CIA", "CIA")] = OtherLogos.CIA,
			[("CosplayHouse", "CosplayHouse")] = OtherLogos.CosplayHouse,
			[("DDlogo1", "DDlogo1")] = OtherLogos.DDlogo1,
			[("DogeCoin", "DogeCoin")] = OtherLogos.DogeCoin,
			[("fishFearMeLogo", "fishFearMeLogo")] = OtherLogos.fishFearMeLogo,
			[("Flowerkidv", "Flowerkidv")] = OtherLogos.Flowerkidv,
			[("Fruit+", "Strawberry")] = OtherLogos.Strawberry,
			[("Fruit+", "Cherry")] = OtherLogos.Cherry,
			[("Fruit+", "Peach")] = OtherLogos.Peach,
			[("Fruit+", "Avocado")] = OtherLogos.Avocado,
			[("galaxy", "galaxy")] = OtherLogos.galaxy,
			[("GamerTshirt", "GamerTshirt")] = OtherLogos.GamerTshirt,
			[("HikkeiruLogo", "HikkeiruLogo")] = OtherLogos.HikkeiruLogo,
			[("HotPinkGames_Logo", "HotPinkGames_Logo")] = OtherLogos.HotPinkGames_Logo,
			[("InHeatLogo", "InHeatLogo")] = OtherLogos.InHeatLogo,
			[("LightSonic", "LightSonic")] = OtherLogos.LightSonic,
			[("MadLogo", "MadLogo")] = OtherLogos.MadLogo,
			[("MILF", "MILF")] = OtherLogos.MILF,
			[("mirthal", "mirthal")] = OtherLogos.mirthal,
			[("Mushroom", "Mushroom")] = OtherLogos.Mushroom,
			[("NitroriLogo", "NitroriLogo")] = OtherLogos.NitroriLogo,
			[("NutekuLogo", "NutekuLogo")] = OtherLogos.NutekuLogo,
			[("Polandball", "Polandball")] = OtherLogos.Polandball,
			[("priestbot_tshirt", "priestbot_tshirt")] = OtherLogos.priestbot_tshirt,
			[("projekt_melody_nbtw_69", "projekt_melody_nbtw_69")] = OtherLogos.projekt_melody_nbtw_69,
			[("projekt_melody_nbtw_logo", "projekt_melody_nbtw_logo")] = OtherLogos.projekt_melody_nbtw_logo,
			[("radioactive", "radioactive")] = OtherLogos.radioactive,
			[("Rose", "Rose")] = OtherLogos.Rose,
			[("RottingSteelLogo", "RottingSteelLogo")] = OtherLogos.RottingSteelLogo,
			[("Shady_Corner_Logo", "Shady_Corner_Logo")] = OtherLogos.Shady_Corner_Logo,
			[("shcHoodie", "shcHoodie")] = OtherLogos.shcHoodie,
			[("shcPanties", "shcPanties")] = OtherLogos.shcPanties,
			[("Sheep", "Sheep")] = OtherLogos.Sheep,
			[("siluman_Alice", "siluman_Alice")] = OtherLogos.siluman_Alice,
			[("siluman_logo", "siluman_logo")] = OtherLogos.siluman_logo,
			[("SJ68WHITE", "SJ68WHITE")] = OtherLogos.SJ68WHITE,
			[("skull", "skull")] = OtherLogos.skull,
			[("sylphy_chibi", "sylphy_chibi")] = OtherLogos.sylphy_chibi,
			[("Temple", "Temple")] = OtherLogos.Temple,
			[("tonis album", "tonis album")] = OtherLogos.tonis_album,
			[("ufo", "ufo")] = OtherLogos.ufo,
			[("USP", "USP")] = OtherLogos.USP,
			[("WeebDesign", "WeebDesign")] = OtherLogos.WeebDesign,
			[("WithStupid", "WithStupid")] = OtherLogos.WithStupid,
			[("world_tamer_logo", "world_tamer_logo")] = OtherLogos.world_tamer_logo,
			[("WyldSpace", "WyldSpace")] = OtherLogos.WyldSpace,
			[("XoulionLogo", "XoulionLogo")] = OtherLogos.XoulionLogo,
			[("yaranaika", "yaranaika")] = OtherLogos.yaranaika
		};
		OtherLogos.All = new SerializableAutomaticReference<Sprite>[56]
		{
			OtherLogos.AGUI_Logo, OtherLogos.Baka, OtherLogos.Banana, OtherLogos.Bedabots_Logo, OtherLogos.BloodyMoon, OtherLogos.BotLogo1, OtherLogos.Bow, OtherLogos.CELESTYN, OtherLogos.CIA, OtherLogos.CosplayHouse,
			OtherLogos.DDlogo1, OtherLogos.DogeCoin, OtherLogos.fishFearMeLogo, OtherLogos.Flowerkidv, OtherLogos.Strawberry, OtherLogos.Cherry, OtherLogos.Peach, OtherLogos.Avocado, OtherLogos.galaxy, OtherLogos.GamerTshirt,
			OtherLogos.HikkeiruLogo, OtherLogos.HotPinkGames_Logo, OtherLogos.InHeatLogo, OtherLogos.LightSonic, OtherLogos.MadLogo, OtherLogos.MILF, OtherLogos.mirthal, OtherLogos.Mushroom, OtherLogos.NitroriLogo, OtherLogos.NutekuLogo,
			OtherLogos.Polandball, OtherLogos.priestbot_tshirt, OtherLogos.projekt_melody_nbtw_69, OtherLogos.projekt_melody_nbtw_logo, OtherLogos.radioactive, OtherLogos.Rose, OtherLogos.RottingSteelLogo, OtherLogos.Shady_Corner_Logo, OtherLogos.shcHoodie, OtherLogos.shcPanties,
			OtherLogos.Sheep, OtherLogos.siluman_Alice, OtherLogos.siluman_logo, OtherLogos.SJ68WHITE, OtherLogos.skull, OtherLogos.sylphy_chibi, OtherLogos.Temple, OtherLogos.tonis_album, OtherLogos.ufo, OtherLogos.USP,
			OtherLogos.WeebDesign, OtherLogos.WithStupid, OtherLogos.world_tamer_logo, OtherLogos.WyldSpace, OtherLogos.XoulionLogo, OtherLogos.yaranaika
		};
		Icons.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["arm"] = Icons.arm,
			["arm_alt"] = Icons.arm_alt,
			["arrow_next"] = Icons.btn_icon_arrow_next_0,
			["arrow_prev"] = Icons.btn_icon_arrow_prev_0,
			["back"] = Icons.back,
			["bait"] = Icons.bait,
			["book_closed"] = Icons.btn_icon_book_0,
			["book_open"] = Icons.btn_icon_book_open_1,
			["bra"] = Icons.bra,
			["cat_ears"] = Icons.cat_ears,
			["chip"] = Icons.chip,
			["close_round"] = Icons.close_round,
			["clothes"] = Icons.clothes,
			["dish_1"] = Icons.dish_1,
			["dish_2"] = Icons.dish_2,
			["dish_3"] = Icons.dish_3,
			["dress"] = Icons.dress,
			["fish"] = Icons.fish,
			["fishing_rod"] = Icons.fishing_rod,
			["food"] = Icons.food,
			["gift"] = Icons.gift,
			["glasses"] = Icons.glasses,
			["halloween"] = Icons.halloween,
			["heart"] = Icons.heart,
			["hoodie"] = Icons.hoodie,
			["Icon_check"] = Icons.Icon_check,
			["Icon_close"] = Icons.Icon_close,
			["Icon_dick"] = Icons.Icon_dick,
			["Icon_dropdown"] = Icons.Icon_dropdown,
			["Icon_energy"] = Icons.Icon_energy,
			["Icon_follow"] = Icons.Icon_follow,
			["Icon_hand"] = Icons.Icon_hand,
			["Icon_noview"] = Icons.Icon_noview,
			["Icon_psyche"] = Icons.Icon_psyche,
			["Icon_Soap"] = Icons.Icon_Soap,
			["Icon_Sponge"] = Icons.Icon_Sponge,
			["Icon_subs"] = Icons.Icon_subs,
			["Icon_tongue"] = Icons.Icon_tongue,
			["Icon_trash"] = Icons.Icon_trash,
			["Icon_Trophy"] = Icons.Icon_Trophy_0,
			["Icon_view"] = Icons.Icon_view,
			["ingredient"] = Icons.ingredient,
			["leg"] = Icons.leg,
			["leg_alt"] = Icons.leg_alt,
			["Lock Locked"] = Icons.Lock_Locked_0,
			["Lock Unlocked"] = Icons.Lock_Unlocked,
			["log"] = Icons.log,
			["Logo_bluesky"] = Icons.Logo_bluesky,
			["Logo_discord"] = Icons.Logo_discord,
			["Logo_itch"] = Icons.Logo_itch,
			["Logo_pat"] = Icons.Logo_pat,
			["Logo_substar"] = Icons.Logo_substar,
			["Logo_tw"] = Icons.Logo_tw,
			["medicine"] = Icons.medicine,
			["necklace"] = Icons.necklace,
			["pants"] = Icons.pants,
			["parts"] = Icons.parts,
			["refresh"] = Icons.btn_icon_refresh_0,
			["save"] = Icons.save,
			["shirt"] = Icons.shirt,
			["shoes"] = Icons.shoes,
			["skip"] = Icons.skip,
			["skirt"] = Icons.skirt,
			["socks"] = Icons.socks,
			["special"] = Icons.special,
			["stuff"] = Icons.stuff,
			["tail"] = Icons.tail,
			["thong"] = Icons.thong,
			["undo"] = Icons.btn_icon_undo_0,
			["vote_downvote"] = Icons.vote_downvote,
			["vote_upvote"] = Icons.vote_upvote
		};
		Icons.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("arm", "arm")] = Icons.arm,
			[("arm_alt", "arm_alt")] = Icons.arm_alt,
			[("arrow_next", "btn_icon_arrow_next_0")] = Icons.btn_icon_arrow_next_0,
			[("arrow_prev", "btn_icon_arrow_prev_0")] = Icons.btn_icon_arrow_prev_0,
			[("back", "back")] = Icons.back,
			[("bait", "bait")] = Icons.bait,
			[("book_closed", "btn_icon_book_0")] = Icons.btn_icon_book_0,
			[("book_open", "btn_icon_book_open_1")] = Icons.btn_icon_book_open_1,
			[("book_open", "btn_icon_book_open_0")] = Icons.btn_icon_book_open_0,
			[("book_open", "btn_icon_book_open_2")] = Icons.btn_icon_book_open_2,
			[("bra", "bra")] = Icons.bra,
			[("cat_ears", "cat_ears")] = Icons.cat_ears,
			[("chip", "chip")] = Icons.chip,
			[("close_round", "close_round")] = Icons.close_round,
			[("clothes", "clothes")] = Icons.clothes,
			[("dish_1", "dish_1")] = Icons.dish_1,
			[("dish_2", "dish_2")] = Icons.dish_2,
			[("dish_3", "dish_3")] = Icons.dish_3,
			[("dress", "dress")] = Icons.dress,
			[("fish", "fish")] = Icons.fish,
			[("fishing_rod", "fishing_rod")] = Icons.fishing_rod,
			[("food", "food")] = Icons.food,
			[("gift", "gift")] = Icons.gift,
			[("glasses", "glasses")] = Icons.glasses,
			[("halloween", "halloween")] = Icons.halloween,
			[("heart", "heart")] = Icons.heart,
			[("hoodie", "hoodie")] = Icons.hoodie,
			[("Icon_check", "Icon_check")] = Icons.Icon_check,
			[("Icon_close", "Icon_close")] = Icons.Icon_close,
			[("Icon_dick", "Icon_dick")] = Icons.Icon_dick,
			[("Icon_dropdown", "Icon_dropdown")] = Icons.Icon_dropdown,
			[("Icon_energy", "Icon_energy")] = Icons.Icon_energy,
			[("Icon_follow", "Icon_follow")] = Icons.Icon_follow,
			[("Icon_hand", "Icon_hand")] = Icons.Icon_hand,
			[("Icon_noview", "Icon_noview")] = Icons.Icon_noview,
			[("Icon_psyche", "Icon_psyche")] = Icons.Icon_psyche,
			[("Icon_Soap", "Icon_Soap")] = Icons.Icon_Soap,
			[("Icon_Sponge", "Icon_Sponge")] = Icons.Icon_Sponge,
			[("Icon_subs", "Icon_subs")] = Icons.Icon_subs,
			[("Icon_tongue", "Icon_tongue")] = Icons.Icon_tongue,
			[("Icon_trash", "Icon_trash")] = Icons.Icon_trash,
			[("Icon_Trophy", "Icon_Trophy_0")] = Icons.Icon_Trophy_0,
			[("Icon_Trophy", "Icon_Trophy_1")] = Icons.Icon_Trophy_1,
			[("Icon_view", "Icon_view")] = Icons.Icon_view,
			[("ingredient", "ingredient")] = Icons.ingredient,
			[("leg", "leg")] = Icons.leg,
			[("leg_alt", "leg_alt")] = Icons.leg_alt,
			[("Lock Locked", "Lock Locked_0")] = Icons.Lock_Locked_0,
			[("Lock Unlocked", "Lock Unlocked")] = Icons.Lock_Unlocked,
			[("log", "log")] = Icons.log,
			[("Logo_bluesky", "Logo_bluesky")] = Icons.Logo_bluesky,
			[("Logo_discord", "Logo_discord")] = Icons.Logo_discord,
			[("Logo_itch", "Logo_itch")] = Icons.Logo_itch,
			[("Logo_pat", "Logo_pat")] = Icons.Logo_pat,
			[("Logo_substar", "Logo_substar")] = Icons.Logo_substar,
			[("Logo_tw", "Logo_tw")] = Icons.Logo_tw,
			[("medicine", "medicine")] = Icons.medicine,
			[("necklace", "necklace")] = Icons.necklace,
			[("pants", "pants")] = Icons.pants,
			[("parts", "parts")] = Icons.parts,
			[("refresh", "btn_icon_refresh_0")] = Icons.btn_icon_refresh_0,
			[("save", "save")] = Icons.save,
			[("shirt", "shirt")] = Icons.shirt,
			[("shoes", "shoes")] = Icons.shoes,
			[("skip", "skip")] = Icons.skip,
			[("skirt", "skirt")] = Icons.skirt,
			[("socks", "socks")] = Icons.socks,
			[("special", "special")] = Icons.special,
			[("stuff", "stuff")] = Icons.stuff,
			[("tail", "tail")] = Icons.tail,
			[("thong", "thong")] = Icons.thong,
			[("undo", "btn_icon_undo_0")] = Icons.btn_icon_undo_0,
			[("vote_downvote", "vote_downvote")] = Icons.vote_downvote,
			[("vote_upvote", "vote_upvote")] = Icons.vote_upvote
		};
		Icons.All = new SerializableAutomaticReference<Sprite>[74]
		{
			Icons.arm, Icons.arm_alt, Icons.btn_icon_arrow_next_0, Icons.btn_icon_arrow_prev_0, Icons.back, Icons.bait, Icons.btn_icon_book_0, Icons.btn_icon_book_open_1, Icons.btn_icon_book_open_0, Icons.btn_icon_book_open_2,
			Icons.bra, Icons.cat_ears, Icons.chip, Icons.close_round, Icons.clothes, Icons.dish_1, Icons.dish_2, Icons.dish_3, Icons.dress, Icons.fish,
			Icons.fishing_rod, Icons.food, Icons.gift, Icons.glasses, Icons.halloween, Icons.heart, Icons.hoodie, Icons.Icon_check, Icons.Icon_close, Icons.Icon_dick,
			Icons.Icon_dropdown, Icons.Icon_energy, Icons.Icon_follow, Icons.Icon_hand, Icons.Icon_noview, Icons.Icon_psyche, Icons.Icon_Soap, Icons.Icon_Sponge, Icons.Icon_subs, Icons.Icon_tongue,
			Icons.Icon_trash, Icons.Icon_Trophy_0, Icons.Icon_Trophy_1, Icons.Icon_view, Icons.ingredient, Icons.leg, Icons.leg_alt, Icons.Lock_Locked_0, Icons.Lock_Unlocked, Icons.log,
			Icons.Logo_bluesky, Icons.Logo_discord, Icons.Logo_itch, Icons.Logo_pat, Icons.Logo_substar, Icons.Logo_tw, Icons.medicine, Icons.necklace, Icons.pants, Icons.parts,
			Icons.btn_icon_refresh_0, Icons.save, Icons.shirt, Icons.shoes, Icons.skip, Icons.skirt, Icons.socks, Icons.special, Icons.stuff, Icons.tail,
			Icons.thong, Icons.btn_icon_undo_0, Icons.vote_downvote, Icons.vote_upvote
		};
		CardsMain.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["CardBack"] = CardsMain.CardBack,
			["CardShadow"] = CardsMain.CardShadow,
			["Clovers_2_white"] = CardsMain.Clovers_2_white,
			["Clovers_3_white"] = CardsMain.Clovers_3_white,
			["Clovers_4_white"] = CardsMain.Clovers_4_white,
			["Clovers_5_white"] = CardsMain.Clovers_5_white,
			["Clovers_6_white"] = CardsMain.Clovers_6_white,
			["Clovers_7_white"] = CardsMain.Clovers_7_white,
			["Clovers_8_white"] = CardsMain.Clovers_8_white,
			["Clovers_9_white"] = CardsMain.Clovers_9_white,
			["Clovers_10_white"] = CardsMain.Clovers_10_white,
			["Clovers_A_white"] = CardsMain.Clovers_A_white,
			["Clovers_Jack_white"] = CardsMain.Clovers_Jack_white,
			["Clovers_King_white"] = CardsMain.Clovers_King_white,
			["Clovers_Queen_white"] = CardsMain.Clovers_Queen_white,
			["Hearts_2_white"] = CardsMain.Hearts_2_white,
			["Hearts_3_white"] = CardsMain.Hearts_3_white,
			["Hearts_4_white"] = CardsMain.Hearts_4_white,
			["Hearts_5_white"] = CardsMain.Hearts_5_white,
			["Hearts_6_white"] = CardsMain.Hearts_6_white,
			["Hearts_7_white"] = CardsMain.Hearts_7_white,
			["Hearts_8_white"] = CardsMain.Hearts_8_white,
			["Hearts_9_white"] = CardsMain.Hearts_9_white,
			["Hearts_10_white"] = CardsMain.Hearts_10_white,
			["Hearts_A_white"] = CardsMain.Hearts_A_white,
			["Hearts_Jack_white"] = CardsMain.Hearts_Jack_white,
			["Hearts_King_white"] = CardsMain.Hearts_King_white,
			["Hearts_Queen_white"] = CardsMain.Hearts_Queen_white,
			["Pikes_2_white"] = CardsMain.Pikes_2_white,
			["Pikes_3_white"] = CardsMain.Pikes_3_white,
			["Pikes_4_white"] = CardsMain.Pikes_4_white,
			["Pikes_5_white"] = CardsMain.Pikes_5_white,
			["Pikes_6_white"] = CardsMain.Pikes_6_white,
			["Pikes_7_white"] = CardsMain.Pikes_7_white,
			["Pikes_8_white"] = CardsMain.Pikes_8_white,
			["Pikes_9_white"] = CardsMain.Pikes_9_white,
			["Pikes_10_white"] = CardsMain.Pikes_10_white,
			["Pikes_A_white"] = CardsMain.Pikes_A_white,
			["Pikes_Jack_white"] = CardsMain.Pikes_Jack_white,
			["Pikes_King_white"] = CardsMain.Pikes_King_white,
			["Pikes_Queen_white"] = CardsMain.Pikes_Queen_white,
			["Tiles_2_white"] = CardsMain.Tiles_2_white,
			["Tiles_3_white"] = CardsMain.Tiles_3_white,
			["Tiles_4_white"] = CardsMain.Tiles_4_white,
			["Tiles_5_white"] = CardsMain.Tiles_5_white,
			["Tiles_6_white"] = CardsMain.Tiles_6_white,
			["Tiles_7_white"] = CardsMain.Tiles_7_white,
			["Tiles_8_white"] = CardsMain.Tiles_8_white,
			["Tiles_9_white"] = CardsMain.Tiles_9_white,
			["Tiles_10_white"] = CardsMain.Tiles_10_white,
			["Tiles_A_white"] = CardsMain.Tiles_A_white,
			["Tiles_Jack_white"] = CardsMain.Tiles_Jack_white,
			["Tiles_King_white"] = CardsMain.Tiles_King_white,
			["Tiles_Queen_white"] = CardsMain.Tiles_Queen_white
		};
		CardsMain.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("CardBack", "CardBack")] = CardsMain.CardBack,
			[("CardShadow", "CardShadow")] = CardsMain.CardShadow,
			[("Clovers_2_white", "Clovers_2_white")] = CardsMain.Clovers_2_white,
			[("Clovers_3_white", "Clovers_3_white")] = CardsMain.Clovers_3_white,
			[("Clovers_4_white", "Clovers_4_white")] = CardsMain.Clovers_4_white,
			[("Clovers_5_white", "Clovers_5_white")] = CardsMain.Clovers_5_white,
			[("Clovers_6_white", "Clovers_6_white")] = CardsMain.Clovers_6_white,
			[("Clovers_7_white", "Clovers_7_white")] = CardsMain.Clovers_7_white,
			[("Clovers_8_white", "Clovers_8_white")] = CardsMain.Clovers_8_white,
			[("Clovers_9_white", "Clovers_9_white")] = CardsMain.Clovers_9_white,
			[("Clovers_10_white", "Clovers_10_white")] = CardsMain.Clovers_10_white,
			[("Clovers_A_white", "Clovers_A_white")] = CardsMain.Clovers_A_white,
			[("Clovers_Jack_white", "Clovers_Jack_white")] = CardsMain.Clovers_Jack_white,
			[("Clovers_King_white", "Clovers_King_white")] = CardsMain.Clovers_King_white,
			[("Clovers_Queen_white", "Clovers_Queen_white")] = CardsMain.Clovers_Queen_white,
			[("Hearts_2_white", "Hearts_2_white")] = CardsMain.Hearts_2_white,
			[("Hearts_3_white", "Hearts_3_white")] = CardsMain.Hearts_3_white,
			[("Hearts_4_white", "Hearts_4_white")] = CardsMain.Hearts_4_white,
			[("Hearts_5_white", "Hearts_5_white")] = CardsMain.Hearts_5_white,
			[("Hearts_6_white", "Hearts_6_white")] = CardsMain.Hearts_6_white,
			[("Hearts_7_white", "Hearts_7_white")] = CardsMain.Hearts_7_white,
			[("Hearts_8_white", "Hearts_8_white")] = CardsMain.Hearts_8_white,
			[("Hearts_9_white", "Hearts_9_white")] = CardsMain.Hearts_9_white,
			[("Hearts_10_white", "Hearts_10_white")] = CardsMain.Hearts_10_white,
			[("Hearts_A_white", "Hearts_A_white")] = CardsMain.Hearts_A_white,
			[("Hearts_Jack_white", "Hearts_Jack_white")] = CardsMain.Hearts_Jack_white,
			[("Hearts_King_white", "Hearts_King_white")] = CardsMain.Hearts_King_white,
			[("Hearts_Queen_white", "Hearts_Queen_white")] = CardsMain.Hearts_Queen_white,
			[("Pikes_2_white", "Pikes_2_white")] = CardsMain.Pikes_2_white,
			[("Pikes_3_white", "Pikes_3_white")] = CardsMain.Pikes_3_white,
			[("Pikes_4_white", "Pikes_4_white")] = CardsMain.Pikes_4_white,
			[("Pikes_5_white", "Pikes_5_white")] = CardsMain.Pikes_5_white,
			[("Pikes_6_white", "Pikes_6_white")] = CardsMain.Pikes_6_white,
			[("Pikes_7_white", "Pikes_7_white")] = CardsMain.Pikes_7_white,
			[("Pikes_8_white", "Pikes_8_white")] = CardsMain.Pikes_8_white,
			[("Pikes_9_white", "Pikes_9_white")] = CardsMain.Pikes_9_white,
			[("Pikes_10_white", "Pikes_10_white")] = CardsMain.Pikes_10_white,
			[("Pikes_A_white", "Pikes_A_white")] = CardsMain.Pikes_A_white,
			[("Pikes_Jack_white", "Pikes_Jack_white")] = CardsMain.Pikes_Jack_white,
			[("Pikes_King_white", "Pikes_King_white")] = CardsMain.Pikes_King_white,
			[("Pikes_Queen_white", "Pikes_Queen_white")] = CardsMain.Pikes_Queen_white,
			[("Tiles_2_white", "Tiles_2_white")] = CardsMain.Tiles_2_white,
			[("Tiles_3_white", "Tiles_3_white")] = CardsMain.Tiles_3_white,
			[("Tiles_4_white", "Tiles_4_white")] = CardsMain.Tiles_4_white,
			[("Tiles_5_white", "Tiles_5_white")] = CardsMain.Tiles_5_white,
			[("Tiles_6_white", "Tiles_6_white")] = CardsMain.Tiles_6_white,
			[("Tiles_7_white", "Tiles_7_white")] = CardsMain.Tiles_7_white,
			[("Tiles_8_white", "Tiles_8_white")] = CardsMain.Tiles_8_white,
			[("Tiles_9_white", "Tiles_9_white")] = CardsMain.Tiles_9_white,
			[("Tiles_10_white", "Tiles_10_white")] = CardsMain.Tiles_10_white,
			[("Tiles_A_white", "Tiles_A_white")] = CardsMain.Tiles_A_white,
			[("Tiles_Jack_white", "Tiles_Jack_white")] = CardsMain.Tiles_Jack_white,
			[("Tiles_King_white", "Tiles_King_white")] = CardsMain.Tiles_King_white,
			[("Tiles_Queen_white", "Tiles_Queen_white")] = CardsMain.Tiles_Queen_white
		};
		CardsMain.All = new SerializableAutomaticReference<Sprite>[54]
		{
			CardsMain.CardBack, CardsMain.CardShadow, CardsMain.Clovers_2_white, CardsMain.Clovers_3_white, CardsMain.Clovers_4_white, CardsMain.Clovers_5_white, CardsMain.Clovers_6_white, CardsMain.Clovers_7_white, CardsMain.Clovers_8_white, CardsMain.Clovers_9_white,
			CardsMain.Clovers_10_white, CardsMain.Clovers_A_white, CardsMain.Clovers_Jack_white, CardsMain.Clovers_King_white, CardsMain.Clovers_Queen_white, CardsMain.Hearts_2_white, CardsMain.Hearts_3_white, CardsMain.Hearts_4_white, CardsMain.Hearts_5_white, CardsMain.Hearts_6_white,
			CardsMain.Hearts_7_white, CardsMain.Hearts_8_white, CardsMain.Hearts_9_white, CardsMain.Hearts_10_white, CardsMain.Hearts_A_white, CardsMain.Hearts_Jack_white, CardsMain.Hearts_King_white, CardsMain.Hearts_Queen_white, CardsMain.Pikes_2_white, CardsMain.Pikes_3_white,
			CardsMain.Pikes_4_white, CardsMain.Pikes_5_white, CardsMain.Pikes_6_white, CardsMain.Pikes_7_white, CardsMain.Pikes_8_white, CardsMain.Pikes_9_white, CardsMain.Pikes_10_white, CardsMain.Pikes_A_white, CardsMain.Pikes_Jack_white, CardsMain.Pikes_King_white,
			CardsMain.Pikes_Queen_white, CardsMain.Tiles_2_white, CardsMain.Tiles_3_white, CardsMain.Tiles_4_white, CardsMain.Tiles_5_white, CardsMain.Tiles_6_white, CardsMain.Tiles_7_white, CardsMain.Tiles_8_white, CardsMain.Tiles_9_white, CardsMain.Tiles_10_white,
			CardsMain.Tiles_A_white, CardsMain.Tiles_Jack_white, CardsMain.Tiles_King_white, CardsMain.Tiles_Queen_white
		};
		CardsCasino.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>> { ["casinoCards"] = CardsCasino._50 };
		CardsCasino.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("casinoCards", "50")] = CardsCasino._50,
			[("casinoCards", "51")] = CardsCasino._51,
			[("casinoCards", "32")] = CardsCasino._32,
			[("casinoCards", "43")] = CardsCasino._43,
			[("casinoCards", "33")] = CardsCasino._33,
			[("casinoCards", "42")] = CardsCasino._42,
			[("casinoCards", "30")] = CardsCasino._30,
			[("casinoCards", "41")] = CardsCasino._41,
			[("casinoCards", "31")] = CardsCasino._31,
			[("casinoCards", "25")] = CardsCasino._25,
			[("casinoCards", "40")] = CardsCasino._40,
			[("casinoCards", "36")] = CardsCasino._36,
			[("casinoCards", "24")] = CardsCasino._24,
			[("casinoCards", "47")] = CardsCasino._47,
			[("casinoCards", "37")] = CardsCasino._37,
			[("casinoCards", "27")] = CardsCasino._27,
			[("casinoCards", "46")] = CardsCasino._46,
			[("casinoCards", "34")] = CardsCasino._34,
			[("casinoCards", "26")] = CardsCasino._26,
			[("casinoCards", "45")] = CardsCasino._45,
			[("casinoCards", "35")] = CardsCasino._35,
			[("casinoCards", "21")] = CardsCasino._21,
			[("casinoCards", "44")] = CardsCasino._44,
			[("casinoCards", "20")] = CardsCasino._20,
			[("casinoCards", "23")] = CardsCasino._23,
			[("casinoCards", "38")] = CardsCasino._38,
			[("casinoCards", "22")] = CardsCasino._22,
			[("casinoCards", "49")] = CardsCasino._49,
			[("casinoCards", "39")] = CardsCasino._39,
			[("casinoCards", "48")] = CardsCasino._48,
			[("casinoCards", "29")] = CardsCasino._29,
			[("casinoCards", "28")] = CardsCasino._28,
			[("casinoCards", "cardBackWhite")] = CardsCasino.cardBackWhite,
			[("casinoCards", "18")] = CardsCasino._18,
			[("casinoCards", "19")] = CardsCasino._19,
			[("casinoCards", "14")] = CardsCasino._14,
			[("casinoCards", "15")] = CardsCasino._15,
			[("casinoCards", "16")] = CardsCasino._16,
			[("casinoCards", "17")] = CardsCasino._17,
			[("casinoCards", "10")] = CardsCasino._10,
			[("casinoCards", "11")] = CardsCasino._11,
			[("casinoCards", "12")] = CardsCasino._12,
			[("casinoCards", "13")] = CardsCasino._13,
			[("casinoCards", "5")] = CardsCasino._5,
			[("casinoCards", "4")] = CardsCasino._4,
			[("casinoCards", "7")] = CardsCasino._7,
			[("casinoCards", "6")] = CardsCasino._6,
			[("casinoCards", "1")] = CardsCasino._1,
			[("casinoCards", "cardBackBlack")] = CardsCasino.cardBackBlack,
			[("casinoCards", "0")] = CardsCasino._0,
			[("casinoCards", "3")] = CardsCasino._3,
			[("casinoCards", "2")] = CardsCasino._2,
			[("casinoCards", "9")] = CardsCasino._9,
			[("casinoCards", "8")] = CardsCasino._8
		};
		CardsCasino.All = new SerializableAutomaticReference<Sprite>[54]
		{
			CardsCasino._50, CardsCasino._51, CardsCasino._32, CardsCasino._43, CardsCasino._33, CardsCasino._42, CardsCasino._30, CardsCasino._41, CardsCasino._31, CardsCasino._25,
			CardsCasino._40, CardsCasino._36, CardsCasino._24, CardsCasino._47, CardsCasino._37, CardsCasino._27, CardsCasino._46, CardsCasino._34, CardsCasino._26, CardsCasino._45,
			CardsCasino._35, CardsCasino._21, CardsCasino._44, CardsCasino._20, CardsCasino._23, CardsCasino._38, CardsCasino._22, CardsCasino._49, CardsCasino._39, CardsCasino._48,
			CardsCasino._29, CardsCasino._28, CardsCasino.cardBackWhite, CardsCasino._18, CardsCasino._19, CardsCasino._14, CardsCasino._15, CardsCasino._16, CardsCasino._17, CardsCasino._10,
			CardsCasino._11, CardsCasino._12, CardsCasino._13, CardsCasino._5, CardsCasino._4, CardsCasino._7, CardsCasino._6, CardsCasino._1, CardsCasino.cardBackBlack, CardsCasino._0,
			CardsCasino._3, CardsCasino._2, CardsCasino._9, CardsCasino._8
		};
		PcIcons.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["arrow"] = PcIcons.arrow,
			["browser"] = PcIcons.browser,
			["config"] = PcIcons.config,
			["deepweb"] = PcIcons.deepweb,
			["load"] = PcIcons.load,
			["OnOff"] = PcIcons.OnOff,
			["save"] = PcIcons.save
		};
		PcIcons.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("arrow", "arrow")] = PcIcons.arrow,
			[("browser", "browser")] = PcIcons.browser,
			[("config", "config")] = PcIcons.config,
			[("deepweb", "deepweb")] = PcIcons.deepweb,
			[("load", "load")] = PcIcons.load,
			[("OnOff", "OnOff")] = PcIcons.OnOff,
			[("save", "save")] = PcIcons.save
		};
		PcIcons.All = new SerializableAutomaticReference<Sprite>[7] { PcIcons.arrow, PcIcons.browser, PcIcons.config, PcIcons.deepweb, PcIcons.load, PcIcons.OnOff, PcIcons.save };
		NewsImages.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["animal"] = NewsImages.animal,
			["army"] = NewsImages.army,
			["blackman"] = NewsImages.blackman,
			["brogan"] = NewsImages.brogan,
			["cartoon"] = NewsImages.cartoon,
			["cemetery"] = NewsImages.cemetery,
			["child"] = NewsImages.child,
			["computer"] = NewsImages.computer,
			["demonstration"] = NewsImages.demonstration,
			["demonstration2"] = NewsImages.demonstration2,
			["fat"] = NewsImages.fat,
			["happykid"] = NewsImages.happykid,
			["hotdog"] = NewsImages.hotdog,
			["island"] = NewsImages.island,
			["jumper"] = NewsImages.jumper,
			["OPanime"] = NewsImages.OPanime,
			["OPanimeboy"] = NewsImages.OPanimeboy,
			["OPblacks"] = NewsImages.OPblacks,
			["OPcar"] = NewsImages.OPcar,
			["OPchildandadult"] = NewsImages.OPchildandadult,
			["OPcompany"] = NewsImages.OPcompany,
			["OPcomputer2"] = NewsImages.OPcomputer2,
			["OPcooking"] = NewsImages.OPcooking,
			["OPdragqueen"] = NewsImages.OPdragqueen,
			["OPfemale"] = NewsImages.OPfemale,
			["OPfuneral"] = NewsImages.OPfuneral,
			["OPnaked"] = NewsImages.OPnaked,
			["OPpolice"] = NewsImages.OPpolice,
			["OPpolice2"] = NewsImages.OPpolice2,
			["OPpolice3"] = NewsImages.OPpolice3,
			["OPpolitician"] = NewsImages.OPpolitician,
			["OPpolitics"] = NewsImages.OPpolitics,
			["OPpoor"] = NewsImages.OPpoor,
			["OPpope"] = NewsImages.OPpope,
			["OPporn"] = NewsImages.OPporn,
			["OPsocialmedia"] = NewsImages.OPsocialmedia,
			["OPsport"] = NewsImages.OPsport,
			["OPsurgery"] = NewsImages.OPsurgery,
			["OPuni"] = NewsImages.OPuni,
			["pills"] = NewsImages.pills,
			["police"] = NewsImages.police,
			["police2"] = NewsImages.police2,
			["police3"] = NewsImages.police3,
			["policechild"] = NewsImages.policechild,
			["priest"] = NewsImages.priest,
			["protest"] = NewsImages.protest,
			["religion"] = NewsImages.religion,
			["religion2"] = NewsImages.religion2,
			["revolution"] = NewsImages.revolution,
			["ring"] = NewsImages.ring,
			["satellite"] = NewsImages.satellite,
			["sewer"] = NewsImages.sewer,
			["sex"] = NewsImages.sex,
			["soldiers"] = NewsImages.soldiers,
			["toilet"] = NewsImages.toilet,
			["virus"] = NewsImages.virus,
			["weapon"] = NewsImages.weapon
		};
		NewsImages.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("animal", "animal")] = NewsImages.animal,
			[("army", "army")] = NewsImages.army,
			[("blackman", "blackman")] = NewsImages.blackman,
			[("brogan", "brogan")] = NewsImages.brogan,
			[("cartoon", "cartoon")] = NewsImages.cartoon,
			[("cemetery", "cemetery")] = NewsImages.cemetery,
			[("child", "child")] = NewsImages.child,
			[("computer", "computer")] = NewsImages.computer,
			[("demonstration", "demonstration")] = NewsImages.demonstration,
			[("demonstration2", "demonstration2")] = NewsImages.demonstration2,
			[("fat", "fat")] = NewsImages.fat,
			[("happykid", "happykid")] = NewsImages.happykid,
			[("hotdog", "hotdog")] = NewsImages.hotdog,
			[("island", "island")] = NewsImages.island,
			[("jumper", "jumper")] = NewsImages.jumper,
			[("OPanime", "OPanime")] = NewsImages.OPanime,
			[("OPanimeboy", "OPanimeboy")] = NewsImages.OPanimeboy,
			[("OPblacks", "OPblacks")] = NewsImages.OPblacks,
			[("OPcar", "OPcar")] = NewsImages.OPcar,
			[("OPchildandadult", "OPchildandadult")] = NewsImages.OPchildandadult,
			[("OPcompany", "OPcompany")] = NewsImages.OPcompany,
			[("OPcomputer2", "OPcomputer2")] = NewsImages.OPcomputer2,
			[("OPcooking", "OPcooking")] = NewsImages.OPcooking,
			[("OPdragqueen", "OPdragqueen")] = NewsImages.OPdragqueen,
			[("OPfemale", "OPfemale")] = NewsImages.OPfemale,
			[("OPfuneral", "OPfuneral")] = NewsImages.OPfuneral,
			[("OPnaked", "OPnaked")] = NewsImages.OPnaked,
			[("OPpolice", "OPpolice")] = NewsImages.OPpolice,
			[("OPpolice2", "OPpolice2")] = NewsImages.OPpolice2,
			[("OPpolice3", "OPpolice3")] = NewsImages.OPpolice3,
			[("OPpolitician", "OPpolitician")] = NewsImages.OPpolitician,
			[("OPpolitics", "OPpolitics")] = NewsImages.OPpolitics,
			[("OPpoor", "OPpoor")] = NewsImages.OPpoor,
			[("OPpope", "OPpope")] = NewsImages.OPpope,
			[("OPporn", "OPporn")] = NewsImages.OPporn,
			[("OPsocialmedia", "OPsocialmedia")] = NewsImages.OPsocialmedia,
			[("OPsport", "OPsport")] = NewsImages.OPsport,
			[("OPsurgery", "OPsurgery")] = NewsImages.OPsurgery,
			[("OPuni", "OPuni")] = NewsImages.OPuni,
			[("pills", "pills")] = NewsImages.pills,
			[("police", "police")] = NewsImages.police,
			[("police2", "police2")] = NewsImages.police2,
			[("police3", "police3")] = NewsImages.police3,
			[("policechild", "policechild")] = NewsImages.policechild,
			[("priest", "priest")] = NewsImages.priest,
			[("protest", "protest")] = NewsImages.protest,
			[("religion", "religion")] = NewsImages.religion,
			[("religion2", "religion2")] = NewsImages.religion2,
			[("revolution", "revolution")] = NewsImages.revolution,
			[("ring", "ring")] = NewsImages.ring,
			[("satellite", "satellite")] = NewsImages.satellite,
			[("sewer", "sewer")] = NewsImages.sewer,
			[("sex", "sex")] = NewsImages.sex,
			[("soldiers", "soldiers")] = NewsImages.soldiers,
			[("toilet", "toilet")] = NewsImages.toilet,
			[("virus", "virus")] = NewsImages.virus,
			[("weapon", "weapon")] = NewsImages.weapon
		};
		NewsImages.All = new SerializableAutomaticReference<Sprite>[57]
		{
			NewsImages.animal, NewsImages.army, NewsImages.blackman, NewsImages.brogan, NewsImages.cartoon, NewsImages.cemetery, NewsImages.child, NewsImages.computer, NewsImages.demonstration, NewsImages.demonstration2,
			NewsImages.fat, NewsImages.happykid, NewsImages.hotdog, NewsImages.island, NewsImages.jumper, NewsImages.OPanime, NewsImages.OPanimeboy, NewsImages.OPblacks, NewsImages.OPcar, NewsImages.OPchildandadult,
			NewsImages.OPcompany, NewsImages.OPcomputer2, NewsImages.OPcooking, NewsImages.OPdragqueen, NewsImages.OPfemale, NewsImages.OPfuneral, NewsImages.OPnaked, NewsImages.OPpolice, NewsImages.OPpolice2, NewsImages.OPpolice3,
			NewsImages.OPpolitician, NewsImages.OPpolitics, NewsImages.OPpoor, NewsImages.OPpope, NewsImages.OPporn, NewsImages.OPsocialmedia, NewsImages.OPsport, NewsImages.OPsurgery, NewsImages.OPuni, NewsImages.pills,
			NewsImages.police, NewsImages.police2, NewsImages.police3, NewsImages.policechild, NewsImages.priest, NewsImages.protest, NewsImages.religion, NewsImages.religion2, NewsImages.revolution, NewsImages.ring,
			NewsImages.satellite, NewsImages.sewer, NewsImages.sex, NewsImages.soldiers, NewsImages.toilet, NewsImages.virus, NewsImages.weapon
		};
		GameLogo.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Sprite>>
		{
			["Logo B empty"] = GameLogo.Logo_B_empty,
			["Logo B small"] = GameLogo.Logo_B_small,
			["Logo C small"] = GameLogo.Logo_C_small
		};
		GameLogo.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Sprite>>
		{
			[("Logo B empty", "Logo B empty")] = GameLogo.Logo_B_empty,
			[("Logo B small", "Logo B small")] = GameLogo.Logo_B_small,
			[("Logo C small", "Logo C small")] = GameLogo.Logo_C_small
		};
		GameLogo.All = new SerializableAutomaticReference<Sprite>[3] { GameLogo.Logo_B_empty, GameLogo.Logo_B_small, GameLogo.Logo_C_small };
	}

	public void RegisterLua()
	{
		UserData.RegisterType<SpriteTextureDataGenerated>();
		UserData.RegisterType<AltBackgroundsContainer>();
		UserData.RegisterType<Sprite>();
		UserData.RegisterType<SerializableAutomaticReference<Sprite>>();
		UserData.RegisterType<FishingBackgroundsContainer>();
		UserData.RegisterType<PartnerLogosContainer>();
		UserData.RegisterType<OtherLogosContainer>();
		UserData.RegisterType<IconsContainer>();
		UserData.RegisterType<CardsMainContainer>();
		UserData.RegisterType<CardsCasinoContainer>();
		UserData.RegisterType<PcIconsContainer>();
		UserData.RegisterType<NewsImagesContainer>();
		UserData.RegisterType<GameLogoContainer>();
	}
}
