using _Project._Code.Configs;
using UnityEngine;

namespace _Project._Code._Grid
{
    public class Visualizer
    {
        private const string LogKey = "Visualizer";
        private readonly GridData _data;
        private readonly GridSettingsConfigDto _gridSettingsConfigDto;
        private readonly MaterialsContainer _materialsContainer = new MaterialsContainer();
        private CubeView[] _cubeInstances;

        public Visualizer(
            GridData data,
            GridSettingsConfigDto gridSettingsConfigDto,
            CubeView[] grid)
        {
            _data = data;
            _gridSettingsConfigDto = gridSettingsConfigDto;
            _cubeInstances = grid;
        }

        public void UpdateVisuals(int currentIndex)
        {
            if (_cubeInstances == null)
            {
                Debug.LogError($"[{LogKey}] _cubeInstances == null");
                return;
            }

            var indexes = _data.GetArea(currentIndex, _gridSettingsConfigDto.GridSize);
            
            if (_cubeInstances.Length != indexes.Length)
            {
                Debug.LogError($"[{LogKey}] _cubeInstances.Length != indexes.Length");
                return;
            }
            
            for (int i = 0; i < indexes.Length; i++)
            {
                if (!TryGetColor(indexes[i], out var color))
                {
                    Debug.LogError($"[{LogKey}] no color for colorID {indexes[i]}");
                    continue;
                }
                
                var material = _materialsContainer.GetMaterial(indexes[i], color);
                _cubeInstances[i].UpdateMaterial(material);
            }
        }
        
        private bool TryGetColor(int digit, out Color color)
        {
            color = Color.white;

            if (_gridSettingsConfigDto.Colors == null ||
                !_gridSettingsConfigDto.Colors.TryGetValue(digit.ToString(), out var hexColor))
            {
                return false;
            }

            return ColorUtility.TryParseHtmlString(hexColor, out color);
        }
    }
}