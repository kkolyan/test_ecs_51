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

        private void OnEnable()
        {
            _updateSystems = new EcsSystems(_world);

            _updateSystems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Add(new UnityCoreTimeSystem())
                .Add(new LoadLevelSystem())
                .Add(new MouseControlSystem())
                .Add(new NavMeshSystem())
                .Add(new PcDollInitSystem())
                .Add(new DoorTriggerSystem())
                .Add(new DestMarkerSystem())
                .Add(new DoorAnimationSystem())
                .Add(new DoorTriggerAnimationSystem())
                .Add(new DoorDollTerminationSystem())
                .Add(new DoorEntityTerminationSystem())
                .Add(new DelComponent<NavigationEvent>())
                .Add(new DelComponent<SceneLoadedEvent>())
                .Add(new DelComponent<TriggerEnterEvent>())
                .Add(new DelComponent<TriggerExitEvent>())
                .Add(new DelComponent<PcInitialization>())
                .Add(new DelComponent<SceneLoadedEvent>());

            InjectWithinSystems(_updateSystems);

            _updateSystems.Inject();
            _updateSystems.Init();

            _world.GetPool<SceneLoadedEvent>().Add(_world.NewEntity());
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