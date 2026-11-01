using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Localization.Tables;
using UnityEngine.Serialization;

[CommandInfo("Narrative", "LocalizedMenu", "Displays a button in a multiple choice menu", 0)]
[AddComponentMenu("")]
public class LocalizedMenu : LocalizedCommandBase, IBlockCaller, IStringLocationIdentifier, ILocalizedFrom<Game>
{
	[Tooltip("Text to display on the menu button")]
	[SerializeField]
	public LocalizedStringReferenceStory textReference = new LocalizedStringReferenceStory();

	[FormerlySerializedAs("targetSequence")]
	[Tooltip("Block to execute when this option is selected")]
	[SerializeField]
	public Block targetBlock;

	[Tooltip("If false, the menu option will be displayed but will not be selectable")]
	[SerializeField]
	public BooleanData interactable = new BooleanData(v: true);

	public string KeyName => LOS.GetMenuKey(ParentBlock.BlockName, CommandIndex);

	public void CreateKeysFrom(Game old)
	{
		targetBlock = old.targetBlock;
		interactable = old.interactable;
		textReference.CreateIfNotExists(KeyName, out var entry);
		var (value, smart) = LOS.OldFormatToSmartString(old.text);
		textReference.SetSmart(smart);
		entry.Value = value;
	}

	public override IEnumerable<LocalizedStringReference> IterateAllLocalizedStringReferences()
	{
		yield return textReference;
	}

	public override bool CreateEmptyKeys()
	{
		return textReference.CreateIfNotExists(KeyName);
	}

	public override bool UpdateKeyNames()
	{
		return textReference.UpdateKeyName(KeyName);
	}

	public override void ClearWithoutDeletingEntries()
	{
		textReference.ClearWithoutDeletingEntry();
	}

	public override void ClearAndDeleteEntries()
	{
		textReference.Delete();
	}

	public override void OnCommandPastedAfterCopy(Command source)
	{
		base.OnCommandPastedAfterCopy(source);
		if (source is LocalizedMenu localizedMenu)
		{
			textReference.CopyFrom(localizedMenu.textReference);
		}
	}

	public override void OnEnter()
	{
		MenuDialog menuDialog = MenuDialog.GetMenuDialog();
		if (menuDialog != null)
		{
			menuDialog.SetActive(state: true);
			string text = textReference.GetString();
			menuDialog.AddOption(text, interactable, hideOption: false, targetBlock);
		}
		Continue();
	}

	public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
	{
		if (targetBlock != null)
		{
			connectedBlocks.Add(targetBlock);
		}
	}

	public override string GetSummary()
	{
		if (targetBlock == null)
		{
			return "Error: No target block selected";
		}
		if (!textReference.Valid)
		{
			return "Error: No button text entry!";
		}
		StringTableEntry entry = textReference.GetEntry();
		if (entry == null)
		{
			return "Error: No button text entry for this language!";
		}
		return entry.Value + " : " + targetBlock.BlockName;
	}

	public override Color GetButtonColor()
	{
		return new Color32(184, 210, 235, 255);
	}

	public override bool HasReference(Variable variable)
	{
		if (!(interactable.booleanRef == variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	public bool MayCallBlock(Block block)
	{
		return block == targetBlock;
	}
}
