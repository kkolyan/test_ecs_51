using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace UnityAware.MonoBehs
{
    public class Door : MonoBehaviour
    {
        [Inject] public IAdjustable adjustable;
        [HideInInspector] [Inject] public NavMeshObstacle obstacle;
    }
}