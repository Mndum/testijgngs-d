public abstract class BasicStateMachineState<TState, TOwner> where TState : BasicStateMachineState<TState, TOwner>
{
	public TOwner Owner => StateMachine.Owner;

	public BasicStateMachine<TState, TOwner> StateMachine { get; private set; }

	public void SetStateMachine(BasicStateMachine<TState, TOwner> stateMachine)
	{
		StateMachine = stateMachine;
	}

	public abstract void EnterState();

	public abstract void ExitState();
}
