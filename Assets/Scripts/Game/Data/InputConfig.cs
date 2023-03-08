using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Game.Data
{
    [CreateAssetMenu(fileName = "Input Config")]
    public class InputConfig : ScriptableObject
    {
        public InputActionReference MoveInputActionReference => moveInputActionReference;

        public InputActionReference PrimaryShootInputActionReference => primaryShootInputActionReference;

        public InputActionReference SecondaryShootInputActionReference => secondaryShootInputActionReference;

        [SerializeField]
        private InputActionReference moveInputActionReference;

        [SerializeField]
        private InputActionReference primaryShootInputActionReference;

        [SerializeField]
        private InputActionReference secondaryShootInputActionReference;
    }
}