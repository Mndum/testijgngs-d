using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;

[CommandInfo("My", "Better menu", "Creates fungus menu, BUT BETTER", 0)]
public class BetterMenu : Command, IBlockCaller, IStringLocationIdentifier
{
	[Serializable]
	public class MenuOption
	{
		public string text;

		public Block targetBlock;

		public BooleanData interactable;

		public BooleanData hideThisOption;

		public MenuOption()
		{
			Init();
		}

		public void Init()
		{
			text = "Option Text";
			interactable = new BooleanData(v: true);
			hideThisOption = new BooleanData(v: false);
		}
	}

	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	public Variable output;

	[SerializeField]
	public List<MenuOption> options = new List<MenuOption>();

	public bool WaitForEnd => output != null;

	public override void OnValidate()
	{
		if (options.Count == 0)
		{
			options.Add(new MenuOption());
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
		Flowchart flowchart = GetFlowchart();
		if (WaitForEnd)
		{
			for (int i = 0; i < options.Count; i++)
			{
				int o = i;
				MenuOption menuOption = options[i];
				string text = flowchart.SubstituteVariables(menuOption.text);
				menuDialog.AddOption(text, menuOption.interactable, menuOption.hideThisOption, menuOption.targetBlock, () =>
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
			foreach (MenuOption option in options)
			{
				string text2 = flowchart.SubstituteVariables(option.text);
				menuDialog.AddOption(text2, option.interactable, option.hideThisOption, option.targetBlock);
			}
		}
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
			MenuOption menuOption = options[i];
			if (menuOption.targetBlock == null && !WaitForEnd)
			{
				return $"Error in option {i}: No target block selected ";
			}
			if (menuOption.text == "")
			{
				return $"Error in option {i}: No button text selected";
			}
			text += $"{i}=\"{menuOption.text}\" ";
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
		if (!options.Any((MenuOption t) => (bool)t.hideThisOption == (bool)variable || (bool)t.interactable == (bool)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	public bool MayCallBlock(Block block)
	{
		return options.Any((MenuOption t) => t.targetBlock == block);
	}

	public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
	{
		connectedBlocks.AddRange(from t in options
			select t.targetBlock into t
			where t != null
			select t);
	}
}
