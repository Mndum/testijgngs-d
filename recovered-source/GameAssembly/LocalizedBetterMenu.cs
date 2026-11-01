using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

[CommandInfo("My", "Localized Better menu", "Creates fungus menu, BUT BETTER", 0)]
public class LocalizedBetterMenu : LocalizedCommandBase, IBlockCaller, IStringLocationIdentifier, ILocalizedFrom<BetterMenu>
{
	[Serializable]
	public class LocalizedMenuOption
	{
		public LocalizedStringReferenceStory text;

		public Block targetBlock;

		public BooleanData interactable;

		public BooleanData hideThisOption;

		public LocalizedMenuOption()
		{
			Init();
		}

		public void Init()
		{
			text = new LocalizedStringReferenceStory();
			interactable = new BooleanData(v: true);
			hideThisOption = new BooleanData(v: false);
		}
	}

	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	private Variable output;

	[SerializeField]
	private List<LocalizedMenuOption> options = new List<LocalizedMenuOption>();

	private bool WaitForEnd => output != null;

	private IEnumerable<(string, LocalizedStringReference)> OptionsWithNames => options.Select((LocalizedMenuOption t, int i) => ((string, LocalizedStringReference))(KeyName(i), t.text));

	public void CreateKeysFrom(BetterMenu old)
	{
		output = old.output;
		options.Clear();
		for (int i = 0; i < old.options.Count; i++)
		{
			BetterMenu.MenuOption menuOption = old.options[i];
			LocalizedMenuOption localizedMenuOption = new LocalizedMenuOption
			{
				interactable = menuOption.interactable,
				hideThisOption = menuOption.hideThisOption,
				targetBlock = menuOption.targetBlock
			};
			string key = KeyName(i);
			localizedMenuOption.text.CreateIfNotExists(key, out var entry);
			var (value, smart) = LOS.OldFormatToSmartString(menuOption.text);
			localizedMenuOption.text.SetSmart(smart);
			entry.Value = value;
			options.Add(localizedMenuOption);
		}
	}

	public override void ClearWithoutDeletingEntries()
	{
		foreach (LocalizedMenuOption option in options)
		{
			option.text?.ClearWithoutDeletingEntry();
		}
	}

	public override void ClearAndDeleteEntries()
	{
		foreach (LocalizedMenuOption option in options)
		{
			option.text?.Delete();
		}
	}

	public override IEnumerable<LocalizedStringReference> IterateAllLocalizedStringReferences()
	{
		return options.Select((LocalizedMenuOption x) => x.text);
	}

	public override bool CreateEmptyKeys()
	{
		return OptionsWithNames.CreateEmptyEntriesMultiple();
	}

	public override bool UpdateKeyNames()
	{
		return OptionsWithNames.UpdateKeyNamesMultiple();
	}

	public string KeyName(int i)
	{
		return LOS.GetBetterMenuKey(ParentBlock.BlockName, CommandIndex, i);
	}

	public override void OnCommandPastedAfterCopy(Command source)
	{
		base.OnCommandPastedAfterCopy(source);
		if (source is LocalizedBetterMenu localizedBetterMenu && localizedBetterMenu.options.Count == options.Count)
		{
			for (int i = 0; i < localizedBetterMenu.options.Count; i++)
			{
				LocalizedMenuOption localizedMenuOption = localizedBetterMenu.options[i];
				options[i].text.CopyFrom(localizedMenuOption.text);
			}
		}
	}

	public override void OnValidate()
	{
		if (options.Count == 0)
		{
			options.Add(new LocalizedMenuOption());
		}
	}

	public override void OnEnter()
	{
		if (options.Count == 0)
		{
			Continue();
			return;
		}
		MenuDialog menuDialog = MenuDialog.GetMenuDialog();
		if (menuDialog == null)
		{
			Continue();
			return;
		}
		menuDialog.SetActive(state: true);
		foreach (LocalizedMenuOption option in options)
		{
			if (!option.hideThisOption)
			{
				option.text.ReportSeen();
			}
		}
		if (WaitForEnd)
		{
			for (int i = 0; i < options.Count; i++)
			{
				int o = i;
				LocalizedMenuOption localizedMenuOption = options[i];
				string text = localizedMenuOption.text.GetString();
				menuDialog.AddOption(text, localizedMenuOption.interactable, localizedMenuOption.hideThisOption, localizedMenuOption.targetBlock, () =>
				{
					Continue();
					if (output is IntegerVariable integerVariable)
					{
						integerVariable.Value = o;
					}
				});
			}
		}
		else
		{
			foreach (LocalizedMenuOption option2 in options)
			{
				string text2 = option2.text.GetString();
				menuDialog.AddOption(text2, option2.interactable, option2.hideThisOption, option2.targetBlock);
			}
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(menuDialog.transform as RectTransform);
		if (!WaitForEnd)
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		if (options.Count == 0)
		{
			return "Error no options!";
		}
		string text = "";
		for (int i = 0; i < options.Count; i++)
		{
			LocalizedMenuOption localizedMenuOption = options[i];
			if (localizedMenuOption.targetBlock == null && !WaitForEnd)
			{
				return $"Error in option {i}: No target block selected ";
			}
			if (!localizedMenuOption.text.Valid)
			{
				return $"Error in option {i}: Error: No text entry!";
			}
			text += $"{i}=\"{localizedMenuOption.text.GetEntry()?.Value}\" ";
		}
		if (output != null)
		{
			return "Adds menu and saves number [" + text + "] to " + output.Key;
		}
		return "Adds menu " + text;
	}

	public override Color GetButtonColor()
	{
		return new Color32(150, 150, 253, 255);
	}

	public override bool HasReference(Variable variable)
	{
		if (!options.Any((LocalizedMenuOption t) => (bool)t.hideThisOption == (bool)variable || (bool)t.interactable == (bool)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	public bool MayCallBlock(Block block)
	{
		return options.Any((LocalizedMenuOption t) => t.targetBlock == block);
	}

	public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
	{
		connectedBlocks.AddRange(from t in options
			select t.targetBlock into t
			where t != null
			select t);
	}
}
