using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;

public sealed class ViewSingleton : SingletonMonoBehaviour<ViewSingleton>
{
	public List<ViewWithSpriteRenderer> GenericViews = new List<ViewWithSpriteRenderer>();

	private View _nextView;

	[SerializeField]
	private View view1;

	[SerializeField]
	private View view2;

	[SerializeField]
	private View view3;

	[SerializeField]
	private View roomView;

	[SerializeField]
	private View roomCharacterView;

	[SerializeField]
	private View roomCharacterViewZoom;

	[SerializeField]
	private View roomCharacterViewEquipment;

	[SerializeField]
	private View sleepView;

	[SerializeField]
	private View doggyRoomView;

	[SerializeField]
	private View blowjobView;

	[SerializeField]
	private View cowgirlView;

	[SerializeField]
	private View roomStoryViewWithoutAutomaticBot;

	[SerializeField]
	private View roomViewWithoutAutomaticBot;

	[SerializeField]
	private View missionaryView;

	[SerializeField]
	private View missionaryView2;

	[SerializeField]
	private View paralaxBoundsShower;

	[SerializeField]
	private View showerView;

	[SerializeField]
	private View showerViewNoBot;

	[SerializeField]
	private View cuddleView;

	[SerializeField]
	private View cuddleViewZoom;

	[SerializeField]
	private View clothierView;

	[SerializeField]
	private View shopView;

	[SerializeField]
	private View changingRoomView;

	[SerializeField]
	private View menuView;

	[SerializeField]
	private View paralaxBounds;

	[SerializeField]
	private View pCView;

	[SerializeField]
	private View outsideView;

	[SerializeField]
	private View developerRoomView;

	[SerializeField]
	private ViewScene scene1;

	[SerializeField]
	private ViewScene scene2;

	[SerializeField]
	private ViewScene scene3;

	[SerializeField]
	private RoomScene mainRoomScene;

	[SerializeField]
	private FrickScene doggyScene;

	[SerializeField]
	private FrickScene blowjobScene;

	[SerializeField]
	private FrickScene cowgirlScene;

	[SerializeField]
	private ViewScene roomStoryScene;

	[SerializeField]
	private FrickScene missionaryScene;

	[SerializeField]
	private ShowerScene showerScene;

	[SerializeField]
	private ViewScene showerSceneNoBot;

	[SerializeField]
	private CuddleScene cuddleScene;

	[SerializeField]
	private ClothierViewScene clothierScene;

	[SerializeField]
	private ClothierViewScene shopScene;

	[SerializeField]
	private ChangingRoomScene changingRoomScene;

	[SerializeField]
	private MainMenuScene menuScene;

	[SerializeField]
	private PCScene pCScene;

	[SerializeField]
	private ViewScene outsideScene;

	[SerializeField]
	private DeveloperViewScene developerRoomScene;

	public View CurrentView => FungusManager.Instance.CameraManager.CurrentView;

	public View ViewForCharacters
	{
		get
		{
			if (_nextView != null)
			{
				return _nextView;
			}
			return CurrentView;
		}
	}

	public View View1 => view1;

	public View View2 => view2;

	public View View3 => view3;

	public View RoomView => roomView;

	public View RoomCharacterView => roomCharacterView;

	public View RoomCharacterViewZoom => roomCharacterViewZoom;

	public View RoomCharacterViewEquipment => roomCharacterViewEquipment;

	public View SleepView => sleepView;

	public View DoggyRoomView => doggyRoomView;

	public View BlowjobView => blowjobView;

	public View CowgirlView => cowgirlView;

	public View RoomStoryViewWithoutAutomaticBot => roomStoryViewWithoutAutomaticBot;

	public View RoomViewWithoutAutomaticBot => roomViewWithoutAutomaticBot;

	public View MissionaryView => missionaryView;

	public View MissionaryView2 => missionaryView2;

	public View ParalaxBoundsShower => paralaxBoundsShower;

	public View ShowerView => showerView;

	public View ShowerViewNoBot => showerViewNoBot;

	public View CuddleView => cuddleView;

	public View CuddleViewZoom => cuddleViewZoom;

	public View ClothierView => clothierView;

	public View ShopView => shopView;

	public View ChangingRoomView => changingRoomView;

	public View MenuView => menuView;

