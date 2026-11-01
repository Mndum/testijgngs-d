using UnityEngine;
using UnityEngine.UI;

public class CuddleStaticGui : StaticGuiBase
{
	[Header("Other")]
	[SerializeField]
	private Button returnButton;

	[SerializeField]
	private ButtonList buttonList;

	public Button ReturnButton => returnButton;

	public ButtonList ButtonList => buttonList;

	protected override void FillReferencesInherited()
	{
		Transform transform = base.transform;
		transform.EnsureComponentInChild<Button>(out returnButton, "Return Button");
		transform.EnsureComponentInChild<ButtonList>(out buttonList, "Buttons");
	}
}
