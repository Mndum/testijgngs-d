using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ColorPaletteManager
{
	public class ColorPalette
	{
		public readonly string Id;

		public string Name;

		public LOS.TupleReference LocalizationEntryReference;

		public bool ShouldBeVisible;

		private HashSet<ColorPaletteEntry> _entries;

		public IReadOnlyCollection<ColorPaletteEntry> Entries => _entries;

		private ColorPalette(string id, string name, HashSet<ColorPaletteEntry> entries)
		{
			Id = id;
			Name = name;
			_entries = entries;
			foreach (ColorPaletteEntry entry in entries)
			{
				Register(entry);
			}
			_allPalettes.Add(this);
		}

		[Obsolete]
		public static ColorPalette CreateInstance(string id, List<ColorPaletteEntry> entries)
		{
			return new ColorPalette(id, id, new HashSet<ColorPaletteEntry>(entries));
		}

		[Obsolete]
		public static ColorPalette CreateInstance(string id, params ColorPaletteEntry[] entries)
		{
			return new ColorPalette(id, id, new HashSet<ColorPaletteEntry>(entries));
		}

		public static ColorPalette CreateInstance(string id, string name, params ColorPaletteEntry[] entries)
		{
			return new ColorPalette(id, name, new HashSet<ColorPaletteEntry>(entries));
		}

		public ColorPaletteEntry GetRandomColor()
		{
			return _entries.GetRandom();
		}

		public void AddColor(ColorPaletteEntry color)
		{
			_entries.Add(color);
			Register(color);
		}

		private void Register(ColorPaletteEntry entry)
		{
			_ColorToColorEntry[entry.Color] = entry;
		}

		public string GetRawName()
		{
			return Name;
		}

		public string GetLocalizedName()
		{
			if (LocalizationEntryReference == null)
			{
				return Name;
			}
			return LocalizationEntryReference.GetWithCustomFallback(Name);
		}

		public override string ToString()
		{
			return "ColorPalette-\"" + Id + "\"";
		}
	}

	public class ColorPaletteEntry
	{
		private string Name;

		public LOS.TupleReference LocalizationEntryReference;

		public Color32 Color { get; private set; }

		private ColorPaletteEntry(string name, Color32 color)
		{
			Name = name;
			Color = color.SetAlpha32(255);
		}

		public static ColorPaletteEntry CreateInstance(string name, Color32 color)
		{
			return new ColorPaletteEntry(name, color);
		}

		public static ColorPaletteEntry CreateInstanceFromString(string str)
		{
			string[] array = str.Split('#');
			if (array.Length == 2)
			{
				string name = array[0].Trim();
				if (ColorUtility.TryParseHtmlString("#" + array[1].Trim(), out var color))
				{
					return new ColorPaletteEntry(name, color);
				}
				Debug.LogError(str + " wrong format of hex color \"#" + array[1] + "\"!");
				return new ColorPaletteEntry(name, UnityEngine.Color.white);
			}
			Debug.LogError(str + " wrong format to be a color palette entry!");
			return new ColorPaletteEntry("ERROR", UnityEngine.Color.white);
		}

		public static implicit operator Color32(ColorPaletteEntry c)
		{
			return c.Color;
		}

		public static implicit operator Color(ColorPaletteEntry c)
		{
			return c.Color;
		}

		public string GetRawName()
		{
			return Name;
		}

		public string GetLocalizedName()
		{
			if (LocalizationEntryReference == null)
			{
				return Name;
			}
			return LocalizationEntryReference.GetWithCustomFallback(Name);
		}
	}

	private static readonly List<ColorPalette> _allPalettes;

	private static readonly Dictionary<Color32, ColorPaletteEntry> _ColorToColorEntry;

	public static readonly ColorPaletteEntry OldPlainWhite;

	public static readonly ColorPaletteEntry OldSadGrey;

	public static readonly ColorPaletteEntry OldCuteMagenta;

	public static readonly ColorPaletteEntry OldUglyBlue;

	public static readonly ColorPaletteEntry OldDreamyCyan;

	public static readonly ColorPaletteEntry OldGrassyGreen;

	public static readonly ColorPaletteEntry OldFakeGold;

	public static readonly ColorPaletteEntry OldBloodyRed;

	public static readonly ColorPaletteEntry OldSpecialTurquoise;

	public static readonly ColorPaletteEntry OldToxicGreen;

	public static readonly ColorPaletteEntry OldKitsunePiku;

	public static readonly ColorPaletteEntry OldAsphalt;

	public static readonly ColorPalette OldPartPalette;

	public static readonly ColorPaletteEntry JunsGrey;

	public static readonly ColorPaletteEntry JunsMagenta;

	public static readonly ColorPaletteEntry JunsPupils;

	public static readonly ColorPaletteEntry JunsIrises;

	public static readonly ColorPaletteEntry DefaultSkin;

	public static readonly ColorPaletteEntry JunsFlesh;

	public static readonly ColorPaletteEntry JunsBlush;

	public static readonly ColorPalette JunPalette;

	public static readonly ColorPaletteEntry White;

	public static readonly ColorPaletteEntry LightGrey;

	public static readonly ColorPaletteEntry Grey;

	public static readonly ColorPaletteEntry DarkGrey;

	public static readonly ColorPaletteEntry VeryDarkGrey;

	public static readonly ColorPaletteEntry Black;

	public static readonly ColorPalette GrayScalePalette;

	public static readonly ColorPaletteEntry UnicornGalaxy;

	public static readonly ColorPaletteEntry CuteMagenta;

	public static readonly ColorPaletteEntry StrawberyMilk;

	public static readonly ColorPaletteEntry ToxicPurple;

	public static readonly ColorPaletteEntry MidnightBlue;

	public static readonly ColorPaletteEntry BasicBlack;

	public static readonly ColorPaletteEntry BasicWhite;

	public static readonly ColorPaletteEntry BloodyRed;

	public static readonly ColorPaletteEntry Miku;

	public static readonly ColorPaletteEntry FakeGold;

	public static readonly ColorPaletteEntry DreamyCyan;

	public static readonly ColorPaletteEntry UglyBlue;

	public static readonly ColorPaletteEntry EBPurple;

	public static readonly ColorPaletteEntry EBBlue;

	public static readonly ColorPaletteEntry EBGreen;

	public static readonly ColorPaletteEntry EBYellow;

	public static readonly ColorPaletteEntry EBOrange;

	public static readonly ColorPaletteEntry EBRed;

	public static readonly ColorPaletteEntry Bastille;

	public static readonly ColorPaletteEntry Mirage;

	public static readonly ColorPaletteEntry Toledo;

	public static readonly ColorPaletteEntry Bunker;

	public static readonly ColorPaletteEntry HippiePink;

	public static readonly ColorPaletteEntry LinkWater;

	public static readonly ColorPaletteEntry TurquoiseBlue;

	public static readonly ColorPaletteEntry Amethyst;

	public static readonly ColorPaletteEntry Fern;

	public static readonly ColorPaletteEntry DarkPink;

	public static readonly ColorPaletteEntry AnnaliesViolet;

	public static readonly ColorPalette DefaultPartPalette;

	public static readonly ColorPaletteEntry VolksAutoGrey;

	public static readonly ColorPaletteEntry NavyGreen;

	public static readonly ColorPaletteEntry TempleCyan;

	public static readonly ColorPaletteEntry AvocadoGreen;

	public static readonly ColorPaletteEntry LunarPrincessGrey;

	public static readonly ColorPaletteEntry SatinBlack;

	public static readonly ColorPaletteEntry StilouBlue;

	public static readonly ColorPaletteEntry StilouRed;

	public static readonly ColorPaletteEntry StilouYellow;

	public static readonly ColorPaletteEntry MonizmedBurgundy;

	public static readonly ColorPaletteEntry MonizmedPurple;

	public static readonly ColorPaletteEntry DiabetePurple;

	public static readonly ColorPaletteEntry DiabeteGold;

	public static readonly ColorPaletteEntry DiabeteColaRed;

	public static readonly ColorPaletteEntry DiabeteColaYellow;

	public static readonly ColorPaletteEntry DiabeteColaBlue;

	public static readonly ColorPaletteEntry DiabeteDrSugarRed;

	public static readonly ColorPaletteEntry DiabeteDrSugarBlue;

	public static readonly ColorPaletteEntry DiabeteSweetPotatoBrown;

	public static readonly ColorPaletteEntry DiabeteSweetPotatoOrange;

	public static readonly ColorPaletteEntry CazinoGreen;

	public static readonly ColorPaletteEntry CazinoBrown;

	public static readonly ColorPalette OtherPalette;

	public static readonly ColorPaletteEntry EbonyClay;

	public static readonly ColorPaletteEntry Rope;

	public static readonly ColorPaletteEntry Leather;

	public static readonly ColorPaletteEntry CopperRust;

	public static readonly ColorPaletteEntry MellowApricot;

	public static readonly ColorPaletteEntry InkBlue;

	public static readonly ColorPaletteEntry DarkLeather;

	public static readonly ColorPaletteEntry BlackLeather;

	public static readonly ColorPaletteEntry WeatheredGray;

	public static readonly ColorPaletteEntry Burgundy;

	public static readonly ColorPalette LeatherPalette;

	public static readonly ColorPaletteEntry DarkUmber;

	public static readonly ColorPaletteEntry Sienna;

	public static readonly ColorPaletteEntry Sandstone;

	public static readonly ColorPaletteEntry Almond;

	public static readonly ColorPaletteEntry LightPeach;

	public static readonly ColorPaletteEntry DeepMahogany;

	public static readonly ColorPaletteEntry Bronze;

	public static readonly ColorPaletteEntry GoldenHoney;

	public static readonly ColorPaletteEntry WarmIvory;

	public static readonly ColorPaletteEntry PalePorcelain;

	public static readonly ColorPaletteEntry GoblinGreen;

	public static readonly ColorPaletteEntry SoftClay;

	public static readonly ColorPaletteEntry Rosewood;

	public static readonly ColorPaletteEntry Tawny;

	public static readonly ColorPaletteEntry BurntSienna;

	public static readonly ColorPaletteEntry EarthBrown;

	public static readonly ColorPaletteEntry PaleDoomer;

	public static readonly ColorPalette SkinTonePalette;

	public static readonly ColorPalette HotPinkPalette;

	public static readonly ColorPalette XoulionMainPalette;

	public static readonly ColorPalette XoulionLogoPalette;

	private static string LocalizationPrefixColor;

	private static string LocalizationPrefixPalette;

	public static IEnumerable<ColorPaletteEntry> AllColors => _ColorToColorEntry.Values;

	public static IEnumerable<ColorPalette> AllPalettes => _allPalettes;

	public static IEnumerable<ColorPalette> AllVisiblePalettes => _allPalettes.Where((ColorPalette x) => x.ShouldBeVisible);

	public static ColorPalette GetColorPaletteByName(string name)
	{
		ColorPalette colorPalette = _allPalettes.FirstOrDefault((ColorPalette t) => t.Id == name);
		if (colorPalette == null)
		{
			Debug.LogWarning("Color pallete \"" + name + "\" not found. Using DefaultPartPalette");
		}
		return colorPalette;
	}

	public static bool TryFindEntry(Color32 color, out ColorPaletteEntry colorPaletteEntry, bool ignoreTransparency = true)
	{
		if (ignoreTransparency)
		{
			color.a = 255;
		}
		return _ColorToColorEntry.TryGetValue(color, out colorPaletteEntry);
	}

	public static string FindName(Color32 color)
	{
		if (TryFindEntry(color, out var colorPaletteEntry))
		{
			return colorPaletteEntry.GetLocalizedName();
		}
		return LOC.Other.ColorName_Custom;
	}

	static ColorPaletteManager()
	{
		_allPalettes = new List<ColorPalette>();
		_ColorToColorEntry = new Dictionary<Color32, ColorPaletteEntry>();
		OldPlainWhite = ColorPaletteEntry.CreateInstance("Old Plain White", Color.white);
		OldSadGrey = ColorPaletteEntry.CreateInstance("Old Sad Grey", new Color32(125, 125, 125, 125));
		OldCuteMagenta = ColorPaletteEntry.CreateInstance("Old Cute Magenta", new Color32(255, 0, 255, 255));
		OldUglyBlue = ColorPaletteEntry.CreateInstance("Old Ugly blue", new Color32(0, 0, 255, 255));
		OldDreamyCyan = ColorPaletteEntry.CreateInstance("Old Dreamy cyan", new Color32(0, 255, 255, 255));
		OldGrassyGreen = ColorPaletteEntry.CreateInstance("Old Grassy green", new Color32(56, 115, 0, 255));
		OldFakeGold = ColorPaletteEntry.CreateInstance("Old Fake gold", new Color32(255, 219, 59, 255));
		OldBloodyRed = ColorPaletteEntry.CreateInstance("Old Bloody red", new Color32(255, 10, 50, 255));
		OldSpecialTurquoise = ColorPaletteEntry.CreateInstance("Old Special Turquoise", new Color32(23, 255, 198, 255));
		OldToxicGreen = ColorPaletteEntry.CreateInstance("Old Toxic Green", new Color32(131, 255, 23, 255));
		OldKitsunePiku = ColorPaletteEntry.CreateInstance("Old Kitsune Piku Blue", new Color32(52, 165, 143, 255));
		OldAsphalt = ColorPaletteEntry.CreateInstance("Old Asphalt", Color.black);
		JunsGrey = ColorPaletteEntry.CreateInstance("Jun's Grey", new Color32(80, 80, 80, 255));
		JunsMagenta = ColorPaletteEntry.CreateInstance("Jun's Magenta", new Color32(238, 0, 85, 255));
		JunsPupils = ColorPaletteEntry.CreateInstance("Jun's Pupils", (Color)new Color32(223, 157, 0, 255) * 0.8f);
		JunsIrises = ColorPaletteEntry.CreateInstance("Jun's Irises", (Color)new Color32(83, 83, 83, 255));
		DefaultSkin = ColorPaletteEntry.CreateInstance("Default Skin", new Color32(255, 240, 236, 255));
		JunsFlesh = ColorPaletteEntry.CreateInstance("Jun's Flesh", new Color32(255, 182, 207, 255));
		JunsBlush = ColorPaletteEntry.CreateInstance("Jun's Blush", new Color32(222, 93, 131, 255));
		White = ColorPaletteEntry.CreateInstance("White", Color.white);
		LightGrey = ColorPaletteEntry.CreateInstance("Light Grey", new Color(0.8f, 0.8f, 0.8f));
		Grey = ColorPaletteEntry.CreateInstance("Grey", new Color(0.6f, 0.6f, 0.6f));
		DarkGrey = ColorPaletteEntry.CreateInstance("Dark Grey", new Color(0.4f, 0.4f, 0.4f));
		VeryDarkGrey = ColorPaletteEntry.CreateInstance("Very Dark Grey", new Color(0.2f, 0.2f, 0.2f));
		Black = ColorPaletteEntry.CreateInstance("Black", new Color(0.1f, 0.1f, 0.1f));
		UnicornGalaxy = ColorPaletteEntry.CreateInstanceFromString("Unicorn galaxy #EB9DF6");
		CuteMagenta = ColorPaletteEntry.CreateInstanceFromString("Cute Magenta #FF6CC3");
		StrawberyMilk = ColorPaletteEntry.CreateInstanceFromString("Strawberry Milk #FF92C0");
		ToxicPurple = ColorPaletteEntry.CreateInstanceFromString("Toxic Purple #A45FBC");
		MidnightBlue = ColorPaletteEntry.CreateInstanceFromString("Midnight Blue #34445C");
		BasicBlack = ColorPaletteEntry.CreateInstanceFromString("Basic Black #242424");
		BasicWhite = ColorPaletteEntry.CreateInstanceFromString("Basic White #FCFBFB");
		BloodyRed = ColorPaletteEntry.CreateInstanceFromString("Bloody Red #832746");
		Miku = ColorPaletteEntry.CreateInstanceFromString("Miku #47E0CC");
		FakeGold = ColorPaletteEntry.CreateInstanceFromString("Fake Gold #D0A04F");
		DreamyCyan = ColorPaletteEntry.CreateInstanceFromString("Dreamy Cyan #8DF0F3");
		UglyBlue = ColorPaletteEntry.CreateInstanceFromString("Ugly Blue #4574CC");
		EBPurple = ColorPaletteEntry.CreateInstanceFromString("Eyes-blinding Purple #F014FF");
		EBBlue = ColorPaletteEntry.CreateInstanceFromString("Eyes-blinding Blue #20E2FF");
		EBGreen = ColorPaletteEntry.CreateInstanceFromString("Eyes-blinding Green #4BFF3B");
		EBYellow = ColorPaletteEntry.CreateInstanceFromString("Eyes-blinding Yellow #FFF800");
		EBOrange = ColorPaletteEntry.CreateInstanceFromString("Eyes-blinding Orange #FF8E38");
		EBRed = ColorPaletteEntry.CreateInstanceFromString("Eyes-blinding Red #FF2061");
		Bastille = ColorPaletteEntry.CreateInstance("Bastille", new Color32(44, 30, 43, 255));
		Mirage = ColorPaletteEntry.CreateInstance("Mirage", new Color32(31, 30, 43, 255));
		Toledo = ColorPaletteEntry.CreateInstance("Toledo", new Color32(47, 29, 38, 255));
		Bunker = ColorPaletteEntry.CreateInstance("Bunker", new Color32(25, 27, 32, 255));
		HippiePink = ColorPaletteEntry.CreateInstance("Hippie Pink", new Color32(192, 80, 107, 255));
		LinkWater = ColorPaletteEntry.CreateInstance("Link Water", new Color32(210, 215, 229, 255));
		TurquoiseBlue = ColorPaletteEntry.CreateInstance("Turquoise Blue", new Color32(69, 240, 253, 255));
		Amethyst = ColorPaletteEntry.CreateInstance("Amethyst", new Color32(154, 106, 202, 255));
		Fern = ColorPaletteEntry.CreateInstance("Fern", new Color32(111, 205, 110, 255));
		DarkPink = ColorPaletteEntry.CreateInstance("Dark Pink", new Color32(230, 93, 153, 255));
		AnnaliesViolet = ColorPaletteEntry.CreateInstance("Annalie's Purple", new Color32(107, 71, 119, 255));
		VolksAutoGrey = ColorPaletteEntry.CreateInstance("VolksAuto Grau™", new Color32(115, 122, 130, 255));
		NavyGreen = ColorPaletteEntry.CreateInstance("Navy Green", new Color32(55, 90, 12, 255));
		TempleCyan = ColorPaletteEntry.CreateInstance("Temple Cyan", new Color32(85, 255, 255, 255));
		AvocadoGreen = ColorPaletteEntry.CreateInstance("Avocado Green", new Color32(86, 130, 3, 255));
		LunarPrincessGrey = ColorPaletteEntry.CreateInstance("Lunar Princess Grey", new Color32(10, 19, 23, 255));
		SatinBlack = ColorPaletteEntry.CreateInstance("Satin Black", new Color32(15, 7, 1, 255));
		StilouBlue = ColorPaletteEntry.CreateInstance("Stilou Blue", new Color32(2, 51, 115, 255));
		StilouRed = ColorPaletteEntry.CreateInstance("Stilou Red", new Color32(217, 20, 56, 255));
		StilouYellow = ColorPaletteEntry.CreateInstance("Stilou Yellow", new Color32(242, 187, 22, 255));
		MonizmedBurgundy = ColorPaletteEntry.CreateInstance("Monizmed Burgundy", new Color32(115, 38, 66, 255));
		MonizmedPurple = ColorPaletteEntry.CreateInstance("Monizmed Purple", new Color32(31, 7, 64, 255));
		DiabetePurple = ColorPaletteEntry.CreateInstance("Diabete Purple", new Color32(99, 51, 166, 255));
		DiabeteGold = ColorPaletteEntry.CreateInstance("Diabete Gold", new Color32(254, 194, 0, 255));
		DiabeteColaRed = ColorPaletteEntry.CreateInstance("Diabete Cola Red", new Color32(242, 5, 5, 255));
		DiabeteColaYellow = ColorPaletteEntry.CreateInstance("Diabete Cola Yellow", new Color32(242, 226, 5, 255));
		DiabeteColaBlue = ColorPaletteEntry.CreateInstance("Diabete Cola Blue", new Color32(4, 15, 217, 255));
		DiabeteDrSugarRed = ColorPaletteEntry.CreateInstance("Diabete Dr. Sugar Red", new Color32(242, 25, 5, 255));
		DiabeteDrSugarBlue = ColorPaletteEntry.CreateInstance("Diabete Dr. Sugar Blue", new Color32(7, 157, 217, 255));
		DiabeteSweetPotatoBrown = ColorPaletteEntry.CreateInstance("Diabete Sweet Potato Brown", new Color32(89, 34, 3, 255));
		DiabeteSweetPotatoOrange = ColorPaletteEntry.CreateInstance("Diabete Sweet Potato Orange", new Color32(242, 92, 5, 255));
		CazinoGreen = ColorPaletteEntry.CreateInstance("Cazino Green", new Color32(33, 122, 38, 255));
		CazinoBrown = ColorPaletteEntry.CreateInstance("Cazino Brown", new Color32(131, 94, 41, 255));
		EbonyClay = ColorPaletteEntry.CreateInstance("Dark Brown Leather", new Color32(50, 37, 29, 255));
		Rope = ColorPaletteEntry.CreateInstance("Brown Leather", new Color32(79, 53, 37, 255));
		Leather = ColorPaletteEntry.CreateInstance("Normal Leather", new Color32(100, 67, 46, 255));
		CopperRust = ColorPaletteEntry.CreateInstance("Light Leather", new Color32(124, 85, 59, 255));
		MellowApricot = ColorPaletteEntry.CreateInstance("Apricot Leather", new Color32(188, 122, 79, 255));
		InkBlue = ColorPaletteEntry.CreateInstance("Ink Blue Leather", new Color32(30, 39, 54, 255));
		DarkLeather = ColorPaletteEntry.CreateInstance("Dark Leather", new Color32(35, 35, 30, 255));
		BlackLeather = ColorPaletteEntry.CreateInstance("Black Leather", new Color32(23, 20, 23, 255));
		WeatheredGray = ColorPaletteEntry.CreateInstance("Gray Leather", new Color32(88, 88, 88, 255));
		Burgundy = ColorPaletteEntry.CreateInstance("Burgundy Leather", new Color32(87, 10, 14, 255));
		DarkUmber = ColorPaletteEntry.CreateInstance("Dark Umber", new Color32(141, 85, 36, 255));
		Sienna = ColorPaletteEntry.CreateInstance("Sienna", new Color32(198, 134, 66, 255));
		Sandstone = ColorPaletteEntry.CreateInstance("Sandstone", new Color32(224, 172, 105, 255));
		Almond = ColorPaletteEntry.CreateInstance("Almond", new Color32(241, 194, 125, 255));
		LightPeach = ColorPaletteEntry.CreateInstance("Light Peach", new Color32(255, 219, 172, 255));
		DeepMahogany = ColorPaletteEntry.CreateInstance("Deep Mahogany", new Color32(92, 53, 27, 255));
		Bronze = ColorPaletteEntry.CreateInstance("Bronze", new Color32(163, 113, 64, 255));
		GoldenHoney = ColorPaletteEntry.CreateInstance("Golden Honey", new Color32(215, 158, 85, 255));
		WarmIvory = ColorPaletteEntry.CreateInstance("Warm Ivory", new Color32(242, 206, 170, 255));
		PalePorcelain = ColorPaletteEntry.CreateInstance("Pale Porcelain", new Color32(255, 230, 198, 255));
		GoblinGreen = ColorPaletteEntry.CreateInstance("Goblin Green", new Color32(108, 142, 67, 255));
		SoftClay = ColorPaletteEntry.CreateInstance("Soft Clay", new Color32(213, 163, 145, 255));
		Rosewood = ColorPaletteEntry.CreateInstance("Rosewood", new Color32(188, 145, 123, 255));
		Tawny = ColorPaletteEntry.CreateInstance("Tawny", new Color32(127, 88, 77, 255));
		BurntSienna = ColorPaletteEntry.CreateInstance("Burnt Sienna", new Color32(99, 68, 61, 255));
		EarthBrown = ColorPaletteEntry.CreateInstance("Earth Brown", new Color32(92, 65, 56, 255));
		PaleDoomer = ColorPaletteEntry.CreateInstance("Pale Doomer", new Color32(235, 235, 229, 255));
		LocalizationPrefixColor = "ColorName_";
		LocalizationPrefixPalette = "ColorPalette_";
		OldPartPalette = ColorPalette.CreateInstance("oldpartpalette", "Old Parts Palette", OldPlainWhite, OldSadGrey, OldCuteMagenta, OldUglyBlue, OldDreamyCyan, OldGrassyGreen, OldFakeGold, OldBloodyRed, OldSpecialTurquoise, OldToxicGreen, OldKitsunePiku, OldAsphalt);
		OldPartPalette.ShouldBeVisible = false;
		DefaultPartPalette = ColorPalette.CreateInstance("partpalette", "Parts Palette", UnicornGalaxy, CuteMagenta, StrawberyMilk, ToxicPurple, MidnightBlue, BasicBlack, BasicWhite, BloodyRed, Miku, FakeGold, DreamyCyan, UglyBlue, EBPurple, EBBlue, EBGreen, EBYellow, EBOrange, EBRed, JunsGrey, DefaultSkin, AnnaliesViolet, Bastille, Mirage, Toledo, Bunker, HippiePink, LinkWater, TurquoiseBlue, Amethyst, Fern, DarkPink);
		DefaultPartPalette.ShouldBeVisible = true;
		JunPalette = ColorPalette.CreateInstance("junpalette", "Jun's Palette", JunsGrey, JunsMagenta, JunsPupils, JunsIrises, DefaultSkin, JunsFlesh, JunsBlush);
		JunPalette.ShouldBeVisible = true;
		SkinTonePalette = ColorPalette.CreateInstance("skinTonePalette", "Skin Tone Palette", DefaultSkin, DarkUmber, Sienna, Sandstone, Almond, LightPeach, DeepMahogany, Bronze, GoldenHoney, WarmIvory, PalePorcelain, GoblinGreen, SoftClay, Rosewood, Tawny, BurntSienna, EarthBrown, PaleDoomer);
		SkinTonePalette.ShouldBeVisible = true;
		GrayScalePalette = ColorPalette.CreateInstance("grayscalepalette", "Grayscale Palette", White, LightGrey, Grey, DarkGrey, VeryDarkGrey, Black);
		GrayScalePalette.ShouldBeVisible = true;
		OtherPalette = ColorPalette.CreateInstance("otherpalette", "Brand's Palette", VolksAutoGrey, NavyGreen, AvocadoGreen, TempleCyan, LunarPrincessGrey, SatinBlack, StilouBlue, StilouRed, StilouYellow, MonizmedBurgundy, MonizmedPurple, DiabetePurple, DiabeteGold, DiabeteColaRed, DiabeteColaYellow, DiabeteColaBlue, DiabeteDrSugarRed, DiabeteDrSugarBlue, DiabeteSweetPotatoBrown, DiabeteSweetPotatoOrange, CazinoGreen, CazinoBrown);
		OtherPalette.ShouldBeVisible = true;
		HotPinkPalette = ColorPalette.CreateInstance("hotmilkpalette", "Hot Pink Palette", BasicBlack, StrawberyMilk, White);
		XoulionMainPalette = ColorPalette.CreateInstance("xoulionMainPalette", "Xoulion's Main Palette", Bastille, Mirage, Toledo, Bunker);
		XoulionLogoPalette = ColorPalette.CreateInstance("xoulionLogoPalette", "Xoulion's Logo Palette", HippiePink, LinkWater, TurquoiseBlue, Amethyst, Fern, DarkPink);
		LeatherPalette = ColorPalette.CreateInstance("leatherPalette", "Leather Palette", EbonyClay, Rope, Leather, CopperRust, MellowApricot, InkBlue, DarkLeather, BlackLeather, WeatheredGray, Burgundy);
		LeatherPalette.ShouldBeVisible = true;
		foreach (var allColorPaletteEntry in GetAllColorPaletteEntries())
		{
			allColorPaletteEntry.Item2.LocalizationEntryReference = new LOS.TupleReference(LOC.Other.TABLE_REFERENCE, LocalizationPrefixColor + allColorPaletteEntry.fieldName);
		}
		foreach (var allColorPalette in GetAllColorPalettes())
		{
			allColorPalette.Item2.LocalizationEntryReference = new LOS.TupleReference(LOC.Other.TABLE_REFERENCE, LocalizationPrefixPalette + allColorPalette.fieldName);
		}
	}

	private static IEnumerable<(string fieldName, ColorPaletteEntry)> GetAllColorPaletteEntries()
	{
		return from x in typeof(ColorPaletteManager).GetFields(BindingFlags.Static | BindingFlags.Public)
			where x.FieldType == typeof(ColorPaletteEntry)
			select (fieldName: x.Name, entry: (ColorPaletteEntry)x.GetValue(null));
	}

	private static IEnumerable<(string fieldName, ColorPalette)> GetAllColorPalettes()
	{
		return from x in typeof(ColorPaletteManager).GetFields(BindingFlags.Static | BindingFlags.Public)
			where x.FieldType == typeof(ColorPalette)
			select (fieldName: x.Name, entry: (ColorPalette)x.GetValue(null));
	}
}
