using Fungus;
using UnityEngine;

[CommandInfo("My", "Deactivate Fungus Overlay", "Deactivates Fungus Overlay", 0)]
public class DeactivateFungusOverlay : Command
{
	public override void OnEnter()
	{
		UiOverlay.Instance.CloseFungusOverlay();
		Continue();
	}

	public override Color GetButtonColor()
	{
		return new Color32(255, 50, 50, 255);
	}
}
