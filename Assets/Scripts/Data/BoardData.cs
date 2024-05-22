using System.Collections.Generic;
using UnityEngine;

namespace JokerGames.Data
{
    
    [CreateAssetMenu(menuName = "Data",fileName = "BoardData")]
    public class BoardData : ScriptableObject
    {
        public int boardWidth;
        public int boardHeight;
        public string tileAssetTag;
        public string cornerTileAssetTag;
        
        public TileData[] TileData;

        
        /// <summary>
        /// Todo update with level manager
        /// </summary>
        public void GenerateBoardData()
        {
            var tileCount =2 * (boardWidth + boardHeight) - 4;
            TileData = new TileData[tileCount];

            int cornerRotation = 180;
            
   
            
            for (int i = 0; i < tileCount; i++)
            {
                int x, y, z, rotation;
                y = 0;
                
                if (i < boardWidth) // Bottom
                {
                    x = i;
                    z = 0;
                    rotation = 0;
                }
                else if (i < boardWidth + boardHeight - 1) // Right
                {
                    x = boardWidth - 1;
                    z = i - boardWidth + 1;
                    rotation = -90;
                }
                else if (i < 2 * boardWidth + boardHeight - 2) // Up
                {
                    x = 2 * boardWidth + boardHeight - 3 - i;
                    z = boardHeight - 1;
                    rotation = 180;
                }
                else // Left
                {
                    x = 0;
                    z = 2 * (boardWidth + boardHeight) - 4 - i;
                    rotation = 90;
                }
                
                bool isCornerPoint = (x == 0 && (z == 0 || z == boardHeight - 1)) ||
                                     (x == boardWidth - 1 && (z == 0 || z == boardHeight - 1));
                
                if (isCornerPoint)
                {
                    cornerRotation -= 90;
                    TileData[i] = new TileData()
                    {
                        tileType = TileType.CornerTile,
                        positionX = x,
                        positionY = y,
                        positionZ = z,
                        rotation = cornerRotation,
                        apple = Random.Range(0,9),
                        pear = Random.Range(0,9),
                        strawberry = Random.Range(0,9),
                    };
                    
                }
                else
                {
                    TileData[i] = new TileData()
                    {
                        tileType = TileType.RegularTile,
                        positionX = x,
                        positionY = y,
                        positionZ = z,
                        rotation = rotation,
                        apple = Random.Range(0,9),
                        pear = Random.Range(0,9),
                        strawberry = Random.Range(0,9),
                    };
                }
                
                
            }
            
        }
    }
}
