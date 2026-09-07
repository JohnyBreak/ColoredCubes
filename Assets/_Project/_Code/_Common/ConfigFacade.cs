using _Project._Code._Grid;
using _Project._Code.Configs;
using UnityEngine;

namespace _Project._Code._Common
{
    public class ConfigFacade
    {
        private const string LogKey = "ConfigFacade";
        private AssetProvider _assetProvider;
        private ConfigReader _configReader = new ConfigReader();
        private GridData _gridData;
        private GridSettingsConfigDto _gridSettingsConfigDto;

        public GridData GridData => _gridData;
        public GridSettingsConfigDto GridSettingsConfigDto => _gridSettingsConfigDto;
        
        public ConfigFacade(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Init()
        {
            LoadSettingsConfig();

            if (_gridSettingsConfigDto == null)
            {
                return;
            }
            
            LoadGridData();
        }
        
        public bool IsValid()
        {
            if (_gridSettingsConfigDto == null)
            {
                Debug.LogError($"[{LogKey}] _settingsConfigDto == null");
                return false;
            }
            
            if (_gridData == null)
            {
                Debug.LogError($"[{LogKey}] _gridData == null");
                return false;
            }
            
            return true;
        }
        
        private void LoadGridData()
        {
            var textResult = GetConfigText(AddressablesNames.GridConfig);

            if (!textResult.IsSuccess)
            {
                return;
            }

            var parser = new GridTextParser();
            var result = parser.Parse(textResult.Object);
            
            if (!result.IsSuccess)
            {
                Debug.LogError($"[{LogKey}] GridData is not parsed");
                return;
            }

            _gridData = result.Object;
        }

        private void LoadSettingsConfig()
        {
            var textResult = GetConfigText(AddressablesNames.SettingsConfig);

            if (!textResult.IsSuccess)
            {
                return;
            }

            var result = _configReader.Deserialize<GridSettingsConfigDto>(textResult.Object);

            if (!result.IsSuccess)
            {
                Debug.LogError($"[{LogKey}] SettingsConfigDto is not deserialized");
                return;
            }

            _gridSettingsConfigDto = result.Object;
        }

        private Result<string> GetConfigText(string configKey)
        {
            var config = _assetProvider.LoadAssetSync<TextAsset>(configKey);
            
            if (config == null)
            {
                Debug.LogError($"[{LogKey}] config with key {configKey} is null");
                return Result<string>.Fail();
            }
            
            if (string.IsNullOrEmpty(config.text))
            {
                Debug.LogError($"[{LogKey}] config with key {configKey} text is null or empty");
                return Result<string>.Fail();
            }

            return Result<string>.Success(config.text);
        }
    }
}