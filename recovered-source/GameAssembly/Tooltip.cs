using TMPro;
using UnityEngine;

[PooledPrefabConfig(3)]
public class Tooltip : MonoBehaviour, IPooledPrefab
{
	public TextMeshProUGUI textMeshPro;

	public GameObject SourcePrefab { get; set; }

	public void SetText(string text)
	{
		textMeshPro.SetText(text);
	}

	public void AfterReturnedToPool()
	{
		textMeshPro.text = "";
	}

	public void BeforeGetFromPool()
	{
	}
}
