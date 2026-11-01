using Fungus;
using IC.AutomaticReference;
using UnityEngine;

namespace Assets.Game.Scripts.FungusCommands.View;

[CommandInfo("Camera", "Fade To Image", "Fades the camera out and in again at a sprite.", 0)]
[AddComponentMenu("")]
public class FadeToImage : Command
{
	[Tooltip("Time for fade effect to complete")]
	[SerializeField]
	public float duration = 1f;

	[Tooltip("Fade from fully visible to opaque at start of fade")]
	[SerializeField]
	public bool fadeOut = true;

	[Tooltip("Wait until the fade has finished before executing next command")]
	[SerializeField]
	public bool waitUntilFinished = true;

	[Tooltip("Color to render fullscreen fade texture with when screen is obscured.")]
	[SerializeField]
	public Color fadeColor = Color.black;

	[Tooltip("Sprite to fade to.")]
	[SerializeField]
	public SpriteSerializableAutomaticReference spriteSerializable;

	[SerializeField]
	public Fungus.View.SubViews subView;

	[Tooltip("Color of sprite to fade into.")]
	[SerializeField]
	public Color color = Color.white;

	public Sprite GetSprite()
	{
		return spriteSerializable?.Get();
	}

	public override void OnEnter()
	{
		CameraManager cameraManager = FungusManager.Instance.CameraManager;
		Fungus.View notCurrentViewSetSprite = SingletonMonoBehaviour<ViewSingleton>.Instance.GetNotCurrentViewSetSprite(GetSprite(), color, subView);
		Camera mainCamera = UiOverlay.Instance.MainCamera;
		if (mainCamera == null || notCurrentViewSetSprite == null)
		{
			Continue();
			return;
		}
		cameraManager.FadeToView(mainCamera, notCurrentViewSetSprite, duration, fadeOut, fadeColor, ViewSceneBase.OnFinishedCallback.FromCommandAndWaitForFinished(this, waitUntilFinished));
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override void OnStopExecuting()
	{
		FungusManager.Instance.CameraManager.Stop();
	}

	public override string GetSummary()
	{
		Sprite sprite = GetSprite();
		if ((object)sprite != null)
		{
			return "Transitions to " + sprite.name;
		}
		return "Transitions to a black screen";
	}

	public override Color GetButtonColor()
	{
		return new Color32(216, 228, 170, 255);
	}
}
