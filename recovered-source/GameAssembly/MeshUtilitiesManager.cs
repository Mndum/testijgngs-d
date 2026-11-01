using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshUtilitiesManager : SingletonMonoBehaviour<MeshUtilitiesManager>
{
	[SerializeField]
	private Shader L2DMeshUvShader;

	[SerializeField]
	private Shader L2DMeshUvWireframeShader;

	[SerializeField]
	private Shader DrawTextureShader;

	[SerializeField]
	private Shader DrawTextureShaderNoBlend;

	[SerializeField]
	private Shader DrawScratchShader;

	[SerializeField]
	private Shader CompareTextureShader;

	[SerializeField]
	private Shader ClearShader;

	[NonSerialized]
	private Material L2DMeshUvMaterial;

	[NonSerialized]
	private Material L2DMeshUvWireframeMaterial;

	[NonSerialized]
	private Material DrawTextureMaterial;

	[NonSerialized]
	private Material DrawTextureMaterialNoBlend;

	[NonSerialized]
	private Material DrawScratchMaterial;

	[NonSerialized]
	private Material CompareTextureMaterial;

	[NonSerialized]
	private Material ClearMaterial;

	[NonSerialized]
	private bool _isInitialized;

	[NonSerialized]
	private Texture2D _nullTexture;

	private Mesh _quadMesh;

	private static readonly int SourceRect = Shader.PropertyToID("_SourceRect");

	private static readonly int TargetRect = Shader.PropertyToID("_TargetRect");

	private static readonly int Rotation = Shader.PropertyToID("_Rotation");

	private static readonly int Color1 = Shader.PropertyToID("_Color");

	private static readonly int MainTexture = Shader.PropertyToID("_MainTex");

	private static readonly int Tex1 = Shader.PropertyToID("_Tex1");

	private static readonly int Tex2 = Shader.PropertyToID("_Tex2");

	private static readonly int SourceRect1 = Shader.PropertyToID("_SourceRect1");

	private static readonly int SourceRect2 = Shader.PropertyToID("_SourceRect2");

	private void InitMaterials()
	{
		L2DMeshUvMaterial = new Material(L2DMeshUvShader);
		L2DMeshUvWireframeMaterial = new Material(L2DMeshUvWireframeShader);
		DrawTextureMaterial = new Material(DrawTextureShader);
		DrawTextureMaterialNoBlend = new Material(DrawTextureShaderNoBlend);
		DrawScratchMaterial = new Material(DrawScratchShader);
		CompareTextureMaterial = new Material(CompareTextureShader);
		ClearMaterial = new Material(ClearShader);
	}

	private void EnsureInitialized()
	{
		if (!_isInitialized)
		{
			_quadMesh = new Mesh
			{
				name = "Quad mesh",
				vertices = new Vector3[4]
				{
					new Vector3(0f, 0f),
					new Vector3(1f, 0f),
					new Vector3(0f, 1f),
					new Vector3(1f, 1f)
				},
				uv = new Vector2[4]
				{
					new Vector2(0f, 0f),
					new Vector2(1f, 0f),
					new Vector2(0f, 1f),
					new Vector2(1f, 1f)
				},
				triangles = new int[6] { 0, 1, 2, 1, 3, 2 }
			};
			_nullTexture = Texture2D.redTexture;
			InitMaterials();
			_isInitialized = true;
		}
	}

	public override void Awake()
	{
		base.Awake();
		EnsureInitialized();
	}

	private Texture2D GetNullTexture()
	{
		return _nullTexture;
	}

	private Mesh GetQuadMesh()
	{
		return _quadMesh;
	}

	public static Rect GetUVRect(Mesh mesh)
	{
		Vector2 vector = mesh.uv[0];
		Vector2 vector2 = mesh.uv[0];
		for (int i = 1; i < mesh.uv.Length; i++)
		{
			Vector2 vector3 = mesh.uv[i];
			if (vector3.x < vector.x)
			{
				vector.x = vector3.x;
			}
			else if (vector3.x > vector2.x)
			{
				vector2.x = vector3.x;
			}
			if (vector3.y < vector.y)
			{
				vector.y = vector3.y;
			}
			else if (vector3.y > vector2.y)
			{
				vector2.y = vector3.y;
			}
		}
		return Rect.MinMaxRect(vector.x, vector.y, vector2.x, vector2.y);
	}

	public static RectInt GetRectInt(Rect rect, Texture texture)
	{
		int num = Mathf.FloorToInt(rect.xMin * (float)texture.width);
		int num2 = Mathf.FloorToInt(rect.yMin * (float)texture.height);
		int num3 = Mathf.CeilToInt(rect.xMax * (float)texture.width);
		int num4 = Mathf.CeilToInt(rect.yMax * (float)texture.height);
		return new RectInt(num, num2, num3 - num, num4 - num2);
	}

	public static Rect GetRect(RectInt intRect, Texture texture)
	{
		return Rect.MinMaxRect((float)intRect.xMin / (float)texture.width, (float)intRect.yMin / (float)texture.height, (float)intRect.xMax / (float)texture.width, (float)intRect.yMax / (float)texture.height);
	}

	private static Vector4 GetDimensions(Rect rect)
	{
		return new Vector4(rect.xMin, rect.yMin, rect.width, rect.height);
	}

	public void DrawLive2DMeshToTexture(Live2DController.Part part, Rect targetRect, Texture texture)
	{
		DrawLive2DMeshToTexture(part, targetRect, texture, Color.white);
	}

	public void DrawLive2DMeshBoundsToTexture(Live2DController.Part part, Rect targetRect)
	{
		try
		{
			GL.wireframe = true;
			Vector4 dimensions = GetDimensions(part.ExpandedNormalizedSize);
			Vector4 dimensions2 = GetDimensions(targetRect);
			L2DMeshUvWireframeMaterial.SetVector(SourceRect, dimensions);
			L2DMeshUvWireframeMaterial.SetVector(TargetRect, dimensions2);
			L2DMeshUvWireframeMaterial.SetColor(Color1, Color.magenta);
			L2DMeshUvWireframeMaterial.SetPass(0);
			Graphics.DrawMeshNow(part.Mesh, Matrix4x4.identity);
		}
		finally
		{
			GL.wireframe = false;
		}
	}

	public void DrawLive2DMeshToTexture(Live2DController.Part part, Rect targetRect, Texture texture, Color color)
	{
		Vector4 dimensions = GetDimensions(part.ExpandedNormalizedSize);
		Vector4 dimensions2 = GetDimensions(targetRect);
		L2DMeshUvMaterial.SetVector(SourceRect, dimensions);
		L2DMeshUvMaterial.SetVector(TargetRect, dimensions2);
		L2DMeshUvMaterial.SetTexture(MainTexture, texture);
		L2DMeshUvMaterial.SetColor(Color1, color);
		L2DMeshUvMaterial.SetPass(0);
		Graphics.DrawMeshNow(part.Mesh, Matrix4x4.identity);
		L2DMeshUvMaterial.SetTexture(MainTexture, _nullTexture);
	}

	public void ClearLive2DMesh(Live2DController.Part part, Rect targetRect, Color color)
	{
		Vector4 dimensions = GetDimensions(part.ExpandedNormalizedSize);
		Vector4 dimensions2 = GetDimensions(targetRect);
		ClearMaterial.SetVector(SourceRect, dimensions);
		ClearMaterial.SetVector(TargetRect, dimensions2);
		ClearMaterial.SetColor(Color1, color);
		ClearMaterial.SetPass(0);
		Graphics.DrawMeshNow(part.Mesh, Matrix4x4.identity);
	}

	public PackedTexturesContainer PackRenderers(List<Live2DController.Part> parts, bool separateTextures = false, int layer = 1)
	{
		if (parts.Count == 0)
		{
			return null;
		}
		PackedTexturesContainer packedTexturesContainer = PackedTexturesContainer.CreateEmpty();
		if (separateTextures)
		{
			foreach (IGrouping<Texture, Live2DController.Part> item in from t in parts.ToLookup((Live2DController.Part t) => t.MainTexture, (Live2DController.Part t) => t)
				orderby t.Key.name
				select t)
			{
				List<Live2DController.Part> parts2 = item.ToList();
				packedTexturesContainer.CreatePackedTexturesFromRenderers(parts2, new string[1] { parts[0].Owner.ControllerEnum.GetControllerId() }, layer);
			}
		}
		else
		{
			packedTexturesContainer.CreatePackedTexturesFromRenderers(parts, new string[1] { parts[0].Owner.ControllerEnum.GetControllerId() }, layer);
		}
		return packedTexturesContainer;
	}

	public void DrawTexture2DNoBlend(Rect sourceRect, Rect targetRect, Texture2D texture, Color color)
	{
		DrawTextureMaterialNoBlend.SetVector(SourceRect, GetDimensions(sourceRect));
		DrawTextureMaterialNoBlend.SetVector(TargetRect, GetDimensions(targetRect));
		DrawTextureMaterialNoBlend.SetColor(Color1, color);
		DrawTextureMaterialNoBlend.SetTexture(MainTexture, texture);
		DrawTextureMaterialNoBlend.SetPass(0);
		Graphics.DrawMeshNow(GetQuadMesh(), Matrix4x4.identity);
		DrawTextureMaterialNoBlend.SetTexture(MainTexture, GetNullTexture());
	}

	public void DrawTexture2D(Rect sourceRect, Rect targetRect, Texture2D texture, Color color)
	{
		DrawTextureMaterial.SetVector(SourceRect, GetDimensions(sourceRect));
		DrawTextureMaterial.SetVector(TargetRect, GetDimensions(targetRect));
		DrawTextureMaterial.SetColor(Color1, color);
		DrawTextureMaterial.SetTexture(MainTexture, texture);
		DrawTextureMaterial.SetPass(0);
		Graphics.DrawMeshNow(GetQuadMesh(), Matrix4x4.identity);
		DrawTextureMaterial.SetTexture(MainTexture, GetNullTexture());
	}

	public void DrawPacked(PackedTexturesContainer.PackedTexture packedTexture, PackedTexturesContainer.PackedDrawable packedDrawable, Rect targetRect, int targetFullWidth, int targetFullHeight, Color color)
	{
		Texture2D texture = packedTexture.GetTexture();
		Rect rect = GetRect(packedDrawable.RectInt, texture);
		DrawableTypeEnum drawableType = packedDrawable.DrawableType;
		if ((uint)(drawableType - 1) <= 3u)
		{
			float num = rect.width * (float)texture.width;
			float num2 = rect.height * (float)texture.height / num;
			float num3 = targetRect.width * (float)targetFullWidth;
			float num4 = targetRect.height * (float)targetFullHeight;
			float num5 = num4 / num3;
			Vector2 center = targetRect.center;
			float num6 = 0.95f;
			if (num2 > num5)
			{
				targetRect.width = num4 * num6 / (num2 * (float)targetFullHeight);
				targetRect.height = num4 * num6 / (float)targetFullHeight;
			}
			else
			{
				targetRect.height = num3 * num2 * num6 / (float)targetFullWidth;
				targetRect.width = num3 * num6 / (float)targetFullWidth;
			}
			if (packedDrawable.DrawableType == DrawableTypeEnum.LogoReflectHorizontally || packedDrawable.DrawableType == DrawableTypeEnum.LogoReflectBoth)
			{
				targetRect.width *= -1f;
			}
			if (packedDrawable.DrawableType == DrawableTypeEnum.LogoReflectBoth || packedDrawable.DrawableType == DrawableTypeEnum.LogoReflectBoth)
			{
				targetRect.height *= -1f;
			}
			targetRect.center = center;
		}
		SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.DrawTexture2D(rect, targetRect, texture, color);
	}

	public void DrawPackedIgnorePreserveAspectRatio(PackedTexturesContainer.PackedTexture packedTexture, PackedTexturesContainer.PackedDrawable packedDrawable, Rect targetRect, Color color, bool blend)
	{
		Texture2D texture = packedTexture.GetTexture();
		Rect rect = GetRect(packedDrawable.RectInt, texture);
		if (blend)
		{
			SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.DrawTexture2D(rect, targetRect, texture, color);
		}
		else
		{
			SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.DrawTexture2DNoBlend(rect, targetRect, texture, color);
		}
	}

	public void DrawScratch(Texture2D texture, float rotation, Rect targetRect, Color color)
	{
		DrawScratchMaterial.SetVector(SourceRect, GetDimensions(new Rect(0f, 0f, 1f, 1f)));
		DrawScratchMaterial.SetVector(TargetRect, GetDimensions(targetRect));
		DrawScratchMaterial.SetFloat(Rotation, rotation);
		DrawScratchMaterial.SetColor(Color1, color);
		DrawScratchMaterial.SetTexture(MainTexture, texture);
		DrawScratchMaterial.SetPass(0);
		Graphics.DrawMeshNow(GetQuadMesh(), Matrix4x4.identity);
		DrawScratchMaterial.SetTexture(MainTexture, GetNullTexture());
	}

	public float Compare(Texture2D t1, RectInt r1, Texture2D t2, RectInt r2)
	{
		Vector2Int vector2Int = Vector2Int.Max(r1.size, r2.size);
		RenderTexture temporary = RenderTexture.GetTemporary(vector2Int.x, vector2Int.y);
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = temporary;
		GL.Clear(clearDepth: true, clearColor: true, Color.clear);
		CompareTextureMaterial.SetVector(SourceRect1, GetDimensions(GetRect(r1, t1)));
		CompareTextureMaterial.SetVector(SourceRect2, GetDimensions(GetRect(r2, t2)));
		CompareTextureMaterial.SetTexture(Tex1, t1);
		CompareTextureMaterial.SetTexture(Tex2, t2);
		CompareTextureMaterial.SetVector(TargetRect, GetDimensions(new Rect(0f, 0f, 1f, 1f)));
		CompareTextureMaterial.SetPass(0);
		Graphics.DrawMeshNow(GetQuadMesh(), Matrix4x4.identity);
		Texture2D texture2D = new Texture2D(vector2Int.x, vector2Int.y, TextureFormat.RGBA32, mipChain: false);
		ImageUtility.ActiveRenderTextureToTextureCpuSide(temporary, texture2D);
		Color32[] pixels = texture2D.GetPixels32();
		int num = 0;
		Color32[] array = pixels;
		for (int i = 0; i < array.Length; i++)
		{
			Color32 color = array[i];
			num += color.r;
		}
		UnityEngine.Object.Destroy(texture2D);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.active = active;
		Texture2D nullTexture = GetNullTexture();
		CompareTextureMaterial.SetTexture(Tex1, nullTexture);
		CompareTextureMaterial.SetTexture(Tex2, nullTexture);
		return (float)num / (255f * (float)pixels.Length);
	}
}
