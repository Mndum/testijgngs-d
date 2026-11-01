using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Live2DControllerSingleton : SingletonMonoBehaviour<Live2DControllerSingleton>
{
	public static readonly ControllerEnum[] AllValidControllersEnums = new ControllerEnum[6]
	{
		ControllerEnum.Missionary,
		ControllerEnum.Doggy,
		ControllerEnum.Blowjob,
		ControllerEnum.Cowgirl,
		ControllerEnum.Interaction,
		ControllerEnum.Shower
	};

	[SerializeField]
	private Live2DPrefabHolder _DoggyCharacterHolder;

	[SerializeField]
	private Live2DPrefabHolder _MissionaryCharacterHolder;

	[SerializeField]
	private Live2DPrefabHolder _InteractCharacterHolder;

	[SerializeField]
	private Live2DPrefabHolder _BlowjobCharacterHolder;

	[SerializeField]
	private Live2DPrefabHolder _CowgirlCharacterHolder;

	[SerializeField]
	private Live2DPrefabHolder _ShowerCharacterHolder;

	public const int Live2DTextureSize = 4096;

	public const int Live2DTextureWidth = 4096;

	public const int Live2DTextureHeight = 4096;

	public const int Live2DMipLevel = 2;

	private int _maxPoolCount = 4;

	private List<RenderTexture> _renderTexturePool = new List<RenderTexture>();

	[field: SerializeField]
	public Live2DPrefabHolder[] Live2DPrefabHolders { get; private set; }

	public Live2DPrefabHolder DoggyCharacterHolder => _DoggyCharacterHolder;

	public Live2DPrefabHolder MissionaryCharacterHolder => _MissionaryCharacterHolder;

	public Live2DPrefabHolder InteractCharacterHolder => _InteractCharacterHolder;

	public Live2DPrefabHolder BlowjobCharacterHolder => _BlowjobCharacterHolder;

	public Live2DPrefabHolder CowgirlCharacterHolder => _CowgirlCharacterHolder;

	public Live2DPrefabHolder ShowerCharacterHolder => _ShowerCharacterHolder;

	public IEnumerable<Live2DPrefabHolder> AllInstantiatedPrefabHolders => Live2DPrefabHolders.Where((Live2DPrefabHolder t) => t.IsInstantiated);

	public IEnumerable<Live2DController> AllInstantiatedControllers => AllInstantiatedPrefabHolders.SelectWhere((Live2DPrefabHolder x) => (x.TryGetInstantiatedController(out var controller), controller));

	public IEnumerable<Live2DController> AllActiveControllers => AllInstantiatedControllers.Where((Live2DController t) => t.isActiveAndEnabled);

	public static Live2DControllerSingleton LuaInstance => SingletonMonoBehaviour<Live2DControllerSingleton>.Instance;

	public static SceneEnumFlag ToFlags(ControllerEnum e)
	{
		return e switch
		{
			ControllerEnum.Missionary => SceneEnumFlag.Missionary, 
			ControllerEnum.Doggy => SceneEnumFlag.Doggy, 
			ControllerEnum.Blowjob => SceneEnumFlag.Blowjob, 
			ControllerEnum.Cowgirl => SceneEnumFlag.Cowgirl, 
			ControllerEnum.Interaction => SceneEnumFlag.Interaction, 
			ControllerEnum.Shower => SceneEnumFlag.Shower, 
			_ => SceneEnumFlag.NoScene, 
		};
	}

	public static SceneEnumFlag ToFlags(ControllerEnum[] e)
	{
		if (e.Length == 0)
		{
			return SceneEnumFlag.NoScene;
		}
		if (e.Length == 1)
		{
			return ToFlags(e[0]);
		}
		return e.Select((ControllerEnum t) => ToFlags(e)).Aggregate((SceneEnumFlag a, SceneEnumFlag b) => a | b);
	}

	public IEnumerable<Live2DController> AllInstantiatedControllersFor(string characterId)
	{
		return AllInstantiatedPrefabHolders.Where((Live2DPrefabHolder x) => x.CharacterId == characterId).SelectWhere((Live2DPrefabHolder x) => (x.TryGetInstantiatedController(out var controller), controller));
	}

	public IEnumerable<Live2DController> AllCurrentlyUsedControllersFor(string characterId)
	{
		return AllInstantiatedPrefabHolders.Where((Live2DPrefabHolder x) => x.CharacterId == characterId && x.CurrentlyUsedBy != null).SelectWhere((Live2DPrefabHolder x) => (x.TryGetInstantiatedController(out var controller), controller));
	}

	public bool IsAnyControllerActiveAndTalkingWithOverlay(string characterId)
	{
		Live2DPrefabHolder[] live2DPrefabHolders = Live2DPrefabHolders;
		foreach (Live2DPrefabHolder live2DPrefabHolder in live2DPrefabHolders)
		{
			if (live2DPrefabHolder.CharacterId == characterId && live2DPrefabHolder.TryGetInstantiatedController(out var controller) && controller.isActiveAndEnabled && controller.CurrentBrain != null && controller.CurrentBrain.IsTalkingWithOverlay)
			{
				return true;
			}
		}
		return false;
	}

	public Live2DController GetReadyController(string characterId)
	{
		return AllCurrentlyUsedControllersFor(characterId).FirstOrDefault();
	}

	public ModCreationLive2DControllersContainer GetModCreationData(string usageName)
	{
		return new ModCreationLive2DControllersContainer(Live2DPrefabHolders.Select((Live2DPrefabHolder x) => x.StartUsingByCode(usageName, (Live2DController c) =>
		{
			c.RestoreOriginalTextures();
		})).ToList());
	}

	private async UniTask MemoryManagementTask()
	{
		while (true)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(1.0), ignoreTimeScale: true);
			TimeSpan timeSpan = (OptionsStatic.LowMemoryModeEnabled ? TimeSpan.FromSeconds(1.0) : TimeSpan.FromSeconds(5.0));
			bool flag = false;
			for (int i = 0; i < Live2DPrefabHolders.Length; i++)
			{
				if (Live2DPrefabHolders[i].IsInstantiated && Live2DPrefabHolders[i].TryGetUnusedTime(out var unusedTime) && unusedTime >= timeSpan)
				{
					flag = true;
					Debug.Log($"{Live2DPrefabHolders[i]} was unused for {unusedTime.TotalSeconds:0.0} seconds! Unloading!");
					Live2DPrefabHolders[i].DestroyIfUnused();
				}
			}
			if (flag)
			{
				Debug.Log("Unloading unused assets started!");
				await Resources.UnloadUnusedAssets().ToUniTask();
				Debug.Log("Unloading unused assets done!");
			}
		}
	}

	public void OnValidate()
	{
		Live2DPrefabHolders = transform.GetComponentsInChildren<Live2DPrefabHolder>(includeInactive: true);
	}

	public void Start()
	{
		Live2DPrefabHolder[] live2DPrefabHolders = Live2DPrefabHolders;
		for (int i = 0; i < live2DPrefabHolders.Length; i++)
		{
			live2DPrefabHolders[i].gameObject.SetActive(value: false);
		}
		MemoryManagementTask().Forget();
	}

	public void UpdateAllTextures()
	{
		foreach (Live2DController allInstantiatedController in SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.AllInstantiatedControllers)
		{
			if (allInstantiatedController.isActiveAndEnabled)
			{
				allInstantiatedController.UpdateTexturesFromCurrentlyEquipped();
			}
		}
	}

	public void ResetAllTextures()
	{
		foreach (Live2DController allInstantiatedController in SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.AllInstantiatedControllers)
		{
			allInstantiatedController.RestoreOriginalTextures();
		}
	}

	public void ReturnToPoll(Texture texture)
	{
		if (texture is RenderTexture item)
		{
			if (_renderTexturePool.Count < _maxPoolCount)
			{
				_renderTexturePool.Add(item);
			}
		}
		else
		{
			UnityEngine.Object.Destroy(texture);
		}
	}

	public RenderTexture GetRenderTexture()
	{
		if (_renderTexturePool.Count > 0)
		{
			RenderTexture renderTexture = _renderTexturePool[_renderTexturePool.Count - 1];
			_renderTexturePool.RemoveAt(_renderTexturePool.Count - 1);
			if (!renderTexture.IsCreated())
			{
				renderTexture.Create();
			}
			return renderTexture;
		}
		return new RenderTexture(4096, 4096, 0, RenderTextureFormat.Default, 2)
		{
			useMipMap = true,
			autoGenerateMips = false
		};
	}
}
