public interface ILive2DModelUser
{
	Live2DModelUsageTypeEnum Live2DModelUsage { get; }

	void PrefabUsageStopped(Live2DPrefabHolder.ILive2DControllerUsage usage);
}
