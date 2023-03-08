using Asteroids.Framework;
using Asteroids.Game.Components;
using UnityEngine;

namespace Asteroids.Game.Systems
{
    public class WrapAroundTeleportationSystem : IECSRunSystem
    {
        public void Run(IECSSystems systems)
        {
            var filter = systems.ECSWorld.Filter<WrapAroundComponent>()
                .Inc<Rigidbody2DComponent>()
                .Inc<RendererReferenceComponent>()
                .End();

            var rigidbody2DReferencePool = systems.ECSWorld.GetPool<Rigidbody2DComponent>();
            var wrapAroundPool = systems.ECSWorld.GetPool<WrapAroundComponent>();
            var rendererPool = systems.ECSWorld.GetPool<RendererReferenceComponent>();

            var cam = Camera.main;

            foreach (var entity in filter)
            {
                ref var wrapAroundComponent = ref wrapAroundPool.Get(entity);
                ref var rendererComponent = ref rendererPool.Get(entity);
                ref var rigidbody2DReference = ref rigidbody2DReferencePool.Get(entity);

                var viewportPosition = cam.WorldToViewportPoint(rigidbody2DReference.position);
                var newPosition = rigidbody2DReference.position;

                if (rendererComponent.value.isVisible)
                {
                    wrapAroundComponent.isWrappingX = false;
                    wrapAroundComponent.isWrappingY = false;
                    continue;
                }

                var isXWrapping = viewportPosition.x is > 1 or < 0;
                var isYWrapping = viewportPosition.y is > 1 or < 0;

                if (!isYWrapping && !isXWrapping) continue;

                if (isXWrapping && !wrapAroundComponent.isWrappingX)
                {
                    newPosition.x = -newPosition.x;
                    wrapAroundComponent.isWrappingX = true;
                }
                if (isYWrapping && !wrapAroundComponent.isWrappingY)
                {
                    newPosition.y = -newPosition.y;
                    wrapAroundComponent.isWrappingY = true;
                }

                rigidbody2DReference.position = newPosition;
            }
        }
    }
}