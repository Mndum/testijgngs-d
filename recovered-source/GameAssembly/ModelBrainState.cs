using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

[GenerateNonJitClassDescription]
public abstract class ModelBrainState : UpdatableStateMachineState<ModelBrainState, ModelBrain>
{
	private ManagedCoroutineWithTarget _brainCoroutine = new ManagedCoroutineWithTarget();

	private ManagedCoroutineWithTarget _reactionCoroutine = new ManagedCoroutineWithTarget();

	private bool _isReactionUnstoppable;

	private ManagedCoroutineWithTarget _parallelTalkCoroutine = new ManagedCoroutineWithTarget();

	public bool IsReacting => _reactionCoroutine.Running;

	public bool IsReactingUnstoppable
	{
		get
		{
			if (_reactionCoroutine.Running)
			{
				return _isReactionUnstoppable;
			}
			return false;
		}
	}

	public bool IsTalking => _parallelTalkCoroutine.Running;

	public Live2DControllerCommonStateFrickData FrickData => GameVariables.Current.botLive2DCommonState.FrickData;

	public bool CanBeChanged { get; protected set; } = true;

	public virtual bool IsStoryState => false;

	public ManagedCoroutine TalkingCoroutine => _parallelTalkCoroutine.InnerManagedCoroutine;

	public virtual bool IsInterested => false;

	public IEnumerator DoTalking(IEnumerator reaction, Action<ManagedCoroutine.FinishedType> onFinished = null, bool noWrapper = false)
	{
		_parallelTalkCoroutine.Start(noWrapper ? reaction : Owner.SafeCoroutineWrapper(reaction), onFinished);
		return _parallelTalkCoroutine.InnerManagedCoroutine;
	}

	public IEnumerator Say(string text, bool waitForInput = false, Action<ManagedCoroutine.FinishedType> onFinished = null, bool noWrapper = false)
	{
		return DoTalking(SingletonMonoBehaviour<FungusEnumerators>.Instance.DoConversation(text, waitForInput), onFinished, noWrapper);
	}

	protected IEnumerator SaySplit(string textToSplit, bool waitForInput = false)
	{
		string text = StringUtils.SplitStringAndPickRandom(textToSplit);
		return Say(text, waitForInput);
	}

	protected IEnumerator MoanIfPossible()
	{
		if (_parallelTalkCoroutine.NotRunning)
		{
			return SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<FrickDialogueArea>().Moan.StartDialogueFromModelBrainStateAndWait(this, isRealDialogue: false);
		}
		return null;
	}

	public IEnumerator DoReactionUnstoppable(IEnumerator reaction, Action<ManagedCoroutine.FinishedType> onFinished = null, bool noWrapper = false)
	{
		_isReactionUnstoppable = true;
		_reactionCoroutine.Start(noWrapper ? reaction : Owner.SafeCoroutineWrapper(reaction), onFinished);
		return _reactionCoroutine.InnerManagedCoroutine;
	}

	public IEnumerator DoReaction(IEnumerator reaction, Action<ManagedCoroutine.FinishedType> onFinished = null, bool noWrapper = false)
	{
		_isReactionUnstoppable = false;
		_reactionCoroutine.Start(noWrapper ? reaction : Owner.SafeCoroutineWrapper(reaction), onFinished);
		return _reactionCoroutine.InnerManagedCoroutine;
	}

	public void StopReaction()
	{
		_reactionCoroutine.Stop();
	}

	[MustUseReturnValue]
	public WaitUntil WaitUntilReactionNotRunning()
	{
		return new WaitUntil(() => _reactionCoroutine.NotRunning);
	}

	[MustUseReturnValue]
	public IEnumerator WaitUntilNotReactingAndNotTalking()
	{
		return new WaitUntil(() => _reactionCoroutine.NotRunning && _parallelTalkCoroutine.NotRunning);
	}

	[MustUseReturnValue]
	public IEnumerator WaitUntilReactionNotRunningOrTimeOut(float timeOut = 1f)
	{
		return CoroutineUtilities.WaitUntilWithTimeout(() => _reactionCoroutine.NotRunning, timeOut);
	}

	public void KillTalkingCoroutine()
	{
		_parallelTalkCoroutine.Stop();
	}

	protected abstract IEnumerator Brain();

	public virtual void OnEventFired(InteractionMemoryEventEnum type)
	{
	}

	protected void RestartBrainCoroutine()
	{
		_brainCoroutine.Start(Owner.SafeCoroutineWrapper(Brain()));
	}

	protected void InitializeCoroutines()
	{
		_brainCoroutine.SetTarget(Owner);
		_reactionCoroutine.SetTarget(Owner);
		_parallelTalkCoroutine.SetTarget(Owner);
	}

	public override void EnterState()
	{
		InitializeCoroutines();
		RestartBrainCoroutine();
	}

	public override void ExitState()
	{
		_brainCoroutine.Stop();
		_reactionCoroutine.Stop();
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
	}
}
public abstract class ModelBrainState<T> : ModelBrainState where T : Live2DController
{
	protected T controller;

	public T Controller => controller;

	public override void EnterState()
	{
		controller = (T)Owner.ConnectedController;
		base.EnterState();
	}
}
