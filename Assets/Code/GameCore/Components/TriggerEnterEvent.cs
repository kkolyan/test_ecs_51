using System;
using Leopotam.EcsLite;

namespace GameCore.Components
{
    [Serializable]
    public struct TriggerEnterEvent
    {
        public EcsPackedEntityWithWorld source;
        public EcsPackedEntityWithWorld other;
    }
}