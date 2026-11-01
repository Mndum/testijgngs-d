using System;
using System.Collections.Generic;
using System.Linq;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Physics;
using UnityEngine;

public sealed class Live2DInteractionController : Live2DController, ILive2DController_HeadAngles, ILive2DController_HeadX, ILive2DController_HeadY, ILive2DController_HeadZ, ILive2DController_BodyAngles, ILive2DController_BodyX, ILive2DController_BodyY, ILive2DController_CatEarsMovement, ILive2DController_Headpat, ILive2DController_TongueMovement, ILive2DController_CommonFace, ILive2DController_MainFace, ILive2DController_Mouth, ILive2DController_EyeMovement, ILive2DController_EyeLMovement, ILive2DController_EyeRMovement, ILive2DController_EyeLEmotes, ILive2DController_EyeLCloseAndShock, ILive2DController_BrowLY, ILive2DController_BrowLRot, ILive2DController_BrowLEmote, ILive2DController_EyeREmotes, ILive2DController_EyeRCloseAndShock, ILive2DController_BrowRY, ILive2DController_BrowRRot, ILive2DController_BrowREmote, ILive2DController_BlushAndHeartEyes, ILive2DController_IndependentBlinking, ILive2DController_EyesHappinessEmote, ILive2DController_SalivaMouth, ILive2DController_OmegaMeter, ILive2DController_TailWiggle, ILive2DController_BodyMod_AllLegs, ILive2DController_BodyMod_ThighSize, ILive2DController_BodyMod_CalfSize, ILive2DController_BodyMod_FootSize, ILive2DController_BodyMod_BoobSize, ILive2DController_BodyMod_AssSize
{
	public enum ArmState
	{
		UpPoint,
		UpHi,
		UpLecture,
		DownNormal,
		DownClenched
	}

	public enum ArmSelection
	{
		Left,
		Right,
		Both
	}

	public enum ArmLCuddleState
	{
		Down,
		Normal,
		Up
	}

	public class StoryInteractionArmsSlot : IStoryEmoteSlot
	{
		public const string AreaArmL = "ArmL";

		public const string AreaArmR = "ArmR";

		public const string AreaArmBoth = "ArmBoth";

		private Live2DInteractionController _controller;

		public StoryInteractionArmsSlot(Live2DInteractionController controller)
		{
			_controller = controller;
		}

		public bool IsTargetSlot(string slotName)
		{
			if (!string.Equals("ArmL", slotName, StringComparison.OrdinalIgnoreCase) && !string.Equals("ArmR", slotName, StringComparison.OrdinalIgnoreCase))
			{
				return string.Equals("ArmBoth", slotName, StringComparison.OrdinalIgnoreCase);
			}
			return true;
		}

		public IEnumerable<string> GetItemId()
		{
			return from ArmState t in Enum.GetValues(typeof(ArmState))
				select t.ToString();
		}

		public IEnumerable<string> GetValidNames()
		{
			return new string[3] { "ArmL", "ArmR", "ArmBoth" };
		}

		public Result IsValidEmote(EmoteData emoteData)
		{
			if (Enum.TryParse<ArmState>(emoteData.ItemId, ignoreCase: true, out var _))
			{
				return Result.Success;
			}
			return Result.FailWithDescription("Emote item " + emoteData.ItemId + " not found in slot " + emoteData.SlotId + "! \nAvailable items: " + string.Join(", ", GetItemId()));
		}

		public void SetDefault()
		{
			SetItem(new EmoteData("ArmBoth", ArmState.DownNormal.ToString()));
		}

		public void SetItem(EmoteData emote)
		{
			ArmSelection selection;
			switch (emote.SlotId)
			{
			default:
				return;
			case "ArmL":
				selection = ArmSelection.Left;
				break;
			case "ArmR":
				selection = ArmSelection.Right;
				break;
			case "ArmBoth":
				selection = ArmSelection.Both;
				break;
			}
			if (Enum.TryParse<ArmState>(emote.ItemId, ignoreCase: true, out var result))
			{
				_controller.SetArmState(result, selection);
			}
			else
			{
				Debug.LogError(emote.ItemId + " cannot be interpreted as ArmState!");
			}
		}
	}

	public enum InteractionSceneTypeEnum
	{
		Normal,
		Cuddle
	}

	public override ControllerEnum ControllerEnum => ControllerEnum.Interaction;

	public override SceneEnumFlag SceneEnum
	{
		get
		{
			if (InteractionSceneType != InteractionSceneTypeEnum.Normal)
			{
				return SceneEnumFlag.InteractionCuddle;
			}
			return SceneEnumFlag.Interaction;
		}
	}

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

	public override bool IsSkirtAside => ParamSkirtUp.Value > 0.5f;

	public ArmLCuddleState ArmLCuddleLState { get; private set; } = ArmLCuddleState.Normal;

	public ArmState ArmLState { get; private set; } = ArmState.DownNormal;

	public ArmState ArmRState { get; private set; } = ArmState.DownNormal;

	public Live2DPhysicsOverride HeadToChestPhysicsOverride { get; private set; }

	public InteractionSceneTypeEnum InteractionSceneType { get; set; }

	public override Vector2 DefaultEyesPosition
	{
		get
		{
			if (InteractionSceneType != InteractionSceneTypeEnum.Normal)
			{
				return new Vector2(0.6f, -0.4f);
			}
			return Vector2.zero;
		}
	}

	public override Vector3 DefaultFaceAngle
	{
		get
		{
			if (InteractionSceneType != InteractionSceneTypeEnum.Normal)
			{
				return new Vector3(-1f, 0f, 0.7f);
			}
			return Vector3.zero;
		}
	}

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

	public Live2DParameter ParamEnableThumbMouth { get; private set; }

	public Live2DParameter ParamThumbMouthX { get; private set; }

	public Live2DParameter ParamBlush { get; private set; }

	public Live2DParameter ParamHeadZ { get; private set; }

	public Live2DParameter ParamHeadX { get; private set; }

	public Live2DParameter ParamHeadY { get; private set; }

	public Live2DParameter ParamEarL { get; private set; }

	public Live2DParameter ParamEarR { get; private set; }

	public Live2DParameter ParamArmLRot { get; private set; }

	public Live2DParameter ParamArmLUp { get; private set; }

	public Live2DParameter ParamArmLGesture { get; private set; }

	public Live2DParameter ParamHandholdingREnable { get; private set; }

	public Live2DParameter ParamHandholdingRY { get; private set; }

	public Live2DParameter ParamArmRRot { get; private set; }

	public Live2DParameter ParamArmRUp { get; private set; }

	public Live2DParameter ParamArmRGesture { get; private set; }

	public Live2DParameter ParamHandholdingLEnable { get; private set; }

	public Live2DParameter ParamHandholdingLY { get; private set; }

	public Live2DParameter ParamBodyZ { get; private set; }

	public Live2DParameter ParamBodyX { get; private set; }

	public Live2DParameter ParamBodyY { get; private set; }

	public Live2DParameter ParamLegR { get; private set; }

	public Live2DParameter ParamLegL { get; private set; }

	public Live2DParameter ParamChestX { get; private set; }

	public Live2DParameter ParamBodyAccessoryPhysics { get; private set; }

	public Live2DParameter ParamHairPhysicsBaseToShort { get; private set; }

	public Live2DParameter ParamHairPhysicsShortToMid { get; private set; }

	public Live2DParameter ParamHairPhysicsMidToLong { get; private set; }

	public Live2DParameter ParamSalivaPhysics { get; private set; }

	public Live2DParameter ParamPupilWiggle { get; private set; }

	public Live2DParameter ParamEarsWiggle { get; private set; }

	public Live2DParameter ParamPhysicsBoobXL { get; private set; }

	public Live2DParameter ParamPhysicsBoobYL { get; private set; }

	public Live2DParameter ParamPhysicsBoobXR { get; private set; }

	public Live2DParameter ParamPhysicsBoobYR { get; private set; }

	public Live2DParameter ParamBodyFeedback { get; private set; }

	public Live2DParameter ParamEnableBoobFondleL { get; private set; }

	public Live2DParameter ParamBoobSqueezeL { get; private set; }

	public Live2DParameter ParamEnableBoobFondleR { get; private set; }

	public Live2DParameter ParamBoobSqueezeR { get; private set; }

	public Live2DParameter ParamHeadpatX { get; private set; }

	public Live2DParameter ParamHeadpatY { get; private set; }

	public Live2DParameter ParamHeadpat { get; private set; }

	public Live2DParameter ParamOmegaMeter { get; private set; }

	public Live2DParameter ParamBlanketSitting { get; private set; }

	public Live2DParameter ParamSkirtUp { get; private set; }

	public Live2DParameter ParamPantiesX { get; private set; }

	public Live2DParameter ParamPillow { get; private set; }

	public Live2DParameter ParamBlanketCuddle { get; private set; }

	public Live2DParameter ParamBlanketCuddleMove { get; private set; }

	public Live2DParameter ParamArmLCuddleUp { get; private set; }

	public Live2DParameter ParamBreastCuddleEnabled { get; private set; }

	public Live2DParameter ParamAnonHeadpatForearm { get; private set; }

	public Live2DParameter ParamAnonHeadpatUpperarm { get; private set; }

	public Live2DParameter ParamArmLCuddleDown { get; private set; }

	public Live2DParameter ParamCuddleHandholdingEnable { get; private set; }

	public Live2DParameter ParamCuddleHandholdingMove { get; private set; }

	public Live2DParameter ParamFaceRubMoveX { get; private set; }

	public Live2DParameter ParamFaceRubMoveY { get; private set; }

	public Live2DParameter ParamFaceRubEnable { get; private set; }

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

	public Live2DParameter ParamAssSize { get; private set; }

	public Live2DParameter ParamThighSize { get; private set; }

	public Live2DParameter ParamCalfSize { get; private set; }

	public Live2DParameter ParamFootLength { get; private set; }

	public Live2DParameter ParamFootWidth { get; private set; }

