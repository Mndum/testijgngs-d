using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fungus;
using IC.AutomaticReference;
using UnityEngine;
using UnityEngine.Serialization;

[CommandInfo("My", "Display Character", "Displays character", 0)]
public class DisplayCharacter : Command
{
	public SerializableAutomaticReference<SimpleCharacter> characterRef;

	[SerializeField]
	public BooleanData setInvisible = new BooleanData(v: true);

	[SerializeField]
	public Vector2 offset;

	[SerializeField]
	public float scale = 1f;

	[SerializeField]
	public View targetView;

	[FormerlySerializedAs("animation")]
	[SerializeField]
	public SimpleCharacterSingleton.AnimationsList transitionAnimation;

	[SerializeField]
	public float time = 0.5f;

	[SerializeField]
	public string forceId = "";

	[SerializeField]
	public List<EmoteData> fadeData;

	public override void OnEnter()
	{
		SimpleCharacterSingleton instance = SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance;
		SimpleCharacter simpleCharacter = instance.InstantiatePrefab(characterRef, forceId);
		if (simpleCharacter != null)
		{
			simpleCharacter.gameObject.SetActive(value: true);
			simpleCharacter.ResetAll();
			if (fadeData.Any())
			{
				instance.StartFade(simpleCharacter, fadeData);
			}
			Transform transform = simpleCharacter.transform;
			Vector3 localScale = transform.localScale;
			localScale = new Vector3(localScale.x * scale, localScale.y * scale, localScale.z * scale);
			transform.localScale = localScale;
			SpriteRenderer[] componentsInChildren = simpleCharacter.gameObject.GetComponentsInChildren<SpriteRenderer>();
			if (setInvisible.Value)
			{
				SpriteRenderer[] array = componentsInChildren;
				foreach (SpriteRenderer obj in array)
				{
					obj.color = obj.color.SetAlpha(0f);
				}
			}
			else
			{
				SpriteRenderer[] array = componentsInChildren;
				foreach (SpriteRenderer obj2 in array)
				{
					obj2.color = obj2.color.SetAlpha(1f);
				}
			}
			View view = (targetView ? targetView : SingletonMonoBehaviour<ViewSingleton>.Instance.ViewForCharacters);
			transform.position = view.transform.position + new Vector3(offset.x, simpleCharacter.yoffest + offset.y, 0f);
			SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.DoAnimation(simpleCharacter, transitionAnimation, null, time);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (!characterRef.IsNull())
		{
			SimpleCharacter simpleCharacter = characterRef.Get();
			StringBuilder stringBuilder;
			if ((object)simpleCharacter != null)
			{
				using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
				{
					if (string.IsNullOrWhiteSpace(forceId))
					{
						stringBuilder.Append(simpleCharacter.DefaultId + ":");
					}
					else
					{
						stringBuilder.Append(simpleCharacter.DefaultId + "(with id " + forceId + "):");
					}
					stringBuilder.Append((targetView != null) ? ("(Target view: " + targetView.name + ") ") : "(Current View) ");
					if (setInvisible.Value)
					{
						switch (transitionAnimation)
						{
						case SimpleCharacterSingleton.AnimationsList.Nothing:
							return "WARNING: Invisible character without transition animation!";
						case SimpleCharacterSingleton.AnimationsList.TransparencyTo1:
							break;
						default:
							return $"WARNING: Invisible character with a invalid transition animation ({transitionAnimation})!";
						}
						stringBuilder.Append($"{transitionAnimation}");
					}
					else
					{
						switch (transitionAnimation)
						{
						case SimpleCharacterSingleton.AnimationsList.Nothing:
							stringBuilder.Append("Instantly visible");
							break;
						case SimpleCharacterSingleton.AnimationsList.EnterScreen:
						case SimpleCharacterSingleton.AnimationsList.EnterScreenFromLeft:
							stringBuilder.Append($"{transitionAnimation}");
							break;
						default:
							return $"WARNING: Visible character with a invalid transition animation ({transitionAnimation})!";
						}
					}
					if (fadeData.Any())
					{
						stringBuilder.Append("   Elements:");
						foreach (EmoteData fadeDatum in fadeData)
						{
							stringBuilder.Append(" " + fadeDatum.SlotId + "." + fadeDatum.ItemId + " " + (fadeDatum.instant ? "(instantly)" : ""));
							if (!simpleCharacter.ItemExists(fadeDatum.SlotId, fadeDatum.ItemId))
							{
								return "Error: " + fadeDatum.SlotId + "." + fadeDatum.ItemId + " NOT FOUND!!!!";
							}
						}
					}
					return stringBuilder.ToString();
				}
			}
		}
		return "No character selected!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
