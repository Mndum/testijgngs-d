using UnityEngine;

[ExecuteInEditMode]
public class DepthVisualizationShaderScript : MonoBehaviour
{
	public Shader shader;

	private Material material;

	private void Awake()
	{
		material = new Material(shader);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
		Graphics.Blit(source, destination, material);
	}
}
