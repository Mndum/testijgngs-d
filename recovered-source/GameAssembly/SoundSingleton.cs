using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IC.AutomaticReference;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundSingleton : SingletonMonoBehaviour<SoundSingleton>
{
	public class MusicPlayer
	{
		private ManagedCoroutine _playCoroutine = new ManagedCoroutine();

		private AsyncOperationHandle<AudioClip> _currentHandle;

		public MusicOption Option { get; private set; }

		private AudioSource AudioSource { get; set; }

		public bool Finished { get; private set; }

		public static float MusicVolumeMulti => MusicVolume;

		public MusicPlayer(MusicOption option)
		{
			Option = option;
		}

		private void ReleaseCurrentHandle()
		{
			if (_currentHandle.IsValid())
			{
				Addressables.Release(_currentHandle);
			}
		}

		private float FadeTime(AudioClip clip)
		{
			return Mathf.Min(1f, clip.length / 10f);
		}

		public void UpdateMusicVolume()
		{
			if (AudioSource != null)
			{
				AudioSource.volume = MusicVolumeMulti;
			}
		}

		public void Play()
		{
			_playCoroutine.Start(SingletonMonoBehaviour<SoundSingleton>.Instance, PlayEnumerator());
		}

		private IEnumerator PlayEnumerator()
		{
			if (Option.MusicWithInfo != null)
			{
				ReleaseCurrentHandle();
				_currentHandle = Addressables.LoadAssetAsync<AudioClip>(Option.MusicWithInfo.GetClipReference());
				yield return _currentHandle;
				if (_currentHandle.Status != AsyncOperationStatus.Succeeded)
				{
					Debug.LogError("Loading song " + Option.MusicWithInfo.NameInfo + " failed!");
					yield return WaitForSecondCache.Seconds1;
				}
				else
				{
					AudioClip result = _currentHandle.Result;
					AudioSource = SingletonMonoBehaviour<SoundSingleton>.Instance.GetFreeAudioSource();
					AudioSource.clip = result;
					AudioSource.loop = false;
					AudioSource.volume = 0f;
					AudioSource.Play();
					float fadeInTime = FadeTime(result);
					float timer = 0f;
					do
					{
						timer += Time.deltaTime;
						AudioSource.volume = timer / fadeInTime * MusicVolumeMulti;
						yield return null;
					}
					while (timer < fadeInTime);
					AudioSource.volume = 1f * MusicVolumeMulti;
					yield return WaitForSecondCache.Seconds1;
					while (AudioSource.isPlaying)
					{
						yield return null;
					}
					AudioSource.Stop();
				}
			}
			else
			{
				yield return WaitForSecondCache.Seconds3;
			}
			Finished = true;
			SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
		}

		private IEnumerator FadeOutEnumerator()
		{
			float fadeOutTime = FadeTime(AudioSource.clip);
			float timer = 0f;
			float from = AudioSource.volume;
			do
			{
				timer += Time.deltaTime;
				AudioSource.volume = Mathf.Lerp(from, 0f, timer / fadeOutTime);
				yield return null;
			}
			while (timer < fadeOutTime);
			ReleaseCurrentHandle();
			SingletonMonoBehaviour<SoundSingleton>.Instance.ReturnToPool(AudioSource);
		}

		public void FadeOut()
		{
			if (Finished)
			{
				return;
			}
			Finished = true;
			_playCoroutine.Stop(SingletonMonoBehaviour<SoundSingleton>.Instance);
			if (AudioSource != null)
			{
				if (AudioSource.isPlaying)
				{
					SingletonMonoBehaviour<SoundSingleton>.Instance.StartCoroutine(FadeOutEnumerator());
					return;
				}
				ReleaseCurrentHandle();
				SingletonMonoBehaviour<SoundSingleton>.Instance.ReturnToPool(AudioSource);
			}
		}

		public void SetPitch(float pitch)
		{
			if (AudioSource != null && !Finished)
			{
				AudioSource.pitch = pitch;
			}
		}

		public void ContinueAs(MusicOption musicOption)
		{
			Option = musicOption;
		}
	}

	[Serializable]
	public class MusicOption
	{
		public enum MusicOptionLayer
		{
			Base = 0,
			AboveBase = 50,
			Forced = 100
		}

		[field: SerializeField]
		public string Id { get; private set; }

		[field: SerializeField]
		public MusicOptionLayer Layer { get; private set; }

		[field: SerializeField]
		public MusicWithInfo MusicWithInfo { get; private set; }

		public Func<bool> Condition { get; private set; }

		public bool MeetsCondition
		{
			get
			{
				if (Condition != null)
				{
					return Condition();
				}
				return true;
			}
		}

		public string Info
		{
			get
			{
				if (!(MusicWithInfo != null))
				{
					return $"No song ({Layer} {Id})";
				}
				return MusicWithInfo.Info + $"({Layer} {Id})";
			}
		}

		public string InfoWithCondition
		{
			get
			{
				if (!(MusicWithInfo != null))
				{
					return "No song (" + Id + " " + (MeetsCondition ? "Condition met" : "Condition not met") + ")";
				}
				return MusicWithInfo.Info + "(" + Id + " " + (MeetsCondition ? "Condition met" : "Condition not met") + ")";
			}
		}

		public MusicOption(string id, MusicOptionLayer layer, MusicWithInfo musicWithInfo)
		{
			Id = id;
			Layer = layer;
			MusicWithInfo = musicWithInfo;
		}

		public MusicOption(string id, MusicOptionLayer layer, Func<bool> condition, MusicWithInfo musicWithInfo)
		{
			Id = id;
			Layer = layer;
			Condition = condition;
			MusicWithInfo = musicWithInfo;
		}
	}

	[Serializable]
	private class CurrentMusicInfo
	{
		public GameObject _tooltipPrefab;

		private GameObject _tooltip;

		private TextMeshProUGUI _tmp;

		private CanvasGroup _group;

		private ManagedCoroutine _fadeCoroutine = new ManagedCoroutine();

		private void SetupTooltip()
		{
			_tooltip = UnityEngine.Object.Instantiate(_tooltipPrefab);
			RectTransform orAddComponent = _tooltip.GetOrAddComponent<RectTransform>();
			orAddComponent.SetParent(UiOverlay.Instance.GuiTransform, worldPositionStays: false);
			Vector2 vector = (orAddComponent.anchorMax = new Vector2(0.5f, 1f));
			Vector2 pivot = (orAddComponent.anchorMin = vector);
			orAddComponent.pivot = pivot;
			orAddComponent.anchoredPosition = new Vector2(0f, -20f);
			_tmp = _tooltip.GetComponentInChildren<TextMeshProUGUI>();
			_group = _tooltip.GetOrAddComponent<CanvasGroup>();
			CanvasGroup canvasGroup = _group;
			bool blocksRaycasts = (_group.interactable = false);
			canvasGroup.blocksRaycasts = blocksRaycasts;
			_tooltip.gameObject.SetActive(value: false);
			_group.alpha = 0f;
		}

		private IEnumerator _changeEnumerator(string newText)
		{
			if (_tooltip.gameObject.activeSelf)
			{
				float alpha = _group.alpha;
				if (alpha > 0f)
				{
					yield return CoroutineUtilities.GenericTween((float f) =>
					{
						_group.alpha = f;
					}, alpha, 0f, alpha);
				}
			}
			else
			{
				_tooltip.gameObject.SetActive(value: true);
				_group.alpha = 0f;
			}
			yield return new WaitForSecondsRealtime(1f);
			_tmp.text = newText;
			yield return CoroutineUtilities.GenericTween((float f) =>
			{
				_group.alpha = f;
			}, 0f, 1f, 1f);
			yield return new WaitForSecondsRealtime(3f);
			yield return CoroutineUtilities.GenericTween((float f) =>
			{
				_group.alpha = f;
			}, 1f, 0f, 1f);
			_tooltip.gameObject.SetActive(value: false);
		}

		private void SongChanged(MusicWithInfo newMusic)
		{
			if (newMusic != null && (bool)OptionsStatic.ShowMusicPopupPref && !MusicOff)
			{
				_fadeCoroutine.Start(SingletonMonoBehaviour<SoundSingleton>.Instance, _changeEnumerator(newMusic.Info));
			}
		}

		public void InitializeMusicInfo()
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.OnMusicChanged += SongChanged;
			SetupTooltip();
		}
	}

	[Serializable]
	public class SoundCategory
	{
		public string name;

		public List<AudioClip> audioClips;

		public SoundCategory()
		{
		}

		public SoundCategory(string name, List<AudioClip> audioClips)
		{
			this.name = name;
			this.audioClips = audioClips;
		}

		public AudioClip GetRandom()
		{
			return audioClips.GetRandom();
		}
	}

	private List<MusicOption> _possibleMusic = new List<MusicOption>();

	private MusicPlayer currentMusicPlayer;

	[SerializeField]
	private CurrentMusicInfo _musicInfo;

	private AudioSource _genericAudioSource;

	private List<AudioSource> _audioSourcesPool = new List<AudioSource>();

	private int maxPooled = 10;

	public static float MusicVolume => OptionsStatic.MusicVolume;

	public static float SfxVolume => OptionsStatic.SfxVolume;

	public static bool MusicOff => Mathf.Approximately(MusicVolume, 0f);

	public bool IsPlayingMusic => currentMusicPlayer != null;

	public event Action<MusicWithInfo> OnMusicChanged;

	public void UpdateMusicVolume()
	{
		if (currentMusicPlayer != null)
		{
			currentMusicPlayer.UpdateMusicVolume();
		}
	}

	public void SetMusicPitch(float pitch)
	{
		if (currentMusicPlayer != null)
		{
			currentMusicPlayer.SetPitch(pitch);
		}
	}

	public string CurrentMusicOptionsSummary()
	{
		if (_possibleMusic.Count == 0)
		{
			return "No music options!";
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			IOrderedEnumerable<IGrouping<MusicOption.MusicOptionLayer, MusicOption>> orderedEnumerable = from t in _possibleMusic.ToLookup((MusicOption t) => t.Layer)
				orderby t.Key descending
				select t;
			stringBuilder.AppendLine($"{_possibleMusic.Count} options:");
			foreach (IGrouping<MusicOption.MusicOptionLayer, MusicOption> item in orderedEnumerable)
			{
				MusicOption.MusicOptionLayer key = item.Key;
				List<MusicOption> list = item.ToList();
				stringBuilder.AppendLine(string.Format("{0} {1} options: {2}", key, list.Count, string.Join(", ", list.Select((MusicOption t) => t.InfoWithCondition))));
			}
			return stringBuilder.ToString();
		}
	}

	private List<MusicOption> FindPossibleSongsToPlay()
	{
		foreach (IGrouping<MusicOption.MusicOptionLayer, MusicOption> item in from t in _possibleMusic.ToLookup((MusicOption t) => t.Layer)
			orderby t.Key descending
			select t)
		{
			_ = item.Key;
			List<MusicOption> list = item.Where((MusicOption t) => t.MeetsCondition).ToList();
			if (list.Count > 0)
			{
				return list;
			}
		}
		return new List<MusicOption>();
	}

	private MusicOption ChooseNextSongToPlay(List<MusicOption> possibleSongs, MusicPlayer musicPlayer = null)
	{
		if (possibleSongs.Count == 0)
		{
			return null;
		}
		if (musicPlayer != null && musicPlayer.Finished && musicPlayer.Option.MusicWithInfo != null)
		{
			MusicWithInfo lastMusic = musicPlayer.Option.MusicWithInfo;
			int num = Enumerable.Count(possibleSongs, (MusicOption t) => t.MusicWithInfo == lastMusic);
			if (possibleSongs.Count - num > 0)
			{
				possibleSongs.RemoveAll((MusicOption t) => t.MusicWithInfo == lastMusic);
			}
		}
		int index = UnityEngine.Random.Range(0, possibleSongs.Count);
		MusicOption musicOption = possibleSongs[index];
		DebugLogMusic("Chosen " + musicOption.Info + " from \n" + CurrentMusicOptionsSummary());
		return musicOption;
	}

	public void PlayNextSong(bool continueIfPossible = true)
	{
		List<MusicOption> list = FindPossibleSongsToPlay();
		MusicOption musicOption;
		if (currentMusicPlayer != null)
		{
			if (!currentMusicPlayer.Finished & continueIfPossible)
			{
				MusicWithInfo currentMusic = currentMusicPlayer.Option.MusicWithInfo;
				int num = list.FindIndex((MusicOption t) => t.MusicWithInfo == currentMusic);
				if (num >= 0)
				{
					musicOption = list[num];
					DebugLogMusic("Playing song " + musicOption.Info + " continuing " + currentMusicPlayer.Option.Info);
					currentMusicPlayer.ContinueAs(musicOption);
					return;
				}
			}
			musicOption = ChooseNextSongToPlay(list, currentMusicPlayer);
			currentMusicPlayer.FadeOut();
			currentMusicPlayer = null;
		}
		else
		{
			musicOption = ChooseNextSongToPlay(list);
		}
		if (musicOption != null)
		{
			InvokeMusicChangedEvent(musicOption.MusicWithInfo);
			DebugLogMusic("Playing song " + musicOption.Info);
			currentMusicPlayer = new MusicPlayer(musicOption);
			currentMusicPlayer.Play();
		}
		else
		{
			InvokeMusicChangedEvent(null);
			DebugLogMusic("Next song not found!");
		}
	}

	private void InvokeMusicChangedEvent(MusicWithInfo musicWithInfo)
	{
		try
		{
			OnMusicChanged?.Invoke(musicWithInfo);
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}

	public void RegisterMusicOptions(params MusicOption[] options)
	{
		RegisterMusicOptions((IEnumerable<MusicOption>)options);
	}

	public void RegisterMusicOptions(IEnumerable<MusicOption> enumerable)
	{
		_possibleMusic.AddRange(enumerable);
	}

	public void RegisterMusicOptions(string id, IEnumerable<MusicWithInfo> list, MusicOption.MusicOptionLayer layer, Func<bool> condition = null)
	{
		RegisterMusicOptions(list.Select((MusicWithInfo t) => new MusicOption(id, layer, condition, t)));
	}

	public void RegisterMusicOptions(string id, IEnumerable<IReference<MusicWithInfo>> list, MusicOption.MusicOptionLayer layer, Func<bool> condition = null)
	{
		RegisterMusicOptions(list.Select((IReference<MusicWithInfo> t) => new MusicOption(id, layer, condition, t.GetResource())));
	}

	public bool IsPlaying(MusicOption option)
	{
		if (currentMusicPlayer == null)
		{
			return false;
		}
		return currentMusicPlayer.Option == option;
	}

	public void UnregisterMusicOptionsById(IMusicSource id, bool stopIfPlaying = true)
	{
		UnregisterMusicOptionsById(id.MusicSourceId, stopIfPlaying);
	}

	public void UnregisterMusicOptionsById(string id, bool stopIfPlaying = true)
	{
		bool flag = false;
		for (int num = _possibleMusic.Count - 1; num >= 0; num--)
		{
			MusicOption musicOption = _possibleMusic[num];
			if (musicOption.Id == id)
			{
				_possibleMusic.RemoveAt(num);
				if (stopIfPlaying && IsPlaying(musicOption))
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			PlayNextSong();
		}
	}

	public void UnregisterMusicOption(MusicOption musicOption, bool stopIfPlaying = true)
	{
		bool flag = false;
		for (int num = _possibleMusic.Count - 1; num >= 0; num--)
		{
			MusicOption musicOption2 = _possibleMusic[num];
			if (musicOption2 == musicOption)
			{
				_possibleMusic.RemoveAt(num);
				if (stopIfPlaying && IsPlaying(musicOption2))
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			PlayNextSong();
		}
	}

	public void UnregisterAllMusic(bool stopIfPlaying = true)
	{
		_possibleMusic.Clear();
		if (stopIfPlaying)
		{
			PlayNextSong();
		}
	}

	private void DebugLogMusic(string text)
	{
		if (DebugDefines.LogMusicDebugInfo)
		{
			Debug.Log(text);
		}
	}

	private void InitializeSFX()
	{
		_genericAudioSource = gameObject.AddComponent<AudioSource>();
	}

	public void PlaySlightlyRandomVolume(AudioClip clip)
	{
		Play(clip, UnityEngine.Random.Range(0.91f, 1f));
	}

	public void Play(AudioClip clip, float volume = 1f, Action OnComplete = null)
	{
		if (OnComplete != null)
		{
			StartCoroutine(CoroutineUtilities.InvokeAfterDelay(OnComplete, clip.length));
		}
		_genericAudioSource.PlayOneShot(clip, volume * SfxVolume);
	}

	private IEnumerator WaitFor(AudioSource sourceToWaitFor)
	{
		yield return new WaitForSeconds(sourceToWaitFor.clip.length / 2f);
		while (sourceToWaitFor.isPlaying)
		{
			yield return null;
		}
	}

	public AudioSource PlayWithPitch(AudioClip clip, float volume = 1f, float pitch = 1f, Action OnComplete = null)
	{
		AudioSource source = GetFreeAudioSource();
		source.clip = clip;
		source.volume = volume * SfxVolume;
		source.pitch = pitch;
		source.Play();
		StartCoroutine(WaitFor(source).Then(() =>
		{
			ReturnToPool(source);
			OnComplete?.Invoke();
		}));
		return source;
	}

	public void PlayRandom(float volume)
	{
		Play(SingletonScriptableObject<SoundDataGenerated>.Instance.Sound.Food_All.GetRandom().Get(), volume);
	}

	public AudioSource GetFreeAudioSource()
	{
		if (_audioSourcesPool.Any())
		{
			AudioSource audioSource = _audioSourcesPool[_audioSourcesPool.Count - 1];
			_audioSourcesPool.RemoveAt(_audioSourcesPool.Count - 1);
			audioSource.enabled = true;
			return audioSource;
		}
		return gameObject.AddComponent<AudioSource>();
	}

	[MustUseReturnValue]
	public AudioSourceUsage GetFreeAudioSource(out AudioSource audioSource)
	{
		audioSource = GetFreeAudioSource();
		return new AudioSourceUsage(audioSource);
	}

	public void ReturnToPool(AudioSource source)
	{
		if (!(source == null))
		{
			if (_audioSourcesPool.Count < maxPooled)
			{
				source.Stop();
				source.clip = null;
				source.loop = false;
				source.volume = 1f;
				source.pitch = 1f;
				source.enabled = false;
				source.playOnAwake = false;
				_audioSourcesPool.Add(source);
			}
			else
			{
				UnityEngine.Object.Destroy(source);
			}
		}
	}

	public SpecialAudioLoop CreateSpecialLoop(AudioClip startClip, AudioClip loopClip, AudioClip endClip, float volume = 1f)
	{
		return new SpecialAudioLoop(CreateAudioSource(startClip), CreateAudioSource(loopClip), CreateAudioSource(endClip), volume * SfxVolume);
	}

	private SpecialAudioSource CreateAudioSource(AudioClip clip)
	{
		AudioSource freeAudioSource = GetFreeAudioSource();
		freeAudioSource.clip = clip;
		return new SpecialAudioSource(freeAudioSource);
	}

	private new void Awake()
	{
		base.Awake();
		InitializeSFX();
		_musicInfo.InitializeMusicInfo();
	}
}
