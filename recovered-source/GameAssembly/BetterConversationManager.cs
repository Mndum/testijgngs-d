using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using Fungus;
using JetBrains.Annotations;
using UnityEngine;

public class BetterConversationManager
{
	public class ConversationItemWriteSettings
	{
		public readonly bool ClearPrev;

		public readonly bool WaitForInput;

		public readonly bool FadeDone;

		public readonly WaitForSeconds WaitForSeconds;

		public static ConversationItemWriteSettings DefaultWriteSettings => new ConversationItemWriteSettings();

		public static ConversationItemWriteSettings NoWaitForInputWriteSettings => new ConversationItemWriteSettings(clearPrev: true, waitForInput: false, fadeDone: true, 1f);

		public ConversationItemWriteSettings(bool clearPrev = true, bool waitForInput = true, bool fadeDone = true, float waitForSeconds = 0f)
		{
			ClearPrev = clearPrev;
			WaitForInput = waitForInput;
			FadeDone = fadeDone;
			WaitForSeconds = ((waitForSeconds > 0f) ? new WaitForSeconds(waitForSeconds) : null);
		}

		public static ConversationItemWriteSettings Get(bool waitForInput)
		{
			if (!waitForInput)
			{
				return NoWaitForInputWriteSettings;
			}
			return DefaultWriteSettings;
		}
	}

	public abstract class ConversationLine
	{
		public abstract IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings);

		public abstract void AppendToStringBuilder(StringBuilder stringBuilder);

		public abstract Result IsValid();

