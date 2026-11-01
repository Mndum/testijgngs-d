using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Website : MonoBehaviour
{
	public bool listed = true;

	protected BrowserPopup BrowserPopup;

	[field: SerializeField]
	public GameUrl GameUrl { get; private set; }

	public GameUrlWithArguments CurrentGameUrl { get; protected set; }

	protected void DisableAllChildren()
	{
		foreach (Transform item in transform)
		{
			item.gameObject.SetActive(value: false);
		}
	}

	protected IEnumerator SlowlyEnableAllChildren(List<Transform> ignoreList)
	{
		List<Transform> list = (from Transform _ in transform
			where !ignoreList.Contains(_)
			orderby Guid.NewGuid()
			select _).ToList();
		foreach (Transform item in list)
		{
			item.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.25f));
		}
	}

	protected IEnumerator SlowlyEnableAllChildren()
	{
		List<Transform> list = (from Transform _ in transform
			orderby Guid.NewGuid()
			select _).ToList();
		foreach (Transform item in list)
		{
			item.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.25f));
		}
	}

	public virtual void ProcessUrl(GameUrlWithArguments gameUrlWithArguments)
	{
		gameUrlWithArguments.Arguments.Clear();
		CurrentGameUrl = gameUrlWithArguments;
	}

	public virtual bool IsWebsiteUnlocked()
	{
		return true;
	}

	public string GetBrowserTitle()
	{
		return CurrentGameUrl.Assembled;
	}

	public virtual IEnumerator Open()
	{
		DisableAllChildren();
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}

	public void SetBrowserScreen(BrowserPopup browserPopup)
	{
		BrowserPopup = browserPopup;
	}

	public void ChangeWebsite(string newwebsite)
	{
		BrowserPopup.ChangeWebsite(newwebsite);
	}
}
