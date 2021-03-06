using Leopotam.EcsLite;

namespace GameCore.Systems
{
    public class DelComponent<T>: IEcsRunSystem, IEcsInitSystem
        where T : struct
    {
        private string _worldName;
        private EcsFilter _filter;
        private EcsPool<T> _pool;

        public DelComponent(string worldName = null) {
            _worldName = worldName;
        }

        public void Init(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld(_worldName);
            _filter = world.Filter<T>().End();
            _pool = world.GetPool<T>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                _pool.Del(entity);
            }
        }
    }
}