using System;

public class InsertionHandlerBlock
{
	public float Block { get; private set; }

	public Func<bool> ActiveFunc { get; private set; }

	public InsertionHandlerBlock(float block, Func<bool> activeFunc)
	{
		Block = block;
		ActiveFunc = activeFunc;
	}
}
