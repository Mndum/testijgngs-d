public abstract class GameState
{
	private GameStateMachine _stateMachine;

	public GameStateMachine StateMachine => _stateMachine;

	protected void GetAndEnableStaticGui<T>(out T output) where T : StaticGuiBase
	{
		GetAndSetActiveStaticGui<T>(out output, active: true);
	}

	public void GetAndSetActiveStaticGui<T>(out T output, bool active) where T : StaticGuiBase
	{
		UiOverlay.Instance.GetStaticGUI<T>(out output);
		output.gameObject.SetActive(active);
		StateMachine.AddClaimedStaticGui(output);
	}

	public void DisableStaticGui<T>(T gui) where T : StaticGuiBase
	{
		StateMachine.RemoveClaimedStaticGui(gui);
	}

	public void DisableAllClaimedStaticGui()
	{
		StateMachine.DisableAllClaimedStaticGui();
	}

	public void InitStateMachine(GameStateMachine stateMachine)
	{
		_stateMachine = stateMachine;
	}

	public abstract void EnterState();

	public abstract void ExitState();

	public abstract void UpdateState();

	public abstract void FixedUpdateState();
}
