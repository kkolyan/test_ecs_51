using UnityEngine;

namespace UnityAware.MonoBehs
{
    public class DestMarker : MonoBehaviour
    {
        public class Factory : PlainResolvingFactory<DestMarker> { }
    }
}