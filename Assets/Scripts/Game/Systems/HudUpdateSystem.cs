using Asteroids.Framework;
using Asteroids.Game.Components;

namespace Asteroids.Game.Systems
{
    public class HudUpdateSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var playerFilter = systems.ECSWorld.Filter<PlayerComponent>()
                .Inc<Rigidbody2DComponent>()
                .Inc<VelocityComponent>()
                .End();

            var hudFilter = systems.ECSWorld.Filter<HudReferenceComponent>().End();

            var rigidbody2dPool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();
            var hudPool = systems.ECSWorld.GetPool<HudReferenceComponent>();

            foreach (var playerEntity in playerFilter)
            {
                ref var playerRb2d = ref rigidbody2dPool.Get(playerEntity);
                var position = playerRb2d.rb2d.position;
                var rotation = playerRb2d.rb2d.rotation;
                ref var velocityComponent = ref velocityPool.Get(playerEntity);
                var velocity = velocityComponent.value;

                foreach (var hudEntity in hudFilter)
                {
                    ref var hud = ref hudPool.Get(hudEntity);
                    hud.value.UpdateData(position, rotation, velocity, 0, 0);
                }
            }
        }
    }
}