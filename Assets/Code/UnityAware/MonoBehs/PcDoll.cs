using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace UnityAware.MonoBehs
{
    public class PcDoll : MonoBehaviour
    {
        [HideInInspector]
        [Inject]
        public NavMeshAgent nma;
        
        public class Factory : PlainResolvingFactory<PcDoll> { }
    }
}