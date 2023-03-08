using System;
using System.Collections.Generic;
using System.Linq;

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

        public HashSet<int> End()
        {
            var filteredEntities = new HashSet<int>();

            var listOfIncludedEntities = new List<List<int>>();

            foreach (var includedType in _included)
            {
                if (!_ecsWorld._pools.ContainsKey(includedType))
                {
                    listOfIncludedEntities.Add(new List<int>());
                    continue;
                }

                var includedEntities = _ecsWorld._pools[includedType].GetEntities();
                listOfIncludedEntities.Add(includedEntities);
            }

            filteredEntities = listOfIncludedEntities
                .Skip(1)
                .Aggregate(
                    new HashSet<int>(listOfIncludedEntities.First()),
                    (h, e) => { h.IntersectWith(e); return h; }
                );

            foreach (var excludedType in _excluded)
            {
                if (!_ecsWorld._pools.ContainsKey(excludedType)) continue;

                var excludedEntities = _ecsWorld._pools[excludedType].GetEntities();
                foreach (var excludedEntity in excludedEntities)
                {
                    if (filteredEntities.Contains(excludedEntity))
                    {
                        filteredEntities.Remove(excludedEntity);
                    }
                }
            }

            return filteredEntities;
        }
    }
}