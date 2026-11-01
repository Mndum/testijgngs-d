using System;
using UnityEngine;

public class ProgressManagerSubtask : IProgressManager, IProgress<float>
{
	public IProgressManager ParentProgressManager { get; private set; }

	public float Start { get; private set; }

	public float End { get; private set; }

	public long MillisecondsSinceLastUpdate => ParentProgressManager.MillisecondsSinceLastUpdate;

	public ProgressManagerSubtask(IProgressManager parentProgressManager, float start, float end)
	{
		ParentProgressManager = parentProgressManager;
		Start = start;
		End = end;
	}

	public void ReportProgress(string title)
	{
		ParentProgressManager.ReportProgress(title);
	}

	public void ReportProgress(float percent)
	{
		ParentProgressManager.ReportProgress(Mathf.Lerp(Start, End, percent));
	}

	public void ReportProgress(float percent, string text)
	{
		ParentProgressManager.ReportProgress(Mathf.Lerp(Start, End, percent), text);
	}
}
