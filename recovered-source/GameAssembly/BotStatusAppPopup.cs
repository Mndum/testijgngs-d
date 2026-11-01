using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class BotStatusAppPopup : PrefabPopup<BotStatusAppPopup>
{
	private sealed class StringLinePool : BetterObjectPool<StringConsoleLine>
	{
		public StringLinePool(int maxCachedInstances = 100)
			: base(maxCachedInstances)
		{
		}

		protected override void OnAfterAddedToPool(StringConsoleLine obj)
		{
			obj.Clear();
		}
	}

	private sealed class StringBuilderLinePool : BetterObjectPool<StringBuilderConsoleLine>
	{
		public StringBuilderLinePool(int maxCachedInstances = 5)
			: base(maxCachedInstances)
		{
		}

		protected override void OnAfterAddedToPool(StringBuilderConsoleLine obj)
		{
			obj.Clear();
		}
	}

	public abstract class ConsoleLine
	{
		public virtual bool HasTextInfoProcessing => false;

		public abstract int GetApproximateLineCount(BotStatusAppPopup popup);

		public virtual void DoTextInfoProcessing(int linkIndex, TextMeshProUGUI _consoleText)
		{
		}

		public abstract void PrintTo(StringBuilder consoleStringBuilder, BotStatusAppPopup _popup);

		public static implicit operator ConsoleLine(string s)
		{
			return (StringConsoleLine)s;
		}

		public static implicit operator ConsoleLine(StringBuilder sb)
		{
			return (StringBuilderConsoleLine)sb;
		}
	}

	public class StringConsoleLine : ConsoleLine
	{
		public enum StringPrintTypesEnum
		{
			Normal,
			Center,
			DecorativeWrapCenter
		}

		private int _lastLineCount;

		public string String { get; set; }

		public StringPrintTypesEnum StringPrintType { get; set; }

		public int MaxVerticalLinesAllowed { get; set; }

		public void Init(string value)
		{
			String = value;
			StringPrintType = StringPrintTypesEnum.Normal;
			MaxVerticalLinesAllowed = 2147483647;
		}

		public void Clear()
		{
			String = "";
			StringPrintType = StringPrintTypesEnum.Normal;
			MaxVerticalLinesAllowed = 2147483647;
		}

		public override int GetApproximateLineCount(BotStatusAppPopup popup)
		{
			return StringPrintType switch
			{
				StringPrintTypesEnum.Center => _lastLineCount, 
				StringPrintTypesEnum.DecorativeWrapCenter => _lastLineCount, 
				_ => MyMathUtility.CountAndWidthToLines(String.Length, popup.WidthCharCount), 
			};
		}

		public override void PrintTo(StringBuilder consoleStringBuilder, BotStatusAppPopup _popup)
		{
			int maxLineLength;
			switch (StringPrintType)
			{
			default:
				consoleStringBuilder.AppendLine(String);
				break;
			case StringPrintTypesEnum.Center:
			{
				int widthCharCount = _popup.WidthCharCount;
				List<StringUtils.LineSplitInfo> lineSplits2;
				using (String.SplitIntoLinesThatFit(widthCharCount, out lineSplits2, out maxLineLength))
				{
					int num5 = (_lastLineCount = Mathf.Min(lineSplits2.Count, MaxVerticalLinesAllowed));
					for (int j = 0; j < num5; j++)
					{
						StringUtils.LineSplitInfo lineSplitInfo2 = lineSplits2[j];
						int num6 = Mathf.Min(widthCharCount, lineSplitInfo2.Length);
						consoleStringBuilder.Append(' ', (widthCharCount - num6) / 2);
						lineSplitInfo2.AppendToStringBuilder(String, consoleStringBuilder);
						consoleStringBuilder.AppendLine();
					}
					break;
				}
			}
			case StringPrintTypesEnum.DecorativeWrapCenter:
			{
				int num = _popup.WidthCharCount - 2;
				List<StringUtils.LineSplitInfo> lineSplits;
				using (String.SplitIntoLinesThatFit(num, out lineSplits, out maxLineLength))
				{
					consoleStringBuilder.Append('O');
					consoleStringBuilder.Append('-', num);
					consoleStringBuilder.Append('O');
					consoleStringBuilder.AppendLine();
					int num2 = Mathf.Min(lineSplits.Count, MaxVerticalLinesAllowed - 2);
					_lastLineCount = num2 + 2;
					for (int i = 0; i < num2; i++)
					{
						StringUtils.LineSplitInfo lineSplitInfo = lineSplits[i];
						int num3 = Mathf.Min(num, lineSplitInfo.Length);
						consoleStringBuilder.Append('|');
						int num4 = (num - num3) / 2;
						int repeatCount = num - num4 - num3;
						consoleStringBuilder.Append(' ', num4);
						lineSplitInfo.AppendToStringBuilder(String, consoleStringBuilder);
						consoleStringBuilder.Append(' ', repeatCount);
						consoleStringBuilder.Append('|');
						consoleStringBuilder.AppendLine();
					}
					consoleStringBuilder.Append('O');
					consoleStringBuilder.Append('-', num);
					consoleStringBuilder.Append('O');
					consoleStringBuilder.AppendLine();
					break;
				}
			}
			}
		}

		public static implicit operator StringConsoleLine(string s)
		{
			_stringLinePool.Acquire(out var pooled);
			pooled.Init(s);
			return pooled;
		}
	}

	public class StringBuilderConsoleLine : ConsoleLine
	{
		public StringBuilder StringBuilder { get; private set; }

		public void Init()
		{
			StringBuilderCache.AcquireManual(out var stringBuilder);
			StringBuilder = stringBuilder;
		}

		public void Init(StringBuilder stringBuilder)
		{
			StringBuilder = stringBuilder;
		}

		public void Clear()
		{
			StringBuilderCache.Release(StringBuilder);
			StringBuilder = null;
		}

		public override int GetApproximateLineCount(BotStatusAppPopup popup)
		{
			return MyMathUtility.CountAndWidthToLines(StringBuilder.Length, popup.WidthCharCount);
		}

		public override void PrintTo(StringBuilder consoleStringBuilder, BotStatusAppPopup _popup)
		{
			consoleStringBuilder.Append(StringBuilder);
			consoleStringBuilder.AppendLine();
		}

		public static implicit operator StringBuilderConsoleLine(StringBuilder sb)
		{
			_stringBuilderLinePool.Acquire(out var pooled);
			pooled.Init(sb);
			return pooled;
		}
	}

	public class CharArrayConsoleLine : ConsoleLine
	{
		public enum CharArrayPrintTypesEnum
		{
			Raw,
			Center
		}

		public int Width;

		public int Height;

		public char[] CharArray;

		public const char LoadedChar = '█';

		public const char NotLoadedChar = '▒';

		public CharArrayPrintTypesEnum PrintType { get; set; }

		public Color32[] Colors { get; set; }

		public override bool HasTextInfoProcessing => Colors != null;

		public CharArrayConsoleLine(int width, CharArrayPrintTypesEnum printType)
		{
			CharArray = new char[width];
			Width = width;
			Height = 1;
			PrintType = printType;
		}

		public CharArrayConsoleLine(char[] charArray)
		{
			CharArray = charArray;
			Width = charArray.Length;
			Height = 1;
			PrintType = CharArrayPrintTypesEnum.Raw;
		}

		public CharArrayConsoleLine(char[] charArray, int width, int height, CharArrayPrintTypesEnum printType)
		{
			CharArray = charArray;
			Width = width;
			Height = height;
			if (width * height != CharArray.Length)
			{
				throw new ArgumentException($"Width({width}) and height({height}) give {width * height}, which doesn't fit the char arrayLength {charArray.Length}");
			}
			PrintType = printType;
		}

		public void SetAsProgress(float percent, char loadedChar = '█', char notLoadedChar = '▒')
		{
			float num = CharArray.Length;
			for (int i = 0; i < CharArray.Length; i++)
			{
				CharArray[i] = (((float)(i + 1) / num <= percent) ? loadedChar : notLoadedChar);
			}
		}

		public void SetAsProgress(int startIndex, int endIndex, float percent, char loadedChar = '█', char notLoadedChar = '▒')
		{
			startIndex = Mathf.Clamp(startIndex, 0, CharArray.Length);
			endIndex = Mathf.Clamp(endIndex, 0, CharArray.Length);
			if (startIndex <= endIndex)
			{
				float num = endIndex - startIndex;
				for (int i = startIndex; i < endIndex; i++)
				{
					CharArray[i] = (((float)(i + 1) / num <= percent) ? loadedChar : notLoadedChar);
				}
			}
		}

		public override void DoTextInfoProcessing(int linkIndex, TextMeshProUGUI _consoleText)
		{
			if (Colors == null)
			{
				return;
			}
			TMP_TextInfo textInfo = _consoleText.textInfo;
			string text = linkIndex.ToString(CultureInfo.InvariantCulture);
			TMP_LinkInfo[] linkInfo = textInfo.linkInfo;
			for (int i = 0; i < linkInfo.Length; i++)
			{
				TMP_LinkInfo tMP_LinkInfo = linkInfo[i];
				if (tMP_LinkInfo.linkIdLength != text.Length || tMP_LinkInfo.GetLinkID() != text)
				{
					continue;
				}
				int num = Mathf.Min(CharArray.Length, Colors.Length);
				int num2 = 0;
				int num3 = textInfo.characterInfo.Length;
				int num4 = tMP_LinkInfo.linkTextfirstCharacterIndex;
				while (num2 < num && num4 < num3)
				{
					TMP_CharacterInfo tMP_CharacterInfo = textInfo.characterInfo[num4];
					char c = CharArray[num2];
					num4++;
					if (tMP_CharacterInfo.character == c)
					{
						if (tMP_CharacterInfo.isVisible)
						{
							Color32 color = Colors[num2];
							int materialReferenceIndex = tMP_CharacterInfo.materialReferenceIndex;
							int vertexIndex = tMP_CharacterInfo.vertexIndex;
							Color32[] colors = textInfo.meshInfo[materialReferenceIndex].colors32;
							colors[vertexIndex] = color;
							colors[vertexIndex + 1] = color;
							colors[vertexIndex + 2] = color;
							colors[vertexIndex + 3] = color;
						}
						num2++;
					}
				}
				break;
			}
		}

		public override int GetApproximateLineCount(BotStatusAppPopup popup)
		{
			if (Width > popup.WidthCharCount)
			{
				return Height * MyMathUtility.CountAndWidthToLines(Width, popup.WidthCharCount);
			}
			return Height;
		}

		public override void PrintTo(StringBuilder consoleStringBuilder, BotStatusAppPopup _popup)
		{
			CharArrayPrintTypesEnum printType = PrintType;
			if (printType == CharArrayPrintTypesEnum.Raw || printType != CharArrayPrintTypesEnum.Center)
			{
				for (int i = 0; i < Height; i++)
				{
					consoleStringBuilder.Append(CharArray, i * Width, Width);
					consoleStringBuilder.AppendLine();
				}
				return;
			}
			int repeatCount = (_popup.WidthCharCount - Width) / 2;
			for (int j = 0; j < Height; j++)
			{
				consoleStringBuilder.Append(' ', repeatCount);
				consoleStringBuilder.Append(CharArray, j * Width, Width);
				consoleStringBuilder.AppendLine();
			}
		}
	}

	[SerializeField]
	private TextMeshProUGUI _title;

	[SerializeField]
	private TextMeshProUGUI _consoleText;

	[SerializeField]
	private TMP_InputField _inputField;

	[SerializeField]
	private ScrollRect _scrollRect;

	[SerializeField]
	private Image _background;

	private StringBuilder _consoleStringBuilder;

	private const float VeryShortDelay = 0.0025f;

	private const float NormalDelay = 0.1f;

	private const float LongDelay = 0.25f;

	private const int InputHistoryMaxLength = 50;

	private const int MaxLines = 100;

	private static StringLinePool _stringLinePool = new StringLinePool();

	private static StringBuilderLinePool _stringBuilderLinePool = new StringBuilderLinePool();

	private List<ConsoleLine> _currentLines = new List<ConsoleLine>();

	private bool playerInputAllowed;

	private int selectedUserInputIndex = -1;

	private List<string> lastUserInputs = new List<string>();

	private bool stopFlagRaised;

	private const string BotAddress = "69.69.69.69";

	private CancellationTokenSource _mainTokenSource;

	private CanvasGroup _canvasGroup;

	public int WidthCharCount { get; private set; }

	public int HeightCharCount { get; private set; }

	public float CharAspectRatio { get; private set; }

	public float CharInverseAspectRatio { get; private set; }

	public void LoadConsoleLook()
	{
		BotStatusAppManager botStatusAppManager = GameVariables.Current.botStatusAppManager;
		SetConsoleColorsInner(botStatusAppManager._consoleStyle);
	}

	public ConsoleStyleStruct GetConsoleStyle()
	{
		return GameVariables.Current.botStatusAppManager._consoleStyle;
	}

	public void SetConsoleStyle(ConsoleStyleStruct style)
	{
		GameVariables.Current.botStatusAppManager._consoleStyle = style;
		SetConsoleColorsInner(style);
	}

	private void SetConsoleColorsInner(ConsoleStyleStruct style)
	{
		Color color = style.TextColor;
		Color color2 = style.BackgroundColor;
		_background.color = color2;
		_consoleText.color = color;
		_inputField.GetComponent<Image>().color = color;
		_inputField.textComponent.color = color;
	}

	public StringConsoleLine GetStringConsoleLine(string data)
	{
		return data;
	}

	public StringBuilderConsoleLine GetStringBuilderConsoleLine()
	{
		StringBuilderConsoleLine stringBuilderConsoleLine = _stringBuilderLinePool.Acquire();
		stringBuilderConsoleLine.Init();
		return stringBuilderConsoleLine;
	}

	private void ReturnToPool(ConsoleLine consoleLine)
	{
		if (consoleLine is StringConsoleLine instance)
		{
			_stringLinePool.Release(instance);
		}
		else if (consoleLine is StringBuilderConsoleLine instance2)
		{
			_stringBuilderLinePool.Release(instance2);
		}
	}

	private void DetermineStyleAndWidth(out TMP_Style style, out float widthEnforcedByMonospace)
	{
		TMP_StyleSheet styleSheet = TMP_Settings.GetStyleSheet();
		switch (LocalizationSettings.SelectedLocaleAsync.Result.Identifier.Code)
		{
		case "zh-Hans":
		case "zh-Hant":
		case "ko":
		case "ja":
			style = styleSheet.GetStyle("MonoSpace1em");
			widthEnforcedByMonospace = 1f;
			break;
		default:
			style = styleSheet.GetStyle("MonoSpace");
			widthEnforcedByMonospace = 0.5f;
			break;
		}
	}

	private void CalculateConsoleSizeValues()
	{
		Canvas.ForceUpdateCanvases();
		Rect localRect = _scrollRect.transform.AsRectTransform().GetLocalRect();
		float width = localRect.width;
		float num = localRect.height - _inputField.transform.AsRectTransform().GetLocalRect().height;
		TextMeshProUGUI consoleText = _consoleText;
		DetermineStyleAndWidth(out var style, out var widthEnforcedByMonospace);
		TMP_Style textStyle = (_inputField.textComponent.textStyle = style);
		consoleText.textStyle = textStyle;
		float fontSize = consoleText.fontSize;
		float num2 = fontSize * widthEnforcedByMonospace;
		CharAspectRatio = widthEnforcedByMonospace;
		CharInverseAspectRatio = 1f / CharAspectRatio;
		WidthCharCount = Mathf.FloorToInt(width / num2) - 2;
		HeightCharCount = Mathf.FloorToInt(num / fontSize) - 1;
		_inputField.characterLimit = WidthCharCount * 5;
		Debug.Log($"Size is {width}x{num} pixels. Console is {WidthCharCount}x{HeightCharCount} characters!");
		Debug.Log($"Character Aspect Ratio is {CharAspectRatio} (Width: {num2}, Height: {fontSize})");
	}

	public ConsoleLine PrintLine(ConsoleLine s)
	{
		_currentLines.Add(s);
		return s;
	}

	public bool RemoveLine(ConsoleLine inputLine)
	{
		int num = _currentLines.IndexOf(inputLine);
		if (num >= 0)
		{
			ReturnToPool(inputLine);
			_currentLines.RemoveAt(num);
			return true;
		}
		return false;
	}

	public void ClearScreen()
	{
		foreach (ConsoleLine currentLine in _currentLines)
		{
			ReturnToPool(currentLine);
		}
		_currentLines.Clear();
	}

	public void UpdateDisplay()
	{
		int num = 0;
		for (int num2 = _currentLines.Count - 1; num2 > 0; num2--)
		{
			num += _currentLines[num2].GetApproximateLineCount(this);
			if (num > 100)
			{
				for (int i = 0; i < num2; i++)
				{
					ReturnToPool(_currentLines[i]);
				}
				_currentLines.RemoveRange(0, num2);
				break;
			}
		}
		_consoleStringBuilder.Clear();
		bool flag = false;
		for (int j = 0; j < _currentLines.Count; j++)
		{
			ConsoleLine consoleLine = _currentLines[j];
			bool hasTextInfoProcessing = consoleLine.HasTextInfoProcessing;
			if (hasTextInfoProcessing)
			{
				_consoleStringBuilder.Append("<link=\"");
				_consoleStringBuilder.Append(j);
				_consoleStringBuilder.Append("\">");
			}
			consoleLine.PrintTo(_consoleStringBuilder, this);
			if (hasTextInfoProcessing)
			{
				_consoleStringBuilder.Append("</link>");
			}
			flag |= hasTextInfoProcessing;
		}
		_consoleText.overrideColorTags = flag;
		_consoleText.SetText(_consoleStringBuilder);
		_consoleText.ForceMeshUpdate();
		if (flag)
		{
			for (int k = 0; k < _currentLines.Count; k++)
			{
				ConsoleLine consoleLine2 = _currentLines[k];
				if (consoleLine2.HasTextInfoProcessing)
				{
					consoleLine2.DoTextInfoProcessing(k, _consoleText);
				}
			}
			_consoleText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
		}
		float num3 = 0f;
		RectTransformExtensions.SetSizeDeltaY(y: ((!string.IsNullOrEmpty(_consoleText.text)) ? _consoleText.GetRenderedValues().y : 0f) + _inputField.GetComponent<RectTransform>().sizeDelta.y, rt: _scrollRect.content);
		_scrollRect.ScrollToBottom();
		transform.AsRectTransform().ForceUpdateRectTransforms();
	}

	private async UniTask ClearScreenDelay(float delay = 0.0025f, float randomDelay = 0.0025f, CancellationToken cancellationToken = default(CancellationToken))
	{
		await UniTask.Delay(TimeSpan.FromSeconds(delay + UnityEngine.Random.Range(0f, randomDelay)), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		ClearScreen();
		UpdateDisplay();
	}

	public async UniTask PrintLineDelay(ConsoleLine s, float delay = 0.0025f, float randomDelay = 0.0025f, CancellationToken cancellationToken = default(CancellationToken))
	{
		await UniTask.Delay(TimeSpan.FromSeconds(delay + UnityEngine.Random.Range(0f, randomDelay)), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		PrintLine(s);
		UpdateDisplay();
	}

	public async UniTask FakeLoading(float length, int width = 20, bool hideAfterDone = true, CancellationToken cancellationToken = default(CancellationToken))
	{
		char[] charArray = new char[width];
		CharArrayConsoleLine charArrayConsoleLine = new CharArrayConsoleLine(charArray);
		Func<float, float> choppyLerp = MyMathUtility.GetRandomChoppyLerp();
		charArrayConsoleLine.SetAsProgress(0f);
		PrintLine(charArrayConsoleLine);
		UpdateDisplay();
		float passed = 0f;
		while (passed < length)
		{
			await UniTask.NextFrame(cancellationToken);
			passed += Time.deltaTime;
			charArrayConsoleLine.SetAsProgress(choppyLerp(passed / length));
			UpdateDisplay();
		}
		if (hideAfterDone)
		{
			RemoveLine(charArrayConsoleLine);
		}
		else
		{
			charArrayConsoleLine.SetAsProgress(1f);
		}
		UpdateDisplay();
	}

	public async UniTask WaitRandomShortDelay(CancellationToken cancellationToken = default(CancellationToken))
	{
		await UniTask.WaitForSeconds(UnityEngine.Random.Range(0.0025f, 0.1f), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
	}

	public async UniTask WaitRandomLongDelay(CancellationToken cancellationToken = default(CancellationToken))
	{
		await UniTask.WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.25f), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
	}

	private void GetPrevUserInputUp()
	{
		selectedUserInputIndex++;
		if (lastUserInputs.TryGetValue(selectedUserInputIndex, out var value))
		{
			_inputField.text = value;
			_inputField.caretPosition = value.Length;
		}
		else if (selectedUserInputIndex >= lastUserInputs.Count)
		{
			selectedUserInputIndex = lastUserInputs.Count - 1;
		}
	}

	private void GetPrevUserInputDown()
	{
		selectedUserInputIndex--;
		string value;
		if (selectedUserInputIndex < 0)
		{
			selectedUserInputIndex = -1;
			_inputField.text = "";
		}
		else if (lastUserInputs.TryGetValue(selectedUserInputIndex, out value))
		{
			_inputField.text = value;
			_inputField.caretPosition = value.Length;
		}
	}

	public void StartPlayerInput()
	{
		_inputField.gameObject.SetActive(value: true);
		selectedUserInputIndex = -1;
		_inputField.Select();
		_inputField.ActivateInputField();
		_inputField.text = "";
		playerInputAllowed = true;
	}

	public bool TryEatPlayerInput(out string str)
	{
		str = _inputField.text;
		if (!string.IsNullOrEmpty(str))
		{
			_inputField.text = "";
			return true;
		}
		return false;
	}

	public void EndPlayerInput()
	{
		_inputField.gameObject.SetActive(value: false);
		playerInputAllowed = false;
	}

	public async UniTask<string> GetPlayerInput(bool saveInMemory = true, CancellationToken cancellationToken = default(CancellationToken))
	{
		StartPlayerInput();
		UniTask<string> sAsync = _inputField.OnSubmitAsync(cancellationToken);
		if (lastUserInputs.Count > 0)
		{
			while (!sAsync.Status.IsCompleted())
			{
				await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
				if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					GetPrevUserInputUp();
				}
				else if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					GetPrevUserInputDown();
				}
			}
		}
		string text = await sAsync;
		if (saveInMemory && (lastUserInputs.Count == 0 || (lastUserInputs[0] != text && !string.IsNullOrWhiteSpace(text))))
		{
			lastUserInputs.Insert(0, text);
			lastUserInputs.EnsureListSizeIsAtMost(50);
		}
		EndPlayerInput();
		return text;
	}

	private async UniTask FileRead(string fileData, CancellationToken cancellationToken)
	{
		foreach (string item in fileData.SplitNewlinesAsStream())
		{
			if (item == "!!delay")
			{
				await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(0.0025f, 0.1f)), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			else if (item == "!!longdelay")
			{
				await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(0.1f, 0.25f)), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			else
			{
				await PrintLineDelay(item, 0.0025f, 0.0025f, cancellationToken);
			}
		}
	}

	public async UniTask ShowEquipment(CancellationToken cancellationToken)
	{
		await PrintLineDelay(LOC.UI.StatusApp_BasicModules, 0.0025f, 0.0025f, cancellationToken);
		List<Item> itemsInNormalSlots;
		using (MyCollectionPool<Item>.GetListAndFill(GameVariables.Current.itemManager.BotEquippedItems(), out itemsInNormalSlots))
		{
			StringBuilderConsoleLine line = GetStringBuilderConsoleLine();
			List<ItemSlot> list = new List<ItemSlot>
			{
				ItemSlot.SlotManager.ArmL,
				ItemSlot.SlotManager.ArmR,
				ItemSlot.SlotManager.LegL,
				ItemSlot.SlotManager.LegR
			};
			int index = 0;
			PrintLine(line);
			foreach (ItemSlot item in list)
			{
				Item firstItemAtSlot = GameVariables.Current.itemManager.GetFirstItemAtSlot(item.Id);
				AppendItemData(line.StringBuilder, item, firstItemAtSlot);
				itemsInNormalSlots.Remove(firstItemAtSlot);
				index++;
				UpdateDisplay();
				await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(0.0025f, 0.1f)), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			line.StringBuilder.AppendLine();
			line.StringBuilder.AppendLine(LOC.UI.StatusApp_OtherModules);
			bool any = false;
			foreach (Item item2 in itemsInNormalSlots)
			{
				if (item2.GetItemPrefab().Category == ItemCategory.Modules)
				{
					AppendItemData(line.StringBuilder, ItemSlot.SlotManager.GetSlot(item2.EquippedSlot), item2);
					index++;
					any = true;
					await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(0.0025f, 0.1f)), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
				}
			}
			if (!any)
			{
				line.StringBuilder.AppendLine(LOC.UI.StatusApp_NoOtherModulesFound);
			}
			await PrintLineDelay(LOC.UI.StatusApp_CompatiblityCheck, 0.0025f, 0.0025f, cancellationToken);
			await FakeLoading(UnityEngine.Random.Range(0.1f, 0.25f), 20, hideAfterDone: true, cancellationToken);
			await PrintLineDelay(LOC.UI.StatusApp_NoCompatiblityIssuesFound, 0.0025f, 0.0025f, cancellationToken);
			void AppendItemData(StringBuilder stringBuilder, ItemSlot slot, Item item)
			{
				if (item != null)
				{
					stringBuilder.AppendLine($"0x{index:X8} \"{slot.FriendlyName}\" = {item.GetName(ItemNameIncludeEnum.Raw),15}");
					ItemPrefab itemPrefab = item.GetItemPrefab();
					if (itemPrefab.HasQuality)
					{
						stringBuilder.Append(LOC.UI.StatusApp_ApproximateCondition);
						stringBuilder.AppendLine($"\t{item.NormalizedQuality:P2}");
					}
					ColorSlot[] colorSlots = itemPrefab.ColorSlots;
					if (colorSlots != null && colorSlots.Length > 0)
					{
						for (int i = 0; i < itemPrefab.ColorSlots.Length; i++)
						{
							ColorSlot colorSlot = itemPrefab.ColorSlots[i];
							string text = ColorUtility.ToHtmlStringRGB(item.GetColor(i));
							stringBuilder.AppendLine("\t" + colorSlot.Name + " = <color=#" + text + ">#" + text + "</color>");
						}
					}
				}
				else
				{
					stringBuilder.AppendLine($"0x{index:X8} \"{slot.FriendlyName}\" = <color=#FF0000>{LOC.UI.EquipmentCommand_ItemMissing,15}</color>");
				}
				stringBuilder.AppendLine();
			}
		}
	}

	private async UniTask Bootup3(CancellationToken cancellationToken)
	{
		await PrintLineDelay(LOC.UI.StatusApp_FullConnetionEstablished, 0.0025f, 0.0025f, cancellationToken);
		await PrintLineDelay(LOC.UI.StatusApp_ConsoleWidth(WidthCharCount), 0.0025f, 0.0025f, cancellationToken);
		await PrintLineDelay(LOC.UI.StatusApp_BeginningHardwareCheck, 0.0025f, 0.0025f, cancellationToken);
		await ShowEquipment(cancellationToken);
	}

	public void RaiseStopFlag()
	{
		stopFlagRaised = true;
	}

	private async UniTask DefaultPlayerConsole(CancellationToken cancellationToken)
	{
		await PrintLineDelay("******************", 0.0025f, 0.0025f, cancellationToken);
		await PrintLineDelay("***JunOS 69.420***", 0.0025f, 0.0025f, cancellationToken);
		await PrintLineDelay("******************", 0.0025f, 0.0025f, cancellationToken);
		await PrintLineDelay(LOC.UI.CurrentDateInfo(GameVariables.Current.GameDateTime), 0.0025f, 0.0025f, cancellationToken);
		await PrintLineDelay(LOC.UI.StatusApp_HelloMessage("help"), 0.0025f, 0.0025f, cancellationToken);
		while (!stopFlagRaised)
		{
			PrintLine("");
			UpdateDisplay();
			string text = await GetPlayerInput(saveInMemory: true, cancellationToken);
			StringUtils.ParsedCommandData playerInputParsed = StringUtils.ParseAsCommand(text);
			if (playerInputParsed.IsEmpty)
			{
				await PrintLineDelay(LOC.UI.CommandInputNothing.TMPColorSurround(Color.red), 0.0001f, 0.0025f, cancellationToken);
				continue;
			}
			await PrintLineDelay(">" + text, 0.0025f, 0.0025f, cancellationToken);
			if (!CommandsPrefabManager.TryGetCommand(playerInputParsed.Command, out var found) || !found.IsAllowed)
			{
				await PrintLineDelay(LOC.UI.CommandNotFound(playerInputParsed.Command).TMPColorSurround(Color.red), 0.0025f, 0.0025f, cancellationToken);
				continue;
			}
			try
			{
				await found.Start(this, playerInputParsed, cancellationToken);
			}
			catch (CommandsPrefabManager.AppCommandParsingException ex)
			{
				await PrintLineDelay(ex.Message.TMPColorSurround(Color.red), 0.0025f, 0.0025f, cancellationToken);
			}
		}
	}

	private async UniTask MainBootUpDefault(CancellationToken cancellationToken)
	{
		await FileRead(R.OtherText.bootup1.Get().text, cancellationToken);
		await ClearScreenDelay(0.0025f, 0.0025f, cancellationToken);
		await FileRead(R.OtherText.bootup2.Get().text, cancellationToken);
		await ClearScreenDelay(0.0025f, 0.0025f, cancellationToken);
		await Bootup3(cancellationToken);
		await ClearScreenDelay(0.0025f, 0.0025f, cancellationToken);
		_title.text = LOC.UI.StatusApp_ConnectedTo + "(69.69.69.69)";
		if (!GameVariables.Current.CheckFlag("console1"))
		{
			GameVariables.Current.AddFlag("console1");
			await FirstTimeOpened();
		}
		await DefaultPlayerConsole(cancellationToken);
		await PrintLineDelay(LOC.UI.StatusAppDisconnect, 0.0025f, 0.0025f, cancellationToken);
		await FakeLoading(0.5f, 20, hideAfterDone: true, cancellationToken);
		CloseFromUIOverlay();
	}

	private async UniTask FirstTimeOpened()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.SetStringVariable("Text", "C:\\system\\debug\\reports\\report_3.txt");
		await DoFungusBlock(FungusBlocks.Console_First);
	}

	public async UniTask DoFungusBlock(FungusBlocks.FungusBlockReference fungusBlockReference)
	{
		UniTaskCompletionSource completion = new UniTaskCompletionSource();
		UiOverlay.Instance.OpenFungusOverlay(Color.clear);
		SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartBlock(fungusBlockReference.Name, () =>
		{
			completion.TrySetResult();
		});
		await completion.Task;
		UiOverlay.Instance.CloseFungusOverlay();
	}

	public void AddCharToInput(char c)
	{
		_inputField.text += c;
	}

	public void SetPlayerInput(string t)
	{
		_inputField.text += t;
	}

	[UsedInLua]
	public void ShowFile(string text)
	{
		if (!(text == "boobs"))
		{
			if (text == "mouth")
			{
				PrintLine(R.OtherText.junMouth.Get().text);
				UpdateDisplay();
			}
			else
			{
				Debug.LogError("Tried to show file \"" + text + "\"! It doesn't exist");
			}
		}
		else
		{
			PrintLine(R.OtherText.junBoobs.Get().text);
			UpdateDisplay();
		}
	}

	[UsedInLua]
	public void ForceSubmitPlayerInput(string input)
	{
		_inputField.text = input;
		_inputField.onSubmit?.Invoke(input);
	}

	[UsedInLua]
	public IEnumerator WaitTillPlayerInputNeeded()
	{
		yield return null;
		yield return new WaitUntil(() => playerInputAllowed);
		yield return null;
	}

	private async UniTask MainBootUpPrologue(CancellationToken cancellationToken)
	{
		for (int i = 0; i < 69; i++)
		{
			await PrintLineDelay($"<color=red>0x{UnityEngine.Random.Range(4096, 65535):X4} ERR{UnityEngine.Random.Range(100, 999)}: {UnityEngine.Random.Range(4096, 65535):X4}{UnityEngine.Random.Range(4096, 65535):X4}{UnityEngine.Random.Range(4096, 65535):X4}</color>", 0.0025f, 0.0025f, cancellationToken);
		}
		await GetPlayerInput(saveInMemory: true, cancellationToken);
		await ClearScreenDelay(0.0025f, 0.0025f, cancellationToken);
		_title.text = LOC.UI.StatusApp_ConnectedTo + "(69.69.69.69)";
		await ShowEquipment(cancellationToken);
		await PrintLineDelay(LOC.UI.StatusApp_Ram + ": 1TB", 0.0025f, 0.0025f, cancellationToken);
		await PrintLineDelay(LOC.UI.StatusApp_CPU + ": 华龙 X1000 龙核9X", 0.0025f, 0.0025f, cancellationToken);
		await PrintLineDelay(LOC.UI.StatusApp_BIOS + ": MP45 (" + LOC.UI.StatusApp_BiosModified + " )", 0.0025f, 0.0025f, cancellationToken);
		await PrintLineDelay(LOC.UI.StatusApp_Memory + ": [" + LOC.UI.UI_Error + "]", 0.0025f, 0.0025f, cancellationToken);
		await GetPlayerInput(saveInMemory: true, cancellationToken);
		await ClearScreenDelay(0.0025f, 0.0025f, cancellationToken);
		for (int i = 0; i < 100; i++)
		{
			await PrintLineDelay($"<color=red>0x{UnityEngine.Random.Range(4096, 65535):X4} ERR{UnityEngine.Random.Range(100, 999)}</color>", 0.0025f, 0.0025f, cancellationToken);
			if (UnityEngine.Random.value < 0.1f)
			{
				await PrintLineDelay("Status critical attempting repairs", 0.0025f, 0.0025f, cancellationToken);
				await FakeLoading(0.1f, 20, hideAfterDone: true, cancellationToken);
				await WaitRandomLongDelay(cancellationToken);
			}
		}
		await ClearScreenDelay(0.0025f, 0.0025f, cancellationToken);
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			await PrintLineDelay(stringBuilder, 0.0025f, 0.0025f, cancellationToken);
			stringBuilder.Append("<color=red>");
			stringBuilder.AppendLine(StatusAppUtils.Base64Encode("Status is not available at the moment. Critical failure of the disc C, critical failure of the disc D, critical failure of the disc E, critical failure of the disc F."));
			await FakeLoading(0.2f, 20, hideAfterDone: true, cancellationToken);
			stringBuilder.AppendLine(StatusAppUtils.Base64Encode("Initializing integrity check of disc C, Initializing integrity check of disc D, Initializing integrity check of disc E, Initializing integrity check of disc F"));
			await FakeLoading(0.1f, 20, hideAfterDone: true, cancellationToken);
			stringBuilder.AppendLine(StatusAppUtils.Base64Encode("Error! System is unable to initialize the integrity check of the disc F. Data corrupted. Cause: Unknown."));
			await FakeLoading(0.1f, 20, hideAfterDone: true, cancellationToken);
			stringBuilder.AppendLine(StatusAppUtils.Base64Encode("Immediate action is required! Should system delete the corrupted files or attempt to use its processing power to fix them?"));
			await FakeLoading(0.1f, 20, hideAfterDone: true, cancellationToken);
		}
	}

	public void Update()
	{
		if (_canvasGroup.interactable && _canvasGroup.blocksRaycasts && playerInputAllowed && Input.anyKeyDown && EventSystem.current.currentSelectedGameObject != _inputField.gameObject && !string.IsNullOrEmpty(Input.inputString))
		{
			_inputField.text += Input.inputString;
			_inputField.Select();
			_inputField.MoveTextEnd(shift: false);
			TMP_InputField inputField = _inputField;
			int selectionAnchorPosition = (_inputField.selectionFocusPosition = _inputField.caretPosition);
			inputField.selectionAnchorPosition = selectionAnchorPosition;
			_inputField.ActivateInputField();
			_scrollRect.ScrollToBottom();
		}
	}

	public void Open(bool isPrologue)
	{
		_canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
		LoadConsoleLook();
		base.Open();
		CalculateConsoleSizeValues();
		_inputField.gameObject.SetActive(value: false);
		StringBuilderCache.AcquireManual(out _consoleStringBuilder);
		_mainTokenSource = new CancellationTokenSource();
		_title.text = LOC.UI.StatusApp_ConnectingTo("69.69.69.69");
		if (isPrologue)
		{
			MainBootUpPrologue(_mainTokenSource.Token).Forget();
		}
		else
		{
			MainBootUpDefault(_mainTokenSource.Token).Forget();
		}
	}

	private void StopTask()
	{
		if (_mainTokenSource != null)
		{
			_mainTokenSource.Cancel();
			_mainTokenSource.Dispose();
			_mainTokenSource = null;
		}
	}

	public override bool CloseInner()
	{
		StopTask();
		StringBuilderCache.Release(_consoleStringBuilder);
		_consoleStringBuilder = null;
		return base.CloseInner();
	}
}
