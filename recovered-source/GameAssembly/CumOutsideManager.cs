using System;
using System.Collections.Generic;
using System.Linq;
using Live2D.Cubism.Framework.UserData;
using Live2D.Cubism.Rendering;
using UnityEngine;
using UnityEngine.Pool;

public class CumOutsideManager : Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_SerializedState, Live2DController.ILive2DControllerManager_Callback
{
	[Serializable]
	public struct CumData(string name, float fillPercent)
	{
		public string Name = name;

		public float FillPercent = fillPercent;

		public CumData Clone()
		{
			return (CumData)MemberwiseClone();
		}
	}

	public class CumLive2DRepresentation
	{
		public const string EmptyId = "";

		private string _name;

		private Live2DController.Part[] _parts;

		private string[] _partsIds;

		private float _capacity;

		private List<Live2DController.Part> required;

		private List<Live2DController.Part> blocking;

		public string Name => _name;

		public IReadOnlyList<Live2DController.Part> Parts => _parts;

		public float CumAmount { get; private set; }

		public float Capacity => _capacity;

		public float CumAmountPercent
		{
			get
			{
				return CumAmount / Capacity;
			}
			set
			{
				CumAmount = Math.Clamp(value, 0f, 1f) * Capacity;
			}
		}

		public float Left => Capacity - CumAmount;

		public bool TryGetPartFor(string id, out Live2DController.Part part)
		{
			if (id == "" && _partsIds.Length == 0)
			{
				part = _parts[0];
				return true;
			}
			for (int i = 0; i < _partsIds.Length; i++)
			{
				if (_partsIds[i] == id)
				{
					part = _parts[i];
					return true;
				}
			}
			part = null;
			return false;
		}

		public CumLive2DRepresentation(string name, Live2DController.Part[] parts, string[] partsIds)
		{
			_name = name;
			_parts = parts;
			_partsIds = partsIds;
		}

		public static CumLive2DRepresentation FromParts(Live2DController live2DController, string nameMain, List<(Live2DController.Part part, string id)> parts)
		{
			parts.Sort(((Live2DController.Part part, string id) a, (Live2DController.Part part, string id) b) => string.Compare(a.id, b.id, StringComparison.Ordinal));
			CumLive2DRepresentation cumLive2DRepresentation = new CumLive2DRepresentation(partsIds: (parts.Count != 1 || !(parts[0].id == "")) ? parts.Select(((Live2DController.Part part, string id) x) => x.id).ToArray() : Array.Empty<string>(), name: nameMain, parts: parts.Select(((Live2DController.Part part, string id) x) => x.part).ToArray());
			float num = 0f;
			bool flag = false;
			foreach (var part2 in parts)
			{
				Live2DController.Part item = part2.part;
				num += (item.Area * 200f + 0.5f) * 5f;
				if (!item.CubismDrawable.TryGetComponent<CubismUserDataTag>(out var component))
				{
					continue;
				}
				if (flag)
				{
					Debug.LogError(nameMain + ": Only one user data specifying blocking is allowed per part. Ignoring the one from " + item.FullName + "! (CumController)");
					continue;
				}
				string[] array = component.Value.Split('\n');
				cumLive2DRepresentation.required = new List<Live2DController.Part>();
				cumLive2DRepresentation.blocking = new List<Live2DController.Part>();
				string[] array2 = array;
				for (int num2 = 0; num2 < array2.Length; num2++)
				{
					string text2;
					string text = (text2 = array2[num2]);
					bool flag2 = false;
					if (text.StartsWith("!"))
					{
						text2 = text2.Substring(1);
						flag2 = true;
					}
					string[] array3 = text2.Split(',');
					foreach (string text3 in array3)
					{
						if (!live2DController.TryGetCachedPart(text3, out var part))
						{
							Debug.LogWarning("Part \"" + text3 + "\" mentioned in CubismUserData of \"" + nameMain + "\" not found! (CumController)");
						}
						else if (flag2)
						{
							cumLive2DRepresentation.blocking.Add(part);
						}
						else
						{
							cumLive2DRepresentation.required.Add(part);
						}
					}
				}
				flag = true;
			}
			if (!flag)
			{
				cumLive2DRepresentation.required = null;
				cumLive2DRepresentation.blocking = null;
			}
			cumLive2DRepresentation._capacity = num / (float)parts.Count;
			return cumLive2DRepresentation;
		}

		private bool Enabled(Live2DController.Part part)
		{
			MeshRenderer meshRenderer = part.MeshRenderer;
			if (meshRenderer.enabled)
			{
				return !meshRenderer.forceRenderingOff;
			}
			return false;
		}

