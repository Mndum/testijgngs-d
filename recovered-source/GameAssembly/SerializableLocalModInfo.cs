using System;

[Serializable]
public class SerializableLocalModInfo
{
	public SerializableGuid ModGuid;

	public bool LoadOnStart;

	public int ModOrder;

	public string ModName;

	public static SerializableLocalModInfo GenerateFromMod(Mod mod, int modOrder)
	{
		return new SerializableLocalModInfo
		{
			ModGuid = mod.ModGuid,
			LoadOnStart = true,
			ModOrder = modOrder,
			ModName = mod.Name
		};
	}
}
