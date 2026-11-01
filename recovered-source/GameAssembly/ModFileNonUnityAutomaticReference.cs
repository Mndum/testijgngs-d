using System;
using IC.AutomaticReference;

public class ModFileNonUnityAutomaticReference<TResource> : AutomaticNonUnityReference<TResource, ModFileBase> where TResource : class
{
	public ModFileBase ModFileSource { get; set; }

	public Func<ModFileBase, TResource> ResourceGetter { get; set; }

	public ModFileNonUnityAutomaticReference(ModFileBase modFileSource, Func<ModFileBase, TResource> resourceGetter)
	{
		ModFileSource = modFileSource;
		ResourceGetter = resourceGetter;
	}

	protected override TResource Load()
	{
		return ResourceGetter(ModFileSource);
	}

	public override ModFileBase GetIdentifier()
	{
		return ModFileSource;
	}

	public override bool IsNull()
	{
		if (ModFileSource != null)
		{
			return ResourceGetter == null;
		}
		return true;
	}
}
