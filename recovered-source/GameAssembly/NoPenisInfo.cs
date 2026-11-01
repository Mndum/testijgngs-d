public class NoPenisInfo : IPenisInfo
{
	public bool CausesAnonsPleasure => false;

	public PenisTypeEnum PenisType => PenisTypeEnum.None;

	public void GetDataAtPlane(float position, float planeAngleZ, out float leftWidth, out float rightWidth, out float width)
	{
		leftWidth = (rightWidth = (width = 0f));
	}
}
