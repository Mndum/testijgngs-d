using System;

public static class DialogueIdExtensions
{
	public static readonly Stage3VanillaDialogueIds[] AllStage3Dialogues = (Stage3VanillaDialogueIds[])Enum.GetValues(typeof(Stage3VanillaDialogueIds));

	public static int GetLastPossibleIndex(this Stage3VanillaDialogueIds id)
	{
		int num = Array.IndexOf(AllStage3Dialogues, id) + 1;
		if (AllStage3Dialogues.Length != num)
		{
			return (int)AllStage3Dialogues[num];
		}
		return (int)(id + 1000);
	}

	public static GameId GetGameId(this Stage3VanillaDialogueIds id, int index)
	{
		return GameId.CreateVanilla((int)(id + index));
	}

	public static GameId GetGameId(this Stage2VanillaDialogueIds id, int index)
	{
		return GameId.CreateVanilla((int)(id + index));
	}

	public static GameId GetGameId(this Stage1VanillaDialogueIds id, int index)
	{
		return GameId.CreateVanilla((int)(id + index));
	}
}
