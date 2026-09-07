using _Project._Code.Configs;
using UnityEngine;

namespace _Project._Code._Grid
{
    public readonly struct GridControllerParams
    {
        public readonly GridData Data;
        public readonly AssetProvider AssetProvider;
        public readonly SettingsConfigDto SettingsConfigDto;
        public readonly Vector3 SpawnPosition;

        public GridControllerParams(GridData data,
            AssetProvider assetProvider,
            SettingsConfigDto settingsConfigDto,
            Vector3 spawnPosition)
        {
            Data = data;
            AssetProvider = assetProvider;
            SpawnPosition = spawnPosition;
            SettingsConfigDto = settingsConfigDto;
        }
    }

    public class GridController
    {
        private const string LogKey = "GridController";
        private readonly GridData _data;
        private readonly AssetProvider _assetProvider;
        private readonly SettingsConfigDto _settingsConfigDto;
        private readonly MaterialsContainer _materialsContainer = new MaterialsContainer();
        private GridSpawner _spawner;
        
        private CubeView[] _cubeInstances;
        private readonly Vector3 _spawnPosition;

        private int _currentIndex = 0; //upper left corner
        // move grid on inputs

        public GridController(GridControllerParams constructParams)
        {
            _data = constructParams.Data;
            _assetProvider = constructParams.AssetProvider;
            _spawnPosition = constructParams.SpawnPosition;
            _settingsConfigDto =  constructParams.SettingsConfigDto;
        }

        public void Init()
        {
            _spawner = new GridSpawner(_assetProvider, _settingsConfigDto);
            _cubeInstances = _spawner.SpawnCubes(_spawnPosition);
            UpdateVisual();
        }


        private void UpdateVisual()
        {
            var result = _data.GetIndexes(_currentIndex, _settingsConfigDto.GridSize);

            if (!result.IsSuccess)
            {
                Debug.LogError($"[{LogKey}] no indexes from data");
                return;
            }

            for (int i = 0; i < result.Object.Length; i++)
            {
                if (!TryGetColor(result.Object[i], out var color))
                {
                    Debug.LogError($"[{LogKey}] no color for index {result.Object[i]}");
                    continue;
                }
                
                var material = _materialsContainer.GetMaterial(result.Object[i], color);
                _cubeInstances[i].UpdateMaterial(material);
            }
        }
        
        private bool TryGetColor(int digit, out Color color)
        {
            color = Color.white;

            if (_settingsConfigDto.Colors == null ||
                !_settingsConfigDto.Colors.TryGetValue(digit.ToString(), out var hexColor))
            {
                return false;
            }

            return ColorUtility.TryParseHtmlString(hexColor, out color);
        }
    }
}