using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.Framework
{
    public interface IECSPool
    {
        public List<int> GetEntities();
        public bool Has(int entity);
        public void Remove(int entity);
    }

    public class ECSPool<T> : IECSPool where T: struct
    {
        private readonly Dictionary<int, int> _entitiesToIndex = new();
        private T[] _componentInstances = Array.Empty<T>();

        public ref T Add(int entity)
        {
            var index = _componentInstances.Length;
            if (_entitiesToIndex.ContainsKey(entity))
            {
                throw new Exception($"Trying to add multiple components of same type to entity! Entity: {entity}, Component Type: {typeof(T)}");
            }

            _entitiesToIndex[entity] = index;
            if (_componentInstances.Length <= index)
            {
                Array.Resize(ref _componentInstances, index + 1);
            }

            _componentInstances[_entitiesToIndex[entity]] = new T();

            return ref _componentInstances[_entitiesToIndex[entity]];
        }

        public List<int> GetEntities()
        {
            return _entitiesToIndex.Keys.ToList();
        }

        public bool Has(int entity)
        {
            return _entitiesToIndex.ContainsKey(entity);
        }

        public void Remove(int entity)
        {
            if (_entitiesToIndex.ContainsKey(entity))
            {
                _entitiesToIndex.Remove(entity);
            }
        }

        public ref T Get(int entity)
        {
            if (!_entitiesToIndex.ContainsKey(entity))
            {
                throw new Exception($"Required component doesn't exist on Entity: {entity}, Component Type: {typeof(T)}");
            }

            return ref _componentInstances[_entitiesToIndex[entity]];
        }
    }
}