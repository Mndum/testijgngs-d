using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using GameItems;
using JetBrains.Annotations;
using StoryConditions;
using UnityEngine;

[Serializable]
[GenerateNonJitClassDescription]
public sealed class GameVariables : ISerializationCallbackReceiver, IHasNonJitClassDescription
{
	[Serializable]
	public class SerializableDictStringDialogueChainMemory : SerializableDictionary<string, DialogueChainMemory>
	{
	}

	public enum StaminaSyncType
	{
		Normal,
		None,
		Recovery
	}

	public enum EatSound
	{
		Crunch,
		CrackOpenAColdOne
	}

	[Serializable]
	public class Flag
	{
		[SerializeField]
		private string name;

		[SerializeField]
		private int timeAdded;

		[SerializeField]
		private int firstTimeAdded;

		[SerializeField]
		private int times;

		public string Name => name;

		public int LastTimeAdded => timeAdded;

		public int FirstTimeAdded => firstTimeAdded;

		public int TimesSet => times;

		public Flag(string _name, int _firstTimeAdded)
		{
			name = _name;
			firstTimeAdded = (timeAdded = _firstTimeAdded);
			times = 1;
		}

		public void Report(int time)
		{
			timeAdded = time;
			times++;
		}

		public void EnsureValid()
		{
			if (firstTimeAdded > timeAdded || firstTimeAdded == 0)
			{
				firstTimeAdded = timeAdded;
			}
			if (times <= 0)
			{
				times = 1;
			}
		}
	}

	public enum CheckType
	{
		Full,
		Start,
		End
	}

	public enum TimeCheckType
	{
		None,
		Before,
		After,
		BeforeRelative,
		AfterRelative
	}

	[HideInInspector]
	[SerializeField]
	private List<Email> emails;

	[SerializeField]
	private List<string> serializedEmails;

	[SerializeField]
	private List<NewEmail> _allEmails;

	[NonSerialized]
	private GameStats _gameStats = new GameStats();

	[NonSerialized]
	private bool _areGameStatsValid;

	public string gameVersion;

	public StockManager stockManager;

	public Item.ItemManager itemManager;

	public EventManager eventManager;

	public CockTwitchManager cockTwitchManager;

	public CocktractManager cocktractManager;

	public DeliveryManager deliveryManager;

	public CookingMinigameManager cookingMinigameManager;

	public FishingMinigameManager fishingMinigameManager;

	public BotStatusAppManager botStatusAppManager;

	public JoinUsBlogManager joinUsBlogManager;

	public SpecialVariablesHolder customData;

	[Header("Stats:")]
	public int timesWentToChurch;

	public int streamCount;

	public int streamedFor;

	public int moneyEarnedFromDonations;

	public int longestStream;

	public int timesCameInside;

	public int timesCameInsideAnal;

	public int timesCameThighjob;

	public int timesCameOutside;

	public int timesCameInMouth;

	public float mlCameInMouth;

	public float mlCameInVagina;

	public float mlCameInAss;

	public float mlCameFromThighjob;

	public float mlCameOutside;

	public float mlOfCumWasted;

	public int timesLostChess;

	public int timesWonChess;

	public int timesLostOldMaid;

	public int timesWonOldMaid;

	public int timesRanAwayOldMaid;

	public int timesLostWordChain;

	public int timesWonWordChain;

	[Header("Other")]
	[SerializeField]
	private List<Color> presetColors;

	public bool lightSwitchOn;

	[UsedInLua]
	public bool bathroomLightOn;

	[Header("Dialogue chain remembering")]
	public NewsDataManager newsDataManager;

	[SerializeField]
	public SerializableDictStringDialogueChainMemory dialogueChainData;

	public string statusText = "";

	public static readonly int FollowersNeededForSlowMode = 800;

	private const int RiotsLength = 5;

	[Header("Story flags:")]
	[SerializeField]
	private List<Flag> flags;

	private Dictionary<string, Flag> _flagsDictionary = new Dictionary<string, Flag>();

	public float search;

	public int priestBotPoints;

	public int nunPoints;

	[Header("Player stats:")]
	[SerializeField]
	public int money;

	public const int maxMoney = 2000000000;

	public int casinoTokens;

	[SerializeField]
	private float _maxCum;

	[SerializeField]
	private float _remainingCum;

	[SerializeField]
	private float _stamina;

	[SerializeField]
	private float _mentalHealth;

	[SerializeField]
	private float _mentalHealthTemporary;

	[SerializeField]
	private float _satiation;

	[SerializeField]
	private float _health;

	[Header("Item effects")]
	public int vinegaraEffectEnd;

	public int deathGripEffectEnd;

	public const float MaxCumSafeAmount = 700f;

	public const float MaxCumAliveAmount = 1000f;

	[Header("Bot stats:")]
	[SerializeField]
	private int _lust;

	[SerializeField]
	private float _longing;

	[SerializeField]
	private float _currentHorniness;

	[SerializeField]
	private int _sympathy;

	[SerializeField]
	private float _mood;

	[SerializeField]
	private int inteligence;

	[SerializeField]
	public Live2DControllerCommonState botLive2DCommonState;

	public string playerName;

	public string botName;

	public static List<int> StageInteligenceLimit = new List<int> { 0, 20, 40, 40 };

	public int stage = 1;

	[SerializeField]
	private int _uniqueConversationsLeft;

	public int time;

	[Header("Timers:")]
	public int lastWorkedAtDay;

	public int lastWentToChurchAt;

	public int lastCuddledAt;

	public bool lastSleptWithBot;

	public int lastWokeUpAt;

	public int lastFuckedAt;

	public int lastBotCameAt;

	public int lastInteractAt;

	public int lastEquipmentAt;

	public int lastOutsideWithBotAt;

	public int lastStreamedAt;

	public int lastTalkedAt;

	public int lastBotStartedTalkAt;

	public int lastHeadpatedAt;

	public int lastAnonsShowerAt;

	public int leastBotCleanAt;

	public int lastHungerInfoAt;

	public int lastMentalHealthInfoAt;

	[Header("Stream stats:")]
	public int subs;

	public int followers;

	public IEnumerable<NewEmail> VisibleEmails => _allEmails.Where((NewEmail x) => x.IsVisible(time));

	public bool AreGameStatsValid => _areGameStatsValid;

	public static string CurrentGameVersion => Application.version;

	public ShopManager ShopManager => itemManager.ShopManager;

	public int TimesCameAll => timesCameInside + timesCameInsideAnal + timesCameOutside + timesCameInMouth + timesCameThighjob;

	[UsedInLua]
	public string JournalPath => "F:\\files\\research\\journal\\1574_12_12.txt";

	public bool BotKnowsAboutStreaming => SingletonMonoBehaviour<StorySingleton>.Instance.Stage3.GetMemory().IsIgnored(GameId.CreateVanilla(13000));

	public bool BotSaidSheCanTasteOnlyCum => SingletonMonoBehaviour<StorySingleton>.Instance.Stage3.GetMemory().IsIgnored(GameId.CreateVanilla(11002));

	public bool StartedStreamingAfterPrologue => !CheckFlag("DidntStreamAfterPrologue");

	private GameId BotJealousOfNunDialogueId => GameId.CreateVanilla(15002);

	public bool BotWasJealousOfNun => SingletonMonoBehaviour<StorySingleton>.Instance.Stage3.GetMemory().IsIgnored(BotJealousOfNunDialogueId);

	public bool ZombieApocalypseDialogueHappened => SingletonMonoBehaviour<StorySingleton>.Instance.Stage3.GetMemory().IsIgnored(15006);

