using System;
using System.Collections.Generic;
using Fungus;
using TMPro;
using UnityEngine;

[CommandInfo("My", "Text Input", "Inputs a text", 0)]
public class TextInput : LocalizedCommandBase
{
	public enum InputType
	{
		NoCheck,
		Name,
		NameToLower
	}

	public LocalizedStringReferenceStory title;

	public LocalizedStringReferenceStory info;

	public LocalizedStringReferenceStory defaultInput;

	[VariableProperty(new Type[] { typeof(StringVariable) })]
	public Variable inputTarget;

	public InputType type;

	public string TitleKeyName => LOS.GetTextInputKey(ParentBlock.BlockName, CommandIndex, "title");

	public string InfoKeyName => LOS.GetTextInputKey(ParentBlock.BlockName, CommandIndex, "info");

	public string DefaultInputKeyName => LOS.GetTextInputKey(ParentBlock.BlockName, CommandIndex, "default");

	private UiOverlay.StringValidationDelegate GetValidationDelegate()
	{
		InputType inputType = type;
		if (inputType == InputType.NoCheck || (uint)(inputType - 1) > 1u)
		{
			return null;
		}
		return UiOverlay.NameLengthValidation;
	}

	private UiOverlay.StringProcessingDelegate GetProcessingDelegate()
	{
		return type switch
		{
			InputType.Name => UiOverlay.NameTrimProcessing, 
			InputType.NameToLower => UiOverlay.NameTrimToLowerProcessing, 
			_ => null, 
		};
	}

	public override void OnEnter()
	{
		Variable variable = inputTarget;
		StringVariable stringVariable = variable as StringVariable;
		if ((object)stringVariable != null)
		{
			UiOverlay.Instance.InputPopupWithValidation(title.GetString(), info.GetString(), LOC.UI.Naming_AreYouSure, (string s) =>
			{
				stringVariable.Value = s;
				Continue();
			}, defaultInput.GetString(), (type != InputType.NoCheck) ? TMP_InputField.ContentType.Name : TMP_InputField.ContentType.Standard, GetValidationDelegate(), GetProcessingDelegate());
		}
		else
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		return "Shows input popup and inputs data!";
	}

	public override bool HasReference(Variable var)
	{
		return var == inputTarget;
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}

	public override IEnumerable<LocalizedStringReference> IterateAllLocalizedStringReferences()
	{
		yield return title;
		yield return info;
		yield return defaultInput;
	}

	public override bool CreateEmptyKeys()
	{
		return title.CreateIfNotExists(TitleKeyName) | info.CreateIfNotExists(InfoKeyName) | defaultInput.CreateIfNotExists(DefaultInputKeyName);
	}

	public override bool UpdateKeyNames()
	{
		return title.UpdateKeyName(TitleKeyName) | info.UpdateKeyName(InfoKeyName) | defaultInput.UpdateKeyName(DefaultInputKeyName);
	}

	public override void ClearWithoutDeletingEntries()
	{
		title.ClearWithoutDeletingEntry();
		info.ClearWithoutDeletingEntry();
		defaultInput.ClearWithoutDeletingEntry();
	}

	public override void ClearAndDeleteEntries()
	{
		title.Delete();
		info.Delete();
		defaultInput.Delete();
	}

	public override void OnCommandPastedAfterCopy(Command source)
	{
		base.OnCommandPastedAfterCopy(source);
		if (source is TextInput textInput)
		{
			title.CopyFrom(textInput.title);
			info.CopyFrom(textInput.info);
			defaultInput.CopyFrom(textInput.defaultInput);
		}
	}
}
