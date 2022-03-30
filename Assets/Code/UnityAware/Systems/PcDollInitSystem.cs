using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.MonoBehs;
using UnityEngine.AI;
using Zenject;

namespace UnityAware.Systems
{
    public class PcDollInitSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PcInitialization>> _seeds = default;

        private EcsPoolInject<Ref<NavMeshAgent>> _nmas = default;

        [Inject] private PcDoll.Factory _pcDolls;

        public void Run(EcsSystems systems)
        {
            foreach (int pc in _seeds.Value)
            {
                PcInitialization pcInitialization = _seeds.Pools.Inc1.Get(pc);

                PcDoll pcDoll = _pcDolls.Create();
                pcDoll.transform.position = pcInitialization.initialPosition;
                _nmas.Value.Add(pc).value = pcDoll.nma;

                pcDoll.gameObject.GetComponent<EntityLink>().link = _seeds.Value.GetWorld().PackEntityWithWorld(pc);
            }
        }
    }
}