		public bool CanBeEnabled()
		{
			if (blocking != null && blocking.Any(Enabled))
			{
				return false;
			}
			if (required != null && required.Any((Live2DController.Part t) => !Enabled(t)))
			{
				return false;
			}
			return true;
		}

		public void AddAndUpdate(float amount)
		{
			CumAmount += amount;
			float capacity = Capacity;
			if (CumAmount > capacity)
			{
				CumAmount = capacity;
			}
			SetAlpha(CumAmount / capacity);
		}

		public void SquirtAndUpdate(float amount)
		{
			GameVariables.Current.mlCameOutside += amount;
			AddAndUpdate(amount);
		}

		public void SetPercentAndUpdate(float percent)
		{
			CumAmountPercent = percent;
			SetAlpha(percent);
		}

		public void ClearPartiallyPercentAndUpdate(float clearPercent = 0.1f)
		{
			CumAmountPercent -= clearPercent;
			SetAlpha(CumAmountPercent);
		}

		private void SetAlpha(float percent)
		{
			float num = percent;
			if (num > 0f)
			{
				num = EasingFunction.EaseOutCubic(0f, 1f, num);
				Set(active: true, num);
			}
			else
			{
				Set(active: false, 0f);
			}
		}

		public void ClearCum()
		{
			CumAmount = 0f;
			Set(active: false, 0f);
		}

		public bool IsFull()
		{
			return CumAmount >= Capacity;
		}

		public bool IsEmpty()
		{
			return CumAmount <= 0f;
		}

		private void Set(bool active, float opacity)
		{
			for (int i = 0; i < _parts.Length; i++)
			{
				Live2DController.Part obj = _parts[i];
				obj.MeshRenderer.forceRenderingOff = !active;
				CubismRenderer cubismRenderer = obj.CubismRenderer;
				cubismRenderer.Color = cubismRenderer.Color.SetAlpha(opacity);
			}
		}

		public CumData ToCumData()
		{
			return new CumData(_name, CumAmountPercent);
		}
	}

	private Lottery<CumLive2DRepresentation> _cachedCumLottery = new Lottery<CumLive2DRepresentation>();

	public const float cumPerSplatter = 5f;

	private Live2DController _live2DController;

	private List<CumLive2DRepresentation> _representedCumSplatters = new List<CumLive2DRepresentation>();

	private List<CumData> _unrepresentedCum = new List<CumData>();

	private Dictionary<CubismRenderer, CumLive2DRepresentation> _rendererToCumSplatter;

	public IEnumerable<CumLive2DRepresentation> RepresentedCumSplatters => _representedCumSplatters;

	public bool AnySplatterNotEmpty => _representedCumSplatters.Any((CumLive2DRepresentation x) => !x.IsEmpty());

	public CumOutsideManager(Live2DController live2DController)
	{
		_live2DController = live2DController;
		List<Live2DController.Part> parts = live2DController.GetGroup(Live2DController.ColoringGroupEnum.Cum).Parts;
		Dictionary<string, List<(Live2DController.Part, string)>> value;
		using (CollectionPool<Dictionary<string, List<(Live2DController.Part, string)>>, KeyValuePair<string, List<(Live2DController.Part, string)>>>.Get(out value))
		{
			for (int i = 0; i < parts.Count; i++)
			{
				Live2DController.Part part = parts[i];
				string fullName = part.FullName;
				if (fullName.StartsWith("CumOutside", StringComparison.Ordinal))
				{
					int num = fullName.LastIndexOf("_", StringComparison.Ordinal);
					if (num >= 0)
					{
						string key = fullName.Substring(0, num);
						string item = fullName.Substring(num + 1);
						value.AddToLookup(key, (part, item));
					}
					else
					{
						value.AddToLookup(fullName, (part, ""));
					}
				}
			}
			if (Application.isEditor)
			{
				Debug.Log("CumOutside: " + value.ToStringBetter((List<(Live2DController.Part part, string id)> list2) => list2.Count.ToString()));
			}
			foreach (var (nameMain, parts2) in value)
			{
				_representedCumSplatters.Add(CumLive2DRepresentation.FromParts(live2DController, nameMain, parts2));
			}
		}
		if (_representedCumSplatters.Count == 0)
		{
			Debug.LogError("No cum outside found in " + live2DController.name);
		}
		_rendererToCumSplatter = new Dictionary<CubismRenderer, CumLive2DRepresentation>();
		for (int num2 = 0; num2 < _representedCumSplatters.Count; num2++)
		{
			CumLive2DRepresentation cumLive2DRepresentation = _representedCumSplatters[num2];
			foreach (Live2DController.Part part2 in cumLive2DRepresentation.Parts)
			{
				_rendererToCumSplatter[part2.CubismRenderer] = cumLive2DRepresentation;
			}
		}
	}

