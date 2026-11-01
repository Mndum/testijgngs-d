using System;
using System.Collections;
using UnityEngine;

public class DeveloperRoomState : GameState
{
	private DeveloperRoomGui _developerRoomGui;

	private DeveloperViewScene _developerViewScene;

	private ManagedCoroutineWithTarget _sheepCoroutine;

	private int introducedCount;

	private string SoundSingletonId => GetType().Name;

	public override void EnterState()
	{
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.AddGlobalFlag("sre");
		GetAndEnableStaticGui<DeveloperRoomGui>(out _developerRoomGui);
		_developerViewScene = SingletonMonoBehaviour<ViewSingleton>.Instance.DeveloperRoomScene;
		_sheepCoroutine = new ManagedCoroutineWithTarget();
		_sheepCoroutine.SetTarget(_developerViewScene);
		_sheepCoroutine.Start(SheepGreetingCoroutine());
		IModificationPeriodButtonList modificationPeriod;
		using (_developerRoomGui.SideButtonList.DoInitialization(out modificationPeriod))
		{
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
			buttonListWrapper.SetText("Talk");
			buttonListWrapper.OnClick = TalkButtonClicked;
			ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton();
			buttonListWrapper2.SetText("Introduction");
			buttonListWrapper2.OnClick = IntroductionButtonClicked;
		}
		_developerRoomGui.ReturnButton.onClick.SetListener(SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToMainMenu);
		SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterAllMusic();
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption(SoundSingletonId, SoundSingleton.MusicOption.MusicOptionLayer.Forced, R.Music.BadEnd_Demise.Get()));
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
	}

	public override void ExitState()
	{
		_developerRoomGui.SideButtonList.ResetButtons();
		SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(SoundSingletonId);
		DisableAllClaimedStaticGui();
		_sheepCoroutine.Stop();
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
	}

	private void IntroductionButtonClicked()
	{
		if (!_sheepCoroutine.Running)
		{
			_sheepCoroutine.Start(SheepIntroduction());
		}
	}

	private IEnumerator SheepIntroduction()
	{
		if (introducedCount == 0)
		{
			yield return BetterConversationManager.DoConversation("Sheep:Who am I?\nSheep:I'm the SHEEP, the god of this world. Or in other words...\nSheep:I'm the <i>the main developer</i> in charge of making this game.\nSheep:What part of the game do I work on?\nSheep:My main skill is programming, but in game dev you really need to be a jack of all trades. I have to work on everything to some extent.\nSheep:Don't worry I don't work alone! I've got Sixie+Majin(The artists) and HMNI(Writer) to back me up!\nSheep:Why aren't they here? Well... the budget for this room was too low...");
		}
		else if (introducedCount == 1)
		{
			yield return BetterConversationManager.DoConversation("Sheep:You want to know more about me?\nSheep:Well...\nSheep:Truth be told I've been working on this room for couple hours now and I'm kind of tired of it, so I think that's all I can be bothered to write...\nSheep:Maybe if you come back to this room in the next update this room will be cooler?\n");
		}
		else if (introducedCount == 2)
		{
			yield return BetterConversationManager.DoConversation("Sheep:...");
		}
		else if (introducedCount == 3)
		{
			yield return BetterConversationManager.DoConversation("Sheep:That's all I have to say about myself.");
		}
		else if (introducedCount == 4)
		{
			yield return BetterConversationManager.DoConversation("Sheep:This is the last unique dialogue you can get by clicking this button... Goodbye!");
		}
		else if (introducedCount == 100)
		{
			yield return BetterConversationManager.DoConversation("Sheep: YOU DID IT! YOU CLICKED THIS BUTTON A HUNDRED TIMES! THE REWARD FOR THIS IS........... NOTHING! YAAAAY!");
		}
		else
		{
			yield return BetterConversationManager.DoConversation("Sheep:...");
		}
		introducedCount++;
	}

	private void TalkButtonClicked()
	{
		if (!_sheepCoroutine.Running)
		{
			_sheepCoroutine.Start(SheepTalk());
		}
	}

	private IEnumerator SheepTalk()
	{
		float value = UnityEngine.Random.value;
		if (value < 0.1f)
		{
			yield return BetterConversationManager.DoConversation("Sheep: This room will be ONLY in English.\nSheep: Why? {wc} Because otherwise game's translators can just read everything that's written here in the translation files...\nSheep: We wouldn't want that would we? ...\nSheep: It wouldn't be a secret room if that were to happen.\n");
		}
		else if (value < 0.2f)
		{
			yield return BetterConversationManager.DoConversation("Sheep:AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\nSheep:Oh, sorry. Didn't see you there.");
		}
		else if (value < 0.4f)
		{
			yield return BetterConversationManager.DoConversation("Sheep:" + Environment.MachineName + "?\nSheep:...\nSheep:That's just your PC name. No need to be scared :)\n");
		}
		else if (value < 0.5f)
		{
			yield return BetterConversationManager.DoConversation("Sheep:Why do I look so angry?\nSheep:... It just seemed fitting\n");
		}
		else if (value < 0.6f)
		{
			yield return BetterConversationManager.DoConversation("Sheep:There aren't many unique dialogues here...\nSheep:Maybe one day this will a great place with many things to do... Today it's not that day.\n");
		}
		else if (value < 0.8f)
		{
			yield return BetterConversationManager.DoConversation("Sheep:The song playing right now was actually composed by me :)\n");
			if (SoundSingleton.MusicOff)
			{
				yield return BetterConversationManager.DoConversation("Sheep:Wait... YOU MOTHERFUCKER! YOU MUTED THE MUSIC! GET THE FUCK OUT OF HERE!\n");
				SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToMainMenu();
			}
			else
			{
				yield return BetterConversationManager.DoConversation("Sheep:Do you like it?\n");
			}
		}
		else
		{
			yield return BetterConversationManager.DoConversation("Sheep:I had great plans for this place...\nSheep:Unfortunately, I had to work on more important parts of the game...\n");
		}
	}

	private IEnumerator SheepGreetingCoroutine()
	{
		yield return BetterConversationManager.DoConversation("Sheep:WELCOME TO THE HOUSE OF THE SHEEP!\n");
	}
}
