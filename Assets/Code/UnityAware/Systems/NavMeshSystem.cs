using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.EcsComponents;
using UnityEngine.AI;

namespace UnityAware.Systems
{
    public class NavMeshSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<NavigationCalculationRequest>> _requests = default;

        private EcsPoolInject<Ref<NavMeshAgent>> _agents = default;

        private EcsPoolInject<NavigationEvent> _navEvents = default;

        public void Run(EcsSystems systems)
        {

            foreach (int requestId in _requests.Value)
            {
                ref NavigationCalculationRequest request = ref _requests.Pools.Inc1.Get(requestId);
                if (request.actor.Unpack(out EcsWorld _, out int actor))
                {
                    _agents.Value.Get(actor).value.SetDestination(request.destination);
                }

                ref NavigationEvent navigationEvent = ref _navEvents.Value.Add(_navEvents.Value.GetWorld().NewEntity());
                navigationEvent.actor = request.actor;
                navigationEvent.destination = request.destination;

                _requests.Value.GetWorld().DelEntity(requestId);
            }
        }
    }
}