using _Project._Code.Configs;
using UnityEngine;

namespace _Project._Code._Grid
{
    public class GridSpawner
    {
        private readonly AssetProvider _assetProvider;
        private readonly SettingsConfigDto _settingsConfigDto;

        public GridSpawner(AssetProvider assetProvider,
            SettingsConfigDto settingsConfigDto)
        {
            _assetProvider = assetProvider;
            _settingsConfigDto = settingsConfigDto;
        }

        public CubeView[] SpawnCubes(Vector3 originPosition)
        {
            var prefab = _assetProvider.LoadAssetSync<GameObject>(AddressablesNames.CubePrefab);
            
            var cubesParent = new GameObject("Cubes");
            cubesParent.transform.position = originPosition;

            var totalCubes = _settingsConfigDto.GridSize * _settingsConfigDto.GridSize;
            var cubeInstances = new CubeView[totalCubes];

            var halfGrid = (_settingsConfigDto.GridSize - 1) * 0.5f;
            var spacing = _settingsConfigDto.Spacing;

            for (var i = 0; i < totalCubes; i++)
            {
                var x = i % _settingsConfigDto.GridSize;
                var z = (_settingsConfigDto.GridSize - 1) - (i / _settingsConfigDto.GridSize);

                var cubeInstance = Object.Instantiate(prefab, cubesParent.transform);
                cubeInstance.name = $"Cube: {x}x{z}";

                var worldPos = new Vector3(
                    (x - halfGrid) * spacing,
                    0f,
                    (z - halfGrid) * spacing
                );

                cubeInstance.transform.localPosition = worldPos;

                cubeInstances[i] = cubeInstance.GetComponent<CubeView>();
            }
            
            _assetProvider.Release(AddressablesNames.CubePrefab);
            return cubeInstances;
        }
    }
}