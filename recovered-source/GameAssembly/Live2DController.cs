using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using GameItems;
using IC.AutomaticReference;
using JetBrains.Annotations;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;
using Live2D.Cubism.Framework.MotionFade;
using Live2D.Cubism.Framework.Physics;
using Live2D.Cubism.Framework.Pose;
using Live2D.Cubism.Framework.Raycasting;
using Live2D.Cubism.Rendering;
using StoryConditions;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public abstract class Live2DController : MonoBehaviour, ICubismUpdatable, IStoryCharacter
{
	public sealed class Part : PackedTexturesContainer.IPackable
	{
		public abstract class PartLayer
		{
			public int Layer { get; }

			public Color Color { get; }

			public Item RelatedItem { get; }

			public abstract bool IsFullLayer { get; }

			protected PartLayer(int layer, Color color, Item relatedItem)
			{
				Layer = layer;
				Color = color;
				RelatedItem = relatedItem;
			}

			public abstract void Draw(Part part, Texture mainTexture, Rect targetRect);
		}

		public sealed class DefaultTextureLayer : PartLayer
		{
			public override bool IsFullLayer => true;

			public DefaultTextureLayer(int layer, Color color, Item relatedItem)
				: base(layer, color, relatedItem)
			{
			}

			public override void Draw(Part part, Texture mainTexture, Rect targetRect)
			{
				SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.DrawLive2DMeshToTexture(part, targetRect, mainTexture, Color);
			}
		}

		public sealed class PackedTexturePartLayer : PartLayer
		{
			public PackedTexturesContainer.PackedDrawable Drawable { get; private set; }

			public PackedTexturesContainer.PackedTexture Texture { get; private set; }

			public override bool IsFullLayer => true;

			public PackedTexturePartLayer(int layer, Color color, PackedTexturesContainer.PackedDrawable drawable, PackedTexturesContainer.PackedTexture texture, Item relatedItem)
				: base(layer, color, relatedItem)
			{
				Drawable = drawable;
				Texture = texture;
			}

			public override void Draw(Part part, Texture mainTexture, Rect targetRect)
			{
				SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.DrawPacked(Texture, Drawable, targetRect, mainTexture.width, mainTexture.height, Color);
			}
		}

		public sealed class ScratchPartLayer : PartLayer
		{
			public class ScratchPartData
			{
				public float Quality { get; set; }

				public ItemPrefab.ScratchTextureType ScratchType { get; set; }

				public ScratchPartData(Item item)
				{
					Quality = item.Quality;
					ScratchType = item.GetItemPrefab().ScratchType;
				}
			}

			private static MD5 md5Hasher = MD5.Create();

			public ScratchPartData Data { get; private set; }

			public override bool IsFullLayer => false;

			public ScratchPartLayer(int layer, ScratchPartData data, Item relatedItem)
				: base(layer, Color.white, relatedItem)
			{
				Data = data;
			}

			private Rect PositionRandomlyOnUvMesh(Part part, Rect targetRect, Texture2D scratchTexture, Texture mainTexture)
			{
				Mesh mesh = part.Mesh;
				Vector2[] uv = mesh.uv;
				int[] triangles = mesh.triangles;
				Rect rect = MeshUtilitiesManager.GetRect(new RectInt(0, 0, scratchTexture.width, scratchTexture.height), mainTexture);
				Rect rect2 = new Rect(targetRect.xMin, targetRect.yMin, targetRect.width - rect.width, targetRect.height - rect.height);
				int num = part.GetRandomTriangleUniformlyByArea() * 3;
				float num2 = UnityEngine.Random.Range(0f, 1f);
				float num3 = UnityEngine.Random.Range(0f, 1f);
				if (num3 < num2)
				{
					float num4 = num3;
					float num5 = num2;
					num2 = num4;
					num3 = num5;
				}
				float num6 = num2 - 0f;
				float num7 = num3 - num2;
				float num8 = 1f - num3;
				Vector2 vector = uv[triangles[num]] * num6 + uv[triangles[num + 1]] * num7 + uv[triangles[num + 2]] * num8;
				vector.x = Mathf.Clamp(vector.x, rect2.xMin, rect2.xMax);
				vector.y = Mathf.Clamp(vector.y, rect2.yMin, rect2.yMax);
				rect.Set(vector.x, vector.y, rect.width, rect.height);
				return rect;
			}

			private void DrawScratch(Part part, Texture mainTexture, Rect targetRect, Texture2D scratchTexture)
			{
				Rect targetRect2 = PositionRandomlyOnUvMesh(part, targetRect, scratchTexture, mainTexture);
				float rotation = UnityEngine.Random.Range(0f, (float)Math.PI * 2f);
				SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.DrawScratch(scratchTexture, rotation, targetRect2, Color);
			}

			private int Combine(int a, int b, int c, int d)
			{
				return (((17 * 31 + a) * 31 + b) * 31 + c) * 31 + d;
			}

			public override void Draw(Part part, Texture mainTexture, Rect targetRect)
			{
				UnityEngine.Random.State state = UnityEngine.Random.state;
				try
				{
					string id = part.CubismDrawable.Id;
					int a = BitConverter.ToInt32(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(id)), 0);
					int c = BitConverter.ToInt32(BitConverter.GetBytes(Data.Quality), 0);
					UnityEngine.Random.InitState(Combine(a, Layer, c, (int)Data.ScratchType));
					if ((double)Data.Quality >= 1.0)
					{
						return;
					}
					float num = (1f - Data.Quality) * UnityEngine.Random.Range(1f, 1.1f);
					float num2 = Mathf.Clamp(part.Area * 1000f, 1f, 1000f);
					TextureSingleton.ScratchContainer container = SingletonMonoBehaviour<TextureSingleton>.Instance.GetContainer(Data.ScratchType);
					if (container.Small.Count > 0)
					{
						int num3 = Mathf.CeilToInt(num * 3.5f * num2);
						for (int i = 0; i < num3; i++)
						{
							DrawScratch(part, mainTexture, targetRect, container.Small[UnityEngine.Random.Range(0, container.Small.Count)]);
						}
					}
					if (Data.Quality < 0.9f && container.Medium.Count > 0 && !id.Contains("Hand"))
					{
						int num4 = Mathf.FloorToInt(num * 3f * Mathf.Sqrt(num2) * UnityEngine.Random.Range(0.7f, 1f));
						for (int j = 0; j < num4; j++)
						{
							DrawScratch(part, mainTexture, targetRect, container.Medium[UnityEngine.Random.Range(0, container.Medium.Count)]);
						}
					}
					if (Data.Quality < 0.75f && container.Big.Count > 0 && !id.Contains("Hand"))
					{
						int num5 = Mathf.FloorToInt(num * Mathf.Sqrt(num2) * UnityEngine.Random.Range(0.7f, 1f));
						for (int k = 0; k < num5; k++)
						{
							DrawScratch(part, mainTexture, targetRect, container.Big[UnityEngine.Random.Range(0, container.Big.Count)]);
						}
					}
				}
				finally
				{
					UnityEngine.Random.state = state;
				}
			}
		}

		public const int VanillaLayer = 0;

		public const int OverlayLayer = 1;

		private ColoringGroup _coloringGroup;

		public string FullName => CubismDrawable.Id;

		public Mesh Mesh => CubismRenderer.Mesh;

		public Texture MainTexture => CubismRenderer.MainTexture;

		public Rect RealNormalizedRect { get; private set; }

		public RectInt ExpandedPixelRect { get; private set; }

		public Rect ExpandedNormalizedSize { get; private set; }

		public float[] TriangleAreas { get; private set; }

		public float Area { get; private set; }

		public Live2DController Owner { get; private set; }

		public Live2DControllerElement.ElementColoringGroup RelatedElementColoringGroup { get; private set; }

		public CubismRenderer CubismRenderer { get; private set; }

		public CubismDrawable CubismDrawable { get; private set; }

		public MeshRenderer MeshRenderer { get; private set; }

		public GameObject GameObject => CubismRenderer.gameObject;

		public Material DefaultMaterial { get; private set; }

		public bool EverDrawn { get; private set; }

		public List<PartLayer> PreviousLayers { get; private set; }

		public List<PartLayer> Layers { get; private set; } = new List<PartLayer>();

		string PackedTexturesContainer.IPackable.Pack_Name => CubismDrawable.Id;

		RectInt PackedTexturesContainer.IPackable.Pack_RectInt => ExpandedPixelRect;

		int PackedTexturesContainer.IPackable.Pack_ColorIndex => 0;

		DrawableTypeEnum PackedTexturesContainer.IPackable.Pack_DrawableType => DrawableTypeEnum.Default;

		bool PackedTexturesContainer.IPackable.Pack_BypassColorScaler => false;

		public bool IsSkinnedClone { get; private set; }

		public ColoringGroup ColoringGroup => _coloringGroup;

		public Part(CubismRenderer cubismRenderer, Live2DController owner)
		{
			CubismRenderer = cubismRenderer;
			CubismDrawable = cubismRenderer.GetComponent<CubismDrawable>();
			MeshRenderer = cubismRenderer.GetComponent<MeshRenderer>();
			Owner = owner;
			FillAllData();
		}

		public Part(CubismDrawable cubismDrawable, Live2DController owner)
		{
			CubismRenderer = cubismDrawable.GetComponent<CubismRenderer>();
			CubismDrawable = cubismDrawable;
			MeshRenderer = cubismDrawable.GetComponent<MeshRenderer>();
			Owner = owner;
			FillAllData();
		}

		private void FillAllData()
		{
			RealNormalizedRect = MeshUtilitiesManager.GetUVRect(Mesh);
			ExpandedPixelRect = MeshUtilitiesManager.GetRectInt(RealNormalizedRect, MainTexture);
			ExpandedNormalizedSize = MeshUtilitiesManager.GetRect(ExpandedPixelRect, MainTexture);
			int[] triangles = Mesh.triangles;
			Vector2[] uv = Mesh.uv;
			TriangleAreas = new float[triangles.Length / 3];
			Area = 0f;
			for (int i = 0; i * 3 < triangles.Length; i++)
			{
				Vector2 vector = uv[triangles[i * 3]];
				Vector2 vector2 = uv[triangles[i * 3 + 1]];
				Vector2 vector3 = uv[triangles[i * 3 + 2]];
				TriangleAreas[i] = Mathf.Abs(vector.x * (vector2.y - vector3.y) + vector2.x * (vector3.y - vector.y) + vector3.x * (vector.y - vector2.y)) / 2f;
				Area += TriangleAreas[i];
			}
			DefaultMaterial = CubismRenderer.MeshRenderer.sharedMaterial;
		}

		public int GetRandomTriangleUniformlyByArea()
		{
			float num = UnityEngine.Random.Range(0f, Area);
			for (int i = 0; i < TriangleAreas.Length; i++)
			{
				num -= TriangleAreas[i];
				if (num < 0f)
				{
					return i;
				}
			}
			return TriangleAreas.Length - 1;
		}

		public void PrepareForDrawing()
		{
			List<PartLayer> layers = PreviousLayers.ClearOrCreate();
			PreviousLayers = Layers;
			Layers = layers;
			CubismRenderer.Material = DefaultMaterial;
		}

		public void Clear()
		{
			PreviousLayers = null;
			Layers = Layers.ClearOrCreate();
		}

		public bool NeedsToBeDrawn()
		{
			return Layers.Count > 0;
		}

		public bool NeedsRedrawing()
		{
			if (IsSkinnedClone)
			{
				return false;
			}
			if (PreviousLayers == null)
			{
				return Layers.Count > 0;
			}
			if (PreviousLayers.Count != Layers.Count)
			{
				return true;
			}
			if (Layers.Count == 0)
			{
				return false;
			}
			int count = PreviousLayers.Count;
			for (int i = 0; i < count; i++)
			{
				PartLayer partLayer = PreviousLayers[i];
				PartLayer partLayer2 = Layers[i];
				if (partLayer.Layer != partLayer2.Layer || partLayer.Color != partLayer2.Color)
				{
					return true;
				}
				if (partLayer is DefaultTextureLayer && partLayer2 is DefaultTextureLayer)
				{
					continue;
				}
				if (partLayer.GetType() != partLayer2.GetType())
				{
					return true;
				}
				if (partLayer is PackedTexturePartLayer packedTexturePartLayer && partLayer2 is PackedTexturePartLayer packedTexturePartLayer2)
				{
					if (packedTexturePartLayer.Texture != packedTexturePartLayer2.Texture || packedTexturePartLayer.Drawable != packedTexturePartLayer2.Drawable)
					{
						return true;
					}
				}
				else if (partLayer is ScratchPartLayer scratchPartLayer && partLayer2 is ScratchPartLayer scratchPartLayer2 && (scratchPartLayer.Data.Quality != scratchPartLayer2.Data.Quality || scratchPartLayer.Data.ScratchType != scratchPartLayer2.Data.ScratchType))
				{
					return true;
				}
			}
			return false;
		}

		public void InitRelatedElementColoringGroup(Live2DControllerElement.ElementColoringGroup relatedElementColoringGroup)
		{
			if (RelatedElementColoringGroup != null)
			{
				Debug.LogError(FullName + " overriding element coloring group! This should never happen!");
			}
			RelatedElementColoringGroup = relatedElementColoringGroup;
		}

		public bool TryGetVanillaLayer(out PartLayer layer)
		{
			layer = null;
			if (Layers.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < Layers.Count && Layers[i].Layer <= 0; i++)
			{
				if (Layers[i].IsFullLayer)
				{
					layer = Layers[i];
					return true;
				}
			}
			return false;
		}

		private int FindInsertIndex(int layer)
		{
			for (int i = 0; i < Layers.Count; i++)
			{
				if (Layers[i].Layer > layer)
				{
					return i;
				}
			}
			return -1;
		}

		private void AddLayer(PartLayer layer)
		{
			if (Layers.Count == 0)
			{
				Layers.Add(layer);
				return;
			}
			int num = FindInsertIndex(layer.Layer);
			if (num == -1)
			{
				num = Layers.Count;
				Layers.Add(layer);
			}
			else
			{
				Layers.Insert(num, layer);
			}
			if (num > 0 && layer.IsFullLayer && layer.Layer <= 0)
			{
				Layers.RemoveRange(0, num);
			}
		}

		public void AddVanilla([CanBeNull] ItemWithCurrentData itemData, Color color)
		{
			if (Layers.Count == 0)
			{
				AddLayer(new DefaultTextureLayer(0, color, itemData?.Item));
				if (itemData?.ScratchPartData != null)
				{
					AddLayer(new ScratchPartLayer(0, itemData.ScratchPartData, itemData.Item));
				}
			}
		}

		public void AddLayers(ItemWithCurrentData itemWithCurrentData, PackedTexturesContainer.PackedTexture packedTexture, PackedTexturesContainer.PackedDrawable packedDrawable)
		{
			Item item = itemWithCurrentData.Item;
			Color color = (item.GetColors().TryGetValue(packedDrawable.ColorIndex, out var value) ? value : Color.white);
			AddLayer(new PackedTexturePartLayer(color: (!packedDrawable.BypassColorScaler && RelatedElementColoringGroup != null) ? RelatedElementColoringGroup.ColorScaler.Scale(color) : color, layer: packedTexture.Layer, drawable: packedDrawable, texture: packedTexture, relatedItem: item));
			if (itemWithCurrentData.ScratchPartData != null)
			{
				AddLayer(new ScratchPartLayer(packedTexture.Layer, itemWithCurrentData.ScratchPartData, item));
			}
		}

		public void SetShader(Shader shader, Action<Material, List<UnityEngine.Object>> customShaderMaterial, List<UnityEngine.Object> objectsReferencedByShaders)
		{
			Material material = new Material(shader);
			material.CopyPropertiesFromMaterial(DefaultMaterial);
			customShaderMaterial?.Invoke(material, objectsReferencedByShaders);
			CubismRenderer.Material = material;
		}

		public void DrawRuntimeWithClear(Rect targetRect, Texture texture)
		{
			if (!IsSkinnedClone)
			{
				SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.ClearLive2DMesh(this, targetRect, Color.clear);
				DrawRuntime(targetRect, texture);
			}
		}

		public void DrawRuntime(Rect targetRect, Texture texture)
		{
			if (!IsSkinnedClone)
			{
				for (int i = 0; i < Layers.Count; i++)
				{
					Layers[i].Draw(this, texture, targetRect);
				}
			}
		}

		public void SetActive(bool active)
		{
			MeshRenderer.enabled = active;
			MeshRenderer.forceRenderingOff = !active;
		}

		public void Draw(Rect targetRectInTexture)
		{
			SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.DrawLive2DMeshToTexture(this, targetRectInTexture, MainTexture);
		}

		public void MarkAsSkinnedClone()
		{
			IsSkinnedClone = true;
		}

		public void SetColoringGroup(ColoringGroup coloringGroup)
		{
			if (_coloringGroup != null)
			{
				Debug.LogError($"Part {FullName} already has a coloring group {_coloringGroup.ColoringGroupEnum}! Changing it to {coloringGroup.ColoringGroupEnum}!");
			}
			_coloringGroup = coloringGroup;
		}

		public Rect CalculateApproximateWorldRect()
		{
			CubismRenderer cubismRenderer = CubismRenderer;
			Bounds bounds = Mesh.bounds;
			Vector3 vector = cubismRenderer.transform.TransformPoint(bounds.min);
			Vector3 vector2 = cubismRenderer.transform.TransformPoint(bounds.max);
			Vector2 position = new Vector2(vector.x, vector.y);
			Vector2 size = new Vector2(vector2.x - vector.x, vector2.y - vector.y);
			return new Rect(position, size);
		}

		public Vector3 CalculateWorldBoundsCenter()
		{
			return CubismRenderer.CalculateWorldBoundsCenter();
		}
	}

	public class TextureContainer
	{
		public List<Part> AllParts { get; private set; }

		public Texture OriginalTexture { get; private set; }

		public Texture CurrentTexture { get; private set; }

		public TextureContainer(List<Part> allParts, Texture originalTexture)
		{
			AllParts = allParts;
			OriginalTexture = originalTexture;
			CurrentTexture = originalTexture;
		}

		public bool IsSizeValid(int width, int height)
		{
			Texture originalTexture = OriginalTexture;
			if (originalTexture.width != width || originalTexture.height != height)
			{
				Debug.LogError($"Live2D textures should be {width}x{height} only! Other ones are unsupported! Ignoring {originalTexture.width}x{originalTexture.height} sized texture \"{originalTexture.name}\"!");
				return false;
			}
			return true;
		}

		public void SetTexture(Texture texture)
		{
			if (CurrentTexture == texture)
			{
				return;
			}
			if (CurrentTexture != OriginalTexture)
			{
				SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.ReturnToPoll(CurrentTexture);
			}
			CurrentTexture = texture;
			foreach (Part allPart in AllParts)
			{
				allPart.CubismRenderer.MainTexture = texture;
			}
		}

		public void PrepareForDrawing()
		{
			foreach (Part allPart in AllParts)
			{
				allPart.PrepareForDrawing();
			}
		}

		public bool TryGetRenderTexture(out RenderTexture renderTexture)
		{
			if (CurrentTexture is RenderTexture renderTexture2)
			{
				renderTexture = renderTexture2;
				return true;
			}
			renderTexture = null;
			return false;
		}

		public bool NeedsRedrawing()
		{
			for (int i = 0; i < AllParts.Count; i++)
			{
				if (AllParts[i].NeedsRedrawing())
				{
					return true;
				}
			}
			return false;
		}

		public bool NeedsAnyDrawing()
		{
			for (int i = 0; i < AllParts.Count; i++)
			{
				if (AllParts[i].NeedsToBeDrawn())
				{
					return true;
				}
			}
			return false;
		}

		public bool CheckIfRenderTextureIsValid()
		{
			if (CurrentTexture is RenderTexture renderTexture)
			{
				return renderTexture.IsCreated();
			}
			return true;
		}

		public void RestoreOriginalTexture()
		{
			if (!(CurrentTexture != OriginalTexture))
			{
				return;
			}
			foreach (Part allPart in AllParts)
			{
				allPart.CubismRenderer.MainTexture = OriginalTexture;
				allPart.Clear();
			}
			SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.ReturnToPoll(CurrentTexture);
			CurrentTexture = OriginalTexture;
		}

		public void ClearAndDrawAllParts()
		{
			Texture originalTexture = OriginalTexture;
			GL.Clear(clearDepth: true, clearColor: true, Color.clear);
			for (int i = 0; i < AllParts.Count; i++)
			{
				Part part = AllParts[i];
				Rect expandedNormalizedSize = part.ExpandedNormalizedSize;
				part.DrawRuntime(expandedNormalizedSize, originalTexture);
			}
		}
	}

	public class ItemWithCurrentData : IDisposable
	{
		public readonly Item Item;

		public readonly ItemPrefab Prefab;

		public Part.ScratchPartLayer.ScratchPartData ScratchPartData;

		public List<string> ActiveElements = MyCollectionPool<string>.GetList();

		public List<PackedTexturesContainer.PackedTexture> ActiveTextures = MyCollectionPool<PackedTexturesContainer.PackedTexture>.GetList();

		public bool VanillaActive;

		public ItemWithCurrentData(Item item)
		{
			Item = item;
			Prefab = item.GetItemPrefab();
		}

		private void ClearLists()
		{
			ActiveElements.Clear();
			ActiveTextures.Clear();
		}

		public void UpdateData(string controller)
		{
			ScratchPartData = (Prefab.HasQuality ? new Part.ScratchPartLayer.ScratchPartData(Item) : null);
			ClearLists();
			if (Prefab.IsEquipable(out var slotEquipData))
			{
				slotEquipData.FillCurrent(this, controller);
				ActiveTextures.Sort(PackedTexturesContainer.PackedTexture.LayerComparer);
			}
			VanillaActive = ActiveTextures == null || ActiveTextures.Count == 0 || ActiveTextures.Any((PackedTexturesContainer.PackedTexture x) => x.IncludeVanillaLayers);
		}

		public void Dispose()
		{
			MyCollectionPool<string>.ReleaseAndSetNull(ref ActiveElements);
			MyCollectionPool<PackedTexturesContainer.PackedTexture>.ReleaseAndSetNull(ref ActiveTextures);
		}
	}

	public enum UpdateTexturesFromCurrentlyEquippedTypeEnum
	{
		Reset,
		Active
	}

	public interface ILive2DControllerManager
	{
	}

	public interface ILive2DControllerManager_Callback
	{
	}

	public interface ILive2DControllerManager_SerializedState : ILive2DControllerManager_Callback
	{
		void SaveState(Live2DControllerCommonState state);

		void LoadState(Live2DControllerCommonState state);
	}

	public interface ILive2DControllerManager_Update : ILive2DControllerManager_Callback
	{
		void Update();
	}

	public interface ILive2DControllerManager_FixedUpdate : ILive2DControllerManager_Callback
	{
		void FixedUpdate();
	}

	public interface ILive2DControllerManager_StartUsageGameplay : ILive2DControllerManager_Callback
	{
		void StartUsageGameplay();
	}

	public interface ILive2DControllerManager_EndUsage : ILive2DControllerManager_Callback
	{
		void EndUsage();
	}

	public interface ILive2DControllerManager_OnDisable : ILive2DControllerManager_Callback
	{
		void OnDisable();
	}

	public interface ILive2DControllerManager_ResetStage1 : ILive2DControllerManager_Callback
	{
		void ResetStage1();
	}

	public enum EyeSelection
	{
		LeftEye,
		RightEye,
		BothEyes,
		BothEyesTarget
	}

	public class ColoringGroup
	{
		private CubismRenderer[] _cubismRenderersCache;

		public ColoringGroupEnum ColoringGroupEnum { get; private set; }

		public bool Enabled { get; private set; }

		public List<Part> Parts { get; private set; }

		[CanBeNull]
		public Part SkinningMainPart { get; private set; }

		public bool IsSkinned => SkinningMainPart != null;

		public bool HasNoParts
		{
			get
			{
				if (Parts.Count == 0)
				{
					return SkinningMainPart == null;
				}
				return false;
			}
		}

		public ColoringGroup(ColoringGroupEnum coloringGroupEnum, List<Part> parts)
		{
			ColoringGroupEnum = coloringGroupEnum;
			Enabled = true;
			Parts = parts;
			for (int i = 0; i < parts.Count; i++)
			{
				parts[i].SetColoringGroup(this);
			}
		}

		public void Toggle(bool value)
		{
			foreach (Part part in Parts)
			{
				part.MeshRenderer.forceRenderingOff = !value;
			}
			if (SkinningMainPart != null)
			{
				SkinningMainPart.MeshRenderer.forceRenderingOff = true;
			}
			Enabled = value;
		}

		public void ColorAndToggle(bool value, Color color)
		{
			foreach (Part part in Parts)
			{
				part.MeshRenderer.forceRenderingOff = !value;
				part.CubismRenderer.Color = color;
			}
			if (SkinningMainPart != null)
			{
				SkinningMainPart.MeshRenderer.forceRenderingOff = true;
			}
			Enabled = value;
		}

		public void Color(Color color)
		{
			foreach (Part part in Parts)
			{
				part.CubismRenderer.Color = color;
			}
		}

		public CubismRenderer[] GetCubismRenderers()
		{
			return _cubismRenderersCache ?? (_cubismRenderersCache = Parts.Select((Part t) => t.CubismRenderer).ToArray());
		}

		public void PrepareAsSkinning(string mainPartId)
		{
			int num = Parts.FindIndex((Part t) => t.FullName == mainPartId);
			if (num < 0)
			{
				if (Parts.Count > 0)
				{
					Debug.LogError("Main part " + mainPartId + " not found!");
				}
				return;
			}
			SkinningMainPart = Parts[num];
			Parts.RemoveAtSwapBack(num);
			foreach (Part part in Parts)
			{
				part.MarkAsSkinnedClone();
			}
		}
	}

	public enum ColoringGroupEnum
	{
		Hair0_Top,
		Hair0_Back,
		Hair0_Braid,
		Hair0_Braid_HairTieAccessory,
		Hair0_Side,
		Hair0_SideAlt,
		Hair0_Front,
		Hair0_FrontClippedUp_Bang,
		Hair0_FrontClippedUp_HairClip,
		Hair1_Top,
		Hair1_Front,
		Hair1_Side,
		Hair1_Back,
		Hair2_Front,
		Hair3_Front,
		Hair3_Side,
		Hair3_Top,
		Hair3_Back,
		Hair4_Ponytail,
		Hair4_KnotAccessory,
		Skirt,
		Shirt,
		ShirtShadow,
		StockingL,
		StockingR,
		Panties,
		Bra,
		Hoodie,
		HoodieStrings,
		Pants,
		PantsBelt,
		Dress1,
		Dress2Alt,
		Dress2,
		ShoeR,
		ShoeL,
		PointyEars,
		WizardHat,
		BellChoker,
		CuteBow,
		PumpkinHairPin,
		Headband,
		NipplePiercingL,
		NipplePiercingR,
		NavelPiercing,
		BikiniTop_Main,
		BikiniTop_Alt,
		BikiniBottom_Main,
		BikiniBottom_Alt,
		BikiniBottom_Uncolored,
		IrisL,
		IrisR,
		PupilL,
		PupilR,
		Sclera,
		Eyelash,
		HeartPupils,
		EyeBrows,
		Nipples,
		Vagina,
		Xray,
		Anus,
		Teeth,
		Mouth,
		Boobs,
		Ass,
		Pelvic,
		Thigh,
		Tummy,
		Skin,
		Blush,
		SharpTeeth,
		CatEarMain,
		CatEarSecondary,
		LegLFoot,
		LegL,
		LegRFoot,
		LegR,
		ArmL,
		ArmR,
		Tail,
		TailPlug,
		Hologram,
		OtherAttachments,
		MCSkin,
		MCCockSkin,
		MCCockTip,
		Shadow,
		Highlight,
		Mask,
		Moddable,
		HitArea,
		Cum,
		Rest
	}

	public class ColorScaler
	{
		public static readonly ColorScaler DefaultColorScaler = new ColorScaler(1f);

		public Color ColorMin { get; private set; }

		public Color ColorMax { get; private set; }

		public ColorScaler(float strength)
		{
			ColorMin = 0.05f.ToColor();
			ColorMax = strength.ToColor();
		}

		public ColorScaler(Color baseColor)
		{
			float r = 1f / baseColor.r;
			float b = 1f / baseColor.b;
			float g = 1f / baseColor.g;
			Color color = new Color(r, g, b);
			ColorMin = color * 0.05f;
			ColorMax = color;
		}

		public ColorScaler(Color colorMin, Color colorMax)
		{
			ColorMin = colorMin;
			ColorMax = colorMax;
		}

		public Color Scale(Color color)
		{
			return ColorMagicV2(color, ColorMin, ColorMax);
		}
	}

	public sealed class Live2DControllerElement
	{
		public sealed class ElementColoringGroup
		{
			public Func<Item, Color?> ColorExtractionFunc = GetColorFromAdditionalData;

			public ColoringGroupEnum[] GroupsEnum;

			public ColoringGroup[] Groups;

			public ColorScaler ColorScaler;

			public Live2DParameter EnableParam;

			public bool ForceToggleEvenWithParam;

			public bool NoItem;

			public ElementColoringGroup(ColorScaler colorScaler, params ColoringGroupEnum[] groups)
			{
				ColorScaler = colorScaler;
				GroupsEnum = groups;
				Groups = new ColoringGroup[GroupsEnum.Length];
				NoItem = false;
			}

			public static ElementColoringGroup CreateInstanceColorIndex0(ColorScaler colorScaler, params ColoringGroupEnum[] groups)
			{
				return new ElementColoringGroup(colorScaler, groups);
			}

			public static ElementColoringGroup CreateInstanceParameter(ColorScaler colorScaler, Live2DParameter enableParam, params ColoringGroupEnum[] groups)
			{
				return new ElementColoringGroup(colorScaler, groups)
				{
					EnableParam = enableParam
				};
			}

			public static ElementColoringGroup CreateInstanceParameterForceToggle(ColorScaler colorScaler, Live2DParameter enableParam, params ColoringGroupEnum[] groups)
			{
				return new ElementColoringGroup(colorScaler, groups)
				{
					EnableParam = enableParam,
					ForceToggleEvenWithParam = true
				};
			}

			public static ElementColoringGroup CreateInstanceDifferentColor(int index, ColorScaler colorScaler, params ColoringGroupEnum[] groups)
			{
				return new ElementColoringGroup(colorScaler, groups)
				{
					ColorExtractionFunc = (Item i) => i?.GetColor(index)
				};
			}

			private static Color? GetColorFromAdditionalData(Item item)
			{
				return item?.GetMainColor();
			}

			public Color GetColor(Item item)
			{
				return ColorScaler.Scale(ColorExtractionFunc(item) ?? Color.magenta);
			}

			public void Init(Live2DController controller)
			{
				for (int i = 0; i < GroupsEnum.Length; i++)
				{
					ColoringGroup coloringGroup = controller.GetGroup(GroupsEnum[i]);
					Groups[i] = coloringGroup;
					foreach (Part part in coloringGroup.Parts)
					{
						part.InitRelatedElementColoringGroup(this);
					}
				}
			}
		}

		public readonly string id;

		private ElementColoringGroup[] groups;

		public IReadOnlyCollection<ElementColoringGroup> Groups => groups;

		public bool IsActive { get; private set; }

		public Live2DControllerElement(string id, params ElementColoringGroup[] group)
		{
			this.id = id;
			groups = group;
		}

		public void Activate(ItemWithCurrentData item, Dictionary<Live2DParameter, bool> enabledParameters)
		{
			bool flag = item != null;
			bool flag2 = false;
			ElementColoringGroup[] array = groups;
			foreach (ElementColoringGroup elementColoringGroup in array)
			{
				bool flag3 = (elementColoringGroup.NoItem ? (!flag) : flag);
				flag2 |= flag3;
				bool flag4 = elementColoringGroup.EnableParam != null && elementColoringGroup.EnableParam.Exists;
				if (!flag4 || elementColoringGroup.ForceToggleEvenWithParam)
				{
					ColoringGroup[] array2 = elementColoringGroup.Groups;
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j].Toggle(flag3);
					}
				}
				if (!flag4)
				{
					continue;
				}
				Live2DParameter enableParam = elementColoringGroup.EnableParam;
				if (enabledParameters.TryGetValue(elementColoringGroup.EnableParam, out var value))
				{
					if (!value & flag3)
					{
						enabledParameters[enableParam] = true;
					}
				}
				else
				{
					enabledParameters[elementColoringGroup.EnableParam] = flag3;
				}
			}
			IsActive = flag2;
		}

		public IEnumerable<(Color color, Part part)> AllPartsOnTextureWithColor(Live2DController controller, Item item)
		{
			foreach (ElementColoringGroup group in Groups)
			{
				Color color = group.GetColor(item);
				ColoringGroup[] array = group.Groups;
				foreach (ColoringGroup coloringGroup in array)
				{
					if (coloringGroup.IsSkinned)
					{
						yield return (color: color, part: coloringGroup.SkinningMainPart);
						continue;
					}
					foreach (Part part in coloringGroup.Parts)
					{
						yield return (color: color, part: part);
					}
				}
			}
		}

		public IEnumerable<(ElementColoringGroup group, Part part)> AllPartsWithOnTextureColoringGroup(Live2DController controller)
		{
			foreach (ElementColoringGroup coloringGroup in Groups)
			{
				ColoringGroup[] array = coloringGroup.Groups;
				foreach (ColoringGroup coloringGroup2 in array)
				{
					if (coloringGroup2.IsSkinned)
					{
						yield return (group: coloringGroup, part: coloringGroup2.SkinningMainPart);
						continue;
					}
					foreach (Part part in coloringGroup2.Parts)
					{
						yield return (group: coloringGroup, part: part);
					}
				}
			}
		}

		public IEnumerable<Part> AllPartsNonSegmented(Live2DController controller)
		{
			foreach (ElementColoringGroup group in Groups)
			{
				ColoringGroup[] array = group.Groups;
				foreach (ColoringGroup coloringGroup in array)
				{
					if (coloringGroup.IsSkinned)
					{
						yield return coloringGroup.SkinningMainPart;
						continue;
					}
					foreach (Part part in coloringGroup.Parts)
					{
						yield return part;
					}
				}
			}
		}
	}

	protected class Layer
	{
		public readonly int Nr;

		public HashSet<ILive2DInteractableHoverable> Interactables = new HashSet<ILive2DInteractableHoverable>();

		public List<CubismRaycastable> Raycastables = new List<CubismRaycastable>();

		public Dictionary<CubismDrawable, ILive2DInteractableHoverable> DrawableToInteractable = new Dictionary<CubismDrawable, ILive2DInteractableHoverable>();

		public HashSet<Live2DGenericInteractable> GenericInteractables = new HashSet<Live2DGenericInteractable>();

		public Live2DFullBodyInteractable FullbodyInteractable;

		public Layer(int nr)
		{
			Nr = nr;
		}
	}

	public enum KissStartPositionType
	{
		MousePosition,
		RendererCenter
	}

	public class KissingDialogueArea : StoryBotDialogueAreaDefinition
	{
		public StoryBotDialogueTagDefinition KissBoobs;

		public StoryBotDialogueTagDefinition KissPelvic;

		public StoryBotDialogueTagDefinition KissEye;

		public StoryBotDialogueTagDefinition KissCatEars;

		public StoryBotDialogueTagDefinition KissThigh;

		public StoryBotDialogueTagDefinition KissTummy;

		public StoryBotDialogueTagDefinition KissAss;

		public StoryBotDialogueTagDefinition KissLeg;

		public StoryBotDialogueTagDefinition KissFoot;

		public StoryBotDialogueTagDefinition KissArm;

		public StoryBotDialogueTagDefinition KissGeneric;

		public StoryBotDialogueTagDefinition KissHair;

		public override string GetPrefix()
		{
			return "Kissing_";
		}

		protected override void InitInner(StorySingleton storySingleton, StoryBotDialogueStage stage1, StoryBotDialogueStage stage2, StoryBotDialogueStage stage3)
		{
			DataCondition<int> sympathyPositive = StoryCondition.CommonConditions.SympathyPositive;
			KissGeneric.CreateContainer(stage3, out var o);
			o.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissGenericDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_23000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissGenericDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_23001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissGenericDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_23002));
			KissBoobs.CreateContainer(stage3, out var o2);
			o2.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissBoobsDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_24000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissBoobsDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_24001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissBoobsDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_24002));
			KissAss.CreateContainer(stage3, out var o3);
			o3.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissAssDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_26000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissAssDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_26001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissAssDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_26002));
			KissPelvic.CreateContainer(stage3, out var o4);
			o4.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissPelvicDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_27000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissPelvicDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_27001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissPelvicDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_27002));
			KissEye.CreateContainer(stage3, out var o5);
			o5.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissEyeDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_28000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissEyeDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_28001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissEyeDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_28002));
			KissCatEars.CreateContainer(stage3, out var o6);
			o6.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissCatEarsDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_29000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissCatEarsDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_29001));
			KissHair.CreateContainer(stage3, out var o7);
			o7.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissHairDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_30000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissHairDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_30001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissHairDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_30002));
			KissLeg.CreateContainer(stage3, out var o8);
			o8.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissLegDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_31000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissLegDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_31001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissLegDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_31002));
			KissThigh.CreateContainer(stage3, out var o9);
			o9.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissThighDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_32000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissThighDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_32001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissThighDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_32002));
			KissTummy.CreateContainer(stage3, out var o10);
			o10.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissTummyDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_33000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissTummyDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_33001));
			KissFoot.CreateContainer(stage3, out var o11);
			o11.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissFootDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_34000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissFootDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_34001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissFootDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_34002));
			KissArm.CreateContainer(stage3, out var o12);
			o12.AddMultipleBranchesWithSameCondition(sympathyPositive, new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissArmDialogues.GetGameId(0), LOC.Other.TupleReferences.BotDialogueStage3_35000), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissArmDialogues.GetGameId(1), LOC.Other.TupleReferences.BotDialogueStage3_35001), new StoryBotDialogueBranch(Stage3VanillaDialogueIds.KissArmDialogues.GetGameId(2), LOC.Other.TupleReferences.BotDialogueStage3_35002));
		}
	}

	public abstract class Live2DControllerItemModifier
	{
		public Item SourceItem { get; private set; }

		public void SetSourceItem(Item item)
		{
			SourceItem = item;
		}

		public abstract void OnAdded(Live2DController live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum type);

		public virtual void OnReadded(Live2DController live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum type)
		{
		}

		public abstract void FixedUpdate(Live2DController live2DController);

		public virtual void OnDisabled(Live2DController live2DController)
		{
		}

		public abstract void OnRemoved(Live2DController live2DController);
	}

	public class CustomizableLive2DControllerItemModifier : Live2DControllerItemModifier
	{
		[CanBeNull]
		public Action<Live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum> OnAddedAction;

		[CanBeNull]
		public Action<Live2DController> OnRemovedAction;

		[CanBeNull]
		public Action<Live2DController> OnFixedUpdateAction;

		[CanBeNull]
		public Action<Live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum> OnReaddedAction;

		private CustomizableLive2DControllerItemModifier()
		{
		}

		public static CustomizableLive2DControllerItemModifier CreateInstance()
		{
			return new CustomizableLive2DControllerItemModifier();
		}

		public override void OnAdded(Live2DController live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum type)
		{
			try
			{
				OnAddedAction?.Invoke(live2DController, type);
			}
			catch (Exception exception)
			{
				Debug.LogError(string.Format("Error in {0} => {1} => {2}", SourceItem, "CustomizableLive2DControllerItemModifier", "OnAddedAction"));
				Debug.LogException(exception);
			}
		}

		public override void OnReadded(Live2DController live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum type)
		{
			try
			{
				OnReaddedAction?.Invoke(live2DController, type);
			}
			catch (Exception exception)
			{
				Debug.LogError(string.Format("Error in {0} => {1} => {2}", SourceItem, "CustomizableLive2DControllerItemModifier", "OnReaddedAction"));
				Debug.LogException(exception);
			}
		}

		public override void FixedUpdate(Live2DController live2DController)
		{
			try
			{
				OnFixedUpdateAction?.Invoke(live2DController);
			}
			catch (Exception exception)
			{
				Debug.LogError(string.Format("Error in {0} => {1} => {2}", SourceItem, "CustomizableLive2DControllerItemModifier", "OnFixedUpdateAction"));
				Debug.LogException(exception);
			}
		}

		public override void OnRemoved(Live2DController live2DController)
		{
			try
			{
				OnRemovedAction?.Invoke(live2DController);
			}
			catch (Exception exception)
			{
				Debug.LogError(string.Format("Error in {0} => {1} => {2}", SourceItem, "CustomizableLive2DControllerItemModifier", "OnFixedUpdateAction"));
				Debug.LogException(exception);
			}
		}
	}

	public sealed class CatBellLive2DControllerItemModifier : Live2DControllerItemModifier
	{
		private class BellMemory
		{
			public bool direction;

			public float directionSpeed;
		}

		private List<BellMemory> _bellMemories;

		private List<CubismPhysicsSubRig> _subRigs;

		private static AudioClip[] _bellSoundEffects;

		private AudioSource _bellAudioSource;

		public override void OnAdded(Live2DController live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum type)
		{
			_subRigs = live2DController.GetComponent<CubismPhysicsController>().Rig.SubRigs.Where((CubismPhysicsSubRig t) => t.Output.Any((CubismPhysicsOutput o) => o.DestinationId.IndexOf("hair", StringComparison.OrdinalIgnoreCase) > 0 || o.DestinationId.IndexOf("legL", StringComparison.OrdinalIgnoreCase) > 0 || o.DestinationId.IndexOf("Skirt", StringComparison.OrdinalIgnoreCase) > 0)).ToList();
			_bellMemories = new List<BellMemory>();
			_bellMemories.AddRange(from t in Enumerable.Range(0, 5)
				select new BellMemory());
			if (_bellSoundEffects == null)
			{
				_bellSoundEffects = R.Sound.Bell_All.LoadAll().ToArray();
			}
			if ((object)_bellAudioSource == null)
			{
				_bellAudioSource = SingletonMonoBehaviour<SoundSingleton>.Instance.GetFreeAudioSource();
			}
		}

		public override void FixedUpdate(Live2DController live2DController)
		{
			if (_bellAudioSource.isPlaying)
			{
				return;
			}
			float num = 0f;
			bool flag = false;
			for (int i = 0; i < _subRigs.Count; i++)
			{
				CubismPhysicsSubRig cubismPhysicsSubRig = _subRigs[i];
				BellMemory bellMemory = _bellMemories[i];
				CubismPhysicsParticle cubismPhysicsParticle = cubismPhysicsSubRig.Particles[cubismPhysicsSubRig.Particles.Length - 1];
				float num2 = cubismPhysicsParticle.Position.x - cubismPhysicsParticle.LastPosition.x;
				bool flag2 = num2 > 0f;
				float a = Mathf.Abs(num2);
				if (bellMemory.direction != flag2)
				{
					num += bellMemory.directionSpeed;
					if (bellMemory.directionSpeed > 0.01f)
					{
						flag = true;
					}
					bellMemory.directionSpeed = 0f;
				}
				else
				{
					bellMemory.directionSpeed = Mathf.Lerp(a, bellMemory.directionSpeed, 0.5f);
				}
				bellMemory.direction = flag2;
			}
			num = (float)Math.Tanh(num * 20f);
			if (flag)
			{
				_bellAudioSource.clip = _bellSoundEffects.GetRandom();
				_bellAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f) + num * 0.05f;
				_bellAudioSource.volume = UnityEngine.Random.Range(0.9f, 1.1f) * num * SoundSingleton.SfxVolume;
				_bellAudioSource.Play();
			}
		}

		public override void OnRemoved(Live2DController live2DController)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.ReturnToPool(_bellAudioSource);
			_bellAudioSource = null;
		}
	}

	public sealed class TailLive2DControllerItemModifier : Live2DControllerItemModifier
	{
		private bool _direction;

		private float _time;

		private float _targetTime = 1f;

		private float _target;

		private float _speedMulti;

		public TailLive2DControllerItemModifier(float speedMulti)
		{
			_speedMulti = speedMulti;
		}

		private void GetValues(Live2DController live2DController, out float _center, out float _range)
		{
			if (live2DController.SceneEnum == SceneEnumFlag.InteractionCuddle)
			{
				_center = -0.65f;
				_range = 0.25f;
			}
			else
			{
				_center = 0f;
				_range = 1f;
			}
		}

		public override void FixedUpdate(Live2DController live2DController)
		{
			if (!(live2DController is ILive2DController_TailWiggle { ParamTailWiggle: var paramTailWiggle }))
			{
				return;
			}
			_time += Time.fixedDeltaTime;
			if (_time > _targetTime)
			{
				_time = 0f;
				GetValues(live2DController, out var _center, out var _range);
				float value = paramTailWiggle.Value;
				float num = UnityEngine.Random.Range(0.4f, 0.9f) * _range;
				if (_direction)
				{
					num = 0f - num + _center;
				}
				float num2 = Mathf.Abs(num - value);
				paramTailWiggle.Value = num;
				Live2DExpression expression = live2DController.Expression;
				float num3 = (0.8f + Mathf.Clamp(expression.LastHappinessValue, -0.3f, 1f) / 2f + expression.LastLewdnessValue / 4f) * _speedMulti * _range;
				float num4 = 0.01f + num2 * UnityEngine.Random.Range(0.9f, 1f) / num3;
				_targetTime = num4 / paramTailWiggle.Length * 0.94f;
				paramTailWiggle.SetWeight(num4);
				_direction = !_direction;
			}
		}

		public override void OnAdded(Live2DController live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum type)
		{
			if (live2DController is ILive2DController_TailWiggle live2DController_TailWiggle)
			{
				live2DController_TailWiggle.ParamTailWiggle.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
				_direction = UnityEngine.Random.value > 0.5f;
			}
		}

		public override void OnRemoved(Live2DController live2DController)
		{
		}
	}

	public sealed class ParameterLive2DControllerItemModifier : Live2DControllerItemModifier
	{
		public class Data
		{
			private readonly ItemModifiableVariableFloat01Percent _floatVariable;

			private readonly Func<Live2DController, Live2DParameter> _parameterGetter;

			[CanBeNull]
			private Live2DParameter _cachedParameter;

			public Data(ItemModifiableVariableFloat01Percent floatVariable, Func<Live2DController, Live2DParameter> parameterGetter)
			{
				_floatVariable = floatVariable;
				_parameterGetter = parameterGetter;
			}

			public void Activate(Live2DController controller, Item sourceItem, UpdateTexturesFromCurrentlyEquippedTypeEnum type)
			{
				_cachedParameter = _parameterGetter(controller);
				ApplyValue(controller, sourceItem);
				if (type == UpdateTexturesFromCurrentlyEquippedTypeEnum.Reset)
				{
					FinishMove();
				}
			}

			public void Deactivate()
			{
				if (_cachedParameter != null)
				{
					_cachedParameter.ResetToDefault();
				}
				_cachedParameter = null;
			}

			public void ApplyValue(Live2DController controller, Item sourceItem)
			{
				if (_cachedParameter != null)
				{
					_cachedParameter.SetPercent(_floatVariable.GetValue(sourceItem));
				}
			}

			public void FinishMove()
			{
				if (_cachedParameter != null)
				{
					_cachedParameter.FinishMove();
				}
			}
		}

		private readonly Data[] _parameters;

		public ParameterLive2DControllerItemModifier(params Data[] parameters)
		{
			_parameters = parameters;
		}

		public override void FixedUpdate(Live2DController live2DController)
		{
			UpdateValues(live2DController);
		}

		private void UpdateValues(Live2DController live2DController)
		{
			Data[] parameters = _parameters;
			for (int i = 0; i < parameters.Length; i++)
			{
				parameters[i].ApplyValue(live2DController, SourceItem);
			}
		}

		public override void OnReadded(Live2DController live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum type)
		{
			OnAdded(live2DController, type);
		}

		public override void OnAdded(Live2DController live2DController, UpdateTexturesFromCurrentlyEquippedTypeEnum type)
		{
			Data[] parameters = _parameters;
			for (int i = 0; i < parameters.Length; i++)
			{
				parameters[i].Activate(live2DController, SourceItem, type);
			}
		}

		public override void OnRemoved(Live2DController live2DController)
		{
			Data[] parameters = _parameters;
			for (int i = 0; i < parameters.Length; i++)
			{
				parameters[i].Deactivate();
			}
		}
	}

	public interface IStoryEmoteSlot
	{
		bool IsTargetSlot(string slotName);

		IEnumerable<string> GetItemId();

		void SetItem(EmoteData emote);

		IEnumerable<string> GetValidNames();

		Result IsValidEmote(EmoteData emoteData);

		void SetDefault();
	}

	public class StoryEmoteManager
	{
		private List<IStoryEmoteSlot> _emoteSlots = new List<IStoryEmoteSlot>();

		public IEnumerable<(string slot, string item)> AllItems
		{
			get
			{
				foreach (IStoryEmoteSlot emoteSlot in _emoteSlots)
				{
					foreach (string name in emoteSlot.GetValidNames())
					{
						foreach (string item in emoteSlot.GetItemId())
						{
							yield return (slot: name, item: item);
						}
					}
				}
			}
		}

		public void AddSlot(IStoryEmoteSlot slot)
		{
			_emoteSlots.Add(slot);
		}

		public void SetDefaultEmote()
		{
			foreach (IStoryEmoteSlot emoteSlot in _emoteSlots)
			{
				emoteSlot.SetDefault();
			}
		}

		public void SetEmote(EmoteData emoteData)
		{
			foreach (IStoryEmoteSlot emoteSlot in _emoteSlots)
			{
				if (emoteSlot.IsTargetSlot(emoteData.SlotId))
				{
					emoteSlot.SetItem(emoteData);
				}
			}
		}

		public Result IsValidEmote(EmoteData emoteData)
		{
			foreach (IStoryEmoteSlot emoteSlot in _emoteSlots)
			{
				if (emoteSlot.IsTargetSlot(emoteData.SlotId))
				{
					return emoteSlot.IsValidEmote(emoteData);
				}
			}
			return Result.FailWithDescription("Slot " + emoteData.SlotId + " not found! Available slots:" + string.Join(", ", _emoteSlots.SelectMany((IStoryEmoteSlot x) => x.GetValidNames())));
		}

		public string GenerateSummary(string characterName)
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				foreach (IStoryEmoteSlot emoteSlot in _emoteSlots)
				{
					foreach (string validName in emoteSlot.GetValidNames())
					{
						stringBuilder.Append("Slot: " + validName);
						stringBuilder.AppendLine();
						foreach (string item in emoteSlot.GetItemId())
						{
							stringBuilder.Append("#");
							stringBuilder.Append(characterName);
							stringBuilder.Append(".");
							stringBuilder.Append(validName);
							stringBuilder.Append(".");
							stringBuilder.Append(item);
							stringBuilder.AppendLine();
						}
						stringBuilder.AppendLine();
					}
				}
				return stringBuilder.ToString();
			}
		}
	}

	public class StoryEmoteExpressionSlot : IStoryEmoteSlot
	{
		private delegate void SetItemAction(StoryEmoteExpressionSlot slot, EmoteData emote);

		public const string Expression_Slot = "Expression";

		public const string Clear_Expression = "Clear";

		public const string VerySad_Expression = "VerySad";

		public const string Sad_Expression = "Sad";

		public const string NoSad_Expression = "NoSad";

		public const string NoHappy_Expression = "NoHappy";

		public const string Happy_Expression = "Happy";

		public const string VeryHappy_Expression = "VeryHappy";

		public const string NoBlush_Expression = "NoBlush";

		public const string Blush_Expression = "Blush";

		public const string VeryBlush_Expression = "VeryBlush";

		public const string NoShock_Expression = "NoShock";

		public const string Shock_Expression = "Shock";

		public const string VeryShock_Expression = "VeryShock";

		public const string NoAngry_Expression = "NoAngry";

		public const string Angry_Expression = "Angry";

		public const string VeryAngry_Expression = "VeryAngry";

		private Live2DController _controller;

		private Dictionary<string, SetItemAction> _items;

		public StoryEmoteExpressionSlot(Live2DController controller)
		{
			_controller = controller;
			_items = new Dictionary<string, SetItemAction>(StringComparer.OrdinalIgnoreCase)
			{
				["Clear"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.ClearExpression();
				},
				["VerySad"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, 0f - UnityEngine.Random.Range(0.7f, 1f));
				},
				["Sad"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, 0f - UnityEngine.Random.Range(0.4f, 0.5f));
				},
				["NoSad"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, 0f);
				},
				["NoHappy"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, 0f);
				},
				["Happy"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, UnityEngine.Random.Range(0.4f, 0.5f));
				},
				["VeryHappy"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Happiness, UnityEngine.Random.Range(0.7f, 1f));
				},
				["NoBlush"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Lewdness, 0f);
				},
				["Blush"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Lewdness, 0.5f);
				},
				["VeryBlush"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Lewdness, 0.9f);
				},
				["NoShock"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Shock, 0f);
				},
				["Shock"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Shock, UnityEngine.Random.Range(0.4f, 0.6f));
				},
				["VeryShock"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Shock, UnityEngine.Random.Range(0.7f, 1f));
				},
				["NoAngry"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Angry, 0f);
				},
				["Angry"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Angry, UnityEngine.Random.Range(0.2f, 0.3f));
				},
				["VeryAngry"] = (StoryEmoteExpressionSlot slot, EmoteData emote) =>
				{
					slot._controller.Expression.AddModifier(Live2DExpression.ExpressionModifierTypeEnum.Angry, UnityEngine.Random.Range(0.5f, 0.7f));
				}
			};
		}

		public Result IsValidEmote(EmoteData emoteData)
		{
			if (_items.TryGetValue(emoteData.ItemId, out var _))
			{
				return Result.Success;
			}
			return Result.FailWithDescription("Emote item " + emoteData.ItemId + " not found in slot " + emoteData.SlotId + "! \nAvailable items: " + string.Join(", ", GetItemId()));
		}

		public IEnumerable<string> GetValidNames()
		{
			return new string[1] { "Expression" };
		}

		public bool IsTargetSlot(string slotName)
		{
			return string.Equals("Expression", slotName, StringComparison.OrdinalIgnoreCase);
		}

		public IEnumerable<string> GetItemId()
		{
			return _items.Keys;
		}

		public void SetDefault()
		{
			SetItem(new EmoteData("Expression", "Clear"));
		}

		public void SetItem(EmoteData emote)
		{
			if (_items.TryGetValue(emote.ItemId, out var value))
			{
				value?.Invoke(this, emote);
			}
			else
			{
				Debug.LogError($"Expression \"{emote}\" not found! Ignored!");
			}
		}
	}

	private float hologramTimer;

	private bool hologramStatus;

	private HashSet<Part> _allParts = new HashSet<Part>();

	private Dictionary<CubismRenderer, Part> _rendererToPart = new Dictionary<CubismRenderer, Part>();

	private Dictionary<CubismDrawable, Part> _drawableToPart = new Dictionary<CubismDrawable, Part>();

	private Dictionary<string, Part> _stringToPart = new Dictionary<string, Part>();

	[NonSerialized]
	private List<CubismRenderer> _sortedRenderersCached;

	private List<TextureContainer> textures;

	[NonSerialized]
	private readonly List<ItemWithCurrentData> _currentlyEquipped = new List<ItemWithCurrentData>();

	[NonSerialized]
	private readonly Dictionary<Item, ItemWithCurrentData> _currentlyEquippedDict = new Dictionary<Item, ItemWithCurrentData>();

	[NonSerialized]
	private readonly Dictionary<string, List<ItemWithCurrentData>> _slotToItems = new Dictionary<string, List<ItemWithCurrentData>>();

	[NonSerialized]
	private List<UnityEngine.Object> _objectsReferencedByShaders = new List<UnityEngine.Object>();

	private List<ILive2DControllerManager> _registeredManagers = new List<ILive2DControllerManager>();

	private ILive2DModelUser _currentUser;

	private CancellationTokenSource _usageCancellationTokenSource;

	protected Dictionary<ColoringGroupEnum, ColoringGroup> coloringGroups = new Dictionary<ColoringGroupEnum, ColoringGroup>();

	public static readonly ColoringGroupEnum[] AllHairNonAccessoryColoringGroups = new ColoringGroupEnum[17]
	{
		ColoringGroupEnum.Hair0_Top,
		ColoringGroupEnum.Hair0_Back,
		ColoringGroupEnum.Hair0_Side,
		ColoringGroupEnum.Hair0_SideAlt,
		ColoringGroupEnum.Hair0_Front,
		ColoringGroupEnum.Hair0_FrontClippedUp_Bang,
		ColoringGroupEnum.Hair0_Braid,
		ColoringGroupEnum.Hair1_Top,
		ColoringGroupEnum.Hair1_Front,
		ColoringGroupEnum.Hair1_Side,
		ColoringGroupEnum.Hair1_Back,
		ColoringGroupEnum.Hair2_Front,
		ColoringGroupEnum.Hair3_Front,
		ColoringGroupEnum.Hair3_Side,
		ColoringGroupEnum.Hair3_Top,
		ColoringGroupEnum.Hair3_Back,
		ColoringGroupEnum.Hair4_Ponytail
	};

	private ProjectedKeyCollection<string, Live2DControllerElement> elements;

	public static readonly string CatEarsElementId = ItemSlot.SlotManager.Head.Id + "_CatEars";

	public static readonly string WizardHatElementId = ItemSlot.SlotManager.Head.Id + "_WizardHat";

	public static readonly string CuteBowElementId = ItemSlot.SlotManager.HairAccessoryL.Id + "_CuteBow";

	public static readonly string PumpkinHairpinId = ItemSlot.SlotManager.HairAccessoryL.Id + "_PumpkinHairPin";

	public static readonly string HeadbandId = ItemSlot.SlotManager.HairAccessoryL.Id + "_Headband";

	public static readonly string SharpTeethId = ItemSlot.SlotManager.Teeth.Id + "_SharpTeeth";

	public static readonly string BellChokerElementId = ItemSlot.SlotManager.NeckAccessory.Id + "_BellChoker";

	public static readonly string ShirtId = ItemSlot.SlotManager.UpperBody.Id + "_Shirt";

	public static readonly string Dress1Id = ItemSlot.SlotManager.UpperBody.Id + "_Dress1";

	public static readonly string Dress2Id = ItemSlot.SlotManager.UpperBody.Id + "_Dress2";

	public static readonly string SkirtId = ItemSlot.SlotManager.LowerBody.Id + "_Skirt";

	public static readonly string PantsId = ItemSlot.SlotManager.LowerBody.Id + "_Pants";

	public static readonly string HoodieId = ItemSlot.SlotManager.UpperBodyOuter.Id + "_Hoodie";

	public static readonly string NippleLeftPiercingId = ItemSlot.SlotManager.NippleLeft.Id + "_Piercing";

	public static readonly string NippleRightPiercingId = ItemSlot.SlotManager.NippleRight.Id + "_Piercing";

	public static readonly string NavelPiercingId = ItemSlot.SlotManager.Navel.Id + "_Piercing";

	public static readonly string BikiniTopId = ItemSlot.SlotManager.Bra.Id + "_Bikini";

	public static readonly string BikiniBottomId = ItemSlot.SlotManager.Panties.Id + "_Bikini";

	public const string BasicHologramId = "Hologram_BasicHologram";

	public const string BasicEyebrowsId = "Eyebrows_BasicEyebrows";

	public const string Hair0_TopId = "Hair0_Top";

	public const string Hair0_FrontId = "Hair0_Front";

	public const string Hair0_FrontClippedUpId = "Hair0_FrontClippedUp";

	public const string Hair0_SideId = "Hair0_Side";

	public const string Hair0_BackId = "Hair0_Back";

	public const string Hair0_BraidId = "Hair0_Braid";

	public const string Hair1_TopId = "Hair1_Top";

	public const string Hair1_FrontId = "Hair1_Front";

	public const string Hair1_SideId = "Hair1_Side";

	public const string Hair1_BackId = "Hair1_Back";

	public const string Hair2_FrontId = "Hair2_Front";

	public const string Hair3_TopId = "Hair3_Top";

	public const string Hair3_FrontId = "Hair3_Front";

	public const string Hair3_SideId = "Hair3_Side";

	public const string Hair3_BackId = "Hair3_Back";

	public const string Hair4_PonytailId = "Hair4_Ponytail";

	public const string Hair4_KnotId = "Hair4_Knot";

	private ILive2DInteractable _hoveredOrHeld;

	private CursorSingleton.CursorOverride _interactableHoverCursorOverride;

	private bool _isHeld;

	private int _currentLayerNumber;

	private List<Layer> _allLayers = new List<Layer>();

	private Dictionary<int, Layer> _layers = new Dictionary<int, Layer>();

	[NonSerialized]
	private List<CubismRaycastHit> _raycastHitCache = new List<CubismRaycastHit>();

	[NonSerialized]
	private List<CubismRenderer> _raycastRendererCache1 = new List<CubismRenderer>();

	[NonSerialized]
	private List<CubismRenderer> _raycastRendererCache2 = new List<CubismRenderer>();

	private FastEnumFlagsDictionary<ILimbUsage> _limbUsages = new FastEnumFlagsDictionary<ILimbUsage>();

	private List<ILimbUsage> _activeLimbUsages = new List<ILimbUsage>();

	private Vector2 _startPosition;

	private Vector2 _targetRendererStartScreenPosition;

	private Part _targetPart;

	private GameObject _kissingMainGameObject;

	private RectTransform _mainRectTransform;

	private RawImage _kissImage;

	private RawImage _kissingImage;

	private CursorSingleton.CursorOverride _cursorOverride;

	private List<Live2DControllerItemModifier> _activeItemModifiers = new List<Live2DControllerItemModifier>();

	private bool _parametersInitialized;

	protected StoryEmoteManager _storyEmoteManager;

	public abstract ControllerEnum ControllerEnum { get; }

	public abstract SceneEnumFlag SceneEnum { get; }

	public ModelBrain CurrentBrain { get; set; }

	public CubismModel Model { get; private set; }

	public bool IsAnyInteractableHeld
	{
		get
		{
			if (!IsKissing)
			{
				return _isHeld;
			}
			return true;
		}
	}

	public CumOutsideManager CumOutsideManager { get; private set; }

	public ICumManagerCommon CumInsideManager { get; private set; }

	public bool IsFullyInitialized { get; private set; }

	private Camera Camera => UiOverlay.Instance.MainCamera;

	public int ExecutionOrder => -1000;

	public bool NeedsUpdateOnEditing => false;

	public bool HasUpdateController { get; set; }

	public EyeManagerCommon Eyes { get; private set; }

	public MouthManagerDefaultMouth Mouth { get; private set; }

	public virtual Vector2 DefaultEyesPosition => Vector2.zero;

	public bool IsShirtAndHoodieAsideOrOff
	{
		get
		{
			if (IsShirtAsideOrOff)
			{
				return IsHoodieAsideOrOff;
			}
			return false;
		}
	}

	public bool IsHoodieAsideOrOff
	{
		get
		{
			if (!IsHoodieAside)
			{
				return !IsHoodieOn;
			}
			return true;
		}
	}

	public virtual bool IsHoodieAside => false;

	public virtual bool IsHoodieOn => IsElementActive(HoodieId);

	public bool IsShirtAsideOrOff
	{
		get
		{
			if (!IsShirtAside)
			{
				return !IsShirtOn;
			}
			return true;
		}
	}

	public virtual bool IsShirtAside => false;

	public virtual bool IsShirtOn => IsElementActive(ShirtId);

	public bool IsPantiesAsideOrOff
	{
		get
		{
			if (!IsPantiesAside)
			{
				return !IsPantiesOn;
			}
			return true;
		}
	}

	public virtual bool IsPantiesAside => false;

	public virtual bool IsPantiesOn
	{
		get
		{
			if (!IsElementActive(ItemSlot.SlotManager.Panties.Id))
			{
				return IsElementActive(BikiniBottomId);
			}
			return true;
		}
	}

	public bool IsBraAsideOrOff
	{
		get
		{
			if (!IsBraAside)
			{
				return !IsBraOn;
			}
			return true;
		}
	}

	public virtual bool IsBraAside => false;

	public virtual bool IsBraOn
	{
		get
		{
			if (!IsElementActive(ItemSlot.SlotManager.Bra.Id))
			{
				return IsElementActive(BikiniTopId);
			}
			return true;
		}
	}

	public virtual bool IsSkirtAside => false;

	public virtual bool IsSkirtOn => IsElementActive(SkirtId);

	public bool IsSkirtAsideOrOff
	{
		get
		{
			if (!IsSkirtAside)
			{
				return !IsSkirtOn;
			}
			return true;
		}
	}

	public virtual bool IsPantsAside
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public virtual bool IsPantsOn => IsElementActive(PantsId);

	public bool IsPantsAsideOrOff
	{
		get
		{
			if (!IsPantsAside)
			{
				return !IsPantsOn;
			}
			return true;
		}
	}

	public virtual bool IsDress2Aside => false;

	public virtual bool IsDress2On => IsElementActive(Dress2Id);

	public bool IsDress2AsideOrOff
	{
		get
		{
			if (!IsDress2Aside)
			{
				return !IsDress2On;
			}
			return true;
		}
	}

	public virtual bool IsDress1Aside => false;

	public virtual bool IsDress1On => IsElementActive(Dress1Id);

	public bool IsDress1AsideOrOff
	{
		get
		{
			if (!IsDress1Aside)
			{
				return !IsDress1On;
			}
			return true;
		}
	}

	public bool IsBoobaVisible
	{
		get
		{
			if (IsBraAsideOrOff && IsShirtAsideOrOff && IsHoodieAsideOrOff && IsDress2AsideOrOff)
			{
				return IsDress1AsideOrOff;
			}
			return false;
		}
	}

	public Live2DToolSelector CurrentToolSelector { get; set; }

	protected Layer CurrentLayer => GetLayer(_currentLayerNumber);

	public virtual Vector3 DefaultFaceAngle => Vector3.zero;

	private Texture2D KissIndicatorTexture => SingletonMonoBehaviour<CursorSingleton>.Instance.GetCursorTexture(CursorSingleton.CursorType.Kiss);

	private Texture2D KissingIndicatorTexture => SingletonMonoBehaviour<CursorSingleton>.Instance.GetCursorTexture(CursorSingleton.CursorType.Kissing);

	public bool IsKissing => _kissingMainGameObject.activeSelf;

	private Live2DParameter[] DynamicParameters { get; set; }

	public Live2DExpression Expression { get; protected set; }

	[CubismParameterName("ParamBraEnabled", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter BraEnabledParameter { get; private set; }

	[CubismParameterName("ParamShirtEnabled", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter ShirtEnabledParameter { get; private set; }

	[CubismParameterName("ParamSkirtEnabled", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter SkirtEnabledParameter { get; set; }

	[CubismParameterName("ParamPantsEnabled", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter PantsEnabledParameter { get; set; }

	[CubismParameterName("ParamHoodieEnabled", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter HoodieEnabledParameter { get; set; }

	[CubismParameterName("ParamDress2Enabled", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter Dress2EnabledParameter { get; set; }

	[CubismParameterName("ParamPantiesEnabled", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter PantiesEnabledParameter { get; private set; }

	[CubismParameterName("ParamShoeLOn", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter ShoeLOnParameter { get; private set; }

	[CubismParameterName("ParamShoeROn", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter ShoeROnParameter { get; private set; }

	[CubismParameterName("ParamAttachmentArmL", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter ArmLParameter { get; private set; }

	[CubismParameterName("ParamAttachmentArmR", 1f, Live2DParameterUpdateTypeEnum.Smooth)]
	public Live2DParameter ArmRParameter { get; private set; }

	public virtual void InitializeGenerated()
	{
	}

	public void FireEvent(InteractionMemoryEventEnum e)
	{
		CurrentBrain.InteractionMemory.FireEvent(e);
	}

	private void EnsureModel()
	{
		if (object.Equals(Model, null))
		{
			Model = GetComponent<CubismModel>();
		}
	}

	public void FixedUpdateHologram()
	{
		hologramTimer += Time.fixedDeltaTime;
		if ((double)hologramTimer > 0.025)
		{
			hologramTimer = 0f;
			GroupColor(ColoringGroupEnum.Hologram, new Color(1f - UnityEngine.Random.value * 0.1f, 1f - UnityEngine.Random.value * 0.1f, 1f - UnityEngine.Random.value * 0.1f, 1f));
			hologramStatus = !hologramStatus;
		}
	}

	public Part GetCachedPart(CubismDrawable cubismDrawable)
	{
		if (_drawableToPart.TryGetValue(cubismDrawable, out var value))
		{
			return value;
		}
		value = new Part(cubismDrawable, this);
		InitPart(value);
		return value;
	}

	public Part GetCachedPart(CubismRenderer cubismRenderer)
	{
		if (_rendererToPart.TryGetValue(cubismRenderer, out var value))
		{
			return value;
		}
		value = new Part(cubismRenderer, this);
		InitPart(value);
		return value;
	}

	public Part GetCachedPart(string id)
	{
		if (_stringToPart.TryGetValue(id, out var value))
		{
			return value;
		}
		return null;
	}

	public bool TryGetCachedPart(string id, out Part part)
	{
		if (_stringToPart.TryGetValue(id, out part))
		{
			return true;
		}
		return false;
	}

	private void InitPart(Part part)
	{
		_rendererToPart.Add(part.CubismRenderer, part);
		_drawableToPart.Add(part.CubismDrawable, part);
		_stringToPart.Add(part.CubismDrawable.Id, part);
		_allParts.Add(part);
	}

	private void InitializeParts()
	{
		CubismDrawable[] drawables = Model.Drawables;
		foreach (CubismDrawable cubismDrawable in drawables)
		{
			InitPart(new Part(cubismDrawable, this));
		}
	}

	public void ToggleDrawable(string drawablename, bool value)
	{
		if (TryGetCachedPart(drawablename, out var part))
		{
			part.MeshRenderer.enabled = value;
		}
	}

	public void ToggleDrawable(string drawablename, bool value, Color? color)
	{
		Color color2 = color ?? Color.white;
		if (TryGetCachedPart(drawablename, out var part))
		{
			part.MeshRenderer.enabled = value;
			part.CubismRenderer.Color = color2;
		}
	}

	public CubismDrawable[] GetDrawables()
	{
		return Model.Drawables;
	}

	public List<CubismRenderer> GetRenderersInDrawOrder()
	{
		if (_sortedRenderersCached != null)
		{
			return _sortedRenderersCached;
		}
		_sortedRenderersCached = (from t in Model.Drawables
			select t.GetComponent<CubismRenderer>() into t
			orderby t.transform.position.z
			select t).ToList();
		return _sortedRenderersCached;
	}

	public IEnumerable<Part> GetParts()
	{
		return _allParts;
	}

	public void RestoreOriginalTextures()
	{
		foreach (TextureContainer texture in textures)
		{
			texture.RestoreOriginalTexture();
		}
	}

	private void InitializeTextures()
	{
		ILookup<Texture, Part> source = _allParts.ToLookup((Part t) => t.MainTexture);
		textures = (from t in source
			select new TextureContainer(t.ToList(), t.Key) into t
			orderby t.OriginalTexture.name
			select t).ToList();
	}

	private void ChangeCurrentlyEquipped(IEnumerable<Item> equippedItems)
	{
		_currentlyEquippedDict.Clear();
		_currentlyEquipped.ClearWithDispose();
		_slotToItems.ClearCachedLookup();
		foreach (Item equippedItem in equippedItems)
		{
			ItemWithCurrentData itemWithCurrentData = new ItemWithCurrentData(equippedItem);
			_currentlyEquippedDict.Add(equippedItem, itemWithCurrentData);
			_currentlyEquipped.Add(itemWithCurrentData);
			_slotToItems.AddToLookup(equippedItem.EquippedSlot, itemWithCurrentData);
		}
		FillIfEmpty(ItemSlot.SlotManager.Skin.Id, ItemEnum.DefaultSkin);
		FillIfEmpty(ItemSlot.SlotManager.HairMain.Id, ItemEnum.DefaultHair);
		FillIfEmpty(ItemSlot.SlotManager.Eyes.Id, ItemEnum.DefaultEyes);
		FillIfEmpty(ItemSlot.SlotManager.HeartPupils.Id, ItemEnum.DefaultHeartPupils);
		FillIfEmpty(ItemSlot.SlotManager.Flesh.Id, ItemEnum.DefaultFlesh);
		FillIfEmpty(ItemSlot.SlotManager.Teeth.Id, ItemEnum.DefaultTeeth);
		FillIfEmpty(ItemSlot.SlotManager.Hologram.Id, ItemEnum.DefaultHologram);
		void FillIfEmpty(string slotId, ItemEnum itemEnum)
		{
			if (!_slotToItems.ContainsKey(slotId))
			{
				Item item = Item.CreateDefaultItemForLive2DController(itemEnum.ToGameId());
				ItemWithCurrentData itemWithCurrentData2 = new ItemWithCurrentData(item);
				_currentlyEquippedDict.Add(item, itemWithCurrentData2);
				_currentlyEquipped.Add(itemWithCurrentData2);
				_slotToItems.AddToLookup(slotId, itemWithCurrentData2, cachedList: true);
			}
		}
	}

	public void ResetParameters()
	{
		CubismPart[] parts = Model.Parts;
		for (int i = 0; i < parts.Length; i++)
		{
			parts[i].Opacity = 1f;
		}
		Live2DParameter[] dynamicParameters = DynamicParameters;
		foreach (Live2DParameter obj in dynamicParameters)
		{
			obj.ResetToDefault();
			obj.FinishMove();
		}
	}

	private void ResetStage1()
	{
		DoManagersCallback((ILive2DControllerManager_ResetStage1 x) =>
		{
			x.ResetStage1();
		});
		ResetParameters();
		Mouth.CloseMouth();
		Live2DParameter[] dynamicParameters = DynamicParameters;
		for (int num = 0; num < dynamicParameters.Length; num++)
		{
			dynamicParameters[num].FinishMove();
		}
	}

	public virtual void ResetBasic()
	{
		Expression.ClearExpression();
	}

	public void ResetEverything()
	{
		InitializeAll();
		ResetStage1();
		ResetLimbUsages();
		UpdateTexturesFromCurrentlyEquipped(UpdateTexturesFromCurrentlyEquippedTypeEnum.Reset);
		AfterResetEverything();
	}

	protected virtual void AfterResetEverything()
	{
	}

	public void UpdateTexturesFromCurrentlyEquipped(UpdateTexturesFromCurrentlyEquippedTypeEnum type = UpdateTexturesFromCurrentlyEquippedTypeEnum.Active)
	{
		Item.ItemManager itemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
		ChangeCurrentlyEquipped(itemManager.AllEquippedItemsInEquipmentOrder());
		UpdateTextures();
		UpdateItemModifiersFromEquipped(type);
	}

	private void UpdateTextures()
	{
		string controllerId = ControllerEnum.GetControllerId();
		Dictionary<string, List<ItemWithCurrentData>> dictionary = new Dictionary<string, List<ItemWithCurrentData>>();
		foreach (ItemWithCurrentData item2 in _currentlyEquipped)
		{
			item2.UpdateData(controllerId);
		}
		foreach (ItemWithCurrentData item3 in _currentlyEquipped)
		{
			foreach (string activeElement in item3.ActiveElements)
			{
				dictionary.AddToLookup(activeElement, item3, cachedList: true);
			}
		}
		foreach (TextureContainer texture in textures)
		{
			texture.PrepareForDrawing();
		}
		Dictionary<Live2DParameter, bool> value;
		using (CollectionPool<Dictionary<Live2DParameter, bool>, KeyValuePair<Live2DParameter, bool>>.Get(out value))
		{
			foreach (Live2DControllerElement element in elements)
			{
				ItemWithCurrentData item = null;
				ItemWithCurrentData itemWithCurrentData = null;
				if (dictionary.TryGetValue(element.id, out var value2))
				{
					item = value2[0];
					itemWithCurrentData = value2.FirstOrDefault((ItemWithCurrentData x) => x.VanillaActive);
				}
				element.Activate(item, value);
				if (!element.IsActive || itemWithCurrentData == null)
				{
					continue;
				}
				foreach (var item4 in element.AllPartsOnTextureWithColor(this, itemWithCurrentData.Item))
				{
					var (color, _) = item4;
					item4.part.AddVanilla(itemWithCurrentData, color);
				}
			}
			foreach (var (live2DParameter2, minMaxBool) in value)
			{
				live2DParameter2.SetMinMaxBool(minMaxBool);
				live2DParameter2.FinishMove();
			}
			foreach (ItemWithCurrentData item5 in _currentlyEquipped)
			{
				if (item5.ActiveTextures == null)
				{
					continue;
				}
				foreach (PackedTexturesContainer.PackedTexture activeTexture in item5.ActiveTextures)
				{
					foreach (PackedTexturesContainer.PackedDrawable packedDrawable in activeTexture.PackedDrawables)
					{
						if (TryGetCachedPart(packedDrawable.Name, out var part))
						{
							part.AddLayers(item5, activeTexture, packedDrawable);
						}
					}
				}
			}
			_objectsReferencedByShaders.Clear();
			foreach (Part allPart in _allParts)
			{
				if (!allPart.TryGetVanillaLayer(out var layer))
				{
					continue;
				}
				Item relatedItem = layer.RelatedItem;
				if (relatedItem == null)
				{
					continue;
				}
				ItemPrefab itemPrefab = relatedItem.GetItemPrefab();
				if (itemPrefab.CustomShaderType == ItemPrefab.CustomShaderTypeEnum.RelatedDrawables)
				{
					IReference<Shader> customShader = itemPrefab.CustomShader;
					Action<Material, List<UnityEngine.Object>> customShaderMaterialPostProcessing = itemPrefab.CustomShaderMaterialPostProcessing;
					if (customShader != null && !customShader.IsNull())
					{
						allPart.SetShader(customShader.GetResource(), customShaderMaterialPostProcessing, _objectsReferencedByShaders);
					}
				}
			}
			ItemWithCurrentData itemWithCurrentData2 = _currentlyEquipped.FirstOrDefault((ItemWithCurrentData x) => x.Prefab.CustomShaderType == ItemPrefab.CustomShaderTypeEnum.Hair);
			if (itemWithCurrentData2 != null)
			{
				IReference<Shader> customShader2 = itemWithCurrentData2.Prefab.CustomShader;
				Action<Material, List<UnityEngine.Object>> customShaderMaterialPostProcessing2 = itemWithCurrentData2.Prefab.CustomShaderMaterialPostProcessing;
				if (customShader2 != null && !customShader2.IsNull())
				{
					ColoringGroupEnum[] allHairNonAccessoryColoringGroups = AllHairNonAccessoryColoringGroups;
					foreach (ColoringGroupEnum coloringGroupEnum in allHairNonAccessoryColoringGroups)
					{
						foreach (Part part2 in GetGroup(coloringGroupEnum).Parts)
						{
							if (part2.Layers.Count > 0)
							{
								part2.SetShader(customShader2.GetResource(), customShaderMaterialPostProcessing2, _objectsReferencedByShaders);
							}
						}
					}
				}
			}
			foreach (Part part3 in GetGroup(ColoringGroupEnum.Moddable).Parts)
			{
				part3.SetActive(part3.Layers.Count > 0);
			}
			FillGroup(ColoringGroupEnum.Rest, Color.white);
			FillGroup(ColoringGroupEnum.MCSkin, Color.white);
			FillGroup(ColoringGroupEnum.MCCockSkin, Color.white);
			FillGroup(ColoringGroupEnum.MCCockTip, ColorMagic(new Color32(255, 182, 207, 255), 1.1f));
			FillGroup(ColoringGroupEnum.Eyelash, Color.white);
			FillGroup(ColoringGroupEnum.Cum, Color.white);
			FillGroup(ColoringGroupEnum.Blush, Color.white);
			FillGroup(ColoringGroupEnum.Shadow, Color.white);
			FillGroup(ColoringGroupEnum.Highlight, Color.white);
			FillGroup(ColoringGroupEnum.HitArea, Color.white);
			FillGroup(ColoringGroupEnum.Mask, Color.white);
			FillGroup(ColoringGroupEnum.Moddable, Color.white);
			FillGroup(ColoringGroupEnum.OtherAttachments, Color.white);
			if ((bool)OptionsStatic.LowMemoryModeEnabled)
			{
				int num2 = 1;
				bool flag2 = false;
				int lowMemoryDivisionLevelPow = OptionsStatic.LowMemoryDivisionLevelPow;
				RenderTextureDescriptor desc = new RenderTextureDescriptor(4096 / lowMemoryDivisionLevelPow, 4096 / lowMemoryDivisionLevelPow, RenderTextureFormat.ARGB32, 0, num2, RenderTextureReadWrite.Linear);
				desc.useMipMap = flag2;
				desc.autoGenerateMips = false;
				RenderTexture temporary = RenderTexture.GetTemporary(desc);
				using (new ImageUtility.UsingTemporaryRenderTexture(temporary))
				{
					foreach (TextureContainer texture2 in textures)
					{
						if (!texture2.IsSizeValid(4096, 4096) || !texture2.NeedsRedrawing())
						{
							continue;
						}
						if (!texture2.NeedsAnyDrawing())
						{
							texture2.RestoreOriginalTexture();
							continue;
						}
						texture2.ClearAndDrawAllParts();
						if (flag2)
						{
							temporary.GenerateMips();
						}
						Texture2D texture2D;
						if (texture2.CurrentTexture != texture2.OriginalTexture)
						{
							texture2D = texture2.CurrentTexture as Texture2D;
							if ((object)texture2D != null && texture2D.mipmapCount == num2)
							{
								goto IL_068e;
							}
						}
						texture2D = new Texture2D(4096 / lowMemoryDivisionLevelPow, 4096 / lowMemoryDivisionLevelPow, TextureFormat.RGBA32, num2, linear: true, createUninitialized: true)
						{
							ignoreMipmapLimit = true,
							mipMapBias = -0.5f
						};
						goto IL_068e;
						IL_068e:
						ImageUtility.ActiveRenderTextureToTextureGpuSide(temporary, texture2D);
						texture2.SetTexture(texture2D);
					}
				}
			}
			else
			{
				RenderTexture active = RenderTexture.active;
				foreach (TextureContainer texture3 in textures)
				{
					if (!texture3.IsSizeValid(4096, 4096) || !texture3.NeedsRedrawing())
					{
						continue;
					}
					if (!texture3.NeedsAnyDrawing())
					{
						texture3.RestoreOriginalTexture();
						continue;
					}
					if (!texture3.TryGetRenderTexture(out var renderTexture))
					{
						renderTexture = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.GetRenderTexture();
					}
					RenderTexture.active = renderTexture;
					texture3.ClearAndDrawAllParts();
					renderTexture.GenerateMips();
					texture3.SetTexture(renderTexture);
				}
				RenderTexture.active = active;
			}
			AfterUpdateTextures();
			dictionary.ClearCachedLookup();
		}
		void FillGroup(ColoringGroupEnum groupName, Color color2)
		{
			foreach (Part part4 in GetGroup(groupName).Parts)
			{
				if (!part4.NeedsToBeDrawn())
				{
					part4.AddVanilla(null, color2);
				}
			}
		}
	}

	protected virtual void AfterUpdateTextures()
	{
	}

	public List<Part> GetAllItemParts(Item item)
	{
		ItemPrefab itemPrefab = item.GetItemPrefab();
		List<Part> list = new List<Part>();
		if (!itemPrefab.IsEquipable(out var slotEquipData))
		{
			return list;
		}
		List<string> list2;
		using (slotEquipData.FillActiveElementsDefaultPooled(out list2))
		{
			foreach (string item3 in list2)
			{
				if (elements.TryGetValue(item3, out var item2))
				{
					list.AddRange(item2.AllPartsNonSegmented(this));
				}
			}
			string controllerId = ControllerEnum.GetControllerId();
			PackedTexturesContainer[] array = itemPrefab.Textures;
			for (int i = 0; i < array.Length; i++)
			{
				foreach (PackedTexturesContainer.PackedDrawable item4 in array[i].GetPackedDrawablesByTarget(controllerId))
				{
					if (TryGetCachedPart(item4.Name, out var part))
					{
						list.Add(part);
					}
				}
			}
			return list;
		}
	}

	private Bounds GetPartsBounds(List<Part> parts)
	{
		Bounds bounds = parts[0].CubismRenderer.MeshRenderer.bounds;
		for (int i = 1; i < parts.Count; i++)
		{
			bounds.Encapsulate(parts[i].CubismRenderer.MeshRenderer.bounds);
		}
		return bounds;
	}

	public bool TryEstimateItemWorldBounds(Item item, out Bounds bounds)
	{
		List<Part> allItemParts = GetAllItemParts(item);
		if (allItemParts.Count == 0)
		{
			bounds = default;
			return false;
		}
		bounds = GetPartsBounds(allItemParts);
		return true;
	}

	public IEnumerator RenderCoroutine(Item item, Action<RenderTexture> OnFinished)
	{
		List<Part> parts = GetAllItemParts(item);
		if (parts.Count == 0)
		{
			OnFinished?.Invoke(null);
			yield break;
		}
		ResetStage1();
		ChangeCurrentlyEquipped(new Item[1] { item });
		UpdateTextures();
		Bounds bounds = GetPartsBounds(parts);
		GameObject gameObject = new GameObject();
		Camera cameraComponent = gameObject.AddComponent<Camera>();
		cameraComponent.enabled = false;
		gameObject.SetActive(value: false);
		cameraComponent.orthographic = true;
		cameraComponent.aspect = 1f;
		cameraComponent.clearFlags = CameraClearFlags.Color;
		cameraComponent.backgroundColor = Color.clear;
		cameraComponent.targetTexture = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.GetRenderTexture();
		cameraComponent.cullingMask = LayerMask.GetMask("TemporaryRenderTarget");
		cameraComponent.forceIntoRenderTexture = true;
		yield return null;
		RenderTexture obj = (RenderTexture.active = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.GetRenderTexture());
		int layer = LayerMask.NameToLayer("TemporaryRenderTarget");
		foreach (Part item2 in parts)
		{
			item2.MeshRenderer.gameObject.layer = layer;
		}
		cameraComponent.MoveCameraToWorldBounds(bounds, UiOverlay.Instance.MainCamera.transform.position.z);
		cameraComponent.Render();
		RenderTexture.active = null;
		foreach (Part item3 in parts)
		{
			item3.MeshRenderer.gameObject.layer = 0;
		}
		OnFinished?.Invoke(obj);
	}

	public void PrepareForDialogue()
	{
		EndKiss();
		LetGoOfCurrentlyHeld();
	}

	private void InitializeCumManagers()
	{
		CumOutsideManager = RegisterManager(new CumOutsideManager(this));
		CumOutsideManager.ClearAllCum();
		CumInsideManager = RegisterManager(new CumManagerCommon(this));
	}

	public void SaveSerializedState()
	{
		Debug.Log("Saving serialized state " + name + "!");
		GameVariables current = GameVariables.Current;
		if (current != null)
		{
			SaveSerializableState_Inner(current.botLive2DCommonState);
		}
		else
		{
			Debug.LogError("Trying to save serializable state without game started!");
		}
	}

	protected virtual void SaveSerializableState_Inner(Live2DControllerCommonState state)
	{
		DoManagersCallback((ILive2DControllerManager_SerializedState x, Live2DControllerCommonState s) =>
		{
			x.SaveState(s);
		}, state);
	}

	public void LoadSerializedState()
	{
		Debug.Log("Loading serialized state " + name + "!");
		GameVariables current = GameVariables.Current;
		if (current != null)
		{
			LoadSerializableState_Inner(current.botLive2DCommonState);
			FinishAllMoves();
		}
		else
		{
			Debug.LogError("Trying to load serializable state without game started!");
		}
	}

	protected virtual void LoadSerializableState_Inner(Live2DControllerCommonState state)
	{
		DoManagersCallback((ILive2DControllerManager_SerializedState x, Live2DControllerCommonState s) =>
		{
			x.LoadState(s);
		}, state);
	}

	protected T RegisterManager<T>(T manager) where T : ILive2DControllerManager
	{
		if (manager == null)
		{
			Debug.LogError("Cannot register null manager.");
			return manager;
		}
		if (_registeredManagers.Contains(manager))
		{
			Debug.LogError("Manager " + manager.GetType().Name + " is already registered.");
			return manager;
		}
		_registeredManagers.Add(manager);
		return manager;
	}

	protected virtual void InitializeManagersInherited()
	{
	}

	private void DoManagersCallback<T>(Action<T> callBack) where T : ILive2DControllerManager_Callback
	{
		for (int i = 0; i < _registeredManagers.Count; i++)
		{
			if (_registeredManagers[i] is T obj)
			{
				callBack(obj);
			}
		}
	}

	private void DoManagersCallback<T, TParam1>(Action<T, TParam1> callBack, TParam1 param1) where T : ILive2DControllerManager_Callback
	{
		for (int i = 0; i < _registeredManagers.Count; i++)
		{
			if (_registeredManagers[i] is T arg)
			{
				callBack(arg, param1);
			}
		}
	}

	public void InitializeAll()
	{
		if (!IsFullyInitialized)
		{
			IsFullyInitialized = true;
			EnsureModel();
			Model.GetComponent<CubismRenderController>().TryInitializeRenderers();
			ResetLayers();
			gameObject.DisableAndDestroyBehaviour<CubismPoseController>();
			gameObject.DisableAndDestroyBehaviour<CubismFadeController>();
			gameObject.DisableAndDestroyBehaviour<Animator>();
			InitializeParts();
			InitializeExpression();
			InitializeTextures();
			InitializeParameters();
			InitializeGenerated();
			InitializeGroups();
			InitializeElements();
			InitializeEyes();
			InitializeMouth();
			InitializeManagersInherited();
			InitializeDefaultInteractable();
			InitializeCumManagers();
			InitializeInherited();
			if (this is ILive2DController_Manager_XRay { XRayManager: null })
			{
				Debug.LogError("XRayManager not initalized in " + name + "!");
			}
		}
	}

	private void InitializeExpression()
	{
		Expression = RegisterManager(new Live2DExpression(this));
	}

	protected virtual void InitializeInherited()
	{
	}

	public void SetEnabled(bool v)
	{
		enabled = v;
	}

	public void BeforeDestroy()
	{
		RestoreOriginalTextures();
		if (CurrentBrain != null)
		{
			CurrentBrain.MoveToStorage();
			CurrentBrain = null;
		}
	}

	public void EnsureBrain(string characterId)
	{
		if (CurrentBrain == null)
		{
			ModelBrain modelBrain = ModelBrain.CreateOrGet(characterId);
			modelBrain.MakeSureInitialized();
			modelBrain.SwitchToController(this);
		}
		else
		{
			CurrentBrain.EnableBrain();
		}
	}

	public CancellationToken GetUsageCancellationToken()
	{
		if (_currentUser == null)
		{
			Debug.LogError("Getting usage cancellation token when controller isn't used! (This shouldn't happen!)");
		}
		if (_usageCancellationTokenSource == null)
		{
			_usageCancellationTokenSource = new CancellationTokenSource();
		}
		return _usageCancellationTokenSource.Token;
	}

	public void StartUsage(string characterId, ILive2DModelUser user)
	{
		if (_currentUser != null)
		{
			Debug.LogError($"Start usage called when already used! {_currentUser} => {user}");
		}
		_currentUser = user;
		Debug.Log($"{name} started usage by {user}");
		InitializeAll();
		Live2DModelUsageTypeEnum live2DModelUsage = user.Live2DModelUsage;
		if (live2DModelUsage != Live2DModelUsageTypeEnum.CodeOnly && live2DModelUsage == Live2DModelUsageTypeEnum.Gameplay)
		{
			EnsureBrain(characterId);
			DoManagersCallback((ILive2DControllerManager_StartUsageGameplay x) =>
			{
				x.StartUsageGameplay();
			});
			OnStartUsageGameplayInherited(characterId, user);
			LoadSerializedState();
		}
	}

	protected virtual void OnStartUsageGameplayInherited(string characterId, ILive2DModelUser user)
	{
	}

	public void EndUsage(ILive2DModelUser user)
	{
		if (_currentUser != user)
		{
			Debug.LogError($"End usage called for a wrong user {user} instead of {_currentUser}");
		}
		_currentUser = null;
		Debug.Log($"{name} ended usage by {user}");
		CancellationTokenSource usageCancellationTokenSource = _usageCancellationTokenSource;
		_usageCancellationTokenSource = null;
		if (usageCancellationTokenSource != null)
		{
			usageCancellationTokenSource.Cancel();
			usageCancellationTokenSource.Dispose();
		}
		DoManagersCallback((ILive2DControllerManager_EndUsage x) =>
		{
			x.EndUsage();
		});
		OnEndUsageInherited(user);
	}

	protected virtual void OnEndUsageInherited(ILive2DModelUser previousUser)
	{
		Live2DModelUsageTypeEnum live2DModelUsage = previousUser.Live2DModelUsage;
		if (live2DModelUsage != Live2DModelUsageTypeEnum.CodeOnly && live2DModelUsage == Live2DModelUsageTypeEnum.Gameplay)
		{
			SaveSerializedState();
		}
	}

	public void Awake()
	{
		InitializeAll();
	}

	public void Start()
	{
		PrepareKissing();
	}

	public void OnDisable()
	{
		DoManagersCallback((ILive2DControllerManager_OnDisable x) =>
		{
			x.OnDisable();
		});
		ResetLimbUsages();
		LetGoOfCurrentlyHeld();
		OnDisableKissing();
		StopAllCoroutines();
	}

	public void Update()
	{
		foreach (TextureContainer texture in textures)
		{
			if (!texture.CheckIfRenderTextureIsValid())
			{
				RestoreOriginalTextures();
				UpdateTexturesFromCurrentlyEquipped();
				break;
			}
		}
		UpdateInteractables();
		UpdateKissing();
		UpdateLimbUsages();
		DoManagersCallback((ILive2DControllerManager_Update x) =>
		{
			x.Update();
		});
	}

	public void OnLateUpdate()
	{
		Live2DParameter[] dynamicParameters = DynamicParameters;
		for (int i = 0; i < dynamicParameters.Length; i++)
		{
			dynamicParameters[i].LateUpdate();
		}
	}

	public void FixedUpdate()
	{
		FixedUpdateItemModifier();
		FixedUpdateHologram();
		DoManagersCallback((ILive2DControllerManager_FixedUpdate x) =>
		{
			x.FixedUpdate();
		});
		FixedUpdateInteractables();
		FixedUpdateLimbUsages();
	}

	private void InitializeEyes()
	{
		Eyes = RegisterManager(InitializeEyesImpl());
	}

	protected abstract EyeManagerCommon InitializeEyesImpl();

	private void InitializeMouth()
	{
		Mouth = RegisterManager(InitializeMouthImpl());
	}

	protected abstract MouthManagerDefaultMouth InitializeMouthImpl();

	[NotNull]
	public abstract Part GetApproximateEyesPositionPart();

	[NotNull]
	public abstract Part GetApproximateMouthPositionPart();

	public virtual Vector3 GetMouthOffset()
	{
		return new Vector3(-100f, 50f, 0f);
	}

	public void GroupColor(ColoringGroupEnum group, Color color)
	{
		if (coloringGroups.TryGetValue(group, out var value))
		{
			value.Color(color);
		}
		else
		{
			Debug.Log($"Tried to color group {group}. Such group wasn't found!");
		}
	}

	public ICollection<KeyValuePair<ColoringGroupEnum, ColoringGroup>> GetGroups()
	{
		return coloringGroups;
	}

	public ColoringGroup GetGroup(ColoringGroupEnum group)
	{
		return coloringGroups[group];
	}

	private static ColoringGroupEnum DetermineGroup(string partName)
	{
		if (string.IsNullOrEmpty(partName))
		{
			return ColoringGroupEnum.Rest;
		}
		if (partName.StartsWith("SkinBoob", StringComparison.Ordinal) || partName.StartsWith("SkinBreast", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Boobs;
		}
		if (partName.StartsWith("SkinButt", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Ass;
		}
		if (partName.StartsWith("SkinVagina", StringComparison.Ordinal) || partName.StartsWith("SkinPelvis", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Pelvic;
		}
		if (partName.StartsWith("SkinLeg", StringComparison.Ordinal) || partName.StartsWith("Thigh", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Thigh;
		}
		if (partName.StartsWith("SkinTorsoLower", StringComparison.Ordinal) || partName.StartsWith("SkinWaistLower", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Tummy;
		}
		if (partName.StartsWith("Skin", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Skin;
		}
		if (partName.StartsWith("H0_Top", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair0_Top;
		}
		if (partName.StartsWith("H0_Back", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair0_Back;
		}
		if (partName.StartsWith("H0_Braid", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair0_Braid;
		}
		if (partName.StartsWith("H0_HairTie", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair0_Braid_HairTieAccessory;
		}
		if (partName.StartsWith("H0_SideAlt", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair0_SideAlt;
		}
		if (partName.StartsWith("H0_Side", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair0_Side;
		}
		if (partName.StartsWith("H0_FrontClippedUp_HairClip", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair0_FrontClippedUp_HairClip;
		}
		if (partName.StartsWith("H0_FrontClippedUp_Bang", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair0_FrontClippedUp_Bang;
		}
		if (partName.StartsWith("H0_Front", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair0_Front;
		}
		if (partName.StartsWith("H1_Top", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair1_Top;
		}
		if (partName.StartsWith("H1_Back", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair1_Back;
		}
		if (partName.StartsWith("H1_Front", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair1_Front;
		}
		if (partName.StartsWith("H1_Side", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair1_Side;
		}
		if (partName.StartsWith("H2_Front", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair2_Front;
		}
		if (partName.StartsWith("H3_Top", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair3_Top;
		}
		if (partName.StartsWith("H3_Back", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair3_Back;
		}
		if (partName.StartsWith("H3_Front", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair3_Front;
		}
		if (partName.StartsWith("H3_Side", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair3_Side;
		}
		if (partName.StartsWith("H4_Knot", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair4_KnotAccessory;
		}
		if (partName.StartsWith("H4_Tail", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hair4_Ponytail;
		}
		if (partName.StartsWith("Cum", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Cum;
		}
		if (partName.StartsWith("Skirt", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.Skirt;
		}
		if (partName.StartsWith("Plainshirt", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.Shirt;
		}
		if (partName.StartsWith("ShadowPlainshirt", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.ShirtShadow;
		}
		if (partName.StartsWith("StockingL", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.StockingL;
		}
		if (partName.StartsWith("StockingR", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.StockingR;
		}
		if (partName.StartsWith("Panties", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.Panties;
		}
		if (partName.StartsWith("LongHoodie", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Hoodie;
		}
		if (partName.StartsWith("AltLongHoodie", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.HoodieStrings;
		}
		if (partName.StartsWith("BPants", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Pants;
		}
		if (partName.StartsWith("AltBPants", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.PantsBelt;
		}
		if (partName.StartsWith("PupilL", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.IrisL;
		}
		if (partName.StartsWith("Pupil", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.IrisR;
		}
		if (partName.StartsWith("Iris1L", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.PupilL;
		}
		if (partName.StartsWith("Iris1", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.PupilR;
		}
		if (partName.StartsWith("EyeBall", StringComparison.Ordinal) || partName.StartsWith("EyeWhite", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Sclera;
		}
		if (partName.StartsWith("Eyelash", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.Eyelash;
		}
		if (partName.StartsWith("Teeth", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Teeth;
		}
		if (partName.StartsWith("SharpTeeth", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.SharpTeeth;
		}
		if (partName.StartsWith("TailMain", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Tail;
		}
		if (partName.StartsWith("TailPlug", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.TailPlug;
		}
		if (partName.StartsWith("Bra", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.Bra;
		}
		if (partName.StartsWith("Iris2", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.HeartPupils;
		}
		if (partName.Contains("Brow", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.EyeBrows;
		}
		if (partName.StartsWith("CatEarFront", StringComparison.OrdinalIgnoreCase) || partName.StartsWith("CatEarBack", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.CatEarMain;
		}
		if (partName.StartsWith("CatEarMid", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.CatEarSecondary;
		}
		if (partName.Contains("Blush", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.Blush;
		}
		if (partName.StartsWith("InnerMouth", StringComparison.Ordinal) || partName.StartsWith("Tounge", StringComparison.Ordinal) || partName.StartsWith("Tongue", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Mouth;
		}
		if (partName.StartsWith("AttachLegLFoot", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.LegLFoot;
		}
		if (partName.StartsWith("AttachLegL", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.LegL;
		}
		if (partName.StartsWith("AttachLegRFoot", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.LegRFoot;
		}
		if (partName.StartsWith("AttachLegR", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.LegR;
		}
		if (partName.StartsWith("AttachArmL", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.ArmL;
		}
		if (partName.StartsWith("AttachArmR", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.ArmR;
		}
		if (partName.StartsWith("MC", StringComparison.Ordinal) || partName.StartsWith("headpet", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.MCSkin;
		}
		if (partName.StartsWith("DickSkin", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.MCCockSkin;
		}
		if (partName.StartsWith("DickMain", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.MCCockTip;
		}
		if (partName.StartsWith("Nipple", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Nipples;
		}
		if (partName.StartsWith("Vagina", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Vagina;
		}
		if (partName.StartsWith("Xray", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Xray;
		}
		if (partName.StartsWith("Anus", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Anus;
		}
		if (partName.StartsWith("HairHologram", StringComparison.Ordinal) || partName.StartsWith("Hologram"))
		{
			return ColoringGroupEnum.Hologram;
		}
		if (partName.StartsWith("CuteBow", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.CuteBow;
		}
		if (partName.StartsWith("Dress1", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Dress1;
		}
		if (partName.StartsWith("Dress2Alt", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Dress2Alt;
		}
		if (partName.StartsWith("Dress2", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Dress2;
		}
		if (partName.StartsWith("Shoe_R", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.ShoeR;
		}
		if (partName.StartsWith("Shoe_L", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.ShoeL;
		}
		if (partName.StartsWith("PointyEar", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.PointyEars;
		}
		if (partName.StartsWith("Moddable", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Moddable;
		}
		if (partName.StartsWith("HitArea", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.HitArea;
		}
		if (partName.StartsWith("WizardHat", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.WizardHat;
		}
		if (partName.StartsWith("BellChoker", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.BellChoker;
		}
		if (partName.StartsWith("PumpkinHairPin", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.PumpkinHairPin;
		}
		if (partName.StartsWith("Headband", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.Headband;
		}
		if (partName.StartsWith("NipPiercingLeft", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.NipplePiercingL;
		}
		if (partName.StartsWith("NipPiercingRight", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.NipplePiercingR;
		}
		if (partName.StartsWith("NavelPiercing", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.NavelPiercing;
		}
		if (partName.Contains("Shadow", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.Shadow;
		}
		if (partName.Contains("Highlight", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.Highlight;
		}
		if (partName.Contains("Mask", StringComparison.OrdinalIgnoreCase))
		{
			return ColoringGroupEnum.Mask;
		}
		if (partName.StartsWith("Attach", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.OtherAttachments;
		}
		if (partName.StartsWith("BikiniTop_A", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.BikiniTop_Alt;
		}
		if (partName.StartsWith("BikiniTop_M", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.BikiniTop_Main;
		}
		if (partName.StartsWith("BikiniBot_A", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.BikiniBottom_Alt;
		}
		if (partName.StartsWith("BikiniBot_M", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.BikiniBottom_Main;
		}
		if (partName.StartsWith("BikiniBot_U", StringComparison.Ordinal))
		{
			return ColoringGroupEnum.BikiniBottom_Uncolored;
		}
		return ColoringGroupEnum.Rest;
	}

	private void InitializeGroups()
	{
		coloringGroups[ColoringGroupEnum.Tail].PrepareAsSkinning("TailMain");
		coloringGroups[ColoringGroupEnum.BikiniBottom_Uncolored].PrepareAsSkinning("BikiniBot_U_Main");
	}

	public static Color ColorMagicV2(Color color, Color min, Color max)
	{
		Color color2 = max - min;
		return (min + color2 * color).SetAlpha(color.a);
	}

	public static Color ColorMagic(Color color, float strength = 1f)
	{
		return (color * strength + new Color(0.05f, 0.05f, 0.05f)).SetAlpha(color.a);
	}

	public IEnumerable<Live2DControllerElement> GetAllElements()
	{
		return elements;
	}

	public Live2DControllerElement GetElementById(string id)
	{
		if (elements.TryGetValue(id, out var item))
		{
			return item;
		}
		return null;
	}

	public bool IsElementActive(string id)
	{
		if (elements.TryGetValue(id, out var item))
		{
			return item.IsActive;
		}
		return false;
	}

	public bool IsAnySubElementActive(string id)
	{
		return elements.FirstOrDefault((Live2DControllerElement x) => x.id.StartsWith(id))?.IsActive ?? false;
	}

	protected virtual bool CanBeRemoved(string id)
	{
		return true;
	}

	private void InitializeElements()
	{
		ColorScaler colorScaler = new ColorScaler(239.ToColor());
		ColorScaler colorScaler2 = new ColorScaler(102.ToColor());
		ColorScaler colorScaler3 = new ColorScaler(90.ToColor());
		ColorScaler colorScaler4 = new ColorScaler(222.ToColor());
		ColorScaler colorScaler5 = new ColorScaler(119.ToColor());
		ColorScaler colorScaler6 = new ColorScaler(251.ToColor());
		ColorScaler colorScaler7 = new ColorScaler(223.ToColor());
		ColorScaler colorScaler8 = new ColorScaler(240.ToColor());
		ColorScaler colorScaler9 = new ColorScaler(220.ToColor());
		ColorScaler colorScaler10 = new ColorScaler(187.ToColor());
		ColorScaler colorScaler11 = new ColorScaler(203.ToColor());
		ColorScaler colorScaler12 = new ColorScaler(1.1f);
		elements = new ProjectedKeyCollection<string, Live2DControllerElement>((Live2DControllerElement t) => t.id)
		{
			new Live2DControllerElement(ItemSlot.SlotManager.Skin.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler, ColoringGroupEnum.Skin, ColoringGroupEnum.Boobs, ColoringGroupEnum.Ass, ColoringGroupEnum.Pelvic, ColoringGroupEnum.Thigh, ColoringGroupEnum.Tummy, ColoringGroupEnum.Anus)),
			new Live2DControllerElement("Eyebrows_BasicEyebrows", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.EyeBrows)),
			new Live2DControllerElement("Hair0_Top", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, default(ColoringGroupEnum))),
			new Live2DControllerElement("Hair0_Front", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair0_Front)),
			new Live2DControllerElement("Hair0_FrontClippedUp", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair0_FrontClippedUp_Bang), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(2, ColorScaler.DefaultColorScaler, ColoringGroupEnum.Hair0_FrontClippedUp_HairClip)),
			new Live2DControllerElement("Hair0_Side", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair0_Side), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, ColorScaler.DefaultColorScaler, ColoringGroupEnum.Hair0_SideAlt)),
			new Live2DControllerElement("Hair0_Back", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair0_Back)),
			new Live2DControllerElement("Hair0_Braid", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair0_Braid), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(2, colorScaler12, ColoringGroupEnum.Hair0_Braid_HairTieAccessory)),
			new Live2DControllerElement("Hair1_Top", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair1_Top)),
			new Live2DControllerElement("Hair1_Front", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair1_Front)),
			new Live2DControllerElement("Hair1_Side", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair1_Side)),
			new Live2DControllerElement("Hair1_Back", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair1_Back)),
			new Live2DControllerElement("Hair2_Front", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair2_Front)),
			new Live2DControllerElement("Hair3_Top", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair3_Top)),
			new Live2DControllerElement("Hair3_Front", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair3_Front)),
			new Live2DControllerElement("Hair3_Side", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair3_Side)),
			new Live2DControllerElement("Hair3_Back", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair3_Back)),
			new Live2DControllerElement("Hair4_Ponytail", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler12, ColoringGroupEnum.Hair4_Ponytail)),
			new Live2DControllerElement("Hair4_Knot", Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, colorScaler12, ColoringGroupEnum.Hair4_KnotAccessory)),
			new Live2DControllerElement(ItemSlot.SlotManager.Eyes.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(0, colorScaler4, ColoringGroupEnum.PupilL), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, colorScaler4, ColoringGroupEnum.PupilR), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(2, colorScaler5, ColoringGroupEnum.IrisL), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(3, colorScaler5, ColoringGroupEnum.IrisR), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(4, colorScaler6, ColoringGroupEnum.Sclera)),
			new Live2DControllerElement(ItemSlot.SlotManager.HeartPupils.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(new ColorScaler(1.1f), ColoringGroupEnum.HeartPupils)),
			new Live2DControllerElement(ItemSlot.SlotManager.Flesh.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(0, colorScaler10, ColoringGroupEnum.Mouth), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, colorScaler9, ColoringGroupEnum.Nipples), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(2, new ColorScaler(1.5f), ColoringGroupEnum.Vagina), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(2, new ColorScaler(1.7f), ColoringGroupEnum.Xray)),
			new Live2DControllerElement(CatEarsElementId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler7, ColoringGroupEnum.CatEarMain), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, colorScaler8, ColoringGroupEnum.CatEarSecondary)),
			new Live2DControllerElement(WizardHatElementId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.WizardHat)),
			new Live2DControllerElement(ItemSlot.SlotManager.Ears.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler, ColoringGroupEnum.PointyEars)),
			new Live2DControllerElement(ItemSlot.SlotManager.LegL.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler3, ColoringGroupEnum.LegL, ColoringGroupEnum.LegLFoot)),
			new Live2DControllerElement(ItemSlot.SlotManager.LegR.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler3, ColoringGroupEnum.LegR, ColoringGroupEnum.LegRFoot)),
			new Live2DControllerElement(ItemSlot.SlotManager.ArmL.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(colorScaler2, ArmLParameter, ColoringGroupEnum.ArmL)),
			new Live2DControllerElement(ItemSlot.SlotManager.ArmR.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(colorScaler2, ArmRParameter, ColoringGroupEnum.ArmR)),
			new Live2DControllerElement(ShirtId, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(ColorScaler.DefaultColorScaler, ShirtEnabledParameter, ColoringGroupEnum.Shirt), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(-1, ColorScaler.DefaultColorScaler, ColoringGroupEnum.ShirtShadow)),
			new Live2DControllerElement(SkirtId, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(ColorScaler.DefaultColorScaler, SkirtEnabledParameter, ColoringGroupEnum.Skirt)),
			new Live2DControllerElement(HoodieId, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(ColorScaler.DefaultColorScaler, HoodieEnabledParameter, ColoringGroupEnum.Hoodie), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, ColorScaler.DefaultColorScaler, ColoringGroupEnum.HoodieStrings)),
			new Live2DControllerElement(Dress1Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.Dress1)),
			new Live2DControllerElement(Dress2Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(ColorScaler.DefaultColorScaler, Dress2EnabledParameter, ColoringGroupEnum.Dress2), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, ColorScaler.DefaultColorScaler, ColoringGroupEnum.Dress2Alt)),
			new Live2DControllerElement(PantsId, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(ColorScaler.DefaultColorScaler, PantsEnabledParameter, ColoringGroupEnum.Pants), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, ColorScaler.DefaultColorScaler, ColoringGroupEnum.PantsBelt)),
			new Live2DControllerElement(ItemSlot.SlotManager.Bra.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(ColorScaler.DefaultColorScaler, BraEnabledParameter, ColoringGroupEnum.Bra)),
			new Live2DControllerElement(ItemSlot.SlotManager.StockingL.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.StockingL)),
			new Live2DControllerElement(ItemSlot.SlotManager.StockingR.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.StockingR)),
			new Live2DControllerElement(ItemSlot.SlotManager.ShoeL.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameterForceToggle(ColorScaler.DefaultColorScaler, ShoeLOnParameter, ColoringGroupEnum.ShoeL)),
			new Live2DControllerElement(ItemSlot.SlotManager.ShoeR.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameterForceToggle(ColorScaler.DefaultColorScaler, ShoeROnParameter, ColoringGroupEnum.ShoeR)),
			new Live2DControllerElement(ItemSlot.SlotManager.Panties.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(ColorScaler.DefaultColorScaler, PantiesEnabledParameter, ColoringGroupEnum.Panties)),
			new Live2DControllerElement(BikiniBottomId, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(ColorScaler.DefaultColorScaler, PantiesEnabledParameter, ColoringGroupEnum.BikiniBottom_Main), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, ColorScaler.DefaultColorScaler, ColoringGroupEnum.BikiniBottom_Alt), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(-1, ColorScaler.DefaultColorScaler, ColoringGroupEnum.BikiniBottom_Uncolored)),
			new Live2DControllerElement(BikiniTopId, Live2DControllerElement.ElementColoringGroup.CreateInstanceParameter(ColorScaler.DefaultColorScaler, PantiesEnabledParameter, ColoringGroupEnum.BikiniTop_Main), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(1, ColorScaler.DefaultColorScaler, ColoringGroupEnum.BikiniTop_Alt)),
			new Live2DControllerElement(CuteBowElementId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.CuteBow)),
			new Live2DControllerElement(PumpkinHairpinId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.PumpkinHairPin)),
			new Live2DControllerElement(HeadbandId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.Headband)),
			new Live2DControllerElement(NavelPiercingId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.NavelPiercing)),
			new Live2DControllerElement(NippleLeftPiercingId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.NipplePiercingL)),
			new Live2DControllerElement(NippleRightPiercingId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.NipplePiercingR)),
			new Live2DControllerElement(ItemSlot.SlotManager.Teeth.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.Teeth)),
			new Live2DControllerElement(SharpTeethId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.SharpTeeth)),
			new Live2DControllerElement(BellChokerElementId, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.BellChoker)),
			new Live2DControllerElement("Hologram_BasicHologram", Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(ColorScaler.DefaultColorScaler, ColoringGroupEnum.Hologram)),
			new Live2DControllerElement(ItemSlot.SlotManager.Tail.Id, Live2DControllerElement.ElementColoringGroup.CreateInstanceColorIndex0(colorScaler11, ColoringGroupEnum.Tail), Live2DControllerElement.ElementColoringGroup.CreateInstanceDifferentColor(-1, ColorScaler.DefaultColorScaler, ColoringGroupEnum.TailPlug))
		};
		foreach (Live2DControllerElement element in elements)
		{
			foreach (Live2DControllerElement.ElementColoringGroup group in element.Groups)
			{
				group.Init(this);
			}
		}
	}

	public void SetCurrentToolSelectorActive(bool value)
	{
		if (CurrentToolSelector != null)
		{
			CurrentToolSelector.gameObject.SetActive(value);
		}
	}

	public bool TryGetHeldInteractable(out ILive2DInteractable interactable)
	{
		interactable = _hoveredOrHeld;
		return _isHeld;
	}

	public bool IsHeld(ILive2DInteractable interactable)
	{
		if (_hoveredOrHeld == interactable)
		{
			return _isHeld;
		}
		return false;
	}

	protected Layer AddLayer(int nr)
	{
		Layer layer = new Layer(nr);
		_allLayers.Add(layer);
		_layers.Add(nr, layer);
		return layer;
	}

	protected Layer GetLayer(int nr)
	{
		if (_layers.TryGetValue(nr, out var value))
		{
			return value;
		}
		return AddLayer(nr);
	}

	private void ResetLayers()
	{
		_allLayers = _allLayers.ClearOrCreate();
		_layers = _layers.ClearOrCreate();
		AddLayer(0);
	}

	public void ChangeLayer(int to)
	{
		if (_currentLayerNumber != to)
		{
			LetGoOfCurrentlyHeld();
			_currentLayerNumber = to;
		}
	}

	public Live2DGenericInteractable AddGenericInteractable(CubismRenderer[] renderers, int layernr = 0)
	{
		Layer layer = GetLayer(layernr);
		Live2DGenericInteractable live2DGenericInteractable = new Live2DGenericInteractable
		{
			Renderers = renderers
		};
		layer.GenericInteractables.Add(live2DGenericInteractable);
		return live2DGenericInteractable;
	}

	public T AddInteractable<T>(IEnumerable<Part> parts, T interactable, CubismRaycastablePrecision precision = CubismRaycastablePrecision.BoundingBox, int layernr = 0) where T : ILive2DInteractableHoverable
	{
		Layer layer = GetLayer(layernr);
		List<CubismRaycastable> list = new List<CubismRaycastable>();
		layer.Interactables.Add(interactable);
		foreach (Part part in parts)
		{
			CubismDrawable cubismDrawable = part.CubismDrawable;
			layer.DrawableToInteractable[cubismDrawable] = interactable;
			CubismRaycastable orAddComponent = cubismDrawable.gameObject.GetOrAddComponent<CubismRaycastable>();
			orAddComponent.Precision = precision;
			if (!layer.Raycastables.Contains(orAddComponent))
			{
				layer.Raycastables.Add(orAddComponent);
			}
			list.Add(orAddComponent);
		}
		interactable.SetConnectedRaycastables(list);
		return interactable;
	}

	public T AddInteractable<T>(IEnumerable<string> drawablenames, T interactable, CubismRaycastablePrecision precision = CubismRaycastablePrecision.BoundingBox, int layernr = 0) where T : ILive2DInteractableHoverable
	{
		List<Part> list = new List<Part>();
		foreach (string drawablename in drawablenames)
		{
			if (TryGetCachedPart(drawablename, out var part))
			{
				list.Add(part);
				continue;
			}
			Debug.LogError("Drawable \"" + drawablename + "\" not found in " + name + "! (Needed for an interactable)");
		}
		return AddInteractable(list, interactable, precision, layernr);
	}

	public T AddInteractable<T>(string drawablename, T interactable, CubismRaycastablePrecision precision = CubismRaycastablePrecision.BoundingBox, int layernr = 0) where T : ILive2DInteractableHoverable
	{
		return AddInteractable(new List<string> { drawablename }, interactable, precision, layernr);
	}

	public T AddInteractableHitArea<T>(Part part, T interactable, CubismRaycastablePrecision precision = CubismRaycastablePrecision.Triangles, int layernr = 0) where T : ILive2DInteractableHoverable
	{
		return AddInteractable(new Part[1] { part }, interactable, precision, layernr);
	}

	public Live2DFullBodyInteractable SetFullBodyInteractable(Live2DFullBodyInteractable fullBodyInteractable, int layernr)
	{
		GetLayer(layernr).FullbodyInteractable = fullBodyInteractable;
		return fullBodyInteractable;
	}

	public void RemoveInteractable(ILive2DInteractableHoverable interactable)
	{
		if (interactable == null)
		{
			Debug.LogError("Trying to remove a null interactable!");
			return;
		}
		foreach (Layer value in _layers.Values)
		{
			if (value.Interactables.Contains(interactable))
			{
				RemoveInteractableInner(interactable, value);
				return;
			}
		}
		Debug.LogError($"Trying to remove interactable {interactable} {interactable.HoverText} from {name}! It wasn't found!");
	}

	private void RemoveInteractableInner(ILive2DInteractableHoverable interactable, Layer layer)
	{
		if (!layer.Interactables.Remove(interactable))
		{
			return;
		}
		foreach (CubismRaycastable connectedRaycastable in interactable.ConnectedRaycastables)
		{
			layer.DrawableToInteractable.Remove(connectedRaycastable.GetComponent<CubismDrawable>());
			layer.Raycastables.Remove(connectedRaycastable);
		}
		if (IsHeld(interactable))
		{
			LetGoOfCurrentlyHeld();
		}
	}

	private Vector2 GetMouseInput()
	{
		return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * (OptionsStatic.SensitivityInstance / (float)Screen.width);
	}

	public void RotateInputToTransformRotation(ref Vector2 v)
	{
		v = transform.InverseTransformDirection(v);
	}

	public void LetGoIfHeld(ILive2DInteractable interactable)
	{
		if (_hoveredOrHeld != null && interactable == _hoveredOrHeld)
		{
			LetGoOfCurrentlyHeld();
		}
	}

	public void LetGoOfCurrentlyHeld()
	{
		if (_hoveredOrHeld != null)
		{
			CurrentToolSelector?.SetText("");
			if (_isHeld)
			{
				_hoveredOrHeld.EndHold();
				_isHeld = false;
			}
			if (_hoveredOrHeld is ILive2DInteractableHoverable live2DInteractableHoverable)
			{
				live2DInteractableHoverable.EndHover();
				CursorSingleton.EndCursorOverride(ref _interactableHoverCursorOverride);
			}
			_hoveredOrHeld = null;
		}
	}

	private void UpdateInteractables()
	{
		if (UiOverlay.Instance.IsOpen)
		{
			return;
		}
		Layer currentLayer = CurrentLayer;
		if (_hoveredOrHeld == null)
		{
			Vector2 mouseInput = GetMouseInput();
			bool flag = Input.GetMouseButtonDown(0) && !UiOverlay.Instance.IsPointerOverGameObject();
			if (!((mouseInput.x == 0f && mouseInput.y == 0f) | flag))
			{
				return;
			}
			Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
			Model.Raycast(currentLayer.Raycastables, ray, 15, ref _raycastHitCache);
			foreach (CubismRaycastHit item in _raycastHitCache)
			{
				if (currentLayer.DrawableToInteractable.TryGetValue(item.Drawable, out var value))
				{
					if (value.Active)
					{
						if (flag)
						{
							_hoveredOrHeld = value;
							_isHeld = true;
							value.StartHover();
							CursorSingleton.ContinueOrStartCursorOverride(ref _interactableHoverCursorOverride, value.HoverCursor);
							_hoveredOrHeld.StartHold(item.Drawable);
						}
						else
						{
							CurrentToolSelector?.SetText(value.HoverText);
							_hoveredOrHeld = value;
							_isHeld = false;
							value.StartHover();
							CursorSingleton.ContinueOrStartCursorOverride(ref _interactableHoverCursorOverride, value.HoverCursor);
						}
						return;
					}
				}
				else
				{
					Debug.LogError(item.Drawable.Id + " has a raycast target, but no interactable!");
				}
			}
			if (!flag)
			{
				return;
			}
			foreach (Live2DGenericInteractable genericInteractable in currentLayer.GenericInteractables)
			{
				if (genericInteractable.Active)
				{
					Model.RaycastQuickBoundingBox(genericInteractable.Renderers, ray, 50, ref _raycastRendererCache1);
					Model.RaycastQuickBoundingBox(_raycastRendererCache1, ray, 20, ref _raycastRendererCache2);
					List<CubismRenderer> raycastRendererCache = _raycastRendererCache2;
					if (raycastRendererCache.Count > 0)
					{
						_hoveredOrHeld = genericInteractable;
						_isHeld = true;
						_hoveredOrHeld.StartHold(raycastRendererCache[0].GetComponent<CubismDrawable>());
						return;
					}
				}
			}
			Live2DFullBodyInteractable fullbodyInteractable = currentLayer.FullbodyInteractable;
			if (fullbodyInteractable != null && fullbodyInteractable.Active)
			{
				Model.RaycastQuickBoundingBox(GetRenderersInDrawOrder(), ray, 100, ref _raycastRendererCache1);
				Model.RaycastQuickBoundingBox(_raycastRendererCache1, ray, _raycastRendererCache1.Count, ref _raycastRendererCache2);
				CubismRenderer cubismRenderer = fullbodyInteractable.choose(_raycastRendererCache2);
				if (cubismRenderer != null)
				{
					_hoveredOrHeld = fullbodyInteractable;
					_isHeld = true;
					_hoveredOrHeld.StartHold(cubismRenderer.GetComponent<CubismDrawable>());
				}
			}
		}
		else if (!_isHeld)
		{
			if (!(_hoveredOrHeld is ILive2DInteractableHoverable live2DInteractableHoverable))
			{
				return;
			}
			Ray ray2 = Camera.ScreenPointToRay(Input.mousePosition);
			Model.Raycast(live2DInteractableHoverable.ConnectedRaycastables, ray2, live2DInteractableHoverable.ConnectedRaycastables.Count, ref _raycastHitCache);
			foreach (CubismRaycastHit item2 in _raycastHitCache)
			{
				if (currentLayer.DrawableToInteractable.TryGetValue(item2.Drawable, out var value2))
				{
					if (value2.Active)
					{
						if (Input.GetMouseButtonDown(0))
						{
							_isHeld = true;
							_hoveredOrHeld.StartHold(item2.Drawable);
						}
						return;
					}
				}
				else
				{
					Debug.LogError(item2.Drawable.Id + " has a raycast target, but no interactable!");
				}
			}
			CurrentToolSelector?.SetText("");
			CursorSingleton.EndCursorOverride(ref _interactableHoverCursorOverride);
			live2DInteractableHoverable.EndHover();
			_hoveredOrHeld = null;
			_isHeld = false;
		}
		else if (Input.GetMouseButtonUp(0) || !_hoveredOrHeld.Active)
		{
			LetGoOfCurrentlyHeld();
		}
		else
		{
			Vector2 mouseInput2 = GetMouseInput();
			_hoveredOrHeld.ContinueHold(mouseInput2);
		}
	}

	private void FixedUpdateInteractables()
	{
		if (_hoveredOrHeld != null && _isHeld)
		{
			_hoveredOrHeld.ContinueHoldFixed();
		}
	}

	private static void InitializeGenericKissInteractables<T>(T thisController) where T : Live2DController
	{
		int live2DLayer = Live2DToolSelector.MouthTool.Live2DLayer;
		KissingDialogueArea kissDialogues = SingletonMonoBehaviour<StorySingleton>.Instance.GetDialogueArea<KissingDialogueArea>();
		List<(ColoringGroupEnum[], StoryBotDialogueTagDefinition, Action<Live2DGenericInteractable>)> list = new List<(ColoringGroupEnum[], StoryBotDialogueTagDefinition, Action<Live2DGenericInteractable>)>();
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.Boobs }, kissDialogues.KissBoobs, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.Pelvic }, kissDialogues.KissPelvic, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.Sclera }, kissDialogues.KissEye, (Live2DGenericInteractable i) =>
		{
			i.OnHoldStart = (CubismDrawable d) =>
			{
				thisController.CurrentBrain.InteractionMemory.ActivateComplexWithData(InteractionMemoryEnum.KissBody, kissDialogues.KissEye);
				thisController.StartKiss(d);
				thisController.Eyes.DisableBlinking = true;
				thisController.Eyes.CloseEyes();
			};
			i.OnHoldEnd = () =>
			{
				thisController.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.KissBody);
				thisController.EndKiss();
				thisController.Eyes.DisableBlinking = false;
				thisController.Eyes.OpenEyes();
			};
		}));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.CatEarMain }, kissDialogues.KissCatEars, (Live2DGenericInteractable i) =>
		{
			i.OnHoldStart = (CubismDrawable d) =>
			{
				thisController.CurrentBrain.InteractionMemory.ActivateComplexWithData(InteractionMemoryEnum.KissBody, kissDialogues.KissCatEars);
				thisController.StartKiss(d);
				if (GameVariables.Current.IsBotAwake && thisController is ILive2DController_CatEarsMovement live2DController_CatEarsMovement)
				{
					if (d.Id.Contains("R"))
					{
						live2DController_CatEarsMovement.ParamEarR.Value = 1f;
					}
					else
					{
						live2DController_CatEarsMovement.ParamEarL.Value = 1f;
					}
				}
			};
			i.OnHoldEnd = () =>
			{
				thisController.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.KissBody);
				thisController.EndKiss();
			};
		}));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.Thigh }, kissDialogues.KissThigh, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.Tummy }, kissDialogues.KissTummy, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.Ass }, kissDialogues.KissAss, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.LegL }, kissDialogues.KissLeg, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.LegR }, kissDialogues.KissLeg, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.LegLFoot }, kissDialogues.KissFoot, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.LegRFoot }, kissDialogues.KissFoot, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.ArmL }, kissDialogues.KissArm, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.ArmR }, kissDialogues.KissArm, null));
		list.Add((new ColoringGroupEnum[1] { ColoringGroupEnum.Skin }, kissDialogues.KissGeneric, null));
		list.Add((AllHairNonAccessoryColoringGroups, kissDialogues.KissHair, null));
		foreach (var item2 in list)
		{
			var (array, _, _) = item2;
			CubismRenderer[] array3;
			if (array.Length > 1)
			{
				List<CubismRenderer> list2 = new List<CubismRenderer>();
				ColoringGroupEnum[] array2 = array;
				foreach (ColoringGroupEnum coloringGroupEnum in array2)
				{
					ColoringGroup coloringGroup = thisController.GetGroup(coloringGroupEnum);
					list2.AddRange(coloringGroup.GetCubismRenderers());
				}
				array3 = list2.ToArray();
			}
			else
			{
				array3 = thisController.GetGroup(array[0]).GetCubismRenderers();
			}
			if (array3.Length == 0)
			{
				continue;
			}
			StoryBotDialogueTagDefinition talkDefinition = item2.Item2;
			Action<Live2DGenericInteractable> item = item2.Item3;
			Live2DGenericInteractable live2DGenericInteractable = thisController.AddGenericInteractable(array3, live2DLayer);
			if (item == null)
			{
				live2DGenericInteractable.OnHoldStart = (CubismDrawable d) =>
				{
					thisController.CurrentBrain.InteractionMemory.ActivateComplexWithData(InteractionMemoryEnum.KissBody, talkDefinition);
					thisController.StartKiss(d);
				};
				live2DGenericInteractable.OnHoldEnd = () =>
				{
					thisController.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.KissBody);
					thisController.EndKiss();
				};
			}
			else
			{
				item(live2DGenericInteractable);
			}
		}
	}

	private void InitializeDefaultInteractable()
	{
		int live2DLayer = Live2DToolSelector.RemoveClothesTool.Live2DLayer;
		SetFullBodyInteractable(new Live2DFullBodyInteractable
		{
			choose = (List<CubismRenderer> allClicked) =>
			{
				List<Item> list = allClicked.Select(GetCachedPart).SelectMany((Part part) => from layer in part.Layers
					where layer.RelatedItem != null && CanBeRemoved(layer.RelatedItem.EquippedSlot) && !layer.RelatedItem.GetItemPrefab().IsDefault
					select layer.RelatedItem).Distinct()
					.ToList();
				if (list.Count == 1)
				{
					UnequipPrompt(list[0]);
				}
				else if (list.Count > 1)
				{
					List<PopupChoice> choices = list.Select((Item t) => new PopupChoice(t.GetName(), () =>
					{
						UnequipPrompt(t);
					})).ToList();
					UiOverlay.Instance.OpenContextMenuPopup(choices);
				}
				return (CubismRenderer)null;
			},
			OnHoldStart = (CubismDrawable t) =>
			{
				LetGoOfCurrentlyHeld();
			}
		}, live2DLayer);
		InitializeGenericKissInteractables(this);
		static void UnequipPrompt(Item item)
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Equipment_Remove(item.GetName()), () =>
			{
				Result result = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.UnequipItemDescription(item);
				if (!result.IsSuccess)
				{
					UiOverlay.Instance.SimplePopup(LOC.UI.Equipment_ButtonUnequip, result.GetDescription() + "\n" + LOC.UI.Equipment_UnequipDependencyPrompt, () =>
					{
						UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.UnequipItemDescription(item, withDependencies: true).GetDescription());
					});
				}
			});
		}
	}

	public bool TryGetCurrentlyHoveredOrHeldInteractablePosition(out Vector3 position)
	{
		if (_hoveredOrHeld != null && _hoveredOrHeld is ILive2DInteractableHoverable live2DInteractableHoverable && live2DInteractableHoverable.ConnectedRaycastables.Count > 0)
		{
			position = live2DInteractableHoverable.ConnectedRaycastables[0].GetComponent<CubismRenderer>().CalculateWorldBoundsCenter();
			return true;
		}
		position = Vector3.zero;
		return false;
	}

	public IReadOnlyCollection<ILimbUsage> GetDistinctLimbUsages()
	{
		return _activeLimbUsages;
	}

	[MustDisposeResource]
	public PooledList<ILimbUsage> GetCopyLimbUsages(out List<ILimbUsage> limbUsages)
	{
		return MyCollectionPool<ILimbUsage>.GetListAndFill(_activeLimbUsages, out limbUsages);
	}

	private void StartLimb(ILimbUsage limbUsage)
	{
		_activeLimbUsages.Add(limbUsage);
		limbUsage.Start();
	}

	private void EndLimb(ILimbUsage limbUsage, LetGoLimbsCaller letGoLimbsCaller)
	{
		_activeLimbUsages.Remove(limbUsage);
		limbUsage.End(letGoLimbsCaller);
		if (letGoLimbsCaller != LetGoLimbsCaller.FromInteractable && TryGetHeldInteractable(out var interactable) && interactable.ReportLimbUsageFinished(limbUsage))
		{
			LetGoOfCurrentlyHeld();
		}
	}

	public bool EnsureLimbs(ILimbUsage limbUsage)
	{
		if (!IsLimbUsageExactlyCorrect(limbUsage))
		{
			UseLimbs(limbUsage);
			return true;
		}
		return false;
	}

	public void UseLimbs(ILimbUsage limbUsage, LetGoLimbsCaller letGoLimbsCaller = LetGoLimbsCaller.Outside)
	{
		LetGoLimbs(limbUsage.TargetLimbUsage, letGoLimbsCaller);
		_limbUsages.SetMultiple((int)limbUsage.TargetLimbUsage, limbUsage);
		StartLimb(limbUsage);
	}

	public void LetGoLimbs(ILimbUsage limbUsage, LetGoLimbsCaller letGoLimbsCaller)
	{
		if (limbUsage != null && _limbUsages.TryFindFlagsForValue(limbUsage, out var flags))
		{
			_limbUsages.SetMultiple(flags, null);
			EndLimb(limbUsage, letGoLimbsCaller);
		}
	}

	public void LetGoLimbs(LimbUsageChoiceEnum limbUsageChoiceEnum, LetGoLimbsCaller letGoLimbsCaller)
	{
		List<ILimbUsage> list;
		using (MyCollectionPool<ILimbUsage>.GetListAndFill(_limbUsages.GetMultiple((int)limbUsageChoiceEnum).Distinct(), out list))
		{
			_limbUsages.SetMultiple((int)limbUsageChoiceEnum, null);
			foreach (ILimbUsage item in list)
			{
				if (item != null)
				{
					EndLimb(item, letGoLimbsCaller);
				}
			}
		}
	}

	public bool IsLimbUsageExactlyCorrect(ILimbUsage limbUsage)
	{
		if (_limbUsages.TryFindFlagsForValue(limbUsage, out var flags))
		{
			return limbUsage.TargetLimbUsage == (LimbUsageChoiceEnum)flags;
		}
		return limbUsage.TargetLimbUsage == LimbUsageChoiceEnum.None;
	}

	public bool TryGetLimbUsageAt(LimbUsageChoiceEnum flag, out ILimbUsage limbUsage)
	{
		limbUsage = _limbUsages.GetSingle((int)flag);
		return limbUsage != null;
	}

	public bool TryGetLimbUsageAt<T>(LimbUsageChoiceEnum flag, out T limbUsage) where T : ILimbUsage
	{
		if (_limbUsages.GetSingle((int)flag) is T val)
		{
			limbUsage = val;
			return true;
		}
		limbUsage = default;
		return false;
	}

	public bool IsLimbActive(LimbUsageChoiceEnum flag)
	{
		ILimbUsage limbUsage;
		return TryGetLimbUsageAt(flag, out limbUsage);
	}

	private void UpdateLimbUsages()
	{
		List<ILimbUsage> limbUsages;
		using (GetCopyLimbUsages(out limbUsages))
		{
			foreach (ILimbUsage item in limbUsages)
			{
				item.Update();
			}
		}
	}

	private void FixedUpdateLimbUsages()
	{
		List<ILimbUsage> limbUsages;
		using (GetCopyLimbUsages(out limbUsages))
		{
			foreach (ILimbUsage item in limbUsages)
			{
				item.FixedUpdate();
			}
		}
	}

	private void ResetLimbUsages()
	{
		List<ILimbUsage> limbUsages;
		using (GetCopyLimbUsages(out limbUsages))
		{
			foreach (ILimbUsage item in limbUsages)
			{
				LetGoLimbs(item, LetGoLimbsCaller.Outside);
			}
		}
	}

	public bool IsApproximatelyDefaultFaceAngle()
	{
		Vector3 defaultFaceAngle = DefaultFaceAngle;
		if (this is ILive2DController_HeadX live2DController_HeadX && !live2DController_HeadX.ParamHeadX.IsApproximately(defaultFaceAngle.x, 0.01f))
		{
			return false;
		}
		if (this is ILive2DController_HeadY live2DController_HeadY && !live2DController_HeadY.ParamHeadY.IsApproximately(defaultFaceAngle.y, 0.01f))
		{
			return false;
		}
		if (this is ILive2DController_HeadZ live2DController_HeadZ && !live2DController_HeadZ.ParamHeadZ.IsApproximately(defaultFaceAngle.z, 0.01f))
		{
			return false;
		}
		return true;
	}

	public void ToDefaultFaceAngle()
	{
		Vector3 defaultFaceAngle = DefaultFaceAngle;
		if (this is ILive2DController_HeadX live2DController_HeadX)
		{
			live2DController_HeadX.ParamHeadX.Value = defaultFaceAngle.x;
		}
		if (this is ILive2DController_HeadY live2DController_HeadY)
		{
			live2DController_HeadY.ParamHeadY.Value = defaultFaceAngle.y;
		}
		if (this is ILive2DController_HeadZ live2DController_HeadZ)
		{
			live2DController_HeadZ.ParamHeadZ.Value = defaultFaceAngle.z;
		}
	}

	private void SetKissingPosition(Vector2 position)
	{
		RectTransform rectTransform = (RectTransform)_mainRectTransform.parent;
		_mainRectTransform.anchoredPosition = rectTransform.InverseTransformPoint(position);
	}

	private IEnumerator Kiss()
	{
		Color color = Color.white.SetAlpha(0f);
		_kissImage.color = Color.white;
		_kissingImage.color = color;
		Coroutine coroutine = _kissImage.StartCoroutine(CoroutineUtilities.FadeToColor(_kissImage, color, 0.2f));
		Coroutine b = _kissImage.StartCoroutine(CoroutineUtilities.FadeToColor(_kissingImage, Color.white, 0.2f));
		yield return coroutine;
		yield return b;
		_kissingImage.transform.localPosition = new Vector3(0f, 0f, 0f);
		yield return CoroutineUtilities.GenericTween((float value) =>
		{
			_kissingImage.transform.localScale = new Vector3(value, value, value);
		}, 1f, 0.8f, 0.1f);
		while (true)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.4f, 1.2f));
			yield return CoroutineUtilities.Vibrate(_kissingImage.transform);
		}
	}

	private void PrepareKissing()
	{
		_kissingMainGameObject = new GameObject("Kissing");
		RectTransform guiTransform = UiOverlay.Instance.GuiTransform;
		_mainRectTransform = _kissingMainGameObject.AddComponent<RectTransform>();
		_mainRectTransform.SetParent(guiTransform, worldPositionStays: false);
		_mainRectTransform.sizeDelta = new Vector2(0f, 0f);
		GameObject gameObject = new GameObject("Kiss image");
		gameObject.transform.SetParent(_mainRectTransform, worldPositionStays: false);
		_kissImage = gameObject.AddComponent<RawImage>();
		_kissImage.texture = KissIndicatorTexture;
		_kissImage.raycastTarget = false;
		GameObject gameObject2 = new GameObject("Kissing image");
		gameObject2.transform.SetParent(_mainRectTransform, worldPositionStays: false);
		_kissingImage = gameObject2.AddComponent<RawImage>();
		_kissingImage.texture = KissingIndicatorTexture;
		_kissingImage.raycastTarget = false;
		_kissingMainGameObject.SetActive(value: false);
	}

	public void StartKiss(CubismDrawable targetDrawable, KissStartPositionType kissStartPositionType = KissStartPositionType.MousePosition, Vector2 offset = default(Vector2))
	{
		if (_kissingMainGameObject.activeSelf)
		{
			EndKiss();
		}
		_kissingMainGameObject.SetActive(value: true);
		_targetPart = GetCachedPart(targetDrawable);
		if (kissStartPositionType == KissStartPositionType.MousePosition || kissStartPositionType != KissStartPositionType.RendererCenter)
		{
			_startPosition = UiOverlay.Instance.MouseToWorld();
		}
		else
		{
			_startPosition = _targetPart.CubismRenderer.MeshRenderer.bounds.center;
		}
		_startPosition += offset;
		SetKissingPosition(_startPosition);
		_targetRendererStartScreenPosition = _targetPart.CubismRenderer.MeshRenderer.bounds.center;
		_kissImage.StartCoroutine(Kiss());
		CursorSingleton.ContinueOrStartCursorOverride(ref _cursorOverride, CursorSingleton.CursorType.NoCursor);
	}

	private void UpdateKissing()
	{
		if (_kissingMainGameObject.activeSelf)
		{
			Vector2 vector = (Vector2)_targetPart.CubismRenderer.MeshRenderer.bounds.center - _targetRendererStartScreenPosition;
			SetKissingPosition(_startPosition + vector);
		}
	}

	public void EndKiss()
	{
		if (_kissingMainGameObject.activeSelf)
		{
			_kissImage.StopAllCoroutines();
			_kissingMainGameObject.SetActive(value: false);
			CursorSingleton.EndCursorOverride(ref _cursorOverride);
		}
	}

	public void OnDisableKissing()
	{
		if (!(_kissingMainGameObject == null))
		{
			EndKiss();
		}
	}

	public static void PrepareCommonHeadpatInteractables<T>(T thisController, Part headpatHitArea, out Live2DInteractableLimbUsage<T> headpatInteractable) where T : Live2DController, ILive2DController_HeadAngles, ILive2DController_Headpat
	{
		thisController.ParamHeadpatX.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		thisController.ParamHeadpatY.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		LimbUsageWithInteractable<T> limbUsageWithInteractable = new LimbUsageWithInteractable<T>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (T c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.Headpat);
				c.ParamHeadpat.SetPercent(1f);
				c.ParamHeadpatX.SetWeight(1f);
				c.ParamHeadpatY.SetWeight(1f);
			},
			HoldUpdateAction = (T c, Vector2 mouseMove) =>
			{
				c.RotateInputToTransformRotation(ref mouseMove);
				c.ParamHeadpatX.AddPercent(mouseMove.x * 10f);
				c.ParamHeadpatY.AddPercent(mouseMove.y * 12f);
				c.ParamHeadX.AddPercent(mouseMove.x * 10f);
				c.ParamHeadX.Value = Mathf.Clamp(c.ParamHeadX.Value, c.ParamHeadX.MinimumValue * 0.8f, c.ParamHeadX.MaximumValue * 0.8f);
				c.ParamHeadY.AddPercent(mouseMove.y * 10f);
				c.ParamHeadZ.AddPercent(mouseMove.x * 10f);
				if (thisController is ILive2DController_BodyAngles live2DController_BodyAngles)
				{
					live2DController_BodyAngles.ParamBodyX.AddPercent(mouseMove.x * 0.23f);
					live2DController_BodyAngles.ParamBodyY.AddPercent(mouseMove.y * 0.23f);
				}
			},
			HoldFixedUpdateAction = (T c) =>
			{
				if (c.CurrentBrain.Pleasure < 0.5f)
				{
					c.CurrentBrain.CausePleasure(0.002f);
				}
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.Mood < 0.3f)
				{
					gameVariables.Mood += 0.001f;
				}
			},
			EndLimbAction = (T c, LetGoLimbsCaller caller) =>
			{
				c.ParamHeadpat.SetPercent(0f);
				c.ParamHeadpatX.SetWeight(2f);
				c.ParamHeadpatY.SetWeight(2f);
				c.ParamHeadpatX.ResetToDefault();
				c.ParamHeadpatY.ResetToDefault();
				c.ToDefaultFaceAngle();
				if (thisController is ILive2DController_BodyAngles live2DController_BodyAngles)
				{
					live2DController_BodyAngles.ParamBodyX.ResetToDefault();
					live2DController_BodyAngles.ParamBodyY.ResetToDefault();
				}
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.Headpat);
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lastHeadpatedAt = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
			}
		};
		headpatInteractable = thisController.AddInteractableHitArea(headpatHitArea, new Live2DInteractableLimbUsage<T>(thisController, LOC.UI.TupleReferences.Interactable_Headpat, limbUsageWithInteractable)
		{
			OnHoverStart = (T c) =>
			{
				if (!c.IsLimbActive(LimbUsageChoiceEnum.RightArm))
				{
					c.ParamHeadpat.SetPercent(1f);
				}
			},
			OnHoverEnd = (T c) =>
			{
				c.ParamHeadpat.SetPercent(0f);
			}
		});
	}

	public static void PrepareCommonMouthInteractables<T>(T thisController, out Live2DInteractableLimbUsage<T> openMouthInteractable, out Live2DInteractableLimbUsage<T> closeMouthInteractables) where T : Live2DController, ILive2DController_Mouth
	{
		LimbUsageWithInteractable<T> limbUsageWithInteractable = new LimbUsageWithInteractable<T>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			HoldUpdateAction = (T c, Vector2 mouseMove) =>
			{
				c.RotateInputToTransformRotation(ref mouseMove);
				c.ParamMouthOpen.AddPercent((0f - mouseMove.y) * 50f);
				if (c.ParamMouthOpen.Value > 0.8f)
				{
					c.Mouth.OpenMouthFull();
					c.LetGoOfCurrentlyHeld();
				}
			}
		};
		openMouthInteractable = new Live2DInteractableLimbUsage<T>(thisController, LOC.UI.TupleReferences.Interactable_OpenHerMouth, limbUsageWithInteractable)
		{
			ActiveFunc = (T c) => c.ParamMouthOpen.Value <= 1f
		};
		thisController.AddInteractable("HitAreaOpenMouth", openMouthInteractable, CubismRaycastablePrecision.Triangles);
		LimbUsageWithInteractable<T> limbUsageWithInteractable2 = new LimbUsageWithInteractable<T>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			HoldUpdateAction = (T c, Vector2 mouseMove) =>
			{
				c.RotateInputToTransformRotation(ref mouseMove);
				c.ParamMouthOpen.AddPercent((0f - mouseMove.y) * 50f);
				if (c.ParamMouthOpen.Value < 1.1f)
				{
					c.Mouth.CloseMouth();
					c.LetGoOfCurrentlyHeld();
				}
			}
		};
		closeMouthInteractables = new Live2DInteractableLimbUsage<T>(thisController, LOC.UI.TupleReferences.Interactable_CloseMouth, limbUsageWithInteractable2)
		{
			ActiveFunc = (T c) => c.ParamMouthOpen.Value > 1f
		};
		thisController.AddInteractable("HitAreaCloseMouth", closeMouthInteractables, CubismRaycastablePrecision.Triangles);
	}

	public static void PrepareCommonTongueInteractables<T>(T thisController, out Live2DInteractable tongueInteractable) where T : Live2DController, ILive2DController_TongueMovement
	{
		tongueInteractable = thisController.AddInteractable("Tounge", new Live2DInteractable
		{
			OnHoldStart = (CubismDrawable d) =>
			{
			},
			OnHeld = (Vector2 mouseMove) =>
			{
				thisController.RotateInputToTransformRotation(ref mouseMove);
				thisController.ParamToungeX.Value += mouseMove.x * 50f;
				thisController.ParamToungeY.Value += mouseMove.y * 50f;
			},
			OnHeldFixed = () =>
			{
			},
			OnHoldEnd = () =>
			{
				thisController.ParamToungeX.ResetToDefault();
				thisController.ParamToungeY.ResetToDefault();
			},
			InfoSls = LOC.UI.TupleReferences.Interactable_PlayTongue
		});
	}

	public static void PrepareCommonMouthKissInteractables<T>(T thisController, out Live2DInteractableLimbUsage<T> suckTongueInteractable, out Live2DInteractableLimbUsage<T> kissMouthInteractable) where T : Live2DController, ILive2DController_HeadAngles, ILive2DController_Mouth
	{
		int live2DLayer = Live2DToolSelector.MouthTool.Live2DLayer;
		LimbUsageWithInteractable<T> limbUsageWithInteractable = new LimbUsageWithInteractable<T>(thisController, LimbUsageChoiceEnum.Mouth)
		{
			StartLimbAction = (T c) =>
			{
				c.ParamMouthOpen.Value = 2f;
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.KissMouth);
			},
			HoldUpdateAction = (T c, Vector2 mouseMove) =>
			{
				c.RotateInputToTransformRotation(ref mouseMove);
				c.ParamHeadX.AddPercent(mouseMove.x * 1f);
				c.ParamHeadX.ClampOffsetFromDefaultValue(0.3f, 0.3f);
				c.ParamHeadY.AddPercent(mouseMove.y * 1f);
				c.ParamHeadY.ClampOffsetFromDefaultValue(0.3f, 0.3f);
				c.ParamMouthOpen.AddPercent(mouseMove.y * 1f);
				c.ParamMouthOpen.ClampValue(1.7f, 2f);
			},
			HoldFixedUpdateAction = (T c) =>
			{
				if (c.CurrentBrain.Pleasure < 0.5f)
				{
					c.CurrentBrain.CausePleasure(0.002f);
				}
			},
			EndLimbAction = (T c, LetGoLimbsCaller caller) =>
			{
				c.ParamMouthOpen.Value = 2f;
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.KissMouth);
				c.ToDefaultFaceAngle();
				c.EndKiss();
			}
		};
		suckTongueInteractable = new Live2DInteractableLimbUsage<T>(thisController, LOC.UI.TupleReferences.Interactable_SuckTongue, limbUsageWithInteractable)
		{
			StartHoldOverride = (Live2DInteractableLimbUsage<T> i, CubismDrawable d) =>
			{
				i.Controller.StartKiss(d, KissStartPositionType.RendererCenter);
				i.EnsureAllLimbs();
			}
		};
		thisController.AddInteractable("Tounge", suckTongueInteractable, CubismRaycastablePrecision.BoundingBox, live2DLayer);
		LimbUsageWithInteractable<T> limbUsageWithInteractable2 = new LimbUsageWithInteractable<T>(thisController, LimbUsageChoiceEnum.Mouth)
		{
			StartLimbAction = (T c) =>
			{
				c.ParamMouthOpen.Value = 1f;
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.KissMouth);
			},
			HoldUpdateAction = (T c, Vector2 mouseMove) =>
			{
				c.RotateInputToTransformRotation(ref mouseMove);
				c.ParamHeadX.AddPercent(mouseMove.x * 1f);
				c.ParamHeadX.ClampOffsetFromDefaultValue(0.3f, 0.3f);
				c.ParamHeadY.AddPercent(mouseMove.y * 1f);
				c.ParamHeadY.ClampOffsetFromDefaultValue(0.3f, 0.3f);
				ComplexEventMemory complexMemory = c.CurrentBrain.InteractionMemory.GetComplexMemory(InteractionMemoryEnum.KissMouth);
				if (!complexMemory.Active)
				{
					c.LetGoOfCurrentlyHeld();
				}
				else if (complexMemory.Length > 6f)
				{
					c.ParamMouthOpen.AddPercent(mouseMove.y * 1f);
					c.ParamMouthOpen.ClampValue(1f, 1.1f);
				}
				else
				{
					c.ParamMouthOpen.AddPercent(mouseMove.y * 1f);
					c.ParamMouthOpen.ClampValue(0.5f, 0.6f);
				}
			},
			HoldFixedUpdateAction = (T c) =>
			{
				if (c.CurrentBrain.Pleasure < 0.5f)
				{
					c.CurrentBrain.CausePleasure(0.002f);
				}
			},
			EndLimbAction = (T c, LetGoLimbsCaller caller) =>
			{
				c.ParamMouthOpen.Value = 0f;
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.KissMouth);
				c.ToDefaultFaceAngle();
				c.EndKiss();
			}
		};
		kissMouthInteractable = new Live2DInteractableLimbUsage<T>(thisController, LOC.UI.TupleReferences.Interactable_KissHer, limbUsageWithInteractable2)
		{
			StartHoldOverride = (Live2DInteractableLimbUsage<T> i, CubismDrawable d) =>
			{
				i.Controller.StartKiss(d, KissStartPositionType.RendererCenter);
				i.EnsureAllLimbs();
			}
		};
		thisController.AddInteractable("InnerMouth", kissMouthInteractable, CubismRaycastablePrecision.BoundingBox, live2DLayer);
	}

	private void ResetItemModifiers()
	{
		foreach (Live2DControllerItemModifier activeItemModifier in _activeItemModifiers)
		{
			activeItemModifier.OnRemoved(this);
		}
		_activeItemModifiers.Clear();
	}

	private void UpdateItemModifiersFromEquipped(UpdateTexturesFromCurrentlyEquippedTypeEnum type)
	{
		for (int num = _activeItemModifiers.Count - 1; num >= 0; num--)
		{
			Live2DControllerItemModifier live2DControllerItemModifier = _activeItemModifiers[num];
			if (!_currentlyEquippedDict.ContainsKey(live2DControllerItemModifier.SourceItem))
			{
				live2DControllerItemModifier.OnRemoved(this);
				_activeItemModifiers.RemoveAt(num);
			}
		}
		foreach (ItemWithCurrentData item in _currentlyEquipped)
		{
			if (item.Prefab.ItemModiferAdditionFunction == null)
			{
				continue;
			}
			Live2DControllerItemModifier live2DControllerItemModifier2 = _activeItemModifiers.FirstOrDefault((Live2DControllerItemModifier x) => x.SourceItem == item.Item);
			if (live2DControllerItemModifier2 == null)
			{
				Live2DControllerItemModifier live2DControllerItemModifier3 = item.Prefab.ItemModiferAdditionFunction(this, item.Item);
				if (live2DControllerItemModifier3 != null)
				{
					live2DControllerItemModifier3.SetSourceItem(item.Item);
					_activeItemModifiers.Add(live2DControllerItemModifier3);
					live2DControllerItemModifier3.OnAdded(this, type);
				}
			}
			else
			{
				live2DControllerItemModifier2.OnReadded(this, type);
			}
		}
	}

	private void FixedUpdateItemModifier()
	{
		for (int i = 0; i < _activeItemModifiers.Count; i++)
		{
			_activeItemModifiers[i].FixedUpdate(this);
		}
	}

	private void AddItemModifier(Live2DControllerItemModifier itemModifier, UpdateTexturesFromCurrentlyEquippedTypeEnum type)
	{
		_activeItemModifiers.Add(itemModifier);
		itemModifier.OnAdded(this, type);
	}

	public Live2DParameter GetParamOrCreateFake<TInterface>(Func<TInterface, Live2DParameter> selector) where TInterface : class
	{
		if (this is TInterface arg)
		{
			return selector(arg);
		}
		return Live2DParameter.CreateFake(directlyControlledByModel: true);
	}

	protected Live2DParameter GetParamToDirectlyControl(string paramName, bool repeat = false)
	{
		Live2DParameter live2DParameter = DynamicParameters.FirstOrDefault((Live2DParameter t) => t.Parameter.name == paramName);
		if (live2DParameter != null)
		{
			live2DParameter.DirectlyControlledByModel = true;
			live2DParameter.Repeat = repeat;
			return live2DParameter;
		}
		return Live2DParameter.CreateFake(directlyControlledByModel: true);
	}

	protected Live2DParameter[] GetParamArrayToDirectlyControl(string paramPrefix, int paramCount)
	{
		Live2DParameter[] array = new Live2DParameter[paramCount];
		for (int i = 0; i < paramCount; i++)
		{
			string text = paramPrefix + i;
			array[i] = GetParamToDirectlyControl(text);
			if (!array[i].Exists)
			{
				Debug.LogError(text + " doesn't exist!");
			}
		}
		return array;
	}

	public void FinishAllMoves()
	{
		Expression.Update();
		Live2DParameter[] dynamicParameters = DynamicParameters;
		for (int i = 0; i < dynamicParameters.Length; i++)
		{
			dynamicParameters[i].FinishMove();
		}
	}

	public ParameterBase GetParamByPropertyName(string parameterName)
	{
		PropertyInfo property = GetType().GetProperty(parameterName);
		if (property == null)
		{
			return null;
		}
		if (property.GetGetMethod() == null)
		{
			return null;
		}
		return property.GetValue(this) as ParameterBase;
	}

	private void InitializeParameters()
	{
		if (!_parametersInitialized)
		{
			_parametersInitialized = true;
			ParametersInitializeInner();
		}
	}

	private IEnumerable<(PropertyInfo propertyInfo, CubismParameterName nameAttr)> GetLive2DParametersWithAttribute()
	{
		foreach (Type item2 in GetType().GetParentTypesWithCurrent().Reverse())
		{
			foreach (PropertyInfo item3 in from t in item2.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where t.PropertyType == typeof(Live2DParameter)
				select t)
			{
				CubismParameterName customAttribute = item3.GetCustomAttribute<CubismParameterName>();
				if (customAttribute != null)
				{
					CubismParameterName item = customAttribute;
					yield return (propertyInfo: item3, nameAttr: item);
				}
			}
		}
	}

	private void ParametersInitializeInner()
	{
		EnsureModel();
		DynamicParameters = new Live2DParameter[Model.Parameters.Length];
		for (int i = 0; i < Model.Parameters.Length; i++)
		{
			DynamicParameters[i] = Live2DParameter.CreateReal(Model.Parameters[i], directlyControlledByModel: false);
		}
		foreach (var item3 in GetLive2DParametersWithAttribute())
		{
			PropertyInfo item = item3.propertyInfo;
			CubismParameterName item2 = item3.nameAttr;
			Live2DParameter paramToDirectlyControl = GetParamToDirectlyControl(item2.Name);
			item.SetValue(this, paramToDirectlyControl);
			if (item2.Type == Live2DParameterUpdateTypeEnum.Linear)
			{
				paramToDirectlyControl.SetWeight(item2.Weight / 2f);
			}
			else
			{
				paramToDirectlyControl.SetWeight(item2.Weight);
			}
			paramToDirectlyControl.UpdateType = item2.Type;
		}
	}

	public StoryEmoteManager GetStoryEmoteManager()
	{
		return _storyEmoteManager ?? (_storyEmoteManager = CreateStoryEmoteManager());
	}

	protected virtual StoryEmoteManager CreateStoryEmoteManager()
	{
		StoryEmoteManager storyEmoteManager = new StoryEmoteManager();
		storyEmoteManager.AddSlot(new StoryEmoteExpressionSlot(this));
		return storyEmoteManager;
	}

	public void SetDefaultEmote(bool instantly)
	{
		GetStoryEmoteManager().SetDefaultEmote();
	}

	public void SetEmote(IEnumerable<EmoteData> emoteDatas)
	{
		StoryEmoteManager storyEmoteManager = GetStoryEmoteManager();
		foreach (EmoteData emoteData in emoteDatas)
		{
			storyEmoteManager.SetEmote(emoteData);
		}
	}

	public Result IsValidEmote(EmoteData emote)
	{
		return GetStoryEmoteManager().IsValidEmote(emote);
	}
}
