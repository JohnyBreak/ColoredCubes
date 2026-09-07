using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project._Code._Grid
{
    public class MaterialsContainer : IDisposable
    {
        private readonly Dictionary<int, Material> m_CachedMaterials = new Dictionary<int, Material>();

        public Material GetMaterial(int colorCode, Color color)
        {
            if (!m_CachedMaterials.TryGetValue(colorCode, out var material))
            {
                material = CreateMaterial(color);
                m_CachedMaterials[colorCode] = material;
            }

            return material;
        }

        private Material CreateMaterial(Color color)
        {
            var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.color = color;
            return material;
        }

        private void Clear()
        {
            foreach (var material in m_CachedMaterials.Values)
            {
                if (material != null)
                {
                    Object.DestroyImmediate(material);
                }
            }

            m_CachedMaterials.Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}