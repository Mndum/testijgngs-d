using System.Collections.Generic;
using Fungus;
using UnityEngine.UI;

public class CuddleState : GameStateWithLive2D<Live2DInteractionController>
{
	private static Live2DToolSelector.Live2DTool[] _cuddleTargetTools = new Live2DToolSelector.Live2DTool[2]
	{
		Live2DToolSelector.HandTool,
		Live2DToolSelector.MouthTool
	};

	private GameStaticGui _gameStaticGui;

	private CuddleStaticGui _cuddleStaticGui;

	private GenericLive2DStaticGui _genericLive2DStaticGui;

	private Button _returnButton;

	protected override Live2DToolSelector TargetToolSelector => _genericLive2DStaticGui.Live2DToolSelector;

	protected override IEnumerable<Live2DToolSelector.Live2DTool> TargetTools => _cuddleTargetTools;

	public CuddleState(Live2DInteractionController live2DController)
		: base(live2DController)
	{
	}

	protected override ModelBrainState GetTargetBrainState()
	{
		return new CuddleBrainState();
	}

	private void ActionButtonClicked()
	{
		List<MenuOption> list = new List<MenuOption>();
		if (CurrentController.CurrentBrain.CurrentState is CuddleBrainState cuddleBrainState)
		{
			cuddleBrainState.FillActionOptions(list);
		}
		UiOverlay.Instance.OpenFungusOverlay();
		list.Add(new MenuOption
		{
			text = LOC.UI.Interact_Nevermind,
			beforeNextBlock = () =>
			{
				UiOverlay.Instance.CloseFungusOverlay();
			}
		});
		SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartMenu(list);
	}

	private void TalkButtonClicked()
	{
		List<MenuOption> list = new List<MenuOption>();
		if (CurrentController.CurrentBrain.CurrentState is CuddleBrainState cuddleBrainState)
		{
			cuddleBrainState.FillTalkOptions(list);
		}
		UiOverlay.Instance.OpenFungusOverlay();
		list.Add(new MenuOption
		{
			text = LOC.UI.Interact_Nevermind,
			beforeNextBlock = () =>
			{
				UiOverlay.Instance.CloseFungusOverlay();
			}
		});
		SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartMenu(list);
	}

	private void UpdateSideButtons()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (_cuddleStaticGui.ButtonList.DoInitialization(out modificationPeriod))
		{
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
			buttonListWrapper.OnClick = ActionButtonClicked;
			buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Interact_Action);
			ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton();
			buttonListWrapper2.OnClick = TalkButtonClicked;
			buttonListWrapper2.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Interact_Talk);
		}
	}

	public override void EnterState()
	{
		GetAndEnableStaticGui<GameStaticGui>(out _gameStaticGui);
		GetAndEnableStaticGui<CuddleStaticGui>(out _cuddleStaticGui);
		GetAndEnableStaticGui<GenericLive2DStaticGui>(out _genericLive2DStaticGui);
		base.EnterState();
		SingletonLoadableMonoBehaviour<AchievementSingleton>.Instance.AchievementHandler.AddAchievement("sleepingTogether");
		_gameStaticGui.SyncGui(GameVariables.Current);
		_returnButton = _cuddleStaticGui.ReturnButton;
		_returnButton.onClick.SetListener(ReturnButtonClicked);
		UpdateSideButtons();
	}

	private void ReturnButtonClicked()
	{
		UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Cuddle_EndPrompt, SingletonLoadableMonoBehaviour<GameScript>.Instance.EndInteracting);
	}

	public override void ExitState()
	{
		_cuddleStaticGui.ButtonList.ResetButtons();
		base.ExitState();
		GameVariables current = GameVariables.Current;
		current.lastCuddledAt = current.time;
		_returnButton.onClick.RemoveAllListeners();
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
		if (!UiOverlay.Instance.IsOpen && Brain.CurrentState is CuddleBrainState { ShouldTimePass: not false } cuddleBrainState)
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.SyncTime(suppressEvents: true);
			if (!Brain.CurrentState.IsReactingUnstoppable && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsOutOfStamina)
			{
				cuddleBrainState.React(CuddleBrainState.CuddleReactions.Sleep);
			}
		}
	}
}
