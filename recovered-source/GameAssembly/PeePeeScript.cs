using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeePeeScript : MonoBehaviour
{
	public class PeeParticle
	{
		private Vector3 _speed;

		public RectTransform RT { get; }

		public PeeParticle(RectTransform rt)
		{
			RT = rt;
			rt.pivot = (rt.anchorMax = (rt.anchorMin = new Vector2(0.5f, 0f)));
		}

		public void SetPosition(Vector2 position)
		{
			RT.localPosition = position;
		}

		public void SetSpeed(Vector3 speed)
		{
			_speed = speed;
		}

		public bool Sync()
		{
			_speed.y -= 0.9f;
			Vector3 anchoredPosition3D = RT.anchoredPosition3D + _speed;
			RT.anchoredPosition3D = anchoredPosition3D;
			return anchoredPosition3D.y <= 0f;
		}

		public void Remove()
		{
			Object.Destroy(RT.gameObject);
		}
	}

	private List<PeeParticle> _peeParticles = new List<PeeParticle>();

	private float timer;

	private const float peeEvery = 0.01f;

	private void CreatePeeParticle()
	{
		GameObject gameObject = new GameObject("pee");
		RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
		rectTransform.SetParent(transform, worldPositionStays: false);
		rectTransform.sizeDelta = new Vector2(3f, 3f);
		gameObject.AddComponent<Image>().color = Color.yellow;
		PeeParticle peeParticle = new PeeParticle(rectTransform);
		Vector3 vector = transform.InverseTransformPoint(UiOverlay.Instance.MouseToWorld()) + new Vector3(Mathf.Sin(Time.fixedUnscaledTime) * 2f, 100f, 0f);
		peeParticle.SetPosition(vector);
		peeParticle.SetSpeed(new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 3f)));
		_peeParticles.Add(peeParticle);
	}

	private void FixedUpdate()
	{
		timer += Time.fixedDeltaTime;
		if (timer > 0.01f)
		{
			if (_peeParticles.Count < 200)
			{
				CreatePeeParticle();
			}
			timer -= 0.01f;
		}
		for (int num = _peeParticles.Count - 1; num >= 0; num--)
		{
			PeeParticle peeParticle = _peeParticles[num];
			if (peeParticle.Sync())
			{
				peeParticle.Remove();
				_peeParticles.RemoveAt(num);
			}
		}
	}
}
