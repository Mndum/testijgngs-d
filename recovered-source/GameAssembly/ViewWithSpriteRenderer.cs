using System;
using Fungus;
using UnityEngine;

[Serializable]
public class ViewWithSpriteRenderer : View
{
	public SpriteRenderer spriteRenderer;

	public void SetSprite(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}

	public void SetSpriteColor(Color color)
	{
		spriteRenderer.color = color;
	}
}
