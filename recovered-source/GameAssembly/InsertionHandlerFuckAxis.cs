public abstract class InsertionHandlerFuckAxis
{
	private float _fuckingSpeedSum;

	private float _fuckingSpeedThrust;

	private float _fuckingSpeedReciprocating;

	public InsertionHandler Owner { get; private set; }

	public Live2DController Controller => Owner.Owner;

	public abstract bool IsInside { get; }

	public abstract bool IsAfterOutside { get; }

	public float FuckingSpeedSum => _fuckingSpeedSum;

	public float FuckingSpeedThrust => _fuckingSpeedThrust;

	public float FuckingSpeedReciprocating => _fuckingSpeedReciprocating;

	public void SetOwner(InsertionHandler owner)
	{
		Owner = owner;
	}

	public abstract void Start(IPenisInfo penisInfo);

	public abstract IPenisInfo GetPenisInfo();

	protected void ReportFuckingSpeedUpdate(float thrust, float repro)
	{
		float num = thrust + repro;
		_fuckingSpeedThrust = GlobalFunctions.SmoothLerpEaseOutDeltaT(_fuckingSpeedThrust, thrust * 10f, 1f / 6f);
		_fuckingSpeedReciprocating = GlobalFunctions.SmoothLerpEaseOutDeltaT(_fuckingSpeedReciprocating, repro * 10f, 1f / 6f);
		_fuckingSpeedSum = GlobalFunctions.SmoothLerpEaseOutDeltaT(_fuckingSpeedSum, num * 10f, 1f / 6f);
	}

	public abstract float GetAngleX();

	public abstract float GetAngleY();

	public abstract void Reset();

	public abstract bool CanThrustForward();

	public abstract bool CanThrustBackwards();

	public abstract void Update();

	public virtual void FixedUpdate()
	{
	}

	public abstract float GetCurrentRealInsertionPosition();

	public virtual float GetRealDistanceToInsertion()
	{
		return 0f - GetCurrentRealInsertionPosition();
	}

	public abstract float GetCurrentInsertionPosition();

	public abstract void ResetAccumulatedMovementToCurrentPosition();
}
