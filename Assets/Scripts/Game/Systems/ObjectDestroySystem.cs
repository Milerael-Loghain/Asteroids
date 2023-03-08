using Asteroids.Framework;
using Asteroids.Game.Components;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class ObjectDestroySystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<DestroyComponent>().End();
            var rigidbody2DPool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();

            foreach (var entity in filter)
            {
                ref var rigidbody = ref rigidbody2DPool.Get(entity);
                Object.Destroy(rigidbody.rb2d.gameObject);

                systems.ECSWorld.RemoveEntity(entity);
            }
        }
    }
}