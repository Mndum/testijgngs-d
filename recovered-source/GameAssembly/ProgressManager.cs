using System;

public class ProgressManager : IProgressManager, IProgress<float>
{
	public Action<string> OnTitle { get; set; }

	public Action<float, string> OnProgress { get; set; }

	public long MillisecondsSinceLastUpdate => 0L;

	public void ReportProgress(float percent)
	{
		OnProgress?.Invoke(percent, null);
	}

	public void ReportProgress(float percent, string text)
	{
		OnProgress?.Invoke(percent, text);
	}

	public void ReportProgress(string title)
	{
		OnTitle?.Invoke(title);
	}
}
