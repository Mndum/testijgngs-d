using System;

public class FadeViewCallbackOverride : ViewSceneBase.IViewSceneCallback
{
	public Action<ViewSceneBase.ViewSceneCallbacksEnum, ViewSceneBase.ViewSceneTransitionInfo> Callback;

	public bool ShouldStopNormalCallbacks => true;

	public FadeViewCallbackOverride(Action<ViewSceneBase.ViewSceneCallbacksEnum, ViewSceneBase.ViewSceneTransitionInfo> callback)
	{
		Callback = callback;
	}

	public void DoCallback(ViewSceneBase.ViewSceneCallbacksEnum sceneCallbacksEnum, ViewSceneBase.ViewSceneTransitionInfo sceneTransitionInfo)
	{
		Callback?.Invoke(sceneCallbacksEnum, sceneTransitionInfo);
	}
}
