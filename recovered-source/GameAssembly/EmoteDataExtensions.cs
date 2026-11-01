using System.Collections.Generic;
using System.Linq;

public static class EmoteDataExtensions
{
	public static string ToEmoteDataString(this IEnumerable<EmoteData> emoteData, string character)
	{
		return string.Join('+', emoteData.Select((EmoteData t) => character + "." + t.ToString()));
	}

	public static string ToEmoteDataString(this IEnumerable<EmoteData> emoteData)
	{
		return string.Join('+', emoteData.Select((EmoteData t) => t.ToString()));
	}
}
