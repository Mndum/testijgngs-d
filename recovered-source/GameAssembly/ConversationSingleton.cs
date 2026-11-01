using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Fungus;
using UnityEngine;

public sealed class ConversationSingleton : SingletonLoadableMonoBehaviour<ConversationSingleton>, IHasStringReferences
{
	public class ConversationInfo
	{
		public UnityEngine.Object Object;

		public string Name;

		public SerializedLocalizedConversation Conversation;

		public ConversationInfo(UnityEngine.Object o, LocalizedStringReferenceContainer container, FieldInfo fieldInfo)
		{
			Object = o;
			Name = container.ContainerName + "_" + fieldInfo.Name;
			Conversation = (SerializedLocalizedConversation)fieldInfo.GetValue(container);
		}

		public ConversationInfo(LocalizedConversationCommand localizedConversationCommand)
		{
			Object = localizedConversationCommand;
			Name = localizedConversationCommand.ParentBlock.BlockName;
			Conversation = localizedConversationCommand.serializedLocalizedConversation;
		}
	}

	public class LocalizedStringReferenceInfo
	{
		public UnityEngine.Object Object;

		public LocalizedStringReference StringReference;

		public string Name => StringReference.GetReference().ToStringWithoutValue();

		public LocalizedStringReferenceInfo(UnityEngine.Object o, LocalizedStringReference stringReference)
		{
			Object = o;
			StringReference = stringReference;
		}
	}

	public interface ISayDialogOverrideCharacter
	{
		IEnumerator DoSay(string Text, BetterConversationManager.ConversationItemWriteSettings settings);
	}

	public class TemporaryCharacter : ISayDialogCharacter
	{
		public string Id { get; }

		public string NameText { get; set; }

		public SayDialog SayDialog { get; set; }

		public Color NameColor { get; set; } = Color.white;

		public TemporaryCharacter(string id)
		{
			Id = id;
		}

		public bool TryGetSayDialog(out SayDialog sayDialog)
		{
			sayDialog = null;
			return false;
		}
	}

	[Serializable]
	public class NormalCharacter : ISayDialogCharacter
	{
		[SerializeField]
		private string _id;

		[SerializeField]
		private LocalizedStringReferenceCharacters _nameStringReference;

		[SerializeField]
		private Color _nameColor = Color.white;

		public string Id => _id;

		public LocalizedStringReferenceCharacters NameStringReference => _nameStringReference;

		public string NameText => _nameStringReference.GetString();

		public Color NameColor => _nameColor;

		public NormalCharacter(string id)
		{
			_id = id;
		}

		public bool TryGetSayDialog(out SayDialog sayDialog)
		{
			sayDialog = null;
			return false;
		}
	}

	public class BotLive2DCharacterAdapter : ISayDialogCharacter
	{
		public string Id => "Bot";

		public string NameText => GameVariables.Current.botName;

		public Color NameColor => ColorPaletteManager.JunsMagenta.Color;

