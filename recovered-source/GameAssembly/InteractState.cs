using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;

public class InteractState : GameStateWithLive2D<Live2DInteractionController>
{
	private GameStaticGui _gameStaticGui;

	private GenericLive2DStaticGui _genericLive2DStaticGui;

	private InteractStaticGui _interactStaticGui;

	private ButtonList _buttonList;

	private ButtonListWrapper _zoomButtonWrapper;

	private ButtonListWrapper _talkButtonWrapper;

	private ButtonListWrapper _doThingsTogetherButtonWrapper;

	private ButtonListWrapper _eqButtonWrapper;

	private ButtonListWrapper _clothesButtonWrapper;

	private ButtonListWrapper _otherButtonWrapper;

	private ButtonListWrapper _frickButtonWrapper;

	private ButtonListWrapper _cleanButtonWrapper;

	private bool _isZoomedIn;

	protected override Live2DToolSelector TargetToolSelector => _genericLive2DStaticGui.Live2DToolSelector;

	public bool StatisticsUnlocked => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("statistics");

	public static bool CookingUnlocked => GameVariables.Current.CanCook;

	public static bool PlayingUnlocked => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("cards");

	public InteractState(Live2DInteractionController live2DController)
		: base(live2DController)
	{
	}

	protected override ModelBrainState GetTargetBrainState()
	{
		return new NewInteractBrainState();
	}

