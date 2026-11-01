using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fungus;
using IC.AutomaticReference;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Physics;
using Live2D.Cubism.Framework.Raycasting;
using Live2D.Cubism.Rendering;
using Unity.Collections;
using UnityEngine;

public sealed class Live2DShowerController : Live2DLewdSceneController, ILive2DController_Lewd_CumInsideMain, ILive2DController_Lewd_CumInsideAnal, ILive2DController_Lewd_CumInsideWomb, ILive2DController_Lewd_CumInsideVagina, ILive2DController_Lewd_CumLeakAdvanced, ILive2DController_Lewd_CumLeakBasic, ILive2DController_Lewd_BotOrgasm, ILive2DController_Lewd_CumShotX, ILive2DController_Lewd_CumShotY, ILive2DController_Lewd_BotTense, ILive2DController_Lewd_CumShotCommon, ILive2DController_Lewd_CumShotBasic, ILive2DController_Lewd_CumShotLength, ILive2DController_MainFace, ILive2DController_Mouth, ILive2DController_EyeMovement, ILive2DController_EyeLMovement, ILive2DController_EyeRMovement, ILive2DController_EyeLEmotes, ILive2DController_EyeLCloseAndShock, ILive2DController_BrowLY, ILive2DController_BrowLRot, ILive2DController_BrowLEmote, ILive2DController_EyeREmotes, ILive2DController_EyeRCloseAndShock, ILive2DController_BrowRY, ILive2DController_BrowRRot, ILive2DController_BrowREmote, ILive2DController_BlushAndHeartEyes, ILive2DController_EyesSmugEmote, ILive2DController_HeadAngles, ILive2DController_HeadX, ILive2DController_HeadY, ILive2DController_HeadZ, ILive2DController_Manager_FrickStaticGuiManager, ILive2DController_Manager_InsertionManager, ILive2DController_Manager_CockManager, ILive2DController_Manager_Camera, ILive2DController_BodyMod_ThighSize, ILive2DController_BodyMod_BoobSize, ILive2DController_BodyMod_AssSize, ILive2DController_Headpat
{
	public enum ShowerSceneCameraEnum
	{
		NoSetting,
		SideCamera,
		POVCamera
	}

	private class SpankLimbUsage : ILimbUsageWithInteractable, ILimbUsage
	{
		private const float SpankTouchValue = 0.3f;

		private bool spankFollowedThrough;

		private int symmetryIndex;

		private Live2DShowerController c;

		public LimbUsageChoiceEnum TargetLimbUsage
		{
			get
			{
				if (symmetryIndex != 0)
				{
					return LimbUsageChoiceEnum.RightArm;
				}
				return LimbUsageChoiceEnum.LeftArm;
			}
		}

		public SpankLimbUsage(int symmetryIndex, Live2DShowerController c)
		{
			this.symmetryIndex = symmetryIndex;
			this.c = c;
		}

		public void Start()
		{
			spankFollowedThrough = false;
			c.SymParam_LR_ParamSpankAnim[symmetryIndex].ResetToDefault();
			c.SymParam_LR_ParamSpankAnim[symmetryIndex].FinishMove();
			c.SymParam_LR_ParamSpankEnable[symmetryIndex].SetMaxValue();
			c.SymParam_LR_ParamSpankY[symmetryIndex].ResetToDefault();
			c.SymParam_LR_ParamSpankY[symmetryIndex].FinishMove();
		}

		public void Update()
		{
		}

		public void FixedUpdate()
		{
		}

		public void End(LetGoLimbsCaller caller)
		{
			c.SymParam_LR_ParamSpankEnable[symmetryIndex].SetMinValue();
		}

		public void ContinueHold(Vector2 mouseMove)
		{
			c.SymParam_LR_ParamSpankY[symmetryIndex].Add(mouseMove.y * 5f);
			if (spankFollowedThrough)
			{
				if (c.SymParam_LR_ParamSpankAnim[symmetryIndex].IsRealValueApproximatelyMaximumValue)
				{
					c.LetGoLimbs(this, LetGoLimbsCaller.Outside);
				}
				return;
			}
			if (c.SymParam_LR_ParamSpankAnim[symmetryIndex].GetRealValue() >= 0.3f)
			{
				float lastFrameRealSpeed = c.SymParam_LR_ParamSpankAnim[symmetryIndex].GetLastFrameRealSpeed();
				float num = MathF.Tanh(lastFrameRealSpeed / 10f);
				float num2 = Mathf.Lerp(0f, 90f, num) * ((float)Math.PI / 180f);
				if (lastFrameRealSpeed > 5f)
				{
					if (lastFrameRealSpeed < 10f)
					{
						R.Sound.Frick_spank_Light_All.PlayRandomWithPitch(num * 0.75f, UnityEngine.Random.Range(0.99f, 1.01f) + num * 0.02f);
					}
					else
					{
						R.Sound.Frick_spank_Normal_All.PlayRandomWithPitch(0.75f + num * 0.3f, UnityEngine.Random.Range(0.99f, 1.01f) + num * 0.02f);
					}
					c.FireEvent(InteractionMemoryEventEnum.Spank);
					Jerk(num2);
					spankFollowedThrough = true;
					c.SymParam_LR_ParamSpankAnim[symmetryIndex].SetMaxValue();
					return;
				}
				c.SymParam_LR_ParamSpankAnim[symmetryIndex].ClampValue(-1f, 0.3f);
				if (c.SymParam_LR_ParamSpankAnim[symmetryIndex].GetRealValueLastFrame() < 0.3f)
				{
					Jerk(num2 * 0.5f);
					if (lastFrameRealSpeed > 1f)
					{
						R.Sound.Frick_spank_Light_All.PlayRandomWithPitch(num * 0.25f, UnityEngine.Random.Range(0.99f, 1.01f) + num * 0.02f);
					}
				}
			}
			float num3 = ((symmetryIndex == 0) ? 1 : (-1));
			c.SymParam_LR_ParamSpankAnim[symmetryIndex].Add(num3 * mouseMove.x * 7f);
			void Jerk(float angle)
			{
				if (symmetryIndex == 0)
				{
					c.PhysicsSubRig_ButtLY.JerkPhysicsInDirection(angle);
				}
				else
				{
					c.PhysicsSubRig_ButtRY.JerkPhysicsInDirection(angle);
				}
			}
		}

		public void ContinueHoldFixed()
		{
		}
	}

	private class WaistGrabLimbUsage : ILimbUsageWithInteractable, ILimbUsage
	{
		private int symmetryIndex;

		private Live2DShowerController c;

		public LimbUsageChoiceEnum TargetLimbUsage
		{
			get
			{
				if (symmetryIndex != 0)
				{
					return LimbUsageChoiceEnum.RightArm;
				}
				return LimbUsageChoiceEnum.LeftArm;
			}
		}

		public WaistGrabLimbUsage(int symmetryIndex, Live2DShowerController c)
		{
			this.symmetryIndex = symmetryIndex;
			this.c = c;
		}

		public void Start()
		{
			c.SymParam_LR_ParamWaistGrabEnable[symmetryIndex].SetMaxValue();
		}

		public void Update()
		{
		}

		public void FixedUpdate()
		{
		}

		public void End(LetGoLimbsCaller caller)
		{
			c.SymParam_LR_ParamWaistGrabEnable[symmetryIndex].SetMinValue();
		}

		public void ContinueHold(Vector2 mouseMove)
		{
			if (c.CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2) && !c.CockManager.IsFlacid)
			{
				o2.ThrustMovementAccumulator -= mouseMove.y * 5f;
				o2.SidewaysMovementAccumulator += mouseMove.x * 30f;
			}
			c.ParamBotBodyStraighten.Value += mouseMove.y * 1.5f;
			c.ButtMovement.Value -= mouseMove.y * 2f;
		}

