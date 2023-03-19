using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class GameOverSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var playerFilter = systems.ECSWorld.Filter<PlayerComponent>().Inc<PlayerScoreComponent>().Inc<DestroyComponent>().End();

            var playerScorePool = systems.ECSWorld.GetPool<PlayerScoreComponent>();

            foreach (var playerEntity in playerFilter)
            {
                Time.timeScale = 0;

                ref var playerScore = ref playerScorePool.Get(playerEntity);
                var uiConfig = systems.GetSharedData<ConfigContainer>().UIConfig;
                var gameOverScreen = Object.Instantiate(uiConfig.GameOverScreenPrefab);
                gameOverScreen.Init(playerScore.value);
            }
        }
    }
}