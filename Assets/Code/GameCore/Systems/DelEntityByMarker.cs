using Leopotam.EcsLite;

namespace GameCore.Systems
{
    public class DelEntityByMarker<T>: IEcsInitSystem, IEcsRunSystem 
        where T : struct
    {
        private string _worldName;
        private EcsFilter _filter;
        private EcsWorld _world;

        public DelEntityByMarker(string worldName = null) {
            _worldName = worldName;
        }

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld(_worldName);
            _filter = _world.Filter<T>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                _world.DelEntity(entity);
            }
        }
    }
}