using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BetterButton : Button
{
	private struct PreparedColorFade
	{
		public bool Started;

		public float Timer;

		public float Duration;

		public Color StartColor;

		public Color EndColor;

		public CanvasRenderer TargetCanvasRenderer;

		public PreparedColorFade(float duration, Color _endColor, CanvasRenderer _targetCanvasRenderer)
		{
			this = default;
			Started = true;
			Timer = 0f;
			Duration = duration;
			StartColor = _targetCanvasRenderer.GetColor();
			EndColor = _endColor;
			TargetCanvasRenderer = _targetCanvasRenderer;
		}

		public void SetT(float t)
		{
			TargetCanvasRenderer.SetColor(Color.LerpUnclamped(StartColor, EndColor, t));
		}

		public void Finish()
		{
			TargetCanvasRenderer.SetColor(EndColor);
		}
	}

	[NonSerialized]
	private PreparedColorFade colorFade;

	[NonSerialized]
	public Action PointerDownAction;

	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		if (transition != Transition.ColorTint)
		{
			base.DoStateTransition(state, instant);
		}
		else
		{
			if (!gameObject.activeInHierarchy)
			{
				return;
			}
			ColorBlock colorBlock = colors;
			Color endColor = state switch
			{
				SelectionState.Normal => colorBlock.normalColor, 
				SelectionState.Highlighted => colorBlock.highlightedColor, 
				SelectionState.Pressed => colorBlock.pressedColor, 
				SelectionState.Selected => colorBlock.selectedColor, 
				SelectionState.Disabled => colorBlock.disabledColor, 
				_ => Color.black, 
			};
			if (targetGraphic == null)
			{
				return;
			}
			CanvasRenderer canvasRenderer = targetGraphic.canvasRenderer;
			if (!(canvasRenderer == null))
			{
				colorFade = new PreparedColorFade(colorBlock.fadeDuration, endColor, canvasRenderer);
				if (instant)
				{
					FinishFade();
				}
			}
		}
	}

	public void Update()
	{
		if (colorFade.Started)
		{
			colorFade.Timer += Time.deltaTime;
			if (colorFade.Timer > colorFade.Duration)
			{
				FinishFade();
				return;
			}
			float num = colorFade.Timer / colorFade.Duration;
			num = 1f - num;
			num *= num;
			num = 1f - num;
			colorFade.SetT(num);
		}
	}

	public void InitDefaultColor()
	{
		if (!(targetGraphic == null))
		{
			CanvasRenderer canvasRenderer = targetGraphic.canvasRenderer;
			if (!(canvasRenderer == null))
			{
				FinishFade();
				colorFade = new PreparedColorFade(0f, colors.normalColor, canvasRenderer);
				FinishFade();
			}
		}
	}

	public void FinishFade()
	{
		if (colorFade.Started)
		{
			colorFade.Finish();
			colorFade.Started = false;
		}
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
		PointerDownAction?.Invoke();
	}
}
