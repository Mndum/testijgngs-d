using System;

[Flags]
public enum ItemNameIncludeEnum
{
	Raw = 0,
	ColorInfo = 1,
	ShowFavourite = 2,
	ItemCount = 4,
	ItemModifiers = 8,
	FishEdibleFishingRod = 0x10,
	Other = 0x20,
	Basic = ItemCount | FishEdibleFishingRod,
	Default = Basic | ColorInfo | ShowFavourite | ItemModifiers,
	DefaultNoFavourite = 0x1D,
	DefaultNoCount = 0x1B,
	DefaultNoColorNoFavourite = 0x1C
}
