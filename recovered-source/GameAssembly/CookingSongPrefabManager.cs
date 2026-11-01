using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameItems;
using Melanchall.DryWetMidi.Interaction;

public static class CookingSongPrefabManager
{
	private static PrefabManagerDataOneToOneLazy<GameId, CookingSong, CookingSongSave>.Prefabs cookingSongsData = new PrefabManagerDataOneToOneLazy<GameId, CookingSong, CookingSongSave>.Prefabs();

	public static IEnumerable<CookingSong> GetAllVisibleSongs()
	{
		return cookingSongsData.AllPrefabs.Where((CookingSong x) => !x.HideIfLocked || x.IsUnlocked);
	}

	public static bool TryGetSongById(GameId key, out CookingSong cookingSong)
	{
		return cookingSongsData.TryGet(key, out cookingSong);
	}

	[LoadableStaticMethod(new Type[] { typeof(ItemPrefab.ItemPrefabManager) })]
	private static UniTask Load()
	{
		Recipe recipe = Recipe.CreateInstance(ItemEnum.BeansOnDispenserBread.ToGameId());
		recipe.AddIngredientExact(ItemEnum.CanOfBakedBeans);
		recipe.AddIngredientExact(ItemEnum.BreadFromDispenser);
		CookingSong prefab = CookingSong.CreateInstance(CookingSongEnumGI.Spirasomnus.ToGameId(), R.Music.Room_Night_Spirasomnus, (CookingSong song) => R.Midi.Nitrori_Spirasomnus.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(6.4f)
			.WithSongOffset(-0.05000000074505806)
			.Build(), recipe);
		cookingSongsData.Add(prefab);
		Recipe recipe2 = Recipe.CreateInstance(ItemEnum.PickledOnionsWithSausage.ToGameId());
		recipe2.AddIngredientExact(ItemEnum.PickledOnions);
		recipe2.AddIngredientExact(ItemEnum.Sausage);
		CookingSong prefab2 = CookingSong.CreateInstance(CookingSongEnumGI.IfOnly.ToGameId(), R.Music.Cuddle_If_Only, (CookingSong song) => R.Midi.RadPechi_IfOnly.ToCookingSongRuntimeDataBuilder(song).ChannelToNotes(0, (CookingSongRuntimeData.Builder builder, int _, Note note) => (TimeConverter.ConvertTo<BarBeatTicksTimeSpan>(note.Time, builder.TempoMap).Beats != 0L) ? NoteButtonTypeEnum.Left : NoteButtonTypeEnum.Right, ignoreLength: true).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(4f)
			.Build(), recipe2);
		cookingSongsData.Add(prefab2);
		Recipe recipe3 = Recipe.CreateInstance(ItemEnum.LandlordsTaco.ToGameId());
		recipe3.AddIngredientExact(ItemEnum.TacoShell);
		recipe3.AddIngredientExact(ItemEnum.PickledOnions);
		recipe3.AddIngredientExact(ItemEnum.Sausage);
		recipe3.AddIngredientExact(ItemEnum.GmoVegetables);
		CookingSong prefab3 = CookingSong.CreateInstance(CookingSongEnumGI.ItsHimAgain.ToGameId(), R.Music.Themes_Landlord_It_s_Him_Again, (CookingSong song) => R.Midi.RadPechi_ItsHimAgain.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.Build(), recipe3, (CookingMinigameManager manager) => manager.PassedSongsCount >= 1);
		cookingSongsData.Add(prefab3);
		Func<CookingMinigameManager, bool> isUnlockedFunc = (CookingMinigameManager manager) => manager.PassedSongsCount >= 3;
		Func<CookingMinigameManager, bool> isUnlockedFunc2 = (CookingMinigameManager manager) => manager.PassedSongsCount >= 4 && manager.GetAtLeastGradeCount(CookingGrade.B) >= 1;
		Func<CookingMinigameManager, bool> isUnlockedFunc3 = (CookingMinigameManager manager) => manager.PassedSongsCount >= 6 && manager.GetAtLeastGradeCount(CookingGrade.B) >= 2;
		Func<CookingMinigameManager, bool> isUnlockedFunc4 = (CookingMinigameManager manager) => manager.PassedSongsCount >= 8 && manager.GetAtLeastGradeCount(CookingGrade.B) >= 2;
		Func<CookingMinigameManager, bool> isUnlockedFunc5 = (CookingMinigameManager manager) => manager.PassedSongsCount >= 10 && manager.GetAtLeastGradeCount(CookingGrade.B) >= 3;
		Func<CookingMinigameManager, bool> isUnlockedFunc6 = (CookingMinigameManager manager) => manager.PassedSongsCount >= 11 && manager.GetAtLeastGradeCount(CookingGrade.A) >= 5;
		Recipe recipe4 = Recipe.CreateInstance(ItemEnum.SmallSalad.ToGameId());
		recipe4.AddIngredientGenericType(EdibleTypeEnum.MainVegetable, 5);
		CookingSong prefab4 = CookingSong.CreateInstance(CookingSongEnumGI.ISOLated.ToGameId(), R.Music.Outside_Riots_ISO_Lated, (CookingSong song) => R.Midi.RadPechi_ISOLated.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.Build(), recipe4, isUnlockedFunc);
		cookingSongsData.Add(prefab4);
		Recipe recipe5 = Recipe.CreateInstance(ItemEnum.SimpleFriedFish.ToGameId());
		recipe5.AddIngredientFishMinWeight(0.1f);
		CookingSong prefab5 = CookingSong.CreateInstance(CookingSongEnumGI.Reflection.ToGameId(), R.Music.Room_Night_Reflection, (CookingSong song) => R.Midi.ToniS_Reflection.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(6f)
			.Build(), recipe5, isUnlockedFunc);
		cookingSongsData.Add(prefab5);
		Recipe recipe6 = Recipe.CreateInstance(ItemEnum.PotatoEggSalad.ToGameId());
		recipe6.AddIngredientExact(ItemEnum.Potatoes, 2);
		recipe6.AddIngredientExact(ItemEnum.Eggs, 2);
		recipe6.AddIngredientExact(ItemEnum.Vinegar);
		CookingSong prefab6 = CookingSong.CreateInstance(CookingSongEnumGI.ExoticHardware.ToGameId(), R.Music.PC_Exotic_Hardware, (CookingSong song) => R.Midi.RadPechi_ExoticHardware.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(7.5f)
			.Build(), recipe6, isUnlockedFunc2);
		cookingSongsData.Add(prefab6);
		Recipe recipe7 = Recipe.CreateInstance(ItemEnum.ChickenNoodleSoup.ToGameId());
		recipe7.AddIngredientExact(ItemEnum.ChickenMeat);
		recipe7.AddIngredientExact(ItemEnum.Onions);
		recipe7.AddIngredientExact(ItemEnum.Garlic);
		recipe7.AddIngredientExact(ItemEnum.Carrot);
		recipe7.AddIngredientGenericType(EdibleTypeEnum.MainVegetable, 3);
		recipe7.AddIngredientGenericType(EdibleTypeEnum.Pasta);
		CookingSong prefab7 = CookingSong.CreateInstance(CookingSongEnumGI.MysteryMeat.ToGameId(), R.Music.Themes_Cashier_Mystery_Meat, (CookingSong song) => R.Midi.Kaedo_MysteryMeat.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left, ignoreLength: true).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right, ignoreLength: true)
			.ChannelToNotes(2, (CookingSongRuntimeData.Builder builder, int _, Note note) => (TimeConverter.ConvertTo<BarBeatTicksTimeSpan>(note.Time, builder.TempoMap).Beats != 0L) ? NoteButtonTypeEnum.Left : NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.Build(), recipe7, isUnlockedFunc2);
		cookingSongsData.Add(prefab7);
		Recipe recipe8 = Recipe.CreateInstance(ItemEnum.AmericanChili.ToGameId());
		recipe8.AddIngredientExact(ItemEnum.RawGroundBeef);
		recipe8.AddIngredientExact(ItemEnum.Tomato, 2);
		recipe8.AddIngredientExact(ItemEnum.Carrot);
		recipe8.AddIngredientExact(ItemEnum.CanOfBakedBeans);
		recipe8.AddIngredientExact(ItemEnum.Onions);
		recipe8.AddIngredientExact(ItemEnum.Garlic);
		CookingSong prefab8 = CookingSong.CreateInstance(CookingSongEnumGI.Hypersexual.ToGameId(), R.Music.Story_Hypersexual, (CookingSong song) => R.Midi.RadPechi_Hypersexual.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Right).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Left)
			.WithVersion(0)
			.Build(), recipe8, isUnlockedFunc2);
		cookingSongsData.Add(prefab8);
		Recipe recipe9 = Recipe.CreateInstance(ItemEnum.CarrotSalad.ToGameId());
		recipe9.AddIngredientExact(ItemEnum.Carrot, 9);
		recipe9.AddIngredientExact(ItemEnum.Vinegar);
		recipe9.AddIngredientGenericType(EdibleTypeEnum.FlavourVegetable);
		CookingSong prefab9 = CookingSong.CreateInstance(CookingSongEnumGI.Carrot.ToGameId(), R.Music.Sex_Carrot, (CookingSong song) => R.Midi.LightM_Carrot.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(6.5f)
			.Build(), recipe9, isUnlockedFunc3);
		cookingSongsData.Add(prefab9);
		Recipe recipe10 = Recipe.CreateInstance(ItemEnum.FriedRice.ToGameId());
		recipe10.AddIngredientExact(ItemEnum.Rice);
		recipe10.AddIngredientExact(ItemEnum.Onions);
		recipe10.AddIngredientExact(ItemEnum.Garlic);
		recipe10.AddIngredientExact(ItemEnum.SoySauce);
		recipe10.AddIngredientGenericType(EdibleTypeEnum.MainVegetable, 2, isOptional: true);
		recipe10.AddIngredientGenericType(EdibleTypeEnum.FlavourVegetable, 2, isOptional: true);
		recipe10.AddIngredientGenericType(EdibleTypeEnum.Meat, 1, isOptional: true);
		CookingSong prefab10 = CookingSong.CreateInstance(CookingSongEnumGI.AirFryer.ToGameId(), R.Music.Cooking_Air_Fryer, (CookingSong song) => R.Midi.DreemChxsr_AirFryer.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(8f)
			.Build(), recipe10, isUnlockedFunc3);
		cookingSongsData.Add(prefab10);
		Recipe recipe11 = Recipe.CreateInstance(ItemEnum.CookedSteak.ToGameId());
		recipe11.AddIngredientExact(ItemEnum.Steak);
		CookingSong prefab11 = CookingSong.CreateInstance(CookingSongEnumGI.ShesRealToMe.ToGameId(), R.Music.Dates_She_s_Real_to_Me, (CookingSong song) => R.Midi.RadPechi_ShesRealToMe.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(6f)
			.Build(), recipe11, isUnlockedFunc3);
		cookingSongsData.Add(prefab11);
		Recipe recipe12 = Recipe.CreateInstance(ItemEnum.Pancakes.ToGameId());
		recipe12.AddIngredientExact(ItemEnum.Milk);
		recipe12.AddIngredientExact(ItemEnum.Sugar);
		recipe12.AddIngredientExact(ItemEnum.Flour);
		recipe12.AddIngredientExact(ItemEnum.Butter);
		recipe12.AddIngredientExact(ItemEnum.Eggs, 2);
		CookingSong prefab12 = CookingSong.CreateInstance(CookingSongEnumGI.CreamNPie.ToGameId(), R.Music.Cooking_Cream_N_Pie, (CookingSong song) => R.Midi.Nitrori_CreamNPie.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(7f)
			.Build(), recipe12, isUnlockedFunc3);
		cookingSongsData.Add(prefab12);
		Recipe recipe13 = Recipe.CreateInstance(ItemEnum.FancyFriedFish.ToGameId());
		recipe13.AddIngredientFishMinWeight(0.4f);
		recipe13.AddIngredientExact(ItemEnum.Butter);
		recipe13.AddIngredientExact(ItemEnum.Eggs);
		recipe13.AddIngredientExact(ItemEnum.Flour);
		CookingSong prefab13 = CookingSong.CreateInstance(CookingSongEnumGI.LetHimCook.ToGameId(), R.Music.Cooking_Let_Him_Cook, (CookingSong song) => R.Midi.SJ68_lethimcook.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(6.5f)
			.Build(), recipe13, isUnlockedFunc4);
		cookingSongsData.Add(prefab13);
		Recipe recipe14 = Recipe.CreateInstance(ItemEnum.SpaghettiBolognese.ToGameId());
		recipe14.AddIngredientExact(ItemEnum.RawGroundBeef);
		recipe14.AddIngredientExact(ItemEnum.Tomato, 4);
		recipe14.AddIngredientExact(ItemEnum.Onions);
		recipe14.AddIngredientExact(ItemEnum.Garlic);
		recipe14.AddIngredientGenericType(EdibleTypeEnum.Pasta);
		CookingSong prefab14 = CookingSong.CreateInstance(CookingSongEnumGI.MoanChoir.ToGameId(), R.Music.Story_Moan_Choir, (CookingSong song) => R.Midi.LightM_MoanChoir.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(2)
			.WithPixelsPerSecondDifficulty(7f)
			.Build(), recipe14, isUnlockedFunc4);
		cookingSongsData.Add(prefab14);
		Recipe recipe15 = Recipe.CreateInstance(ItemEnum.Gyros.ToGameId());
		recipe15.AddIngredientExact(ItemEnum.ChickenMeat, 2);
		recipe15.AddIngredientExact(ItemEnum.Tomato);
		recipe15.AddIngredientExact(ItemEnum.Garlic, 2);
		recipe15.AddIngredientExact(ItemEnum.Vinegar);
		recipe15.AddIngredientExact(ItemEnum.Yoghurt);
		CookingSong prefab15 = CookingSong.CreateInstance(CookingSongEnumGI.EvoxMachine.ToGameId(), R.Music.Sex_Evox_Machine, (CookingSong song) => R.Midi.Nitrori_EvoxMachine.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(7f)
			.Build(), recipe15, isUnlockedFunc4);
		cookingSongsData.Add(prefab15);
		Recipe recipe16 = Recipe.CreateInstance(ItemEnum.Omurice.ToGameId());
		recipe16.AddIngredientExact(ItemEnum.Eggs, 4);
		recipe16.AddIngredientExact(ItemEnum.Rice, 2);
		recipe16.AddIngredientExact(ItemEnum.Butter);
		recipe16.AddIngredientExact(ItemEnum.SoySauce, 1, isOptional: true);
		recipe16.AddIngredientExact(ItemEnum.ChickenMeat, 1, isOptional: true);
		recipe16.AddIngredientExact(ItemEnum.Onions, 1, isOptional: true);
		CookingSong prefab16 = CookingSong.CreateInstance(CookingSongEnumGI.CockTail.ToGameId(), R.Music.Cooking_Cocktail, (CookingSong song) => R.Midi.SJ68_Cocktail.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(8f)
			.Build(), recipe16, isUnlockedFunc4);
		cookingSongsData.Add(prefab16);
		Recipe recipe17 = Recipe.CreateInstance(ItemEnum.SpaghettiCarbonara.ToGameId());
		recipe17.AddIngredientGenericType(EdibleTypeEnum.Pasta);
		recipe17.AddIngredientExact(ItemEnum.Butter);
		recipe17.AddIngredientExact(ItemEnum.Cheese);
		recipe17.AddIngredientExact(ItemEnum.Eggs, 3);
		recipe17.AddIngredientExact(ItemEnum.Garlic, 2);
		CookingSong prefab17 = CookingSong.CreateInstance(CookingSongEnumGI.PPP.ToGameId(), R.Music.Cooking_Pudollarsy_Poppin__Pasta, (CookingSong song) => R.Midi.Kaedo_PPP.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(9f)
			.Build(), recipe17, isUnlockedFunc5);
		cookingSongsData.Add(prefab17);
		Recipe recipe18 = Recipe.CreateInstance(ItemEnum.BeefWelington.ToGameId());
		recipe18.AddIngredientExact(ItemEnum.Steak, 2);
		recipe18.AddIngredientExact(ItemEnum.Butter);
		recipe18.AddIngredientExact(ItemEnum.Flour);
		recipe18.AddIngredientExact(ItemEnum.Eggs);
		recipe18.AddIngredientExact(ItemEnum.Garlic);
		recipe18.AddIngredientExact(ItemEnum.Onions);
		CookingSong prefab18 = CookingSong.CreateInstance(CookingSongEnumGI.ProteinShake.ToGameId(), R.Music.Cooking_Protein_Shake, (CookingSong song) => R.Midi.SJ68_ProteinShake.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.ChannelToNotes(2, (CookingSongRuntimeData.Builder builder, int index, Note note) => (index % 2 == 0) ? NoteButtonTypeEnum.Left : NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(10f)
			.Build(), recipe18, isUnlockedFunc5);
		cookingSongsData.Add(prefab18);
		Recipe recipe19 = Recipe.CreateInstance(ItemEnum.Sushi.ToGameId());
		recipe19.AddIngredientFishMinWeight(1f);
		recipe19.AddIngredientExact(ItemEnum.Rice, 3);
		recipe19.AddIngredientExact(ItemEnum.Vinegar);
		recipe19.AddIngredientExact(ItemEnum.Sugar);
		recipe19.AddIngredientExact(ItemEnum.SoySauce);
		recipe19.AddIngredientExact(ItemEnum.Ginger);
		CookingSong prefab19 = CookingSong.CreateInstance(CookingSongEnumGI.RapidCookingChallenge.ToGameId(), R.Music.Cooking_RAPID_COOKING_CHALLENGE, (CookingSong song) => R.Midi.CELESTYN_RAPIDCOOKINGCHALLENGE.ToCookingSongRuntimeDataBuilder(song).ChannelToNotes(0, (CookingSongRuntimeData.Builder builder, int index, Note note) => (index % 2 != 0) ? NoteButtonTypeEnum.Left : NoteButtonTypeEnum.Right, ignoreLength: true).WithVersion(0)
			.WithPixelsPerSecondDifficulty(10f)
			.Build(), recipe19, isUnlockedFunc6);
		cookingSongsData.Add(prefab19);
		Recipe recipe20 = Recipe.CreateInstance(ItemEnum.SheepsCurry.ToGameId());
		recipe20.AddIngredientExact(ItemEnum.Rice);
		recipe20.AddIngredientExact(ItemEnum.SoySauce);
		recipe20.AddIngredientExact(ItemEnum.Carrot, 2);
		recipe20.AddIngredientExact(ItemEnum.Onions, 2);
		recipe20.AddIngredientExact(ItemEnum.Ginger);
		recipe20.AddIngredientExact(ItemEnum.Potatoes);
		recipe20.AddIngredientExact(ItemEnum.Sugar);
		recipe20.AddIngredientExact(ItemEnum.Yoghurt);
		recipe20.AddIngredientGenericType(EdibleTypeEnum.Meat);
		recipe20.AddIngredientGenericType(EdibleTypeEnum.MainVegetable);
		recipe20.AddIngredientGenericType(EdibleTypeEnum.FlavourVegetable, 1, isOptional: true);
		CookingSong prefab20 = CookingSong.CreateInstance(CookingSongEnumGI.Demise.ToGameId(), R.Music.BadEnd_Demise, (CookingSong song) => R.Midi.Sheep_Demise.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(10f)
			.Build(), recipe20, (CookingMinigameManager x) => SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.HasGlobalFlag("sre"), hideIfLocked: true);
		cookingSongsData.Add(prefab20);
		Recipe recipe21 = Recipe.CreateInstance(ItemEnum.CarrotCake.ToGameId());
		recipe21.AddIngredientExact(ItemEnum.Carrot, 5);
		recipe21.AddIngredientExact(ItemEnum.Eggs, 4);
		recipe21.AddIngredientExact(ItemEnum.Flour, 2);
		recipe21.AddIngredientExact(ItemEnum.Butter, 2);
		recipe21.AddIngredientExact(ItemEnum.Milk);
		recipe21.AddIngredientExact(ItemEnum.Sugar);
		CookingSong prefab21 = CookingSong.CreateInstance(CookingSongEnumGI.WhiteDress.ToGameId(), R.Music.Room_Day_Late_Game_White_Dress, (CookingSong song) => R.Midi.ToniS_WhiteDress.ToCookingSongRuntimeDataBuilder(song).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).ChannelToNotesAllTheSame(1, NoteButtonTypeEnum.Right)
			.WithVersion(0)
			.WithPixelsPerSecondDifficulty(6f)
			.Build(), recipe21, (CookingMinigameManager x) => GameVariables.Current.IsLateGame && x.PassedSongsCount > 8, hideIfLocked: true);
		cookingSongsData.Add(prefab21);
		return UniTask.CompletedTask;
	}

	public static void AddModdedSong(CookingSong cookingSong)
	{
		cookingSongsData.Add(cookingSong);
	}
}
