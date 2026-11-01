using Fungus;

public static class CharacterExtensions
{
	public static bool IsBotCharacter(this ISayDialogCharacter character)
	{
		return character == SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.BotLive2DCharacter;
	}
}
