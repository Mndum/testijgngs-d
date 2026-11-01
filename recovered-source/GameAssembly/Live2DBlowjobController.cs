using System;
using System.Collections.Generic;
using System.Linq;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Physics;
using UnityEngine;

public sealed class Live2DBlowjobController : Live2DLewdSceneController, ILive2DController_BodyAngles, ILive2DController_BodyX, ILive2DController_BodyY, ILive2DController_HeadAngles, ILive2DController_HeadX, ILive2DController_HeadY, ILive2DController_HeadZ, ILive2DController_CatEarsMovement, ILive2DController_CommonFace, ILive2DController_MainFace, ILive2DController_Mouth, ILive2DController_EyeMovement, ILive2DController_EyeLMovement, ILive2DController_EyeRMovement, ILive2DController_EyeLEmotes, ILive2DController_EyeLCloseAndShock, ILive2DController_BrowLY, ILive2DController_BrowLRot, ILive2DController_BrowLEmote, ILive2DController_EyeREmotes, ILive2DController_EyeRCloseAndShock, ILive2DController_BrowRY, ILive2DController_BrowRRot, ILive2DController_BrowREmote, ILive2DController_BlushAndHeartEyes, ILive2DController_IndependentBlinking, ILive2DController_EyesHappinessEmote, ILive2DController_SalivaMouth, ILive2DController_TailWiggle, ILive2DController_Lewd_CumInsideMouth, ILive2DController_Lewd_CumShotBasic, ILive2DController_Manager_InsertionManager, ILive2DController_Manager_FrickStaticGuiManager, ILive2DController_Manager_CockManager, ILive2DController_BodyMod_BoobSize
{
	public sealed class BlowjobCockInsertionFuckAxis : InsertionHandlerFuckAxis
	{
		private Live2DBlowjobController _controller;

		private IPenisInfo CurrentPenis = IPenisInfo.DefaultNoPenisInfo;

		private bool _isInside;

		private float _currentDepth;

		private float _lastUpdateDepth;

		private float _lastFixedUpdateDepth;

		private float _currentDepthChangeDelta;

		private float _LastRealDepth;

		private bool _bottomedOut;

		public override bool IsInside => _isInside;

		public override bool IsAfterOutside => _isInside;

		private float CurrentDeltaIntensity => (float)Math.Tanh(_currentDepthChangeDelta * 20f);

		public float CurrentDepth => _currentDepth;

		public BlowjobCockInsertionFuckAxis(Live2DBlowjobController controller)
		{
			_controller = controller;
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
			_currentDepth = 0f;
			_isInside = false;
			_LastRealDepth = 0f;
		}

		public override bool CanThrustForward()
		{
			return _controller.ParamBodyY.Value < 0.9f;
		}

		public override bool CanThrustBackwards()
		{
			return _controller.ParamBodyY.Value > 0.1f;
		}

		public override void Update()
		{
			float thrustMovementAccumulator = Owner.ThrustMovementAccumulator;
			_controller.ParamMCBodyY.AddPercent(thrustMovementAccumulator * 50f);
			ThrustBot((0f - thrustMovementAccumulator) * _currentDepth * 0.5f);
			_currentDepth = Mathf.Clamp(_currentDepth, 0f, 1f);
			bool isFlacid = _controller.CockManager.IsFlacid;
			_controller.ParamHeadMovementY.Value = 0f - _currentDepth;
			_controller.ParamBodyY.Value = 0f - _currentDepth;
			if ((isFlacid && _currentDepth > _flacidInsertStart) || (!isFlacid && _currentDepth > _hardInsertStart))
			{
				float num = 0f - (_controller.ParamBodyY.Value * 0.1f + _controller.ParamHeadMovementY.Value * 0.7f + _controller.ParamHeadY.Value * 0.1f) + _controller.ParamMCBodyY.Value * 0.1f;
				float num2;
				if (isFlacid)
				{
					num = Mathf.Clamp01(Mathf.InverseLerp(0f, 1f - _flacidInsertStart, num - _flacidInsertStart));
					num2 = Mathf.Clamp01(Mathf.InverseLerp(0f, 1f - _flacidInsertStart, _currentDepth - _flacidInsertStart));
				}
				else
				{
					num = Mathf.Clamp01(num);
					num2 = _currentDepth;
				}
				if (!IsInside)
				{
					_isInside = true;
					_controller.FireEvent(InteractionMemoryEventEnum.Insert);
					_LastRealDepth = num;
				}
				_controller.ParamDickAngleY.Value = 0f - EasingFunction.EaseOutCubic(0f, 1f, num2);
				if (num2 > 0.4f)
				{
					_controller.ParamHeadY.Value = num2 * _controller.ParamHeadY.MinimumValue;
				}
				float num3 = Mathf.Abs(_LastRealDepth - num);
				_controller.CockManager.AddCumBarValue(num3 * 0.04f);
				_LastRealDepth = num;
				_controller.ParamForceMouthOpen.Value = EasingFunction.EaseOutCubic(0f, 1f, num);
				_controller.ParamMouthOpen.Value = 1f;
				float num4 = num * 0.5f;
				float num5 = num * 0.8f;
				_controller.ParamHeadZ.Value = Mathf.Clamp(_controller.ParamHeadZ.Value, 0f - num4, num4);
				_controller.ParamHeadX.Value = Mathf.Clamp(_controller.ParamHeadX.Value, 0f - num5, num5);
				_controller.ParamCockAngleZ.Value = _controller.ParamBodyX.Value;
			}
			else
			{
				if (IsInside)
				{
					if (!_controller.CockManager.IsFlacid)
					{
						float num6 = Mathf.Abs(CurrentDeltaIntensity);
						R.Sound.Blowjob_pullout_All.PlayRandomWithPitch(num6 * 0.5f, 0.75f + num6 * 0.5f + UnityEngine.Random.Range(-0.1f, 0.1f));
					}
					_controller.FireEvent(InteractionMemoryEventEnum.Pullout);
				}
				_controller.ParamForceMouthOpen.ResetToDefault();
				_isInside = false;
			}
			ReportFuckingSpeedUpdate(_controller.ParamBodyY.GetAbsChangeFromLastFrame() * 0.2f, Mathf.Abs(_lastUpdateDepth - _currentDepth));
			_lastUpdateDepth = _currentDepth;
		}

