using Fungus;
using UnityEngine;
using UnityEngine.Serialization;

[CommandInfo("My", "Reset frick character", "Resets frick character", 0)]
public class ResetFrickCharacter : Command
{
	public ViewScene scene;

	public Live2DPrefabHolder prefabHolder;

	[FormerlySerializedAs("transferCum")]
	public bool continueFuck;

	public override void OnEnter()
	{
		Continue();
	}

	public override string GetSummary()
	{
		if (scene == null)
		{
			return "ERROR: No scene selected!";
		}
		if (prefabHolder == null)
		{
			return "ERROR: No model prefab selected!";
		}
		return "Prepares frick character for " + prefabHolder.name;
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}
}
