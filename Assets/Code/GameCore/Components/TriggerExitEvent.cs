using System;
using Leopotam.EcsLite;

namespace GameCore.Components
{
    [Serializable]
    public struct TriggerExitEvent<T> where T: struct
    {
        public EcsPackedEntity source;
        public EcsPackedEntity other;
    }
}