	public View ParalaxBounds => paralaxBounds;

	public View PCView => pCView;

	public View OutsideView => outsideView;

	public View DeveloperRoomView => developerRoomView;

	public ViewScene Scene1 => scene1;

	public ViewScene Scene2 => scene2;

	public ViewScene Scene3 => scene3;

	public RoomScene MainRoomScene => mainRoomScene;

	public FrickScene DoggyScene => doggyScene;

	public FrickScene BlowjobScene => blowjobScene;

	public FrickScene CowgirlScene => cowgirlScene;

	public ViewScene RoomStoryScene => roomStoryScene;

	public FrickScene MissionaryScene => missionaryScene;

	public ShowerScene ShowerScene => showerScene;

	public ViewScene ShowerSceneNoBot => showerSceneNoBot;

	public CuddleScene CuddleScene => cuddleScene;

	public ClothierViewScene ClothierScene => clothierScene;

	public ClothierViewScene ShopScene => shopScene;

	public ChangingRoomScene ChangingRoomScene => changingRoomScene;

	public MainMenuScene MenuScene => menuScene;

	public PCScene PCScene => pCScene;

	public ViewScene OutsideScene => outsideScene;

	public DeveloperViewScene DeveloperRoomScene => developerRoomScene;

	public event Action<View> OnViewChanged
	{
		add
		{
			FungusManager.Instance.CameraManager.ViewChanged += value;
		}
		remove
		{
			FungusManager.Instance.CameraManager.ViewChanged -= value;
		}
	}

	public void SetNextView(View view = null)
	{
		_nextView = view ?? GetNotCurrentView();
	}

	public View GetNotCurrentView()
	{
		try
		{
			if (_nextView != null)
			{
				return _nextView;
			}
			View current = CurrentView;
			return GenericViews.FirstOrDefault((ViewWithSpriteRenderer t) => !View.BelongsToOrIs(current, t)) ?? GenericViews[0];
		}
		finally
		{
			_nextView = null;
		}
	}

	public View GetNotCurrentViewSetSprite(Sprite sprite, Color color, View.SubViews subView = View.SubViews.Main)
	{
		View notCurrentView = GetNotCurrentView();
		if (notCurrentView is ViewWithSpriteRenderer viewWithSpriteRenderer)
		{
			viewWithSpriteRenderer.SetSpriteColor(color);
			viewWithSpriteRenderer.SetSprite(sprite);
		}
		return notCurrentView.GetSubView(subView);
	}