		public void ContinueHoldFixed()
		{
		}
	}

	[NonSerialized]
	private ParticleSystemGroup _flyOffParticleSystem;

	[NonSerialized]
	private ParticleSystem _hideBrushParticleSystem;

	public const float DistanceBetweenSoap = 0.32f;

	public const float SquishedDistanceBetweenSoap = 0.10666666f;

	private const int MaxSoapCount = 16384;

	private float _soapUsed;

	private const float MaxSoapUsed = 12288f;

	private const int n = 4;

	private CameraRectSpatialHash2D<SoapScript> _soapSpatialHash = new CameraRectSpatialHash2D<SoapScript>(64, 36);

	private Dictionary<Part, int> _cleanedAreas = new Dictionary<Part, int>();

	private const int RequiredCleanAreaCount = 13;

	private const int RequiredCleanAreaCleanCount = 500;

	private const int RequiredCleanAreaCleanCountMax = 1000;

	private const float EyeBoundsGrowAmount = 0.1f;

	private ShowerSceneCameraEnum _currentSceneCamera;

	private CancellationTokenSource _cameraSwitchCancellationTokenSource;

	public const string POVCameraPartPostfix = "POV";

	private CockLimbUsage<Live2DShowerController> _cockFuckLimbUsage;

	private BasicInsertionHandleFuckAxis VaginaCockFuckAxis;

	private BasicInsertionHandleFuckAxis VaginaFingeringFuckAxis;

	private BasicInsertionHandleFuckAxis AnalCockFuckAxis;

	private BasicInsertionHandleFuckAxis AnalFingeringFuckAxis;

	private BasicInsertionHandleFuckAxis ThighjobCockFuckAxis;

	public override ControllerEnum ControllerEnum => ControllerEnum.Shower;

	public override SceneEnumFlag SceneEnum => SceneEnumFlag.Shower;

	public ShowerSceneCameraEnum CurrentSceneCamera => _currentSceneCamera;

	public override string CameraCumPartPostfix
	{
		get
		{
			if (_currentSceneCamera != ShowerSceneCameraEnum.SideCamera)
			{
				return "POV";
			}
			return "";
		}
	}

	public InsertionHandler VaginaInsertionHandler { get; private set; }

	public InsertionHandler AnalInsertionHandler { get; private set; }

	public InsertionHandler ThighjobInsertionHandler { get; private set; }

	public LimbUsageWithInteractable<Live2DShowerController> BoobFondleLimbUsage { get; private set; }

	public Live2DInteractableLimbUsage<Live2DShowerController> BoobFondleInteractable { get; private set; }

	public LimbUsageWithInteractable<Live2DShowerController> AssFondleLimbUsage { get; private set; }

	public LimbUsageWithInteractable<Live2DShowerController> AssGrabLimbUsage { get; private set; }

	public Live2DInteractableLimbUsage<Live2DShowerController> AssFondleInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DShowerController> SpankLInteractable { get; private set; }

	public Live2DInteractableLimbUsage<Live2DShowerController> SpankRInteractable { get; private set; }

	public LimbUsageWithInteractable<Live2DShowerController> SpreadPussyLimbUsage { get; private set; }

	public Live2DInteractableLimbUsage<Live2DShowerController> SpreadPussyInteractable { get; private set; }

	public LimbUsageWithInteractable<Live2DShowerController> SpreadAnusLimbUsage { get; private set; }

	public Live2DInteractableLimbUsage<Live2DShowerController> SpreadAnusInteractable { get; private set; }

	public FrickStaticGuiManager FrickStaticGuiManager { get; private set; }

	public InsertionManager InsertionManager { get; private set; }

	public ICockManager CockManager { get; private set; }

	public ICameraManager CameraManager { get; private set; }

	public IAdvancedCumLeakManager AdvancedCumLeakManager { get; private set; }

	public Live2DParameter ParamBoobHandX { get; private set; }

	public Live2DParameter ParamBoobHandY { get; private set; }

	public Live2DParameter ParamBoobHandGrab { get; private set; }

	public Live2DParameter ParamBoobHandEnable { get; private set; }

	public Live2DParameter ParamAssHandLX { get; private set; }

	public Live2DParameter ParamAssHandLY { get; private set; }

	public Live2DParameter ParamAssHandLGrab { get; private set; }

	public Live2DParameter ParamAssHandLEnable { get; private set; }

	public Live2DParameter ParamClitRubEnable { get; private set; }

	public Live2DParameter ParamClitRubZ { get; private set; }

	public Live2DParameter ParamVaginaClitMoveX { get; private set; }

	public Live2DParameter ParamVaginaClitMoveY { get; private set; }

	public Live2DParameter ParamWaistGrabLEnable { get; private set; }

	public Live2DParameter ParamWaistGrabREnable { get; private set; }

	public Live2DParameter ParamSpreadPussyEnable { get; private set; }

	public Live2DParameter ParamSpreadAnusEnable { get; private set; }

	public Live2DParameter ParamSpankLEnable { get; private set; }

	public Live2DParameter ParamSpankLY { get; private set; }

	public Live2DParameter ParamSpankLAnim { get; private set; }

	public Live2DParameter ParamSpankREnable { get; private set; }

	public Live2DParameter ParamSpankRY { get; private set; }

	public Live2DParameter ParamSpankRAnim { get; private set; }

	public Live2DParameter ParamVaginaFingeringEnable { get; private set; }

	public Live2DParameter ParamVaginaFingeringInsert { get; private set; }

	public Live2DParameter ParamVaginaFingeringRotation { get; private set; }

	public Live2DParameter ParamAnusFingeringEnable { get; private set; }

	public Live2DParameter ParamAnusFingeringInsert { get; private set; }

	public Live2DParameter ParamAnusFingeringRotation { get; private set; }

	public Live2DParameter ParamHeadpat { get; private set; }

	public Live2DParameter ParamHeadpatX { get; private set; }

	public Live2DParameter ParamHeadpatY { get; private set; }

	public Live2DParameter ParamEyeballLX { get; private set; }

	public Live2DParameter ParamEyeballLY { get; private set; }

	public Live2DParameter ParamBlush { get; private set; }

	public Live2DParameter Bluescreen { get; private set; }

	public Live2DParameter ParamEyeballRX { get; private set; }

	public Live2DParameter ParamEyeballRY { get; private set; }

	public Live2DParameter ParamMouthOpen { get; private set; }

	public Live2DParameter ParamMouthForm { get; private set; }

	public Live2DParameter ParamEyeLShock { get; private set; }

	public Live2DParameter ParamTense { get; private set; }

	public Live2DParameter ParamEyeRShock { get; private set; }

	public Live2DParameter ParamHeart { get; private set; }

	public Live2DParameter ParamEyeExpression { get; private set; }

	public Live2DParameter ParamBrowLY { get; private set; }

	public Live2DParameter ParamBrowRY { get; private set; }

	public Live2DParameter ParamBrowLRot { get; private set; }

	public Live2DParameter ParamBrowRRot { get; private set; }

	public Live2DParameter ParamBrowLEmote { get; private set; }

	public Live2DParameter ParamBrowREmote { get; private set; }

	public Live2DParameter ParamHeadX { get; private set; }

	public Live2DParameter ParamHeadY { get; private set; }

	public Live2DParameter ParamHeadZ { get; private set; }

	public Live2DParameter ParamCumInside { get; private set; }

	public Live2DParameter ParamCumInsideWomb { get; private set; }

	public Live2DParameter ParamCumInsideAnal { get; private set; }

	public Live2DParameter ParamCumLeakSource { get; private set; }

	public Live2DParameter ParamCumLeakAnim { get; private set; }

	public Live2DParameter ParamCumLeakSize { get; private set; }

	public Live2DParameter ParamDick { get; private set; }

	public Live2DParameter ParamThrust { get; private set; }

	public Live2DParameter ParamThrustAnal { get; private set; }

	public Live2DParameter ParamCumShotLengthY { get; private set; }

	public Live2DParameter ParamCumOutsideX { get; private set; }

	public Live2DParameter ParamCumOutsideY { get; private set; }

	public Live2DParameter ParamFlacidity { get; private set; }

	public Live2DParameter ParamHideCumShot { get; private set; }

	public Live2DParameter ParamCumShotAnim { get; private set; }

	public Live2DParameter ParamAnalCockAngle { get; private set; }

	public Live2DParameter ParamVaginaCockAngle { get; private set; }

	public Live2DParameter ParamVaginaOpen { get; private set; }

	public Live2DParameter ParamAnusOpen { get; private set; }

	public Live2DParameter ParamOrgasm { get; private set; }

	public Live2DParameter ParamThighjobEnable { get; private set; }

	public Live2DParameter ParamThrustThighjob { get; private set; }

	public Live2DParameter ParamThighjobCockAngle { get; private set; }

	public Live2DParameter ParamThighjobOpen { get; private set; }

	public Live2DParameter TonguePhysics { get; private set; }

	public Live2DParameter ParamButtPhysicsLY { get; private set; }

	public Live2DParameter ParamButtPhysicsLZ { get; private set; }

	public Live2DParameter ParamButtPhysicsLX { get; private set; }

	public Live2DParameter ParamButtPhysicsRY { get; private set; }

	public Live2DParameter ParamButtPhysicsRZ { get; private set; }

	public Live2DParameter ParamButtPhysicsRX { get; private set; }

	public Live2DParameter BoobPhysicsX { get; private set; }

	public Live2DParameter BoobPhysicsY { get; private set; }

	public Live2DParameter RibcagePhysicsRotation { get; private set; }

	public Live2DParameter HairPhysics { get; private set; }

	public Live2DParameter CumPhysics { get; private set; }

	public Live2DParameter ParamAssSize { get; private set; }

	public Live2DParameter ParamThighSize { get; private set; }

	public Live2DParameter ParamBoobSize { get; private set; }

	public Live2DParameter ParamPerspectiveToggle { get; private set; }

	public Live2DParameter ParamBotBodyStraighten { get; private set; }

	public Live2DParameter ButtMovement { get; private set; }

	public Live2DParameter ButtPressed { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamWaistGrabEnable { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamSpankEnable { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamSpankY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamSpankAnim { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballX { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeShock { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowRot { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowEmote { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamButtPhysicsY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamButtPhysicsZ { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamButtPhysicsX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobsX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Cum { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_BoobsY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Ribcage { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Hair { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Tongue { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_ButtLY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_ButtRY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_ButtRZ { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_ButtLZ { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_ButtLX { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_ButtRX { get; private set; }

	public Part HitAreaBoob { get; private set; }

	public Part HitAreaEyeL { get; private set; }

	public Part HitAreaEyeR { get; private set; }

	public Part HitAreaAss { get; private set; }

	public Part HitAreaMouth { get; private set; }

	public Part HitAreaMC { get; private set; }

	public Part HitAreaButtL_POV { get; private set; }

	public Part HitAreaMC_POV { get; private set; }

	public Part HitAreaButtR_POV { get; private set; }

	public Part HitAreaWaist_POV { get; private set; }

	public Part HitAreaVaginaSpread_POV { get; private set; }

	public Part HitAreaAnusSide_POV { get; private set; }

	public Part HitAreaAnusCenter_POV { get; private set; }

	public Part HitAreaVaginaCenter_POV { get; private set; }

	public Part HitArea_HeadpatPOV { get; private set; }

	public Part HitArea_Eyes_POV { get; private set; }

	public Part HitAreaVaginaClit_POV { get; private set; }

	public Part Part_AttachArmLHand { get; private set; }

	public Part Part_SkinArm { get; private set; }

	public Part Part_AttachArmLLower { get; private set; }

	public Part Part_AttachArmLUpper { get; private set; }

	public Part Part_SkinButtSeamline { get; private set; }

	public Part Part_Thighs { get; private set; }

	public Part Part_AttachLegLThigh { get; private set; }

	public Part Part_SkinButt { get; private set; }

	public Part Part_SkinPelvis { get; private set; }

	public Part Part_AttachChargeHole { get; private set; }

	public Part Part_Thighs2 { get; private set; }

	public Part Part_SkinArm2 { get; private set; }

	public Part Part_SkinBoob2 { get; private set; }

	public Part Part_NippleL { get; private set; }

	public Part Part_SkinButtOutline { get; private set; }

	public Part Part_ButtReverseMask { get; private set; }

	public Part Part_HairHologram { get; private set; }

	public Part Part_H0_Front_Bang { get; private set; }

	public Part Part_H0_SideLeft { get; private set; }

	public Part Part_H0_BackLeft { get; private set; }

	public Part Part_H0_Back { get; private set; }

	public Part Part_SkinEyelidR { get; private set; }

	public Part Part_SkinEyelidL { get; private set; }

	public Part Part_EyelashL2 { get; private set; }

	public Part Part_EyelashLMid4 { get; private set; }

	public Part Part_EyelashLLow2 { get; private set; }

	public Part Part_EyeWhiteL2 { get; private set; }

	public Part Part_EyelashR2 { get; private set; }

	public Part Part_EyeLashRMid3 { get; private set; }

	public Part Part_EyelashRMid2 { get; private set; }

	public Part Part_EyelashRLow2 { get; private set; }

	public Part Part_EyeWhiteR2 { get; private set; }

	public Part Part_SkinNose { get; private set; }

	public Part Part_HairShadowL { get; private set; }

	public Part Part_HairShadowR { get; private set; }

	public Part Part_SkinFace2 { get; private set; }

	public Part Part_SkinNeck2 { get; private set; }

	public Part Part_H0_BackRight { get; private set; }

	public Part Part_DickSkin { get; private set; }

	public Part Part_DickMain { get; private set; }

	public Part Part_CumDick1 { get; private set; }

	public Part Part_CumDick2 { get; private set; }

	public Part Part_SkinRibcage { get; private set; }

	public Part Part_SkinWaistLower { get; private set; }

	public Part Part_AttachArmRHandBack { get; private set; }

	public Part Part_AttachLegRThigh { get; private set; }

	public Part Part_H0_SideAlt_Strand { get; private set; }

	public Part Part_EyelashLMid3 { get; private set; }

	public Part Part_CumOutsideAssSide2 { get; private set; }

	public Part Part_CumOutsideAssBottom { get; private set; }

	public Part Part_CumOutsideAss2 { get; private set; }

	public Part Part_CumOutsideAss { get; private set; }

	public Part Part_CumOutsideThighBack { get; private set; }

	public Part Part_CumOutsideAssSide { get; private set; }

	public Part Part_CumOutsideAssUp { get; private set; }

	public Part Part_CumOutsideBelly { get; private set; }

	public Part Part_CumOutsideAssUp2 { get; private set; }

	public Part Part_DickMask { get; private set; }

	public Part Part_MCthigh { get; private set; }

	public Part Part_MCbelly { get; private set; }

	public Part Part_MCPalm { get; private set; }

	public Part Part_MCPinky { get; private set; }

	public Part Part_MCThumb { get; private set; }

	public Part Part_MCMiddleFinger { get; private set; }

	public Part Part_MCRingFinger { get; private set; }

	public Part Part_MCIndexFinger { get; private set; }

	public Part Part_MCIndexFinger2 { get; private set; }

	public Part Part_MCRingFinger2 { get; private set; }

	public Part Part_MCMiddleFinger2 { get; private set; }

	public Part Part_MCThumb2 { get; private set; }

	public Part Part_MCPinky2 { get; private set; }

	public Part Part_MCPalm2 { get; private set; }

	public Part Part_AttachArmRLower { get; private set; }

	public Part Part_AttachArmRUpper { get; private set; }

	public Part Part_Iris2L { get; private set; }

	public Part Part_Iris1L { get; private set; }

	public Part Part_PupilL3 { get; private set; }

	public Part Part_Iris2R { get; private set; }

	public Part Part_Iris1R { get; private set; }

	public Part Part_PupilR { get; private set; }

	public Part Part_BrowR { get; private set; }

	public Part Part_BrowL { get; private set; }

	public Part Part_LowerLipMask { get; private set; }

	public Part Part_SkinLowerLip { get; private set; }

	public Part Part_UpperLipMask { get; private set; }

	public Part Part_SkinUpperLip { get; private set; }

	public Part Part_SharpTeethUpper { get; private set; }

	public Part Part_Tongue { get; private set; }

	public Part Part_InnerMouth { get; private set; }

	public Part Part_TeethUpper { get; private set; }

	public Part Part_TeethLower { get; private set; }

	public Part Part_SkinMouth3 { get; private set; }

	public Part Part_SkinMouth2Lower { get; private set; }

	public Part Part_Tongue3 { get; private set; }

	public Part Part_InnerMask { get; private set; }

	public Part Part_InnerMouth2 { get; private set; }

	public Part Part_Blush { get; private set; }

	public Part Part_BluescreenOld { get; private set; }

	public Part Part_NipPiercingLeft { get; private set; }

	public Part Part_SkinSeamlineButtR_POV { get; private set; }

	public Part Part_SkinButtR_POV { get; private set; }

	public Part Part_SkinSeamlineButtL_POV { get; private set; }

	public Part Part_SkinButtL_POV { get; private set; }

	public Part Part_Anus_POV { get; private set; }

	public Part Part_SkinVaginaR_POV { get; private set; }

	public Part Part_SkinVaginaL_POV { get; private set; }

	public Part Part_DickMask_POV { get; private set; }

	public Part Part_CumDick1_POV { get; private set; }

	public Part Part_CumDick2_POV { get; private set; }

	public Part Part_DickShadow { get; private set; }

	public Part Part_DickMain_POV { get; private set; }

	public Part Part_VaginaInner_POV { get; private set; }

	public Part Part_SkinAnusTop_POV { get; private set; }

	public Part Part_AnusInnerAndBottom_POV { get; private set; }

	public Part Part_SkinSeamlineThighR_POV { get; private set; }

	public Part Part_ThighR_POV { get; private set; }

	public Part Part_SkinSeamlineThighL_POV { get; private set; }

	public Part Part_ThighL_POV { get; private set; }

	public Part Part_AttachLegRThigh_POV { get; private set; }

	public Part Part_AttachLegLThigh_POV { get; private set; }

	public Part Part_AttachChargeHole_POV { get; private set; }

	public Part Part_SkinPelvis2_POV { get; private set; }

	public Part Part_SkinScapulaL_POV { get; private set; }

	public Part Part_SkinScapulaR_POV { get; private set; }

	public Part Part_SkinShoulderL_POV { get; private set; }

	public Part Part_SkinShoulderR_POV { get; private set; }

	public Part Part_SkinRibcageBase_POV { get; private set; }

	public Part Part_H0_Back1_POV { get; private set; }

	public Part Part_SkinNeck_POV { get; private set; }

	public Part Part_SkinUpperArmL_POV { get; private set; }

	public Part Part_SkinUpperArmR_POV { get; private set; }

	public Part Part_AttachArmLUpper_POV { get; private set; }

	public Part Part_AttachArmRUpper_POV { get; private set; }

	public Part Part_AttachArmLLower_POV { get; private set; }

	public Part Part_AttachArmRLower_POV { get; private set; }

	public Part Part_AttachArmLHand_POV { get; private set; }

	public Part Part_AttachArmRHand_POV { get; private set; }

	public Part Part_SkinLowerWaist_POV { get; private set; }

	public Part Part_SkinUpperWaist_POV { get; private set; }

	public Part Part_SkinLowerWaistBase_POV { get; private set; }

	public Part Part_SkinUpperWaistBase_POV { get; private set; }

	public Part Part_Shadow_ThighL_POV { get; private set; }

	public Part Part_Shadow_ThighR_POV { get; private set; }

	public Part Part_Shadow_AssL_POV { get; private set; }

	public Part Part_Shadow_AssR_POV { get; private set; }

	public Part Part_MCSpreadL_POV { get; private set; }

	public Part Part_MCSpread_ThumbL_POV { get; private set; }

	public Part Part_MCSpreadR_POV { get; private set; }

	public Part Part_MCSpread_ThumbR_POV { get; private set; }

	public Part Part_MCSpankL_POV { get; private set; }

	public Part Part_MCSpankR_POV { get; private set; }

	public Part Part_MCAnusHandWrist_POV { get; private set; }

	public Part Part_MCAnusHandMain_POV { get; private set; }

	public Part Part_MCAnusHandFinger_POV { get; private set; }

	public Part Part_MCAnusHandThumb_POV { get; private set; }

	public Part Part_MCWaistGrabL_POV { get; private set; }

	public Part Part_MCWaistGrabL2_POV { get; private set; }

	public Part Part_MCWaistGrabR_POV { get; private set; }

	public Part Part_MCWaistGrabR2_POV { get; private set; }

	public Part Part_CumAnal1_POV { get; private set; }

	public Part Part_CumDrip1_POV { get; private set; }

	public Part Part_CumVagina1_POV { get; private set; }

	public Part Part_H0_SideLeftMask_POV { get; private set; }

	public Part Part_H0_SideLeft_POV { get; private set; }

	public Part Part_SkinFaceSeamline_POV { get; private set; }

	public Part Part_SkinSideUpperLip_POV { get; private set; }

	public Part Part_SkinSideLowerLip_POV { get; private set; }

	public Part Part_SkinSideFace_POV { get; private set; }

	public Part Part_H0_Front_Bang_POV { get; private set; }

	public Part Part_MCPet_POV { get; private set; }

	public Part Part_MCVaginaHandMain_POV { get; private set; }

	public Part Part_MCVaginaHandInside_POV { get; private set; }

	public Part Part_MCSpread_ThumbL_POV2 { get; private set; }

	public Part Part_MCSpreadL_POV2 { get; private set; }

	public Part Part_MCSpread_ThumbR_POV2 { get; private set; }

	public Part Part_MCSpreadR_POV2 { get; private set; }

	public Part Part_SkinVaginaTop_POV { get; private set; }

	public Part Part_SkinButtBase_POV { get; private set; }

	public Part Part_Tongue_POV { get; private set; }

	public Part Part_Blush_POV { get; private set; }

	public Part Part_DickSkin_POV { get; private set; }

	public Part Part_CumWomb1_POV { get; private set; }

	public Part Part_CumLeakStretchTop { get; private set; }

	public Part Part_CumLeakStretchBottom { get; private set; }

	public Part Part_SkinSmirk { get; private set; }

	public Part Part_CumOutsideFaceNose_POV { get; private set; }

	public Part Part_CumOutsideFaceLeft_POV { get; private set; }

	public Part Part_CumOutsideInnerThighR_POV { get; private set; }

	public Part Part_CumOutsideInnerThighR2_POV { get; private set; }

	public Part Part_CumOutsideInnerThighLowerL_POV { get; private set; }

	public Part Part_CumOutsideInnerThighL_POV { get; private set; }

	public Part Part_CumOutsideInnerThighL2_POV { get; private set; }

	public Part Part_CumOutsideAss2_POV { get; private set; }

	public Part Part_CumOutsideAss_POV { get; private set; }

	public Part Part_CumOutsideThighBack_POV { get; private set; }

	public Part Part_CumOutsideAssUp_POV { get; private set; }

	public Part Part_CumOutsideAssSide2_POV { get; private set; }

	public Part Part_CumOutsideAssR2_POV { get; private set; }

	public Part Part_CumOutsideAssUp2_POV { get; private set; }

	public Part Part_CumOutsideFaceNose { get; private set; }

	public Part Part_CumOutsideFaceLeft { get; private set; }

	public Part Part_CumOutsideAssR_POV { get; private set; }

	public Part Part_CumOutsideAssCenterR_POV { get; private set; }

	public Part Part_CumOutsideAssCenterL_POV { get; private set; }

	public Part Part_CumOutsideScapulaR_POV { get; private set; }

	public Part Part_CumOutsideThighL_POV { get; private set; }

	public Part Part_CumOutsideWaistUpperCenter_POV { get; private set; }

	public Part Part_CumOutsideAssBottom_POV2 { get; private set; }

	public Part Part_CumOutsideBack1_POV { get; private set; }

	public Part Part_CumOutsideAssR3_POV { get; private set; }

	public Part Part_CumOutsideAssL_POV { get; private set; }

	public Part Part_CumOutsideFaceRight { get; private set; }

	public Part Part_ClitRub { get; private set; }

	public Part Part_VaginaInner { get; private set; }

	private void EnsureParticleSystems()
	{
		if (_flyOffParticleSystem == null)
		{
			_flyOffParticleSystem = R.ShowerParticleSystemsGroups.FlyOffSoapParticleSystem.GetAndInstantiate(transform);
		}
		if (_hideBrushParticleSystem == null)
		{
			_hideBrushParticleSystem = R.ShowerParticleSystems.HideBrushSoapParticleSystem.GetAndInstantiate(transform);
		}
		_flyOffParticleSystem.DisableEmission();
		_hideBrushParticleSystem.DisableEmission();
	}

	private void DestroyParticleSystems()
	{
		if (_flyOffParticleSystem != null)
		{
			UnityEngine.Object.Destroy(_flyOffParticleSystem.gameObject);
		}
		if (_hideBrushParticleSystem != null)
		{
			UnityEngine.Object.Destroy(_hideBrushParticleSystem.gameObject);
		}
	}

	public float GetSoapUsageLeft()
	{
		return Mathf.Clamp01(1f - _soapUsed / 12288f);
	}

	public bool UsedAnySoap()
	{
		return _soapUsed > 0f;
	}

	public bool HasAppliedEnoughSoap()
	{
		return _soapUsed >= 12288f;
	}

	public float GetCleaningProgress()
	{
		float num = 0f;
		foreach (var (_, num3) in _cleanedAreas)
		{
			num += MathF.Tanh((float)num3 / 500f) / 0.7615942f;
		}
		num /= 13f;
		float cleanlinessScore = CumOutsideManager.GetCleanlinessScore(CameraCumPartPostfix);
		return Mathf.Clamp01(Mathf.Lerp(num, num * 0.100000024f + cleanlinessScore * 0.9f, num) * 1.1f);
	}

	public bool HasFinishedCleaning()
	{
		return GetCleaningProgress() >= 0.9999f;
	}

	public bool MoveAllSoapInEyeArea()
	{
		List<SoapScript> list;
		using (MyCollectionPool<SoapScript>.GetList(out list))
		{
			Rect sampleRange = HitAreaEyeL.CalculateApproximateWorldRect().Grow(0.1f);
			_soapSpatialHash.SampleAt(sampleRange, list);
			foreach (SoapScript item in list)
			{
				ForceMoveSoapOrDelete(item, new Vector2(UnityEngine.Random.Range(sampleRange.xMin, sampleRange.xMax), sampleRange.yMin - UnityEngine.Random.Range(0.05f, 0.1f)));
			}
			bool flag = list.Count > 0;
			list.Clear();
			Rect sampleRange2 = HitAreaEyeR.CalculateApproximateWorldRect().Grow(0.1f);
			_soapSpatialHash.SampleAt(sampleRange2, list);
			foreach (SoapScript item2 in list)
			{
				ForceMoveSoapOrDelete(item2, new Vector2(UnityEngine.Random.Range(sampleRange2.xMin, sampleRange2.xMax), sampleRange2.yMin - UnityEngine.Random.Range(0.05f, 0.1f)));
			}
			return flag || list.Count > 0;
		}
	}

	public void LiftSpongeAt(Vector3 position)
	{
		List<SoapScript> list;
		using (MyCollectionPool<SoapScript>.GetList(out list))
		{
			_soapSpatialHash.SampleAt(position, 0.32f, list, (SoapScript script) => script.SoapParticleType == SoapParticleType.Spread);
			if (list.Count > 0)
			{
				list.GetRandom().TransformToLifted();
			}
			CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.Scrubbing);
		}
	}

	private void ForceMoveSoapOrDelete(SoapScript soap, Vector2 NewPosition)
	{
		if (RaycastSoapPosition(NewPosition, out var soapedPart, out var hitTriangleData))
		{
			soap.transform.position = NewPosition;
			soap.MoveSoap(soapedPart, hitTriangleData, NewPosition);
			_soapSpatialHash.UpdateSingle(soap);
		}
		else
		{
			_soapUsed -= soap.GetSoapLife();
			_soapSpatialHash.RemoveSingle(soap);
		}
	}

	private void TryMovingSoap(SoapScript soap, Vector2 movement)
	{
		Vector2 vector = soap.transform.position.ToVector2() + movement;
		if (_soapSpatialHash.NoParticlesInRangeIgnoreSelf(vector, 0.10666666f, soap) && RaycastSoapPosition(vector, out var soapedPart, out var hitTriangleData))
		{
			soap.transform.position = vector;
			soap.MoveSoap(soapedPart, hitTriangleData, movement);
			_soapSpatialHash.UpdateSingle(soap);
		}
	}

	public void SpreadSoap(SoapScript soapPrefab, Vector2 lastMousePosition, Vector2 newMousePosition)
	{
		Vector2 vector = newMousePosition - lastMousePosition;
		float magnitude = vector.magnitude;
		Vector2 vector2 = vector / magnitude;
		ButtMovement.Value += vector.x * 0.01f;
		ParamBotBodyStraighten.Value += vector.y * 0.01f;
		if (RaycastCumPosition(lastMousePosition, out var hitSplatter))
		{
			hitSplatter.ClearPartiallyPercentAndUpdate(UnityEngine.Random.Range(0.3f, 1f) * Time.deltaTime);
			_hideBrushParticleSystem.transform.position = newMousePosition;
			_hideBrushParticleSystem.Emit(1);
		}
		if (!(magnitude > 0f))
		{
			return;
		}
		int num = 0;
		List<SoapScript> list;
		using (MyCollectionPool<SoapScript>.GetList(out list))
		{
			_soapSpatialHash.SampleAt(lastMousePosition, 0.64f, list);
			foreach (SoapScript item in list)
			{
				TryMovingSoap(item, vector * UnityEngine.Random.Range(0.5f, 1f));
				num++;
				_cleanedAreas.AddToCountDictionaryWithMaxValue(item.ConnectedPart, 1000);
			}
			list.RemoveAll((SoapScript x) => x.GetSoapLife() <= 1f);
			if (num == 0)
			{
				return;
			}
			CurrentBrain.InteractionMemory.FireEvent(InteractionMemoryEventEnum.Scrub);
			_hideBrushParticleSystem.transform.position = newMousePosition;
			_hideBrushParticleSystem.Emit(1);
			Vector2 normalized = vector.normalized;
			int num2 = Mathf.Max(Mathf.FloorToInt(magnitude / 0.32f), 1);
			float num3 = list.Sum((SoapScript x) => x.GetSoapLife() - 1f);
			float num4 = num3;
			_flyOffParticleSystem.GetParticleSystem(1).transform.localRotation = Quaternion.FromToRotation(Vector3.right, vector);
			for (int num5 = 1; num5 <= num2; num5++)
			{
				Vector2 vector3 = lastMousePosition + normalized * (0.32f * (float)num5);
				Vector2 vector4 = vector3 + UnityEngine.Random.insideUnitCircle * 0.32f;
				if (num4 >= 1f)
				{
					float num6 = UnityEngine.Random.Range(1f, Mathf.Min(num4, 75f));
					if (TryCreateSoap(soapPrefab, vector4, list.GetRandom().SoapItemData, SoapParticleType.Spread, num6))
					{
						num4 -= num6;
						if (num4 < 1f)
						{
							break;
						}
					}
				}
				if (list.Count > 0 && (double)UnityEngine.Random.value < 0.1)
				{
					TryCreateSoap(soapPrefab, vector4 + UnityEngine.Random.insideUnitCircle * 0.32f, list.GetRandom().SoapItemData, SoapParticleType.Bubble, 1f);
				}
				if ((double)UnityEngine.Random.value < 0.1)
				{
					_flyOffParticleSystem.transform.position = vector4 + UnityEngine.Random.insideUnitCircle * 0.32f;
					_flyOffParticleSystem.EmitIndexWithDirection(0, UnityEngine.Random.Range(1, 2), vector2, 30f);
					_flyOffParticleSystem.EmitIndex(0, UnityEngine.Random.Range(1, 2));
					_flyOffParticleSystem.EmitIndex(1, UnityEngine.Random.Range(1, 2));
				}
				if (RaycastCumPosition(vector3, out var hitSplatter2))
				{
					hitSplatter2.ClearPartiallyPercentAndUpdate(UnityEngine.Random.Range(0.3f, 1f) * Time.deltaTime);
				}
			}
			if (num3 < 1f)
			{
				return;
			}
			float goal = Mathf.Max(num3 - num4, 2f);
			list.Sort((SoapScript a, SoapScript b) => -a.GetSoapLife().CompareTo(b.GetSoapLife()));
			foreach (SoapScript item2 in list)
			{
				item2.ExtractSoapLife(ref goal);
			}
		}
	}

	public void DestroyAllSoap()
	{
		_soapUsed = 0f;
		_soapSpatialHash.Clear();
		_cleanedAreas.Clear();
	}

	private bool RaycastSoapPosition(Vector2 position, out Part soapedPart, out Live2DRaycaster.HitTriangleData hitTriangleData)
	{
		List<CubismRenderer> list;
		using (MyCollectionPool<CubismRenderer>.GetList(out list))
		{
			Ray ray = UiOverlay.Instance.WorldToRay(position);
			Model.RaycastQuickBoundingBox(GetRenderersInDrawOrder(), ray, 15, ref list);
			Live2DRaycaster.CalculateIntersection(Model, ray, out var intersectionInLocalSpace, out var intersectionInWorldSpace);
			for (int i = 0; i < list.Count; i++)
			{
				CubismRenderer cubismRenderer = list[i];
				if (!Live2DRaycaster.TryGetUVCoordinates(cubismRenderer.Mesh, intersectionInLocalSpace, out var _, out hitTriangleData))
				{
					continue;
				}
				Vector3 intersectionInLocalSpace2 = Model.transform.InverseTransformPoint(intersectionInWorldSpace + new Vector3(1f, 1f, 0f) * 0.32f);
				intersectionInLocalSpace2.z = 0f;
				Vector3 intersectionInLocalSpace3 = Model.transform.InverseTransformPoint(intersectionInWorldSpace + new Vector3(-1f, -1f, 0f) * 0.32f);
				intersectionInLocalSpace3.z = 0f;
				if (!Live2DRaycaster.TryGetUVCoordinates(cubismRenderer.Mesh, intersectionInLocalSpace2, out var uv2) || !Live2DRaycaster.TryGetUVCoordinates(cubismRenderer.Mesh, intersectionInLocalSpace3, out var uv3))
				{
					continue;
				}
				soapedPart = GetCachedPart(cubismRenderer);
				NativeArray<byte> alphaSlow = soapedPart.MainTexture.GetAlphaSlow(uv3, uv2, out var textureSize);
				if (!alphaSlow.IsCreated)
				{
					continue;
				}
				int num = Mathf.Min(textureSize.x * textureSize.y, alphaSlow.Length);
				int num2 = 0;
				for (int j = 0; j < num; j++)
				{
					if (alphaSlow[j] > 5)
					{
						num2++;
					}
				}
				if ((double)num2 > 0.8 * (double)textureSize.x * (double)textureSize.y)
				{
					ColoringGroupEnum coloringGroupEnum = soapedPart.ColoringGroup.ColoringGroupEnum;
					if (coloringGroupEnum == ColoringGroupEnum.Sclera || HitAreaEyeL.CalculateApproximateWorldRect().Grow(0.1f).Contains(position) || HitAreaEyeR.CalculateApproximateWorldRect().Grow(0.1f).Contains(position))
					{
						CurrentBrain.InteractionMemory.FireEvent(InteractionMemoryEventEnum.SoapEye);
						return true;
					}
					if (coloringGroupEnum == ColoringGroupEnum.Skin || coloringGroupEnum == ColoringGroupEnum.Boobs || coloringGroupEnum == ColoringGroupEnum.Nipples || coloringGroupEnum == ColoringGroupEnum.Thigh || coloringGroupEnum == ColoringGroupEnum.Tummy || coloringGroupEnum == ColoringGroupEnum.Ass || coloringGroupEnum == ColoringGroupEnum.Pelvic || coloringGroupEnum == ColoringGroupEnum.ArmL || coloringGroupEnum == ColoringGroupEnum.ArmR || coloringGroupEnum == ColoringGroupEnum.LegL || coloringGroupEnum == ColoringGroupEnum.LegR || coloringGroupEnum == ColoringGroupEnum.Hair0_Back || coloringGroupEnum == ColoringGroupEnum.Hair0_Braid || coloringGroupEnum == ColoringGroupEnum.Hair0_Front || coloringGroupEnum == ColoringGroupEnum.Hair0_Side || coloringGroupEnum == ColoringGroupEnum.Hair0_Top || coloringGroupEnum == ColoringGroupEnum.Hair0_SideAlt)
					{
						return true;
					}
					return false;
				}
			}
			soapedPart = null;
			hitTriangleData = default;
			return false;
		}
	}

	private bool RaycastCumPosition(Vector2 position, out CumOutsideManager.CumLive2DRepresentation hitSplatter)
	{
		List<CubismRenderer> list;
		using (MyCollectionPool<CubismRenderer>.GetList(out list))
		{
			List<CubismRenderer> list2;
			using (MyCollectionPool<CubismRenderer>.GetList(out list2))
			{
				Ray ray = UiOverlay.Instance.WorldToRay(position);
				CumOutsideManager.FillNotEmptySplatterRenderers(list2);
				Model.RaycastQuickBoundingBox(list2, ray, 3, ref list);
				for (int i = 0; i < list.Count; i++)
				{
					CubismRenderer renderer = list[i];
					if (CumOutsideManager.TryGetRelatedCumSplatter(renderer, out hitSplatter))
					{
						return true;
					}
				}
				hitSplatter = null;
				return false;
			}
		}
	}

	public bool TryCreateSoap(SoapScript soapPrefab, Vector2 position, SoapItemData soapItemData, SoapParticleType soapParticleType, float soapStrength = 155f)
	{
		if (RaycastSoapPosition(position, out var soapedPart, out var hitTriangleData) && _soapSpatialHash.NoParticlesInRange(position, 0.10666666f))
		{
			if (_soapSpatialHash.Count >= 16384)
			{
				SoapScript random = _soapSpatialHash.GetRandom();
				_soapSpatialHash.RemoveSingle(random);
			}
			SoapScript soapScript = UnityEngine.Object.Instantiate(soapPrefab, position, Quaternion.identity);
			soapScript.InitSoap(soapedPart, hitTriangleData, soapParticleType, soapStrength, soapItemData);
			if (soapParticleType == SoapParticleType.Liquid)
			{
				_soapUsed += soapStrength;
				FireEvent(InteractionMemoryEventEnum.ApplyingSoap);
			}
			_soapSpatialHash.AddSingle(soapScript);
			return true;
		}
		return false;
	}

	private void UpdateSoap()
	{
		_soapSpatialHash.RemoveByPredicate((SoapScript x) => x.IsKilled);
	}

	public new void FixedUpdate()
	{
		base.FixedUpdate();
		UpdateSoap();
	}

	private void BeforeInsertionHandlersUpdate()
	{
		ButtPressed.SetMinValue();
		if (!CockManager.TryGetCockLimbUsage(out var o) || !o.TryGetActiveData(out var o2) || !o2.TryGetActiveFuckAxis(out var currentAxis) || !(currentAxis is BasicInsertionHandleFuckAxis basicInsertionHandleFuckAxis))
		{
			return;
		}
		float currentRealInsertionPosition = basicInsertionHandleFuckAxis.GetCurrentRealInsertionPosition();
		float realValue = ButtMovement.GetRealValue();
		Live2DParameter thrustParameter = basicInsertionHandleFuckAxis.ThrustParameter;
		float thrustMovementAccumulator = o2.ThrustMovementAccumulator;
		if (currentRealInsertionPosition > 0.55f && realValue > -0.25f)
		{
			float num = Mathf.InverseLerp(-0.25f, 1f, realValue);
			float num2 = Mathf.InverseLerp(0.55f, 1f, currentRealInsertionPosition);
			float num3 = Mathf.Sqrt(num * num2);
			float num4 = num3 * num3 * Time.deltaTime * 0.1f;
			thrustParameter.AddOnlyRealValue(0f - num4);
			ButtPressed.Value = num3;
		}
		if (!o2.IsInside())
		{
			return;
		}
		float num5 = 0.22f;
		if (thrustMovementAccumulator < 0f)
		{
			num5 *= 0.75f;
			if (o2.OrificeType == InsertionHandlerOrificeTypeEnum.Thighjob)
			{
				num5 *= 0.1f;
			}
		}
		else if (!basicInsertionHandleFuckAxis.CanThrustForward())
		{
			num5 *= 1.2f;
		}
		float num6 = thrustMovementAccumulator * num5;
		ButtMovement.AddRealAndInterpolatedValue(0f - num6);
		if (num6 > 0f)
		{
			ParamBotBodyStraighten.AddRealAndInterpolatedValue(num6 * 0.25f);
		}
		else
		{
			ParamBotBodyStraighten.AddRealAndInterpolatedValue(num6);
		}
	}

	public new void Update()
	{
		base.Update();
		foreach (SoapScript value in _soapSpatialHash.Values)
		{
			value.UpdateManual();
		}
		_soapSpatialHash.MarkDirty();
	}

	protected override void OnStartUsageGameplayInherited(string characterId, ILive2DModelUser user)
	{
		base.OnStartUsageGameplayInherited(characterId, user);
		EnsureParticleSystems();
		SwitchCameraTo(ShowerSceneCameraEnum.SideCamera);
	}

	protected override void OnEndUsageInherited(ILive2DModelUser previousUser)
	{
		DestroyAllSoap();
		DestroyParticleSystems();
		base.OnEndUsageInherited(previousUser);
	}

	public void OnLeaked(InsertionHandlerOrificeTypeEnum orifice, float ml)
	{
		ml *= 0.06f;
		List<CumOutsideManager.CumLive2DRepresentation> list;
		using (MyCollectionPool<CumOutsideManager.CumLive2DRepresentation>.GetList(out list))
		{
			CumOutsideManager.FillInnerThighRepresentations(list);
			if (list.Count <= 0)
			{
				return;
			}
			for (int i = 0; i < 16; i++)
			{
				float num = UnityEngine.Random.Range(3, 10);
				CumOutsideManager.CumLive2DRepresentation random = list.GetRandom();
				if (num < ml)
				{
					random.SquirtAndUpdate(num);
					continue;
				}
				random.SquirtAndUpdate(ml);
				break;
			}
		}
	}

	public void ToggleCamera()
	{
		switch (_currentSceneCamera)
		{
		case ShowerSceneCameraEnum.NoSetting:
		case ShowerSceneCameraEnum.POVCamera:
			SwitchCameraTo(ShowerSceneCameraEnum.SideCamera);
			break;
		case ShowerSceneCameraEnum.SideCamera:
			SwitchCameraTo(ShowerSceneCameraEnum.POVCamera);
			break;
		}
	}

	private async UniTask CameraSwitchTask(ShowerSceneCameraEnum c, CancellationToken cameraSwitchCancellationTokenSource)
	{
		UniTaskCompletionSource tcs = new UniTaskCompletionSource();
		FungusManager.Instance.CameraManager.Fade(1f, 0.2f, Color.black, () =>
		{
			tcs.TrySetResult();
		});
		await tcs.Task.AttachExternalCancellation(cameraSwitchCancellationTokenSource);
		switch (c)
		{
		default:
			ParamPerspectiveToggle.SetRealValue(0f);
			break;
		case ShowerSceneCameraEnum.SideCamera:
			ParamPerspectiveToggle.SetRealValue(1f);
			transform.localPosition = new Vector3(0.49f, 0f, 0f);
			break;
		case ShowerSceneCameraEnum.POVCamera:
			ParamPerspectiveToggle.SetRealValue(2f);
			transform.localPosition = new Vector3(-0.41f, 0f, 0f);
			break;
		}
		_currentSceneCamera = c;
		FungusManager.Instance.CameraManager.Fade(0f, 0.2f, Color.black, null);
		CancelCameraSwitch();
	}

	private void CancelCameraSwitch()
	{
		CancellationTokenSource cameraSwitchCancellationTokenSource = _cameraSwitchCancellationTokenSource;
		_cameraSwitchCancellationTokenSource = null;
		cameraSwitchCancellationTokenSource.Cancel();
		cameraSwitchCancellationTokenSource.Dispose();
	}

	public void SwitchCameraTo(ShowerSceneCameraEnum c)
	{
		if (_cameraSwitchCancellationTokenSource != null)
		{
			Debug.LogWarning("Trying to switch camera twice!");
			return;
		}
		_cameraSwitchCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(GetUsageCancellationToken());
		CameraSwitchTask(c, _cameraSwitchCancellationTokenSource.Token).Forget();
	}

	public override Part GetCurrentlyUsedDickPart()
	{
		if (_currentSceneCamera != ShowerSceneCameraEnum.SideCamera)
		{
			return Part_DickSkin_POV;
		}
		return Part_DickSkin;
	}

	public override Part GetApproximateEyesPositionPart()
	{
		if (_currentSceneCamera != ShowerSceneCameraEnum.SideCamera)
		{
			return HitArea_Eyes_POV;
		}
		return Part_EyeWhiteL2;
	}

	public override Part GetApproximateMouthPositionPart()
	{
		if (_currentSceneCamera != ShowerSceneCameraEnum.SideCamera)
		{
			return Part_SkinSideLowerLip_POV;
		}
		return HitAreaMouth;
	}

	public override Vector2 GetCockDirection()
	{
		if (_currentSceneCamera != ShowerSceneCameraEnum.SideCamera)
		{
			return Vector2.up;
		}
		return Vector2.left;
	}

	public override float GetCockMaxAngle()
	{
		if (_currentSceneCamera != ShowerSceneCameraEnum.SideCamera)
		{
			return 45f;
		}
		return 35f;
	}

	protected override MouthManagerDefaultMouth InitializeMouthImpl()
	{
		return MouthManagerDefaultMouth.Create(this);
	}

	protected override EyeManagerCommon InitializeEyesImpl()
	{
		return EyeManagerCommon.CreateNoBlinkBothEyes(this);
	}

	protected override void InitializeManagersInherited()
	{
		FrickStaticGuiManager = RegisterManager(new FrickStaticGuiManager(this));
		AdvancedCumLeakManager = RegisterManager(new AdvancedCumLeakManager<Live2DShowerController>(this, 1f, Live2DParameterUpdateTypeEnum.Linear));
		CameraManager = RegisterManager(new CameraManager<Live2DShowerController>(this));
		List<InsertionHandler> list = new List<InsertionHandler>();
		InsertionHandler insertionHandler = new InsertionHandler(this, InsertionHandlerOrificeTypeEnum.Vagina);
		VaginaCockFuckAxis = new BasicInsertionHandleFuckAxis(0f, 0.2f, ParamThrust)
		{
			CockAngleXParameter = ParamVaginaCockAngle,
			ReciprocatingParameter = ButtMovement
		};
		insertionHandler.AddFuckAxis(VaginaCockFuckAxis);
		VaginaFingeringFuckAxis = new BasicInsertionHandleFuckAxis(0f, 0.05f, ParamVaginaFingeringInsert)
		{
			CockAngleXParameter = ParamVaginaFingeringRotation,
			ThrustParameterMapping = new ParameterMappingMinMax(0.1f, 1f, 0f, 0.5f)
		};
		insertionHandler.AddFuckAxis(VaginaFingeringFuckAxis);
		insertionHandler.EntranceSpreadParameter = ParamVaginaOpen;
		insertionHandler.AddFuckAxis(VaginaCockFuckAxis);
		list.Add(insertionHandler);
		VaginaInsertionHandler = insertionHandler;
		InsertionHandler insertionHandler2 = new InsertionHandler(this, InsertionHandlerOrificeTypeEnum.Anal);
		AnalCockFuckAxis = new BasicInsertionHandleFuckAxis(0f, 0.2f, ParamThrustAnal)
		{
			CockAngleXParameter = ParamAnalCockAngle,
			ReciprocatingParameter = ButtMovement
		};
		insertionHandler2.AddFuckAxis(AnalCockFuckAxis);
		AnalFingeringFuckAxis = new BasicInsertionHandleFuckAxis(0f, 0.05f, ParamAnusFingeringInsert)
		{
			CockAngleXParameter = ParamAnusFingeringRotation,
			ThrustParameterMapping = new ParameterMappingMinMax(0f, 1f, 0f, 0.5f)
		};
		insertionHandler2.AddFuckAxis(AnalFingeringFuckAxis);
		insertionHandler2.EntranceSpreadParameter = ParamAnusOpen;
		list.Add(insertionHandler2);
		AnalInsertionHandler = insertionHandler2;
		InsertionHandler insertionHandler3 = new InsertionHandler(this, InsertionHandlerOrificeTypeEnum.Thighjob);
		ThighjobCockFuckAxis = new BasicInsertionHandleFuckAxis(0f, 0.2f, ParamThrustThighjob)
		{
			CockAngleXParameter = ParamThighjobCockAngle,
			ReciprocatingParameter = ButtMovement,
			OnStart = (BasicInsertionHandleFuckAxis x) =>
			{
				ParamThighjobEnable.Set01Bool(value: true);
			},
			OnReset = (BasicInsertionHandleFuckAxis x) =>
			{
				ParamThighjobEnable.Set01Bool(value: false);
			},
			BotPleasureMulti = 0.5f
		};
		insertionHandler3.EntranceSpreadParameter = ParamThighjobOpen;
		insertionHandler3.AddFuckAxis(ThighjobCockFuckAxis);
		list.Add(insertionHandler3);
		ThighjobInsertionHandler = insertionHandler3;
		InsertionManager = RegisterManager(new InsertionManager(list)
		{
			OnBeforeInsertionHandlersUpdate = BeforeInsertionHandlersUpdate
		});
		_cockFuckLimbUsage = new CockLimbUsage<Live2DShowerController>(this, ParamDick, new CockLimbUsage<Live2DShowerController>.CockLimbUsageTarget(VaginaInsertionHandler, VaginaCockFuckAxis), new CockLimbUsage<Live2DShowerController>.CockLimbUsageTarget(AnalInsertionHandler, AnalCockFuckAxis), new CockLimbUsage<Live2DShowerController>.CockLimbUsageTarget(ThighjobInsertionHandler, ThighjobCockFuckAxis));
		CockManager = RegisterManager(new CockManagerCommon<Live2DShowerController>(this, _cockFuckLimbUsage)
		{
			IsMCNaked = true
		});
	}

	protected override void InitializeInherited()
	{
		AddInteractable(new Part[1] { HitAreaMC }, new Live2DInteractable
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
					if (mouseMove.x < 0f)
					{
						mouseMove.x *= 1.25f;
					}
					insertionHandler.ThrustMovementAccumulator -= mouseMove.x * 10f;
					insertionHandler.SidewaysMovementAccumulator += mouseMove.y * 30f;
				}
			},
			OnHeldFixed = null,
			OnHoldEnd = null,
			InfoSls = LOC.UI.TupleReferences.Interactable_FuckHer,
			ActiveFunc = () => !CockManager.IsFlacid
		}, CubismRaycastablePrecision.Triangles);
		AddInteractable(new Part[1] { HitAreaMC_POV }, new Live2DInteractable
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
					if (mouseMove.x < 0f)
					{
						mouseMove.x *= 1.25f;
					}
					insertionHandler.ThrustMovementAccumulator += mouseMove.y * 10f;
					insertionHandler.SidewaysMovementAccumulator += mouseMove.x * 30f;
				}
			},
			OnHeldFixed = null,
			OnHoldEnd = null,
			InfoSls = LOC.UI.TupleReferences.Interactable_FuckHer,
			ActiveFunc = () => !CockManager.IsFlacid
		}, CubismRaycastablePrecision.Triangles);
		AssFondleLimbUsage = new LimbUsageWithInteractable<Live2DShowerController>(this, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DShowerController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.AssFondle);
				c.PhysicsSubRig_ButtLX.StartParameterControl(c.ParamButtPhysicsLX);
				c.PhysicsSubRig_ButtLY.StartParameterControl(c.ParamButtPhysicsLY);
				ParamAssHandLX.ResetToDefault();
				ParamAssHandLY.ResetToDefault();
				c.ParamAssHandLEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DShowerController c, Vector2 mouseMove) =>
			{
				c.ParamButtPhysicsLX.Value += mouseMove.x * 7f;
				c.ParamButtPhysicsLY.Value += mouseMove.y * 7f;
				c.ParamAssHandLX.Value += mouseMove.x * 7f;
				c.ParamAssHandLY.Value += mouseMove.y * 7f;
				float magnitude = new Vector2(c.ParamButtPhysicsLX.GetRealValue(), c.ParamButtPhysicsLY.GetRealValue()).magnitude;
				c.ParamAssHandLGrab.Value = magnitude;
			},
			EndLimbAction = (Live2DShowerController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.AssFondle);
				c.PhysicsSubRig_ButtLX.EndParameterControl(c.ParamButtPhysicsLX);
				c.PhysicsSubRig_ButtLY.EndParameterControl(c.ParamButtPhysicsLY);
				c.ParamAssHandLEnable.SetMinValue();
			}
		};
		AssGrabLimbUsage = new LimbUsageWithInteractable<Live2DShowerController>(this, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DShowerController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.AssGrab);
				c.PhysicsSubRig_ButtLX.StartParameterControl(c.ParamButtPhysicsLX);
				c.PhysicsSubRig_ButtLY.StartParameterControl(c.ParamButtPhysicsLY);
				ParamAssHandLX.ResetToDefault();
				ParamAssHandLY.ResetToDefault();
				c.ParamAssHandLEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DShowerController c, Vector2 mouseMove) =>
			{
				c.ParamButtPhysicsLX.Value += mouseMove.x * 7f;
				c.ButtMovement.Value += mouseMove.x * 4f;
				c.ParamBotBodyStraighten.Value -= mouseMove.x * 3f;
				c.ParamButtPhysicsLY.Value += mouseMove.y * 7f;
				if (c.CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2) && !c.CockManager.IsFlacid)
				{
					_cockFuckLimbUsage.DisableAuto();
					o2.ThrustMovementAccumulator += mouseMove.x * 5f;
					o2.SidewaysMovementAccumulator += mouseMove.y * 30f;
					c.ParamAssHandLGrab.Value = 1f;
				}
				else
				{
					float magnitude = new Vector2(c.ParamButtPhysicsLX.GetRealValue(), c.ParamButtPhysicsLY.GetRealValue()).magnitude;
					c.ParamAssHandLGrab.Value = magnitude;
					c.ParamAssHandLX.Value += mouseMove.x * 7f;
					c.ParamAssHandLY.Value += mouseMove.y * 7f;
				}
			},
			EndLimbAction = (Live2DShowerController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.AssGrab);
				c.PhysicsSubRig_ButtLX.EndParameterControl(c.ParamButtPhysicsLX);
				c.PhysicsSubRig_ButtLY.EndParameterControl(c.ParamButtPhysicsLY);
				c.ParamAssHandLEnable.SetMinValue();
			}
		};
		AssFondleInteractable = AddInteractableHitArea(HitAreaAss, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_GrabAss, AssGrabLimbUsage));
		WaistGrabLimbUsage waistGrabLimbUsage = new WaistGrabLimbUsage(0, this);
		WaistGrabLimbUsage waistGrabLimbUsage2 = new WaistGrabLimbUsage(1, this);
		AddInteractableHitArea(HitAreaWaist_POV, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_GrabWaist, waistGrabLimbUsage, waistGrabLimbUsage2));
		SpankLInteractable = AddInteractableHitArea(HitAreaButtL_POV, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_Spank, new SpankLimbUsage(0, this)));
		SpankRInteractable = AddInteractableHitArea(HitAreaButtR_POV, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_Spank, new SpankLimbUsage(1, this)));
		SpreadAnusLimbUsage = new LimbUsageWithInteractable<Live2DShowerController>(this, LimbUsageChoiceEnum.BothArms)
		{
			StartLimbAction = (Live2DShowerController c) =>
			{
				c.ParamSpreadAnusEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DShowerController c, Vector2 mouseMove) =>
			{
				float value = c.ParamAnusOpen.Value;
				c.ParamAnusOpen.Add(mouseMove.x * 100f);
				c.ParamAnusOpen.ClampValueSoftRepulsionFromMax(-1f, 0.7f, 0.25f);
				float value2 = c.ParamAnusOpen.Value;
				if (value < 0.2f && value2 >= 0.2f)
				{
					c.AdvancedCumLeakManager.StartLeak(InsertionHandlerOrificeTypeEnum.Anal);
				}
				else if (value2 < 0.1f)
				{
					c.AdvancedCumLeakManager.EndLeak();
				}
				if ((double)value2 >= 0.2)
				{
					c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.AnusSpread);
				}
				if ((double)value2 < 0.2)
				{
					c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.AnusSpread);
				}
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.005f / 2f);
			},
			HoldFixedUpdateAction = (Live2DShowerController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0005f);
			},
			EndLimbAction = (Live2DShowerController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.AnusSpread);
				c.ParamAnusOpen.SetMinValue();
				c.AdvancedCumLeakManager.EndLeak();
				c.ParamSpreadAnusEnable.SetMinValue();
			}
		};
		SpreadAnusInteractable = AddInteractableHitArea(HitAreaAnusSide_POV, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_SpreadAnus, SpreadAnusLimbUsage)
		{
			ActiveFunc = (Live2DShowerController c) => !c.IsLimbActive(LimbUsageChoiceEnum.Cock)
		});
		LimbUsageWithInteractable<Live2DShowerController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DShowerController>(this, LimbUsageChoiceEnum.LeftArm)
		{
			StartLimbAction = (Live2DShowerController c) =>
			{
				c.ParamAnusFingeringInsert.ResetToDefault();
				c.ParamAnusFingeringRotation.ResetToDefault();
				c.ParamAnusFingeringEnable.SetMaxValue();
				c.AnalInsertionHandler.SetCurrentAxis(c.AnalFingeringFuckAxis, GameVariables.Current.GetGameStats().OtherFrickGameStats.AnonFingeringInfo.Value);
			},
			HoldUpdateAction = (Live2DShowerController c, Vector2 mouseMove) =>
			{
				c.AnalInsertionHandler.ThrustMovementAccumulator += mouseMove.y * 7f;
				c.AnalInsertionHandler.SidewaysMovementAccumulator += mouseMove.x * 4f;
			},
			EndLimbAction = (Live2DShowerController c, LetGoLimbsCaller caller) =>
			{
				c.ParamAnusFingeringEnable.SetMinValue();
				c.AnalInsertionHandler.Reset();
			}
		};
		AddInteractableHitArea(HitAreaAnusCenter_POV, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_FingeringAnus, limbUsageWithInteractable)
		{
			ActiveFunc = (Live2DShowerController c) => c.IsPantiesAsideOrOff && !c.IsLimbActive(LimbUsageChoiceEnum.Cock)
		});
		LimbUsageWithInteractable<Live2DShowerController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DShowerController>(this, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DShowerController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.ClitRub);
				c.ParamVaginaClitMoveX.ResetToDefault();
				c.ParamVaginaClitMoveY.ResetToDefault();
				c.ParamClitRubZ.ResetToDefault();
				c.ParamClitRubEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DShowerController c, Vector2 mouseMove) =>
			{
				c.ParamVaginaClitMoveX.Value += mouseMove.x * 7f;
				c.ParamVaginaClitMoveY.Value += mouseMove.y * 7f;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude / 0.025f);
			},
			HoldFixedUpdateAction = (Live2DShowerController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0003f);
			},
			EndLimbAction = (Live2DShowerController c, LetGoLimbsCaller caller) =>
			{
				c.ParamClitRubEnable.SetMinValue();
				c.ParamVaginaClitMoveX.ResetToDefault();
				c.ParamVaginaClitMoveY.ResetToDefault();
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.ClitRub);
			}
		};
		AddInteractableHitArea(HitAreaVaginaClit_POV, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_ClitRub, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DShowerController c) => c.IsPantiesAsideOrOff && !c.IsLimbActive(LimbUsageChoiceEnum.Cock)
		});
		SpreadPussyLimbUsage = new LimbUsageWithInteractable<Live2DShowerController>(this, LimbUsageChoiceEnum.BothArms)
		{
			StartLimbAction = (Live2DShowerController c) =>
			{
				c.ParamSpreadPussyEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DShowerController c, Vector2 mouseMove) =>
			{
				float value = c.ParamVaginaOpen.Value;
				c.ParamVaginaOpen.Add(mouseMove.x * 100f);
				c.ParamVaginaOpen.ClampValueSoftRepulsionFromMax(-1f, 1f, 0.25f);
				float value2 = c.ParamVaginaOpen.Value;
				if (value < 0.2f && value2 >= 0.2f)
				{
					c.AdvancedCumLeakManager.StartLeak(InsertionHandlerOrificeTypeEnum.Vagina);
				}
				else if (value2 < 0.1f)
				{
					c.AdvancedCumLeakManager.EndLeak();
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
			HoldFixedUpdateAction = (Live2DShowerController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0005f);
			},
			EndLimbAction = (Live2DShowerController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.PussySpread);
				c.ParamVaginaOpen.SetMinValue();
				c.AdvancedCumLeakManager.EndLeak();
				c.ParamSpreadPussyEnable.SetMinValue();
			}
		};
		SpreadPussyInteractable = AddInteractableHitArea(HitAreaVaginaSpread_POV, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_SpreadPussy, SpreadPussyLimbUsage)
		{
			ActiveFunc = (Live2DShowerController c) => !c.IsLimbActive(LimbUsageChoiceEnum.Cock)
		});
		LimbUsageWithInteractable<Live2DShowerController> limbUsageWithInteractable3 = new LimbUsageWithInteractable<Live2DShowerController>(this, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DShowerController c) =>
			{
				c.ParamVaginaFingeringInsert.ResetToDefault();
				c.ParamVaginaFingeringRotation.ResetToDefault();
				c.ParamVaginaFingeringEnable.SetMaxValue();
				c.VaginaInsertionHandler.SetCurrentAxis(c.VaginaFingeringFuckAxis, GameVariables.Current.GetGameStats().OtherFrickGameStats.AnonFingeringInfo.Value);
			},
			HoldUpdateAction = (Live2DShowerController c, Vector2 mouseMove) =>
			{
				c.VaginaInsertionHandler.ThrustMovementAccumulator += mouseMove.y * 7f;
				c.VaginaInsertionHandler.SidewaysMovementAccumulator += mouseMove.x * 4f;
			},
			EndLimbAction = (Live2DShowerController c, LetGoLimbsCaller caller) =>
			{
				c.ParamVaginaFingeringEnable.SetMinValue();
				c.VaginaInsertionHandler.Reset();
			}
		};
		AddInteractableHitArea(HitAreaVaginaCenter_POV, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_Fingering, limbUsageWithInteractable3)
		{
			ActiveFunc = (Live2DShowerController c) => c.IsPantiesAsideOrOff && !c.IsLimbActive(LimbUsageChoiceEnum.Cock)
		});
		Live2DController.PrepareCommonHeadpatInteractables(this, HitArea_HeadpatPOV, out var _);
		BoobFondleLimbUsage = new LimbUsageWithInteractable<Live2DShowerController>(this, LimbUsageChoiceEnum.BothArms)
		{
			StartLimbAction = (Live2DShowerController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobsX.StartParameterControl(c.BoobPhysicsX);
				c.PhysicsSubRig_BoobsY.StartParameterControl(c.BoobPhysicsY);
				ParamBoobHandX.ResetToDefault();
				ParamBoobHandY.ResetToDefault();
				c.ParamBoobHandEnable.SetMaxValue();
			},
			HoldUpdateAction = (Live2DShowerController c, Vector2 mouseMove) =>
			{
				c.BoobPhysicsX.Value += mouseMove.x * 7f;
				if (c.BoobPhysicsX.IsNearEdge(0.5f))
				{
					c.ParamBoobHandX.Value += mouseMove.x * 7f;
				}
				c.BoobPhysicsY.Value += mouseMove.y * 7f;
				if (c.BoobPhysicsY.IsNearEdge(0.5f))
				{
					c.ParamBoobHandY.Value += mouseMove.y * 7f;
				}
				float magnitude = new Vector2(c.BoobPhysicsX.GetRealValue(), c.BoobPhysicsY.GetRealValue()).magnitude;
				c.ParamBoobHandGrab.Value = magnitude;
				c.CurrentBrain.CausePleasure(mouseMove.sqrMagnitude * 0.01f);
			},
			HoldFixedUpdateAction = (Live2DShowerController c) =>
			{
				c.CurrentBrain.CausePleasure(0.0003f);
			},
			EndLimbAction = (Live2DShowerController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.BoobsFondle);
				c.PhysicsSubRig_BoobsX.EndParameterControl(c.BoobPhysicsX);
				c.PhysicsSubRig_BoobsY.EndParameterControl(c.BoobPhysicsY);
				c.ParamBoobHandEnable.SetMinValue();
			}
		};
		BoobFondleInteractable = AddInteractableHitArea(HitAreaBoob, new Live2DInteractableLimbUsage<Live2DShowerController>(this, LOC.UI.TupleReferences.Interactable_PlayBoobs, BoobFondleLimbUsage));
	}

	public override void InitializeGenerated()
	{
		ParamBoobHandX = GetParamToDirectlyControl("ParamBoobHandX");
		ParamBoobHandY = GetParamToDirectlyControl("ParamBoobHandY");
		ParamBoobHandGrab = GetParamToDirectlyControl("ParamBoobHandGrab");
		ParamBoobHandEnable = GetParamToDirectlyControl("ParamBoobHandEnable");
		ParamAssHandLX = GetParamToDirectlyControl("ParamAssHandLX");
		ParamAssHandLY = GetParamToDirectlyControl("ParamAssHandLY");
		ParamAssHandLGrab = GetParamToDirectlyControl("ParamAssHandLGrab");
		ParamAssHandLEnable = GetParamToDirectlyControl("ParamAssHandLEnable");
		ParamClitRubEnable = GetParamToDirectlyControl("ParamClitRubEnable");
		ParamClitRubZ = GetParamToDirectlyControl("ParamClitRubZ");
		ParamVaginaClitMoveX = GetParamToDirectlyControl("ParamVaginaClitMoveX");
		ParamVaginaClitMoveY = GetParamToDirectlyControl("ParamVaginaClitMoveY");
		ParamWaistGrabLEnable = GetParamToDirectlyControl("ParamWaistGrabLEnable");
		ParamWaistGrabREnable = GetParamToDirectlyControl("ParamWaistGrabREnable");
		ParamSpreadPussyEnable = GetParamToDirectlyControl("ParamSpreadPussyEnable");
		ParamSpreadAnusEnable = GetParamToDirectlyControl("ParamSpreadAnusEnable");
		ParamSpankLEnable = GetParamToDirectlyControl("ParamSpankLEnable");
		ParamSpankLY = GetParamToDirectlyControl("ParamSpankLY");
		ParamSpankLAnim = GetParamToDirectlyControl("ParamSpankLAnim");
		ParamSpankREnable = GetParamToDirectlyControl("ParamSpankREnable");
		ParamSpankRY = GetParamToDirectlyControl("ParamSpankRY");
		ParamSpankRAnim = GetParamToDirectlyControl("ParamSpankRAnim");
		ParamVaginaFingeringEnable = GetParamToDirectlyControl("ParamVaginaFingeringEnable");
		ParamVaginaFingeringInsert = GetParamToDirectlyControl("ParamVaginaFingeringInsert");
		ParamVaginaFingeringRotation = GetParamToDirectlyControl("ParamVaginaFingeringRotation");
		ParamAnusFingeringEnable = GetParamToDirectlyControl("ParamAnusFingeringEnable");
		ParamAnusFingeringInsert = GetParamToDirectlyControl("ParamAnusFingeringInsert");
		ParamAnusFingeringRotation = GetParamToDirectlyControl("ParamAnusFingeringRotation");
		ParamHeadpat = GetParamToDirectlyControl("ParamHeadpat");
		ParamHeadpatX = GetParamToDirectlyControl("ParamHeadpatX");
		ParamHeadpatX.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		ParamHeadpatY = GetParamToDirectlyControl("ParamHeadpatY");
		ParamHeadpatY.UpdateType = Live2DParameterUpdateTypeEnum.Linear;
		ParamEyeballLX = GetParamToDirectlyControl("ParamEyeballLX");
		ParamEyeballLY = GetParamToDirectlyControl("ParamEyeballLY");
		ParamBlush = GetParamToDirectlyControl("ParamBlush");
		Bluescreen = GetParamToDirectlyControl("Bluescreen");
		ParamEyeballRX = GetParamToDirectlyControl("ParamEyeballRX");
		ParamEyeballRY = GetParamToDirectlyControl("ParamEyeballRY");
		ParamMouthOpen = GetParamToDirectlyControl("ParamMouthOpen");
		ParamMouthForm = GetParamToDirectlyControl("ParamMouthForm");
		ParamEyeLShock = GetParamToDirectlyControl("ParamEyeLShock");
		ParamEyeLShock.SetWeight(0.1f);
		ParamTense = GetParamToDirectlyControl("ParamTense");
		ParamEyeRShock = GetParamToDirectlyControl("ParamEyeRShock");
		ParamEyeRShock.SetWeight(0.1f);
		ParamHeart = GetParamToDirectlyControl("ParamHeart");
		ParamEyeExpression = GetParamToDirectlyControl("ParamEyeExpression");
		ParamBrowLY = GetParamToDirectlyControl("ParamBrowLY");
		ParamBrowRY = GetParamToDirectlyControl("ParamBrowRY");
		ParamBrowLRot = GetParamToDirectlyControl("ParamBrowLRot");
		ParamBrowRRot = GetParamToDirectlyControl("ParamBrowRRot");
		ParamBrowLEmote = GetParamToDirectlyControl("ParamBrowLEmote");
		ParamBrowREmote = GetParamToDirectlyControl("ParamBrowREmote");
		ParamHeadX = GetParamToDirectlyControl("ParamHeadX");
		ParamHeadX.SetWeight(10f);
		ParamHeadY = GetParamToDirectlyControl("ParamHeadY");
		ParamHeadY.SetWeight(10f);
		ParamHeadZ = GetParamToDirectlyControl("ParamHeadZ");
		ParamHeadZ.SetWeight(10f);
		ParamCumInside = GetParamToDirectlyControl("ParamCumInside");
		ParamCumInsideWomb = GetParamToDirectlyControl("ParamCumInsideWomb");
		ParamCumInsideAnal = GetParamToDirectlyControl("ParamCumInsideAnal");
		ParamCumLeakSource = GetParamToDirectlyControl("ParamCumLeakSource");
		ParamCumLeakAnim = GetParamToDirectlyControl("ParamCumLeakAnim");
		ParamCumLeakSize = GetParamToDirectlyControl("ParamCumLeakSize");
		ParamDick = GetParamToDirectlyControl("ParamDick");
		ParamThrust = GetParamToDirectlyControl("ParamThrust");
		ParamThrustAnal = GetParamToDirectlyControl("ParamThrustAnal");
		ParamCumShotLengthY = GetParamToDirectlyControl("ParamCumShotLengthY");
		ParamCumOutsideX = GetParamToDirectlyControl("ParamCumOutsideX");
		ParamCumOutsideX.SetWeight(10f);
		ParamCumOutsideY = GetParamToDirectlyControl("ParamCumOutsideY");
		ParamCumOutsideY.SetWeight(10f);
		ParamFlacidity = GetParamToDirectlyControl("ParamFlacidity");
		ParamHideCumShot = GetParamToDirectlyControl("ParamHideCumShot");
		ParamCumShotAnim = GetParamToDirectlyControl("ParamCumShotAnim");
		ParamAnalCockAngle = GetParamToDirectlyControl("ParamAnalCockAngle");
		ParamVaginaCockAngle = GetParamToDirectlyControl("ParamVaginaCockAngle");
		ParamVaginaOpen = GetParamToDirectlyControl("ParamVaginaOpen");
		ParamAnusOpen = GetParamToDirectlyControl("ParamAnusOpen");
		ParamOrgasm = GetParamToDirectlyControl("ParamOrgasm");
		ParamThighjobEnable = GetParamToDirectlyControl("ParamThighjobEnable");
		ParamThrustThighjob = GetParamToDirectlyControl("ParamThrustThighjob");
		ParamThighjobCockAngle = GetParamToDirectlyControl("ParamThighjobCockAngle");
		ParamThighjobOpen = GetParamToDirectlyControl("ParamThighjobOpen");
		TonguePhysics = GetParamToDirectlyControl("TonguePhysics");
		ParamButtPhysicsLY = GetParamToDirectlyControl("ParamButtPhysicsLY");
		ParamButtPhysicsLZ = GetParamToDirectlyControl("ParamButtPhysicsLZ");
		ParamButtPhysicsLX = GetParamToDirectlyControl("ParamButtPhysicsLX");
		ParamButtPhysicsRY = GetParamToDirectlyControl("ParamButtPhysicsRY");
		ParamButtPhysicsRZ = GetParamToDirectlyControl("ParamButtPhysicsRZ");
		ParamButtPhysicsRX = GetParamToDirectlyControl("ParamButtPhysicsRX");
		BoobPhysicsX = GetParamToDirectlyControl("BoobPhysicsX");
		BoobPhysicsY = GetParamToDirectlyControl("BoobPhysicsY");
		RibcagePhysicsRotation = GetParamToDirectlyControl("RibcagePhysicsRotation");
		HairPhysics = GetParamToDirectlyControl("HairPhysics");
		CumPhysics = GetParamToDirectlyControl("CumPhysics");
		ParamAssSize = GetParamToDirectlyControl("ParamAssSize");
		ParamThighSize = GetParamToDirectlyControl("ParamThighSize");
		ParamBoobSize = GetParamToDirectlyControl("ParamBoobSize");
		ParamPerspectiveToggle = GetParamToDirectlyControl("ParamPerspectiveToggle");
		ParamBotBodyStraighten = GetParamToDirectlyControl("ParamBotBodyStraighten");
		ButtMovement = GetParamToDirectlyControl("ButtMovement");
		ButtPressed = GetParamToDirectlyControl("ButtPressed");
		SymParam_LR_ParamWaistGrabEnable = new Live2DParameter[2] { ParamWaistGrabLEnable, ParamWaistGrabREnable };
		SymParam_LR_ParamSpankEnable = new Live2DParameter[2] { ParamSpankLEnable, ParamSpankREnable };
		SymParam_LR_ParamSpankY = new Live2DParameter[2] { ParamSpankLY, ParamSpankRY };
		SymParam_LR_ParamSpankAnim = new Live2DParameter[2] { ParamSpankLAnim, ParamSpankRAnim };
		SymParam_LR_ParamEyeballX = new Live2DParameter[2] { ParamEyeballLX, ParamEyeballRX };
		SymParam_LR_ParamEyeballY = new Live2DParameter[2] { ParamEyeballLY, ParamEyeballRY };
		SymParam_LR_ParamEyeShock = new Live2DParameter[2] { ParamEyeLShock, ParamEyeRShock };
		SymParam_LR_ParamBrowY = new Live2DParameter[2] { ParamBrowLY, ParamBrowRY };
		SymParam_LR_ParamBrowRot = new Live2DParameter[2] { ParamBrowLRot, ParamBrowRRot };
		SymParam_LR_ParamBrowEmote = new Live2DParameter[2] { ParamBrowLEmote, ParamBrowREmote };
		SymParam_LR_ParamButtPhysicsY = new Live2DParameter[2] { ParamButtPhysicsLY, ParamButtPhysicsRY };
		SymParam_LR_ParamButtPhysicsZ = new Live2DParameter[2] { ParamButtPhysicsLZ, ParamButtPhysicsRZ };
		SymParam_LR_ParamButtPhysicsX = new Live2DParameter[2] { ParamButtPhysicsLX, ParamButtPhysicsRX };
		CubismPhysicsController component = Model.GetComponent<CubismPhysicsController>();
		PhysicsSubRig_BoobsX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobsX"));
		PhysicsSubRig_Cum = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Cum"));
		PhysicsSubRig_BoobsY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "BoobsY"));
		PhysicsSubRig_Ribcage = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Ribcage"));
		PhysicsSubRig_Hair = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Hair"));
		PhysicsSubRig_Tongue = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Tongue"));
		PhysicsSubRig_ButtLY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "ButtLY"));
		PhysicsSubRig_ButtRY = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "ButtRY"));
		PhysicsSubRig_ButtRZ = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "ButtRZ"));
		PhysicsSubRig_ButtLZ = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "ButtLZ"));
		PhysicsSubRig_ButtLX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "ButtLX"));
		PhysicsSubRig_ButtRX = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "ButtRX"));
		HitAreaBoob = GetCachedPart("HitAreaBoob");
		HitAreaEyeL = GetCachedPart("HitAreaEyeL");
		HitAreaEyeR = GetCachedPart("HitAreaEyeR");
		HitAreaAss = GetCachedPart("HitAreaAss");
		HitAreaMouth = GetCachedPart("HitAreaMouth");
		HitAreaMC = GetCachedPart("HitAreaMC");
		HitAreaButtL_POV = GetCachedPart("HitAreaButtL_POV");
		HitAreaMC_POV = GetCachedPart("HitAreaMC_POV");
		HitAreaButtR_POV = GetCachedPart("HitAreaButtR_POV");
		HitAreaWaist_POV = GetCachedPart("HitAreaWaist_POV");
		HitAreaVaginaSpread_POV = GetCachedPart("HitAreaVaginaSpread_POV");
		HitAreaAnusSide_POV = GetCachedPart("HitAreaAnusSide_POV");
		HitAreaAnusCenter_POV = GetCachedPart("HitAreaAnusCenter_POV");
		HitAreaVaginaCenter_POV = GetCachedPart("HitAreaVaginaCenter_POV");
		HitArea_HeadpatPOV = GetCachedPart("HitArea_HeadpatPOV");
		HitArea_Eyes_POV = GetCachedPart("HitArea_Eyes_POV");
		HitAreaVaginaClit_POV = GetCachedPart("HitAreaVaginaClit_POV");
		Part_AttachArmLHand = GetCachedPart("AttachArmLHand");
		Part_SkinArm = GetCachedPart("SkinArm");
		Part_AttachArmLLower = GetCachedPart("AttachArmLLower");
		Part_AttachArmLUpper = GetCachedPart("AttachArmLUpper");
		Part_SkinButtSeamline = GetCachedPart("SkinButtSeamline");
		Part_Thighs = GetCachedPart("Thighs");
		Part_AttachLegLThigh = GetCachedPart("AttachLegLThigh");
		Part_SkinButt = GetCachedPart("SkinButt");
		Part_SkinPelvis = GetCachedPart("SkinPelvis");
		Part_AttachChargeHole = GetCachedPart("AttachChargeHole");
		Part_Thighs2 = GetCachedPart("Thighs2");
		Part_SkinArm2 = GetCachedPart("SkinArm2");
		Part_SkinBoob2 = GetCachedPart("SkinBoob2");
		Part_NippleL = GetCachedPart("NippleL");
		Part_SkinButtOutline = GetCachedPart("SkinButtOutline");
		Part_ButtReverseMask = GetCachedPart("ButtReverseMask");
		Part_HairHologram = GetCachedPart("HairHologram");
		Part_H0_Front_Bang = GetCachedPart("H0_Front_Bang");
		Part_H0_SideLeft = GetCachedPart("H0_SideLeft");
		Part_H0_BackLeft = GetCachedPart("H0_BackLeft");
		Part_H0_Back = GetCachedPart("H0_Back");
		Part_SkinEyelidR = GetCachedPart("SkinEyelidR");
		Part_SkinEyelidL = GetCachedPart("SkinEyelidL");
		Part_EyelashL2 = GetCachedPart("EyelashL2");
		Part_EyelashLMid4 = GetCachedPart("EyelashLMid4");
		Part_EyelashLLow2 = GetCachedPart("EyelashLLow2");
		Part_EyeWhiteL2 = GetCachedPart("EyeWhiteL2");
		Part_EyelashR2 = GetCachedPart("EyelashR2");
		Part_EyeLashRMid3 = GetCachedPart("EyeLashRMid3");
		Part_EyelashRMid2 = GetCachedPart("EyelashRMid2");
		Part_EyelashRLow2 = GetCachedPart("EyelashRLow2");
		Part_EyeWhiteR2 = GetCachedPart("EyeWhiteR2");
		Part_SkinNose = GetCachedPart("SkinNose");
		Part_HairShadowL = GetCachedPart("HairShadowL");
		Part_HairShadowR = GetCachedPart("HairShadowR");
		Part_SkinFace2 = GetCachedPart("SkinFace2");
		Part_SkinNeck2 = GetCachedPart("SkinNeck2");
		Part_H0_BackRight = GetCachedPart("H0_BackRight");
		Part_DickSkin = GetCachedPart("DickSkin");
		Part_DickMain = GetCachedPart("DickMain");
		Part_CumDick1 = GetCachedPart("CumDick1");
		Part_CumDick2 = GetCachedPart("CumDick2");
		Part_SkinRibcage = GetCachedPart("SkinRibcage");
		Part_SkinWaistLower = GetCachedPart("SkinWaistLower");
		Part_AttachArmRHandBack = GetCachedPart("AttachArmRHandBack");
		Part_AttachLegRThigh = GetCachedPart("AttachLegRThigh");
		Part_H0_SideAlt_Strand = GetCachedPart("H0_SideAlt_Strand");
		Part_EyelashLMid3 = GetCachedPart("EyelashLMid3");
		Part_CumOutsideAssSide2 = GetCachedPart("CumOutsideAssSide2");
		Part_CumOutsideAssBottom = GetCachedPart("CumOutsideAssBottom");
		Part_CumOutsideAss2 = GetCachedPart("CumOutsideAss2");
		Part_CumOutsideAss = GetCachedPart("CumOutsideAss");
		Part_CumOutsideThighBack = GetCachedPart("CumOutsideThighBack");
		Part_CumOutsideAssSide = GetCachedPart("CumOutsideAssSide");
		Part_CumOutsideAssUp = GetCachedPart("CumOutsideAssUp");
		Part_CumOutsideBelly = GetCachedPart("CumOutsideBelly");
		Part_CumOutsideAssUp2 = GetCachedPart("CumOutsideAssUp2");
		Part_DickMask = GetCachedPart("DickMask");
		Part_MCthigh = GetCachedPart("MCthigh");
		Part_MCbelly = GetCachedPart("MCbelly");
		Part_MCPalm = GetCachedPart("MCPalm");
		Part_MCPinky = GetCachedPart("MCPinky");
		Part_MCThumb = GetCachedPart("MCThumb");
		Part_MCMiddleFinger = GetCachedPart("MCMiddleFinger");
		Part_MCRingFinger = GetCachedPart("MCRingFinger");
		Part_MCIndexFinger = GetCachedPart("MCIndexFinger");
		Part_MCIndexFinger2 = GetCachedPart("MCIndexFinger2");
		Part_MCRingFinger2 = GetCachedPart("MCRingFinger2");
		Part_MCMiddleFinger2 = GetCachedPart("MCMiddleFinger2");
		Part_MCThumb2 = GetCachedPart("MCThumb2");
		Part_MCPinky2 = GetCachedPart("MCPinky2");
		Part_MCPalm2 = GetCachedPart("MCPalm2");
		Part_AttachArmRLower = GetCachedPart("AttachArmRLower");
		Part_AttachArmRUpper = GetCachedPart("AttachArmRUpper");
		Part_Iris2L = GetCachedPart("Iris2L");
		Part_Iris1L = GetCachedPart("Iris1L");
		Part_PupilL3 = GetCachedPart("PupilL3");
		Part_Iris2R = GetCachedPart("Iris2R");
		Part_Iris1R = GetCachedPart("Iris1R");
		Part_PupilR = GetCachedPart("PupilR");
		Part_BrowR = GetCachedPart("BrowR");
		Part_BrowL = GetCachedPart("BrowL");
		Part_LowerLipMask = GetCachedPart("LowerLipMask");
		Part_SkinLowerLip = GetCachedPart("SkinLowerLip");
		Part_UpperLipMask = GetCachedPart("UpperLipMask");
		Part_SkinUpperLip = GetCachedPart("SkinUpperLip");
		Part_SharpTeethUpper = GetCachedPart("SharpTeethUpper");
		Part_Tongue = GetCachedPart("Tongue");
		Part_InnerMouth = GetCachedPart("InnerMouth");
		Part_TeethUpper = GetCachedPart("TeethUpper");
		Part_TeethLower = GetCachedPart("TeethLower");
		Part_SkinMouth3 = GetCachedPart("SkinMouth3");
		Part_SkinMouth2Lower = GetCachedPart("SkinMouth2Lower");
		Part_Tongue3 = GetCachedPart("Tongue3");
		Part_InnerMask = GetCachedPart("InnerMask");
		Part_InnerMouth2 = GetCachedPart("InnerMouth2");
		Part_Blush = GetCachedPart("Blush");
		Part_BluescreenOld = GetCachedPart("BluescreenOld");
		Part_NipPiercingLeft = GetCachedPart("NipPiercingLeft");
		Part_SkinSeamlineButtR_POV = GetCachedPart("SkinSeamlineButtR_POV");
		Part_SkinButtR_POV = GetCachedPart("SkinButtR_POV");
		Part_SkinSeamlineButtL_POV = GetCachedPart("SkinSeamlineButtL_POV");
		Part_SkinButtL_POV = GetCachedPart("SkinButtL_POV");
		Part_Anus_POV = GetCachedPart("Anus_POV");
		Part_SkinVaginaR_POV = GetCachedPart("SkinVaginaR_POV");
		Part_SkinVaginaL_POV = GetCachedPart("SkinVaginaL_POV");
		Part_DickMask_POV = GetCachedPart("DickMask_POV");
		Part_CumDick1_POV = GetCachedPart("CumDick1_POV");
		Part_CumDick2_POV = GetCachedPart("CumDick2_POV");
		Part_DickShadow = GetCachedPart("DickShadow");
		Part_DickMain_POV = GetCachedPart("DickMain_POV");
		Part_VaginaInner_POV = GetCachedPart("VaginaInner_POV");
		Part_SkinAnusTop_POV = GetCachedPart("SkinAnusTop_POV");
		Part_AnusInnerAndBottom_POV = GetCachedPart("AnusInnerAndBottom_POV");
		Part_SkinSeamlineThighR_POV = GetCachedPart("SkinSeamlineThighR_POV");
		Part_ThighR_POV = GetCachedPart("ThighR_POV");
		Part_SkinSeamlineThighL_POV = GetCachedPart("SkinSeamlineThighL_POV");
		Part_ThighL_POV = GetCachedPart("ThighL_POV");
		Part_AttachLegRThigh_POV = GetCachedPart("AttachLegRThigh_POV");
		Part_AttachLegLThigh_POV = GetCachedPart("AttachLegLThigh_POV");
		Part_AttachChargeHole_POV = GetCachedPart("AttachChargeHole_POV");
		Part_SkinPelvis2_POV = GetCachedPart("SkinPelvis2_POV");
		Part_SkinScapulaL_POV = GetCachedPart("SkinScapulaL_POV");
		Part_SkinScapulaR_POV = GetCachedPart("SkinScapulaR_POV");
		Part_SkinShoulderL_POV = GetCachedPart("SkinShoulderL_POV");
		Part_SkinShoulderR_POV = GetCachedPart("SkinShoulderR_POV");
		Part_SkinRibcageBase_POV = GetCachedPart("SkinRibcageBase_POV");
		Part_H0_Back1_POV = GetCachedPart("H0_Back1_POV");
		Part_SkinNeck_POV = GetCachedPart("SkinNeck_POV");
		Part_SkinUpperArmL_POV = GetCachedPart("SkinUpperArmL_POV");
		Part_SkinUpperArmR_POV = GetCachedPart("SkinUpperArmR_POV");
		Part_AttachArmLUpper_POV = GetCachedPart("AttachArmLUpper_POV");
		Part_AttachArmRUpper_POV = GetCachedPart("AttachArmRUpper_POV");
		Part_AttachArmLLower_POV = GetCachedPart("AttachArmLLower_POV");
		Part_AttachArmRLower_POV = GetCachedPart("AttachArmRLower_POV");
		Part_AttachArmLHand_POV = GetCachedPart("AttachArmLHand_POV");
		Part_AttachArmRHand_POV = GetCachedPart("AttachArmRHand_POV");
		Part_SkinLowerWaist_POV = GetCachedPart("SkinLowerWaist_POV");
		Part_SkinUpperWaist_POV = GetCachedPart("SkinUpperWaist_POV");
		Part_SkinLowerWaistBase_POV = GetCachedPart("SkinLowerWaistBase_POV");
		Part_SkinUpperWaistBase_POV = GetCachedPart("SkinUpperWaistBase_POV");
		Part_Shadow_ThighL_POV = GetCachedPart("Shadow_ThighL_POV");
		Part_Shadow_ThighR_POV = GetCachedPart("Shadow_ThighR_POV");
		Part_Shadow_AssL_POV = GetCachedPart("Shadow_AssL_POV");
		Part_Shadow_AssR_POV = GetCachedPart("Shadow_AssR_POV");
		Part_MCSpreadL_POV = GetCachedPart("MCSpreadL_POV");
		Part_MCSpread_ThumbL_POV = GetCachedPart("MCSpread_ThumbL_POV");
		Part_MCSpreadR_POV = GetCachedPart("MCSpreadR_POV");
		Part_MCSpread_ThumbR_POV = GetCachedPart("MCSpread_ThumbR_POV");
		Part_MCSpankL_POV = GetCachedPart("MCSpankL_POV");
		Part_MCSpankR_POV = GetCachedPart("MCSpankR_POV");
		Part_MCAnusHandWrist_POV = GetCachedPart("MCAnusHandWrist_POV");
		Part_MCAnusHandMain_POV = GetCachedPart("MCAnusHandMain_POV");
		Part_MCAnusHandFinger_POV = GetCachedPart("MCAnusHandFinger_POV");
		Part_MCAnusHandThumb_POV = GetCachedPart("MCAnusHandThumb_POV");
		Part_MCWaistGrabL_POV = GetCachedPart("MCWaistGrabL_POV");
		Part_MCWaistGrabL2_POV = GetCachedPart("MCWaistGrabL2_POV");
		Part_MCWaistGrabR_POV = GetCachedPart("MCWaistGrabR_POV");
		Part_MCWaistGrabR2_POV = GetCachedPart("MCWaistGrabR2_POV");
		Part_CumAnal1_POV = GetCachedPart("CumAnal1_POV");
		Part_CumDrip1_POV = GetCachedPart("CumDrip1_POV");
		Part_CumVagina1_POV = GetCachedPart("CumVagina1_POV");
		Part_H0_SideLeftMask_POV = GetCachedPart("H0_SideLeftMask_POV");
		Part_H0_SideLeft_POV = GetCachedPart("H0_SideLeft_POV");
		Part_SkinFaceSeamline_POV = GetCachedPart("SkinFaceSeamline_POV");
		Part_SkinSideUpperLip_POV = GetCachedPart("SkinSideUpperLip_POV");
		Part_SkinSideLowerLip_POV = GetCachedPart("SkinSideLowerLip_POV");
		Part_SkinSideFace_POV = GetCachedPart("SkinSideFace_POV");
		Part_H0_Front_Bang_POV = GetCachedPart("H0_Front_Bang_POV");
		Part_MCPet_POV = GetCachedPart("MCPet_POV");
		Part_MCVaginaHandMain_POV = GetCachedPart("MCVaginaHandMain_POV");
		Part_MCVaginaHandInside_POV = GetCachedPart("MCVaginaHandInside_POV");
		Part_MCSpread_ThumbL_POV2 = GetCachedPart("MCSpread_ThumbL_POV2");
		Part_MCSpreadL_POV2 = GetCachedPart("MCSpreadL_POV2");
		Part_MCSpread_ThumbR_POV2 = GetCachedPart("MCSpread_ThumbR_POV2");
		Part_MCSpreadR_POV2 = GetCachedPart("MCSpreadR_POV2");
		Part_SkinVaginaTop_POV = GetCachedPart("SkinVaginaTop_POV");
		Part_SkinButtBase_POV = GetCachedPart("SkinButtBase_POV");
		Part_Tongue_POV = GetCachedPart("Tongue_POV");
		Part_Blush_POV = GetCachedPart("Blush_POV");
		Part_DickSkin_POV = GetCachedPart("DickSkin_POV");
		Part_CumWomb1_POV = GetCachedPart("CumWomb1_POV");
		Part_CumLeakStretchTop = GetCachedPart("CumLeakStretchTop");
		Part_CumLeakStretchBottom = GetCachedPart("CumLeakStretchBottom");
		Part_SkinSmirk = GetCachedPart("SkinSmirk");
		Part_CumOutsideFaceNose_POV = GetCachedPart("CumOutsideFaceNose_POV");
		Part_CumOutsideFaceLeft_POV = GetCachedPart("CumOutsideFaceLeft_POV");
		Part_CumOutsideInnerThighR_POV = GetCachedPart("CumOutsideInnerThighR_POV");
		Part_CumOutsideInnerThighR2_POV = GetCachedPart("CumOutsideInnerThighR2_POV");
		Part_CumOutsideInnerThighLowerL_POV = GetCachedPart("CumOutsideInnerThighLowerL_POV");
		Part_CumOutsideInnerThighL_POV = GetCachedPart("CumOutsideInnerThighL_POV");
		Part_CumOutsideInnerThighL2_POV = GetCachedPart("CumOutsideInnerThighL2_POV");
		Part_CumOutsideAss2_POV = GetCachedPart("CumOutsideAss2_POV");
		Part_CumOutsideAss_POV = GetCachedPart("CumOutsideAss_POV");
		Part_CumOutsideThighBack_POV = GetCachedPart("CumOutsideThighBack_POV");
		Part_CumOutsideAssUp_POV = GetCachedPart("CumOutsideAssUp_POV");
		Part_CumOutsideAssSide2_POV = GetCachedPart("CumOutsideAssSide2_POV");
		Part_CumOutsideAssR2_POV = GetCachedPart("CumOutsideAssR2_POV");
		Part_CumOutsideAssUp2_POV = GetCachedPart("CumOutsideAssUp2_POV");
		Part_CumOutsideFaceNose = GetCachedPart("CumOutsideFaceNose");
		Part_CumOutsideFaceLeft = GetCachedPart("CumOutsideFaceLeft");
		Part_CumOutsideAssR_POV = GetCachedPart("CumOutsideAssR_POV");
		Part_CumOutsideAssCenterR_POV = GetCachedPart("CumOutsideAssCenterR_POV");
		Part_CumOutsideAssCenterL_POV = GetCachedPart("CumOutsideAssCenterL_POV");
		Part_CumOutsideScapulaR_POV = GetCachedPart("CumOutsideScapulaR_POV");
		Part_CumOutsideThighL_POV = GetCachedPart("CumOutsideThighL_POV");
		Part_CumOutsideWaistUpperCenter_POV = GetCachedPart("CumOutsideWaistUpperCenter_POV");
		Part_CumOutsideAssBottom_POV2 = GetCachedPart("CumOutsideAssBottom_POV2");
		Part_CumOutsideBack1_POV = GetCachedPart("CumOutsideBack1_POV");
		Part_CumOutsideAssR3_POV = GetCachedPart("CumOutsideAssR3_POV");
		Part_CumOutsideAssL_POV = GetCachedPart("CumOutsideAssL_POV");
		Part_CumOutsideFaceRight = GetCachedPart("CumOutsideFaceRight");
		Part_ClitRub = GetCachedPart("ClitRub");
		Part_VaginaInner = GetCachedPart("VaginaInner");
		coloringGroups[ColoringGroupEnum.Hair0_Top] = new ColoringGroup(ColoringGroupEnum.Hair0_Top, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair0_Back] = new ColoringGroup(ColoringGroupEnum.Hair0_Back, new List<Part> { Part_H0_BackLeft, Part_H0_Back, Part_H0_BackRight, Part_H0_Back1_POV });
		coloringGroups[ColoringGroupEnum.Hair0_Braid] = new ColoringGroup(ColoringGroupEnum.Hair0_Braid, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair0_Braid_HairTieAccessory] = new ColoringGroup(ColoringGroupEnum.Hair0_Braid_HairTieAccessory, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair0_Side] = new ColoringGroup(ColoringGroupEnum.Hair0_Side, new List<Part> { Part_H0_SideLeft, Part_H0_SideLeftMask_POV, Part_H0_SideLeft_POV });
		coloringGroups[ColoringGroupEnum.Hair0_SideAlt] = new ColoringGroup(ColoringGroupEnum.Hair0_SideAlt, new List<Part> { Part_H0_SideAlt_Strand });
		coloringGroups[ColoringGroupEnum.Hair0_Front] = new ColoringGroup(ColoringGroupEnum.Hair0_Front, new List<Part> { Part_H0_Front_Bang, Part_H0_Front_Bang_POV });
		coloringGroups[ColoringGroupEnum.Hair0_FrontClippedUp_Bang] = new ColoringGroup(ColoringGroupEnum.Hair0_FrontClippedUp_Bang, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair0_FrontClippedUp_HairClip] = new ColoringGroup(ColoringGroupEnum.Hair0_FrontClippedUp_HairClip, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair1_Top] = new ColoringGroup(ColoringGroupEnum.Hair1_Top, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair1_Front] = new ColoringGroup(ColoringGroupEnum.Hair1_Front, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair1_Side] = new ColoringGroup(ColoringGroupEnum.Hair1_Side, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair1_Back] = new ColoringGroup(ColoringGroupEnum.Hair1_Back, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair2_Front] = new ColoringGroup(ColoringGroupEnum.Hair2_Front, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair3_Front] = new ColoringGroup(ColoringGroupEnum.Hair3_Front, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair3_Side] = new ColoringGroup(ColoringGroupEnum.Hair3_Side, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair3_Top] = new ColoringGroup(ColoringGroupEnum.Hair3_Top, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair3_Back] = new ColoringGroup(ColoringGroupEnum.Hair3_Back, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair4_Ponytail] = new ColoringGroup(ColoringGroupEnum.Hair4_Ponytail, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hair4_KnotAccessory] = new ColoringGroup(ColoringGroupEnum.Hair4_KnotAccessory, new List<Part>());
		coloringGroups[ColoringGroupEnum.Skirt] = new ColoringGroup(ColoringGroupEnum.Skirt, new List<Part>());
		coloringGroups[ColoringGroupEnum.Shirt] = new ColoringGroup(ColoringGroupEnum.Shirt, new List<Part>());
		coloringGroups[ColoringGroupEnum.ShirtShadow] = new ColoringGroup(ColoringGroupEnum.ShirtShadow, new List<Part>());
		coloringGroups[ColoringGroupEnum.StockingL] = new ColoringGroup(ColoringGroupEnum.StockingL, new List<Part>());
		coloringGroups[ColoringGroupEnum.StockingR] = new ColoringGroup(ColoringGroupEnum.StockingR, new List<Part>());
		coloringGroups[ColoringGroupEnum.Panties] = new ColoringGroup(ColoringGroupEnum.Panties, new List<Part>());
		coloringGroups[ColoringGroupEnum.Bra] = new ColoringGroup(ColoringGroupEnum.Bra, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hoodie] = new ColoringGroup(ColoringGroupEnum.Hoodie, new List<Part>());
		coloringGroups[ColoringGroupEnum.HoodieStrings] = new ColoringGroup(ColoringGroupEnum.HoodieStrings, new List<Part>());
		coloringGroups[ColoringGroupEnum.Pants] = new ColoringGroup(ColoringGroupEnum.Pants, new List<Part>());
		coloringGroups[ColoringGroupEnum.PantsBelt] = new ColoringGroup(ColoringGroupEnum.PantsBelt, new List<Part>());
		coloringGroups[ColoringGroupEnum.Dress1] = new ColoringGroup(ColoringGroupEnum.Dress1, new List<Part>());
		coloringGroups[ColoringGroupEnum.Dress2Alt] = new ColoringGroup(ColoringGroupEnum.Dress2Alt, new List<Part>());
		coloringGroups[ColoringGroupEnum.Dress2] = new ColoringGroup(ColoringGroupEnum.Dress2, new List<Part>());
		coloringGroups[ColoringGroupEnum.ShoeR] = new ColoringGroup(ColoringGroupEnum.ShoeR, new List<Part>());
		coloringGroups[ColoringGroupEnum.ShoeL] = new ColoringGroup(ColoringGroupEnum.ShoeL, new List<Part>());
		coloringGroups[ColoringGroupEnum.PointyEars] = new ColoringGroup(ColoringGroupEnum.PointyEars, new List<Part>());
		coloringGroups[ColoringGroupEnum.WizardHat] = new ColoringGroup(ColoringGroupEnum.WizardHat, new List<Part>());
		coloringGroups[ColoringGroupEnum.BellChoker] = new ColoringGroup(ColoringGroupEnum.BellChoker, new List<Part>());
		coloringGroups[ColoringGroupEnum.CuteBow] = new ColoringGroup(ColoringGroupEnum.CuteBow, new List<Part>());
		coloringGroups[ColoringGroupEnum.PumpkinHairPin] = new ColoringGroup(ColoringGroupEnum.PumpkinHairPin, new List<Part>());
		coloringGroups[ColoringGroupEnum.Headband] = new ColoringGroup(ColoringGroupEnum.Headband, new List<Part>());
		coloringGroups[ColoringGroupEnum.NipplePiercingL] = new ColoringGroup(ColoringGroupEnum.NipplePiercingL, new List<Part> { Part_NipPiercingLeft });
		coloringGroups[ColoringGroupEnum.NipplePiercingR] = new ColoringGroup(ColoringGroupEnum.NipplePiercingR, new List<Part>());
		coloringGroups[ColoringGroupEnum.NavelPiercing] = new ColoringGroup(ColoringGroupEnum.NavelPiercing, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniTop_Main] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Main, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniTop_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Alt, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniBottom_Main] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Main, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniBottom_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Alt, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniBottom_Uncolored] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Uncolored, new List<Part>());
		coloringGroups[ColoringGroupEnum.IrisL] = new ColoringGroup(ColoringGroupEnum.IrisL, new List<Part> { Part_PupilL3 });
		coloringGroups[ColoringGroupEnum.IrisR] = new ColoringGroup(ColoringGroupEnum.IrisR, new List<Part> { Part_PupilR });
		coloringGroups[ColoringGroupEnum.PupilL] = new ColoringGroup(ColoringGroupEnum.PupilL, new List<Part> { Part_Iris1L });
		coloringGroups[ColoringGroupEnum.PupilR] = new ColoringGroup(ColoringGroupEnum.PupilR, new List<Part> { Part_Iris1R });
		coloringGroups[ColoringGroupEnum.Sclera] = new ColoringGroup(ColoringGroupEnum.Sclera, new List<Part> { Part_EyeWhiteL2, Part_EyeWhiteR2 });
		coloringGroups[ColoringGroupEnum.Eyelash] = new ColoringGroup(ColoringGroupEnum.Eyelash, new List<Part> { Part_EyelashL2, Part_EyelashLMid4, Part_EyelashLLow2, Part_EyelashR2, Part_EyeLashRMid3, Part_EyelashRMid2, Part_EyelashRLow2, Part_EyelashLMid3 });
		coloringGroups[ColoringGroupEnum.HeartPupils] = new ColoringGroup(ColoringGroupEnum.HeartPupils, new List<Part> { Part_Iris2L, Part_Iris2R });
		coloringGroups[ColoringGroupEnum.EyeBrows] = new ColoringGroup(ColoringGroupEnum.EyeBrows, new List<Part> { Part_BrowR, Part_BrowL });
		coloringGroups[ColoringGroupEnum.Nipples] = new ColoringGroup(ColoringGroupEnum.Nipples, new List<Part> { Part_NippleL });
		coloringGroups[ColoringGroupEnum.Vagina] = new ColoringGroup(ColoringGroupEnum.Vagina, new List<Part> { Part_VaginaInner_POV, Part_VaginaInner });
		coloringGroups[ColoringGroupEnum.Xray] = new ColoringGroup(ColoringGroupEnum.Xray, new List<Part>());
		coloringGroups[ColoringGroupEnum.Anus] = new ColoringGroup(ColoringGroupEnum.Anus, new List<Part> { Part_Anus_POV, Part_AnusInnerAndBottom_POV });
		coloringGroups[ColoringGroupEnum.Teeth] = new ColoringGroup(ColoringGroupEnum.Teeth, new List<Part> { Part_TeethUpper, Part_TeethLower });
		coloringGroups[ColoringGroupEnum.Mouth] = new ColoringGroup(ColoringGroupEnum.Mouth, new List<Part> { Part_Tongue, Part_InnerMouth, Part_Tongue3, Part_InnerMouth2, Part_Tongue_POV });
		coloringGroups[ColoringGroupEnum.Boobs] = new ColoringGroup(ColoringGroupEnum.Boobs, new List<Part> { Part_SkinBoob2 });
		coloringGroups[ColoringGroupEnum.Ass] = new ColoringGroup(ColoringGroupEnum.Ass, new List<Part> { Part_SkinButtSeamline, Part_SkinButt, Part_SkinButtOutline, Part_SkinButtR_POV, Part_SkinButtL_POV, Part_SkinButtBase_POV });
		coloringGroups[ColoringGroupEnum.Pelvic] = new ColoringGroup(ColoringGroupEnum.Pelvic, new List<Part> { Part_SkinPelvis, Part_SkinVaginaR_POV, Part_SkinVaginaL_POV, Part_SkinPelvis2_POV, Part_SkinVaginaTop_POV });
		coloringGroups[ColoringGroupEnum.Thigh] = new ColoringGroup(ColoringGroupEnum.Thigh, new List<Part> { Part_Thighs, Part_Thighs2, Part_ThighR_POV, Part_ThighL_POV });
		coloringGroups[ColoringGroupEnum.Tummy] = new ColoringGroup(ColoringGroupEnum.Tummy, new List<Part> { Part_SkinWaistLower });
		coloringGroups[ColoringGroupEnum.Skin] = new ColoringGroup(ColoringGroupEnum.Skin, new List<Part>
		{
			Part_SkinArm, Part_SkinArm2, Part_SkinEyelidR, Part_SkinEyelidL, Part_SkinNose, Part_SkinFace2, Part_SkinNeck2, Part_SkinRibcage, Part_SkinLowerLip, Part_SkinUpperLip,
			Part_SkinMouth3, Part_SkinMouth2Lower, Part_SkinSeamlineButtR_POV, Part_SkinSeamlineButtL_POV, Part_SkinAnusTop_POV, Part_SkinSeamlineThighR_POV, Part_SkinSeamlineThighL_POV, Part_SkinScapulaL_POV, Part_SkinScapulaR_POV, Part_SkinShoulderL_POV,
			Part_SkinShoulderR_POV, Part_SkinRibcageBase_POV, Part_SkinNeck_POV, Part_SkinUpperArmL_POV, Part_SkinUpperArmR_POV, Part_SkinLowerWaist_POV, Part_SkinUpperWaist_POV, Part_SkinLowerWaistBase_POV, Part_SkinUpperWaistBase_POV, Part_SkinFaceSeamline_POV,
			Part_SkinSideUpperLip_POV, Part_SkinSideLowerLip_POV, Part_SkinSideFace_POV, Part_SkinSmirk
		});
		coloringGroups[ColoringGroupEnum.Blush] = new ColoringGroup(ColoringGroupEnum.Blush, new List<Part> { Part_Blush, Part_Blush_POV });
		coloringGroups[ColoringGroupEnum.SharpTeeth] = new ColoringGroup(ColoringGroupEnum.SharpTeeth, new List<Part> { Part_SharpTeethUpper });
		coloringGroups[ColoringGroupEnum.CatEarMain] = new ColoringGroup(ColoringGroupEnum.CatEarMain, new List<Part>());
		coloringGroups[ColoringGroupEnum.CatEarSecondary] = new ColoringGroup(ColoringGroupEnum.CatEarSecondary, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegLFoot] = new ColoringGroup(ColoringGroupEnum.LegLFoot, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegL] = new ColoringGroup(ColoringGroupEnum.LegL, new List<Part> { Part_AttachLegLThigh, Part_AttachLegLThigh_POV });
		coloringGroups[ColoringGroupEnum.LegRFoot] = new ColoringGroup(ColoringGroupEnum.LegRFoot, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegR] = new ColoringGroup(ColoringGroupEnum.LegR, new List<Part> { Part_AttachLegRThigh, Part_AttachLegRThigh_POV });
		coloringGroups[ColoringGroupEnum.ArmL] = new ColoringGroup(ColoringGroupEnum.ArmL, new List<Part> { Part_AttachArmLHand, Part_AttachArmLLower, Part_AttachArmLUpper, Part_AttachArmLUpper_POV, Part_AttachArmLLower_POV, Part_AttachArmLHand_POV });
		coloringGroups[ColoringGroupEnum.ArmR] = new ColoringGroup(ColoringGroupEnum.ArmR, new List<Part> { Part_AttachArmRHandBack, Part_AttachArmRLower, Part_AttachArmRUpper, Part_AttachArmRUpper_POV, Part_AttachArmRLower_POV, Part_AttachArmRHand_POV });
		coloringGroups[ColoringGroupEnum.Tail] = new ColoringGroup(ColoringGroupEnum.Tail, new List<Part>());
		coloringGroups[ColoringGroupEnum.TailPlug] = new ColoringGroup(ColoringGroupEnum.TailPlug, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hologram] = new ColoringGroup(ColoringGroupEnum.Hologram, new List<Part> { Part_HairHologram });
		coloringGroups[ColoringGroupEnum.OtherAttachments] = new ColoringGroup(ColoringGroupEnum.OtherAttachments, new List<Part> { Part_AttachChargeHole, Part_AttachChargeHole_POV });
		coloringGroups[ColoringGroupEnum.MCSkin] = new ColoringGroup(ColoringGroupEnum.MCSkin, new List<Part>
		{
			Part_MCthigh, Part_MCbelly, Part_MCPalm, Part_MCPinky, Part_MCThumb, Part_MCMiddleFinger, Part_MCRingFinger, Part_MCIndexFinger, Part_MCIndexFinger2, Part_MCRingFinger2,
			Part_MCMiddleFinger2, Part_MCThumb2, Part_MCPinky2, Part_MCPalm2, Part_MCSpreadL_POV, Part_MCSpread_ThumbL_POV, Part_MCSpreadR_POV, Part_MCSpread_ThumbR_POV, Part_MCSpankL_POV, Part_MCSpankR_POV,
			Part_MCAnusHandWrist_POV, Part_MCAnusHandMain_POV, Part_MCAnusHandFinger_POV, Part_MCAnusHandThumb_POV, Part_MCWaistGrabL_POV, Part_MCWaistGrabL2_POV, Part_MCWaistGrabR_POV, Part_MCWaistGrabR2_POV, Part_MCPet_POV, Part_MCVaginaHandMain_POV,
			Part_MCVaginaHandInside_POV, Part_MCSpread_ThumbL_POV2, Part_MCSpreadL_POV2, Part_MCSpread_ThumbR_POV2, Part_MCSpreadR_POV2
		});
		coloringGroups[ColoringGroupEnum.MCCockSkin] = new ColoringGroup(ColoringGroupEnum.MCCockSkin, new List<Part> { Part_DickSkin, Part_DickSkin_POV });
		coloringGroups[ColoringGroupEnum.MCCockTip] = new ColoringGroup(ColoringGroupEnum.MCCockTip, new List<Part> { Part_DickMain, Part_DickMain_POV });
		coloringGroups[ColoringGroupEnum.Shadow] = new ColoringGroup(ColoringGroupEnum.Shadow, new List<Part> { Part_HairShadowL, Part_HairShadowR, Part_DickShadow, Part_Shadow_ThighL_POV, Part_Shadow_ThighR_POV, Part_Shadow_AssL_POV, Part_Shadow_AssR_POV });
		coloringGroups[ColoringGroupEnum.Highlight] = new ColoringGroup(ColoringGroupEnum.Highlight, new List<Part>());
		coloringGroups[ColoringGroupEnum.Mask] = new ColoringGroup(ColoringGroupEnum.Mask, new List<Part> { Part_ButtReverseMask, Part_DickMask, Part_LowerLipMask, Part_UpperLipMask, Part_InnerMask, Part_DickMask_POV });
		coloringGroups[ColoringGroupEnum.Moddable] = new ColoringGroup(ColoringGroupEnum.Moddable, new List<Part>());
		coloringGroups[ColoringGroupEnum.HitArea] = new ColoringGroup(ColoringGroupEnum.HitArea, new List<Part>
		{
			HitAreaBoob, HitAreaEyeL, HitAreaEyeR, HitAreaAss, HitAreaMouth, HitAreaMC, HitAreaButtL_POV, HitAreaMC_POV, HitAreaButtR_POV, HitAreaWaist_POV,
			HitAreaVaginaSpread_POV, HitAreaAnusSide_POV, HitAreaAnusCenter_POV, HitAreaVaginaCenter_POV, HitArea_HeadpatPOV, HitArea_Eyes_POV, HitAreaVaginaClit_POV
		});
		coloringGroups[ColoringGroupEnum.Cum] = new ColoringGroup(ColoringGroupEnum.Cum, new List<Part>
		{
			Part_CumDick1, Part_CumDick2, Part_CumOutsideAssSide2, Part_CumOutsideAssBottom, Part_CumOutsideAss2, Part_CumOutsideAss, Part_CumOutsideThighBack, Part_CumOutsideAssSide, Part_CumOutsideAssUp, Part_CumOutsideBelly,
			Part_CumOutsideAssUp2, Part_CumDick1_POV, Part_CumDick2_POV, Part_CumAnal1_POV, Part_CumDrip1_POV, Part_CumVagina1_POV, Part_CumWomb1_POV, Part_CumLeakStretchTop, Part_CumLeakStretchBottom, Part_CumOutsideFaceNose_POV,
			Part_CumOutsideFaceLeft_POV, Part_CumOutsideInnerThighR_POV, Part_CumOutsideInnerThighR2_POV, Part_CumOutsideInnerThighLowerL_POV, Part_CumOutsideInnerThighL_POV, Part_CumOutsideInnerThighL2_POV, Part_CumOutsideAss2_POV, Part_CumOutsideAss_POV, Part_CumOutsideThighBack_POV, Part_CumOutsideAssUp_POV,
			Part_CumOutsideAssSide2_POV, Part_CumOutsideAssR2_POV, Part_CumOutsideAssUp2_POV, Part_CumOutsideFaceNose, Part_CumOutsideFaceLeft, Part_CumOutsideAssR_POV, Part_CumOutsideAssCenterR_POV, Part_CumOutsideAssCenterL_POV, Part_CumOutsideScapulaR_POV, Part_CumOutsideThighL_POV,
			Part_CumOutsideWaistUpperCenter_POV, Part_CumOutsideAssBottom_POV2, Part_CumOutsideBack1_POV, Part_CumOutsideAssR3_POV, Part_CumOutsideAssL_POV, Part_CumOutsideFaceRight
		});
		coloringGroups[ColoringGroupEnum.Rest] = new ColoringGroup(ColoringGroupEnum.Rest, new List<Part> { Part_BluescreenOld, Part_ClitRub });
	}
}
