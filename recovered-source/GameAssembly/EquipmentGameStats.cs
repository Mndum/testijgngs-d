using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

public class EquipmentGameStats : GameStatContainer
{
	private Dictionary<SusArea, float> _areas = new Dictionary<SusArea, float>();

	public Result CanGoOutside;

	public float ArmLRating;

	public float ArmRRating;

	public float LegLRating;

	public float LegRRating;

	public float SkinRating;

	public float UpperBodyRating;

	public float LowerBodyRating;

	public float AverageQuality;

	public float SusRating;

	public void Recalculate(GameVariables gameVariables)
	{
		ResetStat();
		Item.ItemManager itemManager = gameVariables.itemManager;
		foreach (Item item in itemManager.BotEquippedItems())
		{
			ItemPrefab itemPrefab = item.GetItemPrefab();
			if (itemPrefab.SusModifiers != null)
			{
				SusModifier[] susModifiers = itemPrefab.SusModifiers;
				foreach (SusModifier susModifier in susModifiers)
				{
					_areas.AddToSumDictionary(susModifier.Area, susModifier.Modifier);
				}
			}
		}
		foreach (SusArea item2 in _areas.Keys.ToList())
		{
			_areas[item2] = Mathf.Clamp01(_areas[item2]);
		}
		Item firstItemAtSlot = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ArmL.Id);
		Item firstItemAtSlot2 = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ArmR.Id);
		Item firstItemAtSlot3 = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.LegL.Id);
		Item firstItemAtSlot4 = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.LegR.Id);
		if (firstItemAtSlot == null || firstItemAtSlot2 == null || firstItemAtSlot3 == null || firstItemAtSlot4 == null)
		{
			CanGoOutside = Result.FailWithDescription(LOC.Other.Outside_AllLimbs);
			return;
		}
		if (!IsCovered(SusArea.UpperBody))
		{
			CanGoOutside = Result.FailWithDescription(LOC.Other.Outside_UpperBody);
			return;
		}
		if (!IsCovered(SusArea.LowerBody))
		{
			CanGoOutside = Result.FailWithDescription(LOC.Other.Outside_LowerBody);
			return;
		}
		Item firstItemAtSlot5 = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ShoeL.Id);
		Item firstItemAtSlot6 = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ShoeR.Id);
		if (firstItemAtSlot5 == null || firstItemAtSlot6 == null)
		{
			CanGoOutside = Result.FailWithDescription(LOC.Other.Outside_Shoes);
			return;
		}
		ArmLRating = firstItemAtSlot.InverseNormalizedQuality * (1f - GetCover(SusArea.ArmL));
		ArmRRating = firstItemAtSlot2.InverseNormalizedQuality * (1f - GetCover(SusArea.ArmR));
		LegLRating = firstItemAtSlot3.InverseNormalizedQuality * (1f - GetCover(SusArea.LegL));
		LegRRating = firstItemAtSlot4.InverseNormalizedQuality * (1f - GetCover(SusArea.LegR));
		SkinRating = 1f - GetCover(SusArea.Skin);
		UpperBodyRating = SkinRating * (1f - GetCover(SusArea.UpperBody));
		LowerBodyRating = SkinRating * (1f - GetCover(SusArea.LowerBody));
		AverageQuality = (from t in itemManager.BotEquippedItems()
			where t.IsIllegal
			select (!t.GetItemPrefab().HasQuality) ? 0f : t.InverseNormalizedQuality).DefaultIfEmpty(0f).Average();
		SusRating = new float[8] { ArmLRating, ArmRRating, LegLRating, LegRRating, UpperBodyRating, LowerBodyRating, AverageQuality, AverageQuality }.Average();
		CanGoOutside = Result.Success;
	}

	public float GetCover(SusArea area)
	{
		return _areas.GetValueOrDefault(area, 0f);
	}

	public bool IsCovered(SusArea area)
	{
		return GetCover(area) > 0.1f;
	}

	public bool WouldBeConsideredClothed()
	{
		if (IsCovered(SusArea.UpperBody))
		{
			return IsCovered(SusArea.LowerBody);
		}
		return false;
	}

	public bool WouldBeConsideredNaked()
	{
		return !WouldBeConsideredClothed();
	}

	public void ResetStat()
	{
		_areas.Clear();
		CanGoOutside = Result.Fail;
		ArmLRating = 0f;
		ArmRRating = 0f;
		LegLRating = 0f;
		LegRRating = 0f;
		SkinRating = 0f;
		UpperBodyRating = 0f;
		LowerBodyRating = 0f;
		AverageQuality = 0f;
		SusRating = 0f;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			FieldInfo[] fields = GetType().GetFields();
			foreach (FieldInfo fieldInfo in fields)
			{
				object value = fieldInfo.GetValue(this);
				string text = ((value == null) ? "null" : value.ToString());
				stringBuilder.AppendLine(fieldInfo.Name + " : " + text);
			}
			stringBuilder.AppendLine("Sus:" + SusRating);
			stringBuilder.AppendLine("AverageQuality:" + AverageQuality);
			return stringBuilder.ToString();
		}
	}
}
