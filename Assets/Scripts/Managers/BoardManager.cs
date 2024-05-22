using DG.Tweening;
using JokerGames.AssetManagement;
using JokerGames.Data;
using JokerGames.Dice;
using JokerGames.Managers;
using JokerGames.Scripts.Camera;
using JokerGames.Services;
using JokerGames.UI.Buttons;
using JokerGames.UI.Popups;
using Unity.Mathematics;
using UnityEngine;
using CustomDiceButton = JokerGames.UI.Buttons.CustomDiceButton;
using Random = UnityEngine.Random;

namespace JokerGames
{
    public class BoardManager : MonoBehaviour
    {
        private BoardData _boardData;
        private GameManager _gameManager;
        private ServiceLocator _serviceLocator;
        private Tile[] _tiles;
        private GameObject Pawn;
        private int CurrentTile = 0;
        private DiceRollVisualizer _diceRollVisualizer;
        private bool _playerMovementInProgress;
        private UIManager _UIManager;

        [SerializeField] private CameraFollow _camera;

        private IAssetManagementService assetManagementService;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _serviceLocator = _gameManager._serviceLocator;
            _UIManager = UIManager.Instance;
            _boardData = _gameManager.GetBoardData();
            assetManagementService = _serviceLocator.GetService<IAssetManagementService>();

            InstantiateBoard();

            CreateBoardEnvironment();

            InstantiatePawn();

            InstantiateDice();
            
            ShowDiceButtons();
        }

        /// <summary>
        ///  LoadBoard Objects
        /// </summary>
        void InstantiateBoard()
        {
            TileData[] tilesData = _boardData.TileData;
            _tiles = new Tile[tilesData.Length];
            
            for (var index = 0; index < tilesData.Length; index++)
            {
                var tile = tilesData[index];
                string assetTag = tile.tileType == TileType.RegularTile
                    ? _boardData.tileAssetTag
                    : _boardData.cornerTileAssetTag;

              
                var tileObject = assetManagementService.Instantiate<Tile>(assetTag,
                    new Vector3(tile.positionX, tile.positionY + 10, tile.positionZ),
                    Quaternion.Euler(0, tile.rotation, 0), this.transform);

                _tiles[index] = tileObject;
                
                _tiles[index].Position= new Vector3(tile.positionX, tile.positionY, tile.positionZ);
                
                if (tile.tileType == TileType.RegularTile)
                {
                    tileObject.SetApple(tilesData[index].apple);
                    tileObject.SetPear(tilesData[index].pear);
                    tileObject.SetStrawberrie(tilesData[index].strawberry);
                }
                
                tileObject.transform.DOMoveY(tile.positionY, 0.5f).SetDelay(index / 10f);
            }
        }

        /// <summary>
        /// Game Element Instantiate
        /// </summary>
        void InstantiatePawn()
        {
            Pawn = assetManagementService.Instantiate("Pawn", _tiles[CurrentTile].Position, quaternion.identity,
                transform);
        }

        void InstantiateDice()
        {
            _diceRollVisualizer =
                assetManagementService.Instantiate<DiceRollVisualizer>("Dice", _tiles[CurrentTile].Position, quaternion.identity,
                    transform);
        }

        /// <summary>
        /// Board Environment ToDO should be on Board Data
        /// </summary>
        void CreateBoardEnvironment()
        {
            float centerX = (_boardData.boardWidth - 1) / 2f ;
            float centerZ = (_boardData.boardHeight - 1) / 2f;
            var rectangleCenter =  new Vector3(centerX, 0, centerZ);
            
            var mountainObject = assetManagementService.Instantiate("Mountain",rectangleCenter,Quaternion.identity, null);
            mountainObject.transform.localScale = Vector3.zero;

            mountainObject.transform.DOScale(0.04f, 0.5f);
            
            assetManagementService.Instantiate("Water",rectangleCenter,Quaternion.identity, null);

        }
        

        /// <summary>
        /// Play Process for given dice values
        /// </summary>
        /// <param name="diceOne"></param>
        /// <param name="diceTwo"></param>
        private async void ProcessPlayerMovement(uint diceOne,uint diceTwo)
        {

            _diceRollVisualizer.transform.position = _tiles[CurrentTile].Position;
            
            var diceRoll = await _diceRollVisualizer.VisualizeDiceRoll(diceOne,diceTwo);

            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < diceRoll; i++)
            {
                Vector3[] path = new Vector3[3];
                path[0] = _tiles[CurrentTile].Position;
                var center = Vector3.Lerp(_tiles[CurrentTile].Position, _tiles[GetNextTileIndex()].Position, .5f);
                
                path[1] = new Vector3(center.x,center.y + 1,center.z);
                path[2] = _tiles[GetNextTileIndex()].Position;
                
                sequence.Append(Pawn.transform.DOPath(path, .5f));
                IncreaseCurrentTile();
            }
            
            await _camera.SetTargetTransform(Pawn.transform);
            
            sequence.OnKill((() =>
            {
                _playerMovementInProgress = false;
                _gameManager.IncreaseApple(_tiles[CurrentTile].Apple);
                _gameManager.IncreasePear(_tiles[CurrentTile].Pear);
                _gameManager.IncreaseStrawBerry(_tiles[CurrentTile].Strawberrie);
            }));
            // await Utils.Utils.WaitUntil((() => !sequence.IsPlaying()));
            
        }

        /// <summary>
        /// Increase Current Tile Index
        /// </summary>
        private void IncreaseCurrentTile()
        {
            if (CurrentTile + 1 == _tiles.Length)
            {
                CurrentTile = 0;
            }
            else
            {
                CurrentTile++;
            }
        }

        /// <summary>
        /// Get Next tile Index
        /// </summary>
        /// <returns></returns>
        private int GetNextTileIndex()
        {
            int index;
            if (CurrentTile + 1 == _tiles.Length)
            {
                index = 0;
            }
            else
            {
                index = CurrentTile + 1;
            }
            return index;
        }
        
        
        /// <summary>
        /// Load Dice buttons
        /// </summary>
        private void ShowDiceButtons()
        {
            _UIManager.ShowButton(new DiceButton.DiceButtonDefinition(OnDiceClicked));
            _UIManager.ShowButton(new CustomDiceButton.CustomDiceButtonDefinition(CustomDiceClicked));
        }

        /// <summary>
        /// Dice Click Events
        /// </summary>
        public void CustomDiceClicked()
        {
            if (!_playerMovementInProgress)
            {
                _UIManager.ShowPopup(new DicePopup.DicePopupDefinition(RollCustomDice));
            }
            
        }

        private void RollCustomDice(int diceOne,int diceTwo)
        {
            if (!_playerMovementInProgress)
            {
                _playerMovementInProgress = true;
                ProcessPlayerMovement((uint)diceOne,(uint)diceTwo);
            }
        }
        private void OnDiceClicked()
        {
            if (!_playerMovementInProgress)
            {
                _playerMovementInProgress = true;
                ProcessPlayerMovement((uint)Random.Range(1, 6),(uint)Random.Range(1, 6));
            }
        }
    }
}
