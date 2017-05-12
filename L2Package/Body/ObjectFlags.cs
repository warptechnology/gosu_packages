using System;

namespace L2Package
{
    internal enum ObjectFlags : uint
    {
        RF_Transactional = 0x00000001, //Object is transactional.
        RF_Unreachable = 0x00000002, //Object is not reachable on the object graph.
        RF_Public = 0x00000004, //Object is visible outside its package.
        RF_TagImp = 0x00000008, //Temporary import tag in load/save.
        RF_TagExp = 0x00000010, //Temporary export tag in load/save.
        RF_SourceModified = 0x00000020,//Modified relative to source files.
        RF_TagGarbage = 0x00000040, //Check during garbage collection.
        RF_NeedLoad = 0x00000200, //During load, indicates object needs loading.
        RF_HighlightedName = 0x00000400, //A hardcoded name which should be syntaxhighlighted.
        RF_InSingularFunc = 0x00000800, //In a singular function.
        RF_Suppress = 0x00001000,//Suppressed log name.
        RF_InEndState = 0x00002000, //Within an EndState call.
        RF_Transient = 0x00004000, //Don't save object.
        RF_PreLoading = 0x00008000, //Data is being preloaded from file.
        RF_LoadForClient = 0x00010000, //In-file load for client.
        RF_LoadForServer = 0x00020000, //In-file load for client.
        RF_LoadForEdit = 0x00040000, //In-file load for client.
        RF_Standalone = 0x00080000, //Keep object around for editing even if unreferenced.
        RF_NotForClient = 0x00100000, //Don't load this object for the game client.
        RF_NotForServer = 0x00200000, //Don't load this object for the game server.
        RF_NotForEdit = 0x00400000, //Don't load this object for the editor.
        RF_Destroyed = 0x00800000, //Object Destroy has already been called.
        RF_NeedPostLoad = 0x01000000, //Object needs to be postloaded.
        RF_HasStack = 0x02000000, //Has execution stack.
        RF_Native = 0x04000000, //Native (UClass only).
        RF_Marked = 0x08000000, //Marked (for debugging).
        RF_ErrorShutdown = 0x10000000, //ShutdownAfterError called.
        RF_DebugPostLoad = 0x20000000, //For debugging Serialize calls.
        RF_DebugSerialize = 0x40000000, //For debugging Serialize calls.
        RF_DebugDestroy = 0x80000000 //For debugging Destroy calls.
    }
}