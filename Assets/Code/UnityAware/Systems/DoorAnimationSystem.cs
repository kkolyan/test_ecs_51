using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.MonoBehs;

namespace UnityAware.Systems
{
    public class DoorAnimationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DoorOpenedDirt>> _filter = default;
        private EcsPoolInject<DoorState> _doorStates = default;
        private EcsPoolInject<Ref<IAdjustable>> _adjustables = default;

        public void Run(EcsSystems systems)
        {
            foreach (int doorEnt in _filter.Value)
            {
                _adjustables.Value.Get(doorEnt).value.SetValue(_doorStates.Value.Get(doorEnt).openProgress);
                _filter.Pools.Inc1.Del(doorEnt);
            }
        }
    }
}