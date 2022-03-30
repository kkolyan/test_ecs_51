using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameCore.Components
{
    [Serializable]
    public struct NavigationCalculationResult: IEcsAutoReset<NavigationCalculationResult>
    {
        public EcsPackedEntityWithWorld actor;
        public List<Vector3> route;
        public void AutoReset(ref NavigationCalculationResult c)
        {
            c.route ??= new List<Vector3>();
            c.route.Clear();
        }
    }
}