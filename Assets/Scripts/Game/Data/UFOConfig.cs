using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "UFO Config")]
    public class UFOConfig : ScriptableObject
    {
        public GameObject UfoPrefab => _ufoPrefab;

        public float UfoVelocity => _ufoVelocity;

        public float UfoSpawnCooldown => _ufoSpawnCooldown;

        public float DamageRadius => _damageRadius;

        [SerializeField]
        private GameObject _ufoPrefab;

        [SerializeField]
        private float _ufoVelocity;

        [SerializeField]
        private float _ufoSpawnCooldown;

        [SerializeField]
        private float _damageRadius;
    }
}