public interface ILimbUsage
{
	LimbUsageChoiceEnum TargetLimbUsage { get; }

	void Start();

	void Update();

	void FixedUpdate();

	void End(LetGoLimbsCaller caller);
}
