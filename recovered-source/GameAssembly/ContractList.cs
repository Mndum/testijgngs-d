using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContractList : MonoBehaviour
{
	[SerializeField]
	private ItemList itemList;

	[SerializeField]
	private TextMeshProUGUI emptyText;

	public void Initialize(List<CocktractContract> contracts, ItemList.ItemDelegate onClick, string emptyLocalizedText)
	{
		emptyText.text = emptyLocalizedText;
		itemList.OnAfterUpdated = (int count) =>
		{
			emptyText.gameObject.SetActive(count == 0);
		};
		itemList.ItemCountFunc = () => contracts.Count;
		itemList.ButtonFindFunc = (GameObject tmp) => tmp.transform.GetComponent<Button>();
		itemList.ItemClickedAction = onClick;
		itemList.ItemUpdatedAction = (GameObject tmp, int i, bool focused) =>
		{
			CocktractContract current = contracts[i];
			tmp.GetComponent<ContractDisplayShort>().Current = current;
		};
		itemList.UpdateAll();
	}
}
