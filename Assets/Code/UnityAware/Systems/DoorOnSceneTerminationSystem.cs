using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.Components;

namespace UnityAware.Systems
{
    public class DoorOnSceneTerminationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DoorTermination>> _filter = default;
        private EcsPoolInject<NavMeshObstacleRef> _obstacles = default;

        public void Run(EcsSystems systems)
        {
            foreach (int doorEnt in _filter.Value)
            {
                _obstacles.Value.Get(doorEnt).obstacle.enabled = false;
            }
        }
    }
}