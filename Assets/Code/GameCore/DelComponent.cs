using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameCore
{
    public class DelComponent<T>: IEcsRunSystem where T : struct
    {
        private EcsFilterInject<Inc<T>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}