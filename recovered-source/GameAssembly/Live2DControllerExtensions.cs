using System;
using Live2D.Cubism.Rendering;
using UnityEngine;

public static class Live2DControllerExtensions
{
	public static Vector3 CalculateWorldBoundsCenter(this CubismRenderer cubismRenderer)
	{
		Bounds bounds = cubismRenderer.Mesh.bounds;
		return cubismRenderer.transform.TransformPoint(bounds.center);
	}

	public static ControllerEnum GetControllerEnumFromPositionId(this string sceneId)
	{
		if (Enum.TryParse<ControllerEnum>(sceneId, ignoreCase: true, out var result))
		{
			return result;
		}
		return ControllerEnum.NoScene;
	}

	public static string GetControllerId(this ControllerEnum controller)
	{
		if (controller == ControllerEnum.NoScene)
		{
			return "";
		}
		return controller.ToString().ToLowerInvariant();
	}

	public static string ToFlag(this ControllerEnum controller)
	{
		if (controller == ControllerEnum.NoScene)
		{
			return "";
		}
		return controller.ToString().ToLowerInvariant();
	}

	public static string GetControllerLocalizedName(this ControllerEnum controller)
	{
		return controller switch
		{
			ControllerEnum.Missionary => LOC.UI.Position_Missionary, 
			ControllerEnum.Doggy => LOC.UI.Position_Doggy, 
			ControllerEnum.Blowjob => LOC.UI.Position_Blowjob, 
			ControllerEnum.Cowgirl => LOC.UI.Position_Cowgirl, 
			ControllerEnum.Interaction => LOC.UI.Position_Interaction, 
			_ => LOC.UI.UI_Error, 
		};
	}

	public static Live2DPrefabHolder GetLive2DPrefabHolder(this ControllerEnum controller)
	{
		return controller switch
		{
			ControllerEnum.Doggy => SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.DoggyCharacterHolder, 
			ControllerEnum.Cowgirl => SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.CowgirlCharacterHolder, 
			ControllerEnum.Blowjob => SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.BlowjobCharacterHolder, 
			ControllerEnum.Interaction => SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.InteractCharacterHolder, 
			ControllerEnum.Shower => SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.ShowerCharacterHolder, 
			_ => SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.MissionaryCharacterHolder, 
		};
	}
}
