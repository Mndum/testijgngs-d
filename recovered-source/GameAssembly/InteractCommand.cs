using System.Threading;
using Cysharp.Threading.Tasks;

public class InteractCommand : AppCommand
{
	public override string CommandName => "interact";

	public override string CommandDescription => LOC.UI.InteractCommandDescription;

	public override bool IsListed => IsAllowed;

	public override bool IsAllowed
	{
		get
		{
			GameVariables current = GameVariables.Current;
			if (current.sympathy > 300)
			{
				return current.IsBotSmart;
			}
			return false;
		}
	}

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		await popup.FakeLoading(0.1f, 20, hideAfterDone: true, cancellationToken);
		await popup.DoFungusBlock(FungusBlocks.Console_InteractStart);
		await UniTask.NextFrame();
		await popup.PrintLineDelay(LOC.UI.InteractDirectConverasationOver, 0.0025f, 0.0025f, cancellationToken);
	}
}
