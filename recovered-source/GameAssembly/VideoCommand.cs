using System;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

public class VideoCommand : AppCommand
{
	private class VideoCommandLogic
	{
		private const char backMoreChar = 'a';

		private const char backChar = 's';

		private const char forwardChar = 'd';

		private const char forwardMoreChar = 'f';

		private const char loopChar = 'r';

		private const char gradientChar = 'g';

		private const char customGradientChar = 'c';

		public const int FooterHeight = 6;

		private int _width;

		private int _height;

		private string _videoPath;

		private char[] board;

		private VideoPlayer _videoPlayer;

		private RenderTexture _renderTexture;

		private RenderTexture _renderTextureFlipped;

		private Texture2D _scaledTexture;

		private BotStatusAppPopup.ConsoleLine _loadingHeader;

		private BotStatusAppPopup.CharArrayConsoleLine _videoDisplay;

		private BotStatusAppPopup.CharArrayConsoleLine _progressBar;

		private BotStatusAppPopup.StringConsoleLine _footer;

		private string currentGradient;

		private int gradientIndex;

		public VideoCommandLogic(string videoPath)
		{
			_videoPath = videoPath;
		}

		public async UniTask Start(BotStatusAppPopup popup, CancellationToken cancellationToken)
		{
			_ = 5;
			try
			{
				_loadingHeader = popup.PrintLine(LOC.UI.PlayVideoLoading('l'));
				popup.UpdateDisplay();
				await UniTask.Yield(cancellationToken);
				_videoPlayer = new GameObject("VideoPlayer").AddComponent<VideoPlayer>();
				_videoPlayer.url = _videoPath;
				_videoPlayer.isLooping = false;
				_videoPlayer.renderMode = VideoRenderMode.RenderTexture;
				_videoPlayer.aspectRatio = VideoAspectRatio.Stretch;
				_videoPlayer.playOnAwake = false;
				_videoPlayer.waitForFirstFrame = true;
				_videoPlayer.Prepare();
				UniTaskCompletionSource tcs = new UniTaskCompletionSource();
				_videoPlayer.errorReceived += (VideoPlayer x, string a) =>
				{
					tcs.TrySetException(new Exception(a));
				};
				_videoPlayer.prepareCompleted += (VideoPlayer x) =>
				{
					tcs.TrySetResult();
				};
				UniTask tcsTask = tcs.Task;
				popup.StartPlayerInput();
				while (!tcsTask.Status.IsCompleted())
				{
					if (popup.TryEatPlayerInput(out var str) && str.Contains('l'))
					{
						popup.PrintLine(LOC.UI.PlayVideoLoadingCancelled);
						popup.UpdateDisplay();
						return;
					}
					await UniTask.Yield(cancellationToken);
				}
				await tcsTask;
				float num = (float)_videoPlayer.width / (float)_videoPlayer.height / popup.CharAspectRatio;
				int widthCharCount = popup.WidthCharCount;
				int num2 = popup.HeightCharCount - 6;
				_width = widthCharCount;
				_height = Mathf.FloorToInt((float)widthCharCount / num);
				if (_height > num2)
				{
					_height = num2;
					_width = Mathf.FloorToInt((float)num2 * num);
				}
				board = new char[_width * _height];
				_renderTexture = new RenderTexture(_width, _height, 0, RenderTextureFormat.Default);
				_videoPlayer.targetTexture = _renderTexture;
				_renderTextureFlipped = new RenderTexture(_width, _height, 0, RenderTextureFormat.Default);
				_scaledTexture = new Texture2D(_width, _height);
				SetGradient();
				_videoDisplay = new BotStatusAppPopup.CharArrayConsoleLine(board, _width, _height, BotStatusAppPopup.CharArrayConsoleLine.CharArrayPrintTypesEnum.Center);
				_footer = popup.GetStringConsoleLine("");
				_progressBar = new BotStatusAppPopup.CharArrayConsoleLine(_width, BotStatusAppPopup.CharArrayConsoleLine.CharArrayPrintTypesEnum.Center);
				_footer.StringPrintType = BotStatusAppPopup.StringConsoleLine.StringPrintTypesEnum.DecorativeWrapCenter;
				_footer.MaxVerticalLinesAllowed = 6;
				UpdateFooter();
				_videoPlayer.Play();
				popup.StartPlayerInput();
				while (!_videoPlayer.isPlaying)
				{
					if (popup.TryEatPlayerInput(out var str2) && str2.Contains('l'))
					{
						return;
					}
					await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
				}
				popup.RemoveLine(_loadingHeader);
				popup.PrintLine(_videoDisplay);
				popup.PrintLine(_progressBar);
				popup.PrintLine(_footer);
				long frame = -1L;
				while (_videoPlayer.isPlaying && !cancellationToken.IsCancellationRequested)
				{
					if (popup.TryEatPlayerInput(out var str3))
					{
						string text = str3;
						for (int num3 = 0; num3 < text.Length; num3++)
						{
							switch (text[num3])
							{
							case 'l':
								_videoPlayer.Stop();
								break;
							case 'a':
								MoveSecondsVideo(-10f);
								break;
							case 's':
								MoveSecondsVideo(-1f);
								break;
							case 'd':
								MoveSecondsVideo(1f);
								break;
							case 'f':
								MoveSecondsVideo(10f);
								break;
							case 'r':
								_videoPlayer.isLooping = !_videoPlayer.isLooping;
								UpdateFooter();
								break;
							case 'g':
								NextGradient();
								UpdateFooter();
								break;
							case 'c':
								_videoPlayer.playbackSpeed = 0f;
								popup.EndPlayerInput();
								var (flag, text2) = await AsciiRenderingUtils.GetCustomGradient(popup, cancellationToken);
								if (flag)
								{
									gradientIndex = -1;
									currentGradient = text2;
									UpdateFooter();
								}
								popup.StartPlayerInput();
								_videoPlayer.playbackSpeed = 1f;
								break;
							}
						}
					}
					if (frame != _videoPlayer.frame)
					{
						_progressBar.SetAsProgress((float)_videoPlayer.frame / (float)_videoPlayer.frameCount);
						UpdateBoardWithVideoFrame();
						popup.UpdateDisplay();
					}
					await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
				}
				popup.EndPlayerInput();
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				popup.PrintLine(LOC.UI.ErrorOccured + ex.Message);
			}
			finally
			{
				if (_videoPlayer != null)
				{
					UnityEngine.Object.Destroy(_videoPlayer.gameObject);
				}
				if (_renderTextureFlipped != null)
				{
					UnityEngine.Object.Destroy(_renderTextureFlipped);
				}
				if (_renderTexture != null)
				{
					UnityEngine.Object.Destroy(_renderTexture);
				}
				if (_scaledTexture != null)
				{
					UnityEngine.Object.Destroy(_scaledTexture);
				}
				popup.RemoveLine(_loadingHeader);
				popup.RemoveLine(_videoDisplay);
				popup.RemoveLine(_progressBar);
				popup.RemoveLine(_footer);
			}
			void MoveSecondsVideo(float seconds)
			{
				_videoPlayer.frame = Math.Clamp(_videoPlayer.frame + (long)(_videoPlayer.frameRate * seconds), 0L, (long)_videoPlayer.frameCount);
			}
		}

