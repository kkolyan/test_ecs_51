using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.Components;
using UnityAware.MonoBehs;

namespace UnityAware.Systems
{
    public class DoorTriggerAnimationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TriggerLoweredDirt>> _filter = default;
        private EcsPoolInject<DoorTriggerState> _triggerStates = default;
        private EcsPoolInject<AdjustableRef> _adjustables = default;

        public void Run(EcsSystems systems)
        {
            foreach (int doorEnt in _filter.Value)
            {
                IAdjustable triggerAdjuster = _adjustables.Value.Get(doorEnt).adjustable;
                DoorTriggerState doorTriggerState = _triggerStates.Value.Get(doorEnt);
                triggerAdjuster.SetValue(doorTriggerState.loweringProgress);
                
                _filter.Pools.Inc1.Del(doorEnt);
            }
        }
    }
}