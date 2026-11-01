using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class AdScript : MonoBehaviour
{
	public List<string> possibleMessages;

	public List<string> possibleRedirects;

	public float comeBackTimeMin = 5f;

	public float comeBackTimeMax = 10f;

	private float _comeBackTimer;

	private float _comeBackTime;

	[ReadOnly]
	public Button closeButton;

	[ReadOnly]
	public Button adButton;

	[ReadOnly]
	public Image image;

	[ReadOnly]
	public TextMeshProUGUI tmp;

	public void OnValidate()
	{
		adButton = GetComponent<Button>();
		adButton.transition = Selectable.Transition.None;
		closeButton = GetComponentInChildren<Button>();
		image = GetComponent<Image>();
		tmp = GetComponentInChildren<TextMeshProUGUI>();
	}

	public void HideAd()
	{
		if (image != null)
		{
			image.enabled = false;
		}
		foreach (Transform item in transform)
		{
			item.gameObject.SetActive(value: false);
		}
		_comeBackTimer = 0f;
		_comeBackTime = Random.Range(comeBackTimeMin, comeBackTimeMax);
	}

	public void ShowAd()
	{
		if (adButton != null)
		{
			adButton.onClick.RemoveAllListeners();
			adButton.onClick.AddListener(() =>
			{
				if (possibleRedirects.Any())
				{
					gameObject.GetAncestor<BrowserPopup>().ChangeWebsite(possibleRedirects.GetRandom());
				}
			});
		}
		if (image != null)
		{
			image.enabled = true;
		}
		foreach (Transform item in transform)
		{
			item.gameObject.SetActive(value: true);
		}
		if (tmp != null)
		{
			tmp.raycastTarget = false;
			string inputtext = (possibleMessages.Any() ? possibleMessages.GetRandom() : "OH NOOOOOOOO!");
			tmp.text = DialogueChain.CreateRawComment(inputtext).GetText();
		}
	}

	private void OnEnable()
	{
		ShowAd();
	}

	private void FixedUpdate()
	{
		if (!image.enabled)
		{
			_comeBackTimer += Time.fixedDeltaTime;
			if (_comeBackTimer > _comeBackTime)
			{
				ShowAd();
			}
		}
	}
}
