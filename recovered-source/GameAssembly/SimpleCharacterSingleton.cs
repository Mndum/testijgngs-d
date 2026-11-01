using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Fungus;
using IC.AutomaticReference;
using UnityEngine;

public class SimpleCharacterSingleton : SingletonMonoBehaviour<SimpleCharacterSingleton>
{
	public enum AnimationsList
	{
		Nothing,
		EnterScreen,
		LeaveScreen,
		Shake,
		EnterScreenFromLeft,
		LeaveScreenToLeft,
		TransparencyTo0,
		TransparencyTo1,
		MoveToLeft,
		MoveToRight,
		MoveToLeftAbs,
		MoveToRightAbs,
		LeaveScreenToBottom
	}

	public class SlotFadeData
	{
		public SimpleCharacter.NormalSlot NormalSlot;

		public HashSet<SpriteRenderer> EnabledBefore = new HashSet<SpriteRenderer>();

		public HashSet<SpriteRenderer> EnabledInstantly = new HashSet<SpriteRenderer>();

		public HashSet<SpriteRenderer> ToFadeIn = new HashSet<SpriteRenderer>();

		public HashSet<SpriteRenderer> ToFadeOut = new HashSet<SpriteRenderer>();

		public HashSet<SpriteRenderer> TouchedPositively = new HashSet<SpriteRenderer>();

		public SlotFadeData(SimpleCharacter.NormalSlot normalSlot)
		{
			NormalSlot = normalSlot;
			EnabledBefore.UnionWith(from t in normalSlot.GetAllRenderers()
				where t.gameObject.activeSelf
				select t);
			EnabledInstantly.UnionWith(EnabledBefore);
		}

		public IEnumerable<SpriteRenderer> GetAllToEnableInstantly()
		{
			foreach (SpriteRenderer item in EnabledInstantly)
			{
				if (!EnabledBefore.Contains(item))
				{
					yield return item;
				}
			}
		}

		public IEnumerable<SpriteRenderer> GetAllToDisableInstantly()
		{
			foreach (SpriteRenderer item in EnabledBefore)
			{
				if (!EnabledInstantly.Contains(item))
				{
					yield return item;
				}
			}
		}

		public void Add(string item, bool instant)
		{
			if (true)
			{
				ToFadeIn.Clear();
				if (instant)
				{
					ToFadeOut.Clear();
					EnabledInstantly.Clear();
				}
				else
				{
					ToFadeOut.UnionWith(EnabledInstantly);
				}
			}
			if (!NormalSlot.TryGetItem(item, out var item2))
			{
				return;
			}
			SpriteRenderer itemSpriteRenderer = item2.ItemSpriteRenderer;
			TouchedPositively.Add(itemSpriteRenderer);
			if (instant)
			{
				ToFadeIn.Remove(itemSpriteRenderer);
				ToFadeOut.Remove(itemSpriteRenderer);
				EnabledInstantly.Add(itemSpriteRenderer);
				return;
			}
			if (!EnabledInstantly.Contains(itemSpriteRenderer))
			{
				ToFadeIn.Add(itemSpriteRenderer);
			}
			ToFadeOut.Remove(itemSpriteRenderer);
		}

		public void Finish()
		{
			ToFadeOut.IntersectWith(EnabledInstantly);
		}
	}

	[ReadOnly]
	[SerializeField]
	private SerializableDictionary<string, SerializableAutomaticReference<SimpleCharacter>> runtimeCharacterReferences = new SerializableDictionary<string, SerializableAutomaticReference<SimpleCharacter>>();

	private List<SimpleCharacter> _activeCharacters = new List<SimpleCharacter>();

	private Dictionary<SimpleCharacter, ManagedCoroutine> currentAnimations = new Dictionary<SimpleCharacter, ManagedCoroutine>();

