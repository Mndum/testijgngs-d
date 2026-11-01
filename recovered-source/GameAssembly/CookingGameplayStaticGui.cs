using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Coffee.UIExtensions;
using Cysharp.Threading.Tasks;
using Melanchall.DryWetMidi.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CookingGameplayStaticGui : StaticGuiBase, CookingMinigameInputs.IMap1Actions
{
	public enum CookingMinigameState
	{
		Nothing,
		Loading,
		Playing,
		Done
	}

	public struct QueuedInput(CookingInputFlagEnum inputType, double inputTime)
	{
		public CookingInputFlagEnum InputType = inputType;

		public double InputTime = inputTime;

		public void MakeTimeRelativeToSong(double realTimeSinceStartup, double songCurrentTime)
		{
			InputTime = InputTime - realTimeSinceStartup + songCurrentTime;
		}
	}

	[FormerlySerializedAs("basicNotePool")]
	[Header("Note prefabs")]
	[SerializeField]
	private PooledPrefabReference<CookingNote> basicNotePrefabReference;

	[Header("Other")]
	[SerializeField]
	private Image zButtonDisplay;

	[SerializeField]
	private Image xButtonDisplay;

	[SerializeField]
	private TextMeshProUGUI scoreText;

	[SerializeField]
	private TextMeshProUGUI accuracyText;

	[SerializeField]
	private RectTransform cookingShow;

	[SerializeField]
	private Slider progressSlider;

	[SerializeField]
	private UIParticle feverUiParticle;

	[SerializeField]
	private UIParticle noteHitZParticle;

	[FormerlySerializedAs("noteHitYParticle")]
	[SerializeField]
	private UIParticle noteHitXParticle;

	[Header("Auto")]
	[SerializeField]
	private Button returnButton;

	[SerializeField]
	private Image playAreaImage;

	[SerializeField]
	private TextMeshProUGUI noteText;

	private float _clickedScoreTimer;

	private StringBuilder _scoreStringBuilder = new StringBuilder();

	private string[] _localizedScoreNamesCache;

	private const double AllowedErrorSeconds = 0.2;

	private const double TimeBeforeStart = 0.5;

	private CookingGameplayStartInfo _startInfo;

	private CookingSong _song;

	private AudioClipDisposableWrapper _audioClipWrapper;

	private AudioSource _audioSource;

	private CookingMinigameInputs cookingMinigameInputs;

	[SerializeField]
	private CookingShow _currentCookingShow;

	private CookingMinigameState _cookingMinigameState;

	private Stopwatch _timerStopwatch = new Stopwatch();

	private List<CookingNote> _flyingOffNotes;

	private List<CookingNote> _activeNotes;

	private int _currentStep;

	private int _currentCombo;

	private const double GoodNotesForFever = 10.0;

	private const double GoodNotesTimeForFever = 2.0;

	private const double FeverGainedPerGreat = 0.2;

	private const double FeverMeterMin = -1.0;

	private double _currentFeverMeter;

	private double _lastFeverTime;

	private bool _isFever;

	private double _maxCurrentGameplayAccuracyScore;

	private CookingGameplayScoreData _scoreData;

	private CookingSongRuntimeData _songRuntimeData;

	private bool _songFinished;

	private double _currentTime;

	private double _startOffset;

	private double _lookAheadTimeOffset;

	private bool _leftButtonHeld;

	private bool _rightButtonHeld;

	private List<QueuedInput> _inputQueue = new List<QueuedInput>();

	private CookingInputFlagEnum _lastTouchscreen;

	private CancellationTokenSource _minigameCancellation;

	private Action<CookingResults> _onFinished;

	public Button ReturnButton => returnButton;

	public Image PlayAreaImage => playAreaImage;

	public TextMeshProUGUI NoteText => noteText;

	public bool IsFever => _isFever;

	public double CurrentTime => _currentTime;

	private void PrepareLocalizedScoreNames()
	{
		Array.Resize(ref _localizedScoreNamesCache, 6);
		_localizedScoreNamesCache[0] = "";
		_localizedScoreNamesCache[1] = LOC.UI.CookingMinigame_Great;
		_localizedScoreNamesCache[2] = LOC.UI.CookingMinigame_Good;
		_localizedScoreNamesCache[3] = LOC.UI.CookingMinigame_Bad;
		_localizedScoreNamesCache[4] = LOC.UI.CookingMinigame_WrongKey;
		_localizedScoreNamesCache[5] = LOC.UI.CookingMinigame_Miss;
	}

	private string GetLocalizedScoreName(NoteClickedScoreEnum scoreEnum)
	{
		return _localizedScoreNamesCache[(int)scoreEnum];
	}

	private void ShowNoteScoreText(NoteClickedScoreEnum clickedScoreEnum, int combo)
	{
		Color color = Color.white;
		if (clickedScoreEnum == NoteClickedScoreEnum.None)
		{
			noteText.SetText("");
		}
		else
		{
			_scoreStringBuilder.Clear();
			_scoreStringBuilder.Append(GetLocalizedScoreName(clickedScoreEnum));
			if (combo > 0)
			{
				_scoreStringBuilder.Append(" ");
				_scoreStringBuilder.Append(combo);
			}
			switch (clickedScoreEnum)
			{
			case NoteClickedScoreEnum.Great:
				color = Color.cyan;
				break;
			case NoteClickedScoreEnum.Good:
				color = Color.green;
				break;
			case NoteClickedScoreEnum.Bad:
				color = Color.yellow;
				break;
			case NoteClickedScoreEnum.WrongNote:
			case NoteClickedScoreEnum.Miss:
				color = Color.red;
				break;
			}
			noteText.SetText(_scoreStringBuilder);
		}
		noteText.color = color;
		_clickedScoreTimer = 0f;
		RectTransform rectTransform = noteText.transform.AsRectTransform();
		rectTransform.localScale = Vector3.one;
		rectTransform.SetAnchoredPositionY(0f);
		rectTransform.localRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-5f, 5f));
	}

	private void Update()
	{
		_clickedScoreTimer += Time.deltaTime;
		if (_clickedScoreTimer > 1f)
		{
			noteText.SetText("");
			return;
		}
		float num = EasingFunction.EaseOutQuad(0f, 1f, _clickedScoreTimer);
		RectTransform rectTransform = noteText.transform.AsRectTransform();
		rectTransform.SetAnchoredPositionY(num * 50f);
		rectTransform.localScale = Vector3.one * (1f + num * 0.1f);
	}

	public static bool SameButton(CookingInputFlagEnum inputType, NoteButtonTypeEnum noteButtonTypeEnum)
	{
		if (inputType.HasFlagFast(CookingInputFlagEnum.Left) && noteButtonTypeEnum == NoteButtonTypeEnum.Left)
		{
			return true;
		}
		if (inputType.HasFlagFast(CookingInputFlagEnum.Right) && noteButtonTypeEnum == NoteButtonTypeEnum.Right)
		{
			return true;
		}
		return false;
	}

	private void QueueInput(CookingInputFlagEnum inputType, double time)
	{
		_inputQueue.Add(new QueuedInput(inputType, time));
	}

	private void QueueSimpleInput(CookingInputFlagEnum key, InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			QueueInput(key | CookingInputFlagEnum.Down, context.time);
		}
		else if (context.phase == InputActionPhase.Canceled)
		{
			QueueInput(key | CookingInputFlagEnum.Up, context.time);
		}
	}

	public void OnLeftInput(InputAction.CallbackContext context)
	{
		QueueSimpleInput(CookingInputFlagEnum.Left, context);
	}

	public void OnRightInput(InputAction.CallbackContext context)
	{
		QueueSimpleInput(CookingInputFlagEnum.Right, context);
	}

	public void OnClickAnywhere(InputAction.CallbackContext context)
	{
		Vector2 vector = context.ReadValue<Vector2>();
		Vector3 vector2 = UiOverlay.Instance.MainCamera.ScreenToViewportPoint(vector);
		if (context.phase == InputActionPhase.Started)
		{
			CookingInputFlagEnum cookingInputFlagEnum = ((!(vector2.x > 0.5f)) ? CookingInputFlagEnum.Left : CookingInputFlagEnum.Right);
			cookingInputFlagEnum |= CookingInputFlagEnum.Down;
			QueueInput(cookingInputFlagEnum, context.time);
			_lastTouchscreen = cookingInputFlagEnum;
		}
		else if (context.phase == InputActionPhase.Canceled && _lastTouchscreen.HasFlagFast(CookingInputFlagEnum.Down))
		{
			CookingInputFlagEnum cookingInputFlagEnum2 = (_lastTouchscreen & ~CookingInputFlagEnum.Down) | CookingInputFlagEnum.Up;
			QueueInput(cookingInputFlagEnum2, context.time);
			_lastTouchscreen = cookingInputFlagEnum2;
		}
	}

	private void InitParticles()
	{
		noteHitZParticle.StopEmission();
		noteHitXParticle.StopEmission();
		feverUiParticle.StopEmission();
		Gradient gradient = new Gradient();
		gradient.SetKeys(new GradientColorKey[3]
		{
			new GradientColorKey(Color.white, 0.1f),
			new GradientColorKey(CookingGameplayUtilities.LeftClickMainColor, 0.2f),
			new GradientColorKey(CookingGameplayUtilities.LeftClickSecondColor, 1f)
		}, new GradientAlphaKey[2]
		{
			new GradientAlphaKey(1f, 0f),
			new GradientAlphaKey(0f, 1f)
		});
		Gradient gradient2 = new Gradient();
		gradient2.SetKeys(new GradientColorKey[3]
		{
			new GradientColorKey(Color.white, 0.1f),
			new GradientColorKey(CookingGameplayUtilities.RightClickMainColor, 0.2f),
			new GradientColorKey(CookingGameplayUtilities.RightClickSecondColor, 1f)
		}, new GradientAlphaKey[2]
		{
			new GradientAlphaKey(1f, 0f),
			new GradientAlphaKey(0f, 1f)
		});
		foreach (ParticleSystem particle in noteHitZParticle.particles)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetime = particle.colorOverLifetime;
			colorOverLifetime.color = gradient;
		}
		foreach (ParticleSystem particle2 in noteHitXParticle.particles)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetime2 = particle2.colorOverLifetime;
			colorOverLifetime2.color = gradient2;
		}
		Gradient gradient3 = new Gradient();
		gradient3.mode = GradientMode.Fixed;
		gradient3.SetKeys(new GradientColorKey[2]
		{
			new GradientColorKey(CookingGameplayUtilities.RightClickMainColor, 0.5f),
			new GradientColorKey(CookingGameplayUtilities.LeftClickMainColor, 0.1f)
		}, new GradientAlphaKey[2]
		{
			new GradientAlphaKey(1f, 0f),
			new GradientAlphaKey(1f, 1f)
		});
		foreach (ParticleSystem particle3 in feverUiParticle.particles)
		{
			ParticleSystem.MainModule main = particle3.main;
			main.startColor = gradient3;
		}
	}

	private void ResetAllSessionVariables()
	{
		_cookingMinigameState = CookingMinigameState.Nothing;
		_currentStep = 0;
		_currentCombo = 0;
		_maxCurrentGameplayAccuracyScore = 0.0;
		_currentFeverMeter = 0.0;
		_lastFeverTime = -1.0;
		_isFever = false;
		progressSlider.value = 0f;
		_scoreData = null;
		_leftButtonHeld = (_rightButtonHeld = false);
		_songFinished = false;
		_song = null;
	}

	private void ResetPooled()
	{
		PrefabPool<CookingNote> pool = basicNotePrefabReference.GetPool();
		pool.InitOrResetPooledList(ref _activeNotes);
		pool.InitOrResetPooledList(ref _flyingOffNotes);
	}

	private async UniTask Play(CancellationToken cancellationToken)
	{
		ResetAllSessionVariables();
		ResetPooled();
		InitParticles();
		_currentCookingShow.StartShow(this);
		_cookingMinigameState = CookingMinigameState.Loading;
		_song = _startInfo.Song;
		_songRuntimeData = _song.RuntimeData;
		_scoreData = CookingGameplayScoreData.CreateEmpty(_startInfo.ActiveModifiers);
		UpdateGui(NoteClickedScoreEnum.None);
		try
		{
			_audioClipWrapper = await _song.GetMusic().GetAudioClip();
			if (cookingMinigameInputs == null)
			{
				cookingMinigameInputs = new CookingMinigameInputs();
			}
			cookingMinigameInputs.Enable();
			cookingMinigameInputs.Map1.SetCallbacks(this);
			float x = playAreaImage.transform.AsRectTransform().sizeDelta.x;
			_lookAheadTimeOffset = (double)x / _songRuntimeData.PixelsPerSecond;
			_startOffset = 0.5 + _lookAheadTimeOffset;
			_audioSource = SingletonMonoBehaviour<SoundSingleton>.Instance.GetFreeAudioSource();
			_audioSource.clip = _audioClipWrapper.AudioClip;
			_audioSource.loop = false;
			_audioSource.volume = SoundSingleton.MusicVolume;
			_audioSource.pitch = _startInfo.ActiveModifiers.DetermineTargetPitch();
			_audioSource.PlayScheduled(AudioSettings.dspTime + _startOffset / (double)_audioSource.pitch);
			_timerStopwatch.Restart();
			_cookingMinigameState = CookingMinigameState.Playing;
			while (!_songFinished)
			{
				await UniTask.NextFrame(cancellationToken);
				UpdatePlayFrame();
			}
			_scoreData.IsComplete = true;
			await UniTask.WaitForSeconds(0.5f, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
			_cookingMinigameState = CookingMinigameState.Done;
		}
		finally
		{
			if (cookingMinigameInputs != null)
			{
				cookingMinigameInputs.Map1.RemoveCallbacks(this);
				cookingMinigameInputs.Disable();
			}
			_currentCookingShow.EndShow();
			ResetPooled();
			if (_audioSource != null)
			{
				SingletonMonoBehaviour<SoundSingleton>.Instance.ReturnToPool(_audioSource);
				_audioSource = null;
			}
			if (_audioClipWrapper != null)
			{
				_audioClipWrapper.Dispose();
				_audioClipWrapper = null;
			}
		}
		if (_cookingMinigameState == CookingMinigameState.Done)
		{
			SongFinished();
		}
	}

	private void UpdatePlayFrame()
	{
		List<CookingSongNoteEvent> notes = _songRuntimeData.Notes;
		double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
		double totalSeconds = _timerStopwatch.Elapsed.TotalSeconds;
		double num = ((notes.Count > 0) ? notes[notes.Count - 1].EndTime : ((double)(_audioSource.clip.length / _audioSource.pitch)));
		double currentTime = _currentTime;
		_currentTime = totalSeconds * (double)_audioSource.pitch - _startOffset + _songRuntimeData.SongOffsetSeconds;
		if (_currentTime >= 0.0)
		{
			float time = _audioSource.time;
			if (time > 0f)
			{
				double num2 = (double)time - _currentTime;
				double num3 = Math.Abs(num2);
				if (num3 > 0.2)
				{
					_startOffset = 0f - time;
					_timerStopwatch.Restart();
					Debug.Log($"Audio desynched from the gameplay. (Gameplay: {_currentTime}s Audio:{time}s) Resetting the timer with audio's offset.");
					_currentTime = time;
				}
				else if (num3 > 0.06)
				{
					double num4 = 0.1;
					double num5 = num2 * num4;
					_startOffset -= num5;
				}
			}
		}
		double num6 = Math.Max(_currentTime - currentTime, 0.0);
		double num7 = _currentTime / num;
		progressSlider.value = (float)num7 * progressSlider.maxValue;
		double num8 = _currentTime + _lookAheadTimeOffset;
		if (_currentStep < notes.Count)
		{
			CookingSongNoteEvent cookingSongNoteEvent = notes[_currentStep];
			if (cookingSongNoteEvent.StartTime < num8)
			{
				_activeNotes.Add(StartNote(cookingSongNoteEvent));
				_currentStep++;
			}
		}
		else if (_activeNotes.Count == 0 && _flyingOffNotes.Count == 0)
		{
			_songFinished = true;
		}
		for (int num9 = _flyingOffNotes.Count - 1; num9 >= 0; num9--)
		{
			CookingNote cookingNote = _flyingOffNotes[num9];
			if (cookingNote.UpdateFlyOffProgress())
			{
				_flyingOffNotes.RemoveAtSwapBack(num9);
				basicNotePrefabReference.GetPool().ReturnToPool(cookingNote);
			}
		}
		for (int i = 0; i < _activeNotes.Count; i++)
		{
			_activeNotes[i].UpdateNote(_currentTime, num8, _lookAheadTimeOffset);
		}
		double num10;
		if (_currentTime > 0.0)
		{
			num10 = Math.Abs(TimeConverter.ConvertTo<BarBeatFractionTimeSpan>(MidiExtensions.MetricTimeSpanFromDoubleSeconds(_currentTime), _song.RuntimeData.SongTempoMap).Beats % 1.0 - 0.5) * 2.0;
			num10 = (0.0 - (Math.Cos(Math.PI * num10) - 1.0)) / 2.0;
		}
		else
		{
			double num11 = 60.0 / _song.RuntimeData.AverageBPM;
			num10 = (Math.Cos(_currentTime / num11 * 3.1415927410125732 * 2.0) + 1.0) / 2.0;
		}
		_currentCookingShow.UpdateShow(num10, _activeNotes);
		for (int j = 0; j < _inputQueue.Count; j++)
		{
			QueuedInput input = _inputQueue[j];
			input.MakeTimeRelativeToSong(realtimeSinceStartupAsDouble, _currentTime);
			int num12 = Mathf.Min(_activeNotes.Count, 3);
			int k;
			for (k = 0; k < num12; k++)
			{
				CookingNote note = _activeNotes[k];
				if (ProcessNote())
				{
					break;
				}
				bool ProcessNote()
				{
					CookingNote.ConsumeInfo consumeInfo = note.CanConsumeInput(0.2, input);
					if (consumeInfo.ConsumeResult == CookingInputConsumeResult.Can)
					{
						NoteClickedScoreEnum score = OffsetToScore(consumeInfo.OffsetAbs);
						ScoreNote(ref k, note, score);
						return true;
					}
					if (consumeInfo.ConsumeResult.HasFlagFast(CookingInputConsumeResult.CannotFinished))
					{
						return false;
					}
					if (consumeInfo.ConsumeResult.HasFlagFast(CookingInputConsumeResult.CannotTimeEarly))
					{
						return true;
					}
					if (consumeInfo.ConsumeResult.HasFlagFast(CookingInputConsumeResult.CannotTimeLate))
					{
						return false;
					}
					if (consumeInfo.ConsumeResult.HasFlagFast(CookingInputConsumeResult.CannotWrongType))
					{
						return false;
					}
					if (consumeInfo.ConsumeResult.HasFlagFast(CookingInputConsumeResult.CannotWrongButton))
					{
						ScoreNote(ref k, note, NoteClickedScoreEnum.WrongNote);
						return true;
					}
					return true;
				}
			}
			if (input.InputType.HasFlagFast(CookingInputFlagEnum.Down))
			{
				R.Sound.CookingMinigame_kick.Get().PlaySlightlyRandomVolume();
				if (input.InputType.HasFlagFast(CookingInputFlagEnum.Left))
				{
					_leftButtonHeld = true;
				}
				else
				{
					_rightButtonHeld = true;
				}
			}
			else if (input.InputType.HasFlagFast(CookingInputFlagEnum.Left))
			{
				_leftButtonHeld = false;
			}
			else
			{
				_rightButtonHeld = false;
			}
		}
		zButtonDisplay.color = (_leftButtonHeld ? CookingGameplayUtilities.LeftClickSecondColor : CookingGameplayUtilities.LeftClickMainColor);
		xButtonDisplay.color = (_rightButtonHeld ? CookingGameplayUtilities.RightClickSecondColor : CookingGameplayUtilities.RightClickMainColor);
		_inputQueue.Clear();
		for (int l = 0; l < _activeNotes.Count; l++)
		{
			CookingNote cookingNote2 = _activeNotes[l];
			if (cookingNote2.StartScore != NoteClickedScoreEnum.None)
			{
				if (_currentTime - cookingNote2.RelatedSongNoteEvent.EndTime > 0.2)
				{
					ScoreNote(ref l, cookingNote2, NoteClickedScoreEnum.Miss);
				}
			}
			else if (_currentTime - cookingNote2.RelatedSongNoteEvent.StartTime > 0.2)
			{
				ScoreNote(ref l, cookingNote2, NoteClickedScoreEnum.Miss);
			}
		}
		_currentFeverMeter = Math.Clamp(_currentFeverMeter, -1.0, 2.0);
		_currentFeverMeter -= num6;
		if (_isFever)
		{
			if (_currentFeverMeter < 0.4000000059604645 || _currentCombo < 10)
			{
				_currentFeverMeter = -1.0;
				_lastFeverTime = _currentTime;
				_isFever = false;
				feverUiParticle.StopEmission();
			}
		}
		else if (_currentFeverMeter > 0.800000011920929 && _currentCombo > 10 && _currentTime - _lastFeverTime > 5.0)
		{
			_lastFeverTime = _currentTime;
			_isFever = true;
			feverUiParticle.StartEmission();
		}
	}

	private void ScoreNote(ref int index, CookingNote note, NoteClickedScoreEnum score)
	{
		if (score == NoteClickedScoreEnum.WrongNote || score == NoteClickedScoreEnum.Miss)
		{
			R.Sound.Frick_cum_All.GetRandom().Get().Play();
		}
		_maxCurrentGameplayAccuracyScore++;
		double num = _scoreData.RegisterNote(ref _currentCombo, score);
		double num2 = 0.8 + Math.Min(1.0, (double)_currentCombo / 100.0);
		_currentFeverMeter += 0.2 * num * num2;
		UpdateGui(score);
		if (num > 0.0)
		{
			if (note.RelatedSongNoteEvent.NoteButtonType == NoteButtonTypeEnum.Left)
			{
				noteHitZParticle.MimicBursts();
			}
			else
			{
				noteHitXParticle.MimicBursts();
			}
		}
		if (note.SetNextScore(score))
		{
			_activeNotes.RemoveAt(index);
			index--;
			note.StartFlyingOff();
			_flyingOffNotes.Add(note);
		}
		_currentCookingShow.Score(score, note);
	}

	private CookingNote StartNote(CookingSongNoteEvent noteEvent)
	{
		CookingNote fromPool = basicNotePrefabReference.GetPool().GetFromPool(playAreaImage.transform);
		fromPool.InitializeAsNewNote(noteEvent, _songRuntimeData.PixelsPerSecond);
		return fromPool;
	}

	private void UpdateGui(NoteClickedScoreEnum clickedScoreEnum)
	{
		double value;
		double num;
		if (_scoreData != null)
		{
			value = _scoreData.RawScore;
			num = ((_maxCurrentGameplayAccuracyScore > 0.0) ? (_scoreData.AccuracyScore / _maxCurrentGameplayAccuracyScore) : 1.0);
		}
		else
		{
			value = 0.0;
			num = 1.0;
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.Append(value);
			scoreText.SetText(stringBuilder);
			stringBuilder.Clear();
			stringBuilder.AppendFormat("{0:P2}", num);
			accuracyText.SetText(stringBuilder);
			ShowNoteScoreText(clickedScoreEnum, _currentCombo);
		}
	}

	private NoteClickedScoreEnum OffsetToScore(double offset)
	{
		if (offset < 0.15000000000000002)
		{
			if (offset < 0.05)
			{
				return NoteClickedScoreEnum.Great;
			}
			return NoteClickedScoreEnum.Good;
		}
		if (offset <= 0.2)
		{
			return NoteClickedScoreEnum.Bad;
		}
		return NoteClickedScoreEnum.Miss;
	}

	public void StartGameplay(CookingGameplayStartInfo startInfo, Action<CookingResults> finishedCallback)
	{
		PrepareLocalizedScoreNames();
		_startInfo = startInfo;
		_onFinished = finishedCallback;
		_minigameCancellation = new CancellationTokenSource();
		Play(_minigameCancellation.Token).Forget();
		returnButton.onClick.SetListener(ForceEndSong);
	}

	private void KillMinigameTask()
	{
		if (_minigameCancellation != null)
		{
			_minigameCancellation.Cancel();
			_minigameCancellation.Dispose();
			_minigameCancellation = null;
		}
	}

	public void ForceEndSong()
	{
		KillMinigameTask();
		SongFinished();
	}

	private void SongFinished()
	{
		CookingResults obj = new CookingResults(_startInfo, _scoreData);
		ResetAllSessionVariables();
		_onFinished(obj);
	}

	protected override void FillReferencesInherited()
	{
		Transform transform = base.transform;
		transform.EnsureComponentInChild<Button>(out returnButton, "Return Button");
		transform.EnsureComponentInChild<Image>(out playAreaImage, "Play Area");
		transform.EnsureComponentInChildNested<TextMeshProUGUI>(out noteText, "Play Area", "Target", "Note Text");
	}
}
