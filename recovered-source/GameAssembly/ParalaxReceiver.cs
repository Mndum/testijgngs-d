using UnityEngine;

public class ParalaxReceiver : MonoBehaviour
{
	public float distance = 20f;

	private Vector3 startingLocalPosition;

	public void Awake()
	{
		startingLocalPosition = transform.localPosition;
	}

	public void ApplyParalax(Vector2 paralax)
	{
		transform.localPosition = new Vector3(startingLocalPosition.x + paralax.x / distance, startingLocalPosition.y + paralax.y / distance, startingLocalPosition.z);
	}
}
