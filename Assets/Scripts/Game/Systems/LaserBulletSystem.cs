using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.MonoBehaviours;
using UnityEditor;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class LaserBulletSystem : IECSRunSystem
    {
        private readonly Collider2D[] result = new Collider2D[10];

        public void Run(IECSSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.ECSWorld;
            var filter = ecsWorld.Filter<LaserBulletComponent>().Inc<Rigidbody2DComponent>().End();
            var laserBulletPool = ecsWorld.GetPool<LaserBulletComponent>();
            var rigidbody2DPool = ecsWorld.GetPool<Rigidbody2DComponent>();
            var destroyPool = ecsWorld.GetPool<DestroyComponent>();

            foreach (var entity in filter)
            {
                ref var laserBullet = ref laserBulletPool.Get(entity);
                ref var rigidbody2DComponent = ref rigidbody2DPool.Get(entity);
                var areaStartPoint = new Vector2(rigidbody2DComponent.rb2d.position.x - laserBullet.areaSize.x / 2, rigidbody2DComponent.rb2d.position.y);
                var areaEndPoint = new Vector2(rigidbody2DComponent.rb2d.position.x + laserBullet.areaSize.x / 2, rigidbody2DComponent.rb2d.position.y + laserBullet.areaSize.y);

                var rotation = Quaternion.AngleAxis(rigidbody2DComponent.rb2d.rotation, Vector3.forward);

                var areaStartPointDirection = (areaStartPoint - rigidbody2DComponent.rb2d.position);
                var areaEndPointDirection = (areaEndPoint - rigidbody2DComponent.rb2d.position);

                areaStartPointDirection = rotation * areaStartPointDirection;
                areaEndPointDirection = rotation * areaEndPointDirection;

                areaStartPoint = rigidbody2DComponent.rb2d.position + areaStartPointDirection;
                areaEndPoint = rigidbody2DComponent.rb2d.position + areaEndPointDirection;

                var resultsCount = Physics2D.OverlapAreaNonAlloc(areaStartPoint, areaEndPoint, result, laserBullet.layerMask);
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