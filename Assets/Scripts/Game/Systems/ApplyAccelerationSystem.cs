using Asteroids.Framework;
using Asteroids.Game.Components;

namespace Asteroids.Game.Systems
{
    public class ApplyAccelerationSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<AccelerationComponent>().Inc<VelocityComponent>().End();
            var accelerationPool = systems.ECSWorld.GetPool<AccelerationComponent>();
            var velocityPool = systems.ECSWorld.GetPool<VelocityComponent>();

            foreach (var entity in filter)
            {
                ref var acceleration = ref accelerationPool.Get(entity);
                ref var velocity = ref velocityPool.Get(entity);

                velocity.value += acceleration.value;
            }
        }
    }
}