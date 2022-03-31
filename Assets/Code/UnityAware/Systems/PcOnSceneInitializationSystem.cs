using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.Components;
using UnityAware.MonoBehs;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace UnityAware.Systems
{
    public class PcOnSceneInitializationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PcInitialization>> _seeds = default;

        private EcsPoolInject<Ref<NavMeshAgent>> _nmas = default;
        private EcsPoolInject<Ref<Transform>> _transforms = default;

        [Inject] private Pc.Factory _pcDolls;

        public void Run(EcsSystems systems)
        {
            foreach (int pcEnt in _seeds.Value)
            {
                PcInitialization pcInitialization = _seeds.Pools.Inc1.Get(pcEnt);

                Pc pc = _pcDolls.Create();
                pc.nma.Warp(pcInitialization.initialPosition);
                _nmas.Value.Add(pcEnt).value = pc.nma;
                _transforms.Value.Add(pcEnt).value = pc.transform;

                pc.gameObject.GetComponent<EntityLink>().link = _seeds.Value.GetWorld().PackEntity(pcEnt);
            }
        }
    }
}