using UnityEngine;
using UnityEngine.UI;

public class DeveloperRoomGui : StaticGuiBase
{
	[Header("Auto")]
	[SerializeField]
	private Button returnButton;

	[SerializeField]
	private ButtonList sideButtonList;

	public Button ReturnButton => returnButton;

	public ButtonList SideButtonList => sideButtonList;

	protected override void FillReferencesInherited()
	{
		Transform transform = base.transform;
		transform.EnsureComponentInChild<Button>(out returnButton, "Return Button");
		transform.EnsureComponentInChild<ButtonList>(out sideButtonList, "Button List");
	}
}
