using System.Globalization;
using Fungus;
using UnityEngine;

[CommandInfo("My", "Animates Character", "Animates target character", 0)]
internal class AnimateCharacter : Command
{
	public string targetCharacterId;

	public SimpleCharacterSingleton.AnimationsList animation;

	public float time = 0.5f;

	public override void OnEnter()
	{
		SimpleCharacter activeCharacterById = SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.GetActiveCharacterById(targetCharacterId);
		if (activeCharacterById == null)
		{
			Continue();
		}
		else
		{
			SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.DoAnimation(activeCharacterById, animation, Continue, time);
		}
	}

	public override string GetSummary()
	{
		SimpleCharacter characterByDefaultId = SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.GetCharacterByDefaultId(targetCharacterId);
		if (characterByDefaultId != null)
		{
			return $"{characterByDefaultId.name} gets animated {animation} for {time}";
		}
		return "No character selected!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}

	public object ToSpecialLineString()
	{
		return string.Format("{0}.{1}({2},{3})", targetCharacterId, "animate", animation, time.ToString(CultureInfo.InvariantCulture));
	}
}
