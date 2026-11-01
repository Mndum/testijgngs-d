using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Fungus;
using GameItems;
using IC.AutomaticReference;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat;
using UnityEngine.Localization.SmartFormat.Core.Parsing;
using UnityEngine.Localization.SmartFormat.Core.Settings;
using UnityEngine.UI;

public class GameScript : SingletonLoadableMonoBehaviour<GameScript>
{
	public class CreditsState : ResultGameState
	{
		private class CreditsLine
		{
			public RectTransform rt { get; }

			public TextMeshProUGUI tmp { get; }

			public GameObject gameObject => rt.gameObject;

			public CreditsLine(RectTransform rt, TextMeshProUGUI tmp)
			{
				this.rt = rt;
				this.tmp = tmp;
			}

			public void SetText(string text, float yPosition, float width)
			{
				tmp.text = text;
				rt.SetParent(UiOverlay.Instance.GuiTransform, worldPositionStays: false);
				RectTransform rectTransform = rt;
				Vector2 anchorMin = (rt.anchorMax = new Vector2(0.5f, 0f));
				rectTransform.anchorMin = anchorMin;
				rt.pivot = new Vector2(0.5f, 1f);
				rt.localScale = Vector3.one;
				rt.anchoredPosition3D = Vector3.zero;
				rt.sizeDelta = new Vector2(width, 0f);
				tmp.ForceMeshUpdate();
				rt.sizeDelta = new Vector2(width, Mathf.Max(tmp.preferredHeight, 32.52f));
				rt.anchoredPosition = new Vector2(0f, yPosition);
			}
		}

		private RectTransform guiTransform;

		private Rect guiSize;

		private float speedRatio = 1f;

		private bool fastForward;

		private CancellationTokenSource cts;

		public string EndingId { get; set; }

		private IEnumerator<string> GenerateAllCreditLines()
		{
			TextAsset textAsset = R.CreditsText.Credits.Get();
			TextAsset textAsset2 = R.CreditsText.Credits2.Get();
			TextAsset textAssetTranslation = R.CreditsText.CreditsTranslation.Get();
			TextAsset textAsset3 = R.CreditsText.Credits3.Get();
			if (textAsset == null || textAsset2 == null || textAsset3 == null)
			{
				yield return "Error! Credit files broken!";
				yield break;
			}
			yield return "<b>Factorial Omega: My Dystopian Robot Girlfriend " + Application.version + "</b>";
			string[] array = textAsset.text.Split('\n');
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				yield return array2[i];
			}
			foreach (string item in SingletonScriptableObject<MusicData>.Instance.GenerateCredits())
			{
				yield return item;
			}
			for (int i = 0; i < 15; i++)
			{
				yield return "\n";
			}
			speedRatio = 1.5f;
			string[] array3 = textAsset2.text.Split('\n');
			array2 = array3;
			for (int i = 0; i < array2.Length; i++)
			{
				yield return array2[i];
			}
			for (int i = 0; i < 15; i++)
			{
				yield return "\n";
			}
			speedRatio = 1f;
			yield return "\n\n";
			string[] array4 = textAssetTranslation.text.Split('\n');
			array2 = array4;
			for (int i = 0; i < array2.Length; i++)
			{
				yield return array2[i];
			}
			string[] array5 = textAsset3.text.Split('\n');
			array2 = array5;
			for (int i = 0; i < array2.Length; i++)
			{
				yield return array2[i];
			}
			for (int i = 0; i < 15; i++)
			{
				yield return "\n";
			}
			Achievement achievement = AchievementSingleton.AllAchievements.FirstOrDefault((Achievement x) => x.Id == EndingId);
			if (achievement != null && achievement.AchievementType == AchievementTypeEnum.MainEnding_Good)
			{
				for (int i = 0; i < 20; i++)
				{
					yield return "\n";
				}
				fastForward = false;
				speedRatio = 0.7f;
				yield return "and of course...";
				for (int i = 0; i < 30; i++)
				{
					yield return "\n";
				}
				yield return "biggest thanks goes to someone very important...";
				for (int i = 0; i < 30; i++)
				{
					yield return "\n";
				}
				yield return "you";
				for (int i = 0; i < 30; i++)
				{
					yield return "\n";
				}
				yield return "Without you this story would have never been told.";
			}
			else
			{
				yield return "and you :)";
			}
			for (int i = 0; i < 30; i++)
			{
				yield return "\n";
			}
			yield return "My Dystopian Robot Girlfriend";
			yield return $"By Incontinent Cell 2020-{DateTime.Now.Year}©";
		}

		public override void EnterState()
		{
			guiTransform = UiOverlay.Instance.GuiTransform;
			guiSize = guiTransform.rect;
			cts = new CancellationTokenSource();
			RunCreditsFlowAsync(cts.Token).Forget();
		}

		public void KillAndReportResult()
		{
			if (cts != null)
			{
				cts?.Cancel();
				cts?.Dispose();
				cts = null;
				ReportResult();
			}
		}

		public override void ExitState()
		{
			KillAndReportResult();
		}

		private async UniTask RunCreditsFlowAsync(CancellationToken cancellationToken)
		{
			_ = 1;
			try
			{
				Achievement achievement = AchievementSingleton.AllAchievements.FirstOrDefault((Achievement x) => x.Id == EndingId);
				if (achievement != null)
				{
					await ShowLogoAndEndingNameAsync(achievement, cancellationToken);
				}
				if (!cancellationToken.IsCancellationRequested)
				{
					await ShowAllCreditsAsync(cancellationToken);
					if (!cancellationToken.IsCancellationRequested)
					{
						KillAndReportResult();
					}
				}
			}
			catch (OperationCanceledException)
			{
			}
			catch (Exception exception)
			{
				Debug.LogError("Credits flow error");
				Debug.LogException(exception);
			}
		}

		private CreditsLine CreateNewLine()
		{
			TextMeshProUGUI andInstantiate = R.TextPrefabs.Noto_Sans_NotoSans_Text.GetAndInstantiate();
			andInstantiate.name = "CreditsLine";
			TextMeshProUGUI component = andInstantiate.GetComponent<TextMeshProUGUI>();
			component.alignment = TextAlignmentOptions.Center;
			component.fontSize = 30f;
			component.raycastTarget = false;
			component.color = Color.white;
			return new CreditsLine(andInstantiate.GetComponent<RectTransform>(), component);
		}