	public Live2DParameter ParamBoobSize { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeShock { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowRot { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowEmote { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballX { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEar { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamArmRot { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamArmUp { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamArmGesture { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamHandholdingEnable { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamHandholdingY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamLeg { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamPhysicsBoobX { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamPhysicsBoobY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEnableBoobFondle { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBoobSqueeze { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Hair { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_head___body { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BodyAccessoryPhysics { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobLX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobRX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobLY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobRY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_saliva { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Pupil { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Wiggle { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Tail { get; private set; }

	public Part HitAreaOpenMouth { get; private set; }

	public Part HitAreaCloseMouth { get; private set; }

	public Part HitAreaHeadpat { get; private set; }

	public Part HitAreaFaceStroke { get; private set; }

	public Part HitAreaThumbMouth { get; private set; }

	public Part HitAreaBoobL { get; private set; }

	public Part HitAreaBoobR { get; private set; }

	public Part HitAreaBoobsBoth { get; private set; }

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

	public Part Part_barcode { get; private set; }

	public Part Part_SkinBreastL { get; private set; }

	public Part Part_SkinBreastR { get; private set; }

	public Part Part_SkinArmL { get; private set; }

	public Part Part_SkinArmR { get; private set; }

	public Part Part_SkinNeck { get; private set; }

	public Part Part_SkinBodyBack { get; private set; }

	public Part Part_SkinEyelidClosedL { get; private set; }

	public Part Part_SkinEyelidClosedR { get; private set; }

	public Part Part_SkinEyelidL { get; private set; }

	public Part Part_SkinEyelidR { get; private set; }

	public Part Part_SkinEyelid2L { get; private set; }

	public Part Part_SkinEyelid2R { get; private set; }

	public Part Part_headpet_1 { get; private set; }

	public Part Part_headpet_2 { get; private set; }

	public Part Part_headpet_3 { get; private set; }

	public Part Part_CuteBow { get; private set; }

	public Part Part_OmegaMeterMask { get; private set; }

	public Part Part_Plainshirt_back { get; private set; }

	public Part Part_BlanketFront { get; private set; }

	public Part Part_BlanketBack { get; private set; }

	public Part Part_Shoe_L { get; private set; }

	public Part Part_Shoe_R { get; private set; }

	public Part Part_ModdableHairBack { get; private set; }

	public Part Part_ModdableHairFront { get; private set; }

	public Part Part_ModdableFace { get; private set; }

	public Part Part_ModdableBackLower { get; private set; }

	public Part Part_ModdableBack { get; private set; }

	public Part Part_BellChokerMain { get; private set; }

	public Part Part_PointyEarR { get; private set; }

	public Part Part_PointyEarL { get; private set; }

	public Part Part_BellChokerBell { get; private set; }

	public Part Part_WizardHat { get; private set; }

	public Part Part_AltBPantsBelt2 { get; private set; }

	public Part Part_AltBPantsBelt { get; private set; }

	public Part Part_BPantsCrotch { get; private set; }

	public Part Part_BPantsUpperR { get; private set; }

	public Part Part_BPantsUpperL { get; private set; }

	public Part Part_BPantsLowerR { get; private set; }

	public Part Part_BPantsLowerL { get; private set; }

	public Part Part_LongHoodieRaisedArmFrontR { get; private set; }

	public Part Part_LongHoodieRaisedArmFrontL { get; private set; }

	public Part Part_LongHoodieRaisedArmBackR { get; private set; }

	public Part Part_LongHoodieRaisedArmBackL { get; private set; }

	public Part Part_AltLongHoodieStringR { get; private set; }

	public Part Part_AltLongHoodieStringL { get; private set; }

	public Part Part_LongHoodieHoodFront { get; private set; }

	public Part Part_LongHoodieChest { get; private set; }

	public Part Part_LongHoodieFront { get; private set; }

	public Part Part_LongHoodieUppperArmR { get; private set; }

	public Part Part_LongHoodieUppperArmL { get; private set; }

	public Part Part_LongHoodieLowerArmR { get; private set; }

	public Part Part_LongHoodieLowerArmL { get; private set; }

	public Part Part_LongHoodieHoodBack { get; private set; }

	public Part Part_Plainshirt_front { get; private set; }

	public Part Part_Panties_front { get; private set; }

	public Part Part_Panties_back { get; private set; }

	public Part Part_StockingR { get; private set; }

	public Part Part_StockingL { get; private set; }

	public Part Part_Skirt { get; private set; }

	public Part Part_BraFrontR { get; private set; }

	public Part Part_BraFrontL { get; private set; }

	public Part Part_BrasUpperL { get; private set; }

	public Part Part_BrasUpperR { get; private set; }

	public Part Part_BraMid { get; private set; }

	public Part Part_BraLower { get; private set; }

	public Part Part_PlainShirt_Front { get; private set; }

	public Part Part_PlainShirt_FrontNoBras { get; private set; }

	public Part Part_PlainShirt_Base { get; private set; }

	public Part Part_PlainShirt_SleeveL { get; private set; }

	public Part Part_PlainShirt_SleeveR { get; private set; }

	public Part Part_PlainShirt_Back { get; private set; }

	public Part Part_AttachArmRLowerArmUp { get; private set; }

	public Part Part_AttachArmLLowerArmUp { get; private set; }

	public Part Part_AttachArmRHandUp3 { get; private set; }

	public Part Part_AttachArmLHandUp3 { get; private set; }

	public Part Part_AttachArmRHandUp2 { get; private set; }

	public Part Part_AttachArmLHandUp2 { get; private set; }

	public Part Part_AttachArmRHandUp1 { get; private set; }

	public Part Part_AttachArmLHandUp1 { get; private set; }

	public Part Part_SkinPelvis { get; private set; }

	public Part Part_OmegaMeterInner { get; private set; }

	public Part Part_OmegaMeter { get; private set; }

	public Part Part_SkinBodyFront { get; private set; }

	public Part Part_AttachLegRKnee { get; private set; }

	public Part Part_AttachLegLKnee { get; private set; }

	public Part Part_AttachLegRLower { get; private set; }

	public Part Part_AttachLegLLower { get; private set; }

	public Part Part_AttachLegRFeet { get; private set; }

	public Part Part_AttachLegLFeet { get; private set; }

	public Part Part_SkinThighR { get; private set; }

	public Part Part_SkinThighL { get; private set; }

	public Part Part_SkinButtL { get; private set; }

	public Part Part_SkinButtR { get; private set; }

	public Part Part_AttachLegRThigh { get; private set; }

	public Part Part_AttachLegLThigh { get; private set; }

	public Part Part_AttachArmRLowerArmDown { get; private set; }

	public Part Part_AttachArmLLowerArmDown { get; private set; }

	public Part Part_AttachArmR2 { get; private set; }

	public Part Part_AttachArmL { get; private set; }

	public Part Part_AttachArmRHandDown1 { get; private set; }

	public Part Part_AttachArmLHandDown1 { get; private set; }

	public Part Part_AttachArmRHandDown2 { get; private set; }

	public Part Part_AttachArmLHandDown2 { get; private set; }

	public Part Part_NippleR2 { get; private set; }

	public Part Part_NippleL2 { get; private set; }

	public Part Part_ModdableShirtLogo { get; private set; }

	public Part Part_ModdableHoodieLogo { get; private set; }

	public Part Part_ModdableShirtLeftSleeveLogo { get; private set; }

	public Part Part_ModdableShirtRightSleeveLogo { get; private set; }

	public Part Part_ModdablePantiesLogo { get; private set; }

	public Part Part_AttachArmLLowerCuddle { get; private set; }

	public Part Part_AttachArmLHandCuddle { get; private set; }

	public Part Part_AttachArmLUpperCuddle { get; private set; }

	public Part Part_SkinArmLShoulderCuddleDown { get; private set; }

	public Part Part_BlanketCuddle { get; private set; }

	public Part Part_PillowFront { get; private set; }

	public Part Part_PillowBack { get; private set; }

	public Part Part_MCHandHolding { get; private set; }

	public Part Part_MCHandFaceRubFront { get; private set; }

	public Part Part_MCHandFaceRubBack { get; private set; }

	public Part Part_AttachArmLLowerCuddleUp { get; private set; }

	public Part Part_AttachArmLUpperCuddleUp { get; private set; }

	public Part Part_SkinArmLShoulderCuddleUp { get; private set; }

	public Part Part_BlanketShadow { get; private set; }

	public Part Part_TailMain { get; private set; }

	public Part Part_TailMain2 { get; private set; }

	public Part Part_TailMain3 { get; private set; }

	public Part Part_TailMain4 { get; private set; }

	public Part Part_TailMain5 { get; private set; }

	public Part Part_TailMain6 { get; private set; }

	public Part Part_TailMain7 { get; private set; }

	public Part Part_TailMain8 { get; private set; }

	public Part Part_TailMain9 { get; private set; }

	public Part Part_TailMain10 { get; private set; }

	public Part Part_MCHand { get; private set; }

	public Part Part_MCForearmHandholding { get; private set; }

	public Part Part_MCForearmHandholding2 { get; private set; }

	public Part Part_MCHand2 { get; private set; }

	public Part Part_Dress2Chest { get; private set; }

	public Part Part_Dress2Middle { get; private set; }

	public Part Part_Dress2Back { get; private set; }

	public Part Part_Dress2SleeveR { get; private set; }

	public Part Part_Dress2SleeveL { get; private set; }

	public Part Part_H0_Top_LeafR { get; private set; }

	public Part Part_H0_Top_LeafL { get; private set; }

	public Part Part_H0_Front_Bang { get; private set; }

	public Part Part_H0_Side_BangL { get; private set; }

	public Part Part_H0_Side_BangR { get; private set; }

	public Part Part_H0_SideAlt_Strand { get; private set; }

	public Part Part_H0_Side { get; private set; }

	public Part Part_H0_Braid { get; private set; }

	public Part Part_H0_Back { get; private set; }

	public Part Part_H1_Side_L2 { get; private set; }

	public Part Part_H1_Front_Bang { get; private set; }

	public Part Part_H1_Side_L { get; private set; }

	public Part Part_H1_BackL { get; private set; }

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

	public Part Part_H1_Side_Top { get; private set; }

	public Part Part_H1_Top_Ahoge { get; private set; }

	public Part Part_H1_Back_R2 { get; private set; }

	public Part Part_H1_Back_R1 { get; private set; }

	public Part Part_HairHologram { get; private set; }

	public Part Part_H2_Front_Bang { get; private set; }

	public Part Part_H3_Front { get; private set; }

	public Part Part_H3_Side { get; private set; }

	public Part Part_H3_Back_Top { get; private set; }

	public Part Part_H3_Back { get; private set; }

	public Part Part_H4_Knot { get; private set; }

	public Part Part_H4_Tail { get; private set; }

	public Part Part_Dress1_Bottom { get; private set; }

	public Part Part_Dress1_String { get; private set; }

	public Part Part_Dress1_Chest { get; private set; }

	public Part Part_Dress1_Waist { get; private set; }

	public Part Part_CumOutsideBelly { get; private set; }

	public Part Part_CumOutsideInnerThighLowerL { get; private set; }

	public Part Part_CumOutsideInnerThighL { get; private set; }

	public Part Part_CumOutsideInnerThighL2 { get; private set; }

	public Part Part_CumOutsideBoobL { get; private set; }

	public Part Part_CumOutsideBoobLSide { get; private set; }

	public Part Part_CumOutsideBellyR { get; private set; }

	public Part Part_CumOutsideUnderBoobMiddle { get; private set; }

	public Part Part_CumOutsideBoobRSide { get; private set; }

	public Part Part_CumOutsideBoobR { get; private set; }

	public Part Part_CumOutsideInnerThighR { get; private set; }

	public Part Part_CumOutsideInnerThighR2 { get; private set; }

	public Part Part_CumOutsideFaceRight { get; private set; }

	public Part Part_CumOutsideFaceNose { get; private set; }

	public Part Part_CumOutsideFaceLeft { get; private set; }

	public Part Part_MCRightHandFondle { get; private set; }

	public Part Part_MCLeftHandFondle { get; private set; }

	public Part Part_SkinBraChestR { get; private set; }

	public Part Part_SkinBraChestL { get; private set; }

	public Part Part_ShadowBoobL { get; private set; }

	public Part Part_ShadowBoobR { get; private set; }

	public Part Part_Headband { get; private set; }

	public Part Part_NavelPiercing { get; private set; }

	public Part Part_NipPiercingRight { get; private set; }

	public Part Part_NipPiercingLeft { get; private set; }

	public Part Part_H0_FrontClippedUp_Bang { get; private set; }

	public Part Part_H0_FrontClippedUp_HairClip { get; private set; }

	public Part Part_BikiniBot_A_StrapL { get; private set; }

	public Part Part_BikiniBot_A_StrapR { get; private set; }

	public Part Part_BikiniBot_A_Outline { get; private set; }

	public Part Part_BikiniBot_M_Cloth { get; private set; }

	public Part Part_BikiniTop_A_StrapR { get; private set; }

	public Part Part_BikiniTop_A_OutlineR { get; private set; }

	public Part Part_BikiniTop_M_CupR { get; private set; }

	public Part Part_BikiniTop_A_StrapL { get; private set; }

	public Part Part_BikiniTop_A_OutlineL { get; private set; }

	public Part Part_BikiniTop_M_CupL { get; private set; }

	public Part Part_BikiniTop_A_Middle { get; private set; }

	public override Vector3 GetMouthOffset()
	{
		if (InteractionSceneType == InteractionSceneTypeEnum.Cuddle)
		{
			return new Vector3(-100f, -200f, 0f);
		}
		return new Vector3(-100f, 0f, 0f);
	}

	public override Part GetApproximateEyesPositionPart()
	{
		return Part_EyeBall;
	}

	public override Part GetApproximateMouthPositionPart()
	{
		return Part_InnerMouth;
	}

	public void SetArmState(ArmState state, ArmSelection selection = ArmSelection.Both)
	{
		if (selection == ArmSelection.Left || selection == ArmSelection.Both)
		{
			SetArmLCuddle(ArmLCuddleState.Normal);
			switch (state)
			{
			case ArmState.UpPoint:
				ParamArmLUp.SetPercent(1f);
				ParamArmLGesture.Value = 1f;
				break;
			case ArmState.UpHi:
				ParamArmLUp.SetPercent(1f);
				ParamArmLGesture.Value = 2f;
				break;
			case ArmState.UpLecture:
				ParamArmLUp.SetPercent(1f);
				ParamArmLGesture.Value = 3f;
				break;
			default:
				ParamArmLUp.SetPercent(0f);
				ParamArmLGesture.Value = 1f;
				break;
			case ArmState.DownClenched:
				ParamArmLUp.SetPercent(0f);
				ParamArmLGesture.Value = 2f;
				break;
			}
			ArmLState = state;
		}
		if (selection == ArmSelection.Right || selection == ArmSelection.Both)
		{
			switch (state)
			{
			case ArmState.UpPoint:
				ParamArmRUp.SetPercent(1f);
				ParamArmRGesture.Value = 1f;
				break;
			case ArmState.UpHi:
				ParamArmRUp.SetPercent(1f);
				ParamArmRGesture.Value = 2f;
				break;
			case ArmState.UpLecture:
				ParamArmRUp.SetPercent(1f);
				ParamArmRGesture.Value = 3f;
				break;
			default:
				ParamArmRUp.SetPercent(0f);
				ParamArmRGesture.Value = 1f;
				break;
			case ArmState.DownClenched:
				ParamArmRUp.SetPercent(0f);
				ParamArmRGesture.Value = 2f;
				break;
			}
			ArmRState = state;
		}
	}

	public void SetArmLCuddle(ArmLCuddleState armLCuddleState)
	{
		switch (armLCuddleState)
		{
		case ArmLCuddleState.Down:
			ParamArmLCuddleUp.SetMinValue();
			ParamArmLCuddleDown.SetMaxValue();
			ArmLCuddleLState = ArmLCuddleState.Down;
			break;
		case ArmLCuddleState.Normal:
			ParamArmLCuddleUp.SetMinValue();
			ParamArmLCuddleDown.SetMinValue();
			ArmLCuddleLState = ArmLCuddleState.Normal;
			break;
		case ArmLCuddleState.Up:
			ParamArmLCuddleUp.SetMaxValue();
			ParamArmLCuddleDown.SetMinValue();
			ArmLCuddleLState = ArmLCuddleState.Up;
			break;
		}
	}

	public void SetArmLNormal(ArmState armState)
	{
		SetArmState(armState, ArmSelection.Left);
	}

	private void SetEmoteArms(string armState, ArmSelection armSelection)
	{
		if (Enum.TryParse<ArmState>(armState, ignoreCase: true, out var result))
		{
			SetArmState(result, armSelection);
		}
		else
		{
			Debug.LogError(armState + " cannot be interpreted as the armstate!");
		}
	}

	protected override StoryEmoteManager CreateStoryEmoteManager()
	{
		StoryEmoteManager storyEmoteManager = base.CreateStoryEmoteManager();
		storyEmoteManager.AddSlot(new StoryInteractionArmsSlot(this));
		return storyEmoteManager;
	}

	public void ResetEverything(InteractionSceneTypeEnum interactionSceneType)
	{
		InteractionSceneType = interactionSceneType;
		ArmLCuddleLState = ArmLCuddleState.Normal;
		ArmLState = ArmState.DownNormal;
		ArmRState = ArmState.DownNormal;
		ResetEverything();
	}

	protected override void AfterResetEverything()
	{
		base.AfterResetEverything();
		CubismPhysicsController component = Model.GetComponent<CubismPhysicsController>();
		HeadToChestPhysicsOverride = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig t) => t.Name == "head - body"));
		InteractionSceneTypeEnum interactionSceneType = InteractionSceneType;
		if (interactionSceneType == InteractionSceneTypeEnum.Normal || interactionSceneType != InteractionSceneTypeEnum.Cuddle)
		{
			ParamBreastCuddleEnabled.SetMinValue();
			HeadToChestPhysicsOverride.DisablePhysics = false;
			return;
		}
		HeadToChestPhysicsOverride.DisablePhysics = false;
		ParamChestX.SetMinValue();
		ParamBlanketCuddle.SetMaxValue();
		ParamPillow.SetMaxValue();
		ParamBreastCuddleEnabled.SetMaxValue();
		Eyes.ToDefaultEyesPosition();
		ToDefaultFaceAngle();
		SetArmLCuddle(ArmLCuddleState.Down);
	}

	public static void PrepareBoobsInteractables(Live2DInteractionController thisController, out Live2DInteractableLimbUsage<Live2DInteractionController> boobL, out Live2DInteractableLimbUsage<Live2DInteractionController> boobR, out Live2DInteractableLimbUsage<Live2DInteractionController> boobsBoth)
	{
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobLX.StartParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.StartParameterControl(c.ParamPhysicsBoobYL);
				c.ParamEnableBoobFondleL.SetMaxValue();
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsBoobXL.Value += mouseMove.x * 7f;
				c.ParamPhysicsBoobYL.Value += mouseMove.y * 7f;
				c.ParamBodyFeedback.MoveValueTowardsDefaultInUpdate(Time.deltaTime, 0.25f);
				c.ParamBodyFeedback.Value -= mouseMove.x;
				float magnitude = new Vector2(c.ParamPhysicsBoobXL.GetRealValue(), c.ParamPhysicsBoobYL.GetRealValue()).magnitude;
				c.ParamBoobSqueezeL.Value = magnitude;
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobLX.EndParameterControl(c.ParamPhysicsBoobXL);
				c.PhysicsSubRig_BoobLY.EndParameterControl(c.ParamPhysicsBoobYL);
				c.ParamEnableBoobFondleL.SetMinValue();
				c.ParamChestX.ResetToDefault();
			}
		};
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobRX.StartParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.StartParameterControl(c.ParamPhysicsBoobYR);
				c.ParamEnableBoobFondleR.SetMaxValue();
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				c.ParamPhysicsBoobXR.Value += mouseMove.x * 7f;
				c.ParamPhysicsBoobYR.Value += mouseMove.y * 7f;
				c.ParamBodyFeedback.MoveValueTowardsDefaultInUpdate(Time.deltaTime, 0.25f);
				c.ParamBodyFeedback.Value -= mouseMove.x;
				float magnitude = new Vector2(c.ParamPhysicsBoobXR.GetRealValue(), c.ParamPhysicsBoobYR.GetRealValue()).magnitude;
				c.ParamBoobSqueezeR.Value = magnitude;
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobRX.EndParameterControl(c.ParamPhysicsBoobXR);
				c.PhysicsSubRig_BoobRY.EndParameterControl(c.ParamPhysicsBoobYR);
				c.ParamEnableBoobFondleR.SetMinValue();
				c.ParamChestX.ResetToDefault();
			}
		};
		boobL = thisController.AddInteractableHitArea(thisController.HitAreaBoobL, new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobL, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DInteractionController c) => c.ParamBlanketSitting.Value < 0.5f
		});
		boobR = thisController.AddInteractableHitArea(thisController.HitAreaBoobR, new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobR, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DInteractionController c) => c.ParamBlanketSitting.Value < 0.5f
		});
		boobsBoth = thisController.AddInteractableHitArea(thisController.HitAreaBoobsBoth, new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_PlayBoobs, limbUsageWithInteractable, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DInteractionController c) => c.ParamBlanketSitting.Value < 0.5f
		});
	}

	public static void PrepareHandholdingInteractable(Live2DInteractionController thisController, out Live2DInteractableLimbUsage<Live2DInteractionController> armL, out Live2DInteractableLimbUsage<Live2DInteractionController> armR)
	{
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
				CommonHandholdingStart(c, c.ParamHandholdingLEnable, c.ParamHandholdingLY);
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				CommonHandholdingUpdate(c, ref mouseMove, c.ParamHandholdingLY, c.ParamArmLRot, c.ParamArmLUp);
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				CommonHandholdingEnd(c, c.ParamArmLUp, c.ParamArmLRot, c.ParamHandholdingLEnable);
			}
		};
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
				CommonHandholdingStart(c, c.ParamHandholdingREnable, c.ParamHandholdingRY);
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				CommonHandholdingUpdate(c, ref mouseMove, c.ParamHandholdingRY, c.ParamArmRRot, c.ParamArmRUp, invertX: true);
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				CommonHandholdingEnd(c, c.ParamArmRUp, c.ParamArmRRot, c.ParamHandholdingREnable);
			}
		};
		armL = thisController.AddInteractable(thisController.GetGroup(ColoringGroupEnum.ArmL).Parts, new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_HoldHerHand, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DInteractionController c) => c.ParamArmLUp.Value < 0.4f && c.ParamBlanketSitting.Value < 0.5f
		});
		armR = thisController.AddInteractable(thisController.GetGroup(ColoringGroupEnum.ArmR).Parts, new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_HoldHerHand, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DInteractionController c) => c.ParamArmRUp.Value < 0.4f && c.ParamBlanketSitting.Value < 0.5f
		});
		static void CommonHandholdingEnd(Live2DInteractionController c, Live2DParameter armUpParam, Live2DParameter armRotParam, Live2DParameter handholdingEnableParam)
		{
			armUpParam.ResetToDefault();
			armRotParam.ResetToDefault();
			handholdingEnableParam.SetMinValue();
			c.Eyes.ToDefaultEyesPosition();
			c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.Handholding);
		}
		static void CommonHandholdingStart(Live2DInteractionController c, Live2DParameter handholdingEnableParam, Live2DParameter handholdingYParam)
		{
			handholdingEnableParam.SetMaxValue();
			handholdingYParam.ResetToDefault();
			c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.Handholding);
		}
		static void CommonHandholdingUpdate(Live2DInteractionController c, ref Vector2 mouseMove, Live2DParameter handholdingYParam, Live2DParameter armRotParam, Live2DParameter armUpParam, bool invertX = false)
		{
			c.RotateInputToTransformRotation(ref mouseMove);
			if (invertX)
			{
				mouseMove.x = 0f - mouseMove.x;
			}
			handholdingYParam.Value += mouseMove.y * 20f;
			armRotParam.Value += mouseMove.x * 2f;
			armUpParam.Value = Mathf.Clamp(armUpParam.Value + mouseMove.x * 2f, 0f, 0.1f);
			armRotParam.ClampOffsetFromDefaultValue(0f, 0.4f);
		}
	}

	public static void PrepareFaceRubInteractable(Live2DInteractionController thisController, out Live2DInteractableLimbUsage<Live2DInteractionController> interactable)
	{
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
				c.ParamFaceRubEnable.SetMaxValue();
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.FaceStroke);
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				c.RotateInputToTransformRotation(ref mouseMove);
				c.ParamFaceRubMoveX.AddPercent((0f - mouseMove.y) * 20f);
				c.ParamFaceRubMoveY.AddPercent(mouseMove.x * 20f);
				c.ParamHeadX.AddPercent(mouseMove.x * 1f);
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				c.ParamFaceRubEnable.SetMinValue();
				c.ToDefaultFaceAngle();
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.FaceStroke);
			}
		};
		interactable = new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_RubHerFace, limbUsageWithInteractable);
		thisController.AddInteractableHitArea(thisController.HitAreaFaceStroke, interactable);
	}

	public static void PreparePantiesAndSkirt(Live2DInteractionController thisController, out Live2DInteractableLimbUsage<Live2DInteractionController> _pantiesInteractable, out Live2DInteractableLimbUsage<Live2DInteractionController> _skirtInteractable)
	{
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				c.ParamPantiesX.Value += mouseMove.x * 30f;
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				if (!c.IsPantiesAside)
				{
					c.ParamPantiesX.ResetToDefault();
				}
				else if (c.ParamPantiesX.Value > 0f)
				{
					c.ParamPantiesX.Value = c.ParamPantiesX.MaximumValue;
				}
				else
				{
					c.ParamPantiesX.Value = c.ParamPantiesX.MinimumValue;
				}
			}
		};
		_pantiesInteractable = new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_MovePanties, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DInteractionController c) => c.IsPantsAsideOrOff && c.IsSkirtAsideOrOff && c.IsDress2AsideOrOff && c.IsDress1AsideOrOff
		};
		thisController.AddInteractable(new List<string> { "Panties_front", "Panties_back" }, _pantiesInteractable);
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				c.ParamSkirtUp.Value += mouseMove.y * 30f;
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				if (c.IsSkirtAside)
				{
					c.ParamSkirtUp.Value = c.ParamPantiesX.MaximumValue;
				}
				else
				{
					c.ParamSkirtUp.Value = c.ParamPantiesX.MinimumValue;
				}
			}
		};
		_skirtInteractable = new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_MoveSkirt, limbUsageWithInteractable2);
		thisController.AddInteractable("Skirt", _skirtInteractable);
	}

	public static void PrepareCuddleHeadpatInteractable(Live2DInteractionController thisController, out Live2DInteractableLimbUsage<Live2DInteractionController> _headpatInteractable)
	{
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.Headpat);
				c.ParamHeadpatX.SetWeight(1f);
				c.ParamHeadpatY.SetWeight(1f);
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				c.RotateInputToTransformRotation(ref mouseMove);
				c.ParamHeadpatX.AddPercent(mouseMove.x * 16f);
				c.ParamHeadpatX.ClampOffsetFromDefaultValue(-0.1f, 1f);
				c.ParamHeadpatY.AddPercent(mouseMove.y * 12f);
				c.ParamHeadX.AddPercent(mouseMove.x * 10f);
				c.ParamHeadY.AddPercent(mouseMove.y * 10f);
				c.ParamHeadZ.AddPercent(mouseMove.x * 10f);
				Vector3 vector = new Vector3(c.ParamHeadX.Value, c.ParamHeadY.Value, c.ParamHeadZ.Value);
				Vector3 vec = vector + (c.DefaultFaceAngle - vector) * (Time.deltaTime * 2f);
				(c.ParamHeadX.Value, c.ParamHeadY.Value, c.ParamHeadZ.Value) = vec.Deconstruct();
				c.ParamHeadX.ClampOffsetFromDefaultValue(0.8f, 0.3f);
				c.ParamHeadY.ClampOffsetFromDefaultValue(0.3f, 0.4f);
				c.ParamHeadZ.ClampOffsetFromDefaultValue(-0.3f, 0.8f);
			},
			HoldFixedUpdateAction = (Live2DInteractionController c) =>
			{
				if (c.CurrentBrain.Pleasure < 0.5f)
				{
					c.CurrentBrain.CausePleasure(0.002f);
				}
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				if (gameVariables.Mood < 0.3f)
				{
					gameVariables.Mood += 0.001f;
				}
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				c.ParamHeadpatX.SetWeight(2f);
				c.ParamHeadpatY.SetWeight(2f);
				c.ParamHeadpatX.ResetToDefault();
				c.ParamHeadpatY.ResetToDefault();
				c.Eyes.ToDefaultEyesPosition();
				c.ToDefaultFaceAngle();
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.Headpat);
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.lastHeadpatedAt = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
			}
		};
		_headpatInteractable = thisController.AddInteractableHitArea(thisController.HitAreaHeadpat, new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_Headpat, limbUsageWithInteractable));
		_headpatInteractable.OnHoverStart = (Live2DInteractionController c) =>
		{
			c.ParamHeadpat.SetPercent(1f);
		};
		_headpatInteractable.OnHoverEnd = (Live2DInteractionController c) =>
		{
			c.ParamHeadpat.SetPercent(0f);
		};
		_headpatInteractable.HoverCursor = CursorSingleton.CursorType.NoCursor;
	}

	public static void PrepareHandHoldingCuddleInteractable(Live2DInteractionController thisController, out Live2DInteractableLimbUsage<Live2DInteractionController> _handholdingInteractable)
	{
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
				c.ParamCuddleHandholdingEnable.SetMaxValue();
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.Handholding);
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				c.ParamCuddleHandholdingMove.AddPercent(mouseMove.y * 20f);
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				c.ParamCuddleHandholdingEnable.SetMinValue();
				c.ToDefaultFaceAngle();
				c.Eyes.ToDefaultEyesPosition();
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.Handholding);
			}
		};
		_handholdingInteractable = new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_HoldHerHand, limbUsageWithInteractable);
		thisController.AddInteractable("AttachArmLHandCuddle", _handholdingInteractable);
	}

	public static void PrepareThumbMouthInteractables(Live2DInteractionController thisController, out Live2DInteractableLimbUsage<Live2DInteractionController> _thumbMouthInteractable)
	{
		LimbUsageWithInteractable<Live2DInteractionController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DInteractionController>(thisController, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DInteractionController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.ThumbMouth);
				c.ParamEnableThumbMouth.SetMaxValue();
				c.ParamThumbMouthX.SetPercent(0.5f);
			},
			HoldUpdateAction = (Live2DInteractionController c, Vector2 mouseMove) =>
			{
				c.RotateInputToTransformRotation(ref mouseMove);
				c.ParamThumbMouthX.Value += mouseMove.x * 10f;
				c.ParamHeadX.AddPercent(mouseMove.x * 5f);
				c.ParamHeadX.Value = Mathf.Clamp(c.ParamHeadX.Value, c.ParamHeadX.MinimumValue * 0.8f, c.ParamHeadX.MaximumValue * 0.8f);
				c.ParamHeadY.AddPercent(mouseMove.y * 5f);
			},
			EndLimbAction = (Live2DInteractionController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.ThumbMouth);
				c.ParamEnableThumbMouth.SetMinValue();
				c.ParamThumbMouthX.SetMinValue();
				c.ToDefaultFaceAngle();
			}
		};
		_thumbMouthInteractable = thisController.AddInteractableHitArea(thisController.HitAreaThumbMouth, new Live2DInteractableLimbUsage<Live2DInteractionController>(thisController, LOC.UI.TupleReferences.Interactable_ThumbInMouth, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DInteractionController c) => c.ParamMouthOpen.Value > 1f
		});
	}

	public override void ResetBasic()
	{
		base.ResetBasic();
		ParamBlanketSitting.ResetToDefault();
		ParamBlanketCuddle.ResetToDefault();
	}

	protected override MouthManagerDefaultMouth InitializeMouthImpl()
	{
		return MouthManagerDefaultMouth.Create(this);
	}

	protected override EyeManagerCommon InitializeEyesImpl()
	{
		return EyeManagerCommon.Create(this);
	}

	protected override void InitializeInherited()
	{
		int live2DLayer = Live2DToolSelector.MouthTool.Live2DLayer;
		Live2DGenericInteractable live2DGenericInteractable = AddGenericInteractable(GetGroup(ColoringGroupEnum.Nipples).GetCubismRenderers(), live2DLayer);
		live2DGenericInteractable.OnHoldStart = (CubismDrawable d) =>
		{
			StartKiss(d, KissStartPositionType.RendererCenter);
		};
		live2DGenericInteractable.OnHeld = (Vector2 move) =>
		{
			CurrentBrain.CausePleasure(move.sqrMagnitude * (1f / 60f));
		};
		live2DGenericInteractable.OnHeldFixed = () =>
		{
			CurrentBrain.CausePleasure(0.0005f);
		};
		live2DGenericInteractable.OnHoldEnd = EndKiss;
		Live2DController.PrepareCommonMouthInteractables(this, out var _, out var _);
		Live2DController.PrepareCommonTongueInteractables(this, out var _);
	}

	public override void InitializeGenerated()
	{
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
		ParamEnableThumbMouth = GetParamToDirectlyControl("ParamEnableThumbMouth");
		ParamThumbMouthX = GetParamToDirectlyControl("ParamThumbMouthX");
		ParamBlush = GetParamToDirectlyControl("ParamBlush");
		ParamHeadZ = GetParamToDirectlyControl("ParamHeadZ");
		ParamHeadZ.SetWeight(10f);
		ParamHeadX = GetParamToDirectlyControl("ParamHeadX");
		ParamHeadX.SetWeight(10f);
		ParamHeadY = GetParamToDirectlyControl("ParamHeadY");
		ParamHeadY.SetWeight(10f);
		ParamEarL = GetParamToDirectlyControl("ParamEarL");
		ParamEarR = GetParamToDirectlyControl("ParamEarR");
		ParamArmLRot = GetParamToDirectlyControl("ParamArmLRot");
		ParamArmLUp = GetParamToDirectlyControl("ParamArmLUp");
		ParamArmLGesture = GetParamToDirectlyControl("ParamArmLGesture");
		ParamHandholdingREnable = GetParamToDirectlyControl("ParamHandholdingREnable");
		ParamHandholdingRY = GetParamToDirectlyControl("ParamHandholdingRY");
		ParamArmRRot = GetParamToDirectlyControl("ParamArmRRot");
		ParamArmRUp = GetParamToDirectlyControl("ParamArmRUp");
		ParamArmRGesture = GetParamToDirectlyControl("ParamArmRGesture");
		ParamHandholdingLEnable = GetParamToDirectlyControl("ParamHandholdingLEnable");
		ParamHandholdingLY = GetParamToDirectlyControl("ParamHandholdingLY");
		ParamBodyZ = GetParamToDirectlyControl("ParamBodyZ");
		ParamBodyX = GetParamToDirectlyControl("ParamBodyX");
		ParamBodyY = GetParamToDirectlyControl("ParamBodyY");
		ParamLegR = GetParamToDirectlyControl("ParamLegR");
		ParamLegL = GetParamToDirectlyControl("ParamLegL");
		ParamChestX = GetParamToDirectlyControl("ParamChestX");
		ParamBodyAccessoryPhysics = GetParamToDirectlyControl("ParamBodyAccessoryPhysics");
		ParamHairPhysicsBaseToShort = GetParamToDirectlyControl("ParamHairPhysicsBaseToShort");
		ParamHairPhysicsShortToMid = GetParamToDirectlyControl("ParamHairPhysicsShortToMid");
		ParamHairPhysicsMidToLong = GetParamToDirectlyControl("ParamHairPhysicsMidToLong");
		ParamSalivaPhysics = GetParamToDirectlyControl("ParamSalivaPhysics");
		ParamPupilWiggle = GetParamToDirectlyControl("ParamPupilWiggle");
		ParamEarsWiggle = GetParamToDirectlyControl("ParamEarsWiggle");
		ParamPhysicsBoobXL = GetParamToDirectlyControl("ParamPhysicsBoobXL");
		ParamPhysicsBoobYL = GetParamToDirectlyControl("ParamPhysicsBoobYL");
		ParamPhysicsBoobXR = GetParamToDirectlyControl("ParamPhysicsBoobXR");
		ParamPhysicsBoobYR = GetParamToDirectlyControl("ParamPhysicsBoobYR");
		ParamBodyFeedback = GetParamToDirectlyControl("ParamBodyFeedback");
		ParamEnableBoobFondleL = GetParamToDirectlyControl("ParamEnableBoobFondleL");
		ParamBoobSqueezeL = GetParamToDirectlyControl("ParamBoobSqueezeL");
		ParamEnableBoobFondleR = GetParamToDirectlyControl("ParamEnableBoobFondleR");
		ParamBoobSqueezeR = GetParamToDirectlyControl("ParamBoobSqueezeR");
		ParamHeadpatX = GetParamToDirectlyControl("ParamHeadpatX");
		ParamHeadpatX.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		ParamHeadpatY = GetParamToDirectlyControl("ParamHeadpatY");
		ParamHeadpatY.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		ParamHeadpat = GetParamToDirectlyControl("ParamHeadpat");
		ParamOmegaMeter = GetParamToDirectlyControl("ParamOmegaMeter");
		ParamBlanketSitting = GetParamToDirectlyControl("ParamBlanketSitting");
		ParamSkirtUp = GetParamToDirectlyControl("ParamSkirtUp");
		ParamPantiesX = GetParamToDirectlyControl("ParamPantiesX");
		ParamPillow = GetParamToDirectlyControl("ParamPillow");
		ParamBlanketCuddle = GetParamToDirectlyControl("ParamBlanketCuddle");
		ParamBlanketCuddleMove = GetParamToDirectlyControl("ParamBlanketCuddleMove");
		ParamArmLCuddleUp = GetParamToDirectlyControl("ParamArmLCuddleUp");
		ParamBreastCuddleEnabled = GetParamToDirectlyControl("ParamBreastCuddleEnabled");
		ParamAnonHeadpatForearm = GetParamToDirectlyControl("ParamAnonHeadpatForearm");
		ParamAnonHeadpatUpperarm = GetParamToDirectlyControl("ParamAnonHeadpatUpperarm");
		ParamArmLCuddleDown = GetParamToDirectlyControl("ParamArmLCuddleDown");
		ParamCuddleHandholdingEnable = GetParamToDirectlyControl("ParamCuddleHandholdingEnable");
		ParamCuddleHandholdingMove = GetParamToDirectlyControl("ParamCuddleHandholdingMove");
		ParamFaceRubMoveX = GetParamToDirectlyControl("ParamFaceRubMoveX");
		ParamFaceRubMoveY = GetParamToDirectlyControl("ParamFaceRubMoveY");
		ParamFaceRubEnable = GetParamToDirectlyControl("ParamFaceRubEnable");
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
		ParamAssSize = GetParamToDirectlyControl("ParamAssSize");
		ParamThighSize = GetParamToDirectlyControl("ParamThighSize");
		ParamCalfSize = GetParamToDirectlyControl("ParamCalfSize");
		ParamFootLength = GetParamToDirectlyControl("ParamFootLength");
		ParamFootWidth = GetParamToDirectlyControl("ParamFootWidth");
		ParamBoobSize = GetParamToDirectlyControl("ParamBoobSize");
		SymParam_LR_ParamBrowY = new Live2DParameter[2] { ParamBrowLY, ParamBrowRY };
		SymParam_LR_ParamEyeShock = new Live2DParameter[2] { ParamEyeLShock, ParamEyeRShock };
		SymParam_LR_ParamBrowRot = new Live2DParameter[2] { ParamBrowLRot, ParamBrowRRot };
		SymParam_LR_ParamBrowEmote = new Live2DParameter[2] { ParamBrowLEmote, ParamBrowREmote };
		SymParam_LR_ParamEyeballX = new Live2DParameter[2] { ParamEyeballLX, ParamEyeballRX };
		SymParam_LR_ParamEyeballY = new Live2DParameter[2] { ParamEyeballLY, ParamEyeballRY };
		SymParam_LR_ParamEar = new Live2DParameter[2] { ParamEarL, ParamEarR };
		SymParam_LR_ParamArmRot = new Live2DParameter[2] { ParamArmLRot, ParamArmRRot };
		SymParam_LR_ParamArmUp = new Live2DParameter[2] { ParamArmLUp, ParamArmRUp };
		SymParam_LR_ParamArmGesture = new Live2DParameter[2] { ParamArmLGesture, ParamArmRGesture };
		SymParam_LR_ParamHandholdingEnable = new Live2DParameter[2] { ParamHandholdingLEnable, ParamHandholdingREnable };
		SymParam_LR_ParamHandholdingY = new Live2DParameter[2] { ParamHandholdingLY, ParamHandholdingRY };
		SymParam_LR_ParamLeg = new Live2DParameter[2] { ParamLegL, ParamLegR };
		SymParam_LR_ParamPhysicsBoobX = new Live2DParameter[2] { ParamPhysicsBoobXL, ParamPhysicsBoobXR };
		SymParam_LR_ParamPhysicsBoobY = new Live2DParameter[2] { ParamPhysicsBoobYL, ParamPhysicsBoobYR };
		SymParam_LR_ParamEnableBoobFondle = new Live2DParameter[2] { ParamEnableBoobFondleL, ParamEnableBoobFondleR };
		SymParam_LR_ParamBoobSqueeze = new Live2DParameter[2] { ParamBoobSqueezeL, ParamBoobSqueezeR };
		CubismPhysicsController component = Model.GetComponent<CubismPhysicsController>();
		PhysicsSubRig_Hair = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Hair"));
		PhysicsSubRig_head___body = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "head - body"));
		PhysicsSubRig_BodyAccessoryPhysics = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BodyAccessoryPhysics"));
		PhysicsSubRig_BoobLX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobLX"));
		PhysicsSubRig_BoobRX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobRX"));
		PhysicsSubRig_BoobLY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobLY"));
		PhysicsSubRig_BoobRY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobRY"));
		PhysicsSubRig_saliva = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "saliva"));
		PhysicsSubRig_Pupil = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Pupil"));
		PhysicsSubRig_Wiggle = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Wiggle"));
		PhysicsSubRig_Tail = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Tail"));
		HitAreaOpenMouth = GetCachedPart("HitAreaOpenMouth");
		HitAreaCloseMouth = GetCachedPart("HitAreaCloseMouth");
		HitAreaHeadpat = GetCachedPart("HitAreaHeadpat");
		HitAreaFaceStroke = GetCachedPart("HitAreaFaceStroke");
		HitAreaThumbMouth = GetCachedPart("HitAreaThumbMouth");
		HitAreaBoobL = GetCachedPart("HitAreaBoobL");
		HitAreaBoobR = GetCachedPart("HitAreaBoobR");
		HitAreaBoobsBoth = GetCachedPart("HitAreaBoobsBoth");
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
		Part_barcode = GetCachedPart("barcode");
		Part_SkinBreastL = GetCachedPart("SkinBreastL");
		Part_SkinBreastR = GetCachedPart("SkinBreastR");
		Part_SkinArmL = GetCachedPart("SkinArmL");
		Part_SkinArmR = GetCachedPart("SkinArmR");
		Part_SkinNeck = GetCachedPart("SkinNeck");
		Part_SkinBodyBack = GetCachedPart("SkinBodyBack");
		Part_SkinEyelidClosedL = GetCachedPart("SkinEyelidClosedL");
		Part_SkinEyelidClosedR = GetCachedPart("SkinEyelidClosedR");
		Part_SkinEyelidL = GetCachedPart("SkinEyelidL");
		Part_SkinEyelidR = GetCachedPart("SkinEyelidR");
		Part_SkinEyelid2L = GetCachedPart("SkinEyelid2L");
		Part_SkinEyelid2R = GetCachedPart("SkinEyelid2R");
		Part_headpet_1 = GetCachedPart("headpet_1");
		Part_headpet_2 = GetCachedPart("headpet_2");
		Part_headpet_3 = GetCachedPart("headpet_3");
		Part_CuteBow = GetCachedPart("CuteBow");
		Part_OmegaMeterMask = GetCachedPart("OmegaMeterMask");
		Part_Plainshirt_back = GetCachedPart("Plainshirt_back");
		Part_BlanketFront = GetCachedPart("BlanketFront");
		Part_BlanketBack = GetCachedPart("BlanketBack");
		Part_Shoe_L = GetCachedPart("Shoe_L");
		Part_Shoe_R = GetCachedPart("Shoe_R");
		Part_ModdableHairBack = GetCachedPart("ModdableHairBack");
		Part_ModdableHairFront = GetCachedPart("ModdableHairFront");
		Part_ModdableFace = GetCachedPart("ModdableFace");
		Part_ModdableBackLower = GetCachedPart("ModdableBackLower");
		Part_ModdableBack = GetCachedPart("ModdableBack");
		Part_BellChokerMain = GetCachedPart("BellChokerMain");
		Part_PointyEarR = GetCachedPart("PointyEarR");
		Part_PointyEarL = GetCachedPart("PointyEarL");
		Part_BellChokerBell = GetCachedPart("BellChokerBell");
		Part_WizardHat = GetCachedPart("WizardHat");
		Part_AltBPantsBelt2 = GetCachedPart("AltBPantsBelt2");
		Part_AltBPantsBelt = GetCachedPart("AltBPantsBelt");
		Part_BPantsCrotch = GetCachedPart("BPantsCrotch");
		Part_BPantsUpperR = GetCachedPart("BPantsUpperR");
		Part_BPantsUpperL = GetCachedPart("BPantsUpperL");
		Part_BPantsLowerR = GetCachedPart("BPantsLowerR");
		Part_BPantsLowerL = GetCachedPart("BPantsLowerL");
		Part_LongHoodieRaisedArmFrontR = GetCachedPart("LongHoodieRaisedArmFrontR");
		Part_LongHoodieRaisedArmFrontL = GetCachedPart("LongHoodieRaisedArmFrontL");
		Part_LongHoodieRaisedArmBackR = GetCachedPart("LongHoodieRaisedArmBackR");
		Part_LongHoodieRaisedArmBackL = GetCachedPart("LongHoodieRaisedArmBackL");
		Part_AltLongHoodieStringR = GetCachedPart("AltLongHoodieStringR");
		Part_AltLongHoodieStringL = GetCachedPart("AltLongHoodieStringL");
		Part_LongHoodieHoodFront = GetCachedPart("LongHoodieHoodFront");
		Part_LongHoodieChest = GetCachedPart("LongHoodieChest");
		Part_LongHoodieFront = GetCachedPart("LongHoodieFront");
		Part_LongHoodieUppperArmR = GetCachedPart("LongHoodieUppperArmR");
		Part_LongHoodieUppperArmL = GetCachedPart("LongHoodieUppperArmL");
		Part_LongHoodieLowerArmR = GetCachedPart("LongHoodieLowerArmR");
		Part_LongHoodieLowerArmL = GetCachedPart("LongHoodieLowerArmL");
		Part_LongHoodieHoodBack = GetCachedPart("LongHoodieHoodBack");
		Part_Plainshirt_front = GetCachedPart("Plainshirt_front");
		Part_Panties_front = GetCachedPart("Panties_front");
		Part_Panties_back = GetCachedPart("Panties_back");
		Part_StockingR = GetCachedPart("StockingR");
		Part_StockingL = GetCachedPart("StockingL");
		Part_Skirt = GetCachedPart("Skirt");
		Part_BraFrontR = GetCachedPart("BraFrontR");
		Part_BraFrontL = GetCachedPart("BraFrontL");
		Part_BrasUpperL = GetCachedPart("BrasUpperL");
		Part_BrasUpperR = GetCachedPart("BrasUpperR");
		Part_BraMid = GetCachedPart("BraMid");
		Part_BraLower = GetCachedPart("BraLower");
		Part_PlainShirt_Front = GetCachedPart("PlainShirt_Front");
		Part_PlainShirt_FrontNoBras = GetCachedPart("PlainShirt_FrontNoBras");
		Part_PlainShirt_Base = GetCachedPart("PlainShirt_Base");
		Part_PlainShirt_SleeveL = GetCachedPart("PlainShirt_SleeveL");
		Part_PlainShirt_SleeveR = GetCachedPart("PlainShirt_SleeveR");
		Part_PlainShirt_Back = GetCachedPart("PlainShirt_Back");
		Part_AttachArmRLowerArmUp = GetCachedPart("AttachArmRLowerArmUp");
		Part_AttachArmLLowerArmUp = GetCachedPart("AttachArmLLowerArmUp");
		Part_AttachArmRHandUp3 = GetCachedPart("AttachArmRHandUp3");
		Part_AttachArmLHandUp3 = GetCachedPart("AttachArmLHandUp3");
		Part_AttachArmRHandUp2 = GetCachedPart("AttachArmRHandUp2");
		Part_AttachArmLHandUp2 = GetCachedPart("AttachArmLHandUp2");
		Part_AttachArmRHandUp1 = GetCachedPart("AttachArmRHandUp1");
		Part_AttachArmLHandUp1 = GetCachedPart("AttachArmLHandUp1");
		Part_SkinPelvis = GetCachedPart("SkinPelvis");
		Part_OmegaMeterInner = GetCachedPart("OmegaMeterInner");
		Part_OmegaMeter = GetCachedPart("OmegaMeter");
		Part_SkinBodyFront = GetCachedPart("SkinBodyFront");
		Part_AttachLegRKnee = GetCachedPart("AttachLegRKnee");
		Part_AttachLegLKnee = GetCachedPart("AttachLegLKnee");
		Part_AttachLegRLower = GetCachedPart("AttachLegRLower");
		Part_AttachLegLLower = GetCachedPart("AttachLegLLower");
		Part_AttachLegRFeet = GetCachedPart("AttachLegRFeet");
		Part_AttachLegLFeet = GetCachedPart("AttachLegLFeet");
		Part_SkinThighR = GetCachedPart("SkinThighR");
		Part_SkinThighL = GetCachedPart("SkinThighL");
		Part_SkinButtL = GetCachedPart("SkinButtL");
		Part_SkinButtR = GetCachedPart("SkinButtR");
		Part_AttachLegRThigh = GetCachedPart("AttachLegRThigh");
		Part_AttachLegLThigh = GetCachedPart("AttachLegLThigh");
		Part_AttachArmRLowerArmDown = GetCachedPart("AttachArmRLowerArmDown");
		Part_AttachArmLLowerArmDown = GetCachedPart("AttachArmLLowerArmDown");
		Part_AttachArmR2 = GetCachedPart("AttachArmR2");
		Part_AttachArmL = GetCachedPart("AttachArmL");
		Part_AttachArmRHandDown1 = GetCachedPart("AttachArmRHandDown1");
		Part_AttachArmLHandDown1 = GetCachedPart("AttachArmLHandDown1");
		Part_AttachArmRHandDown2 = GetCachedPart("AttachArmRHandDown2");
		Part_AttachArmLHandDown2 = GetCachedPart("AttachArmLHandDown2");
		Part_NippleR2 = GetCachedPart("NippleR2");
		Part_NippleL2 = GetCachedPart("NippleL2");
		Part_ModdableShirtLogo = GetCachedPart("ModdableShirtLogo");
		Part_ModdableHoodieLogo = GetCachedPart("ModdableHoodieLogo");
		Part_ModdableShirtLeftSleeveLogo = GetCachedPart("ModdableShirtLeftSleeveLogo");
		Part_ModdableShirtRightSleeveLogo = GetCachedPart("ModdableShirtRightSleeveLogo");
		Part_ModdablePantiesLogo = GetCachedPart("ModdablePantiesLogo");
		Part_AttachArmLLowerCuddle = GetCachedPart("AttachArmLLowerCuddle");
		Part_AttachArmLHandCuddle = GetCachedPart("AttachArmLHandCuddle");
		Part_AttachArmLUpperCuddle = GetCachedPart("AttachArmLUpperCuddle");
		Part_SkinArmLShoulderCuddleDown = GetCachedPart("SkinArmLShoulderCuddleDown");
		Part_BlanketCuddle = GetCachedPart("BlanketCuddle");
		Part_PillowFront = GetCachedPart("PillowFront");
		Part_PillowBack = GetCachedPart("PillowBack");
		Part_MCHandHolding = GetCachedPart("MCHandHolding");
		Part_MCHandFaceRubFront = GetCachedPart("MCHandFaceRubFront");
		Part_MCHandFaceRubBack = GetCachedPart("MCHandFaceRubBack");
		Part_AttachArmLLowerCuddleUp = GetCachedPart("AttachArmLLowerCuddleUp");
		Part_AttachArmLUpperCuddleUp = GetCachedPart("AttachArmLUpperCuddleUp");
		Part_SkinArmLShoulderCuddleUp = GetCachedPart("SkinArmLShoulderCuddleUp");
		Part_BlanketShadow = GetCachedPart("BlanketShadow");
		Part_TailMain = GetCachedPart("TailMain");
		Part_TailMain2 = GetCachedPart("TailMain2");
		Part_TailMain3 = GetCachedPart("TailMain3");
		Part_TailMain4 = GetCachedPart("TailMain4");
		Part_TailMain5 = GetCachedPart("TailMain5");
		Part_TailMain6 = GetCachedPart("TailMain6");
		Part_TailMain7 = GetCachedPart("TailMain7");
		Part_TailMain8 = GetCachedPart("TailMain8");
		Part_TailMain9 = GetCachedPart("TailMain9");
		Part_TailMain10 = GetCachedPart("TailMain10");
		Part_MCHand = GetCachedPart("MCHand");
		Part_MCForearmHandholding = GetCachedPart("MCForearmHandholding");
		Part_MCForearmHandholding2 = GetCachedPart("MCForearmHandholding2");
		Part_MCHand2 = GetCachedPart("MCHand2");
		Part_Dress2Chest = GetCachedPart("Dress2Chest");
		Part_Dress2Middle = GetCachedPart("Dress2Middle");
		Part_Dress2Back = GetCachedPart("Dress2Back");
		Part_Dress2SleeveR = GetCachedPart("Dress2SleeveR");
		Part_Dress2SleeveL = GetCachedPart("Dress2SleeveL");
		Part_H0_Top_LeafR = GetCachedPart("H0_Top_LeafR");
		Part_H0_Top_LeafL = GetCachedPart("H0_Top_LeafL");
		Part_H0_Front_Bang = GetCachedPart("H0_Front_Bang");
		Part_H0_Side_BangL = GetCachedPart("H0_Side_BangL");
		Part_H0_Side_BangR = GetCachedPart("H0_Side_BangR");
		Part_H0_SideAlt_Strand = GetCachedPart("H0_SideAlt_Strand");
		Part_H0_Side = GetCachedPart("H0_Side");
		Part_H0_Braid = GetCachedPart("H0_Braid");
		Part_H0_Back = GetCachedPart("H0_Back");
		Part_H1_Side_L2 = GetCachedPart("H1_Side_L2");
		Part_H1_Front_Bang = GetCachedPart("H1_Front_Bang");
		Part_H1_Side_L = GetCachedPart("H1_Side_L");
		Part_H1_BackL = GetCachedPart("H1_BackL");
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
		Part_H1_Side_Top = GetCachedPart("H1_Side_Top");
		Part_H1_Top_Ahoge = GetCachedPart("H1_Top_Ahoge");
		Part_H1_Back_R2 = GetCachedPart("H1_Back_R2");
		Part_H1_Back_R1 = GetCachedPart("H1_Back_R1");
		Part_HairHologram = GetCachedPart("HairHologram");
		Part_H2_Front_Bang = GetCachedPart("H2_Front_Bang");
		Part_H3_Front = GetCachedPart("H3_Front");
		Part_H3_Side = GetCachedPart("H3_Side");
		Part_H3_Back_Top = GetCachedPart("H3_Back_Top");
		Part_H3_Back = GetCachedPart("H3_Back");
		Part_H4_Knot = GetCachedPart("H4_Knot");
		Part_H4_Tail = GetCachedPart("H4_Tail");
		Part_Dress1_Bottom = GetCachedPart("Dress1_Bottom");
		Part_Dress1_String = GetCachedPart("Dress1_String");
		Part_Dress1_Chest = GetCachedPart("Dress1_Chest");
		Part_Dress1_Waist = GetCachedPart("Dress1_Waist");
		Part_CumOutsideBelly = GetCachedPart("CumOutsideBelly");
		Part_CumOutsideInnerThighLowerL = GetCachedPart("CumOutsideInnerThighLowerL");
		Part_CumOutsideInnerThighL = GetCachedPart("CumOutsideInnerThighL");
		Part_CumOutsideInnerThighL2 = GetCachedPart("CumOutsideInnerThighL2");
		Part_CumOutsideBoobL = GetCachedPart("CumOutsideBoobL");
		Part_CumOutsideBoobLSide = GetCachedPart("CumOutsideBoobLSide");
		Part_CumOutsideBellyR = GetCachedPart("CumOutsideBellyR");
		Part_CumOutsideUnderBoobMiddle = GetCachedPart("CumOutsideUnderBoobMiddle");
		Part_CumOutsideBoobRSide = GetCachedPart("CumOutsideBoobRSide");
		Part_CumOutsideBoobR = GetCachedPart("CumOutsideBoobR");
		Part_CumOutsideInnerThighR = GetCachedPart("CumOutsideInnerThighR");
		Part_CumOutsideInnerThighR2 = GetCachedPart("CumOutsideInnerThighR2");
		Part_CumOutsideFaceRight = GetCachedPart("CumOutsideFaceRight");
		Part_CumOutsideFaceNose = GetCachedPart("CumOutsideFaceNose");
		Part_CumOutsideFaceLeft = GetCachedPart("CumOutsideFaceLeft");
		Part_MCRightHandFondle = GetCachedPart("MCRightHandFondle");
		Part_MCLeftHandFondle = GetCachedPart("MCLeftHandFondle");
		Part_SkinBraChestR = GetCachedPart("SkinBraChestR");
		Part_SkinBraChestL = GetCachedPart("SkinBraChestL");
		Part_ShadowBoobL = GetCachedPart("ShadowBoobL");
		Part_ShadowBoobR = GetCachedPart("ShadowBoobR");
		Part_Headband = GetCachedPart("Headband");
		Part_NavelPiercing = GetCachedPart("NavelPiercing");
		Part_NipPiercingRight = GetCachedPart("NipPiercingRight");
		Part_NipPiercingLeft = GetCachedPart("NipPiercingLeft");
		Part_H0_FrontClippedUp_Bang = GetCachedPart("H0_FrontClippedUp_Bang");
		Part_H0_FrontClippedUp_HairClip = GetCachedPart("H0_FrontClippedUp_HairClip");
		Part_BikiniBot_A_StrapL = GetCachedPart("BikiniBot_A_StrapL");
		Part_BikiniBot_A_StrapR = GetCachedPart("BikiniBot_A_StrapR");
		Part_BikiniBot_A_Outline = GetCachedPart("BikiniBot_A_Outline");
		Part_BikiniBot_M_Cloth = GetCachedPart("BikiniBot_M_Cloth");
		Part_BikiniTop_A_StrapR = GetCachedPart("BikiniTop_A_StrapR");
		Part_BikiniTop_A_OutlineR = GetCachedPart("BikiniTop_A_OutlineR");
		Part_BikiniTop_M_CupR = GetCachedPart("BikiniTop_M_CupR");
		Part_BikiniTop_A_StrapL = GetCachedPart("BikiniTop_A_StrapL");
		Part_BikiniTop_A_OutlineL = GetCachedPart("BikiniTop_A_OutlineL");
		Part_BikiniTop_M_CupL = GetCachedPart("BikiniTop_M_CupL");
		Part_BikiniTop_A_Middle = GetCachedPart("BikiniTop_A_Middle");
		coloringGroups[ColoringGroupEnum.Hair0_Top] = new ColoringGroup(ColoringGroupEnum.Hair0_Top, new List<Part> { Part_H0_Top_LeafR, Part_H0_Top_LeafL });
		coloringGroups[ColoringGroupEnum.Hair0_Back] = new ColoringGroup(ColoringGroupEnum.Hair0_Back, new List<Part> { Part_H0_Back });
		coloringGroups[ColoringGroupEnum.Hair0_Braid] = new ColoringGroup(ColoringGroupEnum.Hair0_Braid, new List<Part> { Part_H0_Braid });
		coloringGroups[ColoringGroupEnum.Hair0_Braid_HairTieAccessory] = new ColoringGroup(ColoringGroupEnum.Hair0_Braid_HairTieAccessory, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair0_Side] = new ColoringGroup(ColoringGroupEnum.Hair0_Side, new List<Part> { Part_H0_Side_BangL, Part_H0_Side_BangR, Part_H0_Side });
		coloringGroups[ColoringGroupEnum.Hair0_SideAlt] = new ColoringGroup(ColoringGroupEnum.Hair0_SideAlt, new List<Part> { Part_H0_SideAlt_Strand });
		coloringGroups[ColoringGroupEnum.Hair0_Front] = new ColoringGroup(ColoringGroupEnum.Hair0_Front, new List<Part> { Part_H0_Front_Bang });
		coloringGroups[ColoringGroupEnum.Hair0_FrontClippedUp_Bang] = new ColoringGroup(ColoringGroupEnum.Hair0_FrontClippedUp_Bang, new List<Part> { Part_H0_FrontClippedUp_Bang });
		coloringGroups[ColoringGroupEnum.Hair0_FrontClippedUp_HairClip] = new ColoringGroup(ColoringGroupEnum.Hair0_FrontClippedUp_HairClip, new List<Part> { Part_H0_FrontClippedUp_HairClip });
		coloringGroups[ColoringGroupEnum.Hair1_Top] = new ColoringGroup(ColoringGroupEnum.Hair1_Top, new List<Part> { Part_H1_Top_Ahoge });
		coloringGroups[ColoringGroupEnum.Hair1_Front] = new ColoringGroup(ColoringGroupEnum.Hair1_Front, new List<Part> { Part_H1_Front_Bang });
		coloringGroups[ColoringGroupEnum.Hair1_Side] = new ColoringGroup(ColoringGroupEnum.Hair1_Side, new List<Part> { Part_H1_Side_L2, Part_H1_Side_L, Part_H1_Side_Top });
		coloringGroups[ColoringGroupEnum.Hair1_Back] = new ColoringGroup(ColoringGroupEnum.Hair1_Back, new List<Part> { Part_H1_BackL, Part_H1_Back_R2, Part_H1_Back_R1 });
		coloringGroups[ColoringGroupEnum.Hair2_Front] = new ColoringGroup(ColoringGroupEnum.Hair2_Front, new List<Part> { Part_H2_Front_Bang });
		coloringGroups[ColoringGroupEnum.Hair3_Front] = new ColoringGroup(ColoringGroupEnum.Hair3_Front, new List<Part> { Part_H3_Front });
		coloringGroups[ColoringGroupEnum.Hair3_Side] = new ColoringGroup(ColoringGroupEnum.Hair3_Side, new List<Part> { Part_H3_Side });
		coloringGroups[ColoringGroupEnum.Hair3_Top] = new ColoringGroup(ColoringGroupEnum.Hair3_Top, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair3_Back] = new ColoringGroup(ColoringGroupEnum.Hair3_Back, new List<Part> { Part_H3_Back_Top, Part_H3_Back });
		coloringGroups[ColoringGroupEnum.Hair4_Ponytail] = new ColoringGroup(ColoringGroupEnum.Hair4_Ponytail, new List<Part> { Part_H4_Tail });
		coloringGroups[ColoringGroupEnum.Hair4_KnotAccessory] = new ColoringGroup(ColoringGroupEnum.Hair4_KnotAccessory, new List<Part> { Part_H4_Knot });
		coloringGroups[ColoringGroupEnum.Skirt] = new ColoringGroup(ColoringGroupEnum.Skirt, new List<Part> { Part_Skirt });
		coloringGroups[ColoringGroupEnum.Shirt] = new ColoringGroup(ColoringGroupEnum.Shirt, new List<Part> { Part_Plainshirt_back, Part_Plainshirt_front, Part_PlainShirt_Front, Part_PlainShirt_FrontNoBras, Part_PlainShirt_Base, Part_PlainShirt_SleeveL, Part_PlainShirt_SleeveR, Part_PlainShirt_Back });
		coloringGroups[ColoringGroupEnum.ShirtShadow] = new ColoringGroup(ColoringGroupEnum.ShirtShadow, new List<Part>());
		coloringGroups[ColoringGroupEnum.StockingL] = new ColoringGroup(ColoringGroupEnum.StockingL, new List<Part> { Part_StockingL });
		coloringGroups[ColoringGroupEnum.StockingR] = new ColoringGroup(ColoringGroupEnum.StockingR, new List<Part> { Part_StockingR });
		coloringGroups[ColoringGroupEnum.Panties] = new ColoringGroup(ColoringGroupEnum.Panties, new List<Part> { Part_Panties_front, Part_Panties_back });
		coloringGroups[ColoringGroupEnum.Bra] = new ColoringGroup(ColoringGroupEnum.Bra, new List<Part> { Part_BraFrontR, Part_BraFrontL, Part_BrasUpperL, Part_BrasUpperR, Part_BraMid, Part_BraLower });
		coloringGroups[ColoringGroupEnum.Hoodie] = new ColoringGroup(ColoringGroupEnum.Hoodie, new List<Part>
		{
			Part_LongHoodieRaisedArmFrontR, Part_LongHoodieRaisedArmFrontL, Part_LongHoodieRaisedArmBackR, Part_LongHoodieRaisedArmBackL, Part_LongHoodieHoodFront, Part_LongHoodieChest, Part_LongHoodieFront, Part_LongHoodieUppperArmR, Part_LongHoodieUppperArmL, Part_LongHoodieLowerArmR,
			Part_LongHoodieLowerArmL, Part_LongHoodieHoodBack
		});
		coloringGroups[ColoringGroupEnum.HoodieStrings] = new ColoringGroup(ColoringGroupEnum.HoodieStrings, new List<Part> { Part_AltLongHoodieStringR, Part_AltLongHoodieStringL });
		coloringGroups[ColoringGroupEnum.Pants] = new ColoringGroup(ColoringGroupEnum.Pants, new List<Part> { Part_BPantsCrotch, Part_BPantsUpperR, Part_BPantsUpperL, Part_BPantsLowerR, Part_BPantsLowerL });
		coloringGroups[ColoringGroupEnum.PantsBelt] = new ColoringGroup(ColoringGroupEnum.PantsBelt, new List<Part> { Part_AltBPantsBelt2, Part_AltBPantsBelt });
		coloringGroups[ColoringGroupEnum.Dress1] = new ColoringGroup(ColoringGroupEnum.Dress1, new List<Part> { Part_Dress1_Bottom, Part_Dress1_String, Part_Dress1_Chest, Part_Dress1_Waist });
		coloringGroups[ColoringGroupEnum.Dress2Alt] = new ColoringGroup(ColoringGroupEnum.Dress2Alt, new List<Part>());
		coloringGroups[ColoringGroupEnum.Dress2] = new ColoringGroup(ColoringGroupEnum.Dress2, new List<Part> { Part_Dress2Chest, Part_Dress2Middle, Part_Dress2Back, Part_Dress2SleeveR, Part_Dress2SleeveL });
		coloringGroups[ColoringGroupEnum.ShoeR] = new ColoringGroup(ColoringGroupEnum.ShoeR, new List<Part> { Part_Shoe_R });
		coloringGroups[ColoringGroupEnum.ShoeL] = new ColoringGroup(ColoringGroupEnum.ShoeL, new List<Part> { Part_Shoe_L });
		coloringGroups[ColoringGroupEnum.PointyEars] = new ColoringGroup(ColoringGroupEnum.PointyEars, new List<Part> { Part_PointyEarR, Part_PointyEarL });
		coloringGroups[ColoringGroupEnum.WizardHat] = new ColoringGroup(ColoringGroupEnum.WizardHat, new List<Part> { Part_WizardHat });
		coloringGroups[ColoringGroupEnum.BellChoker] = new ColoringGroup(ColoringGroupEnum.BellChoker, new List<Part> { Part_BellChokerMain, Part_BellChokerBell });
		coloringGroups[ColoringGroupEnum.CuteBow] = new ColoringGroup(ColoringGroupEnum.CuteBow, new List<Part> { Part_CuteBow });
		coloringGroups[ColoringGroupEnum.PumpkinHairPin] = new ColoringGroup(ColoringGroupEnum.PumpkinHairPin, new List<Part>());
		coloringGroups[ColoringGroupEnum.Headband] = new ColoringGroup(ColoringGroupEnum.Headband, new List<Part> { Part_Headband });
		coloringGroups[ColoringGroupEnum.NipplePiercingL] = new ColoringGroup(ColoringGroupEnum.NipplePiercingL, new List<Part> { Part_NipPiercingLeft });
		coloringGroups[ColoringGroupEnum.NipplePiercingR] = new ColoringGroup(ColoringGroupEnum.NipplePiercingR, new List<Part> { Part_NipPiercingRight });
		coloringGroups[ColoringGroupEnum.NavelPiercing] = new ColoringGroup(ColoringGroupEnum.NavelPiercing, new List<Part> { Part_NavelPiercing });
		coloringGroups[ColoringGroupEnum.BikiniTop_Main] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Main, new List<Part> { Part_BikiniTop_M_CupR, Part_BikiniTop_M_CupL });
		coloringGroups[ColoringGroupEnum.BikiniTop_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Alt, new List<Part> { Part_BikiniTop_A_StrapR, Part_BikiniTop_A_OutlineR, Part_BikiniTop_A_StrapL, Part_BikiniTop_A_OutlineL, Part_BikiniTop_A_Middle });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Main] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Main, new List<Part> { Part_BikiniBot_M_Cloth });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Alt, new List<Part> { Part_BikiniBot_A_StrapL, Part_BikiniBot_A_StrapR, Part_BikiniBot_A_Outline });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Uncolored] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Uncolored, new List<Part>());
		coloringGroups[ColoringGroupEnum.IrisL] = new ColoringGroup(ColoringGroupEnum.IrisL, new List<Part> { Part_PupilL });
		coloringGroups[ColoringGroupEnum.IrisR] = new ColoringGroup(ColoringGroupEnum.IrisR, new List<Part> { Part_PupilR });
		coloringGroups[ColoringGroupEnum.PupilL] = new ColoringGroup(ColoringGroupEnum.PupilL, new List<Part> { Part_Iris1L });
		coloringGroups[ColoringGroupEnum.PupilR] = new ColoringGroup(ColoringGroupEnum.PupilR, new List<Part> { Part_Iris1R });
		coloringGroups[ColoringGroupEnum.Sclera] = new ColoringGroup(ColoringGroupEnum.Sclera, new List<Part> { Part_EyeBall });
		coloringGroups[ColoringGroupEnum.Eyelash] = new ColoringGroup(ColoringGroupEnum.Eyelash, new List<Part>());
		coloringGroups[ColoringGroupEnum.HeartPupils] = new ColoringGroup(ColoringGroupEnum.HeartPupils, new List<Part> { Part_Iris2L, Part_Iris2R });
		coloringGroups[ColoringGroupEnum.EyeBrows] = new ColoringGroup(ColoringGroupEnum.EyeBrows, new List<Part> { Part_BrowL, Part_BrowR });
		coloringGroups[ColoringGroupEnum.Nipples] = new ColoringGroup(ColoringGroupEnum.Nipples, new List<Part> { Part_NippleR2, Part_NippleL2 });
		coloringGroups[ColoringGroupEnum.Vagina] = new ColoringGroup(ColoringGroupEnum.Vagina, new List<Part>());
		coloringGroups[ColoringGroupEnum.Xray] = new ColoringGroup(ColoringGroupEnum.Xray, new List<Part>());
		coloringGroups[ColoringGroupEnum.Anus] = new ColoringGroup(ColoringGroupEnum.Anus, new List<Part>());
		coloringGroups[ColoringGroupEnum.Teeth] = new ColoringGroup(ColoringGroupEnum.Teeth, new List<Part> { Part_TeethUpper, Part_TeethLower });
		coloringGroups[ColoringGroupEnum.Mouth] = new ColoringGroup(ColoringGroupEnum.Mouth, new List<Part> { Part_Tounge, Part_InnerMouthTounge, Part_InnerMouth });
		coloringGroups[ColoringGroupEnum.Boobs] = new ColoringGroup(ColoringGroupEnum.Boobs, new List<Part> { Part_SkinBreastL, Part_SkinBreastR });
		coloringGroups[ColoringGroupEnum.Ass] = new ColoringGroup(ColoringGroupEnum.Ass, new List<Part> { Part_SkinButtL, Part_SkinButtR });
		coloringGroups[ColoringGroupEnum.Pelvic] = new ColoringGroup(ColoringGroupEnum.Pelvic, new List<Part> { Part_SkinPelvis });
		coloringGroups[ColoringGroupEnum.Thigh] = new ColoringGroup(ColoringGroupEnum.Thigh, new List<Part>());
		coloringGroups[ColoringGroupEnum.Tummy] = new ColoringGroup(ColoringGroupEnum.Tummy, new List<Part>());
		coloringGroups[ColoringGroupEnum.Skin] = new ColoringGroup(ColoringGroupEnum.Skin, new List<Part>
		{
			Part_SkinNose, Part_SkinFace, Part_SkinArmL, Part_SkinArmR, Part_SkinNeck, Part_SkinBodyBack, Part_SkinEyelidClosedL, Part_SkinEyelidClosedR, Part_SkinEyelidL, Part_SkinEyelidR,
			Part_SkinEyelid2L, Part_SkinEyelid2R, Part_SkinBodyFront, Part_SkinThighR, Part_SkinThighL, Part_SkinArmLShoulderCuddleDown, Part_SkinArmLShoulderCuddleUp, Part_SkinMouthThumbSuck, Part_SkinLipUpper, Part_SkinLipLower,
			Part_SkinLipUpperToungeOut, Part_SkinLipLowerToungeOut, Part_SkinBraChestR, Part_SkinBraChestL
		});
		coloringGroups[ColoringGroupEnum.Blush] = new ColoringGroup(ColoringGroupEnum.Blush, new List<Part> { Part_blush_2, Part_blush });
		coloringGroups[ColoringGroupEnum.SharpTeeth] = new ColoringGroup(ColoringGroupEnum.SharpTeeth, new List<Part> { Part_SharpTeethUpper, Part_SharpTeethLower });
		coloringGroups[ColoringGroupEnum.CatEarMain] = new ColoringGroup(ColoringGroupEnum.CatEarMain, new List<Part> { Part_CatearFrontR, Part_CatearFrontL, Part_CatearBackR, Part_CatearBackL });
		coloringGroups[ColoringGroupEnum.CatEarSecondary] = new ColoringGroup(ColoringGroupEnum.CatEarSecondary, new List<Part> { Part_CatearMidL, Part_CatearMidR });
		coloringGroups[ColoringGroupEnum.LegLFoot] = new ColoringGroup(ColoringGroupEnum.LegLFoot, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegL] = new ColoringGroup(ColoringGroupEnum.LegL, new List<Part> { Part_AttachLegLKnee, Part_AttachLegLLower, Part_AttachLegLFeet, Part_AttachLegLThigh });
		coloringGroups[ColoringGroupEnum.LegRFoot] = new ColoringGroup(ColoringGroupEnum.LegRFoot, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegR] = new ColoringGroup(ColoringGroupEnum.LegR, new List<Part> { Part_AttachLegRKnee, Part_AttachLegRLower, Part_AttachLegRFeet, Part_AttachLegRThigh });
		coloringGroups[ColoringGroupEnum.ArmL] = new ColoringGroup(ColoringGroupEnum.ArmL, new List<Part>
		{
			Part_AttachArmLLowerArmUp, Part_AttachArmLHandUp3, Part_AttachArmLHandUp2, Part_AttachArmLHandUp1, Part_AttachArmLLowerArmDown, Part_AttachArmL, Part_AttachArmLHandDown1, Part_AttachArmLHandDown2, Part_AttachArmLLowerCuddle, Part_AttachArmLHandCuddle,
			Part_AttachArmLUpperCuddle, Part_AttachArmLLowerCuddleUp, Part_AttachArmLUpperCuddleUp
		});
		coloringGroups[ColoringGroupEnum.ArmR] = new ColoringGroup(ColoringGroupEnum.ArmR, new List<Part> { Part_AttachArmRLowerArmUp, Part_AttachArmRHandUp3, Part_AttachArmRHandUp2, Part_AttachArmRHandUp1, Part_AttachArmRLowerArmDown, Part_AttachArmR2, Part_AttachArmRHandDown1, Part_AttachArmRHandDown2 });
		coloringGroups[ColoringGroupEnum.Tail] = new ColoringGroup(ColoringGroupEnum.Tail, new List<Part> { Part_TailMain, Part_TailMain2, Part_TailMain3, Part_TailMain4, Part_TailMain5, Part_TailMain6, Part_TailMain7, Part_TailMain8, Part_TailMain9, Part_TailMain10 });
		coloringGroups[ColoringGroupEnum.TailPlug] = new ColoringGroup(ColoringGroupEnum.TailPlug, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hologram] = new ColoringGroup(ColoringGroupEnum.Hologram, new List<Part> { Part_HairHologram });
		coloringGroups[ColoringGroupEnum.OtherAttachments] = new ColoringGroup(ColoringGroupEnum.OtherAttachments, new List<Part>());
		coloringGroups[ColoringGroupEnum.MCSkin] = new ColoringGroup(ColoringGroupEnum.MCSkin, new List<Part>
		{
			Part_headpet_1, Part_headpet_2, Part_headpet_3, Part_MCHandHolding, Part_MCHandFaceRubFront, Part_MCHandFaceRubBack, Part_MCHand, Part_MCForearmHandholding, Part_MCForearmHandholding2, Part_MCHand2,
			Part_MCRightHandFondle, Part_MCLeftHandFondle
		});
		coloringGroups[ColoringGroupEnum.MCCockSkin] = new ColoringGroup(ColoringGroupEnum.MCCockSkin, new List<Part>());
		coloringGroups[ColoringGroupEnum.MCCockTip] = new ColoringGroup(ColoringGroupEnum.MCCockTip, new List<Part>());
		coloringGroups[ColoringGroupEnum.Shadow] = new ColoringGroup(ColoringGroupEnum.Shadow, new List<Part> { Part_BlanketShadow, Part_ShadowBoobL, Part_ShadowBoobR });
		coloringGroups[ColoringGroupEnum.Highlight] = new ColoringGroup(ColoringGroupEnum.Highlight, new List<Part> { Part_highlight });
		coloringGroups[ColoringGroupEnum.Mask] = new ColoringGroup(ColoringGroupEnum.Mask, new List<Part> { Part_OmegaMeterMask, Part_ThumbMask });
		coloringGroups[ColoringGroupEnum.Moddable] = new ColoringGroup(ColoringGroupEnum.Moddable, new List<Part> { Part_ModdableHairBack, Part_ModdableHairFront, Part_ModdableFace, Part_ModdableBackLower, Part_ModdableBack, Part_ModdableShirtLogo, Part_ModdableHoodieLogo, Part_ModdableShirtLeftSleeveLogo, Part_ModdableShirtRightSleeveLogo, Part_ModdablePantiesLogo });
		coloringGroups[ColoringGroupEnum.HitArea] = new ColoringGroup(ColoringGroupEnum.HitArea, new List<Part> { HitAreaOpenMouth, HitAreaCloseMouth, HitAreaHeadpat, HitAreaFaceStroke, HitAreaThumbMouth, HitAreaBoobL, HitAreaBoobR, HitAreaBoobsBoth });
		coloringGroups[ColoringGroupEnum.Cum] = new ColoringGroup(ColoringGroupEnum.Cum, new List<Part>
		{
			Part_CumOutsideBelly, Part_CumOutsideInnerThighLowerL, Part_CumOutsideInnerThighL, Part_CumOutsideInnerThighL2, Part_CumOutsideBoobL, Part_CumOutsideBoobLSide, Part_CumOutsideBellyR, Part_CumOutsideUnderBoobMiddle, Part_CumOutsideBoobRSide, Part_CumOutsideBoobR,
			Part_CumOutsideInnerThighR, Part_CumOutsideInnerThighR2, Part_CumOutsideFaceRight, Part_CumOutsideFaceNose, Part_CumOutsideFaceLeft
		});
		coloringGroups[ColoringGroupEnum.Rest] = new ColoringGroup(ColoringGroupEnum.Rest, new List<Part>
		{
			Part_lines, Part_barcode, Part_BlanketFront, Part_BlanketBack, Part_OmegaMeterInner, Part_OmegaMeter, Part_BlanketCuddle, Part_PillowFront, Part_PillowBack, Part_ThumbSuckThumb,
			Part_ThumbSuck, Part_Saliva2, Part_Saliva1
		});
	}
}
