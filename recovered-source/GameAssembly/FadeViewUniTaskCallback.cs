using Cysharp.Threading.Tasks;

public class FadeViewUniTaskCallback : ViewSceneBase.IViewSceneCallback
{
	public ViewSceneBase.ViewSceneTransitionInfo IntroInfo;

	public ViewSceneBase.ViewSceneTransitionInfo OutroInfo;

	public ViewSceneBase.ViewSceneCallbacksEnum State { get; private set; }

	public bool ShouldStopNormalCallbacks => false;

	public void DoCallback(ViewSceneBase.ViewSceneCallbacksEnum sceneCallbacksEnum, ViewSceneBase.ViewSceneTransitionInfo sceneTransitionInfo)
	{
		if (sceneCallbacksEnum > State)
		{
			State = sceneCallbacksEnum;
		}
		switch (sceneCallbacksEnum)
		{
		case ViewSceneBase.ViewSceneCallbacksEnum.Intro_PrepareNewView:
			IntroInfo = sceneTransitionInfo.MakeCopy();
			break;
		case ViewSceneBase.ViewSceneCallbacksEnum.Outro_SwitchOldView:
			OutroInfo = sceneTransitionInfo.MakeCopy();
			break;
		case ViewSceneBase.ViewSceneCallbacksEnum.Intro_SwitchNewView:
		case ViewSceneBase.ViewSceneCallbacksEnum.Intro_StartNewView:
		case ViewSceneBase.ViewSceneCallbacksEnum.Outro_EndOldView:
			break;
		}
	}

	public UniTask WaitForCallbackAsync(ViewSceneBase.ViewSceneCallbacksEnum callback)
	{
		if (State >= callback)
		{
			return UniTask.CompletedTask;
		}
		return UniTask.WaitUntil(() => State >= callback);
	}

	public UniTask AwaitIntroPrepare1()
	{
		return WaitForCallbackAsync(ViewSceneBase.ViewSceneCallbacksEnum.Intro_PrepareNewView);
	}

	public UniTask AwaitIntroSwitch2()
	{
		return WaitForCallbackAsync(ViewSceneBase.ViewSceneCallbacksEnum.Intro_SwitchNewView);
	}

	public UniTask AwaitIntroStart3()
	{
		return WaitForCallbackAsync(ViewSceneBase.ViewSceneCallbacksEnum.Intro_StartNewView);
	}

	public UniTask AwaitTillStart()
	{
		return WaitForCallbackAsync(ViewSceneBase.ViewSceneCallbacksEnum.Intro_StartNewView);
	}

	public UniTask AwaitOutroSwitch4()
	{
		return WaitForCallbackAsync(ViewSceneBase.ViewSceneCallbacksEnum.Outro_SwitchOldView);
	}

	public UniTask AwaitOutroEnd5()
	{
		return WaitForCallbackAsync(ViewSceneBase.ViewSceneCallbacksEnum.Outro_EndOldView);
	}
}
