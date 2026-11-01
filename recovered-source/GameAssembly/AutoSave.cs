using Fungus;
using UnityEngine;

[CommandInfo("My", "Try Auto Save", "Tries to do an autosave!", 0)]
public class AutoSave : Command
{
	public override void OnEnter()
	{
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.TryMakeAutoSave();
		Continue();
	}

	public override string GetSummary()
	{
		return "Tries to do an autosave.";
	}

	public override Color GetButtonColor()
	{
		return new Color32(250, 50, 50, 255);
	}
}
