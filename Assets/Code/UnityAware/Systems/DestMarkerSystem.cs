using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.Components;
using UnityAware.MonoBehs;
using UnityEngine;
using Zenject;

namespace UnityAware.Systems
{
    public class DestPointerSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TriggerEnterEvent<DestPointerMarker>>> _enters = "short";
        private EcsFilterInject<Inc<SceneLoadedEvent>> _sceneLoaded = "short";
        private EcsFilterInject<Inc<NavigationEvent>> _navEvents = "short";
        private EcsFilterInject<Inc<DestPointerMarker>> _destPointers = default;

        private EcsPoolInject<DestPointerTransformRef> _transforms = default;

        private EcsWorldInject _world = default;

        [Inject] private DestPointer.Factory _destPointerFactory;

        public void Run(EcsSystems systems)
        {
            foreach (int _ in _sceneLoaded.Value)
            {
                DestPointer destPointer = _destPointerFactory.Create();

                int destPointerEnt = _world.Value.NewEntity();
                _destPointers.Pools.Inc1.Add(destPointerEnt);
                _transforms.Value.Add(destPointerEnt).transform = destPointer.transform;
                destPointer.entityLink.link = _world.Value.PackEntity(destPointerEnt);
                destPointer.gameObject.SetActive(false);
            }

            foreach (int enterEnt in _enters.Value)
            {
                if (_enters.Pools.Inc1.Get(enterEnt).other.Unpack(_world.Value, out int otherEnt))
                {
                    if (_destPointers.Pools.Inc1.Has(otherEnt))
                    {
                        _transforms.Value.Get(otherEnt).transform.gameObject.SetActive(false);
                    }
                }
            }

            foreach (int destPointerEnt in _destPointers.Value)
            {
                foreach (int navEnt in _navEvents.Value)
                {
                    Transform transform = _transforms.Value.Get(destPointerEnt).transform;
                    transform.position = _navEvents.Pools.Inc1.Get(navEnt).destination;
                    transform.gameObject.SetActive(true);
                }
            }
        }
    }
}