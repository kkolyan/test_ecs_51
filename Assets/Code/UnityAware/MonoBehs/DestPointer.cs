using GameCore;
using UnityEngine;
using Zenject;

namespace UnityAware.MonoBehs
{
    public class DestPointer : MonoBehaviour
    {
        [HideInInspector] [Inject] public EntityLink entityLink;

        public class Factory : PlainResolvingFactory<DestPointer> { }
    }
}