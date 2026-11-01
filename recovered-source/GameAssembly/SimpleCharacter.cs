using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

public class SimpleCharacter : MonoBehaviour, IHasSimpleCharacter, IStoryCharacter
{
	public interface ISimpleCharacterItem
	{
		string GetId();
	}

	public interface ISimpleCharacterSlot
	{
		string GetId();

		bool HasItem(string item);

		IEnumerable<string> GetItemIds();

		IEnumerable<ISimpleCharacterItem> GetItems();
	}

	[Serializable]
	public class ComboSlotItem : ISimpleCharacterItem
	{
		public string Id;

		public StoryEmoteItemReference[] ItemsInTheComboSlot;

		public string GetId()
		{
			return Id;
		}
	}

	[Serializable]
	public class ComboSlot : ISimpleCharacterSlot
	{
		public string Id;

		public List<ComboSlotItem> Items;

		public string GetId()
		{
			return Id;
		}

		public bool HasItem(string itemId)
		{
			ComboSlotItem item;
			return TryGetItem(itemId, out item);
		}

		public bool TryGetItem(string itemId, out ComboSlotItem item)
		{
			foreach (ComboSlotItem item2 in Items)
			{
				if (string.Equals(item2.Id, itemId, StringComparison.OrdinalIgnoreCase))
				{
					item = item2;
					return true;
				}
			}
			item = null;
			return false;
		}

		public IEnumerable<string> GetItemIds()
		{
			return Items.Select((ComboSlotItem t) => t.Id);
		}

		public IEnumerable<ISimpleCharacterItem> GetItems()
		{
			return Items;
		}
	}

	[Serializable]
	public class SimpleCharactersItemCache : ISimpleCharacterItem
	{
		public string Id => ItemTransform.name;

		[field: SerializeField]
		public Transform ItemTransform { get; private set; }

		[field: SerializeField]
		public SpriteRenderer ItemSpriteRenderer { get; private set; }

		public SimpleCharactersItemCache(Transform itemTransform)
		{
			ItemTransform = itemTransform;
			ItemSpriteRenderer = itemTransform.GetComponent<SpriteRenderer>();
		}

		public string GetId()
		{
			return Id;
		}
	}

	[Serializable]
	public class NormalSlot : ISimpleCharacterSlot
	{
		[field: SerializeField]
		public Transform SlotTransform { get; private set; }

		[field: SerializeField]
		public SimpleCharactersItemCache[] Items { get; private set; }

		public NormalSlot(Transform slotTransform, SimpleCharactersItemCache[] items)
		{
			SlotTransform = slotTransform;
			Items = items;
		}

		public bool HasItem(string itemId)
		{
			SimpleCharactersItemCache item;
			if (!string.Equals("none", itemId, StringComparison.OrdinalIgnoreCase))
			{
				return TryGetItem(itemId, out item);
			}
			return true;
		}

		public IEnumerable<string> GetItemIds()
		{
			return Items.Select((SimpleCharactersItemCache t) => t.Id);
		}

		public IEnumerable<ISimpleCharacterItem> GetItems()
		{
			return Items;
		}

		public string GetId()
		{
			return SlotTransform.name;
		}

		public bool TryGetItem(string itemId, out SimpleCharactersItemCache item)
		{
			SimpleCharactersItemCache[] items = Items;
			foreach (SimpleCharactersItemCache simpleCharactersItemCache in items)
			{
				if (string.Equals(simpleCharactersItemCache.Id, itemId, StringComparison.OrdinalIgnoreCase))
				{
					item = simpleCharactersItemCache;
					return true;
				}
			}
			item = null;
			return false;
		}

		public IEnumerable<SpriteRenderer> GetAllRenderers()
		{
			return Items.Select((SimpleCharactersItemCache t) => t.ItemSpriteRenderer);
		}
	}

	public float yoffest;

	public const string IgnoreTag = "CharacterScriptIgnore";

	[SerializeField]
	public List<StoryEmoteItemReference> defaultActiveItems;

