using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToyGeneratorDisplay : DataDisplay<ToySingleton.ToyGenerator>
{
	[SerializeField]
	private TextMeshProUGUI text;

	[SerializeField]
	private Button infoButton;

	protected override void SetData(ToySingleton.ToyGenerator newValue)
	{
		_current = newValue;
		text.text = _current.NiceName.GetValue();
		if (_current.InfoButtonAction != null)
		{
			infoButton.onClick.SetListener(_current.InfoButtonAction.Invoke);
			infoButton.gameObject.SetActive(value: true);
		}
		else
		{
			infoButton.onClick.RemoveAllListeners();
			infoButton.gameObject.SetActive(value: false);
		}
	}
}
