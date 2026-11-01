using System.Text;
using TMPro;
using UnityEngine;

public class EmailDisplay : DataDisplay<NewEmail>
{
	[SerializeField]
	private TextMeshProUGUI description;

	protected override void SetData(NewEmail newValue)
	{
		_current = newValue;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(LOC.UI.bangdotic_email_From(newValue.Sender));
			if (!newValue.read)
			{
				stringBuilder.Append("<b>");
				stringBuilder.Append(LOC.UI.bangdotic_email_UnreadPostfix);
				stringBuilder.Append(newValue.Topic);
				stringBuilder.Append("</b>");
			}
			else
			{
				stringBuilder.Append(newValue.Topic);
			}
			description.SetText(stringBuilder);
		}
	}
}
