using System;
using System.Collections.Generic;
using System.Linq;

public class ModCreationLive2DControllersContainer : IDisposable
{
	private List<Live2DController> _controllers;

	private List<Live2DPrefabHolder.Live2DControllerUsage<Live2DController>> _usages;

	private Dictionary<ControllerEnum, Live2DController> _enumToController;

	public IReadOnlyCollection<Live2DController> Controllers => _controllers;

	public ModCreationLive2DControllersContainer(List<Live2DPrefabHolder.Live2DControllerUsage<Live2DController>> usages)
	{
		_usages = usages;
		_controllers = usages.Select((Live2DPrefabHolder.Live2DControllerUsage<Live2DController> x) => x.Controller).ToList();
		_enumToController = _controllers.ToDictionary((Live2DController x) => x.ControllerEnum, (Live2DController x) => x);
	}

	public Live2DController GetController(ControllerEnum value)
	{
		return _enumToController[value];
	}

	public void Dispose()
	{
		_controllers.Clear();
		_usages.ClearWithDispose();
		_enumToController.Clear();
	}
}
