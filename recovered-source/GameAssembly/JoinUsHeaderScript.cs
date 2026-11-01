using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinUsHeaderScript : MonoBehaviour
{
	[SerializeField]
	private RectTransform planProgressRect;

	[SerializeField]
	private Slider planProgress;

	[SerializeField]
	private TextMeshProUGUI _loginInfo;

	[SerializeField]
	private Button _logoutButton;

	private void LogoutButtonClicked()
	{
		GameVariables.Current.joinUsBlogManager.Logout();
		gameObject.GetAncestor<BrowserPopup>().ChangeWebsite("patriciascookingblog.food/login");
	}

	public void Awake()
	{
		GameVariables current = GameVariables.Current;
		JoinUsBlogManager.LoginStatusEnum loginStatus = current.joinUsBlogManager.GetLoginStatus();
		if ((uint)loginStatus <= 1u || loginStatus != JoinUsBlogManager.LoginStatusEnum.LoggedIn)
		{
			_loginInfo.text = LOC.UI.Blog_NotLoggedIn;
			planProgressRect.gameObject.SetActive(value: false);
			_logoutButton.gameObject.SetActive(value: false);
		}
		else
		{
			_loginInfo.text = LOC.UI.Blog_LoggedInAs(JoinUsBlogPrefabManager.Author_AbusedTenant.GetAuthorLocalizedString());
			planProgressRect.gameObject.SetActive(value: true);
			planProgress.value = current.joinUsBlogManager.GetPlanProgress();
			_logoutButton.gameObject.SetActive(value: true);
			_logoutButton.onClick.SetListener(LogoutButtonClicked);
		}
	}
}
