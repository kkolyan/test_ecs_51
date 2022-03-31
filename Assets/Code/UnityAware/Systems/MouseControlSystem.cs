using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityAware.EcsComponents;
using UnityEngine;

namespace UnityAware.Systems
{
    public class MouseControlSystem : IEcsRunSystem
    {
        private RaycastHit[] _results = new RaycastHit[1];

        private readonly EcsPoolInject<NavigationRequest> _navRequests = "short";
        private EcsFilterInject<Inc<PcMarker>> _pcs = default;

        public void Run(EcsSystems systems)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                int hits = Physics.RaycastNonAlloc(mouseRay, _results);
                for (int i = 0; i < hits; i++)
                {
                    foreach (int pc in _pcs.Value)
                    {
                        ref NavigationRequest request = ref _navRequests.Value.Add(_navRequests.Value.GetWorld().NewEntity());
                        request.destination = _results[i].point;
                        request.actor = _pcs.Value.GetWorld().PackEntity(pc);
                    }
                }
            }
        }
    }
}