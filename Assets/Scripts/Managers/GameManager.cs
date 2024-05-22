using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JokerGames.AssetManagement;
using JokerGames.Data;
using JokerGames.Engine;
using JokerGames.Services;
using JokerGames.UI.Buttons;
using JokerGames.UI.Panels;


namespace JokerGames.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private UIManager _uiManager;
        private PlayerData _playerData;
        public ServiceLocator _serviceLocator { get; set; }
        public BoardData _currentBoardData { get; private set; }
        async void  Start()
        {
            _uiManager = UIManager.Instance;

            InitializeServices();
            
            InitializePlayerData();
            
            LoadLevelData();
            
            await InitializeUI();
        }

        /// <summary>
        /// Game Services Initialize
        /// </summary>
        private void InitializeServices()
        {
            _serviceLocator = new ServiceLocator();
            
            IAssetManagementService assetManagementService = new AssetManagementService();
            _serviceLocator.RegisterService(assetManagementService);
            
            IJsonManagementService jsonManagementService = new JsonManagementService();
            _serviceLocator.RegisterService(jsonManagementService);

            ISaveService saveService = new SaveService(jsonManagementService);
            _serviceLocator.RegisterService(saveService);
        }

        /// <summary>
        /// Preload Ui Elements
        /// </summary>
        private async Task InitializeUI()
        {
           await UIManager.Instance.CreateButton(new StartButton.StartButtonDefinition());
           await UIManager.Instance.CreatePanel(new GamePanel.InventoryPanel(_playerData.Apples,_playerData.Pears,_playerData.Strawberries));
        }

        private async Task ShowUI()
        {
            await UIManager.Instance.ShowButton(new StartButton.StartButtonDefinition());
            await UIManager.Instance.ShowPanel(new GamePanel.InventoryPanel(_playerData.Apples,_playerData.Pears,_playerData.Strawberries));
        }

        /// <summary>
        /// Player Data Loading
        /// </summary>
        private void InitializePlayerData()
        {
            ISaveService saveService = _serviceLocator.GetService<ISaveService>();

            _playerData = saveService.Load();
        }

        /// <summary>
        /// Level Data Loading
        /// </summary>
        private void LoadLevelData()
        {
            var assetManagementService = _serviceLocator.GetService<IAssetManagementService>();
            assetManagementService.LoadAsset("Level1",OnLevelDataLoaded);
        }

        private void OnLevelDataLoaded(object data)
        {
            var assetManagementService = _serviceLocator.GetService<IAssetManagementService>();
            List<string> assetList = new List<string>();
            _currentBoardData = data as BoardData;
            
            var tileAssetTag = _currentBoardData.tileAssetTag;
            var cornerTileAssetTag = _currentBoardData.cornerTileAssetTag;
            
            assetList.Add(tileAssetTag);
            assetList.Add(cornerTileAssetTag);
            
            //todo add level data
            assetList.Add("Mountain");
            assetList.Add("Water");
            assetList.Add("Pawn");
            assetList.Add("Dice");

            StartCoroutine(assetManagementService.LoadAllAssetsInParallel(assetList, OnBoardLoaded));

        }

        private void OnBoardLoaded(List<object> list)
        {
            ShowUI();
        }

        public BoardData GetBoardData()
        {
            return _currentBoardData;
        }

        
        /// <summary>
        /// Increase Player Inventories
        /// </summary>
        /// <param name="count"></param>
        public void IncreaseStrawBerry(int count)
        {
            _playerData.Strawberries += count;
            UIManager.Instance.GamePanel.UpdateStrawberry( _playerData.Strawberries);
        }
        
        public void IncreaseApple(int count)
        {
            _playerData.Apples += count;
            UIManager.Instance.GamePanel.UpdateApple( _playerData.Apples);
        }
        
        public void IncreasePear(int count)
        {
            _playerData.Pears += count;
            UIManager.Instance.GamePanel.UpdatePear( _playerData.Pears);
        }

        private void OnApplicationQuit()
        {
            _serviceLocator.GetService<ISaveService>().Save(_playerData);
            
        }
    }
}
