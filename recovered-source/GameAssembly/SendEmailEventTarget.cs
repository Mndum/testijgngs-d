public class SendEmailEventTarget : RuntimeEventTarget
{
	private string EmailId;

	public SendEmailEventTarget(string emailId)
	{
		EmailId = emailId;
	}

	public override bool Start(RuntimeEvent parent)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddEmail(EmailPrefabManager.GetEmail(EmailId));
		return false;
	}

	public override string ToString()
	{
		return "EmailId: \"" + EmailId + "\"";
	}
}
