using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.AI;

namespace UnityAware.Systems
{
    public class DoorDollTerminationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DoorTermination>> _filter = default;
        private EcsPoolInject<Ref<NavMeshObstacle>> _obstacles = default;

        public void Run(EcsSystems systems)
        {
            foreach (int doorEnt in _filter.Value)
            {
                _obstacles.Value.Get(doorEnt).value.enabled = false;
            }
        }
    }
}