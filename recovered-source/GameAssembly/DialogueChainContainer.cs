using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

[Serializable]
public class DialogueChainContainer
{
	private class InnerContainer
	{
		[NonSerialized]
		public List<DialogueChain> _withConditions;

		[NonSerialized]
		public List<DialogueChain> _withoutConditions;

		[SerializeField]
		public List<DialogueChain> _all;

		public InnerContainer()
		{
			_withConditions = new List<DialogueChain>();
			_withoutConditions = new List<DialogueChain>();
			_all = new List<DialogueChain>();
		}

		public void Add(DialogueChain dialogueChain)
		{
			if (dialogueChain.HasConditions)
			{
				_withConditions.Add(dialogueChain);
			}
			else
			{
				_withoutConditions.Add(dialogueChain);
			}
			_all.Add(dialogueChain);
		}
	}

	private List<DialogueChain> cache = new List<DialogueChain>();

	private Dictionary<int, InnerContainer> _tagged;

	private InnerContainer _untagged;

	[SerializeField]
	private InnerContainer _all;

	private Dictionary<Guid, int> nextFreeIds = new Dictionary<Guid, int>();

	[NonSerialized]
	private TableReference TableReference;

	public string Source { get; private set; }

	public DialogueChainMemory Memory => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.GetDialogueChainMemory(Source);

	public List<DialogueChain> UntaggedDialogues => _untagged._all;

	public List<DialogueChain> AllDialogues => _all._all;

	public string KeyPrefix => Source.Split('/').Last();

	public DialogueChainContainer(string filename, TableReference tableReference, DialogueChain.SpecialTagDelegate specialTagPraseFunc = null, Action<DialogueChain> afterParse = null)
	{
		_tagged = new Dictionary<int, InnerContainer>();
		_untagged = new InnerContainer();
		_all = new InnerContainer();
		LoadFile(filename, Guid.Empty, specialTagPraseFunc, afterParse);
		Source = filename;
		TableReference = tableReference;
	}

