using System;

[Flags]
public enum CookingGameplayModifiers
{
	NoMods = 0,
	SlowMode = 1,
	FastMode = 2,
	UltraFastTimeMode = 4,
	InsaneFastTimeMode = 8,
	Practice = 0x10,
	AutoPlay = 0x20,
	OtherMods = Practice,
	AllSpeedMods = SlowMode | FastMode | UltraFastTimeMode | InsaneFastTimeMode
}
