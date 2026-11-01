using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorableUIImage : MaskableGraphic
{
	[SerializeField]
	private Texture m_Texture;

	[SerializeField]
	private float offset = 10f;

	[SerializeField]
	private List<Color> colors;

	public List<Color> Colors
	{
		get
		{
			return colors ?? (colors = new List<Color>());
		}
		set
		{
			colors = value;
			SetVerticesDirty();
			SetMaterialDirty();
		}
	}

	public Texture texture
	{
		get
		{
			return m_Texture;
		}
		set
		{
			if (!(m_Texture == value))
			{
				m_Texture = value;
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}
	}

	public override Texture mainTexture
	{
		get
		{
			if (!(m_Texture == null))
			{
				return m_Texture;
			}
			return Graphic.s_WhiteTexture;
		}
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		if (colors == null)
		{
			colors = new List<Color>();
		}
		if (colors.Count <= 0)
		{
			colors.Add(Color.white);
		}
		vh.Clear();
		Rect rect = rectTransform.rect;
		Vector2 pivot = rectTransform.pivot;
		float x = (0f - pivot.x) * rect.width;
		float y = (0f - pivot.y) * rect.height;
		float x2 = (1f - pivot.x) * rect.width;
		float y2 = (1f - pivot.y) * rect.height;
		Vector2 vector = new Vector2(x, y);
		Vector2 vector2 = new Vector2(x2, y2);
		float num = offset / rect.width;
		Vector2 vector3 = new Vector2(offset, 0f);
		Vector2 vector4 = new Vector2(num, 0f);
		float num2 = (1f - num) / (float)colors.Count;
		float num3 = (rect.width - offset) / (float)colors.Count;
		for (int i = 0; i < colors.Count; i++)
		{
			Vector2 vector5 = new Vector2(vector.x + (float)i * num3, vector.y);
			Vector2 vector6 = new Vector2(vector5.x + num3, vector2.y);
			Vector2 vector7 = new Vector2((float)i * num2, 0f);
			Vector2 vector8 = new Vector2(vector7.x + num2, 1f);
			int currentVertCount = vh.currentVertCount;
			UIVertex v = new UIVertex
			{
				color = colors[i] * color,
				position = vector5,
				uv0 = vector7
			};
			vh.AddVert(v);
			v.position = new Vector2(vector6.x, vector5.y);
			v.uv0 = new Vector2(vector8.x, vector7.y);
			vh.AddVert(v);
			v.position = vector6 + vector3;
			v.uv0 = vector8 + vector4;
			vh.AddVert(v);
			v.position = new Vector2(vector5.x, vector6.y) + vector3;
			v.uv0 = new Vector2(vector7.x, vector8.y) + vector4;
			vh.AddVert(v);
			vh.AddTriangle(currentVertCount, currentVertCount + 2, currentVertCount + 1);
			vh.AddTriangle(currentVertCount + 3, currentVertCount + 2, currentVertCount);
		}
	}
}
