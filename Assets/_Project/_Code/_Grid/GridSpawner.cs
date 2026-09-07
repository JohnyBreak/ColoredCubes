using _Project._Code.Configs;
using UnityEngine;

namespace _Project._Code._Grid
{
    public class GridSpawner
    {
        private readonly AssetProvider _assetProvider;
        private readonly GridSettingsConfigDto _gridSettingsConfigDto;

        public GridSpawner(AssetProvider assetProvider,
            GridSettingsConfigDto gridSettingsConfigDto)
        {
            _assetProvider = assetProvider;
            _gridSettingsConfigDto = gridSettingsConfigDto;
        }

        public CubeView[] SpawnCubes(Vector3 originPosition)
        {
            var prefab = _assetProvider.LoadAssetSync<GameObject>(AddressablesNames.CubePrefab);
            
            var cubesParent = new GameObject("Cubes");
            cubesParent.transform.position = originPosition;

            var totalCubes = _gridSettingsConfigDto.GridSize * _gridSettingsConfigDto.GridSize;
            var cubeInstances = new CubeView[totalCubes];

            var halfGrid = (_gridSettingsConfigDto.GridSize - 1) * 0.5f;
            var spacing = _gridSettingsConfigDto.Spacing;

            for (var i = 0; i < totalCubes; i++)
            {
                var x = i % _gridSettingsConfigDto.GridSize;
                var z = (_gridSettingsConfigDto.GridSize - 1) - (i / _gridSettingsConfigDto.GridSize);

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