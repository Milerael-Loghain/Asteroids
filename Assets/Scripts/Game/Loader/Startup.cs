using Asteroids.Framework;
using Asteroids.Game.Data;
using Asteroids.Game.Systems;
using UnityEngine;

namespace Asteroids.Game.Loader
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private ConfigContainer _configContainer;

        private ECSWorld _ecsWorld;
        private IECSSystems _initSystems;
        private IECSSystems _updateSystems;
        private IECSSystems _fixedUpdateSystems;

        private void Awake()
        {
            _ecsWorld = new ECSWorld();

            _initSystems = new ECSSystems(_ecsWorld, _configContainer)
                .Add(new PlayerInitSystem())
                .Add(new PlayerInputInitSystem())
                .Add(new AsteroidSpawnInitSystem())
                .Add(new UFOSpawnInitSystem());

            _initSystems.Init();

            _updateSystems = new ECSSystems(_ecsWorld, _configContainer)
                .Add(new PlayerInputSystem())
                .Add(new AsteroidSpawnSystem())
                .Add(new PlayerPrimaryShootSystem())
                .Add(new AsteroidSpawnCooldownSystem())
                .Add(new PrimaryShootCooldownSystem())
                .Add(new AsteroidWreckSpawnSystem())
                .Add(new UFOSpawnSystem())
                .Add(new UFOSpawnCooldownSystem())
                .Add(new AutoDestroySystem())
                .Add(new ObjectDestroySystem());

            _fixedUpdateSystems = new ECSSystems(_ecsWorld, _configContainer)
                .Add(new PlayerMovementSystem())
                .Add(new ApplyAccelerationSystem())
                .Add(new UFOAimSystem())
                .Add(new VelocitySlowdownSystem())
                .Add(new ApplyVelocitySystem())
                .Add(new WrapAroundTeleportationSystem())
                .Add(new RotationSystem())
                .Add(new ApplyPositionSystem())
                .Add(new PrimaryGunBulletSystem());
        }

        private void Update()
        {
            _updateSystems.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems.Run();
        }
    }
}