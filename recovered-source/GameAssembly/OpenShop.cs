using Fungus;
using UnityEngine;

[CommandInfo("My", "OpenShop", "Starts shop with items", 0)]
public class OpenShop : Command
{
	public string shopId;

	public override void OnEnter()
	{
		UiOverlay.Instance.OpenShop(shopId, Continue);
	}

	public override string GetSummary()
	{
		return "Opens a shop \"" + shopId + "\"";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}
}
