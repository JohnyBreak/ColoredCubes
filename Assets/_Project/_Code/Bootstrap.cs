using _Project._Code._Common;
using _Project._Code._Grid;
using InputSystem;
using UnityEngine;

namespace _Project._Code
{
    public class Bootstrap : MonoBehaviour
    {
        private const string LogKey = "Bootstrap";
        
        [SerializeField] private Transform _camera;
        [SerializeField] private Vector3 _gridSpawnPosition = Vector3.zero;
        
        private readonly AssetProvider _assetProvider = new AssetProvider();
        private InputReader _inputReader;
        private ConfigFacade _configFacade;
        private GridController _gridController;
        
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            CreateInputReader();
            
            _configFacade = new ConfigFacade(_assetProvider);
            
            _configFacade.Init();

            if (!_configFacade.IsValid())
            {
                return;
            }

            SetupCameraPosition();
            
            _gridController = new GridController(
                new GridControllerParams(
                    _configFacade.GridData,
                    _assetProvider,
                    _configFacade.GridSettingsConfigDto,
                    _inputReader,
                    _gridSpawnPosition));
            
            _gridController.Init();
        }

        private void SetupCameraPosition()
        {
            if(!_camera)
            {
                Debug.LogError($"[{LogKey}] _camera is null");
                return;
            }
            
            CameraPositioner.UpdatePosition(
                _camera, 
                _configFacade.GridSettingsConfigDto, 
                _gridSpawnPosition);
        }

        private void CreateInputReader()
        {
            _inputReader = new GameObject("InputReader").AddComponent<InputReader>();
            _inputReader.transform.parent = transform.parent;
        }

        private void OnDestroy()
        {
            _gridController?.Dispose();
            _assetProvider?.Dispose();
        }
    }
}