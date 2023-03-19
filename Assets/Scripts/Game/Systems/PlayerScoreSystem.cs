using Asteroids.Framework;
using Asteroids.Game.Components;

namespace Asteroids.Game.Systems
{
    public class PlayerScoreSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var playerFilter = systems.ECSWorld.Filter<PlayerScoreComponent>().End();
            var destroyFilter = systems.ECSWorld.Filter<DestroyComponent>().Inc<ScoreBearerComponent>().End();

            var playerScorePool = systems.ECSWorld.GetPool<PlayerScoreComponent>();

            foreach (var playerEntity in playerFilter)
            {
                ref var playerScore = ref playerScorePool.Get(playerEntity);

                foreach (var destroyedEntity in destroyFilter)
                {
                    playerScore.value++;
                }
            }
        }
    }
}