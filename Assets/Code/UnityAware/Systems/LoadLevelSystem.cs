using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.EcsComponents;
using UnityAware.MonoBehs;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace UnityAware.Systems
{
    public class LoadLevelSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SceneLoadedEvent>> _sceneLoaded = default;

        private EcsPoolInject<PcMarker> _pcs = default;
        private EcsPoolInject<PcInitialization> _pcInits = default;
        private EcsPoolInject<DoorTriggerState> _triggers = default;
        private EcsPoolInject<DoorState> _doors = default;
        private EcsPoolInject<InitializationPending> _initPendings = default;
        private EcsPoolInject<Ref<IAdjustable>> _adjustables = default;
        private EcsPoolInject<Ref<NavMeshObstacle>> _obstacles = default;

        private EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            foreach (int _ in _sceneLoaded.Value)
            {
                foreach (StartPos startPos in Object.FindObjectsOfType<StartPos>())
                {
                    int pc = _world.Value.NewEntity();
                    _pcs.Value.Add(pc);
                    _pcInits.Value.Add(pc).initialPosition = startPos.transform.position;

                    startPos.enabled = false;
                }

                foreach (DoorTrigger trigger in Object.FindObjectsOfType<DoorTrigger>())
                {
                    if (!trigger.door)
                    {
                        continue;
                    }

                    int doorEnt = _world.Value.NewEntity();
                    _doors.Value.Add(doorEnt);
                    _initPendings.Value.Add(doorEnt);
                    _adjustables.Value.Add(doorEnt).value = trigger.door.GetComponent<IAdjustable>().FailIfNull();
                    NavMeshObstacle obstacle = trigger.door.GetComponent<NavMeshObstacle>();

                    _obstacles.Value.Add(doorEnt).value = obstacle.FailIfNull();

                    int triggerEnt = _world.Value.NewEntity();
                    ref DoorTriggerState doorTriggerState = ref _triggers.Value.Add(triggerEnt);
                    doorTriggerState.door = _world.Value.PackEntityWithWorld(doorEnt);
                    _initPendings.Value.Add(triggerEnt);
                    _adjustables.Value.Add(triggerEnt).value = trigger.GetComponent<IAdjustable>().FailIfNull(trigger);

                    foreach (EntityLink child in trigger.GetComponentsInChildren<EntityLink>())
                    {
                        child.link = _world.Value.PackEntityWithWorld(triggerEnt);
                    }
                }
            }
        }
    }
}