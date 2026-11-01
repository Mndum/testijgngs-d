using UnityEngine;
using UnityEngine.UI;

public class RoomScript : MonoBehaviour
{
	[Header("Light")]
	public Gradient multiplyGradient;

	public Gradient windowGradient;

	public Gradient morningLightGradient;

	[SerializeField]
	private SpriteRenderer LightMultiply;

	[SerializeField]
	private SpriteRenderer WindowLight;

	[SerializeField]
	private SpriteRenderer MorningLight;

	[Header("LightSwitch")]
	[SerializeField]
	private Button LightSwitchButton;

	private Image _lightSwitchImage;

	[SerializeField]
	private Sprite LightSwitchOnSprite;

	[SerializeField]
	private Sprite LightSwitchOffSprite;

	[SerializeField]
	private AudioClip SwitchOn;

	[SerializeField]
	private AudioClip SwitchOff;

	[SerializeField]
	private GameObject BathroomDoor;

	[SerializeField]
	private SpriteRenderer BathroomDoorLight;

	private float t;

	private Color multiplyColor;

	private Color windowColor;

	private Color morningLightColor;

	private float flickerT;

	private static readonly float flickerHz = 7f;

	private static readonly float flickerLength = 1f / flickerHz;

	private static readonly float flicketT1 = 0.103f;

	private static readonly float flickerT2 = 0.95f;

	public bool LightSwitchOn
	{
		get
		{
			if (GameScript.HasGameStarted)
			{
				return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lightSwitchOn;
			}
			return false;
		}
		private set
		{
			if (GameScript.HasGameStarted)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lightSwitchOn = value;
			}
		}
	}

	private void SyncBathroomDoor()
	{
		if (GameScript.HasGameStarted)
		{
			GameVariables current = GameVariables.Current;
			bool bathroomDoorInstalled = current.BathroomDoorInstalled;
			BathroomDoor.SetActive(bathroomDoorInstalled);
			BathroomDoorLight.gameObject.SetActive(bathroomDoorInstalled && current.bathroomLightOn);
		}
		else
		{
			BathroomDoor.SetActive(value: false);
			BathroomDoorLight.gameObject.SetActive(value: false);
		}
	}

	private void SyncLightSwitchSprite()
	{
		_lightSwitchImage.sprite = (LightSwitchOn ? LightSwitchOnSprite : LightSwitchOffSprite);
	}

	private void LightSwitchClicked()
	{
		LightSwitchOn = !LightSwitchOn;
		SyncLightSwitchSprite();
		SetTime();
		SingletonMonoBehaviour<SoundSingleton>.Instance.Play(LightSwitchOn ? SwitchOn : SwitchOff, Random.Range(0.8f, 1f));
	}

	public void Awake()
	{
		_lightSwitchImage = LightSwitchButton.GetComponent<Image>();
		LightSwitchButton.onClick.SetListener(LightSwitchClicked);
	}

	public void OnEnable()
	{
		if (GameScript.HasGameStarted)
		{
			SetTime();
		}
		SingletonLoadableMonoBehaviour<GameScript>.Instance.OnMinutePassed += SetTime;
		SyncLightSwitchSprite();
		SyncBathroomDoor();
	}

	private void SetTime()
	{
		SetTime(GameVariables.Current.TimeDay);
	}

	private void SetTime(int time)
	{
		float num = 1440f;
		t = (float)time / num;
		bool lightSwitchOn = LightSwitchOn;
		multiplyColor = multiplyGradient.Evaluate(t);
		if (lightSwitchOn)
		{
			LightMultiply.color = (multiplyColor + Color.white) / 2f;
		}
		else
		{
			LightMultiply.color = multiplyColor;
		}
		windowColor = windowGradient.Evaluate(t);
		WindowLight.color = windowColor;
		morningLightColor = morningLightGradient.Evaluate(t);
		if (morningLightColor.a > 0f)
		{
			MorningLight.enabled = true;
			MorningLight.color = morningLightColor;
		}
		else
		{
			MorningLight.enabled = false;
		}
	}

	private void SyncFlicker(float value)
	{
		flickerT += Time.fixedDeltaTime;
		if (flickerT > flickerLength)
		{
			WindowLight.color = windowColor * Random.Range(0.99f - 0.1f * value, 1f);
			flickerT = Random.Range(0f, flickerT / 2f);
		}
	}

	public void FixedUpdate()
	{
		if (t < flicketT1)
		{
			SyncFlicker(1f - t / flicketT1);
		}
		else if (t > flickerT2)
		{
			SyncFlicker(1f - (1f - t) / (1f - flickerT2));
		}
	}

	public void OnDisable()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.OnMinutePassed -= SetTime;
	}
}
