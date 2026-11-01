using TMPro;
using UnityEngine;

public class SheepPopup : PrefabPopup<SheepPopup>
{
	[SerializeField]
	private TextMeshProUGUI titleText;

	[SerializeField]
	private TextMeshProUGUI infoText;

	[SerializeField]
	private ButtonList usefulLinksButtonList;

	[SerializeField]
	private ButtonList buttonList;

	public override bool CanClose()
	{
		return true;
	}
}
