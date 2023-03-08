using Asteroids.Framework;
using Asteroids.Game.Components;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class ApplyVelocitySystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<VelocityComponent>()
                .Inc<Rigidbody2DComponent>()
                .End();

            var rigidbody2DReferencePool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();

            foreach (var entity in filter)
            {
                ref var rigidbody2DReference = ref rigidbody2DReferencePool.Get(entity);
                ref var velocity = ref velocityPool.Get(entity);

                var newPosition = rigidbody2DReference.position + (velocity.value * Time.fixedDeltaTime);
                rigidbody2DReference.position = newPosition;
            }
        }
    }
}