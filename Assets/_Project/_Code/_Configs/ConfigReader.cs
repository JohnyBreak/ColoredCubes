using _Project._Code._Common;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project._Code.Configs
{
    public class ConfigReader
    {
        private const string LogKey = "ConfigReader";
        private readonly JsonSerializerSettings m_JsonSettings;

        public ConfigReader()
        {
            m_JsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include
            };
        }

        public Result<T> Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                Debug.LogError($"[{LogKey}] JSON string is null or empty");
                return Result<T>.Fail();
            }

            try
            {
                var result = JsonConvert.DeserializeObject<T>(json, m_JsonSettings);
                
                if (result == null)
                {
                    Debug.LogError($"[{LogKey}] Deserialization returned null for type {typeof(T).Name}");
                    return Result<T>.Fail();
                }

                Debug.Log($"[{LogKey}] Successfully deserialized {typeof(T).Name}");
                return Result<T>.Success(result);
            }
            catch (JsonException jsonEx)
            {
                Debug.LogError($"[{LogKey}] JSON parsing error for {typeof(T).Name}: {jsonEx.Message}");
                return Result<T>.Fail();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[{LogKey}] Failed to deserialize {typeof(T).Name}: {ex.Message}");
                return Result<T>.Fail();
            }
        }
    }
}