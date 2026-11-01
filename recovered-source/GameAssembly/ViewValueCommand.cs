using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ViewValueCommand : AppCommand
{
	private static Dictionary<string, Func<string>> _nameToValueGetter = new Dictionary<string, Func<string>>(StringComparer.OrdinalIgnoreCase)
	{
		["lust"] = () => GameVariables.Current.lust.ToString(),
		["sympathy"] = () => GameVariables.Current.sympathy.ToString(),
		["mood"] = () => GameVariables.Current.Mood.ToString("0.0f"),
		["isNaked"] = () => GameVariables.Current.IsNaked.ToString(),
		["cumInsideVaginaMl"] = () => LOC.UI.MillilitersDisplay(GameVariables.Current.CumInside),
		["cumInsideAnalMl"] = () => LOC.UI.MillilitersDisplay(GameVariables.Current.CumInsideAnal),
		["cumInsideStomachMl"] = () => LOC.UI.MillilitersDisplay(GameVariables.Current.CumInsideStomach),
		["masterCumExtractedMl"] = () => LOC.UI.MillilitersDisplay(GameVariables.Current.mlOfCumWasted),
		["timesCameInsideVagina"] = () => GameVariables.Current.timesCameInside.ToString(),
		["timesCameInsideAnal"] = () => GameVariables.Current.timesCameInsideAnal.ToString(),
		["timesCameMouth"] = () => GameVariables.Current.timesCameInMouth.ToString(),
		["timesCameOutside"] = () => GameVariables.Current.timesCameOutside.ToString(),
		["timesCameThighjob"] = () => GameVariables.Current.timesCameThighjob.ToString(),
		["horniness"] = () => GameVariables.Current.PotentialHorniness.ToString("P2"),
		["masterName"] = () => GameVariables.Current.playerName,
		["selfName"] = () => GameVariables.Current.botName,
		["cumOnBody"] = () => GameVariables.Current.IsThereCumOnHer.ToString(),
		["timeSinceLastHeadpat"] = () => GlobalFunctions.TimePeriodHours(GameVariables.Current.TimeSinceLastHeadpat),
		["timeSinceLastSex"] = () => GlobalFunctions.TimePeriodHours(GameVariables.Current.TimeSinceLastFuck),
		["timeSinceLastTalkWithMaster"] = () => GlobalFunctions.TimePeriodHours(GameVariables.Current.TimeSinceLastTalk)
	};

	public override string CommandName => "vv";

	public override string CommandDescription => LOC.UI.ViewVariableCommandDescription(CommandName, _nameToValueGetter.First().Key);

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		GetArgument(input, out var value);
		if (_nameToValueGetter.TryGetValue(value, out var value2))
		{
			await popup.PrintLineDelay(value + " = " + value2(), 0.0025f, 0.0025f, cancellationToken);
			return;
		}
		(string key, double dist) closest = (from keyValuePair in _nameToValueGetter
			select (key: keyValuePair.Key, dist: GlobalFunctions.LevenshteinDistance(value, keyValuePair.Key)) into pair
			orderby pair.dist
			select pair).FirstOrDefault();
		await popup.PrintLineDelay(LOC.UI.ValueNotFound(value).TMPColorSurround(Color.red), 0.0025f, 0.0025f, cancellationToken);
		await popup.PrintLineDelay(LOC.UI.ClosestMatch(closest.key), 0.0025f, 0.0025f, cancellationToken);
	}
}
