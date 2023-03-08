using Asteroids.Framework;
using Asteroids.Game.Components;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class AutoDestroySystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<AutoDestroyComponent>().End();

            var autoDestroyPool = systems.ECSWorld.GetPool<AutoDestroyComponent>();
            var destroyPool = systems.ECSWorld.GetPool<DestroyComponent>();

            foreach (var entity in filter)
            {
                ref var autoDestroy = ref autoDestroyPool.Get(entity);
                autoDestroy.lifetime -= Time.deltaTime;

                if (autoDestroy.lifetime <= 0)
                {
                    autoDestroyPool.Remove(entity);
                    destroyPool.Add(entity);
                }
            }
        }
    }
}