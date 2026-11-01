using System.Collections.Generic;
using System.Linq;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Physics;
using Live2D.Cubism.Framework.Raycasting;
using UnityEngine;

public sealed class Live2DDoggyFuckController : Live2DLewdSceneController, ILive2DController_HeadY, ILive2DController_BodyY, ILive2DController_TongueMovement, ILive2DController_OmegaMeter, ILive2DController_BlushAndHeartEyes, ILive2DController_EyeREmotes, ILive2DController_EyeRCloseAndShock, ILive2DController_BrowRY, ILive2DController_BrowRRot, ILive2DController_BrowREmote, ILive2DController_Mouth, ILive2DController_TailWiggle, ILive2DController_Lewd_CumShotY, ILive2DController_Lewd_BotTense, ILive2DController_Lewd_BotOrgasm, ILive2DController_Manager_XRay, ILive2DController_Lewd_CumShotCommon, ILive2DController_Lewd_CumShotBasic, ILive2DController_Lewd_CumShotLength, ILive2DController_Lewd_CumInsideMain, ILive2DController_Lewd_CumInsideAnal, ILive2DController_Lewd_CumInsideWomb, ILive2DController_Lewd_CumInsideVagina, ILive2DController_Manager_FrickStaticGuiManager, ILive2DController_Manager_InsertionManager, ILive2DController_Manager_CockManager, ILive2DController_BodyMod_AllLegs, ILive2DController_BodyMod_ThighSize, ILive2DController_BodyMod_CalfSize, ILive2DController_BodyMod_FootSize, ILive2DController_BodyMod_BoobSize, ILive2DController_BodyMod_AssSize
{
	private const float BlockSkirt = -0.93f;

	private const float BlockSkirtAnal = -0.93f;

	private const float BlockPants = -0.53f;

	private const float BlockPantsAnal = -0.5f;

	private const float BlockDressBoth = -0.5f;

	private CockLimbUsage<Live2DDoggyFuckController> _cockFuckLimbUsage;

	public override ControllerEnum ControllerEnum => ControllerEnum.Doggy;

	public override SceneEnumFlag SceneEnum => SceneEnumFlag.Doggy;

	public override bool IsDress2Aside => ParamDress2Up.Value > 0.45f;

	private bool SkirtHasDoggyAlwaysUpTag => GameVariables.Current.itemManager.FirstItemAtSlotHasTag(ItemSlot.SlotManager.LowerBody.Id, ItemPrefabTag.Doggy_Skirt_AlwaysUp);

	public override bool IsSkirtAside
	{
		get
		{
			if (!(ParamSkirtPullup.Value > 0.45f))
			{
				return SkirtHasDoggyAlwaysUpTag;
			}
			return true;
		}
	}

	public override bool IsPantsAside
	{
		get
		{
			return ParamPantsDown.Value > 0.45f;
		}
		set
		{
			ParamPantsDown.Value = (value ? ParamPantsDown.MaximumValue : ParamPantsDown.MinimumValue);
			ParamPantsDown.FinishMove();
		}
	}

	public Live2DInteractableLimbUsage<Live2DDoggyFuckController> SkirtInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DDoggyFuckController> PantsInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DDoggyFuckController> Dress2UpInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DDoggyFuckController> Dress2DownInteractable { get; private set; }

	public override bool IsPantiesAside => true;

	public Live2DParameter ParamBodyY => ParamPelvisX;

	public Live2DInteractableLimbUsage<Live2DDoggyFuckController> AssGrabInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DDoggyFuckController> HeadGrabInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DDoggyFuckController> BoobFondleInteractable { get; private set; }

	public BasicInsertionHandleFuckAxis VaginaCockFuckAxis { get; private set; }

	public InsertionHandler VaginaInsertionHandler { get; private set; }

	public BasicInsertionHandleFuckAxis AnalCockFuckAxis { get; private set; }

	public InsertionHandler AnalInsertionHandler { get; private set; }

	public IXRayManager XRayManager { get; private set; }

	public FrickStaticGuiManager FrickStaticGuiManager { get; private set; }

	public InsertionManager InsertionManager { get; private set; }

	public ICockManager CockManager { get; private set; }

	public Live2DParameter ParamEyeballRX { get; private set; }

	public Live2DParameter ParamEyeballRY { get; private set; }

	public Live2DParameter ParamHeart { get; private set; }

	public Live2DParameter ParamIrisZoom { get; private set; }

	public Live2DParameter ParamEyesHappy { get; private set; }

	public Live2DParameter ParamBrowREmote { get; private set; }

	public Live2DParameter ParamBrowRRot { get; private set; }

	public Live2DParameter ParamEyeRShock { get; private set; }

	public Live2DParameter ParamBrowRY { get; private set; }

	public Live2DParameter ParamToungeX { get; private set; }

	public Live2DParameter ParamToungeY { get; private set; }

	public Live2DParameter ParamMouthForm { get; private set; }

	public Live2DParameter ParamMouthOpen { get; private set; }

	public Live2DParameter ParamBlush { get; private set; }

	public Live2DParameter ParamHeadY { get; private set; }

	public Live2DParameter ParamEarR { get; private set; }

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

	public Live2DParameter ParamTense { get; private set; }

	public Live2DParameter ParamOmegaMeter { get; private set; }

	public Live2DParameter ParamDick { get; private set; }

	public Live2DParameter ParamXray { get; private set; }

	public Live2DParameter ParamThrust { get; private set; }

	public Live2DParameter ParamCumInside { get; private set; }

	public Live2DParameter ParamCumInsideWomb { get; private set; }

	public Live2DParameter ParamThrustAnal { get; private set; }

	public Live2DParameter ParamCumInsideAnal { get; private set; }

	public Live2DParameter ParamFlacidity { get; private set; }

	public Live2DParameter ParamCumShotLengthY { get; private set; }

	public Live2DParameter ParamCumOutsideY { get; private set; }

	public Live2DParameter ParamHideCumShot { get; private set; }

	public Live2DParameter ParamCumShotAnim { get; private set; }

	public Live2DParameter ParamXRayAnalT0 { get; private set; }

	public Live2DParameter ParamXRayAnalT1 { get; private set; }

	public Live2DParameter ParamXRayAnalT2 { get; private set; }

	public Live2DParameter ParamXRayAnalT3 { get; private set; }

	public Live2DParameter ParamXRayAnalT4 { get; private set; }

	public Live2DParameter ParamXRayAnalT5 { get; private set; }

	public Live2DParameter ParamXRayAnalT6 { get; private set; }

	public Live2DParameter ParamXRayAnalT7 { get; private set; }

	public Live2DParameter ParamXRayAnalT8 { get; private set; }

	public Live2DParameter ParamXRayAnalB0 { get; private set; }

	public Live2DParameter ParamXRayAnalB1 { get; private set; }

	public Live2DParameter ParamXRayAnalB2 { get; private set; }

	public Live2DParameter ParamXRayAnalB3 { get; private set; }

	public Live2DParameter ParamXRayAnalB4 { get; private set; }

	public Live2DParameter ParamXRayAnalB5 { get; private set; }

	public Live2DParameter ParamXRayAnalB6 { get; private set; }

	public Live2DParameter ParamXRayAnalB7 { get; private set; }

	public Live2DParameter ParamXRayAnalB8 { get; private set; }

	public Live2DParameter ParamAnalCockAngle { get; private set; }

	public Live2DParameter ParamXRayVagT0 { get; private set; }

	public Live2DParameter ParamXRayVagT1 { get; private set; }

	public Live2DParameter ParamXRayVagT2 { get; private set; }

	public Live2DParameter ParamXRayVagT3 { get; private set; }

	public Live2DParameter ParamXRayVagT4 { get; private set; }

	public Live2DParameter ParamXRayVagT5 { get; private set; }

	public Live2DParameter ParamXRayVagT6 { get; private set; }

	public Live2DParameter ParamXRayVagT7 { get; private set; }

	public Live2DParameter ParamXRayVagT8 { get; private set; }

	public Live2DParameter ParamXRayVagB0 { get; private set; }

	public Live2DParameter ParamXRayVagB1 { get; private set; }

	public Live2DParameter ParamXRayVagB2 { get; private set; }

	public Live2DParameter ParamXRayVagB3 { get; private set; }

	public Live2DParameter ParamXRayVagB4 { get; private set; }

	public Live2DParameter ParamXRayVagB5 { get; private set; }

	public Live2DParameter ParamXRayVagB6 { get; private set; }

	public Live2DParameter ParamXRayVagB7 { get; private set; }

	public Live2DParameter ParamXRayVagB8 { get; private set; }

	public Live2DParameter ParamVaginaCockAngle { get; private set; }

	public Live2DParameter ParamPelvisX { get; private set; }

	public Live2DParameter ParamOrgasm { get; private set; }

	public Live2DParameter ParamSkirtPullup { get; private set; }

	public Live2DParameter ParamDress2Up { get; private set; }

	public Live2DParameter ParamPantsDown { get; private set; }

	public Live2DParameter ParamFlattenBoobs { get; private set; }

	public Live2DParameter ParamFlattenNipples { get; private set; }

	public Live2DParameter ParamMCGrabHeadEnable { get; private set; }

	public Live2DParameter ParamGrabBoobsEnable { get; private set; }

	public Live2DParameter ParamGrabBoobsTighten { get; private set; }

	public Live2DParameter ParamGrabButtEnable { get; private set; }

	public Live2DParameter CumPhysics { get; private set; }

	public Live2DParameter ParamPhysicsButt { get; private set; }

	public Live2DParameter ParamHairPhysicsBaseToShort { get; private set; }

	public Live2DParameter ParamHairPhysicsShortToMid { get; private set; }

	public Live2DParameter ParamHairPhysicsMidToLong { get; private set; }

	public Live2DParameter ParamPhysicsArm { get; private set; }

	public Live2DParameter ParamPhysicsHead { get; private set; }

	public Live2DParameter ParamPhysicsBoobX { get; private set; }

	public Live2DParameter ParamPhysicsBoobY { get; private set; }

	public Live2DParameter ParamSkirtPhysics { get; private set; }

	public Live2DParameter ParamAssSize { get; private set; }

	public Live2DParameter ParamThighSize { get; private set; }

	public Live2DParameter ParamCalfSize { get; private set; }

	public Live2DParameter ParamFootLength { get; private set; }

	public Live2DParameter ParamFootWidth { get; private set; }

