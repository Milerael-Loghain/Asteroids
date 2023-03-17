using Asteroids.Framework;
using Asteroids.Game.Components;

namespace Asteroids.Game.Systems
{
    public class UFOSpawnInitSystem : IECSInitSystem
    {
        public void Init(IECSSystems systems)
        {
            var ufoSpawnerPool  = systems.ECSWorld.GetPool<UFOSpawnerComponent>();
            var ufoSpawner = systems.ECSWorld.AddEntity();
            ufoSpawnerPool.Add(ufoSpawner);
        }
    }
}