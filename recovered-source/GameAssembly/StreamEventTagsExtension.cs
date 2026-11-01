public static class StreamEventTagsExtension
{
	public static string GetLocalizedRequirement(this InteractionMemoryEventEnum tag)
	{
		string text = "StreamEventRequirement_";
		switch (tag)
		{
		case InteractionMemoryEventEnum.Insert:
		case InteractionMemoryEventEnum.Pullout:
		case InteractionMemoryEventEnum.CumOutside:
		case InteractionMemoryEventEnum.CumInside:
		case InteractionMemoryEventEnum.CumInsideAnal:
		case InteractionMemoryEventEnum.CumInsideMouth:
		case InteractionMemoryEventEnum.GotFlacid:
			return (text + tag).GetOther();
		default:
			return tag.ToString().ToLower();
		}
	}
}
