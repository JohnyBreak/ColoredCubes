using InputSystem;
using UnityEngine;

namespace _Project._Code
{
    // запуститься проинитить все,
    // подгрузить конфиги с аддрессаблов
    // прочитать конфиги 
    // создать кубы, проинитить их
    
    // обновлять визуал по нажатию кнопок
    
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        
        private void Start()
        {
            
        }

        private void Update()
        {
            var movement = _inputReader.GetMovement();
            if (movement == Vector2.zero)
            {
                return;
            }
            
            Debug.Log(DirectionCorrector.GetDirection(movement).ToString());
        }
    }
}