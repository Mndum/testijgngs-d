using Fungus;
using GameItems;
using UnityEngine;

[CommandInfo("My", "Add Item", "Adds item to inventory", 0)]
public class AddItem : Command
{
	public Item item;

	public override void OnEnter()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.AddItem(item.Clone());
		Continue();
	}

	public override string GetSummary()
	{
		return $"Adds {(ItemEnum)item.GameId.Id} to inventory";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}
}
