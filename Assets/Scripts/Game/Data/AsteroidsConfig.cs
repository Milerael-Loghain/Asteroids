using UnityEngine;

namespace Asteroids.Game.Data
{

    [CreateAssetMenu(fileName = "Asteroids Config")]
    public class AsteroidsConfig : ScriptableObject
    {
        public GameObject WholeAsteroidPrefab => _wholeAsteroidPrefab;

        public GameObject AsteroidWreckPrefab => _asteroidWreckPrefab;

        public float WholeAsteroidVelocity => _wholeAsteroidVelocity;

        public float AsteroidWreckVelocity => _asteroidWreckVelocity;

        public float AsteroidSpawnCooldown => _asteroidSpawnCooldown;

        [SerializeField]
        private GameObject _wholeAsteroidPrefab;

        [SerializeField]
        private GameObject _asteroidWreckPrefab;

        [SerializeField]
        private float _wholeAsteroidVelocity;

        [SerializeField]
        private float _asteroidWreckVelocity;

        [SerializeField]
        private float _asteroidSpawnCooldown;
    }
}