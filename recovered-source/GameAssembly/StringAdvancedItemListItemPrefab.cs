using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StringAdvancedItemListItemPrefab : AdvancedItemListPrefab
{
	[SerializeField]
	private TextMeshProUGUI _description;

	[SerializeField]
	private Button _button;

	public TextMeshProUGUI VisibleText => _description;

	public override bool HasDefaultButton => true;

	public override Button GetDefaultButton()
	{
		return _button;
	}
}
