using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public Action<Vector2> MoveClickedEvent;
        
        private Vector2 _moveComposite;
        private Controls _controls;
        
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.Move.started += OnMove;
            }

            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        private void OnDestroy()
        {
            if (_controls != null)
            {
                _controls.Player.Move.started -= OnMove;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveComposite = context.ReadValue<Vector2>();
            MoveClickedEvent?.Invoke(_moveComposite);
        }
    }
}
