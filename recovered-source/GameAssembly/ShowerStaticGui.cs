using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowerStaticGui : StaticGuiBase
{
	[Header("Other")]
	[SerializeField]
	private SoapScript soapPrefab;

	[Header("Auto")]
	[SerializeField]
	private TextMeshProUGUI A_Clean_Text;

	[SerializeField]
	private Slider A_Clean_Progress_Bar;

	[SerializeField]
	private TextMeshProUGUI A_Soap_Text;

	[SerializeField]
	private Slider A_Soap_Left_Bar;

	public SoapScript SoapPrefab => soapPrefab;

	private bool CleaningProgressBarFull => A_Clean_Progress_Bar.value >= 1f;

	private bool OutOfSoap => A_Soap_Left_Bar.value <= 0f;

	public void SetCleaningProgressBar(float percentage)
	{
		bool cleaningProgressBarFull = CleaningProgressBarFull;
		A_Clean_Progress_Bar.value = percentage;
		if (cleaningProgressBarFull != CleaningProgressBarFull)
		{
			UpdateCleaningProgressText();
		}
	}

	private void UpdateCleaningProgressText()
	{
		if (CleaningProgressBarFull)
		{
			A_Clean_Text.text = LOC.UI.Shower_CleanedInfo;
			A_Clean_Progress_Bar.gameObject.SetActive(value: false);
		}
		else
		{
			A_Clean_Text.text = LOC.UI.Shower_Cleanliness;
			A_Clean_Progress_Bar.gameObject.SetActive(value: true);
		}
	}

	public void SetSoapLeftBar(float percentage)
	{
		bool outOfSoap = OutOfSoap;
		A_Soap_Left_Bar.value = percentage;
		if (outOfSoap != OutOfSoap)
		{
			UpdateSoapText();
		}
	}

	private void UpdateSoapText()
	{
		if (OutOfSoap)
		{
			A_Soap_Text.text = LOC.UI.Shower_SoapLeftNoneInfo;
			A_Soap_Left_Bar.gameObject.SetActive(value: false);
		}
		else
		{
			A_Soap_Text.text = LOC.UI.Shower_SoapLeft;
			A_Soap_Left_Bar.gameObject.SetActive(value: true);
		}
	}

	public void InitGui()
	{
		SetCleaningProgressBar(0f);
		UpdateCleaningProgressText();
		SetSoapLeftBar(0f);
		UpdateSoapText();
	}

	public void EndGui()
	{
	}

	protected override void FillReferencesInherited()
	{
		_ = transform;
	}
}
