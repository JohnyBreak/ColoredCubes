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
}