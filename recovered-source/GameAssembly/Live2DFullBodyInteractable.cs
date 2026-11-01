using System;
using System.Collections.Generic;
using System.Linq;
using Live2D.Cubism.Rendering;

public class Live2DFullBodyInteractable : Live2DInteractableDefaultBase, ILive2DInteractable
{
	public Func<List<CubismRenderer>, CubismRenderer> choose = (List<CubismRenderer> t) => t.FirstOrDefault();

	public bool ReportLimbUsageFinished(ILimbUsage limbUsage)
	{
		return false;
	}
}
