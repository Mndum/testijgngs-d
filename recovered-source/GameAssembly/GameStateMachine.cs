using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
	private List<StaticGuiBase> _currentClaimedStaticGui = new List<StaticGuiBase>();

	private GameState _currentState;

	public GameState CurrentState => _currentState;

	public void AddClaimedStaticGui(StaticGuiBase _staticGuiBase)
	{
		if (!_currentClaimedStaticGui.Contains(_staticGuiBase))
		{
			_currentClaimedStaticGui.Add(_staticGuiBase);
		}
	}

	public void RemoveClaimedStaticGui(StaticGuiBase _staticGuiBase)
	{
		_staticGuiBase.gameObject.SetActive(value: false);
		_currentClaimedStaticGui.Remove(_staticGuiBase);
	}

	public void DisableAllClaimedStaticGui()
	{
		foreach (StaticGuiBase item in _currentClaimedStaticGui)
		{
			item.gameObject.SetActive(value: false);
		}
		_currentClaimedStaticGui.Clear();
		UiOverlay.Instance._speechSayDialogPopup.sayDialog.GetWriter().Stop();
	}

	public GameStateMachine(GameState currentState)
	{
		EnterState(currentState);
	}

	private void EnterState(GameState currentState)
	{
		_currentState = currentState;
		currentState.InitStateMachine(this);
		currentState.EnterState();
	}

	public void ChangeState(GameState newstate)
	{
		Debug.Log($"Changed game state to {_currentState} to {newstate}");
		_currentState.DisableAllClaimedStaticGui();
		_currentState.ExitState();
		EnterState(newstate);
	}

	public void Update()
	{
		_currentState.UpdateState();
	}

	public void FixedUpdate()
	{
		_currentState.FixedUpdateState();
	}
}
