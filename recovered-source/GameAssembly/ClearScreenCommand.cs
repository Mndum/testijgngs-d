using System.Threading;
using Cysharp.Threading.Tasks;

public class ClearScreenCommand : AppCommand
{
	public override string CommandName => "cls";

	public override string CommandDescription => LOC.UI.ClsCommandDescription;

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		popup.ClearScreen();
		popup.PrintLine(LOC.UI.ClsResult);
		popup.UpdateDisplay();
		await UniTask.Yield();
	}
}
