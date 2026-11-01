using System;
using Fungus;
using UnityEngine;

[CommandInfo("My", "Minigame", "Starts a minigame", 0)]
internal class MiniGame : Command
{
	[Tooltip("Score output")]
	[VariableProperty(new Type[] { typeof(FloatVariable) })]
	[SerializeField]
	protected Variable scoreOutput;

	[SerializeField]
	private MinigameCore minigamePrefab;

	private GameObject _instantiatedMinigame;

	private void SetScoreAndContinue(float score)
	{
		if (scoreOutput is FloatVariable floatVariable && floatVariable != null)
		{
			floatVariable.Value = score;
		}
		UnityEngine.Object.Destroy(_instantiatedMinigame);
		Continue();
	}

	public override void OnEnter()
	{
		if (minigamePrefab == null)
		{
			Continue();
			return;
		}
		RectTransform overlayTransform = UiOverlay.Instance.OverlayTransform;
		_instantiatedMinigame = minigamePrefab.InstantiateAndInit(SetScoreAndContinue, overlayTransform);
	}

	public override string GetSummary()
	{
		if (scoreOutput == null)
		{
			return "Error: Variable for score output not selected";
		}
		if (minigamePrefab == null)
		{
			return "Error: Minigame not selected";
		}
		return "Plays " + minigamePrefab.name + " and saves score to " + scoreOutput.Key;
	}

	public override bool HasReference(Variable var)
	{
		return var == scoreOutput;
	}

	public override Color GetButtonColor()
	{
		return new Color32(255, 0, 0, 255);
	}
}
