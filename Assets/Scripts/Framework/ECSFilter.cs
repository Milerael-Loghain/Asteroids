using System;
using System.Collections.Generic;

namespace Asteroids.Framework
{
    public class ECSFilter
    {
        private readonly List<Type> _included = new();
        private readonly List<Type> _excluded = new();

        private ECSWorld _ecsWorld;

        public ECSFilter(ECSWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }

        public ECSFilter Inc<T>() where T: struct
        {
            _included.Add(typeof(T));
            return this;
        }

        public ECSFilter Exc<T>() where T: struct
        {
            _excluded.Add(typeof(T));
            return this;
        }

        public List<int> End()
        {
            var filteredEntities = new List<int>(_ecsWorld._entities);

            foreach (var includedType in _included)
            {
                if (_ecsWorld._pools.ContainsKey(includedType))
                {
                    filteredEntities.AddRange(_ecsWorld._pools[includedType].GetEntities());
                }
            }

            foreach (var excludedType in _excluded)
            {
                if (!_ecsWorld._pools.ContainsKey(excludedType)) continue;

                var excludedEntities = _ecsWorld._pools[excludedType].GetEntities();
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