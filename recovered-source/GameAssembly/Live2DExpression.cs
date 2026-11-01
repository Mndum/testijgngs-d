using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Live2DExpression : Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_Update, Live2DController.ILive2DControllerManager_Callback, Live2DController.ILive2DControllerManager_ResetStage1
{
	public enum ExpressionModifierTypeEnum
	{
		Happiness,
		Lewdness,
		Shock,
		Angry,
		Smug
	}

	public struct ExpressionModifier(ExpressionModifierTypeEnum type, float fullTime, float value)
	{
		public ExpressionModifierTypeEnum Type = type;

		public float TimeElapsed = 0f;

		public float FullTime = fullTime;

		public float Value = value;
	}

	public abstract class ExpressionStateBase : BasicStateMachineState<ExpressionStateBase, Live2DExpression>
	{
		public Live2DController Controller => Owner._controller;

		public abstract void ApplyExpressionToModel(ExpressionValues values);
	}

	public class BasicExpressionState : ExpressionStateBase
	{
		public override void EnterState()
		{
		}

		public override void ApplyExpressionToModel(ExpressionValues values)
		{
			if (values.Angry > 0.05f)
			{
				Owner.SetBrowsRot((0f - values.Angry) * 1.3f);
				Owner.SetMouthForm((0f - values.Angry) / 2f + Mathf.Min(0f, values.Happiness / 2f));
				Owner.SetEyesHappiness(0f);
				Owner.SetBrowsEmote(0f);
				Owner.SetBrowsY(0f - values.Angry);
			}
			else
			{
				Owner.SetBrowsRot(0f);
				if (Controller is ILive2DController_CatEarsMovement { ParamEarL: var paramEarL, ParamEarR: var paramEarR })
				{
					float value = (paramEarR.Value = values.Happiness);
					paramEarL.Value = value;
				}
				if (values.Happiness <= 0f)
				{
					Owner.SetMouthForm(values.Happiness);
					Owner.SetEyesHappiness(values.Happiness);
					Owner.SetBrowsEmote(values.Happiness);
				}
				else if (values.Happiness <= 0.7f)
				{
					Owner.SetMouthForm(values.Happiness / 2f);
					Owner.SetEyesHappiness(values.Happiness / 2f);
					Owner.SetBrowsEmote(values.Happiness / 2f);
				}
				else
				{
					Owner.SetMouthForm(1f);
					Owner.SetEyesHappiness(values.Happiness / 2f);
					Owner.SetBrowsEmote(values.Happiness / 2f);
				}
			}
			if (Controller is ILive2DController_BlushAndHeartEyes live2DController_BlushAndHeartEyes)
			{
				if (values.Lewdness < 0.25f)
				{
					live2DController_BlushAndHeartEyes.ParamBlush.Value = 0f;
				}
				else if (values.Lewdness < 1f)
				{
					live2DController_BlushAndHeartEyes.ParamBlush.Value = values.Lewdness;
				}
				else
				{
					live2DController_BlushAndHeartEyes.ParamBlush.Value = values.Lewdness;
				}
			}
			Controller.Eyes.SetHeartEyesTarget(values.Lewdness >= 1f);
			if (values.Shock < 0.1f)
			{
				Controller.Eyes.SetShockEyesValue(0f);
				Owner.SetBrowsY(0f);
			}
			else
			{
				Controller.Eyes.SetShockEyesValue(values.Shock);
				Owner.SetBrowsY(values.Shock);
			}
			if (Controller is ILive2DController_EyesSmugEmote live2DController_EyesSmugEmote)
			{
				live2DController_EyesSmugEmote.ParamEyeExpression.Value = 0f - values.Smug;
			}
		}

		public override void ExitState()
		{
		}
	}

	public class CummingExpressionState : ExpressionStateBase, IDisposable
	{
		public readonly float Length;

		public readonly float Intensity;

		public readonly int NumberOfTwitches;

		public readonly float AdjustedIntensity;

		public readonly float TwitchLength;

		public CummingExpressionState()
		{
			Intensity = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CurrentHorniness;
			AdjustedIntensity = 0.2f + Intensity * 0.8f;
			Length = 3f + Intensity * 5f + UnityEngine.Random.Range(0f, 2f);
			NumberOfTwitches = 15 + Mathf.CeilToInt(25f * Intensity) + UnityEngine.Random.Range(0, 5);
			TwitchLength = Length / (float)NumberOfTwitches * 0.9f;
		}

		public override void EnterState()
		{
			Controller.FireEvent(InteractionMemoryEventEnum.BotOrgasm);
			Owner.SetBrowsRot(0.5f);
			Owner.SetBrowsEmote(-0.1f);
			bool flag = Intensity > 0.4f;
			if (Controller is ILive2DController_SalivaMouth live2DController_SalivaMouth)
			{
				live2DController_SalivaMouth.ParamSaliva.SetPercent(1f);
			}
			if (!(Controller is Live2DDoggyFuckController))
			{
				if (Controller is ILive2DController_Mouth live2DController_Mouth)
				{
					live2DController_Mouth.ParamMouthForm.SetPercent(0.5f + AdjustedIntensity / 2f);
					if (flag)
					{
						if (UnityEngine.Random.value > 0.5f)
						{
							Controller.Mouth.OpenMouthFull();
						}
						else
						{
							Controller.Mouth.SetMouthAtLeast(UnityEngine.Random.Range(1.5f, 1.9f));
						}
					}
					else if (UnityEngine.Random.value > 0.5f)
					{
						Controller.Mouth.SetMouthAtLeast(1f);
					}
					else
					{
						Controller.Mouth.SetMouthAtLeast(UnityEngine.Random.Range(0.7f, 1f));
					}
				}
				if (flag)
				{
					if (!(UnityEngine.Random.value > 0.7f) || !Controller.Eyes.TryLookAtCurrentInteractable())
					{
						Controller.Eyes.SetEyesPosition(-0.4f, 0.3f * AdjustedIntensity, Live2DController.EyeSelection.LeftEye);
						Controller.Eyes.SetEyesPosition(0.4f, 0.3f * AdjustedIntensity, Live2DController.EyeSelection.RightEye);
					}
					Controller.Eyes.HalfCloseEyes(UnityEngine.Random.Range(0.2f, 0.8f));
				}
				else
				{
					if (UnityEngine.Random.value > 0.5f)
					{
						Controller.Eyes.CloseEyesForSeconds(Length);
					}
					else
					{
						Controller.Eyes.HalfCloseEyes(UnityEngine.Random.Range(0.2f, 0.5f));
					}
					if (!(UnityEngine.Random.value > 0.5f) || !Controller.Eyes.TryLookAtCurrentInteractable())
					{
						Controller.Eyes.SetEyesPosition(0f, UnityEngine.Random.Range(0.1f, 0.2f));
					}
				}
			}
			Controller.Eyes.SetHeartEyesTarget(enable: true);
			if (Controller is ILive2DController_EyesSmugEmote { ParamEyeExpression: var paramEyeExpression })
			{
				paramEyeExpression.SetPercent((UnityEngine.Random.value > 0.5f) ? UnityEngine.Random.Range(0.4f, 0.8f) : UnityEngine.Random.Range(-0.8f, -0.4f));
			}
			Controller.Eyes.DisableBlinking = true;
			Controller.Mouth.DisableLetterOutputToMouth = true;
		}

		public override void ApplyExpressionToModel(ExpressionValues values)
		{
			if (Controller is ILive2DController_EyesHappinessEmote live2DController_EyesHappinessEmote)
			{
				live2DController_EyesHappinessEmote.ParamEyesHappy.Value = values.Happiness + AdjustedIntensity;
			}
			if (Controller is ILive2DController_BlushAndHeartEyes live2DController_BlushAndHeartEyes)
			{
				live2DController_BlushAndHeartEyes.ParamBlush.SetPercent(values.Lewdness + AdjustedIntensity * 2f);
			}
		}

		public void DoQuiver1()
		{
			if (Controller is ILive2DController_HeadY live2DController_HeadY)
			{
				live2DController_HeadY.ParamHeadY.AddPercent(UnityEngine.Random.Range(-0.05f, 0.2f));
			}
			if (Controller is ILive2DController_BodyY live2DController_BodyY)
			{
				live2DController_BodyY.ParamBodyY.AddPercent(UnityEngine.Random.Range(-0.1f, 0.1f));
				live2DController_BodyY.ParamBodyY.AddPercent(UnityEngine.Random.Range(0.05f, 0.06f) * AdjustedIntensity);
			}
			if (Controller is ILive2DController_Lewd_BotOrgasm live2DController_Lewd_BotOrgasm)
			{
				live2DController_Lewd_BotOrgasm.ParamOrgasm.SetPercent(AdjustedIntensity);
			}
			if (Controller is ILive2DController_Lewd_LegQuiver live2DController_Lewd_LegQuiver)
			{
				live2DController_Lewd_LegQuiver.ParamLegsQuiver.SetPercent(AdjustedIntensity);
			}
			if (Controller is ILive2DController_Lewd_BotTense { ParamTense: var paramTense })
			{
				paramTense.Value += UnityEngine.Random.Range(0.1f, 0.1f + Intensity * 0.5f);
			}
		}

		public void DoQuiver2()
		{
			if (Controller is ILive2DController_BodyY live2DController_BodyY)
			{
				live2DController_BodyY.ParamBodyY.AddPercent(UnityEngine.Random.Range(-0.06f, -0.05f) * AdjustedIntensity);
			}
		}

		public void AfterQuiver()
		{
			if (Controller is ILive2DController_Lewd_BotOrgasm live2DController_Lewd_BotOrgasm)
			{
				live2DController_Lewd_BotOrgasm.ParamOrgasm.ResetToDefault();
			}
			if (Controller is ILive2DController_Lewd_LegQuiver live2DController_Lewd_LegQuiver)
			{
				live2DController_Lewd_LegQuiver.ParamLegsQuiver.ResetToDefault();
			}
			if (Controller is ILive2DController_Lewd_BotTense live2DController_Lewd_BotTense)
			{
				live2DController_Lewd_BotTense.ParamTense.ResetToDefault();
			}
		}

		public IEnumerator CumFinishTimeOut()
		{
			GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
			gameVariables.lust += 10;
			gameVariables.CurrentHorniness += 0.2f;
			Controller.CurrentBrain.Pleasure /= 5f;
			gameVariables.Longing--;
			gameVariables.Longing /= 2f;
			yield return WaitForSecondCache.Seconds2;
		}

		public override void ExitState()
		{
			Controller.Eyes.DisableBlinking = false;
			Controller.Mouth.DisableLetterOutputToMouth = false;
			Controller.Eyes.SetHeartEyesTarget(enable: false);
			GameVariables current = GameVariables.Current;
			current.lastBotCameAt = GameVariables.Current.time;
			current.botLive2DCommonState.FrickData.D_TimesBotCame++;
		}

		public void Dispose()
		{
			Owner.EndCumming();
		}
	}

	public struct ExpressionValues
	{
		public float Happiness;

		public float Lewdness;

		public float Shock;

		public float Angry;

		public float Smug;

		public void Clamp()
		{
			Happiness = Mathf.Clamp(Happiness, -1f, 1f);
			Lewdness = Mathf.Clamp(Lewdness, 0f, 2f);
			Shock = Mathf.Clamp(Shock, 0f, 1f);
			Angry = Mathf.Clamp(Angry, 0f, 1f);
			Smug = Mathf.Clamp(Smug, 0f, 1f);
		}
	}

	private Live2DController _controller;

	private List<ExpressionModifier> _modifiers = new List<ExpressionModifier>();

	private BasicStateMachine<ExpressionStateBase, Live2DExpression> _expressionStateMachine;

	private ExpressionValues _lastExpressionValues;

	public bool DisableExpression { get; set; }

	public bool IsCumming => _expressionStateMachine.CurrentState is CummingExpressionState;

	public float LastHappinessValue => _lastExpressionValues.Happiness;

	public float LastLewdnessValue => _lastExpressionValues.Lewdness;

	public Live2DExpression(Live2DController controller)
	{
		_controller = controller;
		_expressionStateMachine = new BasicStateMachine<ExpressionStateBase, Live2DExpression>(this);
	}

	public void ClearExpression()
	{
		_modifiers.Clear();
		ReturnToBasicExpressionState();
	}

	public void AddModifier(ExpressionModifierTypeEnum type, float value, float time = 5f)
	{
		ExpressionModifier expressionModifier = new ExpressionModifier(type, time, value);
		int num = FindModifierIndex(type);
		if (num >= 0)
		{
			_modifiers[num] = expressionModifier;
		}
		else
		{
			_modifiers.Add(expressionModifier);
		}
	}

	public string GetDebugString()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine("Current Expression Values (clamped):");
			stringBuilder.AppendLine("──────────────────────────────────────────────────────────────");
			stringBuilder.AppendLine($"Happiness : {_lastExpressionValues.Happiness,6:F3}   range [-1.0 .. 1.0]");
			stringBuilder.AppendLine($"Lewdness  : {_lastExpressionValues.Lewdness,6:F3}    range [0.0 .. 2.0]");
			stringBuilder.AppendLine($"Shock     : {_lastExpressionValues.Shock,6:F3}       range [0.0 .. 1.0]");
			stringBuilder.AppendLine($"Angry     : {_lastExpressionValues.Angry,6:F3}       range [0.0 .. 1.0]");
			stringBuilder.AppendLine($"Smug      : {_lastExpressionValues.Smug,6:F3}        range [0.0 .. 1.0]");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Modifiers: ");
			if (_modifiers.Count == 0)
			{
				stringBuilder.Append("none");
			}
			else
			{
				for (int i = 0; i < _modifiers.Count; i++)
				{
					ExpressionModifier expressionModifier = _modifiers[i];
					string arg = ((expressionModifier.FullTime == 1f / 0f) ? "∞" : $"{expressionModifier.TimeElapsed:F1}/{expressionModifier.FullTime:F1}");
					if (i > 0)
					{
						stringBuilder.Append("  •  ");
					}
					stringBuilder.AppendLine($"{expressionModifier.Type}={expressionModifier.Value:F2}({arg})");
				}
			}
			return stringBuilder.ToString();
		}
	}

	private int FindModifierIndex(ExpressionModifierTypeEnum type)
	{
		for (int i = 0; i < _modifiers.Count; i++)
		{
			if (_modifiers[i].Type == type)
			{
				return i;
			}
		}
		return -1;
	}

	public void ResetStage1()
	{
		ClearExpression();
	}

	[MustDisposeResource]
	public CummingExpressionState StartCumming()
	{
		CummingExpressionState cummingExpressionState = _expressionStateMachine.CurrentState as CummingExpressionState;
		if (cummingExpressionState == null)
		{
			cummingExpressionState = new CummingExpressionState();
			_expressionStateMachine.ChangeState(cummingExpressionState);
		}
		return cummingExpressionState;
	}

	public void EndCumming()
	{
		ReturnToBasicExpressionState();
	}

	private void ReturnToBasicExpressionState()
	{
		if (!(_expressionStateMachine.CurrentState is BasicExpressionState))
		{
			_expressionStateMachine.ChangeState(new BasicExpressionState());
		}
	}

	private void ApplyExpressionToModel(ExpressionValues values)
	{
		_expressionStateMachine.CurrentState.ApplyExpressionToModel(values);
		_lastExpressionValues = values;
	}

	public void Update()
	{
		if (!GameScript.HasGameStarted || !SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotAwake || DisableExpression)
		{
			return;
		}
		ModelBrain currentBrain = _controller.CurrentBrain;
		GameVariables current = GameVariables.Current;
		ExpressionValues values = default;
		if (currentBrain == null || !current.IsBotAwake)
		{
			_expressionStateMachine.CurrentState.ApplyExpressionToModel(values);
			return;
		}
		bool flag = currentBrain.CurrentState?.IsStoryState ?? false;
		values.Happiness = (flag ? (current.Mood * 0.05f) : (current.Mood * 0.4f));
		values.Lewdness = Mathf.Max(current.CurrentHorniness + currentBrain.Pleasure, currentBrain.Pleasure * 1.1f);
		values.Shock = currentBrain.Pain;
		for (int num = _modifiers.Count - 1; num >= 0; num--)
		{
			ExpressionModifier value = _modifiers[num];
			if (!flag)
			{
				value.TimeElapsed += Time.deltaTime;
				if (value.TimeElapsed >= value.FullTime)
				{
					_modifiers.RemoveAt(num);
					continue;
				}
				_modifiers[num] = value;
			}
			switch (value.Type)
			{
			case ExpressionModifierTypeEnum.Happiness:
				values.Happiness += value.Value;
				break;
			case ExpressionModifierTypeEnum.Lewdness:
				values.Lewdness += value.Value;
				break;
			case ExpressionModifierTypeEnum.Shock:
				values.Shock += value.Value;
				break;
			case ExpressionModifierTypeEnum.Angry:
				values.Angry += value.Value;
				break;
			case ExpressionModifierTypeEnum.Smug:
				values.Smug += value.Value;
				break;
			}
		}
		values.Clamp();
		ApplyExpressionToModel(values);
	}

	private void SetBrowsY(float value)
	{
		if (_controller is ILive2DController_BrowLY live2DController_BrowLY)
		{
			live2DController_BrowLY.ParamBrowLY.SetPercentDirectional(value);
		}
		if (_controller is ILive2DController_BrowRY live2DController_BrowRY)
		{
			live2DController_BrowRY.ParamBrowRY.SetPercentDirectional(value);
		}
	}

	private void SetBrowsEmote(float value)
	{
		if (_controller is ILive2DController_BrowLEmote live2DController_BrowLEmote)
		{
			live2DController_BrowLEmote.ParamBrowLEmote.Value = value;
		}
		if (_controller is ILive2DController_BrowREmote live2DController_BrowREmote)
		{
			live2DController_BrowREmote.ParamBrowREmote.Value = value;
		}
	}

	private void SetBrowsRot(float value)
	{
		if (_controller is ILive2DController_BrowLRot live2DController_BrowLRot)
		{
			live2DController_BrowLRot.ParamBrowLRot.SetPercentDirectional(value);
		}
		if (_controller is ILive2DController_BrowRRot live2DController_BrowRRot)
		{
			live2DController_BrowRRot.ParamBrowRRot.SetPercentDirectional(value);
		}
	}

	private void SetMouthForm(float value)
	{
		if (_controller is ILive2DController_Mouth live2DController_Mouth)
		{
			live2DController_Mouth.ParamMouthForm.Value = value;
		}
	}

	private void SetEyesHappiness(float value)
	{
		if (_controller is ILive2DController_EyesHappinessEmote live2DController_EyesHappinessEmote)
		{
			live2DController_EyesHappinessEmote.ParamEyesHappy.Value = value;
		}
	}
}
