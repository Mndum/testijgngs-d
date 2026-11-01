using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailWebsite : Website
{
	[SerializeField]
	private GameObject _email;

	[SerializeField]
	private SortableItemList _emailList;

	private SortableItemList.ItemListAdapter<NewEmail> _emailListAdapter;

	[SerializeField]
	private TextMeshProUGUI _noEmailsText;

	[SerializeField]
	private TextMeshProUGUI _headerStatusText;

	[SerializeField]
	private Button _deleteSpamEmails;

	private void DeleteButtonClicked(NewEmail email)
	{
		UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.bangdotic_email_DeletePrompt, () =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.RemoveEmail(email);
			_emailListAdapter.UpdateAll();
		});
	}

	private void DeleteSpamButtonClicked()
	{
		UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.bangdotic_DeleteSpamPrompt, () =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.RemoveOpenedSpamEmails();
			_emailListAdapter.UpdateAll();
		});
	}

	public void FocusOnItem(int i)
	{
		_emailListAdapter.Focus(i);
	}

	public override IEnumerator Open()
	{
		_headerStatusText.text = LOC.UI.bangdotic_EmailStatusHeader("Anon");
		_deleteSpamEmails.onClick.SetListener(DeleteSpamButtonClicked);
		_emailListAdapter = _emailList.SetAdapter(DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.VisibleEmails));
		_emailListAdapter.ItemUpdatedAction = (GameObject o, NewEmail email, int i, bool focused) =>
		{
			if (o.TryGetComponent<EmailDisplay>(out var component))
			{
				component.Current = email;
			}
		};
		_emailListAdapter.ItemFocusedAction = (NewEmail email, int i) =>
		{
			Transform transform = _email.transform;
			TextMeshProUGUI component = transform.Find("Topic").GetComponent<TextMeshProUGUI>();
			TextMeshProUGUI component2 = transform.Find("Sender").GetComponent<TextMeshProUGUI>();
			TextMeshProUGUI component3 = transform.Find("Content").GetComponent<TextMeshProUGUI>();
			Button component4 = transform.Find("Delete Email").GetComponent<Button>();
			if (email != null)
			{
				component.text = LOC.UI.bangdotic_email_Topic(email.Topic);
				component2.text = LOC.UI.bangdotic_email_From(email.Sender);
				component3.text = email.Text;
				component4.interactable = true;
				component4.onClick.SetListener(() =>
				{
					DeleteButtonClicked(email);
				});
				if (!email.read)
				{
					email.ReadEmail();
					_emailListAdapter.UpdateButton(i);
				}
			}
			else
			{
				component.text = LOC.UI.bangdotic_email_NoEmailSelected;
				component2.text = "";
				component3.text = "";
				component4.interactable = false;
				component4.onClick.RemoveAllListeners();
			}
		};
		_emailListAdapter.OnAfterUpdated = (int count) =>
		{
			_noEmailsText.gameObject.SetActive(count <= 0);
		};
		_emailList.UpdateAll();
		DisableAllChildren();
		yield return WaitForSecondCache.Seconds0_1;
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
