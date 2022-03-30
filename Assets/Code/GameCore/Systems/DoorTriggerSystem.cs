using GameCore.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace GameCore.Systems
{
    public class DoorTriggerSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TriggerEnterEvent>> _enters = default;
        private EcsFilterInject<Inc<TriggerExitEvent>> _exits = default;
        private EcsFilterInject<Inc<DoorTriggerState>> _triggers = default;

        private EcsPoolInject<DoorState> _doors = default;
        private EcsPoolInject<DoorOpenedDirt> _doorOpenedDirt = default;
        private EcsPoolInject<TriggerLoweredDirt> _triggerLoweredDirt = default;
        private EcsPoolInject<DoorTermination> _terminations = default;

        [Inject] private CoreTime _time;
        [Inject] private GameSettings _settings;

        public void Run(EcsSystems systems)
        {
            EcsPool<DoorTriggerState> triggers = _triggers.Pools.Inc1;
            foreach (int enterEnt in _enters.Value)
            {
                if (_enters.Pools.Inc1.Get(enterEnt).other.Unpack(out _, out int otherEnt))
                {
                    if (triggers.Has(otherEnt))
                    {
                        triggers.Get(otherEnt).stoodOn = true;
                    }
                }
            }

            foreach (int exitEnt in _exits.Value)
            {
                if (_exits.Pools.Inc1.Get(exitEnt).other.Unpack(out _, out int otherEnt))
                {
                    if (triggers.Has(otherEnt))
                    {
                        triggers.Get(otherEnt).stoodOn = false;
                    }
                }
            }

            foreach (int triggerEnt in _triggers.Value)
            {
                ref DoorTriggerState triggerState = ref triggers.Get(triggerEnt);

                if (triggerState.door.Unpack(out EcsWorld _, out int doorEnt))
                {
                    ref DoorState doorState = ref _doors.Value.Get(doorEnt);
                    if (triggerState.stoodOn)
                    {
                        if (triggerState.loweringProgress < 1)
                        {
                            triggerState.loweringProgress += _settings.triggerLoweringSpeed * _time.deltaTime;
                            _triggerLoweredDirt.Value.GetOrAdd(triggerEnt);
                        }

                        if (doorState.openProgress < 1)
                        {
                            doorState.openProgress += _settings.doorOpenSpeed * _time.deltaTime;
                            _doorOpenedDirt.Value.GetOrAdd(doorEnt);
                            if (doorState.openProgress >= 1 && !doorState.opened)
                            {
                                _terminations.Value.Add(doorEnt);
                            }
                        }
                    }
                    else
                    {
                        if (triggerState.loweringProgress > 0)
                        {
                            triggerState.loweringProgress -= _settings.triggerLoweringSpeed * _time.deltaTime;
                            _triggerLoweredDirt.Value.GetOrAdd(triggerEnt);
                        }
                    }
                }
            }
        }
    }
}