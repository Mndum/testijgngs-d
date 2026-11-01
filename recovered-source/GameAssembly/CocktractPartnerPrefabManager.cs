using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public static class CocktractPartnerPrefabManager
{
	public class CocktractCompanyPrefabManagerData : PrefabManagerDataOneToOne<string, CocktractPartnerPrefab, CocktractPartner>
	{
		protected override CocktractPartnerPrefab GenerateEmpty(string key)
		{
			return new GenericCocktractPartnerPrefab(key, 2147483647);
		}

		protected override CocktractPartner CreateSerializedPart(string key, CocktractPartnerPrefab prefab)
		{
			return new CocktractPartner(key, prefab);
		}
	}

	private static CocktractCompanyPrefabManagerData data = new CocktractCompanyPrefabManagerData();

	public static string luxeId = "Luxe";

	public static string stilouId = "Stilou";

	public static string volksAutoId = "VolksAuto";

	public static string monizmedId = "Monizmed";

	public static string diabeteId = "Diabete";

	public static string cazinoId = "Cazino";

	public static CocktractPartnerPrefab GetCocktractsCompanyPrefab(string name)
	{
		return data.Get(name);
	}

	public static void EnsureCocktractPartners(List<CocktractPartner> savedCompanies)
	{
		data.EnsureSerializedDataExistsAndHasPrefabs(savedCompanies);
		savedCompanies.Sort((CocktractPartner a, CocktractPartner b) => a.Prefab.Order.CompareTo(b.Prefab.Order));
	}

	[LoadableStaticMethod(null)]
	private static UniTask Load()
	{
		data.Init(new LuxeCocktractPrefab(0), new StilouPrefab(10), new VolksautoCocktractsPrefab(20), new MonizmedPrefab(30), new DiabeteCocktractsPrefab(40), new CazinoCocktractsPrefab(50));
		return UniTask.CompletedTask;
	}
}
