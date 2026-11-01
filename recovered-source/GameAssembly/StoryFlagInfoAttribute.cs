using System;

[AttributeUsage(AttributeTargets.Field)]
public class StoryFlagInfoAttribute : Attribute
{
	public string Comment;

	public StoryFlagInfoAttribute(string comment)
	{
		Comment = comment;
	}
}
