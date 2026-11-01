using TMPro;
using UnityEngine;

public class ContractDisplayShort : DataDisplay<CocktractContract>
{
	[SerializeField]
	private TextMeshProUGUI description;

	protected override void SetData(CocktractContract newValue)
	{
		if (newValue != null)
		{
			if (newValue != _current)
			{
				description.text = newValue.GenerateHistoryDescription();
			}
		}
		else
		{
			description.text = LOC.UI.UI_Error;
		}
		_current = newValue;
	}
}
