using Fungus;
using UnityEngine;

[CommandInfo("My", "Display Live2D Character", "Displays Live2D character", 0)]
internal class DisplayLive2DCharacter : Command
{
	[SerializeField]
	public Live2DPrefabHolder character;

	[SerializeField]
	public Vector3Data size = new Vector3Data(new Vector3(1f, 1f, 1f));

	[SerializeField]
	public Vector3Data offset;

	[SerializeField]
	public View targetView;

	public override void OnEnter()
	{
		if (character != null)
		{
			View view = (targetView ? targetView : SingletonMonoBehaviour<ViewSingleton>.Instance.ViewForCharacters);
			if (view.Scene is ViewScene viewScene)
			{
				Live2DController controller = viewScene.MovePrefabInto(character, size, offset).Controller;
				controller.UpdateTexturesFromCurrentlyEquipped(Live2DController.UpdateTexturesFromCurrentlyEquippedTypeEnum.Reset);
				controller.CurrentBrain.ChangeState(new StoryBrainState());
				controller.ResetBasic();
			}
			else
			{
				Debug.LogError("Cannot move a live2d character into a " + view.name + ". It's scene is not a ViewScene!");
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (character != null)
		{
			return character.name + " appears " + ((targetView != null) ? ("and moved to " + targetView.name) : "and moved to the current view");
		}
		return "No character selected!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(255, 150, 255, 255);
	}
}
