using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using Asteroids.Scripts.Game.MonoBehaviours;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class PlayerInitSystem : IECSInitSystem
    {
        public void Init(IECSSystems systems)
        {
            var playerConfig = systems.GetSharedData<ConfigContainer>().PlayerConfig;

            var rigidbody2DReferencePool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var playerComponentPool = systems.ECSWorld.GetPool<PlayerComponent>();
            var velocityComponentPool = systems.ECSWorld.GetPool<VelocityComponent>();
            var rotationComponentPool = systems.ECSWorld.GetPool<RotationComponent>();
            var rendererComponentPool = systems.ECSWorld.GetPool<RendererReferenceComponent>();
            var wrapAroundComponentPool = systems.ECSWorld.GetPool<WrapAroundComponent>();

            var playerEntity = systems.ECSWorld.AddEntity();
            var playerGO = Object.Instantiate(playerConfig.PlayerPrefab);
            var playerRB2D = playerGO.GetComponent<Rigidbody2D>();

            var entityReference = playerGO.AddComponent<EntityReference>();
            entityReference.Entity = playerEntity;

            ref var rigidBody2DComponent = ref rigidbody2DReferencePool.Add(playerEntity);
            rigidBody2DComponent.rb2d = playerRB2D;

            playerComponentPool.Add(playerEntity);
            velocityComponentPool.Add(playerEntity);
            rotationComponentPool.Add(playerEntity);
            wrapAroundComponentPool.Add(playerEntity);

            ref var rendererComponent = ref rendererComponentPool.Add(playerEntity);
            rendererComponent.value = playerGO.GetComponent<Renderer>();
        }
    }
}