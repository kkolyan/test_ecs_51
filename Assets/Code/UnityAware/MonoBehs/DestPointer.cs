using UnityEngine;

namespace UnityAware.MonoBehs
{
    public class DestPointer : MonoBehaviour
    {
        public class Factory : PlainResolvingFactory<DestPointer> { }
    }
}