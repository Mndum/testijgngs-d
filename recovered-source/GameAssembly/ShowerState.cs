using System.Collections.Generic;
using System.Linq;
using Live2D.Cubism.Core;
using UnityEngine;

public class ShowerState : GameStateWithLive2D<Live2DShowerController>, IMusicSource
{
	private enum ShowerStateState
	{
		None,
		Cleaning,
		CleaningDone
	}

	private abstract class ShowerStateSubState : ButtonListButtonSource
	{
		public abstract bool PassTime { get; }

		public abstract void ReturnButtonClicked();
	}

	private class ShowerStateCleaningRightButtons : ShowerStateSubState
	{
		private ShowerState _showerState;

		public override bool PassTime => false;

		public ShowerStateCleaningRightButtons(ShowerState showerState)
		{
			_showerState = showerState;
		}

		public override void AddButtonsToList(IModificationPeriodButtonList buttonList)
		{
			ButtonListWrapper buttonListWrapper = buttonList.AddButton();
			buttonListWrapper.OnClick = SoapButtonClicked;
			buttonListWrapper.SetText(LOC.UI.Shower_Soap);
			ButtonListWrapper buttonListWrapper2 = buttonList.AddButton();
			buttonListWrapper2.OnClick = SpongeButtonClicked;
			buttonListWrapper2.SetText(LOC.UI.Shower_Sponge);
			ButtonListWrapper buttonListWrapper3 = buttonList.AddButton();
			buttonListWrapper3.OnClick = WashOffSoapButtonClicked;
			buttonListWrapper3.SetText(LOC.UI.Shower_WashOff);
			if (OptionsStatic.DebugEnabled)
			{
				ButtonListWrapper buttonListWrapper4 = buttonList.AddButton();
				buttonListWrapper4.OnClick = SwitchToSex;
				buttonListWrapper4.SetText("DEBUG: SWITCH TO SEX");
			}
		}

		private void SoapButtonClicked()
		{
			if (_showerState.HasSoap())
			{
				_showerState.Live2DToolSelector.SetCurrentTool(Live2DToolSelector.SoapTool);
				return;
			}
			Selection<Item> selection = SelectionFactory.CreateInstanceCustom((Item x) => x.GetCount());
			selection.MaxTotalItemCount = 1;
			selection.MaxUniqueItemCount = 1;
			selection.PartialAllowed = false;
			CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
				.WithAllFolder()
				.WithDefaultGameItemSetItem()
				.WithDefaultGameItemListItemPool()
				.Build((from x in GameVariables.Current.itemManager.AllItemsEnumerable()
					where x.GetItemPrefab().Category == ItemCategory.Soap
					select x).ToFrozenDataSource())
				.ToChooseItemsComplexPopupBuilderItem()
				.WithStartingSelection(selection)
				.WithOnFinished((UiOverlay.PopupFinishTypesEnum a, Selection<Item> b) =>
				{
					if (a == UiOverlay.PopupFinishTypesEnum.Successful && b.TryGetFirstItem(out var item))
					{
						GameVariables.Current.itemManager.ConsumeItem(item, 1, out var outCopy);
						_showerState.SetSoap(outCopy);
						_showerState.Live2DToolSelector.SetCurrentTool(Live2DToolSelector.SoapTool);
					}
				})
				.OpenPopup(LOC.UI.Shower_Soap, LOC.UI.Shower_ChooseSoap, allowClosing: true, showCancel: true);
		}

		private void SpongeButtonClicked()
		{
			_showerState.Live2DToolSelector.SetCurrentTool(Live2DToolSelector.SpongeTool);
		}

		private void WashOffSoapButtonClicked()
		{
			if (_showerState.CurrentController.HasFinishedCleaning())
			{
				_showerState.BrainReact(ShowerBrainState.ShowerReactions.WashOffSoap);
				_showerState.ChangeShowerState(ShowerStateState.CleaningDone);
			}
			else
			{
				_showerState.BrainReact(ShowerBrainState.ShowerReactions.NeedsScrubbing);
			}
		}

		private void SwitchToSex()
		{
			_showerState.ChangeShowerState(ShowerStateState.CleaningDone);
			((ShowerBrainState)_showerState.Brain.CurrentState).StartAfterWashing();
		}

