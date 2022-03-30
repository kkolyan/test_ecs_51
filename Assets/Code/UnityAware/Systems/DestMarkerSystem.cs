using System.Collections.Generic;
using System.Linq;
using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.EcsComponents;
using UnityAware.MonoBehs;
using UnityEngine;
using Zenject;

namespace UnityAware.Systems
{
    public class DestMarkerSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TriggerEnterEvent>> _enters = default;
        private EcsFilterInject<Inc<SceneLoadedEvent>> _sceneLoaded = default;
        private EcsFilterInject<Inc<NavigationEvent>> _navEvents = default;
        private EcsFilterInject<Inc<DestMarkerState>> _destMarkers = default;

        private EcsPoolInject<Ref<Transform>> _transforms = default;

        private EcsWorldInject _world = default;

        [Inject] private DestMarker.Factory _destMarkerFactory;

        public void Run(EcsSystems systems)
        {
            foreach (int _ in _sceneLoaded.Value)
            {
                DestMarker destMarker = _destMarkerFactory.Create();

                int destMarkerEnt = _world.Value.NewEntity();
                _destMarkers.Pools.Inc1.Add(destMarkerEnt);
                _transforms.Value.Add(destMarkerEnt).value = destMarker.transform;
                destMarker.GetComponent<EntityLink>().link = _world.Value.PackEntityWithWorld(destMarkerEnt);
                destMarker.gameObject.SetActive(false);
            }

            foreach (int enterEnt in _enters.Value)
            {
                if (_enters.Pools.Inc1.Get(enterEnt).other.Unpack(out _, out int otherEnt))
                {
                    if (_destMarkers.Pools.Inc1.Has(otherEnt))
                    {
                        _transforms.Value.Get(otherEnt).value.gameObject.SetActive(false);
                    }
                }
            }

            foreach (int destMarkerEnt in _destMarkers.Value)
            {
                foreach (int navEnt in _navEvents.Value)
                {
                    Transform transform = _transforms.Value.Get(destMarkerEnt).value;
                    transform.position = _navEvents.Pools.Inc1.Get(navEnt).destination;
                    transform.gameObject.SetActive(true);
                }
            }
        }
    }
}