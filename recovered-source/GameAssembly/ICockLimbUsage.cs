public interface ICockLimbUsage : ILimbUsage, ILimbUsageGUI
{
	bool IsInNormalState { get; }

	bool IsInCummingState { get; }

	bool CanHideCock { get; }

	AutoTypeEnum AutoStage { get; }

	bool IfCumImminentSpeedItUp();

	void SelectNextTarget();

	bool IsInside();

	InsertionHandlerOrificeTypeEnum CurrentTargetOrificeType();

	bool TryGetActiveData(out InsertionHandler o);

	bool TryGetActiveData(out InsertionHandler o, out InsertionHandlerFuckAxis fuckAxis);

	void PullOutInstantly();

	void PullOutAuto();
}
