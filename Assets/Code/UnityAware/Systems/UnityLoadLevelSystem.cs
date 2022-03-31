using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.Components;
using UnityAware.MonoBehs;
using UnityEngine;

namespace UnityAware.Systems
{
    public class UnityLoadLevelSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SceneLoadedEvent>> _sceneLoaded = "short";

        private EcsPoolInject<PcMarker> _pcs = default;
        private EcsPoolInject<PcInitialization> _pcInits = default;
        private EcsPoolInject<DoorTriggerState> _triggers = default;
        private EcsPoolInject<DoorState> _doors = default;
        private EcsPoolInject<AdjustableRef> _adjustables = default;
        private EcsPoolInject<NavMeshObstacleRef> _obstacles = default;

        private EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            foreach (int _ in _sceneLoaded.Value)
            {
                foreach (StartPosition startPos in Object.FindObjectsOfType<StartPosition>())
                {
                    int pc = _world.Value.NewEntity();
                    _pcs.Value.Add(pc);
                    _pcInits.Value.Add(pc).initialPosition = startPos.transform.position;

                    startPos.gameObject.SetActive(false);
                }

                foreach (DoorTrigger trigger in Object.FindObjectsOfType<DoorTrigger>())
                {
                    if (!trigger.door)
                    {
                        continue;
                    }

                    int doorEnt = _world.Value.NewEntity();
                    _doors.Value.Add(doorEnt);
                    _adjustables.Value.Add(doorEnt).adjustable= trigger.door.adjustable;

                    _obstacles.Value.Add(doorEnt).obstacle = trigger.door.obstacle;

                    int triggerEnt = _world.Value.NewEntity();
                    ref DoorTriggerState doorTriggerState = ref _triggers.Value.Add(triggerEnt);
                    doorTriggerState.door = _world.Value.PackEntity(doorEnt);
                    _adjustables.Value.Add(triggerEnt).adjustable = trigger.adjustable;

                    foreach (EntityLink child in trigger.GetComponentsInChildren<EntityLink>())
                    {
                        child.link = _world.Value.PackEntity(triggerEnt);
                    }
                }
            }
        }
    }
}