using Asteroids.Framework;
using Asteroids.Game.Components;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class RotationSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<RotationComponent>()
                .Inc<Rigidbody2DComponent>()
                .End();

            var rigidbody2DReferencePool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var rotationComponentPool = systems.ECSWorld.GetPool<RotationComponent>();

            foreach (var entity in filter)
            {
                ref var rigidbody2DReference = ref rigidbody2DReferencePool.Get(entity);
                ref var rotationVelocity = ref rotationComponentPool.Get(entity);

                var newPosition = rigidbody2DReference.rb2d.rotation + (rotationVelocity.value * Time.fixedDeltaTime);
                rigidbody2DReference.rb2d.MoveRotation(newPosition);
            }
        }
    }
}