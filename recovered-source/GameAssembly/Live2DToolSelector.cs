using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Live2DToolSelector : MonoBehaviour
{
	public class Live2DTool
	{
		public SimpleLocalizedString LocalizedString;

		public int Live2DLayer;

		public CursorSingleton.CursorType CursorType;

		public Live2DToolSelectorIcon Icon;

		public string Name => LocalizedString.Value;
	}

	public enum Live2DToolSelectorIcon
	{
		Hand,
		Mouth,
		Remove,
		Soap,
		Sponge
	}

	[SerializeField]
	private Button Button;

	[SerializeField]
	private TextMeshProUGUI Description;

	private Image _iconImage;

	private TooltipDisplayArea tooltipDisplayArea;

	private Live2DController _controller;

	private int _currentTool;

	private List<Live2DTool> _tools = new List<Live2DTool>();

	private CursorSingleton.CursorOverride _cursorOverride;

	public static readonly Live2DTool HandTool = new Live2DTool
	{
		LocalizedString = LOC.UI.TupleReferences.Live2DTool_Hand,
		Live2DLayer = 0,
		Icon = Live2DToolSelectorIcon.Hand
	};

	public static readonly Live2DTool MouthTool = new Live2DTool
	{
		LocalizedString = LOC.UI.TupleReferences.Live2DTool_Mouth,
		Live2DLayer = 1,
		CursorType = CursorSingleton.CursorType.Kiss,
		Icon = Live2DToolSelectorIcon.Mouth
	};

	public static readonly Live2DTool RemoveClothesTool = new Live2DTool
	{
		LocalizedString = LOC.UI.TupleReferences.Live2DTool_Remove,
		Live2DLayer = 2,
		Icon = Live2DToolSelectorIcon.Remove
	};

	public static readonly Live2DTool SoapTool = new Live2DTool
	{
		LocalizedString = LOC.UI.TupleReferences.Live2DTool_Soap,
		Live2DLayer = 3,
		Icon = Live2DToolSelectorIcon.Soap,
		CursorType = CursorSingleton.CursorType.Soap
	};

	public static readonly Live2DTool SpongeTool = new Live2DTool
	{
		LocalizedString = LOC.UI.TupleReferences.Live2DTool_Sponge,
		Live2DLayer = 4,
		Icon = Live2DToolSelectorIcon.Sponge,
		CursorType = CursorSingleton.CursorType.Sponge
	};

	private static readonly Live2DTool[] CommonTools = new Live2DTool[3] { HandTool, MouthTool, RemoveClothesTool };

	private bool Initialized;

	public void Initialize()
	{
		if (!Initialized)
		{
			Button.onClick.SetListener(ChangeButtonClicked);
			_iconImage = Button.transform.Find("Image").GetComponent<Image>();
			tooltipDisplayArea = Button.GetComponent<TooltipDisplayArea>();
			Initialized = true;
		}
	}

	public void Awake()
	{
		Initialize();
	}

	public void ChangeController(Live2DController controller, IEnumerable<Live2DTool> targetTools = null)
	{
		Initialize();
		if (_controller != null)
		{
			_controller.CurrentToolSelector = null;
		}
		_controller = controller;
		_controller.CurrentToolSelector = this;
		_tools = _tools.ClearOrCreate();
		if (targetTools != null)
		{
			_tools.AddRange(targetTools);
		}
		else
		{
			_tools.AddRange(CommonTools);
		}
		_currentTool = 0;
		SyncCurrentTool();
		SetText("");
	}

	public void SyncCurrentTool()
	{
		if (_currentTool >= 0 && _currentTool < _tools.Count)
		{
			Button.gameObject.SetActive(value: true);
			Live2DTool live2DTool = _tools[_currentTool];
			_controller?.ChangeLayer(live2DTool.Live2DLayer);
			StringBuilderCache.AcquireWithUsing(out var stringBuilder);
			stringBuilder.AppendLine(LOC.UI.Live2DTool_CurrentTool(live2DTool.Name));
			stringBuilder.Append(LOC.UI.Live2DTool_TabTip);
			tooltipDisplayArea.tooltipText = stringBuilder.ToString();
			tooltipDisplayArea.ForceTextUpdate();
			switch (live2DTool.Icon)
			{
			default:
				_iconImage.sprite = R.Icons.Icon_hand.Get();
				break;
			case Live2DToolSelectorIcon.Mouth:
				_iconImage.sprite = R.Icons.Icon_tongue.Get();
				break;
			case Live2DToolSelectorIcon.Remove:
				_iconImage.sprite = R.Icons.Icon_trash.Get();
				break;
			case Live2DToolSelectorIcon.Soap:
				_iconImage.sprite = R.Icons.Icon_Soap.Get();
				break;
			case Live2DToolSelectorIcon.Sponge:
				_iconImage.sprite = R.Icons.Icon_Sponge.Get();
				break;
			}
			if (live2DTool.CursorType != CursorSingleton.CursorType.Null)
			{
				CursorSingleton.ContinueOrStartCursorOverride(ref _cursorOverride, live2DTool.CursorType);
			}
			else
			{
				CursorSingleton.EndCursorOverride(ref _cursorOverride);
			}
		}
		else
		{
			Button.gameObject.SetActive(value: false);
			_controller?.ChangeLayer(-1);
			CursorSingleton.EndCursorOverride(ref _cursorOverride);
		}
	}

	private void ChangeButtonClicked()
	{
		_currentTool++;
		if (_currentTool >= _tools.Count)
		{
			_currentTool = 0;
		}
		SyncCurrentTool();
	}

	public void SetCurrentTool(int toolIndex)
	{
		_currentTool = toolIndex;
		SyncCurrentTool();
	}

	public void SetCurrentTool(Live2DTool tool)
	{
		_currentTool = _tools.IndexOf(tool);
		if (_currentTool < 0)
		{
			Debug.LogError("Tool " + tool.Name + " not found in " + name + "!");
		}
		SyncCurrentTool();
	}

	public void SetText(string text)
	{
		Description.text = text;
	}

	public void OnEnable()
	{
		_currentTool = 0;
		SyncCurrentTool();
	}

	public void OnDisable()
	{
		if (gameObject.scene.isLoaded)
		{
			_currentTool = -1;
			SyncCurrentTool();
		}
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && !UiOverlay.Instance.IsEditingInputField() && Button.IsInteractable())
		{
			ChangeButtonClicked();
		}
	}
}
