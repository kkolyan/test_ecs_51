using UnityEngine;
using Zenject;

namespace UnityAware.MonoBehs
{
    public class DoorTrigger: MonoBehaviour
    {
        public Door door;

        [Inject] public IAdjustable adjustable;
    }
}