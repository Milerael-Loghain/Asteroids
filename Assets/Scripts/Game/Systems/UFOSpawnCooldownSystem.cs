using Asteroids.Framework;
using Asteroids.Game.Components;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class UFOSpawnCooldownSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<UFOSpawnerComponent>()
                .Inc<UFOSpawnCooldownComponent>()
                .End();

            var ufoSpawnCooldownPool = systems.ECSWorld.GetPool<UFOSpawnCooldownComponent>();

            foreach (var entity in filter)
            {
                ref var spawnCooldown = ref ufoSpawnCooldownPool.Get(entity);
                spawnCooldown.Value -= Time.deltaTime;

                if (spawnCooldown.Value <= 0)
                {
                    ufoSpawnCooldownPool.Remove(entity);
                }
            }
        }
    }
}