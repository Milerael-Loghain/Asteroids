using Asteroids.Framework;
using Asteroids.Game.Components;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class AsteroidSpawnCooldownSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<AsteroidSpawner>()
                .Inc<AsteroidsSpawnCooldown>()
                .End();

            var asteroidsSpawnCooldownPool = systems.ECSWorld.GetPool<AsteroidsSpawnCooldown>();

            foreach (var entity in filter)
            {
                ref var spawnCooldown = ref asteroidsSpawnCooldownPool.Get(entity);
                spawnCooldown.Value -= Time.deltaTime;

                if (spawnCooldown.Value <= 0)
                {
                    asteroidsSpawnCooldownPool.Remove(entity);
                }
            }
        }
    }
}