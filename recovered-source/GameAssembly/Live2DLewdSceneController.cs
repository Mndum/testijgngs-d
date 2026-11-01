using System.Linq;
using UnityEngine;

public abstract class Live2DLewdSceneController : Live2DController
{
	public struct CumOutsideCockValues(float angleX, float angleY, float cumshotLength)
	{
		public float AngleX = angleX;

		public float AngleY = angleY;

		public float CumshotLength = cumshotLength;
	}

	public virtual string CameraCumPartPostfix => "";

	public virtual Part GetCurrentlyUsedDickPart()
	{
		return GetGroup(ColoringGroupEnum.MCCockSkin).Parts.First();
	}

	public abstract Vector2 GetCockDirection();

	public virtual float GetCockMaxAngle()
	{
		return 35f;
	}

	public CumOutsideCockValues CalculateCumOutsideAimParameters(CumOutsideManager.CumLive2DRepresentation target)
	{
		Rect rect = GetCurrentlyUsedDickPart().MeshRenderer.bounds.ToRectIgnoreZ();
		if (target.TryGetPartFor(CameraCumPartPostfix, out var part))
		{
			Vector2 cockDirection = GetCockDirection();
			Vector2 vector = (Vector2)part.CalculateWorldBoundsCenter() - rect.GetPointOnRectEdgeInDirection(-cockDirection);
			float num = Vector2.SignedAngle(vector, cockDirection);
			float magnitude = vector.magnitude;
			if (cockDirection == Vector2.up)
			{
				float angleX = num / GetCockMaxAngle();
				float angleY = magnitude / 18f;
				float cumshotLength = magnitude / 9f - 0.2f;
				return new CumOutsideCockValues(angleX, angleY, cumshotLength);
			}
			if (cockDirection == Vector2.right || cockDirection == Vector2.left)
			{
				float angleX2 = Random.Range(-0.2f, 0.2f);
				float num2 = num / GetCockMaxAngle();
				if (cockDirection == Vector2.right)
				{
					num2 = 0f - num2;
				}
				float cumshotLength2 = magnitude / 9f - 0.2f;
				return new CumOutsideCockValues(angleX2, num2, cumshotLength2);
			}
			Debug.LogError($"Unsupported cock direction: {cockDirection}! Falling back to center!");
		}
		return new CumOutsideCockValues(Random.Range(-0.2f, 0.2f), Random.Range(-0.1f, 1f), Random.Range(0.3f, 0.6f));
	}

	protected override void SaveSerializableState_Inner(Live2DControllerCommonState state)
	{
		base.SaveSerializableState_Inner(state);
		state.FrickData.S_IsPantsAside = IsPantsAside;
	}

	protected override void LoadSerializableState_Inner(Live2DControllerCommonState state)
	{
		base.LoadSerializableState_Inner(state);
		IsPantsAside = state.FrickData.S_IsPantsAside;
	}

	public void ManifestHorninessAndResetFrickCommonData()
	{
		GameVariables.Current.ManifestHorninessAndResetFrickData();
	}

	public void ActivateBotFullLeadIfPossible()
	{
		if (CurrentBrain.CurrentState is ICommonLewdSceneBrainState commonLewdSceneBrainState)
		{
			commonLewdSceneBrainState.SetBrainFrickControlStatus(BotFrickControlStatusEnum.BotFullLead);
			if (commonLewdSceneBrainState is CowgirlBrainState cowgirlBrainState)
			{
				cowgirlBrainState.React(CowgirlBrainState.CowgirlReactions.Insert);
			}
		}
	}
}
