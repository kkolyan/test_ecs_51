using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.Components;
using UnityEngine;
using UnityEngine.AI;

namespace UnityAware.Systems
{
    public class PcAnimationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PcMarker>> _pcs = default;
        private EcsFilterInject<Inc<PcInitialization>> _pcInits = default;

        private EcsPoolInject<NavMeshAgentRef> _agents = default;
        private EcsPoolInject<AnimatorRef> _animators = default;

        private static readonly int SpeedHash = Animator.StringToHash("Speed");

        public void Run(EcsSystems systems)
        {
            foreach (int pcEnt in _pcInits.Value)
            {
                NavMeshAgent agent = _agents.Value.Get(pcEnt).agent;
                if (agent)
                {
                    Animator animator = agent.GetComponentInChildren<Animator>();
                    if (animator)
                    {
                        _animators.Value.Add(pcEnt).animator = animator;
                    }
                }
            }

            foreach (int pcEnt in _pcs.Value)
            {
                if (_animators.Value.Has(pcEnt))
                {
                    Animator animator = _animators.Value.Get(pcEnt).animator;
                    NavMeshAgent agent = _agents.Value.Get(pcEnt).agent;
                    animator.SetFloat(SpeedHash, agent.velocity.magnitude / agent.speed);
                }
            }
        }
    }
}