using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class AppCommandWithFlags : AppCommand
{
	public override string CommandDescriptionDetailed
	{
		get
		{
			List<CommandFlagInfo> documentedFlagsWithDescriptions = GetDocumentedFlagsWithDescriptions();
			if (documentedFlagsWithDescriptions.Count == 0)
			{
				return LOC.UI.CommandFlagsNone;
			}
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.AppendLine(LOC.UI.CommandFlagsList);
				int num = Mathf.Min(15, documentedFlagsWithDescriptions.Max((CommandFlagInfo x) => x.Flag.Length));
				foreach (CommandFlagInfo item in documentedFlagsWithDescriptions)
				{
					stringBuilder.Append("-");
					stringBuilder.Append(item.Flag);
					stringBuilder.Append(' ', num - item.Flag.Length);
					stringBuilder.Append(" - ");
					stringBuilder.Append(item.Description);
					stringBuilder.AppendLine();
				}
				return stringBuilder.ToString();
			}
		}
	}

	protected abstract List<CommandFlagInfo> GetDocumentedFlagsWithDescriptions();
}
