using JetBrains.Annotations;
using UnityEngine;

public class Live2DHolder : MonoBehaviour, ILive2DModelUser
{
	private Live2DPrefabHolder.ILive2DControllerUsage _usage;

	[CanBeNull]
	public Live2DPrefabHolder CurrentPrefab => _usage?.PrefabHolder;

	[CanBeNull]
	public Live2DController CurrentController => _usage?.Controller;

	public Live2DModelUsageTypeEnum Live2DModelUsage => Live2DModelUsageTypeEnum.Gameplay;

	public bool HasValidUsage()
	{
		if (_usage != null)
		{
			return _usage.IsValid;
		}
		return false;
	}

	public Live2DPrefabHolder.Live2DControllerUsage<T> StartUsing<T>(Live2DPrefabHolder prefabHolder, Live2DPrefabHolder.CustomResetFunctionDelegate<T> customResetFunctionDelegate) where T : Live2DController
	{
		StopCurrentUsage();
		Live2DPrefabHolder.Live2DControllerUsage<T> result = (Live2DPrefabHolder.Live2DControllerUsage<T>)(_usage = prefabHolder.StartUsingBy(this, customResetFunctionDelegate));
		_usage.Controller.transform.SetParent(transform, worldPositionStays: false);
		return result;
	}

	public void PrefabUsageStopped(Live2DPrefabHolder.ILive2DControllerUsage usage)
	{
		if (usage == _usage)
		{
			_usage = null;
		}
	}

	public void StopCurrentUsage()
	{
		_usage?.Dispose();
		_usage = null;
	}
}
