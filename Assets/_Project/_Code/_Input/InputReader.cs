using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        private Vector2 _moveComposite;
        private Controls _controls;

        public Vector2 GetMovement()
        {
            return _moveComposite;
        }

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }

            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            _moveComposite = context.ReadValue<Vector2>();
        }
    }
}
