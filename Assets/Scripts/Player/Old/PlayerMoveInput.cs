using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Old
{
    public class PlayerMoveInput : MonoBehaviour
    {
        private Vector2 _movementInput;

        [SerializeField] private float movementSpeed;

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
        }
    }
}