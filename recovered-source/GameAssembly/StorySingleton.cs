using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Fungus;
using GameItems;
using StoryConditions;
using UnityEngine;
using UnityEngine.Scripting;

public sealed class StorySingleton : SingletonMonoBehaviour<StorySingleton>
{
	[AttributeUsage(AttributeTargets.Field)]
	public class PersonalitySourceIgnoreAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class PersonalityIndexAttribute : Attribute
	{
		public readonly int Index;

		public PersonalityIndexAttribute(int index)
		{
			Index = index;
		}
	}

	public abstract class EmptyPersonalityModuleConversationContainer : LocalizedStringReferenceContainer
	{
	}

	public abstract class BasePersonalityModuleConversationContainer : EmptyPersonalityModuleConversationContainer
	{
		[PersonalityIndex(0)]
		public SerializedLocalizedConversation Frick_StartFrick;

		[PersonalityIndex(1)]
		public SerializedLocalizedConversation Frick_GenericFuckBrainStateRandomDialogueCockOutside;

		[PersonalityIndex(2)]
		public SerializedLocalizedConversation Frick_GenericFuckBrainStateRandomDialogueCockInside;

		[PersonalityIndex(3)]
		public SerializedLocalizedConversation Frick_GenericFuckBrainStateRandomDialogueCockInsideAnal;

		[PersonalityIndex(4)]
		public SerializedLocalizedConversation Frick_CumInside;

		[PersonalityIndex(5)]
		public SerializedLocalizedConversation Frick_CumInsideAnal;

		[PersonalityIndex(6)]
		public SerializedLocalizedConversation Frick_CumOutside;

		[PersonalityIndex(7)]
		public SerializedLocalizedConversation Frick_CumTogether;

		[PersonalityIndex(8)]
		public SerializedLocalizedConversation Frick_BotCumming;

		[PersonalityIndex(9)]
		public SerializedLocalizedConversation Frick_Moan;

		[PersonalityIndex(10)]
		public SerializedLocalizedConversation Frick_EdgingOn;

		[PersonalityIndex(11)]
		public SerializedLocalizedConversation Frick_EdgingOff;

		[PersonalityIndex(12)]
		public SerializedLocalizedConversation Frick_Headpat;

		[PersonalityIndex(13)]
		public SerializedLocalizedConversation Frick_RapeBegStopSuccess;

		[PersonalityIndex(14)]
		public SerializedLocalizedConversation Frick_RapeBegStopRefuse;

		[PersonalityIndex(15)]
		public SerializedLocalizedConversation Frick_RapeFlacidReaction;

		[PersonalityIndex(16)]
		public SerializedLocalizedConversation Frick_FingeringBotCumming;

		[PersonalityIndex(18)]
		public SerializedLocalizedConversation Frick_FingeringDuring;

		[PersonalityIndex(19)]
		public SerializedLocalizedConversation Frick_BoobFondle;

		[PersonalityIndex(20)]
		public SerializedLocalizedConversation Frick_NipplePull;

		[PersonalityIndex(21)]
		public SerializedLocalizedConversation Frick_PussySpread;

		[PersonalityIndex(22)]
		public SerializedLocalizedConversation Frick_ClitRub;

		[PersonalityIndex(23)]
		public SerializedLocalizedConversation Frick_Handholding;

		[PersonalityIndex(24)]
		public SerializedLocalizedConversation Frick_UpsetOnlySheMoves;

		[PersonalityIndex(25)]
		public SerializedLocalizedConversation Frick_CumThighjob;

		[PersonalityIndex(26)]
		public SerializedLocalizedConversation Frick_FlacidBoredReaction;

		[PersonalityIndex(27)]
		public SerializedLocalizedConversation Frick_FingeringAnusDuring;

		[PersonalityIndex(28)]
		public SerializedLocalizedConversation Frick_AnusSpread;

		[PersonalityIndex(29)]
		public SerializedLocalizedConversation Frick_FingeringAnusBotCumming;

		[PersonalityIndex(200)]
		public SerializedLocalizedConversation Blowjob_EdgingAlmostCame;

		[PersonalityIndex(201)]
		public SerializedLocalizedConversation Blowjob_EdgingHadToSlowdown;

		[PersonalityIndex(202)]
		public SerializedLocalizedConversation Blowjob_EdgingForceStop;

		[PersonalityIndex(203)]
		public SerializedLocalizedConversation Blowjob_SwallowFull;

		[PersonalityIndex(204)]
		public SerializedLocalizedConversation Blowjob_SwallowHalf;

		[PersonalityIndex(205)]
		public SerializedLocalizedConversation Blowjob_SwallowLittle;

		[PersonalityIndex(206)]
		public SerializedLocalizedConversation Blowjob_SwallowAlmostEmpty;

		[PersonalityIndex(207)]
		public SerializedLocalizedConversation Blowjob_SwitchToCowgirlBotLead;

		[PersonalityIndex(208)]
		public SerializedLocalizedConversation Blowjob_SwitchToCowgirlBotRape;

		[PersonalityIndex(209)]
		public SerializedLocalizedConversation Blowjob_SwallowWithMl;

		[PersonalityIndex(400)]
		public SerializedLocalizedConversation Cowgirl_RemovePants;

		[PersonalityIndex(401)]
		public SerializedLocalizedConversation Cowgirl_BeforeFirstInsertBotLead;

		[PersonalityIndex(402)]
		public SerializedLocalizedConversation Cowgirl_BeforeFirstInsert;

		[PersonalityIndex(403)]
		public SerializedLocalizedConversation Cowgirl_EdgingForceStopped;

		[PersonalityIndex(404)]
		public SerializedLocalizedConversation Cowgirl_EdgingBreak;

		[PersonalityIndex(405)]
		public SerializedLocalizedConversation Cowgirl_FlacidReaction;

		[PersonalityIndex(406)]
		public SerializedLocalizedConversation Cowgirl_WhereToCumQuestion;

		[PersonalityIndex(407)]
		public SerializedLocalizedConversation Cowgirl_WhereToCumQuestionDidntAnswer;

		[PersonalityIndex(408)]
		public SerializedLocalizedConversation Cowgirl_BeforeFirstInsertRape;

		[PersonalityIndex(409)]
		public SerializedLocalizedConversation Cowgirl_MadeMcCumInsideWhileCummingHerself;

		[PersonalityIndex(410)]
		public SerializedLocalizedConversation Cowgirl_BeforeSwitchToBlowjobBotLead;

		[PersonalityIndex(411)]
		public SerializedLocalizedConversation Cowgirl_BeforeSwitchToBlowjobRape;

		[PersonalityIndex(412)]
		public SerializedLocalizedConversation Cowgirl_AfterFirstInsertBotRape;
	}

	[Serializable]
	public class TsunTsunPersonalityModuleConversations : BasePersonalityModuleConversationContainer
	{
		public override string ContainerName => "TsunTsun";
	}

	[Serializable]
	public class CatgirlPersonalityModuleConversations : BasePersonalityModuleConversationContainer
	{
		public override string ContainerName => "Catgirl";
	}

	[Serializable]
	public class DriverPersonalityModuleConversations : EmptyPersonalityModuleConversationContainer
	{
		public SerializedLocalizedConversation Moan;

		public SerializedLocalizedConversation IDrive;

		public override string ContainerName => "Driver";
	}

	[Serializable]
	public class RobotPersonalityModuleConversations : BasePersonalityModuleConversationContainer
	{
		public override string ContainerName => "Robot";
	}

	[Serializable]
	public class YanderePersonalityModuleConversations : BasePersonalityModuleConversationContainer
	{
		public override string ContainerName => "Yandere";
	}

	[Serializable]
	public class SlutPersonalityModuleConversations : BasePersonalityModuleConversationContainer
	{
		public override string ContainerName => "Slut";
	}

	[Serializable]
	public class FemcelPersonalityModuleConversations : BasePersonalityModuleConversationContainer
	{
		public override string ContainerName => "Femcel";
	}

	[Serializable]
	public class HimederePersonalityModuleConversations : BasePersonalityModuleConversationContainer
	{
		public override string ContainerName => "Himedere";
	}

	[Serializable]
	public class SillyCatPersonalityModuleConversations : BasePersonalityModuleConversationContainer
	{
		public override string ContainerName => "SillyCat";
	}

	[Serializable]
	public class AraAraPersonalityModuleConversations : BasePersonalityModuleConversationContainer
	{
		public override string ContainerName => "Dom";
	}

	public abstract class PersonalityModuleDefinition
	{
		protected readonly StoryCondition[] EmptyConditions = new StoryCondition[1] { StoryCondition.AlwaysFalseCondition };

		protected Dictionary<string, StoryBotDialogueBranchContainer> _containers = new Dictionary<string, StoryBotDialogueBranchContainer>();

		protected HashSet<GameId> _items = new HashSet<GameId>();

		public static IEnumerable<StoryBotDialogueTagDefinition> GetAllPersonalityModifiableTags(StorySingleton storySingleton)
		{
			Type[] usualAreas = UsualAreas;
			foreach (Type key in usualAreas)
			{
				StoryBotDialogueAreaDefinition area = storySingleton._storyBotDialogueAreaDefinitions[key];
				foreach (FieldInfo allTagField in area.GetAllTagFields())
				{
					if (allTagField.GetCustomAttribute<PersonalitySourceIgnoreAttribute>() == null)
					{
						yield return (StoryBotDialogueTagDefinition)allTagField.GetValue(area);
					}
				}
			}
		}

		public void AddItem(GameId gameId)
		{
			_items.Add(gameId);
			StoryCondition storyCondition = ((_items.Count != 1) ? ((StoryCondition)StoryCondition.Or(((IEnumerable<GameId>)_items).Select((Func<GameId, StoryCondition>)((GameId x) => StoryCondition.HasItemEquipped(x))).ToArray())) : ((StoryCondition)StoryCondition.HasItemEquipped(_items.First())));
			foreach (KeyValuePair<string, StoryBotDialogueBranchContainer> container in _containers)
			{
				container.Value.SetConditions(storyCondition);
			}
		}
	}

	public class LuaPersonalityModuleDefinition : PersonalityModuleDefinition
	{
		[Preserve]
		public StoryBotDialogueBranchContainer PrepareContainer(string tagId, int stageNr = 3)
		{
			SingletonMonoBehaviour<StorySingleton>.Instance.GetStage(stageNr).GetTag(tagId).CreateContainer(out var container, StoryBotContainerTypeEnum.Override);
			_containers.Add(tagId, container);
			container.SetConditions(EmptyConditions);
			return container;
		}
	}

	public abstract class ConversationContainerPersonalityModuleDefinition<T> : PersonalityModuleDefinition where T : EmptyPersonalityModuleConversationContainer
	{
		protected void PrepareOverrideContainer(StoryBotDialogueStage stage, StoryBotDialogueTagDefinition tag, out StoryBotDialogueBranchContainer container)
		{
			tag.CreateContainer(stage, out container, StoryBotContainerTypeEnum.Override);
			_containers.Add(tag.Id, container);
			container.SetConditions(EmptyConditions);
		}

		public void FillCommonContainers(int startIndex, StorySingleton storySingleton, T conversations)
		{
			StoryBotDialogueStage stage = storySingleton._stage3;
			Dictionary<string, (FieldInfo, PersonalityIndexAttribute)> dictionary = (from x in conversations.GetAllLocalizedConversationFields()
				select (field: x, indexAttribute: x.GetCustomAttribute<PersonalityIndexAttribute>()) into x
				where x.indexAttribute != null
				select x).ToDictionary(((FieldInfo field, PersonalityIndexAttribute indexAttribute) x) => x.field.Name, ((FieldInfo field, PersonalityIndexAttribute indexAttribute) x) => x);
			foreach (StoryBotDialogueTagDefinition allPersonalityModifiableTag in PersonalityModuleDefinition.GetAllPersonalityModifiableTags(storySingleton))
			{
				string id = allPersonalityModifiableTag.Id;
				if (dictionary.TryGetValue(id, out var value))
				{
					int num = startIndex + value.Item2.Index;
					SerializedLocalizedConversation serializedLocalizedConversation = (SerializedLocalizedConversation)value.Item1.GetValue(conversations);
					if (stage.TryGetTag(id, out var _))
					{
						PrepareOverrideContainer(stage, allPersonalityModifiableTag, out var container2);
						container2.AddBranch(new StoryBotDialogueBranch(num, serializedLocalizedConversation));
					}
					else
					{
						Debug.LogError("Tag " + id + " not found!");
					}
				}
				else
				{
					Debug.LogError("Field " + id + " not found in " + typeof(T).Name);
				}
			}
		}
	}

	public class TsunTsunPersonalityDefinition : ConversationContainerPersonalityModuleDefinition<TsunTsunPersonalityModuleConversations>
	{
		public TsunTsunPersonalityDefinition(StorySingleton storySingleton)
		{
			FillCommonContainers(500000, storySingleton, SingletonScriptableObject<CFLocalizationDataSO>.Instance.TsunTsunPersonalityModuleConversations);
		}
	}

	public class CatgirlPersonalityDefinition : ConversationContainerPersonalityModuleDefinition<CatgirlPersonalityModuleConversations>
	{
		public CatgirlPersonalityDefinition(StorySingleton storySingleton)
		{
			FillCommonContainers(503000, storySingleton, SingletonScriptableObject<CFLocalizationDataSO>.Instance.CatgirlPersonalityModuleConversations);
		}
	}

	public class DriverPersonalityDefinition : ConversationContainerPersonalityModuleDefinition<DriverPersonalityModuleConversations>
	{
		public DriverPersonalityDefinition(StorySingleton storySingleton)
		{
			StoryBotDialogueStage stage = storySingleton._stage3;
			DriverPersonalityModuleConversations driverPersonalityModuleConversations = SingletonScriptableObject<CFLocalizationDataSO>.Instance.DriverPersonalityModuleConversations;
			SerializedLocalizedConversation moan = driverPersonalityModuleConversations.Moan;
			SerializedLocalizedConversation iDrive = driverPersonalityModuleConversations.IDrive;
			Stage3VanillaDialogueIds id = Stage3VanillaDialogueIds.DriverPersonalityDialogues;
			int num = 1000;
			foreach (StoryBotDialogueTagDefinition allPersonalityModifiableTag in PersonalityModuleDefinition.GetAllPersonalityModifiableTags(storySingleton))
			{
				string id2 = allPersonalityModifiableTag.Id;
				PrepareOverrideContainer(stage, allPersonalityModifiableTag, out var container);
				if (id2 == "Frick_Moan")
				{
					container.AddBranch(new StoryBotDialogueBranch(id.GetGameId(num), moan));
				}
				else
				{
					container.AddBranch(new StoryBotDialogueBranch(id.GetGameId(num), iDrive));
				}
				num++;
			}
		}
	}

	public class RobotPersonalityDefinition : ConversationContainerPersonalityModuleDefinition<RobotPersonalityModuleConversations>
	{
		public RobotPersonalityDefinition(StorySingleton storySingleton)
		{
			FillCommonContainers(509000, storySingleton, SingletonScriptableObject<CFLocalizationDataSO>.Instance.RobotPersonalityModuleConversations);
		}
	}

	public class YanderePersonalityDefinition : ConversationContainerPersonalityModuleDefinition<YanderePersonalityModuleConversations>
	{
		public YanderePersonalityDefinition(StorySingleton storySingleton)
		{
			FillCommonContainers(512000, storySingleton, SingletonScriptableObject<CFLocalizationDataSO>.Instance.YanderePersonalityModuleConversations);
		}
	}

	public class SlutPersonalityDefinition : ConversationContainerPersonalityModuleDefinition<SlutPersonalityModuleConversations>
	{
		public SlutPersonalityDefinition(StorySingleton storySingleton)
		{
			FillCommonContainers(515000, storySingleton, SingletonScriptableObject<CFLocalizationDataSO>.Instance.SlutPersonalityModuleConversations);
		}
	}

	public class FemcelPersonalityDefinition : ConversationContainerPersonalityModuleDefinition<FemcelPersonalityModuleConversations>
	{
		public FemcelPersonalityDefinition(StorySingleton storySingleton)
		{
			FillCommonContainers(518000, storySingleton, SingletonScriptableObject<CFLocalizationDataSO>.Instance.FemcelPersonalityModuleConversations);
		}
	}

	public class HimederePersonalityDefinition : ConversationContainerPersonalityModuleDefinition<HimederePersonalityModuleConversations>
	{
		public HimederePersonalityDefinition(StorySingleton storySingleton)
		{
			FillCommonContainers(521000, storySingleton, SingletonScriptableObject<CFLocalizationDataSO>.Instance.HimederePersonalityModuleConversations);
		}
	}

	public class SillyCatPersonalityDefinition : ConversationContainerPersonalityModuleDefinition<SillyCatPersonalityModuleConversations>
	{
		public SillyCatPersonalityDefinition(StorySingleton storySingleton)
		{
			FillCommonContainers(524000, storySingleton, SingletonScriptableObject<CFLocalizationDataSO>.Instance.SillyCatPersonalityModuleConversations);
		}
	}

	public class AraAraPersonalityDefinition : ConversationContainerPersonalityModuleDefinition<AraAraPersonalityModuleConversations>
	{
		public AraAraPersonalityDefinition(StorySingleton storySingleton)
		{
			FillCommonContainers(527000, storySingleton, SingletonScriptableObject<CFLocalizationDataSO>.Instance.AraAraPersonalityModuleConversations);
		}
	}

	public class AllDefinedPersonalities
	{
		public TsunTsunPersonalityDefinition TsunTsunPersonality { get; private set; }

		public CatgirlPersonalityDefinition CatgirlPersonality { get; private set; }

		public DriverPersonalityDefinition DriverPersonality { get; private set; }

		public RobotPersonalityDefinition RobotPersonality { get; private set; }

		public YanderePersonalityDefinition YanderePersonality { get; private set; }

		public SlutPersonalityDefinition SlutPersonality { get; private set; }

		public FemcelPersonalityDefinition FemcelPersonality { get; private set; }

		public HimederePersonalityDefinition HimederePersonality { get; private set; }

		public SillyCatPersonalityDefinition SillyCatPersonality { get; private set; }

		public AraAraPersonalityDefinition AraAraPersonality { get; private set; }

		public AllDefinedPersonalities(StorySingleton storySingleton)
		{
			TsunTsunPersonality = new TsunTsunPersonalityDefinition(storySingleton);
			CatgirlPersonality = new CatgirlPersonalityDefinition(storySingleton);
			DriverPersonality = new DriverPersonalityDefinition(storySingleton);
			RobotPersonality = new RobotPersonalityDefinition(storySingleton);
			SillyCatPersonality = new SillyCatPersonalityDefinition(storySingleton);
			YanderePersonality = new YanderePersonalityDefinition(storySingleton);
			SlutPersonality = new SlutPersonalityDefinition(storySingleton);
			FemcelPersonality = new FemcelPersonalityDefinition(storySingleton);
			HimederePersonality = new HimederePersonalityDefinition(storySingleton);
			AraAraPersonality = new AraAraPersonalityDefinition(storySingleton);
		}
	}

	public class Stages
	{
		public readonly StoryBotDialogueStage Stage1;

		public readonly StoryBotDialogueStage Stage2;

		public readonly StoryBotDialogueStage Stage3;

