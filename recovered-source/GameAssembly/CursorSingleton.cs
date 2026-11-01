using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public sealed class CursorSingleton : SingletonMonoBehaviour<CursorSingleton>
{
	public enum CursorType
	{
		Null,
		NoCursor,
		Normal,
		Link,
		Click,
		Wait,
		Grab,
		Pat,
		Point,
		Spread,
		Kiss,
		Kissing,
		Soap,
		Sponge
	}

	public class CursorOverride : IDisposable
	{
		public CursorType TargetType;

		public CursorOverride(CursorType targetType)
		{
			TargetType = targetType;
		}

		public void Dispose()
		{
			SingletonMonoBehaviour<CursorSingleton>.Instance.RemoveCursorOverride(this);
		}
	}

	[SerializeField]
	private Texture2D _normalCursor;

	[SerializeField]
	private Texture2D _linkCursor;

	[SerializeField]
	private Texture2D _clickCoursor;

	[SerializeField]
	private Texture2D _waitCursor;

	[SerializeField]
	private Texture2D _grabCursor;

	[SerializeField]
	private Texture2D _patCursor;

	[SerializeField]
	private Texture2D _pointCursor;

	[SerializeField]
	private Texture2D _spreadCursor;

	[SerializeField]
	private Texture2D _kissCursor;

	[SerializeField]
	private Texture2D _kissingCursor;

	[SerializeField]
	private Texture2D _spongeCursor;

	[SerializeField]
	private Texture2D _soapCursor;

	[SerializeField]
	private Vector2 _hotSpot;

	private const int MaxOverrideCount = 64;

	private List<CursorOverride> _cursorOverrides = new List<CursorOverride>();

	public CursorType CurrentType { get; private set; } = CursorType.NoCursor;

	public Texture2D GetCursorTexture(CursorType cursorType)
	{
		return cursorType switch
		{
			CursorType.Normal => _normalCursor, 
			CursorType.Link => _linkCursor, 
			CursorType.Click => _clickCoursor, 
			CursorType.Wait => _waitCursor, 
			CursorType.Grab => _grabCursor, 
			CursorType.Pat => _patCursor, 
			CursorType.Point => _pointCursor, 
			CursorType.Spread => _spreadCursor, 
			CursorType.Kiss => _kissCursor, 
			CursorType.Kissing => _kissingCursor, 
			_ => null, 
		};
	}

	private CursorMode AutoMode()
	{
		return CursorMode.Auto;
	}

	private void SetCursor(CursorType cursorType)
	{
		if (CurrentType != cursorType)
		{
			Cursor.visible = true;
			switch (cursorType)
			{
			default:
				Cursor.SetCursor(_normalCursor, _hotSpot, AutoMode());
				break;
			case CursorType.Link:
				Cursor.SetCursor(_linkCursor, _hotSpot, AutoMode());
				break;
			case CursorType.Click:
				Cursor.SetCursor(_clickCoursor, _hotSpot, AutoMode());
				break;
			case CursorType.Wait:
				Cursor.SetCursor(_waitCursor, _hotSpot, AutoMode());
				break;
			case CursorType.Grab:
				Cursor.SetCursor(_grabCursor, _hotSpot, AutoMode());
				break;
			case CursorType.Pat:
				Cursor.SetCursor(_patCursor, _hotSpot, CursorMode.ForceSoftware);
				break;
			case CursorType.Point:
				Cursor.SetCursor(_pointCursor, _hotSpot, CursorMode.ForceSoftware);
				break;
			case CursorType.Spread:
				Cursor.SetCursor(_spreadCursor, _hotSpot, CursorMode.ForceSoftware);
				break;
			case CursorType.Kiss:
				Cursor.SetCursor(_kissCursor, new Vector2((float)_kissCursor.width / 2f, (float)_kissCursor.height / 2f), CursorMode.ForceSoftware);
				break;
			case CursorType.Kissing:
				Cursor.SetCursor(_kissingCursor, new Vector2((float)_kissingCursor.width / 2f, (float)_kissingCursor.height / 2f), CursorMode.ForceSoftware);
				break;
			case CursorType.Soap:
				Cursor.SetCursor(_soapCursor, new Vector2(14f, 14f), CursorMode.ForceSoftware);
				break;
			case CursorType.Sponge:
				Cursor.SetCursor(_spongeCursor, new Vector2((float)_spongeCursor.width / 2f, (float)_spongeCursor.height / 2f), CursorMode.ForceSoftware);
				break;
			case CursorType.NoCursor:
				Cursor.visible = false;
				Cursor.SetCursor(null, _hotSpot, CursorMode.Auto);
				break;
			}
			CurrentType = cursorType;
		}
	}

	public static void ContinueOrStartCursorOverride(ref CursorOverride cursorOverride, CursorType cursorType)
	{
		SingletonMonoBehaviour<CursorSingleton>.Instance.ContinueOrStartCursorOverride_Inner(ref cursorOverride, cursorType);
	}

	private void ContinueOrStartCursorOverride_Inner(ref CursorOverride cursorOverride, CursorType cursorType)
	{
		if (cursorType == CursorType.Null)
		{
			EndCursorOverride(ref cursorOverride);
		}
		else if (cursorOverride != null)
		{
			if (cursorOverride.TargetType != cursorType)
			{
				cursorOverride.TargetType = cursorType;
				if (_cursorOverrides.Count > 0 && _cursorOverrides[_cursorOverrides.Count - 1] == cursorOverride)
				{
					UpdateCursors();
				}
			}
		}
		else
		{
			cursorOverride = StartCursorOverride_Inner(cursorType);
		}
	}

	public static void EndCursorOverride(ref CursorOverride cursorOverride)
	{
		SingletonMonoBehaviour<CursorSingleton>.Instance.EndCursorOverride_Inner(ref cursorOverride);
	}

	private void EndCursorOverride_Inner(ref CursorOverride cursorOverride)
	{
		if (cursorOverride != null)
		{
			RemoveCursorOverride(cursorOverride);
			cursorOverride = null;
		}
	}

	[MustUseReturnValue]
	private CursorOverride StartCursorOverride_Inner(CursorType cursorType)
	{
		if (_cursorOverrides.Count >= 64)
		{
			_cursorOverrides.Clear();
			Debug.LogError("Cursor overrides overflow! Removing ALL overrides!");
		}
		CursorOverride cursorOverride = new CursorOverride(cursorType);
		_cursorOverrides.Add(cursorOverride);
		UpdateCursors();
		return cursorOverride;
	}

	private void RemoveCursorOverride(CursorOverride cursorOverride)
	{
		for (int i = 0; i < _cursorOverrides.Count; i++)
		{
			if (_cursorOverrides[i] == cursorOverride)
			{
				bool flag = i == _cursorOverrides.Count - 1;
				_cursorOverrides.RemoveAt(i);
				if (flag)
				{
					UpdateCursors();
				}
				return;
			}
		}
		Debug.LogWarning($"Trying to remove an inexistent cursor override! You most likely disposed multiple times! Target type: {cursorOverride.TargetType}");
	}

	public void ClearOverrides()
	{
		_cursorOverrides.Clear();
	}

	private void UpdateCursors()
	{
		if (_cursorOverrides.Count == 0)
		{
			SetCursor(CursorType.Normal);
		}
		else
		{
			SetCursor(_cursorOverrides[_cursorOverrides.Count - 1].TargetType);
		}
	}

	public new void Awake()
	{
		base.Awake();
		UpdateCursors();
	}
}
