using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class SelectionDataExtensions
{
	public const int MaxDescriptionLength = 100;

	public static void AppendDescribeSelection(this Selection<Item> selection, StringBuilder stringBuilder, int approximateMaxLength = 100)
	{
		selection.AppendDescribeSelection(stringBuilder, (Item item, StringBuilder builder) =>
		{
			item.GetNameStringBuilder(stringBuilder, ItemNameIncludeEnum.DefaultNoCount);
		}, approximateMaxLength);
	}

	public static void AppendDescribeSelection<T>(this Selection<T> selection, StringBuilder stringBuilder, Action<T, StringBuilder> writeNameAction, int approximateMaxLength = 100)
	{
		if (selection.SelectedUniqueItemCount > 0)
		{
			int num = 0;
			foreach (var (arg, value, num2) in selection.GetAllItemsWithCounts().Select((KeyValuePair<T, int> a, int i) => (Key: a.Key, Value: a.Value, i: i)))
			{
				if (stringBuilder.Length < approximateMaxLength)
				{
					if (num2 != 0)
					{
						stringBuilder.Append(", ");
					}
					writeNameAction(arg, stringBuilder);
					stringBuilder.Append(" x ");
					stringBuilder.Append(value);
				}
				else
				{
					num++;
				}
			}
			if (num > 0)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(LOC.UI.SelectionTruncatedSummaryEnd(num));
			}
		}
		else
		{
			stringBuilder.Append(LOC.UI.Items_NoItemsSelected);
		}
		if (selection.HasTotalItemCountLimit && selection.TotalItemCountLeft > 0)
		{
			stringBuilder.Append(' ');
			stringBuilder.AppendSurroundParentheses(LOC.UI.ItemsMissing(selection.TotalItemCountLeft));
		}
	}

	public static string ToStringDescribeSelection<T>(this Selection<T> selection, Action<T, StringBuilder> writeNameAction, int approximateMaxLength = 100)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			selection.AppendDescribeSelection(stringBuilder, writeNameAction, approximateMaxLength);
			return stringBuilder.ToString();
		}
	}
}
