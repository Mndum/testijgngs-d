using UnityEngine;

public class DeveloperViewScene : ViewScene
{
	[SerializeField]
	private SpriteRenderer sheep;

	public SpriteRenderer Sheep => sheep;

	public void Update()
	{
		sheep.transform.localScale = Vector3.one * (Mathf.Sin(Time.time * 5f) * 0.05f + 0.95f);
		sheep.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Cos(Time.time * 3.4f) * 5f);
	}

	protected override void StartNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new DeveloperRoomState());
	}
}
