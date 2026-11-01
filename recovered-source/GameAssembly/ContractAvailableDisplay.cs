using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ContractAvailableDisplay : DataDisplay<CocktractContract>
{
	[SerializeField]
	private TextMeshProUGUI description;

	[FormerlySerializedAs("newContractText")]
	[SerializeField]
	private TextMeshProUGUI specialInfoText;

	[SerializeField]
	private LogoDisplay logoDisplay;

	[FormerlySerializedAs("tooltip")]
	[SerializeField]
	private TooltipDisplayArea tooltipDisplayArea;

	protected override void SetData(CocktractContract newValue)
	{
		if (newValue != null)
		{
			if (newValue.IsGold)
			{
				specialInfoText.gameObject.SetActive(value: true);
				specialInfoText.text = LOC.UI.Contracts_GoldContract.TMPColorSurround(Color.yellow);
				tooltipDisplayArea.tooltipText = LOC.UI.Contracts_GoldContractTooltip;
				tooltipDisplayArea.enabled = true;
			}
			else if (!newValue.Seen)
			{
				specialInfoText.gameObject.SetActive(value: true);
				specialInfoText.text = LOC.UI.cocktracts_NewContract.TMPColorSurround(Color.green);
				tooltipDisplayArea.enabled = false;
			}
			else
			{
				specialInfoText.gameObject.SetActive(value: false);
				tooltipDisplayArea.enabled = false;
			}
			description.text = newValue.GenerateAvailableDescription();
			if (logoDisplay != null)
			{
				logoDisplay.Current = newValue.Partner;
			}
		}
		else
		{
			description.text = LOC.UI.UI_Error;
			if (logoDisplay != null)
			{
				logoDisplay.Current = null;
			}
			tooltipDisplayArea.enabled = false;
		}
		_current = newValue;
	}
}
