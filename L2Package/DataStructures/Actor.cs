using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class AActor : UObject
    {
        public UObject UObject
        {
            get
            {
                UObject obj = new UObject();
                obj.Flags = this.Flags;
                obj.Name = this.Name;
                return obj;
            }
        }

        public AActor() : base()
        {
            Location = new UVector();
            Rotation = new URotator();
            DrawScale3D = new UVector();
            PrePivot = new UVector();
            StaticMesh = 0;
        }
        //core properties
        [UEExport]
        public bool bBlockActors { set; get; }
        [UEExport]
        public bool bBlockPlayers { set; get; }
        [UEExport]
        public bool bCollideActors { set; get; }
        [UEExport]
        public bool bDeleteMe { set; get; }
        [UEExport]
        public bool bHidden { set; get; }
        [UEExport]
        public float DrawScale { set; get; }
        [UEExport]
        public UVector Location { get; set; }
        [UEExport]
        public URotator Rotation { get; set; }
        [UEExport]
        public UVector DrawScale3D { get; set; }
        [UEExport]
        public UVector PrePivot { get; set; }
        [UEExport]
        public int StaticMesh { get; set; }

        //ancilary properties
        [UEExport]
        public bool bDynamicActorFilterState { set; get; }
        [UEExport]
        public bool bLightChanged { set; get; }
        [UEExport]
        public bool bUpdateShadow { set; get; }
        [UEExport]
        public bool bHideShadow { set; get; }
        [UEExport]
        public bool bHideRightHandMesh { set; get; }
        [UEExport]
        public bool bHideLeftHandMesh { set; get; }
        [UEExport]
        public bool bNeedCleanup { set; get; }
        [UEExport]
        public bool bShadowOnly { set; get; }
        [UEExport]
        public int CreatureID { set; get; }
        [UEExport]
        public bool NoCheatCollision { set; get; }
        [UEExport]
        public bool CanIngnoreCollision { set; get; }
        [UEExport]
        public bool bDeleteNow { set; get; }
        [UEExport]
        public bool bAlwaysVisible { set; get; }
        [UEExport]
        public bool bTicked { set; get; }
        [UEExport]
        public bool bTimerLoop { set; get; }
        [UEExport]
        public bool bOnlyOwnerSee { set; get; }
        [UEExport]
        public bool bOnlyDrawIfAttached { set; get; }
        [UEExport]
        public bool bTrailerAllowRotation { set; get; }
        [UEExport]
        public bool bTrailerSameRotation { set; get; }
        [UEExport]
        public bool bTrailerPrePivot { set; get; }
        [UEExport]
        public bool bTrailerNoOwnerDestroy { set; get; }
        [UEExport]
        public bool bRelativeTrail { set; get; }
        [UEExport]
        public UVector RelativeTrailOffset { set; get; }
        [UEExport]
        public bool bSelfRotation { set; get; }
        [UEExport]
        public bool bWorldGeometry { set; get; }
        [UEExport]
        public bool bAcceptsProjectors { set; get; }
        [UEExport]
        public bool bOrientOnSlope { set; get; }
        [UEExport]
        public bool bOnlyAffectPawns { set; get; }
        [UEExport]
        public bool bDisableSorting { set; get; }
        [UEExport]
        public bool bIgnoreEncroachers { set; get; }
        [UEExport]
        public bool bShowOctreeNodes { set; get; }
        [UEExport]
        public bool bWasSNFiltered { set; get; }
        [UEExport]
        public bool bNetTemporary { set; get; }
        [UEExport]
        public bool bOnlyRelevantToOwner { set; get; }
        [UEExport]
        public bool bNetDirty { set; get; }
        [UEExport]
        public bool bAlwaysRelevant { set; get; }
        [UEExport]
        public bool bReplicateInstigator { set; get; }
        [UEExport]
        public bool bReplicateMovement { set; get; }
        [UEExport]
        public bool bSkipActorPropertyReplication { set; get; }
        [UEExport]
        public bool bUpdateSimulatedPosition { set; get; }
        [UEExport]
        public bool bTearOff { set; get; }
        [UEExport]
        public bool bOnlyDirtyReplication { set; get; }
        [UEExport]
        public bool bReplicateAnimations { set; get; }
        [UEExport]
        public bool bNetInitialRotation { set; get; }
        [UEExport]
        public bool bCompressedPosition { set; get; }
        [UEExport]
        public bool bAlwaysZeroBoneOffset { set; get; }
        [UEExport]
        public UVector RelativeLocInVehicle { set; get; }
        [UEExport]
        public int VehicleID { set; get; }
        [UEExport]
        public bool bVehicleTargetMove { set; get; }
        [UEExport]
        public bool bVehicleCompensativeMove { set; get; }
        [UEExport]
        public bool bHasActorTarget { set; get; }
        [UEExport]
        public bool bL2DesiredRotated { set; get; }
        [UEExport]
        public URotator L2DesriedRotator { set; get; }
        [UEExport]
        public bool L2NeedTick { set; get; }
        [UEExport]
        public bool bCheckChangableLevel { set; get; }
        [UEExport]
        public bool bImmediatelyStop { set; get; }
        [UEExport]
        public bool bNeedPostSpawnProcess { set; get; }
        //EActorViewType L2ActorViewtype {set; get;}
        [UEExport]
        public float L2ActorViewDuration { set; get; }
        [UEExport]
        public float L2ActorViewElapsedTime { set; get; }
        [UEExport]
        public float L2LodViewDuration { set; get; }
        [UEExport]
        public float L2LodViewElapsedTime { set; get; }
        [UEExport]
        public int L2CurrentLod { set; get; }
        [UEExport]
        public int L2ServerObjectRealID { set; get; }
        [UEExport]
        public int L2ServerObjectID { set; get; }
        //EL2ObjectType L2ServerObjectType {set; get;}
        //int NetTag {set; get;}
        [UEExport]
        public float NetUpdateTime { set; get; }
        [UEExport]
        public float NetUpdateFrequency { set; get; }
        [UEExport]
        public float NetPriority { set; get; }
        //name AttachmentBone {set; get;}
        //EAttachType AttachType {set; get;}
        //LevelInfo Level {set; get;}
        //transient Level XLevel {set; get;}
        [UEExport]
        public float TimerRate { set; get; }
        //transient float LastRenderTime {set; get;}
        //transient array<int> Leaves {set; get;}
        //Inventory Inventory {set; get;}
        [UEExport]
        public float TimerCounter { set; get; }
        //transient MeshInstance MeshInstance {set; get;}
        //ETargetSpineStatus TargetSpineStatus {set; get;}
        [UEExport]
        public List<AActor> Child { set; get; }
        [UEExport]
        public List<AActor> Touching { set; get; }
        [UEExport]
        public List<int> OctreeNodes { set; get; }
        [UEExport]
        public Box OctreeBox { set; get; }
        [UEExport]
        public UVector OctreeBoxCenter { set; get; }
        [UEExport]
        public UVector OctreeBoxRadii { set; get; }
        [UEExport]
        public AActor Deleted { set; get; }
        [UEExport]
        public float LatentFloat { set; get; }
        [UEExport]
        public int CollisionTag { set; get; }
        [UEExport]
        public int JoinedTag { set; get; }
        [UEExport]
        public UVector Velocity { set; get; }
        [UEExport]
        public UVector Acceleration { set; get; }
        [UEExport]
        public int AttachTag { set; get; }
        [UEExport]
        public List<AActor> Attached { set; get; }
        [UEExport]
        public UVector RelativeLocation { set; get; }
        [UEExport]
        public URotator RelativeRotation { set; get; }
        [UEExport]
        public bool bHardAttach { set; get; }
        //Material Texture {set; get;}
        [UEExport]
        public int StaticMeshInstance { set; get; }
        [UEExport]
        public UModel Brush { set; get; }      
        [UEExport]
        public float OverlayTimer { set; get; }
        [UEExport]
        public UColor OverlayColor { set; get; }
        [UEExport]
        public byte AmbientGlow { set; get; }
        [UEExport]
        public byte MaxLights { set; get; }
        [UEExport]
        public float CullDistance { set; get; }
        [UEExport]
        public float ScaleGlow { set; get; }
        [UEExport]
        public URotator SwayRotationOrig { set; get; }
        [UEExport]
        public bool bDontBatch { set; get; }
        [UEExport]
        public bool bUnlit { set; get; }
        [UEExport]
        public bool bShadowCast { set; get; }
        [UEExport]
        public bool bStaticLighting { set; get; }
        [UEExport]
        public bool bUseLightingFromBase { set; get; }
        [UEExport]
        public bool bIgnoredRange { set; get; }
        [UEExport]
        public bool bUnlitCheck { set; get; }
        [UEExport]
        public bool bCulledSunlight { set; get; }
        [UEExport]
        public bool bHurtEntry { set; get; }
        [UEExport]
        public bool bGameRelevant { set; get; }
        [UEExport]
        public bool bCollideWhenPlacing { set; get; }
        [UEExport]
        public bool bTravel { set; get; }
        [UEExport]
        public bool bMovable { set; get; }
        [UEExport]
        public bool bDestroyInPainVolume { set; get; }
        [UEExport]
        public bool bShouldBaseAtStartup { set; get; }
        [UEExport]
        public bool bPendingDelete { set; get; }
        [UEExport]
        public bool bAnimByOwner { set; get; }
        [UEExport]
        public bool bOwnerNoSee { set; get; }
        [UEExport]
        public bool bCanTeleport { set; get; }
        [UEExport]
        public bool bClientAnim { set; get; }
        [UEExport]
        public bool bDisturbFluidSurface { set; get; }
        [UEExport]
        public bool bAlwaysTick { set; get; }
        [UEExport]
        public float TransientSoundVolume { set; get; }
        [UEExport]
        public float TransientSoundRadius { set; get; }
        [UEExport]
        public float CollisionRadius { set; get; }
        [UEExport]
        public float CollisionHeight { set; get; }
        [UEExport]
        public bool bCollideWorld { set; get; }
        [UEExport]
        public bool bProjTarget { set; get; }
        [UEExport]
        public bool bBlockZeroExtentTraces { set; get; }
        [UEExport]
        public bool bBlockNonZeroExtentTraces { set; get; }
        [UEExport]
        public bool bAutoAlignToTerrain { set; get; }
        [UEExport]
        public bool bUseCylinderCollision { set; get; }
        [UEExport]
        public bool bBlockKarma { set; get; }
        [UEExport]
        public bool bNetNotify { set; get; }
        [UEExport]
        public bool bIgnoreOutOfWorld { set; get; }
        [UEExport]
        public bool bBounce { set; get; }
        [UEExport]
        public bool bFixedRotationDir { set; get; }
        [UEExport]
        public bool bRotateToDesired { set; get; }
        [UEExport]
        public bool bInterpolating { set; get; }
        [UEExport]
        public bool bJustTeleported { set; get; }
        [UEExport]
        public float Mass { set; get; }
        [UEExport]
        public float Buoyancy { set; get; }
        [UEExport]
        public URotator RotationRate { set; get; }
        [UEExport]
        public URotator KayboardRotationRate { set; get; }
        [UEExport]
        public int KeyboardRotationYawFromServer { set; get; }
        [UEExport]
        public URotator RotationLimit { set; get; }
        [UEExport]
        public URotator DesiredRotation { set; get; }
        [UEExport]
        public AActor PendingTouch { set; get; }
        [UEExport]
        public UVector ColLocation { set; get; }
        //EForceType ForceType {set; get;}
        //float ForceRadius {set; get;}
        //float ForceScale {set; get;}
        [UEExport]
        public bool bNetInitial { set; get; }
        [UEExport]
        public bool bNetOwner { set; get; }
        [UEExport]
        public bool bNetRelevant { set; get; }
        [UEExport]
        public bool bDemoRecording { set; get; }
        [UEExport]
        public bool bClientDemoRecording { set; get; }
        [UEExport]
        public bool bClientDemoNetFunc { set; get; }
        [UEExport]
        public bool bNoRepMesh { set; get; }
        [UEExport]
        public bool bHiddenEd { set; get; }
        [UEExport]
        public bool bHiddenEdGroup { set; get; }
        [UEExport]
        public bool bDirectional { set; get; }
        [UEExport]
        public bool bSelected { set; get; }
        [UEExport]
        public bool bEdShouldSnap { set; get; }
        [UEExport]
        public bool bEdSnap { set; get; }
        [UEExport]
        public bool bTempEditor { set; get; }
        [UEExport]
        public bool bObsolete { set; get; }
        [UEExport]
        public bool bPathColliding { set; get; }
        [UEExport]
        public bool bPathTemp { set; get; }
        [UEExport]
        public bool bScriptInitialized { set; get; }
        [UEExport]
        public bool bLockLocation { set; get; }
        [UEExport]
        public bool bLockUndelete { set; get; }
        [UEExport]
        public TextureModifyinfo TexModifyInfo { set; get; }
        //EActorEffectType L2ActorEffecttype {set; get;}
        [UEExport]
        public bool bTangentLoad { set; get; }
        [UEExport]
        public int nUseNormalmap { set; get; }
    }
}
