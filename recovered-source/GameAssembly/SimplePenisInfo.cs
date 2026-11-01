using UnityEngine;

public class SimplePenisInfo : IPenisInfo
{
	private CurveInterpolator _upCurve;

	private CurveInterpolator _downCurve;

	private CurveInterpolator _leftCurve;

	private CurveInterpolator _rightCurve;

	public bool CausesAnonsPleasure { get; private set; }

	public PenisTypeEnum PenisType { get; private set; }

	public SimplePenisInfo(CurveInterpolator upCurve, CurveInterpolator downCurve, CurveInterpolator leftCurve, CurveInterpolator rightCurve, bool causesAnonsPleasure)
	{
		_upCurve = upCurve;
		_downCurve = downCurve;
		_leftCurve = leftCurve;
		_rightCurve = rightCurve;
		CausesAnonsPleasure = causesAnonsPleasure;
	}

	public SimplePenisInfo(bool causesAnonsPleasure, PenisTypeEnum penisTypeEnum)
	{
		CausesAnonsPleasure = causesAnonsPleasure;
		PenisType = penisTypeEnum;
		_upCurve = (_downCurve = (_leftCurve = (_rightCurve = CurveInterpolator.Default)));
	}

	public SimplePenisInfo SetSymmetricalFromTop(CurveInterpolator leftRightCurve, CurveInterpolator upCurve, CurveInterpolator downCurve)
	{
		_leftCurve = (_rightCurve = leftRightCurve);
		_upCurve = upCurve;
		_downCurve = downCurve;
		return this;
	}

	public SimplePenisInfo SetSymmetricalFromTopAndSides(CurveInterpolator leftRightCurve, CurveInterpolator upDownCurve)
	{
		_leftCurve = (_rightCurve = leftRightCurve);
		_downCurve = (_upCurve = upDownCurve);
		return this;
	}

	public SimplePenisInfo SetSymmetricalFromSides(CurveInterpolator leftCurve, CurveInterpolator rightCurve, CurveInterpolator upDownCurve)
	{
		_leftCurve = leftCurve;
		_rightCurve = rightCurve;
		_downCurve = (_upCurve = upDownCurve);
		return this;
	}

	public void GetWidthAtAngle(float position, float angleZ, out float width)
	{
		angleZ = Mathf.Repeat(angleZ, 360f);
		CurveInterpolator curveInterpolator;
		CurveInterpolator curveInterpolator2;
		float num;
		if (angleZ >= 0f && angleZ < 90f)
		{
			curveInterpolator = _downCurve;
			curveInterpolator2 = _rightCurve;
			num = angleZ / 90f;
		}
		else if (angleZ >= 90f && angleZ < 180f)
		{
			curveInterpolator = _rightCurve;
			curveInterpolator2 = _upCurve;
			num = (angleZ - 90f) / 90f;
		}
		else if (angleZ >= 180f && angleZ < 270f)
		{
			curveInterpolator = _upCurve;
			curveInterpolator2 = _leftCurve;
			num = (angleZ - 180f) / 90f;
		}
		else
		{
			curveInterpolator = _leftCurve;
			curveInterpolator2 = _downCurve;
			num = (angleZ - 270f) / 90f;
		}
		if (Mathf.Approximately(num, 0f))
		{
			width = curveInterpolator.Interpolate(position);
		}
		else if (Mathf.Approximately(num, 1f))
		{
			width = curveInterpolator2.Interpolate(position);
		}
		else
		{
			width = Mathf.LerpUnclamped(curveInterpolator.Interpolate(position), curveInterpolator2.Interpolate(position), num);
		}
	}

	public void GetDataAtPlane(float position, float planeAngleZ, out float leftWidth, out float rightWidth, out float width)
	{
		GetWidthAtAngle(position, planeAngleZ + 90f, out leftWidth);
		GetWidthAtAngle(position, planeAngleZ - 90f, out rightWidth);
		width = leftWidth + rightWidth;
	}
}
