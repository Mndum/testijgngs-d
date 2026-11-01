using System.Collections;
using UnityEngine;

public class CasinoWebsite : CasinoWebsiteCore
{
	public override IEnumerator Open()
	{
		yield return new WaitForSeconds(0.1f);
		BrowserPopup.FinishLoading();
	}
}
