using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class ItemModifier : SpecialVariablesHolder, ISerializationCallbackReceiver, IGameStatModifier
{
	[SerializeField]
	private GameId _id;

	private ItemModifierPrefab _itemModifierPrefab;

	[SerializeField]
	private float _l = 1f;

	[field: NonSerialized]
	public Item SourceItem { get; private set; }

	public bool AllowedMultiple => GetItemModifierPrefab().AllowedMultiple;

	public GameId Id => _id;

	public float Level
	{
		get
		{
			return _l;
		}
		set
		{
			_l = value;
		}
	}

	public string Char => GetItemModifierPrefab().GetModifierChar(this);

	public string Name => GetItemModifierPrefab().GetModifierName(this);

	public string Description => GetItemModifierPrefab().GetModifierDescription(this);

	public Color32[] Colors => GetItemModifierPrefab().GetModifierColors(this);

	public int Order => GetItemModifierPrefab().DefaultGameStatModifierOrder;

	public bool IsValid => GetItemModifierPrefab().IsValidGameStatModifier;

	public ItemModifier(Item item, GameId itemModifierId, float targetRarity)
	{
		_id = itemModifierId;
		Init(item);
		GetItemModifierPrefab().FillCreatedItemModifier(this, targetRarity);
	}

	public void Init(Item item)
	{
		SourceItem = item;
	}

	public ItemModifierPrefab GetItemModifierPrefab()
	{
		return _itemModifierPrefab ?? (_itemModifierPrefab = ItemModifierPrefabManager.GetItemModifierPrefab(_id));
	}

	public float GetLinearBoostFromLevel(float boostPerLevel)
	{
		return _l * boostPerLevel;
	}

	public float GetQuadraticBoostFromLevel(float boostPerLevel)
	{
		return _l * _l * boostPerLevel;
	}

	public float ApplyDirect(ItemModifierDirectItemChangeFloat target, float value)
	{
		return GetItemModifierPrefab().DirectModifierFloat(this, target, value);
	}

	public void ApplyDirect(ItemModifierDirectItemChangeStringBuilder target, StringBuilder value)
	{
		GetItemModifierPrefab().DirectModifierStringBuilder(this, target, value);
	}

	public void FillGameStatModifiers(List<IGameStatModifier> gameStatModifiers)
	{
		GetItemModifierPrefab().FillAdditionalGameStatModifiers(this, gameStatModifiers);
	}

	public Color32 GetFirstColor()
	{
		Color32[] modifierColors = GetItemModifierPrefab().GetModifierColors(this);
		if (modifierColors.Length == 0)
		{
			return MyColor32Presets.Black;
		}
		return modifierColors[0];
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		OnAfterDeserializeSpecialVariableHolder();
	}

	public ItemModifier Clone()
	{
		ItemModifier itemModifier = (ItemModifier)MemberwiseClone();
		CloneSpecialVariables(itemModifier);
		return itemModifier;
	}

	public void ApplyToGameStats(GameVariables gameVariables, GameStats gameStats)
	{
		GetItemModifierPrefab().ApplyAsGameStatModifier(this, gameVariables, gameStats);
	}
}
