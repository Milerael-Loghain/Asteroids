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
            var playerFilter = systems.ECSWorld.Filter<PlayerComponent>().Inc<DestroyComponent>().End();

            foreach (var playerEntity in playerFilter)
            {
                Time.timeScale = 0;

                var uiConfig = systems.GetSharedData<ConfigContainer>().UIConfig;
                Object.Instantiate(uiConfig.GameOverScreenPrefab);
            }
        }
    }
}