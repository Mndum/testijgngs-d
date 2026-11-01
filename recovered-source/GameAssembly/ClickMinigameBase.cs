using System;
using System.Collections;
using UnityEngine;

public abstract class ClickMinigameBase : MinigameCore
{
	public enum Score
	{
		NotStarted,
		None,
		Miss,
		Okay,
		Good,
		Perfect
	}

	private RectTransform _mainBar;

	private RectTransform _okayBar;

	private RectTransform _goodBar;

	private RectTransform _perfectBar;

	private RectTransform _cursor;

	private float _cursorPos;

	private float _target;

	private float _targetPixels;

	protected Score _currentScore;

	private void SetUpBar(RectTransform bar, float width, float difficultyPixels)
	{
		bar.SetLeft(Mathf.Max(0f, _targetPixels - difficultyPixels));
		bar.SetRight(Mathf.Max(0f, width - _targetPixels - difficultyPixels));
	}

	public void SetCursorPos(float pos)
	{
		float x = _mainBar.sizeDelta.x;
		_cursor.anchoredPosition = new Vector2(x * pos, 0f);
	}

	private void SetUpBars(float target, float difficulty)
	{
		_target = target;
		_cursorPos = Mathf.Repeat(target + 0.5f, 1f);
		float x = _mainBar.sizeDelta.x;
		_targetPixels = x * target;
		float num = x * difficulty;
		SetUpBar(_okayBar, x, num);
		SetUpBar(_goodBar, x, num / 2f);
		SetUpBar(_perfectBar, x, num / 8f);
		SetCursorPos(_cursorPos);
		_currentScore = Score.NotStarted;
	}

	public bool IsIn(float positionPixels, RectTransform bar)
	{
		Vector2 leftAndRight = bar.GetLeftAndRight();
		leftAndRight.y = _mainBar.sizeDelta.x - leftAndRight.y;
		if (positionPixels >= leftAndRight.x)
		{
			return positionPixels <= leftAndRight.y;
		}
		return false;
	}

	public void Restart(float difficulty)
	{
		float target = UnityEngine.Random.Range(0f + difficulty, 1f - difficulty);
		SetUpBars(target, difficulty);
	}

	protected override void Init(Action<float> onComplete)
	{
		base.Init(onComplete);
		_mainBar = transform.Find("Bar") as RectTransform;
		_okayBar = _mainBar.Find("Okay") as RectTransform;
		_goodBar = _mainBar.Find("Good") as RectTransform;
		_perfectBar = _mainBar.Find("Perfect") as RectTransform;
		_cursor = _mainBar.Find("Cursor") as RectTransform;
		StartCoroutine(Game());
	}

	private void Update()
	{
		if (_currentScore != Score.None)
		{
			return;
		}
		if (Input.anyKeyDown)
		{
			float x = _cursor.anchoredPosition.x;
			if (IsIn(x, _perfectBar))
			{
				_currentScore = Score.Perfect;
				return;
			}
			if (IsIn(x, _goodBar))
			{
				_currentScore = Score.Good;
				return;
			}
			if (IsIn(x, _okayBar))
			{
				_currentScore = Score.Okay;
				return;
			}
			_currentScore = Score.Miss;
		}
		float num = 1f - Mathf.Min(Mathf.Abs(_target - _cursorPos), Mathf.Abs(1f + _target - _cursorPos));
		_cursorPos = Mathf.Repeat(_cursorPos + Time.deltaTime * num, 1f);
		SetCursorPos(_cursorPos);
	}

	protected abstract IEnumerator Game();
}
