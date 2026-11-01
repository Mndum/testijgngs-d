using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class ModelBrain : MonoBehaviour
{
	public enum DialogueType
	{
		StartBlock,
		StartComment
	}

	[SerializeField]
	[Range(0f, 1f)]
	private float pleasure;

	[SerializeField]
	[Range(0f, 1f)]
	private float pain;

	private static float _painDecayInSeconds = 0.4f;

	public const float PleasureDecayFixedUpdate = 1E-05f;

	private static float _painDecay;

	private InteractionMemory _interactionMemory = new InteractionMemory();

	private WaitUntil _waitUntilInner;

	private UpdatableStateMachine<ModelBrainState, ModelBrain> _modelStateMachine;

	private Live2DController _connectedController;

	private string relatedControllerId;

	private static Dictionary<string, ModelBrain> _brainById = new Dictionary<string, ModelBrain>();

	public float Pleasure
	{
		get
		{
			return pleasure;
		}
		set
		{
			pleasure = Mathf.Clamp(value, 0f, 1f);
		}
	}

	public float Pain
	{
		get
		{
			return pain;
		}
		set
		{
			pain = Mathf.Clamp(value, 0f, 1f);
		}
	}

	public bool CanCum => Pleasure >= 0.99f;

	public bool IsInterested
	{
		get
		{
			if (!ConnectedController.IsAnyInteractableHeld && !_interactionMemory.IsInterested && !(Pleasure > 0.3f))
			{
				return _modelStateMachine.CurrentState.IsInterested;
			}
			return true;
		}
	}

	public PersonalityGameStats PersonalityData => GameVariables.Current.GetPersonalityGameStats();

	public InteractionMemory InteractionMemory => _interactionMemory;

	public bool NotRecentlyStartedTalking
	{
		get
		{
			if (!_interactionMemory.GetSimpleMemory(InteractionMemoryEventEnum.BotStartedTalking).IsRecent(8f))
			{
				return !CurrentState.IsTalking;
			}
			return false;
		}
	}

	public bool NotTalkedForAWhile
	{
		get
		{
			if (!_interactionMemory.GetSimpleMemory(InteractionMemoryEventEnum.BotStartedTalking).IsRecent(15f))
			{
				return !CurrentState.IsTalking;
			}
			return false;
		}
	}

	private bool CanContinueCoroutine => !UiOverlay.Instance.IsOpen;

	private WaitUntil WaitUntilCanContinueCoroutine => _waitUntilInner ?? (_waitUntilInner = new WaitUntil(() => CanContinueCoroutine));

	private bool CanChangeState
	{
		get
		{
			ModelBrainState currentState = _modelStateMachine.CurrentState;
			if (currentState == null || currentState.CanBeChanged)
			{
				return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotAwake;
			}
			return false;
		}
	}

	public ModelBrainState CurrentState => _modelStateMachine.CurrentState;

	public Live2DController ConnectedController => _connectedController;

	public string RelatedControllerId => relatedControllerId;

	public bool Initialized { get; private set; }

	public bool IsTalkingWithOverlay { get; private set; }

	public void CausePleasure(float amount)
	{
		if (amount > 0f)
		{
			GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
			float num = amount * (1f + gameVariables.CurrentHorniness);
			Pleasure += num;
			gameVariables.CurrentHorniness += amount * 0.5f;
			if (gameVariables.sympathy > 50 && gameVariables.lust > 50 && gameVariables.Mood < 0.2f)
			{
				gameVariables.Mood += amount / 10f;
			}
		}
		else
		{
			Pleasure += amount;
		}
	}

	public void CausePain(float amount)
	{
		Pain += amount;
	}

	public void Awake()
	{
		_painDecay = Time.fixedDeltaTime / _painDecayInSeconds;
	}

	private void UpdatePainAndPleasure()
	{
		Pleasure -= 1E-05f;
		Pain -= _painDecay;
	}

	private void InitInteractionMemory()
	{
		_interactionMemory.ClearOnEventFired();
		InteractionMemory interactionMemory = _interactionMemory;
		interactionMemory.OnEventFired = (Action<InteractionMemoryEventEnum>)Delegate.Combine(interactionMemory.OnEventFired, new Action<InteractionMemoryEventEnum>(OnEventFired));
	}

	private void OnEventFired(InteractionMemoryEventEnum obj)
	{
		CurrentState?.OnEventFired(obj);
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.TryGetStateAs<NewFrickState>(out var state))
		{
			state.CurrentStreamManager?.ReportTagToEventReceivers(obj);
		}
	}

	private void ResetMemories()
	{
		_interactionMemory.Reset();
	}

	private void UpdateMemories()
	{
		_interactionMemory.Update();
	}

	public IEnumerator SafeCoroutineWrapper(IEnumerator enumerator)
	{
		if (enumerator == null)
		{
			yield return null;
			yield break;
		}
		if (!CanContinueCoroutine)
		{
			yield return WaitUntilCanContinueCoroutine;
		}
		while (enumerator.MoveNext())
		{
			if (!CanContinueCoroutine)
			{
				yield return WaitUntilCanContinueCoroutine;
			}
			if (enumerator.Current is IEnumerator enumerator2)
			{
				yield return SafeCoroutineWrapper(enumerator2);
			}
			else
			{
				yield return enumerator.Current;
			}
			if (!CanContinueCoroutine)
			{
				yield return WaitUntilCanContinueCoroutine;
			}
		}
	}

	public Coroutine StartSafeCoroutine(IEnumerator enumerator)
	{
		return StartCoroutine(SafeCoroutineWrapper(enumerator));
	}

	public void ChangeState(ModelBrainState newState, bool force = false)
	{
		if (CanChangeState | force)
		{
			_modelStateMachine.ChangeState(newState);
		}
	}

	public void RestoreDefaultFaceState()
	{
		ConnectedController.ToDefaultFaceAngle();
		ConnectedController.Eyes.ToDefaultEyesPosition();
		ConnectedController.Mouth.CloseMouth();
	}

	public void BrainUpdate()
	{
		GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
		if (ConnectedController is ILive2DController_OmegaMeter { ParamOmegaMeter: var paramOmegaMeter })
		{
			paramOmegaMeter.Value = (gameVariables.IsBotAwake ? Pleasure : 0f);
		}
		_modelStateMachine.Update();
	}

	public void Update()
	{
		if (!IsTalkingWithOverlay)
		{
			BrainUpdate();
		}
	}

	private void BrainFixedUpdate()
	{
		UpdatePainAndPleasure();
		_modelStateMachine.FixedUpdate();
		UpdateMemories();
	}

	public void FixedUpdate()
	{
		if (!IsTalkingWithOverlay)
		{
			BrainFixedUpdate();
		}
	}

	public static void DeleteAllBrains()
	{
		List<ModelBrain> list;
		using (MyCollectionPool<ModelBrain>.GetListAndFill(_brainById.Values, out list))
		{
			foreach (ModelBrain item in list)
			{
				item.DeleteBrain();
			}
		}
	}

	public static bool TryGet(string characterId, out ModelBrain brain)
	{
		return _brainById.TryGetValue(characterId, out brain);
	}

	public static ModelBrain CreateOrGet(string characterId)
	{
		if (TryGet(characterId, out var brain))
		{
			return brain;
		}
		brain = new GameObject("ModelBrain").AddComponent<ModelBrain>();
		brain.relatedControllerId = characterId;
		_brainById[characterId] = brain;
		return brain;
	}

	private void RemoveConnectionToCurrentController()
	{
		if (_connectedController != null && _connectedController.CurrentBrain == this)
		{
			_connectedController.CurrentBrain = null;
		}
		_connectedController = null;
	}

	public void MoveToStorage()
	{
		gameObject.SetActive(value: false);
		transform.SetParent(SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.transform);
		RemoveConnectionToCurrentController();
	}

	public void EnableBrain()
	{
		if (_connectedController == null)
		{
			Debug.LogError("Trying to enable brain but there is no connected controller.");
			return;
		}
		gameObject.SetActive(value: true);
		MakeSureInitialized();
	}

	public void SwitchToController(Live2DController live2DController)
	{
		gameObject.SetActive(value: true);
		transform.SetParent(live2DController.transform);
		ResetMemories();
		RemoveConnectionToCurrentController();
		_connectedController = live2DController;
		_connectedController.CurrentBrain = this;
	}

	public void DisableBrain()
	{
		gameObject.SetActive(value: false);
		MakeSureUninitialized();
	}

	public void DeleteBrain()
	{
		MakeSureUninitialized();
		RemoveConnectionToCurrentController();
		_brainById.Remove(relatedControllerId);
		UnityEngine.Object.Destroy(gameObject);
	}

	private void EnsureEmptyBrainState()
	{
		if (_modelStateMachine == null)
		{
			_modelStateMachine = new UpdatableStateMachine<ModelBrainState, ModelBrain>(this);
			_modelStateMachine.ChangeState(new EmptyBrainState());
		}
		else if (!(_modelStateMachine.CurrentState is EmptyBrainState))
		{
			_modelStateMachine.ChangeState(new EmptyBrainState());
		}
	}

	public void MakeSureInitialized()
	{
		if (!Initialized)
		{
			Initialized = true;
			EnsureEmptyBrainState();
			EnableConversationManager();
			InitInteractionMemory();
		}
	}

	private void OnEnable()
	{
		MakeSureInitialized();
	}

	private void MakeSureUninitialized()
	{
		if (Initialized)
		{
			EnsureEmptyBrainState();
			StopAllCoroutines();
			DisableConversationManager();
			Initialized = false;
		}
	}

	public void OnDisable()
	{
		MakeSureUninitialized();
	}

	private void EnableConversationManager()
	{
		SayDialogSignals.OnDialogFinished += TextEnd;
		SayDialogSignals.OnSayDialogGlyph += TextChange;
	}

	private void DisableConversationManager()
	{
		SayDialogSignals.OnDialogFinished -= TextEnd;
		SayDialogSignals.OnSayDialogGlyph -= TextChange;
	}

	private void TextChange(SayDialog dialog, char s)
	{
		if (dialog.SpeakingCharacter.IsBotCharacter())
		{
			_connectedController.Mouth.SayLetter(s);
		}
	}

	private void TextEnd(SayDialog sayDialog)
	{
		_connectedController.Mouth.SayEnd();
	}

	public bool ShouldBlockDialogue()
	{
		if (_connectedController.IsAnyInteractableHeld)
		{
			return true;
		}
		if (UiOverlay.Instance.IsOpen)
		{
			return true;
		}
		return false;
	}
}
