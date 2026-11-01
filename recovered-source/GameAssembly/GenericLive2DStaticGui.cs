using UnityEngine;

public class GenericLive2DStaticGui : StaticGuiBase
{
	[Header("Other")]
	[SerializeField]
	private Live2DToolSelector live2DToolSelector;

	public Live2DToolSelector Live2DToolSelector => live2DToolSelector;

	protected override void FillReferencesInherited()
	{
		transform.EnsureComponentInChild<Live2DToolSelector>(out live2DToolSelector, "Live2D Tool Selector");
	}
}
