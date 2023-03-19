using Asteroids.Game.MonoBehaviours;
using UnityEngine;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "UI Config")]
    public class UIConfig : ScriptableObject
    {
        public PlayerHud PlayerHudPrefab => _playerHudPrefab;

        public GameOverScreen GameOverScreenPrefab => _gameOverScreenPrefab;

        [SerializeField]
        private PlayerHud _playerHudPrefab;

        [SerializeField]
        private GameOverScreen _gameOverScreenPrefab;
    }
}