		public override void FixedUpdate()
		{
			if (_controller.CockManager.TargetCumBarValue > 0.3f)
			{
				GameVariables.Current.CurrentHorniness += 0.01f * Time.fixedDeltaTime;
			}
			_currentDepthChangeDelta = _currentDepth - _lastFixedUpdateDepth;
			if (IsInside)
			{
				float currentDeltaIntensity = CurrentDeltaIntensity;
				Mathf.Abs(currentDeltaIntensity);
				if (_currentDepth + _currentDepthChangeDelta >= 0.9f && !_bottomedOut && currentDeltaIntensity > 0.03f && !_controller.CockManager.IsFlacid)
				{
					R.Sound.Blowjob_bottomout_All.PlayRandom(UnityEngine.Random.Range(0.2f, 0.4f) * CurrentDeltaIntensity);
					_bottomedOut = true;
				}
				if (_currentDepth <= 0.8f && _currentDepthChangeDelta < 0f)
				{
					_bottomedOut = false;
				}
				_controller.FireEvent((Owner.MovementSoundHandler.SmoothedAbsDelta < 0.4f) ? InteractionMemoryEventEnum.BlowjobSlow : InteractionMemoryEventEnum.BlowjobFast);
			}
			_lastFixedUpdateDepth = _currentDepth;
		}

		public override float GetCurrentRealInsertionPosition()
		{
			return _LastRealDepth;
		}

		public override float GetCurrentInsertionPosition()
		{
			return _lastFixedUpdateDepth;
		}

		public override void ResetAccumulatedMovementToCurrentPosition()
		{
			_controller.ParamBodyY.Value = _controller.ParamBodyY.GetRealValue();
		}

		public void ThrustBot(float direction)
		{
			_currentDepth += direction;
		}
	}

	private bool _isPantsAsideMemorized;

	private Live2DInteractableLimbUsage<Live2DBlowjobController> _headInteractable;

	private Live2DInteractableLimbUsage<Live2DBlowjobController> _thumbMouthInteractable;

	private CockLimbUsage<Live2DBlowjobController> _cockFuckLimbUsage;

	private static float _flacidInsertStart = 0.55f;

	private static float _hardInsertStart = 0.05f;

	public override ControllerEnum ControllerEnum => ControllerEnum.Blowjob;

	public override SceneEnumFlag SceneEnum => SceneEnumFlag.Blowjob;

	public override bool IsPantsAside
	{
		get
		{
			return _isPantsAsideMemorized;
		}
		set
		{
			_isPantsAsideMemorized = value;
		}
	}

	public bool HeadHeld => IsHeld(_headInteractable);

	public InsertionHandler MouthInsertionHandler { get; private set; }

	public BlowjobCockInsertionFuckAxis MouthFuckAxis { get; private set; }

	public FrickStaticGuiManager FrickStaticGuiManager { get; private set; }

	public InsertionManager InsertionManager { get; private set; }

	public ICockManager CockManager { get; private set; }

	public float TipHeight => Mathf.Lerp(_hardInsertStart, _flacidInsertStart, ParamFlacidity.GetRealValue());

	public Live2DParameter ParamEnableThumbMouth { get; private set; }

	public Live2DParameter ParamThumbMouthX { get; private set; }

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

	public Live2DParameter ParamCumInMouth { get; private set; }

	public Live2DParameter ParamHideCumShot { get; private set; }

	public Live2DParameter ParamCumShotAnim { get; private set; }

	public Live2DParameter ParamFlacidity { get; private set; }

	public Live2DParameter ParamDickAngleY { get; private set; }

	public Live2DParameter ParamHeadMovementY { get; private set; }

	public Live2DParameter ParamCumOutsideY { get; private set; }

	public Live2DParameter ParamBodyY { get; private set; }

	public Live2DParameter ParamBodyX { get; private set; }

	public Live2DParameter ParamCockAngleZ { get; private set; }

	public Live2DParameter ParamForceMouthOpen { get; private set; }

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

	public Live2DParameter ParamToungeX { get; private set; }

	public Live2DParameter ParamToungeY { get; private set; }

	public Live2DParameter ParamBlush { get; private set; }

	public Live2DParameter ParamHeadZ { get; private set; }

	public Live2DParameter ParamHeadX { get; private set; }

	public Live2DParameter ParamHeadY { get; private set; }

	public Live2DParameter ParamEarL { get; private set; }

	public Live2DParameter ParamEarR { get; private set; }

	public Live2DParameter ParamSalivaPhysics { get; private set; }

	public Live2DParameter ParamPupilWiggle { get; private set; }

	public Live2DParameter ParamEarsWiggle { get; private set; }

	public Live2DParameter ParamHeadpatX { get; private set; }

	public Live2DParameter ParamHeadpatY { get; private set; }

	public Live2DParameter ParamHeadpat { get; private set; }

	public Live2DParameter ParamBrowLY { get; private set; }

	public Live2DParameter ParamBrowRY { get; private set; }

	public Live2DParameter ParamMouthForm { get; private set; }

	public Live2DParameter ParamFlattenNipples { get; private set; }

	public Live2DParameter ParamMCBodyY { get; private set; }

	public Live2DParameter ParamLegsSwitch { get; private set; }

	public Live2DParameter ParamHairPhysicsBaseToShort { get; private set; }

	public Live2DParameter ParamHairPhysicsShortToMid { get; private set; }

	public Live2DParameter ParamHairPhysicsMidToLong { get; private set; }

