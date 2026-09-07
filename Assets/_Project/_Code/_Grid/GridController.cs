using System;
using _Project._Code.Configs;
using InputSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project._Code._Grid
{
    public class GridController : IDisposable
    {
        private const string LogKey = "GridController";
        private readonly InputReader _inputReader;
        private readonly AssetProvider _assetProvider;
        private readonly GridData _data;
        private readonly GridSettingsConfigDto _gridSettingsConfigDto;
        private GridSpawner _spawner;
        private readonly Vector3 _spawnPosition;
        private Visualizer _visualizer;
        private int _currentIndex;

        public GridController(GridControllerParams constructParams)
        {
            _data = constructParams.Data;
            _assetProvider = constructParams.AssetProvider;
            _spawnPosition = constructParams.SpawnPosition;
            _gridSettingsConfigDto =  constructParams.GridSettingsConfigDto;
            _inputReader = constructParams.InputReader;
        }

        public void Init()
        {
            if (!_inputReader)
            {
                Debug.LogError($"[{LogKey}] _inputReader is null");
                return;
            }
            
            _spawner = new GridSpawner(
                _assetProvider,
                _gridSettingsConfigDto);
            
            _visualizer = new Visualizer(
                _data,
                _gridSettingsConfigDto,
                _spawner.SpawnCubes(_spawnPosition));

            RandomStartIndex();

            UpdateVisual();
            
            _inputReader.MoveClickedEvent += OnMoveClicked;
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

            UpdateOnMove(dir);
        }
        
        private void UpdateOnMove(InputDirection direction)
        {
            _currentIndex = _data.GetNeighbourIndex(_currentIndex, direction);
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            _visualizer.UpdateVisuals(_currentIndex);
        }

        private void RandomStartIndex()
        {
            _currentIndex = Random.Range(0, _data.Count);
        }

        public void Dispose()
        {
            _visualizer?.Dispose();
            
            if (_inputReader)
            {
                _inputReader.MoveClickedEvent -= OnMoveClicked;
            }
        }
    }
}