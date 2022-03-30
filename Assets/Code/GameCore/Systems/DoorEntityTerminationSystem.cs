using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameCore.Systems
{
    public class DoorEntityTerminationSystem: IEcsRunSystem
    {
        private EcsFilterInject<Inc<DoorTermination>> _terminations = default;
        
        public void Run(EcsSystems systems)
        {
            foreach (int doorEnt in _terminations.Value)
            {
                _terminations.Value.GetWorld().DelEntity(doorEnt);
            }
        }
    }
}