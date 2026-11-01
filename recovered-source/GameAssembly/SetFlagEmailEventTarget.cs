public class SetFlagEmailEventTarget : EmailEventTarget
{
	public readonly string Flag;

	public SetFlagEmailEventTarget(string flag)
	{
		Flag = flag;
	}

	public override bool Start(NewEmail parent)
	{
		GameVariables.Current.AddFlag(Flag);
		return false;
	}
}
