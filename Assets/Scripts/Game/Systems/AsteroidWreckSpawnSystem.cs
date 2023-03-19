using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using Asteroids.Game.MonoBehaviours;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class AsteroidWreckSpawnSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<AsteroidComponent>()
                .Inc<DestroyComponent>()
                .End();

            var rigidbody2dPool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var wrapAroundPool = systems.ECSWorld.GetPool<WrapAroundComponent>();
            var rendererPool = systems.ECSWorld.GetPool<RendererReferenceComponent>();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();
            var constantVelocityPool = systems.ECSWorld.GetPool<ConstantVelocityComponent>();
            var playerDamagePool = systems.ECSWorld.GetPool<PlayerDamageComponent>();
            var scoreBearerPool = systems.ECSWorld.GetPool<ScoreBearerComponent>();

            var asteroidsConfig = systems.GetSharedData<ConfigContainer>().AsteroidsConfig;

            foreach (var destroyedEntity in filter)
            {
                ref var originalRigidbody = ref rigidbody2dPool.Get(destroyedEntity);

                for (int i = 0; i < 2; i++)
                {
                    var velocityDirection = Random.insideUnitCircle.normalized;

                    var asteroidGO = Object.Instantiate(asteroidsConfig.AsteroidWreckPrefab, originalRigidbody.position, Quaternion.identity);
                    var asteroidRB2d = asteroidGO.GetComponent<Rigidbody2D>();

                    var asteroidWreckEntity = systems.ECSWorld.AddEntity();

                    var entityReference = asteroidGO.AddComponent<EntityReference>();
                    entityReference.Entity = asteroidWreckEntity;

                    constantVelocityPool.Add(asteroidWreckEntity);

                    ref var wrapAroundComponent = ref wrapAroundPool.Add(asteroidWreckEntity);
                    wrapAroundComponent.isWrappingX = true;
                    wrapAroundComponent.isWrappingY = true;

                    ref var rendererComponent = ref rendererPool.Add(asteroidWreckEntity);
                    rendererComponent.value = asteroidGO.GetComponent<Renderer>();

                    ref var rigidBody2DComponent = ref rigidbody2dPool.Add(asteroidWreckEntity);
                    rigidBody2DComponent.rb2d = asteroidRB2d;
                    rigidBody2DComponent.position = originalRigidbody.position;

                    ref var velocityComponent = ref velocityPool.Add(asteroidWreckEntity);
                    velocityComponent.value = asteroidsConfig.AsteroidWreckVelocity * velocityDirection;

                    ref var playerDamageComponent = ref playerDamagePool.Add(asteroidWreckEntity);
                    playerDamageComponent.damageRadius = asteroidsConfig.AsteroidWreckDamageRadius;

                    scoreBearerPool.Add(asteroidWreckEntity);
                }
            }
        }
    }
}