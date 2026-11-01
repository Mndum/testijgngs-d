using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AdvancedCumLeakManager<T> : IAdvancedCumLeakManager, Live2DController.ILive2DControllerManager where T : Live2DController, ILive2DController_Lewd_CumLeakAdvanced, ILive2DController_Lewd_CumInsideMain
{
	private readonly T _controller;

	private InsertionHandlerOrificeTypeEnum _currentOrifice;

	private float _leakAnimWeight;

	private Live2DParameterUpdateTypeEnum _leakAnimUpdateType;

	private CancellationTokenSource _currentLeakTokenSource;

	public AdvancedCumLeakManager(T controller, float leakAnimWeight, Live2DParameterUpdateTypeEnum leakAnimUpdateType)
	{
		_controller = controller;
		_leakAnimWeight = leakAnimWeight;
		_leakAnimUpdateType = leakAnimUpdateType;
	}

	public void StartLeak(InsertionHandlerOrificeTypeEnum orifice)
	{
		EndLeak();
		if (!TryGetSourceValue(orifice, out var _))
		{
			Debug.LogError($"Unsupported leak orifice: {orifice}");
			return;
		}
		_currentLeakTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_controller.GetUsageCancellationToken());
		_currentOrifice = orifice;
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

	public bool TryGetSourceValue(InsertionHandlerOrificeTypeEnum orifice, out float value)
	{
		switch (orifice)
		{
		case InsertionHandlerOrificeTypeEnum.Vagina:
			value = 0f;
			return true;
		case InsertionHandlerOrificeTypeEnum.Anal:
			value = 1f;
			return true;
		default:
			value = 0f;
			return false;
		}
	}

	private async UniTask Leak(CancellationToken cancellationToken)
	{
		if (_currentOrifice == InsertionHandlerOrificeTypeEnum.Vagina)
		{
			_controller.CumInsideManager.SetFastWombLeakActive(value: true);
		}
		await UniTask.WaitForSeconds(Random.Range(0.2f, 0.5f), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		while (true)
		{
			float leakMl = Random.Range(8f, 25f);
			if (_controller.CumInsideManager.TryExtractCum(_currentOrifice, leakMl) && TryGetSourceValue(_currentOrifice, out var value))
			{
				_controller.ParamCumLeakSource.SetRealValue(value);
				_controller.ParamCumLeakSize.SetPercent(Mathf.InverseLerp(8f, 25f, leakMl));
				_controller.ParamCumLeakAnim.Value = _controller.ParamCumLeakAnim.MaximumValue;
				_controller.ParamCumLeakAnim.SetWeight(_leakAnimWeight);
				_controller.ParamCumLeakAnim.UpdateType = _leakAnimUpdateType;
				await UniTask.WaitUntil(() => _controller.ParamCumLeakAnim.IsRealValueApproximately(_controller.ParamCumLeakAnim.MaximumValue, 0.05f), PlayerLoopTiming.Update, cancellationToken);
				_controller.OnLeaked(_currentOrifice, leakMl);
				EndLeakAnimation();
				await UniTask.WaitForSeconds(Random.Range(0.1f, 0.2f), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			await UniTask.WaitForSeconds(0.1f, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		}
	}
}
