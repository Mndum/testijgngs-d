using System;

public abstract class ResultGameState : GameState
{
	private bool _isDone;

	public Action OnResultOverride;

	protected void ReportResult()
	{
		if (!_isDone)
		{
			_isDone = true;
			if (OnResultOverride != null)
			{
				OnResultOverride?.Invoke();
				OnResultOverride = null;
			}
			else
			{
				DefaultOnResult();
			}
		}
	}

	protected abstract void DefaultOnResult();
}
public abstract class ResultGameState<TResult> : GameState
{
	private bool _isDone;

	public Action<TResult> OnResultOverride;

	protected void ReportResult(TResult result)
	{
		if (!_isDone)
		{
			_isDone = true;
			if (OnResultOverride != null)
			{
				OnResultOverride?.Invoke(result);
				OnResultOverride = null;
			}
			else
			{
				DefaultOnResult(result);
			}
		}
	}

	protected abstract void DefaultOnResult(TResult result);
}
