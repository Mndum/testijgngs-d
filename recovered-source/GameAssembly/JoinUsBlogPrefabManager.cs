using System.Linq;
using System.Reflection;
using StoryConditions;

public static class JoinUsBlogPrefabManager
{
	private const string AuthorPrefix = "Author";

	private const string BlogPrefix = "Blog";

	private const string PostPrefix = "Post";

	private const string TopicPrefix = "Topic";

	public const string JoinUsUrl = "patriciascookingblog.food";

	public const string JoinUsLoginUrl = "patriciascookingblog.food/login";

	public const string JoinUsBlogUrl = "patriciascookingblog.food/blog";

	public const string JoinUsMainUrl = "patriciascookingblog.food/main";

	public static readonly JoinUsAuthor Author_Admin;

	public static readonly JoinUsAuthor Author_WhiteKing;

	public static readonly JoinUsAuthor Author_Valgal;

	public static readonly JoinUsAuthor Author_MarcusSmith37721;

	public static readonly JoinUsAuthor Author_DAK1124;

	public static readonly JoinUsAuthor Author_DAK1125;

	public static readonly JoinUsAuthor Author_TommiiJ;

	public static readonly JoinUsAuthor Author_JuiceMaster;

	public static readonly JoinUsAuthor Author_CuteCocoNut;

	public static readonly JoinUsAuthor Author_AbusedTenant;

	public static readonly JoinUsAuthor Author_HucpaTan;

	public static readonly JoinUsAuthor Author_DagONE;

	public static readonly JoinUsAuthor Author_AiijinAnimeLover;

	public static readonly JoinUsAuthor Author_NorfFcHooligan;

	public static readonly JoinUsAuthor Author_Jantey;

	public static readonly JoinUsAuthor Author_Holopeanoo;

	public static readonly JoinUsAuthor Author_Donkeyman123;

	public static readonly JoinUsAuthor Author_Geebah;

	public static readonly JoinUsAuthor Author_KissMaster;

	public static readonly JoinUsAuthor Author_ShadowPawUwU;

	public static readonly JoinUsAuthor Author_Galgamesh;

	public static readonly JoinUsAuthor Author_LittleRichard;

	public static readonly JoinUsAuthor Author_PeePooLover;

	public static readonly JoinUsAuthor Author_gravymybeloved;

	public static readonly JoinUsAuthor Author_user214125126373473;

	public static readonly JoinUsBlog Blog1;

	public static readonly JoinUsBlog Blog2;

	public static readonly JoinUsBlog Blog3;

	public static readonly JoinUsBlog Blog4;

	public static readonly JoinUsBlog BlogBigDaddy1;

	public static readonly JoinUsBlog BlogBigDaddy2;

	public static readonly JoinUsBlog BlogBigDaddy3;

	public static readonly JoinUsBlog BlogRichard1;

	public static readonly JoinUsBlog BlogRichard2;

	private static JoinUsBlog[] _blogs;

	private static JoinUsAuthor[] _allAuthors;

	public static JoinUsBlog[] AllBlogs => _blogs ?? (_blogs = (from x in typeof(JoinUsBlogPrefabManager).GetFields(BindingFlags.Static | BindingFlags.Public)
		where x.FieldType == typeof(JoinUsBlog)
		select (JoinUsBlog)x.GetValue(null)).ToArray());

	public static JoinUsAuthor[] AllAuthors => _allAuthors ?? (_allAuthors = (from x in typeof(JoinUsBlogPrefabManager).GetFields(BindingFlags.Static | BindingFlags.Public)
		where x.FieldType == typeof(JoinUsAuthor)
		select (JoinUsAuthor)x.GetValue(null)).ToArray());

	public static string GetAuthorLocalizedStringKey(string authorKey)
	{
		return "Author_" + authorKey;
	}

	public static string GetAuthorLocalizedString(string authorKey)
	{
		return LOC.Blog.GS(GetAuthorLocalizedStringKey(authorKey));
	}

	public static string GetPostLocalizedStringKey(string blogKey, int postIndex)
	{
		return string.Format("{0}_{1}_{2}_{3}", "Blog", blogKey, "Post", postIndex);
	}

	public static string GetPostLocalizedString(string blogKey, int postIndex)
	{
		return LOC.Blog.GS(GetPostLocalizedStringKey(blogKey, postIndex));
	}

	public static string GetTopicLocalizedStringKey(string blogKey)
	{
		return "Blog_" + blogKey + "_Topic";
	}