		public virtual bool IsSpecial()
		{
			return true;
		}
	}

	public class ConversationEmoteDefault : ConversationLine
	{
		public string CharacterId;

		public ConversationEmoteDefault(string characterId)
		{
			CharacterId = characterId;
		}

		public override IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings)
		{
			if (SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.TryGetActiveStoryCharacter(CharacterId, out var character))
			{
				character.SetDefaultEmote(instantly: false);
			}
			else
			{
				Debug.LogError("Character " + CharacterId + " not found! Can't make it lok default!");
			}
			return null;
		}

		public override void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			stringBuilder.Append('#');
			stringBuilder.Append(CharacterId);
			stringBuilder.Append('.');
			stringBuilder.Append("default");
		}

		public override Result IsValid()
		{
			IStoryCharacter character;
			return SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.TryGetStoryCharacterEditor(CharacterId, out character);
		}
	}

	public class ConversationEmote : ConversationLine
	{
		public string CharacterId;

		public EmoteData Data;

		public ConversationEmote(string id, string slot, string item)
		{
			CharacterId = id;
			Data = new EmoteData(slot, item);
		}

		public override IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings)
		{
			if (SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.TryGetActiveStoryCharacter(CharacterId, out var character))
			{
				character.SetEmote(new EmoteData[1] { Data });
			}
			else
			{
				Debug.LogWarning($"Character {CharacterId} not found! Can't do {Data}!");
			}
			return null;
		}

		public override void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			stringBuilder.Append('#');
			stringBuilder.Append(CharacterId);
			stringBuilder.Append('.');
			stringBuilder.Append(Data);
		}

		public override Result IsValid()
		{
			return SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.IsValidEmote(CharacterId, Data);
		}
	}

	public class ConversationAnimation : ConversationLine
	{
		public string CharacterId;

		public SimpleCharacterSingleton.AnimationsList Animation;

		public float Time;

		public ConversationAnimation(string id, SimpleCharacterSingleton.AnimationsList animation, float time)
		{
			CharacterId = id;
			Animation = animation;
			Time = time;
		}

		public override IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings)
		{
			if (SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.TryGetActiveStoryCharacter(CharacterId, out var character))
			{
				if (character is SimpleCharacter simpleCharacter)
				{
					SingletonMonoBehaviour<SimpleCharacterSingleton>.Instance.DoAnimation(simpleCharacter, Animation, null, Time);
				}
				else
				{
					Debug.LogError("Character " + CharacterId + " found, but animation works only on simple characters!");
				}
			}
			else
			{
				Debug.LogError("Character " + CharacterId + " not found!!");
			}
			return null;
		}

		public override void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			stringBuilder.Append('#');
			stringBuilder.Append(CharacterId);
			stringBuilder.Append('.');
			stringBuilder.Append("animate");
			stringBuilder.Append('(');
			stringBuilder.Append(Animation);
			stringBuilder.Append(',');
			stringBuilder.Append(Time.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(')');
		}

		public override Result IsValid()
		{
			if (Animation == SimpleCharacterSingleton.AnimationsList.Nothing)
			{
				return Result.FailWithDescription("No animation");
			}
			if (Time < 0f)
			{
				return Result.FailWithDescription("Negative time");
			}
			if (Time > 10000f)
			{
				return Result.FailWithDescription("Waaaaay too long time!");
			}
			return Result.Success;
		}
	}

	public class BrokenLine : ConversationLine
	{
		private ConversationLine OriginalLine;

		private ConversationData.SpecialParsingException Exception;

		public BrokenLine(ConversationData.SpecialParsingException exception, ConversationLine originalLine = null)
		{
			OriginalLine = originalLine;
			Exception = exception;
		}

		public override IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings)
		{
			return null;
		}

		public override void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			if (OriginalLine != null)
			{
				OriginalLine.AppendToStringBuilder(stringBuilder);
			}
			stringBuilder.Append(Exception.Line);
		}

		public override Result IsValid()
		{
			return Result.FromException(Exception);
		}
	}

	public abstract class ScopeConversationLine : ConversationLine
	{
		public int OuterStartIndex => InnerStartIndex - 1;

		public int OuterEndIndex => InnerEndIndex + 1;

		public int InnerLength => InnerEndIndex - InnerStartIndex + 1;

		public int OuterLength => InnerLength + 2;

		public bool ScopeNotEmpty => InnerStartIndex <= InnerEndIndex;

		public int InnerStartIndex { get; private set; }

		public int InnerEndIndex { get; private set; } = -1;

		public void Init(int start, int end)
		{
			InnerStartIndex = start + 1;
			InnerEndIndex = end - 1;
		}

		public bool ContainsOuter(int index)
		{
			if (OuterStartIndex <= index)
			{
				return index <= OuterEndIndex;
			}
			return false;
		}
	}

	public class PickRandomLine : ScopeConversationLine
	{
		public List<CaseLine> ScopeCases;

		public List<int> CalculateSingleLineIndices()
		{
			List<int> list = new List<int>();
			if (ScopeCases == null)
			{
				for (int i = InnerStartIndex; i <= InnerEndIndex; i++)
				{
					list.Add(i);
				}
			}
			else
			{
				int j;
				for (j = InnerStartIndex; j <= InnerEndIndex; j++)
				{
					if (ScopeCases.All((CaseLine x) => !x.ContainsOuter(j)))
					{
						list.Add(j);
					}
				}
			}
			return list;
		}

		public override IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings)
		{
			if (!ScopeNotEmpty)
			{
				Debug.LogError("Unintialized PickRandomLine!");
				return null;
			}
			List<int> list = CalculateSingleLineIndices();
			if (ScopeCases != null)
			{
				int maxExclusive = ScopeCases.Count + list.Count;
				int num = UnityEngine.Random.Range(0, maxExclusive);
				if (num < ScopeCases.Count)
				{
					currentIndex = ScopeCases[num].InnerStartIndex - 1;
					return null;
				}
			}
			if (list.Count > 0)
			{
				int random = list.GetRandom();
				if (!lines.TryGetValue(random, out var value))
				{
					Debug.LogError($"Tried to go to line {random}! Only {lines.Count} lines found!");
					return null;
				}
				currentIndex = OuterEndIndex;
				return value.Do(lines, ref currentIndex, originalSayDialog, writeSettings);
			}
			Debug.LogError("Something went very wrong in PickRandomLine. Couldn't find any single line indices!");
			return null;
		}

		public override void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			stringBuilder.Append('#');
			stringBuilder.Append("pickrandom");
			if (!ScopeNotEmpty)
			{
				stringBuilder.Append("//Invalid indices!");
			}
			else
			{
				stringBuilder.Append("//");
			}
			stringBuilder.Append("(");
			stringBuilder.Append(InnerStartIndex);
			stringBuilder.Append(", ");
			stringBuilder.Append(InnerEndIndex);
			stringBuilder.Append(")");
			if (ScopeCases == null)
			{
				return;
			}
			stringBuilder.Append(" ");
			for (int i = 0; i < ScopeCases.Count; i++)
			{
				CaseLine caseLine = ScopeCases[i];
				stringBuilder.Append("Case ");
				stringBuilder.Append(i);
				stringBuilder.Append(" (");
				stringBuilder.Append(caseLine.OuterStartIndex);
				stringBuilder.Append(",");
				stringBuilder.Append(caseLine.OuterEndIndex);
				stringBuilder.Append(")");
				stringBuilder.Append(",");
			}
			List<int> list = CalculateSingleLineIndices();
			for (int j = 0; j < list.Count; j++)
			{
				int value = list[j];
				stringBuilder.Append("Case ");
				stringBuilder.Append(j + ScopeCases.Count);
				stringBuilder.Append(" (");
				stringBuilder.Append(value);
				stringBuilder.Append(")");
				if (j != list.Count - 1)
				{
					stringBuilder.Append(",");
				}
			}
		}

		public override Result IsValid()
		{
			return ScopeNotEmpty;
		}

		public void AddCase(CaseLine caseEndLine)
		{
			if (ScopeCases == null)
			{
				ScopeCases = new List<CaseLine>();
			}
			caseEndLine.Parent = this;
			ScopeCases.Add(caseEndLine);
		}
	}

	public class CaseLine : ScopeConversationLine
	{
		public PickRandomLine Parent;

		public override IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings)
		{
			if (Parent != null)
			{
				currentIndex = Parent.OuterEndIndex;
			}
			return null;
		}

		public override void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			stringBuilder.Append('#');
			stringBuilder.Append("case");
		}

		public override Result IsValid()
		{
			return ScopeNotEmpty;
		}
	}

	public class ClearConsoleLine : ConversationLine
	{
		public override IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings)
		{
			if (UiOverlay.Instance.TryGetTopPopupOfType<BotStatusAppPopup>(out var popup))
			{
				popup.ClearScreen();
				popup.UpdateDisplay();
			}
			return null;
		}

		public override void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			stringBuilder.Append('#');
			stringBuilder.Append("cls");
		}

		public override Result IsValid()
		{
			return Result.Success;
		}
	}

	public class EndLine : ConversationLine
	{
		public ScopeConversationLine UsedBy;

		public bool IsUsed => UsedBy != null;

		public override IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings)
		{
			if (UsedBy is CaseLine { Parent: { } parent })
			{
				currentIndex = parent.OuterEndIndex;
			}
			return null;
		}

		public override void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			stringBuilder.Append('#');
			stringBuilder.Append("end");
			if (IsUsed)
			{
				stringBuilder.Append("//Started at ");
				stringBuilder.Append(UsedBy.OuterStartIndex);
			}
		}

		public override Result IsValid()
		{
			return IsUsed;
		}
	}

	public sealed class SayLine : ConversationLine
	{
		private sealed class TextEqualityComparer : IEqualityComparer<SayLine>
		{
			public bool Equals(SayLine x, SayLine y)
			{
				if (x == y)
				{
					return true;
				}
				if (x == null)
				{
					return false;
				}
				if (y == null)
				{
					return false;
				}
				return string.Compare(x.UnlocalizedText, y.UnlocalizedText, StringComparison.Ordinal) == 0;
			}

			public int GetHashCode(SayLine obj)
			{
				return obj.UnlocalizedText.Length;
			}
		}

		public string CharacterName;

		public string UnlocalizedText;

		[CanBeNull]
		private LocalizedStringReference _localizedStringReference;

		public bool HasText => !string.IsNullOrWhiteSpace(UnlocalizedText);

		public bool HasCharacterName => !string.IsNullOrWhiteSpace(CharacterName);

		public bool CompletelyEmpty
		{
			get
			{
				if (!HasCharacterName)
				{
					return !HasText;
				}
				return false;
			}
		}

		public static IEqualityComparer<SayLine> TextComparer { get; } = new TextEqualityComparer();

		public SayLine(string characterName, string text)
		{
			CharacterName = characterName;
			UnlocalizedText = text;
		}

		private static int GetFNV1aHashCode(string str)
		{
			if (str == null)
			{
				return 0;
			}
			int length = str.Length;
			int num = length;
			for (int i = 0; i < length; i++)
			{
				num = (num ^ str[i]) * 16777619;
			}
			return num;
		}

		public override IEnumerator Do(ReadOnlyCollection<ConversationLine> lines, ref int currentIndex, SayDialog originalSayDialog, ConversationItemWriteSettings writeSettings)
		{
			bool isNewText = CheckIfUnseenReport();
			string text = GetLocalizedTextOrDefault();
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			ISayDialogCharacter character = SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.GetCharacter(CharacterName);
			ConversationSingleton.ISayDialogOverrideCharacter overrideCharacter = character as ConversationSingleton.ISayDialogOverrideCharacter;
			if (overrideCharacter != null)
			{
				return Say();
			}
			if (character == null || !character.TryGetSayDialog(out var sayDialog))
			{
				sayDialog = originalSayDialog;
			}
			SayDialog.ActiveSayDialog = originalSayDialog;
			sayDialog.SetActive(state: true);
			sayDialog.SetCharacter(character);
			return Say2();
			IEnumerator Say()
			{
				yield return overrideCharacter.DoSay(text, writeSettings);
				yield return writeSettings.WaitForSeconds;
			}
			IEnumerator Say2()
			{
				yield return sayDialog.Say(text, isNewText, writeSettings.ClearPrev, writeSettings.WaitForInput, writeSettings.FadeDone, null);
				yield return writeSettings.WaitForSeconds;
			}
		}

		public override void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			if (HasCharacterName)
			{
				int fNV1aHashCode = GetFNV1aHashCode(CharacterName);
				Color32 color = new Color32((byte)((fNV1aHashCode >> 16) & 0xFF), (byte)((fNV1aHashCode >> 8) & 0xFF), (byte)(fNV1aHashCode & 0xFF), 255);
				stringBuilder.TMPColorSurroundStringBuilderAppend(color, CharacterName);
				stringBuilder.Append(": ");
			}
			if (HasText)
			{
				stringBuilder.Append(UnlocalizedText);
			}
		}

		public override Result IsValid()
		{
			return Result.Success;
		}

		public override bool IsSpecial()
		{
			return false;
		}

		private string GetLocalizedTextOrDefault()
		{
			if (_localizedStringReference != null)
			{
				return _localizedStringReference.GetString();
			}
			return UnlocalizedText;
		}

		private bool CheckIfUnseenReport()
		{
			if (_localizedStringReference != null && _localizedStringReference.Valid && (string)_localizedStringReference.TargetTable == (string)LOC.Story.TABLE_REFERENCE)
			{
				return SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.CheckIfUnseenStoryTextIdReport(_localizedStringReference.KeyId);
			}
			return true;
		}

		public void SetLocalization(LocalizedStringReference stringReference)
		{
			_localizedStringReference = stringReference;
		}

		public void SetNoLocalization()
		{
			_localizedStringReference = null;
		}
	}

	public class ConversationData
	{
		public abstract class Token
		{
			public abstract string GetLocation();

			public override string ToString()
			{
				return GetType().Name + " " + GetLocation();
			}
		}

		private abstract class RangeToken : Token
		{
			public int Start;

			public int End;

			public RangeToken(int start, int end)
			{
				Start = start;
				End = end;
			}

			public string GetString(string line)
			{
				return line.Substring(Start, End - Start + 1);
			}

			public override string GetLocation()
			{
				return $"(Index: {Start}-{End})";
			}
		}

		private class NameToken : RangeToken
		{
			public NameToken(int start, int end)
				: base(start, end)
			{
			}
		}

		private class NumberToken : RangeToken
		{
			public NumberToken(int start, int end)
				: base(start, end)
			{
			}
		}

		private class SpecialCharToken : Token
		{
			public char Char;

			public int Index;

			public SpecialCharToken(char c, int index)
			{
				Char = c;
				Index = index;
			}

			public char Get(string line)
			{
				return Char;
			}

			public override string GetLocation()
			{
				return $"(Index: {Index})";
			}

			public override string ToString()
			{
				return $"({Char} {GetLocation()})";
			}
		}

		public class SpecialParsingException : Exception
		{
			private readonly Token relatedToken;

			public string Line { get; private set; }

			public override string Message
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(base.Message);
					if (Line != null)
					{
						stringBuilder.AppendLine("At line:");
						stringBuilder.AppendLine(Line);
					}
					if (relatedToken != null)
					{
						stringBuilder.AppendLine(relatedToken.GetLocation());
					}
					return stringBuilder.ToString();
				}
			}

			public SpecialParsingException(string message, Token relatedToken = null)
				: base(message)
			{
				this.relatedToken = relatedToken;
			}

			public void AddLineContext(string line)
			{
				Line = line;
			}
		}

		public const char VerbatimChar = '@';

		public const char SpecialChar = '#';

		private readonly char Colon = ':';

		public const char Plus = '+';

		public const char Dot = '.';

		public const char LeftParenthesis = '(';

		public const char RightParenthesis = ')';

		public const char Comma = ',';

		private readonly char[] SpecialChars = new char[5] { '.', '(', ')', '+', ',' };

		public const string AnimateCharacterFunctionName = "animate";

		public const string DefaultCharacterFunctionName = "default";

		public const string PickRandomKeyword = "pickrandom";

		public const string PickRandomKeywordShort = "r";

		public const string EndKeyword = "end";

		public const string CaseKeyword = "case";

		public const string ClearConsoleKeyword = "cls";

		private static List<Token> _tokenCache = new List<Token>();

		private static int _index;

		private static List<int> _scopeLinesCached = new List<int>();

		private List<SayLine> _sayLines = new List<SayLine>();

		private List<ConversationLine> _allLines = new List<ConversationLine>();

		private List<Result> _parsingErrors;

		public ReadOnlyCollection<SayLine> SayLines => _sayLines.AsReadOnly();

		public ReadOnlyCollection<ConversationLine> AllLines => _allLines.AsReadOnly();

		private bool Peek<T>(out T token) where T : Token
		{
			if (_index < _tokenCache.Count && _tokenCache[_index] is T val)
			{
				token = val;
				return true;
			}
			token = null;
			return false;
		}

		private void Consume<T>(out T token) where T : Token
		{
			if (_index >= _tokenCache.Count)
			{
				throw new SpecialParsingException($"Expected {typeof(T)}, but reached the end!");
			}
			Token token2 = _tokenCache[_index];
			if (!(token2 is T val))
			{
				throw new SpecialParsingException($"Expected {typeof(T)}, but got {token2} at {token2.GetLocation()}!", token2);
			}
			token = val;
			_index++;
		}

		private void Consume()
		{
			_index++;
		}

		private void ConsumeSpecialChar(char c)
		{
			if (_index >= _tokenCache.Count)
			{
				throw new SpecialParsingException($"Expected \"{c}\", but reached the end!");
			}
			Token token = _tokenCache[_index];
			if (!(token is SpecialCharToken specialCharToken))
			{
				throw new SpecialParsingException($"Expected \"{c}\", but got {token} at {token.GetLocation()}!", token);
			}
			if (c != specialCharToken.Char)
			{
				throw new SpecialParsingException($"Expected \"{c}\", but got \"{specialCharToken.Char}\"", token);
			}
			_index++;
		}

		private bool PeekSpecialChar(char c)
		{
			if (Peek<SpecialCharToken>(out var token))
			{
				return c == token.Char;
			}
			return false;
		}

		private void ConsumeArguments<T>(out T first) where T : Token
		{
			ConsumeSpecialChar('(');
			Consume<T>(out first);
			ConsumeSpecialChar(')');
		}

		private int ConsumeArguments<T1, T2>(out T1 first, out T2 second, int optionalCount = 0) where T1 : Token where T2 : Token
		{
			int num = 2 - optionalCount;
			first = null;
			second = null;
			int num2 = 0;
			ConsumeSpecialChar('(');
			if (num >= 1 || Peek<T1>(out var _))
			{
				Consume<T1>(out first);
				num2++;
				if (num >= 2 || PeekSpecialChar(','))
				{
					ConsumeSpecialChar(',');
					Consume<T2>(out second);
					num2++;
				}
			}
			ConsumeSpecialChar(')');
			return num2;
		}

		private void ParseCharacterItem(NameToken nameToken, string line)
		{
			string text = nameToken.GetString(line);
			ConsumeSpecialChar('.');
			Consume<NameToken>(out var token);
			string text2 = token.GetString(line);
			if (!(text2 == "animate"))
			{
				if (text2 == "default")
				{
					AddSpecialLine(new ConversationEmoteDefault(text));
					return;
				}
				ConsumeSpecialChar('.');
				Consume<NameToken>(out var token2);
				ConversationEmote conversationEmote = new ConversationEmote(text, text2, token2.GetString(line));
				if (PeekSpecialChar('('))
				{
					ConsumeArguments<NameToken>(out var first);
					string text3 = first.GetString(line);
					if (!string.Equals(text3, "instant", StringComparison.OrdinalIgnoreCase))
					{
						throw new SpecialParsingException("Unknown argument \"" + text3 + "\" in " + first.GetLocation(), first);
					}
					conversationEmote.Data.instant = true;
				}
				AddSpecialLine(conversationEmote);
				return;
			}
			int num = ConsumeArguments<NameToken, NumberToken>(out var first2, out var second, 1);
			string text4 = first2.GetString(line);
			if (!Enum.TryParse<SimpleCharacterSingleton.AnimationsList>(text4, ignoreCase: true, out var result))
			{
				throw new SpecialParsingException(text4 + " is not an allowed animation!", first2);
			}
			float result2;
			if (num >= 2)
			{
				string text5 = second.GetString(line);
				if (!float.TryParse(text5, NumberStyles.Any, CultureInfo.InvariantCulture, out result2))
				{
					throw new SpecialParsingException(text5 + " is not an allowed time!", second);
				}
			}
			else
			{
				result2 = 0.5f;
			}
			AddSpecialLine(new ConversationAnimation(text, result, result2));
		}

		private void AddSpecialLine(ConversationLine line)
		{
			_allLines.Add(line);
		}

		private Result ParseSpecialLine(string line)
		{
			_tokenCache.Clear();
			int start = 0;
			int i = 0;
			while (i < line.Length)
			{
				char c = line[i];
				i++;
				if (SpecialChars.Contains(c))
				{
					_tokenCache.Add(new SpecialCharToken(c, i - 1));
				}
				else if (char.IsDigit(c))
				{
					for (; i < line.Length && (char.IsDigit(line[i]) || line[i] == '.'); i++)
					{
					}
					_tokenCache.Add(new NumberToken(start, i - 1));
				}
				else if (char.IsLetter(c) || c == '_')
				{
					for (; i < line.Length && (char.IsLetterOrDigit(line[i]) || line[i] == '_'); i++)
					{
					}
					_tokenCache.Add(new NameToken(start, i - 1));
				}
				start = i;
			}
			try
			{
				_index = 0;
				if (_index < _tokenCache.Count)
				{
					Consume<NameToken>(out var token);
					switch (token.GetString(line))
					{
					case "pickrandom":
					case "r":
						_scopeLinesCached.Add(_allLines.Count);
						_allLines.Add(new PickRandomLine());
						break;
					case "end":
						_scopeLinesCached.Add(_allLines.Count);
						_allLines.Add(new EndLine());
						break;
					case "case":
						_scopeLinesCached.Add(_allLines.Count);
						_allLines.Add(new CaseLine());
						break;
					case "cls":
						_scopeLinesCached.Add(_allLines.Count);
						_allLines.Add(new ClearConsoleLine());
						break;
					default:
						ParseCharacterItem(token, line);
						while (PeekSpecialChar('+'))
						{
							Consume();
							Consume<NameToken>(out var token2);
							ParseCharacterItem(token2, line);
						}
						break;
					}
					if (Peek<Token>(out var token3))
					{
						throw new SpecialParsingException($"Unexpected token {token3} at {token3.GetLocation()}!", token3);
					}
					return Result.Success;
				}
			}
			catch (SpecialParsingException ex)
			{
				ex.AddLineContext(line);
				_allLines.Add(new BrokenLine(ex));
				ExceptionResult exceptionResult = Result.FromException(ex);
				AddParsingError(exceptionResult);
				exceptionResult.DebugLogError();
				return exceptionResult;
			}
			return Result.Success;
		}

		private void AddLine(string name, string text)
		{
			if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(text))
			{
				SayLine item = new SayLine(name, text);
				_allLines.Add(item);
				_sayLines.Add(item);
			}
		}

		private bool PeekScopeLine(out ConversationLine token, out int itemIndex)
		{
			if (_index < _scopeLinesCached.Count)
			{
				itemIndex = _scopeLinesCached[_index];
				token = _allLines[itemIndex];
				return true;
			}
			itemIndex = -1;
			token = null;
			return false;
		}

		private bool PeekScopeLine<T>(out T token, out int itemIndex) where T : ConversationLine
		{
			if (_index < _scopeLinesCached.Count)
			{
				itemIndex = _scopeLinesCached[_index];
				if (_allLines[itemIndex] is T val)
				{
					token = val;
					return true;
				}
			}
			itemIndex = -1;
			token = null;
			return false;
		}

		private void ParsePickRandomLine(PickRandomLine pickRandomLine, int startIndex)
		{
			bool flag = false;
			Consume();
			ConversationLine token;
			int itemIndex;
			while (!flag && PeekScopeLine(out token, out itemIndex))
			{
				if (!(token is EndLine endLine))
				{
					if (token is CaseLine caseLine)
					{
						Consume();
						if (PeekScopeLine(out var token2, out var itemIndex2))
						{
							if (!(token2 is CaseLine))
							{
								if (token2 is EndLine endLine2)
								{
									Consume();
									endLine2.UsedBy = caseLine;
									caseLine.Init(itemIndex, itemIndex2);
									pickRandomLine.AddCase(caseLine);
								}
								else
								{
									SpecialParsingException ex = new SpecialParsingException($"Pick random's case expected {typeof(EndLine)}!");
									_allLines[itemIndex] = new BrokenLine(ex, token);
									AddParsingError(Result.FromException(ex));
									flag = true;
								}
							}
							else
							{
								caseLine.Init(itemIndex, itemIndex2);
								pickRandomLine.AddCase(caseLine);
							}
						}
						else
						{
							SpecialParsingException ex2 = new SpecialParsingException($"Pick random's case expected {typeof(EndLine)}!");
							_allLines[itemIndex] = new BrokenLine(ex2, token);
							AddParsingError(Result.FromException(ex2));
							flag = true;
						}
					}
					else
					{
						SpecialParsingException ex3 = new SpecialParsingException($"Pick random got unexpected {token}\"!");
						_allLines[itemIndex] = new BrokenLine(ex3, token);
						AddParsingError(Result.FromException(ex3));
						flag = true;
					}
					continue;
				}
				Consume();
				endLine.UsedBy = pickRandomLine;
				pickRandomLine.Init(startIndex, itemIndex);
				return;
			}
			pickRandomLine.Init(startIndex, _allLines.Count);
		}

		private void ParseScopes()
		{
			_index = 0;
			ConversationLine token;
			int itemIndex;
			while (PeekScopeLine(out token, out itemIndex))
			{
				if (token is PickRandomLine pickRandomLine)
				{
					ParsePickRandomLine(pickRandomLine, itemIndex);
					continue;
				}
				Consume();
				AddParsingError(Result.FailWithDescription($"Unexpected {token.GetType()} at line {itemIndex}"));
			}
		}

		public void Parse(string text)
		{
			_scopeLinesCached.Clear();
			Clear();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			int num = 0;
			int length = text.Length;
			while (num < length)
			{
				int num2 = text.IndexOf('\n', num);
				bool flag = false;
				if (num2 == -1)
				{
					num2 = length;
				}
				else if (num2 > 0 && text[num2 - 1] == '\r')
				{
					flag = true;
					num2--;
				}
				int num3 = num2 - num;
				if (num3 > 0)
				{
					switch (text[num])
					{
					case '#':
						ParseSpecialLine(text.Substring(num + 1, num3 - 1));
						break;
					case '@':
						AddLine("", text.Substring(num + 1, num3 - 1));
						break;
					default:
					{
						int num4 = text.IndexOf(Colon, num, num3);
						if (num4 <= num)
						{
							AddLine("", text.Substring(num, num3));
							break;
						}
						string name = text.Substring(num, num4 - num);
						string text2;
						if (num4 + 1 >= num2)
						{
							text2 = "";
						}
						else
						{
							text2 = ((num4 + 2 >= num2 || !char.IsWhiteSpace(text[num4 + 1])) ? text.Substring(num4 + 1, num2 - num4 - 1) : text.Substring(num4 + 2, num2 - num4 - 2));
						}
						AddLine(name, text2);
						break;
					}
					}
				}
				num = ((!flag) ? (num2 + 1) : (num2 + 2));
			}
			ParseScopes();
		}

		public IEnumerator DoConversation(ConversationItemWriteSettings writeSettings = null)
		{
			if (!_allLines.Any())
			{
				yield break;
			}
			if (writeSettings == null)
			{
				writeSettings = ConversationItemWriteSettings.DefaultWriteSettings;
			}
			SayDialog originalSayDialog = SayDialog.GetSayDialog();
			ReadOnlyCollection<ConversationLine> lines = AllLines;
			for (int i = 0; i < lines.Count; i++)
			{
				IEnumerator enumerator = lines[i].Do(lines, ref i, originalSayDialog, writeSettings);
				if (enumerator != null)
				{
					yield return enumerator;
				}
				yield return null;
			}
		}

		public void SetLocalization(IEnumerable<LocalizedStringReference> localizedLines)
		{
			using IEnumerator<LocalizedStringReference> enumerator = localizedLines.GetEnumerator();
			for (int i = 0; i < _sayLines.Count; i++)
			{
				SayLine sayLine = _sayLines[i];
				if (enumerator.MoveNext())
				{
					sayLine.SetLocalization(enumerator.Current);
				}
				else
				{
					sayLine.SetNoLocalization();
				}
			}
		}

		public void Clear()
		{
			_sayLines.Clear();
			_allLines.Clear();
			_parsingErrors = null;
		}

		private void AddParsingError(Result result)
		{
			if (_parsingErrors == null)
			{
				_parsingErrors = new List<Result>();
			}
			_parsingErrors.Add(result);
		}

		public Result CheckAllSpecialItems()
		{
			return Result.All(_allLines.Select((ConversationLine t) => t.IsValid()));
		}

		public bool TryGetParsingErrors(out ReadOnlyCollection<Result> errors)
		{
			if (_parsingErrors == null)
			{
				errors = null;
				return false;
			}
			errors = _parsingErrors.AsReadOnly();
			return true;
		}

		public string RichTextRepresentation(bool lineIndexing = true)
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				for (int i = 0; i < _allLines.Count; i++)
				{
					ConversationLine conversationLine = _allLines[i];
					Result result = conversationLine.IsValid();
					if (result.IsSuccess)
					{
						bool flag = conversationLine.IsSpecial();
						if (flag)
						{
							stringBuilder.AppendColorTagStart(Color.green);
						}
						if (lineIndexing)
						{
							stringBuilder.Append(i);
							stringBuilder.Append(". ");
						}
						conversationLine.AppendToStringBuilder(stringBuilder);
						if (flag)
						{
							stringBuilder.AppendColorTagEnd();
						}
					}
					else
					{
						stringBuilder.AppendColorTagStart(Color.red);
						if (lineIndexing)
						{
							stringBuilder.Append(i);
							stringBuilder.Append(". ");
						}
						conversationLine.AppendToStringBuilder(stringBuilder);
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("Error ^ :");
						stringBuilder.AppendLine(result.GetDescription());
						stringBuilder.AppendColorTagEnd();
					}
					stringBuilder.AppendLine();
				}
				return stringBuilder.ToString();
			}
		}

		public string HtmlRepresentation(bool lineIndexing = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < _allLines.Count; i++)
			{
				ConversationLine conversationLine = _allLines[i];
				Result result = conversationLine.IsValid();
				string text;
				if (result.IsSuccess)
				{
					text = (conversationLine.IsSpecial() ? "#2ecc71" : "#2c3e50");
				}
				else
				{
					text = "#e74c3c";
				}
				stringBuilder.Append("<span style=\"color:" + text + "\">");
				conversationLine.AppendToStringBuilder(stringBuilder);
				stringBuilder.Append("</span>");
				if (!result.IsSuccess)
				{
					stringBuilder.Append(" <span style=\"color:#c0392b; font-size:0.9em;\">");
					stringBuilder.Append(WebUtility.HtmlEncode("← Error: " + result.GetDescription()));
					stringBuilder.Append("</span>");
				}
				stringBuilder.Append("<br>");
			}
			return stringBuilder.ToString();
		}
	}

	public static IEnumerator DoConversation(ConversationData conversationData, ConversationItemWriteSettings writeSettings = null)
	{
		return conversationData.DoConversation(writeSettings);
	}

	public static IEnumerator DoConversation(string conversationText, ConversationItemWriteSettings writeSettings = null)
	{
		ConversationData conversationData = new ConversationData();
		conversationData.Parse(conversationText);
		return DoConversation(conversationData, writeSettings);
	}

	public static IEnumerator DoModdableConversation(string conversationText, ConversationItemWriteSettings writeSettings = null)
	{
		ConversationData conversationData = new ConversationData();
		conversationData.Parse(conversationText);
		return DoConversation(conversationData, writeSettings);
	}
}
