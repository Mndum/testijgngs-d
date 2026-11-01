using UnityEngine;

public class InsertionPlaneVisualizer
{
	public float PlaneAngleZ;

	public const float TopViewPlaneAngleZ = 0f;

	public const float RightSideViewAngleZ = 90f;

	public float XRayStartOffset;

	public float XRayDistanceMultiplier = 1f;

	public float XRaySpreadMultiplier = 1.2f;

	public int XrayDivisionCount;

	public Live2DParameter[] XRayLParameters;

	public Live2DParameter[] XRayRParameters;

	public InsertionPlaneVisualizer(float planeAngleZ, int xrayDivisionCount, Live2DParameter[] xRayLParameters, Live2DParameter[] xRayRParameters)
	{
		PlaneAngleZ = planeAngleZ;
		XrayDivisionCount = xrayDivisionCount;
		XRayLParameters = xRayLParameters;
		XRayRParameters = xRayRParameters;
	}

	private float DetermineAngle(float angleXRadians, float angleYRadians)
	{
		return Mathf.Lerp(angleXRadians, angleYRadians, PlaneAngleZ / 90f);
	}

	public void Update(float thrust, float angleX, float angleY, IPenisInfo penisInfo)
	{
		float f = DetermineAngle(angleX, angleY);
		float num = Mathf.Tan(f);
		float num2 = Mathf.Cos(f);
		float num3 = (thrust + XRayStartOffset) * XRayDistanceMultiplier;
		for (int i = 0; i < XrayDivisionCount; i++)
		{
			float num4 = (float)i / (float)XrayDivisionCount;
			float num5 = num4 * num * 2f;
			float num6 = num4 / num2;
			float position = num3 - num6;
			penisInfo.GetDataAtPlane(position, PlaneAngleZ, out var leftWidth, out var rightWidth, out var width);
			if (width > 0f)
			{
				float num7 = leftWidth + num5;
				float num8 = rightWidth - num5;
				XRayLParameters[i].Value = num7 * XRaySpreadMultiplier;
				XRayRParameters[i].Value = num8 * XRaySpreadMultiplier;
			}
			else
			{
				XRayLParameters[i].Value = 0f;
				XRayRParameters[i].Value = 0f;
			}
		}
	}
}
