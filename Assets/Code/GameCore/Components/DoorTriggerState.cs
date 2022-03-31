using System;
using Leopotam.EcsLite;

namespace GameCore.Components
{
    [Serializable]
    public struct DoorTriggerState
    {
        public float loweringProgress;
        public bool stoodOn;
        public EcsPackedEntity door;
    }
}