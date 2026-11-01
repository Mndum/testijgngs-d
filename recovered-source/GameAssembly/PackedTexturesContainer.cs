using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using IC.AutomaticReference;
using RectpackSharp;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "PackedTexture", menuName = "PackedTexture/New", order = 1)]
public sealed class PackedTexturesContainer : ScriptableObject, ICloneable<PackedTexturesContainer>, ICloneable
{
	[Serializable]
	public class PackedDrawable
	{
		public string Name;

		public RectInt RectInt;

		public int ColorIndex;

		public DrawableTypeEnum DrawableType;

		public bool BypassColorScaler;

		public PackedDrawable()
		{
		}

		public PackedDrawable(string name, RectInt rectInt, int colorIndex, DrawableTypeEnum drawableType, bool bypassColorScaler)
		{
			Name = name;
			RectInt = rectInt;
			ColorIndex = colorIndex;
			DrawableType = drawableType;
			BypassColorScaler = bypassColorScaler;
		}

		public PackedDrawable Clone(int offset)
		{
			PackedDrawable packedDrawable = (PackedDrawable)MemberwiseClone();
			packedDrawable.ColorIndex += offset;
			return packedDrawable;
		}

		public PackablePackedColorIndex ToPackable(PackedTexture texture)
		{
			return new PackablePackedColorIndex(texture, this);
		}
	}

	[Serializable]
	public class PackedTexturesContainerJson
	{
		public List<PackedTextureJson> PackedTextures = new List<PackedTextureJson>();
	}

	[Serializable]
	public class PackedTextureJson
	{
		[HideInInspector]
		public string TextureName;

		public int Layer;

		public List<PackedDrawable> PackedDrawables = new List<PackedDrawable>();

		public string[] Targets = Array.Empty<string>();

		public bool DontIncludeVanillaLayers;

		public bool IncludeVanillaLayers => !DontIncludeVanillaLayers;
	}

	[Serializable]
	public class PackedTexture : PackedTextureJson, ISerializationCallbackReceiver
	{
		private sealed class LayerRelationalComparer : IComparer<PackedTexture>
		{
			public int Compare(PackedTexture x, PackedTexture y)
			{
				if (x == y)
				{
					return 0;
				}
				if (y == null)
				{
					return 1;
				}
				return x?.Layer.CompareTo(y.Layer) ?? (-1);
			}
		}

		public Texture2DSerializableAutomaticReference SerializedTextureReference = new Texture2DSerializableAutomaticReference();

		public IReference<Texture2D> RuntimeTextureReference;

		public string TargetsNames
		{
			get
			{
				if (Targets.Length == 0)
				{
					return "No targets";
				}
				return string.Join(", ", Targets);
			}
		}

		public bool NoSerializedTexture => SerializedTextureReference.IsNull();

		public static IComparer<PackedTexture> LayerComparer { get; } = new LayerRelationalComparer();

		public void SortTargets()
		{
			if (Targets.Length > 1)
			{
				Array.Sort(Targets);
			}
		}

		public bool HasTarget(string target)
		{
			return Targets.Contains(target);
		}

		public void AddTarget(string target)
		{
			Targets = Targets.Append(target).ToArray();
			SortTargets();
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			SortTargets();
		}

		public Texture2D GetTexture()
		{
			if (SerializedTextureReference != null && !SerializedTextureReference.IsNull())
			{
				return SerializedTextureReference.Get();
			}
			if (RuntimeTextureReference != null && !RuntimeTextureReference.IsNull())
			{
				return RuntimeTextureReference.GetResource();
			}
			return null;
		}

		public void SetRuntimeTexture(Texture2D texture2D)
		{
			RuntimeTextureReference = new RuntimeReference<Texture2D>(texture2D);
		}

