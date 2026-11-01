using UnityEngine;

[ExecuteInEditMode]
public class HellShaderScript : MonoBehaviour
{
	public Shader shader;

	private Material material;

	public float depth;

	private static readonly int Depth = Shader.PropertyToID("_depth");

	private void Awake()
	{
		material = new Material(shader);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetPass(0);
		material.SetFloat(Depth, depth);
		Graphics.Blit(source, destination, material);
	}
}
