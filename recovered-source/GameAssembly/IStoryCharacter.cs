using System.Collections.Generic;

public interface IStoryCharacter
{
	void SetEmote(IEnumerable<EmoteData> emoteData);

	Result IsValidEmote(EmoteData emote);

	void SetDefaultEmote(bool instantly);
}