	public override void Awake()
	{
		base.Awake();
		foreach (Transform item in transform)
		{
			item.gameObject.SetActive(value: true);
		}
		ViewScene[] componentsInChildren = GetComponentsInChildren<ViewScene>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Prepare();
		}
		foreach (Transform item2 in transform)
		{
			item2.gameObject.SetActive(value: false);
		}
	}

	private void InitSceneReference<T>(ref T scene, ViewScene newScene, string targetName) where T : ViewScene
	{
		if (newScene.GetViewSceneName() != targetName)
		{
			Debug.LogError("Wrong name of a view. Expected \"" + targetName + "\" got \"" + newScene.name + "\"!");
		}
		else
		{
			scene = (T)newScene;
		}
	}

	private void InitViewReference(ref View view, View newView, string targetName)
	{
		if (newView.name != targetName)
		{
			Debug.LogError("Wrong name of a view. Expected \"" + targetName + "\" got \"" + newView.name + "\"!");
		}
		else
		{
			view = newView;
		}
	}

	public void UpdateViewReferences()
	{
		int num = 28;
		View[] componentsInChildren = GetComponentsInChildren<View>(includeInactive: true);
		if (componentsInChildren.Length != num)
		{
			Debug.LogError("Wrong view count! Regenerate ViewSingleton script!");
			return;
		}
		InitViewReference(ref view1, componentsInChildren[0], "View 1");
		InitViewReference(ref view2, componentsInChildren[1], "View 2");
		InitViewReference(ref view3, componentsInChildren[2], "View 3");
		InitViewReference(ref roomView, componentsInChildren[3], "Room View");
		InitViewReference(ref roomCharacterView, componentsInChildren[4], "Room Character View");
		InitViewReference(ref roomCharacterViewZoom, componentsInChildren[5], "Room Character View Zoom");
		InitViewReference(ref roomCharacterViewEquipment, componentsInChildren[6], "Room Character View Equipment");
		InitViewReference(ref sleepView, componentsInChildren[7], "Sleep View");
		InitViewReference(ref doggyRoomView, componentsInChildren[8], "Doggy Room View");
		InitViewReference(ref blowjobView, componentsInChildren[9], "Blowjob View");
		InitViewReference(ref cowgirlView, componentsInChildren[10], "Cowgirl View");
		InitViewReference(ref roomStoryViewWithoutAutomaticBot, componentsInChildren[11], "Room Story View Without Automatic Bot");
		InitViewReference(ref roomViewWithoutAutomaticBot, componentsInChildren[12], "Room View Without Automatic Bot");
		InitViewReference(ref missionaryView, componentsInChildren[13], "Missionary View");
		InitViewReference(ref missionaryView2, componentsInChildren[14], "Missionary View 2");
		InitViewReference(ref paralaxBoundsShower, componentsInChildren[15], "Paralax Bounds Shower");
		InitViewReference(ref showerView, componentsInChildren[16], "Shower View");
		InitViewReference(ref showerViewNoBot, componentsInChildren[17], "Shower View No Bot");
		InitViewReference(ref cuddleView, componentsInChildren[18], "Cuddle View");
		InitViewReference(ref cuddleViewZoom, componentsInChildren[19], "Cuddle View Zoom");
		InitViewReference(ref clothierView, componentsInChildren[20], "Clothier View");
		InitViewReference(ref shopView, componentsInChildren[21], "Shop View");
		InitViewReference(ref changingRoomView, componentsInChildren[22], "Changing Room View");
		InitViewReference(ref menuView, componentsInChildren[23], "Menu View");
		InitViewReference(ref paralaxBounds, componentsInChildren[24], "Paralax Bounds");
		InitViewReference(ref pCView, componentsInChildren[25], "PC View");
		InitViewReference(ref outsideView, componentsInChildren[26], "Outside View");
		InitViewReference(ref developerRoomView, componentsInChildren[27], "Developer Room View");
		int num2 = 19;
		ViewScene[] componentsInChildren2 = GetComponentsInChildren<ViewScene>(includeInactive: true);
		if (componentsInChildren2.Length != num2)
		{
			Debug.LogError("Wrong scene count! Regenerate ViewSingleton script!");
			return;
		}
		InitSceneReference(ref scene1, componentsInChildren2[0], "Scene 1");
		InitSceneReference(ref scene2, componentsInChildren2[1], "Scene 2");
		InitSceneReference(ref scene3, componentsInChildren2[2], "Scene 3");
		InitSceneReference(ref mainRoomScene, componentsInChildren2[3], "MainRoomScene");
		InitSceneReference(ref doggyScene, componentsInChildren2[4], "DoggyScene");
		InitSceneReference(ref blowjobScene, componentsInChildren2[5], "BlowjobScene");
		InitSceneReference(ref cowgirlScene, componentsInChildren2[6], "CowgirlScene");
		InitSceneReference(ref roomStoryScene, componentsInChildren2[7], "RoomStoryScene");
		InitSceneReference(ref missionaryScene, componentsInChildren2[8], "Missionary Scene");
		InitSceneReference(ref showerScene, componentsInChildren2[9], "Shower Scene");
		InitSceneReference(ref showerSceneNoBot, componentsInChildren2[10], "ShowerSceneNoBot");
		InitSceneReference(ref cuddleScene, componentsInChildren2[11], "Cuddle Scene");
		InitSceneReference(ref clothierScene, componentsInChildren2[12], "Clothier Scene");
		InitSceneReference(ref shopScene, componentsInChildren2[13], "Shop Scene");
		InitSceneReference(ref changingRoomScene, componentsInChildren2[14], "Changing Room Scene");
		InitSceneReference(ref menuScene, componentsInChildren2[15], "Menu Scene");
		InitSceneReference(ref pCScene, componentsInChildren2[16], "PC Scene");
		InitSceneReference(ref outsideScene, componentsInChildren2[17], "Outside Scene");
		InitSceneReference(ref developerRoomScene, componentsInChildren2[18], "Developer Room Scene");
	}
}
