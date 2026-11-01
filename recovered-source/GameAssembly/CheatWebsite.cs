using System.Collections;

internal class CheatWebsite : Website
{
	public void GetMoney()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.GetADollar();
	}

	public override IEnumerator Open()
	{
		BrowserPopup.FinishLoading();
		yield break;
	}

	public void OnDestroy()
	{
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.SaveAllChanges();
	}
}
