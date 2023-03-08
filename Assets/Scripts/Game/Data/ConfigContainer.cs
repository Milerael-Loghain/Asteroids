using UnityEngine;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "Config Container")]
    public class ConfigContainer : ScriptableObject
    {
        public InputConfig InputConfig => _inputConfig;

        public SpaceConfig SpaceConfig => _spaceConfig;

        public PlayerConfig PlayerConfig => _playerConfig;

        public AsteroidsConfig AsteroidsConfig => _asteroidsConfig;

        public UFOConfig UfoConfig => _ufoConfig;

        [SerializeField]
        private InputConfig _inputConfig;

        [SerializeField]
        private SpaceConfig _spaceConfig;

        [SerializeField]
        private PlayerConfig _playerConfig;

        [SerializeField]
        private AsteroidsConfig _asteroidsConfig;

        [SerializeField]
        private UFOConfig _ufoConfig;
    }
}