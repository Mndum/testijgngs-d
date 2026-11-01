using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using IC.AutomaticReference;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class LoadingManager
{
	public abstract class LoadableReferenceBase
	{
		public string OptionalName;

		public Type[] Dependencies { get; private set; }

		public abstract Type SourceType { get; }

		public string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(OptionalName))
				{
					return OptionalName;
				}
				return SourceType.ToString();
			}
		}

		public LoadableReferenceBase(Type[] dependencies, string optionalName)
		{
			Dependencies = dependencies;
			OptionalName = optionalName;
		}

		public abstract UniTask Load(IProgressManager progressManager);
	}

	public class LoadableReferenceSingletonStaticMethod : LoadableReferenceBase
	{
		public override Type SourceType { get; }

		public Func<IProgressManager, UniTask> TaskCreator { get; }

		public LoadableReferenceSingletonStaticMethod(Type[] dependencies, string optionalName, Type sourceType, Func<IProgressManager, UniTask> taskCreator)
			: base(dependencies, optionalName)
		{
			SourceType = sourceType;
			TaskCreator = taskCreator;
		}

		public override UniTask Load(IProgressManager progressManager)
		{
			return TaskCreator(progressManager);
		}
	}

	public class LoadableReferenceSingleton<T> : LoadableReferenceBase where T : SingletonLoadableMonoBehaviour<T>
	{
		public T Value { get; private set; }

		public override Type SourceType => typeof(T);

		public LoadableReferenceSingleton(Type[] dependencies, string optionalName, T value)
			: base(dependencies, optionalName)
		{
			Value = value;
		}

		public override UniTask Load(IProgressManager progressManager)
		{
			return Value.Load(progressManager);
		}
	}

	private class LoadNode : IFrameDisplayProgressManager, IProgressManager, IProgress<float>
	{
		public LoadableReferenceBase Loadable;

		public List<LoadNode> DependenciesRemaining = new List<LoadNode>();

		private FrameDisplayProgressManager _progressManagerImplementation = new FrameDisplayProgressManager();

		public long MillisecondsSinceLastUpdate => _progressManagerImplementation.MillisecondsSinceLastUpdate;

		public void ReportProgress(string title)
		{
			_progressManagerImplementation.ReportProgress(title);
		}

		public void ReportProgress(float percent)
		{
			_progressManagerImplementation.ReportProgress(percent);
		}

		public void ReportProgress(float percent, string title)
		{
			_progressManagerImplementation.ReportProgress(percent, title);
		}

		public void UseProgressValues(out float progress, out string title)
		{
			_progressManagerImplementation.UseProgressValues(out progress, out title);
		}
	}

	private static List<LoadableReferenceBase> _loadables = new List<LoadableReferenceBase>();

	public static bool LocalizationLoadingSuccessful = false;

	public static void RegisterToLoad(LoadableReferenceBase loadableReferenceBase)
	{
		_loadables.Add(loadableReferenceBase);
	}

	private static void LogLoadingInfo(string text)
	{
		if (DebugDefines.LogLoadingInfo)
		{
			Debug.Log(text);
		}
	}

	public static async UniTask LoadAll(IProgressManager progressManager)
	{
		UniTask<bool> task = LoadAllInner(progressManager);
		while (!task.Status.IsCompleted())
		{
			await UniTask.NextFrame();
		}
		if (!(await task))
		{
			Debug.LogError("Loading failed!");
			await UniTask.Never(CancellationToken.None);
		}
	}

	private static async UniTask<bool> LoadAllInner(IProgressManager progressManager)
	{
		Application.runInBackground = true;
		Type[] types = typeof(LoadingManager).Assembly.GetTypes();
		foreach (Type type in types)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			foreach (MethodInfo methodInfo in methods)
			{
				if (!methodInfo.IsStatic || !(methodInfo.ReturnType == typeof(UniTask)) || !(methodInfo.GetCustomAttributes(typeof(LoadableStaticMethod), inherit: false).FirstOrDefault() is LoadableStaticMethod loadableStaticMethod))
				{
					continue;
				}
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length == 0)
				{
					Func<UniTask> x = (Func<UniTask>)methodInfo.CreateDelegate(typeof(Func<UniTask>));
					_loadables.Add(new LoadableReferenceSingletonStaticMethod(loadableStaticMethod.Dependencies, null, type, (IProgressManager _) => x()));
				}
				else if (parameters.Length == 1 && parameters[0].ParameterType == typeof(IProgressManager))
				{
					Func<IProgressManager, UniTask> taskCreator = (Func<IProgressManager, UniTask>)methodInfo.CreateDelegate(typeof(Func<IProgressManager, UniTask>));
					_loadables.Add(new LoadableReferenceSingletonStaticMethod(loadableStaticMethod.Dependencies, null, type, taskCreator));
				}
				else
				{
					Debug.LogError($"UNEXPECTED PARAMETER IN {methodInfo.Name} in {type}!!!");
				}
			}
		}
		float localizationEnd = 0.4f;
		try
		{
			progressManager.ReportProgress("Loading localization");
			LogLoadingInfo("Loading localization!");
			await OptionsStatic.InnerLocalizationEnumerator(progressManager.PrepareSubTask(0f, localizationEnd));
			LOS.TupleReference mENU_IO_LOADING = LOC.Polyglot.TupleReferences.MENU_IO_LOADING;
			AsyncOperationHandle<string> stringTask = LocalizationSettings.Instance.GetStringDatabase().GetLocalizedStringAsync(mENU_IO_LOADING.GetTableReference(), mENU_IO_LOADING.GetTableEntryReference(), null, FallbackBehavior.UseProjectSettings);
			await stringTask;
			progressManager.ReportProgress(stringTask.Result);
			LocalizationLoadingSuccessful = true;
		}
		catch (Exception exception)
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			bool flag = currentDirectory.Contains("\u00a0");
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.AppendColorTagStart(Color.red);
				stringBuilder.AppendLine("Loading localization failed!");
				stringBuilder.AppendColorTagEnd();
				if (flag)
				{
					stringBuilder.AppendLine("Your game's path \"" + currentDirectory + "\" contains a non breaking space (ALT + 255)");
					stringBuilder.AppendLine("There's a bug in Unity that doesn't allow resources to load when NBSP is in game's path!");
				}
				else
				{
					stringBuilder.AppendLine("Following things can cause it");
					stringBuilder.AppendLine("- Game's path is too long");
					stringBuilder.AppendLine("- Game's path contains weird characters. For example: non breaking space (ALT + 255)");
				}
				stringBuilder.AppendLine();
				stringBuilder.AppendColorTagStart(Color.green);
				stringBuilder.AppendLine("Move the game somewhere else to fix this issue!");
				Debug.LogException(exception);
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, stringBuilder.ToString(), QuitHandler.ForceQuitGame);
				return false;
			}
		}
		await UniTask.Yield();
		if (SaveFileHelper.NoAccess(SaveFileHelper.SaveDirectory, out var exception2))
		{
			UniTaskCompletionSource t = new UniTaskCompletionSource();
			UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.NoFileAccessPrompt + "\n" + exception2, () =>
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_Error, LOC.UI.NoFileAccessPrompt2, () =>
				{
					t.TrySetResult();
				}, QuitHandler.ForceQuitGame);
			});
			await t.Task;
		}
		if (!OptionsStatic.LastLoadingSucceded)
		{
			UniTaskCompletionSource t2 = new UniTaskCompletionSource();
			Action action = () =>
			{
				t2.TrySetResult();
			};
			List<(Mod mod, SerializableLocalModInfo modInfo)> modsToDisable = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.AllModsWithModInfo.Where(((Mod mod, SerializableLocalModInfo modInfo) tuple) => tuple.modInfo.LoadOnStart).ToList();
			if (modsToDisable.Count > 0)
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_Error, LOC.UI.Loading_FailedProbablyBecauseOfModsPrompt + "\n" + LOC.UI.Loading_IgnoreIfClosedGame, () =>
				{
					foreach (var item in modsToDisable)
					{
						item.modInfo.LoadOnStart = false;
					}
					SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.SaveLocalModInfo();
					action();
				}, action);
			}
			else
			{
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.Loading_FailedNoIdeaWhy + "\n" + LOC.UI.Loading_IgnoreIfClosedGame, action);
			}
			await t2.Task;
			await UniTask.Yield();
		}
		SaveFileHelper.OnSavingError = (Exception ex) =>
		{
			UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.Error_GameCannotSave + $"\nException:{ex}!");
		};
		OptionsStatic.LastLoadingSucceded.SetValue(value: false);
		SingletonMonoBehaviour<FpsManager>.Instance.StartLoading();
		progressManager.ReportProgress(LOC.Polyglot.MENU_IO_LOADING);
		await UniTask.Yield();
		await LoadLoadables(progressManager.PrepareSubTask(localizationEnd, 0.9f));
		SingletonMonoBehaviour<FpsManager>.Instance.EndLoading();
		SingletonMonoBehaviour<AutomaticReferenceSingleton>.Instance.ForceCleaning();
		progressManager.ReportProgress(1f, LOC.UI.Loading_Finished);
		await UniTask.NextFrame();
		OptionsStatic.LastLoadingSucceded.SetValue(value: true);
		Application.lowMemory += () =>
		{
			Debug.LogError("LOW MEM");
			PrefabPoolStatic.ForceClear();
			ReferenceBase.PurgeDeadAutoReferences();
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Resources.UnloadUnusedAssets();
		};
		LogLoadingInfo("Finished loading");
		Application.runInBackground = OptionsStatic.KeepRunningWhenMinimizedPref;
		return true;
	}

	private static async UniTask LoadLoadables(IProgressManager progressManager)
	{
		List<LoadNode> remainingLoadNodes = new List<LoadNode>();
		PopulateLoadNodeList(remainingLoadNodes);
		List<LoadNode> loadStartOrder = new List<LoadNode>();
		List<LoadNode> finishedLoadNodes = new List<LoadNode>();
		List<(UniTask task, LoadNode loadNode)> runningLoadNodes = new List<(UniTask, LoadNode)>();
		while (remainingLoadNodes.Count > 0)
		{
			for (int num = remainingLoadNodes.Count - 1; num >= 0; num--)
			{
				LoadNode loadNode = remainingLoadNodes[num];
				if (loadNode.DependenciesRemaining.Count == 0)
				{
					LogLoadingInfo(loadNode.Loadable.Name + ": STARTED LOADING");
					remainingLoadNodes.RemoveAt(num);
					loadStartOrder.Add(loadNode);
					runningLoadNodes.Add((loadNode.Loadable.Load(loadNode), loadNode));
				}
			}
			if (runningLoadNodes.Count == 0)
			{
				throw new Exception("Cannot load \"" + string.Join(", ", remainingLoadNodes.Select((LoadNode t) => t.Loadable.Name)) + "\"! This is a complete disaster!");
			}
			await UpdateRunningTasks();
		}
		while (runningLoadNodes.Count > 0)
		{
			UpdateProgress();
			await UpdateRunningTasks();
		}
		void NodeFinishedLoading(int finishedTaskIndex)
		{
			LoadNode item = runningLoadNodes[finishedTaskIndex].loadNode;
			LogLoadingInfo(item.Loadable.Name + ": FINISHED LOADING");
			runningLoadNodes.RemoveAt(finishedTaskIndex);
			finishedLoadNodes.Add(item);
			foreach (LoadNode item2 in remainingLoadNodes)
			{
				item2.DependenciesRemaining.Remove(item);
			}
		}
		void UpdateProgress()
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.Append(LOC.UI.CurrentlyLoading);
				float num2 = 0f;
				for (int i = 0; i < runningLoadNodes.Count; i++)
				{
					LoadNode item = runningLoadNodes[i].loadNode;
					item.UseProgressValues(out var progress, out var title);
					num2 += progress;
					stringBuilder.Append(item.Loadable.Name);
					if (!string.IsNullOrEmpty(title))
					{
						stringBuilder.Append('(');
						stringBuilder.Append(title);
						stringBuilder.Append(')');
					}
					if (i != runningLoadNodes.Count - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				float percent = ((float)finishedLoadNodes.Count + num2) / (float)_loadables.Count;
				string text = stringBuilder.ToString();
				progressManager.ReportProgress(percent, text);
				LogLoadingInfo(text);
			}
		}
		async UniTask UpdateRunningTasks()
		{
			while (true)
			{
				bool flag = false;
				for (int i = runningLoadNodes.Count - 1; i >= 0; i--)
				{
					if (runningLoadNodes[i].task.Status.IsCompleted())
					{
						await runningLoadNodes[i].task;
						flag = true;
						NodeFinishedLoading(i);
					}
				}
				if (flag)
				{
					break;
				}
				UpdateProgress();
				await UniTask.NextFrame();
			}
		}
	}

	private static void PopulateLoadNodeList(List<LoadNode> loadNode)
	{
		Dictionary<Type, LoadNode> dictionary = new Dictionary<Type, LoadNode>();
		foreach (LoadableReferenceBase loadable in _loadables)
		{
			LoadNode loadNode2 = new LoadNode
			{
				Loadable = loadable
			};
			dictionary[loadable.SourceType] = loadNode2;
			loadNode.Add(loadNode2);
		}
		foreach (LoadNode value2 in dictionary.Values)
		{
			if (value2.Loadable.Dependencies != null)
			{
				Type[] dependencies = value2.Loadable.Dependencies;
				foreach (Type type in dependencies)
				{
					if (dictionary.TryGetValue(type, out var value))
					{
						value2.DependenciesRemaining.Add(value);
					}
					else
					{
						Debug.LogError($"Dependency of {value2.Loadable.SourceType} ({type.FullName}) not found!!!");
					}
				}
			}
			else
			{
				value2.DependenciesRemaining = new List<LoadNode>();
			}
		}
	}
}
