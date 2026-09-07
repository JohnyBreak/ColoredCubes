using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using Object = UnityEngine.Object;

public sealed class AssetProvider : IDisposable
{
    private readonly HashSet<AsyncOperationHandle> _activeHandles = new();
    private readonly Dictionary<string, AsyncOperationHandle> _loadedHandles = new();
    private readonly Dictionary<string, int> _refCounts = new(); 
    
    public T LoadAssetSync<T>(string key) where T : Object
    {
        if (IncrementRefCount(key))
        {
            var cachedHandle = _loadedHandles[key];
            return cachedHandle.Result as T;
        }
        
        var handle = Addressables.LoadAssetAsync<T>(key);
        handle.WaitForCompletion();

        _activeHandles.Add(handle);
        _loadedHandles[key] = handle;

        return handle.Result;
    }
    
    public void Release(string key)
    {
        if (!_loadedHandles.TryGetValue(key, out var handle))
        {
            return;
        }
        
        if (!handle.IsValid())
        {
            _loadedHandles.Remove(key);
            _refCounts.Remove(key);
            return;
        }

        _refCounts[key]--;
        if (_refCounts[key] > 0)
        {
            return;
        }
        
        Addressables.Release(handle);
        _activeHandles.Remove(handle);
        _loadedHandles.Remove(key);
        _refCounts.Remove(key);
    }
    
    public void CleanUp()
    {
        foreach (var handle in _activeHandles)
        {
            if (handle.IsValid())
                Addressables.Release(handle);
        }
        _activeHandles.Clear();
        _loadedHandles.Clear();
    }
    
    private bool IncrementRefCount(string key)
    {
        if (_refCounts.TryGetValue(key, out int count))
        {
            _refCounts[key] = count + 1;
            return _loadedHandles.ContainsKey(key);
        }

        _refCounts[key] = 1;
        return false;
    }
    
    public void Dispose()
    {
        CleanUp();
    } 
}