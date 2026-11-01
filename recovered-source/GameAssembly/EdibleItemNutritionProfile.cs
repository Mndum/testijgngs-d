using System.Linq;
using System.Text;
using UnityEngine;

public class EdibleItemNutritionProfile : GameStatContainer
{
	public class NutritionProfilePool : BetterObjectPool<EdibleItemNutritionProfile>
	{
		protected override void OnAfterAddedToPool(EdibleItemNutritionProfile obj)
		{
		}

		protected override void OnBeforeAcquiredFromPool(EdibleItemNutritionProfile obj)
		{
			obj.ResetAllStats();
		}

		public NutritionProfilePool()
			: base(16)
		{
		}
	}

	public GameStat<bool> IsSafeToEat = new GameStat<bool>(defaultValue: true);

	public GameStat<GameVariables.EatSound> EatSound = new GameStat<GameVariables.EatSound>(GameVariables.EatSound.Crunch);

	public GameStatFixedValue Satiation = new GameStatFixedValue(0.1f);

	public GameStatFixedValue RawHealthChange = new GameStatFixedValue(0f);

	public GameStatFixedValue StaminaChange = new GameStatFixedValue(0f);

	public GameStatFixedValue MentalHealthChange = new GameStatFixedValue(0f);

	public GameStatFixedValue MaxSafeCumChange = new GameStatFixedValue(0f);

	public GameStatFixedValue MaxUnsafeCumChange = new GameStatFixedValue(0f);

	public GameStatFixedValue CurrentCumChange = new GameStatFixedValue(0f);

	public GameStatFixedValue CurrentCumChangePercentMaxCum = new GameStatFixedValue(0f);

	public static readonly NutritionProfilePool Pool = new NutritionProfilePool();

	public void Apply(GameVariables gameVariables)
	{
		gameVariables.Satiation += Satiation.Value;
		gameVariables.MentalHealth += MentalHealthChange.Value;
		gameVariables.Health += RawHealthChange.Value;
		gameVariables.Stamina += StaminaChange.Value;
		float value = MaxSafeCumChange.Value;
		float num = gameVariables.MaxCum + value;
		if (num < 350f)
		{
			gameVariables.MaxCum += value;
		}
		else if (num < 700f)
		{
			gameVariables.MaxCum += value * 0.75f;
		}
		else if (num < 1000f)
		{
			gameVariables.MaxCum += value * 0.001f;
		}
		else
		{
			gameVariables.MaxCum += value * 0.0001f;
		}
		gameVariables.RemainingCum += CurrentCumChange.Value + CurrentCumChangePercentMaxCum.Value * gameVariables.MaxCum;
	}

	public void AddDishSubItem(EdibleItemNutritionProfile subItemProfile, int count)
	{
		foreach (var (gameStat, gameStat2) in IterateAllStats().Zip(subItemProfile.IterateAllStats(), (IGameStat t, IGameStat other) => (t: t, other: other)))
		{
			if (gameStat is GameStatFixedValue gameStatFixedValue)
			{
				GameStatFixedValue gameStatFixedValue2 = (GameStatFixedValue)gameStat2;
				gameStatFixedValue.Value += gameStatFixedValue2.Value * (float)count;
			}
		}
	}

	public void FinishDishCalculation(float dishQuality, int itemPriceSum, int itemCount)
	{
		float num = (1f + (float)itemPriceSum / 2500f) * dishQuality;
		float num2 = Mathf.Max(dishQuality - 0.5f, 0f);
		MaxSafeCumChange.Value += num * 4f + num2 * 5f;
		if ((double)dishQuality > 0.8 && itemPriceSum > 1000)
		{
			MentalHealthChange.Value += 0.1f + num2 * 0.05f;
		}
	}

	public void AppendDescribeEffects(StringBuilder stringBuilder)
	{
		if (!IsSafeToEat.Value)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_YouNeedToCookThis);
			return;
		}
		GameVariables current = GameVariables.Current;
		stringBuilder.AppendLineSubHeader(LOC.UI.ItemDescription_NutritionalValues);
		stringBuilder.AppendStartDisplay(LOC.UI.FoodSatiation_Label);
		float num = Satiation.Calculate();
		if (num >= 0.9f)
		{
			stringBuilder.Append(LOC.Other.FoodSatiation_FullMeal);
		}
		else if (num >= 0.5f)
		{
			stringBuilder.Append(LOC.Other.FoodSatiation_Meal);
		}
		else if (num >= 0.3f)
		{
			stringBuilder.Append(LOC.Other.FoodSatiation_LightMeal);
		}
		else if (num >= 0.1f)
		{
			stringBuilder.Append(LOC.Other.FoodSatiation_Snack);
		}
		else
		{
			stringBuilder.Append(LOC.Other.FoodSatiation_BarelyABite);
		}
		stringBuilder.Append(" (");
		stringBuilder.AppendPercent(num);
		stringBuilder.Append(" ");
		stringBuilder.Append(LOC.UI.FoodSatiation_Satiation);
		stringBuilder.Append(")");
		stringBuilder.AppendLine();
		float num2 = CurrentCumChange.Calculate() + CurrentCumChangePercentMaxCum.Calculate() * current.MaxCum;
		if ((double)num2 > 0.1)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_CurrentCumIncrease(num2));
		}
		else if ((double)num2 < -0.1)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_CurrentCumDecrease(0f - num2));
		}
		float num3 = MaxSafeCumChange.Calculate();
		float num4 = MaxUnsafeCumChange.Calculate();
		float num5 = num3 + num4;
		if ((double)num5 > 0.1)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_MaxCumIncrease(num5));
		}
		else if ((double)num5 < -0.1)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_MaxCumDecrease(0f - num5));
		}
		float num6 = StaminaChange.Calculate();
		if (num6 > 0.1f)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_StaminaRecovery(num6));
		}
		else if (num6 < -0.1f)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_StaminaLoss(0f - num6));
		}
		float num7 = MentalHealthChange.Calculate();
		if ((double)num7 >= 0.2)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_MentalHealthIncreaseBig);
		}
		else if ((double)num7 > 0.1)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_MentalHealthIncreaseModerate);
		}
		else if ((double)num7 > 0.01)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_MentalHealthIncreaseSmall);
		}
		else if ((double)num7 < -0.01)
		{
			stringBuilder.AppendLine(LOC.UI.FoodEffect_MentalHealthDecrease);
		}
		stringBuilder.TMPAppendFinePrintSizeTag();
		stringBuilder.AppendLine(LOC.UI.FoodEffect_WarningPrompt);
		stringBuilder.TMPAppendCloseSizeTag();
		stringBuilder.AppendLine();
	}
}
