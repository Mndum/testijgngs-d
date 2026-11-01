using System.Collections.Generic;
using System.Linq;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Physics;
using Live2D.Cubism.Framework.Raycasting;
using UnityEngine;

public sealed class Live2DCowgirlController : Live2DLewdSceneController, ILive2DController_HeadAngles, ILive2DController_HeadX, ILive2DController_HeadY, ILive2DController_HeadZ, ILive2DController_CatEarsMovement, ILive2DController_Headpat, ILive2DController_TongueMovement, ILive2DController_CommonFace, ILive2DController_MainFace, ILive2DController_Mouth, ILive2DController_EyeMovement, ILive2DController_EyeLMovement, ILive2DController_EyeRMovement, ILive2DController_EyeLEmotes, ILive2DController_EyeLCloseAndShock, ILive2DController_BrowLY, ILive2DController_BrowLRot, ILive2DController_BrowLEmote, ILive2DController_EyeREmotes, ILive2DController_EyeRCloseAndShock, ILive2DController_BrowRY, ILive2DController_BrowRRot, ILive2DController_BrowREmote, ILive2DController_BlushAndHeartEyes, ILive2DController_IndependentBlinking, ILive2DController_EyesHappinessEmote, ILive2DController_SalivaMouth, ILive2DController_OmegaMeter, ILive2DController_TailWiggle, ILive2DController_Manager_XRay, ILive2DController_Lewd_CumShotCommon, ILive2DController_Lewd_CumShotBasic, ILive2DController_Lewd_CumShotLength, ILive2DController_Lewd_CumInsideWomb, ILive2DController_Lewd_CumInsideVagina, ILive2DController_Lewd_LegQuiver, ILive2DController_Lewd_BotOrgasm, ILive2DController_Manager_FrickStaticGuiManager, ILive2DController_Manager_InsertionManager, ILive2DController_Manager_CockManager, ILive2DController_BodyMod_ThighSize, ILive2DController_BodyMod_BoobSize, ILive2DController_BodyMod_AssSize
{
	public class CowgirlCockInsertionFuckAxis : InsertionHandlerFuckAxis
	{
		private Live2DCowgirlController _cowgirlController;

		private float _lastInsert;

		private IPenisInfo CurrentPenis = IPenisInfo.DefaultNoPenisInfo;

		public Live2DParameter BotBodyMovementYParameter { get; set; }

		private Live2DParameter ParamCockInside { get; set; }

		public Live2DParameter ThrustParameter { get; private set; }

		private Live2DParameter DickAngleYParameter { get; set; }

		private Live2DParameter DickAngleXParameter { get; set; }

		public bool DisableDickAngle { get; set; }

		public bool IsInsideCowgirl
		{
			get
			{
				return !Mathf.Approximately(ParamCockInside.Value, ParamCockInside.MinimumValue);
			}
			set
			{
				if (IsInsideCowgirl != value)
				{
					if (value)
					{
						ParamCockInside.Value = ParamCockInside.MaximumValue;
					}
					else
					{
						ParamCockInside.Value = ParamCockInside.MinimumValue;
					}
					ParamCockInside.FinishMove();
				}
			}
		}

		public override bool IsInside => IsInsideCowgirl;

		public override bool IsAfterOutside => IsInsideCowgirl;

		public CowgirlCockInsertionFuckAxis(Live2DCowgirlController cowgirlController, Live2DParameter botBodyMovementYParameter, Live2DParameter paramCockInside, Live2DParameter thrustParameter, Live2DParameter dickAngleYParameter, Live2DParameter dickAngleXParameter)
		{
			_cowgirlController = cowgirlController;
			BotBodyMovementYParameter = botBodyMovementYParameter;
			ParamCockInside = paramCockInside;
			ThrustParameter = thrustParameter;
			DickAngleYParameter = dickAngleYParameter;
			DickAngleXParameter = dickAngleXParameter;
		}

		public void PutInside()
		{
			R.Sound.Frick_insert_vagina_All.GetRandom().Get().Play();
			Controller.FireEvent(InteractionMemoryEventEnum.Insert);
			IsInsideCowgirl = true;
		}

		public override void Start(IPenisInfo penisInfo)
		{
			CurrentPenis = penisInfo;
		}

		public override IPenisInfo GetPenisInfo()
		{
			return CurrentPenis;
		}

		public override float GetAngleX()
		{
			return 0f;
		}

		public override float GetAngleY()
		{
			return 0f;
		}

		public override void Reset()
		{
			DickAngleYParameter.ResetToDefault();
			DisableDickAngle = false;
			IsInsideCowgirl = false;
			_lastInsert = 0f;
		}

		public override bool CanThrustForward()
		{
			return ThrustParameter.Value < 0.9f;
		}

		public override bool CanThrustBackwards()
		{
			return ThrustParameter.Value > 0.1f;
		}

		public override void Update()
		{
			float thrustMovementAccumulator = Owner.ThrustMovementAccumulator;
			ThrustParameter.Value += thrustMovementAccumulator;
			ThrustParameter.ClampValue(0f, 1f);
			if (IsInsideCowgirl)
			{
				float value = 1f - BotBodyMovementYParameter.GetRealPercent();
				float realValue = ThrustParameter.GetRealValue();
				float num = EasingFunction.EaseInQuad(0f, 1f, value) + realValue;
				if (num >= 1f)
				{
					float num2 = num - _lastInsert;
					float num3 = num - 1f;
					float targetChange = ThrustParameter.GetTargetChange();
					float targetChange2 = BotBodyMovementYParameter.GetTargetChange();
					if (num2 > 0.01f && _lastInsert < 0.99f)
					{
						float volume = Mathf.Min(0.5f + num2 * 5f, 1f) + Random.Range(0f, 0.1f);
						R.Sound.Frick_slam_All.GetRandom().Get().Play(volume);
					}
					float num4 = Mathf.Abs(targetChange) + Mathf.Abs(targetChange2);
					float num6;
					float num7;
					if (!Mathf.Approximately(0f, num4))
					{
						float num5 = ((targetChange + targetChange2) / num4 + 1f) / 2f;
						num6 = num3 * (1f - num5);
						num7 = num3 * num5;
					}
					else
					{
						num6 = num3 * 0.5f;
						num7 = num3 * 0.5f;
					}
					ThrustParameter.SetOnlyRealValue(ThrustParameter.GetRealValue() - num6);
					BotBodyMovementYParameter.SetOnlyRealValue(BotBodyMovementYParameter.GetRealValue() + num7);
					num = 1f;
				}
				float num8 = Mathf.Abs(num - _lastInsert);
				_cowgirlController.CockManager.AddCumBarValue(num8 * 0.0135f);
				Controller.CurrentBrain.CausePleasure(num8 * 0.015f);
				if (!DisableDickAngle)
				{
					DickAngleYParameter.Value = 0f - (realValue * 0.8f + num * 0.2f);
				}
				_lastInsert = num;
				ReportFuckingSpeedUpdate(ThrustParameter.GetAbsChangeFromLastFrame() * 0.2f, BotBodyMovementYParameter.GetAbsChangeFromLastFrame());
			}
			else
			{
				ReportFuckingSpeedUpdate(0f, 0f);
				_lastInsert = 0f;
			}
		}

		public override float GetCurrentRealInsertionPosition()
		{
			if (!(_lastInsert <= 0f))
			{
				return 0.1f + _lastInsert * 0.9f;
			}
			return -1f;
		}

		public override float GetCurrentInsertionPosition()
		{
			return GetCurrentRealInsertionPosition();
		}

		public override void ResetAccumulatedMovementToCurrentPosition()
		{
			ThrustParameter.Value = ThrustParameter.GetRealValue();
		}
	}

	private Live2DInteractableLimbUsage<Live2DCowgirlController> NippleLInteractable;

	private Live2DInteractableLimbUsage<Live2DCowgirlController> NippleRInteractable;

	private Live2DInteractableLimbUsage<Live2DCowgirlController> NippleBothInteractable;

	private Live2DInteractableLimbUsage<Live2DCowgirlController> BoobLFondleInteractable;

	private Live2DInteractableLimbUsage<Live2DCowgirlController> BoobRFondleInteractable;

	private Live2DInteractableLimbUsage<Live2DCowgirlController> BoobBothFondleInteractable;

	private Live2DInteractableLimbUsage<Live2DCowgirlController> ClitRubInteractable;

	private Live2DInteractableLimbUsage<Live2DCowgirlController> FingeringInteractable;

	private CockLimbUsage<Live2DCowgirlController> _cockFuckLimbUsage;

	public override ControllerEnum ControllerEnum => ControllerEnum.Cowgirl;

	public override SceneEnumFlag SceneEnum => SceneEnumFlag.Cowgirl;

	public Live2DInteractableLimbUsage<Live2DCowgirlController> LegsGrabInteractable { get; private set; }

	public override bool IsPantiesAside => !Mathf.Approximately(0f, ParamToSide.Value);

	public override bool IsPantsAside
	{
		get
		{
			return !Mathf.Approximately(0f, ParamPantsToSide.Value);
		}
		set
		{
			ParamPantsToSide.Value = (value ? ParamPantsToSide.MaximumValue : ParamPantsToSide.MinimumValue);
			ParamPantsToSide.FinishMove();
		}
	}

	public CowgirlCockInsertionFuckAxis VaginaCockInsertionFuckAxis { get; private set; }

	public BasicInsertionHandleFuckAxis VaginaFingeringFuckAxis { get; private set; }

	public InsertionHandler VaginaInsertionHandler { get; private set; }

	public bool McBodyVisible
	{
		get
		{
			return ParamMCBodyVisible.Get01Bool();
		}
		set
		{
			ParamMCBodyVisible.Set01Bool(value);
		}
	}

	public IXRayManager XRayManager { get; private set; }

	public FrickStaticGuiManager FrickStaticGuiManager { get; private set; }

	public InsertionManager InsertionManager { get; private set; }

	public ICockManager CockManager { get; private set; }

	public Live2DParameter Param_Angle_Rotation_1_TailMain { get; private set; }

	public Live2DParameter Param_Angle_Rotation_2_TailMain { get; private set; }

	public Live2DParameter Param_Angle_Rotation_3_TailMain { get; private set; }

	public Live2DParameter Param_Angle_Rotation_4_TailMain { get; private set; }

	public Live2DParameter Param_Angle_Rotation_5_TailMain { get; private set; }

	public Live2DParameter Param_Angle_Rotation_6_TailMain { get; private set; }

	public Live2DParameter Param_Angle_Rotation_7_TailMain { get; private set; }

	public Live2DParameter Param_Angle_Rotation_8_TailMain { get; private set; }

	public Live2DParameter Param_Angle_Rotation_9_TailMain { get; private set; }

	public Live2DParameter ParamTailWiggle { get; private set; }

	public Live2DParameter ParamBrowRY { get; private set; }

	public Live2DParameter ParamBrowLY { get; private set; }

	public Live2DParameter ParamEyeRShock { get; private set; }

	public Live2DParameter ParamEyeLShock { get; private set; }

	public Live2DParameter ParamBrowLRot { get; private set; }

	public Live2DParameter ParamBrowRRot { get; private set; }

	public Live2DParameter ParamBrowLEmote { get; private set; }

	public Live2DParameter ParamBrowREmote { get; private set; }

	public Live2DParameter ParamEyeOpen { get; private set; }

	public Live2DParameter ParamEyesHappy { get; private set; }

	public Live2DParameter ParamIrisZoom { get; private set; }

	public Live2DParameter ParamHeart { get; private set; }

	public Live2DParameter ParamEyeballRX { get; private set; }

	public Live2DParameter ParamEyeballRY { get; private set; }

	public Live2DParameter ParamEyeballLX { get; private set; }

	public Live2DParameter ParamEyeballLY { get; private set; }

	public Live2DParameter ParamMouthOpen { get; private set; }

	public Live2DParameter ParamSaliva { get; private set; }

	public Live2DParameter ParamMouthForm { get; private set; }

	public Live2DParameter ParamToungeX { get; private set; }

	public Live2DParameter ParamToungeY { get; private set; }

	public Live2DParameter ParamBlush { get; private set; }

	public Live2DParameter ParamHeadZ { get; private set; }

	public Live2DParameter ParamHeadX { get; private set; }

	public Live2DParameter ParamHeadY { get; private set; }

	public Live2DParameter ParamEarL { get; private set; }

	public Live2DParameter ParamEarR { get; private set; }

	public Live2DParameter ParamChestX { get; private set; }

	public Live2DParameter ParamSalivaPhysics { get; private set; }

	public Live2DParameter ParamHairX { get; private set; }

	public Live2DParameter ParamPupilWiggle { get; private set; }

	public Live2DParameter ParamEarsWiggle { get; private set; }

