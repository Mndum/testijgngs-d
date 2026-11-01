using UnityEngine;

[ExecuteInEditMode]
public class PixelShaderScript : MonoBehaviour
{
	[SerializeField]
	[Range(1f, 50f)]
	public int pixelsInWidth = 1;

	[SerializeField]
	[Range(1f, 50f)]
	public int pixelsInHeight = 1;

	public Shader shader;

	private Material material;

	private static readonly int PixelHeight = Shader.PropertyToID("_pixelHeight");

	private static readonly int PixelWidth = Shader.PropertyToID("_pixelWidth");

	private void Awake()
	{
		material = new Material(shader);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		float value = (float)pixelsInWidth / 1920f;
		float value2 = (float)pixelsInHeight / 1080f;
		material.SetFloat(PixelWidth, value);
		material.SetFloat(PixelHeight, value2);
		Graphics.Blit(source, destination, material);
	}
}
