using System;
using System.Collections;
using System.Text;
using Fungus;
using TMPro;
using UnityEngine;
using Zalgo;

public class HellWebsite : Website
{
	public static string WebsiteUrl = "h\u200be\u200bl\u200bl";

	[SerializeField]
	private Transform eyeL;

	[SerializeField]
	private Transform eyeR;

	[SerializeField]
	private TextMeshProUGUI text;

	[SerializeField]
	private TextMeshProUGUI text2;

	[SerializeField]
	private TextMeshProUGUI text3;

	private HellShaderScript _hellShaderScript;

	private static readonly string[] badWords = new string[12]
	{
		"death", "suicide", "sex", "murder", "genocide", "intestines", "corpse", "burn alive", "hell", "WHYYYY GOD WHYYYYYYY",
		"The end", "slaughter"
	};

	private static readonly string[] bibleQuotes = new string[6] { "So the Lord God said to the serpent, “Because you have done this, Cursed are you above all livestock and all wild animals! You will crawl on your belly and you will eat dust all the days of your life.", "Cursed is the ground because of you; through painful toil you will eat food from it all the days of your life. It will produce thorns and thistles for you, and you will eat the plants of the field. By the sweat of your brow you will eat your food until you return to the ground, since from it you were taken; for dust you are and to dust you will return.", "The first angel sounded his trumpet, and there came hail and fire mixed with blood, and it was hurled down on the earth. A third of the earth was burned up, a third of the trees were burned up, and all the green grass was burned up. ", "Anyone whose name was not found written in the book of life was thrown into the lake of fire. ", "If your hand causes you to stumble, cut it off. It is better for you to enter life maimed than with two hands to go into hell, where the fire never goes out. ", "For if God did not spare angels when they sinned, but sent them to hell, putting them in chains of darkness to be held for judgment." };

	private static readonly string[] nonnseneQuotes = new string[9] { "The shadows danced with whispers of the forgotten, while the moon sang songs of madness and despair.", "The abyss opened its eyes, and the stars whispered secrets that no mortal could comprehend.", "The void spoke in tongues, and the darkness howled with laughter.", "The veil between worlds grows thin, and the shadows writhe with ancient terrors.", "The forest wept blood, and the trees whispered secrets that no human should know.", "The ancient city lies forgotten, its walls and spires dripping with the ichor of aeons.", "The black stars shine with an unclean light, casting their baleful gaze upon a world trembling on the brink of annihilation.", "Beyond the veil of reality lies the gibbering maw of chaos, waiting to devour all who dare to glimpse its unfathomable horror.", "Ph'nglui mglw'nafh Cthulhu R'lyeh wgah'nagl fhtagn." };

	private static string[] lovecraftianSuffixes = new string[8] { "thulhu", "gorgon", "tenebra", "yoggoth", "necron", "eldritch", "ichor", "shoggoth" };

	private float madness;

	private float lastPitch = 1f;

	private float targetPitch = 0.6f;

	private float pitchChangeTimer;

	private float pitchChangeTimerLength = 3f;

	public string PossiblePlayerName => Environment.UserName;

