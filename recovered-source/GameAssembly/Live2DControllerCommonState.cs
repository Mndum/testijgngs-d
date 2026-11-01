using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Live2DControllerCommonState
{
	public List<CumOutsideManager.CumData> CumOutsideData = new List<CumOutsideManager.CumData>();

	[SerializeField]
	private float _cumInsideVaginaPercent;

	[SerializeField]
	private float _cumInsideWombPercent;

	public const float VaginaCumCapacity = 50f;

	public const float WombCumCapacity = 40f;

	public const float VaginaAndWombCumCapacity = 90f;

	[SerializeField]
	private float _cumInsideAnalPercent;

	public const float AnalCumCapacity = 30f;

	[SerializeField]
	private float _cumInMouthPercent;

	public const float MouthCumCapacity = 30f;

	[SerializeField]
	private float _cumInsideStomachPercent;

	public const float StomachCumCapacity = 1000f;

	[SerializeField]
	private Live2DControllerCommonStateFrickData frickData = new Live2DControllerCommonStateFrickData();

	public float CumOutsideSum => CumOutsideData.Sum((CumOutsideManager.CumData x) => x.FillPercent);

	public bool IsThereCumOnHer => CumOutsideData.Count > 0;

	public float CumInsideWomb
	{
		get
		{
			return _cumInsideWombPercent * 40f;
		}
		set
		{
			CumInsideWombPercent = value / 40f;
		}
	}

	public float CumInsideWombPercent
	{
		get
		{
			return _cumInsideWombPercent;
		}
		set
		{
			_cumInsideWombPercent = Mathf.Clamp01(value);
		}
	}

	public float CumInsideVagina
	{
		get
		{
			return _cumInsideVaginaPercent * 50f;
		}
		set
		{
			CumInsideVaginaPercent = value / 50f;
		}
	}

	public float CumInsideVaginaPercent
	{
		get
		{
			return _cumInsideVaginaPercent;
		}
		set
		{
			_cumInsideVaginaPercent = Mathf.Clamp01(value);
		}
	}

	public float CumInsideVaginaAndWomb => CumInsideVagina + CumInsideWomb;

	public float CumInsideAnal
	{
		get
		{
			return _cumInsideAnalPercent * 30f;
		}
		set
		{
			CumInsideAnalPercent = value / 30f;
		}
	}

	public float CumInsideAnalPercent
	{
		get
		{
			return _cumInsideAnalPercent;
		}
		set
		{
			_cumInsideAnalPercent = Mathf.Clamp01(value);
		}
	}

	public float CumInMouth
	{
		get
		{
			return _cumInMouthPercent * 30f;
		}
		set
		{
			CumInMouthPercent = value / 30f;
		}
	}

	public float CumInMouthPercent
	{
		get
		{
			return _cumInMouthPercent;
		}
		set
		{
			_cumInMouthPercent = Mathf.Clamp01(value);
		}
	}

	public float CumInsideStomach
	{
		get
		{
			return _cumInsideStomachPercent * 1000f;
		}
		set
		{
			CumInsideStomachPercent = value / 1000f;
		}
	}

	public float CumInsideStomachPercent
	{
		get
		{
			return _cumInsideStomachPercent;
		}
		set
		{
			_cumInsideStomachPercent = Mathf.Clamp01(value);
		}
	}

	public Live2DControllerCommonStateFrickData FrickData => frickData;

	public void ResetFrickData()
	{
		frickData = new Live2DControllerCommonStateFrickData();
	}

	public void RemoveCumOutside()
	{
		CumOutsideData.Clear();
	}

	public void MinutePassed()
	{
		float num = 0f;
		if (_cumInsideWombPercent > 0f)
		{
			float num2 = Mathf.Min(_cumInsideWombPercent, 0.00069444446f);
			CumInsideWombPercent -= num2;
			CumInsideVaginaPercent += num2;
		}
		if (_cumInsideVaginaPercent > 0f)
		{
			float num3 = Mathf.Min(_cumInsideVaginaPercent, 0.00069444446f);
			CumInsideVaginaPercent -= num3;
			num += num3;
		}
		if (_cumInsideAnalPercent > 0f)
		{
			CumInsideAnalPercent -= 0.00069444446f;
			num += 0.00069444446f;
		}
		CumInsideStomach += num;
		CumInsideStomach -= 0.022401433f;
	}
}
