using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class LaserShootChargesCooldownSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<PlayerComponent>()
                .Inc<PlayerLaserShootComponent>()
                .End();

            var playerLaserShootPool = systems.ECSWorld.GetPool<PlayerLaserShootComponent>();
            var playerConfig = systems.GetSharedData<ConfigContainer>().PlayerConfig;
            var laserGunConfig = playerConfig.LaserGunConfig;

            foreach (var entity in filter)
            {
                ref var playerLaserShootComponent = ref playerLaserShootPool.Get(entity);
                playerLaserShootComponent.cooldown -= Time.deltaTime;

                if (playerLaserShootComponent.cooldown <= 0)
                {
                    playerLaserShootComponent.chargesCount++;
                    playerLaserShootComponent.cooldown = laserGunConfig.LaserChargesCooldown;
                }
            }
        }
    }
}