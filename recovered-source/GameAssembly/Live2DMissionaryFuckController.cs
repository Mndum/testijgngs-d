using System.Collections.Generic;
using System.Linq;
using Fungus;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Physics;
using Live2D.Cubism.Framework.Raycasting;
using UnityEngine;

public sealed class Live2DMissionaryFuckController : Live2DLewdSceneController, ILive2DController_BodyAngles, ILive2DController_BodyX, ILive2DController_BodyY, ILive2DController_HeadAngles, ILive2DController_HeadX, ILive2DController_HeadY, ILive2DController_HeadZ, ILive2DController_CatEarsMovement, ILive2DController_Headpat, ILive2DController_TongueMovement, ILive2DController_CommonFace, ILive2DController_MainFace, ILive2DController_Mouth, ILive2DController_EyeMovement, ILive2DController_EyeLMovement, ILive2DController_EyeRMovement, ILive2DController_EyeLEmotes, ILive2DController_EyeLCloseAndShock, ILive2DController_BrowLY, ILive2DController_BrowLRot, ILive2DController_BrowLEmote, ILive2DController_EyeREmotes, ILive2DController_EyeRCloseAndShock, ILive2DController_BrowRY, ILive2DController_BrowRRot, ILive2DController_BrowREmote, ILive2DController_BlushAndHeartEyes, ILive2DController_IndependentBlinking, ILive2DController_EyesHappinessEmote, ILive2DController_SalivaMouth, ILive2DController_OmegaMeter, ILive2DController_TailWiggle, ILive2DController_Lewd_BotTense, ILive2DController_Lewd_BotOrgasm, ILive2DController_Manager_XRay, ILive2DController_Lewd_CumShot, ILive2DController_Lewd_CumShotCommon, ILive2DController_Lewd_CumShotBasic, ILive2DController_Lewd_CumShotLength, ILive2DController_Lewd_CumShotX, ILive2DController_Lewd_CumInsideMain, ILive2DController_Lewd_CumInsideAnal, ILive2DController_Lewd_CumInsideWomb, ILive2DController_Lewd_CumInsideVagina, ILive2DController_Lewd_LegQuiver, ILive2DController_Lewd_CumLeakBasic, ILive2DController_Manager_FrickStaticGuiManager, ILive2DController_Manager_InsertionManager, ILive2DController_Manager_CockManager, ILive2DController_Manager_Camera, ILive2DController_BodyMod_AllLegs, ILive2DController_BodyMod_ThighSize, ILive2DController_BodyMod_CalfSize, ILive2DController_BodyMod_FootSize, ILive2DController_BodyMod_BoobSize, ILive2DController_BodyMod_AssSize
{
	public enum MissionarySceneCameraEnum
	{
		NoSetting,
		NormalView,
		ExpandedView
	}

	private Live2DInteractableLimbUsage<Live2DMissionaryFuckController> _handholdingInteractable;

	private Live2DInteractableLimbUsage<Live2DMissionaryFuckController> _pussyFingeringInteractable;

	private Live2DInteractableLimbUsage<Live2DMissionaryFuckController> _pussySpreadInteractable;

	private Live2DInteractableLimbUsage<Live2DMissionaryFuckController> _clitRubInteractable;

	private Live2DInteractableLimbUsage<Live2DMissionaryFuckController> _pussyEatOutInteractable;

	private Live2DInteractableLimbUsage<Live2DMissionaryFuckController> _assEatOutInteractable;

	private Live2DInteractableLimbUsage<Live2DMissionaryFuckController> _thumbMouthInteractable;

	private BasicInsertionHandleFuckAxis VaginaCockFuckAxis;

	private BasicInsertionHandleFuckAxis VaginaFingeringFuckAxis;

	private BasicInsertionHandleFuckAxis AnalCockFuckAxis;

	private BasicInsertionHandleFuckAxis AnalFingeringFuckAxis;

	private CockLimbUsage<Live2DMissionaryFuckController> _cockFuckLimbUsage;

	private MissionarySceneCameraEnum _currentSceneCamera;

	public override ControllerEnum ControllerEnum => ControllerEnum.Missionary;

	public override SceneEnumFlag SceneEnum => SceneEnumFlag.Missionary;

	public Live2DInteractable ShirtInteractable { get; private set; }

	public Live2DInteractable HoodieInteractable { get; private set; }

	public Live2DInteractable PantsInteractable { get; private set; }

	public Live2DInteractable PantiesInteractable { get; private set; }

	public bool ArePantiesToRight => ParamPantiesX.Value > 0.8f;

	public bool ArePantiesToLeft => ParamPantiesX.Value < -0.8f;

	public override bool IsPantiesAside
	{
		get
		{
			if (!ArePantiesToLeft)
			{
				return ArePantiesToRight;
			}
			return true;
		}
	}

	public override bool IsShirtAside => ParamShirt.Value > 0.45f;

	public override bool IsHoodieAside => ParamHoodie.Value > 0.45f;

	public override bool IsPantsAside
	{
		get
		{
			return ParamPantsAside.Value > 0.01f;
		}
		set
		{
			ParamPantsAside.Value = (value ? ParamPantsAside.MaximumValue : ParamPantsAside.MinimumValue);
			ParamPantsAside.FinishMove();
		}
	}

	public override bool IsSkirtAside => true;

	public Live2DInteractableLimbUsage<Live2DMissionaryFuckController> BodyInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DMissionaryFuckController> NippleLInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DMissionaryFuckController> NippleRInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DMissionaryFuckController> NippleBothInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DMissionaryFuckController> BoobLFondleInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DMissionaryFuckController> BoobRFondleInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DMissionaryFuckController> BoobBothFondleInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DMissionaryFuckController> LegsGrabInteractable { get; private set; }

	public Live2DParameter ParamBodyY => ParamPelvisY;

	public Live2DParameter ParamBodyX => ParamPelvisX;

	public InsertionHandler VaginaInsertionHandler { get; private set; }

	public InsertionHandler AnalInsertionHandler { get; private set; }

	public MissionarySceneCameraEnum CurrentSceneCamera => _currentSceneCamera;

	public ICameraManager CameraManager { get; private set; }

	public IXRayManager XRayManager { get; private set; }

	public FrickStaticGuiManager FrickStaticGuiManager { get; private set; }

	public InsertionManager InsertionManager { get; private set; }

	public ICockManager CockManager { get; private set; }

	public ISimpleCumLeakManager CumLeakManager { get; private set; }

	public Live2DParameter ParamEyeballLX { get; private set; }

	public Live2DParameter ParamEyeballLY { get; private set; }

	public Live2DParameter ParamEyeballRX { get; private set; }

	public Live2DParameter ParamEyeballRY { get; private set; }

	public Live2DParameter ParamHeart { get; private set; }

	public Live2DParameter ParamIrisZoom { get; private set; }

	public Live2DParameter ParamEyesHappy { get; private set; }

	public Live2DParameter ParamEyeOpen { get; private set; }

	public Live2DParameter ParamBrowREmote { get; private set; }

	public Live2DParameter ParamBrowLEmote { get; private set; }

	public Live2DParameter ParamBrowRRot { get; private set; }

	public Live2DParameter ParamBrowLRot { get; private set; }

	public Live2DParameter ParamEyeLShock { get; private set; }

	public Live2DParameter ParamEyeRShock { get; private set; }

	public Live2DParameter ParamBrowLY { get; private set; }

	public Live2DParameter ParamBrowRY { get; private set; }

	public Live2DParameter ParamToungeY { get; private set; }

	public Live2DParameter ParamToungeX { get; private set; }

	public Live2DParameter ParamMouthForm { get; private set; }

	public Live2DParameter ParamSaliva { get; private set; }

	public Live2DParameter ParamMouthOpen { get; private set; }

	public Live2DParameter ParamBlush { get; private set; }

	public Live2DParameter ParamHeadX { get; private set; }

	public Live2DParameter ParamHeadZ { get; private set; }

	public Live2DParameter ParamHeadY { get; private set; }

	public Live2DParameter ParamEarsFlick { get; private set; }

	public Live2DParameter ParamEarR { get; private set; }

	public Live2DParameter ParamEarL { get; private set; }

	public Live2DParameter ParamHeadpatX { get; private set; }

	public Live2DParameter ParamHeadpatY { get; private set; }

	public Live2DParameter ParamHeadpat { get; private set; }

	public Live2DParameter ParamHairX { get; private set; }

	public Live2DParameter ParamSalivaPhysics { get; private set; }

	public Live2DParameter ParamPupilWiggle { get; private set; }

	public Live2DParameter ParamEarsWiggle { get; private set; }

	public Live2DParameter ParamSocksEnabled { get; private set; }

	public Live2DParameter ParamPantsAside { get; private set; }

	public Live2DParameter ParamHoodie { get; private set; }

	public Live2DParameter ParamShirt { get; private set; }

	public Live2DParameter ParamPantiesX { get; private set; }

	public Live2DParameter ParamPantiesY { get; private set; }

	public Live2DParameter ParamFlattenBoobs { get; private set; }

	public Live2DParameter ParamFlattenNipples { get; private set; }

	public Live2DParameter ParamPelvisX { get; private set; }

	public Live2DParameter ParamPelvisY { get; private set; }

	public Live2DParameter ParamHands { get; private set; }

	public Live2DParameter ParamVaginaAroused { get; private set; }

	public Live2DParameter ParamTense { get; private set; }

	public Live2DParameter ParamOrgasm { get; private set; }

	public Live2DParameter ParamLegsQuiver { get; private set; }

	public Live2DParameter ParamOmegaMeter { get; private set; }

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

	public Live2DParameter ParamCumLeakAnim { get; private set; }

	public Live2DParameter ParamCumInside { get; private set; }

	public Live2DParameter ParamCumInsideWomb { get; private set; }

	public Live2DParameter ParamVaginaSpread { get; private set; }

	public Live2DParameter ParamVaginaClitMoveX { get; private set; }

	public Live2DParameter ParamVaginaClitMoveY { get; private set; }

	public Live2DParameter ParamCockAngleXVaginaEntrance { get; private set; }

	public Live2DParameter ParamXrayAnalR0 { get; private set; }

	public Live2DParameter ParamXrayAnalR1 { get; private set; }

	public Live2DParameter ParamXrayAnalR2 { get; private set; }

	public Live2DParameter ParamXrayAnalR3 { get; private set; }

	public Live2DParameter ParamXrayAnalR4 { get; private set; }

	public Live2DParameter ParamXrayAnalR5 { get; private set; }

	public Live2DParameter ParamXrayAnalR6 { get; private set; }

	public Live2DParameter ParamXrayAnalR7 { get; private set; }

	public Live2DParameter ParamXrayAnalL0 { get; private set; }

	public Live2DParameter ParamXrayAnalL1 { get; private set; }

	public Live2DParameter ParamXrayAnalL2 { get; private set; }

	public Live2DParameter ParamXrayAnalL3 { get; private set; }

	public Live2DParameter ParamXrayAnalL4 { get; private set; }

	public Live2DParameter ParamXrayAnalL5 { get; private set; }

	public Live2DParameter ParamXrayAnalL6 { get; private set; }

	public Live2DParameter ParamXrayAnalL7 { get; private set; }

	public Live2DParameter ParamAnalSpread { get; private set; }

	public Live2DParameter ParamCumInsideAnal { get; private set; }

	public Live2DParameter ParamCockAngleXAnalEntrance { get; private set; }

	public Live2DParameter ParamDick { get; private set; }

	public Live2DParameter ParamXray { get; private set; }

	public Live2DParameter ParamThrust { get; private set; }

	public Live2DParameter ParamThrustAnal { get; private set; }

	public Live2DParameter ParamCumShotAnim { get; private set; }

	public Live2DParameter ParamFlacidity { get; private set; }

	public Live2DParameter ParamCumOutsideX { get; private set; }

	public Live2DParameter ParamCumShotLengthY { get; private set; }

	public Live2DParameter ParamHideCumShot { get; private set; }

	public Live2DParameter ParamAnusFingeringEnable { get; private set; }

	public Live2DParameter ParamAnusFingeringInsert { get; private set; }

	public Live2DParameter ParamAnusFingeringRotation { get; private set; }

	public Live2DParameter ParamEnableLegLGrab { get; private set; }

	public Live2DParameter ParamEnableLegRGrab { get; private set; }

	public Live2DParameter ParamEnableWaistGrab { get; private set; }

	public Live2DParameter ParamEnableFingering { get; private set; }

	public Live2DParameter ParamFingeringMovement { get; private set; }

	public Live2DParameter ParamFingeringRotation { get; private set; }

	public Live2DParameter ParamEnableSpread { get; private set; }

	public Live2DParameter ParamSpreadMove { get; private set; }

	public Live2DParameter ParamEnableThumbMouth { get; private set; }

	public Live2DParameter ParamThumbMouthX { get; private set; }

	public Live2DParameter ParamHandholdingEnableL { get; private set; }

	public Live2DParameter ParamHandholdingEnableR { get; private set; }

	public Live2DParameter ParamHandholdingTightenMCL { get; private set; }

	public Live2DParameter ParamHandholdingTightenMCR { get; private set; }

	public Live2DParameter ParamClitRubEnable { get; private set; }

	public Live2DParameter ParamClitRubZ { get; private set; }

	public Live2DParameter ParamEnableNipplePinchR { get; private set; }

	public Live2DParameter ParamNippleRPinchX { get; private set; }

	public Live2DParameter ParamNippleRPinchY { get; private set; }

	public Live2DParameter ParamNipplePinchSqueezeR { get; private set; }

	public Live2DParameter ParamNipplePinchRotationR { get; private set; }

	public Live2DParameter ParamEnableNipplePinchL { get; private set; }

	public Live2DParameter ParamNippleLPinchX { get; private set; }

	public Live2DParameter ParamNippleLPinchY { get; private set; }

	public Live2DParameter ParamNipplePinchSqueezeL { get; private set; }

	public Live2DParameter ParamNipplePinchRotationL { get; private set; }

	public Live2DParameter ParamEnableBoobFondleL { get; private set; }

	public Live2DParameter ParamBoobSqueezeL { get; private set; }

	public Live2DParameter ParamEnableBoobFondleR { get; private set; }

	public Live2DParameter ParamBoobSqueezeR { get; private set; }

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

	public Live2DParameter ParamHairPhysicsBaseToShort { get; private set; }

	public Live2DParameter ParamHairPhysicsShortToMid { get; private set; }

	public Live2DParameter ParamHairPhysicsMidToLong { get; private set; }

	public Live2DParameter ParamPhysicsBoobXL { get; private set; }

	public Live2DParameter ParamPhysicsBoobYL { get; private set; }

	public Live2DParameter ParamPhysicsBoobXR { get; private set; }

	public Live2DParameter ParamPhysicsBoobYR { get; private set; }

	public Live2DParameter ParamBoobRJiggle { get; private set; }

	public Live2DParameter ParamBoobLJiggle { get; private set; }

	public Live2DParameter ParamLegRPhysics { get; private set; }

	public Live2DParameter ParamLegLPhysics { get; private set; }

	public Live2DParameter ParamPhysicsLowerLegL { get; private set; }

	public Live2DParameter ParamPhysicsLowerLegR { get; private set; }

	public Live2DParameter ParamPhysicsFootL { get; private set; }

	public Live2DParameter ParamPhysicsFootR { get; private set; }

	public Live2DParameter ParamPhysicsCumWomb { get; private set; }

	public Live2DParameter ParamAssSize { get; private set; }

	public Live2DParameter ParamBoobSize { get; private set; }

	public Live2DParameter ParamThighSize { get; private set; }

