using System;
using Cysharp.Threading.Tasks;

public interface IProgressManager : IProgress<float>
{
	long MillisecondsSinceLastUpdate { get; }

	void ReportProgress(string title);

	void ReportProgress(float percent);

	void IProgress<float>.Report(float percent)
	{
		ReportProgress(percent);
	}

	void ReportProgress(float percent, string title);

	void ProgressListAfter(int i, int count)
	{
		if (i >= count)
		{
			ReportProgress(1f);
		}
		else if (i < 0)
		{
			ReportProgress(0f);
		}
		else
		{
			ReportProgress((float)(i + 1) / (float)count);
		}
	}

	ProgressManagerSubtask PrepareSubTask(float start, float end)
	{
		return new ProgressManagerSubtask(this, start, end);
	}

	async UniTask YieldIfMillisecondsSinceLastUpdate(long milliseconds)
	{
		if (MillisecondsSinceLastUpdate >= milliseconds)
		{
			await UniTask.Yield();
		}
	}

	async UniTask YieldIfTooLongSinceFrame()
	{
		if (MillisecondsSinceLastUpdate >= 100)
		{
			await UniTask.Yield();
		}
	}
}
