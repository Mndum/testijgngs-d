using System;
using Fungus;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class GameUtilities : SingletonMonoBehaviour<GameUtilities>
{
	[Preserve]
	public SaveContainer Save => SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save;

	[Preserve]
	public SaveSingleton SaveSingleton => SingletonLoadableMonoBehaviour<SaveSingleton>.Instance;

	[Preserve]
	public void AddItemToInventory(Item item)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.AddItem(item);
	}

	[Preserve]
	public void FadeToView(View targetView, Action afterFinished = null, float duration = 1f, bool fadeOut = true, Color? color = null)
	{
		Color valueOrDefault = color.GetValueOrDefault(Color.black);
		FungusManager.Instance.CameraManager.FadeToView(UiOverlay.Instance.MainCamera, targetView, duration, fadeOut, valueOrDefault, ViewSceneBase.OnFinishedCallback.FromAction(afterFinished));
	}

	[Preserve]
	public void GetCurrentYearToInteger()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.SetIntegerVariable("Integer", DateTime.Now.Year);
	}

	[Preserve]
	public void MoveToView(View targetView, Action afterFinished = null, float duration = 1f, CameraManager.PanType panType = CameraManager.PanType.Normal)
	{
		FungusManager.Instance.CameraManager.MoveToView(UiOverlay.Instance.MainCamera, targetView, duration, panType, ViewSceneBase.OnFinishedCallback.FromAction(afterFinished));
	}

	[Preserve]
	public void FadeOrMoveToView(View targetView, Action afterFinished, float duration = 1f)
	{
		View currentView = SingletonMonoBehaviour<ViewSingleton>.Instance.CurrentView;
		ViewSceneBase viewSceneBase = currentView?.Scene;
		ViewSceneBase scene = targetView.Scene;
		if (scene != null && viewSceneBase == scene && scene is ViewScene viewScene && viewScene.CanMove(currentView, targetView))
		{
			float duration2 = ((currentView == targetView) ? 0f : duration);
			MoveToView(targetView, afterFinished, duration2);
		}
		else
		{
			FadeToView(targetView, afterFinished, duration);
		}
	}

	[UsedInLua]
	public WaitForSeconds WaitForSecondsYield(float seconds)
	{
		if (SingletonScriptableObject<DebugSO>.Instance.IsRunningPlaytest)
		{
			return WaitForSecondCache.Seconds0_05;
		}
		return new WaitForSeconds(seconds);
	}
}
