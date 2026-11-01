using Fungus;
using UnityEngine;
using UnityEngine.Serialization;

[CommandInfo("My", "Hide Characters", "Hides character", 0)]
internal class HideCharacter : Command
{
	public string targetCharacterId;

	[FormerlySerializedAs("animation")]
	[SerializeField]
	public SimpleCharacterSingleton.AnimationsList transitionAnimation;

	[SerializeField]
	public float time = 0.5f;

	private void Finish(SimpleCharacter simpleCharacter)
	{
		SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.HideCharacter(simpleCharacter);
		Continue();
	}

	public override void OnEnter()
	{
		SimpleCharacter character = SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.GetActiveCharacterById(targetCharacterId);
		if (character == null)
		{
			Continue();
			return;
		}
		SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.DoAnimation(character, transitionAnimation, () =>
		{
			Finish(character);
		}, time);
	}

	public override string GetSummary()
	{
		SimpleCharacter characterByDefaultId = SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.GetCharacterByDefaultId(targetCharacterId);
		if (characterByDefaultId != null)
		{
			return $"{characterByDefaultId.name} disappears animation: {transitionAnimation}";
		}
		return "No character selected!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
