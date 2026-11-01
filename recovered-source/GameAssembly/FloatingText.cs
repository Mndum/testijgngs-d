using System.Text;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

[PooledPrefabConfig(16)]
public class FloatingText : MonoBehaviour, IPooledPrefab
{
	private const float moveYTarget = 100f;

	private const float fadeDuration = 1.5f;

	[SerializeField]
	private TextMeshProUGUI text;

	public TextMeshProUGUI Text => text;

	public GameObject SourcePrefab { get; set; }

	public void StartFloating(string value)
	{
		text.text = value;
		StartAnimation().Forget();
	}

	public void StartFloating(StringBuilder value)
	{
		text.SetText(value);
		StartAnimation().Forget();
	}

	private void ClampToParent()
	{
		text.ForceMeshUpdate();
		RectTransform rectTransform = (RectTransform)transform;
		rectTransform.sizeDelta = text.GetPreferredValues(600f, 1000f);
		Rect rect = ((RectTransform)rectTransform.parent).rect;
		Rect rect2 = rectTransform.rect;
		Vector2 vector = rect.min - rect2.min;
		Vector2 vector2 = rect.max - rect2.max;
		Vector3 localPosition = rectTransform.localPosition;
		rectTransform.localPosition = new Vector3(Mathf.Clamp(localPosition.x, vector.x, vector2.x), Mathf.Clamp(localPosition.y, vector.y, vector2.y), localPosition.z);
	}

	private async UniTaskVoid StartAnimation()
	{
		ClampToParent();
		float elapsedTime = 0f;
		float angle = Random.Range(-0.25f, 0.25f);
		Vector3 startPosition = transform.localPosition;
		text.color = text.color.SetAlpha(1f);
		while (elapsedTime < 1.5f)
		{
			float num = elapsedTime / 1.5f;
			num *= num;
			float x = startPosition.x + Mathf.Sin(angle) * num * num * 100f;
			float y = startPosition.y + num * 100f;
			transform.localPosition = new Vector3(x, y, startPosition.z);
			transform.localRotation = Quaternion.Euler(0f, 0f, num * angle * 57.29578f * -0.25f);
			if (num > 0.5f)
			{
				text.color = text.color.SetAlpha(1f - (num - 0.5f) * 2f);
			}
			elapsedTime += Time.deltaTime;
			await UniTask.Yield();
		}
		this.ReturnToPool();
	}

	public void AfterReturnedToPool()
	{
		text.text = "";
	}

	public void BeforeGetFromPool()
	{
	}
}
