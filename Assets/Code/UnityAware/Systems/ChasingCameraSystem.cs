using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.EcsComponents;
using UnityEngine;

namespace UnityAware.Systems
{
    public class ChasingCameraSystem: IEcsRunSystem
    {
        private EcsFilterInject<Inc<PcMarker>> _pcState = default;
        private EcsPoolInject<Ref<Transform>> _transforms = default; 
        public void Run(EcsSystems systems)
        {
            foreach (int pcEnt in _pcState.Value)
            {
                Camera.main.transform.root.transform.position = _transforms.Value.Get(pcEnt).value.position;
            }
        }
    }
}