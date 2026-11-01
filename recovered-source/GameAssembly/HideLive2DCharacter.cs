using Fungus;
using UnityEngine;

[CommandInfo("My", "Hide Live2D Character", "Hides Live2D character", 0)]
internal class HideLive2DCharacter : Command
{
	[SerializeField]
	public Live2DPrefabHolder character;

	public override void OnEnter()
	{
		if (character != null && character.CurrentlyUsedBy != null)
		{
			character.StopUsingCurrent();
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (character != null)
		{
			return character.name + " disappars";
		}
		return "No character selected!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(255, 150, 255, 255);
	}
}
