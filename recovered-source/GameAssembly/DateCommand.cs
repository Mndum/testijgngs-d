using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

public class DateCommand : AppCommandWithFlags
{
	public const string DayOfWeekFlag = "w";

	public override string CommandName => "date";

	public override string CommandDescription => LOC.UI.DateCommandDescription;

	protected override List<CommandFlagInfo> GetDocumentedFlagsWithDescriptions()
	{
		return new List<CommandFlagInfo>
		{
			new CommandFlagInfo("w", LOC.UI.DateCommandDayOfWeekFlagDescription)
		};
	}

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		if (input.HasFlag("w"))
		{
			await popup.PrintLineDelay(LOC.UI.CurrentDayOfTheWeek(GameVariables.Current.GameDateTime), 0.0025f, 0.0025f, cancellationToken);
		}
		else
		{
			await popup.PrintLineDelay(LOC.UI.CurrentDateInfo(GameVariables.Current.GameDateTime), 0.0025f, 0.0025f, cancellationToken);
		}
	}
}
