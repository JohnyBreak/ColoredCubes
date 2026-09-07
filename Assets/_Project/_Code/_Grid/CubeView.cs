using UnityEngine;

namespace _Project._Code._Grid
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        public void UpdateMaterial(Material newMaterial)
        {
            if (!newMaterial)
            {
                return;
            }

            _meshRenderer.material = newMaterial;
        }
    }
}