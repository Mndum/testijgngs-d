using UnityEngine;

public class StaticPopup : UIPopup
{
	public override bool IsNotStatic => false;

	public override bool CanClose()
	{
		return true;
	}

	public void OnEnable()
	{
		gameObject.GetOrAddComponent<CanvasGroup>().alpha = 0f;
		SetAlpha(1f);
	}

	public override void Open()
	{
	}

	public override bool CloseInner()
	{
		return false;
	}
}
