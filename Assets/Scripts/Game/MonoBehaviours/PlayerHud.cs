using TMPro;
using UnityEngine;

namespace Asteroids.Game.MonoBehaviours
{
    public class PlayerHud : MonoBehaviour
    {
        private readonly string DataFormat =
            " Position: {0} \n Rotation: {1} \n Velocity: {2} \n LaserCount: {3} \n LaserCooldown: {4}";

        [SerializeField]
        private TextMeshProUGUI _textComponent;

        public void UpdateData(Vector2 position, float rotation, Vector2 velocity, int laserCount, float laserCooldown)
        {
            _textComponent.text = string.Format(DataFormat, position, rotation, velocity, laserCount, laserCooldown);
        }
    }
}