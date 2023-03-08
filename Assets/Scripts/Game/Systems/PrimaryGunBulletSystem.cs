using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Scripts.Game.MonoBehaviours;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class PrimaryGunBulletSystem : IECSRunSystem
    {
        private readonly Collider2D[] result = new Collider2D[1];

        public void Run(IECSSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.ECSWorld;
            var filter = ecsWorld.Filter<BulletComponent>().Inc<Rigidbody2DComponent>().End();
            var basicBulletPool = ecsWorld.GetPool<BulletComponent>();
            var rigidbody2DPool = ecsWorld.GetPool<Rigidbody2DComponent>();
            var destroyPool = ecsWorld.GetPool<DestroyComponent>();

            foreach (var entity in filter)
            {
                ref var basicBullet = ref basicBulletPool.Get(entity);
                ref var rigidbody2DComponent = ref rigidbody2DPool.Get(entity);

                var resultsCount = Physics2D.OverlapCircleNonAlloc(rigidbody2DComponent.position, basicBullet.radius, result, basicBullet.layerMask);
                if (resultsCount > 0)
                {
                    var resultCollider = result[0];
                    if (resultCollider == null) continue;
                    var collisionGO = resultCollider.gameObject.GetComponent<EntityReference>();

                    destroyPool.Add(entity);
                    destroyPool.Add(collisionGO.Entity);
                }
            }
        }
    }
}