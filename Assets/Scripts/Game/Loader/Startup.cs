using Asteroids.Framework;
using Asteroids.Game.Data;
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

            _initSystems = new ECSSystems(_ecsWorld, _configContainer);
            _initSystems.Init();

            _updateSystems = new ECSSystems(_ecsWorld, _configContainer);

            _fixedUpdateSystems = new ECSSystems(_ecsWorld, _configContainer);
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