using Asteroids.Framework;
using Asteroids.Game.Components;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class PrimaryShootCooldownSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<PlayerComponent>()
                .Inc<PrimaryShootCooldownComponent>()
                .End();

            var shootCooldownPool = systems.ECSWorld.GetPool<PrimaryShootCooldownComponent>();

            foreach (var entity in filter)
            {
                ref var spawnCooldown = ref shootCooldownPool.Get(entity);
                spawnCooldown.value -= Time.deltaTime;

                if (spawnCooldown.value <= 0)
                {
                    shootCooldownPool.Remove(entity);
                }
            }
        }
    }
}