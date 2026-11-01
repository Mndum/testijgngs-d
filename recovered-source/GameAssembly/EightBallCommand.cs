using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EightBallCommand : AppCommand
{
	private const int EightBallResponseCount = 8;

	public override string CommandName => "8ball";

	public override string CommandDescription => LOC.UI.EightBallCommandDescription;

	public string GetEightBallResponse(int index)
	{
		return index switch
		{
			0 => LOC.UI.EightBallCommandYes, 
			1 => LOC.UI.EightBallCommandNo, 
			2 => LOC.UI.EightBallCommandMaybe, 
			3 => LOC.UI.EightBallCommandDefinitely, 
			4 => LOC.UI.EightBallCommandIDontThinkSo, 
			5 => LOC.UI.EightBallCommandAbsolutely, 
			6 => LOC.UI.EightBallCommandNotLikely, 
			_ => LOC.UI.EightBallCommandTryAgainLater, 
		};
	}

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		GetArgument(input, out var question, useRawInput: true);
		await popup.PrintLineDelay(R.OtherText._8ballAsciiArt.Get().text, 0.0025f, 0.0025f, cancellationToken);
		await popup.FakeLoading(2f, 20, hideAfterDone: true, cancellationToken);
		await popup.PrintLineDelay(LOC.UI.EightBallCommandText(question, GetEightBallResponse(Random.Range(0, 8))), 0.0025f, 0.0025f, cancellationToken);
	}
}
