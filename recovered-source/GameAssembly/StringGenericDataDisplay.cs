using TMPro;
using UnityEngine;

public class StringGenericDataDisplay : MonoBehaviour, IPooledPrefab
{
	[SerializeField]
	private TextMeshProUGUI _textMeshProUGUI;

	public TextMeshProUGUI MainText => _textMeshProUGUI;

	public GameObject SourcePrefab { get; set; }

	public void AfterReturnedToPool()
	{
		_textMeshProUGUI.text = "";
	}

	public void BeforeGetFromPool()
	{
	}
}
