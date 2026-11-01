using System;

[Flags]
public enum CookingInputFlagEnum
{
	None = 0,
	Left = 1,
	Right = 2,
	Up = 4,
	Down = 8,
	LeftDown = Left | Down,
	LeftUp = Left | Up,
	RightDown = Right | Down,
	RightUp = Right | Up
}
