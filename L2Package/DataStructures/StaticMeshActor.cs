using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class StaticMeshActor : AActor
    {
        public bool bAgitDefaultStaticMesh { set; get; }
        public bool bExactProjectileCollision { set; get; }
        public bool bTimeReactor { set; get; }
        public float HideTime { set; get; }
        public float ShowTime { set; get; }
        public int AccessoryIndex { set; get; }
        public int AgitId { set; get; }
        public int AgitStatus { set; get; }
        public int CurrAccessoryType { set; get; }

        public StaticMeshActor()
        {
            bExactProjectileCollision = true;
            //bStatic = True;
            bWorldGeometry = true;
            bShadowCast = true;
            bStaticLighting = true;
            CollisionRadius = 1.00f;
            CollisionHeight = 1.00f;
            bCollideActors = true;
            bBlockActors = true;
            bBlockPlayers = true;
            bBlockKarma = true;
            bEdShouldSnap = true;
        }

        public StaticMeshActor(AActor Actr)
        {
            bBlockActors = Actr.bBlockActors;
            bBlockPlayers = Actr.bBlockPlayers;
            bCollideActors = Actr.bCollideActors;
            bDeleteMe = Actr.bDeleteMe;
            bHidden = Actr.bHidden;
            DrawScale = Actr.DrawScale;
            Location = Actr.Location;
            Rotation = Actr.Rotation;
            DrawScale3D = Actr.DrawScale3D;
            PrePivot = Actr.PrePivot;
            StaticMesh = Actr.StaticMesh;
            bDynamicActorFilterState = Actr.bDynamicActorFilterState;
            bLightChanged = Actr.bLightChanged;
            bUpdateShadow = Actr.bUpdateShadow;
            bHideShadow = Actr.bHideShadow;
            bHideRightHandMesh = Actr.bHideRightHandMesh;
            bHideLeftHandMesh = Actr.bHideLeftHandMesh;
            bNeedCleanup = Actr.bNeedCleanup;
            bShadowOnly = Actr.bShadowOnly;
            CreatureID = Actr.CreatureID;
            NoCheatCollision = Actr.NoCheatCollision;
            CanIngnoreCollision = Actr.CanIngnoreCollision;
            bDeleteNow = Actr.bDeleteNow;
            bAlwaysVisible = Actr.bAlwaysVisible;
            bTicked = Actr.bTicked;
            bTimerLoop = Actr.bTimerLoop;
            bOnlyOwnerSee = Actr.bOnlyOwnerSee;
            bOnlyDrawIfAttached = Actr.bOnlyDrawIfAttached;
            bTrailerAllowRotation = Actr.bTrailerAllowRotation;
            bTrailerSameRotation = Actr.bTrailerSameRotation;
            bTrailerPrePivot = Actr.bTrailerPrePivot;
            bTrailerNoOwnerDestroy = Actr.bTrailerNoOwnerDestroy;
            bRelativeTrail = Actr.bRelativeTrail;
            RelativeTrailOffset = Actr.RelativeTrailOffset;
            bSelfRotation = Actr.bSelfRotation;
            bWorldGeometry = Actr.bWorldGeometry;
            bAcceptsProjectors = Actr.bAcceptsProjectors;
            bOrientOnSlope = Actr.bOrientOnSlope;
            bOnlyAffectPawns = Actr.bOnlyAffectPawns;
            bDisableSorting = Actr.bDisableSorting;
            bIgnoreEncroachers = Actr.bIgnoreEncroachers;
            bShowOctreeNodes = Actr.bShowOctreeNodes;
            bWasSNFiltered = Actr.bWasSNFiltered;
            bNetTemporary = Actr.bNetTemporary;
            bOnlyRelevantToOwner = Actr.bOnlyRelevantToOwner;
            bNetDirty = Actr.bNetDirty;
            bAlwaysRelevant = Actr.bAlwaysRelevant;
            bReplicateInstigator = Actr.bReplicateInstigator;
            bReplicateMovement = Actr.bReplicateMovement;
            bSkipActorPropertyReplication = Actr.bSkipActorPropertyReplication;
            bUpdateSimulatedPosition = Actr.bUpdateSimulatedPosition;
            bTearOff = Actr.bTearOff;
            bOnlyDirtyReplication = Actr.bOnlyDirtyReplication;
            bReplicateAnimations = Actr.bReplicateAnimations;
            bNetInitialRotation = Actr.bNetInitialRotation;
            bCompressedPosition = Actr.bCompressedPosition;
            bAlwaysZeroBoneOffset = Actr.bAlwaysZeroBoneOffset;
            RelativeLocInVehicle = Actr.RelativeLocInVehicle;
            VehicleID = Actr.VehicleID;
            bVehicleTargetMove = Actr.bVehicleTargetMove;
            bVehicleCompensativeMove = Actr.bVehicleCompensativeMove;
            bHasActorTarget = Actr.bHasActorTarget;
            bL2DesiredRotated = Actr.bL2DesiredRotated;
            L2DesriedRotator = Actr.L2DesriedRotator;
            L2NeedTick = Actr.L2NeedTick;
            bCheckChangableLevel = Actr.bCheckChangableLevel;
            bImmediatelyStop = Actr.bImmediatelyStop;
            bNeedPostSpawnProcess = Actr.bNeedPostSpawnProcess;
            L2ActorViewDuration = Actr.L2ActorViewDuration;
            L2ActorViewElapsedTime = Actr.L2ActorViewElapsedTime;
            L2LodViewDuration = Actr.L2LodViewDuration;
            L2LodViewElapsedTime = Actr.L2LodViewElapsedTime;
            L2CurrentLod = Actr.L2CurrentLod;
            L2ServerObjectRealID = Actr.L2ServerObjectRealID;
            L2ServerObjectID = Actr.L2ServerObjectID;
            NetUpdateTime = Actr.NetUpdateTime;
            NetUpdateFrequency = Actr.NetUpdateFrequency;
            NetPriority = Actr.NetPriority;
            TimerRate = Actr.TimerRate;
            TimerCounter = Actr.TimerCounter;
            Child = Actr.Child;
            Touching = Actr.Touching;
            OctreeNodes = Actr.OctreeNodes;
            OctreeBox = Actr.OctreeBox;
            OctreeBoxCenter = Actr.OctreeBoxCenter;
            OctreeBoxRadii = Actr.OctreeBoxRadii;
             Deleted = Actr.Deleted;
            LatentFloat = Actr.LatentFloat;
            CollisionTag = Actr.CollisionTag;
            JoinedTag = Actr.JoinedTag;
            Location = Actr.Location;
            Rotation = Actr.Rotation;
            Velocity = Actr.Velocity;
            Acceleration = Actr.Acceleration;
            AttachTag = Actr.AttachTag;
            Attached = Actr.Attached;
            RelativeLocation = Actr.RelativeLocation;
            RelativeRotation = Actr.RelativeRotation;
            bHardAttach = Actr.bHardAttach;
            StaticMeshInstance = Actr.StaticMeshInstance;
            Brush = Actr.Brush;
            DrawScale = Actr.DrawScale;
            DrawScale3D = Actr.DrawScale3D;
            PrePivot = Actr.PrePivot;
            OverlayTimer = Actr.OverlayTimer;
            OverlayColor = Actr.OverlayColor;
            AmbientGlow = Actr.AmbientGlow;
            MaxLights = Actr.MaxLights;
            CullDistance = Actr.CullDistance;
            ScaleGlow = Actr.ScaleGlow;
            SwayRotationOrig = Actr.SwayRotationOrig;
            bDontBatch = Actr.bDontBatch;
            bUnlit = Actr.bUnlit;
            bShadowCast = Actr.bShadowCast;
            bStaticLighting = Actr.bStaticLighting;
            bUseLightingFromBase = Actr.bUseLightingFromBase;
            bIgnoredRange = Actr.bIgnoredRange;
            bUnlitCheck = Actr.bUnlitCheck;
            bCulledSunlight = Actr.bCulledSunlight;
            bHurtEntry = Actr.bHurtEntry;
            bGameRelevant = Actr.bGameRelevant;
            bCollideWhenPlacing = Actr.bCollideWhenPlacing;
            bTravel = Actr.bTravel;
            bMovable = Actr.bMovable;
            bDestroyInPainVolume = Actr.bDestroyInPainVolume;
            bShouldBaseAtStartup = Actr.bShouldBaseAtStartup;
            bPendingDelete = Actr.bPendingDelete;
            bAnimByOwner = Actr.bAnimByOwner;
            bOwnerNoSee = Actr.bOwnerNoSee;
            bCanTeleport = Actr.bCanTeleport;
            bClientAnim = Actr.bClientAnim;
            bDisturbFluidSurface = Actr.bDisturbFluidSurface;
            bAlwaysTick = Actr.bAlwaysTick;
            TransientSoundVolume = Actr.TransientSoundVolume;
            TransientSoundRadius = Actr.TransientSoundRadius;
            CollisionRadius = Actr.CollisionRadius;
            CollisionHeight = Actr.CollisionHeight;
            bCollideWorld = Actr.bCollideWorld;
            bProjTarget = Actr.bProjTarget;
            bBlockZeroExtentTraces = Actr.bBlockZeroExtentTraces;
            bBlockNonZeroExtentTraces = Actr.bBlockNonZeroExtentTraces;
            bAutoAlignToTerrain = Actr.bAutoAlignToTerrain;
            bUseCylinderCollision = Actr.bUseCylinderCollision;
            bBlockKarma = Actr.bBlockKarma;
            bNetNotify = Actr.bNetNotify;
            bIgnoreOutOfWorld = Actr.bIgnoreOutOfWorld;
            bBounce = Actr.bBounce;
            bFixedRotationDir = Actr.bFixedRotationDir;
            bRotateToDesired = Actr.bRotateToDesired;
            bInterpolating = Actr.bInterpolating;
            bJustTeleported = Actr.bJustTeleported;
            Mass = Actr.Mass;
            Buoyancy = Actr.Buoyancy;
            RotationRate = Actr.RotationRate;
            KayboardRotationRate = Actr.KayboardRotationRate;
            KeyboardRotationYawFromServer = Actr.KeyboardRotationYawFromServer;
            RotationLimit = Actr.RotationLimit;
            DesiredRotation = Actr.DesiredRotation;
            PendingTouch = Actr.PendingTouch;
            ColLocation = Actr.ColLocation;
            bNetInitial = Actr.bNetInitial;
            bNetOwner = Actr.bNetOwner;
            bNetRelevant = Actr.bNetRelevant;
            bDemoRecording = Actr.bDemoRecording;
            bClientDemoRecording = Actr.bClientDemoRecording;
            bClientDemoNetFunc = Actr.bClientDemoNetFunc;
            bNoRepMesh = Actr.bNoRepMesh;
            bHiddenEd = Actr.bHiddenEd;
            bHiddenEdGroup = Actr.bHiddenEdGroup;
            bDirectional = Actr.bDirectional;
            bSelected = Actr.bSelected;
            bEdShouldSnap = Actr.bEdShouldSnap;
            bEdSnap = Actr.bEdSnap;
            bTempEditor = Actr.bTempEditor;
            bObsolete = Actr.bObsolete;
            bPathColliding = Actr.bPathColliding;
            bPathTemp = Actr.bPathTemp;
            bScriptInitialized = Actr.bScriptInitialized;
            bLockLocation = Actr.bLockLocation;
            bLockUndelete = Actr.bLockUndelete;
            TexModifyInfo = Actr.TexModifyInfo;
            bTangentLoad = Actr.bTangentLoad;
            nUseNormalmap = Actr.nUseNormalmap;


        }

        public AActor Actor {
            get
            {
                AActor A = new AActor();
                A.bBlockActors = this.bBlockActors;
                A.bBlockPlayers = this.bBlockPlayers;
                A.bCollideActors = this.bCollideActors;
                A.bDeleteMe = this.bDeleteMe;
                A.bHidden = this.bHidden;
                A.DrawScale = this.DrawScale;
                A.DrawScale3D = this.DrawScale3D;
                A.Flags = this.Flags;
                A.Location = this.Location;
                A.Name = this.Name;
                A.PrePivot = this.PrePivot;
                A.Rotation = this.Rotation;
                A.StaticMesh = this.StaticMesh;
                A.Name = this.Name;
                return A;
            }
        }
        
        public static bool operator ==(StaticMeshActor left, StaticMeshActor right)
        {
            return left.Actor == right.Actor;
        }
        public static bool operator !=(StaticMeshActor left, StaticMeshActor right) { return !(left == right); }

        
    }
}
