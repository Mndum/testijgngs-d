using System;
using Fungus;
using IC.AutomaticReference;
using UnityEngine;

public class CuddleScene : ViewScene
{
	[SerializeField]
	private Live2DHolder _holder;

	private Live2DPrefabHolder.Live2DControllerUsage<Live2DInteractionController> _currentUsage;

	[NonSerialized]
	private EquipmentSet _equipmentSetBefore;

	[field: SerializeField]
	public View MainView { get; private set; }

	[field: SerializeField]
	public View ZoomView { get; private set; }

	protected override void PrepareNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		gameObject.SetActive(value: true);
	}

	protected override void SwitchNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameSceneGameObject)
		{
			Live2DPrefabHolder interactCharacterHolder = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.InteractCharacterHolder;
			if (_equipmentSetBefore == null)
			{
				_equipmentSetBefore = EquipmentSet.GenerateFromCurrent("Before cuddle");
				Item.ItemManager itemManager = GameVariables.Current.itemManager;
				itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.UpperBody.Id);
				itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.UpperBodyOuter.Id);
			}
			_currentUsage = _holder.StartUsing(interactCharacterHolder, (Live2DInteractionController controller) =>
			{
				controller.ResetEverything(Live2DInteractionController.InteractionSceneTypeEnum.Cuddle);
			});
			if (!info.SameScene)
			{
				SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(GetType().Name, R.Music.Cuddle_All.LoadAll(), SoundSingleton.MusicOption.MusicOptionLayer.Base);
				SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
			}
		}
	}

	protected override void StartNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameSceneGameObject && !SingletonLoadableMonoBehaviour<GameScript>.Instance.TryGetStateAs<CuddleState>(out var _))
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new CuddleState(_currentUsage.Controller));
		}
	}

	protected override void SwitchOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameSceneGameObject)
		{
			_equipmentSetBefore.Equip(resetFirst: true);
			_equipmentSetBefore = null;
		}
	}

	protected override void EndOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameSceneGameObject)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(GetType().Name);
			_currentUsage?.Dispose();
			_currentUsage = null;
			gameObject.SetActive(value: false);
		}
	}
}
