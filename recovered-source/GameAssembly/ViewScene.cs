using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class ViewScene : ViewSceneBase, ILive2DModelUser
{
	public string NameOverride;

	[SerializeField]
	private List<View> views;

	private List<Live2DPrefabHolder.ILive2DControllerUsage> activePrefabUsages = new List<Live2DPrefabHolder.ILive2DControllerUsage>();

	public View FirstView => views[0];

	public Live2DModelUsageTypeEnum Live2DModelUsage => Live2DModelUsageTypeEnum.Gameplay;

	public string GetViewSceneName()
	{
		if (!string.IsNullOrEmpty(NameOverride))
		{
			return NameOverride;
		}
		return name;
	}

	public virtual void Prepare()
	{
		foreach (View view in views)
		{
			if (view.Scene == null || view.Scene == this)
			{
				view.Scene = this;
			}
			else
			{
				Debug.LogError($"\"{name} cannot claim view {view}. {view.Scene} already owns it!\"");
			}
		}
		if (views.Count == 0)
		{
			Debug.LogError("No view found in scene \"" + name + "\"");
		}
	}

	protected override void EndOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameScene)
		{
			for (int num = activePrefabUsages.Count - 1; num >= 0; num--)
			{
				activePrefabUsages[num]?.Dispose();
			}
			activePrefabUsages.Clear();
		}
		base.EndOldView(info, thisCallback);
	}

	public virtual bool CanMove(View currentView, View nextView)
	{
		return true;
	}

	public void PrefabUsageStopped(Live2DPrefabHolder.ILive2DControllerUsage usage)
	{
		for (int num = activePrefabUsages.Count - 1; num >= 0; num--)
		{
			if (activePrefabUsages[num] == usage)
			{
				activePrefabUsages.RemoveAtSwapBack(num);
				break;
			}
		}
	}

	public Live2DPrefabHolder.ILive2DControllerUsage MovePrefabInto(Live2DPrefabHolder prefabHolder, Vector3 size, Vector3 offset)
	{
		Live2DPrefabHolder.ILive2DControllerUsage live2DControllerUsage = prefabHolder.StartUsingBy(this);
		activePrefabUsages.Add(live2DControllerUsage);
		Live2DController controller = live2DControllerUsage.Controller;
		controller.transform.SetParent(transform, worldPositionStays: false);
		controller.transform.localScale = size;
		controller.transform.localPosition = offset;
		return live2DControllerUsage;
	}
}
