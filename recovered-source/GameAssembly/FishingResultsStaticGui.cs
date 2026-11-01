using System;
using System.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingResultsStaticGui : StaticGuiBase
{
	private StringBuilder stringBuilder = new StringBuilder();

	private bool _speedUp;

	private Action _onFinished;

	[Header("Auto")]
	[SerializeField]
	private ScrollRect A_Scroll_View;

	[SerializeField]
	private TextMeshProUGUI A_Result_Text;

	[SerializeField]
	private ButtonList A_Bottom_Buttons;

	private async UniTask DisplayAndShake()
	{
		A_Result_Text.SetText(stringBuilder);
		LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
		A_Scroll_View.ScrollToBottom();
		if (!_speedUp)
		{
			R.Sound.Generic_Whoosh_All.GetRandom().Get().Play();
			await (Tween)transform.DOShakePosition(0.2f, 20f, 20);
			await UniTask.Delay(50);
		}
	}

	private async UniTask DisplayResultsTask(FishingResults results)
	{
		using (results)
		{
			stringBuilder.AppendLineHeader(LOC.UI.Fishing_FishingResults);
			if (results.CaughtFishes > 0)
			{
				stringBuilder.AppendLine(LOC.UI.CaughtCountFish(results.CaughtFishes));
				await DisplayAndShake();
			}
			if (results.CaughtItems > 0)
			{
				stringBuilder.AppendLine(LOC.UI.CaughtItemCount(results.CaughtItems));
				await DisplayAndShake();
			}
			if (results.CaughtItems == 0 && results.CaughtFishes == 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(LOC.UI.Fishing_FishingResultsNothing);
				await DisplayAndShake();
			}
			else
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLineSubHeader(LOC.UI.CatchListHeader);
				await DisplayAndShake();
				foreach (var (item, flag) in results.ItemsWithFlags)
				{
					item.GetNameStringBuilder(stringBuilder);
					flag.AppendLocalizedString(stringBuilder);
					stringBuilder.AppendLine();
					await DisplayAndShake();
				}
			}
			IModificationPeriodButtonList modificationPeriod;
			using (A_Bottom_Buttons.DoInitialization(out modificationPeriod))
			{
				ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Alternative);
				buttonListWrapper.SetText(LOC.Polyglot.MENU_LABEL_EXIT);
				buttonListWrapper.OnClick = OnFinished;
			}
		}
	}

	public void Update()
	{
		if (Input.anyKeyDown)
		{
			_speedUp = true;
		}
	}

	private void OnFinished()
	{
		if (_onFinished != null)
		{
			_onFinished();
			_onFinished = null;
		}
	}

	public void DisplayResults(FishingResults results, Action onFinished)
	{
		_onFinished = onFinished;
		_speedUp = false;
		stringBuilder.Clear();
		A_Bottom_Buttons.ResetButtons();
		DisplayResultsTask(results).Forget();
	}
}
