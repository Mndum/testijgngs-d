using UnityEngine;
using UnityEngine.UI;

public class FlashImage : MonoBehaviour
{
	private Image image;

	private float timer;

	public float speed = 0.1f;

	private void Start()
	{
		image = GetComponent<Image>();
	}

	private void FixedUpdate()
	{
		timer += Time.fixedDeltaTime;
		if (timer > speed)
		{
			image.enabled = !image.enabled;
			timer = 0f;
		}
	}
}
