using UnityEngine;

public class UpdatableStateMachine<TState, TOwner> where TState : UpdatableStateMachineState<TState, TOwner>
{
	public string MachineName { get; private set; }

	public TOwner Owner { get; private set; }

	public TState CurrentState { get; private set; }

	public bool DebugMode { get; set; }

	public UpdatableStateMachine(TOwner owner)
		: this(owner, owner.GetType().Name)
	{
	}

	public UpdatableStateMachine(TOwner owner, string machineName)
	{
		MachineName = machineName;
		Owner = owner;
	}

	private void Log(string s)
	{
		if (DebugMode)
		{
			Debug.LogWarning(s);
		}
		else
		{
			Debug.Log(s);
		}
	}

	public void ChangeState(TState newstate)
	{
		if (CurrentState != null)
		{
			Log($"{MachineName}: Switching from {CurrentState} to {newstate}!");
			CurrentState.ExitState();
		}
		else
		{
			Log($"{MachineName}: Starting with {newstate}!");
		}
		CurrentState = newstate;
		CurrentState.SetStateMachine(this);
		CurrentState.EnterState();
	}

	public void Update()
	{
		CurrentState?.UpdateState();
	}

	public void FixedUpdate()
	{
		CurrentState?.FixedUpdateState();
	}
}
