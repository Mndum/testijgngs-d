using System.Collections.Generic;
using System.Linq;
using StoryConditions;

public class JoinUsBlog : IKey<string>
{
	public class Builder
	{
		private readonly JoinUsBlog _blog;

		private readonly JoinUsBlogItem _mainPost;

		public Builder(string key, string entryId, JoinUsAuthor author, int mainBlogCommentScore, params StoryCondition[] allowedConditions)
		{
			_blog = new JoinUsBlog(key, author, entryId, allowedConditions);
			_mainPost = new JoinUsBlogItem(0, _blog, author, mainBlogCommentScore);
			_blog._items.Add(_mainPost);
		}

		public Builder ReplyToMainPost(int index, JoinUsAuthor author, int commentScore)
		{
			JoinUsBlogItem item = new JoinUsBlogItem(index, _blog, author, commentScore);
			_blog._items.Add(item);
			return this;
		}

		public Builder MarkLastAsAccountDeleted()
		{
			_blog._items[_blog._items.Count - 1].PostDecorations |= JoinUsBlogItem.SpecialPostDecorations.AccountDeleted;
			return this;
		}

		public Builder MarkLastAsAccountBanned()
		{
			_blog._items[_blog._items.Count - 1].PostDecorations |= JoinUsBlogItem.SpecialPostDecorations.AccountBanned;
			return this;
		}

		public Builder ReplyToLast(JoinUsAuthor author, int commentScore)
		{
			ReplyTo(_blog._items[_blog._items.Count - 1], author, commentScore);
			return this;
		}

		public Builder ReplyToLastPostParent(JoinUsAuthor author, int commentScore)
		{
			ReplyTo(_blog._items[_blog._items.Count - 1].ReplyTo, author, commentScore);
			return this;
		}

		public Builder ReplyToLastPostParentParent(JoinUsAuthor author, int commentScore)
		{
			ReplyTo(_blog._items[_blog._items.Count - 1].ReplyTo.ReplyTo, author, commentScore);
			return this;
		}

		public Builder ReplyTo(JoinUsBlogItem blogItem, JoinUsAuthor author, int commentScore)
		{
			JoinUsBlogItem item = new JoinUsBlogItem(_blog._items[_blog._items.Count - 1].Index + 10, _blog, author, commentScore, blogItem, blogItem.Depth + 1);
			_blog._items.Add(item);
			return this;
		}

		public JoinUsBlog Build()
		{
			return _blog;
		}
	}

	public readonly string BlogEntryId;

	public readonly JoinUsAuthor JoinUsAuthor;

	public readonly StoryCondition[] AllowedConditions;

	private List<JoinUsBlogItem> _items = new List<JoinUsBlogItem>();

	public string Key { get; }

	public JoinUsBlogItem MainPost => _items[0];

	public int CommentCount => _items.Count - 1;

	public JoinUsBlog(string key, JoinUsAuthor joinUsAuthor, string blogEntryId, StoryCondition[] allowedConditions)
	{
		Key = key;
		JoinUsAuthor = joinUsAuthor;
		BlogEntryId = blogEntryId;
		AllowedConditions = allowedConditions;
	}

	public string GetTopicLocalizedString()
	{
		return JoinUsBlogPrefabManager.GetTopicLocalizedString(Key);
	}

	public bool CanBeOpened()
	{
		return AllowedConditions.ConditionsMet();
	}

	public IReadOnlyCollection<JoinUsBlogItem> GetAllItems()
	{
		return _items;
	}

	public IEnumerable<JoinUsBlogItem> GetComments()
	{
		return _items.Skip(1);
	}

	public string GetEntryUrl()
	{
		return new GameUrlWithArguments("patriciascookingblog.food/blog", new Dictionary<string, string> { ["EntryId"] = BlogEntryId }).AssembledWithArguments;
	}

	public string GetEntryLoginUrl()
	{
		return new GameUrlWithArguments("patriciascookingblog.food/login", new Dictionary<string, string> { ["EntryId"] = BlogEntryId }).AssembledWithArguments;
	}
}
