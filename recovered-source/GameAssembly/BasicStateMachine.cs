using UnityEngine;

public class BasicStateMachine<TState, TOwner> where TState : BasicStateMachineState<TState, TOwner>
{
	public string MachineName { get; private set; }

	public TOwner Owner { get; private set; }

	public TState CurrentState { get; private set; }

	public BasicStateMachine(TOwner owner)
		: this(owner, owner.GetType().Name)
	{
	}

	public BasicStateMachine(TOwner owner, string machineName)
	{
		MachineName = machineName;
		Owner = owner;
	}

	public void ChangeState(TState newState)
	{
		if (CurrentState != null)
		{
			Debug.Log(MachineName + ": Switching from " + CurrentState.GetType().Name + " to " + newState.GetType().Name + "!");
			CurrentState.ExitState();
		}
		else
		{
			Debug.Log(MachineName + ": Starting with " + newState.GetType().Name + "!");
		}
		CurrentState = newState;
		CurrentState.SetStateMachine(this);
		CurrentState.EnterState();
	}

	public void KillStateMachine()
	{
		TState currentState = CurrentState;
		CurrentState = null;
		currentState?.ExitState();
	}
}
