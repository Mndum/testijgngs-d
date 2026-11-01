using System.Collections;

public abstract class CommonLewdSceneBrainState<T> : ModelBrainState<T>, ICommonLewdSceneBrainState where T : Live2DLewdSceneController
{
	protected FrickDialogueArea CommonDialogue => SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<FrickDialogueArea>();

	public override void EnterState()
	{
		base.EnterState();
		Owner.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.CockInside);
		Owner.InteractionMemory.ActivateComplex(InteractionMemoryEnum.CockOutside);
	}

	protected IEnumerator BegStop()
	{
		yield return CommonDialogue.AnonRapeBegStop.StartDialogueFromModelBrainStateAndWait(this);
		float num = 3f;
		if (Owner.PersonalityData.BotRapeBegStopOdds.Check((float)FrickData.D_TimesBeggedToStop / num))
		{
			yield return CommonDialogue.RapeBegStopSuccess.StartDialogueFromModelBrainStateAndWait(this);
			SetBrainFrickControlStatus(BotFrickControlStatusEnum.None);
			DoStopBotLeadReaction();
		}
		else
		{
			yield return CommonDialogue.RapeBegStopRefuse.StartDialogueFromModelBrainStateAndWait(this);
			FrickData.D_TimesBeggedToStop++;
		}
	}

	public void SetBrainFrickControlStatus(BotFrickControlStatusEnum botFrickControlStatus)
	{
		FrickData.D_BotLeadStatus = botFrickControlStatus;
		FrickData.D_TimesBeggedToStop = 0;
		UiOverlay.Instance.GetStaticGUI<FrickStaticGui>(out var staticGui);
		staticGui.RightButtonList.ResetAndUpdateEverything();
	}

	public abstract void DoStopBotLeadReaction();
}
