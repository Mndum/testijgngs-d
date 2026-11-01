using System;
using System.Diagnostics;

public class FrameDisplayProgressManager : IFrameDisplayProgressManager, IProgressManager, IProgress<float>
{
	private string _currentTitle;

	private float _currentProgress;

	private Stopwatch _stopwatch = new Stopwatch();

	public long MillisecondsSinceLastUpdate => _stopwatch.ElapsedMilliseconds;

	public FrameDisplayProgressManager()
	{
		_stopwatch.Start();
	}

	public void ReportProgress(float percent)
	{
		_currentProgress = percent;
	}

	public void ReportProgress(float percent, string text)
	{
		_currentProgress = percent;
		if (text != null)
		{
			_currentTitle = text;
		}
	}

	public void ReportProgress(string title)
	{
		_currentTitle = title;
	}

	public void UseProgressValues(out float progress, out string title)
	{
		_stopwatch.Restart();
		progress = _currentProgress;
		title = _currentTitle;
	}
}
