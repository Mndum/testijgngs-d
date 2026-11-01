using UnityEngine;

public class PrefabPopup : PrefabPopup<PrefabPopup>
{
}
public class PrefabPopup<T> : UIPopup where T : PrefabPopup<T>
{
	public T InstantiatePrefab()
	{
		return (T)Object.Instantiate(this, UiOverlay.Instance.OverlayTransform);
	}

	public override bool CloseInner()
	{
		Object.Destroy(gameObject);
		return true;
	}

	public override bool CanClose()
	{
		return false;
	}
}
