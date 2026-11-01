using System.Collections;
using UnityEngine;

public class CocktractsPartnersWebsite : CocktractsWebsiteBase
{
	[SerializeField]
	private ItemList partnerList;

	public override IEnumerator Open()
	{
		partnerList.ItemCountFunc = () => CTM.CocktractPartners.Count;
		partnerList.ItemUpdatedAction = (GameObject tmp, int i, bool focused) =>
		{
			CocktractPartner current = CTM.CocktractPartners[i];
			tmp.GetComponent<PartnerDisplay>().Current = current;
		};
		partnerList.UpdateAll();
		yield return base.Open();
	}
}