	public static string GetTopicLocalizedString(string blogKey)
	{
		return LOC.Blog.GS(GetTopicLocalizedStringKey(blogKey));
	}

	public static bool TryGetBlogByKey(string key, out JoinUsBlog foundBlog)
	{
		JoinUsBlog[] allBlogs = AllBlogs;
		foreach (JoinUsBlog joinUsBlog in allBlogs)
		{
			if (joinUsBlog.Key == key)
			{
				foundBlog = joinUsBlog;
				return true;
			}
		}
		foundBlog = null;
		return false;
	}

	public static bool TryGetBlogByEntryId(string entryId, out JoinUsBlog foundBlog)
	{
		JoinUsBlog[] allBlogs = AllBlogs;
		foreach (JoinUsBlog joinUsBlog in allBlogs)
		{
			if (joinUsBlog.BlogEntryId == entryId)
			{
				foundBlog = joinUsBlog;
				return true;
			}
		}
		foundBlog = null;
		return false;
	}

	static JoinUsBlogPrefabManager()
	{
		Author_Admin = new JoinUsAuthor("Admin");
		Author_WhiteKing = new JoinUsAuthor("WhiteKing");
		Author_Valgal = new JoinUsAuthor("Valgal");
		Author_MarcusSmith37721 = new JoinUsAuthor("MarcusSmith37721");
		Author_DAK1124 = new JoinUsAuthor("DAK1124");
		Author_DAK1125 = new JoinUsAuthor("DAK1125");
		Author_TommiiJ = new JoinUsAuthor("TommiiJ");
		Author_JuiceMaster = new JoinUsAuthor("JuiceMaster");
		Author_CuteCocoNut = new JoinUsAuthor("CuteCocoNut");
		Author_AbusedTenant = new JoinUsAuthor("AbusedTenantMCAccount");
		Author_HucpaTan = new JoinUsAuthor("HucpaTan");
		Author_DagONE = new JoinUsAuthor("DagONE");
		Author_AiijinAnimeLover = new JoinUsAuthor("AiijinAnimeLover");
		Author_NorfFcHooligan = new JoinUsAuthor("NorfFcHooligan");
		Author_Jantey = new JoinUsAuthor("Jantey");
		Author_Holopeanoo = new JoinUsAuthor("Holopeanoo");
		Author_Donkeyman123 = new JoinUsAuthor("Donkeyman123");
		Author_Geebah = new JoinUsAuthor("Geebah");
		Author_KissMaster = new JoinUsAuthor("KissMaster");
		Author_ShadowPawUwU = new JoinUsAuthor("ShadowPawUwU");
		Author_Galgamesh = new JoinUsAuthor("Galgamesh");
		Author_LittleRichard = new JoinUsAuthor("Little_Richard");
		Author_PeePooLover = new JoinUsAuthor("PeePooLover");
		Author_gravymybeloved = new JoinUsAuthor("gravymybeloved");
		Author_user214125126373473 = new JoinUsAuthor("user214125126373473");
		JoinUsBlog.Builder builder = new JoinUsBlog.Builder("Blog1", "d6265c23de0a90076e36e94e8aefaa28", Author_Admin, 2561, StoryCondition.FlagSet("joinusemail3"));
		builder.ReplyToMainPost(1000, Author_WhiteKing, 356);
		builder.ReplyToMainPost(2000, Author_Valgal, 231);
		builder.ReplyToMainPost(3000, Author_MarcusSmith37721, -462).MarkLastAsAccountDeleted().ReplyToLast(Author_DAK1124, 13);
		builder.ReplyToMainPost(4000, Author_TommiiJ, 146).ReplyToLast(Author_Admin, 253).ReplyToLast(Author_TommiiJ, -15)
			.ReplyToLast(Author_Admin, 52);
		builder.ReplyToMainPost(5000, Author_JuiceMaster, 24);
		builder.ReplyToMainPost(6000, Author_CuteCocoNut, -16).ReplyToLast(Author_WhiteKing, 57).ReplyToLast(Author_CuteCocoNut, -5)
			.ReplyToLast(Author_DAK1124, 23);
		Blog1 = builder.Build();
		JoinUsBlog.Builder builder2 = new JoinUsBlog.Builder("Blog2", "e7cfe8674c0f19879144a254f2c3fc0a", Author_Admin, 2203, StoryCondition.FlagSet("joinusemail4"));
		builder2.ReplyToMainPost(1000, Author_HucpaTan, 501).ReplyToLast(Author_Admin, 251).ReplyToLast(Author_DagONE, 35);
		builder2.ReplyToMainPost(2000, Author_WhiteKing, 105).ReplyToLast(Author_DAK1124, -15).ReplyToLast(Author_WhiteKing, 31)
			.ReplyToLast(Author_DAK1124, 5);
		builder2.ReplyToMainPost(3000, Author_Valgal, 172);
		builder2.ReplyToMainPost(4000, Author_TommiiJ, -152).ReplyToLast(Author_Admin, 173).ReplyToLast(Author_AiijinAnimeLover, -16);
		builder2.ReplyToMainPost(5000, Author_NorfFcHooligan, -127).ReplyToLast(Author_Jantey, 78).ReplyToLast(Author_NorfFcHooligan, -33);
		builder2.ReplyToMainPost(6000, Author_JuiceMaster, 52).ReplyToLast(Author_Admin, 34).ReplyToLast(Author_JuiceMaster, 7)
			.ReplyToLast(Author_Admin, 0)
			.ReplyToLast(Author_JuiceMaster, -1);
		builder2.ReplyToMainPost(7000, Author_Holopeanoo, -2).MarkLastAsAccountBanned();
		Blog2 = builder2.Build();
		JoinUsBlog.Builder builder3 = new JoinUsBlog.Builder("Blog3", "32bb2e221381a5fe3fb9a3672d485ff3", Author_Admin, 2203, StoryCondition.FlagSet("joinuseemail5"));
		builder3.ReplyToMainPost(1000, Author_TommiiJ, -23).ReplyToLast(Author_Admin, 461).ReplyToLast(Author_TommiiJ, -366)
			.ReplyToLastPostParent(Author_Donkeyman123, -62)
			.MarkLastAsAccountDeleted()
			.ReplyToLast(Author_DAK1124, 121);
		builder3.ReplyToMainPost(2000, Author_JuiceMaster, 252).ReplyToLast(Author_NorfFcHooligan, -34);
		builder3.ReplyToMainPost(3000, Author_Valgal, 25).ReplyToLast(Author_Geebah, -2).ReplyToLast(Author_Valgal, 3)
			.ReplyToLast(Author_Geebah, 1);
		builder3.ReplyToMainPost(4000, Author_KissMaster, -23);
		builder3.ReplyToMainPost(5000, Author_WhiteKing, 45).ReplyToLast(Author_Admin, 67);
		Blog3 = builder3.Build();
		JoinUsBlog.Builder builder4 = new JoinUsBlog.Builder("Blog4", "aba62e4300942842e58345db113801ad", Author_Admin, 2412, StoryCondition.FlagSet("joinuseemail6"));
		builder4.ReplyToMainPost(1000, Author_WhiteKing, 435).ReplyToLast(Author_Admin, 521);
		builder4.ReplyToMainPost(2000, Author_TommiiJ, -123).ReplyToLast(Author_Admin, 218).ReplyToLastPostParent(Author_ShadowPawUwU, 53)
			.ReplyToLast(Author_TommiiJ, -23)
			.ReplyToLast(Author_Galgamesh, 52);
		builder4.ReplyToMainPost(3000, Author_DAK1124, 150);
		builder4.ReplyToMainPost(4000, Author_JuiceMaster, 1).ReplyToLast(Author_LittleRichard, 1).ReplyToLast(Author_JuiceMaster, 0);
		builder4.ReplyToMainPost(5000, Author_Valgal, 0).ReplyToLast(Author_Valgal, 0);
		Blog4 = builder4.Build();
		JoinUsBlog.Builder builder5 = new JoinUsBlog.Builder("BlogBigDaddy1", "54d771e79dbfea7790cb9a79e08a28c3", Author_Admin, 2523, StoryCondition.FlagSet("joinus7_bigdaddy1email"));
		builder5.ReplyToMainPost(1000, Author_JuiceMaster, 79).ReplyToLast(Author_DAK1124, 268).ReplyToLastPostParent(Author_Admin, 468)
			.ReplyToLast(Author_JuiceMaster, -34)
			.ReplyToLast(Author_NorfFcHooligan, 64)
			.ReplyToLast(Author_Valgal, 12);
		builder5.ReplyToMainPost(2000, Author_PeePooLover, 48);
		builder5.ReplyToMainPost(3000, Author_WhiteKing, 31);
		builder5.ReplyToMainPost(4000, Author_TommiiJ, 3).MarkLastAsAccountBanned().ReplyToLast(Author_Admin, 35)
			.ReplyToLast(Author_DAK1124, 23)
			.ReplyToLastPostParent(Author_WhiteKing, 2);
		BlogBigDaddy1 = builder5.Build();
		JoinUsBlog.Builder builder6 = new JoinUsBlog.Builder("BlogBigDaddy2", "bc4138258d7b38136031cf9c9594967e", Author_Admin, 2296, StoryCondition.FlagSet("joinus7_bigdaddy4email"));
		builder6.ReplyToMainPost(1000, Author_WhiteKing, 341).ReplyToLast(Author_NorfFcHooligan, 43).ReplyToLast(Author_JuiceMaster, 3)
			.ReplyToLastPostParentParent(Author_JuiceMaster, 64);
		builder6.ReplyToMainPost(2000, Author_DAK1124, 4);
		builder6.ReplyToMainPost(3000, Author_Valgal, 142).ReplyToLast(Author_HucpaTan, 46).ReplyToLast(Author_gravymybeloved, 29);
		BlogBigDaddy2 = builder6.Build();
		JoinUsBlog.Builder builder7 = new JoinUsBlog.Builder("BlogBigDaddy3", "c21b4f30afec8dad6e26dabd468d4a6f", Author_Admin, 659, StoryCondition.FlagSet("joinus7_bigdaddy9email"));
		builder7.ReplyToMainPost(1000, Author_JuiceMaster, 427).MarkLastAsAccountBanned().ReplyToLast(Author_Admin, -263)
			.ReplyToLast(Author_JuiceMaster, 179)
			.MarkLastAsAccountBanned()
			.ReplyToLastPostParent(Author_WhiteKing, 162)
			.ReplyToLast(Author_Admin, -112);
		builder7.ReplyToMainPost(2000, Author_Valgal, -58).MarkLastAsAccountBanned().ReplyToLast(Author_Admin, -36)
			.ReplyToLast(Author_Geebah, 0);
		builder7.ReplyToMainPost(3000, Author_DAK1124, -59).MarkLastAsAccountBanned();
		builder7.ReplyToMainPost(4000, Author_NorfFcHooligan, 35);
		builder7.ReplyToMainPost(5000, Author_DagONE, -34);
		BlogBigDaddy3 = builder7.Build();
		JoinUsBlog.Builder builder8 = new JoinUsBlog.Builder("BlogRichard1", "62e409b2d564a93e1f72eca52f7677f6", Author_Admin, 2522, StoryCondition.FlagSet("joinus7_richard1email"));
		builder8.ReplyToMainPost(1000, Author_NorfFcHooligan, 373).ReplyToLast(Author_Jantey, 141);
		builder8.ReplyToMainPost(2000, Author_HucpaTan, -67).ReplyToLast(Author_Admin, 15).ReplyToLast(Author_TommiiJ, -1)
			.ReplyToLast(Author_Admin, 0)
			.ReplyToLast(Author_TommiiJ, 0);
		builder8.ReplyToMainPost(3000, Author_JuiceMaster, 79).ReplyToLast(Author_Admin, 121);
		builder8.ReplyToMainPost(4000, Author_Valgal, -254).MarkLastAsAccountBanned().ReplyToLast(Author_Admin, 245);
		builder8.ReplyToMainPost(5000, Author_DagONE, 17);
		builder8.ReplyToMainPost(6000, Author_DAK1125, -45).MarkLastAsAccountBanned().ReplyToLast(Author_Geebah, 5);
		BlogRichard1 = builder8.Build();
		JoinUsBlog.Builder builder9 = new JoinUsBlog.Builder("BlogRichard2", "0f35464c5953b436d4082b544195f560", Author_Admin, 2522, StoryCondition.FlagSet("joinus7_richard4email"));
		builder9.ReplyToMainPost(1000, Author_Geebah, 326).ReplyToLast(Author_Admin, 121).ReplyToLastPostParent(Author_Jantey, 110);
		builder9.ReplyToMainPost(2000, Author_WhiteKing, 311);
		builder9.ReplyToMainPost(3000, Author_JuiceMaster, 127);
		builder9.ReplyToMainPost(4000, Author_TommiiJ, 41).ReplyToLast(Author_NorfFcHooligan, 89);
		builder9.ReplyToMainPost(5000, Author_DagONE, 41).ReplyToLast(Author_Galgamesh, 23).ReplyToLast(Author_DagONE, -12);
		builder9.ReplyToMainPost(6000, Author_user214125126373473, 12);
		builder9.ReplyToMainPost(7000, Author_HucpaTan, -35).ReplyToLast(Author_AiijinAnimeLover, 10);
		BlogRichard2 = builder9.Build();
	}
}
