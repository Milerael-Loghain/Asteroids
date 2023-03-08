using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class PlayerMovementSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<PlayerComponent>()
                .Inc<PlayerInputComponent>()
                .Inc<Rigidbody2DComponent>()
                .Inc<RotationComponent>()
                .End();

            var playerInputPool = systems.ECSWorld.GetPool<PlayerInputComponent>();
            var rigidbody2DReferencePool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var accelerationPool = systems.ECSWorld.GetPool<AccelerationComponent>();
            var rotationPool = systems.ECSWorld.GetPool<RotationComponent>();

            var playerConfig = systems.GetSharedData<ConfigContainer>().PlayerConfig;

            foreach (var entity in filter)
            {
                ref var rigidbody2DReference = ref rigidbody2DReferencePool.Get(entity);
                ref var playerInput = ref playerInputPool.Get(entity);

                var playerInputX = playerInput.moveValue.x;

                ref var rotation = ref rotationPool.Get(entity);
                rotation.value = playerConfig.RotationVelocity * playerInputX * -1;

                var playerInputMultiplierY = Mathf.Clamp(playerInput.moveValue.y, 0, 1);
                var accelerationVector = rigidbody2DReference.rb2d.transform.up * playerConfig.Acceleration * playerInputMultiplierY;

                if (Mathf.Approximately(accelerationVector.sqrMagnitude, 0))
                {
                    if (accelerationPool.Has(entity))
                    {
                        accelerationPool.Remove(entity);
                        return;
                    }

                    return;
                }

                if (accelerationPool.Has(entity))
                {
                    ref var acceleration = ref accelerationPool.Get(entity);
                    acceleration.value = accelerationVector;
                }
                else
                {
                    ref var acceleration = ref accelerationPool.Add(entity);
                    acceleration.value = accelerationVector;
                }

            }
        }
    }
}