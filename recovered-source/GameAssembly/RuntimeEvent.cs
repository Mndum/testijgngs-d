using JetBrains.Annotations;
using StoryConditions;

public class RuntimeEvent
{
	[CanBeNull]
	public readonly string RelatedFlag;

	public readonly StoryCondition[] Conditions;

	public readonly RuntimeEventTarget Target;

	public RuntimeEvent(string relatedFlag, RuntimeEventTarget target, params StoryCondition[] conditions)
	{
		Target = target;
		RelatedFlag = relatedFlag;
		Conditions = conditions;
	}

	public RuntimeEvent(string relatedFlag, RuntimeEventTarget target, int day, int hour, int minute, params StoryCondition[] conditions)
	{
		Target = target;
		RelatedFlag = relatedFlag;
		Conditions = conditions.AddConditionsAtFront(StoryCondition.TimeAfter(GameVariables.TimeFromDate(day, hour, minute)));
	}
}
