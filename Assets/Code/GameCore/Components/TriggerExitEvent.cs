using System;
using Leopotam.EcsLite;

namespace GameCore.Components
{
    [Serializable]
    public struct TriggerExitEvent
    {
        public EcsPackedEntity source;
        public EcsPackedEntity other;
    }
}