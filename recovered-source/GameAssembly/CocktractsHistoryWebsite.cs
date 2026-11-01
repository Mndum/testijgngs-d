using System.Collections;
using UnityEngine;

public class CocktractsHistoryWebsite : CocktractsWebsiteBase
{
	[SerializeField]
	private ContractList contractList;

	public override IEnumerator Open()
	{
		contractList.Initialize(CTM.PastContracts, (int x) =>
		{
			BrowserPopup.ChangeWebsite(CocktractsContractWebsite.CreateUrl("PastContracts", x));
		}, LOC.UI.cocktracts_NoHistoryContracts);
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
