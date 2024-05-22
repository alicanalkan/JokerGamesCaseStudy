using System;
using UnityEngine;
using System.Collections;
using Object = System.Object;
using System.Collections.Generic;

namespace JokerGames.AssetManagement
{
    public interface IAssetManagementService 
    {
        T GetLoadedAssets<T>(string label) where T : UnityEngine.Object;
        void LoadAsset(string label, Action<Object> callback);
        IEnumerator LoadAllAssetsInParallel(IEnumerable<string> labels, Action<List<Object>> onAllLoaded);
        void UnloadAsset(string label);

        T Instantiate<T>(string label, Vector3 position, Quaternion rotation, Transform parentTransform)
            where T : MonoBehaviour, IAddressableTag;

        GameObject Instantiate(string label, Vector3 position, Quaternion rotation, Transform parentTransform);
        GameObject Instantiate(object loadedObject, Transform parentTransform, string prefabName);
        void Destroy(GameObject obj);
    }
    
    /// <summary>
    /// Every instantiatable asset should have a label.
    /// </summary>
    public interface IAddressableTag
    {
        string Label { get; set; }
    }

}
