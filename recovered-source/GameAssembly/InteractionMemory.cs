using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

public class InteractionMemory
{
	public enum InteractionMemoryDebugEnum
	{
		Active,
		ActiveOrRecent,
		Full
	}

	private SimpleEventMemory[] _allSimpleMemories;

	public Action<InteractionMemoryEventEnum> OnEventFired;

	private ComplexEventMemory[] _allComplexMemories;

	public IReadOnlyCollection<SimpleEventMemory> SimpleMemories => _allSimpleMemories;

	public IReadOnlyCollection<ComplexEventMemory> ComplexMemories => _allComplexMemories;

	public bool IsInterested
	{
		get
		{
			if (GetSimpleMemory(InteractionMemoryEventEnum.Spank).IsRecent())
			{
				return true;
			}
			List<InteractionMemoryEnum> list;
			using (MyCollectionPool<InteractionMemoryEnum>.GetList(out list))
			{
				for (int i = 0; i < _allComplexMemories.Length; i++)
				{
					if (_allComplexMemories[i].IsActiveOrRecent())
					{
						list.Add((InteractionMemoryEnum)i);
					}
				}
				list.Remove(InteractionMemoryEnum.CockOutside);
				return list.Count > 0;
			}
		}
	}

	public InteractionMemory()
	{
		_allSimpleMemories = new SimpleEventMemory[46];
		for (int i = 0; i < _allSimpleMemories.Length; i++)
		{
			_allSimpleMemories[i] = new SimpleEventMemory();
		}
		GetSimpleMemory(InteractionMemoryEventEnum.StartStream).RecentTime = 30f;
		_allComplexMemories = new ComplexEventMemory[27];
		for (int j = 0; j < _allComplexMemories.Length; j++)
		{
			_allComplexMemories[j] = new ComplexEventMemory();
		}
	}

	[MustUseReturnValue]
	public PooledList<InteractionMemoryEventEnum> GetRecentSimpleMemories(out List<InteractionMemoryEventEnum> recentSimpleMemories)
	{
		PooledList<InteractionMemoryEventEnum> list = MyCollectionPool<InteractionMemoryEventEnum>.GetList(out recentSimpleMemories);
		for (int i = 0; i < _allSimpleMemories.Length; i++)
		{
			if (_allSimpleMemories[i].IsRecent())
			{
				recentSimpleMemories.Add((InteractionMemoryEventEnum)i);
			}
		}
		return list;
	}

	public SimpleEventMemory GetSimpleMemory(InteractionMemoryEventEnum e)
	{
		return _allSimpleMemories[(int)e];
	}

	public void ClearOnEventFired()
	{
		OnEventFired = null;
	}

	public void FireEvent(InteractionMemoryEventEnum e)
	{
		GetSimpleMemory(e).FireSingle();
		switch (e)
		{
		case InteractionMemoryEventEnum.FuckingNotMoving:
		case InteractionMemoryEventEnum.FuckingSlow:
		case InteractionMemoryEventEnum.FuckingMedium:
		case InteractionMemoryEventEnum.FuckingFast:
		case InteractionMemoryEventEnum.FuckingNotMovingAnal:
		case InteractionMemoryEventEnum.FuckingSlowAnal:
		case InteractionMemoryEventEnum.FuckingMediumAnal:
		case InteractionMemoryEventEnum.FuckingFastAnal:
			ActivateComplex(InteractionMemoryEnum.Fucking, e);
			break;
		case InteractionMemoryEventEnum.ThighjobNotMoving:
		case InteractionMemoryEventEnum.ThighjobSlow:
		case InteractionMemoryEventEnum.ThighjobMedium:
		case InteractionMemoryEventEnum.ThighjobFast:
			ActivateComplex(InteractionMemoryEnum.Thighjob, e);
			break;
		case InteractionMemoryEventEnum.BlowjobSlow:
		case InteractionMemoryEventEnum.BlowjobFast:
		case InteractionMemoryEventEnum.LickingTheTip:
			ActivateComplex(InteractionMemoryEnum.Blowjob, e);
			break;
		case InteractionMemoryEventEnum.Insert:
			ActivateComplex(InteractionMemoryEnum.CockInside, e);
			DeactivateComplex(InteractionMemoryEnum.CockOutside, e);
			break;
		case InteractionMemoryEventEnum.Pullout:
			DeactivateComplex(InteractionMemoryEnum.CockInside, e);
			ActivateComplex(InteractionMemoryEnum.CockOutside, e);
			DeactivateComplex(InteractionMemoryEnum.Fucking, e);
			break;
		case InteractionMemoryEventEnum.FingeringInsert:
			ActivateComplex(InteractionMemoryEnum.Fingering, e);
			break;
		case InteractionMemoryEventEnum.FingeringSlow:
		case InteractionMemoryEventEnum.FingeringFast:
			ActivateComplex(InteractionMemoryEnum.Fingering, e);
			break;
		case InteractionMemoryEventEnum.FingeringPullout:
			DeactivateComplex(InteractionMemoryEnum.Fingering, e);
			break;
		case InteractionMemoryEventEnum.FingeringAnalInsert:
			ActivateComplex(InteractionMemoryEnum.FingeringAnus, e);
			break;
		case InteractionMemoryEventEnum.FingeringAnalSlow:
		case InteractionMemoryEventEnum.FingeringAnalFast:
			ActivateComplex(InteractionMemoryEnum.FingeringAnus, e);
			break;
		case InteractionMemoryEventEnum.FingeringAnalPullout:
			DeactivateComplex(InteractionMemoryEnum.FingeringAnus, e);
			break;
		case InteractionMemoryEventEnum.GotFlacid:
			ActivateComplex(InteractionMemoryEnum.Flacidity, e);
			break;
		case InteractionMemoryEventEnum.Scrub:
			ActivateComplex(InteractionMemoryEnum.Scrubbing, e);
			break;
		}
		OnEventFired?.Invoke(e);
	}

