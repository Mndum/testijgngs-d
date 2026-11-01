using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public abstract class StoryBotDialogueAreaDefinition
{
	public abstract string GetPrefix();

	public IEnumerable<FieldInfo> GetAllTagFields()
	{
		return from field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
			where field.FieldType == typeof(StoryBotDialogueTagDefinition)
			select field;
	}

	public void Init(StorySingleton storySingleton, StorySingleton.Stages stages)
	{
		string prefix = GetPrefix();
		foreach (FieldInfo allTagField in GetAllTagFields())
		{
			if (allTagField.GetValue(this) == null)
			{
				allTagField.SetValue(this, new StoryBotDialogueTagDefinition(prefix + allTagField.Name));
			}
		}
		InitInner(storySingleton, stages.Stage1, stages.Stage2, stages.Stage3);
	}

	protected abstract void InitInner(StorySingleton storySingleton, StoryBotDialogueStage stage1, StoryBotDialogueStage stage2, StoryBotDialogueStage stage3);
}