	[SerializeField]
	private List<ComboSlot> comboSlots = new List<ComboSlot>();

	[ReadOnly]
	[SerializeField]
	private List<NormalSlot> _slotCaches;

	public const string NoneItemId = "none";

	[Tooltip("Will not be used for summary generation if it's not!")]
	[field: SerializeField]
	public bool Unique { get; private set; } = true;

	[field: SerializeField]
	public string DefaultId { get; private set; }

	[field: NonSerialized]
	public Vector3 DefaultScale { get; private set; } = Vector3.one;

	public ReadOnlyCollection<ComboSlot> ComboSlots => comboSlots.AsReadOnly();

	public ReadOnlyCollection<NormalSlot> NormalSlots => _slotCaches.AsReadOnly();

	public string InstanceId { get; private set; }

	public SimpleCharacter GetSimpleCharacter()
	{
		return this;
	}

	public bool TryGetSlotComboSlot(string id, out ComboSlot comboSlot)
	{
		int num = comboSlots.FindIndex((ComboSlot x) => string.Equals(x.GetId(), id, StringComparison.OrdinalIgnoreCase));
		if (num < 0)
		{
			comboSlot = null;
			return false;
		}
		comboSlot = ComboSlots[num];
		return true;
	}

	public bool TryGetEquivalentComboSlotItem(IEnumerable<StoryEmoteItemReference> items, out ComboSlot comboSlotOut, out ComboSlotItem comboSlotItemOut)
	{
		HashSet<StoryEmoteItemReference> hashSet = new HashSet<StoryEmoteItemReference>(items, StoryEmoteItemReference.SlotIdItemIdComparer);
		foreach (ComboSlot comboSlot in comboSlots)
		{
			foreach (ComboSlotItem item in comboSlot.Items)
			{
				HashSet<StoryEmoteItemReference> hashSet2 = new HashSet<StoryEmoteItemReference>(item.ItemsInTheComboSlot, StoryEmoteItemReference.SlotIdItemIdComparer);
				if (hashSet.SetEquals(hashSet2))
				{
					comboSlotOut = comboSlot;
					comboSlotItemOut = item;
					return true;
				}
			}
		}
		comboSlotOut = null;
		comboSlotItemOut = null;
		return false;
	}

	public void ResetSlotCaches()
	{
		_slotCaches = _slotCaches.ClearOrCreate();
		foreach (Transform item in base.transform)
		{
			if (item.CompareTag("CharacterScriptIgnore"))
			{
				continue;
			}
			SimpleCharactersItemCache[] array = new SimpleCharactersItemCache[item.childCount];
			int num = 0;
			foreach (Transform item2 in item)
			{
				array[num] = new SimpleCharactersItemCache(item2);
				num++;
			}
			_slotCaches.Add(new NormalSlot(item, array));
		}
	}

	public bool TryGetSimpleCharactersSlotCache(string id, out NormalSlot normalSlot)
	{
		int num = _slotCaches.FindIndex((NormalSlot x) => string.Equals(x.GetId(), id, StringComparison.OrdinalIgnoreCase));
		if (num < 0)
		{
			normalSlot = null;
			return false;
		}
		normalSlot = _slotCaches[num];
		return true;
	}

	public IEnumerable<ISimpleCharacterSlot> GetSlots()
	{
		return comboSlots.Cast<ISimpleCharacterSlot>().Concat(_slotCaches);
	}

	public IEnumerable<string> GetItems()
	{
		return GetSlots().SelectMany((ISimpleCharacterSlot t) => t.GetItemIds());
	}

	public bool SlotExists(string slotId)
	{
		ISimpleCharacterSlot simpleCharacterSlot;
		return TryGetSlot(slotId, out simpleCharacterSlot);
	}

	public bool TryGetSlot(string slotId, out ISimpleCharacterSlot simpleCharacterSlot)
	{
		if (TryGetSimpleCharactersSlotCache(slotId, out var normalSlot))
		{
			simpleCharacterSlot = normalSlot;
			return true;
		}
		if (TryGetSlotComboSlot(slotId, out var comboSlot))
		{
			simpleCharacterSlot = comboSlot;
			return true;
		}
		simpleCharacterSlot = null;
		return false;
	}

