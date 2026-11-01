using Fungus;
using UnityEngine;

[CommandInfo("My", "Create New Email", "Creates email", 0)]
public class CreateNewEmail : Command
{
	public NewEmail email;

	public override void OnEnter()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddEmail(email.Clone());
		Continue();
	}

	public override string GetSummary()
	{
		return "Creates new email";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}
}
