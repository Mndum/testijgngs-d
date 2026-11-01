using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class EyeClosingShader : MonoBehaviour
{
	public Shader shader;

	private Material material;

	[Range(0f, 1f)]
	public float closed;

	private static readonly int Closed = Shader.PropertyToID("_closed");

	private void Awake()
	{
		material = new Material(shader);
	}

	public void CloseInstantly()
	{
		enabled = true;
		closed = 1f;
	}

	public void OpenInstantly()
	{
		enabled = false;
		closed = 0f;
	}

	public IEnumerator QuickCloseEyes()
	{
		enabled = true;
		return EyeMoveTo(1f, 0.2f);
	}

	public IEnumerator QuickOpenEyes()
	{
		yield return EyeMoveTo(0f, 0.2f);
		enabled = false;
	}

	public IEnumerator QuickFallAsleep()
	{
		enabled = true;
		closed = 0f;
		return EyeMoveTo(1f, 0.2f);
	}

	public IEnumerator FallAsleep()
	{
		enabled = true;
		closed = 0f;
		int precloseCount = Random.Range(2, 5);
		for (int i = 0; i < precloseCount; i++)
		{
			float timePerUnit = Random.Range(0.4f, 0.6f);
			yield return EyeMoveTo(Random.Range(0.5f, 0.8f), timePerUnit);
			yield return EyeMoveTo(0f, 0.5f);
			yield return WaitForSecondCache.Seconds0_2;
		}
		yield return EyeMoveTo(1f, 1f);
	}

	public IEnumerator GoToSleep()
	{
		enabled = true;
		closed = 0f;
		int precloseCount = Random.Range(1, 2);
		for (int i = 0; i < precloseCount; i++)
		{
			float timePerUnit = Random.Range(0.4f, 0.6f);
			yield return EyeMoveTo(Random.Range(0.5f, 0.8f), timePerUnit);
			yield return EyeMoveTo(0f, 0.5f);
			yield return WaitForSecondCache.Seconds0_2;
		}
		yield return EyeMoveTo(1f, 0.5f);
	}

	public IEnumerator WakeUp()
	{
		yield return EyeMoveTo(0.9f, 4f);
		yield return EyeMoveTo(0f, 2f);
		enabled = false;
	}

	private IEnumerator EyeMoveTo(float target, float timePerUnit)
	{
		float start = closed;
		float num = Mathf.Abs(target - start);
		yield return CoroutineUtilities.GenericTween((float t) =>
		{
			closed = Mathf.Lerp(start, target, t);
		}, timePerUnit * num);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetPass(0);
		material.SetFloat(Closed, closed);
		Graphics.Blit(source, destination, material);
	}
}
