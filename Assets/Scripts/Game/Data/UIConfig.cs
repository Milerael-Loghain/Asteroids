using Asteroids.Game.MonoBehaviours;
using UnityEngine;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "UI Config")]
    public class UIConfig : ScriptableObject
    {
        public PlayerHud PlayerHudPrefab => _playerHudPrefab;

        [SerializeField]
        private PlayerHud _playerHudPrefab;
    }
}