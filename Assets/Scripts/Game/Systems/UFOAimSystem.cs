using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;

namespace Asteroids.Game.Systems
{
    public class UFOAimSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var playerFilter = systems.ECSWorld.Filter<PlayerComponent>()
                .Inc<Rigidbody2DComponent>()
                .End();

            var ufoFilter = systems.ECSWorld.Filter<UFOComponent>()
                .Inc<VelocityComponent>()
                .Inc<Rigidbody2DComponent>()
                .End();

            var rigidbody2DReferencePool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();

            var ufoConfig = systems.GetSharedData<ConfigContainer>().UfoConfig;

            foreach (var playerEntity in playerFilter)
            {
                ref var playerRB2d = ref rigidbody2DReferencePool.Get(playerEntity);
                var playerPosition = playerRB2d.position;

                foreach (var ufoEntity in ufoFilter)
                {
                    ref var ufoRB2d = ref rigidbody2DReferencePool.Get(ufoEntity);
                    var ufoPosition = ufoRB2d.position;
                    var direction = (playerPosition - ufoPosition).normalized;
                    var speed = direction * ufoConfig.UfoVelocity;

                    ref var ufoVelocity = ref velocityPool.Get(ufoEntity);
                    ufoVelocity.value = speed;
                }
            }
        }
    }
}