using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CocktractsAvailableWebsite : CocktractsWebsiteBase
{
	[SerializeField]
	private ItemList itemList;

	[SerializeField]
	private TextMeshProUGUI noContractsText;

	public override IEnumerator Open()
	{
		List<CocktractContract> contracts = CTM.AvailableContracts;
		itemList.OnAfterUpdated = (int count) =>
		{
			noContractsText.text = LOC.UI.cocktracts_NoAvailableContracts;
			noContractsText.gameObject.SetActive(count == 0);
		};
		itemList.ItemCountFunc = () => contracts.Count;
		itemList.ButtonFindFunc = (GameObject tmp) => tmp.GetComponent<Button>();
		itemList.ItemClickedAction = (int index) =>
		{
			BrowserPopup.ChangeWebsite(CocktractsContractWebsite.CreateUrl("AvailableContracts", index));
		};
		itemList.ItemUpdatedAction = (GameObject tmp, int i, bool focused) =>
		{
			if (contracts.TryGetValue(i, out var value))
			{
				tmp.GetComponent<ContractAvailableDisplay>().Current = value;
			}
		};
		itemList.UpdateAll();
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
