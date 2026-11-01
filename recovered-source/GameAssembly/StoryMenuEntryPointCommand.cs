using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("My", "StoryMenuEntryPoint", "Open a menu using a menu story entry point!!", 0)]
public class StoryMenuEntryPointCommand : Command, IBlockCaller, IStringLocationIdentifier
{
	[SerializeField]
	protected string storyEntryPointId;

	public StoryMenuEntryPoint StoryEntryPoint => SingletonMonoBehaviour<StorySingleton>.Instance.GetStoryMenuEntryPoint(storyEntryPointId);

	public override void OnEnter()
	{
		StoryMenuEntryPoint entryPoint = StoryEntryPoint;
		if (entryPoint == null)
		{
			Debug.LogError("Entry point " + storyEntryPointId + " not found!");
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
		if (OptionsStatic.DebugEnabled && Input.GetKey(KeyCode.D))
		{
			foreach (StoryMenuBranch branch in entryPoint.Branches)
			{
				menuDialog.AddOption(branch.Summary(richText: true, runTime: true) + (branch.CanHappen ? "(Available)" : "(Unavailable)"), interactable: true, hideOption: false, null, () =>
				{
					entryPoint.ActivateBranch(this, branch);
				});
			}
		}
		else
		{
			foreach (var currentMenu in entryPoint.GetCurrentMenus())
			{
				var (branch2, flag) = currentMenu;
				menuDialog.AddOption(branch2.MenuText.GetString(), flag, hideOption: false, null, flag ? ((Action)(() =>
				{
					entryPoint.ActivateBranch(this, branch2);
				})) : null);
			}
		}
		Continue();
	}

	public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
	{
		if (StoryEntryPoint != null)
		{
			StoryEntryPoint.AddConnectedBlocks(ref connectedBlocks, GetFlowchart());
		}
	}

	public override string GetSummary()
	{
		if (StoryEntryPoint != null)
		{
			return "Opens menu with options defined in " + storyEntryPointId + "! Then continues this block";
		}
		return "Error: " + storyEntryPointId + " entry point not found!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 191, 217, 255);
	}

	public bool MayCallBlock(Block block)
	{
		return StoryEntryPoint?.MayCallBlock(block) ?? false;
	}
}