	public string SimpleDebugString(InteractionMemoryDebugEnum debugMode)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			for (int i = 0; i < _allSimpleMemories.Length; i++)
			{
				InteractionMemoryEventEnum interactionMemoryEventEnum = (InteractionMemoryEventEnum)i;
				SimpleEventMemory simpleEventMemory = _allSimpleMemories[i];
				switch (debugMode)
				{
				case InteractionMemoryDebugEnum.Active:
				case InteractionMemoryDebugEnum.ActiveOrRecent:
					if (!simpleEventMemory.IsRecent())
					{
						continue;
					}
					break;
				}
				stringBuilder.Append($"{interactionMemoryEventEnum}: {simpleEventMemory.Length:0.0}");
				if (simpleEventMemory.IsRecent())
				{
					stringBuilder.Append("(RECENT)");
				}
				stringBuilder.AppendLine();
			}
			if (stringBuilder.Length == 0)
			{
				return "No active simple memories!";
			}
			return stringBuilder.ToString();
		}
	}

	public ComplexEventMemory GetComplexMemory(InteractionMemoryEnum e)
	{
		return _allComplexMemories[(int)e];
	}

	public bool IsActiveComplexWithSourceEventTimeout(InteractionMemoryEnum e)
	{
		ComplexEventMemory complexMemory = GetComplexMemory(e);
		if (!complexMemory.IsActiveOrRecent())
		{
			return false;
		}
		if (complexMemory.LastRememberedActivateEvent == InteractionMemoryEventEnum.None)
		{
			return false;
		}
		return GetSimpleMemory(complexMemory.LastRememberedActivateEvent).IsRecent();
	}

	public void ActivateComplex(InteractionMemoryEnum e, InteractionMemoryEventEnum sourceEvent = InteractionMemoryEventEnum.None)
	{
		ComplexEventMemory complexMemory = GetComplexMemory(e);
		complexMemory.Activate(InteractionMemoryEventEnum.None);
		complexMemory.AdditionalData = null;
	}

	public void ActivateComplexWithData<T>(InteractionMemoryEnum e, T additionalData)
	{
		ComplexEventMemory complexMemory = GetComplexMemory(e);
		complexMemory.Activate(InteractionMemoryEventEnum.None);
		complexMemory.AdditionalData = additionalData;
	}

	public void DeactivateComplex(InteractionMemoryEnum e, InteractionMemoryEventEnum sourceEvent = InteractionMemoryEventEnum.None)
	{
		ComplexEventMemory complexMemory = GetComplexMemory(e);
		complexMemory.Deactivate(sourceEvent);
		complexMemory.AdditionalData = null;
	}

	public string ComplexDebugString(InteractionMemoryDebugEnum debugMode)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			for (int i = 0; i < _allComplexMemories.Length; i++)
			{
				InteractionMemoryEnum interactionMemoryEnum = (InteractionMemoryEnum)i;
				ComplexEventMemory complexEventMemory = _allComplexMemories[i];
				switch (debugMode)
				{
				case InteractionMemoryDebugEnum.Active:
					if (!complexEventMemory.Active)
					{
						continue;
					}
					break;
				case InteractionMemoryDebugEnum.ActiveOrRecent:
					if (!complexEventMemory.IsActiveOrRecent())
					{
						continue;
					}
					break;
				}
				stringBuilder.Append($"{interactionMemoryEnum}:");
				if (complexEventMemory.Active)
				{
					stringBuilder.Append($"Active for {complexEventMemory.Length:0.0} From:{complexEventMemory.LastRememberedActivateEvent}");
				}
				else
				{
					stringBuilder.Append($"Inactive for {complexEventMemory.Length:0.0}");
				}
				if (complexEventMemory.TryGetAdditionalDataAs<object>(out var activateData))
				{
					stringBuilder.Append(" Additional data(" + activateData.ToString() + ")");
				}
				if (complexEventMemory.IsActiveOrRecent())
				{
					stringBuilder.Append("(RECENT)");
				}
				stringBuilder.AppendLine();
			}
			if (debugMode != InteractionMemoryDebugEnum.Full && stringBuilder.Length == 0)
			{
				stringBuilder.AppendLine("No active complex memories!");
			}
			stringBuilder.AppendLine("Is interested: " + (IsInterested ? "true" : "false"));
			return stringBuilder.ToString();
		}
	}

	public void Reset()
	{
		SimpleEventMemory[] allSimpleMemories = _allSimpleMemories;
		for (int i = 0; i < allSimpleMemories.Length; i++)
		{
			allSimpleMemories[i].Reset();
		}
		ComplexEventMemory[] allComplexMemories = _allComplexMemories;
		for (int i = 0; i < allComplexMemories.Length; i++)
		{
			allComplexMemories[i].Reset();
		}
	}

	public void Update()
	{
		SimpleEventMemory[] allSimpleMemories = _allSimpleMemories;
		for (int i = 0; i < allSimpleMemories.Length; i++)
		{
			allSimpleMemories[i].Update();
		}
		ComplexEventMemory[] allComplexMemories = _allComplexMemories;
		for (int i = 0; i < allComplexMemories.Length; i++)
		{
			allComplexMemories[i].Update();
		}
	}

	public float GetStreamScore()
	{
		float num = 0f;
		for (int i = 0; i < _allSimpleMemories.Length; i++)
		{
			InteractionMemoryEventEnum tag = (InteractionMemoryEventEnum)i;
			if (_allSimpleMemories[i].IsRecent())
			{
				num += StreamEventScore(tag);
			}
		}
		for (int j = 0; j < _allComplexMemories.Length; j++)
		{
			InteractionMemoryEnum tag2 = (InteractionMemoryEnum)j;
			ComplexEventMemory complexEventMemory = _allComplexMemories[j];
			if (complexEventMemory.IsActiveOrRecent())
			{
				num += StreamMemoryScore(tag2, complexEventMemory);
			}
		}
		num /= 2f;
		return (float)Math.Tanh(num);
	}

	private float StreamMemoryScore(InteractionMemoryEnum tag, ComplexEventMemory complexEventMemory)
	{
		switch (tag)
		{
		case InteractionMemoryEnum.PussySpread:
		case InteractionMemoryEnum.AnusSpread:
			return 1f;
		case InteractionMemoryEnum.Headpat:
			return 0.5f;
		case InteractionMemoryEnum.KissMouth:
		case InteractionMemoryEnum.FaceStroke:
		case InteractionMemoryEnum.Handholding:
		case InteractionMemoryEnum.NipplePull:
		case InteractionMemoryEnum.BoobsFondle:
			return 0.3f;
		case InteractionMemoryEnum.CockInside:
			return 0.1f;
		case InteractionMemoryEnum.CockOutside:
			return 0f;
		case InteractionMemoryEnum.Blowjob:
			switch (complexEventMemory.LastRememberedActivateEvent)
			{
			case InteractionMemoryEventEnum.BlowjobSlow:
				return 1f;
			case InteractionMemoryEventEnum.LickingTheTip:
				return 2f;
			case InteractionMemoryEventEnum.BlowjobFast:
				return 3f;
			}
			break;
		case InteractionMemoryEnum.Fucking:
			switch (complexEventMemory.LastRememberedActivateEvent)
			{
			case InteractionMemoryEventEnum.FuckingSlow:
			case InteractionMemoryEventEnum.FuckingSlowAnal:
				return 1f;
			case InteractionMemoryEventEnum.FuckingMedium:
			case InteractionMemoryEventEnum.FuckingMediumAnal:
				return 2f;
			case InteractionMemoryEventEnum.FuckingFast:
			case InteractionMemoryEventEnum.FuckingFastAnal:
				return 3f;
			}
			break;
		case InteractionMemoryEnum.Flacidity:
			return -100f;
		default:
			return 0f;
		}
		return 0f;
	}

	private float StreamEventScore(InteractionMemoryEventEnum tag)
	{
		switch (tag)
		{
		case InteractionMemoryEventEnum.GotFlacid:
			return -100f;
		case InteractionMemoryEventEnum.StartStream:
		case InteractionMemoryEventEnum.StreamHourPassed:
		case InteractionMemoryEventEnum.Insert:
		case InteractionMemoryEventEnum.Pullout:
			return 0.05f;
		case InteractionMemoryEventEnum.SwallowingCumLittle:
			return 5f;
		case InteractionMemoryEventEnum.SwallowingCumALot:
			return 10f;
		case InteractionMemoryEventEnum.CumOutside:
		case InteractionMemoryEventEnum.CumInside:
		case InteractionMemoryEventEnum.CumInsideMouth:
			return 10f;
		default:
			return 0f;
		}
	}
}