		public bool TryGetSayDialog(out SayDialog sayDialog)
		{
			sayDialog = null;
			if (SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.IsAnyControllerActiveAndTalkingWithOverlay("bot"))
			{
				return false;
			}
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.TryGetStateAs<GameScript.StoryState>(out var _))
			{
				return false;
			}
			sayDialog = UiOverlay.Instance._speechSayDialogPopup.sayDialog;
			return true;
		}
	}

	public class ConsoleSystemCharacter : ISayDialogCharacter, ISayDialogOverrideCharacter
	{
		public string Id => "System";

		public string NameText => "System";

		public Color NameColor => Color.white;

		public bool TryGetSayDialog(out SayDialog sayDialog)
		{
			sayDialog = null;
			return false;
		}

		public IEnumerator DoSay(string Text, BetterConversationManager.ConversationItemWriteSettings settings)
		{
			if (UiOverlay.Instance.TryGetTopPopupOfType<BotStatusAppPopup>(out var botStatusAppPopup))
			{
				yield return WaitForSecondCache.RandomUnderTenthSecond();
				botStatusAppPopup.PrintLine(Text);
				botStatusAppPopup.UpdateDisplay();
			}
		}
	}

	public class ConsoleBotCharacter : ISayDialogCharacter, ISayDialogOverrideCharacter
	{
		public string Id => "BotConsole";

		public string NameText => "BotConsole";

		public Color NameColor => Color.white;

		public bool TryGetSayDialog(out SayDialog sayDialog)
		{
			sayDialog = null;
			return false;
		}

		public IEnumerator DoSay(string Text, BetterConversationManager.ConversationItemWriteSettings settings)
		{
			if (UiOverlay.Instance.TryGetTopPopupOfType<BotStatusAppPopup>(out var botStatusAppPopup))
			{
				yield return WaitForSecondCache.RandomUnderHalfSecond();
				botStatusAppPopup.PrintLine("<color=#FF69B4>" + GameVariables.Current.botName + "</color>: " + Text);
				botStatusAppPopup.UpdateDisplay();
			}
		}
	}

	public class ConsoleYouCharacter : ISayDialogCharacter, ISayDialogOverrideCharacter
	{
		private static WaitUntil AnyKeyPressed = new WaitUntil(() => Input.anyKeyDown);

		public string Id => "YouConsole";

		public string NameText => "YouConsole";

		public Color NameColor => Color.white;

		public bool TryGetSayDialog(out SayDialog sayDialog)
		{
			sayDialog = null;
			return false;
		}

		public IEnumerator DoSay(string Text, BetterConversationManager.ConversationItemWriteSettings settings)
		{
			if (!UiOverlay.Instance.TryGetTopPopupOfType<BotStatusAppPopup>(out var botStatusAppPopup))
			{
				yield break;
			}
			botStatusAppPopup.StartPlayerInput();
			foreach (char c in Text)
			{
				botStatusAppPopup.AddCharToInput(c);
				if (!Input.anyKey)
				{
					yield return WaitForSecondCache.RandomUnderTenthSecond();
				}
				else
				{
					yield return WaitForSecondCache.Seconds0_01;
				}
			}
			yield return AnyKeyPressed;
			botStatusAppPopup.EndPlayerInput();
			yield return null;
			botStatusAppPopup.PrintLine(Text ?? "");
			botStatusAppPopup.UpdateDisplay();
			yield return null;
		}
	}

	[SerializeField]
	public List<NormalCharacter> NormalCharacters = new List<NormalCharacter>();

	private Dictionary<string, ISayDialogCharacter> _characterInterfaceAdapters = new Dictionary<string, ISayDialogCharacter>();

	private Dictionary<string, WeakReference<ISayDialogCharacter>> _weakReferencesAdapters = new Dictionary<string, WeakReference<ISayDialogCharacter>>();

	public BotLive2DCharacterAdapter BotLive2DCharacter { get; private set; }

	public IEnumerable<ConversationInfo> GetAllConversations()
	{
		LocalizedConversationCommand[] components = SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.GetComponents<LocalizedConversationCommand>();
		foreach (LocalizedConversationCommand localizedConversationCommand in components)
		{
			yield return new ConversationInfo(localizedConversationCommand);
		}
		MonoBehaviour[] array = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		foreach (MonoBehaviour script in array)
		{
			if (!(script is IHasLocalizedStringReferenceContainers hasLocalizedStringReferenceContainers))
			{
				continue;
			}
			foreach (LocalizedStringReferenceContainer container in hasLocalizedStringReferenceContainers.GetAllContainers())
			{
				foreach (FieldInfo allLocalizedConversationField in container.GetAllLocalizedConversationFields())
				{
					yield return new ConversationInfo(script, container, allLocalizedConversationField);
				}
			}
		}
		CFLocalizationDataSO allConversations = SingletonScriptableObject<CFLocalizationDataSO>.Instance;
		foreach (LocalizedStringReferenceContainer container in allConversations.GetAllContainers())
		{
			foreach (FieldInfo allLocalizedConversationField2 in container.GetAllLocalizedConversationFields())
			{
				yield return new ConversationInfo(allConversations, container, allLocalizedConversationField2);
			}
		}
	}

	public IEnumerable<LocalizedStringReferenceInfo> GetAllSerializedStringReferences()
	{
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsByType<UnityEngine.Object>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		foreach (UnityEngine.Object script in array)
		{
			if (script is IHasLocalizedStringReferenceContainers hasLocalizedStringReferenceContainers)
			{
				foreach (LocalizedStringReferenceContainer allContainer in hasLocalizedStringReferenceContainers.GetAllContainers())
				{
					foreach (LocalizedStringReference item in allContainer.IterateAllLocalizedStringReferences())
					{
						yield return new LocalizedStringReferenceInfo(script, item);
					}
				}
			}
			else
			{
				if (!(script is IHasStringReferences hasStringReferences))
				{
					continue;
				}
				foreach (LocalizedStringReference item2 in hasStringReferences.IterateAllLocalizedStringReferences())
				{
					yield return new LocalizedStringReferenceInfo(script, item2);
				}
			}
		}
		CFLocalizationDataSO allConversations = SingletonScriptableObject<CFLocalizationDataSO>.Instance;
		foreach (LocalizedStringReferenceContainer allContainer2 in allConversations.GetAllContainers())
		{
			foreach (LocalizedStringReference item3 in allContainer2.IterateAllLocalizedStringReferences())
			{
				yield return new LocalizedStringReferenceInfo(allConversations, item3);
			}
		}
	}

	public void ClearWithoutDeletingEntries()
	{
		Debug.LogError("Tried to delete all characteres!");
	}

	public void ClearAndDeleteEntries()
	{
		Debug.LogError("Tried to delete all characteres!");
	}

	public IEnumerable<LocalizedStringReference> IterateAllLocalizedStringReferences()
	{
		return NormalCharacters.Select((NormalCharacter x) => x.NameStringReference);
	}

	public bool CreateEmptyKeys()
	{
		bool result = false;
		foreach (NormalCharacter normalCharacter in NormalCharacters)
		{
			if (normalCharacter.NameStringReference.CreateIfNotExists(normalCharacter.Id))
			{
				normalCharacter.NameStringReference.SetStringProjectLocale(normalCharacter.Id);
				result = true;
			}
		}
		return result;
	}

	public bool UpdateKeyNames()
	{
		return false;
	}

	public NormalCharacter MakeSureNormalCharacterExistsEditor(string id)
	{
		if (TryGetNormalCharacterEditor(id, out var normalCharacter, out var _))
		{
			return normalCharacter;
		}
		normalCharacter = new NormalCharacter(id);
		NormalCharacters.Add(normalCharacter);
		return normalCharacter;
	}

	public bool TryGetNormalCharacterEditor(string id, out NormalCharacter normalCharacter, out int index)
	{
		index = NormalCharacters.FindIndex((NormalCharacter x) => x.Id == id);
		if (index < 0)
		{
			normalCharacter = null;
			return false;
		}
		normalCharacter = NormalCharacters[index];
		return true;
	}

	private void AddAdapter(ISayDialogCharacter characterAdapter)
	{
		string id = characterAdapter.Id;
		if (_characterInterfaceAdapters.ContainsKey(id))
		{
			Debug.Log("Overriding character adapter with id: " + id + "!");
		}
		_characterInterfaceAdapters[id] = characterAdapter;
	}

	public ISayDialogCharacter GetTemporaryCharacter(string characterName)
	{
		TemporaryCharacter temporaryCharacter = new TemporaryCharacter(characterName)
		{
			NameText = characterName
		};
		_weakReferencesAdapters[characterName] = new WeakReference<ISayDialogCharacter>(temporaryCharacter);
		return temporaryCharacter;
	}

	public ISayDialogCharacter GetCharacter(string characterId)
	{
		if (characterId == null)
		{
			return null;
		}
		if (_characterInterfaceAdapters.TryGetValue(characterId, out var value))
		{
			return value;
		}
		if (_weakReferencesAdapters.TryGetValue(characterId, out var value2))
		{
			if (value2.TryGetTarget(out value))
			{
				return value;
			}
			_characterInterfaceAdapters.Remove(characterId);
		}
		return GetTemporaryCharacter(characterId);
	}

	public bool TryGetActiveStoryCharacter(string characterId, out IStoryCharacter character)
	{
		if (SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.TryGetActiveCharacterById(characterId, out var character2))
		{
			character = character2;
			return true;
		}
		return (UnityEngine.Object)(character = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.GetReadyController(characterId)) != null;
	}

	public bool TryGetStoryCharacterEditor(string characterId, out IStoryCharacter character)
	{
		return TryGetActiveStoryCharacter(characterId, out character);
	}

	public Result IsValidEmote(string characterId, EmoteData emote)
	{
		if (TryGetStoryCharacterEditor(characterId, out var character))
		{
			Result result = character.IsValidEmote(emote);
			if (result.IsSuccess)
			{
				return Result.Success;
			}
			return result.CreateNewWithPrefix("In character " + characterId + ":");
		}
		return Result.FailWithDescription("Character " + characterId + " not found!");
	}

	public override Type[] GetDependenciesArray()
	{
		return null;
	}

	protected override UniTask LoadInner(IProgressManager progressManager)
	{
		foreach (NormalCharacter normalCharacter in NormalCharacters)
		{
			AddAdapter(normalCharacter);
		}
		BotLive2DCharacterAdapter characterAdapter = (BotLive2DCharacter = new BotLive2DCharacterAdapter());
		AddAdapter(characterAdapter);
		AddAdapter(new ConsoleSystemCharacter());
		AddAdapter(new ConsoleBotCharacter());
		AddAdapter(new ConsoleYouCharacter());
		return UniTask.CompletedTask;
	}
}
