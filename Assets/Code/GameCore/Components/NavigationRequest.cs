using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameCore.Components
{
    [Serializable]
    public struct NavigationRequest
    {
        public EcsPackedEntity actor;
        public Vector3 destination;
    }
}