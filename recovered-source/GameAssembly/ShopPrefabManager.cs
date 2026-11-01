using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameItems;
using UnityEngine;

public static class ShopPrefabManager
{
	public class ShopPrefabManagerData : PrefabManagerDataOneToOne<string, ShopPrefab, Shop>
	{
		protected override ShopPrefab GenerateEmpty(string key)
		{
			return new ShopPrefab(key, "Error");
		}

		protected override Shop CreateSerializedPart(string key, ShopPrefab prefab)
		{
			return new Shop(key, prefab);
		}
	}

	private static ShopPrefabManagerData data = new ShopPrefabManagerData();

	public const string realmenfishingshopid = "realmenfishing";

	public const string bigdaddyhurtshopid = "ladyparts.ic";

	public const string groceryshopid = "grocery";

	public const string pharmacyshopid = "pharmacy";

	public const string clothiershopid = "clothier";

	public const string richpeopleshopid = "richpeopleshop";

	public const string notfoundshopid = "notfound";

	public static ShopPrefab GetShopPrefab(string name)
	{
		return data.Get(name);
	}

	public static bool TryGetShopPrefab(string shopId, out ShopPrefab shopPrefab)
	{
		return data.TryGet(shopId, out shopPrefab);
	}

	public static void EnsureShops(List<Shop> shops)
	{
		data.EnsureSerializedDataExistsAndHasPrefabs(shops);
	}

	[LoadableStaticMethod(new Type[] { typeof(ItemPrefab.ItemPrefabManager) })]
	private static UniTask Load()
	{
		List<ShopPrefab> list = new List<ShopPrefab>();
		list.Add(CreateBDHShopPrefab());
		list.Add(CreateGroceryShopPrefab());
		list.Add(CreatePharmacyShopPrefab());
		list.Add(CreateClothierShopPrefab());
		list.Add(CreateRealMenFishingShopPrefab());
		list.Add(CreateRichPeopleShopPrefab());
		data.Init(list);
		return UniTask.CompletedTask;
	}

	private static ShopPrefab CreateRichPeopleShopPrefab()
	{
		ShopPrefab shopPrefab = new ShopPrefab("richpeopleshop", LOC.UI.TupleReferences.Shop_RichPeopleShopName, 5);
		shopPrefab.GenericContainer.AddItems(new List<ItemEnum>
		{
			ItemEnum.Rice,
			ItemEnum.Potatoes,
			ItemEnum.Flour,
			ItemEnum.Tomato,
			ItemEnum.Carrot,
			ItemEnum.Steak,
			ItemEnum.ChickenMeat,
			ItemEnum.Onions,
			ItemEnum.Eggs,
			ItemEnum.Milk,
			ItemEnum.Garlic,
			ItemEnum.Ginger,
			ItemEnum.SoySauce,
			ItemEnum.Vinegar,
			ItemEnum.Cheese,
			ItemEnum.Butter,
			ItemEnum.RawGroundBeef,
			ItemEnum.Pasta,
			ItemEnum.Sugar,
			ItemEnum.Yoghurt
		});
		shopPrefab.SingleContainer.AddItems(() => GameVariables.Current.BathroomDoorInstalled, 1f, new List<ItemEnum> { ItemEnum.ColorfulSoap }.Select((ItemEnum x) => x.ToGameId()));
		return shopPrefab;
	}

	private static ShopPrefab CreateRealMenFishingShopPrefab()
	{
		ShopPrefab shopPrefab = new ShopPrefab("realmenfishing", LOC.UI.TupleReferences.FishingWebsite_Name);
		shopPrefab.SingleContainer.AddItems(null, 4f, ItemEnum.BasicFishingRod);
		shopPrefab.SingleContainer.AddItems(() => GameVariables.Current.fishingMinigameManager.FishCaughtCount >= 5 && GameVariables.Current.fishingMinigameManager.MaxCaughtFishSize > 2.0, 2f, ItemEnum.ReinforcedFishingRod);
		shopPrefab.SingleContainer.AddItems(() => GameVariables.Current.fishingMinigameManager.FishCaughtCount >= 10 && GameVariables.Current.fishingMinigameManager.MaxCaughtFishSize > 7.0, 1.5f, ItemEnum.FiberGlassFishingRod);
		shopPrefab.SingleContainer.AddItems(() => GameVariables.Current.fishingMinigameManager.FishCaughtCount >= 20 && GameVariables.Current.fishingMinigameManager.MaxCaughtFishSize > 20.0, 1f, ItemEnum.CarbonFiberFishingRod);
		shopPrefab.SingleContainer.AddItems(() => GameVariables.Current.fishingMinigameManager.FishCaughtCount >= 40 && GameVariables.Current.fishingMinigameManager.MaxCaughtFishSize > 70.0, 0.5f, ItemEnum.HyperSteelFishingRod);
		shopPrefab.SingleContainer.AddItems(ItemEnum.FishShirt);
		shopPrefab.GenericContainer.AddItems(ItemEnum.WormBait, ItemEnum.LarvaeBait, ItemEnum.MeatBait, ItemEnum.FishChunksBait, ItemEnum.CornBait, ItemEnum.ShrimpBait, ItemEnum.MusselsBait);
		return shopPrefab;
	}

