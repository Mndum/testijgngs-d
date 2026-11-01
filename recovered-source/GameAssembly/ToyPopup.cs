using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToyPopup : PrefabPopup
{
	[SerializeField]
	private TextMeshProUGUI currentToyInfo;

	[SerializeField]
	private TextMeshProUGUI currentToyDynamicInfo;

	[SerializeField]
	private Slider currentSlider;

	[SerializeField]
	private ItemList toyItemList;

	[SerializeField]
	private Toggle driverDepthToggle;

	[SerializeField]
	private Button disconnectButton;

	[SerializeField]
	private Button calibrateButton;

	[SerializeField]
	private TextMeshProUGUI noSupportedToysInfo;

	public override bool DontDrawPrevious => true;

	public void RefreshList()
	{
		ReadOnlyCollection<ToySingleton.ToyGenerator> list = SingletonMonoBehaviour<ToySingleton>.Instance.ToyGenerators;
		toyItemList.ItemCountFunc = () => list.Count;
		toyItemList.ItemUpdatedAction = (GameObject go, int index, bool focused) =>
		{
			if (list.TryGetValue(index, out var value))
			{
				go.GetComponent<ToyGeneratorDisplay>().Current = value;
			}
		};
		toyItemList.ItemClickedAction = (int index) =>
		{
			if (list.TryGetValue(index, out var value))
			{
				IToy currentToy = SingletonMonoBehaviour<ToySingleton>.Instance.CurrentToy;
				if (currentToy != null && currentToy.GeneratedFrom == value)
				{
					UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.Options_AlreadyConnectedPrompt);
				}
				else
				{
					SingletonMonoBehaviour<ToySingleton>.Instance.SetToy(value);
					UiOverlay.Instance.ProgressPopup(LOC.UI.Options_Connecting, LOC.UI.Options_TryingToConnect, SingletonMonoBehaviour<ToySingleton>.Instance.CurrentToy.Connect, RefreshCurrentInfoFull);
				}
			}
		};
		toyItemList.OnAfterUpdated = (int count) =>
		{
			noSupportedToysInfo.gameObject.SetActive(count == 0);
		};
		toyItemList.UpdateAll();
	}

	private void PrepareListeners()
	{
		disconnectButton.onClick.SetListener(() =>
		{
			SingletonMonoBehaviour<ToySingleton>.Instance.DisconnectCurrentToy();
			RefreshCurrentInfoFull();
		});
		calibrateButton.onClick.SetListener(() =>
		{
			SingletonMonoBehaviour<ToySingleton>.Instance.CurrentToy?.CalibrateStart();
		});
	}

	public void RefreshCurrentInfoFull()
	{
		IToy currentToy = SingletonMonoBehaviour<ToySingleton>.Instance.CurrentToy;
		if (currentToy != null)
		{
			currentToyInfo.text = currentToy.GeneratedFrom.NiceName.Value + "\n" + currentToy.StatusDescription;
			disconnectButton.gameObject.SetActive(value: true);
			calibrateButton.gameObject.SetActive(currentToy.Active && !OptionsStatic.AGUIUseDriverDepth.Value);
			currentToy.Update();
			float insertion = currentToy.Insertion;
			currentSlider.value = insertion;
			currentSlider.gameObject.SetActive(value: true);
			currentToyDynamicInfo.text = LOC.UI.Options_ToyCurrentInsertion(insertion);
			currentToyDynamicInfo.gameObject.SetActive(value: true);
			driverDepthToggle.gameObject.SetActive(value: true);
		}
		else
		{
			currentToyInfo.text = LOC.UI.Options_NoToy;
			disconnectButton.gameObject.SetActive(value: false);
			calibrateButton.gameObject.SetActive(value: false);
			currentSlider.gameObject.SetActive(value: false);
			currentToyDynamicInfo.gameObject.SetActive(value: false);
			driverDepthToggle.gameObject.SetActive(value: false);
		}
	}

	public override void Open()
	{
		base.Open();
		PrepareListeners();
		RefreshList();
		RefreshCurrentInfoFull();
		driverDepthToggle.onValueChanged.RemoveAllListeners();
		driverDepthToggle.isOn = OptionsStatic.AGUIUseDriverDepth.Value;
		driverDepthToggle.onValueChanged.AddListener(driverDepthToggleValueChanged);
	}

	private void driverDepthToggleValueChanged(bool value)
	{
		OptionsStatic.AGUIUseDriverDepth.Value = value;
		RefreshCurrentInfoFull();
	}

	public void Update()
	{
		RefreshCurrentInfoFull();
	}
}
