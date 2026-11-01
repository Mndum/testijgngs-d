using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SlotsWebsite : CasinoWebsiteCore
{
	[Serializable]
	public class SlotType
	{
		[SerializeField]
		public string name;

		[SerializeField]
		public Sprite sprite;

		[FormerlySerializedAs("winningMultiplier")]
		[SerializeField]
		public int tripleWinningMultiplier;

		[SerializeField]
		public int doubleWinningMultiplier;

		[SerializeField]
		public int singleWinningMultiplier;

		[SerializeField]
		public int rate;

		public int GetWinning(WinningType type)
		{
			return type switch
			{
				WinningType.Double => doubleWinningMultiplier, 
				WinningType.Single => singleWinningMultiplier, 
				_ => tripleWinningMultiplier, 
			};
		}
	}

	private class Lane
	{
		private static float yOffset = 400f;

		private SlotsWebsite slotsWebsite;

		private List<RectTransform> oldSlots;

		private AudioClip clickSound;

		private LinkedListNode<RectTransform> _lockedInslot;

		private RectTransform rectangle;

		private LinkedList<RectTransform> slots;

		private float speed;

		private static float acceleration = 3000f;

		private static float maxSpeed = 3000f;

		public bool IsLockedIn => _lockedInslot != null;

		public bool ReachedMaxSpeed => Mathf.Approximately(speed, maxSpeed);

		public RectTransform LockedInSlot => _lockedInslot.Value;

		public Lane(SlotsWebsite slotsWebsite, RectTransform rectangle, AudioClip clickSound)
		{
			this.slotsWebsite = slotsWebsite;
			this.rectangle = rectangle;
			this.clickSound = clickSound;
			slots = new LinkedList<RectTransform>();
			oldSlots = new List<RectTransform>();
			Fill();
			LockIn();
		}

		private RectTransform CreateSlot(Sprite sprite, float position)
		{
			RectTransform rectTransform;
			if (oldSlots.Any())
			{
				rectTransform = oldSlots.Last();
				oldSlots.RemoveAt(oldSlots.Count - 1);
				GameObject gameObject = rectTransform.gameObject;
				gameObject.name = sprite.name;
				rectTransform.anchoredPosition = new Vector2(0f, position);
				gameObject.GetComponent<Image>().sprite = sprite;
			}
			else
			{
				GameObject gameObject2 = new GameObject(sprite.name);
				rectTransform = gameObject2.AddComponent<RectTransform>();
				rectTransform.SetParent(rectangle, worldPositionStays: false);
				rectTransform.anchoredPosition = new Vector2(0f, position);
				rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
				gameObject2.AddComponent<Image>().sprite = sprite;
			}
			return rectTransform;
		}

		private void Fill()
		{
			float y = rectangle.sizeDelta.y;
			for (float num = 0f - yOffset; num < y + yOffset; num += (float)slotsWebsite.spriteHeight)
			{
				RectTransform value = CreateSlot(slotsWebsite.GetRandomSlotSprite(), num);
				slots.AddLast(value);
			}
		}

		private void MoveY(float move)
		{
			if (!slots.Any())
			{
				return;
			}
			foreach (RectTransform slot in slots)
			{
				Vector2 anchoredPosition = slot.anchoredPosition;
				anchoredPosition.y += move;
				slot.anchoredPosition = anchoredPosition;
			}
			float y = slots.First.Value.anchoredPosition.y;
			float y2 = slots.Last.Value.anchoredPosition.y;
			if (y > 0f - yOffset)
			{
				RectTransform value = CreateSlot(slotsWebsite.GetRandomSlotSprite(), y - (float)slotsWebsite.spriteHeight);
				slots.AddFirst(value);
				if (clickSound != null)
				{
					SingletonMonoBehaviour<SoundSingleton>.Instance.Play(clickSound, UnityEngine.Random.Range(0.9f, 1f));
				}
			}
			if (y2 > rectangle.sizeDelta.y + yOffset)
			{
				oldSlots.Add(slots.Last.Value);
				slots.RemoveLast();
			}
		}

		public void Update(float time)
		{
			if (IsLockedIn)
			{
				float num = rectangle.anchoredPosition.y / 2f;
				float y = LockedInSlot.anchoredPosition.y;
				if (!Mathf.Approximately(num - y, 0f))
				{
					float move = (num + y) / 2f - y;
					MoveY(move);
				}
			}
			else
			{
				MoveY(speed * time);
				speed += acceleration * time;
				if (speed > maxSpeed)
				{
					speed = maxSpeed;
				}
			}
		}

		public void LockIn()
		{
			float num = rectangle.anchoredPosition.y / 2f;
			for (LinkedListNode<RectTransform> linkedListNode = slots.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				RectTransform value = linkedListNode.Value;
				float num2 = slotsWebsite.spriteHeight;
				Vector2 anchoredPosition = value.anchoredPosition;
				float num3 = anchoredPosition.y - num2 / 2f;
				float num4 = anchoredPosition.y + num2 / 2f;
				if (num3 <= num && num4 >= num)
				{
					_lockedInslot = linkedListNode;
					break;
				}
			}
		}

		public void Restart()
		{
			_lockedInslot = null;
			speed = 0f;
		}

		public List<Sprite> GetLockedInAndSurroundingSlots()
		{
			return new List<Sprite>
			{
				_lockedInslot.Next.Value.GetComponent<Image>().sprite,
				_lockedInslot.Value.GetComponent<Image>().sprite,
				_lockedInslot.Previous.Value.GetComponent<Image>().sprite
			};
		}
	}

	public enum WinningType
	{
		Triple,
		Double,
		Single
	}

	private class SlotData
	{
		public SlotType SlotType;

		public bool Used;
	}

	[SerializeField]
	private int laneCount = 3;

	[SerializeField]
	private List<SlotType> slotTypes;

	[SerializeField]
	private AudioClip slotClick;

	[SerializeField]
	private int sumOfRates;

	[SerializeField]
	private int spriteWidth = 300;

	[SerializeField]
	private int spriteHeight = 300;

	private int _currentStake;

	private StakeSelector _stakeSelector;

	private RectTransform _laneContainer;

	private Button _interactButton;

	private TextMeshProUGUI _interactButtonText;

	private CasinoTextPopup _casinoTextPopup;

	private List<Lane> _lanes;

	private List<(string, int)> _winningsList = new List<(string, int)>();

	public void OnValidate()
	{
		sumOfRates = slotTypes.Aggregate(0, (int a, SlotType b) => a + b.rate);
	}

	private SlotType GetSlotTypeBySprite(Sprite sprite)
	{
		SlotType slotType = slotTypes.Find((SlotType t) => t.sprite == sprite);
		if (slotType == null)
		{
			Debug.Log("Slot type not found by sprite " + sprite.name + " returning first");
			return slotTypes[0];
		}
		return slotType;
	}

	private Sprite GetRandomSlotSprite()
	{
		int num = UnityEngine.Random.Range(0, sumOfRates);
		for (int i = 0; i < slotTypes.Count - 1; i++)
		{
			num -= slotTypes[i].rate;
			if (num < 0)
			{
				return slotTypes[i].sprite;
			}
		}
		return slotTypes[slotTypes.Count - 1].sprite;
	}

	public void CreateLanes()
	{
		_laneContainer.sizeDelta = new Vector2(laneCount * spriteWidth, 0f);
		_lanes = new List<Lane>();
		for (int i = 0; i < laneCount; i++)
		{
			RectTransform rectTransform = new GameObject("Lane " + i).AddComponent<RectTransform>();
			rectTransform.SetParent(_laneContainer, worldPositionStays: false);
			rectTransform.pivot = new Vector2(0.5f, 1f);
			rectTransform.sizeDelta = new Vector2(spriteWidth, 0f);
			_lanes.Add(new Lane(this, rectTransform, slotClick));
		}
	}

	private (string, int) CalculateWinnings(SlotType a, string nameOfRow, WinningType winningType)
	{
		return winningType switch
		{
			WinningType.Double => (LOC.UI.casinodotic_Double + " " + nameOfRow + " " + a.name.ToUpper(), a.doubleWinningMultiplier * _currentStake), 
			WinningType.Single => (LOC.UI.casinodotic_Single + " " + a.name.ToUpper(), a.singleWinningMultiplier * _currentStake), 
			_ => (LOC.UI.casinodotic_Triple + " " + nameOfRow + " " + a.name.ToUpper(), a.tripleWinningMultiplier * _currentStake), 
		};
	}

	private void CalculateRowWinnings(List<SlotData> c, string nameOfRow)
	{
		CalculateRowWinnings(c[0], c[1], c[2], nameOfRow);
	}

	private void CalculateSingleWinnings(SlotType a)
	{
		if (a.singleWinningMultiplier != 0)
		{
			_winningsList.Add(CalculateWinnings(a, "", WinningType.Single));
		}
	}

	private void CalculateRowWinnings(SlotData aa, SlotData bb, SlotData cc, string nameOfRow)
	{
		SlotType slotType = aa.SlotType;
		SlotType slotType2 = bb.SlotType;
		SlotType slotType3 = cc.SlotType;
		if (slotType == slotType2 && slotType2 == slotType3 && slotType.tripleWinningMultiplier != 0)
		{
			aa.Used = (bb.Used = (cc.Used = true));
			_winningsList.Add(CalculateWinnings(slotType, nameOfRow, WinningType.Triple));
		}
		else if (slotType == slotType2 && slotType.doubleWinningMultiplier != 0)
		{
			aa.Used = (bb.Used = true);
			_winningsList.Add(CalculateWinnings(slotType, nameOfRow, WinningType.Double));
		}
		else if (slotType2 == slotType3 && slotType3.doubleWinningMultiplier != 0)
		{
			bb.Used = (cc.Used = true);
			_winningsList.Add(CalculateWinnings(slotType2, nameOfRow, WinningType.Double));
		}
	}

	private void SetButtonText(bool ready)
	{
		if (ready)
		{
			_interactButtonText.text = LOC.UI.casinodotic_ClickToStopWheel;
		}
		else
		{
			_interactButtonText.text = LOC.UI.casinodotic_ClickToStopColumn;
		}
	}

	private void CalculateWinnings()
	{
		if (_lanes.Count != 3)
		{
			Debug.Log("Winnings implemented only for 3 lanes!");
			return;
		}
		List<List<SlotData>> list = new List<List<SlotData>>();
		for (int i = 0; i < 3; i++)
		{
			list.Add((from s in _lanes[i].GetLockedInAndSurroundingSlots()
				select new SlotData
				{
					SlotType = GetSlotTypeBySprite(s)
				}).ToList());
		}
		_winningsList.Clear();
		for (int num = 0; num < 3; num++)
		{
			CalculateRowWinnings(list[num], LOC.UI.casinodotic_Vertical);
		}
		for (int num2 = 0; num2 < 3; num2++)
		{
			CalculateRowWinnings(list[0][num2], list[1][num2], list[2][num2], LOC.UI.casinodotic_Horizontal);
		}
		CalculateRowWinnings(list[0][0], list[1][1], list[2][2], LOC.UI.casinodotic_Diagonal);
		CalculateRowWinnings(list[2][0], list[1][1], list[0][2], LOC.UI.casinodotic_Diagonal);
		for (int num3 = 0; num3 < 3; num3++)
		{
			for (int num4 = 0; num4 < 3; num4++)
			{
				if (!list[num4][num3].Used)
				{
					CalculateSingleWinnings(list[num4][num3].SlotType);
				}
			}
		}
		_casinoTextPopup.StartShowing(_winningsList);
		_stakeSelector.LockedIn = false;
	}

	private void ButtonClicked()
	{
		if (_casinoTextPopup.IsRunning)
		{
			_casinoTextPopup.Skip = true;
			return;
		}
		if (!_lanes[_lanes.Count - 1].IsLockedIn)
		{
			if (!_lanes[0].ReachedMaxSpeed)
			{
				return;
			}
			for (int i = 0; i < _lanes.Count; i++)
			{
				Lane lane = _lanes[i];
				if (!lane.IsLockedIn)
				{
					lane.LockIn();
					if (i == _lanes.Count - 1)
					{
						CalculateWinnings();
						SetButtonText(ready: true);
					}
					break;
				}
			}
			return;
		}
		_currentStake = _stakeSelector.GetStake();
		if (_currentStake > AvailableTokens || _currentStake < 1)
		{
			return;
		}
		_stakeSelector.LockedIn = true;
		AvailableTokens -= _currentStake;
		foreach (Lane lane2 in _lanes)
		{
			lane2.Restart();
		}
		SetButtonText(ready: false);
	}

	public void Awake()
	{
		_stakeSelector = transform.GetComponentInChildren<StakeSelector>();
		_laneContainer = transform.Find("Lane Container") as RectTransform;
		_interactButton = transform.Find("Interact Button").GetComponent<Button>();
	}

	public void Update()
	{
		foreach (Lane lane in _lanes)
		{
			lane.Update(Time.deltaTime);
		}
	}

	public override IEnumerator Open()
	{
		_interactButton.onClick.RemoveAllListeners();
		_interactButton.onClick.AddListener(ButtonClicked);
		_interactButtonText = _interactButton.GetComponentInChildren<TextMeshProUGUI>();
		_casinoTextPopup = gameObject.GetOrAddComponent<CasinoTextPopup>();
		SetButtonText(ready: true);
		CreateLanes();
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		yield return SlowlyEnableAllChildren(new List<Transform> { _casinoTextPopup.WinningsText.transform as RectTransform });
		BrowserPopup.FinishLoading();
	}
}