	public Live2DParameter ParamCalfSize { get; private set; }

	public Live2DParameter ParamFootWidth { get; private set; }

	public Live2DParameter ParamFootLength { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballX { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowEmote { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowRot { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeShock { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEar { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag0 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag1 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag2 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag3 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag4 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag5 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag6 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXRayVag7 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXrayAnal0 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXrayAnal1 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXrayAnal2 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXrayAnal3 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXrayAnal4 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXrayAnal5 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXrayAnal6 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamXrayAnal7 { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEnableLegGrab { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamHandholdingEnable { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamHandholdingTightenMC { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEnableNipplePinch { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamNipplePinchX { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamNipplePinchY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamNipplePinchSqueeze { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamNipplePinchRotation { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEnableBoobFondle { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBoobSqueeze { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamPhysicsBoobX { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamPhysicsBoobY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBoobJiggle { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamLegPhysics { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamPhysicsLowerLeg { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamPhysicsFoot { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Tail { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_UpperLegR { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_UpperLegL { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_LowerLegR { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_LowerLegL { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobLY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobRY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobLX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobLJiggle { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobRX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobRJiggle { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Hair { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Saliva { get; private set; }

	public Part HitAreaSpread { get; private set; }

	public Part HitAreaBoobR { get; private set; }

	public Part HitAreaNippleL { get; private set; }

	public Part HitAreaNippleR { get; private set; }

	public Part HitAreaBoobL { get; private set; }

	public Part HitAreaBothNipples { get; private set; }

	public Part HitAreaBothBoobs { get; private set; }

	public Part HitAreaGrabLegL { get; private set; }

	public Part HitAreaGrabLegR { get; private set; }

	public Part HitAreaWaistGrab { get; private set; }

	public Part HitAreaOpenMouth { get; private set; }

	public Part HitAreaCloseMouth { get; private set; }

	public Part HitAreaFaceStroke { get; private set; }

	public Part HitAreaThumbMouth { get; private set; }

	public Part HitAreaFingering { get; private set; }

	public Part HitAreaClitRub { get; private set; }

	public Part HitAreaHandholdingL { get; private set; }

	public Part HitAreaHandholdingR { get; private set; }

	public Part HitAreaHeadpat { get; private set; }

	public Part HitAreaLongHoodieMoveDown { get; private set; }

	public Part HitAreaLongHoodieMoveUp { get; private set; }

	public Part HitAreaAnusCenter { get; private set; }

	public Part HitArea_Panties { get; private set; }

	public Part Part_GlowOmegaMeterMask { get; private set; }

	public Part Part_Plainshirt_front { get; private set; }

	public Part Part_ShadowPlainshirt_shadow { get; private set; }

	public Part Part_Plainshirt_lowerback { get; private set; }

	public Part Part_Plainshirt_main_alt { get; private set; }

	public Part Part_Plainshirt_main_top_clone { get; private set; }

	public Part Part_Plainshirt_sleeveL_lower { get; private set; }

	public Part Part_Plainshirt_sleeveL_Shoulder { get; private set; }

	public Part Part_Plainshirt_sleeveL_back { get; private set; }

	public Part Part_Plainshirt_sleeveR_lower { get; private set; }

	public Part Part_Plainshirt_sleeveR_Shoulder { get; private set; }

	public Part Part_Plainshirt_sleeveR_back { get; private set; }

	public Part Part_Plainshirt_alt { get; private set; }

	public Part Part_ShadowPlainshirt_alt_shadow { get; private set; }

	public Part Part_Plainshirt_alt_back { get; private set; }

	public Part Part_Plainshirt_chest2 { get; private set; }

	public Part Part_Panties_upper { get; private set; }

	public Part Part_Panties_Above { get; private set; }

	public Part Part_Panties_Bottom { get; private set; }

	public Part Part_StockingR_foot { get; private set; }

	public Part Part_StockingL_Lower { get; private set; }

	public Part Part_StockingL_foot { get; private set; }

	public Part Part_StockingL_thigh { get; private set; }

	public Part Part_StockingR_lower { get; private set; }

	public Part Part_StockingR_thigh { get; private set; }

	public Part Part_BraChest { get; private set; }

	public Part Part_BraTopL { get; private set; }

	public Part Part_BraTopR { get; private set; }

	public Part Part_SkirtUpper { get; private set; }

	public Part Part_SkirtBack { get; private set; }

	public Part Part_ModdableBack { get; private set; }

	public Part Part_BPantsLowerR { get; private set; }

	public Part Part_BPantsLowerL { get; private set; }

	public Part Part_BPantsUpperR { get; private set; }

	public Part Part_BPantsUpperL { get; private set; }

	public Part Part_AltBPantsBelt { get; private set; }

	public Part Part_BPantsCrotch { get; private set; }

	public Part Part_AltLongHoodieStringL { get; private set; }

	public Part Part_AltLongHoodieStringR { get; private set; }

	public Part Part_LongHoodieHoodFront { get; private set; }

	public Part Part_LongHoodieHoodBack { get; private set; }

	public Part Part_LongHoodieBottomFront { get; private set; }

	public Part Part_LongHoodieChest { get; private set; }

	public Part Part_LongHoodieFront { get; private set; }

	public Part Part_LongHoodieSleeve2L { get; private set; }

	public Part Part_LongHoodieSleeve2R { get; private set; }

	public Part Part_LongHoodieSleeveL { get; private set; }

	public Part Part_LongHoodieSleeveR { get; private set; }

	public Part Part_LongHoodieBack { get; private set; }

	public Part Part_BPantsRemoved { get; private set; }

	public Part Part_LongHoodieUpFront { get; private set; }

	public Part Part_LongHoodieUpBack { get; private set; }

	public Part Part_CumOutsideUnderBoobMiddle { get; private set; }

	public Part Part_CumOutsideBoobR { get; private set; }

	public Part Part_CumOutsideInnerThighR { get; private set; }

	public Part Part_CumOutsideInnerThighL { get; private set; }

	public Part Part_CumOutsideBelly { get; private set; }

	public Part Part_CumWombLv1 { get; private set; }

	public Part Part_CumWombLv2 { get; private set; }

	public Part Part_CumWombLv3 { get; private set; }

	public Part Part_CumWombLv4 { get; private set; }

	public Part Part_CumVaginaLeak2 { get; private set; }

	public Part Part_CumVaginaLeak1 { get; private set; }

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

	public Part Part_SkinNeck { get; private set; }

	public Part Part_ModdableHairBack { get; private set; }

	public Part Part_ModdableFace { get; private set; }

	public Part Part_SkinVaginaPenetratedFront { get; private set; }

	public Part Part_SkinVaginaPenetratedBack { get; private set; }

	public Part Part_SkinVaginaL { get; private set; }

	public Part Part_SkinVaginaR { get; private set; }

	public Part Part_SkinLegRFront { get; private set; }

	public Part Part_SkinLegRBack { get; private set; }

	public Part Part_SkinLegLFront { get; private set; }

	public Part Part_SkinLegLBack { get; private set; }

	public Part Part_SkinPelvis { get; private set; }

	public Part Part_SkinButtL { get; private set; }

	public Part Part_SkinButtR { get; private set; }

	public Part Part_SkinBoobL { get; private set; }

	public Part Part_SkinBoobR { get; private set; }

	public Part Part_SkinRibcageFront { get; private set; }

	public Part Part_SkinArmR { get; private set; }

	public Part Part_SkinArmL { get; private set; }

	public Part Part_SkinShoulderL { get; private set; }

	public Part Part_SkinShoulderR { get; private set; }

	public Part Part_SkinRibcageBack { get; private set; }

	public Part Part_SkinTorsoLower { get; private set; }

	public Part Part_SkinTorsoUpper { get; private set; }

	public Part Part_DickSkin { get; private set; }

	public Part Part_DickMain { get; private set; }

	public Part Part_CumDick1 { get; private set; }

	public Part Part_CumDick2 { get; private set; }

	public Part Part_Xrayouter { get; private set; }

	public Part Part_XrayWomb { get; private set; }

	public Part Part_XrayBase { get; private set; }

	public Part Part_XrayAnalOuter { get; private set; }

	public Part Part_XrayAnalInnerR { get; private set; }

	public Part Part_XrayAnalInnerL { get; private set; }

	public Part Part_XrayAnalBase { get; private set; }

	public Part Part_VaginaInner { get; private set; }

	public Part Part_VaginaBase { get; private set; }

	public Part Part_AnusFront { get; private set; }

	public Part Part_AnusBack { get; private set; }

	public Part Part_InsideLegR { get; private set; }

	public Part Part_InsideLegL { get; private set; }

	public Part Part_NippleL { get; private set; }

	public Part Part_HighlightBoobL { get; private set; }

	public Part Part_NippleR { get; private set; }

	public Part Part_HightlightBoobR { get; private set; }

	public Part Part_Barcode { get; private set; }

	public Part Part_InsdeArmR { get; private set; }

	public Part Part_InsdeArmL { get; private set; }

	public Part Part_GlowOmegaMeterInner { get; private set; }

	public Part Part_GlowOmegaMeter { get; private set; }

	public Part Part_ModdableShirtLogo { get; private set; }

	public Part Part_ModdableShirtLeftSleeveLogo { get; private set; }

	public Part Part_ModdableShirtRightSleeveLogo { get; private set; }

	public Part Part_ModdablePantiesLogo { get; private set; }

	public Part Part_ModdableHoodieLogo { get; private set; }

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

	public Part Part_CumOutsideBellyR { get; private set; }

	public Part Part_CumOutsideBoobRSide { get; private set; }

	public Part Part_CumOutsideFaceRight { get; private set; }

	public Part Part_CumOutsideFaceNose { get; private set; }

	public Part Part_CumOutsideFaceLeft { get; private set; }

	public Part Part_PointyEarL { get; private set; }

	public Part Part_PointyEarR { get; private set; }

	public Part Part_BellChokerMain { get; private set; }

	public Part Part_BellChokerBell { get; private set; }

	public Part Part_XrayInnerL { get; private set; }

	public Part Part_XrayInnerR2 { get; private set; }

	public Part Part_Plainshirt_main_bottom { get; private set; }

	public Part Part_Dress2RolledUpFront { get; private set; }

	public Part Part_Dress2BottomBack { get; private set; }

	public Part Part_Dress2Chest { get; private set; }

	public Part Part_Dress2AltWaist { get; private set; }

	public Part Part_Dress2AltRib { get; private set; }

	public Part Part_Dress2SleeveL { get; private set; }

	public Part Part_Dress2SleeveR { get; private set; }

	public Part Part_DickMask { get; private set; }

	public Part Part_ThumbMask { get; private set; }

	public Part Part_ThumbSuckThumb { get; private set; }

	public Part Part_ThumbSuck { get; private set; }

	public Part Part_SkinMouthThumbSuck { get; private set; }

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

	public Part Part_MCRightHandVaginaSpread { get; private set; }

	public Part Part_MCRightHandThumbVaginaSpread { get; private set; }

	public Part Part_MCLeftHandVaginaSpread { get; private set; }

	public Part Part_MCLeftHandThumbVaginaSpread { get; private set; }

	public Part Part_MCLeftHandLegGrab { get; private set; }

	public Part Part_MCRightHandLegGrab { get; private set; }

	public Part Part_MCLeftHandNippleGrab { get; private set; }

	public Part Part_MCRightHandNippleGrab { get; private set; }

	public Part Part_ArtMesh8 { get; private set; }

	public Part Part_MCLeftHandThumbNippleGrab { get; private set; }

	public Part Part_MCRightHandWaistGrab { get; private set; }

	public Part Part_MCLeftHandWaistGrab { get; private set; }

	public Part Part_MCLeftHandMiddleFingersFingering { get; private set; }

	public Part Part_MCLeftHandFingering { get; private set; }

	public Part Part_MCRightHandFondle { get; private set; }

	public Part Part_MCLeftHandFondle { get; private set; }

	public Part Part_AttachArmRHandHandholdingAbove { get; private set; }

	public Part Part_MCLeftHandHandholding { get; private set; }

	public Part Part_AttachArmLHandHandholdingAbove { get; private set; }

	public Part Part_MCRightHandHandholding { get; private set; }

	public Part Part_ClitRub { get; private set; }

	public Part Part_CumAnal { get; private set; }

	public Part Part_CumVagina2 { get; private set; }

	public Part Part_SkinCumflation { get; private set; }

	public Part Part_AttachLegRFront { get; private set; }

	public Part Part_AttachLegRFoot { get; private set; }

	public Part Part_AttachLegRBack { get; private set; }

	public Part Part_AttachLegLFront { get; private set; }

	public Part Part_AttachLegLFoot { get; private set; }

	public Part Part_AttachLegLBack { get; private set; }

	public Part Part_AttachArmLForearm { get; private set; }

	public Part Part_AttachArmLHand1 { get; private set; }

	public Part Part_AttachArmLHand2 { get; private set; }

	public Part Part_AttachArmL { get; private set; }

	public Part Part_AttachArmRForearm { get; private set; }

	public Part Part_AttachArmRHand1 { get; private set; }

	public Part Part_AttachArmRHand2 { get; private set; }

	public Part Part_AttachArmR { get; private set; }

	public Part Part_Dress1_LowerFront { get; private set; }

	public Part Part_Dress1_Chest { get; private set; }

	public Part Part_Dress1_StringR { get; private set; }

	public Part Part_Dress1_Pelvis { get; private set; }

	public Part Part_Dress1_Waist { get; private set; }

	public Part Part_Dress1_Ribcage { get; private set; }

	public Part Part_Dress1_LowerBack { get; private set; }

	public Part Part_Dress1_StringsL { get; private set; }

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

	public Part Part_CumOutsideInnerThighLowerL { get; private set; }

	public Part Part_CumOutsideInnerThighL2 { get; private set; }

	public Part Part_CumOutsideInnerThighR2 { get; private set; }

	public Part Part_CumOutsideBoobL { get; private set; }

	public Part Part_CumOutsideBoobLSide { get; private set; }

	public Part Part_NipPiercingLeft { get; private set; }

	public Part Part_NipPiercingRight { get; private set; }

	public Part Part_NavelPiercing { get; private set; }

	public Part Part_MCAnusHandWrist_POV { get; private set; }

	public Part Part_MCAnusHandMain_POV { get; private set; }

	public Part Part_MCAnusHandFinger_POV { get; private set; }

	public Part Part_MCAnusHandThumb_POV { get; private set; }

	public Part Part_CumWomb1 { get; private set; }

	public Part Part_CumVagina1 { get; private set; }

	public Part Part_VaginaInner2 { get; private set; }

	public Part Part_ModdableHairFront { get; private set; }

	public Part Part_H0_FrontClippedUp_Bang { get; private set; }

	public Part Part_H0_FrontClippedUp_HairClip { get; private set; }

	public Part Part_H0_Front_Bang { get; private set; }

	public Part Part_H0_Side_BangL { get; private set; }

	public Part Part_H0_Side_BangR { get; private set; }

	public Part Part_H0_SideAlt_Strand { get; private set; }

	public Part Part_H2_Front_Bang { get; private set; }

	public Part Part_H3_Front { get; private set; }

	public Part Part_CuteBow { get; private set; }

	public Part Part_Headband { get; private set; }

	public Part Part_WizardHat { get; private set; }

	public Part Part_BikiniBot_U_Main { get; private set; }

	public Part Part_BikiniTop_M_Left { get; private set; }

	public Part Part_BikiniTop_M_Right { get; private set; }

	public Part Part_BikiniTop_A_StrapR { get; private set; }

	public Part Part_BikiniTop_A_StrapL { get; private set; }

	public Part Part_BikiniBot_U_Main2 { get; private set; }

	public Part Part_BikiniBot_U_Main3 { get; private set; }

	public Part Part_BikiniBot_U_Main4 { get; private set; }

	public Part Part_BikiniBot_U_Main5 { get; private set; }

	public Part Part_BikiniBot_U_Main6 { get; private set; }

	public Part Part_BikiniBot_U_Main7 { get; private set; }

	public Part Part_BikiniTop_A_OutlineL { get; private set; }

	public Part Part_BikiniTop_A_OutlineR { get; private set; }

	public Part Part_BikiniTop_A_StrapMiddle { get; private set; }

	private static void PrepareHandholdingInteractables(Live2DMissionaryFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.Handholding);
				c.ParamHandholdingEnableL.SetMaxValue();
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamHandholdingTightenMCL.Value += mouseMove.x * 7f;
				c.ParamTense.Value -= mouseMove.y * 3.5f;
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.Handholding);
				c.ParamHandholdingEnableL.SetMinValue();
			}
		};
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.Handholding);
				c.ParamHandholdingEnableR.SetMaxValue();
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamHandholdingTightenMCR.Value += mouseMove.x * 7f;
				c.ParamTense.Value -= mouseMove.y * 3.5f;
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.Handholding);
				c.ParamHandholdingEnableR.SetMinValue();
			}
		};
		thisController._handholdingInteractable = thisController.AddInteractable(new string[2] { "HitAreaHandholdingL", "HitAreaHandholdingR" }, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_HoldHerHand, limbUsageWithInteractable, limbUsageWithInteractable2)
		{
			StartHoldOverride = (Live2DInteractableLimbUsage<Live2DMissionaryFuckController> t, CubismDrawable d) =>
			{
				Live2DMissionaryFuckController controller = t.Controller;
				if (controller.IsElementActive(ItemSlot.SlotManager.ArmL.Id))
				{
					controller.EnsureLimbs(t.GetLimbUsages()[0]);
				}
				if (controller.IsElementActive(ItemSlot.SlotManager.ArmR.Id))
				{
					controller.EnsureLimbs(t.GetLimbUsages()[1]);
				}
			},
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsElementActive(ItemSlot.SlotManager.ArmL.Id) || c.IsElementActive(ItemSlot.SlotManager.ArmR.Id),
			LimbUsageType = LimbUsageLive2DInteractableTypeEnum.ContinueAfterLetGo
		}, CubismRaycastablePrecision.Triangles);
	}

	private static void PrepareBodyInteractable(Live2DMissionaryFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.BothArms)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.ParamEnableWaistGrab.SetMaxValue();
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamBodyX.Value += mouseMove.x * 100f;
				c.ParamPelvisY.Value += mouseMove.y * 10f;
				if (c.CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2))
				{
					o2.ThrustMovementAccumulator = (0f - mouseMove.y) * 5f;
				}
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.ParamEnableWaistGrab.SetMinValue();
			}
		};
		thisController.BodyInteractable = thisController.AddInteractableHitArea(thisController.HitAreaWaistGrab, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_MoveBody, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DMissionaryFuckController a) => a.IsHoodieAsideOrOff
		});
	}

	private static void PreparePussyInteractables(Live2DMissionaryFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.ParamFingeringMovement.ResetToDefault();
				c.ParamFingeringRotation.ResetToDefault();
				c.ParamEnableFingering.SetMaxValue();
				c.VaginaInsertionHandler.SetCurrentAxis(c.VaginaFingeringFuckAxis, GameVariables.Current.GetGameStats().OtherFrickGameStats.AnonFingeringInfo.Value);
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.VaginaInsertionHandler.ThrustMovementAccumulator += mouseMove.y * 10f;
				c.VaginaInsertionHandler.SidewaysMovementAccumulator += mouseMove.x * 5f;
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.ParamEnableFingering.SetMinValue();
				c.VaginaInsertionHandler.Reset();
			}
		};
		thisController._pussyFingeringInteractable = thisController.AddInteractableHitArea(thisController.HitAreaFingering, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_Fingering, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsPantiesAsideOrOff && !c.IsLimbActive(LimbUsageChoiceEnum.Cock)
		});
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.BothArms)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.ParamSpreadMove.ResetToDefault();
				c.ParamEnableSpread.Value = c.ParamEnableSpread.MaximumValue;
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				float value = c.ParamVaginaSpread.Value;
				c.ParamVaginaSpread.Add(mouseMove.x * 100f);
				c.ParamVaginaSpread.ClampValueSoftRepulsionFromMax(0f, 0.7f, 0.25f);
				c.ParamSpreadMove.Value = c.ParamVaginaSpread.Value;
				float value2 = c.ParamVaginaSpread.Value;
				if (value < 0.2f && value2 >= 0.2f)
				{
					c.CumLeakManager.StartLeak();
				}
				else if (value2 < 0.1f)
				{
					c.CumLeakManager.EndLeak();
				}
				if ((double)value2 >= 0.2)
				{
					c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.PussySpread);
				}
				if ((double)value2 < 0.2)
				{
					c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.PussySpread);
				}
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f);
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0005f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.PussySpread);
				c.ParamEnableSpread.Value = c.ParamEnableSpread.MinimumValue;
				c.ParamSpreadMove.ResetToDefault();
				c.CumLeakManager.EndLeak();
				c.ParamVaginaSpread.ResetToDefault();
			}
		};
		thisController._pussySpreadInteractable = thisController.AddInteractableHitArea(thisController.HitAreaSpread, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_SpreadPussy, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsPantiesAsideOrOff && !c.VaginaInsertionHandler.IsActive()
		});
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable3 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.ClitRub);
				c.ParamVaginaClitMoveX.ResetToDefault();
				c.ParamVaginaClitMoveY.ResetToDefault();
				c.ParamClitRubZ.ResetToDefault();
				c.ParamClitRubEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamVaginaClitMoveX.Value += mouseMove.x * 7f;
				c.ParamVaginaClitMoveY.Value += mouseMove.y * 7f;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude / 0.025f);
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0003f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.ParamClitRubEnable.SetMinValue();
				c.ParamVaginaClitMoveX.ResetToDefault();
				c.ParamVaginaClitMoveY.ResetToDefault();
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.ClitRub);
			}
		};
		thisController._clitRubInteractable = thisController.AddInteractableHitArea(thisController.HitAreaClitRub, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_ClitRub, limbUsageWithInteractable3)
		{
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsPantiesAsideOrOff && !c.CockManager.IsCockLimbInside()
		});
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable4 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.ParamAnusFingeringInsert.ResetToDefault();
				c.ParamAnusFingeringRotation.ResetToDefault();
				c.ParamAnusFingeringEnable.SetMaxValue();
				c.AnalInsertionHandler.SetCurrentAxis(c.AnalFingeringFuckAxis, GameVariables.Current.GetGameStats().OtherFrickGameStats.AnonFingeringInfo.Value);
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.AnalInsertionHandler.ThrustMovementAccumulator += mouseMove.y * 7f;
				c.AnalInsertionHandler.SidewaysMovementAccumulator += mouseMove.x * 4f;
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.ParamAnusFingeringEnable.SetMinValue();
				c.AnalInsertionHandler.Reset();
			}
		};
		thisController.AddInteractableHitArea(thisController.HitAreaAnusCenter, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_FingeringAnus, limbUsageWithInteractable4)
		{
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsPantiesAsideOrOff && !c.IsLimbActive(LimbUsageChoiceEnum.Cock)
		});
		int live2DLayer = Live2DToolSelector.MouthTool.Live2DLayer;
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable5 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.Mouth)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.EatingOutPussy);
				c.CurrentBrain.CausePleasure(0.04f);
				c.ParamVaginaSpread.Value = 0.2f;
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamBodyY.Value += mouseMove.y * 10f;
				c.ParamBodyX.Value += mouseMove.x * 10f;
				c.ParamVaginaSpread.Value += mouseMove.x * 3f;
				c.ParamVaginaSpread.Value = Mathf.Clamp(c.ParamVaginaSpread.Value, 0.15f, 0.25f);
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f);
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0015f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.EatingOutPussy);
				c.ParamVaginaSpread.ResetToDefault();
				c.EndKiss();
			}
		};
		thisController._pussyEatOutInteractable = thisController.AddInteractableHitArea(thisController.HitAreaFingering, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_EatHerOut, limbUsageWithInteractable5)
		{
			StartHoldOverride = (Live2DInteractableLimbUsage<Live2DMissionaryFuckController> i, CubismDrawable d) =>
			{
				i.Controller.StartKiss(d, KissStartPositionType.RendererCenter, new Vector2(0f, 0f));
				i.EnsureAllLimbs();
			},
			ActiveFunc = (Live2DMissionaryFuckController c) => !c.CockManager.IsCockVisible()
		}, CubismRaycastablePrecision.Triangles, live2DLayer);
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable6 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.Mouth)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.EatingOutAss);
				c.CurrentBrain.CausePleasure(0.04f);
				c.ParamAnalSpread.Value = 0.2f;
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamBodyX.Value += mouseMove.x * 10f;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f);
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.001f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.EatingOutAss);
				c.ParamAnalSpread.ResetToDefault();
				c.EndKiss();
			}
		};
		thisController._assEatOutInteractable = thisController.AddInteractable("AnusBack", new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_EatHerAssOut, limbUsageWithInteractable6)
		{
			StartHoldOverride = (Live2DInteractableLimbUsage<Live2DMissionaryFuckController> i, CubismDrawable d) =>
			{
				i.Controller.StartKiss(d, KissStartPositionType.RendererCenter, new Vector2(0f, 0f));
				i.EnsureAllLimbs();
			},
			ActiveFunc = (Live2DMissionaryFuckController c) => !c.CockManager.IsCockVisible()
		}, CubismRaycastablePrecision.BoundingBox, live2DLayer);
	}

	private void UpdateBoobs()
	{
		ParamFlattenNipples.Value = ((IsBraAsideOrOff && IsShirtAndHoodieAsideOrOff && !IsDress1On) ? 0f : 1f);
		ParamFlattenNipples.SetWeight(0.1f);
		if (!IsShirtAndHoodieAsideOrOff)
		{
			ParamFlattenBoobs.Value = 1f;
		}
		else if (IsDress1On || IsDress2On)
		{
			ParamFlattenBoobs.Value = (IsBraOn ? 0.5f : 0f);
		}
		else
		{
			ParamFlattenBoobs.Value = 0f;
		}
	}

	private static void PrepareBoobsInteractables(Live2DMissionaryFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobLX.StartParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.StartParameterControl(c.ParamPhysicsBoobYL);
				c.ParamEnableBoobFondleL.SetMaxValue();
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsBoobXL.Value += mouseMove.x * 7f;
				c.ParamPhysicsBoobYL.Value += mouseMove.y * 7f;
				float magnitude = new Vector2(c.ParamPhysicsBoobXL.GetRealValue(), c.ParamPhysicsBoobYL.GetRealValue()).magnitude;
				c.ParamBoobSqueezeL.Value = magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f / 2f);
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.00015f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobLX.EndParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.EndParameterControl(c.ParamPhysicsBoobYL);
				c.ParamEnableBoobFondleL.SetMinValue();
			}
		};
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobRX.StartParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.StartParameterControl(c.ParamPhysicsBoobYR);
				c.ParamEnableBoobFondleR.SetMaxValue();
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsBoobXR.Value += mouseMove.x * 7f;
				c.ParamPhysicsBoobYR.Value += mouseMove.y * 7f;
				float magnitude = new Vector2(c.ParamPhysicsBoobXR.GetRealValue(), c.ParamPhysicsBoobYR.GetRealValue()).magnitude;
				c.ParamBoobSqueezeR.Value = magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f / 2f);
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.00015f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobRX.EndParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.EndParameterControl(c.ParamPhysicsBoobYR);
				c.ParamEnableBoobFondleR.SetMinValue();
			}
		};
		thisController.BoobLFondleInteractable = thisController.AddInteractableHitArea(thisController.HitAreaBoobL, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobL, limbUsageWithInteractable));
		thisController.BoobRFondleInteractable = thisController.AddInteractableHitArea(thisController.HitAreaBoobR, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobR, limbUsageWithInteractable2));
		thisController.BoobBothFondleInteractable = thisController.AddInteractableHitArea(thisController.HitAreaBothBoobs, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobs, limbUsageWithInteractable, limbUsageWithInteractable2));
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable3 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.NipplePull);
				c.PhysicsSubRig_BoobLX.StartParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.StartParameterControl(c.ParamPhysicsBoobYL);
				c.ParamEnableNipplePinchL.SetMaxValue();
				c.ParamNippleLPinchY.Value = 0.5f;
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamNippleLPinchX.Value += mouseMove.x * 250f;
				c.ParamNippleLPinchY.Value += mouseMove.y * 250f;
				if (c.ParamNippleLPinchY.Value < 0.5f)
				{
					c.ParamNippleLPinchY.Value = 0.5f;
				}
				c.ParamNipplePinchSqueezeL.Value = new Vector2(c.ParamNippleLPinchX.GetRealValue(), c.ParamNippleLPinchY.GetRealValue()).magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f / 2f);
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.00015f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.NipplePull);
				c.ParamPhysicsBoobYL.SetRealValue(1f);
				c.ParamPhysicsBoobXL.SetRealValue(c.ParamNippleLPinchX.GetRealValue());
				c.PhysicsSubRig_BoobLX.EndParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.EndParameterControl(c.ParamPhysicsBoobYL);
				c.ParamEnableNipplePinchL.SetMinValue();
				c.ParamNippleLPinchX.ResetToDefault();
				c.ParamNippleLPinchY.ResetToDefault();
			}
		};
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable4 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.NipplePull);
				c.PhysicsSubRig_BoobRX.StartParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.StartParameterControl(c.ParamPhysicsBoobYR);
				c.ParamEnableNipplePinchR.SetMaxValue();
				c.ParamNippleRPinchY.Value = 0.5f;
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamNippleRPinchX.Value += mouseMove.x * 250f;
				c.ParamNippleRPinchY.Value += mouseMove.y * 250f;
				if (c.ParamNippleRPinchY.Value < 0.5f)
				{
					c.ParamNippleRPinchY.Value = 0.5f;
				}
				c.ParamNipplePinchSqueezeR.Value = new Vector2(c.ParamNippleRPinchX.GetRealValue(), c.ParamNippleRPinchY.GetRealValue()).magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f / 2f);
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.00015f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.NipplePull);
				c.ParamPhysicsBoobYR.SetRealValue(1f);
				c.ParamPhysicsBoobXR.SetRealValue(c.ParamNippleRPinchX.GetRealValue());
				c.PhysicsSubRig_BoobRX.EndParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.EndParameterControl(c.ParamPhysicsBoobYR);
				c.ParamEnableNipplePinchR.SetMinValue();
				c.ParamNippleRPinchX.ResetToDefault();
				c.ParamNippleRPinchY.ResetToDefault();
			}
		};
		thisController.NippleLInteractable = thisController.AddInteractableHitArea(thisController.HitAreaNippleL, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_PullLeftNipple, limbUsageWithInteractable3)
		{
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsBoobaVisible
		});
		thisController.NippleRInteractable = thisController.AddInteractableHitArea(thisController.HitAreaNippleR, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_PullRightNipple, limbUsageWithInteractable4)
		{
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsBoobaVisible
		});
		thisController.NippleBothInteractable = thisController.AddInteractableHitArea(thisController.HitAreaBothNipples, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_PullBothNipples, limbUsageWithInteractable3, limbUsageWithInteractable4)
		{
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsBoobaVisible
		});
		int live2DLayer = Live2DToolSelector.MouthTool.Live2DLayer;
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable5 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.Mouth)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.NippleSuck);
				c.PhysicsSubRig_BoobLX.StartParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.StartParameterControl(c.ParamPhysicsBoobYL);
				c.ParamNippleLPinchY.Value = 0.5f;
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamNippleLPinchX.Value += mouseMove.x * 250f;
				c.ParamNippleLPinchY.Value += mouseMove.y * 250f;
				if (c.ParamNippleLPinchY.Value < 0.5f)
				{
					c.ParamNippleLPinchY.Value = 0.5f;
				}
				c.ParamNipplePinchSqueezeL.Value = new Vector2(c.ParamNippleLPinchX.GetRealValue(), c.ParamNippleLPinchY.GetRealValue()).magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * (1f / 60f));
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0005f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.NippleSuck);
				c.ParamPhysicsBoobYL.SetRealValue(1f);
				c.ParamPhysicsBoobXL.SetRealValue(c.ParamNippleLPinchX.GetRealValue());
				c.PhysicsSubRig_BoobLX.EndParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.EndParameterControl(c.ParamPhysicsBoobYL);
				c.ParamNippleLPinchX.ResetToDefault();
				c.ParamNippleLPinchY.ResetToDefault();
				c.EndKiss();
			}
		};
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable6 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.Mouth)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.NippleSuck);
				c.PhysicsSubRig_BoobRX.StartParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.StartParameterControl(c.ParamPhysicsBoobYR);
				c.ParamNippleRPinchY.Value = 0.5f;
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamNippleRPinchX.Value += mouseMove.x * 250f;
				c.ParamNippleRPinchY.Value += mouseMove.y * 250f;
				if (c.ParamNippleRPinchY.Value < 0.5f)
				{
					c.ParamNippleRPinchY.Value = 0.5f;
				}
				c.ParamNipplePinchSqueezeR.Value = new Vector2(c.ParamNippleRPinchX.GetRealValue(), c.ParamNippleRPinchY.GetRealValue()).magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * (1f / 60f));
			},
			HoldFixedUpdateAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0005f);
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.NippleSuck);
				c.ParamPhysicsBoobYR.SetRealValue(1f);
				c.ParamPhysicsBoobXR.SetRealValue(c.ParamNippleRPinchX.GetRealValue());
				c.PhysicsSubRig_BoobRX.EndParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.EndParameterControl(c.ParamPhysicsBoobYR);
				c.ParamNippleRPinchX.ResetToDefault();
				c.ParamNippleRPinchY.ResetToDefault();
				c.EndKiss();
			}
		};
		thisController.NippleLInteractable = thisController.AddInteractableHitArea(thisController.HitAreaNippleL, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_SuckLeftNipple, limbUsageWithInteractable5)
		{
			StartHoldOverride = (Live2DInteractableLimbUsage<Live2DMissionaryFuckController> i, CubismDrawable d) =>
			{
				i.Controller.StartKiss(d, KissStartPositionType.RendererCenter);
				i.EnsureAllLimbs();
			},
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsBoobaVisible
		}, CubismRaycastablePrecision.Triangles, live2DLayer);
		thisController.NippleRInteractable = thisController.AddInteractableHitArea(thisController.HitAreaNippleR, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_SuckRightNipple, limbUsageWithInteractable6)
		{
			StartHoldOverride = (Live2DInteractableLimbUsage<Live2DMissionaryFuckController> i, CubismDrawable d) =>
			{
				i.Controller.StartKiss(d, KissStartPositionType.RendererCenter);
				i.EnsureAllLimbs();
			},
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsBoobaVisible
		}, CubismRaycastablePrecision.Triangles, live2DLayer);
	}

	private static void PrepareGrabLegsInteractables(Live2DMissionaryFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.PhysicsSubRig_UpperLegL.StartParameterControl(c.ParamLegLPhysics);
				c.ParamEnableLegLGrab.SetMaxValue();
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamLegLPhysics.Value += mouseMove.y * 100f;
				c.ParamBodyX.Value += mouseMove.x * 40f;
				c.ParamPelvisY.Value += mouseMove.y * 3f;
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.PhysicsSubRig_UpperLegL.EndParameterControl(c.ParamLegLPhysics);
				c.ParamEnableLegLGrab.SetMinValue();
			}
		};
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.PhysicsSubRig_UpperLegR.StartParameterControl(c.ParamLegRPhysics);
				c.ParamEnableLegRGrab.SetMaxValue();
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamLegRPhysics.Value += mouseMove.y * 100f;
				c.ParamBodyX.Value += mouseMove.x * 40f;
				c.ParamPelvisY.Value += mouseMove.y * 3f;
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.PhysicsSubRig_UpperLegR.EndParameterControl(c.ParamLegRPhysics);
				c.ParamEnableLegRGrab.SetMinValue();
			}
		};
		thisController.LegsGrabInteractable = thisController.AddInteractable(new string[2] { "HitAreaGrabLegL", "HitAreaGrabLegR" }, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_MoveLegs, limbUsageWithInteractable, limbUsageWithInteractable2)
		{
			StartHoldOverride = (Live2DInteractableLimbUsage<Live2DMissionaryFuckController> t, CubismDrawable d) =>
			{
				Live2DMissionaryFuckController controller = t.Controller;
				if (controller.IsElementActive(ItemSlot.SlotManager.LegL.Id))
				{
					controller.EnsureLimbs(t.GetLimbUsages()[0]);
				}
				if (controller.IsElementActive(ItemSlot.SlotManager.LegR.Id))
				{
					controller.EnsureLimbs(t.GetLimbUsages()[1]);
				}
			},
			ActiveFunc = (Live2DMissionaryFuckController c) => c.IsElementActive(ItemSlot.SlotManager.LegL.Id) || c.IsElementActive(ItemSlot.SlotManager.LegR.Id),
			LimbUsageType = LimbUsageLive2DInteractableTypeEnum.ContinueAfterLetGo
		}, CubismRaycastablePrecision.Triangles);
	}

	private static void PrepareThumbMouthInteractables(Live2DMissionaryFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DMissionaryFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DMissionaryFuckController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DMissionaryFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.ThumbMouth);
				c.ParamEnableThumbMouth.SetMaxValue();
				c.ParamThumbMouthX.SetPercent(0.5f);
			},
			HoldUpdateAction = (Live2DMissionaryFuckController c, Vector2 mouseMove) =>
			{
				c.ParamThumbMouthX.Value += mouseMove.x * 10f;
			},
			EndLimbAction = (Live2DMissionaryFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.ThumbMouth);
				c.ParamEnableThumbMouth.SetMinValue();
				c.ParamThumbMouthX.SetMinValue();
			}
		};
		thisController._thumbMouthInteractable = thisController.AddInteractableHitArea(thisController.HitAreaThumbMouth, new Live2DInteractableLimbUsage<Live2DMissionaryFuckController>(thisController, LOC.UI.TupleReferences.Interactable_ThumbInMouth, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DMissionaryFuckController c) => c.ParamMouthOpen.Value > 1f
		});
	}

	protected override void AfterUpdateTextures()
	{
		base.AfterUpdateTextures();
		UpdateBoobs();
	}

	protected override void OnStartUsageGameplayInherited(string characterId, ILive2DModelUser user)
	{
		base.OnStartUsageGameplayInherited(characterId, user);
		SwitchCameraTo(MissionarySceneCameraEnum.NormalView);
	}

	public void ToggleCamera()
	{
		switch (_currentSceneCamera)
		{
		case MissionarySceneCameraEnum.NoSetting:
		case MissionarySceneCameraEnum.NormalView:
			SwitchCameraTo(MissionarySceneCameraEnum.ExpandedView);
			break;
		case MissionarySceneCameraEnum.ExpandedView:
			SwitchCameraTo(MissionarySceneCameraEnum.NormalView);
			break;
		}
	}

	public void SwitchCameraTo(MissionarySceneCameraEnum c)
	{
		_currentSceneCamera = c;
		ViewSingleton instance = SingletonMonoBehaviour<ViewSingleton>.Instance;
		switch (c)
		{
		case MissionarySceneCameraEnum.NormalView:
			if (instance.CurrentView != instance.MissionaryView)
			{
				FungusManager.Instance.CameraManager.MoveToViewWithArguments<FrickScene, FrickSceneTransitionData>(instance.MissionaryView, 0.2f, Fungus.CameraManager.PanType.Normal, new FrickSceneTransitionData(FrickSceneInitFlags.SameScene));
			}
			break;
		case MissionarySceneCameraEnum.ExpandedView:
			if (instance.CurrentView != instance.MissionaryView2)
			{
				FungusManager.Instance.CameraManager.MoveToViewWithArguments<FrickScene, FrickSceneTransitionData>(instance.MissionaryView2, 0.2f, Fungus.CameraManager.PanType.Normal, new FrickSceneTransitionData(FrickSceneInitFlags.SameScene));
			}
			break;
		}
	}

	protected override MouthManagerDefaultMouth InitializeMouthImpl()
	{
		return MouthManagerDefaultMouth.Create(this);
	}

	public override Part GetApproximateEyesPositionPart()
	{
		return Part_EyeBall;
	}

	public override Part GetApproximateMouthPositionPart()
	{
		return Part_InnerMouth;
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
		CumLeakManager = RegisterManager(new SimpleCumLeakManager<Live2DMissionaryFuckController>(this));
		CameraManager = RegisterManager(new CameraManager<Live2DMissionaryFuckController>(this));
		List<InsertionHandler> list = new List<InsertionHandler>();
		InsertionHandler insertionHandler = new InsertionHandler(this, InsertionHandlerOrificeTypeEnum.Vagina);
		VaginaCockFuckAxis = new BasicInsertionHandleFuckAxis(-0.2f, 0.2f, ParamThrust)
		{
			CockAngleXParameter = ParamCockAngleXVaginaEntrance
		};
		insertionHandler.AddFuckAxis(VaginaCockFuckAxis);
		VaginaFingeringFuckAxis = new BasicInsertionHandleFuckAxis(-0.2f, 0.2f, ParamFingeringMovement)
		{
			CockAngleXParameter = ParamFingeringRotation,
			ThrustParameterMapping = new ParameterMappingMinMax(0f, 1f, 0f, 0.5f)
		};
		insertionHandler.AddFuckAxis(VaginaFingeringFuckAxis);
		insertionHandler.Blockages.Add(new InsertionHandlerBlock(VaginaCockFuckAxis.InsideStart, () => !IsPantiesAsideOrOff));
		insertionHandler.Blockages.Add(new InsertionHandlerBlock(VaginaCockFuckAxis.InsideStart - 0.1f, () => !IsPantsAsideOrOff));
		insertionHandler.AddVisualizationPlane(new InsertionPlaneVisualizer(0f, 8, GetParamArrayToDirectlyControl("ParamXRayVagL", 8), GetParamArrayToDirectlyControl("ParamXRayVagR", 8))
		{
			XRayStartOffset = -0.05f
		});
		insertionHandler.EntranceMultiplier = 2f;
		insertionHandler.EntranceSpreadParameter = ParamVaginaSpread;
		insertionHandler.InsertionYDeformationParameter = ParamVaginaPush;
		list.Add(insertionHandler);
		VaginaInsertionHandler = insertionHandler;
		InsertionHandler insertionHandler2 = new InsertionHandler(this, InsertionHandlerOrificeTypeEnum.Anal);
		AnalCockFuckAxis = new BasicInsertionHandleFuckAxis(0f, 0.2f, ParamThrustAnal)
		{
			CockAngleXParameter = ParamCockAngleXAnalEntrance
		};
		insertionHandler2.AddFuckAxis(AnalCockFuckAxis);
		AnalFingeringFuckAxis = new BasicInsertionHandleFuckAxis(0f, 0.05f, ParamAnusFingeringInsert)
		{
			CockAngleXParameter = ParamAnusFingeringRotation,
			ThrustParameterMapping = new ParameterMappingMinMax(0f, 1f, 0f, 0.5f)
		};
		insertionHandler2.AddFuckAxis(AnalFingeringFuckAxis);
		insertionHandler2.Blockages.Add(new InsertionHandlerBlock(-0.11f, () => !IsPantiesAsideOrOff));
		insertionHandler2.Blockages.Add(new InsertionHandlerBlock(-0.15f, () => !IsPantsAsideOrOff));
		insertionHandler2.AddVisualizationPlane(new InsertionPlaneVisualizer(0f, 8, GetParamArrayToDirectlyControl("ParamXrayAnalL", 8), GetParamArrayToDirectlyControl("ParamXrayAnalR", 8))
		{
			XRayStartOffset = -0.205f
		});
		insertionHandler2.EntranceSpreadParameter = ParamAnalSpread;
		list.Add(insertionHandler2);
		AnalInsertionHandler = insertionHandler2;
		InsertionManager = RegisterManager(new InsertionManager(list));
		_cockFuckLimbUsage = new CockLimbUsage<Live2DMissionaryFuckController>(this, ParamDick, new CockLimbUsage<Live2DMissionaryFuckController>.CockLimbUsageTarget(VaginaInsertionHandler, VaginaCockFuckAxis), new CockLimbUsage<Live2DMissionaryFuckController>.CockLimbUsageTarget(AnalInsertionHandler, AnalCockFuckAxis));
		CockManager = RegisterManager(new CockManagerCommon<Live2DMissionaryFuckController>(this, _cockFuckLimbUsage));
	}

	protected override void InitializeInherited()
	{
		AddInteractable("DickSkin", new Live2DInteractable
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
				if (_cockFuckLimbUsage.TryGetActiveData(out var insertionHandler))
				{
					insertionHandler.ThrustMovementAccumulator += mouseMove.y * 10f;
					insertionHandler.SidewaysMovementAccumulator -= mouseMove.x * 40f;
				}
			},
			OnHeldFixed = null,
			OnHoldEnd = null,
			InfoSls = LOC.UI.TupleReferences.Interactable_FuckHer,
			ActiveFunc = () => !CockManager.IsFlacid
		});
		PrepareBoobsInteractables(this);
		Live2DController.PrepareCommonHeadpatInteractables(this, HitAreaHeadpat, out var headpatInteractable);
		Live2DController.PrepareCommonMouthKissInteractables(this, out headpatInteractable, out var kissMouthInteractable);
		Live2DController.PrepareCommonMouthInteractables(this, out kissMouthInteractable, out headpatInteractable);
		Live2DController.PrepareCommonTongueInteractables(this, out var _);
		ShirtInteractable = AddInteractable(new List<string> { "Plainshirt_front", "Plainshirt_alt" }, new Live2DInteractable
		{
			OnHoldStart = (CubismDrawable d) =>
			{
			},
			OnHeld = (Vector2 mouseMove) =>
			{
				bool isShirtAside = IsShirtAside;
				ParamShirt.Value += mouseMove.y * 10f;
				if (!isShirtAside && ParamShirt.Value > 0.45f)
				{
					R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
					LetGoOfCurrentlyHeld();
					ParamShirt.Value = ParamShirt.MaximumValue;
					UpdateBoobs();
				}
				if (isShirtAside && ParamShirt.Value < 0.98f)
				{
					R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
					LetGoOfCurrentlyHeld();
					ParamShirt.Value = ParamShirt.MinimumValue;
					UpdateBoobs();
				}
			},
			OnHeldFixed = () =>
			{
			},
			OnHoldEnd = () =>
			{
			},
			InfoSls = LOC.UI.TupleReferences.Interactable_MoveShirt,
			ActiveFunc = () => IsHoodieAsideOrOff && IsShirtOn
		}, CubismRaycastablePrecision.Triangles);
		HoodieInteractable = AddInteractable(new Part[2] { HitAreaLongHoodieMoveDown, HitAreaLongHoodieMoveUp }, new Live2DInteractable
		{
			OnHoldStart = (CubismDrawable d) =>
			{
			},
			OnHeld = (Vector2 mouseMove) =>
			{
				bool isHoodieAside = IsHoodieAside;
				ParamHoodie.Value += mouseMove.y * 10f;
				if (!isHoodieAside && ParamHoodie.Value > 0.45f)
				{
					R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
					LetGoOfCurrentlyHeld();
					ParamHoodie.Value = ParamHoodie.MaximumValue;
					UpdateBoobs();
				}
				if (isHoodieAside && ParamHoodie.Value < 0.98f)
				{
					R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
					LetGoOfCurrentlyHeld();
					ParamHoodie.Value = ParamHoodie.MinimumValue;
					UpdateBoobs();
				}
			},
			OnHeldFixed = () =>
			{
			},
			OnHoldEnd = () =>
			{
			},
			InfoSls = LOC.UI.TupleReferences.Interactable_MoveHoodie,
			ActiveFunc = () => (!IsShirtAside || !IsShirtOn) && IsHoodieOn
		}, CubismRaycastablePrecision.Triangles);
		PantsInteractable = AddInteractable(new List<string> { "BPantsLowerL", "BPantsUpperL", "BPantsCrotch", "BPantsRemoved" }, new Live2DInteractable
		{
			OnHoldStart = (CubismDrawable d) =>
			{
			},
			OnHeld = (Vector2 mouseMove) =>
			{
				if (!Mathf.Approximately(mouseMove.sqrMagnitude, 0f))
				{
					if (IsPantsAside)
					{
						if (mouseMove.x > 0f)
						{
							R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
							ParamPantsAside.SetMinValue();
							LetGoOfCurrentlyHeld();
						}
					}
					else if (mouseMove.x < 0f)
					{
						R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
						ParamPantsAside.SetMaxValue();
						LetGoOfCurrentlyHeld();
					}
				}
			},
			OnHeldFixed = () =>
			{
			},
			OnHoldEnd = () =>
			{
			},
			InfoSls = LOC.UI.TupleReferences.Interactable_RemovePutOnPants
		}, CubismRaycastablePrecision.Triangles);
		PantiesInteractable = AddInteractableHitArea(HitArea_Panties, new Live2DInteractable
		{
			OnHoldStart = (CubismDrawable d) =>
			{
			},
			OnHeld = (Vector2 mouseMove) =>
			{
				ParamPantiesY.Value += mouseMove.y * 10f;
				ParamPantiesX.Value += mouseMove.x * 30f;
				ParamPantiesY.ClampValueSoftRepulsionFromMax(-1f, 1f, 0.4f);
			},
			OnHeldFixed = () =>
			{
			},
			OnHoldEnd = () =>
			{
				if (!IsPantiesAside)
				{
					ParamPantiesX.ResetToDefault();
				}
				else if (ParamPantiesX.Value > 0f)
				{
					ParamPantiesX.Value = ParamPantiesX.MaximumValue;
				}
				else
				{
					ParamPantiesX.Value = ParamPantiesX.MinimumValue;
				}
				ParamPantiesY.ResetToDefault();
			},
			InfoSls = LOC.UI.TupleReferences.Interactable_MovePullPanties,
			ActiveFunc = () => !CockManager.IsCockLimbInside() && IsPantiesOn && IsPantsAsideOrOff
		});
		PrepareThumbMouthInteractables(this);
		PrepareHandholdingInteractables(this);
		PreparePussyInteractables(this);
		PrepareGrabLegsInteractables(this);
		PrepareBodyInteractable(this);
	}

	public override void InitializeGenerated()
	{
		ParamEyeballLX = GetParamToDirectlyControl("ParamEyeballLX");
		ParamEyeballLY = GetParamToDirectlyControl("ParamEyeballLY");
		ParamEyeballRX = GetParamToDirectlyControl("ParamEyeballRX");
		ParamEyeballRY = GetParamToDirectlyControl("ParamEyeballRY");
		ParamHeart = GetParamToDirectlyControl("ParamHeart");
		ParamIrisZoom = GetParamToDirectlyControl("ParamIrisZoom");
		ParamEyesHappy = GetParamToDirectlyControl("ParamEyesHappy");
		ParamEyeOpen = GetParamToDirectlyControl("ParamEyeOpen");
		ParamBrowREmote = GetParamToDirectlyControl("ParamBrowREmote");
		ParamBrowLEmote = GetParamToDirectlyControl("ParamBrowLEmote");
		ParamBrowRRot = GetParamToDirectlyControl("ParamBrowRRot");
		ParamBrowLRot = GetParamToDirectlyControl("ParamBrowLRot");
		ParamEyeLShock = GetParamToDirectlyControl("ParamEyeLShock");
		ParamEyeLShock.SetWeight(0.1f);
		ParamEyeRShock = GetParamToDirectlyControl("ParamEyeRShock");
		ParamEyeRShock.SetWeight(0.1f);
		ParamBrowLY = GetParamToDirectlyControl("ParamBrowLY");
		ParamBrowRY = GetParamToDirectlyControl("ParamBrowRY");
		ParamToungeY = GetParamToDirectlyControl("ParamToungeY");
		ParamToungeX = GetParamToDirectlyControl("ParamToungeX");
		ParamMouthForm = GetParamToDirectlyControl("ParamMouthForm");
		ParamSaliva = GetParamToDirectlyControl("ParamSaliva");
		ParamMouthOpen = GetParamToDirectlyControl("ParamMouthOpen");
		ParamBlush = GetParamToDirectlyControl("ParamBlush");
		ParamHeadX = GetParamToDirectlyControl("ParamHeadX");
		ParamHeadX.SetWeight(10f);
		ParamHeadZ = GetParamToDirectlyControl("ParamHeadZ");
		ParamHeadZ.SetWeight(10f);
		ParamHeadY = GetParamToDirectlyControl("ParamHeadY");
		ParamHeadY.SetWeight(10f);
		ParamEarsFlick = GetParamToDirectlyControl("ParamEarsFlick");
		ParamEarR = GetParamToDirectlyControl("ParamEarR");
		ParamEarL = GetParamToDirectlyControl("ParamEarL");
		ParamHeadpatX = GetParamToDirectlyControl("ParamHeadpatX");
		ParamHeadpatX.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		ParamHeadpatY = GetParamToDirectlyControl("ParamHeadpatY");
		ParamHeadpatY.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		ParamHeadpat = GetParamToDirectlyControl("ParamHeadpat");
		ParamHairX = GetParamToDirectlyControl("ParamHairX");
		ParamSalivaPhysics = GetParamToDirectlyControl("ParamSalivaPhysics");
		ParamPupilWiggle = GetParamToDirectlyControl("ParamPupilWiggle");
		ParamEarsWiggle = GetParamToDirectlyControl("ParamEarsWiggle");
		ParamSocksEnabled = GetParamToDirectlyControl("ParamSocksEnabled");
		ParamPantsAside = GetParamToDirectlyControl("ParamPantsAside");
		ParamHoodie = GetParamToDirectlyControl("ParamHoodie");
		ParamShirt = GetParamToDirectlyControl("ParamShirt");
		ParamPantiesX = GetParamToDirectlyControl("ParamPantiesX");
		ParamPantiesY = GetParamToDirectlyControl("ParamPantiesY");
		ParamFlattenBoobs = GetParamToDirectlyControl("ParamFlattenBoobs");
		ParamFlattenNipples = GetParamToDirectlyControl("ParamFlattenNipples");
		ParamPelvisX = GetParamToDirectlyControl("ParamPelvisX");
		ParamPelvisY = GetParamToDirectlyControl("ParamPelvisY");
		ParamHands = GetParamToDirectlyControl("ParamHands");
		ParamVaginaAroused = GetParamToDirectlyControl("ParamVaginaAroused");
		ParamTense = GetParamToDirectlyControl("ParamTense");
		ParamOrgasm = GetParamToDirectlyControl("ParamOrgasm");
		ParamLegsQuiver = GetParamToDirectlyControl("ParamLegsQuiver");
		ParamOmegaMeter = GetParamToDirectlyControl("ParamOmegaMeter");
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
		ParamCumLeakAnim = GetParamToDirectlyControl("ParamCumLeakAnim");
		ParamCumInside = GetParamToDirectlyControl("ParamCumInside");
		ParamCumInsideWomb = GetParamToDirectlyControl("ParamCumInsideWomb");
		ParamVaginaSpread = GetParamToDirectlyControl("ParamVaginaSpread");
		ParamVaginaClitMoveX = GetParamToDirectlyControl("ParamVaginaClitMoveX");
		ParamVaginaClitMoveY = GetParamToDirectlyControl("ParamVaginaClitMoveY");
		ParamCockAngleXVaginaEntrance = GetParamToDirectlyControl("ParamCockAngleXVaginaEntrance");
		ParamXrayAnalR0 = GetParamToDirectlyControl("ParamXrayAnalR0");
		ParamXrayAnalR1 = GetParamToDirectlyControl("ParamXrayAnalR1");
		ParamXrayAnalR2 = GetParamToDirectlyControl("ParamXrayAnalR2");
		ParamXrayAnalR3 = GetParamToDirectlyControl("ParamXrayAnalR3");
		ParamXrayAnalR4 = GetParamToDirectlyControl("ParamXrayAnalR4");
		ParamXrayAnalR5 = GetParamToDirectlyControl("ParamXrayAnalR5");
		ParamXrayAnalR6 = GetParamToDirectlyControl("ParamXrayAnalR6");
		ParamXrayAnalR7 = GetParamToDirectlyControl("ParamXrayAnalR7");
		ParamXrayAnalL0 = GetParamToDirectlyControl("ParamXrayAnalL0");
		ParamXrayAnalL1 = GetParamToDirectlyControl("ParamXrayAnalL1");
		ParamXrayAnalL2 = GetParamToDirectlyControl("ParamXrayAnalL2");
		ParamXrayAnalL3 = GetParamToDirectlyControl("ParamXrayAnalL3");
		ParamXrayAnalL4 = GetParamToDirectlyControl("ParamXrayAnalL4");
		ParamXrayAnalL5 = GetParamToDirectlyControl("ParamXrayAnalL5");
		ParamXrayAnalL6 = GetParamToDirectlyControl("ParamXrayAnalL6");
		ParamXrayAnalL7 = GetParamToDirectlyControl("ParamXrayAnalL7");
		ParamAnalSpread = GetParamToDirectlyControl("ParamAnalSpread");
		ParamCumInsideAnal = GetParamToDirectlyControl("ParamCumInsideAnal");
		ParamCockAngleXAnalEntrance = GetParamToDirectlyControl("ParamCockAngleXAnalEntrance");
		ParamDick = GetParamToDirectlyControl("ParamDick");
		ParamXray = GetParamToDirectlyControl("ParamXray");
		ParamThrust = GetParamToDirectlyControl("ParamThrust");
		ParamThrustAnal = GetParamToDirectlyControl("ParamThrustAnal");
		ParamCumShotAnim = GetParamToDirectlyControl("ParamCumShotAnim");
		ParamFlacidity = GetParamToDirectlyControl("ParamFlacidity");
		ParamCumOutsideX = GetParamToDirectlyControl("ParamCumOutsideX");
		ParamCumOutsideX.SetWeight(10f);
		ParamCumShotLengthY = GetParamToDirectlyControl("ParamCumShotLengthY");
		ParamHideCumShot = GetParamToDirectlyControl("ParamHideCumShot");
		ParamAnusFingeringEnable = GetParamToDirectlyControl("ParamAnusFingeringEnable");
		ParamAnusFingeringInsert = GetParamToDirectlyControl("ParamAnusFingeringInsert");
		ParamAnusFingeringRotation = GetParamToDirectlyControl("ParamAnusFingeringRotation");
		ParamEnableLegLGrab = GetParamToDirectlyControl("ParamEnableLegLGrab");
		ParamEnableLegRGrab = GetParamToDirectlyControl("ParamEnableLegRGrab");
		ParamEnableWaistGrab = GetParamToDirectlyControl("ParamEnableWaistGrab");
		ParamEnableFingering = GetParamToDirectlyControl("ParamEnableFingering");
		ParamFingeringMovement = GetParamToDirectlyControl("ParamFingeringMovement");
		ParamFingeringRotation = GetParamToDirectlyControl("ParamFingeringRotation");
		ParamEnableSpread = GetParamToDirectlyControl("ParamEnableSpread");
		ParamSpreadMove = GetParamToDirectlyControl("ParamSpreadMove");
		ParamEnableThumbMouth = GetParamToDirectlyControl("ParamEnableThumbMouth");
		ParamThumbMouthX = GetParamToDirectlyControl("ParamThumbMouthX");
		ParamHandholdingEnableL = GetParamToDirectlyControl("ParamHandholdingEnableL");
		ParamHandholdingEnableR = GetParamToDirectlyControl("ParamHandholdingEnableR");
		ParamHandholdingTightenMCL = GetParamToDirectlyControl("ParamHandholdingTightenMCL");
		ParamHandholdingTightenMCR = GetParamToDirectlyControl("ParamHandholdingTightenMCR");
		ParamClitRubEnable = GetParamToDirectlyControl("ParamClitRubEnable");
		ParamClitRubZ = GetParamToDirectlyControl("ParamClitRubZ");
		ParamEnableNipplePinchR = GetParamToDirectlyControl("ParamEnableNipplePinchR");
		ParamNippleRPinchX = GetParamToDirectlyControl("ParamNippleRPinchX");
		ParamNippleRPinchY = GetParamToDirectlyControl("ParamNippleRPinchY");
		ParamNipplePinchSqueezeR = GetParamToDirectlyControl("ParamNipplePinchSqueezeR");
		ParamNipplePinchRotationR = GetParamToDirectlyControl("ParamNipplePinchRotationR");
		ParamEnableNipplePinchL = GetParamToDirectlyControl("ParamEnableNipplePinchL");
		ParamNippleLPinchX = GetParamToDirectlyControl("ParamNippleLPinchX");
		ParamNippleLPinchY = GetParamToDirectlyControl("ParamNippleLPinchY");
		ParamNipplePinchSqueezeL = GetParamToDirectlyControl("ParamNipplePinchSqueezeL");
		ParamNipplePinchRotationL = GetParamToDirectlyControl("ParamNipplePinchRotationL");
		ParamEnableBoobFondleL = GetParamToDirectlyControl("ParamEnableBoobFondleL");
		ParamBoobSqueezeL = GetParamToDirectlyControl("ParamBoobSqueezeL");
		ParamEnableBoobFondleR = GetParamToDirectlyControl("ParamEnableBoobFondleR");
		ParamBoobSqueezeR = GetParamToDirectlyControl("ParamBoobSqueezeR");
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
		ParamHairPhysicsBaseToShort = GetParamToDirectlyControl("ParamHairPhysicsBaseToShort");
		ParamHairPhysicsShortToMid = GetParamToDirectlyControl("ParamHairPhysicsShortToMid");
		ParamHairPhysicsMidToLong = GetParamToDirectlyControl("ParamHairPhysicsMidToLong");
		ParamPhysicsBoobXL = GetParamToDirectlyControl("ParamPhysicsBoobXL");
		ParamPhysicsBoobYL = GetParamToDirectlyControl("ParamPhysicsBoobYL");
		ParamPhysicsBoobXR = GetParamToDirectlyControl("ParamPhysicsBoobXR");
		ParamPhysicsBoobYR = GetParamToDirectlyControl("ParamPhysicsBoobYR");
		ParamBoobRJiggle = GetParamToDirectlyControl("ParamBoobRJiggle");
		ParamBoobLJiggle = GetParamToDirectlyControl("ParamBoobLJiggle");
		ParamLegRPhysics = GetParamToDirectlyControl("ParamLegRPhysics");
		ParamLegLPhysics = GetParamToDirectlyControl("ParamLegLPhysics");
		ParamPhysicsLowerLegL = GetParamToDirectlyControl("ParamPhysicsLowerLegL");
		ParamPhysicsLowerLegR = GetParamToDirectlyControl("ParamPhysicsLowerLegR");
		ParamPhysicsFootL = GetParamToDirectlyControl("ParamPhysicsFootL");
		ParamPhysicsFootR = GetParamToDirectlyControl("ParamPhysicsFootR");
		ParamPhysicsCumWomb = GetParamToDirectlyControl("ParamPhysicsCumWomb");
		ParamAssSize = GetParamToDirectlyControl("ParamAssSize");
		ParamBoobSize = GetParamToDirectlyControl("ParamBoobSize");
		ParamThighSize = GetParamToDirectlyControl("ParamThighSize");
		ParamCalfSize = GetParamToDirectlyControl("ParamCalfSize");
		ParamFootWidth = GetParamToDirectlyControl("ParamFootWidth");
		ParamFootLength = GetParamToDirectlyControl("ParamFootLength");
		SymParam_LR_ParamEyeballX = new Live2DParameter[2] { ParamEyeballLX, ParamEyeballRX };
		SymParam_LR_ParamEyeballY = new Live2DParameter[2] { ParamEyeballLY, ParamEyeballRY };
		SymParam_LR_ParamBrowEmote = new Live2DParameter[2] { ParamBrowLEmote, ParamBrowREmote };
		SymParam_LR_ParamBrowRot = new Live2DParameter[2] { ParamBrowLRot, ParamBrowRRot };
		SymParam_LR_ParamEyeShock = new Live2DParameter[2] { ParamEyeLShock, ParamEyeRShock };
		SymParam_LR_ParamBrowY = new Live2DParameter[2] { ParamBrowLY, ParamBrowRY };
		SymParam_LR_ParamEar = new Live2DParameter[2] { ParamEarL, ParamEarR };
		SymParam_LR_ParamXRayVag0 = new Live2DParameter[2] { ParamXRayVagL0, ParamXRayVagR0 };
		SymParam_LR_ParamXRayVag1 = new Live2DParameter[2] { ParamXRayVagL1, ParamXRayVagR1 };
		SymParam_LR_ParamXRayVag2 = new Live2DParameter[2] { ParamXRayVagL2, ParamXRayVagR2 };
		SymParam_LR_ParamXRayVag3 = new Live2DParameter[2] { ParamXRayVagL3, ParamXRayVagR3 };
		SymParam_LR_ParamXRayVag4 = new Live2DParameter[2] { ParamXRayVagL4, ParamXRayVagR4 };
		SymParam_LR_ParamXRayVag5 = new Live2DParameter[2] { ParamXRayVagL5, ParamXRayVagR5 };
		SymParam_LR_ParamXRayVag6 = new Live2DParameter[2] { ParamXRayVagL6, ParamXRayVagR6 };
		SymParam_LR_ParamXRayVag7 = new Live2DParameter[2] { ParamXRayVagL7, ParamXRayVagR7 };
		SymParam_LR_ParamXrayAnal0 = new Live2DParameter[2] { ParamXrayAnalL0, ParamXrayAnalR0 };
		SymParam_LR_ParamXrayAnal1 = new Live2DParameter[2] { ParamXrayAnalL1, ParamXrayAnalR1 };
		SymParam_LR_ParamXrayAnal2 = new Live2DParameter[2] { ParamXrayAnalL2, ParamXrayAnalR2 };
		SymParam_LR_ParamXrayAnal3 = new Live2DParameter[2] { ParamXrayAnalL3, ParamXrayAnalR3 };
		SymParam_LR_ParamXrayAnal4 = new Live2DParameter[2] { ParamXrayAnalL4, ParamXrayAnalR4 };
		SymParam_LR_ParamXrayAnal5 = new Live2DParameter[2] { ParamXrayAnalL5, ParamXrayAnalR5 };
		SymParam_LR_ParamXrayAnal6 = new Live2DParameter[2] { ParamXrayAnalL6, ParamXrayAnalR6 };
		SymParam_LR_ParamXrayAnal7 = new Live2DParameter[2] { ParamXrayAnalL7, ParamXrayAnalR7 };
		SymParam_LR_ParamEnableLegGrab = new Live2DParameter[2] { ParamEnableLegLGrab, ParamEnableLegRGrab };
		SymParam_LR_ParamHandholdingEnable = new Live2DParameter[2] { ParamHandholdingEnableL, ParamHandholdingEnableR };
		SymParam_LR_ParamHandholdingTightenMC = new Live2DParameter[2] { ParamHandholdingTightenMCL, ParamHandholdingTightenMCR };
		SymParam_LR_ParamEnableNipplePinch = new Live2DParameter[2] { ParamEnableNipplePinchL, ParamEnableNipplePinchR };
		SymParam_LR_ParamNipplePinchX = new Live2DParameter[2] { ParamNippleLPinchX, ParamNippleRPinchX };
		SymParam_LR_ParamNipplePinchY = new Live2DParameter[2] { ParamNippleLPinchY, ParamNippleRPinchY };
		SymParam_LR_ParamNipplePinchSqueeze = new Live2DParameter[2] { ParamNipplePinchSqueezeL, ParamNipplePinchSqueezeR };
		SymParam_LR_ParamNipplePinchRotation = new Live2DParameter[2] { ParamNipplePinchRotationL, ParamNipplePinchRotationR };
		SymParam_LR_ParamEnableBoobFondle = new Live2DParameter[2] { ParamEnableBoobFondleL, ParamEnableBoobFondleR };
		SymParam_LR_ParamBoobSqueeze = new Live2DParameter[2] { ParamBoobSqueezeL, ParamBoobSqueezeR };
		SymParam_LR_ParamPhysicsBoobX = new Live2DParameter[2] { ParamPhysicsBoobXL, ParamPhysicsBoobXR };
		SymParam_LR_ParamPhysicsBoobY = new Live2DParameter[2] { ParamPhysicsBoobYL, ParamPhysicsBoobYR };
		SymParam_LR_ParamBoobJiggle = new Live2DParameter[2] { ParamBoobLJiggle, ParamBoobRJiggle };
		SymParam_LR_ParamLegPhysics = new Live2DParameter[2] { ParamLegLPhysics, ParamLegRPhysics };
		SymParam_LR_ParamPhysicsLowerLeg = new Live2DParameter[2] { ParamPhysicsLowerLegL, ParamPhysicsLowerLegR };
		SymParam_LR_ParamPhysicsFoot = new Live2DParameter[2] { ParamPhysicsFootL, ParamPhysicsFootR };
		CubismPhysicsController component = Model.GetComponent<CubismPhysicsController>();
		PhysicsSubRig_Tail = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Tail"));
		PhysicsSubRig_UpperLegR = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "UpperLegR"));
		PhysicsSubRig_UpperLegL = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "UpperLegL"));
		PhysicsSubRig_LowerLegR = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "LowerLegR"));
		PhysicsSubRig_LowerLegL = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "LowerLegL"));
		PhysicsSubRig_BoobLY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobLY"));
		PhysicsSubRig_BoobRY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobRY"));
		PhysicsSubRig_BoobLX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobLX"));
		PhysicsSubRig_BoobLJiggle = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobLJiggle"));
		PhysicsSubRig_BoobRX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobRX"));
		PhysicsSubRig_BoobRJiggle = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobRJiggle"));
		PhysicsSubRig_Hair = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Hair"));
		PhysicsSubRig_Saliva = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Saliva"));
		HitAreaSpread = GetCachedPart("HitAreaSpread");
		HitAreaBoobR = GetCachedPart("HitAreaBoobR");
		HitAreaNippleL = GetCachedPart("HitAreaNippleL");
		HitAreaNippleR = GetCachedPart("HitAreaNippleR");
		HitAreaBoobL = GetCachedPart("HitAreaBoobL");
		HitAreaBothNipples = GetCachedPart("HitAreaBothNipples");
		HitAreaBothBoobs = GetCachedPart("HitAreaBothBoobs");
		HitAreaGrabLegL = GetCachedPart("HitAreaGrabLegL");
		HitAreaGrabLegR = GetCachedPart("HitAreaGrabLegR");
		HitAreaWaistGrab = GetCachedPart("HitAreaWaistGrab");
		HitAreaOpenMouth = GetCachedPart("HitAreaOpenMouth");
		HitAreaCloseMouth = GetCachedPart("HitAreaCloseMouth");
		HitAreaFaceStroke = GetCachedPart("HitAreaFaceStroke");
		HitAreaThumbMouth = GetCachedPart("HitAreaThumbMouth");
		HitAreaFingering = GetCachedPart("HitAreaFingering");
		HitAreaClitRub = GetCachedPart("HitAreaClitRub");
		HitAreaHandholdingL = GetCachedPart("HitAreaHandholdingL");
		HitAreaHandholdingR = GetCachedPart("HitAreaHandholdingR");
		HitAreaHeadpat = GetCachedPart("HitAreaHeadpat");
		HitAreaLongHoodieMoveDown = GetCachedPart("HitAreaLongHoodieMoveDown");
		HitAreaLongHoodieMoveUp = GetCachedPart("HitAreaLongHoodieMoveUp");
		HitAreaAnusCenter = GetCachedPart("HitAreaAnusCenter");
		HitArea_Panties = GetCachedPart("HitArea_Panties");
		Part_GlowOmegaMeterMask = GetCachedPart("GlowOmegaMeterMask");
		Part_Plainshirt_front = GetCachedPart("Plainshirt_front");
		Part_ShadowPlainshirt_shadow = GetCachedPart("ShadowPlainshirt_shadow");
		Part_Plainshirt_lowerback = GetCachedPart("Plainshirt_lowerback");
		Part_Plainshirt_main_alt = GetCachedPart("Plainshirt_main_alt");
		Part_Plainshirt_main_top_clone = GetCachedPart("Plainshirt_main_top_clone");
		Part_Plainshirt_sleeveL_lower = GetCachedPart("Plainshirt_sleeveL_lower");
		Part_Plainshirt_sleeveL_Shoulder = GetCachedPart("Plainshirt_sleeveL_Shoulder");
		Part_Plainshirt_sleeveL_back = GetCachedPart("Plainshirt_sleeveL_back");
		Part_Plainshirt_sleeveR_lower = GetCachedPart("Plainshirt_sleeveR_lower");
		Part_Plainshirt_sleeveR_Shoulder = GetCachedPart("Plainshirt_sleeveR_Shoulder");
		Part_Plainshirt_sleeveR_back = GetCachedPart("Plainshirt_sleeveR_back");
		Part_Plainshirt_alt = GetCachedPart("Plainshirt_alt");
		Part_ShadowPlainshirt_alt_shadow = GetCachedPart("ShadowPlainshirt_alt_shadow");
		Part_Plainshirt_alt_back = GetCachedPart("Plainshirt_alt_back");
		Part_Plainshirt_chest2 = GetCachedPart("Plainshirt_chest2");
		Part_Panties_upper = GetCachedPart("Panties_upper");
		Part_Panties_Above = GetCachedPart("Panties_Above");
		Part_Panties_Bottom = GetCachedPart("Panties_Bottom");
		Part_StockingR_foot = GetCachedPart("StockingR_foot");
		Part_StockingL_Lower = GetCachedPart("StockingL_Lower");
		Part_StockingL_foot = GetCachedPart("StockingL_foot");
		Part_StockingL_thigh = GetCachedPart("StockingL_thigh");
		Part_StockingR_lower = GetCachedPart("StockingR_lower");
		Part_StockingR_thigh = GetCachedPart("StockingR_thigh");
		Part_BraChest = GetCachedPart("BraChest");
		Part_BraTopL = GetCachedPart("BraTopL");
		Part_BraTopR = GetCachedPart("BraTopR");
		Part_SkirtUpper = GetCachedPart("SkirtUpper");
		Part_SkirtBack = GetCachedPart("SkirtBack");
		Part_ModdableBack = GetCachedPart("ModdableBack");
		Part_BPantsLowerR = GetCachedPart("BPantsLowerR");
		Part_BPantsLowerL = GetCachedPart("BPantsLowerL");
		Part_BPantsUpperR = GetCachedPart("BPantsUpperR");
		Part_BPantsUpperL = GetCachedPart("BPantsUpperL");
		Part_AltBPantsBelt = GetCachedPart("AltBPantsBelt");
		Part_BPantsCrotch = GetCachedPart("BPantsCrotch");
		Part_AltLongHoodieStringL = GetCachedPart("AltLongHoodieStringL");
		Part_AltLongHoodieStringR = GetCachedPart("AltLongHoodieStringR");
		Part_LongHoodieHoodFront = GetCachedPart("LongHoodieHoodFront");
		Part_LongHoodieHoodBack = GetCachedPart("LongHoodieHoodBack");
		Part_LongHoodieBottomFront = GetCachedPart("LongHoodieBottomFront");
		Part_LongHoodieChest = GetCachedPart("LongHoodieChest");
		Part_LongHoodieFront = GetCachedPart("LongHoodieFront");
		Part_LongHoodieSleeve2L = GetCachedPart("LongHoodieSleeve2L");
		Part_LongHoodieSleeve2R = GetCachedPart("LongHoodieSleeve2R");
		Part_LongHoodieSleeveL = GetCachedPart("LongHoodieSleeveL");
		Part_LongHoodieSleeveR = GetCachedPart("LongHoodieSleeveR");
		Part_LongHoodieBack = GetCachedPart("LongHoodieBack");
		Part_BPantsRemoved = GetCachedPart("BPantsRemoved");
		Part_LongHoodieUpFront = GetCachedPart("LongHoodieUpFront");
		Part_LongHoodieUpBack = GetCachedPart("LongHoodieUpBack");
		Part_CumOutsideUnderBoobMiddle = GetCachedPart("CumOutsideUnderBoobMiddle");
		Part_CumOutsideBoobR = GetCachedPart("CumOutsideBoobR");
		Part_CumOutsideInnerThighR = GetCachedPart("CumOutsideInnerThighR");
		Part_CumOutsideInnerThighL = GetCachedPart("CumOutsideInnerThighL");
		Part_CumOutsideBelly = GetCachedPart("CumOutsideBelly");
		Part_CumWombLv1 = GetCachedPart("CumWombLv1");
		Part_CumWombLv2 = GetCachedPart("CumWombLv2");
		Part_CumWombLv3 = GetCachedPart("CumWombLv3");
		Part_CumWombLv4 = GetCachedPart("CumWombLv4");
		Part_CumVaginaLeak2 = GetCachedPart("CumVaginaLeak2");
		Part_CumVaginaLeak1 = GetCachedPart("CumVaginaLeak1");
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
		Part_SkinNeck = GetCachedPart("SkinNeck");
		Part_ModdableHairBack = GetCachedPart("ModdableHairBack");
		Part_ModdableFace = GetCachedPart("ModdableFace");
		Part_SkinVaginaPenetratedFront = GetCachedPart("SkinVaginaPenetratedFront");
		Part_SkinVaginaPenetratedBack = GetCachedPart("SkinVaginaPenetratedBack");
		Part_SkinVaginaL = GetCachedPart("SkinVaginaL");
		Part_SkinVaginaR = GetCachedPart("SkinVaginaR");
		Part_SkinLegRFront = GetCachedPart("SkinLegRFront");
		Part_SkinLegRBack = GetCachedPart("SkinLegRBack");
		Part_SkinLegLFront = GetCachedPart("SkinLegLFront");
		Part_SkinLegLBack = GetCachedPart("SkinLegLBack");
		Part_SkinPelvis = GetCachedPart("SkinPelvis");
		Part_SkinButtL = GetCachedPart("SkinButtL");
		Part_SkinButtR = GetCachedPart("SkinButtR");
		Part_SkinBoobL = GetCachedPart("SkinBoobL");
		Part_SkinBoobR = GetCachedPart("SkinBoobR");
		Part_SkinRibcageFront = GetCachedPart("SkinRibcageFront");
		Part_SkinArmR = GetCachedPart("SkinArmR");
		Part_SkinArmL = GetCachedPart("SkinArmL");
		Part_SkinShoulderL = GetCachedPart("SkinShoulderL");
		Part_SkinShoulderR = GetCachedPart("SkinShoulderR");
		Part_SkinRibcageBack = GetCachedPart("SkinRibcageBack");
		Part_SkinTorsoLower = GetCachedPart("SkinTorsoLower");
		Part_SkinTorsoUpper = GetCachedPart("SkinTorsoUpper");
		Part_DickSkin = GetCachedPart("DickSkin");
		Part_DickMain = GetCachedPart("DickMain");
		Part_CumDick1 = GetCachedPart("CumDick1");
		Part_CumDick2 = GetCachedPart("CumDick2");
		Part_Xrayouter = GetCachedPart("Xrayouter");
		Part_XrayWomb = GetCachedPart("XrayWomb");
		Part_XrayBase = GetCachedPart("XrayBase");
		Part_XrayAnalOuter = GetCachedPart("XrayAnalOuter");
		Part_XrayAnalInnerR = GetCachedPart("XrayAnalInnerR");
		Part_XrayAnalInnerL = GetCachedPart("XrayAnalInnerL");
		Part_XrayAnalBase = GetCachedPart("XrayAnalBase");
		Part_VaginaInner = GetCachedPart("VaginaInner");
		Part_VaginaBase = GetCachedPart("VaginaBase");
		Part_AnusFront = GetCachedPart("AnusFront");
		Part_AnusBack = GetCachedPart("AnusBack");
		Part_InsideLegR = GetCachedPart("InsideLegR");
		Part_InsideLegL = GetCachedPart("InsideLegL");
		Part_NippleL = GetCachedPart("NippleL");
		Part_HighlightBoobL = GetCachedPart("HighlightBoobL");
		Part_NippleR = GetCachedPart("NippleR");
		Part_HightlightBoobR = GetCachedPart("HightlightBoobR");
		Part_Barcode = GetCachedPart("Barcode");
		Part_InsdeArmR = GetCachedPart("InsdeArmR");
		Part_InsdeArmL = GetCachedPart("InsdeArmL");
		Part_GlowOmegaMeterInner = GetCachedPart("GlowOmegaMeterInner");
		Part_GlowOmegaMeter = GetCachedPart("GlowOmegaMeter");
		Part_ModdableShirtLogo = GetCachedPart("ModdableShirtLogo");
		Part_ModdableShirtLeftSleeveLogo = GetCachedPart("ModdableShirtLeftSleeveLogo");
		Part_ModdableShirtRightSleeveLogo = GetCachedPart("ModdableShirtRightSleeveLogo");
		Part_ModdablePantiesLogo = GetCachedPart("ModdablePantiesLogo");
		Part_ModdableHoodieLogo = GetCachedPart("ModdableHoodieLogo");
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
		Part_CumOutsideBellyR = GetCachedPart("CumOutsideBellyR");
		Part_CumOutsideBoobRSide = GetCachedPart("CumOutsideBoobRSide");
		Part_CumOutsideFaceRight = GetCachedPart("CumOutsideFaceRight");
		Part_CumOutsideFaceNose = GetCachedPart("CumOutsideFaceNose");
		Part_CumOutsideFaceLeft = GetCachedPart("CumOutsideFaceLeft");
		Part_PointyEarL = GetCachedPart("PointyEarL");
		Part_PointyEarR = GetCachedPart("PointyEarR");
		Part_BellChokerMain = GetCachedPart("BellChokerMain");
		Part_BellChokerBell = GetCachedPart("BellChokerBell");
		Part_XrayInnerL = GetCachedPart("XrayInnerL");
		Part_XrayInnerR2 = GetCachedPart("XrayInnerR2");
		Part_Plainshirt_main_bottom = GetCachedPart("Plainshirt_main_bottom");
		Part_Dress2RolledUpFront = GetCachedPart("Dress2RolledUpFront");
		Part_Dress2BottomBack = GetCachedPart("Dress2BottomBack");
		Part_Dress2Chest = GetCachedPart("Dress2Chest");
		Part_Dress2AltWaist = GetCachedPart("Dress2AltWaist");
		Part_Dress2AltRib = GetCachedPart("Dress2AltRib");
		Part_Dress2SleeveL = GetCachedPart("Dress2SleeveL");
		Part_Dress2SleeveR = GetCachedPart("Dress2SleeveR");
		Part_DickMask = GetCachedPart("DickMask");
		Part_ThumbMask = GetCachedPart("ThumbMask");
		Part_ThumbSuckThumb = GetCachedPart("ThumbSuckThumb");
		Part_ThumbSuck = GetCachedPart("ThumbSuck");
		Part_SkinMouthThumbSuck = GetCachedPart("SkinMouthThumbSuck");
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
		Part_MCRightHandVaginaSpread = GetCachedPart("MCRightHandVaginaSpread");
		Part_MCRightHandThumbVaginaSpread = GetCachedPart("MCRightHandThumbVaginaSpread");
		Part_MCLeftHandVaginaSpread = GetCachedPart("MCLeftHandVaginaSpread");
		Part_MCLeftHandThumbVaginaSpread = GetCachedPart("MCLeftHandThumbVaginaSpread");
		Part_MCLeftHandLegGrab = GetCachedPart("MCLeftHandLegGrab");
		Part_MCRightHandLegGrab = GetCachedPart("MCRightHandLegGrab");
		Part_MCLeftHandNippleGrab = GetCachedPart("MCLeftHandNippleGrab");
		Part_MCRightHandNippleGrab = GetCachedPart("MCRightHandNippleGrab");
		Part_ArtMesh8 = GetCachedPart("ArtMesh8");
		Part_MCLeftHandThumbNippleGrab = GetCachedPart("MCLeftHandThumbNippleGrab");
		Part_MCRightHandWaistGrab = GetCachedPart("MCRightHandWaistGrab");
		Part_MCLeftHandWaistGrab = GetCachedPart("MCLeftHandWaistGrab");
		Part_MCLeftHandMiddleFingersFingering = GetCachedPart("MCLeftHandMiddleFingersFingering");
		Part_MCLeftHandFingering = GetCachedPart("MCLeftHandFingering");
		Part_MCRightHandFondle = GetCachedPart("MCRightHandFondle");
		Part_MCLeftHandFondle = GetCachedPart("MCLeftHandFondle");
		Part_AttachArmRHandHandholdingAbove = GetCachedPart("AttachArmRHandHandholdingAbove");
		Part_MCLeftHandHandholding = GetCachedPart("MCLeftHandHandholding");
		Part_AttachArmLHandHandholdingAbove = GetCachedPart("AttachArmLHandHandholdingAbove");
		Part_MCRightHandHandholding = GetCachedPart("MCRightHandHandholding");
		Part_ClitRub = GetCachedPart("ClitRub");
		Part_CumAnal = GetCachedPart("CumAnal");
		Part_CumVagina2 = GetCachedPart("CumVagina2");
		Part_SkinCumflation = GetCachedPart("SkinCumflation");
		Part_AttachLegRFront = GetCachedPart("AttachLegRFront");
		Part_AttachLegRFoot = GetCachedPart("AttachLegRFoot");
		Part_AttachLegRBack = GetCachedPart("AttachLegRBack");
		Part_AttachLegLFront = GetCachedPart("AttachLegLFront");
		Part_AttachLegLFoot = GetCachedPart("AttachLegLFoot");
		Part_AttachLegLBack = GetCachedPart("AttachLegLBack");
		Part_AttachArmLForearm = GetCachedPart("AttachArmLForearm");
		Part_AttachArmLHand1 = GetCachedPart("AttachArmLHand1");
		Part_AttachArmLHand2 = GetCachedPart("AttachArmLHand2");
		Part_AttachArmL = GetCachedPart("AttachArmL");
		Part_AttachArmRForearm = GetCachedPart("AttachArmRForearm");
		Part_AttachArmRHand1 = GetCachedPart("AttachArmRHand1");
		Part_AttachArmRHand2 = GetCachedPart("AttachArmRHand2");
		Part_AttachArmR = GetCachedPart("AttachArmR");
		Part_Dress1_LowerFront = GetCachedPart("Dress1_LowerFront");
		Part_Dress1_Chest = GetCachedPart("Dress1_Chest");
		Part_Dress1_StringR = GetCachedPart("Dress1_StringR");
		Part_Dress1_Pelvis = GetCachedPart("Dress1_Pelvis");
		Part_Dress1_Waist = GetCachedPart("Dress1_Waist");
		Part_Dress1_Ribcage = GetCachedPart("Dress1_Ribcage");
		Part_Dress1_LowerBack = GetCachedPart("Dress1_LowerBack");
		Part_Dress1_StringsL = GetCachedPart("Dress1_StringsL");
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
		Part_CumOutsideInnerThighLowerL = GetCachedPart("CumOutsideInnerThighLowerL");
		Part_CumOutsideInnerThighL2 = GetCachedPart("CumOutsideInnerThighL2");
		Part_CumOutsideInnerThighR2 = GetCachedPart("CumOutsideInnerThighR2");
		Part_CumOutsideBoobL = GetCachedPart("CumOutsideBoobL");
		Part_CumOutsideBoobLSide = GetCachedPart("CumOutsideBoobLSide");
		Part_NipPiercingLeft = GetCachedPart("NipPiercingLeft");
		Part_NipPiercingRight = GetCachedPart("NipPiercingRight");
		Part_NavelPiercing = GetCachedPart("NavelPiercing");
		Part_MCAnusHandWrist_POV = GetCachedPart("MCAnusHandWrist_POV");
		Part_MCAnusHandMain_POV = GetCachedPart("MCAnusHandMain_POV");
		Part_MCAnusHandFinger_POV = GetCachedPart("MCAnusHandFinger_POV");
		Part_MCAnusHandThumb_POV = GetCachedPart("MCAnusHandThumb_POV");
		Part_CumWomb1 = GetCachedPart("CumWomb1");
		Part_CumVagina1 = GetCachedPart("CumVagina1");
		Part_VaginaInner2 = GetCachedPart("VaginaInner2");
		Part_ModdableHairFront = GetCachedPart("ModdableHairFront");
		Part_H0_FrontClippedUp_Bang = GetCachedPart("H0_FrontClippedUp_Bang");
		Part_H0_FrontClippedUp_HairClip = GetCachedPart("H0_FrontClippedUp_HairClip");
		Part_H0_Front_Bang = GetCachedPart("H0_Front_Bang");
		Part_H0_Side_BangL = GetCachedPart("H0_Side_BangL");
		Part_H0_Side_BangR = GetCachedPart("H0_Side_BangR");
		Part_H0_SideAlt_Strand = GetCachedPart("H0_SideAlt_Strand");
		Part_H2_Front_Bang = GetCachedPart("H2_Front_Bang");
		Part_H3_Front = GetCachedPart("H3_Front");
		Part_CuteBow = GetCachedPart("CuteBow");
		Part_Headband = GetCachedPart("Headband");
		Part_WizardHat = GetCachedPart("WizardHat");
		Part_BikiniBot_U_Main = GetCachedPart("BikiniBot_U_Main");
		Part_BikiniTop_M_Left = GetCachedPart("BikiniTop_M_Left");
		Part_BikiniTop_M_Right = GetCachedPart("BikiniTop_M_Right");
		Part_BikiniTop_A_StrapR = GetCachedPart("BikiniTop_A_StrapR");
		Part_BikiniTop_A_StrapL = GetCachedPart("BikiniTop_A_StrapL");
		Part_BikiniBot_U_Main2 = GetCachedPart("BikiniBot_U_Main2");
		Part_BikiniBot_U_Main3 = GetCachedPart("BikiniBot_U_Main3");
		Part_BikiniBot_U_Main4 = GetCachedPart("BikiniBot_U_Main4");
		Part_BikiniBot_U_Main5 = GetCachedPart("BikiniBot_U_Main5");
		Part_BikiniBot_U_Main6 = GetCachedPart("BikiniBot_U_Main6");
		Part_BikiniBot_U_Main7 = GetCachedPart("BikiniBot_U_Main7");
		Part_BikiniTop_A_OutlineL = GetCachedPart("BikiniTop_A_OutlineL");
		Part_BikiniTop_A_OutlineR = GetCachedPart("BikiniTop_A_OutlineR");
		Part_BikiniTop_A_StrapMiddle = GetCachedPart("BikiniTop_A_StrapMiddle");
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
		coloringGroups[ColoringGroupEnum.Skirt] = new ColoringGroup(ColoringGroupEnum.Skirt, new List<Part> { Part_SkirtUpper, Part_SkirtBack });
		coloringGroups[ColoringGroupEnum.Shirt] = new ColoringGroup(ColoringGroupEnum.Shirt, new List<Part>
		{
			Part_Plainshirt_front, Part_Plainshirt_lowerback, Part_Plainshirt_main_alt, Part_Plainshirt_main_top_clone, Part_Plainshirt_sleeveL_lower, Part_Plainshirt_sleeveL_Shoulder, Part_Plainshirt_sleeveL_back, Part_Plainshirt_sleeveR_lower, Part_Plainshirt_sleeveR_Shoulder, Part_Plainshirt_sleeveR_back,
			Part_Plainshirt_alt, Part_Plainshirt_alt_back, Part_Plainshirt_chest2, Part_Plainshirt_main_bottom
		});
		coloringGroups[ColoringGroupEnum.ShirtShadow] = new ColoringGroup(ColoringGroupEnum.ShirtShadow, new List<Part> { Part_ShadowPlainshirt_shadow, Part_ShadowPlainshirt_alt_shadow });
		coloringGroups[ColoringGroupEnum.StockingL] = new ColoringGroup(ColoringGroupEnum.StockingL, new List<Part> { Part_StockingL_Lower, Part_StockingL_foot, Part_StockingL_thigh });
		coloringGroups[ColoringGroupEnum.StockingR] = new ColoringGroup(ColoringGroupEnum.StockingR, new List<Part> { Part_StockingR_foot, Part_StockingR_lower, Part_StockingR_thigh });
		coloringGroups[ColoringGroupEnum.Panties] = new ColoringGroup(ColoringGroupEnum.Panties, new List<Part> { Part_Panties_upper, Part_Panties_Above, Part_Panties_Bottom });
		coloringGroups[ColoringGroupEnum.Bra] = new ColoringGroup(ColoringGroupEnum.Bra, new List<Part> { Part_BraChest, Part_BraTopL, Part_BraTopR });
		coloringGroups[ColoringGroupEnum.Hoodie] = new ColoringGroup(ColoringGroupEnum.Hoodie, new List<Part>
		{
			Part_LongHoodieHoodFront, Part_LongHoodieHoodBack, Part_LongHoodieBottomFront, Part_LongHoodieChest, Part_LongHoodieFront, Part_LongHoodieSleeve2L, Part_LongHoodieSleeve2R, Part_LongHoodieSleeveL, Part_LongHoodieSleeveR, Part_LongHoodieBack,
			Part_LongHoodieUpFront, Part_LongHoodieUpBack
		});
		coloringGroups[ColoringGroupEnum.HoodieStrings] = new ColoringGroup(ColoringGroupEnum.HoodieStrings, new List<Part> { Part_AltLongHoodieStringL, Part_AltLongHoodieStringR });
		coloringGroups[ColoringGroupEnum.Pants] = new ColoringGroup(ColoringGroupEnum.Pants, new List<Part> { Part_BPantsLowerR, Part_BPantsLowerL, Part_BPantsUpperR, Part_BPantsUpperL, Part_BPantsCrotch, Part_BPantsRemoved });
		coloringGroups[ColoringGroupEnum.PantsBelt] = new ColoringGroup(ColoringGroupEnum.PantsBelt, new List<Part> { Part_AltBPantsBelt });
		coloringGroups[ColoringGroupEnum.Dress1] = new ColoringGroup(ColoringGroupEnum.Dress1, new List<Part> { Part_Dress1_LowerFront, Part_Dress1_Chest, Part_Dress1_StringR, Part_Dress1_Pelvis, Part_Dress1_Waist, Part_Dress1_Ribcage, Part_Dress1_LowerBack, Part_Dress1_StringsL });
		coloringGroups[ColoringGroupEnum.Dress2Alt] = new ColoringGroup(ColoringGroupEnum.Dress2Alt, new List<Part> { Part_Dress2AltWaist, Part_Dress2AltRib });
		coloringGroups[ColoringGroupEnum.Dress2] = new ColoringGroup(ColoringGroupEnum.Dress2, new List<Part> { Part_Dress2RolledUpFront, Part_Dress2BottomBack, Part_Dress2Chest, Part_Dress2SleeveL, Part_Dress2SleeveR });
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
		coloringGroups[ColoringGroupEnum.BikiniTop_Main] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Main, new List<Part> { Part_BikiniTop_M_Left, Part_BikiniTop_M_Right });
		coloringGroups[ColoringGroupEnum.BikiniTop_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Alt, new List<Part> { Part_BikiniTop_A_StrapR, Part_BikiniTop_A_StrapL, Part_BikiniTop_A_OutlineL, Part_BikiniTop_A_OutlineR, Part_BikiniTop_A_StrapMiddle });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Main] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Main, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniBottom_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Alt, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniBottom_Uncolored] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Uncolored, new List<Part> { Part_BikiniBot_U_Main, Part_BikiniBot_U_Main2, Part_BikiniBot_U_Main3, Part_BikiniBot_U_Main4, Part_BikiniBot_U_Main5, Part_BikiniBot_U_Main6, Part_BikiniBot_U_Main7 });
		coloringGroups[ColoringGroupEnum.IrisL] = new ColoringGroup(ColoringGroupEnum.IrisL, new List<Part> { Part_PupilL });
		coloringGroups[ColoringGroupEnum.IrisR] = new ColoringGroup(ColoringGroupEnum.IrisR, new List<Part> { Part_PupilR });
		coloringGroups[ColoringGroupEnum.PupilL] = new ColoringGroup(ColoringGroupEnum.PupilL, new List<Part> { Part_Iris1L });
		coloringGroups[ColoringGroupEnum.PupilR] = new ColoringGroup(ColoringGroupEnum.PupilR, new List<Part> { Part_Iris1R });
		coloringGroups[ColoringGroupEnum.Sclera] = new ColoringGroup(ColoringGroupEnum.Sclera, new List<Part> { Part_EyeBall });
		coloringGroups[ColoringGroupEnum.Eyelash] = new ColoringGroup(ColoringGroupEnum.Eyelash, new List<Part>());
		coloringGroups[ColoringGroupEnum.HeartPupils] = new ColoringGroup(ColoringGroupEnum.HeartPupils, new List<Part> { Part_Iris2L, Part_Iris2R });
		coloringGroups[ColoringGroupEnum.EyeBrows] = new ColoringGroup(ColoringGroupEnum.EyeBrows, new List<Part> { Part_BrowL, Part_BrowR });
		coloringGroups[ColoringGroupEnum.Nipples] = new ColoringGroup(ColoringGroupEnum.Nipples, new List<Part> { Part_NippleL, Part_NippleR });
		coloringGroups[ColoringGroupEnum.Vagina] = new ColoringGroup(ColoringGroupEnum.Vagina, new List<Part> { Part_VaginaInner, Part_VaginaBase, Part_VaginaInner2 });
		coloringGroups[ColoringGroupEnum.Xray] = new ColoringGroup(ColoringGroupEnum.Xray, new List<Part> { Part_Xrayouter, Part_XrayWomb, Part_XrayBase, Part_XrayAnalOuter, Part_XrayAnalInnerR, Part_XrayAnalInnerL, Part_XrayAnalBase, Part_XrayInnerL, Part_XrayInnerR2 });
		coloringGroups[ColoringGroupEnum.Anus] = new ColoringGroup(ColoringGroupEnum.Anus, new List<Part> { Part_AnusFront, Part_AnusBack });
		coloringGroups[ColoringGroupEnum.Teeth] = new ColoringGroup(ColoringGroupEnum.Teeth, new List<Part> { Part_TeethUpper, Part_TeethLower });
		coloringGroups[ColoringGroupEnum.Mouth] = new ColoringGroup(ColoringGroupEnum.Mouth, new List<Part> { Part_Tounge, Part_InnerMouthTounge, Part_InnerMouth });
		coloringGroups[ColoringGroupEnum.Boobs] = new ColoringGroup(ColoringGroupEnum.Boobs, new List<Part> { Part_SkinBoobL, Part_SkinBoobR });
		coloringGroups[ColoringGroupEnum.Ass] = new ColoringGroup(ColoringGroupEnum.Ass, new List<Part> { Part_SkinButtL, Part_SkinButtR });
		coloringGroups[ColoringGroupEnum.Pelvic] = new ColoringGroup(ColoringGroupEnum.Pelvic, new List<Part> { Part_SkinVaginaPenetratedFront, Part_SkinVaginaPenetratedBack, Part_SkinVaginaL, Part_SkinVaginaR, Part_SkinPelvis });
		coloringGroups[ColoringGroupEnum.Thigh] = new ColoringGroup(ColoringGroupEnum.Thigh, new List<Part> { Part_SkinLegRFront, Part_SkinLegRBack, Part_SkinLegLFront, Part_SkinLegLBack });
		coloringGroups[ColoringGroupEnum.Tummy] = new ColoringGroup(ColoringGroupEnum.Tummy, new List<Part> { Part_SkinTorsoLower });
		coloringGroups[ColoringGroupEnum.Skin] = new ColoringGroup(ColoringGroupEnum.Skin, new List<Part>
		{
			Part_SkinNose, Part_SkinFace, Part_SkinEyelidClosedL, Part_SkinEyelidClosedR, Part_SkinEyelidL, Part_SkinEyelidR, Part_SkinEyelid2L, Part_SkinEyelid2R, Part_SkinNeck, Part_SkinRibcageFront,
			Part_SkinArmR, Part_SkinArmL, Part_SkinShoulderL, Part_SkinShoulderR, Part_SkinRibcageBack, Part_SkinTorsoUpper, Part_SkinMouthThumbSuck, Part_SkinLipUpper, Part_SkinLipLower, Part_SkinLipUpperToungeOut,
			Part_SkinLipLowerToungeOut, Part_SkinCumflation
		});
		coloringGroups[ColoringGroupEnum.Blush] = new ColoringGroup(ColoringGroupEnum.Blush, new List<Part> { Part_blush_2, Part_blush });
		coloringGroups[ColoringGroupEnum.SharpTeeth] = new ColoringGroup(ColoringGroupEnum.SharpTeeth, new List<Part> { Part_SharpTeethUpper, Part_SharpTeethLower });
		coloringGroups[ColoringGroupEnum.CatEarMain] = new ColoringGroup(ColoringGroupEnum.CatEarMain, new List<Part> { Part_CatearFrontR, Part_CatearFrontL, Part_CatearBackR, Part_CatearBackL });
		coloringGroups[ColoringGroupEnum.CatEarSecondary] = new ColoringGroup(ColoringGroupEnum.CatEarSecondary, new List<Part> { Part_CatearMidL, Part_CatearMidR });
		coloringGroups[ColoringGroupEnum.LegLFoot] = new ColoringGroup(ColoringGroupEnum.LegLFoot, new List<Part> { Part_AttachLegLFoot });
		coloringGroups[ColoringGroupEnum.LegL] = new ColoringGroup(ColoringGroupEnum.LegL, new List<Part> { Part_AttachLegLFront, Part_AttachLegLBack });
		coloringGroups[ColoringGroupEnum.LegRFoot] = new ColoringGroup(ColoringGroupEnum.LegRFoot, new List<Part> { Part_AttachLegRFoot });
		coloringGroups[ColoringGroupEnum.LegR] = new ColoringGroup(ColoringGroupEnum.LegR, new List<Part> { Part_AttachLegRFront, Part_AttachLegRBack });
		coloringGroups[ColoringGroupEnum.ArmL] = new ColoringGroup(ColoringGroupEnum.ArmL, new List<Part> { Part_AttachArmLHandHandholdingAbove, Part_AttachArmLForearm, Part_AttachArmLHand1, Part_AttachArmLHand2, Part_AttachArmL });
		coloringGroups[ColoringGroupEnum.ArmR] = new ColoringGroup(ColoringGroupEnum.ArmR, new List<Part> { Part_AttachArmRHandHandholdingAbove, Part_AttachArmRForearm, Part_AttachArmRHand1, Part_AttachArmRHand2, Part_AttachArmR });
		coloringGroups[ColoringGroupEnum.Tail] = new ColoringGroup(ColoringGroupEnum.Tail, new List<Part> { Part_TailMain10, Part_TailMain9, Part_TailMain8, Part_TailMain7, Part_TailMain6, Part_TailMain5, Part_TailMain4, Part_TailMain3, Part_TailMain2, Part_TailMain });
		coloringGroups[ColoringGroupEnum.TailPlug] = new ColoringGroup(ColoringGroupEnum.TailPlug, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hologram] = new ColoringGroup(ColoringGroupEnum.Hologram, new List<Part> { Part_HairHologram });
		coloringGroups[ColoringGroupEnum.OtherAttachments] = new ColoringGroup(ColoringGroupEnum.OtherAttachments, new List<Part>());
		coloringGroups[ColoringGroupEnum.MCSkin] = new ColoringGroup(ColoringGroupEnum.MCSkin, new List<Part>
		{
			Part_headpet_1, Part_headpet_2, Part_headpet_3, Part_MCRightHandVaginaSpread, Part_MCRightHandThumbVaginaSpread, Part_MCLeftHandVaginaSpread, Part_MCLeftHandThumbVaginaSpread, Part_MCLeftHandLegGrab, Part_MCRightHandLegGrab, Part_MCLeftHandNippleGrab,
			Part_MCRightHandNippleGrab, Part_MCLeftHandThumbNippleGrab, Part_MCRightHandWaistGrab, Part_MCLeftHandWaistGrab, Part_MCLeftHandMiddleFingersFingering, Part_MCLeftHandFingering, Part_MCRightHandFondle, Part_MCLeftHandFondle, Part_MCLeftHandHandholding, Part_MCRightHandHandholding,
			Part_MCAnusHandWrist_POV, Part_MCAnusHandMain_POV, Part_MCAnusHandFinger_POV, Part_MCAnusHandThumb_POV
		});
		coloringGroups[ColoringGroupEnum.MCCockSkin] = new ColoringGroup(ColoringGroupEnum.MCCockSkin, new List<Part> { Part_DickSkin });
		coloringGroups[ColoringGroupEnum.MCCockTip] = new ColoringGroup(ColoringGroupEnum.MCCockTip, new List<Part> { Part_DickMain });
		coloringGroups[ColoringGroupEnum.Shadow] = new ColoringGroup(ColoringGroupEnum.Shadow, new List<Part>());
		coloringGroups[ColoringGroupEnum.Highlight] = new ColoringGroup(ColoringGroupEnum.Highlight, new List<Part> { Part_highlight, Part_HighlightBoobL });
		coloringGroups[ColoringGroupEnum.Mask] = new ColoringGroup(ColoringGroupEnum.Mask, new List<Part> { Part_GlowOmegaMeterMask, Part_DickMask, Part_ThumbMask });
		coloringGroups[ColoringGroupEnum.Moddable] = new ColoringGroup(ColoringGroupEnum.Moddable, new List<Part> { Part_ModdableBack, Part_ModdableHairBack, Part_ModdableFace, Part_ModdableShirtLogo, Part_ModdableShirtLeftSleeveLogo, Part_ModdableShirtRightSleeveLogo, Part_ModdablePantiesLogo, Part_ModdableHoodieLogo, Part_ModdableHairFront });
		coloringGroups[ColoringGroupEnum.HitArea] = new ColoringGroup(ColoringGroupEnum.HitArea, new List<Part>
		{
			HitAreaSpread, HitAreaBoobR, HitAreaNippleL, HitAreaNippleR, HitAreaBoobL, HitAreaBothNipples, HitAreaBothBoobs, HitAreaGrabLegL, HitAreaGrabLegR, HitAreaWaistGrab,
			HitAreaOpenMouth, HitAreaCloseMouth, HitAreaFaceStroke, HitAreaThumbMouth, HitAreaFingering, HitAreaClitRub, HitAreaHandholdingL, HitAreaHandholdingR, HitAreaHeadpat, HitAreaLongHoodieMoveDown,
			HitAreaLongHoodieMoveUp, HitAreaAnusCenter, HitArea_Panties
		});
		coloringGroups[ColoringGroupEnum.Cum] = new ColoringGroup(ColoringGroupEnum.Cum, new List<Part>
		{
			Part_CumOutsideUnderBoobMiddle, Part_CumOutsideBoobR, Part_CumOutsideInnerThighR, Part_CumOutsideInnerThighL, Part_CumOutsideBelly, Part_CumWombLv1, Part_CumWombLv2, Part_CumWombLv3, Part_CumWombLv4, Part_CumVaginaLeak2,
			Part_CumVaginaLeak1, Part_CumDick1, Part_CumDick2, Part_CumOutsideBellyR, Part_CumOutsideBoobRSide, Part_CumOutsideFaceRight, Part_CumOutsideFaceNose, Part_CumOutsideFaceLeft, Part_CumAnal, Part_CumVagina2,
			Part_CumOutsideInnerThighLowerL, Part_CumOutsideInnerThighL2, Part_CumOutsideInnerThighR2, Part_CumOutsideBoobL, Part_CumOutsideBoobLSide, Part_CumWomb1, Part_CumVagina1
		});
		coloringGroups[ColoringGroupEnum.Rest] = new ColoringGroup(ColoringGroupEnum.Rest, new List<Part>
		{
			Part_lines, Part_InsideLegR, Part_InsideLegL, Part_HightlightBoobR, Part_Barcode, Part_InsdeArmR, Part_InsdeArmL, Part_GlowOmegaMeterInner, Part_GlowOmegaMeter, Part_ThumbSuckThumb,
			Part_ThumbSuck, Part_Saliva2, Part_Saliva1, Part_ArtMesh8, Part_ClitRub
		});
	}
}