	public void ClearAllCum()
	{
		foreach (CumLive2DRepresentation representedCumSplatter in _representedCumSplatters)
		{
			representedCumSplatter.ClearCum();
		}
		_unrepresentedCum.Clear();
	}

	public (CumLive2DRepresentation container, bool isFull) GetRandom()
	{
		_cachedCumLottery.Clear();
		for (int i = 0; i < _representedCumSplatters.Count; i++)
		{
			CumLive2DRepresentation cumLive2DRepresentation = _representedCumSplatters[i];
			if (cumLive2DRepresentation.CanBeEnabled() && !cumLive2DRepresentation.IsFull())
			{
				_cachedCumLottery.AddItem(cumLive2DRepresentation, cumLive2DRepresentation.Capacity);
			}
		}
		if (_cachedCumLottery.TryGetRandom(out var result))
		{
			return (container: result, isFull: false);
		}
		_cachedCumLottery.Clear();
		for (int j = 0; j < _representedCumSplatters.Count; j++)
		{
			CumLive2DRepresentation cumLive2DRepresentation2 = _representedCumSplatters[j];
			if (cumLive2DRepresentation2.CanBeEnabled())
			{
				_cachedCumLottery.AddItem(cumLive2DRepresentation2, cumLive2DRepresentation2.Capacity);
			}
		}
		if (_cachedCumLottery.TryGetRandom(out result))
		{
			return (container: result, isFull: true);
		}
		return (container: _representedCumSplatters.GetRandomOrErr(), isFull: true);
	}

	public float GetCleanlinessScore(string id)
	{
		float num = 0f;
		float num2 = 0f;
		foreach (CumLive2DRepresentation representedCumSplatter in _representedCumSplatters)
		{
			if (representedCumSplatter.TryGetPartFor(id, out var _))
			{
				num2 += representedCumSplatter.Capacity;
				if (representedCumSplatter.CumAmountPercent > 0.3f)
				{
					num += representedCumSplatter.CumAmount;
				}
			}
		}
		return Mathf.Clamp01(1f - num / num2);
	}

	public void FillInnerThighRepresentations(List<CumLive2DRepresentation> representations)
	{
		foreach (CumLive2DRepresentation representedCumSplatter in _representedCumSplatters)
		{
			if (representedCumSplatter.Name.StartsWith("CumOutsideInnerThigh", StringComparison.Ordinal))
			{
				representations.Add(representedCumSplatter);
			}
		}
	}

	public void FillNotEmptySplatterRenderers(List<CubismRenderer> renderers)
	{
		foreach (CumLive2DRepresentation representedCumSplatter in _representedCumSplatters)
		{
			if (!representedCumSplatter.IsEmpty())
			{
				for (int i = 0; i < representedCumSplatter.Parts.Count; i++)
				{
					Live2DController.Part part = representedCumSplatter.Parts[i];
					renderers.Add(part.CubismRenderer);
				}
			}
		}
	}

	public bool TryGetRelatedCumSplatter(CubismRenderer renderer, out CumLive2DRepresentation representation)
	{
		return _rendererToCumSplatter.TryGetValue(renderer, out representation);
	}

	private void SetCum(CumData cumData)
	{
		CumLive2DRepresentation cumLive2DRepresentation = _representedCumSplatters.Find((CumLive2DRepresentation x) => x.Name == cumData.Name);
		if (cumLive2DRepresentation != null)
		{
			cumLive2DRepresentation.SetPercentAndUpdate(cumData.FillPercent);
		}
		else
		{
			_unrepresentedCum.Add(cumData);
		}
	}

	public void SaveState(Live2DControllerCommonState tmp)
	{
		List<CumData> cumOutsideData = tmp.CumOutsideData;
		cumOutsideData.Clear();
		foreach (CumLive2DRepresentation representedCumSplatter in _representedCumSplatters)
		{
			if (!representedCumSplatter.IsEmpty())
			{
				cumOutsideData.Add(representedCumSplatter.ToCumData());
			}
		}
		cumOutsideData.AddRange(_unrepresentedCum);
	}

	public void LoadState(Live2DControllerCommonState tmp)
	{
		ClearAllCum();
		foreach (CumData cumOutsideDatum in tmp.CumOutsideData)
		{
			SetCum(cumOutsideDatum);
		}
	}
}
