using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScrollingTexture : MonoBehaviour
{
	public Vector2 ScrollSpeed;

	public void UpdateBlock()
	{
		SpriteRenderer component = GetComponent<SpriteRenderer>();
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		component.GetPropertyBlock(materialPropertyBlock);
		materialPropertyBlock.SetVector("_ScrollSpeed", ScrollSpeed);
		component.SetPropertyBlock(materialPropertyBlock);
	}

	private void OnValidate()
	{
		UpdateBlock();
	}

	private void Start()
	{
		UpdateBlock();
	}

	private void OnEnable()
	{
		UpdateBlock();
	}
}
