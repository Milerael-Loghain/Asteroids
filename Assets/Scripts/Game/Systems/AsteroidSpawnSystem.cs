using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using Asteroids.Scripts.Game.MonoBehaviours;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class AsteroidSpawnSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<AsteroidSpawner>()
                .Exc<AsteroidsSpawnCooldown>()
                .End();

            var asteroidsSpawnCooldownPool = systems.ECSWorld.GetPool<AsteroidsSpawnCooldown>();

            var asteroidPool = systems.ECSWorld.GetPool<AsteroidComponent>();
            var rigidbody2dPool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var wrapAroundPool = systems.ECSWorld.GetPool<WrapAroundComponent>();
            var rendererPool = systems.ECSWorld.GetPool<RendererReferenceComponent>();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();
            var constantVelocityPool = systems.ECSWorld.GetPool<ConstantVelocityComponent>();

            var asteroidsConfig = systems.GetSharedData<ConfigContainer>().AsteroidsConfig;

            var cam = Camera.main;

            foreach (var spawnerEntity in filter)
            {
                var velocityDirection = Random.insideUnitCircle.normalized;

                var spawnXViewport = Random.Range(1.05f, 1.1f);
                var spawnYViewport = Random.Range(1.05f, 1.1f);
                var worldPosition = cam.ViewportToWorldPoint(new Vector3(spawnXViewport, spawnYViewport, cam.nearClipPlane));
                worldPosition.x *= -Mathf.Sign(velocityDirection.x);
                worldPosition.y *= -Mathf.Sign(velocityDirection.y);
                worldPosition.z = 0;

                var asteroidGO = Object.Instantiate(asteroidsConfig.WholeAsteroidPrefab, worldPosition, Quaternion.identity);
                var asteroidRB2d = asteroidGO.GetComponent<Rigidbody2D>();

                var asteroidEntity = systems.ECSWorld.AddEntity();
                var entityReference = asteroidGO.AddComponent<EntityReference>();
                entityReference.Entity = asteroidEntity;

                asteroidPool.Add(asteroidEntity);
                constantVelocityPool.Add(asteroidEntity);

                ref var wrapAroundComponent = ref wrapAroundPool.Add(asteroidEntity);
                wrapAroundComponent.isWrappingX = true;
                wrapAroundComponent.isWrappingY = true;

                ref var rendererComponent = ref rendererPool.Add(asteroidEntity);
                rendererComponent.value = asteroidGO.GetComponent<Renderer>();

                ref var rigidBody2DComponent = ref rigidbody2dPool.Add(asteroidEntity);
                rigidBody2DComponent.rb2d = asteroidRB2d;
                rigidBody2DComponent.position = worldPosition;

                ref var velocityComponent = ref velocityPool.Add(asteroidEntity);
                velocityComponent.value = asteroidsConfig.WholeAsteroidVelocity * velocityDirection;

                ref var spawnCooldown = ref asteroidsSpawnCooldownPool.Add(spawnerEntity);
                spawnCooldown.Value = asteroidsConfig.AsteroidSpawnCooldown;
            }
        }
    }
}