		private async UniTask ShowLogoAndEndingNameAsync(Achievement endingAchievement, CancellationToken ct)
		{
			await UniTask.WaitForSeconds(1, ignoreTimeScale: false, PlayerLoopTiming.Update, ct);
			Sprite sprite = R.GameLogo.Logo_C_small.Get();
			GameObject logoGo = new GameObject("CreditsLogo");
			logoGo.transform.SetParent(UiOverlay.Instance.GuiTransform, worldPositionStays: false);
			Image image = logoGo.AddComponent<Image>();
			image.sprite = sprite;
			image.preserveAspect = true;
			image.color = new Color(1f, 1f, 1f, 0f);
			RectTransform component = logoGo.GetComponent<RectTransform>();
			Vector2 anchorMin = (component.anchorMax = new Vector2(0.5f, 0.5f));
			component.anchorMin = anchorMin;
			component.pivot = new Vector2(0.5f, 0.5f);
			component.anchoredPosition = new Vector2(0f, 160f);
			component.sizeDelta = new Vector2(500f, 254f);
			component.localScale = 1.4f.ToVector3();
			TextMeshProUGUI endingText = R.TextPrefabs.Noto_Sans_NotoSans_Text.GetAndInstantiate();
			endingText.transform.SetParent(UiOverlay.Instance.GuiTransform, worldPositionStays: false);
			endingText.alignment = TextAlignmentOptions.Center;
			endingText.fontSize = 68f;
			endingText.color = new Color(1f, 1f, 1f, 0f);
			RectTransform orAddComponent = endingText.gameObject.GetOrAddComponent<RectTransform>();
			anchorMin = (orAddComponent.anchorMax = new Vector2(0.5f, 0.5f));
			orAddComponent.anchorMin = anchorMin;
			orAddComponent.pivot = new Vector2(0.5f, 0.5f);
			orAddComponent.anchoredPosition = new Vector2(0f, -300f);
			orAddComponent.sizeDelta = new Vector2(1200f, 300f);
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.TMPAppendDoubleGoldenRatioSmallSizeTag();
				stringBuilder.AppendLine(LOC.UI.Credits_GoodEndingAchieved);
				stringBuilder.TMPAppendCloseSizeTag();
				stringBuilder.AppendLine(endingAchievement.Name);
				endingText.SetText(stringBuilder);
			}
			Sequence sequence = DOTween.Sequence();
			try
			{
				sequence.Append(image.DOFade(1f, 1.9f).SetEase(Ease.OutQuad));
				sequence.Join(endingText.DOFade(1f, 1.7f).SetEase(Ease.OutQuad));
				sequence.Join(orAddComponent.DOScale(1.08f, 0.9f).From(0.92f).SetEase(Ease.OutBack));
				sequence.AppendInterval(3.2f);
				sequence.Append(DOTween.Sequence().Join(image.DOFade(0f, 1.1f).SetEase(Ease.InQuad)).Join(endingText.DOFade(0f, 1.1f).SetEase(Ease.InQuad)));
				sequence.Join(component.DOAnchorPosY(component.anchoredPosition.y + 40f, 1.1f));
				sequence.Join(orAddComponent.DOAnchorPosY(orAddComponent.anchoredPosition.y - 40f, 1.1f));
				await sequence.ToUniTask(TweenCancelBehaviour.Kill, ct);
			}
			finally
			{
				if (logoGo != null)
				{
					UnityEngine.Object.Destroy(logoGo);
				}
				if (endingText.gameObject != null)
				{
					UnityEngine.Object.Destroy(endingText.gameObject);
				}
			}
		}

		private async UniTask ShowAllCreditsAsync(CancellationToken ct)
		{
			LinkedList<CreditsLine> currentLines = new LinkedList<CreditsLine>();
			IEnumerator<string> creditsEnumerator = GenerateAllCreditLines();
			bool finishedShowingStrings = false;
			try
			{
				while (!ct.IsCancellationRequested)
				{
					await UniTask.Yield(PlayerLoopTiming.Update, ct);
					float num = (fastForward ? 2000f : 100f) * speedRatio * Time.deltaTime;
					foreach (CreditsLine item in currentLines)
					{
						Vector2 anchoredPosition = item.rt.anchoredPosition;
						anchoredPosition.y += num;
						item.rt.anchoredPosition = anchoredPosition;
					}
					if (finishedShowingStrings)
					{
						CreditsLine creditsLine = currentLines.Last?.Value;
						if (creditsLine != null && creditsLine.rt.anchoredPosition.y - creditsLine.rt.sizeDelta.y > guiSize.height)
						{
							break;
						}
						continue;
					}
					while (true)
					{
						CreditsLine creditsLine2 = currentLines.Last?.Value;
						if ((creditsLine2 == null || !(creditsLine2.rt.anchoredPosition.y - creditsLine2.rt.sizeDelta.y > 0f)) && currentLines.Count != 0)
						{
							break;
						}
						if (creditsEnumerator.MoveNext())
						{
							AddNewLine(creditsEnumerator.Current);
							continue;
						}
						finishedShowingStrings = true;
						break;
					}
				}
			}
			finally
			{
				foreach (CreditsLine item2 in currentLines)
				{
					UnityEngine.Object.Destroy(item2.gameObject);
				}
				currentLines.Clear();
			}
			void AddNewLine(string text)
			{
				CreditsLine creditsLine3 = currentLines.First?.Value;
				CreditsLine creditsLine4 = currentLines.Last?.Value;
				CreditsLine creditsLine5;
				if (creditsLine3 != null && creditsLine3.rt.anchoredPosition.y - creditsLine3.rt.sizeDelta.y > guiSize.height)
				{
					creditsLine5 = creditsLine3;
					currentLines.RemoveFirst();
				}
				else
				{
					creditsLine5 = CreateNewLine();
				}
				if (creditsLine4 != null)
				{
					creditsLine5.SetText(text, creditsLine4.rt.anchoredPosition.y - creditsLine4.rt.sizeDelta.y, guiSize.width);
				}
				else
				{
					creditsLine5.SetText(text, 0f, guiSize.width);
				}
				currentLines.AddLast(creditsLine5);
			}
		}

		public override void UpdateState()
		{
			if (cts == null || cts.IsCancellationRequested)
			{
				return;
			}
			if (fastForward)
			{
				if (!Input.anyKey)
				{
					fastForward = false;
				}
			}
			else if (Input.anyKeyDown)
			{
				fastForward = true;
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				KillAndReportResult();
			}
		}

		public override void FixedUpdateState()
		{
		}

		protected override void DefaultOnResult()
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryState();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToMainMenu();
		}
	}

	public class EquipmentState : GameStateWithLive2D<Live2DInteractionController>
	{
		private class RandomClothesApplier
		{
			public Func<Item.ItemManager, bool> ApplyFunc;

			public int TimesLeft;

			public RandomClothesApplier(Func<Item.ItemManager, bool> applyFunc, int timesLeft = 1)
			{
				ApplyFunc = applyFunc;
				TimesLeft = timesLeft;
			}
		}

		private EquipmentStaticGuiStateEnum _startState;

		public int _zoomLevel;

		public bool _positionLocked = true;

		private EquipmentStaticGui _equipmentStaticGui;

		private string[] genericLines = new string[1] { "..." };

		private static int genericDialogueCount = 24;

		private static string EquipmentPrefix = "Equipment_";

		private static string EquipmentGenericPrefix = EquipmentPrefix + "Generic_";

		[NonSerialized]
		private float lastComment;

		[NonSerialized]
		private Item lastItem;

		private ButtonListWrapper _lockButtonWrapper;

		protected override Live2DToolSelector TargetToolSelector => _equipmentStaticGui.ToolSelector;

		public EquipmentState(Live2DInteractionController live2DController, EquipmentStaticGuiStateEnum startState)
			: base(live2DController)
		{
			_startState = startState;
		}

		protected override ModelBrainState GetTargetBrainState()
		{
			return new EquipmentBrainState();
		}

		private void ZoomButtonClicked()
		{
			_zoomLevel = (_zoomLevel + 1) % 4;
			_equipmentStaticGui.FreeCamera.SetZoomLevel(_zoomLevel);
		}

		public override void EnterState()
		{
			GetAndEnableStaticGui<EquipmentStaticGui>(out _equipmentStaticGui);
			_equipmentStaticGui.Init(_startState);
			base.EnterState();
			_equipmentStaticGui.FreeCamera.InitBounds(SingletonMonoBehaviour<ViewSingleton>.Instance.RoomView);
			CurrentController.ParamBlanketSitting.SetPercent(0f);
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.OnItemEquipped += OnItemEquipped;
			genericLines = (from i in Enumerable.Range(0, genericDialogueCount)
				select LocalizationSettings.StringDatabase.GetLocalizedStringAsync(LOC.Other.TABLE_REFERENCE, EquipmentGenericPrefix + i, null, null, FallbackBehavior.UseProjectSettings, null).Result).ToArray();
			UpdateSideButtons();
			SyncLockButtonText();
		}

		public void OnItemEquipped(string slot, Item item)
		{
			if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("GoingOutside3") || item == lastItem || !(UnityEngine.Random.value < 0.2f) || lastComment + 2f > Time.time)
			{
				return;
			}
			lastItem = item;
			lastComment = Time.time;
			if (item.GetItemPrefab().Category != ItemCategory.Clothing)
			{
				return;
			}
			string text;
			if (item.GameId.IsVanilla)
			{
				switch (item.GetVanillaItemEnum())
				{
				default:
					text = null;
					break;
				case ItemEnum.PlainShirt:
					text = LOC.Other.Equipment_Shirt_0;
					break;
				case ItemEnum.StripedPanties:
					text = LOC.Other.Equipment_StripedPanties_0;
					break;
				case ItemEnum.StockingL:
				case ItemEnum.StockingR:
					text = LOC.Other.Equipment_Stocking_0;
					break;
				case ItemEnum.LingeriePanties:
				case ItemEnum.LingerieStockingL:
				case ItemEnum.LingerieStockingR:
				case ItemEnum.LingerieBra:
					text = LOC.Other.Equipment_Lingerie_0;
					break;
				case ItemEnum.LongSockL:
				case ItemEnum.LongSockR:
					text = LOC.Other.Equipment_LongSock_0;
					break;
				case ItemEnum.CuteBow:
					text = LOC.Other.Equipment_CuteBow_0;
					break;
				case ItemEnum.PlainBra:
					text = LOC.Other.Equipment_PlainBra_0;
					break;
				case ItemEnum.PleatedSkirt:
					text = ((UnityEngine.Random.value < 0.5f) ? LOC.Other.Equipment_PleatedSkirt_0 : LOC.Other.Equipment_PleatedSkirt_1);
					break;
				case ItemEnum.PlainSneakerL:
				case ItemEnum.PlainSneakerR:
					text = LOC.Other.Equipment_PlainSneakers_0;
					break;
				case ItemEnum.SneakerL:
				case ItemEnum.SneakerR:
					text = LOC.Other.Equipment_Sneakers_0;
					break;
				}
				if (UnityEngine.Random.value > 0.5f && slot == ItemSlot.SlotManager.Panties.Id)
				{
					text = ((UnityEngine.Random.value < 0.5f) ? LOC.Other.Equipment_Panties_0 : LOC.Other.Equipment_Panties_1);
				}
			}
			else
			{
				text = null;
			}
			if (text == null)
			{
				text = genericLines.GetRandom();
			}
			if (Brain.CurrentState is EquipmentBrainState equipmentBrainState)
			{
				equipmentBrainState.SayComment(text);
			}
		}

		public override void ExitState()
		{
			_equipmentStaticGui.SideButtonList.ResetButtons();
			base.ExitState();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lastEquipmentAt = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.OnItemEquipped -= OnItemEquipped;
			SingletonMonoBehaviour<CursorSingleton>.Instance.ClearOverrides();
		}

		private void SyncLockButtonText()
		{
			_lockButtonWrapper.UpdateButtonText();
			if (_positionLocked)
			{
				Live2DToolSelector.gameObject.SetActive(value: true);
				return;
			}
			CurrentController.ChangeLayer(-1);
			Live2DToolSelector.gameObject.SetActive(value: false);
		}

		private void UpdateSideButtons()
		{
			IModificationPeriodButtonList modificationPeriod;
			using (_equipmentStaticGui.SideButtonList.DoInitialization(out modificationPeriod))
			{
				ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
				buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Equipment_Zoom);
				buttonListWrapper.OnClick = ZoomButtonClicked;
				_lockButtonWrapper = modificationPeriod.AddButton(10);
				_lockButtonWrapper.TextFunc = () => (!_positionLocked) ? LOC.UI.Equipment_LockPosition : LOC.UI.Equipment_UnlockPosition;
				_lockButtonWrapper.OnClick = LockButtonClicked;
				ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(20);
				buttonListWrapper2.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Undress);
				buttonListWrapper2.OnClick = UndressButtonClicked;
				ButtonListWrapper buttonListWrapper3 = modificationPeriod.AddButton(40);
				buttonListWrapper3.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Equipment_RandomOutfit);
				buttonListWrapper3.OnClick = RandomOutfitButtonClicked;
			}
		}

		private void UndressButtonClicked()
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.UndressPrompt, () =>
			{
				GameVariables.Current.itemManager.UnequipAllBotClothes();
			});
		}

		private void LockButtonClicked()
		{
			_positionLocked = !_positionLocked;
			_equipmentStaticGui.FreeCamera.StopMove();
			SyncLockButtonText();
		}

		private void RandomOutfitButtonClicked()
		{
			if (Input.GetKey(KeyCode.LeftControl))
			{
				DoRandomClothes();
			}
			else
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Equipment_RandomOutfitPrompt, DoRandomClothes);
			}
		}

		private void DoRandomClothes()
		{
			GameVariables.Current.itemManager.UnequipAllBotClothes();
			using (GameVariables.Current.itemManager.DelayCallbacks("RandomOutfitButtonClicked"))
			{
				Lottery<RandomClothesApplier> lottery = new Lottery<RandomClothesApplier>();
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.UpperBodyOuter), 3), UnityEngine.Random.Range(3f, 15f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.LowerBody), 2), UnityEngine.Random.Range(3f, 15f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.UpperBody), 2), UnityEngine.Random.Range(3f, 15f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.Panties)), UnityEngine.Random.Range(1f, 2f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.Bra)), UnityEngine.Random.Range(1f, 2f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.HairAccessoryL)), UnityEngine.Random.Range(0.1f, 2f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.NeckAccessory)), UnityEngine.Random.Range(0.1f, 2f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.Glasses)), UnityEngine.Random.Range(0.1f, 2f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.TongueAccessory)), UnityEngine.Random.Range(0.1f, 2f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.EyeMakeup), UnityEngine.Random.Range(1, 5)), UnityEngine.Random.Range(0.1f, 2f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.Septum)), UnityEngine.Random.Range(0.1f, 2f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.Lips)), UnityEngine.Random.Range(0.1f, 2f));
				lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.TryEquippingARandomItemAtSlot(ItemSlot.SlotManager.Manicure)), UnityEngine.Random.Range(0.1f, 2f));
				List<Item.ItemManager.ItemPairContainer> groups;
				using (GameVariables.Current.itemManager.GetAllPairedItems(out groups))
				{
					List<Item> allClothes;
					using (MyCollectionPool<Item>.GetListAndFill(from x in GameVariables.Current.itemManager.AllItemsEnumerable()
						where x.GetItemPrefab().Category == ItemCategory.Clothing
						select x, out allClothes))
					{
						if (groups.Count > 0)
						{
							lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) =>
							{
								if (groups[UnityEngine.Random.Range(0, groups.Count)].TryGetRandomPairMatchColor(out var i, out var i2))
								{
									itemManager.Equip(i, forceRemoveForbidden: false);
									itemManager.Equip(i2, forceRemoveForbidden: false);
									return true;
								}
								return false;
							}, UnityEngine.Random.Range(2, 5)), 5f);
							lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) =>
							{
								var (item, item2) = groups[UnityEngine.Random.Range(0, groups.Count)].GetRandomPair();
								itemManager.Equip(item, forceRemoveForbidden: false);
								itemManager.Equip(item2, forceRemoveForbidden: false);
								return true;
							}, UnityEngine.Random.Range(1, 4)), 1f);
						}
						if (allClothes.Count > 0)
						{
							lottery.AddItem(new RandomClothesApplier((Item.ItemManager itemManager) => itemManager.Equip(allClothes.GetRandom(), forceRemoveForbidden: false) == Item.ItemManager.EquipResults.Success, UnityEngine.Random.Range(1, 10)), UnityEngine.Random.Range(0.1f, 4f));
						}
						int num = UnityEngine.Random.Range(2, 20);
						for (int num2 = 0; num2 < num; num2++)
						{
							if (lottery.Empty)
							{
								break;
							}
							var (randomClothesApplier, index) = lottery.GetRandomWithIndex();
							randomClothesApplier.TimesLeft--;
							randomClothesApplier.ApplyFunc(GameVariables.Current.itemManager);
							if (randomClothesApplier.TimesLeft <= 0)
							{
								lottery.RemoveItemAtIndex(index);
							}
						}
					}
				}
			}
		}

		public override void UpdateState()
		{
			if (!UiOverlay.Instance.IsOpen && !_positionLocked)
			{
				_equipmentStaticGui.FreeCamera.UpdatePosition();
			}
		}

		public override void FixedUpdateState()
		{
		}
	}

	public class GameWithBotState : GameStateWithLive2D<Live2DInteractionController>
	{
		private GameStaticGui _gameStaticGui;

		private OldMaidGui _oldMaidGui;

		public GameWithBotState(Live2DInteractionController live2DController)
			: base(live2DController)
		{
		}

		protected override ModelBrainState GetTargetBrainState()
		{
			return new OldMaidBrainState(_oldMaidGui);
		}

		private void End()
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.StartInteract((InteractState s) =>
			{
				s.Brain.ChangeState(new NewInteractBrainState
				{
					AlreadyDidGreeting = true
				}, force: true);
			}, isZoom: false);
		}

		public override void EnterState()
		{
			GetAndEnableStaticGui<OldMaidGui>(out _oldMaidGui);
			GetAndEnableStaticGui<GameStaticGui>(out _gameStaticGui);
			base.EnterState();
			_oldMaidGui.StartGame((OldMaidBrainState)Brain.CurrentState, End);
		}

		public override void ExitState()
		{
			base.ExitState();
			_oldMaidGui.Clear();
		}

		public override void UpdateState()
		{
		}

		public override void FixedUpdateState()
		{
		}
	}

	public class MainMenuState : GameState
	{
		private MenuStaticGui _menuStaticGui;

		private int _clickCount;

		private float _lastClickTime;

		private const int ClickToEnterSecretRoom = 8;

		public override void EnterState()
		{
			GetAndEnableStaticGui<MenuStaticGui>(out _menuStaticGui);
			SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterAllMusic();
			SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption("MainMenuState", SoundSingleton.MusicOption.MusicOptionLayer.AboveBase, R.Music.MainMenu_Arabesque.Get()));
			SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
			SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption("MainMenuState", SoundSingleton.MusicOption.MusicOptionLayer.AboveBase, R.Music.MainMenu_Clair_de_lune.Get()));
			if (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform && (bool)OptionsStatic.ShowWebGLMobileWarning)
			{
				UiOverlay.Instance.Popup(LOC.Polyglot.MENU_LABEL_WARNING, LOC.UI.WebGLMobileWarning, new List<PopupChoice>
				{
					new PopupChoice(LOC.Polyglot.MENU_LABEL_OK),
					new PopupChoice(LOC.UI.UI_DontShowTheWarningAgain, () =>
					{
						OptionsStatic.ShowWebGLMobileWarning.SetValue(value: false);
					})
				});
			}
			if (SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.HasAnySaves && Application.platform == RuntimePlatform.WebGLPlayer && (bool)OptionsStatic.ShowWebGLSaveWarning)
			{
				UiOverlay.Instance.Popup(LOC.Polyglot.MENU_LABEL_WARNING, LOC.UI.WebGLWarningMightLoseSavePrompt, new List<PopupChoice>
				{
					new PopupChoice(LOC.Polyglot.MENU_LABEL_OK),
					new PopupChoice(LOC.UI.UI_DontShowTheWarningAgain, () =>
					{
						OptionsStatic.ShowWebGLSaveWarning.SetValue(value: false);
					})
				});
			}
			OptionsStatic.ShowShillingInfo();
			SingletonMonoBehaviour<ViewSingleton>.Instance.MenuScene.InccelBannerButton.onClick.SetListener(InccelButtonClicked);
		}

		private void EnterDeveloperRoom()
		{
			SingletonMonoBehaviour<FungusEnumerators>.Instance.StartDeveloperRoom().Forget();
		}

		private void InccelButtonClicked()
		{
			GlobalFunctions.ButtonClickingInARow_Click(ref _clickCount, ref _lastClickTime);
			if (_clickCount >= 8)
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.SecretRoomPrompt, EnterDeveloperRoom);
				_clickCount = 0;
			}
		}

		public override void ExitState()
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById("MainMenuState");
			SingletonMonoBehaviour<ViewSingleton>.Instance.MenuScene.InccelBannerButton.onClick.RemoveAllListeners();
		}

		public override void UpdateState()
		{
		}

		public override void FixedUpdateState()
		{
		}
	}

	public class PcState : GameState
	{
		private PCStaticGui _pcStaticGui;

		private CursorSingleton.CursorOverride _cursorOverride;

		public override void EnterState()
		{
			GetAndEnableStaticGui<PCStaticGui>(out _pcStaticGui);
		}

		public override void ExitState()
		{
			UiOverlay.Instance.CloseUiOverlay();
			CursorSingleton.EndCursorOverride(ref _cursorOverride);
		}

		private bool CanButtonBeClicked(Button button)
		{
			if (button.onClick != null)
			{
				if (button.onClick.GetListenerNumber() + button.onClick.GetPersistentEventCount() != 0)
				{
					return button.interactable;
				}
				return false;
			}
			return button.interactable;
		}

		private bool CanBeClicked(GameObject tmp)
		{
			if (tmp == null)
			{
				return false;
			}
			Button component = tmp.GetComponent<Button>();
			if (component != null)
			{
				return CanButtonBeClicked(component);
			}
			Transform parent = tmp.transform.parent;
			if (parent != null)
			{
				Button component2 = parent.GetComponent<Button>();
				if (component2 != null && CanButtonBeClicked(component2))
				{
					return true;
				}
			}
			return false;
		}

		public override void UpdateState()
		{
			if (SingletonMonoBehaviour<CursorSingleton>.Instance.CurrentType == CursorSingleton.CursorType.Wait)
			{
				return;
			}
			if (((CustomStandaloneInputModule)EventSystem.current.currentInputModule).TryGetPointerData(out var pointerEventData))
			{
				GameObject pointerEnter = pointerEventData.pointerEnter;
				if (pointerEnter == null)
				{
					CursorSingleton.EndCursorOverride(ref _cursorOverride);
					return;
				}
				SpecialCursor component = pointerEnter.GetComponent<SpecialCursor>();
				if (component != null)
				{
					CursorSingleton.ContinueOrStartCursorOverride(ref _cursorOverride, component.type);
				}
				else if (CanBeClicked(pointerEnter))
				{
					CursorSingleton.ContinueOrStartCursorOverride(ref _cursorOverride, CursorSingleton.CursorType.Link);
				}
				else
				{
					CursorSingleton.ContinueOrStartCursorOverride(ref _cursorOverride, CursorSingleton.CursorType.Normal);
				}
			}
			else
			{
				CursorSingleton.EndCursorOverride(ref _cursorOverride);
			}
		}

		public override void FixedUpdateState()
		{
			if (!UiOverlay.Instance.HasFungusOverlay())
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.SyncTime(suppressEvents: true);
			}
		}
	}

	public class RoomState : GameStateWithLive2D<Live2DInteractionController>
	{
		private GameStaticGui _gameStaticGui;

		private RoomStaticGui _roomStaticGui;

		public RoomState(Live2DInteractionController live2DController)
			: base(live2DController)
		{
		}

		protected override ModelBrainState GetTargetBrainState()
		{
			return new RoomBrainState();
		}

		public override void EnterState()
		{
			GetAndEnableStaticGui<GameStaticGui>(out _gameStaticGui);
			GetAndEnableStaticGui<RoomStaticGui>(out _roomStaticGui);
			base.EnterState();
			_gameStaticGui.SyncGui(GameVariables.Current);
			_roomStaticGui.UpdateInteractButtonPosition(CurrentController);
			GameVariables.Current.itemManager.MoveCarriedToHome();
		}

		public override void ExitState()
		{
			base.ExitState();
		}

		public override void UpdateState()
		{
			if (!UiOverlay.Instance.IsOpen)
			{
				_roomStaticGui.UpdateInteractButtonPosition(CurrentController);
			}
		}

		public override void FixedUpdateState()
		{
			if (!UiOverlay.Instance.IsOpen)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.SyncTime(suppressEvents: false);
			}
		}
	}

	public class StoryState : GameState
	{
		private GameStaticGui _gameStaticGui;

		private FungusBlocks.FungusBlockReference _targetBlock;

		private Action _onTargetBlockFinished;

		public StoryState(FungusBlocks.FungusBlockReference targetBlock, Action onTargetBlockFinished = null)
		{
			_targetBlock = targetBlock;
			_onTargetBlockFinished = onTargetBlockFinished;
		}

		public override void EnterState()
		{
			GetAndSetActiveStaticGui<GameStaticGui>(out _gameStaticGui, HasGameStarted);
			if (HasGameStarted)
			{
				_gameStaticGui.SyncGui(GameVariables.Current);
			}
			if (_targetBlock != null)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartBlock(_targetBlock.Name, _onTargetBlockFinished);
				_onTargetBlockFinished = null;
			}
		}

		public override void ExitState()
		{
		}

		public override void UpdateState()
		{
		}

		public override void FixedUpdateState()
		{
		}

		public override string ToString()
		{
			if (_targetBlock == null)
			{
				return "StoryState(Empty)";
			}
			return $"StoryState(Block \"{_targetBlock}\")";
		}
	}

	public class LoadProcedureData
	{
		public Action<GameVariables> BeforeLoad;

		public Action FinishedLoad;

		public Action AfterClosedOverlay;
	}

	[Header("Basic")]
	[SerializeField]
	private Flowchart _flowchart;

	[Header("Main")]
	[SerializeReference]
	private GameVariables _gameVariables;

	private float _minuteInSeconds = 2f;

	private GameStateMachine _gameStateMachine;

	private float _timer;

	private GameStaticGui _gameStaticGui;

	public Flowchart Flowchart => _flowchart;

	public GameVariables GameVariables => _gameVariables;

	public static bool HasGameStarted { get; private set; }

	public GameState GameState => _gameStateMachine.CurrentState;

	public GameStaticGui GameStaticGui => _gameStaticGui;

	public event Action OnMinutePassed;

	public event Action<GameState> OnChangeState;

	public T GetGameVariableValue<T>(string name)
	{
		if (GameVariables.ClassDescription.GetMembers<T>().TryGetValue(_gameVariables, name, out var value))
		{
			return value;
		}
		Debug.LogError("Tried to get property/field \"" + name + "\" from GameVariables! No such property!");
		return default;
	}

	public void SetGameVariableValue<T>(string name, T value)
	{
		if (!GameVariables.ClassDescription.GetMembers<T>().SetValue(_gameVariables, name, value))
		{
			Debug.LogError("Tried to set property/field \"" + name + "\" from GameVariables! No such property!");
		}
	}

	public void StartPrologue()
	{
		ChangeToStoryStateBlockReference(FungusBlocks.Prologue_Start);
	}

	public void StartNewGame()
	{
		StartNewGame(new LoadProcedureData());
	}

	public void StartNewGame(LoadProcedureData lpd)
	{
		if (lpd == null)
		{
			lpd = GenerateDefaultLPD();
		}
		lpd.BeforeLoad = (GameVariables gv) =>
		{
			gv.NewGameInitialization();
		};
		LoadGame(GameVariables.CreateStartOfTheGame(GameVariables.TimeFromDate(1, 21, 37), UnityEngine.Random.Range(50, 100)), lpd).ShowResultIfFail();
	}

	public void LoadDebugSave(LoadProcedureData lpd = null)
	{
		Debug.Log("Loading debug save!");
		SaveContainer.SaveSlotData saveSlotData = SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.GetSaveSlotReadonly(SaveContainer.SaveType.Manual).FirstOrDefault((SaveContainer.SaveSlotData t) => t.Notes.Equals("DEBUG", StringComparison.InvariantCultureIgnoreCase));
		if (saveSlotData != null)
		{
			saveSlotData.TryLoad(lpd).ShowResultIfFail();
			return;
		}
		Debug.LogError("Cannot find save with \"DEBUG\" as a note. Starting a new game!");
		StartNewGame(lpd);
	}

	private void DefaultAfterLoaded()
	{
		ChangeToStoryStateBlockReference(FungusBlocks.exclamationI_GameLoad);
		_gameStaticGui.gameObject.SetActive(value: false);
	}

	private LoadProcedureData GenerateDefaultLPD()
	{
		return new LoadProcedureData
		{
			FinishedLoad = DefaultAfterLoaded
		};
	}

	public Result LoadGame(GameVariables save, LoadProcedureData lpd = null)
	{
		if (lpd == null)
		{
			lpd = GenerateDefaultLPD();
		}
		EndGame();
		GameVariables gameVariables = _gameVariables;
		_gameVariables = save;
		Result result = _gameVariables.TransitionGameVersion(out var infoText);
		if (!result.IsSuccess)
		{
			_gameVariables = gameVariables;
			return result;
		}
		lpd.BeforeLoad?.Invoke(save);
		HasGameStarted = true;
		ModsSingleton.LuaEnvironment.Interpreter.Globals["gv"] = _gameVariables;
		_flowchart.SetStringVariable("playerName", _gameVariables.playerName);
		_flowchart.SetStringVariable("botName", _gameVariables.botName);
		_gameVariables.AfterLoad();
		lpd.FinishedLoad?.Invoke();
		UiOverlay.Instance.CloseUiOverlay();
		if (infoText != null)
		{
			UiOverlay.Instance.OkPopup(LOC.Polyglot.MENU_LABEL_WARNING, LOC.UI.NewSaveRecomendedPrompt0_90).sortOrder = 100;
			UiOverlay.Instance.SyncAllPopups();
		}
		lpd.AfterClosedOverlay?.Invoke();
		OptionsStatic.TimeStartCheckPoint();
		return true;
	}

	public void EndGame()
	{
		if (_gameVariables != null && HasGameStarted)
		{
			_gameVariables.BeforeUnload();
		}
		_gameVariables = null;
		HasGameStarted = false;
		ModelBrain.DeleteAllBrains();
	}

	public void ChangeToStoryState()
	{
		ChangeState(new StoryState(null));
	}

	public void ChangeToStoryStateBlockReference(FungusBlocks.FungusBlockReference fungusBlockReference, Action onTargetBlockComplete = null)
	{
		string text = fungusBlockReference.Name;
		if (_flowchart.TryGetBlock(text, out var _))
		{
			ChangeState(new StoryState(fungusBlockReference, onTargetBlockComplete));
		}
		else
		{
			Debug.LogError($"BLOCK \"{text}\" Length:{text.Length} NOT FOUND!!! Story state not entered");
		}
	}

	public void FungusStartBlock(string blockName, Action onComplete = null)
	{
		if (_flowchart.TryGetBlock(blockName, out var block))
		{
			_flowchart.ExecuteBlock(block, 0, onComplete);
			return;
		}
		Debug.LogError($"BLOCK \"{blockName}\" Length:{blockName.Length} NOT FOUND!!!");
		onComplete?.Invoke();
	}

	public MenuDialog FungusStartMenu(List<MenuOption> options, bool automaticUIOverlay = false)
	{
		MenuDialog menuDialog = MenuDialog.GetMenuDialog();
		if (menuDialog != null)
		{
			menuDialog.SetActive(state: true);
			if (options.Count == 0)
			{
				options.Add(new MenuOption
				{
					text = "No options available!"
				});
			}
			if (automaticUIOverlay)
			{
				UiOverlay.Instance.OpenFungusOverlay();
				foreach (MenuOption menuOption in options)
				{
					string text = _flowchart.SubstituteVariables(menuOption.text);
					menuDialog.AddOption(text, menuOption.interactable, menuOption.hideThisOption, menuOption.targetBlock, () =>
					{
						UiOverlay.Instance.CloseFungusOverlay();
						menuOption.beforeNextBlock?.Invoke();
					});
				}
			}
			else
			{
				foreach (MenuOption option in options)
				{
					string text2 = _flowchart.SubstituteVariables(option.text);
					menuDialog.AddOption(text2, option.interactable, option.hideThisOption, option.targetBlock, option.beforeNextBlock);
				}
			}
		}
		return menuDialog;
	}

	public void FungusSetCustomText(string text)
	{
		_flowchart.SetStringVariable("Text", text);
	}

	public void ShowText(string text)
	{
		UiOverlay.Instance.OpenFungusOverlay();
		StartCoroutine(SingletonMonoBehaviour<FungusEnumerators>.Instance.DoConversation(text).Then(UiOverlay.Instance.CloseFungusOverlay));
	}

	public void RollCredits()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.EndGame();
		ChangeState(new CreditsState());
	}

	public void GoOutside()
	{
		List<PopupChoice> list = new List<PopupChoice>
		{
			new PopupChoice
			{
				Action = () =>
				{
					ChangeToStoryStateBlockReference(FungusBlocks.Outside_OutsideStart);
				},
				Text = LOC.UI.Room_OutsideAlone
			}
		};
		if (GameVariables.HasTheOptionToGoOutsideWithBot)
		{
			list.Add(new PopupChoice
			{
				Action = () =>
				{
					ChangeToStoryStateBlockReference(FungusBlocks.Outside_OutsideStartWithBot);
				},
				Text = LOC.UI.Room_OutsideWithBot
			});
		}
		if (GameVariables.CheckFlag("Annalie_ending8") && !GameVariables.CheckFlag("Annalie_ending9"))
		{
			list.Add(new PopupChoice
			{
				Action = () =>
				{
					ChangeToStoryStateBlockReference(FungusBlocks.Ending_AnnalieESL9);
				},
				Text = LOC.UI.Room_LeaveTheCity
			});
		}
		if (GameVariables.CheckFlag("Landlord_ending6") && !GameVariables.CheckFlag("Landlord_ending7"))
		{
			list.Add(new PopupChoice
			{
				Action = () =>
				{
					ChangeToStoryStateBlockReference(FungusBlocks.Ending_LandlordESL7);
				},
				Text = LOC.UI.Room_LeaveTheCity
			});
		}
		list.Add(new PopupChoice
		{
			Action = ChangeToRoomState,
			Text = LOC.UI.Interact_Nevermind
		});
		UiOverlay.Instance.OpenContextMenuPopup(list, addCancel: false);
	}

	public void PrepareFricking()
	{
		ChangeToStoryStateBlockReference(FungusBlocks.Sex_PrepareFricking);
	}

	public void EndFricking()
	{
		if (TryGetStateAs<NewFrickState>(out var state))
		{
			state.DisableBrain();
		}
		ChangeToStoryStateBlockReference(FungusBlocks.Sex_End_Frick);
	}

	public void EndInteracting()
	{
		(_gameStateMachine.CurrentState as IGameStateWithLive2D)?.DisableBrain();
		ChangeToStoryState();
		SingletonMonoBehaviour<GameUtilities>.Instance.FadeOrMoveToView(SingletonMonoBehaviour<ViewSingleton>.Instance.RoomView, ChangeToRoomState);
	}

	public void StartInteract()
	{
		StartInteract(null, isZoom: false);
	}

	public void StartInteract(Action<InteractState> OnStarted, bool isZoom)
	{
		ChangeToStoryState();
		SingletonMonoBehaviour<GameUtilities>.Instance.FadeOrMoveToView(InteractState.GetInteractView(isZoom), () =>
		{
			InteractState interactState = new InteractState(SingletonMonoBehaviour<ViewSingleton>.Instance.MainRoomScene.GetBotCharacter());
			ChangeState(interactState);
			OnStarted?.Invoke(interactState);
		});
	}

	public void StartEquipment()
	{
		StartEquipment(EquipmentStaticGuiStateEnum.Equipment);
	}

	public void StartEquipmentClothes()
	{
		StartEquipment(EquipmentStaticGuiStateEnum.Clothes);
	}

	public void StartEquipment(EquipmentStaticGuiStateEnum targetState)
	{
		ChangeToStoryState();
		SingletonMonoBehaviour<GameUtilities>.Instance.FadeOrMoveToView(SingletonMonoBehaviour<ViewSingleton>.Instance.RoomCharacterViewEquipment, () =>
		{
			ChangeState(new EquipmentState(SingletonMonoBehaviour<ViewSingleton>.Instance.MainRoomScene.GetBotCharacter(), targetState));
		});
	}

	public void StartCooking()
	{
		ChangeToStoryState();
		SingletonMonoBehaviour<FungusEnumerators>.Instance.StartCooking().Forget();
	}

	public void StartBathroom()
	{
		ChangeToStoryStateBlockReference(FungusBlocks.Bathroom_Start);
	}

	public void StartFishing()
	{
		ChangeToStoryState();
		SingletonMonoBehaviour<FungusEnumerators>.Instance.StartFishing().Forget();
	}

	public void StartFishingGameplay(FishingStartInfo fishingStartInfo)
	{
		ChangeToStoryState();
		SingletonMonoBehaviour<FungusEnumerators>.Instance.StartFishingGameplay(fishingStartInfo).Forget();
	}

	public void GoHome()
	{
		ChangeToStoryStateBlockReference(FungusBlocks.Go_Home);
	}

	public void GoToSleep()
	{
		ChangeToStoryStateBlockReference(FungusBlocks.Sleep_Go_to_sleep);
	}

	public void ShowStatus()
	{
		if (Application.isEditor && Input.GetKey(KeyCode.D))
		{
			UiOverlay.Instance.InputPopup("Choose a block to go to!", "Choose a block to go to!", (string chosen) =>
			{
				if (_flowchart.TryGetBlock(chosen, out var _))
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.StartCoroutine(CoroutineUtilities.InvokeAfterDelay(() =>
					{
						ChangeState(new StoryState(null));
						FungusStartBlock(chosen);
					}, 1f));
				}
				else
				{
					Debug.LogError("\"" + chosen + "\" not found!");
				}
			}, FungusBlocks.Go_Home.Name);
		}
		else
		{
			ShowText(GameVariables.FullStatusText());
		}
	}

	public void StartPc()
	{
		ChangeToStoryStateBlockReference(FungusBlocks.StartPC);
	}

	public void ChangeToPcState()
	{
		ChangeState(new PcState());
	}

	public void ChangeToRoomState()
	{
		ChangeState(new RoomState(SingletonMonoBehaviour<ViewSingleton>.Instance.MainRoomScene.GetBotCharacter()));
		PassMinute(suppressEvents: false, GameVariables.StaminaSyncType.Normal, sound: true);
	}

	public void ChangeToMainMenu()
	{
		SingletonMonoBehaviour<FungusEnumerators>.Instance.StartMainMenu().Forget();
	}

	public void ReturnToMainMenuPrompt()
	{
		ChangeToStoryState();
		UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.UI_ReturnToMainMenuPrompt, ChangeToMainMenu, ChangeToRoomState);
	}

	public void OpenNarrativeLog()
	{
		UiOverlay.Instance.OpenNarrativeLog();
	}

	public void ChangeState(GameState newState)
	{
		OnChangeState?.Invoke(newState);
		_gameStateMachine.ChangeState(newState);
	}

	public async UniTask ChangeStateWaitTillEndUniTask(GameState newState)
	{
		UniTaskCompletionSource completionSource = new UniTaskCompletionSource();
		OnChangeState += OnAction;
		ChangeState(newState);
		try
		{
			await completionSource.Task;
		}
		finally
		{
			OnChangeState -= OnAction;
		}
		void OnAction(GameState x)
		{
			if (x != newState)
			{
				completionSource.TrySetResult();
			}
		}
	}

	public UniTask<T> WaitTillStatesResultOverrideDefault<T>(ResultGameState<T> newState)
	{
		UniTaskCompletionSource<T> completionSource = new UniTaskCompletionSource<T>();
		newState.OnResultOverride = (T x) =>
		{
			completionSource.TrySetResult(x);
		};
		ChangeState(newState);
		return completionSource.Task;
	}

	private void InitStateMachine()
	{
		_gameStateMachine = new GameStateMachine(new EmptyGameState());
	}

	public void PassMinute(bool suppressEvents, GameVariables.StaminaSyncType syncStamina, bool sound = false)
	{
		GameVariables.time++;
		_gameStaticGui.SyncGui(_gameVariables);
		OnMinutePassed?.Invoke();
		_timer = 0f;
		if (sound && GameVariables.time % 5 == 0)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.Play(R.Sound.Room_Clock_tick.Get(), UnityEngine.Random.Range(0.9f, 1.1f));
		}
		GameVariables.MinutePassed(syncStamina, suppressEvents);
	}

	public void SyncTime(bool suppressEvents, GameVariables.StaminaSyncType syncStamina = GameVariables.StaminaSyncType.Normal)
	{
		_timer += Time.fixedDeltaTime;
		if (_timer > _minuteInSeconds)
		{
			PassMinute(suppressEvents, syncStamina, sound: true);
		}
	}

	private async UniTask TimePassing(int minutes, Action finishAction, bool suppressEvents, GameVariables.StaminaSyncType syncStamina)
	{
		for (int i = 0; i < minutes; i++)
		{
			PassMinute(suppressEvents, syncStamina);
			if (i % 4 == 0)
			{
				await UniTask.Yield();
			}
		}
		finishAction?.Invoke();
	}

	public void StartPassTime(int minutes, Action finishAction, bool suppressEvents, GameVariables.StaminaSyncType syncStamina)
	{
		TimePassing(minutes, finishAction, suppressEvents, syncStamina).Forget();
	}

	public void PassTimeInstantly(int minutes, GameVariables.StaminaSyncType syncStamina)
	{
		for (int i = 0; i < minutes; i++)
		{
			PassMinute(suppressEvents: true, syncStamina);
		}
	}

	private async UniTask SleepTimePassing(int minutes, Action finishAction, bool suppressEvents, GameVariables.StaminaSyncType syncStamina)
	{
		for (int i = 0; i < minutes; i++)
		{
			PassMinute(suppressEvents, syncStamina);
			if (i % 6 == 0)
			{
				await UniTask.Yield();
			}
			if (GameVariables.Stamina >= 1f)
			{
				break;
			}
		}
		finishAction?.Invoke();
	}

	public void AddTime(int time)
	{
		GameVariables.time += time;
		_gameStaticGui.SyncGui(_gameVariables);
	}

	public void SetTime(int time)
	{
		GameVariables.time = time;
		_gameStaticGui.SyncGui(_gameVariables);
	}

	public void Sleep(int minutes, Action finishAction, bool suppressEvents, GameVariables.StaminaSyncType syncStamina)
	{
		SleepTimePassing(minutes, finishAction, suppressEvents, syncStamina).Forget();
	}

	public UniTask SleepUniTask(int minutes, Action finishAction, bool suppressEvents, GameVariables.StaminaSyncType syncStamina)
	{
		return SleepTimePassing(minutes, finishAction, suppressEvents, syncStamina);
	}

	private void OpenUtmLinkInner(string link, string utm_source)
	{
		UiOverlay.Instance.ShowFloatingTextAtMouse(LOC.UI.OpenedLink(link));
		Application.OpenURL(StringUtils.PrepareUtmLink(link, utm_source));
	}

	public void OpenPatreon(string utm_source)
	{
		OpenUtmLinkInner("https://www.patreon.com/inccel", utm_source);
	}

	public void OpenDirectSubscribePatreon(string utm_source)
	{
		OpenUtmLinkInner("https://www.patreon.com/join/inccel", utm_source);
	}

	public void OpenItchIORating(string utm_source)
	{
		OpenUtmLinkInner(SingletonMonoBehaviour<ItchIoSingleton>.Instance.GetItchGameLink() + "/rate", utm_source);
	}

	public void OpenSubscribeStar(string utm_source)
	{
		OpenUtmLinkInner("https://subscribestar.adult/incontinent-cell", utm_source);
	}

	public void OpenItchIO(string utm_source)
	{
		OpenUtmLinkInner(SingletonMonoBehaviour<ItchIoSingleton>.Instance.GetItchGameLink(), utm_source);
	}

	public void OpenBluesky(string utm_source)
	{
		OpenUtmLinkInner("https://bsky.app/profile/incontinentcell.bsky.social", utm_source);
	}

	public void OpenTwitter(string utm_source)
	{
		OpenUtmLinkInner("https://twitter.com/incontinentcell", utm_source);
	}

	public void OpenDiscord(string utm_source)
	{
		OpenUtmLinkInner("https://discord.com/invite/AXEpsfauHc", utm_source);
		UiOverlay.Instance.OkPopup("Discord", LOC.UI.DiscordNotExpired);
	}

	public int GetStreamLength()
	{
		if (_gameStateMachine.CurrentState is NewFrickState newFrickState)
		{
			return newFrickState.GetFrickLength();
		}
		return 0;
	}

	public int GetStreamViewers()
	{
		if (_gameStateMachine.CurrentState is NewFrickState newFrickState)
		{
			return newFrickState.GetStreamViewers();
		}
		return 0;
	}

	public bool TryGetStateAs<T>(out T state) where T : GameState
	{
		if (_gameStateMachine.CurrentState is T val)
		{
			state = val;
			return true;
		}
		state = null;
		return false;
	}

	public override Type[] GetDependenciesArray()
	{
		return new Type[12]
		{
			typeof(ConversationSingleton),
			typeof(AchievementSingleton),
			typeof(SaveSingleton),
			typeof(ModsSingleton),
			typeof(CookingSongPrefabManager),
			typeof(FishingLocationDataManager),
			typeof(ShopPrefabManager),
			typeof(StockCompanyPrefabManager),
			typeof(ItemPrefab.ItemPrefabManager),
			typeof(CocktractPartnerPrefabManager),
			typeof(ItemSlot.SlotManager),
			typeof(DialogueChainSingleton)
		};
	}

	public void StartupAfterLoad()
	{
		DebugSO debugSo = SingletonScriptableObject<DebugSO>.Instance;
		if (debugSo.IsRunningPlaytest || debugSo.DoAfterLoad == DebugSO.DebugDoAfterLoadEnum.MainMenu_NormalGameStart)
		{
			ChangeToMainMenu();
			return;
		}
		LoadProcedureData loadProcedureData = GenerateDefaultLPD();
		loadProcedureData.FinishedLoad = () =>
		{
			switch (debugSo.DoAfterLoad)
			{
			case DebugSO.DebugDoAfterLoadEnum.LoadDebugSave:
				DefaultAfterLoaded();
				break;
			case DebugSO.DebugDoAfterLoadEnum.Missionary:
				SingletonMonoBehaviour<FungusEnumerators>.Instance.StartSex(streamOn: false, SingletonMonoBehaviour<ViewSingleton>.Instance.MissionaryView).Forget();
				break;
			case DebugSO.DebugDoAfterLoadEnum.Cowgirl:
				SingletonMonoBehaviour<FungusEnumerators>.Instance.StartSex(streamOn: false, SingletonMonoBehaviour<ViewSingleton>.Instance.CowgirlView).Forget();
				break;
			case DebugSO.DebugDoAfterLoadEnum.Doggy:
				SingletonMonoBehaviour<FungusEnumerators>.Instance.StartSex(streamOn: false, SingletonMonoBehaviour<ViewSingleton>.Instance.DoggyRoomView).Forget();
				break;
			case DebugSO.DebugDoAfterLoadEnum.Blowjob:
				SingletonMonoBehaviour<FungusEnumerators>.Instance.StartSex(streamOn: false, SingletonMonoBehaviour<ViewSingleton>.Instance.BlowjobView).Forget();
				break;
			case DebugSO.DebugDoAfterLoadEnum.Shower:
				SingletonMonoBehaviour<GameUtilities>.Instance.FadeToView(SingletonMonoBehaviour<ViewSingleton>.Instance.ShowerView);
				break;
			case DebugSO.DebugDoAfterLoadEnum.BotLead:
				SingletonMonoBehaviour<FungusEnumerators>.Instance.StartBotFullLeadSex().Forget();
				break;
			case DebugSO.DebugDoAfterLoadEnum.Equipment:
				StartEquipment();
				break;
			case DebugSO.DebugDoAfterLoadEnum.PC:
			case DebugSO.DebugDoAfterLoadEnum.Browser:
			case DebugSO.DebugDoAfterLoadEnum.BotStatusApp:
				SingletonLoadableMonoBehaviour<GameScript>.Instance.StartPc();
				break;
			case DebugSO.DebugDoAfterLoadEnum.ClothierWithBot:
				ChangeToStoryStateBlockReference(FungusBlocks.Outside_Clothier_startWithBot);
				break;
			case DebugSO.DebugDoAfterLoadEnum.Cuddle:
				GameVariables.Current.AddFlag("SleepingWithBotUnlocked");
				SingletonMonoBehaviour<GameUtilities>.Instance.FadeToView(SingletonMonoBehaviour<ViewSingleton>.Instance.CuddleView);
				break;
			case DebugSO.DebugDoAfterLoadEnum.Cooking:
				StartCooking();
				break;
			case DebugSO.DebugDoAfterLoadEnum.Fishing:
				StartFishing();
				break;
			case DebugSO.DebugDoAfterLoadEnum.CockTrackts:
			case DebugSO.DebugDoAfterLoadEnum.GoOutsidePrompt:
				break;
			}
		};
		loadProcedureData.AfterClosedOverlay = () =>
		{
			switch (debugSo.DoAfterLoad)
			{
			case DebugSO.DebugDoAfterLoadEnum.GoOutsidePrompt:
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GoOutside();
				break;
			case DebugSO.DebugDoAfterLoadEnum.CockTrackts:
				UiOverlay.Instance.OpenBrowser("cocktracts.ic");
				GameVariables.Current.cocktractManager.GenerateAllContracts();
				break;
			case DebugSO.DebugDoAfterLoadEnum.Browser:
				UiOverlay.Instance.OpenBrowser(SingletonScriptableObject<DebugSO>.Instance.AfterLoadData);
				break;
			case DebugSO.DebugDoAfterLoadEnum.BotStatusApp:
				UiOverlay.Instance.OpenBotStatusApp();
				break;
			}
		};
		LoadDebugSave(loadProcedureData);
	}

	protected override UniTask LoadInner(IProgressManager progressManager)
	{
		OptionsStatic.SyncResolution();
		StaticUIBehaviour<ResizeEvent>.Instance.OnResize.AddListener(OptionsStatic.SyncLetterbox);
		return UniTask.CompletedTask;
	}

	private new void Awake()
	{
		base.Awake();
		DebugDefines.InitDefault();
	}

	private void Start()
	{
		Smart.Default.Settings.ParseErrorAction = ErrorAction.MaintainTokens;
		Smart.Default.Settings.FormatErrorAction = ErrorAction.MaintainTokens;
		Smart.Default.OnFormattingFailure += (object sender, FormattingErrorEventArgs args) =>
		{
			Debug.LogError($"{sender} {args.Placeholder} {args.ErrorIndex}");
		};
		Smart.Default.Parser.OnParsingFailure += (object sender, ParsingErrorEventArgs args) =>
		{
			Debug.LogError($"{sender} {args.Errors}");
		};
		OptionsStatic.LoadSettings();
		InitStateMachine();
		UiOverlay.Instance.GetStaticGUI<GameStaticGui>(out _gameStaticGui);
		UiOverlay.Instance.ProgressPopup("Loading!", "", LoadingManager.LoadAll, StartupAfterLoad);
	}

	public void TakeScreenshot()
	{
		string text = "Screen " + DateTime.Now.Ticks + ".png";
		string text2;
		string text3;
		switch (Application.platform)
		{
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXPlayer:
			text2 = Path.Combine(Application.dataPath, "..", "Screenshots");
			text3 = Path.Combine(text2, text);
			break;
		case RuntimePlatform.WindowsPlayer:
		case RuntimePlatform.WindowsEditor:
		case RuntimePlatform.LinuxPlayer:
		case RuntimePlatform.LinuxEditor:
			text2 = Path.Combine(Application.dataPath, "..", "Screenshots");
			text3 = Path.Combine(text2, text);
			break;
		case RuntimePlatform.IPhonePlayer:
		case RuntimePlatform.Android:
		case RuntimePlatform.WebGLPlayer:
		{
			byte[] bytes = ScreenCapture.CaptureScreenshotAsTexture().EncodeToPNG();
			SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.SaveToFile((MyFileBrowserSingleton.DownloadResult result) =>
			{
				Debug.Log($"Screenshot: {result}");
			}, bytes, text);
			return;
		}
		default:
			Debug.LogWarning($"Screenshots are not supported on this platform - {Application.platform}.");
			return;
		}
		if (!Directory.Exists(text2))
		{
			Directory.CreateDirectory(text2);
		}
		ScreenCapture.CaptureScreenshot(text3);
		if (Application.isEditor)
		{
			string fullPath = Path.GetFullPath(text3);
			Debug.Log(fullPath);
			Debug.Log("Screenshot saved to <a href=\"" + fullPath + "\">" + fullPath + "</a>");
		}
		else
		{
			Debug.Log("Screenshot saved to \"" + text3 + "\"");
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F) && !UiOverlay.Instance.IsEditingInputField() && !UiOverlay.Instance.TryGetTopPopupOfType<BotStatusAppPopup>(out var _))
		{
			OptionsStatic.ChangeFullscreen(!OptionsStatic.FullScreenPref);
		}
		if (Input.GetKeyDown(KeyCode.F12) && !UiOverlay.Instance.IsEditingInputField())
		{
			TakeScreenshot();
		}
		if (HasGameStarted)
		{
			if (Input.GetKey(KeyCode.S) && !UiOverlay.Instance.IsEditingInputField() && OptionsStatic.DebugEnabled)
			{
				GameVariables.Satiation = 1f;
				for (int i = 0; i < 10; i++)
				{
					PassMinute(suppressEvents: true, GameVariables.StaminaSyncType.Recovery, sound: true);
				}
			}
			if (Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.T) && !UiOverlay.Instance.IsEditingInputField() && OptionsStatic.DebugEnabled)
			{
				GameVariables.itemManager.ReloadTexturesInAllEquippedItems(null);
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Done, LOC.UI.ReloadedAllTexturesPrompt);
			}
		}
		_gameStateMachine.Update();
	}

	private void FixedUpdate()
	{
		_gameStateMachine.FixedUpdate();
		if (GameVariables != null)
		{
			_gameStaticGui.SyncMoney(GameVariables);
		}
	}

	public void CloseGame()
	{
		QuitHandler.TryQuitGame();
	}
}
