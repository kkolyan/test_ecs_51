using GameCore.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace UnityAware.MonoBehs
{
    public class TriggerEventSource: MonoBehaviour
    {
        private EcsPool<TriggerEnterEvent> _enters;
        private EcsPool<TriggerExitEvent> _exits;
        private EcsWorld _world;

        [Inject]
        public void Init(EcsWorld world)
        {
            _world = world;
            _enters = world.GetPool<TriggerEnterEvent>();
            _exits = world.GetPool<TriggerExitEvent>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EntityLink otherLink))
            {
                ref TriggerEnterEvent ev = ref _enters.Add(_world.NewEntity());
                ev.source = GetComponent<EntityLink>().link;
                ev.other = otherLink.link;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EntityLink otherLink))
            {
                ref TriggerExitEvent ev = ref _exits.Add(_world.NewEntity());
                ev.source = GetComponent<EntityLink>().link;
                ev.other = otherLink.link;
            }
        }
    }
}