	public Live2DParameter ParamBoobSize { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_ArmPhysics { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_HairPhysics { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_HeadPhysics { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobsX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobsY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_ButtPhysics { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Skirt { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Tail { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_CumPhysics { get; private set; }

	public Part HitAreaBoobFondle { get; private set; }

	public Part HitAreaAssGrab { get; private set; }

	public Part HitAreaHeadGrab { get; private set; }

	public Part HitAreaDress2Down { get; private set; }

	public Part HitAreaDress2RolledUp { get; private set; }

	public Part HitAreaSkirt { get; private set; }

	public Part Part_DickSkin { get; private set; }

	public Part Part_DickMain { get; private set; }

	public Part Part_AttachArmR { get; private set; }

	public Part Part_AttachArmRForearm { get; private set; }

	public Part Part_AttachArmRHand { get; private set; }

	public Part Part_AttachArmLHand { get; private set; }

	public Part Part_OmegaMeterMask { get; private set; }

	public Part Part_StockingR_foot { get; private set; }

	public Part Part_StockingR_lower { get; private set; }

	public Part Part_StockingR_thigh { get; private set; }

	public Part Part_Panties { get; private set; }

	public Part Part_StockingL_foot { get; private set; }

	public Part Part_StockingL_lower { get; private set; }

	public Part Part_StockingL_thigh { get; private set; }

	public Part Part_BraUpper { get; private set; }

	public Part Part_BraChest { get; private set; }

	public Part Part_BraLower { get; private set; }

	public Part Part_SkirtDownLower { get; private set; }

	public Part Part_SkirtDownBack { get; private set; }

	public Part Part_SkirtUpper { get; private set; }

	public Part Part_SkirtDownBack2 { get; private set; }

	public Part Part_SkirtUpper2 { get; private set; }

	public Part Part_ModdableBack { get; private set; }

	public Part Part_AltBPantsBeltDown { get; private set; }

	public Part Part_BPantsCrotchDown { get; private set; }

	public Part Part_BPantsLowerFront { get; private set; }

	public Part Part_BPantsThighFrontDown { get; private set; }

	public Part Part_BPantsThighFront { get; private set; }

	public Part Part_AltBPantsBelt { get; private set; }

	public Part Part_BPantsCrotch { get; private set; }

	public Part Part_BPantsLowerBack { get; private set; }

	public Part Part_BPantsThighBackDown { get; private set; }

	public Part Part_BPantsThighBack { get; private set; }

	public Part Part_LongHoodieHood { get; private set; }

	public Part Part_LongHoodieSleeveFront { get; private set; }

	public Part Part_LongHoodieSleeveBack { get; private set; }

	public Part Part_LongHoodieChest { get; private set; }

	public Part Part_LongHoodieTorso { get; private set; }

	public Part Part_AltLongHoodieStringFront { get; private set; }

	public Part Part_AltLongHoodieStringBack { get; private set; }

	public Part Part_PlainShirt_shoulder1_1 { get; private set; }

	public Part Part_PlainShirt_shoulder1_2 { get; private set; }

	public Part Part_ShadowPlainShirt_arm { get; private set; }

	public Part Part_PlainShirt_shoulder2_1 { get; private set; }

	public Part Part_PlainShirt_shoulder2_2 { get; private set; }

	public Part Part_PlainShirt_chest { get; private set; }

	public Part Part_PlainShirt_lower { get; private set; }

	public Part Part_PlainShirt_main { get; private set; }

	public Part Part_XrayVaginaCover { get; private set; }

	public Part Part_XrayVaginaWomb { get; private set; }

	public Part Part_XrayVaginaTop1 { get; private set; }

	public Part Part_XrayVaginaBottom { get; private set; }

	public Part Part_XrayVaginaBase { get; private set; }

	public Part Part_CumWomb4 { get; private set; }

	public Part Part_CumWomb3 { get; private set; }

	public Part Part_CumWomb2 { get; private set; }

	public Part Part_CumWomb1 { get; private set; }

	public Part Part_XrayAnalCover { get; private set; }

	public Part Part_XrayAnalBot { get; private set; }

	public Part Part_XrayAnalTop { get; private set; }

	public Part Part_XrayAnalBase { get; private set; }

	public Part Part_Hologram { get; private set; }

	public Part Part_BrowR { get; private set; }

	public Part Part_SkinUpperEyelid { get; private set; }

	public Part Part_SkinLowerEyelid { get; private set; }

	public Part Part_HightlightEye { get; private set; }

	public Part Part_Iris2 { get; private set; }

	public Part Part_Iris1 { get; private set; }

	public Part Part_Pupil { get; private set; }

	public Part Part_SkinMouth { get; private set; }

	public Part Part_Tongue { get; private set; }

	public Part Part_SkinNeck { get; private set; }

	public Part Part_FaceLine2 { get; private set; }

	public Part Part_SkinLipUpper { get; private set; }

	public Part Part_TeethSharp { get; private set; }

	public Part Part_TeethNormal { get; private set; }

	public Part Part_SkinHeadBase { get; private set; }

	public Part Part_EyeWhite { get; private set; }

	public Part Part_NippleR { get; private set; }

	public Part Part_HighlightBoob { get; private set; }

	public Part Part_SkinBoobFront { get; private set; }

	public Part Part_AttachLegRLower { get; private set; }

	public Part Part_AttachLegRFoot { get; private set; }

	public Part Part_AttachLegR { get; private set; }

	public Part Part_AttachLegLLower { get; private set; }

	public Part Part_AttachLegLFoot { get; private set; }

	public Part Part_AttachLegL { get; private set; }

	public Part Part_SkinMask { get; private set; }

	public Part Part_SkinShoulderNoAtt { get; private set; }

	public Part Part_SkinArmUpperNoAttFront1 { get; private set; }

	public Part Part_SkinShoulder { get; private set; }

	public Part Part_SkinArmUpper_Front { get; private set; }

	public Part Part_SkinRibcage { get; private set; }

	public Part Part_SkinThighFront { get; private set; }

	public Part Part_SkinButt { get; private set; }

	public Part Part_SkinThighBack { get; private set; }

	public Part Part_SkinPelvis { get; private set; }

	public Part Part_AttachChargeHole { get; private set; }

	public Part Part_GlowOmegaMeterInner { get; private set; }

	public Part Part_GlowOmegameter { get; private set; }

	public Part Part_SkinWaistLower { get; private set; }

	public Part Part_SkinWaistUpper { get; private set; }

	public Part Part_ModdableHairBack { get; private set; }

	public Part Part_ModdableHairFront { get; private set; }

	public Part Part_ModdableFace { get; private set; }

	public Part Part_ModdableShirtLogo { get; private set; }

	public Part Part_ModdableShirtRightSleeveLogo { get; private set; }

	public Part Part_ModdableHoodieLogo { get; private set; }

	public Part Part_TailMain { get; private set; }

	public Part Part_TailPlug { get; private set; }

	public Part Part_TailMain2 { get; private set; }

	public Part Part_TailMain3 { get; private set; }

	public Part Part_TailMain4 { get; private set; }

	public Part Part_TailMain5 { get; private set; }

	public Part Part_TailMain6 { get; private set; }

	public Part Part_TailMain7 { get; private set; }

	public Part Part_TailMain8 { get; private set; }

	public Part Part_TailMain9 { get; private set; }

	public Part Part_TailMain10 { get; private set; }

	public Part Part_CumOutsideAss { get; private set; }

	public Part Part_CumOutsideThighBack { get; private set; }

	public Part Part_CumOutsideAssSide { get; private set; }

	public Part Part_CumOutsideAssUp { get; private set; }

	public Part Part_CumOutsideBoobRSide { get; private set; }

	public Part Part_CumOutsideBoobR { get; private set; }

	public Part Part_CumOutsideBelly { get; private set; }

	public Part Part_Blush1 { get; private set; }

	public Part Part_Blush3 { get; private set; }

	public Part Part_Dress2BottomDown { get; private set; }

	public Part Part_Dress2BottomRolledUp { get; private set; }

	public Part Part_Dress2Sleeve { get; private set; }

	public Part Part_Dress2Chest { get; private set; }

	public Part Part_Dress2AltRib { get; private set; }

	public Part Part_Dress2Waist { get; private set; }

	public Part Part_CumDick2 { get; private set; }

	public Part Part_CumDick1 { get; private set; }

	public Part Part_CumAnalNew { get; private set; }

	public Part Part_H0_HairTie { get; private set; }

	public Part Part_H0_Braid_Lower { get; private set; }

	public Part Part_H0_Braid_1 { get; private set; }

	public Part Part_H0_Braid_2 { get; private set; }

	public Part Part_H0_Front_Bang { get; private set; }

	public Part Part_H0_Top_Leaf1 { get; private set; }

	public Part Part_H0_Top_Leaf2 { get; private set; }

	public Part Part_H0_Back_1 { get; private set; }

	public Part Part_H0_Back_2 { get; private set; }

	public Part Part_H2_Front_Bang { get; private set; }

	public Part Part_H1_SideRight { get; private set; }

	public Part Part_H1_Front_Bang { get; private set; }

	public Part Part_H1_Top_Ahoge { get; private set; }

	public Part Part_H1_Back { get; private set; }

	public Part Part_H1_Side_L { get; private set; }

	public Part Part_H3_Front { get; private set; }

	public Part Part_H3_Side_Right { get; private set; }

	public Part Part_H3_Back { get; private set; }

	public Part Part_H3_Side_Left { get; private set; }

	public Part Part_H4_Knot { get; private set; }

	public Part Part_H4_Tail_Front { get; private set; }

	public Part Part_H4_Tail_Back { get; private set; }

	public Part Part_H4_Tail_FrontTop { get; private set; }

	public Part Part_CumVaginalNew { get; private set; }

	public Part Part_MCHandHeadGrab { get; private set; }

	public Part Part_MCHandBoobs { get; private set; }

	public Part Part_MCHandGrabButt { get; private set; }

	public Part Part_Dress1_Bottom { get; private set; }

	public Part Part_Dress1_Waist { get; private set; }

	public Part Part_Dress1_Chest { get; private set; }

	public Part Part_Dress1_Ribcage { get; private set; }

	public Part Part_CumOutsideAssSide2 { get; private set; }

	public Part Part_CumOutsideAssBottom { get; private set; }

	public Part Part_CumOutsideAss2 { get; private set; }

	public Part Part_CumOutsideAssUp2 { get; private set; }

	public Part Part_H1_Side_Middle { get; private set; }

	public Part Part_NipPiercingRight { get; private set; }

	public Part Part_Headband { get; private set; }

	public Part Part_PointyEarR { get; private set; }

	public Part Part_WizardHat { get; private set; }

	public Part Part_BellChokerMain { get; private set; }

	public Part Part_BellChokerBell { get; private set; }

	public Part Part_CatearBackR { get; private set; }

	public Part Part_CatearMidR { get; private set; }

	public Part Part_CatearFrontL { get; private set; }

	public Part Part_BikiniTop_A_BackRibbon { get; private set; }

	public Part Part_BikiniTop_A_BackString { get; private set; }

	public Part Part_BikiniTop_A_CupOutlineBottom { get; private set; }

	public Part Part_BikiniTop_A_CupOutline { get; private set; }

	public Part Part_BikiniTop_M_Cup { get; private set; }

	public Part Part_BikiniBot_A_Ribbon { get; private set; }

	public Part Part_BikiniBot_A_String { get; private set; }

	public Part Part_H0_Side_BangL { get; private set; }

	public Part Part_H0_Side_BangR2 { get; private set; }

	public Part Part_H0_FrontClippedUp_Bang { get; private set; }

	public Part Part_H0_FrontClippedUp_HairClip { get; private set; }

	public override Part GetApproximateEyesPositionPart()
	{
		return GetCachedPart("EyeWhite");
	}

	public override Part GetApproximateMouthPositionPart()
	{
		return GetCachedPart("SkinHeadBase");
	}

	private void UpdateDress2()
	{
		if (IsDress2Aside)
		{
			ParamDress2Up.SetMaxValue();
			return;
		}
		ParamDress2Up.SetMinValue();
		ParamThrust.SetPercent(-1f);
		ParamThrustAnal.SetPercent(-1f);
	}

	private void UpdateSkirt()
	{
		if (SkirtHasDoggyAlwaysUpTag)
		{
			ParamSkirtPullup.SetMaxValue();
			ParamSkirtPullup.FinishMove();
		}
	}

	private static void InitClothesInteractables(Live2DDoggyFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DDoggyFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DDoggyFuckController>(thisController, LimbUsageChoiceEnum.BothArms)
		{
			HoldUpdateAction = (Live2DDoggyFuckController c, Vector2 mouseMove) =>
			{
				if (c.IsPantsAside)
				{
					c.ParamPantsDown.Value += (0f - mouseMove.y) * 10f;
					if (c.ParamPantsDown.Value < 0.98f)
					{
						R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
						c.ParamPantsDown.Value = c.ParamPantsDown.MinimumValue;
						c.ParamThrust.SetPercent(-1f);
						c.ParamThrustAnal.SetPercent(-1f);
						c.LetGoOfCurrentlyHeld();
					}
				}
				else
				{
					c.ParamPantsDown.Value += (0f - mouseMove.y) * 10f - mouseMove.x * 10f;
					if (c.ParamPantsDown.Value > 0.2f)
					{
						R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
						c.ParamPantsDown.Value = c.ParamPantsDown.MaximumValue;
						c.LetGoOfCurrentlyHeld();
					}
				}
			}
		};
		thisController.PantsInteractable = thisController.AddInteractable(new List<string> { "BPantsCrotch", "BPantsCrotchDown" }, new Live2DInteractableLimbUsage<Live2DDoggyFuckController>(thisController, LOC.UI.TupleReferences.Interactable_RemovePutOnPants, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DDoggyFuckController c) => c.ParamThrust.GetRealValue() <= -0.53f && c.ParamThrustAnal.GetRealValue() <= -0.5f && c.IsPantsOn
		}, CubismRaycastablePrecision.Triangles);
		LimbUsageWithInteractable<Live2DDoggyFuckController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DDoggyFuckController>(thisController, LimbUsageChoiceEnum.BothArms)
		{
			HoldUpdateAction = (Live2DDoggyFuckController c, Vector2 mouseMove) =>
			{
				bool isSkirtAside = c.IsSkirtAside;
				c.ParamSkirtPullup.Value += mouseMove.x * 10f;
				if (isSkirtAside)
				{
					if (c.ParamSkirtPullup.Value < 0.98f)
					{
						R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
						c.ParamSkirtPullup.Value = c.ParamSkirtPullup.MinimumValue;
						c.ParamThrust.SetPercent(-1f);
						c.ParamThrustAnal.SetPercent(-1f);
						c.LetGoOfCurrentlyHeld();
					}
				}
				else if (c.ParamSkirtPullup.Value > 0.45f)
				{
					R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
					c.ParamSkirtPullup.Value = c.ParamSkirtPullup.MaximumValue;
					c.LetGoOfCurrentlyHeld();
				}
			}
		};
		thisController.SkirtInteractable = thisController.AddInteractableHitArea(thisController.HitAreaSkirt, new Live2DInteractableLimbUsage<Live2DDoggyFuckController>(thisController, LOC.UI.TupleReferences.Interactable_MoveSkirt, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DDoggyFuckController c) => c.IsSkirtOn && !c.SkirtHasDoggyAlwaysUpTag && c.ParamThrust.GetRealValue() <= -0.93f && c.ParamThrustAnal.GetRealValue() <= -0.93f
		});
		LimbUsageWithInteractable<Live2DDoggyFuckController> limbUsageWithInteractable3 = new LimbUsageWithInteractable<Live2DDoggyFuckController>(thisController, LimbUsageChoiceEnum.BothArms)
		{
			HoldUpdateAction = (Live2DDoggyFuckController c, Vector2 mouseMove) =>
			{
				c.ParamDress2Up.Value += mouseMove.x * 10f;
				if (c.ParamDress2Up.Value < 0.98f)
				{
					R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
					c.ParamDress2Up.Value = c.ParamDress2Up.MinimumValue;
					c.LetGoOfCurrentlyHeld();
				}
			},
			EndLimbAction = (Live2DDoggyFuckController c, LetGoLimbsCaller caller) =>
			{
				c.UpdateDress2();
			}
		};
		thisController.Dress2UpInteractable = thisController.AddInteractableHitArea(thisController.HitAreaDress2RolledUp, new Live2DInteractableLimbUsage<Live2DDoggyFuckController>(thisController, LOC.UI.TupleReferences.Interactable_MoveDress, limbUsageWithInteractable3)
		{
			ActiveFunc = (Live2DDoggyFuckController c) => c.IsDress2On && c.IsDress2Aside && c.ParamThrust.GetRealValue() <= -0.93f && c.ParamThrustAnal.GetRealValue() <= -0.93f
		});
		LimbUsageWithInteractable<Live2DDoggyFuckController> limbUsageWithInteractable4 = new LimbUsageWithInteractable<Live2DDoggyFuckController>(thisController, LimbUsageChoiceEnum.BothArms)
		{
			HoldUpdateAction = (Live2DDoggyFuckController c, Vector2 mouseMove) =>
			{
				c.ParamDress2Up.Value += mouseMove.x * 10f;
				if (c.ParamDress2Up.Value > 0.45f)
				{
					R.Sound.Frick_clothes_move_All.PlayRandomSlightlyRandomVolumeAndPitch();
					c.ParamDress2Up.Value = c.ParamDress2Up.MaximumValue;
					c.LetGoOfCurrentlyHeld();
				}
			},
			EndLimbAction = (Live2DDoggyFuckController c, LetGoLimbsCaller caller) =>
			{
				c.UpdateDress2();
			}
		};
		thisController.Dress2DownInteractable = thisController.AddInteractableHitArea(thisController.HitAreaDress2Down, new Live2DInteractableLimbUsage<Live2DDoggyFuckController>(thisController, LOC.UI.TupleReferences.Interactable_MoveDress, limbUsageWithInteractable4)
		{
			ActiveFunc = (Live2DDoggyFuckController c) => c.IsDress2On && !c.IsDress2Aside && c.ParamThrust.GetRealValue() <= -0.93f && c.ParamThrustAnal.GetRealValue() <= -0.93f
		});
	}

	protected override bool CanBeRemoved(string id)
	{
		if (id != ItemSlot.SlotManager.LegL.Id)
		{
			return id != ItemSlot.SlotManager.LegR.Id;
		}
		return false;
	}

	private static void InitAssInteractable(Live2DDoggyFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DDoggyFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DDoggyFuckController>(thisController, LimbUsageChoiceEnum.BothArms)
		{
			StartLimbAction = (Live2DDoggyFuckController c) =>
			{
				c.PhysicsSubRig_ButtPhysics.StartParameterControl(c.ParamPhysicsButt);
				c.ParamGrabButtEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DDoggyFuckController c, Vector2 mouseMove) =>
			{
				if (c.CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2) && o.IsInside())
				{
					o2.ThrustMovementAccumulator = (0f - mouseMove.x) * 4f;
				}
				c.ParamPhysicsButt.Value += mouseMove.x * 7f;
				if (c.ParamPhysicsButt.IsNearEdge())
				{
					c.ParamPelvisX.Value += mouseMove.x * 7f;
				}
			},
			EndLimbAction = (Live2DDoggyFuckController c, LetGoLimbsCaller caller) =>
			{
				c.PhysicsSubRig_ButtPhysics.EndParameterControl(c.ParamPhysicsButt);
				c.ParamGrabButtEnable.SetMinValue();
			}
		};
		thisController.AssGrabInteractable = new Live2DInteractableLimbUsage<Live2DDoggyFuckController>(thisController, LOC.UI.TupleReferences.Interactable_GrabAss, limbUsageWithInteractable);
		thisController.AddInteractableHitArea(thisController.HitAreaAssGrab, thisController.AssGrabInteractable);
	}

	private static void InitHeadInteractable(Live2DDoggyFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DDoggyFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DDoggyFuckController>(thisController, LimbUsageChoiceEnum.BothArms)
		{
			StartLimbAction = (Live2DDoggyFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.GrabHead);
				c.PhysicsSubRig_HeadPhysics.StartParameterControl(c.ParamPhysicsHead);
				c.ParamMCGrabHeadEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DDoggyFuckController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsHead.Value += mouseMove.x * 15f;
				c.ParamHeadY.Value += mouseMove.x * 15f;
				c.ParamPhysicsHead.MoveValueTowardsDefaultInUpdate(Time.deltaTime, 1f);
				if (c.ParamPhysicsHead.IsNearEdge())
				{
					c.ParamBodyY.Value += mouseMove.x * 7f;
				}
			},
			EndLimbAction = (Live2DDoggyFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.GrabHead);
				c.PhysicsSubRig_HeadPhysics.EndParameterControl(c.ParamPhysicsHead);
				c.ParamHeadY.ResetToDefault();
				c.ParamMCGrabHeadEnable.SetMinValue();
			}
		};
		thisController.HeadGrabInteractable = new Live2DInteractableLimbUsage<Live2DDoggyFuckController>(thisController, LOC.UI.TupleReferences.Interactable_GrabHead, limbUsageWithInteractable);
		thisController.AddInteractableHitArea(thisController.HitAreaHeadGrab, thisController.HeadGrabInteractable);
	}

	private static void InitBoobInteractables(Live2DDoggyFuckController thisController)
	{
		LimbUsageWithInteractable<Live2DDoggyFuckController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DDoggyFuckController>(thisController, LimbUsageChoiceEnum.BothArms)
		{
			StartLimbAction = (Live2DDoggyFuckController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobsX.StartParameterControl(c.ParamPhysicsBoobX);
				c.PhysicsSubRig_BoobsY.StartParameterControl(c.ParamPhysicsBoobY);
				c.ParamGrabBoobsTighten.SetMinValue();
				c.ParamGrabBoobsEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DDoggyFuckController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsBoobX.Value += mouseMove.x * 7f;
				c.ParamPhysicsBoobY.Value += mouseMove.y * 7f;
				c.ParamGrabBoobsTighten.Value = new Vector2(c.ParamPhysicsBoobX.Value, c.ParamPhysicsBoobY.Value).magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f);
			},
			HoldFixedUpdateAction = (Live2DDoggyFuckController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0003f);
			},
			EndLimbAction = (Live2DDoggyFuckController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobsX.EndParameterControl(c.ParamPhysicsBoobX);
				c.PhysicsSubRig_BoobsY.EndParameterControl(c.ParamPhysicsBoobY);
				c.ParamGrabBoobsEnable.SetMinValue();
			}
		};
		thisController.BoobFondleInteractable = new Live2DInteractableLimbUsage<Live2DDoggyFuckController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobs, limbUsageWithInteractable);
		thisController.AddInteractableHitArea(thisController.HitAreaBoobFondle, thisController.BoobFondleInteractable);
	}

	private void UpdateBoobs()
	{
		ParamFlattenNipples.Value = ((IsBraAsideOrOff && IsShirtAndHoodieAsideOrOff && !IsDress1On) ? 0f : 1f);
		ParamFlattenNipples.SetWeight(0.1f);
		ParamFlattenBoobs.Value = (IsShirtAndHoodieAsideOrOff ? 0f : 1f);
	}

	protected override void AfterUpdateTextures()
	{
		base.AfterUpdateTextures();
		UpdateBoobs();
		UpdateSkirt();
	}

	protected override MouthManagerDefaultMouth InitializeMouthImpl()
	{
		return MouthManagerDefaultMouth.Create(this);
	}

	protected override EyeManagerCommon InitializeEyesImpl()
	{
		return EyeManagerCommon.CreateNoBlinkRightEyeOnly(this);
	}

	public override Vector2 GetCockDirection()
	{
		return Vector2.right;
	}

	protected override void InitializeManagersInherited()
	{
		XRayManager = RegisterManager(XRayManagerCommon.CreateInstance(this));
		FrickStaticGuiManager = RegisterManager(new FrickStaticGuiManager(this));
		List<InsertionHandler> list = new List<InsertionHandler>();
		InsertionHandler insertionHandler = new InsertionHandler(this, InsertionHandlerOrificeTypeEnum.Vagina);
		VaginaCockFuckAxis = new BasicInsertionHandleFuckAxis(-0.14f, 0.05f, ParamThrust)
		{
			CockAngleXParameter = ParamVaginaCockAngle,
			BodyMovementParameterOverride = ParamPelvisX
		};
		insertionHandler.Blockages.Add(new InsertionHandlerBlock(-0.93f, () => !IsSkirtAsideOrOff));
		insertionHandler.Blockages.Add(new InsertionHandlerBlock(-0.53f, () => !IsPantsAsideOrOff));
		insertionHandler.Blockages.Add(new InsertionHandlerBlock(-0.5f, () => !IsDress2AsideOrOff));
		insertionHandler.Blockages.Add(new InsertionHandlerBlock(VaginaCockFuckAxis.InsideStart, () => !IsPantiesAsideOrOff));
		insertionHandler.AddFuckAxis(VaginaCockFuckAxis);
		insertionHandler.AddVisualizationPlane(new InsertionPlaneVisualizer(90f, 9, GetParamArrayToDirectlyControl("ParamXRayVagT", 9), GetParamArrayToDirectlyControl("ParamXRayVagB", 9))
		{
			XRayStartOffset = -0.05f
		});
		list.Add(insertionHandler);
		VaginaInsertionHandler = insertionHandler;
		InsertionHandler insertionHandler2 = new InsertionHandler(this, InsertionHandlerOrificeTypeEnum.Anal);
		AnalCockFuckAxis = new BasicInsertionHandleFuckAxis(-0.21f, 0f, ParamThrustAnal)
		{
			CockAngleXParameter = ParamAnalCockAngle,
			BodyMovementParameterOverride = ParamPelvisX
		};
		insertionHandler2.Blockages.Add(new InsertionHandlerBlock(-0.93f, () => !IsSkirtAsideOrOff));
		insertionHandler2.Blockages.Add(new InsertionHandlerBlock(-0.5f, () => !IsPantsAsideOrOff));
		insertionHandler2.Blockages.Add(new InsertionHandlerBlock(-0.5f, () => !IsDress2AsideOrOff));
		insertionHandler2.Blockages.Add(new InsertionHandlerBlock(AnalCockFuckAxis.InsideStart, () => !IsPantiesAsideOrOff));
		insertionHandler2.AddFuckAxis(AnalCockFuckAxis);
		insertionHandler2.AddVisualizationPlane(new InsertionPlaneVisualizer(90f, 9, GetParamArrayToDirectlyControl("ParamXRayAnalT", 9), GetParamArrayToDirectlyControl("ParamXRayAnalB", 9))
		{
			XRayStartOffset = -0.05f,
			XRayDistanceMultiplier = 0.9f
		});
		list.Add(insertionHandler2);
		AnalInsertionHandler = insertionHandler2;
		InsertionManager = RegisterManager(new InsertionManager(list));
		_cockFuckLimbUsage = new CockLimbUsage<Live2DDoggyFuckController>(this, ParamDick, new CockLimbUsage<Live2DDoggyFuckController>.CockLimbUsageTarget(VaginaInsertionHandler, VaginaCockFuckAxis), new CockLimbUsage<Live2DDoggyFuckController>.CockLimbUsageTarget(AnalInsertionHandler, AnalCockFuckAxis));
		CockManager = RegisterManager(new CockManagerCommon<Live2DDoggyFuckController>(this, _cockFuckLimbUsage));
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
					insertionHandler.ThrustMovementAccumulator += mouseMove.x * 10f;
					insertionHandler.SidewaysMovementAccumulator -= mouseMove.y * 40f;
				}
			},
			OnHeldFixed = null,
			OnHoldEnd = null,
			InfoSls = LOC.UI.TupleReferences.Interactable_FuckHer,
			ActiveFunc = () => !CockManager.IsFlacid
		});
		InitBoobInteractables(this);
		InitAssInteractable(this);
		InitClothesInteractables(this);
		InitHeadInteractable(this);
	}

	public override void InitializeGenerated()
	{
		ParamEyeballRX = GetParamToDirectlyControl("ParamEyeballRX");
		ParamEyeballRY = GetParamToDirectlyControl("ParamEyeballRY");
		ParamHeart = GetParamToDirectlyControl("ParamHeart");
		ParamIrisZoom = GetParamToDirectlyControl("ParamIrisZoom");
		ParamEyesHappy = GetParamToDirectlyControl("ParamEyesHappy");
		ParamBrowREmote = GetParamToDirectlyControl("ParamBrowREmote");
		ParamBrowRRot = GetParamToDirectlyControl("ParamBrowRRot");
		ParamEyeRShock = GetParamToDirectlyControl("ParamEyeRShock");
		ParamEyeRShock.SetWeight(0.1f);
		ParamBrowRY = GetParamToDirectlyControl("ParamBrowRY");
		ParamToungeX = GetParamToDirectlyControl("ParamToungeX");
		ParamToungeY = GetParamToDirectlyControl("ParamToungeY");
		ParamMouthForm = GetParamToDirectlyControl("ParamMouthForm");
		ParamMouthOpen = GetParamToDirectlyControl("ParamMouthOpen");
		ParamBlush = GetParamToDirectlyControl("ParamBlush");
		ParamHeadY = GetParamToDirectlyControl("ParamHeadY");
		ParamHeadY.SetWeight(10f);
		ParamEarR = GetParamToDirectlyControl("ParamEarR");
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
		ParamTense = GetParamToDirectlyControl("ParamTense");
		ParamOmegaMeter = GetParamToDirectlyControl("ParamOmegaMeter");
		ParamDick = GetParamToDirectlyControl("ParamDick");
		ParamXray = GetParamToDirectlyControl("ParamXray");
		ParamThrust = GetParamToDirectlyControl("ParamThrust");
		ParamCumInside = GetParamToDirectlyControl("ParamCumInside");
		ParamCumInsideWomb = GetParamToDirectlyControl("ParamCumInsideWomb");
		ParamThrustAnal = GetParamToDirectlyControl("ParamThrustAnal");
		ParamCumInsideAnal = GetParamToDirectlyControl("ParamCumInsideAnal");
		ParamFlacidity = GetParamToDirectlyControl("ParamFlacidity");
		ParamCumShotLengthY = GetParamToDirectlyControl("ParamCumShotLengthY");
		ParamCumOutsideY = GetParamToDirectlyControl("ParamCumOutsideY");
		ParamCumOutsideY.SetWeight(10f);
		ParamHideCumShot = GetParamToDirectlyControl("ParamHideCumShot");
		ParamCumShotAnim = GetParamToDirectlyControl("ParamCumShotAnim");
		ParamXRayAnalT0 = GetParamToDirectlyControl("ParamXRayAnalT0");
		ParamXRayAnalT1 = GetParamToDirectlyControl("ParamXRayAnalT1");
		ParamXRayAnalT2 = GetParamToDirectlyControl("ParamXRayAnalT2");
		ParamXRayAnalT3 = GetParamToDirectlyControl("ParamXRayAnalT3");
		ParamXRayAnalT4 = GetParamToDirectlyControl("ParamXRayAnalT4");
		ParamXRayAnalT5 = GetParamToDirectlyControl("ParamXRayAnalT5");
		ParamXRayAnalT6 = GetParamToDirectlyControl("ParamXRayAnalT6");
		ParamXRayAnalT7 = GetParamToDirectlyControl("ParamXRayAnalT7");
		ParamXRayAnalT8 = GetParamToDirectlyControl("ParamXRayAnalT8");
		ParamXRayAnalB0 = GetParamToDirectlyControl("ParamXRayAnalB0");
		ParamXRayAnalB1 = GetParamToDirectlyControl("ParamXRayAnalB1");
		ParamXRayAnalB2 = GetParamToDirectlyControl("ParamXRayAnalB2");
		ParamXRayAnalB3 = GetParamToDirectlyControl("ParamXRayAnalB3");
		ParamXRayAnalB4 = GetParamToDirectlyControl("ParamXRayAnalB4");
		ParamXRayAnalB5 = GetParamToDirectlyControl("ParamXRayAnalB5");
		ParamXRayAnalB6 = GetParamToDirectlyControl("ParamXRayAnalB6");
		ParamXRayAnalB7 = GetParamToDirectlyControl("ParamXRayAnalB7");
		ParamXRayAnalB8 = GetParamToDirectlyControl("ParamXRayAnalB8");
		ParamAnalCockAngle = GetParamToDirectlyControl("ParamAnalCockAngle");
		ParamXRayVagT0 = GetParamToDirectlyControl("ParamXRayVagT0");
		ParamXRayVagT1 = GetParamToDirectlyControl("ParamXRayVagT1");
		ParamXRayVagT2 = GetParamToDirectlyControl("ParamXRayVagT2");
		ParamXRayVagT3 = GetParamToDirectlyControl("ParamXRayVagT3");
		ParamXRayVagT4 = GetParamToDirectlyControl("ParamXRayVagT4");
		ParamXRayVagT5 = GetParamToDirectlyControl("ParamXRayVagT5");
		ParamXRayVagT6 = GetParamToDirectlyControl("ParamXRayVagT6");
		ParamXRayVagT7 = GetParamToDirectlyControl("ParamXRayVagT7");
		ParamXRayVagT8 = GetParamToDirectlyControl("ParamXRayVagT8");
		ParamXRayVagB0 = GetParamToDirectlyControl("ParamXRayVagB0");
		ParamXRayVagB1 = GetParamToDirectlyControl("ParamXRayVagB1");
		ParamXRayVagB2 = GetParamToDirectlyControl("ParamXRayVagB2");
		ParamXRayVagB3 = GetParamToDirectlyControl("ParamXRayVagB3");
		ParamXRayVagB4 = GetParamToDirectlyControl("ParamXRayVagB4");
		ParamXRayVagB5 = GetParamToDirectlyControl("ParamXRayVagB5");
		ParamXRayVagB6 = GetParamToDirectlyControl("ParamXRayVagB6");
		ParamXRayVagB7 = GetParamToDirectlyControl("ParamXRayVagB7");
		ParamXRayVagB8 = GetParamToDirectlyControl("ParamXRayVagB8");
		ParamVaginaCockAngle = GetParamToDirectlyControl("ParamVaginaCockAngle");
		ParamPelvisX = GetParamToDirectlyControl("ParamPelvisX");
		ParamOrgasm = GetParamToDirectlyControl("ParamOrgasm");
		ParamSkirtPullup = GetParamToDirectlyControl("ParamSkirtPullup");
		ParamDress2Up = GetParamToDirectlyControl("ParamDress2Up");
		ParamPantsDown = GetParamToDirectlyControl("ParamPantsDown");
		ParamFlattenBoobs = GetParamToDirectlyControl("ParamFlattenBoobs");
		ParamFlattenNipples = GetParamToDirectlyControl("ParamFlattenNipples");
		ParamMCGrabHeadEnable = GetParamToDirectlyControl("ParamMCGrabHeadEnable");
		ParamGrabBoobsEnable = GetParamToDirectlyControl("ParamGrabBoobsEnable");
		ParamGrabBoobsTighten = GetParamToDirectlyControl("ParamGrabBoobsTighten");
		ParamGrabButtEnable = GetParamToDirectlyControl("ParamGrabButtEnable");
		CumPhysics = GetParamToDirectlyControl("CumPhysics");
		ParamPhysicsButt = GetParamToDirectlyControl("ParamPhysicsButt");
		ParamHairPhysicsBaseToShort = GetParamToDirectlyControl("ParamHairPhysicsBaseToShort");
		ParamHairPhysicsShortToMid = GetParamToDirectlyControl("ParamHairPhysicsShortToMid");
		ParamHairPhysicsMidToLong = GetParamToDirectlyControl("ParamHairPhysicsMidToLong");
		ParamPhysicsArm = GetParamToDirectlyControl("ParamPhysicsArm");
		ParamPhysicsHead = GetParamToDirectlyControl("ParamPhysicsHead");
		ParamPhysicsBoobX = GetParamToDirectlyControl("ParamPhysicsBoobX");
		ParamPhysicsBoobY = GetParamToDirectlyControl("ParamPhysicsBoobY");
		ParamSkirtPhysics = GetParamToDirectlyControl("ParamSkirtPhysics");
		ParamAssSize = GetParamToDirectlyControl("ParamAssSize");
		ParamThighSize = GetParamToDirectlyControl("ParamThighSize");
		ParamCalfSize = GetParamToDirectlyControl("ParamCalfSize");
		ParamFootLength = GetParamToDirectlyControl("ParamFootLength");
		ParamFootWidth = GetParamToDirectlyControl("ParamFootWidth");
		ParamBoobSize = GetParamToDirectlyControl("ParamBoobSize");
		CubismPhysicsController component = Model.GetComponent<CubismPhysicsController>();
		PhysicsSubRig_ArmPhysics = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "ArmPhysics"));
		PhysicsSubRig_HairPhysics = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "HairPhysics"));
		PhysicsSubRig_HeadPhysics = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "HeadPhysics"));
		PhysicsSubRig_BoobsX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobsX"));
		PhysicsSubRig_BoobsY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobsY"));
		PhysicsSubRig_ButtPhysics = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "ButtPhysics"));
		PhysicsSubRig_Skirt = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Skirt"));
		PhysicsSubRig_Tail = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Tail"));
		PhysicsSubRig_CumPhysics = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "CumPhysics"));
		HitAreaBoobFondle = GetCachedPart("HitAreaBoobFondle");
		HitAreaAssGrab = GetCachedPart("HitAreaAssGrab");
		HitAreaHeadGrab = GetCachedPart("HitAreaHeadGrab");
		HitAreaDress2Down = GetCachedPart("HitAreaDress2Down");
		HitAreaDress2RolledUp = GetCachedPart("HitAreaDress2RolledUp");
		HitAreaSkirt = GetCachedPart("HitAreaSkirt");
		Part_DickSkin = GetCachedPart("DickSkin");
		Part_DickMain = GetCachedPart("DickMain");
		Part_AttachArmR = GetCachedPart("AttachArmR");
		Part_AttachArmRForearm = GetCachedPart("AttachArmRForearm");
		Part_AttachArmRHand = GetCachedPart("AttachArmRHand");
		Part_AttachArmLHand = GetCachedPart("AttachArmLHand");
		Part_OmegaMeterMask = GetCachedPart("OmegaMeterMask");
		Part_StockingR_foot = GetCachedPart("StockingR_foot");
		Part_StockingR_lower = GetCachedPart("StockingR_lower");
		Part_StockingR_thigh = GetCachedPart("StockingR_thigh");
		Part_Panties = GetCachedPart("Panties");
		Part_StockingL_foot = GetCachedPart("StockingL_foot");
		Part_StockingL_lower = GetCachedPart("StockingL_lower");
		Part_StockingL_thigh = GetCachedPart("StockingL_thigh");
		Part_BraUpper = GetCachedPart("BraUpper");
		Part_BraChest = GetCachedPart("BraChest");
		Part_BraLower = GetCachedPart("BraLower");
		Part_SkirtDownLower = GetCachedPart("SkirtDownLower");
		Part_SkirtDownBack = GetCachedPart("SkirtDownBack");
		Part_SkirtUpper = GetCachedPart("SkirtUpper");
		Part_SkirtDownBack2 = GetCachedPart("SkirtDownBack2");
		Part_SkirtUpper2 = GetCachedPart("SkirtUpper2");
		Part_ModdableBack = GetCachedPart("ModdableBack");
		Part_AltBPantsBeltDown = GetCachedPart("AltBPantsBeltDown");
		Part_BPantsCrotchDown = GetCachedPart("BPantsCrotchDown");
		Part_BPantsLowerFront = GetCachedPart("BPantsLowerFront");
		Part_BPantsThighFrontDown = GetCachedPart("BPantsThighFrontDown");
		Part_BPantsThighFront = GetCachedPart("BPantsThighFront");
		Part_AltBPantsBelt = GetCachedPart("AltBPantsBelt");
		Part_BPantsCrotch = GetCachedPart("BPantsCrotch");
		Part_BPantsLowerBack = GetCachedPart("BPantsLowerBack");
		Part_BPantsThighBackDown = GetCachedPart("BPantsThighBackDown");
		Part_BPantsThighBack = GetCachedPart("BPantsThighBack");
		Part_LongHoodieHood = GetCachedPart("LongHoodieHood");
		Part_LongHoodieSleeveFront = GetCachedPart("LongHoodieSleeveFront");
		Part_LongHoodieSleeveBack = GetCachedPart("LongHoodieSleeveBack");
		Part_LongHoodieChest = GetCachedPart("LongHoodieChest");
		Part_LongHoodieTorso = GetCachedPart("LongHoodieTorso");
		Part_AltLongHoodieStringFront = GetCachedPart("AltLongHoodieStringFront");
		Part_AltLongHoodieStringBack = GetCachedPart("AltLongHoodieStringBack");
		Part_PlainShirt_shoulder1_1 = GetCachedPart("PlainShirt_shoulder1_1");
		Part_PlainShirt_shoulder1_2 = GetCachedPart("PlainShirt_shoulder1_2");
		Part_ShadowPlainShirt_arm = GetCachedPart("ShadowPlainShirt_arm");
		Part_PlainShirt_shoulder2_1 = GetCachedPart("PlainShirt_shoulder2_1");
		Part_PlainShirt_shoulder2_2 = GetCachedPart("PlainShirt_shoulder2_2");
		Part_PlainShirt_chest = GetCachedPart("PlainShirt_chest");
		Part_PlainShirt_lower = GetCachedPart("PlainShirt_lower");
		Part_PlainShirt_main = GetCachedPart("PlainShirt_main");
		Part_XrayVaginaCover = GetCachedPart("XrayVaginaCover");
		Part_XrayVaginaWomb = GetCachedPart("XrayVaginaWomb");
		Part_XrayVaginaTop1 = GetCachedPart("XrayVaginaTop1");
		Part_XrayVaginaBottom = GetCachedPart("XrayVaginaBottom");
		Part_XrayVaginaBase = GetCachedPart("XrayVaginaBase");
		Part_CumWomb4 = GetCachedPart("CumWomb4");
		Part_CumWomb3 = GetCachedPart("CumWomb3");
		Part_CumWomb2 = GetCachedPart("CumWomb2");
		Part_CumWomb1 = GetCachedPart("CumWomb1");
		Part_XrayAnalCover = GetCachedPart("XrayAnalCover");
		Part_XrayAnalBot = GetCachedPart("XrayAnalBot");
		Part_XrayAnalTop = GetCachedPart("XrayAnalTop");
		Part_XrayAnalBase = GetCachedPart("XrayAnalBase");
		Part_Hologram = GetCachedPart("Hologram");
		Part_BrowR = GetCachedPart("BrowR");
		Part_SkinUpperEyelid = GetCachedPart("SkinUpperEyelid");
		Part_SkinLowerEyelid = GetCachedPart("SkinLowerEyelid");
		Part_HightlightEye = GetCachedPart("HightlightEye");
		Part_Iris2 = GetCachedPart("Iris2");
		Part_Iris1 = GetCachedPart("Iris1");
		Part_Pupil = GetCachedPart("Pupil");
		Part_SkinMouth = GetCachedPart("SkinMouth");
		Part_Tongue = GetCachedPart("Tongue");
		Part_SkinNeck = GetCachedPart("SkinNeck");
		Part_FaceLine2 = GetCachedPart("FaceLine2");
		Part_SkinLipUpper = GetCachedPart("SkinLipUpper");
		Part_TeethSharp = GetCachedPart("TeethSharp");
		Part_TeethNormal = GetCachedPart("TeethNormal");
		Part_SkinHeadBase = GetCachedPart("SkinHeadBase");
		Part_EyeWhite = GetCachedPart("EyeWhite");
		Part_NippleR = GetCachedPart("NippleR");
		Part_HighlightBoob = GetCachedPart("HighlightBoob");
		Part_SkinBoobFront = GetCachedPart("SkinBoobFront");
		Part_AttachLegRLower = GetCachedPart("AttachLegRLower");
		Part_AttachLegRFoot = GetCachedPart("AttachLegRFoot");
		Part_AttachLegR = GetCachedPart("AttachLegR");
		Part_AttachLegLLower = GetCachedPart("AttachLegLLower");
		Part_AttachLegLFoot = GetCachedPart("AttachLegLFoot");
		Part_AttachLegL = GetCachedPart("AttachLegL");
		Part_SkinMask = GetCachedPart("SkinMask");
		Part_SkinShoulderNoAtt = GetCachedPart("SkinShoulderNoAtt");
		Part_SkinArmUpperNoAttFront1 = GetCachedPart("SkinArmUpperNoAttFront1");
		Part_SkinShoulder = GetCachedPart("SkinShoulder");
		Part_SkinArmUpper_Front = GetCachedPart("SkinArmUpper_Front");
		Part_SkinRibcage = GetCachedPart("SkinRibcage");
		Part_SkinThighFront = GetCachedPart("SkinThighFront");
		Part_SkinButt = GetCachedPart("SkinButt");
		Part_SkinThighBack = GetCachedPart("SkinThighBack");
		Part_SkinPelvis = GetCachedPart("SkinPelvis");
		Part_AttachChargeHole = GetCachedPart("AttachChargeHole");
		Part_GlowOmegaMeterInner = GetCachedPart("GlowOmegaMeterInner");
		Part_GlowOmegameter = GetCachedPart("GlowOmegameter");
		Part_SkinWaistLower = GetCachedPart("SkinWaistLower");
		Part_SkinWaistUpper = GetCachedPart("SkinWaistUpper");
		Part_ModdableHairBack = GetCachedPart("ModdableHairBack");
		Part_ModdableHairFront = GetCachedPart("ModdableHairFront");
		Part_ModdableFace = GetCachedPart("ModdableFace");
		Part_ModdableShirtLogo = GetCachedPart("ModdableShirtLogo");
		Part_ModdableShirtRightSleeveLogo = GetCachedPart("ModdableShirtRightSleeveLogo");
		Part_ModdableHoodieLogo = GetCachedPart("ModdableHoodieLogo");
		Part_TailMain = GetCachedPart("TailMain");
		Part_TailPlug = GetCachedPart("TailPlug");
		Part_TailMain2 = GetCachedPart("TailMain2");
		Part_TailMain3 = GetCachedPart("TailMain3");
		Part_TailMain4 = GetCachedPart("TailMain4");
		Part_TailMain5 = GetCachedPart("TailMain5");
		Part_TailMain6 = GetCachedPart("TailMain6");
		Part_TailMain7 = GetCachedPart("TailMain7");
		Part_TailMain8 = GetCachedPart("TailMain8");
		Part_TailMain9 = GetCachedPart("TailMain9");
		Part_TailMain10 = GetCachedPart("TailMain10");
		Part_CumOutsideAss = GetCachedPart("CumOutsideAss");
		Part_CumOutsideThighBack = GetCachedPart("CumOutsideThighBack");
		Part_CumOutsideAssSide = GetCachedPart("CumOutsideAssSide");
		Part_CumOutsideAssUp = GetCachedPart("CumOutsideAssUp");
		Part_CumOutsideBoobRSide = GetCachedPart("CumOutsideBoobRSide");
		Part_CumOutsideBoobR = GetCachedPart("CumOutsideBoobR");
		Part_CumOutsideBelly = GetCachedPart("CumOutsideBelly");
		Part_Blush1 = GetCachedPart("Blush1");
		Part_Blush3 = GetCachedPart("Blush3");
		Part_Dress2BottomDown = GetCachedPart("Dress2BottomDown");
		Part_Dress2BottomRolledUp = GetCachedPart("Dress2BottomRolledUp");
		Part_Dress2Sleeve = GetCachedPart("Dress2Sleeve");
		Part_Dress2Chest = GetCachedPart("Dress2Chest");
		Part_Dress2AltRib = GetCachedPart("Dress2AltRib");
		Part_Dress2Waist = GetCachedPart("Dress2Waist");
		Part_CumDick2 = GetCachedPart("CumDick2");
		Part_CumDick1 = GetCachedPart("CumDick1");
		Part_CumAnalNew = GetCachedPart("CumAnalNew");
		Part_H0_HairTie = GetCachedPart("H0_HairTie");
		Part_H0_Braid_Lower = GetCachedPart("H0_Braid_Lower");
		Part_H0_Braid_1 = GetCachedPart("H0_Braid_1");
		Part_H0_Braid_2 = GetCachedPart("H0_Braid_2");
		Part_H0_Front_Bang = GetCachedPart("H0_Front_Bang");
		Part_H0_Top_Leaf1 = GetCachedPart("H0_Top_Leaf1");
		Part_H0_Top_Leaf2 = GetCachedPart("H0_Top_Leaf2");
		Part_H0_Back_1 = GetCachedPart("H0_Back_1");
		Part_H0_Back_2 = GetCachedPart("H0_Back_2");
		Part_H2_Front_Bang = GetCachedPart("H2_Front_Bang");
		Part_H1_SideRight = GetCachedPart("H1_SideRight");
		Part_H1_Front_Bang = GetCachedPart("H1_Front_Bang");
		Part_H1_Top_Ahoge = GetCachedPart("H1_Top_Ahoge");
		Part_H1_Back = GetCachedPart("H1_Back");
		Part_H1_Side_L = GetCachedPart("H1_Side_L");
		Part_H3_Front = GetCachedPart("H3_Front");
		Part_H3_Side_Right = GetCachedPart("H3_Side_Right");
		Part_H3_Back = GetCachedPart("H3_Back");
		Part_H3_Side_Left = GetCachedPart("H3_Side_Left");
		Part_H4_Knot = GetCachedPart("H4_Knot");
		Part_H4_Tail_Front = GetCachedPart("H4_Tail_Front");
		Part_H4_Tail_Back = GetCachedPart("H4_Tail_Back");
		Part_H4_Tail_FrontTop = GetCachedPart("H4_Tail_FrontTop");
		Part_CumVaginalNew = GetCachedPart("CumVaginalNew");
		Part_MCHandHeadGrab = GetCachedPart("MCHandHeadGrab");
		Part_MCHandBoobs = GetCachedPart("MCHandBoobs");
		Part_MCHandGrabButt = GetCachedPart("MCHandGrabButt");
		Part_Dress1_Bottom = GetCachedPart("Dress1_Bottom");
		Part_Dress1_Waist = GetCachedPart("Dress1_Waist");
		Part_Dress1_Chest = GetCachedPart("Dress1_Chest");
		Part_Dress1_Ribcage = GetCachedPart("Dress1_Ribcage");
		Part_CumOutsideAssSide2 = GetCachedPart("CumOutsideAssSide2");
		Part_CumOutsideAssBottom = GetCachedPart("CumOutsideAssBottom");
		Part_CumOutsideAss2 = GetCachedPart("CumOutsideAss2");
		Part_CumOutsideAssUp2 = GetCachedPart("CumOutsideAssUp2");
		Part_H1_Side_Middle = GetCachedPart("H1_Side_Middle");
		Part_NipPiercingRight = GetCachedPart("NipPiercingRight");
		Part_Headband = GetCachedPart("Headband");
		Part_PointyEarR = GetCachedPart("PointyEarR");
		Part_WizardHat = GetCachedPart("WizardHat");
		Part_BellChokerMain = GetCachedPart("BellChokerMain");
		Part_BellChokerBell = GetCachedPart("BellChokerBell");
		Part_CatearBackR = GetCachedPart("CatearBackR");
		Part_CatearMidR = GetCachedPart("CatearMidR");
		Part_CatearFrontL = GetCachedPart("CatearFrontL");
		Part_BikiniTop_A_BackRibbon = GetCachedPart("BikiniTop_A_BackRibbon");
		Part_BikiniTop_A_BackString = GetCachedPart("BikiniTop_A_BackString");
		Part_BikiniTop_A_CupOutlineBottom = GetCachedPart("BikiniTop_A_CupOutlineBottom");
		Part_BikiniTop_A_CupOutline = GetCachedPart("BikiniTop_A_CupOutline");
		Part_BikiniTop_M_Cup = GetCachedPart("BikiniTop_M_Cup");
		Part_BikiniBot_A_Ribbon = GetCachedPart("BikiniBot_A_Ribbon");
		Part_BikiniBot_A_String = GetCachedPart("BikiniBot_A_String");
		Part_H0_Side_BangL = GetCachedPart("H0_Side_BangL");
		Part_H0_Side_BangR2 = GetCachedPart("H0_Side_BangR2");
		Part_H0_FrontClippedUp_Bang = GetCachedPart("H0_FrontClippedUp_Bang");
		Part_H0_FrontClippedUp_HairClip = GetCachedPart("H0_FrontClippedUp_HairClip");
		coloringGroups[ColoringGroupEnum.Hair0_Top] = new ColoringGroup(ColoringGroupEnum.Hair0_Top, new List<Part> { Part_H0_Top_Leaf1, Part_H0_Top_Leaf2 });
		coloringGroups[ColoringGroupEnum.Hair0_Back] = new ColoringGroup(ColoringGroupEnum.Hair0_Back, new List<Part> { Part_H0_Back_1, Part_H0_Back_2 });
		coloringGroups[ColoringGroupEnum.Hair0_Braid] = new ColoringGroup(ColoringGroupEnum.Hair0_Braid, new List<Part> { Part_H0_Braid_Lower, Part_H0_Braid_1, Part_H0_Braid_2 });
		coloringGroups[ColoringGroupEnum.Hair0_Braid_HairTieAccessory] = new ColoringGroup(ColoringGroupEnum.Hair0_Braid_HairTieAccessory, new List<Part> { Part_H0_HairTie });
		coloringGroups[ColoringGroupEnum.Hair0_Side] = new ColoringGroup(ColoringGroupEnum.Hair0_Side, new List<Part> { Part_H0_Side_BangL, Part_H0_Side_BangR2 });
		coloringGroups[ColoringGroupEnum.Hair0_SideAlt] = new ColoringGroup(ColoringGroupEnum.Hair0_SideAlt, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair0_Front] = new ColoringGroup(ColoringGroupEnum.Hair0_Front, new List<Part> { Part_H0_Front_Bang });
		coloringGroups[ColoringGroupEnum.Hair0_FrontClippedUp_Bang] = new ColoringGroup(ColoringGroupEnum.Hair0_FrontClippedUp_Bang, new List<Part> { Part_H0_FrontClippedUp_Bang });
		coloringGroups[ColoringGroupEnum.Hair0_FrontClippedUp_HairClip] = new ColoringGroup(ColoringGroupEnum.Hair0_FrontClippedUp_HairClip, new List<Part> { Part_H0_FrontClippedUp_HairClip });
		coloringGroups[ColoringGroupEnum.Hair1_Top] = new ColoringGroup(ColoringGroupEnum.Hair1_Top, new List<Part> { Part_H1_Top_Ahoge });
		coloringGroups[ColoringGroupEnum.Hair1_Front] = new ColoringGroup(ColoringGroupEnum.Hair1_Front, new List<Part> { Part_H1_Front_Bang });
		coloringGroups[ColoringGroupEnum.Hair1_Side] = new ColoringGroup(ColoringGroupEnum.Hair1_Side, new List<Part> { Part_H1_SideRight, Part_H1_Side_L, Part_H1_Side_Middle });
		coloringGroups[ColoringGroupEnum.Hair1_Back] = new ColoringGroup(ColoringGroupEnum.Hair1_Back, new List<Part> { Part_H1_Back });
		coloringGroups[ColoringGroupEnum.Hair2_Front] = new ColoringGroup(ColoringGroupEnum.Hair2_Front, new List<Part> { Part_H2_Front_Bang });
		coloringGroups[ColoringGroupEnum.Hair3_Front] = new ColoringGroup(ColoringGroupEnum.Hair3_Front, new List<Part> { Part_H3_Front });
		coloringGroups[ColoringGroupEnum.Hair3_Side] = new ColoringGroup(ColoringGroupEnum.Hair3_Side, new List<Part> { Part_H3_Side_Right, Part_H3_Side_Left });
		coloringGroups[ColoringGroupEnum.Hair3_Top] = new ColoringGroup(ColoringGroupEnum.Hair3_Top, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair3_Back] = new ColoringGroup(ColoringGroupEnum.Hair3_Back, new List<Part> { Part_H3_Back });
		coloringGroups[ColoringGroupEnum.Hair4_Ponytail] = new ColoringGroup(ColoringGroupEnum.Hair4_Ponytail, new List<Part> { Part_H4_Tail_Front, Part_H4_Tail_Back, Part_H4_Tail_FrontTop });
		coloringGroups[ColoringGroupEnum.Hair4_KnotAccessory] = new ColoringGroup(ColoringGroupEnum.Hair4_KnotAccessory, new List<Part> { Part_H4_Knot });
		coloringGroups[ColoringGroupEnum.Skirt] = new ColoringGroup(ColoringGroupEnum.Skirt, new List<Part> { Part_SkirtDownLower, Part_SkirtDownBack, Part_SkirtUpper, Part_SkirtDownBack2, Part_SkirtUpper2 });
		coloringGroups[ColoringGroupEnum.Shirt] = new ColoringGroup(ColoringGroupEnum.Shirt, new List<Part> { Part_PlainShirt_shoulder1_1, Part_PlainShirt_shoulder1_2, Part_PlainShirt_shoulder2_1, Part_PlainShirt_shoulder2_2, Part_PlainShirt_chest, Part_PlainShirt_lower, Part_PlainShirt_main });
		coloringGroups[ColoringGroupEnum.ShirtShadow] = new ColoringGroup(ColoringGroupEnum.ShirtShadow, new List<Part> { Part_ShadowPlainShirt_arm });
		coloringGroups[ColoringGroupEnum.StockingL] = new ColoringGroup(ColoringGroupEnum.StockingL, new List<Part> { Part_StockingL_foot, Part_StockingL_lower, Part_StockingL_thigh });
		coloringGroups[ColoringGroupEnum.StockingR] = new ColoringGroup(ColoringGroupEnum.StockingR, new List<Part> { Part_StockingR_foot, Part_StockingR_lower, Part_StockingR_thigh });
		coloringGroups[ColoringGroupEnum.Panties] = new ColoringGroup(ColoringGroupEnum.Panties, new List<Part> { Part_Panties });
		coloringGroups[ColoringGroupEnum.Bra] = new ColoringGroup(ColoringGroupEnum.Bra, new List<Part> { Part_BraUpper, Part_BraChest, Part_BraLower });
		coloringGroups[ColoringGroupEnum.Hoodie] = new ColoringGroup(ColoringGroupEnum.Hoodie, new List<Part> { Part_LongHoodieHood, Part_LongHoodieSleeveFront, Part_LongHoodieSleeveBack, Part_LongHoodieChest, Part_LongHoodieTorso });
		coloringGroups[ColoringGroupEnum.HoodieStrings] = new ColoringGroup(ColoringGroupEnum.HoodieStrings, new List<Part> { Part_AltLongHoodieStringFront, Part_AltLongHoodieStringBack });
		coloringGroups[ColoringGroupEnum.Pants] = new ColoringGroup(ColoringGroupEnum.Pants, new List<Part> { Part_BPantsCrotchDown, Part_BPantsLowerFront, Part_BPantsThighFrontDown, Part_BPantsThighFront, Part_BPantsCrotch, Part_BPantsLowerBack, Part_BPantsThighBackDown, Part_BPantsThighBack });
		coloringGroups[ColoringGroupEnum.PantsBelt] = new ColoringGroup(ColoringGroupEnum.PantsBelt, new List<Part> { Part_AltBPantsBeltDown, Part_AltBPantsBelt });
		coloringGroups[ColoringGroupEnum.Dress1] = new ColoringGroup(ColoringGroupEnum.Dress1, new List<Part> { Part_Dress1_Bottom, Part_Dress1_Waist, Part_Dress1_Chest, Part_Dress1_Ribcage });
		coloringGroups[ColoringGroupEnum.Dress2Alt] = new ColoringGroup(ColoringGroupEnum.Dress2Alt, new List<Part> { Part_Dress2AltRib });
		coloringGroups[ColoringGroupEnum.Dress2] = new ColoringGroup(ColoringGroupEnum.Dress2, new List<Part> { Part_Dress2BottomDown, Part_Dress2BottomRolledUp, Part_Dress2Sleeve, Part_Dress2Chest, Part_Dress2Waist });
		coloringGroups[ColoringGroupEnum.ShoeR] = new ColoringGroup(ColoringGroupEnum.ShoeR, new List<Part>());
		coloringGroups[ColoringGroupEnum.ShoeL] = new ColoringGroup(ColoringGroupEnum.ShoeL, new List<Part>());
		coloringGroups[ColoringGroupEnum.PointyEars] = new ColoringGroup(ColoringGroupEnum.PointyEars, new List<Part> { Part_PointyEarR });
		coloringGroups[ColoringGroupEnum.WizardHat] = new ColoringGroup(ColoringGroupEnum.WizardHat, new List<Part> { Part_WizardHat });
		coloringGroups[ColoringGroupEnum.BellChoker] = new ColoringGroup(ColoringGroupEnum.BellChoker, new List<Part> { Part_BellChokerMain, Part_BellChokerBell });
		coloringGroups[ColoringGroupEnum.CuteBow] = new ColoringGroup(ColoringGroupEnum.CuteBow, new List<Part>());
		coloringGroups[ColoringGroupEnum.PumpkinHairPin] = new ColoringGroup(ColoringGroupEnum.PumpkinHairPin, new List<Part>());
		coloringGroups[ColoringGroupEnum.Headband] = new ColoringGroup(ColoringGroupEnum.Headband, new List<Part> { Part_Headband });
		coloringGroups[ColoringGroupEnum.NipplePiercingL] = new ColoringGroup(ColoringGroupEnum.NipplePiercingL, new List<Part>());
		coloringGroups[ColoringGroupEnum.NipplePiercingR] = new ColoringGroup(ColoringGroupEnum.NipplePiercingR, new List<Part> { Part_NipPiercingRight });
		coloringGroups[ColoringGroupEnum.NavelPiercing] = new ColoringGroup(ColoringGroupEnum.NavelPiercing, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniTop_Main] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Main, new List<Part> { Part_BikiniTop_M_Cup });
		coloringGroups[ColoringGroupEnum.BikiniTop_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Alt, new List<Part> { Part_BikiniTop_A_BackRibbon, Part_BikiniTop_A_BackString, Part_BikiniTop_A_CupOutlineBottom, Part_BikiniTop_A_CupOutline });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Main] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Main, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniBottom_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Alt, new List<Part> { Part_BikiniBot_A_Ribbon, Part_BikiniBot_A_String });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Uncolored] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Uncolored, new List<Part>());
		coloringGroups[ColoringGroupEnum.IrisL] = new ColoringGroup(ColoringGroupEnum.IrisL, new List<Part>());
		coloringGroups[ColoringGroupEnum.IrisR] = new ColoringGroup(ColoringGroupEnum.IrisR, new List<Part> { Part_Pupil });
		coloringGroups[ColoringGroupEnum.PupilL] = new ColoringGroup(ColoringGroupEnum.PupilL, new List<Part>());
		coloringGroups[ColoringGroupEnum.PupilR] = new ColoringGroup(ColoringGroupEnum.PupilR, new List<Part> { Part_Iris1 });
		coloringGroups[ColoringGroupEnum.Sclera] = new ColoringGroup(ColoringGroupEnum.Sclera, new List<Part> { Part_EyeWhite });
		coloringGroups[ColoringGroupEnum.Eyelash] = new ColoringGroup(ColoringGroupEnum.Eyelash, new List<Part>());
		coloringGroups[ColoringGroupEnum.HeartPupils] = new ColoringGroup(ColoringGroupEnum.HeartPupils, new List<Part> { Part_Iris2 });
		coloringGroups[ColoringGroupEnum.EyeBrows] = new ColoringGroup(ColoringGroupEnum.EyeBrows, new List<Part> { Part_BrowR });
		coloringGroups[ColoringGroupEnum.Nipples] = new ColoringGroup(ColoringGroupEnum.Nipples, new List<Part> { Part_NippleR });
		coloringGroups[ColoringGroupEnum.Vagina] = new ColoringGroup(ColoringGroupEnum.Vagina, new List<Part>());
		coloringGroups[ColoringGroupEnum.Xray] = new ColoringGroup(ColoringGroupEnum.Xray, new List<Part> { Part_XrayVaginaCover, Part_XrayVaginaWomb, Part_XrayVaginaTop1, Part_XrayVaginaBottom, Part_XrayVaginaBase, Part_XrayAnalCover, Part_XrayAnalBot, Part_XrayAnalTop, Part_XrayAnalBase });
		coloringGroups[ColoringGroupEnum.Anus] = new ColoringGroup(ColoringGroupEnum.Anus, new List<Part>());
		coloringGroups[ColoringGroupEnum.Teeth] = new ColoringGroup(ColoringGroupEnum.Teeth, new List<Part> { Part_TeethSharp, Part_TeethNormal });
		coloringGroups[ColoringGroupEnum.Mouth] = new ColoringGroup(ColoringGroupEnum.Mouth, new List<Part> { Part_Tongue });
		coloringGroups[ColoringGroupEnum.Boobs] = new ColoringGroup(ColoringGroupEnum.Boobs, new List<Part> { Part_SkinBoobFront });
		coloringGroups[ColoringGroupEnum.Ass] = new ColoringGroup(ColoringGroupEnum.Ass, new List<Part> { Part_SkinButt });
		coloringGroups[ColoringGroupEnum.Pelvic] = new ColoringGroup(ColoringGroupEnum.Pelvic, new List<Part> { Part_SkinPelvis });
		coloringGroups[ColoringGroupEnum.Thigh] = new ColoringGroup(ColoringGroupEnum.Thigh, new List<Part>());
		coloringGroups[ColoringGroupEnum.Tummy] = new ColoringGroup(ColoringGroupEnum.Tummy, new List<Part> { Part_SkinWaistLower });
		coloringGroups[ColoringGroupEnum.Skin] = new ColoringGroup(ColoringGroupEnum.Skin, new List<Part>
		{
			Part_SkinUpperEyelid, Part_SkinLowerEyelid, Part_SkinMouth, Part_SkinNeck, Part_SkinLipUpper, Part_SkinHeadBase, Part_SkinMask, Part_SkinShoulderNoAtt, Part_SkinArmUpperNoAttFront1, Part_SkinShoulder,
			Part_SkinArmUpper_Front, Part_SkinRibcage, Part_SkinThighFront, Part_SkinThighBack, Part_SkinWaistUpper
		});
		coloringGroups[ColoringGroupEnum.Blush] = new ColoringGroup(ColoringGroupEnum.Blush, new List<Part> { Part_Blush1, Part_Blush3 });
		coloringGroups[ColoringGroupEnum.SharpTeeth] = new ColoringGroup(ColoringGroupEnum.SharpTeeth, new List<Part>());
		coloringGroups[ColoringGroupEnum.CatEarMain] = new ColoringGroup(ColoringGroupEnum.CatEarMain, new List<Part> { Part_CatearBackR, Part_CatearFrontL });
		coloringGroups[ColoringGroupEnum.CatEarSecondary] = new ColoringGroup(ColoringGroupEnum.CatEarSecondary, new List<Part> { Part_CatearMidR });
		coloringGroups[ColoringGroupEnum.LegLFoot] = new ColoringGroup(ColoringGroupEnum.LegLFoot, new List<Part> { Part_AttachLegLFoot });
		coloringGroups[ColoringGroupEnum.LegL] = new ColoringGroup(ColoringGroupEnum.LegL, new List<Part> { Part_AttachLegLLower, Part_AttachLegL });
		coloringGroups[ColoringGroupEnum.LegRFoot] = new ColoringGroup(ColoringGroupEnum.LegRFoot, new List<Part> { Part_AttachLegRFoot });
		coloringGroups[ColoringGroupEnum.LegR] = new ColoringGroup(ColoringGroupEnum.LegR, new List<Part> { Part_AttachLegRLower, Part_AttachLegR });
		coloringGroups[ColoringGroupEnum.ArmL] = new ColoringGroup(ColoringGroupEnum.ArmL, new List<Part> { Part_AttachArmLHand });
		coloringGroups[ColoringGroupEnum.ArmR] = new ColoringGroup(ColoringGroupEnum.ArmR, new List<Part> { Part_AttachArmR, Part_AttachArmRForearm, Part_AttachArmRHand });
		coloringGroups[ColoringGroupEnum.Tail] = new ColoringGroup(ColoringGroupEnum.Tail, new List<Part> { Part_TailMain, Part_TailMain2, Part_TailMain3, Part_TailMain4, Part_TailMain5, Part_TailMain6, Part_TailMain7, Part_TailMain8, Part_TailMain9, Part_TailMain10 });
		coloringGroups[ColoringGroupEnum.TailPlug] = new ColoringGroup(ColoringGroupEnum.TailPlug, new List<Part> { Part_TailPlug });
		coloringGroups[ColoringGroupEnum.Hologram] = new ColoringGroup(ColoringGroupEnum.Hologram, new List<Part> { Part_Hologram });
		coloringGroups[ColoringGroupEnum.OtherAttachments] = new ColoringGroup(ColoringGroupEnum.OtherAttachments, new List<Part> { Part_AttachChargeHole });
		coloringGroups[ColoringGroupEnum.MCSkin] = new ColoringGroup(ColoringGroupEnum.MCSkin, new List<Part> { Part_MCHandHeadGrab, Part_MCHandBoobs, Part_MCHandGrabButt });
		coloringGroups[ColoringGroupEnum.MCCockSkin] = new ColoringGroup(ColoringGroupEnum.MCCockSkin, new List<Part> { Part_DickSkin });
		coloringGroups[ColoringGroupEnum.MCCockTip] = new ColoringGroup(ColoringGroupEnum.MCCockTip, new List<Part> { Part_DickMain });
		coloringGroups[ColoringGroupEnum.Shadow] = new ColoringGroup(ColoringGroupEnum.Shadow, new List<Part>());
		coloringGroups[ColoringGroupEnum.Highlight] = new ColoringGroup(ColoringGroupEnum.Highlight, new List<Part> { Part_HighlightBoob });
		coloringGroups[ColoringGroupEnum.Mask] = new ColoringGroup(ColoringGroupEnum.Mask, new List<Part> { Part_OmegaMeterMask });
		coloringGroups[ColoringGroupEnum.Moddable] = new ColoringGroup(ColoringGroupEnum.Moddable, new List<Part> { Part_ModdableBack, Part_ModdableHairBack, Part_ModdableHairFront, Part_ModdableFace, Part_ModdableShirtLogo, Part_ModdableShirtRightSleeveLogo, Part_ModdableHoodieLogo });
		coloringGroups[ColoringGroupEnum.HitArea] = new ColoringGroup(ColoringGroupEnum.HitArea, new List<Part> { HitAreaBoobFondle, HitAreaAssGrab, HitAreaHeadGrab, HitAreaDress2Down, HitAreaDress2RolledUp, HitAreaSkirt });
		coloringGroups[ColoringGroupEnum.Cum] = new ColoringGroup(ColoringGroupEnum.Cum, new List<Part>
		{
			Part_CumWomb4, Part_CumWomb3, Part_CumWomb2, Part_CumWomb1, Part_CumOutsideAss, Part_CumOutsideThighBack, Part_CumOutsideAssSide, Part_CumOutsideAssUp, Part_CumOutsideBoobRSide, Part_CumOutsideBoobR,
			Part_CumOutsideBelly, Part_CumDick2, Part_CumDick1, Part_CumAnalNew, Part_CumVaginalNew, Part_CumOutsideAssSide2, Part_CumOutsideAssBottom, Part_CumOutsideAss2, Part_CumOutsideAssUp2
		});
		coloringGroups[ColoringGroupEnum.Rest] = new ColoringGroup(ColoringGroupEnum.Rest, new List<Part> { Part_HightlightEye, Part_FaceLine2, Part_GlowOmegaMeterInner, Part_GlowOmegameter });
	}
}