	public static string GenerateLovecraftianName()
	{
		int num = UnityEngine.Random.Range(4, 10);
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			for (int i = 0; i < num; i++)
			{
				if (i == 0)
				{
					stringBuilder.Append(char.ToUpper(StringUtils.Consonants.GetRandom()));
				}
				else
				{
					stringBuilder.Append(char.ToLower((i % 2 == 0) ? StringUtils.Consonants.GetRandom() : StringUtils.Vowels.GetRandom()));
				}
			}
			stringBuilder.Append(lovecraftianSuffixes.GetRandom());
			return stringBuilder.ToString();
		}
	}

	public override void ProcessUrl(GameUrlWithArguments gameUrlWithArguments)
	{
		gameUrlWithArguments.Domain = gameUrlWithArguments.Domain.Replace("\u200b", "");
		CurrentGameUrl = gameUrlWithArguments;
	}

	private void End()
	{
		UiOverlay.Instance.CloseUiOverlay();
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ShowText("You:" + LOC.Other.WhatTheHell);
	}

	public void IncreaseMadness(float val)
	{
		madness += val;
		_hellShaderScript.depth = madness;
	}

	private string GenerateScaryString()
	{
		float value = UnityEngine.Random.value;
		if (value < 0.5f)
		{
			return badWords.GetRandom();
		}
		if (value < 0.7f)
		{
			return bibleQuotes.GetRandom();
		}
		return nonnseneQuotes.GetRandom();
	}

	private string GenerateRandomChars()
	{
		int num = UnityEngine.Random.Range(4, 10);
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			for (int i = 0; i < num; i++)
			{
				if (i == 0)
				{
					stringBuilder.Append(StringUtils.Consonants.GetRandom());
				}
				else
				{
					stringBuilder.Append((i % 2 == 0) ? StringUtils.Consonants.GetRandom() : StringUtils.Vowels.GetRandom());
				}
			}
			return stringBuilder.ToString();
		}
	}

	public string GenerateWelcomeString()
	{
		float value = UnityEngine.Random.value;
		if (GameVariables.Current.CheckFlag("hell"))
		{
			if (value < 0.5f)
			{
				return new ZalgoString("You again? I'm already coming for you... " + GenerateRandomChars() + " Just wait. " + GenerateRandomChars() + PossiblePlayerName + GenerateRandomChars());
			}
			if (value < 0.7f)
			{
				return new ZalgoString("HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHA");
			}
			return new ZalgoString("HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHHAHHhahhahahahahahahhaha");
		}
		if (value < 0.5f)
		{
			return new ZalgoString("Welcome to hell " + GenerateRandomChars() + PossiblePlayerName);
		}
		if (value < 0.7f)
		{
			return new ZalgoString("WHy? ARE YOU HERE?");
		}
		return new ZalgoString("Leave.");
	}

	public override IEnumerator Open()
	{
		_hellShaderScript = UiOverlay.Instance.MainCamera.GetComponent<HellShaderScript>();
		_hellShaderScript.enabled = true;
		_hellShaderScript.depth = 0f;
		yield return new WaitForSeconds(5f);
		this.text.text = GenerateWelcomeString();
		GameVariables.Current.AddFlag("hell");
		yield return new WaitForSeconds(3f);
		float value = UnityEngine.Random.value;
		if (value < 0.2f)
		{
			UiOverlay.Instance.OkPopup("You will die.", GenerateScaryString());
			IncreaseMadness(0.1f);
			yield return new WaitForSeconds(3f);
		}
		else if (value < 0.4f)
		{
			UiOverlay.Instance.SimplePopup("DIE", string.Concat(new ZalgoString("DIE"), GenerateRandomChars()), () =>
			{
				IncreaseMadness(0.2f);
			});
			IncreaseMadness(0.1f);
			yield return new WaitForSeconds(3f);
		}
		else if (value < 0.6f)
		{
			string text = GenerateLovecraftianName();
			UiOverlay.Instance.SimplePopup(text, new ZalgoString(text + "H e called you to join him in cleaning the earth from the filth the humanity is."), () =>
			{
				IncreaseMadness(0.2f);
			});
			this.text.text = text + " is coming to earth.";
			IncreaseMadness(0.1f);
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitForSeconds(1f);
		if (UnityEngine.Random.value > 0.9f)
		{
			string characterName = new ZalgoString(GenerateLovecraftianName());
			FungusManager.Instance.NarrativeLog.AddLine(SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.GetTemporaryCharacter(characterName), "He comes!");
			for (int num = 0; num < 20; num++)
			{
				FungusManager.Instance.NarrativeLog.AddLine(SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.GetTemporaryCharacter(characterName), GenerateScaryString());
			}
			IncreaseMadness(0.1f);
			FungusManager.Instance.NarrativeLog.AddLine(SingletonLoadableMonoBehaviour<ConversationSingleton>.Instance.GetTemporaryCharacter(PossiblePlayerName), LOC.Other.IAmScared + GenerateRandomChars());
			UiOverlay.Instance.OpenNarrativeLog();
			yield return new WaitForSeconds(4f);
		}
		if (UnityEngine.Random.value > 0.9f)
		{
			int laughterLength = UnityEngine.Random.Range(50, 100);
			this.text.text = "HAHAHAhahaha" + PossiblePlayerName + GenerateRandomChars() + GenerateLovecraftianName();
			for (int i = 0; i < laughterLength; i++)
			{
				this.text.text += (((double)UnityEngine.Random.value > 0.6) ? "ha" : "HA");
				yield return new WaitForSeconds(0.1f);
			}
		}
		yield return new WaitForSeconds(5f);
		IncreaseMadness(1f);
		if (UnityEngine.Random.value > 0.5f)
		{
			lastPitch = 2f;
			targetPitch = 0.1f;
			pitchChangeTimerLength = 40f;
			yield return new WaitForSeconds(35f);
		}
		else
		{
			lastPitch = 1f;
			targetPitch = 0.5f;
			pitchChangeTimerLength = 10f;
		}
		yield return new WaitForSeconds(9f);
		End();
	}

	public void OnEnable()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption("HellWebsite", SoundSingleton.MusicOption.MusicOptionLayer.Forced, SingletonScriptableObject<MusicData>.Instance.Demise));
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
	}

	public void Update()
	{
		if (pitchChangeTimer > pitchChangeTimerLength)
		{
			pitchChangeTimer = 0f;
			pitchChangeTimerLength = UnityEngine.Random.Range(0f, 20f);
			lastPitch = targetPitch;
			targetPitch = UnityEngine.Random.Range(0.7f, 1.1f);
			Transform transform = eyeL;
			Vector3 localScale = (eyeR.localScale = new Vector3(1f, 0.1f, 1f));
			transform.localScale = localScale;
		}
		else
		{
			pitchChangeTimer += Time.deltaTime;
			SingletonMonoBehaviour<SoundSingleton>.Instance.SetMusicPitch(Mathf.Lerp(lastPitch, targetPitch, pitchChangeTimer / pitchChangeTimerLength));
			Transform transform2 = eyeL;
			Vector3 localScale = (eyeR.localScale = new Vector3(1f, 1f, 1f));
			transform2.localScale = localScale;
		}
		GameVariables.Current.MentalHealth -= Time.deltaTime * 0.01f;
	}

	public void FixedUpdate()
	{
		if ((double)madness < 0.2)
		{
			text2.text = "";
			text3.text = "";
		}
		else if (madness < 0.7f)
		{
			text2.text = badWords.GetRandom();
			text3.text = badWords.GetRandom();
		}
		else
		{
			text2.text = new ZalgoString(GenerateScaryString());
			text3.text = new ZalgoString(GenerateScaryString());
		}
	}

	public void OnDisable()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById("HellWebsite");
		if (_hellShaderScript != null)
		{
			_hellShaderScript.enabled = false;
		}
	}
}
