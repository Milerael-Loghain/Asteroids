using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Framework
{
    public interface IECSSystem
    {

    }

    public interface IECSInitSystem : IECSSystem {
        public void Init(IECSSystems systems);
    }

    public interface IECSRunSystem : IECSSystem {
        public void Run(IECSSystems systems);
    }

    public interface IECSSystems
    {
        public IECSSystems Add(IECSSystem system);
        public T GetSharedData<T>() where T : class;
        public ECSWorld ECSWorld { get; }

        public void Init();
        public void Run();
    }

    public class ECSSystems : IECSSystems
    {
        private List<IECSRunSystem> _ecsRunSystems = new();
        private List<IECSInitSystem> _ecsInitSystems = new();
        private Object _sharedData;
        private ECSWorld _ecsWorld;

        public ECSWorld ECSWorld => _ecsWorld;

        public ECSSystems(ECSWorld ecsWorld, Object sharedData)
        {
            _ecsWorld = ecsWorld;
            _sharedData = sharedData;
        }

        public IECSSystems Add(IECSSystem system)
        {
            if (system is IECSInitSystem ecsInitSystem)
            {
                _ecsInitSystems.Add(ecsInitSystem);
            }
            else if (system is IECSRunSystem ecsRunSystem)
            {
                _ecsRunSystems.Add(ecsRunSystem);
            }

            return this;
        }

        public T GetSharedData<T>() where T : class
        {
            return _sharedData as T;
        }

        public void Init()
        {
            foreach (var ecsInitSystem in _ecsInitSystems)
            {
                ecsInitSystem.Init(this);
            }
        }

        public void Run()
        {
            foreach (var ecsRunSystem in _ecsRunSystems)
            {
                ecsRunSystem.Run(this);
            }
        }
    }
}