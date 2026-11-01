using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPopup : PrefabPopup<ProgressPopup>
{
	[SerializeField]
	private TextMeshProUGUI titleTmp;

	[SerializeField]
	private TextMeshProUGUI textTmp;

	[SerializeField]
	private Slider slider;

	private float _smoothedProgress;

	private Action _onFinished;

	private FrameDisplayProgressManager _frameDisplayProgressManager;

	public void Open(string title, string text, Func<IProgressManager, UniTask> generator, Action finished)
	{
		textTmp.text = text;
		titleTmp.text = title;
		_onFinished = finished;
		Open();
		_frameDisplayProgressManager = new FrameDisplayProgressManager();
		RunProgress(generator(_frameDisplayProgressManager)).Forget();
	}

	private void Finished()
	{
		CloseFromUIOverlay();
		_onFinished?.Invoke();
	}

	private async UniTask RunProgress(UniTask secondTask)
	{
		await secondTask;
		await UniTask.NextFrame();
		Finished();
	}

	public void Update()
	{
		_frameDisplayProgressManager.UseProgressValues(out var progress, out var title);
		textTmp.text = title;
		_smoothedProgress = GlobalFunctions.MoveTowards(_smoothedProgress, progress, Time.deltaTime / 2f);
		slider.value = _smoothedProgress;
	}
}
