using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameCore.Components
{
    [Serializable]
    public struct NavigationEvent
    {
        public EcsPackedEntityWithWorld actor;
        public Vector3 destination;
    }
}