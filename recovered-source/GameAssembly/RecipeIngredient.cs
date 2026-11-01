using System.Collections.Generic;
using System.Text;

public abstract class RecipeIngredient
{
	public readonly int Count;

	public readonly bool IsOptional;

	public virtual bool RequiresHelp => false;

	protected RecipeIngredient(int count, bool isOptional)
	{
		Count = count;
		IsOptional = isOptional;
	}

	public virtual string GetHelpDescription()
	{
		return LOC.UI.UI_Error;
	}

	protected abstract int GetApproximateSinglePrice();

	public int GetApproximatePrice()
	{
		return GetApproximateSinglePrice() * Count;
	}

	public string GetName()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			AppendName(stringBuilder);
			return stringBuilder.ToString();
		}
	}

	public void AppendName(StringBuilder stringBuilder)
	{
		AppendSingleName(stringBuilder);
		if (Count > 1)
		{
			stringBuilder.Append(" x ");
			stringBuilder.Append(Count);
		}
		if (IsOptional)
		{
			stringBuilder.Append(" (");
			stringBuilder.Append(LOC.UI.CookingSong_IngredientOptional);
			stringBuilder.Append(")");
		}
	}

	protected abstract void AppendSingleName(StringBuilder stringBuilder);

	public abstract IEnumerable<Item> FindInInventory(Item.ItemManager itemManager);
}
