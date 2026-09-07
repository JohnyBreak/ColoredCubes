using _Project._Code._Common;
using _Project._Code._Grid;
using InputSystem;
using UnityEngine;

namespace _Project._Code
{
    // запуститься проинитить все,
    // подгрузить конфиги с аддрессаблов
    // прочитать конфиги 
    // создать кубы, проинитить их
    // обновить положение камеры
    
    // обновлять визуал по нажатию кнопок
    
    public class Bootstrap : MonoBehaviour
    {
        private const string LogKey = "Bootstrap";
        
        [SerializeField] private InputReader _inputReader;
        
        private AssetProvider _assetProvider = new AssetProvider();
        private ConfigFacade _configFacade;
        
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _configFacade = new ConfigFacade(_assetProvider);
            
            _configFacade.Init();

            if (!_configFacade.IsValid())
            {
                return;
            }

            var controller = new GridController(
                new GridControllerParams(
                    _configFacade.GridData,
                    _assetProvider,
                    _configFacade.SettingsConfigDto,
                    Vector3.zero));
            
            controller.Init();
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

        private void OnDestroy()
        {
            _assetProvider?.Dispose();
        }
    }
}