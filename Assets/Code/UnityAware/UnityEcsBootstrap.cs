using GameCore.Components;
using GameCore.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.Components;
using UnityAware.Systems;
using UnityEngine;
using Zenject;

namespace UnityAware
{
    public class UnityEcsBootstrap : MonoBehaviour
    {
        private EcsSystems _updateSystems;

        [Inject] private DiContainer _container;
        [Inject] private EcsWorld _world;
        [Inject(Id = "short")] private EcsWorld _worldShort;

        private void OnEnable()
        {
            _updateSystems = new EcsSystems(_world);

            _updateSystems.AddWorld(_worldShort, "short");
            _updateSystems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem("short"))
#endif
                .Add(new UnityCoreTimeSystem())
                .Add(new UnityLoadLevelSystem())
                .Add(new MouseControlSystem())
                .Add(new UnityNavigationSystem())
                .Add(new PcOnSceneInitializationSystem())
                .Add(new DoorTriggerSystem())
                .Add(new DestPointerSystem())
                .Add(new DoorAnimationSystem())
                .Add(new DoorTriggerAnimationSystem())
                .Add(new DoorOnSceneTerminationSystem())
                .Add(new PcAnimationSystem())
                .Add(new ChasingCameraSystem())
                .Add(new DelEntityByMarker<DoorTermination>())
                .Add(new DelComponent<NavigationEvent>("short"))
                .Add(new DelComponent<SceneLoadedEvent>("short"))
                .Add(new DelComponent<TriggerEnterEvent<DestPointerMarker>>("short"))
                .Add(new DelComponent<TriggerExitEvent<DestPointerMarker>>("short"))
                .Add(new DelComponent<TriggerEnterEvent<DoorTriggerState>>("short"))
                .Add(new DelComponent<TriggerExitEvent<DoorTriggerState>>("short"))
                .Add(new DelComponent<SceneLoadedEvent>("short"))
                .Add(new DelComponent<PcInitialization>());


            InjectWithinSystems(_updateSystems);

            _updateSystems.Inject();
            _updateSystems.Init();

            _worldShort.GetPool<SceneLoadedEvent>().Add(_worldShort.NewEntity());
        }

        private void InjectWithinSystems(EcsSystems systems)
        {
            IEcsSystem[] list = null;
            int count = systems.GetAllSystems(ref list);
            for (int i = 0; i < count; i++)
            {
                _container.Inject(list[i]);
            }
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

        private void OnDisable()
        {
            _updateSystems?.Destroy();
        }
    }
}