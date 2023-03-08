using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;

namespace Asteroids.Game.Systems
{
    public class VelocitySlowdownSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<VelocityComponent>().Exc<ConstantVelocityComponent>().End();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();
            var spaceConfig = systems.GetSharedData<ConfigContainer>().SpaceConfig;

            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity);

                velocity.value *= spaceConfig.AccelerationSlowdown;
            }
        }
    }
}