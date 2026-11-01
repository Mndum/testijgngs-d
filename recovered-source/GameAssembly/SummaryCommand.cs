using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;

public class SummaryCommand : AppCommand
{
	public override string CommandName => "summary";

	public override string CommandDescription => LOC.UI.SummaryCommandDescription;

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			GameVariables current = GameVariables.Current;
			if (current.IsBotAwake)
			{
				stringBuilder.AppendLine(LOC.UI.SummaryCommandName(current.botName));
				stringBuilder.AppendLine(LOC.UI.SummaryCommandMasterName(current.playerName));
			}
			stringBuilder.AppendLine(LOC.UI.SummaryCommandLust(current.lust));
			stringBuilder.AppendLine(LOC.UI.SummaryCommandSympathy(current.sympathy));
			if (current.lastHeadpatedAt > 0)
			{
				stringBuilder.AppendLine(LOC.UI.SummaryCommandTimeSinceLastHeadpat(GlobalFunctions.TimePeriodHours(current.TimeSinceLastHeadpat)));
			}
			await popup.PrintLineDelay(stringBuilder.ToString(), 0.0025f, 0.0025f, cancellationToken);
		}
	}
}
