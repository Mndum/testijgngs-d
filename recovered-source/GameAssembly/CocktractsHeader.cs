using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CocktractsHeader : MonoBehaviour
{
	private class CocktractsHeaderButtonData
	{
		public string Text;

		public string Address;

		public CocktractsHeaderButtonData(string text, string address)
		{
			Text = text;
			Address = address;
		}
	}

	private void OnEnable()
	{
		List<CocktractsHeaderButtonData> buttons = new List<CocktractsHeaderButtonData>
		{
			new CocktractsHeaderButtonData(LOC.UI.cocktracts_header_home, "cocktracts.ic"),
			new CocktractsHeaderButtonData(LOC.UI.cocktracts_header_available, "cocktracts.ic/available"),
			new CocktractsHeaderButtonData(LOC.UI.cocktracts_header_partners, "cocktracts.ic/partners"),
			new CocktractsHeaderButtonData(LOC.UI.cocktracts_header_history, "cocktracts.ic/history"),
			new CocktractsHeaderButtonData(LOC.UI.cocktracts_header_help, "cocktracts.ic/help")
		};
		ItemList componentInChildren = GetComponentInChildren<ItemList>();
		componentInChildren.ItemCountFunc = () => buttons.Count;
		componentInChildren.ItemClickedAction = (int x) =>
		{
			if (buttons.TryGetValue(x, out var value))
			{
				gameObject.GetAncestor<BrowserPopup>().ChangeWebsite(value.Address);
			}
		};
		componentInChildren.ItemUpdatedAction = (GameObject gameObject, int x, bool _) =>
		{
			if (buttons.TryGetValue(x, out var value))
			{
				gameObject.GetComponentInChildren<TextMeshProUGUI>().text = value.Text;
			}
		};
		componentInChildren.UpdateAll();
		(componentInChildren.transform as RectTransform).ForceRebuildLayoutRecursive();
	}
}
