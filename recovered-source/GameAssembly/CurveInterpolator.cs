public class CurveInterpolator
{
	public static readonly CurveInterpolator Default = new CurveInterpolator(0.5f, new float[1] { 0.5f }, new float[1] { 0.5f });

	private readonly float outputMaxWidth;

	private readonly float[] xPoints;

	private readonly float[] yPoints;

	public CurveInterpolator(float outputMaxWidth, float[] xPoints, float[] yPoints)
	{
		this.outputMaxWidth = outputMaxWidth;
		this.xPoints = xPoints;
		this.yPoints = yPoints;
	}

	public float Interpolate(float x)
	{
		if (x <= 0f)
		{
			return 0f;
		}
		if (x >= 1f)
		{
			return outputMaxWidth;
		}
		int i;
		for (i = 0; i < xPoints.Length && xPoints[i] < x; i++)
		{
		}
		float num;
		float num2;
		float num3;
		float num4;
		if (i == 0)
		{
			num = 0f;
			num2 = 0f;
			num3 = xPoints[0];
			num4 = yPoints[0];
		}
		else if (i == xPoints.Length)
		{
			num = xPoints[xPoints.Length - 1];
			num2 = yPoints[yPoints.Length - 1];
			num3 = 1f;
			num4 = 1f;
		}
		else
		{
			num = xPoints[i - 1];
			num2 = yPoints[i - 1];
			num3 = xPoints[i];
			num4 = yPoints[i];
		}
		float num5 = (x - num) / (num3 - num);
		return (num2 + num5 * (num4 - num2)) * outputMaxWidth;
	}
}
