using _Project._Code.Configs;
using InputSystem;
using UnityEngine;

namespace _Project._Code._Grid
{
    public readonly struct GridControllerParams
    {
        public readonly GridData Data;
        public readonly AssetProvider AssetProvider;
        public readonly GridSettingsConfigDto GridSettingsConfigDto;
        public readonly InputReader InputReader;
        public readonly Vector3 SpawnPosition;

        public GridControllerParams(GridData data,
            AssetProvider assetProvider,
            GridSettingsConfigDto gridSettingsConfigDto,
            InputReader inputReader,
            Vector3 spawnPosition)
        {
            Data = data;
            AssetProvider = assetProvider;
            SpawnPosition = spawnPosition;
            GridSettingsConfigDto = gridSettingsConfigDto;
            InputReader = inputReader;
        }
    }
}