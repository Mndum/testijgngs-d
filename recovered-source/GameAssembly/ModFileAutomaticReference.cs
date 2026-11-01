using IC.AutomaticReference;
using UnityEngine;

public abstract class ModFileAutomaticReference<TResource> : AutomaticReference<TResource, ModFileBase> where TResource : Object
{
	public ModFileBase ModFileSource { get; set; }

	public ModFileAutomaticReference(ModFileBase modFileSource)
	{
		ModFileSource = modFileSource;
	}

	public override ModFileBase GetIdentifier()
	{
		return ModFileSource;
	}

	public override bool IsNull()
	{
		return ModFileSource == null;
	}
}
