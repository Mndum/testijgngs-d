using System;
using System.IO;
using System.Linq;
using System.Reflection;
using IC.AutomaticReference;
using JetBrains.Annotations;
using UnityEngine;

public class Live2DPrefabHolder : MonoBehaviour
{
	public interface ILive2DControllerUsage : IDisposable
	{
		Live2DController Controller { get; }

		Live2DPrefabHolder PrefabHolder { get; }

		bool IsValid { get; }

		ILive2DModelUser User { get; }

		void StopInner();
	}

	public sealed class Live2DControllerUsage<T> : ILive2DControllerUsage, IDisposable where T : Live2DController
	{
		public readonly Live2DPrefabHolder PrefabHolder;

		public readonly ILive2DModelUser User;

		public readonly T Controller;

		public bool IsValid { get; private set; }

		ILive2DModelUser ILive2DControllerUsage.User => User;

		Live2DController ILive2DControllerUsage.Controller => Controller;

		Live2DPrefabHolder ILive2DControllerUsage.PrefabHolder => PrefabHolder;

		public Live2DControllerUsage(Live2DPrefabHolder prefabHolder, ILive2DModelUser user, T controller)
		{
			PrefabHolder = prefabHolder;
			User = user;
			Controller = controller;
			IsValid = true;
		}

		public override string ToString()
		{
			if (!IsValid)
			{
				return typeof(T).Name + " Usage [INVALID/Disposed]";
			}
			string text = User?.ToString() ?? "null";
			string text2 = Controller?.ToString() ?? "null";
			string text3 = PrefabHolder?.name ?? "null";
			return typeof(T).Name + " Usage [Valid] | User: " + text + " | Controller: " + text2 + " | Prefab: " + text3;
		}

		public void StopInner()
		{
			IsValid = false;
			User.PrefabUsageStopped(this);
			Controller.EndUsage(User);
		}

		public void Dispose()
		{
			if (IsValid)
			{
				PrefabHolder.StopUsing(this);
			}
			else
			{
				Debug.LogWarning("Trying to dispose an invalid Live2DControllerUsage");
			}
		}
	}

	public delegate void CustomResetFunctionDelegate<T>(T live2DController) where T : Live2DController;

	private class Live2DModelCodeUser : ILive2DModelUser
	{
		public readonly string Name;

		public Live2DModelUsageTypeEnum Live2DModelUsage => Live2DModelUsageTypeEnum.CodeOnly;

		public Live2DModelCodeUser(string name)
		{
			Name = name;
		}

		public void PrefabUsageStopped(ILive2DControllerUsage usage)
		{
		}

		public override string ToString()
		{
			return "CodeUsage(" + Name + ")";
		}
	}

	public const string BotCharacterId = "bot";

	[SerializeField]
	private string characterId;

	public string Path;

	public SerializableAutomaticReference<GameObject> SourcePrefab;

	public string ControllerTypeName;

	[NonSerialized]
	private Live2DController instantiatedPrefab;

	[CanBeNull]
	private ILive2DControllerUsage _currentHolderUsage;

	private DateTime _lastUsageTime;

	public string CharacterId => characterId;

	public string PathDirectory => System.IO.Path.GetDirectoryName(Path);

	public Type ControllerType
	{
		get
		{
			return (from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly assembly) => assembly.GetTypes())
				where type.IsSubclassOf(typeof(Live2DController))
				select type).FirstOrDefault((Type t) => t.Name == ControllerTypeName);
		}
		set
		{
			ControllerTypeName = value.Name;
		}
	}

	public bool IsInstantiated => instantiatedPrefab != null;

	[CanBeNull]
	public ILive2DModelUser CurrentlyUsedBy => _currentHolderUsage?.User;

	[CanBeNull]
	public ILive2DControllerUsage CurrentUsage => _currentHolderUsage;

	private Live2DController GetOrCreateInstantiatedController()
	{
		if (instantiatedPrefab == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(SourcePrefab.Get(), transform);
			instantiatedPrefab = gameObject.GetComponent<Live2DController>();
			instantiatedPrefab.InitializeAll();
		}
		return instantiatedPrefab;
	}

	public bool TryGetInstantiatedController(out Live2DController controller)
	{
		controller = instantiatedPrefab;
		return controller != null;
	}

	public ILive2DControllerUsage StartUsingBy(ILive2DModelUser modelUser)
	{
		return StartUsingBy<Live2DController>(modelUser, null);
	}

	private Live2DController InitController()
	{
		Live2DController orCreateInstantiatedController = GetOrCreateInstantiatedController();
		orCreateInstantiatedController.transform.localPosition = Vector3.zero;
		orCreateInstantiatedController.transform.localScale = Vector3.one;
		orCreateInstantiatedController.gameObject.SetActive(value: true);
		orCreateInstantiatedController.SetEnabled(v: true);
		return orCreateInstantiatedController;
	}

	public Live2DControllerUsage<T> StartUsingByCode<T>(string usageName, CustomResetFunctionDelegate<T> customResetAction) where T : Live2DController
	{
		Live2DModelCodeUser modelUser = new Live2DModelCodeUser(usageName);
		return StartUsingBy(modelUser, customResetAction);
	}

	public Live2DControllerUsage<T> StartUsingBy<T>(ILive2DModelUser modelUser, CustomResetFunctionDelegate<T> customResetAction) where T : Live2DController
	{
		StopUsingCurrent();
		Live2DControllerUsage<T> live2DControllerUsage = (Live2DControllerUsage<T>)(_currentHolderUsage = new Live2DControllerUsage<T>(this, modelUser, (T)InitController()));
		if (customResetAction == null)
		{
			live2DControllerUsage.Controller.ResetEverything();
		}
		else
		{
			T controller = live2DControllerUsage.Controller;
			if ((object)controller != null)
			{
				customResetAction(controller);
			}
			else
			{
				Debug.LogError($"Cannot cast {live2DControllerUsage.Controller} to {typeof(T).Name}!");
			}
		}
		live2DControllerUsage.Controller.StartUsage(characterId, live2DControllerUsage.User);
		return live2DControllerUsage;
	}

	private void StopUsing(ILive2DControllerUsage usage)
	{
		if (usage == _currentHolderUsage)
		{
			StopUsingCurrent();
		}
	}

	public void StopUsingCurrent()
	{
		if (_currentHolderUsage != null)
		{
			_currentHolderUsage.StopInner();
			GetOrCreateInstantiatedController().transform.SetParent(transform, worldPositionStays: false);
			_currentHolderUsage = null;
			_lastUsageTime = DateTime.Now;
		}
	}

	public bool TryGetUnusedTime(out TimeSpan unusedTime)
	{
		unusedTime = DateTime.Now - _lastUsageTime;
		if (IsInstantiated)
		{
			return _currentHolderUsage == null;
		}
		return false;
	}

	public void DestroyIfUnused()
	{
		if (_currentHolderUsage == null)
		{
			instantiatedPrefab.BeforeDestroy();
			instantiatedPrefab.SmartDestroyMainGameObjectIfNotNull();
			instantiatedPrefab = null;
		}
	}
}