	private static ShopPrefab CreateClothierShopPrefab()
	{
		ShopPrefab shopPrefab = new ShopPrefab("clothier", LOC.UI.TupleReferences.Shop_Annalie, 80);
		shopPrefab.SingleContainer.AddItems(ItemEnum.PlainBra, ItemEnum.HeartGlasses, ItemEnum.EyeShadow, ItemEnum.BaggyPants, ItemEnum.BaggyHoodie, ItemEnum.StockingPair, ItemEnum.PlainSneakerPair, ItemEnum.ShortSockPair, ItemEnum.Headband);
		shopPrefab.SingleContainer.AddItems(ItemEnum.PlainPanties);
		shopPrefab.SingleContainer.AddItems(null, 0.5f, ItemEnum.FishPanties, ItemEnum.BananaPanties, ItemEnum.YaranaikaPanties, ItemEnum.USBPanties, ItemEnum.WyldspacePanties, ItemEnum.SpecialHaremClassPanties, ItemEnum.RadioactivePanties);
		shopPrefab.SingleContainer.AddItems(ItemEnum.PlainShirt);
		shopPrefab.SingleContainer.AddItems(null, 0.3f, ItemEnum.SheepShirt, ItemEnum.SylphyChibiShirt, ItemEnum.DogeCoinShirt, ItemEnum.WithStupidShirt, ItemEnum.GamerShirt, ItemEnum.HotPinkShirt, ItemEnum.InHeatShirt, ItemEnum.DestinationDeliriumShirt, ItemEnum.SilumanShirt, ItemEnum.SilumanAliceShirt, ItemEnum.FungusShirt, ItemEnum.YaranaikaShirt, ItemEnum.BakaShirt, ItemEnum.CIAShirt, ItemEnum.PolandBallShirt, ItemEnum.WyldspaceShirt, ItemEnum.MirthalShirt, ItemEnum.AGUIShirt, ItemEnum.BedabotsShirt, ItemEnum.ProjektMelodyShirt, ItemEnum.XoulionShirt, ItemEnum.UfoShirt);
		shopPrefab.SingleContainer.AddItems(ItemEnum.BaggyHoodie);
		shopPrefab.SingleContainer.AddItems(null, 0.3f, ItemEnum.DogeCoinHoodie, ItemEnum.CosplayHouseHoodie, ItemEnum.PolandBallHoodie, ItemEnum.MilfHunterHoodie, ItemEnum.AGUIHoodie, ItemEnum.SpecialHaremClassHoodie, ItemEnum.BedabotsHoodie, ItemEnum.ProjektMelodyHoodie, ItemEnum.WorldTamerHoodie, ItemEnum.XoulionHoodie, ItemEnum.CelestynHoodie, ItemEnum.FlowerKidVHoodie, ItemEnum.NutekuHoodie, ItemEnum.SkullHoodie, ItemEnum.ToniSHoodie, ItemEnum.MadHoodie, ItemEnum.SJ68Hoodie, ItemEnum.LightSonicHoodie, ItemEnum.RottingSteelHoodie, ItemEnum.NitroriHoodie, ItemEnum.ShadyCornerHoodie);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("church"), 0.2f, ItemEnum.TempleShirt);
		shopPrefab.SingleContainer.AddItems(() => GameVariables.Current.followers > 500, 2f, ItemEnum.BotShirt);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("GoingOutside1"), 5f, ItemEnum.SneakerPair);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.sympathy > 100, 1f, ItemEnum.PleatedSkirt, ItemEnum.LongSockPair);
		Func<bool> condition = () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.sympathy > 170;
		shopPrefab.SingleContainer.AddItems(condition, 1f, ItemEnum.CuteBow, ItemEnum.StripedSockPair, ItemEnum.OvalGlasses);
		shopPrefab.SingleContainer.AddItems(condition, 1f, ItemEnum.StripedPanties, ItemEnum.StrawberryStripedPanties, ItemEnum.PeachStripedPanties, ItemEnum.AvocadoPanties, ItemEnum.CherryStripedPanties, ItemEnum.XoulionPanties, ItemEnum.HikkeiruPanties);
		shopPrefab.SingleContainer.AddItems(condition, 2f, ItemEnum.ClassyShoePair, ItemEnum.VintageDress);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.sympathy > 300, 1f, ItemEnum.ColoredOvalGlasses, ItemEnum.HairClip, ItemEnum.Eyeliner, ItemEnum.NaturalEyelashes, ItemEnum.BikiniBra, ItemEnum.BikiniPanties, ItemEnum.BikiniSet);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.sympathy > 500, 1f, ItemEnum.ColoredHeartGlasses, ItemEnum.Manicure, ItemEnum.HeartEyeSticker, ItemEnum.SimpleChoker);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lust > 175, 1f, ItemEnum.BuckleCollar, ItemEnum.HeartChoker, ItemEnum.GothEyeMakeup, ItemEnum.BloodyMoonHoodie, ItemEnum.KneeHighSockPair);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lust > 250, 1f, ItemEnum.SeptumPiercing, ItemEnum.SeptumPiercing2, ItemEnum.WeebHoodie, ItemEnum.Lipstick, ItemEnum.TwoStripedStockingPair);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lust > 300, 1f, ItemEnum.ColoredBuckleCollar, ItemEnum.MiniSkirt, ItemEnum.EyePatch, ItemEnum.SpikedCollar, ItemEnum.NavelPiercing);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lust > 500, 1f, ItemEnum.BellChoker, ItemEnum.TonguePiercing, ItemEnum.MiniSkirt, ItemEnum.EyePatch, ItemEnum.Pedicure, ItemEnum.NipplePiercingPair);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lust > 275 && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.sympathy > 210, 2f, ItemEnum.LingeriePanties, ItemEnum.LingerieBra, ItemEnum.LingerieStockingPair, ItemEnum.SlipDress, ItemEnum.StirrupStockingPair);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lust > 275 && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.sympathy > 500, 2f, ItemEnum.TransparentBikiniBra, ItemEnum.TransparentBikiniPanties, ItemEnum.TransparentBikiniSet);
		shopPrefab.SingleContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.HalloweenActive, 1f, ItemEnum.PointyEars, ItemEnum.TinyWizardHat, ItemEnum.PumpkinHairpin, ItemEnum.SpiderPlush, ItemEnum.PumpkinMat, ItemEnum.HyperCamoPointyEars);
		return shopPrefab;
	}

	private static ShopPrefab CreatePharmacyShopPrefab()
	{
		ShopPrefab shopPrefab = new ShopPrefab("pharmacy", LOC.UI.TupleReferences.Shop_Pharmacy);
		shopPrefab.GenericContainer.AddItems(new List<ItemEnum>
		{
			ItemEnum.SempillX,
			ItemEnum.Vinegara,
			ItemEnum.DeathGrip
		});
		shopPrefab.GenericContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Day > 7, 1f, ItemEnum.VinegaraPremium);
		shopPrefab.GenericContainer.AddItems(() => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("BlaubeimMedicine"), 1f, ItemEnum.Antidepressants);
		shopPrefab.PriceCalculateFunc = (ShopItem item, int i) =>
		{
			int num = ShopPrefab.DefaultPriceCalculateFunc(item, i);
			int num2 = Mathf.FloorToInt((float)num * GameVariables.Current.MedicineDiscount);
			return num - num2;
		};
		return shopPrefab;
	}

	private static ShopPrefab CreateGroceryShopPrefab()
	{
		ShopPrefab shopPrefab = new ShopPrefab("grocery", LOC.UI.TupleReferences.Shop_ConvenienceStore);
		shopPrefab.GenericContainer.AddItems(new List<ItemEnum>
		{
			ItemEnum.CanOfBakedBeans,
			ItemEnum.PickledOnions,
			ItemEnum.Sausage,
			ItemEnum.InstantNoodles,
			ItemEnum.EnergyDrink,
			ItemEnum.ProteinBar,
			ItemEnum.Chips,
			ItemEnum.Soda,
			ItemEnum.Sweets,
			ItemEnum.TacoShell,
			ItemEnum.GmoVegetables,
			ItemEnum.Soyrent,
			ItemEnum.ChickenNuggets
		});
		shopPrefab.GenericContainer.AddItems(() => GameVariables.Current.BathroomDoorInstalled, 1f, new List<ItemEnum> { ItemEnum.BasicSoap }.Select((ItemEnum x) => x.ToGameId()));
		return shopPrefab;
	}

	private static ShopPrefab CreateBDHShopPrefab()
	{
		ShopPrefab shopPrefab = new ShopPrefab("ladyparts.ic", LOC.UI.TupleReferences.Shop_bigdaddyhurtshop, 50);
		shopPrefab.SingleContainer.AddItems(new List<ItemEnum>
		{
			ItemEnum.CatEars,
			ItemEnum.BasicLegL,
			ItemEnum.BasicLegR,
			ItemEnum.BasicArmL,
			ItemEnum.BasicArmR,
			ItemEnum.HyperCamoLegL,
			ItemEnum.HyperCamoLegR,
			ItemEnum.HyperCamoArmL,
			ItemEnum.HyperCamoArmR,
			ItemEnum.SharpTeeth,
			ItemEnum.HyperCamoSharpTeeth,
			ItemEnum.HyperCamoSkin,
			ItemEnum.HyperCamoHair,
			ItemEnum.HyperCamoEyes,
			ItemEnum.HyperCamoFlesh,
			ItemEnum.HyperCamoCatEars,
			ItemEnum.HyperCamoPointyEars,
			ItemEnum.HightechHyperCamoSkin
		});
		shopPrefab.SingleContainer.AddItems(new List<ItemEnum>
		{
			ItemEnum.CatTail,
			ItemEnum.CatTailDifferentEnd,
			ItemEnum.HyperCamoCatTail,
			ItemEnum.HyperCamoCatTailDifferentEnd,
			ItemEnum.TailFluffy,
			ItemEnum.HyperCamoTailFluffy,
			ItemEnum.StripedTailFluffy,
			ItemEnum.HyperCamoStripedTailFluffy
		});
		shopPrefab.SingleContainer.AddItems(ItemEnum.HypercamoHimeCutHair, ItemEnum.HypercamoPonytailHair, ItemEnum.HypercamoEyeCoveringBangHair, ItemEnum.HologramDisable);
		shopPrefab.SingleContainer.AddItems(() => GameVariables.Current.BotMetAnnalie, 1f, ItemEnum.HypercamoClothierHair);
		shopPrefab.SingleContainer.AddItems(null, 0.1f, ItemEnum.ExperimentalArmL, ItemEnum.ExperimentalArmR, ItemEnum.ExperimentalLegR, ItemEnum.ExperimentalLegL);
		shopPrefab.SingleContainer.AddItems(null, 0.1f, ItemEnum.HypercamoExperimentalArmL, ItemEnum.HypercamoExperimentalArmR, ItemEnum.HypercamoExperimentalLegR, ItemEnum.HypercamoExperimentalLegL);
		Func<bool> condition = () =>
		{
			GameVariables current = GameVariables.Current;
			return current.sympathy > 200 && current.IsBotSmart && current.RiotsAfter;
		};
		shopPrefab.SingleContainer.AddItems(condition, 0.4f, ItemEnum.ThighSizeModule);
		shopPrefab.SingleContainer.AddItems(condition, 0.5f, ItemEnum.CalfSizeModule);
		shopPrefab.SingleContainer.AddItems(condition, 0.5f, ItemEnum.FeetSizeModule);
		shopPrefab.SingleContainer.AddItems(condition, 0.1f, ItemEnum.BoobSizeModule);
		shopPrefab.SingleContainer.AddItems(condition, 0.2f, ItemEnum.AssSizeModule);
		Func<bool> condition2 = () =>
		{
			GameVariables current = GameVariables.Current;
			return current.sympathy > 200 && current.IsBotSmart;
		};
		shopPrefab.SingleContainer.AddItems(condition2, 0.25f, ItemEnum.TsunTsunPersonalityModule, ItemEnum.CatgirlPersonalityModule, ItemEnum.RobotPersonalityModule, ItemEnum.YanderePersonalityModule, ItemEnum.SlutPersonalityModule, ItemEnum.FemcelPersonalityModule, ItemEnum.HimederePersonalityModule, ItemEnum.AraAraPersonalityModule);
		shopPrefab.SingleContainer.AddItems(() => GameVariables.Current.CheckFlag("botRaep"), 0.25f, ItemEnum.LibidoDestroyer);
		shopPrefab.SingleContainer.AddItems(() => GameVariables.Current.CheckFlag("libido_destroyer"), 0.25f, ItemEnum.LibidoAmplifier);
		shopPrefab.SingleContainer.AddItems(condition2, 0.001f, ItemEnum.DriverPersonalityModule);
		shopPrefab.SingleContainer.AddItems(condition2, 0.0001f, ItemEnum.SillyCatPersonalityModule);
		shopPrefab.SingleContainer.AddItems(null, 0.0001f, ItemEnum.RainbowHair);
		shopPrefab.SingleContainer.AddItems(null, 1E-05f, ItemEnum.RainbowSkin);
		shopPrefab.SingleContainer.AddItems(null, 1E-05f, ItemEnum.GalaxyHair);
		shopPrefab.SingleContainer.AddItems(null, 1E-06f, ItemEnum.FourthDimensionHair);
		return shopPrefab;
	}
}
