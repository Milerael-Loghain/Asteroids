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
                .Inc<PlayerLaserShootComponent>()
                .End();

            var hudFilter = systems.ECSWorld.Filter<HudReferenceComponent>().End();

            var rigidbody2dPool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();
            var hudPool = systems.ECSWorld.GetPool<HudReferenceComponent>();
            var playerLaserShootComponentPool = systems.ECSWorld.GetPool<PlayerLaserShootComponent>();

            foreach (var playerEntity in playerFilter)
            {
                ref var playerRb2d = ref rigidbody2dPool.Get(playerEntity);
                var position = playerRb2d.rb2d.position;
                var rotation = playerRb2d.rb2d.rotation;
                ref var velocityComponent = ref velocityPool.Get(playerEntity);
                var velocity = velocityComponent.value;
                ref var laserShootComponentSystem = ref playerLaserShootComponentPool.Get(playerEntity);
                var laserChargesCount = laserShootComponentSystem.chargesCount;
                var laserChargesCooldown = laserShootComponentSystem.cooldown;

                foreach (var hudEntity in hudFilter)
                {
                    ref var hud = ref hudPool.Get(hudEntity);
                    hud.value.UpdateData(position, rotation, velocity, laserChargesCount, laserChargesCooldown);
                }
            }
        }
    }
}