		public override void ReturnButtonClicked()
		{
			if (!_showerState.HasSoap())
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Shower_ExitPromptBeforeAnything, _showerState.EndScene);
			}
			else
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Shower_ExitPromptNotFinished, _showerState.EndScene);
			}
		}
	}

	private class ShowerStateFrickRightButtons : ShowerStateSubState
	{
		private ShowerState _showerState;

		private ButtonListWrapper _noMusicButtonWrapper;

		private ButtonListWrapper sexButton;

		public bool HasPulledOutAlready;

		private SoundSingleton.MusicOption _silenceMusicOption;

		private bool MusicOff => _silenceMusicOption != null;

		public override bool PassTime => true;

		public ShowerStateFrickRightButtons(ShowerState showerState)
		{
			_showerState = showerState;
		}

		public override void AddButtonsToList(IModificationPeriodButtonList buttonList)
		{
			_noMusicButtonWrapper = buttonList.AddButton(-20);
			_noMusicButtonWrapper.TextFunc = () => (!MusicOff) ? LOC.UI.Frick_MusicOn : LOC.UI.Frick_MusicOff;
			_noMusicButtonWrapper.OnClick = NoMusicButtonClicked;
			_noMusicButtonWrapper.ActiveFunc = () => !SoundSingleton.MusicOff && HasPulledOutAlready;
			sexButton = buttonList.AddButton();
			sexButton.OnClick = PulloutButtonClicked;
			sexButton.SetText(LOC.UI.Shower_Sex);
			sexButton.ActiveFunc = () => !_showerState._frickStaticGui.CumBar.gameObject.activeSelf;
		}

		private void PulloutButtonClicked()
		{
			_showerState._frickStaticGui.SetDefaultUIActive(active: true, noXray: true);
			_showerState.CurrentController.CockManager.WhipOutCock();
			ConnectedButtonList.UpdateButtonSingle(sexButton);
			if (!HasPulledOutAlready)
			{
				_showerState.BrainReact(ShowerBrainState.ShowerReactions.CockOut);
				SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(_showerState.MusicSourceId, R.Music.Sex_All, SoundSingleton.MusicOption.MusicOptionLayer.AboveBase);
				SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
				HasPulledOutAlready = true;
				ConnectedButtonList.UpdateButtonSingle(_noMusicButtonWrapper);
			}
		}

		private void NoMusicButtonClicked()
		{
			if (_silenceMusicOption == null)
			{
				_silenceMusicOption = new SoundSingleton.MusicOption(_showerState.MusicSourceId, SoundSingleton.MusicOption.MusicOptionLayer.Forced, null);
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

		public override void ReturnButtonClicked()
		{
			if (HasPulledOutAlready)
			{
				if (NewFrickState.CheckAndShowSafety(_showerState.CurrentController))
				{
					_showerState.BrainReact(ShowerBrainState.ShowerReactions.EndSex);
				}
			}
			else
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Shower_ExitPrompt, _showerState.EndScene);
			}
		}
	}

	private static Live2DToolSelector.Live2DTool[] _showerCleanTargetTools = new Live2DToolSelector.Live2DTool[3]
	{
		Live2DToolSelector.HandTool,
		Live2DToolSelector.SoapTool,
		Live2DToolSelector.SpongeTool
	};

	private static Live2DToolSelector.Live2DTool[] _showerFuckTargetTools = new Live2DToolSelector.Live2DTool[2]
	{
		Live2DToolSelector.HandTool,
		Live2DToolSelector.SpongeTool
	};

	private ShowerStaticGui _showerStaticGui;

	private FrickStaticGui _frickStaticGui;

	private GenericLive2DStaticGui _genericLive2DStaticGui;

	private ShowerStateSubState _subState;

	private Item _currentSoapItem;

	private SoapItemData _soapItemData;

	private ShowerStateState CurrentState;

	protected override Live2DToolSelector TargetToolSelector => _genericLive2DStaticGui.Live2DToolSelector;

	protected override IEnumerable<Live2DToolSelector.Live2DTool> TargetTools => _showerCleanTargetTools;

	public string MusicSourceId => "Shower State";

	public ShowerState(Live2DShowerController live2DController)
		: base(live2DController)
	{
	}

	protected override ModelBrainState GetTargetBrainState()
	{
		return new ShowerBrainState();
	}

	private bool HasSoap()
	{
		return _currentSoapItem != null;
	}

	private void SetSoap(Item soap)
	{
		_currentSoapItem = soap;
		_soapItemData = new SoapItemData();
		_soapItemData.SoapColor = (soap.HasColor ? soap.GetMainColor() : ((Color)new Color32(224, 191, 235, 255)));
		UpdateSoapLeft();
	}

	private void UpdateSoapLeft()
	{
		if (HasSoap())
		{
			_showerStaticGui.SetSoapLeftBar(CurrentController.GetSoapUsageLeft());
		}
		else
		{
			_showerStaticGui.SetSoapLeftBar(0f);
		}
	}

	private void UpdateCleaningProgress()
	{
		_showerStaticGui.SetCleaningProgressBar(CurrentController.GetCleaningProgress());
	}

	private void ChangeShowerState(ShowerStateState newState)
	{
		bool flag = CurrentState == ShowerStateState.None;
		if (!flag)
		{
			_subState?.Remove();
			_subState = null;
		}
		switch (newState)
		{
		case ShowerStateState.Cleaning:
			_subState = new ShowerStateCleaningRightButtons(this);
			_frickStaticGui.RightButtonList.AddButtonSource(_subState);
			break;
		case ShowerStateState.CleaningDone:
		{
			_subState = new ShowerStateFrickRightButtons(this);
			_frickStaticGui.RightButtonList.AddButtonSource(_subState);
			TargetToolSelector.ChangeController(CurrentController, _showerFuckTargetTools);
			_showerStaticGui.gameObject.SetActive(value: false);
			_showerStaticGui.EndGui();
			GetAndEnableStaticGui<GameStaticGui>(out var _);
			break;
		}
		}
		CurrentState = newState;
		if (!flag)
		{
			UpdateAllUI();
		}
	}

	private void BrainReact(ShowerBrainState.ShowerReactions reaction)
	{
		((ShowerBrainState)Brain.CurrentState).React(reaction);
	}

	public override void EnterState()
	{
		GetAndEnableStaticGui<ShowerStaticGui>(out _showerStaticGui);
		GetAndEnableStaticGui<GenericLive2DStaticGui>(out _genericLive2DStaticGui);
		GetAndEnableStaticGui<FrickStaticGui>(out _frickStaticGui);
		base.EnterState();
		SingletonLoadableMonoBehaviour<AchievementSingleton>.Instance.AchievementHandler.AddAchievement("shower");
		_showerStaticGui.InitGui();
		_frickStaticGui.InitGui(ReturnButtonClicked, streamOn: false, defaultUiActive: false);
		ChangeShowerState(ShowerStateState.Cleaning);
		Vector2? lastBubblePosition = null;
		CurrentController.SetFullBodyInteractable(new Live2DFullBodyInteractable
		{
			OnHoldStart = (CubismDrawable x) =>
			{
				lastBubblePosition = null;
			},
			OnHeldFixed = () =>
			{
				Vector2 vector = UiOverlay.Instance.MouseToWorld().ToVector2();
				if (CurrentController.HasAppliedEnoughSoap())
				{
					UiOverlay.Instance.ShowFloatingText(vector, LOC.UI.Shower_EnoughSoap);
					CurrentController.LetGoOfCurrentlyHeld();
				}
				else if (!HasSoap())
				{
					UiOverlay.Instance.ShowFloatingText(vector, LOC.UI.Shower_NoSoap);
					CurrentController.LetGoOfCurrentlyHeld();
				}
				else if (lastBubblePosition.HasValue)
				{
					Vector2 vector2 = lastBubblePosition.Value - vector;
					float magnitude = vector2.magnitude;
					if (!(magnitude < 0.01f))
					{
						Vector2 vector3 = vector2 / magnitude;
						int num = Mathf.FloorToInt(magnitude / 0.32f);
						int num2 = 0;
						for (int i = 0; i < num; i++)
						{
							Vector2 position = vector + vector3 * i * 0.32f;
							if (CurrentController.TryCreateSoap(_showerStaticGui.SoapPrefab, position, _soapItemData, SoapParticleType.Liquid))
							{
								lastBubblePosition = vector;
								num2++;
							}
						}
						if (num2 > 0)
						{
							UpdateSoapLeft();
						}
					}
				}
				else if (CurrentController.TryCreateSoap(_showerStaticGui.SoapPrefab, vector, _soapItemData, SoapParticleType.Liquid))
				{
					lastBubblePosition = vector;
					UpdateSoapLeft();
				}
			}
		}, Live2DToolSelector.SoapTool.Live2DLayer);
		CursorSingleton.CursorOverride fondleSpongeOverride = null;
		Vector2? lastClearPosition = null;
		CurrentController.SetFullBodyInteractable(new Live2DFullBodyInteractable
		{
			OnHoldStart = (CubismDrawable x) =>
			{
				lastClearPosition = null;
			},
			OnHeld = (Vector2 move) =>
			{
				if (CurrentController.IsLimbUsageExactlyCorrect(CurrentController.BoobFondleLimbUsage))
				{
					CurrentController.BoobFondleLimbUsage.ContinueHold(move * 2f);
				}
				if (CurrentController.IsLimbUsageExactlyCorrect(CurrentController.AssFondleLimbUsage))
				{
					CurrentController.AssFondleLimbUsage.ContinueHold(move * 3f);
				}
			},
			OnHeldFixed = () =>
			{
				Vector2 vector = UiOverlay.Instance.MouseToWorld().ToVector2();
				Ray ray = UiOverlay.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
				if (CurrentController.IsLimbUsageExactlyCorrect(CurrentController.BoobFondleLimbUsage))
				{
					Rect r = CurrentController.HitAreaBoob.CalculateApproximateWorldRect();
					if (r.DistanceFromEdge(vector).SqrMagnitude() > 4f)
					{
						CurrentController.LetGoLimbs(CurrentController.BoobFondleLimbUsage, LetGoLimbsCaller.Outside);
						CursorSingleton.EndCursorOverride(ref fondleSpongeOverride);
					}
					else
					{
						vector = r.ClampPoint(vector);
						CurrentController.BoobFondleLimbUsage.ContinueHoldFixed();
					}
				}
				else if (CurrentController.IsLimbUsageExactlyCorrect(CurrentController.AssFondleLimbUsage))
				{
					Rect r2 = CurrentController.HitAreaAss.CalculateApproximateWorldRect();
					if (r2.DistanceFromEdge(vector).SqrMagnitude() > 4f)
					{
						CurrentController.LetGoLimbs(CurrentController.AssFondleLimbUsage, LetGoLimbsCaller.Outside);
						CursorSingleton.EndCursorOverride(ref fondleSpongeOverride);
					}
					else
					{
						vector = r2.ClampPoint(vector);
						CurrentController.AssFondleLimbUsage.ContinueHoldFixed();
					}
				}
				else
				{
					if (CurrentController.Model.RaycastSingle(ray, CurrentController.HitAreaBoob.CubismRenderer) && CurrentController.EnsureLimbs(CurrentController.BoobFondleLimbUsage))
					{
						CursorSingleton.ContinueOrStartCursorOverride(ref fondleSpongeOverride, CursorSingleton.CursorType.NoCursor);
					}
					if (CurrentController.Model.RaycastSingle(ray, CurrentController.HitAreaAss.CubismRenderer) && CurrentController.EnsureLimbs(CurrentController.AssFondleLimbUsage))
					{
						CursorSingleton.ContinueOrStartCursorOverride(ref fondleSpongeOverride, CursorSingleton.CursorType.NoCursor);
					}
				}
				if (lastClearPosition.HasValue)
				{
					CurrentController.SpreadSoap(_showerStaticGui.SoapPrefab, lastClearPosition.Value, vector);
					UpdateCleaningProgress();
				}
				lastClearPosition = vector;
			},
			OnHoldEnd = () =>
			{
				CurrentController.LetGoLimbs(CurrentController.BoobFondleLimbUsage, LetGoLimbsCaller.Outside);
				CurrentController.LetGoLimbs(CurrentController.AssFondleLimbUsage, LetGoLimbsCaller.Outside);
				CurrentController.LiftSpongeAt(UiOverlay.Instance.MouseToWorld().ToVector2());
				CursorSingleton.EndCursorOverride(ref fondleSpongeOverride);
			}
		}, Live2DToolSelector.SpongeTool.Live2DLayer);
		UpdateAllUI();
	}

	private void UpdateAllUI()
	{
		((ILive2DController_Manager_FrickStaticGuiManager)CurrentController)?.FrickStaticGuiManager.UpdateUI();
		_frickStaticGui.RightButtonList.ResetAndUpdateEverything();
	}

	private void ReturnButtonClicked()
	{
		if (_subState != null)
		{
			_subState.ReturnButtonClicked();
		}
		else
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Shower_ExitPrompt, EndScene);
		}
	}

	private void EndScene()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.EndInteracting();
	}

	public override void ExitState()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(this);
		_showerStaticGui.EndGui();
		_frickStaticGui.EndGui();
		base.ExitState();
		GameVariables current = GameVariables.Current;
		current.lastAnonsShowerAt = current.time;
		current.botLive2DCommonState.ResetFrickData();
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
		if (!UiOverlay.Instance.IsOpen && _subState != null && _subState.PassTime)
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.SyncTime(suppressEvents: true);
			if (!Brain.CurrentState.IsReactingUnstoppable && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsOutOfStamina)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Out_of_stamina);
			}
		}
	}
}
