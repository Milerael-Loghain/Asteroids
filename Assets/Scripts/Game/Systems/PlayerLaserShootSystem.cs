using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class PlayerLaserShootSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<PlayerComponent>()
                .Inc<PlayerInputComponent>()
                .Inc<Rigidbody2DComponent>()
                .Inc<PlayerLaserShootComponent>()
                .Exc<LaserShootCooldownComponent>()
                .End();

            var playerInputPool = systems.ECSWorld.GetPool<PlayerInputComponent>();
            var laserBulletPool = systems.ECSWorld.GetPool<LaserBulletComponent>();
            var rigidbody2dPool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var playerLaserShootPool = systems.ECSWorld.GetPool<PlayerLaserShootComponent>();
            var laserShootCooldownPool = systems.ECSWorld.GetPool<LaserShootCooldownComponent>();
            var autoDestroyPool = systems.ECSWorld.GetPool<AutoDestroyComponent>();
            var nonMovablePool = systems.ECSWorld.GetPool<NonMovableComponent>();

            var playerConfig = systems.GetSharedData<ConfigContainer>().PlayerConfig;

            foreach (var entity in filter)
            {
                ref var playerInput = ref playerInputPool.Get(entity);
                ref var playerLaserShoot = ref playerLaserShootPool.Get(entity);
                if (!playerInput.secondaryShootValue) continue;
                if (playerLaserShoot.chargesCount <= 0) continue;

                ref var playerRB2D = ref rigidbody2dPool.Get(entity);

                var bulletGO = Object.Instantiate(playerConfig.LaserGunConfig.LaserBulletPrefab, playerRB2D.rb2d.transform);
                var bulletRb2D = bulletGO.GetComponent<Rigidbody2D>();

                var bulletEntity = systems.ECSWorld.AddEntity();

                ref var laserBulletComponent = ref laserBulletPool.Add(bulletEntity);
                laserBulletComponent.layerMask = playerConfig.LaserGunConfig.LaserBulletLayerMask;
                laserBulletComponent.areaSize = playerConfig.LaserGunConfig.LaserAreaSize;

                ref var rigidBody2DComponent = ref rigidbody2dPool.Add(bulletEntity);
                rigidBody2DComponent.rb2d = bulletRb2D;
                rigidBody2DComponent.position = playerRB2D.position;

                ref var autoDestroyComponent = ref autoDestroyPool.Add(bulletEntity);
                autoDestroyComponent.lifetime = playerConfig.LaserGunConfig.LaserBulletLifetime;

                nonMovablePool.Add(bulletEntity);

                ref var shootCooldown = ref laserShootCooldownPool.Add(entity);
                shootCooldown.value = playerConfig.LaserGunConfig.LaserBulletLifetime;

                playerLaserShoot.chargesCount--;
            }
        }
    }
}