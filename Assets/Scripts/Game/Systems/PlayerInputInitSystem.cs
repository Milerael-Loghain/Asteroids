using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;

namespace Asteroids.Game.Systems
{
    public class PlayerInputInitSystem : IECSInitSystem
    {
        public void Init(IECSSystems systems)
        {
            var configContainer = systems.GetSharedData<ConfigContainer>();
            var filter = systems.ECSWorld.Filter<PlayerComponent>().End();

            var inputConfig = configContainer.InputConfig;

            inputConfig.MoveInputActionReference.action.Enable();
            inputConfig.PrimaryShootInputActionReference.action.Enable();

            foreach (var entity in filter)
            {
                var playerInputPool = systems.ECSWorld.GetPool<PlayerInputComponent>();
                playerInputPool.Add(entity);
            }
        }
    }
}