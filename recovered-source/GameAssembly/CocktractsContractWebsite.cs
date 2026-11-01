using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CocktractsContractWebsite : CocktractsWebsiteBase
{
	[SerializeField]
	private ContractDisplay contractDisplay;

	private CocktractContract _cocktractContract;

	private string _source;

	private static readonly string SourceArgument = "Source";

	private static readonly string IndexArgument = "Index";

	private bool redirectToHell;

	public static GameUrlWithArguments CreateUrl(string source, int index)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string> { [SourceArgument] = source };
		dictionary[IndexArgument] = index.ToString(CultureInfo.InvariantCulture);
		return new GameUrlWithArguments("cocktracts.ic/contract", dictionary);
	}

	public override void ProcessUrl(GameUrlWithArguments gameUrlWithArguments)
	{
		if (gameUrlWithArguments.Arguments.TryGetValue(SourceArgument, out _source))
		{
			if (!gameUrlWithArguments.Arguments.TryGetValue(IndexArgument, out var value) || !int.TryParse(value, out var result))
			{
				result = 0;
			}
			if (!CTM.TryGetContract(_source, result, out _cocktractContract) && (_source == "666" || value == "666"))
			{
				redirectToHell = true;
			}
		}
		CurrentGameUrl = gameUrlWithArguments;
	}

	public override IEnumerator Open()
	{
		contractDisplay.Current = _cocktractContract;
		if (_cocktractContract != null)
		{
			_cocktractContract.Seen = true;
		}
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		if (redirectToHell)
		{
			BrowserPopup.ChangeWebsite(HellWebsite.WebsiteUrl);
			yield break;
		}
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
