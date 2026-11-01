using System;

[Flags]
public enum LimbUsageChoiceEnum
{
	None = 0,
	LeftArm = 1,
	RightArm = 2,
	Cock = 4,
	Mouth = 8,
	BothArms = LeftArm | RightArm
}
