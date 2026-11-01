using TMPro;
using UnityEngine;

public class PartnerDisplay : DataDisplay<CocktractPartner>
{
	[SerializeField]
	private TextMeshProUGUI nameTMP;

	[SerializeField]
	private TextMeshProUGUI descriptionTMP;

	[SerializeField]
	private TextMeshProUGUI specialDataTMP;

	[SerializeField]
	private LogoDisplay logoDisplay;

	protected override void SetData(CocktractPartner newValue)
	{
		if (newValue != null)
		{
			_current = newValue;
			nameTMP.text = _current.Name;
			descriptionTMP.text = _current.Description;
			specialDataTMP.text = _current.GetFinishedContractsDescription();
			logoDisplay.Current = newValue;
		}
		else
		{
			_current = null;
			nameTMP.text = LOC.UI.UI_Error;
			descriptionTMP.text = LOC.UI.UI_Error;
			specialDataTMP.text = LOC.UI.UI_Error;
			logoDisplay.Current = null;
		}
	}
}
