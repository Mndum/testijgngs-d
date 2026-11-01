using System.Collections.Generic;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StyleCommand : AppCommand
{
	private const char nextTextCharInput = 't';

	private const char nextBackgroundCharInput = 'b';

	private const char randomCharInput = 'r';

	private const char defaultCharInput = 'd';

	private const char exitCharInput = 'l';

	private BotStatusAppPopup.StringConsoleLine _content;

	private BotStatusAppPopup _popup;

	private List<Color32> _colors = new List<Color32>
	{
		new Color32(0, 255, 0, 255),
		new Color32(0, 0, 0, 255),
		new Color32(255, 0, 0, 255),
		new Color32(0, 0, 128, 255),
		new Color32(0, 128, 0, 255),
		new Color32(0, 128, 128, 255),
		new Color32(28, 0, 0, 255),
		new Color32(128, 0, 128, 255),
		new Color32(128, 128, 0, 255),
		new Color32(192, 192, 192, 255),
		new Color32(128, 128, 128, 255),
		new Color32(0, 0, 255, 255),
		new Color32(0, 255, 255, 255),
		new Color32(255, 0, 0, 255),
		new Color32(255, 0, 255, 255),
		new Color32(255, 255, 0, 255),
		new Color32(255, 255, 255, 255),
		new Color32(69, 69, 255, 255),
		ColorPaletteManager.CuteMagenta,
		ColorPaletteManager.Miku,
		ColorPaletteManager.JunsGrey,
		ColorPaletteManager.JunsFlesh,
		ColorPaletteManager.JunsPupils,
		ColorPaletteManager.FakeGold,
		ColorPaletteManager.DiabeteSweetPotatoBrown,
		ColorPaletteManager.Amethyst,
		new Color32(255, 165, 0, 255),
		new Color32(0, 255, 0, 255),
		new Color32(255, 192, 203, 255),
		new Color32(75, 0, 130, 255),
		new Color32(255, 105, 180, 255),
		new Color32(255, 69, 0, 255),
		new Color32(139, 69, 19, 255),
		new Color32(255, 215, 0, 255),
		new Color32(60, 179, 113, 255),
		new Color32(0, 191, 255, 255)
	};

	private int _currentTextColorIndex;

	private int _currentBackgroundColorIndex;

	public override string CommandName => "style";

	public override string CommandDescription => LOC.UI.StyleCommandDescription;

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		_popup = popup;
		_content = popup.GetStringConsoleLine("");
		_content.StringPrintType = BotStatusAppPopup.StringConsoleLine.StringPrintTypesEnum.DecorativeWrapCenter;
		popup.PrintLine(_content);
		InitIndexes();
		UpdateShownInfo();
		popup.UpdateDisplay();
		popup.StartPlayerInput();
		bool keepGoing = true;
		while (!cancellationToken.IsCancellationRequested & keepGoing)
		{
			if (popup.TryEatPlayerInput(out var str))
			{
				string text = str;
				for (int i = 0; i < text.Length; i++)
				{
					switch (text[i])
					{
					case 't':
						NextTextColor();
						UpdateConsoleColors();
						UpdateShownInfo();
						popup.UpdateDisplay();
						break;
					case 'b':
						NextBackgroundColor();
						UpdateConsoleColors();
						UpdateShownInfo();
						popup.UpdateDisplay();
						break;
					case 'd':
						_currentTextColorIndex = 0;
						_currentBackgroundColorIndex = 1;
						UpdateConsoleColors();
						UpdateShownInfo();
						popup.UpdateDisplay();
						break;
					case 'r':
						_currentBackgroundColorIndex = Random.Range(0, _colors.Count);
						_currentTextColorIndex = Random.Range(0, _colors.Count);
						NextTextColor();
						UpdateConsoleColors();
						UpdateShownInfo();
						popup.UpdateDisplay();
						break;
					case 'l':
						keepGoing = false;
						break;
					}
				}
			}
			await UniTask.Delay(50, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		}
		popup.EndPlayerInput();
		await popup.PrintLineDelay(LOC.UI.StyleCommandSaving, 0.0025f, 0.0025f, cancellationToken);
		await popup.FakeLoading(Random.Range(0.25f, 1f), 20, hideAfterDone: true, cancellationToken);
		await popup.PrintLineDelay(LOC.UI.StyleCommandSavingDone, 0.0025f, 0.0025f, cancellationToken);
		popup.RemoveLine(_content);
		popup.PrintLine(LOC.UI.CommandClosed(CommandName));
		popup.UpdateDisplay();
	}

	private void UpdateShownInfo()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(LOC.UI.StyleCommandHeader);
			stringBuilder.AppendLine(LOC.UI.StyleCommandInfo);
			stringBuilder.AppendLine($"{'t'} => {LOC.UI.StyleCommandNextTextColor}");
			stringBuilder.AppendLine($"{'b'} => {LOC.UI.StyleCommandNextBackgroundColor}");
			stringBuilder.AppendLine($"{'d'} => {LOC.UI.StyleCommandDefaultColors}");
			stringBuilder.AppendLine($"{'r'} => {LOC.UI.StyleCommandRandomColors}");
			stringBuilder.AppendLine($"{'l'} => {LOC.Polyglot.MENU_LABEL_EXIT}");
			_content.String = stringBuilder.ToString();
		}
	}

	private void FindOrSetColorInList(out int index, List<Color32> colors, Color32 currentColor)
	{
		index = _colors.IndexOf(currentColor);
		if (index < 0)
		{
			colors[colors.Count - 1] = currentColor;
			index = colors.Count - 1;
		}
	}

	private void NextColorWithEnoughContrast(ref int index, List<Color32> colors, Color32 toContrastWith)
	{
		index %= colors.Count;
		for (int i = index + 1; i < colors.Count; i++)
		{
			if (toContrastWith.HasEnoughContrast(colors[i]))
			{
				index = i;
				return;
			}
		}
		for (int j = 0; j < index; j++)
		{
			if (toContrastWith.HasEnoughContrast(colors[j]))
			{
				index = j;
				return;
			}
		}
		index = (index + 1) % colors.Count;
	}

	private void InitIndexes()
	{
		ConsoleStyleStruct consoleStyle = _popup.GetConsoleStyle();
		FindOrSetColorInList(out _currentTextColorIndex, _colors, consoleStyle.TextColor);
		FindOrSetColorInList(out _currentBackgroundColorIndex, _colors, consoleStyle.BackgroundColor);
	}

	private void NextTextColor()
	{
		NextColorWithEnoughContrast(ref _currentTextColorIndex, _colors, _colors[_currentBackgroundColorIndex]);
	}

	private void NextBackgroundColor()
	{
		NextColorWithEnoughContrast(ref _currentBackgroundColorIndex, _colors, _colors[_currentTextColorIndex]);
	}

	private void UpdateConsoleColors()
	{
		ConsoleStyleStruct consoleStyle = _popup.GetConsoleStyle();
		consoleStyle.TextColor = _colors[_currentTextColorIndex];
		consoleStyle.BackgroundColor = _colors[_currentBackgroundColorIndex];
		_popup.SetConsoleStyle(consoleStyle);
	}
}