	public bool ItemExists(string slotId, string itemId)
	{
		if (TryGetSimpleCharactersSlotCache(slotId, out var normalSlot) && normalSlot.HasItem(itemId))
		{
			return true;
		}
		if (TryGetSlotComboSlot(slotId, out var comboSlot) && comboSlot.HasItem(itemId))
		{
			return true;
		}
		return false;
	}

	public void ResetSlots()
	{
		foreach (NormalSlot slotCache in _slotCaches)
		{
			slotCache.SlotTransform.gameObject.SetActive(value: true);
			SimpleCharactersItemCache[] items = slotCache.Items;
			for (int i = 0; i < items.Length; i++)
			{
				items[i].ItemTransform.gameObject.SetActive(value: false);
			}
		}
	}

	public void ResetScale()
	{
		transform.localScale = DefaultScale;
	}

	public void ResetAll()
	{
		ResetSlots();
		SetDefaultEmote();
		transform.localScale = DefaultScale;
	}

	public SpriteRenderer[] AllActiveSpriteRenderers()
	{
		return GetComponentsInChildren<SpriteRenderer>();
	}

	public void SetDefaultEmote(bool instantly = true)
	{
		SetEmote(defaultActiveItems.Select((StoryEmoteItemReference t) => new EmoteData
		{
			instant = instantly,
			ItemId = t.ItemId,
			SlotId = t.SlotId
		}));
	}

	public void SetEmote(IEnumerable<EmoteData> emoteData)
	{
		SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.StartFade(this, emoteData);
	}

	public Result IsValidEmote(EmoteData emoteData)
	{
		if (TryGetSlot(emoteData.SlotId, out var simpleCharacterSlot))
		{
			if (simpleCharacterSlot.HasItem(emoteData.ItemId))
			{
				return Result.Success;
			}
			return Result.FailWithDescription("Emote item " + emoteData.ItemId + " not found in slot " + emoteData.SlotId + "! \nAvailable items: " + string.Join(", ", from t in simpleCharacterSlot.GetItems()
				select t.GetId()));
		}
		return Result.FailWithDescription("Slot " + emoteData.SlotId + " not found! Available slots:" + string.Join(", ", from t in GetSlots()
			select t.GetId()));
	}

	public void OnValidate()
	{
		ResetSlotCaches();
	}

	public string GenerateSlotSummary()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine("***Default:***");
			foreach (StoryEmoteItemReference defaultActiveItem in defaultActiveItems)
			{
				stringBuilder.AppendLine("#" + DefaultId + "." + defaultActiveItem.SlotId + "." + defaultActiveItem.ItemId);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("***Combo slots:***");
			foreach (ComboSlot comboSlot in comboSlots)
			{
				stringBuilder.Append(comboSlot.Id);
				stringBuilder.AppendLine(":");
				foreach (ComboSlotItem item in comboSlot.Items)
				{
					stringBuilder.Append("#" + DefaultId + "." + comboSlot.Id + "." + item.Id + " => (");
					stringBuilder.AppendJoin(", ", item.ItemsInTheComboSlot.Select((StoryEmoteItemReference t) => t.SlotId + "." + t.ItemId));
					stringBuilder.AppendLine(")");
				}
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("***Normal elements:***");
			foreach (NormalSlot slotCache in _slotCaches)
			{
				stringBuilder.Append(slotCache.GetId());
				stringBuilder.AppendLine(":");
				SimpleCharactersItemCache[] items = slotCache.Items;
				foreach (SimpleCharactersItemCache simpleCharactersItemCache in items)
				{
					stringBuilder.AppendLine("#" + DefaultId + "." + slotCache.GetId() + "." + simpleCharactersItemCache.Id);
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}
	}

	public void Initialize(string id)
	{
		InstanceId = id;
		DefaultScale = transform.localScale;
	}
}