		public Stages(StoryBotDialogueStage stage1, StoryBotDialogueStage stage2, StoryBotDialogueStage stage3)
		{
			Stage1 = stage1;
			Stage2 = stage2;
			Stage3 = stage3;
		}
	}

	[Serializable]
	public class StoryMenuBranchesStrings : LocalizedStringReferenceContainer
	{
		public LocalizedStringReferenceStory StartSexStream;

		public LocalizedStringReferenceStory StartSexNoStream;

		public LocalizedStringReferenceStory SleepWithBot;

		public LocalizedStringReferenceStory SleepAloneTillRested;

		public LocalizedStringReferenceStory SleepAloneFor480;

		public LocalizedStringReferenceStory SleepAloneFor240;

		public LocalizedStringReferenceStory SleepAloneFor60;

		public LocalizedStringReferenceStory SleepAloneDontSleep;

		public LocalizedStringReferenceStory CumChoice_CleanItOffWithARag;

		public LocalizedStringReferenceStory CumChoice_LeaveItOnHer;

		public LocalizedStringReferenceStory Bathroom_BotShowerAlone;

		public LocalizedStringReferenceStory Bathroom_AnonShowerAlone;

		public LocalizedStringReferenceStory Bathroom_ShowerTogether;

		public LocalizedStringReferenceStory OutsideAloneGrocery;

		public LocalizedStringReferenceStory OutsideAloneDispenser;

		public LocalizedStringReferenceStory OutsideAlonePharmacy;

		public LocalizedStringReferenceStory OutsideAloneClothier;

		public LocalizedStringReferenceStory OutsideAloneLabourOffice;

		public LocalizedStringReferenceStory OutsideAloneWalk;

		public LocalizedStringReferenceStory OutsideAloneGoFishing;

		public LocalizedStringReferenceStory OutsideAloneVisitFisher;

		public LocalizedStringReferenceStory GoToChurch;

		public LocalizedStringReferenceStory GoToChurchToInvestigate;

		public LocalizedStringReferenceStory GoToPoliceStationReportNun;

		public LocalizedStringReferenceStory OutsideAloneGoToRichArea;

		public LocalizedStringReferenceStory GoToAShopForRichPeople;

		public LocalizedStringReferenceStory VisitBDH;

		public LocalizedStringReferenceStory OutsideAloneReturnToSlums;

		public LocalizedStringReferenceStory GoBackHome;

		public LocalizedStringReferenceStory DEBUG;

		public LocalizedStringReferenceStory Nevermind;

		public LocalizedStringReferenceStory OutsideWithBotArtGalleryDate1;

		public LocalizedStringReferenceStory OutsideWithBotArtGalleryDate2;

		public LocalizedStringReferenceStory OutsideWithBotRestaurantDate1;

		public LocalizedStringReferenceStory OutsideWithBotRestaurantDate2;

		public LocalizedStringReferenceStory OutsideWithBotKino1;

		public LocalizedStringReferenceStory OutsideWithBotKino2;

		public LocalizedStringReferenceStory OutsideWithBotKaraoke1;

		public LocalizedStringReferenceStory OutsideWithBotIceSkating1;

		public LocalizedStringReferenceStory OutsideWithBotPlanetarium;

		public LocalizedStringReferenceStory OutsideWithBotAmusementPark2;

		public LocalizedStringReferenceStory OutsideWithBotIceSkatingWithShanice;

		public LocalizedStringReferenceStory OutsideWithBotCafeWithAnnalie;

		public LocalizedStringReferenceStory OutsideWithBotBowlingWithMelissa;

		public LocalizedStringReferenceStory OutsideWithBotBowlingWithMelissaAndAnnalie_Melissa9;

		public LocalizedStringReferenceStory OutsideWithBotBowlingWithMelissaAndAnnalie_Melissa11;

		public LocalizedStringReferenceStory OutsideWithBotKaraokeWithMelissaAndAnnalie_Melissa12;

		public LocalizedStringReferenceStory OutsideWithBotCafeWithMelissaAndAnnalie_Melissa16;

		public LocalizedStringReferenceStory OutsideWithBotGokartsWithMelissaAndAnnalie_Melissa17;

		public LocalizedStringReferenceStory OutsideWithBotBowlingWithMelissaAndAnnalie_Melissa19;

		public LocalizedStringReferenceStory OutsideWithBotCafeWithMelissaAndAnnalie_Melissa21;

		public LocalizedStringReferenceStory OutsideWithBotKaraokeWithMelissa_Melissa21_5;

		public LocalizedStringReferenceStory OutsideWithBotMeetupWithGrumpyAnnalie;

		public LocalizedStringReferenceStory OutsideWithBotCheckOutTheRunway;

		public LocalizedStringReferenceStory OutsideWithBotMeetAtCafeWithAnnalieAndHerFriends;

		public LocalizedStringReferenceStory OutsideWithBotGoToAnnaliesFashionShow;

		public LocalizedStringReferenceStory OutsideWithBotGoFishingTogether;

		public LocalizedStringReferenceStory OutsideWithBotAquariumDate;

		public LocalizedStringReferenceStory OutsideWithBotGoOnADate;

		public LocalizedStringReferenceStory OutsideWithBotHangOutWithFriends;

		public LocalizedStringReferenceStory OutsideWithBotTrickOrTreat;

		public LocalizedStringReferenceStory OutsideWithBotClothierWithBot;

		public LocalizedStringReferenceStory OutsideWithBotWalk;

		public LocalizedStringReferenceStory OutsideWithBotInvestigateLab;

		public LocalizedStringReferenceStory OutsideWithBotGoBackHome;

		public LocalizedStringReferenceStory Fisher_SellFish;

		public LocalizedStringReferenceStory Fisher_SmokeFishJoint;

		public LocalizedStringReferenceStory Fisher_Leave;

		public override string ContainerName => "StoryMenu";
	}

	public static readonly Type[] UsualAreas = new Type[3]
	{
		typeof(FrickDialogueArea),
		typeof(BlowjobBrainState.BlowjobDialogueArea),
		typeof(CowgirlBrainState.CowgirlSceneDialogueArea)
	};

	private AllDefinedPersonalities _allDefinedPersonalities;

	private StoryBotDialogueStage _stage1;

	private StoryBotDialogueStage _stage2;

	private StoryBotDialogueStage _stage3;

	private Dictionary<Type, StoryBotDialogueAreaDefinition> _storyBotDialogueAreaDefinitions = new Dictionary<Type, StoryBotDialogueAreaDefinition>();

	private List<StoryAutoEntryPoint> _storyAutoEntryPoints = new List<StoryAutoEntryPoint>();

	private List<StoryMenuEntryPoint> _storyMenuEntryPoints = new List<StoryMenuEntryPoint>();

	private bool _initialized;

	public StoryBotDialogueStage Stage1
	{
		get
		{
			MakeSureInitialized();
			return _stage1;
		}
	}

	public StoryBotDialogueStage Stage2
	{
		get
		{
			MakeSureInitialized();
			return _stage2;
		}
	}

	public StoryBotDialogueStage Stage3
	{
		get
		{
			MakeSureInitialized();
			return _stage3;
		}
	}

	public IEnumerable<StoryBotDialogueStage> AllStages
	{
		get
		{
			MakeSureInitialized();
			return GetAllStages();
		}
	}

	public AllDefinedPersonalities GetAllDefinedPersonalities()
	{
		MakeSureInitialized();
		return _allDefinedPersonalities;
	}

	private void PreparePersonalityDefinitions()
	{
		_allDefinedPersonalities = new AllDefinedPersonalities(this);
	}

