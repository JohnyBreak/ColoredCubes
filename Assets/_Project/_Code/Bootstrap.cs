using _Project._Code._Grid;
using _Project._Code.Configs;
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
        private const string LogKey = "Bootstrap";
        [SerializeField] private InputReader _inputReader;
        
        private AssetProvider _assetProvider = new AssetProvider();
        private ConfigReader _configReader = new ConfigReader();
        private GridData _gridData;
        private SettingsConfigDto _settingsConfigDto;
        
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            LoadSettingsConfig();

            LoadGridData();

            if (_settingsConfigDto == null)
            {
                Debug.LogError($"[{LogKey}] _settingsConfigDto == null");
                return;
            }
            
            if (_gridData == null)
            {
                Debug.LogError($"[{LogKey}] _gridData == null");
                return;
            }

            var controller = new GridController(
                new GridControllerParams(
                    _gridData,
                    _assetProvider,
                    _settingsConfigDto,
                    Vector3.zero));
            
            controller.Init();
        }

        private void LoadGridData()
        {
            var config = _assetProvider.LoadAssetSync<TextAsset>(AddressablesNames.GridConfig);
            
            if (config == null)
            {
                Debug.LogError($"[{LogKey}] gridConfig == null");
                return;
            }
            
            if (string.IsNullOrEmpty(config.text))
            {
                Debug.LogError($"[{LogKey}] gridConfig text is null or empty");
                return;
            }

            var parser = new GridTextParser();
            var result = parser.Parse(config.text);
            
            if (!result.IsSuccess)
            {
                Debug.LogError($"[{LogKey}] GridData is not parsed");
                return;
            }

            _gridData = result.Object;
        }

        private void LoadSettingsConfig()
        {
            var config = _assetProvider.LoadAssetSync<TextAsset>(AddressablesNames.SettingsConfig);
            
            if (config == null)
            {
                Debug.LogError($"[{LogKey}] settingsConfig == null");
                return;
            }
            if (string.IsNullOrEmpty(config.text))
            {
                Debug.LogError($"[{LogKey}] settingsConfig text is null or empty");
                return;
            }

            var result = _configReader.Deserialize<SettingsConfigDto>(config.text);

            if (!result.IsSuccess)
            {
                Debug.LogError($"[{LogKey}] SettingsConfigDto is not deserialized");
                return;
            }

            _settingsConfigDto = result.Object;
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