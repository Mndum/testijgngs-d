using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup targetCanvasGroup;

	public float fadeDuration = 0.1f;

	private float _lastDisable = 0.25f;

	private TweenerCore<float, float, FloatOptions> tween;

	private void OnValidate()
	{
		targetCanvasGroup = GetComponent<CanvasGroup>();
	}

	private void OnEnable()
	{
		if (!Mathf.Approximately(_lastDisable, Time.time))
		{
			targetCanvasGroup.alpha = 0f;
			tween = DOTween.To(() => targetCanvasGroup.alpha, (float x) =>
			{
				targetCanvasGroup.alpha = x;
			}, 1f, fadeDuration).SetEase(Ease.OutQuart);
		}
		else
		{
			targetCanvasGroup.alpha = 1f;
		}
	}

	private void OnDisable()
	{
		_lastDisable = Time.time;
		if (tween != null)
		{
			tween.Kill();
			tween = null;
		}
	}
}
