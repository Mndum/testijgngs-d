using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fungus;
using UnityEngine;

public class StoryBlockTarget : StoryBranchTarget
{
	public class Step
	{
		public Command CallingCommand;

		public Block TargetBlock;

		public Step(Command callingCommand, Block targetBlock)
		{
			CallingCommand = callingCommand;
			TargetBlock = targetBlock;
		}
	}

	public class Route
	{
		public HashSet<Block> VisitedBlocks = new HashSet<Block>();

		public List<Step> Steps = new List<Step>();

		public List<string> Flags = new List<string>();

		public void AddStep(Step step)
		{
			Steps.Add(step);
			VisitedBlocks.Add(step.TargetBlock);
		}

		public Route MakeCopy()
		{
			return new Route
			{
				VisitedBlocks = new HashSet<Block>(VisitedBlocks),
				Flags = Flags.ToList(),
				Steps = Steps.ToList()
			};
		}

		public void Describe(StringBuilder sb)
		{
			sb.Append("Route: ");
			sb.AppendJoin(", ", Steps.Select((Step t) => t.TargetBlock.BlockName));
			sb.AppendLine();
			sb.Append("Flags: ");
			sb.AppendJoin(", ", Flags);
			sb.AppendLine();
		}
	}

	public string TargetBlockName { get; private set; }

	public string TargetLabel { get; private set; }

	public bool NoTargetLabel => string.IsNullOrWhiteSpace(TargetLabel);

	public StoryBlockTarget(string targetBlockName, string targetLabel = null)
	{
		TargetBlockName = targetBlockName;
		TargetLabel = targetLabel;
	}

	public StoryBlockTarget(FungusBlocks.FungusBlockReference targetBlock)
	{
		TargetBlockName = targetBlock.Name;
		TargetLabel = targetBlock.Label;
	}

	public override void Activate(Action onFinished)
	{
		Flowchart flowchart = SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart;
		Block targetBlock = GetTargetBlock(flowchart);
		if (targetBlock == null)
		{
			Debug.LogError("Target block " + TargetBlockName + " not found!");
			onFinished?.Invoke();
			return;
		}
		if (NoTargetLabel)
		{
			flowchart.ExecuteBlock(targetBlock, 0, onFinished);
			return;
		}
		int labelIndex = targetBlock.GetLabelIndex(TargetLabel);
		if (labelIndex < 0)
		{
			Debug.LogError("Target label " + TargetLabel + " in block " + TargetBlockName + " not found!");
			onFinished?.Invoke();
		}
		else
		{
			flowchart.ExecuteBlock(targetBlock, labelIndex, onFinished);
		}
	}

	public override Result CheckValidity(Dictionary<string, Block> blocks)
	{
		if (!blocks.TryGetValue(TargetBlockName, out var value))
		{
			return Result.FailWithDescription("Block \"" + TargetBlockName + "\" not found!");
		}
		if (NoTargetLabel)
		{
			return true;
		}
		if (value.GetLabelIndex(TargetLabel) < 0)
		{
			return Result.FailWithDescription("Label \"" + TargetLabel + "\" in block \"" + TargetBlockName + "\" not found!");
		}
		return Result.Success;
	}

	public override string ToString()
	{
		if (NoTargetLabel)
		{
			return "Block: " + TargetBlockName;
		}
		return "Block: " + TargetBlockName + "(Label: " + TargetLabel + ")";
	}

	public Block GetTargetBlock(Flowchart flowchart)
	{
		flowchart.TryGetBlock(TargetBlockName, out var block);
		return block;
	}

	public static List<Route> AnalyseRoutes(Block sourceBlock, Block targetBlock, HashSet<Block> endBlocks, int maxSteps = 5)
	{
		Route route = new Route();
		route.AddStep(new Step(null, targetBlock));
		List<Route> list = new List<Route>();
		if (sourceBlock == targetBlock)
		{
			return list;
		}
		InnerAnalyseRoutes(route, list, endBlocks, maxSteps);
		return list;
	}

	private static void InnerAnalyseRoutes(Route currentRoute, List<Route> allRoutes, HashSet<Block> endBlocks, int stepsLeft)
	{
		if (stepsLeft <= 0)
		{
			return;
		}
		Block targetBlock = currentRoute.Steps.Last().TargetBlock;
		bool flag = false;
		List<Block> connectedBlocks = new List<Block>();
		foreach (Command command in targetBlock.CommandList)
		{
			if (command is ModifyFlag modifyFlag)
			{
				currentRoute.Flags.Add(modifyFlag.flag);
				continue;
			}
			command.GetConnectedBlocks(ref connectedBlocks);
			if (connectedBlocks.Count <= 0)
			{
				continue;
			}
			foreach (Block item in connectedBlocks)
			{
				if (endBlocks.Contains(item))
				{
					flag = true;
					allRoutes.Add(currentRoute);
				}
				else if (!currentRoute.VisitedBlocks.Contains(item))
				{
					flag = true;
					Route route = currentRoute.MakeCopy();
					route.AddStep(new Step(command, item));
					InnerAnalyseRoutes(route, allRoutes, endBlocks, stepsLeft - 1);
				}
			}
			connectedBlocks.Clear();
		}
		if (!flag && !(currentRoute.Steps.Last().CallingCommand is Call { callMode: CallMode.WaitUntilFinished }))
		{
			allRoutes.Add(currentRoute);
		}
	}
}
