using Asteroids.Framework;
using Asteroids.Game.Components;

namespace Asteroids.Game.Systems
{
    public class ApplyPositionSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<Rigidbody2DComponent>().End();
            var rigidbody2DReferencePool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();


            foreach (var entity in filter)
            {
                ref var rigidbody2DReference = ref rigidbody2DReferencePool.Get(entity);
                rigidbody2DReference.rb2d.MovePosition(rigidbody2DReference.position);
            }
        }
    }
}