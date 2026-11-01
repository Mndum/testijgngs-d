using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CowsayCommand : AppCommandWithFlags
{
	public string JunFlag = "jun";

	public string AnnalieFlag = "annalie";

	public string SheepFlag = "sheep";

	public string GayFlag = "g";

	public override string CommandName => "cowsay";

	public override string CommandDescription => LOC.UI.CowsayCommandDescription;

	protected override List<CommandFlagInfo> GetDocumentedFlagsWithDescriptions()
	{
		return new List<CommandFlagInfo>
		{
			new CommandFlagInfo(JunFlag, LOC.UI.CowsayJunFlagDescription),
			new CommandFlagInfo(AnnalieFlag, LOC.UI.CowsayAnnalieFlagDescription),
			new CommandFlagInfo(GayFlag, LOC.UI.CowsayGayFlagDescription)
		};
	}

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		GetArgument(input, out var argument, useRawInput: true);
		int allowedLineWidth = 40;
		int a = 5;
		string value;
		if (input.HasFlag(JunFlag))
		{
			value = R.OtherText.junsay.Get().text;
		}
		else if (input.HasFlag(AnnalieFlag))
		{
			value = R.OtherText.annaliesay.Get().text;
		}
		else
		{
			value = ((!input.HasFlag(SheepFlag)) ? R.OtherText.cowsay.Get().text : R.OtherText.sheepsay.Get().text);
		}
		if (input.HasFlag(GayFlag))
		{
			string text = argument;
			double num = new System.Random(BitConverter.ToInt32(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(text)), 0) + 33027).NextDouble(0.0, 1.0);
			argument = LOC.Other.CowGayMeter(text, num);
		}
		StringBuilderCache.AcquireManual(out var stringBuilder);
		argument = argument.Replace("\\n", "\n");
		List<StringUtils.LineSplitInfo> lineSplits;
		int maxLineLength;
		using (argument.SplitIntoLinesThatFit(allowedLineWidth, out lineSplits, out maxLineLength))
		{
			int num2 = Mathf.Max(a, maxLineLength);
			stringBuilder.Append(' ');
			stringBuilder.Append('-', num2 + 2);
			stringBuilder.Append(" \n");
			foreach (StringUtils.LineSplitInfo item in lineSplits)
			{
				stringBuilder.Append("< ");
				item.AppendToStringBuilder(argument, stringBuilder);
				stringBuilder.Append(' ', num2 - item.Length);
				stringBuilder.Append(" >\n");
			}
			stringBuilder.Append(' ');
			stringBuilder.Append('-', num2 + 2);
			stringBuilder.Append(" \n");
			stringBuilder.AppendLine(value);
		}
		await popup.PrintLineDelay(stringBuilder, 0.0025f, 0.0025f, cancellationToken);
	}
}
