using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

public class FishingResults : IDisposable
{
	public readonly FishingStartInfo FishingStartInfo;

	private List<Item> _caughtItems = MyCollectionPool<Item>.GetList();

	private List<FishRegisteringFlagsEnum> _caughtItemsFlags = MyCollectionPool<FishRegisteringFlagsEnum>.GetList();

	private FishingDuelResults _duelResults;

	public int CaughtFishes { get; private set; }

	public int CaughtItems { get; private set; }

	public IEnumerable<(Item item, FishRegisteringFlagsEnum flag)> ItemsWithFlags => _caughtItems.Zip(_caughtItemsFlags, (Item a, FishRegisteringFlagsEnum b) => (a: a, b: b));

	private FishingResults(FishingStartInfo fishingStartInfo)
	{
		FishingStartInfo = fishingStartInfo;
	}

	public static FishingResults CreateNormal(FishingStartInfo fishingStartInfo)
	{
		return new FishingResults(fishingStartInfo);
	}

	public static FishingResults CreateDuel(FishingStartInfo fishingStartInfo)
	{
		return new FishingResults(fishingStartInfo)
		{
			_duelResults = new FishingDuelResults()
		};
	}

	public bool TryGetDuelResults(out FishingDuelResults duelResults)
	{
		duelResults = _duelResults;
		return duelResults != null;
	}

	[NotNull]
	public FishingDuelResults GetDuelResultsOrErr()
	{
		if (_duelResults == null)
		{
			throw new Exception("Duel results is null!");
		}
		return _duelResults;
	}

	public void AddItem(Item item, FishRegisteringFlagsEnum result)
	{
		_caughtItems.Add(item);
		_caughtItemsFlags.Add(result);
		if (item.IsFish)
		{
			CaughtFishes++;
		}
		else
		{
			CaughtItems++;
		}
	}

	public void Dispose()
	{
		MyCollectionPool<Item>.ReleaseAndSetNull(ref _caughtItems);
		MyCollectionPool<FishRegisteringFlagsEnum>.ReleaseAndSetNull(ref _caughtItemsFlags);
	}
}
