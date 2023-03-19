using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Game.Systems
{
    public class PlayerInputSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var configContainer = systems.GetSharedData<ConfigContainer>();
            var inputConfig = configContainer.InputConfig;

            InputSystem.Update();

            var filter = systems.ECSWorld.Filter<PlayerInputComponent>().End();
            var playerInputComponentPool = systems.ECSWorld.GetPool<PlayerInputComponent>();
            foreach (var entity in filter)
            {
                ref var playerInput = ref playerInputComponentPool.Get(entity);
                playerInput.moveValue = inputConfig.MoveInputActionReference.action.ReadValue<Vector2>();
                playerInput.primaryShootValue = inputConfig.PrimaryShootInputActionReference.action.triggered;
                playerInput.secondaryShootValue = inputConfig.SecondaryShootInputActionReference.action.triggered;
            }
        }
    }
}