using _Project._Code.Configs;
using UnityEngine;

namespace _Project._Code._Grid
{
    public class GridController
    {
        private readonly AssetProvider _assetProvider;
        private readonly GridData _data;
        private readonly SettingsConfigDto _settingsConfigDto;
        private GridSpawner _spawner;
        private readonly Vector3 _spawnPosition;
        private Visualizer _visualizer;
        private int _currentIndex = 0;
        
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
            _spawner = new GridSpawner(
                _assetProvider,
                _settingsConfigDto);
            
            _visualizer = new Visualizer(
                _data,
                _settingsConfigDto,
                _spawner.SpawnCubes(_spawnPosition));
            
            _visualizer.UpdateVisuals(_currentIndex);
        }
    }
}