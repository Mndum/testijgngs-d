using UnityEngine;

public class PCScene : ViewScene
{
	[SerializeField]
	private AudioClip _startPC;

	[SerializeField]
	private AudioClip _loopPC;

	[SerializeField]
	private AudioClip _endPC;

	private SpecialAudioLoop _pcSoundLoop;

	public override void Prepare()
	{
		base.Prepare();
		_pcSoundLoop = null;
	}

	public void StartPCSoundLoop()
	{
		if (_pcSoundLoop == null)
		{
			_pcSoundLoop = SingletonMonoBehaviour<SoundSingleton>.Instance.CreateSpecialLoop(_startPC, _loopPC, _endPC);
			_pcSoundLoop.Play();
		}
	}

	public void StopPCSoundLoop()
	{
		StopPCSoundLoop(kill: false);
	}

	public void StopPCSoundLoop(bool kill)
	{
		if (_pcSoundLoop != null)
		{
			_pcSoundLoop.End(kill);
			_pcSoundLoop = null;
		}
	}

	protected override void PrepareNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		gameObject.SetActive(value: true);
		StartPCSoundLoop();
	}

	protected override void StartNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
	}

	protected override void SwitchNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
	}

	protected override void EndOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameScene)
		{
			StopPCSoundLoop();
		}
	}
}
