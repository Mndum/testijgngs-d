using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class TextureSingleton : SingletonMonoBehaviour<TextureSingleton>
{
	[Serializable]
	public class ScratchContainer
	{
		public List<Texture2D> Small = new List<Texture2D>();

		public List<Texture2D> Medium = new List<Texture2D>();

		public List<Texture2D> Big = new List<Texture2D>();

		public void AddTexture(Texture2D texture)
		{
			int num = texture.width * texture.height;
			if (num < 256)
			{
				Small.Add(texture);
			}
			else if (num < 1296)
			{
				Medium.Add(texture);
			}
			else
			{
				Big.Add(texture);
			}
		}

		public void Clear()
		{
			Small.Clear();
			Medium.Clear();
			Big.Clear();
		}

		public void AddTextures(ScratchContainer b)
		{
			Small.AddRange(b.Small);
			Medium.AddRange(b.Medium);
			Big.AddRange(b.Big);
		}
	}

	[Serializable]
	public class TypeContainerDictionary : SerializableDictionary<ItemPrefab.ScratchTextureType, ScratchContainer>
	{
	}

	[SerializeField]
	private TypeContainerDictionary allScratchTextures = new TypeContainerDictionary();

	[Header("Clothes and parts")]
	public SpecialTextureHolder lingerieBra;

	public SpecialTextureHolder lingerieSockL;

	public SpecialTextureHolder lingerieSockR;

	public SpecialTextureHolder lingeriePanties;

	public SpecialTextureHolder strippedPanties;

	public SpecialTextureHolder sneakerL;

	public SpecialTextureHolder sneakerR;

	public SpecialTextureHolder hightechHypercamoSkin;

	public SpecialTextureHolder longSockL;

	public SpecialTextureHolder longSockR;

	public SpecialTextureHolder heartChoker;

	public SpecialTextureHolder heartGlasses;

	public SpecialTextureHolder eyeShadow;

	public SpecialTextureHolder gothEyeMakeup;

	public SpecialTextureHolder pumpkinHairpin;

	public SpecialTextureHolder septumPiercing;

	public SpecialTextureHolder stripedSockL;

	public SpecialTextureHolder stripedSockR;

	public SpecialTextureHolder septumPiercing2;

	public SpecialTextureHolder experimentalLegR;

	public SpecialTextureHolder experimentalLegL;

	public SpecialTextureHolder experimentalArmR;

	public SpecialTextureHolder experimentalArmL;

	public SpecialTextureHolder catTailDifferentEnd;

	public SpecialTextureHolder tailFluffy;

	public SpecialTextureHolder tailFluffyStripes;

	public SpecialTextureHolder lipstick;

	public SpecialTextureHolder vintageDress;

	public SpecialTextureHolder classyShoeL;

	public SpecialTextureHolder classyShoeR;

	public SpecialTextureHolder clothierHair;

	public SpecialTextureHolder eyecoveringBangHair;

	public SpecialTextureHolder himeCutHair;

	public SpecialTextureHolder ovalGlasses;

	public SpecialTextureHolder eyePatch;

	public SpecialTextureHolder hairClips;

	public SpecialTextureHolder tonguePiercing;

	public SpecialTextureHolder stirrupStockingL;

	public SpecialTextureHolder stirrupStockingR;

	public SpecialTextureHolder manicure;

	public SpecialTextureHolder pedicure;

	public SpecialTextureHolder beltCollar;

	public SpecialTextureHolder eyeliner;

	public SpecialTextureHolder heartEyeSticker;

	public SpecialTextureHolder naturalEyelashes;

	public SpecialTextureHolder simpleChoker;

	public SpecialTextureHolder shortSockL;

	public SpecialTextureHolder shortSockR;

	public SpecialTextureHolder twoStripedStockingL;

	public SpecialTextureHolder twoStripedStockingR;

	public SpecialTextureHolder kneeHighL;

	public SpecialTextureHolder kneeHighR;

	public SpecialTextureHolder spikedCollar;

	public SpecialTextureHolder miniSkirt;

	public SpecialTextureHolder bikiniBottom;

	public ScratchContainer GetContainer(ItemPrefab.ScratchTextureType type)
	{
		if (allScratchTextures.TryGetValue(type, out var value))
		{
			return value;
		}
		return allScratchTextures[type] = new ScratchContainer();
	}

	public void OnValidate()
	{
		foreach (KeyValuePair<ItemPrefab.ScratchTextureType, ScratchContainer> allScratchTexture in allScratchTextures)
		{
			allScratchTexture.Value.Clear();
		}
		Texture2D[] array = Resources.LoadAll<Texture2D>("Live2D/Quality/");
		List<ItemPrefab.ScratchTextureType> list = ((ItemPrefab.ScratchTextureType[])Enum.GetValues(typeof(ItemPrefab.ScratchTextureType))).Where((ItemPrefab.ScratchTextureType t) => t != ItemPrefab.ScratchTextureType.Universal).ToList();
		List<string> list2 = list.Select((ItemPrefab.ScratchTextureType t) => t.ToString()).ToList();
		List<ScratchContainer> list3 = list.Select(GetContainer).ToList();
		ScratchContainer container = GetContainer(ItemPrefab.ScratchTextureType.Universal);
		Texture2D[] array2 = array;
		foreach (Texture2D texture2D in array2)
		{
			bool flag = false;
			for (int num2 = 0; num2 < list.Count; num2++)
			{
				if (texture2D.name.Contains(list2[num2]))
				{
					list3[num2].AddTexture(texture2D);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				container.AddTexture(texture2D);
			}
		}
		foreach (ScratchContainer item in list3)
		{
			item.AddTextures(container);
		}
	}

	public override void Awake()
	{
		base.Awake();
	}
}
