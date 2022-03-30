using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameCore.Components
{
    [Serializable]
    public struct NavigationCalculationRequest
    {
        public EcsPackedEntityWithWorld actor;
        public Vector3 destination;
    }
}