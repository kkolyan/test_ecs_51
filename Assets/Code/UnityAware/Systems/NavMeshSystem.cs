using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.AI;

namespace UnityAware.Systems
{
    public class NavMeshSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<NavigationRequest>> _requests = "short";

        private EcsPoolInject<Ref<NavMeshAgent>> _agents = default;

        private EcsPoolInject<NavigationEvent> _navEvents = "short";

        private EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {

            foreach (int requestId in _requests.Value)
            {
                ref NavigationRequest request = ref _requests.Pools.Inc1.Get(requestId);
                if (request.actor.Unpack(_world.Value, out int actor))
                {
                    _agents.Value.Get(actor).value.SetDestination(request.destination);

                    ref NavigationEvent navigationEvent = ref _navEvents.Value.Add(_navEvents.Value.GetWorld().NewEntity());
                    navigationEvent.actor = request.actor;
                    navigationEvent.destination = request.destination;
                }

                _requests.Value.GetWorld().DelEntity(requestId);
            }
        }
    }
}