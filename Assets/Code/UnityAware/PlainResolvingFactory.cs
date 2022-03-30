using Zenject;

namespace UnityAware
{
    // I'd be glad to use PrefabFactory instead, but that require a prefab original reference at the `Create` call site, which is inconvenient in some cases.
    public abstract class PlainResolvingFactory<T>
    {
        [Inject] private DiContainer _container;
        
        public T Create()
        {
            return _container.Resolve<T>();
        }
    }
}