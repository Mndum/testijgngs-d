using System;
using JetBrains.Annotations;

[Serializable]
public sealed class SerializableModInfoStub
{
	public SerializableGuid ModGuid;

	[CanBeNull]
	public string ModName;

	public SerializableModInfoStub(SerializableGuid modGuid, string modName)
	{
		ModGuid = modGuid;
		ModName = modName;
	}
}
