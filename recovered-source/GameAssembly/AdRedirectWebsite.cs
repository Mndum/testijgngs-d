using System.Collections;
using TMPro;
using UnityEngine;

internal class AdRedirectWebsite : Website
{
	public override IEnumerator Open()
	{
		yield return new WaitForSeconds(Random.Range(0.1f, 1.25f));
		BrowserPopup.FinishLoading();
		WebsiteSerializableAsset randomAdWebsite = SingletonMonoBehaviour<InternetSingleton>.Instance.GetRandomAdWebsite();
		string address = randomAdWebsite.GameUrl.Assembled;
		GetComponentInChildren<TextMeshProUGUI>().text = LOC.UI.adredirectdotic_Text2(address);
		yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));
		BrowserPopup.ChangeWebsite(address);
	}
}
