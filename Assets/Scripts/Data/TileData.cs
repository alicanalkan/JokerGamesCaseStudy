using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JokerGames.Data
{
    [Serializable]
    public class TileData
    {
        // Tile type
        public TileType tileType;
        //Position Values
        public int positionX;
        public int positionY;
        public int positionZ;
        public int rotation;
        
        // Items

        public int apple;
        public int pear;
        public int strawberry;

    }
}
