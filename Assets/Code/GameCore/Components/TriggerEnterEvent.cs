using System;
using Leopotam.EcsLite;

namespace GameCore.Components
{
    [Serializable]
    public struct TriggerEnterEvent
    {
        public EcsPackedEntity source;
        public EcsPackedEntity other;
    }
}