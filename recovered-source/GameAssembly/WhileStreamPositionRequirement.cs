using System;
using Fungus;
using UnityEngine;

[Serializable]
public class WhileStreamPositionRequirement : Requirement, IStreamStartAndEndReceiver
{
	private bool wasLastSuccessful;

	[field: SerializeField]
	public ControllerEnum TargetController { get; private set; }

	public WhileStreamPositionRequirement()
	{
	}

	public WhileStreamPositionRequirement(ControllerEnum targetController)
	{
		TargetController = targetController;
	}

	public void StreamStarted(StreamManager streamManager)
	{
		SyncView(SingletonMonoBehaviour<ViewSingleton>.Instance.CurrentView);
		SingletonMonoBehaviour<ViewSingleton>.Instance.OnViewChanged += SyncView;
	}

	public void StreamFinished(StreamManager streamManager)
	{
		SingletonMonoBehaviour<ViewSingleton>.Instance.OnViewChanged -= SyncView;
	}

	public void StreamLateFinished(StreamManager streamManager)
	{
	}

	public override float GetDifficulty()
	{
		return TargetController switch
		{
			ControllerEnum.Missionary => 0.1f, 
			ControllerEnum.Doggy => 0.15f, 
			ControllerEnum.Blowjob => 0.2f, 
			ControllerEnum.Cowgirl => 0.25f, 
			_ => 0f, 
		};
	}

	public override bool CheckIfPossible()
	{
		return true;
	}

	public override bool CheckIfDone()
	{
		return wasLastSuccessful;
	}

	private void SyncView(View view)
	{
		if (view?.Scene is FrickScene frickScene && frickScene.TargetControllerEnum == TargetController)
		{
			wasLastSuccessful = true;
		}
		else
		{
			wasLastSuccessful = false;
		}
	}

	protected override string GenerateDescriptionInternal()
	{
		return LOC.UI.Requirements_Position(TargetController.GetControllerLocalizedName());
	}

	public static WhileStreamPositionRequirement CreateRandom()
	{
		GameVariables current = GameVariables.Current;
		switch (UnityEngine.Random.Range(0, 4))
		{
		default:
			return new WhileStreamPositionRequirement(ControllerEnum.Missionary);
		case 1:
			if (current.CanDoDoggy)
			{
				return new WhileStreamPositionRequirement(ControllerEnum.Doggy);
			}
			break;
		case 2:
			if (current.CanDoBlowjob)
			{
				return new WhileStreamPositionRequirement(ControllerEnum.Blowjob);
			}
			break;
		case 3:
			if (current.CanDoCowgirl)
			{
				return new WhileStreamPositionRequirement(ControllerEnum.Cowgirl);
			}
			break;
		}
		return new WhileStreamPositionRequirement(ControllerEnum.Missionary);
	}
}
