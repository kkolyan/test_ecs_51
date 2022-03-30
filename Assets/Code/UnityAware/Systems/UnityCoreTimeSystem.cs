using GameCore;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace UnityAware.Systems
{
    public class UnityCoreTimeSystem : IEcsRunSystem
    {
        [Inject] private CoreTime _time;

        public void Run(EcsSystems systems)
        {
            _time.deltaTime = Time.deltaTime;
        }
    }
}