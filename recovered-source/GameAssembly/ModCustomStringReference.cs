public sealed class ModCustomStringReference
{
	public readonly Mod SourceMod;

	public readonly string Key;

	public readonly bool IsSmartString;

	public ModCustomStringReference(Mod sourceMod, string key, bool isSmartString)
	{
		SourceMod = sourceMod;
		Key = key;
		IsSmartString = isSmartString;
	}

	public string Get()
	{
		return ModdableTable.GetCustomString(SourceMod, Key);
	}

	public string Get(params object[] args)
	{
		return ModdableTable.GetCustomString(SourceMod, Key, args);
	}
}
