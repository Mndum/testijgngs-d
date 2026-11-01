using System;
using System.Collections.Generic;
using System.Text;

public class EdibleItemPrefabData : LuaUtilities.ILuaTransferable, ICloneable<EdibleItemPrefabData>, ICloneable
{
	public EdibleTypeEnum EdibleType;

	public Action<Item, EdibleItemNutritionProfile> NutritionProfileFiller;

	public EdibleItemPrefabData(EdibleTypeEnum edibleType, Action<Item, EdibleItemNutritionProfile> nutritionProfileFiller)
	{
		EdibleType = edibleType;
		NutritionProfileFiller = nutritionProfileFiller;
	}

	public static EdibleItemPrefabData CreateInstance(EdibleTypeEnum edibleTypeEnum, Action<Item, EdibleItemNutritionProfile> getNutritionProfile)
	{
		return new EdibleItemPrefabData(edibleTypeEnum, getNutritionProfile);
	}

	public static EdibleItemPrefabData CreateInstanceDish(Action<Item, EdibleItemNutritionProfile> getNutritionProfile = null)
	{
		if (getNutritionProfile == null)
		{
			getNutritionProfile = DefaultDishNutritionProfileCalculator;
		}
		return new EdibleItemPrefabData(EdibleTypeEnum.Dish, getNutritionProfile);
	}

	public static void DefaultDishNutritionProfileCalculator(Item item, EdibleItemNutritionProfile profile)
	{
		List<Item> list;
		using (MyCollectionPool<Item>.GetListAndFill(item.GetDishItems(), out list))
		{
			if (list.Count <= 0)
			{
				return;
			}
			EdibleItemNutritionProfile pooled;
			using (EdibleItemNutritionProfile.Pool.Acquire(out pooled))
			{
				int num = 0;
				int num2 = 0;
				foreach (Item dishItem in item.GetDishItems())
				{
					EdibleItemPrefabData edibleData = dishItem.GetItemPrefab().EdibleData;
					if (edibleData != null)
					{
						num2 += dishItem.GetPrice();
						pooled.ResetAllStats();
						edibleData.FillNutritionProfile(dishItem, pooled);
						profile.AddDishSubItem(pooled, dishItem.GetCount());
						num++;
					}
				}
				profile.FinishDishCalculation(item.DishQuality, num2, num);
			}
		}
	}

	public void FillNutritionProfile(Item item, EdibleItemNutritionProfile profile)
	{
		NutritionProfileFiller?.Invoke(item, profile);
	}

	public string ToLuaString()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine("(function()");
			stringBuilder.AppendLine("local ntf = function(item, nutritionProfile)");
			stringBuilder.AppendLine("-- Here you can set the nutritional values of the dish. This data is dynamic and cannot be exported to lua!");
			stringBuilder.AppendLine("-- For example: ");
			stringBuilder.AppendLine("nutritionProfile.Satiation.Value = 0.1");
			stringBuilder.AppendLine("end");
			stringBuilder.AppendLine("local ed = EdibleItemPrefabData.CreateInstance(" + EdibleType.ToLuaString() + ", ntf)");
			stringBuilder.Append("\treturn ed end)()");
			return stringBuilder.ToString();
		}
	}

	public EdibleItemPrefabData Clone()
	{
		return (EdibleItemPrefabData)MemberwiseClone();
	}
}
