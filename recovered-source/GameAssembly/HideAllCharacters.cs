using Fungus;
using UnityEngine;

[CommandInfo("My", "Hide All Characters", "Hide all characters", 0)]
internal class HideAllCharacters : Command
{
	public override void OnEnter()
	{
		SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.HideAllCharacters();
		Continue();
	}

	public override string GetSummary()
	{
		return "All characters disappear!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
