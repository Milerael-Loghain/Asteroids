using Asteroids.Framework;
using Asteroids.Game.Components;

namespace Asteroids.Game.Systems
{
    public class AsteroidSpawnInitSystem : IECSInitSystem
    {
        public void Init(IECSSystems systems)
        {
            var asteroidSpawnerPool = systems.ECSWorld.GetPool<AsteroidSpawner>();
            var asteroidSpawner = systems.ECSWorld.AddEntity();
            asteroidSpawnerPool.Add(asteroidSpawner);
        }
    }
}