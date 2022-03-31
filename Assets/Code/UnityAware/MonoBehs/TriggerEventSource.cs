using GameCore.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace UnityAware.MonoBehs
{
    public abstract class TriggerEventSource<T> : MonoBehaviour 
        where T: struct
    {
        private EcsPool<TriggerEnterEvent<T>> _enters;
        private EcsPool<TriggerExitEvent<T>> _exits;
        private EcsWorld _world;
        
        [Inject] private EntityLink _entityLink;

        [Inject]
        public void SetWorld([Inject(Id = "short")] EcsWorld world)
        {
            _world = world;
            _enters = world.GetPool<TriggerEnterEvent<T>>();
            _exits = world.GetPool<TriggerExitEvent<T>>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EntityLink otherLink))
            {
                ref TriggerEnterEvent<T> ev = ref _enters.Add(_world.NewEntity());
                ev.source = _entityLink.link;
                ev.other = otherLink.link;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EntityLink otherLink))
            {
                ref TriggerExitEvent<T> ev = ref _exits.Add(_world.NewEntity());
                ev.source = _entityLink.link;
                ev.other = otherLink.link;
            }
        }
    }
}