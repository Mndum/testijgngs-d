using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class CocktracktsMainWebsite : CocktractsWebsiteBase
{
	[SerializeField]
	private TextMeshProUGUI mainSummary;

	[SerializeField]
	private ContractDisplay currentContractDisplay;

	private void UpdateCurrentContract()
	{
		CocktractContract current = CTM.CurrentContracts.FirstOrDefault();
		currentContractDisplay.SetEmptyText(LOC.UI.Contract_NoContract);
		currentContractDisplay.Current = current;
		UpdateSummary();
	}

	private void UpdateSummary()
	{
		CocktractContract.ContractsAggregateData contractsAggregateData = CocktractContract.AggregateData(CTM.PastContracts);
		mainSummary.text = LOC.UI.cocktracts_Main_Summary(contractsAggregateData.CompletedCount, contractsAggregateData.FailedAndAbandonedCount);
	}

	public override IEnumerator Open()
	{
		UpdateCurrentContract();
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
