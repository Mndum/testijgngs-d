using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RoomButtonPooledPrefab : MonoBehaviour, IPooledPrefab
{
	[SerializeField]
	private Button button;

	[SerializeField]
	private TextMeshProUGUI tmp;

	[SerializeField]
	private RectTransform rectTransform;

	[SerializeField]
	private RectTransform indicatorCenter;

	public Button Button => button;

	public TextMeshProUGUI ButtonText => tmp;

	public RectTransform RectTransform => rectTransform;

	public GameObject SourcePrefab { get; set; }

	public void SetAnchoredPositionToRect(Rect rect)
	{
		rectTransform.anchoredPosition = rect.min;
		rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
	}

	public void SetLocalPosition(Vector2 localPosition, Vector2 size)
	{
		rectTransform.localPosition = localPosition;
		rectTransform.sizeDelta = new Vector2(size.x, size.y);
	}

	public void SetIndicatorCenter(Vector2 anchor01)
	{
		RectTransform rectTransform = indicatorCenter;
		Vector2 anchorMax = (indicatorCenter.anchorMin = anchor01);
		rectTransform.anchorMax = anchorMax;
	}

	public void AfterReturnedToPool()
	{
		tmp.text = "";
	}

	public void BeforeGetFromPool()
	{
	}
}
