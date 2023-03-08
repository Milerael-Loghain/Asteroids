using UnityEngine;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "UFO Config")]
    public class UFOConfig : ScriptableObject
    {
        public GameObject UfoPrefab => _ufoPrefab;

        public float UfoVelocity => _ufoVelocity;

        [SerializeField]
        private GameObject _ufoPrefab;

        [SerializeField]
        private float _ufoVelocity;
    }
}