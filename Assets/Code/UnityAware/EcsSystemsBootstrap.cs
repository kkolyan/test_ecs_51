using GameCore;
using GameCore.Components;
using GameCore.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.EcsComponents;
using UnityAware.Systems;
using UnityEngine;
using Zenject;

namespace UnityAware
{
    public class EcsSystemsBootstrap : MonoBehaviour
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
                .Add(new LoadLevelSystem())
                .Add(new MouseControlSystem())
                .Add(new NavMeshSystem())
                .Add(new PcDollInitSystem())
                .Add(new DoorTriggerSystem())
                .Add(new DestPointerSystem())
                .Add(new DoorAnimationSystem())
                .Add(new DoorTriggerAnimationSystem())
                .Add(new DoorDollTerminationSystem())
                .Add(new DoorEntityTerminationSystem())
                .Add(new PcAnimationSystem())
                .Add(new ChasingCameraSystem())
                .Add(new DelComponent<NavigationEvent>("short"))
                .Add(new DelComponent<SceneLoadedEvent>("short"))
                .Add(new DelComponent<TriggerEnterEvent>("short"))
                .Add(new DelComponent<TriggerExitEvent>("short"))
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