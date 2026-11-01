using UnityEngine;

public class InteractStaticGui : StaticGuiBase
{
	[Header("Other")]
	[SerializeField]
	private ButtonList buttonList;

	public ButtonList ButtonList => buttonList;

	protected override void FillReferencesInherited()
	{
		transform.EnsureComponentInChild<ButtonList>(out buttonList, "Buttons");
	}
}
