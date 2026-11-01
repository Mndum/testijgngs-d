using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("My", "Fade Character Element", "Fades Character Element", 0)]
public class FadeCharacterElement : Command
{
	public string targetCharacterId;

	[SerializeField]
	public List<EmoteData> fadeData;

	public override void OnEnter()
	{
		SimpleCharacter activeCharacterById = SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.GetActiveCharacterById(targetCharacterId);
		if (activeCharacterById != null)
		{
			activeCharacterById.SetEmote(fadeData);
		}
		else
		{
			Debug.LogError("Character not found by id " + targetCharacterId);
		}
		Continue();
	}

	public override string GetSummary()
	{
		SimpleCharacter characterByDefaultId = SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.GetCharacterByDefaultId(targetCharacterId);
		if (characterByDefaultId != null)
		{
			string text = characterByDefaultId.name + " ";
			{
				foreach (EmoteData fadeDatum in fadeData)
				{
					text = text + " " + fadeDatum.SlotId + "." + fadeDatum.ItemId + " " + (fadeDatum.instant ? "(instantly)" : "");
					if (!characterByDefaultId.ItemExists(fadeDatum.SlotId, fadeDatum.ItemId))
					{
						return "Error: " + fadeDatum.SlotId + "." + fadeDatum.ItemId + " NOT FOUND!!!!";
					}
				}
				return text;
			}
		}
		return "Error: No character selected!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
