using UnityEngine;

namespace InputSystem
{
    public enum InputDirection
    {
        None = 0,
        Up = 1,
        Right = 2,
        Down = 3,
        Left = 4
    }

    public static class DirectionCorrector
    {
        private const float _threshold = .85f;
        
        public static InputDirection GetDirection(Vector2 input)
        {
            if (input == Vector2.zero)
            {
                return InputDirection.None;
            }
            
            if (input.y > _threshold)
            {
                return InputDirection.Up;
            }
            
            if (input.x > _threshold)
            {
                return InputDirection.Right;
            }
            
            if (input.y < -_threshold)
            {
                return InputDirection.Down;
            }
            
            if (input.x < -_threshold)
            {
                return InputDirection.Left;
            }
            
            return InputDirection.None;
        }
    }
}