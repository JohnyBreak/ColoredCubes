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
        private GridController _gridController;
        
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (!_inputReader)
            {
                Debug.LogError($"[{LogKey}] _inputReader is null");
                return;
            }

            _inputReader.MoveClickedEvent += OnMoveClicked;
            
            _configFacade = new ConfigFacade(_assetProvider);
            
            _configFacade.Init();

            if (!_configFacade.IsValid())
            {
                return;
            }

            _gridController = new GridController(
                new GridControllerParams(
                    _configFacade.GridData,
                    _assetProvider,
                    _configFacade.SettingsConfigDto,
                    Vector3.zero));
            
            _gridController.Init();
        }

        private void OnMoveClicked(Vector2 input)
        {
            if (input == Vector2.zero)
            {
                return;
            }

            var dir = DirectionCorrector.GetDirection(input);
            
            if (dir == InputDirection.None)
            {
                return;
            }

            _gridController?.UpdateOnMove(dir);
        }

        private void OnDestroy()
        {
            if (_inputReader)
            {
                _inputReader.MoveClickedEvent -= OnMoveClicked;
            }
            _assetProvider?.Dispose();
        }
    }
}