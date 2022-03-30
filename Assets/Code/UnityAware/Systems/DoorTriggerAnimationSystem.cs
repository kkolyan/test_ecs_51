using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.MonoBehs;

namespace UnityAware.Systems
{
    public class DoorTriggerAnimationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TriggerLoweredDirt, DoorTriggerState, Ref<IAdjustable>>> _filter = default;
        private EcsPoolInject<DoorTriggerState> _triggerStates = default;
        private EcsPoolInject<Ref<IAdjustable>> _adjustables = default;

        public void Run(EcsSystems systems)
        {
            foreach (int doorEnt in _filter.Value)
            {
                _adjustables.Value.Get(doorEnt).value.SetValue(_triggerStates.Value.Get(doorEnt).loweringProgress);
                _filter.Pools.Inc1.Del(doorEnt);
            }
        }
    }
}