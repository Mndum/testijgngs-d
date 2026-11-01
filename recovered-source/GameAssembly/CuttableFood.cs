using UnityEngine;
using UnityEngine.UI;

public class CuttableFood : MaskableGraphic, IPooledPrefab
{
	[SerializeField]
	private Sprite _sprite;

	[Range(0f, 1f)]
	public float cutX1;

	[Range(0f, 1f)]
	public float cutX2 = 1f;

	public float flyingOffProgress;

	private Vector2 flyingOffStartPosition;

	private float flyingOffRotation;

	private const float UpFlyOff = 150f;

	private const float SideFlyOff = -25f;

	public Sprite Sprite => _sprite;

	public override Texture mainTexture => _sprite.texture;

	public GameObject SourcePrefab { get; set; }

	private void SetSprite(Sprite sprite)
	{
		_sprite = sprite;
		Rect textureRect = sprite.textureRect;
		rectTransform.sizeDelta = new Vector2(textureRect.width, textureRect.height);
	}

	private void SetCuts(float _cutX1, float _cutX2)
	{
		cutX1 = _cutX1;
		cutX2 = _cutX2;
		SetVerticesDirty();
	}

	public void InitCutting(Sprite sprite)
	{
		SetSprite(sprite);
		SetCuts(0f, 1f);
		rectTransform.localScale = Vector3.one;
		rectTransform.localRotation = Quaternion.identity;
	}

	public void CutAt(float x, out float sliceStart)
	{
		sliceStart = cutX1;
		SetCuts(x, cutX2);
	}

	public void InitFlyingOff(Sprite sprite, float _cutX1, float _cutX2)
	{
		flyingOffStartPosition = rectTransform.anchoredPosition;
		SetSprite(sprite);
		SetCuts(_cutX1, _cutX2);
		flyingOffProgress = 0.05f;
		flyingOffRotation = Random.Range(-5, 5);
	}

	public bool UpdateFlyingOff()
	{
		flyingOffProgress += Time.deltaTime * 1.7f;
		float num = 1f - (1f - flyingOffProgress) * (1f - flyingOffProgress);
		RectTransform rectTransform = base.rectTransform;
		rectTransform.anchoredPosition = flyingOffStartPosition + new Vector2(num * -25f, (num - num * num) * 150f);
		rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, num);
		rectTransform.Rotate(Vector3.forward, Time.deltaTime * flyingOffRotation, Space.Self);
		return flyingOffProgress > 1f;
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		if (!(_sprite == null))
		{
			Texture2D texture = _sprite.texture;
			Rect r = _sprite.textureRect.NormalizeTo(texture);
			Rect item = RectExtensions.SplitVerticallyByLeftWidth(leftWidth: cutX2 - cutX1, r: r.SplitVerticallyByLeftWidth(cutX1).right).left;
			Rect pixelAdjustedRect = GetPixelAdjustedRect();
			float width = pixelAdjustedRect.width;
			float num = width * cutX1;
			float num2 = width * (1f - cutX2);
			float leftWidth = width - num - num2;
			Rect item2 = pixelAdjustedRect.SplitVerticallyByLeftWidth(num).right.SplitVerticallyByLeftWidth(leftWidth).left;
			_ = 0f;
			Rect rect = item;
			Rect rect2 = item2;
			vh.AddVert(new Vector3(rect2.x, rect2.y), Color.white, new Vector2(rect.x, rect.y));
			vh.AddVert(new Vector3(rect2.x, rect2.y + rect2.height), Color.white, new Vector2(rect.x, rect.y + rect.height));
			vh.AddVert(new Vector3(rect2.x + rect2.width, rect2.y + rect2.height), Color.white, new Vector2(rect.x + rect.width, rect.y + rect.height));
			vh.AddVert(new Vector3(rect2.x + rect2.width, rect2.y), Color.white, new Vector2(rect.x + rect.width, rect.y));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
			_ = 0f;
		}
	}

	public void AfterReturnedToPool()
	{
	}

	public void BeforeGetFromPool()
	{
	}
}
