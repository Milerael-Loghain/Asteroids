using Asteroids.Framework;
using Asteroids.Game.Components;
using Asteroids.Game.Data;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class HudInitSystem : IECSInitSystem
    {
        public void Init(IECSSystems systems)
        {
            var uiConfig = systems.GetSharedData<ConfigContainer>().UIConfig;
            var playerHudInstance = Object.Instantiate(uiConfig.PlayerHudPrefab);

            var hudEntity = systems.ECSWorld.AddEntity();
            var hudPool = systems.ECSWorld.GetPool<HudReferenceComponent>();
            ref var hud = ref hudPool.Add(hudEntity);
            hud.value = playerHudInstance;
        }
    }
}