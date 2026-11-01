using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Localization.Settings;

[CommandInfo("Narrative", "LocalizedConversation", "Do multiple say and portrait commands in a single localized block of text. Format is: [character] [portrait] [stage position] [hide] [<<< | >>>] [clear | noclear] [wait | nowait] [fade | nofade] [: Story text]", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class LocalizedConversationCommand : LocalizedCommandBase
{
	[SerializeField]
	public SerializedLocalizedConversation serializedLocalizedConversation;

	private string LegacyKeyName => LOS.GetOldConversationKey(ParentBlock.BlockName, CommandIndex);

	public override IEnumerable<LocalizedStringReference> IterateAllLocalizedStringReferences()
	{
		return serializedLocalizedConversation.localizedLines;
	}

	public override bool CreateEmptyKeys()
	{
		return serializedLocalizedConversation.CreateEmptyKeys(LineKeyName);
	}

	public override bool UpdateKeyNames()
	{
		return serializedLocalizedConversation.UpdateKeyNames(LineKeyName);
	}

	public override void ClearWithoutDeletingEntries()
	{
		serializedLocalizedConversation.ClearWithoutDeletingEntries();
	}

	public override void ClearAndDeleteEntries()
	{
		serializedLocalizedConversation.ClearAndDeleteEntries();
	}

	private string LineKeyName(int lineIndex)
	{
		return LOS.GetConversationLineKey(ParentBlock.BlockName, CommandIndex, lineIndex);
	}

	public override void OnCommandPastedAfterCopy(Command source)
	{
		serializedLocalizedConversation.mainText = "";
		serializedLocalizedConversation.localizedLines.Clear();
	}

	protected virtual IEnumerator DoConversation()
	{
		bool convertCharacterStringLiterals = LocalizationSettings.StringDatabase.SmartFormatter.Settings.ConvertCharacterStringLiterals;
		LocalizationSettings.StringDatabase.SmartFormatter.Settings.ConvertCharacterStringLiterals = false;
		IEnumerator conversationEnumerator = serializedLocalizedConversation.GetConversationEnumerator();
		LocalizationSettings.StringDatabase.SmartFormatter.Settings.ConvertCharacterStringLiterals = convertCharacterStringLiterals;
		yield return conversationEnumerator;
		Continue();
	}

	public override void OnEnter()
	{
		StartCoroutine(DoConversation());
	}

	public override string GetSummary()
	{
		return serializedLocalizedConversation.mainText;
	}

	public override Color GetButtonColor()
	{
		return new Color32(184, 210, 235, 255);
	}
}
