using Fungus;
using UnityEngine;

public class RoomScene : ViewScene
{
	private enum BotState
	{
		None,
		Sitting,
		Standing
	}

	private RectTransform _possibleStandingArea;

	private RectTransform _possibleSittingArea;

	[SerializeField]
	private Live2DHolder _interactionHolder;

	private Live2DPrefabHolder.Live2DControllerUsage<Live2DInteractionController> _interactionControllerUsage;

	private float _scaleMin = 1.8f;

	private float _scaleMax = 2.2f;

	private BotState _currentBotState;

	public override void Prepare()
	{
		base.Prepare();
		_possibleStandingArea = (RectTransform)transform.Find("PossibleStandingArea");
		_possibleSittingArea = (RectTransform)transform.Find("PossibleSittingArea");
		_currentBotState = BotState.None;
	}

	public Live2DInteractionController GetBotCharacter()
	{
		if (_interactionControllerUsage.TryGetController(out var controller))
		{
			return controller;
		}
		Debug.LogError("Tried to get room scene interaction controller! Room scene doesn't currently have the bot!!!");
		return null;
	}

	protected override void PrepareNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		gameObject.SetActive(value: true);
	}

	protected override void SwitchNewView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (_interactionControllerUsage == null || !_interactionControllerUsage.IsValid)
		{
			_interactionControllerUsage = _interactionHolder.StartUsing(SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.InteractCharacterHolder, (Live2DInteractionController live2DInteractionController) =>
			{
				live2DInteractionController.ResetEverything(Live2DInteractionController.InteractionSceneTypeEnum.Normal);
				live2DInteractionController.ChangeLayer(-1);
			});
		}
		else
		{
			Live2DInteractionController controller = _interactionControllerUsage.Controller;
			controller.EnsureBrain(SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.InteractCharacterHolder.CharacterId);
			controller.SetEnabled(v: true);
		}
		Live2DInteractionController controller2 = _interactionControllerUsage.Controller;
		controller2.ParamBlanketSitting.SetPercent(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CanStand ? 0f : 1f);
		controller2.ParamBlanketSitting.FinishMove();
		controller2.CurrentBrain.ChangeState(new StoryBrainState(), force: true);
		View oldView = info.OldView;
		if (oldView == null || oldView.Scene != this)
		{
			_currentBotState = BotState.None;
		}
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CanStand)
		{
			controller2.ParamBlanketSitting.SetPercent(0f);
			if (oldView == null || oldView.Scene != this || _currentBotState != BotState.Standing)
			{
				MoveBotToRandomStandingPosition();
				_currentBotState = BotState.Standing;
			}
		}
		else
		{
			controller2.ParamBlanketSitting.SetPercent(1f);
			if (oldView == null || oldView.Scene != this || _currentBotState != BotState.Sitting)
			{
				MoveBotToRandomSittingPosition();
				_currentBotState = BotState.Sitting;
			}
		}
		controller2.ParamBlanketSitting.FinishMove();
		if (info.SameScene)
		{
			return;
		}
		string id = GetType().Name;
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(id, R.Music.Room_Day_All, SoundSingleton.MusicOption.MusicOptionLayer.Base, () =>
		{
			int hour = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Hour;
			return hour >= 8 && hour <= 20;
		});
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(id, R.Music.Room_Night_All, SoundSingleton.MusicOption.MusicOptionLayer.Base, () =>
		{
			int hour = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Hour;
			return hour < 8 || hour > 20;
		});
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(id, R.Music.Room_Low_Sanity_All, SoundSingleton.MusicOption.MusicOptionLayer.Base, () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.MentalHealth < 0.5f);
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption(id, SoundSingleton.MusicOption.MusicOptionLayer.AboveBase, () =>
		{
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlag("WeebMusic"))
			{
				int hour = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Hour;
				if (hour <= 16)
				{
					return hour <= 12;
				}
				return true;
			}
			return false;
		}, SingletonScriptableObject<MusicData>.Instance.NeighbourSong));
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(id, R.Music.Room_Day_Late_Game_All, SoundSingleton.MusicOption.MusicOptionLayer.Base, () =>
		{
			GameVariables current = GameVariables.Current;
			return current.IsLateGame && (current.Hour > 15 || current.Hour < 4);
		});
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption(id, SoundSingleton.MusicOption.MusicOptionLayer.Base, () => GameVariables.Current.MentalHealth < 0.5f, R.Music.Room_Special_Nightmare.Get()));
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
	}

	protected override void EndOldView(ViewSceneTransitionInfo info, ViewSceneCallbacksEnum thisCallback)
	{
		if (!info.SameScene)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(GetType().Name);
		}
		if (!info.SameSceneGameObject)
		{
			_interactionControllerUsage.Dispose();
			_interactionControllerUsage = null;
			gameObject.SetActive(value: false);
		}
	}

	private void MoveBotToRandomStandingPosition()
	{
		Live2DHolder interactionHolder = _interactionHolder;
		Rect worldRect = _possibleStandingArea.GetWorldRect();
		float value = Random.value;
		Vector2 vector = new Vector2(Random.Range(worldRect.xMin, worldRect.xMax), Mathf.Lerp(worldRect.yMin, worldRect.yMax, value));
		float num = Mathf.Lerp(_scaleMax, _scaleMin, value);
		RectTransform rectTransform = (RectTransform)interactionHolder.transform;
		rectTransform.localScale = new Vector3(num, num, num);
		vector.y += rectTransform.sizeDelta.y * 0.5f * num;
		rectTransform.position = vector;
	}

	private void MoveBotToRandomSittingPosition()
	{
		Live2DHolder interactionHolder = _interactionHolder;
		Rect worldRect = _possibleSittingArea.GetWorldRect();
		float x = (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotAwake ? Random.Range(worldRect.xMin, worldRect.xMax) : worldRect.xMax);
		Vector2 vector = new Vector2(x, worldRect.yMin);
		RectTransform rectTransform = (RectTransform)interactionHolder.transform;
		float scaleMin = _scaleMin;
		rectTransform.localScale = new Vector3(scaleMin, scaleMin, scaleMin);
		vector.y += rectTransform.sizeDelta.y * 0.5f * scaleMin;
		rectTransform.position = vector;
	}
}
