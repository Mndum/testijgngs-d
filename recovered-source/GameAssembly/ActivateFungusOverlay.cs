using Fungus;
using UnityEngine;

[CommandInfo("My", "Activate Fungus Overlay", "Activates Fungus Overlay", 0)]
public class ActivateFungusOverlay : Command
{
	public bool _defaultColor = true;

	public Color _overlayColor;

	[HideInInspector]
	public bool _removeAtStateChange;

	[HideInInspector]
	public string _message;

	[HideInInspector]
	public bool _stopFixedUpate;

	public override void OnEnter()
	{
		if (_defaultColor)
		{
			UiOverlay.Instance.OpenFungusOverlay();
		}
		else
		{
			UiOverlay.Instance.OpenFungusOverlay(_overlayColor);
		}
		Continue();
	}

	public override string GetSummary()
	{
		return "Activates fungus overlay without any message.";
	}

	public override Color GetButtonColor()
	{
		return new Color32(255, 50, 50, 255);
	}
}
