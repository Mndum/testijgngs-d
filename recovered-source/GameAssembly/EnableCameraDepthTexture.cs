using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class EnableCameraDepthTexture : MonoBehaviour
{
	private void OnEnable()
	{
		GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
	}
}
