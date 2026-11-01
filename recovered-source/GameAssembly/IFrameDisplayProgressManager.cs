using System;

public interface IFrameDisplayProgressManager : IProgressManager, IProgress<float>
{
	void UseProgressValues(out float progress, out string title);
}
