using System;

[Flags]
public enum CookingInputConsumeResult
{
	Can = 0,
	CannotTimeEarly = 1,
	CannotTimeLate = 2,
	CannotWrongType = 4,
	CannotWrongButton = 8,
	CannotFinished = 0x10
}
