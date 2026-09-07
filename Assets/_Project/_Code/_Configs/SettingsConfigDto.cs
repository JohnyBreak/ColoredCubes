using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project._Code.Configs
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class SettingsConfigDto
    {
        [SerializeField, JsonProperty("gridSize")]
        private int m_GridSize;

        [SerializeField, JsonProperty("spacing")]
        private float m_Spacing;

        [SerializeField, JsonProperty("colors")]
        private Dictionary<string, string> m_Colors;

        public int GridSize => m_GridSize;
        public float Spacing => m_Spacing;
        public IReadOnlyDictionary<string, string> Colors => m_Colors;
    }
}