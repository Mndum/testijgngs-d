using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fungus;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class NewFrickState : GameStateWithLive2D<Live2DLewdSceneController>, IMusicSource
{
	private class FrickStateButtonSource : ButtonListButtonSource
	{
		private NewFrickState _newFrickState;

		private ButtonListWrapper _noMusicButtonWrapper;

		private ButtonListWrapper _positionButtonWrapper;

		private SoundSingleton.MusicOption _silenceMusicOption;

		private bool MusicOff => _silenceMusicOption != null;

		public FrickStateButtonSource(NewFrickState newFrickState)
		{
			_newFrickState = newFrickState;
		}

		public override void AddButtonsToList(IModificationPeriodButtonList buttonList)
		{
			_noMusicButtonWrapper = buttonList.AddButton(-20);
			_noMusicButtonWrapper.TextFunc = () => (!MusicOff) ? LOC.UI.Frick_MusicOn : LOC.UI.Frick_MusicOff;
			_noMusicButtonWrapper.OnClick = NoMusicButtonClicked;
			_noMusicButtonWrapper.ActiveFunc = () => !SoundSingleton.MusicOff;
			_positionButtonWrapper = buttonList.AddButton();
			_positionButtonWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Frick_Position);
			_positionButtonWrapper.OnClick = _newFrickState.PositionButtonClicked;
			_positionButtonWrapper.ActiveFunc = () => !GameVariables.Current.botLive2DCommonState.FrickData.IsBotCompletelyInTheLead;
			ButtonListWrapper buttonListWrapper = buttonList.AddButton(-70);
			buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Options_ToySettings);
			buttonListWrapper.OnClick = UiOverlay.Instance.OpenToyPopup;
			buttonListWrapper.ActiveFunc = () => SingletonMonoBehaviour<ToySingleton>.Instance.ShowToySettings;
		}

		private void NoMusicButtonClicked()
		{
			if (_silenceMusicOption == null)
			{
				_silenceMusicOption = new SoundSingleton.MusicOption(_newFrickState.MusicSourceId, SoundSingleton.MusicOption.MusicOptionLayer.Forced, null);
				SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(_silenceMusicOption);
				SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
			}
			else
			{
				SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOption(_silenceMusicOption);
				_silenceMusicOption = null;
			}
			_noMusicButtonWrapper.UpdateButtonText();
		}
	}

	private class PositionChangeData
	{
		public ControllerEnum Enum;

		public Func<bool> IsAllowed;

		public View TargetScene;

		public string RequirementText;

		public bool HideIfNotAllowed;

		public bool HasRequirementText => !string.IsNullOrEmpty(RequirementText);

		public PositionChangeData(ControllerEnum @enum, Func<bool> isAllowed, View targetScene)
		{
			Enum = @enum;
			IsAllowed = isAllowed;
			TargetScene = targetScene;
		}
	}

	private static Live2DToolSelector.Live2DTool[] _blowjobTargetTools = new Live2DToolSelector.Live2DTool[2]
	{
		Live2DToolSelector.HandTool,
		Live2DToolSelector.RemoveClothesTool
	};

	private Slider _cumBar;

	private FrickSceneInitFlags _initFlags;

	private int _frickStart;

	private EquipmentSet _startingEquipmentSet;

	private GameStaticGui _gameStaticGui;

	private GenericLive2DStaticGui _genericLive2DStaticGui;

	private FrickStaticGui _frickStaticGui;

	[CanBeNull]
	private StreamManager _currentStreamManager;

	private Button _returnButton;

	private FrickStateButtonSource _buttonSource;

	protected override Live2DToolSelector TargetToolSelector => _genericLive2DStaticGui.Live2DToolSelector;

	public string MusicSourceId => "NewFrickState";

	protected override IEnumerable<Live2DToolSelector.Live2DTool> TargetTools
	{
		get
		{
			if (!(CurrentController is Live2DBlowjobController))
			{
				return null;
			}
			return _blowjobTargetTools;
		}
	}

	private bool StreamOnFlagSet => (_initFlags & FrickSceneInitFlags.StreamOn) != 0;

	private bool BotFullLeadFlagSet => (_initFlags & FrickSceneInitFlags.BotFullLead) != 0;

	public FrickSceneInitFlags InitFlags => _initFlags;

	[CanBeNull]
	public StreamManager CurrentStreamManager => _currentStreamManager;

	protected override ModelBrainState GetTargetBrainState()
	{
		if (CurrentController is Live2DBlowjobController)
		{
			return new BlowjobBrainState();
		}
		if (CurrentController is Live2DCowgirlController)
		{
			return new CowgirlBrainState();
		}
		return new GenericFuckBrainState();
	}

	public NewFrickState(Live2DLewdSceneController controller, FrickSceneInitFlags initFlags)
		: base(controller)
	{
		_initFlags = initFlags;
	}

	private void AfterControllerChanged()
	{
		if (BotFullLeadFlagSet)
		{
			CurrentController.ActivateBotFullLeadIfPossible();
		}
		UpdateMainButtonList();
		_initFlags &= ~FrickSceneInitFlags.BotFullLead;
	}

	public override void ChangeLive2DController(Live2DLewdSceneController controller)
	{
		base.ChangeLive2DController(controller);
		AfterControllerChanged();
	}

	public override void EnterState()
	{
		GetAndEnableStaticGui<GameStaticGui>(out _gameStaticGui);
		GetAndEnableStaticGui<FrickStaticGui>(out _frickStaticGui);
		GetAndEnableStaticGui<GenericLive2DStaticGui>(out _genericLive2DStaticGui);
		base.EnterState();
		_frickStart = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		_startingEquipmentSet = EquipmentSet.GenerateFromCurrent("Before Frick");
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(MusicSourceId, R.Music.Sex_All, SoundSingleton.MusicOption.MusicOptionLayer.AboveBase);
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
		_cumBar = _frickStaticGui.CumBar;
		_cumBar.value = 0f;
		_buttonSource = new FrickStateButtonSource(this);
		_frickStaticGui.RightButtonList.AddButtonSource(_buttonSource);
		AfterControllerChanged();
		bool streamOnFlagSet = StreamOnFlagSet;
		_frickStaticGui.InitGui(ReturnButtonClicked, streamOnFlagSet);
		if (streamOnFlagSet)
		{
			_currentStreamManager = new StreamManager(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time, this, _frickStaticGui);
			Brain.InteractionMemory.FireEvent(InteractionMemoryEventEnum.StartStream);
		}
		else
		{
			_currentStreamManager = null;
		}
		UpdateMainButtonList();
	}

	public override void ExitState()
	{
		DisableAllClaimedStaticGui();
		_frickStaticGui.EndGui();
		SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(MusicSourceId);
		GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
		base.ExitState();
		SingletonMonoBehaviour<CursorSingleton>.Instance.ClearOverrides();
		EndStreamIfOn();
		gameVariables.EndSex();
		CurrentController = null;
	}

	private void RestoreEquipmentSet()
	{
		_startingEquipmentSet.Equip(resetFirst: true);
	}

	private void EndStreamIfOn()
	{
		_currentStreamManager?.EndStream();
	}

	public override void UpdateState()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			SingletonMonoBehaviour<ToySingleton>.Instance.ShowToySettings = true;
			UpdateMainButtonList();
		}
	}

	public override void FixedUpdateState()
	{
		if (!UiOverlay.Instance.IsOpen)
		{
			_currentStreamManager?.SyncStream(Time.fixedDeltaTime, _cumBar.value + Brain.Pleasure);
			SingletonLoadableMonoBehaviour<GameScript>.Instance.SyncTime(suppressEvents: true);
			if (!Brain.CurrentState.IsReactingUnstoppable && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsOutOfStamina)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Out_of_stamina);
			}
		}
	}

	private void UpdateMainButtonList()
	{
		if (CurrentController is ILive2DController_Manager_FrickStaticGuiManager live2DController_Manager_FrickStaticGuiManager)
		{
			live2DController_Manager_FrickStaticGuiManager.FrickStaticGuiManager.UpdateUI();
		}
		_buttonSource.ConnectedButtonList.ResetAndUpdateEverything();
	}

	private void ClearAllButtonList()
	{
		_buttonSource.ConnectedButtonList.ResetButtons();
		_frickStaticGui.LeftButtonList.ResetButtons();
	}

	public static bool CanSafelyEndSexScene(Live2DController controller)
	{
		bool flag = true;
		bool flag2 = true;
		if (controller is ILive2DController_Manager_CockManager live2DController_Manager_CockManager)
		{
			flag = !live2DController_Manager_CockManager.CockManager.IsCockLimbInside();
			flag2 = live2DController_Manager_CockManager.CockManager.IsCockNormalStateOrHidden;
		}
		bool isCumming = controller.Expression.IsCumming;
		if (flag & flag2)
		{
			return !isCumming;
		}
		return false;
	}

	public static bool CheckAndShowSafety(Live2DController controller)
	{
		if (!CanSafelyEndSexScene(controller))
		{
			UiOverlay.Instance.OkPopup(LOC.UI.UI_CantDoThat, LOC.UI.Frick_EndFailedPrompt);
			return false;
		}
		return true;
	}

	private void PositionButtonClicked()
	{
		if (!CheckAndShowSafety(CurrentController))
		{
			return;
		}
		List<PositionChangeData> list = new List<PositionChangeData>
		{
			new PositionChangeData(ControllerEnum.Missionary, () => true, SingletonMonoBehaviour<ViewSingleton>.Instance.MissionaryView),
			new PositionChangeData(ControllerEnum.Doggy, () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CanDoDoggy, SingletonMonoBehaviour<ViewSingleton>.Instance.DoggyRoomView)
			{
				RequirementText = LOC.UI.Frick_LegsRequired,
				HideIfNotAllowed = true
			},
			new PositionChangeData(ControllerEnum.Blowjob, () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CanDoBlowjob, SingletonMonoBehaviour<ViewSingleton>.Instance.BlowjobView),
			new PositionChangeData(ControllerEnum.Cowgirl, () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CanDoCowgirl, SingletonMonoBehaviour<ViewSingleton>.Instance.CowgirlView)
			{
				RequirementText = LOC.UI.Frick_LegsAndArmsRequired,
				HideIfNotAllowed = true
			}
		};
		List<MenuOption> list2 = new List<MenuOption>();
		ControllerEnum controllerEnum = CurrentController.ControllerEnum;
		foreach (PositionChangeData pcd in list)
		{
			if (controllerEnum == pcd.Enum)
			{
				continue;
			}
			bool flag = pcd.IsAllowed();
			if (flag || !pcd.HideIfNotAllowed)
			{
				list2.Add(new MenuOption(pcd.Enum.GetControllerLocalizedName().FirstCharToUpper() + pcd.RequirementText, flag, () =>
				{
					GoToFrickScene(pcd.TargetScene);
				}));
			}
		}
		list2.Add(new MenuOption(LOC.UI.Interact_Nevermind, () =>
		{
			UiOverlay.Instance.CloseFungusOverlay();
		}));
		UiOverlay.Instance.OpenFungusOverlay();
		SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartMenu(list2);
		void GoToFrickScene(View view)
		{
			ClearAllButtonList();
			SingletonMonoBehaviour<FungusEnumerators>.Instance.SwitchSexScene(view).ContinueWith(() =>
			{
				UiOverlay.Instance.CloseFungusOverlay();
			}).Forget();
		}
	}

	private void ReturnButtonClicked()
	{
		if (GameVariables.Current.botLive2DCommonState.FrickData.IsBotCompletelyInTheLead)
		{
			ModelBrainState currentState = CurrentController.CurrentBrain.CurrentState;
			if (currentState is CowgirlBrainState cowgirlBrainState)
			{
				cowgirlBrainState.React(CowgirlBrainState.CowgirlReactions.BegStop);
			}
			else if (currentState is BlowjobBrainState blowjobBrainState)
			{
				blowjobBrainState.React(BlowjobBrainState.BlowjobReaction.BegStop);
			}
		}
		else if (_currentStreamManager != null)
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Frick_StreamEndPrompt, () =>
			{
				UiOverlay.Instance.OkPopup(LOC.UI.Frick_LastStreamSummary, LOC.UI.Frick_LastStreamSummaryPrompt(_currentStreamManager.StreamLength, StringUtils.TMPColorNumber(_currentStreamManager.SubsGained), StringUtils.TMPColorNumber(_currentStreamManager.SubsLost, negative: true), StringUtils.TMPColorNumber(_currentStreamManager.SubsGained - _currentStreamManager.SubsLost), SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.subs, _currentStreamManager.MoneyEarnedDonations), () =>
				{
					AskRestore(SingletonLoadableMonoBehaviour<GameScript>.Instance.EndFricking);
				});
			});
			if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("unlockedslowmodeemail") && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.followers >= GameVariables.FollowersNeededForSlowMode)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddFlag("unlockedslowmodeemail");
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				string emailId = EmailPrefabManager.SlowmodeEmail;
				string[] array = new string[1];
				int followersNeededForSlowMode = GameVariables.FollowersNeededForSlowMode;
				array[0] = followersNeededForSlowMode.ToString();
				gameVariables.AddEmail(EmailPrefabManager.GetEmail(emailId, array));
			}
		}
		else
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Frick_EndFrickPrompt, () =>
			{
				AskRestore(SingletonLoadableMonoBehaviour<GameScript>.Instance.EndFricking);
			});
		}
		void AskRestore(Action then)
		{
			if (!_startingEquipmentSet.IsCurrentlyEquipped())
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.Frick_Restore, LOC.UI.Frick_RestorePrompt, () =>
				{
					RestoreEquipmentSet();
					then?.Invoke();
				}, () =>
				{
					then?.Invoke();
				});
			}
			else
			{
				then?.Invoke();
			}
		}
	}

	public int GetFrickLength()
	{
		return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time - _frickStart;
	}

	public int GetStreamViewers()
	{
		if (_currentStreamManager == null)
		{
			return 0;
		}
		return _currentStreamManager.Viewers;
	}
}
