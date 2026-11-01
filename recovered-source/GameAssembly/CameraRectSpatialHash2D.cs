using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CameraRectSpatialHash2D<T> where T : MonoBehaviour
{
	private readonly int _cellCountX;

	private readonly int _cellCountY;

	private float _cellSizeX;

	private float _cellSizeY;

	private Dictionary<T, int> _dataToIndex;

	private List<T>[] _lookup;

	private bool _dirty;

	private Rect _cameraRect;

	public int Count => _dataToIndex.Count;

	public Dictionary<T, int>.KeyCollection Values => _dataToIndex.Keys;

	public CameraRectSpatialHash2D(int cellCountX, int cellCountY)
	{
		_cellCountX = Mathf.Max(1, cellCountX);
		_cellCountY = Mathf.Max(1, cellCountY);
		int num = _cellCountX * _cellCountY;
		_lookup = new List<T>[num];
		for (int i = 0; i < num; i++)
		{
			_lookup[i] = new List<T>(4);
		}
		_cellSizeX = 0f;
		_cellSizeY = 0f;
		_dataToIndex = new Dictionary<T, int>();
	}

	public T GetRandom()
	{
		if (_dataToIndex.Count == 0)
		{
			return null;
		}
		int index = UnityEngine.Random.Range(0, _dataToIndex.Count);
		return _dataToIndex.Keys.ElementAt(index);
	}

	private void UpdateAll()
	{
		RefreshCameraRect();
		List<(T, int)> list;
		using (MyCollectionPool<(T, int)>.GetList(out list))
		{
			foreach (var (val2, num2) in _dataToIndex)
			{
				int cellIndex = GetCellIndex(val2);
				if (num2 != cellIndex)
				{
					_lookup[num2].Remove(val2);
					_lookup[cellIndex].Add(val2);
					list.Add((val2, cellIndex));
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				(T, int) tuple = list[i];
				_dataToIndex[tuple.Item1] = tuple.Item2;
			}
		}
	}

	public void UpdateSingle(T key)
	{
		UpdateIfDirty();
		if (!_dataToIndex.TryGetValue(key, out var value))
		{
			Debug.LogError("Can't update a particle that's not in the spacial hash!");
			return;
		}
		int cellIndex = GetCellIndex(key);
		if (value != cellIndex)
		{
			_lookup[value].Remove(key);
			_lookup[cellIndex].Add(key);
			_dataToIndex[key] = cellIndex;
		}
	}

	public void AddSingle(T p)
	{
		UpdateIfDirty();
		int cellIndex = GetCellIndex(p);
		_lookup[cellIndex].Add(p);
		_dataToIndex.Add(p, cellIndex);
	}

	public void RemoveByPredicate(Predicate<T> predicate, bool destroyGameObject = true)
	{
		UpdateIfDirty();
		List<T> list;
		using (MyCollectionPool<T>.GetList(out list))
		{
			foreach (var (val2, num2) in _dataToIndex)
			{
				if (predicate(val2))
				{
					if (!_lookup[num2].Remove(val2))
					{
						Debug.LogError("Trying to remove a particle that's not in the spatial hash!");
					}
					list.Add(val2);
				}
			}
			foreach (T item in list)
			{
				_dataToIndex.Remove(item);
				if (destroyGameObject)
				{
					UnityEngine.Object.Destroy(item.gameObject);
				}
			}
		}
	}

	public void RemoveSingle(T p, bool destroyGameObject = true)
	{
		UpdateIfDirty();
		if (_dataToIndex.Remove(p, out var value) && !_lookup[value].Remove(p))
		{
			Debug.LogError("Trying to remove a particle that's not in the spatial hash!");
		}
		if (destroyGameObject)
		{
			UnityEngine.Object.Destroy(p.gameObject);
		}
	}

	public void Clear(bool destroyGameObject = true)
	{
		for (int i = 0; i < _lookup.Length; i++)
		{
			_lookup[i].Clear();
		}
		if (destroyGameObject)
		{
			foreach (KeyValuePair<T, int> item in _dataToIndex)
			{
				item.Deconstruct(out var key, out var _);
				UnityEngine.Object.Destroy(key.gameObject);
			}
		}
		_dataToIndex.Clear();
	}

	public void RemoveMany(List<T> list, bool destroyGameObject = true)
	{
		for (int i = 0; i < list.Count; i++)
		{
			T val = list[i];
			if (_dataToIndex.Remove(val, out var value) && !_lookup[value].Remove(val))
			{
				Debug.LogError("Trying to remove a particle that's not in the spatial hash!");
			}
		}
		if (!destroyGameObject)
		{
			return;
		}
		foreach (T item in list)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
	}

	public void SampleAt(Vector2 position, float sampleRange, List<T> found, Predicate<T> predicate)
	{
		UpdateIfDirty();
		float num = sampleRange * sampleRange;
		GetRange(position, sampleRange, out var minCellX, out var maxCellX, out var minCellY, out var maxCellY);
		for (int i = minCellY; i <= maxCellY; i++)
		{
			for (int j = minCellX; j <= maxCellX; j++)
			{
				int num2 = i * _cellCountX + j;
				List<T> list = _lookup[num2];
				int count = list.Count;
				for (int k = 0; k < count; k++)
				{
					T val = list[k];
					if (((Vector2)val.transform.position - position).sqrMagnitude <= num && predicate(val))
					{
						found.Add(val);
					}
				}
			}
		}
	}

	public void SampleAt(Vector2 position, float sampleRange, List<T> found)
	{
		UpdateIfDirty();
		float num = sampleRange * sampleRange;
		GetRange(position, sampleRange, out var minCellX, out var maxCellX, out var minCellY, out var maxCellY);
		for (int i = minCellY; i <= maxCellY; i++)
		{
			for (int j = minCellX; j <= maxCellX; j++)
			{
				int num2 = i * _cellCountX + j;
				List<T> list = _lookup[num2];
				int count = list.Count;
				for (int k = 0; k < count; k++)
				{
					T val = list[k];
					if (((Vector2)val.transform.position - position).sqrMagnitude <= num)
					{
						found.Add(val);
					}
				}
			}
		}
	}

	public void SampleAt(Rect sampleRange, List<T> found)
	{
		UpdateIfDirty();
		GetRange(sampleRange.min, sampleRange.max, out var minCellX, out var maxCellX, out var minCellY, out var maxCellY);
		for (int i = minCellY; i <= maxCellY; i++)
		{
			for (int j = minCellX; j <= maxCellX; j++)
			{
				int num = i * _cellCountX + j;
				List<T> list = _lookup[num];
				for (int k = 0; k < list.Count; k++)
				{
					T val = list[k];
					Vector2 point = val.transform.position;
					if (sampleRange.Contains(point))
					{
						found.Add(val);
					}
				}
			}
		}
	}

	public bool NoParticlesInRange(Vector2 position, float sampleRange)
	{
		T value;
		return !TrySampleFirst(position, sampleRange, out value);
	}

	public bool NoParticlesInRangeIgnoreSelf(Vector2 position, float sampleRange, T self)
	{
		T value;
		return !TrySampleFirstIgnoreSelf(position, sampleRange, self, out value);
	}

	public bool TrySampleFirst(Vector2 position, float sampleRange, out T value)
	{
		UpdateIfDirty();
		float num = sampleRange * sampleRange;
		GetRange(position, sampleRange, out var minCellX, out var maxCellX, out var minCellY, out var maxCellY);
		for (int i = minCellY; i <= maxCellY; i++)
		{
			for (int j = minCellX; j <= maxCellX; j++)
			{
				int num2 = i * _cellCountX + j;
				List<T> list = _lookup[num2];
				int count = list.Count;
				for (int k = 0; k < count; k++)
				{
					T val = list[k];
					if (((Vector2)val.transform.position - position).sqrMagnitude <= num)
					{
						value = val;
						return true;
					}
				}
			}
		}
		value = null;
		return false;
	}

	public bool TrySampleFirstIgnoreSelf(Vector2 position, float sampleRange, T self, out T value)
	{
		UpdateIfDirty();
		float num = sampleRange * sampleRange;
		GetRange(position, sampleRange, out var minCellX, out var maxCellX, out var minCellY, out var maxCellY);
		for (int i = minCellY; i <= maxCellY; i++)
		{
			for (int j = minCellX; j <= maxCellX; j++)
			{
				int num2 = i * _cellCountX + j;
				List<T> list = _lookup[num2];
				int count = list.Count;
				for (int k = 0; k < count; k++)
				{
					T val = list[k];
					if (((Vector2)val.transform.position - position).sqrMagnitude <= num && val != self)
					{
						value = val;
						return true;
					}
				}
			}
		}
		value = null;
		return false;
	}

	public void PrintDebugData()
	{
		List<int> list;
		using (MyCollectionPool<int>.GetList(out list))
		{
			list.ResizeList(5);
			int num = 0;
			List<T>[] lookup = _lookup;
			foreach (List<T> list2 in lookup)
			{
				int index = Mathf.Clamp(list2.Count, 0, 4);
				list[index]++;
				if (list2.Count > num)
				{
					num = list2.Count;
				}
			}
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				for (int j = 0; j < list.Count; j++)
				{
					stringBuilder.AppendLine($"{j}={list[j]}");
				}
				stringBuilder.AppendLine($"Max bucket: {num}");
				int num2 = _lookup.Length - list[0];
				float num3 = (float)_dataToIndex.Count / (float)num2;
				stringBuilder.AppendLine($"Average in non empty bucket: {num3:0.00}");
				stringBuilder.AppendLine($"Count: {_dataToIndex.Count}");
				Vector2Int rangeSize = GetRangeSize(0.32f);
				int num4 = rangeSize.x * rangeSize.y;
				stringBuilder.AppendLine($"32 pix range size {rangeSize} => Average checks: {(float)num4 * num3}");
				Vector2Int rangeSize2 = GetRangeSize(0.64f);
				int num5 = rangeSize2.x * rangeSize2.y;
				stringBuilder.AppendLine($"64 pix range size {rangeSize2} => Average checks: {(float)num5 * num3}");
				Debug.Log(stringBuilder.ToString());
			}
		}
	}

	private void UpdateIfDirty()
	{
		if (_dirty)
		{
			UpdateAll();
			_dirty = false;
		}
	}

	private Vector2Int GetRangeSize(float minDistance)
	{
		Vector2 vector = -Vector2.one * minDistance;
		Vector2 vector2 = Vector2.one * minDistance;
		int num = Mathf.FloorToInt((vector.x - _cameraRect.xMin) / _cellSizeX);
		int num2 = Mathf.FloorToInt((vector2.x - _cameraRect.xMin) / _cellSizeX);
		int num3 = Mathf.FloorToInt((vector.y - _cameraRect.yMin) / _cellSizeY);
		int num4 = Mathf.FloorToInt((vector2.y - _cameraRect.yMin) / _cellSizeY);
		return new Vector2Int(num2 - num + 1, num4 - num3 + 1);
	}

	private void GetRange(Vector2 position, float minDistance, out int minCellX, out int maxCellX, out int minCellY, out int maxCellY)
	{
		Vector2 vector = position - Vector2.one * minDistance;
		Vector2 vector2 = position + Vector2.one * minDistance;
		minCellX = Mathf.FloorToInt((vector.x - _cameraRect.xMin) / _cellSizeX);
		maxCellX = Mathf.FloorToInt((vector2.x - _cameraRect.xMin) / _cellSizeX);
		minCellY = Mathf.FloorToInt((vector.y - _cameraRect.yMin) / _cellSizeY);
		maxCellY = Mathf.FloorToInt((vector2.y - _cameraRect.yMin) / _cellSizeY);
		minCellX = Mathf.Clamp(minCellX, 0, _cellCountX - 1);
		maxCellX = Mathf.Clamp(maxCellX, 0, _cellCountX - 1);
		minCellY = Mathf.Clamp(minCellY, 0, _cellCountY - 1);
		maxCellY = Mathf.Clamp(maxCellY, 0, _cellCountY - 1);
	}

	private void GetRange(Vector2 worldMin, Vector2 worldMax, out int minCellX, out int maxCellX, out int minCellY, out int maxCellY)
	{
		minCellX = Mathf.FloorToInt((worldMin.x - _cameraRect.xMin) / _cellSizeX);
		maxCellX = Mathf.FloorToInt((worldMax.x - _cameraRect.xMin) / _cellSizeX);
		minCellY = Mathf.FloorToInt((worldMin.y - _cameraRect.yMin) / _cellSizeY);
		maxCellY = Mathf.FloorToInt((worldMax.y - _cameraRect.yMin) / _cellSizeY);
		minCellX = Mathf.Clamp(minCellX, 0, _cellCountX - 1);
		maxCellX = Mathf.Clamp(maxCellX, 0, _cellCountX - 1);
		minCellY = Mathf.Clamp(minCellY, 0, _cellCountY - 1);
		maxCellY = Mathf.Clamp(maxCellY, 0, _cellCountY - 1);
	}

	private void RefreshCameraRect()
	{
		_cameraRect = UiOverlay.Instance.GetCameraRect();
		float width = _cameraRect.width;
		float height = _cameraRect.height;
		_cellSizeX = width / (float)_cellCountX;
		_cellSizeY = height / (float)_cellCountY;
	}

	private int GetCellIndex(T t)
	{
		return PosToCellIndex(t.transform.position);
	}

	private int PosToCellIndex(Vector2 pos)
	{
		float num = pos.x - _cameraRect.xMin;
		float num2 = pos.y - _cameraRect.yMin;
		int num3 = Mathf.Clamp(Mathf.FloorToInt(num / _cellSizeX), 0, _cellCountX - 1);
		return Mathf.Clamp(Mathf.FloorToInt(num2 / _cellSizeY), 0, _cellCountY - 1) * _cellCountX + num3;
	}

	public void MarkDirty()
	{
		_dirty = true;
	}
}
