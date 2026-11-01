using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CocktractsPartnerWebsite : CocktractsWebsiteBase
{
	[SerializeField]
	private ContractList contractList;

	[SerializeField]
	private DataDisplay<CocktractPartner> partnerDisplay;

	private CocktractPartner _partner;

	private bool redirectToHell;

	public const string IdArgument = "Id";

	public override void ProcessUrl(GameUrlWithArguments gameUrlWithArguments)
	{
		if (gameUrlWithArguments.Arguments.TryGetValue("Id", out var value) && !CTM.TryGetPartnerById(value, out _partner) && value == "666")
		{
			redirectToHell = true;
		}
		CurrentGameUrl = gameUrlWithArguments;
	}

	public override IEnumerator Open()
	{
		partnerDisplay.Current = _partner;
		if (_partner != null)
		{
			List<CocktractContract> tmp = _partner.FindRelatedContracts().ToList();
			contractList.Initialize(tmp, (int x) =>
			{
				if (tmp.TryGetValue(x, out var value))
				{
					var (source, index) = CTM.GetSource(value);
					BrowserPopup.ChangeWebsite(CocktractsContractWebsite.CreateUrl(source, index));
				}
			}, LOC.UI.cocktracts_NoRelatedContracts);
		}
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		if (redirectToHell)
		{
			BrowserPopup.ChangeWebsite(HellWebsite.WebsiteUrl);
			yield break;
		}
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
