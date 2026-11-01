using System;

[Flags]
public enum SceneEnumFlag
{
	NoScene = 0,
	Missionary = 1,
	Doggy = 2,
	Blowjob = 4,
	Cowgirl = 8,
	Interaction = 0x10,
	InteractionCuddle = 0x20,
	Shower = 0x40,
	All = -1
}
