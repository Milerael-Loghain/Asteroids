using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using Asteroids.Game.MonoBehaviours;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class PlayerDamageSystem : IECSRunSystem
    {
        private readonly Collider2D[] result = new Collider2D[1];

        public void Run(IECSSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.ECSWorld;
            var filter = ecsWorld.Filter<PlayerDamageComponent>().Inc<Rigidbody2DComponent>().End();
            var playerDamagePool = ecsWorld.GetPool<PlayerDamageComponent>();
            var rigidbody2DPool = ecsWorld.GetPool<Rigidbody2DComponent>();
            var destroyPool = ecsWorld.GetPool<DestroyComponent>();

            var playerConfig = ecsSystems.GetSharedData<ConfigContainer>().PlayerConfig;

            foreach (var entity in filter)
            {
                ref var playerDamageComponent = ref playerDamagePool.Get(entity);
                ref var rigidbody2DComponent = ref rigidbody2DPool.Get(entity);

                var resultsCount = Physics2D.OverlapCircleNonAlloc(rigidbody2DComponent.position, playerDamageComponent.damageRadius, result, playerConfig.PlayerLayers);
                if (resultsCount > 0)
                {
                    var resultCollider = result[0];
                    if (resultCollider == null) continue;
                    var collisionGO = resultCollider.gameObject.GetComponent<EntityReference>();

                    destroyPool.Add(collisionGO.Entity);
                }
            }
        }
    }
}