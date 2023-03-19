using UnityEngine;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public LayerMask PlayerLayers => _playerLayers;

        public GameObject PlayerPrefab => _playerPrefab;

        public float Acceleration => _acceleration;

        public float RotationVelocity => rotationVelocity;

        public PrimaryGunConfig PrimaryGunConfig => _primaryGunConfig;
        public LaserGunConfig LaserGunConfig => _laserGunConfig;

        [SerializeField]
        private LayerMask _playerLayers;

        [SerializeField]
        private GameObject _playerPrefab;

        [SerializeField]
        private float _acceleration;

        [SerializeField]
        private float rotationVelocity;

        [SerializeField]
        private PrimaryGunConfig _primaryGunConfig;

        [SerializeField]
        private LaserGunConfig _laserGunConfig;
    }
}