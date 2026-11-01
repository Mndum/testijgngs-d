using System;
using IC.AutomaticReference;
using Melanchall.DryWetMidi.Core;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
internal class ModUtilities : SingletonMonoBehaviour<ModUtilities>
{
	private void CheckIfInInitialization()
	{
		if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.IsLoaded)
		{
			Debug.LogError("Tried to use a method that you can only use during initialization! That's wrong! You shouldn't do it!");
		}
	}

	[Preserve]
	public ModCustomStringReference CreateLocalizableString(Guid modGuid, string key, string text, bool smartString = false)
	{
		CheckIfInInitialization();
		Mod modByGuid = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetModByGuid(modGuid);
		if (modByGuid == null)
		{
			Debug.LogError($"Mod with guid {modGuid} not found!");
			return null;
		}
		Debug.Log($"Created localizable string:\n\"{key}\" = \"{text}\" (smartString:{smartString})");
		return modByGuid.CreateCustomLocalizedString(key, text, smartString);
	}

	[Preserve]
	public ModCustomStringReference GetLocalizableString(Guid modGuid, string key)
	{
		CheckIfInInitialization();
		Mod modByGuid = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetModByGuid(modGuid);
		if (modByGuid == null)
		{
			Debug.LogError($"Mod with guid {modGuid} not found!");
			return null;
		}
		if (!modByGuid.TryGetCustomLocalizedString(key, out var customString))
		{
			Debug.LogError($"Variable \"{key}\" not found in mod {modGuid}!");
			return null;
		}
		return customString;
	}

	[Preserve]
	public string GetLocalizableStringRaw(Guid modGuid, string key)
	{
		Mod modByGuid = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetModByGuid(modGuid);
		if (modByGuid == null)
		{
			Debug.LogError($"Mod with guid {modGuid} not found!");
			return "ERROR";
		}
		return ModdableTable.GetCustomString(modByGuid, key);
	}

	[Preserve]
	public ItemPrefab CreateItemPrefab()
	{
		CheckIfInInitialization();
		return new ItemPrefab("No name", "No description", null, 1);
	}

	[Preserve]
	public ItemPrefab CreateItemPrefab(string name, string description, int price)
	{
		CheckIfInInitialization();
		return new ItemPrefab(name, description, null, price);
	}

	[Preserve]
	public GameId CreateNewItemAutoAssignId(Guid modGuid, ItemPrefab itemPrefab)
	{
		CheckIfInInitialization();
		return ItemPrefab.ItemPrefabManager.AddModItem(modGuid, itemPrefab);
	}

	[Preserve]
	public GameId CreateNewItem(Guid modGuid, ItemPrefab itemPrefab)
	{
		return CreateNewItemAutoAssignId(modGuid, itemPrefab);
	}

	[Preserve]
	public GameId CreateNewItem(int id, Guid modGuid, ItemPrefab itemPrefab)
	{
		CheckIfInInitialization();
		return ItemPrefab.ItemPrefabManager.AddModItem(modGuid, itemPrefab, id);
	}

	[Preserve]
	public void AddGenericItemToShop(string shopId, GameId gameId, Func<bool> availabilityFunc = null, float occurenceRate = 1f)
	{
		CheckIfInInitialization();
		if (ShopPrefabManager.TryGetShopPrefab(shopId, out var shopPrefab))
		{
			shopPrefab.GenericContainer.AddItems(availabilityFunc, occurenceRate, gameId);
		}
		else
		{
			Debug.LogWarning("No shop " + shopId + " found!");
		}
	}

	[Preserve]
	public void AddSingleBuyItemToShop(string shopId, GameId gameId, Func<bool> availabilityFunc = null, float occurenceRate = 1f)
	{
		CheckIfInInitialization();
		if (ShopPrefabManager.TryGetShopPrefab(shopId, out var shopPrefab))
		{
			shopPrefab.SingleContainer.AddItems(availabilityFunc, occurenceRate, gameId);
		}
		else
		{
			Debug.LogWarning("No shop " + shopId + " found!");
		}
	}

	[Preserve]
	public ModFileBase GetModFile(Guid modGuid, string path)
	{
		ModFileBase modFileBase = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetModByGuid(modGuid).FindFile(path);
		if (modFileBase == null)
		{
			Debug.LogError($"Mod {modGuid} doesn't have a file \"{path}\"! Returning null.");
		}
		return modFileBase;
	}

	[Preserve]
	public PackedTexturesContainer GetPackedTexture(Guid modGuid, string formatPath)
	{
		Mod modByGuid = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetModByGuid(modGuid);
		return PackedTexturesContainer.LoadFromModFiles(formatPath, modByGuid.FilesIncluded);
	}

	[Preserve]
	public Texture2D GetTexture(Guid modGuid, string path)
	{
		return GetTextureReference(modGuid, path).Get();
	}

	[Preserve]
	public Texture2DModFileAutomaticReference GetTextureReference(Guid modGuid, string path)
	{
		return new Texture2DModFileAutomaticReference(GetModFile(modGuid, path));
	}

	[Preserve]
	public IReference<MidiFile> GetMidiReference(Guid modGuid, string path)
	{
		return new ModFileNonUnityAutomaticReference<MidiFile>(GetModFile(modGuid, path), (ModFileBase modFile) => MidiFile.Read(modFile.OpenReadStream()));
	}

	[Preserve]
	public IReference<IMusicWithInfo> GetAsMusicWithInfo(Guid modGuid, string path, string songName = "Unknown", string songAuthor = "Unknown")
	{
		return new ModFileNonUnityAutomaticReference<IMusicWithInfo>(GetModFile(modGuid, path), (ModFileBase modFile) => new ModMusicWithInfo(modFile, songName, songAuthor));
	}

	[Preserve]
	public IReference<AudioClip> GetAsAudioClipReference(Guid modGuid, string path)
	{
		return new ModFileNonUnityAutomaticReference<AudioClip>(GetModFile(modGuid, path), (ModFileBase modFile) => modFile.LoadAsAudioClip());
	}

	public void AddSong(CookingSong cookingSong)
	{
		CookingSongPrefabManager.AddModdedSong(cookingSong);
	}

	[Preserve]
	public Sprite GetSprite(Guid modGuid, string path, float ppu = 100f)
	{
		return GetSpriteReference(modGuid, path, ppu).GetResource();
	}

	[Preserve]
	public IReference<Sprite> GetSpriteReference(Guid modGuid, string path, float ppu = 100f)
	{
		return new Texture2DModFileAutomaticReference(GetModFile(modGuid, path)).Convert((Texture2D tex) => Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), ppu));
	}

	[Preserve]
	public IReference<Sprite> GetAsSprite(Guid modGuid, string path, float ppu = 100f)
	{
		return GetSpriteReference(modGuid, path, ppu);
	}

	[Preserve]
	public Guid GetNewGuid()
	{
		return Guid.NewGuid();
	}

	public StorySingleton.LuaPersonalityModuleDefinition PrepareNewPersonalityDefinition()
	{
		return new StorySingleton.LuaPersonalityModuleDefinition();
	}

	[UsedInLua]
	public ManagedCoroutineWithTarget StartCoroutine(Closure closure, Action onFinished = null)
	{
		ManagedCoroutineWithTarget managedCoroutineWithTarget = new ManagedCoroutineWithTarget();
		managedCoroutineWithTarget.SetTarget(this);
		managedCoroutineWithTarget.Start(ModsSingleton.LuaEnvironment.RunLuaCoroutine(closure), onFinished);
		return managedCoroutineWithTarget;
	}

	[UsedInLua]
	public WaitForSeconds WaitForSeconds(float seconds)
	{
		return SingletonMonoBehaviour<GameUtilities>.Instance.WaitForSecondsYield(seconds);
	}

	[UsedInLua]
	public void StopCoroutine(ManagedCoroutineWithTarget coroutine)
	{
		coroutine?.Stop();
	}

	[Preserve]
	public GameVariables GetGameVariables()
	{
		if (!GameScript.HasGameStarted)
		{
			throw new Exception("Game hasn't started yet! You can't use GetGameVariables during initialization. It contains game data that exist after game starts/loads.");
		}
		return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
	}

	public ControllerEnum GetMissionaryController()
	{
		return ControllerEnum.Missionary;
	}

	public ControllerEnum GetDoggyController()
	{
		return ControllerEnum.Doggy;
	}

	public ControllerEnum GetInteractController()
	{
		return ControllerEnum.Interaction;
	}

	public ControllerEnum GetController(string id)
	{
		return id.GetControllerEnumFromPositionId();
	}
}
