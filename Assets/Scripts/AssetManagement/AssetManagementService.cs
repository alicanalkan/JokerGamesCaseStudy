using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using Object = System.Object;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace JokerGames.AssetManagement
{
    public class AssetManagementService : IAssetManagementService
    {
        /// <summary>
        /// Cache Assets
        /// </summary>
        private readonly Dictionary<string, AsyncOperationHandle<Object>> _assetHandles = new();
        private readonly Dictionary<string, Object> _cachedLoadedAssets = new();
        private readonly Dictionary<string, List<Action<Object>>> _assetCallbacks = new();
        private readonly Dictionary<string, int> _assetReferenceCount = new();
        
        /// <summary>
        /// Paralel Asset Loading for  fast loading
        /// </summary>
        /// <param name="labels"></param>
        /// <param name="onAllLoaded"></param>
        /// <returns></returns>
        public IEnumerator LoadAllAssetsInParallel(IEnumerable<string> labels, Action<List<Object>> onAllLoaded)
        {
            int assetsToLoadCount = labels.Count();
            int loadedAssetsCount = 0;
            List<Object> loadedAssets = new List<Object>();

            foreach (var label in labels)
            {
                LoadAsset(label, (asset) =>
                {
                    if (asset != null)
                    {
                        loadedAssets.Add(asset);
                    }

                    loadedAssetsCount++;
                });
            }

            // Wait for all assets to be loaded
            yield return new WaitUntil(() => loadedAssetsCount == assetsToLoadCount);

            onAllLoaded?.Invoke(loadedAssets);
        }

        /// <summary>
        /// Get cached assets
        /// </summary>
        /// <param name="label"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetLoadedAssets<T>(string label) where T : UnityEngine.Object
        {
            if (_cachedLoadedAssets[label] != null) return _cachedLoadedAssets[label] as T;

            Debug.LogWarning($"Tried to load asset that isn't loaded: {label}");
            return null;
        }

        /// <summary>
        /// Get Loaded asset
        /// </summary>
        /// <param name="label"></param>
        /// <param name="callback"></param>
        public void LoadAsset(string label, Action<Object> callback)
        {
            if(string.IsNullOrEmpty(label))
            {
                Debug.LogError($"Tried to load asset with null label.");
                return;
            }
            
            if (_cachedLoadedAssets.TryGetValue(label, out var asset))
            {
                callback?.Invoke(asset);
                return;
            }

            if (!_assetCallbacks.ContainsKey(label))
            {
                _assetCallbacks[label] = new List<Action<Object>>();
            }

            _assetCallbacks[label].Add(callback);

            if (!_assetHandles.ContainsKey(label))
            {
                var handle = Addressables.LoadAssetAsync<Object>(label);
                handle.Completed += op => OnAssetLoadCompleted(label, op);
                _assetHandles[label] = handle;
            }
        }

        /// <summary>
        /// Instantiate Loaded GameObjects
        /// </summary>
        /// <param name="label"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parentTransform"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Instantiate<T>(string label, Vector3 position, Quaternion rotation, Transform parentTransform)
            where T : MonoBehaviour, IAddressableTag
        {
            // Check if the asset exists in cache
            if (_cachedLoadedAssets.TryGetValue(label, out var asset))
            {
                GameObject instance =
                    UnityEngine.Object.Instantiate(asset as GameObject, position, rotation, parentTransform);
                
                if (!instance.TryGetComponent(out T labelComponent))
                {
                    labelComponent = instance.AddComponent<T>();
                    labelComponent.Label = label;
                }
                
                // Increment reference count for the asset
                if (_assetReferenceCount.ContainsKey(label))
                {
                    _assetReferenceCount[label]++;
                }
                else
                {
                    _assetReferenceCount[label] = 1;
                }

                return labelComponent; // return the added component
            }

            Debug.LogWarning(
                $"Tried to instantiate a prefab using an asset that isn't loaded: {label}");
            return null;
        }

        /// <summary>
        /// Already loaded GameObject with transform and label.
        /// </summary>
        /// <param name="loadedObject">Object you have loaded</param>
        /// <param name="parentTransform">Where you want to instantiate as child</param>
        /// <param name="prefabName">Addressable Tag</param>
        /// <returns></returns>
        public GameObject Instantiate(object loadedObject, Transform parentTransform, string prefabName)
        {
            GameObject instance = UnityEngine.Object.Instantiate(loadedObject as GameObject, parentTransform);

            if (string.IsNullOrEmpty(prefabName))
            {
                Debug.LogError($"{instance.name} doesn't have an Addressable label.");
            }
            else
            {
                if (_assetReferenceCount.ContainsKey(prefabName))
                {
                    _assetReferenceCount[prefabName]++;
                }
                else
                {
                    _assetReferenceCount[prefabName] = 1;
                }
            }

            return instance;
        }

        public GameObject Instantiate(string label, Vector3 position, Quaternion rotation, Transform parentTransform)
        {
            // Check if the asset exists in cache
            if (_cachedLoadedAssets.TryGetValue(label, out var asset))
            {
                GameObject instance =
                    UnityEngine.Object.Instantiate(asset as GameObject, position, rotation, parentTransform);

                // Increment reference count for the asset
                if (_assetReferenceCount.ContainsKey(label))
                {
                    _assetReferenceCount[label]++;
                }
                else
                {
                    _assetReferenceCount[label] = 1;
                }

                return instance;
            }

            Debug.LogWarning(
                $"Tried to instantiate a prefab using an asset that isn't loaded: {label}");

            return null;
        }

        public void Destroy(GameObject obj)
        {
            IAddressableTag labelComponent = obj.GetComponent<IAddressableTag>();

            if (labelComponent == null)
            {
                Debug.LogWarning(
                    "Attempted to destroy object that doesn't have an Addressable label.");
                return;
            }

            string label = labelComponent.Label;

            if (!_cachedLoadedAssets.ContainsKey(label))
            {
                Debug.LogWarning(
                    $"Attempted to destroy object of an asset that isn't loaded: {label}");
                return;
            }

            // Decrement reference count for the asset
            if (_assetReferenceCount.ContainsKey(label))
            {
                _assetReferenceCount[label]--;

                if (_assetReferenceCount[label] <= 0)
                {
                    UnloadAsset(label);
                    _assetReferenceCount.Remove(label);
                }
            }
            else
            {
                Debug.LogWarning(
                    $"Destroyed object had no recorded reference for asset: {label}");
            }

            UnityEngine.Object.Destroy(obj);
        }

        private void OnAssetLoadCompleted(string label, AsyncOperationHandle<Object> op)
        {
            _assetHandles.Remove(label);

            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Asset loaded successfully: {label}");

                _cachedLoadedAssets.Add(label, op.Result);

                foreach (var callback in _assetCallbacks[label])
                {
                    callback?.Invoke(op.Result);
                }

                _assetCallbacks.Remove(label);
            }
            else
            {
                Debug.LogError($"Asset load failed: {label}");
                // Handle error callbacks or notifications here if needed
            }
        }

        public void UnloadAsset(string label)
        {
            if (!_cachedLoadedAssets.ContainsKey(label))
            {
                Debug.LogWarning($"Attempted to unload asset that isn't loaded: {label}");
                return;
            }

            Debug.Log($"Unloading asset: {label}");

            Addressables.Release(_cachedLoadedAssets[label]);
            _cachedLoadedAssets.Remove(label);
        }
    }
}
