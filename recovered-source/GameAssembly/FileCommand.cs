using System.Threading;
using Cysharp.Threading.Tasks;

public class FileCommand : AppCommand
{
	public const string Report1Path = "C:\\system\\debug\\reports\\report_1.txt";

	public const string Report2Path = "C:\\system\\debug\\reports\\report_2.txt";

	public const string Report3Path = "C:\\system\\debug\\reports\\report_3.txt";

	public const string AsimovPath = "F:\\Jun\\system\\safety\\Asimov3.xftt";

	public const string SomeFilePath = "C:\\system\\some file.txt";

	public const string Journal1Path = "F:\\files\\research\\journal\\1574_12_12.txt";

	public const string Journal2Path = "F:\\files\\research\\journal\\1578_12_16.txt";

	public const string Journal3Path = "F:\\files\\research\\journal\\1580_12_18.txt";

	public override string CommandName => "file";

	public override string CommandDescription => LOC.UI.FileCommandDescription;

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		if (input.Arguments.Length > 1)
		{
			await popup.PrintLineDelay(LOC.UI.FileCommandHelp, 0.0025f, 0.0025f, cancellationToken);
			await ExampleUsage(popup, cancellationToken);
			return;
		}
		if (input.Arguments.Length == 0)
		{
			await popup.PrintLineDelay(LOC.UI.FileCommandMustInputFilename, 0.0025f, 0.0025f, cancellationToken);
			await ExampleUsage(popup, cancellationToken);
			return;
		}
		string text = input.Arguments[0].Replace("/", "\\");
		switch (text)
		{
		case "F:\\Jun\\system\\safety\\Asimov3.xftt":
			await popup.PrintLineDelay(LOC.UI.FileCommandAccessDenied, 0.0025f, 0.0025f, cancellationToken);
			break;
		case "C:\\system\\debug\\reports\\report_1.txt":
			await FileShow(popup, text, LOC.Other.Report1File, cancellationToken);
			break;
		case "C:\\system\\debug\\reports\\report_2.txt":
			await FileShow(popup, text, LOC.Other.Report2File, cancellationToken);
			break;
		case "C:\\system\\debug\\reports\\report_3.txt":
			await FileShow(popup, text, LOC.Other.Report3File, cancellationToken);
			break;
		case "F:\\files\\research\\journal\\1574_12_12.txt":
			GameVariables.Current.AddFlag("secret_files");
			await FileShow(popup, text, LOC.Other.JournalFile1, cancellationToken);
			break;
		case "F:\\files\\research\\journal\\1578_12_16.txt":
			GameVariables.Current.AddFlag("secret_files");
			await FileShow(popup, text, LOC.Other.JournalFile2, cancellationToken);
			break;
		case "F:\\files\\research\\journal\\1580_12_18.txt":
			GameVariables.Current.AddFlag("secret_files");
			await FileShow(popup, text, LOC.Other.JournalFile3, cancellationToken);
			break;
		case "C:\\system\\some file.txt":
			await popup.PrintLineDelay(LOC.UI.FileCommandEmptyFile, 0.0025f, 0.0025f, cancellationToken);
			break;
		default:
			await popup.PrintLineDelay(LOC.UI.FileCommandFileNotFound, 0.0025f, 0.0025f, cancellationToken);
			break;
		}
	}

	private static async UniTask FileShow(BotStatusAppPopup popup, string path, string content, CancellationToken cancellationToken)
	{
		await popup.PrintLineDelay(LOC.UI.FileCommandFileContents(path), 0.0025f, 0.0025f, cancellationToken);
		await popup.PrintLineDelay(content, 0.0025f, 0.0025f, cancellationToken);
	}

	private async UniTask ExampleUsage(BotStatusAppPopup popup, CancellationToken cancellationToken)
	{
		await popup.PrintLineDelay(LOC.UI.CommandExampleUsage, 0.0025f, 0.0025f, cancellationToken);
		await popup.PrintLineDelay(CommandName + " \"C:\\system\\some file.txt\"", 0.0025f, 0.0025f, cancellationToken);
	}
}
