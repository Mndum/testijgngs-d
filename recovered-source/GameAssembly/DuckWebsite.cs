using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DuckWebsite : Website
{
	private LinkedList<GameObject> quacks = new LinkedList<GameObject>();

	private float timer;

	private float duckHappiness;

	private float nextQuack = 2.5f;

	private bool isBeingHeadpatted;

	[SerializeField]
	private RectTransform imageRectTransform;

	private Image image;

	[SerializeField]
	private AudioClip quack;

	[SerializeField]
	private Sprite sadDuck;

	[SerializeField]
	private Sprite happyDuck;

	public void Start()
	{
		image = imageRectTransform.gameObject.GetComponent<Image>();
		EventTrigger orAddComponent = imageRectTransform.gameObject.GetOrAddComponent<EventTrigger>();
		orAddComponent.triggers.Clear();
		EventTrigger.Entry entry = new EventTrigger.Entry
		{
			eventID = EventTriggerType.BeginDrag
		};
		entry.callback.AddListener((BaseEventData data) =>
		{
			isBeingHeadpatted = true;
		});
		orAddComponent.triggers.Add(entry);
		EventTrigger.Entry entry2 = new EventTrigger.Entry
		{
			eventID = EventTriggerType.EndDrag
		};
		entry2.callback.AddListener((BaseEventData data) =>
		{
			isBeingHeadpatted = false;
		});
		orAddComponent.triggers.Add(entry2);
	}

	public void FixedUpdate()
	{
		Vector2 pivot = imageRectTransform.pivot;
		pivot.y = (pivot.y + 0.5f) / 2f;
		if (duckHappiness <= 1.5f)
		{
			if (isBeingHeadpatted)
			{
				duckHappiness += Time.fixedDeltaTime;
			}
			else
			{
				duckHappiness = 0f;
			}
			image.sprite = sadDuck;
			timer += Time.fixedDeltaTime;
			if (timer >= nextQuack)
			{
				GameObject gameObject = new GameObject("quack");
				RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
				rectTransform.SetParent(transform, worldPositionStays: false);
				Vector2 vector = (rectTransform.pivot = new Vector2(Random.value, Random.value));
				Vector2 anchorMax = (rectTransform.anchorMin = vector);
				rectTransform.anchorMax = anchorMax;
				rectTransform.rotation = Quaternion.Euler(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(0f, 360f));
				TextMeshProUGUI textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
				textMeshProUGUI.text = ((Random.value < 0.5f) ? LOC.UI.duckdotic_Quack : LOC.UI.duckdotic_QUACKexclamationexclamationexclamation);
				textMeshProUGUI.color = Color.black;
				SingletonMonoBehaviour<SoundSingleton>.Instance.Play(quack, Random.Range(0.9f, 1.1f));
				pivot.y = 0.4f;
				quacks.AddLast(gameObject);
				if (quacks.Count > 50)
				{
					Object.Destroy(quacks.First.Value);
					quacks.RemoveFirst();
				}
				timer = 0f;
				nextQuack = Random.Range(0.05f, 1f);
			}
		}
		else
		{
			image.sprite = happyDuck;
			if (!isBeingHeadpatted)
			{
				duckHappiness -= Time.fixedDeltaTime;
			}
			else
			{
				duckHappiness += Time.fixedDeltaTime;
			}
			foreach (GameObject quack in quacks)
			{
				Object.Destroy(quack);
			}
			quacks.Clear();
		}
		imageRectTransform.pivot = pivot;
	}

	public override IEnumerator Open()
	{
		BrowserPopup.FinishLoading();
		yield break;
	}
}
