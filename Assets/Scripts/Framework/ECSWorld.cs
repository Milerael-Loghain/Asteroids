using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Framework
{
    public class ECSWorld
    {
        private List<int> _entities = new();
        private Dictionary<Type, IECSPool> _pools = new();

        public int AddEntity()
        {
            var newEntity = 0;
            while (_entities.Contains(newEntity))
            {
                newEntity++;
            }

            _entities.Add(newEntity);

            Debug.Log($"New entity Added: {newEntity}");

            return newEntity;
        }

        public void RemoveEntity(int entity)
        {
            if (!_entities.Contains(entity)) return;

            foreach (var ecsPool in _pools.Values)
            {
                ecsPool.Remove(entity);
            }

            _entities.Remove(entity);

            Debug.Log($"Entity Removed: {entity}");
        }

        public ECSPool<T> GetPool<T>() where T: struct
        {
            var type = typeof(T);
            if (!_pools.ContainsKey(type))
            {
                _pools[type] = new ECSPool<T>();
            }

            return (ECSPool<T>) _pools[type];
        }

        public ECSFilter Filter<T>() where T: struct
        {
            var filter = new ECSFilter();
            return filter.Inc<T>();
        }

        public List<int> End(ECSFilter ecsFilter)
        {
            var filteredEntities = new List<int>(_entities);

            foreach (var includedType in ecsFilter.Included)
            {
                if (_pools.ContainsKey(includedType))
                {
                    filteredEntities.AddRange(_pools[includedType].GetEntities());
                }
            }

            foreach (var excludedType in ecsFilter.Excluded)
            {
                if (!_pools.ContainsKey(excludedType)) continue;

                var excludedEntities = _pools[excludedType].GetEntities();
                foreach (var excludedEntity in excludedEntities)
                {
                    if (filteredEntities.Contains(excludedEntity))
                    {
                        filteredEntities.Add(excludedEntity);
                    }
                }
            }

            return filteredEntities;
        }
    }
}