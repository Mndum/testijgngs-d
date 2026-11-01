using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SimpleCumLeakManager<T> : ISimpleCumLeakManager, Live2DController.ILive2DControllerManager where T : Live2DController, ILive2DController_Lewd_CumLeakBasic, ILive2DController_Lewd_CumInsideMain
{
	private readonly T _controller;

	private CancellationTokenSource _currentLeakTokenSource;

	public SimpleCumLeakManager(T controller)
	{
		_controller = controller;
	}

	public void StartLeak()
	{
		EndLeak();
		_currentLeakTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_controller.GetUsageCancellationToken());
		Leak(_currentLeakTokenSource.Token).Forget();
	}

	private void EndLeakAnimation()
	{
		_controller.ParamCumLeakAnim.Value = _controller.ParamCumLeakAnim.MinimumValue;
		_controller.ParamCumLeakAnim.FinishMove();
	}

	public void EndLeak()
	{
		if (_currentLeakTokenSource != null)
		{
			_currentLeakTokenSource.Cancel();
			_currentLeakTokenSource.Dispose();
			_currentLeakTokenSource = null;
		}
		EndLeakAnimation();
		_controller.CumInsideManager.SetFastWombLeakActive(value: false);
	}

	private async UniTask Leak(CancellationToken cancellationToken)
	{
		_controller.CumInsideManager.SetFastWombLeakActive(value: true);
		await UniTask.WaitForSeconds(Random.Range(0.2f, 0.5f), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		while (true)
		{
			if (_controller.CumInsideManager.TryExtractCum(InsertionHandlerOrificeTypeEnum.Vagina, Random.Range(8, 20)))
			{
				_controller.ParamCumLeakAnim.Value = _controller.ParamCumLeakAnim.MaximumValue;
				_controller.ParamCumLeakAnim.SetWeight(30f);
				_controller.ParamCumLeakAnim.UpdateType = Live2DParameterUpdateTypeEnum.Smooth;
				await UniTask.WaitUntil(() => _controller.ParamCumLeakAnim.IsRealValueApproximately(_controller.ParamCumLeakAnim.MaximumValue, 0.01f), PlayerLoopTiming.Update, cancellationToken);
				EndLeakAnimation();
			}
			await UniTask.WaitForSeconds(0.1f, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		}
	}
}