	public SimpleCharacter GetCharacterByDefaultId(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return null;
		}
		if (runtimeCharacterReferences.TryGetValue(id, out var value))
		{
			return value.Get();
		}
		foreach (KeyValuePair<string, SerializableAutomaticReference<SimpleCharacter>> runtimeCharacterReference in runtimeCharacterReferences)
		{
			if (string.Equals(runtimeCharacterReference.Key, id, StringComparison.OrdinalIgnoreCase))
			{
				return runtimeCharacterReference.Value.Get();
			}
		}
		return null;
	}

	public SimpleCharacter GetActiveCharacterById(string id)
	{
		return _activeCharacters.FirstOrDefault((SimpleCharacter t) => string.Equals(t.InstanceId, id, StringComparison.OrdinalIgnoreCase));
	}

	public bool TryGetOnlyActiveCharacterOrErr(out SimpleCharacter character)
	{
		if (_activeCharacters.Count <= 0)
		{
			character = null;
			Debug.LogError("No active character found!");
			return false;
		}
		if (_activeCharacters.Count > 1)
		{
			Debug.LogError($"Expected a single character. Got {_activeCharacters.Count}. Returning first anyway!");
		}
		character = _activeCharacters[0];
		return true;
	}

	public bool TryGetActiveCharacterById(string id, out SimpleCharacter character)
	{
		int num = _activeCharacters.FindIndex((SimpleCharacter t) => string.Equals(t.InstanceId, id, StringComparison.OrdinalIgnoreCase));
		if (num < 0)
		{
			character = null;
			return false;
		}
		character = _activeCharacters[num];
		return character != null;
	}

	public SimpleCharacter InstantiatePrefab(SerializableAutomaticReference<SimpleCharacter> prefabRef, string forceId = null)
	{
		SimpleCharacter simpleCharacter = prefabRef.Get();
		string defaultId = simpleCharacter.DefaultId;
		if (string.IsNullOrWhiteSpace(forceId))
		{
			forceId = defaultId;
		}
		int num = _activeCharacters.FindIndex((SimpleCharacter t) => t.InstanceId == forceId);
		if (num >= 0)
		{
			return _activeCharacters[num];
		}
		SimpleCharacter simpleCharacter2 = UnityEngine.Object.Instantiate(simpleCharacter, transform);
		simpleCharacter2.Initialize(forceId);
		_activeCharacters.Add(simpleCharacter2);
		return simpleCharacter2;
	}

	public void HideCharacter(SimpleCharacter simpleCharacter)
	{
		KillAnimation(simpleCharacter);
		_activeCharacters.Remove(simpleCharacter);
		UnityEngine.Object.Destroy(simpleCharacter.gameObject);
	}

	public void HideAllCharacters()
	{
		KillAllAnimations();
		foreach (SimpleCharacter activeCharacter in _activeCharacters)
		{
			UnityEngine.Object.Destroy(activeCharacter.gameObject);
		}
		_activeCharacters.Clear();
	}

	private (IEnumerator enumerator, Action finish) MoveCharacter(SimpleCharacter simpleCharacter, Vector3 currentPos, Vector3 targetPos, float time)
	{
		Transform tr = simpleCharacter.transform;
		return (enumerator: CoroutineUtilities.GenericTween((float t) =>
		{
			tr.position = Vector3.Lerp(currentPos, targetPos, t);
		}, time), finish: () =>
		{
			tr.position = targetPos;
		});
	}

	private (IEnumerator enumerator, Action finish) MoveCharacterIntoTheView(SimpleCharacter simpleCharacter, float time, float offset)
	{
		Vector3 position = simpleCharacter.transform.position;
		Vector3 currentPos = position;
		currentPos.x += offset;
		return MoveCharacter(simpleCharacter, currentPos, position, time);
	}

	private (IEnumerator enumerator, Action finish) MoveCharacterHorizontally(SimpleCharacter simpleCharacter, float time, float offset)
	{
		Vector3 position = simpleCharacter.transform.position;
		Vector3 targetPos = position + new Vector3(offset, 0f, 0f);
		return MoveCharacter(simpleCharacter, position, targetPos, time);
	}

	private (IEnumerator enumerator, Action finish) MoveCharacterHorizontallyAbsolute(SimpleCharacter simpleCharacter, float time, float offset)
	{
		Vector3 position = simpleCharacter.transform.position;
		View currentView = SingletonMonoBehaviour<ViewSingleton>.Instance.CurrentView;
		if (currentView == null)
		{
			Debug.LogError("Current view is null!");
			return MoveCharacter(simpleCharacter, position, position, time);
		}
		Vector3 targetPos = currentView.transform.position + new Vector3(offset, simpleCharacter.yoffest, 0f);
		return MoveCharacter(simpleCharacter, position, targetPos, time);
	}

	private (IEnumerator enumerator, Action finish) MoveCharacterOutOfTheView(SimpleCharacter simpleCharacter, float time, System.Numerics.Vector2 offset)
	{
		Vector3 position = simpleCharacter.transform.position;
		Vector3 targetPos = position;
		targetPos += new Vector3(offset.X, offset.Y, 0f);
		return MoveCharacter(simpleCharacter, position, targetPos, time);
	}

	private (IEnumerator enumerator, Action finish) ShakeCharacter(SimpleCharacter simpleCharacter, float time)
	{
		Vector3 startPosition = simpleCharacter.transform.localPosition;
		return (enumerator: CoroutineUtilities.Vibrate(simpleCharacter.transform, time, 0.1f, 0.1f), finish: () =>
		{
			simpleCharacter.transform.localPosition = startPosition;
		});
	}

	public Dictionary<SimpleCharacter.NormalSlot, SlotFadeData> ProcessChanges(SimpleCharacter simpleCharacter, IEnumerable<EmoteData> fadeData)
	{
		Dictionary<SimpleCharacter.NormalSlot, SlotFadeData> slotsAffected = new Dictionary<SimpleCharacter.NormalSlot, SlotFadeData>();
		foreach (EmoteData fadeDatum in fadeData)
		{
			SimpleCharacter.ComboSlot comboSlot;
			if (simpleCharacter.TryGetSimpleCharactersSlotCache(fadeDatum.SlotId, out var normalSlot))
			{
				GetAffectedSlot(normalSlot).Add(fadeDatum.ItemId, fadeDatum.instant);
			}
			else if (simpleCharacter.TryGetSlotComboSlot(fadeDatum.SlotId, out comboSlot))
			{
				if (!comboSlot.TryGetItem(fadeDatum.ItemId, out var item))
				{
					continue;
				}
				StoryEmoteItemReference[] itemsInTheComboSlot = item.ItemsInTheComboSlot;
				foreach (StoryEmoteItemReference storyEmoteItemReference in itemsInTheComboSlot)
				{
					if (simpleCharacter.TryGetSimpleCharactersSlotCache(storyEmoteItemReference.SlotId, out var normalSlot2))
					{
						GetAffectedSlot(normalSlot2).Add(storyEmoteItemReference.ItemId, fadeDatum.instant);
					}
					else
					{
						Debug.LogError("Slot " + storyEmoteItemReference.SlotId + " not found!");
					}
				}
			}
			else
			{
				Debug.LogError("Slot " + fadeDatum.SlotId + " not found!");
			}
		}
		foreach (KeyValuePair<SimpleCharacter.NormalSlot, SlotFadeData> item2 in slotsAffected)
		{
			item2.Value.Finish();
		}
		return slotsAffected;
		SlotFadeData GetAffectedSlot(SimpleCharacter.NormalSlot slot)
		{
			if (!slotsAffected.TryGetValue(slot, out var value))
			{
				return slotsAffected[slot] = new SlotFadeData(slot);
			}
			return value;
		}
	}

	public void StartFade(SimpleCharacter simpleCharacter, IEnumerable<EmoteData> fadeData)
	{
		StartFade(simpleCharacter, fadeData, out var _);
	}

	public bool StartFade(SimpleCharacter simpleCharacter, IEnumerable<EmoteData> fadeData, out Dictionary<SimpleCharacter.NormalSlot, SlotFadeData> slotsAffected)
	{
		if (!simpleCharacter.gameObject.activeInHierarchy)
		{
			Debug.LogWarning("Character " + simpleCharacter.name + " is not active! Fade not working! \n" + $"Active self: {simpleCharacter.gameObject.activeSelf}\n" + $"Active in hierarchy: {simpleCharacter.gameObject.activeInHierarchy}\n" + $"Parent: {simpleCharacter.transform.parent}");
			slotsAffected = null;
			return false;
		}
		KillAnimation(simpleCharacter);
		slotsAffected = ProcessChanges(simpleCharacter, fadeData);
		SpriteRenderer component = simpleCharacter.GetComponent<SpriteRenderer>();
		float mainAlpha = ((component != null) ? component.color.a : 1f);
		foreach (KeyValuePair<SimpleCharacter.NormalSlot, SlotFadeData> item in slotsAffected)
		{
			item.Key.SlotTransform.gameObject.SetActive(value: true);
			foreach (SpriteRenderer item2 in item.Value.GetAllToEnableInstantly())
			{
				item2.gameObject.SetActive(value: true);
				item2.color = item2.color.SetAlpha(mainAlpha);
			}
			foreach (SpriteRenderer item3 in item.Value.GetAllToDisableInstantly())
			{
				item3.gameObject.SetActive(value: false);
			}
		}
		List<Action<float>> fadeInFunctions = new List<Action<float>>();
		List<Action<float>> fadeOutFunctions = new List<Action<float>>();
		List<Action> onFinishedFunctions = new List<Action>();
		foreach (KeyValuePair<SimpleCharacter.NormalSlot, SlotFadeData> item4 in slotsAffected)
		{
			if (item4.Value.ToFadeIn.Count == 0 && item4.Value.ToFadeOut.Count == 0)
			{
				continue;
			}
			if (item4.Value.ToFadeOut.Count > 0)
			{
				SpriteRenderer[] fadingOutRenderers = item4.Value.ToFadeOut.ToArray();
				float[] fadingOutRenderersTransparencies = fadingOutRenderers.Select((SpriteRenderer t) => t.color.a).ToArray();
				Action<float> fadeOut = (float t) =>
				{
					for (int i = 0; i < fadingOutRenderers.Length; i++)
					{
						SpriteRenderer obj = fadingOutRenderers[i];
						obj.color = obj.color.SetAlpha(Mathf.Lerp(fadingOutRenderersTransparencies[i], 0f, t));
					}
				};
				fadeOutFunctions.Add(fadeOut);
				onFinishedFunctions.Add(() =>
				{
					fadeOut(1f);
					SpriteRenderer[] array2 = fadingOutRenderers;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i].gameObject.SetActive(value: false);
					}
				});
			}
			if (item4.Value.ToFadeIn.Count <= 0)
			{
				continue;
			}
			SpriteRenderer[] fadingInRenderers = item4.Value.ToFadeIn.ToArray();
			float[] fadingInRenderersTransparencies = fadingInRenderers.Select((SpriteRenderer t) => t.color.a).ToArray();
			SpriteRenderer[] array = fadingInRenderers;
			for (int num = 0; num < array.Length; num++)
			{
				array[num].gameObject.SetActive(value: true);
			}
			Action<float> fadeIn = (float t) =>
			{
				for (int i = 0; i < fadingInRenderers.Length; i++)
				{
					SpriteRenderer obj = fadingInRenderers[i];
					obj.color = obj.color.SetAlpha(Mathf.Lerp(fadingInRenderersTransparencies[i], mainAlpha, t));
				}
			};
			fadeInFunctions.Add(fadeIn);
			onFinishedFunctions.Add(() =>
			{
				fadeIn(1f);
			});
		}
		if (fadeInFunctions.Count == 0 && fadeOutFunctions.Count == 0)
		{
			return true;
		}
		float time = 0.1f;
		StartAnimation(simpleCharacter, E(), () =>
		{
			foreach (Action item5 in onFinishedFunctions)
			{
				item5();
			}
		});
		return true;
		IEnumerator E()
		{
			if (fadeInFunctions.Any())
			{
				yield return CoroutineUtilities.GenericTweenEaseOutQuart((float t) =>
				{
					foreach (Action<float> item6 in fadeInFunctions)
					{
						item6(t);
					}
				}, 0f, 1f, time / 2f);
			}
			if (fadeOutFunctions.Any())
			{
				yield return CoroutineUtilities.GenericTweenEaseOutQuart((float t) =>
				{
					foreach (Action<float> item7 in fadeOutFunctions)
					{
						item7(t);
					}
				}, 0f, 1f, time / 2f);
			}
		}
	}

	private void FadeCharacterTransparency(SimpleCharacter simpleCharacter, float startTransparency, float targetTransparency, float time, Action OnFinished)
	{
		KillAnimation(simpleCharacter);
		SpriteRenderer[] spriteRenderers = simpleCharacter.AllActiveSpriteRenderers();
		StartAnimation(simpleCharacter, CoroutineUtilities.GenericTweenEaseOutQuart(Progress, 0f, 1f, time), () =>
		{
			Progress(1f);
			OnFinished?.Invoke();
		});
		void Progress(float t)
		{
			for (int i = 0; i < spriteRenderers.Length; i++)
			{
				float alpha = Mathf.Lerp(startTransparency, targetTransparency, t);
				spriteRenderers[i].color = spriteRenderers[i].color.SetAlpha(alpha);
			}
		}
	}

	public void KillAnimation(SimpleCharacter simpleCharacter)
	{
		if (currentAnimations.TryGetValue(simpleCharacter, out var value))
		{
			value.Stop(this);
			currentAnimations.Remove(simpleCharacter);
		}
	}

	private void KillAllAnimations()
	{
		foreach (KeyValuePair<SimpleCharacter, ManagedCoroutine> currentAnimation in currentAnimations)
		{
			currentAnimation.Value.Stop(this);
		}
		currentAnimations.Clear();
	}

	private void StartAnimation(SimpleCharacter simpleCharacter, IEnumerator enumerator, Action OnFinished)
	{
		StartAnimation(simpleCharacter, (enumerator: enumerator, finish: null), OnFinished);
	}

	private void StartAnimation(SimpleCharacter simpleCharacter, (IEnumerator enumerator, Action finish) tuple, Action OnFinished)
	{
		if (currentAnimations.TryGetValue(simpleCharacter, out var value))
		{
			value.Stop(this);
		}
		else
		{
			ManagedCoroutine managedCoroutine = (currentAnimations[simpleCharacter] = new ManagedCoroutine());
			value = managedCoroutine;
		}
		value.Start(this, tuple.enumerator, (ManagedCoroutine.FinishedType x) =>
		{
			if (x == ManagedCoroutine.FinishedType.Normal)
			{
				currentAnimations.Remove(simpleCharacter);
			}
			tuple.finish?.Invoke();
			OnFinished?.Invoke();
		});
	}

	public void DoAnimation(SimpleCharacter simpleCharacter, AnimationsList animationsList, Action OnFinished, float time)
	{
		switch (animationsList)
		{
		default:
			OnFinished?.Invoke();
			break;
		case AnimationsList.EnterScreen:
			StartAnimation(simpleCharacter, MoveCharacterIntoTheView(simpleCharacter, time, UiOverlay.Instance.GetMainCameraSize().x), OnFinished);
			break;
		case AnimationsList.LeaveScreen:
			StartAnimation(simpleCharacter, MoveCharacterOutOfTheView(simpleCharacter, time, new System.Numerics.Vector2(UiOverlay.Instance.GetMainCameraSize().x, 0f)), OnFinished);
			break;
		case AnimationsList.Shake:
			StartAnimation(simpleCharacter, ShakeCharacter(simpleCharacter, time), OnFinished);
			break;
		case AnimationsList.EnterScreenFromLeft:
			StartAnimation(simpleCharacter, MoveCharacterIntoTheView(simpleCharacter, time, 0f - UiOverlay.Instance.GetMainCameraSize().x), OnFinished);
			break;
		case AnimationsList.LeaveScreenToLeft:
			StartAnimation(simpleCharacter, MoveCharacterOutOfTheView(simpleCharacter, time, new System.Numerics.Vector2(0f - UiOverlay.Instance.GetMainCameraSize().x, 0f)), OnFinished);
			break;
		case AnimationsList.TransparencyTo0:
			FadeCharacterTransparency(simpleCharacter, 1f, 0f, time, OnFinished);
			break;
		case AnimationsList.TransparencyTo1:
			FadeCharacterTransparency(simpleCharacter, 0f, 1f, time, OnFinished);
			break;
		case AnimationsList.MoveToLeft:
			StartAnimation(simpleCharacter, MoveCharacterHorizontally(simpleCharacter, time, -3.25f), OnFinished);
			break;
		case AnimationsList.MoveToRight:
			StartAnimation(simpleCharacter, MoveCharacterHorizontally(simpleCharacter, time, 3.25f), OnFinished);
			break;
		case AnimationsList.MoveToLeftAbs:
			StartAnimation(simpleCharacter, MoveCharacterHorizontallyAbsolute(simpleCharacter, time, -5f), OnFinished);
			break;
		case AnimationsList.MoveToRightAbs:
			StartAnimation(simpleCharacter, MoveCharacterHorizontallyAbsolute(simpleCharacter, time, 5f), OnFinished);
			break;
		case AnimationsList.LeaveScreenToBottom:
			StartAnimation(simpleCharacter, MoveCharacterOutOfTheView(simpleCharacter, time, new System.Numerics.Vector2(0f, (0f - UiOverlay.Instance.GetMainCameraSize().y) * 2f)), OnFinished);
			break;
		}
	}
}