	public string GenerateCommonPersonalityModuleConversations()
	{
		MakeSureInitialized();
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			Type[] usualAreas = UsualAreas;
			foreach (Type key in usualAreas)
			{
				StoryBotDialogueAreaDefinition storyBotDialogueAreaDefinition = _storyBotDialogueAreaDefinitions[key];
				foreach (FieldInfo allTagField in storyBotDialogueAreaDefinition.GetAllTagFields())
				{
					StoryBotDialogueTagDefinition storyBotDialogueTagDefinition = (StoryBotDialogueTagDefinition)allTagField.GetValue(storyBotDialogueAreaDefinition);
					stringBuilder.AppendLine("public SerializedLocalizedConversation " + storyBotDialogueTagDefinition.Id + ";");
				}
			}
			return stringBuilder.ToString();
		}
	}

	private StoryBotDialogueStage GetStageInner(int stageNr)
	{
		if (stageNr > 1)
		{
			if (stageNr <= 2)
			{
				return _stage2;
			}
			return _stage3;
		}
		return _stage1;
	}

	private IEnumerable<StoryBotDialogueStage> GetAllStages()
	{
		yield return _stage1;
		yield return _stage2;
		yield return _stage3;
	}

	private StoryBotDialogueStage GetStage(int stageNr, string queryTag)
	{
		StoryBotDialogueStage stageInner = GetStageInner(stageNr);
		if (queryTag == "Interact_MainDialogue")
		{
			return stageInner;
		}
		if (stageInner.IsPrologueFinished())
		{
			return stageInner;
		}
		return GetStageInner(stageNr - 1);
	}

	public StoryBotDialogueStage GetStage(int stageNr)
	{
		MakeSureInitialized();
		return GetStageInner(stageNr);
	}

	public T GetDialogueArea<T>() where T : StoryBotDialogueAreaDefinition
	{
		MakeSureInitialized();
		if (_storyBotDialogueAreaDefinitions.TryGetValue(typeof(T), out var value))
		{
			return (T)value;
		}
		throw new Exception(typeof(T).Name + " not found!");
	}

	public bool TryGetTag(int stageNr, string queryTag, out StoryBotDialogueTag storyTag)
	{
		MakeSureInitialized();
		return GetStage(stageNr, queryTag).TryGetTag(queryTag, out storyTag);
	}

	public bool TryGetTag(string queryTag, out StoryBotDialogueTag storyTag)
	{
		return TryGetTag(GameVariables.Current.stage, queryTag, out storyTag);
	}

	private void InitializeStoryBotDialogueEntryPoint()
	{
		DataCondition<int> dataCondition = StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Below, 700);
		DataCondition<int> dataCondition2 = StoryCondition.VariableCondition("Day", DataConditionComparators.SingleArgumentComparators.Above, 2);
		DataCondition<float> dataCondition3 = StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.5f);
		DataCondition<int> sympathyDetest = StoryCondition.CommonConditions.SympathyDetest;
		DataCondition<int> sympathyHate = StoryCondition.CommonConditions.SympathyHate;
		DataCondition<int> sympathyDislike = StoryCondition.CommonConditions.SympathyDislike;
		DataCondition<int> sympathyPositive = StoryCondition.CommonConditions.SympathyPositive;
		DataCondition<int> sympathyLike = StoryCondition.CommonConditions.SympathyLike;
		DataCondition<int> dataCondition4 = StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.AboveEqual, 150);
		DataCondition<int> sympathyAffection = StoryCondition.CommonConditions.SympathyAffection;
		DataCondition<int> sympathyLove = StoryCondition.CommonConditions.SympathyLove;
		DataCondition<int> dataCondition5 = StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.Above, 100);
		DataCondition<int> dataCondition6 = StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.AboveEqual, 300);
		DataCondition<int> dataCondition7 = StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.Above, 300);
		DataCondition<int> dataCondition8 = StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.Below, 500);
		DataCondition<int> dataCondition9 = StoryCondition.VariableCondition("lust", DataConditionComparators.TwoArgumentComparators.Between, 30, 100);
		DataCondition<int> dataCondition10 = StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.AboveEqual, 350);
		DataCondition<int> dataCondition11 = StoryCondition.VariableCondition("Day", DataConditionComparators.SingleArgumentComparators.Above, 15);
		DataCondition<int> dataCondition12 = StoryCondition.VariableCondition("Day", DataConditionComparators.SingleArgumentComparators.AboveEqual, 16);
		DataCondition<int> dataCondition13 = StoryCondition.VariableCondition("Day", DataConditionComparators.SingleArgumentComparators.Above, 9);
		DataCondition<int> dataCondition14 = StoryCondition.VariableCondition("Hour", DataConditionComparators.SingleArgumentComparators.Below, 4);
		DataCondition<int> dataCondition15 = StoryCondition.VariableCondition("Hour", DataConditionComparators.TwoArgumentComparators.Between, 5, 11);
		DataCondition<int> dataCondition16 = StoryCondition.VariableCondition("Hour", DataConditionComparators.SingleArgumentComparators.Below, 3);
		DataCondition<int> dataCondition17 = StoryCondition.VariableCondition("Hour", DataConditionComparators.TwoArgumentComparators.Between, 17, 22);
		DataCondition<int> dataCondition18 = StoryCondition.VariableCondition("Hour", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 5, 21);
		DataCondition<int> dataCondition19 = StoryCondition.VariableCondition("Hour", DataConditionComparators.SingleArgumentComparators.Below, 7);
		DataCondition<int> dataCondition20 = StoryCondition.VariableCondition("Hour", DataConditionComparators.SingleArgumentComparators.BelowEqual, 5);
		DataCondition<int> dataCondition21 = StoryCondition.VariableCondition("Hour", DataConditionComparators.SingleArgumentComparators.BelowEqual, 4);
		DataCondition<int> dataCondition22 = StoryCondition.VariableCondition("Hour", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 7, 20);
		DataCondition<int> dataCondition23 = StoryCondition.VariableCondition("TimesCameAll", DataConditionComparators.SingleArgumentComparators.AboveEqual, 1);
		DataCondition<float> dataCondition24 = StoryCondition.VariableCondition("Stamina", DataConditionComparators.SingleArgumentComparators.Below, 0.1f);
		DataCondition<float> dataCondition25 = StoryCondition.VariableCondition("Stamina", DataConditionComparators.SingleArgumentComparators.Below, 0.3f);
		DataCondition<float> dataCondition26 = StoryCondition.VariableCondition("Stamina", DataConditionComparators.SingleArgumentComparators.Below, 0.2f);
		DataCondition<float> dataCondition27 = StoryCondition.VariableCondition("Stamina", DataConditionComparators.SingleArgumentComparators.Above, 0.2f);
		DataCondition<float> dataCondition28 = StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.2f);
		DataCondition<float> dataCondition29 = StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Above, 0.2f);
		DataCondition<int> dataCondition30 = StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Below, 6000);
		DataCondition<int> dataCondition31 = StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 2000);
		DataCondition<int> dataCondition32 = StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 200);
		DataCondition<int> dataCondition33 = StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Below, 10000);
		DataCondition<int> dataCondition34 = StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 100000);
		DataCondition<int> dataCondition35 = StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Below, 5000);
		FlagConditionWithTime flagConditionWithTime = StoryCondition.FlagSetAfterRelative("dadJoke", 2880, negate: true);
		FlagConditionBasic flagConditionBasic = StoryCondition.FlagSet("chess");
		DataCondition<int> dataCondition36 = StoryCondition.VariableCondition("AwakeFor", DataConditionComparators.SingleArgumentComparators.Below, 60);
		FlagConditionBasic flagConditionBasic2 = StoryCondition.FlagSet("love");
		FlagConditionBasic flagConditionBasic3 = StoryCondition.FlagSet("statistics");
		DataCondition<int> dataCondition37 = StoryCondition.VariableCondition("timesCameInside", DataConditionComparators.SingleArgumentComparators.AboveEqual, 10);
		DataCondition<int> dataCondition38 = StoryCondition.VariableCondition("timesCameInsideAnal", DataConditionComparators.SingleArgumentComparators.AboveEqual, 10);
		FlagConditionBasic flagConditionBasic4 = StoryCondition.FlagSet("GoingOutside3");
		DataCondition<int> dataCondition39 = StoryCondition.VariableCondition("TimeSinceLastWentOutsideWithBot", DataConditionComparators.SingleArgumentComparators.Above, 720);
		DataCondition<float> dataCondition40 = StoryCondition.VariableCondition("sus", DataConditionComparators.SingleArgumentComparators.Above, 0.5f);
		DataCondition<float> dataCondition41 = StoryCondition.VariableCondition("Satiation", DataConditionComparators.SingleArgumentComparators.Below, 0.2f);
		FlagConditionBasic flagConditionBasic5 = StoryCondition.FlagSet("date");
		DataCondition<bool> dataCondition42 = StoryCondition.VariableCondition("HasAllLimbs", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		DataCondition<int> dataCondition43 = StoryCondition.VariableCondition("TimeSinceLastFuck", DataConditionComparators.SingleArgumentComparators.AboveEqual, 2880);
		DataCondition<int> dataCondition44 = StoryCondition.VariableCondition("timesWentToChurch", DataConditionComparators.SingleArgumentComparators.AboveEqual, 2);
		DataCondition<bool> dataCondition45 = StoryCondition.VariableCondition("IsWearingCatEars", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		DataCondition<int> dataCondition46 = StoryCondition.VariableCondition("streamedFor", DataConditionComparators.SingleArgumentComparators.Above, 500);
		DataCondition<bool> dataCondition47 = StoryCondition.VariableCondition("HasAtLeastOneArm", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		DataCondition<bool> dataCondition48 = StoryCondition.VariableCondition("HasAtLeastOneLeg", DataConditionComparators.SingleArgumentComparators.Equal, value: false);
		DataCondition<bool> dataCondition49 = StoryCondition.VariableCondition("HasBothArms", DataConditionComparators.SingleArgumentComparators.Equal, value: false);
		DataCondition<int> dataCondition50 = StoryCondition.VariableCondition("followers", DataConditionComparators.SingleArgumentComparators.Above, 1000);
		DataCondition<float> dataCondition51 = StoryCondition.VariableCondition("MaxCum", DataConditionComparators.SingleArgumentComparators.Below, 40f);
		DataCondition<bool> dataCondition52 = StoryCondition.VariableCondition("BotKnowsAboutStreaming", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		DataCondition<bool> dataCondition53 = StoryCondition.VariableCondition("HasBothArms", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		DataCondition<bool> dataCondition54 = StoryCondition.VariableCondition("HasBothLegs", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		DataCondition<bool> dataCondition55 = StoryCondition.VariableCondition("IsWearingAnyClothes", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		FlagConditionBasic flagConditionBasic6 = StoryCondition.FlagSet("firstClothes");
		AndCondition andCondition = StoryCondition.And(StoryCondition.ItemInSlotCondition(ItemSlot.SlotManager.Panties.Id), StoryCondition.HasItemEquipped(ItemEnum.LingeriePanties.ToGameId(), negate: true));
		ItemCondition itemCondition = StoryCondition.HasItemEquipped(ItemEnum.CuteBow.ToGameId());
		AndCondition andCondition2 = StoryCondition.And(StoryCondition.HasItemEquipped(ItemEnum.StockingL.ToGameId()), StoryCondition.HasItemEquipped(ItemEnum.StockingR.ToGameId()));
		ItemCondition itemCondition2 = StoryCondition.HasItemEquipped(ItemEnum.PleatedSkirt.ToGameId());
		ItemCondition itemCondition3 = StoryCondition.HasItemEquipped(ItemEnum.LingerieBra.ToGameId());
		ItemCondition itemCondition4 = StoryCondition.HasItemEquipped(ItemEnum.LingeriePanties.ToGameId());
		AndCondition andCondition3 = StoryCondition.And(StoryCondition.HasItemEquipped(ItemEnum.LingerieStockingL.ToGameId()), StoryCondition.HasItemEquipped(ItemEnum.LingerieStockingR.ToGameId()));
		DataCondition<int> dataCondition56 = StoryCondition.VariableCondition("timesCameInside", DataConditionComparators.SingleArgumentComparators.AboveEqual, 2);
		DataCondition<int> dataCondition57 = StoryCondition.VariableCondition("timesCameInsideAnal", DataConditionComparators.SingleArgumentComparators.AboveEqual, 2);
		DataCondition<int> dataCondition58 = StoryCondition.VariableCondition("timesCameOutside", DataConditionComparators.SingleArgumentComparators.AboveEqual, 2);
		DataCondition<int> dataCondition59 = StoryCondition.VariableCondition("TimeSinceLastHeadpat", DataConditionComparators.SingleArgumentComparators.AboveEqual, 2880);
		DataCondition<int> dataCondition60 = StoryCondition.VariableCondition("TimeSinceLastFuck", DataConditionComparators.SingleArgumentComparators.Below, 1000);
		DataCondition<int> dataCondition61 = StoryCondition.VariableCondition("TimeSinceLastHeadpat", DataConditionComparators.TwoArgumentComparators.BetweenEqual, 50, 260);
		DataCondition<int> dataCondition62 = StoryCondition.VariableCondition("casinoTokens", DataConditionComparators.SingleArgumentComparators.Above, 10000);
		DataCondition<float> dataCondition63 = StoryCondition.VariableCondition("search", DataConditionComparators.SingleArgumentComparators.Above, 0.5f);
		DataCondition<int> dataCondition64 = StoryCondition.VariableCondition("MoneySpentOnClothes", DataConditionComparators.SingleArgumentComparators.Above, 6000);
		DataCondition<int> dataCondition65 = StoryCondition.VariableCondition("TimeSinceLastStream", DataConditionComparators.SingleArgumentComparators.Below, 1440);
		DataCondition<float> dataCondition66 = StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.4f);
		DataCondition<float> dataCondition67 = StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.3f);
		DataCondition<int> dataCondition68 = StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.Above, 700);
		DataCondition<int> dataCondition69 = StoryCondition.VariableCondition("TimeSinceLastFuck", DataConditionComparators.SingleArgumentComparators.Above, 2880);
		DataCondition<float> dataCondition70 = StoryCondition.VariableCondition("RemainingCum", DataConditionComparators.SingleArgumentComparators.AboveEqual, 15f);
		DataCondition<float> dataCondition71 = StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.AboveEqual, 0.2f);
		DataCondition<bool> dataCondition72 = StoryCondition.VariableCondition("CanHalloweenStart", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		DataCondition<bool> dataCondition73 = StoryCondition.VariableCondition("LandlordDecorationsUp", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		DataCondition<bool> dataCondition74 = StoryCondition.VariableCondition("SpiderPlushUp", DataConditionComparators.SingleArgumentComparators.Equal, value: true);
		_stage1 = new StoryBotDialogueStage("Dialogue/BotDialogueStage1");
		_stage2 = new StoryBotDialogueStage("Dialogue/BotDialogueStage2");
		_stage3 = new StoryBotDialogueStage("Dialogue/BotDialogueStage3");
		_storyBotDialogueAreaDefinitions.Clear();
		Stages stages = new Stages(_stage1, _stage2, _stage3);
		InitArea<Live2DController.KissingDialogueArea>();
		NewInteractBrainState.InteractDialogueArea interactDialogueArea = InitArea<NewInteractBrainState.InteractDialogueArea>();
		InitArea<CuddleBrainState.CuddleDialogue>();
		InitArea<ShowerBrainState.ShowerDialogue>();
		InitArea<FrickDialogueArea>();
		InitArea<BlowjobBrainState.BlowjobDialogueArea>();
		InitArea<CowgirlBrainState.CowgirlSceneDialogueArea>();
		_stage1.AddVanilla(out var main, out var prologue);
		prologue.AddMultipleBranchesWithoutConditions(new StoryBotDialogueBranch(Stage1VanillaDialogueIds.LinearDialogues.GetGameId(0), FungusBlocks.Stage1_Linear1).MarkOneTime(), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.LinearDialogues.GetGameId(100), FungusBlocks.Stage1_Linear2).MarkOneTime(), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.LinearDialogues.GetGameId(300), FungusBlocks.Stage1_Linear3).MarkOneTime());
		main.AddMultipleBranchesWithoutConditions(new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(0), FungusBlocks.Stage1_Generic1), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(1), FungusBlocks.Stage1_Generic2), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(2), FungusBlocks.Stage1_Generic3), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(3), FungusBlocks.Stage1_Generic4), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(4), FungusBlocks.Stage1_Generic5), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(5), FungusBlocks.Stage1_Generic6), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(6), FungusBlocks.Stage1_Generic7), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(7), FungusBlocks.Stage1_Generic8), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(8), FungusBlocks.Stage1_Generic9), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.GenericDialogues.GetGameId(9), FungusBlocks.Stage1_Generic10), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.UniqueDialogues.GetGameId(1), FungusBlocks.Stage1_Unique2).MarkOneTime(), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.UniqueDialogues.GetGameId(3), FungusBlocks.Stage1_Unique4).MarkOneTime(), new StoryBotDialogueBranch(Stage1VanillaDialogueIds.UniqueDialogues.GetGameId(4), FungusBlocks.Stage1_Unique5).MarkOneTime());
		main.AddBranch(new StoryBotDialogueBranch(Stage1VanillaDialogueIds.UniqueDialogues.GetGameId(0), FungusBlocks.Stage1_Unique1).MarkOneTime().AddConditions(dataCondition));
		main.AddBranch(new StoryBotDialogueBranch(Stage1VanillaDialogueIds.UniqueDialogues.GetGameId(2), FungusBlocks.Stage1_Unique3).MarkOneTime().AddConditions(dataCondition2, dataCondition3));
		_stage2.AddVanilla(out var main2, out var prologue2);
		prologue2.AddMultipleBranchesWithoutConditions(new StoryBotDialogueBranch(Stage2VanillaDialogueIds.LinearDialogues.GetGameId(0), FungusBlocks.Stage2_Linear1).MarkOneTime());
		main2.AddMultipleBranchesWithoutConditions(new StoryBotDialogueBranch(Stage2VanillaDialogueIds.GenericDialogues.GetGameId(0), FungusBlocks.Stage2_Generic1), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.GenericDialogues.GetGameId(1), FungusBlocks.Stage2_Generic2), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.GenericDialogues.GetGameId(2), FungusBlocks.Stage2_Generic3), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.UniqueDialogues.GetGameId(0), FungusBlocks.Stage2_Unique1).MarkOneTime(), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.UniqueDialogues.GetGameId(1), FungusBlocks.Stage2_Unique2).MarkOneTime(), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.UniqueDialogues.GetGameId(2), FungusBlocks.Stage2_Unique3).MarkOneTime(), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.UniqueDialogues.GetGameId(3), FungusBlocks.Stage2_Unique4).MarkOneTime(), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.UniqueDialogues.GetGameId(4), FungusBlocks.Stage2_Unique5).MarkOneTime(), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.UniqueDialogues.GetGameId(5), FungusBlocks.Stage2_Unique6).MarkOneTime());
		main2.AddBranch(new StoryBotDialogueBranch(Stage2VanillaDialogueIds.UniqueDialogues.GetGameId(8), FungusBlocks.Stage2_Unique9).MarkOneTime().AddConditions(StoryCondition.FlagSetAfterRelative("coffee", 5760)));
		interactDialogueArea.BotStart.CreateContainer(_stage2, out var o);
		o.AddMultipleBranchesWithoutConditions(new StoryBotDialogueBranch(Stage2VanillaDialogueIds.UniqueDialogues.GetGameId(6), FungusBlocks.Stage2_Unique7).MarkOneTime(), new StoryBotDialogueBranch(Stage2VanillaDialogueIds.UniqueDialogues.GetGameId(7), FungusBlocks.Stage2_Unique8).MarkOneTime());
		_stage3.AddVanilla(out var main3, out var prologue3);
		prologue3.AddMultipleBranchesWithoutConditions(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.LinearDialogues.GetGameId(0), FungusBlocks.Stage3_Linear1).MarkOneTime());
		main3.AddMultipleBranchesWithSameCondition(sympathyDetest, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.DetestDialogues.GetGameId(0), FungusBlocks.Stage3_GenericDetest1), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.DetestDialogues.GetGameId(1), FungusBlocks.Stage3_GenericDetest2), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.DetestDialogues.GetGameId(2), FungusBlocks.Stage3_GenericDetest3));
		main3.AddMultipleBranchesWithSameCondition(sympathyHate, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HateDialogues.GetGameId(0), FungusBlocks.Stage3_GenericHate1), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HateDialogues.GetGameId(1), FungusBlocks.Stage3_GenericHate2));
		main3.AddMultipleBranchesWithSameCondition(sympathyDislike, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.DislikeDialogues.GetGameId(0), FungusBlocks.Stage3_GenericDislike1), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.DislikeDialogues.GetGameId(1), FungusBlocks.Stage3_GenericDislike2), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.DislikeDialogues.GetGameId(2), FungusBlocks.Stage3_GenericDislike3), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.DislikeDialogues.GetGameId(3), FungusBlocks.Stage3_GenericDislike4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.DislikeDialogues.GetGameId(4), FungusBlocks.Stage3_GenericDislike5), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDislikeDialogues.GetGameId(0), FungusBlocks.Stage3_UniqueDislike1).MarkOneTime());
		main3.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(0), FungusBlocks.Stage3_Generic1), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(1), FungusBlocks.Stage3_Generic2), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(2), FungusBlocks.Stage3_Generic3), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(3), FungusBlocks.Stage3_Generic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(4), FungusBlocks.Stage3_Generic5), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericGameDialogues.GetGameId(0), FungusBlocks.Stage3_GenericGame1).AddConditions(flagConditionBasic), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericGameDialogues.GetGameId(1), FungusBlocks.Stage3_GenericGame2).AddConditions(StoryCondition.FlagSet("cards")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericGameDialogues.GetGameId(2), FungusBlocks.Stage3_GenericGame3).AddConditions(StoryCondition.FlagSet("wordchain")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDialogues.GetGameId(0), FungusBlocks.Stage3_Unique1).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDialogues.GetGameId(1), FungusBlocks.Stage3_Unique2).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDialogues.GetGameId(2), FungusBlocks.Stage3_Unique4).MarkOneTime().AddConditions(dataCondition41, flagConditionBasic5), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDialogues.GetGameId(3), FungusBlocks.Stage3_Unique5).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDialogues.GetGameId(4), FungusBlocks.Stage3_Unique6).MarkOneTime().AddConditions(dataCondition42), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDialogues.GetGameId(5), FungusBlocks.Stage3_Unique7).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueGameDialogues.GetGameId(0), FungusBlocks.Stage3_UniqueGame1).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueGameDialogues.GetGameId(1), FungusBlocks.Stage3_UniqueGame2).MarkOneTime().AddConditions(StoryCondition.FlagSetBeforeRelative("chess", 1440)), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueGameDialogues.GetGameId(2), FungusBlocks.Stage3_UniqueGame3).MarkOneTime().AddConditions(StoryCondition.FlagSetBeforeRelative("cards", 1440)));
		main3.AddMultipleBranchesWithSameCondition(sympathyLike, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLikeDialogues.GetGameId(1), FungusBlocks.Stage3_GenericLike2).AddConditions(dataCondition14), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLikeDialogues.GetGameId(3), FungusBlocks.Stage3_GenericLike4).AddConditions(dataCondition9), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(1), FungusBlocks.Stage3_UniqueLike2).MarkOneTime().AddConditions(dataCondition10, dataCondition43, dataCondition23), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(700), FungusBlocks.Stage3_nunBack).MarkOneTime().AddConditions(StoryCondition.FlagSet("church-nun_back5")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HalloweenDialogues.GetGameId(0), FungusBlocks.Stage3_HalloweenEvent1).MarkOneTime().AddConditions(dataCondition71, dataCondition72, StoryCondition.FlagSet("clothier4"), dataCondition11, dataCondition22), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HalloweenDialogues.GetGameId(1), FungusBlocks.Stage3_HalloweenEvent2).MarkOneTime().AddConditions(dataCondition71, dataCondition73), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.HalloweenDialogues.GetGameId(2), FungusBlocks.Stage3_HalloweenEvent3).MarkOneTime().AddConditions(dataCondition71, dataCondition74), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(15), FungusBlocks.Stage3_Melissa4bot).MarkOneTime().AddConditions(StoryCondition.CommonConditions.Melissa4Before5), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(16), FungusBlocks.Stage3_Melissa5bot).MarkOneTime().AddConditions(StoryCondition.CommonConditions.Melissa5Done), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(17), FungusBlocks.Stage3_churchHoboBots2).MarkOneTime().AddConditions(StoryCondition.FlagSet("church_hobo_bots")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(18), FungusBlocks.Stage3_AfterRepairs).MarkOneTime().AddConditions(StoryCondition.FlagSet("ice_skating1")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(19), FungusBlocks.Stage3_RiotsReporter).MarkOneTime().AddConditions(StoryCondition.CommonConditions.RiotsActive), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(21), FungusBlocks.Stage3_SecondPaymentDayLateBotTalk).MarkOneTime().AddConditions(StoryCondition.FlagSet("SecondPaymentDayLate")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(22), FungusBlocks.Stage3_LandlordCockblock3).MarkOneTime().AddConditions(StoryCondition.FlagSet("Landlord_Cockblock2"), dataCondition22, StoryCondition.CommonConditions.EnoughNutForSex));
		main3.AddMultipleBranchesWithSameCondition(sympathyLove, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(7), FungusBlocks.Stage3_GenericLove8).AddConditions(flagConditionBasic2, dataCondition29), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericSLoveDialogues.GetGameId(0), FungusBlocks.Stage3_GenericSLove1).AddConditions(dataCondition7), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(4), FungusBlocks.Stage3_UniqueLove5).MarkOneTime().AddConditions(flagConditionBasic2, dataCondition68, dataCondition69, dataCondition70, StoryCondition.CommonConditions.EverFuckedBot), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(6), FungusBlocks.Stage3_UniqueLove7).MarkOneTime().AddConditions(flagConditionBasic2), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(7), FungusBlocks.Stage3_UniqueLove8).MarkOneTime().AddConditions(flagConditionBasic2, StoryCondition.VariableConditionBoolTrue("ZombieApocalypseDialogueHappened")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(8), FungusBlocks.Stage3_UniqueLove9).MarkOneTime().AddConditions(flagConditionBasic2, StoryCondition.VariableConditionBoolTrue("FirstHorrorStoryDialogueHappened")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(9), FungusBlocks.Stage3_UniqueLove10).MarkOneTime().AddConditions(flagConditionBasic2, StoryCondition.FlagSet("Landlord_Cockblock6")));
		main3.AddMultipleBranchesWithSameCondition(sympathyAffection, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(0), FungusBlocks.Stage3_UniqueAffection1).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(3), FungusBlocks.Stage3_UniqueAffection4).MarkOneTime().AddConditions(flagConditionBasic), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(4), FungusBlocks.Stage3_UniqueAffection5).MarkOneTime().AddConditions(dataCondition59, dataCondition8, dataCondition60), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(6), FungusBlocks.Stage3_UniqueAffection7).MarkOneTime().AddConditions(dataCondition54), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(10), FungusBlocks.Stage3_UniqueAffection11).MarkOneTime().AddConditions(flagConditionBasic4, dataCondition63), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(17), FungusBlocks.Stage3_UniqueAffection18).MarkOneTime().AddConditions(dataCondition54, dataCondition47), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(18), FungusBlocks.Stage3_UniqueAffection19).MarkOneTime().AddConditions(dataCondition54, dataCondition47, dataCondition31, flagConditionBasic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(19), FungusBlocks.Stage3_UniqueAffection20).MarkOneTime().AddConditions(dataCondition54, dataCondition47, dataCondition32, flagConditionBasic5), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(21), FungusBlocks.Stage3_UniqueAffection22).MarkOneTime().AddConditions(dataCondition32, flagConditionBasic5), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(22), FungusBlocks.Stage3_UniqueAffection23).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(24), FungusBlocks.Stage3_UniqueAffection25).MarkOneTime().AddConditions(dataCondition21, dataCondition71, StoryCondition.Delegate(() => SingletonMonoBehaviour<StorySingleton>.Instance.Stage3.GetMemory().IsIgnored(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(10)))), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(25), FungusBlocks.Stage3_UniqueAffection26).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(26), FungusBlocks.Stage3_UniqueAffection27).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(27), FungusBlocks.Stage3_UniqueAffection28).MarkOneTime());
		main3.AddBranch(new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(500), FungusBlocks.Stage3_GoingOutside1).MarkOneTime().AddConditions(dataCondition4, StoryCondition.FlagSet("GoingOutside1")));
		interactDialogueArea.BotStart.CreateContainer(_stage3, out var o2);
		o2.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(5), FungusBlocks.Stage3_Generic6), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(6), FungusBlocks.Stage3_Generic7), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(7), FungusBlocks.Stage3_Generic8).AddConditions(flagConditionWithTime), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(8), FungusBlocks.Stage3_Generic9).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericDialogues.GetGameId(9), FungusBlocks.Stage3_Generic10).AddConditions(dataCondition24), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDialogues.GetGameId(6), FungusBlocks.Stage3_Unique8).MarkOneTime().AddConditions(dataCondition43, dataCondition44), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDialogues.GetGameId(7), FungusBlocks.Stage3_Unique9).MarkOneTime(), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueDialogues.GetGameId(8), FungusBlocks.Stage3_Unique10).MarkOneTime().AddConditions(dataCondition45));
		o2.AddMultipleBranchesWithSameCondition(sympathyLike, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLikeDialogues.GetGameId(0), FungusBlocks.Stage3_GenericLike1).AddConditions(dataCondition24), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLikeDialogues.GetGameId(2), FungusBlocks.Stage3_GenericLike3).AddConditions(dataCondition15, dataCondition36), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLikeDialogues.GetGameId(4), FungusBlocks.Stage3_GenericLike5).AddConditions(dataCondition25), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLikeDialogues.GetGameId(5), FungusBlocks.Stage3_GenericLike6).AddConditions(dataCondition25, dataCondition16), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLikeDialogues.GetGameId(6), FungusBlocks.Stage3_GenericLike7).AddConditions(dataCondition17), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(0), FungusBlocks.Stage3_UniqueLike1).MarkOneTime().AddConditions(dataCondition46), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(2), FungusBlocks.Stage3_UniqueLike3).MarkOneTime().AddConditions(dataCondition13), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(3), FungusBlocks.Stage3_UniqueLike4).MarkOneTime().AddConditions(dataCondition28), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(4), FungusBlocks.Stage3_UniqueLike5).MarkOneTime().AddConditions(dataCondition47, dataCondition48, dataCondition49), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(5), FungusBlocks.Stage3_UniqueLike6).MarkOneTime().AddConditions(dataCondition50, dataCondition51, dataCondition52), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(6), FungusBlocks.Stage3_UniqueLike7).MarkOneTime().AddConditions(dataCondition53, dataCondition54), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(7), FungusBlocks.Stage3_FirstClothes).MarkOneTime().AddConditions(dataCondition55, flagConditionBasic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(8), FungusBlocks.Stage3_Panties).MarkOneTime().AddConditions(flagConditionBasic6, andCondition, flagConditionBasic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(9), FungusBlocks.Stage3_CuteBow).MarkOneTime().AddConditions(flagConditionBasic6, itemCondition, flagConditionBasic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(10), FungusBlocks.Stage3_Stockings).MarkOneTime().AddConditions(flagConditionBasic6, andCondition2, flagConditionBasic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(11), FungusBlocks.Stage3_PleatedSkirt).MarkOneTime().AddConditions(flagConditionBasic6, itemCondition2, flagConditionBasic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(12), FungusBlocks.Stage3_LingerieBra).MarkOneTime().AddConditions(flagConditionBasic6, itemCondition3, flagConditionBasic4, StoryCondition.CommonConditions.EverStartedStream), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(13), FungusBlocks.Stage3_LingeriePanties).MarkOneTime().AddConditions(flagConditionBasic6, itemCondition4, flagConditionBasic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(14), FungusBlocks.Stage3_LingerieStockings).MarkOneTime().AddConditions(flagConditionBasic6, andCondition3, flagConditionBasic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLikeDialogues.GetGameId(20), FungusBlocks.Stage3_AnnalieHairstyle).MarkOneTime().AddConditions(StoryCondition.CommonConditions.BotMetAnnalie, StoryCondition.FlagNotSet("AnnalieHairstyleComment"), StoryCondition.CommonConditions.HasAnnalieHairstyle));
		o2.AddMultipleBranchesWithSameCondition(sympathyLove, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(0), FungusBlocks.Stage3_GenericLove1).AddConditions(dataCondition7), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(1), FungusBlocks.Stage3_GenericLove2).AddConditions(flagConditionBasic2), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(2), FungusBlocks.Stage3_GenericLove3).AddConditions(dataCondition5), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(3), FungusBlocks.Stage3_GenericLove4).AddConditions(dataCondition7), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(4), FungusBlocks.Stage3_GenericLove5).AddConditions(dataCondition7, flagConditionBasic3, dataCondition37), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(5), FungusBlocks.Stage3_GenericLove6).AddConditions(dataCondition7, flagConditionBasic3, dataCondition38), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(6), FungusBlocks.Stage3_GenericLove7).AddConditions(dataCondition7, dataCondition28), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(8), FungusBlocks.Stage3_GenericLove10).AddConditions(dataCondition40, dataCondition30), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(9), FungusBlocks.Stage3_GenericLove11).AddConditions(flagConditionBasic4, dataCondition39), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(10), FungusBlocks.Stage3_GenericLove12).AddConditions(flagConditionBasic4, dataCondition39, dataCondition26, dataCondition19), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericLoveDialogues.GetGameId(11), FungusBlocks.Stage3_GenericLove13).AddConditions(flagConditionBasic4, dataCondition39, dataCondition27, dataCondition18), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.GenericSLoveDialogues.GetGameId(1), FungusBlocks.Stage3_GenericSLove2).AddConditions(dataCondition26, StoryCondition.CommonConditions.SleepTogetherUnlocked), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(0), FungusBlocks.Stage3_UniqueLove1).MarkOneTime().SetOdds(500f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(1), FungusBlocks.Stage3_UniqueLove2).MarkOneTime().AddConditions(StoryCondition.FlagSet("prelove")).SetOdds(500f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(2), FungusBlocks.Stage3_UniqueLove3).MarkOneTime().AddConditions(flagConditionBasic2, dataCondition66), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(3), FungusBlocks.Stage3_UniqueLove4).MarkOneTime().AddConditions(flagConditionBasic2, dataCondition67, dataCondition12), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(5), FungusBlocks.Stage3_UniqueLove6).MarkOneTime().AddConditions(flagConditionBasic2, StoryCondition.FlagSet("psychologist1")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(10), FungusBlocks.Stage3_UniqueLove11).MarkOneTime().AddConditions(flagConditionBasic2, StoryCondition.FlagSet("housewife9"), StoryCondition.VariableCondition("timesCameInsideAnal", DataConditionComparators.SingleArgumentComparators.Above, 15), StoryCondition.VariableCondition("TimesShoweredTogether", DataConditionComparators.SingleArgumentComparators.Above, 5)), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueLoveDialogues.GetGameId(11), FungusBlocks.Stage3_UniqueLove12).MarkOneTime().AddConditions(flagConditionBasic2, StoryCondition.FlagSet("Fish_joint5")));
		o2.AddMultipleBranchesWithSameCondition(sympathyAffection, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(1), FungusBlocks.Stage3_UniqueAffection2).MarkOneTime().AddConditions(dataCondition6, dataCondition56, dataCondition57, dataCondition58), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(2), FungusBlocks.Stage3_UniqueAffection3).MarkOneTime().AddConditions(dataCondition43, StoryCondition.FlagSet("church-endJustGoThereGood"), StoryCondition.FlagNotSet("church-MeetingNunWithTheBot")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(5), FungusBlocks.Stage3_UniqueAffection6).MarkOneTime().AddConditions(dataCondition61), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(7), FungusBlocks.Stage3_UniqueAffection8).MarkOneTime().AddConditions(dataCondition34), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(8), FungusBlocks.Stage3_UniqueAffection9).MarkOneTime().AddConditions(dataCondition35, dataCondition62), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(9), FungusBlocks.Stage3_UniqueAffection10).MarkOneTime().AddConditions(dataCondition21).SetOdds(5f), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(11), FungusBlocks.Stage3_UniqueAffection12).MarkOneTime().AddConditions(flagConditionBasic4, dataCondition20), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(12), FungusBlocks.Stage3_UniqueAffection13).MarkOneTime().AddConditions(StoryCondition.FlagSet("LandlordConspiracy2")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(13), FungusBlocks.Stage3_UniqueAffection14).MarkOneTime().AddConditions(StoryCondition.FlagSet("LandlordConspiracy3")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(14), FungusBlocks.Stage3_UniqueAffection15).MarkOneTime().AddConditions(dataCondition64, dataCondition33), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(15), FungusBlocks.Stage3_UniqueAffection16).MarkOneTime().AddConditions(flagConditionBasic4, dataCondition64, dataCondition65, StoryCondition.CommonConditions.EverStartedStream), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(16), FungusBlocks.Stage3_UniqueAffection17).MarkOneTime().AddConditions(flagConditionBasic4), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(20), FungusBlocks.Stage3_UniqueAffection21).MarkOneTime().AddConditions(dataCondition32, StoryCondition.FlagSet("kino1")), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.UniqueAffectionDialogues.GetGameId(23), FungusBlocks.Stage3_UniqueAffection24).MarkOneTime().AddConditions(StoryCondition.FlagSet("interact5")));
		PreparePersonalityDefinitions();
		foreach (StoryBotDialogueStage allStage in GetAllStages())
		{
			allStage.InitVanilla();
		}
		T InitArea<T>() where T : StoryBotDialogueAreaDefinition, new()
		{
			T val = new T();
			_storyBotDialogueAreaDefinitions.Add(typeof(T), val);
			val.Init(this, stages);
			return val;
		}
	}

	public StoryAutoEntryPoint GetStoryAutoEntryPoint(string id)
	{
		MakeSureInitialized();
		return _storyAutoEntryPoints.FirstOrDefault((StoryAutoEntryPoint t) => t.Id == id);
	}

	private void InitializeStoryEntryPoints()
	{
		_storyAutoEntryPoints = _storyAutoEntryPoints.ClearOrCreate();
		DataCondition<bool> afterRiots = StoryCondition.CommonConditions.AfterRiots;
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.BeforeFallAsleepAlone.ToString(), new StoryAutoBranch(FungusBlocks.Sleep_SleepTogetherEvent, "SleepingWithBotUnlocked", StoryCondition.FlagSet("love"), StoryCondition.CommonConditions.IsBotSmart, StoryCondition.VariableCondition("Mood", DataConditionComparators.SingleArgumentComparators.Above, -0.1f), StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 200), StoryCondition.DayAbove(20))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Sleep_BotFullLeadSex, "FirstBotRaepMc", StoryCondition.FlagSet("love"), StoryCondition.CommonConditions.IsBotSmart, StoryCondition.CommonConditions.SympathyLove, StoryCondition.CommonConditions.VeryHighLonging, StoryCondition.CommonConditions.EnoughNutForSex, StoryCondition.CommonConditions.CanDoCowgirl, StoryCondition.DayAbove(25), StoryCondition.VariableCondition("mlOfCumWasted", DataConditionComparators.SingleArgumentComparators.Above, 10f)), new StoryAutoBranch(FungusBlocks.Sleep_BotFullLeadSex, null, StoryCondition.FlagSetBeforeRelative("botRaep", 5760), StoryCondition.FlagSet("FirstBotRaepMc"), StoryCondition.CommonConditions.IsBotSmart, StoryCondition.CommonConditions.SympathyLove, StoryCondition.CommonConditions.VeryHighLonging, StoryCondition.CommonConditions.EnoughNutForSex, StoryCondition.CommonConditions.CanDoCowgirl)
		{
			RandomOdds = 0.2f
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.OnWakeUp.ToString(), new StoryAutoBranch(FungusBlocks.WakeUp_Schizo_End, null, StoryCondition.FlagSet("schizo"), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.001f), StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 100)), new StoryAutoBranch(FungusBlocks.WakeUp_FishjointDream, "Fish_joint_dream", StoryCondition.Or(StoryCondition.FlagSet("Fish_joint3"), StoryCondition.FlagSet("Fish_joint2.5_Priestbot")), StoryCondition.FlagNotSet("Fish_joint5"), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.1f)), new StoryAutoBranch(FungusBlocks.WakeUp_Headache_hash3, "Headache3", StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.3f), StoryCondition.DayAbove(15)), new StoryAutoBranch(FungusBlocks.WakeUp_BotBringsFood, "RiotsBotBringsFood", StoryCondition.CommonConditions.IsBotSmart, StoryCondition.CommonConditions.SympathyLike, StoryCondition.CommonConditions.RiotsActive, StoryCondition.VariableCondition("Satiation", DataConditionComparators.SingleArgumentComparators.Below, 0.3f)), new StoryAutoBranch(FungusBlocks.WakeUp_Housewife7, "housewife7", StoryCondition.FlagSetAfterRelative("outofstamina", 60), StoryCondition.CommonConditions.SympathyLove, StoryCondition.CommonConditions.AfterRiots)));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.AfterFrick.ToString(), new StoryAutoBranch(FungusBlocks.Sex_After_First_Stream_Frick, "AfterFirstFuck", StoryCondition.FlagNotSet("DidntStreamAfterPrologue")), new StoryAutoBranch(FungusBlocks.Sex_After_LandlordCockblock1, true, "Landlord_Cockblock1", StoryCondition.CommonConditions.LateAtNight, StoryCondition.CommonConditions.Melissa5Done, StoryCondition.CommonConditions.MadeNoiseLastSex, StoryCondition.CommonConditions.LandlordKnowsAboutBot), new StoryAutoBranch(FungusBlocks.Sex_After_LandlordCockblock2, true, "Landlord_Cockblock2", StoryCondition.CommonConditions.LateAtNight, StoryCondition.FlagSet("Landlord_Cockblock1"), StoryCondition.CommonConditions.MadeNoiseLastSex), new StoryAutoBranch(FungusBlocks.Sex_After_LandlordCockblock3dot5, true, "Landlord_Cockblock3.5", StoryCondition.FlagSet("Landlord_Cockblock3"), StoryCondition.FlagNotSet("Landlord_Cockblock_Recorded"))
		{
			IsFlagBlocking = false
		}, new StoryAutoBranch(FungusBlocks.Sex_After_LandlordCockblockGeneric, true, "Landlord_CockblockGeneric", StoryCondition.CommonConditions.LateAtNight, StoryCondition.FlagSet("Landlord_Cockblock2"), StoryCondition.FlagNotSet("Landlord_Cockblock4"), StoryCondition.CommonConditions.MadeNoiseLastSex)
		{
			IsFlagBlocking = false
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.AfterFrick_CumOnHer.ToString(), new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGeneric, null, StoryCondition.VariableConditionBoolFalse("IsBotSmart")), new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_Entry, null, StoryCondition.VariableConditionBoolTrue("IsBotSmart"))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.AfterFrick_CumOnHerGenericSmart.ToString(), new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_Start_Label_Bot1, null)
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_Start_Label_Bot2, null)
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_Start_Label_Bot3, null)
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_Start_Label_Anon1, null)
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_Start_Label_Anon2, null)
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_Start_Label_Anon3, null)
		{
			RandomOdds = 0.5f
		})
		{
			NothingChosenOdds = 0f
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.AfterFrick_CumOnHerGenericSmart_LeaveOnHer.ToString(), new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_Uninterested, null, StoryCondition.CommonConditions.NotHorny)
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_Horny, null, StoryCondition.CommonConditions.Horny)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_VeryHorny, null, StoryCondition.CommonConditions.VeryHorny)
		{
			RandomOdds = 2f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_UsuallyCleans, null, StoryCondition.CommonConditions.UsuallyCleansCumAfterSex)
		{
			RandomOdds = 2f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_UsuallyLeavesUninterested, null, StoryCondition.CommonConditions.UsuallyLeavesCumAfterSex, StoryCondition.CommonConditions.NotHorny)
		{
			RandomOdds = 2f
		}, new StoryAutoBranch(FungusBlocks.Sex_After_CumOnHerGenericSmart_LeaveGeneric_Label_UsuallyLeavesHorny, null, StoryCondition.CommonConditions.UsuallyLeavesCumAfterSex, StoryCondition.CommonConditions.Horny)
		{
			RandomOdds = 2f
		})
		{
			NothingChosenOdds = 0f
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Bathroom.ToString(), new StoryAutoBranch(FungusBlocks.Bathroom_Bathroom4, "Bathroom4"), new StoryAutoBranch(FungusBlocks.Bathroom_Generic, null)));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ReturnHome.ToString(), new StoryAutoBranch(FungusBlocks.ReturnHome_Coffee_event, "coffee", StoryCondition.DayAbove(3), StoryCondition.HourBetweenEqual(12, 22)), new StoryAutoBranch(FungusBlocks.Return_Home_Housewife1, "housewife1", StoryCondition.CommonConditions.SympathyAffection, StoryCondition.MoneyAbove(2000), StoryCondition.CommonConditions.HasLegsAndOneArm, StoryCondition.CommonConditions.IsBotSmart), new StoryAutoBranch(FungusBlocks.Return_Home_Housewife2, "housewife2", StoryCondition.FlagSetBeforeRelative("housewife1", 1200), StoryCondition.CommonConditions.SympathyAffection, StoryCondition.CommonConditions.HasLegsAndOneArm), new StoryAutoBranch(FungusBlocks.Return_Home_Housewife3, "housewife3", StoryCondition.FlagSetBeforeRelative("housewife2", 1200), StoryCondition.MoneyAbove(2000), StoryCondition.CommonConditions.SympathyAffection, StoryCondition.CommonConditions.HasLegsAndOneArm), new StoryAutoBranch(FungusBlocks.Return_Home_Housewife4, "housewife4", StoryCondition.FlagSetBeforeRelative("housewife3", 1200), StoryCondition.MoneyAbove(2000), StoryCondition.CommonConditions.SympathyAffection, StoryCondition.CommonConditions.HasLegsAndOneArm), new StoryAutoBranch(FungusBlocks.Return_Home_Housewife5, "housewife5", StoryCondition.FlagSetBeforeRelative("housewife4", 1200), StoryCondition.CommonConditions.SympathyLove, StoryCondition.CommonConditions.HasLegsAndOneArm), new StoryAutoBranch(FungusBlocks.Return_Home_Housewife6, "housewife6", StoryCondition.FlagSetBeforeRelative("housewife5", 1200), StoryCondition.CommonConditions.SympathyLove, StoryCondition.CommonConditions.HasLegsAndOneArm), new StoryAutoBranch(FungusBlocks.Return_Home_Beans, "housewife_beans", StoryCondition.CommonConditions.CanStartPoorReturnHomeFoodEvent, StoryCondition.HasItem(ItemEnum.CanOfBakedBeans.ToGameId()), StoryCondition.HasItem(ItemEnum.BreadFromDispenser.ToGameId()))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Return_Home_SausageOnions, "housewife_sausageonions", StoryCondition.CommonConditions.CanStartPoorReturnHomeFoodEvent, StoryCondition.HourBetweenEqual(12, 17), StoryCondition.HasItem(ItemEnum.Sausage.ToGameId()), StoryCondition.HasItem(ItemEnum.Onions.ToGameId()), StoryCondition.HasItem(ItemEnum.Soda.ToGameId()))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Return_Home_DrinkTea, "housewife_tea", StoryCondition.CommonConditions.CanStartAnyReturnHomeFoodEvent, StoryCondition.HourBetweenEqual(17, 12), StoryCondition.CommonConditions.SympathyLove)
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Return_Home_Bread, "housewife_bread", StoryCondition.CommonConditions.CanStartAnyReturnHomeFoodEvent, StoryCondition.HourBetweenEqual(7, 11), StoryCondition.CommonConditions.SympathyLove, StoryCondition.HasItem(ItemEnum.Flour.ToGameId()), StoryCondition.HasItem(ItemEnum.Eggs.ToGameId()), StoryCondition.HasItem(ItemEnum.Milk.ToGameId()), StoryCondition.HasItem(ItemEnum.Tomato.ToGameId()), StoryCondition.HasItem(ItemEnum.ChickenMeat.ToGameId()))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Return_Home_DrinkHotChocolate, "housewife_hotchocolate", StoryCondition.CommonConditions.CanStartAnyReturnHomeFoodEvent, StoryCondition.HourBetweenEqual(17, 12), StoryCondition.CommonConditions.SympathyLove, StoryCondition.HasItem(ItemEnum.Milk.ToGameId()))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Return_Home_OnionRings, "housewife_onionrings", StoryCondition.CommonConditions.CanStartAnyReturnHomeFoodEvent, StoryCondition.CommonConditions.SympathyLove, StoryCondition.HasItem(ItemEnum.Onions.ToGameId()), StoryCondition.HasItem(ItemEnum.Eggs.ToGameId()), StoryCondition.HasItem(ItemEnum.Flour.ToGameId()))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Return_Home_DrinkCarrotJuice, "housewife_carrotjuice", StoryCondition.CommonConditions.CanStartAnyReturnHomeFoodEvent, StoryCondition.CommonConditions.SympathyLove, StoryCondition.HasItem(ItemEnum.Carrot.ToGameId()))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Return_Home_SteakWithPotatoes, "housewife_steakpotatoes", StoryCondition.CommonConditions.CanStartAnyReturnHomeFoodEvent, StoryCondition.CommonConditions.SympathyLove, StoryCondition.HasItem(ItemEnum.Steak.ToGameId()), StoryCondition.HasItem(ItemEnum.Potatoes.ToGameId()), StoryCondition.HasItem(ItemEnum.Garlic.ToGameId()))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Return_Home_Housewife8, "housewife8", StoryCondition.FlagSetBeforeRelative("housewife7", 1200), StoryCondition.FlagSet("richpeopleshop_membership"), StoryCondition.CommonConditions.SympathyLove, StoryCondition.CommonConditions.HasLegsAndOneArm, StoryCondition.VariableCondition("CookedDishesCount", DataConditionComparators.SingleArgumentComparators.Above, 10), StoryCondition.VariableCondition("DishesPassedAtLeastBCount", DataConditionComparators.SingleArgumentComparators.Above, 2), StoryCondition.VariableConditionBoolTrue("HasAtLeast5UnhealthyFoods")), new StoryAutoBranch(FungusBlocks.Return_Home_Housewife10, "housewife10", StoryCondition.FlagSetBeforeRelative("housewife6", 1200), StoryCondition.CommonConditions.SympathyLove, StoryCondition.FlagSet("interact6"))
		{
			RandomOdds = 0.1f
		}, new StoryAutoBranch(FungusBlocks.Return_Home_Housewife11, "housewife11", StoryCondition.FlagSetBeforeRelative("housewife_tea", 1200), StoryCondition.CommonConditions.SympathyLove, StoryCondition.FlagSet("compliments"), StoryCondition.FlagSet("richpeopleshop_membership"), StoryCondition.HourBetweenEqual(6, 11), StoryCondition.MoneyAbove(10000), StoryCondition.VariableCondition("Satiation", DataConditionComparators.SingleArgumentComparators.Below, 0.15f))
		{
			RandomOdds = 0.5f
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.OutsideLeaveRoom.ToString(), new StoryAutoBranch(FungusBlocks.Outside_JoinUs7Richard3, "joinus7_richard3", StoryCondition.FlagSet("joinus7_richard2")), new StoryAutoBranch(FungusBlocks.StaircaseAlone_Melissa4Landlord, "Melissa4Landlord", StoryCondition.CommonConditions.Melissa4Before5NotRejection), new StoryAutoBranch(FungusBlocks.LandlordMoneyEvent_Start, "LandlordMoneyEvent", StoryCondition.MoneyAbove(10000), StoryCondition.DayAbove(14)), new StoryAutoBranch(FungusBlocks.StaircaseAlone_LandlordESL1, "Landlord_ending1", StoryCondition.FlagSet("Annalie_ending1"), StoryCondition.FlagNotSet("Annalie_ending2"), StoryCondition.FlagSet("helpedlandlord"), StoryCondition.FlagSet("good_soul"), StoryCondition.CommonConditions.LandlordNotAsleep, StoryCondition.FlagNotSet("ShadyLawyerTrickedLandlord"))
		{
			RandomOdds = 0.4f
		}, new StoryAutoBranch(FungusBlocks.StaircaseAlone_LandlordESL2, "Landlord_ending2", StoryCondition.FlagSet("Landlord_ending1_accepted"), StoryCondition.MoneyAbove(11000000), StoryCondition.CommonConditions.LandlordNotAsleep, StoryCondition.FlagNotSet("Landlord_ending2_gamemoney")), new StoryAutoBranch(FungusBlocks.StaircaseAlone_LandlordESL2, "Landlord_ending2", StoryCondition.FlagSet("Landlord_ending1_accepted"), StoryCondition.MoneyAbove(11000000), StoryCondition.CommonConditions.LandlordNotAsleep, StoryCondition.FlagNotSet("Landlord_ending2_gamemoney"))
		{
			IsFlagBlocking = false,
			RandomOdds = 0.2f
		}, new StoryAutoBranch(FungusBlocks.StaircaseAlone_LandlordESL3, "Landlord_ending3", StoryCondition.FlagSetBeforeRelative("Landlord_ending2_gamemoney", 288), StoryCondition.CommonConditions.LandlordNotAsleep, StoryCondition.MoneyAbove(1000000))
		{
			RandomOdds = 0.33f
		}, new StoryAutoBranch(FungusBlocks.StaircaseAlone_LandlordESL4, "Landlord_ending4", StoryCondition.FlagSetBeforeRelative("Landlord_ending3", 288), StoryCondition.CommonConditions.LandlordNotAsleep, StoryCondition.MoneyAbove(1000000), StoryCondition.VariableConditionBoolTrue("HasAtLeastOneArm"))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.StaircaseAlone_LandlordESL5, "Landlord_ending5", StoryCondition.FlagSetBeforeRelative("Landlord_ending4", 288), StoryCondition.CommonConditions.LandlordNotAsleep, StoryCondition.MoneyAbove(1000000))
		{
			RandomOdds = 0.6f
		}, new StoryAutoBranch(FungusBlocks.StaircaseAlone_Bathroom1, "Bathroom1", StoryCondition.CommonConditions.AfterRiots, StoryCondition.MoneyAbove(100000), StoryCondition.CommonConditions.SympathyLove, StoryCondition.FlagSetBeforeRelative("FirstPaymentAfterBotDiscovery", 480), StoryCondition.CommonConditions.HasTheOptionToGoOutsideWithBot, StoryCondition.CommonConditions.LandlordNotAsleep), new StoryAutoBranch(FungusBlocks.StaircaseAlone_Bathroom2, null, StoryCondition.FlagNotSet("Bathroom2_bought"), StoryCondition.FlagSet("Bathroom1"), StoryCondition.FlagSetAfterRelative("Bathroom2", 60, negate: true), StoryCondition.Or(StoryCondition.And(StoryCondition.MoneyAbove(100000), StoryCondition.FlagSet("Bathroom1_100k")), StoryCondition.MoneyAbove(200000)), StoryCondition.CommonConditions.LandlordNotAsleep)));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.OutsideBeforeMenu.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Stonks, "stonks"), new StoryAutoBranch(FungusBlocks.Outside_RiotsStart, "riots_start_event", StoryCondition.DayAbove(28), StoryCondition.HourBetweenEqual(12, 20), StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 6000), StoryCondition.VariableConditionBoolFalse("MajorOutsideEventActive"), StoryCondition.FlagSetBeforeRelative("GoingOutside3", 2880), StoryCondition.VariableConditionBoolTrue("AnyChurchEndingDone"), StoryCondition.Or(StoryCondition.FlagNotSet("church-endJustGoThereGood"), StoryCondition.FlagSet("church-nun_back4"))), new StoryAutoBranch(FungusBlocks.Outside_AfterRiotsNotice, true, "RiotsEndNotice", StoryCondition.CommonConditions.AfterRiots), new StoryAutoBranch(FungusBlocks.Outside_RiotsEndingSoonNotice, true, "RiotsEndingSoonNotice", StoryCondition.CommonConditions.RiotsEndingSoon), new StoryAutoBranch(FungusBlocks.Outside_FishJointEnding, "Fish_joint_ending", StoryCondition.FlagSet("Fish_joint_dream"), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.01f)), new StoryAutoBranch(FungusBlocks.Outside_MentalHealthSpook, null, StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.51f), StoryCondition.VariableConditionBoolTrue("TimeForNextMentalHealthInfo"))
		{
			OnActivateAction = () =>
			{
				GameVariables.Current.lastMentalHealthInfoAt = GameVariables.Current.time;
			}
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.PayRent_Before.ToString(), new StoryAutoBranch(FungusBlocks.RE_Rent_FirstAfterBotDiscovery, "FirstPaymentAfterBotDiscovery", StoryCondition.FlagSet("LandlordConspiracy2")), new StoryAutoBranch(FungusBlocks.RE_Rent_FirstAfterBathroom, "FirstPaymentAfterBathroom", StoryCondition.FlagSet("Bathroom2_bought")), new StoryAutoBranch(FungusBlocks.RE_Rent_FirstDayLate, "FirstPaymentDayLate", StoryCondition.VariableConditionBoolTrue("IsAboveADayLateOnRent")), new StoryAutoBranch(FungusBlocks.RE_Rent_SecondDayLate, "SecondPaymentDayLate", StoryCondition.FlagSet("FirstPaymentDayLate"), StoryCondition.VariableConditionBoolTrue("IsAboveADayLateOnRent")), new StoryAutoBranch(FungusBlocks.RE_Rent_FirstAfterShadyLawyer, "FirstPaymentAfterShadyLawyer", StoryCondition.FlagSet("ShadyLawyerTrickedLandlord")), new StoryAutoBranch(FungusBlocks.Re_Rent_DayLateGeneric, null, StoryCondition.FlagSet("SecondPaymentDayLate"), StoryCondition.VariableConditionBoolTrue("IsAboveADayLateOnRent")), new StoryAutoBranch(FungusBlocks.RE_Rent_FirstEvening, "FirstPaymentEvening", StoryCondition.HourBetweenEqual(20, 24)), new StoryAutoBranch(FungusBlocks.RE_Rent_FirstAfterRiots, "FirstPaymentAfterRiots", StoryCondition.CommonConditions.AfterRiots), new StoryAutoBranch(FungusBlocks.RE_Rent_LandlordDaughter2, "LandlordDaughter2", StoryCondition.FlagSetBeforeRelative("LandlordDaughter1", 720)), new StoryAutoBranch(FungusBlocks.RE_Rent_GoodSoul, "FirstPaymentGoodSoul", StoryCondition.FlagSetBeforeRelative("good_soul", 1440)), new StoryAutoBranch(FungusBlocks.RE_Rent_RichPeopleShop, "FirstPaymentRichPeopleShop", StoryCondition.FlagSet("richpeopleshop_membership")), new StoryAutoBranch(FungusBlocks.RE_Rent_FirstFishing, "FirstPaymentFishing", StoryCondition.FlagSet("fishing3")), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C1, true, null, 2f, StoryCondition.DayBelow(21)), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C2, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C3, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C4, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C5, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C6, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C7, true, null, 8f, StoryCondition.HourBetweenEqual(20, 24)), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C8, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C9, true, null, 8f, StoryCondition.HourBetweenEqual(4, 9)), new StoryAutoBranch(FungusBlocks.RE_Rent_BeforeGeneric_Label_C10, true, null, 1f))
		{
			NothingChosenOdds = 0f
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.PayRent_After.ToString(), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C1, true, null, 2f), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C1, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C2, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C3, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C4, true, null, 0.4f, StoryCondition.MoneyAbove(100000)), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C5, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C6, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C7, true, null, 1f), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C8, true, null, 0.5f), new StoryAutoBranch(FungusBlocks.RE_Rent_AfterGeneric_Label_C9, true, null, 1f, StoryCondition.CommonConditions.LandlordKnowsAboutBot))
		{
			NothingChosenOdds = 0f
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Walk.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Halloween_TrickOrTreat, "HalloweenTrickOrTreat", StoryCondition.VariableConditionBoolTrue("HalloweenActive"), StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 100)), new StoryAutoBranch(FungusBlocks.Outside_anotherrobopriest, "Priestbot_Mark_1", StoryCondition.FlagSet("church-endJustGoThereGood"), StoryCondition.HourBetweenEqual(9, 19)), new StoryAutoBranch(FungusBlocks.Outside_NunBack1, "church-nun_back1", StoryCondition.FlagSetBeforeRelative("Priestbot_Mark_1", 2880), StoryCondition.FlagSetBeforeRelative("church-whatHappenedAfter", 2880), StoryCondition.HourBetweenEqual(7, 20), StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 50), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Above, 0.3f)), new StoryAutoBranch(FungusBlocks.Outside_junkie, "hobo", StoryCondition.HourBetweenEqual(22, 4), StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 1000)), new StoryAutoBranch(FungusBlocks.Outside_MeetingMrsFisher1, "fishing1", StoryCondition.HourBetweenEqual(5, 16), StoryCondition.VariableCondition("Day", DataConditionComparators.SingleArgumentComparators.AboveEqual, 3)), new StoryAutoBranch(FungusBlocks.Outside_RandomSearch1, "randomsearch1", StoryCondition.DayAbove(16), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Above, 0.2f), StoryCondition.HourBetweenEqual(23, 5), StoryCondition.Or(StoryCondition.FlagNotSet("joinus"), StoryCondition.FlagSet("JoinUs2"), StoryCondition.FlagSet("joinusemail2rejected"))), new StoryAutoBranch(FungusBlocks.Outside_Hobo1, "hobo1eventHappened", StoryCondition.VariableCondition("Day", DataConditionComparators.SingleArgumentComparators.Above, 10), StoryCondition.FlagSetBeforeRelative("hobo", 720), StoryCondition.HourBetweenEqual(23, 5)), new StoryAutoBranch(FungusBlocks.Outside_RandomSearch2, "randomsearch2", StoryCondition.FlagSetBeforeRelative("randomsearch1", 720), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Above, 0.2f), StoryCondition.HourBetweenEqual(23, 5)), new StoryAutoBranch(FungusBlocks.Outside_Hobo2, "hobo2", StoryCondition.FlagSetBeforeRelative("hobo1", 720)), new StoryAutoBranch(FungusBlocks.Outside_Melissa4Hobo, "Melissa4Hobo", StoryCondition.CommonConditions.Melissa4Before5NotRejection, StoryCondition.FlagSetBeforeRelative("hobo2", 720)), new StoryAutoBranch(FungusBlocks.Outside_BigdaddyStoryline1, "bigdaddystoryline1", StoryCondition.FlagSetBeforeRelative("bigDaddysAdviceFinished", 1440), StoryCondition.HourBetweenEqual(19, 3)), new StoryAutoBranch(FungusBlocks.Outside_NunInvestigation1, "nunInvestigation1", StoryCondition.FlagSetBeforeRelative("hobo2", 720), StoryCondition.FlagSetBeforeRelative("church-endTheWorstEnd", 2880)), new StoryAutoBranch(FungusBlocks.Outside_Melissa1, "Melissa1", StoryCondition.FlagSetBeforeRelative("Melissa", 720)), new StoryAutoBranch(FungusBlocks.Outside_Melissa6, "Melissa6", StoryCondition.HourBetweenEqual(10, 17), StoryCondition.CommonConditions.Melissa5Done, StoryCondition.FlagSet("bowling1"), StoryCondition.FlagSet("Annalie_cafe2")), new StoryAutoBranch(FungusBlocks.Outside_IslandPrologue1, "Island_Prologue1", StoryCondition.CommonConditions.CanStartIslandEnding), new StoryAutoBranch(FungusBlocks.Outside_IslandPrologue2, "Island_Prologue2", StoryCondition.FlagSet("Island_Prologue1"), StoryCondition.FlagNotSet("willing_to_leave")), new StoryAutoBranch(FungusBlocks.Outside_IslandPrologue3, "Island_Prologue3", StoryCondition.FlagSet("Island_Prologue2"), StoryCondition.FlagNotSet("willing_to_leave")), new StoryAutoBranch(FungusBlocks.Outside_LandlordESL6, "Landlord_ending6", StoryCondition.FlagSet("Landlord_ending5"), StoryCondition.HourBetweenEqual(12, 23), StoryCondition.MoneyAbove(1000000)), new StoryAutoBranch(FungusBlocks.Outside_FishJointWalk, "Fish_joint6_walk", StoryCondition.FlagSet("Fish_joint5")), new StoryAutoBranch(FungusBlocks.Outside_Psychologist1, "psychologist1", StoryCondition.FlagSetBeforeRelative("BlaubeimMedicine", 1440), StoryCondition.HourBetweenEqual(23, 5))
		{
			RandomOdds = 0.2f
		}, new StoryAutoBranch(FungusBlocks.Outside_Lorerant1, "lorerant1", StoryCondition.HourBetweenEqual(23, 5), StoryCondition.DayAbove(8))
		{
			RandomOdds = 0.2f
		}, new StoryAutoBranch(FungusBlocks.Outside_trashdiving, "trashdiving", StoryCondition.DayAbove(17), StoryCondition.HourBetweenEqual(23, 5))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Outside_Housewife9, "housewife9", StoryCondition.FlagSetBeforeRelative("housewife6", 1200), StoryCondition.FlagSet("Bathroom4"), StoryCondition.CommonConditions.VeryHighLonging, StoryCondition.CommonConditions.SympathyLove, StoryCondition.CommonConditions.VeryHighLust, StoryCondition.CommonConditions.EnoughNutForSex, StoryCondition.CommonConditions.EverFuckedBot)
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Outside_IslandPrologue4_Generic, "Island_Prologue4", StoryCondition.FlagSet("Island_Prologue3"), StoryCondition.FlagNotSet("willing_to_leave"))
		{
			IsFlagBlocking = false,
			RandomOdds = 0.1f
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Walk_Riots.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Riots_FirstWalk, "riots_firstWalk"), new StoryAutoBranch(FungusBlocks.Outside_Riots_ChurchRiots2, "church_hobo_bots", StoryCondition.HourBetweenEqual(22, 5), StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 500), StoryCondition.FlagSet("hobo1"), StoryCondition.FlagSet("church_riots1")), new StoryAutoBranch(FungusBlocks.Outside_Riots_WalkGeneric_Label_generic1, null, StoryCondition.FlagSet("riots_beaten_up"))
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Outside_Riots_WalkGeneric_Label_generic2, null)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Outside_Riots_WalkGeneric_Label_generic3, null)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Outside_Riots_WalkGeneric_Label_generic4, null)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Outside_Riots_WalkGeneric_Label_generic5, null)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Outside_Riots_WalkGeneric_Label_generic6, null)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Outside_Riots_WalkGeneric_Label_generic7, null)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Outside_Riots_WalkGeneric_Label_generic8, null)
		{
			RandomOdds = 1f
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.BigDaddyHurtVisit_Riots.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Bigdaddystoryline_RiotsFirst, "Big_Daddy_Riots"), new StoryAutoBranch(FungusBlocks.Outside_VisitBigDaddy_Label_AlreadyVisitedToday, null, StoryCondition.FlagSetAfterRelative("Bigdaddystoryline_timer", 1080)), new StoryAutoBranch(FungusBlocks.Outside_Bigdaddystoryline_RiotsGeneric_Label_Generic1, null)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Outside_Bigdaddystoryline_RiotsGeneric_Label_Generic2, null)
		{
			RandomOdds = 1f
		})
		{
			OnActivation = BDHOnActivate
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.BigDaddyHurtVisit.ToString(), new StoryAutoBranch(FungusBlocks.Outside_VisitBigDaddy_Label_AlreadyVisitedToday, null, StoryCondition.FlagSetAfterRelative("Bigdaddystoryline_timer", 1080)), new StoryAutoBranch(FungusBlocks.Outside_VisitBigDaddy_Label_Work, null, StoryCondition.HourBetweenEqual(7, 18)), new StoryAutoBranch(FungusBlocks.Outside_VisitBigDaddy_Label_Sleeping, null, StoryCondition.HourBetweenEqual(3, 6)), new StoryAutoBranch(FungusBlocks.Outside_Bigdaddystoryline2, "bigdaddystoryline2", StoryCondition.FlagSet("bigdaddystoryline1")), new StoryAutoBranch(FungusBlocks.Outside_Bigdaddystoryline3, "bigdaddystoryline3", StoryCondition.FlagSet("bigdaddystoryline2"), StoryCondition.Or(StoryCondition.FlagSetBeforeRelative("joinusemail2opened", 800), StoryCondition.And(StoryCondition.FlagSetBeforeRelative("JoinUsEmail", 5760), StoryCondition.FlagNotSet("joinus")))), new StoryAutoBranch(FungusBlocks.Outside_JoinUs7BigDaddy3, "joinus7_bigdaddy3", StoryCondition.FlagSet("joinus7_bigdaddy2")), new StoryAutoBranch(FungusBlocks.Outside_BigdaddyInvitation, "bigdaddyinvitation", StoryCondition.FlagSet("bigdaddystoryline3")), new StoryAutoBranch(FungusBlocks.Outside_Bigdaddystoryline4, "bigdaddystoryline4", StoryCondition.FlagSet("bigdaddystoryline3"), StoryCondition.FlagSet("dinnerwithbigdaddy1")), new StoryAutoBranch(FungusBlocks.Outside_Melissa4BigDaddy, "Melissa4BigDaddy", StoryCondition.CommonConditions.Melissa4Before5NotRejection))
		{
			OnActivation = BDHOnActivate
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Grocery_After.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Paper_bag, "groceries", StoryCondition.DayAbove(9), StoryCondition.HourBetweenEqual(9, 20)), new StoryAutoBranch(FungusBlocks.Outside_Walk_with_Landlord, "walkwithlandlord", StoryCondition.DayAbove(17), StoryCondition.HourBetweenEqual(9, 20), StoryCondition.FlagSet("groceries"), StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 100))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.RichPeopleShopMembership_Before.ToString()));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.RichPeopleShop_Main.ToString(), new StoryAutoBranch(FungusBlocks.RichShop_FirstVisit, false, "richpeopleshop_firstvisit"), new StoryAutoBranch(FungusBlocks.RichShop_VisitNoMembership, false, null, StoryCondition.FlagNotSet("richpeopleshop_membership")), new StoryAutoBranch(FungusBlocks.RichShop_VisitWithMembership, false, null, StoryCondition.FlagSet("richpeopleshop_membership"))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.RichPeopleShopMembership_After.ToString()));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Grocery_Riots.ToString(), new StoryAutoBranch(FungusBlocks.Shop_Riots_FirstTime, false, "Grocery_Riots_FirstTime"), new StoryAutoBranch(FungusBlocks.Shop_Riots_Hungry, false, "Grocery_Riots_Hungry", StoryCondition.VariableCondition("Satiation", DataConditionComparators.SingleArgumentComparators.Below, 0.3f))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Shop_Riots_Hungry2, false, "Grocery_Riots_Hungry2", StoryCondition.VariableCondition("Satiation", DataConditionComparators.SingleArgumentComparators.Below, 0.3f), StoryCondition.FlagSet("Grocery_Riots_Hungry"))));
		StoryCondition melissaRejection = StoryCondition.CommonConditions.MelissaRejection;
		FlagConditionSpecialWithTime melissa5Done = StoryCondition.CommonConditions.Melissa5Done;
		OrCondition orCondition = StoryCondition.NotOr(melissaRejection, melissa5Done);
		StoryAutoEntryPoint storyAutoEntryPoint = new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Grocery_AfterRiots.ToString(), new StoryAutoBranch(FungusBlocks.Shop_AfterRiots_FirstTime, true, "Melissa"), new StoryAutoBranch(FungusBlocks.Shop_Melissa2, true, "Melissa2", StoryCondition.FlagSetBeforeRelative("Melissa1", 720)), new StoryAutoBranch(FungusBlocks.Shop_Melissa3, true, "Melissa3", StoryCondition.FlagSetBeforeRelative("Melissa2", 720)), new StoryAutoBranch(FungusBlocks.Shop_Melissa4, true, "Melissa4", StoryCondition.FlagSetBeforeRelative("Melissa3", 720)), new StoryAutoBranch(FungusBlocks.Shop_Melissa4Rejection, false, "Melissa4rejection", StoryCondition.FlagSetBeforeRelative("Melissa4", 4320), StoryCondition.FlagNotSet("Melissa4bot")), new StoryAutoBranch(FungusBlocks.Shop_Melissa14, false, "Melissa14", StoryCondition.FlagSet("Melissa13")), new StoryAutoBranch(FungusBlocks.Shop_NoMelissa, true, null, StoryCondition.CommonConditions.MelissaNotInShop), new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericBefore_Label_Normal1, true, null, orCondition)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericBefore_Label_Normal2, true, null, orCondition)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericBefore_Label_Normal3, true, null, orCondition)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericRejection_Label_Afternoon, true, null, melissaRejection, StoryCondition.HourBetweenEqual(12, 18))
		{
			RandomOdds = 2f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericRejection_Label_Morning, true, null, melissaRejection, StoryCondition.HourBetweenEqual(5, 12))
		{
			RandomOdds = 2f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericRejection_Label_Normal1, true, null, melissaRejection)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericRejection_Label_Normal2, true, null, melissaRejection)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericRejection_Label_Normal3, true, null, melissaRejection)
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericMelissa5_Label_Afternoon, true, null, melissa5Done, StoryCondition.HourBetweenEqual(12, 18))
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericMelissa5_Label_Evening, true, null, melissa5Done, StoryCondition.HourBetweenEqual(18, 23))
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericMelissa5_Label_Night, true, null, melissa5Done, StoryCondition.HourBetweenEqual(23, 5))
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericMelissa5_Label_Morning, true, null, melissa5Done, StoryCondition.HourBetweenEqual(5, 12))
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Shop_MelissaGenericMelissa5_Label_Normal, true, null, melissa5Done)
		{
			RandomOdds = 1f
		});
		storyAutoEntryPoint.NothingChosenOdds = 0f;
		_storyAutoEntryPoints.Add(storyAutoEntryPoint);
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.FoodDispenser_Riots.ToString(), new StoryAutoBranch(FungusBlocks.Dispenser_Riots_FirstTime, false, "Dispenser_Riots_FirstTime"), new StoryAutoBranch(FungusBlocks.Dispenser_Riots_Hobo, false, "Dispenser_Riots_Hobo", StoryCondition.FlagSet("hobo1"))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.FoodDispenser_AfterRiots.ToString(), new StoryAutoBranch(FungusBlocks.Dispenser_AfterRiots, "Dispenser_AfterRiots")));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Pharmacy_Riots.ToString(), new StoryAutoBranch(FungusBlocks.Pharmacy_Riots_FirstTime, false, "Pharmacy_Riots_FirstTime"), new StoryAutoBranch(FungusBlocks.Pharmacy_Riots_Random, false, "Pharmacy_Riots_Random")
		{
			RandomOdds = 0.5f
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Pharmacy_AfterRiots.ToString(), new StoryAutoBranch(FungusBlocks.Pharmacy_AfterRiots, true, "Pharmacy_AfterRiots")));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.LabourOffice_Riots.ToString(), new StoryAutoBranch(FungusBlocks.Labour_Office_Riots_FirstTime, false, "LabourOffice_Riots_FirstTime"), new StoryAutoBranch(FungusBlocks.Labour_Office_Riots_Landlord, false, "LabourOffice_Riots_Landlord", StoryCondition.VariableConditionBoolTrue("EnoughStaminaToWork"))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.LabourOffice_AfterRiots.ToString(), new StoryAutoBranch(FungusBlocks.Labour_Office_AfterRiots, true, "LabourOffice_AfterRiots")));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Church_MainStory.ToString(), new StoryAutoBranch(FungusBlocks.Church_TheWorstEndStart, null, StoryCondition.FlagSet("church-nextTheWorstEnd")), new StoryAutoBranch(FungusBlocks.Church_Revival, null, StoryCondition.FlagSet("church-nextRevival")), new StoryAutoBranch(FungusBlocks.Church_DrugiePojscieBA, null, StoryCondition.FlagSet("church-nextDrugiePojscieBA")), new StoryAutoBranch(FungusBlocks.Church_DrugiePojscieABA, null, StoryCondition.FlagSet("church-nextDrugiePojscieABA")), new StoryAutoBranch(FungusBlocks.Church_DrugiePojscieBB, null, StoryCondition.FlagSet("church-nextDrugiePojscieBB")), new StoryAutoBranch(FungusBlocks.Church_SpotkanieBC, null, StoryCondition.FlagSet("church-nextSpotkanieBC")), new StoryAutoBranch(FungusBlocks.Church_Spotkanie_2B, null, StoryCondition.FlagSet("church-nextSpotkanieB")), new StoryAutoBranch(FungusBlocks.Church_Spotyka_bota___crash, null, StoryCondition.FlagSet("church-nextSpotykaBotaCrash")), new StoryAutoBranch(FungusBlocks.Church_Monolog_przed_kosciolem, null))
		{
			OnActivation = (StoryAutoBranch branch) =>
			{
				GameVariables.Current.lastWentToChurchAt = GameVariables.Current.time;
				GameVariables.Current.timesWentToChurch++;
			}
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Church_AfterStory_VisitStart.ToString(), new StoryAutoBranch(new StoryBlockTarget(FungusBlocks.Church_AfterStory_VisitStart_Label_TooLate), null, StoryCondition.HourBetweenEqual(22, 5)), new StoryAutoBranch(FungusBlocks.Church_AfterStory_NunBack2, "church-nun_back2"), new StoryAutoBranch(new StoryBlockTarget(FungusBlocks.Church_AfterStory_VisitStart_Label_AlreadyVisitedToday), null, StoryCondition.FlagSetAfterRelative("church-nun_back_visit_timer", 540), StoryCondition.FlagNotSet("church-nun_back4")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_NunBack3, "church-nun_back3", StoryCondition.FlagSet("church-nun_back2")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_NunBack4, "church-nun_back4", StoryCondition.FlagSet("church-nun_back3")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_VisitStart_Label_GenericGreeting, null, StoryCondition.FlagSetAfterRelative("church-nun_back_visit_timer", 480)), new StoryAutoBranch(FungusBlocks.Church_AfterStory_AfterRiotsNeverWent, "church-afterriots_neverwent", afterRiots, StoryCondition.FlagNotSet("church_riots1")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_AfterRiotsSavedPriestbot, "church-afterriots_savedpriestbot", afterRiots, StoryCondition.FlagSet("church_riots1"), StoryCondition.FlagSet("church_priestbot_back_riots")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_AfterRiotsDidntSavePriestbot, "church-afterriots_didntsavepriestbot", afterRiots, StoryCondition.FlagSet("church_riots1"), StoryCondition.FlagNotSet("church_priestbot_back_riots")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_Melissa4church, "Melissa4church", afterRiots, StoryCondition.CommonConditions.Melissa4Before5NotRejection, StoryCondition.FlagSet("church-nun_back1")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_NunBack5, "church-nun_back5", StoryCondition.FlagSet("church-nun_back4"), StoryCondition.FlagSet("church-MeetingNunWithTheBot")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_BigDaddyStorylineChurchPart1, "church-bigdaddystorylinepart1", StoryCondition.FlagSet("church-bigdaddystorylinepossible"), StoryCondition.FlagSet("bigdaddystoryline3")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_GokusMotivation, "church-nun_back_gokusmotivation", StoryCondition.FlagSet("church-nun_back4"))
		{
			RandomOdds = 0.5f
		})
		{
			OnActivation = (StoryAutoBranch x) =>
			{
				if (x.RelatedFlag != null)
				{
					GameVariables.Current.AddFlag("church-nun_back_visit_timer");
				}
			}
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Church_Riots.ToString(), new StoryAutoBranch(FungusBlocks.Church_AfterStory_Riots1, "church_riots1"), new StoryAutoBranch(FungusBlocks.Church_AfterStory_Riots3, "church_priestbot_back_riots", StoryCondition.FlagSet("church_hobo_bots")), new StoryAutoBranch(FungusBlocks.Church_AfterStory_RiotsGeneric_Label_Generic1, null, StoryCondition.FlagNotSet("church_priestbot_back_riots"))
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Church_AfterStory_RiotsGeneric_Label_Generic2, null, StoryCondition.FlagNotSet("church_priestbot_back_riots"))
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Church_AfterStory_RiotsGeneric_Label_Generic3, null, StoryCondition.FlagSet("church_priestbot_back_riots"))
		{
			RandomOdds = 1f
		}, new StoryAutoBranch(FungusBlocks.Church_AfterStory_RiotsGeneric_Label_Generic4, null, StoryCondition.FlagSet("church_priestbot_back_riots"))
		{
			RandomOdds = 1f
		})
		{
			NothingChosenOdds = 0f
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.OutsideWithBotExposureEvent.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Exposure_night, true, "exposure_night", StoryCondition.HourBetweenEqual(20, 6)), new StoryAutoBranch(FungusBlocks.Outside_Exposure_day, true, "exposure_day", StoryCondition.HourBetweenEqual(7, 20)), new StoryAutoBranch(FungusBlocks.Outside_Exposure_little_girl, true, "exposure_little_girl", StoryCondition.HourBetweenEqual(8, 20), StoryCondition.Or(StoryCondition.FlagSet("exposure_day"), StoryCondition.FlagSet("exposure_night"))), new StoryAutoBranch(FungusBlocks.Outside_Generic_Exposure, true, null, StoryCondition.Or(StoryCondition.FlagSet("exposure_day"), StoryCondition.FlagSet("exposure_night")))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.OutsideWithBotBeforeMenu.ToString(), new StoryAutoBranch(FungusBlocks.Outside_AfterRiotsWithBot, true, "afterRiots_withBot_firstTime", afterRiots), new StoryAutoBranch(FungusBlocks.Outside_RandomSearch3, true, "randomsearch3", StoryCondition.FlagSetBeforeRelative("randomsearch2", 1440), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Above, 0.2f), StoryCondition.HourBetweenEqual(23, 4), StoryCondition.VariableCondition("sus", DataConditionComparators.SingleArgumentComparators.Below, 0.4f))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.WalkWithBot.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Walk_FirstWalk, "WalkWithBot"), new StoryAutoBranch(FungusBlocks.Outside_Walk_Melissa5AfterRejection, "Melissa5AfterRejection", StoryCondition.CommonConditions.Melissa5NotDone, StoryCondition.FlagSet("Melissa4rejection"), StoryCondition.VariableCondition("Satiation", DataConditionComparators.SingleArgumentComparators.Below, 0.5f), StoryCondition.HourBetweenEqual(23, 7)), new StoryAutoBranch(FungusBlocks.Outside_Walk_Melissa5Normal, "Melissa5Normal", StoryCondition.CommonConditions.Melissa5NotDone, StoryCondition.FlagNotSet("Melissa4rejection"), StoryCondition.FlagSet("Melissa4bot")), new StoryAutoBranch(FungusBlocks.Outside_Walk_LandlordConspiracy1, "LandlordConspiracy1", StoryCondition.FlagSet("LandlordConspiracy0"), StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 100), StoryCondition.HourBetweenEqual(8, 20)), new StoryAutoBranch(FungusBlocks.Outside_Walk_MeetingNunWithBot, "church-MeetingNunWithTheBot", StoryCondition.FlagSet("church-nun_back2"), StoryCondition.HourBetweenEqual(6, 21)), new StoryAutoBranch(FungusBlocks.Outside_Walk_Karaoke2, "Karaoke2", StoryCondition.FlagSet("church-bigdaddystorylinepossible"), StoryCondition.FlagSet("church-MeetingNunWithTheBot"), StoryCondition.FlagSet("Karaoke1"), StoryCondition.HourBetweenEqual(6, 21)), new StoryAutoBranch(FungusBlocks.Outside_Walk_AirPollutionMask, "AirPollutionMask", StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 100), StoryCondition.HourBetweenEqual(9, 18), StoryCondition.VariableCondition("money", DataConditionComparators.SingleArgumentComparators.Above, 2500))
		{
			RandomOdds = 0.5f
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.OutsideWithBotReturnStaircase.ToString(), new StoryAutoBranch(FungusBlocks.Outside_LandlordDaughter1, true, "LandlordDaughter1", StoryCondition.FlagSet("LandlordConspiracy3"), StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 100), StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.Above, 150), StoryCondition.HourBetweenEqual(10, 15), StoryCondition.FlagSetAfterRelative("LandlordStaircase", 720, negate: true)), new StoryAutoBranch(FungusBlocks.Outside_GenericStaircase, true, null, StoryCondition.FlagSetAfterRelative("LandlordStaircase", 480, negate: true))
		{
			RandomOdds = 0.1f
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.OutsideWithBotExitStaircase.ToString(), new StoryAutoBranch(FungusBlocks.Outside_GoingOutside3, "GoingOutside3"), new StoryAutoBranch(FungusBlocks.Outside_LandlordConspiracy0, true, "LandlordConspiracy0", StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 100), StoryCondition.HourBetweenEqual(8, 20), StoryCondition.FlagSetAfterRelative("LandlordStaircase", 720, negate: true)), new StoryAutoBranch(FungusBlocks.Outside_LandlordConspiracy3, true, "LandlordConspiracy3", StoryCondition.VariableCondition("sympathy", DataConditionComparators.SingleArgumentComparators.Above, 100), StoryCondition.FlagSet("LandlordConspiracy2"), StoryCondition.HourBetweenEqual(8, 20), StoryCondition.FlagSetAfterRelative("LandlordStaircase", 720, negate: true)), new StoryAutoBranch(FungusBlocks.Outside_GenericStaircase, true, null, StoryCondition.FlagSetAfterRelative("LandlordStaircase", 480, negate: true))
		{
			RandomOdds = 0.05f
		}));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ClothierAloneEnter.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Clothier_event4, true, "clothier4", StoryCondition.FlagSet("clothier3")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_HalloweenAlone, "ClothierHalloweenAlone", StoryCondition.VariableConditionBoolTrue("HalloweenActive"), StoryCondition.FlagNotSet("ClothierHalloweenWithBot")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_Melissa4Annalie, true, "Melissa4Annalie", StoryCondition.CommonConditions.Melissa4Before5NotRejection, StoryCondition.FlagSet("GoingOutside3")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_Birthday, true, "birthday", StoryCondition.FlagSet("clothier1"), StoryCondition.HourBetweenEqual(5, 9)), new StoryAutoBranch(FungusBlocks.Outside_Clothier_MentalHealth, true, "AnnalieMentalHealth", StoryCondition.FlagSet("clothier1"), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.3f)), new StoryAutoBranch(FungusBlocks.Outside_Clothier_Coat, true, "coat", StoryCondition.FlagSet("clothier1"), StoryCondition.HourBetweenEqual(5, 9)), new StoryAutoBranch(FungusBlocks.Outside_Clothier_GoingOutside1, true, "GoingOutside1", StoryCondition.FlagSet("clothier4"), StoryCondition.VariableCondition("MoneySpentOnClothes", DataConditionComparators.SingleArgumentComparators.Above, 4000), StoryCondition.DayAbove(10)), new StoryAutoBranch(FungusBlocks.Outside_Clothier_GoingOutsideSide, true, "GoingOutsideSide", StoryCondition.FlagSet("GoingOutside1"), StoryCondition.FlagNotSet("GoingOutside3")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_Question, true, "Question", StoryCondition.VariableCondition("ClothierItemsBought", DataConditionComparators.SingleArgumentComparators.Above, 5)), new StoryAutoBranch(FungusBlocks.Outside_Clothier_BestClient, true, "BestClient", StoryCondition.VariableCondition("ClothierItemsBought", DataConditionComparators.SingleArgumentComparators.Above, 20)), new StoryAutoBranch(FungusBlocks.Outside_Clothier_LandlordCockblock5, true, "Landlord_Cockblock5", StoryCondition.FlagSet("Landlord_Cockblock4_Help")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_LandlordCockblock6, true, "Landlord_Cockblock6", StoryCondition.FlagSetBeforeRelative("Landlord_Cockblock5", 2880)), new StoryAutoBranch(FungusBlocks.Outside_Clothier_GenericGreeting, true, null)));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ClothierAloneLeave.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Clothier_event1, true, "clothier1", StoryCondition.VariableCondition("ClothierItemsBought", DataConditionComparators.SingleArgumentComparators.Above, 3)), new StoryAutoBranch(FungusBlocks.Outside_Clothier_event2, "clothier2", StoryCondition.VariableCondition("ClothierItemsBought", DataConditionComparators.SingleArgumentComparators.Above, 7), StoryCondition.FlagSet("clothier1")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_event3, "clothier3", StoryCondition.VariableCondition("ClothierItemsBought", DataConditionComparators.SingleArgumentComparators.Above, 10), StoryCondition.FlagSet("clothier2")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_Melissa6to7info, true, null, StoryCondition.FlagSet("Melissa6"), StoryCondition.FlagNotSet("Melissa7")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AnnalieESL7to8info, true, null, StoryCondition.CommonConditions.AnnalieRunwayReady), new StoryAutoBranch(FungusBlocks.Outside_Clothier_GenericFarewell, true, null)));
		FlagConditionBasic flagConditionBasic = StoryCondition.FlagSet("Annalie_riots1");
		FlagConditionBasic flagConditionBasic2 = StoryCondition.FlagNotSet("Annalie_riots1");
		OrCondition orCondition2 = StoryCondition.Or(StoryCondition.FlagSet("Annalie_afterriots_helped"), StoryCondition.FlagSet("Annalie_afterriots_neverWent"));
		AndCondition andCondition = StoryCondition.And(StoryCondition.FlagNotSet("Annalie_afterriotsWithBot_helped"), StoryCondition.FlagNotSet("Annalie_afterriotsWithBot_neverWent"), StoryCondition.FlagNotSet("Annalie_afterriotsWithBot_wentAlone"));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ClothierWithBotBeforeEnter.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AfterRiotsWithBotWentAlone, "Annalie_afterriotsWithBot_wentAlone", StoryCondition.CommonConditions.AfterRiots, orCondition2, andCondition), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AfterRiotsWithBotHelped, "Annalie_afterriotsWithBot_helped", StoryCondition.CommonConditions.AfterRiots, flagConditionBasic, andCondition), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AfterRiotsWithBotNeverWent, "Annalie_afterriotsWithBot_neverWent", StoryCondition.CommonConditions.AfterRiots, flagConditionBasic2, andCondition), new StoryAutoBranch(FungusBlocks.Outside_Clothier_Melissa18BotBlock, null, StoryCondition.FlagSet("Melissa17"), StoryCondition.FlagNotSet("Melissa18")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_Melissa24BotBlock, null, StoryCondition.FlagSet("Melissa22"), StoryCondition.FlagNotSet("Melissa24")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AnnalieESL6, "Annalie_ending6", StoryCondition.MoneyAbove(1000), StoryCondition.FlagSetBeforeRelative("Annalie_ending5", 720))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ClothierWithBotEnter.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Clothier_HalloweenWithBot, "ClothierHalloweenWithBot", StoryCondition.VariableConditionBoolTrue("HalloweenActive")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_HalloweenWithBot2, "ClothierHalloweenWithBot2", StoryCondition.VariableConditionBoolTrue("HalloweenActive"), StoryCondition.FlagSet("ClothierHalloweenWithBot"), StoryCondition.VariableConditionBoolTrue("HasHalloweenCostume")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_HairstyleComment, "AnnalieHairstyleComment", StoryCondition.CommonConditions.HasAnnalieHairstyle), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AnnalieCafe1, "Annalie_cafe1", StoryCondition.CommonConditions.AfterRiots, StoryCondition.FlagSet("date")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AnnalieESL7to8infoWithbot, true, null, StoryCondition.CommonConditions.AnnalieRunwayReady), new StoryAutoBranch(FungusBlocks.Outside_Clothier_CustomClothing, true, "CustomClothing"), new StoryAutoBranch(FungusBlocks.Outside_Clothier_GenericGreetingWithBot, true, null)));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.HalloweenTreatOrTrickWithBot.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Halloween_TOT1, false, "HalloweenTOT1"), new StoryAutoBranch(FungusBlocks.Outside_Halloween_TOT2, false, "HalloweenTOT2"), new StoryAutoBranch(FungusBlocks.Outside_Halloween_TOT3, false, "HalloweenTOT3"), new StoryAutoBranch(FungusBlocks.Outside_Halloween_TOT4, false, "HalloweenTOT4")));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ClothierWithBotLeave.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Clothier_GenericFarewellWithBot, true, null)));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ClothierWithBot_EnterChangingRoom.ToString())
		{
			EntryPointExplanation = "Before Jun and Player enter the changing room. Still on the \"shop\" view!"
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ClothierWithBot_EnteredChangingRoom.ToString())
		{
			EntryPointExplanation = "Jun and Player just entered the changing room."
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ClothierWithBot_LeavingChangingRoom.ToString())
		{
			EntryPointExplanation = "Before Jun and Player leave the changing room. Still on the \"changing room\" view!"
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.ClothierWithBot_LeftChangingRoom.ToString())
		{
			EntryPointExplanation = "Jun and Player just left the changing room."
		});
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Clothier_Riots.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Clothier_Riots1, "Annalie_riots1"), new StoryAutoBranch(FungusBlocks.Outside_Clothier_Riots2, "Annalie_riots2", StoryCondition.FlagSetBeforeRelative("Annalie_riots1", 1080))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Clothier_AfterRiots.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AfterRiots, "Annalie_afterriots_helped", flagConditionBasic, andCondition), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AfterRiots2, "Annalie_riots_returnedMoney", StoryCondition.FlagSetBeforeRelative("Annalie_gave_money", 18720), StoryCondition.Or(StoryCondition.FlagSetBeforeRelative("Annalie_afterriots_helped", 7200), StoryCondition.FlagSetBeforeRelative("Annalie_afterriotsWithBot_helped", 7200))), new StoryAutoBranch(FungusBlocks.Outside_Clothier_AfterRiotsNeverWent, "Annalie_afterriots_neverWent", flagConditionBasic2, andCondition)));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Clothier_BeforeEnter.ToString(), new StoryAutoBranch(FungusBlocks.Outside_Clothier_BeforeEnter_Melissa7, "Melissa7", StoryCondition.FlagSet("Melissa6"), StoryCondition.HourBetweenEqual(17, 19)), new StoryAutoBranch(FungusBlocks.Outside_Clothier_BeforeEnter_Melissa18, "Melissa18", StoryCondition.FlagSet("Melissa17")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_BeforeEnter_Melissa24, "Melissa24", StoryCondition.FlagSet("Melissa22")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_BeforeEnter_AnnalieESL1, "Annalie_ending1", StoryCondition.FlagSet("willing_to_leave")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_BeforeEnter_AnnalieESL2, "Annalie_ending2", StoryCondition.MoneyAbove(1000), StoryCondition.FlagSet("Annalie_ending1"), StoryCondition.FlagNotSet("Landlord_ending1_accepted")), new StoryAutoBranch(FungusBlocks.Outside_Clothier_BeforeEnter_AnnalieESL4, "Annalie_ending4", StoryCondition.MoneyAbove(1000), StoryCondition.FlagSet("Annalie_ending3"))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Fishing_BeforePrepare.ToString(), new StoryAutoBranch(FungusBlocks.Fishing_Fishing2, "fishing2", StoryCondition.CommonConditions.FisherAtMicroplasticCoast)));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Fishing_FisherEnter.ToString(), new StoryAutoBranch(FungusBlocks.Fisher_WrongTime, null, StoryCondition.NotAnd(StoryCondition.CommonConditions.FisherAtMicroplasticCoast)), new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint1, "Fish_joint1", StoryCondition.FlagSet("fishing4"), StoryCondition.VariableCondition("FishesCaught", DataConditionComparators.SingleArgumentComparators.AboveEqual, 40), StoryCondition.FlagSet("dinnerwithbigdaddy1"), StoryCondition.FlagSet("hobo"), StoryCondition.CommonConditions.ChurchAnyEndingDone, StoryCondition.Or(StoryCondition.FlagNotSet("church-endJustGoThereGood"), StoryCondition.FlagSet("church-nun_back1"))), new StoryAutoBranch(FungusBlocks.Fisher_GenericGreeting, true, null)));
		FlagConditionBasic flagConditionBasic3 = StoryCondition.FlagSet("fishing3");
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Fishing_FisherTalk.ToString(), new StoryAutoBranch(FungusBlocks.Fisher_Talk1, true, "fishing3", StoryCondition.FishesCaught(6)), new StoryAutoBranch(FungusBlocks.Fisher_Talk2, true, "fishing4", StoryCondition.FishesCaught(16)), new StoryAutoBranch(FungusBlocks.Fisher_Bluegill, true, "Fishing_Bluegill_ManyTalk", flagConditionBasic3, StoryCondition.FishesCaught(ItemEnum.Bluegill, 10)), new StoryAutoBranch(FungusBlocks.Fisher_LargeMouthBassBig, true, "Fishing_LargemouthBass_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.LargemouthBass)), new StoryAutoBranch(FungusBlocks.Fisher_LargeMouthBassMany, true, "Fishing_LargemouthBass_ManyTalk", flagConditionBasic3, StoryCondition.FishesCaught(ItemEnum.LargemouthBass, 10)), new StoryAutoBranch(FungusBlocks.Fisher_NorthernPikeBig, true, "Fishing_NorthernPike_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.NorthernPike)), new StoryAutoBranch(FungusBlocks.Fisher_YellowPerchBig, true, "Fishing_YellowPerch_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.YellowPerch)), new StoryAutoBranch(FungusBlocks.Fisher_CatfishBig, true, "Fishing_Catfish_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.Catfish)), new StoryAutoBranch(FungusBlocks.Fisher_CatfishMany, true, "Fishing_Catish_ManyTalk", flagConditionBasic3, StoryCondition.FishesCaught(ItemEnum.Catfish, 13)), new StoryAutoBranch(FungusBlocks.Fisher_WalleyeBIg, true, "Fishing_Walleye_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.Walleye)), new StoryAutoBranch(FungusBlocks.Fisher_CarpMany, true, "Fishing_Carp_ManyTalk", flagConditionBasic3, StoryCondition.FishesCaught(ItemEnum.Carp, 13)), new StoryAutoBranch(FungusBlocks.Fisher_TilapiaHeavy, true, "Fishing_Tilapia_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.Tilapia)), new StoryAutoBranch(FungusBlocks.Fisher_RainbowTroutMany, true, "Fishing_RainbowTrout_ManyTalk", flagConditionBasic3, StoryCondition.FishesCaught(ItemEnum.RainbowTrout, 13)), new StoryAutoBranch(FungusBlocks.Fisher_SmallMouthBassMany, true, "Fishing_SmallmouthBass_ManyTalk", flagConditionBasic3, StoryCondition.FishesCaught(ItemEnum.Carp, 13)), new StoryAutoBranch(FungusBlocks.Fisher_BrownTroutBig, true, "Fishing_BrownTrout_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.BrownTrout)), new StoryAutoBranch(FungusBlocks.Fisher_SalmonBig, true, "Fishing_Salmon_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.Salmon)), new StoryAutoBranch(FungusBlocks.Fisher_RedSnapperBig, true, "Fishing_RedSnapper_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.RedSnapper)), new StoryAutoBranch(FungusBlocks.Fisher_BlackSeaBassBig, true, "Fishing_BlackSeaBass_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.BlackSeaBass)), new StoryAutoBranch(FungusBlocks.Fisher_HalibutBig, true, "Fishing_Halibut_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.Halibut)), new StoryAutoBranch(FungusBlocks.Fisher_GoldenSalmonBig, true, "Fishing_GoldenSalmon_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.GoldenSalmon)), new StoryAutoBranch(FungusBlocks.Fisher_MackrelMany, true, "Fishing_Mackerel_ManyTalk", flagConditionBasic3, StoryCondition.FishesCaught(ItemEnum.Carp, 13)), new StoryAutoBranch(FungusBlocks.Fisher_FlounderAny, true, "Fishing_Flounder_AnyTalk", flagConditionBasic3, StoryCondition.FishesCaught(ItemEnum.Flounder, 1)), new StoryAutoBranch(FungusBlocks.Fisher_FlounderBig, true, "Fishing_Flounder_BigTalk", flagConditionBasic3, StoryCondition.CaughtFishBig(ItemEnum.Flounder))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Fishing_FisherLeave.ToString(), new StoryAutoBranch(FungusBlocks.Fisher_GenericLeave, true, null)));
		StoryCondition storyCondition = StoryCondition.Or(StoryCondition.And(StoryCondition.FlagSet("Fish_joint2_win"), StoryCondition.FlagNotSet("church-endTheWorstEnd"), StoryCondition.FlagNotSet("church-nun_back1")), StoryCondition.FlagSet("Fish_joint2.5_Shanice"), StoryCondition.FlagSet("Fish_joint2.5_Priestbot_win"));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.Fishing_FishingJoint.ToString(), new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint1Repeat, "Fish_joint1", StoryCondition.FlagSet("Fish_joint1_loss"), StoryCondition.FlagNotSet("Fish_joint1_win"))
		{
			IsFlagBlocking = false
		}, new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint2, "Fish_joint2", StoryCondition.FlagSet("Fish_joint1_win")), new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint2Repeat, "Fish_joint2", StoryCondition.FlagSet("Fish_joint1_win"), StoryCondition.FlagNotSet("Fish_joint2_win"))
		{
			IsFlagBlocking = false
		}, new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint2dot5Priestbot, "Fish_joint2.5_Priestbot", StoryCondition.FlagSet("church-nun_back1"), StoryCondition.FlagSet("Fish_joint2_win")), new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint2dot5PriestbotRepeat, "Fish_joint2.5_Priestbot", StoryCondition.FlagSet("church-nun_back1"), StoryCondition.FlagSet("Fish_joint2_win"), StoryCondition.FlagNotSet("Fish_joint2.5_Priestbot_win"))
		{
			IsFlagBlocking = false
		}, new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint2dot5Shanice, "Fish_joint2.5_Shanice", StoryCondition.FlagSet("church-endTheWorstEnd"), StoryCondition.FlagSet("Fish_joint2_win")), new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint3, "Fish_joint3", storyCondition), new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint3Repeat, "Fish_joint3", storyCondition, StoryCondition.FlagNotSet("Fish_joint3_win"))
		{
			IsFlagBlocking = false
		}, new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint4, "Fish_joint4", StoryCondition.FlagSet("Fish_joint3_win")), new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint4Repeat, "Fish_joint4", StoryCondition.FlagSet("Fish_joint3_win"), StoryCondition.FlagNotSet("Fish_joint4_win"))
		{
			IsFlagBlocking = false
		}, new StoryAutoBranch(FungusBlocks.Fisher_J_FishJoint5, "Fish_joint5", StoryCondition.FlagSet("Fish_joint4_win"))));
		_storyAutoEntryPoints.Add(new StoryAutoEntryPoint(StoryAutoEntryPointEnum.BotConsole_Interact.ToString(), new StoryAutoBranch(FungusBlocks.Console_Interact1, true, "interact1"), new StoryAutoBranch(FungusBlocks.Console_Interact2, true, "interact2", StoryCondition.FlagSet("interact1")), new StoryAutoBranch(FungusBlocks.Console_Interact3, true, "interact3", StoryCondition.FlagSet("interact2")), new StoryAutoBranch(FungusBlocks.Console_Interact4, true, "interact4", StoryCondition.FlagSet("interact3")), new StoryAutoBranch(FungusBlocks.Console_Interact5, true, "interact5", StoryCondition.FlagSet("interact4")), new StoryAutoBranch(FungusBlocks.Console_Interactrandom1, true, "interactr1", StoryCondition.FlagSet("interact3"), StoryCondition.MoneyAbove(100000), StoryCondition.CommonConditions.Melissa5Done, StoryCondition.CommonConditions.MelissaAlive)
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Console_Interactrandom2, true, "interactr2", StoryCondition.FlagSet("interact3"), StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.Above, 500), StoryCondition.VariableCondition("Longing", DataConditionComparators.SingleArgumentComparators.Above, 5f))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Console_Interactrandom3, true, "interactr3", StoryCondition.FlagSet("interact3"), StoryCondition.VariableCondition("Satiation", DataConditionComparators.SingleArgumentComparators.Below, 0.2f))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Console_Interactrandom4, true, "interactr4", StoryCondition.FlagSet("interact3"), StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.Above, 700))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Console_Interactrandom5, true, "interactr5", StoryCondition.FlagSet("interact3"))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Console_Interactrandom6, true, "interactr6")
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Console_Interactrandom7, true, "interactr7", StoryCondition.VariableCondition("Stamina", DataConditionComparators.SingleArgumentComparators.Below, 0.2f), StoryCondition.VariableCondition("MentalHealth", DataConditionComparators.SingleArgumentComparators.Below, 0.4f))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Console_Interactrandom8, true, "interactr8", StoryCondition.FlagSet("interact3"), StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.Above, 300), StoryCondition.VariableCondition("Longing", DataConditionComparators.SingleArgumentComparators.Above, 5f))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Console_Interactrandom9, true, "interactr9", StoryCondition.FlagSet("interactr8"), StoryCondition.VariableCondition("lust", DataConditionComparators.SingleArgumentComparators.Above, 300), StoryCondition.VariableCondition("Longing", DataConditionComparators.SingleArgumentComparators.Above, 5f))
		{
			RandomOdds = 0.5f
		}, new StoryAutoBranch(FungusBlocks.Console_InteractGeneric, true, null)));
		static void BDHOnActivate(StoryAutoBranch x)
		{
			if (x.RelatedFlag != null)
			{
				GameVariables.Current.AddFlag("Bigdaddystoryline_timer");
			}
		}
	}

	public StoryMenuEntryPoint GetStoryMenuEntryPoint(string id)
	{
		MakeSureInitialized();
		return _storyMenuEntryPoints.FirstOrDefault((StoryMenuEntryPoint t) => t.Id == id);
	}

	private void InitializeStoryMenuEntryPoints()
	{
		_storyMenuEntryPoints = _storyMenuEntryPoints.ClearOrCreate();
		_ = StoryCondition.CommonConditions.RiotsActive;
		DataCondition<bool> riotsNotActive = StoryCondition.CommonConditions.RiotsNotActive;
		StoryMenuBranchesStrings storyMenuBranchesStrings = SingletonScriptableObject<CFLocalizationDataSO>.Instance.StoryMenuBranchesStrings;
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.SexChoice.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.StartSexStream, FungusBlocks.Sex_Stream_Missionary, null, StoryCondition.CommonConditions.CanStartStream), new StoryMenuBranch(storyMenuBranchesStrings.StartSexNoStream, FungusBlocks.Sex_Missionary, null), new StoryMenuBranch(storyMenuBranchesStrings.Nevermind, FungusBlocks.Go_Home, null)));
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.BathroomChoice.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.Bathroom_BotShowerAlone, FungusBlocks.Bathroom_BotShowerAlone, null), new StoryMenuBranch(storyMenuBranchesStrings.Bathroom_ShowerTogether, FungusBlocks.Bathroom_TogetherGeneric, null, StoryCondition.VariableConditionBoolTrue("HasAnySoap"), StoryCondition.VariableConditionBoolTrue("HasAllLimbs"))
		{
			ShowerGrayedOutWhenUnavailable = true
		}, new StoryMenuBranch(storyMenuBranchesStrings.Nevermind, FungusBlocks.Go_Home, null)));
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.AfterFrick_CumOnHerAnonsChoice.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.CumChoice_CleanItOffWithARag, FungusBlocks.Sex_After_CumOnHerGenericSmart_Rag, "AfterSex_Rag")
		{
			IsFlagBlocking = false
		}, new StoryMenuBranch(storyMenuBranchesStrings.Bathroom_BotShowerAlone, FungusBlocks.Bathroom_BotShowerAlone, "AfterSex_ShowerAlone", StoryCondition.FlagSet("Bathroom4"))
		{
			IsFlagBlocking = false
		}, new StoryMenuBranch(storyMenuBranchesStrings.Bathroom_ShowerTogether, FungusBlocks.Bathroom_TogetherGeneric, "AfterSex_ShowerTogether", StoryCondition.FlagSet("Bathroom4"), StoryCondition.VariableConditionBoolTrue("HasAnySoap"), StoryCondition.VariableConditionBoolTrue("HasAllLimbs"))
		{
			IsFlagBlocking = false
		}, new StoryMenuBranch(storyMenuBranchesStrings.CumChoice_LeaveItOnHer, FungusBlocks.Sex_After_CumOnHerGenericSmart_Leave, "AfterSex_LeaveCum")
		{
			IsFlagBlocking = false
		}));
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.SleepChoice.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.SleepWithBot, FungusBlocks.Sleep_Sleep_With_Bot, null, StoryCondition.CommonConditions.SleepTogetherUnlocked, StoryCondition.CommonConditions.SympathyPositive), new StoryMenuBranch(storyMenuBranchesStrings.SleepAloneTillRested, FungusBlocks.Sleep_Sleep_until_rested, null, StoryCondition.VariableCondition("Stamina", DataConditionComparators.SingleArgumentComparators.Below, 0.8f)), new StoryMenuBranch(storyMenuBranchesStrings.SleepAloneFor480, FungusBlocks.Sleep_Sleep_for_480_minutes, null), new StoryMenuBranch(storyMenuBranchesStrings.SleepAloneFor240, FungusBlocks.Sleep_Sleep_for_240_minutes, null), new StoryMenuBranch(storyMenuBranchesStrings.SleepAloneFor60, FungusBlocks.Sleep_Sleep_for_60_minutes, null), new StoryMenuBranch(storyMenuBranchesStrings.SleepAloneDontSleep, FungusBlocks.Go_Home, null)));
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.OutsideAlone.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAloneGrocery, FungusBlocks.Shop_Main, null, StoryCondition.Or(StoryCondition.DayAbove(4), StoryCondition.FlagSet("food"))), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAloneDispenser, FungusBlocks.Dispenser_Main, null, StoryCondition.Or(StoryCondition.DayAbove(4), StoryCondition.FlagSet("food"))), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAlonePharmacy, FungusBlocks.Pharmacy_Main, null, StoryCondition.Or(StoryCondition.DayAbove(4), StoryCondition.FlagSet("food"))), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAloneClothier, FungusBlocks.Outside_Clothier_Main, null, StoryCondition.VariableConditionBoolTrue("IsBotSmart"), StoryCondition.Or(StoryCondition.HourBetweenEqual(10, 16), StoryCondition.FlagSet("clothiervisit"))), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAloneLabourOffice, FungusBlocks.Labour_Office_Main, null, StoryCondition.FlagSet("labouroffice")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAloneWalk, FungusBlocks.Outside_Walk, null), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAloneGoFishing, FungusBlocks.Fishing_Start, null, StoryCondition.FlagSet("fishing1")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAloneVisitFisher, FungusBlocks.Fisher_Start, null, StoryCondition.FlagSet("fishing2")), new StoryMenuBranch(storyMenuBranchesStrings.GoToChurch, FungusBlocks.Church_Church_start, null, StoryCondition.FlagSet("church"), StoryCondition.FlagSetSpecialWithTime("church-end", GameVariables.CheckType.Start, 0, GameVariables.TimeCheckType.None, negate: true), StoryCondition.HourBetweenEqual(7, 21), StoryCondition.VariableCondition("TimeSinceLastWentToChurch", DataConditionComparators.SingleArgumentComparators.Above, 720)), new StoryMenuBranch(storyMenuBranchesStrings.GoToChurchToInvestigate, FungusBlocks.Church_AfterStory_NunInvestigation2, "nunInvestigation2", StoryCondition.FlagSet("church"), StoryCondition.FlagSetBeforeRelative("nunInvestigation1WentToChurch", 1440), riotsNotActive), new StoryMenuBranch(storyMenuBranchesStrings.GoToChurch, FungusBlocks.Church_AfterStory_VisitStart, null, StoryCondition.FlagSet("church"), StoryCondition.FlagSet("church-nun_back1")), new StoryMenuBranch(storyMenuBranchesStrings.GoToPoliceStationReportNun, FungusBlocks.Outside_NunInvestigation3, null, StoryCondition.FlagSet("nunInvestigation2"), StoryCondition.HourBetweenEqual(10, 16), riotsNotActive), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAloneGoToRichArea, FungusBlocks.Outside_RichArea_Main, null, StoryCondition.StoryMenuEntryPointConditionalExistCondition(StoryMenuEntryPointsEnum.OutsideAloneRichArea.ToString())), new StoryMenuBranch(storyMenuBranchesStrings.GoBackHome, FungusBlocks.Go_Home, null), new StoryMenuBranch(storyMenuBranchesStrings.DEBUG, FungusBlocks.Debug_DEBUG, null, StoryCondition.VariableConditionBoolTrue("DebugEnabled"))));
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.OutsideAloneRichArea.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.VisitBDH, FungusBlocks.Outside_VisitBigDaddy, null, StoryCondition.FlagSet("bigdaddystoryline1")), new StoryMenuBranch(storyMenuBranchesStrings.GoToAShopForRichPeople, FungusBlocks.RichShop_Main, null, StoryCondition.FlagSet("food"), StoryCondition.DayAbove(5), StoryCondition.FlagSet("richpeopleshopallowed")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideAloneReturnToSlums, FungusBlocks.Outside_RichArea_Return, null)));
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.FisherMain.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.Fisher_SellFish, FungusBlocks.Fisher_Shop, null), new StoryMenuBranch(storyMenuBranchesStrings.Fisher_SmokeFishJoint, FungusBlocks.Fisher_StartFishJoint, null, StoryCondition.StoryAutoEntryPointAnyExistCondition(StoryAutoEntryPointEnum.Fishing_FishingJoint.ToString()), StoryCondition.FlagNotSet("Fish_joint_ending")), new StoryMenuBranch(storyMenuBranchesStrings.Fisher_Leave, FungusBlocks.Fisher_End, null)));
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.OutsideWithBotDates.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotArtGalleryDate1, FungusBlocks.Outside_ArtGalleryDate1, null, StoryCondition.FlagNotSet("artgallery1"), StoryCondition.FlagSet("bigDaddysAdviceFinished")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotArtGalleryDate2, FungusBlocks.Outside_ArtGalleryDate2, null, StoryCondition.FlagNotSet("artgallery2"), StoryCondition.FlagSet("bigDaddysAdviceFinished"), StoryCondition.FlagSet("artgallery1")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotRestaurantDate1, FungusBlocks.Outside_RestaurantDate1, null, StoryCondition.FlagNotSet("restaurant1")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotRestaurantDate2, FungusBlocks.Outside_RestaurantDate2, null, StoryCondition.FlagNotSet("restaurant2"), StoryCondition.FlagSet("restaurant1")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotKino1, FungusBlocks.Outside_Kino1, null, StoryCondition.FlagNotSet("kino1"), StoryCondition.FlagSet("kinoTalk")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotKino2, FungusBlocks.Outside_Kino2, null, StoryCondition.FlagNotSet("kino2"), StoryCondition.FlagSet("kinoTalk2")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotKaraoke1, FungusBlocks.Outside_Karaoke1, null, StoryCondition.FlagNotSet("Karaoke1"), StoryCondition.FlagSet("KaraokeTalk")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotIceSkating1, FungusBlocks.Outside_IceSkating1, null, StoryCondition.FlagNotSet("ice_skating1"), StoryCondition.FlagSet("church-nun_back7"), StoryCondition.CommonConditions.AfterRiots), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotPlanetarium, FungusBlocks.Outside_Planetarium1, null, StoryCondition.FlagNotSet("planetarium1"), StoryCondition.MoneyAbove(10000), StoryCondition.FlagSet("Jun_inv_star_gazing")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotAmusementPark2, FungusBlocks.Outside_AmusementPark2, null, StoryCondition.FlagNotSet("amusement_park2"), StoryCondition.MoneyAbove(10000), StoryCondition.FlagSet("amusement_park1")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotGoFishingTogether, FungusBlocks.Outside_FishingDate1, "Fishing_date1", StoryCondition.FlagSet("Fish_joint7_talk"), StoryCondition.HourBetweenEqual(10, 18)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotGoFishingTogether, FungusBlocks.Outside_FishingDate2, "Fishing_date2", StoryCondition.FlagSetBeforeRelative("Fishing_date1", 720), StoryCondition.HourBetweenEqual(10, 18)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotAquariumDate, FungusBlocks.Outside_AquariumDate, null, StoryCondition.FlagSetBeforeRelative("amusement_park2", 720), StoryCondition.MoneyAbove(100000), StoryCondition.FlagNotSet("aquarium_date")), new StoryMenuBranch(storyMenuBranchesStrings.Nevermind, FungusBlocks.Outside_OutsideWithBot_Label_StartOutside, null)));
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.OutsideWithBotFriends.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotIceSkatingWithShanice, FungusBlocks.Outside_IceSkating2Shanice, "ice_skating2", StoryCondition.FlagSet("ice_skating1"), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotCafeWithAnnalie, FungusBlocks.Outside_AnnalieCafe2, "Annalie_cafe2", StoryCondition.FlagSet("Annalie_cafe1"), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotMeetupWithGrumpyAnnalie, FungusBlocks.Outside_AnnalieESL3, "Annalie_ending3", StoryCondition.FlagSet("Annalie_ending2"), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500), StoryCondition.Or(StoryCondition.FlagNotSet("church-endJustGoThereGood"), StoryCondition.FlagSet("ice_skating1"))), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotCheckOutTheRunway, FungusBlocks.Outside_AnnalieESL5, "Annalie_ending5", StoryCondition.FlagSet("Annalie_ending4"), StoryCondition.HourBetweenEqual(11, 22)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotMeetAtCafeWithAnnalieAndHerFriends, FungusBlocks.Outside_AnnalieESL7, "Annalie_ending7", StoryCondition.FlagSetBeforeRelative("Annalie_ending6", 720), StoryCondition.HourBetweenEqual(11, 17)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotGoToAnnaliesFashionShow, FungusBlocks.Outside_AnnalieESL8, "Annalie_ending8", StoryCondition.FlagSetBeforeRelative("Annalie_ending7", 720), StoryCondition.HourBetweenEqual(10, 18)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotBowlingWithMelissa, FungusBlocks.Outside_BowlingMelissa, "bowling1", StoryCondition.CommonConditions.Melissa5Done, StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotBowlingWithMelissaAndAnnalie_Melissa9, FungusBlocks.Outside_Melissa9, "Melissa9", StoryCondition.FlagSet("Melissa8"), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotBowlingWithMelissaAndAnnalie_Melissa11, FungusBlocks.Outside_Melissa11, "Melissa11", StoryCondition.FlagSetBeforeRelative("Melissa10", 720), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotKaraokeWithMelissaAndAnnalie_Melissa12, FungusBlocks.Outside_Melissa12, "Melissa12", StoryCondition.FlagSetBeforeRelative("Melissa11", 720), StoryCondition.FlagSet("Karaoke1"), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotCafeWithMelissaAndAnnalie_Melissa16, FungusBlocks.Outside_Melissa16, "Melissa16", StoryCondition.FlagSetBeforeRelative("Melissa15", 720), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotGokartsWithMelissaAndAnnalie_Melissa17, FungusBlocks.Outside_Melissa17, "Melissa17", StoryCondition.FlagSetBeforeRelative("Melissa16", 720), StoryCondition.FlagSet("gokart1"), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotBowlingWithMelissaAndAnnalie_Melissa19, FungusBlocks.Outside_Melissa19, "Melissa19", StoryCondition.FlagSetBeforeRelative("Melissa18", 1080), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotCafeWithMelissaAndAnnalie_Melissa21, FungusBlocks.Outside_Melissa21, "Melissa21", StoryCondition.FlagSet("Melissa20"), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotKaraokeWithMelissa_Melissa21_5, FungusBlocks.Outside_Melissa21dot5, "Melissa21.5", StoryCondition.FlagSet("Melissa21"), StoryCondition.FlagSet("karaoke_idol"), StoryCondition.HourBetweenEqual(10, 18), StoryCondition.MoneyAbove(500)), new StoryMenuBranch(storyMenuBranchesStrings.Nevermind, FungusBlocks.Outside_OutsideWithBot_Label_StartOutside, null)));
		_storyMenuEntryPoints.Add(new StoryMenuEntryPoint(StoryMenuEntryPointsEnum.OutsideWithBot.ToString(), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotGoOnADate, FungusBlocks.Outside_Date, null, StoryCondition.FlagSet("date"), StoryCondition.StoryMenuEntryPointConditionalExistCondition(StoryMenuEntryPointsEnum.OutsideWithBotDates.ToString())), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotHangOutWithFriends, FungusBlocks.Outside_Friends, null, StoryCondition.FlagSet("date"), StoryCondition.StoryMenuEntryPointConditionalExistCondition(StoryMenuEntryPointsEnum.OutsideWithBotFriends.ToString())), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotTrickOrTreat, FungusBlocks.Outside_Halloween_TOTStart, null, StoryCondition.VariableConditionBoolTrue("HalloweenActive"), StoryCondition.FlagSet("ClothierHalloweenWithBot2")), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotClothierWithBot, FungusBlocks.Outside_Clothier_startWithBot, null), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotWalk, FungusBlocks.Outside_Walk_WalkWithBotStart, null, StoryCondition.FlagSetAfterRelative("WalkWithBot", 360, negate: true)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotInvestigateLab, FungusBlocks.Outside_NunBack7, "church-nun_back7", StoryCondition.FlagSet("church-nun_back6"), StoryCondition.HourBetweenEqual(18, 22)), new StoryMenuBranch(storyMenuBranchesStrings.OutsideWithBotGoBackHome, FungusBlocks.Outside_OutsideEndWithBot, null)));
	}

	public Flowchart GetFlowchart()
	{
		return SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart;
	}

	public Block CreatEmptyFungusBlock()
	{
		if (!Application.isPlaying)
		{
			Debug.LogError("Creating empty fungus blocks when not in the players is very wrong!!!! Something probable died!");
		}
		return GetFlowchart().CreateBlock(Vector2.right * 5f, "Temporary Block");
	}

	public T AddCommandToFungusBlock<T>(Block block) where T : Command
	{
		Flowchart flowchart = GetFlowchart();
		T val = flowchart.gameObject.AddComponent<T>();
		val.ItemId = flowchart.NextItemId();
		val.ParentBlock = block;
		val.CommandIndex = block.CommandList.Count;
		val.OnCommandAdded(block);
		block.AddCommandAtEnd(val);
		return val;
	}

	public void DestroyBlock(Block block)
	{
		foreach (Command command in block.CommandList)
		{
			command.OnCommandRemoved(block);
		}
		foreach (Command command2 in block.CommandList)
		{
			UnityEngine.Object.Destroy(command2);
		}
		UnityEngine.Object.Destroy(block);
	}

	public void MakeSureInitialized()
	{
		if (!_initialized || _storyAutoEntryPoints.Count == 0)
		{
			InitializeStoryEntryPoints();
			InitializeStoryMenuEntryPoints();
			InitializeStoryBotDialogueEntryPoint();
			_initialized = true;
		}
	}

	public string GenerateSummary(bool richText = false)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine("Menu entry points:");
			for (int i = 0; i < _storyMenuEntryPoints.Count; i++)
			{
				StoryMenuEntryPoint storyMenuEntryPoint = _storyMenuEntryPoints[i];
				stringBuilder.AppendLine($"{i + 1}. \"{storyMenuEntryPoint.Id}\"");
				storyMenuEntryPoint.AppendExplanation(stringBuilder);
				stringBuilder.AppendLine("Branches:");
				foreach (StoryMenuBranch branch in storyMenuEntryPoint.Branches)
				{
					stringBuilder.AppendLine(branch.Summary(richText));
					stringBuilder.AppendLine();
				}
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine("--------------------------------------------------------------------");
			stringBuilder.AppendLine("Auto entry points:");
			for (int j = 0; j < _storyAutoEntryPoints.Count; j++)
			{
				StoryAutoEntryPoint storyAutoEntryPoint = _storyAutoEntryPoints[j];
				stringBuilder.AppendLine($"{j + 1}. \"{storyAutoEntryPoint.Id}\"");
				storyAutoEntryPoint.AppendExplanation(stringBuilder);
				stringBuilder.AppendLine("Branches:");
				foreach (StoryAutoBranch branch2 in storyAutoEntryPoint.Branches)
				{
					stringBuilder.AppendLine(branch2.Summary(richText));
					stringBuilder.AppendLine();
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}
	}

	public static void ParseOldSystem(LuaCondition startingCommand)
	{
		Block parentBlock = startingCommand.ParentBlock;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = startingCommand.CommandIndex; i < parentBlock.CommandList.Count; i += 3)
		{
			if (parentBlock.CommandList[i] is LuaCondition luaCondition && parentBlock.CommandList[i + 1] is ModifyFlag modifyFlag && parentBlock.CommandList[i + 2] is Call call)
			{
				string blockName = call.targetBlock.BlockName;
				string flag = modifyFlag.flag;
				List<string> list = luaCondition.luaCompareString.Split(new string[1] { " and " }, StringSplitOptions.None).ToList();
				int num = list.RemoveAll((string s) => s.StartsWith("not") && s.Contains(flag));
				string text = ((list.Count > 0) ? (", \n()=>(" + string.Join(" && ", list) + ")") : "");
				if (num == 1)
				{
					stringBuilder.AppendLine("new StoryEntryPointBranch(\n\"" + blockName + "\", \n\"" + flag + "\" " + text + "),");
				}
				else
				{
					stringBuilder.AppendLine("new StoryEntryPointBranch(\n\"" + blockName + "\", \n\"" + flag + "\" " + text + "),//THIS ONE IS WRONG!!!");
				}
			}
			else
			{
				Debug.LogError($"Something wrong at index {i}");
			}
		}
		Debug.Log(stringBuilder.ToString());
	}
}
