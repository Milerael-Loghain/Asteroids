using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class PlayerPrimaryShootSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<PlayerComponent>()
                .Inc<PlayerInputComponent>()
                .Inc<Rigidbody2DComponent>()
                .Exc<PrimaryShootCooldownComponent>()
                .End();

            var playerInputPool = systems.ECSWorld.GetPool<PlayerInputComponent>();
            var bulletPool = systems.ECSWorld.GetPool<BulletComponent>();
            var rigidbody2dPool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var rendererPool = systems.ECSWorld.GetPool<RendererReferenceComponent>();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();
            var constantVelocityPool = systems.ECSWorld.GetPool<ConstantVelocityComponent>();
            var primaryShootCooldownPool = systems.ECSWorld.GetPool<PrimaryShootCooldownComponent>();
            var autoDestroyPool = systems.ECSWorld.GetPool<AutoDestroyComponent>();

            var playerConfig = systems.GetSharedData<ConfigContainer>().PlayerConfig;

            foreach (var entity in filter)
            {
                ref var playerInput = ref playerInputPool.Get(entity);
                if (!playerInput.primaryShootValue) continue;

                ref var playerRB2D = ref rigidbody2dPool.Get(entity);

                var bulletGO = Object.Instantiate(playerConfig.PrimaryGunConfig.PrimaryBulletPrefab, playerRB2D.position, Quaternion.identity);
                var bulletRb2D = bulletGO.GetComponent<Rigidbody2D>();

                var bulletEntity = systems.ECSWorld.AddEntity();
                constantVelocityPool.Add(bulletEntity);

                ref var bulletComponent = ref bulletPool.Add(bulletEntity);
                bulletComponent.layerMask = playerConfig.PrimaryGunConfig.PrimaryBulletLayerMask;
                bulletComponent.radius = playerConfig.PrimaryGunConfig.PrimaryBulletRadius;

                ref var rendererComponent = ref rendererPool.Add(bulletEntity);
                rendererComponent.value = bulletGO.GetComponent<Renderer>();

                ref var rigidBody2DComponent = ref rigidbody2dPool.Add(bulletEntity);
                rigidBody2DComponent.rb2d = bulletRb2D;
                rigidBody2DComponent.position = playerRB2D.position;

                ref var velocityComponent = ref velocityPool.Add(bulletEntity);
                velocityComponent.value = playerConfig.PrimaryGunConfig.PrimaryBulletVelocity * playerRB2D.rb2d.transform.up;

                ref var autoDestroyComponent = ref autoDestroyPool.Add(bulletEntity);
                autoDestroyComponent.lifetime = playerConfig.PrimaryGunConfig.PrimaryBulletLifetime;

                ref var shootCooldown = ref primaryShootCooldownPool.Add(entity);
                shootCooldown.value = playerConfig.PrimaryGunConfig.PrimaryBulletCooldown;
            }
        }
    }
}