	private void LoadFile(string filename, Guid guid, DialogueChain.SpecialTagDelegate specialTagPraseFunc, Action<DialogueChain> afterParse)
	{
		TextAsset textAsset = Resources.Load(filename) as TextAsset;
		if (textAsset != null)
		{
			List<int> list = new List<int>();
			string[] array = textAsset.text.Split(new string[3] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
			foreach (string text in array)
			{
				if (text.Length > 1)
				{
					list.Clear();
					int nextFreeId = GetNextFreeId(guid);
					DialogueChain dialogueChain = DialogueChain.CreateChainComment(text, GameId.CreateGeneric(guid, nextFreeId), specialTagPraseFunc, this, list);
					afterParse?.Invoke(dialogueChain);
					if (dialogueChain.Identificator.Id < nextFreeId)
					{
						Debug.LogError(text + "\n Identificator is lower than nextFreeId. It shouldn't be like this!\nLine ignored!");
						continue;
					}
					nextFreeIds[guid] = dialogueChain.Identificator.Id + 1;
					Add(dialogueChain, list);
				}
			}
		}
		else
		{
			Debug.LogError(filename + " not found!!!");
			_untagged.Add(DialogueChain.CreateRawComment("ERROR"));
		}
	}

	public void Assert(params Func<DialogueChain, string>[] asserts)
	{
		foreach (DialogueChain item in _all._all)
		{
			for (int i = 0; i < asserts.Length; i++)
			{
				string text = asserts[i](item);
				if (!string.IsNullOrEmpty(text))
				{
					Debug.LogWarning(item.GetReadyText() + " - Assert Failed: " + text);
				}
			}
		}
	}

	private DialogueChain OptimizedRandomSearch(List<DialogueChain> container)
	{
		if (!container.Any())
		{
			return null;
		}
		DialogueChain random = container.GetRandom();
		if (random.ConditionsMet())
		{
			return random;
		}
		DialogueChain random2 = container.GetRandom();
		if (random2.ConditionsMet())
		{
			return random2;
		}
		DialogueChain random3 = container.GetRandom();
		if (random3.ConditionsMet())
		{
			return random3;
		}
		DialogueChain random4 = container.GetRandom();
		if (random4.ConditionsMet())
		{
			return random4;
		}
		cache.Clear();
		foreach (DialogueChain item in container)
		{
			if (item.ConditionsMet())
			{
				cache.Add(item);
			}
		}
		return cache.GetRandom();
	}

	private DialogueChain OptimizedRandomSearch(List<DialogueChain> container, Func<DialogueChain, bool> additionalPassFunc)
	{
		if (additionalPassFunc == null)
		{
			return OptimizedRandomSearch(container);
		}
		if (!container.Any())
		{
			return null;
		}
		DialogueChain random = container.GetRandom();
		if (random.ConditionsMet() && additionalPassFunc(random))
		{
			return random;
		}
		DialogueChain random2 = container.GetRandom();
		if (random2.ConditionsMet() && additionalPassFunc(random2))
		{
			return random2;
		}
		DialogueChain random3 = container.GetRandom();
		if (random3.ConditionsMet() && additionalPassFunc(random3))
		{
			return random3;
		}
		DialogueChain random4 = container.GetRandom();
		if (random4.ConditionsMet() && additionalPassFunc(random4))
		{
			return random4;
		}
		cache.Clear();
		foreach (DialogueChain item in container)
		{
			if (item.ConditionsMet() && additionalPassFunc(item))
			{
				cache.Add(item);
			}
		}
		return cache.GetRandom();
	}

	public DialogueChain GetRandomPossibleTagged(int tag)
	{
		if (_tagged.TryGetValue(tag, out var value))
		{
			return OptimizedRandomSearch(value._all);
		}
		return null;
	}

	public DialogueChain GetRandomPossibleTaggedWithout(int tag, Func<DialogueChain, bool> passFunction)
	{
		if (_tagged.TryGetValue(tag, out var value))
		{
			return OptimizedRandomSearch(value._all, passFunction);
		}
		return null;
	}

	public DialogueChain GetRandomPossibleTaggedWithoutIgnored(int tag, Func<DialogueChain, bool> additionalPassFunc = null)
	{
		if (additionalPassFunc == null)
		{
			return GetRandomPossibleTaggedWithout(tag, IsNotIgnored);
		}
		return GetRandomPossibleTaggedWithout(tag, (DialogueChain t) => IsNotIgnored(t) && additionalPassFunc(t));
	}

	public DialogueChain GetRandomPossibleUntagged()
	{
		return OptimizedRandomSearch(_untagged._all);
	}

	public DialogueChain GetRandomPossibleUntaggedWithout(Func<DialogueChain, bool> passFunction)
	{
		return OptimizedRandomSearch(_untagged._all, passFunction);
	}

	public DialogueChain GetRandomPossibleUntaggedWithoutIgnored(Func<DialogueChain, bool> additionalPassFunc = null)
	{
		if (additionalPassFunc == null)
		{
			return GetRandomPossibleUntaggedWithout(IsNotIgnored);
		}
		return GetRandomPossibleUntaggedWithout((DialogueChain t) => IsNotIgnored(t) && additionalPassFunc(t));
	}

	public DialogueChain GetRandomPossibleUntaggedPrioritizeConditionsWithout(Func<DialogueChain, bool> passFunction)
	{
		DialogueChain random = _untagged._withConditions.Where((DialogueChain t) => t.ConditionsMet()).Where(passFunction).ToList()
			.GetRandom();
		if (random == null)
		{
			return _untagged._withoutConditions.Where(passFunction).ToList().GetRandom();
		}
		return random;
	}

	public DialogueChain GetRandomPossibleUntaggedPrioritizeConditionsWithoutIgnoredAnd(IReadOnlyCollection<GameId> ignored)
	{
		return GetRandomPossibleUntaggedPrioritizeConditionsWithoutIgnored((DialogueChain t) => !ignored.Contains(t.Identificator));
	}

	public DialogueChain GetRandomPossibleUntaggedPrioritizeConditionsWithoutIgnored(Func<DialogueChain, bool> additionalPassFunc = null)
	{
		if (additionalPassFunc == null)
		{
			return GetRandomPossibleUntaggedPrioritizeConditionsWithout(IsNotIgnored);
		}
		return GetRandomPossibleUntaggedPrioritizeConditionsWithout((DialogueChain t) => IsNotIgnored(t) && additionalPassFunc(t));
	}

	public DialogueChain GetFirstPossibleUntaggedWithout(Func<DialogueChain, bool> passFunction)
	{
		return _untagged._all.FirstOrDefault((DialogueChain t) => t.ConditionsMet() && passFunction(t));
	}

	public DialogueChain GetFirstPossibleUntaggedWithoutIgnored(Func<DialogueChain, bool> additionalPassFunc = null)
	{
		if (additionalPassFunc == null)
		{
			return GetFirstPossibleUntaggedWithout(IsNotIgnored);
		}
		return GetFirstPossibleUntaggedWithout((DialogueChain t) => IsNotIgnored(t) && additionalPassFunc(t));
	}

	public DialogueChain GetById(GameId id)
	{
		return _all._all.FirstOrDefault((DialogueChain t) => t.Identificator == id);
	}

	public bool IsLastDone()
	{
		if (_all._all.Count == 0)
		{
			return true;
		}
		if (Memory.IsIgnored(_all._all.Last().Identificator))
		{
			return true;
		}
		return false;
	}

	public bool IsNotIgnored(DialogueChain dialogueChain)
	{
		return !Memory.IsIgnored(dialogueChain.Identificator);
	}

	public void Add(DialogueChain dialogueChain, List<int> tags)
	{
		if (tags.Count == 0)
		{
			_untagged.Add(dialogueChain);
		}
		else
		{
			foreach (int tag in tags)
			{
				if (!_tagged.TryGetValue(tag, out var value))
				{
					InnerContainer innerContainer = (_tagged[tag] = new InnerContainer());
					value = innerContainer;
				}
				value.Add(dialogueChain);
			}
		}
		_all.Add(dialogueChain);
	}

	public Dictionary<int, List<DialogueChain>> GetAllTaggedDialogues()
	{
		return _tagged.ToDictionary((KeyValuePair<int, InnerContainer> t) => t.Key, (KeyValuePair<int, InnerContainer> t) => t.Value._all);
	}

	public List<DialogueChain> GetTaggedDialogues(int tag)
	{
		if (_tagged.TryGetValue(tag, out var value))
		{
			return value._all;
		}
		return new List<DialogueChain>();
	}

	private int GetNextFreeId(Guid guid)
	{
		if (nextFreeIds.TryGetValue(guid, out var value))
		{
			return value;
		}
		nextFreeIds.Add(guid, 0);
		return 0;
	}

	public void LocalizeText(Locale newLocale)
	{
		StringTable result = LocalizationSettings.StringDatabase.GetTableAsync(TableReference, newLocale).Result;
		string keyPrefix = KeyPrefix;
		foreach (DialogueChain allDialogue in AllDialogues)
		{
			string text = keyPrefix + "_" + allDialogue.Identificator.ToShortString();
			StringTableEntry entryWithFallback = result.GetEntryWithFallback(text);
			if (entryWithFallback == null)
			{
				Debug.LogError("No entry found for " + text + "!");
				continue;
			}
			allDialogue.Localize(entryWithFallback.Value);
			int num = 0;
			DialogueChain next = allDialogue.Next;
			while (next != null)
			{
				string text2 = text + "_Next_" + num;
				entryWithFallback = result.GetEntryWithFallback(text2);
				if (entryWithFallback != null)
				{
					next.Localize(entryWithFallback.Value);
				}
				else
				{
					Debug.LogError("No entry found for " + text2 + "!");
				}
				next = next.Next;
				num++;
			}
		}
	}
}
