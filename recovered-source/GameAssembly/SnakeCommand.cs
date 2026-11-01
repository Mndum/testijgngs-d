using System.Collections.Generic;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SnakeCommand : AppCommandWithFlags
{
	private class SnakeCommandGame
	{
		private const char LeftInput = 'a';

		private const char RightInput = 'd';

		private const char UpInput = 'w';

		private const char DownInput = 's';

		private const char ExitInput = 'l';

		private const char SnakeHeadChar = '@';

		private const char SnakeHorizontalChar = '|';

		private const char SnakeBend1Char = '\\';

		private const char SnakeBend2Char = '/';

		private const char SnakeEndChar = 'o';

		private const char SnakeVerticalChar = '-';

		private const char FoodChar = '*';

		private const char EmptyChar = '.';

		private int _score;

		private int _width;

		private int _height;

		private int _snakeLength;

		private Vector2Int _foodPosition;

		private List<Vector2Int> _snake;

		private Vector2Int _direction;

		private char[] board;

		private BotStatusAppPopup.StringConsoleLine _snakeHeader;

		private BotStatusAppPopup.StringConsoleLine _snakeFooter;

		public float Progress01 => (float)_snakeLength / (float)(_width * _height);

		public SnakeCommandGame(int width, int height)
		{
			_width = width;
			_height = height;
			board = new char[_width * _height];
			_snakeLength = 1;
			_direction = Vector2Int.right;
			_snake = new List<Vector2Int>();
			_snake.Add(new Vector2Int(_width / 2, _height / 2));
			TryPlaceFood();
		}

		public async UniTask StartGame(BotStatusAppPopup popup, CancellationToken cancellationToken)
		{
			_snakeHeader = popup.GetStringConsoleLine(LOC.UI.SnakeCommandHeader);
			_snakeHeader.StringPrintType = BotStatusAppPopup.StringConsoleLine.StringPrintTypesEnum.DecorativeWrapCenter;
			_snakeHeader.MaxVerticalLinesAllowed = 4;
			BotStatusAppPopup.CharArrayConsoleLine charArrayLine = new BotStatusAppPopup.CharArrayConsoleLine(board, _width, _height, BotStatusAppPopup.CharArrayConsoleLine.CharArrayPrintTypesEnum.Center);
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.Append(LOC.Polyglot.MENU_OPTIONS_CONTROLS);
				stringBuilder.Append(" : ");
				stringBuilder.Append($"{'w'} = {LOC.Polyglot.MENU_DIR_UP}, ");
				stringBuilder.Append($"{'s'} = {LOC.Polyglot.MENU_DIR_DOWN}, ");
				stringBuilder.Append($"{'a'} = {LOC.Polyglot.MENU_DIR_LEFT}, ");
				stringBuilder.Append($"{'d'} = {LOC.Polyglot.MENU_DIR_RIGHT}, ");
				stringBuilder.Append($"{'l'} = {LOC.Polyglot.MENU_LABEL_EXIT}");
				_snakeFooter = popup.GetStringConsoleLine(stringBuilder.ToString());
				_snakeFooter.StringPrintType = BotStatusAppPopup.StringConsoleLine.StringPrintTypesEnum.DecorativeWrapCenter;
				_snakeFooter.MaxVerticalLinesAllowed = 3;
			}
			UpdateScoreDisplay();
			UpdateBoard();
			await popup.PrintLineDelay(_snakeHeader, 0.0025f, 0.0025f, cancellationToken);
			await popup.PrintLineDelay(charArrayLine, 0.0025f, 0.0025f, cancellationToken);
			await popup.PrintLineDelay(_snakeFooter, 0.0025f, 0.0025f, cancellationToken);
			bool keepGoing = true;
			popup.StartPlayerInput();
			while (!cancellationToken.IsCancellationRequested)
			{
				if (popup.TryEatPlayerInput(out var str))
				{
					string text = str;
					for (int i = 0; i < text.Length; i++)
					{
						switch (text[i])
						{
						case 'w':
							SetDirectionIfPossible(Vector2Int.down);
							break;
						case 's':
							SetDirectionIfPossible(Vector2Int.up);
							break;
						case 'a':
							SetDirectionIfPossible(Vector2Int.left);
							break;
						case 'd':
							SetDirectionIfPossible(Vector2Int.right);
							break;
						case 'l':
							keepGoing = false;
							break;
						}
					}
				}
				MoveSnake();
				if (CheckCollision() || !keepGoing)
				{
					UpdateBoard();
					await popup.PrintLineDelay(LOC.Polyglot.GAME_OBJECTIVE_GAMEOVER_CAPS, 0.0025f, 0.0025f, cancellationToken);
					return;
				}
				if (_snake[0] == _foodPosition)
				{
					_snakeLength++;
					_score += _snakeLength;
					UpdateScoreDisplay();
					if (_snakeLength == _width * _height || !TryPlaceFood())
					{
						UpdateBoard();
						await popup.PrintLineDelay(LOC.Polyglot.GAME_STATUS_YOU_WIN, 0.0025f, 0.0025f, cancellationToken);
						return;
					}
				}
				UpdateBoard();
				popup.UpdateDisplay();
				float num = Mathf.Lerp(0.1f, 0.05f, Progress01);
				await UniTask.WaitForSeconds((_direction.y == 0) ? num : (num * popup.CharInverseAspectRatio), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			popup.EndPlayerInput();
		}

		private void SetDirectionIfPossible(Vector2Int newDirection)
		{
			if (_snake.Count <= 1 || Loop(_snake[0] + newDirection) != _snake[1])
			{
				_direction = newDirection;
			}
		}

		private Vector2Int Loop(Vector2Int value)
		{
			if (value.x >= _width)
			{
				value.x = 0;
			}
			if (value.x < 0)
			{
				value.x = _width - 1;
			}
			if (value.y >= _height)
			{
				value.y = 0;
			}
			if (value.y < 0)
			{
				value.y = _height - 1;
			}
			return value;
		}

		private void MoveSnake()
		{
			Vector2Int vector2Int = _snake[0];
			Vector2Int item = Loop(vector2Int + _direction);
			_snake.Insert(0, item);
			_snake.EnsureListSizeIsAtMost(_snakeLength);
		}

		private bool CheckCollision()
		{
			Vector2Int vector2Int = _snake[0];
			for (int i = 1; i < _snake.Count; i++)
			{
				if (_snake[i] == vector2Int)
				{
					return true;
				}
			}
			return false;
		}

		private bool TryPlaceFood()
		{
			for (int i = 0; i < 50; i++)
			{
				_foodPosition = new Vector2Int(Random.Range(0, _width), Random.Range(0, _height));
				if (!_snake.Contains(_foodPosition))
				{
					return true;
				}
			}
			for (int j = 0; j < _width; j++)
			{
				for (int k = 0; k < _height; k++)
				{
					_foodPosition = new Vector2Int(j, k);
					if (!_snake.Contains(_foodPosition))
					{
						return true;
					}
				}
			}
			return false;
		}

		private int PositionToBoard(Vector2Int position)
		{
			return position.x + position.y * _width;
		}

		private void UpdateScoreDisplay()
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.AppendLine(LOC.UI.SnakeCommandHeader);
				stringBuilder.AppendLine(LOC.UI.SnakeCommandScore(_score, _snakeLength));
				_snakeHeader.String = stringBuilder.ToString();
			}
		}

		private void UpdateBoard()
		{
			for (int i = 0; i < board.Length; i++)
			{
				board[i] = '.';
			}
			int count = _snake.Count;
			if (_snake.Count > 1)
			{
				board[PositionToBoard(_snake[_snake.Count - 1])] = 'o';
			}
			for (int j = 1; j < count - 1; j++)
			{
				Vector2Int vector2Int = _snake[j - 1];
				Vector2Int position = _snake[j];
				Vector2Int vector2Int2 = _snake[j + 1];
				int num = PositionToBoard(position);
				if (vector2Int.x == position.x && position.x == vector2Int2.x)
				{
					board[num] = '|';
					continue;
				}
				if (vector2Int.y == position.y && position.y == vector2Int2.y)
				{
					board[num] = '-';
					continue;
				}
				Vector2Int vector2Int3 = vector2Int2 - vector2Int;
				if (vector2Int3.x + vector2Int3.y == 0)
				{
					board[num] = '/';
				}
				else
				{
					board[num] = '\\';
				}
			}
			board[PositionToBoard(_snake[0])] = '@';
			board[PositionToBoard(_foodPosition)] = '*';
		}
	}

	private const int SnakeHeaderHeight = 4;

	private const int SnakeFooterHeight = 3;

	public override string CommandName => "snake";

	public override string CommandDescription => LOC.UI.SnakeCommandDescription;

	protected override List<CommandFlagInfo> GetDocumentedFlagsWithDescriptions()
	{
		return new List<CommandFlagInfo>();
	}

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		int width = 20;
		int height = 10;
		Result result = TryGetArgumentAtIndexAsNumber(input, 0, out var number, 3, popup.WidthCharCount);
		ThrowIfResultWithDescription(result);
		if (result.IsSuccess)
		{
			width = number;
		}
		Result result2 = TryGetArgumentAtIndexAsNumber(input, 1, out var number2, 3, popup.HeightCharCount - 4 - 3);
		ThrowIfResultWithDescription(result2);
		if (result2.IsSuccess)
		{
			height = number2;
		}
		await new SnakeCommandGame(width, height).StartGame(popup, cancellationToken);
	}
}