	public bool FirstHorrorStoryDialogueHappened => SingletonMonoBehaviour<StorySingleton>.Instance.Stage3.GetMemory().IsIgnored(15009);

	public bool SecondHorrorStoryHappened => SingletonMonoBehaviour<StorySingleton>.Instance.Stage3.GetMemory().IsIgnored(15024);

	public bool HasAnySoap => itemManager.HasItem((Item x) => x.GetItemPrefab().Category == ItemCategory.Soap);

	public int TimesShoweredTogether => GetFlagSetCount("ShowerTogetherComplete");

	public int TimesAnonShower => GetFlagSetCount("ShowerAnon");

	public int TimesBotShower => GetFlagSetCount("ShowerBot");

	public int CookedDishesCount => cookingMinigameManager.GetPlayCount();

	public int DishesPassedAtLeastBCount => cookingMinigameManager.GetAtLeastGradeCount(CookingGrade.B);

	public bool CanTakeFisherToShow
	{
		get
		{
			if (fishingMinigameManager.FishCaughtCount >= 25)
			{
				return CheckFlag("fishing4");
			}
			return false;
		}
	}

	public bool HasGoodChurchEnding => CheckFlag("church-endJustGoThereGood");

	public bool CanTakeShaniceToShow => CheckFlag("church-nun_back1");

	public int TimesAnonCameDuringLastSexScene => botLive2DCommonState.FrickData.D_TimesAnonCame;

	public bool MadeNoiseLastSexScene => botLive2DCommonState.FrickData.D_TimesAnonCame > 0;

	[UsedInLua]
	public bool IsThereCumOnHer => botLive2DCommonState.IsThereCumOnHer;

	public bool IsBotAwake => stage >= 2;

	public bool IsBotSmart => stage == 3;

	public int TimeDay => time % 1440;

	public int Day => TimeToDay(time);

	public int Hour => TimeToHour(time);

	public int Minute => time % 60;

	public DateTime GameDateTime => RawGameTimeToGameDateTime(time);

	public int DayOfTheWeek => TimeToDayOfTheWeek(time);

	[UsedInLua]
	public int FishesCaught => fishingMinigameManager.FishCaughtCount;

	public int DifferentFishesCaught => fishingMinigameManager.DifferentFishesCaughtCount;