	public Live2DParameter ParamBoobSize { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeShock { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowRot { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowEmote { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballX { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEyeballY { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamEar { get; private set; }

	public Live2DParameter[] SymParam_LR_ParamBrowY { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Hair { get; private set; }

	public Live2DPhysicsOverride PhysicsSubRig_Tail { get; private set; }

	public Part HitAreaHeadpat { get; private set; }

	public Part HitAreaOpenMouth { get; private set; }

	public Part HitAreaCloseMouth { get; private set; }

	public Part HitAreaFaceStroke { get; private set; }

	public Part HitAreaThumbMouth { get; private set; }

	public Part Part_BellChokerBell { get; private set; }

	public Part Part_ModdableHairBack { get; private set; }

	public Part Part_ModdableFace { get; private set; }

	public Part Part_ModdableBack { get; private set; }

	public Part Part_BellChokerMain { get; private set; }

	public Part Part_PointyEarR { get; private set; }

	public Part Part_PointyEarL { get; private set; }

	public Part Part_AltLongHoodieString { get; private set; }

	public Part Part_LongHoodieHoodFront { get; private set; }

	public Part Part_LongHoodieHoodBack { get; private set; }

	public Part Part_LongHoodieChest { get; private set; }

	public Part Part_LongHoodieFront { get; private set; }

	public Part Part_LongHoodieShoulderR { get; private set; }

	public Part Part_LongHoodieShoulderL { get; private set; }

	public Part Part_CumDick1 { get; private set; }

	public Part Part_CumDick2 { get; private set; }

	public Part Part_DickSkin { get; private set; }

	public Part Part_DickMain { get; private set; }

	public Part Part_MCBody { get; private set; }

	public Part Part_AttachArmRHandR { get; private set; }

	public Part Part_AttachArmL { get; private set; }

	public Part Part_MClegL { get; private set; }

	public Part Part_MClegR { get; private set; }

	public Part Part_AttachArmLHandBack { get; private set; }

	public Part Part_AttachArmRHandBackR { get; private set; }

	public Part Part_PlainShirt_Front { get; private set; }

	public Part Part_PlainShirt_FrontNoBras { get; private set; }

	public Part Part_PlainShirt_Base { get; private set; }

	public Part Part_PlainShirt_SleeveL { get; private set; }

	public Part Part_PlainShirt_SleeveR { get; private set; }

	public Part Part_PlainShirt_Back { get; private set; }

	public Part Part_BraFrontR { get; private set; }

	public Part Part_BraFrontL { get; private set; }

	public Part Part_BrasUpperL { get; private set; }

	public Part Part_BrasUpperR { get; private set; }

	public Part Part_BraMid { get; private set; }

	public Part Part_BraLower { get; private set; }

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

	public Part Part_barcode { get; private set; }

	public Part Part_NippleR { get; private set; }

	public Part Part_NippleL { get; private set; }

	public Part Part_SkinBreastL { get; private set; }

	public Part Part_SkinBreastR { get; private set; }

	public Part Part_SkinChest { get; private set; }

	public Part Part_SkinBodyFront { get; private set; }

	public Part Part_SkinNeck { get; private set; }

	public Part Part_SkinShoulderL { get; private set; }

	public Part Part_SkinShoulderR { get; private set; }

	public Part Part_SkinArmL { get; private set; }

	public Part Part_SkinArmR { get; private set; }

	public Part Part_SkinBodyBack { get; private set; }

	public Part Part_CumOutsideFaceRight { get; private set; }

	public Part Part_CumOutsideFaceNose { get; private set; }

	public Part Part_CumOutsideFaceLeft { get; private set; }

	public Part Part_CockMask { get; private set; }

	public Part Part_ModdableShirtLeftSleeveLogo { get; private set; }

	public Part Part_ModdableShirtRightSleeveLogo { get; private set; }

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

	public Part Part_Dress2Chest { get; private set; }

	public Part Part_Dress2AltRib { get; private set; }

	public Part Part_Dress2AltBack { get; private set; }

	public Part Part_Dress2SleeveR { get; private set; }

	public Part Part_Dress2SleeveL { get; private set; }

	public Part Part_DressSleeveMask { get; private set; }

	public Part Part_Dress1_Strings { get; private set; }

	public Part Part_Dress1_Chest { get; private set; }

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

	public Part Part_CumMouth1 { get; private set; }

	public Part Part_CumMouth2 { get; private set; }

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

	public Part Part_ModdableHairFront { get; private set; }

	public Part Part_Headband { get; private set; }

	public Part Part_CuteBow { get; private set; }

	public Part Part_WizardHat { get; private set; }

	public Part Part_H0_FrontClippedUp_Bang { get; private set; }

	public Part Part_H0_FrontClippedUp_HairClip { get; private set; }

	public Part Part_H0_Front_Bang { get; private set; }

	public Part Part_H0_Side_BangL { get; private set; }

	public Part Part_H0_Side_BangR { get; private set; }

	public Part Part_H0_SideAlt_Strand { get; private set; }

	public Part Part_H2_Front_Bang { get; private set; }

	public Part Part_H3_Front { get; private set; }

	public Part Part_BikiniTop_A_StrapR { get; private set; }

	public Part Part_BikiniTop_A_OutlineR { get; private set; }

	public Part Part_BikiniTop_M_CupR { get; private set; }

	public Part Part_BikiniTop_A_StrapL { get; private set; }

	public Part Part_BikiniTop_A_OutlineL { get; private set; }

	public Part Part_BikiniTop_M_CupL { get; private set; }

	public Part Part_BikiniTop_A_Middle { get; private set; }

	private static void PrepareHeadInteractables(Live2DBlowjobController thisControllerLewd)
	{
		LimbUsageWithInteractable<Live2DBlowjobController> limbUsageWithInteractable = new LimbUsageWithInteractable<Live2DBlowjobController>(thisControllerLewd, LimbUsageChoiceEnum.RightArm)
		{
			HoldUpdateAction = (Live2DBlowjobController c, Vector2 mouseMove) =>
			{
				c.ParamHeadpatX.AddPercent(mouseMove.x * 10f);
				c.ParamHeadpatY.AddPercent(mouseMove.y * 12f);
				c.ParamHeadX.AddPercent(mouseMove.x * 10f);
				float num = (c.CockManager.IsFlacid ? Mathf.Lerp(c.MouthFuckAxis.CurrentDepth, 0.2f, 0.8f) : 0.8f);
				c.ParamHeadX.Value = Mathf.Clamp(c.ParamHeadX.Value, c.ParamHeadX.MinimumValue * num, c.ParamHeadX.MaximumValue * num);
				c.ParamHeadY.AddPercent(mouseMove.y * 10f);
				c.ParamHeadY.Value = Mathf.Clamp(c.ParamHeadY.Value, c.ParamHeadY.MinimumValue, c.ParamHeadY.MaximumValue * 0.5f);
				c.ParamHeadZ.AddPercent(mouseMove.x * 10f);
				if (Mathf.Abs(mouseMove.y) > 0.001f)
				{
					c.FireEvent(InteractionMemoryEventEnum.ForcedBlowjob);
				}
				c.MouthFuckAxis.ThrustBot((0f - mouseMove.y) * 10f);
			},
			HoldFixedUpdateAction = (Live2DBlowjobController c) =>
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
			EndLimbAction = (Live2DBlowjobController c, LetGoLimbsCaller caller) =>
			{
				c.ParamHeadpatX.ResetToDefault();
				c.ParamHeadpatY.ResetToDefault();
				c.ParamHeadX.ResetToDefault();
				c.ParamHeadZ.ResetToDefault();
			}
		};
		thisControllerLewd._headInteractable = thisControllerLewd.AddInteractableHitArea(thisControllerLewd.HitAreaHeadpat, new Live2DInteractableLimbUsage<Live2DBlowjobController>(thisControllerLewd, LOC.UI.TupleReferences.Interactable_GrabHead, limbUsageWithInteractable)
		{
			OnHoverStart = (Live2DBlowjobController c) =>
			{
				c.ParamHeadpat.SetPercent(1f);
			},
			OnHoverEnd = (Live2DBlowjobController c) =>
			{
				c.ParamHeadpat.SetPercent(0f);
			},
			HoverCursor = CursorSingleton.CursorType.NoCursor
		});
		LimbUsageWithInteractable<Live2DBlowjobController> limbUsageWithInteractable2 = new LimbUsageWithInteractable<Live2DBlowjobController>(thisControllerLewd, LimbUsageChoiceEnum.RightArm)
		{
			StartLimbAction = (Live2DBlowjobController c) =>
			{
				c.CurrentBrain.InteractionMemory.ActivateComplex(InteractionMemoryEnum.ThumbMouth);
				c.ParamEnableThumbMouth.SetMaxValue();
				c.ParamThumbMouthX.SetPercent(0.5f);
			},
			HoldUpdateAction = (Live2DBlowjobController c, Vector2 mouseMove) =>
			{
				c.ParamHeadMovementY.Value = 0f;
				c.ParamHeadY.Value = 0f;
				c.ParamBodyY.Value = 0f;
				c.ParamThumbMouthX.Value += mouseMove.x * 10f;
			},
			EndLimbAction = (Live2DBlowjobController c, LetGoLimbsCaller caller) =>
			{
				c.CurrentBrain.InteractionMemory.DeactivateComplex(InteractionMemoryEnum.ThumbMouth);
				c.ParamEnableThumbMouth.SetMinValue();
				c.ParamThumbMouthX.SetMinValue();
			}
		};
		thisControllerLewd._thumbMouthInteractable = thisControllerLewd.AddInteractableHitArea(thisControllerLewd.HitAreaThumbMouth, new Live2DInteractableLimbUsage<Live2DBlowjobController>(thisControllerLewd, LOC.UI.TupleReferences.Interactable_ThumbInMouth, limbUsageWithInteractable2)
		{
			ActiveFunc = (Live2DBlowjobController c) => c.ParamMouthOpen.Value > 1f && c.CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var _, out var fuckAxis) && fuckAxis.GetCurrentInsertionPosition() < c.TipHeight
		});
	}

	protected override MouthManagerDefaultMouth InitializeMouthImpl()
	{
		return MouthManagerDefaultMouth.Create(this);
	}

	protected override EyeManagerCommon InitializeEyesImpl()
	{
		return EyeManagerCommon.Create(this);
	}

	public override Part GetApproximateEyesPositionPart()
	{
		return Part_EyeBall;
	}

	public override Part GetApproximateMouthPositionPart()
	{
		return Part_InnerMouth;
	}

	public override Vector2 GetCockDirection()
	{
		return Vector2.up;
	}

	protected override void InitializeManagersInherited()
	{
		FrickStaticGuiManager = RegisterManager(new FrickStaticGuiManager(this));
		List<InsertionHandler> list = new List<InsertionHandler>();
		InsertionHandler insertionHandler = new InsertionHandler(this, InsertionHandlerOrificeTypeEnum.Mouth)
		{
			MovementSoundHandler = new FuckMovementSoundHandler(() => R.Sound.Blowjob_move_All.GetRandom().Get())
		};
		MouthFuckAxis = new BlowjobCockInsertionFuckAxis(this);
		insertionHandler.AddFuckAxis(MouthFuckAxis);
		list.Add(insertionHandler);
		MouthInsertionHandler = insertionHandler;
		InsertionManager = RegisterManager(new InsertionManager(list));
		_cockFuckLimbUsage = new CockLimbUsage<Live2DBlowjobController>(this, null, new CockLimbUsage<Live2DBlowjobController>.CockLimbUsageTarget(MouthInsertionHandler, MouthFuckAxis));
		CockManager = RegisterManager(new CockManagerCommon<Live2DBlowjobController>(this, _cockFuckLimbUsage)
		{
			AlwaysEnableCockAtStart = true,
			AllowHidingCock = false,
			AllowFlaccidCumming = true,
			AutoFuckImplemented = false,
			AutoPulloutImplemented = false,
			ToyUpdateOverride = (CockManagerCommon<Live2DBlowjobController> x) =>
			{
				IToy currentToy = SingletonMonoBehaviour<ToySingleton>.Instance.CurrentToy;
				if (currentToy != null && currentToy.Active)
				{
					currentToy.Update();
					float insertion = currentToy.Insertion;
					if (x.IsCockLimbInside())
					{
						ParamMCBodyY.Value = insertion;
					}
					if (currentToy.CumButtonClicked)
					{
						CockManager.AddCumBarValue(0.4f * Time.deltaTime);
					}
				}
			}
		});
	}

	protected override void InitializeInherited()
	{
		PrepareHeadInteractables(this);
		AddInteractable(new List<string> { "MClegL", "MClegR", "MCBody" }, new Live2DInteractable
		{
			OnHoldStart = (CubismDrawable d) =>
			{
			},
			OnHeld = (Vector2 mouseMove) =>
			{
				if (CockManager.TryGetCockLimbUsage(out var o) && o.TryGetActiveData(out var o2))
				{
					o2.ThrustMovementAccumulator += mouseMove.y * 10f;
					o2.SidewaysMovementAccumulator -= mouseMove.x * 40f;
				}
			},
			OnHeldFixed = () =>
			{
			},
			OnHoldEnd = () =>
			{
			},
			InfoSls = LOC.UI.TupleReferences.Interactable_ThrustHips
		});
		Live2DController.PrepareCommonMouthInteractables(this, out var openMouthInteractable, out var closeMouthInteractables);
		Func<Live2DBlowjobController, bool> oldOpenActiveFunc = openMouthInteractable.ActiveFunc;
		openMouthInteractable.ActiveFunc = (Live2DBlowjobController c) => oldOpenActiveFunc(c) && !CockManager.IsCockLimbInside();
		Func<Live2DBlowjobController, bool> oldCloseActiveFunc = closeMouthInteractables.ActiveFunc;
		closeMouthInteractables.ActiveFunc = (Live2DBlowjobController c) => oldCloseActiveFunc(c) && !CockManager.IsCockLimbInside();
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

	public override void InitializeGenerated()
	{
		ParamEnableThumbMouth = GetParamToDirectlyControl("ParamEnableThumbMouth");
		ParamThumbMouthX = GetParamToDirectlyControl("ParamThumbMouthX");
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
		ParamCumInMouth = GetParamToDirectlyControl("ParamCumInMouth");
		ParamHideCumShot = GetParamToDirectlyControl("ParamHideCumShot");
		ParamCumShotAnim = GetParamToDirectlyControl("ParamCumShotAnim");
		ParamFlacidity = GetParamToDirectlyControl("ParamFlacidity");
		ParamDickAngleY = GetParamToDirectlyControl("ParamDickAngleY");
		ParamHeadMovementY = GetParamToDirectlyControl("ParamHeadMovementY");
		ParamCumOutsideY = GetParamToDirectlyControl("ParamCumOutsideY");
		ParamBodyY = GetParamToDirectlyControl("ParamBodyY");
		ParamBodyX = GetParamToDirectlyControl("ParamBodyX");
		ParamCockAngleZ = GetParamToDirectlyControl("ParamCockAngleZ");
		ParamForceMouthOpen = GetParamToDirectlyControl("ParamForceMouthOpen");
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
		ParamSalivaPhysics = GetParamToDirectlyControl("ParamSalivaPhysics");
		ParamPupilWiggle = GetParamToDirectlyControl("ParamPupilWiggle");
		ParamEarsWiggle = GetParamToDirectlyControl("ParamEarsWiggle");
		ParamHeadpatX = GetParamToDirectlyControl("ParamHeadpatX");
		ParamHeadpatY = GetParamToDirectlyControl("ParamHeadpatY");
		ParamHeadpat = GetParamToDirectlyControl("ParamHeadpat");
		ParamBrowLY = GetParamToDirectlyControl("ParamBrowLY");
		ParamBrowRY = GetParamToDirectlyControl("ParamBrowRY");
		ParamMouthForm = GetParamToDirectlyControl("ParamMouthForm");
		ParamFlattenNipples = GetParamToDirectlyControl("ParamFlattenNipples");
		ParamMCBodyY = GetParamToDirectlyControl("ParamMCBodyY");
		ParamLegsSwitch = GetParamToDirectlyControl("ParamLegsSwitch");
		ParamHairPhysicsBaseToShort = GetParamToDirectlyControl("ParamHairPhysicsBaseToShort");
		ParamHairPhysicsShortToMid = GetParamToDirectlyControl("ParamHairPhysicsShortToMid");
		ParamHairPhysicsMidToLong = GetParamToDirectlyControl("ParamHairPhysicsMidToLong");
		ParamBoobSize = GetParamToDirectlyControl("ParamBoobSize");
		SymParam_LR_ParamEyeShock = new Live2DParameter[2] { ParamEyeLShock, ParamEyeRShock };
		SymParam_LR_ParamBrowRot = new Live2DParameter[2] { ParamBrowLRot, ParamBrowRRot };
		SymParam_LR_ParamBrowEmote = new Live2DParameter[2] { ParamBrowLEmote, ParamBrowREmote };
		SymParam_LR_ParamEyeballX = new Live2DParameter[2] { ParamEyeballLX, ParamEyeballRX };
		SymParam_LR_ParamEyeballY = new Live2DParameter[2] { ParamEyeballLY, ParamEyeballRY };
		SymParam_LR_ParamEar = new Live2DParameter[2] { ParamEarL, ParamEarR };
		SymParam_LR_ParamBrowY = new Live2DParameter[2] { ParamBrowLY, ParamBrowRY };
		CubismPhysicsController component = Model.GetComponent<CubismPhysicsController>();
		PhysicsSubRig_Hair = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Hair"));
		PhysicsSubRig_Tail = new Live2DPhysicsOverride(component.Rig.SubRigs.First((CubismPhysicsSubRig sr) => sr.Name == "Tail"));
		HitAreaHeadpat = GetCachedPart("HitAreaHeadpat");
		HitAreaOpenMouth = GetCachedPart("HitAreaOpenMouth");
		HitAreaCloseMouth = GetCachedPart("HitAreaCloseMouth");
		HitAreaFaceStroke = GetCachedPart("HitAreaFaceStroke");
		HitAreaThumbMouth = GetCachedPart("HitAreaThumbMouth");
		Part_BellChokerBell = GetCachedPart("BellChokerBell");
		Part_ModdableHairBack = GetCachedPart("ModdableHairBack");
		Part_ModdableFace = GetCachedPart("ModdableFace");
		Part_ModdableBack = GetCachedPart("ModdableBack");
		Part_BellChokerMain = GetCachedPart("BellChokerMain");
		Part_PointyEarR = GetCachedPart("PointyEarR");
		Part_PointyEarL = GetCachedPart("PointyEarL");
		Part_AltLongHoodieString = GetCachedPart("AltLongHoodieString");
		Part_LongHoodieHoodFront = GetCachedPart("LongHoodieHoodFront");
		Part_LongHoodieHoodBack = GetCachedPart("LongHoodieHoodBack");
		Part_LongHoodieChest = GetCachedPart("LongHoodieChest");
		Part_LongHoodieFront = GetCachedPart("LongHoodieFront");
		Part_LongHoodieShoulderR = GetCachedPart("LongHoodieShoulderR");
		Part_LongHoodieShoulderL = GetCachedPart("LongHoodieShoulderL");
		Part_CumDick1 = GetCachedPart("CumDick1");
		Part_CumDick2 = GetCachedPart("CumDick2");
		Part_DickSkin = GetCachedPart("DickSkin");
		Part_DickMain = GetCachedPart("DickMain");
		Part_MCBody = GetCachedPart("MCBody");
		Part_AttachArmRHandR = GetCachedPart("AttachArmRHandR");
		Part_AttachArmL = GetCachedPart("AttachArmL");
		Part_MClegL = GetCachedPart("MClegL");
		Part_MClegR = GetCachedPart("MClegR");
		Part_AttachArmLHandBack = GetCachedPart("AttachArmLHandBack");
		Part_AttachArmRHandBackR = GetCachedPart("AttachArmRHandBackR");
		Part_PlainShirt_Front = GetCachedPart("PlainShirt_Front");
		Part_PlainShirt_FrontNoBras = GetCachedPart("PlainShirt_FrontNoBras");
		Part_PlainShirt_Base = GetCachedPart("PlainShirt_Base");
		Part_PlainShirt_SleeveL = GetCachedPart("PlainShirt_SleeveL");
		Part_PlainShirt_SleeveR = GetCachedPart("PlainShirt_SleeveR");
		Part_PlainShirt_Back = GetCachedPart("PlainShirt_Back");
		Part_BraFrontR = GetCachedPart("BraFrontR");
		Part_BraFrontL = GetCachedPart("BraFrontL");
		Part_BrasUpperL = GetCachedPart("BrasUpperL");
		Part_BrasUpperR = GetCachedPart("BrasUpperR");
		Part_BraMid = GetCachedPart("BraMid");
		Part_BraLower = GetCachedPart("BraLower");
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
		Part_barcode = GetCachedPart("barcode");
		Part_NippleR = GetCachedPart("NippleR");
		Part_NippleL = GetCachedPart("NippleL");
		Part_SkinBreastL = GetCachedPart("SkinBreastL");
		Part_SkinBreastR = GetCachedPart("SkinBreastR");
		Part_SkinChest = GetCachedPart("SkinChest");
		Part_SkinBodyFront = GetCachedPart("SkinBodyFront");
		Part_SkinNeck = GetCachedPart("SkinNeck");
		Part_SkinShoulderL = GetCachedPart("SkinShoulderL");
		Part_SkinShoulderR = GetCachedPart("SkinShoulderR");
		Part_SkinArmL = GetCachedPart("SkinArmL");
		Part_SkinArmR = GetCachedPart("SkinArmR");
		Part_SkinBodyBack = GetCachedPart("SkinBodyBack");
		Part_CumOutsideFaceRight = GetCachedPart("CumOutsideFaceRight");
		Part_CumOutsideFaceNose = GetCachedPart("CumOutsideFaceNose");
		Part_CumOutsideFaceLeft = GetCachedPart("CumOutsideFaceLeft");
		Part_CockMask = GetCachedPart("CockMask");
		Part_ModdableShirtLeftSleeveLogo = GetCachedPart("ModdableShirtLeftSleeveLogo");
		Part_ModdableShirtRightSleeveLogo = GetCachedPart("ModdableShirtRightSleeveLogo");
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
		Part_Dress2Chest = GetCachedPart("Dress2Chest");
		Part_Dress2AltRib = GetCachedPart("Dress2AltRib");
		Part_Dress2AltBack = GetCachedPart("Dress2AltBack");
		Part_Dress2SleeveR = GetCachedPart("Dress2SleeveR");
		Part_Dress2SleeveL = GetCachedPart("Dress2SleeveL");
		Part_DressSleeveMask = GetCachedPart("DressSleeveMask");
		Part_Dress1_Strings = GetCachedPart("Dress1_Strings");
		Part_Dress1_Chest = GetCachedPart("Dress1_Chest");
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
		Part_CumMouth1 = GetCachedPart("CumMouth1");
		Part_CumMouth2 = GetCachedPart("CumMouth2");
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
		Part_ModdableHairFront = GetCachedPart("ModdableHairFront");
		Part_Headband = GetCachedPart("Headband");
		Part_CuteBow = GetCachedPart("CuteBow");
		Part_WizardHat = GetCachedPart("WizardHat");
		Part_H0_FrontClippedUp_Bang = GetCachedPart("H0_FrontClippedUp_Bang");
		Part_H0_FrontClippedUp_HairClip = GetCachedPart("H0_FrontClippedUp_HairClip");
		Part_H0_Front_Bang = GetCachedPart("H0_Front_Bang");
		Part_H0_Side_BangL = GetCachedPart("H0_Side_BangL");
		Part_H0_Side_BangR = GetCachedPart("H0_Side_BangR");
		Part_H0_SideAlt_Strand = GetCachedPart("H0_SideAlt_Strand");
		Part_H2_Front_Bang = GetCachedPart("H2_Front_Bang");
		Part_H3_Front = GetCachedPart("H3_Front");
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
		coloringGroups[ColoringGroupEnum.Skirt] = new ColoringGroup(ColoringGroupEnum.Skirt, new List<Part>());
		coloringGroups[ColoringGroupEnum.Shirt] = new ColoringGroup(ColoringGroupEnum.Shirt, new List<Part> { Part_PlainShirt_Front, Part_PlainShirt_FrontNoBras, Part_PlainShirt_Base, Part_PlainShirt_SleeveL, Part_PlainShirt_SleeveR, Part_PlainShirt_Back });
		coloringGroups[ColoringGroupEnum.ShirtShadow] = new ColoringGroup(ColoringGroupEnum.ShirtShadow, new List<Part>());
		coloringGroups[ColoringGroupEnum.StockingL] = new ColoringGroup(ColoringGroupEnum.StockingL, new List<Part>());
		coloringGroups[ColoringGroupEnum.StockingR] = new ColoringGroup(ColoringGroupEnum.StockingR, new List<Part>());
		coloringGroups[ColoringGroupEnum.Panties] = new ColoringGroup(ColoringGroupEnum.Panties, new List<Part>());
		coloringGroups[ColoringGroupEnum.Bra] = new ColoringGroup(ColoringGroupEnum.Bra, new List<Part> { Part_BraFrontR, Part_BraFrontL, Part_BrasUpperL, Part_BrasUpperR, Part_BraMid, Part_BraLower });
		coloringGroups[ColoringGroupEnum.Hoodie] = new ColoringGroup(ColoringGroupEnum.Hoodie, new List<Part> { Part_LongHoodieHoodFront, Part_LongHoodieHoodBack, Part_LongHoodieChest, Part_LongHoodieFront, Part_LongHoodieShoulderR, Part_LongHoodieShoulderL });
		coloringGroups[ColoringGroupEnum.HoodieStrings] = new ColoringGroup(ColoringGroupEnum.HoodieStrings, new List<Part> { Part_AltLongHoodieString });
		coloringGroups[ColoringGroupEnum.Pants] = new ColoringGroup(ColoringGroupEnum.Pants, new List<Part>());
		coloringGroups[ColoringGroupEnum.PantsBelt] = new ColoringGroup(ColoringGroupEnum.PantsBelt, new List<Part>());
		coloringGroups[ColoringGroupEnum.Dress1] = new ColoringGroup(ColoringGroupEnum.Dress1, new List<Part> { Part_Dress1_Strings, Part_Dress1_Chest });
		coloringGroups[ColoringGroupEnum.Dress2Alt] = new ColoringGroup(ColoringGroupEnum.Dress2Alt, new List<Part> { Part_Dress2AltRib, Part_Dress2AltBack });
		coloringGroups[ColoringGroupEnum.Dress2] = new ColoringGroup(ColoringGroupEnum.Dress2, new List<Part> { Part_Dress2Chest, Part_Dress2SleeveR, Part_Dress2SleeveL });
		coloringGroups[ColoringGroupEnum.ShoeR] = new ColoringGroup(ColoringGroupEnum.ShoeR, new List<Part>());
		coloringGroups[ColoringGroupEnum.ShoeL] = new ColoringGroup(ColoringGroupEnum.ShoeL, new List<Part>());
		coloringGroups[ColoringGroupEnum.PointyEars] = new ColoringGroup(ColoringGroupEnum.PointyEars, new List<Part> { Part_PointyEarR, Part_PointyEarL });
		coloringGroups[ColoringGroupEnum.WizardHat] = new ColoringGroup(ColoringGroupEnum.WizardHat, new List<Part> { Part_WizardHat });
		coloringGroups[ColoringGroupEnum.BellChoker] = new ColoringGroup(ColoringGroupEnum.BellChoker, new List<Part> { Part_BellChokerBell, Part_BellChokerMain });
		coloringGroups[ColoringGroupEnum.CuteBow] = new ColoringGroup(ColoringGroupEnum.CuteBow, new List<Part> { Part_CuteBow });
		coloringGroups[ColoringGroupEnum.PumpkinHairPin] = new ColoringGroup(ColoringGroupEnum.PumpkinHairPin, new List<Part>());
		coloringGroups[ColoringGroupEnum.Headband] = new ColoringGroup(ColoringGroupEnum.Headband, new List<Part> { Part_Headband });
		coloringGroups[ColoringGroupEnum.NipplePiercingL] = new ColoringGroup(ColoringGroupEnum.NipplePiercingL, new List<Part>());
		coloringGroups[ColoringGroupEnum.NipplePiercingR] = new ColoringGroup(ColoringGroupEnum.NipplePiercingR, new List<Part>());
		coloringGroups[ColoringGroupEnum.NavelPiercing] = new ColoringGroup(ColoringGroupEnum.NavelPiercing, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniTop_Main] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Main, new List<Part> { Part_BikiniTop_M_CupR, Part_BikiniTop_M_CupL });
		coloringGroups[ColoringGroupEnum.BikiniTop_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniTop_Alt, new List<Part> { Part_BikiniTop_A_StrapR, Part_BikiniTop_A_OutlineR, Part_BikiniTop_A_StrapL, Part_BikiniTop_A_OutlineL, Part_BikiniTop_A_Middle });
		coloringGroups[ColoringGroupEnum.BikiniBottom_Main] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Main, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniBottom_Alt] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Alt, new List<Part>());
		coloringGroups[ColoringGroupEnum.BikiniBottom_Uncolored] = new ColoringGroup(ColoringGroupEnum.BikiniBottom_Uncolored, new List<Part>());
		coloringGroups[ColoringGroupEnum.IrisL] = new ColoringGroup(ColoringGroupEnum.IrisL, new List<Part> { Part_PupilL });
		coloringGroups[ColoringGroupEnum.IrisR] = new ColoringGroup(ColoringGroupEnum.IrisR, new List<Part> { Part_PupilR });
		coloringGroups[ColoringGroupEnum.PupilL] = new ColoringGroup(ColoringGroupEnum.PupilL, new List<Part> { Part_Iris1L });
		coloringGroups[ColoringGroupEnum.PupilR] = new ColoringGroup(ColoringGroupEnum.PupilR, new List<Part> { Part_Iris1R });
		coloringGroups[ColoringGroupEnum.Sclera] = new ColoringGroup(ColoringGroupEnum.Sclera, new List<Part> { Part_EyeBall });
		coloringGroups[ColoringGroupEnum.Eyelash] = new ColoringGroup(ColoringGroupEnum.Eyelash, new List<Part>());
		coloringGroups[ColoringGroupEnum.HeartPupils] = new ColoringGroup(ColoringGroupEnum.HeartPupils, new List<Part> { Part_Iris2L, Part_Iris2R });
		coloringGroups[ColoringGroupEnum.EyeBrows] = new ColoringGroup(ColoringGroupEnum.EyeBrows, new List<Part> { Part_BrowL, Part_BrowR });
		coloringGroups[ColoringGroupEnum.Nipples] = new ColoringGroup(ColoringGroupEnum.Nipples, new List<Part> { Part_NippleR, Part_NippleL });
		coloringGroups[ColoringGroupEnum.Vagina] = new ColoringGroup(ColoringGroupEnum.Vagina, new List<Part>());
		coloringGroups[ColoringGroupEnum.Xray] = new ColoringGroup(ColoringGroupEnum.Xray, new List<Part>());
		coloringGroups[ColoringGroupEnum.Anus] = new ColoringGroup(ColoringGroupEnum.Anus, new List<Part>());
		coloringGroups[ColoringGroupEnum.Teeth] = new ColoringGroup(ColoringGroupEnum.Teeth, new List<Part> { Part_TeethUpper, Part_TeethLower });
		coloringGroups[ColoringGroupEnum.Mouth] = new ColoringGroup(ColoringGroupEnum.Mouth, new List<Part> { Part_Tounge, Part_InnerMouthTounge, Part_InnerMouth });
		coloringGroups[ColoringGroupEnum.Boobs] = new ColoringGroup(ColoringGroupEnum.Boobs, new List<Part> { Part_SkinBreastL, Part_SkinBreastR });
		coloringGroups[ColoringGroupEnum.Ass] = new ColoringGroup(ColoringGroupEnum.Ass, new List<Part>());
		coloringGroups[ColoringGroupEnum.Pelvic] = new ColoringGroup(ColoringGroupEnum.Pelvic, new List<Part>());
		coloringGroups[ColoringGroupEnum.Thigh] = new ColoringGroup(ColoringGroupEnum.Thigh, new List<Part>());
		coloringGroups[ColoringGroupEnum.Tummy] = new ColoringGroup(ColoringGroupEnum.Tummy, new List<Part>());
		coloringGroups[ColoringGroupEnum.Skin] = new ColoringGroup(ColoringGroupEnum.Skin, new List<Part>
		{
			Part_SkinNose, Part_SkinFace, Part_SkinEyelidClosedL, Part_SkinEyelidClosedR, Part_SkinEyelidL, Part_SkinEyelidR, Part_SkinEyelid2L, Part_SkinEyelid2R, Part_SkinChest, Part_SkinBodyFront,
			Part_SkinNeck, Part_SkinShoulderL, Part_SkinShoulderR, Part_SkinArmL, Part_SkinArmR, Part_SkinBodyBack, Part_SkinMouthThumbSuck, Part_SkinLipUpper, Part_SkinLipLower, Part_SkinLipUpperToungeOut,
			Part_SkinLipLowerToungeOut
		});
		coloringGroups[ColoringGroupEnum.Blush] = new ColoringGroup(ColoringGroupEnum.Blush, new List<Part> { Part_blush_2, Part_blush });
		coloringGroups[ColoringGroupEnum.SharpTeeth] = new ColoringGroup(ColoringGroupEnum.SharpTeeth, new List<Part> { Part_SharpTeethUpper, Part_SharpTeethLower });
		coloringGroups[ColoringGroupEnum.CatEarMain] = new ColoringGroup(ColoringGroupEnum.CatEarMain, new List<Part> { Part_CatearFrontR, Part_CatearFrontL, Part_CatearBackR, Part_CatearBackL });
		coloringGroups[ColoringGroupEnum.CatEarSecondary] = new ColoringGroup(ColoringGroupEnum.CatEarSecondary, new List<Part> { Part_CatearMidL, Part_CatearMidR });
		coloringGroups[ColoringGroupEnum.LegLFoot] = new ColoringGroup(ColoringGroupEnum.LegLFoot, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegL] = new ColoringGroup(ColoringGroupEnum.LegL, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegRFoot] = new ColoringGroup(ColoringGroupEnum.LegRFoot, new List<Part>());
		coloringGroups[ColoringGroupEnum.LegR] = new ColoringGroup(ColoringGroupEnum.LegR, new List<Part>());
		coloringGroups[ColoringGroupEnum.ArmL] = new ColoringGroup(ColoringGroupEnum.ArmL, new List<Part> { Part_AttachArmL, Part_AttachArmLHandBack });
		coloringGroups[ColoringGroupEnum.ArmR] = new ColoringGroup(ColoringGroupEnum.ArmR, new List<Part> { Part_AttachArmRHandR, Part_AttachArmRHandBackR });
		coloringGroups[ColoringGroupEnum.Tail] = new ColoringGroup(ColoringGroupEnum.Tail, new List<Part> { Part_TailMain10, Part_TailMain9, Part_TailMain8, Part_TailMain7, Part_TailMain6, Part_TailMain5, Part_TailMain4, Part_TailMain3, Part_TailMain2, Part_TailMain });
		coloringGroups[ColoringGroupEnum.TailPlug] = new ColoringGroup(ColoringGroupEnum.TailPlug, new List<Part>());
		coloringGroups[ColoringGroupEnum.Hologram] = new ColoringGroup(ColoringGroupEnum.Hologram, new List<Part> { Part_HairHologram });
		coloringGroups[ColoringGroupEnum.OtherAttachments] = new ColoringGroup(ColoringGroupEnum.OtherAttachments, new List<Part>());
		coloringGroups[ColoringGroupEnum.MCSkin] = new ColoringGroup(ColoringGroupEnum.MCSkin, new List<Part> { Part_MCBody, Part_MClegL, Part_MClegR, Part_headpet_1, Part_headpet_2, Part_headpet_3 });
		coloringGroups[ColoringGroupEnum.MCCockSkin] = new ColoringGroup(ColoringGroupEnum.MCCockSkin, new List<Part> { Part_DickSkin });
		coloringGroups[ColoringGroupEnum.MCCockTip] = new ColoringGroup(ColoringGroupEnum.MCCockTip, new List<Part> { Part_DickMain });
		coloringGroups[ColoringGroupEnum.Shadow] = new ColoringGroup(ColoringGroupEnum.Shadow, new List<Part>());
		coloringGroups[ColoringGroupEnum.Highlight] = new ColoringGroup(ColoringGroupEnum.Highlight, new List<Part> { Part_highlight });
		coloringGroups[ColoringGroupEnum.Mask] = new ColoringGroup(ColoringGroupEnum.Mask, new List<Part> { Part_CockMask, Part_DressSleeveMask, Part_ThumbMask });
		coloringGroups[ColoringGroupEnum.Moddable] = new ColoringGroup(ColoringGroupEnum.Moddable, new List<Part> { Part_ModdableHairBack, Part_ModdableFace, Part_ModdableBack, Part_ModdableShirtLeftSleeveLogo, Part_ModdableShirtRightSleeveLogo, Part_ModdableHairFront });
		coloringGroups[ColoringGroupEnum.HitArea] = new ColoringGroup(ColoringGroupEnum.HitArea, new List<Part> { HitAreaHeadpat, HitAreaOpenMouth, HitAreaCloseMouth, HitAreaFaceStroke, HitAreaThumbMouth });
		coloringGroups[ColoringGroupEnum.Cum] = new ColoringGroup(ColoringGroupEnum.Cum, new List<Part> { Part_CumDick1, Part_CumDick2, Part_CumOutsideFaceRight, Part_CumOutsideFaceNose, Part_CumOutsideFaceLeft, Part_CumMouth1, Part_CumMouth2 });
		coloringGroups[ColoringGroupEnum.Rest] = new ColoringGroup(ColoringGroupEnum.Rest, new List<Part> { Part_lines, Part_barcode, Part_ThumbSuckThumb, Part_ThumbSuck, Part_Saliva2, Part_Saliva1 });
	}
}
