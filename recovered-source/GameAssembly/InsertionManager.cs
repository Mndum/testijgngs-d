using System;
using System.Collections.Generic;
using JetBrains.Annotations;

public class InsertionManager : Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_FixedUpdate, Live2DController.ILive2DControllerManager_Callback, Live2DController.ILive2DControllerManager_Update, Live2DController.ILive2DControllerManager_StartUsageGameplay, Live2DController.ILive2DControllerManager_EndUsage
{
	public List<InsertionHandler> InsertionHandlers { get; private set; }

	public Action OnBeforeInsertionHandlersUpdate { get; set; }

	public InsertionManager(List<InsertionHandler> insertionHandlers)
	{
		InsertionHandlers = insertionHandlers;
	}

	[NotNull]
	public InsertionHandler GetNextInsertionHandler(InsertionHandler current)
	{
		int num = InsertionHandlers.IndexOf(current);
		num = ((num >= 0) ? ((num + 1) % InsertionHandlers.Count) : 0);
		return InsertionHandlers[num];
	}

	public void StartUsageGameplay()
	{
		foreach (InsertionHandler insertionHandler in InsertionHandlers)
		{
			insertionHandler.Reset();
		}
	}

	public void EndUsage()
	{
		foreach (InsertionHandler insertionHandler in InsertionHandlers)
		{
			insertionHandler.CleanUpManagedObjects();
		}
	}

	public void Update()
	{
		if (UiOverlay.Instance.IsOpen)
		{
			return;
		}
		OnBeforeInsertionHandlersUpdate?.Invoke();
		foreach (InsertionHandler insertionHandler in InsertionHandlers)
		{
			insertionHandler.Update();
		}
	}

	public void FixedUpdate()
	{
		if (UiOverlay.Instance.IsOpen)
		{
			return;
		}
		foreach (InsertionHandler insertionHandler in InsertionHandlers)
		{
			insertionHandler.FixedUpdate();
		}
	}
}
