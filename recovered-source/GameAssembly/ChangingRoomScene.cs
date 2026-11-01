using UnityEngine;

public class ChangingRoomScene : ViewScene
{
	[SerializeField]
	private Live2DHolder _interactionHolder;

	private Live2DPrefabHolder.Live2DControllerUsage<Live2DInteractionController> _interactionControllerUsage;

	public Live2DInteractionController GetBotCharacter()
	{
		if (_interactionControllerUsage.TryGetController(out var controller))
		{
			return controller;
		}
		Debug.LogError("Tried to get room scene interaction controller! Room scene doesn't currently have the bot!!!");
		return null;
	}

	protected override void PrepareNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		gameObject.SetActive(value: true);
	}

	protected override void SwitchNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		_interactionControllerUsage = _interactionHolder.StartUsing(SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.InteractCharacterHolder, (Live2DInteractionController c) =>
		{
			c.ResetEverything(Live2DInteractionController.InteractionSceneTypeEnum.Normal);
			c.ChangeLayer(-1);
		});
		_interactionControllerUsage.Controller.CurrentBrain.ChangeState(new StoryBrainState(), force: true);
	}

	protected override void EndOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameSceneGameObject)
		{
			_interactionControllerUsage?.Dispose();
			_interactionControllerUsage = null;
			gameObject.SetActive(value: false);
		}
	}
}
