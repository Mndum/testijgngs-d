using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrowserPopup : PrefabPopup<BrowserPopup>
{
	private TextMeshProUGUI _title;

	private Button _nextButton;

	private Button _backButton;

	private Button _refreshButton;

	private TMP_InputField _input;

	private ScrollRect _websiteScrollRect;

	private Transform _websiteHolder;

	private List<string> websiteHistory = new List<string>();

	private CursorSingleton.CursorOverride _cursorOverride;

	private Website _currentWebsite;

	private Coroutine _websiteLoading;

	private RectTransform _refreshButtonImageRectTransform;

	private float _refreshRotation;

	public string CurrentWebsite
	{
		get
		{
			if (websiteHistory.Count != 0)
			{
				return websiteHistory[websiteHistory.Count - 1];
			}
			return null;
		}
	}

	public static string FormatLink(string link, string text = null)
	{
		if (text == null)
		{
			text = link;
		}
		return "<color=blue><u><link=\"" + link + "\">" + text + "</link></u></color>";
	}

	public void Awake()
	{
		_title = transform.Find("Title").GetComponent<TextMeshProUGUI>();
		_title.text = "";
		_nextButton = transform.Find("Next Button").GetComponent<Button>();
		_backButton = transform.Find("Back Button").GetComponent<Button>();
		_refreshButton = transform.Find("Refresh Button").GetComponent<Button>();
		_input = transform.Find("Website Input").GetComponent<TMP_InputField>();
		_websiteScrollRect = transform.Find("Website ScrollView").GetComponent<ScrollRect>();
		_websiteHolder = _websiteScrollRect.transform.Find("ScrollView");
		_refreshButtonImageRectTransform = _refreshButton.transform.GetChild(0).GetComponent<RectTransform>();
		_backButton.onClick.AddListener(PrevWebsite);
		_nextButton.onClick.AddListener(ChangeWebsite);
		_input.onEndEdit.AddListener(ChangeWebsite);
		_refreshButton.onClick.AddListener(() =>
		{
			ChangeWebsite(CurrentWebsite, addToHistory: false);
		});
	}

	public void RemovePreviousWebsite()
	{
		if (_websiteLoading != null)
		{
			StopCoroutine(_websiteLoading);
			_websiteLoading = null;
		}
		if (_websiteHolder == null)
		{
			return;
		}
		_input.text = "";
		foreach (Transform item in _websiteHolder)
		{
			Object.Destroy(item.gameObject);
		}
	}

	public void ChangeWebsite()
	{
		ChangeWebsite(_input.text);
	}

	public void ChangeWebsite(string address)
	{
		ChangeWebsite(address, addToHistory: true);
	}

	public void ChangeWebsite(string address, bool addToHistory)
	{
		if (string.IsNullOrWhiteSpace(address))
		{
			address = "error.ic";
		}
		GameUrlWithArguments address2 = new GameUrlWithArguments(address);
		ChangeWebsite(address2, addToHistory);
	}

	public bool TryGetCurrentWebsiteAs<T>(out T website) where T : Website
	{
		if (_currentWebsite != null && _currentWebsite is T val)
		{
			website = val;
			return true;
		}
		website = null;
		return false;
	}

	public void ChangeWebsite(GameUrlWithArguments address, bool addToHistory = true)
	{
		RemovePreviousWebsite();
		bool flag = SingletonMonoBehaviour<InternetSingleton>.Instance.TryFindWebsite(address, out var websiteSearchData);
		string assembled = websiteSearchData.OutputAddress.Assembled;
		Website website = websiteSearchData.Website;
		_currentWebsite = Object.Instantiate(website, _websiteHolder);
		_websiteScrollRect.content = _currentWebsite.GetComponent<RectTransform>();
		_currentWebsite.SetBrowserScreen(this);
		_currentWebsite.ProcessUrl(websiteSearchData.OutputAddress);
		if (flag && website.listed)
		{
			SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.ReportWebsiteSeen(assembled);
		}
		string assembledWithArguments = websiteSearchData.OutputAddress.AssembledWithArguments;
		_input.text = assembledWithArguments;
		_title.text = _currentWebsite.GetBrowserTitle();
		Debug.Log("Website: " + assembledWithArguments);
		if (addToHistory && CurrentWebsite != assembledWithArguments)
		{
			websiteHistory.Add(assembledWithArguments);
			SyncBackButton();
		}
		CursorSingleton.ContinueOrStartCursorOverride(ref _cursorOverride, CursorSingleton.CursorType.Wait);
		_websiteLoading = StartCoroutine(_currentWebsite.Open());
	}

	public new void OnDisable()
	{
		base.OnDisable();
		CursorSingleton.EndCursorOverride(ref _cursorOverride);
	}

	public void PrevWebsite()
	{
		if (websiteHistory.Count >= 2)
		{
			string address = websiteHistory[websiteHistory.Count - 2];
			websiteHistory.RemoveAt(websiteHistory.Count - 1);
			ChangeWebsite(address, addToHistory: false);
			SyncBackButton();
		}
	}

	public void SyncBackButton()
	{
		_backButton.interactable = websiteHistory.Count > 1;
	}

	public void FinishLoading()
	{
		CursorSingleton.EndCursorOverride(ref _cursorOverride);
		_websiteLoading = null;
	}

	private void OpenInner(string website)
	{
		base.Open();
		ChangeWebsite(website);
		SyncBackButton();
	}

	public override bool CanClose()
	{
		return true;
	}

	public override void Open()
	{
		OpenInner(SingletonMonoBehaviour<InternetSingleton>.Instance.DefaultWebsite);
	}

	public void Open(string website)
	{
		OpenInner(website);
	}

	[UsedInLua]
	public void Refresh()
	{
		ChangeWebsite(CurrentWebsite);
	}

	public void FixedUpdate()
	{
		if (_websiteLoading != null)
		{
			_refreshRotation = Mathf.Repeat(_refreshRotation + 10f, 360f);
			_refreshButtonImageRectTransform.localRotation = Quaternion.Euler(0f, 0f, _refreshRotation);
		}
		else if (!Mathf.Approximately(0f, _refreshRotation))
		{
			if (_refreshRotation > 360f)
			{
				_refreshRotation = 0f;
				_refreshButtonImageRectTransform.localRotation = Quaternion.identity;
			}
			else
			{
				_refreshRotation += 20f;
				_refreshButtonImageRectTransform.localRotation = Quaternion.Euler(0f, 0f, _refreshRotation);
			}
		}
	}

	[UsedInLua]
	public void SelectEmail(int index)
	{
		EmailWebsite componentInChildren = _websiteHolder.GetComponentInChildren<EmailWebsite>();
		if (componentInChildren != null)
		{
			componentInChildren.FocusOnItem(index);
		}
	}

	[UsedInLua]
	public void JoinUsOpenBlogLoginPage(string key)
	{
		if (JoinUsBlogPrefabManager.TryGetBlogByKey(key, out var foundBlog))
		{
			ChangeWebsite(foundBlog.GetEntryLoginUrl());
		}
		else
		{
			Debug.LogError("Blog with key " + key + " not found!");
		}
	}

	[UsedInLua]
	public IEnumerator JoinUsLogin_Enumerator()
	{
		if (!TryGetCurrentWebsiteAs<JoinUsLoginWebsite>(out var website))
		{
			Debug.LogError("Current website isn't a login website!");
			yield break;
		}
		website.LoginButtonClicked();
		yield return new WaitUntil(() => _currentWebsite is JoinUsBlogWebsite);
	}

	[UsedInLua]
	public IEnumerator JoinUsOpenBlog_Enumerator(string key)
	{
		if (!JoinUsBlogPrefabManager.TryGetBlogByKey(key, out var foundBlog))
		{
			Debug.LogError("Blog with key " + key + " not found!");
			yield break;
		}
		ChangeWebsite(foundBlog.GetEntryLoginUrl());
		yield return null;
		yield return null;
		yield return null;
		if (!(_currentWebsite is JoinUsBlogWebsite))
		{
			yield return JoinUsLogin_Enumerator();
		}
	}
}
