using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class JoinUsBlogWebsite : JoinUsWebsiteBase
{
	[SerializeField]
	private SortableItemList _commentItemList;

	[SerializeField]
	private JoinUsItemDisplay _itemPrefab;

	[SerializeField]
	private TextMeshProUGUI _mainPostTopText;

	[SerializeField]
	private TextMeshProUGUI _mainPostBottomText;

	[SerializeField]
	private JoinUsItemScoreDisplay mainPostScoreDisplay;

	private string _selectedBlogEntryId;

	public const string BlogIdArgument = "EntryId";

	private void ShowMainPost(JoinUsBlog blog)
	{
		JoinUsBlogItem mainPost = blog.MainPost;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLineStringDisplay(LOC.UI.Blog_WrittenBy, mainPost.Author.GetAuthorLocalizedString());
			stringBuilder.AppendLineStringDisplay(LOC.UI.Blog_Topic, blog.GetTopicLocalizedString());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(mainPost.GetTextLocalizedString());
			stringBuilder.AppendLine();
			_mainPostTopText.SetText(stringBuilder);
			stringBuilder.Clear();
			mainPostScoreDisplay.DisplayScore(mainPost);
			stringBuilder.TMPAppendHeaderSizeTag();
			stringBuilder.Append(LOC.UI.Blog_CommentsHeader);
			stringBuilder.Append(" (");
			stringBuilder.Append(blog.CommentCount);
			stringBuilder.Append("):");
			_mainPostBottomText.SetText(stringBuilder);
		}
	}

	private void OpenBlog(JoinUsBlog blog)
	{
		ShowMainPost(blog);
		SortableItemList.ItemListAdapter<JoinUsBlogItem> itemListAdapter = _commentItemList.SetAdapter(blog.GetComments().ToFrozenDataSource());
		itemListAdapter.ItemUpdatedAction = (GameObject go, JoinUsBlogItem item, int i, bool focused) =>
		{
			go.GetComponent<JoinUsItemDisplay>().SetEntry(item);
		};
		itemListAdapter.UpdateAll();
	}

	public void OnDestroy()
	{
		GameVariables.Current.joinUsBlogManager.ClearUnnecessary();
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
		if (JoinUsBlogPrefabManager.TryGetBlogByEntryId(_selectedBlogEntryId, out var foundBlog) && foundBlog.CanBeOpened())
		{
			if (GameVariables.Current.joinUsBlogManager.GetLoginStatus() != JoinUsBlogManager.LoginStatusEnum.LoggedIn)
			{
				BrowserPopup.ChangeWebsite(foundBlog.GetEntryLoginUrl());
				yield break;
			}
			SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
			OpenBlog(foundBlog);
			BrowserPopup.FinishLoading();
		}
		else
		{
			BrowserPopup.ChangeWebsite("patriciascookingblog.food/main");
		}
	}
}
