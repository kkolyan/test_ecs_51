using System;
using Leopotam.EcsLite;

namespace GameCore.Components
{
    [Serializable]
    public struct TriggerEnterEvent<T> where T: struct
    {
        public EcsPackedEntity source;
        public EcsPackedEntity other;
    }
}