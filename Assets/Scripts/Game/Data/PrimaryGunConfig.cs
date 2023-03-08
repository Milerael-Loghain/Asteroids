using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "Primary Gun Config")]
    public class PrimaryGunConfig : ScriptableObject
    {
        public GameObject PrimaryBulletPrefab => _primaryBulletPrefab;

        public float PrimaryBulletVelocity => _primaryBulletVelocity;

        public LayerMask PrimaryBulletLayerMask => _primaryBulletLayerMask;

        public float PrimaryBulletCooldown => _primaryBulletCooldown;

        public float PrimaryBulletLifetime => _primaryBulletLifetime;

        public float PrimaryBulletRadius => _primaryBulletRadius;

        [SerializeField]
        private GameObject _primaryBulletPrefab;

        [SerializeField]
        private float _primaryBulletVelocity;

        [SerializeField]
        private LayerMask _primaryBulletLayerMask;

        [SerializeField]
        private float _primaryBulletCooldown;

        [SerializeField]
        private float _primaryBulletLifetime;

        [SerializeField]
        private float _primaryBulletRadius;
    }
}