	public override void EnterState()
	{
		GetAndEnableStaticGui<GameStaticGui>(out _gameStaticGui);
		GetAndEnableStaticGui<InteractStaticGui>(out _interactStaticGui);
		GetAndEnableStaticGui<GenericLive2DStaticGui>(out _genericLive2DStaticGui);
		base.EnterState();
		_gameStaticGui.SyncGui(GameVariables.Current);
		_buttonList = _interactStaticGui.ButtonList;
		IModificationPeriodButtonList modificationPeriod;
		using (_buttonList.DoInitialization(out modificationPeriod))
		{
			_zoomButtonWrapper = modificationPeriod.AddButton(-10);
			_zoomButtonWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Equipment_Zoom);
			_zoomButtonWrapper.OnClick = ZoomButtonClicked;
			_talkButtonWrapper = modificationPeriod.AddButton();
			_talkButtonWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Interact_Talk);
			_talkButtonWrapper.OnClick = TalkButtonClicked;
			_doThingsTogetherButtonWrapper = modificationPeriod.AddButton(5);
			_doThingsTogetherButtonWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Room_DoSomethingTogether);
			_doThingsTogetherButtonWrapper.ActiveFunc = () => !_isZoomedIn;
			_doThingsTogetherButtonWrapper.OnClick = DoThingsTogetherButtonClicked;
			_eqButtonWrapper = modificationPeriod.AddButton(10);
			_eqButtonWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Room_Equipment);
			_eqButtonWrapper.OnClick = () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.StartEquipment();
			};
			_clothesButtonWrapper = modificationPeriod.AddButton(15);
			_clothesButtonWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Room_Clothes);
			_clothesButtonWrapper.OnClick = () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.StartEquipment(EquipmentStaticGuiStateEnum.Clothes);
			};
			_clothesButtonWrapper.ActiveFunc = () => GameVariables.Current.HasMetAnnalie;
			_cleanButtonWrapper = modificationPeriod.AddButton(25);
			_cleanButtonWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Interact_Clean);
			_cleanButtonWrapper.OnClick = CleanButtonClicked;
			_cleanButtonWrapper.ActiveFunc = () => GameVariables.Current.IsThereCumOnHer && !_isZoomedIn;
		}
	}

	public override void ExitState()
	{
		_buttonList.ResetButtons();
		base.ExitState();
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lastInteractAt = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		SingletonMonoBehaviour<CursorSingleton>.Instance.ClearOverrides();
	}

	private void GetDialogueChains(HashSet<StoryBotDialogueBranch> uniqueDialogues, HashSet<StoryBotDialogueBranch> genericDialogues)
	{
		int uniqueDialoguesCount = Mathf.Min(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.UniqueConversationsLeft, 3);
		if (SingletonMonoBehaviour<StorySingleton>.Instance.TryGetTag("Interact_MainDialogue", out var storyTag))
		{
			storyTag.GetDialogues(uniqueDialogues, genericDialogues, uniqueDialoguesCount, SceneEnumFlag.Interaction);
		}
	}

	private bool CanMoveToNextStage()
	{
		if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.ReadyForNextStage)
		{
			return false;
		}
		int stage = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.stage;
		return SingletonMonoBehaviour<StorySingleton>.Instance.GetStage(stage).IsPrologueFinished();
	}

	private void StartStoryStateBlock(FungusBlocks.FungusBlockReference blockReference)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(blockReference, () =>
		{
			OnStoryStateFinished(_isZoomedIn);
		});
	}

	public void StartDialogue(StoryBotDialogueBranch branch)
	{
		StoryBranchTarget storyBranchTarget = branch.ActivateGetTarget();
		if (storyBranchTarget is IBotDialogueBranchTarget botDialogueBranchTarget)
		{
			botDialogueBranchTarget.Activate(Brain.CurrentState);
			return;
		}
		CurrentController.PrepareForDialogue();
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryState();
		CurrentController.CurrentBrain.EnableBrain();
		CurrentController.CurrentBrain.ChangeState(new StoryBrainState());
		CurrentController.SetEnabled(v: true);
		storyBranchTarget.Activate(() =>
		{
			OnStoryStateFinished(_isZoomedIn);
		});
	}

	private static void OnStoryStateFinished(bool isZoomedIn = false)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lastTalkedAt = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.TryGetStateAs<GameScript.StoryState>(out var _))
		{
			return;
		}
		SingletonLoadableMonoBehaviour<GameScript>.Instance.StartInteract((InteractState x) =>
		{
			if (x.Brain.CurrentState is NewInteractBrainState newInteractBrainState)
			{
				newInteractBrainState.AlreadyDidGreeting = true;
			}
		}, isZoomedIn);
	}

	public static View GetInteractView(bool zoomedIn)
	{
		if (!zoomedIn)
		{
			return SingletonMonoBehaviour<ViewSingleton>.Instance.RoomCharacterView;
		}
		return SingletonMonoBehaviour<ViewSingleton>.Instance.RoomCharacterViewZoom;
	}

	public void ZoomButtonClicked()
	{
		_isZoomedIn = !_isZoomedIn;
		View interactView = GetInteractView(_isZoomedIn);
		UiOverlay.Instance.OpenFungusOverlay(Color.clear);
		SingletonMonoBehaviour<GameUtilities>.Instance.FadeOrMoveToView(interactView, () =>
		{
			UiOverlay.Instance.CloseFungusOverlay();
			base.EnterState();
			if (CurrentController.CurrentBrain.CurrentState is NewInteractBrainState newInteractBrainState)
			{
				newInteractBrainState.AlreadyDidGreeting = true;
			}
			_buttonList.UpdateCurrentButtonState();
		});
	}

	public void TalkButtonClicked()
	{
		if (CanMoveToNextStage())
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.stage++;
		}
		HashSet<StoryBotDialogueBranch> hashSet;
		using (MyCollectionPool<StoryBotDialogueBranch>.GetHashSet(out hashSet))
		{
			HashSet<StoryBotDialogueBranch> hashSet2;
			using (MyCollectionPool<StoryBotDialogueBranch>.GetHashSet(out hashSet2))
			{
				GetDialogueChains(hashSet, hashSet2);
				List<MenuOption> list = new List<MenuOption>();
				if (hashSet2.Any())
				{
					StoryBotDialogueBranch random = hashSet2.GetRandom();
					list.Add(new MenuOption
					{
						text = LOC.UI.Interact_SmallTalk,
						beforeNextBlock = () =>
						{
							StartDialogue(random);
						}
					});
				}
				list.AddRange(hashSet.Select((StoryBotDialogueBranch t) => new MenuOption
				{
					text = t.Title + (t.IsOneTime ? "" : LOC.UI.Interact_Generic),
					beforeNextBlock = () =>
					{
						StartDialogue(t);
						SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.UniqueConversationsLeft--;
					}
				}));
				if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.stage >= 3 && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Day > 20)
				{
					list.Add(new MenuOption
					{
						text = LOC.UI.ChangeBotName,
						beforeNextBlock = () =>
						{
							StartStoryStateBlock(FungusBlocks.Stage3_ChangeBotName);
						}
					});
					list.Add(new MenuOption
					{
						text = LOC.UI.ChangePlayerName,
						beforeNextBlock = () =>
						{
							StartStoryStateBlock(FungusBlocks.Stage3_ChangePlayerName);
						}
					});
				}
				if (StatisticsUnlocked)
				{
					list.Add(new MenuOption
					{
						text = LOC.UI.Interact_Statistics,
						beforeNextBlock = () =>
						{
							StartStoryStateBlock(FungusBlocks.Stage3_Statistics);
						}
					});
				}
				list.Add(new MenuOption
				{
					text = LOC.UI.Interact_Nevermind
				});
				SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartMenu(list, automaticUIOverlay: true);
			}
		}
	}

	private void CleanButtonClicked()
	{
		List<MenuOption> list = new List<MenuOption>();
		list.Add(new MenuOption
		{
			text = LOC.UI.Interact_CleanWithARag,
			beforeNextBlock = () =>
			{
				StartStoryStateBlock(FungusBlocks.StageAny_CleanWithARag);
			}
		});
		if (GameVariables.Current.FirstShowerHappened)
		{
			list.Add(new MenuOption
			{
				text = LOC.UI.Room_Shower,
				beforeNextBlock = () =>
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.StartBathroom();
				}
			});
		}
		list.Add(new MenuOption
		{
			text = LOC.UI.Interact_Nevermind
		});
		SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartMenu(list, automaticUIOverlay: true);
	}

	public void DoThingsTogetherButtonClicked()
	{
		List<PopupChoice> list = new List<PopupChoice>();
		if (PlayingUnlocked)
		{
			list.Add(new PopupChoice
			{
				Text = LOC.UI.Interact_PlayGame,
				Action = PlayButtonClicked
			});
		}
		if (CookingUnlocked)
		{
			list.Add(new PopupChoice
			{
				Text = LOC.UI.Room_Cook,
				Action = () =>
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.StartCooking();
				}
			});
		}
		list.Add(new PopupChoice
		{
			Text = LOC.UI.Interact_Sex,
			Action = () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.PrepareFricking();
			}
		});
		UiOverlay.Instance.OpenContextMenuPopup(list);
	}

	public void PlayButtonClicked()
	{
		List<PopupChoice> list = new List<PopupChoice>();
		if (PlayingUnlocked)
		{
			list.Add(new PopupChoice
			{
				Text = LOC.UI.Interact_OldMaid,
				Action = () =>
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new GameScript.GameWithBotState(CurrentController));
				}
			});
		}
		UiOverlay.Instance.OpenContextMenuPopup(list);
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
		if (!UiOverlay.Instance.IsOpen)
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.SyncTime(suppressEvents: true);
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsOutOfStamina)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Out_of_stamina);
			}
		}
	}
}
