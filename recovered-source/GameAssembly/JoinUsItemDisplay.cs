using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinUsItemDisplay : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI tmp;

	[SerializeField]
	private RectTransform realBody;

	[SerializeField]
	private LayoutElement layoutElement;

	[SerializeField]
	private JoinUsItemScoreDisplay scoreDisplay;

	public void SetEntry(JoinUsBlogItem entry)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			switch (entry.PostDecorations)
			{
			case JoinUsBlogItem.SpecialPostDecorations.AccountDeleted:
				stringBuilder.AppendColorTagStart(MyColor32Presets.MatrixGreen);
				stringBuilder.Append("<s><b>");
				stringBuilder.Append(entry.Author.GetAuthorLocalizedString());
				stringBuilder.Append("</b></s>");
				stringBuilder.AppendColorTagEnd();
				stringBuilder.Append("(");
				stringBuilder.AppendColorTagStart(MyColor32Presets.Red);
				stringBuilder.Append(LOC.UI.Blog_AccountDeleted);
				stringBuilder.AppendColorTagEnd();
				stringBuilder.Append(")");
				break;
			case JoinUsBlogItem.SpecialPostDecorations.AccountBanned:
				stringBuilder.AppendColorTagStart(MyColor32Presets.MatrixGreen);
				stringBuilder.Append("<s><b>");
				stringBuilder.Append(entry.Author.GetAuthorLocalizedString());
				stringBuilder.Append("</b></s>");
				stringBuilder.AppendColorTagEnd();
				stringBuilder.Append("(");
				stringBuilder.AppendColorTagStart(MyColor32Presets.Red);
				stringBuilder.Append(LOC.UI.Blog_AccountBanned);
				stringBuilder.AppendColorTagEnd();
				stringBuilder.Append(")");
				break;
			default:
				stringBuilder.AppendColorTagStart(MyColor32Presets.MatrixGreen);
				stringBuilder.Append("<b>");
				stringBuilder.Append(entry.Author.GetAuthorLocalizedString());
				stringBuilder.Append("</b>  ");
				stringBuilder.AppendColorTagEnd();
				break;
			}
			stringBuilder.AppendLine();
			if (entry.ReplyTo != null)
			{
				stringBuilder.AppendColorTagStart(MyColor32Presets.MatrixGreen);
				stringBuilder.Append("@");
				stringBuilder.Append(entry.ReplyTo.Author.GetAuthorLocalizedString());
				stringBuilder.Append(" ");
				stringBuilder.AppendColorTagEnd();
			}
			stringBuilder.Append(entry.GetTextLocalizedString());
			float num = 32f;
			scoreDisplay.DisplayScore(entry);
			float num2 = 10f;
			float num3 = Mathf.Min(entry.Depth, 6) * 100;
			float num4 = 1840f - num3 - num2 * 2f;
			realBody.SetLeft(num3);
			tmp.SetText(stringBuilder);
			Vector2 preferredValues = tmp.GetPreferredValues(num4, 1500f);
			float right = Mathf.Max(num4 - preferredValues.x, 0f);
			realBody.SetRight(right);
			layoutElement.preferredHeight = preferredValues.y + num2 * 2f + num;
		}
	}
}
