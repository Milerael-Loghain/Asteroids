using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using Asteroids.Game.MonoBehaviours;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class UFOSpawnSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<UFOSpawnerComponent>()
                .Exc<UFOSpawnCooldownComponent>()
                .End();

            var ufoSpawnCooldownPool = systems.ECSWorld.GetPool<UFOSpawnCooldownComponent>();

            var ufoPool = systems.ECSWorld.GetPool<UFOComponent>();
            var rigidbody2dPool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var wrapAroundPool = systems.ECSWorld.GetPool<WrapAroundComponent>();
            var rendererPool = systems.ECSWorld.GetPool<RendererReferenceComponent>();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();
            var constantVelocityPool = systems.ECSWorld.GetPool<ConstantVelocityComponent>();

            var ufoConfig = systems.GetSharedData<ConfigContainer>().UfoConfig;

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

                var ufoGO = Object.Instantiate(ufoConfig.UfoPrefab, worldPosition, Quaternion.identity);
                var UFORB2d = ufoGO.GetComponent<Rigidbody2D>();

                var ufoEntity = systems.ECSWorld.AddEntity();
                var entityReference = ufoGO.AddComponent<EntityReference>();
                entityReference.Entity = ufoEntity;

                ufoPool.Add(ufoEntity);
                constantVelocityPool.Add(ufoEntity);

                ref var wrapAroundComponent = ref wrapAroundPool.Add(ufoEntity);
                wrapAroundComponent.isWrappingX = true;
                wrapAroundComponent.isWrappingY = true;

                ref var rendererComponent = ref rendererPool.Add(ufoEntity);
                rendererComponent.value = ufoGO.GetComponent<Renderer>();

                ref var rigidBody2DComponent = ref rigidbody2dPool.Add(ufoEntity);
                rigidBody2DComponent.rb2d = UFORB2d;
                rigidBody2DComponent.position = worldPosition;

                velocityPool.Add(ufoEntity);

                ref var spawnCooldown = ref ufoSpawnCooldownPool.Add(spawnerEntity);
                spawnCooldown.Value = ufoConfig.UfoSpawnCooldown;
            }
        }
    }
}