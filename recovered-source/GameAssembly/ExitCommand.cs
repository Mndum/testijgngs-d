using System.Threading;
using Cysharp.Threading.Tasks;

public class ExitCommand : AppCommand
{
	public override string CommandName => "exit";

	public override string CommandDescription => LOC.UI.ExitCommandDescription;

	public override UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		popup.RaiseStopFlag();
		return UniTask.CompletedTask;
	}
}
