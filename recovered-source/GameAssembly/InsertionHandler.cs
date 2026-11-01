using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public sealed class InsertionHandler : IJavalikeSubclass<Live2DController>
{
	public FuckMovementSoundHandler MovementSoundHandler = new FuckMovementSoundHandler(() => R.Sound.Frick_move_All.GetRandom().Get());

	public float EntranceOffset;

	public float EntranceMultiplier = 1f;

	private int _currentFuckAxisIndex = -1;

	private List<InsertionHandlerFuckAxis> _fuckAxisList = new List<InsertionHandlerFuckAxis>();

	private List<InsertionPlaneVisualizer> _insertionPlanes = new List<InsertionPlaneVisualizer>();

	public float ThrustMovementAccumulator;

	public float SidewaysMovementAccumulator;

	public float YMovementAccumulator;

	public Live2DController Owner { get; }

	public InsertionHandlerOrificeTypeEnum OrificeType { get; private set; }

	public List<InsertionHandlerBlock> Blockages { get; private set; } = new List<InsertionHandlerBlock>();

	public Live2DParameter EntranceSpreadParameter { get; set; }

	public Live2DParameter InsertionYDeformationParameter { get; set; }

	public InsertionHandler(Live2DController owner, InsertionHandlerOrificeTypeEnum orificeOrificeType)
	{
		Owner = owner;
		OrificeType = orificeOrificeType;
	}

	public bool IsActive()
	{
		return _currentFuckAxisIndex >= 0;
	}

	public bool TryGetActiveFuckAxis([MaybeNullWhen(false)] out InsertionHandlerFuckAxis currentAxis)
	{
		return _fuckAxisList.TryGetValue(_currentFuckAxisIndex, out currentAxis);
	}

	public void AddFuckAxis(InsertionHandlerFuckAxis axis)
	{
		axis.SetOwner(this);
		_fuckAxisList.Add(axis);
	}

	public void AddVisualizationPlane(InsertionPlaneVisualizer plane)
	{
		_insertionPlanes.Add(plane);
	}

	private void UpdateVisualization(float insertDepth)
	{
		IPenisInfo penisInfo = IPenisInfo.DefaultNoPenisInfo;
		float angleX = 0f;
		float angleY = 0f;
		if (TryGetActiveFuckAxis(out var currentAxis))
		{
			penisInfo = currentAxis.GetPenisInfo();
			angleX = currentAxis.GetAngleX();
			angleY = currentAxis.GetAngleY();
		}
		foreach (InsertionPlaneVisualizer insertionPlane in _insertionPlanes)
		{
			insertionPlane.Update(insertDepth, angleX, angleY, penisInfo);
		}
		if (EntranceSpreadParameter != null)
		{
			float position = insertDepth + EntranceOffset;
			penisInfo.GetDataAtPlane(position, 0f, out var _, out var _, out var width);
			EntranceSpreadParameter.SetRealValue(width * EntranceMultiplier);
		}
	}

	public string GetLocalizedStringName()
	{
		return OrificeType switch
		{
			InsertionHandlerOrificeTypeEnum.Anal => LOC.UI.Frick_Anal, 
			InsertionHandlerOrificeTypeEnum.Vagina => LOC.UI.Frick_Vagina, 
			InsertionHandlerOrificeTypeEnum.Thighjob => LOC.UI.Frick_ThighJob, 
			_ => LOC.UI.UI_Error, 
		};
	}

	public bool IsInside()
	{
		if (TryGetActiveFuckAxis(out var currentAxis))
		{
			return currentAxis.IsInside;
		}
		return false;
	}

	public void ResetAccumulatedMovementToCurrentPosition()
	{
		if (TryGetActiveFuckAxis(out var currentAxis))
		{
			currentAxis.ResetAccumulatedMovementToCurrentPosition();
		}
		ThrustMovementAccumulator = (YMovementAccumulator = (SidewaysMovementAccumulator = 0f));
	}

	public void Update()
	{
		float num = -1f;
		if (TryGetActiveFuckAxis(out var currentAxis))
		{
			currentAxis.Update();
			num = currentAxis.GetCurrentRealInsertionPosition();
			UpdateVisualization(num);
		}
		ThrustMovementAccumulator = (YMovementAccumulator = (SidewaysMovementAccumulator = 0f));
	}

	public void FixedUpdate()
	{
		float insertion = -1f;
		bool isInside = false;
		if (TryGetActiveFuckAxis(out var currentAxis))
		{
			currentAxis.FixedUpdate();
			insertion = currentAxis.GetCurrentRealInsertionPosition();
			isInside = currentAxis.IsInside;
		}
		MovementSoundHandler.FixedUpdate(isInside, insertion);
	}

	public void Reset()
	{
		if (TryGetActiveFuckAxis(out var currentAxis))
		{
			currentAxis.Reset();
		}
		_currentFuckAxisIndex = -1;
		UpdateVisualization(-1f);
		MovementSoundHandler.Reset();
		ThrustMovementAccumulator = (YMovementAccumulator = (SidewaysMovementAccumulator = 0f));
	}

	public void ForceCockAllTheWayInside()
	{
		ThrustMovementAccumulator = 1f;
	}

	public void ForceCockAllTheWayOutside()
	{
		ThrustMovementAccumulator = -1f;
	}

	private bool SetCurrentAxis(int index, IPenisInfo penisInfo)
	{
		if (_fuckAxisList.IsInRange(index))
		{
			if (index != _currentFuckAxisIndex)
			{
				if (TryGetActiveFuckAxis(out var currentAxis))
				{
					currentAxis.Reset();
				}
				_fuckAxisList[index].Reset();
				_fuckAxisList[index].Start(penisInfo);
				_currentFuckAxisIndex = index;
				if (Owner is ILive2DController_Manager_XRay live2DController_Manager_XRay)
				{
					live2DController_Manager_XRay.XRayManager.SyncXRay();
				}
			}
			return true;
		}
		return false;
	}

	public bool SetCurrentAxis(InsertionHandlerFuckAxis fuckAxis, IPenisInfo penisInfo)
	{
		int index = _fuckAxisList.IndexOf(fuckAxis);
		if (SetCurrentAxis(index, penisInfo))
		{
			return true;
		}
		Debug.LogError($"{fuckAxis} not found in {Owner}");
		return false;
	}

	public void EndIfCurrentAxis(int i)
	{
		if (_currentFuckAxisIndex == i)
		{
			Reset();
		}
	}

	public bool IsCurrentAxis(InsertionHandlerFuckAxis fuckAxis)
	{
		int num = _fuckAxisList.IndexOf(fuckAxis);
		if (num >= 0)
		{
			return _currentFuckAxisIndex == num;
		}
		return false;
	}

	public bool ContainsAxis(InsertionHandlerFuckAxis targetAxis)
	{
		return _fuckAxisList.Contains(targetAxis);
	}

	public void CleanUpManagedObjects()
	{
		MovementSoundHandler.Reset();
		foreach (InsertionHandlerFuckAxis fuckAxis in _fuckAxisList)
		{
			if (fuckAxis is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}
}
