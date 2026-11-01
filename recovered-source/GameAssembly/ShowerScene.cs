using System;
using GameItems;
using IC.AutomaticReference;
using UnityEngine;

public class ShowerScene : ViewScene, IMusicSource
{
	[SerializeField]
	private Live2DHolder _holder;

	[NonSerialized]
	private EquipmentSet _equipmentSetBefore;

	[NonSerialized]
	private Item _fakeHairItem;

	private Live2DPrefabHolder.Live2DControllerUsage<Live2DShowerController> _currentUsage;

	public string MusicSourceId => "Shower Scene";

	protected override void PrepareNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		gameObject.SetActive(value: true);
	}

	protected override void SwitchNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameSceneGameObject)
		{
			Live2DPrefabHolder showerCharacterHolder = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.ShowerCharacterHolder;
			_equipmentSetBefore = EquipmentSet.GenerateFromCurrent("Before shower");
			Item.ItemManager itemManager = GameVariables.Current.itemManager;
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.UpperBody.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.UpperBodyOuter.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.LowerBody.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.Glasses.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.Bra.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.Panties.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.Tail.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.NeckAccessory.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.Septum.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.ShoeL.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.ShoeR.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.StockingL.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.StockingR.Id);
			Item firstItemAtSlot = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.HairMain.Id);
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.HairMain.Id);
			if (firstItemAtSlot != null)
			{
				_fakeHairItem = Item.CreateRandomItem(ItemEnum.DefaultHair.ToGameId());
				_fakeHairItem.SetColors(firstItemAtSlot.GetColors());
				itemManager.AddItem(_fakeHairItem);
				itemManager.Equip(_fakeHairItem, forceRemoveForbidden: true);
			}
			itemManager.UnequipItemWithDependencies(ItemSlot.SlotManager.HairAdditional.Id);
			_currentUsage = _holder.StartUsing<Live2DShowerController>(showerCharacterHolder, null);
			if (!info.SameScene)
			{
				SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(MusicSourceId, R.Music.Cuddle_All.LoadAll(), SoundSingleton.MusicOption.MusicOptionLayer.Base);
				SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
			}
		}
	}

	protected override void StartNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameSceneGameObject && !SingletonLoadableMonoBehaviour<GameScript>.Instance.TryGetStateAs<ShowerState>(out var _))
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new ShowerState(_currentUsage.Controller));
		}
	}

	protected override void SwitchOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameSceneGameObject)
		{
			if (_fakeHairItem != null)
			{
				GameVariables.Current.itemManager.RemoveItem(_fakeHairItem);
				_fakeHairItem = null;
			}
			if (_equipmentSetBefore != null)
			{
				_equipmentSetBefore.Equip(resetFirst: true);
				_equipmentSetBefore = null;
			}
			_currentUsage?.Dispose();
			_currentUsage = null;
		}
	}

	protected override void EndOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameSceneGameObject)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(this);
			_equipmentSetBefore = null;
			_currentUsage?.Dispose();
			_currentUsage = null;
			gameObject.SetActive(value: false);
		}
	}
}
