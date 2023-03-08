using UnityEngine;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "Space Config")]
    public class SpaceConfig : ScriptableObject
    {
        public float AccelerationSlowdown => _accelerationSlowdown;

        [SerializeField]
        [Range(0, 1)]
        private float _accelerationSlowdown;
    }
}