using System;
using IC.AutomaticReference;
using UnityEngine;

public class CookingSong : IKey<GameId>
{
	private WeakReference<CookingSongRuntimeData> _cookingSongRuntimeData;

	public readonly GameId Key;

	public readonly IReference<IMusicWithInfo> MusicReference;

	public readonly Func<CookingSong, CookingSongRuntimeData> RuntimeDataGenerator;

	public readonly Recipe Recipe;

	public readonly Func<CookingMinigameManager, bool> IsUnlockedFunc;

	public readonly bool HideIfLocked;

	public CookingSongRuntimeData RuntimeData
	{
		get
		{
			CookingSongRuntimeData target;
			if (_cookingSongRuntimeData != null)
			{
				if (_cookingSongRuntimeData.TryGetTarget(out target))
				{
					return target;
				}
				target = CalculateRuntimeData();
				_cookingSongRuntimeData.SetTarget(target);
				return target;
			}
			target = CalculateRuntimeData();
			_cookingSongRuntimeData = new WeakReference<CookingSongRuntimeData>(target);
			return target;
		}
	}

	public bool IsUnlocked => IsUnlockedFast(GameVariables.Current.cookingMinigameManager);

	GameId IKey<GameId>.Key => Key;

	private CookingSong(GameId key, IReference<IMusicWithInfo> musicReference, Func<CookingSong, CookingSongRuntimeData> runtimeDataGenerator, Recipe recipe, Func<CookingMinigameManager, bool> isUnlockedFunc = null, bool hideIfLocked = false)
	{
		Key = key;
		MusicReference = musicReference;
		RuntimeDataGenerator = runtimeDataGenerator;
		Recipe = recipe;
		HideIfLocked = hideIfLocked;
		IsUnlockedFunc = isUnlockedFunc;
	}

	public static CookingSong CreateInstance(GameId key, IReference<IMusicWithInfo> musicReference, Func<CookingSong, CookingSongRuntimeData> runtimeDataGenerator, Recipe recipe, Func<CookingMinigameManager, bool> isUnlockedFunc = null, bool hideIfLocked = false)
	{
		return new CookingSong(key, musicReference, runtimeDataGenerator, recipe, isUnlockedFunc, hideIfLocked);
	}

	public IMusicWithInfo GetMusic()
	{
		IMusicWithInfo musicWithInfo = MusicReference?.GetResource();
		if (musicWithInfo != null)
		{
			return musicWithInfo;
		}
		Debug.Log($"{Key}: Failed to load cooking song audio file! Falling back to default (Demise)!");
		return R.Music.BadEnd_Demise.Get();
	}

	private CookingSongRuntimeData CalculateRuntimeData()
	{
		try
		{
			return RuntimeDataGenerator(this);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			Debug.Log($"{Key}: Failed to generate cooking song runtime data! Above exception happened! Falling back to default (Demise)!");
			return R.Midi.Sheep_Demise.ToCookingSongRuntimeDataBuilder(this).ChannelToNotesAllTheSame(0, NoteButtonTypeEnum.Left).Build();
		}
	}

	public bool IsUnlockedFast(CookingMinigameManager cookingMinigameManager)
	{
		if (IsUnlockedFunc != null)
		{
			return IsUnlockedFunc(cookingMinigameManager);
		}
		return true;
	}

	public CookingSongSave GetSave()
	{
		return GameVariables.Current.cookingMinigameManager.GetSave(this);
	}
}
