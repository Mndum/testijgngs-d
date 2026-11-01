using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("My", "Connection visualization", "Visualizes connections!", 0)]
public class ConnectionVisualization : Command
{
	public enum ConnectionVisualizationType
	{
		EmailEvents,
		RandomEvents
	}

	public ConnectionVisualizationType VisualizationType;

	public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
	{
		Flowchart flowchart = GetFlowchart();
		switch (VisualizationType)
		{
		case ConnectionVisualizationType.EmailEvents:
		{
			foreach (EmailPrefabManager.EmailPrefab allEmail in EmailPrefabManager.AllEmails)
			{
				if (allEmail.OnReadEvent is FungusBlockEmailEventTarget fungusBlockEmailEventTarget && flowchart.TryGetBlock(fungusBlockEmailEventTarget.Block.Name, out var block2))
				{
					connectedBlocks.Add(block2);
				}
			}
			break;
		}
		case ConnectionVisualizationType.RandomEvents:
		{
			List<RuntimeEvent> list;
			using (MyCollectionPool<RuntimeEvent>.GetList(out list))
			{
				EventManager.FillRuntimeEvents(list);
				foreach (RuntimeEvent item in list)
				{
					if (item.Target is FungusBlockEventTarget fungusBlockEventTarget && flowchart.TryGetBlock(fungusBlockEventTarget.Block.Name, out var block))
					{
						connectedBlocks.Add(block);
					}
				}
				break;
			}
		}
		}
	}

	public override Color GetButtonColor()
	{
		return new Color32(250, 50, 50, 255);
	}
}
