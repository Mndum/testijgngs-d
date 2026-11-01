using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContractDisplay : DataDisplay<CocktractContract>
{
	private class ButtonData
	{
		public string Text;

		public Action OnClick;

		public ButtonData(string text, Action click)
		{
			Text = text;
			OnClick = click;
		}
	}

	[SerializeField]
	private TextMeshProUGUI description;

	[SerializeField]
	private TextMeshProUGUI narration;

	[SerializeField]
	private RectTransform header;

	[SerializeField]
	private LogoDisplay logoDisplay;

	[SerializeField]
	private RectTransform mainScrollViewRectTransform;

	[SerializeField]
	private RectTransform buttonsRectTransform;

	[SerializeField]
	private ItemList buttonItemList;

	private List<ButtonData> _buttonData = new List<ButtonData>();

	private bool _initialized;

	private string _emptyText;

	public void SetEmptyText(string text)
	{
		_emptyText = text;
	}

	private void Init()
	{
		if (_initialized)
		{
			return;
		}
		buttonItemList.ItemCountFunc = () => _buttonData.Count;
		buttonItemList.ItemUpdatedAction = (GameObject go, int index, bool focused) =>
		{
			if (_buttonData.TryGetValue(index, out var value))
			{
				go.GetComponentInChildren<TextMeshProUGUI>().text = value.Text;
			}
		};
		buttonItemList.ItemClickedAction = (int index) =>
		{
			if (_buttonData.TryGetValue(index, out var value))
			{
				value.OnClick?.Invoke();
			}
		};
		buttonItemList.OnAfterUpdated = (int count) =>
		{
			float num = 10f;
			float num2 = 0f;
			if (count == 0)
			{
				buttonsRectTransform.gameObject.SetActive(value: false);
			}
			else
			{
				buttonsRectTransform.gameObject.SetActive(value: true);
				num2 += buttonsRectTransform.sizeDelta.y + num;
			}
			mainScrollViewRectTransform.anchoredPosition = new Vector2(0f, num2 / 2f);
			mainScrollViewRectTransform.sizeDelta = new Vector2(0f, 0f - num2);
		};
		_initialized = true;
	}

	protected override void SetData(CocktractContract newValue)
	{
		Init();
		_current = newValue;
		if (_current != null)
		{
			header.gameObject.SetActive(value: true);
			description.text = _current.GenerateDescription();
			narration.text = _current.GetNarration();
			if (logoDisplay != null)
			{
				logoDisplay.Current = _current.Partner;
			}
			_buttonData.Clear();
			if (_current.Status == CocktractContract.ContractStatus.Available)
			{
				if (!GameVariables.Current.cocktractManager.HasActiveContract)
				{
					_buttonData.Add(new ButtonData(LOC.UI.cocktracts_AcceptContract, () =>
					{
						UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Contract_AreYouSureYouWantToAccept + "\n" + LOC.UI.Contract_RememberOnlyOne + "\n\n" + _current.GenerateDescription(), () =>
						{
							GameVariables.Current.cocktractManager.AcceptContract(_current);
							UiOverlay.Instance.GetCurrentlyOpenedBrowser().ChangeWebsite("cocktracts.ic");
						});
					}));
				}
				_buttonData.Add(new ButtonData(LOC.UI.cocktracts_RejectContract, () =>
				{
					UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Contract_AreYouSureYouWantToReject + "\n" + LOC.UI.Contract_ThisWillAngerThePartner(_current.Partner.Name) + "\n\n" + _current.GenerateDescription(), () =>
					{
						GameVariables.Current.cocktractManager.RejectContract(_current);
						UiOverlay.Instance.GetCurrentlyOpenedBrowser().ChangeWebsite("cocktracts.ic/available");
					});
				}));
			}
			else if (_current.Status == CocktractContract.ContractStatus.Accepted)
			{
				if (!_current.Completed)
				{
					_buttonData.Add(new ButtonData(LOC.UI.cocktracts_AbandonContract, () =>
					{
						UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Contract_AreYouSureYouWantToAbandonCurrent + "\n" + LOC.UI.Contract_ThisWillAngerThePartner(_current.Partner.Name) + "\n" + (_current.Possible ? LOC.UI.Contract_CompletionPossible : LOC.UI.Contract_FailedPrompt), () =>
						{
							GameVariables.Current.cocktractManager.AbandonContract(_current);
							SetData(null);
						});
					}));
				}
				else
				{
					_buttonData.Add(new ButtonData(LOC.UI.cocktracts_CompleteContract, () =>
					{
						UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, LOC.UI.Contract_CompletePrompt + "\n\n" + _current.GenerateDescription(), () =>
						{
							GameVariables.Current.cocktractManager.CompleteContract(_current);
							SetData(null);
						});
					}));
				}
			}
			buttonItemList.UpdateAll();
		}
		else
		{
			header.gameObject.SetActive(value: false);
			TextMeshProUGUI textMeshProUGUI = narration;
			string text = (description.text = (string.IsNullOrEmpty(_emptyText) ? LOC.UI.UI_Error : _emptyText));
			textMeshProUGUI.text = text;
			if (logoDisplay != null)
			{
				logoDisplay.Current = null;
			}
			_buttonData.Clear();
			buttonItemList.UpdateAll();
		}
	}
}
