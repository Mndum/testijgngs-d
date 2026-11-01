using System.Collections.Generic;

public class ModdableLocalizedConversation : LocalizedConversation
{
	public List<LocalizedStringReferenceModdable> localizedLines = new List<LocalizedStringReferenceModdable>();

	public override IEnumerable<LocalizedStringReference> GetLocalizedLines()
	{
		return localizedLines;
	}

	public override void CreateLinesAtEnd(int count)
	{
		for (int i = 0; i < count; i++)
		{
			localizedLines.Add(new LocalizedStringReferenceModdable());
		}
	}
}