		private void UpdateBoardWithVideoFrame()
		{
			ImageUtility.RunTimeFlipTexture(_renderTexture, _renderTextureFlipped, flipX: false, flipY: true);
			using (new ImageUtility.UsingRenderTexture(_renderTextureFlipped))
			{
				ImageUtility.ActiveRenderTextureToTextureCpuSide(_renderTextureFlipped, _scaledTexture);
			}
			Color32[] pixels = _scaledTexture.GetPixels32();
			_videoDisplay.Colors = pixels;
			for (int i = 0; i < _height * _width; i++)
			{
				Color32 color = pixels[i];
				float brightness = (0.2126f * (float)(int)color.r + 0.7152f * (float)(int)color.g + 0.0722f * (float)(int)color.b) / 255f;
				board[i] = AsciiRenderingUtils.GetShadeCharacter(currentGradient, brightness);
			}
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

		private void UpdateFooter()
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.AppendLine($"{LOC.UI.PlayVideoVideoSize} {_width}x{_height} {LOC.UI.PlayVideoCurrentGradient} \"{currentGradient}\"");
				stringBuilder.AppendLine($"'{'a'}' '{'s'}' '{'d'}' '{'f'}' = {LOC.UI.PlayVideoSkip}");
				if (gradientIndex >= 0)
				{
					stringBuilder.Append($"{'g'} = {LOC.Polyglot.MENU_LABEL_GRAPHICS} ({gradientIndex + 1}/{AsciiRenderingUtils.GradientCount}), ");
				}
				else
				{
					stringBuilder.Append($"{'g'} = {LOC.Polyglot.MENU_LABEL_GRAPHICS} ({LOC.UI.CubeCommandGradientCustom}), ");
				}
				stringBuilder.Append($"{'r'} = {(_videoPlayer.isLooping ? LOC.UI.PlayVideoTurnOffLooping : LOC.UI.PlayVideoTurnOnLooping)}, ");
				stringBuilder.Append($"{'c'} = {LOC.UI.CubeCommandGradientCustom}, ");
				stringBuilder.Append($"{'l'} = {LOC.Polyglot.MENU_LABEL_EXIT}");
				_footer.String = stringBuilder.ToString();
			}
		}
	}

	public override string CommandName => "playvideo";

	public override string CommandDescription => LOC.UI.PlayVideoDescription;

	public override bool IsListed => false;

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		string text = "";
		if (input.Arguments.Length != 0)
		{
			text = input.Arguments[0];
		}
		if (string.IsNullOrEmpty(text))
		{
			popup.PrintLine(LOC.UI.PlayVideoInvalidPath);
			return;
		}
		await new VideoCommandLogic(text).Start(popup, cancellationToken);
		popup.PrintLine(LOC.UI.CommandClosed(CommandName));
	}
}
