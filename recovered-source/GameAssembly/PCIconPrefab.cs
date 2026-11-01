using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PCIconPrefab : MonoBehaviour, IPooledPrefab
{
	[SerializeField]
	private Image iconImage;

	[SerializeField]
	private TextMeshProUGUI iconText;

	[SerializeField]
	private Button button;

	public TextMeshProUGUI IconText => iconText;

	public GameObject SourcePrefab { get; set; }

	public void Init(Sprite sprite, string text, UnityAction onClick)
	{
		iconImage.sprite = sprite;
		iconText.text = text;
		button.onClick.SetListenerEnsureNotNull(onClick);
	}

	public void AfterReturnedToPool()
	{
		Init(null, "", null);
	}

	public void BeforeGetFromPool()
	{
	}
}
