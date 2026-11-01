public abstract class UpdatableStateMachineState<TState, TOwner> where TState : UpdatableStateMachineState<TState, TOwner>
{
	public TOwner Owner => StateMachine.Owner;

	public UpdatableStateMachine<TState, TOwner> StateMachine { get; private set; }

	public void SetStateMachine(UpdatableStateMachine<TState, TOwner> stateMachine)
	{
		StateMachine = stateMachine;
	}

	public abstract void EnterState();

	public abstract void ExitState();

	public abstract void UpdateState();

	public abstract void FixedUpdateState();
}
