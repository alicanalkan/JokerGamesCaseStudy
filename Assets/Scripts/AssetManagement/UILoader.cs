using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JokerGames.Engine;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace JokerGames.AssetManagement
{
    [Serializable]
    public class ViewRefPair
    {
        public AssetReference Reference;
        public ViewName Name;
    }
    
    /// <summary>
    /// Asset Keys
    /// </summary>
    public enum ViewName
    {
        DiceButton,
        StartGameButton,
        GamePanel,
        DicePopup,
        CustomDiceButton
    }

    public class UILoader: Singleton<UILoader>
    {
        [SerializeField] private List<ViewRefPair> viewRefPairs = new List<ViewRefPair>();
        
        /// <summary>
        /// Loading Addressables
        /// </summary>
        /// <param name="viewName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> LoadMonoView<T>(ViewName viewName) where T : MonoView
        {
            var assetRef = viewRefPairs.Find(item => item.Name == viewName).Reference;
            return await LoadAsset<T>(assetRef);
        }
        
        public async Task<T> LoadMonoView<T>(ViewName viewName,Transform parent) where T : MonoView
        {
            var assetRef = viewRefPairs.Find(item => item.Name == viewName).Reference;
            return await LoadAsset<T>(assetRef,parent);
        }

        private async Task<T> LoadAsset<T>(AssetReference assetRef) where T : MonoView
        {
            var handle = Addressables.InstantiateAsync(assetRef);
            var source = new TaskCompletionSource<T>();
            handle.Completed += h =>
            {
                var go = h.Result;
                var monoView = go.GetComponent<T>();
                monoView.Initialize();
                source.SetResult(monoView);
            };
            return await source.Task;
        }
        
        private async Task<T> LoadAsset<T>(AssetReference assetRef,Transform parent) where T : MonoView
        {
            var handle = Addressables.InstantiateAsync(assetRef,parent);
            var source = new TaskCompletionSource<T>();
            handle.Completed += h =>
            {
                var go = h.Result;
                var monoView = go.GetComponent<T>();
                monoView.Initialize();
                source.SetResult(monoView);
            };
            return await source.Task;
        }
    }
}
