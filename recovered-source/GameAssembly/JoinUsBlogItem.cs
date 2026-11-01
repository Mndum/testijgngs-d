using System;

public class JoinUsBlogItem
{
	[Flags]
	public enum SpecialPostDecorations
	{
		AccountDeleted = 1,
		AccountBanned = 2
	}

	public readonly int Index;

	public readonly JoinUsBlog MainBlog;

	public readonly JoinUsAuthor Author;

	public bool IsMainPost => Index == 0;

	public int CommentScore { get; private set; }

	public JoinUsBlogItem ReplyTo { get; private set; }

	public int Depth { get; private set; }

	public SpecialPostDecorations PostDecorations { get; set; }

	public JoinUsBlogItem(int index, JoinUsBlog mainBlog, JoinUsAuthor author, int commentScore, JoinUsBlogItem joinUsBlogItem = null, int depth = 0)
	{
		Index = index;
		MainBlog = mainBlog;
		Author = author;
		Depth = depth;
		CommentScore = commentScore;
		ReplyTo = joinUsBlogItem;
	}

	public string GetTextLocalizedString()
	{
		return JoinUsBlogPrefabManager.GetPostLocalizedString(MainBlog.Key, Index);
	}
}