	public bool HasAtLeastOneLeg
	{
		get
		{
			if (itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.LegL.Id) == null)
			{
				return itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.LegR.Id) != null;
			}
			return true;
		}
	}

	public bool HasBothLegs
	{
		get
		{
			if (itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.LegL.Id) != null)
			{
				return itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.LegR.Id) != null;
			}
			return false;
		}
	}

	public bool HasAtLeastOneArm
	{
		get
		{
			if (itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ArmL.Id) == null)
			{
				return itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ArmR.Id) != null;
			}
			return true;
		}
	}

	public bool HasBothArms
	{
		get
		{
			if (itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ArmL.Id) != null)
			{
				return itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ArmR.Id) != null;
			}
			return false;
		}
	}

	public bool HasAtLeast5UnhealthyFoods => FungusEnumerators.UnhealthyFoodGameIds.Sum((GameId x) => itemManager.GetItemCount(x)) >= 5;

	public bool IsWearingPanties
	{
		get
		{
			Item item;
			return itemManager.TryGetItemAtSlot(ItemSlot.SlotManager.Panties, out item);
		}
	}

	public int AfterSexCleanedCount => GetFlagSetCount("AfterSex_Rag") + GetFlagSetCount("AfterSex_ShowerAlone") + GetFlagSetCount("AfterSex_ShowerTogether");

	public int AfterSexLeftCount => GetFlagSetCount("AfterSex_LeaveCum");

	public bool UsuallyCleansCumAfterSex
	{
		get
		{
			CumCleaningStats(out var cleanedRatio, out var sum);
			if (cleanedRatio > 0.7f)
			{
				return sum > 3;
			}
			return false;
		}
	}

	public bool UsuallyLeavesCumAfterSex
	{
		get
		{
			CumCleaningStats(out var cleanedRatio, out var sum);
			if (cleanedRatio < 0.3f)
			{
				return sum > 3;
			}
			return false;
		}
	}

	public bool HasBothLegsAndAtLeastOneArm
	{
		get
		{
			if (HasBothLegs)
			{
				return HasAtLeastOneArm;
			}
			return false;
		}
	}

	public bool HasAllLimbs
	{
		get
		{
			if (HasBothArms)
			{
				return HasBothLegs;
			}
			return false;
		}
	}

	public bool CanDoDoggy => HasBothLegs;

	public bool CanDoBlowjob => IsBotAwake;

	public bool CanDoCowgirl
	{
		get
		{
			if (HasBothLegs && IsBotSmart && HasBothArms && lust > 75)
			{
				return sympathy > 75;
			}
			return false;
		}
	}

	public bool ReadyForNextStage
	{
		get
		{
			if (stage >= 0 && stage < StageInteligenceLimit.Count)
			{
				if (stage == StageInteligenceLimit.Count - 1)
				{
					return false;
				}
				return inteligence == StageInteligenceLimit[stage];
			}
			Debug.LogError("Something went horrribly wrong with stages!!!! Reseting to stage 1!!");
			stage = 1;
			inteligence = 0;
			return false;
		}
	}

	public bool IsWearingCatEars
	{
		get
		{
			Item firstItemAtSlot = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.Ears.Id);
			if (firstItemAtSlot != null)
			{
				return firstItemAtSlot.GetVanillaItemEnum() == ItemEnum.CatEars;
			}
			return false;
		}
	}

	public bool CanStand
	{
		get
		{
			if (HasBothLegs)
			{
				return IsBotAwake;
			}
			return false;
		}
	}

	public bool IsWearingAnyClothes => itemManager.BotEquippedItems().Any((Item x) => x.GetItemPrefab().Category == ItemCategory.Clothing);

	public bool CanEnableSlowMode
	{
		get
		{
			if (followers > FollowersNeededForSlowMode)
			{
				return CheckFlag("unlockedslowmode");
			}
			return false;
		}
	}

	public bool IsRentDue
	{
		get
		{
			GetRentData(out var daysTillRent, out var _);
			return daysTillRent <= 0;
		}
	}

	public bool IsAboveADayLateOnRent
	{
		get
		{
			GetRentData(out var daysTillRent, out var _);
			return daysTillRent < 0;
		}
	}

	public bool IsNaked => GetGameStats().EquipmentGameStats.WouldBeConsideredNaked();

	[UsedInLua]
	public bool IsAnnalieOnIslandEnding
	{
		get
		{
			if (!CheckFlag("Annalie_ending9"))
			{
				return CheckFlag("Annalie_gave_money");
			}
			return true;
		}
	}

	[UsedInLua]
	public bool IsFisherOnIslandEnding
	{
		get
		{
			if (!CheckFlag("runway_fisher"))
			{
				return CanTakeFisherToShow;
			}
			return true;
		}
	}

	[UsedInLua]
	public bool IsLandlordOnIslandEnding => CheckFlag("Landlord_ending7");

	[UsedInLua]
	public bool IsBritanniaOnIslandEnding => CheckFlag("Britannia_island");

	[UsedInLua]
	public bool IsShaniceAndPriestbotOnIslandEnding => CheckFlag("church-nun_back1");

	[UsedInLua]
	public bool IsEveryoneOnTheIsland
	{
		get
		{
			if (IsAnnalieOnIslandEnding && IsFisherOnIslandEnding && IsLandlordOnIslandEnding && IsBritanniaOnIslandEnding)
			{
				return IsShaniceAndPriestbotOnIslandEnding;
			}
			return false;
		}
	}

	public bool HasTheOptionToGoOutsideWithBot
	{
		get
		{
			if (CheckFlag("GoingOutside2"))
			{
				return sympathy > 100;
			}
			return false;
		}
	}

	public bool CanGoOutsideWithBot
	{
		get
		{
			if (HasTheOptionToGoOutsideWithBot && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.GetGameStats().EquipmentGameStats.CanGoOutside.IsSuccess && !RiotsActive)
			{
				return !StoryCondition.CommonConditions.JoinUsDAKDanger.ConditionMet();
			}
			return false;
		}
	}

	public bool IsItSafeToStartLongActivity
	{
		get
		{
			if ((double)Satiation > 0.4)
			{
				return (double)Stamina > 0.4;
			}
			return false;
		}
	}

	public bool EnoughStaminaForLongActivity => (double)Stamina > 0.4;

	public bool HasMetAnnalie => CheckFlag("clothiervisit");

	public bool CanCook
	{
		get
		{
			if (!itemManager.HasItem(ItemEnum.LandlordsOldStove.ToGameId()))
			{
				return itemManager.HasItem(ItemEnum.FixedLandlordsOldStove.ToGameId());
			}
			return true;
		}
	}

	public bool HasAnnalieHairstyle
	{
		get
		{
			if (itemManager.TryGetItemsAtSlot(ItemSlot.SlotManager.HairMain.Id, out var _items) && _items.Count == 1)
			{
				return _items[0].GameId == ItemEnum.HypercamoClothierHair.ToGameId();
			}
			return false;
		}
	}

	public bool BathroomDoorInstalled => CheckFlag("Bathroom3");

	public bool FirstShowerHappened => CheckFlag("Bathroom4");

	public float sus => GetGameStats().EquipmentGameStats.SusRating;

	public float FishJointStatBoost
	{
		get
		{
			float num = 1f;
			if (CheckFlag("Fish_joint1_win"))
			{
				num += 0.1f;
			}
			if (CheckFlag("Fish_joint2_win"))
			{
				num += 0.1f;
			}
			if (CheckFlag("Fish_joint3_win"))
			{
				num += 0.1f;
			}
			if (CheckFlag("Fish_joint4_win"))
			{
				num += 0.1f;
			}
			return num;
		}
	}

	public int BotDiscoveryRentIncreaseValue
	{
		get
		{
			if (CheckFlag("good_soul"))
			{
				return 300;
			}
			return 400;
		}
	}

	[UsedInLua]
	public int WeeklyRent
	{
		get
		{
			int num = 800;
			if (CheckFlag("LandlordConspiracy2"))
			{
				num += BotDiscoveryRentIncreaseValue;
			}
			if (CheckFlag("Bathroom2_bought"))
			{
				num += 1000;
			}
			if (!CheckFlag("ShadyLawyerTrickedLandlord"))
			{
				if (CheckFlagBeforeRelative("FirstPaymentDayLate", 5))
				{
					num += 100;
				}
				if (CheckFlagBeforeRelative("SecondPaymentDayLate", 5))
				{
					num += 250;
				}
			}
			return num;
		}
	}

	public float susandsearch => sus + search / 2f;

	public int MoneySpentOnClothes
	{
		get
		{
			if (itemManager.ShopManager.TryGetShop("clothier", out var shop))
			{
				return shop.MoneySpent;
			}
			return 0;
		}
	}

	public float Random01 => UnityEngine.Random.value;

	public bool CanHalloweenStart
	{
		get
		{
			DateTime now = DateTime.Now;
			return Math.Abs((new DateTime(now.Year, 10, 31, 12, 0, 0) - now).TotalDays) <= 10.0;
		}
	}

	public bool HalloweenActive => CheckFlagAfterRelative("HalloweenStarted", 5760);

	public bool HalloweenStarted => CheckFlag("HalloweenStarted");

	public bool HalloweenHappened
	{
		get
		{
			if (HalloweenStarted)
			{
				return !HalloweenActive;
			}
			return false;
		}
	}

	public bool MajorOutsideEventActive
	{
		get
		{
			if (!HalloweenActive)
			{
				return RiotsActive;
			}
			return true;
		}
	}

	public bool AnyChurchEndingDone => CheckFlagSpecial("church-end", CheckType.Start);

	public bool WorkedTodayAlready => lastWorkedAtDay == Day;

	public bool EnoughStaminaToWork => Stamina > 0.5f;

	public bool LandlordDecorationsUp
	{
		get
		{
			if (!itemManager.IsEquipped(ItemEnum.BatBanner))
			{
				return itemManager.IsEquipped(ItemEnum.GhostDecoration);
			}
			return true;
		}
	}

	[UsedInLua]
	public bool EverCameInBot
	{
		get
		{
			if (timesCameInside <= 0 && timesCameInsideAnal <= 0)
			{
				return timesCameInMouth > 0;
			}
			return true;
		}
	}

	public bool SpiderPlushUp => itemManager.IsEquipped(ItemEnum.SpiderPlush);

	public bool HasWitchHat => itemManager.IsEquipped(ItemEnum.TinyWizardHat);

	public bool BotMetAnnalie => CheckFlag("GoingOutside3");

	public bool HasPointyEars
	{
		get
		{
			if (!itemManager.IsEquipped(ItemEnum.PointyEars))
			{
				return itemManager.IsEquipped(ItemEnum.HyperCamoPointyEars);
			}
			return true;
		}
	}

	public bool IsLateGame
	{
		get
		{
			if (Day > 15 && IsBotSmart)
			{
				return sympathy > 500;
			}
			return false;
		}
	}

	[UsedInLua]
	public bool RiotsActive => CheckFlagAfterRelative("riots_start_event", 7200);

	public bool RiotsEndingSoon
	{
		get
		{
			if (RiotsActive)
			{
				return CheckFlagBeforeRelative("riots_start_event", 4320);
			}
			return false;
		}
	}

	[UsedInLua]
	public bool RiotsAfter
	{
		get
		{
			if (CheckFlag("riots_start_event"))
			{
				return !RiotsActive;
			}
			return false;
		}
	}

	[UsedInLua]
	public bool HasHalloweenCostume
	{
		get
		{
			if (!HasPointyEars)
			{
				return HasWitchHat;
			}
			return true;
		}
	}

	public float MedicineDiscount
	{
		get
		{
			if (cocktractManager.TryGetPartnerById(CocktractPartnerPrefabManager.monizmedId, out var partner))
			{
				return Mathf.Clamp((float)partner.GetSpecialData<GenericCocktractSpecialData>().NextMilestoneLevel * 0.15f, 0f, 0.8f);
			}
			return 0f;
		}
	}

	[UsedInLua]
	public int DispenserLevel
	{
		get
		{
			if (cocktractManager.TryGetPartnerById(CocktractPartnerPrefabManager.diabeteId, out var partner))
			{
				return partner.GetSpecialData<GenericCocktractSpecialData>().NextMilestoneLevel;
			}
			return 0;
		}
	}

	public int ClothierItemsBought => itemManager.ShopManager.GetShop("clothier").ItemsBought;

	[UsedInLua]
	public bool EverStartedStream => CheckFlag("StreamStarted");

	[UsedInLua]
	public bool NeverStartedStream => !EverStartedStream;

	[UsedInLua]
	public bool CanStartStream => StoryCondition.CommonConditions.CanStartStream.ConditionMet();

	public static GameVariables Current => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;

	public static GameVariablesClassDescription ClassDescription => GameVariablesClassDescription.Instance;

	public IEnumerable<string> AllFlags => _flagsDictionary.Keys;

	public bool VinegaraActive => time < vinegaraEffectEnd;

	public bool DeathGripActive => time < deathGripEffectEnd;

	public float RemainingCum
	{
		get
		{
			return _remainingCum;
		}
		set
		{
			_remainingCum = Mathf.Clamp(value, 0f, MaxCum * 2f);
		}
	}

	public float MaxCum
	{
		get
		{
			return _maxCum;
		}
		set
		{
			_maxCum = Mathf.Max(value, 0.5f);
		}
	}

	public float Stamina
	{
		get
		{
			return _stamina;
		}
		set
		{
			_stamina = Mathf.Clamp(value, 0f, 1f);
		}
	}

	public bool IsOutOfStamina => Stamina <= 0f;

	public float Satiation
	{
		get
		{
			return _satiation;
		}
		set
		{
			_satiation = Mathf.Clamp(value, 0f, 1f);
		}
	}

	public float RealMentalHealth => _mentalHealth;

	public float MentalHealth
	{
		get
		{
			return _mentalHealth + _mentalHealthTemporary;
		}
		set
		{
			float num = value - MentalHealth;
			float num2 = _mentalHealth + num;
			if (num2 <= 0f)
			{
				MentalHealthTemporary += num2;
				_mentalHealth = 0f;
			}
			else
			{
				_mentalHealth = Mathf.Clamp(num2, 0f, 1f);
			}
		}
	}

	public float MentalHealthTemporary
	{
		get
		{
			return _mentalHealthTemporary;
		}
		set
		{
			_mentalHealthTemporary = Mathf.Clamp(value, 0f, 1f);
		}
	}

	public float InverseMentalHealth => 1f - MentalHealth;

	public float Health
	{
		get
		{
			return _health;
		}
		set
		{
			_health = Mathf.Clamp(value, 0f, 1f);
		}
	}

	public float CurrentHorniness
	{
		get
		{
			return _currentHorniness;
		}
		set
		{
			_currentHorniness = (IsBotAwake ? Mathf.Clamp01(value) : 0f);
		}
	}

	public int lust
	{
		get
		{
			return _lust;
		}
		set
		{
			int num = value - _lust;
			Longing += (float)num / 100f;
			_lust = value;
		}
	}

	public int sympathy
	{
		get
		{
			return _sympathy;
		}
		set
		{
			int num = value - _sympathy;
			Mood += (float)num / 20f;
			if (num > 0)
			{
				MentalHealth += (float)num / 30f;
			}
			_sympathy = value;
		}
	}

	public float PotentialHorniness => Mathf.Max(_longing / 10f, _currentHorniness);

	public float Longing
	{
		get
		{
			return _longing;
		}
		set
		{
			_longing = (IsBotAwake ? Mathf.Clamp(value, 0f, 10f) : 0f);
		}
	}

	public float Mood
	{
		get
		{
			return _mood;
		}
		set
		{
			_mood = (IsBotAwake ? Mathf.Clamp(value, -1f, 1f) : 0f);
		}
	}

	public int Inteligence
	{
		get
		{
			return inteligence;
		}
		set
		{
			inteligence = value;
			if (stage >= 0 && stage < StageInteligenceLimit.Count)
			{
				int num = StageInteligenceLimit[stage];
				if (inteligence > num)
				{
					inteligence = num;
				}
			}
			else
			{
				Debug.LogError("Something went horrribly wrong with stages!!!! Reseting to stage 1!!");
				stage = 1;
				inteligence = 0;
			}
		}
	}

	public float CumInside => botLive2DCommonState.CumInsideVaginaAndWomb;

	public float CumInsideAnal
	{
		get
		{
			return botLive2DCommonState.CumInsideAnal;
		}
		set
		{
			botLive2DCommonState.CumInsideAnal = value;
		}
	}

	public float CumInsideStomach
	{
		get
		{
			return botLive2DCommonState.CumInsideStomach;
		}
		set
		{
			botLive2DCommonState.CumInsideStomach = value;
		}
	}

	public int UniqueConversationsLeft
	{
		get
		{
			return _uniqueConversationsLeft;
		}
		set
		{
			_uniqueConversationsLeft = Mathf.Clamp(value, 0, 5);
		}
	}

	public int TimeSinceLastWentToChurch => time - lastWentToChurchAt;

	public int TimeSinceLastCuddle => time - lastCuddledAt;

	public int AwakeFor => time - lastWokeUpAt;

	public bool EverFuckedBot => lastFuckedAt > 0;

	public int TimeSinceLastFuck => time - lastFuckedAt;

	public int TimeSinceBotCame => time - lastBotCameAt;

	public int TimeSinceLastInteract => time - lastInteractAt;

	public int TimeSinceLastEquipment => time - lastEquipmentAt;

	public int TimeSinceLastWentOutsideWithBot => time - lastOutsideWithBotAt;

	public int TimeSinceLastSawBot => Mathf.Min(TimeSinceLastInteract, TimeSinceLastFuck, TimeSinceLastWentOutsideWithBot, TimeSinceLastEquipment, TimeSinceLastCuddle);

	public int TimeSinceLastStream => time - lastStreamedAt;

	public int TimeSinceLastTalk => time - lastTalkedAt;

	public int TimeSinceBotLastStartedTalk => time - lastBotStartedTalkAt;

	public int TimeSinceLastHeadpat => time - lastHeadpatedAt;

	public int TimeSinceLastAnonsShower => time - lastAnonsShowerAt;

	public int DaysSinceLastAnonsShower => TimeToDay(time) - TimeToDay(lastAnonsShowerAt);

	public int TimeSinceLastBotClean => time - leastBotCleanAt;

	public int DaysSinceLastBotClean => TimeToDay(time) - TimeToDay(leastBotCleanAt);

	public int TimeSinceLastHungerInfo => time - lastHungerInfoAt;

	public bool TimeForNextMentalHealthInfo => TimeSinceLastMentalHealthInfo >= 2160;

	public int TimeSinceLastMentalHealthInfo => time - lastMentalHealthInfoAt;

	public void RemoveAllEmailsWithId(string id)
	{
		_allEmails.RemoveAll((NewEmail x) => x.emailId == id);
	}

	public void RemoveEmail(NewEmail email)
	{
		_allEmails.Remove(email);
	}

	public void RemoveOpenedSpamEmails()
	{
		_allEmails.RemoveAll((NewEmail x) => x.read && x.IsSpam);
	}

	public void AddEmail(NewEmail email)
	{
		EmailPrefabManager.FillPrefabData(email);
		_allEmails.Insert(0, email);
	}

	public void PlanEmail(NewEmail email, [CanBeNull] EventManager.PlanningData planningData)
	{
		if (planningData != null)
		{
			email.visibleAt = planningData.GetPlannedTime(time);
		}
		AddEmail(email);
	}

	public void OnBeforeSerializeEmail()
	{
	}

	public void OnAfterDeserializeEmail()
	{
		List<string> list = serializedEmails;
		if (list != null && list.Count > 0)
		{
			foreach (string serializedEmail in serializedEmails)
			{
				if (EmailBase.Deserialize(serializedEmail) is NewEmail item)
				{
					_allEmails.Add(item);
				}
			}
			serializedEmails.Clear();
		}
		if (emails != null && emails.Count > 0)
		{
			Regex regex = new Regex("{.+?}");
			Regex regex2 = new Regex("[$&+,:;=?@#|'<>.^*()%!-]");
			List<KeyValuePair<string, Email>> list2 = EmailPrefabManager.AllOldEmails.ToList();
			foreach (Email email in emails)
			{
				int num = list2.FindIndex((KeyValuePair<string, Email> oldEmail) => oldEmail.Value.sender == email.sender && oldEmail.Value.text == email.text && oldEmail.Value.topic == email.topic);
				if (num >= 0 && num < list2.Count)
				{
					NewEmail email2 = EmailPrefabManager.GetEmail(list2[num].Key);
					email2.read = email.read;
					_allEmails.Add(email2);
					continue;
				}
				try
				{
					foreach (KeyValuePair<string, Email> item2 in list2)
					{
						if (!item2.Value.text.Contains('{') || !item2.Value.text.Contains('}') || !(item2.Value.sender == email.sender) || !(item2.Value.topic == email.topic))
						{
							continue;
						}
						string input = regex2.Replace(item2.Value.text, "\\$&");
						Regex regex3 = new Regex(regex.Replace(input, (Match t) => "(?<_" + t.Value.Trim('{', '}') + ">.+?)"));
						Match match = regex3.Match(email.text);
						if (match.Success)
						{
							string[] groupNames = regex3.GetGroupNames();
							Dictionary<int, string> dictionary = new Dictionary<int, string>();
							string[] array = groupNames;
							foreach (string text in array)
							{
								if (text.StartsWith("_"))
								{
									int key = int.Parse(text.Substring(1));
									Group obj = match.Groups[text];
									dictionary[key] = obj.Value;
								}
							}
							int num3 = dictionary.Keys.Max();
							List<string> list3 = new List<string>();
							for (int num4 = 0; num4 <= num3; num4++)
							{
								if (dictionary.TryGetValue(num4, out var value))
								{
									list3.Add(value);
								}
								else
								{
									list3.Add("0");
								}
							}
							NewEmail email3 = EmailPrefabManager.GetEmail(item2.Key, list3);
							email3.read = email.read;
							_allEmails.Add(email3);
							break;
						}
						Debug.LogWarning("Failed matching regex email \"" + email.topic + "\" from \"" + email.sender + "\"");
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
			emails.Clear();
		}
		EmailPrefabManager.FillPrefabData(_allEmails);
	}

	public void InvalidateGameStats()
	{
		_areGameStatsValid = false;
	}

	public GameStats GetGameStats()
	{
		if (_areGameStatsValid)
		{
			return _gameStats;
		}
		_gameStats.ResetStats();
		_gameStats.EquipmentGameStats.Recalculate(this);
		_gameStats.FishingGameStats.Recalculate(this);
		List<IGameStatModifier> list;
		using (MyCollectionPool<IGameStatModifier>.GetList(out list))
		{
			foreach (Item item in itemManager.AllEquippedItems())
			{
				item.PopulateGameStatModifierList(list);
			}
			list.Sort(IGameStatModifier.OrderComparer);
			foreach (IGameStatModifier item2 in list)
			{
				item2.ApplyToGameStats(this, _gameStats);
			}
		}
		_areGameStatsValid = true;
		return _gameStats;
	}

	public PersonalityGameStats GetPersonalityGameStats()
	{
		return GetGameStats().PersonalityGameStats;
	}

	private GameVariables()
	{
	}

	private GameVariables(int time, int money)
	{
		this.time = time;
		this.money = money;
		stockManager = StockManager.CreateInstance();
		eventManager = EventManager.CreateInstance();
		itemManager = Item.ItemManager.CreateInstance();
		emails = new List<Email>();
		_allEmails = new List<NewEmail>();
		flags = new List<Flag>();
		presetColors = new List<Color>();
		newsDataManager = NewsDataManager.CreateInstance();
		cockTwitchManager = CockTwitchManager.CreateInstance();
		cocktractManager = CocktractManager.CreateInstance();
		cookingMinigameManager = new CookingMinigameManager();
		fishingMinigameManager = new FishingMinigameManager();
		botStatusAppManager = new BotStatusAppManager();
		deliveryManager = new DeliveryManager();
		joinUsBlogManager = new JoinUsBlogManager();
		botLive2DCommonState = new Live2DControllerCommonState();
		dialogueChainData = new SerializableDictStringDialogueChainMemory();
		RemainingCum = (MaxCum = 20f);
		Stamina = (float)(1620 - (Hour * 60 + Minute)) / 1440f;
		Satiation = (_health = 1f);
		_mentalHealth = 0.9f;
		stage = 1;
		UniqueConversationsLeft = 3;
		gameVersion = CurrentGameVersion;
		FillAllSubClasses();
	}

	public static GameVariables CreateStartOfTheGame(int time, int money)
	{
		return new GameVariables(time, money);
	}

	public void NewGameInitialization()
	{
		stockManager.InitializeNewGame();
		itemManager.ShopManager.ShuffleShops();
		newsDataManager.InitializeNewGame();
		cockTwitchManager.InitializeNewGame();
	}

	private void FillAllSubClasses()
	{
		FieldInfo[] fields = typeof(GameVariables).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (typeof(GameVariablesSubClass).IsAssignableFrom(fieldInfo.FieldType))
			{
				((GameVariablesSubClass)fieldInfo.GetValue(this)).InitGameVariables(this);
			}
		}
	}

	public bool IsAValidSaveAfter075Update()
	{
		if (string.IsNullOrWhiteSpace(gameVersion))
		{
			return false;
		}
		if (ParsedVersion.CreateFromString(gameVersion).IsOlder(ParsedVersion.Version0_75))
		{
			return false;
		}
		return true;
	}

	public Result TransitionGameVersion(out string infoText)
	{
		infoText = null;
		if (gameVersion == CurrentGameVersion)
		{
			return true;
		}
		if (string.IsNullOrEmpty(gameVersion))
		{
			Debug.LogWarning("Transitioning from 0.69 to 0.75!");
			if (Day >= 4)
			{
				AddFlag("church");
			}
			gameVersion = ParsedVersion.Version0_75.ToString();
		}
		ParsedVersion parsedVersion = ParsedVersion.CreateFromString(gameVersion);
		if (parsedVersion.MinorVersion <= 75)
		{
			SetVersion(ParsedVersion.Version0_80);
			Debug.LogWarning("Transitioning from 0.75 to 0.80!");
			itemManager.DeleteSlot("Irises");
			itemManager.DeleteSlot("HairSecondary");
		}
		if (parsedVersion.MinorVersion <= 85)
		{
			SetVersion(ParsedVersion.Version0_86_0);
			itemManager.DeleteSlot("Skirt");
		}
		if (parsedVersion.MinorVersion <= 86)
		{
			SetVersion(ParsedVersion.Version0_87_0);
			itemManager.DeleteSlot("UpperBody_Shirt");
			itemManager.DeleteSlot("Teeth_SharpTeeth");
		}
		if (parsedVersion.IsOlder(ParsedVersion.Version0_90_1))
		{
			eventManager.ConvertOldShittyEvents();
			SetVersion(ParsedVersion.Version0_90_1);
			infoText = LOC.UI.NewSaveRecomendedPrompt0_90;
		}
		if (parsedVersion.IsOlder(ParsedVersion.Version0_90_15))
		{
			SetVersion(ParsedVersion.Version0_90_15);
		}
		if (parsedVersion.IsOlder(ParsedVersion.Version0_96_1))
		{
			itemManager.DeleteSlot("EyeShadow");
			SetVersion(ParsedVersion.Version0_96_1);
		}
		ParsedVersion version0_97_ = ParsedVersion.Version0_97_12;
		if (parsedVersion.IsOlder(version0_97_))
		{
			SetVersion(version0_97_);
			return Result.Success;
		}
		if (parsedVersion.IsExactlyTheSame(version0_97_))
		{
			return Result.Success;
		}
		return Result.FailWithDescription("Couldn't transition from version \"" + gameVersion + "\" to current game version \"" + CurrentGameVersion + "\"!");
		void SetVersion(ParsedVersion newVersion)
		{
			string text = newVersion.ToString();
			Debug.Log("Transitioning save from \"" + gameVersion + "\" to \"" + text + "\"");
			parsedVersion = newVersion;
			gameVersion = text;
		}
	}

	public void SetPresetColors(IEnumerable<Color> colors)
	{
		presetColors.Clear();
		presetColors.AddRange(colors);
	}

	public IReadOnlyList<Color> GetPresetColors()
	{
		return presetColors;
	}

	public DialogueChainMemory GetDialogueChainMemory(string source)
	{
		if (dialogueChainData.TryGetValue(source, out var value))
		{
			return value;
		}
		value = new DialogueChainMemory();
		dialogueChainData.Add(source, value);
		return value;
	}

	public void OnAfterDeserializeDialogueChain()
	{
		foreach (StoryBotDialogueStage allStage in SingletonMonoBehaviour<StorySingleton>.Instance.AllStages)
		{
			if (!dialogueChainData.Remove(allStage.OldOriginalSourceLinear, out var value))
			{
				continue;
			}
			DialogueChainMemory dialogueChainMemory = GetDialogueChainMemory(allStage.OldOriginalSource);
			foreach (GameId ignoredGameId in value.IgnoredGameIds)
			{
				dialogueChainMemory.AddIgnored(ignoredGameId);
			}
		}
	}

	public void BeforeUnload()
	{
		itemManager.ToggleSpecialEffects(value: false);
	}

	public void AfterLoad()
	{
		UpdateRentStatus();
		itemManager.ToggleSpecialEffects(value: true);
	}

	public void GetRentData(out int daysTillRent, out bool paidToday)
	{
		int num = TimeToDay(GetFlagLastTimeOrDefault("LandlordEvent"));
		int num2 = (num / 7 + 1) * 7 + 1;
		int day = Day;
		daysTillRent = num2 - day;
		paidToday = num == day;
	}

	public void UpdateRentStatus()
	{
		GetRentData(out var daysTillRent, out var _);
		if (daysTillRent < 0)
		{
			statusText = LOC.UI.RentDue.TMPColorSurround(Color.red);
		}
		else if (daysTillRent <= 0)
		{
			statusText = LOC.UI.UI_RentToday;
		}
		else if (daysTillRent <= 5)
		{
			statusText = LOC.UI.UI_RentIn(LOC.UI.UI_PluralDays(daysTillRent));
		}
		else
		{
			statusText = "";
		}
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameStaticGui.UpdateRentStatusText(this);
	}

	private void DayPassedUpdateVariables()
	{
		search -= 0.04f;
		if (search < 0f)
		{
			search = 0f;
		}
		if (money > 50000)
		{
			AddFlag("richpeopleshopallowed");
		}
		UpdateRentStatus();
		fishingMinigameManager.UpdateDayPassed();
	}

	private void HourPassedUpdateVariables()
	{
		float num = UnityEngine.Random.Range(-0.5f, 0.5f);
		if (TimeSinceLastTalk > 2880)
		{
			num += UnityEngine.Random.Range(-1f, 0f);
		}
		else if (Mood < 0f)
		{
			num += UnityEngine.Random.Range(0f, 0.4f);
		}
		if (TimeSinceLastHeadpat > 2880)
		{
			num += UnityEngine.Random.Range(-1f, 0f);
		}
		else if (Mood < 0f)
		{
			num += UnityEngine.Random.Range(0f, 0.4f);
		}
		if (Mood > 0.2f)
		{
			num += UnityEngine.Random.Range(-1f, 0f);
		}
		Mood += num / 48f;
	}

	private void MinutePassedUpdateVariables(StaminaSyncType type)
	{
		GameStats gameStats = GetGameStats();
		if (lust > 200)
		{
			float num = MathF.Tanh((float)lust / 2000f) * 10f;
			Longing += gameStats.CommonGameStats.BotLongingGainBoost.CalculateBoosted(num / 5760f);
		}
		if (_currentHorniness > 0f)
		{
			float num2 = 1f / 60f;
			Longing += num2 / 10f;
			CurrentHorniness -= num2;
		}
		float value = 0f;
		if (RemainingCum < MaxCum)
		{
			value = MaxCum / 1800f;
		}
		else if (RemainingCum < MaxCum * 2f)
		{
			value = MaxCum / 10080f;
		}
		RemainingCum += gameStats.CommonGameStats.CumRegenerationBoost.CalculateBoosted(value);
		botLive2DCommonState.MinutePassed();
		GameStatPercentBoost sanityDrainBoost = gameStats.CommonGameStats.SanityDrainBoost;
		MentalHealthTemporary -= sanityDrainBoost.CalculateBoosted(0.00017361112f);
		MentalHealth -= sanityDrainBoost.CalculateBoosted(1.0064412E-05f);
		switch (type)
		{
		case StaminaSyncType.Normal:
			Stamina -= (1f + Health / 5f) / 960f;
			break;
		case StaminaSyncType.Recovery:
			Stamina += (1f - Health / 5f) / 480f;
			break;
		}
		Satiation -= 0.00069444446f;
		if (Mathf.Approximately(Satiation, 0f))
		{
			Health -= 0.00034722223f;
		}
		else
		{
			Health += 6.9444446E-05f;
		}
	}

	private void DayPassed()
	{
		DayPassedUpdateVariables();
		cockTwitchManager.EndOfDay();
		cocktractManager.EndOfDay();
	}

	private void HalfADayPassed()
	{
		if (Day >= 5)
		{
			Inteligence += 5;
		}
		else
		{
			Inteligence += 3;
		}
	}

	private void FourHoursPassed()
	{
		UniqueConversationsLeft++;
		itemManager.ShopManager.ShuffleShops();
	}

	private void HourPassed()
	{
		int hour = Hour;
		cocktractManager.HourlyUpdate();
		eventManager.SyncConditionalEvents();
		newsDataManager.HourPassed(hour);
		HourPassedUpdateVariables();
	}

	public void MinutePassed(StaminaSyncType type, bool suppressEvents)
	{
		if (time % 60 == 0)
		{
			if (time % 1440 == 0)
			{
				DayPassed();
			}
			if (time % 720 == 0)
			{
				HalfADayPassed();
			}
			if (time % 240 == 0)
			{
				FourHoursPassed();
			}
			HourPassed();
		}
		MinutePassedUpdateVariables(type);
		cockTwitchManager.MinutePassed();
		stockManager.MinutePassed(time);
		if (!suppressEvents)
		{
			suppressEvents = eventManager.SyncEvents();
		}
		if (!suppressEvents)
		{
			suppressEvents = deliveryManager.UpdateDeliveries();
		}
	}

	public void Eat(Item item)
	{
		EdibleItemPrefabData edibleData = item.GetItemPrefab().EdibleData;
		if (edibleData == null)
		{
			Debug.LogWarning("Tried to eat an item without EdibleData defined!");
			return;
		}
		EdibleItemNutritionProfile pooled;
		using (EdibleItemNutritionProfile.Pool.Acquire(out pooled))
		{
			edibleData.FillNutritionProfile(item, pooled);
			float value = pooled.Satiation.Value;
			if (Satiation + value > 1f && Satiation > 0.95f)
			{
				UiOverlay.Instance.OkPopup(LOC.UI.Equipment_Can_tEatTitle, LOC.UI.Equipment_Can_tEatPrompt);
				return;
			}
			pooled.Apply(this);
			itemManager.ConsumeItem(item, 1);
			switch (pooled.EatSound.Value)
			{
			case EatSound.Crunch:
				SingletonMonoBehaviour<SoundSingleton>.Instance.Play(R.Sound.Food_eat.Get());
				break;
			case EatSound.CrackOpenAColdOne:
				SingletonMonoBehaviour<SoundSingleton>.Instance.Play(R.Sound.Food_drink.Get());
				break;
			}
			if (!CheckFlag("diarrhea") && UnityEngine.Random.Range(0f, 1f) < 0.1f)
			{
				AddFlag("diarrhea");
			}
		}
	}

	public float ExtractCumFromBalls()
	{
		float num = UnityEngine.Random.Range(5.1f + 0.001f * RemainingCum, 6f + 0.01f * RemainingCum);
		if (num >= RemainingCum)
		{
			num = RemainingCum;
		}
		RemainingCum -= num;
		mlOfCumWasted += num;
		lust += Mathf.CeilToInt(num);
		Debug.Log(RemainingCum + "\\" + MaxCum + " " + RemainingCum / MaxCum + "%");
		Debug.Log("CUM: " + num + "ml");
		return num;
	}

	public float GetCumPercent()
	{
		return RemainingCum / MaxCum;
	}

	public string CumStatus()
	{
		float num = 6f + 0.01f * RemainingCum;
		if (MaxCum < num)
		{
			return LOC.UI.Cum_NoCum;
		}
		string text = " " + LOC.UI.Cum_Dynamic((int)(RemainingCum / num));
		if (RemainingCum > MaxCum)
		{
			return LOC.UI.Cum_Overfilled + text;
		}
		if (RemainingCum > MaxCum * 0.75f)
		{
			return LOC.UI.Cum_Full + text;
		}
		if (RemainingCum > MaxCum * 0.5f)
		{
			return LOC.UI.Cum_Half + text;
		}
		if (RemainingCum > MaxCum * 0.25f)
		{
			return LOC.UI.Cum_Recovering + text;
		}
		return LOC.UI.Cum_Empty + text;
	}

	public string SatiationStatus()
	{
		if (Satiation > 0.8f)
		{
			return LOC.UI.Satiation_Full;
		}
		if (Satiation > 0.5f)
		{
			return LOC.UI.Satiation_LittleHungry;
		}
		if (Satiation > 0.25f)
		{
			return LOC.UI.Satiation_Hungry;
		}
		return LOC.UI.Satiation_VeryHungry;
	}

	public string HealthStatus()
	{
		if (Health > 0.8f)
		{
			return LOC.UI.Health_VeryHealthy;
		}
		if (Health > 0.65f)
		{
			return LOC.UI.Health_Healthy;
		}
		if (Health > 0.5f)
		{
			return LOC.UI.Health_Tired;
		}
		if (Health > 0.3f)
		{
			return LOC.UI.Health_Awful;
		}
		if (Health > 0.25f)
		{
			return LOC.UI.Health_Terrible;
		}
		return LOC.UI.Health_NearDeath;
	}

	public string AnonShowerStatus()
	{
		int daysSinceLastAnonsShower = DaysSinceLastAnonsShower;
		if (daysSinceLastAnonsShower <= 0)
		{
			return LOC.UI.UI_ShoweredToday;
		}
		if (daysSinceLastAnonsShower == 1)
		{
			return LOC.UI.UI_ShoweredYesterday;
		}
		return LOC.UI.UI_YouHaventShoweredForXDays(daysSinceLastAnonsShower);
	}

	public string BotShowerStatus()
	{
		if (!botLive2DCommonState.IsThereCumOnHer)
		{
			return LOC.UI.Cleanliness_Clean;
		}
		if (botLive2DCommonState.CumOutsideSum > 6f)
		{
			return LOC.UI.Cleanliness_ALotOfCum;
		}
		return LOC.UI.Cleanliness_SomeCum;
	}

	public string FullStatusText()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(CumStatus());
			stringBuilder.AppendLine(HealthStatus());
			if (BathroomDoorInstalled)
			{
				stringBuilder.AppendLine(AnonShowerStatus());
				stringBuilder.AppendLine(BotShowerStatus());
			}
			return stringBuilder.ToString();
		}
	}

	[UsedInLua]
	public void AnonShowerEffect()
	{
		lastAnonsShowerAt = time;
		Current.AddFlag("ShowerAnon");
	}

	[UsedInLua]
	public void BotShowerEffect()
	{
		leastBotCleanAt = time;
		CleanCumOutside();
		Current.AddFlag("ShowerBot");
	}

	[UsedInLua]
	public void BotCleanWithRagEffect()
	{
		leastBotCleanAt = time;
		CleanCumOutside();
	}

	public void CleanCumOutside()
	{
		Live2DController live2DController = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.AllActiveControllers.FirstOrDefault();
		if (live2DController != null)
		{
			live2DController.SaveSerializedState();
		}
		botLive2DCommonState.RemoveCumOutside();
		if (live2DController != null)
		{
			live2DController.LoadSerializedState();
		}
	}

	public void ManifestHorninessAndResetFrickData()
	{
		ManifestHorninessFromLonging();
		Live2DController live2DController = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.AllActiveControllers.FirstOrDefault();
		if (live2DController != null)
		{
			live2DController.SaveSerializedState();
		}
		botLive2DCommonState.ResetFrickData();
		if (live2DController != null)
		{
			live2DController.LoadSerializedState();
		}
	}

	public void EndSex()
	{
		lastFuckedAt = time;
	}

	public void ClearMood()
	{
		_mood = 0f;
	}

	public void MarkAsCheater()
	{
		AddFlag("c");
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.AddGlobalFlag("c");
		SingletonLoadableMonoBehaviour<AchievementSingleton>.Instance.AchievementHandler.AddAchievement("saveEdited");
	}

	private void CumCleaningStats(out float cleanedRatio, out int sum)
	{
		int afterSexCleanedCount = AfterSexCleanedCount;
		int afterSexLeftCount = AfterSexLeftCount;
		sum = afterSexCleanedCount + afterSexLeftCount;
		cleanedRatio = (float)afterSexCleanedCount / (float)sum;
	}

	public void OnBeforeSerialize()
	{
		OnBeforeSerializeEmail();
	}

	public void OnAfterDeserialize()
	{
		FillAllSubClasses();
		_flagsDictionary = _flagsDictionary.ClearOrCreate();
		foreach (Flag flag in flags)
		{
			flag.EnsureValid();
			_flagsDictionary.Add(flag.Name, flag);
		}
		OnAfterDeserializeEmail();
		OnAfterDeserializeDialogueChain();
		cocktractManager.FillPrefabData();
		stockManager.FillPrefabData();
		itemManager.ShopManager.FillPrefabData();
	}

	public int GetFlagLastTimeOrDefault(string flag, int defaultValue = 0)
	{
		if (TryGetFlag(flag, out var flag2))
		{
			return flag2.LastTimeAdded;
		}
		return defaultValue;
	}

	public bool TryGetFlag(string flagString, out Flag flag)
	{
		return _flagsDictionary.TryGetValue(flagString, out flag);
	}

	public void AddFlag(string flag)
	{
		if (TryGetFlag(flag, out var flag2))
		{
			flag2.Report(time);
			return;
		}
		flag2 = new Flag(flag, time);
		flags.Add(flag2);
		_flagsDictionary.Add(flag, flag2);
	}

	public void RemoveFlag(string flag)
	{
		_flagsDictionary.Remove(flag);
		if (flags.RemoveAll((Flag t) => t.Name == flag) == 0)
		{
			Debug.LogWarning(flag + " flag doesn't exist, but tried to remove it!");
		}
	}

	public bool CheckFlag(string flag)
	{
		if (flag == null)
		{
			return false;
		}
		Flag flag2;
		return TryGetFlag(flag, out flag2);
	}

	public int GetFlagSetCount(string flag)
	{
		if (!TryGetFlag(flag, out var flag2))
		{
			return 0;
		}
		return flag2.TimesSet;
	}

	public bool CheckFlagBeforeRelative(string flag, int t)
	{
		return CheckFlagWithTime(flag, -t, TimeCheckType.BeforeRelative);
	}

	public bool CheckFlagAfterRelative(string flag, int t)
	{
		return CheckFlagWithTime(flag, -t, TimeCheckType.AfterRelative);
	}

	public bool CheckFlagWithTime(string flag, int timePeriod, TimeCheckType timeCheck)
	{
		return CheckFlagSpecialWithTime(flag, CheckType.Full, timePeriod, timeCheck);
	}

	public bool CheckFlagSpecial(string flag, CheckType type)
	{
		return type switch
		{
			CheckType.Start => flags.Any((Flag f) => f.Name.StartsWith(flag)), 
			CheckType.End => flags.Any((Flag f) => f.Name.EndsWith(flag)), 
			_ => CheckFlag(flag), 
		};
	}

	private bool CheckFlagTime(Flag f, int timePeriod, TimeCheckType timeCheck)
	{
		return timeCheck switch
		{
			TimeCheckType.Before => f.LastTimeAdded < timePeriod, 
			TimeCheckType.After => f.LastTimeAdded > timePeriod, 
			TimeCheckType.BeforeRelative => f.LastTimeAdded < time + timePeriod, 
			TimeCheckType.AfterRelative => f.LastTimeAdded > time + timePeriod, 
			_ => true, 
		};
	}

	public bool CheckFlagSpecialWithTime(string flag, CheckType type, int timePeriod, TimeCheckType timeCheck)
	{
		switch (type)
		{
		default:
		{
			if (TryGetFlag(flag, out var flag2))
			{
				return CheckFlagTime(flag2, timePeriod, timeCheck);
			}
			return false;
		}
		case CheckType.Start:
			foreach (Flag flag3 in flags)
			{
				if (CheckFlagTime(flag3, timePeriod, timeCheck) && flag3.Name.StartsWith(flag))
				{
					return true;
				}
			}
			return false;
		case CheckType.End:
			foreach (Flag flag4 in flags)
			{
				if (CheckFlagTime(flag4, timePeriod, timeCheck) && flag4.Name.EndsWith(flag))
				{
					return true;
				}
			}
			return false;
		}
	}

	public static string TimeCheckTypeString(TimeCheckType timeCheckType, string value)
	{
		return timeCheckType switch
		{
			TimeCheckType.Before => "before " + value, 
			TimeCheckType.After => "after " + value, 
			TimeCheckType.BeforeRelative => "before currenttime+" + value, 
			TimeCheckType.AfterRelative => "after currenttime+" + value, 
			_ => "ever", 
		};
	}

	public void ResetChurchStoryline()
	{
		priestBotPoints = 0;
		nunPoints = 0;
		timesWentToChurch = 0;
		lastWentToChurchAt = 0;
		foreach (Flag item in flags.ToList())
		{
			if (item.Name.StartsWith("church") || item.Name.StartsWith("nunInvestigation"))
			{
				RemoveFlag(item.Name);
			}
		}
		RemoveFlag("Priestbot_Mark_1");
		RemoveFlag("church-Big_Daddy_Knows");
		AddFlag("church");
		StoryBotDialogueStage stage = SingletonMonoBehaviour<StorySingleton>.Instance.Stage3;
		stage.GetMemory(this).RemoveIgnored(GameId.CreateVanilla(15700));
		stage.GetMemory(this).RemoveIgnored(BotJealousOfNunDialogueId);
	}

	[UsedInLua]
	public bool TryTakeMoney(int amount)
	{
		if (money >= amount)
		{
			money -= amount;
			return true;
		}
		return false;
	}

	[UsedInLua]
	public bool ForceTakeMoney(int amount)
	{
		if (money >= amount)
		{
			money -= amount;
			return true;
		}
		money = 0;
		return false;
	}

	public void AddMoney(int amount)
	{
		if (amount > 0)
		{
			if (money > 2000000000 - amount)
			{
				money = 2000000000;
			}
			else
			{
				money += amount;
			}
		}
	}

	public void GetADollar()
	{
		money++;
	}

	private void ManifestHorninessFromLonging()
	{
		_currentHorniness = Mathf.Max(_longing / 10f, _currentHorniness);
		if (_currentHorniness > 0.3f)
		{
			_mood = Mathf.Max(0f, _mood);
		}
	}

	public void SwallowCum(float ml)
	{
		CumInsideStomach += ml;
	}

	public void SetPlayerName(string name)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.SetStringVariable("playerName", name);
		playerName = name;
	}

	public void SetBotName(string name)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.SetStringVariable("botName", name);
		botName = name;
	}

	[UsedInLua]
	public void ReportSleepEnd(bool withBot)
	{
		lastSleptWithBot = withBot;
		lastWokeUpAt = time;
	}

	public static DateTime RawGameTimeToGameDateTime(int time)
	{
		int year = DateTime.Now.Year + 1;
		int month = 9;
		int day = 1;
		DateTime dateTime = new DateTime(year, month, day, new GregorianCalendar());
		int dayOfWeek = (int)DateTime.Now.DayOfWeek;
		int num = ((dayOfWeek == 0) ? 6 : (dayOfWeek - 1));
		int num2 = -num;
		if (num2 < 0)
		{
			num2 += 7;
		}
		if (num2 > 0)
		{
			dateTime = dateTime.AddDays(num2);
		}
		int num3 = time / 1440;
		dateTime = dateTime.AddDays(num3);
		int hours = time / 60 % 24;
		int minutes = time % 60;
		return dateTime.ChangeTime(hours, minutes);
	}

	public static int TimeToDayOfTheWeek(int time)
	{
		return DayToDayOfTheWeek(time);
	}

	public static int DayToDayOfTheWeek(int day)
	{
		return (day - 1) % 7;
	}

	public static int TimeToDay(int time)
	{
		return time / 1440;
	}

	public static int TimeFromDate(int day, int hour, int minute)
	{
		return day * 1440 + hour * 60 + minute;
	}

	public static int TimeInDay(int hour, int minute)
	{
		return hour * 60 + minute;
	}

	public static string TimeToHourString(int time)
	{
		int num = time / 60 % 24;
		int num2 = time % 60;
		return $"{num:00}:{num2:00}";
	}

	public static string TimeToDateString(int time)
	{
		int num = time / 1440;
		int num2 = time / 60 % 24;
		int num3 = time % 60;
		return $"{LOC.UI.UI_Day(num)} {num2:00}:{num3:00}";
	}

	public static int LastMultiple(int time, int number)
	{
		return time / number * number;
	}

	public static bool HappenedRecently(int occuredTime, int currentTime, int recentTimeMinutes = 120)
	{
		return occuredTime + recentTimeMinutes >= currentTime;
	}

	public static int TimeToHour(int time)
	{
		return time / 60 % 24;
	}
}