		public bool TryDrawBounds(out Texture2D boundsTexture, ModCreationLive2DControllersContainer live2DControllers)
		{
			Texture2D texture = GetTexture();
			Live2DController[] targets = Targets.Select((string t) => live2DControllers.GetController(t.GetControllerEnumFromPositionId())).ToArray();
			if (targets.Length == 0 || texture == null)
			{
				boundsTexture = null;
				return false;
			}
			RenderTexture active = RenderTexture.active;
			RenderTexture renderTexture = (RenderTexture.active = RenderTexture.GetTemporary(texture.width, texture.height));
			GL.Clear(clearDepth: true, clearColor: true, Color.clear);
			foreach (PackedDrawable packedDrawable in PackedDrawables)
			{
				if (TryGetPart(packedDrawable.Name, out var part))
				{
					Rect rect = MeshUtilitiesManager.GetRect(packedDrawable.RectInt, texture);
					SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.DrawLive2DMeshBoundsToTexture(part, rect);
				}
			}
			boundsTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, mipChain: false);
			ImageUtility.ActiveRenderTextureToTextureCpuSide(renderTexture, boundsTexture);
			RenderTexture.active = active;
			RenderTexture.ReleaseTemporary(renderTexture);
			return true;
			bool TryGetPart(string partName, out Live2DController.Part reference)
			{
				reference = null;
				Live2DController[] array = targets;
				foreach (Live2DController live2DController in array)
				{
					reference = live2DController.GetCachedPart(partName);
					if (reference != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		public PackedTexture Clone(int offset)
		{
			PackedTexture packedTexture = new PackedTexture();
			packedTexture.Layer = Layer + offset;
			packedTexture.PackedDrawables = PackedDrawables.Select((PackedDrawable t) => t.Clone(offset)).ToList();
			packedTexture.Targets = Targets.ToArray();
			packedTexture.SetRuntimeTexture(GetTexture());
			return packedTexture;
		}
	}

	public interface IPackable
	{
		string Pack_Name { get; }

		RectInt Pack_RectInt { get; }

		int Pack_ColorIndex { get; }

		DrawableTypeEnum Pack_DrawableType { get; }

		bool Pack_BypassColorScaler { get; }

		void Draw(Rect targetRectInTexture);
	}

	public interface IPackableExpanded : IPackable
	{
		int Pack_Layer { get; }

		string[] Pack_Targets { get; }
	}

	public class PackablePackedColorIndex : IPackableExpanded, IPackable
	{
		public PackedTexture Texture;

		public PackedDrawable Drawable;

		public string Pack_Name => Drawable.Name;

		public RectInt Pack_RectInt => Drawable.RectInt;

		public string[] Pack_Targets => Texture.Targets;

		public int Pack_ColorIndex => Drawable.ColorIndex;

		public DrawableTypeEnum Pack_DrawableType => Drawable.DrawableType;

		public bool Pack_BypassColorScaler => Drawable.BypassColorScaler;

		public int Pack_Layer => Texture.Layer;

		public PackablePackedColorIndex(PackedTexture texture, PackedDrawable drawable)
		{
			Texture = texture;
			Drawable = drawable;
		}

		public void Draw(Rect targetRectInTexture)
		{
			SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.DrawPackedIgnorePreserveAspectRatio(Texture, Drawable, targetRectInTexture, Color.white, blend: false);
		}
	}

	private class DrawableApprovedGrouping : IPackableExpanded, IPackable
	{
		public List<PackablePackedColorIndex> Drawables;

		public string[] Baked_Targets;

		public PackablePackedColorIndex Key => Drawables.First();

		public string Pack_Name => Key.Pack_Name;

		public RectInt Pack_RectInt => Key.Pack_RectInt;

		public int Pack_Layer => Key.Pack_Layer;

		public string[] Pack_Targets => Baked_Targets;

		public int Pack_ColorIndex => Key.Pack_ColorIndex;

		public DrawableTypeEnum Pack_DrawableType => Key.Pack_DrawableType;

		public bool Pack_BypassColorScaler => Key.Pack_BypassColorScaler;

		public DrawableApprovedGrouping(PackablePackedColorIndex first)
		{
			Drawables = new List<PackablePackedColorIndex> { first };
		}

		public bool Fits(PackablePackedColorIndex element)
		{
			Texture2D texture = Key.Texture.GetTexture();
			RectInt pack_RectInt = Key.Pack_RectInt;
			Texture2D texture2 = element.Texture.GetTexture();
			RectInt pack_RectInt2 = element.Pack_RectInt;
			return SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.Compare(texture, pack_RectInt, texture2, pack_RectInt2) < 0.05f;
		}

		public void BakeTargets()
		{
			Baked_Targets = (from t in Drawables.SelectMany((PackablePackedColorIndex t) => t.Pack_Targets)
				orderby t
				select t).ToArray();
		}

		public void Draw(Rect targetRectInTexture)
		{
			Key.Draw(targetRectInTexture);
		}
	}

	private class OptimizationBasicComparer : IEqualityComparer<PackablePackedColorIndex>
	{
		private float Error(int a, int b)
		{
			if (a > b)
			{
				return (float)(a - b) / (float)b - 1f;
			}
			return (float)(b - a) / (float)a - 1f;
		}

		public bool Equals(PackablePackedColorIndex t1, PackablePackedColorIndex t2)
		{
			if (t1.Texture.Layer != t2.Texture.Layer)
			{
				return false;
			}
			PackedDrawable drawable = t1.Drawable;
			PackedDrawable drawable2 = t2.Drawable;
			if (Error(drawable.RectInt.width, drawable2.RectInt.width) > 0.01f)
			{
				return false;
			}
			if (Error(drawable.RectInt.height, drawable2.RectInt.height) > 0.01f)
			{
				return false;
			}
			if (drawable.ColorIndex != drawable2.ColorIndex)
			{
				return false;
			}
			if (drawable.DrawableType != drawable2.DrawableType)
			{
				return false;
			}
			if (drawable.BypassColorScaler != drawable2.BypassColorScaler)
			{
				return false;
			}
			return string.Equals(drawable.Name, drawable2.Name, StringComparison.Ordinal);
		}

		public int GetHashCode(PackablePackedColorIndex t)
		{
			PackedDrawable drawable = t.Drawable;
			return (t.Texture.Layer ^ drawable.ColorIndex ^ drawable.Name.GetHashCode()).GetHashCode();
		}
	}

	public class TargetComparer : IEqualityComparer<string[]>
	{
		public bool Equals(string[] a, string[] b)
		{
			if (a == null && b == null)
			{
				return true;
			}
			if (a == null)
			{
				return false;
			}
			if (b == null)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (!string.Equals(a[i], b[i], StringComparison.Ordinal))
				{
					return false;
				}
			}
			return true;
		}

		public int GetHashCode(string[] t)
		{
			return t.Length;
		}
	}

	public List<PackedTexture> PackedTextures = new List<PackedTexture>();

	private const int DefaultPadding = 5;

	public IEnumerable<PackedDrawable> PackedDrawables => PackedTextures.SelectMany((PackedTexture a) => a.PackedDrawables);

	public IEnumerable<(PackedTexture packedTexture, PackedDrawable packedDrawable)> PackedDrawablesWithTheirTextures => PackedTextures.SelectMany((PackedTexture a) => a.PackedDrawables.Select((PackedDrawable b) => (a: a, b: b)));

	public IOrderedEnumerable<IGrouping<int, PackedTexture>> TexturesInLayers => from t in PackedTextures.ToLookup((PackedTexture t) => t.Layer)
		orderby t.Key
		select t;

	public IEnumerable<PackedDrawable> GetPackedDrawablesByTarget(ControllerEnum target)
	{
		string controllerId = target.GetControllerId();
		return GetPackedDrawablesByTarget(controllerId);
	}

	public IEnumerable<PackedDrawable> GetPackedDrawablesByTarget(string id)
	{
		return PackedTextures.Where((PackedTexture t) => t.HasTarget(id)).SelectMany((PackedTexture t) => t.PackedDrawables);
	}

	public IEnumerable<(PackedTexture packedTexture, PackedDrawable packedDrawable)> GetRelated(string target, string rendererName)
	{
		foreach (PackedTexture packedTexture in PackedTextures.OrderBy((PackedTexture t) => t.Layer))
		{
			if (!packedTexture.HasTarget(target))
			{
				continue;
			}
			foreach (PackedDrawable item in packedTexture.PackedDrawables.Where((PackedDrawable t) => t.Name == rendererName))
			{
				if (item.Name == rendererName)
				{
					yield return (packedTexture: packedTexture, packedDrawable: item);
				}
			}
		}
	}

	public bool HasDrawable(string target, string name)
	{
		return PackedTextures.Where((PackedTexture t) => t.HasTarget(target)).SelectMany((PackedTexture t) => t.PackedDrawables).Any((PackedDrawable a) => a.Name == name);
	}

	public string ToJson()
	{
		return JsonUtility.ToJson(new PackedTexturesContainerJson
		{
			PackedTextures = ((IEnumerable<PackedTexture>)PackedTextures).Select((Func<PackedTexture, PackedTextureJson>)((PackedTexture t) => t)).ToList()
		}, prettyPrint: true);
	}

	public List<ControllerEnum> GetAffectedControllers()
	{
		HashSet<string> hashSet;
		using (MyCollectionPool<string>.GetHashSet(out hashSet))
		{
			foreach (PackedTexture packedTexture in PackedTextures)
			{
				string[] targets = packedTexture.Targets;
				foreach (string item in targets)
				{
					hashSet.Add(item);
				}
			}
			return hashSet.Select((string x) => x.GetControllerEnumFromPositionId()).ToListPooledNoUsing();
		}
	}

	public string GetNameUsingAffectedControllersId()
	{
		List<ControllerEnum> affectedControllers = GetAffectedControllers();
		if (affectedControllers.Count > 0)
		{
			return string.Join(", ", affectedControllers.Select((ControllerEnum t) => t.GetControllerId()));
		}
		return "No targets!";
	}

	public string GetLuaNameUsingAffectedControllersId()
	{
		List<ControllerEnum> affectedControllers = GetAffectedControllers();
		if (affectedControllers.Count > 0)
		{
			return string.Join("_", affectedControllers.Select((ControllerEnum t) => t.GetControllerId()));
		}
		return "Targetless";
	}

	public bool AreVanillaLayersIncluded(string controller)
	{
		foreach (PackedTexture packedTexture in PackedTextures)
		{
			if (packedTexture.HasTarget(controller) && packedTexture.IncludeVanillaLayers)
			{
				return true;
			}
		}
		return false;
	}

	public string TextureNameGenerator(string name, int index, int layer)
	{
		return $"{name}_texture{index}_layer{layer}.png";
	}

	public IEnumerable<ModFileBase> SaveTexturesToModfiles(string folder, string name)
	{
		foreach (IGrouping<int, PackedTexture> texturesInLayer in TexturesInLayers)
		{
			int layer = texturesInLayer.Key;
			foreach (var item in texturesInLayer.Select((PackedTexture packedTexture, int index) => (packedTexture: packedTexture, index: index)))
			{
				string text = (item.packedTexture.TextureName = TextureNameGenerator(name, item.index, layer));
				if (item.packedTexture.RuntimeTextureReference is Texture2DModFileAutomaticReference texture2DModFileAutomaticReference)
				{
					yield return texture2DModFileAutomaticReference.ModFileSource.CopyToMemory(folder + text);
				}
				else
				{
					yield return new ModFileInMemoryTexture(folder + text, item.packedTexture.GetTexture());
				}
			}
		}
	}

	public IEnumerable<ModFileBase> SaveToModfiles(string path)
	{
		string folder = Path.GetDirectoryName(path) + "/";
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
		foreach (ModFileBase item in SaveTexturesToModfiles(folder, fileNameWithoutExtension))
		{
			yield return item;
		}
		yield return new ModFileInMemory(path, ToJson());
	}

	public void SaveToPath(string path)
	{
		foreach (ModFileBase item in SaveToModfiles(path))
		{
			File.WriteAllBytes(item.NormalizedPath, item.Data);
		}
	}

	public static PackedTexturesContainer LoadFromModFiles(string formatPath, ReadOnlyCollection<ModFileBase> modfiles)
	{
		string text = (Path.GetDirectoryName(formatPath) + "/").Replace('\\', '/');
		ModFileBase modFileBase = Find(formatPath);
		PackedTexturesContainer packedTexturesContainer = CreateEmpty();
		JsonUtility.FromJsonOverwrite(modFileBase.StringData, packedTexturesContainer);
		foreach (PackedTexture packedTexture in packedTexturesContainer.PackedTextures)
		{
			ModFileBase modFileSource = Find(text + packedTexture.TextureName);
			packedTexture.RuntimeTextureReference = new Texture2DModFileAutomaticReference(modFileSource);
		}
		return packedTexturesContainer;
		ModFileBase Find(string name)
		{
			ModFileBase modFileBase2 = modfiles.FirstOrDefault((ModFileBase t) => t.NormalizedPath == name);
			if (modFileBase2 != null)
			{
				return modFileBase2;
			}
			throw new FileNotFoundException("File: \"" + name + "\" Files: " + string.Join(", ", modfiles.Select((ModFileBase t) => "\"" + t.NormalizedPath + "\"")));
		}
	}

	public static PackedTexturesContainer LoadFromPath(string path)
	{
		string text = Path.GetDirectoryName(path) + "/";
		string json = File.ReadAllText(path);
		PackedTexturesContainer packedTexturesContainer = CreateEmpty();
		JsonUtility.FromJsonOverwrite(json, packedTexturesContainer);
		foreach (PackedTexture packedTexture in packedTexturesContainer.PackedTextures)
		{
			packedTexture.SetRuntimeTexture(ImageUtility.LoadTexture(text + packedTexture.TextureName));
		}
		return packedTexturesContainer;
	}

	private PackedTexturesContainer()
	{
	}

	public static PackedTexturesContainer CreateEmpty()
	{
		return ScriptableObject.CreateInstance<PackedTexturesContainer>();
	}

	private static PackedTexture CreatePackedTexture(IEnumerable<IPackable> packedDrawables, string[] targets, int layer = 0, int padding = 5)
	{
		List<IPackable> list = packedDrawables.OrderBy((IPackable t) => t.Pack_Name).ToList();
		PackingRectangle[] array = list.Select((IPackable x, int index) => new PackingRectangle(0u, 0u, (uint)(x.Pack_RectInt.width + padding * 2), (uint)(x.Pack_RectInt.height + padding * 2), index)).ToArray();
		RectanglePacker.Pack(array, out var bounds);
		(int x, int y) tuple = Round((int)bounds.Width, (int)bounds.Height);
		int item = tuple.x;
		int item2 = tuple.y;
		RenderTexture temporary = RenderTexture.GetTemporary(item, item2, 0);
		RenderTexture active = RenderTexture.active;
		PackedTexture packedTexture = new PackedTexture
		{
			Layer = layer
		};
		Texture2D texture2D = new Texture2D(item, item2, TextureFormat.RGBA32, mipChain: false);
		RenderTexture.active = temporary;
		GL.Clear(clearDepth: true, clearColor: true, Color.clear);
		PackingRectangle[] array2 = array;
		for (int num = 0; num < array2.Length; num++)
		{
			PackingRectangle packingRectangle = array2[num];
			IPackable packable = list[packingRectangle.Id];
			RectInt rectInt = new RectInt((int)packingRectangle.X + padding, (int)packingRectangle.Y + padding, (int)packingRectangle.Width - padding * 2, (int)packingRectangle.Height - padding * 2);
			Rect rect = MeshUtilitiesManager.GetRect(rectInt, texture2D);
			packable.Draw(rect);
			packedTexture.PackedDrawables.Add(new PackedDrawable(packable.Pack_Name, rectInt, packable.Pack_ColorIndex, packable.Pack_DrawableType, packable.Pack_BypassColorScaler));
		}
		ImageUtility.ActiveRenderTextureToTextureGpuSide(temporary, texture2D);
		packedTexture.SetRuntimeTexture(texture2D);
		packedTexture.Targets = targets;
		RenderTexture.active = active;
		RenderTexture.ReleaseTemporary(temporary);
		return packedTexture;
		static int NextPowerOf(int x)
		{
			int num2;
			for (num2 = 1; num2 < x; num2 *= 2)
			{
			}
			return num2;
		}
		static (int x, int y) Round(int x, int y)
		{
			int num2 = RoundUp(x, 4);
			int num3 = RoundUp(y, 4);
			int num4 = NextPowerOf(num2);
			int num5 = NextPowerOf(num3);
			if (num4 * num5 - num2 * num3 < 4096)
			{
				return (x: num4, y: num5);
			}
			return (x: num2, y: num3);
		}
		static int RoundUp(int toRound, int num3)
		{
			int num2 = toRound % num3;
			if (num2 == 0)
			{
				return toRound;
			}
			return num3 - num2 + toRound;
		}
	}

	public void CreatePackedTexturesFromRenderers(List<Live2DController.Part> parts, string[] targets, int layer = 0)
	{
		PackedTexture item = CreatePackedTexture(parts, targets, layer);
		PackedTextures.Add(item);
		Sort();
	}

	public PackedTexturesContainer Clone()
	{
		PackedTexturesContainer packedTexturesContainer = CreateEmpty();
		packedTexturesContainer.PackedTextures = PackedTextures.Select((PackedTexture t) => t.Clone(0)).ToList();
		return packedTexturesContainer;
	}

	public void Sort()
	{
		PackedTextures.Sort((PackedTexture a, PackedTexture b) =>
		{
			int num = a.Layer.CompareTo(b.Layer);
			if (num != 0)
			{
				return num;
			}
			int num2 = a.PackedDrawables.Count.CompareTo(b.PackedDrawables.Count);
			if (num2 != 0)
			{
				return num2;
			}
			return (a.TextureName != null && b.TextureName != null) ? a.TextureName.CompareTo(b) : 0;
		});
	}

	public void DuplicateTexture(PackedTexture packedTexture, int offset = 1)
	{
		PackedTexture item = packedTexture.Clone(offset);
		PackedTextures.Add(item);
		Sort();
	}

	public void DuplicateLayer(int layer, int offset = 1)
	{
		foreach (PackedTexture item in PackedTextures.Where((PackedTexture t) => t.Layer == layer).ToList())
		{
			DuplicateTexture(item, offset);
		}
		Sort();
	}

	public void JoinLayer(int layer, string[] packedTextureTargets)
	{
		TargetComparer targetsCompare = new TargetComparer();
		IEnumerable<PackablePackedColorIndex> packedDrawables = PackedTextures.Where((PackedTexture t) => t.Layer == layer && targetsCompare.Equals(t.Targets, packedTextureTargets)).ToList().SelectMany((PackedTexture a) => a.PackedDrawables.Select((PackedDrawable b) => b.ToPackable(a)));
		PackedTextures.RemoveAll((PackedTexture t) => t.Layer == layer && targetsCompare.Equals(t.Targets, packedTextureTargets));
		PackedTexture item = CreatePackedTexture(packedDrawables, packedTextureTargets, layer);
		PackedTextures.Add(item);
		Sort();
	}

	public void SplitTexture(List<PackedDrawable> packedDrawables, PackedTexture packedTexture)
	{
		List<PackedDrawable> source = packedTexture.PackedDrawables.Except(packedDrawables).ToList();
		string[] targets = packedTexture.Targets;
		PackedTexture value = CreatePackedTexture(source.Select((PackedDrawable t) => t.ToPackable(packedTexture)), targets.ToArray());
		int index = PackedTextures.IndexOf(packedTexture);
		PackedTextures[index] = value;
		PackedTexture item = CreatePackedTexture(packedDrawables.Select((PackedDrawable t) => t.ToPackable(packedTexture)), targets.ToArray());
		PackedTextures.Add(item);
		Sort();
	}

	public void AddAllTexturesTarget(string target)
	{
		foreach (PackedTexture packedTexture in PackedTextures)
		{
			if (!packedTexture.Targets.Contains(target))
			{
				packedTexture.Targets = packedTexture.Targets.Append(target).ToArray();
				packedTexture.SortTargets();
			}
		}
	}

	public static Result Optimize(ref PackedTexturesContainer[] packedTexturesContainers)
	{
		List<PackablePackedColorIndex> source = packedTexturesContainers.SelectMany((PackedTexturesContainer x) => x.PackedTextures).SelectMany((PackedTexture x) => x.PackedDrawables.Select((PackedDrawable y) => y.ToPackable(x))).ToList();
		List<PackedTexturesContainer> list = new List<PackedTexturesContainer>();
		List<IGrouping<PackablePackedColorIndex, PackablePackedColorIndex>> list2 = source.GroupBy((PackablePackedColorIndex x) => x, new OptimizationBasicComparer()).ToList();
		List<PackablePackedColorIndex> list3 = new List<PackablePackedColorIndex>();
		List<DrawableApprovedGrouping> list4 = new List<DrawableApprovedGrouping>();
		List<DrawableApprovedGrouping> list5 = new List<DrawableApprovedGrouping>();
		foreach (IGrouping<PackablePackedColorIndex, PackablePackedColorIndex> item3 in list2)
		{
			List<PackablePackedColorIndex> list6 = item3.ToList();
			if (list6.Count <= 1)
			{
				list3.Add(list6[0]);
				continue;
			}
			list5.Clear();
			list5.Add(new DrawableApprovedGrouping(list6[0]));
			for (int num = 1; num < list6.Count; num++)
			{
				bool flag = false;
				PackablePackedColorIndex packablePackedColorIndex = list6[num];
				foreach (DrawableApprovedGrouping item4 in list5)
				{
					if (item4.Fits(packablePackedColorIndex))
					{
						item4.Drawables.Add(packablePackedColorIndex);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list5.Add(new DrawableApprovedGrouping(packablePackedColorIndex));
				}
			}
			foreach (DrawableApprovedGrouping item5 in list5)
			{
				if (item5.Drawables.Count <= 1)
				{
					list3.Add(item5.Key);
				}
				else
				{
					list4.Add(item5);
				}
			}
		}
		if (list4.Count == 0)
		{
			return Result.FailWithDescription("No targets for optimization found! Texture is already fully optimized.");
		}
		foreach (DrawableApprovedGrouping item6 in list4)
		{
			item6.BakeTargets();
		}
		IEnumerable<IPackableExpanded> source2 = ((IEnumerable<IPackableExpanded>)list3).Concat((IEnumerable<IPackableExpanded>)list4);
		(int, int, int, int) tuple = CountStats(packedTexturesContainers);
		foreach (IGrouping<string[], IPackableExpanded> item7 in from t in source2.ToLookup((IPackableExpanded t) => t.Pack_Targets, new TargetComparer())
			orderby t.Key.Length descending
			select t)
		{
			string[] key = item7.Key;
			PackedTexturesContainer packedTexturesContainer = CreateEmpty();
			foreach (IGrouping<int, IPackableExpanded> item8 in from t in item7.ToLookup((IPackableExpanded t) => t.Pack_Layer)
				orderby t.Key
				select t)
			{
				int key2 = item8.Key;
				List<IPackableExpanded> list7 = item8.ToList();
				float num2 = 0f;
				int num3 = 0;
				for (int num4 = 0; num4 < list7.Count; num4++)
				{
					float num5 = (float)AreaWithPadding(list7[num4]) * 1.2f;
					num2 += num5;
					if (num2 > 4194304f)
					{
						PackedTexture item = CreatePackedTexture(list7.GetRange(num3, num4 - num3 + 1), key.ToArray(), key2);
						packedTexturesContainer.PackedTextures.Add(item);
						num3 = num4 + 1;
						num2 = 0f;
					}
				}
				if (num3 != list7.Count)
				{
					PackedTexture item2 = CreatePackedTexture(list7.GetRange(num3, list7.Count - 1 - num3 + 1), key.ToArray(), key2);
					packedTexturesContainer.PackedTextures.Add(item2);
				}
			}
			packedTexturesContainer.Sort();
			list.Add(packedTexturesContainer);
		}
		packedTexturesContainers = list.ToArray();
		(int, int, int, int) tuple2 = CountStats(packedTexturesContainers);
		return Result.SuccessWithDescription("Before:\n" + $"Packed Texture Containers: {tuple.Item1}\n" + $"Packed Textures: {tuple.Item2}\n" + $"Packed Drawables: {tuple.Item3}\n" + "Uncompressed Texture Data: " + GlobalFunctions.FormatBytes(tuple.Item4) + "\n\nAfter:\n" + $"Packed Texture Containers: {tuple2.Item1}\n" + $"Packed Textures: {tuple2.Item2}\n" + $"Packed Drawables: {tuple2.Item3}\n" + "Uncompressed Texture Data: " + GlobalFunctions.FormatBytes(tuple2.Item4) + "\n");
		static int AreaWithPadding(IPackableExpanded packableExpanded)
		{
			RectInt pack_RectInt = packableExpanded.Pack_RectInt;
			return (pack_RectInt.width + 10) * (pack_RectInt.height + 10);
		}
		static (int packedTexturesContainers, int packedTextures, int packedDrawables, int uncompressedTextureDataInBytes) CountStats(PackedTexturesContainer[] array)
		{
			return (packedTexturesContainers: array.Length, packedTextures: array.Sum((PackedTexturesContainer t) => t.PackedTextures.Count), packedDrawables: array.Sum((PackedTexturesContainer t) => t.PackedTextures.Sum((PackedTexture x) => x.PackedDrawables.Count)), uncompressedTextureDataInBytes: array.Sum((PackedTexturesContainer t) => t.PackedTextures.Sum((PackedTexture x) =>
			{
				Texture2D texture = x.GetTexture();
				return texture.width * texture.height * 4;
			})));
		}
	}

	public Result CheckValidity()
	{
		if (PackedTextures.Count == 0)
		{
			return Result.FailWithDescription("No textures!");
		}
		bool value = true;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			foreach (IGrouping<int, PackedTexture> texturesInLayer in TexturesInLayers)
			{
				int key = texturesInLayer.Key;
				List<PackedTexture> list = texturesInLayer.ToList();
				for (int i = 0; i < list.Count - 1; i++)
				{
					PackedTexture packedTexture = list[i];
					for (int j = i + 1; j < list.Count; j++)
					{
						PackedTexture packedTexture2 = list[j];
						foreach (var item2 in from a in packedTexture.PackedDrawables
							join b in packedTexture2.PackedDrawables on a.Name equals b.Name
							select (a: a, b: b))
						{
							PackedDrawable item = item2.a;
							stringBuilder.AppendLine($"Duplicate drawable {item.Name} found in layer {key}.");
							value = false;
						}
					}
				}
			}
			foreach (PackedTexture packedTexture3 in PackedTextures)
			{
				if (packedTexture3.GetTexture() == null)
				{
					stringBuilder.AppendLine(packedTexture3.TextureName + ": Texture is null");
					value = false;
				}
				if (packedTexture3.PackedDrawables.Count == 0)
				{
					stringBuilder.AppendLine(packedTexture3.TextureName + ": No drawables");
					value = false;
				}
			}
			return Result.CreateFromBool(value, stringBuilder.ToString());
		}
	}

	public void ReloadTexturesFromFiles(StringBuilder log)
	{
		for (int i = 0; i < PackedTextures.Count; i++)
		{
			if (PackedTextures[i].RuntimeTextureReference is Texture2DModFileAutomaticReference { ModFileSource: var modFileSource } texture2DModFileAutomaticReference)
			{
				if (PackedTextures.Count > 0)
				{
					log?.AppendLine($"Texture index {i}");
					log?.AppendLine();
				}
				if (modFileSource is WorkingDirectoryModFileOnHardDrive workingDirectoryModFileOnHardDrive)
				{
					if (workingDirectoryModFileOnHardDrive.Exists())
					{
						log?.AppendLine("File found at path \"" + workingDirectoryModFileOnHardDrive.HardDrivePath + "\"!");
						AutomaticReference<Texture2D, ModFileBase>.RemoveCacheEntry(modFileSource);
						log?.AppendLine("Cache cleared.");
						try
						{
							texture2DModFileAutomaticReference.Get();
							log?.AppendLine("Reference reloaded");
						}
						catch (IOException arg)
						{
							log?.TMPColorSurroundStringBuilderAppend(Color.red, $"Reference reloading failed! Exception: {arg}");
						}
					}
					else
					{
						log?.TMPColorSurroundStringBuilderAppend(Color.red, "File \"" + workingDirectoryModFileOnHardDrive.HardDrivePath + "\" not found! More sure the names are the same as before!");
					}
				}
				else
				{
					log?.AppendLine("Not on hard drive. Reloading skipped!");
				}
			}
			log?.AppendLine();
			log?.AppendLine();
		}
	}
}
