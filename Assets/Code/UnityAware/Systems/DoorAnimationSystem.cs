using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.Components;

namespace UnityAware.Systems
{
    public class DoorAnimationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DoorOpenedDirt>> _filter = default;
        private EcsPoolInject<DoorState> _doorStates = default;
        private EcsPoolInject<AdjustableRef> _adjustables = default;

        public void Run(EcsSystems systems)
        {
            foreach (int doorEnt in _filter.Value)
            {
                _adjustables.Value.Get(doorEnt).adjustable.SetValue(_doorStates.Value.Get(doorEnt).openProgress);
                _filter.Pools.Inc1.Del(doorEnt);
            }
        }
    }
}