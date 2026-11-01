using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;

public class HelpCommand : AppCommand
{
	public const string Name = "help";

	public override string CommandName => "help";

	public override string CommandDescription => LOC.UI.HelpCommandDescription(CommandName);

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		if (TryGetArgumentAtIndex(input, 0, out var a))
		{
			if (CommandsPrefabManager.TryGetCommand(a, out var found))
			{
				StringBuilder stringBuilder;
				using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
				{
					stringBuilder.Append("#");
					stringBuilder.AppendLine(LOC.UI.HelpCommandCommandNameHeader);
					stringBuilder.AppendLine(found.CommandName);
					stringBuilder.Append("\n#");
					stringBuilder.AppendLine(LOC.UI.HelpCommandDescriptionHeader);
					stringBuilder.AppendLine(found.CommandDescription);
					string commandDescriptionDetailed = found.CommandDescriptionDetailed;
					if (!string.IsNullOrWhiteSpace(commandDescriptionDetailed))
					{
						stringBuilder.Append("\n#");
						stringBuilder.AppendLine(LOC.UI.HelpCommandDetailedInfoHeader);
						stringBuilder.AppendLine(commandDescriptionDetailed);
					}
					await popup.PrintLineDelay(stringBuilder.ToString(), 0.0025f, 0.0025f, cancellationToken);
				}
			}
			else
			{
				await popup.PrintLineDelay(LOC.UI.CommandNotFound(a), 0.0025f, 0.0025f, cancellationToken);
			}
			return;
		}
		foreach (AppCommand listedCommand in CommandsPrefabManager.GetListedCommands())
		{
			await popup.PrintLineDelay(listedCommand.CommandName + " - " + listedCommand.CommandDescription, 0.0025f, 0.0025f, cancellationToken);
		}
	}
}