	public Live2DParameter ParamHeadpatX { get; private set; }

	public Live2DParameter ParamHeadpatY { get; private set; }

	public Live2DParameter ParamHeadpat { get; private set; }

	public Live2DParameter ParamHairPhysicsBaseToShort { get; private set; }

	public Live2DParameter ParamHairPhysicsShortToMid { get; private set; }

	public Live2DParameter ParamHairPhysicsMidToLong { get; private set; }

	public Live2DParameter ParamPhysicsBoobXL { get; private set; }

	public Live2DParameter ParamPhysicsBoobYL { get; private set; }

	public Live2DParameter ParamPhysicsBoobXR { get; private set; }

	public Live2DParameter ParamPhysicsBoobYR { get; private set; }

	public Live2DParameter ParamHoodieStringL { get; private set; }

	public Live2DParameter ParamHoodieStringR { get; private set; }

	public Live2DParameter ParamSkirtYPhysics { get; private set; }

	public Live2DParameter ParamGrabBoobLEnable { get; private set; }

	public Live2DParameter ParamBoobSqueezeL { get; private set; }

	public Live2DParameter ParamGrabBoobREnable { get; private set; }

	public Live2DParameter ParamBoobSqueezeR { get; private set; }

	public Live2DParameter ParamNippleREnable { get; private set; }

	public Live2DParameter ParamNippleRSqueeze { get; private set; }

	public Live2DParameter ParamNippleLEnable { get; private set; }

	public Live2DParameter ParamNippleLSqueeze { get; private set; }

	public Live2DParameter ParamGrabLegREnable { get; private set; }

	public Live2DParameter ParamGrabLegLEnable { get; private set; }

	public Live2DParameter ParamFingeringEnable { get; private set; }

	public Live2DParameter ParamFingeringThrust { get; private set; }

	public Live2DParameter ParamFingeringHandWet { get; private set; }

	public Live2DParameter ParamFingeringAngleX { get; private set; }

	public Live2DParameter ParamClitRubEnable { get; private set; }

	public Live2DParameter ParamClitRubX { get; private set; }

	public Live2DParameter ParamClitRubY { get; private set; }

	public Live2DParameter ParamClitMoveX { get; private set; }

	public Live2DParameter ParamClitMoveY { get; private set; }

	public Live2DParameter ParamVaginaPush { get; private set; }

	public Live2DParameter ParamXRayVagR0 { get; private set; }

	public Live2DParameter ParamXRayVagR1 { get; private set; }

	public Live2DParameter ParamXRayVagR2 { get; private set; }

	public Live2DParameter ParamXRayVagR3 { get; private set; }

	public Live2DParameter ParamXRayVagR4 { get; private set; }

	public Live2DParameter ParamXRayVagR5 { get; private set; }

	public Live2DParameter ParamXRayVagR6 { get; private set; }

	public Live2DParameter ParamXRayVagR7 { get; private set; }

	public Live2DParameter ParamXRayVagL0 { get; private set; }

	public Live2DParameter ParamXRayVagL1 { get; private set; }

	public Live2DParameter ParamXRayVagL2 { get; private set; }

	public Live2DParameter ParamXRayVagL3 { get; private set; }

	public Live2DParameter ParamXRayVagL4 { get; private set; }

	public Live2DParameter ParamXRayVagL5 { get; private set; }

	public Live2DParameter ParamXRayVagL6 { get; private set; }

	public Live2DParameter ParamXRayVagL7 { get; private set; }

	public Live2DParameter ParamVaginaSpread { get; private set; }

	public Live2DParameter ParamPhysicsCumWomb { get; private set; }

	public Live2DParameter ParamMCBodyVisible { get; private set; }

	public Live2DParameter ParamCockAngleX { get; private set; }

	public Live2DParameter ParamCockAngleY { get; private set; }

	public Live2DParameter ParamCumShotAnim { get; private set; }

	public Live2DParameter ParamFlacidity { get; private set; }

	public Live2DParameter ParamCumShotLengthY { get; private set; }

	public Live2DParameter ParamHideCumShot { get; private set; }

	public Live2DParameter ParamBotBodyMovementY { get; private set; }

	public Live2DParameter ParamInsertableInside { get; private set; }

	public Live2DParameter ParamOmegaMeter { get; private set; }

	public Live2DParameter ParamMoveMc { get; private set; }

	public Live2DParameter ParamXray { get; private set; }

	public Live2DParameter ParamOrgasm { get; private set; }

	public Live2DParameter ParamCumInsideWomb { get; private set; }

	public Live2DParameter ParamCumInside { get; private set; }

	public Live2DParameter ParamLegsQuiver { get; private set; }

	public Live2DParameter ParamToSide { get; private set; }

	public Live2DParameter ParamPantsToSide { get; private set; }

	public Live2DParameter ParamFlattenNipples { get; private set; }

	public Live2DParameter ParamFlattenBoobs { get; private set; }

	public Live2DParameter ParamAssSize { get; private set; }

	public Live2DParameter ParamBoobSize { get; private set; }

