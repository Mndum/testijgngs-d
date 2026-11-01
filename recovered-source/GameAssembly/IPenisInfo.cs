public interface IPenisInfo
{
	static IPenisInfo DefaultNoPenisInfo;

	static IPenisInfo AnonPenisInfo;

	static IPenisInfo AnonFingeringInfo;

	bool CausesAnonsPleasure { get; }

	PenisTypeEnum PenisType { get; }

	void GetDataAtPlane(float position, float planeAngleZ, out float leftWidth, out float rightWidth, out float width);

	static IPenisInfo()
	{
		DefaultNoPenisInfo = new NoPenisInfo();
		AnonPenisInfo = new SimplePenisInfo(causesAnonsPleasure: true, PenisTypeEnum.AnonPenis).SetSymmetricalFromTop(new CurveInterpolator(0.5f, new float[3] { 0.07f, 0.28f, 0.55f }, new float[3] { 0.4f, 0.78f, 0.88f }), new CurveInterpolator(0.25f, new float[5] { 0.072f, 0.255f, 0.4f, 0.518f, 1f }, new float[5] { 0.488f, 0.563f, 0.5f, 0.238f, 0.975f }), new CurveInterpolator(0.45f, new float[4] { 0.017f, 0.247f, 0.56f, 1f }, new float[4] { 0.387f, 0.858f, 0.99f, 0.67f }));
		AnonFingeringInfo = new SimplePenisInfo(causesAnonsPleasure: false, PenisTypeEnum.Fingers).SetSymmetricalFromSides(new CurveInterpolator(0.3f, new float[2] { 0.05f, 0.1f }, new float[2] { 0.4f, 0.8f }), new CurveInterpolator(0.2f, new float[2] { 0.05f, 0.1f }, new float[2] { 0.4f, 0.8f }), new CurveInterpolator(0.25f, new float[2] { 0.05f, 0.1f }, new float[2] { 0.4f, 0.8f }));
	}
}
