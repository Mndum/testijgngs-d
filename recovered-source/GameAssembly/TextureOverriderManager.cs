using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using IC.AutomaticReference;
using UnityEngine;

public class TextureOverriderManager : MonoBehaviour
{
	public static string RoomID = "Room";

	public static string ClothierID = "Clothier";

	private static List<TextureOverriderManager> _allManagers = new List<TextureOverriderManager>();

	private Dictionary<string, SpriteRendererAutomaticReferenceHandler> _spriteAdapters = new Dictionary<string, SpriteRendererAutomaticReferenceHandler>();

	[field: SerializeField]
	public string Id { get; private set; }

	public static ReadOnlyCollection<TextureOverriderManager> AllManagers => _allManagers.AsReadOnly();

	public static TextureOverriderManager RoomManager => GetTextureOverriderManager(RoomID);

	public static TextureOverriderManager ClothierManager => GetTextureOverriderManager(ClothierID);

	private IEnumerable<SpriteRendererAutomaticReferenceHandler> AllAdapters => _spriteAdapters.Values;

	public static TextureOverriderManager GetTextureOverriderManager(string id)
	{
		TextureOverriderManager textureOverriderManager = _allManagers.FirstOrDefault((TextureOverriderManager t) => t.Id == id);
		if (textureOverriderManager == null)
		{
			Debug.LogError("TextureOverriderManager with id \"" + id + "\" not found!\nPotential matches: " + string.Join(", ", _allManagers.Select((TextureOverriderManager t) => "\"" + t.Id + "\"")));
			return null;
		}
		return textureOverriderManager;
	}

	public void SpecialEffectsActionLogic(bool value, Item item, string targetId, IReference<Sprite> sprite)
	{
		if (item.IsEquipped() & value)
		{
			SetSprite(item.GetUniqueGuid(), targetId, sprite);
		}
		else
		{
			RestoreSprite(item.GetUniqueGuid(), targetId, sprite);
		}
	}

	public Action<bool, Item> GetSpecialEffectsAction(string targetId, IReference<Sprite> sprite)
	{
		return (bool value, Item item) =>
		{
			SpecialEffectsActionLogic(value, item, targetId, sprite);
		};
	}

	public void SetSprite(string targetId, IReference<Sprite> sprite)
	{
		if (Item.CurrentSpecialEffectsActionItem == null)
		{
			Debug.LogError("Trying to call SetSprite without guid outside of SpecialEffectAction!!! That's not allowed. Use a version that takes guid as the first parameter!");
		}
		else
		{
			SetSprite(Item.CurrentSpecialEffectsActionItem.GetUniqueGuid(), targetId, sprite);
		}
	}

	public void RestoreSprite(string targetId, IReference<Sprite> sprite)
	{
		if (Item.CurrentSpecialEffectsActionItem == null)
		{
			Debug.LogError("Trying to call SetSprite without guid outside of SpecialEffectAction!!! That's not allowed. Use a version that takes guid as the first parameter!");
		}
		else
		{
			RestoreSprite(Item.CurrentSpecialEffectsActionItem.GetUniqueGuid(), targetId, sprite);
		}
	}

	public void SetSprite(Guid uniqueGuid, string targetId, IReference<Sprite> sprite)
	{
		if (TryGetValue(targetId, out var found))
		{
			found.SetOverrideSprite(uniqueGuid, sprite);
		}
	}

	public void RestoreSprite(Guid uniqueGuid, string targetId, IReference<Sprite> sprite = null)
	{
		if (TryGetValue(targetId, out var found))
		{
			found.ClearOverrideSpriteIfCurrent(uniqueGuid);
		}
	}

	private bool TryGetValue(string id, out SpriteRendererAutomaticReferenceHandler found)
	{
		if (!_spriteAdapters.TryGetValue(id, out found))
		{
			Debug.LogError("Adapter with id \"" + id + "\" not found in TextureOverriderManager with id \"" + Id + "\"!\nPotential matches: " + string.Join(", ", _spriteAdapters.Select((KeyValuePair<string, SpriteRendererAutomaticReferenceHandler> t) => "\"" + t.Key + "\"")));
			return false;
		}
		return true;
	}

	public void RestoreAll()
	{
		foreach (SpriteRendererAutomaticReferenceHandler allAdapter in AllAdapters)
		{
			allAdapter.ClearOverrideSprite();
		}
	}

	private void PopulateAdaptersRecursive(Transform t, string path = "")
	{
		string text = path + "/";
		foreach (Transform item in t)
		{
			SpriteRendererAutomaticReferenceHandler component = item.GetComponent<SpriteRendererAutomaticReferenceHandler>();
			if ((object)component != null && component != null)
			{
				_spriteAdapters[text + item.name] = component;
			}
			if (item.childCount != 0)
			{
				PopulateAdaptersRecursive(item, text + item.name);
			}
		}
	}

	private void PopulateAdapters()
	{
		_spriteAdapters = _spriteAdapters.ClearOrCreate();
		PopulateAdaptersRecursive(transform);
	}

	public void Awake()
	{
		_allManagers.Add(this);
		PopulateAdapters();
	}

	public string GenerateSummary()
	{
		PopulateAdapters();
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<string, SpriteRendererAutomaticReferenceHandler> spriteAdapter in _spriteAdapters)
		{
			stringBuilder.Append("\"" + spriteAdapter.Key + "\" - ");
			if (spriteAdapter.Value.DefaultSprite != null && !string.IsNullOrWhiteSpace(spriteAdapter.Value.DefaultSprite.resourcesReferenceLoader.path))
			{
				stringBuilder.Append(" default sprite in \"" + spriteAdapter.Value.DefaultSprite.resourcesReferenceLoader.path + "\"");
			}
			else
			{
				stringBuilder.Append(" no default sprite");
			}
			stringBuilder.AppendLine();
		}
		return stringBuilder.ToString();
	}
}
