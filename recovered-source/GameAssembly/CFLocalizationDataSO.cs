using System.Collections.Generic;
using UnityEngine;

public sealed class CFLocalizationDataSO : SingletonScriptableObject<CFLocalizationDataSO>, IHasLocalizedStringReferenceContainers
{
	[Header("All")]
	[SerializeField]
	private FrickConversations frickConversations;

	[SerializeField]
	private BlowjobBrainState.BlowjobConversations blowjobConversations;

	[SerializeField]
	private CowgirlBrainState.CowgirlConversations cowgirlConversations;

	[SerializeField]
	private CuddleBrainState.CuddleConversations cuddleConversations;

	[SerializeField]
	private NewInteractBrainState.InteractConversations interactConversations;

	[SerializeField]
	private ShowerBrainState.ShowerConversations showerConversations;

	[SerializeField]
	private FungusEnumerators.PlayerNamingStrings playerNamingStrings;

	[SerializeField]
	private FungusEnumerators.ClothierStrings clothierStrings;

	[SerializeField]
	private StorySingleton.StoryMenuBranchesStrings storyMenuBranchesStrings;

	[Header("Personality Modules")]
	[SerializeField]
	private StorySingleton.TsunTsunPersonalityModuleConversations tsunTsunPersonalityModuleConversations;

	[SerializeField]
	private StorySingleton.CatgirlPersonalityModuleConversations catgirlPersonalityModuleConversations;

	[SerializeField]
	private StorySingleton.DriverPersonalityModuleConversations driverPersonalityModuleConversations;

	[SerializeField]
	private StorySingleton.RobotPersonalityModuleConversations robotPersonalityModuleConversations;

	[SerializeField]
	private StorySingleton.YanderePersonalityModuleConversations yanderePersonalityModuleConversations;

	[SerializeField]
	private StorySingleton.SlutPersonalityModuleConversations slutPersonalityModuleConversations;

	[SerializeField]
	private StorySingleton.FemcelPersonalityModuleConversations femcelPersonalityModuleConversations;

	[SerializeField]
	private StorySingleton.HimederePersonalityModuleConversations himederePersonalityModuleConversations;

	[SerializeField]
	private StorySingleton.SillyCatPersonalityModuleConversations sillyCatPersonalityModuleConversations;

	[SerializeField]
	private StorySingleton.AraAraPersonalityModuleConversations araAraPersonalityModuleConversations;

	public FrickConversations FrickConversations => frickConversations;

	public BlowjobBrainState.BlowjobConversations BlowjobConversations => blowjobConversations;

	public CowgirlBrainState.CowgirlConversations CowgirlConversations => cowgirlConversations;

	public CuddleBrainState.CuddleConversations CuddleConversations => cuddleConversations;

	public NewInteractBrainState.InteractConversations InteractConversations => interactConversations;

	public ShowerBrainState.ShowerConversations ShowerConversations => showerConversations;

	public FungusEnumerators.PlayerNamingStrings PlayerNamingStrings => playerNamingStrings;

	public FungusEnumerators.ClothierStrings ClothierStrings => clothierStrings;

	public StorySingleton.StoryMenuBranchesStrings StoryMenuBranchesStrings => storyMenuBranchesStrings;

	public StorySingleton.TsunTsunPersonalityModuleConversations TsunTsunPersonalityModuleConversations => tsunTsunPersonalityModuleConversations;

	public StorySingleton.CatgirlPersonalityModuleConversations CatgirlPersonalityModuleConversations => catgirlPersonalityModuleConversations;

	public StorySingleton.DriverPersonalityModuleConversations DriverPersonalityModuleConversations => driverPersonalityModuleConversations;

	public StorySingleton.RobotPersonalityModuleConversations RobotPersonalityModuleConversations => robotPersonalityModuleConversations;

	public StorySingleton.YanderePersonalityModuleConversations YanderePersonalityModuleConversations => yanderePersonalityModuleConversations;

	public StorySingleton.SlutPersonalityModuleConversations SlutPersonalityModuleConversations => slutPersonalityModuleConversations;

	public StorySingleton.FemcelPersonalityModuleConversations FemcelPersonalityModuleConversations => femcelPersonalityModuleConversations;

	public StorySingleton.HimederePersonalityModuleConversations HimederePersonalityModuleConversations => himederePersonalityModuleConversations;

	public StorySingleton.SillyCatPersonalityModuleConversations SillyCatPersonalityModuleConversations => sillyCatPersonalityModuleConversations;

	public StorySingleton.AraAraPersonalityModuleConversations AraAraPersonalityModuleConversations => araAraPersonalityModuleConversations;

	public IEnumerable<LocalizedStringReferenceContainer> GetAllContainers()
	{
		yield return frickConversations;
		yield return blowjobConversations;
		yield return cowgirlConversations;
		yield return cuddleConversations;
		yield return interactConversations;
		yield return showerConversations;
		yield return playerNamingStrings;
		yield return clothierStrings;
		yield return storyMenuBranchesStrings;
		yield return tsunTsunPersonalityModuleConversations;
		yield return catgirlPersonalityModuleConversations;
		yield return driverPersonalityModuleConversations;
		yield return robotPersonalityModuleConversations;
		yield return yanderePersonalityModuleConversations;
		yield return slutPersonalityModuleConversations;
		yield return femcelPersonalityModuleConversations;
		yield return himederePersonalityModuleConversations;
		yield return sillyCatPersonalityModuleConversations;
		yield return araAraPersonalityModuleConversations;
	}
}
