using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class SayDialogPopup : StaticPopup
{
	[SerializeField]
	private Button skipButton;

	[SerializeField]
	private Button narrationLogButton;

	public bool IsBlocking;

	public SayDialog sayDialog;

	private Writer _writer;

	private Button _button;

	private NonDrawingGraphicMy _nonDrawingGraphic;

	private float _lastFloatingTextTime;

	private const float SKIP_PROMPT_COOLDOWN = 1.5f;

	private void SetButtonActive(bool active)
	{
		if (IsBlocking)
		{
			active = true;
		}
		_button.enabled = active;
		_nonDrawingGraphic.enabled = active;
	}

	public void NextLine()
	{
		_writer.OnNextLineEvent(Writer.InputFlagState.NormalInput);
	}

	public void NextLineSkip(bool forced)
	{
		if (forced)
		{
			_writer.OnNextLineEvent(Writer.InputFlagState.NormalInput);
		}
		else if (_writer.IsWritingNewTextAndUnseenSkipIsDisabled())
		{
			float unscaledTime = Time.unscaledTime;
			if (unscaledTime - _lastFloatingTextTime >= 1.5f)
			{
				_lastFloatingTextTime = unscaledTime;
				UiOverlay.Instance.ShowFloatingText(UiOverlay.Instance.ViewportToWorld(new Vector3(0.5f, 0.5f, 0f)), LOC.UI.UI_NewSkipEnabledNewTextPrompt);
			}
		}
		else
		{
			_writer.OnNextLineEvent(Writer.InputFlagState.SkipInput);
		}
	}

	public void Awake()
	{
		_writer = sayDialog.GetWriter();
		_nonDrawingGraphic = gameObject.GetOrAddComponent<NonDrawingGraphicMy>();
		gameObject.GetOrAddComponent<CanvasRenderer>();
		_button = gameObject.GetOrAddComponent<Button>();
		_button.onClick.SetListener(NextLine);
		if (skipButton != null && skipButton.TryGetComponent<TooltipDisplayArea>(out var component))
		{
			component.TextFunc = () => LOC.UI.UI_SkipButtonTooltip;
		}
		if (narrationLogButton != null && narrationLogButton.TryGetComponent<TooltipDisplayArea>(out var component2))
		{
			component2.TextFunc = () => LOC.UI.UI_NarrationLogTooltip;
		}
		SetButtonActive(active: false);
		WriterSignals.OnWriterState += OnWriterStateChanged;
	}

	private void OnWriterStateChanged(Writer writer, WriterState writerState)
	{
		if (writer == _writer)
		{
			switch (writerState)
			{
			case WriterState.PauseWaitForInput:
				SetButtonActive(active: true);
				break;
			case WriterState.Resume:
			case WriterState.ResumeWaitForInput:
				SetButtonActive(active: false);
				break;
			}
		}
	}

	public override bool CloseInner()
	{
		return false;
	}

	public void Update()
	{
		Vector2 mouseScrollDelta = Input.mouseScrollDelta;
		if (mouseScrollDelta.y == 0f || !Application.isFocused)
		{
			return;
		}
		UiOverlay instance = UiOverlay.Instance;
		if (instance.MaxSortOrder < sortOrder && instance.IsMouseInsideWindow() && !instance.IsEditingInputField())
		{
			if (mouseScrollDelta.y < 0f)
			{
				NextLine();
			}
			else if (sayDialog.gameObject.activeSelf)
			{
				UiOverlay.Instance.OpenNarrativeLog();
			}
		}
	}
}
