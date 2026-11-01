using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class InternetSingleton : SingletonMonoBehaviour<InternetSingleton>
{
	public class WebsiteSearchData
	{
		public Website Website;

		public GameUrlWithArguments OutputAddress;
	}

	[SerializeField]
	private List<WebsiteSerializableAsset> websitePrefabs;

	[SerializeField]
	private List<WebsiteSerializableAsset> adWebsitePrefabs;

	[SerializeField]
	private Website website404Prefab;

	public IReadOnlyCollection<WebsiteSerializableAsset> ListedWebsitePrefabs => websitePrefabs.Where((WebsiteSerializableAsset t) => t.IsListed).ToList();

	public IReadOnlyCollection<WebsiteSerializableAsset> WebsitePrefabs => websitePrefabs;

	public IReadOnlyCollection<WebsiteSerializableAsset> AdWebsitePrefabs => adWebsitePrefabs;

	public string DefaultWebsite => "bang.ic";

	private WebsiteSearchData Website404(GameUrlWithArguments parsed)
	{
		return new WebsiteSearchData
		{
			Website = website404Prefab,
			OutputAddress = parsed
		};
	}

	public bool IsValidListedWebsiteExactMatch(string tmp)
	{
		GameUrl gameUrl = new GameUrl(tmp);
		return websitePrefabs.Exists((WebsiteSerializableAsset t) => t.IsListed && t.GameUrl.Equals(gameUrl));
	}

	public bool TryFindWebsite(GameUrlWithArguments parsed, out WebsiteSearchData websiteSearchData)
	{
		WebsiteSerializableAsset websiteSerializableAsset = websitePrefabs.Find((WebsiteSerializableAsset t) => t.GameUrl.Equals(parsed));
		if (websiteSerializableAsset != null)
		{
			Website website = websiteSerializableAsset.WebsiteReference.Get();
			if (!website.IsWebsiteUnlocked())
			{
				websiteSearchData = Website404(parsed);
				return false;
			}
			parsed.ChangeTo(websiteSerializableAsset.GameUrl);
			websiteSearchData = new WebsiteSearchData
			{
				Website = website,
				OutputAddress = parsed
			};
			return true;
		}
		var (websiteSerializableAsset2, num) = websitePrefabs.MaxBy((WebsiteSerializableAsset t) => t.GameUrl.IsSubDomain(parsed));
		if (websiteSerializableAsset2 != null && num > 0)
		{
			Website website2 = websiteSerializableAsset2.WebsiteReference.Get();
			if (!website2.IsWebsiteUnlocked())
			{
				websiteSearchData = Website404(parsed);
				return false;
			}
			parsed.ChangeTo(websiteSerializableAsset2.GameUrl);
			websiteSearchData = new WebsiteSearchData
			{
				Website = website2,
				OutputAddress = parsed
			};
			return true;
		}
		websiteSearchData = new WebsiteSearchData
		{
			Website = website404Prefab,
			OutputAddress = parsed
		};
		return false;
	}

	public WebsiteSerializableAsset GetRandomAdWebsite()
	{
		return adWebsitePrefabs.GetRandom();
	}
}