	public Live2DParameter ParamThighSize { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeShock { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowRot { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowEmote { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballX { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEar { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamPhysicsBoobX { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamPhysicsBoobY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamHoodieString { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamGrabBoobEnable { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBoobSqueeze { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamNippleEnable { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamNippleSqueeze { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamGrabLegEnable { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag0 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag1 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag2 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag3 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag4 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag5 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag6 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag7 { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobLY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobRY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobLX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_HoodieStringL { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobRX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_HoodieStringR { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_HeadShake { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Skirt { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Tail { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Hair { get; private set; }

	public Part HitAreaBoobL { get; private set; }

	public Part HitAreaBoobR { get; private set; }

	public Part HitAreaLegR { get; private set; }

	public Part HitAreaLegL { get; private set; }

	public Part HitAreaBothBoobs { get; private set; }

	public Part HitAreaFingering { get; private set; }

	public Part HitAreaClit { get; private set; }

	public Part HitAreaBothNipples { get; private set; }

	public Part HitAreaNippleL { get; private set; }

	public Part HitAreaNippleR { get; private set; }

	public Part HitAreaHeadpat { get; private set; }

	public Part HitAreaOpenMouth { get; private set; }

	public Part HitAreaCloseMouth { get; private set; }

	public Part HitAreaPanties { get; private set; }

	public Part Part_CumOutsideBellyR { get; private set; }

	public Part Part_CumOutsideInnerThighL2 { get; private set; }

	public Part Part_CumOutsideInnerThighLowerL { get; private set; }

	public Part Part_CumOutsideInnerThighR2 { get; private set; }

	public Part Part_StockingR { get; private set; }

	public Part Part_StockingL { get; private set; }

	public Part Part_PantiesFront { get; private set; }

	public Part Part_PantiesBack { get; private set; }

	public Part Part_CumDick1 { get; private set; }

	public Part Part_CumDick2 { get; private set; }

	public Part Part_DickSkin { get; private set; }

	public Part Part_DickMain { get; private set; }

	public Part Part_MCBody { get; private set; }

	public Part Part_PlainShirtChest { get; private set; }

	public Part Part_PlainShirtFront { get; private set; }

	public Part Part_PlainShirtSleeveR { get; private set; }

	public Part Part_PlainShirtSleeveL { get; private set; }

	public Part Part_PlainShirtBack { get; private set; }

	public Part Part_BraChest { get; private set; }

	public Part Part_BraTop { get; private set; }

	public Part Part_BraBottom { get; private set; }

	public Part Part_SkirtLowerFront { get; private set; }

	public Part Part_SkirtUpper { get; private set; }

	public Part Part_SkirtLowerBack { get; private set; }

	public Part Part_AltBPantsBelt2 { get; private set; }

	public Part Part_AltBPantsBelt { get; private set; }

	public Part Part_BPantsCrotch { get; private set; }

	public Part Part_BPantsThighL { get; private set; }

	public Part Part_BPantsThighR { get; private set; }

	public Part Part_AltLongHoodieStringR { get; private set; }

	public Part Part_AltLongHoodieStringL { get; private set; }

	public Part Part_LongHoodieHood { get; private set; }

	public Part Part_LongHoodieChest { get; private set; }

	public Part Part_LongHoodieFront { get; private set; }

	public Part Part_LongHoodieUppperArmL { get; private set; }

	public Part Part_LongHoodieUppperArmR { get; private set; }

	public Part Part_LongHoodieBack { get; private set; }

	public Part Part_AltBPantsBeltBehind { get; private set; }

	public Part Part_BPantsBehind { get; private set; }

	public Part Part_BellChokerMain { get; private set; }

	public Part Part_PointyEarL { get; private set; }

	public Part Part_PointyEarR { get; private set; }

	public Part Part_BellChokerBell { get; private set; }

	public Part Part_headpet_1 { get; private set; }

	public Part Part_headpet_2 { get; private set; }

	public Part Part_headpet_3 { get; private set; }

	public Part Part_HairHologram { get; private set; }

	public Part Part_CatearFrontR { get; private set; }

	public Part Part_CatearFrontL { get; private set; }

	public Part Part_CatearMidL { get; private set; }

	public Part Part_CatearMidR { get; private set; }

	public Part Part_CatearBackR { get; private set; }

	public Part Part_CatearBackL { get; private set; }

	public Part Part_blush_2 { get; private set; }

	public Part Part_blush { get; private set; }

	public Part Part_lines { get; private set; }

	public Part Part_SkinNose { get; private set; }

	public Part Part_BrowL { get; private set; }

	public Part Part_BrowR { get; private set; }

	public Part Part_SkinLipUpper { get; private set; }

	public Part Part_SkinLipLower { get; private set; }

	public Part Part_SkinLipUpperToungeOut { get; private set; }

	public Part Part_SkinLipLowerToungeOut { get; private set; }

	public Part Part_SharpTeethUpper { get; private set; }

	public Part Part_TeethUpper { get; private set; }

	public Part Part_Saliva2 { get; private set; }

	public Part Part_Saliva1 { get; private set; }

	public Part Part_Tounge { get; private set; }

	public Part Part_SharpTeethLower { get; private set; }

	public Part Part_TeethLower { get; private set; }

	public Part Part_InnerMouthTounge { get; private set; }

	public Part Part_InnerMouth { get; private set; }

	public Part Part_SkinFace { get; private set; }

	public Part Part_highlight { get; private set; }

	public Part Part_Iris2L { get; private set; }

	public Part Part_Iris2R { get; private set; }

	public Part Part_Iris1L { get; private set; }

	public Part Part_Iris1R { get; private set; }

	public Part Part_PupilL { get; private set; }

	public Part Part_PupilR { get; private set; }

	public Part Part_EyeBall { get; private set; }

	public Part Part_SkinEyelidClosedL { get; private set; }

	public Part Part_SkinEyelidClosedR { get; private set; }

	public Part Part_SkinEyelidL { get; private set; }

	public Part Part_SkinEyelidR { get; private set; }

	public Part Part_SkinEyelid2L { get; private set; }

	public Part Part_SkinEyelid2R { get; private set; }

	public Part Part_SkinNeck2 { get; private set; }

	public Part Part_ModdableHairBack { get; private set; }

	public Part Part_ModdableFace { get; private set; }

	public Part Part_NippleL { get; private set; }

	public Part Part_NippleR { get; private set; }

	public Part Part_SkinBoobL { get; private set; }

	public Part Part_SkinBoobR { get; private set; }

	public Part Part_SkinVaginaL { get; private set; }

	public Part Part_SkinVaginaR { get; private set; }

	public Part Part_VaginaClit { get; private set; }

	public Part Part_VaginaInner { get; private set; }

	public Part Part_SkinPalvisTop { get; private set; }

	public Part Part_AttachLegL { get; private set; }

	public Part Part_AttachLegR { get; private set; }

	public Part Part_SkinThighL { get; private set; }

	public Part Part_SkinThighR { get; private set; }

	public Part Part_SkinButtL { get; private set; }

	public Part Part_SkinButtR { get; private set; }

	public Part Part_SkinPelvis { get; private set; }

	public Part Part_OmegaSignInside { get; private set; }

	public Part Part_OmegaSign { get; private set; }

	public Part Part_SkinWaist { get; private set; }

	public Part Part_Barcode { get; private set; }

	public Part Part_SkinRibcage { get; private set; }

	public Part Part_SkinUpperArmL { get; private set; }

	public Part Part_SkinUpperArmR { get; private set; }

	public Part Part_AttachArmL { get; private set; }

	public Part Part_AttachArmR { get; private set; }

	public Part Part_AttachArmLForearm { get; private set; }

	public Part Part_AttachArmRForearm { get; private set; }

	public Part Part_CockMask { get; private set; }

	public Part Part_ModdableBack { get; private set; }

	public Part Part_ModdableShirtLogo { get; private set; }

	public Part Part_ModdableShirtRightSleeveLogo { get; private set; }

	public Part Part_ModdableShirtLeftSleeveLogo { get; private set; }

	public Part Part_ModdableHoodieLogo { get; private set; }

	public Part Part_ModdablePantiesLogo { get; private set; }

	public Part Part_SkinCumflation { get; private set; }

	public Part Part_TailMain10 { get; private set; }

	public Part Part_TailMain9 { get; private set; }

	public Part Part_TailMain8 { get; private set; }

	public Part Part_TailMain7 { get; private set; }

	public Part Part_TailMain6 { get; private set; }

	public Part Part_TailMain5 { get; private set; }

	public Part Part_TailMain4 { get; private set; }

	public Part Part_TailMain3 { get; private set; }

	public Part Part_TailMain2 { get; private set; }

	public Part Part_TailMain { get; private set; }

	public Part Part_CumOutsideInnerThighL { get; private set; }

	public Part Part_CumOutsideInnerThighR { get; private set; }

	public Part Part_Dress2BottomFront { get; private set; }

	public Part Part_Dress2BottomBack { get; private set; }

	public Part Part_Dress2Chest { get; private set; }

	public Part Part_Dress2AltWaist { get; private set; }

	public Part Part_Dress2AltUpper { get; private set; }

	public Part Part_Dress2RArm { get; private set; }

	public Part Part_Dress2LArm { get; private set; }

	public Part Part_CumOutsideFaceRight { get; private set; }

	public Part Part_CumOutsideFaceNose { get; private set; }

	public Part Part_CumOutsideFaceLeft { get; private set; }

	public Part Part_CumOutsideBoobLSide { get; private set; }

	public Part Part_CumWombLv8 { get; private set; }

	public Part Part_CumWombLv7 { get; private set; }

	public Part Part_CumWombLv6 { get; private set; }

	public Part Part_CumWombLv5 { get; private set; }

	public Part Part_XrayBase2 { get; private set; }

	public Part Part_XrayWomb2 { get; private set; }

	public Part Part_Xrayouter2 { get; private set; }

	public Part Part_XrayInnerL2 { get; private set; }

	public Part Part_XrayInnerR3 { get; private set; }

	public Part Part_CumOutsideBoobRSide { get; private set; }

	public Part Part_CumOutsideBoobR { get; private set; }

	public Part Part_CumOutsideBoobL { get; private set; }

	public Part Part_CumOutsideUnderBoobMiddle { get; private set; }

	public Part Part_CumOutsideBelly { get; private set; }

	public Part Part_OmegaMask { get; private set; }

	public Part Part_VagWetMCFinger { get; private set; }

	public Part Part_MCFinger { get; private set; }

	public Part Part_MCRubClit { get; private set; }

	public Part Part_MCGrabThighR { get; private set; }

	public Part Part_MCGrabThighL { get; private set; }

	public Part Part_MCGrabNippleR { get; private set; }

	public Part Part_MCGrabNippleL { get; private set; }

	public Part Part_MCGrabBoobL { get; private set; }

	public Part Part_MCGrabBoobR { get; private set; }

	public Part Part_CumVagina2 { get; private set; }

	public Part Part_Dress1_Chest { get; private set; }

	public Part Part_Dress1_StringR { get; private set; }

	public Part Part_Dress1_LowerFront { get; private set; }

	public Part Part_Dress1_Waist { get; private set; }

	public Part Part_Dress1_Ribcage { get; private set; }

	public Part Part_Dress1_LowerBack { get; private set; }

	public Part Part_Dress1_StringL { get; private set; }

	public Part Part_H0_Top_LeafR { get; private set; }

	public Part Part_H0_Top_LeafL { get; private set; }

	public Part Part_H0_Side { get; private set; }

	public Part Part_H0_Braid { get; private set; }

	public Part Part_H0_Back { get; private set; }

	public Part Part_H1_Top_Ahoge { get; private set; }

	public Part Part_H1_Side_Top { get; private set; }

	public Part Part_H1_Side_L2 { get; private set; }

	public Part Part_H1_Front_Bang { get; private set; }

	public Part Part_H1_Side_L { get; private set; }

	public Part Part_H1_BackL { get; private set; }

	public Part Part_H1_Back_R1 { get; private set; }

	public Part Part_H1_Back_R2 { get; private set; }

	public Part Part_H3_Side { get; private set; }

	public Part Part_H3_Back_Top { get; private set; }

	public Part Part_H3_Back { get; private set; }

	public Part Part_H4_Knot { get; private set; }

	public Part Part_H4_Tail { get; private set; }

	public Part Part_ShadowBoobL { get; private set; }

	public Part Part_ShadowBoobR { get; private set; }

	public Part Part_NipPiercingLeft { get; private set; }

	public Part Part_NipPiercingRight { get; private set; }

	public Part Part_NavelPiercing { get; private set; }

	public Part Part_ModdableHairFront { get; private set; }

	public Part Part_CuteBow { get; private set; }

	public Part Part_Headband { get; private set; }

	public Part Part_WizardHat { get; private set; }

	public Part Part_H0_FrontClippedUp_Bang { get; private set; }

	public Part Part_H0_FrontClippedUp_HairClip { get; private set; }

	public Part Part_H0_Front_Bang { get; private set; }

	public Part Part_H0_Side_BangL { get; private set; }

	public Part Part_H0_Side_BangR { get; private set; }

	public Part Part_H0_SideAlt_Strand { get; private set; }

	public Part Part_H2_Front_Bang { get; private set; }

	public Part Part_H3_Front { get; private set; }

	public Part Part_BikiniBot_A_BowL { get; private set; }

	public Part Part_BikiniBot_A_BowR { get; private set; }

	public Part Part_BikiniBot_M_Cloth { get; private set; }

	public Part Part_BikiniBot_A_Outline { get; private set; }

	public Part Part_BikiniTop_A_OutlineL { get; private set; }

	public Part Part_BikiniTop_M_CupL { get; private set; }

	public Part Part_BikiniTop_A_OutlineR { get; private set; }

	public Part Part_BikiniTop_M_CupR { get; private set; }

	public Part Part_BikiniTop_A_StrapL { get; private set; }

	public Part Part_BikiniTop_A_StrapR { get; private set; }

	public Part Part_BikiniTop_A_Middle { get; private set; }

	protected override bool CanBeRemoved(string id)
	{
		if (id != ItemSlot.SlotManager.LegL.Id && id != ItemSlot.SlotManager.LegR.Id && id != ItemSlot.SlotManager.ArmL.Id)
		{
			return id != ItemSlot.SlotManager.ArmR.Id;
		}
		return false;
	}

	private static void PrepareGrabLegsInteractables(Live2DCowgirlController thisController)
	{
		LimbUsageWithInteractable<Live2DCowgirlController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DCowgirlController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DCowgirlController c) =>
			{
				c.ParamGrabLegLEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DCowgirlController c, Vector2 mouseMove) =>
			{
				c.ParamBotBodyMovementY.Value += mouseMove.y * 5f;
				if (c.CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2) && o2.IsInside())
				{
					o2.ThrustMovementAccumulator = (0f - mouseMove.y) * 2.5f;
				}
			},
			EndLimbAction = (Live2DCowgirlController c, LetGoLimbsCaller caller) =>
			{
				c.ParamGrabLegLEnable.SetMinValue();
			}
		};
		LimbUsageWithInteractable<Live2DCowgirlController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DCowgirlController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DCowgirlController c) =>
			{
				c.ParamGrabLegREnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DCowgirlController c, Vector2 mouseMove) =>
			{
				c.ParamBotBodyMovementY.Value += mouseMove.y * 5f;
				if (c.CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2) && o2.IsInside())
				{
					o2.ThrustMovementAccumulator = (0f - mouseMove.y) * 2.5f;
				}
			},
			EndLimbAction = (Live2DCowgirlController c, LetGoLimbsCaller caller) =>
			{
				c.ParamGrabLegREnable.SetMinValue();
			}
		};
		thisController.LegsGrabInteractable = thisController.AddInteractable(new string[2] { "HitAreaLegL", "HitAreaLegR" }, new Live2DInteractableLimbUsage<Live2DCowgirlController>(thisController, LOC.UI.TupleReferences.Interactable_MoveLegs, limbUsageWithInteractable, limbUsageWithInteractable2), CubismRaycastablePrecision.Triangles);
	}

	public static void PrepareNippleInteractables(Live2DCowgirlController thisController)
	{
		LimbUsageWithInteractable<Live2DCowgirlController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DCowgirlController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.NipplePull);
				c.PhysicsSubRig_BoobLX.StartParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.StartParameterControl(c.ParamPhysicsBoobYL);
				c.ParamNippleLEnable.SetMaxValue();
				c.ParamNippleLSqueeze.Value = 0.5f;
			},
			HoldUpdateAction = (Live2DCowgirlController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsBoobXL.Value += mouseMove.x * 15f;
				c.ParamPhysicsBoobYL.Value += mouseMove.y * 15f;
				float magnitude = new Vector2(c.ParamPhysicsBoobXL.GetRealValue(), c.ParamPhysicsBoobYL.GetRealValue()).magnitude;
				c.ParamNippleLSqueeze.Value = 0.25f + magnitude * 0.75f;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f / 2f);
			},
			HoldFixedUpdateAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.CausePleasure(0.00015f);
			},
			EndLimbAction = (Live2DCowgirlController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.NipplePull);
				c.PhysicsSubRig_BoobLX.EndParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.EndParameterControl(c.ParamPhysicsBoobYL);
				c.ParamNippleLEnable.SetMinValue();
			}
		};
		LimbUsageWithInteractable<Live2DCowgirlController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DCowgirlController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.NipplePull);
				c.PhysicsSubRig_BoobRX.StartParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.StartParameterControl(c.ParamPhysicsBoobYR);
				c.ParamNippleREnable.SetMaxValue();
				c.ParamNippleRSqueeze.Value = 0.25f;
			},
			HoldUpdateAction = (Live2DCowgirlController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsBoobXR.Value += mouseMove.x * 15f;
				c.ParamPhysicsBoobYR.Value += mouseMove.y * 15f;
				float magnitude = new Vector2(c.ParamPhysicsBoobXR.GetRealValue(), c.ParamPhysicsBoobYR.GetRealValue()).magnitude;
				c.ParamNippleRSqueeze.Value = 0.25f + magnitude * 0.75f;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f / 2f);
			},
			HoldFixedUpdateAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.CausePleasure(0.00015f);
			},
			EndLimbAction = (Live2DCowgirlController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.NipplePull);
				c.PhysicsSubRig_BoobRX.EndParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.EndParameterControl(c.ParamPhysicsBoobYR);
				c.ParamNippleREnable.SetMinValue();
			}
		};
		thisController.NippleLInteractable = thisController.AddInteractableHitArea(thisController.HitAreaNippleL, new Live2DInteractableLimbUsage<Live2DCowgirlController>(thisController, LOC.UI.TupleReferences.Interactable_PullLeftNipple, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DCowgirlController c) => c.IsBoobaVisible
		});
		thisController.NippleRInteractable = thisController.AddInteractableHitArea(thisController.HitAreaNippleR, new Live2DInteractableLimbUsage<Live2DCowgirlController>(thisController, LOC.UI.TupleReferences.Interactable_PullRightNipple, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DCowgirlController c) => c.IsBoobaVisible
		});
		thisController.NippleBothInteractable = thisController.AddInteractableHitArea(thisController.HitAreaBothNipples, new Live2DInteractableLimbUsage<Live2DCowgirlController>(thisController, LOC.UI.TupleReferences.Interactable_PullBothNipples, limbUsageWithInteractable, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DCowgirlController c) => c.IsBoobaVisible
		});
	}

	public static void PrepareBoobInteractables(Live2DCowgirlController thisController)
	{
		LimbUsageWithInteractable<Live2DCowgirlController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DCowgirlController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobLX.StartParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.StartParameterControl(c.ParamPhysicsBoobYL);
				c.ParamGrabBoobLEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DCowgirlController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsBoobXL.Value += mouseMove.x * 7f;
				c.ParamPhysicsBoobYL.Value += mouseMove.y * 7f;
				float magnitude = new Vector2(c.ParamPhysicsBoobXL.GetRealValue(), c.ParamPhysicsBoobYL.GetRealValue()).magnitude;
				c.ParamBoobSqueezeL.Value = magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f / 2f);
			},
			HoldFixedUpdateAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.CausePleasure(0.00015f);
			},
			EndLimbAction = (Live2DCowgirlController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobLX.EndParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.EndParameterControl(c.ParamPhysicsBoobYL);
				c.ParamGrabBoobLEnable.SetMinValue();
			}
		};
		LimbUsageWithInteractable<Live2DCowgirlController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DCowgirlController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobRX.StartParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.StartParameterControl(c.ParamPhysicsBoobYR);
				c.ParamGrabBoobREnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DCowgirlController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsBoobXR.Value += mouseMove.x * 7f;
				c.ParamPhysicsBoobYR.Value += mouseMove.y * 7f;
				float magnitude = new Vector2(c.ParamPhysicsBoobXR.GetRealValue(), c.ParamPhysicsBoobYR.GetRealValue()).magnitude;
				c.ParamBoobSqueezeR.Value = magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f / 2f);
			},
			HoldFixedUpdateAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.CausePleasure(0.00015f);
			},
			EndLimbAction = (Live2DCowgirlController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobRX.EndParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.EndParameterControl(c.ParamPhysicsBoobYR);
				c.ParamGrabBoobREnable.SetMinValue();
			}
		};
		thisController.BoobLFondleInteractable = thisController.AddInteractableHitArea(thisController.HitAreaBoobL, new Live2DInteractableLimbUsage<Live2DCowgirlController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobL, limbUsageWithInteractable));
		thisController.BoobRFondleInteractable = thisController.AddInteractableHitArea(thisController.HitAreaBoobR, new Live2DInteractableLimbUsage<Live2DCowgirlController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobR, limbUsageWithInteractable2));
		thisController.BoobBothFondleInteractable = thisController.AddInteractableHitArea(thisController.HitAreaBothBoobs, new Live2DInteractableLimbUsage<Live2DCowgirlController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobs, limbUsageWithInteractable, limbUsageWithInteractable2));
	}

	private static void PrepareVaginaInteractables(Live2DCowgirlController thisController)
	{
		LimbUsageWithInteractable<Live2DCowgirlController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DCowgirlController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DCowgirlController c) =>
			{
				c.ParamFingeringEnable.SetMaxValue();
				c.ParamFingeringHandWet.SetMinValue();
				c.ParamFingeringThrust.ResetToDefault();
				c.ParamFingeringThrust.FinishMove();
				c.ParamInsertableInside.SetMaxValue();
				c.VaginaInsertionHandler.SetCurrentAxis(c.VaginaFingeringFuckAxis, GameVariables.Current.GetGameStats().OtherFrickGameStats.AnonFingeringInfo.Value);
			},
			HoldUpdateAction = (Live2DCowgirlController c, Vector2 mouseMove) =>
			{
				c.VaginaInsertionHandler.ThrustMovementAccumulator += mouseMove.y * 10f;
				c.VaginaInsertionHandler.SidewaysMovementAccumulator += mouseMove.x * 5f;
				if (c.ParamFingeringHandWet.Value < c.ParamFingeringThrust.Value * 0.8f)
				{
					c.ParamFingeringHandWet.Value = c.ParamFingeringThrust.Value * 0.8f;
				}
			},
			EndLimbAction = (Live2DCowgirlController c, LetGoLimbsCaller caller) =>
			{
				c.ParamFingeringEnable.SetMinValue();
				c.ParamInsertableInside.SetMinValue();
				c.VaginaInsertionHandler.Reset();
			}
		};
		thisController.FingeringInteractable = thisController.AddInteractableHitArea(thisController.HitAreaFingering, new Live2DInteractableLimbUsage<Live2DCowgirlController>(thisController, LOC.UI.TupleReferences.Interactable_Fingering, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DCowgirlController c) => !c.McBodyVisible && c.IsPantiesAsideOrOff
		});
		LimbUsageWithInteractable<Live2DCowgirlController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DCowgirlController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.ClitRub);
				c.ParamClitRubEnable.SetMaxValue();
				c.ParamClitRubX.ResetToDefault();
				c.ParamClitRubY.ResetToDefault();
			},
			HoldUpdateAction = (Live2DCowgirlController c, Vector2 mouseMove) =>
			{
				c.ParamClitRubX.Value += mouseMove.x * 7f;
				c.ParamClitRubY.Value += mouseMove.y * 7f;
				c.ParamClitMoveX.Value = c.ParamClitRubX.Value;
				c.ParamClitMoveY.Value = c.ParamClitRubY.Value;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.025f);
			},
			HoldFixedUpdateAction = (Live2DCowgirlController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0003f);
			},
			EndLimbAction = (Live2DCowgirlController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.ClitRub);
				c.ParamClitRubEnable.SetMinValue();
				c.ParamClitRubX.ResetToDefault();
				c.ParamClitRubY.ResetToDefault();
				c.ParamClitMoveX.ResetToDefault();
				c.ParamClitMoveY.ResetToDefault();
			}
		};
		thisController.ClitRubInteractable = thisController.AddInteractableHitArea(thisController.HitAreaClit, new Live2DInteractableLimbUsage<Live2DCowgirlController>(thisController, LOC.UI.TupleReferences.Interactable_ClitRub, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DCowgirlController c) => c.VaginaCockInsertionFuckAxis.IsInsideCowgirl || !c.McBodyVisible
		});
	}

	private void UpdateBoob()
	{
		ParamFlattenNipples.Value = ((IsBraAsideOrOff && IsShirtAndHoodieAsideOrOff && !IsDress1On) ? 0f : 1f);
	}

	protected override void AfterUpdateTextures()
	{
		base.AfterUpdateTextures();
		UpdateBoob();
	}

	public override Part GetApproximateEyesPositionPart()
	{
		return Part_EyeBall;
	}

	public override Part GetApproximateMouthPositionPart()
	{
		return Part_InnerMouth;
	}

	protected override MouthManagerDefaultMouth InitializeMouthImpl()
	{
		return MouthManagerDefaultMouth.Create(this);
	}

	protected override EyeManagerCommon InitializeEyesImpl()
	{
		return EyeManagerCommon.Create(this);
	}

	public override Vector2 GetCockDirection()
	{
		return Vector2.up;
	}

	protected override void InitializeManagersInherited()
	{
		XRayManager = RegisterManager(XRayManagerCommon.CreateInstance(this));
		FrickStaticGuiManager = RegisterManager(new FrickStaticGuiManager(this));
		List<InsertionHandler> list = new List<InsertionHandler>();
		InsertionHandler insertionHandler = new InsertionHandler(this, InsertionHandlerOrificeTypeEnum.Vagina);
		VaginaCockInsertionFuckAxis = new CowgirlCockInsertionFuckAxis(this, ParamBotBodyMovementY, ParamInsertableInside, ParamMoveMc, ParamCockAngleY, ParamCockAngleX);
		insertionHandler.AddFuckAxis(VaginaCockInsertionFuckAxis);
		VaginaFingeringFuckAxis = new BasicInsertionHandleFuckAxis(-0.2f, 0.2f, ParamFingeringThrust)
		{
			CockAngleXParameter = ParamFingeringAngleX,
			ThrustParameterMapping = new ParameterMappingMinMax(0f, 1f, 0f, 0.5f)
		};
		insertionHandler.AddFuckAxis(VaginaFingeringFuckAxis);
		insertionHandler.AddVisualizationPlane(new InsertionPlaneVisualizer(0f, 8, GetParamArrayToDirectlyControl("ParamXRayVagL", 8), GetParamArrayToDirectlyControl("ParamXRayVagR", 8))
		{
			XRayStartOffset = -0.05f
		});
		insertionHandler.EntranceSpreadParameter = ParamVaginaSpread;
		insertionHandler.InsertionYDeformationParameter = ParamVaginaPush;
		list.Add(insertionHandler);
		VaginaInsertionHandler = insertionHandler;
		InsertionManager = RegisterManager(new InsertionManager(list));
		_cockFuckLimbUsage = new CockLimbUsage<Live2DCowgirlController>(this, ParamMCBodyVisible, new CockLimbUsage<Live2DCowgirlController>.CockLimbUsageTarget(VaginaInsertionHandler, VaginaCockInsertionFuckAxis));
		CockManager = RegisterManager(new CockManagerCommon<Live2DCowgirlController>(this, _cockFuckLimbUsage)
		{
			AlwaysEnableCockAtStart = true,
			AutoPulloutImplemented = false
		});
	}

	protected override void InitializeInherited()
	{
		AddInteractable("MCBody", new Live2DInteractable
		{
			OnHoldStart = (CubismDrawable d) =>
			{
				if (IsLimbUsageExactlyCorrect(_cockFuckLimbUsage))
				{
					_cockFuckLimbUsage.DisableAuto();
				}
				else
				{
					CockManager.WhipOutCock();
				}
			},
			OnHeld = (Vector2 mouseMove) =>
			{
				if (CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2))
				{
					o2.ThrustMovementAccumulator += mouseMove.y * 10f;
					o2.SidewaysMovementAccumulator -= mouseMove.x * 40f;
				}
			},
			OnHeldFixed = null,
			OnHoldEnd = null,
			InfoSls = LOC.UI.TupleReferences.Interactable_ThrustHips,
			ActiveFunc = () => !CockManager.IsFlacid
		});
		Live2DController.PrepareCommonHeadpatInteractables(this, HitAreaHeadpat, out var headpatInteractable);
		Live2DController.PrepareCommonMouthKissInteractables(this, out headpatInteractable, out var _);
		PrepareGrabLegsInteractables(this);
		PrepareBoobInteractables(this);
		PrepareNippleInteractables(this);
		PrepareVaginaInteractables(this);
		AddInteractableHitArea(HitAreaPanties, new Live2DInteractable
		{
			OnHoldStart = (CubismDrawable d) =>
			{
			},
			OnHeld = (Vector2 mouseMove) =>
			{
				ParamToSide.Value -= mouseMove.x * 30f;
				if (CockManager.IsCockLimbInside() && ParamToSide.Value < 0.9f)
				{
					ParamToSide.Value = 0.9f;
				}
			},
			OnHeldFixed = () =>
			{
			},
			OnHoldEnd = () =>
			{
				if (CockManager.IsCockLimbInside())
				{
					ParamToSide.Value = ParamToSide.MaximumValue;
				}
				else if (!IsPantiesAside)
				{
					ParamToSide.ResetToDefault();
				}
				else
				{
					ParamToSide.Value = ParamToSide.MaximumValue;
				}
			},
			InfoSls = LOC.UI.TupleReferences.Interactable_MovePanties,
			ActiveFunc = () => IsPantsAsideOrOff && IsPantiesOn
		});
	}

	public override void InitializeGenerated()
	{
		Param_Angle_Rotation_1_TailMain = GetParamToDirectlyControl("Param_Angle_Rotation_1_TailMain");
		Param_Angle_Rotation_2_TailMain = GetParamToDirectlyControl("Param_Angle_Rotation_2_TailMain");
		Param_Angle_Rotation_3_TailMain = GetParamToDirectlyControl("Param_Angle_Rotation_3_TailMain");
		Param_Angle_Rotation_4_TailMain = GetParamToDirectlyControl("Param_Angle_Rotation_4_TailMain");
		Param_Angle_Rotation_5_TailMain = GetParamToDirectlyControl("Param_Angle_Rotation_5_TailMain");
		Param_Angle_Rotation_6_TailMain = GetParamToDirectlyControl("Param_Angle_Rotation_6_TailMain");
		Param_Angle_Rotation_7_TailMain = GetParamToDirectlyControl("Param_Angle_Rotation_7_TailMain");
		Param_Angle_Rotation_8_TailMain = GetParamToDirectlyControl("Param_Angle_Rotation_8_TailMain");
		Param_Angle_Rotation_9_TailMain = GetParamToDirectlyControl("Param_Angle_Rotation_9_TailMain");
		ParamTailWiggle = GetParamToDirectlyControl("ParamTailWiggle");
		ParamBrowRY = GetParamToDirectlyControl("ParamBrowRY");
		ParamBrowLY = GetParamToDirectlyControl("ParamBrowLY");
		ParamEyeRShock = GetParamToDirectlyControl("ParamEyeRShock");
		ParamEyeRShock.SetWeight(0.1f);
		ParamEyeLShock = GetParamToDirectlyControl("ParamEyeLShock");
		ParamEyeLShock.SetWeight(0.1f);
		ParamBrowLRot = GetParamToDirectlyControl("ParamBrowLRot");
		ParamBrowRRot = GetParamToDirectlyControl("ParamBrowRRot");
		ParamBrowLEmote = GetParamToDirectlyControl("ParamBrowLEmote");
		ParamBrowREmote = GetParamToDirectlyControl("ParamBrowREmote");
		ParamEyeOpen = GetParamToDirectlyControl("ParamEyeOpen");
		ParamEyesHappy = GetParamToDirectlyControl("ParamEyesHappy");
		ParamIrisZoom = GetParamToDirectlyControl("ParamIrisZoom");
		ParamHeart = GetParamToDirectlyControl("ParamHeart");
		ParamEyeballRX = GetParamToDirectlyControl("ParamEyeballRX");
		ParamEyeballRY = GetParamToDirectlyControl("ParamEyeballRY");
		ParamEyeballLX = GetParamToDirectlyControl("ParamEyeballLX");
		ParamEyeballLY = GetParamToDirectlyControl("ParamEyeballLY");
		ParamMouthOpen = GetParamToDirectlyControl("ParamMouthOpen");
		ParamSaliva = GetParamToDirectlyControl("ParamSaliva");
		ParamMouthForm = GetParamToDirectlyControl("ParamMouthForm");
		ParamToungeX = GetParamToDirectlyControl("ParamToungeX");
		ParamToungeY = GetParamToDirectlyControl("ParamToungeY");
		ParamBlush = GetParamToDirectlyControl("ParamBlush");
		ParamHeadZ = GetParamToDirectlyControl("ParamHeadZ");
		ParamHeadZ.SetWeight(10f);
		ParamHeadX = GetParamToDirectlyControl("ParamHeadX");
		ParamHeadX.SetWeight(10f);
		ParamHeadY = GetParamToDirectlyControl("ParamHeadY");
		ParamHeadY.SetWeight(10f);
		ParamEarL = GetParamToDirectlyControl("ParamEarL");
		ParamEarR = GetParamToDirectlyControl("ParamEarR");
		ParamChestX = GetParamToDirectlyControl("ParamChestX");
		ParamSalivaPhysics = GetParamToDirectlyControl("ParamSalivaPhysics");
		ParamHairX = GetParamToDirectlyControl("ParamHairX");
		ParamPupilWiggle = GetParamToDirectlyControl("ParamPupilWiggle");
		ParamEarsWiggle = GetParamToDirectlyControl("ParamEarsWiggle");
		ParamHeadpatX = GetParamToDirectlyControl("ParamHeadpatX");
		ParamHeadpatX.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		ParamHeadpatY = GetParamToDirectlyControl("ParamHeadpatY");
		ParamHeadpatY.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		ParamHeadpat = GetParamToDirectlyControl("ParamHeadpat");
		ParamHairPhysicsBaseToShort = GetParamToDirectlyControl("ParamHairPhysicsBaseToShort");
		ParamHairPhysicsShortToMid = GetParamToDirectlyControl("ParamHairPhysicsShortToMid");
		ParamHairPhysicsMidToLong = GetParamToDirectlyControl("ParamHairPhysicsMidToLong");
		ParamPhysicsBoobXL = GetParamToDirectlyControl("ParamPhysicsBoobXL");
		ParamPhysicsBoobYL = GetParamToDirectlyControl("ParamPhysicsBoobYL");
		ParamPhysicsBoobXR = GetParamToDirectlyControl("ParamPhysicsBoobXR");
		ParamPhysicsBoobYR = GetParamToDirectlyControl("ParamPhysicsBoobYR");
		ParamHoodieStringL = GetParamToDirectlyControl("ParamHoodieStringL");
		ParamHoodieStringR = GetParamToDirectlyControl("ParamHoodieStringR");
		ParamSkirtYPhysics = GetParamToDirectlyControl("ParamSkirtYPhysics");
		ParamGrabBoobLEnable = GetParamToDirectlyControl("ParamGrabBoobLEnable");
		ParamBoobSqueezeL = GetParamToDirectlyControl("ParamBoobSqueezeL");
		ParamGrabBoobREnable = GetParamToDirectlyControl("ParamGrabBoobREnable");
		ParamBoobSqueezeR = GetParamToDirectlyControl("ParamBoobSqueezeR");
		ParamNippleREnable = GetParamToDirectlyControl("ParamNippleREnable");
		ParamNippleRSqueeze = GetParamToDirectlyControl("ParamNippleRSqueeze");
		ParamNippleLEnable = GetParamToDirectlyControl("ParamNippleLEnable");
		ParamNippleLSqueeze = GetParamToDirectlyControl("ParamNippleLSqueeze");
		ParamGrabLegREnable = GetParamToDirectlyControl("ParamGrabLegREnable");
		ParamGrabLegLEnable = GetParamToDirectlyControl("ParamGrabLegLEnable");
		ParamFingeringEnable = GetParamToDirectlyControl("ParamFingeringEnable");
		ParamFingeringThrust = GetParamToDirectlyControl("ParamFingeringThrust");
		ParamFingeringHandWet = GetParamToDirectlyControl("ParamFingeringHandWet");
		ParamFingeringAngleX = GetParamToDirectlyControl("ParamFingeringAngleX");
		ParamClitRubEnable = GetParamToDirectlyControl("ParamClitRubEnable");
		ParamClitRubX = GetParamToDirectlyControl("ParamClitRubX");
		ParamClitRubY = GetParamToDirectlyControl("ParamClitRubY");
		ParamClitMoveX = GetParamToDirectlyControl("ParamClitMoveX");
		ParamClitMoveY = GetParamToDirectlyControl("ParamClitMoveY");
		ParamVaginaPush = GetParamToDirectlyControl("ParamVaginaPush");
		ParamXRayVagR0 = GetParamToDirectlyControl("ParamXRayVagR0");
		ParamXRayVagR1 = GetParamToDirectlyControl("ParamXRayVagR1");
		ParamXRayVagR2 = GetParamToDirectlyControl("ParamXRayVagR2");
		ParamXRayVagR3 = GetParamToDirectlyControl("ParamXRayVagR3");
		ParamXRayVagR4 = GetParamToDirectlyControl("ParamXRayVagR4");
		ParamXRayVagR5 = GetParamToDirectlyControl("ParamXRayVagR5");
		ParamXRayVagR6 = GetParamToDirectlyControl("ParamXRayVagR6");
		ParamXRayVagR7 = GetParamToDirectlyControl("ParamXRayVagR7");
		ParamXRayVagL0 = GetParamToDirectlyControl("ParamXRayVagL0");
		ParamXRayVagL1 = GetParamToDirectlyControl("ParamXRayVagL1");
		ParamXRayVagL2 = GetParamToDirectlyControl("ParamXRayVagL2");
		ParamXRayVagL3 = GetParamToDirectlyControl("ParamXRayVagL3");
		ParamXRayVagL4 = GetParamToDirectlyControl("ParamXRayVagL4");
		ParamXRayVagL5 = GetParamToDirectlyControl("ParamXRayVagL5");
		ParamXRayVagL6 = GetParamToDirectlyControl("ParamXRayVagL6");
		ParamXRayVagL7 = GetParamToDirectlyControl("ParamXRayVagL7");
		ParamVaginaSpread = GetParamToDirectlyControl("ParamVaginaSpread");
		ParamPhysicsCumWomb = GetParamToDirectlyControl("ParamPhysicsCumWomb");
		ParamMCBodyVisible = GetParamToDirectlyControl("ParamMCBodyVisible");
		ParamCockAngleX = GetParamToDirectlyControl("ParamCockAngleX");
		ParamCockAngleY = GetParamToDirectlyControl("ParamCockAngleY");
		ParamCumShotAnim = GetParamToDirectlyControl("ParamCumShotAnim");
		ParamFlacidity = GetParamToDirectlyControl("ParamFlacidity");
		ParamCumShotLengthY = GetParamToDirectlyControl("ParamCumShotLengthY");
		ParamHideCumShot = GetParamToDirectlyControl("ParamHideCumShot");
		ParamBotBodyMovementY = GetParamToDirectlyControl("ParamBotBodyMovementY");
		ParamInsertableInside = GetParamToDirectlyControl("ParamInsertableInside");
		ParamOmegaMeter = GetParamToDirectlyControl("ParamOmegaMeter");
		ParamMoveMc = GetParamToDirectlyControl("ParamMoveMc");
		ParamXray = GetParamToDirectlyControl("ParamXray");
		ParamOrgasm = GetParamToDirectlyControl("ParamOrgasm");
		ParamCumInsideWomb = GetParamToDirectlyControl("ParamCumInsideWomb");
		ParamCumInside = GetParamToDirectlyControl("ParamCumInside");
		ParamLegsQuiver = GetParamToDirectlyControl("ParamLegsQuiver");
		ParamToSide = GetParamToDirectlyControl("ParamToSide");
		ParamPantsToSide = GetParamToDirectlyControl("ParamPantsToSide");
		ParamFlattenNipples = GetParamToDirectlyControl("ParamFlattenNipples");
		ParamFlattenBoobs = GetParamToDirectlyControl("ParamFlattenBoobs");
		ParamAssSize = GetParamToDirectlyControl("ParamAssSize");
		ParamBoobSize = GetParamToDirectlyControl("ParamBoobSize");
		ParamThighSize = GetParamToDirectlyControl("ParamThighSize");
		SymParam_LR_ParamBrowY = new Live2DParameter[2] { ParamBrowLY, ParamBrowRY };
		SymParam_LR_ParamEyeShock = new Live2DParameter[2] { ParamEyeLShock, ParamEyeRShock };
		SymParam_LR_ParamBrowRot = new Live2DParameter[2] { ParamBrowLRot, ParamBrowRRot };
		SymParam_LR_ParamBrowEmote = new Live2DParameter[2] { ParamBrowLEmote, ParamBrowREmote };
		SymParam_LR_ParamEyeballX = new Live2DParameter[2] { ParamEyeballLX, ParamEyeballRX };
		SymParam_LR_ParamEyeballY = new Live2DParameter[2] { ParamEyeballLY, ParamEyeballRY };
		SymParam_LR_ParamEar = new Live2DParameter[2] { ParamEarL, ParamEarR };
		SymParam_LR_ParamPhysicsBoobX = new Live2DParameter[2] { ParamPhysicsBoobXL, ParamPhysicsBoobXR };
		SymParam_LR_ParamPhysicsBoobY = new Live2DParameter[2] { ParamPhysicsBoobYL, ParamPhysicsBoobYR };
		SymParam_LR_ParamHoodieString = new Live2DParameter[2] { ParamHoodieStringL, ParamHoodieStringR };
		SymParam_LR_ParamGrabBoobEnable = new Live2DParameter[2] { ParamGrabBoobLEnable, ParamGrabBoobREnable };
		SymParam_LR_ParamBoobSqueeze = new Live2DParameter[2] { ParamBoobSqueezeL, ParamBoobSqueezeR };
		SymParam_LR_ParamNippleEnable = new Live2DParameter[2] { ParamNippleLEnable, ParamNippleREnable };
		SymParam_LR_ParamNippleSqueeze = new Live2DParameter[2] { ParamNippleLSqueeze, ParamNippleRSqueeze };
		SymParam_LR_ParamGrabLegEnable = new Live2DParameter[2] { ParamGrabLegLEnable, ParamGrabLegREnable };
		SymParam_LR_ParamXRayVag0 = new Live2DParameter[2] { ParamXRayVagL0, ParamXRayVagR0 };
		SymParam_LR_ParamXRayVag1 = new Live2DParameter[2] { ParamXRayVagL1, ParamXRayVagR1 };
		SymParam_LR_ParamXRayVag2 = new Live2DParameter[2] { ParamXRayVagL2, ParamXRayVagR2 };
		SymParam_LR_ParamXRayVag3 = new Live2DParameter[2] { ParamXRayVagL3, ParamXRayVagR3 };
		SymParam_LR_ParamXRayVag4 = new Live2DParameter[2] { ParamXRayVagL4, ParamXRayVagR4 };
		SymParam_LR_ParamXRayVag5 = new Live2DParameter[2] { ParamXRayVagL5, ParamXRayVagR5 };
		SymParam_LR_ParamXRayVag6 = new Live2DParameter[2] { ParamXRayVagL6, ParamXRayVagR6 };
		SymParam_LR_ParamXRayVag7 = new Live2DParameter[2] { ParamXRayVagL7, ParamXRayVagR7 };
		CubismPhysicsController component = Model.GetComponent<CubismPhysicsController>();
		PhysicsSubRig_BoobLY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobLY"));
		PhysicsSubRig_BoobRY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobRY"));
		PhysicsSubRig_BoobLX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobLX"));
		PhysicsSubRig_HoodieStringL = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "HoodieStringL"));
		PhysicsSubRig_BoobRX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobRX"));
		PhysicsSubRig_HoodieStringR = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "HoodieStringR"));
		PhysicsSubRig_HeadShake = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "HeadShake"));
		PhysicsSubRig_Skirt = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Skirt"));
		PhysicsSubRig_Tail = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Tail"));
		PhysicsSubRig_Hair = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Hair"));
		HitAreaBoobL = GetCachedPart("HitAreaBoobL");
		HitAreaBoobR = GetCachedPart("HitAreaBoobR");
		HitAreaLegR = GetCachedPart("HitAreaLegR");
		HitAreaLegL = GetCachedPart("HitAreaLegL");
		HitAreaBothBoobs = GetCachedPart("HitAreaBothBoobs");
		HitAreaFingering = GetCachedPart("HitAreaFingering");
		HitAreaClit = GetCachedPart("HitAreaClit");
		HitAreaBothNipples = GetCachedPart("HitAreaBothNipples");
		HitAreaNippleL = GetCachedPart("HitAreaNippleL");
		HitAreaNippleR = GetCachedPart("HitAreaNippleR");
		HitAreaHeadpat = GetCachedPart("HitAreaHeadpat");
		HitAreaOpenMouth = GetCachedPart("HitAreaOpenMouth");
		HitAreaCloseMouth = GetCachedPart("HitAreaCloseMouth");
		HitAreaPanties = GetCachedPart("HitAreaPanties");
		Part_CumOutsideBellyR = GetCachedPart("CumOutsideBellyR");
		Part_CumOutsideInnerThighL2 = GetCachedPart("CumOutsideInnerThighL2");
		Part_CumOutsideInnerThighLowerL = GetCachedPart("CumOutsideInnerThighLowerL");
		Part_CumOutsideInnerThighR2 = GetCachedPart("CumOutsideInnerThighR2");
		Part_StockingR = GetCachedPart("StockingR");
		Part_StockingL = GetCachedPart("StockingL");
		Part_PantiesFront = GetCachedPart("PantiesFront");
		Part_PantiesBack = GetCachedPart("PantiesBack");
		Part_CumDick1 = GetCachedPart("CumDick1");
		Part_CumDick2 = GetCachedPart("CumDick2");
		Part_DickSkin = GetCachedPart("DickSkin");
		Part_DickMain = GetCachedPart("DickMain");
		Part_MCBody = GetCachedPart("MCBody");
		Part_PlainShirtChest = GetCachedPart("PlainShirtChest");
		Part_PlainShirtFront = GetCachedPart("PlainShirtFront");
		Part_PlainShirtSleeveR = GetCachedPart("PlainShirtSleeveR");
		Part_PlainShirtSleeveL = GetCachedPart("PlainShirtSleeveL");
		Part_PlainShirtBack = GetCachedPart("PlainShirtBack");
		Part_BraChest = GetCachedPart("BraChest");
		Part_BraTop = GetCachedPart("BraTop");
		Part_BraBottom = GetCachedPart("BraBottom");
		Part_SkirtLowerFront = GetCachedPart("SkirtLowerFront");
		Part_SkirtUpper = GetCachedPart("SkirtUpper");
		Part_SkirtLowerBack = GetCachedPart("SkirtLowerBack");
		Part_AltBPantsBelt2 = GetCachedPart("AltBPantsBelt2");
		Part_AltBPantsBelt = GetCachedPart("AltBPantsBelt");
		Part_BPantsCrotch = GetCachedPart("BPantsCrotch");
		Part_BPantsThighL = GetCachedPart("BPantsThighL");
		Part_BPantsThighR = GetCachedPart("BPantsThighR");
		Part_AltLongHoodieStringR = GetCachedPart("AltLongHoodieStringR");
		Part_AltLongHoodieStringL = GetCachedPart("AltLongHoodieStringL");
		Part_LongHoodieHood = GetCachedPart("LongHoodieHood");
		Part_LongHoodieChest = GetCachedPart("LongHoodieChest");
		Part_LongHoodieFront = GetCachedPart("LongHoodieFront");
		Part_LongHoodieUppperArmL = GetCachedPart("LongHoodieUppperArmL");
		Part_LongHoodieUppperArmR = GetCachedPart("LongHoodieUppperArmR");
		Part_LongHoodieBack = GetCachedPart("LongHoodieBack");
		Part_AltBPantsBeltBehind = GetCachedPart("AltBPantsBeltBehind");
		Part_BPantsBehind = GetCachedPart("BPantsBehind");
		Part_BellChokerMain = GetCachedPart("BellChokerMain");
		Part_PointyEarL = GetCachedPart("PointyEarL");
		Part_PointyEarR = GetCachedPart("PointyEarR");
		Part_BellChokerBell = GetCachedPart("BellChokerBell");
		Part_headpet_1 = GetCachedPart("headpet_1");
		Part_headpet_2 = GetCachedPart("headpet_2");
		Part_headpet_3 = GetCachedPart("headpet_3");
		Part_HairHologram = GetCachedPart("HairHologram");
		Part_CatearFrontR = GetCachedPart("CatearFrontR");
		Part_CatearFrontL = GetCachedPart("CatearFrontL");
		Part_CatearMidL = GetCachedPart("CatearMidL");
		Part_CatearMidR = GetCachedPart("CatearMidR");
		Part_CatearBackR = GetCachedPart("CatearBackR");
		Part_CatearBackL = GetCachedPart("CatearBackL");
		Part_blush_2 = GetCachedPart("blush_2");
		Part_blush = GetCachedPart("blush");
		Part_lines = GetCachedPart("lines");
		Part_SkinNose = GetCachedPart("SkinNose");
		Part_BrowL = GetCachedPart("BrowL");
		Part_BrowR = GetCachedPart("BrowR");
		Part_SkinLipUpper = GetCachedPart("SkinLipUpper");
		Part_SkinLipLower = GetCachedPart("SkinLipLower");
		Part_SkinLipUpperToungeOut = GetCachedPart("SkinLipUpperToungeOut");
		Part_SkinLipLowerToungeOut = GetCachedPart("SkinLipLowerToungeOut");
		Part_SharpTeethUpper = GetCachedPart("SharpTeethUpper");
		Part_TeethUpper = GetCachedPart("TeethUpper");
		Part_Saliva2 = GetCachedPart("Saliva2");
		Part_Saliva1 = GetCachedPart("Saliva1");
		Part_Tounge = GetCachedPart("Tounge");
		Part_SharpTeethLower = GetCachedPart("SharpTeethLower");
		Part_TeethLower = GetCachedPart("TeethLower");
		Part_InnerMouthTounge = GetCachedPart("InnerMouthTounge");
		Part_InnerMouth = GetCachedPart("InnerMouth");
		Part_SkinFace = GetCachedPart("SkinFace");
		Part_highlight = GetCachedPart("highlight");
		Part_Iris2L = GetCachedPart("Iris2L");
		Part_Iris2R = GetCachedPart("Iris2R");
		Part_Iris1L = GetCachedPart("Iris1L");
		Part_Iris1R = GetCachedPart("Iris1R");
		Part_PupilL = GetCachedPart("PupilL");
		Part_PupilR = GetCachedPart("PupilR");
		Part_EyeBall = GetCachedPart("EyeBall");
		Part_SkinEyelidClosedL = GetCachedPart("SkinEyelidClosedL");
		Part_SkinEyelidClosedR = GetCachedPart("SkinEyelidClosedR");
		Part_SkinEyelidL = GetCachedPart("SkinEyelidL");
		Part_SkinEyelidR = GetCachedPart("SkinEyelidR");
		Part_SkinEyelid2L = GetCachedPart("SkinEyelid2L");
		Part_SkinEyelid2R = GetCachedPart("SkinEyelid2R");
		Part_SkinNeck2 = GetCachedPart("SkinNeck2");
		Part_ModdableHairBack = GetCachedPart("ModdableHairBack");
		Part_ModdableFace = GetCachedPart("ModdableFace");
		Part_NippleL = GetCachedPart("NippleL");
		Part_NippleR = GetCachedPart("NippleR");
		Part_SkinBoobL = GetCachedPart("SkinBoobL");
		Part_SkinBoobR = GetCachedPart("SkinBoobR");
		Part_SkinVaginaL = GetCachedPart("SkinVaginaL");
		Part_SkinVaginaR = GetCachedPart("SkinVaginaR");
		Part_VaginaClit = GetCachedPart("VaginaClit");
		Part_VaginaInner = GetCachedPart("VaginaInner");
		Part_SkinPalvisTop = GetCachedPart("SkinPalvisTop");
		Part_AttachLegL = GetCachedPart("AttachLegL");
		Part_AttachLegR = GetCachedPart("AttachLegR");
		Part_SkinThighL = GetCachedPart("SkinThighL");
		Part_SkinThighR = GetCachedPart("SkinThighR");
		Part_SkinButtL = GetCachedPart("SkinButtL");
		Part_SkinButtR = GetCachedPart("SkinButtR");
		Part_SkinPelvis = GetCachedPart("SkinPelvis");
		Part_OmegaSignInside = GetCachedPart("OmegaSignInside");
		Part_OmegaSign = GetCachedPart("OmegaSign");
		Part_SkinWaist = GetCachedPart("SkinWaist");
		Part_Barcode = GetCachedPart("Barcode");
		Part_SkinRibcage = GetCachedPart("SkinRibcage");
		Part_SkinUpperArmL = GetCachedPart("SkinUpperArmL");
		Part_SkinUpperArmR = GetCachedPart("SkinUpperArmR");
		Part_AttachArmL = GetCachedPart("AttachArmL");
		Part_AttachArmR = GetCachedPart("AttachArmR");
		Part_AttachArmLForearm = GetCachedPart("AttachArmLForearm");
		Part_AttachArmRForearm = GetCachedPart("AttachArmRForearm");
		Part_CockMask = GetCachedPart("CockMask");
		Part_ModdableBack = GetCachedPart("ModdableBack");
		Part_ModdableShirtLogo = GetCachedPart("ModdableShirtLogo");
		Part_ModdableShirtRightSleeveLogo = GetCachedPart("ModdableShirtRightSleeveLogo");
		Part_ModdableShirtLeftSleeveLogo = GetCachedPart("ModdableShirtLeftSleeveLogo");
		Part_ModdableHoodieLogo = GetCachedPart("ModdableHoodieLogo");
		Part_ModdablePantiesLogo = GetCachedPart("ModdablePantiesLogo");
		Part_SkinCumflation = GetCachedPart("SkinCumflation");
		Part_TailMain10 = GetCachedPart("TailMain10");
		Part_TailMain9 = GetCachedPart("TailMain9");
		Part_TailMain8 = GetCachedPart("TailMain8");
		Part_TailMain7 = GetCachedPart("TailMain7");
		Part_TailMain6 = GetCachedPart("TailMain6");
		Part_TailMain5 = GetCachedPart("TailMain5");
		Part_TailMain4 = GetCachedPart("TailMain4");
		Part_TailMain3 = GetCachedPart("TailMain3");
		Part_TailMain2 = GetCachedPart("TailMain2");
		Part_TailMain = GetCachedPart("TailMain");
		Part_CumOutsideInnerThighL = GetCachedPart("CumOutsideInnerThighL");
		Part_CumOutsideInnerThighR = GetCachedPart("CumOutsideInnerThighR");
		Part_Dress2BottomFront = GetCachedPart("Dress2BottomFront");
		Part_Dress2BottomBack = GetCachedPart("Dress2BottomBack");
		Part_Dress2Chest = GetCachedPart("Dress2Chest");
		Part_Dress2AltWaist = GetCachedPart("Dress2AltWaist");
		Part_Dress2AltUpper = GetCachedPart("Dress2AltUpper");
		Part_Dress2RArm = GetCachedPart("Dress2RArm");
		Part_Dress2LArm = GetCachedPart("Dress2LArm");
		Part_CumOutsideFaceRight = GetCachedPart("CumOutsideFaceRight");
		Part_CumOutsideFaceNose = GetCachedPart("CumOutsideFaceNose");
		Part_CumOutsideFaceLeft = GetCachedPart("CumOutsideFaceLeft");
		Part_CumOutsideBoobLSide = GetCachedPart("CumOutsideBoobLSide");
		Part_CumWombLv8 = GetCachedPart("CumWombLv8");
		Part_CumWombLv7 = GetCachedPart("CumWombLv7");
		Part_CumWombLv6 = GetCachedPart("CumWombLv6");
		Part_CumWombLv5 = GetCachedPart("CumWombLv5");
		Part_XrayBase2 = GetCachedPart("XrayBase2");
		Part_XrayWomb2 = GetCachedPart("XrayWomb2");
		Part_Xrayouter2 = GetCachedPart("Xrayouter2");
		Part_XrayInnerL2 = GetCachedPart("XrayInnerL2");
		Part_XrayInnerR3 = GetCachedPart("XrayInnerR3");
		Part_CumOutsideBoobRSide = GetCachedPart("CumOutsideBoobRSide");
		Part_CumOutsideBoobR = GetCachedPart("CumOutsideBoobR");
		Part_CumOutsideBoobL = GetCachedPart("CumOutsideBoobL");
		Part_CumOutsideUnderBoobMiddle = GetCachedPart("CumOutsideUnderBoobMiddle");
		Part_CumOutsideBelly = GetCachedPart("CumOutsideBelly");
		Part_OmegaMask = GetCachedPart("OmegaMask");
		Part_VagWetMCFinger = GetCachedPart("VagWetMCFinger");
		Part_MCFinger = GetCachedPart("MCFinger");
		Part_MCRubClit = GetCachedPart("MCRubClit");
		Part_MCGrabThighR = GetCachedPart("MCGrabThighR");
		Part_MCGrabThighL = GetCachedPart("MCGrabThighL");
		Part_MCGrabNippleR = GetCachedPart("MCGrabNippleR");
		Part_MCGrabNippleL = GetCachedPart("MCGrabNippleL");
		Part_MCGrabBoobL = GetCachedPart("MCGrabBoobL");
		Part_MCGrabBoobR = GetCachedPart("MCGrabBoobR");
		Part_CumVagina2 = GetCachedPart("CumVagina2");
		Part_Dress1_Chest = GetCachedPart("Dress1_Chest");
		Part_Dress1_StringR = GetCachedPart("Dress1_StringR");
		Part_Dress1_LowerFront = GetCachedPart("Dress1_LowerFront");
		Part_Dress1_Waist = GetCachedPart("Dress1_Waist");
		Part_Dress1_Ribcage = GetCachedPart("Dress1_Ribcage");
		Part_Dress1_LowerBack = GetCachedPart("Dress1_LowerBack");
		Part_Dress1_StringL = GetCachedPart("Dress1_StringL");
		Part_H0_Top_LeafR = GetCachedPart("H0_Top_LeafR");
		Part_H0_Top_LeafL = GetCachedPart("H0_Top_LeafL");
		Part_H0_Side = GetCachedPart("H0_Side");
		Part_H0_Braid = GetCachedPart("H0_Braid");
		Part_H0_Back = GetCachedPart("H0_Back");
		Part_H1_Top_Ahoge = GetCachedPart("H1_Top_Ahoge");
		Part_H1_Side_Top = GetCachedPart("H1_Side_Top");
		Part_H1_Side_L2 = GetCachedPart("H1_Side_L2");
		Part_H1_Front_Bang = GetCachedPart("H1_Front_Bang");
		Part_H1_Side_L = GetCachedPart("H1_Side_L");
		Part_H1_BackL = GetCachedPart("H1_BackL");
		Part_H1_Back_R1 = GetCachedPart("H1_Back_R1");
		Part_H1_Back_R2 = GetCachedPart("H1_Back_R2");
		Part_H3_Side = GetCachedPart("H3_Side");
		Part_H3_Back_Top = GetCachedPart("H3_Back_Top");
		Part_H3_Back = GetCachedPart("H3_Back");
		Part_H4_Knot = GetCachedPart("H4_Knot");
		Part_H4_Tail = GetCachedPart("H4_Tail");
		Part_ShadowBoobL = GetCachedPart("ShadowBoobL");
		Part_ShadowBoobR = GetCachedPart("ShadowBoobR");
		Part_NipPiercingLeft = GetCachedPart("NipPiercingLeft");
		Part_NipPiercingRight = GetCachedPart("NipPiercingRight");
		Part_NavelPiercing = GetCachedPart("NavelPiercing");
		Part_ModdableHairFront = GetCachedPart("ModdableHairFront");
		Part_CuteBow = GetCachedPart("CuteBow");
		Part_Headband = GetCachedPart("Headband");
		Part_WizardHat = GetCachedPart("WizardHat");
		Part_H0_FrontClippedUp_Bang = GetCachedPart("H0_FrontClippedUp_Bang");
		Part_H0_FrontClippedUp_HairClip = GetCachedPart("H0_FrontClippedUp_HairClip");
		Part_H0_Front_Bang = GetCachedPart("H0_Front_Bang");
		Part_H0_Side_BangL = GetCachedPart("H0_Side_BangL");
		Part_H0_Side_BangR = GetCachedPart("H0_Side_BangR");
		Part_H0_SideAlt_Strand = GetCachedPart("H0_SideAlt_Strand");
		Part_H2_Front_Bang = GetCachedPart("H2_Front_Bang");
		Part_H3_Front = GetCachedPart("H3_Front");
		Part_BikiniBot_A_BowL = GetCachedPart("BikiniBot_A_BowL");
		Part_BikiniBot_A_BowR = GetCachedPart("BikiniBot_A_BowR");
		Part_BikiniBot_M_Cloth = GetCachedPart("BikiniBot_M_Cloth");
		Part_BikiniBot_A_Outline = GetCachedPart("BikiniBot_A_Outline");
		Part_BikiniTop_A_OutlineL = GetCachedPart("BikiniTop_A_OutlineL");
		Part_BikiniTop_M_CupL = GetCachedPart("BikiniTop_M_CupL");
		Part_BikiniTop_A_OutlineR = GetCachedPart("BikiniTop_A_OutlineR");
		Part_BikiniTop_M_CupR = GetCachedPart("BikiniTop_M_CupR");
		Part_BikiniTop_A_StrapL = GetCachedPart("BikiniTop_A_StrapL");
		Part_BikiniTop_A_StrapR = GetCachedPart("BikiniTop_A_StrapR");
		Part_BikiniTop_A_Middle = GetCachedPart("BikiniTop_A_Middle");
		coloringGroups[ColoringGroupEnum.Hair0_Top] = new ColoringGroup(ColoringGroupEnum.Hair0_Top, new List<Part> { Part_H0_Top_LeafR, Part_H0_Top_LeafL });
		coloringGroups[ColoringGroupEnum.Hair0_Back] = new ColoringGroup(ColoringGroupEnum.Hair0_Back, new List<Part> { Part_H0_Back });
		coloringGroups[ColoringGroupEnum.Hair0_Braid] = new ColoringGroup(ColoringGroupEnum.Hair0_Braid, new List<Part> { Part_H0_Braid });
		coloringGroups[ColoringGroupEnum.Hair0_Braid_HairTieAccessory] = new ColoringGroup(ColoringGroupEnum.Hair0_Braid_HairTieAccessory, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair0_Side] = new ColoringGroup(ColoringGroupEnum.Hair0_Side, new List<Part> { Part_H0_Side, Part_H0_Side_BangL, Part_H0_Side_BangR });
		coloringGroups[ColoringGroupEnum.Hair0_SideAlt] = new ColoringGroup(ColoringGroupEnum.Hair0_SideAlt, new List<Part> { Part_H0_SideAlt_Strand });
		coloringGroups[ColoringGroupEnum.Hair0_Front] = new ColoringGroup(ColoringGroupEnum.Hair0_Front, new List<Part> { Part_H0_Front_Bang });
		coloringGroups[ColoringGroupEnum.Hair0_FrontClippedUp_Bang] = new ColoringGroup(ColoringGroupEnum.Hair0_FrontClippedUp_Bang, new List<Part> { Part_H0_FrontClippedUp_Bang });
		coloringGroups[ColoringGroupEnum.Hair0_FrontClippedUp_HairClip] = new ColoringGroup(ColoringGroupEnum.Hair0_FrontClippedUp_HairClip, new List<Part> { Part_H0_FrontClippedUp_HairClip });
		coloringGroups[ColoringGroupEnum.Hair1_Top] = new ColoringGroup(ColoringGroupEnum.Hair1_Top, new List<Part> { Part_H1_Top_Ahoge });
		coloringGroups[ColoringGroupEnum.Hair1_Front] = new ColoringGroup(ColoringGroupEnum.Hair1_Front, new List<Part> { Part_H1_Front_Bang });
		coloringGroups[ColoringGroupEnum.Hair1_Side] = new ColoringGroup(ColoringGroupEnum.Hair1_Side, new List<Part> { Part_H1_Side_Top, Part_H1_Side_L2, Part_H1_Side_L });
		coloringGroups[ColoringGroupEnum.Hair1_Back] = new ColoringGroup(ColoringGroupEnum.Hair1_Back, new List<Part> { Part_H1_BackL, Part_H1_Back_R1, Part_H1_Back_R2 });
		coloringGroups[ColoringGroupEnum.Hair2_Front] = new ColoringGroup(ColoringGroupEnum.Hair2_Front, new List<Part> { Part_H2_Front_Bang });
		coloringGroups[ColoringGroupEnum.Hair3_Front] = new ColoringGroup(ColoringGroupEnum.Hair3_Front, new List<Part> { Part_H3_Front });
		coloringGroups[ColoringGroupEnum.Hair3_Side] = new ColoringGroup(ColoringGroupEnum.Hair3_Side, new List<Part> { Part_H3_Side });
		coloringGroups[ColoringGroupEnum.Hair3_Top] = new ColoringGroup(ColoringGroupEnum.Hair3_Top, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair3_Back] = new ColoringGroup(ColoringGroupEnum.Hair3_Back, new List<Part> { Part_H3_Back_Top, Part_H3_Back });
		coloringGroups[ColoringGroupEnum.Hair4_Ponytail] = new ColoringGroup(ColoringGroupEnum.Hair4_Ponytail, new List<Part> { Part_H4_Tail });
		coloringGroups[ColoringGroupEnum.Hair4_KnotAccessory] = new ColoringGroup(ColoringGroupEnum.Hair4_KnotAccessory, new List<Part> { Part_H4_Knot });
		coloringGroups[ColoringGroupEnum.Skirt] = new ColoringGroup(ColoringGroupEnum.Skirt, new List<Part> { Part_SkirtLowerFront, Part_SkirtUpper, Part_SkirtLowerBack });
		coloringGroups[ColoringGroupEnum.Shirt] = new ColoringGroup(ColoringGroupEnum.Shirt, new List<Part> { Part_PlainShirtChest, Part_PlainShirtFront, Part_PlainShirtSleeveR, Part_PlainShirtSleeveL, Part_PlainShirtBack });
		coloringGroups[ColoringGroupEnum.ShirtShadow] = new ColoringGroup(ColoringGroupEnum.ShirtShadow, new List<Part>());
		coloringGroups[ColoringGroupEnum.StockingL] = new ColoringGroup(ColoringGroupEnum.StockingL, new List<Part> { Part_StockingL });
		coloringGroups[ColoringGroupEnum.StockingR] = new ColoringGroup(ColoringGroupEnum.StockingR, new List<Part> { Part_StockingR });
		coloringGroups[ColoringGroupEnum.Panties] = new ColoringGroup(ColoringGroupEnum.Panties, new List<Part> { Part_PantiesFront, Part_PantiesBack });
		coloringGroups[ColoringGroupEnum.Bra] = new ColoringGroup(ColoringGroupEnum.Bra, new List<Part> { Part_BraChest, Part_BraTop, Part_BraBottom });
		coloringGroups[ColoringGroupEnum.Hoodie] = new ColoringGroup(ColoringGroupEnum.Hoodie, new List<Part> { Part_LongHoodieHood, Part_LongHoodieChest, Part_LongHoodieFront, Part_LongHoodieUppperArmL, Part_LongHoodieUppperArmR, Part_LongHoodieBack });
		coloringGroups[ColoringGroupEnum.HoodieStrings] = new ColoringGroup(ColoringGroupEnum.HoodieStrings, new List<Part> { Part_AltLongHoodieStringR, Part_AltLongHoodieStringL });
		coloringGroups[ColoringGroupEnum.Pants] = new ColoringGroup(ColoringGroupEnum.Pants, new List<Part> { Part_BPantsCrotch, Part_BPantsThighL, Part_BPantsThighR, Part_BPantsBehind });
		coloringGroups[ColoringGroupEnum.PantsBelt] = new ColoringGroup(ColoringGroupEnum.PantsBelt, new List<Part> { Part_AltBPantsBelt2, Part_AltBPantsBelt, Part_AltBPantsBeltBehind });
		coloringGroups[ColoringGroupEnum.Dress1] = new ColoringGroup(ColoringGroupEnum.Dress1, new List<Part> { Part_Dress1_Chest, Part_Dress1_StringR, Part_Dress1_LowerFront, Part_Dress1_Waist, Part_Dress1_Ribcage, Part_Dress1_LowerBack, Part_Dress1_StringL });
		coloringGroups[ColoringGroupEnum.Dress2Alt] = new ColoringGroup(ColoringGroupEnum.Dress2Alt, new List<Part> { Part_Dress2AltWaist, Part_Dress2AltUpper });
		coloringGroups[ColoringGroupEnum.Dress2] = new ColoringGroup(ColoringGroupEnum.Dress2, new List<Part> { Part_Dress2BottomFront, Part_Dress2BottomBack, Part_Dress2Chest, Part_Dress2RArm, Part_Dress2LArm });
		coloringGroups[ColoringGroupEnum.ShoeR] = new ColoringGroup(ColoringGroupEnum.ShoeR, new List<Part>());
		coloringGroups[ColoringGroupEnum.ShoeL] = new ColoringGroup(ColoringGroupEnum.ShoeL, new List<Part>());
		coloringGroups[ColoringGroupEnum.PointyEars] = new ColoringGroup(ColoringGroupEnum.PointyEars, new List<Part> { Part_PointyEarL, Part_PointyEarR });
		coloringGroups[ColoringGroupEnum.WizardHat] = new ColoringGroup(ColoringGroupEnum.WizardHat, new List<Part> { Part_WizardHat });
		coloringGroups[ColoringGroupEnum.BellChoker] = new ColoringGroup(ColoringGroupEnum.BellChoker, new List<Part> { Part_BellChokerMain, Part_BellChokerBell });
		coloringGroups[ColoringGroupEnum.CuteBow] = new ColoringGroup(ColoringGroupEnum.CuteBow, new List<Part> { Part_CuteBow });
		coloringGroups[ColoringGroupEnum.PumpkinHairPin] = new ColoringGroup(ColoringGroupEnum.PumpkinHairPin, new List<Part>());
		coloringGroups[ColoringGroupEnum.Headband] = new ColoringGroup(ColoringGroupEnum.Headband, new List<Part> { Part_Headband });
		coloringGroups[ColoringGroupEnum.NipplePiercingL] = new ColoringGroup(ColoringGroupEnum.NipplePiercingL, new List<Part> { Part_NipPiercingLeft });
		coloringGroups[ColoringGroupEnum.NipplePiercingR] = new ColoringGroup(ColoringGroupEnum.NipplePiercingR, new List<Part> { Part_NipPiercingRight });
		coloringGroups[ColoringGroupEnum.NavelPiercing] = new ColoringGroup(ColoringGroupEnum.NavelPiercing, new List<Part> { Part_NavelPiercing });
		coloringGroups[ColoringGroupEnum.BikiniTop_Main] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Main, new List<Part> { Part_BikiniTop_M_CupL, Part_BikiniTop_M_CupR });
		coloringGroups[ColoringGroupEnum.BikiniTop_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Alt, new List<Part> { Part_BikiniTop_A_OutlineL, Part_BikiniTop_A_OutlineR, Part_BikiniTop_A_StrapL, Part_BikiniTop_A_StrapR, Part_BikiniTop_A_Middle });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Main] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Main, new List<Part> { Part_BikiniBot_M_Cloth });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Alt, new List<Part> { Part_BikiniBot_A_BowL, Part_BikiniBot_A_BowR, Part_BikiniBot_A_Outline });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Uncolored] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Uncolored, new List<Part>());
		coloringGroups[ColoringGroupEnum.IrisL] = new ColoringGroup(ColoringGroupEnum.IrisL, new List<Part> { Part_PupilL });
		coloringGroups[ColoringGroupEnum.IrisR] = new ColoringGroup(ColoringGroupEnum.IrisR, new List<Part> { Part_PupilR });
		coloringGroups[ColoringGroupEnum.PupilL] = new ColoringGroup(ColoringGroupEnum.PupilL, new List<Part> { Part_Iris1L });
		coloringGroups[ColoringGroupEnum.PupilR] = new ColoringGroup(ColoringGroupEnum.PupilR, new List<Part> { Part_Iris1R });
		coloringGroups[ColoringGroupEnum.Sclera] = new ColoringGroup(ColoringGroupEnum.Sclera, new List<Part> { Part_EyeBall });
		coloringGroups[ColoringGroupEnum.Eyelash] = new ColoringGroup(ColoringGroupEnum.Eyelash, new List<Part>());
		coloringGroups[ColoringGroupEnum.HeartPupils] = new ColoringGroup(ColoringGroupEnum.HeartPupils, new List<Part> { Part_Iris2L, Part_Iris2R });
		coloringGroups[ColoringGroupEnum.EyeBrows] = new ColoringGroup(ColoringGroupEnum.EyeBrows, new List<Part> { Part_BrowL, Part_BrowR });
		coloringGroups[ColoringGroupEnum.Nipples] = new ColoringGroup(ColoringGroupEnum.Nipples, new List<Part> { Part_NippleL, Part_NippleR });
		coloringGroups[ColoringGroupEnum.Vagina] = new ColoringGroup(ColoringGroupEnum.Vagina, new List<Part> { Part_VaginaClit, Part_VaginaInner });
		coloringGroups[ColoringGroupEnum.Xray] = new ColoringGroup(ColoringGroupEnum.Xray, new List<Part> { Part_XrayBase2, Part_XrayWomb2, Part_Xrayouter2, Part_XrayInnerL2, Part_XrayInnerR3 });
		coloringGroups[ColoringGroupEnum.Anus] = new ColoringGroup(ColoringGroupEnum.Anus, new List<Part>());
		coloringGroups[ColoringGroupEnum.Teeth] = new ColoringGroup(ColoringGroupEnum.Teeth, new List<Part> { Part_TeethUpper, Part_TeethLower });
		coloringGroups[ColoringGroupEnum.Mouth] = new ColoringGroup(ColoringGroupEnum.Mouth, new List<Part> { Part_Tounge, Part_InnerMouthTounge, Part_InnerMouth });
		coloringGroups[ColoringGroupEnum.Boobs] = new ColoringGroup(ColoringGroupEnum.Boobs, new List<Part> { Part_SkinBoobL, Part_SkinBoobR });
		coloringGroups[ColoringGroupEnum.Ass] = new ColoringGroup(ColoringGroupEnum.Ass, new List<Part> { Part_SkinButtL, Part_SkinButtR });
		coloringGroups[ColoringGroupEnum.Pelvic] = new ColoringGroup(ColoringGroupEnum.Pelvic, new List<Part> { Part_SkinVaginaL, Part_SkinVaginaR, Part_SkinPelvis });
		coloringGroups[ColoringGroupEnum.Thigh] = new ColoringGroup(ColoringGroupEnum.Thigh, new List<Part>());
		coloringGroups[ColoringGroupEnum.Tummy] = new ColoringGroup(ColoringGroupEnum.Tummy, new List<Part>());
		coloringGroups[ColoringGroupEnum.Skin] = new ColoringGroup(ColoringGroupEnum.Skin, new List<Part>
		{
			Part_SkinNose, Part_SkinLipUpper, Part_SkinLipLower, Part_SkinLipUpperToungeOut, Part_SkinLipLowerToungeOut, Part_SkinFace, Part_SkinEyelidClosedL, Part_SkinEyelidClosedR, Part_SkinEyelidL, Part_SkinEyelidR,
			Part_SkinEyelid2L, Part_SkinEyelid2R, Part_SkinNeck2, Part_SkinPalvisTop, Part_SkinThighL, Part_SkinThighR, Part_SkinWaist, Part_SkinRibcage, Part_SkinUpperArmL, Part_SkinUpperArmR,
			Part_SkinCumflation
		});
		coloringGroups[ColoringGroupEnum.Blush] = new ColoringGroup(ColoringGroupEnum.Blush, new List<Part> { Part_blush_2, Part_blush });
		coloringGroups[ColoringGroupEnum.SharpTeeth] = new ColoringGroup(ColoringGroupEnum.SharpTeeth, new List<Part> { Part_SharpTeethUpper, Part_SharpTeethLower });
		coloringGroups[ColoringGroupEnum.CatEarMain] = new ColoringGroup(ColoringGroupEnum.CatEarMain, new List<Part> { Part_CatearFrontR, Part_CatearFrontL, Part_CatearBackR, Part_CatearBackL });
		coloringGroups[ColoringGroupEnum.CatEarSecondary] = new ColoringGroup(ColoringGroupEnum.CatEarSecondary, new List<Part> { Part_CatearMidL, Part_CatearMidR });
		coloringGroups[ColoringGroupEnum.LegLFoot] = new ColoringGroup(ColoringGroupEnum.LegLFoot, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegL] = new ColoringGroup(ColoringGroupEnum.LegL, new List<Part> { Part_AttachLegL });
		coloringGroups[ColoringGroupEnum.LegRFoot] = new ColoringGroup(ColoringGroupEnum.LegRFoot, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegR] = new ColoringGroup(ColoringGroupEnum.LegR, new List<Part> { Part_AttachLegR });
		coloringGroups[ColoringGroupEnum.ArmL] = new ColoringGroup(ColoringGroupEnum.ArmL, new List<Part> { Part_AttachArmL, Part_AttachArmLForearm });
		coloringGroups[ColoringGroupEnum.ArmR] = new ColoringGroup(ColoringGroupEnum.ArmR, new List<Part> { Part_AttachArmR, Part_AttachArmRForearm });
		coloringGroups[ColoringGroupEnum.Tail] = new ColoringGroup(ColoringGroupEnum.Tail, new List<Part> { Part_TailMain10, Part_TailMain9, Part_TailMain8, Part_TailMain7, Part_TailMain6, Part_TailMain5, Part_TailMain4, Part_TailMain3, Part_TailMain2, Part_TailMain });
		coloringGroups[ColoringGroupEnum.TailPlug] = new ColoringGroup(ColoringGroupEnum.TailPlug, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hologram] = new ColoringGroup(ColoringGroupEnum.Hologram, new List<Part> { Part_HairHologram });
		coloringGroups[ColoringGroupEnum.OtherAttachments] = new ColoringGroup(ColoringGroupEnum.OtherAttachments, new List<Part>());
		coloringGroups[ColoringGroupEnum.MCSkin] = new ColoringGroup(ColoringGroupEnum.MCSkin, new List<Part>
		{
			Part_MCBody, Part_headpet_1, Part_headpet_2, Part_headpet_3, Part_MCFinger, Part_MCRubClit, Part_MCGrabThighR, Part_MCGrabThighL, Part_MCGrabNippleR, Part_MCGrabNippleL,
			Part_MCGrabBoobL, Part_MCGrabBoobR
		});
		coloringGroups[ColoringGroupEnum.MCCockSkin] = new ColoringGroup(ColoringGroupEnum.MCCockSkin, new List<Part> { Part_DickSkin });
		coloringGroups[ColoringGroupEnum.MCCockTip] = new ColoringGroup(ColoringGroupEnum.MCCockTip, new List<Part> { Part_DickMain });
		coloringGroups[ColoringGroupEnum.Shadow] = new ColoringGroup(ColoringGroupEnum.Shadow, new List<Part> { Part_ShadowBoobL, Part_ShadowBoobR });
		coloringGroups[ColoringGroupEnum.Highlight] = new ColoringGroup(ColoringGroupEnum.Highlight, new List<Part> { Part_highlight });
		coloringGroups[ColoringGroupEnum.Mask] = new ColoringGroup(ColoringGroupEnum.Mask, new List<Part> { Part_CockMask, Part_OmegaMask });
		coloringGroups[ColoringGroupEnum.Moddable] = new ColoringGroup(ColoringGroupEnum.Moddable, new List<Part> { Part_ModdableHairBack, Part_ModdableFace, Part_ModdableBack, Part_ModdableShirtLogo, Part_ModdableShirtRightSleeveLogo, Part_ModdableShirtLeftSleeveLogo, Part_ModdableHoodieLogo, Part_ModdablePantiesLogo, Part_ModdableHairFront });
		coloringGroups[ColoringGroupEnum.HitArea] = new ColoringGroup(ColoringGroupEnum.HitArea, new List<Part>
		{
			HitAreaBoobL, HitAreaBoobR, HitAreaLegR, HitAreaLegL, HitAreaBothBoobs, HitAreaFingering, HitAreaClit, HitAreaBothNipples, HitAreaNippleL, HitAreaNippleR,
			HitAreaHeadpat, HitAreaOpenMouth, HitAreaCloseMouth, HitAreaPanties
		});
		coloringGroups[ColoringGroupEnum.Cum] = new ColoringGroup(ColoringGroupEnum.Cum, new List<Part>
		{
			Part_CumOutsideBellyR, Part_CumOutsideInnerThighL2, Part_CumOutsideInnerThighLowerL, Part_CumOutsideInnerThighR2, Part_CumDick1, Part_CumDick2, Part_CumOutsideInnerThighL, Part_CumOutsideInnerThighR, Part_CumOutsideFaceRight, Part_CumOutsideFaceNose,
			Part_CumOutsideFaceLeft, Part_CumOutsideBoobLSide, Part_CumWombLv8, Part_CumWombLv7, Part_CumWombLv6, Part_CumWombLv5, Part_CumOutsideBoobRSide, Part_CumOutsideBoobR, Part_CumOutsideBoobL, Part_CumOutsideUnderBoobMiddle,
			Part_CumOutsideBelly, Part_CumVagina2
		});
		coloringGroups[ColoringGroupEnum.Rest] = new ColoringGroup(ColoringGroupEnum.Rest, new List<Part> { Part_lines, Part_Saliva2, Part_Saliva1, Part_OmegaSignInside, Part_OmegaSign, Part_Barcode, Part_VagWetMCFinger });
	}
}
