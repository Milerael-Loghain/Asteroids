using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "Laser Gun Config")]
    public class LaserGunConfig : ScriptableObject
    {
        public GameObject LaserBulletPrefab => laserBulletPrefab;

        public LayerMask LaserBulletLayerMask => laserBulletLayerMask;

        public float LaserChargesCooldown => laserChargesCooldown;

        public float LaserBulletLifetime => _laserBulletLifetime;

        public Vector2 LaserAreaSize => _laserAreaSize;

        [SerializeField]
        private GameObject laserBulletPrefab;

        [SerializeField]
        private LayerMask laserBulletLayerMask;

        [SerializeField]
        private float laserChargesCooldown;

        [SerializeField]
        private float _laserBulletLifetime;

        [SerializeField]
        private Vector2 _laserAreaSize;
    }
}