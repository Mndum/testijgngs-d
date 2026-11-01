using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TimeoutSimplePopup : PrefabPopup<TimeoutSimplePopup>
{
	[SerializeField]
	private TextMeshProUGUI titleTmp;

	[SerializeField]
	private TextMeshProUGUI textTmp;

	[SerializeField]
	private ButtonList buttonList;

	private CursorSingleton.CursorOverride _cursorOverride;

	private CancellationTokenSource _countdownCts;

	private int _defaultChoiceIndex;

	public void Open(string title, Func<TimeSpan, string> textGenerator, List<PopupChoice> choices, int defaultChoiceIndex = 0, TimeSpan? timeoutDuration = null)
	{
		TimeSpan duration = timeoutDuration ?? TimeSpan.FromSeconds(15.0);
		_defaultChoiceIndex = Mathf.Clamp(defaultChoiceIndex, 0, choices.Count - 1);
		titleTmp.text = title;
		buttonList.InitializeFromPopupChoices(choices, OnChoiceSelected);
		Open();
		CursorSingleton.ContinueOrStartCursorOverride(ref _cursorOverride, CursorSingleton.CursorType.Normal);
		StartCountdown(textGenerator, duration);
	}

	private void OnChoiceSelected()
	{
		CancelCountdown();
		CloseFromUIOverlay();
	}

	private async void StartCountdown(Func<TimeSpan, string> textGenerator, TimeSpan duration)
	{
		CancelCountdown();
		_countdownCts = new CancellationTokenSource();
		DateTime endTime = DateTime.UtcNow + duration;
		try
		{
			while (DateTime.UtcNow < endTime)
			{
				TimeSpan arg = endTime - DateTime.UtcNow;
				textTmp.text = textGenerator(arg);
				await UniTask.Delay(100, ignoreTimeScale: false, PlayerLoopTiming.Update, _countdownCts.Token);
			}
			textTmp.text = textGenerator(TimeSpan.Zero);
			await UniTask.Delay(500, ignoreTimeScale: false, PlayerLoopTiming.Update, _countdownCts.Token);
			buttonList.Buttons.ElementAt(_defaultChoiceIndex).Click();
		}
		catch (OperationCanceledException)
		{
		}
		finally
		{
			_countdownCts = null;
		}
	}

	private void CancelCountdown()
	{
		if (_countdownCts != null)
		{
			_countdownCts.Cancel();
			_countdownCts.Dispose();
			_countdownCts = null;
		}
	}

	protected void OnDestroy()
	{
		CancelCountdown();
		CursorSingleton.EndCursorOverride(ref _cursorOverride);
	}
}
