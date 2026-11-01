using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fungus;
using UnityEngine;

[CommandInfo("My", "StoryEntryPoint", "Enter a story entry point!", 0)]
public class StoryEntryPointCommand : Command, IBlockCaller, IStringLocationIdentifier
{
	[SerializeField]
	protected string storyEntryPointId;

	public StoryAutoEntryPoint StoryAutoEntryPoint => SingletonMonoBehaviour<StorySingleton>.Instance.GetStoryAutoEntryPoint(storyEntryPointId);

	public override void OnEnter()
	{
		StoryAutoEntryPoint entryPoint = StoryAutoEntryPoint;
		if (entryPoint == null)
		{
			Debug.LogError("Entry point " + storyEntryPointId + " not found!");
			Continue();
			return;
		}
		StoryAutoBranch entryPointBranch = entryPoint.GetBranch();
		if (OptionsStatic.DebugEnabled && Input.GetKey(KeyCode.D))
		{
			StoryAutoBranch nullBranch = new StoryAutoBranch(new StoryBlockTarget("Nothing"), null);
			if (entryPointBranch == null)
			{
				entryPointBranch = nullBranch;
			}
			Selection<StoryAutoBranch> selection = SelectionFactory.CreateInstanceSingular<StoryAutoBranch>();
			selection.PartialAllowed = false;
			selection.MaxUniqueItemCount = 1;
			selection.AddItemFull(entryPointBranch);
			Debug.Log(selection.ToStringDescribeSelection((StoryAutoBranch s, StringBuilder i) =>
			{
				i.Append(s);
			}));
			CompoundItemList.StartBuilder<StoryAutoBranch, StringAdvancedItemListItemPrefab>().WithToStringOnly((StoryAutoBranch a, StringRepresentationHelperEnum b) => a.Summary(richText: true, runTime: true)).WithNoSorting()
				.WithAllFolder()
				.WithSetItem((StoryAutoBranch item, StringAdvancedItemListItemPrefab display, CompoundItemList.CompoundItemListAdapter<StoryAutoBranch, StringAdvancedItemListItemPrefab> adapter) =>
				{
					StringBuilder stringBuilder;
					using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
					{
						stringBuilder.AppendLine(item.Summary(richText: true, runTime: true));
						if (entryPointBranch == item)
						{
							stringBuilder.Append("(Would be selected!)");
						}
						display.VisibleText.SetText(stringBuilder);
					}
				})
				.WithDefaultListItemPool()
				.Build(entryPoint.Branches.Append(nullBranch).ToFrozenDataSource())
				.ToChooseItemsComplexPopupBuilderGenericString()
				.WithStartingSelection(selection)
				.WithOnFinished((UiOverlay.PopupFinishTypesEnum a, Selection<StoryAutoBranch> selection2) =>
				{
					if (a == UiOverlay.PopupFinishTypesEnum.Successful && selection2.TryGetFirstItem(out var item) && item != nullBranch)
					{
						entryPoint.ActivateBranch(this, item);
					}
					else
					{
						Continue();
					}
				})
				.OpenPopup("DEBUG", "Debug branch info", allowClosing: false)
				.sortOrder = UiOverlay.FunugsOverlayLayer + 1;
			UiOverlay.Instance.SyncAllPopups();
		}
		else if (entryPointBranch != null)
		{
			entryPoint.ActivateBranch(this, entryPointBranch);
		}
		else
		{
			Continue();
		}
	}

	public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
	{
		if (StoryAutoEntryPoint != null)
		{
			StoryAutoEntryPoint.AddConnectedBlocks(ref connectedBlocks, GetFlowchart());
		}
	}

	public override string GetSummary()
	{
		if (StoryAutoEntryPoint != null)
		{
			return "Calls story entry point if any valid branch exists in " + storyEntryPointId + "! Otherwise continues this block.";
		}
		return "Error: " + storyEntryPointId + " entry point not found!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 191, 217, 255);
	}

	public bool MayCallBlock(Block block)
	{
		return StoryAutoEntryPoint?.MayCallBlock(block) ?? false;
	}
}
