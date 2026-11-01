using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using StoryConditions;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class DialogueChain
{
	public delegate string SpecialTagDelegate(string input, List<int> tags);

	[Serializable]
	public class Variable
	{
		[SerializeField]
		private string name;

		public object Value;

		public string Name => name;

		public Variable(string name, object value)
		{
			Value = value;
			this.name = name;
		}
	}

	private string inputText;

	private string rawText;

	private string readyText;

	private bool hasSpecial;

	private Dictionary<string, Variable> _nameToVariable;

	private static List<string> _emotes = new List<string> { "pipiangry", "cum", "cumgear", "robocrying", "pipilaugh", "sprinkle", "piggers", "robogirl" };

	[field: SerializeField]
	public GameId Identificator { get; private set; }

	public DialogueChainContainer SourceContainer { get; private set; }

	public DialogueChain Next { get; private set; }

	public string EntryReference => SourceContainer.KeyPrefix + "_" + Identificator.ToShortString();

	public bool IsOneTime => GetVariableOrDefault("onetime", defaultValue: false);

	public int Donation => GetVariableOrDefault("donation", 0);

	public int SubDirection => GetVariableOrDefault("sub", 0);

	public int Times => GetVariableOrDefault("times", 1);

	public bool Unique => GetVariableOrDefault("unique", defaultValue: true);

	public bool ReplacePrevious => GetVariableOrDefault("replace", defaultValue: false);

	public List<StoryCondition> Conditions { get; private set; }

	public bool HasConditions
	{
		get
		{
			if (Conditions != null)
			{
				return Conditions.Count != 0;
			}
			return false;
		}
	}

	private DialogueChain(GameId id, DialogueChainContainer container)
	{
		Identificator = id;
		SourceContainer = container;
		readyText = "";
		_nameToVariable = null;
	}

	private void ProcessInputText(string _inputtext, SpecialTagDelegate specialTagPraseFunc, List<int> tags)
	{
		inputText = _inputtext;
		inputText = inputText.Replace("\\n", "\n");
		string[] array = inputText.Split('[', ']');
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != "")
				{
					if (i % 2 == 0)
					{
						stringBuilder.Append(array[i]);
					}
					else
					{
						ProcessModificator(array[i], stringBuilder, tags, specialTagPraseFunc);
					}
				}
			}
			rawText = stringBuilder.ToString();
		}
		ProcessEmotes();
	}

	private static DialogueChain CreateAndProcessInputText(string inputtext, GameId id, SpecialTagDelegate specialTagPraseFunc, DialogueChainContainer container, List<int> tags)
	{
		DialogueChain dialogueChain = new DialogueChain(id, container);
		dialogueChain.ProcessInputText(inputtext, specialTagPraseFunc, tags);
		return dialogueChain;
	}

	private void ProcessModificator(string text, StringBuilder rawTextBuilder, List<int> tags, SpecialTagDelegate specialTagPraseFunc = null)
	{
		string[] array = text.Split(',');
		switch (array[0].ToLower())
		{
		case "tag":
		case "tags":
		{
			if (array.Length <= 1)
			{
				FormatError("No tags given!", inputText);
				break;
			}
			if (specialTagPraseFunc == null)
			{
				FormatError("No tag parse function!", inputText);
				break;
			}
			if (tags == null)
			{
				FormatError("No tag list given! Tags are ignored!", inputText);
				break;
			}
			for (int num = 1; num < array.Length; num++)
			{
				string text2 = specialTagPraseFunc(array[num], tags);
				if (text2 != null)
				{
					FormatError(array[num] + " Error from special tag function:" + text2, inputText);
				}
			}
			break;
		}
		case "times":
			if (array.Length == 2)
			{
				if (int.TryParse(array[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
				{
					SetVariable("times", result);
				}
				else
				{
					FormatError(text + " argument is not an int!", inputText);
				}
			}
			else
			{
				FormatError(text + " not enough arguments!", inputText);
			}
			break;
		case "notunique":
			SetVariable("unique", value: false);
			break;
		case "condition":
		{
			StoryCondition storyCondition = StoryConditionParser.TryCreate(array, inputText);
			if (storyCondition != null)
			{
				AddCondition(storyCondition);
			}
			break;
		}
		case "sub":
			if (array.Length == 2)
			{
				if (int.TryParse(array[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var result3))
				{
					SetVariable("sub", result3);
				}
				else
				{
					FormatError(text + " argument is not an int!", inputText);
				}
			}
			else
			{
				SetVariable("sub", 1);
			}
			break;
		case "donation":
			if (array.Length == 2)
			{
				if (int.TryParse(array[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var result4))
				{
					SetVariable("donation", result4);
				}
				else
				{
					FormatError(text + " argument is not an int!", inputText);
				}
			}
			else
			{
				FormatError("Donation without money! WROOONG", inputText);
			}
			break;
		case "replace":
			SetVariable("replace", value: true);
			break;
		case "var":
			if (array.Length == 3)
			{
				SetVariable(array[1], array[2]);
			}
			else
			{
				FormatError(text + " not enough arguments!", inputText);
			}
			break;
		case "img":
		case "image":
			if (array.Length == 2)
			{
				SetVariable("image", array[1]);
			}
			else
			{
				FormatError(text + " not enough arguments!", inputText);
			}
			break;
		case "onetime":
			SetVariable("onetime", value: true);
			break;
		case "funguscomment":
			SetVariable("dialoguetype", ModelBrain.DialogueType.StartComment);
			break;
		case "fungusmanualoverlay":
			SetVariable("automaticfungusoverlay", value: false);
			break;
		case "id":
			if (array.Length == 2)
			{
				if (int.TryParse(array[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var result2))
				{
					if (result2 >= 0)
					{
						Identificator = GameId.CreateGeneric(Identificator.Guid, result2);
					}
					else
					{
						FormatError(text + " argument smaller than 0", inputText);
					}
				}
				else
				{
					FormatError(text + " argument is not an int!", inputText);
				}
			}
			else
			{
				FormatError(text + " not enough arguments!", inputText);
			}
			break;
		case "title":
			if (array.Length >= 2)
			{
				SetVariable("title", array.Skip(1).Aggregate((string a, string b) => a + b));
			}
			else
			{
				FormatError(text + " not enough arguments!", inputText);
			}
			break;
		case "never":
			AddCondition(StoryCondition.AlwaysFalseCondition);
			break;
		default:
			FormatError(text + " found no such modificator! Interpreted as text", inputText);
			rawTextBuilder.Append("[" + text + "]");
			break;
		}
	}

	private void ProcessEmotes()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			string[] array = rawText.Split(':');
			for (int i = 0; i < array.Length; i++)
			{
				if (i % 2 == 0)
				{
					stringBuilder.Append(array[i]);
				}
				else if (_emotes.Contains(array[i]))
				{
					stringBuilder.Append("<sprite name=\"" + array[i] + "\">");
				}
				else if (i == array.Length - 1)
				{
					stringBuilder.Append(":" + array[i]);
				}
				else
				{
					stringBuilder.Append(":" + array[i] + ":");
				}
			}
			readyText = stringBuilder.ToString();
		}
		hasSpecial = readyText.Contains("{") && readyText.Contains("}");
	}

	public static void FormatError(string text, string fullrawinput)
	{
		Debug.LogWarning("\"" + fullrawinput + "\" :" + text);
	}

	public static DialogueChain CreateRawComment(string inputtext, SpecialTagDelegate specialTagPraseFunc = null, DialogueChainContainer container = null, List<int> tags = null)
	{
		return CreateAndProcessInputText(inputtext, GameId.CreateVanilla(-1), specialTagPraseFunc, container, tags);
	}

	public static DialogueChain CreateChainComment(string inputtext, GameId id, SpecialTagDelegate specialTagPraseFunc = null, DialogueChainContainer container = null, List<int> tags = null)
	{
		string[] array = inputtext.Split('|');
		if (array.Length == 1)
		{
			return CreateAndProcessInputText(inputtext, id, specialTagPraseFunc, container, tags);
		}
		DialogueChain dialogueChain = CreateAndProcessInputText(array[0], id, specialTagPraseFunc, container, tags);
		DialogueChain dialogueChain2 = dialogueChain;
		for (int i = 1; i < array.Length; i++)
		{
			DialogueChain dialogueChain3 = (dialogueChain2.Next = CreateAndProcessInputText(array[i], id, specialTagPraseFunc, container, tags));
			dialogueChain2 = dialogueChain3;
		}
		return dialogueChain;
	}

	public void Localize(string newRawText)
	{
		rawText = newRawText;
		ProcessEmotes();
	}

	public bool TryGetVariable(string name, out Variable output)
	{
		if (_nameToVariable == null)
		{
			output = null;
			return false;
		}
		return _nameToVariable.TryGetValue(name, out output);
	}

	public bool TryGetVariable<T>(string name, out T output)
	{
		if (TryGetVariable(name, out var output2))
		{
			if (output2.Value is T val)
			{
				output = val;
				return true;
			}
			FormatError($"Variable '{name}' with data '{output2.Value}' can't be parsed as {typeof(T)}!!!", readyText);
			output = default;
			return false;
		}
		output = default;
		return false;
	}

	public T GetVariableOrDefault<T>(string name, T defaultValue)
	{
		if (TryGetVariable(name, out var output))
		{
			object value = output.Value;
			if (value is T)
			{
				return (T)value;
			}
			FormatError($"Variable '{name}' with data '{output.Value}' can't be parsed as {typeof(T)}!!!", readyText);
			return defaultValue;
		}
		return defaultValue;
	}

	private Variable CreateVariable<T>(string name, T value)
	{
		Variable variable = new Variable(name, value);
		_nameToVariable[name] = variable;
		return variable;
	}

	private Variable SetVariable<T>(string name, T value)
	{
		if (_nameToVariable == null)
		{
			_nameToVariable = new Dictionary<string, Variable>();
			return CreateVariable(name, value);
		}
		if (TryGetVariable(name, out var output))
		{
			output.Value = value;
			return output;
		}
		return CreateVariable(name, value);
	}

	public void AddCondition(StoryCondition condition)
	{
		if (Conditions == null)
		{
			List<StoryCondition> list = (Conditions = new List<StoryCondition>());
		}
		Conditions.Add(condition);
	}

	public bool ConditionsMet()
	{
		try
		{
			if (Conditions == null)
			{
				return true;
			}
			foreach (StoryCondition condition in Conditions)
			{
				if (!condition.ConditionMet())
				{
					return false;
				}
			}
		}
		catch (Exception message)
		{
			Debug.LogError($"Error checking conditions in dialogue chain in {SourceContainer.Source} id:{Identificator}");
			Debug.LogError(message);
			return false;
		}
		return true;
	}

	public void Activate()
	{
		if (IsOneTime)
		{
			SourceContainer?.Memory.AddIgnored(Identificator);
		}
	}

	public string GetInputText()
	{
		return inputText;
	}

	public string GetRawText()
	{
		return rawText;
	}

	public string GetReadyText()
	{
		return readyText;
	}

	public string GetText()
	{
		if (!hasSpecial)
		{
			return readyText;
		}
		string[] array = readyText.Split('{', '}');
		if (array.Length == 1)
		{
			return readyText;
		}
		StringBuilderCache.AcquireManual(out var stringBuilder);
		for (int i = 0; i < array.Length; i++)
		{
			if (i % 2 == 0)
			{
				stringBuilder.Append(array[i]);
				continue;
			}
			if (array[i].StartsWith("$"))
			{
				stringBuilder.Append("{" + array[i] + "}");
				continue;
			}
			string randomWord = SingletonLoadableMonoBehaviour<WordGeneratorSingleton>.Instance.GetRandomWord(array[i]);
			if (randomWord != "")
			{
				stringBuilder.Append(randomWord);
			}
			else
			{
				FormatError("{" + array[i] + "} is wrong!", readyText);
			}
		}
		return StringBuilderCache.ReleaseWithToString(stringBuilder);
	}

	public string GetText(int seed)
	{
		if (!hasSpecial)
		{
			return readyText;
		}
		string[] array = readyText.Split('{', '}');
		if (array.Length == 1)
		{
			return readyText;
		}
		StringBuilderCache.AcquireManual(out var stringBuilder);
		for (int i = 0; i < array.Length; i++)
		{
			if (i % 2 == 0)
			{
				stringBuilder.Append(array[i]);
				continue;
			}
			string randomWord = SingletonLoadableMonoBehaviour<WordGeneratorSingleton>.Instance.GetRandomWord(array[i], seed + i);
			if (randomWord != "")
			{
				stringBuilder.Append(randomWord);
			}
			else
			{
				FormatError("{" + array[i] + "} is wrong!", readyText);
			}
		}
		return StringBuilderCache.ReleaseWithToString(stringBuilder);
	}

	public string GetCommentText()
	{
		return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(LOC.Other.TABLE_REFERENCE, EntryReference, null, FallbackBehavior.UseProjectSettings).Result;
	}

	public string GetSummary()
	{
		return $"Id:{Identificator} Text:{GetReadyText()}";
	}
}
