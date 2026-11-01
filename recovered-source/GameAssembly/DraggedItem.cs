using TMPro;
using UnityEngine;

[PooledPrefabConfig(2)]
public class DraggedItem : MonoBehaviour, IPooledPrefab
{
	[SerializeField]
	private TextMeshProUGUI _itemName;

	public TextMeshProUGUI ItemName => _itemName;

	public GameObject SourcePrefab { get; set; }

	public void AfterReturnedToPool()
	{
		_itemName.text = "";
	}

	public void BeforeGetFromPool()
	{
	}
}
