using UnityEngine;

[CreateAssetMenu(fileName = "DebugSO", menuName = "SO Singleton/DebugSO", order = 1)]
public class DebugSO : SingletonScriptableObject<DebugSO>
{
	public enum DebugDoAfterLoadEnum
	{
		MainMenu_NormalGameStart,
		LoadDebugSave,
		Missionary,
		Doggy,
		Blowjob,
		Cowgirl,
		Cuddle,
		PC,
		Browser,
		CockTrackts,
		BotLead,
		ClothierWithBot,
		Cooking,
		Fishing,
		GoOutsidePrompt,
		BotStatusApp,
		Equipment,
		Shower
	}

	[field: SerializeField]
	public DebugDoAfterLoadEnum DoAfterLoad { get; set; }

	[field: SerializeField]
	public string AfterLoadData { get; set; }

	public bool IsRunningPlaytest { get; private set; }

	public void ResetDebugVars()
	{
		DoAfterLoad = DebugDoAfterLoadEnum.MainMenu_NormalGameStart;
		AfterLoadData = "";
	}

	public void SetUpUnitPlaytest()
	{
		IsRunningPlaytest = true;
	}

	private void OnEnable()
	{
		IsRunningPlaytest = false;
	}
}
