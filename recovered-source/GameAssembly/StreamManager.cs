using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StreamManager
{
	public class DonationsManager
	{
		private StreamManager _streamManager;

		private ManagedCoroutine _showCoroutine = new ManagedCoroutine();

		private Queue<Donation> _donations = new Queue<Donation>();

		private GameObject _frickGuiGO;

		private GameObject _donationGO;

		private CanvasGroup _donationCanvasGroup;

		private TextMeshProUGUI _donationMessageText;

		private TextMeshProUGUI _donationValueText;

		public IReadOnlyCollection<Donation> Donations => _donations;

		private float SpeedUp => 1f / (float)Donations.Count;

		public DonationsManager(StreamManager streamManager, FrickStaticGui frickGui)
		{
			_streamManager = streamManager;
			_frickGuiGO = frickGui.gameObject;
			_donationGO = frickGui.DonationTransform.gameObject;
			_donationCanvasGroup = _donationGO.GetComponent<CanvasGroup>();
			_donationMessageText = frickGui.DonationMessageText;
			_donationValueText = frickGui.DonationValueText;
		}

		public void NewDonation(Donation _donation)
		{
			_donations.Enqueue(_donation);
			if (!_showCoroutine.Running)
			{
				Show(_donation);
			}
		}

		private void Show(Donation _donation)
		{
			_showCoroutine.Start(_frickGuiGO.GetComponent<CanvasGroupFader>(), DonationEnumerator(_donation), () =>
			{
				Donation target = _donations.Dequeue();
				_donationGO.gameObject.SetActive(value: false);
				if (Donations.Count > 0)
				{
					Show(_donations.Peek());
				}
				_streamManager.OnReplyableRemoved(target);
			});
		}

		private IEnumerator DonationEnumerator(Donation donation)
		{
			_donationGO.gameObject.SetActive(value: true);
			DialogueChain dialogueChain = donation.DialogueChain;
			_donationMessageText.text = dialogueChain.GetText();
			_donationValueText.text = dialogueChain.Donation + "$";
			LayoutRebuilder.ForceRebuildLayoutImmediate(_donationGO.transform as RectTransform);
			if (Donations.Count <= 1)
			{
				_donationCanvasGroup.alpha = 0f;
				yield return CoroutineUtilities.GenericTweenEaseOutQuart((float f) =>
				{
					_donationCanvasGroup.alpha = f;
				}, 0f, 1f, 0.25f);
			}
			else
			{
				_donationCanvasGroup.alpha = 1f;
			}
			yield return new WaitForSecondsRealtime(3f * SpeedUp);
			if (Donations.Count <= 1)
			{
				yield return CoroutineUtilities.GenericTweenEaseInQuart((float f) =>
				{
					_donationCanvasGroup.alpha = f;
				}, 1f, 0f, 0.25f);
			}
			else
			{
				_donationCanvasGroup.alpha = 0f;
			}
		}
	}

	private class StreamManagerButtonSource : ButtonListButtonSource
	{
		private StreamManager _streamManager;

		private ButtonListWrapper _contractButtonWrapper;

		private ButtonListWrapper _hideButtonWrapper;

		private ButtonListWrapper _slowModeButtonWrapper;

		public StreamManagerButtonSource(StreamManager streamManager)
		{
			_streamManager = streamManager;
		}

		public override void AddButtonsToList(IModificationPeriodButtonList buttonList)
		{
			_contractButtonWrapper = buttonList.AddButton(-50);
			_contractButtonWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Contract_CurrentContract);
			_contractButtonWrapper.OnClick = ContractButtonClicked;
			_contractButtonWrapper.ActiveFunc = () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cocktractManager.HasActiveContract;
			_slowModeButtonWrapper = buttonList.AddButton(-10);
			_slowModeButtonWrapper.TextFunc = () =>
			{
				if (_streamManager == null)
				{
					return "Error!";
				}
				return (!_streamManager.SlowModeEnabled) ? LOC.UI.Frick_SlowModeEnabled : LOC.UI.Frick_SlowModeDisable;
			};
			_slowModeButtonWrapper.OnClick = SlowModeButtonClicked;
			_slowModeButtonWrapper.ActiveFunc = () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CanEnableSlowMode;
			_hideButtonWrapper = buttonList.AddButton(10);
			_hideButtonWrapper.TextFunc = () =>
			{
				if (_streamManager == null)
				{
					return "Error!";
				}
				return (!_streamManager._frickStaticGui.IsChatVisible()) ? LOC.UI.Frick_CommentsHidden : LOC.UI.Frick_CommentsVisible;
			};
			_hideButtonWrapper.OnClick = ToggleCommentsButtonClicked;
		}

		public override void OnAfterAdded()
		{
			_streamManager._frickStaticGui.OnRightGuiStateChanged += OnRightGuiStateChanged;
		}

		public override void OnBeforeRemoved()
		{
			_streamManager._frickStaticGui.OnRightGuiStateChanged -= OnRightGuiStateChanged;
		}

		private void OnRightGuiStateChanged(FrickStaticGui.RightGuiState rightGuiState)
		{
			_hideButtonWrapper.UpdateButtonText();
		}

		private void ToggleCommentsButtonClicked()
		{
			FrickStaticGui frickStaticGui = _streamManager._frickStaticGui;
			if (frickStaticGui.IsChatVisible())
			{
				frickStaticGui.SetRightGuiState(FrickStaticGui.RightGuiState.ClosingChat);
			}
			else
			{
				frickStaticGui.SetRightGuiState(FrickStaticGui.RightGuiState.OpeningChat);
			}
		}

		private void SlowModeButtonClicked()
		{
			if (_streamManager != null)
			{
				_streamManager.SlowModeEnabled = !_streamManager.SlowModeEnabled;
				if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("firstslowmode"))
				{
					UiOverlay.Instance.OkPopup(LOC.UI.Frick_SlowMode, LOC.UI.Frick_SlowmodeUnlockedPrompt(GameVariables.FollowersNeededForSlowMode));
					SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddFlag("firstslowmode");
				}
				_streamManager.CreateNewComment(DialogueChain.CreateRawComment((_streamManager.SlowModeEnabled ? LOC.UI.Frick_SlowModeEnabled : LOC.UI.Frick_SlowModeDisabled).TMPColorSurround(Color.red), StreamEventTagsParse)).RemoveOnClick();
				SyncSlowModeButton();
			}
		}

		private void SyncSlowModeButton()
		{
			ConnectedButtonList.UpdateButtonSingle(_slowModeButtonWrapper);
		}

		private void ContractButtonClicked()
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cocktractManager.ShowCurrentContract();
		}
	}

	public enum BundleStreamEventsTags
	{
		Cum,
		Fucking,
		FuckingAnal,
		Blowjob,
		Fingering
	}

	private List<IStreamMinutePassedReceiver> _minutePassedReceivers;

	private List<IStreamStartAndEndReceiver> _streamStartAndEndReceivers;

	private List<IStreamEventReceiver> _streamEventReceivers;

	private FrickStaticGui _frickStaticGui;

	private GameVariables gv;

	private Transform _streamInfoWindow;

	private TextMeshProUGUI _viewersInfo;

	private TextMeshProUGUI _followersInfo;

	private TextMeshProUGUI _subsInfo;

	private RectTransform _commentHolder;

	private GameObject _commentPrefab;

	private float _maxCommentPosY;

	private List<Comment> _freeCommentsPool = new List<Comment>();

	private List<Comment> _comments = new List<Comment>();

	private List<IStreamReplyable> _replyable = new List<IStreamReplyable>();

	private List<IStreamReplyable> _streamReplyableCache = new List<IStreamReplyable>();

	private DonationsManager _donationsManager;

	private float _timer;

	private readonly float PositiveCommentRatio;

	private float TargetDonations = -10f;

	private readonly float _streamBonus = 1f;

	private int _targetViewerNumberRaw;

	private int _viewerNumber;

	private float _streamMood;

	private readonly float SlowModeMulti = 30f;

	public const float ViewerToSubRatio = 1f / 3f;

	public const float MaxFollowerToSubRatio = 0.8f;

	private int _messageCountSinceLastUpdate;

	private float _messagesPerSecond;

	public int StreamStart { get; private set; }

	public int LastDonation { get; private set; }

	public int SubsGained { get; private set; }

	public int SubsLost { get; private set; }

	public int MoneyEarnedDonations { get; private set; }

	public int StreamLength => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time - StreamStart;

	public int Viewers => _viewerNumber;

	public int MessageCount { get; private set; }

	public NewFrickState FrickState { get; private set; }

	private float _syncEvery => 0.1f;

	public bool SlowModeEnabled { get; set; }

	private float TargetMessagesPerSecond
	{
		get
		{
			float num = (float)_viewerNumber * 0.0025f;
			return Mathf.Min(num, Mathf.Log(num + 1f) * 20f);
		}
	}

	public StreamManager(int streamStart, NewFrickState frickState, FrickStaticGui frickGui)
	{
		_frickStaticGui = frickGui;
		_frickStaticGui.RightButtonList.AddButtonSource(new StreamManagerButtonSource(this));
		gv = GameVariables.Current;
		_targetViewerNumberRaw = 5 + gv.followers + gv.subs;
		StreamStart = (LastDonation = streamStart);
		_commentHolder = frickGui.CommentHolder;
		frickGui.CommentPrefab.GetComponentInChildren<TextMeshProUGUI>().font = LocaleFontMetadata.GetFontCurrentLocale(LocaleFontMetadata.FontType.MainFont);
		_commentPrefab = frickGui.CommentPrefab;
		_maxCommentPosY = _commentHolder.rect.height - _commentHolder.sizeDelta.y / 2f;
		_streamInfoWindow = frickGui.StreamInfoWindow;
		_streamInfoWindow.gameObject.SetActive(value: true);
		_viewersInfo = frickGui.ViewersInfo;
		_followersInfo = frickGui.FollowersInfo;
		_subsInfo = frickGui.SubsInfo;
		_viewersInfo.gameObject.GetComponentInParent<TooltipDisplayArea>().tooltipText = LOC.UI.Frick_ViewersTooltip;
		_followersInfo.gameObject.GetComponentInParent<TooltipDisplayArea>().tooltipText = LOC.UI.Frick_FollowersTooltip;
		_subsInfo.gameObject.GetComponentInParent<TooltipDisplayArea>().tooltipText = LOC.UI.Frick_SubsTooltip;
		SyncGui();
		_donationsManager = new DonationsManager(this, frickGui);
		PositiveCommentRatio = 0.5f;
		CreateNewComment(DialogueChain.CreateRawComment(LOC.UI.Frick_ChatStart, StreamEventTagsParse)).RemoveOnClick();
		GameScript instance = SingletonLoadableMonoBehaviour<GameScript>.Instance;
		_streamBonus = gv.itemManager.CalculateStreamingBonus();
		_minutePassedReceivers = gv.cocktractManager.GetAllRequirements<IStreamMinutePassedReceiver>();
		instance.OnMinutePassed += OnMinutePassed;
		FrickState = frickState;
		_streamStartAndEndReceivers = gv.cocktractManager.GetAllRequirements<IStreamStartAndEndReceiver>();
		_streamEventReceivers = gv.cocktractManager.GetAllRequirements<IStreamEventReceiver>();
		gv.AddFlag("StreamStarted");
		foreach (IStreamStartAndEndReceiver streamStartAndEndReceiver in _streamStartAndEndReceivers)
		{
			streamStartAndEndReceiver.StreamStarted(this);
		}
		OnMinutePassed();
	}

	public void EndStream()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.OnMinutePassed -= OnMinutePassed;
		foreach (IStreamStartAndEndReceiver streamStartAndEndReceiver in _streamStartAndEndReceivers)
		{
			streamStartAndEndReceiver.StreamFinished(this);
		}
		foreach (IStreamStartAndEndReceiver streamStartAndEndReceiver2 in _streamStartAndEndReceivers)
		{
			streamStartAndEndReceiver2.StreamLateFinished(this);
		}
		GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
		gameVariables.streamCount++;
		int streamLength = StreamLength;
		gameVariables.streamedFor += streamLength;
		if (gameVariables.longestStream < streamLength)
		{
			gameVariables.longestStream = streamLength;
		}
		gameVariables.lastStreamedAt = gameVariables.time;
		foreach (Transform item in _commentHolder.transform)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
	}

	private void OnMinutePassed()
	{
		foreach (IStreamMinutePassedReceiver minutePassedReceiver in _minutePassedReceivers)
		{
			minutePassedReceiver.MinutePassed(this);
		}
	}

	public void SyncStream(float time, float cumScore)
	{
		if ((StreamLength + 1) % 60 == 0)
		{
			FrickState.Brain.InteractionMemory.FireEvent(InteractionMemoryEventEnum.StreamHourPassed);
		}
		_timer += time;
		if (_timer > _syncEvery)
		{
			if (_messageCountSinceLastUpdate > 0)
			{
				RemoveCommentsOutOfBounds();
			}
			MessageCount += _messageCountSinceLastUpdate;
			float num = (float)_messageCountSinceLastUpdate / _syncEvery;
			_messagesPerSecond = (num + _messagesPerSecond) / 2f;
			SyncViewers(cumScore);
			_timer -= _syncEvery;
			_messageCountSinceLastUpdate = 0;
			if (_replyable.Any() && UnityEngine.Random.value > 0.5f)
			{
				int count = UnityEngine.Random.Range(0, _replyable.Count);
				_streamReplyableCache.Clear();
				_streamReplyableCache.AddRange(_replyable.Take(count));
				_replyable.RemoveRange(0, count);
				foreach (IStreamReplyable item in _streamReplyableCache)
				{
					ReplyTo(item);
				}
			}
			CommentsSync();
		}
		SyncGui();
	}

	private void ReplyTo(IStreamReplyable target, bool replacementAllowed = true)
	{
		DialogueChain dialogueChain = target.ConsumeResponse();
		if (dialogueChain == null)
		{
			return;
		}
		if (dialogueChain.ReplacePrevious)
		{
			if (replacementAllowed && target is Comment target2)
			{
				ReplaceComment(target2, dialogueChain);
			}
		}
		else
		{
			AddInteraction(dialogueChain);
		}
	}

	public void OnReplyableRemoved(IStreamReplyable target)
	{
		if (target.CanBeRepliedTo)
		{
			_replyable.Remove(target);
			if (UnityEngine.Random.value > 0.5f)
			{
				ReplyTo(target, replacementAllowed: false);
			}
		}
	}

	private void AddInteraction(string text)
	{
		AddInteraction(DialogueChain.CreateRawComment(text, StreamEventTagsParse));
	}

	private void AddInteraction(DialogueChain dialogueChain, int positivity = 0)
	{
		if (dialogueChain == null)
		{
			return;
		}
		int num = Math.Min(dialogueChain.Times, 7);
		if (dialogueChain.Unique && AlreadyExists(dialogueChain))
		{
			return;
		}
		dialogueChain.Activate();
		for (int i = 0; i < num; i++)
		{
			int donation = dialogueChain.Donation;
			if (donation == 0)
			{
				CreateNewComment(dialogueChain, positivity);
				continue;
			}
			Donation donation2 = new Donation(dialogueChain);
			_donationsManager.NewDonation(donation2);
			gv.AddMoney(donation);
			gv.moneyEarnedFromDonations += donation;
			MoneyEarnedDonations += donation;
			TargetDonations -= UnityEngine.Random.Range((float)donation * 0.5f, (float)donation * 1.5f);
			LastDonation = gv.time;
		}
	}

	private void MoveAllComments(float by)
	{
		MoveComments(by, _comments.Count);
	}

	private void MoveToPool(Comment comment)
	{
		comment.SetActive(value: false);
		comment.RemoveOnClick();
		OnReplyableRemoved(comment);
		_freeCommentsPool.Add(comment);
	}

	private void MoveComments(float by, int endIndex)
	{
		for (int i = 0; i < endIndex; i++)
		{
			_comments[i].MoveY(by);
		}
	}

	private void RemoveCommentsOutOfBounds()
	{
		int num = -1;
		for (int i = 0; i < _comments.Count && _comments[i].Position > _maxCommentPosY; i++)
		{
			num = i;
		}
		if (num == -1)
		{
			return;
		}
		List<Comment> range = _comments.GetRange(0, num + 1);
		_comments.RemoveRange(0, num + 1);
		foreach (Comment item in range)
		{
			MoveToPool(item);
		}
	}

	private void ReplaceComment(Comment target, DialogueChain response)
	{
		float num = target.Replace(response);
		int endIndex = _comments.FindIndex((Comment t) => t == target);
		MoveComments(num, endIndex);
		if (target.CanBeRepliedTo)
		{
			_replyable.Add(target);
		}
	}

	private void BanComment(Comment comment, int positivity)
	{
		ReplaceComment(comment, DialogueChain.CreateRawComment(LOC.UI.Frick_ChatBanned, StreamEventTagsParse));
		_viewerNumber--;
		if (_viewerNumber < 0)
		{
			_viewerNumber = 0;
		}
		comment.RemoveOnClick();
		_streamMood -= ((positivity > 0) ? (4 * positivity) : positivity);
	}

	private Comment GetFromPoolOrCreate()
	{
		if (_freeCommentsPool.Count == 0)
		{
			return new Comment(_commentPrefab, _commentHolder.transform);
		}
		Comment comment = _freeCommentsPool[_freeCommentsPool.Count - 1];
		_freeCommentsPool.RemoveAt(_freeCommentsPool.Count - 1);
		comment.ResetPosition();
		return comment;
	}

	public Comment CreateNewComment(DialogueChain dialogueChain, int positivity = 0)
	{
		_messageCountSinceLastUpdate++;
		float num = 5f;
		Comment fromPoolOrCreate = GetFromPoolOrCreate();
		fromPoolOrCreate.SetDialogueChain(dialogueChain);
		fromPoolOrCreate.AddOnClick((Comment t) =>
		{
			BanComment(t, positivity);
		});
		fromPoolOrCreate.SetActive(value: true);
		MoveAllComments(num + fromPoolOrCreate.Height);
		_comments.Add(fromPoolOrCreate);
		if (fromPoolOrCreate.CanBeRepliedTo)
		{
			_replyable.Add(fromPoolOrCreate);
		}
		return fromPoolOrCreate;
	}

	public bool AlreadyExists(DialogueChain dialogueChain)
	{
		if (!_donationsManager.Donations.Any((Donation m) => m.DialogueChain == dialogueChain))
		{
			return _comments.Exists((Comment m) => m.DialogueChain == dialogueChain);
		}
		return true;
	}

	public static string StreamEventTagsParse(string input, List<int> Tags)
	{
		if (Enum.TryParse<InteractionMemoryEventEnum>(input, ignoreCase: true, out var result))
		{
			Tags.Add((int)result);
			return null;
		}
		if (Enum.TryParse<BundleStreamEventsTags>(input, ignoreCase: true, out var result2))
		{
			switch (result2)
			{
			case BundleStreamEventsTags.Cum:
				Tags.Add(28);
				Tags.Add(27);
				Tags.Add(29);
				return null;
			case BundleStreamEventsTags.Fucking:
				Tags.Add(6);
				Tags.Add(7);
				Tags.Add(9);
				Tags.Add(8);
				return null;
			case BundleStreamEventsTags.FuckingAnal:
				Tags.Add(14);
				Tags.Add(15);
				Tags.Add(17);
				Tags.Add(16);
				return null;
			case BundleStreamEventsTags.Blowjob:
				Tags.Add(19);
				Tags.Add(18);
				Tags.Add(20);
				return null;
			case BundleStreamEventsTags.Fingering:
				Tags.Add(36);
				Tags.Add(35);
				return null;
			}
		}
		return "\"" + input + "\" is not a proper StreamEventTags enum!";
	}

	public void ReportTagToEventReceivers(InteractionMemoryEventEnum tag)
	{
		foreach (IStreamEventReceiver streamEventReceiver in _streamEventReceivers)
		{
			streamEventReceiver.StreamEvent(this, tag);
		}
	}

	public float CurrentTagScore()
	{
		return FrickState.Brain.InteractionMemory.GetStreamScore();
	}

	private void GenerateRandomComment(List<InteractionMemoryEventEnum> currentTags)
	{
		if (currentTags.Any() && UnityEngine.Random.value < 0.5f)
		{
			InteractionMemoryEventEnum random = currentTags.GetRandom();
			if (UnityEngine.Random.Range(0f, 1f) < PositiveCommentRatio)
			{
				AddInteraction(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.PositiveComments.GetRandomPossibleTagged((int)random), 1);
			}
			else
			{
				AddInteraction(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.NegativeComments.GetRandomPossibleTagged((int)random), -1);
			}
		}
		else if (UnityEngine.Random.Range(0f, 1f) < PositiveCommentRatio)
		{
			AddInteraction(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.PositiveComments.GetRandomPossibleUntagged(), 1);
		}
		else
		{
			AddInteraction(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.NegativeComments.GetRandomPossibleUntagged(), -1);
		}
	}

	private void CommentsSync()
	{
		float num = TargetMessagesPerSecond;
		if (SlowModeEnabled)
		{
			num /= SlowModeMulti;
		}
		if (_messagesPerSecond < num)
		{
			float num2 = Mathf.Max((num * 2f - _messagesPerSecond) * _syncEvery - (float)_messageCountSinceLastUpdate, 0f);
			int num3 = Mathf.FloorToInt(num2);
			float num4 = num2 - (float)num3;
			if (num3 > 0 || num4 > 0.01f)
			{
				List<InteractionMemoryEventEnum> recentSimpleMemories;
				using (FrickState.Brain.InteractionMemory.GetRecentSimpleMemories(out recentSimpleMemories))
				{
					for (int i = 0; i < num3; i++)
					{
						GenerateRandomComment(recentSimpleMemories);
					}
					if (num4 > 0.01f && UnityEngine.Random.value < num4)
					{
						GenerateRandomComment(recentSimpleMemories);
					}
				}
			}
		}
		float value = TargetMessagesPerSecond * _syncEvery;
		TargetDonations += gv.GetGameStats().StreamGameStats.streamDonationBoostPercent.CalculateBoosted(value);
		if (!(TargetDonations > 0f) || _viewerNumber <= 0)
		{
			return;
		}
		int num5 = gv.time - LastDonation;
		if (num5 > 0)
		{
			float num6 = 0.15f * (float)num5;
			num6 *= Mathf.Min(2f, 1f + Mathf.Log(1f + (float)_viewerNumber / 100f));
			if (UnityEngine.Random.Range(0f, 1f) < num6)
			{
				AddInteraction(SingletonLoadableMonoBehaviour<DialogueChainSingleton>.Instance.Donations.GetRandomPossibleUntagged(), 1);
			}
		}
	}

	private void SyncGui()
	{
		_viewersInfo.text = _viewerNumber.ToString();
		_subsInfo.text = gv.subs.ToString();
		_followersInfo.text = gv.followers.ToString();
	}

	private void SyncViewers(float cumScore)
	{
		GameVariables gameVariables = gv;
		int num = gameVariables.GetGameStats().StreamGameStats.streamViewerBoostPercent.CalculateBoostedInt(_targetViewerNumberRaw);
		_viewerNumber = GlobalFunctions.MoveTowards(_viewerNumber, num, UnityEngine.Random.Range(0, Mathf.RoundToInt(Mathf.Log(Mathf.Max(num, _viewerNumber)))), 10);
		int a = Mathf.CeilToInt((float)_viewerNumber * (1f / 3f));
		int b = Mathf.CeilToInt((float)gameVariables.followers * 0.8f);
		int num2 = Mathf.Min(a, b);
		if (num2 < 5)
		{
			num2 = 5;
		}
		int num3 = Mathf.Max(Mathf.CeilToInt((float)num2 / 100f), 2);
		if (num2 >= gv.subs)
		{
			float value = UnityEngine.Random.value;
			if (value < 0.2f)
			{
				int num4 = UnityEngine.Random.Range(1, 2 + num3);
				gv.subs += num4;
				SubsGained += num4;
			}
			else if (value < 0.5f)
			{
				int num5 = UnityEngine.Random.Range(2, 4 + num3);
				gv.subs += num5;
				SubsGained += num5;
			}
		}
		else
		{
			float value2 = UnityEngine.Random.value;
			if (value2 < 0.01f)
			{
				int num6 = UnityEngine.Random.Range(1, num3);
				gv.subs -= num6;
				SubsLost += num6;
			}
			else if (value2 < 0.02f)
			{
				int num7 = UnityEngine.Random.Range(1, 2);
				gv.subs -= num7;
				SubsLost += num7;
			}
		}
		gv.subs = Mathf.Max(gv.subs, 0);
		float num8 = (CurrentTagScore() + cumScore * 0.15f) * _streamBonus;
		if (_streamMood > 0.01f)
		{
			_streamMood -= 0.01f;
		}
		else if (_streamMood < -0.01f)
		{
			_streamMood += 0.0025f;
		}
		else
		{
			_streamMood = 0f;
		}
		num8 += (float)Math.Tanh(_streamMood) / 4f;
		int subs = gameVariables.subs;
		int followers = gameVariables.followers;
		if (UnityEngine.Random.value <= 0.4f)
		{
			if (_streamMood < 0f && (double)UnityEngine.Random.value <= 0.1 - (double)_streamMood)
			{
				_targetViewerNumberRaw -= UnityEngine.Random.Range(1, 2);
			}
			if (UnityEngine.Random.Range(0f, 2f) <= num8)
			{
				_targetViewerNumberRaw += UnityEngine.Random.Range(1, 2);
			}
			if (_targetViewerNumberRaw < 10 + subs + followers)
			{
				_targetViewerNumberRaw += ((UnityEngine.Random.value < 0.49f + num8) ? UnityEngine.Random.Range(1, 3) : UnityEngine.Random.Range(-3, 0));
			}
			else if (_targetViewerNumberRaw < 50 + subs + followers)
			{
				_targetViewerNumberRaw += ((UnityEngine.Random.value < 0.45f + num8) ? UnityEngine.Random.Range(1, 3) : UnityEngine.Random.Range(-4, 0));
			}
			else if (_targetViewerNumberRaw < 500 + subs + followers)
			{
				_targetViewerNumberRaw += ((UnityEngine.Random.value < 0.35f + num8) ? UnityEngine.Random.Range(1, 5) : UnityEngine.Random.Range(-6, 0));
			}
			else if (_targetViewerNumberRaw < 4000 + subs + followers)
			{
				_targetViewerNumberRaw += ((UnityEngine.Random.value < 0.1f + num8) ? UnityEngine.Random.Range(1, 50) : UnityEngine.Random.Range(-22, 0));
			}
			else
			{
				_targetViewerNumberRaw += ((UnityEngine.Random.value < num8) ? UnityEngine.Random.Range(1, 100) : UnityEngine.Random.Range(-101, 0));
			}
		}
		if (UnityEngine.Random.value <= 0.05f)
		{
			int num9 = (int)Mathf.Sqrt(_viewerNumber) + _viewerNumber / 4;
			if (num9 > followers)
			{
				gameVariables.followers++;
				if (UnityEngine.Random.Range(0f, 1f) <= 0.1f)
				{
					_targetViewerNumberRaw--;
				}
			}
			else if (num9 < followers)
			{
				_targetViewerNumberRaw++;
				if (UnityEngine.Random.Range(0f, 1f) <= 0.01f)
				{
					gameVariables.followers--;
				}
			}
			if (_viewerNumber > 10 && UnityEngine.Random.Range(0f, 2f) <= num8)
			{
				gameVariables.followers += UnityEngine.Random.Range(1, 3);
			}
			if (gameVariables.followers < 0)
			{
				gameVariables.followers = 0;
			}
		}
		if (_targetViewerNumberRaw < 0)
		{
			_targetViewerNumberRaw = 0;
		}
		if (_viewerNumber < 0)
		{
			_viewerNumber = 0;
		}
	}
}
