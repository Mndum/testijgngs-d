using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinUsLoginWebsite : JoinUsWebsiteBase
{
	[SerializeField]
	private TextMeshProUGUI _loginInput;

	[SerializeField]
	private TextMeshProUGUI _passwordInput;

	[SerializeField]
	private Button _loginButton;

	private CancellationTokenSource _loginCancellationToken;

	private string _selectedBlogEntryId;

	public const string BlogIdArgument = "EntryId";

	private async UniTask DoTheLogin(CancellationToken cancellationToken)
	{
		string authorLocalizedString = JoinUsBlogPrefabManager.Author_AbusedTenant.GetAuthorLocalizedString();
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			string text = authorLocalizedString;
			foreach (char value in text)
			{
				stringBuilder.Append(value);
				_loginInput.SetText(stringBuilder);
				await UniTask.Delay(Random.Range(25, 100), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			await UniTask.Delay(Random.Range(100, 200), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			stringBuilder.Clear();
			text = "**********";
			foreach (char value2 in text)
			{
				stringBuilder.Append(value2);
				_passwordInput.SetText(stringBuilder);
				await UniTask.Delay(Random.Range(25, 100), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			await UniTask.Delay(Random.Range(300, 500), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			GameVariables.Current.joinUsBlogManager.Login();
			if (CurrentGameUrl.Arguments.Any())
			{
				BrowserPopup.ChangeWebsite(new GameUrlWithArguments("patriciascookingblog.food/blog", CurrentGameUrl.Arguments));
			}
			else
			{
				BrowserPopup.ChangeWebsite("patriciascookingblog.food/main");
			}
		}
	}

	private void KillAll()
	{
		if (_loginCancellationToken != null)
		{
			_loginCancellationToken.Cancel();
			_loginCancellationToken.Dispose();
			_loginCancellationToken = null;
		}
	}

	public void LoginButtonClicked()
	{
		if (_loginCancellationToken == null)
		{
			_loginCancellationToken = new CancellationTokenSource();
			DoTheLogin(_loginCancellationToken.Token).Forget();
		}
	}

	private void OnDestroy()
	{
		KillAll();
	}

	public override void ProcessUrl(GameUrlWithArguments gameUrlWithArguments)
	{
		if (gameUrlWithArguments.Arguments.TryGetValue("EntryId", out var value))
		{
			_selectedBlogEntryId = value;
		}
		CurrentGameUrl = gameUrlWithArguments;
	}

	public override IEnumerator Open()
	{
		if (GameVariables.Current.joinUsBlogManager.GetLoginStatus() == JoinUsBlogManager.LoginStatusEnum.LoggedIn)
		{
			if (JoinUsBlogPrefabManager.TryGetBlogByEntryId(_selectedBlogEntryId, out var foundBlog) && foundBlog.CanBeOpened())
			{
				BrowserPopup.ChangeWebsite(foundBlog.GetEntryUrl());
			}
			else
			{
				BrowserPopup.ChangeWebsite("patriciascookingblog.food/main");
			}
		}
		else
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
			_loginButton.onClick.SetListener(LoginButtonClicked);
			BrowserPopup.FinishLoading();
		}
		yield break;
	}
}
