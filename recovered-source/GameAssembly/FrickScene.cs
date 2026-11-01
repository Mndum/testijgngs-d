using JetBrains.Annotations;
using UnityEngine;

public class FrickScene : ViewScene, IHasTransitionData<FrickSceneTransitionData>, IHasTransitionData
{
	[SerializeField]
	private ControllerEnum controller;

	[SerializeField]
	public Live2DHolder _holder;

	private Live2DPrefabHolder.Live2DControllerUsage<Live2DLewdSceneController> _controllerUsage;

	public ControllerEnum TargetControllerEnum => controller;

	[CanBeNull]
	public Live2DLewdSceneController GetHeldController()
	{
		return _holder.CurrentController as Live2DLewdSceneController;
	}

	protected override void PrepareNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		base.PrepareNewView(info, thisCallback);
	}

	protected override void SwitchNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		base.SwitchNewView(info, thisCallback);
		if (!info.TryGetRelatedArguments<FrickSceneTransitionData>(thisCallback, out var arguments))
		{
			return;
		}
		if (info.SameScene)
		{
			if (arguments.Usage == null)
			{
				Debug.LogError("Usage is null when switching to the same scene! This is wrong!");
			}
			ShowErrorIfNoSameSceneFlag(arguments);
			return;
		}
		if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.TryGetStateAs<NewFrickState>(out var _))
		{
			GameVariables.Current.ManifestHorninessAndResetFrickData();
		}
		Live2DPrefabHolder.ILive2DControllerUsage live2DControllerUsage = (arguments.Usage = _holder.StartUsing<Live2DLewdSceneController>(controller.GetLive2DPrefabHolder(), null));
		arguments.Controller = (Live2DLewdSceneController)live2DControllerUsage.Controller;
	}

	protected override void StartNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		base.StartNewView(info, thisCallback);
		if (info.TryGetRelatedArguments<FrickSceneTransitionData>(thisCallback, out var arguments))
		{
			if (info.SameScene)
			{
				ShowErrorIfNoSameSceneFlag(arguments);
				return;
			}
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.TryGetStateAs<NewFrickState>(out var state))
			{
				state.ChangeLive2DController(arguments.Controller);
				return;
			}
			state = new NewFrickState(arguments.Controller, arguments.InitFlags);
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(state);
		}
	}

	protected override void SwitchOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		base.SwitchOldView(info, thisCallback);
		if (!info.TryGetRelatedArguments<FrickSceneTransitionData>(thisCallback, out var arguments))
		{
			return;
		}
		if (info.SameScene)
		{
			if (info.TryGetNewArguments<FrickSceneTransitionData>(out var arguments2))
			{
				arguments2.Usage = arguments.Usage;
				arguments.Usage = null;
			}
			else
			{
				Debug.LogError("Old transition data not found! Can't transition the usage from it!");
			}
		}
		else if (arguments.Usage != null)
		{
			Debug.Log($"Disposing {arguments.Usage}");
			arguments.Usage.Dispose();
			arguments.Usage = null;
		}
	}

	protected override void EndOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		base.EndOldView(info, thisCallback);
	}

	private static void ShowErrorIfNoSameSceneFlag(FrickSceneTransitionData transitionData)
	{
		if (!transitionData.InitFlags.HasFlag(FrickSceneInitFlags.SameScene))
		{
			Debug.LogError("Same scene transition doesn't have same scene flag!");
		}
	}
}
