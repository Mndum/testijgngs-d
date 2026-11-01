using System;

[Serializable]
public class Live2DControllerCommonStateFrickData
{
	public float S_TargetCumBarValue;

	public float S_CurrentCumBarValue;

	public bool S_IsCockOut;

	public bool S_IsFlaccid;

	public bool S_IsPantsAside;

	public bool D_HasSaidStartFrickConversation;

	public BotFrickControlStatusEnum D_BotLeadStatus;

	public int D_TimesBotCame;

	public int D_TimesAnonCame;

	public int D_TimesBeggedToStop;

	public bool D_AlreadyInsertedOnce;

	public bool D_UnzippedPantsAlready;

	public bool D_HadToSlowDown;

	public bool D_HadToStop;

	public int D_EdgedForThrusts;

	public OnCumChoiceEnum D_CurrentOnCum;

	public bool D_ContinueAfterCumming = true;

	public bool D_Edge;

	public bool IsBotCompletelyInTheLead => D_BotLeadStatus == BotFrickControlStatusEnum.BotFullLead;

	public bool BotHasLead => D_BotLeadStatus >= BotFrickControlStatusEnum.BotLead;
}
