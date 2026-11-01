public class JoinUsAuthor : IKey<string>
{
	public string Key { get; }

	public JoinUsAuthor(string key)
	{
		Key = key;
	}

	public string GetAuthorLocalizedString()
	{
		return JoinUsBlogPrefabManager.GetAuthorLocalizedString(Key);
	}
}
