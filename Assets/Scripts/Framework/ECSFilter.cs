using System;
using System.Collections.Generic;

namespace Asteroids.Framework
{
    public class ECSFilter
    {
        public List<Type> Included = new();
        public List<Type> Excluded = new();

        public ECSFilter Inc<T>() where T: struct
        {
            Included.Add(typeof(T));
            return this;
        }

        public ECSFilter Exc<T>() where T: struct
        {
            Excluded.Add(typeof(T));
            return this;
        }
    }
}