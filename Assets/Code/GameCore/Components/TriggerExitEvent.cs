using System;
using Leopotam.EcsLite;

namespace GameCore.Components
{
    [Serializable]
    public struct TriggerExitEvent
    {
        public EcsPackedEntityWithWorld source;
        public EcsPackedEntityWithWorld other;
    }
}