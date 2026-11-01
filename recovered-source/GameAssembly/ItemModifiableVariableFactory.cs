public class ItemModifiableVariableFactory
{
	public static ItemModifiableVariableIntBackedBool CreateInstanceBoolBackedVariable(string variableName, SimpleLocalizedString localizedString, bool defaultValue = false)
	{
		if (localizedString == null)
		{
			localizedString = SimpleLocalizedString.CreateInstanceReplacement(variableName);
		}
		return new ItemModifiableVariableIntBackedBool(variableName, localizedString, defaultValue);
	}

	public static ItemModifiableVariableFloat01Percent CreateInstanceFloatVariable(string variableName, SimpleLocalizedString localizedString, float defaultValue = 0f)
	{
		if (localizedString == null)
		{
			localizedString = SimpleLocalizedString.CreateInstanceReplacement(variableName);
		}
		return new ItemModifiableVariableFloat01Percent(variableName, localizedString, defaultValue);
	}
}
