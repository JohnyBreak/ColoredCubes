using _Project._Code.Configs;
using UnityEngine;

namespace _Project._Code
{
    public static class CameraPositioner
    {
        public static void UpdatePosition(Transform camera, GridSettingsConfigDto config, Vector3 gridPosition)
        {
            var gridWorldSize = (config.GridSize - 1) * config.Spacing;
            camera.position = new Vector3(0, gridWorldSize, -gridWorldSize);
            camera.LookAt(gridPosition);
        }
    }
}