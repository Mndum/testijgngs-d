using System;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CubeCommand : AppCommand
{
	private class CubeCommandLogic
	{
		public const int FooterHeight = 4;

		private const char NextSDFInput = 'n';

		private const char NextGradientInput = 'g';

		private const char CustomGradientInput = 'c';

		private const char EmptyChar = ' ';

		private int _width;

		private int _height;

		private float _charAspectRatio;

		private float _x = 90f;

		private float _y;

		private float _z;

		private Quaternion itemRotation;

		private char[] board;

		private BotStatusAppPopup.CharArrayConsoleLine _cubeDisplay;

		private BotStatusAppPopup.StringConsoleLine _footer;

		private const float MaxDistance = 10f;

		private const float Epsilon = 0.001f;

		private Func<Vector3, float> currentSDF;

		private int sdfIndex;

		private const int sdfCount = 5;

		private string currentGradient;

		private int gradientIndex;

		public CubeCommandLogic(int width, int height, float charAspectRatio)
		{
			_width = width;
			_height = height;
			_charAspectRatio = charAspectRatio;
			board = new char[_width * _height];
			SetSDF();
			SetGradient();
		}

		private void UpdateFooter()
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.Append(LOC.Polyglot.MENU_OPTIONS_CONTROLS);
				stringBuilder.Append(" : ");
				stringBuilder.Append($"{'n'} = {LOC.Polyglot.MENU_LABEL_NEXT} ({sdfIndex + 1}/{5}), ");
				if (gradientIndex >= 0)
				{
					stringBuilder.Append($"{'g'} = {LOC.Polyglot.MENU_LABEL_GRAPHICS} ({gradientIndex + 1}/{AsciiRenderingUtils.GradientCount}), ");
				}
				else
				{
					stringBuilder.Append($"{'g'} = {LOC.Polyglot.MENU_LABEL_GRAPHICS} ({LOC.UI.CubeCommandGradientCustom}), ");
				}
				stringBuilder.Append($"{'c'} = {LOC.UI.CubeCommandGradientCustom}, ");
				stringBuilder.Append($"{'l'} = {LOC.Polyglot.MENU_LABEL_EXIT}");
				_footer.String = stringBuilder.ToString();
			}
		}

		public async UniTask Start(BotStatusAppPopup popup, CancellationToken cancellationToken)
		{
			_cubeDisplay = new BotStatusAppPopup.CharArrayConsoleLine(board, _width, _height, BotStatusAppPopup.CharArrayConsoleLine.CharArrayPrintTypesEnum.Center);
			_footer = popup.GetStringConsoleLine("");
			_footer.StringPrintType = BotStatusAppPopup.StringConsoleLine.StringPrintTypesEnum.DecorativeWrapCenter;
			_footer.MaxVerticalLinesAllowed = 4;
			UpdateFooter();
			popup.PrintLine(_cubeDisplay);
			popup.PrintLine(_footer);
			bool keepGoing = true;
			popup.StartPlayerInput();
			while (!cancellationToken.IsCancellationRequested & keepGoing)
			{
				if (popup.TryEatPlayerInput(out var str))
				{
					string text = str;
					for (int i = 0; i < text.Length; i++)
					{
						switch (text[i])
						{
						case 'n':
							NextSDF();
							UpdateFooter();
							break;
						case 'g':
							NextGradient();
							UpdateFooter();
							break;
						case 'c':
							popup.EndPlayerInput();
							var (flag, text2) = await AsciiRenderingUtils.GetCustomGradient(popup, cancellationToken);
							if (flag)
							{
								gradientIndex = -1;
								currentGradient = text2;
								UpdateFooter();
							}
							popup.StartPlayerInput();
							break;
						case 'l':
							keepGoing = false;
							break;
						}
					}
				}
				_x++;
				_y += 2f;
				_z += 3f;
				itemRotation = Quaternion.Euler(_x, _y, _z);
				UpdateBoard();
				popup.UpdateDisplay();
				await UniTask.Delay(50, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			}
			popup.EndPlayerInput();
			popup.RemoveLine(_cubeDisplay);
			popup.RemoveLine(_footer);
		}

		private void UpdateBoard()
		{
			Vector3 normalized = new Vector3(1f, 1f, 1f).normalized;
			Vector3 vector = new Vector3(0f, 0f, -2f);
			for (int i = 0; i < _height; i++)
			{
				for (int j = 0; j < _width; j++)
				{
					Vector3 vector2 = vector;
					vector2.x = (float)j / (float)_width * 2f - 1f;
					vector2.y = ((float)i / (float)_height * 2f - 1f) / _charAspectRatio;
					vector2.z = vector.z + 1f;
					Vector3 normalized2 = (vector2 - vector).normalized;
					if (TryRayMarch(vector2, normalized2, out var totalDistance, out var normal, out var _))
					{
						float brightness = Mathf.Max(Vector3.Dot(normalized, normal), 0f) * Mathf.LerpUnclamped(1f, 0.5f, totalDistance / 10f);
						board[j + i * _width] = AsciiRenderingUtils.GetShadeCharacter(currentGradient, brightness);
					}
					else
					{
						board[j + i * _width] = ' ';
					}
				}
			}
		}

		private bool TryRayMarch(Vector3 rayOrigin, Vector3 rayDir, out float totalDistance, out Vector3 normal, out Vector3 finalRayPos)
		{
			normal = Vector3.up;
			totalDistance = 0f;
			finalRayPos = rayOrigin;
			for (int i = 0; i < 100; i++)
			{
				float currentSdf = GetCurrentSdf(finalRayPos);
				if (currentSdf < 0.01f)
				{
					normal = new Vector3(currentSdf - GetCurrentSdf(finalRayPos + new Vector3(0.001f, 0f, 0f)), currentSdf - GetCurrentSdf(finalRayPos + new Vector3(0f, 0.001f, 0f)), currentSdf - GetCurrentSdf(finalRayPos + new Vector3(0f, 0f, 0.001f))).normalized;
					return true;
				}
				totalDistance += currentSdf;
				finalRayPos = rayOrigin + totalDistance * rayDir;
				if (totalDistance > 10f)
				{
					totalDistance = 10f;
					return false;
				}
			}
			return false;
		}

		private float GetCurrentSdf(Vector3 position)
		{
			return currentSDF(itemRotation * position);
		}

		private void NextSDF()
		{
			sdfIndex = (sdfIndex + 1) % 5;
			SetSDF();
		}

		private void SetSDF()
		{
			switch (sdfIndex)
			{
			default:
				currentSDF = SDFBox;
				break;
			case 1:
				currentSDF = SDFDonut;
				break;
			case 2:
				currentSDF = SDFSphere;
				break;
			case 3:
				currentSDF = SDFCylinder;
				break;
			case 4:
				currentSDF = SDFCone;
				break;
			}
		}

		private Vector3 Abs(Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		private float SDFSphere(Vector3 p)
		{
			float num = 0.6f;
			return p.magnitude - num;
		}

		private float SDFBox(Vector3 p)
		{
			Vector3 vector = new Vector3(0.5f, 0.5f, 0.5f);
			Vector3 vector2 = Vector3.Max(Abs(p) - vector, Vector3.zero);
			return vector2.magnitude + Mathf.Min(Mathf.Max(vector2.x, Mathf.Max(vector2.y, vector2.z)), 0f);
		}

		private float SDFDonut(Vector3 p)
		{
			Vector3 vector = new Vector2(0.45f, 0.34f);
			Vector2 vector2 = new Vector2(new Vector2(p.x, p.z).magnitude - vector.x, p.y);
			return vector2.magnitude - vector.y;
		}

		private float SDFCylinder(Vector3 p)
		{
			float num = 0.5f;
			float num2 = 1f;
			Vector2 vector = new Vector2(new Vector2(p.x, p.z).magnitude - num, Mathf.Abs(p.y) - num2 * 0.5f);
			return Mathf.Max(vector.x, vector.y);
		}

		private float SDFCone(Vector3 p)
		{
			Vector2 lhs = new Vector2(0.5f, 0.5f);
			float num = 0.25f;
			float magnitude = new Vector2(p.x, p.z).magnitude;
			return Mathf.Max(Vector2.Dot(lhs, new Vector2(magnitude, p.y)) - num, 0f - num - p.y);
		}

		private void NextGradient()
		{
			AsciiRenderingUtils.NextGradient(ref gradientIndex);
			SetGradient();
		}

		private void SetGradient()
		{
			currentGradient = AsciiRenderingUtils.GetGradient(ref gradientIndex);
		}
	}

	public override string CommandName => "cube";

	public override string CommandDescription => LOC.UI.CubeCommandDescription;

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		int width = popup.WidthCharCount;
		int height = popup.HeightCharCount - 4;
		Result result = TryGetArgumentAtIndexAsNumber(input, 0, out var number, 5, popup.WidthCharCount);
		ThrowIfResultWithDescription(result);
		if (result.IsSuccess)
		{
			width = number;
		}
		Result result2 = TryGetArgumentAtIndexAsNumber(input, 0, out var number2, 5, popup.HeightCharCount - 4);
		ThrowIfResultWithDescription(result2);
		if (result2.IsSuccess)
		{
			width = number2;
		}
		await new CubeCommandLogic(width, height, popup.CharInverseAspectRatio).Start(popup, cancellationToken);
		popup.PrintLine(LOC.UI.CommandClosed(CommandName));
	}
}
